'********************************************************************************************************
'Filename:      clsXMLProjectFile.vb
'Description:   Friend class that contains functions for reading and wrting project and config files.
'This class has been updated to manage project files and to provide a globally available instance of the 
'class that is used to hold all global project related variables.  In prior versions of MapWindow (3.x) 
'this the global variables were stored in a variety of disparate places including the main MapWindow form.  
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
'Last Update:   1/12/2005, dpa
'3/23/2005 fixed Recent Projects menu, mgray
'6/9/2005 fixed grid loading to not rebuild the image every time - dpa
'7/21/2005 Added functionality to warn the user if a layer is missing when a project is being loaded, asking them if they'd like to locate said file. - Chris Michaelis 
'9/19/2005 Added functionality to overwrite the default "DefaultDir"
'4/29/2007 Tom Shanley (tws) added save/restore of shape-level formatting; and setting to control that
'3/31/2008 Jiri Kadlec (jk) Added option to specify language (overrides default Windows Regional and Language settings)
'5/8/2008 (jk) changed the default location of default.mwcfg, mapwindowdock.config and mwLanguage.config files
'5/26/2008 Jiri Kadlec (jk) Prevented LoadPreviewMap() from displaying an error message when a project doesn't
'                           have a preview map image specified, corrected handling of .mwcfg configuration files.
'5/27/2008 Jiri Kadlec (jk) When an existing project is loaded, try to find a project-specific .mwcfg file. If it's
'                           not found, recreate it from default.mwcfg.
'8/6/2008  Brian Marchionni When deploying custom made applications using the core MapWindow application the mapwindow.mwcfg file
'                           would be save to Documents and Settings\[User name]\Application Data\MapWindow\mapwindow.mwcfg this has been
'                           changed to save to Documents and Settings\[User name]\Application Data\[executables filename -.exe]\mapwindow.mwcfg
'9/6/2008 Jiri Kadlec (jk)  Prevented infinite loop in CreateConfigFileFromDefault() when default.mwcfg was missing
'10/6/2009 Paul Meems       Fixing bug #1312 (Dropping missing layer doesn't set the 'changes made' trigger)
'11/6/2009 Paul Meems       Placing hard-coded messages to resource file for translation
'20/8/2009 Paul Meems       Fixing bug #1124 (Plugin ProjectLoading only called if plugin is in project's <Plugins>)
'10/12/2009 Mark Gray       Fixed issue caused by previous fix: plugins did not get their settingsString in ProjectLoading event any more if already loaded before project
'10/03/2010 Chrisrian Degrassi  Improvements on BookmarkViews
'********************************************************************************************************

Imports System.Xml
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Partial Friend Class XmlProjectFile
    'Private Variables
    Private p_Doc As New XmlDocument
    Private m_ErrorOccured As Boolean
    Private m_ErrorMsg As String = "The following errors occured:" + Chr(13) + Chr(13)
    Private m_ForcedModified As Boolean = False 'Paul Meems Bug #1312

    'for retrieving localized message strings
    Private resources As System.Resources.ResourceManager = _
        New System.Resources.ResourceManager("MapWindow.GlobalResource", _
        System.Reflection.Assembly.GetExecutingAssembly())

    'Public Variables 
    Public ProjectFileName As String
    Public ConfigFileName As String
    Public ConfigLoaded As Boolean
    Public Modified As Boolean
    Public RecentProjects As New Collections.ArrayList
    Public m_MapUnits As String 'Meters, Feet, etc
    Public ShowStatusBarCoords_Projected As Boolean = True 'Default to true
    Public ShowStatusBarCoords_Alternate As String = "(None)" 'Default to true
    Public StatusBarCoordsNumDecimals As Integer = 3
    Public StatusBarAlternateCoordsNumDecimals As Integer = 3
    Public StatusBarCoordsUseCommas As Boolean = True
    Public StatusBarAlternateCoordsUseCommas As Boolean = True
    Public NoPromptToSendErrors As Boolean = False
    Public SaveShapeSettings As Boolean = False ' default false >> no surprises for other plugin developers doing shape-level formatting
    Public BookmarkedViews As New ArrayList
    Public TransparentSelection As Boolean = True
    Public ProjectBackColor As Color = Color.White 'map background color of MapWindow project (jk 5/10/2008)
    Public UseDefaultBackColor As Boolean = True 'true if the default (application-level) background color is used
    Public Shared m_MainToolbarButtons As New Hashtable
    'Public ProjectProjection As String 'PROJ4 string
    ' project projection
    Public GeoProjection As New MapWinGIS.GeoProjection

    Public Property ProjectProjection() As String
        Get
            Return GeoProjection.ExportToProj4()
        End Get
        Set(ByVal value As String)
            If Not GeoProjection.ImportFromProj4(value) Then
                GeoProjection.ImportFromWKT(value)
            End If
        End Set
    End Property

    Public Property ProjectProjectionWKT() As String
        Get
            Return GeoProjection.ExportToWKT()
        End Get
        Set(ByVal value As String)
            GeoProjection.ImportFromWKT(value)
        End Set
    End Property


#Region "Bookmarked view class"
    ''' <summary>
    ''' Holds named extents of the map
    ''' </summary>
    Public Class BookmarkedView
        Public _Name As String
        Public _Exts As MapWinGIS.Extents

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New(ByVal BookmarkName As String, ByVal BookmarkExts As MapWinGIS.Extents)
            _Name = BookmarkName
            _Exts = BookmarkExts
        End Sub

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New(ByVal BookmarkName As String, ByVal xMin As Double, ByVal yMin As Double, ByVal xMax As Double, ByVal yMax As Double)
            _Name = BookmarkName
            _Exts = New MapWinGIS.Extents
            _Exts.SetBounds(xMin, yMin, 0, xMax, yMax, 0)
        End Sub

        ''' <summary>
        ''' Name of the bookmark
        ''' </summary>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        ''' <summary>
        ''' Extents associated with bookmark
        ''' </summary>
        Public Property Exts() As MapWinGIS.Extents
            Get
                Return _Exts
            End Get
            Set(ByVal value As MapWinGIS.Extents)
                _Exts = value
            End Set
        End Property

        ''' <summary>
        ''' Custom serialization
        ''' </summary>
        Public Overrides Function ToString() As String
            Return (Name + " (" & Exts.xMin.ToString() + ", " + Exts.xMax.ToString() + "), (" + Exts.yMin.ToString() + ", " + Exts.yMax.ToString() + ")")
        End Function
    End Class
#End Region

#Region "Path and location"
    Public ReadOnly Property DefaultConfigFile() As String
        Get
            Return System.IO.Path.Combine(BinFolder, "default.mwcfg")
        End Get
    End Property

    Public ReadOnly Property UserConfigFile() As String
        Get
            Return System.IO.Path.Combine(GetApplicationDataDir(), "mapwindow.mwcfg")
        End Get
    End Property

    ''' <summary>
    ''' save the configuration files to "Application Data" Special folder.
    ''' The folder which contains MapWindow binaries in "Program Files" may be read-only on some shared
    ''' Windows systems (#bug 691). This function tries to create a directory "Application Data\MapWindow"
    ''' (usually located in "Documents and Settings\[User name]\Application Data"). If it fails, the folder
    ''' of MW executable file is used for storing the configuration files.
    ''' the mapwindow.mwcfg file now saves to "Documents and Settings\[User name]\Application Data\[executables name - .exe]\mapwindow.mwcfg"
    ''' </summary>
    Public Shared Function GetApplicationDataDir() As String
        Dim AppDataDir As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        Dim ExecutableName As String = Left(System.Windows.Forms.Application.ExecutablePath, System.Windows.Forms.Application.ExecutablePath.Length - 4).Remove(0, System.Windows.Forms.Application.StartupPath.Length + 1)
        Try
            AppDataDir = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ExecutableName)
            If Not System.IO.Directory.Exists(AppDataDir) Then
                MapWinUtility.Logger.Dbg("Creating MapWindow Application Data Directory: " + AppDataDir)
                IO.Directory.CreateDirectory(AppDataDir)
            End If
        Catch e As IO.IOException
            MapWinUtility.Logger.Dbg("Save Configuration - MapWindow Application Data Directory: Exception: " + e.ToString())
        Catch
        End Try
        Return AppDataDir
    End Function
#End Region

