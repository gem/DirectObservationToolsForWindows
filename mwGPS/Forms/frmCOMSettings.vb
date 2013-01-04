'********************************************************************************************************
'File Name: frmCOMSettings.vb
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

Public Class frmCOMSettings

    Public CloseOnConnect As Boolean

    ''' <summary>
    ''' Handler to initialize form settings to current port settings on load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCOMSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbxPort.Items.Clear()
        Dim portList As String() = System.IO.Ports.SerialPort.GetPortNames()
        For i As Integer = 0 To portList.Length - 1
            cmbxPort.Items.Add(portList(i))
        Next

        If g_GPS.Port <> "" And cmbxPort.Items.IndexOf(g_GPS.Port) <> -1 Then
            cmbxPort.SelectedItem = g_GPS.Port
        Else
            cmbxPort.SelectedIndex = cmbxPort.Items.Count - 1
        End If

        cmbxBaud.SelectedItem = g_GPS.Baud
        setFinished()
    End Sub

    ''' <summary>
    ''' Handler for the baud being changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbxBaud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxBaud.SelectedIndexChanged
        g_GPS.Baud = cmbxBaud.SelectedItem
    End Sub

    ''' <summary>
    ''' Handler for the port being changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbxPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxPort.SelectedIndexChanged
        g_GPS.Port = cmbxPort.SelectedItem
    End Sub

    ''' <summary>
    ''' Handler for the connect button pressed, which tests the connection with current settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        If g_GPS.IsConnected Then
            g_GPS.ClosePort()
            setFinished()
        Else
            g_GPS.StartReceiving()
            If testConnect(True) Then
                If CloseOnConnect Then
                    Me.Hide()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handler for the autoconnect button, which tries every combination of ports and bauds til it finds a GPS connection or runs out of combinations
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAutoconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoconnect.Click
        If Not g_GPS.IsConnected Then
            For i As Integer = 0 To cmbxPort.Items.Count - 1
                cmbxPort.SelectedIndex = i
                For j As Integer = 0 To cmbxBaud.Items.Count - 1
                    cmbxBaud.SelectedIndex = j
                    If testConnect(False) Then
                        setFinished()
                        If CloseOnConnect Then
                            Me.Hide()
                        End If
                        Exit Sub
                    End If
                Next
            Next
        End If
        setFinished()
    End Sub

    ''' <summary>
    ''' Function which tests the current connections and captures GPS controller exceptions
    ''' </summary>
    ''' <param name="showMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function testConnect(ByVal showMessage As Boolean) As Boolean
        Dim result As Boolean = False
        setConnecting()

        Try
            result = g_GPS.TestConnectSettings(showMessage)
        Catch ex As Exception
            If ex.Message = "BadCOMSettings" Then
                MsgBox("The COM Port and Baud Rate specified did not allow a connection. Please try other settings.", MsgBoxStyle.Exclamation, "Connection Attempt")
            ElseIf ex.Message = "NotConnectedGPS" Then
                MsgBox("The device on this port does not appear to be a NMEA 0183-compliant GPS unit. Retry these settings if you believe it to be correct. Otherwise, please try other settings.", MsgBoxStyle.Exclamation, "Connection Attempt")
            End If
        End Try

        If showMessage Then
            setFinished()
        End If
        Return result
    End Function

    ''' <summary>
    ''' sets the form into a connecting mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setConnecting()
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        btnConnect.Text = "Connecting"
        btnConnect.Enabled = False
        btnAutoconnect.Enabled = False
        btnClose.Enabled = False
        cmbxBaud.Enabled = False
        cmbxPort.Enabled = False
        System.Windows.Forms.Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Sets the form into a finished mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setFinished()
        btnConnect.Enabled = True
        btnClose.Enabled = True
        If g_GPS.IsConnected Then
            btnConnect.Text = "Disconnect"
            btnAutoconnect.Enabled = False
            cmbxBaud.Enabled = False
            cmbxPort.Enabled = False
        Else
            btnConnect.Text = "Connect"
            btnAutoconnect.Enabled = True
            cmbxBaud.Enabled = True
            cmbxPort.Enabled = True
        End If
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

End Class