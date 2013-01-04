'********************************************************************************************************
'File Name: mwGPS.vb
'Description: This screen is used to specify parameters for adding a new field.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'July 17, 2007: Allen Anselmo allen.anselmo@gmail.com - 
'               Added licensing and comments to original code

Imports System.IO
Imports System.windows.Forms
Imports System.Collections.Generic
Public Class mwGPS
    Implements MapWindow.Interfaces.IPlugin

#Region "Private Variables"
    'Used for removing items on terminate
    Private _addedButtons As New System.Collections.Stack()
    Private _addedMenus As New System.Collections.Stack()

    'Used for drawing gps location
    Private _oldLat As Double = -999
    Private _oldLon As Double = -999
    Private _forcePanDraw As Boolean = False
    Private _MinPanDistance As Integer = 1
    Private _PanExtent As New MapWinGIS.Extents
    Private _trackCount As Integer = 0
    Private _trackCountToSave As Integer = 10
    Private _trackLineLastPt As MapWinGIS.Point

    Private _TrackPointLayer As MapWindow.Interfaces.Layer = Nothing
    Private _TrackLineLayer As MapWindow.Interfaces.Layer = Nothing

    Private _MeasureCursor As Cursor = Nothing

    'Private _tmpTesting As Boolean = False
#End Region

