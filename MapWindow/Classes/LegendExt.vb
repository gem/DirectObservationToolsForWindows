
'Public Class LegendExt
'    Inherits LegendControl.Legend
'    Friend WithEvents m_legendEditor As LegendEditorForm
'    Friend m_GroupHandle As Integer 'used for legend events
'    Friend m_PluginManager As PluginTracker

'#Region "Handling events"

'    Private Sub Legend_GroupDoubleClick(ByVal Handle As Integer) Handles Me.GroupDoubleClick
'        'First see if the plug-ins want this event.  
'        'If not then show the legend editor.
'        If m_PluginManager.LegendDoubleClick(Handle, Interfaces.ClickLocation.Group) = False Then
'            If m_legendEditor Is Nothing Then
'                'Make this dockable. 11/27/2006 CDM
'                m_legendEditor = LegendEditorForm.CreateAndShowGRP(Handle)
'                'm_legendEditor = New LegendEditorForm(Handle, False, Me.MapMain)
'                'Me.AddOwnedForm(m_legendEditor)
'                'm_legendEditor.Show()
'            Else
'                m_legendEditor.LoadProperties(Handle, False)
'            End If
'        End If
'    End Sub

'    Private Sub Legend_GroupExpandedChanged(ByVal Handle As Integer, ByVal Expanded As Boolean) Handles Me.GroupExpandedChanged
'        SetModified(True)
'    End Sub

'    Private Sub Legend_GroupMouseDown(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Me.GroupMouseDown
'        'Display the context menu for the legend - based on a click on a group.
'        '4/24/2005 - dpa - Fixed location display of the menu 
'        'Dim newPt As System.Drawing.Point
'        If m_PluginManager.LegendMouseDown(Handle, button, Interfaces.ClickLocation.Group) = False Then
'            If button = MouseButtons.Right Then

'                m_GroupHandle = Handle
'                ShowLayerMenu(Interfaces.ClickLocation.Group)
'            End If
'        End If
'    End Sub

'    Private Sub Legend_GroupMouseUp(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Me.GroupMouseUp
'        'This one only gets passed to the plugins
'        m_PluginManager.LegendMouseUp(Handle, CInt(IIf(button = MouseButtons.Left, vbLeftButton, vbRightButton)), Interfaces.ClickLocation.Group)
'    End Sub

'    Private Sub Legend_LayerMouseDown(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Me.LayerMouseDown
'        'Display the context menu for the legend on a right click on a layer.
'        '4/24/2005 - dpa - Fixed location display of the menu 
'        Dim Pt As New System.Drawing.Point, newPT As New System.Drawing.Point

'        Legend.SelectedLayer = Handle
'        'first see if the plugins are going to handle it.
'        If m_PluginManager.LegendMouseDown(Handle, button, Interfaces.ClickLocation.Layer) = False Then
'            If button = MouseButtons.Right Then

'                m_GroupHandle = -1
'                ShowLayerMenu(Interfaces.ClickLocation.Layer)


'                'mnuLegend.Items(2).Enabled = True
'                ''Pt = MapWinUtility.MiscUtils.GetCursorLocation()
'                ''newPT.X = CType((Pt.X - Me.Left + 5), Integer)
'                ''newPT.Y = CType((Pt.Y - Me.Top - 40), Integer)
'                'newPT.X = Legend.PointToClient(Legend.MousePosition).X
'                'newPT.Y = Legend.PointToClient(Legend.MousePosition).Y
'                'mnuLegend.Show(frmMain, newPT)
'                ''Note the duplicate calls to .Show -- seems to be required, especially when undocked

'                '' 4/05/2008 Earljon Hidalgo - display first the items before renaming the Text
'                'mnuLegend.Items(2).Visible = True
'                'mnuLegend.Items(4).Enabled = True

'                '' 4/4/2008 jk - make sure "Remove Layer" and "Zoom to Layer" is displayed correctly
'                'mnuLegend.Items(2).Text = resources.GetString("mnuRemoveLayer.Text")
'                'mnuLegend.Items(4).Text = resources.GetString("mnuZoomToLayer.Text")

'                'mnuLegend.Show(Legend, newPT)
'            End If
'        End If
'    End Sub

'    ''' <summary>
'    ''' Displaying drawing options for the layer
'    ''' </summary>
'    ''' <param name="Handle">Handle of the layer that was clicked</param>
'    ''' <remarks></remarks>
'    Private Sub Legend_LayerColorboxClicked(ByVal Handle As Integer) Handles Me.LayerColorboxClicked
'        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
'            frmMain.Plugins.BroadcastMessage("LAYER_EDIT_SYMBOLOGY" + Handle.ToString())
'        End If
'    End Sub

