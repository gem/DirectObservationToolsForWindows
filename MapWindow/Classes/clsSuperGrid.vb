Imports System.IO

<CLSCompliant(False)> _
Public Class SuperGrid
    Inherits MapWinGIS.GridClass

    Public Structure sVertex
        Dim X As Double
        Dim Y As Double
    End Structure

    Private Structure sDEMData
        Dim HorizUnits As Integer
        Dim VertUnits As Integer
        Dim Notes As String
        Dim Vertices() As sVertex
        Dim Min As Double
        Dim Max As Double
        Dim NumCols As Integer
        Dim Values(,) As Integer
        Dim ColStarts() As sVertex
        Dim NumElevs() As Integer
        Public Function MaxY() As Double
            Dim i As Integer, Y As Double
            Y = Vertices(0).Y
            For i = 0 To 3
                If Vertices(i).Y > Y Then
                    Y = Vertices(i).Y
                End If
            Next i
            Return Y
        End Function
        Public Function MaxX() As Double
            Dim i As Integer, X As Double
            X = Vertices(0).X
            For i = 0 To 3
                If Vertices(i).X > X Then
                    X = Vertices(i).X
                End If
            Next i
            Return X
        End Function
        Public Function MinX() As Double
            Dim i As Integer, X As Double
            X = Vertices(0).X
            For i = 0 To 3
                If Vertices(i).X < X Then
                    X = Vertices(i).X
                End If
            Next i
            Return X
        End Function
        Public Function MinY() As Double
            Dim i As Integer, Y As Double
            Y = Vertices(0).Y
            For i = 0 To 3
                If Vertices(i).Y < Y Then
                    Y = Vertices(i).Y
                End If
            Next i
            Return Y
        End Function

    End Structure

    Public Structure sFLTData
        Dim ncols As Integer        '2199
        Dim nrows As Integer        '1861
        Dim xllcenter As Double     '-120.36583333496
        Dim yllcenter As Double     '46.992222223902
        Dim cellsize As Double      '0.0002777777778
        Dim NODATA_value As Integer '-9999
        Dim byteorder As String     'LSBFIRST
        Dim Values(,) As Single
    End Structure

    Public Overrides Function SetInvalidValuesToNodata(ByVal MinThresholdValue As Double, ByVal MaxThresholdValue As Double) As Boolean
        Return MyBase.SetInvalidValuesToNodata(MinThresholdValue, MaxThresholdValue)
    End Function

    'use this for importing *.flt
    Private Function ImportFLTFormat(ByVal InFile As String, ByRef Callback As MapWinGIS.ICallback) As Boolean
        'Read the .FLT data stream and process it
        'Dan Ames April 2003
        Dim FLTData As New sFLTData()
        Try
            ReadHDRFile(FLTData, InFile.Substring(0, InFile.LastIndexOf(".")) & ".hdr")
            ReadFLTData(FLTData, InFile, Callback)
            MakeGrid(FLTData, InFile, Callback)
            Return True

        Catch ex As System.Exception
            mapwinutility.logger.msg("Error in ImportDEMFormat importing " & InFile & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    'Please don't comment this out - if this function is giving you trouble it means
    'your copy of MapWinGIS.ocx and/or Interop.MapWinGIS.dll and AxInterop.MapWinGIS.dll are out of date.
    'If you have any questions contact cmichaelis@happysquirrel.com
    Public Overrides Function Resource(ByVal newSrcPath As String) As Boolean
        Return MyBase.Resource(newSrcPath)
    End Function

    Private Sub ReadHDRFile(ByRef FLTData As sFLTData, ByVal HeaderFile As String)
        'process the header information using standard file reading
        'Dan Ames April 2003
        '
        'Header should be a text file with the extension hdr and content like this:
        'ncols         2199
        'nrows(1861)
        'xllcorner(-120.36583333496)
        'yllcorner(46.992222223902)
        'cellsize(0.0002777777778)
        'NODATA_value(-9999)
        'byteorder(LSBFIRST)
        Dim FileNum As Integer, OneLine As String
        Dim isCorner As Boolean = False

        FileNum = FreeFile()
        FileOpen(FileNum, HeaderFile, OpenMode.Input, OpenAccess.Read)
        Do Until EOF(FileNum)
            OneLine = LCase(LineInput(FileNum))
            If InStr(OneLine, "ncols") > 0 Then
                Integer.TryParse(OneLine.Replace("ncols", "").Replace(vbTab, "").Replace(" ", ""), FLTData.ncols)
            End If
            If InStr(OneLine, "nrows") > 0 Then
                Integer.TryParse(OneLine.Replace("nrows", "").Replace(vbTab, "").Replace(" ", ""), FLTData.nrows)
            End If
            'Corners vs. Center fixed later on
            If InStr(OneLine, "xllcorner") > 0 Then
                isCorner = True
                Double.TryParse(OneLine.Replace("xllcorner", "").Replace(vbTab, "").Replace(" ", ""), FLTData.xllcenter)
            End If
            If InStr(OneLine, "yllcorner") > 0 Then
                isCorner = True
                Double.TryParse(OneLine.Replace("yllcorner", "").Replace(vbTab, "").Replace(" ", ""), FLTData.yllcenter)
            End If
            If InStr(OneLine, "xllcenter") > 0 Then
                Double.TryParse(OneLine.Replace("xllcenter", "").Replace(vbTab, "").Replace(" ", ""), FLTData.xllcenter)
            End If
            If InStr(OneLine, "yllcenter") > 0 Then
                Double.TryParse(OneLine.Replace("yllcenter", "").Replace(vbTab, "").Replace(" ", ""), FLTData.yllcenter)
            End If
            If InStr(OneLine, "cellsize") > 0 Then
                Dim tStr As String, tDbl As Double
                tStr = OneLine.Substring(OneLine.IndexOfAny(New Char() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}))
                Double.TryParse(tStr, tDbl)
                tDbl = Math.Round(tDbl, 13)
                FLTData.cellsize = tDbl
            End If
            If InStr(OneLine, "nodata_value") > 0 Then
                Double.TryParse(OneLine.Replace("nodata_value", "").Replace(" ", "").Replace(vbTab, ""), FLTData.NODATA_value)
                'FLTData.NODATA_value = CInt(Mid(OneLine, 15))
            End If
            If InStr(OneLine, "byteorder") > 0 Then
                FLTData.byteorder = OneLine.Replace("byteorder", "").Replace(" ", "").Replace(vbTab, "")
            End If
        Loop

        If isCorner Then
            FLTData.xllcenter += FLTData.cellsize / 2
            FLTData.yllcenter += FLTData.cellsize / 2
        End If

        FileClose(FileNum)
    End Sub

    Private Sub ReadFLTData(ByRef FLTData As sFLTData, ByVal DataFile As String, ByRef Callback As MapWinGIS.ICallback)
        'Read in a USGS Float (.flt) grid data file
        'Dan Ames April 2003
        Dim i As Integer, j As Integer
        Dim fs As FileStream = Nothing
        Dim r As BinaryReader
        Try
            ReDim FLTData.Values(FLTData.ncols - 1, FLTData.nrows - 1)

            fs = File.Open(DataFile, FileMode.Open)
            r = New BinaryReader(fs)
            fs.Seek(0, SeekOrigin.Begin)
            For j = 0 To FLTData.nrows - 1
                For i = 0 To FLTData.ncols - 1
                    FLTData.Values(i, j) = r.ReadSingle()
                Next
                If Not Callback Is Nothing Then
                    Callback.Progress(MyBase.Key, j / FLTData.nrows * 50, "Reading FLT Data")
                End If

            Next
        Catch ex As System.Exception
            mapwinutility.logger.msg("Error in ReadFLTData: " & vbCrLf & ex.Message)
        End Try

        Try
            fs.Close()
        Catch
        End Try
    End Sub

    '---------------------------------------------------------------------------------------
    ' Procedure : grd2asc
    ' DateTime  : 6 januari 2005 16:33
    ' Author    : Paul Meems
    ' Purpose   : Convert Surfer7 Binaire grid file to ASCII gridfile
    '---------------------------------------------------------------------------------------
    Public Function grd2asc(ByRef sGridfile As String, ByRef sAscfile As String, Optional ByRef errMsg As String = Nothing) As Boolean
        Dim bResult As Boolean
        Dim lAscGridWriter As System.IO.TextWriter
        Dim arr() As Double
        Dim x As Long, y As Long
        Dim lFileBinaryRead As Integer
        Dim nRows As Int32
        Dim nCols As Int32
        Dim lDataLength As Long
        Dim xLL As Double, yLL As Double
        Dim xSize As Double, ySize As Double, blankValue As Double
        Const cNoDataValue As String = "-99"
        Dim lMaxPoints As Long, lMinPoints As Long
        Dim sRegel As String, sCellWaarde As String
        Dim dblTempValue As Double
        Try
            'init:
            bResult = False

            lFileBinaryRead = FreeFile()
            FileOpen(lFileBinaryRead, sGridfile, OpenMode.Binary)

            'From: http://www.geospatialdesigns.com/surfer7_format.htm
            FileGet(lFileBinaryRead, nRows, 21)
            FileGet(lFileBinaryRead, nCols, 25)
            FileGet(lFileBinaryRead, xLL, 29)
            FileGet(lFileBinaryRead, yLL, 37)
            FileGet(lFileBinaryRead, xSize, 45)
            FileGet(lFileBinaryRead, ySize, 53)
            FileGet(lFileBinaryRead, blankValue, 85)

            lDataLength = ((nRows * nCols) - 1)
            ReDim arr(lDataLength)

            FileGet(lFileBinaryRead, CType(arr, Double()), 101)

            'Ready reading.
            'Close file:
            FileClose(lFileBinaryRead)

            'Create file:
            'If necassary delete old file:
            Try
                Kill(sAscfile)
            Catch
            End Try

            lAscGridWriter = New System.IO.StreamWriter(sAscfile)

            'Write header:
            'NCOLS  672
            lAscGridWriter.WriteLine("NCOLS" & vbTab & CStr(nCols))
            'NROWS  464
            lAscGridWriter.WriteLine("NROWS" & vbTab & CStr(nRows))
            'XLLCENTER  500010
            lAscGridWriter.WriteLine("XLLCENTER" & vbTab & CStr(xLL))
            'YLLCENTER  4830030
            lAscGridWriter.WriteLine("YLLCENTER" & vbTab & CStr(yLL))
            lAscGridWriter.WriteLine("DX" & vbTab & CStr(xSize))
            lAscGridWriter.WriteLine("DY" & vbTab & CStr(ySize))
            'NODATA_VALUE -99
            lAscGridWriter.WriteLine("NODATA_VALUE" & vbTab & cNoDataValue)

            'The data:
            lMaxPoints = UBound(arr)
            lMinPoints = LBound(arr)
            'Read from bottom to top:
            'Per row:
            For x = lMaxPoints - nCols To lMinPoints - nCols Step -1 * nCols
                sRegel = ""
                For y = 1 To nCols
                    'x is start of row:
                    dblTempValue = arr(x + y)
                    If dblTempValue < blankValue Then
                        sCellWaarde = Math.Round(dblTempValue, 4)
                    Else
                        sCellWaarde = cNoDataValue
                    End If
                    If sRegel = "" Then
                        sRegel = sCellWaarde
                    Else
                        sRegel = sRegel & vbTab & sCellWaarde
                    End If
                Next y
                'Compler row, so write it down:
                lAscGridWriter.WriteLine(sRegel)
                'Next row
            Next x

            'Close file:
            lAscGridWriter.Close()

            '*****************
            bResult = True
        Catch ex As Exception
            If Not errMsg Is Nothing Then errMsg = ex.ToString
        Finally
            'Clean up arrays:
            Erase arr
            ReDim arr(0)
            'Ready:

        End Try
        Return bResult
    End Function

    'use this for importing .dem
    Private Function ImportDEMFormat(ByVal InFile As String, ByRef Callback As MapWinGIS.ICallback) As Boolean
        'Read the .DEM data stream and process it
        'Dan Ames April 2003
        Dim FileStream As Stream
        Dim DEMData As New sDEMData
        Try
            FileStream = System.IO.File.Open(InFile, FileMode.Open, FileAccess.Read)
            If Not (FileStream Is Nothing) Then
                DEMData = ReadDEMData(FileStream, Callback)
                FileStream.Close()
                FileStream.Dispose()
                MakeGrid(DEMData, Callback)
                Return True
            Else
                Return False
            End If
        Catch ex As System.Exception
            MapWinUtility.Logger.Msg("Error in ImportDEMFormat importing " & InFile & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Private Function ImportSURFERFormat(ByVal InFile As String, ByRef Callback As MapWinGIS.ICallback) As Boolean
        'Read the .DEM data stream and process it
        'Dan Ames April 2003
        Dim tempPath As String = System.IO.Path.ChangeExtension(GetMWTempFile, "")
        System.IO.Directory.CreateDirectory(tempPath)
        Dim tempAsc As String = tempPath + "\" + System.IO.Path.GetFileNameWithoutExtension(InFile) + ".asc"

        If Not grd2asc(InFile, tempAsc) Then Return False

        MyBase.Close()
        If Not MyBase.Open(tempAsc) Then Return False

        Return True
    End Function

    Private Sub MakeGrid(ByVal DEMData As sDEMData, ByRef Callback As MapWinGIS.ICallback)
        'make a grid object out of the DEMdata from a .DEM file
        Dim h As New MapWinGIS.GridHeader
        Dim i As Integer, j As Integer, m As Integer, n As Integer
        h.dX = 30
        h.dY = 30
        h.Notes = "DEMData.Notes"
        h.NodataValue = -1
        h.NumberCols = DEMData.NumCols
        h.NumberRows = Int((DEMData.MaxY - DEMData.MinY) / 30) + 1
        h.XllCenter = DEMData.MinX()
        h.YllCenter = DEMData.MinY()

        MapWinUtility.Logger.Dbg("Make a grid object out of the DEMdata from a .DEM file")
        MyBase.Close()
        MapWinUtility.Logger.Dbg("Before createNew")
        MyBase.CreateNew("", h, MapWinGIS.GridDataType.ShortDataType, h.NodataValue, True, MapWinGIS.GridFileType.Binary)
        MapWinUtility.Logger.Dbg("New grid created with only a header")

        For i = 0 To DEMData.NumCols - 1
            For j = 0 To DEMData.NumElevs(i) - 1
                MyBase.ProjToCell(DEMData.ColStarts(i).X, DEMData.ColStarts(i).Y + (30 * j), m, n)
                MyBase.Value(m, n) = DEMData.Values(i, j)
            Next j
            MapWinUtility.Logger.Dbg("Creating MapWinGIS grid object. numCols: " + i.ToString())
            If Not Callback Is Nothing Then
                Callback.Progress(MyBase.Key, 50 + i / DEMData.NumCols * 50, "Creating MapWinGIS grid object")
            End If
        Next i
        MapWinUtility.Logger.Dbg("Finished MakeGrid")
    End Sub

    Private Sub MakeGrid(ByVal FLTData As sFLTData, ByVal InitialFile As String, ByRef Callback As MapWinGIS.ICallback)
        'make a grid object out of the fltdata from a .FLT file
        'Dan Ames April 2003
        Dim h As New MapWinGIS.GridHeader
        Dim i As Integer, j As Integer
        h.dX = FLTData.cellsize
        h.dY = FLTData.cellsize
        h.Notes = "Grid imported from " & System.IO.Path.GetFileName(InitialFile)
        h.NodataValue = FLTData.NODATA_value
        h.NumberCols = FLTData.ncols
        h.NumberRows = FLTData.nrows
        h.XllCenter = FLTData.xllcenter
        h.YllCenter = FLTData.yllcenter

        MyBase.Close()
        MyBase.CreateNew("", h, MapWinGIS.GridDataType.FloatDataType, -1, True, MapWinGIS.GridFileType.Binary)

        For i = 0 To FLTData.ncols - 1
            For j = 0 To FLTData.nrows - 1
                MyBase.Value(i, j) = FLTData.Values(i, j)
            Next j
            If Not Callback Is Nothing Then
                Callback.Progress(MyBase.Key, 50 + i / FLTData.ncols * 50, "Creating MapWinGIS grid")
            End If
        Next i
    End Sub
    Private Function ReadDEMData(ByVal fileStream As Stream, ByRef Callback As MapWinGIS.ICallback) As sDEMData
        'This function reads in the USGS .DEM format (ASCII text)
        'Dan Ames April 2003
        Dim i As Integer
        Dim j As Integer, numElevs As Integer
        Dim Elev As Integer
        Dim Off2 As Integer, Off3 As Integer, Inserts As Integer
        Dim DEMData As New sDEMData
        Dim chunckResult As String
        Try
            'get header data
            DEMData.Notes = GetChunk(fileStream, 1, 144)
            DEMData.HorizUnits = Int(GetChunk(fileStream, 529, 6))
            DEMData.VertUnits = Int(GetChunk(fileStream, 535, 6))
            ReDim DEMData.Vertices(3)
            For i = 0 To 3
                DEMData.Vertices(i).X = Fdbl(GetChunk(fileStream, 547 + (48 * i), 24))
                DEMData.Vertices(i).Y = Fdbl(GetChunk(fileStream, 571 + (48 * i), 24))
            Next
            DEMData.Min = Fdbl(GetChunk(fileStream, 739, 24))
            DEMData.Max = Fdbl(GetChunk(fileStream, 763, 24))
            DEMData.NumCols = Int(GetChunk(fileStream, 859, 6))
            ReDim DEMData.Values(DEMData.NumCols - 1, 0)
            ReDim DEMData.NumElevs(DEMData.NumCols - 1)
            MapWinUtility.Logger.Dbg("Header info of the DEM:")
            MapWinUtility.Logger.Dbg("DEMData.Notes: " + DEMData.Notes)
            MapWinUtility.Logger.Dbg("DEMData.HorizUnits: " + DEMData.HorizUnits.ToString())
            MapWinUtility.Logger.Dbg("DEMData.VertUnits: " + DEMData.VertUnits.ToString())
            MapWinUtility.Logger.Dbg("DEMData.Min: " + DEMData.Min.ToString())
            MapWinUtility.Logger.Dbg("DEMData.Max: " + DEMData.Max.ToString())
            MapWinUtility.Logger.Dbg("DEMData.NumCols: " + DEMData.NumCols.ToString())

            'get elevation data
            Off2 = 1024
            Inserts = 0
            ReDim DEMData.ColStarts(DEMData.NumCols - 1)
            MapWinUtility.Logger.Dbg("DEM Data")
            For i = 0 To DEMData.NumCols - 1
                'read the num elevs, starting x and starting y
                numElevs = Int(GetChunk(fileStream, Off2 + 13, 6))
                DEMData.NumElevs(i) = numElevs
                DEMData.ColStarts(i).X = Fdbl(GetChunk(fileStream, Off2 + 25, 24))
                DEMData.ColStarts(i).Y = Fdbl(GetChunk(fileStream, Off2 + 49, 24))

                If numElevs - 1 > UBound(DEMData.Values, 2) Then
                    ReDim Preserve DEMData.Values(DEMData.NumCols - 1, numElevs - 1)
                End If
                For j = 0 To numElevs - 1
                    Inserts = 0
                    If j > 145 Then
                        Inserts = (Int((j - 145) / 171) + 1)
                    End If
                    Off3 = Off2 + 145 + (j * 6) + Inserts * 4 '4 spaces between each extended group
                    chunckResult = GetChunk(fileStream, Off3, 6)
                    If Trim(chunckResult) <> String.Empty Then
                        If (Integer.TryParse(chunckResult, Elev) = False) Then
                            ' What to do know?
                            ' Probably set Elev to NODATA?
                            Elev = -1
                            MapWinUtility.Logger.Dbg("ReadDEMData Error. GetChunk returned non-integer:" + chunckResult)
                        End If
                    End If
                    DEMData.Values(i, j) = Elev
                    'MapWinUtility.Logger.Dbg(String.Format("i: {0}, j: {1}, Elev: {2} ", i.ToString(), j.ToString(), Elev.ToString()))
                Next j
                'compute new offset based on number of elevs in current record
                If numElevs <= 146 Then
                    Off2 = Off2 + 1024
                Else
                    Inserts = (Int((numElevs - 146) / 170) + 1)
                    Off2 = Off2 + 1024 + Inserts * 1024
                End If
                'update progressbar
                If Not Callback Is Nothing Then
                    Callback.Progress(MyBase.Key, i / DEMData.NumCols * 50, "Reading DEM data")
                End If
            Next i
            MapWinUtility.Logger.Dbg("Finished ReadDEMData")
            Return DEMData
        Catch ex As System.Exception
            Dim msg As String
            msg = "Error in ReadDEMData"
            MapWinUtility.Logger.Msg(msg + ": " + ex.Message)
            Debug.WriteLine(msg + ": " + ex.ToString())
        End Try
        Return Nothing
    End Function

    Private Function GetChunk(ByVal St As Stream, ByVal Offset As Integer, ByVal Count As Integer) As String
        'Reads in a specified chunk of at stream and returns it as text
        'Dan Ames April, 2003
        Dim b() As Byte
        Dim s As String = ""
        ReDim b(Count - 1)
        St.Seek(Offset - 1, SeekOrigin.Begin)
        St.Read(b, 0, Count)
        'Debug.WriteLine("In GetChunk")
        'For i As Integer = 0 To UBound(b)
        '    s = s & Chr(CInt(b(i)))
        '    'Debug.WriteLine("s: " + s)
        'Next i
        'Return s
        ' Paul Meems - 19 August 2009
        Return System.Text.Encoding.ASCII.GetString(b).Trim()
    End Function

    Private Function Fdbl(ByVal s As String) As Double
        'Used to convert a fortran double into a VB double
        'Dan Ames April 2003
        Dim d As Double
        s = Replace(s, "D+", "e", 1, -1, CompareMethod.Text)
        d = CDbl(s)
        Return d
    End Function

    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub Finalize()
        Try
            MyBase.Close()
            MyBase.Finalize()
        Catch
        End Try
    End Sub

    Public Overrides ReadOnly Property CdlgFilter() As String
        Get
            Dim arr() As String
            Dim newFormats As String
            Dim oldLength As Integer

            arr = MyBase.CdlgFilter.Split("|")
            If arr.Length > 1 Then
                newFormats = arr(1)
                newFormats &= ";*.dem;*.flt"
                oldLength = arr.Length

                ReDim Preserve arr(arr.Length + 3) ' Add two new formats
                arr(1) = newFormats
                arr(oldLength) = "USGS NED Grid Float (*.flt)"
                arr(oldLength + 1) = "*.flt"
                arr(oldLength + 2) = "USGS DEM (*.dem)"
                arr(oldLength + 3) = "*.dem"
                Return String.Join("|", arr)
            Else
                Return MyBase.CdlgFilter
            End If
        End Get
    End Property

    Public Overrides Sub CellToProj(ByVal Column As Integer, ByVal Row As Integer, ByRef x As Double, ByRef y As Double)
        MyBase.CellToProj(Column, Row, x, y)
    End Sub

    Public Overrides Function Clear(ByVal ClearValue As Object) As Boolean
        MyBase.Clear(ClearValue)
    End Function

    Public Overrides Function Close() As Boolean
        MyBase.Close()
    End Function

    Public Overrides ReadOnly Property DataType() As MapWinGIS.GridDataType
        Get
            Return MyBase.DataType
        End Get
    End Property

    Public Overrides ReadOnly Property ErrorMsg(ByVal ErrorCode As Integer) As String
        Get
            Return MyBase.ErrorMsg(ErrorCode)
        End Get
    End Property

    Public Overrides ReadOnly Property Filename() As String
        Get
            Return MyBase.Filename
        End Get
    End Property

    Public Overrides Property GlobalCallback() As MapWinGIS.ICallback
        Get
            Return MyBase.GlobalCallback
        End Get
        Set(ByVal Value As MapWinGIS.ICallback)
            MyBase.GlobalCallback = Value
        End Set
    End Property

    Public Overrides ReadOnly Property Header() As MapWinGIS.GridHeader
        Get
            Return MyBase.Header
        End Get
    End Property

    Public Overrides ReadOnly Property InRam() As Boolean
        Get
            Return MyBase.InRam
        End Get
    End Property

    Public Overrides Property Key() As String
        Get
            Return MyBase.Key
        End Get
        Set(ByVal Value As String)
            MyBase.Key = Value
        End Set
    End Property

    Public Overrides ReadOnly Property LastErrorCode() As Integer
        Get
            Return MyBase.LastErrorCode
        End Get
    End Property

    Public Overrides ReadOnly Property Maximum() As Object
        Get
            Return MyBase.Maximum
        End Get
    End Property

    Public Overrides ReadOnly Property Minimum() As Object
        Get
            Return MyBase.Minimum
        End Get
    End Property

    Public Overrides Sub ProjToCell(ByVal x As Double, ByVal y As Double, ByRef Column As Integer, ByRef Row As Integer)
        MyBase.ProjToCell(x, y, Column, Row)
    End Sub

    Public Overrides Function Save(Optional ByVal Filename As String = "", Optional ByVal GridFileType As MapWinGIS.GridFileType = MapWinGIS.GridFileType.UseExtension, Optional ByVal cBack As MapWinGIS.ICallback = Nothing) As Boolean
        MyBase.Save(Filename, GridFileType, cBack)
    End Function

    Public Overrides Property Value(ByVal Column As Integer, ByVal Row As Integer) As Object
        Get
            Return MyBase.Value(Column, Row)
        End Get
        Set(ByVal Value As Object)
            MyBase.Value(Column, Row) = Value
        End Set
    End Property

    Public Overrides Function CreateNew(ByVal Filename As String, ByVal Header As MapWinGIS.GridHeader, ByVal DataType As MapWinGIS.GridDataType, ByVal InitialValue As Object, Optional ByVal InRam As Boolean = True, Optional ByVal FileType As MapWinGIS.GridFileType = MapWinGIS.GridFileType.UseExtension, Optional ByVal cBack As MapWinGIS.ICallback = Nothing) As Boolean
        MyBase.CreateNew(Filename, Header, DataType, InitialValue, InRam, FileType, cBack)
    End Function

    Public Overrides Function Open(ByVal Filename As String, Optional ByVal DataType As MapWinGIS.GridDataType = MapWinGIS.GridDataType.UnknownDataType, Optional ByVal InRam As Boolean = True, Optional ByVal FileType As MapWinGIS.GridFileType = MapWinGIS.GridFileType.UseExtension, Optional ByVal cBack As MapWinGIS.ICallback = Nothing) As Boolean
        Dim bgdequiv As String = System.IO.Path.ChangeExtension(Filename, ".bgd")
        If Filename.ToLower.EndsWith(".flt") Then
            'Optimize by not converting if it already has been converted
            If IO.File.Exists(bgdequiv) AndAlso MapWinUtility.DataManagement.CheckFile2Newest(Filename, bgdequiv) Then
                Return MyBase.Open(bgdequiv, MapWinGIS.GridDataType.UnknownDataType, InRam, MapWinGIS.GridFileType.Binary, cBack)
            Else
                If ImportFLTFormat(Filename, cBack) Then
                    MyBase.Save(bgdequiv, MapWinGIS.GridFileType.Binary, cBack)
                    Return True
                Else
                    Return False
                End If
            End If
        ElseIf Filename.ToLower.EndsWith(".dem") Then
            'Optimize by not converting if it already has been converted
            If IO.File.Exists(bgdequiv) AndAlso MapWinUtility.DataManagement.CheckFile2Newest(Filename, bgdequiv) Then
                Return MyBase.Open(bgdequiv, MapWinGIS.GridDataType.UnknownDataType, InRam, MapWinGIS.GridFileType.Binary, cBack)
            Else
                If ImportDEMFormat(Filename, cBack) Then
                    MapWinUtility.Logger.Dbg("Save grid with name: " + bgdequiv)
                    MyBase.Save(bgdequiv, MapWinGIS.GridFileType.Binary, cBack)
                    Return True
                Else
                    Return False
                End If
            End If
        ElseIf Filename.ToLower.EndsWith(".grd") Then
            'Optimize by not converting if it already has been converted
            If IO.File.Exists(bgdequiv) AndAlso MapWinUtility.DataManagement.CheckFile2Newest(Filename, bgdequiv) Then
                Return MyBase.Open(bgdequiv, MapWinGIS.GridDataType.UnknownDataType, InRam, MapWinGIS.GridFileType.Binary, cBack)
            Else
                If ImportSURFERFormat(Filename, cBack) Then
                    MyBase.Save(bgdequiv, MapWinGIS.GridFileType.Binary, cBack)
                    Return True
                Else
                    Return False
                End If
            End If
        Else
            Return MyBase.Open(Filename, DataType, InRam, FileType, cBack)
        End If
    End Function

    Public Overrides Function AssignNewProjection(ByVal Projection As String) As Boolean
        MyBase.AssignNewProjection(Projection)
    End Function

    Public Overrides ReadOnly Property RasterColorTableColoringScheme() As MapWinGIS.GridColorScheme
        Get
            Return MyBase.RasterColorTableColoringScheme()
        End Get
    End Property

    Public Overrides Function GetRow(ByVal Row As Integer, ByRef Vals As Single) As Boolean
        Return MyBase.GetRow(Row, Vals)
    End Function

    Public Overrides Function PutRow(ByVal Row As Integer, ByRef Vals As Single) As Boolean
        Return MyBase.PutRow(Row, Vals)
    End Function

    Public Overrides Function GetFloatWindow(ByVal StartRow As Integer, ByVal EndRow As Integer, ByVal StartCol As Integer, ByVal EndCol As Integer, ByRef Vals As Single) As Boolean
        Return MyBase.GetFloatWindow(StartRow, EndRow, StartCol, EndCol, Vals)
    End Function

    Public Overrides Function PutFloatWindow(ByVal StartRow As Integer, ByVal EndRow As Integer, ByVal StartCol As Integer, ByVal EndCol As Integer, ByRef Vals As Single) As Boolean
        Return MyBase.PutFloatWindow(StartRow, EndRow, StartCol, EndCol, Vals)
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
End Class