#Region "Unused Plug-in Interface Elements"


    <CLSCompliant(False)> _
    Public Sub LegendDoubleClick(ByVal Handle As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.LegendDoubleClick

    End Sub

    <CLSCompliant(False)> _
    Public Sub LegendMouseDown(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.LegendMouseDown

    End Sub

    <CLSCompliant(False)> _
    Public Sub LegendMouseUp(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.LegendMouseUp

    End Sub

    Public Sub MapDragFinished(ByVal Bounds As System.Drawing.Rectangle, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.MapDragFinished

    End Sub

    Public Sub MapMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.MapMouseUp

    End Sub

    Public Sub Message(ByVal msg As String, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.Message

    End Sub

    Public Sub ProjectSaving(ByVal ProjectFile As String, ByRef SettingsString As String) Implements MapWindow.Interfaces.IPlugin.ProjectSaving
    End Sub


    <CLSCompliant(False)> _
    Public Sub ShapesSelected(ByVal Handle As Integer, ByVal SelectInfo As MapWindow.Interfaces.SelectInfo) Implements MapWindow.Interfaces.IPlugin.ShapesSelected

    End Sub



    Public Sub LayerSelected(ByVal Handle As Integer) Implements MapWindow.Interfaces.IPlugin.LayerSelected

    End Sub

    Public Sub ProjectLoading(ByVal ProjectFile As String, ByVal SettingsString As String) Implements MapWindow.Interfaces.IPlugin.ProjectLoading

    End Sub

    Public Sub MapMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.MapMouseDown

    End Sub

    Public Sub MapMouseMove(ByVal ScreenX As Integer, ByVal ScreenY As Integer, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.MapMouseMove
    End Sub

#End Region

#Region "Plug-in Information"
    Public ReadOnly Property Name() As String Implements MapWindow.Interfaces.IPlugin.Name
        Get
            Return "GPS Tools"
        End Get
    End Property

    Public ReadOnly Property Description() As String Implements MapWindow.Interfaces.IPlugin.Description
        Get
            Return "This tool allows for COM-connected GPS units to be used with MapWindow. Primary developer: Allen Anselmo"
        End Get
    End Property

    Public ReadOnly Property Author() As String Implements MapWindow.Interfaces.IPlugin.Author
        Get
            Return "ISU Geosciences"
        End Get
    End Property

    Public ReadOnly Property Version() As String Implements MapWindow.Interfaces.IPlugin.Version
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(Me.GetType.Assembly.Location).FileVersion
        End Get
    End Property

    Public ReadOnly Property BuildDate() As String Implements MapWindow.Interfaces.IPlugin.BuildDate
        Get
            Return System.IO.File.GetLastWriteTime(Me.GetType.Assembly.Location)
        End Get
    End Property

    Public ReadOnly Property SerialNumber() As String Implements MapWindow.Interfaces.IPlugin.SerialNumber
        Get
            Return "O+CW7YC/96VDT+A"
            ' This is matched to the author name in a serial number issuing program Dan ran.
        End Get
    End Property

#End Region

#Region "Start and Stop Methods"

    ''' <summary>
    ''' Event triggered on execution of the plugin
    ''' </summary>
    ''' <param name="MapWin"></param>
    ''' <param name="ParentHandle"></param>
    ''' <remarks></remarks>
    <CLSCompliant(False)> _
    Public Sub Initialize(ByVal MapWin As MapWindow.Interfaces.IMapWin, ByVal ParentHandle As Integer) Implements MapWindow.Interfaces.IPlugin.Initialize
        g_MapWin = MapWin  '  This sets global for use elsewhere in program
        g_handle = ParentHandle
        g_StatusBar = g_MapWin.StatusBar.AddPanel("", 2, 10, Windows.Forms.StatusBarPanelAutoSize.Spring)

        Dim tempPtr As System.IntPtr = ParentHandle
        Dim mapFrm As System.Windows.Forms.Form = System.Windows.Forms.Control.FromHandle(tempPtr)
        mapFrm.AddOwnedForm(g_DisplayFrm)

        g_Settings.Initialize()
        g_Settings.LoadConfig()

        addMenus()
        addToolbars()

        'Added to save time debugging when no GPS connection available and thus the prompt is pointless
#If DEBUG Then
        g_promptIfNotConnected = False
#End If

        g_GPS.InitializeCom()

        'handlers for core GPS functionality
        AddHandler g_GPS.NMEASentenceReceived, AddressOf GPSControllerNMEASentenceReceived
        AddHandler g_GPS.BasicInfoUpdated, AddressOf GPSControllerBasicInfoUpdated
        g_GPS.StartReceiving()
    End Sub

    ''' <summary>
    ''' Event triggered on termination of the plugin
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Terminate() Implements MapWindow.Interfaces.IPlugin.Terminate
        If g_DisplayFrm.Visible Then
            g_DisplayFrm.Close()
        End If
        g_GPS.StopReceiving()
        'Gives it a quarter of a second for the GPS to stop receiving
        System.Threading.Thread.Sleep(250)
        RemoveHandler g_GPS.NMEASentenceReceived, AddressOf GPSControllerNMEASentenceReceived
        RemoveHandler g_GPS.BasicInfoUpdated, AddressOf GPSControllerBasicInfoUpdated

        g_MapWin.StatusBar.RemovePanel(g_StatusBar)

        Dim t As MapWindow.Interfaces.Toolbar = g_MapWin.Toolbar
        While (_addedButtons.Count > 0)
            Try
                t.RemoveButton(_addedButtons.Pop().ToString())
            Catch ex As Exception

            End Try
        End While
        t.RemoveToolbar(g_ToolBarName)

        While (_addedMenus.Count > 0)
            Try
                g_MapWin.Menus.Remove(_addedMenus.Pop().ToString())
            Catch ex As Exception

            End Try
        End While

        If g_GPS.IsConnected Then
            g_GPS.ClosePort()
        End If
        g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
        If Not g_TrackSF Is Nothing Then
            g_SaveTracks = False
            g_TrackSF.Projection = g_MapWin.Project.ProjectProjection
            g_TrackSF.StopEditingShapes()
            g_TrackSF.Close()
            g_TrackSF = Nothing
        End If
        g_Settings.SaveConfig()
    End Sub

#End Region

#Region "Used Interface Methods"

    ''' <summary>
    ''' Triggered when a menu or toolbar item is clicked
    ''' </summary>
    ''' <param name="ItemName"></param>
    ''' <param name="Handled"></param>
    ''' <remarks></remarks>
    Public Sub ItemClicked(ByVal ItemName As String, ByRef Handled As Boolean) Implements MapWindow.Interfaces.IPlugin.ItemClicked
        Select Case ItemName
            Case g_mnuCOMConnect, g_btnConnect
                doComClick()
            Case g_mnuGPSSettings, g_btnGPSSettings
                doGPSSettingsClick()
            Case g_mnuDisplayGPSInfo, g_btnDisplay
                doDisplayClick()
            Case g_mnuPanCheck, g_btnPan
                doPanClick()
            Case g_mnuCenterOnPanCheck, g_btnCenterOnPan
                doCenterOnPanClick()
            Case g_mnuDrawPointCheck, g_btnDrawPoint
                doDrawClick()
            Case g_mnuArrowCheck, g_btnDrawArrow
                doArrowCheckClick()
            Case g_mnuLogTracks
                doLogTracksClick()
            Case g_mnuLogNmea
                doLogNmeaClick()
            Case "tmp"
                doTmp()
        End Select
    End Sub

    ''' <summary>
    ''' Triggered when map extents changed. used here to default the extents so that it will recenter correctly if drawing a GPS position
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MapExtentsChanged() Implements MapWindow.Interfaces.IPlugin.MapExtentsChanged
        If g_Settings.PanWithGPS Then
            'Will ensure that the map recenters correctly if drawing GPS location
            _PanExtent.SetBounds(0, 0, 0, 0, 0, 0)
        End If
    End Sub

    Public Sub LayerRemoved(ByVal Handle As Integer) Implements MapWindow.Interfaces.IPlugin.LayerRemoved
        _TrackPointLayer = Nothing
        _TrackLineLayer = Nothing
        Try
            For i As Integer = 0 To g_MapWin.Layers.NumLayers - 1
                Dim currpath As String = g_MapWin.Layers.Item(i).FileName
                If currpath = g_Settings.TrackPointsLogPath Then
                    _TrackPointLayer = g_MapWin.Layers.Item(i)
                End If

                If currpath = g_Settings.TrackLineLogPath Then
                    _TrackLineLayer = g_MapWin.Layers.Item(i)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    <CLSCompliant(False)> _
    Public Sub LayersAdded(ByVal Layers() As MapWindow.Interfaces.Layer) Implements MapWindow.Interfaces.IPlugin.LayersAdded
        _TrackPointLayer = Nothing
        _TrackLineLayer = Nothing
        For i As Integer = 0 To g_MapWin.Layers.NumLayers - 1
            Dim currpath As String = g_MapWin.Layers.Item(i).FileName
            If currpath = g_Settings.TrackPointsLogPath Then
                _TrackPointLayer = g_MapWin.Layers.Item(i)
            End If

            If currpath = g_Settings.TrackLineLogPath Then
                _TrackLineLayer = g_MapWin.Layers.Item(i)
            End If
        Next
    End Sub

    Public Sub LayersCleared() Implements MapWindow.Interfaces.IPlugin.LayersCleared
        _TrackPointLayer = Nothing
        _TrackLineLayer = Nothing
    End Sub
#End Region

#Region "Helper Functions"

#Region "   Menu/Toolbar Items"
    ''' <summary>
    ''' Sub used to add all the menus used by the mwGPS plugin
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addMenus()
        Dim nil As Object
        nil = Nothing
        With g_MapWin.Menus
            .AddMenu(g_mnuGPSMain, nil, "GPS Tools")
            _addedMenus.Push(g_mnuGPSMain)
            .AddMenu(g_mnuDrawPointCheck, g_mnuGPSMain, nil, "Draw GPS Location on Map")
            .Item(g_mnuDrawPointCheck).Checked = g_Settings.DrawLocation
            _addedMenus.Push(g_mnuDrawPointCheck)
            .AddMenu(g_mnuArrowCheck, g_mnuGPSMain, nil, "Draw GPS Location as Arrow")
            .Item(g_mnuArrowCheck).Checked = g_Settings.DrawArrow
            _addedMenus.Push(g_mnuArrowCheck)
            .AddMenu(g_mnuPanCheck, g_mnuGPSMain, nil, "Pan Map with GPS Location")
            .Item(g_mnuPanCheck).Checked = g_Settings.PanWithGPS
            _addedMenus.Push(g_mnuPanCheck)
            .AddMenu(g_mnuCenterOnPanCheck, g_mnuGPSMain, nil, "Center on GPS Location when Panning")
            .Item(g_mnuCenterOnPanCheck).Checked = g_Settings.CenterOnPan
            _addedMenus.Push(g_mnuCenterOnPanCheck)
            .AddMenu(g_mnuDivider1, g_mnuGPSMain, nil, "-")
            _addedMenus.Push(g_mnuDivider1)
            .AddMenu(g_mnuLogTracks, g_mnuGPSMain, nil, "Start Logging Tracks")
            .Item(g_mnuLogTracks).Checked = False
            _addedMenus.Push(g_mnuLogTracks)
            .AddMenu(g_mnuLogNmea, g_mnuGPSMain, nil, "Start Logging NMEA Sentences")
            .Item(g_mnuLogNmea).Checked = False
            _addedMenus.Push(g_mnuLogNmea)
            .AddMenu(g_mnuDivider5, g_mnuGPSMain, nil, "-")
            _addedMenus.Push(g_mnuDivider5)
            .AddMenu(g_mnuCOMConnect, g_mnuGPSMain, nil, "Connect to a GPS Unit")
            _addedMenus.Push(g_mnuCOMConnect)
            .AddMenu(g_mnuGPSSettings, g_mnuGPSMain, nil, "GPS Settings")
            _addedMenus.Push(g_mnuGPSSettings)
            .AddMenu(g_mnuDisplayGPSInfo, g_mnuGPSMain, nil, "Display GPS Information")
            _addedMenus.Push(g_mnuDisplayGPSInfo)

        End With
    End Sub

    ''' <summary>
    ''' Sub used to add all toolbars and buttons used by the mwGPS plugin
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addToolbars()
        g_MapWin.Toolbar.AddToolbar(g_ToolBarName)
        If g_showSettingButtons Then
            Dim tlbrbtnDrawPoint As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnDrawPoint, g_ToolBarName, "", "")
            Dim drawpoint_ico As New Drawing.Icon(Me.GetType, "DrawGPSLoc.ico")
            tlbrbtnDrawPoint.Picture = drawpoint_ico
            tlbrbtnDrawPoint.Tooltip = "Draw GPS Location on Map"
            tlbrbtnDrawPoint.Pressed = True
            _addedButtons.Push(g_btnDrawPoint)

            Dim tlbrbtnDrawArrow As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnDrawArrow, g_ToolBarName, "", "")
            Dim drawarrow_ico As New Drawing.Icon(Me.GetType, "DrawGPSArrow.ico")
            tlbrbtnDrawArrow.Picture = drawarrow_ico
            tlbrbtnDrawArrow.Tooltip = "Draw GPS Location as Arrow"
            tlbrbtnDrawArrow.Pressed = True
            _addedButtons.Push(g_btnDrawArrow)

            Dim tlbrbtnPan As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnPan, g_ToolBarName, "", "")
            Dim pan_ico As New Drawing.Icon(Me.GetType, "PanWithLoc.ico")
            tlbrbtnPan.Picture = pan_ico
            tlbrbtnPan.Tooltip = "Pan Map with GPS Location"
            tlbrbtnPan.Pressed = True
            _addedButtons.Push(g_btnPan)

            Dim tlbrbtnCenterOnPan As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnCenterOnPan, g_ToolBarName, "", "")
            Dim centeronpan_ico As New Drawing.Icon(Me.GetType, "CenterOnPan.ico")
            tlbrbtnCenterOnPan.Picture = centeronpan_ico
            tlbrbtnCenterOnPan.Tooltip = "Center on GPS Location when Panning"
            tlbrbtnCenterOnPan.Pressed = True
            _addedButtons.Push(g_btnCenterOnPan)

            g_MapWin.Toolbar.AddButtonDropDownSeparator(g_btnDiv1, g_ToolBarName, "")
            _addedButtons.Push(g_btnDiv1)
        End If

        Dim tlbrbtnGPSConnect As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnConnect, g_ToolBarName, "", "")
        Dim connect_ico As New Drawing.Icon(Me.GetType, "FormConnection.ico")
        tlbrbtnGPSConnect.Picture = connect_ico
        tlbrbtnGPSConnect.Tooltip = "Form Connection with GPS Unit"
        _addedButtons.Push(g_btnConnect)

        Dim tlbrbtnLogManager As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnGPSSettings, g_ToolBarName, "", "")
        Dim log_ico As New Drawing.Icon(Me.GetType, "DataLogging.ico")
        tlbrbtnLogManager.Picture = log_ico
        tlbrbtnLogManager.Tooltip = "GPS Settings"
        _addedButtons.Push(g_btnGPSSettings)

        Dim tlbrbtnDisplayGPSInfo As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton(g_btnDisplay, g_ToolBarName, "", "")
        Dim display_ico As New Drawing.Icon(Me.GetType, "DisplayGPSInfo.ico")
        tlbrbtnDisplayGPSInfo.Picture = display_ico
        tlbrbtnDisplayGPSInfo.Tooltip = "Display GPS Information Form"
        _addedButtons.Push(g_btnDisplay)


        'Dim tlbrbtntmp As MapWindow.Interfaces.ToolbarButton = g_MapWin.Toolbar.AddButton("tmp", g_ToolBarName, "", "")
        '_addedButtons.Push("tmp")
    End Sub

