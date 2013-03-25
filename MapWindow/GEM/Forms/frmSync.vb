Imports System.IO
Imports System.Data
Imports System.Data.SQLite

Public Class frmSync

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click
        Try
            '
            ' Name: Ok_Click
            ' Purpose: To Merge multiple GEM Projects
            ' Written: K.Adlam, 2013
            '
            '
            ' Checks
            '
            If Me.TargetDatabase.Text = "" Then Exit Sub
            '
            ' Check Folder exists
            '
            Dim dirPath As String = IO.Path.GetDirectoryName(Me.TargetDatabase.Text)
            If (Not Directory.Exists(dirPath)) Then
                IO.Directory.CreateDirectory(dirPath)
            End If
            '
            ' Check Target database has correct extension
            '
            If (IO.Path.GetExtension(Me.TargetDatabase.Text) <> ".db3") Then
                Me.TargetDatabase.Text = IO.Path.ChangeExtension(Me.TargetDatabase.Text, ".db3")
            End If
            '
            ' Check target database does not exist
            '
            If (IO.File.Exists(Me.TargetDatabase.Text)) Then
                MsgBox("Target database already exists")
                Exit Sub
            End If
            '
            ' Check we have at least two databases
            '
            If (Me.SourceDatabases.CheckedItems.Count < 2) Then
                MsgBox("At least two databases must be selected to merge")
                Exit Sub
            End If
            '
            ' Copy Database matching the project if it is checked otherwise use the first database
            ' in the Checked items list
            '
            Dim sourceDatabase As String = IO.Path.ChangeExtension(Me.SourceProject.Text, "db3")

            If (Not Me.SourceDatabases.CheckedItems.Contains(sourceDatabase)) Then
                sourceDatabase = Me.SourceDatabases.CheckedItems.Item(0)
            End If
            IO.File.Copy(sourceDatabase, Me.TargetDatabase.Text)
            '
            ' Append the other databases
            '
            For i As Integer = 1 To Me.SourceDatabases.CheckedItems.Count - 1
                If (Me.SourceDatabases.CheckedItems.Item(i) <> sourceDatabase) Then
                    Call SyncDatabase(Me.SourceDatabases.CheckedItems.Item(i), Me.TargetDatabase.Text)
                End If
            Next
            '
            ' Copy Project file
            '
            IO.File.Copy(Me.SourceProject.Text, IO.Path.ChangeExtension(Me.TargetDatabase.Text, ".gemprj"))
            '
            ' Create media Folder
            '
            Dim strFile As String = IO.Path.GetFileNameWithoutExtension(Me.TargetDatabase.Text) & "_gemmedia"
            Dim mediaDir As String = IO.Path.Combine(dirPath, strFile)
            If (Not Directory.Exists(mediaDir)) Then
                IO.Directory.CreateDirectory(mediaDir)
            End If
            '
            ' Copy media files from separate Gem media folders into the target media folder
            '
            For i As Integer = 0 To Me.SourceDatabases.CheckedItems.Count - 1
                Call CopyMediaFiles(Me.SourceDatabases.CheckedItems.Item(i), mediaDir)
            Next
            '
            ' Completion message
            '
            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Dispose()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


    Private Sub CopyMediaFiles(ByVal strSourceDatabase As String, ByVal strTargetFolder As String)
        '
        ' Name: CopyMediaFiles
        ' Purpose: To copy media files from separate projects to target media folder
        ' Written: K.Adlam, 26/2/2013
        '
        '
        ' Get media folder for source database
        '
        Dim dirPath As String = IO.Path.GetDirectoryName(strSourceDatabase)
        Dim strFolder As String = IO.Path.GetFileNameWithoutExtension(strSourceDatabase) & "_gemmedia"
        Dim mediaDir As String = IO.Path.Combine(dirPath, strFolder)
        If (Not IO.Directory.Exists(mediaDir)) Then
            MsgBox("WARNING: Media folder " & mediaDir & " does not exist")
            Exit Sub
        End If
        '
        ' Copy files from source to target
        '
        Dim di As DirectoryInfo = New DirectoryInfo(mediaDir)

        For Each fi As FileInfo In di.GetFiles
            Dim strFile As String = IO.Path.GetFileName(fi.FullName)
            Dim targetFile As String = IO.Path.Combine(strTargetFolder, strFile)
            If (Not IO.File.Exists(targetFile)) Then
                IO.File.Copy(fi.FullName, targetFile)
            End If
        Next

    End Sub

    Private Sub SelectFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectFiles.Click
        '
        ' Name: SelectFiles_Click
        ' Purpose: To allow the user to select GEM database from the file system and add them to
        '          the source database list
        ' Written: K.Adlam, 2013
        '
        Dim openFileDialog1 As New OpenFileDialog

        With openFileDialog1

            .Filter = "gemdb files (*.gemdb)|*.gemdb|All files (*.*)|*.*"
            .FilterIndex = 1
            .RestoreDirectory = True
            .Multiselect = True

            If .ShowDialog() = DialogResult.OK Then
                '
                ' Add selected file paths to Checked list box 
                ' and associated project files to Combobox
                '
                For Each fName As String In openFileDialog1.FileNames
                    Me.SourceDatabases.Items.Add(fName)
                    If (IO.File.Exists(IO.Path.ChangeExtension(fName, ".gemprj"))) Then
                        Me.SourceProject.Items.Add(IO.Path.ChangeExtension(fName, ".gemprj"))
                    End If
                Next
                '
                ' Default Source Project
                '
                If (Me.SourceProject.Items.Count > 0) Then
                    Me.SourceProject.Text = Me.SourceProject.Items.Item(0)
                End If
                '
                ' Check all items by default
                '
                For i As Integer = 0 To SourceDatabases.Items.Count - 1
                    SourceDatabases.SetItemChecked(i, True)
                Next
            End If

        End With

    End Sub

    Private Sub SelectFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectFolder.Click
        '
        ' Name: SelectFolder_Click
        ' Purpose: To allow the user to select a folder containing GEM databases and add them to
        '           the source database list
        ' Written: K.Adlam, 2013
        '
        Dim MyFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        MyFolderBrowser.Description = "Select the folder containg GEM databases to be merged"
        MyFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer
        If MyFolderBrowser.ShowDialog() = Windows.Forms.DialogResult.OK Then

            Dim diSource As DirectoryInfo = New DirectoryInfo(MyFolderBrowser.SelectedPath)
            '
            ' Add selected file paths to Checked list box 
            ' and associated project files to Combobox
            '
            For Each fi As FileInfo In diSource.GetFiles("*.gemdb", SearchOption.TopDirectoryOnly)
                Me.SourceDatabases.Items.Add(fi.FullName)
                If (IO.File.Exists(IO.Path.ChangeExtension(fi.FullName, ".gemprj"))) Then
                    Me.SourceProject.Items.Add(IO.Path.ChangeExtension(fi.FullName, ".gemprj"))
                End If
            Next
            '
            ' Default Source Project
            '
            If (Me.SourceProject.Items.Count > 0) Then
                Me.SourceProject.Text = Me.SourceProject.Items.Item(0)
            End If
            '
            ' Check all items by default
            '
            For i As Integer = 0 To SourceDatabases.Items.Count - 1
                SourceDatabases.SetItemChecked(i, True)
            Next

        End If

    End Sub

    Private Sub ClearList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearList.Click
        Me.SourceDatabases.Items.Clear()
        Me.SourceProject.Items.Clear()
    End Sub


    Private Sub TargetBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TargetBrowse.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        With SaveFileDialog1
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
        '
        ' Name: Sync
        ' Purpose: To synchronize specified tables
        ' Written: K.Adlam, 2013
        '
        Call SyncTable(SQLcommand, "GEM_PROJECT", "PROJ_UID")
        Call SyncTable(SQLcommand, "GEM_OBJECT", "OBJ_UID")
        Call SyncTable(SQLcommand, "MEDIA_DETAIL", "MEDIA_UID")
        Call SyncTable(SQLcommand, "GED", "GED_UID")
        Call SyncTable(SQLcommand, "CONSEQUENCES", "CONSEQ_UID")
    End Sub

    Sub SyncTable(ByVal SQLcommand As SQLiteCommand, ByVal strTable As String, ByVal strKeyField As String)
        '
        ' Name: SyncTable
        ' Purpose: To synchronize a table
        ' Written: K.Adlam, 2013
        '
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

End Class