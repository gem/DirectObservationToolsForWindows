Public Class ListItem
    Private mID As String = String.Empty
    Private mName As String = String.Empty

    Public Sub New(ByVal ItemID As String, ByVal ItemName As String)
        MyBase.New()
        mID = ItemID
        mName = ItemName
    End Sub

    Public Overrides Function ToString() As String
        Return Me.mName
    End Function

    Public Property ID As String
        Get
            Return Me.mID
        End Get
        Set(ByVal value As String)
            Me.mID = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return Me.mName
        End Get
        Set(ByVal value As String)
            Me.mName = value
        End Set
    End Property
End Class
