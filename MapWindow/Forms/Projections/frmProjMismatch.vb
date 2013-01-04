'11/11/2005 - Paul Meems - Starting with translating resourcefile into Dutch.
'8/9/2006 - pm - Translation of new strings into Dutch
'1/28/2007 - Jiri Kadlec - changed ResourceManager (message strings moved to GlobalResource.resx)

'Public Class frmProjMismatch
'    Inherits System.Windows.Forms.Form
'#Region "Declarations"
'    'changed by JiriKadlec
'    Private resources As System.Resources.ResourceManager = _
'    New System.Resources.ResourceManager("MapWindow.GlobalResource", System.Reflection.Assembly.GetExecutingAssembly())

'    Friend WithEvents btnCancel As System.Windows.Forms.Button
'    Private Shared waitingForUser As Boolean
'#End Region

'#Region " Windows Form Designer generated code "

'    Public Sub New()
'        MyBase.New()

'        'This call is required by the Windows Form Designer.
'        InitializeComponent()

'        'Add any initialization after the InitializeComponent() call

'    End Sub

'    'Form overrides dispose to clean up the component list.
'    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
'        If disposing Then
'            If Not (components Is Nothing) Then
'                components.Dispose()
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    'Required by the Windows Form Designer
'    Private components As System.ComponentModel.IContainer

