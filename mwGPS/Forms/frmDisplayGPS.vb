'********************************************************************************************************
'File Name: frmDisplayGPS.vb
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

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmDisplayGPS

#Region "Private Variables"
    Private _MaxLogLines As Integer = 100
    Private Delegate Sub NMEAInvoker(ByVal Sentence As String)
    Private Delegate Sub SatInfoInvoker(ByVal CurrSequence As Integer, ByVal SatsID() As String, ByVal SatsAzi() As String, ByVal SatsElev() As String, ByVal SatsSNR() As String)

    Private _oldBearing As Double = -999
    Private _oldSatsID(0 To 11) As String
    Private _oldSatsAzi(0 To 11) As String
    Private _oldSatsElev(0 To 11) As String
    Private _oldSatsSNR(0 To 11) As String

    Private _forceArrowDraw As Boolean = False
    Private _forceSatDraw As Boolean = False
#End Region

#Region "Form Event Handlers"
    ''' <summary>
    ''' Handler to intitiailize the form when loading
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDisplayNmea_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        Dim iWidth As Integer = Screen.PrimaryScreen.Bounds.Size.Width
        Dim iHeight As Integer = Screen.PrimaryScreen.Bounds.Size.Height
        Me.Location = New Drawing.Point(iWidth - Me.Width, iHeight - Me.Height - 65)

#If DEBUG Then
        btnFeedFile.Visible = True
#End If

        AddHandler g_GPS.NMEASentenceReceived, AddressOf GPSControllerNMEASentenceReceived
        AddHandler g_GPS.BasicInfoUpdated, AddressOf GPSControllerBasicInfoUpdated
        AddHandler g_GPS.SatInfoFound, AddressOf GPSControllerSatInfoFound
    End Sub

    ''' <summary>
    ''' Handler to close off the gps com handlers and clean up after simulated input
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDisplayNmea_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler g_GPS.NMEASentenceReceived, AddressOf GPSControllerNMEASentenceReceived
        RemoveHandler g_GPS.BasicInfoUpdated, AddressOf GPSControllerBasicInfoUpdated
        RemoveHandler g_GPS.SatInfoFound, AddressOf GPSControllerSatInfoFound

        g_GPS.StopFileRead = True

        g_MapWin.View.Draw.ClearDrawing(g_ArrowDrawingHandle)
    End Sub

    ''' <summary>
    ''' Handler to a button which only shows in debug, that allows the feeding of a NMEA log text file for simulated GPS data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFeedFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeedFile.Click
        If Not g_GPS.IsConnected Then
            g_GPS.FeedNmeaFile(g_FileInputPath, g_FileInputSpeed)
        End If
    End Sub

    ''' <summary>
    ''' Handler for close button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' handler for changing the selected tabs. Generally used to force redraw of dynamic elements 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbctrlTabs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbctrlTabs.SelectedIndexChanged
        If tbctrlTabs.SelectedIndex = 0 Then
            _forceArrowDraw = True
        Else

            _forceSatDraw = True
        End If
    End Sub

    ''' <summary>
    ''' Handler for the lock window checkbox that gets rid of the border or returns it
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkbxShowBorder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowBorder.CheckedChanged
        If Not chkbxShowBorder.Checked Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        End If
        If tbctrlTabs.SelectedIndex = 0 Then
            _forceArrowDraw = True
        Else
            _forceSatDraw = True
        End If
    End Sub

    ''' <summary>
    ''' Handler for the Compass mode that compresses the form to show only the GPS compass or returns it to normal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkbxCompassMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxCompassMode.CheckedChanged
        If chkbxCompassMode.Checked Then
            tbctrlTabs.Enabled = False
            tbctrlTabs.SelectedIndex = 0
            lblStatus.Visible = False
            lblLatDD.Visible = False
            lblLonDD.Visible = False
            lblElev.Visible = False
            lblSpeed.Visible = False
            lblBearing.Visible = False
            lblSats.Visible = False
            lblPdop.Visible = False
            lblHdop.Visible = False
            lblVdop.Visible = False

            Me.Width = 211
            If chkbxShowBorder.Checked Then
                Me.Height = 198
                Me.Location = New Drawing.Point(Me.Location.X + 220, Me.Location.Y + 44)
            Else
                Me.Height = 222
                Me.Location = New Drawing.Point(Me.Location.X + 220, Me.Location.Y + 22)
            End If
        Else
            tbctrlTabs.Enabled = True
            lblStatus.Visible = True
            lblLatDD.Visible = True
            lblLonDD.Visible = True
            lblElev.Visible = True
            lblSpeed.Visible = True
            lblBearing.Visible = True
            lblSats.Visible = True
            lblPdop.Visible = True
            lblHdop.Visible = True
            lblVdop.Visible = True
            Me.Width = Me.MaximumSize.Width
            If chkbxShowBorder.Checked Then
                Me.Height = Me.MaximumSize.Height - 24
                Me.Location = New Drawing.Point(Me.Location.X - 220, Me.Location.Y - 44)
            Else
                Me.Height = Me.MaximumSize.Height
                Me.Location = New Drawing.Point(Me.Location.X - 220, Me.Location.Y - 22)
            End If
        End If
    End Sub
