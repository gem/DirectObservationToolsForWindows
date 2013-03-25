Imports System
Imports System.IO
Imports System.Text
Imports System.Linq

Public Class frmGEM2ZIP

    Private counter As Long = 0 ' to ensure and ids as unique

    Private Sub Gem2KML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


    Private Sub KMLBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KMLBrowse.Click
        With SaveFileDialog1
            .FileName = ""
            .Filter = "Zip files (*.zip)|*.zip|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.KMZFilePath.Text = .FileName
            End If
        End With
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click

        Try
            '
            ' Refresh database
            '
            If (Not gemdb Is Nothing) Then Call gemdb.RefreshGEMDataTableContents()
            '
            ' Check input for blanks
            '
            If (Me.GEMDatabase.Text = "" Or Me.KMZFilePath.Text = "") Then Exit Sub
            '
            ' Check GEM database exists
            '
            If (Not IO.File.Exists(Me.GEMDatabase.Text)) Then
                MsgBox("GEM database " & Me.GEMDatabase.Text & " does not exist")
                Exit Sub
            End If
            '
            ' Check Folder exists if not try to create one
            '
            Dim inFolder As String = IO.Path.GetDirectoryName(Me.GEMDatabase.Text)
            Dim outFolder As String = IO.Path.GetDirectoryName(Me.KMZFilePath.Text)
            Dim inDB As String = Me.GEMDatabase.Text
            Dim inPRJ As String = inFolder & "\" & IO.Path.GetFileNameWithoutExtension(inDB) & ".gemprj"
            Dim inMEDIA As String = inFolder & "\" & IO.Path.GetFileNameWithoutExtension(inDB) & "_gemmedia"
            If (Not IO.Directory.Exists(outFolder)) Then
                IO.Directory.CreateDirectory(outFolder)
            End If
            If (Not IO.Directory.Exists(outFolder)) Then
                MessageBox.Show("Cannot find output directory")
                Exit Sub
            End If
            '
            ' Make the Zip file
            '
            'DB, PRJ and MEDIA
            If Not IO.File.Exists(inDB) Then
                MessageBox.Show("Cannot find database")
                Exit Sub
            End If
            If Not IO.File.Exists(inPRJ) Then
                MessageBox.Show("Cannot find project file")
                Exit Sub
            End If
            If Not IO.Directory.Exists(inMEDIA) Then
                MessageBox.Show("Cannot find media folder")
                Exit Sub
            End If

            Dim di As DirectoryInfo = New DirectoryInfo(inMEDIA)
            Using zip As Ionic.Zip.ZipFile = New Ionic.Zip.ZipFile()
                zip.AddFile(inDB, "")
                zip.AddFile(inPRJ, "")
                For Each fi As FileInfo In di.GetFiles("*", SearchOption.AllDirectories)
                    'Get the path right of _gemmedia/
                    Dim fn As String = IO.Path.GetFileName(fi.FullName)
                    Dim shortPath As String = fi.FullName.Substring(fi.FullName.IndexOf(IO.Path.GetDirectoryName(inMEDIA)) + IO.Path.GetDirectoryName(inMEDIA).Length)
                    Dim shortPathWithoutFilename As String = shortPath.Substring(0, shortPath.IndexOf(fn))
                    zip.AddFile(fi.FullName, shortPathWithoutFilename)
                Next
                zip.Save(Me.KMZFilePath.Text)
            End Using

            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Me.Dispose()

    End Sub



End Class