#End Region

#Region "   Itemclicked Items"
    Private Sub doTmp()
        'MapWinGeoProc.SpatialOperations.SpatialJoin("D:\dev\zSampleData\GPS Data\Edits\My_edit.shp", "D:\dev\zSampleData\GPS Data\Edits\My_edit2.shp", "D:\dev\zSampleData\GPS Data\Edits\join_output.shp", MapWinGeoProc.SpatialJoinTypes.Nearest)
        '_tmpTesting = Not _tmpTesting
        'g_MapWin.Toolbar.ButtonItem("tmp").Pressed = _tmpTesting
        'If _tmpTesting Then
        '    g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmNone
        'Else
        '    g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn
        'End If
    End Sub

    ''' <summary>
    ''' Used to open the connect to GPS form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doComClick()
        Dim COMForm As New frmCOMSettings()
        COMForm.CloseOnConnect = False
        COMForm.ShowDialog()
    End Sub

    ''' <summary>
    ''' used to open the Display GPS information form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doDisplayClick()
        checkConnect()
        If g_DisplayFrm Is Nothing Or g_DisplayFrm.IsDisposed Then
            g_DisplayFrm = New frmDisplayGPS
            Dim tempPtr As System.IntPtr = g_handle
            Dim mapFrm As System.Windows.Forms.Form = System.Windows.Forms.Control.FromHandle(tempPtr)
            mapFrm.AddOwnedForm(g_DisplayFrm)
        End If
        g_DisplayFrm.Show()
    End Sub

    ''' <summary>
    ''' Used to open the log manager form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doGPSSettingsClick()
        Dim gpssettingsFrm As New frmGPSSettings
        Dim tempPtr As System.IntPtr = g_handle
        Dim mapFrm As System.Windows.Forms.Form = System.Windows.Forms.Control.FromHandle(tempPtr)
        mapFrm.AddOwnedForm(gpssettingsFrm)

        gpssettingsFrm.ShowDialog()
    End Sub

    ''' <summary>
    ''' Used to change the pan with location property
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doPanClick()
        Dim currChecked As Boolean
        currChecked = g_Settings.PanWithGPS
        g_MapWin.Menus.Item(g_mnuPanCheck).Checked = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnPan).Pressed = Not currChecked
        End If

        g_MapWin.Menus.Item(g_mnuCenterOnPanCheck).Enabled = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnCenterOnPan).Enabled = Not currChecked
        End If


        g_Settings.PanWithGPS = Not currChecked
        If currChecked Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
        Else
            _forcePanDraw = True
        End If
    End Sub

    ''' <summary>
    ''' Used to change the center on pan property
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doCenterOnPanClick()
        Dim currChecked As Boolean
        currChecked = g_Settings.CenterOnPan
        g_MapWin.Menus.Item(g_mnuCenterOnPanCheck).Checked = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnCenterOnPan).Pressed = Not currChecked
        End If
        g_Settings.CenterOnPan = Not currChecked
        If currChecked Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
        Else
            _forcePanDraw = True
        End If
    End Sub

    ''' <summary>
    ''' used to change the draw GPS location property
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doDrawClick()
        Dim currChecked As Boolean

        currChecked = g_Settings.DrawLocation
        g_MapWin.Menus.Item(g_mnuDrawPointCheck).Checked = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnDrawPoint).Pressed = Not currChecked
        End If

        g_MapWin.Menus.Item(g_mnuArrowCheck).Enabled = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnDrawArrow).Enabled = Not currChecked
        End If

        g_Settings.DrawLocation = Not currChecked
        If currChecked Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
        Else
            _forcePanDraw = True
        End If
    End Sub

    ''' <summary>
    ''' Used to change the draw GPS location as arrow property
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doArrowCheckClick()
        Dim currChecked As Boolean
        currChecked = g_Settings.DrawArrow

        g_MapWin.Menus.Item(g_mnuArrowCheck).Checked = Not currChecked
        If g_showSettingButtons Then
            g_MapWin.Toolbar.ButtonItem(g_btnDrawArrow).Pressed = Not currChecked
        End If

        g_Settings.DrawArrow = Not currChecked
        If currChecked Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
        Else
            _forcePanDraw = True
        End If
    End Sub

    Private Sub doLogTracksClick()
        If g_Settings.TrackPointsLogPath <> "" Then
            Dim currChecked As Boolean
            currChecked = g_SaveTracks
            g_MapWin.Menus.Item(g_mnuLogTracks).Checked = Not currChecked

            If Not g_SaveTracks Then
                g_TrackSF = New MapWinGIS.Shapefile
                g_TrackSF.Open(g_Settings.TrackPointsLogPath)
                g_TrackSF.StartEditingShapes()
                g_TrackLineSF = New MapWinGIS.Shapefile
                g_TrackLineSF.Open(g_Settings.TrackLineLogPath)
                g_TrackLineSF.StartEditingShapes()
                g_MapWin.Menus.Item(g_mnuLogTracks).Text = "Stop Logging Tracks"
            Else
                g_TrackSF.StopEditingShapes()
                g_TrackSF.Close()
                g_TrackLineSF.StopEditingShapes()
                g_TrackLineSF.Close()
                _trackLineLastPt = Nothing
                g_MapWin.Menus.Item(g_mnuLogTracks).Text = "Start Logging Tracks"
            End If

            g_SaveTracks = Not currChecked
        Else
            MsgBox("You must specify a log path in the GPS Settings form to log.", MsgBoxStyle.Exclamation, "mwGPS Error")
        End If
    End Sub

    Private Sub doLogNmeaClick()
        If g_Settings.NMEALogPath <> "" Then
            Dim currChecked As Boolean
            currChecked = g_SaveNmea
            g_MapWin.Menus.Item(g_mnuLogNmea).Checked = Not currChecked


            If Not g_SaveNmea Then
                g_MapWin.Menus.Item(g_mnuLogNmea).Text = "Stop Logging NMEA Sentences"
            Else
                g_MapWin.Menus.Item(g_mnuLogNmea).Text = "Start Logging NMEA Sentences"
            End If

            g_SaveNmea = Not currChecked
        Else
            MsgBox("You must specify a log path in the GPS Settings form to log.", MsgBoxStyle.Exclamation, "mwGPS Error")
        End If
    End Sub
