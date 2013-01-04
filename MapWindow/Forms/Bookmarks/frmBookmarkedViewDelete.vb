Public Class frmBookmarkedViewDelete

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmBookmarkedViewDelete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateList()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        For i As Integer = ListBox1.SelectedIndices.Count - 1 To 0 Step -1
            ProjInfo.BookmarkedViews.RemoveAt(ListBox1.SelectedIndices(i))
            'Do this in the loop, in case nothing was selected for deletion
            modMain.frmMain.SetModified(True)
        Next
        UpdateList()
    End Sub

    Private Sub UpdateList()
        ListBox1.Items.Clear()
        For i As Integer = 0 To ProjInfo.BookmarkedViews.Count - 1
            Dim b As XmlProjectFile.BookmarkedView = ProjInfo.BookmarkedViews(i)

            'ListBox1.Items.Add(b.Name + " (" & b.Exts.xMin.ToString() + ", " + b.Exts.xMax.ToString() + "), (" + b.Exts.yMin.ToString() + ", " + b.Exts.yMax.ToString() + ")")

            'Christian Degrassi 2010-03-09: This fixes part of Enhancement 1639
            ListBox1.Items.Add(b)
        Next
    End Sub
End Class