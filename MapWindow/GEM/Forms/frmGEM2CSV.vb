Imports SharpKml.Base
Imports SharpKml.Dom
Imports SharpKml.Engine
Imports System
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SQLite

Public Class frmGEM2CSV

    Private Sub frmGEM2CSV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = frmMain.Icon
        If Not gemdb Is Nothing Then
            Me.GEMDatabase.Text = gemdb.DatabasePath
        End If
    End Sub


    Private Sub SourceBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceBrowse.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "GEM database files (*.db3)|*.db3|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.GEMDatabase.Text = .FileName
            End If
        End With
    End Sub


    Private Sub CSVFileBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CSVFileBrowse.Click

        With SaveFileDialog1
            .FileName = ""
            .Filter = "CSV files (*.csv)|*.csv|" & "All files|*.*"

            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.CSVFilename.Text = .FileName
            End If

        End With

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click
        Try
            Call gemdb.RefreshGEMDataTableContents()

            If Me.CSVFilename.Text <> "" Then
                Dim s As New StreamWriter(IO.Path.ChangeExtension(Me.CSVFilename.Text, ".csv"))
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
                                rowString = Replace(Replace(row.Item(col.ColumnName), ",", ".."), vbCrLf, " ")
                            Else
                                rowString = rowString & "," & Replace(Replace(row.Item(col.ColumnName), ",", ".."), vbCrLf, " ")
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


            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Me.Dispose()

    End Sub




End Class
