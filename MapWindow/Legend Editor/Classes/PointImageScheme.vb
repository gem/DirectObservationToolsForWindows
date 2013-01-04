'Multiple icons on a single point layer
'Added by Chris Michaelis 11/12/2006

' TODO: REMOVEME: This class has been moved to MapWindow.Interfaces.PointImageScheme so that we can use
' this data type to draw point images in the legend.
Public Class REMOVEMEPointImageScheme
    Friend m_Items As New Hashtable
    Friend m_ItemVisibility As New Hashtable
    Private m_FieldIndex As Long
    Private m_LastKnownLayerHandle As Long

    Public ReadOnly Property NumberItems() As Integer
        Get
            Return m_Items.Count
        End Get
    End Property

    Public Property Visible(ByVal FieldValue As String) As Boolean
        Get
            If Not m_ItemVisibility.Contains(FieldValue) Then Return True

            Return m_ItemVisibility(FieldValue)
        End Get
        Set(ByVal value As Boolean)
            If m_ItemVisibility.Contains(FieldValue) Then
                m_ItemVisibility(FieldValue) = value
            Else
                m_ItemVisibility.Add(FieldValue, value)
            End If
        End Set
    End Property

    Public Property ImageIndex(ByVal FieldValue As String) As Long
        Get
            If Not m_Items.Contains(FieldValue) Then Return -1

            Return m_Items(FieldValue)
        End Get
        Set(ByVal value As Long)
            If m_Items.Contains(FieldValue) Then
                m_Items(FieldValue) = value
            Else
                m_Items.Add(FieldValue, value)
            End If
        End Set
    End Property

    Public Sub Clear()
        m_Items.Clear()
        m_FieldIndex = -1
    End Sub

    Public Property FieldIndex() As Long
        Get
            Return m_FieldIndex
        End Get
        Set(ByVal value As Long)
            m_FieldIndex = value
        End Set
    End Property

    Public Property LastKnownLayerHandle() As Long
        Get
            Return m_LastKnownLayerHandle
        End Get
        Set(ByVal value As Long)
            m_LastKnownLayerHandle = value
        End Set
    End Property

    Public Sub New(ByVal lyrHandle As Long)
        m_FieldIndex = -1
        m_LastKnownLayerHandle = lyrHandle
    End Sub
End Class
