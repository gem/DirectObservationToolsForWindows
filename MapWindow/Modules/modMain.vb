'********************************************************************************************************
'File Name: modMain.vb
'Description: Entry point for MapWindow
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'1/12/2005 - new entry point for MapWindow (dpa)
'1/31/2005 - minor modifications. (dpa)
'2/2/2005  - commented out redundant call to frmMain.InitializeVars() in LoadMainForm (jlk)
'2/3/2005  - moved display of WelcomeScreen (jlk)
'7/29/2005 - added a exception handler class(Lailin Chen)
'7/29/2005 - added a event handler to the Application object to handle uncaught exceptions(Lailin Chen)
'9/22/2005 - added function to send welcome screen message to a configured plug-in
'12/21/2005 - Added ability to load a layer from the command line. (cdm)
'2/18/2008 - minor modifications (lcw)
'2/18/08 - referenced newest version of library (2.2.x); global changes to method references required (lcw)
'3/31/2008 - added ability to load language settings from the configuration file (Jiri kadlec)
'08/5/2008 - changed the default location of configuration files, moved language settings to a separate
'             file, moved initialization of the Script form after the LoadCulture() function (jk)
'26/5/2008 - Fixed configuration file reading behavior (jk)
'********************************************************************************************************

Imports System.Threading
Imports System.Security.Principal

