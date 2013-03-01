'********************************************************************************************************
'File Name: frmMain.vb
'Description: Main GUI interface for the MapWindow application.
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
'1/15/2005 - Streamline work to speed up loading. Created sub main entry point in modMain. (dpa)
'1/31/2005 - Total overhaul to remove DotNetBar component. (dpa)
'2/2/2005  - Clarified comments and inserted questions(jlk)
'3/16/2005 - Overhaul to add menus at run time based on a key using the same function plugins use. (dpa)
'3/23/2005 - fixed Recent Projects menu (mgray)
'6/30/2005 - cdm - Fixed recent projects menu to be able to handle a project stored in a path whose name has a space
'6/30/2005 - cdm - We now allow a plug-in to specify that it belongs in a submenu of the plugins menu via the syntax "Subcategory::Plugin Name" as the plugin name string.
'7/11/2005 - cdm - added custom window title option support
'8/20/2005 - cdm - Sort the plugins menu
'8/29/2005 - Lailin Chen - Fixed the annoying bug that prevents you from using the form designer
'8/30/2005 - Lailin Chen - Implemented the zoom drop down menu.
'9/07/2005 - Lailin Chen - Implemented the default mwZoom function.
'10/13/2005 - Paul Meems - Starting again with transferring all strings to the resourcefiles.
'9/07/2005 - Lailin Chen - Get the "Zoom to layer" and "Remove Layer" right click menu back.
'2/7/2006 - cdm - Implemented the measuring tool contributed by Jack MacDonald. Extended it
'                 to consider map units & projection, and extended it to do a mapinfo-style
'                 cumulative measure. Measuring tool now on toolbar next to select.
'6/1/2006 - Christopher Michaelis (cdm) - Massive overhaul for a new GUI. Changed lots and lots of stuff.
'6/12/2006 - cdm - Added fully dockable tool panels and menus.
'7/20/2006 - cdm - Added 'Check for Updates' functionality using
'                  the UpdateCheck tool provided by Aqua TERRA Consultants.
'7/31/2006 - Paul Meems (pm) - Translated some new strings to Dutch
'4/26/2007 - Tom Shanley (tws) - Enhanced doSaveGeoreferenced() to support hi-res exports
'6/01/2007 - Tom Shanley (tws) - show hourglass during save project - whcih can take a while now if you save shape-level formatting
'1/28/2008 - Jiri Kadlec (jk) - Changed the declaration of ResourceManager to access strings in the 
'                               new separate resource file GlobalResources.resx
'3/10/2008 - Dan Ames (dpa) - mnuOpen now also can be used to open a single layer (as well as a project)
'                           to support novice users who run the program and can't find the "Add Layer" button
'                           but instead try to open their layer with the "open" button or menu.
'3/18/2008 - jk - corrected area and distance measurement for shapefiles with lat/long coordinates
'4/05/2008 - Earljon Hidalgo - Fixed a bug when Right-clicking Properties context menu with a blank layer in DoEditProperties() function
'5/05/2008 - jk - changed the default location of MapWindowDock.config to "Application data" special folder.
'8/28/2008 - jk - fixed area and distance measurement when using alternate units - use a new conversion method
'10/1/2008 - Earljon Hidalgo (ejh) - Added icons for MapWindow UI including context menus. Icons provided by famfamfam.
'10/4/2008 - ejh - Enhancement: Added dynamic icon menu loading based on the state of plugin (enabled, disabled and/or belongs to a submenu
'6/11/2009 - Paul Meems: Fixing bug 913 & 953
'6/17/2009 - Paul Meems: Fixing bug 912, 1315, 1311, 876 & 877
'9/16/2009 - Paul Meems: Fixing bug 1412
'7/3/2010 - Christian Degrassi: Fixing bug 1318, 1635
'9/3/2010 - Christian Degrassi: Implemented new Bookmark "Add New" dialog. Issue 1638
'9/3/2010 - Christian Degrassi: Implemented "Bookmark Manager" Enhancement. Issue 1639
'22/3/2010 - Christian Degrassi: Implement Layer Type aware menu on main toolbar. Enhancement 1651
'25/05/2010 - Dave Gilmore: Added new Projection methods to use MapWinGeoProc.Projections and New Projection selection
'28-nov-2010 - sleschinski - Support for changes in the legend control. Integration with symbology plug-in
'7/14/2011 - Teva Veluppillai - Fix the issues realted to the zoom previous keyboard shortcut key - BugID 0002004
'7/28/2011 - Teva Veluppillai - Add the MapWindowVersion on the settings property form
'7/28/2011 - Teva Veluppillai - Add the floatingscale bar on the settings property form
'8/1/2011 - Teva Veluppillai - Add the show or hide the label for the redraw speed on the settings property form
'8/4/2011 - Teva Veluppillai - added Auto Create Spatial Index property
'********************************************************************************************************

Option Compare Text         'So that in text comparisons, ".mwprj" and ".MWPRJ" are equivalent
Imports System.Collections.Generic
Imports MapWindow.Classes
Imports MapWindow.Interfaces
Imports System.IO
Imports System.Globalization
Imports System.Linq
Imports MapWindow.Controls.Projections
'
Imports System.Data.Objects
Imports System.Data



Partial Friend Class MapWindowForm
    'We can't call this class frmMain because the instance of the form that we use throughout is "frmMain"
    Inherits System.Windows.Forms.Form
    Implements Interfaces.IMapWin

    Friend Class BarsProperties
        Friend CanUndock As Boolean
        Public CanDockRight As Boolean
        Public CanDockLeft As Boolean
        Public CanHide As Boolean
    End Class

#Region "Declarations"



    Friend dckPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
    Friend previewPanel As clsMWDockPanel
    Friend legendPanel As clsMWDockPanel
    Friend mapPanel As clsMWDockPanel

    Friend m_FloatingScalebar_Enabled As Boolean = False
    Private m_FloatingScalebar_PictureBox As PictureBox = Nothing
    Friend m_FloatingScalebar_ContextMenu_SelectedPosition As String = "LowerRight"
    Friend m_FloatingScalebar_ContextMenu_SelectedUnit As String = ""
    Friend m_FloatingScalebar_ContextMenu_ForeColor As System.Drawing.Color = System.Drawing.Color.Black
    Friend m_FloatingScalebar_ContextMenu_BackColor As System.Drawing.Color = System.Drawing.Color.White
    Friend WithEvents m_FloatingScalebar_ContextMenu As ContextMenu
    Friend WithEvents m_FloatingScalebar_ContextMenu_UL As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_UR As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_LL As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_LR As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_FC As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_BC As Windows.Forms.MenuItem
    Friend WithEvents m_FloatingScalebar_ContextMenu_CU As Windows.Forms.MenuItem

    Friend WithEvents m_GisToolbox As MapWindow.Controls.GisToolbox.GisToolbox = Nothing
    Friend m_txtToolHelp As RichTextBox   ' a label to display help on selected tool

    Friend Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Integer
    Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function GetDeviceCaps Lib "gdi32" (ByVal hDC As Integer, ByVal nIndex As Integer) As Integer

    Friend Const vbLeftButton As Integer = 1 'Microsoft.VisualBasic.Compatibility.VB6.MouseButtonConstants.LeftButton
    Friend Const vbRightButton As Integer = 2 'Microsoft.VisualBasic.Compatibility.VB6.MouseButtonConstants.RightButton
    Friend Const vbMiddleButton As Integer = 4 'Microsoft.VisualBasic.Compatibility.VB6.MouseButtonConstants.MiddleButton

    Friend m_ComboBoxes As New Hashtable        'Stores combo boxes that were dynamically added as controls to the toolbar area.

    ' TODO: Cho & DK 5/5/10, REMOVEME: We don't use this hash table anymore
    ' because MapWindow.LegendControl.Layer.PointImageScheme is directly accessed.
    'Public m_PointImageSchemes As New Hashtable

    Public m_FillStippleSchemes As New Hashtable
    Friend m_UserInteraction As New clsUserInteraction
    Friend m_Project As MapWindow.Project
    Friend m_layers As MapWindow.Layers
    Friend m_View As MapWindow.View
    Friend m_UIPanel As New MapWindow.clsUIPanel
    Friend m_Reports As MapWindow.Reports
    Friend m_Menu As MapWindow.Menus
    Friend m_PreviewMapContextMenuStrip As New ContextMenuStrip
    Friend m_HasBeenSaved As Boolean
    Friend m_PreviewMap As MapWindow.PreviewMap
    Friend m_LegendPanel As MapWindow.LegendPanel
    Friend m_Toolbar As MapWindow.Toolbar
    Friend m_PluginManager As PluginTracker
    Friend m_StatusBar As MapWindow.StatusBar
    Friend m_GroupHandle As Integer 'used for legend events
    Friend m_Extents As ArrayList
    Friend m_CurrentExtent As Integer
    Friend m_IsManualExtentsChange As Boolean
    Friend g_ViewBackColor As Integer
    Friend g_ColorPalettes As Xml.XmlElement
    Friend g_PreviewMapProp As BarsProperties
    Friend g_LegendProp As BarsProperties
    Friend m_Labels As LabelClass
    Friend m_AutoVis As DynamicVisibilityClass = New DynamicVisibilityClass()
    Friend WithEvents m_legendEditor As LegendEditorForm
    Friend m_HandleFileDrop As Boolean = True

    Private Const RecentProjectPrefix As String = "mnuRecentProjects_"
    Private Const BookmarkedViewPrefix As String = "mnuBookmarkedView_"

    Private m_startX, m_startY As Integer
    Private oldX, oldY As Integer

    Friend CustomWindowTitle As String = ""
    Friend Title_ShowFullProjectPath As Boolean = False

    'Jiri Kadlec 1/31/2008
    Public resources As System.Resources.ResourceManager = _
        New System.Resources.ResourceManager("MapWindow.GlobalResource", System.Reflection.Assembly.GetExecutingAssembly())
    'Private resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MapWindowForm))


    Private MapToolTipObject As New ToolTip
    Private WithEvents MapToolTipTimer As New System.Windows.Forms.Timer
    Private MapToolTipsLastMoveTime As DateTime = Now
    Friend WithEvents mnuSaveAsLayerFile As System.Windows.Forms.ToolStripMenuItem
    Private m_MapToolTipsAtLeastOneLayer As Boolean = False
    Friend WithEvents mnuLegendShapefileCategories As System.Windows.Forms.ToolStripMenuItem

    ' to avoid seeking multiple layers
    Public m_CancelledPromptToBrowse As Double = 0

    Private m_lastScale As Double = 0

    Friend m_legendTabControl As TabControl = Nothing

    'Christian Degrassi 2010-03-22: Preparing for Enhancement 1651
    'Private _LayersMenuItemFactory As LayersMenuItemFactory
#End Region

