'********************************************************************************************************
' Filename:  clsXMLProjectOld.vb
' Description: implements loading and saving of the config file for MW
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'The Original Code is MapWindow Open Source. 
' --------------------------------------------------------------------------------------------------
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'05-apr-2011 - sleschinski - copied the code form the clsXmlProjectFile

Imports System.Xml

Partial Friend Class XmlProjectFile 
#Region "Loading"

    ''' <summary>
    ''' Loads configuration file. It is assumed that the frmmain may or may not have been created yet.    
    ''' </summary>
    ''' <param name="Load_Plugins"></param>
    ''' <returns></returns>
    ''' <remarks>This function loads a config file and returns success or failure. Updated in version 4 to use the configfilename stored in this class and to not use the dotnetbar stuff.</remarks>
    Public Function LoadConfig(ByVal Load_Plugins As Boolean) As Boolean

        Dim odir As String = System.IO.Directory.GetCurrentDirectory()

        Try
            Dim Doc As New XmlDocument  'The xmldocument config file
            Dim Root As XmlElement      'An xml element

            If Not frmMain Is Nothing Then

                frmMain.Cursor = System.Windows.Forms.Cursors.WaitCursor
                Windows.Forms.Application.DoEvents()
                frmMain.m_PluginManager.UnloadAll()
                'frmMain.m_PluginManager.UnloadApplicationPlugins()     ' lsu: no need t unload application plug-ins I expect
            End If

            ' May/29/2008 Jiri Kadlec
            ' check if the directory of ConfigFileName exists before changing it
            ' if it does not exist, use the standard location of ConfigFileName
            Dim ConfigDir As String = IO.Path.GetDirectoryName(ConfigFileName)
            If Not IO.Directory.Exists(ConfigDir) Then
                ConfigDir = GetApplicationDataDir()
                ConfigFileName = Me.UserConfigFile 'set config file to "Documents and Settings\(user name)\Application Data\user.mwcfg
            End If

            'the paths of any files saved in the configuration are relative paths. This is why the
            'current directory must be changed when reading or writing from the config file.
            ChDir(ConfigDir)

            '**** add the following elements to "mwcfg" ****
            Doc = New XmlDocument

            'Chris M 3/13/2006 - if the config doesn't exist, save -- this will safe a new default.
            'Jiri Kadlec May/29/2008 - if the config doesn't exist, try creating it from default.mwcfg.

            'TODO: always check the version of default config file. If current MW version is higher
            'than config file version, overwrite it using default compiled settings.
            If Not System.IO.File.Exists(ConfigFileName) Then
                MapWinUtility.Logger.Dbg("Loading Configuration: Creating configuration file from default")
                CreateConfigFileFromDefault(ConfigFileName)
                'Prepare the default application plugin location first:
                'AppInfo.ApplicationPluginDir = BinFolder + "\ApplicationPlugins"
                'frmMain.m_PluginManager.LoadApplicationPlugins(AppInfo.ApplicationPluginDir)
                'SaveConfig()
            End If

            MapWinUtility.Logger.Dbg("Loading Configuration: " + ConfigFileName)
            Doc.Load(ConfigFileName)
            Root = Doc.DocumentElement

            'load the View
            LoadView(Root.Item("View"))

            'force the mapwindow to show
            'frmMain.Show()
            System.Windows.Forms.Application.DoEvents()

            'load Appinfo
            LoadAppInfo(Root.Item("AppInfo"))

            'load recent files
            LoadRecentProjects(Root.Item("RecentProjects"))

            'load the color Palettes
            If (Not Root.Item("ColorPalettes") Is Nothing) Then
                LoadColorPalettes(Root.Item("ColorPalettes"))
            Else
                frmMain.g_ColorPalettes = p_Doc.CreateElement("ColorPalettes")
            End If

            'load the Plugins
            If Load_Plugins = True Then
                LoadPlugins(Root.Item("Plugins"), True)
            End If

            ' adding favorite projections
            Dim nodeProjection As XmlElement = Root.Item("FavoriteProjections")
            If Not nodeProjection Is Nothing Then
                Dim list As Generic.List(Of Integer) = frmMain.ApplicationInfo.FavoriteProjections
                list.Clear()

                Dim text As String = nodeProjection.InnerText
                Dim separator() As String = {";"}
                Dim code As Integer
                For Each s As String In text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    If Int32.TryParse(s, code) Then list.Add(code)
                Next
            End If

            ' lsu: don't see reason to reload application plug-ins load the application plugins
            'LoadApplicationPlugins(Root.Item("ApplicationPlugins"), True)

            frmMain.Update()
            ConfigLoaded = True

            'change the cursor back to the default
            frmMain.Cursor = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            m_ErrorOccured = True
            m_ErrorMsg = ex.ToString
        Finally
            System.IO.Directory.SetCurrentDirectory(odir)
        End Try

        If m_ErrorOccured Then
            MapWinUtility.Logger.Msg(m_ErrorMsg, MsgBoxStyle.Exclamation, "Configuration File Error Report")
            m_ErrorOccured = False
        End If
    End Function

    ''' <summary>
    ''' This function will create a new MapWindow configuration file using the settings 
    ''' from the default configuration file "default.mwcfg" from the MW executable directory 
    ''' in case default.mwcfg does not exist, it creates a new configuration file using 
    ''' default compiled-in settings
    ''' </summary>
    Public Sub CreateConfigFileFromDefault(ByVal NewConfigFile As String)

        'this is the read-only default configuration file (default.mwcfg) provided by MapWindow installation
        Dim DefaultConfigFileName As String = Me.DefaultConfigFile '..\MapWindow\default.mwcfg

        'Always make sure we are in the MW executable directory to make relative paths work
        Dim odir As String = CurDir()
        Dim MapWindowDir As String = BinFolder
        If odir <> MapWindowDir Then
            ChDir(MapWindowDir)
        End If

        If Not System.IO.File.Exists(DefaultConfigFileName) Then
            'Jiri Kadlec May/29/2008 this is the only place where MapWindow
            'WRITES to default.mwcfg. this code is executed only in case default.mwcfg
            'in the MW executable folder does not exist.
            Dim originalCfgFile = ConfigFileName
            ConfigFileName = DefaultConfigFileName
            'LoadConfig(True) 9/8/2008 removed by Jiri Kadlec - this caused infinite recursion
            SaveConfig()
            ConfigFileName = originalCfgFile
        End If

        Try
            'NewConfigFile is usually located in Documents and Settings\[user name]\Application Data\MapWindow.
            'it can also have a custom location specified by the project file.
            System.IO.File.Copy(DefaultConfigFileName, NewConfigFile, True)

            'make sure "ApplicationPlugins" , "helpFilePath" and "DefaultDir" paths are correct. 
            'these paths should be a relative paths to the location of NewConfigFile. Also correct 
            'helpFilePath and DefaultDir.

            Dim doc As New XmlDocument
            doc.Load(DefaultConfigFileName)
            Dim Root As XmlElement = doc.DocumentElement

            'correct helpFilePath and defaultDir
            Dim AppInfoElement As XmlElement = Root.Item("AppInfo")
            Dim HelpFilePath As String = ""
            Dim NewHelpFilePath As String = ""
            If AppInfoElement.HasAttribute("HelpFilePath") Then
                Try
                    HelpFilePath = System.IO.Path.GetFullPath(AppInfoElement.Attributes("HelpFilePath").Value)
                Catch
                    HelpFilePath = MapWindowDir
                End Try
                NewHelpFilePath = Me.GetRelativePath(HelpFilePath, NewConfigFile)
            End If

            Dim DefaultDir As String = ""
            Dim NewDefaultDir As String = ""
            If AppInfoElement.HasAttribute("DefaultDir") Then
                Try
                    DefaultDir = System.IO.Path.GetFullPath(AppInfoElement.Attributes("DefaultDir").Value)
                Catch
                    DefaultDir = System.IO.Path.GetFullPath("Sample Projects")
                End Try
                NewDefaultDir = Me.GetRelativePath(DefaultDir, NewConfigFile)
            End If

            Dim ApplicationPluginElement As XmlElement = Root.Item("ApplicationPlugins")
            Dim AppPluginDir As String = ApplicationPluginElement.Attributes("PluginDir").Value
            Dim AppPluginPath As String = ""

            Try
                AppPluginPath = System.IO.Path.GetFullPath(AppPluginDir)
            Catch ex As Exception
                'the Application plugin path specified by default.mwcfg is not valid - try to use
                'the default \ApplicationPlugins subfolder in MW executable directory instead.
                AppPluginPath = System.IO.Path.Combine(BinFolder, "ApplicationPlugins")
            End Try

            AppPluginDir = Me.GetRelativePath(AppPluginPath, NewConfigFile)

            doc.Load(NewConfigFile)
            Root = doc.DocumentElement
            AppInfoElement = Root.Item("AppInfo")
            AppInfoElement.Attributes("HelpFilePath").Value = NewHelpFilePath
            AppInfoElement.Attributes("DefaultDir").Value = NewDefaultDir
            ApplicationPluginElement = Root.Item("ApplicationPlugins")
            ApplicationPluginElement.Attributes("PluginDir").Value = AppPluginDir
            doc.Save(NewConfigFile)

            MapWinUtility.Logger.Dbg("Copied configuration file from " + _
            DefaultConfigFileName + " to " + NewConfigFile)
        Catch ex As Exception
            MapWinUtility.Logger.Dbg("Creating config from default - Error - unable to copy " + _
            "configuration file from " + DefaultConfigFileName + " to " + NewConfigFile)
            NewConfigFile = DefaultConfigFileName
        Finally
            If MapWindowDir <> odir Then
                ChDir(odir)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Reads the custom application info from the config file. 
    ''' This can be called before frmMain is loaded to determine 
    ''' whether or not to show a splash screen and for how long.
    ''' </summary>
    Private Sub LoadAppInfo(ByVal AppInfoXML As XmlElement)
        Dim Type As String

        Try
            AppInfo.Name = AppInfoXML.Attributes("Name").InnerText
            AppInfo.Version = AppInfoXML.Attributes("Version").InnerText
            AppInfo.BuildDate = AppInfoXML.Attributes("BuildDate").InnerText
            AppInfo.Developer = AppInfoXML.Attributes("Developer").InnerText
            AppInfo.Comments = AppInfoXML.Attributes("Comments").InnerText
            AppInfo.SplashTime = CInt(AppInfoXML.Attributes("SplashTime").InnerText)

            Try
                AppInfo.LogfilePath = AppInfoXML.Attributes("LogfilePath").InnerText
                'Enable logging:
                MapWinUtility.Logger.StartToFile(AppInfo.LogfilePath, False, True, False)
            Catch
                AppInfo.LogfilePath = ""
            End Try

            Try
                NoPromptToSendErrors = CBool(AppInfoXML.Attributes("NoPromptToSendErrors").InnerText)
            Catch
                NoPromptToSendErrors = False
            End Try

            Dim NeverShowProjectionDialog As Boolean = False
            Try
                NeverShowProjectionDialog = CBool(AppInfoXML.Attributes("NeverShowProjectionDialog").InnerText)
            Catch ex As Exception
            End Try
            AppInfo.NeverShowProjectionDialog = NeverShowProjectionDialog

            If AppInfoXML.Attributes("WelcomePlugin") Is Nothing Then
                AppInfo.WelcomePlugin = Nothing
            Else
                AppInfo.WelcomePlugin = AppInfoXML.Attributes("WelcomePlugin").InnerText
            End If

            Try
                Dim strPath As String = System.IO.Path.GetFullPath(AppInfoXML.Attributes("DefaultDir").InnerText)
                If strPath <> Nothing Then
                    AppInfo.DefaultDir = strPath
                End If
            Catch
                'Should do some kind of logging here if the dir is invalid
            End Try

            If (AppInfoXML.HasAttribute("URL")) Then
                AppInfo.URL = AppInfoXML.Attributes("URL").InnerText
            End If

            If (AppInfoXML.HasAttribute("ShowWelcomeScreen")) Then
                AppInfo.ShowWelcomeScreen = Boolean.Parse(AppInfoXML.Attributes("ShowWelcomeScreen").InnerText)
            End If

            If (AppInfoXML.HasAttribute("DisplayAutoCreatespatialindex")) Then
                AppInfo.ShowAutoCreateSpatialindex = Boolean.Parse(AppInfoXML.Attributes("DisplayAutoCreatespatialindex").InnerText)
            End If

            If (AppInfoXML.HasAttribute("DisplayFloatingScalebar")) Then
                AppInfo.ShowFloatingScalebar = Boolean.Parse(AppInfoXML.Attributes("DisplayFloatingScalebar").InnerText)
            End If

            If (AppInfoXML.HasAttribute("DisplayMapWindowVersion")) Then
                AppInfo.ShowMapWindowVersion = Boolean.Parse(AppInfoXML.Attributes("DisplayMapWindowVersion").InnerText)
            End If

            If (AppInfoXML.HasAttribute("DisplayAvoidCollision")) Then
                AppInfo.ShowAvoidCollision = Boolean.Parse(AppInfoXML.Attributes("DisplayAvoidCollision").InnerText)
            End If

            If (AppInfoXML.HasAttribute("DisplayRedrawSpeed")) Then
                AppInfo.ShowHideRedrawSpeed = Boolean.Parse(AppInfoXML.Attributes("DisplayRedrawSpeed").InnerText)
            End If

            If (AppInfoXML.Attributes("HelpFilePath").InnerText <> "") Then
                AppInfo.HelpFilePath = System.IO.Path.GetFullPath(AppInfoXML.Attributes("HelpFilePath").InnerText)
            Else
                AppInfo.HelpFilePath = ""
            End If

            If (AppInfo.SplashTime < 0) Then
                AppInfo.SplashTime = 0
            End If

            If (AppInfoXML.HasAttribute("ShowDynVisWarnings")) Then
                AppInfo.ShowDynamicVisibilityWarnings = Boolean.Parse(AppInfoXML.Attributes("ShowDynVisWarnings").InnerText)
            End If

            If (AppInfoXML.HasAttribute("ShowLayerAfterDynVis")) Then
                AppInfo.ShowLayerAfterDynamicVisibility = Boolean.Parse(AppInfoXML.Attributes("ShowLayerAfterDynVis").InnerText)
            End If

            If (AppInfoXML.HasAttribute("SymbologyLoadingBehavior")) Then
                AppInfo.SymbologyLoadingBehavior = [Enum].Parse(GetType(SymbologyBehavior), (AppInfoXML.Attributes("SymbologyLoadingBehavior").InnerText))
            End If

            If (AppInfoXML.HasAttribute("ProjectionMismatchBehavior")) Then
                AppInfo.ProjectionMismatchBehavior = [Enum].Parse(GetType(ProjectionMismatchBehavior), (AppInfoXML.Attributes("ProjectionMismatchBehavior").InnerText))
            End If

            If (AppInfoXML.HasAttribute("ProjectionAbsenceBehavior")) Then
                AppInfo.ProjectionAbsenceBehavior = [Enum].Parse(GetType(ProjectionAbsenceBehavior), (AppInfoXML.Attributes("ProjectionAbsenceBehavior").InnerText))
            End If

            If (AppInfoXML.HasAttribute("ShowLoadingReport")) Then
                AppInfo.ShowLoadingReport = Boolean.Parse(AppInfoXML.Attributes("ShowLoadingReport").InnerText)
            End If

            If (AppInfoXML.HasAttribute("ProjectReloading")) Then
                AppInfo.ProjectReloading = Boolean.Parse(AppInfoXML.Attributes("ProjectReloading").InnerText)
            End If

            'load the window title
            'Version Numbers: frmMain.Text = AppInfo.Name + " " + App.VersionString ' for now, will be rewritten later
            frmMain.Text = AppInfo.Name + " " ' for now, will be rewritten later

            'load the help munu text
            frmMain.m_Menu.AddMenu("mnuAboutMapWindow", "mnuHelp", Nothing, "&About " & AppInfo.Name)

            'load the Splash image
            With AppInfoXML.Item("SplashPicture").Item("Image")
                Type = .Attributes("Type").InnerText
                AppInfo.SplashPicture = CType(ConvertStringToImage(.InnerText, Type), Image)
            End With

            'load the Application icon
            With AppInfoXML.Item("WindowIcon").Item("Image")
                Type = .Attributes("Type").InnerText
                AppInfo.FormIcon = CType(ConvertStringToImage(.InnerText, Type), Icon)
                If .InnerText = "" Then
                ElseIf .InnerText = "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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD////////////////////////////xH///wAf//4AD//8AAf/+AAD//AAAf/wAAH/gAAA/AAAAPwAAAD8AAAA/wAAAIfgAACD4AAAA+AAAAPwwAA/8OBh//hw4//8f8f//j+P//8OH///zn////////////////////////////w==" Then
                    'This is an old icon - let's just force them to upgrade, shall we?
                    '(e.g., don't change the icon - use what the form designer currently has for it)
                Else
                    frmMain.Icon = CType(ConvertStringToImage(.InnerText, Type), Icon)
                End If
            End With

        Catch e As System.Exception
            m_ErrorMsg += "Error: Loading the appinfoxml" + Chr(13)
            m_ErrorOccured = True
            Exit Sub
        End Try
    End Sub

    ''' <summary>
    ''' Loads color palettes
    ''' </summary>
    Private Sub LoadColorPalettes(ByVal colorPalettes As XmlElement)
        Try
            frmMain.g_ColorPalettes = colorPalettes
        Catch ex As System.Exception
            m_ErrorMsg += "Error: Loading the ColorPalettes" + Chr(13)
            m_ErrorOccured = True
            Exit Sub
        End Try
    End Sub

