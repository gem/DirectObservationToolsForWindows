Imports System.Linq
Imports System.Collections.Generic

Public Class frmPhotoMan

    Private Sub frmPhotoMan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GEMDataset.MEDIA_DETAIL' table. You can move, or remove it, as needed.
        Me.MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
        Me.Icon = frmMain.Icon
    End Sub



    Private Sub ShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAll.CheckedChanged
        If ShowAll.Checked Then
            'Remove filter
            MEDIADETAILBindingSource.Filter = Nothing
        Else
            'Filter media for datagrid
            MEDIADETAILBindingSource.Filter = "FILENAME = '' OR FILENAME IS NULL"
        End If
    End Sub


    Sub OpenPaint(ByVal strFileName As String)
        '
        ' Name: OpenPaint
        ' Purpose: To open MS Paint with an existing file or with a new blank file
        ' Written: K.Adlam, 29/11/12
        '
        '
        ' Define size of image
        '
        Dim width As Integer = 800
        Dim height As Integer = 600
        '
        ' If file does not exist then create a new blank one
        '
        If (Not IO.File.Exists(strFileName)) Then
            Dim bm As New Bitmap(width, height)
            Dim pGraphics As Graphics = Graphics.FromImage(bm)
            pGraphics.FillRectangle(Brushes.White, 0, 0, width, height)
            bm.Save(strFileName, System.Drawing.Imaging.ImageFormat.Jpeg)
        End If
        '
        ' Open file in Paint
        '
        System.Diagnostics.Process.Start("mspaint", strFileName)
        '
    End Sub


    Private Sub dgMedia_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgMedia.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim ht As DataGridView.HitTestInfo
            ht = dgMedia.HitTest(e.X, e.Y)
            If ht.Type = DataGridViewHitTestType.Cell Then
                'dgMedia.ContextMenuStrip = mnuCell
            ElseIf ht.Type = DataGridViewHitTestType.RowHeader Then
                'dgMedia.CurrentCell = dgMedia(0, ht.RowIndex)
                'dgMedia.Rows(ht.RowIndex).Selected = True
                mnuRow.Show(dgMedia, e.Location)
                'dgMedia.ContextMenuStrip = mnuRow
            ElseIf ht.Type = DataGridViewHitTestType.ColumnHeader Then
                'dgMedia.ContextMenuStrip = mnuColumn
            End If
        End If
    End Sub




    Private Sub ShowMediaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMediaToolStripMenuItem.Click
        'SHOW MEDIA
        Dim currentuid As String = getMediaUIDFromSelectedMediaRow()

        Call updateMediaTable()
        Dim currentrow As GEMDataset.MEDIA_DETAILRow = getMediaRowFromUID(currentuid)
        If currentrow Is Nothing Then
            MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not currentrow.IsFILENAMENull AndAlso currentrow.FILENAME <> "" Then
            If IO.File.Exists(gemdb.MediaPath & "\" & currentrow.FILENAME) Then
                If currentrow.MEDIA_TYPE = "SKETCH" Then
                    Call OpenPaint(gemdb.MediaPath & "\" & currentrow.FILENAME)
                Else
                    System.Diagnostics.Process.Start(gemdb.MediaPath & "\" & currentrow.FILENAME)
                End If
            Else
                MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub LinkToMediaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkToMediaToolStripMenuItem.Click
        'ADD MEDIA
        Dim currentuid As String = getMediaUIDFromSelectedMediaRow()
        Call updateMediaTable()
        Dim currentrow As GEMDataset.MEDIA_DETAILRow = getMediaRowFromUID(currentuid)
        If currentrow Is Nothing Then
            MessageBox.Show("Please create a record before adding media", "Add record first", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If currentrow.MEDIA_TYPE = "SKETCH" Then
            Call OpenPaint(gemdb.MediaPath & "\" & currentrow.MEDIA_UID & ".jpg")
            currentrow.ORIG_FILEN = gemdb.MediaPath & "\" & currentrow.MEDIA_UID & ".jpg"
            currentrow.FILENAME = currentrow.MEDIA_UID & ".jpg"
            MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
        Else
            With New OpenFileDialog
                .FileName = ""
                .Filter = "All files|*.*"
                If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                    Dim newFilepath As String = gemdb.MediaPath & "\" & currentrow.MEDIA_UID & IO.Path.GetExtension(.FileName)
                    Dim newFilepathShort As String = currentrow.MEDIA_UID & IO.Path.GetExtension(.FileName)
                    If IO.File.Exists(newFilepath) Then
                        If MessageBox.Show("The current record already has a media file associated with it, do you want to overwrite it?", "Overwrite file - " & currentrow.MEDIA_UID & "." & IO.Path.GetExtension(.FileName), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then
                            Exit Sub
                        End If
                    End If

                    IO.File.Copy(.FileName, newFilepath, True)
                    currentrow.ORIG_FILEN = .FileName
                    currentrow.FILENAME = newFilepathShort
                    MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
                End If
            End With
        End If
    End Sub

    Private Function getMediaRowFromUID(ByVal mediauid As String) As GEMDataset.MEDIA_DETAILRow
        Return (From row In Me.GEMDataset.MEDIA_DETAIL Where row.MEDIA_UID = mediauid Select row).FirstOrDefault
    End Function

    Private Function getMediaUIDFromSelectedMediaRow() As String
        Return dgMedia.CurrentRow.Cells.Item(1).Value.ToString
    End Function

    Private Sub updateMediaTable()

        Me.Validate()
        dgMedia.EndEdit()
        dgMedia.Update()
        MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
        MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
    End Sub







    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With New FolderBrowserDialog
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If IO.Directory.Exists(.SelectedPath) Then
                    Dim wordList As List(Of String) = IO.Directory.GetFiles(.SelectedPath).ToList()
                    Dim fuz As New FuzzySearch
                    For Each rec As GEMDataset.MEDIA_DETAILRow In Me.GEMDataset.MEDIA_DETAIL
                        If rec.IsFILENAMENull Then
                            If Not rec.IsMEDIA_NUMBNull Then
                                Dim matchedimage As String = fuz.SearchImages(rec.MEDIA_NUMB, wordList)
                                If matchedimage <> "" Then
                                    Dim newFilepath As String = gemdb.MediaPath & "\" & rec.MEDIA_UID & IO.Path.GetExtension(matchedimage)
                                    Dim newFilepathShort As String = rec.MEDIA_UID & IO.Path.GetExtension(matchedimage)
                                    rec.ORIG_FILEN = matchedimage
                                    rec.FILENAME = newFilepathShort
                                    IO.File.Copy(matchedimage, newFilepath, True)
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        End With

        updateMediaTable()

    End Sub


End Class