'    'NOTE: The following procedure is required by the Windows Form Designer
'    'It can be modified using the Windows Form Designer.  
'    'Do not modify it using the code editor.
'    Friend WithEvents grpProjectionMismatch As System.Windows.Forms.GroupBox
'    Friend WithEvents DiffProjectionLabel As System.Windows.Forms.Label
'    Friend WithEvents Label2 As System.Windows.Forms.Label
'    Friend WithEvents rdReproject As System.Windows.Forms.RadioButton
'    Friend WithEvents rdNothing As System.Windows.Forms.RadioButton
'    Friend WithEvents pnlReprojectOptions As System.Windows.Forms.Panel
'    Friend WithEvents rdOverwrite As System.Windows.Forms.RadioButton
'    Friend WithEvents rdNewFile As System.Windows.Forms.RadioButton
'    Friend WithEvents rdAbort As System.Windows.Forms.RadioButton
'    Friend WithEvents btnOK As System.Windows.Forms.Button
'    Friend WithEvents grpNoLayerProjection As System.Windows.Forms.GroupBox
'    Friend WithEvents rdNothing_2 As System.Windows.Forms.RadioButton
'    Friend WithEvents rdSetLayerToMap As System.Windows.Forms.RadioButton
'    Friend WithEvents Label3 As System.Windows.Forms.Label
'    Friend WithEvents NoProjectionLabel As System.Windows.Forms.Label
'    Friend WithEvents Label5 As System.Windows.Forms.Label
'    Friend WithEvents chbDoNothing As System.Windows.Forms.CheckBox
'    Friend WithEvents chbDifferingProj_UseForSession As System.Windows.Forms.CheckBox
'    Friend WithEvents chbNoProj_UseForSession As System.Windows.Forms.CheckBox
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjMismatch))
'        Me.grpProjectionMismatch = New System.Windows.Forms.GroupBox
'        Me.chbDifferingProj_UseForSession = New System.Windows.Forms.CheckBox
'        Me.pnlReprojectOptions = New System.Windows.Forms.Panel
'        Me.rdNewFile = New System.Windows.Forms.RadioButton
'        Me.rdOverwrite = New System.Windows.Forms.RadioButton
'        Me.rdNothing = New System.Windows.Forms.RadioButton
'        Me.rdReproject = New System.Windows.Forms.RadioButton
'        Me.Label2 = New System.Windows.Forms.Label
'        Me.DiffProjectionLabel = New System.Windows.Forms.Label
'        Me.rdAbort = New System.Windows.Forms.RadioButton
'        Me.btnOK = New System.Windows.Forms.Button
'        Me.grpNoLayerProjection = New System.Windows.Forms.GroupBox
'        Me.chbNoProj_UseForSession = New System.Windows.Forms.CheckBox
'        Me.Label5 = New System.Windows.Forms.Label
'        Me.rdNothing_2 = New System.Windows.Forms.RadioButton
'        Me.rdSetLayerToMap = New System.Windows.Forms.RadioButton
'        Me.Label3 = New System.Windows.Forms.Label
'        Me.NoProjectionLabel = New System.Windows.Forms.Label
'        Me.chbDoNothing = New System.Windows.Forms.CheckBox
'        Me.btnCancel = New System.Windows.Forms.Button
'        Me.grpProjectionMismatch.SuspendLayout()
'        Me.pnlReprojectOptions.SuspendLayout()
'        Me.grpNoLayerProjection.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'grpProjectionMismatch
'        '
'        Me.grpProjectionMismatch.Controls.Add(Me.chbDifferingProj_UseForSession)
'        Me.grpProjectionMismatch.Controls.Add(Me.pnlReprojectOptions)
'        Me.grpProjectionMismatch.Controls.Add(Me.rdNothing)
'        Me.grpProjectionMismatch.Controls.Add(Me.rdReproject)
'        Me.grpProjectionMismatch.Controls.Add(Me.Label2)
'        Me.grpProjectionMismatch.Controls.Add(Me.DiffProjectionLabel)
'        Me.grpProjectionMismatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        resources.ApplyResources(Me.grpProjectionMismatch, "grpProjectionMismatch")
'        Me.grpProjectionMismatch.Name = "grpProjectionMismatch"
'        Me.grpProjectionMismatch.TabStop = False
'        '
'        'chbDifferingProj_UseForSession
'        '
'        resources.ApplyResources(Me.chbDifferingProj_UseForSession, "chbDifferingProj_UseForSession")
'        Me.chbDifferingProj_UseForSession.Name = "chbDifferingProj_UseForSession"
'        '
'        'pnlReprojectOptions
'        '
'        Me.pnlReprojectOptions.Controls.Add(Me.rdNewFile)
'        Me.pnlReprojectOptions.Controls.Add(Me.rdOverwrite)
'        resources.ApplyResources(Me.pnlReprojectOptions, "pnlReprojectOptions")
'        Me.pnlReprojectOptions.Name = "pnlReprojectOptions"
'        '
'        'rdNewFile
'        '
'        Me.rdNewFile.Checked = True
'        resources.ApplyResources(Me.rdNewFile, "rdNewFile")
'        Me.rdNewFile.Name = "rdNewFile"
'        Me.rdNewFile.TabStop = True
'        '
'        'rdOverwrite
'        '
'        resources.ApplyResources(Me.rdOverwrite, "rdOverwrite")
'        Me.rdOverwrite.Name = "rdOverwrite"
'        '
'        'rdNothing
'        '
'        resources.ApplyResources(Me.rdNothing, "rdNothing")
'        Me.rdNothing.Name = "rdNothing"
'        '
'        'rdReproject
'        '
'        Me.rdReproject.Checked = True
'        resources.ApplyResources(Me.rdReproject, "rdReproject")
'        Me.rdReproject.Name = "rdReproject"
'        Me.rdReproject.TabStop = True
'        '
'        'Label2
'        '
'        resources.ApplyResources(Me.Label2, "Label2")
'        Me.Label2.Name = "Label2"
'        '
'        'DiffProjectionLabel
'        '
'        resources.ApplyResources(Me.DiffProjectionLabel, "DiffProjectionLabel")
'        Me.DiffProjectionLabel.Name = "DiffProjectionLabel"
'        '
'        'rdAbort
'        '
'        resources.ApplyResources(Me.rdAbort, "rdAbort")
'        Me.rdAbort.Name = "rdAbort"
'        '
'        'btnOK
'        '
'        resources.ApplyResources(Me.btnOK, "btnOK")
'        Me.btnOK.Name = "btnOK"
'        '
'        'grpNoLayerProjection
'        '
'        Me.grpNoLayerProjection.Controls.Add(Me.chbNoProj_UseForSession)
'        Me.grpNoLayerProjection.Controls.Add(Me.Label5)
'        Me.grpNoLayerProjection.Controls.Add(Me.rdNothing_2)
'        Me.grpNoLayerProjection.Controls.Add(Me.rdSetLayerToMap)
'        Me.grpNoLayerProjection.Controls.Add(Me.Label3)
'        Me.grpNoLayerProjection.Controls.Add(Me.NoProjectionLabel)
'        Me.grpNoLayerProjection.Controls.Add(Me.rdAbort)
'        Me.grpNoLayerProjection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        resources.ApplyResources(Me.grpNoLayerProjection, "grpNoLayerProjection")
'        Me.grpNoLayerProjection.Name = "grpNoLayerProjection"
'        Me.grpNoLayerProjection.TabStop = False
'        '
'        'chbNoProj_UseForSession
'        '
'        resources.ApplyResources(Me.chbNoProj_UseForSession, "chbNoProj_UseForSession")
'        Me.chbNoProj_UseForSession.Name = "chbNoProj_UseForSession"
'        '
'        'Label5
'        '
'        resources.ApplyResources(Me.Label5, "Label5")
'        Me.Label5.Name = "Label5"
'        '
'        'rdNothing_2
'        '
'        resources.ApplyResources(Me.rdNothing_2, "rdNothing_2")
'        Me.rdNothing_2.Name = "rdNothing_2"
'        '
'        'rdSetLayerToMap
'        '
'        Me.rdSetLayerToMap.Checked = True
'        resources.ApplyResources(Me.rdSetLayerToMap, "rdSetLayerToMap")
'        Me.rdSetLayerToMap.Name = "rdSetLayerToMap"
'        Me.rdSetLayerToMap.TabStop = True
'        '
'        'Label3
'        '
'        resources.ApplyResources(Me.Label3, "Label3")
'        Me.Label3.Name = "Label3"
'        '
'        'NoProjectionLabel
'        '
'        resources.ApplyResources(Me.NoProjectionLabel, "NoProjectionLabel")
'        Me.NoProjectionLabel.Name = "NoProjectionLabel"
'        '
'        'chbDoNothing
'        '
'        resources.ApplyResources(Me.chbDoNothing, "chbDoNothing")
'        Me.chbDoNothing.Name = "chbDoNothing"
'        '
'        'btnCancel
'        '
'        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
'        resources.ApplyResources(Me.btnCancel, "btnCancel")
'        Me.btnCancel.Name = "btnCancel"
'        '
'        'frmProjMismatch
'        '
'        Me.AcceptButton = Me.btnOK
'        resources.ApplyResources(Me, "$this")
'        Me.CancelButton = Me.btnCancel
'        Me.ControlBox = False
'        Me.Controls.Add(Me.btnCancel)
'        Me.Controls.Add(Me.chbDoNothing)
'        Me.Controls.Add(Me.btnOK)
'        Me.Controls.Add(Me.grpProjectionMismatch)
'        Me.Controls.Add(Me.grpNoLayerProjection)
'        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
'        Me.MaximizeBox = False
'        Me.MinimizeBox = False
'        Me.Name = "frmProjMismatch"
'        Me.grpProjectionMismatch.ResumeLayout(False)
'        Me.pnlReprojectOptions.ResumeLayout(False)
'        Me.grpNoLayerProjection.ResumeLayout(False)
'        Me.ResumeLayout(False)

