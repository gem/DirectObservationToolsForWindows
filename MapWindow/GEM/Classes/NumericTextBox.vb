Public Class NumericTextBox
    Inherits TextBox

    'blocks typing non numerics but allows backspace tab etc
    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)

        MyBase.OnKeyPress(e)
        If (Char.IsControl(e.KeyChar) Or Char.IsDigit(e.KeyChar)) Then
                Exit Sub
        Else
            e.Handled = True
        End If

    End Sub

    'Stops copy and pasting etc
    Private Sub NumericTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        Dim tb As TextBox = CType(sender, TextBox)
        If IsNumeric(tb.Text) Then
            Exit Sub
        Else
            tb.Text = ""
        End If
    End Sub

End Class

