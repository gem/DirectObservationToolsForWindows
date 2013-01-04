'********************************************************************************************************
'File Name: clsView.vb
'Description: Public class used to access the main map view through the plugin interface.
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
'1/31/2005 - minor modifications. (dpa)
'28-nov-2010 - Some fixes for the new iplementation of shapefile selection (ocx-based)
'01-june-2011 - Old selection routine is removed. Instance of SelectInfo class is created by user request only.
'********************************************************************************************************

Imports MapWinGIS
Imports System
Imports MapWindow.Interfaces

Public Class View
    Implements Interfaces.View


    ' selected shapes (cache needed for speeding up old code)
    ' should be cleared after the change of layer
    Private m_selection As MapWindow.Interfaces.SelectInfo = Nothing

    Public Sub New()
        MyBase.New()
    End Sub

    '-------------------Private members for public properties-------------------
    Private m_SelectColor As Integer = RGB(255, 255, 0) ' System.Drawing.Color.Yellow.ToArgb() And &HFFFFFF
    Private m_SelectionPersistence As Boolean
    Private m_SelectionTolerance As Double
    Private m_Selectmethod As MapWinGIS.SelectMode = SelectMode.INTERSECTION
    Private m_SelectionOperation As SelectionOperation = SelectionOperation.SelectInvert

    '-------------------Subs-------------------
    Public Sub ForceFullRedraw() Implements Interfaces.View.ForceFullRedraw

        ' redrawing map
        Dim locked As Boolean = Me.IsMapLocked
        Do While Me.IsMapLocked
            Me.UnlockMap()
        Loop
        Me.Redraw()
        Application.DoEvents()
        If locked Then Me.LockMap()

        ' redrawing legend
        locked = Me.IsMapLocked
        Do While Me.LegendControl.Locked
            Me.LegendControl.Unlock()
        Loop
        Me.LegendControl.Refresh()
        Application.DoEvents()
        If locked Then Me.LegendControl.Lock()
    End Sub

    Public ReadOnly Property LegendControl() As LegendControl.Legend Implements Interfaces.View.LegendControl
        Get
            Return frmMain.Legend
        End Get
    End Property

    Public Property ExtentHistory() As System.Collections.ArrayList Implements Interfaces.View.ExtentHistory
        Get
            Return frmMain.m_Extents
        End Get
        Set(ByVal value As System.Collections.ArrayList)
            frmMain.m_Extents = value
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Function Identify(ByVal ProjX As Double, ByVal ProjY As Double, ByVal Tolerance As Double) As Interfaces.IdentifiedLayers Implements Interfaces.View.Identify
        Dim tShp As MapWindow.IdentifiedShapes
        Dim tLyr As New MapWindow.IdentifiedLayers
        Dim i As Integer, j As Integer
        Dim tSF As MapWinGIS.Shapefile, o As Object, box As MapWinGIS.Extents, res As Object = Nothing

        For i = 0 To frmMain.MapMain.NumLayers - 1
            If frmMain.MapMain.get_LayerVisible(frmMain.MapMain.get_LayerHandle(i)) = True Then
                o = frmMain.MapMain.get_GetObject(frmMain.MapMain.get_LayerHandle(i))
                If TypeOf o Is MapWinGIS.Shapefile Then
                    tSF = CType(o, MapWinGIS.Shapefile)
                    o = Nothing

                    box = New MapWinGIS.Extents
                    box.SetBounds(ProjX, ProjY, 0, ProjX, ProjY, 0)

                    If tSF.SelectShapes(box, Tolerance, MapWinGIS.SelectMode.INTERSECTION, res) Then
                        tShp = New MapWindow.IdentifiedShapes

                        Dim arr As System.Array
                        arr = CType(res, System.Array)
                        For j = 0 To UBound(arr)
                            tShp.Add(CType(arr.GetValue(j), Integer))
                        Next j

                        tLyr.Add(tShp, frmMain.MapMain.get_LayerHandle(i))
                    End If

                End If
            End If
        Next i
        Identify = tLyr
    End Function

    Public Property LegendVisible() As Boolean Implements Interfaces.View.LegendVisible
        Get
            Return frmMain.m_Menu("mnuLegendVisible").Checked
        End Get
        Set(ByVal value As Boolean)
            If value Then
                frmMain.legendPanel.Show()
                frmMain.legendPanel.VisibleState = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
            Else
                frmMain.legendPanel.Hide()
            End If
            frmMain.legendPanel.Visible = value

            If frmMain.m_Menu("mnuLegendVisible").Checked = Not value Then
                frmMain.HandleClickedMenu("mnuLegendVisible")
            End If
        End Set
    End Property

    Public Property LabelsUseProjectLevel() As Boolean Implements Interfaces.View.LabelsUseProjectLevel
        Get
            Return AppInfo.LabelsUseProjectLevel
        End Get
        Set(ByVal value As Boolean)
            AppInfo.LabelsUseProjectLevel = value
            frmMain.SetModified(True)
        End Set
    End Property

    Public Sub LabelsEdit(ByVal LayerHandle As Integer) Implements Interfaces.View.LabelsEdit
        If frmMain.Layers.IsValidHandle(LayerHandle) AndAlso (frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.LineShapefile Or frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.PointShapefile Or frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.PolygonShapefile) Then
            frmMain.DoLabelsEdit(LayerHandle)
        End If
    End Sub

    Public Sub LabelsRelabel(ByVal LayerHandle As Integer) Implements Interfaces.View.LabelsRelabel
        If frmMain.Layers.IsValidHandle(LayerHandle) AndAlso (frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.LineShapefile Or frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.PointShapefile Or frmMain.Layers.Item(LayerHandle).LayerType = Interfaces.eLayerType.PolygonShapefile) Then
            frmMain.DoLabelsRelabel(LayerHandle)
        End If
    End Sub

    Public Property PreviewVisible() As Boolean Implements Interfaces.View.PreviewVisible
        Get
            Return frmMain.m_Menu("mnuPreviewVisible").Checked
        End Get
        Set(ByVal value As Boolean)
            If frmMain.m_Menu("mnuPreviewVisible").Checked = Not value Then
                frmMain.HandleClickedMenu("mnuPreviewVisible")
            End If
        End Set
    End Property

    Public Sub LockLegend() Implements Interfaces.View.LockLegend
        frmMain.Legend.Lock()
    End Sub

    Public Sub UnlockLegend() Implements Interfaces.View.UnlockLegend
        frmMain.Legend.Unlock()
    End Sub

    Public Sub LockMap() Implements Interfaces.View.LockMap
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
    End Sub

    Public Sub UnlockMap() Implements Interfaces.View.UnlockMap
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
    End Sub

    Public Sub PixelToProj(ByVal PixelX As Double, ByVal PixelY As Double, ByRef ProjX As Double, ByRef ProjY As Double) Implements Interfaces.View.PixelToProj
        frmMain.MapMain.PixelToProj(PixelX, PixelY, ProjX, ProjY)
    End Sub

    Public Sub ProjToPixel(ByVal ProjX As Double, ByVal ProjY As Double, ByRef PixelX As Double, ByRef PixelY As Double) Implements Interfaces.View.ProjToPixel
        frmMain.MapMain.ProjToPixel(ProjX, ProjY, PixelX, PixelY)
    End Sub

    Public Sub Redraw() Implements Interfaces.View.Redraw
        frmMain.MapMain.Redraw()
    End Sub

    Public Sub ShowToolTip(ByVal Text As String, ByVal Milliseconds As Integer) Implements Interfaces.View.ShowToolTip
        frmMain.MapMain.ShowToolTip(Text, Milliseconds)
    End Sub

    Public Sub ZoomToMaxExtents() Implements Interfaces.View.ZoomToMaxExtents
        frmMain.MapMain.ZoomToMaxExtents()
        frmMain.m_PreviewMap.UpdateLocatorBox()
    End Sub

    Public Sub ZoomIn(ByVal Percent As Double) Implements Interfaces.View.ZoomIn
        frmMain.MapMain.ZoomIn(Percent)
        frmMain.m_PreviewMap.UpdateLocatorBox()
    End Sub

    Public Sub ZoomOut(ByVal Percent As Double) Implements Interfaces.View.ZoomOut
        frmMain.MapMain.ZoomOut(Percent)
        frmMain.m_PreviewMap.UpdateLocatorBox()
    End Sub

    Public Sub ZoomToPrev() Implements Interfaces.View.ZoomToPrev
        frmMain.MapMain.ZoomToPrev()
        frmMain.m_PreviewMap.UpdateLocatorBox()
    End Sub

    Public Property HandleFileDrop() As Boolean Implements Interfaces.View.HandleFileDrop
        Get
            Return frmMain.m_HandleFileDrop
        End Get
        Set(ByVal value As Boolean)
            frmMain.m_HandleFileDrop = value
        End Set
    End Property

    '-------------------Functions-------------------
    <CLSCompliant(False)> _
    Public Function Snapshot(ByVal Bounds As MapWinGIS.Extents) As MapWinGIS.Image Implements Interfaces.View.Snapshot
        Snapshot = CType(frmMain.MapMain.SnapShot(Bounds), MapWinGIS.Image)
    End Function

    '-------------------Properties-------------------
    Public Property BackColor() As System.Drawing.Color Implements Interfaces.View.BackColor
        Get
            Dim map As MapWinGIS.Map = CType(CType(frmMain.MapMain, AxHost).GetOcx(), MapWinGIS.Map)
            Return MapWinUtility.Colors.IntegerToColor(map.BackColor)
        End Get
        Set(ByVal Value As System.Drawing.Color)
            'note: calling frmMain.MapMain.BackColor doesn't correctly set the back color.
            'by getting the ocx object directly, we can set the value directly and it works correctly  
            Dim map As MapWinGIS.Map = CType(CType(frmMain.MapMain, AxHost).GetOcx(), MapWinGIS.Map)
            map.BackColor = MapWinUtility.Colors.ColorToUInteger(Value)
            map = Nothing
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property CursorMode() As MapWinGIS.tkCursorMode Implements Interfaces.View.CursorMode
        Get
            CursorMode = frmMain.MapMain.CursorMode
        End Get
        Set(ByVal Value As MapWinGIS.tkCursorMode)
            frmMain.MapMain.CursorMode = Value

            frmMain.UpdateButtons()
        End Set
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property Draw() As Interfaces.Draw Implements Interfaces.View.Draw
        Get
            Dim df As New MapWindow.Draw
            Draw = df
        End Get
    End Property

    Public Property ExtentPad() As Double Implements Interfaces.View.ExtentPad
        Get
            ExtentPad = frmMain.MapMain.ExtentPad
        End Get
        Set(ByVal Value As Double)
            frmMain.MapMain.ExtentPad = Value
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property Extents() As MapWinGIS.Extents Implements Interfaces.View.Extents
        Get
            Extents = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
        End Get
        Set(ByVal Value As MapWinGIS.Extents)
            frmMain.MapMain.Extents = Value
            frmMain.m_PreviewMap.UpdateLocatorBox()
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property MapCursor() As MapWinGIS.tkCursor Implements Interfaces.View.MapCursor
        Get
            MapCursor = frmMain.MapMain.MapCursor
        End Get
        Set(ByVal Value As MapWinGIS.tkCursor)
            frmMain.MapMain.MapCursor = Value
        End Set
    End Property

    Public Property MapState() As String Implements Interfaces.View.MapState
        Get
            MapState = frmMain.MapMain.MapState
        End Get
        Set(ByVal Value As String)
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
            Try
                frmMain.Legend.Lock()
                frmMain.Legend.Layers.Clear()
                frmMain.MapMain.RemoveAllLayers()
                frmMain.MapPreview.ClearDrawings()
                frmMain.MapPreview.RemoveAllLayers()
                frmMain.MapMain.MapState = Value
                frmMain.Legend.Unlock()
            Finally
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End Try
        End Set
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property SelectedShapes() As Interfaces.SelectInfo Implements Interfaces.View.SelectedShapes
        Get
            ' let's try to return cached selection
            If Not m_selection Is Nothing AndAlso m_selection.LayerHandle = frmMain.Layers.CurrentLayer Then
                Return m_selection
            End If

            If frmMain.Layers.CurrentLayer > -1 Then
                Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(frmMain.Layers.CurrentLayer)
                If Not layer Is Nothing Then
                    m_selection = frmMain.Layers(frmMain.Layers.CurrentLayer).SelectedShapes
                    Return m_selection
                End If
            End If

            ' fictitious layer
            Dim info As New SelectInfo(-1)
            Return info
        End Get
    End Property

    Public Property SelectionPersistence() As Boolean Implements Interfaces.View.SelectionPersistence
        Get
            SelectionPersistence = m_SelectionPersistence
        End Get
        Set(ByVal Value As Boolean)
            m_SelectionPersistence = Value
        End Set
    End Property

    Public Property SelectionTolerance() As Double Implements Interfaces.View.SelectionTolerance
        Get
            SelectionTolerance = m_SelectionTolerance
        End Get
        Set(ByVal Value As Double)
            m_SelectionTolerance = Value
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property SelectMethod() As MapWinGIS.SelectMode Implements Interfaces.View.SelectMethod
        Get
            SelectMethod = m_Selectmethod
        End Get
        Set(ByVal Value As MapWinGIS.SelectMode)
            m_Selectmethod = Value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectColor() As System.Drawing.Color Implements Interfaces.View.SelectColor
        Get
            SelectColor = MapWinUtility.Colors.IntegerToColor(m_SelectColor)
        End Get
        Set(ByVal Value As System.Drawing.Color)
            m_SelectColor = MapWinUtility.Colors.ColorToInteger(Value)

            ' To make it consistent with older behavior this color will be set for all layers
            ' though these colors can easily be different
            For i As Integer = 0 To frmMain.MapMain.NumLayers - 1
                Dim handle As Integer = frmMain.MapMain.get_LayerHandle(i)
                Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_Shapefile(handle)
                If Not sf Is Nothing Then
                    sf.SelectionColor = m_SelectColor
                End If
            Next i
        End Set
    End Property

    Public Property Tag() As String Implements Interfaces.View.Tag
        Get
            Tag = frmMain.MapMain.Key
        End Get
        Set(ByVal Value As String)
            frmMain.MapMain.Key = Value
        End Set
    End Property

    Public Property UserCursorHandle() As Integer Implements Interfaces.View.UserCursorHandle
        Get
            UserCursorHandle = frmMain.MapMain.UDCursorHandle
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.UDCursorHandle = Value
        End Set
    End Property

    Public Property ZoomPercent() As Double Implements Interfaces.View.ZoomPercent
        Get
            ZoomPercent = frmMain.MapMain.ZoomPercent
        End Get
        Set(ByVal Value As Double)
            frmMain.MapMain.ZoomPercent = Value
        End Set
    End Property

    '--------------------------------------MapWin 2.0 Selection Routines--------------------------------------
    '30 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 2
    '---------------------------------------------------------------------------------------------------------

    Private Sub AddToSelectList(ByVal MapLayerHandle As Integer, ByVal lSelectedShapes() As Integer)

        Dim curShape As MapWinGIS.Shape, sf As MapWinGIS.Shapefile
        Dim j As Integer, k As Integer, foundIndex As Integer
        Dim newSel As MapWindow.SelectedShape

        sf = CType(frmMain.MapMain.get_GetObject(MapLayerHandle), MapWinGIS.Shapefile)
        If sf Is Nothing Then Exit Sub

        On Error Resume Next
        If UBound(lSelectedShapes) >= 0 Then
            If Err.Number = 0 Then
                On Error GoTo 0
                If Not m_SelectionPersistence Then sf.SelectNone()
                For j = 0 To UBound(lSelectedShapes)
                    sf.ShapeSelected(lSelectedShapes(j)) = True
                Next j
            End If
            m_selection = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Select shapes in the given point on the map. The operation selection depends upon SelectionOperation
    ''' </summary>
    ''' <param name="ScreenX">X in screen coordinates</param>
    ''' <param name="ScreenY">Y in screen coordinates</param>
    ''' <param name="ctrlDown">The state of Ctrl key</param>
    ''' <returns>The resulting state of map selection as array of shapes</returns>
    ''' <remarks></remarks>
    Friend Function SelectShapesByPoint(ByVal ScreenX As Integer, ByVal ScreenY As Integer, Optional ByVal ctrlDown As Boolean = False) As MapWindow.SelectInfo
        SelectShapesByPoint = Nothing

        If frmMain.Legend.SelectedLayer = -1 Then Return Nothing

        Dim x As Double, y As Double
        PixelToProj(ScreenX, ScreenY, x, y)

        Dim bounds As New MapWinGIS.Extents
        bounds.SetBounds(x, y, 0, x, y, 0)

        Dim type As Interfaces.eLayerType = frmMain.Layers(frmMain.Layers.CurrentLayer).LayerType
        If (type = Interfaces.eLayerType.Grid Or _
            type = Interfaces.eLayerType.Image Or _
            type = Interfaces.eLayerType.Invalid) Then
            Return Nothing
        End If

        Dim obj As Object = frmMain.MapMain.get_GetObject(frmMain.Layers.CurrentLayer)
        Dim sf As MapWinGIS.Shapefile = CType(obj, MapWinGIS.Shapefile)

        ' calculating tolerance
        Dim tolerance As Double = m_SelectionTolerance
        If (tolerance = 0) Then
            Select Case sf.ShapefileType
                Case ShpfileType.SHP_POLYGON, ShpfileType.SHP_POLYGONM, ShpfileType.SHP_POLYGONZ
                    tolerance = 0.0#
                Case ShpfileType.SHP_POINT, ShpfileType.SHP_POINTM, ShpfileType.SHP_POINTZ

                    Dim x1 As Double, y1 As Double
                    Dim size As Integer = sf.DefaultDrawingOptions.PointSize / 2

                    PixelToProj(ScreenX + size, ScreenY + size, x1, y1)
                    tolerance = System.Math.Sqrt((x - x1) ^ 2 + (y - y1) ^ 2)

                Case Else
                    Dim x1 As Double, y1 As Double
                    PixelToProj(ScreenX + 5, ScreenY + 5, x1, y1)

                    tolerance = Math.Sqrt((x - x1) ^ 2 + (y - y1) ^ 2)
            End Select
        End If

        If ctrlDown Then
            m_SelectionOperation = SelectionOperation.SelectAdd
        Else
            m_SelectionOperation = SelectionOperation.SelectNew
        End If

        ' doing selection
        Return PerformSelection(sf, bounds, tolerance)
    End Function

    ''' <summary>
    ''' Selects shapes in the specified rectangle
    ''' </summary>
    ''' <param name="ScreenLeft"></param>
    ''' <param name="ScreenRight"></param>
    ''' <param name="ScreenTop"></param>
    ''' <param name="ScreenBottom"></param>
    ''' <param name="ctrlDown"></param>
    Friend Function SelectShapesByRectangle(ByVal ScreenLeft As Integer, ByVal ScreenRight As Integer, ByVal ScreenTop As Integer, ByVal ScreenBottom As Integer, Optional ByVal ctrlDown As Boolean = False) As MapWindow.SelectInfo
        SelectShapesByRectangle = Nothing

        If frmMain.Legend.SelectedLayer = -1 Then Return Nothing

        Dim type As Interfaces.eLayerType = frmMain.Layers(frmMain.Layers.CurrentLayer).LayerType
        If (type = Interfaces.eLayerType.Grid Or _
            type = Interfaces.eLayerType.Image Or _
            type = Interfaces.eLayerType.Invalid) Then
            Return Nothing
        End If

        Dim sf As MapWinGIS.Shapefile = CType(frmMain.MapMain.get_GetObject(frmMain.Layers.CurrentLayer), MapWinGIS.Shapefile)

        Dim geoL As Double, geoR As Double, geoT As Double, geoB As Double
        frmMain.MapMain.PixelToProj(ScreenLeft, ScreenTop, geoL, geoT)
        frmMain.MapMain.PixelToProj(ScreenRight, ScreenBottom, geoR, geoB)

        Dim bounds As New MapWinGIS.Extents
        bounds.SetBounds(geoL, geoB, 0, geoR, geoT, 0)

        If ctrlDown Then
            m_SelectionOperation = SelectionOperation.SelectAdd
        Else
            m_SelectionOperation = SelectionOperation.SelectNew
        End If

        Return PerformSelection(sf, bounds, 0.0#)
    End Function

    ''' <summary>
    ''' Perform operation on selection defined by SelectionOperation, adding/exluding shapes in the specified bounds
    ''' </summary>
    ''' <param name="bounds">Bounds to select shapes in</param>
    ''' <param name="tolerance">The bounds will be enlarge on this value</param>
    Friend Function PerformSelection(ByRef sf As MapWinGIS.Shapefile, ByRef bounds As MapWinGIS.Extents, ByVal tolerance As Double) As SelectInfo

        Dim m_SelectedShapes As New SelectInfo(Me.LegendControl.SelectedLayer)

        If m_SelectedShapes Is Nothing Then m_SelectedShapes = New MapWindow.SelectInfo(Me.LegendControl.SelectedLayer)
        m_SelectedShapes.ClearSelectedShapesTemp()

        ' the selection itself
        Dim arr As Object = Nothing
        Dim results As System.Array = Nothing
        If sf.SelectShapes(bounds, tolerance, m_Selectmethod, arr) Then results = CType(arr, System.Array)

        If m_SelectionOperation = SelectionOperation.SelectNew Then sf.SelectNone()

        ' modifing selection according to the returned indices
        Dim i As Integer

        If Not results Is Nothing AndAlso results.Length > 0 Then
            Select Case m_SelectionOperation
                Case SelectionOperation.SelectNew
                    For i = 0 To results.Length - 1
                        sf.ShapeSelected(results(i)) = True
                    Next
                Case SelectionOperation.SelectAdd
                    For i = 0 To results.Length - 1
                        sf.ShapeSelected(results(i)) = True
                    Next
                Case SelectionOperation.SelectExclude
                    For i = 0 To results.Length - 1
                        sf.ShapeSelected(results(i)) = False
                    Next
                Case SelectionOperation.SelectInvert
                    For i = 0 To results.Length - 1
                        sf.ShapeSelected(results(i)) = Not sf.ShapeSelected(results(i))
                    Next
            End Select
        Else
            If m_SelectionOperation = SelectionOperation.SelectNew Then
                sf.SelectNone()
            End If
        End If

        ' returning selection info
        For i = 0 To sf.NumShapes - 1
            If sf.ShapeSelected(i) Then
                Dim shape As New SelectedShape
                shape.Add(i)
                m_SelectedShapes.AddSelectedShape(shape)
            End If
        Next i

        m_selection = Nothing

        Me.Redraw()
        Return m_SelectedShapes
    End Function

    ''' <summary>
    ''' Changes selection of the shapefile adding new shapes using the specified mode
    ''' </summary>
    ''' <param name="Indices">Indices of shapes which fell into selection</param>
    ''' <param name="Mode">Selection mode</param>
    ''' <remarks></remarks>
    Public Function UpdateSelection(ByVal LayerHandle As Integer, ByRef Indices() As Integer, ByVal Mode As MapWindow.Interfaces.SelectionOperation) _
                                                                                            As Interfaces.SelectInfo Implements Interfaces.View.UpdateSelection
        Dim i As Integer

        ' clearing the cache
        m_selection = Nothing

        Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(LayerHandle)
        If layer Is Nothing Then Return Nothing
        Dim sf As MapWinGIS.Shapefile = TryCast(layer.GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then Return Nothing

        If Mode = SelectionOperation.SelectNew Then
            sf.SelectNone()
        End If

        If Not Indices Is Nothing AndAlso Indices.Length > 0 Then
            Select Case Mode
                Case SelectionOperation.SelectNew
                    For i = 0 To Indices.Length - 1
                        sf.ShapeSelected(Indices(i)) = True
                    Next
                Case SelectionOperation.SelectAdd
                    For i = 0 To Indices.Length - 1
                        sf.ShapeSelected(Indices(i)) = True
                    Next
                Case SelectionOperation.SelectExclude
                    For i = 0 To Indices.Length - 1
                        sf.ShapeSelected(Indices(i)) = False
                    Next
                Case SelectionOperation.SelectInvert
                    For i = 0 To Indices.Length - 1
                        sf.ShapeSelected(Indices(i)) = Not sf.ShapeSelected(Indices(i))
                    Next
            End Select
        End If

        frmMain.UpdateButtons()

        Dim handled As Boolean = False
        frmMain.FireLayerSelectionChanged(LayerHandle, handled)

        Return layer.SelectedShapes
    End Function

    ''' <summary>
    ''' Clears selection. In the old version meant changing the colors of shapes only. In the new one Shapefile.ShapeSelected property is changed
    ''' </summary>
    Public Sub ClearSelectedShapes() Implements Interfaces.View.ClearSelectedShapes
        For i As Integer = 0 To frmMain.Layers.NumLayers - 1
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(frmMain.Layers.GetHandle(i))
            If Not layer Is Nothing Then layer.ClearSelection()
        Next
    End Sub

    <CLSCompliant(False)> _
    Public Function [Select](ByVal ScreenX As Integer, ByVal ScreenY As Integer, ByVal ClearOldSelection As Boolean) As MapWindow.Interfaces.SelectInfo Implements MapWindow.Interfaces.View.Select
        Return Me.SelectShapesByPoint(ScreenX, ScreenY, Not ClearOldSelection)
    End Function

    <CLSCompliant(False)> _
    Public Function [Select](ByVal ScreenBounds As System.Drawing.Rectangle, ByVal ClearOldSelection As Boolean) As MapWindow.Interfaces.SelectInfo Implements MapWindow.Interfaces.View.Select
        Return Me.SelectShapesByRectangle(ScreenBounds.Left, ScreenBounds.Right, ScreenBounds.Top, ScreenBounds.Bottom, Not ClearOldSelection)
    End Function
    ''' <summary>
    ''' Gets or sets the shape drawing method
    ''' </summary>
    ''' <remarks>Added by Paul Meems on May 12 2010</remarks>
    Public Property ShapeDrawingMethod() As MapWinGIS.tkShapeDrawingMethod Implements Interfaces.View.ShapeDrawingMethod
        Get
            Return frmMain.MapMain.ShapeDrawingMethod()
        End Get
        Set(ByVal value As MapWinGIS.tkShapeDrawingMethod)
            frmMain.MapMain.ShapeDrawingMethod = value
        End Set
    End Property
    ''' <summary>
    ''' Gets the height of the map control
    ''' </summary>
    ''' <remarks>Added by Paul Meems on May 26 2010</remarks>
    Public ReadOnly Property MapHeight() As Integer Implements Interfaces.View.MapHeight
        Get
            Return frmMain.MapMain.Height
        End Get
    End Property
    ''' <summary>
    ''' Gets the width of the map control
    ''' </summary>
    ''' <remarks>Added by Paul Meems on May 26 2010</remarks>
    Public ReadOnly Property MapWidth() As Integer Implements Interfaces.View.MapWidth
        Get
            Return frmMain.MapMain.Width
        End Get
    End Property
    ''' <summary>
    ''' Gets or sets the map scale of the current view
    ''' </summary>
    ''' <value>The scale to set</value>
    ''' <returns>The current scale in map units</returns>
    ''' <remarks>Added by Paul Meems on August 16 2010</remarks>
    Public Property Scale() As Double Implements Interfaces.View.Scale
        Get
            ' Set the correct map units so the CurrentScale method will work properly:
            frmMain.MapMain.MapUnits = GetMapUnits(frmMain.m_Project.MapUnits)
            Return frmMain.MapMain.CurrentScale
        End Get
        Set(ByVal value As Double)
            frmMain.MapMain.CurrentScale = value
        End Set
    End Property
    ''' <summary>
    ''' Converts the project map units which is a string to the tkUnitsOfMeasure enumeration
    ''' </summary>
    ''' <param name="projectMapUnits">The project map units</param>
    ''' <returns>the tkUnitsOfMeasure</returns>
    ''' <remarks>Added by Paul Meems on August 16 2010</remarks>
    Private Function GetMapUnits(ByVal projectMapUnits As String) As MapWinGIS.tkUnitsOfMeasure
        Select Case projectMapUnits.ToLower()
            Case "lat/long"
                Return MapWinGIS.tkUnitsOfMeasure.umDecimalDegrees
            Case "meters"
                Return MapWinGIS.tkUnitsOfMeasure.umMeters
            Case "centimeters"
                Return MapWinGIS.tkUnitsOfMeasure.umCentimeters
            Case "feet"
                Return MapWinGIS.tkUnitsOfMeasure.umFeets
            Case "inches"
                Return MapWinGIS.tkUnitsOfMeasure.umInches
            Case "kilometers"
                Return MapWinGIS.tkUnitsOfMeasure.umKilometers
            Case "miles"
                Return MapWinGIS.tkUnitsOfMeasure.umMiles
            Case "millimeters"
                Return MapWinGIS.tkUnitsOfMeasure.umMiliMeters
            Case "yards"
                Return MapWinGIS.tkUnitsOfMeasure.umYards
            Case "us-ft"
                Return MapWinGIS.tkUnitsOfMeasure.umFeets
            Case Else
                Return MapWinGIS.tkUnitsOfMeasure.umMeters
        End Select
    End Function
    ''' <summary>
    ''' Gets if the map is locked or not
    ''' </summary>
    ''' <remarks>Added by Paul Meems on September 19 2010</remarks>
    Public ReadOnly Property IsMapLocked() As Boolean Implements Interfaces.View.IsMapLocked
        Get
            If (frmMain.MapMain.IsLocked = MapWinGIS.tkLockMode.lmLock) Then
                Return True
            End If
            Return False
        End Get
    End Property
    ''' <summary>
    ''' Gets or sets the CanUseImageGrouping property
    ''' Used for speeding up images with a lot of NoData values
    ''' </summary>
    ''' <remarks>Added by Paul Meems on September 19 2010</remarks>
    Public Property CanUseImageGrouping() As Boolean Implements Interfaces.View.CanUseImageGrouping
        Get
            Return frmMain.MapMain.CanUseImageGrouping
        End Get
        Set(ByVal value As Boolean)
            frmMain.MapMain.CanUseImageGrouping = value
        End Set
    End Property

    ''' <summary>
    ''' Returns combined extents of all visible layers
    ''' </summary>
    Public ReadOnly Property MaxVisibleExtents() As MapWinGIS.Extents Implements Interfaces.View.MaxVisibleExtents
        Get
            Dim tExts As New MapWinGIS.Extents
            Dim bFoundVisibleLayer As Boolean
            Dim maxX, maxY, minX, minY As Double
            Dim i As Integer
            Dim dx, dy As Double

            For i = 0 To frmMain.MapMain.NumLayers - 1
                If frmMain.MapMain.get_LayerVisible(frmMain.MapMain.get_LayerHandle(i)) = True Then
                    tExts = frmMain.Layers(frmMain.MapMain.get_LayerHandle(i)).Extents
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
            dx = dx * frmMain.MapMain.ExtentPad
            maxX = maxX + dx
            minX = minX - dx

            dy = maxY - minY
            dy = dy * frmMain.MapMain.ExtentPad
            maxY = maxY + dy
            minY = minY - dy

            tExts = New MapWinGIS.Extents
            tExts.SetBounds(minX, minY, 0, maxX, maxY, 0)
            Return tExts
        End Get
    End Property

    Public Sub ShowLegend() Implements Interfaces.View.ShowLegend
        If Not frmMain.m_legendTabControl Is Nothing AndAlso frmMain.m_legendTabControl.TabCount > 1 Then
            frmMain.m_legendTabControl.SelectedIndex = 0
        End If
    End Sub

    Public Sub ShowToolbox() Implements Interfaces.View.ShowToolbox
        If Not frmMain.m_legendTabControl Is Nothing AndAlso frmMain.m_legendTabControl.TabCount > 1 Then
            frmMain.m_legendTabControl.SelectedIndex = 1
        End If
    End Sub
End Class