Module modMain
    'Global friend variables go here...
    Friend gemdb As GEMDatabase
    Friend memoryShape As MapWinGIS.Shapefile

    Friend frmMain As MapWindowForm
    Friend ProjInfo As New XmlProjectFile   'stores info about the current MapWindow project
    Friend AppInfo As New cAppInfo          'stores info about the current MapWindow configuration 
    Friend Scripts As frmScript             'the Scripts form is initialized after executing LoadCulture()
    Friend BinFolder As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location)
    Public g_error As String 'Last error message
    Public g_ShowDetailedErrors As Boolean = True
    Public g_KillList As New ArrayList()
    Friend g_SyncPluginMenuDefer As Boolean = False
    Public ProjectionDB As New MapWindow.Controls.Projections.ProjectionDatabase

    ' command line arguments
    Public M_RESET_DEFAULTS As Boolean = False  ' config settings will be restored to defaults
    Public M_NOPLUGINS As Boolean = False       ' no plugins apart from application one will be loded
    Public M_NOLOGO As Boolean = False          ' no logo will be shown
    Public M_NOERRORS As Boolean = False        ' custom exception handler will be switched off
    Public M_NOWELCOME As Boolean = False      ' no welcome window will be diplayed
    Public M_OLDSYMBOLOGY As Boolean = False    ' old legend/legend editor/symbology for shapefiles are to be used
    'Public M_ADVANCED_MODE As Boolean = False   ' user promting when loading options will be used as default

    ''' <summary>
    '''  Entry point for MapWindow 
    ''' </summary>
    Public Sub Main()

        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Dim isElevated As Boolean = principal.IsInRole(WindowsBuiltInRole.Administrator)
        If Not isElevated Then
            MessageBox.Show("The application needs to run as Administrator to be guaranteed to work effectively. Please right click the executable file and run as administrator. Alternatively alter the User Account Control in the Control Panel.", "Run As Admin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        ' Analyzing command line arguments
        Dim settings As String = Microsoft.VisualBasic.Command().ToLower()
        If settings.Contains("/resettodefaults") Then M_RESET_DEFAULTS = True
        If settings.Contains("/noplugins") Then M_NOPLUGINS = True
        If settings.Contains("/noerrors") Then M_NOERRORS = True
        If settings.Contains("/nologo") Then M_NOLOGO = True
        If settings.Contains("/nowelcome") Then M_NOWELCOME = True
        If settings.Contains("/oldsymbology") Then M_OLDSYMBOLOGY = True
        'If settings.Contains("/advanced") Then M_ADVANCED_MODE = True

        'Chris M 11/11/2006 - Please leave thread exception handler at very
        'beginning, so users won't get "Send to Microsoft" ugli crashes when
        'missing DLLs etc

        ' Uncaught app exceptions will be hadled by CustomExceptionHandler class
        Dim eh As CustomExceptionHandler = New CustomExceptionHandler
        If Not M_NOERRORS Then
            AddHandler Application.ThreadException, AddressOf eh.OnThreadException
        End If

        'moved by LCW 2/18/08--according to MS must be at beginning of Main--fixes problems with appearance of main form until resized
        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
        Catch
        End Try

        '  Restoring default settings
        If M_RESET_DEFAULTS Then
            MapWinUtility.TryDelete(BinFolder + "\default.mwcfg", False)
            MapWinUtility.TryDelete(BinFolder + "\MapWindowDock.config", False)

            '5/7/2008 jk -- new location of default configuration file is in the "Application Data" directory
            MapWinUtility.TryDelete(System.IO.Path.Combine(XmlProjectFile.GetApplicationDataDir(), "default.mwcfg"), False)
            MapWinUtility.TryDelete(System.IO.Path.Combine(XmlProjectFile.GetApplicationDataDir(), "MapWindowDock.config"), False)

            MapWinUtility.Logger.Msg("MapWindow defaults have been restored.", MsgBoxStyle.Information, "Defaults Restored")
            End
        End If

        '08/09/2006 Chris Michaelis -- Fire off a thread to ensure that the
        'proj datum shift files are present and set as an environment variable.
        Try
            Dim projnadCheck As New Thread(AddressOf CheckPROJNAD)
            projnadCheck.Start()
            ' TODO Check the new GDAL_DATA as wel?
        Catch
        End Try

        '3/30/2008 added by Jiri Kadlec: load regional and language settings 
        'use the Language settings (Culture) specified in the configuration file if specified by the user
        LoadCulture()

        '5/08/2008 jk: frmScript must be initialized after loading the language settings in order
        'to display the translated version
        Scripts = New frmScript

        ' Search for config file in command line
        Dim broadcastCmdLine As Boolean = False
        RunConfigCommandLine(broadcastCmdLine)

        ' logo screen
        Dim formLogo As New frmLogo

        If Not M_NOLOGO Then
            formLogo.Show()
            formLogo.lblVersion.Text = App.VersionString & " [BETA]"
            Application.DoEvents()
        End If

        ' cheching whether ocx is available, trying to register it if not
        If Not CheckOCXRegistration() Then Return

        ' creating instance of form
        If Not M_NOLOGO Then
            formLogo.lblStatus.Text = "Initialization..."
            formLogo.lblStatus.Refresh()
        End If

        frmMain = New MapWindowForm
        MapWinUtility.Logger.ProgressStatus = New MWProgressStatus

        ' reverting to the old symbology
        If M_OLDSYMBOLOGY Then
            frmMain.MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmStandard
        End If

        ' loading projection database
        formLogo.lblStatus.Text = "Reading projection database..."
        formLogo.lblStatus.Refresh()
        ProjectionDB.ReadFromExecutablePath(Application.ExecutablePath)

        ' clsMenus uses global reference to frmMain 
        ' when called from frmMain constructor those classes generate null reference exception
        ' therefore it should be called later
        frmMain.CreateMenus()

        ' loading application plugins
        If Not M_NOLOGO Then
            formLogo.lblStatus.Text = "Loading application plug-ins..."
            formLogo.lblStatus.Refresh()
        End If

        frmMain.m_PluginManager.LoadApplicationPlugins(AppInfo.ApplicationPluginDir)

        ' plug-in loading
        If Not M_NOPLUGINS Then
            If Not M_NOLOGO Then
                formLogo.lblStatus.Text = "Loading plug-ins..."
                formLogo.lblStatus.Refresh()
                formLogo.Refresh()
            End If

            frmMain.m_PluginManager.LoadPlugins()

            'make sure all plugins are loaded in the plugin menu
            frmMain.SynchPluginMenu()
        End If

        ' loading configuration
        ' should be prior the showing of the form as position and size are stored here
        LoadConfig()

        ' hiding the logo
        If Not M_NOLOGO Then
            formLogo.Close()
            formLogo.Dispose()
        End If

        ' showing the main form
        frmMain.LoadToolStripSettings(frmMain.StripDocker)
        frmMain.mapPanel.Show(frmMain.dckPanel)
        frmMain.Show()
        frmMain.Update()
        frmMain.SetModified(False)
        frmMain.m_HasBeenSaved = True
        frmMain.MapMain.Focus()
        Application.DoEvents()

        'AppInfo.UseSplashScreen

        'Determine whether or not to show the welcome screen
        'if the app was started with a project then don't show the welcome screen.
        If (AppInfo.ShowWelcomeScreen And Not broadcastCmdLine) And Not M_NOWELCOME Then
            ShowWelcomeScreen()
        End If

        'If there was a project file, extract it into the projinfo object.
        'This will also handle loading of shapefiles.
        Dim broadcastCmdLine_2 As Boolean = False
        RunProjectCommandLine(Microsoft.VisualBasic.Command(), broadcastCmdLine_2)

        If broadcastCmdLine Or broadcastCmdLine_2 Then
            frmMain.Plugins.BroadcastMessage("COMMAND_LINE:" & Microsoft.VisualBasic.Command())
        End If

        'All is ready and done... so if a script is waiting to run, do it now.
        If Not Scripts.pFileName = "" Then
            Scripts.RunSavedScript()
        End If

        Try
            Application.Run(frmMain)
        Catch e As System.ObjectDisposedException
            'ignore, occurs when application.exit called 
        End Try

        ' remove custom error handler if applicable
        If Not M_NOERRORS Then
            RemoveHandler Application.ThreadException, AddressOf eh.OnThreadException
        End If

        'ANY CODE below this point will be executed when the application terminates.
        For Each s As String In g_KillList
            Try
                System.IO.File.Delete(s)
            Catch e As Exception
                Debug.WriteLine("Failed to delete temp file: " & s & " " & e.Message)
            End Try
        Next
        g_KillList.Clear()

        'Show a survey on the first run if the user has elected to take it.
        Try
            Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\MapWindow", True)
            If regKey.GetValue("ShowSurvey", "False") = "True" Then
                System.Diagnostics.Process.Start("http://www.MapWindow.org/EndFirstRunSurvey.php")
                regKey.DeleteValue("ShowSurvey")
            End If
        Catch e As Exception
            MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Creates main form. Registers MapWinGis if needed
    ''' </summary>
    ''' <remarks>It would be better to perform registration checking separately</remarks>
    Private Function CheckOCXRegistration() As Boolean
        Dim Created As Boolean = False

        Dim pnt As MapWinGIS.Point
        Try
            ' trying to create instance of any COM class
            ' in case we failed, most likely we have problem with MapWinGis registration
            pnt = New MapWinGIS.Point
            Created = True

        Catch NoFileException As System.IO.FileNotFoundException
            'Likely no mapwingis.ocx
            If MsgBox("One or more required files could not be found. Please ensure that MapWindow is properly installed." & vbCrLf _
                    & "View or send error details?", MsgBoxStyle.YesNo, "Unable to start MapWindow") = MsgBoxResult.Yes Then
                ShowError(NoFileException)
            End If
        Catch CreateMainFormException As System.Runtime.InteropServices.COMException
            'Likely cause is that MapWinGIS.ocx is not registered
            Dim MsgDetails As String = ""
            Dim RegisterFilename As String = BinFolder + "\MapWinGIS.ocx"
            If Not IO.File.Exists(RegisterFilename) Then
                RegisterFilename = My.Computer.FileSystem.SpecialDirectories.ProgramFiles + "\Common Files\MapWindow\MapWinGIS.ocx"
            End If
            If IO.File.Exists(RegisterFilename) Then
                Try
                    Shell("regsvr32 /u /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)
                    Shell("regsvr32 /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)

                    pnt = New MapWinGIS.Point
                    Created = True

                    'Since that was needed and worked, go ahead and try registering TauDEM too since it generally needs registering at the same time
                    RegisterFilename = BinFolder + "\Plugins\watershed_delin\mwtaudem.dll"
                    If IO.File.Exists(RegisterFilename) Then
                        Try
                            Shell("regsvr32 /u /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)
                            Shell("regsvr32 /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)
                            RegisterFilename = BinFolder + "\Plugins\watershed_delin\tkTaudem.dll"
                            Shell("regsvr32 /u /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)
                            Shell("regsvr32 /s """ & RegisterFilename & """", AppWinStyle.Hide, True, 5000)
                        Catch
                        End Try
                    End If
                Catch
                    MsgDetails = "(Could not register MapWinGIS.ocx)"
                End Try
            Else
                MsgDetails = "(Could not find MapWinGIS.ocx)"
            End If

            If Not Created Then
                If MsgBox("A reboot is required after installing." & vbCrLf _
                        & "If rebooting does not help, register MapWinGIS.ocx" & vbCrLf _
                        & MsgDetails & vbCrLf _
                        & "View or send error details?", MsgBoxStyle.YesNo, _
                          "Unable to start MapWindow") = MsgBoxResult.Yes Then
                    ShowError(CreateMainFormException)
                End If
            End If
        Catch NonComException As Exception
            ShowError(NonComException)
        End Try
        Return Created
    End Function

    ''' <summary>
    ''' Shows welcome screen to choose a project of file
    ''' </summary>
    Public Sub ShowWelcomeScreen()
        'Chris M Jan 2 06 -- Also test to see if it's "".
        If Not AppInfo.WelcomePlugin Is Nothing AndAlso Not AppInfo.WelcomePlugin = "" And Not AppInfo.WelcomePlugin = "WelcomeScreen" Then
            frmMain.Plugins.BroadcastMessage("WELCOME_SCREEN")
        Else
            Dim welcomeScreen As New frmWelcomeScreen
            welcomeScreen.ShowDialog(frmMain)
        End If
    End Sub

    ''' <summary>
    ''' Porvides temp file, which will be deleted afterwards automatically
    ''' </summary>
    Public Function GetMWTempFile() As String
        'Ensure we have this in our kill list!
        Dim ret As String = System.IO.Path.GetTempFileName
        Try
            System.IO.File.Delete(ret) 'Frequently, GetMWTempFile() + ".jpg" etc - resulting in zero byte temp files
        Catch
        End Try
        g_KillList.Add(ret)
        Return ret
    End Function

    '08/09/2006 Chris Michaelis for the AquaTerra "striped" reprojection problem where
    'the proj_nad environment variable was missing. This ensures it will be set.
    Private Sub CheckPROJNAD()
        If System.IO.File.Exists(BinFolder & "\setenv.exe") AndAlso System.IO.Directory.Exists(BinFolder & "\PROJ_NAD") Then
            Try
                Dim psi As New ProcessStartInfo
                psi.FileName = BinFolder & "\setenv.exe"
                psi.Arguments = "-a PROJ_LIB " & BinFolder & "\PROJ_NAD"
                psi.CreateNoWindow = True
                psi.WindowStyle = ProcessWindowStyle.Hidden
                Diagnostics.Process.Start(psi)
            Catch e As Exception
                MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
            End Try
            Try
                Dim psi As New ProcessStartInfo
                psi.FileName = BinFolder & "\setenv.exe"
                psi.Arguments = "-a GDAL_DATA " & BinFolder & "\GDAL_DATA"
                psi.CreateNoWindow = True
                psi.WindowStyle = ProcessWindowStyle.Hidden
                Diagnostics.Process.Start(psi)
            Catch e As Exception
                MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
            End Try

            ' Chris Michaelis 1/25/2007
            ' The SetEnv trick doesn't always work. Fall back on System.Environment also
            ' (System.Environment doesn't work sometimes; but the combo of the two seem to catch all cases)
            Try
                Environment.SetEnvironmentVariable("PROJ_LIB", BinFolder & "\PROJ_NAD", EnvironmentVariableTarget.Machine)
                Environment.SetEnvironmentVariable("PROJ_LIB", BinFolder & "\PROJ_NAD", EnvironmentVariableTarget.User)
                Environment.SetEnvironmentVariable("PROJ_LIB", BinFolder & "\PROJ_NAD", EnvironmentVariableTarget.Process)

                Environment.SetEnvironmentVariable("GDAL_DATA", BinFolder & "\GDAL_DATA", EnvironmentVariableTarget.Machine)
                Environment.SetEnvironmentVariable("GDAL_DATA", BinFolder & "\GDAL_DATA", EnvironmentVariableTarget.User)
                Environment.SetEnvironmentVariable("GDAL_DATA", BinFolder & "\GDAL_DATA", EnvironmentVariableTarget.Process)
            Catch e As Exception
                MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Shows an error dialog
    ''' </summary>
    ''' <param name="ex">Exception to show description of</param>
    ''' <param name="email">Email address</param>
    ''' <remarks></remarks>
    Public Sub ShowError(ByVal ex As System.Exception, Optional ByVal email As String = "")
        MapWinUtility.Logger.Dbg(ex.ToString())
        modMain.CustomExceptionHandler.SendNextToEmail = email
        modMain.CustomExceptionHandler.OnThreadException(ex)
    End Sub

    ''' <summary>
    ''' Runs the project specified in the command line
    ''' </summary>
    ''' <param name="CommandLine">Command line string</param>
    ''' <param name="broadcastCmdLine">In case command line settings wasn't processed, they will be broadcasted to the plugins</param>
    Public Sub RunProjectCommandLine(ByVal CommandLine As String, ByRef broadcastCmdLine As Boolean)
        'These two objects are used to get the list of supported formats in case the
        'parameter is a layer to add. cdm 12-21-2005
        Dim grd As New MapWinGIS.Grid
        Dim sf As New MapWinGIS.Shapefile
        Dim img As New MapWinGIS.Image

        'Used to get command line project or config file names.

        MapWinUtility.Logger.Dbg("In RunProjectCommandLine: " + CommandLine)

        If Len(CommandLine) <> 0 Then
            'remove the quotes(") on both sides of the string if they exist
            CommandLine = CommandLine.Replace("""", "")

            ' get rid of arguments
            Dim index As Integer = CommandLine.IndexOf("/")
            If index = 0 Then
                CommandLine = ""
            ElseIf index > 0 Then
                CommandLine = CommandLine.Substring(0, index)
            End If

            CommandLine = CommandLine.Trim()
            If CommandLine.Length > 0 Then

                'if it is a MapWindow file then open it
                Dim ext As String = System.IO.Path.GetExtension(CommandLine).ToLower()

                If ext = ".gemprj" Or ext = ".vwr" Then
                    'First, however, ensure the current project has been saved if the
                    'thing dragged was a project file.
                    If Not frmMain.m_HasBeenSaved Or ProjInfo.Modified Then
                        If frmMain.PromptToSaveProject() = MsgBoxResult.Cancel Then
                            Exit Sub
                        End If
                    End If

                    ' Paul Meems, 23 Oct 2009
                    ' Clear view before opening new project:
                    frmMain.Layers.Clear()
                    ' End modification, Paul Meems, 23 Oct 2009
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    ProjInfo.ProjectFileName = CommandLine
                    If Not ProjInfo.LoadProject(CommandLine) Then
                        ' Paul Meems 10 Aug 2010: moved error box to here from LoadProject():
                        ' TODO Needs localization:
                        MapWinUtility.Logger.Msg("Errors occured while opening this project file", MsgBoxStyle.Exclamation, "Project File Error Report")
                    End If

                ElseIf Not ext = "" And Not grd.CdlgFilter().IndexOf(ext) = -1 Then
                    'This is a layer that's supported by our Grid object. cdm 12-21-2005
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    frmMain.SetModified(True)
                    frmMain.m_layers.AddLayer(CommandLine, , , Layers.GetDefaultLayerVis())
                ElseIf Not ext = "" And Not sf.CdlgFilter().IndexOf(ext) = -1 Then
                    'This is a layer that's supported by our Shapefile object. cdm 12-21-2005
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    frmMain.SetModified(True)
                    frmMain.m_layers.AddLayer(CommandLine, , , Layers.GetDefaultLayerVis())
                ElseIf Not ext = "" And Not img.CdlgFilter().IndexOf(ext) = -1 Then
                    'This is a layer that's supported by our Image object. cdm 4-19-2006
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    frmMain.SetModified(True)
                    frmMain.m_layers.AddLayer(CommandLine, , , Layers.GetDefaultLayerVis())
                ElseIf Not ext = "" And CommandLine.ToLower().EndsWith(".mwsymb") Then
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    frmMain.SetModified(True)
                    frmMain.m_layers.AddLayer(CommandLine, , , Layers.GetDefaultLayerVis())
                ElseIf ext = ".cs" Or ext = ".vb" Then
                    'It's probably a script - run it. Do it later, though, so just set filename for now.
                    AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(CommandLine)
                    Scripts.pFileName = CommandLine
                ElseIf ext = ".grd" Then
                    'Warn the user.
                    MapWinUtility.Logger.Msg("The file you've attempted to open, " + System.IO.Path.GetFileName(CommandLine) + ", could be either a surfer grid or an ESRI grid image." _
                    + vbCrLf + vbCrLf + "If the former, please use the GIS Tools plug-in to convert the grid to a compatible format." + vbCrLf + "If the latter, please open the sta.adf file instead.", MsgBoxStyle.Information, "Grid Conversion Required")
                Else
                    'Broadcast this cmdline message to all the plugins
                    'But can not do it now because the frmMain still haven't initialized
                    broadcastCmdLine = True
                End If
            End If
        End If

        'Set to nothing so that GC will do it's magic
        grd = Nothing
        sf = Nothing
        ' Paul Meems, 19 Oct. 2009
        ' Added:
        img = Nothing

        MapWinUtility.Logger.Dbg("Finished RunProjectCommandLine()")
    End Sub

    ''' <summary>
    ''' Reads config file name if any from the command line options
    ''' </summary>
    ''' <param name="broadcastCmdLine">Returns true in case the command line should be analyzed further</param>
    Private Sub RunConfigCommandLine(ByRef broadcastCmdLine As Boolean)
        'Used to get command line project or config file names.
        Dim S As String = Microsoft.VisualBasic.Command()
        If Len(S) <> 0 Then
            'remove the quotes(") on both sides of the string if they exist
            S = S.Replace("""", "")

            ' get rid of arguments
            Dim index As Integer = S.IndexOf("/")
            If index = 0 Then
                S = ""
            ElseIf index > 0 Then
                S = S.Substring(0, index)
            End If

            S = S.Trim()
            If S.Length = 0 Then Return

            'if it is a MapWindow file then open it
            Dim ext As String = System.IO.Path.GetExtension(S).ToLower()

            If ext = ".mwcfg" Then
                ProjInfo.ConfigFileName = S
            Else
                'Broadcast this cmdline message to all the plugins
                'But can not do it now because the frmMain still haven't initialized
                broadcastCmdLine = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Loads configuration settings of the program
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadConfig()
        '26/5/2008 Jiri Kadlec -- The new search order for a configuration file is:
        ' 1) search for a configuration file path in the project file. If it exists, set ConfigFileName to 
        '    the path specified in the project file. This option is used when opening existing projects.
        '
        ' 2) if ConfigFileName is "default.mwcfg" in the MW executable directory, copy the content of 
        '    "default.mwcfg" to a new file "MapWindow.mwcfg" in "Documents and Settings\user\Application Data" 
        '    special folder And change ConfigFileName to "MapWindow.mwcfg".
        '
        ' 3) if a project file doesn't exist or if there is no configuration file path specified in the project
        '    file, search for a file "MapWindow.mwcfg" in "Documents and Settings\[user name]\Application Data" special folder.
        '    if "MapWindow.mwcfg" exists, set ConfigFileName to "MapWindow.mwcfg". This option is used when
        '    starting a new project.
        ' 4) if "MapWindow.mwcfg" doesn't exist, search for a file "default.mwcfg" in the MW executable directory
        '    and copy the content of "default.mwcfg" to "MapWindow.mwcfg".
        ' 5) Finally, compare the date of last modification of ConfigFileName and "default.mwcfg". If "default.mwcfg" is
        '    newer than ConfigFileName, overwrite ConfigFileName by the content of default.mwcfg. This option is used after
        '    a reinstallation of MapWindow.

        Dim DefaultConfigFileName As String = ProjInfo.DefaultConfigFile

        'Load the project file if there is one
        'The project file will indicate which config file to use.
        'the application config file will be automatically loaded when loading the project file.
        g_SyncPluginMenuDefer = True
        If Len(ProjInfo.ProjectFileName) > 0 Then
            'ProjInfo.LoadProject() function will automatically set the value of ConfigFileName.
            ProjInfo.LoadProject(ProjInfo.ProjectFileName)
        End If

        If Len(ProjInfo.ConfigFileName) = 0 Then
            ' A config file is not specified in the project - try to find or create a file "user.mwcfg" in
            ' "~\Application Data\MapWindow" special folder. This option is used when the user clicks 
            ' on MapWindow or when a new project is created (Jiri Kadlec 5/26/2008)

            ProjInfo.ConfigFileName = ProjInfo.UserConfigFile()

            'Create a new config file from the default configuration file
            If Not System.IO.File.Exists(ProjInfo.ConfigFileName) Then
                ProjInfo.CreateConfigFileFromDefault(ProjInfo.ConfigFileName)
            End If
        End If

        '5/26/2008 Jiri Kadlec
        'check if the file "default.mwcfg" in MW executable directory has been modified (by a new MapWindow installation). 
        'in that case, update the Configuration file with the content of default.mwcfg.
        If ProjInfo.CompareFilesByTime(DefaultConfigFileName, ProjInfo.ConfigFileName) > 0 Then
            ProjInfo.CreateConfigFileFromDefault(ProjInfo.ConfigFileName)
        End If

        'ProjInfo.ConfigFileName has been set up - load the project configuration now
        If ProjInfo.ConfigLoaded = False Then
            ProjInfo.LoadConfig(True)
        End If

        g_SyncPluginMenuDefer = False
        frmMain.SynchPluginMenu()
    End Sub

    ''' <summary>
    ''' Load the locale (language and culture settings) from the configuration file.
    ''' </summary>
    Public Sub LoadCulture()
        ' 30/3/2008 - added by Jiri Kadlec - 
        Dim OverrideSystemLocale As Boolean = False
        Dim Locale As String = String.Empty

        Try
            'try loading the locale from the configuration file
            'this will work if there is a file mwLanguage.config in the application directory
            'if the file doesn't exist, the regional language settings is used.
            Dim configFileName As String = System.IO.Path.Combine(ProjInfo.GetApplicationDataDir(), "mwLanguage.config")
            If System.IO.File.Exists(configFileName) Then
                Dim doc As New Xml.XmlDocument()
                doc.Load(configFileName)
                Dim root As Xml.XmlElement = doc.DocumentElement
                Dim cultureXml As Xml.XmlElement = root.Item("Culture")
                If cultureXml.HasAttribute("OverrideSystemLocale") Then
                    Dim OverrideXml As Xml.XmlElement = root.Item("OverrideSystemLocale")
                    If cultureXml.HasAttribute("OverrideSystemLocale") Then
                        OverrideSystemLocale = Boolean.Parse(cultureXml.Attributes("OverrideSystemLocale").InnerText)
                        If (cultureXml.HasAttribute("Locale") And OverrideSystemLocale = True) Then
                            Locale = cultureXml.Attributes("Locale").InnerText
                        End If
                    End If
                End If
            End If
        Catch
            'non-critical, the default system locale will be used
        End Try

        Try
            If Locale <> String.Empty Then
                Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(Locale)
                AppInfo.OverrideSystemLocale = True
                AppInfo.Locale = Locale
            Else
                'language not specified, use the system regional language settings
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture
                AppInfo.OverrideSystemLocale = False
                AppInfo.Locale = Thread.CurrentThread.CurrentUICulture.Name
            End If
        Catch ex As Exception
            MapWinUtility.Logger.Dbg("error setting user-specified culture: " & ex.Message)

            'in case of any exception, use the default windows regional and language settings
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture
        End Try
    End Sub

    'Chris Michaelis, Feb 22 2008 for bugzilla 778
    Public Sub SaveFormPosition(ByVal Fo As Form)
        Dim rk As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\MapWindow4")
        If (Fo.Visible And Not Fo.WindowState = System.Windows.Forms.FormWindowState.Minimized And Fo.Location.X > -1 And Fo.Location.Y > -1 And Fo.Size.Width > 1 And Fo.Size.Height > 1) Then
            rk.SetValue(Fo.Name + "_x", Fo.Location.X)
            rk.SetValue(Fo.Name + "_y", Fo.Location.Y)
            rk.SetValue(Fo.Name + "_w", Fo.Size.Width)
            rk.SetValue(Fo.Name + "_h", Fo.Size.Height)
        End If
    End Sub
    'Chris Michaelis, Feb 22 2008 for bugzilla 778
    Public Sub LoadFormPosition(ByVal Fo As Form)
        Try
            Dim rk As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\MapWindow4", False)
            If Not rk.GetValue(Fo.Name + "_x").ToString() = "" And Not rk.GetValue(Fo.Name + "_y").ToString() = "" And Not rk.GetValue(Fo.Name + "_w").ToString() = "" And Not rk.GetValue(Fo.Name + "_h").ToString() = "" Then
                Fo.Location = New System.Drawing.Point(Double.Parse(rk.GetValue(Fo.Name + "_x").ToString()), Double.Parse(rk.GetValue(Fo.Name + "_y").ToString()))
                Fo.Size = New System.Drawing.Size(Double.Parse(rk.GetValue(Fo.Name + "_w").ToString()), Double.Parse(rk.GetValue(Fo.Name + "_h").ToString()))
            End If
        Catch
            ' No key created yet -- will be created when the form is moved or resized
        End Try
    End Sub

    '7/29/2005 - added a exception handler class(Lailin Chen)
    'To handle all the uncaught exceptions.
    Public Class CustomExceptionHandler
        ' Used for the very next exception, then cleared (and returned to default)
        Public Shared SendNextToEmail As String = ""

        ' Handles the exception event.
        Public Shared Sub OnThreadException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
            OnThreadException(t.Exception)
        End Sub

        Public Shared Sub OnThreadException(ByVal e As Exception)
            If e.Message.Contains("UnauthorizedAccessException") Then
                MapWinUtility.Logger.Msg("An Unauthorized Access error was generated. Please ensure you have access to all files you're trying to work with, and that the files aren't in use by other applications.", MsgBoxStyle.Exclamation, "Unauthorized Access Exception")
                SendNextToEmail = ""
                Exit Sub
            End If

            Try
                If Not ProjInfo.NoPromptToSendErrors Then
                    Dim errorBox As New ErrorDialog(e, SendNextToEmail)
                    errorBox.ShowDialog()
                Else
                    Dim errorBox As New ErrorDialogNoSend(e)
                    errorBox.ShowDialog()
                End If
            Catch ex As Exception
                Dim errorBox As New ErrorDialog(e, SendNextToEmail)
                errorBox.ShowDialog()
            Finally
                SendNextToEmail = ""
            End Try
        End Sub
    End Class

    Public Function StippleToString(ByVal stipple As MapWinGIS.tkFillStipple) As String
        Select Case stipple
            Case MapWinGIS.tkFillStipple.fsDiagonalDownLeft
                Return "Diagonal Down-Left"
            Case MapWinGIS.tkFillStipple.fsDiagonalDownRight
                Return "Dialgonal Down-Right"
            Case MapWinGIS.tkFillStipple.fsVerticalBars
                Return "Vertical"
            Case MapWinGIS.tkFillStipple.fsHorizontalBars
                Return "Horizontal"
            Case MapWinGIS.tkFillStipple.fsPolkaDot
                Return "Cross/Dot"
            Case Else 'MapWinGIS.tkFillStipple.fsNone, MapWinGIS.tkFillStipple.fsCustom
                Return "None"
        End Select
    End Function

    Public Function StringToStipple(ByVal str As String) As MapWinGIS.tkFillStipple
        Select Case str
            Case "Diagonal Down-Left"
                Return MapWinGIS.tkFillStipple.fsDiagonalDownLeft
            Case "Dialgonal Down-Right"
                Return MapWinGIS.tkFillStipple.fsDiagonalDownRight
            Case "Vertical"
                Return MapWinGIS.tkFillStipple.fsVerticalBars
            Case "Horizontal"
                Return MapWinGIS.tkFillStipple.fsHorizontalBars
            Case "Cross/Dot"
                Return MapWinGIS.tkFillStipple.fsPolkaDot
            Case Else
                Return MapWinGIS.tkFillStipple.fsNone
        End Select
    End Function

    Public Sub FindSafeWindowLocation(ByRef W As Integer, ByRef H As Integer, ByRef Location As Point)
        Dim Index, UpperBound As Int16
        Dim maxw, maxh As Int16

        'Gets an array of all the screens connected to the system.
        Dim Screens() As System.Windows.Forms.Screen = System.Windows.Forms.Screen.AllScreens
        UpperBound = Screens.GetUpperBound(0)

        For Index = 0 To UpperBound
            With Screens(Index).WorkingArea
                maxw = Math.Max(maxw, .Right)
                maxh = Math.Max(maxh, .Bottom)
            End With
        Next

        If Location.X + W > maxw Then Location.X = maxw - W
        Location.X = Math.Max(0, Location.X)
        If Location.Y + H > maxh Then Location.Y = maxh - H
        Location.Y = Math.Max(0, Location.Y)
    End Sub

    '04/29/2010 DK - Read XML attribute value from XML file from a given root of the xml file
    Public Function ReadLayerPropertyValueFromXML(ByVal XMLFileName As String, ByVal XMLRootName As String, ByVal XMLPropertyName As String) As String

        Dim doc As New Xml.XmlDocument()
        doc.Load(XMLFileName)
        Dim root As Xml.XmlElement = doc.DocumentElement(XMLRootName)
        Dim XmlProperty As Xml.XmlAttribute = root.Attributes(XMLPropertyName)
        Return XmlProperty.Value.ToString

    End Function
End Module
