Imports System.IO
Imports System.Data
Imports System.Data.SQLite
Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Data.EntityClient
Imports System.Linq
Imports System.Reflection
Imports System.Collections.Generic

Public Class TempShared

    'Public Shared Function CreateNewGEMSQLiteDatabase(ByVal sqlFile As String) As Boolean

    '    Try
    '        '
    '        ' Get location to create new SQLite database from user
    '        '
    '        Dim newPath As New SaveFileDialog
    '        newPath.Filter = "SQLite 3 (*.db3)|*.db3|All Files|"
    '        If newPath.ShowDialog() = DialogResult.OK Then
    '            ExecuteSQLFromFile(newPath.FileName, sqlFile)
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function
















    'Private m_GEM_Database As String

    ''Sub CreateTestDB()
    ''    '
    ''    ' Create test database
    ''    '
    ''    Dim f As New SaveFileDialog
    ''    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|"
    ''    f.ShowDialog()
    ''    m_GEM_Shapefile = f.FileName.Replace(".db3", ".shp")
    ''    m_GEM_FOP_Table = f.FileName
    ''    'Create Database
    ''    Dim SQLconnect As New SQLite.SQLiteConnection()
    ''    'Database Doesn't Exist so Created at Path
    ''    SQLconnect.ConnectionString = "Data Source=" & f.FileName & ";"
    ''    SQLconnect.Open()
    ''    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    ''    'SQL query to Create Table

    ''    SQLcommand.CommandText = "CREATE TABLE GEM_PROJECT(id INTEGER PRIMARY KEY AUTOINCREMENT, GEM_PROJECT_ID TEXT(50), PROJECT_NAME TEXT(100), PROJECT_DATE DATE, HAZARD_TYPE TEXT(10), PROJECT_LOCALE TEXT(100), SURVEYOR TEXT);"
    ''    SQLcommand.ExecuteNonQuery()

    ''    SQLcommand.CommandText = "CREATE TABLE test(id INTEGER PRIMARY KEY AUTOINCREMENT, UUID TEXT, X NUMERIC(6), Y NUMERIC(7), title TEXT, description TEXT);"
    ''    SQLcommand.ExecuteNonQuery()
    ''    SQLcommand.CommandText = "INSERT INTO test (UUID,x,y,title, description) VALUES ('KAMA001',200,200,'This is a title', 'This is a Description')"
    ''    SQLcommand.ExecuteNonQuery()
    ''    SQLcommand.Dispose()

    ''    SQLconnect.Close()

    ''End Sub

    'Sub test2()
    '    '
    '    ' Create empty database
    '    '
    '    Dim f As New SaveFileDialog
    '    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|"
    '    f.ShowDialog()
    '    'Create Database
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    'Database Doesn't Exist so Created at Path
    '    SQLconnect.ConnectionString = "Data Source=" & f.FileName & ";"
    '    SQLconnect.Open()
    '    SQLconnect.Close()
    'End Sub

    'Sub test3()
    '    '
    '    ' Create table
    '    '
    '    Dim f As New OpenFileDialog
    '    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|*.*"
    '    If f.ShowDialog() = DialogResult.OK Then
    '        Dim SQLconnect As New SQLite.SQLiteConnection()
    '        Dim SQLcommand As SQLiteCommand
    '        SQLconnect.ConnectionString = "Data Source=" & f.FileName & ";"
    '        SQLconnect.Open()
    '        SQLcommand = SQLconnect.CreateCommand
    '        'SQL query to Create Table
    '        SQLcommand.CommandText = "CREATE TABLE test(id INTEGER PRIMARY KEY AUTOINCREMENT, X NUMERIC(6), Y NUMERIC(7), title TEXT, description TEXT);"
    '        SQLcommand.ExecuteNonQuery()
    '        SQLcommand.Dispose()
    '        SQLconnect.Close()
    '    End If
    'End Sub

    'Sub test4()
    '    '
    '    ' insert, update, delete
    '    '
    '    Dim f As New OpenFileDialog
    '    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|*.*"
    '    If f.ShowDialog() = DialogResult.OK Then
    '        Dim SQLconnect As New SQLite.SQLiteConnection()
    '        Dim SQLcommand As SQLiteCommand
    '        SQLconnect.ConnectionString = "Data Source=" & f.FileName & ";"
    '        SQLconnect.Open()
    '        SQLcommand = SQLconnect.CreateCommand
    '        'Insert Record into Foo
    '        SQLcommand.CommandText = "INSERT INTO test (x,y,title, description) VALUES (200,200,'This is a title', 'This is a Description')"
    '        'Update Last Created Record in Foo
    '        'SQLcommand.CommandText = "UPDATE foo SET title = 'New Title', description = 'New Description' WHERE id = last_insert_rowid()"
    '        'Delete Last Created Record from Foo
    '        'SQLcommand.CommandText = "DELETE FROM foo WHERE id = last_insert_rowid()"
    '        SQLcommand.ExecuteNonQuery()
    '        SQLcommand.Dispose()
    '        SQLconnect.Close()
    '    End If
    'End Sub

    'Public Shared Function GetDataTable(ByVal strDatabase As String, ByVal sql As String) As DataTable
    '    '
    '    ' Name: GetDataTable
    '    ' Purpose: To return a Datatable by executing the specified SQL
    '    '          against the specified database
    '    ' Written: K. Adlam, 23/11/11
    '    '
    '    Dim dt As DataTable = New DataTable
    '    Try
    '        Dim cnn As SQLiteConnection = New SQLiteConnection("Data Source=" & strDatabase)
    '        cnn.Open()
    '        Dim mycommand As SQLiteCommand = New SQLiteCommand(cnn)
    '        mycommand.CommandText = sql
    '        Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
    '        dt.Load(reader)
    '        reader.Close()
    '        cnn.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Return dt
    '    '
    'End Function

    'Public Shared Function GetDataTable2(ByVal strDatabase As String, ByVal sql As String, ByVal strName As String) As DataTable
    '    '
    '    ' Name: GetDataTable
    '    ' Purpose: To return a Datatable by executing the specified SQL
    '    '          against the specified database
    '    ' Written: K. Adlam, 06/12/11
    '    '
    '    Dim dt As DataTable = New DataTable
    '    Try
    '        Dim cnn As SQLiteConnection = New SQLiteConnection("Data Source=" & strDatabase)
    '        cnn.Open()
    '        Dim sqlCommand As SQLiteCommand = New SQLiteCommand(sql, cnn)
    '        '
    '        ' Create data adapter
    '        '
    '        Dim pDataAdapter As SQLiteDataAdapter = New SQLiteDataAdapter
    '        pDataAdapter.SelectCommand = sqlCommand
    '        '
    '        ' Fill table
    '        '
    '        Dim pDataset As DataSet = New DataSet
    '        pDataAdapter.Fill(pDataset, strName)

    '        cnn.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Return dt
    '    '
    'End Function


    'Function GetListOfTables(ByVal strDatabase As String) As ArrayList
    '    '
    '    ' Name: GetListOfTables
    '    ' Purpose: To return an ArrayList of table names in the specified database
    '    ' Written: K. Adlam, 23/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SchemaTable As DataTable = SQLconnect.GetSchema(SQLiteMetaDataCollectionNames.Tables)
    '    Dim pArrayList As New ArrayList
    '    For int As Integer = 0 To SchemaTable.Rows.Count - 1
    '        If SchemaTable.Rows(int)!TABLE_TYPE.ToString = "table" Then
    '            pArrayList.Add(SchemaTable.Rows(int)!TABLE_NAME.ToString())
    '        End If
    '    Next
    '    SQLconnect.Close()
    '    Return pArrayList
    'End Function

    ''Sub test7()
    ''    '
    ''    ' Select
    ''    '
    ''    Dim f As New OpenFileDialog
    ''    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|*.*"
    ''    If f.ShowDialog() = DialogResult.OK Then
    ''        Dim SQLconnect As New SQLite.SQLiteConnection()
    ''        SQLconnect.ConnectionString = "Data Source=" & f.FileName & ";"
    ''        SQLconnect.Open()
    ''        Dim CommandText As String = "SELECT * FROM test"
    ''        Dim DB As New SQLiteDataAdapter(CommandText, SQLconnect)
    ''        Dim DS As New DataSet
    ''        DS.Reset()
    ''        DB.Fill(DS)
    ''        Dim DT As DataTable = DS.Tables(0)
    ''        MsgBox(DT.TableName)
    ''        CreateShpFromDataTable(DT, "X", "Y", "E:\GEM\testout.shp")
    ''        GC.Collect()
    ''        SQLconnect.Close()
    ''    End If
    ''End Sub

    ''Public Shared Function CreateShpFromDataTable(ByVal dt As DataTable, ByVal XField As String, ByVal YField As String, ByVal strPath As String) As MapWinGIS.Shapefile
    ''    '
    ''    ' Name: CreateShpFromDataTable
    ''    ' Purpose: To Create a Point shapefile from a DataTable.
    ''    '       The names of the X and Y fields are passed as parameters.
    ''    ' Written: K. Adlam, 06/12/11
    ''    '

    ''    'If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
    ''    'If System.IO.File.Exists(strPath.Replace(".shp", ".shx")) Then System.IO.File.Delete(strPath.Replace(".shp", ".shx"))
    ''    'If System.IO.File.Exists(strPath.Replace(".shp", ".dbf")) Then System.IO.File.Delete(strPath.Replace(".shp", ".dbf"))
    ''    'If System.IO.File.Exists(strPath.Replace(".shp", ".prj")) Then System.IO.File.Delete(strPath.Replace(".shp", ".prj"))
    ''    '
    ''    ' Create point shapefile and start editing
    ''    '
    ''    Dim sf As New MapWinGIS.Shapefile
    ''    Try
    ''        If System.IO.File.Exists(strPath) Then
    ''            '
    ''            ' Remove contents if shapefile already exists
    ''            '
    ''            sf.Open(strPath)
    ''            sf.StartEditingShapes(True)
    ''            For ir As Long = 0 To sf.NumShapes - 1
    ''                sf.EditDeleteShape(0)
    ''            Next
    ''            '
    ''            ' Remove fields
    ''            '
    ''            Dim inum As Integer = sf.NumFields - 1
    ''            For ifld As Integer = 0 To inum
    ''                sf.EditDeleteField(0)
    ''            Next
    ''        Else
    ''            '
    ''            ' Create New shapefile
    ''            '
    ''            sf.CreateNew(strPath, MapWinGIS.ShpfileType.SHP_POINT)
    ''            sf.StartEditingShapes(True)
    ''        End If
    ''        '
    ''        'Loop each field in datatable and add to new shapefile
    ''        '
    ''        For Each col As DataColumn In dt.Columns
    ''            Dim fld As New MapWinGIS.Field
    ''            fld.Name = col.ColumnName
    ''            '
    ''            ' Set data type
    ''            '
    ''            Dim strDataType As String = col.DataType.Name.ToUpper()
    ''            Select Case strDataType
    ''                Case "STRING"
    ''                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    ''                Case "SINGLE"
    ''                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    ''                Case "DOUBLE"
    ''                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    ''                Case "DECIMAL"
    ''                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    ''                Case "INT16"
    ''                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    ''                Case "INT32"
    ''                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    ''                Case "INT64"
    ''                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    ''                Case "DATETIME"
    ''                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    ''                Case Else
    ''                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    ''            End Select
    ''            '
    ''            ' Add Field to shapefile
    ''            '
    ''            sf.EditInsertField(fld, sf.NumFields) 'Insert all fields into shapefile
    ''        Next col
    ''        '
    ''        ' get index to x and y fields
    ''        '
    ''        Dim ixcolumn As Integer = dt.Columns.Item(XField).Ordinal
    ''        Dim iycolumn As Integer = dt.Columns.Item(YField).Ordinal
    ''        '
    ''        ' Copy the rows from the datatable to the shapefile
    ''        '
    ''        For irow As Long = 0 To dt.Rows.Count - 1
    ''            Dim dr As DataRow = dt.Rows(irow)
    ''            '
    ''            ' Add shape
    ''            '
    ''            Dim shp As MapWinGIS.Shape = MakePointShape(dr(ixcolumn), dr(iycolumn))
    ''            sf.EditInsertShape(shp, sf.NumShapes) 'Insert shapes
    ''            '
    ''            ' add attribute fields
    ''            '
    ''            For field As Integer = 0 To dt.Columns.Count - 1
    ''                sf.EditCellValue(field, irow, CStr(dr(field)))
    ''            Next
    ''        Next
    ''        '
    ''        'Stop editing the shapefile and save changes
    ''        '
    ''        sf.StopEditingShapes(True, True)


    ''    Catch ex As Exception
    ''        MsgBox(ex.ToString)
    ''    End Try

    ''    Return sf
    ''    '
    ''End Function

    'Sub AddPointToShapefile(ByVal uuid As String, ByVal x As Double, ByVal y As Double, ByVal sf As MapWinGIS.Shapefile)
    '    '
    '    ' Name: AddPointToShapefile
    '    ' Purpose: To add a poit feature to an ESRI shapefile
    '    ' Written: K. Adlam, 23/11/11
    '    '
    '    '
    '    ' create shape from x,y
    '    '
    '    Dim shp As MapWinGIS.Shape = MakePointShape(x, y) ' create shape
    '    sf.EditInsertShape(shp, sf.NumShapes) 'Insert shape
    '    '
    '    ' add attribute
    '    '
    '    AddAttribute(sf, "UUID", sf.NumShapes, uuid)
    '    AddAttribute(sf, "X", sf.NumShapes, x)
    '    AddAttribute(sf, "Y", sf.NumShapes, y)
    '    '
    'End Sub

    'Sub AddRecordToDatabaseTable(ByVal uuid As String, ByVal x As Double, ByVal y As Double, ByVal strDatabase As String)
    '    '
    '    ' Name: AddRecordToDatabaseTable
    '    ' Purpose: To add a record to sqlite database table
    '    ' Written: K. Adlam, 18/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    '    SQLcommand.CommandText = "INSERT INTO test (uuid,x,y) VALUES (uuid,x,y)"
    '    SQLcommand.ExecuteNonQuery()
    '    SQLcommand.Dispose()
    '    SQLconnect.Close()
    '    ''
    'End Sub

    'Sub AddAttribute(ByVal sf As MapWinGIS.Shapefile, ByVal strField As String, ByVal rec As Long, ByVal theValue As Object)
    '    '
    '    ' Name: AddAttribute
    '    ' Purpose: To add a single attribute value to shapefile
    '    ' Written: K. Adlam, 18/11/11
    '    '
    '    Dim iField As Long = GetFieldIndex(sf, strField)
    '    sf.EditCellValue(iField, rec, theValue)
    '    '
    'End Sub

    'Function GetFieldIndex(ByVal sf As MapWinGIS.Shapefile, ByVal strField As String) As Integer
    '    '
    '    ' Name: GetFieldIndex
    '    ' Purpose: To get a field index from a shapefile given a field name
    '    ' Written: K. Adlam, 18/11/11
    '    '
    '    Dim i As Integer = -1
    '    For i = 0 To sf.NumFields
    '        Dim pField As MapWinGIS.Field = sf.Field(i)
    '        If (pField.Name.ToLower = strField.ToLower) Then
    '            Return i
    '            Exit For
    '        End If
    '    Next
    '    Return i
    '    '
    'End Function

    'Function GetProjectSettingValue(ByVal strDatabase As String, ByVal strSetting As String)
    '    '
    '    ' Name: GetProjectSettingValue
    '    ' Purpose: To get a value for project settings table
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    '    Dim strCommand As String = "SELECT PROJ_VALUE FROM PROJECT_SETTINGS where PROJ_SETTING = '" & strSetting & "'"
    '    SQLcommand.CommandText = strCommand
    '    Dim SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
    '    If (SQLreader.HasRows) Then
    '        SQLreader.Read()
    '        GetProjectSettingValue = SQLreader("PROJ_VALUE")
    '    Else
    '        GetProjectSettingValue = ""
    '    End If
    '    SQLcommand.Dispose()
    '    SQLconnect.Close()
    '    '
    'End Function

    'Sub SetProjectSettingValue(ByVal strDatabase As String, ByVal strSetting As String, ByVal strValue As String)
    '    '
    '    ' Name: SetProjectSettingValue
    '    ' Purpose: To set a value in the project settings table
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim strCommand As String = ""
    '    Dim strWhere As String = "PROJ_SETTING='" & strSetting & "'"
    '    If (GetNumberOfRows(strDatabase, "PROJECT_SETTINGS", strWhere) = 0) Then
    '        strCommand = "INSERT INTO PROJECT_SETTINGS (PROJ_SETTING,PROJ_VALUE) VALUES ('" & strSetting & "','" & strValue & "')"
    '    Else
    '        strCommand = "UPDATE PROJECT_SETTINGS SET PROJ_VALUE = '" & strValue & "' WHERE PROJ_SETTING = '" & strSetting & "'"
    '    End If
    '    Call ExecuteSQLCommand(strDatabase, strCommand)
    '    '
    'End Sub

    'Function GetNumberOfRows(ByVal strDatabase As String, ByVal strTable As String, Optional ByVal strWhere As String = "") As Long
    '    '
    '    ' Name: GetNumberOfRows
    '    ' Purpose: To return the number of rows in a table
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim strCommand As String = "SELECT COUNT(*) FROM " & strTable & " WHERE " & strWhere
    '    Dim numRows As Long = ExecuteScalar(strDatabase, strCommand)
    '    Return numRows
    '    '
    'End Function

    'Function TableExists(ByVal strDatabase As String, ByVal strTable As String) As Boolean
    '    '
    '    ' Name: TableExists
    '    ' Purpose: To check if a table exists in SQLite database
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SchemaTable As DataTable = SQLconnect.GetSchema(SQLiteMetaDataCollectionNames.Tables)
    '    For int As Integer = 0 To SchemaTable.Rows.Count - 1
    '        If SchemaTable.Rows(int)!TABLE_TYPE.ToString = "table" Then
    '            If (SchemaTable.Rows(int)!TABLE_NAME.ToString.ToLower = strTable.ToLower) Then
    '                Return True
    '                Exit Function
    '            End If
    '        End If
    '    Next
    '    Return False
    '    SQLconnect.Close()
    '    '
    'End Function

    'Public Shared Function ExecuteSQLCommand(ByVal strDatabase As String, ByVal strCommand As String) As Long
    '    '
    '    ' Name: ExecuteSQLCommand
    '    ' Purpose: To execute any Non-Query command against the specified database
    '    '       The number of rows affected is returned
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    '    SQLcommand.CommandText = strCommand
    '    Dim irows As Long = SQLcommand.ExecuteNonQuery()
    '    SQLcommand.Dispose()
    '    SQLconnect.Close()
    '    Return irows
    '    ''
    'End Function

    'Function ExecuteScalar(ByVal strDatabase As String, ByVal strCommand As String) As Long
    '    '
    '    ' Name: ExecuteSQLCommand
    '    ' Purpose: To execute any Scalar command against the specified database
    '    '       The number of rows affected is returned
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    '    SQLcommand.CommandText = strCommand
    '    Dim ireturn As Object = SQLcommand.ExecuteScalar
    '    SQLcommand.Dispose()
    '    SQLconnect.Close()
    '    Return ireturn
    '    ''
    'End Function

    'Sub CreateNewGEMDatabase(ByVal strDatabase As String, ByVal strSQLFile As String)
    '    '
    '    ' Name: CreateNewGEMDatabase
    '    ' Purpose: To create a new GEM SQLite database
    '    '
    '    ' Written: K. Adlam, 23/11/11
    '    '
    '    ' Check SQL file exists
    '    '
    '    If (Not IO.File.Exists(strSQLFile)) Then
    '        MsgBox("Cannot create database, SQL file does not exist")
    '        Exit Sub
    '    End If
    '    '
    '    ' check folder path exists if not create folders
    '    '
    '    If (Not IO.Directory.Exists(IO.Path.GetDirectoryName(strDatabase))) Then
    '        IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(strDatabase))
    '    End If
    '    '
    '    ' Create empty database
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.CreateFile(strDatabase)
    '    '
    '    ' Create contents of database from SQL file
    '    '
    '    ExecuteSQLFromFile(strDatabase, strSQLFile)
    '    '
    'End Sub

    'Public Shared Sub ExecuteSQLFromFile(ByVal strDatabase As String, ByVal strFile As String)
    '    '
    '    ' Name: ExecuteSQLFromFile
    '    ' Purpose: To execute SQL commands stored in a textfile against the
    '    '       specified SQLite database
    '    '
    '    ' Written: K. Adlam, 23/11/11
    '    '
    '    If (IO.File.Exists(strFile)) Then
    '        Dim sr As StreamReader = New StreamReader(strFile)
    '        Dim strSQL As String = sr.ReadToEnd
    '        ExecuteSQLCommand(strDatabase, strSQL)
    '    End If
    '    '
    'End Sub

    'Function MakePointShape(ByVal x As Double, ByVal y As Double) As MapWinGIS.Shape
    '    '
    '    ' Name: MakePointShape
    '    ' Purpose: To create a point shape
    '    ' Written: K. Adlam, 18/11/11
    '    '
    '    Dim pnt As New MapWinGIS.Point()
    '    pnt.x = x
    '    pnt.y = y
    '    Dim shp As New MapWinGIS.Shape()
    '    shp.Create(MapWinGIS.ShpfileType.SHP_POINT)
    '    shp.InsertPoint(pnt, 0)
    '    Return shp
    '    ''
    'End Function

    'Public Shared Sub ADDFOP2(ByVal x As Double, ByVal y As Double, ByVal id As String, ByVal shapefile As String)
    '    Try
    '        '
    '        ' Name: ADDFOP2
    '        ' Purpose: To add a FOP to the database
    '        ' Written: K. Adlam, 06/12/11
    '        '
    '        '
    '        ' create shape
    '        '
    '        Dim pnt As New MapWinGIS.Point()
    '        pnt.x = x
    '        pnt.y = y
    '        Dim shp As New MapWinGIS.Shape()
    '        shp.Create(MapWinGIS.ShpfileType.SHP_POINT)
    '        shp.InsertPoint(pnt, 0)
    '        '
    '        ' Open shapefile and start editing,insert shape and stop editing
    '        '
    '        Dim sf As New MapWinGIS.Shapefile
    '        sf.Open(shapefile)
    '        sf.StartEditingShapes(True)
    '        Dim index As Long = 0
    '        sf.EditInsertShape(shp, index)
    '        sf.EditCellValue(2, index, "id1")
    '        sf.StopEditingShapes(True, True)
    '        '
    '        ' add attribute
    '        '

    '        'If Not sf.SaveAs("c:\points.shp", Nothing) Then
    '        '    ' null is for callback, we don't use it
    '        '    ' most likely the file with the same name already exists
    '        '    'MessageBox.Show(sf.get_ErrorMsg(sf.LastErrorCode))
    '        'End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    'End Sub

    ''Public Shared Sub StartSurveyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartSurveyButton.Click
    ''    If (Me.Earthquake.Text = "" Or Me.Surveyor.Text = "" Or TheDate.Text = "" Or SurveyName.Text = "") Then
    ''        MsgBox("Please enter all setup details before proceeding.")
    ''        TabControl1.SelectedTab = TabPage1
    ''        m_tabLock = True
    ''    Else
    ''        m_tabLock = False
    ''        MsgBox("Start Survey")
    ''    End If

    ''End Sub

    ''Public Shared Sub PreLoadMapsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreLoadMapsButton.Click
    ''    MsgBox("Display pre-load maps dialog")
    ''End Sub

    ''Public Shared Sub ClearAllDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAllDataButton.Click
    ''    MsgBox(" Clear all data")
    ''End Sub

    ''Public Shared Sub ShowHelpButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHelpButton.Click
    ''    MsgBox("Display help")
    ''End Sub

    ''Sub test()
    ''    '
    ''    ' Add shapefile to toc
    ''    '
    ''    'Dim handle1 As Integer
    ''    'Me.Legend.Map = MapMain.GetOcx
    ''    Dim shp1 As New MapWinGIS.Shapefile
    ''    shp1.Open("e:\GEM\theme1.shp")
    ''    m_layers.AddLayer(shp1, "test")
    ''    '
    ''    ' Create new shapefile
    ''    '
    ''    'Dim mShape As New MapWinGIS.Shape
    ''    'mShape.ShapeType = MapWinGIS.ShpfileType.SHP_POINT
    ''    'Dim pPoint As New MapWinGIS.Point()
    ''    'pPoint.x = 10
    ''    'pPoint.y = 20
    ''    'mShape.InsertPoint(pPoint, 0)
    ''    'Dim shp2 As New MapWinGIS.Shapefile
    ''    'shp2.CreateNew("e:\GEM\temp2.shp", MapWinGIS.ShpfileType.SHP_POINT)
    ''    ''shp2.Open("e:\GEM\temp.shp")
    ''    'shp2.StartEditingShapes(True)
    ''    'shp2.EditInsertShape(mShape, shp2.NumShapes)
    ''    'shp2.EditInsertShape(mShape, shp2.NumShapes)
    ''    'shp2.EditInsertShape(mShape, shp2.NumShapes)
    ''    'shp2.StopEditingShapes(True, True)
    ''    'm_layers.AddLayer(shp2, "test3")




    ''End Sub

    ''Public Shared Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    ''    'Call AddFOBS(GetUUID, 300, 300, sf)
    ''    'Dim dt As DataTable = GetDataTable(m_GEM_Database, "Select * from PROJECT_SETTINGS")
    ''    'MsgBox(dt.Rows.Count)
    ''    Dim f As New OpenFileDialog
    ''    f.Filter = "SQLite 3 (*.sql)|*.sql|All Files|*.*"
    ''    If f.ShowDialog() = DialogResult.OK Then
    ''        Call ExecuteSQLFromFile(m_GEM_Database, f.FileName)
    ''    End If
    ''End Sub

    'Sub AddFOBS(ByVal uuid As String, ByVal x As Double, ByVal y As Double, ByVal sf As MapWinGIS.Shapefile, ByVal strDatabase As String)
    '    Call AddPointToShapefile(uuid, x, y, sf)
    '    Call AddRecordToDatabaseTable(uuid, x, y, strDatabase)

    'End Sub

    'Function GetUUID() As String
    '    '
    '    ' Name: GetUUID
    '    ' Purpose: To construct a unique identifier using the name of the user and 
    '    '          current Date and time
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Return Strings.Left(Environ("USERNAME"), 10) & _
    '    Date.Now.ToString("yyyyMMddHHmmssfff")
    'End Function

    'Function GetGUID() As Guid
    '    '
    '    ' Name: GetGUID
    '    ' Purpose: To get a GUID
    '    ' Written: K. Adlam, 22/11/11
    '    '
    '    Dim pGuid As Guid = Guid.NewGuid()
    '    Return pGuid
    '    '
    'End Function

    'Sub PopulateComboBoxFromTable(ByVal pCombobox As ComboBox, ByVal strDatabase As String, ByVal sql As String)
    '    '
    '    ' Select
    '    '
    '    Dim SQLconnect As New SQLite.SQLiteConnection()
    '    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    '    SQLconnect.Open()
    '    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    '    SQLcommand.CommandText = sql
    '    Dim SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
    '    '
    '    ' Clear combo and populate with values
    '    '
    '    pCombobox.Items.Clear()
    '    While SQLreader.Read()
    '        pCombobox.Items.Add(SQLreader(0))
    '    End While
    '    '
    '    ' clean up
    '    '
    '    SQLcommand.Dispose()
    '    SQLconnect.Close()

    'End Sub

    ''Sub code_Snippet()
    ''    Dim custConn As SqlConnection = New SqlConnection("Data Source=localhost;Integrated Security=SSPI;" & _
    ''                                             "Initial Catalog=northwind;")
    ''    Dim custDA As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM Customers", custConn)

    ''    Dim orderConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & _
    ''                                                           "Data Source=c:\Program Files\Microsoft Office\" & _
    ''                                                           "Office\Samples\northwind.mdb;")
    ''    Dim orderDA As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM Orders", orderConn)

    ''    custConn.Open()
    ''    orderConn.Open()

    ''    Dim custDS As DataSet = New DataSet()

    ''    custDA.Fill(custDS, "Customers")
    ''    orderDA.Fill(custDS, "Orders")

    ''    custConn.Close()
    ''    orderConn.Close()

    ''    Dim custOrderRel As DataRelation = custDS.Relations.Add("CustOrders", _
    ''                                         custDS.Tables("Customers").Columns("CustomerID"), _
    ''                                         custDS.Tables("Orders").Columns("CustomerID"))

    ''    Dim pRow, cRow As DataRow

    ''    For Each pRow In custDS.Tables("Customers").Rows
    ''        Console.WriteLine(pRow("CustomerID").ToString())

    ''        For Each cRow In pRow.GetChildRows(custOrderRel)
    ''            Console.WriteLine(vbTab & cRow("OrderID").ToString())
    ''        Next
    ''    Next

    ''End Sub

    ''Public Shared Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    ''    'Call Load_GEM_Database(m_GEM_Database)
    ''    Call SetProjectSettingValue(m_GEM_Database, "HAZARD", "Earthquake")
    ''    MsgBox(GetProjectSettingValue(m_GEM_Database, "HAZARD"))
    ''    Call SetProjectSettingValue(m_GEM_Database, "HAZARD", "Earthquake2")
    ''    MsgBox(GetProjectSettingValue(m_GEM_Database, "HAZARD"))
    ''End Sub

    ''Sub Load_GEM_Database(ByVal strDatabase As String)

    ''End Sub

    ''Public Shared Sub Earthquake_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Earthquake.LostFocus
    ''    Call SetProjectSettingValue(m_GEM_Database, "EARTHQUAKE", Me.Earthquake.Text)
    ''End Sub

    ''Public Shared Sub Surveyor_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Surveyor.LostFocus
    ''    Call SetProjectSettingValue(m_GEM_Database, "SURVEYOR", Me.Surveyor.Text)
    ''End Sub

    ''Public Shared Sub SurveyName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SurveyName.LostFocus
    ''    Call SetProjectSettingValue(m_GEM_Database, "SURVEY_NAME", Me.SurveyName.Text)
    ''End Sub



    ''Sub PopulateTreeview()
    ''    'With TreeView1
    ''    '    .CheckBoxes = True
    ''    '    .BeginUpdate()
    ''    '    .Nodes.Add("Reinforced Concrete")
    ''    '    .Nodes(0).Nodes.Add("Unknown material")
    ''    '    .Nodes(0).Nodes(0).Nodes.Add("Reinforced concrete, unspecified")
    ''    '    .Nodes(0).Nodes(0).Nodes.Add("Reinforced concrete, cast-in-place")
    ''    '    .Nodes(0).Nodes.Add("Precast concrete")
    ''    '    .Nodes(0).Nodes.Add("Composite steel and concrete")
    ''    '    .Nodes(0).Nodes.Add("Prestressed concrete")
    ''    '    .Nodes.Add("Steel")
    ''    '    .Nodes(1).Nodes.Add("Steel, unspecified")
    ''    '    .Nodes(1).Nodes.Add("Steel sections, hot-rolled and tubular")
    ''    '    .Nodes(1).Nodes.Add("Steel, cold-formed")
    ''    '    .Nodes.Add("Masonry")
    ''    '    .Nodes(2).Nodes.Add("Masonry, unspecified")
    ''    '    .Nodes.Add("Earthen")
    ''    '    .Nodes.Add("Timber")
    ''    '    .Nodes.Add("Other")
    ''    '    .EndUpdate()
    ''    'End With

    ''    TreeView1.CheckBoxes = True
    ''    TreeView1.ShowNodeToolTips = True
    ''    Dim dt As DataTable = GetDataTable(m_GEM_Database, "SELECT * FROM GEM_DICTIONARY")
    ''    Call FillTreeView(TreeView1.Nodes, "OBJECT", dt)
    ''End Sub

    'Sub FillTreeView(ByVal parentNode As TreeNodeCollection, ByVal parentID As String, ByVal dt As DataTable)
    '    For Each row As DataRow In dt.Rows
    '        If row("PARENT") = parentID Then
    '            Dim key As [String] = row("CODE").ToString()
    '            Dim text As [String] = row("DESCRIPTION").ToString()
    '            Dim tooltipText As String = row("EXPLANATION").ToString
    '            Dim newParentNode As TreeNode = parentNode.Add(key, text)
    '            newParentNode.ToolTipText = tooltipText
    '            FillTreeView(newParentNode.Nodes, row("CODE"), dt)
    '            'Dim newParentNode As TreeNodeCollection = parentNode.Add(key, text).Nodes
    '            'FillTreeView(newParentNode, row("ID"), dt)
    '        End If
    '    Next
    'End Sub



    ''Public Shared Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
    ''    RemoveHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck
    ''    'For Each node As TreeNode In e.Node.Nodes
    ''    'node.Checked = e.Node.Checked
    ''    'Next
    ''    If e.Node.Checked Then
    ''        '
    ''        ' Check all ancestors
    ''        '
    ''        Dim pNode As TreeNode = e.Node.Parent
    ''        Do Until pNode Is Nothing
    ''            pNode.Checked = True
    ''            pNode = pNode.Parent
    ''        Loop
    ''        '
    ''        ' Do not allow multiple children at level 1
    ''        '
    ''        'If (e.Node.Level = 1) Then
    ''        Dim cNode As TreeNode = e.Node.Parent
    ''        If (Not cNode Is Nothing) Then
    ''            For Each pcnode As TreeNode In cNode.Nodes
    ''                If (Not pcnode Is e.Node) Then
    ''                    pcnode.Checked = False
    ''                End If
    ''            Next
    ''        End If
    ''        'End If
    ''    Else
    ''        '
    ''        ' uncheck all children
    ''        '
    ''        Call UncheckChildNodes(e.Node)
    ''    End If
    ''    AddHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck

    ''    Me.ListBox1.Items.Clear()
    ''    Call PopulateListbox(Me.ListBox1)

    ''End Sub

    ''Public Shared Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
    ''    If node.Nodes.Count = 0 Then
    ''        Return False
    ''    End If
    ''    Dim childNode As TreeNode
    ''    For Each childNode In node.Nodes
    ''        If childNode.Checked Then
    ''            Return True
    ''        End If
    ''        ' Recursively check the children of the current child node.
    ''        If HasCheckedChildNodes(childNode) Then
    ''            Return True
    ''        End If
    ''    Next childNode
    ''    Return False
    ''End Function 'HasCheckedChildNodes 

    ''Public Shared Sub UncheckChildNodes(ByVal node As TreeNode)
    ''    Dim childNode As TreeNode
    ''    For Each childNode In node.Nodes
    ''        childNode.Checked = False
    ''        ' Recursively uncheck the children of the current child node.
    ''        Call UncheckChildNodes(childNode)
    ''    Next childNode
    ''End Sub


    ''Public Shared Sub TreeView1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseMove
    ''    Dim currentNode As TreeNode = Me.TreeView1.GetNodeAt(e.X, e.Y)
    ''    If (Not currentNode Is Nothing) Then
    ''        'If (Not currentNode.Tag.ToString() = Me.toolTip1.GetToolTip(Me.TreeView1).ToString()) Then
    ''        '    Me.toolTip1.SetToolTip(Me.TreeView1, currentNode.Tag.ToString())
    ''        'Else
    ''        '    Me.toolTip1.SetToolTip(Me.TreeView1, "")
    ''        'End If

    ''        'Me.ToolTip1.SetToolTip(Me.TreeView1, "fred")
    ''    End If
    ''End Sub

    ''Public Shared Sub NewGEMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewGEMDB.Click
    ''    '
    ''    ' Get location from user
    ''    '
    ''    Dim f As New SaveFileDialog
    ''    f.Filter = "SQLite 3 (*.db3)|*.db3|All Files|*.*"
    ''    If f.ShowDialog() = DialogResult.OK Then
    ''        Dim sqlFile As String = My.Resources.GemSqlLiteCreateTableScript
    ''        Call ExecuteSQLCommand(f.FileName, sqlFile)
    ''    End If
    ''End Sub

    ''Public Shared Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    ''    '
    ''    ' Execute SQL from specified file against current GEM database
    ''    '
    ''    Dim f As New OpenFileDialog
    ''    f.Filter = "SQL File (*.sql)|*.sql|All Files|*.*"
    ''    If f.ShowDialog() = DialogResult.OK Then
    ''        Call ExecuteSQLFromFile(m_GEM_Database, f.FileName)
    ''    End If
    ''End Sub

    ''Public Shared Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
    ''    'Me.WebBrowser1.Navigate("file://e:/GEM/GEM_Glossary.pdf#nameddest=PRECAST", True)
    ''    Me.WebBrowser1.Navigate("file://e:/GEM/GEM_Glossary.htm#PRECAST", False)
    ''End Sub

    'Sub RemoveSubPaths(ByVal pListbox As ListBox, ByVal pNode As TreeNode)
    '    '
    '    ' Check all ancestors
    '    '
    '    Dim pParentNode As TreeNode = pNode.Parent
    '    Do Until pParentNode Is Nothing
    '        pListbox.Items.Remove(pParentNode.FullPath)
    '        pParentNode = pParentNode.Parent
    '    Loop
    'End Sub

    ''Sub PopulateListbox(ByVal pListbox As ListBox)
    ''    For Each pNode As TreeNode In TreeView1.Nodes
    ''        Call PopulateListBoxFromTreeView(pListbox, pNode)
    ''    Next
    ''End Sub

    ''Public Shared Sub PopulateListBoxFromTreeView(ByVal pListBox As ListBox, ByVal node As TreeNode)
    ''    If (node.Checked And (Not HasCheckedChildren(node))) Then
    ''        pListBox.Items.Add(node.FullPath)
    ''    End If
    ''    For Each childNode As TreeNode In node.Nodes
    ''        ' Recursively populate listbox.
    ''        Call PopulateListBoxFromTreeView(pListBox, childNode)
    ''    Next childNode
    ''End Sub

    'Function HasCheckedChildren(ByVal pNode As TreeNode) As Boolean
    '    For Each childNode As TreeNode In pNode.Nodes
    '        If (childNode.Checked) Then
    '            Return True
    '            Exit Function
    '        End If
    '    Next
    '    Return False
    'End Function


    ''Public Shared Sub TreeView1_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    ''    If (e.Button = Windows.Forms.MouseButtons.Right) Then
    ''        Me.WebBrowser1.Navigate("file://e:/GEM/GEM_Glossary.pdf#nameddest=" & e.Node.Name, True)
    ''        'MsgBox("Help for " & e.Node.Name)
    ''    End If
    ''End Sub


    ''Public Shared Sub ZipFiles()

    ''    Dim zipPath As String = "C:\TEMP\Compression\myzip.zip"

    ''    'Open the zip file if it exists, else create a new one 
    ''    Dim zip As Package = ZipPackage.Open(zipPath, _
    ''         IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)

    ''    'Add as many files as you like:
    ''    AddToArchive(zip, "C:\TEMP\Compression\Compress Me1.txt")
    ''    AddToArchive(zip, "C:\TEMP\Compression\Compress Me2.txt")
    ''    AddToArchive(zip, "C:\TEMP\Compression\Compress Me3.txt")

    ''    zip.Close() 'Close the zip file

    ''End Sub


    ''Public Shared Sub AddToArchive(ByVal zip As Package, _
    ''                     ByVal fileToAdd As String)

    ''    'Replace spaces with an underscore (_) 
    ''    Dim uriFileName As String = fileToAdd.Replace(" ", "_")

    ''    'A Uri always starts with a forward slash "/" 
    ''    Dim zipUri As String = String.Concat("/", IO.Path.GetFileName(uriFileName))

    ''    Dim partUri As New Uri(zipUri, UriKind.Relative)
    ''    Dim contentType As String = Net.Mime.MediaTypeNames.Application.Zip

    ''    'The PackagePart contains the information: 
    ''    ' Where to extract the file when it's extracted (partUri) 
    ''    ' The type of content stream (MIME type):  (contentType) 
    ''    ' The type of compression:  (CompressionOption.Normal)   
    ''    Dim pkgPart As PackagePart = zip.CreatePart(partUri, contentType, CompressionOption.Normal)

    ''    'Read all of the bytes from the file to add to the zip file 
    ''    Dim bites As Byte() = File.ReadAllBytes(fileToAdd)

    ''    'Compress and write the bytes to the zip file 
    ''    pkgPart.GetStream().Write(bites, 0, bites.Length)

    ''End Sub


    ''Public Shared Shared Sub CreatePackage()
    ''    ' Convert system path and file names to Part URIs. In this example
    ''    ' Dim partUriDocument as Uri /* /Content/Document.xml */ =
    ''    '     PackUriHelper.CreatePartUri(
    ''    '         New Uri("Content\Document.xml", UriKind.Relative))
    ''    ' Dim partUriResource as Uri /* /Resources/Image1.jpg */ =
    ''    '     PackUriHelper.CreatePartUri(
    ''    '         New Uri("Resources\Image1.jpg", UriKind.Relative))
    ''    Dim partUriDocument As Uri = PackUriHelper.CreatePartUri(New Uri(documentPath, UriKind.Relative))
    ''    Dim partUriResource As Uri = PackUriHelper.CreatePartUri(New Uri(resourcePath, UriKind.Relative))

    ''    ' Create the Package
    ''    ' (If the package file already exists, FileMode.Create will
    ''    '  automatically delete it first before creating a new one.
    ''    '  The 'using' statement insures that 'package' is
    ''    '  closed and disposed when it goes out of scope.)
    ''    Using package As Package = Package.Open(packagePath, FileMode.Create)
    ''        ' Add the Document part to the Package
    ''        Dim packagePartDocument As PackagePart = package.CreatePart(partUriDocument, System.Net.Mime.MediaTypeNames.Text.Xml)

    ''        ' Copy the data to the Document Part
    ''        Using fileStream As New FileStream(documentPath, FileMode.Open, FileAccess.Read)
    ''            CopyStream(fileStream, packagePartDocument.GetStream())
    ''        End Using ' end:using(fileStream) - Close and dispose fileStream.

    ''        ' Add a Package Relationship to the Document Part
    ''        package.CreateRelationship(packagePartDocument.Uri, TargetMode.Internal, PackageRelationshipType)

    ''        ' Add a Resource Part to the Package
    ''        Dim packagePartResource As PackagePart = package.CreatePart(partUriResource, System.Net.Mime.MediaTypeNames.Image.Jpeg)

    ''        ' Copy the data to the Resource Part
    ''        Using fileStream As New FileStream(resourcePath, FileMode.Open, FileAccess.Read)
    ''            CopyStream(fileStream, packagePartResource.GetStream())
    ''        End Using ' end:using(fileStream) - Close and dispose fileStream.

    ''        ' Add Relationship from the Document part to the Resource part
    ''        packagePartDocument.CreateRelationship(New Uri("../resources/image1.jpg", UriKind.Relative), TargetMode.Internal, ResourceRelationshipType)

    ''    End Using ' end:using (Package package) - Close and dispose package.

    ''End Sub ' end:CreatePackage()



    'Public Shared Function CreateShpFromGEM_Object(ByVal gemObject As GEM_OBJECT) As MapWinGIS.Shapefile
    '    '
    '    ' Name: CreateShpFromGEM_Object
    '    ' Purpose: To Create a Point shapefile from a GEM_OBJECT.
    '    '       The names of the X and Y fields are passed as parameters.
    '    ' Written: K. Adlam, 06/12/11
    '    '

    '    '
    '    ' Create point shapefile and start editing
    '    '
    '    memoryShape = New MapWinGIS.Shapefile
    '    memoryShape.CreateNew("", MapWinGIS.ShpfileType.SHP_POINT)
    '    memoryShape.FastMode = True
    '    memoryShape.CacheExtents = True
    '    Try
    '        '
    '        ' Create New shapefile
    '        '

    '        memoryShape.StartEditingShapes(True)
    '        '
    '        'Loop each field in datatable and add to new shapefile
    '        '
    '        For Each col As DataColumn In dt.Columns
    '            Dim fld As New MapWinGIS.Field
    '            fld.Name = col.ColumnName
    '            '
    '            ' Set data type
    '            '
    '            Dim strDataType As String = col.DataType.Name.ToUpper()
    '            Select Case strDataType
    '                Case "STRING"
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '                Case "SINGLE"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "DOUBLE"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "DECIMAL"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "INT16"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "INT32"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "INT64"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "DATETIME"
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '                Case Else
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '            End Select
    '            '
    '            ' Add Field to shapefile
    '            '
    '            sf.EditInsertField(fld, sf.NumFields) 'Insert all fields into shapefile
    '        Next col
    '        '
    '        ' get index to x and y fields
    '        '
    '        Dim ixcolumn As Integer = dt.Columns.Item(XField).Ordinal
    '        Dim iycolumn As Integer = dt.Columns.Item(YField).Ordinal
    '        ''
    '        '' Copy the rows from the datatable to the shapefile
    '        ''
    '        'For irow As Long = 0 To dt.Rows.Count - 1
    '        '    Dim dr As DataRow = dt.Rows(irow)
    '        '    '
    '        '    ' Add shape
    '        '    '
    '        '    Dim shp As MapWinGIS.Shape = MakePointShape(dr(ixcolumn), dr(iycolumn))
    '        '    sf.EditInsertShape(shp, sf.NumShapes) 'Insert shapes
    '        '    '
    '        '    ' add attribute fields
    '        '    '
    '        '    For field As Integer = 0 To dt.Columns.Count - 1
    '        '        sf.EditCellValue(field, irow, CStr(dr(field)))
    '        '    Next
    '        'Next
    '        '
    '        'Stop editing the shapefile and save changes
    '        '
    '        sf.StopEditingShapes(True, True)


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Return sf
    '    '
    'End Function


    'Public Function LINQ2Datatable() As DataTable
    '    Dim gemObjectQuery As ObjectQuery(Of GEM_OBJECT) = _
    '    From gemObj In gemdb.Database.GEM_OBJECT _
    '    Select gemObj

    '    Dim gemObject As GEM_OBJECT
    '    For Each gemObject In gemObjectQuery
    '        ' MsgBox(gemObject.EPSG_CODE)
    '    Next

    '    Dim pDataTable As DataTable = EQToDataTable(gemObjectQuery)
    '    'Dim context As gem = New GEM_OBJECT
    '    'Dim pDataTable As DataTable = EntityToDatatable(gemObjectQuery, gemObjectQuery.Context)


    '    For Each column As DataRow In pDataTable.Columns
    '    Next
    '    MsgBox(pDataTable.Columns.Count)

    '    ' Dim shp As MapWinGIS.Shapefile = CreateShpFromDataTable(pDataTable, "X", "Y", "")
    '    'MsgBox(shp.Table.ToString)

    '    Return pDataTable

    'End Function


    ''Public Function ToDataTable(ByVal items As List(Of T)) As DataTable
    ''    Dim dataTable As DataTable = New DataTable(GetType(T).Name)
    ''    'Get all the properties
    ''    Dim Props() As PropertyInfo = GetType(T).GetProperties((BindingFlags.Public Or BindingFlags.Instance))
    ''    For Each prop As PropertyInfo In Props
    ''        'Setting column names as Property names
    ''        dataTable.Columns.Add(prop.Name)
    ''    Next
    ''    For Each item As T In items
    ''        Dim values As var = New Object((Props.Length) - 1) {}
    ''        Dim i As Integer = 0
    ''        Do While (i < Props.Length)
    ''            'inserting property values to datatable rows
    ''            values(i) = Props(i).GetValue(item, Nothing)
    ''            i = (i + 1)
    ''        Loop
    ''        dataTable.Rows.Add(values)
    ''    Next
    ''    'put a breakpoint here and check datatable
    ''    Return dataTable
    ''End Function

    'Public Function EQToDataTable(ByVal parIList As System.Collections.IEnumerable) As System.Data.DataTable
    '    Dim ret As New System.Data.DataTable()
    '    Try
    '        Dim ppi As System.Reflection.PropertyInfo() = Nothing
    '        If parIList Is Nothing Then Return ret
    '        For Each itm As Object In parIList
    '            If ppi Is Nothing Then
    '                ppi = DirectCast(itm.[GetType](), System.Type).GetProperties()
    '                For Each pi As System.Reflection.PropertyInfo In ppi
    '                    Dim colType As System.Type = pi.PropertyType
    '                    If (colType.IsGenericType) AndAlso
    '                       (colType.GetGenericTypeDefinition() Is GetType(System.Nullable(Of ))) Then colType = colType.GetGenericArguments()(0)
    '                    ret.Columns.Add(New System.Data.DataColumn(pi.Name, colType))
    '                Next
    '            End If
    '            Dim dr As System.Data.DataRow = ret.NewRow
    '            For Each pi As System.Reflection.PropertyInfo In ppi
    '                dr(pi.Name) = If(pi.GetValue(itm, Nothing) Is Nothing, DBNull.Value, pi.GetValue(itm, Nothing))
    '            Next
    '            ret.Rows.Add(dr)
    '        Next
    '        For Each c As System.Data.DataColumn In ret.Columns
    '            c.ColumnName = c.ColumnName.Replace("_", " ")
    '        Next
    '    Catch ex As Exception
    '        ret = New System.Data.DataTable()
    '    End Try
    '    Return ret
    'End Function


    ''Public Function EQToShapefile(ByVal parIList As System.Collections.IEnumerable) As MapWinGIS.Shapefile
    ''    Dim ret As New MapWinGIS.Shapefile
    ''    Try
    ''        Dim ppi As System.Reflection.PropertyInfo() = Nothing
    ''        If parIList Is Nothing Then Return ret
    ''        For Each itm As Object In parIList
    ''            If ppi Is Nothing Then
    ''                ppi = DirectCast(itm.[GetType](), System.Type).GetProperties()
    ''                For Each pi As System.Reflection.PropertyInfo In ppi
    ''                    Dim colType As System.Type = pi.PropertyType
    ''                    If (colType.IsGenericType) AndAlso
    ''                       (colType.GetGenericTypeDefinition() Is GetType(System.Nullable(Of ))) Then colType = colType.GetGenericArguments()(0)
    ''                    ret.Columns.Add(New System.Data.DataColumn(pi.Name, colType))

    ''                Next
    ''            End If
    ''            Dim dr As System.Data.DataRow = ret.NewRow
    ''            For Each pi As System.Reflection.PropertyInfo In ppi
    ''                dr(pi.Name) = If(pi.GetValue(itm, Nothing) Is Nothing, DBNull.Value, pi.GetValue(itm, Nothing))
    ''            Next
    ''            ret.Rows.Add(dr)
    ''        Next
    ''        For Each c As System.Data.DataColumn In ret.Columns
    ''            c.ColumnName = c.ColumnName.Replace("_", " ")
    ''        Next
    ''    Catch ex As Exception
    ''        ret = New MapWinGIS.Shapefile
    ''    End Try
    ''    Return ret
    ''End Function




    'Private Function GEM_Object_To_Shapefile() As MapWinGIS.Shapefile
    '    '
    '    ' Name: GEM_Object_Shapefile
    '    ' Purpose: To Create a Point shapefile with the same structure as GEM_OBJECT.
    '    ' Written: K. Adlam, 09/08/12
    '    '
    '    ' Create point shapefile
    '    '
    '    memoryShape = New MapWinGIS.Shapefile
    '    memoryShape.CreateNew("", MapWinGIS.ShpfileType.SHP_POINT)
    '    'memoryShape.FastMode = True
    '    'memoryShape.CacheExtents = True
    '    Try
    '        '
    '        ' Start editing
    '        '
    '        memoryShape.StartEditingShapes(True)
    '        memoryShape.EditInsertField(MakeField("OBJECT_UID", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("PROJECT_UID", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("OBJECT_SCOPE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("X", MapWinGIS.FieldType.DOUBLE_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("Y", MapWinGIS.FieldType.DOUBLE_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("EPSG_CODE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("SOURCE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("OBJECT_COMMENT", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("DATE_MADE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("USER_MADE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("DATE_CHANGE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        memoryShape.EditInsertField(MakeField("USER_CHANGE", MapWinGIS.FieldType.STRING_FIELD), memoryShape.NumFields)
    '        '
    '        ' Query the object table
    '        '
    '        Dim gemObjectQuery As ObjectQuery(Of GEM_OBJECT) = _
    '        From gemObj In gemdb.Database.GEM_OBJECT _
    '        Select gemObj
    '        '
    '        ' Loop through the GEM objects
    '        '
    '        Dim irow As Long = 0
    '        For Each gemObject As GEM_OBJECT In gemObjectQuery
    '            '
    '            ' Add shape
    '            '
    '            If (Not (gemObject.X Is Nothing Or gemObject.Y Is Nothing)) Then
    '                Dim shp As MapWinGIS.Shape = MakePointShape(gemObject.X, gemObject.Y)
    '                memoryShape.EditInsertShape(shp, memoryShape.NumShapes) 'Insert shape
    '                '
    '                ' add attribute fields
    '                '
    '                memoryShape.EditCellValue(0, irow, gemObject.OBJECT_UID)
    '                memoryShape.EditCellValue(1, irow, gemObject.PROJECT_UID)
    '                memoryShape.EditCellValue(2, irow, gemObject.OBJECT_SCOPE)
    '                memoryShape.EditCellValue(3, irow, gemObject.X)
    '                memoryShape.EditCellValue(4, irow, gemObject.Y)
    '                memoryShape.EditCellValue(5, irow, gemObject.EPSG_CODE)
    '                memoryShape.EditCellValue(6, irow, gemObject.SOURCE)
    '                memoryShape.EditCellValue(7, irow, gemObject.OBJECT_COMMENT)
    '                memoryShape.EditCellValue(8, irow, gemObject.DATE_MADE)
    '                memoryShape.EditCellValue(9, irow, gemObject.USER_MADE)
    '                memoryShape.EditCellValue(10, irow, gemObject.DATE_CHANGE)
    '                memoryShape.EditCellValue(11, irow, gemObject.USER_CHANGE)
    '                irow = irow + 1
    '            End If
    '        Next
    '        '
    '        'Stop editing the shapefile and save changes
    '        '
    '        memoryShape.StopEditingShapes(True, True)


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Return memoryShape
    '    '
    'End Function

    'Function MakeField(ByVal strName As String, ByVal pType As MapWinGIS.FieldType) As MapWinGIS.Field
    '    Dim pField As New MapWinGIS.Field
    '    pField.Name = strName
    '    pField.Type = pType
    '    Return pField
    'End Function


    'Public Function CreateShpFromDataTable(ByVal dt As DataTable, ByVal XField As String, ByVal YField As String, ByVal strPath As String) As MapWinGIS.Shapefile
    '    '
    '    ' Name: CreateShpFromDataTable
    '    ' Purpose: To Create a Point shapefile from a DataTable.
    '    '       The names of the X and Y fields are passed as parameters.
    '    ' Written: K. Adlam, 06/12/11
    '    '
    '    ' Create point shapefile and start editing
    '    '
    '    Dim sf As New MapWinGIS.Shapefile
    '    Try
    '        '
    '        ' Create New shapefile
    '        '
    '        sf.CreateNew(strPath, MapWinGIS.ShpfileType.SHP_POINT)
    '        sf.StartEditingShapes(True)
    '        '
    '        'Loop each field in datatable and add to new shapefile
    '        '
    '        For Each col As DataColumn In dt.Columns
    '            Dim fld As New MapWinGIS.Field
    '            fld.Name = col.ColumnName
    '            '
    '            ' Set data type
    '            '
    '            Dim strDataType As String = col.DataType.Name.ToUpper()
    '            Select Case strDataType
    '                Case "STRING"
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '                Case "SINGLE"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "DOUBLE"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "DECIMAL"
    '                    fld.Type = MapWinGIS.FieldType.DOUBLE_FIELD
    '                Case "INT16"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "INT32"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "INT64"
    '                    fld.Type = MapWinGIS.FieldType.INTEGER_FIELD
    '                Case "DATETIME"
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '                Case Else
    '                    fld.Type = MapWinGIS.FieldType.STRING_FIELD
    '            End Select
    '            '
    '            ' Add Field to shapefile
    '            '
    '            sf.EditInsertField(fld, sf.NumFields) 'Insert all fields into shapefile
    '        Next col
    '        '
    '        ' get index to x and y fields
    '        '
    '        Dim ixcolumn As Integer = dt.Columns.Item(XField).Ordinal
    '        Dim iycolumn As Integer = dt.Columns.Item(YField).Ordinal
    '        '
    '        ' Copy the rows from the datatable to the shapefile
    '        '
    '        For irow As Long = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(irow)
    '            '
    '            ' Add shape
    '            '
    '            Dim shp As MapWinGIS.Shape = MakePointShape(dr(ixcolumn), dr(iycolumn))
    '            sf.EditInsertShape(shp, sf.NumShapes) 'Insert shapes
    '            '
    '            ' add attribute fields
    '            '
    '            For field As Integer = 0 To dt.Columns.Count - 1
    '                If (Not IsDBNull(dr(field))) Then
    '                    sf.EditCellValue(field, irow, CStr(dr(field)))
    '                End If
    '            Next
    '        Next
    '        '
    '        'Stop editing the shapefile and save changes
    '        '
    '        sf.StopEditingShapes(True, True)


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Return sf
    '    '
    'End Function

    'Function MakePointShape(ByVal x As Double, ByVal y As Double) As MapWinGIS.Shape
    '    '
    '    ' Name: MakePointShape
    '    ' Purpose: To create a point shape
    '    ' Written: K. Adlam, 18/11/11
    '    '
    '    Dim pnt As New MapWinGIS.Point()
    '    pnt.x = x
    '    pnt.y = y
    '    Dim shp As New MapWinGIS.Shape()
    '    shp.Create(MapWinGIS.ShpfileType.SHP_POINT)
    '    shp.InsertPoint(pnt, 0)
    '    Return shp
    '    ''
    'End Function

    ''Sub AddPointToShapefile(ByVal uuid As String, ByVal x As Double, ByVal y As Double, ByVal sf As MapWinGIS.Shapefile)
    ''    '
    ''    ' Name: AddPointToShapefile
    ''    ' Purpose: To add a poit feature to an ESRI shapefile
    ''    ' Written: K. Adlam, 23/11/11
    ''    '
    ''    '
    ''    ' create shape from x,y
    ''    '
    ''    Dim shp As MapWinGIS.Shape = MakePointShape(x, y) ' create shape
    ''    sf.EditInsertShape(shp, sf.NumShapes) 'Insert shape
    ''    '
    ''    ' add attribute
    ''    '
    ''    AddAttribute(sf, "UUID", sf.NumShapes, uuid)
    ''    AddAttribute(sf, "X", sf.NumShapes, x)
    ''    AddAttribute(sf, "Y", sf.NumShapes, y)
    ''    '
    ''End Sub

    ''Sub AddRecordToDatabaseTable(ByVal uuid As String, ByVal x As Double, ByVal y As Double, ByVal strDatabase As String)
    ''    '
    ''    ' Name: AddRecordToDatabaseTable
    ''    ' Purpose: To add a record to sqlite database table
    ''    ' Written: K. Adlam, 18/11/11
    ''    '
    ''    Dim SQLconnect As New SQLite.SQLiteConnection()
    ''    SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
    ''    SQLconnect.Open()
    ''    Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
    ''    SQLcommand.CommandText = "INSERT INTO test (uuid,x,y) VALUES (uuid,x,y)"
    ''    SQLcommand.ExecuteNonQuery()
    ''    SQLcommand.Dispose()
    ''    SQLconnect.Close()
    ''    ''
    ''End Sub

    ''Sub AddAttribute(ByVal sf As MapWinGIS.Shapefile, ByVal strField As String, ByVal rec As Long, ByVal theValue As Object)
    ''    '
    ''    ' Name: AddAttribute
    ''    ' Purpose: To add a single attribute value to shapefile
    ''    ' Written: K. Adlam, 18/11/11
    ''    '
    ''    Dim iField As Long = GetFieldIndex(sf, strField)
    ''    sf.EditCellValue(iField, rec, theValue)
    ''    '
    ''End Sub



    ''Public Function EntityToDatatable(ByVal Result As IQueryable, ByVal Ctx As ObjectContext) As DataTable
    ''    Try
    ''        Using SQLCon As New SqlConnection(CType(Ctx.Connection, EntityConnection).StoreConnection.ConnectionString)
    ''            Using Cmd As New SqlCommand(CType(Result, ObjectQuery).ToTraceString, SQLCon)
    ''                For Each Param As ObjectParameter In CType(Result, ObjectQuery).Parameters
    ''                    Cmd.Parameters.AddWithValue(Param.Name, Param.Value)
    ''                Next
    ''                Using DA As New SqlDataAdapter(Cmd)
    ''                    Using DT As New DataTable
    ''                        DA.Fill(DT)
    ''                        Return DT
    ''                    End Using
    ''                End Using
    ''            End Using
    ''        End Using
    ''    Catch
    ''        Throw
    ''    End Try
    ''End Function

    ''Public Sub EntityToDatatable(ByVal Result As IQueryable, ByVal Ctx As ObjectContext, ByVal DT As DataTable)
    ''    Try
    ''        Using SQLCon As New SqlConnection(CType(Ctx.Connection, EntityConnection).StoreConnection.ConnectionString)
    ''            Using Cmd As New SqlCommand(CType(Result, ObjectQuery).ToTraceString, SQLCon)
    ''                For Each Param As ObjectParameter In CType(Result, ObjectQuery).Parameters
    ''                    Cmd.Parameters.AddWithValue(Param.Name, Param.Value)
    ''                Next
    ''                Using DA As New SqlDataAdapter(Cmd)
    ''                    DA.Fill(DT)
    ''                End Using
    ''            End Using
    ''        End Using
    ''    Catch
    ''        Throw
    ''    End Try
    ''End Sub

    ''Private Sub frmMedia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    ''End Sub

 

End Class