'    End Sub

'#End Region

'    'Test the projection and rectify as needed.
'    'NewLayer may be changed, as may lyrFilename - this is if it's reprojected
'    'to a different file. Abort signifies aborting of adding the layer.
'    Public Sub TestProjection(ByRef newLayer As Object, ByRef abort As Boolean, ByRef lyrFilename As String)
'        Try
'            waitingForUser = False

'            If newLayer Is Nothing Then
'                GoAway()
'                Return
'            End If

'            Dim lyrProjection As New MapWinGIS.GeoProjection
'            If TypeOf newLayer Is MapWinGIS.Shapefile Then
'                lyrProjection = CType(newLayer, MapWinGIS.Shapefile).GeoProjection
'            ElseIf TypeOf newLayer Is MapWinGIS.Grid Then
'                lyrProjection = CType(newLayer, MapWinGIS.Grid).Header.GeoProjection
'            End If

'            'Case 1:
'            '   The project has no projection.
'            If modMain.frmMain.Project.GeoProjection.IsEmpty Then
'                'Case 1a: The input object has a projection.
'                If Not lyrProjection.IsEmpty Then
'                    'Set this projection as the project projection, and life goes on.
'                    modMain.frmMain.Project.GeoProjection = lyrProjection
'                    GoAway()
'                    Return
'                End If