'    ''' <summary>
'    ''' Displaying drawing options for the shapefile category
'    ''' </summary>
'    Private Sub Legend_LayerCategoryClicked(ByVal Handle As Integer, ByVal Category As Integer) Handles Me.LayerCategoryClicked
'        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
'            frmMain.Plugins.BroadcastMessage("LAYER_EDIT_SYMBOLOGY" + Handle.ToString() + "!" + Category.ToString())
'        End If
'    End Sub

'    ''' <summary>
'    ''' Ensures closure of legend editor properties if group was removed
'    ''' </summary>
'    Private Sub Legend_GroupRemoved(ByVal Handle As Integer) Handles Me.GroupRemoved
'        If Not m_legendEditor Is Nothing AndAlso m_legendEditor.m_groupHandle > -1 Then
'            If m_legendEditor.m_groupHandle = Handle Then
'                m_legendEditor.LoadProperties(-1, True)
'            End If
'        End If
'    End Sub

'    ''' <summary>
'    ''' Displaying drawing options for charts
'    ''' </summary>
'    Private Sub Legend_LayerChartClicked(ByVal Handle As Integer) Handles Me.LayerChartClicked
'        DoChartsEdit(Handle)
'    End Sub

'    ''' <summary>
'    ''' Displaying options for chart field
'    ''' </summary>
'    Private Sub Legend_LayerChartFieldClicked(ByVal Handle As Integer, ByVal FieldIndex As Integer) Handles Me.LayerChartFieldClicked
'        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
'            frmMain.Plugins.BroadcastMessage("CHARTS_EDIT" + Handle.ToString() + "!" + FieldIndex.ToString())
'        End If
'    End Sub

'#End Region

'    ''' <summary>
'    ''' Shows the menu at certain position with a content appropriate to the selected layer
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private Sub ShowLayerMenu(ByVal Location As Interfaces.ClickLocation)

'        '    Dim pnt As New System.Drawing.Point

'        '    mnuTableEditorLaunch.Visible = (Location = ClickLocation.Layer)
'        '    ToolStripMenuLabelSetup.Visible = (Location = ClickLocation.Layer)
'        '    ToolStripMenuCharts.Visible = (Location = ClickLocation.Layer)
'        '    mnuLegendShapefileCategories.Visible = (Location = ClickLocation.Layer)
'        '    ToolStripMenuRelabel.Visible = (Location = ClickLocation.Layer)
'        '    mnuSaveAsLayerFile.Visible = (Location = ClickLocation.Layer)

'        '    If Location = ClickLocation.Group Then

'        '        mnuLegend.Items(2).Enabled = True
'        '        mnuLegend.Items(4).Enabled = True

'        '        ' what are this items?
'        '        If Legend.Groups.ItemByHandle(m_GroupHandle).LayerCount > 0 Then
'        '            mnuLegend.Items(4).Enabled = True
'        '        Else
'        '            mnuLegend.Items(4).Enabled = False
'        '            mnuLegend.Items(5).Enabled = False
'        '        End If

'        '        mnuLegend.Items(2).Text = Resources.GetString("mnuRemoveGroup.Text")
'        '        mnuLegend.Items(4).Text = Resources.GetString("mnuZoomToGroup.Text")

'        '    ElseIf Location = ClickLocation.Layer Then

'        '        mnuLegend.Items(2).Enabled = True
'        '        mnuLegend.Items(4).Enabled = True
'        '        mnuLegend.Items(2).Text = Resources.GetString("mnuRemoveLayer.Text")
'        '        mnuLegend.Items(4).Text = Resources.GetString("mnuZoomToLayer.Text")

'        '        Dim isShapefile As Boolean
'        '        isShapefile = Layers(Legend.SelectedLayer).LayerType = eLayerType.LineShapefile Or _
'        '                       Layers(Legend.SelectedLayer).LayerType = eLayerType.PointShapefile Or _
'        '                       Layers(Legend.SelectedLayer).LayerType = eLayerType.PolygonShapefile

'        '        ' shapefile specific properties
'        '        mnuTableEditorLaunch.Visible = isShapefile
'        '        ToolStripMenuLabelSetup.Visible = isShapefile
'        '        ToolStripMenuCharts.Visible = isShapefile
'        '        mnuLegendShapefileCategories.Visible = isShapefile
'        '        ToolStripMenuRelabel.Visible = isShapefile And MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology
'        '    Else
'        '        mnuLegend.Items(2).Enabled = False
'        '        mnuLegend.Items(4).Enabled = False
'        '    End If

'        '    pnt.X = Legend.PointToClient(Legend.MousePosition).X
'        '    pnt.Y = Legend.PointToClient(Legend.MousePosition).Y
'        '    mnuLegend.Show(frmMain, pnt)
'        '    mnuLegend.Show(Legend, pnt)
'        '    'Note the duplicate calls to .Show -- seems to be required, especially when undocked
'    End Sub
'End Class