#End Region

#Region "Saving"

    ''' <summary>
    ''' Saves configuration file
    ''' </summary>
    Public Function SaveConfig() As Boolean

        Dim AppDataDir As String = GetApplicationDataDir() '8/5/2008 jk - find the default directory for config files

        'save dock panel configuration
        Try
            MapWinUtility.Logger.Dbg("Saving Dock Panel Configuration: " + _
            System.IO.Path.Combine(AppDataDir, "MapWindowDock.config"))
            frmMain.dckPanel.SaveAsXml(System.IO.Path.Combine(AppDataDir, "MapWindowDock.config"))
        Catch e As System.IO.IOException
            MapWinUtility.Logger.Dbg("Saving Dock Panel Configuration: Exception: " + e.ToString())
        End Try

        '8/5/2008 jk - save the language settings
        SaveCulture()

        'This function saves the config file. In prior versions, filename was 
        'a parameter.  Now it is a local variable. 
        'Also, version 3 used the DotNetBar which had an export function
        'that would export the current layout of the bars. This has been removed.
        Dim Root As XmlElement

        Try
            p_Doc.LoadXml("<Mapwin type='configurationfile' version='" + App.VersionString() + "'></Mapwin>")
            Root = p_Doc.DocumentElement

            'Add the AppInfo
            AddAppInfo(p_Doc, Root)

            'Add the recent projects
            AddRecentProjects(p_Doc, Root)

            'Add the properties of the view to the project file
            AddViewElement(p_Doc, Root)

            'Add the list of the plugins to the project file
            AddPluginsElement(p_Doc, Root, True)

            'Add the application plugins - these are plugins that are 
            'required by a particular application - e.g. BASINS specific 
            'plugins.
            AddApplicationPluginsElement(p_Doc, Root, True)

            'add the ColorPalettes to the config file
            AddColorPalettes(p_Doc, Root)

            ' adding favorite projections
            Dim list As Generic.List(Of Integer) = frmMain.ApplicationInfo.FavoriteProjections
            If (list.Count > 0) Then
                Dim s As String = ""
                For Each proj As Integer In list
                    s += proj.ToString() + ";"
                Next
                ' removing last semicolon
                If (s.Length > 0) Then s = s.Substring(0, s.Length - 1)
                Dim el As XmlElement = p_Doc.CreateElement("FavoriteProjections")
                el.InnerText = s
                Root.AppendChild(el)
            End If

            MapWinUtility.Logger.Dbg("Saving Configuration: " + ConfigFileName)
            p_Doc.Save(ConfigFileName)

            Return True
        Catch ex As System.Exception
            ShowError(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' This sub saves the custom culture (language) settings. If OverrideSystemLocale is true, 
    ''' MapWindow will use the language specified by "Locale" instead of the Windows Regional 
    ''' and Language settings. This information is saved in a separate file, because the culture 
    ''' must be loaded very soon after MapWindow startup, before the initialization of the UI. 
    ''' added by Jiri Kadlec 8.May 2008
    ''' ''' </summary>
    Private Sub SaveCulture()
        Try
            Dim cultureFileName As String = System.IO.Path.Combine(GetApplicationDataDir(), "mwLanguage.config")
            Dim doc As New XmlDocument

            ' Use the XmlDeclaration class to place the
            ' <?xml version="1.0"?> declaration at the top of our XML file
            Dim dec As XmlDeclaration = doc.CreateXmlDeclaration("1.0", Nothing, Nothing)
            doc.AppendChild(dec)

            Dim Root As XmlElement = doc.CreateElement("mwLanguageSettings")
            doc.AppendChild(Root)
            Dim CultureXML As XmlElement = doc.CreateElement("Culture")

            Dim OverrideXML As XmlAttribute = doc.CreateAttribute("OverrideSystemLocale")
            OverrideXML.InnerText = AppInfo.OverrideSystemLocale
            CultureXML.Attributes.Append(OverrideXML)

            Dim LanguageXML As XmlAttribute = doc.CreateAttribute("Locale")
            LanguageXML.InnerText = AppInfo.Locale
            CultureXML.Attributes.Append(LanguageXML)
            Root.AppendChild(CultureXML)

            doc.Save(cultureFileName)

        Catch ex As System.Exception
            MapWinUtility.Logger.Dbg("Saving Language Configuration: Exception: " + ex.ToString())
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saves application settings
    ''' </summary>
    ''' <param name="m_Doc"></param>
    ''' <param name="Parent"></param>
    ''' <remarks></remarks>
    Private Sub AddAppInfo(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        'This sub writes the customizable application info to the configuration file.
        'This info is now pulled from the global AppInfo object as of version 4.
        '1/16/2005
        Dim AppInfoXML As XmlElement = m_Doc.CreateElement("AppInfo")
        Dim SplashImage As XmlElement = m_Doc.CreateElement("SplashPicture")
        Dim WindowIcon As XmlElement = m_Doc.CreateElement("WindowIcon")
        Dim Name As XmlAttribute = m_Doc.CreateAttribute("Name")
        Dim Version As XmlAttribute = m_Doc.CreateAttribute("Version")
        Dim BuildDate As XmlAttribute = m_Doc.CreateAttribute("BuildDate")
        Dim Developer As XmlAttribute = m_Doc.CreateAttribute("Developer")
        Dim Comments As XmlAttribute = m_Doc.CreateAttribute("Comments")
        Dim HelpFilePath As XmlAttribute = m_Doc.CreateAttribute("HelpFilePath")
        Dim UseSplashScreen As XmlAttribute = m_Doc.CreateAttribute("UseSplashScreen")
        Dim SplashPicture As XmlAttribute = m_Doc.CreateAttribute("SplashPicture")
        Dim SplashTime As XmlAttribute = m_Doc.CreateAttribute("SplashTime")
        Dim DefaultDir As XmlAttribute = m_Doc.CreateAttribute("DefaultDir")
        Dim URL As XmlAttribute = m_Doc.CreateAttribute("URL")
        Dim ShowWelcomeScreen As XmlAttribute = m_Doc.CreateAttribute("ShowWelcomeScreen")
        Dim WelcomePlugin As XmlAttribute = m_Doc.CreateAttribute("WelcomePlugin")
        Dim NeverShowProjectionDialog As XmlAttribute = m_Doc.CreateAttribute("NeverShowProjectionDialog")
        Dim NoPromptToSendErrorsXml As XmlAttribute = m_Doc.CreateAttribute("NoPromptToSendErrors")
        Dim LogfilePathXml As XmlAttribute = m_Doc.CreateAttribute("LogfilePath")
        Dim ShowDynVisWarningsXml As XmlAttribute = m_Doc.CreateAttribute("ShowDynVisWarnings")
        Dim ShowLayerAfterDynVisXml As XmlAttribute = m_Doc.CreateAttribute("ShowLayerAfterDynVis")
        Dim SymbologyLoadingBehavior As XmlAttribute = m_Doc.CreateAttribute("SymbologyLoadingBehavior")
        Dim ProjectionMismatch As XmlAttribute = m_Doc.CreateAttribute("ProjectionMismatchBehavior")
        Dim ProjectionAbsence As XmlAttribute = m_Doc.CreateAttribute("ProjectionAbsenceBehavior")
        Dim ShowLoadingReport As XmlAttribute = m_Doc.CreateAttribute("ShowLoadingReport")
        Dim ProjectReloading As XmlAttribute = m_Doc.CreateAttribute("ProjectReloading")
        Dim DisplayFloatingScalebar As XmlAttribute = m_Doc.CreateAttribute("DisplayFloatingScalebar")
        Dim DisplayRedrawSpeed As XmlAttribute = m_Doc.CreateAttribute("DisplayRedrawSpeed")
        Dim DisplayMapWindowVersion As XmlAttribute = m_Doc.CreateAttribute("DisplayMapWindowVersion")
        Dim DisplayAvoidCollision As XmlAttribute = m_Doc.CreateAttribute("DisplayAvoidCollision")
        Dim DisplayAutoCreatespatialindex As XmlAttribute = m_Doc.CreateAttribute("DisplayAutoCreatespatialindex")

        'Set the attributes
        ShowLayerAfterDynVisXml.InnerText = AppInfo.ShowLayerAfterDynamicVisibility
        ShowDynVisWarningsXml.InnerText = AppInfo.ShowDynamicVisibilityWarnings
        LogfilePathXml.InnerText = AppInfo.LogfilePath
        Name.InnerText = AppInfo.Name
        ' Paul Meems - 20 August 2009
        ' It's reading the config file and writes the same value back again,
        ' Making the version number stays 4.5
        ' It's better to get the real values:
        'Version.InnerText = AppInfo.Version
        Version.InnerText = App.VersionString()
        BuildDate.InnerText = AppInfo.BuildDate 'Is always empty
        Developer.InnerText = AppInfo.Developer
        Comments.InnerText = AppInfo.Comments
        'HelpFilePath.InnerText = GetRelativePath(AppInfo.HelpFilePath, System.Reflection.Assembly.GetAssembly(Me.GetType).Location)
        HelpFilePath.InnerText = GetRelativePath(AppInfo.HelpFilePath, ConfigFileName) 'changed by Jiri Kadlec May-30-2008
        SplashTime.InnerText = AppInfo.SplashTime.ToString()
        DefaultDir.InnerText = GetRelativePath(AppInfo.DefaultDir, System.Reflection.Assembly.GetAssembly(Me.GetType).Location)
        URL.InnerText = AppInfo.URL
        ShowWelcomeScreen.InnerText = AppInfo.ShowWelcomeScreen.ToString()
        WelcomePlugin.InnerText = AppInfo.WelcomePlugin
        NeverShowProjectionDialog.InnerText = AppInfo.NeverShowProjectionDialog.ToString
        NoPromptToSendErrorsXml.InnerText = NoPromptToSendErrors.ToString()
        SymbologyLoadingBehavior.InnerText = AppInfo.SymbologyLoadingBehavior
        ProjectionMismatch.InnerText = AppInfo.ProjectionMismatchBehavior
        ProjectionAbsence.InnerText = AppInfo.ProjectionAbsenceBehavior
        ShowLoadingReport.InnerText = AppInfo.ShowLoadingReport
        ProjectReloading.InnerText = AppInfo.ProjectReloading
        DisplayFloatingScalebar.InnerText = AppInfo.ShowFloatingScalebar
        DisplayRedrawSpeed.InnerText = AppInfo.ShowHideRedrawSpeed
        DisplayMapWindowVersion.InnerText = AppInfo.ShowMapWindowVersion
        DisplayAvoidCollision.InnerText = AppInfo.ShowAvoidCollision
        DisplayAutoCreatespatialindex.InnerText = AppInfo.ShowAutoCreateSpatialindex


        'Add the attributes to the appInfo element
        With AppInfoXML.Attributes
            .Append(Name)
            .Append(Version)
            .Append(BuildDate)
            .Append(Developer)
            .Append(Comments)
            .Append(HelpFilePath)
            .Append(UseSplashScreen)
            .Append(SplashTime)
            .Append(DefaultDir)
            .Append(URL)
            .Append(ShowWelcomeScreen)
            .Append(WelcomePlugin)
            .Append(NeverShowProjectionDialog)
            .Append(NoPromptToSendErrorsXml)
            .Append(LogfilePathXml)
            .Append(ShowDynVisWarningsXml)
            .Append(ShowLayerAfterDynVisXml)
            .Append(SymbologyLoadingBehavior)
            .Append(ProjectionMismatch)
            .Append(ProjectionAbsence)
            .Append(ShowLoadingReport)
            .Append(ProjectReloading)
            .Append(DisplayFloatingScalebar)
            .Append(DisplayRedrawSpeed)
            .Append(DisplayMapWindowVersion)
            .Append(DisplayAvoidCollision)
            .Append(DisplayAutoCreatespatialindex)
        End With

        SaveImage(m_Doc, AppInfo.SplashPicture, SplashImage)
        SaveImage(m_Doc, frmMain.Icon, WindowIcon)

        AppInfoXML.AppendChild(WindowIcon)
        AppInfoXML.AppendChild(SplashImage)

        Parent.AppendChild(AppInfoXML)
    End Sub

    ''' <summary>
    ''' Saves recently opened projects
    ''' </summary>
    Private Sub AddRecentProjects(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        'Adds information about the recent projects to the XML document
        'Changed in v.4. to pull recent projects from projinfo object.
        '1/16/2005
        Try
            Dim i As Integer
            Dim RecentFiles As XmlElement = m_Doc.CreateElement("RecentProjects")
            Dim FileXML As XmlElement

            If ProjInfo.RecentProjects.Count <> 0 Then
                For i = 0 To ProjInfo.RecentProjects.Count - 1
                    FileXML = m_Doc.CreateElement("Project")
                    FileXML.InnerText = Me.GetRelativePath(ProjInfo.RecentProjects(i).ToString, ConfigFileName)
                    RecentFiles.AppendChild(FileXML)
                Next
            End If

            Parent.AppendChild(RecentFiles)
        Catch ex As System.Exception
            ShowError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Serializes view properties
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddViewElement(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        'Adds information about the current view to the config file. 
        'At this point, frmMain must exist or this function will die.
        Dim View As XmlElement = m_Doc.CreateElement("View")
        Dim WindowWidth As XmlAttribute = m_Doc.CreateAttribute("WindowWidth")
        Dim WindowHeight As XmlAttribute = m_Doc.CreateAttribute("WindowHeight")
        Dim LocationX As XmlAttribute = m_Doc.CreateAttribute("LocationX")
        Dim LocationY As XmlAttribute = m_Doc.CreateAttribute("LocationY")
        Dim WindowState As XmlAttribute = m_Doc.CreateAttribute("WindowState")
        Dim ViewColor As XmlAttribute = m_Doc.CreateAttribute("ViewBackColor")
        Dim CanUndockPreviewMap As XmlAttribute = m_Doc.CreateAttribute("CanUndockPreviewMap")
        Dim CanUndockLegend As XmlAttribute = m_Doc.CreateAttribute("CanUndockLegend")
        Dim CanHidePreviewMap As XmlAttribute = m_Doc.CreateAttribute("CanHidePreviewMap")
        Dim CanHideLegend As XmlAttribute = m_Doc.CreateAttribute("CanHideLegend")
        Dim ShowCustomizeContextMenuStrip As XmlAttribute = m_Doc.CreateAttribute("ShowCustomizeContextMenuStrip")
        Dim CanPreviewMapDockLeft As XmlAttribute = m_Doc.CreateAttribute("CanPreviewMapDockLeft")
        Dim CanLegendDockLeft As XmlAttribute = m_Doc.CreateAttribute("CanLegendDockLeft")
        Dim CanPreviewMapDockRight As XmlAttribute = m_Doc.CreateAttribute("CanPreviewMapDockRight")
        Dim CanLegendDockRight As XmlAttribute = m_Doc.CreateAttribute("CanLegendDockRight")
        Dim LoadTIFFandIMGasgridAttr As Xml.XmlAttribute = m_Doc.CreateAttribute("LoadTIFFandIMGasgrid")
        Dim LoadESRIAsGridAttr As Xml.XmlAttribute = m_Doc.CreateAttribute("LoadESRIAsGrid")
        Dim MouseWheelBehavior As Xml.XmlAttribute = m_Doc.CreateAttribute("MouseWheelBehavior")
        Dim TransparentSelectionAttr As Xml.XmlAttribute = m_Doc.CreateAttribute("TransparentSelection")
        Dim LabelsUseProjectLevel As Xml.XmlAttribute = m_Doc.CreateAttribute("LabelsUseProjectLevel")
        ' state of dock windows
        Dim LegendVisible As Xml.XmlAttribute = m_Doc.CreateAttribute("LegendVisible")
        Dim PreviewVisible As Xml.XmlAttribute = m_Doc.CreateAttribute("PreviewVisible")

        'set the properties
        With frmMain
            If .WindowState = FormWindowState.Maximized Then
                WindowState.InnerText = CInt(.WindowState.Maximized).ToString
                LocationX.InnerText = "692"
                LocationY.InnerText = "531"
                WindowWidth.InnerText = "163"
                WindowHeight.InnerText = "68"
            ElseIf .WindowState = FormWindowState.Normal Then
                WindowState.InnerText = CInt(FormWindowState.Normal).ToString
                LocationX.InnerText = .Location.X.ToString()
                LocationY.InnerText = .Location.Y.ToString()
                WindowWidth.InnerText = .Width.ToString()
                WindowHeight.InnerText = .Height.ToString()
            ElseIf .WindowState = FormWindowState.Minimized Then
                WindowState.InnerText = CInt(FormWindowState.Minimized).ToString
                LocationX.InnerText = "692"
                LocationY.InnerText = "531"
                WindowWidth.InnerText = "163"
                WindowHeight.InnerText = "68"
            End If

            LoadTIFFandIMGasgridAttr.InnerText = AppInfo.LoadTIFFandIMGasgrid.ToString()
            LoadESRIAsGridAttr.InnerText = AppInfo.LoadESRIAsGrid.ToString()
            MouseWheelBehavior.InnerText = AppInfo.MouseWheelZoom.ToString()
            LabelsUseProjectLevel.InnerText = AppInfo.LabelsUseProjectLevel.ToString()

            'Save the preview map and legend prop
            CanUndockPreviewMap.InnerText = frmMain.g_PreviewMapProp.CanUndock.ToString 'frmMain.dockMan.Bars("dwPreviewMap").CanUndock.ToString
            CanPreviewMapDockRight.InnerText = frmMain.g_PreviewMapProp.CanDockRight.ToString 'frmMain.dockMan.Bars("dwPreviewMap").CanDockRight.ToString
            CanPreviewMapDockLeft.InnerText = frmMain.g_PreviewMapProp.CanDockLeft.ToString 'frmMain.dockMan.Bars("dwPreviewMap").CanDockLeft.ToString
            CanHidePreviewMap.InnerText = frmMain.g_PreviewMapProp.CanHide.ToString 'frmMain.dockMan.Bars("dwPreviewMap").CanHide.ToString

            CanUndockLegend.InnerText = frmMain.g_LegendProp.CanUndock.ToString 'frmMain.dockMan.Bars("dwLegend").CanUndock.ToString
            CanHideLegend.InnerText = frmMain.g_LegendProp.CanHide.ToString 'frmMain.dockMan.Bars("dwLegend").CanHide.ToString
            CanLegendDockLeft.InnerText = frmMain.g_LegendProp.CanDockLeft.ToString 'frmMain.dockMan.Bars("dwLegend").CanDockLeft.ToString
            CanLegendDockRight.InnerText = frmMain.g_LegendProp.CanDockRight.ToString 'frmMain.dockMan.Bars("dwLegend").CanDockRight.ToString

            'save the view back color
            ViewColor.InnerText = MapWinUtility.Colors.ColorToInteger(AppInfo.DefaultBackColor).ToString
            TransparentSelectionAttr.InnerText = TransparentSelection.ToString()

            LegendVisible.InnerText = frmMain.legendPanel.Visible
            ' PM, 18 jan. 2011, Added check if previewPanel is created:
            If Not frmMain.previewPanel Is Nothing Then
                PreviewVisible.InnerText = frmMain.previewPanel.Visible
            Else
                PreviewVisible.InnerText = False
            End If

        End With

        'add attributes to the view
        View.Attributes.Append(LabelsUseProjectLevel)
        View.Attributes.Append(TransparentSelectionAttr)
        View.Attributes.Append(WindowWidth)
        View.Attributes.Append(WindowHeight)
        View.Attributes.Append(LocationX)
        View.Attributes.Append(LocationY)
        View.Attributes.Append(WindowState)
        View.Attributes.Append(ViewColor)
        View.Attributes.Append(CanUndockPreviewMap)
        View.Attributes.Append(CanUndockLegend)
        View.Attributes.Append(CanHidePreviewMap)
        View.Attributes.Append(CanHideLegend)
        View.Attributes.Append(ShowCustomizeContextMenuStrip)
        View.Attributes.Append(CanPreviewMapDockLeft)
        View.Attributes.Append(CanLegendDockLeft)
        View.Attributes.Append(CanPreviewMapDockRight)
        View.Attributes.Append(CanLegendDockRight)
        View.Attributes.Append(LoadTIFFandIMGasgridAttr)
        View.Attributes.Append(LoadESRIAsGridAttr)
        View.Attributes.Append(MouseWheelBehavior)
        View.Attributes.Append(LegendVisible)
        View.Attributes.Append(PreviewVisible)

        Parent.AppendChild(View)
    End Sub

    ''' <summary>
    ''' Adds the plugins to the configuration file.
    ''' </summary>
    ''' <param name="m_Doc"></param>
    ''' <param name="Parent"></param>
    ''' <param name="LoadingConfig"></param>
    Private Sub AddPluginsElement(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement, ByVal LoadingConfig As Boolean)
        Dim Plugins As XmlElement = m_Doc.CreateElement("Plugins")
        Dim Plugin As Interfaces.PluginInfo

        Dim ar As Collection = frmMain.m_PluginManager.LoadedPlugins
        'Note that collections start at 1 for some bizarre reason
        For i As Integer = 1 To ar.Count
            Plugin = CType(frmMain.m_PluginManager.PluginsList(MapWinUtility.PluginManagementTools.GenerateKey(ar(i).GetType())), Interfaces.PluginInfo)
            AddPluginElement(m_Doc, ar(i), Plugin.Key, Plugins, LoadingConfig)
        Next

        Parent.AppendChild(Plugins)
    End Sub

    ''' <summary>
    ''' Adds information for a single plugin to the configuration file.
    ''' </summary>
    ''' <param name="m_Doc"></param>
    ''' <param name="Plugin"></param>
    ''' <param name="PluginKey"></param>
    ''' <param name="Parent"></param>
    ''' <param name="LoadingConfig"></param>
    Private Sub AddPluginElement(ByRef m_Doc As Xml.XmlDocument, ByVal Plugin As Object, ByVal PluginKey As String, ByVal Parent As XmlElement, ByVal LoadingConfig As Boolean)
        Dim NewPlugin As XmlElement = m_Doc.CreateElement("Plugin")
        Dim SettingsString As XmlAttribute = m_Doc.CreateAttribute("SettingsString")
        Dim KeyXML As XmlAttribute = m_Doc.CreateAttribute("Key")
        Dim SetString As String = ""

        'Plugin properties
        If LoadingConfig = False Then
            'Saving project
            If TypeOf Plugin Is MapWindow.Interfaces.IPlugin Or TypeOf Plugin Is MapWindow.PluginInterfaces.IProjectEvents Then
                Plugin.ProjectSaving(ProjectFileName, SetString)
            Else
                SetString = ""
            End If
        End If

        SettingsString.InnerText = SetString
        KeyXML.InnerText = PluginKey

        NewPlugin.Attributes.Append(SettingsString)
        NewPlugin.Attributes.Append(KeyXML)

        Parent.AppendChild(NewPlugin)
    End Sub

    ''' <summary>
    ''' Adds information about application required plugins to the config file XML
    ''' </summary>
    ''' <param name="m_Doc"></param>
    ''' <param name="Parent"></param>
    ''' <param name="LoadingConfig"></param>
    Private Sub AddApplicationPluginsElement(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement, ByVal LoadingConfig As Boolean)

        Dim Plugins As XmlElement = m_Doc.CreateElement("ApplicationPlugins")
        Dim Dir As XmlAttribute = m_Doc.CreateAttribute("PluginDir")
        Dim Plugin As Interfaces.IPlugin
        Dim Item As DictionaryEntry

        'save the application dir
        Dir.InnerText = Me.GetRelativePath(AppInfo.ApplicationPluginDir, ConfigFileName)

        'save all of the application plugins
        For Each Item In frmMain.m_PluginManager.m_ApplicationPlugins
            If Not Item.Value Is Nothing Then
                If TypeOf Item.Value Is Interfaces.IPlugin Then
                    Plugin = CType(Item.Value, Interfaces.IPlugin)
                    AddPluginElement(m_Doc, Plugin, Item.Key.ToString(), Plugins, LoadingConfig)
                Else
                    Plugin = CType(Item.Value, PluginInterfaces.IBasePlugin)
                    AddPluginElement(m_Doc, Plugin, Item.Key.ToString(), Plugins, LoadingConfig)
                End If
            End If
        Next

        Plugins.Attributes.Append(Dir)
        Parent.AppendChild(Plugins)
    End Sub

    ''' <summary>
    ''' Adds info about the color pallettes to the config xml
    ''' </summary>
    Private Sub AddColorPalettes(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)

        Try
            Dim ColorPalettes As XmlElement = m_Doc.CreateElement("ColorPalettes")

            If Not frmMain.g_ColorPalettes Is Nothing Then
                Dim docFragment As Xml.XmlDocumentFragment = m_Doc.CreateDocumentFragment
                docFragment.InnerXml = frmMain.g_ColorPalettes.InnerXml

                ColorPalettes.AppendChild(docFragment)
            End If

            Parent.AppendChild(ColorPalettes)
        Catch ex As System.Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

End Class