'                'Case 1b: The input object has no projection.
'                If lyrProjection.IsEmpty Then
'                    'Do nothing - just work in "no projection" mode
'                    GoAway()
'                    Return
'                End If
'            End If

'            'Case 2:
'            '   The project has a projection.
'            If Not modMain.frmMain.Project.GeoProjection.IsEmpty Then

'                'Case 2a: The input object has a projection.
'                If Not lyrProjection.IsEmpty Then

'                    'Case 2a-1: Layer projection == Project projection
'                    If modMain.frmMain.Project.GeoProjection.IsSame(lyrProjection) Then
'                        'Do nothing - everything is in accordance
'                        GoAway()
'                        Return
'                    Else

'                        'Case 2a-2: Layer projection != project projection. Warn user.
'                        If AppInfo.NeverShowProjectionDialog Then
'                            'Never Show This Window ("Always Do Nothing") was checked
'                            'at some point in history. Default to do nothing.
'                            rdNothing.Checked = True
'                        Else
'                            If AppInfo.ProjectionDialog_PreviousMismatchAnswer <> "" Then
'                                Me.LoadPreviousAnswer(AppInfo.ProjectionDialog_PreviousMismatchAnswer)
'                            Else
'                                grpProjectionMismatch.Visible = True
'                                'PM added:
'                                grpNoLayerProjection.Visible = False

'                                DiffProjectionLabel.Text &= vbCrLf & vbCrLf & resources.GetString("msgProjectProjection.Text") 'JK - localization

'                                ' TODO: search projection name from EPSG database
'                                Dim p As New clsProjections
'                                Dim prj As clsProjections.clsProjection = p.FindProjectionByPROJ4(frmMain.Project.GeoProjection.ExportToProj4())
'                                If Not prj Is Nothing Then
'                                    DiffProjectionLabel.Text &= prj.Name
'                                Else
'                                    DiffProjectionLabel.Text &= resources.GetString("msgCustomProjection.Text") 'JK-localization "Custom"
'                                End If

'                                ' TODO: search projection name from EPSG database
'                                DiffProjectionLabel.Text &= vbCrLf & resources.GetString("msgNewLayerProjection.Text") 'JK - localization "New Layer: "
'                                Dim prj2 As clsProjections.clsProjection = p.FindProjectionByPROJ4(lyrProjection.ExportToProj4())
'                                If Not prj2 Is Nothing Then
'                                    DiffProjectionLabel.Text &= prj2.Name
'                                Else
'                                    DiffProjectionLabel.Text &= resources.GetString("msgCustomProjection.Text") 'JK - localization of "Custom"
'                                End If

'                                grpProjectionMismatch.BringToFront()
'                                Me.Text = resources.GetString("WarningProjectionMismatch.Text")
'                                waitingForUser = True

'                                DiffProjectionLabel.Text += vbCrLf + resources.GetString("msgFileName.Text") + System.IO.Path.GetFileName(lyrFilename)

'                                If Me.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
'                                    abort = True
'                                    GoAway()
'                                    Return
'                                End If

'                                'Did they just close the window?
'                                If Not Me.DialogResult = DialogResult.OK Then Exit Sub