#End Region

#Region "   Drawing Items"
    ''' <summary>
    ''' used to set a user defined cursor, which for some reason cuts down on flickering during refreshing a drawing layer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setCustomCursor()
        If g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmNone Or g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmSelection Then
            _MeasureCursor = New Cursor(Me.GetType, "drawLine.ico")
        ElseIf g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmPan Then
            _MeasureCursor = New Cursor(Me.GetType, "hand.ico")
        ElseIf g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn Then
            _MeasureCursor = New Cursor(Me.GetType, "zoom_in_16_2.ico")
        ElseIf g_MapWin.View.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut Then
            _MeasureCursor = New Cursor(Me.GetType, "zoom_out_16.ico")
        End If
        g_MapWin.View.UserCursorHandle = _MeasureCursor.Handle
        g_MapWin.View.MapCursor = MapWinGIS.tkCursor.crsrUserDefined
    End Sub

#End Region

#Region "Misc Items"
    ''' <summary>
    ''' Checks if the GPS is connected and prompts the user with the connect form if not
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkConnect()
        If g_promptIfNotConnected Then
            If Not g_GPS.IsConnected Then
                Dim COMForm As New frmCOMSettings()
                COMForm.CloseOnConnect = True
                COMForm.ShowDialog()
            End If
        End If
    End Sub

