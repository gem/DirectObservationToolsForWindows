' Original ripped from Gerd Klevesaat,
' http://www.codeguru.com/Csharp/Csharp/cs_controls/propertygrid/comments.php/c4795
'
' Made minor improvements.
'
' (c) 2004 Wout de Zeeuw

Imports System
Imports System.Resources

Namespace PropertyGridUtils
    ''' <summary>
    ''' Optional attribute for detailed specification of where
    ''' <see cref="GlobalizedTypeConverter"/> should look for its resources.
    ''' </summary>
    ''' <remarks>
    ''' See also <seealso cref="GlobalizedTypeAttribute"/>
    ''' </remarks>
    <AttributeUsage(AttributeTargets.[Property])> _
    Public Class GlobalizedPropertyAttribute
        Inherits Attribute
        Private m_baseName As String
        Private m_displayNameId As String
        Private m_descriptionId As String
        Private m_categoryId As String

        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>
        ''' Place where <see cref="ResourceManager"/> can find its resources.
        ''' </summary>
        Public Property BaseName() As String
            Get
                Return m_baseName
            End Get
            Set(ByVal value As String)
                m_baseName = value
            End Set
        End Property

        ''' <summary>
        ''' Resource name for a property's DisplayName.
        ''' </summary>
        Public Property DisplayNameId() As String
            Get
                Return m_displayNameId
            End Get
            Set(ByVal value As String)
                m_displayNameId = value
            End Set
        End Property

        ''' <summary>
        ''' Resource name for a property's Description.
        ''' </summary>
        Public Property DescriptionId() As String
            Get
                Return m_descriptionId
            End Get
            Set(ByVal value As String)
                m_descriptionId = value
            End Set
        End Property

        ''' <summary>
        ''' Resource name for a property's Category.
        ''' </summary>
        Public Property CategoryId() As String
            Get
                Return m_categoryId
            End Get
            Set(ByVal value As String)
                m_categoryId = value
            End Set
        End Property
    End Class
End Namespace