'                                If (chbDifferingProj_UseForSession.Checked) Then
'                                    Me.SavePreviousMismatchAnswer(AppInfo.ProjectionDialog_PreviousMismatchAnswer)
'                                End If
'                            End If
'                        End If

'                        'Check the user-selected options and do whatever they said.
'                        If rdReproject.Checked Then
'                            If TypeOf newLayer Is MapWinGIS.Shapefile Then
'                                Dim sfSource As MapWinGIS.Shapefile = CType(newLayer, MapWinGIS.Shapefile)
'                                Dim origFilename As String = sfSource.Filename
'                                Dim sfNew As MapWinGIS.Shapefile
'                                Dim count As Integer

'                                Dim orig As Cursor = Cursor
'                                Cursor = Cursors.WaitCursor

'                                If rdOverwrite.Checked Then

'                                    sfNew = New MapWinGIS.Shapefile
'                                    If sfNew.Open(origFilename, sfSource.GlobalCallback) Then
'                                        If sfNew.ReprojectInPlace(modMain.frmMain.Project.GeoProjection, count) Then
'                                            If sfNew.NumShapes = count Then
'                                                sfSource.Close()
'                                                If sfNew.Save() Then
'                                                    newLayer = sfNew
'                                                End If
'                                            End If
'                                        End If
'                                    End If

'                                ElseIf rdNewFile.Checked Then

'                                    ' TODO: probably more projection name should be included in the filename
'                                    Dim newFilename As String = System.IO.Path.GetDirectoryName(origFilename)
'                                    newFilename = newFilename & CType(IIf(newFilename.EndsWith("\"), "", "\"), String) & System.IO.Path.GetFileNameWithoutExtension(origFilename) + "_reprojected.shp"

'                                    sfNew = sfSource.Reproject(modMain.frmMain.Project.GeoProjection, count)
'                                    If Not sfNew Is Nothing Then
'                                        sfNew.GlobalCallback = sfSource.GlobalCallback
'                                        sfNew.SaveAs(newFilename)
'                                        sfSource.Close()
'                                        newLayer = sfNew
'                                    End If
'                                End If

'                                Cursor = orig
'                            ElseIf TypeOf newLayer Is MapWinGIS.Grid Then
'                                If rdOverwrite.Checked Then
'                                    Dim origFilename As String = CType(newLayer, MapWinGIS.Grid).Filename
'                                    Dim tempFilename As String = GetMWTempFile() + "." + System.IO.Path.GetExtension(origFilename)
'                                    CType(newLayer, MapWinGIS.Grid).Close()

'                                    Dim orig As Cursor = Cursor
'                                    Cursor = Cursors.WaitCursor
'                                    MapWinGeoProc.SpatialReference.ProjectGrid(lyrProjection.ExportToProj4(), modMain.ProjInfo.ProjectProjection, origFilename, tempFilename, True)
'                                    Cursor = orig

'                                    Try
'                                        Kill(origFilename)
'                                    Catch ex As Exception
'                                    End Try

'                                    System.IO.File.Move(tempFilename, origFilename)

'                                    'Reopen the layer to continue adding it
'                                    CType(newLayer, MapWinGIS.Grid).Open(origFilename)
'                                    CType(newLayer, MapWinGIS.Grid).AssignNewProjection(modMain.ProjInfo.ProjectProjection)
'                                ElseIf rdNewFile.Checked Then
'                                    Dim origFilename As String = CType(newLayer, MapWinGIS.Grid).Filename
'                                    Dim newext As String = System.IO.Path.GetExtension(origFilename)
'                                    Dim newFilename As String = System.IO.Path.GetDirectoryName(origFilename)
'                                    newFilename = newFilename & CType(IIf(newFilename.EndsWith("\"), "", "\"), String) & System.IO.Path.GetFileNameWithoutExtension(origFilename) + "_reprojected" + newext

'                                    CType(newLayer, MapWinGIS.Grid).Close()