#End Region


#Region "GPS Event Handlers"
    ''' <summary>
    ''' Handler which captures NMEA sentence updates from the GPS controller
    ''' </summary>
    ''' <param name="Sentence"></param>
    ''' <remarks></remarks>
    Private Sub GPSControllerNMEASentenceReceived(ByVal Sentence As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New NMEAInvoker(AddressOf HandleNMEAThread), Sentence)
        Else
            HandleNMEAThread(Sentence)
        End If
    End Sub

    ''' <summary>
    ''' Handler which captures Basic info updates from the GPS controller
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GPSControllerBasicInfoUpdated()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New MethodInvoker(AddressOf HandleBasicInfoUpdated))
        Else
            HandleBasicInfoUpdated()
        End If
    End Sub

    ''' <summary>
    ''' Handler which handles sat info updates from the GPS controller
    ''' </summary>
    ''' <param name="CurrSequence"></param>
    ''' <param name="SatsID"></param>
    ''' <param name="SatsAzi"></param>
    ''' <param name="SatsElev"></param>
    ''' <param name="SatsSNR"></param>
    ''' <remarks></remarks>
    Private Sub GPSControllerSatInfoFound(ByVal CurrSequence As Integer, ByVal SatsID() As String, ByVal SatsAzi() As String, ByVal SatsElev() As String, ByVal SatsSNR() As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New SatInfoInvoker(AddressOf HandleSatInfoThread), CurrSequence, SatsID, SatsAzi, SatsElev, SatsSNR)
        Else
            HandleSatInfoThread(CurrSequence, SatsID, SatsAzi, SatsElev, SatsSNR)
        End If
    End Sub
#End Region

