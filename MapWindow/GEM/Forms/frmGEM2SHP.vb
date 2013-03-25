Imports SharpKml.Base
Imports SharpKml.Dom
Imports SharpKml.Engine
Imports System
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SQLite
Imports MapWinGIS

Public Class frmGEM2SHP

    Private Sub Gem2KML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmMain.Icon
        If Not gemdb Is Nothing Then
            Me.GEMDatabase.Text = gemdb.DatabasePath
        End If
    End Sub

    Sub MakeShapefile(ByVal strGEMDatabase As String, ByVal shpFileFolder As String)
        '
        ' Export Buildings
        '
        Dim strSQL As String = "SELECT * FROM BUILDING_DECODE"
        Dim pDataTable As DataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "BUILDING")
        CreateShpFromDataTable(pDataTable, "X", "Y", shpFileFolder & "\Buildings.shp")
        '
        ' Export MEDIA_DETAIL
        '
        strSQL = "SELECT * FROM MEDIA_DETAIL_DECODE"
        pDataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "MEDIA_DETAIL")
        CreateShpFromDataTable(pDataTable, "X", "Y", shpFileFolder & "\Media_Detail.shp")
        '
        ' Export Consequences
        '
        strSQL = "SELECT * FROM CONSEQUENCES_DECODE"
        pDataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "CONSEQUENCES")
        CreateShpFromDataTable(pDataTable, "X", "Y", shpFileFolder & "\Consequences.shp")
        '
        ' Export GED
        '
        strSQL = "SELECT * FROM GED_DECODE"
        pDataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "GED")
        CreateShpFromDataTable(pDataTable, "X", "Y", shpFileFolder & "\GED.shp")

    End Sub

    Function GetDataTableFromDatabase(ByVal strGEMDatabase As String, ByVal strSQL As String, ByVal strTableName As String) As DataTable
        '
        ' connect to database
        '
        Dim connection As SQLiteConnection = New SQLiteConnection("Data Source=" & strGEMDatabase)
        connection.Open()
        '
        ' GEM_OBJECT
        '
        Dim cmdSQLite As SQLite.SQLiteCommand = connection.CreateCommand()
        With cmdSQLite
            .CommandType = CommandType.Text
            .CommandText = strSQL
        End With


        Dim pDataTable As New DataTable(strTableName)
        Dim pAdapter As New SQLiteDataAdapter(cmdSQLite)
        pAdapter.Fill(pDataTable)
        connection.Close()
        Return pDataTable

    End Function

    Private Sub SourceBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceBrowse.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "GEM database files (*.db3)|*.db3|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.GEMDatabase.Text = .FileName
            End If
        End With
    End Sub

    Private Sub KMLBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SHPBrowse.Click
        With FolderBrowserDialog1
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.SHPFileFolder.Text = .SelectedPath
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
            If (Me.GEMDatabase.Text = "" Or Me.SHPFileFolder.Text = "") Then Exit Sub
            '
            ' Check the GEM database exists
            '
            If (Not IO.File.Exists(Me.GEMDatabase.Text)) Then
                MsgBox("GEM database " & Me.GEMDatabase.Text & " does not exist")
            End If
            '
            ' Check Folder exists if not try to create one
            '
            If (Not IO.Directory.Exists(Me.SHPFileFolder.Text)) Then
                IO.Directory.CreateDirectory(Me.SHPFileFolder.Text)
            End If
            '
            ' Make the shapefiles
            '
            Call MakeShapefile(Me.GEMDatabase.Text, Me.SHPFileFolder.Text)

            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Me.Dispose()

    End Sub





    Private Function CreateShpFromDataTable(ByVal dt As DataTable, ByVal XField As String, ByVal YField As String, ByVal strPath As String) As MapWinGIS.Shapefile
        '
        ' Name: CreateShpFromDataTable
        ' Purpose: To Create a Point shapefile from a DataTable.
        '       The names of the X and Y fields are passed as parameters.
        ' Written: K. Adlam, 06/12/11
        '

        'If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        'If System.IO.File.Exists(strPath.Replace(".shp", ".shx")) Then System.IO.File.Delete(strPath.Replace(".shp", ".shx"))
        'If System.IO.File.Exists(strPath.Replace(".shp", ".dbf")) Then System.IO.File.Delete(strPath.Replace(".shp", ".dbf"))
        'If System.IO.File.Exists(strPath.Replace(".shp", ".prj")) Then System.IO.File.Delete(strPath.Replace(".shp", ".prj"))
        '
        ' Create point shapefile and start editing
        '
        Dim sf As New MapWinGIS.Shapefile
        Try
            If System.IO.File.Exists(strPath) Then
                '
                ' Remove contents if shapefile already exists
                '
                sf.Open(strPath)
                sf.StartEditingShapes(True)
                For ir As Long = 0 To sf.NumShapes - 1
                    sf.EditDeleteShape(0)
                Next
                '
                ' Remove fields
                '
                Dim inum As Integer = sf.NumFields - 1
                For ifld As Integer = 0 To inum
                    sf.EditDeleteField(0)
                Next
            Else
                '
                ' Create New shapefile, set projection to WGS84 and start editing
                '
                sf.CreateNew(strPath, MapWinGIS.ShpfileType.SHP_POINT)
                Dim proj As geoprojection = New geoprojection
                proj.importfromepsg(4326)
                sf.GeoProjection = proj
                sf.StartEditingShapes(True)
            End If
            '
            'Loop each field in datatable and add to new shapefile
            '
            For Each col As DataColumn In dt.Columns
                Dim fld As New MapWinGIS.Field
                fld.Name = col.ColumnName
                '
                ' Set data type
                '
                Dim strDataType As String = col.DataType.Name.ToUpper()
                Select Case strDataType
                    Case "STRING"
                        fld.Type = MapWinGIS.FieldType.STRING_FIELD
                    Case "SINGLE"
                        fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    Case "DOUBLE"
                        fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    Case "DECIMAL"
                        fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    Case "INT16"
                        fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
                    Case "INT32"
                        fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
                    Case "INT64"
                        fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
                    Case "DATETIME"
                        fld.Type = MapWinGIS.FieldType.STRING_FIELD
                    Case Else
                        fld.Type = MapWinGIS.FieldType.STRING_FIELD
                End Select
                '
                ' Add Field to shapefile
                '
                sf.EditInsertField(fld, sf.NumFields) 'Insert all fields into shapefile
            Next col
            '
            ' get index to x and y fields
            '
            Dim ixcolumn As Integer = dt.Columns.Item(XField).Ordinal
            Dim iycolumn As Integer = dt.Columns.Item(YField).Ordinal
            '
            ' Copy the rows from the datatable to the shapefile
            '
            For irow As Long = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(irow)
                '
                ' Add shape
                '
                If (Not (IsDBNull(dr(ixcolumn)) Or IsDBNull(dr(iycolumn)))) Then
                    Dim shp As MapWinGIS.Shape = MakePointShape(dr(ixcolumn), dr(iycolumn))
                    sf.EditInsertShape(shp, sf.NumShapes) 'Insert shapes
                    '
                    ' add attribute fields
                    '
                    For field As Integer = 0 To dt.Columns.Count - 1
                        If (Not IsDBNull(dr(field))) Then
                            sf.EditCellValue(field, irow, CStr(dr(field)))
                        End If
                    Next
                End If
            Next
            '
            'Stop editing the shapefile and save changes
            '
            sf.StopEditingShapes(True, True)


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Return sf
        '
    End Function

    Function MakePointShape(ByVal x As Double, ByVal y As Double) As MapWinGIS.Shape
        '
        ' Name: MakePointShape
        ' Purpose: To create a point shape
        ' Written: K. Adlam, 18/11/11
        '
        Dim pnt As New MapWinGIS.Point()
        pnt.x = x
        pnt.y = y
        Dim shp As New MapWinGIS.Shape()
        shp.Create(MapWinGIS.ShpfileType.SHP_POINT)
        shp.InsertPoint(pnt, 0)
        Return shp
        ''
    End Function

End Class
