'Imports DotSpatial.Projections
'Imports DotSpatial.Projections.Forms

'Public Class clsProjectionTester

'    Private Shared Function GetProjectionFromLayer(ByRef newLayer As Object) As String
'        If newLayer Is Nothing Then
'            Return ""
'        End If

'        Dim lyrProjection As String = ""
'        If TypeOf newLayer Is MapWinGIS.Shapefile Then
'            lyrProjection = CType(newLayer, MapWinGIS.Shapefile).Projection
'        ElseIf TypeOf newLayer Is MapWinGIS.Grid Then
'            lyrProjection = CType(newLayer, MapWinGIS.Grid).Header.Projection
'        ElseIf TypeOf newLayer Is MapWinGIS.Image Then
'            lyrProjection = CType(newLayer, MapWinGIS.Image).GetProjection()
'        End If

'        If lyrProjection Is Nothing Then
'            lyrProjection = ""
'        ElseIf Not (lyrProjection.ToLower.StartsWith("+proj")) Then
'            lyrProjection = ""
'            ' Get rid of things like "UTM Zone 12 North" rather than "+proj=utm +zone=12 +ellps=GRS80 +units=m +no_defs"
'        End If

'        If Not (modMain.frmMain.Project.ProjectProjection.ToLower.StartsWith("+proj")) Then
'            modMain.frmMain.Project.ProjectProjection = ""
'            ' Get rid of things like "UTM Zone 12 North" rather than "+proj=utm +zone=12 +ellps=GRS80 +units=m +no_defs"
'        End If
'        Return lyrProjection
'    End Function

'    Private Shared Sub SetProjectProjection(ByVal newProjection As String)
'        ' Try using the layer projection
'        If newProjection = "" Then
'            'Do nothing - just work in "no projection" mode
'        Else
'            modMain.frmMain.Project.ProjectProjection = newProjection
'        End If
'    End Sub

'    'Test the projection and rectify as needed.
'    'NewLayer may be changed, as may lyrFilename - this is if it's reprojected
'    'to a different file. Abort signifies aborting of adding the layer.
'    Public Shared Sub AlterProjection(ByRef newLayer As Object, ByRef abort As Boolean, ByRef lyrFilename As String)
'        Try
'            If newLayer Is Nothing Then
'                Return
'            End If

'            Dim lyrProjection As String = GetProjectionFromLayer(newLayer)

'            ' hack: for Paul Meams, for the time being, to load tiles when they don't return projection information.
'            If TypeOf newLayer Is MapWinGIS.Image And lyrProjection = "" Then
'                Return
'            End If

'            '   The project has no projection.
'            If modMain.frmMain.Project.ProjectProjection = "" Then
'                SetProjectProjection(lyrProjection)
'            Else
'                If lyrProjection.Trim() = modMain.frmMain.Project.ProjectProjection.Trim() Then
'                    Return
'                End If

'                If AppInfo.NeverShowProjectionDialog Then Return

'                'There might be trouble if we can't simply ask, but must overwrite an existing file etc in MapWindow
'                ' Need further testing to determine that

'                'todo: use AppInfo.ProjectionDialog_PreviousMismatchAnswer

'                Dim p As New clsProjections
'                Dim prj As New clsProjections.clsProjection
'                prj = p.FindProjectionByPROJ4(frmMain.Project.ProjectProjection)

'                Dim form As New UndefinedProjectionDialog()
'                form.OriginalString = lyrProjection

'                Dim mapProjection As ProjectionInfo = New ProjectionInfo(modMain.frmMain.Project.ProjectProjection.ToString())
'                mapProjection.Name = prj.Name
'                form.MapProjection = mapProjection
'                form.ShowDialog()

'                Dim newProjection As String = form.Result.ToProj4String()

'                If TypeOf newLayer Is MapWinGIS.Shapefile Then
'                    CType(newLayer, MapWinGIS.Shapefile).Projection = newProjection
'                ElseIf TypeOf newLayer Is MapWinGIS.Grid Then
'                    CType(newLayer, MapWinGIS.Grid).AssignNewProjection(newProjection)
'                ElseIf TypeOf newLayer Is MapWinGIS.Image Then
'                    CType(newLayer, MapWinGIS.Image).SetProjection(newProjection)
'                End If

'                If form.AlwaysUse Then
'                    AppInfo.ProjectionDialog_PreviousMismatchAnswer = newProjection
'                End If
'            End If
'        Catch e As Exception
'            'Most likely an exception from the map, an invalid prj file.
'            MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
'        End Try
'    End Sub


'    Private Function IsSameProjectionAsProject(ByVal proj As String) As Boolean
'        'Save a COM call if possible. No need to compare for equivalence if they're identical
'        If modMain.frmMain.Project.ProjectProjection.Trim() = proj.Trim() Then Return True

'        If clsProjections.PartsCompare(modMain.frmMain.Project.ProjectProjection, proj.Trim, True) Then Return True

'        If modMain.frmMain.MapMain.IsSameProjection(proj, modMain.frmMain.Project.ProjectProjection) Then
'            Return True
'        Else
'            Return False
'        End If
'    End Function


'End Class
