Public Class frmErrorDialogMoreInfo

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtFullText.Text)
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Me.Close()
    End Sub

    Private Sub frmErrorDialogMoreInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = Owner.Top
        Me.Left = Owner.Left
        txtFullText.SelectionLength = 0
    End Sub

    Private Sub txtFullText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFullText.KeyDown
        e.SuppressKeyPress = True
    End Sub
End Class