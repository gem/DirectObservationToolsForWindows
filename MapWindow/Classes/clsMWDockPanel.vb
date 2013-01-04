<CLSCompliant(False)> _
Public Class clsMWDockPanel
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Private m_Name As String

    Public Sub New(ByVal Name As String)
        MyBase.New()

        m_Name = Name
        Me.Text = Name
    End Sub

    Protected Overrides Function GetPersistString() As String
        Return "mwDockPanel_" & m_Name
    End Function
End Class
