'********************************************************************************************************
'File Name: frmBookmarkManager.vb
'Description: Bookmark Manager.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 

'9/3/2010 - Christian Degrassi: Implemented "Bookmark Manager" Enhancement. Issue 1639
'********************************************************************************************************

Option Compare Text         'So that in text comparisons, ".mwprj" and ".MWPRJ" are equivalent
Imports MapWindow.Interfaces
Imports System.Threading
Imports System.Globalization

Public Class frmBookmarkManager

    Private _IsModified As Boolean
    Private _BookmarkedViews As ArrayList

#Region "Private Methods"

    Private Sub UpdateCheckListBookmarks()

        CheckedListBookmarks.Items.Clear()
        For Each Bookmark As XmlProjectFile.BookmarkedView In _BookmarkedViews
            CheckedListBookmarks.Items.Add(Bookmark)
        Next

    End Sub

    Private Sub ShowSelectedbookmarkExtents()

        If (CheckedListBookmarks.SelectedIndex <> -1) Then
            TextBoxXMin.Text = CType(CheckedListBookmarks.SelectedItem, XmlProjectFile.BookmarkedView).Exts.xMin
            TextBoxXMax.Text = CType(CheckedListBookmarks.SelectedItem, XmlProjectFile.BookmarkedView).Exts.xMax
            TextBoxYMin.Text = CType(CheckedListBookmarks.SelectedItem, XmlProjectFile.BookmarkedView).Exts.yMin
            TextBoxYMax.Text = CType(CheckedListBookmarks.SelectedItem, XmlProjectFile.BookmarkedView).Exts.yMax
        Else
            TextBoxXMin.Text = String.Empty
            TextBoxXMax.Text = String.Empty
            TextBoxYMin.Text = String.Empty
            TextBoxYMax.Text = String.Empty
        End If

    End Sub

    Private Sub EditSelectedBookmark()

        Dim EditDialog As frmBookmarksAddNew = Nothing
        Dim SelectedBookmark As XmlProjectFile.BookmarkedView = Nothing

        If (CheckedListBookmarks.SelectedIndex <> -1) Then

            SelectedBookmark = CheckedListBookmarks.SelectedItem
            EditDialog = New frmBookmarksAddNew(SelectedBookmark.Name, SelectedBookmark.Exts)
            EditDialog.Text = "Edit Bookmark"

            If (EditDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then
                SelectedBookmark.Name = EditDialog.BookmarkName
                SelectedBookmark.Exts = EditDialog.BookmarkExtents
                _IsModified = True

                ShowSelectedbookmarkExtents()
            End If

        End If

    End Sub

    Private Sub DeleteSelectedBookmark()

        If (CheckedListBookmarks.CheckedItems.Count > 0) Then

            If (MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Deleting Bookmarks") = MsgBoxResult.Yes) Then

                '_Bookmarks.RemoveAt(CheckedListBookmarks.SelectedIndex)
                For Each Bookmark As XmlProjectFile.BookmarkedView In CheckedListBookmarks.CheckedItems
                    _BookmarkedViews.Remove(Bookmark)
                Next

            End If

            UpdateCheckListBookmarks()
            ShowSelectedbookmarkExtents()

        Else
            MsgBox("Check at least one Bookmark from the list")
        End If

    End Sub

    Private Sub CheckAll()
        For i As Integer = 0 To CheckedListBookmarks.Items.Count - 1
            CheckedListBookmarks.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub UnCheckAll()
        For i As Integer = 0 To CheckedListBookmarks.Items.Count - 1
            CheckedListBookmarks.SetItemChecked(i, False)
        Next
    End Sub

    Private Function ExportXML(ByVal BookmarkedViews As ArrayList, ByVal Path As String) As Boolean

        Dim BookmarkExporter As BookmarksManager.BookmarkExportXML = Nothing
        Dim Bookmarks As BookmarksManager.Bookmarks = Nothing

        If (BookmarkedViews IsNot Nothing And BookmarkedViews.Count > 0) Then

            Bookmarks = New BookmarksManager.Bookmarks(BookmarkedViews)
            BookmarkExporter = New BookmarksManager.BookmarkExportXML()
            BookmarkExporter.Bookmarks = Bookmarks

            Return BookmarkExporter.Save(Path)

        Else
            Return False
        End If


    End Function

    Private Function ExportCSV(ByVal BookmarkedViews As ArrayList, ByVal Path As String, ByVal HasHeaders As Boolean) As Boolean

        Dim BookmarkExporter As BookmarksManager.BookmarkExportCSV = Nothing
        Dim Bookmarks As BookmarksManager.Bookmarks = Nothing

        If (BookmarkedViews IsNot Nothing And BookmarkedViews.Count > 0) Then

            Bookmarks = New BookmarksManager.Bookmarks(BookmarkedViews)
            BookmarkExporter = New BookmarksManager.BookmarkExportCSV()
            BookmarkExporter.Bookmarks = Bookmarks
            BookmarkExporter.HasHeaders = HasHeaders

            Return BookmarkExporter.Save(Path)

        Else
            Return False
        End If

    End Function

    Private Function ImportXML(ByVal Path As String) As Boolean

        Dim BookmarkImporter As BookmarksManager.BookmarkImportXML = Nothing
        Dim Bookmarks As BookmarksManager.Bookmarks = Nothing
        Dim Result As Boolean = False

        Dim BookmarkedView As XmlProjectFile.BookmarkedView = Nothing

        BookmarkImporter = New BookmarksManager.BookmarkImportXML()
        Result = BookmarkImporter.Open(Path)

        If Result Then

            Bookmarks = BookmarkImporter.Bookmarks

            For Each bkm As BookmarksManager.Bookmark In Bookmarks
                With bkm
                    BookmarkedView = New XmlProjectFile.BookmarkedView(.Name, .Extents.xMin, .Extents.yMin, .Extents.xMax, .Extents.yMax)
                    _BookmarkedViews.Add(BookmarkedView)
                End With
            Next

        End If

        Return Result

    End Function

    Private Function ImportCSV(ByVal Path As String, ByVal HasHeader As Boolean) As Boolean

        Dim BookmarkImporter As BookmarksManager.BookmarkImportCSV = Nothing
        Dim Bookmarks As BookmarksManager.Bookmarks = Nothing
        Dim Result As Boolean = False

        Dim BookmarkedView As XmlProjectFile.BookmarkedView = Nothing

        BookmarkImporter = New BookmarksManager.BookmarkImportCSV()
        BookmarkImporter.HasHeader = HasHeader
        Result = BookmarkImporter.Open(Path)

        If Result Then

            Bookmarks = BookmarkImporter.Bookmarks

            For Each bkm As BookmarksManager.Bookmark In Bookmarks
                With bkm
                    BookmarkedView = New XmlProjectFile.BookmarkedView(.Name, .Extents.xMin, .Extents.yMin, .Extents.xMax, .Extents.yMax)
                    _BookmarkedViews.Add(BookmarkedView)
                End With
            Next

        End If

        Return Result

    End Function


#End Region

    Public ReadOnly Property IsModified()
        Get
            Return _IsModified
        End Get
    End Property

    Public Sub New(ByVal CurrentBookmarks As ArrayList)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _IsModified = False
        _BookmarkedViews = CurrentBookmarks

    End Sub

    Private Sub frmBookmarkManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateCheckListBookmarks()
    End Sub

    Private Sub CheckedListBookmarks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckedListBookmarks.SelectedIndexChanged
        ShowSelectedbookmarkExtents()
    End Sub

    Private Sub ButtonEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        EditSelectedBookmark()
    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        DeleteSelectedBookmark()
    End Sub

    Private Sub ButtonCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCheckAll.Click
        CheckAll()
    End Sub

    Private Sub ButtonUnCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUnCheckAll.Click
        UnCheckAll()
    End Sub

    Private Sub TSMIExportXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMIExportXML.Click

        Dim BookmarksChecked As ArrayList = Nothing
        Dim SFDialog As SaveFileDialog = Nothing

        If (CheckedListBookmarks.CheckedItems.Count > 0) Then

            SFDialog = New SaveFileDialog()

            SFDialog.Filter = "XML Bookmarks|*.xml"

            If (SFDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then

                If (System.IO.File.Exists(SFDialog.FileName)) Then
                    System.IO.File.Delete(SFDialog.FileName)
                End If

                BookmarksChecked = New ArrayList

                For Each bkmv As XmlProjectFile.BookmarkedView In CheckedListBookmarks.CheckedItems
                    BookmarksChecked.Add(bkmv)
                Next

                If (ExportXML(BookmarksChecked, SFDialog.FileName)) Then
                    MessageBox.Show("Export Completed", "Exporting Bookmarks")
                End If

            End If

        Else
            MsgBox("Check at least one Bookmark", MsgBoxStyle.Information, "Exporting Bookmarks")
        End If



    End Sub

    Private Sub TSMIExportCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMIExportCSV.Click

        Dim BookmarksChecked As ArrayList = Nothing
        Dim SFDialog As SaveFileDialog = Nothing

        If (CheckedListBookmarks.CheckedItems.Count > 0) Then

            SFDialog = New SaveFileDialog()

            SFDialog.Filter = "CSV Bookmarks|*.csv"

            If (SFDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then

                If (System.IO.File.Exists(SFDialog.FileName)) Then
                    System.IO.File.Delete(SFDialog.FileName)
                End If

                BookmarksChecked = New ArrayList

                For Each bkmv As XmlProjectFile.BookmarkedView In CheckedListBookmarks.CheckedItems
                    BookmarksChecked.Add(bkmv)
                Next

                If (ExportCSV(BookmarksChecked, SFDialog.FileName, MsgBox("Save Headers?", MsgBoxStyle.YesNo, "CSV Options") = MsgBoxResult.Yes)) Then
                    MessageBox.Show("Export Completed", "Exporting Bookmarks")
                End If

            End If

        Else
            MsgBox("Check at least one Bookmark", MsgBoxStyle.Information, "Exporting Bookmarks")
        End If

    End Sub

    Private Sub TSMIImportXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMIImportXML.Click

        Dim SFDialog As OpenFileDialog = Nothing
        SFDialog = New OpenFileDialog()

        SFDialog.Filter = "XML Bookmarks|*.xml"

        If (SFDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then

            If (ImportXML(SFDialog.FileName)) Then
                UpdateCheckListBookmarks()
                ShowSelectedbookmarkExtents()
                MessageBox.Show("Import Completed", "Importing Bookmarks")
            End If

        End If

    End Sub

    Private Sub TSMIImportCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMIImportCSV.Click

        Dim SFDialog As OpenFileDialog = Nothing
        SFDialog = New OpenFileDialog()

        SFDialog.Filter = "CSV Bookmarks|*.csv"

        If (SFDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then

            If (ImportCSV(SFDialog.FileName, MsgBox("Does the CSV have Headers?", MsgBoxStyle.YesNo, "CSV Options") = MsgBoxResult.Yes)) Then
                UpdateCheckListBookmarks()
                ShowSelectedbookmarkExtents()
                MessageBox.Show("Import Completed", "Importing Bookmarks")
            End If

        End If

    End Sub

End Class