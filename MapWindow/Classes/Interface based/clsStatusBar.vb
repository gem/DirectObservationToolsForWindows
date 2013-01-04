Imports MapWindow.Controls.Projections
Imports System.Globalization

Public Class StatusBar
    Inherits Form
    Implements Interfaces.StatusBar


#Region "Declarations"
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusBarPanelStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarPanelScale As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusTooltip As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusAlternativeUnits As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusUnits As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusPlaceHolder As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarPanelProjection As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents btnChoose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAbsenceBehavior As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMismatchBehavior As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnShowWarnings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnShowReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAbsenceAssign As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAbsenceIgnore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAbsenceSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMismatchIgnore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMismatchProjectOld As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMismatchSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip
#End Region

#Region "Initialization"
    Public Sub New()
        InitializeComponent()
        Me.Controls.Remove(Me.StatusBar1)
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.StatusBar1 = New System.Windows.Forms.StatusStrip
        Me.StatusBarPanelProjection = New System.Windows.Forms.ToolStripSplitButton
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btnChoose = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAbsenceBehavior = New System.Windows.Forms.ToolStripMenuItem
        Me.btnAbsenceAssign = New System.Windows.Forms.ToolStripMenuItem
        Me.btnAbsenceIgnore = New System.Windows.Forms.ToolStripMenuItem
        Me.btnAbsenceSkip = New System.Windows.Forms.ToolStripMenuItem
        Me.btnMismatchBehavior = New System.Windows.Forms.ToolStripMenuItem
        Me.btnMismatchIgnore = New System.Windows.Forms.ToolStripMenuItem
        Me.btnMismatchProjectOld = New System.Windows.Forms.ToolStripMenuItem
        Me.btnMismatchSkip = New System.Windows.Forms.ToolStripMenuItem
        Me.btnShowWarnings = New System.Windows.Forms.ToolStripMenuItem
        Me.btnShowReport = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnProperties = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusPlaceHolder = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusUnits = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusAlternativeUnits = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusTooltip = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarPanelScale = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarPanelStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.StatusBar1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusBar1
        '
        Me.StatusBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusBarPanelProjection, Me.StatusPlaceHolder, Me.StatusUnits, Me.StatusAlternativeUnits, Me.StatusTooltip, Me.StatusBarPanelScale, Me.StatusBarPanelStatus, Me.ProgressBar1})
        Me.StatusBar1.Location = New System.Drawing.Point(0, 248)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(629, 24)
        Me.StatusBar1.TabIndex = 5
        Me.StatusBar1.Text = "StatusStrip1"
        '
        'StatusBarPanelProjection
        '
        Me.StatusBarPanelProjection.DropDown = Me.ContextMenuStrip1
        Me.StatusBarPanelProjection.Image = Global.MapWindow.GlobalResource.imgMap
        Me.StatusBarPanelProjection.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.StatusBarPanelProjection.Name = "StatusBarPanelProjection"
        Me.StatusBarPanelProjection.Size = New System.Drawing.Size(102, 22)
        Me.StatusBarPanelProjection.Text = "Not defined"
        Me.StatusBarPanelProjection.ToolTipText = "Project coordinate system and projection"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnChoose, Me.ToolStripSeparator1, Me.btnAbsenceBehavior, Me.btnMismatchBehavior, Me.btnShowWarnings, Me.btnShowReport, Me.ToolStripSeparator2, Me.btnProperties})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.OwnerItem = Me.StatusBarPanelProjection
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(182, 170)
        '
        'btnChoose
        '
        Me.btnChoose.Name = "btnChoose"
        Me.btnChoose.Size = New System.Drawing.Size(181, 22)
        Me.btnChoose.Text = "Choose projection"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(178, 6)
        '
        'btnAbsenceBehavior
        '
        Me.btnAbsenceBehavior.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAbsenceAssign, Me.btnAbsenceIgnore, Me.btnAbsenceSkip})
        Me.btnAbsenceBehavior.Name = "btnAbsenceBehavior"
        Me.btnAbsenceBehavior.Size = New System.Drawing.Size(181, 22)
        Me.btnAbsenceBehavior.Text = "Absence behavior"
        '
        'btnAbsenceAssign
        '
        Me.btnAbsenceAssign.Name = "btnAbsenceAssign"
        Me.btnAbsenceAssign.Size = New System.Drawing.Size(178, 22)
        Me.btnAbsenceAssign.Text = "Assign from project"
        '
        'btnAbsenceIgnore
        '
        Me.btnAbsenceIgnore.Name = "btnAbsenceIgnore"
        Me.btnAbsenceIgnore.Size = New System.Drawing.Size(178, 22)
        Me.btnAbsenceIgnore.Text = "Ignore the absence"
        '
        'btnAbsenceSkip
        '
        Me.btnAbsenceSkip.Name = "btnAbsenceSkip"
        Me.btnAbsenceSkip.Size = New System.Drawing.Size(178, 22)
        Me.btnAbsenceSkip.Text = "Skip the file"
        '
        'btnMismatchBehavior
        '
        Me.btnMismatchBehavior.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnMismatchIgnore, Me.btnMismatchProjectOld, Me.btnMismatchSkip})
        Me.btnMismatchBehavior.Name = "btnMismatchBehavior"
        Me.btnMismatchBehavior.Size = New System.Drawing.Size(181, 22)
        Me.btnMismatchBehavior.Text = "Mismatch behavior"
        '
        'btnMismatchIgnore
        '
        Me.btnMismatchIgnore.Name = "btnMismatchIgnore"
        Me.btnMismatchIgnore.Size = New System.Drawing.Size(164, 22)
        Me.btnMismatchIgnore.Text = "Ignore mismatch"
        '
        'btnMismatchProjectOld
        '
        Me.btnMismatchProjectOld.Name = "btnMismatchProjectOld"
        Me.btnMismatchProjectOld.Size = New System.Drawing.Size(164, 22)
        Me.btnMismatchProjectOld.Text = "Reproject the file"
        '
        'btnMismatchSkip
        '
        Me.btnMismatchSkip.Name = "btnMismatchSkip"
        Me.btnMismatchSkip.Size = New System.Drawing.Size(164, 22)
        Me.btnMismatchSkip.Text = "Skip the file"
        '
        'btnShowWarnings
        '
        Me.btnShowWarnings.Name = "btnShowWarnings"
        Me.btnShowWarnings.Size = New System.Drawing.Size(181, 22)
        Me.btnShowWarnings.Text = "Show warnings"
        '
        'btnShowReport
        '
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(181, 22)
        Me.btnShowReport.Text = "Show loading report"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(178, 6)
        '
        'btnProperties
        '
        Me.btnProperties.Name = "btnProperties"
        Me.btnProperties.Size = New System.Drawing.Size(181, 22)
        Me.btnProperties.Text = "Properties"
        '
        'StatusPlaceHolder
        '
        Me.StatusPlaceHolder.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.StatusPlaceHolder.Name = "StatusPlaceHolder"
        Me.StatusPlaceHolder.Size = New System.Drawing.Size(4, 19)
        '
        'StatusUnits
        '
        Me.StatusUnits.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.StatusUnits.Name = "StatusUnits"
        Me.StatusUnits.Size = New System.Drawing.Size(4, 19)
        Me.StatusUnits.Visible = False
        '
        'StatusAlternativeUnits
        '
        Me.StatusAlternativeUnits.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.StatusAlternativeUnits.Name = "StatusAlternativeUnits"
        Me.StatusAlternativeUnits.Size = New System.Drawing.Size(4, 19)
        Me.StatusAlternativeUnits.Visible = False
        '
        'StatusTooltip
        '
        Me.StatusTooltip.Name = "StatusTooltip"
        Me.StatusTooltip.Size = New System.Drawing.Size(160, 19)
        Me.StatusTooltip.Spring = True
        '
        'StatusBarPanelScale
        '
        Me.StatusBarPanelScale.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarPanelScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.StatusBarPanelScale.DoubleClickEnabled = True
        Me.StatusBarPanelScale.Name = "StatusBarPanelScale"
        Me.StatusBarPanelScale.Size = New System.Drawing.Size(26, 19)
        Me.StatusBarPanelScale.Text = "1:1"
        Me.StatusBarPanelScale.ToolTipText = "Double click to show the scale"
        '
        'StatusBarPanelStatus
        '
        Me.StatusBarPanelStatus.Name = "StatusBarPanelStatus"
        Me.StatusBarPanelStatus.Size = New System.Drawing.Size(160, 19)
        Me.StatusBarPanelStatus.Spring = True
        Me.StatusBarPanelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProgressBar1
        '
        Me.ProgressBar1.AutoSize = False
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(120, 18)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.Visible = False
        '
        'StatusBar
        '
        Me.ClientSize = New System.Drawing.Size(629, 272)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "StatusBar"
        Me.StatusBar1.ResumeLayout(False)
        Me.StatusBar1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region "Handling map events"
    Friend Sub HandleProjectionChanged(ByVal oldProjection As MapWinGIS.GeoProjection, ByVal newProjection As MapWinGIS.GeoProjection)
        Me.StatusBarPanelProjection.Text = IIf(Not newProjection.IsEmpty, newProjection.Name, "Not defined")
    End Sub

    Friend Sub HandleExtentsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Paul Meems 10 August 2009
        ' Add current scale to statusbar
        Dim scalePanel As ToolStripStatusLabel = StatusBar1.Items("StatusBarPanelScale")
        Dim scale As String = modMain.frmMain.View.Scale.ToString(CultureInfo.InvariantCulture)
        scalePanel.Text = "1:" + Math.Round(Double.Parse(scale, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture)
    End Sub

    Friend Sub HandleMapMouseMove(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseMoveEvent)
        'Update the status bar items if necessary, depending on the project settings.
        Try
            Dim x, y As Double

            Dim ScreenX As Integer = e.x
            Dim Screeny As Integer = e.y

            Dim m_AlternateCoordToolbar As ToolStripStatusLabel = StatusAlternativeUnits
            Dim m_ProjectedCoordToolbar As ToolStripStatusLabel = StatusUnits
            modMain.frmMain.MapMain.PixelToProj(ScreenX, Screeny, x, y)

            'Provide lat/long (unprojected)
            'If no projection is provided, cannot do this.
            Try

                If Not (modMain.frmMain.Project.GeoProjection.IsEmpty() Or modMain.ProjInfo.m_MapUnits = "Lat/Long") Then
                    'If it's not set to anything and the map units are not lat/long, default to lat/long.
                    'TODO : shouldn't we rather show 'unknown units?'
                    If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" And Not modMain.ProjInfo.m_MapUnits = "" And Not modMain.ProjInfo.m_MapUnits = "Lat/Long" Then
                        modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long"
                    End If

                    'status bar for coordinates in alternate units
                    If Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "" And Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" Then
                        If Not m_AlternateCoordToolbar.Visible Then m_AlternateCoordToolbar.Visible = True

                        If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long" Then
                            'alternate units are in Lat/Long (decimal degrees) - reproject the point
                            If (Not MapWinGeoProc.SpatialReference.ProjectPoint(x, y, modMain.ProjInfo.ProjectProjection, "+proj=latlong +ellps=WGS84 +datum=WGS84")) Then
                                MapWinUtility.Logger.Dbg("DEBUG: " + MapWinGeoProc.Error.GetLastErrorMsg())
                            Else
                                StatusAlternativeUnits.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals, modMain.ProjInfo.StatusBarAlternateCoordsUseCommas, "Lat/Long")
                            End If

                        Else
                            'else do conversion from units to units
                            Dim UOMOrig As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Meters
                            Dim UOMDest As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Kilometers

                            '08/28/2008 Jiri Kadlec - use the new unit conversion function from MapWinGeoProc
                            UOMOrig = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnits)
                            UOMDest = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnitsAlternate)

                            Dim newText As String = ""
                            Dim numDecimals As Integer = modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals
                            Dim useCommas As Boolean = modMain.ProjInfo.StatusBarAlternateCoordsUseCommas
                            Try
                                Dim strX, strY As Double
                                strX = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, x)
                                strY = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, y)

                                newText = FormatCoords(strX, strY, numDecimals, useCommas, UOMDest.ToString())

                            Catch ex As Exception
                                newText = FormatCoords(x, y, numDecimals, useCommas, UOMOrig.ToString())
                            End Try
                            m_AlternateCoordToolbar.Text = newText

                        End If
                    Else
                        If m_AlternateCoordToolbar.Visible Then m_AlternateCoordToolbar.Visible = False
                    End If
                Else 'Can't do it - make sure the status bar panel isn't there
                    If m_AlternateCoordToolbar.Visible Then m_AlternateCoordToolbar.Visible = False
                End If
            Catch ex As Exception
                'Very likely a projection error.
                'Just display the coords.
                If m_AlternateCoordToolbar.Visible Then m_AlternateCoordToolbar.Visible = False
            End Try

            ' Re-get the coords...
            modMain.frmMain.MapMain.PixelToProj(ScreenX, Screeny, x, y)

            'Provide coordinate in projected system (may be unknown or just pixels in the case of images, etc)
            If (modMain.ProjInfo.ShowStatusBarCoords_Projected) Then
                If Not m_ProjectedCoordToolbar.Visible Then m_ProjectedCoordToolbar.Visible = True
                'Dim cvter As New ScaleBarUtility

                m_ProjectedCoordToolbar.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarCoordsNumDecimals, modMain.ProjInfo.StatusBarCoordsUseCommas, modMain.frmMain.Project.MapUnits)

            Else
                If m_ProjectedCoordToolbar.Visible Then m_ProjectedCoordToolbar.Visible = False
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.ToString())
        End Try
    End Sub

    Private Function FormatCoords(ByVal x As Double, ByVal y As Double, ByVal decimals As Integer, ByVal useCommas As String, ByVal units As String) As String
        '5/9/2008 Jiri Kadlec - this function is used by MapMouseMove() to format the coordinates in
        'the MW status bar.
        Dim nf As String 'the number formatting string

        If useCommas = True Then
            nf = "N" + decimals.ToString
        Else
            nf = "F" + decimals.ToString
        End If
        If units = "Lat/Long" Then
            Return String.Format("Lat: {0} Long: {1}", y.ToString(nf), x.ToString(nf))
        Else
            Return String.Format("X: {0} Y: {1} {2}", x.ToString(nf), y.ToString(nf), units)
        End If
    End Function
