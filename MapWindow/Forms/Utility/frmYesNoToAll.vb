Public Class frmYesNoToAll
    Public Shadows Enum DialogResult
        Yes
        No
        YesToAll
        NoToAll
        Undefined
    End Enum

    Private Result As DialogResult = DialogResult.Undefined

    Public Shared Function ShowPrompt(ByVal Message As String, ByVal Title As String) As DialogResult
        Dim f As New frmYesNoToAll
        f.Label1.Text = Message
        f.Text = Title
        f.ShowDialog()
        Return f.Result
    End Function

    Private Sub frmYesNoToAll_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Result = DialogResult.Undefined Then e.Cancel = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Result = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Result = DialogResult.No
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Result = DialogResult.YesToAll
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Result = DialogResult.NoToAll
        Me.Close()
    End Sub
End Class