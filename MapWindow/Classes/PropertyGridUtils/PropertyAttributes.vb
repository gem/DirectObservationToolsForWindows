' (c) 2004 Wout de Zeeuw

Imports System

Namespace PropertyGridUtils
    ''' <summary>
    ''' Has some attributes defining how a property behaves in 
    ''' a <see cref="System.Windows.Forms.PropertyGrid"/>.
    ''' </summary>
    Public Class PropertyAttributes
        Implements IComparable
        Private m_name As String
        Private m_isBrowsable As Boolean
        Private m_isReadOnly As Boolean
        Private m_displayName As String
        Private m_description As String
        Private m_category As String
        Private m_order As Integer = 0

        Public Sub New(ByVal name As String)
            Me.m_name = name
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return m_name
            End Get
        End Property

        ''' <summary>
        ''' Is the property visible in a property grid.
        ''' </summary>
        Public Property IsBrowsable() As Boolean
            Get
                Return m_isBrowsable
            End Get
            Set(ByVal value As Boolean)
                m_isBrowsable = value
            End Set
        End Property

        Public Property IsReadOnly() As Boolean
            Get
                Return m_isReadOnly
            End Get
            Set(ByVal value As Boolean)
                m_isReadOnly = value
            End Set
        End Property
        Public Property DisplayName() As String
            Get
                Return m_displayName
            End Get
            Set(ByVal value As String)
                m_displayName = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return m_description
            End Get
            Set(ByVal value As String)
                m_description = value
            End Set
        End Property
        Public Property Category() As String
            Get
                Return m_category
            End Get
            Set(ByVal value As String)
                m_category = value
            End Set
        End Property
        Public Property Order() As Integer
            Get
                Return m_order
            End Get
            Set(ByVal value As Integer)
                m_order = value
            End Set
        End Property

#Region "IComparable"

        Public Function CompareTo(ByVal obj As Object) As Integer _
        Implements IComparable.CompareTo
            ' Compare this pair's order to another.  If the numeric order is the same, sort by display name.
            Dim other As PropertyAttributes = DirectCast(obj, PropertyAttributes)
            If m_order = other.Order Then
                Return String.Compare(m_displayName, other.DisplayName)
            Else
                Return IIf((m_order < other.Order), -1, 1)
            End If
        End Function

#End Region
    End Class
End Namespace