'                                    Dim orig As Cursor = Cursor
'                                    Cursor = Cursors.WaitCursor
'                                    MapWinGeoProc.SpatialReference.ProjectGrid(lyrProjection.ExportToProj4(), modMain.ProjInfo.ProjectProjection, origFilename, newFilename, True)
'                                    Cursor = orig

'                                    If Not System.IO.File.Exists(newFilename) Then
'                                        'PM
'                                        'mapwinutility.logger.msg("Error: The reprojected grid file doesn't exist! Check for an errorlog.txt. Aborting layer add.", MsgBoxStyle.Exclamation)
'                                        MapWinUtility.Logger.Msg(resources.GetString("msgReprojectedGridDoesNotExists.Text"), MsgBoxStyle.Exclamation)
'                                        abort = True
'                                        GoAway()
'                                        Return
'                                    Else
'                                        'Reopen the layer to continue adding it
'                                        CType(newLayer, MapWinGIS.Grid).Open(newFilename)
'                                        CType(newLayer, MapWinGIS.Grid).AssignNewProjection(modMain.ProjInfo.ProjectProjection)
'                                        lyrFilename = newFilename
'                                    End If
'                                End If
'                            End If
'                        ElseIf rdNothing.Checked Then
'                            'Exactly as it says... nothing.
'                        ElseIf rdAbort.Checked Then
'                            'Flag an abort for the caller (clsLayers)
'                            abort = True
'                        End If

'                        GoAway()
'                        Return
'                    End If
'                End If

'                    'Case 2b: The input layer has no projection.
'                If lyrProjection.IsEmpty Then
'                    'Prompt the user to see if they'd like to set this layer's projection to the project projection
'                    If AppInfo.NeverShowProjectionDialog Then
'                        'Never Show This Window ("Always Do Nothing") was checked
'                        'at some point in history. Default to do nothing.
'                        rdNothing.Checked = True
'                    Else
'                        If AppInfo.ProjectionDialog_PreviousNoProjAnswer <> "" Then
'                            LoadPreviousAnswer(AppInfo.ProjectionDialog_PreviousNoProjAnswer)
'                        Else
'                            grpNoLayerProjection.Visible = True
'                            'PM Added:
'                            grpProjectionMismatch.Visible = False

'                            NoProjectionLabel.Text &= vbCrLf & vbCrLf & resources.GetString("msgProjection.Text") 'JK - localization of "Projection: "
'                            Dim p As New clsProjections
'                            Dim prj As New clsProjections.clsProjection
'                            prj = p.FindProjectionByPROJ4(frmMain.Project.ProjectProjection)
'                            If Not prj Is Nothing Then
'                                NoProjectionLabel.Text &= prj.Name
'                            Else
'                                NoProjectionLabel.Text &= resources.GetString("msgCustomProjection.Text") 'JK - localization of "Custom"
'                            End If

'                            grpNoLayerProjection.BringToFront()
'                            'PM Changed:
'                            'Me.Text = "Warning: No Projection on Layer"
'                            Me.Text = resources.GetString("WarningNoProjectionOnLayer.Text")

'                            NoProjectionLabel.Text += vbCrLf + resources.GetString("msgFileName.Text") + System.IO.Path.GetFileName(lyrFilename) 'JK-localization of "File name: "

'                            waitingForUser = True
'                            If Me.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
'                                abort = True
'                                GoAway()
'                                Return
'                            End If

'                            'Did they close the window?
'                            If Not Me.DialogResult = DialogResult.OK Then Exit Sub

'                            If (chbNoProj_UseForSession.Checked) Then
'                                SavePreviousNoProjAnswer(AppInfo.ProjectionDialog_PreviousNoProjAnswer)
'                            End If
'                        End If
'                    End If

'                    'Do what the user specified
'                    If (rdSetLayerToMap.Checked) Then
'                        If TypeOf newLayer Is MapWinGIS.Shapefile Then
'                            CType(newLayer, MapWinGIS.Shapefile).GeoProjection = modMain.ProjInfo.GeoProjection
'                        ElseIf TypeOf newLayer Is MapWinGIS.Grid Then
'                            CType(newLayer, MapWinGIS.Grid).AssignNewProjection(modMain.ProjInfo.GeoProjection.ExportToProj4())
'                        End If
'                    ElseIf (rdAbort.Checked) Then
'                        abort = True
'                    End If

