#Region " Windows Form Designer generated code "
Partial Friend Class MapWindowForm
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents StripDocker As ToolStripContainer
    Friend WithEvents mnuLegend As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTableEditorLaunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuBreak1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuBreak2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuLabelSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuRelabel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbStandard As ToolStripExtensions.ToolStripEx
    Friend WithEvents tbbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuZoom As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem30 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuZoomPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomPreviewMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomMax As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomLayer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuZoomShape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLayerButton As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MapPreview As AxMapWinGIS.AxMap
    Friend WithEvents Legend As LegendControl.Legend
    Friend WithEvents MapMain As AxMapWinGIS.AxMap
    Friend WithEvents mnuBtnAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tmrMenuTips As System.Windows.Forms.Timer
    Friend WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents tlbMain As MapWindow.ToolStripExtensions.ToolStripEx
    Friend WithEvents tbbPan As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbZoom As MapWindow.ToolStripExtensions.ToolStripEx
    Friend WithEvents tbbZoomIn As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomOut As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomExtent As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomSelected As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbZoomLayer As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbLayers As MapWindow.ToolStripExtensions.ToolStripEx
    Friend WithEvents tbbAddLayer As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbRemoveLayer As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbClearLayers As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbSymbologyManager As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextToolstrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToggleTextLabelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MapWindowForm))
        Me.StripDocker = New System.Windows.Forms.ToolStripContainer()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.tlbLayers = New MapWindow.ToolStripExtensions.ToolStripEx()
        Me.ContextToolstrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToggleTextLabelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilsToolbar = New System.Windows.Forms.ImageList(Me.components)
        Me.tbbAddLayer = New System.Windows.Forms.ToolStripButton()
        Me.tbbRemoveLayer = New System.Windows.Forms.ToolStripButton()
        Me.tbbClearLayers = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbbSymbologyManager = New System.Windows.Forms.ToolStripButton()
        Me.tbbLayerProperties = New System.Windows.Forms.ToolStripButton()
        Me.tlbStandard = New MapWindow.ToolStripExtensions.ToolStripEx()
        Me.tbbNew = New System.Windows.Forms.ToolStripButton()
        Me.tbbOpen = New System.Windows.Forms.ToolStripButton()
        Me.tbbSave = New System.Windows.Forms.ToolStripButton()
        Me.tbbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tbbProjectSettings = New System.Windows.Forms.ToolStripButton()
        Me.tlbZoom = New MapWindow.ToolStripExtensions.ToolStripEx()
        Me.tbbPan = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomIn = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomOut = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomExtent = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomSelected = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomNext = New System.Windows.Forms.ToolStripButton()
        Me.tbbZoomLayer = New System.Windows.Forms.ToolStripButton()
        Me.tbbAddPoint = New System.Windows.Forms.ToolStripButton()
        Me.tbbQueryPoint = New System.Windows.Forms.ToolStripButton()
        Me.tbbExportData = New System.Windows.Forms.ToolStripButton()
        Me.tlbMain = New MapWindow.ToolStripExtensions.ToolStripEx()
        Me.tbbSelect = New System.Windows.Forms.ToolStripButton()
        Me.tbbDeSelectLayer = New System.Windows.Forms.ToolStripButton()
        Me.mnuLegend = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuBreak1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuRelabel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuLabelSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuCharts = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLegendShapefileCategories = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTableEditorLaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveAsLayerFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuBreak2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLayerButton = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuBtnAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoom = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuZoomPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoomNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuZoomPreviewMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoomMax = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoomLayer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoomSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZoomShape = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapPreview = New AxMapWinGIS.AxMap()
        Me.MapMain = New AxMapWinGIS.AxMap()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrMenuTips = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New MapWindow.ToolStripExtensions.MenuStripEx()
        Me.StripDocker.ContentPanel.SuspendLayout()
        Me.StripDocker.TopToolStripPanel.SuspendLayout()
        Me.StripDocker.SuspendLayout()
        Me.tlbLayers.SuspendLayout()
        Me.ContextToolstrip.SuspendLayout()
        Me.tlbStandard.SuspendLayout()
        Me.tlbZoom.SuspendLayout()
        Me.tlbMain.SuspendLayout()
        Me.mnuLegend.SuspendLayout()
        Me.mnuLayerButton.SuspendLayout()
        Me.mnuZoom.SuspendLayout()
        CType(Me.MapPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MapMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StripDocker
        '
        '
        'StripDocker.ContentPanel
        '
        Me.StripDocker.ContentPanel.Controls.Add(Me.panel1)
        resources.ApplyResources(Me.StripDocker.ContentPanel, "StripDocker.ContentPanel")
        resources.ApplyResources(Me.StripDocker, "StripDocker")
        Me.StripDocker.Name = "StripDocker"
        '
        'StripDocker.TopToolStripPanel
        '
        resources.ApplyResources(Me.StripDocker.TopToolStripPanel, "StripDocker.TopToolStripPanel")
        Me.StripDocker.TopToolStripPanel.Controls.Add(Me.tlbLayers)
        Me.StripDocker.TopToolStripPanel.Controls.Add(Me.tlbStandard)
        Me.StripDocker.TopToolStripPanel.Controls.Add(Me.tlbMain)
        Me.StripDocker.TopToolStripPanel.Controls.Add(Me.tlbZoom)
        '
        'panel1
        '
        resources.ApplyResources(Me.panel1, "panel1")
        Me.panel1.Name = "panel1"
        '
        'tlbLayers
        '
        Me.tlbLayers.AllowItemReorder = True
        Me.tlbLayers.ClickThrough = True
        Me.tlbLayers.ContextMenuStrip = Me.ContextToolstrip
        resources.ApplyResources(Me.tlbLayers, "tlbLayers")
        Me.tlbLayers.ImageList = Me.ilsToolbar
        Me.tlbLayers.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tlbLayers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbbAddLayer, Me.tbbRemoveLayer, Me.tbbClearLayers, Me.ToolStripSeparator1, Me.tbbSymbologyManager, Me.tbbLayerProperties})
        Me.tlbLayers.Name = "tlbLayers"
        Me.tlbLayers.SuppressHighlighting = True
        '
        'ContextToolstrip
        '
        Me.ContextToolstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToggleTextLabelsToolStripMenuItem})
        Me.ContextToolstrip.Name = "ContextToolstrip"
        resources.ApplyResources(Me.ContextToolstrip, "ContextToolstrip")
        '
        'ToggleTextLabelsToolStripMenuItem
        '
        Me.ToggleTextLabelsToolStripMenuItem.Name = "ToggleTextLabelsToolStripMenuItem"
        resources.ApplyResources(Me.ToggleTextLabelsToolStripMenuItem, "ToggleTextLabelsToolStripMenuItem")
        '
        'ilsToolbar
        '
        Me.ilsToolbar.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        resources.ApplyResources(Me.ilsToolbar, "ilsToolbar")
        Me.ilsToolbar.Tag = "Can be removed"
        Me.ilsToolbar.TransparentColor = System.Drawing.Color.Transparent
        '
        'tbbAddLayer
        '
        resources.ApplyResources(Me.tbbAddLayer, "tbbAddLayer")
        Me.tbbAddLayer.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbAddLayer.Image = Global.MapWindow.GlobalResource.layer_add
        Me.tbbAddLayer.Name = "tbbAddLayer"
        '
        'tbbRemoveLayer
        '
        Me.tbbRemoveLayer.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbRemoveLayer.Image = Global.MapWindow.GlobalResource.layer_remove
        Me.tbbRemoveLayer.Name = "tbbRemoveLayer"
        resources.ApplyResources(Me.tbbRemoveLayer, "tbbRemoveLayer")
        '
        'tbbClearLayers
        '
        Me.tbbClearLayers.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbClearLayers.Image = Global.MapWindow.GlobalResource.remove_all_layers
        Me.tbbClearLayers.Name = "tbbClearLayers"
        resources.ApplyResources(Me.tbbClearLayers, "tbbClearLayers")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'tbbSymbologyManager
        '
        Me.tbbSymbologyManager.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbSymbologyManager.Image = Global.MapWindow.GlobalResource.layer_symbology
        Me.tbbSymbologyManager.Name = "tbbSymbologyManager"
        resources.ApplyResources(Me.tbbSymbologyManager, "tbbSymbologyManager")
        '
        'tbbLayerProperties
        '
        Me.tbbLayerProperties.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbLayerProperties.Image = Global.MapWindow.GlobalResource.layer_properties
        Me.tbbLayerProperties.Name = "tbbLayerProperties"
        resources.ApplyResources(Me.tbbLayerProperties, "tbbLayerProperties")
        '
        'tlbStandard
        '
        Me.tlbStandard.AllowItemReorder = True
        Me.tlbStandard.AllowMerge = False
        Me.tlbStandard.ClickThrough = True
        Me.tlbStandard.ContextMenuStrip = Me.ContextToolstrip
        resources.ApplyResources(Me.tlbStandard, "tlbStandard")
        Me.tlbStandard.ImageList = Me.ilsToolbar
        Me.tlbStandard.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tlbStandard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbbNew, Me.tbbOpen, Me.tbbSave, Me.tbbPrint, Me.tbbProjectSettings})
        Me.tlbStandard.Name = "tlbStandard"
        Me.tlbStandard.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.tlbStandard.SuppressHighlighting = True
        '
        'tbbNew
        '
        resources.ApplyResources(Me.tbbNew, "tbbNew")
        Me.tbbNew.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbNew.Image = Global.MapWindow.GlobalResource.project
        Me.tbbNew.Name = "tbbNew"
        '
        'tbbOpen
        '
        Me.tbbOpen.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbOpen.Image = Global.MapWindow.GlobalResource.open
        Me.tbbOpen.Name = "tbbOpen"
        resources.ApplyResources(Me.tbbOpen, "tbbOpen")
        '
        'tbbSave
        '
        Me.tbbSave.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbSave.Image = Global.MapWindow.GlobalResource.saveNew
        Me.tbbSave.Name = "tbbSave"
        resources.ApplyResources(Me.tbbSave, "tbbSave")
        '
        'tbbPrint
        '
        Me.tbbPrint.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbPrint.Image = Global.MapWindow.GlobalResource.print
        Me.tbbPrint.Name = "tbbPrint"
        resources.ApplyResources(Me.tbbPrint, "tbbPrint")
        '
        'tbbProjectSettings
        '
        Me.tbbProjectSettings.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbProjectSettings.Image = Global.MapWindow.GlobalResource.project_settings
        Me.tbbProjectSettings.Name = "tbbProjectSettings"
        resources.ApplyResources(Me.tbbProjectSettings, "tbbProjectSettings")
        '
        'tlbZoom
        '
        Me.tlbZoom.AllowItemReorder = True
        Me.tlbZoom.ClickThrough = True
        Me.tlbZoom.ContextMenuStrip = Me.ContextToolstrip
        resources.ApplyResources(Me.tlbZoom, "tlbZoom")
        Me.tlbZoom.ImageList = Me.ilsToolbar
        Me.tlbZoom.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tlbZoom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbbPan, Me.tbbZoomIn, Me.tbbZoomOut, Me.tbbZoomExtent, Me.tbbZoomSelected, Me.tbbZoomPrevious, Me.tbbZoomNext, Me.tbbZoomLayer, Me.tbbAddPoint, Me.tbbQueryPoint, Me.tbbExportData})
        Me.tlbZoom.Name = "tlbZoom"
        Me.tlbZoom.SuppressHighlighting = True
        '
        'tbbPan
        '
        Me.tbbPan.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbPan.Image = Global.MapWindow.GlobalResource.pan
        Me.tbbPan.Name = "tbbPan"
        resources.ApplyResources(Me.tbbPan, "tbbPan")
        '
        'tbbZoomIn
        '
        resources.ApplyResources(Me.tbbZoomIn, "tbbZoomIn")
        Me.tbbZoomIn.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomIn.Name = "tbbZoomIn"
        '
        'tbbZoomOut
        '
        Me.tbbZoomOut.ForeColor = System.Drawing.Color.DarkBlue
        resources.ApplyResources(Me.tbbZoomOut, "tbbZoomOut")
        Me.tbbZoomOut.Name = "tbbZoomOut"
        '
        'tbbZoomExtent
        '
        Me.tbbZoomExtent.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomExtent.Image = Global.MapWindow.GlobalResource.zoom_extentNew
        Me.tbbZoomExtent.Name = "tbbZoomExtent"
        resources.ApplyResources(Me.tbbZoomExtent, "tbbZoomExtent")
        '
        'tbbZoomSelected
        '
        Me.tbbZoomSelected.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomSelected.Image = Global.MapWindow.GlobalResource.zoom_selectionNew
        Me.tbbZoomSelected.Name = "tbbZoomSelected"
        resources.ApplyResources(Me.tbbZoomSelected, "tbbZoomSelected")
        '
        'tbbZoomPrevious
        '
        Me.tbbZoomPrevious.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomPrevious.Image = Global.MapWindow.GlobalResource.zoom_lastNew
        Me.tbbZoomPrevious.Name = "tbbZoomPrevious"
        resources.ApplyResources(Me.tbbZoomPrevious, "tbbZoomPrevious")
        '
        'tbbZoomNext
        '
        Me.tbbZoomNext.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomNext.Image = Global.MapWindow.GlobalResource.zoom_nextNew
        Me.tbbZoomNext.Name = "tbbZoomNext"
        resources.ApplyResources(Me.tbbZoomNext, "tbbZoomNext")
        '
        'tbbZoomLayer
        '
        Me.tbbZoomLayer.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbZoomLayer.Image = Global.MapWindow.GlobalResource.zoom_layerNew
        Me.tbbZoomLayer.Name = "tbbZoomLayer"
        resources.ApplyResources(Me.tbbZoomLayer, "tbbZoomLayer")
        '
        'tbbAddPoint
        '
        Me.tbbAddPoint.CheckOnClick = True
        Me.tbbAddPoint.ForeColor = System.Drawing.Color.DarkBlue
        resources.ApplyResources(Me.tbbAddPoint, "tbbAddPoint")
        Me.tbbAddPoint.Name = "tbbAddPoint"
        '
        'tbbQueryPoint
        '
        Me.tbbQueryPoint.CheckOnClick = True
        Me.tbbQueryPoint.ForeColor = System.Drawing.Color.DarkBlue
        resources.ApplyResources(Me.tbbQueryPoint, "tbbQueryPoint")
        Me.tbbQueryPoint.Name = "tbbQueryPoint"
        '
        'tbbExportData
        '
        Me.tbbExportData.ForeColor = System.Drawing.Color.DarkBlue
        resources.ApplyResources(Me.tbbExportData, "tbbExportData")
        Me.tbbExportData.Name = "tbbExportData"
        '
        'tlbMain
        '
        Me.tlbMain.AllowItemReorder = True
        Me.tlbMain.ClickThrough = True
        Me.tlbMain.ContextMenuStrip = Me.ContextToolstrip
        resources.ApplyResources(Me.tlbMain, "tlbMain")
        Me.tlbMain.ImageList = Me.ilsToolbar
        Me.tlbMain.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tlbMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbbSelect, Me.tbbDeSelectLayer})
        Me.tlbMain.Name = "tlbMain"
        Me.tlbMain.SuppressHighlighting = True
        '
        'tbbSelect
        '
        Me.tbbSelect.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbSelect.Image = Global.MapWindow.GlobalResource.selectNew
        Me.tbbSelect.Name = "tbbSelect"
        resources.ApplyResources(Me.tbbSelect, "tbbSelect")
        '
        'tbbDeSelectLayer
        '
        Me.tbbDeSelectLayer.ForeColor = System.Drawing.Color.DarkBlue
        Me.tbbDeSelectLayer.Image = Global.MapWindow.GlobalResource.deselect
        Me.tbbDeSelectLayer.Name = "tbbDeSelectLayer"
        resources.ApplyResources(Me.tbbDeSelectLayer, "tbbDeSelectLayer")
        '
        'mnuLegend
        '
        Me.mnuLegend.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripMenuBreak1, Me.ToolStripMenuRelabel, Me.ToolStripMenuLabelSetup, Me.ToolStripMenuCharts, Me.ToolStripMenuItem8, Me.mnuLegendShapefileCategories, Me.mnuTableEditorLaunch, Me.mnuSaveAsLayerFile, Me.ToolStripMenuItem9, Me.ToolStripMenuItem11, Me.ToolStripMenuItem12, Me.ToolStripMenuItem13, Me.ToolStripMenuItem14, Me.ToolStripMenuBreak2, Me.ToolStripMenuItem16})
        Me.mnuLegend.Name = "mnuLegend"
        resources.ApplyResources(Me.mnuLegend, "mnuLegend")
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.MapWindow.GlobalResource.imgGroupAdd
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        resources.ApplyResources(Me.ToolStripMenuItem2, "ToolStripMenuItem2")
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Image = Global.MapWindow.GlobalResource.layer_add
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        resources.ApplyResources(Me.ToolStripMenuItem3, "ToolStripMenuItem3")
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Image = Global.MapWindow.GlobalResource.layer_remove
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        resources.ApplyResources(Me.ToolStripMenuItem4, "ToolStripMenuItem4")
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Image = Global.MapWindow.GlobalResource.remove_all_layers
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        resources.ApplyResources(Me.ToolStripMenuItem5, "ToolStripMenuItem5")
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.MapWindow.GlobalResource.zoom_layerNew
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        resources.ApplyResources(Me.ToolStripMenuItem6, "ToolStripMenuItem6")
        '
        'ToolStripMenuBreak1
        '
        Me.ToolStripMenuBreak1.Name = "ToolStripMenuBreak1"
        resources.ApplyResources(Me.ToolStripMenuBreak1, "ToolStripMenuBreak1")
        '
        'ToolStripMenuRelabel
        '
        Me.ToolStripMenuRelabel.Image = Global.MapWindow.GlobalResource.label_reload
        Me.ToolStripMenuRelabel.Name = "ToolStripMenuRelabel"
        resources.ApplyResources(Me.ToolStripMenuRelabel, "ToolStripMenuRelabel")
        '
        'ToolStripMenuLabelSetup
        '
        Me.ToolStripMenuLabelSetup.Image = Global.MapWindow.GlobalResource.label_properties
        Me.ToolStripMenuLabelSetup.Name = "ToolStripMenuLabelSetup"
        resources.ApplyResources(Me.ToolStripMenuLabelSetup, "ToolStripMenuLabelSetup")
        '
        'ToolStripMenuCharts
        '
        Me.ToolStripMenuCharts.Image = Global.MapWindow.GlobalResource.charts_properties
        Me.ToolStripMenuCharts.Name = "ToolStripMenuCharts"
        resources.ApplyResources(Me.ToolStripMenuCharts, "ToolStripMenuCharts")
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Image = Global.MapWindow.GlobalResource.imgMetadata
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        resources.ApplyResources(Me.ToolStripMenuItem8, "ToolStripMenuItem8")
        '
        'mnuLegendShapefileCategories
        '
        Me.mnuLegendShapefileCategories.Image = Global.MapWindow.GlobalResource.layer_categories16
        Me.mnuLegendShapefileCategories.Name = "mnuLegendShapefileCategories"
        resources.ApplyResources(Me.mnuLegendShapefileCategories, "mnuLegendShapefileCategories")
        '
        'mnuTableEditorLaunch
        '
        Me.mnuTableEditorLaunch.Image = Global.MapWindow.GlobalResource.table_editor16
        Me.mnuTableEditorLaunch.Name = "mnuTableEditorLaunch"
        resources.ApplyResources(Me.mnuTableEditorLaunch, "mnuTableEditorLaunch")
        '
        'mnuSaveAsLayerFile
        '
        Me.mnuSaveAsLayerFile.Image = Global.MapWindow.GlobalResource.layer_symbology16
        Me.mnuSaveAsLayerFile.Name = "mnuSaveAsLayerFile"
        resources.ApplyResources(Me.mnuSaveAsLayerFile, "mnuSaveAsLayerFile")
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        resources.ApplyResources(Me.ToolStripMenuItem9, "ToolStripMenuItem9")
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Image = Global.MapWindow.GlobalResource.group_expand
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        resources.ApplyResources(Me.ToolStripMenuItem11, "ToolStripMenuItem11")
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Image = Global.MapWindow.GlobalResource.imgExpandAll
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        resources.ApplyResources(Me.ToolStripMenuItem12, "ToolStripMenuItem12")
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Image = Global.MapWindow.GlobalResource.group_collapse
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        resources.ApplyResources(Me.ToolStripMenuItem13, "ToolStripMenuItem13")
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Image = Global.MapWindow.GlobalResource.imgCollapseAll
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        resources.ApplyResources(Me.ToolStripMenuItem14, "ToolStripMenuItem14")
        '
        'ToolStripMenuBreak2
        '
        Me.ToolStripMenuBreak2.Name = "ToolStripMenuBreak2"
        resources.ApplyResources(Me.ToolStripMenuBreak2, "ToolStripMenuBreak2")
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.Image = Global.MapWindow.GlobalResource.layer_properties16
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        resources.ApplyResources(Me.ToolStripMenuItem16, "ToolStripMenuItem16")
        '
        'mnuLayerButton
        '
        resources.ApplyResources(Me.mnuLayerButton, "mnuLayerButton")
        Me.mnuLayerButton.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBtnAdd, Me.mnuBtnRemove, Me.mnuBtnClear})
        Me.mnuLayerButton.Name = "mnuLayerButton"
        '
        'mnuBtnAdd
        '
        Me.mnuBtnAdd.Checked = True
        Me.mnuBtnAdd.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuBtnAdd, "mnuBtnAdd")
        Me.mnuBtnAdd.Image = Global.MapWindow.GlobalResource.layer_add
        Me.mnuBtnAdd.Name = "mnuBtnAdd"
        '
        'mnuBtnRemove
        '
        resources.ApplyResources(Me.mnuBtnRemove, "mnuBtnRemove")
        Me.mnuBtnRemove.Image = Global.MapWindow.GlobalResource.layer_remove
        Me.mnuBtnRemove.Name = "mnuBtnRemove"
        '
        'mnuBtnClear
        '
        resources.ApplyResources(Me.mnuBtnClear, "mnuBtnClear")
        Me.mnuBtnClear.Image = Global.MapWindow.GlobalResource.remove_all_layers
        Me.mnuBtnClear.Name = "mnuBtnClear"
        '
        'mnuZoom
        '
        Me.mnuZoom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuZoomPrevious, Me.mnuZoomNext, Me.ToolStripMenuItem30, Me.mnuZoomPreviewMap, Me.mnuZoomMax, Me.mnuZoomLayer, Me.mnuZoomSelected, Me.mnuZoomShape})
        Me.mnuZoom.Name = "mnuZoom"
        resources.ApplyResources(Me.mnuZoom, "mnuZoom")
        '
        'mnuZoomPrevious
        '
        Me.mnuZoomPrevious.Image = Global.MapWindow.GlobalResource.zoom_lastNew
        Me.mnuZoomPrevious.Name = "mnuZoomPrevious"
        resources.ApplyResources(Me.mnuZoomPrevious, "mnuZoomPrevious")
        '
        'mnuZoomNext
        '
        Me.mnuZoomNext.Image = Global.MapWindow.GlobalResource.zoom_nextNew
        Me.mnuZoomNext.Name = "mnuZoomNext"
        resources.ApplyResources(Me.mnuZoomNext, "mnuZoomNext")
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        resources.ApplyResources(Me.ToolStripMenuItem30, "ToolStripMenuItem30")
        '
        'mnuZoomPreviewMap
        '
        resources.ApplyResources(Me.mnuZoomPreviewMap, "mnuZoomPreviewMap")
        Me.mnuZoomPreviewMap.Name = "mnuZoomPreviewMap"
        '
        'mnuZoomMax
        '
        Me.mnuZoomMax.Image = Global.MapWindow.GlobalResource.zoom_extentNew
        Me.mnuZoomMax.Name = "mnuZoomMax"
        resources.ApplyResources(Me.mnuZoomMax, "mnuZoomMax")
        '
        'mnuZoomLayer
        '
        Me.mnuZoomLayer.Checked = True
        Me.mnuZoomLayer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuZoomLayer.Image = Global.MapWindow.GlobalResource.zoom_layerNew
        Me.mnuZoomLayer.Name = "mnuZoomLayer"
        resources.ApplyResources(Me.mnuZoomLayer, "mnuZoomLayer")
        '
        'mnuZoomSelected
        '
        Me.mnuZoomSelected.Image = Global.MapWindow.GlobalResource.zoom_selectionNew
        Me.mnuZoomSelected.Name = "mnuZoomSelected"
        resources.ApplyResources(Me.mnuZoomSelected, "mnuZoomSelected")
        '
        'mnuZoomShape
        '
        Me.mnuZoomShape.Name = "mnuZoomShape"
        resources.ApplyResources(Me.mnuZoomShape, "mnuZoomShape")
        '
        'MapPreview
        '
        resources.ApplyResources(Me.MapPreview, "MapPreview")
        Me.MapPreview.Name = "MapPreview"
        Me.MapPreview.OcxState = CType(resources.GetObject("MapPreview.OcxState"), System.Windows.Forms.AxHost.State)
        '
        'MapMain
        '
        resources.ApplyResources(Me.MapMain, "MapMain")
        Me.MapMain.Name = "MapMain"
        Me.MapMain.OcxState = CType(resources.GetObject("MapMain.OcxState"), System.Windows.Forms.AxHost.State)
        '
        'tmrMenuTips
        '
        Me.tmrMenuTips.Interval = 1000
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ClickThrough = True
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.SuppressHighlighting = True
        '
        'MapWindowForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.StripDocker)
        Me.Controls.Add(Me.MenuStrip1)
        Me.KeyPreview = True
        Me.Name = "MapWindowForm"
        Me.StripDocker.ContentPanel.ResumeLayout(False)
        Me.StripDocker.TopToolStripPanel.ResumeLayout(False)
        Me.StripDocker.TopToolStripPanel.PerformLayout()
        Me.StripDocker.ResumeLayout(False)
        Me.StripDocker.PerformLayout()
        Me.tlbLayers.ResumeLayout(False)
        Me.tlbLayers.PerformLayout()
        Me.ContextToolstrip.ResumeLayout(False)
        Me.tlbStandard.ResumeLayout(False)
        Me.tlbStandard.PerformLayout()
        Me.tlbZoom.ResumeLayout(False)
        Me.tlbZoom.PerformLayout()
        Me.tlbMain.ResumeLayout(False)
        Me.tlbMain.PerformLayout()
        Me.mnuLegend.ResumeLayout(False)
        Me.mnuLayerButton.ResumeLayout(False)
        Me.mnuZoom.ResumeLayout(False)
        CType(Me.MapPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MapMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripMenuCharts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MapWindow.ToolStripExtensions.MenuStripEx
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbbProjectSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbLayerProperties As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbDeSelectLayer As System.Windows.Forms.ToolStripButton
    Private WithEvents ilsToolbar As System.Windows.Forms.ImageList
    Friend WithEvents tbbQueryPoint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbAddPoint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbExportData As System.Windows.Forms.ToolStripButton

    
End Class
#End Region

