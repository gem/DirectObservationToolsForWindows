'
' (C) Paul Tingey 2004 
'
Imports System

Namespace PropertyGridUtils
    <AttributeUsage(AttributeTargets.[Property])> _
    Public Class PropertyOrderAttribute
        Inherits Attribute
        '
        ' Simple attribute to allow the order of a property to be specified
        '
        Private m_order As Integer

        Public Sub New(ByVal order As Integer)
            Me.m_order = order
        End Sub

        Public ReadOnly Property Order() As Integer
            Get
                Return m_order
            End Get
        End Property
    End Class
End Namespace