'                    'Don't bother to check the 'DoNothing' option, we'll exit anyhow.
'                    GoAway()
'                    Return
'                End If
'            End If

'            'Code execution should not get here, but if it does, make this all go away
'            GoAway()
'        Catch e As Exception
'            'Most likely an exception from the map, an invalid prj file.
'            MapWinUtility.Logger.Dbg("DEBUG: " + e.ToString())
'        End Try
'    End Sub

'    Private Function IsSameProjectionAsProject(ByVal proj As String) As Boolean
'        'Save a COM call if possible. No need to compare for equivalence if they're identical
'        If modMain.frmMain.Project.ProjectProjection.Trim() = proj.Trim() Then Return True

'        If clsProjections.PartsCompare(modMain.frmMain.Project.ProjectProjection, proj.Trim, True) Then Return True

'        If modMain.frmMain.MapMain.IsSameProjection(proj, modMain.frmMain.Project.ProjectProjection) Then Return True

'        Return False
'    End Function

'    Private Sub GoAway()
'        If (waitingForUser) Then
'            waitingForUser = False
'        Else
'            Me.Close()
'        End If
'    End Sub

'    Private Sub frmProjMismatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'    End Sub

'    Private Sub rdReproject_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdReproject.CheckedChanged
'        pnlReprojectOptions.Enabled = rdReproject.Checked
'    End Sub

'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
'        waitingForUser = False
'        Me.DialogResult = DialogResult.OK
'        Me.Hide()
'    End Sub

'    Private Sub LoadPreviousAnswer(ByVal PreviousAnswer As String)
'        Select Case PreviousAnswer
'            'No Projection
'            Case rdNothing_2.Name
'                rdNothing_2.Checked = True
'            Case rdSetLayerToMap.Name
'                rdSetLayerToMap.Checked = True

'                'Mismatched Projection
'            Case rdOverwrite.Name
'                rdReproject.Checked = True
'                rdOverwrite.Checked = True
'            Case rdNewFile.Name
'                rdReproject.Checked = True
'                rdNewFile.Checked = True
'            Case rdNothing.Name
'                rdNothing.Checked = True
'            Case rdAbort.Name
'                rdAbort.Checked = True
'        End Select
'    End Sub

'    Private Sub SavePreviousMismatchAnswer(ByRef PreviousMismatchAnswer As String)
'        If rdReproject.Checked And rdOverwrite.Checked Then
'            PreviousMismatchAnswer = rdOverwrite.Name
'        ElseIf rdReproject.Checked And rdNewFile.Checked Then
'            PreviousMismatchAnswer = rdNewFile.Name
'        ElseIf rdNothing.Checked Then
'            PreviousMismatchAnswer = rdNothing.Name
'        ElseIf rdAbort.Checked Then
'            PreviousMismatchAnswer = rdAbort.Name
'        End If
'    End Sub

'    Private Sub SavePreviousNoProjAnswer(ByRef PreviousNoProjAnswer As String)
'        If rdNothing_2.Checked Then
'            PreviousNoProjAnswer = rdNothing_2.Name
'        ElseIf rdSetLayerToMap.Checked Then
'            PreviousNoProjAnswer = rdSetLayerToMap.Name
'        End If
'    End Sub

'    Private Sub chbDoNothing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDoNothing.CheckedChanged
'        If (Me.Visible) Then
'            If (chbDoNothing.Checked) Then
'                rdNothing.Checked = True
'                rdNothing_2.Checked = True

'                AppInfo.NeverShowProjectionDialog = True
'            Else
'                AppInfo.NeverShowProjectionDialog = False
'            End If
'        End If
'    End Sub

'    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
'        waitingForUser = False
'        Me.DialogResult = DialogResult.Cancel
'        Me.Hide()
'    End Sub
'End Class
