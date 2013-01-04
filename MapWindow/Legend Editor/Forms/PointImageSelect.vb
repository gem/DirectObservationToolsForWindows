'Multiple icons on a single point layer
'Added by Chris Michaelis 11/12/2006

Public Class PointImageSelect
    Public newidx As Integer = -1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        Dim fod As New OpenFileDialog
        ' Paul Meems - 20 August 2009
        ' Added png as filter
        fod.Filter = "All Image Formats|*.ico;*.bmp;*.gif;*.jpg;*.png|Icons (*.ico)|*.ico|Bitmaps (*.bmp)|*.bmp|GIFs (*.gif)|*.gif|JPeg (*.jpg)|*.jpg|PNG (*.png)|*.png"
        If fod.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso Not fod.FileName = "" Then
            If fod.FileName.ToLower().EndsWith(".ico") Then
                Dim NetImage As New System.Drawing.Icon(fod.FileName)

                Dim imgUtil As New MapWinUtility.ImageUtils
                Dim ico As New MapWinGIS.Image
                If Not ico Is Nothing Then ico.Picture = imgUtil.ImageToIPictureDisp(CType(NetImage.ToBitmap(), System.Drawing.Image))

                'Try to find transparency color from the first pixel rather than assuming 0,0,211
                ico.TransparencyColor = ico.Value(0, 0)
                'Default 0, 0, 211 --> ico.TransparencyColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 0, 211)))

                newidx = frmMain.MapMain.set_UDPointImageListAdd(frmMain.Layers.CurrentLayer, ico)
                ico = Nothing 'Prevent GC on this object
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Hide()
            Else
                Dim NetImage As New System.Drawing.Bitmap(fod.FileName)

                Dim imgUtil As New MapWinUtility.ImageUtils
                Dim ico As New MapWinGIS.Image
                If Not ico Is Nothing Then ico.Picture = imgUtil.ImageToIPictureDisp(CType(NetImage, System.Drawing.Image))

                'Try to find transparency color from the first pixel rather than assuming 0,0,211
                ico.TransparencyColor = ico.Value(0, 0)
                'Default 0, 0, 211 --> ico.TransparencyColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 0, 211)))

                newidx = frmMain.MapMain.set_UDPointImageListAdd(frmMain.Layers.CurrentLayer, ico)
                ico = Nothing 'Prevent GC on this object
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub PopulateImageCombo()
        flp1.Controls.Clear()
        GC.Collect()

        For i As Integer = 0 To frmMain.MapMain.get_UDPointImageListCount(frmMain.Layers.CurrentLayer) - 1
            Dim g As MapWinGIS.Image
            Dim imgcvter As New MapWinUtility.ImageUtils

            g = frmMain.MapMain.get_UDPointImageListItem(frmMain.Layers.CurrentLayer, i)

            Dim img As System.Drawing.Image = imgcvter.IPictureDispToImage(g.Picture)
            If Not img Is Nothing Then
                Dim pb As New PictureBox
                pb.Image = img
                pb.SizeMode = PictureBoxSizeMode.AutoSize
                pb.Tag = i
                AddHandler pb.Click, AddressOf IconClicked
                flp1.Controls.Add(pb)
            End If
            img = Nothing 'prevent GC
        Next
    End Sub

    Private Sub IconClicked(ByVal sender As Object, ByVal e As EventArgs)
        newidx = CType(sender, PictureBox).Tag
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub PointImageSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '20 August 2009 Paul Meems - Load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new
        PopulateImageCombo()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub chbHidePoint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbHidePoint.CheckedChanged
        flp1.Enabled = Not chbHidePoint.Checked
        If chbHidePoint.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Hide()
        End If
    End Sub
End Class