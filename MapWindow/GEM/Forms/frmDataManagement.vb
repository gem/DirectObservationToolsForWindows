Imports System.IO
Imports System.Data
Imports System.Linq
Public Class frmDataManagement

    Private Sub frmDataManagement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = frmMain.Icon
    End Sub

    Private Sub SyncDatabases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncDatabases.Click
        Try
            Dim pForm As New frmSync
            pForm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ExportToKML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToKML.Click
        Dim pForm As New frmGEM2KML
        pForm.ShowDialog()
    End Sub

    Private Sub ExportToShapefile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToShapefile.Click
        Dim pForm As New frmGEM2SHP
        pForm.ShowDialog()
    End Sub

    Private Sub ExportToCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToCSV.Click
        Dim pForm As New frmGEM2CSV
        pForm.ShowDialog()
    End Sub

 
    Private Sub btnZip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZip.Click
        Dim pForm As New frmGEM2ZIP
        pForm.ShowDialog()
    End Sub
End Class