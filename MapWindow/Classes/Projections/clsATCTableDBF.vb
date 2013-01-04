Option Strict Off
Option Explicit On
Imports MapWinUtility
Friend Class clsATCTableDBF
    Inherits clsATCTable

    '===========================================================================
    ' Subject: READ DBASE III                    Date: 1/25/88 (00:00)
    ' Author:  David Perry                       Code: QB, PDS
    ' Keys:    READ,DBASE,III                  Packet: MISC.ABC
    '===========================================================================

    'This QB source was adjusted for use with VB by Robert Smith
    'on June 14, 1999, source was provided to Smith by Marc Hoogerwerf
    'contact Smith via: www.smithvoice.com/vbfun.htm

    'This code was turned into a class by Mark Gray at Aqua Terra March 14, 2001
    'modification and extensions continue through 2003

    'Converted to VB.NET by Mark Gray at Aqua Terra October 2004

    'dBaseIII file header, 32 bytes
    Private Class clsHeader
        Public version As Byte
        Public dbfYear As Byte
        Public dbfMonth As Byte
        Public dbfDay As Byte
        Public NumRecs As Integer
        Public NumBytesHeader As Short
        Public NumBytesRec As Short
        Public Trash(19) As Byte

        Public Sub ReadFromFile(ByVal inFile As Short)
            FileGet(inFile, version)
            FileGet(inFile, dbfYear)
            FileGet(inFile, dbfMonth)
            FileGet(inFile, dbfDay)
            FileGet(inFile, NumRecs)
            FileGet(inFile, NumBytesHeader)
            FileGet(inFile, NumBytesRec)
            FileGet(inFile, Trash)
        End Sub

        Public Sub WriteToFile(ByVal outFile As Short)
            FilePut(outFile, version)
            FilePut(outFile, dbfYear)
            FilePut(outFile, dbfMonth)
            FilePut(outFile, dbfDay)
            FilePut(outFile, NumRecs)
            FilePut(outFile, NumBytesHeader)
            FilePut(outFile, NumBytesRec)
            FilePut(outFile, Trash)
        End Sub
    End Class


    'Field Descriptions, 32 bytes * Number of Fields
    'Up to 128 Fields
    Private Class clsFieldDescriptor
        Public FieldName As String    '10 character limit, 11th char is 0 in DBF file
        Public FieldType As String    'C = Character, D = Date, N = Numeric, L = Logical, M = Memo
        Public DataAddress As Integer 'offset from record start to field start
        Public FieldLength As Byte    'Byte type limits field size in DBF to 255 bytes
        Public DecimalCount As Byte   'Hint for number of decimal places to display
        Public Trash(13) As Byte      'Extra bytes in field descriptor that are not used

        Public Sub ReadFromFile(ByVal inFile As Short)
            Dim buf As Byte() : ReDim buf(11)
            FileGet(inFile, buf) 'Get field name plus type character
            FieldName = System.Text.ASCIIEncoding.ASCII.GetString(buf, 0, 11)
            FieldType = Chr(buf(11))
            FileGet(inFile, DataAddress)
            FileGet(inFile, FieldLength)
            FileGet(inFile, DecimalCount)
            FileGet(inFile, Trash)
        End Sub

        Public Sub WriteToFile(ByVal outFile As Short)
            Dim buf As Byte() : ReDim buf(11)
            buf = System.Text.ASCIIEncoding.ASCII.GetBytes(FieldName)
            If buf.Length() <> 12 Then ReDim Preserve buf(11)
            buf(10) = 0
            buf(11) = Asc(FieldType)
            FilePut(outFile, buf)
            'FilePut(outFile, DataAddress)
            FilePut(outFile, CInt(0)) 'DataAddress = 0 'Nobody seems to leave non-zero values in file
            FilePut(outFile, FieldLength)
            FilePut(outFile, DecimalCount)
            FilePut(outFile, Trash)
        End Sub
    End Class

    Private pFilename As String
    Private pHeader As clsHeader
    Private pFields() As clsFieldDescriptor
    Private pNumFields As Integer
    Private pData() As Byte
    Private pDataBytes As Integer
    Private pCurrentRecord As Integer
    Private pCurrentRecordStart As Integer

    'Capacity in pData for records. Set to pHeader.NumRecs when data is read from a file
    'and in InitData when creating a new DBF from scratch. May increase in Let Value.
    Private pNumRecsCapacity As Integer


    Public Property FieldDecimalCount(ByVal aFieldNumber As Integer) As Byte
        Get
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                FieldDecimalCount = pFields(aFieldNumber).DecimalCount
            Else
                FieldDecimalCount = 0
            End If
        End Get
        Set(ByVal Value As Byte)
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                pFields(aFieldNumber).DecimalCount = Value
            End If
        End Set
    End Property

    Public Overrides Property CurrentRecord() As Integer
        Get
            CurrentRecord = pCurrentRecord
        End Get
        Set(ByVal Value As Integer)
            Try
                If Value > pHeader.NumRecs Then NumRecords = Value
                If Value < 1 Or Value > pHeader.NumRecs Then
                    pCurrentRecord = 1
                Else
                    pCurrentRecord = Value
                End If
                pCurrentRecordStart = pHeader.NumBytesRec * (pCurrentRecord - 1) + 1
            Catch ex As Exception
                Logger.Msg("Cannot set CurrentRecord to " & Value & vbCr & ex.Message, "Let CurrentRecord")
            End Try
        End Set
    End Property


    Public Overrides Property FieldLength(ByVal aFieldNumber As Integer) As Integer
        Get
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                FieldLength = pFields(aFieldNumber).FieldLength
            Else
                FieldLength = 0
            End If
        End Get
        Set(ByVal Value As Integer)
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                pFields(aFieldNumber).FieldLength = Value
            End If
        End Set
    End Property

    'FieldName is a maximum of 10 characters long, padded to 11 characters with nulls

    Public Overrides Property FieldName(ByVal aFieldNumber As Integer) As String
        Get
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                FieldName = TrimNull(pFields(aFieldNumber).FieldName)
            Else
                FieldName = "Undefined"
            End If
        End Get
        Set(ByVal Value As String)
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                Value = Trim(Left(Value, 10))
                pFields(aFieldNumber).FieldName = Value & New String(Chr(0), 11 - Len(Value))
            End If
        End Set
    End Property


    'C = Character, D = Date, N = Numeric, L = Logical, M = Memo
    Public Overrides Property FieldType(ByVal aFieldNumber As Integer) As String
        Get
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                FieldType = pFields(aFieldNumber).FieldType
            Else
                FieldType = "Undefined"
            End If
        End Get
        Set(ByVal Value As String)
            If aFieldNumber > 0 And aFieldNumber <= pNumFields Then
                pFields(aFieldNumber).FieldType = Value
            End If
        End Set
    End Property

    Public Overrides Property NumFields() As Integer
        Get
            NumFields = pNumFields
        End Get
        Set(ByVal Value As Integer)
            Dim iField As Integer
            pNumFields = Value
            ReDim pFields(pNumFields)
            For iField = 1 To pNumFields
                pFields(iField) = New clsFieldDescriptor
                pFields(iField).FieldType = "C"
            Next
            pHeader.NumBytesHeader = (pNumFields + 1) * 32 + 1
        End Set
    End Property


    Public Overrides Property NumRecords() As Integer
        Get
            NumRecords = pHeader.NumRecs
        End Get
        Set(ByVal Value As Integer)
            Dim iBlank As Integer
            If Value > pHeader.NumRecs Then
                pHeader.NumRecs = Value
                iBlank = pDataBytes + 1
                If Value > pNumRecsCapacity Then
                    'Expand the data array capacity
                    pNumRecsCapacity = (Value + 1) * 1.5
                    ReDim Preserve pData(pNumRecsCapacity * pHeader.NumBytesRec)
                End If
                pDataBytes = pHeader.NumRecs * pHeader.NumBytesRec
                'fill all newly allocated bytes of data array with spaces
                While iBlank <= pDataBytes
                    pData(iBlank) = 32
                    iBlank = iBlank + 1
                End While
            ElseIf Value < pHeader.NumRecs Then
                'Shrink the data array
                pHeader.NumRecs = Value
                pDataBytes = pHeader.NumRecs * pHeader.NumBytesRec
                pNumRecsCapacity = Value
                ReDim Preserve pData(pDataBytes)
            End If
        End Set
    End Property


    Public Overrides Property Value(ByVal aFieldNumber As Integer) As String
        Get
            Dim FieldStart As Integer
            Dim I As Integer
            Dim strRet As String
            If pCurrentRecord < 1 Or pCurrentRecord > pHeader.NumRecs Then
                Value = "Invalid Current Record Number"
            ElseIf aFieldNumber < 1 Or aFieldNumber > pNumFields Then
                Value = "Invalid Field Number"
            Else
                FieldStart = pCurrentRecordStart + pFields(aFieldNumber).DataAddress

                strRet = ""
                For I = 0 To pFields(aFieldNumber).FieldLength - 1
                    If pData(FieldStart + I) > 0 Then
                        strRet = strRet & Chr(pData(FieldStart + I))
                    Else
                        I = 256
                    End If
                Next
                Value = Trim(strRet)
                '    If pFields(aFieldNumber).FieldType = "N" Then
                '      Dim dblval As Double
                '      dblval = CDbl(strRet)
                '      If pFields(aFieldNumber).DecimalCount <> 0 Then
                '        dblval = dblval * 10 ^ pFields(aFieldNumber).DecimalCount
                '      End If
                '      Value = dblval
                '    End If
            End If
        End Get
        Set(ByVal Value As String)
            Dim FieldStart As Integer
            Dim I As Integer
            Dim strRet As String
            Dim lenStr As Integer

            If pHeader.NumBytesRec = 0 Then InitData()

            Try
                If pCurrentRecord < 1 Then
                    'Value = "Invalid Current Record Number"
                ElseIf aFieldNumber < 1 Or aFieldNumber > pNumFields Then
                    'Value = "Invalid Field Number"
                Else
                    pData(pCurrentRecordStart) = 32 'clear record deleted flag or overwrite EOF

                    FieldStart = pCurrentRecordStart + pFields(aFieldNumber).DataAddress

                    strRet = Value
                    lenStr = Len(strRet)
                    If lenStr > pFields(aFieldNumber).FieldLength Then
                        strRet = Left(strRet, pFields(aFieldNumber).FieldLength)
                    ElseIf pFields(aFieldNumber).FieldType = "N" Then
                        strRet = Space(pFields(aFieldNumber).FieldLength - lenStr) & strRet
                    Else
                        strRet = strRet & Space(pFields(aFieldNumber).FieldLength - lenStr)
                    End If
                    For I = 0 To pFields(aFieldNumber).FieldLength - 1
                        pData(FieldStart + I) = Asc(Mid(strRet, I + 1, 1))
                    Next
                End If
            Catch ex As Exception
                Logger.Msg("Cannot set field #" & aFieldNumber & " = '" & Value & "' in record #" & pCurrentRecord & vbCr & ex.Message, "Let Value")
            End Try
        End Set
    End Property



    Public Function FindMatch(ByVal aFieldNum() As Integer, ByVal aOperation() As String, ByVal aFieldVal() As Object, Optional ByVal aMatchAny As Boolean = False, Optional ByVal aStartRecord As Integer = 1, Optional ByVal aEndRecord As Integer = -1) As Boolean
        Dim numRules As Integer
        Dim iRule As Integer
        Dim lValue As Object
        Dim allMatch As Boolean
        Dim thisMatches As Boolean
        Dim NotAtTheEnd As Boolean
        numRules = UBound(aFieldNum)

        If aEndRecord < 0 Then aEndRecord = pHeader.NumRecs

        'If we are supposed to look for matches only in records that don't exist, we won't find any
        If aStartRecord > pHeader.NumRecs Then
            FindMatch = False
            Exit Function
        End If

        CurrentRecord = aStartRecord
        NotAtTheEnd = True
        While NotAtTheEnd And CurrentRecord <= aEndRecord
            iRule = 1
            allMatch = True
            While iRule <= numRules And allMatch
                thisMatches = False
                lValue = Value(aFieldNum(iRule))
                Select Case aoperation(iRule)
                    Case "="
                        If lValue = aFieldVal(iRule) Then thisMatches = True
                    Case "<"
                        If lValue < aFieldVal(iRule) Then thisMatches = True
                    Case ">"
                        If lValue > aFieldVal(iRule) Then thisMatches = True
                    Case "<="
                        If lValue <= aFieldVal(iRule) Then thisMatches = True
                    Case ">="
                        If lValue >= aFieldVal(iRule) Then thisMatches = True
                    Case Else : MapWinUtility.Logger.Dbg("DEBUG: " + "Unrecognized operation:" & aOperation(iRule))
                End Select
                If aMatchAny Then
                    If thisMatches Then
                        FindMatch = True
                        Exit Function '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    End If
                Else
                    If Not thisMatches Then
                        allMatch = False
                    End If
                End If
                iRule = iRule + 1
            End While
            If allMatch And Not aMatchAny Then
                FindMatch = True
                Exit Function '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            End If
            If pCurrentRecord < pHeader.NumRecs Then
                MoveNext()
            Else
                NotAtTheEnd = False
            End If
        End While
        CurrentRecord = aStartRecord
        FindMatch = False
    End Function

    'Dimension and initialize data buffer to all spaces (except for initial carriage return)
    'Do not call on an existing DBF since all data will be removed from memory
    'If creating a new DBF:
    ' Call after setting NumRecords, NumFields and all FieldLength
    ' Call before setting any Value
    Public Sub InitData()
        Dim I As Integer

        SetDataAddresses()

        pHeader.NumBytesRec = pFields(pNumFields).DataAddress + pFields(pNumFields).FieldLength

        pNumRecsCapacity = pHeader.NumRecs
        pDataBytes = pHeader.NumRecs * pHeader.NumBytesRec
        ReDim pData(pDataBytes)
        pData(0) = 13
        For I = 1 To pDataBytes
            pData(I) = 32
        Next
    End Sub

    Private Sub SetDataAddresses()
        Dim I As Integer
        pFields(1).DataAddress = 1
        For I = 2 To pNumFields
            pFields(I).DataAddress = pFields(I - 1).DataAddress + pFields(I - 1).FieldLength
        Next
    End Sub


    Private Function TrimNull(ByVal Value As String) As String
        Dim nullPos As Integer
        nullPos = InStr(Value, Chr(0))
        If nullPos = 0 Then
            TrimNull = Trim(Value)
        Else
            TrimNull = Trim(Left(Value, nullPos - 1))
        End If
    End Function

    Public Sub New()
        MyBase.New()
        ' Set up our templates for comparing arrays
        'pLongHeader1(0) = 1 ' Number of dimensions
        'pLongHeader1(1) = 4 ' Bytes per element (long = 4)
        'pLongHeader1(4) = &H7FFFFFFF ' Array size

        'pLongHeader2(0) = 1 ' Number of dimensions
        'pLongHeader2(1) = 4 ' Bytes per element (long = 4)
        'pLongHeader2(4) = &H7FFFFFFF ' Array size

        Clear()
    End Sub

    Public Overrides Sub Clear()
        ClearData()
        pHeader.version = 3
        pHeader.dbfDay = 1
        pHeader.dbfMonth = 1
        pHeader.dbfYear = 70
        pHeader.NumBytesHeader = 32
        pHeader.NumBytesRec = 0
        pNumFields = 0
        ReDim pFields(0)
    End Sub

    Public Overrides Sub ClearData()
        If pHeader Is Nothing Then pHeader = New clsHeader
        pHeader.NumRecs = 0
        pDataBytes = 0
        pCurrentRecord = 1
        pCurrentRecordStart = 0
        pNumRecsCapacity = 0
        ReDim pData(0)
    End Sub

    Public Overrides Function Cousin() As IATCTable
        Dim iField As Integer
        Dim newDBF As New clsATCTableDBF
        With newDBF
            '    .Year = CInt(Format(Now, "yyyy")) - 1900
            '    .Month = CByte(Format(Now, "mm"))
            '    .Day = CByte(Format(Now, "dd"))
            .NumFields = pNumFields

            For iField = 1 To pNumFields
                .FieldName(iField) = FieldName(iField)
                .FieldType(iField) = FieldType(iField)
                .FieldLength(iField) = FieldLength(iField)
                .FieldDecimalCount(iField) = FieldDecimalCount(iField)
            Next
        End With
        Return newDBF
    End Function

    Public Overrides Function CreationCode() As String
        Dim retval As String
        Dim iField As Integer

        retval = "Dim newDBF as clsDBF"
        retval &= vbCrLf & "set newDBF = new clsDBF"
        retval &= vbCrLf & "With newDBF"

        retval &= vbCrLf & "  .Year = CInt(Format(Now, ""yyyy"")) - 1900"
        retval &= vbCrLf & "  .Month = CByte(Format(Now, ""mm""))"
        retval &= vbCrLf & "  .Day = CByte(Format(Now, ""dd""))"
        retval &= vbCrLf & "  .NumFields = " & pNumFields
        retval &= vbCrLf

        For iField = 1 To pNumFields
            With pFields(iField)
                retval &= vbCrLf & "  .FieldName(" & iField & ") = """ & TrimNull(.FieldName) & """"
                retval &= vbCrLf & "  .FieldType(" & iField & ") = """ & .FieldType & """"
                retval &= vbCrLf & "  .FieldLength(" & iField & ") = " & .FieldLength
                retval &= vbCrLf & "  .FieldDecimalCount(" & iField & ") = " & .DecimalCount
                retval &= vbCrLf
            End With
        Next
        retval &= vbCrLf & "  '.NumRecords = " & pHeader.NumRecs
        retval &= vbCrLf & "  '.InitData"
        retval &= vbCrLf & "End With"
        retval &= vbCrLf
        Return retval
    End Function

    'Returns zero if the named field does not appear in this file
    Public Overrides Function FieldNumber(ByVal aFieldName As String) As Integer
        Dim retval As Integer
        For retval = 1 To pNumFields
            If TrimNull(pFields(retval).FieldName) = aFieldName Then
                FieldNumber = retval
                Exit Function
            End If
        Next
    End Function

    Public Overrides Function OpenFile(ByVal aFilename As String) As Boolean
        'Dim header As clsHeader, FieldDes As clsFieldDescriptor    'Creating variables for user-defined types
        'Dim memo As String * 512                               'Create a 512 byte fixed string variable
        ' to read memo fields
        Dim inFile As Short
        Dim I As Integer

        If Not IO.File.Exists(aFilename) Then
            Return False 'can't open a file that doesn't exist
        End If

        pFilename = aFilename

        inFile = FreeFile()
        FileOpen(inFile, aFilename, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        pHeader.ReadFromFile(inFile)
        Select Case pHeader.version 'Be sure we're using a dBASE III file
            Case 3 'Normal dBASEIII file
                '   Case &H83 'Open a .DBT file
            Case Else
                Logger.Msg("This is not a dBASE III file: '" & aFilename & "'", "OpenDBF")
                FileClose(inFile)
                Return False
        End Select

        NumFields = pHeader.NumBytesHeader \ 32 - 1 'Calculate the number of fields

        For I = 1 To pNumFields
            pFields(I).ReadFromFile(inFile) 'Looping through NumFields by reading in 32 byte records
        Next I

        SetDataAddresses()

        pDataBytes = LOF(inFile) - pHeader.NumBytesHeader  'Adding one seems to help with some files
        ReDim pData(pDataBytes)
        FileGet(inFile, pData)
        pNumRecsCapacity = pHeader.NumRecs
        FileClose(inFile)
        If pHeader.NumRecs > 0 Then
            MoveFirst()
        Else
            pCurrentRecord = 0
        End If
        Return True
    End Function

    Public Overrides Function SummaryFields(Optional ByVal aFormat As String = "tab,headers,expandtype") As String
        Dim retval As String = ""
        Dim iTrash As Integer
        Dim iField As Integer
        Dim ShowTrash As Boolean
        Dim ShowHeaders As Boolean
        Dim ExpandType As Boolean

        If InStr(LCase(aFormat), "trash") > 0 Then ShowTrash = True
        If InStr(LCase(aFormat), "headers") > 0 Then ShowHeaders = True
        If InStr(LCase(aFormat), "expandtype") > 0 Then ExpandType = True

        If InStr(LCase(aFormat), "text") > 0 Then 'text version
            For iField = 1 To pNumFields
                With pFields(iField)
                    retval &= vbCrLf & "Field " & iField & ": '" & TrimNull(.FieldName) & "'"
                    retval &= vbCrLf & "    Type: " & .FieldType & " "
                    If ExpandType Then
                        Select Case .FieldType
                            Case "C" : retval &= "Character"
                            Case "D" : retval &= "Date     "
                            Case "N" : retval &= "Numeric  "
                            Case "L" : retval &= "Logical  "
                            Case "M" : retval &= "Memo     "
                        End Select
                    Else
                        retval &= .FieldType
                    End If
                    retval &= vbCrLf & "    Length: " & .FieldLength & " "
                    retval &= vbCrLf & "    DecimalCount: " & .DecimalCount & " "
                    If ShowTrash Then
                        retval &= vbCrLf & "    Trash: "
                        For iTrash = 1 To 14
                            retval &= .Trash(iTrash) & " "
                        Next
                    End If
                End With
                retval &= vbCrLf
            Next
        Else 'table version
            If ShowHeaders Then
                retval &= "Field "
                retval &= vbTab & "Name "
                retval &= vbTab & "Type "
                retval &= vbTab & "Length "
                retval &= vbTab & "DecimalCount "
                If ShowTrash Then
                    For iTrash = 1 To 14
                        retval &= vbTab & "Trash" & iTrash
                    Next
                End If
            End If
            retval &= vbCrLf
            'now field details
            For iField = 1 To pNumFields
                With pFields(iField)
                    retval &= iField & vbTab & "'" & TrimNull(.FieldName) & "' "
                    If ExpandType Then
                        Select Case .FieldType
                            Case "C" : retval &= vbTab & "Character"
                            Case "D" : retval &= vbTab & "Date     "
                            Case "N" : retval &= vbTab & "Numeric  "
                            Case "L" : retval &= vbTab & "Logical  "
                            Case "M" : retval &= vbTab & "Memo     "
                        End Select
                    Else
                        retval &= vbTab & .FieldType
                    End If
                    retval &= vbTab & .FieldLength
                    retval &= vbTab & .DecimalCount
                    If ShowTrash Then
                        retval &= vbCrLf & "    Trash: "
                        For iTrash = 1 To 14
                            retval &= vbTab & .Trash(iTrash)
                        Next
                    End If
                End With
                retval &= vbCrLf
            Next
        End If
        Return retval
    End Function

    Public Overrides Function SummaryFile(Optional ByVal aFormat As String = "tab,headers") As String
        Dim retval As String = ""
        Dim iTrash As Integer
        Dim ShowTrash As Boolean
        Dim ShowHeaders As Boolean

        If InStr(LCase(aFormat), "trash") > 0 Then ShowTrash = True
        If InStr(LCase(aFormat), "headers") > 0 Then ShowHeaders = True

        If LCase(aFormat) = "text" Then 'text version
            With pHeader
                retval = "DBF Header: "
                retval &= vbCrLf & "    FileName: " & pFilename
                retval &= vbCrLf & "    Version: " & .version
                retval &= vbCrLf & "    Date: " & .dbfYear + 1900 & "/" & .dbfMonth & "/" & .dbfDay
                retval &= vbCrLf & "    NumRecs: " & .NumRecs
                retval &= vbCrLf & "    NumBytesHeader: " & .NumBytesHeader
                retval &= vbCrLf & "    NumBytesRec: " & .NumBytesRec
                If ShowTrash Then
                    retval &= vbCrLf & "    Trash: "
                    For iTrash = 1 To 20
                        retval &= pHeader.Trash(iTrash) & " "
                    Next
                End If
            End With
        Else 'table version
            'build header header
            If ShowHeaders Then
                retval = "FileName "
                retval &= vbTab & "Version "
                retval &= vbTab & "Date "
                retval &= vbTab & "NumFields "
                retval &= vbTab & "NumRecs "
                retval &= vbTab & "NumBytesHeader "
                retval &= vbTab & "NumBytesRec "
            End If
            If ShowTrash Then
                For iTrash = 0 To 19
                    retval &= vbTab & "Trash" & iTrash
                Next
            End If
            retval &= vbCrLf
            With pHeader 'now header data
                retval &= pFilename
                retval &= vbTab & .version
                retval &= vbTab & .dbfYear + 1900 & "/" & .dbfMonth & "/" & .dbfDay
                retval &= vbTab & pNumFields
                retval &= vbTab & .NumRecs
                retval &= vbTab & .NumBytesHeader
                retval &= vbTab & .NumBytesRec
                If ShowTrash Then
                    For iTrash = 0 To 19
                        retval &= vbTab & pHeader.Trash(iTrash)
                    Next
                End If
                retval &= vbCrLf
            End With
        End If
        SummaryFile = retval
    End Function

    Public Overrides Function WriteFile(ByVal aFilename As String) As Boolean
        Dim OutFile As Short
        Dim lField As Integer
TryAgain:
        Try
            If IO.File.Exists(aFilename) Then
                Kill(aFilename)
            Else
                IO.Directory.CreateDirectory((IO.Path.GetDirectoryName(aFilename)))
            End If

            OutFile = FreeFile()
            FileOpen(OutFile, aFilename, OpenMode.Binary)
            pHeader.WriteToFile(OutFile)

            For lField = 1 To pNumFields
                pFields(lField).WriteToFile(OutFile) 'FilePutObject(OutFile, pFields(I), (32 * I) + 1)
            Next

            'If we have over-allocated for adding more records, trim unused records
            If pNumRecsCapacity > pHeader.NumRecs Then
                pNumRecsCapacity = pHeader.NumRecs
                ReDim Preserve pData(pHeader.NumRecs * pHeader.NumBytesRec)
            End If

            FilePut(OutFile, pData)
            FileClose(OutFile)

            pFilename = aFilename
            Return True

        Catch ex As Exception
            If Logger.Msg("Error saving " & aFilename & vbCr & ex.Message, "Write DBF", MsgBoxStyle.AbortRetryIgnore) = MsgBoxResult.Retry Then
                Try
                    FileClose(OutFile)
                Catch
                    'ignore error if file cannot be closed
                End Try
                GoTo TryAgain
            End If
            Return False
        End Try
    End Function
End Class
