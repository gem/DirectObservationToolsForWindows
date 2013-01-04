'************************************************************
'Class:         Project
'Context:       Public - part of the plug-in interface
'Filename:      clsProject.vb
'Last Update:   1/16/2005, dpa
'Description:
'This class is the public class object that plugins see.  
'It used to keep it's own copy of the XMLProjectFile, but 
'as of the OSS version 4.0, this object now uses the handle to
'the global friend ProjInfo instance of the xmlProjectFile. 
'New error checking added for version 4.0.
'
'Modifications 
'05/15/2007 Tom Shanley (tws) added  setting to control save of shape-level formatting
'04/19/2009 Brian Marchion (bm) added method to save a copy of the current project
'************************************************************
Imports MapWindow.Controls.Projections

Public Class Project
    Implements MapWindow.Interfaces.Project

    Public Sub OpenIntoCurrent(ByVal filename As String) Implements Interfaces.Project.LoadIntoCurrentProject
        modMain.frmMain.DoOpenIntoCurrent(filename)
    End Sub

    Public Function Load(ByVal FileName As String) As Boolean Implements MapWindow.Interfaces.Project.Load
        'Implements the IMapWin.Interfaces.Project.Load function that 
        'allows a plugin to load a project file. 
        If System.IO.File.Exists(FileName) Then
            ProjInfo.ProjectFileName = FileName
            Return ProjInfo.LoadProject(FileName)
        Else
            Return False
        End If
    End Function

    Public Function Save(ByVal FileName As String) As Boolean Implements MapWindow.Interfaces.Project.Save
        'Implements the IMapWin.Interfaces.Project.Save function that 
        'allows a plugin to save a project file. 
        'ARA 1/10/2009 commented out to allow any extension for the config file
        'If System.IO.Path.GetExtension(FileName) <> ".mwprj" Then
        '    'Add the .mwprj extension if needed
        '    FileName += ".mwprj"
        'End If
        ProjInfo.ProjectFileName = FileName
        Dim retval = ProjInfo.SaveProject()
        frmMain.SetModified(False)
        Return retval
    End Function

    Public Function SaveCopy(ByVal FileName As String) As Boolean Implements MapWindow.Interfaces.Project.SaveCopy
        'BM 04/19/2009 added method to save a copy of the current project 
        'Implements the IMapWin.Interfaces.Project.Save function that 
        'allows a plugin to save a copy of a project file.
        Dim backupFilename As String = ProjInfo.ProjectFileName
        ProjInfo.ProjectFileName = FileName
        Dim retval = ProjInfo.SaveProject()
        ProjInfo.ProjectFileName = backupFilename
        frmMain.SetModified(False)
        Return retval

    End Function

    Public Function LoadLayerSymbologyFromProjectFile(ByVal Filename As String, ByVal Handle As Integer) As Boolean Implements MapWindow.Interfaces.Project.LoadLayerSymbologyFromProjectFile
        'Implements the IMapWin.Interfaces.Project.LoadLayerSymbologyFromProjectFile function that 
        'allows a plugin to load the layer symbology for a specific layer from a project file 
        Dim projectFile As New Xml.XmlDocument
        projectFile.Load(Filename)

        'Make sure the handle exists 
        If frmMain.Layers(Handle) Is Nothing Then
            Return False
        End If

        Dim projectXML As New XmlProjectFile
        For Each layerNode As Xml.XmlNode In projectFile.GetElementsByTagName("Layer")
            If layerNode.Attributes("Name").Value = frmMain.Layers(Handle).Name And System.IO.Path.GetFileName(layerNode.Attributes("Path").Value).ToLower() = System.IO.Path.GetFileName(frmMain.Layers(Handle).FileName).ToLower() Then
                projectXML.LoadLayerProperties(layerNode, Handle, True)
                Return True
            End If
        Next

        Return False

    End Function

    Public Function SaveConfig(ByVal FileName As String) As Boolean Implements MapWindow.Interfaces.Project.SaveConfig
        'Implements the IMapWin.Interfaces.Project.SaveConfig function that 
        'allows a plugin to save a config file. 
        If System.IO.Path.GetExtension(FileName) <> ".mwcfg" Then
            FileName += ".mwcfg"
        End If
        ProjInfo.ConfigFileName = FileName
        Return ProjInfo.SaveConfig()
    End Function

    Public ReadOnly Property ConfigFileName() As String Implements MapWindow.Interfaces.Project.ConfigFileName
        'Implements the IMapWin.Interfaces.Project.ConfigFileName property. 
        'Update in version 4, pulls the config filename from the projinfo object
        'instead of from the frmMain object.
        Get
            Return ProjInfo.ConfigFileName
        End Get
    End Property

    Public ReadOnly Property FileName() As String Implements MapWindow.Interfaces.Project.FileName
        'Implements the IMapWin.Interfaces.Project.FileName property.
        'Update in version 4, pulls the filename from the projinfo object
        'instead of from the frmMain object.
        Get
            Return ProjInfo.ProjectFileName
        End Get
    End Property

    Public Property Modified() As Boolean Implements MapWindow.Interfaces.Project.Modified
        'Implements the IMapWin.Interfaces.Project.Modified property.
        'Update in version 4, pulls the modified status from the projinfo object
        'instead of from the frmMain object.
        Get
            Return ProjInfo.Modified
        End Get
        Set(ByVal Value As Boolean)
            frmMain.SetModified(Value)
        End Set
    End Property

    Public Property MapUnits() As String Implements Interfaces.Project.MapUnits
        Get
            If modMain.ProjInfo.m_MapUnits Is Nothing Or modMain.ProjInfo.m_MapUnits = "" Then
                modMain.ProjInfo.m_MapUnits = "" 'In case it was 'nothing'

                'Try to detect the map unit from the proj4 string;
                'this will only work for meters however.
                If Not ProjectProjection() = "" Then
                    If InStr(ProjectProjection().ToLower, "+proj=longlat") > 0 Or InStr(ProjectProjection().ToLower, "+proj=latlong") > 0 Then
                        modMain.ProjInfo.m_MapUnits = "Lat/Long"
                    ElseIf InStr(ProjectProjection().ToLower, "+units=m") > 0 Then
                        modMain.ProjInfo.m_MapUnits = "Meters"
                    ElseIf InStr(ProjectProjection().ToLower, "+units=ft") > 0 Then
                        modMain.ProjInfo.m_MapUnits = "Feet"
                    ElseIf InStr(ProjectProjection().ToLower, "+to_meter=") > 0 Then
                        '---Cho 1/20/2009: Support for feet.
                        Dim toMeter As Double
                        toMeter = Convert.ToDouble(System.Text.RegularExpressions.Regex.Replace(ProjectProjection().ToLower, "^.*to_meter=([.0-9]+).*$", "$1"))
                        If toMeter > 0.3047 And toMeter < 0.3049 Then modMain.ProjInfo.m_MapUnits = "Feet"
                    End If
                End If

            End If

            Return modMain.ProjInfo.m_MapUnits.Trim()
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Value = ""

            modMain.ProjInfo.m_MapUnits = Value.Trim()
        End Set
    End Property

    Public Property MapUnitsAlternate() As String Implements Interfaces.Project.MapUnitsAlternate
        Get
            If modMain.ProjInfo.ShowStatusBarCoords_Alternate Is Nothing Or modMain.ProjInfo.ShowStatusBarCoords_Alternate = "" Then
                Return ""
            Else
                Return modMain.ProjInfo.ShowStatusBarCoords_Alternate.Trim()
            End If
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Value = ""

            modMain.ProjInfo.ShowStatusBarCoords_Alternate = Value.Trim()
        End Set
    End Property

    Public ReadOnly Property ConfigLoaded() As Boolean Implements Interfaces.Project.ConfigLoaded
        Get
            Return modMain.ProjInfo.ConfigLoaded
        End Get
    End Property

    Public ReadOnly Property RecentProjects() As System.Collections.ArrayList Implements Interfaces.Project.RecentProjects
        Get
            Return modMain.ProjInfo.RecentProjects
        End Get
    End Property

    ' tws 05/15/07
    Public Property SaveShapeSettings() As Boolean Implements Interfaces.Project.SaveShapeSettings
        Get
            Return modMain.ProjInfo.SaveShapeSettings
        End Get
        Set(ByVal value As Boolean)
            modMain.ProjInfo.SaveShapeSettings = value
        End Set
    End Property