#Region "Initialization"


    ''' <summary>
    ''' Creates a new instance of MapWindowForm class
    ''' </summary>
    Public Sub New()

        ' required call
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'StatusBarPanelStatus.AutoSize = False
        'StatusBarPanelStatus.Spring = True
        'StatusBarPanelStatus.Visible = True

        'September/28/2009 Paul Meems - load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new
        m_PluginManager = New PluginTracker

        InitializeMapsAndLegends()

        ' initialieze variables
        m_Project = New Project
        m_layers = New Layers
        m_View = New View
        m_Menu = New Menus
        m_LegendPanel = New LegendPanel
        m_Toolbar = New Toolbar
        m_HasBeenSaved = True
        m_Extents = New ArrayList

        m_StatusBar = New MapWindow.StatusBar
        Me.Controls.Add(m_StatusBar.StatusBar1)
        AddHandler Me.MapMain.MouseMoveEvent, AddressOf m_StatusBar.HandleMapMouseMove
        AddHandler Me.MapMain.ExtentsChanged, AddressOf m_StatusBar.HandleExtentsChanged
        AddHandler Me.Project.ProjectionChanged, AddressOf m_StatusBar.HandleProjectionChanged

        m_PreviewMap = New PreviewMap
        m_Reports = New MapWindow.Reports
        m_UIPanel = New MapWindow.clsUIPanel
        PreviewMap.LocatorBoxColor = System.Drawing.Color.Red
        m_Labels = New LabelClass
        m_AutoVis = New DynamicVisibilityClass
        g_PreviewMapProp = New BarsProperties
        g_LegendProp = New BarsProperties
        m_CurrentExtent = -1
        m_IsManualExtentsChange = False

        MapToolTipTimer.Interval = 1000


        Me.KeyPreview = True
    End Sub

    ''' <summary>
    ''' Initialize docking panels
    ''' </summary>
    ''' 
    Public Sub InitializeMapsAndLegends()

        Me.Legend = New LegendControl.Legend
        Me.Legend.Map = CType(MapMain.GetOcx, MapWinGIS.Map)
        Me.Legend.BackColor = Color.White
        Me.Legend.Dock = DockStyle.Fill
        Me.Legend.Location = New Point(0, 50)
        Me.Legend.Name = resources.GetString("LegendName.Text")
        'TODO PM Should't this be a setting:
        Me.Legend.SelectedColor = System.Drawing.Color.FromArgb(CType(240, Byte), CType(240, Byte), CType(240, Byte))
        Me.Legend.SelectedLayer = -1

        dckPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel
        dckPanel.Parent = panel1
        dckPanel.Dock = DockStyle.Fill
        dckPanel.BringToFront()
        dckPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi

        '5/5/2008 jk - changed the default location of MapWindowDock.config
        Dim DockConfigFile As String = System.IO.Path.Combine(modMain.ProjInfo.GetApplicationDataDir(), "MapWindowDock.config")

        'Attempt to restore a default docking configuration if none is found.
        If Not System.IO.File.Exists(DockConfigFile) Then
            Dim sr As New IO.BinaryReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(Me.GetType, "MapWindowDockTemplate.config"), System.Text.Encoding.GetEncoding("UTF-16"))
            Dim sw As New IO.BinaryWriter(New IO.FileStream(DockConfigFile, IO.FileMode.Create), System.Text.Encoding.GetEncoding("UTF-16"))
            Dim buf(1024) As Char
            Dim n As Long = 1
            While n > 0
                n = sr.Read(buf, 0, 512)
                sw.Write(buf, 0, n)
            End While
            sw.Close()
            sr.Close()
        End If

        ' reading configuration of dock windows
        Dim NeedDefaultConfig As Boolean = True
        If System.IO.File.Exists(DockConfigFile) Then
            Try
                dckPanel.LoadFromXml(DockConfigFile, CType(AddressOf BuildDockContent, WeifenLuo.WinFormsUI.Docking.DeserializeDockContent))
                NeedDefaultConfig = False 'Succeeded in reading config, don't need to use defaults below
            Catch
                'corrupt config file, we will use defaults below and file should be fixed when MW exits normally
                MapWinUtility.Logger.Dbg(DockConfigFile + " is corrupted. Using default values.")
            End Try
        End If

        ' there is no saved state of dock windows
        If NeedDefaultConfig Then
            CreateMapPanel()
            CreateLegendPanel()
            CreatePreviewPanel()
        End If

        ' Setting menu according to the version of control
        If Me.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            mnuLegendShapefileCategories.Visible = False
        Else
            ToolStripMenuRelabel.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Utility function. Used to call panels intitalization when dock settings are loaded from the file
    ''' </summary>
    ''' <param name="persistString">The name of the pane</param>
    Private Function BuildDockContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.IDockContent
        Select Case persistString
            Case "mwDockPanel_Legend"
                Return CreateLegendPanel()
            Case "mwDockPanel_Preview Map"
                Return CreatePreviewPanel()
            Case "mwDockPanel_Map View"
                Return CreateMapPanel()
            Case "MapWindow.LegendEditorForm"
                Return Nothing 'We won't be able to recreate it at this point.
            Case Else
                Return Nothing
        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' Creates a map panel
    ''' </summary>
    Public Function CreateMapPanel() As WeifenLuo.WinFormsUI.Docking.DockContent
        mapPanel = New clsMWDockPanel("Map View")
        mapPanel.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        mapPanel.Controls.Add(MapMain)

        'mapPanel.Show(dckPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float)
        'mapPanel.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document
        CType(MapMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(MapMain, System.ComponentModel.ISupportInitialize).EndInit()

        MapMain.DoubleBuffer = True
        MapMain.SendMouseDown = True
        MapMain.SendMouseMove = True
        MapMain.SendMouseUp = True
        MapMain.SendSelectBoxDrag = False
        MapMain.SendSelectBoxFinal = True
        MapMain.ShowVersionNumber = True

        ' PM 10 March 2011 - Make this setting off by default:
        MapMain.ShowRedrawTime = False

        Legend.Map = CType(MapMain.GetOcx, MapWinGIS.Map)

        AddHandler mapPanel.FormClosing, AddressOf DockedPanelClosing
        Return mapPanel
    End Function

    ''' <summary>
    ''' Creates preview panel
    ''' </summary>
    Public Function CreatePreviewPanel() As WeifenLuo.WinFormsUI.Docking.DockContent

        previewPanel = New clsMWDockPanel("Preview Map")
        previewPanel.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        previewPanel.Controls.Add(MapPreview)

        CType(MapPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(MapPreview, System.ComponentModel.ISupportInitialize).EndInit()

        'Initialize preview map settings - force redraw
        MapPreview.CursorMode = MapWinGIS.tkCursorMode.cmNone
        MapPreview.MapCursor = MapWinGIS.tkCursor.crsrArrow
        MapPreview.SendMouseDown = True
        MapPreview.SendMouseMove = True
        MapPreview.SendMouseUp = True
        MapPreview.SendSelectBoxDrag = False
        MapPreview.SendSelectBoxFinal = False
        MapPreview.MouseWheelSpeed = 1.0        ' no mouse wheeling will be perfromed
        MapPreview.DoubleBuffer = True

        'CDM 1/22/06 - The preview map depends on the old-style behavior
        MapPreview.MapResizeBehavior = MapWinGIS.tkResizeBehavior.rbClassic

        '2/19/08 LCW: added icon to tab and added icon to resources
        previewPanel.Icon = New System.Drawing.Icon(Me.GetType, "MapPanel.ico")
        MapPreview.Dock = DockStyle.Fill

        ' prevents form from closing (hiding only)
        AddHandler previewPanel.FormClosing, AddressOf DockedPanelClosing
        Return previewPanel
    End Function

    ''' <summary>
    ''' Creates legend panel
    ''' </summary>
    Public Function CreateLegendPanel() As WeifenLuo.WinFormsUI.Docking.DockContent

        m_legendTabControl = New TabControl()
        m_legendTabControl.TabPages.Add(New TabPage())
        m_legendTabControl.TabPages.Add(New TabPage())
        m_legendTabControl.TabPages(0).Text = "Layers"
        m_legendTabControl.TabPages(1).Text = "Toolbox"
        m_legendTabControl.Dock = DockStyle.Fill

        ' legend
        Legend.Dock = DockStyle.Fill
        m_legendTabControl.TabPages(0).Controls.Add(Legend)

        ' gis toolbox
        m_GisToolbox = New MapWindow.Controls.GisToolbox.GisToolbox()
        m_GisToolbox.Dock = DockStyle.Fill

        m_GisToolbox.SplitterDistance = m_GisToolbox.Height * 0.8
        m_legendTabControl.TabPages(1).Controls.Add(m_GisToolbox)

        ' creating legend panel
        legendPanel = New clsMWDockPanel("Legend")
        legendPanel.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        legendPanel.Controls.Add(m_legendTabControl)

        '2/19/08 LCW: added icon to tab and added icon to resources (borrowed from MapWinInterfaces)
        legendPanel.Icon = New System.Drawing.Icon(Me.GetType, "legend.ico")

        '31 July 2011 Paul Meems: Add images to the tabs:
        'Dim imglist As New ImageList
        'imglist.Images.Add(New System.Drawing.Bitmap(Me.GetType, "LayersTab.png"))
        'imglist.Images.Add(New System.Drawing.Bitmap(Me.GetType, "ToolboxTab.png"))
        'tabs.ImageList = imglist
        'tabs.TabPages(0).ImageIndex = 0
        'tabs.TabPages(1).ImageIndex = 1

        ' prevents closing of the legend (hiding only)
        AddHandler legendPanel.FormClosing, AddressOf DockedPanelClosing
        Return legendPanel
    End Function

    ''' <summary>
    ''' Shows description for selcted tool
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <remarks></remarks>
    Private Sub GeoprocessingTreeView_ToolSelected(ByVal name As String, ByVal description As String)
        m_txtToolHelp.Clear()
        m_txtToolHelp.Text += name
        m_txtToolHelp.Text += Environment.NewLine + Environment.NewLine + description

        m_txtToolHelp.Select(0, name.Length)
        m_txtToolHelp.SelectionFont = New Font(m_txtToolHelp.Font, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Shows and hides legend panel, updates the state of the menu
    ''' </summary>
    ''' <param name="LegendVisible">The state of legend panel to be set</param>
    Public Sub UpdateLegendPanel(ByVal LegendVisible As Boolean)
        If LegendVisible Then
            legendPanel.Show()
        Else
            legendPanel.Hide()
        End If
        If Not m_Menu.Item("mnuLegendVisible") Is Nothing Then
            m_Menu.Item("mnuLegendVisible").Checked = LegendVisible
        End If
    End Sub

    ''' <summary>
    ''' Shows and hides preview panel, updates the state of the menu
    ''' </summary>
    ''' <param name="PreviewVisible">The state of the preview panel to be set</param>
    Public Sub UpdatePreviewPanel(ByVal PreviewVisible As Boolean)

        ' PM, 18 jan. 2011, Added check if previewPanel is created:
        If Not previewPanel Is Nothing Then
            If PreviewVisible Then
                previewPanel.Show()
            Else
                previewPanel.Hide()
            End If
            If Not m_Menu.Item("mnuPreviewVisible") Is Nothing Then
                m_Menu.Item("mnuPreviewVisible").Checked = PreviewVisible
            End If
        Else
            If Not m_Menu.Item("mnuPreviewVisible") Is Nothing Then
                m_Menu.Item("mnuPreviewVisible").Checked = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Closing the dock forms
    ''' </summary>
    Private Sub DockedPanelClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
        ' Paul Meems, May 30 2010
        ' Added try catch, if the menu has been deleted by a plug-in you get an error
        Try
            MapWinUtility.Logger.Dbg("sender.text: " + sender.text)
            If sender.text = "Map View" Then
                e.Cancel = True
                'Disallow closing of this.
            End If

            If sender.text = "Legend" Or sender.text = "Preview Map" Then
                ' prevents closing of the dock forms, hides them instead
                Dim w As clsMWDockPanel = CType(sender, clsMWDockPanel)
                w.Hide()
                e.Cancel = True

                If sender.text = "Legend" Then
                    m_Menu.Item("mnuLegendVisible").Checked = False
                ElseIf sender.text = "Preview Map" Then
                    m_Menu.Item("mnuPreviewVisible").Checked = False
                End If
            End If
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' Loads the saved settings of the toolstrips, like location, position and
    ''' to show text labels or not.
    ''' </summary>
    ''' <param name="toolStripContainer">The toolstrip container to use</param>
    ''' <remarks>Added by Paul Meems on April 10, 2011</remarks>
    Friend Sub LoadToolStripSettings(ByVal toolStripContainer As ToolStripContainer)

        ' Load the toolstrip settings:
        ToolStripManager.LoadSettings(Me)

        ' To hold the toolstrips we want to relocate:
        Dim toolstrips As IList(Of ToolStrip) = New List(Of ToolStrip)

        ' Get the settings the ToolStripManager skips and get the custom settings as well:
        For Each panel As Control In toolStripContainer.Controls
            If TypeOf panel Is ToolStripContentPanel Then
                ' Skip this one
                Continue For
            End If

            If Not (TypeOf panel Is ToolStripPanel) Then
                ' Move on
                Continue For
            End If

            Dim myToolstripPanel As ToolStripPanel = TryCast(panel, ToolStripPanel)
            If myToolstripPanel Is Nothing Then Continue For

            If Not myToolstripPanel.HasChildren Then
                Continue For
            End If

            For Each myControl As Control In myToolstripPanel.Controls
                If Not (TypeOf myControl Is ToolStrip) Then
                    Continue For
                End If

                Dim myToolstrip As ToolStrip = TryCast(myControl, ToolStrip)
                If myToolstrip Is Nothing Then Continue For
                If myToolstrip.Name = "MenuStrip1" Then Continue For

                Dim toolStripSettings = New ToolStripSettings(myToolstrip.Name)
                If toolStripSettings.Location.X = -1 Then
                    ' No user.config file was found

                    ' Set the default toolstrips to a default location:
                    SetToolStripsManually(toolStripContainer)
                    'Stop processing the rest:
                    Return
                End If

                SetDisplayStyle(myToolstrip, toolStripSettings.DisplayStyle)

                ' Now the toolstrips are no longer restores to the correct panel,
                ' change that as well.
                Dim toolStripPanelName As String = toolStripSettings.ToolStripPanelName
                If toolStripPanelName.EndsWith(".Top") Then
                    If Not (toolStripContainer.TopToolStripPanel.Controls.Contains(myToolstrip)) Then
                        toolStripContainer.TopToolStripPanel.Join(myToolstrip)
                    End If
                End If
                If toolStripPanelName.EndsWith(".Bottom") Then
                    If Not (toolStripContainer.BottomToolStripPanel.Controls.Contains(myToolstrip)) Then
                        toolStripContainer.BottomToolStripPanel.Join(myToolstrip)
                    End If
                End If
                If toolStripPanelName.EndsWith(".Left") Then
                    If Not (toolStripContainer.LeftToolStripPanel.Controls.Contains(myToolstrip)) Then
                        toolStripContainer.LeftToolStripPanel.Join(myToolstrip)
                    End If
                End If
                If toolStripPanelName.EndsWith(".Right") Then
                    If Not (toolStripContainer.RightToolStripPanel.Controls.Contains(myToolstrip)) Then
                        toolStripContainer.RightToolStripPanel.Join(myToolstrip)
                    End If
                End If

                ' Set the location even if it is not working properly
                ' It will create the necessary rows anyway:
                myToolstrip.Location = toolStripSettings.Location

                ' Save this control to properly set them later,
                ' fixing the issue of random placements of the toolbars:
                If toolStripSettings.ToolStripPanelName.Contains(".Top") Then
                    toolstrips.Add(myToolstrip)
                End If
            Next myControl
        Next panel

        ' The toolstrips seems to be randomly placed, try to set them right:
        RelocateToolStrips(toolstrips, toolStripContainer)

    End Sub

    Private Sub SetDisplayStyle(ByRef myToolstrip As ToolStrip, ByVal displayStyle As String)
        If String.IsNullOrEmpty(displayStyle) Then
            ' Default value
            displayStyle = "ImageAndText"
        End If

        ' Set display style for all toolstrips in all panels:
        For i As Integer = 0 To myToolstrip.Items.Count - 1
            myToolstrip.Tag = displayStyle
            myToolstrip.Items(i).DisplayStyle = If(displayStyle = "ImageAndText", ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.Image)
        Next i
    End Sub


    Private Sub RelocateToolStrips(ByVal toolStrips As IList(Of ToolStrip), ByVal toolStripContainer As ToolStripContainer)

        ' This terrible code was added by Paul Meems on May 30, 2011
        ' to try to fix the locations of the toolstrips
        ' It seems to be working

        Dim toolstripsDic As IDictionary(Of ToolStrip, Integer) = New Dictionary(Of ToolStrip, Integer)

        ' Save the controls first, with the rowID so we can order them by row:
        For Each myToolstrip As ToolStrip In toolStrips
            'Set default to the last row:
            Dim rowID As Integer = toolStripContainer.TopToolStripPanel.Rows.Length - 1
            ' Use the saved settings:
            Dim toolStripSettings = New ToolStripSettings(myToolstrip.Name)

            ' Get row ID:
            For i As Integer = 0 To toolStripContainer.TopToolStripPanel.Rows.Length - 1
                If toolStripContainer.TopToolStripPanel.Rows(i).Bounds.Y >= toolStripSettings.Location.Y Then
                    rowID = i
                    Exit For
                End If
            Next i

            Debug.WriteLine(String.Format("{0} is on row {1}", myToolstrip.Name, rowID))
            toolstripsDic.Add(myToolstrip, rowID)
        Next myToolstrip

        ' Now remove the toolstrips in the top panel (the other panels are working OK):
        For Each myToolstrip As ToolStrip In toolStrips
            toolStripContainer.TopToolStripPanel.Controls.Remove(myToolstrip)
        Next myToolstrip

        Dim firstValue As Integer = 0
        Dim currentRowID As Integer = 0
        Dim tmp As IDictionary(Of ToolStrip, Integer) = New Dictionary(Of ToolStrip, Integer)
        ' Order the dictionary by row ID:
        For Each pair As KeyValuePair(Of ToolStrip, Integer) In toolstripsDic.OrderBy(Function(m) m.Value)
            ' Because we've just removed the controls the locations have changed so use the locations from the settings file:
            Dim toolStripSettings = New ToolStripSettings(pair.Key.Name)

            ' pair.value is rowID:
            If firstValue = pair.Value Then
                ' This control is on the same row so add it to the dictionary including the X position from the settings file
                ' so we can order it later:
                tmp.Add(pair.Key, toolStripSettings.Location.X)
                Debug.WriteLine(String.Format("Adding to tmp: {0} with X is {1}", pair.Key.Name, toolStripSettings.Location.X))
            Else
                ' New row

                ' Add the toolstrips of the previous row first.
                ' Order the dictionary by X location in descending order:
                For Each keyValue As KeyValuePair(Of ToolStrip, Integer) In tmp.OrderByDescending(Function(m) m.Value)
                    ' Add the toolstrip at the beginning of the row:
                    toolStripContainer.TopToolStripPanel.Join(keyValue.Key, currentRowID)
                    Debug.WriteLine(String.Format("Adding {0} to row {1}", keyValue.Key.Name, currentRowID))
                Next keyValue

                ' Go to next row:
                currentRowID = currentRowID + 1
                tmp.Clear()
                tmp.Add(pair.Key, toolStripSettings.Location.X)
                Debug.WriteLine(String.Format("Adding to tmp: {0} with X is {1}", pair.Key.Name, toolStripSettings.Location.X))
                firstValue = pair.Value
            End If
        Next pair

        ' Add last row:
        If tmp.Count > 0 Then
            ' Order the dictionary by X location in descending order:
            For Each keyValue As KeyValuePair(Of ToolStrip, Integer) In tmp.OrderByDescending(Function(m) m.Value)
                ' Add the toolstrip at the beginning of the row:
                toolStripContainer.TopToolStripPanel.Join(keyValue.Key, currentRowID)
                Debug.WriteLine(String.Format("Adding {0} to row {1}", keyValue.Key.Name, currentRowID))
            Next keyValue
            tmp.Clear()
        End If

        ' Sometimes the displaystyle (Image or ImageAndText) is not properly set
        ' So set them again to be sure:
        For Each myToolstrip As ToolStrip In toolStrips
            ' Use the saved settings:
            Dim toolStripSettings = New ToolStripSettings(myToolstrip.Name)

            SetDisplayStyle(myToolstrip, toolStripSettings.DisplayStyle)
        Next myToolstrip

    End Sub

    Sub SetToolStripsManually(ByVal toolStripContainer As ToolStripContainer)
        ' Controls.Clear doesn't work because then other toolstrips (loaded by a plugin) are removed as well:
        toolStripContainer.TopToolStripPanel.Controls.Remove(Me.tlbMain)
        toolStripContainer.TopToolStripPanel.Controls.Remove(Me.tlbLayers)
        toolStripContainer.TopToolStripPanel.Controls.Remove(Me.tlbStandard)
        toolStripContainer.TopToolStripPanel.Controls.Remove(Me.tlbZoom)

        ' Remove the other toolstrips:
        Dim pluginsToolstrips As Stack(Of Control) = New Stack(Of Control)
        For Each ctrl As Control In toolStripContainer.TopToolStripPanel.Controls
            pluginsToolstrips.Push(ctrl)
        Next ctrl
        toolStripContainer.TopToolStripPanel.Controls.Clear()

        ' First row:
        toolStripContainer.TopToolStripPanel.Join(Me.tlbMain, 0)
        toolStripContainer.TopToolStripPanel.Join(Me.tlbLayers, 0)
        toolStripContainer.TopToolStripPanel.Join(Me.tlbStandard, 0)

        'Second row:
        ' Add toolstrips from plug-ins:
        While pluginsToolstrips.Count > 0
            toolStripContainer.TopToolStripPanel.Join(pluginsToolstrips.Pop, 1)
        End While

        ' Make sure the zoom toolstrip is first on the second row by adding it last:
        toolStripContainer.TopToolStripPanel.Join(Me.tlbZoom, 1)
    End Sub

    ''' <summary>
    ''' Save the settings of the toolstrips, like location, position and
    ''' to show text labels or not.
    ''' </summary>
    ''' <param name="toolStripContainer">The toolstrip container to use</param>
    ''' <remarks>Added by Paul Meems on April 10, 2011</remarks>
    Private Sub SaveToolStripSettings(ByVal toolStripContainer As ToolStripContainer)
        ToolStripManager.SaveSettings(Me)

        ' Save the display style as well:
        For Each panel As Control In toolStripContainer.Controls
            If TypeOf panel Is ToolStripContentPanel Then
                ' Skip this one
                Continue For
            End If

            If Not (TypeOf panel Is ToolStripPanel) Then
                ' Move on
                Continue For
            End If

            Dim myToolstripPanel As ToolStripPanel = TryCast(panel, ToolStripPanel)
            If myToolstripPanel Is Nothing Then Continue For

            If Not myToolstripPanel.HasChildren Then
                Continue For
            End If

            For Each myControl As Control In myToolstripPanel.Controls
                If Not (TypeOf myControl Is ToolStrip) Then
                    Continue For
                End If

                Dim displayStyle = "ImageAndText"
                Dim myToolstrip As ToolStrip = TryCast(myControl, ToolStrip)
                If myToolstrip Is Nothing Then Continue For
                If myToolstrip.Name = "MenuStrip1" Then Continue For

                If myToolstrip.Tag IsNot Nothing Then
                    displayStyle = myToolstrip.Tag.ToString()
                End If

                Dim toolStripSettings = New ToolStripSettings(myToolstrip.Name)
                toolStripSettings.DisplayStyle = displayStyle

                toolStripSettings.Save()
            Next
        Next
    End Sub

    ''' <summary>
    '''  Creating variables. Is called from frmMain_Load.
    ''' </summary>
    Public Sub CreateMenus()

        'Creates all of the menus
        Me.SetUpMenus()

        'First: If the appinfo help file exists, display the "Contents" help item. This is to be
        'a custom help file defined in the configuration file.
        frmMain.m_Menu("mnuContents").Visible = System.IO.File.Exists(AppInfo.HelpFilePath)

        'Second: Add the menu item for the online documentation 
        'Checking for web connection can slow down startup, so just go ahead and show the menu item
        'frmMain.m_Menu("mnuOnlineDocs").Visible = True

        ' If the offline documentation exists, add a menu item for that
        'frmMain.m_Menu("mnuOfflineDocs").Visible = (System.IO.File.Exists(BinFolder & "\OfflineDocs\index.html"))

        ' updating buttons
        UpdateButtons()

        ' save all menus and toolbars
        XmlProjectFile.SaveMainToolbarButtons()

        'Default start mode:
        tbbZoomIn.Checked = True
    End Sub

    ''' <summary>
    ''' Creating main menu
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetUpMenus()

        Dim Nil As Object = Nothing
        'Set up the File menu
        ' Renamed to Project: m_Menu.AddMenu("mnuFile", Nil, resources.GetString("mnuFile.Text"))
        m_Menu.AddMenu("mnuFile", Nil, resources.GetString("mnuProject.Text"))
        m_Menu.AddMenu("mnuNew", "mnuFile", resources.GetObject("project"), resources.GetString("mnuNew.Text"))
        m_Menu.AddMenu("mnuFileBreak0", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuOpen", "mnuFile", resources.GetObject("open"), resources.GetString("mnuOpen.Text"))
        m_Menu.AddMenu("mnuOpenProjectIntoGroup", "mnuFile", resources.GetObject("group_open"), resources.GetString("mnuOpenProjectIntoGroup.Text"))
        m_Menu.AddMenu("mnuFileBreak1", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuSave", "mnuFile", resources.GetObject("saveNew"), resources.GetString("mnuSave.Text"))
        m_Menu.AddMenu("mnuSaveAs", "mnuFile", resources.GetObject("save_as"), resources.GetString("mnuSaveAs.Text"))
        m_Menu.AddMenu("mnuFileBreak2", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuPrint", "mnuFile", resources.GetObject("print"), resources.GetString("mnuPrint.Text"))
        m_Menu.AddMenu("mnuFileBreak3", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuRecentProjects", "mnuFile", resources.GetObject("recent_maps"), resources.GetString("mnuRecentProjects.Text"))
        m_Menu.AddMenu("mnuExport", "mnuFile", resources.GetObject("map_export"), resources.GetString("mnuExport.Text"))
        m_Menu.AddMenu("mnuSaveMapImage", "mnuExport", resources.GetObject("export_map"), resources.GetString("mnuSaveMapImage.Text"))
        m_Menu.AddMenu("mnuSaveGeorefMapImage", "mnuExport", resources.GetObject("export_georeferenced"), resources.GetString("mnuSaveGeorefMapImage.Text"))
        m_Menu.AddMenu("mnuSaveScaleBar", "mnuExport", resources.GetObject("scale-bar"), resources.GetString("mnuSaveScaleBar.Text"))
        m_Menu.AddMenu("mnuSaveNorthArrow", "mnuExport", resources.GetObject("northArrow"), resources.GetString("mnuSaveNorthArrow.Text"))
        m_Menu.AddMenu("mnuSaveLegend", "mnuExport", resources.GetObject("legend"), resources.GetString("mnuSaveLegend.Text"))
        m_Menu.AddMenu("mnuFileBreak4", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuProjectSettings", "mnuFile", resources.GetObject("project-settings"), resources.GetString("mnuProjectSettings.Text"))
        'm_Menu.AddMenu("mnuCheckForUpdates", "mnuFile", resources.GetObject("imgUpdate"), resources.GetString("mnuCheckUpdates.Text"))
        m_Menu.AddMenu("mnuFileBreak5", "mnuFile", Nil, "-")
        m_Menu.AddMenu("mnuExit", "mnuFile", resources.GetObject("quit"), resources.GetString("mnuExit.Text"))

        'Set up the Edit menu
        m_PreviewMapContextMenuStrip.Items.Add(resources.GetString("mnuUpdatePreviewFull.Text"), Nothing, New System.EventHandler(AddressOf PreviewMapContextMenuStrip_UpdatePreviewFull))
        m_PreviewMapContextMenuStrip.Items.Add(resources.GetString("mnuUpdatePreviewCurr.Text"), Nothing, New System.EventHandler(AddressOf PreviewMapContextMenuStrip_UpdatePreview))
        m_Menu.AddMenu("mnuUpdatePreviewCurr", "mnuPreview", Nil, resources.GetString("mnuUpdatePreviewCurr.Text"))

        m_PreviewMapContextMenuStrip.Items.Add(resources.GetString("mnuClearPreview.Text"), Nothing, New System.EventHandler(AddressOf PreviewMapContextMenuStrip_ClearPreview))

        ' LAYER MENU
        m_Menu.AddMenu("mnuLayer", Nil, resources.GetString("mnuLayer.Text"))
        m_Menu.AddMenu("mnuAddLayer", "mnuLayer", resources.GetObject("layer-add"), resources.GetString("mnuAddLayer.Text"))
        m_Menu.AddMenu("mnuAddECWPLayer", "mnuLayer", resources.GetObject("layer_add_ecwp"), resources.GetString("mnuAddECWPLayer.Text"))  'OK
        m_Menu.AddMenu("mnuRemoveLayer", "mnuLayer", resources.GetObject("layer-remove"), resources.GetString("mnuRemoveLayer.Text"))
        m_Menu.AddMenu("mnuClearLayer", "mnuLayer", resources.GetObject("remove-all-layers"), resources.GetString("mnuClearLayer.Text"))
        m_Menu.AddMenu("mnuLayerBreak1", "mnuLayer", Nil, "-")
        m_Menu.AddMenu("mnuLayerLabels", "mnuLayer", resources.GetObject("label_properties"), resources.GetString("mnuLayerLabels.Text")) 'OK
        m_Menu.AddMenu("mnuLayerCharts", "mnuLayer", resources.GetObject("charts_properties"), resources.GetString("mnuLayerCharts.Text"))
        m_Menu.AddMenu("mnuLayerAttributeTable", "mnuLayer", resources.GetObject("table_editor"), resources.GetString("mnuLayerAttributeTable.Text"))
        m_Menu.AddMenu("mnuLayerCategories", "mnuLayer", resources.GetObject("layer_categories"), resources.GetString("mnuLayerCategories.Text"))
        m_Menu.AddMenu("mnuOptionsManager", "mnuLayer", resources.GetObject("layer-symbology"), resources.GetString("mnuOptionsManager.Text"))
        m_Menu.AddMenu("mnuLayerRelabel", "mnuLayer", resources.GetObject("label_reload"), resources.GetString("mnuLayerRelabel.Text"))
        m_Menu.AddMenu("mnuLayerBreak2", "mnuLayer", Nil, "-")
        ' selection
        m_Menu.AddMenu("mnuQueryLayer", "mnuLayer", resources.GetObject("layer_query"), resources.GetString("mnuQueryLayer.Text"))
        m_Menu.AddMenu("mnuClearSelectedShapes", "mnuLayer", resources.GetObject("deselect"), resources.GetString("mnuClearSelectedShapes.Text")).Enabled = False
        m_Menu.AddMenu("mnuLayerBreak3", "mnuLayer", Nil, "-")
        m_Menu.AddMenu("mnuLayerProperties", "mnuLayer", resources.GetObject("layer-properties"), resources.GetString("mnuLayerProperties.Text"))

        ' VIEW MENU
        m_Menu.AddMenu("mnuView", Nil, resources.GetString("mnuView.Text"))
        'Panels Menu
        m_Menu.AddMenu("mnuRestoreMenu", "mnuView", resources.GetObject("panels"), resources.GetString("mnuPanels.Text"))
        m_Menu.AddMenu("mnuLegendVisible", "mnuRestoreMenu", Nil, resources.GetString("mnuShowLegend.Text")).Checked = IIf(legendPanel Is Nothing, False, True)
        m_Menu.AddMenu("mnuPreviewVisible", "mnuRestoreMenu", Nil, resources.GetString("mnuShowPreviewMap.Text")).Checked = IIf(previewPanel Is Nothing, False, True)
        'm_Menu.AddMenu("mnuLegendEditor", "mnuRestoreMenu", Nil, resources.GetString("mnuLegendEditor.Text")).Checked = IIf(Me.m_legendEditor Is Nothing, False, True)
        m_Menu.AddMenu("mnuViewBreak2", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuSetScale", "mnuView", resources.GetObject("set_map_scale"), resources.GetString("mnuSetScale.Text"))
        m_Menu.AddMenu("mnuShowScaleBar", "mnuView", resources.GetObject("scale-bar-add"), resources.GetString("mnuShowScaleBar.Text"))
        ' Copy
        m_Menu.AddMenu("mnuViewBreak5", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuCopy", "mnuView", resources.GetObject("imgCopy"), resources.GetString("mnuCopy.Text"))
        m_Menu.AddMenu("mnuCopyMap", "mnuCopy", resources.GetObject("export_map"), resources.GetString("mnuCopyMap.Text"))
        m_Menu.AddMenu("mnuCopyLegend", "mnuCopy", resources.GetObject("legend"), resources.GetString("mnuCopyLegend.Text"))
        m_Menu.AddMenu("mnuCopyScaleBar", "mnuCopy", resources.GetObject("scale-bar"), resources.GetString("mnuCopyScaleBar.Text"))
        m_Menu.AddMenu("mnuCopyNorthArrow", "mnuCopy", resources.GetObject("northArrow"), resources.GetString("mnuCopyNorthArrow.Text"))
        m_Menu.AddMenu("mnuViewBreak3", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuZoomIn", "mnuView", resources.GetObject("zoom-inNew"), resources.GetString("mnuZoomIn.Text"))
        m_Menu.AddMenu("mnuZoomOut", "mnuView", resources.GetObject("zoom-outNew"), resources.GetString("mnuZoomOut.Text"))
        m_Menu.AddMenu("mnuZoomToFullExtents", "mnuView", resources.GetObject("zoom-extentNew"), resources.GetString("mnuZoomToFullExtents.Text"))
        'Start this one disabled
        m_Menu.AddMenu("mnuZoomToPreviewExtents", "mnuView", resources.GetObject("imgMapExtents"), resources.GetString("mnuZoomToPreviewExtents.Text")).Enabled = False
        m_Menu.AddMenu("mnuViewBreak4", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuPreviousZoom", "mnuView", resources.GetObject("zoom-lastNew"), resources.GetString("mnuPreviousZoom.Text"))
        m_Menu.AddMenu("mnuNextZoom", "mnuView", resources.GetObject("zoom-nextNew"), resources.GetString("mnuNextZoom.Text"))
        m_Menu.AddMenu("mnuViewBreak6", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuClearAllSelection", "mnuView", resources.GetObject("deselect_all"), resources.GetString("mnuClearAllSelection.Text"))
        ''Add Preview map functions
        m_Menu.AddMenu("mnuPreviewSep", "mnuView", Nil, "-")
        m_Menu.AddMenu("mnuPreview", "mnuView", resources.GetObject("imgMapPreview"), resources.GetString("mnuPreview.Text"))
        m_Menu.AddMenu("mnuUpdatePreviewFull", "mnuPreview", Nil, resources.GetString("mnuUpdatePreviewFull.Text"))
        m_Menu.AddMenu("mnuUpdatePreviewCurr", "mnuPreview", Nil, resources.GetString("mnuUpdatePreviewCurr.Text"))
        m_Menu.AddMenu("mnuClearPreview", "mnuPreview", Nil, resources.GetString("mnuClearPreview.Text"))

        'Christian Degrassi 2010-03-02: This fixes issue 1318
        m_Menu.AddMenu("mnuBookmarks", Nil, resources.GetString("mnuBookmarks.Text"))
        m_Menu.AddMenu("mnuBookmarkView", "mnuBookmarks", resources.GetObject("imgBookmarkAdd"), resources.GetString("mnuBookmarkView.Text"))
        'Christian Degrassi 2010-03-09: This menu item is obsolete due to the new "Bookmarks Manager"
        m_Menu.AddMenu("mnuBookmarkedViews", "mnuBookmarks", resources.GetObject("bookmarks_view"), resources.GetString("mnuBookmarkedViews.Text"))
        'Christian Degrassi 2010-03-02: This fixes part of issue 1364
        m_Menu.AddMenu("mnuBookmarksBreak1", "mnuBookmarks", Nil, "-")
        m_Menu.AddMenu("mnuBookmarksManager", "mnuBookmarks", resources.GetObject("bookmark_manager"), resources.GetString("mnuBookmarksManager.Text"))

        'Set up the Plug-ins menu
        m_Menu.AddMenu("mnuPlugins", Nil, resources.GetString("mnuPlugins.Text"))
        m_Menu.AddMenu("mnuEditPlugins", "mnuPlugins", resources.GetObject("imgPluginEdit"), resources.GetString("mnuEditPlugins.Text"))
        m_Menu.AddMenu("mnuScript", "mnuPlugins", resources.GetObject("imgScripts"), resources.GetString("mnuScript.Text"))
        m_Menu.AddMenu("mnuPluginsBreak1", "mnuPlugins", Nil, "-")

        'Set up the Help menu
        m_Menu.AddMenu("mnuHelp", Nil, resources.GetString("mnuHelp.Text"))
        m_Menu.AddMenu("mnuContents", "mnuHelp", Nil, resources.GetString("mnuContents.Text"))

        'Additional help menu items - cdm 12/22/2005.
        'These are hidden or displayed according to need in modMain.LoadMainForm.
        'This includes hiding offline if connection is available, etc.
        m_Menu.AddMenu("mnuDocs", "mnuHelp", resources.GetObject("documentation"), resources.GetString("mnuDocs.Text"))
        'm_Menu.AddMenu("mnuOnlineDocs", "mnuHelp", resources.GetObject("documentation"), resources.GetString("mnuOnlineDocs.Text"))
        ' m_Menu.AddMenu("mnuTutorials", "mnuHelp", resources.GetObject("tutorials"), "Getting started with MapWindow")
        'm_Menu.AddMenu("mnuBugReport", "mnuHelp", resources.GetObject("mantis"), "Report a bug")
        'm_Menu.AddMenu("mnuDonate", "mnuHelp", resources.GetObject("paypal"), "Help us continue the development: Donate!")
        'm_Menu.AddMenu("mnuOfflineDocs", "mnuHelp", resources.GetObject("imgHelp"), resources.GetString("mnuOfflineDocs.Text"))
        'm_Menu.AddMenu("mnuCheckForUpdates", "mnuHelp", resources.GetObject("imgUpdate"), resources.GetString("mnuCheckUpdates.Text"))

        m_Menu.AddMenu("mnuHelpBreak1", "mnuHelp", Nil, "-")
        m_Menu.AddMenu("mnuShortcuts", "mnuHelp", resources.GetObject("imgKeyboard"), resources.GetString("mnuShortcuts.Text"))
        m_Menu.AddMenu("mnuHelpBreak2", "mnuHelp", Nil, "-")

        m_Menu.AddMenu("mnuWelcomeScreen", "mnuHelp", resources.GetObject("welcome"), resources.GetString("mnuWelcomeScreen.Text"))
        m_Menu.AddMenu("mnuAboutMapWindow", "mnuHelp", resources.GetObject("about"), resources.GetString("mnuAboutMapWindow.Text"))

        mnuZoomPrevious.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_back_16.ico")
        mnuZoomNext.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_forward_16_2.ico")
        mnuZoomLayer.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_to_layer.ico")
        mnuZoomMax.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_full_extent.ico")
        mnuZoomSelected.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_select_16_2.ico")
        mnuZoomPreviewMap.Image = New System.Drawing.Bitmap(Me.GetType, "zoom_preview.ico")

        'Context menu for floating scale bar
        m_FloatingScalebar_ContextMenu = New ContextMenu()
        m_FloatingScalebar_ContextMenu_UL = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_UpperLeft.Text"), AddressOf FloatingScalebar_UpperLeft_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_UL)
        m_FloatingScalebar_ContextMenu_UR = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_UpperRight.Text"), AddressOf FloatingScalebar_UpperRight_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_UR)
        m_FloatingScalebar_ContextMenu_LL = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_LowerLeft.Text"), AddressOf FloatingScalebar_LowerLeft_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_LL)
        m_FloatingScalebar_ContextMenu_LR = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_LowerRight.Text"), AddressOf FloatingScalebar_LowerRight_Click)
        m_FloatingScalebar_ContextMenu_LR.Checked = True
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_LR)
        m_FloatingScalebar_ContextMenu.MenuItems.Add("-")
        m_FloatingScalebar_ContextMenu_FC = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_ChooseForecolor.Text"), AddressOf FloatingScalebar_ChooseForecolor_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_FC)
        m_FloatingScalebar_ContextMenu_BC = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_ChooseBackcolor.Text"), AddressOf FloatingScalebar_ChooseBackcolor_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_BC)
        m_FloatingScalebar_ContextMenu_CU = New Windows.Forms.MenuItem(resources.GetString("sbContextMenu_ChangeUnits.Text"), AddressOf FloatingScalebar_ChangeUnits_Click)
        m_FloatingScalebar_ContextMenu.MenuItems.Add(m_FloatingScalebar_ContextMenu_CU)
    End Sub
#End Region

#Region "Public Properties"
    ''' <summary>
    ''' Event fired when MapWindow has finised to load project file
    ''' </summary>
    Public Event ProjectLoaded(ByVal projectName As String, ByVal errors As Boolean) Implements Interfaces.IMapWin.ProjectLoaded

    ''' <summary>
    ''' Raises Project loaded event
    ''' </summary>
    Friend Sub FireProjectLoaded(ByVal projectName As String, ByVal errors As Boolean)
        RaiseEvent ProjectLoaded(projectName, errors)
    End Sub

    ''' <summary>
    ''' Event fired when the state of custom object asociated with layer was loaded from project file
    ''' </summary>
    Public Event CustomObjectLoaded As CustomObjectLoadedDelegate Implements MapWindow.Interfaces.IMapWin.CustomObjectLoaded

    ''' <summary>
    ''' Raises events CustomObjectLoaded event
    ''' </summary>
    Friend Sub FireCustomEventLoaded(ByVal layerHandle As Integer, ByVal objectKey As String, ByVal state As String, ByRef handled As Boolean)
        RaiseEvent CustomObjectLoaded(layerHandle, objectKey, state, handled)
    End Sub

    ''' <summary>
    ''' Event filred when selection of shapefile layer is changed
    ''' </summary>
    ''' <remarks></remarks>
    Public Event LayerSelectionChanged As LayerSelectionChangedDelegate Implements MapWindow.Interfaces.IMapWin.LayerSelectionChanged

    ''' <summary>
    ''' Raises events LayerSelectionChanged event
    ''' </summary>
    Friend Sub FireLayerSelectionChanged(ByVal layerHandle As Integer, ByRef handled As Boolean)
        RaiseEvent LayerSelectionChanged(layerHandle, handled)
    End Sub

    Public ReadOnly Property UserInteraction() As UserInteraction Implements Interfaces.IMapWin.UserInteraction
        Get
            Return m_UserInteraction
        End Get
    End Property

    Public Sub ClearCustomWindowTitle() Implements Interfaces.IMapWin.ClearCustomWindowTitle
        CustomWindowTitle = ""
    End Sub

    Public ReadOnly Property GetOCX() As Object Implements Interfaces.IMapWin.GetOCX
        Get
            Return MapMain
        End Get
    End Property

    Public WriteOnly Property DisplayFullProjectPath() As Boolean Implements Interfaces.IMapWin.DisplayFullProjectPath
        Set(ByVal Value As Boolean)
            Title_ShowFullProjectPath = Value
            SetModified(False) 'Force rewrite of title
        End Set
    End Property

    Public Sub SetCustomWindowTitle(ByVal NewTitleText As String) Implements Interfaces.IMapWin.SetCustomWindowTitle
        CustomWindowTitle = NewTitleText
        SetModified(False) 'Force rewrite of title
    End Sub

    Public ReadOnly Property LastError() As String Implements Interfaces.IMapWin.LastError
        Get
            Dim tStr As String

            If g_error Is Nothing Then Return ""

            tStr = tStr.Copy(g_error)
            g_error = ""
            Return tStr
        End Get
    End Property

    Public ReadOnly Property Layers() As Interfaces.Layers Implements Interfaces.IMapWin.Layers
        Get
            Return m_layers
        End Get
    End Property

    Public ReadOnly Property View() As Interfaces.View Implements Interfaces.IMapWin.View
        Get
            Return m_View
        End Get
    End Property

    Public ReadOnly Property Menus() As Interfaces.Menus Implements Interfaces.IMapWin.Menus
        Get
            Return m_Menu
        End Get
    End Property

    Public ReadOnly Property Plugins() As Interfaces.Plugins Implements Interfaces.IMapWin.Plugins
        Get
            Return m_PluginManager.oldInt
        End Get
    End Property

    Public ReadOnly Property PreviewMap() As Interfaces.PreviewMap Implements Interfaces.IMapWin.PreviewMap
        Get
            Return m_PreviewMap
        End Get
    End Property
    ' Because legendpanel and LegendPanel is the same is VB.NET ;(
    ' I added an I to indicate its implementing an Interface:
    Public ReadOnly Property LegendIPanel() As Interfaces.LegendPanel Implements Interfaces.IMapWin.LegendPanel
        Get
            Return m_LegendPanel
        End Get
    End Property

    Public ReadOnly Property StatusBar() As Interfaces.StatusBar Implements Interfaces.IMapWin.StatusBar
        Get
            Return m_StatusBar
        End Get
    End Property

    Public ReadOnly Property Toolbar() As Interfaces.Toolbar Implements Interfaces.IMapWin.Toolbar
        Get
            Return m_Toolbar
        End Get
    End Property

    Public ReadOnly Property Reports() As MapWindow.Interfaces.Reports Implements MapWindow.Interfaces.IMapWin.Reports
        Get
            Return m_Reports
        End Get
    End Property

    Public ReadOnly Property UIPanel() As MapWindow.Interfaces.UIPanel Implements MapWindow.Interfaces.IMapWin.UIPanel
        Get
            Return m_UIPanel
        End Get
    End Property

    Public ReadOnly Property Project() As MapWindow.Interfaces.Project Implements MapWindow.Interfaces.IMapWin.Project
        Get
            Return m_Project
        End Get
    End Property

#End Region

#Region "MapWindowForm Events"
    ''' <summary>
    ''' Adding shortcut keys for the application
    ''' </summary>
    Private Function HandleShortcutKeys(ByVal e As Keys) As Boolean
        'Chris Michaelis - May 2006. See BugZilla 124.

        'The following is the only reliable way to detect control AND other keys
        'using ProcessCmdKey.
        Dim keycodes As New Hashtable
        Dim keystates As New Hashtable
        keycodes.Add("control", &H11)
        keycodes.Add("shift", &H10)
        keycodes.Add("ks", &H53)
        keycodes.Add("ko", &H4F)
        keycodes.Add("kc", &H43)
        keycodes.Add("kp", &H50)
        keycodes.Add("ki", &H49)
        keycodes.Add("kh", &H48)
        keycodes.Add("kprint", &H2A)
        keycodes.Add("kf4", &H73)
        keycodes.Add("khome", &H24)
        keycodes.Add("kinsert", &H2D)
        keycodes.Add("kdelete", &H2E)
        keycodes.Add("kpageup", &H21)
        keycodes.Add("kpagedown", &H22)
        keycodes.Add("kleftarrow", &H25)
        keycodes.Add("kuparrow", &H26)
        keycodes.Add("krightarrow", &H27)
        keycodes.Add("kdownarrow", &H28)
        keycodes.Add("kplus", &HBB)
        keycodes.Add("kminus", &HBD)
        keycodes.Add("kspacebar", &H20)
        keycodes.Add("kenter", &HD)
        'Teva Veluppillai 7/14/2010
        'keycodes.Add("bs", &H8)
        keycodes.Add("kbackspace", &H8)


        Dim i As IEnumerator = keycodes.GetEnumerator()
        While i.MoveNext
            If CBool(GetAsyncKeyState(i.Current.value)) = True Then keystates.Add(i.Current.key, True)
        End While

        'Paul Meems 6/11/2009
        'Added:
        If keystates.Count = 0 Then
            Return False
        End If

        If keystates.Contains("ks") AndAlso keystates.Contains("control") Then
            DoSave()
            Return True
        ElseIf keystates.Contains("kbackspace") Then
            'Paul Meems 6/11/2009
            'Added:
            mnuZoomPrevious.Checked = True
            'Teva Veluppillai 7/14/2010
            HandleZoomButtonClick("tbbZoomPrevious")
            Return True
        ElseIf keystates.Contains("kp") AndAlso keystates.Contains("shift") AndAlso keystates.Contains("control") Then
            HandleZoomButtonClick("tbbPan")
        ElseIf keystates.Contains("ko") AndAlso keystates.Contains("shift") AndAlso keystates.Contains("control") Then
            HandleZoomButtonClick("tbbZoomOut")
        ElseIf keystates.Contains("ki") AndAlso keystates.Contains("shift") AndAlso keystates.Contains("control") Then
            HandleZoomButtonClick("tbbZoomIn")
        ElseIf keystates.Contains("ki") AndAlso keystates.Contains("control") Then
            HandleButtonClick("Identify")
        ElseIf keystates.Contains("kh") AndAlso keystates.Contains("control") Then
            HandleButtonClick("tbbSelect")
        ElseIf keystates.Contains("kenter") And keystates.Contains("control") Then
            If Not Legend.SelectedLayer = -1 Then
                If m_legendEditor Is Nothing Then
                    m_legendEditor = LegendEditorForm.CreateAndShowLYR()
                Else
                    m_legendEditor.LoadProperties(Handle, True)
                End If
            End If
        ElseIf keystates.Contains("kspacebar") And keystates.Contains("control") Then
            Legend.Layers.ItemByHandle(Legend.SelectedLayer).Visible = Not Legend.Layers.ItemByHandle(Legend.SelectedLayer).Visible
        ElseIf keystates.Contains("kuparrow") And keystates.Contains("control") Then
            Dim ar As New ArrayList()
            For z As Integer = 0 To Legend.Groups.Count - 1
                For zz As Integer = 0 To Legend.Groups(z).LayerCount - 1
                    ar.Add(Legend.Groups(z).Item(zz).Handle)
                Next
            Next

            For z As Integer = 0 To ar.Count - 1
                If Legend.SelectedLayer = ar(z) And z + 1 < ar.Count Then
                    Legend.SelectedLayer = ar(z + 1)
                    Exit For
                End If
            Next
        ElseIf keystates.Contains("kdownarrow") And keystates.Contains("control") Then
            'Legend selection shift
            Dim ar As New ArrayList()
            For z As Integer = 0 To Legend.Groups.Count - 1
                For zz As Integer = 0 To Legend.Groups(z).LayerCount - 1
                    ar.Add(Legend.Groups(z).Item(zz).Handle)
                Next
            Next

            For z As Integer = 0 To ar.Count - 1
                If Legend.SelectedLayer = ar(z) And z - 1 > -1 Then
                    Legend.SelectedLayer = ar(z - 1)
                    Exit For
                End If
            Next

        ElseIf keystates.Contains("ko") AndAlso keystates.Contains("control") Then
            DoOpen()
            Return True
        ElseIf keystates.Contains("kc") AndAlso keystates.Contains("control") Then
            DoCopyMap()
            Return True
        ElseIf keystates.Contains("kp") AndAlso keystates.Contains("control") Then
            DoPrint()
            Return True
        ElseIf keystates.Contains("kprint") Then
            DoPrint()
            Return True
        ElseIf keystates.Contains("kf4") AndAlso keystates.Contains("control") Then
            doClose()
            Return True
        ElseIf keystates.Contains("khome") AndAlso keystates.Contains("control") Then
            If Not Legend Is Nothing Then
                If Legend.SelectedLayer <> -1 Then
                    DoZoomToLayer()
                End If
            End If
            Return True
        ElseIf keystates.Contains("kdelete") Then
            If Not Legend Is Nothing Then
                If Legend.SelectedLayer <> -1 Then
                    '7/31/2006 PM
                    'If mapwinutility.logger.msg("Are you sure you wish to remove the currently selected layer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Remove Current Layer?") = MsgBoxResult.Yes Then
                    If MapWinUtility.Logger.Msg(resources.GetString("msgHandleShortcutKeys.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.Question, AppInfo.Name) = MsgBoxResult.Yes Then
                        DoRemoveLayer()
                    End If
                End If
            End If
            Return True
        ElseIf keystates.Contains("kinsert") Then
            DoAddLayer()
            Return True
        ElseIf keystates.Contains("khome") Then
            doZoomToFullExtents()
            Return True
        ElseIf keystates.Contains("kpageup") Then
            'Pan up slightly - 50% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim ydiff As Double = (exts.yMax - exts.yMin) / 2
                exts.SetBounds(exts.xMin, exts.yMin + ydiff, exts.zMin, exts.xMax, exts.yMax + ydiff, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("kpagedown") Then
            'Pan down slightly - 50% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim ydiff As Double = (exts.yMax - exts.yMin) / 2
                exts.SetBounds(exts.xMin, exts.yMin - ydiff, exts.zMin, exts.xMax, exts.yMax - ydiff, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("kuparrow") And MapMain.Focused Then
            'Pan up slightly - 25% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim ydiff As Double = (exts.yMax - exts.yMin) / 4
                exts.SetBounds(exts.xMin, exts.yMin + ydiff, exts.zMin, exts.xMax, exts.yMax + ydiff, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("kdownarrow") And MapMain.Focused Then
            'Pan down slightly - 25% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim ydiff As Double = (exts.yMax - exts.yMin) / 4
                exts.SetBounds(exts.xMin, exts.yMin - ydiff, exts.zMin, exts.xMax, exts.yMax - ydiff, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("kleftarrow") And MapMain.Focused Then
            'Pan Left slightly - 25% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim xdiff As Double = (exts.xMax - exts.xMin) / 4
                exts.SetBounds(exts.xMin - xdiff, exts.yMin, exts.zMin, exts.xMax - xdiff, exts.yMax, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("krightarrow") And MapMain.Focused Then
            'Pan Right slightly - 25% of view
            Dim exts As MapWinGIS.Extents = MapMain.Extents
            If Not exts.xMin = 0 And Not exts.xMax = 0 And Not exts.yMin = 0 And Not exts.yMax = 0 Then
                Dim xdiff As Double = (exts.xMax - exts.xMin) / 4
                exts.SetBounds(exts.xMin + xdiff, exts.yMin, exts.zMin, exts.xMax + xdiff, exts.yMax, exts.zMax)
                MapMain.Extents = exts
            End If
            Return True
        ElseIf keystates.Contains("kplus") Then
            'Zoom in by 25%
            MapMain.ZoomIn(0.25)
            Return True
        ElseIf keystates.Contains("kminus") Then
            'Zoom out by 25%
            MapMain.ZoomOut(0.25)
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Sets focus to the map
    ''' </summary>
    Private Sub MapWindowForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MapMain.Focus()
    End Sub

    ''' <summary>
    ''' Overriding ProcessCmdKey is needed to get the arrow keys; it seems to catch other keys better than KeyDown as well.
    ''' </summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'Check to ensure we're focused before doing things to the map (Bugzilla 366)
        If MapMain.Focused() OrElse StripDocker.Focused OrElse MapPreview.Focused OrElse Legend.Focused Then
            'If the message was WM_KEYDOWN...
            If msg.Msg = &H100 Then
                'Try to handle it. If I don't care about the particular key, pass it
                'on to someone who does
                If Not HandleShortcutKeys(keyData) Then
                    MyBase.ProcessCmdKey(msg, keyData)
                End If
            Else
                'Perhaps someone else cares about this message?
                MyBase.ProcessCmdKey(msg, keyData)
            End If
        End If
    End Function


    ''' <summary>
    ''' Updating scale bar and statusbar
    ''' </summary>
    Private Sub MapWindowForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        UpdateFloatingScalebar()
    End Sub

    ''' <summary>
    ''' Handles the closing of main form. Saving settings, unlading plug-ins
    ''' </summary>
    Private Sub MapWindowForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        ProjInfo.SaveConfig()

        'PM Added
        SaveToolStripSettings(Me.StripDocker)

        ' For bug #946:
        MapWinUtility.Logger.Dbg("In MapWindowForm_Closing. ApplicationInfo.DefaultDir: " + Me.ApplicationInfo.DefaultDir)

        If Not m_HasBeenSaved Or ProjInfo.Modified Then
            If PromptToSaveProject() = MsgBoxResult.Cancel Then
                e.Cancel = True
                Me.DialogResult = DialogResult.Cancel
                Exit Sub
            End If
        End If

        If Not m_legendEditor Is Nothing Then m_legendEditor.Close()

        g_SyncPluginMenuDefer = True
        m_PluginManager.UnloadAll() ' cleans up plugins on shutdown
        m_PluginManager.UnloadApplicationPlugins()
        Me.DialogResult = DialogResult.OK
        'm_PluginManager.WriteSettingsFile()

    End Sub

#End Region

#Region "Legend Events"

    Private Sub Legend_GroupExpandedChanged(ByVal Handle As Integer, ByVal Expanded As Boolean) Handles Legend.GroupExpandedChanged
        SetModified(True)
    End Sub

    'Chris Michaelis 11/11/2006, see http://bugs.mapwindow.org/show_bug.cgi?id=340
    ' 10/17/2007 - SaveShapeLayerProps == misnomer - can also be used for saving grid coloring scheme.
    ' Can't change name now without breaking interface
    Friend Function SaveShapeLayerProps(ByVal handle As Integer, Optional ByVal filename As String = "") As Boolean
        If Layers(handle).LayerType = eLayerType.LineShapefile Or Legend.Layers.ItemByHandle(handle).Type = eLayerType.PointShapefile Or Legend.Layers.ItemByHandle(handle).Type = eLayerType.PolygonShapefile Then
            Dim doc As New Xml.XmlDocument
            Dim outfn As String = filename
            Dim node As Xml.XmlNode = doc.CreateElement("SFRendering")
            ProjInfo.AddLayerElement(doc, Layers(handle), node)
            doc.AppendChild(node)
            Try
                If outfn = "" Then outfn = System.IO.Path.ChangeExtension(CType(MapMain.get_GetObject(handle), MapWinGIS.Shapefile).Filename, ".mwsr")
                doc.Save(outfn)
            Catch e As Exception
                Dim errmsg As String = e.ToString() 'Default to exception text
                Try
                    If System.IO.File.Exists(outfn) Then
                        Dim fi As New System.IO.FileInfo(outfn)
                        'Note -- parenthesis in line below are critical (will always return true without)
                        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly Then
                            errmsg = "File is read-only: " + outfn
                        End If
                    ElseIf System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(outfn)) Then
                        Dim fi As New System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(outfn))
                        'Note -- parenthesis in line below are critical (will always return true without)
                        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly Then
                            errmsg = "Directory is read-only: " + System.IO.Path.GetDirectoryName(outfn)
                        End If
                    End If
                Catch
                End Try
                MapWinUtility.Logger.Dbg("Unable to save shapefile properties (mwsr file): " + errmsg)
                'Likely no need to worry; almost always is an access denied message for one reason or another.
            End Try
            Return True
        ElseIf Layers(handle).LayerType = eLayerType.Grid Then
            ' SaveShapeLayerProps == misnomer - can also be used for saving grid coloring scheme.
            ' Can't change name now without breaking interface
            Try
                Dim outfn As String = filename
                If outfn = "" Then outfn = System.IO.Path.ChangeExtension(Layers(handle).FileName, ".mwsr")
                ColoringSchemeTools.ExportScheme(Layers(handle), outfn)
            Catch
            End Try
        End If

        Return False
    End Function

    ''' <summary>
    ''' Loads rendering info for shapefile or grid (.mwsr file)
    ''' </summary>
    ''' Chris Michaelis 11/11/2006, see http://bugs.mapwindow.org/show_bug.cgi?id=340
    Friend Function LoadRenderingOptions(ByVal handle As Integer, Optional ByVal filename As String = "", Optional ByVal PluginCall As Boolean = False) As Boolean

        If Layers(handle).LayerType = eLayerType.LineShapefile Or _
           Layers(handle).LayerType = eLayerType.PointShapefile Or _
           Layers(handle).LayerType = eLayerType.PolygonShapefile Then

            If filename = "" Then
                Dim sf As MapWinGIS.Shapefile = CType(MapMain.get_GetObject(handle), MapWinGIS.Shapefile)
                If File.Exists(IO.Path.ChangeExtension(sf.Filename, ".mwsr")) Then

                    Dim doc As New Xml.XmlDocument
                    doc.Load(IO.Path.ChangeExtension(sf.Filename, ".mwsr"))
                    If Not doc.GetElementsByTagName("SFRendering").Count = 0 AndAlso Not doc.GetElementsByTagName("SFRendering")(0).ChildNodes.Count = 0 Then
                        ProjInfo.LoadLayerProperties(doc.GetElementsByTagName("SFRendering")(0).ChildNodes(0), handle, PluginCall)
                        SetModified(True)
                        Return True
                    End If

                End If
            Else
                If File.Exists(filename) Then
                    Dim doc As New Xml.XmlDocument
                    doc.Load(filename)
                    If Not doc.GetElementsByTagName("SFRendering").Count = 0 AndAlso Not doc.GetElementsByTagName("SFRendering")(0).ChildNodes.Count = 0 Then
                        ProjInfo.LoadLayerProperties(doc.GetElementsByTagName("SFRendering")(0).ChildNodes(0), handle, PluginCall)
                        SetModified(True)
                        Return True
                    End If
                End If
            End If
        ElseIf Layers(handle).LayerType = eLayerType.Grid Then
            ' SaveShapeLayerProps == misnomer - can also be used for saving grid coloring scheme.
            ' Can't change name now without breaking interface
            If filename = "" Then
                If File.Exists(IO.Path.ChangeExtension(Layers(handle).FileName, ".mwsr")) Then
                    filename = IO.Path.ChangeExtension(Layers(handle).FileName, ".mwsr")
                Else
                    'May be too early for grid filename to be set
                    Try
                        If Not frmMain.MapMain.get_GetObject(handle) Is Nothing Then
                            filename = IO.Path.ChangeExtension(CType(frmMain.MapMain.get_GetObject(handle), MapWinGIS.ImageClass).Filename, ".mwsr")
                        End If
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine(ex.ToString())
                    End Try
                End If
            End If
            If System.IO.File.Exists(filename) Then Layers(handle).ColoringScheme = ColoringSchemeTools.ImportScheme(Layers(handle), filename)
        End If

        Return False
    End Function

    Private Sub Legend_GroupMouseDown(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Legend.GroupMouseDown
        'Display the context menu for the legend - based on a click on a group.
        '4/24/2005 - dpa - Fixed location display of the menu 
        'Dim newPt As System.Drawing.Point
        If m_PluginManager.LegendMouseDown(Handle, button, Interfaces.ClickLocation.Group) = False Then
            If button = MouseButtons.Right Then

                m_GroupHandle = Handle
                ShowLayerMenu(Interfaces.ClickLocation.Group)
            End If
        End If
    End Sub

    Private Sub Legend_GroupDoubleClick(ByVal Handle As Integer) Handles Legend.GroupDoubleClick
        'First see if the plug-ins want this event.  
        'If not then show the legend editor.
        If m_PluginManager.LegendDoubleClick(Handle, Interfaces.ClickLocation.Group) = False Then
            If m_legendEditor Is Nothing Then
                'Make this dockable. 11/27/2006 CDM
                m_legendEditor = LegendEditorForm.CreateAndShowGRP(Handle)
                'm_legendEditor = New LegendEditorForm(Handle, False, Me.MapMain)
                'Me.AddOwnedForm(m_legendEditor)
                'm_legendEditor.Show()
            Else
                m_legendEditor.LoadProperties(Handle, False)
            End If
        End If
    End Sub

    Private Sub Legend_LegendClick(ByVal button As System.Windows.Forms.MouseButtons, ByVal Location As System.Drawing.Point) Handles Legend.LegendClick
        'Display the context menu for the legend.
        '4/24/2005 - dpa - Fixed location display of the menu 
        'Dim Pt As System.Drawing.Point
        'Dim newPt As System.Drawing.Point
        If m_PluginManager.LegendMouseDown(-1, button, Interfaces.ClickLocation.None) = False Then
            If button = MouseButtons.Right Then

                m_GroupHandle = -1
                ShowLayerMenu(Interfaces.ClickLocation.None)


                ''Pt = MapWinUtility.MiscUtils.GetCursorLocation()
                ''newPt.X = CType((Pt.X - Me.Left + 5), Integer)
                ''newPt.Y = CType((Pt.Y - Me.Top - 40), Integer)
                'newPt.X = Legend.PointToClient(Legend.MousePosition).X
                'newPt.Y = Legend.PointToClient(Legend.MousePosition).Y
                'mnuLegend.Show(frmMain, newPt)
                'mnuLegend.Items(2).Enabled = False
                'mnuLegend.Items(4).Enabled = False
                'mnuLegend.Items(5).Enabled = False
                'mnuLegend.Items(12).Visible = False
                ''Note the duplicate calls to .Show (one above higher) -- seems to be required, especially when undocked
                'mnuLegend.Show(Legend, newPt)
            End If
        End If
    End Sub

    Private Sub Legend_LayerSelected(ByVal Handle As Integer) Handles Legend.LayerSelected
        Try
            Dim lastMode As MapWinGIS.tkCursorMode = MapMain.CursorMode

            ' don't see reason to clear selection for the new ocx
            If (MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
                m_View.ClearSelectedShapes()
            End If
            If Not m_legendEditor Is Nothing Then
                'If Handle < 0 Then
                '    m_legendEditor.Close()
                '    m_legendEditor = Nothing
                '    Exit Sub
                'End If

                ' don't close the form, simply clear the info
                m_legendEditor.LoadProperties(Handle, True)
            End If

            If Handle = -1 Then Return

            m_layers.CurrentLayer = Handle

            m_PluginManager.LayerSelected(Handle)

            'Hide the attribute table menu item if the plugin is not available.
            If Not Plugins Is Nothing AndAlso Not frmMain.m_PluginManager.m_ApplicationPlugins Is Nothing Then
                mnuTableEditorLaunch.Visible = (frmMain.m_PluginManager.m_ApplicationPlugins.Contains("mwTableEditor_mwTableEditorClass"))
            End If

            'Prevent any plug-ins from changing current map cursor mode on this event:
            MapMain.CursorMode = lastMode
            UpdateButtons()

            'Christian Degrassi 2010-03-22; Preparing Enhancement 1651
            '_LayersMenuItemFactory.UpdateMenuForLayer(m_layers.Item(Handle))
        Catch
        End Try
    End Sub

    Private Sub Legend_GroupMouseUp(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Legend.GroupMouseUp
        'This one only gets passed to the plugins
        m_PluginManager.LegendMouseUp(Handle, CInt(IIf(button = MouseButtons.Left, vbLeftButton, vbRightButton)), Interfaces.ClickLocation.Group)
    End Sub

    Private Sub Legend_LayerMouseDown(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Legend.LayerMouseDown
        'Display the context menu for the legend on a right click on a layer.
        '4/24/2005 - dpa - Fixed location display of the menu 
        Dim Pt As New System.Drawing.Point, newPT As New System.Drawing.Point

        Legend.SelectedLayer = Handle
        'first see if the plugins are going to handle it.
        If m_PluginManager.LegendMouseDown(Handle, button, Interfaces.ClickLocation.Layer) = False Then
            If button = MouseButtons.Right Then

                m_GroupHandle = -1
                ShowLayerMenu(Interfaces.ClickLocation.Layer)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Shows the menu at certain position with a content appropriate to the selected layer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowLayerMenu(ByVal Location As Interfaces.ClickLocation)

        Dim pnt As New System.Drawing.Point

        mnuTableEditorLaunch.Visible = (Location = ClickLocation.Layer)
        ToolStripMenuLabelSetup.Visible = (Location = ClickLocation.Layer)
        ToolStripMenuCharts.Visible = (Location = ClickLocation.Layer)
        mnuLegendShapefileCategories.Visible = (Location = ClickLocation.Layer)
        ToolStripMenuRelabel.Visible = (Location = ClickLocation.Layer)
        mnuSaveAsLayerFile.Visible = (Location = ClickLocation.Layer)

        If Location = ClickLocation.Group Then

            mnuLegend.Items(2).Enabled = True
            mnuLegend.Items(4).Enabled = True

            ' what are this items?
            If Legend.Groups.ItemByHandle(m_GroupHandle).LayerCount > 0 Then
                mnuLegend.Items(4).Enabled = True
            Else
                mnuLegend.Items(4).Enabled = False
                mnuLegend.Items(5).Enabled = False
            End If

            mnuLegend.Items(2).Text = resources.GetString("mnuRemoveGroup.Text")
            mnuLegend.Items(4).Text = resources.GetString("mnuZoomToGroup.Text")

        ElseIf Location = ClickLocation.Layer Then

            mnuLegend.Items(2).Enabled = True
            mnuLegend.Items(4).Enabled = True
            mnuLegend.Items(2).Text = resources.GetString("mnuRemoveLayer.Text")
            mnuLegend.Items(4).Text = resources.GetString("mnuZoomToLayer.Text")

            Dim isShapefile As Boolean
            isShapefile = Layers(Legend.SelectedLayer).LayerType = eLayerType.LineShapefile Or _
                           Layers(Legend.SelectedLayer).LayerType = eLayerType.PointShapefile Or _
                           Layers(Legend.SelectedLayer).LayerType = eLayerType.PolygonShapefile

            ' shapefile specific properties
            mnuTableEditorLaunch.Visible = isShapefile
            ToolStripMenuLabelSetup.Visible = isShapefile
            ToolStripMenuCharts.Visible = isShapefile
            mnuLegendShapefileCategories.Visible = isShapefile
            ToolStripMenuRelabel.Visible = isShapefile And MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology
        Else
            mnuLegend.Items(2).Enabled = False
            mnuLegend.Items(4).Enabled = False
        End If

        pnt.X = Legend.PointToClient(Legend.MousePosition).X
        pnt.Y = Legend.PointToClient(Legend.MousePosition).Y
        mnuLegend.Show(frmMain, pnt)
        mnuLegend.Show(Legend, pnt)
        'Note the duplicate calls to .Show -- seems to be required, especially when undocked
    End Sub

    Private Sub DoAddGroup()
        Legend.Groups.Add()
        SetModified(True)
    End Sub

    Private Sub DoRemoveGroup()
        If Legend.Groups.IsValidHandle(m_GroupHandle) Then
            Dim groupName As String = Legend.Groups.ItemByHandle(m_GroupHandle).Text
            If Legend.Groups.ItemByHandle(m_GroupHandle).LayerCount > 0 Then
                '10/12/2005 PM
                'If mapwinutility.logger.msg("Are you sure you want to remove the" & vbCrLf & "selected group and its layer(s)?", MsgBoxStyle.YesNo, "Remove Group?") = MsgBoxResult.Yes Then
                If MapWinUtility.Logger.Msg(resources.GetString("msgRemoveGroup.Text"), MsgBoxStyle.YesNo, resources.GetString("titleRemoveGroup.Text")) = MsgBoxResult.Yes Then
                    Legend.Groups.Remove(m_GroupHandle)
                    SetModified(True)
                End If
            Else
                Legend.Groups.Remove(m_GroupHandle)
                SetModified(True)
            End If
            '---Cho 12/31/2008: Let plugins know that a group was removed.
            frmMain.Plugins.BroadcastMessage("GroupRemoved Name=" + groupName)
        End If
    End Sub

    Private Sub DoZoomToLayer()
        MapMain.ZoomToLayer(Legend.SelectedLayer)
        SetModified(True)
    End Sub

    Private Sub DoZoomToGroup()
        Dim maxX As Double, maxY As Double
        Dim minX As Double, minY As Double
        Dim dx As Double, dy As Double
        Dim i As Integer, tExts As MapWinGIS.Extents
        Dim bFoundVisibleLayer As Boolean

        If Legend.Groups.IsValidHandle(m_GroupHandle) = False Then Exit Sub
        bFoundVisibleLayer = False
        Dim LayersInGroup As New ArrayList
        For i = 0 To Legend.Groups.ItemByHandle(m_GroupHandle).LayerCount - 1
            LayersInGroup.Add(Legend.Groups.ItemByHandle(m_GroupHandle)(i).Handle)
        Next
        For Each i In LayersInGroup
            If MapMain.get_LayerVisible(i) = True Then
                tExts = Layers(i).Extents
                With tExts
                    If bFoundVisibleLayer = False Then
                        maxX = .xMax
                        minX = .xMin
                        maxY = .yMax
                        minY = .yMin
                        bFoundVisibleLayer = True
                    Else
                        If .xMax > maxX Then maxX = .xMax
                        If .yMax > maxY Then maxY = .yMax
                        If .xMin < minX Then minX = .xMin
                        If .yMin < minY Then minY = .yMin
                    End If
                End With
            End If
        Next i
        ' Pad extents now
        dx = maxX - minX
        dx = dx * MapMain.ExtentPad
        maxX = maxX + dx
        minX = minX - dx
        dy = maxY - minY
        dy = dy * MapMain.ExtentPad
        maxY = maxY + dy
        minY = minY - dy
        tExts = New MapWinGIS.Extents
        tExts.SetBounds(minX, minY, 0, maxX, maxY, 0)
        MapMain.Extents = tExts
        tExts = Nothing
        SetModified(True)

    End Sub

    Private Sub DoViewMetaData()
        Dim MetaDataFiles() As String = MapWinUtility.DataManagement.GetMetaDataFiles(Layers(Layers.CurrentLayer).FileName)
        If MetaDataFiles Is Nothing OrElse MetaDataFiles(0) Is Nothing Then
            MsgBox("No metadata is available. It can be created using the Open Metadata Manager, or may be available from the original data source.", MsgBoxStyle.Information, "No Metadata Available")
            Exit Sub
        Else
            System.Diagnostics.Process.Start(MetaDataFiles(0))
        End If

        'March 2008 - Should this launch the Metadata Editor plug-in Allen wrote?
    End Sub

    Private Sub DoExpandGroups()
        Legend.Groups.ExpandAll()
        SetModified(True)
    End Sub

    Private Sub DoExpandAll()
        Legend.Lock()
        Legend.Groups.ExpandAll()
        Legend.Layers.ExpandAll()
        Legend.Unlock()
        SetModified(True)
    End Sub

    Private Sub DoCollapseGroups()
        Legend.Groups.CollapseAll()
        SetModified(True)
    End Sub

    Private Sub DoCollapseAll()
        Legend.Lock()
        Legend.Groups.CollapseAll()
        Legend.Layers.CollapseAll()
        Legend.Unlock()
        SetModified(True)
    End Sub

    Private Sub DoEditProperties()
        'Christian Degrassi 2010-03-22: Refactoring as Part of Enhancement 1651
        If (m_GroupHandle <> -1) Then
            ShowGroupProperties(m_GroupHandle)
        Else
            ShowLayerProperties(Legend.SelectedLayer)
        End If

    End Sub

    'Christian degrassi 2010-03-22: Part of Enhancement 1651
    ''' <summary>
    ''' Shows Property Editor for a specific Layer
    ''' </summary>
    ''' <param name="LayerHandle"></param>
    ''' <remarks></remarks>
    Private Sub ShowLayerProperties(ByVal LayerHandle As Integer)
        If Legend.Layers.IsValidHandle(LayerHandle) Then
            If (MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
                If (m_legendEditor Is Nothing) Then
                    m_legendEditor = LegendEditorForm.CreateAndShowLYR(LayerHandle)
                End If
                m_legendEditor.LoadProperties(LayerHandle, True)
            Else

                Dim layerType As MapWindow.Interfaces.eLayerType = frmMain.Layers(LayerHandle).LayerType

                If layerType = eLayerType.LineShapefile Or _
                   layerType = eLayerType.PointShapefile Or _
                   layerType = eLayerType.PolygonShapefile Then

                    If Not m_legendEditor Is Nothing Then m_legendEditor.LoadProperties(LayerHandle, True)
                    Plugins.BroadcastMessage("SYMBOLOGY_EDIT" + Convert.ToString(Legend.SelectedLayer))
                Else
                    If (m_legendEditor Is Nothing) Then
                        m_legendEditor = LegendEditorForm.CreateAndShowLYR(LayerHandle)
                    Else
                        m_legendEditor.LoadProperties(LayerHandle, True)
                    End If
                End If
            End If
        End If
    End Sub

    'Christian degrassi 2010-03-22: Part of Enhancement 1651
    ''' <summary>
    ''' Shows Property Editor for a specific Group
    ''' </summary>
    ''' <param name="GroupHandle"></param>
    ''' <remarks></remarks>
    Private Sub ShowGroupProperties(ByVal GroupHandle As Integer)
        If Legend.Groups.IsValidHandle(GroupHandle) Then

            If (m_legendEditor Is Nothing) Then
                m_legendEditor = LegendEditorForm.CreateAndShowGRP(GroupHandle)
            End If

            m_legendEditor.LoadProperties(GroupHandle, False)

        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        DoAddGroup()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        DoAddLayer()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Dim menuText As String
        menuText = ToolStripMenuItem4.Text

        If ToolStripMenuItem4.Text = resources.GetString("mnuRemoveGroup.Text") Then
            DoRemoveGroup()
        Else
            DoRemoveLayer()
        End If
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        DoClearLayers()
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        Dim menuText As String
        menuText = resources.GetString("mnuZoomToLayer.Text")

        If menuText = resources.GetString("mnuZoomToLayer.Text") Then
            DoZoomToLayer()
        Else
            DoZoomToGroup()
        End If

        'Notify plugins - since this is done after handing it locally,
        'they can't override it -- but they do notice it.
        m_PluginManager.ItemClicked(menuText)
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        DoViewMetaData()
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        DoExpandGroups()
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        DoExpandAll()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        DoCollapseGroups()
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        DoCollapseAll()
    End Sub

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click
        DoEditProperties()
    End Sub

    Private Sub mnuTableEditorLaunch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTableEditorLaunch.Click
        Plugins.BroadcastMessage("TableEditorStart")
    End Sub

    Private Sub Legend_LayerMouseUp(ByVal Handle As Integer, ByVal button As System.Windows.Forms.MouseButtons) Handles Legend.LayerMouseUp
        m_PluginManager.LegendMouseUp(Handle, CInt(IIf(button = MouseButtons.Left, vbLeftButton, vbRightButton)), Interfaces.ClickLocation.Layer)
    End Sub

    Private Sub Legend_GroupPositionChanged(ByVal Handle As Integer) Handles Legend.GroupPositionChanged
        'All we do here is mark a modification for later saving.
        SetModified(True)
    End Sub

    Private Sub Legend_LayerDoubleClick(ByVal Handle As Integer) Handles Legend.LayerDoubleClick
        'If the plug-ins don't want this event then we handle it by showing layer properties.
        If m_PluginManager.LegendDoubleClick(Handle, Interfaces.ClickLocation.Layer) = False Then
            If frmMain.m_layers(Handle).LayerType <> Interfaces.eLayerType.Invalid Then

                'If (MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then

                '    If m_legendEditor Is Nothing Then
                '        m_legendEditor = LegendEditorForm.CreateAndShowLYR()
                '        'Make this dockable. 11/27/2006 CDM
                '        'm_legendEditor.Show()
                '    Else
                '        m_legendEditor.LoadProperties(Handle, True)
                '    End If
                'Else
                Dim layerType As MapWindow.Interfaces.eLayerType = frmMain.Layers(Handle).LayerType

                If layerType = eLayerType.LineShapefile Or _
                   layerType = eLayerType.PointShapefile Or _
                   layerType = eLayerType.PolygonShapefile Then

                    Plugins.BroadcastMessage("SYMBOLOGY_EDIT" + Convert.ToString(Legend.SelectedLayer))
                    If Not m_legendEditor Is Nothing Then m_legendEditor.LoadProperties(Handle, True)
                Else
                    If m_legendEditor Is Nothing Then
                        m_legendEditor = LegendEditorForm.CreateAndShowLYR()
                    Else
                        m_legendEditor.LoadProperties(Handle, True)
                    End If
                End If
                'End If
            Else
                '10/13/2005 PM
                'mapwinutility.logger.msg("No properties available for this layer type.", , "Error")
                MapWinUtility.Logger.Msg(resources.GetString("msgLegendLayerDoubleClick.Text"), resources.GetString("msgError.Text"))
            End If
        End If
    End Sub

    Private Sub Legend_LayerPositionChanged(ByVal Handle As Integer) Handles Legend.LayerPositionChanged
        SetModified(True)
    End Sub

    Private Sub Legend_LayerVisibleChanged(ByVal Handle As Integer, ByVal NewState As Boolean, ByRef Cancel As Boolean) Handles Legend.LayerVisibleChanged
        '12/13/2008 ARA modified to use new application setting as well as to cancel layer change when coming out of dynamic visibility in order to ensure proper functionality
        Dim SendMsgOverload As Boolean = False

        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            If Not m_AutoVis(Handle) Is Nothing Then
                If m_AutoVis(Handle).UseDynamicExtents = True Then
                    Cancel = True 'Always cancel for layer display when exiting dynamic visibility

                    If AppInfo.ShowDynamicVisibilityWarnings Then
                        Dim DynVisMsg As String
                        If AppInfo.ShowLayerAfterDynamicVisibility Then
                            DynVisMsg = resources.GetString("DisableDynamicVis.Text")
                        Else
                            DynVisMsg = resources.GetString("DisableDynamicVis2.Text")
                        End If
                        If MapWinUtility.Logger.Msg(DynVisMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, AppInfo.Name) = MsgBoxResult.Yes Then
                            m_layers(Handle).UseDynamicVisibility = False
                            m_layers(Handle).Visible = AppInfo.ShowLayerAfterDynamicVisibility
                            NewState = AppInfo.ShowLayerAfterDynamicVisibility
                            SendMsgOverload = True
                            SetModified(True)
                        End If
                    Else
                        m_layers(Handle).UseDynamicVisibility = False
                        m_layers(Handle).Visible = AppInfo.ShowLayerAfterDynamicVisibility
                        NewState = AppInfo.ShowLayerAfterDynamicVisibility
                        SendMsgOverload = True
                        SetModified(True)
                    End If
                End If
            Else
                SetModified(True)
            End If
        Else
            ' the code for the new version
            Dim dynVisibility As Boolean = frmMain.MapMain.get_LayerDynamicVisibility(Handle)
            If (dynVisibility) Then
                Dim minScale As Double = frmMain.MapMain.get_LayerMinVisibleScale(Handle)
                Dim maxScale As Double = frmMain.MapMain.get_LayerMaxVisibleScale(Handle)
                Dim scale As Double = frmMain.MapMain.CurrentScale

                Dim turnOff As Boolean = False
                If scale <= minScale Or scale >= maxScale Then
                    Cancel = True 'Always cancel for layer display when exiting dynamic visibility

                    turnOff = True  ' layer isn't visible, and then user wants it to, so we turn off dynamic visibility
                    If AppInfo.ShowDynamicVisibilityWarnings Then
                        Dim DynVisMsg As String
                        If AppInfo.ShowLayerAfterDynamicVisibility Then
                            DynVisMsg = resources.GetString("DisableDynamicVis.Text")
                        Else
                            DynVisMsg = resources.GetString("DisableDynamicVis2.Text")
                        End If

                        If MapWinUtility.Logger.Msg(DynVisMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, AppInfo.Name) = MsgBoxResult.No Then
                            turnOff = False     ' we leave DV in case user chose it
                        End If
                    End If
                End If

                ' turns off the dynamic visibility
                If turnOff Then
                    m_layers(Handle).UseDynamicVisibility = False
                    m_layers(Handle).Visible = AppInfo.ShowLayerAfterDynamicVisibility
                    NewState = AppInfo.ShowLayerAfterDynamicVisibility
                    SendMsgOverload = True
                    SetModified(True)
                End If
            Else
                SetModified(True)
            End If
        End If

        If SendMsgOverload Or Not Cancel Then frmMain.Plugins.BroadcastMessage("LayerVisibleChanged " + NewState.ToString() + " Handle=" + Handle.ToString())
    End Sub

    Private Sub m_legendEditor_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_legendEditor.Closed
        m_legendEditor = Nothing
    End Sub

    Private Sub Legend_LayerCheckboxClicked(ByVal Handle As Integer, ByVal NewState As Boolean) Handles Legend.LayerCheckboxClicked
        frmMain.Plugins.BroadcastMessage("LayerCheckboxClicked " + NewState.ToString() + " Handle=" + Handle.ToString())
    End Sub

    Public Sub RefreshDynamicVisibility1() Implements Interfaces.IMapWin.RefreshDynamicVisibility
        m_AutoVis.TestLayerZoomExtents()
    End Sub

    ''' <summary>
    ''' Returns reference to geoprocessing toobox in the form of tree view
    ''' </summary>
    Public ReadOnly Property GeoprocessingToolbox() As MapWindow.Interfaces.IGisToolBox Implements Interfaces.IMapWin.GisToolbox
        Get
            Return m_GisToolbox
        End Get
    End Property

#End Region

#Region "MapPreview Events"

    ''' <summary>
    ''' Handling Resize even for the preview map
    ''' </summary>
    Private Sub MapPreview_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MapPreview.SizeChanged
        If Not m_PreviewMap Is Nothing Then m_PreviewMap.UpdateLocatorBox()
    End Sub

    ''' <summary>
    ''' Handling mouse down event fr the preview map
    ''' </summary>
    Private Sub MapPreview_MouseDownEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseDownEvent) Handles MapPreview.MouseDownEvent
        If e.button = vbRightButton Then
            'Note the duplicate calls -- seems to be required, especially when undocked
            m_PreviewMapContextMenuStrip.Show(frmMain, MapPreview.PointToClient(MapPreview.MousePosition))
            m_PreviewMapContextMenuStrip.Show(MapPreview, MapPreview.PointToClient(MapPreview.MousePosition))
        Else
            'Determine if the box will start dragging
            If InBox(m_PreviewMap.g_ExtentsRect, CDbl(e.x), CDbl(e.y)) Then
                m_PreviewMap.g_Dragging = True
                oldX = e.x
                oldY = e.y
                m_startX = e.x
                m_startY = e.y
            Else
                m_PreviewMap.g_Dragging = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handling mouse up event fr the preview map
    ''' </summary>
    Private Sub MapPreview_MouseUpEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseUpEvent) Handles MapPreview.MouseUpEvent
        'Stop Dragging
        Dim newExts As New MapWinGIS.Extents
        Dim xMin, xMax, yMin, yMax As Double

        If m_PreviewMap.g_Dragging Then
            MapPreview.PixelToProj(m_PreviewMap.g_ExtentsRect.Left, m_PreviewMap.g_ExtentsRect.Top, xMin, yMax)
            MapPreview.PixelToProj(m_PreviewMap.g_ExtentsRect.Right, m_PreviewMap.g_ExtentsRect.Bottom, xMax, yMin)
            With newExts
                newExts.SetBounds(xMin, yMin, 0, xMax, yMax, 0)
            End With
            ' set the cursor
            MapPreview.MapCursor = MapWinGIS.tkCursor.crsrSizeAll
            ' apply the extents
            MapMain.Extents = newExts
            newExts = Nothing
            MapMain.Focus()
        Else
            ' Probably clicked outside the box, center on that location.
            Dim curCenterX, curCenterY As Integer
            With m_PreviewMap.g_ExtentsRect
                curCenterX = CInt((.Right + .Left) / 2)
                curCenterY = CInt((.Bottom + .Top) / 2)
                .Offset(e.x - curCenterX, e.y - curCenterY)
            End With
            MapPreview.PixelToProj(m_PreviewMap.g_ExtentsRect.Left, m_PreviewMap.g_ExtentsRect.Top, xMin, yMax)
            MapPreview.PixelToProj(m_PreviewMap.g_ExtentsRect.Right, m_PreviewMap.g_ExtentsRect.Bottom, xMax, yMin)
            With newExts
                newExts.SetBounds(xMin, yMin, 0, xMax, yMax, 0)
            End With
            ' set the cursor
            MapPreview.MapCursor = MapWinGIS.tkCursor.crsrSizeAll
            ' apply the extents
            MapMain.Extents = newExts
            newExts = Nothing
            MapMain.Focus()
        End If
        m_PreviewMap.g_Dragging = False
    End Sub

    ''' <summary>
    ''' Handling muse move event for the preview map
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MapPreview_MouseMoveEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseMoveEvent) Handles MapPreview.MouseMoveEvent
        'Move the Box
        If m_PreviewMap.g_Dragging = True AndAlso e.button = vbLeftButton Then
            m_PreviewMap.g_ExtentsRect.Offset(e.x - oldX, e.y - oldY)
            m_PreviewMap.DrawBox(m_PreviewMap.g_ExtentsRect)
            oldX = e.x
            oldY = e.y
        Else
            If e.button <> vbLeftButton Then m_PreviewMap.g_Dragging = False
            If InBox(m_PreviewMap.g_ExtentsRect, CDbl(e.x), CDbl(e.y)) Then
                MapPreview.MapCursor = MapWinGIS.tkCursor.crsrSizeAll
            Else
                MapPreview.MapCursor = MapWinGIS.tkCursor.crsrArrow
            End If
        End If
    End Sub

#End Region

#Region "MapMain Events"



    Private Sub MapMain_ExtentsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapMain.ExtentsChanged
        If m_Extents Is Nothing Then Exit Sub
        If MapMain.NumLayers = 0 Then Exit Sub

        MapWinUtility.Logger.Dbg(String.Format("Start ExtentsChanged. CurrentExtent: {0}. Extents.Count: {1}", m_CurrentExtent.ToString(), m_Extents.Count.ToString()))

        m_PreviewMap.UpdateLocatorBox()
        UpdateFloatingScalebar()

        If m_IsManualExtentsChange = True Then
            m_IsManualExtentsChange = False 'reset the flag for the next extents change
        Else
            FlushForwardHistory()
            m_Extents.Add(MapMain.Extents)
            m_CurrentExtent = m_Extents.Count - 1
        End If

        UpdateButtons()

        'update label/layer info to see if labels/layers need to be visible or not
        m_Labels.TestLabelZoomExtents()
        m_AutoVis.TestLayerZoomExtents()

        ' checking if visiblity state of any layer has changed
        ' the legend will be updated then, we don't want to update legend 
        ' on every zoom operation as it can be slow
        Dim scale As Double = Me.MapMain.CurrentScale()

        If (scale <> m_lastScale) Then
            For i As Integer = 0 To MapMain.NumLayers - 1
                Dim handle As Integer = MapMain.get_LayerHandle(i)
                If MapMain.get_LayerDynamicVisibility(handle) Then
                    Dim min As Double = MapMain.get_LayerMinVisibleScale(handle)
                    Dim max As Double = MapMain.get_LayerMaxVisibleScale(handle)
                    Dim visibleBefore As Boolean = (m_lastScale >= min And m_lastScale <= max)
                    Dim visibleNow As Boolean = (scale >= min And scale <= max)
                    If (visibleBefore <> visibleNow) Then
                        Legend.Refresh()
                        Exit For
                    End If
                End If
            Next
        End If

        m_lastScale = scale


        m_PluginManager.MapExtentsChanged()

        MapWinUtility.Logger.Dbg(String.Format("End ExtentsChanged. CurrentExtent: {0}. Extents.Count: {1}", m_CurrentExtent.ToString(), m_Extents.Count.ToString()))



        'Commented this next line out because this function gets called when the project is loading, so the following line makes it so that you always have to deal with the "save project" dialog.
        'SetModified(True)
    End Sub

    Private Sub MapMain_FileDropped(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_FileDroppedEvent) Handles MapMain.FileDropped
        'Chris M Oct 24 2007 for Bugzilla #560
        'ATC Apr 15 2008 - if RunProjectCommandLine does not recognize file, try the plugins
        Dim lBroadcast As Boolean = False
        If m_HandleFileDrop Then
            'Chris M April 17 2006 for Bugzilla #123
            'Since the filename could now be a project file, grid, shapefile,
            'or even script -- just run it using the "RunProjectCommandLine" function,
            'which does the same thing we're aiming at here.
            RunProjectCommandLine(e.filename, lBroadcast)

            'Old -- just added the layer blindly as if it were data
            'm_layers.AddLayer(e.filename)
        Else
            lBroadcast = True
        End If
        If lBroadcast Then
            Dim pnt As Point = MapMain.PointToClient(Cursor.Position)
            Dim MousedownX As Double = 0
            Dim MousedownY As Double = 0
            View.PixelToProj(pnt.X, pnt.Y, MousedownX, MousedownY)
            Plugins.BroadcastMessage("FileDropEvent|" + MousedownX.ToString() + "|" + MousedownY.ToString() + "|" + e.filename)
        End If
        MapWinUtility.Logger.Dbg("Finshed MapMain_FileDropped()")
    End Sub

    Private Sub MapMain_MouseDownEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseDownEvent) Handles MapMain.MouseDownEvent
        If (MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn Or MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut Or MapMain.CursorMode = MapWinGIS.tkCursorMode.cmPan) Then
            SetModified(True)
        End If

        If AppInfo.AreaMeasuringCurrently Then
            'Handle area measurement
            Dim CurrentLayerGood As Boolean = False
            Dim currPoint As New MapWinGIS.Point
            Dim locx, locy As Double

            ' Get actual location and store it in a point which is added to point list
            View.PixelToProj(e.x, e.y, locx, locy)
            currPoint.x = locx
            currPoint.y = locy
            AppInfo.AreaMeasuringlstDrawPoints.Add(currPoint)

            If View.CursorMode = MapWinGIS.tkCursorMode.cmNone Then
                ' User clicked the right mouse button -- stop measuring area and display message
                If e.button = 2 Then
                    '1/23/09 JK moved the code to MouseUpEvent
                    'AreaMeasuringDisplayResult()
                    'AreaMeasuringStop()
                    ''Start the next one, after having cleared settings from the previous
                    'AreaMeasuringBegin()
                Else
                    View.Draw.DrawPoint(locx, locy, 3, Drawing.Color.Red)
                    If (AppInfo.AreaMeasuringLastStartPtX = -1) Then
                        AppInfo.AreaMeasuringLastStartPtX = System.Windows.Forms.Control.MousePosition.X
                        AppInfo.AreaMeasuringLastStartPtY = System.Windows.Forms.Control.MousePosition.Y
                        AppInfo.AreaMeasuringStartPtX = AppInfo.AreaMeasuringLastStartPtX
                        AppInfo.AreaMeasuringStartPtY = AppInfo.AreaMeasuringLastStartPtY
                        AppInfo.AreaMeasuringEraseLast = False
                    Else
                        'Reverse the one to the start place
                        System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(AppInfo.AreaMeasuringStartPtX, AppInfo.AreaMeasuringStartPtY), New System.Drawing.Point(AppInfo.AreaMeasuringLastEndX, AppInfo.AreaMeasuringLastEndY), AppInfo.AreaMeasuringmycolor)
                        'Permanently draw line (already drawn, don't erase -- just move it)
                        AppInfo.AreaMeasuringReversibleDrawn.Add(AppInfo.AreaMeasuringLastStartPtX)
                        AppInfo.AreaMeasuringReversibleDrawn.Add(AppInfo.AreaMeasuringLastStartPtY)
                        AppInfo.AreaMeasuringReversibleDrawn.Add(System.Windows.Forms.Control.MousePosition.X)
                        AppInfo.AreaMeasuringReversibleDrawn.Add(System.Windows.Forms.Control.MousePosition.Y)
                        'Update for next loop
                        AppInfo.AreaMeasuringLastStartPtX = System.Windows.Forms.Control.MousePosition.X
                        AppInfo.AreaMeasuringLastStartPtY = System.Windows.Forms.Control.MousePosition.Y
                        AppInfo.AreaMeasuringEraseLast = False
                    End If
                End If
            End If
        Else
            m_PluginManager.MapMouseDown(e.button, e.shift, e.x, e.y)
        End If
    End Sub

    Private Sub MapMain_MouseMoveEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseMoveEvent) Handles MapMain.MouseMoveEvent
        If AppInfo.AreaMeasuringCurrently Then
            AppInfo.AreaMeasuringmycolor = Drawing.Color.White
            If AppInfo.AreaMeasuringEraseLast Then
                System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(AppInfo.AreaMeasuringLastStartPtX, AppInfo.AreaMeasuringLastStartPtY), New System.Drawing.Point(AppInfo.AreaMeasuringLastEndX, AppInfo.AreaMeasuringLastEndY), AppInfo.AreaMeasuringmycolor)
                System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(AppInfo.AreaMeasuringStartPtX, AppInfo.AreaMeasuringStartPtY), New System.Drawing.Point(AppInfo.AreaMeasuringLastEndX, AppInfo.AreaMeasuringLastEndY), AppInfo.AreaMeasuringmycolor)
            End If
            If Not AppInfo.AreaMeasuringLastStartPtX = -1 Then
                AppInfo.AreaMeasuringLastEndX = System.Windows.Forms.Control.MousePosition.X
                AppInfo.AreaMeasuringLastEndY = System.Windows.Forms.Control.MousePosition.Y
                System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(AppInfo.AreaMeasuringLastStartPtX, AppInfo.AreaMeasuringLastStartPtY), New System.Drawing.Point(AppInfo.AreaMeasuringLastEndX, AppInfo.AreaMeasuringLastEndY), AppInfo.AreaMeasuringmycolor)
                System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(AppInfo.AreaMeasuringStartPtX, AppInfo.AreaMeasuringStartPtY), New System.Drawing.Point(AppInfo.AreaMeasuringLastEndX, AppInfo.AreaMeasuringLastEndY), AppInfo.AreaMeasuringmycolor)
                AppInfo.AreaMeasuringEraseLast = True
            End If
        ElseIf AppInfo.MeasuringCurrently Then
            'Handle distance measurement
            Dim a As Double
            Dim b As Double
            If AppInfo.MeasuringStartX <> 0 And AppInfo.MeasuringStartY <> 0 Then ' a start point has been established
                MapMain.PixelToProj(e.x, e.y, a, b)

                'the units specified in File..Settings..Project Settings..Map Data Units (default - meters)
                Dim DataUnit As UnitOfMeasure = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnits)

                Dim MeasureUnit As UnitOfMeasure = DataUnit 'the unit specified in Project Settings..Show Additional Unit
                'Prefer alternate units
                MeasureUnit = MapWinGeoProc.UnitConverter.StringToUOM(modMain.ProjInfo.ShowStatusBarCoords_Alternate)

                'Don't add to cumulative distance yet - do that on mouse up.
                'For now, just add the length of the current segment to the prior segments.
                Dim tempAdditionalDistance As Double = distance(AppInfo.MeasuringStartX, AppInfo.MeasuringStartY, a, b)

                'Since the Distance function made the conversion from DecimalDegrees to Kilometers, 
                'data units are now effectively kilometers.
                If DataUnit = UnitOfMeasure.DecimalDegrees Then DataUnit = UnitOfMeasure.Kilometers
                If MeasureUnit = UnitOfMeasure.DecimalDegrees Then MeasureUnit = DataUnit

                'Allow Convert to handle it properly to return the unit the user wants.
                'Jiri Kadlec 8/28/2008 use the new unit conversion code from MapWinGeoProc
                Dim newDist As Double = 0
                Try
                    newDist = MapWinGeoProc.UnitConverter.ConvertLength(DataUnit, MeasureUnit, _
                        AppInfo.MeasuringTotalDistance + tempAdditionalDistance)
                Catch ex As Exception
                    newDist = AppInfo.MeasuringTotalDistance + tempAdditionalDistance
                End Try

                '3/16/2008 JK - internationalization
                StatusBar.Item(GetOrRemovePanel(resources.GetString("msgPanelDistance.Text"))).Text = _
                    resources.GetString("msgPanelDistance.Text") & " " & formatDistance(newDist) & _
                    " " & MeasureUnit.ToString & " " & resources.GetString("msgDistanceStartOver.Text")

                ' establish a new finishPoint and draw the line
                AppInfo.MeasuringScreenPointFinish = New Point(e.x, e.y)

                If (Not AppInfo.MeasuringDrawing = -1) Then
                    MapMain.ClearDrawing(AppInfo.MeasuringDrawing)
                    AppInfo.MeasuringDrawing = MapMain.NewDrawing(MapWinGIS.tkDrawReferenceList.dlScreenReferencedList)
                End If

                If AppInfo.MeasuringDrawing = -1 Then AppInfo.MeasuringDrawing = MapMain.NewDrawing(MapWinGIS.tkDrawReferenceList.dlScreenReferencedList)

                AppInfo.MeasuringDrawPreviousSegments()

                MapMain.DrawLine(AppInfo.MeasuringScreenPointStart.X, AppInfo.MeasuringScreenPointStart.Y, AppInfo.MeasuringScreenPointFinish.X, AppInfo.MeasuringScreenPointFinish.Y, 2, Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)))
            End If
        End If

        'Still notify other plugins of the move, even if measuring:
        m_PluginManager.MapMouseMove(e.x, e.y)

        'Map Tooltips - update last move time
        If MapTooltipsAtLeastOneLayer Then MapToolTipsLastMoveTime = Now

        ' 2/20/08 LCW: this will allow you to edit one of the auto-hide property grids (leaving it selected) and then move the mouse back over the graph
        ' and have it dismiss the property grid without clicking on the graph
        If dckPanel.ActivePane IsNot Nothing Then
            If Not dckPanel.ActivePane.NestedDockingStatus.IsDisplaying Then MapMain.Focus()
        End If

    End Sub

    Private Sub MapToolTipTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MapToolTipTimer.Tick
        'If we've held the mouse position steady for 1 second, proceed and show a tooltip
        If Now.Subtract(MapToolTipsLastMoveTime).Seconds < 1 Then Return

        'Prevent calling multiple searches for a tooltip at once
        '(e.g., huge shapefile)

        MapToolTipTimer.Stop()
        MapToolTipTimer.Enabled = False

        Try
            'The first layer to match is going to win...
            For i As Integer = 0 To Legend.Layers.Count - 1
                If Legend.Layers(i).MapTooltipsEnabled And (Legend.Layers(i).Type = eLayerType.LineShapefile Or Legend.Layers(i).Type = eLayerType.PointShapefile Or Legend.Layers(i).Type = eLayerType.PolygonShapefile) Then
                    Dim shapes As Object = Nothing
                    Dim sf As MapWinGIS.Shapefile = MapMain.get_GetObject(Legend.Layers(i).Handle)
                    Dim ec As Point = MapMain.PointToClient(System.Windows.Forms.Cursor.Position)
                    Dim a, b, c, d As Double
                    MapMain.PixelToProj(ec.X - 8, ec.Y - 8, a, b)
                    MapMain.PixelToProj(ec.X + 8, ec.Y + 8, c, d)
                    Dim extents As New MapWinGIS.Extents
                    extents.SetBounds(a, b, 0, c, d, 0)
                    'Use my own tolerance (in pixels) rather than SelectShapes tolerance (in map units)
                    If sf.SelectShapes(extents, 0, MapWinGIS.SelectMode.INTERSECTION, shapes) AndAlso (shapes IsNot Nothing AndAlso shapes.Length > 0) Then
                        'Assume only first
                        Dim fp As Point = Me.PointToClient(System.Windows.Forms.Cursor.Position)
                        MapToolTipObject.Show(sf.CellValue(Legend.Layers(i).MapTooltipFieldIndex, CType(shapes(0), Integer)), Me, fp.X, fp.Y, 2000)
                        Exit For 'Found one - call it good. We don't want to be showing multiple tooltips.
                    End If
                End If
            Next
        Catch
        End Try

        '...and back on:
        MapToolTipTimer.Enabled = True
        MapToolTipTimer.Start()
    End Sub

    Public Sub UpdateMapToolTipsAtLeastOneLayer()
        MapTooltipsAtLeastOneLayer = False
        Try
            'The first layer to match is going to win...
            For i As Integer = 0 To Legend.Layers.Count - 1
                If Legend.Layers(i).MapTooltipsEnabled And (Legend.Layers(i).Type = eLayerType.LineShapefile Or Legend.Layers(i).Type = eLayerType.PointShapefile Or Legend.Layers(i).Type = eLayerType.PolygonShapefile) Then
                    MapTooltipsAtLeastOneLayer = True
                    Return 'No need to keep going
                End If
            Next
        Catch
        End Try
    End Sub

    Private Sub MapMain_SelectBoxFinal(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_SelectBoxFinalEvent) Handles MapMain.SelectBoxFinal
        Const vbKeyControl As Integer = 17
        Dim tSI As MapWindow.SelectInfo

        If MapMain.NumLayers = 0 Then Exit Sub

        If MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn Then
            m_PreviewMap.UpdateLocatorBox()

        ElseIf MapMain.CursorMode = MapWinGIS.tkCursorMode.cmSelection Then
            If m_PluginManager.MapDragFinished(New Rectangle(e.left, e.top, e.right - e.left, e.top - e.bottom)) = False Then
                'dpa 02/15/02
                If Layers(Legend.SelectedLayer).LayerType = eLayerType.Image Then Exit Sub
                tSI = m_View.SelectShapesByRectangle(e.left, e.right, e.top, e.bottom, CBool(GetAsyncKeyState(vbKeyControl)))
                m_PluginManager.ShapesSelected(Legend.SelectedLayer, tSI)
                UpdateButtons()
            End If
        End If
    End Sub

#End Region

#Region "Toolbar Functions"

    ''' <summary>
    ''' Enable or disable the default buttons based on the map state
    ''' </summary>
    ''' <remarks>Modified by Paul Meems on April 13, 2011</remarks>
    Friend Sub UpdateButtons()



            ' 3 Nov. 2010 Paul Meems - If the toolbar has been removed by a plug-in there's no need to update the buttons:
            If frmMain.StripDocker.TopToolStripPanel.Visible = False Then
                Return
            End If

            ' updating main menu state
            Dim isLayerSelected As Boolean = Me.Legend.SelectedLayer > -1
            Dim isShapefileLayer As Boolean = False

            Dim layer As MapWindow.Interfaces.Layer = Layers(Me.Legend.SelectedLayer)
            If Not layer Is Nothing Then
                If layer.LayerType = eLayerType.LineShapefile Or _
                   layer.LayerType = eLayerType.PointShapefile Or _
                   layer.LayerType = eLayerType.PolygonShapefile Then
                    isShapefileLayer = True
                End If
            End If

            ' Enables or disabled the buttons:
            Dim numLayers As Integer = MapMain.NumLayers
            Dim previewMapExtentsIsValid As Boolean = PreviewMapExtentsValid()

            If Not gemdb Is Nothing And numLayers > 0 Then
                tbbAddPoint.Enabled = True
                tbbQueryPoint.Enabled = True
            Else
                tbbAddPoint.Enabled = False
                tbbQueryPoint.Enabled = False
            End If


            'tlbMain:
            tbbSelect.Enabled = isShapefileLayer
            ' Moved down for better selection check tbbDeSelectLayer.Enabled = isShapefileLayer AndAlso (m_View.SelectedShapes.NumSelected > 0)
            tbbPan.Enabled = (numLayers > 0)

            'tlbZoom
            tbbZoomExtent.Enabled = (numLayers > 0)
            tbbZoomIn.Enabled = (numLayers > 0)
            tbbZoomLayer.Enabled = (numLayers > 0)
            tbbZoomNext.Enabled = (numLayers > 0) AndAlso (m_CurrentExtent < m_Extents.Count - 1) AndAlso (m_Extents.Count > 0)
            tbbZoomOut.Enabled = (numLayers > 0)
            tbbZoomPrevious.Enabled = (numLayers > 0) AndAlso (m_Extents.Count > 0) AndAlso (m_CurrentExtent > 0)
            tbbZoomSelected.Enabled = (numLayers > 0) AndAlso (m_View.SelectedShapes.NumSelected > 0)

            mnuZoomPrevious.Enabled = tbbZoomPrevious.Enabled
            mnuZoomNext.Enabled = tbbZoomNext.Enabled
            mnuZoomMax.Enabled = tbbZoomExtent.Enabled
            mnuZoomLayer.Enabled = tbbZoomLayer.Enabled
            mnuZoomSelected.Enabled = tbbZoomSelected.Enabled
            mnuZoomShape.Enabled = False

            If Not m_Menu.Item("mnuZoomToPreviewExtents") Is Nothing Then Me.mnuZoomPreviewMap.Enabled = (numLayers > 0 AndAlso previewMapExtentsIsValid)
            If Not (Legend Is Nothing OrElse Legend.SelectedLayer = -1) Then
                If Legend.SelectedLayer >= 0 Then
                    Dim lt As eLayerType
                    If m_layers Is Nothing OrElse m_layers(Legend.SelectedLayer) Is Nothing Then
                        lt = eLayerType.Invalid
                    Else
                        lt = m_layers(Legend.SelectedLayer).LayerType
                    End If
                    mnuZoomShape.Enabled = (lt = eLayerType.LineShapefile Or _
                                                      lt = eLayerType.PointShapefile Or _
                                                      lt = eLayerType.PolygonShapefile)
                End If
            End If

            'tlbStandard
            tbbSave.Enabled = (Not m_HasBeenSaved) Or ProjInfo.Modified
            tbbPrint.Enabled = (numLayers > 0)

            'tlbLayers
            tbbRemoveLayer.Enabled = (numLayers > 0)
            tbbClearLayers.Enabled = (numLayers > 0)
            tbbSymbologyManager.Enabled = isShapefileLayer
            tbbLayerProperties.Enabled = (numLayers > 0)

            'Next, toggle the zoom buttons if anything has changed:
            Dim value As MapWinGIS.tkCursorMode = MapMain.CursorMode
            tbbSelect.Checked = (value = MapWinGIS.tkCursorMode.cmSelection)
            tbbPan.Checked = (value = MapWinGIS.tkCursorMode.cmPan)
            tbbZoomIn.Checked = (value = MapWinGIS.tkCursorMode.cmZoomIn)
        tbbZoomOut.Checked = (value = MapWinGIS.tkCursorMode.cmZoomOut)



        If MapMain.CursorMode <> MapWinGIS.tkCursorMode.cmNone Then
            tbbQueryPoint.Checked = False
            tbbAddPoint.Checked = False
        End If


            frmMain.Menus("mnuOptionsManager").Enabled = isLayerSelected
            frmMain.Menus("mnuLayerProperties").Enabled = isLayerSelected
            frmMain.Menus("mnuLayerLabels").Enabled = isShapefileLayer
            frmMain.Menus("mnuLayerRelabel").Enabled = isShapefileLayer
            frmMain.Menus("mnuLayerCharts").Enabled = isShapefileLayer
            frmMain.Menus("mnuLayerAttributeTable").Enabled = isShapefileLayer
            frmMain.Menus("mnuLayerCategories").Enabled = isShapefileLayer
            frmMain.Menus("mnuQueryLayer").Enabled = isShapefileLayer

            'frmMain.Menus("mnuLegendEditor").Checked = Not Me.m_legendEditor Is Nothing

            Dim selection As Boolean = False
            Dim sf As MapWinGIS.Shapefile = Me.MapMain.get_Shapefile(Me.Legend.SelectedLayer)
            If Not sf Is Nothing Then
                selection = sf.NumSelected > 0
            End If
            frmMain.Menus("mnuClearSelectedShapes").Enabled = selection
            tbbDeSelectLayer.Enabled = selection

            selection = False
            For i As Integer = 0 To Me.MapMain.NumLayers - 1
                sf = Me.MapMain.get_Shapefile(Me.MapMain.get_LayerHandle(i))
                If Not sf Is Nothing Then
                    If sf.NumSelected > 0 Then
                        selection = True
                        Exit For
                    End If
                End If
            Next i
            frmMain.Menus("mnuClearAllSelection").Enabled = selection

            'toolbar items added from table editor:
            Dim btn As MapWindow.Interfaces.ToolbarButton = Me.m_Toolbar.ButtonItem("TableEditorButton")
            If Not btn Is Nothing Then btn.Enabled = isShapefileLayer

            ' toolbar items added from symbology plug-in
            btn = Me.m_Toolbar.ButtonItem("Categories")
            If Not btn Is Nothing Then btn.Enabled = isShapefileLayer

            btn = Me.m_Toolbar.ButtonItem("Label Mover")
            If Not btn Is Nothing Then btn.Enabled = isShapefileLayer

            btn = Me.m_Toolbar.ButtonItem("Query")
            If Not btn Is Nothing Then btn.Enabled = isShapefileLayer

            If (tbbSelect.Checked Or tbbPan.Checked Or tbbZoomIn.Checked Or tbbZoomOut.Checked) Then MapMain.MapCursor = MapWinGIS.tkCursor.crsrMapDefault

            MapWinUtility.Logger.Dbg("Finished UpdateButtons()")
    End Sub

    Public Sub tlbMain_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbMain.ItemClicked
        Dim Btn As Windows.Forms.ToolStripItem = e.ClickedItem
        Dim BtnName As String = CType(Btn.Name, String)
        If BtnName.Trim() = "" And TypeOf (Btn.Tag) Is String Then BtnName = CStr(Btn.Tag)
        HandleButtonClick(BtnName, Btn)
    End Sub

    Public Sub HandleButtonClick(ByVal BtnName As String, Optional ByVal Btn As ToolStripItem = Nothing)

        'TODO: PM April 2011, Shouldn't we start with calling m_PluginManager.ItemClicked(BtnName)
        ' If it returns false try the default values?
        ' Then plug-ins can override every button if they like to.

        Dim Handled As Boolean
        Select Case BtnName
            Case "tbbAddRemove"
                ' TODO
                If Not Btn Is Nothing Then CType(Btn, ToolStripDropDownButton).HideDropDown()

                If mnuBtnAdd.Checked Then
                    DoAddLayer()
                ElseIf mnuBtnRemove.Checked Then
                    DoRemoveLayer()
                ElseIf mnuBtnClear.Checked Then
                    DoClearLayers()
                End If
            Case "tbbSelect"
                MapMain.CursorMode = MapWinGIS.tkCursorMode.cmSelection
                UpdateButtons()
                'Notify plugins - since this is done after handing it locally,
                'they can't override it -- but they do notice it.
                Handled = m_PluginManager.ItemClicked(BtnName)
            Case "tbbDeSelectLayer"
                DoClearLayerSelection()
                UpdateButtons()
                'Notify plugins - since this is done after handing it locally,
                'they can't override it -- but they do notice it.
                Handled = m_PluginManager.ItemClicked(BtnName)
            Case Else ' Plugins can override anything else
                Handled = m_PluginManager.ItemClicked(BtnName)
        End Select

        'Now that the plug-in manager has possibly sent off plug-in requests,
        'make sure that no plug-ins changed map cursor states:
        UpdateButtons()
    End Sub

    Private Sub tlbZoom_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbZoom.ItemClicked

        Dim Btn As Windows.Forms.ToolStripButton = e.ClickedItem
        Dim BtnName As String = CType(Btn.Name, String)
        If BtnName.Trim() = "" And TypeOf (Btn.Tag) Is String Then BtnName = CStr(Btn.Tag)
        HandleZoomButtonClick(BtnName)
    End Sub

    ''' <summary>
    ''' Handle the click event of the buttons on the zoom toolbar
    ''' </summary>
    ''' <param name="BtnName">The name of the button</param>
    ''' <remarks>Added April 12 2011, by Paul Meems</remarks>
    Friend Sub HandleZoomButtonClick(ByVal BtnName As String)

        Dim Handled As Boolean
        Select Case BtnName
            Case "tbbAddPoint"
                MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
            Case "tbbQueryPoint"
                MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
            Case "tbbPan"
                MapMain.CursorMode = MapWinGIS.tkCursorMode.cmPan
                UpdateButtons()
                'Notify plugins - since this is done after handing it locally,
                'they can't override it -- but they do notice it.
                Handled = m_PluginManager.ItemClicked(BtnName)
            Case "tbbZoomIn"
                doZoomIn()
                'Notify plugins - since this is done after handing it locally,
                'they can't override it -- but they do notice it.
                Handled = m_PluginManager.ItemClicked(BtnName)

            Case "tbbZoomOut"
                doZoomOut()
                'Notify plugins - since this is done after handing it locally,
                'they can't override it -- but they do notice it.
                Handled = m_PluginManager.ItemClicked(BtnName)

            Case "tbbZoomExtent"
                DoZoomMax()

            Case "tbbZoomSelected"
                DoZoomSelected()

            Case "tbbZoomPrevious"
                DoZoomPrevious()

            Case "tbbZoomNext"
                DoZoomNext()

            Case "tbbZoomLayer"
                DoZoomLayer()

            Case Else
                ' Plugins can override anything else
                Handled = m_PluginManager.ItemClicked(BtnName)
        End Select

        'Now that the plug-in manager has possibly sent off plug-in requests,
        'make sure that no plug-ins changed map cursor states:
        UpdateButtons()
    End Sub

    Private Sub tlbStandard_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbStandard.ItemClicked
        Dim Btn As Windows.Forms.ToolStripItem = e.ClickedItem
        Dim BtnName As String = CType(Btn.Name, String)
        If BtnName.Trim() = "" And TypeOf (Btn.Tag) Is String Then BtnName = CStr(Btn.Tag)
        HandleStandardButtonClick(BtnName, Btn)
    End Sub

    ''' <summary>
    ''' Handle the click event of the buttons on the standard toolbar
    ''' </summary>
    ''' <param name="BtnName">The name of the button</param>
    ''' <param name="Btn">The toolstrip button</param>
    ''' <remarks>Added April 13 2011, by Paul Meems</remarks>
    Friend Sub HandleStandardButtonClick(ByVal BtnName As String, ByVal Btn As ToolStripButton)

        Dim Handled As Boolean = m_PluginManager.ItemClicked(BtnName)
        If Not Handled Then
            'the plugin didn't override the buttons. So handle them now.
            Select Case BtnName
                Case "tbbPrint"
                    DoPrint()
                Case "tbbSave"
                    DoSave()
                Case "tbbNew"
                    DoNew()
                Case "tbbOpen"
                    DoOpen()
                Case "tbbProjectSettings"
                    DoProjectSettings()
            End Select
        End If

        'Now that the plug-in manager has possibly sent off plug-in requests,
        'make sure that no plug-ins changed map cursor states:
        UpdateButtons()
    End Sub

    Private Sub tlbLayers_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbLayers.ItemClicked
        Dim Btn As Windows.Forms.ToolStripItem = e.ClickedItem
        Dim BtnName As String = CType(Btn.Name, String)
        If BtnName.Trim() = "" And TypeOf (Btn.Tag) Is String Then BtnName = CStr(Btn.Tag)
        HandleLayersButtonClick(BtnName, Btn)
    End Sub

    ''' <summary>
    ''' Handle the click event of the buttons on the layers toolbar
    ''' </summary>
    ''' <param name="BtnName">The name of the button</param>
    ''' <param name="Btn">The toolstrip button</param>
    ''' <remarks>Added April 13 2011, by Paul Meems</remarks>
    Friend Sub HandleLayersButtonClick(ByVal BtnName As String, ByVal Btn As ToolStripButton)
        If AppInfo.MeasuringCurrently And Not BtnName = "tbbMeasure" Then AppInfo.MeasuringStop()
        If AppInfo.AreaMeasuringCurrently And Not BtnName = "tbbMeasureArea" Then AppInfo.AreaMeasuringStop()

        'the plugin didn't override the buttons. So handle them now.
        Select Case BtnName
            Case "tbbAddLayer"
                DoAddLayer()
            Case "tbbRemoveLayer"
                DoRemoveLayer()
            Case "tbbClearLayers"
                DoClearLayers()
            Case "tbbSymbologyManager"
                DoSymbologyManager()
            Case "tbbLayerProperties"
                ShowLayerProperties(Legend.SelectedLayer)
            Case Else
                ' Plugins can override anything else
                m_PluginManager.ItemClicked(BtnName)
        End Select

        'Now that the plug-in manager has possibly sent off plug-in requests,
        'make sure that no plug-ins changed map cursor states:
        UpdateButtons()
    End Sub

    Public Sub CustomCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Fires when user clicks a custom combo box on the main toolbar or floating toolbar.
        Dim cbClicked As Windows.Forms.ToolStripItem = CType(sender, Windows.Forms.ToolStripItem)
        m_PluginManager.ItemClicked(cbClicked.Name)
    End Sub

#End Region

#Region "Menu Functions"
    'All of these menu functions were extracted from DotNetBar code and are
    'new in the open source version 4
    'updated 1/25/2005
    'updated 10/1/2008 - Earljon Hidalgo (ejh) - Added icons for MapWindow UI including context menus



    Private Sub DoSaveGeoreferenced()
        If MapMain.NumLayers < 1 Then Return

        Dim newfilename As String
        Dim saveFileDialog1 As New SaveFileDialog

        saveFileDialog1.Filter = "All Image Types (*.bmp; *.gif; *.jpg)|*.bmp;*.gif;*.jpg|JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif"

        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        Dim image As New MapWinGIS.Image
        Dim success As Boolean
        '+ tws 04/06 2007:
        ' optionally clip the output to one of the layers in the map
        ' and size/zoom it to the user's preference
        '
        Dim dlgExport As New frmExport
        dlgExport.MainForm = Me
        dlgExport.sfd = saveFileDialog1
        Dim ok As DialogResult = dlgExport.ShowDialog()
        If ok <> Windows.Forms.DialogResult.OK Or dlgExport.newfilename Is Nothing Then
            Exit Sub
        End If
        newfilename = dlgExport.newfilename
        Dim oldMsg As String = modMain.g_error
        If dlgExport.SelectedLayer > -1 Then
            image = CType(MapMain.SnapShot2(dlgExport.SelectedLayer, dlgExport.ImageZoom, dlgExport.ImageWidth), MapWinGIS.Image)
        Else
            Dim extents As MapWinGIS.Extents = MapMain.Extents
            image = CType(MapMain.SnapShot(extents), MapWinGIS.Image)
        End If
        If image Is Nothing Then
            ' it looks like something SHOULD: implement MapWinGIS.ICallback,
            '    AND call CMap::SetGlobalCallback; and its .Error() should put the last error in 
            '    modmain.g_error; right now 04/26/2007 nothing actually does that (clsLayers implements the
            '    interface but nothing ever calls the method to set it). if that is done, and we have failed here, 
            '    we should have a new message from the code in the ocx to display here
            Dim errTxt As String = "Export failed"
            If Not oldMsg Is modMain.g_error Then
                errTxt &= vbCrLf & modMain.g_error
            End If
            MapWinUtility.Logger.Msg(errTxt, MsgBoxStyle.Critical)
            Me.RefreshMap() ' need to refresh, to be sure we cleaned up
            Return
        End If
        '- tws

        Dim configpath As String

        Dim deltax, deltay, xtopleft, ytopleft As Double

        If newfilename <> "" Then
            success = image.Save(newfilename, False, MapWinGIS.ImageType.USE_FILE_EXTENSION)
            'If the save wasn't successful display an error message
            If Not success Then
                '12/10/2005 PM:
                'mapwinutility.logger.msg("There were errors saving the image", MsgBoxStyle.Exclamation, "Could Not Save")
                MapWinUtility.Logger.Msg(resources.GetString("msgErrorSavingImage.Text"), MsgBoxStyle.Exclamation, resources.GetString("titleErrorSavingImage.Text"))
                Exit Sub
            End If

            'this bit creates the world file
            'Set Pathname to the text file:

            deltax = Math.Round(image.dX, 10)
            deltay = image.dY
            xtopleft = Math.Round(image.XllCenter, 6)
            ytopleft = Math.Round((image.YllCenter + Math.Abs((image.Height - 2) * deltay)), 6)
            deltay = Math.Round(deltay, 10)

            'Set the appropriate world file path
            Dim pathNoExt As String = System.IO.Path.GetDirectoryName(newfilename) + "\" + System.IO.Path.GetFileNameWithoutExtension(newfilename)
            Dim ext As String = "wld"
            Select Case System.IO.Path.GetExtension(newfilename).ToLower
                Case ".bmp"
                    ext = ".bpw"
                Case ".gif"
                    ext = ".gfw"
                Case ".jpg", ".jpeg"
                    ext = ".jgw"
            End Select

            configpath = pathNoExt + ext

            FileOpen(100, configpath, OpenMode.Output)

            PrintLine(100, deltax)
            PrintLine(100, "0")
            PrintLine(100, "0")
            PrintLine(100, "-" & deltay)
            PrintLine(100, xtopleft)
            PrintLine(100, ytopleft)

            FileClose(100)
        End If

        Try
            image.Close()
            image = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetLayerPrettyProjection(ByVal Handle As Long) As String
        If Not m_layers.IsValidHandle(Handle) Then Return ""

        Dim proj As String = ""

        If m_layers(Handle).LayerType = eLayerType.LineShapefile Or m_layers(Handle).LayerType = eLayerType.PointShapefile Or m_layers(Handle).LayerType = eLayerType.PolygonShapefile Then
            Try
                proj = CType(frmMain.Layers(Handle).GetObject(), MapWinGIS.Shapefile).Projection
            Catch
            End Try
        ElseIf m_layers(Handle).LayerType = eLayerType.Grid Then
            Try
                proj = CType(frmMain.Layers(Handle).GetGridObject(), MapWinGIS.Grid).Header.Projection
            Catch
            End Try
        ElseIf m_layers(Handle).LayerType = eLayerType.Image Then
            Try
                proj = CType(frmMain.Layers(Handle).GetObject(), MapWinGIS.Image).GetProjection()
            Catch
            End Try
        Else
            Return ""
        End If

        If proj = "" Then Return "(None)"
        Return proj

        ' TODO: implement some 'pretty' formatting

        'Dim projUtil As New clsProjections
        'proj = projUtil.FindProjectionByPROJ4(proj).Name + " (" + proj + ")"
        'Return proj
    End Function

    Friend Sub DoSetScale()
        If MapMain.NumLayers = 0 Then
            MapWinUtility.Logger.Msg("Please add data to the map before setting the scale.", MsgBoxStyle.Information, "Add Data First")
            Return
        End If

        Dim getscale As New frmSetScale(GetCurrentScale)
        If getscale.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Paul Meems June 1, 2010
            ' Added predefined scales, issue #933:
            If getscale.cboPredefinedScales.Text.StartsWith("[") Then
                SetScale(getscale.txtNewScale.Text)
            Else
                SetScale(getscale.cboPredefinedScales.Text)
            End If
        End If
    End Sub

    Public Function GetScaleUnit() As MapWindow.Interfaces.UnitOfMeasure
        Dim ScaleUnit As MapWindow.Interfaces.UnitOfMeasure

        'Try map units
        Select Case frmMain.Project.MapUnits
            Case MapWindow.Interfaces.UnitOfMeasure.Centimeters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Centimeters
            Case MapWindow.Interfaces.UnitOfMeasure.Feet.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Feet
            Case MapWindow.Interfaces.UnitOfMeasure.Inches.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Inches
            Case MapWindow.Interfaces.UnitOfMeasure.Kilometers.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Kilometers
            Case MapWindow.Interfaces.UnitOfMeasure.Meters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Meters
            Case MapWindow.Interfaces.UnitOfMeasure.Miles.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Miles
            Case MapWindow.Interfaces.UnitOfMeasure.Millimeters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Millimeters
            Case MapWindow.Interfaces.UnitOfMeasure.Yards.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Yards
            Case MapWindow.Interfaces.UnitOfMeasure.DecimalDegrees.ToString()
                'Disallow showing degrees as a measurement.
                ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
            Case "Lat/Long"
                ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
        End Select

        'Prefer alternate coordinate system, it set
        Select Case modMain.ProjInfo.ShowStatusBarCoords_Alternate
            Case MapWindow.Interfaces.UnitOfMeasure.Centimeters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Centimeters
            Case MapWindow.Interfaces.UnitOfMeasure.Feet.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Feet
            Case MapWindow.Interfaces.UnitOfMeasure.Inches.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Inches
            Case MapWindow.Interfaces.UnitOfMeasure.Kilometers.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Kilometers
            Case MapWindow.Interfaces.UnitOfMeasure.Meters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Meters
            Case MapWindow.Interfaces.UnitOfMeasure.Miles.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Miles
            Case MapWindow.Interfaces.UnitOfMeasure.Millimeters.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Millimeters
            Case MapWindow.Interfaces.UnitOfMeasure.Yards.ToString()
                ScaleUnit = Interfaces.UnitOfMeasure.Yards
            Case MapWindow.Interfaces.UnitOfMeasure.DecimalDegrees.ToString()
                'Disallow showing degrees as a measurement.
                ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
            Case "Lat/Long"
                ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
        End Select

        'Lastly, if the floating scale bar unit is set, use that
        If Not frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit = "" Then
            Select Case frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit
                Case MapWindow.Interfaces.UnitOfMeasure.Centimeters.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Centimeters
                Case MapWindow.Interfaces.UnitOfMeasure.Feet.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Feet
                Case MapWindow.Interfaces.UnitOfMeasure.Inches.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Inches
                Case MapWindow.Interfaces.UnitOfMeasure.Kilometers.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Kilometers
                Case MapWindow.Interfaces.UnitOfMeasure.Meters.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Meters
                Case MapWindow.Interfaces.UnitOfMeasure.Miles.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Miles
                Case MapWindow.Interfaces.UnitOfMeasure.Millimeters.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Millimeters
                Case MapWindow.Interfaces.UnitOfMeasure.Yards.ToString()
                    ScaleUnit = Interfaces.UnitOfMeasure.Yards
                Case MapWindow.Interfaces.UnitOfMeasure.DecimalDegrees.ToString()
                    'Disallow showing degrees as a measurement.
                    ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
                Case "Lat/Long"
                    ScaleUnit = MapWindow.Interfaces.UnitOfMeasure.Kilometers
            End Select
        End If
        Return ScaleUnit
    End Function

    Private Sub DoSaveScaleBarImage()
        If MapMain.NumLayers < 1 Then Return

        Dim newfilename As String
        ' Paul Meems 06/17/2009
        newfilename = getImagenameFromSaveDialog()
        If newfilename = "" Then
            Exit Sub
        End If

        If newfilename <> "" Then
            Dim sb As New ScaleBarUtility
            Dim img As Image

            Dim mapunit As UnitOfMeasure = UnitOfMeasure.Meters 'default

            If (Not modMain.frmMain.Project.MapUnits = "") Then
                Select Case modMain.frmMain.Project.MapUnits.ToLower()
                    Case "lat/long"
                        mapunit = UnitOfMeasure.DecimalDegrees
                    Case "meters"
                        mapunit = UnitOfMeasure.Meters
                    Case "centimeters"
                        mapunit = UnitOfMeasure.Centimeters
                    Case "feet"
                        mapunit = UnitOfMeasure.Feet
                    Case "inches"
                        mapunit = UnitOfMeasure.Inches
                    Case "kilometers"
                        mapunit = UnitOfMeasure.Kilometers
                    Case "meters"
                        mapunit = UnitOfMeasure.Meters
                    Case "miles"
                        mapunit = UnitOfMeasure.Miles
                    Case "millimeters"
                        mapunit = UnitOfMeasure.Millimeters
                    Case "yards"
                        mapunit = UnitOfMeasure.Yards
                End Select
            End If

            Dim sbunit As UnitOfMeasure = GetScaleUnit()
            If sbunit = UnitOfMeasure.DecimalDegrees Then sbunit = UnitOfMeasure.Meters

            img = sb.GenerateScaleBar(CType(frmMain.MapMain.Extents, MapWinGIS.Extents), mapunit, sbunit, 300, Color.White, Color.Black)

            ' Paul Meems 06/17/2009
            ' Bug #876: ScaleBar/NorthArrow/Legend export format 
            saveImageByName(newfilename, img)
            img.Dispose()

        End If
    End Sub

    ' Paul Meems 06/17/2009
    ' This part is used in all save image functions
    ' So I created a seperate function
    Private Function getImagenameFromSaveDialog() As String
        Dim saveFileDialog1 As New SaveFileDialog
        Dim retVal As String = ""

        saveFileDialog1.Filter = "Image Files(*.BMP;*.GIF;*.PNG;*.JPG)|*.bmp;*.gif;*.png;*.jpg"

        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            retVal = saveFileDialog1.FileName
        End If
        'Clean up:
        saveFileDialog1.Dispose()

        Return retVal

    End Function

    ' Paul Meems 06/17/2009
    ' Bug #876: ScaleBar/NorthArrow/Legend export format 
    ' The image is saved as bmp but it actually a png according to IrFanView
    ' So I added the ImageFormat:
    Private Sub saveImageByName(ByVal newfilename As String, ByVal image As System.Drawing.Bitmap)
        If newfilename <> "" Then
            Dim ext As String = System.IO.Path.GetExtension(newfilename).ToLower()
            If (ext.IndexOf("jpg") > -1) Then
                image.Save(newfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
            ElseIf (ext.IndexOf("png") > -1) Then
                image.Save(newfilename, System.Drawing.Imaging.ImageFormat.Png)
            ElseIf (ext.IndexOf("gif") > -1) Then
                image.Save(newfilename, System.Drawing.Imaging.ImageFormat.Gif)
            Else
                image.Save(newfilename, System.Drawing.Imaging.ImageFormat.Bmp)
            End If
        End If

    End Sub

    Private Sub doSaveNorthArrow()
        Dim newfilename As String
        ' Paul Meems 06/17/2009
        newfilename = getImagenameFromSaveDialog()
        If newfilename = "" Then
            Exit Sub
        End If

        Dim image As System.Drawing.Bitmap
        ' Paul Meems 06/17/2009
        ' Bug #1311: Exception on exporting North Arrow
        ' NorthArrow.ico wasn't an embedded resource file. I fixed that
        ' and changed to using the png, because that one is much prettier
        'image = New System.Drawing.Bitmap(Me.GetType, "NorthArrow.ico")
        image = New System.Drawing.Bitmap(Me.GetType, "NorthArrow.png")

        ' Paul Meems 06/17/2009
        ' Bug #876: ScaleBar/NorthArrow/Legend export format 
        saveImageByName(newfilename, image)

        ' Dispose of the image file.
        image.Dispose()

    End Sub

    Private Sub doCopyNorthArrow()
        Dim image As System.Drawing.Bitmap
        ' Paul Meems 06/17/2009
        ' Bug 877: North Arrow export colours 
        ' Use png instead of ico file:
        'image = New System.Drawing.Bitmap(Me.GetType, "NorthArrow.ico")
        image = New System.Drawing.Bitmap(Me.GetType, "NorthArrow.png")
        ' Paul Meems 06/17/2009
        ' Bug#1315: copy map, legend, scalebar and north arrow doesn't work 
        'Works for IrFanView, WordPad, but not OpenOffice (Bug #912). Perhaps not a bug of MapWindow?
        Clipboard.SetDataObject(image)
    End Sub

    Private Sub doCopyScaleBar()
        Dim sb As New ScaleBarUtility
        Dim img As Image

        Dim mapunit As UnitOfMeasure = UnitOfMeasure.Meters 'default

        If (Not modMain.frmMain.Project.MapUnits = "") Then
            Select Case modMain.frmMain.Project.MapUnits.ToLower()
                Case "lat/long"
                    mapunit = UnitOfMeasure.DecimalDegrees
                Case "meters"
                    mapunit = UnitOfMeasure.Meters
                Case "centimeters"
                    mapunit = UnitOfMeasure.Centimeters
                Case "feet"
                    mapunit = UnitOfMeasure.Feet
                Case "inches"
                    mapunit = UnitOfMeasure.Inches
                Case "kilometers"
                    mapunit = UnitOfMeasure.Kilometers
                Case "meters"
                    mapunit = UnitOfMeasure.Meters
                Case "miles"
                    mapunit = UnitOfMeasure.Miles
                Case "millimeters"
                    mapunit = UnitOfMeasure.Millimeters
                Case "yards"
                    mapunit = UnitOfMeasure.Yards
            End Select
        End If

        Dim sbunit As UnitOfMeasure = GetScaleUnit()
        If sbunit = UnitOfMeasure.DecimalDegrees Then sbunit = UnitOfMeasure.Meters

        img = sb.GenerateScaleBar(CType(frmMain.MapMain.Extents, MapWinGIS.Extents), mapunit, sbunit, 300, Color.White, Color.Black)

        Clipboard.SetDataObject(img)
    End Sub

    Private Sub DoSaveMapImage()
        If MapMain.NumLayers < 1 Then Return

        Dim newfilename As String
        ' Paul Meems 06/17/2009
        newfilename = getImagenameFromSaveDialog()
        If newfilename = "" Then
            Exit Sub
        End If

        Dim image As New MapWinGIS.Image
        Dim success As Boolean
        image = CType(MapMain.SnapShot(MapMain.Extents), MapWinGIS.Image)

        success = image.Save(newfilename, False, MapWinGIS.ImageType.USE_FILE_EXTENSION)
        'If the save wasn't successful display an error message
        If Not success Then
            MapWinUtility.Logger.Msg("There were errors saving the image.", MsgBoxStyle.Exclamation, "Could Not Save")
            Exit Sub
        End If

        Try
            image.Close()
            image = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DoCopyMap()
        If MapMain.NumLayers < 1 Then Return

        'Copies the current map view to the clipboard
        Dim cvter As New MapWinUtility.ImageUtils
        Clipboard.SetDataObject(cvter.IPictureDispToImage(CType(MapMain.SnapShot(MapMain.Extents), MapWinGIS.Image).Picture))
    End Sub

    Private Sub DoCopyLegend()
        If MapMain.NumLayers < 1 Then Return

        'Copies the current legend to the clipboard
        Clipboard.SetDataObject(Legend.Snapshot(True, Legend.Width))
        MapMain.Focus()
        Legend.Focus()
    End Sub

    Private Sub DoSaveLegend()

        If MapMain.NumLayers < 1 Then Return

        Dim newfilename As String
        ' Paul Meems 06/17/2009
        newfilename = getImagenameFromSaveDialog()
        If newfilename = "" Then
            Exit Sub
        End If

        'Legend.Snapshot(True, Legend.Width).Save(newfilename)
        ' Paul Meems 06/17/2009
        ' Bug #876: ScaleBar/NorthArrow/Legend export format 
        Dim img As System.Drawing.Bitmap = Legend.Snapshot(True, Legend.Width)
        saveImageByName(newfilename, img)
        img.Dispose()

        MapMain.Focus()
        Legend.Focus()
    End Sub

    Friend Sub doClose()
        'Closes the current project
        If Not m_HasBeenSaved Or ProjInfo.Modified Then
            If PromptToSaveProject() = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        ProjInfo.ProjectFileName = ""
        frmMain.Layers.Clear()
        frmMain.Legend.Groups.Clear()
        ProjInfo.BookmarkedViews.Clear()
        frmMain.BuildBookmarkedViewsMenu()
        ClearPreview()
        m_AutoVis = New DynamicVisibilityClass
        ResetViewState()
        m_HasBeenSaved = True
        SetModified(False)
    End Sub

    Private Sub doExit()
        'Exit MapWindow
        'save the current configuration
        '---Cho 1/9/2009: MapWindowForm_Closing will take care of this. If we have the following code
        '---here, the user will see the closing dialog twice if he/she answers No.
        'ProjInfo.SaveConfig()
        'If Not m_HasBeenSaved Or ProjInfo.Modified Then
        '    If PromptToSaveProject() = MsgBoxResult.Cancel Then
        '        Exit Sub
        '    End If
        'End If

        Me.Close()
    End Sub

    Private Sub doEditPlugins()
        'Show the plugin manager form
        m_PluginManager.ShowPluginDialog()
    End Sub

    Friend Sub ResetViewState(Optional ByVal LeaveFloatingScalebar As Boolean = False)
        If Not LeaveFloatingScalebar And m_FloatingScalebar_Enabled Then
            m_FloatingScalebar_Enabled = False
            UpdateFloatingScalebar()
        End If

        If AppInfo.MeasuringCurrently Then AppInfo.MeasuringStop()
        If AppInfo.AreaMeasuringCurrently Then AppInfo.AreaMeasuringStop()

        frmMain.MapMain.UDCursorHandle = -1
        frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        frmMain.MapMain.MapCursor = MapWinGIS.tkCursor.crsrArrow
        frmMain.UpdateButtons()
    End Sub

    Private Sub doZoomIn()
        MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn
        UpdateButtons()
    End Sub

    Private Sub doZoomOut()
        MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut
        UpdateButtons()
    End Sub

    Private Sub doPreviousZoom()
        If AppInfo.MeasuringCurrently Then AppInfo.MeasuringStop()
        MapMain.ZoomToPrev()
        SetModified(True)
    End Sub

    Private Sub doNextZoom()
        If AppInfo.MeasuringCurrently Then AppInfo.MeasuringStop()
        'todo: add this
    End Sub

    Public Function PreviewMapExtentsValid() As Boolean
        ' PM, 18 jan. 2011, Added check if previewPanel is created:
        If frmMain.previewPanel Is Nothing Then Return True

        Dim ext As MapWinGIS.Extents = MapPreview.Extents
        ' May/17/2010 === DK: The following line cannot catch the invalid map extents
        'If ext.xMin = 0 And ext.xMax = 0 And ext.yMin = 0 And ext.yMax = 0 Then Return False

        If (ext.xMin = -0.5 And ext.xMax = 0.5) _
        Or (ext.yMin = -0.5 And ext.yMax = 0.5) Then
            Return False
        End If

        Return True
    End Function

    Private Sub doZoomToPreview()
        ' PM, 18 jan. 2011, Added check if previewPanel is created:
        If frmMain.previewPanel Is Nothing Then Return

        'Dim ext As MapWinGIS.Extents = MapPreview.Extents
        If Not PreviewMapExtentsValid() Then
            MapWinUtility.Logger.Msg("The preview map has not been set." + vbCrLf + vbCrLf + "A preview map may be set up by right-clicking in the Preview Map window and choosing one of the Update options.", MsgBoxStyle.Information, "No Preview Map Set")
        Else
            If AppInfo.MeasuringCurrently Then AppInfo.MeasuringStop()
            MapMain.Extents = MapPreview.Extents
            SetModified(True)
        End If
    End Sub

    Private Sub doZoomToFullExtents()
        MapMain.ZoomToMaxExtents()
        SetModified(True)
        If AppInfo.MeasuringCurrently Then AppInfo.MeasuringStop()
    End Sub

    Private Sub doClearPreview()
        'Clear the preview map
        ClearPreview()
        SetModified(True)
    End Sub

    Private Sub doUpdatePreview(Optional ByVal FullExtents As Boolean = False)
        'Updates the preview map
        m_PreviewMap.GetPictureFromMap(FullExtents)
        SetModified(True)
    End Sub

    Private Sub DoProjectSettings()
        'Use the property grid instead of the old dialog to do project settings. 
        Dim dlg As ProjectSettings = New ProjectSettings
        dlg.ShowDialog()
    End Sub

    Private Sub doPluginNameClick(ByVal PluginKey As String)
        'This sub is called when the user clicks a menu item that was placed on the plugins menu during "SynchPluginMenu"
        '1/25/2005 - dpa
        '3/16/2005 - updated to work off a runtime created plugin parent menu

        If m_PluginManager.PluginIsLoaded(PluginKey) Then
            m_PluginManager.StopPlugin(PluginKey)
            m_Menu("plugin_" & PluginKey).Checked = False
            m_Menu("plugin_" & PluginKey).Picture = GlobalResource.imgPluginDisabled
        Else
            m_PluginManager.StartPlugin(PluginKey)
            m_Menu("plugin_" & PluginKey).Checked = True
            m_Menu("plugin_" & PluginKey).Picture = GlobalResource.imgPlugin
        End If

        'Bugzilla 380 -- Plugins are stored in the project, so a plugin change
        'should set the modified flag.
        SetModified(True)
    End Sub

    Private Sub doContents()
        'Show the help file 
        '1/25/2005 - dpa
        If (System.IO.File.Exists(AppInfo.HelpFilePath)) Then
            System.Diagnostics.Process.Start(AppInfo.HelpFilePath)
        Else
            '7/31/2006 PM
            'mapwinutility.logger.msg("Help file does not exist.", MsgBoxStyle.Exclamation, "Missing help file")
            MapWinUtility.Logger.Msg(resources.GetString("msgHelpfileDoesNotExist"), MsgBoxStyle.Exclamation, "Missing help file")
        End If
    End Sub

    Private Sub doAboutMapWindow()
        'Shows the "about" dialog 
        '1/25/2005 - dpa
        Dim about As New frmAbout
        about.ShowAbout(Me)
    End Sub

    ' Care of Sphextor, MapWindow Phorums: http://www.MapWindow.org/phorum/read.php?3,318,3320#msg-3320
    Public Function GetCurrentScale() As String
        ' Paul Meems - August 17 2010
        ' Using new (faster) method:
        Return View.Scale.ToString(CultureInfo.InvariantCulture)
        'Return ExtentsToScale(MapMain.Extents).ToString(CultureInfo.InvariantCulture)
    End Function

    Public Function ExtentsToScale(ByVal ext As MapWinGIS.Extents) As Double
        ' Paul Meems - August 17 2010
        ' Using new (faster) method:
        Return View.Scale
        'Return MapWinGeoProc.ScaleTools.calcScale(ext, Project.MapUnits, MapMain.Width, MapMain.Height)
    End Function

    ' Care of Ted Dunsford of MW team, Shade1974 of MapWindow Phorums: http://www.MapWindow.org/phorum/read.php?3,318,3320#msg-3320
    Private Sub SetScale(ByVal NewScale As String)
        If Not IsNumeric(NewScale) Then Exit Sub
        ' Paul Meems - August 17 2010
        ' Using new (faster) method:
        View.Scale = Double.Parse(NewScale, CultureInfo.InvariantCulture)
        'MapMain.Extents = ScaleToExtents(Double.Parse(NewScale, CultureInfo.InvariantCulture), MapMain.Extents)
    End Sub

    Public Function ScaleToExtents(ByVal scale As Double, ByVal ext As MapWinGIS.Extents) As MapWinGIS.Extents
        Dim pt As MapWinGIS.Point = New MapWinGIS.Point()
        pt.x = (ext.xMin + ext.xMax) / 2
        pt.y = (ext.yMin + ext.yMax) / 2

        ' Paul Meems, 9 October 2009
        ' If Scale is 0 or no projection is available don't scale
        Dim newExtents As New MapWinGIS.Extents
        If scale = 0 OrElse Project.MapUnits = "" Then
            newExtents.SetBounds(pt.x, pt.y, 0, pt.x, pt.y, 0)
        Else
            newExtents = MapWinGeoProc.ScaleTools.ExtentFromScale(Convert.ToInt32(scale), pt, Project.MapUnits, MapMain.Width, MapMain.Height)
        End If
        Return newExtents
    End Function

    Private Sub doMapWindowDotCom()
        'Shows MapWindow.org
        '1/25/2005 (dpa)
        Try
            System.Diagnostics.Process.Start("http://www.MapWindow.org")
        Catch ex As System.Exception
            ShowError(ex)
        End Try
    End Sub

    Friend Sub SynchPluginMenu()
        'Clears the list of plug-ins from the plugins menu and then refreshes them.
        '1/25/2005 - dpa - updated
        '3/16/2005 - dpa - changed to work on run-time created plug-in parent menu.
        '10/04/2008 - Earljon Hidalgo - Added dynamic loading of plugin icons
        If Not g_SyncPluginMenuDefer Then
            MapWinUtility.Logger.Dbg("SyncStart")
            Dim ParentMenu As Windows.Forms.ToolStripMenuItem
            Dim ChildMenu As MapWindow.Interfaces.MenuItem
            Dim MenuKey As String
            Dim i As Integer = 0

            ' Paul Meems 16 sept 2009, Bug 1412 
            ' It is possible the plugin menu is removed by a plug-in. Adding check else a null pointer is thrown:
            If Not frmMain.m_Menu.m_MenuTable.ContainsKey(frmMain.m_Menu.MenuTableKey("mnuPlugins")) Then
                MapWinUtility.Logger.Dbg("mnuPlugins is not available. Leaving SynchPluginMenu")
                Return
            End If
            ParentMenu = CType(m_Menu.m_MenuTable("mnuPlugins"), Windows.Forms.ToolStripMenuItem)

            Dim alph_PluginList As Hashtable = m_PluginManager.PluginsList
            Dim Names() As String, Keys() As String
            i = 0
            ReDim Names(alph_PluginList.Count - 1)
            ReDim Keys(alph_PluginList.Count - 1)
            Dim ienum As IDictionaryEnumerator = alph_PluginList.GetEnumerator()
            While ienum.MoveNext
                Names(i) = ienum.Value.name
                Keys(i) = ienum.Value.key
                i += 1
            End While

            ' 2 - Sort using a custom IComparer
            MapWinUtility.Logger.Dbg("SyncSort")
            Names.Sort(Names, Keys)
            MapWinUtility.Logger.Dbg("Number of plugins in the list: " + Names.Length.ToString())

            ' 3 - Now add the plugin menu items at the end of the menu, using the sorted arraylist
            MapWinUtility.Logger.Dbg("SyncAdd")
            For i = 0 To Names.Length - 1
                MenuKey = "plugin_" & Keys(i)

                ' Chris Michaelis June 30 2005 - allow a plug-in
                ' to specify that it belongs in a submenu of the plugins menu
                ' via the syntax "Subcategory::Plugin Name" as the plugin name string.
                ' Chris Michaelis May 2008 - allow multiple levels of menus

                ' Earljon Hidalgo Oct 04 2008 - Added dynamic icon menu loading
                ' based on the state of plugin (enabled, disabled and/or belongs to
                ' a submenu
                Try
                    Dim WorkingName As String = Names(i)
                    Dim subCat As String = ""
                    Dim LastMenu As String = "mnuPlugins"
                    Dim oPicture As New Object
                    Dim bPluginState As Boolean
                    While InStr(WorkingName, "::") > 0
                        subCat = "subcat_" + WorkingName.Substring(0, InStr(WorkingName, "::"))
                        oPicture = GlobalResource.imgPluginSub
                        If Not m_Menu.Contains(subCat) Then m_Menu.AddMenu(subCat, LastMenu, oPicture, WorkingName.Substring(0, InStr(WorkingName, "::") - 1))
                        LastMenu = subCat
                        'Move to next segment (or final segment wo/ ::)
                        WorkingName = WorkingName.Substring(InStr(WorkingName, "::") + 1)
                    End While

                    bPluginState = m_PluginManager.PluginIsLoaded(Keys(i))
                    ' For Bug #1124
                    If bPluginState Then MapWinUtility.Logger.Dbg(String.Format("Plugin {0} is loaded", Keys(i)))

                    oPicture = IIf(bPluginState = True, GlobalResource.imgPlugin, GlobalResource.imgPluginDisabled)
                    If Not subCat = "" Then
                        ChildMenu = m_Menu.AddMenu(MenuKey, subCat, oPicture, WorkingName)
                    Else
                        'There were no submenus requested
                        ChildMenu = m_Menu.AddMenu(MenuKey, "mnuPlugins", oPicture, Names(i))
                    End If

                    ChildMenu.Checked = bPluginState
                    ChildMenu.Picture = oPicture

                Catch ex As Exception
                    MapWinUtility.Logger.Msg(ex.ToString())
                End Try
            Next
            MapWinUtility.Logger.Dbg("SyncDone")

            MapWinUtility.Logger.Dbg("Ensuring Help menu is last...")
            m_Menu.EnsureHelpItemLast()
        End If
    End Sub

    Private Sub PreviewMapContextMenuStrip_UpdatePreview(ByVal sender As Object, ByVal e As System.EventArgs)
        doUpdatePreview()
    End Sub

    Private Sub PreviewMapContextMenuStrip_UpdatePreviewFull(ByVal sender As Object, ByVal e As System.EventArgs)
        doUpdatePreview(True)
    End Sub

    Private Sub PreviewMapContextMenuStrip_ClearPreview(ByVal sender As Object, ByVal e As System.EventArgs)
        doClearPreview()
    End Sub

    Friend Sub CustomMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'This sub is called when the user clicks a menu item that was placed on the 
        'main menu by a plugin
        '1/30/2005 - dpa - updated
        '3/16/2005 - dpa - using this event for regular menu clicks as well now (e.g. file/new)

        Dim item As ToolStripItem = CType(sender, ToolStripItem)
        HandleClickedMenu(item.Name)
    End Sub

    Public Sub HandleClickedMenu(ByVal MenuName As String)
        'First see if it is a plugin name menu
        If MenuName.StartsWith("plugin_") = True Then
            doPluginNameClick(MenuName.Substring(7))
            Exit Sub
        End If

        'send the click event to all the plugins
        If Not (m_PluginManager.ItemClicked(MenuName)) Then

            'If we get here, then the menu click event was not handled by a plug-in 
            'so we will try to handle it here.  For example in the case of File/New the
            'plugin could handle this click, if so then we don't get to this point.
            'If no plugin handles File/New, then we'll do it.

            Select Case MenuName
                'help menus - do these first so that the logic about keeping help at the
                'end of the menu list works.
                Case "mnuDocs"
                    System.Diagnostics.Process.Start(App.Path & "\LocalResources\MapWindow486Help.pdf")
                Case "mnuOnlineDocs"
                    System.Diagnostics.Process.Start("http://www.mapwindow.org/apps/wiki/doku.php")
                Case "mnuTutorials"
                    System.Diagnostics.Process.Start("http://www.mapwindow.org/apps/wiki/doku.php?id=getting_started")
                Case "mnuBugReport"
                    System.Diagnostics.Process.Start("http://bugs.mapwindow.org")
                Case "mnuDonate"
                    System.Diagnostics.Process.Start("http://www.mapwindow.org/pages/donate.php")
                Case "mnuOfflineDocs"
                    System.Diagnostics.Process.Start(BinFolder & "\OfflineDocs\index.html")
                Case "mnuContents" : doContents()
                Case "mnuWelcomeScreen" : ShowWelcomeScreen()
                Case "mnuAboutMapWindow" : doAboutMapWindow()
                Case "mnuMapWindowDotCom" : doMapWindowDotCom()
                Case "mnuLegendVisible" : UpdateLegendPanel(Not m_Menu.Item("mnuLegendVisible").Checked)
                Case "mnuPreviewVisible" : UpdatePreviewPanel(Not m_Menu.Item("mnuPreviewVisible").Checked)
                Case "mnuNew" : DoNew()
                Case "mnuOpen" : DoOpen()
                Case "mnuOpenProjectIntoGroup" : DoOpenIntoCurrent()
                Case "mnuSave" : DoSave()
                Case "mnuSaveAs" : DoSaveAs()
                Case "mnuPrint" : DoPrint()
                Case "mnuProjectSettings" : DoProjectSettings()
                Case "mnuClose" : doClose()
                Case "mnuCheckForUpdates" : CheckForUpdates()
                Case "mnuExit" : doExit()

                    'edit menus
                Case "mnuCopyMap" : DoCopyMap()
                Case "mnuCopyLegend" : DoCopyLegend()
                Case "mnuSaveLegend" : DoSaveLegend()

                Case "mnuCopyScaleBar" : doCopyScaleBar()
                Case "mnuCopyNorthArrow" : doCopyNorthArrow()
                Case "mnuSaveMapImage" : DoSaveMapImage()
                Case "mnuSaveNorthArrow" : doSaveNorthArrow()
                Case "mnuSaveScaleBar" : DoSaveScaleBarImage()

                Case "mnuSaveGeorefMapImage" : DoSaveGeoreferenced()
                Case "mnuUpdatePreviewFull" : doUpdatePreview(True)
                Case "mnuUpdatePreviewCurr" : doUpdatePreview()
                Case "mnuClearPreview" : doClearPreview()

                    'view menus
                Case "mnuAddLayer" : DoAddLayer()
                Case "mnuAddECWPLayer" : DoAddECWPLayer()
                Case "mnuRemoveLayer" : DoRemoveLayer()
                Case "mnuClearLayers" : DoClearLayers()
                Case "mnuClearSelectedShapes" : DoClearLayerSelection()
                Case "mnuShowScaleBar" : DoToggleScalebar()
                Case "mnuSetScale" : DoSetScale()

                    'CDM 4/7/2006 - Also catch the one on the View menu. It has been
                    'renamed to properly say "Clear LayerS", plural, that is, note the S on the end.
                Case "mnuClearLayer" : DoClearLayers()

                Case "mnuZoomToPreviewExtents" : doZoomToPreview()
                Case "mnuZoomPreviewMap" : doZoomToPreview()
                Case "mnuZoomIn" : doZoomIn()
                Case "mnuZoomOut" : doZoomOut()
                Case "mnuZoomToFullExtents" : doZoomToFullExtents()
                Case "mnuPreviousZoom" : doPreviousZoom()
                Case "mnuNextZoom" : doNextZoom()

                Case "mnuClearAllSelection" : DoClearSelection()

                    'plugins menus
                Case "mnuEditPlugins" : doEditPlugins()

                Case "mnuBookmarkView"
                    'Chris Michaelis May 14 2007

                    'TODO These strings will need to be localized eventually by those
                    'who speak all of the localized languages.

                    'Christian Degrassi 2010-03-09: This fixes part of enhancement 1638
                    Dim AddBMDialog As frmBookmarksAddNew = Nothing
                    Dim newName As String = String.Empty
                    Dim newExtents As MapWinGIS.Extents = Nothing

                    newName = "Bookmark " + (ProjInfo.BookmarkedViews.Count + 1).ToString()
                    AddBMDialog = New frmBookmarksAddNew(newName, MapMain.Extents)
                    If AddBMDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

                        newName = AddBMDialog.BookmarkName
                        newExtents = AddBMDialog.BookmarkExtents

                        If Not newName.Trim() = "" Then
                            ProjInfo.BookmarkedViews.Add(New XmlProjectFile.BookmarkedView(newName, newExtents))
                            modMain.frmMain.SetModified(True)
                            BuildBookmarkedViewsMenu()
                        End If
                    End If

                    'Christian Degrassi 2010-03-09: Obsolete due to Enhancement 1639
                    'Case "mnuBookmarkDelete"
                    ''Chris Michaelis May 14 2007

                    ''Note that project modified flag is set on actual deletion in the form.
                    'If ProjInfo.BookmarkedViews.Count > 0 Then
                    '    Dim delform As New frmBookmarkedViewDelete()
                    '    delform.ShowDialog()
                    '    BuildBookmarkedViewsMenu()
                    'Else
                    '    MapWinUtility.Logger.Msg("There are no bookmarked views to delete.", MsgBoxStyle.Information, "No Bookmarked Views")
                    'End If

                Case "mnuBookmarkedViews"
                    'No action needed

                Case "mnuBookmarksManager"

                    Dim BookmarksManagerDialog As frmBookmarkManager = Nothing

                    BookmarksManagerDialog = New frmBookmarkManager(modMain.ProjInfo.BookmarkedViews)
                    BookmarksManagerDialog.ShowDialog()

                    modMain.frmMain.SetModified(BookmarksManagerDialog.IsModified)

                    BuildBookmarkedViewsMenu()

                Case "mnuLayersAddNew"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    DoAddLayer()
                Case "mnuLayerLabels"
                    DoLabelsEdit(Legend.SelectedLayer)
                Case "mnuLayerRelabel"
                    DoLabelsRelabel(Legend.SelectedLayer)
                Case "mnuLayerCharts"
                    DoChartsEdit(Legend.SelectedLayer)
                Case "mnuLayerAttributeTable"
                    Plugins.BroadcastMessage("TableEditorStart")
                Case "mnuLayerCategories"
                    DoEditCategories()
                Case "mnuOptionsManager"
                    DoSymbologyManager()
                Case "mnuLayerProperties"
                    ShowLayerProperties(Legend.SelectedLayer)
                Case "mnuQueryLayer"
                    DoQueryShapefile()
                Case "mnuLayersManager"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Layers Manager ...")
                Case "mnuLegendEditor"
                    If Me.m_legendEditor Is Nothing Then
                        If Legend.Layers.IsValidHandle(Legend.SelectedLayer) Then
                            If (m_legendEditor Is Nothing) Then
                                m_legendEditor = LegendEditorForm.CreateAndShowLYR(Legend.SelectedLayer)
                            End If
                            m_legendEditor.LoadProperties(Legend.SelectedLayer, True)
                        Else
                            If (Legend.Groups.IsValidHandle(m_GroupHandle)) Then
                                ShowGroupProperties(m_GroupHandle)
                            Else
                                m_legendEditor = LegendEditorForm.CreateAndShowLYR(Legend.SelectedLayer)
                            End If
                        End If
                    Else
                        Me.m_legendEditor.Close()
                        Me.m_legendEditor = Nothing
                    End If
                    UpdateButtons()
                Case "mnuLayersCurrentRemove"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Current Layer Remove ...")
                    DoRemoveLayer()

                Case "mnuLayersCurrentProperties"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Current Layer Properties ...")
                    ShowLayerProperties(Layers.CurrentLayer)

                Case "mnuLayersCurrentReLoadImage"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Reloading as Image ...")
                    Dim CurrentImageLayer As Layer
                    Dim CurrentImage As MapWinGIS.Image
                    Dim CurrentGrid As MapWinGIS.Grid
                    CurrentImageLayer = Me.Layers(Legend.SelectedLayer)
                    If (TypeOf (CurrentImageLayer.GetGridObject) Is MapWinGIS.Grid) Then
                        Debug.Print("... Confirming Grid Type")
                        CurrentGrid = CurrentImageLayer.GetGridObject
                        CurrentImage = New MapWinGIS.Image
                        CurrentImage.Open(CurrentGrid.Filename, MapWinGIS.ImageType.USE_FILE_EXTENSION, True, Nothing)
                        Layers.Add(CurrentImage, CurrentImageLayer.Name)
                    End If

                Case "mnuLayersCurrentReLoadGrid"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Reloading as Grid ...")
                    Dim CurrentImageLayer As Layer
                    Dim CurrentImage As MapWinGIS.Image
                    Dim CurrentGrid As MapWinGIS.Grid
                    CurrentImageLayer = Me.Layers(Legend.SelectedLayer)
                    If (TypeOf (CurrentImageLayer.GetObject) Is MapWinGIS.Image) Then
                        Debug.Print("... Confirming Image Type")
                        CurrentImage = CurrentImageLayer.GetObject
                        CurrentGrid = New MapWinGIS.Grid
                        CurrentGrid.Open(CurrentImage.Filename, MapWinGIS.GridDataType.UnknownDataType, True, MapWinGIS.GridFileType.UseExtension, Nothing)
                        Layers.Add(CurrentGrid, CurrentImageLayer.Name)
                    End If

                Case "mnuLayersCurrentAttributeTable"
                    'Christian Degrassi 2010-03-22: Enhancement 1651
                    Debug.Print("Attribute Table ...")
                    Plugins.BroadcastMessage("TableEditorStart")

                Case "mnuScript"
                    'Chris Michaelis Jan 1 2006 - Adapted from the script system written
                    'by Mark Gray of AquaTerra.
                    If Scripts Is Nothing Or Scripts.IsDisposed Then
                        Scripts = New frmScript
                        'Paul Meems 3 aug 2008 Added:
                        Scripts.Owner = Me
                    End If
                    'Paul Meems 28 sept 2009 Added:
                    Scripts.Icon = Me.Icon
                    Scripts.Show()

                Case "mnuShortcuts"
                    '7/31/2006 PM
                    'mapwinutility.logger.msg("The following keyboard shortcuts are available:" + vbCrLf + vbCrLf + _
                    '    "Del - Remove the currently selected layer." + vbCrLf + _
                    '    "Ins - Add a layer." + vbCrLf + vbCrLf + _
                    '    "Ctrl-S - Save the project." + vbCrLf + _
                    '    "Ctrl-O - Open a project." + vbCrLf + _
                    '    "Ctrl-C - Copy a map snapshot to the clipboard." + vbCrLf + _
                    '    "Ctrl-P - Open the Print Preview window." + vbCrLf + _
                    '    "Ctrl-F4 - Close the current project." + vbCrLf + vbCrLf + _
                    '    "Home - Zoom to Full Extents" + vbCrLf + _
                    '    "Ctrl-Home - Zoom to Selected Layer" + vbCrLf + _
                    '    "Plus - Zoom in on center of map, 25%" + vbCrLf + _
                    '    "Minus - Zoom out on center of map, 25%" + vbCrLf + vbCrLf + _
                    '    "Page-Up - Pan Up (50% of View)" + vbCrLf + _
                    '    "Page-Down - Pan Down (50% of View)" + vbCrLf + _
                    '    "Up Arrow - Pan Up (25% of View)" + vbCrLf + _
                    '    "Down Arrow - Pan Down (25% of View)" + vbCrLf + _
                    '    "Left Arrow - Pan Left (25% of View)" + vbCrLf + _
                    '    "Right Arrow - Pan Right (25% of View)", _
                    '    MsgBoxStyle.Information, AppInfo.Name)
                    Dim strMessage As String
                    strMessage = resources.GetString("msgShortcutsTitle.Text") + vbCrLf + vbCrLf + _
                                 resources.GetString("msgShortcutsDel.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsIns.Text") + vbCrLf + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlS.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlO.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlC.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlP.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlI.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlH.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlF4.Text") + vbCrLf + vbCrLf + _
                                 resources.GetString("msgShortcutsHome.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlHome.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsPlus.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsMinus.Text") + vbCrLf + vbCrLf + _
                                 resources.GetString("msgShortcutsPageUp.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsPageDown.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsArrowUp.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsArrowDown.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsArrowLeft.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsArrowRight.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlShiftI.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlShiftO.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlShiftP.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlSpace.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlArrows.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsCtrlEnter.Text") + vbCrLf + _
                                 resources.GetString("msgShortcutsBackspace.Text")

                    doNonModalMessageBox(strMessage, MsgBoxStyle.Information, "Keyboard Shortcuts")
                Case Else
                    If MenuName.StartsWith(BookmarkedViewPrefix) Then
                        'Zoom to this view
                        Dim sViewNumber As String = MenuName.Replace(BookmarkedViewPrefix, "")
                        Dim iViewNumber As Integer = -1
                        If Integer.TryParse(sViewNumber, iViewNumber) AndAlso Not iViewNumber = -1 Then
                            MapMain.Extents = ProjInfo.BookmarkedViews(iViewNumber).Exts
                            SetModified(True)
                        Else
                            MapWinUtility.Logger.Msg("The bookmarked view was not recognized.", MsgBoxStyle.Exclamation, "Unable to Find Bookmark")
                        End If

                    ElseIf MenuName.StartsWith(RecentProjectPrefix) Then
                        'Load a recent project

                        'Chris Michaelis, Bugzilla 319
                        If Not m_HasBeenSaved Or ProjInfo.Modified Then
                            If PromptToSaveProject() = MsgBoxResult.Cancel Then
                                Exit Sub
                            End If
                        End If

                        'Chris Michaelis June 30 2005, also see BuildRecentProjectsMenu
                        If Not Project.Load(MenuName.Substring(RecentProjectPrefix.Length).Replace("{32}", " ")) Then
                            MapWinUtility.Logger.Msg("Could not load " & MenuName.Substring(RecentProjectPrefix.Length), MsgBoxStyle.OkOnly, "Recent Project")
                        End If
                    End If
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Clears selection of a single layer
    ''' </summary>
    Private Sub DoClearLayerSelection()
        Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(Me.Legend.SelectedLayer)
        If Not layer Is Nothing Then
            layer.ClearSelection()
            Dim handled As Boolean
            Me.View.Redraw()
            UpdateButtons()
            Me.FireLayerSelectionChanged(Me.Legend.SelectedLayer, handled)
        End If
    End Sub

    ''' <summary>
    ''' Clears selection from all layer in the project
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoClearSelection()
        ' 28-nov-2010
        Me.View.ClearSelectedShapes()
        Me.View.Redraw()

        For i As Integer = 0 To frmMain.Layers.NumLayers - 1
            Dim handled As Boolean
            Me.FireLayerSelectionChanged(Me.Layers.GetHandle(i), handled)
        Next
        'frmMain.Menus("mnuClearSelectedShapes").Enabled = False
    End Sub

    Private Sub mnuBtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBtnAdd.Click
        mnuBtnAdd.Checked = True
        mnuBtnRemove.Checked = False
        mnuBtnClear.Checked = False
        'tlbStandard.Items(5).Image = GlobalResource.mnuLayerAdd 'Add layer icon
        DoAddLayer()
    End Sub

    Private Sub mnuBtnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBtnRemove.Click
        mnuBtnAdd.Checked = False
        mnuBtnRemove.Checked = True
        mnuBtnClear.Checked = False
        'Used to change the picture on the dropdown to indicate the last used
        'tlbStandard.Items(5).Image = GlobalResource.mnuLayerRemove ' "remove layer" picture
        DoRemoveLayer()
    End Sub

    Private Sub mnuBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBtnClear.Click
        mnuBtnAdd.Checked = False
        mnuBtnRemove.Checked = False
        mnuBtnClear.Checked = True
        'Used to change the picture on the dropdown to indicate the last used
        'tlbStandard.Items(5).Image = GlobalResource.mnuLayerClear ' "clear layers" picture
        DoClearLayers()
    End Sub

#End Region

#Region "General Functions"

    Friend Sub ClearPreview()

        'Paul Meems, 21 Februari: added check for MapPreview
        If Not previewPanel.IsDisposed Then
            frmMain.MapPreview.ClearDrawings()
            frmMain.MapPreview.RemoveAllLayers()
            UpdateButtons()
        End If

    End Sub



    'Prints a simple layout
    'This is called by a menu click and a button click.
    Private Sub DoPrint()
        If MapMain.NumLayers = 0 Then
            MsgBox("Please add data to the map before printing.", MsgBoxStyle.Information, "Add Data First")
            Return
        End If

        ' Will be handled by the layout plugin now
        Dim Handled As Boolean = m_PluginManager.ItemClicked("tbbPrint")
        If Not Handled Then
            Dim printForm As New frmPrintSidebarLayout
            printForm.ShowDialog()
        End If
    End Sub

    Friend Sub DoNew(Optional ByVal fromOpen As Boolean = False)

        gemdb = Nothing
        memoryShape = Nothing

        If Not m_HasBeenSaved Or ProjInfo.Modified Then
            If PromptToSaveProject() = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        ProjInfo.m_MapUnits = "" 'reset map data units. 22/2/2008 by Jiri Kadlec for bug 680
        ProjInfo.ProjectFileName = ""
        modMain.frmMain.Project.GeoProjection = New MapWinGIS.GeoProjection
        m_FloatingScalebar_Enabled = False
        UpdateFloatingScalebar()
        frmMain.Layers.Clear()
        frmMain.Legend.Groups.Clear()
        ProjInfo.BookmarkedViews.Clear()
        frmMain.BuildBookmarkedViewsMenu()
        ClearPreview()

        ' August 5, 2011 - Paul Meems: The tiles plug-in is causing issues, so unload it:
        Dim lKey As String = frmMain.Plugins.GetPluginKey("Tiled Map")
        If Not String.IsNullOrEmpty(lKey) Then frmMain.Plugins.StopPlugin(lKey)

        ' The following line was removed due to the reason specified in the declaration section of this file.
        'm_PointImageSchemes.Clear() 
        m_FillStippleSchemes.Clear()
        SetModified(False)

        ProjInfo.SaveConfig() 'Save any configuration-level changes before we reload the config. 3/23/2006 by CDM for bug 102

        ProjInfo.LoadConfig(True)

        ResetViewState()

        'Create GEM Database
        If Not fromOpen Then
            DoSaveAs()
            If gemdb Is Nothing Then Exit Sub

            Dim f As New frmProjectDetails
            f.ShowDialog()

            If m_Project.ProjectProjection = "" Then
                m_Project.SetProjectProjectionByDialog()
                If m_Project.ProjectProjection = "" Then
                    Exit Sub
                End If
                SetModified(True)
            End If
        End If

    End Sub

    Public Sub DoOpenIntoCurrent(Optional ByVal Filename As String = "")
        If Filename = "" Then
            Dim cdlOpen As New OpenFileDialog
            cdlOpen.Filter = "GEM Project (*.gemprj)|*.gemprj"

            If (System.IO.Directory.Exists(AppInfo.DefaultDir)) Then
                cdlOpen.InitialDirectory = AppInfo.DefaultDir
            End If
            If Not cdlOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then Return
            Filename = cdlOpen.FileName
        End If

        If System.IO.File.Exists(Filename) Then
            AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(Filename)

            DoNew(True)
            ProjInfo.ProjectFileName = Filename
            gemdb = New GEMDatabase(System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb"))


            'Load the project into the current project (the first true)
            'with the group name matching the project filename sans .mwprj
            'Chris Michaelis, BugZilla 368
            If Not ProjInfo.LoadProject(Filename, True, System.IO.Path.GetFileNameWithoutExtension(Filename)) Then
                ' Paul Meems 10 Aug 2010: moved error box to here from LoadProject():
                ' TODO Needs localization:
                MapWinUtility.Logger.Msg("Errors occured while opening this project file", MsgBoxStyle.Exclamation, "Project File Error Report")
            End If

            AddGEMLayerIfNotPresent()
            Dim f As New frmProjectDetails
            f.ShowDialog()

            m_HasBeenSaved = False
            SetModified(False)
        End If
        Call UpdateButtons()
    End Sub

    Friend Sub DoOpen(Optional ByVal Filename As String = "")
        'Opens an existing project or opens a layer into the current project.


        'dpa 3/10/2008 - adding layer types to open dialog so that confused people can easily open a single layer.
        If Filename = "" Then
            Dim cdlOpen As New OpenFileDialog

            Dim gr As New MapWinGIS.Grid
            Dim im As New MapWinGIS.Image
            Dim sf As New MapWinGIS.Shapefile
            Dim LayerFilters As String = gr.CdlgFilter & "|" & im.CdlgFilter & "|" & sf.CdlgFilter
            gr = Nothing : im = Nothing : sf = Nothing
            cdlOpen.Filter = "GEM Project (*.gemprj)|*.gemprj" & "|" & LayerFilters

            'check to see if they want to save the project
            If Not m_HasBeenSaved Or ProjInfo.Modified Then
                If PromptToSaveProject() = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If

            'open a new project
            If (System.IO.Directory.Exists(AppInfo.DefaultDir)) Then
                cdlOpen.InitialDirectory = AppInfo.DefaultDir
            End If
            cdlOpen.ShowDialog()
            Filename = cdlOpen.FileName
        End If

        If System.IO.File.Exists(Filename) Then
            'save the location of the last open dir
            AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(Filename)

            If System.IO.Path.GetExtension(Filename) = ".gemprj" Then
                ' August 5, 2011 -  Paul Meems: Clear current project first:

                'GEM Open Existing database
                DoNew(True)
                ProjInfo.ProjectFileName = Filename
                gemdb = New GEMDatabase(System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb"))


                If Not ProjInfo.LoadProject(Filename) Then
                    ' Paul Meems 10 Aug 2010: moved error box to here from LoadProject():
                    ' TODO Needs localization:
                    MapWinUtility.Logger.Msg("Errors occured while opening this project file", MsgBoxStyle.Exclamation, "Project File Error Report")
                End If

                AddGEMLayerIfNotPresent()
                Dim f As New frmProjectDetails
                f.ShowDialog()

                m_HasBeenSaved = True
                ProjInfo.ProjectFileName = Filename
                SetModified(False)
            Else
                'a layer was selected (not a project file)
                If Not m_layers.AddLayer(Filename, System.IO.Path.GetFileNameWithoutExtension(Filename), , MapWindow.Layers.GetDefaultLayerVis, , , , , , , , True) Is Nothing Then
                    'Set the modified flag if successful
                    SetModified(True)
                End If
            End If
        End If
        Call UpdateButtons()
    End Sub

    Friend Sub DoSave()

        'If MapMain.NumLayers < 1 Then Return

        'Saves the current project
        Try
            ' this looks like a bunch of changes in a diff, but it's not (tws 6/27/07)
            Dim cdlSave As New SaveFileDialog
            cdlSave.Filter = "GEM Project (*.gemprj)|*.gemprj"
            If Not m_HasBeenSaved Or ProjInfo.ProjectFileName = String.Empty Then
                cdlSave.InitialDirectory = AppInfo.DefaultDir
                If (cdlSave.ShowDialog = DialogResult.Cancel) Then Exit Sub

                If (System.IO.Path.GetExtension(cdlSave.FileName) <> ".gemprj") Then
                    cdlSave.FileName &= ".gemprj"
                End If
                ProjInfo.ProjectFileName = cdlSave.FileName
                Me.Cursor = Cursors.WaitCursor
                If (ProjInfo.SaveProject()) Then
                    m_HasBeenSaved = True
                    ProjInfo.ProjectFileName = cdlSave.FileName
                    SetModified(False)
                    If gemdb Is Nothing Then
                        'GEM - save database
                        gemdb = New GEMDatabase(System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb"))
                        Dim f As New frmProjectDetails
                        f.ShowDialog()
                    End If
                End If
            Else
                Me.Cursor = Cursors.WaitCursor
                If (ProjInfo.SaveProject()) Then
                    m_HasBeenSaved = True
                    SetModified(False)
                    If gemdb Is Nothing Then
                        'GEM - save database
                        gemdb = New GEMDatabase(System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb"))
                        Dim f As New frmProjectDetails
                        f.ShowDialog()
                    End If
                End If
            End If
        Finally ' exceptions still propagate up, but we will NEVER leave the hourglass on
            Me.Cursor = Cursors.Default
        End Try
        Call UpdateButtons()
    End Sub

    Private Sub DoSaveAs()
        'If MapMain.NumLayers < 1 Then Return

        'Saves the project under a new file name
        Dim cdlSave As New SaveFileDialog
        cdlSave.Filter = "GEM Project (*.gemprj)|*.gemprj"
        If (cdlSave.ShowDialog = DialogResult.Cancel) Then
            If gemdb Is Nothing Then
                MessageBox.Show("You need to save the project before you can insert into the GEM database", "Save Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Exit Sub
        End If

        If (System.IO.Path.GetExtension(cdlSave.FileName) <> ".gemprj") Then
            cdlSave.FileName &= ".gemprj"
        End If

        ProjInfo.ProjectFileName = cdlSave.FileName
        If (ProjInfo.SaveProject()) Then
            m_HasBeenSaved = True
            ProjInfo.ProjectFileName = cdlSave.FileName
            SetModified(False)

            'GEM - save database
            If gemdb Is Nothing Then
                'Create new data
                gemdb = New GEMDatabase(System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb"))
            Else
                'Copy existing data base
                Dim existingDB As String = gemdb.DatabasePath
                Dim newDB As String = System.IO.Path.ChangeExtension(ProjInfo.ProjectFileName, ".gemdb")
                If Not IO.File.Exists(newDB) And IO.File.Exists(existingDB) Then
                    System.IO.File.Copy(existingDB, newDB)
                    gemdb = New GEMDatabase(newDB)
                Else
                    MessageBox.Show("An error has occured whilst copying the database")
                End If
            End If
        End If
        Call UpdateButtons()
    End Sub

    Private Sub DoAddLayer()
        'Adds a layer to the map
        If Not m_layers.AddLayer(, , , MapWindow.Layers.GetDefaultLayerVis, , , , , , , , True) Is Nothing Then
            'Set the modified flag if successful
            SetModified(True)
        End If
        Call UpdateButtons()
    End Sub

    ''' <summary>
    ''' Starts symbology manager
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoSymbologyManager()
        If (Legend.SelectedLayer <> -1) Then
            Dim lyr As Layer = Layers(Legend.SelectedLayer)
            If Not lyr Is Nothing Then
                If lyr.FileName = "" Then
                    MessageBox.Show("The layer hasn't got disk-based version. Options can't be saved.")
                Else
                    frmMain.Plugins.BroadcastMessage("SYMBOLOGY_MANAGER:" + lyr.Handle.ToString())
                End If
            End If
        End If
    End Sub

    Private Sub DoAddECWPLayer()
        Dim lyr As String = InputBox("Please enter the URL of the ECWP layer you wish to add:", "Add ECWP Layer", "ecwp://")
        If lyr.Trim() = "" Or lyr.Trim() = "ecwp://" Then Return

        If Not m_layers.AddLayer(lyr, "ECWP Layer", , False, , , , , , , , ) Is Nothing Then
            SetModified(True)
        End If
    End Sub

    Private Sub DoRemoveLayer()
        'Removes a layer from the map
        '1/25/2005 - dpa
        Dim curHandle As Integer = Legend.SelectedLayer
        If curHandle <> -1 Then
            m_layers.Remove(curHandle)
            Legend.Refresh()
            SetModified(True)
        End If
    End Sub

    Private Sub DoClearLayers()
        'Clear all layers from the map
        '1/25/2005 - dpa
        '13/10/2005 - PM
        'If mapwinutility.logger.msg("Are you sure you want to remove all layers?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm remove all layers") = MsgBoxResult.Yes Then
        If MapWinUtility.Logger.Msg(resources.GetString("msgClearLayers.Text"), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, resources.GetString("titleClearLayers.Text")) = MsgBoxResult.Yes Then
            m_layers.Clear()
            Legend.Layers.Clear()

            'Prevent asking if you want to save an empty project. CDM 2/22/2006
            SetModified(False)
        End If
    End Sub

    Friend Function PromptToSaveProject() As MsgBoxResult
        '1/31/2005 Modified this to read like the equivalent MS Word dialog. 
        Dim cdlSave As New SaveFileDialog
        Dim Result As MsgBoxResult

        cdlSave.Filter = "GEM Project (*.gemprj)|*.gemprj"
        If System.IO.Path.GetFileNameWithoutExtension(ProjInfo.ProjectFileName) = "" Then
            '13/10/2005 - PM
            'Result = mapwinutility.logger.msg("Do you want to save the changes to this project?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Exclamation, AppInfo.Name)
            Result = MapWinUtility.Logger.Msg(resources.GetString("msgSaveProject1.Text") & resources.GetString("msgSaveProject2.Text"), MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Exclamation, AppInfo.Name)
        Else
            '13/10/2005 - PM
            'Result = mapwinutility.logger.msg("Do you want to save the changes to " & System.IO.Path.GetFileNameWithoutExtension(ProjInfo.ProjectFileName) & "?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Exclamation, AppInfo.Name)
            Result = MapWinUtility.Logger.Msg(resources.GetString("msgSaveProject1.Text") & System.IO.Path.GetFileNameWithoutExtension(ProjInfo.ProjectFileName) & "?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Exclamation, AppInfo.Name)
        End If

        Select Case Result
            Case MsgBoxResult.Yes
                If m_HasBeenSaved = True And MapWinUtility.Strings.IsEmpty(ProjInfo.ProjectFileName) = False Then
                    ProjInfo.SaveProject()
                    m_HasBeenSaved = True
                    SetModified(False)
                Else
                    cdlSave.InitialDirectory = AppInfo.DefaultDir
                    If cdlSave.ShowDialog() = DialogResult.Cancel Then Return MsgBoxResult.Cancel

                    If (System.IO.Path.GetExtension(cdlSave.FileName) <> ".gemprj") Then
                        cdlSave.FileName &= ".gemprj"
                    End If
                    ProjInfo.ProjectFileName = cdlSave.FileName
                    ProjInfo.SaveProject()
                    m_HasBeenSaved = True
                    ProjInfo.ProjectFileName = cdlSave.FileName
                    SetModified(False)
                End If
                Return MsgBoxResult.Yes
            Case MsgBoxResult.Cancel
                Return MsgBoxResult.Cancel
            Case MsgBoxResult.No
                SetModified(False)
                Return MsgBoxResult.No
        End Select
    End Function

    Public Sub SetModified(ByVal Status As Boolean)
        'Sets the "modified" status of the current project, 
        'changing the ProjInfo object and the caption of the form.
        'Modified for version 4 to use the projinfo object.

        ' cdm 11/12/2006 - prevent setting modified if there are no layers and the filename is empty
        If Not ProjInfo Is Nothing AndAlso ((ProjInfo.ProjectFileName Is Nothing OrElse ProjInfo.ProjectFileName.Trim() = "") And frmMain.MapMain.NumLayers = 0) Then
            Status = False
        End If

        If (ProjInfo.Modified <> Status) Then
            ProjInfo.Modified = Status
            frmMain.UpdateButtons()
        End If

        ' 7/28/2011 Teva - added the MapWindowVersion property
        MapMain.ShowVersionNumber = AppInfo.ShowMapWindowVersion
        MapMain.ShowRedrawTime = AppInfo.ShowHideRedrawSpeed

        ' 7/28/2011 Teva - added the FloatingScalebar property
        m_FloatingScalebar_Enabled = AppInfo.ShowFloatingScalebar
        UpdateFloatingScalebar()

        ' lsu: the following lines will override individual settings for all layers
        ' Isn't it supposed to work as default value at loading only ?
        ' I comment this, until it's resolved

        ' 8/3/2011 Teva - added avoid collision property
        For i As Integer = 0 To frmMain.Layers.Count() - 1
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(frmMain.Layers.GetHandle(i))   ' lsu: fixed the error at removing the layer
            'If Not layer Is Nothing Then layer.UseLabelCollision = AppInfo.ShowAvoidCollision
        Next

        ' lsu: to create the index, sf.CreateSpatialIndex should be used; 
        ' Also this should be used at loading of layer only; otherwise per-layer settings will overridden
        ' I comment this, until it's resolved

        ' 8/4/2011 Teva - added Auto Create Spatial Index property
        If frmMain.Layers.NumLayers > 0 Then
            Dim sf As MapWinGIS.Shapefile = TryCast(m_layers(m_layers.CurrentLayer).GetObject(), MapWinGIS.Shapefile)
            'If Not (sf Is Nothing) Then sf.UseSpatialIndex = AppInfo.ShowAutoCreateSpatialindex
            frmMain.MapMain.Redraw()
        End If

        ' cdm 7/11/05 - added custom window title option support
        If MapWinUtility.Strings.IsEmpty(ProjInfo.ProjectFileName) Then
            'Version Numbers: frmMain.Text = AppInfo.Name + " " + App.VersionString + CType(IIf(CustomWindowTitle = "", "", " - " + CustomWindowTitle), String) + CType(IIf(Status, "*", ""), String)
            frmMain.Text = AppInfo.Name + " " + CType(IIf(CustomWindowTitle = "", "", " - " + CustomWindowTitle), String) + CType(IIf(Status, "*", ""), String)
        Else
            'Version Numbers: frmMain.Text = AppInfo.Name + " " + App.VersionString + CType(IIf(CustomWindowTitle = "", "", " - " + CustomWindowTitle), String) + " - " + CType(IIf(frmMain.Title_ShowFullProjectPath, ProjInfo.ProjectFileName, System.IO.Path.GetFileNameWithoutExtension(ProjInfo.ProjectFileName)), String) + CType(IIf(Status, "*", ""), String)
            frmMain.Text = AppInfo.Name + " " + CType(IIf(CustomWindowTitle = "", "", " - " + CustomWindowTitle), String) + " - " + CType(IIf(frmMain.Title_ShowFullProjectPath, ProjInfo.ProjectFileName, System.IO.Path.GetFileNameWithoutExtension(ProjInfo.ProjectFileName)), String) + CType(IIf(Status, "*", ""), String)
        End If
    End Sub

    Private Function InBox(ByVal rect As Rectangle, ByVal x As Double, ByVal y As Double) As Boolean
        If x >= rect.Left AndAlso x <= rect.Right AndAlso y <= rect.Bottom AndAlso y >= rect.Top Then Return True
    End Function

    Private Function Dist(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        Return (Math.Sqrt((x2 - x1) ^ 2 + (y2 - y1) ^ 2))
    End Function

    Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)
        If MapMain.Focused = False Then Exit Sub

        If AppInfo.MouseWheelZoom = MouseWheelZoomDir.NoAction Then Exit Sub

        If e.Delta > 0 Then
            If AppInfo.MouseWheelZoom = MouseWheelZoomDir.WheelUpZoomsIn Then
                ZoomToMouseCursor(1 - m_View.ZoomPercent)
                'm_View.ZoomIn(m_View.ZoomPercent)
            Else
                ZoomToMouseCursor(1 / (1 - m_View.ZoomPercent))
                'm_View.ZoomOut(m_View.ZoomPercent)
            End If
            SetModified(True)
        ElseIf e.Delta < 0 Then
            If AppInfo.MouseWheelZoom = MouseWheelZoomDir.WheelUpZoomsIn Then
                ZoomToMouseCursor(1 / (1 - m_View.ZoomPercent))
                'm_View.ZoomOut(m_View.ZoomPercent)
            Else
                ZoomToMouseCursor(1 - m_View.ZoomPercent)
                'm_View.ZoomIn(m_View.ZoomPercent)
            End If
            SetModified(True)
        End If
    End Sub

    ' lsu 22 jul 2009
    Private Declare Function GetWindowRect Lib "user32" Alias "GetWindowRect" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Private Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Private Sub ZoomToMouseCursor(ByVal dRatio As Double)
        Dim ext As MapWinGIS.Extents
        Dim tRect As RECT
        Dim tPoint As POINTAPI
        Dim dWidth As Double
        Dim dHeight As Double
        Dim dXCent As Double
        Dim dYCent As Double
        Dim dx As Double
        Dim dy As Double

        ' absolute cursor position
        GetCursorPos(tPoint)
        GetWindowRect(Me.MapMain.HWnd, tRect)
        MapMain.PixelToProj(CDbl(tPoint.x - tRect.Left), CDbl(tPoint.y - tRect.Top), dXCent, dYCent)

        If tPoint.x < tRect.Left Or tPoint.x > tRect.Right Or tPoint.y < tRect.Top Or tPoint.y > tRect.Bottom Then Exit Sub

        ' relative cursor position
        dx = (tPoint.x - tRect.Left) / (tRect.Right - tRect.Left)
        dy = (tPoint.y - tRect.Top) / (tRect.Bottom - tRect.Top)

        ' new extents
        ext = MapMain.Extents
        dHeight = (ext.yMax - ext.yMin) * dRatio
        dWidth = (ext.xMax - ext.xMin) * dRatio

        ext = New MapWinGIS.Extents
        ext.SetBounds(dXCent - dWidth * dx, dYCent - dHeight * (1 - dy), 0, dXCent + dWidth * (1 - dx), dYCent + dHeight * dy, 0)
        m_View.Extents = ext
        MapMain.Redraw()
    End Sub

    Public Sub ShowErrorDialog(ByVal ex As System.Exception) Implements MapWindow.Interfaces.IMapWin.ShowErrorDialog
        ShowError(ex, "root@mapwindow.org")
    End Sub

    Public Sub ShowErrorDialog(ByVal ex As System.Exception, ByVal sendToEmail As String) Implements MapWindow.Interfaces.IMapWin.ShowErrorDialog
        ShowError(ex, sendToEmail)
    End Sub

    Public Function GetProjectionFromUser(ByVal DialogCaption As String, ByVal DefaultProjection As String) As String Implements Interfaces.IMapWin.GetProjectionFromUser
        Dim form As New frmChooseProjection(modMain.ProjectionDB, modMain.frmMain)
        form.Text = DialogCaption
        If (form.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Dim proj As MapWinGIS.GeoProjection = form.projectionTreeView1.SelectedProjection
            If Not proj Is Nothing Then Return proj.ExportToProj4()
        End If
        Return ""
    End Function

    Public Sub RefreshMap() Implements Interfaces.IMapWin.Refresh
        For i As Integer = 0 To frmMain.m_layers.NumLayers - 1
            If Not frmMain.m_layers(frmMain.m_layers.GetHandle(i)).FillStippleScheme Is Nothing Then
                frmMain.m_layers(frmMain.m_layers.GetHandle(i)).HatchingRecalculate()
            End If
        Next
        frmMain.MapMain.Redraw()
    End Sub

    Private Sub FlushForwardHistory()
        Dim i As Integer
        MapWinUtility.Logger.Dbg("In FlushForwardHistory CurrentExtent: " + m_CurrentExtent.ToString())
        If m_Extents.Count > 0 Then
            If m_CurrentExtent < m_Extents.Count - 1 Then
                'm_Extents.RemoveRange(m_CurrentExtent + 1, m_Extents.Count - m_CurrentExtent)
                For i = m_Extents.Count - 1 To m_CurrentExtent + 1 Step -1
                    m_Extents.RemoveAt(i)
                Next i
            Else
                m_CurrentExtent = m_Extents.Count - 1
            End If
        End If
    End Sub

#End Region


    Public Sub BuildRecentProjectsMenu()

        '3/17/05 mg
        Dim i As Integer
        Dim filename As String
        Dim key As String
        Dim keysToRemove As New ArrayList

        'Find RecentProject menu items to remove
        'note: cannot remove within For Each, so we remove them next
        For Each key In m_Menu.m_MenuTable.Keys
            If key.StartsWith(RecentProjectPrefix) Then
                keysToRemove.Add(key)
            End If
        Next

        For Each key In keysToRemove
            m_Menu.Remove(key)
        Next

        'Add all current ProjInfo.RecentProjects to the menu
        For i = 0 To ProjInfo.RecentProjects.Count - 1
            filename = Trim(ProjInfo.RecentProjects(i).ToString)
            If Not filename = "" And Not filename = ".gemprj" Then
                ' Chris Michaelis June 30 2005 -- when a path with spaces gets put here, the spaces get cut out when it's turned into a key.
                ' Replacing the space with {32}, the ascii code for space. Also see CustomMenu_Click which does the reverse.
                key = RecentProjectPrefix & filename.Replace(" ", "{32}")
                m_Menu.AddMenu(key, "mnuRecentProjects", Nothing, System.IO.Path.GetFileNameWithoutExtension(filename))
            End If
        Next

    End Sub

    Private Sub DoZoomPrevious()
        If m_Extents.Count > 0 And m_CurrentExtent > 0 Then
            m_IsManualExtentsChange = True
            m_CurrentExtent -= 1
            MapWinUtility.Logger.Dbg("In DoZoomPrevious. New CurrentExtent: " + m_CurrentExtent.ToString())
            MapMain.Extents = m_Extents(m_CurrentExtent)
        End If
        'Paul Meems 6/11/2009
        'Is also done in MapMain_ExtentsChanged.
        'No need to do it twice:
        'UpdateButtons()
    End Sub

    Private Sub DoZoomNext()
        If m_CurrentExtent < m_Extents.Count - 1 Then
            m_CurrentExtent += 1
            m_IsManualExtentsChange = True
            MapMain.Extents = m_Extents(m_CurrentExtent)
        End If
        UpdateButtons()
    End Sub

    Private Sub DoZoomMax()
        MapMain.ZoomToMaxVisibleExtents()
        UpdateButtons()
    End Sub

    Private Sub DoZoomLayer()
        MapMain.ZoomToLayer(Legend.SelectedLayer)
        UpdateButtons()
    End Sub

    Private Sub DoZoomSelected()
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not View.SelectedShapes Is Nothing AndAlso View.SelectedShapes.NumSelected > 0 Then
                ' This code borrowed from the MapWindow.  It should be included on the 
                ' MapWindow.View or MapWindow.View.SelectedShapes interfaces sometime
                ' becuase it is a very useful function, and will have to be duplicated
                ' many times if it is not added.

                Dim maxX, maxY, minX, minY As Double
                Dim dx, dy As Double
                Dim tExts As MapWinGIS.Extents

                Dim i As Integer

                With View.SelectedShapes(0)
                    maxX = .Extents.xMax
                    minX = .Extents.xMin
                    maxY = .Extents.yMax
                    minY = .Extents.yMin
                End With
                For i = 0 To View.SelectedShapes.NumSelected - 1
                    If m_layers(m_layers.CurrentLayer).Visible = False Then
                        m_layers(m_layers.CurrentLayer).Visible = True
                    End If
                    With View.SelectedShapes(i).Extents
                        If .xMax > maxX Then maxX = .xMax
                        If .yMax > maxY Then maxY = .yMax
                        If .xMin < minX Then minX = .xMin
                        If .yMin < minY Then minY = .yMin
                    End With
                Next i

                ' Pad extents now
                dx = maxX - minX
                dx = dx / 8
                If dx = 0 Then
                    dx = 1
                End If
                maxX = maxX + dx
                minX = minX - dx

                dy = maxY - minY
                dy = dy / 8
                If dy = 0 Then
                    dy = 1
                End If
                maxY = maxY + dy
                minY = minY - dy

                tExts = New MapWinGIS.Extents
                If View.SelectedShapes.NumSelected = 1 And m_layers(m_layers.CurrentLayer).LayerType = MapWindow.Interfaces.eLayerType.PointShapefile Then
                    Dim sf As MapWinGIS.Shapefile = CType(m_layers(m_layers.CurrentLayer).GetObject(), MapWinGIS.Shapefile)
                    'Use shape extents - best we can do
                    Dim xpad As Double = (1 / 100) * (sf.Extents.xMax - sf.Extents.xMin)
                    Dim ypad As Double = (1 / 100) * (sf.Extents.yMax - sf.Extents.yMin)
                    tExts.SetBounds(minX + xpad, minY - ypad, 0, maxX - xpad, maxY + ypad, 0)
                Else
                    tExts.SetBounds(minX, minY, 0, maxX, maxY, 0)
                End If
                View.Extents = tExts
                tExts = Nothing
            End If
        Catch e As Exception
            MapWinUtility.Logger.Dbg("Error: " + e.ToString())
            Exit Sub
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DoZoomShape()
        Dim maxX As Double, maxY As Double
        Dim minX As Double, minY As Double
        Dim dx As Double, dy As Double
        Dim i As Integer, tExts As MapWinGIS.Extents
        With m_View.SelectedShapes(0)
            maxX = .Extents.xMax
            minX = .Extents.xMin
            maxY = .Extents.yMax
            minY = .Extents.yMin
        End With
        For i = 0 To m_View.SelectedShapes.NumSelected - 1
            With m_View.SelectedShapes(i).Extents
                If .xMax > maxX Then maxX = .xMax
                If .yMax > maxY Then maxY = .yMax
                If .xMin < minX Then minX = .xMin
                If .yMin < minY Then minY = .yMin
            End With
        Next i

        ' Pad extents now
        dx = maxX - minX
        dx = dx * m_View.ExtentPad
        maxX = maxX + dx
        minX = minX - dx

        dy = maxY - minY
        dy = dy * m_View.ExtentPad
        maxY = maxY + dy
        minY = minY - dy

        tExts = New MapWinGIS.Extents
        tExts.SetBounds(minX, minY, 0, maxX, maxY, 0)
        MapMain.Extents = tExts
        tExts = Nothing
        UpdateButtons()
    End Sub

    Public ReadOnly Property ApplicationInfo() As Interfaces.AppInfo Implements Interfaces.IMapWin.ApplicationInfo
        Get
            Return modMain.AppInfo
        End Get
    End Property


    Friend Function GetOrRemovePanel(ByVal psStartText As String, Optional ByVal pbRemove As Boolean = False) As Integer
        Dim i As Integer
        Dim l As Integer
        l = Len(psStartText)

        For i = StatusBar.NumPanels - 1 To 0 Step -1
            If Microsoft.VisualBasic.Left(StatusBar.Item(i).Text, l) = psStartText Then
                GetOrRemovePanel = i
                If pbRemove = True Then
                    StatusBar.RemovePanel(i)
                End If
            End If
        Next i
    End Function

    '3/21/2008 added by Jiri Kadlec - format the distance or area calculated by "Measure distance"
    'or "Measure Area" tools and shown in the status bar using the current project settings
    '(number of decimal places will be the same as specified in Status Bar Comma Separators
    'and Status Bar Decimal Places.
    Friend Function formatDistance(ByVal dist As Double) As String
        Dim decimals As Integer = ProjInfo.StatusBarAlternateCoordsNumDecimals
        Dim useCommas As Integer = ProjInfo.StatusBarAlternateCoordsUseCommas

        Dim nf As String 'the number formatting string
        If useCommas = True Then
            nf = "N" + decimals.ToString
        Else
            nf = "F" + decimals.ToString
        End If
        ' Paul Meems 15 sept Added CultureInfo.InvariantCulture:
        Return dist.ToString(nf, CultureInfo.InvariantCulture)

    End Function

    'calculate a distance between two points.
    'the result is in Project.MapUnits.
    'if the project map units are decimal degrees, the result is in kilometers.
    Private Function distance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        If (Project.MapUnits.ToLower = "lat/long") Then
            'jiri kadlec 22/2/2008 corrected order of lat/long parameters in LLDistance
            Return LLDistance(y1, x1, y2, x2)
        Else
            Return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2))
        End If
    End Function

    Private Function deg2rad(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function

    Private Function rad2deg(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function

    'Chris M 7/10/2006 for Buzilla 175
    'the input coordinates must be in decimal degrees.
    'output is always in kilometers
    Private Function LLDistance(ByVal p1lat As Double, ByVal p1lon As Double, ByVal p2lat As Double, ByVal p2lon As Double) As Double
        Dim FLATTENING As Double = 1 / 298.257223563

        Dim lat1 As Double = deg2rad(p1lat)
        Dim lon1 As Double = deg2rad(p1lon)
        Dim lat2 As Double = deg2rad(p2lat)
        Dim lon2 As Double = deg2rad(p2lon)

        Dim F As Double = (lat1 + lat2) / 2.0
        Dim G As Double = (lat1 - lat2) / 2.0
        Dim L As Double = (lon1 - lon2) / 2.0

        Dim sing As Double = Math.Sin(G)
        Dim cosl As Double = Math.Cos(L)
        Dim cosf As Double = Math.Cos(F)
        Dim sinl As Double = Math.Sin(L)
        Dim sinf As Double = Math.Sin(F)
        Dim cosg As Double = Math.Cos(G)

        Dim S As Double = sing * sing * cosl * cosl + cosf * cosf * sinl * sinl
        Dim C As Double = cosg * cosg * cosl * cosl + sinf * sinf * sinl * sinl
        Dim W As Double = Math.Atan2(Math.Sqrt(S), Math.Sqrt(C))
        Dim R As Double = Math.Sqrt((S * C)) / W
        Dim H1 As Double = (3 * R - 1.0) / (2.0 * C)
        Dim H2 As Double = (3 * R + 1.0) / (2.0 * S)
        Dim D As Double = 2 * W * 6378.135
        Return (D * (1 + FLATTENING * H1 * sinf * sinf * cosg * cosg - FLATTENING * H2 * cosf * cosf * sing * sing))
    End Function

    'Chris Michaelis for Bugzilla 155
    Public Shared Sub SaveCustomColors(ByRef dlg As ColorDialog)
        If dlg Is Nothing Then Exit Sub
        If dlg.CustomColors Is Nothing Then Exit Sub

        Dim c As Integer
        Dim i As Integer = 0
        For Each c In dlg.CustomColors
            SaveSetting("MapWindow", "CustomColors", "Color" + i.ToString(), c.ToString())
            i += 1
        Next
        SaveSetting("MapWindow", "CustomColors", "Count", (i + 1).ToString())
    End Sub

    'Chris Michaelis for Bugzilla 155
    Public Shared Sub LoadCustomColors(ByRef dlg As ColorDialog)
        If dlg Is Nothing Then Exit Sub

        Dim count As Integer = Integer.Parse(GetSetting("MapWindow", "CustomColors", "Count", 0))
        If count = 0 Then Exit Sub

        Dim newColors(count) As Integer

        For i As Integer = 0 To count - 1
            newColors(i) = Integer.Parse(GetSetting("MapWindow", "CustomColors", "Color" + i.ToString(), System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Color.White))))
        Next

        dlg.CustomColors = newColors
    End Sub



    'Christopher Michaelis, June 12, 2006
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer

    'Christopher Michaelis, June 12, 2006
    Public Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure

    'Christopher Michaelis, June 12, 2006
    Public Shared Function GetCursorLocation() As Point
        Dim pnt As POINTAPI
        GetCursorPos(pnt)
        Return New Point(pnt.x, pnt.y)
    End Function

    'Christopher Michaelis, June 12, 2006
    Private Function InMyFormBounds(ByVal pt As Point) As Boolean
        If pt.X < Me.Location.X + Me.Width And pt.X > Me.Location.X _
        And pt.Y < Me.Location.Y + Me.Height And pt.Y > Me.Location.Y Then
            Return True
        End If

        Return False
    End Function

    'Christopher Michaelis, June 12, 2006
    Private Sub FloatingBar_Move(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf (sender) Is Form Then
            If CType(sender, Form).Controls.Count > 0 Then
                If TypeOf (CType(sender, Form).Controls(0)) Is ToolStripContainer Then
                    If CType(CType(sender, Form).Controls(0), ToolStripContainer).TopToolStripPanel.Controls.Count > 0 Then
                        UndockableToolstrip_EndDrag(CType(CType(sender, Form).Controls(0), ToolStripContainer).TopToolStripPanel.Controls(0), e)
                    End If
                End If
            End If
        End If
    End Sub

    'Christopher Michaelis, June 12, 2006
    Public Sub UndockableToolstrip_EndDrag(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbStandard.EndDrag, tlbMain.EndDrag, tlbZoom.EndDrag
        Dim cPt As Point = GetCursorLocation()
        Dim ts As ToolStrip = CType(sender, ToolStrip)
        Dim myType As Type = Me.GetType()
        Dim InFrmBoundary As Boolean = InMyFormBounds(cPt)

        If ts.Tag = "" Then ts.Tag = "Docked" 'Assume docked; somebody forgot to set it!

        If InFrmBoundary And ts.Tag = "Docked" Then
            'Docking normally within my form; do nothing.
            Return
        ElseIf InFrmBoundary And ts.Tag = "Floating" Then
            'Redocking!
            Dim oldTS As ToolStripPanel = ts.Parent
            oldTS.Controls.Remove(ts)
            StripDocker.TopToolStripPanel.Controls.Add(ts)
            RemoveHandler oldTS.ParentForm.Move, AddressOf FloatingBar_Move
            oldTS.ParentForm.Close()
            ts.Tag = "Docked"
        ElseIf Not InFrmBoundary And ts.Tag = "Docked" Then
            'The user wants the toolstrip to undock and float
            'Make a floating toolbar:
            Dim FloatingToolbar As New frmFloatingToolbar
            If TypeOf ts.Parent Is ToolStripPanel Then
                CType(ts.Parent, ToolStripPanel).Controls.Remove(ts)
            Else
                MapWinUtility.Logger.Msg(ts.Parent.GetType().ToString())
            End If
            ' TODO: Make it work better. The floating toolbar should have a border and a close option,
            ' On close it should go back to the panel or a menu option should be added to reload closed toolbars
            ' See http://en.csharp-online.net/Tool%2C_Menu%2C_and_Status_Strips%E2%80%94Floating_ToolStrips
            FloatingToolbar.tsc.TopToolStripPanel.Controls.Add(ts)
            FloatingToolbar.Width = ts.Width + 5
            FloatingToolbar.Height = ts.Height
            FloatingToolbar.MinimumSize = New Size(ts.Width + 5, ts.Height)
            FloatingToolbar.SetDesktopLocation(cPt.X, cPt.Y)
            AddHandler FloatingToolbar.Move, AddressOf FloatingBar_Move

            FloatingToolbar.Show()
            ts.Tag = "Floating"
        ElseIf Not InFrmBoundary And ts.Tag = "Floating" Then
            'The bar is floating and they dragged it -- try to
            'detect new location.
            Try
                CType(ts.Parent.Parent.Parent, Form).Location = cPt
            Catch
            End Try
        End If
    End Sub

    'Chris Michaelis July 20 2006
    Private Sub CheckForUpdates()
        Dim myVersion As String = App.VersionString
        'The version needs to be numeric, i.e. only one decimal.
        While Not myVersion.LastIndexOf(".") = myVersion.IndexOf(".")
            myVersion = myVersion.Substring(0, myVersion.LastIndexOf(".")) & myVersion.Substring(myVersion.LastIndexOf(".") + 1)
        End While

        Dim updateCheckFilename As String = BinFolder & "\UpdateCheck.exe"

        If System.IO.File.Exists(updateCheckFilename) Then
            Dim prcs As New Diagnostics.ProcessStartInfo
            prcs.FileName = updateCheckFilename
            prcs.Arguments = "http://www.MapWindow.org/CheckForUpdates.php?p=MapWindowApp" & "&cv=" & myVersion.ToString() & " " & Diagnostics.Process.GetCurrentProcess().Id.ToString()
            Diagnostics.Process.Start(prcs)
        Else
            '7/31/2006 PM
            'If mapwinutility.logger.msg("The Update Check tool could not be located; all updates will need to be manually downloaded." & vbCrLf & vbCrLf & "Open the Downloads page now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update Check Tool Not Found") = MsgBoxResult.Yes Then
            Dim strMessage As String = String.Format(resources.GetString("msgUpdateToolNotFound.Text"), vbCrLf & vbCrLf)
            If MapWinUtility.Logger.Msg(strMessage, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, AppInfo.Name) = MsgBoxResult.Yes Then
                Diagnostics.Process.Start("http://www.MapWindow.org/download.php?show_details=1")
            End If
        End If
    End Sub

    Private Sub mnuZoomButtons_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuZoomPrevious.Click, mnuZoomNext.Click, mnuZoomMax.Click, mnuZoomLayer.Click, mnuZoomSelected.Click, mnuZoomShape.Click, mnuZoomPreviewMap.Click
        Dim b As Windows.Forms.ToolStripMenuItem = CType(sender, Windows.Forms.ToolStripMenuItem)

        mnuZoomPrevious.Checked = IIf(b.Name = "mnuZoomPrevious", True, False)
        mnuZoomNext.Checked = IIf(b.Name = "mnuZoomNext", True, False)
        mnuZoomMax.Checked = IIf(b.Name = "mnuZoomMax", True, False)
        mnuZoomLayer.Checked = IIf(b.Name = "mnuZoomLayer", True, False)
        mnuZoomSelected.Checked = IIf(b.Name = "mnuZoomSelected", True, False)
        mnuZoomShape.Checked = IIf(b.Name = "mnuZoomShape", True, False)
        mnuZoomPreviewMap.Checked = IIf(b.Name = "mnuZoomPreviewMap", True, False)

        'tbbZoom.Image = b.Image

        If mnuZoomPrevious.Checked Then
            MapWinUtility.Logger.Dbg("mnuZoomPrevious.Checked before DoZoomPrevious() CurrentExtent: " + m_CurrentExtent.ToString())
            DoZoomPrevious()
        ElseIf mnuZoomNext.Checked Then
            DoZoomNext()
        ElseIf mnuZoomMax.Checked Then
            DoZoomMax()
        ElseIf mnuZoomLayer.Checked Then
            DoZoomLayer()
        ElseIf mnuZoomSelected.Checked Then
            DoZoomSelected()
        ElseIf mnuZoomShape.Checked Then
            DoZoomShape()
        ElseIf mnuZoomPreviewMap.Checked Then
            doZoomToPreview()
        End If
    End Sub

    Private Sub DoToggleScalebar()
        SetModified(True)
        m_FloatingScalebar_Enabled = Not m_FloatingScalebar_Enabled
        ApplicationInfo.ShowFloatingScalebar = m_FloatingScalebar_Enabled
        UpdateFloatingScalebar()
    End Sub

    Public Sub UpdateFloatingScalebar()
        'If this menu doesn't exist, MW is initializing; skip for now.
        If Menus Is Nothing OrElse Menus("mnuShowScaleBar") Is Nothing Then Return

        'Every time we update the scale bar, verify
        'on the menu items' checked status as well as
        'destroy or create the picturebox if needed
        Menus("mnuShowScaleBar").Checked = m_FloatingScalebar_Enabled

        'Paul Meems: If no projection is available the scale bar has no use:
        If frmMain.Project.ProjectProjection = String.Empty Then m_FloatingScalebar_Enabled = False

        If Not m_FloatingScalebar_Enabled Then
            If Not m_FloatingScalebar_PictureBox Is Nothing AndAlso mapPanel.Contains(m_FloatingScalebar_PictureBox) Then
                mapPanel.Controls.Remove(m_FloatingScalebar_PictureBox)
                m_FloatingScalebar_PictureBox.Image.Dispose()
                m_FloatingScalebar_PictureBox.Image = Nothing
                m_FloatingScalebar_PictureBox.Dispose()
                m_FloatingScalebar_PictureBox = Nothing
            End If
        Else
            'First, test to ensure that the window is not minimized. Otherwise, when the scale bar tries
            'to generate itself, the distance per pixel will be infinite
            'and therefore the range will overflow
            If Me.WindowState = FormWindowState.Minimized Then Return

            If m_FloatingScalebar_PictureBox Is Nothing Then
                m_FloatingScalebar_PictureBox = New Windows.Forms.PictureBox
                AddHandler m_FloatingScalebar_PictureBox.Click, AddressOf FloatingScalebarClick
            End If

            If Not mapPanel Is Nothing AndAlso Not mapPanel.IsDisposed Then
                If Not mapPanel.Controls.Contains(m_FloatingScalebar_PictureBox) Then
                    mapPanel.Controls.Add(m_FloatingScalebar_PictureBox)
                End If
            End If

            'OK -- created and enabled, draw it

            m_FloatingScalebar_PictureBox.Visible = False

            ' Why static?
            Static sb As New ScaleBarUtility

            'Default -- overridden by project units if set
            Dim mapunit As UnitOfMeasure = UnitOfMeasure.Meters

            If (Not Project.MapUnits = "") Then
                mapunit = MapWinGeoProc.UnitConverter.StringToUOM(Project.MapUnits) '08/28/08 jk - new conversion function
            End If

            'Default - overridden by "Status Bar Alternate" units, then may be
            'further overridden by the context menu setting.
            Dim ScaleUnit As UnitOfMeasure = mapunit

            'Prefer alternate coordinate system
            ScaleUnit = MapWinGeoProc.UnitConverter.StringToUOM(modMain.ProjInfo.ShowStatusBarCoords_Alternate) '08/28/08 jk - new conversion function

            If Not m_FloatingScalebar_ContextMenu_SelectedUnit = "" Then
                ScaleUnit = MapWinGeoProc.UnitConverter.StringToUOM(m_FloatingScalebar_ContextMenu_SelectedUnit)
            End If

            'Disallow showing degrees as a measurement.
            If ScaleUnit = UnitOfMeasure.DecimalDegrees Then ScaleUnit = UnitOfMeasure.Kilometers

            m_FloatingScalebar_PictureBox.BorderStyle = BorderStyle.FixedSingle
            m_FloatingScalebar_PictureBox.Image = sb.GenerateScaleBar(CType(MapMain.Extents, MapWinGIS.Extents), mapunit, ScaleUnit, 300, m_FloatingScalebar_ContextMenu_BackColor, m_FloatingScalebar_ContextMenu_ForeColor)
            m_FloatingScalebar_PictureBox.SizeMode = PictureBoxSizeMode.AutoSize

            Select Case m_FloatingScalebar_ContextMenu_SelectedPosition
                Case "UpperLeft"
                    m_FloatingScalebar_PictureBox.Location = New Point(0, 0)
                Case "UpperRight"
                    m_FloatingScalebar_PictureBox.Location = New Point(MapMain.Width - m_FloatingScalebar_PictureBox.Width, 0)
                Case "LowerLeft"
                    m_FloatingScalebar_PictureBox.Location = New Point(0, MapMain.Height - m_FloatingScalebar_PictureBox.Height)
                Case "LowerRight"
                    m_FloatingScalebar_PictureBox.Location = New Point(MapMain.Width - m_FloatingScalebar_PictureBox.Width, MapMain.Height - m_FloatingScalebar_PictureBox.Height)
                Case Else
                    m_FloatingScalebar_PictureBox.Location = New Point(MapMain.Width - m_FloatingScalebar_PictureBox.Width, MapMain.Height - m_FloatingScalebar_PictureBox.Height)
            End Select

            m_FloatingScalebar_PictureBox.BringToFront()
            m_FloatingScalebar_PictureBox.Visible = True
        End If
    End Sub

    Private Sub FloatingScalebarClick(ByVal sender As Object, ByVal e As EventArgs)
        m_FloatingScalebar_ContextMenu.Show(Me, Me.PointToClient(Cursor.Position))
    End Sub

    Private Sub FloatingScalebar_UpperLeft_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_FloatingScalebar_ContextMenu_SelectedPosition = "UpperLeft"
        m_FloatingScalebar_ContextMenu_UL.Checked = True
        m_FloatingScalebar_ContextMenu_UR.Checked = False
        m_FloatingScalebar_ContextMenu_LL.Checked = False
        m_FloatingScalebar_ContextMenu_LR.Checked = False

        SetModified(True)
        UpdateFloatingScalebar()
    End Sub

    Private Sub FloatingScalebar_UpperRight_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_FloatingScalebar_ContextMenu_SelectedPosition = "UpperRight"
        m_FloatingScalebar_ContextMenu_UL.Checked = False
        m_FloatingScalebar_ContextMenu_UR.Checked = True
        m_FloatingScalebar_ContextMenu_LL.Checked = False
        m_FloatingScalebar_ContextMenu_LR.Checked = False

        SetModified(True)
        UpdateFloatingScalebar()
    End Sub

    Private Sub FloatingScalebar_LowerLeft_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_FloatingScalebar_ContextMenu_SelectedPosition = "LowerLeft"
        m_FloatingScalebar_ContextMenu_UL.Checked = False
        m_FloatingScalebar_ContextMenu_UR.Checked = False
        m_FloatingScalebar_ContextMenu_LL.Checked = True
        m_FloatingScalebar_ContextMenu_LR.Checked = False

        SetModified(True)
        UpdateFloatingScalebar()
    End Sub

    Private Sub FloatingScalebar_LowerRight_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_FloatingScalebar_ContextMenu_SelectedPosition = "LowerRight"
        m_FloatingScalebar_ContextMenu_UL.Checked = False
        m_FloatingScalebar_ContextMenu_UR.Checked = False
        m_FloatingScalebar_ContextMenu_LL.Checked = False
        m_FloatingScalebar_ContextMenu_LR.Checked = True

        SetModified(True)
        UpdateFloatingScalebar()
    End Sub

    Private Sub FloatingScalebar_ChooseForecolor_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim picker As New ColorPickerSingle
        If picker.ShowDialog() = Windows.Forms.DialogResult.OK Then
            m_FloatingScalebar_ContextMenu_ForeColor = picker.btnStartColor.BackColor
            SetModified(True)
            UpdateFloatingScalebar()
        End If
    End Sub

    Private Sub FloatingScalebar_ChooseBackcolor_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim picker As New ColorPickerSingle
        If picker.ShowDialog() = Windows.Forms.DialogResult.OK Then
            m_FloatingScalebar_ContextMenu_BackColor = picker.btnStartColor.BackColor
            SetModified(True)
            UpdateFloatingScalebar()
        End If
    End Sub

    Private Sub FloatingScalebar_ChangeUnits_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim slct As New frmChooseDisplayUnits
        If slct.ShowDialog() = Windows.Forms.DialogResult.OK Then
            m_FloatingScalebar_ContextMenu_SelectedUnit = slct.list.Items(slct.list.SelectedIndex)
            SetModified(True)
            UpdateFloatingScalebar()
        End If
    End Sub

    Public Sub BuildBookmarkedViewsMenu()

        'Christian degrassi 2010-03-07: This fixes issue 1635
        'NOTE: This issue was resolved by avoiding m_Menu.Remove(key) which would not 
        'handle menus removal correcly (issue 1637)
        Dim keysToRemove As New ArrayList
        Dim ParentMenuKey As String = "mnuBookmarkedViews"
        Dim ParentToolStripMenuItem As Windows.Forms.ToolStripMenuItem = Nothing

        ParentToolStripMenuItem = m_Menu.m_MenuTable(m_Menu.MenuTableKey(ParentMenuKey))
        If (ParentToolStripMenuItem Is Nothing) Then
            ' Start Paul Meems May 30 2010
            ' It's perfectly valid to not have this menu, so don't throw an exception
            ' Throw New Exception("The 'Bookmarked Views' Menu Item could not be found")
            Return
            ' End Paul Meems May 30 2010
        End If

        'Find menu items to remove
        For Each key As String In m_Menu.m_MenuTable.Keys
            If key.StartsWith(BookmarkedViewPrefix) Then
                keysToRemove.Add(key)
            End If
        Next

        For Each key As String In keysToRemove
            m_Menu.m_MenuTable.Remove(key)
        Next

        'Removes all sub menu items from the Bookmarked Views Menu
        ParentToolStripMenuItem.DropDownItems.Clear()

        'Add all current ProjInfo.BookmarkedViews to the menu
        For i As Integer = 0 To ProjInfo.BookmarkedViews.Count - 1
            If Not ProjInfo.BookmarkedViews(i).Name = "" Then
                m_Menu.AddMenu(BookmarkedViewPrefix & i, ParentMenuKey, Nothing, ProjInfo.BookmarkedViews(i).Name.trim())
            End If
        Next
    End Sub

    Private Sub ToolStripMenuLabelSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuLabelSetup.Click
        DoLabelsEdit(Legend.SelectedLayer)
    End Sub

    Private Sub ToolStripMenuCharts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuCharts.Click
        DoChartsEdit(Legend.SelectedLayer)
    End Sub

    Private Sub ToolStripMenuRelabel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuRelabel.Click
        DoLabelsRelabel(Legend.SelectedLayer)
    End Sub

    Public Sub DoLabelsEdit(ByVal Handle As Integer)
        If Not Legend.SelectedLayer = -1 AndAlso (Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.LineShapefile Or Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PointShapefile Or Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PolygonShapefile) Then
            frmMain.Plugins.BroadcastMessage("LABEL_EDIT:" + Handle.ToString())
        Else
            MsgBox("Please ensure that a shapefile layer is selected before attempting to work with labels.", MsgBoxStyle.Information, "Select Shapefile Layer")
        End If
    End Sub

    ''' <summary>
    ''' Shows form for editing charts
    ''' </summary>
    Public Sub DoChartsEdit(ByVal Handle As Integer)
        If Not Legend.SelectedLayer = -1 AndAlso _
            (Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.LineShapefile Or _
             Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PointShapefile Or _
             Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PolygonShapefile) Then
            frmMain.Plugins.BroadcastMessage("CHARTS_EDIT" + Handle.ToString())
            'frmMain.Plugins.BroadcastMessage("CHART_EDIT:" + Handle.ToString())
        Else
            MsgBox("Please ensure that a shapefile layer is selected before attempting to work with charts.", MsgBoxStyle.Information, "Select Shapefile Layer")
        End If
    End Sub

    Public Sub DoLabelsRelabel(ByVal handle As Integer)
        If Not Legend.SelectedLayer = -1 AndAlso (Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.LineShapefile Or Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PointShapefile Or Legend.Layers.ItemByHandle(Legend.SelectedLayer).Type = eLayerType.PolygonShapefile) Then
            frmMain.Plugins.BroadcastMessage("LABEL_RELABEL:" + handle.ToString())
        Else
            MsgBox("Please ensure that a shapefile layer is selected before attempting to work with labels.", MsgBoxStyle.Information, "Select Shapefile Layer")
        End If
    End Sub

    'Paul Meems 10 August 2009 to invoke the setScale form:
    Private Sub StatusBar1_PanelClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.StatusBarPanelClickEventArgs)

    End Sub

    ''' <summary>
    ''' Saves XML file the the properties of the current layer
    ''' </summary>
    Private Sub mnuSaveAsLayerFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAsLayerFile.Click
        DoSymbologyManager()
        'Is this still used?
    End Sub

    ''' <summary>
    ''' Displaying drawing options for the layer
    ''' </summary>
    ''' <param name="Handle">Handle of the layer that was clicked</param>
    ''' <remarks></remarks>
    Private Sub Legend_LayerColorboxClicked(ByVal Handle As Integer) Handles Legend.LayerColorboxClicked
        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            frmMain.Plugins.BroadcastMessage("LAYER_EDIT_SYMBOLOGY" + Handle.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Displaying drawing options for the shapefile category
    ''' </summary>
    Private Sub Legend_LayerCategoryClicked(ByVal Handle As Integer, ByVal Category As Integer) Handles Legend.LayerCategoryClicked
        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            frmMain.Plugins.BroadcastMessage("LAYER_EDIT_SYMBOLOGY" + Handle.ToString() + "!" + Category.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Ensures closure of legend editor properties if group was removed
    ''' </summary>
    Private Sub Legend_GroupRemoved(ByVal Handle As Integer) Handles Legend.GroupRemoved
        If Not m_legendEditor Is Nothing AndAlso m_legendEditor.m_groupHandle > -1 Then
            If m_legendEditor.m_groupHandle = Handle Then
                m_legendEditor.LoadProperties(-1, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Displaying drawing options for charts
    ''' </summary>
    Private Sub Legend_LayerChartClicked(ByVal Handle As Integer) Handles Legend.LayerChartClicked
        DoChartsEdit(Handle)
    End Sub

    ''' <summary>
    ''' Displaying options for chart field
    ''' </summary>
    Private Sub Legend_LayerChartFieldClicked(ByVal Handle As Integer, ByVal FieldIndex As Integer) Handles Legend.LayerChartFieldClicked
        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            frmMain.Plugins.BroadcastMessage("CHARTS_EDIT" + Handle.ToString() + "!" + FieldIndex.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Diplays shapefile categories detailed form
    ''' </summary>
    Private Sub mnuLegendShapefileCategories_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLegendShapefileCategories.Click
        DoEditCategories()
    End Sub

    ''' <summary>
    ''' Edits shapefile categories
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoEditCategories()
        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            Dim lyr As Layer = Layers(Legend.SelectedLayer)
            If lyr.LayerType = eLayerType.LineShapefile Or _
                lyr.LayerType = eLayerType.PointShapefile Or _
                lyr.LayerType = eLayerType.PolygonShapefile Then
                If Not lyr Is Nothing Then
                    frmMain.Plugins.BroadcastMessage("SHAPEFILE_CATEGORIES_EDIT" + lyr.Handle.ToString())
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Start shapefile query builder
    ''' </summary>
    Private Sub DoQueryShapefile()
        If (MapMain.ShapeDrawingMethod = MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
            Dim lyr As Layer = Layers(Legend.SelectedLayer)
            If lyr.LayerType = eLayerType.LineShapefile Or _
                lyr.LayerType = eLayerType.PointShapefile Or _
                lyr.LayerType = eLayerType.PolygonShapefile Then
                If Not lyr Is Nothing Then
                    frmMain.Plugins.BroadcastMessage("QUERY_SHAPEFILE" + lyr.Handle.ToString())
                    UpdateButtons()
                End If
            End If
        End If
    End Sub

    Public Property MapTooltipsAtLeastOneLayer() As Boolean
        Get
            Return m_MapToolTipsAtLeastOneLayer
        End Get
        Set(ByVal value As Boolean)
            m_MapToolTipsAtLeastOneLayer = value
            MapToolTipTimer.Enabled = value
            If value Then
                MapToolTipTimer.Start()
            Else
                MapToolTipTimer.Stop()
            End If
        End Set
    End Property
    ''' <summary>
    ''' The item clicked event of the contextmenu on the toolstrip.
    ''' It toggles between showing and hiding the text labels on the toolstrip
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Added by Paul Meems on April 10, 2011</remarks>
    Private Sub ContextToolstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextToolstrip.ItemClicked
        Dim contextMenuStrip As ContextMenuStrip = DirectCast(e.ClickedItem.GetCurrentParent(), ContextMenuStrip)
        Dim toolStrip As ToolStrip = TryCast(contextMenuStrip.SourceControl, ToolStrip)
        If toolStrip Is Nothing Then
            Return
        End If

        If toolStrip.Tag Is Nothing Then
            ' Set the default value
            toolStrip.Tag = "ImageAndText"
        End If

        ' Set values:
        Dim displayStyle As ToolStripItemDisplayStyle
        If toolStrip.Tag.ToString() = "Image" Then
            toolStrip.Tag = "ImageAndText"
            displayStyle = ToolStripItemDisplayStyle.ImageAndText
        Else
            toolStrip.Tag = "Image"
            displayStyle = ToolStripItemDisplayStyle.Image
        End If

        ' Change items:
        For i As Integer = 0 To toolStrip.Items.Count - 1
            toolStrip.Items(i).DisplayStyle = displayStyle
        Next
    End Sub
    ''' <summary>
    ''' Attempt to prevent the menustrip to move all over the place
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Added by Paul Meems on April 12 2011</remarks>
    Private Sub MenuStrip1_LocationChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (Me.MenuStrip1.Location.X = 0 AndAlso Me.MenuStrip1.Location.Y = 0) Then Return

        'Don't move this menustrip
        Me.MenuStrip1.Location = New Point(0, 0)
    End Sub
    ''' <summary>
    ''' Occurs when the form is first shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Added by Paul Meems on April 12, 2011</remarks>
    Private Sub MapWindowForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        ' Just calling Toolstripmanager.LoadSettings isn't enough
        ' The locations of the toolstrips must be set manually ;(
        ' Added PM on April 12, 2011:

        ' lsu: moved to modMain to hide positioning from user
        'Me.LoadToolStripSettings(Me.StripDocker)
        Me.UpdateButtons()
    End Sub

    ''' <summary>
    ''' Gets an opened instance of projection database
    ''' </summary>
    Public ReadOnly Property ProjectionDatabase() As Interfaces.IProjectionDatabase Implements Interfaces.IMapWin.ProjectionDatabase
        Get
            Return modMain.ProjectionDB
        End Get
    End Property


    Private Sub MapMain_MouseUpEvent(ByVal sender As System.Object, ByVal e As AxMapWinGIS._DMapEvents_MouseUpEvent) Handles MapMain.MouseUpEvent


        Dim tSI As MapWindow.SelectInfo
        Dim ctrlDown As Boolean

        'WS - GEM Insert and Identify
        If MapMain.NumLayers <> 0 Then
            Dim a As Double
            Dim b As Double
            MapMain.PixelToProj(CDbl(e.x), CDbl(e.y), a, b)

            If tbbAddPoint.Checked Then

                If m_Project.ProjectProjection = "" Then
                    MessageBox.Show("Please set a projection on the project file", "Set Projection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    m_Project.SetProjectProjectionByDialog()
                    If m_Project.ProjectProjection = "" Then
                        Exit Sub
                    End If
                    SetModified(True)
                End If

                AddGEMLayerIfNotPresent()

                MapMain.Redraw()
                MapMain.Refresh()

                MapWinGeoProc.SpatialReference.ProjectPoint(a, b, ProjInfo.ProjectProjection, "+proj=latlong +ellps=WGS84 +datum=WGS84")

                tbbAddPoint.Checked = False
                MapMain.MapCursor = MapMain.MapCursor.crsrArrow

                Dim newUID As String = System.Guid.NewGuid.ToString

                Call Add_GEM_Object_Feature_To_Shapefile(newUID, a, b)
                Dim detailsForm As New frmDetails(newUID, a, b)
                detailsForm.ShowDialog()
                MapMain.Redraw()
                MapMain.Refresh()
            ElseIf tbbQueryPoint.Checked Then

                If m_Project.ProjectProjection = "" Then
                    MessageBox.Show("Please set a projection on the project file", "Set Projection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    m_Project.SetProjectProjectionByDialog()
                    If m_Project.ProjectProjection = "" Then
                        Exit Sub
                    End If
                    SetModified(True)
                End If

                If GetGEMLayerID() <> -1 And Not memoryShape Is Nothing Then

                    Dim extents As New MapWinGIS.Extents

                    extents.SetBounds(a, b, 0, a, b, 0)

                    'Calculate the tolerance of 20 pixels in meters
                    Dim X1 As Double, Y1 As Double, X2 As Double, Y2 As Double, tolerance As Double
                    MapMain.PixelToProj(0, 0, X1, Y1)
                    MapMain.PixelToProj(40, 40, X2, Y2)
                    tolerance = Math.Abs(X2 - X1)

                    Dim ids As New Object
                    Dim Result = memoryShape.SelectShapes(extents, tolerance, MapWinGIS.SelectMode.INTERSECTION, ids)

                    If ids.Length Then
                        tbbQueryPoint.Checked = False
                        Dim clickedObj_UID As String = memoryShape.Table.CellValue(memoryShape.Table.FieldIndexByName("OBJ_UID"), ids(0))
                        Dim detailsForm As New frmDetails(clickedObj_UID)
                        detailsForm.ShowDialog()
                        MapMain.Redraw()
                        MapMain.Refresh()
                    End If
                End If
            ElseIf MapMain.CursorMode = MapWinGIS.tkCursorMode.cmPan Or MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn Or MapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut Then
                m_PreviewMap.UpdateLocatorBox()
            ElseIf (MapMain.CursorMode = MapWinGIS.tkCursorMode.cmSelection) And (e.button = vbLeftButton) Then
                If m_PluginManager.MapMouseUp(e.button, e.shift, e.x, e.y) = False Then
                    'dpa 02/15/02
                    'Chris M 12/13/2006 for Bugzilla 378 -- change the "OrElse" in the statement below to "And".
                    If Layers(Legend.SelectedLayer).LayerType <> eLayerType.Image And Layers(Legend.SelectedLayer).LayerType <> eLayerType.Grid Then
                        If (e.shift = 2) Or (e.shift = 3) Then ctrlDown = True Else ctrlDown = False
                        tSI = m_View.SelectShapesByPoint(e.x, e.y, ctrlDown)

                        m_PluginManager.ShapesSelected(Legend.SelectedLayer, tSI)
                        UpdateButtons()
                        'End If
                    End If
                End If
                Exit Sub
            End If
        End If

        m_PluginManager.MapMouseUp(e.button, e.shift, e.x, e.y)
        ' End If
    End Sub


    Friend Sub AddGPSTools()
        If Not m_PluginManager.PluginIsLoaded("mwGPS_mwGPS") Then
            doPluginNameClick("mwGPS_mwGPS")
        End If
    End Sub

    Friend Sub AddGEMLayerIfNotPresent()
        Dim gemLayerID As Integer = GetGEMLayerID()
        If gemLayerID = -1 Then
            'GEM Layer not found so load a new one
            memoryShape = CreateShapefileAndImportData()
            m_layers.AddLayer(memoryShape, "GEM Observations")
            gemLayerID = GetGEMLayerID()
            If gemLayerID <> -1 Then
                m_layers(gemLayerID).Color = Color.Yellow
            End If
        Else
            'TODO: what if the tmpfile doesn't exist anymore?!
            memoryShape = MapMain.get_GetObject(gemLayerID)
        End If

        UpdateButtons()
        MapMain.Redraw()
        MapMain.Refresh()

    End Sub

    Friend Function GetGEMLayerID() As Integer

        For layerid As Integer = 0 To Legend.Layers.Count - 1
            If (MapMain.get_LayerName(layerid).ToString.Contains("GEM Observations")) Then
                Return layerid
                Exit For
            End If
        Next
        Return -1

    End Function


    Public Function CreateShapefileAndImportData() As MapWinGIS.Shapefile
        '
        ' Name: GEM_Object_Shapefile
        ' Purpose: To Create a Point shapefile with the same structure as GEM_OBJECT.
        ' Written: K. Adlam, 09/08/12
        '
        ' Create point shapefile (now temp shapefile as memory shapefile caused issues with attribute editor
        '
        'Dim tempShapefile As String = IO.Path.Combine(IO.Path.GetTempPath, "gem_temp.shp")
        'If (Not DeleteShapefile(tempShapefile)) Then ' In case of failure to delete
        '    tempShapefile = IO.Path.GetTempFileName()
        '    tempShapefile = IO.Path.ChangeExtension(tempShapefile, ".shp")
        'End If

        tempShapeFile = IO.Path.Combine(IO.Path.GetTempPath, IO.Path.GetRandomFileName)
        tempShapeFile = IO.Path.ChangeExtension(tempShapeFile, ".shp")
        '
        ' Create new shapefile
        '
        memoryShape = New MapWinGIS.Shapefile
        memoryShape.CreateNew(tempShapefile, MapWinGIS.ShpfileType.SHP_POINT)
        memoryShape.GeoProjection = ProjInfo.GeoProjection
        'memoryShape.GeoProjection = Project.GeoProjection


        'memoryShape.FastMode = True
        'memoryShape.CacheExtents = True
        Try
            '
            ' Start editing
            '
            memoryShape.StartEditingShapes(True)
            memoryShape.EditInsertField(MakeField("OBJ_UID", MapWinGIS.FieldType.STRING_FIELD, 36), memoryShape.NumFields)
            memoryShape.EditInsertField(MakeField("PROJECT_UID", MapWinGIS.FieldType.STRING_FIELD, 36), memoryShape.NumFields)
            '  memoryShape.EditInsertField(MakeField("X", MapWinGIS.FieldType.DOUBLE_FIELD), memoryShape.NumFields)
            ' memoryShape.EditInsertField(MakeField("Y", MapWinGIS.FieldType.DOUBLE_FIELD), memoryShape.NumFields)



            Call gemdb.RefreshGEMDataTableContents()
            '
            ' Loop through the GEM objects
            '
            Dim irow As Integer = 0
            For Each row As GEMDataset.GEM_OBJECTRow In gemdb.Dataset.GEM_OBJECT
                Dim shp As MapWinGIS.Shape = MakePointShape(row.X, row.Y)
                memoryShape.EditInsertShape(shp, memoryShape.NumShapes) 'Insert shape
                '
                ' add attribute fields
                '
                memoryShape.EditCellValue(0, irow, row.OBJ_UID)
                memoryShape.EditCellValue(1, irow, row.PROJ_UID)
                ' memoryShape.EditCellValue(3, irow, row.X)
                ' memoryShape.EditCellValue(4, irow, row.Y)
                irow = irow + 1
            Next
            '
            'Stop editing the shapefile and save changes
            '
            memoryShape.StopEditingShapes(True, True)


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        Return memoryShape
        '
    End Function

    'Private Function DeleteShapefile(ByVal strPath As String) As Boolean

    '    Try
    '        Dim strDirectory As String = IO.Path.GetDirectoryName(strPath)
    '        Dim strFile As String = IO.Path.GetFileNameWithoutExtension(strPath)
    '        Dim di As IO.DirectoryInfo = New DirectoryInfo(strDirectory)
    '        For Each fi As FileInfo In di.GetFiles(strFile & ".*", SearchOption.TopDirectoryOnly)
    '            IO.File.Delete(fi.FullName)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try


    'End Function

    'Public Sub Modify_GEM_Object_Feature_To_Shapefile(ByVal strObjectUid As String)
    '    '
    '    ' Name: Mofify_GEM_Object_Feature_To_Shapefile
    '    ' Purpose: To add a feature to GEM memory shapefile.
    '    ' Written: K. Adlam, 10/08/12
    '    '
    '    Try
    '        '
    '        ' Start editing
    '        '
    '        memoryShape.StartEditingShapes(True)
    '        '
    '        ' Get GEM_OBJECT by ID
    '        '
    '        Dim gemObject As GEMDataset.GEM_OBJECTRow = (From gemObj In gemdb.Dataset.GEM_OBJECT Where gemObj.OBJ_UID = "1" Select gemObj).First
    '        '
    '        ' Get matching record in memory shapefile
    '        '
    '        Dim strQuery As String = "[OBJECT_UID] = ""1"""
    '        Dim result As Object = Nothing
    '        Dim errorString As String = ""
    '        memoryShape.Table.Query(strQuery, result, errorString)
    '        '
    '        ' Modifyshape
    '        '
    '        Dim irow As Long = 0

    '        Dim shp As MapWinGIS.Shape = MakePointShape(gemObject.X, gemObject.Y)
    '        memoryShape.EditInsertShape(shp, irow) 'Insert shape
    '        '
    '        ' add attribute fields
    '        '
    '        memoryShape.EditCellValue(0, irow, gemObject.OBJ_UID)
    '        memoryShape.EditCellValue(1, irow, gemObject.PROJECT_UID)
    '        memoryShape.EditCellValue(3, irow, gemObject.X)
    '        memoryShape.EditCellValue(4, irow, gemObject.Y)

    '        '
    '        'Stop editing the shapefile and save changes
    '        '
    '        memoryShape.StopEditingShapes(True, True)


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    '    '
    'End Sub

    Function MakeField(ByVal strName As String, ByVal pType As MapWinGIS.FieldType, Optional ByVal width As Integer = 0) As MapWinGIS.Field
        Dim pField As New MapWinGIS.Field
        pField.Name = strName
        pField.Type = pType
        If (width <> 0) Then
            pField.Width = width
        End If
        Return pField
    End Function



    Public Sub Add_GEM_Object_Feature_To_Shapefile(ByVal uid As String, ByVal x As Double, ByVal y As Double)

        'TODO: Make point yellow and refresh map

        Try
            '
            ' Start editing
            '
            memoryShape.StartEditingShapes(True)
            '
            ' Add shape
            '
            Dim irow As Long = memoryShape.NumShapes
            Dim shp As MapWinGIS.Shape = MakePointShape(x, y)
            memoryShape.EditInsertShape(shp, irow) 'Insert shape
            '
            ' add attribute fields
            '
            memoryShape.EditCellValue(0, irow, uid)
            memoryShape.EditCellValue(1, irow, gemdb.CurrentProjectUID)
            ' memoryShape.EditCellValue(2, irow, x)
            ' memoryShape.EditCellValue(3, irow, y)
            '
            'Stop editing the shapefile and save changes
            '
            memoryShape.StopEditingShapes(True, True)

            MapMain.Redraw()
            MapMain.Refresh()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        '
    End Sub

    Function MakePointShape(ByVal x As Double, ByVal y As Double) As MapWinGIS.Shape
        '
        ' Name: MakePointShape
        ' Purpose: To create a point shape
        ' Written: K. Adlam, 18/11/11
        '
        Dim proj As MapWinGIS.GeoProjection = New MapWinGIS.GeoProjection
        'proj.ImportFromEPSG(4326)

        MapWinGeoProc.SpatialReference.ProjectPoint(x, y, "+proj=latlong +ellps=WGS84 +datum=WGS84", ProjInfo.ProjectProjection)

        Dim pnt As New MapWinGIS.Point()
        pnt.x = x
        pnt.y = y
        Dim shp As New MapWinGIS.Shape()
        shp.Create(MapWinGIS.ShpfileType.SHP_POINT)
        shp.InsertPoint(pnt, 0)
        Return shp
        ''
    End Function


    Private Sub tbbExportData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbExportData.Click
        Dim pform As New frmDataManagement
        pform.ShowDialog()

    End Sub

    Private Sub panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panel1.Paint

    End Sub


    Private Sub tbbAddPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbAddPoint.Click
        MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        tbbQueryPoint.Checked = False
       
    End Sub

    Private Sub tbbQueryPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbQueryPoint.Click
        MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        tbbAddPoint.Checked = False

    End Sub
End Class

' Chris Michaelis August 20 2005 - to Sort the Plugins menu
Friend Class StringPairSorter
    Implements System.Collections.IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        If CType(x, String())(0) > CType(x, String())(1) Then Return 1
        If CType(x, String())(0) < CType(x, String())(1) Then Return -1
        If CType(x, String())(0) = CType(x, String())(1) Then Return 0
    End Function

End Class

Public Enum GeoTIFFAndImgBehavior
    LoadAsImage = 0
    LoadAsGrid = 1
    Automatic = 2
End Enum

Public Enum ESRIBehavior
    LoadAsImage = 0
    LoadAsGrid = 1
End Enum

Public Enum MouseWheelZoomDir
    WheelUpZoomsIn = 0
    WheelUpZoomsOut = 1
    NoAction = 2
End Enum