#End Region
#End Region

#Region "GPS Event Handlers"
    ''' <summary>
    ''' Triggered when the GPS receives a NMEA string. used for logging NMEA input
    ''' </summary>
    ''' <param name="Sentence"></param>
    ''' <remarks></remarks>
    Private Sub GPSControllerNMEASentenceReceived(ByVal Sentence As String)
        If g_SaveNmea Then
            File.AppendAllText(g_Settings.NMEALogPath, Sentence + vbCrLf)
        End If
    End Sub

    ''' <summary>
    ''' Triggered when the GPS settings have all been updated. In this case used for track logging and drawing of GPS location
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GPSControllerBasicInfoUpdated()
        Try
            If g_GPS.HasFix Then
                'Set custom cursor stuff to make less flickering
                Dim currCursorMode As MapWinGIS.tkCursorMode = MapWinGIS.tkCursorMode.cmNone
                If g_MapWin.View.CursorMode <> MapWinGIS.tkCursorMode.cmNone Then
                    currCursorMode = g_MapWin.View.CursorMode
                End If
                setCustomCursor()

                'Project the current GPS location
                Dim ProjectedX, ProjectedY As Double
                ProjectedX = g_GPS.LongitudeDD
                ProjectedY = g_GPS.LatitudeDD
                If g_MapWin.Project.ProjectProjection <> "" Then
                    MapWinGeoProc.SpatialReference.ProjectPoint(ProjectedX, ProjectedY, "+proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs", g_MapWin.Project.ProjectProjection)
                End If

                'If no layers added, then no point to try and draw on them
                If g_MapWin.Layers.NumLayers > 0 Then
                    'Only mess with panning if panning turned on and then either center on or shift by direction accordingly
                    If g_Settings.PanWithGPS Then
                        If g_Settings.CenterOnPan Then
                            If ProjectedX > _PanExtent.xMax Or ProjectedX < _PanExtent.xMin Or ProjectedY > _PanExtent.yMax Or ProjectedY < _PanExtent.yMin Then
                                centerOnPoint(ProjectedX, ProjectedY)
                            End If
                        Else
                            If _PanExtent.xMax = 0 Then
                                centerOnPoint(ProjectedX, ProjectedY)
                            End If
                            If ProjectedY > _PanExtent.yMax Then 'North
                                shiftExtentByDirection(ProjectedX, ProjectedY, 0)
                            ElseIf ProjectedX > _PanExtent.xMax Then 'East
                                shiftExtentByDirection(ProjectedX, ProjectedY, 1)
                            ElseIf ProjectedY < _PanExtent.yMin Then 'South
                                shiftExtentByDirection(ProjectedX, ProjectedY, 2)
                            ElseIf ProjectedX < _PanExtent.xMin Then 'West
                                shiftExtentByDirection(ProjectedX, ProjectedY, 3)
                            End If
                        End If
                    End If

                    'Only draw if drawing turned on and then draw arrow or point accordingly
                    If g_Settings.DrawLocation Then
                        If g_Settings.DrawArrow Then
                            If g_GPS.Bearing <> -999 Then
                                drawLocArrow(ProjectedX, ProjectedY, g_GPS.Bearing)
                            End If
                        Else
                            drawLocPoint(ProjectedX, ProjectedY)
                        End If
                    End If
                    _forcePanDraw = False
                End If

                'If saving tracks, add the point
                If g_SaveTracks And ((Not g_TrackSF Is Nothing) Or (Not g_TrackLineSF Is Nothing)) Then
                    saveTrack(ProjectedX, ProjectedY)
                End If
            End If
        Catch ex As Exception
            'Almost assuredly the thing throwing a serial event on closing. do nothing
        End Try
    End Sub


    ''' <summary>
    ''' Used to calculate geoidal distance between two decimal degree points
    ''' </summary>
    ''' <param name="Lat1"></param>
    ''' <param name="Long1"></param>
    ''' <param name="Lat2"></param>
    ''' <param name="Long2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Distance(ByVal Lat1 As Double, ByVal Long1 As Double, ByVal Lat2 As Double, ByVal Long2 As Double) As Double
        'calculates the distance between two decimal degree points
        Const DegToRadians As Double = (2 * Math.PI) / 360
        Const RadiusEarth As Double = 3963 ' in miles

        Dim result As Double = RadiusEarth * Math.Acos(Math.Sin(Lat1 * DegToRadians) _
        * Math.Sin(Lat2 * DegToRadians) _
        + Math.Cos(Lat1 * DegToRadians) _
        * Math.Cos(Lat2 * DegToRadians) _
        * Math.Cos(Long2 * DegToRadians - Long1 * DegToRadians))
        result = result * 1609.344
        Return result
    End Function

    ''' <summary>
    ''' Center the extents on a given location
    ''' </summary>
    ''' <param name="ProjectedX"></param>
    ''' <param name="ProjectedY"></param>
    ''' <remarks></remarks>
    Private Sub centerOnPoint(ByVal ProjectedX As Double, ByVal ProjectedY As Double)
        Dim oldExt As MapWinGIS.Extents = g_MapWin.View.Extents
        Dim newExt As New MapWinGIS.Extents()
        Dim mpX As Double = (oldExt.xMax - oldExt.xMin) / 2
        Dim mpY As Double = (oldExt.yMax - oldExt.yMin) / 2

        newExt.SetBounds(ProjectedX - mpX, ProjectedY - mpY, 0, ProjectedX + mpX, ProjectedY + mpY, 0)
        g_MapWin.View.Extents = newExt
        Dim panMpX As Double = mpX * 0.9
        Dim panMpY As Double = mpY * 0.9
        _PanExtent.SetBounds(ProjectedX - panMpX, ProjectedY - panMpY, 0, ProjectedX + panMpX, ProjectedY + panMpY, 0)
    End Sub

    ''' <summary>
    ''' Shift the extents so that the given point is to the edge from the direction shifted
    ''' </summary>
    ''' <param name="ProjectedX"></param>
    ''' <param name="ProjectedY"></param>
    ''' <param name="Direction"></param>
    ''' <remarks></remarks>
    Private Sub shiftExtentByDirection(ByVal ProjectedX As Double, ByVal ProjectedY As Double, ByVal Direction As Integer)
        Dim oldExt As MapWinGIS.Extents = g_MapWin.View.Extents
        If oldExt.xMin = 0 And oldExt.xMax = 0 And oldExt.yMax = 0 And oldExt.yMin = 0 Then
            centerOnPoint(ProjectedX, ProjectedY)
            Exit Sub
        End If
        Dim newExt As New MapWinGIS.Extents()
        Dim divNum As Integer = 8
        Dim divX, divY As Double
        divX = (oldExt.xMax - oldExt.xMin) / divNum
        divY = (oldExt.yMax - oldExt.yMin) / divNum
        Select Case Direction
            Case 0 'North
                newExt.SetBounds(oldExt.xMin, oldExt.yMin + (divNum - 2) * divY, 0, oldExt.xMax, oldExt.yMax + (divNum - 2) * divY, 0)
                g_MapWin.View.Extents = newExt
            Case 1 'East
                newExt.SetBounds(oldExt.xMin + (divNum - 2) * divX, oldExt.yMin, 0, oldExt.xMax + (divNum - 2) * divX, oldExt.yMax, 0)
                g_MapWin.View.Extents = newExt
            Case 2 'South
                newExt.SetBounds(oldExt.xMin, oldExt.yMin - (divNum - 2) * divY, 0, oldExt.xMax, oldExt.yMax - (divNum - 2) * divY, 0)
                g_MapWin.View.Extents = newExt
            Case 3 'West
                newExt.SetBounds(oldExt.xMin - (divNum - 2) * divX, oldExt.yMin, 0, oldExt.xMax - (divNum - 2) * divX, oldExt.yMax, 0)
                g_MapWin.View.Extents = newExt
        End Select

        _PanExtent.SetBounds(newExt.xMin + divX, newExt.yMin + divY, 0, newExt.xMax - divX, newExt.yMax - divY, 0)
    End Sub

    ''' <summary>
    ''' Draws a point on the map at the location of the GPS
    ''' </summary>
    ''' <param name="ProjectedX"></param>
    ''' <param name="ProjectedY"></param>
    ''' <remarks></remarks>
    Private Sub drawLocPoint(ByVal ProjectedX As Double, ByVal ProjectedY As Double)
        If g_ArrowDrawingHandle <> -1 Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
            g_ArrowDrawingHandle = g_MapWin.View.Draw.NewDrawing(MapWinGIS.tkDrawReferenceList.dlSpatiallyReferencedList)
        Else
            g_ArrowDrawingHandle = g_MapWin.View.Draw.NewDrawing(MapWinGIS.tkDrawReferenceList.dlSpatiallyReferencedList)
        End If

        Dim arrowLength As Double = (g_MapWin.View.Extents.xMax - g_MapWin.View.Extents.xMin) / 60
        Dim lineWidth As Integer = g_Settings.CrosshairThickness
        'crosshair
        g_MapWin.View.Draw.DrawLine(ProjectedX, ProjectedY + arrowLength / 2, ProjectedX, ProjectedY - arrowLength / 2, lineWidth, g_Settings.CrosshairColor)
        g_MapWin.View.Draw.DrawLine(ProjectedX - arrowLength / 2, ProjectedY, ProjectedX + arrowLength / 2, ProjectedY, lineWidth, g_Settings.CrosshairColor)
        g_MapWin.View.Draw.DrawCircle(ProjectedX, ProjectedY, 8, g_Settings.ArrowColor, False)
    End Sub

    ''' <summary>
    ''' Draws an arrow on the map at the location of the GPS and in the bearing direction
    ''' </summary>
    ''' <param name="ProjectedX"></param>
    ''' <param name="ProjectedY"></param>
    ''' <param name="Bearing"></param>
    ''' <remarks></remarks>
    Private Sub drawLocArrow(ByVal ProjectedX As Double, ByVal ProjectedY As Double, ByVal Bearing As Double)
        Dim lineWidth As Integer = g_Settings.CrosshairThickness
        If g_ArrowDrawingHandle <> -1 Then
            g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
            g_ArrowDrawingHandle = g_MapWin.View.Draw.NewDrawing(MapWinGIS.tkDrawReferenceList.dlSpatiallyReferencedList)
        Else
            g_ArrowDrawingHandle = g_MapWin.View.Draw.NewDrawing(MapWinGIS.tkDrawReferenceList.dlSpatiallyReferencedList)
        End If
        
        Dim arrowLength As Double = (g_MapWin.View.Extents.xMax - g_MapWin.View.Extents.xMin) / 60
        Dim arrowWidth As Double = arrowLength / 3

        Dim xlist(0 To 2) As Double
        Dim ylist(0 To 2) As Double
        RotatePoint(ProjectedX, ProjectedY + arrowLength / 2 + arrowLength / 1.5, ProjectedX, ProjectedY, Bearing, xlist(0), ylist(0))
        RotatePoint(ProjectedX - arrowLength / 3, ProjectedY + arrowLength / 2, ProjectedX, ProjectedY, Bearing, xlist(1), ylist(1))
        RotatePoint(ProjectedX + arrowLength / 3, ProjectedY + arrowLength / 2, ProjectedX, ProjectedY, Bearing, xlist(2), ylist(2))
        g_MapWin.View.Draw.DrawPolygon(xlist, ylist, g_Settings.ArrowColor, True)

        g_MapWin.View.Draw.DrawCircle(ProjectedX, ProjectedY, 8, g_Settings.ArrowColor, False)

        'crosshair
        g_MapWin.View.Draw.DrawLine(ProjectedX, ProjectedY + arrowLength / 2, ProjectedX, ProjectedY - arrowLength / 2, lineWidth, g_Settings.CrosshairColor)
        g_MapWin.View.Draw.DrawLine(ProjectedX - arrowLength / 2, ProjectedY, ProjectedX + arrowLength / 2, ProjectedY, lineWidth, g_Settings.CrosshairColor)
    End Sub

    ''' <summary>
    ''' Calculates the rotation of a point around a center by a bearing in degrees clockwise from north
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <param name="x_center"></param>
    ''' <param name="y_center"></param>
    ''' <param name="bearing"></param>
    ''' <param name="x_out"></param>
    ''' <param name="y_out"></param>
    ''' <remarks></remarks>
    Private Sub RotatePoint(ByVal x As Double, ByVal y As Double, ByVal x_center As Double, ByVal y_center As Double, ByVal bearing As Double, ByRef x_out As Double, ByRef y_out As Double)
        x_out = x_center + ((x - x_center) * Math.Cos(-1 * bearing * Math.PI / 180) - (y - y_center) * Math.Sin(-1 * bearing * Math.PI / 180))
        y_out = y_center + ((y - y_center) * Math.Cos(-1 * bearing * Math.PI / 180) + (x - x_center) * Math.Sin(-1 * bearing * Math.PI / 180))
    End Sub

    ''' <summary>
    ''' Saves a track point with various data to a track shapefile
    ''' </summary>
    ''' <param name="ProjectedX"></param>
    ''' <param name="ProjectedY"></param>
    ''' <remarks></remarks>
    Private Sub saveTrack(ByVal ProjectedX As Double, ByVal ProjectedY As Double)
        If ProjectedX <> -999 And ProjectedY <> -999 And g_GPS.Altitude <> -999 And g_GPS.PDOP <> -999 And g_GPS.VDOP <> -999 And g_GPS.HDOP <> -999 And g_GPS.UTCTime <> "" And g_GPS.UTCDate <> "" Then
            Dim pt As New MapWinGIS.Point
            pt.x = ProjectedX
            pt.y = ProjectedY

            If Not g_TrackLineSF Is Nothing Then
                If _trackLineLastPt Is Nothing Then
                    _trackLineLastPt = pt
                Else
                    If _trackLineLastPt Is Nothing Then
                        Dim currshp As MapWinGIS.Shape = g_TrackLineSF.Shape(g_TrackLineSF.NumShapes - 1)
                        _trackLineLastPt = currshp.Point(currshp.numPoints - 1)
                    End If
                    Dim lnshp As New MapWinGIS.Shape
                    lnshp.Create(MapWinGIS.ShpfileType.SHP_POLYLINE)
                    lnshp.InsertPoint(_trackLineLastPt, lnshp.numPoints)
                    lnshp.InsertPoint(pt, lnshp.numPoints)

                    g_TrackLineSF.EditInsertShape(lnshp, g_TrackLineSF.NumShapes)
                    _trackLineLastPt = pt
                End If
                If _trackCount >= _trackCountToSave Then
                    _trackCount = 0
                    g_TrackLineSF.StopEditingShapes()
                    g_TrackLineSF.Close()
                    If Not _TrackLineLayer Is Nothing Then
                        reloadLayer(_TrackLineLayer)
                    End If
                    g_TrackLineSF.Open(g_Settings.TrackLineLogPath)
                    g_TrackLineSF.StartEditingShapes()
                Else
                    _trackCount = _trackCount + 1
                End If
            End If

            If Not g_TrackSF Is Nothing Then
                Dim ptshp As New MapWinGIS.Shape
                ptshp.Create(MapWinGIS.ShpfileType.SHP_POINT)
                ptshp.InsertPoint(pt, 0)
                g_TrackSF.EditInsertShape(ptshp, g_TrackSF.NumShapes)

                Dim shpIdx As Integer = g_TrackSF.NumShapes - 1
                Dim currfield As Integer = 1
                If g_Settings.LogDate Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.UTCDate)
                    currfield = currfield + 1
                End If

                If g_Settings.LogTime Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.UTCTime)
                    currfield = currfield + 1
                End If

                If g_Settings.LogLocX Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, ProjectedX)
                    currfield = currfield + 1
                End If

                If g_Settings.LogLocY Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, ProjectedY)
                    currfield = currfield + 1
                End If

                If g_Settings.LogElev Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.Altitude)
                    currfield = currfield + 1
                End If

                If g_Settings.LogPDOP Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.PDOP)
                    currfield = currfield + 1
                End If

                If g_Settings.LogHDOP Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.HDOP)
                    currfield = currfield + 1
                End If

                If g_Settings.LogVDOP Then
                    g_TrackSF.EditCellValue(currfield, shpIdx, g_GPS.VDOP)
                    currfield = currfield + 1
                End If
                If _trackCount >= _trackCountToSave Then
                    _trackCount = 0
                    g_TrackSF.StopEditingShapes()
                    g_TrackSF.Close()
                    If Not _TrackPointLayer Is Nothing Then
                        reloadLayer(_TrackPointLayer)
                    End If
                    g_TrackSF.Open(g_Settings.TrackPointsLogPath)
                    g_TrackSF.StartEditingShapes()

                Else
                    _trackCount = _trackCount + 1
                End If
            End If
        End If
    End Sub

    Private Sub reloadLayer(ByVal lyr As MapWindow.Interfaces.Layer)
        Dim lyrname As String = lyr.Name
        Dim lyrpath As String = lyr.FileName
        Dim lyrgroup As Integer = lyr.GroupHandle
        Dim lyrgrouppos As Integer = lyr.GroupPosition
        Dim tmpPropPath As String = lyr.FileName + ".tmp"
        lyr.SaveShapeLayerProps(tmpPropPath)
        g_MapWin.Layers.Remove(lyr.Handle)

        Dim newlyr As MapWindow.Interfaces.Layer = g_MapWin.Layers.Add(lyrpath, lyrname)
        newlyr.LoadShapeLayerProps(tmpPropPath)
        File.Delete(tmpPropPath)
        g_MapWin.Layers.MoveLayer(newlyr.Handle, lyrgrouppos, lyrgroup)
    End Sub
#End Region

#Region "Interoperation Code"
    ''' <summary>
    ''' A public function allowing another plugin or code to obtain the GPS controller object created in this plugin so that they can access the same COM-driven events raised by it.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGPSController() As clsGPSController
        Return g_GPS
    End Function

#End Region

End Class

