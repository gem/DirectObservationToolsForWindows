' (c) 2004 Wout de Zeeuw

Imports System
Imports System.ComponentModel

Namespace PropertyGridUtils
    ''' <summary>
    ''' Enhances the <see cref="PropertyDescriptor"/>.
    ''' </summary>
    ''' <remarks>
    ''' All values are gotten from the <see cref="PropertyAttributes"/> 
    ''' object passed to the constructor.
    ''' </remarks>
    Public Class PropertyDescriptorEx
        Inherits PropertyDescriptor
        Private basePropertyDescriptor As PropertyDescriptor
        Private propertyAttributes As PropertyAttributes

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        Public Sub New(ByVal basePropertyDescriptor As PropertyDescriptor, ByVal propertyAttributes As PropertyAttributes)
            MyBase.New(basePropertyDescriptor)
            Me.basePropertyDescriptor = basePropertyDescriptor
            Me.propertyAttributes = propertyAttributes
        End Sub

        Public Overloads Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return basePropertyDescriptor.CanResetValue(component)
        End Function

        Public Overloads Overrides ReadOnly Property ComponentType() As Type
            Get
                Return basePropertyDescriptor.ComponentType
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property DisplayName() As String
            Get
                Return propertyAttributes.DisplayName
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property Description() As String
            Get
                Return propertyAttributes.Description
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property Category() As String
            Get
                Return propertyAttributes.Category
            End Get
        End Property

        Public Overloads Overrides Function GetValue(ByVal component As Object) As Object
            Return Me.basePropertyDescriptor.GetValue(component)
        End Function

        Public Overloads Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return propertyAttributes.IsReadOnly
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property IsBrowsable() As Boolean
            Get
                Return propertyAttributes.IsBrowsable
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property Name() As String
            Get
                Return Me.basePropertyDescriptor.Name
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property PropertyType() As Type
            Get
                Return Me.basePropertyDescriptor.PropertyType
            End Get
        End Property

        Public Overloads Overrides Sub ResetValue(ByVal component As Object)
            Me.basePropertyDescriptor.ResetValue(component)
        End Sub

        Public Overloads Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return Me.basePropertyDescriptor.ShouldSerializeValue(component)
        End Function

        Public Overloads Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            Me.basePropertyDescriptor.SetValue(component, value)
        End Sub
    End Class
End Namespace