#Region "Display Subs"
    ''' <summary>
    ''' Module which handles the display of NMEA sentences in the listbox if on that tab
    ''' </summary>
    ''' <param name="Sentence"></param>
    ''' <remarks></remarks>
    Private Sub HandleNMEAThread(ByVal Sentence As String)
        If Me.Visible And Not chkbxCompassMode.Checked And tbctrlTabs.SelectedIndex = 2 Then
            lstbxNmea.SelectedIndex = lstbxNmea.Items.Add(Sentence)
            If lstbxNmea.Items.Count > _MaxLogLines Then
                lstbxNmea.Items.RemoveAt(0)
            End If
            Application.DoEvents()
        End If
    End Sub

    ''' <summary>
    ''' Module which handles the basic label updates on the main tab
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleBasicInfoUpdated()
        If Me.Visible Then
            If Not chkbxCompassMode.Checked Then
                If tbctrlTabs.SelectedIndex = 0 Then
                    If g_GPS.HasFix Then
                        lblStatus.Text = "Status: Has Fix"
                        lblLatDD.Text = "Latitude (DD): " + g_GPS.LatitudeDD.ToString("F6")
                        lblLonDD.Text = "Longitude (DD): " + g_GPS.LongitudeDD.ToString("F6")
                        lblSpeed.Text = "Speed: " + g_GPS.Speed.ToString("F4")
                        lblBearing.Text = "Bearing: " + g_GPS.Bearing.ToString("F1")
                        lblPdop.Text = "PDOP: " + g_GPS.PDOP.ToString()
                        lblHdop.Text = "HDOP: " + g_GPS.HDOP.ToString()
                        lblVdop.Text = "VDOP: " + g_GPS.VDOP.ToString()
                        lblSats.Text = "# of Satellites: " + g_GPS.SatsUsed.ToString()
                        lblElev.Text = "Elevation: " + g_GPS.Altitude.ToString("F1")
                        drawArrow(g_GPS.Bearing)
                    Else
                        lblStatus.Text = "Status: No Fix"
                        lblLatDD.Text = "Latitude (DD): "
                        lblLonDD.Text = "Longitude (DD): "
                        lblSpeed.Text = "Speed: "
                        lblBearing.Text = "Bearing: "
                        lblPdop.Text = "PDOP: "
                        lblHdop.Text = "HDOP: "
                        lblVdop.Text = "VDOP: "
                        lblSats.Text = "# of Satellites: "
                        lblElev.Text = "Elevation: "
                    End If
                    Application.DoEvents()
                ElseIf tbctrlTabs.SelectedIndex = 1 Then
                    If g_GPS.HasFix Then
                        lblSatPdop.Text = "PDOP: " + g_GPS.PDOP.ToString
                        lblSatHdop.Text = "HDOP: " + g_GPS.HDOP.ToString
                        lblSatVdop.Text = "VDOP: " + g_GPS.VDOP.ToString
                        lblSatsInUse.Text = "Number of Satellites In Use: " + g_GPS.SatsUsed.ToString
                    Else
                        lblSatPdop.Text = "PDOP: "
                        lblSatHdop.Text = "HDOP: "
                        lblSatVdop.Text = "VDOP: "
                        lblSatsInUse.Text = "Number of Satellites In Use: "
                    End If
                    Application.DoEvents()
                End If
            Else
                drawArrow(g_GPS.Bearing)
                Application.DoEvents()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Module which handles the drawing of the compass arrow when on the main tab
    ''' </summary>
    ''' <param name="Bearing"></param>
    ''' <remarks></remarks>
    Private Sub drawArrow(ByVal Bearing As Double)
        If g_GPS.HasFix Then
            If _oldBearing = -999 Then
                _oldBearing = Bearing
                _forceArrowDraw = True
            End If

            If Fix(_oldBearing) <> Fix(Bearing) Or _forceArrowDraw Then
                Dim g As Graphics = picCompass.CreateGraphics
                Dim currBitMap As Image = My.Resources.Resources.compass_point
                Dim currWidth As Integer = currBitMap.Width
                Dim currHeight As Integer = currBitMap.Height
                Dim transMatrix As New System.Drawing.Drawing2D.Matrix
                Dim pf As New PointF(currWidth / 2, currHeight / 2)
                Dim degrees As Double = Bearing
                transMatrix.RotateAt(degrees, pf)
                g.Transform = transMatrix
                picCompass.Refresh()
                g.DrawImage(currBitMap, New Rectangle(0, 0, currWidth, currHeight), 0, 0, currWidth, currHeight, GraphicsUnit.Pixel)
                _forceArrowDraw = False
                _oldBearing = Bearing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Module which handles labels and display when on the satellite tab
    ''' </summary>
    ''' <param name="CurrSequence"></param>
    ''' <param name="SatsID"></param>
    ''' <param name="SatsAzi"></param>
    ''' <param name="SatsElev"></param>
    ''' <param name="SatsSNR"></param>
    ''' <remarks></remarks>
    Private Sub HandleSatInfoThread(ByVal CurrSequence As Integer, ByVal SatsID() As String, ByVal SatsAzi() As String, ByVal SatsElev() As String, ByVal SatsSNR() As String)
        Dim startIdx As Integer
        Dim currIdx As Integer
        Dim drawSats As Boolean = False

        If Me.Visible And Not chkbxCompassMode.Checked And tbctrlTabs.SelectedIndex = 1 And g_GPS.HasFix Then
            If CurrSequence = 1 Then
                startIdx = 0
            ElseIf CurrSequence = 2 Then
                startIdx = 4
            ElseIf CurrSequence = 3 Then
                startIdx = 8
            End If

            For satBlock As Integer = 1 To 4
                currIdx = startIdx + (satBlock - 1)

                If _oldSatsID(currIdx) <> SatsID(currIdx) Then
                    _oldSatsID(currIdx) = SatsID(currIdx)
                    drawSats = True
                End If
                If _oldSatsAzi(currIdx) <> SatsAzi(currIdx) Then
                    _oldSatsAzi(currIdx) = SatsAzi(currIdx)
                    drawSats = True
                End If
                If _oldSatsElev(currIdx) <> SatsElev(currIdx) Then
                    _oldSatsElev(currIdx) = SatsElev(currIdx)
                    drawSats = True
                End If
                If _oldSatsSNR(currIdx) <> SatsSNR(currIdx) Then
                    _oldSatsSNR(currIdx) = SatsSNR(currIdx)
                    drawSats = True
                End If
            Next
            If drawSats Or _forceSatDraw Then
                drawSatData()
                _forceSatDraw = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Module which handles drawing of the satellite position and signal to noise graphs
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub drawSatData()
        Dim g As Graphics = picSatellites.CreateGraphics()
        Dim g2 As Graphics = picSatBars.CreateGraphics()
        Dim centerX As Integer = picSatellites.Width / 2
        Dim centerY As Integer = picSatellites.Height / 2
        Dim maxRadius As Double = (Math.Min(picSatellites.Height, picSatellites.Width) - 40) / 2
        Dim barboxHeight As Integer = picSatBars.Height
        Dim barboxWidth As Integer = picSatBars.Width
        Dim bottomBarRatio As Double = 0.8
        Dim bardiv As Double = barboxWidth / 12
        Dim barwidth As Integer = Fix(bardiv * 0.7)
        Dim barMaxHeight As Integer = (barboxHeight * bottomBarRatio)
        Dim h As Double
        Dim x, y, barx, barperc As Integer
        Dim penGraphLines As New Pen(Color.Black, 1)
        Dim penSat As New Pen(System.Drawing.Color.LightGoldenrodYellow, 4)
        Dim satFont As Font = New Font("Arial", 8, FontStyle.Regular)

        picSatellites.Refresh()
        picSatBars.Refresh()
        g2.DrawRectangle(penGraphLines, New Rectangle(0, 0, barboxWidth - 2, barboxHeight - 2))
        g2.DrawLine(penGraphLines, New Point(0, barboxHeight * bottomBarRatio), New Point(barboxWidth - 2, Int(barboxHeight * bottomBarRatio)))
        For i As Integer = 0 To 11
            Dim currID As String = _oldSatsID(i)
            Dim currElev As String = _oldSatsElev(i)
            Dim currazi As String = _oldSatsAzi(i)
            Dim currSNR As String = _oldSatsSNR(i)
            If currElev <> "" And currazi <> "" And currSNR <> "" Then
                h = System.Convert.ToDouble(System.Math.Cos(currElev * Math.PI / 180)) * maxRadius
                x = Fix(centerX + h * Math.Sin(currazi * Math.PI / 180))
                y = Fix(centerY - h * Math.Cos(currazi * Math.PI / 180))
                g.DrawRectangle(penSat, x, y, 1, 1)
                g.DrawString(currID, satFont, New System.Drawing.SolidBrush(Color.White), New Point(x - 4, y + 2))

                barx = Fix(bardiv * i) + 2
                barperc = Fix(barMaxHeight * (currSNR * 0.01))
                g2.DrawString(currID, satFont, Brushes.Black, New Point(barx, barMaxHeight + 2))
                g2.FillRectangle(Brushes.Gray, New Rectangle(barx, barMaxHeight - barperc, barwidth, barperc))
                If barperc <> 0 Then
                    g2.DrawString(currSNR, satFont, Brushes.White, New Point(barx - 1, barMaxHeight - barperc))
                End If
            End If
        Next
        Application.DoEvents()
    End Sub

#End Region

End Class