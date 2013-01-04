Public Class Shapes
    Implements IEnumerable
    Implements Interfaces.Shapes

    Friend Class ShapeEnumerator
        Implements System.Collections.IEnumerator

        Private m_Collection As MapWindow.Interfaces.Shapes
        Private m_Idx As Integer = -1

        Public Sub New(ByVal inp As MapWindow.Shapes)
            m_Collection = inp
            m_Idx = -1
        End Sub

        Public Sub Reset() Implements IEnumerator.Reset
            m_Idx = -1
        End Sub

        Public ReadOnly Property Current() As Object Implements IEnumerator.Current
            Get
                Return m_Collection.Item(m_Idx)
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            m_Idx += 1

            If m_Idx >= m_Collection.NumShapes Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return New ShapeEnumerator(Me)
    End Function

    Public Sub New()
        m_LayerHandle = -1
    End Sub

    '-------------------Private members for public properties-------------------
    Private m_LayerHandle As Integer

    '--------------------------------------Shapes Public Interface--------------------------------------
    '22 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 1
    '-----------------------------------------------------------------------------------------------------

    '-------------------Properties-------------------
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

    Public ReadOnly Property NumShapes() As Integer Implements Interfaces.Shapes.NumShapes
        Get
            Dim sfo As MapWinGIS.Shapefile

            If m_LayerHandle <> -1 Then
                sfo = CType(frmMain.MapMain.get_GetObject(m_LayerHandle), MapWinGIS.Shapefile)

                If Not sfo Is Nothing Then
                    NumShapes = sfo.NumShapes
                Else
                    g_error = "NumShapes:  Object variable not set" & vbCrLf & "Could not get shapefile object from map"
                End If

            Else
                g_error = "NumShapes:  Invalid layer handle set in object"

            End If
        End Get
    End Property

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal Index As Integer) As Interfaces.Shape Implements Interfaces.Shapes.Item
        Get
            If Index >= 0 And Index < NumShapes Then
                Return New Shape(m_LayerHandle, Index)
            Else
                g_error = "Shape:  Invalid index"
                Return Nothing
            End If
        End Get
    End Property
End Class


