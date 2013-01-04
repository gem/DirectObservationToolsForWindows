Imports System.Configuration

Namespace Classes

    <SettingsProvider("System.Configuration.LocalFileSettingsProvider")> _
    <SettingsGroupName("System.Windows.Forms.ToolStripSettings.MapWindow.MapWindowForm")> _
    Class ToolStripSettings
        Inherits ApplicationSettingsBase

        Public Sub New(ByVal settingsKey As String)
            MyBase.New(settingsKey)
        End Sub

        <UserScopedSetting()> _
        Public Property Location() As Point
            Get
                If Me("Location") Is Nothing Then
                    If Me.GetPreviousVersion("Location") Is Nothing Then
                        Return New Point(-1, -1)
                    End If

                    Return DirectCast(Me.GetPreviousVersion("Location"), Point)
                End If

                Return DirectCast(Me("Location"), Point)
            End Get
            Set(ByVal value As Point)
                Me("Location") = value
            End Set
        End Property

        <UserScopedSetting()> _
        Public Property ToolStripPanelName() As String
            Get
                If String.IsNullOrEmpty(DirectCast(Me("ToolStripPanelName"), String)) Then
                    ' Try previous version of settings file:
                    If String.IsNullOrEmpty(DirectCast(Me.GetPreviousVersion("ToolStripPanelName"), String)) Then
                        ' Default value:
                        Return String.Empty
                    End If

                    Return DirectCast(Me.GetPreviousVersion("ToolStripPanelName"), String)
                End If

                Return DirectCast(Me("ToolStripPanelName"), String)
            End Get
            Set(ByVal value As String)
                Me("ToolStripPanelName") = value
            End Set
        End Property

        <UserScopedSetting()> _
        Public Property DisplayStyle() As String
            Get
                Const defaultValue As String = "ImageAndText"
                If Me("DisplayStyle") Is Nothing OrElse DirectCast(Me("DisplayStyle"), String) = String.Empty Then
                    ' Try previous version of settings file:
                    If Me.GetPreviousVersion("DisplayStyle") Is Nothing OrElse DirectCast(Me.GetPreviousVersion("DisplayStyle"), String) = String.Empty Then
                        ' Default value
                        Return defaultValue
                    End If

                    Return DirectCast(Me.GetPreviousVersion("DisplayStyle"), String)
                End If

                Return DirectCast(Me("DisplayStyle"), String)
            End Get
            Set(ByVal value As String)
                Me("DisplayStyle") = value
            End Set
        End Property
    End Class
End Namespace