#End Region

#Region "Handling StatusStrip events"
    ''' <summary>
    ''' Handles item clicked event
    ''' </summary>
    Private Sub StatusBar1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles StatusBar1.ItemClicked

        If e.ClickedItem Is StatusBar1.Items("StatusBarPanelScale") Then
            ' it's breaking the encapsulation
            frmMain.DoSetScale()
        ElseIf e.ClickedItem Is Me.StatusBarPanelProjection Then
            'StatusBarPanelProjection_ButtonClick(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "StatusBar interface"
    Public Overloads Property Enabled() As Boolean Implements Interfaces.StatusBar.Enabled
        Get
            Enabled = StatusBar1.Enabled
        End Get
        Set(ByVal Value As Boolean)
            StatusBar1.Enabled = Value
        End Set
    End Property

    Public Property ShowProgressBar() As Boolean Implements Interfaces.StatusBar.ShowProgressBar
        Get
            Return ProgressBar1.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.ProgressBar1.Visible = Value
            If Not Value Then Me.StatusBarPanelStatus.Text = ""
            Application.DoEvents()
        End Set
    End Property

    Public Property ProgressBarValue() As Integer Implements MapWindow.Interfaces.StatusBar.ProgressBarValue
        Get
            Return ProgressBar1.Value
        End Get
        Set(ByVal Value As Integer)
            If Value > 100 Then
                Value = 100
            ElseIf Value < 0 Then
                Value = 0
            End If
            If Value > 0 Then
                If Not Me.ProgressBar1.Visible Then Me.ProgressBar1.Visible = True
            Else
                Me.ProgressBar1.Visible = False
            End If
            ProgressBar1.Value = Value
            Try
                Windows.Forms.Application.DoEvents()
            Catch
            End Try
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Function AddPanel(ByVal InsertAt As Integer) As Interfaces.StatusBarItem Implements Interfaces.StatusBar.AddPanel
        Try
            If InsertAt <= 0 Then InsertAt = 0
            If InsertAt > Me.StatusBar1.Items.Count Then InsertAt = Me.StatusBar1.Items.Count

            Dim newPanel As New ToolStripStatusLabel
            StatusBar1.Items.Insert(InsertAt, newPanel)

            Dim newItem As New MapWindow.StatusBarItem(newPanel)
            Return newItem
        Catch ex As Exception
            Throw New Exception("Failed to add StatusBar Panel." & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    <CLSCompliant(False)> _
    Public Function AddPanel() As Interfaces.StatusBarItem Implements Interfaces.StatusBar.AddPanel
        Return Me.AddPanel(Me.StatusBar1.Items.Count)
    End Function

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal Index As Integer) As Interfaces.StatusBarItem Implements Interfaces.StatusBar.Item
        Get
            If (Index < 0 Or Index >= Me.StatusBar1.Items.Count) Then Return Nothing
            Return New StatusBarItem(Me.StatusBar1.Items(Index))
        End Get
    End Property

    Public Sub RemovePanel(ByVal Index As Integer) Implements Interfaces.StatusBar.RemovePanel
        Try
            If (StatusBar1.Items.Count > Index) Then
                StatusBar1.Items.RemoveAt(Index)
            End If
            If NumPanels = 0 Then Dim item As MapWindow.Interfaces.StatusBarItem = Me.AddPanel()
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Public Sub RemovePanel1(ByRef panel As Interfaces.StatusBarItem) Implements Interfaces.StatusBar.RemovePanel
        If panel Is Nothing Then Return
        Dim item As MapWindow.StatusBarItem = TryCast(panel, MapWindow.StatusBarItem)
        If Not item Is Nothing Then
            For i As Integer = 0 To Me.StatusBar1.Items.Count - 1
                If item.m_item Is Me.StatusBar1.Items(i) Then
                    Me.RemovePanel(i)
                    Return
                End If
            Next
        End If
    End Sub

    <System.Obsolete("use overloaded method", True)> _
    Public Sub RemovePanel(ByRef Panel As System.Windows.Forms.StatusBarPanel) Implements MapWindow.Interfaces.StatusBar.RemovePanel
        ' it's not possible to remove StausBarPanel as ToolStripStatusLabels are used
    End Sub

    <System.Obsolete("use overloaded method", True)> _
    Public Function AddPanel(ByVal PanelText As String, ByVal Position As Integer, ByVal PanelWidth As Integer, ByVal AutoSize As System.Windows.Forms.StatusBarPanelAutoSize) As System.Windows.Forms.StatusBarPanel Implements MapWindow.Interfaces.StatusBar.AddPanel
        ' it's not possible to return StausBarPanel as ToolStripStatusLabels are used
        Return Nothing
    End Function

    Public ReadOnly Property NumPanels() As Integer Implements MapWindow.Interfaces.StatusBar.NumPanels
        Get
            Return StatusBar1.Items.Count
        End Get
    End Property

    <System.Obsolete("No resizing is needed ever more")> _
    Public Sub ResizeProgressBar() Implements Interfaces.StatusBar.ResizeProgressBar
        ' do nothing
    End Sub
#End Region

#Region "Projections"
    ''' <summary>
    ''' Handles mouse clicks on the projections button
    ''' </summary>
    Private Sub StatusBarPanelProjection_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarPanelProjection.ButtonClick
        If modMain.frmMain.Project.GeoProjection.IsEmpty Then
            Me.ChooseProjection()
        Else
            Me.ShowProperties()
        End If
    End Sub

    ''' <summary>
    ''' Shows projection selection dialog
    ''' </summary>
    Private Sub ChooseProjection()
        modMain.frmMain.m_Project.SetProjectProjectionByDialog()
    End Sub

    ''' <summary>
    ''' Shows properties for the selected projection
    ''' </summary>
    Private Sub ShowProperties()
        ' there is projection, let's see its properties
        Dim cs As CoordinateSystem = modMain.ProjectionDB.GetCoordinateSystem(modMain.frmMain.Project.GeoProjection, ProjectionSearchType.UseDialects)
        If Not cs Is Nothing Then
            Dim form As New frmProjectionProperties(cs, CType(frmMain.ProjectionDatabase, MapWindow.Controls.Projections.ProjectionDatabase))
            form.ShowDialog()
            form.Dispose()
        Else
            Dim proj As MapWinGIS.GeoProjection = modMain.frmMain.Project.GeoProjection
            If Not proj.IsEmpty Then
                Dim form As New frmProjectionProperties(proj)
                form.ShowDialog()
                form.Dispose()
            Else
                MessageBox.Show("There is no projection to show information about", modMain.frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handling context menu
    ''' </summary>
    Private Sub StatusBarPanelProjection_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles StatusBarPanelProjection.DropDownItemClicked
        Select Case e.ClickedItem.Name
            Case btnChoose.Name
                Me.ChooseProjection()
            Case btnProperties.Name
                Me.ShowProperties()
            Case btnShowWarnings.Name
                btnShowWarnings.Checked = Not btnShowWarnings.Checked
                modMain.AppInfo.NeverShowProjectionDialog = Not btnShowWarnings.Checked
            Case btnShowReport.Name
                btnShowReport.Checked = Not btnShowReport.Checked
                modMain.AppInfo.ShowLoadingReport = btnShowReport.Checked
        End Select
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        btnShowWarnings.Checked = Not modMain.AppInfo.NeverShowProjectionDialog
        btnShowReport.Checked = modMain.AppInfo.ShowLoadingReport
        Me.SetAbsenceBehavior(modMain.AppInfo.ProjectionAbsenceBehavior)
        Me.SetMismatchBehavior(modMain.AppInfo.ProjectionMismatchBehavior)
    End Sub

    Private Sub SetAbsenceBehavior(ByVal behavior As ProjectionAbsenceBehavior)
        btnAbsenceAssign.Checked = (behavior = ProjectionAbsenceBehavior.AssignFromProject)
        btnAbsenceIgnore.Checked = (behavior = ProjectionAbsenceBehavior.IgnoreAbsence)
        btnAbsenceSkip.Checked = (behavior = ProjectionAbsenceBehavior.SkipFile)
    End Sub

    Private Sub SetMismatchBehavior(ByVal behavior As ProjectionMismatchBehavior)
        btnMismatchIgnore.Checked = (behavior = ProjectionMismatchBehavior.IgnoreMismatch)
        btnMismatchProjectOld.Checked = (behavior = ProjectionMismatchBehavior.Reproject)
        btnMismatchSkip.Checked = (behavior = ProjectionMismatchBehavior.SkipFile)
    End Sub

    Private Sub btnAbsenceBehavior_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles btnAbsenceBehavior.DropDownItemClicked
        Select Case e.ClickedItem.Name
            Case btnAbsenceAssign.Name
                modMain.AppInfo.ProjectionAbsenceBehavior = ProjectionAbsenceBehavior.AssignFromProject
                Me.SetAbsenceBehavior(modMain.AppInfo.ProjectionAbsenceBehavior)
            Case btnAbsenceIgnore.Name
                modMain.AppInfo.ProjectionAbsenceBehavior = ProjectionAbsenceBehavior.IgnoreAbsence
                Me.SetAbsenceBehavior(modMain.AppInfo.ProjectionAbsenceBehavior)
            Case btnAbsenceSkip.Name
                modMain.AppInfo.ProjectionAbsenceBehavior = ProjectionAbsenceBehavior.SkipFile
                Me.SetAbsenceBehavior(modMain.AppInfo.ProjectionAbsenceBehavior)
        End Select
    End Sub

    Private Sub btnMismatchIgnore_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles btnMismatchBehavior.DropDownItemClicked
        Select Case e.ClickedItem.Name
            Case btnMismatchBehavior.Name
                modMain.AppInfo.ProjectionMismatchBehavior = ProjectionMismatchBehavior.IgnoreMismatch
                Me.SetMismatchBehavior(modMain.AppInfo.ProjectionMismatchBehavior)
            Case btnMismatchProjectOld.Name
                modMain.AppInfo.ProjectionMismatchBehavior = ProjectionMismatchBehavior.Reproject
                Me.SetMismatchBehavior(modMain.AppInfo.ProjectionMismatchBehavior)
            Case btnMismatchSkip.Name
                modMain.AppInfo.ProjectionMismatchBehavior = ProjectionMismatchBehavior.SkipFile
                Me.SetMismatchBehavior(modMain.AppInfo.ProjectionMismatchBehavior)
        End Select
    End Sub
#End Region

    ''' <summary>
    ''' Show message related to progress bar task
    ''' </summary>
    Friend Sub ShowMessage(ByVal message As String)
        Me.StatusBarPanelStatus.Text = message
    End Sub

    
End Class


