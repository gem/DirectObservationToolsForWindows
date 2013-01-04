'********************************************************************************************************
'File Name: clsGPSController.vb
'Description: This screen is used to specify parameters for adding a new field.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'July 17, 2007: Allen Anselmo allen.anselmo@gmail.com - 
'               Added licensing and comments to original code

Imports System.IO
Public Class clsGPSController

#Region "Events"
    Public Event BasicInfoUpdated()
    Public Event NMEASentenceReceived(ByVal Sentence As String)
    Public Event TimeDateFound(ByVal UTCTime As String, ByVal UTCDate As String)
    Public Event PositionFound(ByVal LatitudeDD As Double, ByVal LongitudeDD As Double)
    Public Event SpeedBearingFound(ByVal Speed As Double, ByVal Bearing As Double)
    Public Event DOPFound(ByVal PDOP As Double, ByVal HDOP As Double, ByVal VDOP As Double)
    Public Event SatCountFound(ByVal SatCount As Integer)
    Public Event ElevationFound(ByVal Elevation As Double)
    Public Event SatInfoFound(ByVal CurrSequence As Integer, ByVal SatsID() As String, ByVal SatsAzi() As String, ByVal SatsElev() As String, ByVal SatsSNR() As String)
#End Region

#Region "Properties"
    Private WithEvents _SerialPort As New Ports.SerialPort
    Private _DoProcessing As Boolean = True
    Private _FeedNmeaInputPath As String
    Private _FeedNmeaDelay As Integer

    Private _stopFileRead As Boolean = False
    Public Property StopFileRead() As Boolean
        Get
            Return _stopFileRead
        End Get
        Set(ByVal value As Boolean)
            _stopFileRead = value
        End Set
    End Property

    Private _COMBaud As String = "4800"
    Public Property Baud() As String
        Get
            Return _COMBaud
        End Get
        Set(ByVal value As String)
            If Not _SerialPort.IsOpen Then
                _COMBaud = value
                _SerialPort.BaudRate = value
            End If
        End Set
    End Property

    Private _COMPort As String = "COM5"
    Public Property Port() As String
        Get
            Return _COMPort
        End Get
        Set(ByVal value As String)
            If Not _SerialPort.IsOpen Then
                _COMPort = value
                _SerialPort.PortName = value
            End If
        End Set
    End Property

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _SerialPort.IsOpen
        End Get
    End Property

    Private _Latitude As Double = -999
    Public ReadOnly Property Latitude() As Double
        Get
            Return _Latitude
        End Get
    End Property

    Private _Longitude As Double = -999
    Public ReadOnly Property Longitude() As Double
        Get
            Return _Longitude
        End Get
    End Property

    Private _LatitudeDD As Double = -999
    Public ReadOnly Property LatitudeDD() As Double
        Get
            Return _LatitudeDD
        End Get
    End Property

    Private _LongitudeDD As Double = -999
    Public ReadOnly Property LongitudeDD() As Double
        Get
            Return _LongitudeDD
        End Get
    End Property

    Private _Altitude As Double
    Public ReadOnly Property Altitude() As Double
        Get
            Return _Altitude
        End Get
    End Property

    Private _Speed As Double
    Public ReadOnly Property Speed() As Double
        Get
            Return _Speed
        End Get
    End Property

    Private _Bearing As Double
    Public ReadOnly Property Bearing() As Double
        Get
            Return _Bearing
        End Get
    End Property

    Private _UTCTime As String
    Public ReadOnly Property UTCTime() As String
        Get
            Return _UTCTime
        End Get
    End Property

    Private _UTCdate As String
    Public ReadOnly Property UTCDate() As String
        Get
            Return _UTCdate
        End Get
    End Property

    Private _PDOP As Double
    Public ReadOnly Property PDOP() As Double
        Get
            Return _PDOP
        End Get
    End Property

    Private _HDOP As Double
    Public ReadOnly Property HDOP() As Double
        Get
            Return _HDOP
        End Get
    End Property

    Private _VDOP As Double
    Public ReadOnly Property VDOP() As Double
        Get
            Return _VDOP
        End Get
    End Property

    Private _SatsUsed As Double
    Public ReadOnly Property SatsUsed() As Integer
        Get
            Return _SatsUsed
        End Get
    End Property

    Private _Sats(0 To 11) As String
    Public ReadOnly Property Satellites() As String()
        Get
            Return _Sats
        End Get
    End Property

    Private _SatsID(0 To 11) As String
    Public ReadOnly Property SatelliteIDs() As String()
        Get
            Return _SatsID
        End Get
    End Property

    Private _SatsElev(0 To 11) As String
    Public ReadOnly Property SatelliteElevations() As String()
        Get
            Return _SatsElev
        End Get
    End Property

    Private _SatsAzi(0 To 11) As String
    Public ReadOnly Property SatelliteAzimuths() As String()
        Get
            Return _SatsAzi
        End Get
    End Property

    Private _SatsSNR(0 To 11) As String
    Public ReadOnly Property SatelliteSigToNoises() As String()
        Get
            Return _SatsSNR
        End Get
    End Property

    Private _SatFix As Boolean
    Public ReadOnly Property HasFix() As Boolean
        Get
            Return _SatFix
        End Get
    End Property

    Private _isReceiving As Boolean = False
    Public ReadOnly Property IsReceiving() As Boolean
        Get
            Return _isReceiving
        End Get
    End Property

