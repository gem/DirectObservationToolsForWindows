Imports System.IO
Imports System.Data
Imports System.Data.Objects.DataClasses
Imports System.Data.SQLite
Imports System.Data.EntityClient
Imports System.Data.SqlClient
Imports System.Linq

Public Class GEMDatabase

    Public SQLiteConn As New SQLite.SQLiteConnection
    Private mDatabasePath As String
    Private mDataset As New GEMDataset
    Private mCurrentProjectUID As String
    Private mMediaPath As String

    Private mProjectAdapter As New GEMDatasetTableAdapters.GEM_PROJECTTableAdapter
    Private mObjectAdapter As New GEMDatasetTableAdapters.GEM_OBJECTTableAdapter
    Private mSettingsAdapter As New GEMDatasetTableAdapters.SETTINGSTableAdapter
    Private mFieldsAdapter As New GEMDatasetTableAdapters.DIC_FIELDSTableAdapter

    Public Property SettingsAdapter As GEMDatasetTableAdapters.SETTINGSTableAdapter
        Get
            Return mSettingsAdapter
        End Get
        Set(ByVal value As GEMDatasetTableAdapters.SETTINGSTableAdapter)
            mSettingsAdapter = value
        End Set
    End Property

    Public Property DatabasePath() As String
        Get
            Return mDatabasePath
        End Get
        Set(ByVal value As String)
            mDatabasePath = value
        End Set
    End Property

    Public Property MediaPath() As String
        Get
            Return mMediaPath
        End Get
        Set(ByVal value As String)
            mMediaPath = value
        End Set
    End Property


    Public Property Dataset() As GEMDataset
        Get
            Return mDataset
        End Get
        Set(ByVal value As GEMDataset)
            mDataset = value
        End Set
    End Property

    Public Property CurrentProjectUID() As String
        Get
            If mCurrentProjectUID = "" Then
                If Not getFirstProjectRecord() Is Nothing Then
                    mCurrentProjectUID = getFirstProjectRecord.PROJ_UID
                End If
                Return mCurrentProjectUID
            Else
                Return mCurrentProjectUID
            End If
        End Get
        Set(ByVal value As String)
            mCurrentProjectUID = value
        End Set
    End Property

    Public Sub New(ByVal strDBPath As String)

        mMediaPath = IO.Path.GetDirectoryName(strDBPath) & "\" & IO.Path.GetFileNameWithoutExtension(strDBPath) & "_gemmedia"

        'Create GEM database on disk if it does not exist
        If Not IO.File.Exists(strDBPath) Then
               If CreateNewGEMSQLiteDatabase(strDBPath) = False Then
                'Error created database
                Exit Sub
            Else
                If Not IO.Directory.Exists(mMediaPath) Then
                    IO.Directory.CreateDirectory(mMediaPath)
                End If
            End If
        End If

        mDatabasePath = strDBPath
        My.Settings.Item("GEMConnectionString") = "data source=" & mDatabasePath

        Call SetupAdapterConnections()

    End Sub

    Private Sub SetupAdapterConnections()
        mProjectAdapter.Connection.Open()

        Dim gemCommand As SQLiteCommand = mProjectAdapter.Connection.CreateCommand()
        gemCommand.CommandText = "PRAGMA foreign_keys = ON"
        gemCommand.ExecuteNonQuery()

        Call RefreshGEMDataTableContents()
    End Sub

    Public Sub RefreshGEMDataTableContents()
        mObjectAdapter.Fill(Me.Dataset.GEM_OBJECT)
        mProjectAdapter.Fill(Me.Dataset.GEM_PROJECT)
        mSettingsAdapter.Fill(Me.Dataset.SETTINGS)
        mFieldsAdapter.Fill(Me.Dataset.DIC_FIELDS)
    End Sub

    Public Function CreateNewGEMSQLiteDatabase(ByVal strDBPath As String) As Boolean

        Try
            ExecuteSQLFromFile(strDBPath, App.Path & "\LocalResources\sqlite\gem.sql")
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Sub ExecuteSQLFromFile(ByVal strDatabase As String, ByVal strFile As String)
        '
        ' Name: ExecuteSQLFromFile
        ' Purpose: To execute SQL commands stored in a textfile against the
        '       specified SQLite database
        '
        ' Written: K. Adlam, 23/11/11
        '
        If (IO.File.Exists(strFile)) Then
            Dim sr As StreamReader = New StreamReader(strFile)
            Dim strSQL As String = sr.ReadToEnd
            ExecuteSQLCommand(strDatabase, strSQL)
        End If
        '
    End Sub

    Public Sub ExecuteSQLFromFile(ByVal strFile As String)
        '
        ' Name: ExecuteSQLFromFile
        ' Purpose: To execute SQL commands stored in a textfile against the
        '       specified SQLite database
        '
        ' Written: K. Adlam, 23/11/11
        '
        If (IO.File.Exists(strFile)) Then
            Dim sr As StreamReader = New StreamReader(strFile)
            Dim strSQL As String = sr.ReadToEnd
            ExecuteSQLCommand(mDatabasePath, strSQL)
        End If
        '
    End Sub

    Public Function ExecuteSQLCommand(ByVal strDatabase As String, ByVal strCommand As String) As Long
        '
        ' Name: ExecuteSQLCommand
        ' Purpose: To execute any Non-Query command against the specified database
        '       The number of rows affected is returned
        ' Written: K. Adlam, 22/11/11
        '
        Dim SQLconnect As New SQLite.SQLiteConnection()
        SQLconnect.ConnectionString = "Data Source=" & strDatabase & ";"
        SQLconnect.Open()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = strCommand
        Dim irows As Long = SQLcommand.ExecuteNonQuery()
        SQLcommand.Dispose()
        SQLconnect.Close()
        Return irows
        '
    End Function

    Public Function ExecuteSQLCommand(ByVal strCommand As String) As Long
        '
        ' Name: ExecuteSQLCommand
        ' Purpose: To execute any Non-Query command against the specified database
        '       The number of rows affected is returned
        ' Written: K. Adlam, 22/11/11
        '
        Dim SQLconnect As New SQLite.SQLiteConnection()
        SQLconnect.ConnectionString = "Data Source=" & mDatabasePath & ";"
        SQLconnect.Open()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = strCommand
        Dim irows As Long = SQLcommand.ExecuteNonQuery()
        SQLcommand.Dispose()
        SQLconnect.Close()
        Return irows
        '
    End Function


    Public Sub addUser(ByVal name As String)

        Dim sett As GEMDataset.SETTINGSRow = (From proj In gemdb.Dataset.SETTINGS Select proj).FirstOrDefault
        If sett Is Nothing Then
            Dim row As GEMDataset.SETTINGSRow = Me.Dataset.SETTINGS.NewSETTINGSRow
            row.KEY = "CURRENT_USER"
            row.VALUE = name
            Me.Dataset.SETTINGS.Rows.Add(row)
            mSettingsAdapter.Update(gemdb.Dataset.SETTINGS)
        Else
            sett.VALUE = name
            mSettingsAdapter.Update(gemdb.Dataset.SETTINGS)
        End If
        RefreshGEMDataTableContents()

    End Sub

    Public Sub addProject(ByVal name As String, ByVal summary As String, ByVal dateval As Date)

        Dim sett As GEMDataset.GEM_PROJECTRow = (From proj In gemdb.Dataset.GEM_PROJECT Select proj).FirstOrDefault
        If sett Is Nothing Then
            Dim row As GEMDataset.GEM_PROJECTRow = gemdb.Dataset.GEM_PROJECT.NewGEM_PROJECTRow
            mCurrentProjectUID = System.Guid.NewGuid.ToString
            row.PROJ_UID = mCurrentProjectUID
            row.PROJ_NAME = name
            row.PROJ_SUMRY = summary
            row.PROJ_DATE = dateval
            row.HAZRD_TYPE = "SEISM"
            Me.Dataset.GEM_PROJECT.Rows.Add(row)
            mProjectAdapter.Update(gemdb.Dataset.GEM_PROJECT)
        Else
            mCurrentProjectUID = sett.PROJ_UID
            sett.PROJ_NAME = name
            sett.PROJ_SUMRY = summary
            sett.PROJ_DATE = dateval
            sett.HAZRD_TYPE = "SEISM"
            mProjectAdapter.Update(gemdb.Dataset.GEM_PROJECT)
        End If
        RefreshGEMDataTableContents()

    End Sub


    'Public Sub addObject(ByVal name As String, ByVal summary As String, ByVal dateval As Date)

    '    Dim row As GEMDataset.GEM_OBJECTRow = Me.Dataset.GEM_OBJECT.NewGEM_OBJECTRow
    '    row.PROJ_UID = mCurrentProjectUID
    '    row.OBJ_UID = System.Guid.NewGuid.ToString
    '    Me.Dataset.GEM_OBJECT.Rows.Add(row)
    '    mObjectAdapter.Update(Me.Dataset.GEM_OBJECT)

    'End Sub

    Public Function getFirstUserFromSettingsTable() As String
        getFirstUserFromSettingsTable = ""

        Call RefreshGEMDataTableContents()

        If (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "CURRENT_USER" Select setting.VALUE).Count > 0 Then
            getFirstUserFromSettingsTable = (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "CURRENT_USER" Select setting.VALUE).First
        End If

        Return getFirstUserFromSettingsTable
    End Function

    Public Function getFirstProjectRecord() As GEMDataset.GEM_PROJECTRow
        getFirstProjectRecord = Nothing

        Call RefreshGEMDataTableContents()
        getFirstProjectRecord = (From proj In gemdb.Dataset.GEM_PROJECT Select proj).FirstOrDefault

        Return getFirstProjectRecord
    End Function


End Class
