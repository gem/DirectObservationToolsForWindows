Public Class frmChooseDisplayUnits

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub frmChooseDisplayUnits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        list.Items.Add("Meters")
        list.Items.Add("Centimeters")
        list.Items.Add("Feet")
        list.Items.Add("Inches")
        list.Items.Add("Kilometers")
        list.Items.Add("Miles")
        list.Items.Add("Millimeters")
        list.Items.Add("NauticalMiles")
        list.Items.Add("Yards")

        Dim StartWith As String = frmMain.Project.MapUnits

        If Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "" Then
            StartWith = modMain.ProjInfo.ShowStatusBarCoords_Alternate
        End If

        If Not frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit = "" Then
            StartWith = frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit
        End If

        For i As Integer = 0 To list.Items.Count - 1
            If list.Items(i).ToString().ToLower() = StartWith.ToLower() Then
                list.SelectedIndex = i
                Exit For
            End If
        Next

        If list.SelectedIndex = -1 Then list.SelectedIndex = 1 'Meters
    End Sub
End Class