Public Class frmSetScale

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtNewScale.Text = ""
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub

    Public Sub New(ByVal CurrentScale As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
        'Me.Icon = My.Resources.MapWindow_new
        ' Paul Meems, The above does not seem to work.
        ' This does:
        Me.Icon = frmMain.Icon

        ' Paul Meems, June 1, 2010
        ' Changed the appearance
        Try
            Dim d As Double = Math.Round(Double.Parse(CurrentScale, System.Globalization.CultureInfo.InvariantCulture))
            CurrentScale = d.ToString(System.Globalization.CultureInfo.InvariantCulture)
            txtOldScale.Text = CurrentScale
            txtNewScale.Text = CurrentScale

            ' Added combobox, issue #933:
            cboPredefinedScales.Items.Clear()
            Dim defaultValue As String = "[Custom]"
            cboPredefinedScales.Items.Add(defaultValue)

            ' First add some low custom scales:
            cboPredefinedScales.Items.Add("100")
            cboPredefinedScales.Items.Add("250")
            cboPredefinedScales.Items.Add("500")
            cboPredefinedScales.Items.Add("1000")
            cboPredefinedScales.Items.Add("1500")
            ' Paul Meems - August 17 2010, use more or less the same scales as OSM uses for their zoom levels:
            ' From http://wiki.openstreetmap.org/wiki/FAQ#What_is_the_map_scale_for_a_particular_zoom_level_of_the_map.3F
            Dim scale As Integer = 2250
            For i As Integer = 0 To 18
                If i > 0 Then scale = scale * 2
                cboPredefinedScales.Items.Add(scale.ToString)
                If CurrentScale = scale.ToString() Then defaultValue = scale.ToString()
            Next
            cboPredefinedScales.SelectedItem = defaultValue

        Catch
        End Try
    End Sub

    Private Sub txtNewScale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNewScale.TextChanged
        cboPredefinedScales.SelectedItem = "[Custom]"
    End Sub
End Class