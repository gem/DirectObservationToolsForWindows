Option Strict Off

Friend Class LegendEditorForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    'Inherits System.Windows.Forms.Form
    'Made this dockable -- Chris M 11/27/2006

    Dim lyrType As Interfaces.eLayerType
    Dim lyr As MapWindow.Interfaces.Layer
    Dim obj As Object
    Dim map As AxMapWinGIS.AxMap
    Public m_groupHandle As Integer = -1
    'Public m_isLayer As Boolean = False

    Public Shared Function CreateAndShowGRP(ByVal GroupHandle As Integer) As LegendEditorForm
        Dim sz As New Size(GetSetting("MapWindow", "Legend", "w", 272), GetSetting("MapWindow", "Legend", "h", 480))
        Dim newLeg As New LegendEditorForm(GroupHandle, False, frmMain.MapMain)
        '2/18/08 LCW - referenced new version of WeifenLuo's control (version 2.2.2864) which required global replacement of WeifenLuo.WinformsUI.Docking
        'as well as following (not 100% sure this is equivalent)
        'newLeg.size = sz
        If newLeg.FloatPane IsNot Nothing Then newLeg.FloatPane.Size = sz
        frmMain.AddOwnedForm(newLeg)
        newLeg.Show(frmMain.dckPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float)
        Return newLeg
    End Function

    ''' <summary>
    ''' Creates and Shows Property Editor for a specific Layer
    ''' </summary>
    ''' <param name="LayerHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateAndShowLYR(ByVal LayerHandle As Integer) As LegendEditorForm
        Dim sz As New Size(GetSetting("MapWindow", "Legend", "w", 272), GetSetting("MapWindow", "Legend", "h", 480))
        'Christian Degrassi 2010-03-22: Part of Enhancement 1651
        Dim newLeg As New LegendEditorForm(LayerHandle, True, frmMain.MapMain)
        newLeg.Size = sz
        frmMain.AddOwnedForm(newLeg)
        newLeg.Show(frmMain.dckPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float)
        Return newLeg
    End Function

    ''' <summary>
    ''' Creates and Shows Property Editor for Current Layer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateAndShowLYR() As LegendEditorForm

        'Christian Degrassi 2010-03-22: Part of enhancement 1651
        Return CreateAndShowLYR(frmMain.Layers.CurrentLayer)

    End Function

    Public Sub New(ByVal Handle As Integer, ByVal IsLayer As Boolean, ByVal axmap As AxMapWinGIS.AxMap)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        LoadProperties(Handle, IsLayer)
        map = axmap

        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
    End Sub

    Public Sub LoadProperties(ByVal Handle As Integer, ByVal IsLayer As Boolean)
        If IsLayer Then
            m_groupHandle = -1

            lyr = frmMain.m_layers(Handle)
            If lyr Is Nothing Then
                PropertyGrid1.SelectedObject = Nothing
            Else
                lyrType = frmMain.m_layers(Handle).LayerType

                Select Case frmMain.m_layers(Handle).LayerType
                    Case Interfaces.eLayerType.Grid
                        PropertyGrid1.SelectedObject = New GridInfo(Handle)

                    Case Interfaces.eLayerType.Image
                        PropertyGrid1.SelectedObject = New ImageInfo(Handle)

                    Case Interfaces.eLayerType.LineShapefile
                        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                            PropertyGrid1.SelectedObject = New PolylineSFInfo(Handle)
                        Else
                            PropertyGrid1.SelectedObject = New ShapefileInfo(Handle)
                        End If

                    Case Interfaces.eLayerType.PointShapefile
                        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                            PropertyGrid1.SelectedObject = New PointSFInfo(Handle)
                        Else
                            PropertyGrid1.SelectedObject = New ShapefileInfo(Handle)
                        End If

                    Case Interfaces.eLayerType.PolygonShapefile
                        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                            Dim PlygonSf As New PolygonSFInfo(Handle, map)
                            PlygonSf.m_Layer = lyr
                            PropertyGrid1.SelectedObject = PlygonSf
                        Else
                            PropertyGrid1.SelectedObject = New ShapefileInfo(Handle)
                        End If
                End Select
            End If
        Else
            ' it is a group
            If Handle > -1 Then
                PropertyGrid1.SelectedObject = New GroupInfo(Handle)
            Else
                PropertyGrid1.SelectedObject = Nothing
            End If
            m_groupHandle = Handle
        End If
    End Sub

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
            End If
        End If
        'Dispose the Property Object to save the changes to disk(if necessary)
        'CDM - this below block doesn't seem necessary, since the label setup dialog
        'will have applied any changes automatically anyway. Can speed things up by removing this.
        'If TypeOf (PropertyGrid1.SelectedObject) Is System.IDisposable Then
        '    CType(PropertyGrid1.SelectedObject, IDisposable).Dispose()
        '    If TypeOf (PropertyGrid1.SelectedObject) Is PolygonSFInfo Then
        '        frmMain.m_Labels.LoadLabelInfo(CType(PropertyGrid1.SelectedObject, PolygonSFInfo).m_Layer, Nothing)
        '    End If
        'End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LegendEditorForm))
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.AccessibleDescription = Nothing
        Me.PropertyGrid1.AccessibleName = Nothing
        resources.ApplyResources(Me.PropertyGrid1, "PropertyGrid1")
        Me.PropertyGrid1.BackgroundImage = Nothing
        Me.PropertyGrid1.Font = Nothing
        Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid1.Name = "PropertyGrid1"
        '
        'LegendEditorForm
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.PropertyGrid1)
        Me.KeyPreview = True
        Me.Name = "LegendEditorForm"
        Me.TabText = "Legend Editor"
        Me.ToolTipText = Nothing
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LegendEditorForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Bugzilla 383 -- this form is already owned but still causes frmmain to
        'fall to background for some reason. Just bring it back.
        frmMain.Focus()
    End Sub

    Private Sub LegendEditorForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
                Select Case PropertyGrid1.SelectedGridItem.Label
                    Case "ColoringScheme"
                        Select Case lyrType
                            Case Interfaces.eLayerType.Grid
                                ' it doesn't make sense to not have a grid coloring scheme.  Don't allow the user to delete it completely. Just set up a single break from min to max
                                MapWinUtility.Logger.Msg("Cannot delete the coloring scheme for a grid.  Please modify values using the Coloring Scheme Editor.", "Error")

                            Case Interfaces.eLayerType.LineShapefile
                                Dim cs As MapWinGIS.ShapefileColorScheme = CType(CType(PropertyGrid1.SelectedObject, PolylineSFInfo).ColoringScheme, MapWinGIS.ShapefileColorScheme)
                                While cs.NumBreaks > 0
                                    cs.Remove(0)
                                End While
                                CType(PropertyGrid1.SelectedObject, PolylineSFInfo).ColoringScheme = cs
                                PropertyGrid1.Refresh()
                                frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).Refresh()
                                cs = Nothing
                            Case Interfaces.eLayerType.PointShapefile
                                Dim cs As MapWinGIS.ShapefileColorScheme = CType(CType(PropertyGrid1.SelectedObject, PointSFInfo).ColoringScheme, MapWinGIS.ShapefileColorScheme)
                                While cs.NumBreaks > 0
                                    cs.Remove(0)
                                End While
                                CType(PropertyGrid1.SelectedObject, PointSFInfo).ColoringScheme = cs
                                PropertyGrid1.Refresh()
                                frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).Refresh()
                                cs = Nothing
                            Case Interfaces.eLayerType.PolygonShapefile
                                Dim cs As MapWinGIS.ShapefileColorScheme = CType(CType(PropertyGrid1.SelectedObject, PolygonSFInfo).ColoringScheme, MapWinGIS.ShapefileColorScheme)
                                While cs.NumBreaks > 0
                                    cs.Remove(0)
                                End While
                                CType(PropertyGrid1.SelectedObject, PolygonSFInfo).ColoringScheme = cs
                                PropertyGrid1.Refresh()
                                frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).Refresh()
                                cs = Nothing
                        End Select

                    Case "LegendPicture"
                        Select Case lyrType
                            Case Interfaces.eLayerType.Grid
                                CType(PropertyGrid1.SelectedObject, GridInfo).LegendPicture = Nothing
                                PropertyGrid1.Refresh()
                            Case Interfaces.eLayerType.Image
                                CType(PropertyGrid1.SelectedObject, ImageInfo).LegendPicture = Nothing
                                PropertyGrid1.Refresh()
                            Case Interfaces.eLayerType.LineShapefile
                                CType(PropertyGrid1.SelectedObject, PolylineSFInfo).LegendPicture = Nothing
                                PropertyGrid1.Refresh()
                            Case Interfaces.eLayerType.PointShapefile
                                CType(PropertyGrid1.SelectedObject, PointSFInfo).LegendPicture = Nothing
                                PropertyGrid1.Refresh()
                            Case Interfaces.eLayerType.PolygonShapefile
                                CType(PropertyGrid1.SelectedObject, PolygonSFInfo).LegendPicture = Nothing
                                PropertyGrid1.Refresh()
                        End Select


                    Case "MapBitmap"
                        Select Case lyrType
                            Case Interfaces.eLayerType.PointShapefile
                                CType(PropertyGrid1.SelectedObject, PointSFInfo).MapBitmap = Nothing
                                If lyrType = Interfaces.eLayerType.PointShapefile Then
                                    lyr.PointType = MapWinGIS.tkPointType.ptSquare
                                End If
                                PropertyGrid1.Refresh()
                        End Select


                End Select
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Private Sub LegendEditorForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If (frmMain.Layers.NumLayers > 0) Then
            frmMain.SaveShapeLayerProps(frmMain.Layers.CurrentLayer)
            frmMain.Plugins.BroadcastMessage("LegendEditorClosed Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            frmMain.Legend.Refresh()
        End If
    End Sub

    Private Sub ResizeHandle(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Visible AndAlso Me.Width > 0 AndAlso Me.Height > 0 Then
            SaveSetting("MapWindow", "Legend", "w", Me.Width)
            SaveSetting("MapWindow", "Legend", "h", Me.Height)
        End If
    End Sub


End Class
