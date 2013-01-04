'********************************************************************************************************
'File Name: clsLayer.vb
'Description: Public class on the plug-in interface - represents a layer.  
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
'1/31/2005 - No change from the public domain version. 
'3/17/2006 - Chris Michaelis - Added the ability to have multiple point images in a single layer.
'28-nov-2010 - sleschinki - Added MaxVisibleScale, MinVisible scale properties to use dynamic visibility
'                           implemented in MapWinGis. AxMap.VersionNumber is used to switch between new/old mode
'********************************************************************************************************
Imports System.Runtime.InteropServices
Imports System.Xml

Public Class Layer
    Implements Interfaces.Layer


    '-------------------Private members for public properties-------------------
    Private m_LayerHandle As Integer
    Private m_Shapes As MapWindow.Shapes
    Private m_error As String



    Public Sub New()
        MyBase.New()
        m_LayerHandle = -1
    End Sub

    Public Property ExpansionBoxCustomRenderFunction() As LegendControl.ExpansionBoxCustomRenderer Implements Interfaces.Layer.ExpansionBoxCustomRenderFunction
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxCustomRenderFunction
        End Get
        Set(ByVal value As LegendControl.ExpansionBoxCustomRenderer)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxCustomRenderFunction = value
        End Set
    End Property

    ''' <summary>
    ''' Clears shapefile selection
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearSelection() Implements Interfaces.Layer.ClearSelection
        Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_Shapefile(m_LayerHandle)
        If Not sf Is Nothing Then sf.SelectNone()
        If Me.Handle = frmMain.Layers.CurrentLayer Then
            frmMain.UpdateButtons()
        End If
    End Sub

    ''' <summary>
    ''' Returns selection for the given layer. An NULL reference will be returned for non-shapefile object
    ''' </summary>
    Public ReadOnly Property SelectShapes() As MapWindow.Interfaces.SelectInfo Implements Interfaces.Layer.SelectedShapes
        Get
            If Me.LayerType = eLayerType.LineShapefile Or _
           Me.LayerType = eLayerType.PolygonShapefile Or _
           Me.LayerType = eLayerType.PointShapefile Then
                Dim info As SelectInfo = New SelectInfo(m_LayerHandle)
                Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_Shapefile(m_LayerHandle)
                If Not sf Is Nothing Then
                    For i As Integer = 0 To sf.NumShapes - 1
                        If sf.ShapeSelected(i) Then
                            Dim shp As New SelectedShape
                            shp.Add(i)
                            info.AddSelectedShape(shp)
                        End If
                    Next
                End If
                Return info
            Else
                Dim info As SelectInfo = New SelectInfo(m_LayerHandle)
                Return info
            End If
        End Get
    End Property

    Public Property ExpansionBoxForceAllowed() As Boolean Implements Interfaces.Layer.ExpansionBoxForceAllowed
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxForceAllowed
        End Get
        Set(ByVal value As Boolean)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxForceAllowed = value
        End Set
    End Property

    Public Property ExpansionBoxCustomHeightFunction() As LegendControl.ExpansionBoxCustomHeight Implements Interfaces.Layer.ExpansionBoxCustomHeightFunction
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxCustomHeightFunction
        End Get
        Set(ByVal value As LegendControl.ExpansionBoxCustomHeight)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).ExpansionBoxCustomHeightFunction = value
        End Set
    End Property

    Public Sub HatchingRecalculate() Implements MapWindow.Interfaces.Layer.HatchingRecalculate
        If Not frmMain.m_FillStippleSchemes.Contains(m_LayerHandle) _
        OrElse frmMain.m_FillStippleSchemes(m_LayerHandle) Is Nothing _
        OrElse CType(frmMain.m_FillStippleSchemes(m_LayerHandle), MapWindow.Interfaces.ShapefileFillStippleScheme).NumHatches = 0 Then
            If Not frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).HatchingScheme Is Nothing Then frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).HatchingScheme.ClearHatches()
            frmMain.MapMain.set_ShapeLayerFillStipple(m_LayerHandle, FillStipple)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Refresh()
            Exit Sub
        End If

        Dim obj As Object = frmMain.MapMain.get_GetObject(m_LayerHandle)
        If TypeOf (obj) Is MapWinGIS.Shapefile Then
            Dim sf As MapWinGIS.Shapefile = CType(obj, MapWinGIS.Shapefile)
            Dim fs As MapWindow.Interfaces.ShapefileFillStippleScheme = frmMain.m_FillStippleSchemes(m_LayerHandle)
            For i As Integer = 0 To sf.NumShapes - 1
                Dim brk As MapWindow.Interfaces.ShapefileFillStippleBreak = fs.GetHatch(sf.CellValue(fs.FieldHandle, i).ToString())
                If Not brk Is Nothing Then
                    frmMain.MapMain.set_ShapeFillStipple(m_LayerHandle, i, brk.Hatch)
                    frmMain.MapMain.set_ShapeStippleTransparent(m_LayerHandle, i, brk.Transparent)
                    frmMain.MapMain.set_ShapeStippleColor(m_LayerHandle, i, System.Drawing.ColorTranslator.ToOle(brk.LineColor))
                End If
            Next
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Refresh()
        End If
    End Sub

    Public Property FillStippleTransparent() As Boolean Implements Interfaces.Layer.FillStippleTransparency
        Get
            Return frmMain.MapMain.get_ShapeLayerStippleTransparent(m_LayerHandle)
        End Get
        Set(ByVal value As Boolean)
            frmMain.MapMain.set_ShapeLayerStippleTransparent(m_LayerHandle, value)
        End Set
    End Property

    Public Property FillStippleLineColor() As Color Implements Interfaces.Layer.FillStippleLineColor
        Get
            Return frmMain.MapMain.get_ShapeLayerStippleColor(m_LayerHandle)
        End Get
        Set(ByVal value As Color)
            frmMain.MapMain.set_ShapeLayerStippleColor(m_LayerHandle, System.Drawing.ColorTranslator.ToOle(value))
        End Set
    End Property

    Public Property FillStippleScheme() As MapWindow.Interfaces.ShapefileFillStippleScheme Implements MapWindow.Interfaces.Layer.FillStippleScheme
        Get
            If frmMain.m_FillStippleSchemes.Contains(m_LayerHandle) Then
                Return frmMain.m_FillStippleSchemes(m_LayerHandle)
            End If
            Return Nothing
        End Get
        Set(ByVal value As MapWindow.Interfaces.ShapefileFillStippleScheme)
            If Not value.LayerHandle = Handle Then value.LayerHandle = Handle
            If frmMain.m_FillStippleSchemes Is Nothing Then frmMain.m_FillStippleSchemes = New Hashtable
            If frmMain.m_FillStippleSchemes.Contains(m_LayerHandle) Then
                frmMain.m_FillStippleSchemes(m_LayerHandle) = value
            Else
                frmMain.m_FillStippleSchemes.Add(m_LayerHandle, value)
            End If
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).HatchingScheme = value
            HatchingRecalculate()
        End Set
    End Property

    Overrides Function ToString() As String Implements Interfaces.Layer.ToString
        Dim s As String = "Layer Name: " + Name + Environment.NewLine
        s += "Layer Filename: " + FileName + Environment.NewLine
        Select Case Me.LayerType
            Case Interfaces.eLayerType.Grid
                s += "Layer Type: Grid" + Environment.NewLine
            Case Interfaces.eLayerType.Image
                s += "Layer Type: Image" + Environment.NewLine
            Case Interfaces.eLayerType.Invalid
                s += "Layer Type: Invalid" + Environment.NewLine
            Case Interfaces.eLayerType.LineShapefile
                s += "Layer Type: Polyline Shapefile" + Environment.NewLine
            Case Interfaces.eLayerType.PointShapefile
                s += "Layer Type: Point Shapefile" + Environment.NewLine
            Case Interfaces.eLayerType.PolygonShapefile
                s += "Layer Type: Polygon Shapefile" + Environment.NewLine
        End Select
        s += "Extents: (" + Extents.xMin.ToString() + ", " + Extents.yMin.ToString() + " " + Extents.xMax.ToString() + ", " + Extents.yMax.ToString() + ")"

        Return s
    End Function

    Function LoadRenderingOptions() As Boolean Implements Interfaces.Layer.LoadShapeLayerProps
        If m_LayerHandle = -1 Then Return False

        Return frmMain.LoadRenderingOptions(m_LayerHandle)
    End Function

    Function LoadRenderingOptions(ByVal loadFromFilename As String) As Boolean Implements Interfaces.Layer.LoadShapeLayerProps
        If m_LayerHandle = -1 Then Return False

        Return frmMain.LoadRenderingOptions(m_LayerHandle, loadFromFilename)
    End Function

    Property VerticesVisible() As Boolean Implements Interfaces.Layer.VerticesVisible
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).VerticesVisible
        End Get
        Set(ByVal value As Boolean)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).VerticesVisible = value
            frmMain.MapMain.set_ShapeLayerDrawPoint(m_LayerHandle, value)
        End Set
    End Property

    Function SaveShapeLayerProps() As Boolean Implements Interfaces.Layer.SaveShapeLayerProps
        If m_LayerHandle = -1 Then Return False

        Return frmMain.SaveShapeLayerProps(m_LayerHandle)
    End Function

    Function SaveShapeLayerProps(ByVal saveToFilename As String) As Boolean Implements Interfaces.Layer.SaveShapeLayerProps
        If m_LayerHandle = -1 Then Return False

        Return frmMain.SaveShapeLayerProps(m_LayerHandle, saveToFilename)
    End Function



    '--------------------------------------Layer Public Interface--------------------------------------
    '22 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 1
    '----------------------------------------------------------------------------------------------------

    '-------------------Subs-------------------

    <CLSCompliant(False)> _
    Public Sub AddLabel(ByVal Text As String, ByVal TextColor As System.Drawing.Color, ByVal xPos As Double, ByVal yPos As Double, ByVal Justification As MapWinGIS.tkHJustification) Implements Interfaces.Layer.AddLabel
        frmMain.MapMain.AddLabel(m_LayerHandle, Text, MapWinUtility.Colors.ColorToUInteger(TextColor), xPos, yPos, Justification)
    End Sub

    Public Property PointImageScheme() As Interfaces.ShapefilePointImageScheme Implements Interfaces.Layer.PointImageScheme
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).PointImageScheme
        End Get
        Set(ByVal value As Interfaces.ShapefilePointImageScheme)
            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).PointImageScheme = value
        End Set
    End Property

    Public Property ShapeLayerFillTransparency() As Single Implements Interfaces.Layer.ShapeLayerFillTransparency
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return 0
            End If

            Return frmMain.MapMain.get_ShapeLayerFillTransparency(m_LayerHandle)
        End Get
        Set(ByVal value As Single)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return
            End If

            frmMain.MapMain.set_ShapeLayerFillTransparency(m_LayerHandle, value)
        End Set
    End Property
    Public Property ImageLayerFillTransparency() As Single Implements Interfaces.Layer.ImageLayerFillTransparency
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return 0
            End If

            Return frmMain.MapMain.get_ImageLayerPercentTransparent(m_LayerHandle)
        End Get
        Set(ByVal value As Single)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return
            End If

            frmMain.MapMain.set_ImageLayerPercentTransparent(m_LayerHandle, value)
        End Set
    End Property

    Public Sub ClearLabels() Implements Interfaces.Layer.ClearLabels
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"

        Else
            frmMain.MapMain.ClearLabels(m_LayerHandle)

        End If
    End Sub

    Public Sub Font(ByVal FontName As String, ByVal FontSize As Integer) Implements Interfaces.Layer.Font
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
        Else
            frmMain.MapMain.LayerFont(m_LayerHandle, FontName, FontSize)
        End If
    End Sub
    Public Sub Font(ByVal FontName As String, ByVal FontSize As Integer, ByVal FontStyle As System.Drawing.FontStyle) Implements Interfaces.Layer.Font
        'set the fontstyle
        'Paul Meems 6/11/2009
        'Bug #913: Label setup ignores font style 

        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
        Else
            'TODO Does not yet work if more styles are selected
            Dim isBold As Boolean = False
            Dim isItalic As Boolean = False
            Dim isUnderline As Boolean = False
            If FontStyle = Drawing.FontStyle.Bold Then isBold = True
            If FontStyle = Drawing.FontStyle.Italic Then isItalic = True
            If FontStyle = Drawing.FontStyle.Underline Then isUnderline = True
            If FontStyle = Drawing.FontStyle.Bold + Drawing.FontStyle.Italic Then
                isBold = True
                isItalic = True
            End If
            If FontStyle = Drawing.FontStyle.Bold + Drawing.FontStyle.Underline Then
                isBold = True
                isUnderline = True
            End If
            If FontStyle = Drawing.FontStyle.Italic + Drawing.FontStyle.Underline Then
                isItalic = True
                isUnderline = True
            End If
            If FontStyle = Drawing.FontStyle.Bold + Drawing.FontStyle.Italic + Drawing.FontStyle.Underline Then
                isBold = True
                isItalic = True
                isUnderline = True
            End If

            frmMain.MapMain.LayerFontEx(m_LayerHandle, FontName, FontSize, isBold, isItalic, isUnderline)
        End If
    End Sub

    Public Sub LabelColor(ByVal labelColor As Color)
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"

        Else
            frmMain.MapMain.LabelColor(m_LayerHandle, Convert.ToUInt32(RGB(labelColor.R, labelColor.G, labelColor.B)))
        End If
    End Sub

    Public Sub ZoomTo() Implements Interfaces.Layer.ZoomTo
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"

        Else
            frmMain.MapMain.ZoomToLayer(m_LayerHandle)
            frmMain.m_PreviewMap.UpdateLocatorBox()

        End If
    End Sub

    Public Function GetObject() As Object Implements Interfaces.Layer.GetObject
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
            Return Nothing
        Else
            GetObject = frmMain.MapMain.get_GetObject(m_LayerHandle)
        End If
    End Function

    Public Sub MoveTo(ByVal NewPosition As Integer, ByVal TargetGroup As Integer) Implements Interfaces.Layer.MoveTo
        If frmMain.Legend.Groups.IsValidHandle(TargetGroup) = False Then
            frmMain.Legend.Layers.MoveLayerWithinGroup(m_LayerHandle, NewPosition)
        Else
            frmMain.Legend.Layers.MoveLayer(m_LayerHandle, TargetGroup, NewPosition)
        End If
    End Sub

    '-------------------Properties-------------------
    Public Property HideFromLegend() As Boolean Implements Interfaces.Layer.HideFromLegend
        Get
            If m_LayerHandle = -1 Then Return False
            If frmMain.Legend.Layers.ItemByHandle(m_LayerHandle) Is Nothing Then Return False

            Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).HideFromLegend
        End Get
        Set(ByVal value As Boolean)
            If m_LayerHandle = -1 Then Return
            If frmMain.Legend.Layers.ItemByHandle(m_LayerHandle) Is Nothing Then Return

            frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).HideFromLegend = value
        End Set
    End Property

    Public Property Color() As System.Drawing.Color Implements Interfaces.Layer.Color
        Get
            Try
                If m_LayerHandle = -1 Then
                    m_error = "Layer object not initialized with a valid handle"
                Else
                    Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                    If o Is Nothing Then Exit Property

                    If TypeOf o Is MapWinGIS.Shapefile Then
                        Dim s As MapWinGIS.Shapefile = CType(o, MapWinGIS.Shapefile)
                        Select Case s.ShapefileType
                            Case MapWinGIS.ShpfileType.SHP_MULTIPOINT, MapWinGIS.ShpfileType.SHP_MULTIPOINTM, MapWinGIS.ShpfileType.SHP_MULTIPOINTZ
                                Color = frmMain.MapMain.get_ShapeLayerPointColor(m_LayerHandle)
                            Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
                                Color = frmMain.MapMain.get_ShapeLayerPointColor(m_LayerHandle)
                            Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
                                Color = frmMain.MapMain.get_ShapeLayerFillColor(m_LayerHandle)
                            Case MapWinGIS.ShpfileType.SHP_POLYLINE, MapWinGIS.ShpfileType.SHP_POLYLINEM, MapWinGIS.ShpfileType.SHP_POLYLINEZ
                                Color = frmMain.MapMain.get_ShapeLayerLineColor(m_LayerHandle)
                        End Select
                        'Color = frmMain.MapMain.get_ShapeLayerPointColor(m_LayerHandle)
                    End If
                End If
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If o Is Nothing Then Exit Property

                If TypeOf o Is MapWinGIS.Shapefile Then
                    frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
                    Try
                        frmMain.m_View.ClearSelectedShapes()

                        Dim sf As MapWinGIS.Shapefile = CType(o, MapWinGIS.Shapefile)
                        Select Case sf.ShapefileType
                            Case MapWinGIS.ShpfileType.SHP_MULTIPOINT, MapWinGIS.ShpfileType.SHP_MULTIPOINTM, MapWinGIS.ShpfileType.SHP_MULTIPOINTZ
                                frmMain.MapMain.set_ShapeLayerPointColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(Value))
                            Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
                                frmMain.MapMain.set_ShapeLayerPointColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(Value))
                            Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
                                frmMain.MapMain.set_ShapeLayerFillColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(Value))
                            Case MapWinGIS.ShpfileType.SHP_POLYLINE, MapWinGIS.ShpfileType.SHP_POLYLINEM, MapWinGIS.ShpfileType.SHP_POLYLINEZ
                                frmMain.MapMain.set_ShapeLayerLineColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(Value))
                        End Select

                        frmMain.Legend.Refresh()

                    Finally
                        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
                    End Try
                End If
                o = Nothing
            End If
        End Set
    End Property

    Public Property DrawFill() As Boolean Implements Interfaces.Layer.DrawFill
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                DrawFill = frmMain.MapMain.get_ShapeLayerDrawFill(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.MapMain.set_ShapeLayerDrawFill(m_LayerHandle, Value)
            End If
        End Set
    End Property

    Public Property Expanded() As Boolean Implements Interfaces.Layer.Expanded
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return False
            Else
                Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Expanded
            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Expanded = Value
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property Extents() As MapWinGIS.Extents Implements Interfaces.Layer.Extents
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return Nothing
            Else
                Dim newExts As New MapWinGIS.Extents
                Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If o Is Nothing Then Extents = Nothing : Exit Property

                If TypeOf o Is MapWinGIS.Shapefile Then
                    Return CType(o, MapWinGIS.Shapefile).Extents

                ElseIf TypeOf o Is MapWinGIS.Image Then
                    Dim x As MapWinGIS.Image = CType(o, MapWinGIS.Image)
                    '7-Oct-2009 Rob Cairns (Bug#1435)
                    'newExts.SetBounds(x.XllCenter - (0.5 * x.dX), x.YllCenter - (0.5 * x.dY), 0, _
                    '                  x.XllCenter - (0.5 * x.dX) + (x.Width * x.dX), x.YllCenter - (0.5 * x.dY) + (x.Height * x.dY), 0)
                    'newExts.SetBounds(x.GetOriginalXllCenter - (0.5 * x.GetOriginal_dX), x.GetOriginalYllCenter - (0.5 * x.GetOriginal_dY), 0, _
                    '                  x.GetOriginalXllCenter - (0.5 * x.GetOriginal_dX) + (x.GetOriginalWidth * x.GetOriginal_dX), x.GetOriginalYllCenter - (0.5 * x.GetOriginal_dY) + (x.GetOriginalHeight * x.GetOriginal_dY), 0)
                    newExts.SetBounds(x.OriginalXllCenter - (0.5 * x.OriginalDX), x.OriginalYllCenter - (0.5 * x.OriginalDY), 0, _
                                      x.OriginalXllCenter - (0.5 * x.OriginalDX) + (x.OriginalWidth * x.OriginalDX), x.OriginalYllCenter - (0.5 * x.OriginalDY) + (x.OriginalHeight * x.OriginalDY), 0)

                    Return newExts
                Else
                    'must be a grid or unsupported type
                    Return Nothing
                End If

                o = Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property FileName() As String Implements Interfaces.Layer.FileName
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return ""
            Else
                Dim o As Object
                If Me.LayerType = Interfaces.eLayerType.Grid Then
                    Return frmMain.MapMain.get_GridFileName(m_LayerHandle)
                Else
                    o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                    If o Is Nothing Then
                        FileName = ""
                        Exit Property
                    End If

                    If Me.LayerType = Interfaces.eLayerType.Image Then
                        Return CType(o, MapWinGIS.Image).Filename
                    ElseIf Me.LayerType = Interfaces.eLayerType.Invalid Then
                        Return ""
                    Else 'I must be a shapefile
                        Return CType(o, MapWinGIS.Shapefile).Filename
                    End If
                    o = Nothing
                End If
            End If
        End Get
    End Property

    Public Property Projection() As String Implements Interfaces.Layer.Projection
        Get
            Try
                If m_LayerHandle = -1 Then
                    m_error = "Layer object not initialized with a valid handle"
                    Return ""
                Else
                    Dim o As Object = frmMain.MapMain.get_GetObject(m_LayerHandle)
                    If TypeOf o Is MapWinGIS.Grid Then
                        Return CType(o, MapWinGIS.Grid).Header.Projection()
                    ElseIf TypeOf o Is MapWinGIS.Image Then
                        Return CType(o, MapWinGIS.Image).GetProjection()
                    ElseIf TypeOf o Is MapWinGIS.Shapefile Then
                        Return CType(o, MapWinGIS.Shapefile).Projection
                    Else
                        Return ""
                    End If
                End If
            Catch
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            Try
                If m_LayerHandle = -1 Then
                    m_error = "Layer object not initialized with a valid handle"
                Else
                    Dim o As Object = frmMain.MapMain.get_GetObject(m_LayerHandle)
                    If TypeOf o Is MapWinGIS.Grid Then
                        CType(o, MapWinGIS.Grid).AssignNewProjection(value)
                    ElseIf TypeOf o Is MapWinGIS.Image Then
                        CType(o, MapWinGIS.Image).SetProjection(value)
                    ElseIf TypeOf o Is MapWinGIS.Shapefile Then
                        CType(o, MapWinGIS.Shapefile).Projection = value
                    Else
                    End If
                End If
            Catch
            End Try
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property FillStipple() As MapWinGIS.tkFillStipple Implements Interfaces.Layer.FillStipple
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                FillStipple = frmMain.MapMain.get_ShapeLayerFillStipple(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkFillStipple)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.MapMain.set_ShapeLayerFillStipple(m_LayerHandle, Value)
            End If
        End Set
    End Property

    Public Property Handle() As Integer Implements Interfaces.Layer.Handle
        Get
            Handle = m_LayerHandle
        End Get
        Set(ByVal Value As Integer)
            If m_LayerHandle = -1 Then
                m_LayerHandle = Value
            End If
        End Set
    End Property

    Public Property LabelsVisible() As Boolean Implements Interfaces.Layer.LabelsVisible
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                LabelsVisible = frmMain.MapMain.get_LayerLabelsVisible(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.MapMain.set_LayerLabelsVisible(m_LayerHandle, Value)
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property LayerType() As Interfaces.eLayerType Implements Interfaces.Layer.LayerType
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                Return Me.GetLayerType(m_LayerHandle)
            End If
        End Get
    End Property

    Public Property ColoringScheme() As Object Implements Interfaces.Layer.ColoringScheme
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return Nothing
            Else
                Return frmMain.MapMain.GetColorScheme(m_LayerHandle)
            End If
        End Get
        Set(ByVal NewLegend As Object)
            Dim bRes As Boolean

            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                If NewLegend Is Nothing Then
                    Select Case LayerType
                        Case Interfaces.eLayerType.Grid
                            frmMain.MapMain.SetImageLayerColorScheme(m_LayerHandle, Nothing)

                        Case Interfaces.eLayerType.LineShapefile, Interfaces.eLayerType.PointShapefile, Interfaces.eLayerType.PolygonShapefile
                            Dim newScheme As New MapWinGIS.ShapefileColorScheme
                            newScheme.LayerHandle = m_LayerHandle
                            frmMain.MapMain.ApplyLegendColors(newScheme)
                    End Select
                    frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Refresh()

                    Exit Property
                Else
                    If TypeOf NewLegend Is MapWinGIS.ShapefileColorScheme Then
                        Dim scheme As MapWinGIS.ShapefileColorScheme = CType(NewLegend, MapWinGIS.ShapefileColorScheme)
                        scheme.LayerHandle = m_LayerHandle
                        bRes = CBool(frmMain.MapMain.ApplyLegendColors(NewLegend))
                        If bRes = False Then
                            m_error = frmMain.MapMain.get_ErrorMsg(frmMain.MapMain.LastErrorCode)
                            Exit Property
                        End If
                    ElseIf TypeOf NewLegend Is MapWinGIS.GridColorScheme Then
                        Dim scheme As MapWinGIS.GridColorScheme = CType(NewLegend, MapWinGIS.GridColorScheme)
                        If LayerType = Interfaces.eLayerType.Grid Then
                            frmMain.Layers.RebuildGridLayer(m_LayerHandle, GetGridObject(), scheme)
                            ColoringSchemeTools.ExportScheme(frmMain.Layers(m_LayerHandle), IO.Path.ChangeExtension(Me.FileName, ".mwleg"))
                        Else
                            frmMain.MapMain.SetImageLayerColorScheme(m_LayerHandle, NewLegend)
                        End If
                    End If
                    frmMain.MapMain.Invalidate()
                    frmMain.MapMain.Redraw()
                    frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Refresh()
                End If
            End If
        End Set
    End Property

    Public Property Icon() As Object Implements Interfaces.Layer.Icon
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return Nothing
            Else
                Return frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Icon
            End If
        End Get
        Set(ByVal Value As Object)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Icon = Value
            End If
        End Set
    End Property

    Public Property LineOrPointSize() As Single Implements Interfaces.Layer.LineOrPointSize
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                If LayerType = Interfaces.eLayerType.PointShapefile Then
                    LineOrPointSize = frmMain.MapMain.get_ShapeLayerPointSize(m_LayerHandle)
                ElseIf LayerType = Interfaces.eLayerType.LineShapefile OrElse LayerType = Interfaces.eLayerType.PolygonShapefile Then
                    LineOrPointSize = frmMain.MapMain.get_ShapeLayerLineWidth(m_LayerHandle)
                End If
            End If
        End Get
        Set(ByVal Value As Single)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
                Try
                    Select Case LayerType
                        Case Interfaces.eLayerType.LineShapefile
                            frmMain.MapMain.set_ShapeLayerLineWidth(m_LayerHandle, Value)
                        Case Interfaces.eLayerType.PointShapefile
                            frmMain.MapMain.set_ShapeLayerPointSize(m_LayerHandle, Value)
                        Case Interfaces.eLayerType.PolygonShapefile
                            frmMain.MapMain.set_ShapeLayerLineWidth(m_LayerHandle, Value)
                    End Select
                Finally
                    frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
                End Try
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property LineStipple() As MapWinGIS.tkLineStipple Implements Interfaces.Layer.LineStipple
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                LineStipple = frmMain.MapMain.get_ShapeLayerLineStipple(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkLineStipple)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                frmMain.MapMain.set_ShapeLayerLineStipple(m_LayerHandle, Value)
            End If
        End Set
    End Property

    Public Property Name() As String Implements Interfaces.Layer.Name
        Get
            Name = frmMain.MapMain.get_LayerName(m_LayerHandle)
        End Get
        Set(ByVal Value As String)
            frmMain.MapMain.set_LayerName(m_LayerHandle, Value)
            frmMain.Legend.Refresh()
        End Set
    End Property

    Public Property OutlineColor() As System.Drawing.Color Implements Interfaces.Layer.OutlineColor
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                OutlineColor = frmMain.MapMain.get_ShapeLayerLineColor(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If o Is Nothing Then Exit Property

                If TypeOf o Is MapWinGIS.Shapefile Then
                    Dim sf As MapWinGIS.Shapefile
                    sf = CType(o, MapWinGIS.Shapefile)
                    If sf.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGON Or sf.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGONM Or sf.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGONZ Then
                        frmMain.MapMain.set_ShapeLayerLineColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(Value))
                        frmMain.Legend.Refresh()
                    End If
                End If
                o = Nothing
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Property PointType() As MapWinGIS.tkPointType Implements Interfaces.Layer.PointType
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                PointType = frmMain.MapMain.get_ShapeLayerPointType(m_LayerHandle)

            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkPointType)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                frmMain.MapMain.set_ShapeLayerPointType(m_LayerHandle, Value)

            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property Shapes() As MapWindow.Interfaces.Shapes Implements Interfaces.Layer.Shapes
        Get
            If m_LayerHandle >= 0 Then
                If m_Shapes Is Nothing Then
                    m_Shapes = New MapWindow.Shapes
                    m_Shapes.LayerHandle = m_LayerHandle
                End If
                Shapes = m_Shapes
            Else
                m_error = "Invalid layer handle set in object."
                Return Nothing
            End If
        End Get
    End Property

    Public Property Tag() As String Implements Interfaces.Layer.Tag
        Get
            Tag = frmMain.MapMain.get_LayerKey(m_LayerHandle)
        End Get
        Set(ByVal Value As String)
            frmMain.MapMain.set_LayerKey(m_LayerHandle, Value)
        End Set
    End Property

    Public Property ImageTransparentColor() As System.Drawing.Color Implements Interfaces.Layer.ImageTransparentColor
        Get
            Try
                Dim img As MapWinGIS.Image

                img = CType(frmMain.MapMain.get_GetObject(m_LayerHandle), MapWinGIS.Image)
                If img Is Nothing Then Exit Property

                ImageTransparentColor = MapWinUtility.Colors.IntegerToColor(img.TransparencyColor)
                img = Nothing
            Catch ex As System.Exception
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As System.Drawing.Color)
            Try
                Dim img As MapWinGIS.Image
                Dim o As Object

                o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If Not TypeOf o Is MapWinGIS.Image Then
                    g_error = "The layer is the wrong type!"
                    Exit Property
                End If

                img = CType(o, MapWinGIS.Image)
                If img Is Nothing Then Exit Property

                img.TransparencyColor = MapWinUtility.Colors.ColorToUInteger(Value)
                ' Start Paul Meems May 30 2010
                ' Redraw is only necessary if UseTransparencyColor is true
                If img.UseTransparencyColor Then
                    frmMain.MapMain.Redraw()
                End If
                img = Nothing
                ' End Paul Meems May 30 2010
            Catch ex As System.Exception
                ShowError(ex)
            End Try

        End Set
    End Property

    Public Property ImageTransparentColor2() As System.Drawing.Color Implements Interfaces.Layer.ImageTransparentColor2
        Get
            Try
                Dim img As MapWinGIS.Image

                img = CType(frmMain.MapMain.get_GetObject(m_LayerHandle), MapWinGIS.Image)
                If img Is Nothing Then Exit Property

                ImageTransparentColor2 = MapWinUtility.Colors.IntegerToColor(img.TransparencyColor2)
                img = Nothing
            Catch ex As System.Exception
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As System.Drawing.Color)
            Try
                Dim img As MapWinGIS.Image
                Dim o As Object

                o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If Not TypeOf o Is MapWinGIS.Image Then
                    g_error = "The layer is the wrong type!"
                    Exit Property
                End If

                img = CType(o, MapWinGIS.Image)
                If img Is Nothing Then Exit Property

                img.TransparencyColor2 = MapWinUtility.Colors.ColorToUInteger(Value)
                ' Start Paul Meems May 30 2010
                ' Redraw is only necessary if UseTransparencyColor is true
                If img.UseTransparencyColor Then
                    frmMain.MapMain.Redraw()
                End If
                img = Nothing
                ' End Paul Meems May 30 2010
            Catch ex As System.Exception
                ShowError(ex)
            End Try

        End Set
    End Property

    Public Property UseTransparentColor() As Boolean Implements Interfaces.Layer.UseTransparentColor
        Get
            Dim img As MapWinGIS.Image

            img = CType(frmMain.MapMain.get_GetObject(m_LayerHandle), MapWinGIS.Image)
            If img Is Nothing Then Exit Property 'error

            UseTransparentColor = img.UseTransparencyColor
            img = Nothing
        End Get
        Set(ByVal Value As Boolean)
            Dim img As MapWinGIS.Image

            Try
                img = CType(frmMain.MapMain.get_GetObject(m_LayerHandle), MapWinGIS.Image)
            Catch
                Exit Property
            End Try
            If img Is Nothing Then Exit Property

            If Value <> img.UseTransparencyColor Then
                img.UseTransparencyColor = Value
                frmMain.MapMain.Refresh()
                frmMain.Legend.Layers.ItemByHandle(m_LayerHandle).Refresh()
            End If

            img = Nothing
        End Set
    End Property

    Public Property UserLineStipple() As Integer Implements Interfaces.Layer.UserLineStipple
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                UserLineStipple = frmMain.MapMain.get_UDLineStipple(m_LayerHandle)

            End If
        End Get
        Set(ByVal Value As Integer)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                frmMain.MapMain.set_UDLineStipple(m_LayerHandle, Value)

            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property UserPointType() As MapWinGIS.Image Implements Interfaces.Layer.UserPointType
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
                Return Nothing
            Else
                UserPointType = CType(frmMain.MapMain.get_UDPointType(m_LayerHandle), MapWinGIS.Image)
            End If
        End Get
        Set(ByVal Value As MapWinGIS.Image)
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"

            Else
                frmMain.MapMain.set_UDPointType(m_LayerHandle, Value)

            End If
        End Set
    End Property

    Public Property Visible() As Boolean Implements Interfaces.Layer.Visible
        Get
            If m_LayerHandle = -1 Then
                m_error = "Layer object not initialized with a valid handle"
            Else
                Visible = frmMain.MapMain.get_LayerVisible(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                m_error = "Invalid LayerHandle set in Object"
            Else
                frmMain.MapMain.set_LayerVisible(m_LayerHandle, Value)
                frmMain.Plugins.BroadcastMessage("LayerVisibleChanged " + Value.ToString() + " Handle=" + m_LayerHandle.ToString())
                frmMain.Legend.Refresh()
            End If
        End Set
    End Property

    Friend Function GetLayerType(ByVal LayerHandle As Integer) As MapWindow.Interfaces.eLayerType
        Dim lyrObj As Object

        lyrObj = frmMain.MapMain.get_GetObject(LayerHandle)

        If lyrObj Is Nothing Then
            Return MapWindow.Interfaces.eLayerType.Invalid
            Exit Function
        End If

        If TypeOf lyrObj Is MapWinGIS.Shapefile Then
            Dim sf As MapWinGIS.Shapefile = CType(lyrObj, MapWinGIS.Shapefile)
            Select Case sf.ShapefileType
                Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
                    Return MapWindow.Interfaces.eLayerType.PolygonShapefile

                Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
                    Return MapWindow.Interfaces.eLayerType.PointShapefile

                Case MapWinGIS.ShpfileType.SHP_POLYLINE, MapWinGIS.ShpfileType.SHP_POLYLINEM, MapWinGIS.ShpfileType.SHP_POLYLINEZ
                    Return MapWindow.Interfaces.eLayerType.LineShapefile

                Case MapWinGIS.ShpfileType.SHP_MULTIPOINT, MapWinGIS.ShpfileType.SHP_MULTIPOINTM, MapWinGIS.ShpfileType.SHP_MULTIPOINTZ
                    Return MapWindow.Interfaces.eLayerType.PointShapefile

                Case Else
                    Return MapWindow.Interfaces.eLayerType.Invalid
            End Select
        ElseIf TypeOf lyrObj Is MapWinGIS.Image Then
            If frmMain.Legend.Layers.ItemByHandle(LayerHandle).Type = MapWindow.Interfaces.eLayerType.Grid Then
                Return MapWindow.Interfaces.eLayerType.Grid
            Else
                Return MapWindow.Interfaces.eLayerType.Image
            End If
        Else
            Return MapWindow.Interfaces.eLayerType.Invalid
        End If
    End Function

    <CLSCompliant(False)> _
    Public ReadOnly Property GetGridObject() As MapWinGIS.Grid Implements MapWindow.Interfaces.Layer.GetGridObject
        Get
            If Not frmMain.m_layers Is Nothing AndAlso frmMain.m_layers.m_Grids.ContainsKey(m_LayerHandle) Then
                Return CType(frmMain.m_layers.m_Grids(m_LayerHandle), MapWinGIS.Grid)
            Else
                Return Nothing
            End If
        End Get
    End Property


    Public Property GlobalPosition() As Integer Implements MapWindow.Interfaces.Layer.GlobalPosition
        Get
            Return frmMain.MapMain.get_LayerPosition(m_LayerHandle)
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.MoveLayer(frmMain.MapMain.get_LayerPosition(m_LayerHandle), Value)
        End Set
    End Property

    Public Property GroupPosition() As Integer Implements MapWindow.Interfaces.Layer.GroupPosition
        Get
            Return frmMain.Legend.Layers.PositionInGroup(m_LayerHandle)
        End Get
        Set(ByVal Value As Integer)
            frmMain.Legend.Layers.MoveLayerWithinGroup(m_LayerHandle, Value)
        End Set
    End Property

    Public Property GroupHandle() As Integer Implements MapWindow.Interfaces.Layer.GroupHandle
        Get
            Return frmMain.Legend.Layers.GroupOf(m_LayerHandle)
        End Get
        Set(ByVal Value As Integer)
            Dim TopLayer As Integer

            If frmMain.Legend.Groups.IsValidHandle(Value) Then
                TopLayer = frmMain.Legend.Groups.ItemByHandle(Value).LayerCount
                frmMain.Legend.Layers.MoveLayer(m_LayerHandle, Value, TopLayer)
            End If

        End Set
    End Property

    Public Function GetUserFillStipple(ByVal Row As Integer) As Integer Implements MapWindow.Interfaces.Layer.GetUserFillStipple
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"

        Else
            Return frmMain.MapMain.get_UDFillStipple(m_LayerHandle, Row)

        End If
    End Function


    Public Sub SetUserFillStipple(ByVal Row As Integer, ByVal Value As Integer) Implements MapWindow.Interfaces.Layer.SetUserFillStipple
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"

        Else
            frmMain.MapMain.set_UDFillStipple(m_LayerHandle, Row, Value)

        End If
    End Sub

    <CLSCompliant(False)> _
    Public Function UserPointImageListAdd(ByVal newValue As MapWinGIS.Image) As Long Implements MapWindow.Interfaces.Layer.UserPointImageListAdd
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
            Return -1
        Else
            Return frmMain.MapMain.set_UDPointImageListAdd(m_LayerHandle, newValue)
        End If
    End Function

    Public Function UserPointImageListCount() As Long Implements MapWindow.Interfaces.Layer.UserPointImageListCount
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
            Return -1
        Else
            Return frmMain.MapMain.get_UDPointImageListCount(m_LayerHandle)
        End If
    End Function

    <CLSCompliant(False)> _
    Public Function UserPointImageListItem(ByVal ImageIndex As Long) As MapWinGIS.Image Implements MapWindow.Interfaces.Layer.UserPointImageListItem
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
            Return Nothing
        Else
            If ImageIndex > frmMain.MapMain.get_UDPointImageListCount(m_LayerHandle) - 1 Then
                m_error = "Specified index not set to an image. Use UserPointImageListAdd"
                Return Nothing
            Else
                Return CType(frmMain.MapMain.get_UDPointImageListItem(m_LayerHandle, CInt(ImageIndex)), MapWinGIS.Image)
            End If
        End If
    End Function

    Public Sub ClearUDPointImageList() Implements Interfaces.Layer.ClearUDPointImageList
        If m_LayerHandle = -1 Then
            m_error = "Layer object not initialized with a valid handle"
        Else
            frmMain.MapMain.ClearUDPointImageList(m_LayerHandle)
        End If
    End Sub

    Public Sub ShowVertices(ByVal color As System.Drawing.Color, ByVal vertexSize As Integer) Implements MapWindow.Interfaces.Layer.ShowVertices
        If Me.LayerType <> Interfaces.eLayerType.PointShapefile Then
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
            Try
                frmMain.MapMain.set_ShapeLayerPointSize(m_LayerHandle, CSng(vertexSize))
                frmMain.MapMain.set_ShapeLayerPointColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(color))
                frmMain.MapMain.set_ShapeLayerDrawPoint(m_LayerHandle, True)
            Finally
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End Try
        End If
    End Sub

    Public Sub HideVertices() Implements MapWindow.Interfaces.Layer.HideVertices
        If Me.LayerType <> Interfaces.eLayerType.PointShapefile Then
            frmMain.MapMain.set_ShapeLayerDrawPoint(m_LayerHandle, False)
        End If
    End Sub

    Public Sub UpdateLabelInfo() Implements MapWindow.Interfaces.Layer.UpdateLabelInfo
        'reload the label info for any changes 
        frmMain.m_Labels.LoadLabelInfo(Me, Nothing)
    End Sub

    <CLSCompliant(False)> _
    Public Property DynamicVisibilityScale() As Double Implements MapWindow.Interfaces.Layer.DynamicVisibilityScale
        Get
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                If frmMain.m_AutoVis Is Nothing Then frmMain.m_AutoVis = New DynamicVisibilityClass()
                Return frmMain.m_AutoVis(m_LayerHandle).DynamicScale
            Else
                MapWinUtility.Logger.Dbg("DynamicVisibilityScale property is obsolete. Use MinVisibleScale and MaxVisibleScale instead.")
                Return 0.0
            End If
        End Get
        Set(ByVal Value As Double)
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                If frmMain.m_AutoVis Is Nothing Then frmMain.m_AutoVis = New DynamicVisibilityClass()
                frmMain.m_AutoVis(m_LayerHandle).DynamicScale = Value
            Else
                ' do nothing
                MapWinUtility.Logger.Dbg("DynamicVisibilityScale property is obsolete. Use MinVisibleScale and MaxVisibleScale instead.")
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property DynamicVisibilityExtents() As MapWinGIS.Extents Implements MapWindow.Interfaces.Layer.DynamicVisibilityExtents
        Get
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                If frmMain.m_AutoVis Is Nothing Then frmMain.m_AutoVis = New DynamicVisibilityClass()
                Return frmMain.m_AutoVis(m_LayerHandle).DynamicExtents
            Else
                MapWinUtility.Logger.Dbg("DynamicVisibilityExtents property is obsolete. Use MinVisibleScale and MaxVisibleScale instead.")
                Return Nothing
            End If
        End Get
        Set(ByVal Value As MapWinGIS.Extents)
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                If frmMain.m_AutoVis Is Nothing Then frmMain.m_AutoVis = New DynamicVisibilityClass()
                frmMain.m_AutoVis(m_LayerHandle).DynamicExtents = Value
            Else
                MapWinUtility.Logger.Dbg("DynamicVisibilityExtents property is obsolete. Use MinVisibleScale and MaxVisibleScale instead.")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Toggles dynamic visibility of the layer
    ''' </summary>
    Public Property UseDynamicVisibility() As Boolean Implements MapWindow.Interfaces.Layer.UseDynamicVisibility
        Get
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                Return frmMain.m_AutoVis(m_LayerHandle).UseDynamicExtents
            Else
                Return frmMain.MapMain.get_LayerDynamicVisibility(m_LayerHandle)
            End If
        End Get
        Set(ByVal Value As Boolean)
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                frmMain.m_AutoVis(m_LayerHandle).UseDynamicExtents = Value
            Else
                frmMain.MapMain.set_LayerDynamicVisibility(m_LayerHandle, Value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Addresses AxMap.LayerMaxVisibleScale property
    ''' </summary>
    Public Property MaxVisibleScale() As Double Implements Interfaces.Layer.MaxVisibleScale
        Get
            Return frmMain.MapMain.get_LayerMaxVisibleScale(Me.m_LayerHandle)
        End Get
        Set(ByVal value As Double)
            frmMain.MapMain.set_LayerMaxVisibleScale(Me.m_LayerHandle, value)
        End Set
    End Property

    ''' <summary>
    ''' Addresses AxMap.LayerMinVisibleScale property
    ''' </summary>
    Public Property MinVisibleScale() As Double Implements Interfaces.Layer.MinVisibleScale
        Get
            Return frmMain.MapMain.get_LayerMinVisibleScale(Me.m_LayerHandle)
        End Get
        Set(ByVal value As Double)
            frmMain.MapMain.set_LayerMinVisibleScale(Me.m_LayerHandle, value)
        End Set
    End Property

    Public Sub ReloadLabels(ByVal lblFilename As String) Implements MapWindow.Interfaces.Layer.ReloadLabels
        frmMain.m_Labels.LoadLabelInfo(Me, Nothing, lblFilename)
    End Sub

    Public Property SkipOverDuringSave() As Boolean Implements MapWindow.Interfaces.Layer.SkipOverDuringSave
        Get
            For g As Integer = 0 To frmMain.Legend.Groups.Count - 1
                For l As Integer = 0 To frmMain.Legend.Groups(g).LayerCount - 1
                    If frmMain.Legend.Groups(g)(l).Handle = m_LayerHandle Then
                        Return frmMain.Legend.Groups(g)(l).SkipOverDuringSave
                    End If
                Next
            Next

            Return False
        End Get
        Set(ByVal value As Boolean)
            For g As Integer = 0 To frmMain.Legend.Groups.Count - 1
                For l As Integer = 0 To frmMain.Legend.Groups(g).LayerCount - 1
                    If frmMain.Legend.Groups(g)(l).Handle = m_LayerHandle Then
                        frmMain.Legend.Groups(g)(l).SkipOverDuringSave = value
                        Return
                    End If
                Next
            Next
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Sub AddLabelEx(ByVal Text As String, ByVal TextColor As System.Drawing.Color, ByVal xPos As Double, ByVal yPos As Double, ByVal Justification As MapWinGIS.tkHJustification, ByVal Rotation As Double) Implements Interfaces.Layer.AddLabelEx
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        frmMain.MapMain.AddLabelEx(m_LayerHandle, Text, System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(TextColor)), xPos, yPos, Justification, Rotation)
    End Sub

    Public Property LabelsOffset() As Integer Implements Interfaces.Layer.LabelsOffset
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.get_LayerLabelsOffset(m_LayerHandle)
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.set_LayerLabelsOffset(m_LayerHandle, Value)
        End Set
    End Property

    Public Property LabelsScale() As Boolean Implements Interfaces.Layer.LabelsScale
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.get_LayerLabelsScale(m_LayerHandle)
        End Get
        Set(ByVal Value As Boolean)
            frmMain.MapMain.set_LayerLabelsScale(m_LayerHandle, Value)
        End Set
    End Property

    Public Property LabelsShadow() As Boolean Implements Interfaces.Layer.LabelsShadow
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.get_LayerLabelsShadow(m_LayerHandle)
        End Get
        Set(ByVal Value As Boolean)
            frmMain.MapMain.set_LayerLabelsShadow(m_LayerHandle, Value)
        End Set
    End Property

    Public Property LabelsShadowColor() As System.Drawing.Color Implements Interfaces.Layer.LabelsShadowColor
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.get_LayerLabelsShadowColor(m_LayerHandle)
        End Get
        Set(ByVal Value As System.Drawing.Color)
            frmMain.MapMain.set_LayerLabelsShadowColor(m_LayerHandle, System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Value)))
        End Set
    End Property

    Public Property LineSeparationFactor() As Integer Implements Interfaces.Layer.LineSeparationFactor
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.LineSeparationFactor
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.LineSeparationFactor = Value
        End Set
    End Property

    Public Property StandardViewWidth() As Double Implements Interfaces.Layer.StandardViewWidth
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return 0 'Unsupported on get.
        End Get
        Set(ByVal Value As Double)
            frmMain.MapMain.SetLayerStandardViewWidth(m_LayerHandle, Value)
        End Set
    End Property

    Public Property UseLabelCollision() As Boolean Implements Interfaces.Layer.UseLabelCollision
        'Added for version 4 to support 3.1 plug-ins
        'Filled in by Chris Michaelis, April 2006
        Get
            Return frmMain.MapMain.get_UseLabelCollision(m_LayerHandle)
        End Get
        Set(ByVal Value As Boolean)
            frmMain.MapMain.set_UseLabelCollision(m_LayerHandle, Value)
        End Set
    End Property

    '-------------------Start Paul Meems 12 May 2010-------------------
    Public Property ShapeLayerFillColor() As System.Drawing.Color Implements Interfaces.Layer.ShapeLayerFillColor
        Get
            Return frmMain.MapMain.get_ShapeLayerFillColor(m_LayerHandle)
        End Get
        Set(ByVal value As System.Drawing.Color)
            frmMain.MapMain.set_ShapeLayerFillColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(value))
        End Set
    End Property
    Public Property ShapeLayerLineColor() As System.Drawing.Color Implements Interfaces.Layer.ShapeLayerLineColor
        Get
            Return frmMain.MapMain.get_ShapeLayerLineColor(m_LayerHandle)
        End Get
        Set(ByVal value As System.Drawing.Color)
            frmMain.MapMain.set_ShapeLayerLineColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(value))
        End Set
    End Property
    Public Property ShapeLayerPointColor() As System.Drawing.Color Implements Interfaces.Layer.ShapeLayerPointColor
        Get
            Return frmMain.MapMain.get_ShapeLayerPointColor(m_LayerHandle)
        End Get
        Set(ByVal value As System.Drawing.Color)
            frmMain.MapMain.set_ShapeLayerPointColor(m_LayerHandle, MapWinUtility.Colors.ColorToUInteger(value))
        End Set
    End Property
    Public Property ShapeLayerPointSize() As Single Implements Interfaces.Layer.ShapeLayerPointSize
        Get
            Return frmMain.MapMain.get_ShapeLayerPointSize(m_LayerHandle)
        End Get
        Set(ByVal value As Single)
            frmMain.MapMain.set_ShapeLayerPointSize(m_LayerHandle, value)
        End Set
    End Property

    '-------------------End Paul Meems 12 May 2010-------------------

    ''' <summary>
    ''' Returns custom object associated with layer
    ''' </summary>
    Public Function GetCustomObject(ByVal key As String) As Object Implements Interfaces.Layer.GetCustomObject
        Dim layer As LegendControl.Layer = frmMain.Legend.Layers.ItemByHandle(Me.Handle)
        If Not layer Is Nothing Then
            Return layer.GetCustomObject(key)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Sets custom object for the layer
    ''' </summary>
    Public Sub SetCustomObject(ByVal obj As Object, ByVal key As String) Implements Interfaces.Layer.SetCustomObject
        Dim layer As LegendControl.Layer = frmMain.Legend.Layers.ItemByHandle(Me.Handle)
        If Not layer Is Nothing Then
            layer.SetCustomObject(obj, key)
        End If
    End Sub

    Public Function LoadShapefileCategories(ByVal filename As String) As Boolean Implements Interfaces.Layer.LoadShapefileCategories

        Dim sf As MapWinGIS.Shapefile = TryCast(Me.GetObject(), MapWinGIS.Shapefile)
        If Not sf Is Nothing Then
            If Not System.IO.File.Exists(filename) Then Return False

            Try
                Dim xmlDoc As New XmlDocument
                xmlDoc.Load(filename)

                If (Not xmlDoc.DocumentElement.HasAttribute("FileVersion") Or Not xmlDoc.DocumentElement.HasAttribute("FileType")) Then
                    MessageBox.Show("Failed to load categories: invalid file", frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Dim s As String = xmlDoc.DocumentElement.Attributes("FileType").InnerText
                    If (s.ToLower() = "shapefilecategories") Then
                        Dim xel As XmlElement = xmlDoc.DocumentElement("Categories")
                        sf.Categories.Deserialize(xel.InnerText)
                        Return True
                    Else
                        MessageBox.Show("Failed to load categories: invalid file", frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Failed to load options." + Environment.NewLine + ex.Message, _
                                 frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Return False
    End Function

    Public Function SaveShapefileCategories(ByVal filename As String) As Boolean Implements Interfaces.Layer.SaveShapefileCategories

        Dim sf As MapWinGIS.Shapefile = TryCast(Me.GetObject(), MapWinGIS.Shapefile)
        If Not sf Is Nothing Then
            If System.IO.File.Exists(filename) Then Return False

            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml("<MapWindow version= '" + "'></MapWindow>")

            Dim xelRoot As XmlElement = xmlDoc.DocumentElement
            Dim attr As XmlAttribute = xmlDoc.CreateAttribute("FileType")
            attr.InnerText = "ShapefileCategories"
            xelRoot.Attributes.Append(attr)

            attr = xmlDoc.CreateAttribute("FileVersion")
            attr.InnerText = "0"
            xelRoot.Attributes.Append(attr)

            Dim xel As XmlElement = xmlDoc.CreateElement("Categories")
            Dim s As String = sf.Categories.Serialize()
            xel.InnerText = s
            xelRoot.AppendChild(xel)

            Try
                xmlDoc.Save(filename)
            Catch ex As Exception
                MessageBox.Show("Failed to save options." + Environment.NewLine + ex.Message, _
                             frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Function
End Class
