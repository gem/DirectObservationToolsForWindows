'Added by Chris Michaelis 11/12/2006 to have somewhat better organization for these new functions

Public Class clsUserInteraction
    Implements MapWindow.Interfaces.UserInteraction

    Public Function GetColorRamp(ByVal suggestedStart As System.Drawing.Color, ByVal suggestedEnd As System.Drawing.Color, ByRef selectedStart As System.Drawing.Color, ByRef selectedEnd As System.Drawing.Color) As Boolean Implements Interfaces.UserInteraction.GetColorRamp
        Dim dlg As New ColorPicker(suggestedStart, suggestedEnd)
        selectedStart = suggestedStart
        selectedEnd = suggestedEnd
        If dlg.ShowDialog() = DialogResult.OK Then
            selectedStart = dlg.btnStartColor.BackColor
            selectedEnd = dlg.btnEndColor.BackColor
            Return True
        End If
        Return False
    End Function

    Public Function GetProjectionFromUser(ByVal DialogCaption As String, ByVal DefaultProjection As String) As String Implements Interfaces.UserInteraction.GetProjectionFromUser
        Return frmMain.GetProjectionFromUser(DialogCaption, DefaultProjection)
    End Function
End Class
