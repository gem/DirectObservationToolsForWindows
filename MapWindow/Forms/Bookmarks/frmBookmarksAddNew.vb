'********************************************************************************************************
'File Name: frmBookmarksAddNew.vb
'Description: Add a New Bookmark or Edit Existing Bookmark. Dialog.
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
'9/3/2010 Christian Degrassi: Created this form as Enhancement for the MapWindow Bookmark Manager. enhancement 1638

Option Compare Text         'So that in text comparisons, ".mwprj" and ".MWPRJ" are equivalent
Imports MapWindow.Interfaces
Imports System.Threading
Imports System.Globalization

Public Class frmBookmarksAddNew

    Dim _BookmarkExtents As MapWinGIS.Extents
    Dim _BookmarkName As String

#Region "Private Methods"

    Private Sub DisplayExtentsValues()
        If (_BookmarkExtents IsNot Nothing) Then
            TextBoxXMin.Text = _BookmarkExtents.xMin.ToString()
            TextBoxXMax.Text = _BookmarkExtents.xMax.ToString()
            TextBoxYMin.Text = _BookmarkExtents.yMin.ToString()
            TextBoxYMax.Text = _BookmarkExtents.yMax.ToString()
        Else
            TextBoxXMin.Text = "-1"
            TextBoxXMax.Text = "1"
            TextBoxYMin.Text = "-1"
            TextBoxYMax.Text = "1"
        End If
    End Sub

    Private Sub DisplayNameValue()
        TextBoxName.Text = _BookmarkName
    End Sub

    Private Sub UpdateExtents()

        Dim xMin As Double
        Dim xMax As Double
        Dim yMin As Double
        Dim yMax As Double
        Dim zMin As Double
        Dim zMax As Double

        Dim Result As Boolean = False

        Result = Double.TryParse(TextBoxXMin.Text, xMin) And Double.TryParse(TextBoxXMax.Text, xMax)
        Result = Result And (Double.TryParse(TextBoxYMin.Text, yMin) And Double.TryParse(TextBoxYMax.Text, yMax))

        If (Result) Then
            If (_BookmarkExtents IsNot Nothing) Then
                zMin = _BookmarkExtents.zMin
                zMax = _BookmarkExtents.zMax
            Else
                zMin = 0
                zMax = 0
            End If

            _BookmarkExtents = New MapWinGIS.Extents
            _BookmarkExtents.SetBounds(xMin, yMin, zMin, xMax, yMax, zMax)
        End If

    End Sub

    Private Sub UpdateName()
        _BookmarkName = TextBoxName.Text
    End Sub

    Private Sub UpdateEntries()
        UpdateExtents()
        UpdateName()
    End Sub

    Private Function IsValidExtent() As Boolean

        Dim xMin As Double
        Dim xMax As Double
        Dim yMin As Double
        Dim yMax As Double

        Dim Result As Boolean = False

        Result = Double.TryParse(TextBoxXMin.Text, xMin) And Double.TryParse(TextBoxXMax.Text, xMax)
        Result = Result And (Double.TryParse(TextBoxYMin.Text, yMin) And Double.TryParse(TextBoxYMax.Text, yMax))

        Return Result

    End Function

    Private Function IsValidName() As Boolean
        Return (Not TextBoxName.Text = String.Empty)
    End Function

    Private Function HasValidEntries() As Boolean
        Return (IsValidExtent() And IsValidName())
    End Function

#End Region

    Public ReadOnly Property BookmarkExtents() As MapWinGIS.Extents
        Get
            Return _BookmarkExtents
        End Get
    End Property

    Public ReadOnly Property BookmarkName() As String
        Get
            Return _BookmarkName
        End Get
    End Property

    Public Sub New(ByVal NewName As String, ByVal NewExtents As MapWinGIS.Extents)

        InitializeComponent()

        If (NewExtents IsNot Nothing) Then
            _BookmarkExtents = New MapWinGIS.Extents
            _BookmarkExtents.SetBounds(NewExtents.xMin, NewExtents.yMin, NewExtents.zMin, NewExtents.xMax, NewExtents.yMax, NewExtents.zMax)
        End If

        _BookmarkName = NewName

    End Sub

    Private Sub frmBookmarksAddNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisplayExtentsValues()
        DisplayNameValue()

    End Sub

    Private Sub TextBoxName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxName.TextChanged
        ButtonOK.Enabled = HasValidEntries()

    End Sub

    Private Sub TextBoxYMax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxYMax.TextChanged
        ButtonOK.Enabled = HasValidEntries()
    End Sub

    Private Sub TextBoxXMin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxXMin.TextChanged
        ButtonOK.Enabled = HasValidEntries()
    End Sub

    Private Sub TextBoxXMax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxXMax.TextChanged
        ButtonOK.Enabled = HasValidEntries()
    End Sub

    Private Sub TextBoxYMin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxYMin.TextChanged
        ButtonOK.Enabled = HasValidEntries()
    End Sub

    Private Sub ButtonRevert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRevert.Click
        DisplayExtentsValues()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        UpdateEntries()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class