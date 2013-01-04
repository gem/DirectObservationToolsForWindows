'********************************************************************************************************
'File Name: AppSetGrid.vb
'Description: Main GUI interface for the MapWindow application-level settings.
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
'5/4/2008 - Jiri Kadlec (jk) - created this class for a separate property grid. The "Category", "Display Name"
'                              and "Description" entries are now stored in a resource file (AppSetGrid.resx) 
'                              enabling translation to other languages
'(code extracted from PrjSetGrid)
'5/8/2008 - (jk) - Display a list of currently supported MapWindow languages in the "Language" property
'7/28/2011 - Teva - Display MapWindow version on the setting dialog box.
'7/28/2011 - Teva - Display MapWindow floating scalebar on the setting dialog box.
'8/1/2011 - Teva - Show or hide the label for the redraw speed on the settings dialog box.
'8/3/2011 - Teva - Added the Avoid Collision property
'********************************************************************************************************
Option Strict Off
Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Drawing.Design
Imports System.Windows.Forms.Design
Imports MapWindow.PropertyGridUtils

<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter))> _
Public Class AppSetGrid

    'for retrieving localized message strings
    Private resources As System.Resources.ResourceManager = _
        New System.Resources.ResourceManager("MapWindow.GlobalResource", _
        System.Reflection.Assembly.GetExecutingAssembly())

    ' DISPLAY OPTIONS -- Map Background Color
    ' Background color of main map (application-level). This setting can
    ' be overridden by project-level map background color
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property BackgroundColor() As System.Drawing.Color
        Get
            Dim AppC As Color = AppInfo.DefaultBackColor
            Return Color.FromArgb(AppC.A, AppC.R, AppC.G, AppC.B)
            Return AppInfo.DefaultBackColor '5/5/2008 JK
        End Get
        Set(ByVal Value As System.Drawing.Color)
            'frmMain.MapMain.CtlBackColor = Value
            'Bug 767 - for some reason, this is necessary in order to pick up
            'system colors. Makes sense somewhat given how the object gets translated
            'to the OCX later on.
            If modMain.ProjInfo.UseDefaultBackColor = True Then
                frmMain.View.BackColor = Color.FromArgb(Value.A, Value.R, Value.G, Value.B)
                frmMain.SetModified(True)
            End If
            AppInfo.DefaultBackColor = Color.FromArgb(Value.A, Value.R, Value.G, Value.B) '5/5/2008 JK
        End Set
    End Property

    ' DISPLAY OPTIONS -- Mouse Wheel Zoom
    ' Controls the direction of zoom when the mouse wheel is used (or none)
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory"), ReadOnlyAttribute(False)> _
    Public Property MouseWheelZoom() As MouseWheelZoomDir
        Get
            Return AppInfo.MouseWheelZoom
        End Get
        Set(ByVal value As MouseWheelZoomDir)
            AppInfo.MouseWheelZoom = value
        End Set
    End Property

    ' MapWindow Version -- DisplayMapWindow Version?
    ' Sets whether the MapWindow Version should be displayed when starting MapWindow.
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory")> _
    Public Property DisplayMapWindowVersion() As Boolean
        Get
            Return AppInfo.ShowMapWindowVersion
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowMapWindowVersion = Value
            frmMain.SetModified(True)
        End Set
    End Property


    'MapWindow Version -- DisplayMapWindow FloatingScalebar?
    ' Sets whether the floatingscalebar should be displayed when starting MapWindow.
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory")> _
    Public Property DisplayFloatingScalebar() As Boolean
        Get
            Return AppInfo.ShowFloatingScalebar
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowFloatingScalebar = Value
            frmMain.SetModified(True)
        End Set
    End Property


    'MapWindow Version -- DisplayMapWindow RedrawSpeed?
    ' Sets whether show/hide the redraw speed label when starting MapWindow.
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory")> _
    Public Property DisplayRedrawSpeed() As Boolean
        Get
            Return AppInfo.ShowHideRedrawSpeed
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowHideRedrawSpeed = Value
            frmMain.SetModified(True)
        End Set
    End Property



    'A quick hack to allow the propertygrid filename editor to be used to *save* files, not just open them
    Friend Class LogFileEditor
        Inherits UITypeEditor
        Private sfd As New SaveFileDialog()

        Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.Modal
        End Function

        Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
            sfd.FileName = value.ToString()
            sfd.Filter = "Log Files (*.log; *.txt)|*.txt;*.log"
            If sfd.ShowDialog() = DialogResult.OK Then
                Return sfd.FileName
            End If
            Return MyBase.EditValue(context, provider, value)
        End Function
    End Class

    ' DEBUG LOG -- Debug Log File
    ' Sets the location for a debug log to be written. If blank (empty) no log will be written. 
    ' Log messages are written using MapWinUtility.Logger.
    <Editor(GetType(LogFileEditor), GetType(System.Drawing.Design.UITypeEditor)), _
    GlobalizedProperty(CategoryId:="DebugLogCategory")> _
    Public Property DebugLog() As String
        Get
            Return AppInfo.LogfilePath
        End Get
        Set(ByVal value As String)
            AppInfo.LogfilePath = value
        End Set
    End Property

    ' cdm 1/22/06
    ' MAP BEHAVIOR -- Resize Behavior
    ' Controls how the map contents will behave when the window is resized.
    <GlobalizedProperty(CategoryId:="MapBehaviorCategory"), _
    ReadOnlyAttribute(False), _
    CLSCompliant(False)> _
    Public Property ResizeBehavior() As MapWinGIS.tkResizeBehavior
        Get
            Return frmMain.MapMain.MapResizeBehavior
        End Get
        Set(ByVal Value As MapWinGIS.tkResizeBehavior)
            frmMain.MapMain.MapResizeBehavior = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' DISPLAY OPTIONS -- Symbology behavior
    ' Determines the loading behavior (random, default, user prompting)
    <GlobalizedProperty(CategoryId:="MapBehaviorCategory"), _
    PropertyOrder(2), ReadOnlyAttribute(False)> _
    Public Property SymbologyBehavior() As SymbologyBehavior
        Get
            Return AppInfo.SymbologyLoadingBehavior
        End Get
        Set(ByVal Value As SymbologyBehavior)
            AppInfo.SymbologyLoadingBehavior = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' MAP BEHAVIOR -- Project-Level Labels
    ' Determines whether labels are loaded from and saved to a project-specific label 
    ' file or from a shapefile-specific label file. Using a project-level label file will create 
    ' a new subdirectory with the project's name.
    <GlobalizedProperty(CategoryId:="MapBehaviorCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property LabelsUseProjectLevel() As Boolean
        Get
            Return AppInfo.LabelsUseProjectLevel
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.LabelsUseProjectLevel = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' cdm 3/13/2006
    ' FILE FORMATS -- TIFF/IMG/DEM Loading Behavior
    ' Determines whether a TIFF or an IMG file should be loaded as a grid or as an image, 
    ' or automatically determined.
    <GlobalizedProperty(CategoryId:="FileFormatCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property GeoTIFFAndImgBehavior() As GeoTIFFAndImgBehavior
        Get
            Return AppInfo.LoadTIFFandIMGasgrid
        End Get
        Set(ByVal Value As GeoTIFFAndImgBehavior)
            AppInfo.LoadTIFFandIMGasgrid = Value
            frmMain.SetModified(True)
        End Set
    End Property


    ' FILE FORMATS -- ArcInfo Grid Loading Behavior
    ' Determines whether an ESRI ArcInfo grid should be loaded as a grid or as an image.
    <GlobalizedProperty(CategoryId:="FileFormatCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ESRIBehavior() As ESRIBehavior
        Get
            Return AppInfo.LoadESRIAsGrid
        End Get
        Set(ByVal Value As ESRIBehavior)
            AppInfo.LoadESRIAsGrid = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' Sets avoid collision when starting MapWindow.
    <GlobalizedProperty(CategoryId:="ShapeFileCategory")> _
    Public Property DisplayAvoidCollision() As Boolean
        Get
            Return AppInfo.ShowAvoidCollision
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowAvoidCollision = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' Sets auto create spatial index when starting MapWindow.
    <GlobalizedProperty(CategoryId:="ShapeFileCategory")> _
    Public Property DisplayAutoCreatespatialindex() As Boolean
        Get
            Return AppInfo.ShowAutoCreatespatialindex
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowAutoCreatespatialindex = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' WELCOME SCREEN -- Display Welcome Screen?
    ' Sets whether the welcome screen should be displayed when starting MapWindow.
    <GlobalizedProperty(CategoryId:="WelcomeScreenCategory")> _
    Public Property DisplayWelcome() As Boolean
        Get
            Return AppInfo.ShowWelcomeScreen
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowWelcomeScreen = Value
            frmMain.SetModified(True)
        End Set
    End Property



    ' SHOW DYNAMIC VISIBILITY WARNINGS -- 
    ' Sets whether dynamic visibility warnings will be shown when turning off dynvis layer
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory")> _
    Public Property DisplayDynVisWarning() As Boolean
        Get
            Return AppInfo.ShowDynamicVisibilityWarnings
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowDynamicVisibilityWarnings = Value
            frmMain.SetModified(True)
        End Set
    End Property



    ' SHOW DYNAMIC VISIBILITY WARNINGS -- 
    ' Sets whether dynamic visibility warnings will be shown when turning off dynvis layer
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory")> _
    Public Property ShowLayerAfterDynVis() As Boolean
        Get
            Return AppInfo.ShowLayerAfterDynamicVisibility
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowLayerAfterDynamicVisibility = Value
            frmMain.SetModified(True)
        End Set
    End Property


    ' jk 3/31/2008
    ' LANGUAGE SETTINGS -- Enable user-specified language
    ' Override the system regional and language settings. 
    ' This change will take effect after restarting MapWindow.

    <GlobalizedProperty(CategoryId:="LanguageSettingsCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property OverrideSystemLocale() As Boolean
        Get
            Return modMain.AppInfo.OverrideSystemLocale
        End Get
        Set(ByVal value As Boolean)
            modMain.AppInfo.OverrideSystemLocale = value
        End Set
    End Property

    ' jk 3/31/2008
    ' LANGUAGE SETTINGS -- Language
    ' The user-specified language to use in MapWindow. This change will take effect after restarting MapWindow.

    <GlobalizedProperty(CategoryId:="LanguageSettingsCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(CultureCls))> _
    Public Property Locale() As String
        Get
            Return New CultureInfo(modMain.AppInfo.Locale).EnglishName
        End Get
        Set(ByVal value As String)
            MapWinUtility.Logger.Msg(String.Format(resources.GetString("msgChangeLanguage.Text"), value), _
                resources.GetString("msgChangeLanguage.Title"))
            modMain.AppInfo.Locale = CultureCls.FindCulture(value).ToString
            'modMain.AppInfo.Locale = System.Globalization.CultureInfo.GetCultureInfo(value).ToString
        End Set
    End Property

    '3/31/2008 added by JK
    ' 5/7/2008 - modified JK - only the currently supported cultures with an existing translation
    ' are displayed in the list.
    'Support class for selecting a user-specified MapWindow language
    Public Class CultureCls
        Inherits StringConverter

        'support combo box style select
        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        'generate the list for selection
        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection

            'the list will contain the names of all currently translated cultures
            Dim list As New ArrayList
            Dim files As System.IO.FileInfo()
            Dim sateliteName As String = "MapWindow.resources.dll"

            Dim execDir As New System.IO.DirectoryInfo(BinFolder)
            Dim cult As CultureInfo
            For Each subDir As System.IO.DirectoryInfo In execDir.GetDirectories()
                files = subDir.GetFiles()
                If files.Length > 0 Then
                    If files(0).Name = sateliteName Then
                        cult = New CultureInfo(subDir.Name)
                        list.Add(cult.EnglishName)
                    End If
                End If
            Next
            list.Sort()
            Return New StandardValuesCollection(list)

        End Function

        'do not need the values exclusive to each other
        Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
            Return False
        End Function

        'find a culture, given the english name
        'if not found, return the current culture as specified by operating system
        Public Shared Function FindCulture(ByVal engName As String) As CultureInfo
            For Each culture As CultureInfo In CultureInfo.GetCultures(CultureTypes.AllCultures)
                If culture.EnglishName = engName Then
                    Return culture
                End If
            Next
            Return CultureInfo.CurrentUICulture
        End Function

    End Class
End Class
