Public Class Shape
    Implements Interfaces.Shape

    '-------------------Private members for public properties-------------------
    Private m_LayerHandle As Integer
    Private m_ShapeIndex As Integer

    Public Sub New()
        m_LayerHandle = -1
        m_ShapeIndex = -1
    End Sub

    Friend Sub New(ByVal LayerHandle As Integer, ByVal ShapeIndex As Integer)
        m_LayerHandle = LayerHandle
        m_ShapeIndex = ShapeIndex
    End Sub
    '--------------------------------------Shape Public Interface--------------------------------------
    '22 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 1
    '----------------------------------------------------------------------------------------------------

    '-------------------Subs-------------------
    Public Sub ZoomTo() Implements Interfaces.Shape.ZoomTo
        If m_LayerHandle = -1 Then
            g_error = "ZoomTo:  Invalid LayerHandle set in object"

        ElseIf m_ShapeIndex = -1 Then
            g_error = "ZoomTo:  Invalid ShapeIndex set in object"

        Else
            frmMain.MapMain.ZoomToShape(m_LayerHandle, m_ShapeIndex)
            frmMain.m_PreviewMap.UpdateLocatorBox()

        End If
    End Sub

    '-------------------Properties-------------------
    Public Property ShapeFillTransparency() As Single Implements Interfaces.Shape.ShapeFillTransparency
        Get
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"
            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"
            Else
                Return frmMain.MapMain.get_ShapeFillTransparency(m_LayerHandle, m_ShapeIndex)
            End If

            Return 0
        End Get
        Set(ByVal value As Single)
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"
            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"
            Else
                frmMain.MapMain.set_ShapeFillTransparency(m_LayerHandle, m_ShapeIndex, value)
            End If
        End Set
    End Property
    Public Property ShapePointImageListID() As Long Implements Interfaces.Shape.ShapePointImageListID
        Get
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"
            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"
            Else
                Return frmMain.MapMain.get_ShapePointImageListID(m_LayerHandle, m_ShapeIndex)
            End If
        End Get
        Set(ByVal value As Long)
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"
            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"
            Else
                frmMain.MapMain.set_ShapePointImageListID(m_LayerHandle, m_ShapeIndex, CInt(value))
            End If
        End Set
    End Property

    Public Property Color() As System.Drawing.Color Implements Interfaces.Shape.Color
        Get
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"

            Else
                Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If o Is Nothing Then Exit Property

                If TypeOf o Is MapWinGIS.Shapefile Then
                    Dim s As MapWinGIS.Shapefile
                    s = CType(o, MapWinGIS.Shapefile)
                    Select Case s.ShapefileType
                        Case MapWinGIS.ShpfileType.SHP_MULTIPOINT, MapWinGIS.ShpfileType.SHP_MULTIPOINTM, MapWinGIS.ShpfileType.SHP_MULTIPOINTZ
                            Color = frmMain.MapMain.get_ShapePointColor(m_LayerHandle, m_ShapeIndex)
                        Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
                            Color = frmMain.MapMain.get_ShapePointColor(m_LayerHandle, m_ShapeIndex)
                        Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
                            Color = frmMain.MapMain.get_ShapeFillColor(m_LayerHandle, m_ShapeIndex)
                        Case MapWinGIS.ShpfileType.SHP_POLYLINE, MapWinGIS.ShpfileType.SHP_POLYLINEM, MapWinGIS.ShpfileType.SHP_POLYLINEZ
                            Color = frmMain.MapMain.get_ShapeLineColor(m_LayerHandle, m_ShapeIndex)
                    End Select
                End If
                o = Nothing
                'Color = frmMain.MapMain.get_ShapePointColor(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If m_LayerHandle = -1 Then
                g_error = "Color:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "Color:  Invalid ShapeIndex set in object"

            Else
                Try
                    Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                    If o Is Nothing Then Exit Property

                    If TypeOf o Is MapWinGIS.Shapefile Then
                        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
                        Try
                            frmMain.m_View.ClearSelectedShapes()
                            Dim s As MapWinGIS.Shapefile = CType(o, MapWinGIS.Shapefile)

                            Select Case s.ShapefileType
                                Case MapWinGIS.ShpfileType.SHP_MULTIPOINT, MapWinGIS.ShpfileType.SHP_MULTIPOINTM, MapWinGIS.ShpfileType.SHP_MULTIPOINTZ
                                    frmMain.MapMain.set_ShapePointColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(Value))
                                Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
                                    frmMain.MapMain.set_ShapePointColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(Value))
                                Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
                                    frmMain.MapMain.set_ShapeFillColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(Value))
                                Case MapWinGIS.ShpfileType.SHP_POLYLINE, MapWinGIS.ShpfileType.SHP_POLYLINEM, MapWinGIS.ShpfileType.SHP_POLYLINEZ
                                    frmMain.MapMain.set_ShapeLineColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(Value))
                            End Select
                        Finally
                            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
                        End Try
                    End If
                    o = Nothing
                Catch ex As System.Exception
                    g_error = ex.Message
                    ShowError(ex)
                End Try
            End If
        End Set
    End Property

    Public Property DrawFill() As Boolean Implements Interfaces.Shape.DrawFill
        Get
            If m_LayerHandle = -1 Then
                g_error = "DrawFill:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "DrawFill:  Invalid ShapeIndex set in object"

            Else
                DrawFill = frmMain.MapMain.get_ShapeDrawFill(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                g_error = "DrawFill:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "DrawFill:  Invalid ShapeIndex set in object"

            Else
                frmMain.MapMain.set_ShapeDrawFill(m_LayerHandle, m_ShapeIndex, Value)

            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property FillStipple() As MapWinGIS.tkFillStipple Implements Interfaces.Shape.FillStipple
        Get
            If m_LayerHandle = -1 Then
                g_error = "FillStipple:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "FillStipple:  Invalid ShapeIndex set in object"

            Else
                FillStipple = frmMain.MapMain.get_ShapeFillStipple(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkFillStipple)
            If m_LayerHandle = -1 Then
                g_error = "FillStipple:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "FillStipple:  Invalid ShapeIndex set in object"

            Else
                frmMain.MapMain.set_ShapeFillStipple(m_LayerHandle, m_ShapeIndex, Value)

            End If
        End Set
    End Property

    Friend Property LayerHandle() As Integer
        Get
            LayerHandle = m_LayerHandle
        End Get
        Set(ByVal Value As Integer)
            If m_LayerHandle = -1 Then
                m_LayerHandle = Value
            End If
        End Set
    End Property

    Public Property LineOrPointSize() As Single Implements Interfaces.Shape.LineOrPointSize
        Get
            If m_LayerHandle = -1 Then
                g_error = "LineOrPointSize:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "LineOrPointSize:  Invalid ShapeIndex set in object"

            Else
                Dim LayerType As Interfaces.eLayerType = frmMain.m_layers(m_LayerHandle).LayerType
                If LayerType = Interfaces.eLayerType.PointShapefile Then
                    LineOrPointSize = frmMain.MapMain.get_ShapeLayerPointSize(m_LayerHandle)
                ElseIf LayerType = Interfaces.eLayerType.LineShapefile OrElse LayerType = Interfaces.eLayerType.PolygonShapefile Then
                    LineOrPointSize = frmMain.MapMain.get_ShapeLayerLineWidth(m_LayerHandle)
                End If
            End If
        End Get
        Set(ByVal Value As Single)
            If m_LayerHandle = -1 Then
                g_error = "LineOrPointSize:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "LineOrPointSize:  Invalid ShapeIndex set in object"

            Else
                Select Case frmMain.m_layers(m_LayerHandle).LayerType
                    Case Interfaces.eLayerType.LineShapefile
                        frmMain.MapMain.set_ShapeLineWidth(m_LayerHandle, m_ShapeIndex, Value)
                    Case Interfaces.eLayerType.PointShapefile
                        frmMain.MapMain.set_ShapePointSize(m_LayerHandle, m_ShapeIndex, Value)
                    Case Interfaces.eLayerType.PolygonShapefile
                        frmMain.MapMain.set_ShapeLineWidth(m_LayerHandle, m_ShapeIndex, Value)
                End Select
            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property LineStipple() As MapWinGIS.tkLineStipple Implements Interfaces.Shape.LineStipple
        Get
            If m_LayerHandle = -1 Then
                g_error = "LineStipple:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "LineStipple:  Invalid ShapeIndex set in object"

            Else
                LineStipple = frmMain.MapMain.get_ShapeLineStipple(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkLineStipple)
            If m_LayerHandle = -1 Then
                g_error = "LineStipple:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "LineStipple:  Invalid ShapeIndex set in object"

            Else
                frmMain.MapMain.set_ShapeLineStipple(m_LayerHandle, m_ShapeIndex, Value)

            End If
        End Set
    End Property

    Public Property OutlineColor() As System.Drawing.Color Implements Interfaces.Shape.OutlineColor
        Get
            If m_LayerHandle = -1 Then
                g_error = "OutlineColor:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "OutlineColor:  Invalid ShapeIndex set in object"

            Else
                OutlineColor = frmMain.MapMain.get_ShapeLineColor(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If m_LayerHandle = -1 Then
                g_error = "OutlineColor:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "OutlineColor:  Invalid ShapeIndex set in object"

            Else
                Dim o As Object : o = frmMain.MapMain.get_GetObject(m_LayerHandle)
                If o Is Nothing Then Exit Property

                If TypeOf o Is MapWinGIS.Shapefile Then
                    Dim s As MapWinGIS.Shapefile = CType(o, MapWinGIS.Shapefile)
                    If s.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGON Or s.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGONM Or s.ShapefileType = MapWinGIS.ShpfileType.SHP_POLYGONZ Then
                        frmMain.MapMain.set_ShapeLineColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(Value))
                    End If

                End If

                o = Nothing

            End If
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property PointType() As MapWinGIS.tkPointType Implements Interfaces.Shape.PointType
        Get
            If m_LayerHandle = -1 Then
                g_error = "Shape.PointType:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "PointType:  Invalid ShapeIndex set in object"

            Else
                PointType = frmMain.MapMain.get_ShapePointType(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As MapWinGIS.tkPointType)
            If m_LayerHandle = -1 Then
                g_error = "PointType:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "PointType:  Invalid ShapeIndex set in object"

            Else
                frmMain.MapMain.set_ShapePointType(m_LayerHandle, m_ShapeIndex, Value)

            End If
        End Set
    End Property

    Friend Property ShapeIndex() As Integer
        Get
            ShapeIndex = m_ShapeIndex
        End Get
        Set(ByVal Value As Integer)
            If m_ShapeIndex = -1 Then
                m_ShapeIndex = Value

            Else
                g_error = "ShapeIndex:  Cannot modify property after it has been set"

            End If
        End Set
    End Property

    Public Property Visible() As Boolean Implements Interfaces.Shape.Visible
        Get
            If m_LayerHandle = -1 Then
                g_error = "Visible:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "Visible:  Invalid ShapeIndex set in object"

            Else
                Visible = frmMain.MapMain.get_ShapeVisible(m_LayerHandle, m_ShapeIndex)

            End If
        End Get
        Set(ByVal Value As Boolean)
            If m_LayerHandle = -1 Then
                g_error = "Visible:  Invalid LayerHandle set in object"

            ElseIf m_ShapeIndex = -1 Then
                g_error = "Visible:  Invalid ShapeIndex set in object"

            Else
                frmMain.MapMain.set_ShapeVisible(m_LayerHandle, m_ShapeIndex, Value)

            End If
        End Set
    End Property

    Public Sub ShowVertices(ByVal color As System.Drawing.Color, ByVal vertexSize As Integer) Implements MapWindow.Interfaces.Shape.ShowVertices
        If frmMain.m_layers(m_LayerHandle).LayerType <> Interfaces.eLayerType.PointShapefile Then
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
            Try
                frmMain.MapMain.set_ShapePointSize(m_LayerHandle, m_ShapeIndex, CSng(vertexSize))
                frmMain.MapMain.set_ShapePointColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(color))
                frmMain.MapMain.set_ShapeDrawPoint(m_LayerHandle, m_ShapeIndex, True)
            Finally
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End Try
        End If
    End Sub

    Public Sub HideVertices() Implements MapWindow.Interfaces.Shape.HideVertices
        If frmMain.m_layers(m_LayerHandle).LayerType <> Interfaces.eLayerType.PointShapefile Then
            frmMain.MapMain.set_ShapeDrawPoint(m_LayerHandle, m_ShapeIndex, False)
        End If
    End Sub
    '-------------------Start Paul Meems 12 May 2010-------------------
    Public Property ShapeFillColor() As System.Drawing.Color Implements Interfaces.Shape.ShapeFillColor
        Get
            Return frmMain.MapMain.get_ShapeFillColor(m_LayerHandle, m_ShapeIndex)
        End Get
        Set(ByVal value As System.Drawing.Color)
            frmMain.MapMain.set_ShapeFillColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(value))
        End Set
    End Property
    Public Property ShapeLineColor() As System.Drawing.Color Implements Interfaces.Shape.ShapeLineColor
        Get
            Return frmMain.MapMain.get_ShapeLineColor(m_LayerHandle, m_ShapeIndex)
        End Get
        Set(ByVal value As System.Drawing.Color)
            frmMain.MapMain.set_ShapeLineColor(m_LayerHandle, m_ShapeIndex, MapWinUtility.Colors.ColorToUInteger(value))
        End Set
    End Property
    Public Property ShapeLineWidth() As Single Implements Interfaces.Shape.ShapeLineWidth
        Get
            Return frmMain.MapMain.get_ShapeLineWidth(m_LayerHandle, m_ShapeIndex)
        End Get
        Set(ByVal value As Single)
            frmMain.MapMain.set_ShapeLineWidth(m_LayerHandle, m_ShapeIndex, value)
        End Set
    End Property

    '-------------------End Paul Meems 12 May 2010-------------------
End Class


