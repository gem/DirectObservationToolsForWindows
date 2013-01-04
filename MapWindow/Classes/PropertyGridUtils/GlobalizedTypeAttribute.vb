' (c) 2004 Wout de Zeeuw

Imports System

Namespace PropertyGridUtils
    ''' <summary>
    ''' Optional attribute for detailed specification of where
    ''' <see cref="GlobalizedTypeConverter"/> should look for its resources.
    ''' </summary>
    ''' <remarks>
    ''' See also <seealso cref="GlobalizedPropertyAttribute"/>
    ''' </remarks>
    <AttributeUsage(AttributeTargets.[Class] Or AttributeTargets.[Interface])> _
    Public Class GlobalizedTypeAttribute
        Inherits Attribute
        Private m_baseName As String

        ''' <summary>
        ''' Place where ResourceManager can find its resources.
        ''' </summary>
        Public Property BaseName() As String
            Get
                Return m_baseName
            End Get
            Set(ByVal value As String)
                m_baseName = value
            End Set
        End Property
    End Class
End Namespace
