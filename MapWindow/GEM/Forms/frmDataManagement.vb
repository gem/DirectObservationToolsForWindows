Imports System.IO
Imports System.Data
Imports System.Linq
Public Class frmDataManagement

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

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

        Call gemdb.RefreshGEMDataTableContents()

        Dim f As New Windows.Forms.SaveFileDialog
        f.Filter = "Comma Seperated File (*.csv)|*.csv"
        f.ShowDialog()

        If f.FileName <> "" Then
            Dim s As New StreamWriter(f.FileName)
            Dim colString As String = ""

            'TODO: ask for short or long fieldnames
            Dim useLongNames As Boolean = True
            For Each col As DataColumn In gemdb.Dataset.GEM_OBJECT.Columns
                Dim shortColName As String = col.ColumnName
                Dim fn As GEMDataset.DIC_FIELDSRow = (From row In gemdb.Dataset.DIC_FIELDS Select row Where row.SHORT_FIELD_NAME = shortColName).FirstOrDefault
                Dim longColName As String = shortColName
                Dim colName As String = ""
                If Not fn Is Nothing Then
                    longColName = fn.LONG_FIELD_NAME
                End If
                If useLongNames Then
                    colName = longColName
                Else
                    colName = shortColName
                End If
                If colString = "" Then
                    colString = colName
                Else
                    colString = colString & "," & colName
                End If
            Next
            s.WriteLine(colString)
            For Each row As GEMDataset.GEM_OBJECTRow In gemdb.Dataset.GEM_OBJECT
                Dim rowString As String = ""
                Dim counter As Integer = 0
                For Each col As DataColumn In gemdb.Dataset.GEM_OBJECT.Columns
                    If Not IsDBNull(row.Item(col.ColumnName)) Then
                        If counter = 0 Then
                            rowString = Replace(row.Item(col.ColumnName), ",", "..")
                        Else
                            rowString = rowString & "," & Replace(row.Item(col.ColumnName), ",", "..")
                        End If
                    Else
                        If counter > 0 Then
                            rowString = rowString & ","
                        End If
                    End If
                    counter += 1
                Next
                s.WriteLine(rowString)
            Next
            s.Close()
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub
End Class