#Region "Save Project File"
    ''' <summary>
    ''' Saves the project file. Returns true on success.
    ''' This function saves XML project files. Expects a current frmMain object from which to grab some info.
    ''' </summary>
    Public Function SaveProject() As Boolean

        If (frmMain.MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            Return SaveProjectNew()
        End If

        Dim node As XmlElement
        Dim Ver As String
        Dim ConfigPath As XmlAttribute

        If Len(ProjectFileName) = 0 Then
            Return False
            Exit Function
        End If

        Try
            Ver = App.VersionString()

            '**** add the following elements to "mwprj" ****
            p_Doc = New XmlDocument
            Dim prjName As String = frmMain.Text.Replace("'", "")

            Dim type As String

            'p_Doc.LoadXml("<Mapwin name='" + System.Web.HttpUtility.UrlEncode(prjName) + "' type='projectfile' version='" + System.Web.HttpUtility.UrlEncode(Ver) + "'></Mapwin>")
            type = "projectfile." & IIf(frmMain.MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology, 1, 0)
            p_Doc.LoadXml("<Mapwin name='" + System.Web.HttpUtility.UrlEncode(prjName) + "' type='" & type & "' version='" + System.Web.HttpUtility.UrlEncode(Ver) + "'></Mapwin>")
            node = p_Doc.DocumentElement

            'Add the configuration path
            ConfigPath = p_Doc.CreateAttribute("ConfigurationPath")
            ConfigPath.InnerText = GetRelativePath(ConfigFileName, ProjectFileName)
            node.Attributes.Append(ConfigPath)

            'Add the projection
            Dim proj As Xml.XmlAttribute = p_Doc.CreateAttribute("ProjectProjection")
            proj.InnerText = ProjectProjection
            node.Attributes.Append(proj)

            Dim projWKT As Xml.XmlAttribute = p_Doc.CreateAttribute("ProjectProjectionWKT")
            projWKT.InnerText = ProjectProjectionWKT
            node.Attributes.Append(projWKT)

            'Add the map units
            Dim mapunit As Xml.XmlAttribute = p_Doc.CreateAttribute("MapUnits")
            mapunit.InnerText = modMain.frmMain.Project.MapUnits
            node.Attributes.Append(mapunit)

            'Add the status bar coord customizations
            Dim xStatusBarAlternateCoordsNumDecimals As Xml.XmlAttribute = p_Doc.CreateAttribute("StatusBarAlternateCoordsNumDecimals")
            xStatusBarAlternateCoordsNumDecimals.InnerText = StatusBarAlternateCoordsNumDecimals.ToString()
            node.Attributes.Append(xStatusBarAlternateCoordsNumDecimals)
            Dim xStatusBarCoordsNumDecimals As Xml.XmlAttribute = p_Doc.CreateAttribute("StatusBarCoordsNumDecimals")
            xStatusBarCoordsNumDecimals.InnerText = StatusBarCoordsNumDecimals.ToString()
            node.Attributes.Append(xStatusBarCoordsNumDecimals)
            Dim xStatusBarAlternateCoordsUseCommas As Xml.XmlAttribute = p_Doc.CreateAttribute("StatusBarAlternateCoordsUseCommas")
            xStatusBarAlternateCoordsUseCommas.InnerText = StatusBarAlternateCoordsUseCommas.ToString()
            node.Attributes.Append(xStatusBarAlternateCoordsUseCommas)
            Dim xStatusBarCoordsUseCommas As Xml.XmlAttribute = p_Doc.CreateAttribute("StatusBarCoordsUseCommas")
            xStatusBarCoordsUseCommas.InnerText = StatusBarCoordsUseCommas.ToString()
            node.Attributes.Append(xStatusBarCoordsUseCommas)

            Dim ShowFloatingScaleBar As Xml.XmlAttribute = p_Doc.CreateAttribute("ShowFloatingScaleBar")
            ShowFloatingScaleBar.InnerText = frmMain.m_FloatingScalebar_Enabled.ToString()
            node.Attributes.Append(ShowFloatingScaleBar)

            Dim FloatingScaleBarPosition As Xml.XmlAttribute = p_Doc.CreateAttribute("FloatingScaleBarPosition")
            FloatingScaleBarPosition.InnerText = frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition
            node.Attributes.Append(FloatingScaleBarPosition)

            Dim FloatingScaleBarUnit As Xml.XmlAttribute = p_Doc.CreateAttribute("FloatingScaleBarUnit")
            FloatingScaleBarUnit.InnerText = frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit
            node.Attributes.Append(FloatingScaleBarUnit)

            Dim FloatingScaleBarForecolor As Xml.XmlAttribute = p_Doc.CreateAttribute("FloatingScaleBarForecolor")
            FloatingScaleBarForecolor.InnerText = frmMain.m_FloatingScalebar_ContextMenu_ForeColor.ToArgb().ToString()
            node.Attributes.Append(FloatingScaleBarForecolor)

            Dim FloatingScaleBarBackcolor As Xml.XmlAttribute = p_Doc.CreateAttribute("FloatingScaleBarBackcolor")
            FloatingScaleBarBackcolor.InnerText = frmMain.m_FloatingScalebar_ContextMenu_BackColor.ToArgb().ToString()
            node.Attributes.Append(FloatingScaleBarBackcolor)

            'Add the map resize behavior
            Dim resizebehavior As Xml.XmlAttribute = p_Doc.CreateAttribute("MapResizeBehavior")
            resizebehavior.InnerText = CType(modMain.frmMain.MapMain.MapResizeBehavior, Short).ToString()
            node.Attributes.Append(resizebehavior)

            'Add whether to display various coordinate systems in the status bar
            Dim coord_projected As Xml.XmlAttribute = p_Doc.CreateAttribute("ShowStatusBarCoords_Projected")
            coord_projected.InnerText = ShowStatusBarCoords_Projected.ToString()
            node.Attributes.Append(coord_projected)
            Dim coord_alternate As Xml.XmlAttribute = p_Doc.CreateAttribute("ShowStatusBarCoords_Alternate")
            coord_alternate.InnerText = ShowStatusBarCoords_Alternate
            node.Attributes.Append(coord_alternate)

            'Add the save shape settings behavior
            Dim saveshapesettinfgsbehavior As Xml.XmlAttribute = p_Doc.CreateAttribute("SaveShapeSettings")
            saveshapesettinfgsbehavior.InnerText = Me.SaveShapeSettings.ToString()
            node.Attributes.Append(saveshapesettinfgsbehavior)

            'Add the project-level map background color settings (5/4/2008 added by JK)
            Dim backColor_useDefault As Xml.XmlAttribute = p_Doc.CreateAttribute("ViewBackColor_UseDefault")
            backColor_useDefault.InnerText = UseDefaultBackColor.ToString
            node.Attributes.Append(backColor_useDefault)
            Dim backColor As Xml.XmlAttribute = p_Doc.CreateAttribute("ViewBackColor")
            backColor.InnerText = (MapWinUtility.Colors.ColorToInteger(ProjectBackColor)).ToString
            node.Attributes.Append(backColor)

            'Add this project to the list of recent projects
            AddToRecentProjects(ProjectFileName)

            'Add the list of the plugins to the project file
            AddPluginsElement(p_Doc, node, False)

            'Add the application plugins
            AddApplicationPluginsElement(p_Doc, node, False)

            'Add extents of map
            AddExtentsElement(p_Doc, node, frmMain.MapMain.Extents)

            'Add the layers
            AddLayers(p_Doc, node)

            'Add view bookmarks
            AddBookmarks(p_Doc, node)

            'Add the properies fo the preview Map to the project file
            AddPreViewMapElement(p_Doc, node)

            'Save the project file.
            MapWinUtility.Logger.Dbg("Saving Project: " + ProjectFileName)
            Try
                p_Doc.Save(ProjectFileName)
                frmMain.SetModified(False)
                Return True
            Catch e As System.UnauthorizedAccessException
                Dim ro As Boolean = False
                If System.IO.File.Exists(ProjectFileName) Then
                    Dim fi As New System.IO.FileInfo(ProjectFileName)
                    If fi.IsReadOnly Then ro = True
                End If
                If ro Then
                    'MapWinUtility.Logger.Msg("The project file could not be saved because it is read-only." + Environment.NewLine + Environment.NewLine + "Please have your system administrator grant write access to the file:" + Environment.NewLine + ProjectFileName, MsgBoxStyle.Exclamation, "Read-Only File")
                    'Paul Meems 6/11/2009
                    Dim msg As String = String.Format(frmMain.resources.GetString("msgProjectReadOnly.Text"), ProjectFileName)
                    MapWinUtility.Logger.Msg(msg, MsgBoxStyle.Exclamation, resources.GetString("msgProjectReadOnly.Title"))
                Else
                    'MapWinUtility.Logger.Msg("The project file could not be saved due to insufficient access." + Environment.NewLine + Environment.NewLine + "Please have your system administrator grant access to the file:" + Environment.NewLine + ProjectFileName, MsgBoxStyle.Exclamation, "Insufficient Access")
                    'Paul Meems 6/11/2009
                    Dim msg As String = String.Format(resources.GetString("msgProjectInsufficientAccess.Text"), ProjectFileName)
                    MapWinUtility.Logger.Msg(msg, MsgBoxStyle.Exclamation, resources.GetString("msgProjectInsufficientAccess.Title"))
                End If
                Return False
            End Try
        Catch ex As System.Exception
            ShowError(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Serializes bookmarks (saved extents of map)
    ''' </summary>
    Private Sub AddBookmarks(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        Dim bookmarksElem As XmlElement = m_Doc.CreateElement("Bookmarks")

        For i As Integer = 0 To BookmarkedViews.Count - 1
            Dim bm As XmlElement = m_Doc.CreateElement("Bookmark")
            Dim attr As XmlAttribute = m_Doc.CreateAttribute("Name")
            attr.InnerText = BookmarkedViews(i).name
            bm.Attributes.Append(attr)
            AddExtentsElement(m_Doc, bm, BookmarkedViews(i).exts)

            bookmarksElem.AppendChild(bm)
        Next

        Parent.AppendChild(bookmarksElem)
    End Sub

    ''' <summary>
    ''' Adds extents information to the project XML
    ''' </summary>
    Private Sub AddExtentsElement(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement, ByVal Exts As MapWinGIS.Extents)
        Dim Extents As XmlElement = m_Doc.CreateElement("Extents")
        Dim xMax As XmlAttribute = m_Doc.CreateAttribute("xMax")
        Dim yMax As XmlAttribute = m_Doc.CreateAttribute("yMax")
        Dim xMin As XmlAttribute = m_Doc.CreateAttribute("xMin")
        Dim yMin As XmlAttribute = m_Doc.CreateAttribute("yMin")

        ' Paul Meems, 23 Oct. 2009
        ' Make in Culture Invariant:
        With Exts
            xMax.InnerText = .xMax.ToString(System.Globalization.CultureInfo.InvariantCulture)
            yMax.InnerText = .yMax.ToString(System.Globalization.CultureInfo.InvariantCulture)
            xMin.InnerText = .xMin.ToString(System.Globalization.CultureInfo.InvariantCulture)
            yMin.InnerText = .yMin.ToString(System.Globalization.CultureInfo.InvariantCulture)
        End With

        Extents.Attributes.Append(xMax)
        Extents.Attributes.Append(yMax)
        Extents.Attributes.Append(xMin)
        Extents.Attributes.Append(yMin)

        Parent.AppendChild(Extents)
    End Sub

    ''' <summary>
    ''' Adds preview map information to the project xml
    ''' </summary>
    Private Sub AddPreViewMapElement(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)

        Dim prevMap As XmlElement = m_Doc.CreateElement("PreviewMap")
        Dim visible As XmlAttribute = m_Doc.CreateAttribute("Visible")
        Dim dx As XmlAttribute = m_Doc.CreateAttribute("dx")
        Dim dy As XmlAttribute = m_Doc.CreateAttribute("dy")
        Dim xllcenter As XmlAttribute = m_Doc.CreateAttribute("xllcenter")
        Dim yllcenter As XmlAttribute = m_Doc.CreateAttribute("yllcenter")

        If (frmMain.MapPreview.NumLayers > 0) Then
            Dim img As MapWinGIS.Image = CType(frmMain.MapPreview.get_GetObject(0), MapWinGIS.Image)
            With img
                dx.InnerText = .dX.ToString
                dy.InnerText = .dY.ToString
                xllcenter.InnerText = .XllCenter.ToString
                yllcenter.InnerText = .YllCenter.ToString
            End With
        Else
            dx.InnerText = "0"
            dy.InnerText = "0"
            xllcenter.InnerText = "0"
            yllcenter.InnerText = "0"
        End If

        prevMap.Attributes.Append(dx)
        prevMap.Attributes.Append(dy)
        prevMap.Attributes.Append(xllcenter)
        prevMap.Attributes.Append(yllcenter)

        'set the properties'
        SaveImage(m_Doc, frmMain.PreviewMap.Picture, prevMap)

        'add the elements to the prevMap
        Parent.AppendChild(prevMap)
    End Sub

    ''' <summary>
    ''' Adds information about groups and layers in the project
    ''' </summary>
    Private Sub AddLayers(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        'Add info about the current layers to the XML project file
        Dim Groups As XmlElement = m_Doc.CreateElement("Groups")
        Dim Layers As XmlElement
        Dim Group As XmlElement
        Dim Name As XmlAttribute
        Dim Expanded As XmlAttribute
        Dim Position As XmlAttribute
        Dim LayerPos As XmlAttribute = m_Doc.CreateAttribute("Position")
        Dim NumGroups, numLayers As Integer
        Dim LHandle As Integer
        Dim g, l As Integer

        'Add all groups and their layers
        NumGroups = frmMain.Legend.Groups.Count
        For g = 0 To NumGroups - 1
            Group = m_Doc.CreateElement("Group")
            Name = m_Doc.CreateAttribute("Name")
            Expanded = m_Doc.CreateAttribute("Expanded")
            Position = m_Doc.CreateAttribute("Position")

            'Add the properties of the element
            Name.InnerText = frmMain.Legend.Groups(g).Text
            Expanded.InnerText = frmMain.Legend.Groups(g).Expanded.ToString
            Position.InnerText = g.ToString
            Group.Attributes.Append(Name)
            Group.Attributes.Append(Expanded)
            Group.Attributes.Append(Position)
            SaveImage(m_Doc, frmMain.Legend.Groups(g).Icon, Group)

            'Add all the layers under this group
            numLayers = frmMain.Legend.Groups(g).LayerCount
            If (numLayers > 0) Then
                Layers = m_Doc.CreateElement("Layers")
                For l = 0 To numLayers - 1
                    If Not frmMain.Legend.Groups(g)(l).SkipOverDuringSave Then
                        LHandle = frmMain.Legend.Groups(g)(l).Handle

                        If (frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
                            AddLayerElement(m_Doc, frmMain.Layers(LHandle), Layers)
                        Else

                            Dim xmlFile As New clsXMLLayerFile
                            Dim xelLayer As XmlElement = xmlFile.Layer2XML(frmMain.Layers(LHandle), m_Doc, ProjectFileName)
                            Layers.AppendChild(xelLayer)
                        End If
                    End If
                Next
                Group.AppendChild(Layers)
            End If
            Groups.AppendChild(Group)
        Next
        Parent.AppendChild(Groups)
    End Sub

    ''' <summary>
    ''' Serializes properties of the particular layer
    ''' </summary>
    Friend Sub AddLayerElement(ByRef m_doc As Xml.XmlDocument, ByVal mapWinLayer As Interfaces.Layer, ByVal parent As Xml.XmlNode)

        Dim layer As XmlElement = m_doc.CreateElement("Layer")
        Dim name As XmlAttribute = m_doc.CreateAttribute("Name")
        Dim groupname As XmlAttribute = m_doc.CreateAttribute("GroupName")
        Dim type As XmlAttribute = m_doc.CreateAttribute("Type")
        Dim path As XmlAttribute = m_doc.CreateAttribute("Path")
        Dim tag As XmlAttribute = m_doc.CreateAttribute("Tag")
        Dim legPic As XmlAttribute = m_doc.CreateAttribute("LegendPicture")
        Dim visible As XmlAttribute = m_doc.CreateAttribute("Visible")
        Dim labelsVisible As XmlAttribute = m_doc.CreateAttribute("LabelsVisible")
        Dim expanded As XmlAttribute = m_doc.CreateAttribute("Expanded")

        'set the properties of the elements
        name.InnerText = mapWinLayer.Name()
        groupname.InnerText = frmMain.Layers.Groups.ItemByHandle(mapWinLayer.GroupHandle).Text
        type.InnerText = CInt(mapWinLayer.LayerType).ToString
        tag.InnerText = mapWinLayer.Tag()
        visible.InnerText = mapWinLayer.Visible().ToString
        labelsVisible.InnerText = mapWinLayer.LabelsVisible().ToString
        expanded.InnerText = mapWinLayer.Expanded.ToString
        SaveImage(m_doc, mapWinLayer.Icon, layer)
        'SaveImage(m_doc, mapWinLayer.UserPointType.Picture, layer, "MapImage")      ' Attention: this was commented

        'check to see if there is a grid associated with this layer
        Dim fileName As String = mapWinLayer.FileName

        If (IO.File.Exists(mapWinLayer.FileName) = False) Then
            'check to see if this layers is in memory if so prompt to save
            'Dim results As Microsoft.VisualBasic.MsgBoxResult
            'Dim nameF As String

            'Only case this is still true for is ECWP, and the functionality never worked anyway - commented out
            'results = MapWinUtility.Logger.Msg(.Name + " file is in memory do you want to save it", MsgBoxStyle.YesNo Or MsgBoxStyle.Information, "Save Layer")
            'Paul Meems 6/11/2009
            'Dim msg As String = String.Format(resources.GetString("msgSaveInMemoryLayer.Text"), .Name)
            'results = MapWinUtility.Logger.Msg(msg, MsgBoxStyle.YesNo Or MsgBoxStyle.Information, resources.GetString("msgSaveInMemoryLayer.Title"))

            'If (results = MsgBoxResult.Yes) Then
            '    nameF = Me.SaveLayer(mapWinLayer, .Handle, mapWinLayer.LayerType)
            '    If (nameF <> "") Then
            '        fileName = nameF
            '    End If
            'End If
        End If

        If Len(fileName) <> 0 Then
            path.InnerText = GetRelativePath(fileName, ProjectFileName)
        Else
            path.InnerText = GetRelativePath(mapWinLayer.FileName, ProjectFileName)
        End If

        'add the elements to the layer node
        layer.Attributes.Append(name)
        layer.Attributes.Append(groupname)
        layer.Attributes.Append(type)
        layer.Attributes.Append(path)
        layer.Attributes.Append(tag)
        layer.Attributes.Append(legPic)
        layer.Attributes.Append(visible)
        layer.Attributes.Append(labelsVisible)
        layer.Attributes.Append(expanded)

        If TypeOf (mapWinLayer.GetObject) Is MapWinGIS.IShapefile Then
            'if it is a shapfile then add the shape properties to the layer
            AddShapeFileElement(m_doc, mapWinLayer, layer)
        ElseIf TypeOf (mapWinLayer.GetObject) Is MapWinGIS.IImage Or TypeOf (mapWinLayer.GetObject) Is MapWinGIS.Grid Then
            'add the grid file properties
            AddGridElement(m_doc, mapWinLayer, layer)
        End If

        'add DynamicVisibility options
        AddDynamicVisibility(m_doc, mapWinLayer, layer)

        'add the layer to the parent
        parent.AppendChild(layer)
    End Sub

    ''' <summary>
    ''' Serializes dynamic visiblity settings for the layer
    ''' </summary>
    Private Sub AddDynamicVisibility(ByRef m_Doc As Xml.XmlDocument, ByVal mapWinLayer As MapWindow.Interfaces.Layer, ByVal parent As Xml.XmlNode)

        'add DynamicVisibility options
        Dim dynamicVisibility As XmlElement = m_Doc.CreateElement("DynamicVisibility")
        Dim useDynamicVisibility As XmlAttribute = m_Doc.CreateAttribute("UseDynamicVisibility")
        Dim Scale As XmlAttribute = m_Doc.CreateAttribute("Scale")

        'DynamicVisibility prop
        With mapWinLayer
            If (Not frmMain.m_AutoVis(.Handle) Is Nothing) Then
                useDynamicVisibility.InnerText = frmMain.m_AutoVis(.Handle).UseDynamicExtents.ToString
                Scale.InnerText = frmMain.m_AutoVis(.Handle).DynamicScale.ToString()
            Else
                useDynamicVisibility.InnerText = CStr(False)
                Scale.InnerText = "0"
            End If
        End With

        'add DynamicVisibility
        dynamicVisibility.Attributes.Append(useDynamicVisibility)
        dynamicVisibility.Attributes.Append(Scale)

        'add the layer to the parent
        parent.AppendChild(dynamicVisibility)
    End Sub
#End Region

#Region "Load Project File"
    ''' <summary>
    '''  Loads project XML file with a specified name
    ''' </summary>
    Public Function LoadProject(ByVal Filename As String, Optional ByVal LayersOnly As Boolean = False, Optional ByVal LayersIntoGroup As String = "") As Boolean

        g_SyncPluginMenuDefer = True

        If Not IO.File.Exists(Filename) Then Return False
        If Not TranslateLegacyVWR(Filename) Then Return False

        Dim odir As String = System.IO.Directory.GetCurrentDirectory()

        Try
            BookmarkedViews.Clear()
            frmMain.BuildBookmarkedViewsMenu()

            'Set default directory to folder containing current project
            ChDir(System.IO.Path.GetDirectoryName(Filename))
            AppInfo.DefaultDir = CurDir()

            'add the project to the most recent projects
            Me.AddToRecentProjects(Filename)

            If Not LayersOnly Then
                frmMain.Layers.Clear()
                frmMain.Legend.Groups.Clear()
                frmMain.ClearPreview()
                frmMain.m_AutoVis = New DynamicVisibilityClass()
            End If

            Dim Doc As New XmlDocument
            Doc.Load(Filename)
            Dim Root As XmlElement = Doc.DocumentElement

            ' Treating different versions of projects
            Dim version As Integer = 0
            If Root.HasAttribute("type") Then
                If Root.Attributes("type").InnerText.EndsWith(".1") Then version = 1
                If Root.Attributes("type").InnerText.EndsWith(".2") Then version = 2
            End If

            ' in the new version all the properties are moved lower
            Dim node As XmlElement
            If version = 2 Then
                node = Root.Item("MapWindow4")
                If node Is Nothing Then
                    MessageBox.Show("Corrupted file", frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                node = Root
            End If

            If (version = 1 And frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
                MessageBox.Show("Project version doesn't match MapWinGIS version." & vbNewLine & _
                                "You need a new version of MapWinGIS binaries to open this project.", "Version mismatch", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return True
            End If

            ' loads configuration
            Me.LoadMW4Settings(node, LayersOnly)

            frmMain.Legend.Lock()
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
            frmMain.Layers.StartAddingSession()

            Try
                'clear all of the Dynamic visibility layer
                If Not LayersOnly Then frmMain.m_AutoVis.Clear()

                'make sure we are in the proper directory so relative paths work
                ChDir(System.IO.Path.GetDirectoryName(Filename))

                If version = 2 Then
                    ' groups must be loaded prior loading the layers (ocs-based serialization)
                    Me.LoadGroupsNew(node.Item("Groups"))

                    ' loads layers
                    Dim nodeOcx As XmlNode = Root.Item("MapWinGIS")
                    If Not nodeOcx Is Nothing Then Me.LoadLayers(nodeOcx, node)

                    ' loading control specific options such as extents
                    Dim res As Boolean = frmMain.MapMain.DeserializeMapState(nodeOcx.OuterXml, False, Filename)
                ElseIf version < 2 Then

                    '  old version
                    Me.LoadGroups(node.Item("Groups"), version, LayersOnly, LayersIntoGroup)
                End If

                If Not LayersOnly Then
                    'load the extents
                    If version < 2 Then
                        Me.LoadExtents(node.Item("Extents"))
                    End If

                    'load the Preview Map
                    If Not LoadPreviewMap(node.Item("PreviewMap")) Then
                        m_ErrorOccured = True
                        m_ErrorMsg = "Could not load preview map"
                    End If

                    'load the Plugins causes it to call project loading
                    Me.LoadPlugins(node.Item("Plugins"), False)

                    'load the application plugins causes it to call project loading
                    Me.LoadApplicationPlugins(node.Item("ApplicationPlugins"), False)

                    'Load bookmarks
                    Me.LoadBookmarks(node)
                End If
            Finally
                If modMain.AppInfo.ProjectReloading Then
                    ' project is reloaded after reprojection so old extents can be invalid
                    modMain.AppInfo.ProjectReloading = False
                    frmMain.MapMain.ZoomToMaxExtents()
                End If

                frmMain.Layers.StopAddingSession(False)

                Do While frmMain.View.IsMapLocked()
                    frmMain.View.UnlockMap()
                Loop
                Do While frmMain.View.LegendControl.Locked
                    frmMain.View.LegendControl.Unlock()
                Loop
            End Try

            'BugZilla 315: Default directory should start set to project location
            AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(Filename)

            If Not LayersOnly Then
                frmMain.ResetViewState(frmMain.m_FloatingScalebar_Enabled)
                frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
                frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn
            End If

            'frmMain.UpdateButtons()

            'Paul Meems 10 june 2009 Fixing bug #1312:
            'Dropping missing layer doesn't set the 'changes made' trigger 
            If Not Me.m_ForcedModified Then
                frmMain.SetModified(False)
            End If

            g_SyncPluginMenuDefer = False
            frmMain.SynchPluginMenu()
            System.IO.Directory.SetCurrentDirectory(odir)
            ' Paul Meems 17 Sept 2009: Moved down
            ' Return True

        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadProject(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        Finally
            System.IO.Directory.SetCurrentDirectory(odir)

        End Try

        MapWinUtility.Logger.Status("")
        MapWinUtility.Logger.Dbg("Finished LoadProject")

        If m_ErrorOccured Then
            ' Paul Meems 10 Aug 2010: Don't show an error box, just log it:
            'MapWinUtility.Logger.Msg(m_ErrorMsg, MsgBoxStyle.Exclamation, "Project File Error Report")
            MapWinUtility.Logger.Dbg("Project File Error Report: " & m_ErrorMsg)
            m_ErrorOccured = False

            ' Paul Meems 10 Aug 2010: Let plugins know that the project file was loaded with errors.
            frmMain.FireProjectLoaded(Filename, True)
            frmMain.Plugins.BroadcastMessage("Project loaded with errors")
            Return False
        End If

        ' Paul Meems 17 Sept 2009: Let plugins know that the project file was loaded.
        frmMain.FireProjectLoaded(Filename, False)
        frmMain.Plugins.BroadcastMessage("Project loaded")
        Return True

    End Function

    ''' <summary>
    ''' Loads MW4 specific settings (located in the MapWin tag in the old format and MapWin.MapWindow4 in the new format)
    ''' </summary>
    Private Function LoadMW4Settings(ByVal node As XmlElement, ByVal LayersOnly As Boolean) As Boolean
        If node Is Nothing Then Return False

        Dim DefaultConfigFileName As String = Me.DefaultConfigFile
        Dim oldConfigPath As String = ConfigFileName

        If Not LayersOnly Then
            '**** Load the config file if it exists ********
            Dim NewConfigFile As String
            If node.HasAttribute("ConfigurationPath") Then
                NewConfigFile = node.Attributes("ConfigurationPath").InnerText
            Else
                NewConfigFile = Me.UserConfigFile
            End If
            Dim NewConfigPath As String = ""

            Try
                NewConfigPath = System.IO.Path.GetFullPath(NewConfigFile)
            Catch ex As Exception
                ' May-29-2008 Jiri Kadlec
                'if the path specified in the project file is not a valid path - use the default
                'read/write config file location
                MapWinUtility.Logger.Dbg("Loading configuration file " + NewConfigFile + " is not a valid path.")
                NewConfigPath = Me.UserConfigFile
                MapWinUtility.Logger.Dbg("Changed configuration file used by the project to " + NewConfigPath)
            End Try

            ' May-29-2008 Jiri Kadlec
            ' Check if the directory of the .mwcfg file specified by the project exists - if it doesn't exist, use the user
            ' .mwcfg file location in "Documents and Settings" instead.
            Dim NewConfigDir As String = System.IO.Path.GetDirectoryName(NewConfigPath)
            If Not System.IO.Directory.Exists(NewConfigDir) Then
                MapWinUtility.Logger.Dbg("Loading configuration - the directory" + NewConfigDir + " - does not exist.")
                NewConfigPath = Me.UserConfigFile
                MapWinUtility.Logger.Dbg("Changed configuration file used by the project to " + NewConfigPath)
            End If

            Try
                'If the location of NewConfigFile is in MW executable directory or if it's called 
                '"default.mwcfg" or "mapwindow.mwcfg", create and use a new file in
                '"Documents and Settings\User\ApplicationData\user.mwcfg" (Jiri Kadlec 5/26/2008)
                If NewConfigPath.ToLower() = DefaultConfigFileName.ToLower() Or _
                        NewConfigPath.ToLower().IndexOf("default.mwcfg") >= 0 Then
                    NewConfigPath = Me.UserConfigFile
                    CreateConfigFileFromDefault(NewConfigPath)
                End If

                If System.IO.File.Exists(NewConfigPath) Then

                    '5/26/2008 Jiri Kadlec
                    'check if the file "default.mwcfg" in MW executable directory has been modified 
                    '(for example, by a new MapWindow installation. 
                    'in that case, update the Configuration file with the content of default.mwcfg.
                    'for projects with a custom configuration file which doesn't contain a string
                    'default.mwcfg, mapwindow.mwcfg or user.mwcfg, don't do any modification.
                    If CompareFilesByTime(DefaultConfigFileName, NewConfigPath) > 0 And _
                    System.IO.Path.GetDirectoryName(NewConfigPath) = GetApplicationDataDir() Then
                        CreateConfigFileFromDefault(NewConfigPath)
                        ConfigFileName = NewConfigPath
                    End If

                    If oldConfigPath.ToLower <> NewConfigPath.ToLower Then
                        'The project has a different configuration file than the previous project - 
                        'save the old configFile before loading the new one
                        If Not ConfigFileName Is Nothing Then
                            MapWinUtility.Logger.Dbg("Configuration file name changed from " + oldConfigPath + _
                            " to " + NewConfigPath + " - running SaveConfig()") '5/26/2008 Jiri Kadlec
                            SaveConfig()
                        End If
                        ConfigFileName = NewConfigPath
                        LoadConfig(True)
                    End If
                Else
                    'the configuration file specified in the project settings does not exist -
                    'recreate it from the default configuration file .default.mwcfg
                    CreateConfigFileFromDefault(NewConfigFile)
                    MapWinUtility.Logger.Dbg("Configuration file " + NewConfigFile + _
                            " does not exist. Recreating " + ConfigFileName + " from default.") '5/26/2008 Jiri Kadlec
                    ConfigFileName = System.IO.Path.GetFullPath(NewConfigFile)
                    LoadConfig(True)
                End If
            Catch ex As Exception
                MapWinUtility.Logger.Msg("ERROR - no configuration path or error loading it" & ex.Message & " " & ex.StackTrace)
            End Try

            'Load the projection if it exists
            Dim proj As MapWinGIS.GeoProjection = New MapWinGIS.GeoProjection
            If node.HasAttribute("ProjectProjectionWKT") Then
                proj.ImportFromWKT(node.Attributes("ProjectProjectionWKT").InnerText)
            Else
                If node.HasAttribute("ProjectProjection") Then
                    If Not proj.ImportFromProj4(node.Attributes("ProjectProjection").InnerText) Then
                        proj.ImportFromWKT(node.Attributes("ProjectProjection").InnerText)
                    End If
                End If
            End If
            modMain.frmMain.Project.GeoProjection = proj

            'Load the map units if the setting exists
            Try
                m_MapUnits = node.Attributes("MapUnits").InnerText
            Catch ex As Exception
                m_MapUnits = ""
            End Try

            'Load the map background color if it exists
            Try
                If node.HasAttribute("ViewBackColor_UseDefault") AndAlso node.HasAttribute("ViewBackColor") Then
                    Dim useDefault As Boolean = Convert.ToBoolean(node.Attributes("ViewBackColor_UseDefault").InnerText)
                    If useDefault = False Then
                        UseDefaultBackColor = False
                        Dim backColorId As String = node.Attributes("ViewBackColor").InnerText
                        If backColorId <> "" Then
                            ProjectBackColor = MapWinUtility.Colors.IntegerToColor(Convert.ToInt32(backColorId))
                        End If
                    Else
                        ProjectBackColor = AppInfo.DefaultBackColor
                    End If
                Else
                    ProjectBackColor = AppInfo.DefaultBackColor
                End If
                frmMain.View.BackColor = Color.FromArgb(ProjectBackColor.A, ProjectBackColor.R, ProjectBackColor.G, ProjectBackColor.B)
            Catch
                ProjectBackColor = AppInfo.DefaultBackColor
                frmMain.View.BackColor = Color.FromArgb(ProjectBackColor.A, ProjectBackColor.R, ProjectBackColor.G, ProjectBackColor.B)
            End Try

            'Load the status bar coord customizations
            Try
                StatusBarAlternateCoordsNumDecimals = Integer.Parse(node.Attributes("StatusBarAlternateCoordsNumDecimals").InnerText)
                StatusBarCoordsNumDecimals = Integer.Parse(node.Attributes("StatusBarCoordsNumDecimals").InnerText)
                StatusBarAlternateCoordsUseCommas = Boolean.Parse(node.Attributes("StatusBarAlternateCoordsUseCommas").InnerText)
                StatusBarCoordsUseCommas = Boolean.Parse(node.Attributes("StatusBarCoordsUseCommas").InnerText)
            Catch ex As Exception
                StatusBarAlternateCoordsNumDecimals = 3
                StatusBarCoordsNumDecimals = 3
                StatusBarAlternateCoordsUseCommas = True
                StatusBarCoordsUseCommas = True
            End Try

            Try
                frmMain.m_FloatingScalebar_Enabled = Boolean.Parse(node.Attributes("ShowFloatingScaleBar").InnerText)
            Catch ex As Exception
                frmMain.m_FloatingScalebar_Enabled = False
            End Try

            ' Paul Meems 16 sept 2009, Bug 1412 
            ' It is possible the menue are removed by a plug-in. Adding check else a null pointer is thrown:
            If frmMain.m_Menu.m_MenuTable.ContainsKey(frmMain.m_Menu.MenuTableKey("mnuShowScaleBar")) Then
                frmMain.Menus("mnuShowScaleBar").Checked = frmMain.m_FloatingScalebar_Enabled
            End If

            Try
                frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = node.Attributes("FloatingScaleBarPosition").InnerText
            Catch ex As Exception
                frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = "LowerRight"
            Finally
                frmMain.m_FloatingScalebar_ContextMenu_UL.Checked = IIf(frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = "UpperLeft", True, False)
                frmMain.m_FloatingScalebar_ContextMenu_UR.Checked = IIf(frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = "UpperRight", True, False)
                frmMain.m_FloatingScalebar_ContextMenu_LL.Checked = IIf(frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = "LowerLeft", True, False)
                frmMain.m_FloatingScalebar_ContextMenu_LR.Checked = IIf(frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition = "LowerRight", True, False)
            End Try

            Try
                frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit = node.Attributes("FloatingScaleBarUnit").InnerText
            Catch ex As Exception
                frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit = "" 'No Override
            End Try

            Try
                frmMain.m_FloatingScalebar_ContextMenu_ForeColor = System.Drawing.Color.FromArgb(Integer.Parse(node.Attributes("FloatingScaleBarForecolor").InnerText))
            Catch ex As Exception
                frmMain.m_FloatingScalebar_ContextMenu_ForeColor = Color.Black
            End Try

            Try
                frmMain.m_FloatingScalebar_ContextMenu_BackColor = System.Drawing.Color.FromArgb(Integer.Parse(node.Attributes("FloatingScaleBarBackcolor").InnerText))
            Catch ex As Exception
                frmMain.m_FloatingScalebar_ContextMenu_BackColor = Color.White
            End Try

            'Load the map resize behavior if it exists
            Try
                modMain.frmMain.MapMain.MapResizeBehavior = CType(Short.Parse(node.Attributes("MapResizeBehavior").InnerText), MapWinGIS.tkResizeBehavior)
            Catch ex As Exception
                'Leave it at the default defined in CMap's constructor
            End Try

            'Load whether to display various coordinate systems in the status bar.
            'Default to true while doing this.
            Try
                ShowStatusBarCoords_Projected = Boolean.Parse(node.Attributes("ShowStatusBarCoords_Projected").InnerText)
            Catch ex As Exception
                ShowStatusBarCoords_Projected = True
            End Try
            Try
                ShowStatusBarCoords_Alternate = node.Attributes("ShowStatusBarCoords_Alternate").InnerText
            Catch ex As Exception
                ShowStatusBarCoords_Alternate = MapWindow.Interfaces.UnitOfMeasure.Kilometers.ToString()
            End Try
        End If

        ' load the SaveShapeSettings behavior
        Try
            Me.SaveShapeSettings = Boolean.Parse(node.Attributes("SaveShapeSettings").InnerText)
        Catch ex As Exception
            Me.SaveShapeSettings = False
        End Try
    End Function

    ''' <summary>
    ''' Loads the list of the recent projects
    ''' </summary>
    Private Sub LoadRecentProjects(ByVal RecentFiles As XmlElement)
        Try
            If (RecentFiles Is Nothing) Then Exit Sub

            Dim iChild As Integer
            Dim iRecentProject As Integer
            Dim path As String
            Dim pathLower As String
            Dim file As Xml.XmlNode
            Dim numChildNodes As Integer = RecentFiles.ChildNodes.Count

            'clear all previous files
            ProjInfo.RecentProjects.Clear()

            For iChild = 0 To numChildNodes - 1
                file = RecentFiles.ChildNodes(iChild)

                'get the full path of the file
                path = System.IO.Path.GetFullPath(file.InnerText)

                'Make sure we don't already have this project in the list.
                'Find a duplicate even if it has different capitalization.
                pathLower = path.ToLower
                iRecentProject = 0
                While iRecentProject < ProjInfo.RecentProjects.Count() AndAlso _
                    ProjInfo.RecentProjects.Item(iRecentProject).ToString.ToLower <> pathLower
                    iRecentProject += 1
                End While
                'iRecentProject = Count means we did not find a duplicate
                'Also, don't add recent projects that no longer exist
                If iRecentProject = ProjInfo.RecentProjects.Count() AndAlso _
                   System.IO.File.Exists(path) Then
                    ProjInfo.RecentProjects.Add(path)
                End If
            Next

            frmMain.BuildRecentProjectsMenu()

        Catch ex As System.Exception
            m_ErrorMsg += "Error: Loading the LoadRecentProjects" + Chr(13)
            m_ErrorOccured = True
            Exit Sub
        End Try
    End Sub

#Region "Loading"

    ''' <summary>
    ''' Loads extents of the map
    ''' </summary>
    Private Sub LoadExtents(ByVal ext As XmlElement)
        Dim extents As New MapWinGIS.Extents
        Dim xMax As Double
        Dim yMax As Double
        Dim xMin As Double
        Dim yMin As Double

        Try

            ' Paul Meems, 23 Oct. 2009
            ' Use InvariantCulture:
            xMin = MapWinUtility.MiscUtils.ParseDouble(ext.Attributes("xMin").InnerText, 0.0)   ' Double.Parse(ext.Attributes("xMin").InnerText.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture)
            yMin = MapWinUtility.MiscUtils.ParseDouble(ext.Attributes("yMin").InnerText, 0.0)   'Double.Parse(ext.Attributes("yMin").InnerText.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture)
            xMax = MapWinUtility.MiscUtils.ParseDouble(ext.Attributes("xMax").InnerText, 0.0)   'Double.Parse(ext.Attributes("xMax").InnerText.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture)
            yMax = MapWinUtility.MiscUtils.ParseDouble(ext.Attributes("yMax").InnerText, 0.0)   'Double.Parse(ext.Attributes("yMax").InnerText.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture)
            ' End modifications, Paul Meems, 23 Oct. 2009

            extents.SetBounds(xMin, yMin, 0, xMax, yMax, 0)

            frmMain.m_View.Extents = extents

        Catch e As System.Exception
            m_ErrorMsg += "Error: Loading the extents" + Chr(13)
            m_ErrorOccured = True
        End Try

    End Sub

    ''' <summary>
    ''' Restores the state of the preview window
    ''' </summary>
    Private Function LoadPreviewMap(ByVal previewMap As XmlElement) As Boolean

        ' TODO: Check if the preview map is needed (bug 1373)

        Try
            ' Verify that all fields are valid; Changed Or into OrElse and added checking for image
            If (previewMap.Item("Image").Attributes("Type").InnerText = String.Empty _
                OrElse previewMap.Attributes("dx").InnerText = String.Empty _
                OrElse previewMap.Attributes("dy").InnerText = String.Empty _
                OrElse previewMap.Attributes("xllcenter").InnerText = String.Empty _
                OrElse previewMap.Attributes("yllcenter").InnerText = String.Empty) Then
                Return True
            End If

            'get the extents of the preview Map
            Dim dx As Double = MapWinUtility.MiscUtils.ParseDouble(previewMap.Attributes("dx").InnerText, 1.0)  'CDbl(previewMap.Attributes("dx").InnerText)
            Dim dy As Double = MapWinUtility.MiscUtils.ParseDouble(previewMap.Attributes("dy").InnerText, 1.0)  'CDbl(previewMap.Attributes("dy").InnerText)
            Dim xllcenter As Double = MapWinUtility.MiscUtils.ParseDouble(previewMap.Attributes("xllcenter").InnerText, 0.0)  'CDbl(previewMap.Attributes("xllcenter").InnerText)
            Dim yllcenter As Double = MapWinUtility.MiscUtils.ParseDouble(previewMap.Attributes("yllcenter").InnerText, 0.0)  'CDbl(previewMap.Attributes("yllcenter").InnerText)

            ' Paul Meems: Why not use a Using statement for img?
            Dim type As String = previewMap.Item("Image").Attributes("Type").InnerText
            Dim img As New System.Drawing.Bitmap(CType(ConvertStringToImage(previewMap.Item("Image").InnerText, type), System.Drawing.Image))

            If (Not img Is Nothing) Then
                Dim Image As New MapWinGIS.Image
                Image.dX = dx
                Image.dY = dy
                Image.XllCenter = xllcenter
                Image.YllCenter = yllcenter
                Image.DownsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic
                Image.UpsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic

                Dim cvter As New MapWinUtility.ImageUtils
                Image.Picture = CType(cvter.ImageToIPictureDisp(CType(img, Image)), stdole.IPictureDisp)
                RestorePreviewMap(Image)
                Image = Nothing 'Prevent GC
            End If

            Return True
        Catch e As System.Exception
            MapWinUtility.Logger.Dbg("Error in LoadPreviewMap(), Message: " + e.Message + Chr(13))
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Shows image in the preview image
    ''' </summary>
    ''' <param name="image"></param>
    ''' <remarks></remarks>
    Public Sub RestorePreviewMap(ByRef image As MapWinGIS.Image)
        Try
            frmMain.MapPreview.LockWindow(MapWinGIS.tkLockMode.lmLock)
            frmMain.MapPreview.RemoveAllLayers()
            frmMain.MapPreview.AddLayer(image, True)
            frmMain.MapPreview.ExtentPad = 0
            frmMain.MapPreview.ZoomToMaxExtents()
            frmMain.m_PreviewMap.m_ShowLocatorBox = True
            frmMain.m_PreviewMap.UpdateLocatorBox()
        Catch ex As Exception
            g_error = ex.Message
            Throw (ex)
        Finally
            frmMain.MapPreview.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        End Try
    End Sub

    ''' <summary>
    ''' Loads view properties
    ''' </summary>
    Private Function LoadView(ByVal view As XmlElement) As Boolean
        Try
            frmMain.WindowState = CType(view.Attributes("WindowState").InnerText, Windows.Forms.FormWindowState)

            If (Len(view.Attributes("ViewBackColor").InnerText) <> 0) Then
                frmMain.g_ViewBackColor = CInt(view.Attributes("ViewBackColor").InnerText)
                Dim backColor As System.Drawing.Color
                backColor = MapWinUtility.Colors.IntegerToColor(frmMain.g_ViewBackColor)
                AppInfo.DefaultBackColor = backColor '4/5/2008 added by JK
                frmMain.View.BackColor = backColor
            End If

            If frmMain.WindowState = FormWindowState.Normal Then
                Dim w As Integer = CInt(view.Attributes("WindowWidth").InnerText)
                Dim h As Integer = CInt(view.Attributes("WindowHeight").InnerText)
                Dim drawPoint As New System.Drawing.Point(CInt(view.Attributes("LocationX").InnerText), CInt(view.Attributes("LocationY").InnerText))
                FindSafeWindowLocation(w, h, drawPoint)
                frmMain.Width = w
                frmMain.Height = h
                frmMain.Location = drawPoint
            End If

            Try
                Select Case view.Attributes("LoadTIFFandIMGasgrid").InnerText
                    Case GeoTIFFAndImgBehavior.Automatic.ToString()
                        AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.Automatic
                    Case GeoTIFFAndImgBehavior.LoadAsImage.ToString()
                        AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsImage
                    Case GeoTIFFAndImgBehavior.LoadAsGrid.ToString()
                        AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsGrid
                    Case Else
                        'Default
                        AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.Automatic
                End Select
            Catch ex As Exception
                AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.Automatic
            End Try

            Try
                Select Case view.Attributes("MouseWheelBehavior").InnerText
                    Case MouseWheelZoomDir.NoAction.ToString()
                        AppInfo.MouseWheelZoom = MouseWheelZoomDir.NoAction
                    Case MouseWheelZoomDir.WheelUpZoomsOut.ToString()
                        AppInfo.MouseWheelZoom = MouseWheelZoomDir.WheelUpZoomsOut
                    Case MouseWheelZoomDir.WheelUpZoomsIn.ToString()
                        AppInfo.MouseWheelZoom = MouseWheelZoomDir.WheelUpZoomsIn
                End Select
            Catch ex As Exception
                AppInfo.MouseWheelZoom = MouseWheelZoomDir.WheelUpZoomsIn
            End Try

            Try
                Select Case view.Attributes("LoadESRIAsGrid").InnerText
                    Case ESRIBehavior.LoadAsImage.ToString()
                        AppInfo.LoadESRIAsGrid = ESRIBehavior.LoadAsImage
                    Case ESRIBehavior.LoadAsGrid.ToString()
                        AppInfo.LoadESRIAsGrid = ESRIBehavior.LoadAsGrid
                    Case Else
                        AppInfo.LoadESRIAsGrid = ESRIBehavior.LoadAsGrid
                End Select
            Catch ex As Exception
                AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsGrid ' ESRIBehavior.LoadAsGrid
            End Try

            Try
                AppInfo.LabelsUseProjectLevel = Boolean.Parse(view.Attributes("LabelsUseProjectLevel").InnerText)
            Catch ex As Exception
                AppInfo.LabelsUseProjectLevel = False
            End Try

            If frmMain.WindowState = FormWindowState.Minimized Then
                frmMain.WindowState = FormWindowState.Normal
            End If

            Try
                Boolean.TryParse(view.Attributes("TransparentSelection").InnerText, TransparentSelection)
            Catch
                TransparentSelection = True
            End Try

            ' legend panel visiblity
            Dim visible As Boolean = True
            Try
                Boolean.TryParse(view.Attributes("LegendVisible").InnerText, visible)
            Catch
                visible = True
            End Try
            frmMain.UpdateLegendPanel(visible)

            ' preview visiblity
            Try
                Boolean.TryParse(view.Attributes("PreviewVisible").InnerText, visible)
            Catch
                visible = True
            End Try
            frmMain.UpdatePreviewPanel(visible)

            Dim col As ICollection = frmMain.m_UIPanel.m_Panels.Keys()
            For Each item As String In col
                frmMain.m_UIPanel.SetPanelVisible(item, True)
            Next item

        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadView(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
            LoadView = False
            Exit Function
        End Try

        LoadView = True
    End Function

    ''' <summary>
    ''' Loads bookmarks (saved extents)
    ''' </summary>
    Private Sub LoadBookmarks(ByVal view As XmlElement)
        Dim nl As XmlNodeList = view.GetElementsByTagName("Bookmark")
        For Each x As XmlNode In nl
            Try
                If x.ChildNodes.Count > 0 Then
                    Dim exts As New MapWinGIS.Extents
                    Dim xMax As Double = MapWinUtility.MiscUtils.ParseDouble(x.ChildNodes(0).Attributes("xMax").InnerText, 0.0)  'CDbl(x.ChildNodes(0).Attributes("xMax").InnerText)
                    Dim yMax As Double = MapWinUtility.MiscUtils.ParseDouble(x.ChildNodes(0).Attributes("yMax").InnerText, 0.0)  'CDbl(x.ChildNodes(0).Attributes("yMax").InnerText)
                    Dim xMin As Double = MapWinUtility.MiscUtils.ParseDouble(x.ChildNodes(0).Attributes("xMin").InnerText, 0.0)  'CDbl(x.ChildNodes(0).Attributes("xMin").InnerText)
                    Dim yMin As Double = MapWinUtility.MiscUtils.ParseDouble(x.ChildNodes(0).Attributes("yMin").InnerText, 0.0)  'CDbl(x.ChildNodes(0).Attributes("yMin").InnerText)
                    exts.SetBounds(xMin, yMin, 0, xMax, yMax, 0)
                    Dim bm As New BookmarkedView(x.Attributes("Name").InnerText, exts)
                    BookmarkedViews.Add(bm)
                End If
            Catch
            End Try
        Next

        If BookmarkedViews.Count > 0 Then frmMain.BuildBookmarkedViewsMenu()
    End Sub

#End Region

#Region "Load Layers Functions"

    Private Sub LoadGroups(ByVal groups As XmlElement, ByVal ProjectVersion As String, Optional ByVal LayersOnly As Boolean = False, Optional ByVal LayersIntoGroup As String = "")
        Dim hGroup As Integer

        'set the loading project so i can control the progress bar
        'frmMain.m_LoadingProject = True

        'find the total number of layers to be loaded
        Dim totalNumLayers As Integer, group As XmlElement, numLayersLoaded As Integer = 0
        For Each group In groups
            If (Not group.Item("Layers") Is Nothing) Then
                totalNumLayers += group.Item("Layers").ChildNodes.Count
            End If
        Next
        MapWinUtility.Logger.Dbg("In LoadGroups. Number of layers: " & totalNumLayers)

        If Not String.IsNullOrEmpty(LayersIntoGroup) Then
            hGroup = frmMain.Legend.Groups.Add(LayersIntoGroup, frmMain.Legend.Groups.Count)
            frmMain.Legend.Groups.ItemByHandle(hGroup).Expanded = True
        End If

        For Each group In groups
            Try
                If Not LayersOnly Then
                    'set the group properties
                    hGroup = frmMain.Legend.Groups.Add(group.Attributes("Name").InnerText, CInt(group.Attributes("Position").InnerText))
                    frmMain.Legend.Groups.ItemByHandle(hGroup).Expanded = CBool(group.Attributes("Expanded").InnerText)

                    'set the group icon
                    If (Len(group.Item("Image").InnerText) > 0) Then
                        frmMain.Legend.Groups.ItemByHandle(hGroup).Icon = ConvertStringToImage(group.Item("Image").InnerText, group.Item("Image").Attributes("Type").InnerText)
                    End If
                End If

                If ProjectVersion < 2 Then
                    If Not group.Item("Layers") Is Nothing Then
                        If group.Item("Layers").ChildNodes.Count > 0 Then
                            For Each lLayer As XmlElement In group.Item("Layers").ChildNodes
                                Try
                                    If ProjectVersion = 0 Then
                                        ' old layer options
                                        frmMain.MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmStandard
                                        LoadLayerProperties(lLayer)
                                    ElseIf ProjectVersion = 1 Then
                                        Dim xmlFile As New clsXMLLayerFile
                                        xmlFile.LoadLayer(lLayer)
                                        frmMain.MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology
                                    ElseIf ProjectVersion = 2 Then
                                        LoadLayerProperties(lLayer)
                                    End If

                                    numLayersLoaded += 1
                                    MapWinUtility.Logger.Progress(numLayersLoaded, totalNumLayers)
                                Catch e As System.Exception
                                    If Not m_ErrorMsg.Contains(e.Message) Then
                                        m_ErrorMsg &= "Error in LoadLayers(), Message: " & e.Message & Chr(13)
                                    End If
                                    m_ErrorOccured = True
                                End Try
                            Next
                            'test the AutoVis
                            frmMain.m_AutoVis.TestLayerZoomExtents()
                        End If
                    End If
                End If

            Catch e As System.Exception
                If Not m_ErrorMsg.Contains(e.Message) Then
                    m_ErrorMsg &= "Error in LoadGroups(), Message: " & e.Message & Chr(13)
                End If
                m_ErrorOccured = True
            End Try
        Next

        'done loading layers 
        'frmMain.m_LoadingProject = False
    End Sub

    ''' <summary>
    ''' Loads layer and it's properties (old-style project)
    ''' </summary>
    ''' <param name="layer"></param>
    ''' <param name="ExistingLayerHandle"></param>
    ''' <param name="PluginCall"></param>
    ''' <remarks></remarks>
    Friend Sub LoadLayerProperties(ByVal layer As XmlNode, Optional ByVal ExistingLayerHandle As Integer = -1, Optional ByVal PluginCall As Boolean = False)
        Static LoopPrevention As Boolean = False
        If (LoopPrevention) Then Return

        Try
            Dim filePath As String = layer.Attributes("Path").InnerText
            Dim name As String = layer.Attributes("Name").InnerText
            Dim groupname As String = "" 'New element as of 8/2/2007
            Try
                If Not layer.Attributes("GroupName") Is Nothing Then
                    groupname = layer.Attributes("GroupName").InnerText
                End If
            Catch
            End Try
            Dim layerVisible As Boolean = CBool(layer.Attributes("Visible").InnerText)
            Dim type As Integer = CInt(layer.Attributes("Type").InnerText)
            Dim expanded As Boolean = CBool(layer.Attributes("Expanded").InnerText)
            Dim tag As String = layer.Attributes("Tag").InnerText
            Dim labelsVisible As Boolean = CBool(layer.Attributes("LabelsVisible").InnerText)
            Dim handle As Integer
            Dim imageType As String
            Dim gridScheme As MapWinGIS.GridColorScheme = Nothing

            'set the panel text for the progress bar
            'If Not m_panel Is Nothing Then m_panel.Text = "Loading " & name & "..."
            MapWinUtility.Logger.Status("Loading " & name & "...")

            If type = MapWindow.Interfaces.eLayerType.LineShapefile Or type = MapWindow.Interfaces.eLayerType.PointShapefile Or type = MapWindow.Interfaces.eLayerType.PolygonShapefile Then
                'add shapefile layer
                With layer.Item("ShapeFileProperties")
                    Dim color As Integer = CInt(.Attributes("Color").InnerText)
                    Dim outlineColor As Integer = CInt(.Attributes("OutLineColor").InnerText)
                    Dim drawFill As Boolean = CBool(.Attributes("DrawFill").InnerText)
                    Dim lineOrPointSize As Single = CSng(.Attributes("LineOrPointSize").InnerText)
                    Dim pointType As MapWinGIS.tkPointType = CType(.Attributes("PointType").InnerText, MapWinGIS.tkPointType)
                    Dim lineStipple As MapWinGIS.tkLineStipple = CType(.Attributes("LineStipple").InnerText, MapWinGIS.tkLineStipple)
                    Dim fillStipple As MapWinGIS.tkFillStipple = CType(.Attributes("FillStipple").InnerText, MapWinGIS.tkFillStipple)

                    Dim fillStippleLineColor As Color = Drawing.Color.Black
                    If .Attributes("FillStippleLineColor") IsNot Nothing Then fillStippleLineColor = System.Drawing.Color.FromArgb(Integer.Parse(.Attributes("FillStippleLineColor").InnerText))

                    Dim fillStippleTransparent As Boolean = True
                    If .Attributes("FillStippleTransparent") IsNot Nothing Then fillStippleTransparent = Boolean.Parse(.Attributes("FillStippleTransparent").InnerText)

                    Dim transPercent As Single = 1
                    Try
                        transPercent = CSng(.Attributes("TransparencyPercent").InnerText)
                    Catch
                    End Try
                    Dim userPointType As New MapWinGIS.Image
                    userPointType.Picture = CType(ConvertStringToImage(.Item("CustomPointType").Item("Image").InnerText, .Item("CustomPointType").Item("Image").Attributes("Type").InnerText), stdole.IPictureDisp)

                    If (Not userPointType Is Nothing) Then
                        If (.Attributes("UseTransparency").InnerText = "") Then
                            userPointType.UseTransparencyColor = False
                        Else
                            userPointType.UseTransparencyColor = CBool(.Attributes("UseTransparency").InnerText)
                        End If

                        If (.Attributes("TransparencyColor").InnerText = "") Then
                            userPointType.TransparencyColor = Convert.ToUInt32(0)
                        Else
                            userPointType.TransparencyColor = Convert.ToUInt32(.Attributes("TransparencyColor").InnerText)
                        End If
                    End If

                    'Need to move the layer?
                    Try
                        LoopPrevention = True
                        If ExistingLayerHandle = -1 Then
                            'We are adding this layer from within a project file.
                            'Don't change the ordering because the project file
                            'specifies this; but still add the layer
                            'Debugging: MsgBox("No move")

                            'make sure the file exists
                            'Chris Michaelis July 26 05 - changed to warn the user and ask if they'd like to find it.
                            ' Old: If (System.IO.File.Exists(filePath) = False) Then Exit Sub

                            If (System.IO.File.Exists(filePath) = False) Then
                                If filePath.ToLower().Trim().StartsWith("ecwp://") Then
                                    handle = frmMain.m_layers.AddLayer(filePath, name, , layerVisible, color, outlineColor, drawFill, lineOrPointSize, pointType)(0).Handle
                                Else
                                    ' ThisF operation will have changed the current working directory.
                                    ' Preserve it and set it back, or all subsequent layers will not be found either.
                                    Dim cwd As String = CurDir()

                                    ' PromptBrowse will set the file path by reference and return true,
                                    ' or return false if the user cancels.
                                    If Not PromptToBrowse(filePath, name) Then
                                        'Paul Meems 10 june 2009 Fixing bug #1312:
                                        'Dropping missing layer doesn't set the 'changes made' trigger 
                                        Me.m_ForcedModified = True
                                        Exit Sub
                                    End If


                                    'add layer
                                    handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filePath), name, , layerVisible, color, outlineColor, drawFill, lineOrPointSize, pointType)(0).Handle

                                    'Restore CWD
                                    ChDir(cwd)
                                End If
                            Else
                                'add layer
                                handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filePath), name, , layerVisible, color, outlineColor, drawFill, lineOrPointSize, pointType)(0).Handle
                            End If
                            Else
                                'Debugging: MsgBox("move")
                                'Added...!
                                handle = ExistingLayerHandle

                                'Determine if we need to move it
                                Dim destGroup As Integer = -1
                                'Is the saved group in the list of groups?
                                For iz As Integer = 0 To frmMain.Layers.Groups.Count - 1
                                    If frmMain.Layers.Groups(iz).Text.ToLower().Trim() = groupname.ToLower.Trim() And Not groupname.Trim() = "" Then
                                        destGroup = frmMain.Layers.Groups(iz).Handle
                                        Exit For
                                    End If
                                Next

                                'We don't try to do this if the layer was added via a plug-in.
                                'We trust plug-ins to handle positioning layers appropriately,
                                'esp. as most have specific requirements there.
                                If Not PluginCall Then
                                    If destGroup = -1 Then
                                        'Create the group -- see BugZilla 529, requested by ATC
                                        destGroup = frmMain.Layers.Groups.Add(groupname.Trim(), 0)
                                    End If

                                    frmMain.Layers.MoveLayer(handle, 0, destGroup)
                                End If
                            End If
                    Catch ex As Exception
                        Debug.WriteLine("Error in LoadLayerProperties: " & ex.ToString)
                    Finally
                        LoopPrevention = False
                    End Try

                    frmMain.Layers(handle).Name = name
                    frmMain.Layers(handle).LineStipple = lineStipple
                    frmMain.Layers(handle).FillStipple = fillStipple
                    frmMain.Layers(handle).FillStippleLineColor = fillStippleLineColor
                    frmMain.Layers(handle).FillStippleTransparency = fillStippleTransparent
                    frmMain.Layers(handle).ShapeLayerFillTransparency = transPercent
                    frmMain.Layers(handle).LineOrPointSize = lineOrPointSize
                    frmMain.Layers(handle).DrawFill = drawFill
                    If type = MapWindow.Interfaces.eLayerType.PointShapefile Then
                        'Vertices are always visible - layer visibility is used to
                        'toggle overall visibility here.
                        frmMain.Layers(handle).VerticesVisible = True
                    Else
                        frmMain.Layers(handle).VerticesVisible = False
                        Try
                            frmMain.Layers(handle).VerticesVisible = Boolean.Parse(.Attributes("VerticesVisible").InnerText)
                        Catch
                        End Try
                    End If

                    Try
                        frmMain.Layers(handle).LabelsVisible = Boolean.Parse(.Attributes("LabelsVisible").InnerText)
                    Catch
                        frmMain.Layers(handle).LabelsVisible = True
                    End Try

                    Try
                        Integer.TryParse(.Attributes("MapTooltipField").InnerText, frmMain.Legend.Layers.ItemByHandle(handle).MapTooltipFieldIndex)
                        Boolean.TryParse(.Attributes("MapTooltipsEnabled").InnerText, frmMain.Legend.Layers.ItemByHandle(handle).MapTooltipsEnabled)
                        frmMain.UpdateMapToolTipsAtLeastOneLayer()
                    Catch
                    End Try

                    Try
                        'NOTE: These must go above coloring scheme or they will
                        'override the coloring scheme!
                        frmMain.Layers(handle).OutlineColor = System.Drawing.ColorTranslator.FromOle(outlineColor)
                        frmMain.Layers(handle).Color = System.Drawing.ColorTranslator.FromOle(color)
                        'frmMain.MapMain.set_ShapeLayerFillColor(handle, color)
                        'frmMain.MapMain.set_ShapeLayerLineColor(handle, outlineColor)
                    Catch
                    End Try

                    'load the coloring scheme
                    If Not .Item("Legend") Is Nothing Then
                        LoadShpFileColoringScheme(.Item("Legend"), handle)
                    End If

                    'add the userpointtype image
                    If (Not userPointType Is Nothing) Then
                        frmMain.Layers(handle).UserPointType = userPointType
                    End If

                    frmMain.Layers(handle).PointType = pointType

                    DeserializePointImageScheme(handle, layer.Item("ShapeFileProperties"))

                    DeserializeFillStippleScheme(handle, layer.Item("ShapeFileProperties"))

                    ' tws 04/29/2007
                    'load the shape-level formatting scheme
                    If Not .Item("ShapePropertiesList") Is Nothing Then
                        LoadShapePropertiesList(.Item("ShapePropertiesList"), handle)
                    End If
                End With

                'We just loaded a layer -- update/create the mwsr file
                'and if we weren't loading the mwsr file to begin with, i.e. ExistingLayerHandle = -1
                '(http://bugs.mapwindow.org/show_bug.cgi?id=340)
                If ExistingLayerHandle = -1 Then
                    frmMain.SaveShapeLayerProps(handle)
                End If

            ElseIf type = MapWindow.Interfaces.eLayerType.Image Or type = MapWindow.Interfaces.eLayerType.Grid Then
                'add image or grid layer

                ' Paul Meems: Shouldn't these values come from the app. settings?
                Dim transparentColor As Integer = 0
                Dim useTransparency As Boolean = False
                Dim imageLayerFillTransparency As Single = 100 ' Paul Meems Bug 1779 Default to 100 instead of 0
                Dim useHistogram As Boolean = False
                Dim allowHillshade As Boolean = True
                Dim bufferSize As Integer = 100
                Dim setToGrey As Boolean = False
                Dim imageColorScheme As MapWinGIS.PredefinedColorScheme = MapWinGIS.PredefinedColorScheme.FallLeaves
                ' Start Paul Meems May 20 2010, Issue 1691
                Dim imageUpSamplingMethod As MapWinGIS.tkInterpolationMode = MapWinGIS.tkInterpolationMode.imNone
                Dim imageDownSamplingMethod As MapWinGIS.tkInterpolationMode = MapWinGIS.tkInterpolationMode.imNone
                ' End Paul Meems May 20 2010, Issue 1691
                ' Start Paul Meems May 25 2010, Issue 1714
                Dim transparentColor2 As Integer = 0
                ' End Paul Meems May 25 2010, Issue 1714

                Dim GridProperty As XmlNode = layer.Item("GridProperty")
                If GridProperty IsNot Nothing Then
                    With GridProperty
                        Try
                            transparentColor = CInt(.Attributes("TransparentColor").InnerText)
                            ' Start Paul Meems May 25 2010, Issue 1714
                            ' Old situation:
                            transparentColor2 = transparentColor
                            ' End Paul Meems May 25 2010, Issue 1714
                        Catch
                        End Try
                        ' Start Paul Meems May 25 2010, Issue 1714
                        Try
                            ' New situation:
                            transparentColor = CInt(.Attributes("TransparentColor").InnerText)
                            transparentColor2 = CInt(.Attributes("TransparentColor2").InnerText)
                        Catch
                        End Try
                        ' End Paul Meems May 25 2010, Issue 1714
                        Try
                            useTransparency = CBool(.Attributes("UseTransparency").InnerText)
                        Catch
                        End Try
                        Try
                            imageLayerFillTransparency = CSng(.Attributes("ImageLayerFillTransparency").InnerText)
                        Catch
                        End Try
                        Try
                            useHistogram = CBool(.Attributes("UseHistogram").InnerText)
                        Catch
                        End Try
                        Try
                            allowHillshade = CBool(.Attributes("AllowHillshade").InnerText)
                        Catch
                        End Try
                        Try
                            setToGrey = CBool(.Attributes("SetToGrey").InnerText)
                        Catch
                        End Try
                        Try
                            bufferSize = CInt(.Attributes("BufferSize").InnerText)
                        Catch
                        End Try
                        Try
                            Dim cs As String
                            cs = .Attributes("ImageColorScheme").InnerText
                            Select Case cs
                                Case "DeadSea"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.DeadSea
                                Case "Desert"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.Desert
                                Case "FallLeaves"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.FallLeaves
                                Case "Glaciers"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.Glaciers
                                Case "Highway1"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.Highway1
                                Case "Meadow"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.Meadow
                                Case "SummerMountains"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.SummerMountains
                                Case "ValleyFires"
                                    imageColorScheme = MapWinGIS.PredefinedColorScheme.ValleyFires
                            End Select
                        Catch
                        End Try
                        ' Start Paul Meems May 20 2010, fixes for Issue 1691 (Upsampling and downsampling method hard coded to "imNone") 
                        ' New settings, might not be present in all project files!
                        Try
                            Dim up As String
                            up = .Attributes("UpSamplingMethod").InnerText
                            Dim dw As String
                            dw = .Attributes("DownSamplingMethod").InnerText
                            For Each samplingMode As MapWinGIS.tkInterpolationMode In [Enum].GetValues(GetType(MapWinGIS.tkInterpolationMode))
                                If samplingMode.ToString().Equals(up) Then imageUpSamplingMethod = samplingMode
                                If samplingMode.ToString().Equals(dw) Then imageDownSamplingMethod = samplingMode
                            Next
                        Catch
                        End Try
                        ' End Paul Meems May 20 2010, Issue 1691

                        'load the coloring scheme if it is a grid
                        If .HasChildNodes Then
                            gridScheme = LoadGridFileColoringScheme(.Item("Legend"))
                        End If
                    End With
                End If
                'make sure the file exists
                'Chris Michaelis July 26 05 - changed to warn the user and ask if they'd like to find it.
                ' Old: If (System.IO.File.Exists(filePath) = False) Then Exit Sub

                If (System.IO.File.Exists(filePath) = False) Then
                    If filePath.ToLower().Trim().StartsWith("ecwp://") Then
                        handle = frmMain.m_layers.AddLayer(filePath, name, , layerVisible, , , , , )(0).Handle
                    Else
                        ' This operation will have changed the current working directory.
                        ' Preserve it and set it back, or all subsequent layers will not be found either.
                        Dim cwd As String = CurDir()

                        ' PromptBrowse will set the file path by reference and return true,
                        ' or return false if the user cancels.
                        If Not PromptToBrowse(filePath, name) Then Exit Sub

                        'add layer -- add explicitly as an image or a grid.
                        'Chris Michaelis 2/1/2006 -- Changed this to explicitly
                        'load it as an image or a grid, depending on what was
                        'saved in the project file.
                        'This corrects the bug of explicitly adding an image-class GeoTIFF
                        'as a grid (e.g. to read the first band of 0-255 image values, or
                        'if the IsTIFFGrid call failed, etc), then saving it to a project file,
                        'then improperly loading it as an image when loading the
                        'project file -- improperly trusting the output of IsTIFFGrid. This 
                        'won't have a noticeable effect most of the time, but when it does 
                        'matter this will be a beneficial change.
                        If (type = MapWindow.Interfaces.eLayerType.Image) Then
                            Dim tImg As New MapWinGIS.Image
                            tImg.Open(System.IO.Path.GetFullPath(filePath))
                            handle = frmMain.m_layers.AddLayer(tImg, name, , layerVisible, , , , , , gridScheme)(0).Handle
                            tImg = Nothing
                            'Don't delete or close tImg or it will close the underlying
                            'object that was just added to the map.
                        ElseIf (type = MapWindow.Interfaces.eLayerType.Grid) Then
                            Dim tGrd As New MapWinGIS.Grid
                            tGrd.Open(System.IO.Path.GetFullPath(filePath))
                            handle = frmMain.m_layers.AddLayer(tGrd, name, , layerVisible, , , , , , gridScheme)(0).Handle
                            tGrd = Nothing
                            'Don't delete or close tImg or it will close the underlying
                            'object that was just added to the map.
                        End If

                        'Old code that didn't distinguish between image
                        'or grid explicitly (thus relying on IsTiffGrid()):
                        'handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filePath), name, , layerVisible, , , , , , gridScheme)(0).Handle

                        'Restore CWD
                        ChDir(cwd)
                    End If
                Else
                    'add layer 
                    'Note - This will automatically pick up the coloring scheme for the legend.
                    'before, we were setting the coloringscheme property on the layer.  This 
                    'forced a rebuild of the grid image each time and was much slower.  
                    'dpa 6/9/2005

                    'add layer -- add explicitly as an image or a grid.
                    'Chris Michaelis 2/1/2006 -- Changed this to explicitly
                    'load it as an image or a grid, depending on what was
                    'saved in the project file.
                    'This corrects the bug of explicitly adding an image-class GeoTIFF
                    'as a grid (e.g. to read the first band of 0-255 image values, or
                    'if the IsTIFFGrid call failed, etc), then saving it to a project file,
                    'then improperly loading it as an image when loading the
                    'project file -- improperly trusting the output of IsTIFFGrid. This 
                    'won't have a noticeable effect most of the time, but when it does 
                    'matter this will be a beneficial change.
                    If (type = MapWindow.Interfaces.eLayerType.Image) Then
                        Dim tImg As New MapWinGIS.Image
                        tImg.Open(System.IO.Path.GetFullPath(filePath))
                        ' Paul Meems: Why open the image object as layer here, below you change the image properties
                        ' Wouldn't it make more sense to call AddLayer after all properties are set?
                        handle = frmMain.m_layers.AddLayer(tImg, name, , layerVisible, , , , , , gridScheme)(0).Handle
                        frmMain.m_layers(handle).ImageLayerFillTransparency = imageLayerFillTransparency / 100
                        tImg.UseHistogram = useHistogram
                        tImg.AllowHillshade = allowHillshade
                        tImg.SetToGrey = setToGrey
                        tImg.BufferSize = bufferSize
                        tImg.ImageColorScheme = imageColorScheme
                        ' Start Paul Meems Aug 11 2010, Issue 1786
                        If tImg.CanUseGrouping Then
                            If Not tImg.UseTransparencyColor Then
                                ' Set the correct NoData value:
                                Dim colors As Object = Nothing, frequences As Object = Nothing
                                ' 0.5 is maximum buffer size in megabytes
                                Dim count As Integer = tImg.GetUniqueColors(0.5, colors, frequences)
                                ' Number of unique colors:
                                If count > 0 Then
                                    Dim myColors As UInteger() = TryCast(colors, UInteger())

                                    Dim result As Boolean = False
                                    ' It is the most common color
                                    tImg.SetNoDataValue(Convert.ToDouble(myColors(0)), result)
                                    ' Set the transparency:
                                    transparentColor = myColors(0)
                                    transparentColor2 = myColors(0)
                                End If
                            End If
                            ' Change some default settings:
                            imageDownSamplingMethod = MapWinGIS.tkInterpolationMode.imHighQualityBilinear
                            imageUpSamplingMethod = MapWinGIS.tkInterpolationMode.imHighQualityBilinear
                        End If
                        ' End Paul Meems Aug 11 2010, Issue 1786
                        ' Start Paul Meems May 20 2010, Issue 1691
                        tImg.DownsamplingMode = imageDownSamplingMethod
                        tImg.UpsamplingMode = imageUpSamplingMethod
                        ' End Paul Meems May 20 2010, Issue 1691
                        ' Start Paul Meems May 25 2010, Issue 1714
                        tImg.TransparencyColor = transparentColor
                        tImg.TransparencyColor2 = transparentColor2
                        tImg.UseTransparencyColor = useTransparency
                        ' End Paul Meems May 25 2010, Issue 1714

                        ' Paul Meems Not yet used properties, shouldn't they be used?
                        'tImg.TransparencyPercent
                        'tImg.DrawingMethod 

                        ' Paul Meems: Should AddLayer be called here instead of above?

                        ' Cleaning up:
                        tImg = Nothing
                        'Don't delete or close tImg or it will close the underlying
                        'object that was just added to the map.
                    ElseIf (type = MapWindow.Interfaces.eLayerType.Grid) Then

                        Dim tGrd As New MapWinGIS.Grid

                        tGrd.Open(System.IO.Path.GetFullPath(filePath))
                        'MsgBox(String.Format("starting m_layers.AddLayer(tGrd,{0},,{1},,,,,,{2})", name, layerVisible, "gridScheme")) 'DEBUG JK
                        handle = frmMain.m_layers.AddLayer(tGrd, name, , layerVisible, , , , , , gridScheme)(0).Handle
                        tGrd = Nothing
                        'Don't delete or close tImg or it will close the underlying
                        'object that was just added to the map.
                    End If

                    'Old code that didn't distinguish between image
                    'or grid explicitly (thus relying on IsTiffGrid()):
                    'handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filePath), name, , layerVisible, , , , , , gridScheme)(0).Handle
                End If

                'add image properties

                ' May 25 2010 Paul Meems: Whay do this here and not directly on the img object?
                ' I move this up:
                frmMain.Layers(handle).ImageTransparentColor = MapWinUtility.Colors.IntegerToColor(transparentColor)
                'frmMain.Layers(handle).UseTransparentColor = useTransparency

                If type = MapWindow.Interfaces.eLayerType.Grid Then
                    ' 10/17/2007 - SaveShapeLayerProps == misnomer - can also be used for saving grid coloring scheme.
                    ' Can't change name now without breaking interface
                    frmMain.SaveShapeLayerProps(handle)
                End If
            Else
                'make sure the file exists
                'Chris Michaelis July 26 05 - changed to warn the user and ask if they'd like to find it.
                ' Old: If (System.IO.File.Exists(filePath) = False) Then Exit Sub
                If (System.IO.File.Exists(filePath) = False) Then
                    ' This operation will have changed the current working directory.
                    ' Preserve it and set it back, or all subsequent layers will not be found either.
                    Dim cwd As String = CurDir()

                    ' PromptBrowse will set the file path by reference and return true,
                    ' or return false if the user cancels.
                    If Not PromptToBrowse(filePath, name) Then Exit Sub

                    handle = frmMain.m_layers.AddLayer(filePath, name, , layerVisible)(0).Handle

                    'Restore CWD
                    ChDir(cwd)
                Else
                    'add some other layer
                    handle = frmMain.m_layers.AddLayer(filePath, name, , layerVisible)(0).Handle
                End If
            End If

            'properties of all layers
            With frmMain.Layers(handle)
                .Expanded = expanded
                .Tag = tag
                .LabelsVisible = labelsVisible
            End With

            'add the layer image
            Dim layerImage As XmlNode = layer.Item("Image")
            imageType = ""
            If layerImage IsNot Nothing Then
                Try
                    imageType = layerImage.Attributes("Type").InnerText
                    If Len(imageType) > 0 Then
                        frmMain.Layers(handle).Icon = ConvertStringToImage(layerImage.InnerText, imageType)
                    End If
                Catch
                End Try
            End If
            MapWinUtility.Logger.Status("")
            frmMain.Layers(handle).Expanded = expanded

            'load the Dynamic Visibility options
            LoadDynamicVisibility(frmMain.Layers(handle), layer.Item("DynamicVisibility"))
        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadLayerProperties(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        End Try
    End Sub

    Private Sub LoadDynamicVisibility(ByVal mapWinLayer As MapWindow.Interfaces.Layer, ByVal node As Xml.XmlNode)
        Try
            'add DynamicVisibility options
            Dim useDynamicVisibility As Boolean = False
            Dim xMin As Double = 0
            Dim yMin As Double = 0
            Dim xMax As Double = 0
            Dim yMax As Double = 0

            If node IsNot Nothing Then
                useDynamicVisibility = CBool(node.Attributes("UseDynamicVisibility").InnerText)

                If node.Attributes("Scale") IsNot Nothing Then
                    'Scale
                    Dim exts As New MapWinGIS.Extents
                    exts.SetBounds(0, 0, 0, 0, 0, 0)
                    Try
                        exts = frmMain.ScaleToExtents(Double.Parse(node.Attributes("Scale").InnerText), frmMain.MapMain.Extents)
                    Catch e As Exception
                        System.Diagnostics.Debug.WriteLine(e.ToString())
                    End Try
                    If (exts Is Nothing) Then
                        exts = New MapWinGIS.Extents
                        exts.SetBounds(0, 0, 0, 0, 0, 0)
                    End If
                    xMin = exts.xMin
                    xMax = exts.xMax
                    yMin = exts.yMin
                    yMax = exts.yMax
                Else
                    'Extents
                    xMin = MapWinUtility.MiscUtils.ParseDouble(node.Attributes("xMin").InnerText, 0.0) 'CDbl(node.Attributes("xMin").InnerText)
                    yMin = MapWinUtility.MiscUtils.ParseDouble(node.Attributes("yMin").InnerText, 0.0) 'CDbl(node.Attributes("yMin").InnerText)
                    xMax = MapWinUtility.MiscUtils.ParseDouble(node.Attributes("xMax").InnerText, 0.0) 'CDbl(node.Attributes("xMax").InnerText)
                    yMax = MapWinUtility.MiscUtils.ParseDouble(node.Attributes("yMax").InnerText, 0.0) 'CDbl(node.Attributes("yMax").InnerText)
                End If
            End If
            Dim ex As MapWinGIS.Extents = New MapWinGIS.Extents
            ex.SetBounds(xMin, yMin, 0, xMax, yMax, 0)

            'DynamicVisibility prop
            With mapWinLayer

                'set the extents
                If Not (xMin = 0 And yMin = 0 And xMax = 0 And yMax = 0) Then
                    'CDM 1/2/2007 Remove the auto-vis item if the handle is already
                    'loaded -- should only be on a fluke, since it's emptied
                    'on close now too.
                    If frmMain.m_AutoVis.Contains(.Handle) Then frmMain.m_AutoVis.Remove(.Handle)

                    frmMain.m_AutoVis.Add(.Handle, ex, useDynamicVisibility)
                End If

            End With
        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadDynamicVisibility(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        End Try
    End Sub

#End Region

#Region "Load Plugins"

    Private Sub LoadApplicationPlugins(ByVal plugins As XmlElement, ByVal loadingConfig As Boolean)
        'exit if this element does not exists
        If (plugins Is Nothing) Then Exit Sub

        If (loadingConfig) Then
            'get the application plugin dir
            If (plugins.Attributes("PluginDir").InnerText <> "") Then
                AppInfo.ApplicationPluginDir = System.IO.Path.GetFullPath(plugins.Attributes("PluginDir").InnerText)
            End If

            frmMain.m_PluginManager.LoadApplicationPlugins(AppInfo.ApplicationPluginDir)
        End If

        For Each pluginNode As XmlNode In plugins.ChildNodes
            Try
                Dim pluginName As String = pluginNode.Attributes("Key").InnerText
                LoadPlugin(pluginName)
                frmMain.m_PluginManager.ProjectLoading(pluginName, ProjectFileName, pluginNode.Attributes("SettingsString").InnerText)
            Catch e As System.Exception
                m_ErrorMsg += "Error in LoadApplicationPlugins(), Message: " + e.Message + Chr(13)
                m_ErrorOccured = True
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Load all plugins specified, then call ProjectLoading on all plugins whether specified or already present
    ''' </summary>
    ''' <param name="plugins">XML describing plugins to load</param>
    ''' <param name="loadingConfig">unused argument</param>
    Private Sub LoadPlugins(ByVal plugins As XmlElement, ByVal loadingConfig As Boolean)
        Dim lPluginSettings As New Generic.Dictionary(Of String, String) 'settingsString for each plugin in project

        For Each pluginNode As XmlNode In plugins.ChildNodes
            Dim pluginName As String = pluginNode.Attributes("Key").InnerText
            Try
                LoadPlugin(pluginName)
                lPluginSettings.Add(pluginName, pluginNode.Attributes("SettingsString").InnerText)
            Catch e As Exception
                MapWinUtility.Logger.Dbg("Exception loading plugin '" & pluginName & "': " & e.Message)
            End Try
        Next

        'Send ProjectLoading to all plugins that are present, whether just loaded above or already loaded before
        Dim loadedPlugins As Collection = frmMain.m_PluginManager.LoadedPlugins()
        For Each plugin As Interfaces.IPlugin In loadedPlugins
            Dim key As String = MapWinUtility.PluginManagementTools.GenerateKey(plugin.GetType())
            If lPluginSettings.ContainsKey(key) Then 'include settingsString if found in argument plugins above
                frmMain.m_PluginManager.ProjectLoading(key, ProjectFileName, lPluginSettings(key))
            Else
                frmMain.m_PluginManager.ProjectLoading(key, ProjectFileName, "")
            End If
        Next
    End Sub

    ''' <summary>
    ''' Load a plugin if it is not already loaded
    ''' </summary>
    ''' <param name="pluginName">Name of plugin to load</param>
    ''' <remarks></remarks>
    Private Sub LoadPlugin(ByVal pluginName As String)
        If frmMain.m_PluginManager.PluginIsLoaded(pluginName) Then
            MapWinUtility.Logger.Dbg("PlugIn Already Loaded: " & pluginName)
        Else
            MapWinUtility.Logger.Dbg("LoadPlugIn: " & pluginName)
            frmMain.m_PluginManager.StartPlugin(pluginName)
            MapWinUtility.Logger.Dbg("Loaded: " & pluginName)
        End If
    End Sub
#End Region

#End Region

#Region "Utilities"

    ''' <summary>
    ''' Serializes image object
    ''' </summary>
    Private Sub SaveImage(ByRef m_Doc As Xml.XmlDocument, ByVal img As Object, ByVal parent As XmlElement, Optional ByVal elementName As String = "Image")
        Dim image As XmlElement = m_Doc.CreateElement("Image")
        Dim type As XmlAttribute = m_Doc.CreateAttribute("Type")
        Dim typ As String = ""

        'set the properies of the image
        image.InnerText = ConvertImageToString(img, typ)
        type.InnerText = typ

        'add the properties to the images
        image.Attributes.Append(type)
        parent.AppendChild(image)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Shared Sub SaveMainToolbarButtons()
        'Try
        'store the mapwindow default button items
        Dim item As Collections.DictionaryEntry
        Dim enumerator As Collections.IEnumerator = frmMain.m_Toolbar.m_Buttons.GetEnumerator
        While (enumerator.MoveNext)
            item = CType(enumerator.Current, Collections.DictionaryEntry)
            If (Not m_MainToolbarButtons.ContainsKey(item.Key)) Then
                m_MainToolbarButtons.Add(item.Key, item.Value)
            End If
        End While

        'store the mapwindow default bars items
        enumerator = frmMain.m_Toolbar.tbars.GetEnumerator
        While (enumerator.MoveNext)
            item = CType(enumerator.Current, Collections.DictionaryEntry)
            If (Not m_MainToolbarButtons.ContainsKey(item.Key)) Then
                m_MainToolbarButtons.Add(item.Key, item.Value)
            End If
        End While

        'store the mapwindow default menus items
        enumerator = frmMain.m_Menu.m_MenuTable.GetEnumerator
        While (enumerator.MoveNext)
            item = CType(enumerator.Current, Collections.DictionaryEntry)
            If (Not m_MainToolbarButtons.ContainsKey(item.Key)) Then
                m_MainToolbarButtons.Add(item.Key, item.Value)
            End If
        End While
        'Catch ex As Exception
        '  ShowError(ex)
        'End Try
    End Sub

    ''' <summary>
    '''  Converts in-memory image to string representation
    ''' </summary>
    ''' <param name="img">Image to be converted</param>
    ''' <param name="type">Returns type of image</param>
    ''' <returns>String representation of image</returns>
    ''' <remarks></remarks>
    Shared Function ConvertImageToString(ByVal img As Object, ByRef type As String) As String
        Dim s As String = ""
        Dim path As String = GetMWTempFile()

        If Not img Is Nothing Then
            Try
                'find the type of image it is
                If TypeOf img Is Icon Then
                    type = "Icon"
                    Dim image As Icon = CType(img, Icon)

                    'write the image to a temp file
                    Dim outStream As IO.Stream = IO.File.OpenWrite(path)
                    image.Save(outStream)
                    outStream.Close()
                ElseIf TypeOf img Is stdole.IPictureDisp Then
                    type = "IPictureDisp"
                    Dim cvter As New MapWinUtility.ImageUtils
                    Dim image As Image = New Bitmap(cvter.IPictureDispToImage(img))

                    'save bitmap
                    image.Save(path)
                ElseIf TypeOf img Is Bitmap Then
                    type = "Bitmap"
                    Dim image As Image = CType(img, Bitmap)

                    'save bitmap
                    image.Save(path)
                ElseIf TypeOf img Is MapWinGIS.Image Then

                    type = "MapWinGIS.Image"
                    Dim utils As New MapWinUtility.ImageUtils
                    Dim mwimg As MapWinGIS.Image = CType(img, MapWinGIS.Image)
                    Dim image As Image = New Bitmap(utils.IPictureDispToImage(mwimg.Picture))

                    image.Save(path)
                Else
                    type = "Unknown"
                    Return ""
                End If

                'initialize the reader to read binary
                Dim inStream As IO.Stream = IO.File.OpenRead(path)
                Dim reader As New System.IO.BinaryReader(inStream)

                'read in each byte and convert it to a char
                Dim numbytes As Long = reader.BaseStream.Length
                s = System.Convert.ToBase64String(reader.ReadBytes(CInt(numbytes)))

                reader.Close()

                'delete the temp file
                System.IO.File.Delete(path)

                Return s
            Catch e As System.Exception
                'm_ErrorMsg += "Error in ConvertImageToString(), Message: " + e.Message + Chr(13)
                'm_ErrorOccured = True
                frmMain.ShowErrorDialog(e)
                Return s
            End Try
        End If

        If (System.IO.File.Exists(path)) Then
            System.IO.File.Delete(path)
        End If

        Return s
    End Function

    ''' <summary>
    ''' Creates in-memory image from it's string representation.
    ''' </summary>
    Shared Function ConvertStringToImage(ByVal image As String, ByVal type As String) As Object
        Dim icon As Icon
        Dim bmp As Bitmap
        Dim mybyte() As Byte
        Dim path As String
        Dim outStream As IO.Stream

        If Len(image) > 0 Then
            Try
                path = GetMWTempFile()
                g_KillList.Add(path)

                outStream = IO.File.OpenWrite(path)

                mybyte = System.Convert.FromBase64String(image)
                'write the image to a temp file
                ' cdm - modernize: size = UBound(mybyte)
                ' cdm - modernize: For i = 0 To size
                outStream.Write(mybyte, 0, mybyte.Length)
                ' cdm - modernize: Next
                outStream.Close()

                'open the image
                Select Case type
                    Case "Icon"
                        icon = New Icon(path)
                        Return icon
                    Case "Bitmap"
                        bmp = New Bitmap(path)
                        Return bmp
                    Case "IPictureDisp"
                        bmp = New Bitmap(path)
                        Dim cvter As New MapWinUtility.ImageUtils
                        Return cvter.ImageToIPictureDisp(bmp)
                    Case "MapWinGIS.Image"
                        bmp = New Bitmap(path)
                        Dim img As New MapWinGIS.Image
                        Dim utils As New MapWinUtility.ImageUtils
                        img.Picture = CType(utils.ImageToIPictureDisp(bmp), stdole.IPictureDisp)
                        Return img
                End Select

            Catch ex As System.Exception
                frmMain.ShowErrorDialog(ex)
            End Try
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns path to file relative to project path
    ''' </summary>
    Public Shared Function GetRelativePath(ByVal Filename As String, ByVal ProjectFile As String) As String
        ' For bug #946:
        MapWinUtility.Logger.Dbg("Start GetRelativePath. Filename: " + Filename)

        GetRelativePath = ""
        Dim a() As String, b() As String
        Dim i As Integer, j As Integer, k As Integer, Offset As Integer

        If Len(Filename) = 0 Or Len(ProjectFile) = 0 Then
            Return ""
        End If

        Try
            'If the drive is different then use the full path
            If IO.Path.GetPathRoot(Filename).ToLower() <> System.IO.Path.GetPathRoot(ProjectFile).ToLower() Then
                ' For bug #946:
                MapWinUtility.Logger.Dbg("The drive is different, use full path")
                GetRelativePath = Filename
                Exit Function
            End If
            Dim dirinfo As System.IO.DirectoryInfo ' use to tell when GetParent() fails
            '
            'load a()
            ReDim a(0)
            a(0) = Filename
            i = 0
            Do
                i = i + 1
                ReDim Preserve a(i)
                Try
                    dirinfo = System.IO.Directory.GetParent(a(i - 1))
                    If (dirinfo Is Nothing) Then
                        a(i) = ""
                    Else
                        a(i) = dirinfo.FullName.ToLower()
                    End If
                Catch ex As Exception
                    a(i) = ""
                End Try
            Loop Until a(i) = ""
            '
            'load b()
            ReDim b(0)
            b(0) = ProjectFile
            i = 0
            Do
                i = i + 1
                ReDim Preserve b(i)
                Try
                    dirinfo = System.IO.Directory.GetParent(b(i - 1))
                    If (dirinfo Is Nothing) Then
                        b(i) = ""
                    Else
                        b(i) = dirinfo.FullName.ToLower()
                    End If
                    ' b(i) = System.IO.Directory.GetParent(b(i - 1)).FullName.ToLower()
                Catch ex As Exception
                    b(i) = ""
                End Try
            Loop Until b(i) = ""
            '
            'look for match
            For i = 0 To UBound(a)
                For j = 0 To UBound(b)
                    If a(i) = b(j) Then
                        'found match
                        GoTo [CONTINUE]
                    End If
                Next j
            Next i
[CONTINUE]:
            ' j is num steps to get from BasePath to common path
            ' so I need this many of "..\"
            For k = 1 To j - 1
                GetRelativePath = GetRelativePath & "..\"
            Next k

            'everything past a(i) needs to be appended now.
            If a(i).EndsWith("\") Then
                Offset = 0
            Else
                Offset = 1
            End If
            GetRelativePath = GetRelativePath & Filename.Substring(Len(a(i)) + Offset)
            ' For bug #946:
            MapWinUtility.Logger.Dbg("Finished GetRelativePath: " + GetRelativePath)
        Catch e As System.Exception
            MapWinUtility.Logger.Dbg("Error GetRelativePath: " + e.ToString())
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' This function compares the last time of creation or modification of the two files. 
    ''' If file1 was changed more recently than file2, it returns 1. If file2 was changed more 
    ''' recently than file1, it returns 2. in other cases (one of files does not exist of time 
    ''' of modification is the same), return zero.
    ''' </summary>
    Public Function CompareFilesByTime(ByVal file1 As String, ByVal file2 As String) As Integer
        If System.IO.File.Exists(file1) And System.IO.File.Exists(file2) Then
            Dim fi1 As New System.IO.FileInfo(file1)
            Dim fi2 As New System.IO.FileInfo(file2)

            Dim fi1Changed As DateTime = fi1.LastWriteTime
            If fi1.CreationTime > fi1Changed Then
                fi1Changed = fi1.CreationTime
            End If

            Dim fi2Changed As DateTime = fi2.LastWriteTime
            If fi2.CreationTime > fi2Changed Then
                fi2Changed = fi2.CreationTime
            End If

            If fi1Changed > fi2Changed Then
                Return 1
            Else
                Return -1
            End If
        End If
        'return zero if we cannot find out 
        Return 0
    End Function

    ''' <summary>
    ''' Requests filename for the layer being saved from the user
    ''' </summary>
    Private Function SaveLayer(ByVal layer As Object, ByVal handle As Integer, ByVal layerType As MapWindow.Interfaces.eLayerType) As String
        Dim cdlSave As New SaveFileDialog
        Dim fileName As String = ""

        Try

            'check to see if it is a shapefile
            If (layerType = Interfaces.eLayerType.LineShapefile _
             Or layerType = Interfaces.eLayerType.PointShapefile _
             Or layerType = Interfaces.eLayerType.PolygonShapefile) Then

                cdlSave.Filter = "Shapefile (*.shp)|*.shp"
                cdlSave.ShowDialog()
                fileName = Trim(cdlSave.FileName)

                If (fileName <> "") Then
                    DeleteShapeFile(fileName)
                    If (CType(layer, MapWinGIS.Shapefile).SaveAs(fileName) = False) Then
                        MapWinUtility.Logger.Msg("Failed to save Layer", MsgBoxStyle.Exclamation)
                    End If
                End If
                'check to see if it is a image
            ElseIf (layerType = Interfaces.eLayerType.Image) Then
                cdlSave.Filter = "Bitmap (*.bmp)| *.bmp| GIF (*.gif)| *.gif"
                cdlSave.ShowDialog()
                fileName = Trim(cdlSave.FileName)

                If (fileName <> "") Then
                    If (CType(layer, MapWinGIS.Image).Save(fileName) = False) Then
                        MapWinUtility.Logger.Msg("Failed to save Layer", MsgBoxStyle.Exclamation)
                    End If
                End If
                'check to see if it is a grid
            ElseIf (layerType = Interfaces.eLayerType.Grid) Then
                Dim grid As MapWinGIS.Grid
                cdlSave.Filter = "Binary (*.bgd)| *.bgd|Ascii (*.asc)|*.asc"
                cdlSave.ShowDialog()
                fileName = Trim(cdlSave.FileName)

                If (fileName <> "") Then
                    grid = frmMain.Layers(handle).GetGridObject()
                    If (grid.Save(fileName) = False) Then
                        MapWinUtility.Logger.Msg("Failed to save Layer, Message: " + grid.ErrorMsg(grid.LastErrorCode), MsgBoxStyle.Exclamation)
                    End If
                End If

            End If
        Catch ex As System.Exception
            ShowError(ex)
        End Try

        Return fileName
    End Function

    ''' <summary>
    ''' Function for deleting a shapefile with its three pieces.
    ''' TODO: there are actually more files (projection, spatial index, symbology)
    ''' </summary>
    Public Sub DeleteShapeFile(ByVal fileName As String)
        Dim f1, f2, f3 As String

        f1 = System.IO.Path.ChangeExtension(fileName, ".shp")
        f2 = System.IO.Path.ChangeExtension(fileName, ".shx")
        f3 = System.IO.Path.ChangeExtension(fileName, ".dbf")

        If System.IO.File.Exists(f1) Then System.IO.File.Delete(f1)
        If System.IO.File.Exists(f2) Then System.IO.File.Delete(f2)
        If System.IO.File.Exists(f3) Then System.IO.File.Delete(f3)
    End Sub

    ''' <summary>
    ''' TODO: comment
    ''' </summary>
    ''' <param name="ProjectName"></param>
    ''' <remarks></remarks>
    Private Sub AddToRecentProjects(ByVal ProjectName As String)
        Try

            ' 3 Nov. 2010 Paul Meems - Plug-ins may remove this menu so first check if it still exists:
            If Not frmMain.m_Menu.m_MenuTable.Contains("mnuRecentProjects") Then
                Return
            End If

            'Remove any recent project names that match this one
            Dim NewNameLower As String = ProjectName.ToLower
            Dim iRecent As Integer = ProjInfo.RecentProjects.Count - 1
            While iRecent >= 0
                If CStr(ProjInfo.RecentProjects.Item(iRecent)).ToLower = NewNameLower Then
                    ProjInfo.RecentProjects.RemoveAt(iRecent)
                End If
                iRecent -= 1
            End While

            'Add this name to the start of the list
            ProjInfo.RecentProjects.Insert(0, ProjectName)

            'Make sure the list doesn't get longer than 10 items
            If (ProjInfo.RecentProjects.Count > 10) Then
                ProjInfo.RecentProjects.RemoveAt(ProjInfo.RecentProjects.Count - 1)
            End If
            frmMain.BuildRecentProjectsMenu()
        Catch ex As System.Exception
            ShowError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Used to let the user browse for a missing layer when loading a project.
    ''' 'If user cancelled one of these prompts less than a minute ago, skip prompting for this layer 
    ''' Avoids asking user about every layer when several layer files are missing and all should be dropped
    ''' </summary>
    Friend Shared Function PromptToBrowse(ByRef filePath As String, ByVal displayName As String) As Boolean

        If displayName = "GEM Observations" Then
            'If the tmp shapefile is missing create a new one
            Dim shp As MapWinGIS.Shapefile = frmMain.CreateShapefileAndImportData()
            filePath = shp.Filename
            shp = Nothing
            Return True
        End If


        If (Now.ToOADate - frmMain.m_CancelledPromptToBrowse) * 1440 < 1 Then ' 1440 minutes per day
            Return False
        End If

        'Paul Meems 6/11/2009
        Dim msg As String = String.Format(frmMain.resources.GetString("msgDropMissingLayer.Text"), filePath + CType(IIf(displayName = "", "", " (" + displayName + ")"), String))
        Dim msgTitle As String = String.Format(frmMain.resources.GetString("msgDropMissingLayer.Title"), CType(IIf(displayName = "", "", " - " + displayName), String))

        Dim rslt As MsgBoxResult = MapWinUtility.Logger.Msg(msg, MsgBoxStyle.YesNoCancel, msgTitle)

        Select Case rslt
            Case MsgBoxResult.No
                Return False
            Case MsgBoxResult.Cancel
                frmMain.m_CancelledPromptToBrowse = Now.ToOADate
                Return False
            Case MsgBoxResult.Yes
                Dim cdlOpen As New OpenFileDialog

                'set the default dir
                If (System.IO.Directory.Exists(AppInfo.DefaultDir)) Then
                    cdlOpen.InitialDirectory = AppInfo.DefaultDir
                End If

                cdlOpen.FileName = ""
                cdlOpen.Title = "Locate Map Layer"
                cdlOpen.Filter = (New Layers).GetSupportedFormats()

                cdlOpen.CheckFileExists = True
                cdlOpen.CheckPathExists = True
                cdlOpen.Multiselect = False
                cdlOpen.ShowReadOnly = False

                ' Default to the missing filename
                cdlOpen.FileName = System.IO.Path.GetFileName(filePath)

                If Not cdlOpen.ShowDialog() = DialogResult.Cancel Then
                    filePath = cdlOpen.FileName
                Else
                    Return False 'Cancelled
                End If
        End Select

        Return True
    End Function

#End Region

End Class