#End Region

#Region "Public Methods"
    ''' <summary>
    ''' A module used to initialize the serial port communications
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCom()
        _SerialPort.ReadTimeout = 1000
        _SerialPort.ReadBufferSize = 4096
        _SerialPort.ReceivedBytesThreshold = 1
    End Sub

    ''' <summary>
    ''' A function used to test the current COM port settings, with or with exceptions thrown
    ''' </summary>
    ''' <param name="ThrowExceptions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TestConnectSettings(ByVal ThrowExceptions As Boolean) As Boolean
        Dim result As Boolean = False
        Dim tmpRead As String = ""
        Dim numRetries As Integer = 5
        _DoProcessing = False
        Try
            _SerialPort.Open()
        Catch ex As Exception
            If ThrowExceptions Then
                Throw New ApplicationException("BadCOMSettings")
            End If
        End Try

        If _SerialPort.IsOpen Then
            For i As Integer = 1 To numRetries
                System.Threading.Thread.Sleep(250)
                Try
                    tmpRead = _SerialPort.ReadLine
                Catch ex As Exception
                    _SerialPort.Close()
                    If ThrowExceptions Then
                        Throw New ApplicationException("BadCOMSettings")
                    End If
                    Exit For
                End Try

                If tmpRead.StartsWith("$") Then
                    result = True
                    Exit For
                End If
                If result = False And i = numRetries Then
                    _SerialPort.Close()
                    If ThrowExceptions Then
                        Throw New ApplicationException("NotConnectedGPS")
                    End If
                    Exit For
                End If
            Next
        End If
        _DoProcessing = True
        Return result
    End Function

    ''' <summary>
    ''' A module used to start the GPS communicating with the COM connected GPS unit
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartReceiving()
        AddHandler _SerialPort.DataReceived, AddressOf srlprtGPS_DataReceived
        _isReceiving = True
        _DoProcessing = True
    End Sub

    ''' <summary>
    ''' A module used to stop the GPS communicating with the COM connected GPS unit
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopReceiving()
        _DoProcessing = False
        _isReceiving = False
        RemoveHandler _SerialPort.DataReceived, AddressOf srlprtGPS_DataReceived
    End Sub

    ''' <summary>
    ''' A module used to close the serial port open to the GPS unit
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClosePort()
        If _isReceiving Then
            StopReceiving()
            System.Threading.Thread.Sleep(250)
        End If
        _SerialPort.Close()
    End Sub

    ''' <summary>
    ''' A module used to spawn a thread which will read a NMEA input text file and force the GPS contoller to act as if it is receiving real GPS data. Used for debugging primarily.
    ''' </summary>
    ''' <param name="InputPath"></param>
    ''' <param name="delay"></param>
    ''' <remarks></remarks>
    Public Sub FeedNmeaFile(ByVal InputPath As String, ByVal delay As Integer)
        _FeedNmeaInputPath = InputPath
        _FeedNmeaDelay = delay
        Dim Thread1 As New System.Threading.Thread(AddressOf FeedNmeaThread)
        Thread1.Priority = Threading.ThreadPriority.Highest
        Thread1.Start()
    End Sub

    ''' <summary>
    ''' The thread function spawned to feed the NMEA input. Running on a separate thread is the closest to receiving real GPS data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FeedNmeaThread()
        Dim sr As New StreamReader(_FeedNmeaInputPath)
        Dim currline As String
        _stopFileRead = False

        While Not sr.EndOfStream
            System.Threading.Thread.Sleep(_FeedNmeaDelay)
            currline = sr.ReadLine
            If currline.StartsWith("$G") And currline.Contains("*") Then
                parseNMEA(currline)
            End If

            If _stopFileRead Then
                Exit While
            End If
        End While
        sr.Close()
    End Sub

#End Region

