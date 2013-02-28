Imports System.IO
Imports System.Data
Imports System.Data.SQLite

Public Class frmSync_Old

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click
        Try
            If Me.SourceDatabase.Text = "" Or Me.TargetDatabase.Text = "" Then Exit Sub
            Call SyncDatabase(Me.SourceDatabase.Text, Me.TargetDatabase.Text)
            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try



    End Sub

    Private Sub SourceBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceBrowse.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "GEM database files (*.gemdb)|*.gemdb|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.SourceDatabase.Text = .FileName
            End If
        End With
    End Sub

    Private Sub TargetBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TargetBrowse.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "Gem database files (*.gemdb)|*.gemdb|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.TargetDatabase.Text = .FileName
            End If
        End With
    End Sub

    Sub SyncDatabase(ByVal strSourceDatabase As String, ByVal strTargetDatabase As String)
        '
        ' Name: SyncDatabase
        ' Purpose: to update the target database with new or updated rows from the source database.
        ' Written: K.Adlam, 28/11/12
        '
        ' Check source and target databases exist
        '
        If (Not IO.File.Exists(strSourceDatabase)) Then
            MsgBox("Source database " & strSourceDatabase & " does not exist")
            Exit Sub
        End If
        '
        If (Not IO.File.Exists(strTargetDatabase)) Then
            MsgBox("Target database " & strTargetDatabase & " does not exist")
            Exit Sub
        End If
        '
        ' Connect to source database
        '
        Dim SQLconnect As New SQLite.SQLiteConnection()
        SQLconnect.ConnectionString = "Data Source=" & strTargetDatabase & ";"
        SQLconnect.Open()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        '
        ' Attach Target database
        '
        SQLcommand.CommandText = "ATTACH '" & strSourceDatabase & "' AS SOURCE;"
        SQLcommand.ExecuteNonQuery()
        ''
        '' Set foreign keys off
        ''
        'SQLcommand.CommandText = "PRAGMA foreign_keys = OFF"
        'SQLcommand.ExecuteNonQuery()
        '
        ' Start Transaction
        '
        SQLcommand.CommandText = "BEGIN TRANSACTION;"
        SQLcommand.ExecuteNonQuery()
        '
        ' Sync the database
        '
        Call Sync(SQLcommand)
        '
        ' Commit Transaction
        '
        SQLcommand.CommandText = "COMMIT;"
        SQLcommand.ExecuteNonQuery()
        '
        ' Detach Target database
        '
        SQLcommand.CommandText = "DETACH SOURCE"
        SQLcommand.ExecuteNonQuery()
        '
        ' Close database connection
        '
        SQLcommand.Dispose()
        SQLconnect.Close()

    End Sub

    Sub Sync(ByVal SQLcommand As SQLiteCommand)
        Call SyncTable(SQLcommand, "GEM_PROJECT", "PROJ_UID")
        Call SyncTable(SQLcommand, "GEM_OBJECT", "OBJ_UID")
        Call SyncTable(SQLcommand, "MEDIA_DETAIL", "MEDIA_UID")
        Call SyncTable(SQLcommand, "GED", "GED_UID")
        Call SyncTable(SQLcommand, "CONSEQUENCES", "CONSEQ_UID")
    End Sub

    Sub SyncTable(ByVal SQLcommand As SQLiteCommand, ByVal strTable As String, ByVal strKeyField As String)

        Dim strSql As String = _
        "DELETE FROM MAIN." & strTable & " WHERE MAIN." & strTable & "." & strKeyField & _
        " IN (SELECT SOURCE." & strTable & "." & strKeyField & " FROM SOURCE." & strTable & _
        " WHERE MAIN." & strTable & "." & strKeyField & " = SOURCE." & strTable & "." & strKeyField & _
        " AND ((SOURCE." & strTable & "." & " DATE_MADE > MAIN." & strTable & ".DATE_MADE) OR " & _
        " (MAIN." & strTable & ".DATE_MADE IS NULL AND SOURCE." & strTable & ".DATE_MADE IS NOT NULL)));"

        SQLcommand.CommandText = strSql
        SQLcommand.ExecuteNonQuery()

        strSql = _
        "INSERT INTO MAIN." & strTable & " SELECT * FROM SOURCE." & strTable & _
        " WHERE SOURCE." & strTable & "." & strKeyField & _
        " NOT IN (SELECT MAIN." & strTable & "." & strKeyField & " FROM MAIN." & strTable & ");"

        SQLcommand.CommandText = strSql
        SQLcommand.ExecuteNonQuery()

    End Sub


    Private Sub frmSync_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = frmMain.Icon
    End Sub
End Class