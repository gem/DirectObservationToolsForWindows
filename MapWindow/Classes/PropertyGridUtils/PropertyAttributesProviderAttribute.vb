'
' * Created by SharpDevelop.
' * User: wout
' * Date: 9-10-2004
' * Time: 17:58
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 


Imports System
Imports System.Reflection

Namespace PropertyGridUtils
    ''' <summary>
    ''' Delegate that allows a class to change property attributes that
    ''' define a property's behaviour in 
    ''' a <see cref="System.Windows.Forms.PropertyGrid"/>.
    ''' </summary>
    Public Delegate Sub PropertyAttributesProvider(ByVal propertyAttributes As PropertyAttributes)

    ''' <summary>
    ''' Use this attribute on a property to set a property's
    ''' <see cref="PropertyAttributesProvider"/> delegate.
    ''' </summary>
    <AttributeUsage(AttributeTargets.[Property])> _
    Public Class PropertyAttributesProviderAttribute
        Inherits Attribute
        Private propertyAttributesProviderName As String

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        Public Sub New(ByVal propertyAttributesProviderName As String)
            Me.propertyAttributesProviderName = propertyAttributesProviderName
        End Sub

        ''' <summary>
        ''' Get the <see cref="PropertyAttributesProvider"/> specified by the
        ''' <see cref="PropertyAttributesProviderAttribute"/> on given target object.
        ''' </summary>
        Public Function GetPropertyAttributesProvider(ByVal target As Object) As MethodInfo
            Return target.[GetType]().GetMethod(propertyAttributesProviderName)
        End Function

        Public ReadOnly Property Name() As String
            Get
                Return propertyAttributesProviderName
            End Get
        End Property
    End Class
End Namespace