#Region "Private Methods"
    ''' <summary>
    ''' The event handler for the Serial port data received event. Used to drive all processes of the GPS controller
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub srlprtGPS_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Dim currline As String = ""
        If _DoProcessing Then
            If _SerialPort.IsOpen Then
                Try
                    currline = _SerialPort.ReadLine()
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    If Not _SerialPort Is Nothing And _SerialPort.IsOpen Then
                        _SerialPort.Close()
                    End If
                    StopReceiving()
                End Try
            End If

            If currline.StartsWith("$G") And currline.Contains("*") Then
                parseNMEA(currline)
            End If

        End If
    End Sub

    ''' <summary>
    ''' A module used to parse NMEA-0183 sentences
    ''' </summary>
    ''' <param name="currline"></param>
    ''' <remarks></remarks>
    Private Sub parseNMEA(ByVal currline As String)
        Dim tokens As String() = currline.Split(",")
        RaiseEvent NMEASentenceReceived(currline)

        Select Case tokens(0)
            Case "$GPRMC"
                parseLocAndSpeedRMC(tokens)
            Case "$GPGSA"
                parseActiveSatAndDopGSA(tokens)
            Case "$GPGSV"
                parseSatInfoGSV(tokens)
            Case "$GPGGA"
                parseFixGGA(tokens)
        End Select

        If currline.StartsWith("$GPRMC") Then
            RaiseEvent BasicInfoUpdated()
        End If
    End Sub

    ''' <summary>
    ''' Parses the RMC string for location, bearing, and speed from the GPS
    ''' </summary>
    ''' <param name="tokens"></param>
    ''' <remarks></remarks>
    Private Sub parseLocAndSpeedRMC(ByVal tokens As String())
        Dim timeUTC As String = tokens(1)
        Dim status As String = tokens(2)
        Dim lat As String = tokens(3)
        Dim northsouth As String = tokens(4)
        Dim lon As String = tokens(5)
        Dim eastwest As String = tokens(6)
        Dim speedKnots As String = tokens(7)
        Dim dir As String = tokens(8)
        Dim dateUTC As String = tokens(9)
        Dim MphPerKnotConv As Double = 1.150779
        Dim LatWGS, LonWGS, LatDD, LonDD, currSpeed, currBearing As Double
        Dim SatFix As Boolean = False

        'Find sat fix
        If status <> "" Then
            If status = "A" Then
                _SatFix = True
            ElseIf status = "V" Then
                _SatFix = False
            End If
        End If

        If timeUTC <> "" And dateUTC <> "" Then
            _UTCTime = timeUTC
            _UTCdate = dateUTC
            RaiseEvent TimeDateFound(timeUTC, dateUTC)
        End If

        'If _SatFix Then
        If lat <> "" And northsouth <> "" Then
            If northsouth = "S" Then
                LatWGS = -1 * Double.Parse(lat)
                LatDD = -1 * (Int32.Parse(lat.Substring(0, 2)) + (Double.Parse(lat.Substring(2)) / 60.0))
            Else
                LatWGS = Double.Parse(lat)
                LatDD = (Int32.Parse(lat.Substring(0, 2)) + (Double.Parse(lat.Substring(2)) / 60.0))
            End If
        Else
            LatWGS = -999
            LatDD = -999
        End If

        If lon <> "" And eastwest <> "" Then
            If eastwest = "W" Then
                LonWGS = -1 * Double.Parse(lon)
                LonDD = -1 * (Int32.Parse(lon.Substring(0, 3)) + (Double.Parse(lon.Substring(3)) / 60.0))
            Else
                LonWGS = Double.Parse(lon)
                LonDD = (Int32.Parse(lon.Substring(0, 3)) + (Double.Parse(lon.Substring(3)) / 60.0))
            End If
        Else
            LonWGS = -999
            LonDD = -999
        End If

        If LatDD <> -999.0 And LonDD <> -999.0 Then
            _Longitude = LonWGS
            _Latitude = LatWGS
            _LatitudeDD = LatDD
            _LongitudeDD = LonDD
            RaiseEvent PositionFound(LatDD, LonDD)
        End If

        If speedKnots <> "" Then
            currSpeed = Double.Parse(speedKnots) * MphPerKnotConv

        Else
            currSpeed = -999
        End If

        If dir <> "" Then
            currBearing = Double.Parse(dir)
        Else
            currBearing = -999
        End If

        If currSpeed <> -999.0 And currBearing <> -999.0 Then
            _Speed = currSpeed
            _Bearing = currBearing
            RaiseEvent SpeedBearingFound(currSpeed, currBearing)
        End If
        'End If
    End Sub

    ''' <summary>
    ''' Parses the GSA string for satellite and DOP information
    ''' </summary>
    ''' <param name="tokens"></param>
    ''' <remarks></remarks>
    Private Sub parseActiveSatAndDopGSA(ByVal tokens As String())
        If tokens.Length = 18 Then
            Dim pdoptxt As String = tokens(15)
            Dim hdoptxt As String = tokens(16)
            Dim vdoptxt As String = tokens(17).Substring(0, tokens(17).IndexOf("*"))
            Dim VDOPval, HDOPval, PDOPval As Double
            'Dim Sats(0 to 11) As String

            'For i As Integer = 3 To 14
            '    Sats(i - 3) = tokens(i)
            'Next

            If pdoptxt <> "" Then
                PDOPval = Double.Parse(pdoptxt)
            Else
                PDOPval = -999
            End If
            If hdoptxt <> "" Then
                HDOPval = Double.Parse(hdoptxt)
            Else
                HDOPval = -999
            End If
            If vdoptxt <> "" Then
                VDOPval = Double.Parse(vdoptxt)
            Else
                VDOPval = -999
            End If

            If PDOPval <> -999 And HDOPval <> -999 And VDOPval <> -999 Then
                _PDOP = PDOPval
                _VDOP = VDOPval
                _HDOP = HDOPval
                RaiseEvent DOPFound(PDOPval, HDOPval, VDOPval)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Parses the GGA string for fix information
    ''' </summary>
    ''' <param name="tokens"></param>
    ''' <remarks></remarks>
    Private Sub parseFixGGA(ByVal tokens As String())
        Dim timeUTC As String = tokens(1)
        Dim lat As String = tokens(2)
        Dim northsouth As String = tokens(3)
        Dim lon As String = tokens(4)
        Dim eastwest As String = tokens(5)
        Dim posFix As String = tokens(6)
        Dim satsUsedtxt As String = tokens(7)
        Dim HDOPval As String = tokens(8)
        Dim altitudetxt As String = tokens(9)
        Dim SatFixval As Boolean
        Dim SatsUsedval As Integer
        Dim Altitudeval As Double

        If posFix <> "" Then
            If posFix = "0" Then
                SatFixval = False
            Else
                SatFixval = True
            End If
        End If

        If SatFixval Then
            If satsUsedtxt <> "" Then
                SatsUsedval = Int32.Parse(satsUsedtxt)
            Else
                SatsUsedval = -999
            End If

            If SatsUsedval <> -999 Then
                _SatsUsed = SatsUsedval
                RaiseEvent SatCountFound(SatsUsedval)
            End If

            If altitudetxt <> "" Then
                Altitudeval = Double.Parse(altitudetxt)
            Else
                Altitudeval = -999
            End If

            If Altitudeval <> -999 Then
                _Altitude = Altitudeval
                RaiseEvent ElevationFound(Altitudeval)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Parses the GSV string for satellite positions and signal
    ''' </summary>
    ''' <param name="tokens"></param>
    ''' <remarks></remarks>
    Private Sub parseSatInfoGSV(ByVal tokens As String())
        Dim startIdx As Integer
        Dim currIdx As Integer
        Dim CurrSequence As String = tokens(2)
        Dim CurrSeqNum As Integer
        Dim CurrID As String
        Dim CurrAzimuth As String
        Dim CurrElevation As String
        Dim CurrSigToNoise As String

        If CurrSequence <> "" Then
            CurrSeqNum = Int32.Parse(CurrSequence)

            If CurrSeqNum = 1 Then
                startIdx = 0
            ElseIf CurrSeqNum = 2 Then
                startIdx = 4
            ElseIf CurrSeqNum = 3 Then
                startIdx = 8
            End If
        End If
        If tokens.Length = 20 Then
            For satBlock As Integer = 1 To 4

                CurrID = tokens(4 * satBlock)
                CurrElevation = tokens(4 * satBlock + 1)
                CurrAzimuth = tokens(4 * satBlock + 2)
                If satBlock = 4 Then
                    CurrSigToNoise = tokens(4 * satBlock + 3).Substring(0, tokens(4 * satBlock + 3).IndexOf("*"))
                Else
                    CurrSigToNoise = tokens(4 * satBlock + 3)
                End If

                currIdx = startIdx + (satBlock - 1)
                _SatsID(currIdx) = CurrID
                _SatsAzi(currIdx) = CurrAzimuth
                _SatsElev(currIdx) = CurrElevation
                _SatsSNR(currIdx) = CurrSigToNoise
            Next
            RaiseEvent SatInfoFound(CurrSeqNum, _SatsID, _SatsAzi, _SatsElev, _SatsSNR)
        End If
    End Sub
#End Region

End Class