#Region "Projection"
    ''' <summary>
    ''' Occurs when project projection is changed
    ''' </summary>
    Public Event ProjectionChanged(ByVal oldProjection As MapWinGIS.GeoProjection, ByVal newProjection As MapWinGIS.GeoProjection) Implements Interfaces.Project.ProjectionChanged

    ''' <summary>
    ''' Gets or sets projection string for the project. This properties was left for backward compatibility,
    ''' new GeoProjection property should be used instead
    ''' </summary>
    Public Property ProjectProjection() As String Implements Interfaces.Project.ProjectProjection
        Get
            Dim prj As MapWinGIS.GeoProjection = modMain.ProjInfo.GeoProjection
            If prj.IsEmpty() Then
                Return ""
            Else
                Return prj.ExportToProj4()
            End If
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Value = ""
            Dim prj As MapWinGIS.GeoProjection = New MapWinGIS.GeoProjection
            prj.ImportFromProj4(Value)
            Me.GeoProjection = prj
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets project projection
    ''' </summary>
    Public Property GeoProjection() As MapWinGIS.GeoProjection Implements Interfaces.Project.GeoProjection
        Get
            Return modMain.ProjInfo.GeoProjection
        End Get
        Set(ByVal value As MapWinGIS.GeoProjection)
            If Not value Is Nothing Then
                Dim projOld As MapWinGIS.GeoProjection = modMain.ProjInfo.GeoProjection

                If value.IsEmpty Then
                    modMain.ProjInfo.GeoProjection = New MapWinGIS.GeoProjection
                Else

                    ' try to ensure that projection is in database
                    If Not modMain.ProjectionDB Is Nothing Then
                        Dim cs As MapWindow.Controls.Projections.CoordinateSystem = modMain.ProjectionDB.GetCoordinateSystem(value, ProjectionSearchType.Standard)
                        If cs Is Nothing Then
                            cs = modMain.ProjectionDB.GetCoordinateSystem(value, ProjectionSearchType.UseDialects)
                            If Not cs Is Nothing Then
                                Dim proj As MapWinGIS.GeoProjection = New MapWinGIS.GeoProjection()
                                If proj.ImportFromEPSG(cs.Code) Then value = proj
                            End If
                        End If
                    End If
                    modMain.ProjInfo.GeoProjection.CopyFrom(value)
                End If

                RaiseEvent ProjectionChanged(projOld, modMain.ProjInfo.GeoProjection)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Displays "choose projection" dialog and return projection selected by user
    ''' </summary>
    Public Function GetProjectionFromUser() As MapWinGIS.GeoProjection
        
        Dim form As New frmChooseProjection(modMain.ProjectionDB, modMain.frmMain)
        Dim proj As MapWinGIS.GeoProjection = Nothing

        If (form.ShowDialog(modMain.frmMain) = Windows.Forms.DialogResult.OK) Then
            ' do we have a selected projection ?
            If form.projectionTreeView1.SelectedProjection Is Nothing And Not _
                  form.projectionTreeView1.SelectedCoordinateSystem Is Nothing Then
                MessageBox.Show("Failed to initialize the selected projection", modMain.AppInfo.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                proj = form.projectionTreeView1.SelectedProjection
            End If
        End If

        form.Dispose()
        Return proj
    End Function

    ''' <summary>
    ''' Shows dialog to set new project projection
    ''' Can be called from status bar and project properties
    ''' </summary>
    ''' <returns>True if new projection was selected</returns>
    Public Function SetProjectProjectionByDialog() As MapWinGIS.GeoProjection
        Dim proj As MapWinGIS.GeoProjection = Me.GetProjectionFromUser()

        Dim needsReloading As Boolean = False

        If Not proj Is Nothing Then
            ' we need to test is it possible to do it without reprojection
            If modMain.frmMain.Layers.NumLayers > 0 And Not modMain.frmMain.Project.GeoProjection.IsEmpty Then

                Dim ext As MapWinGIS.Extents = frmMain.MapMain.MaxExtents
                If Not proj.IsSameExt(modMain.frmMain.Project.GeoProjection, ext) Then
                    If MessageBox.Show("The operation requires reloading of the project to do the reprojection of layers. Do you want to continue?", _
                    modMain.frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        needsReloading = True
                    Else
                        Return Nothing
                    End If
                End If
            End If

            ' settings the new projection - reloading must be done by caller
            Dim projOld As MapWinGIS.GeoProjection = modMain.frmMain.Project.GeoProjection
            modMain.frmMain.Project.GeoProjection = proj

            If needsReloading Then
                ' saving it 
                If Me.Modified Then frmMain.DoSave()

                ' user canceled the saving
                If Me.Modified Then
                    modMain.frmMain.Project.GeoProjection = projOld
                    Return Nothing
                Else
                    ' ensure that a user will receive the necessary dialogs on reloading
                    frmMain.ApplicationInfo.ShowLoadingReport = True
                    frmMain.ApplicationInfo.NeverShowProjectionDialog = False

                    ' to set max extents after reloading
                    modMain.AppInfo.ProjectReloading = True

                    ' that's all, it's resposibility of user to do the rest:
                    ' close the project and open it once more
                    Return proj
                End If
            End If
        End If

        Return Nothing
    End Function
#End Region
End Class
