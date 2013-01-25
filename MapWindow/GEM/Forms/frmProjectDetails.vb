Imports System.Data.SQLite
Imports System.Collections.Generic
Imports System.Linq
Imports MapWindow

Public Class frmProjectDetails

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = frmMain.Icon
    End Sub

    Private Sub frmProjectDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If gemdb Is Nothing Then
            Throw New Exception("Unable to load database")
        End If
        If gemdb.Dataset Is Nothing Then
            Throw New Exception("Unable to load database")
        End If

        yourName.Text = gemdb.getFirstUserFromSettingsTable
        If Not gemdb.getFirstProjectRecord Is Nothing Then
            surveyTitle.Text = gemdb.getFirstProjectRecord.PROJ_NAME
            surveySummary.Text = gemdb.getFirstProjectRecord.PROJ_SUMRY
            projectDate.Value = gemdb.getFirstProjectRecord.PROJ_DATE
        End If

    End Sub



    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click

        If yourName.Text = "" Or surveyTitle.Text = "" Or surveySummary.Text = "" Then
            MessageBox.Show("Please enter all the required information")
            Exit Sub
        End If

        'Update User Information
        If (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "CURRENT_USER" Select setting).Count > 0 Then
            'Update user setting
            Dim currentUser As GEMDataset.SETTINGSRow = (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "CURRENT_USER" Select setting).First
            currentUser.VALUE = yourName.Text
        Else
            'Insert user setting
            Call gemdb.addUser(yourName.Text)
        End If

        'Update Project information
        Call gemdb.addProject(surveyTitle.Text, surveySummary.Text, projectDate.Value)
        '
        ' Close dialog
        '
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()


    End Sub


End Class