'********************************************************************************************************
'File Name: frmLogManager_Load.vb
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

Imports System.io
Public Class frmLogManager

    ''' <summary>
    ''' A handler to initialize the log manager form when laoding
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmLogManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtbxTracksPath.Text = g_TrackPath
        txtbxNmeaPath.Text = g_NmeaPath

        If g_SaveTracks Then
            btnLogTrack.Text = "Stop Logging Tracks to Shapefile"
        Else
            btnLogTrack.Text = "Start Logging Tracks to Shapefile"
        End If
        If g_SaveNmea Then
            btnLogNmea.Text = "Stop Logging NMEA Sentences"
        Else
            btnLogNmea.Text = "Start Logging NMEA Sentences"
        End If
    End Sub

    ''' <summary>
    ''' A handler to select all the track setting checkboxes at once
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkbxSelectAllTrack_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxSelectAllTrack.CheckedChanged
        chkbxLogDate.Checked = chkbxSelectAllTrack.Checked
        chkbxLogTime.Checked = chkbxSelectAllTrack.Checked
        chkbxLogX.Checked = chkbxSelectAllTrack.Checked
        chkbxLogY.Checked = chkbxSelectAllTrack.Checked
        chkbxLogElev.Checked = chkbxSelectAllTrack.Checked
        chkbxLogPDOP.Checked = chkbxSelectAllTrack.Checked
        chkbxLogHDOP.Checked = chkbxSelectAllTrack.Checked
        chkbxLogVDOP.Checked = chkbxSelectAllTrack.Checked
    End Sub

    ''' <summary>
    ''' A handler to start or stop logging of tracks to a shapefile
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogTrack.Click
        If Not g_SaveTracks Then
            Dim dlgSave As New System.Windows.Forms.SaveFileDialog
            dlgSave.Filter = "Shapefiles (*.shp)|*.shp"
            dlgSave.AddExtension = True
            dlgSave.DefaultExt = "shp"
            If dlgSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                g_TrackPath = dlgSave.FileName
                If File.Exists(g_TrackPath) Then
                    MapWinGeoProc.DataManagement.DeleteShapefile(g_TrackPath)
                End If
                g_TrackSF = New MapWinGIS.Shapefile
                g_TrackSF.Projection = g_MapWin.Project.ProjectProjection
                g_TrackSF.CreateNew(g_TrackPath, MapWinGIS.ShpfileType.SHP_POINT)
                g_TrackSF.StartEditingShapes()

                g_LogDate = chkbxLogDate.Checked
                g_LogTime = chkbxLogTime.Checked
                g_LogLocX = chkbxLogX.Checked
                g_LogLocY = chkbxLogY.Checked
                g_LogElev = chkbxLogElev.Checked
                g_LogPDOP = chkbxLogPDOP.Checked
                g_LogHDOP = chkbxLogHDOP.Checked
                g_LogVDOP = chkbxLogVDOP.Checked
                Dim mwshapeid As New MapWinGIS.Field
                mwshapeid.Name = "MWShapeID"
                mwshapeid.Type = MapWinGIS.FieldType.INTEGER_FIELD
                g_TrackSF.EditInsertField(mwshapeid, g_TrackSF.NumFields)

                If g_LogDate Then
                    Dim datefield As New MapWinGIS.Field
                    datefield.Name = "Date"
                    datefield.Type = MapWinGIS.FieldType.STRING_FIELD
                    g_TrackSF.EditInsertField(datefield, g_TrackSF.NumFields)
                End If

                If g_LogTime Then
                    Dim timefield As New MapWinGIS.Field
                    timefield.Name = "Time"
                    timefield.Type = MapWinGIS.FieldType.STRING_FIELD
                    g_TrackSF.EditInsertField(timefield, g_TrackSF.NumFields)
                End If

                If g_LogLocX Then
                    Dim locXfield As New MapWinGIS.Field
                    locXfield.Name = "LocX"
                    locXfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(locXfield, g_TrackSF.NumFields)
                End If

                If g_LogLocY Then
                    Dim locYfield As New MapWinGIS.Field
                    locYfield.Name = "LocY"
                    locYfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(locYfield, g_TrackSF.NumFields)
                End If

                If g_LogElev Then
                    Dim elevfield As New MapWinGIS.Field
                    elevfield.Name = "Elev"
                    elevfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(elevfield, g_TrackSF.NumFields)
                End If

                If g_LogPDOP Then
                    Dim pdopfield As New MapWinGIS.Field
                    pdopfield.Name = "PDOP"
                    pdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(pdopfield, g_TrackSF.NumFields)
                End If

                If g_LogHDOP Then
                    Dim hdopfield As New MapWinGIS.Field
                    hdopfield.Name = "HDOP"
                    hdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(hdopfield, g_TrackSF.NumFields)
                End If

                If g_LogVDOP Then
                    Dim vdopfield As New MapWinGIS.Field
                    vdopfield.Name = "VDOP"
                    vdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                    g_TrackSF.EditInsertField(vdopfield, g_TrackSF.NumFields)
                End If
                chkbxSelectAllTrack.Enabled = False
                chkbxLogDate.Enabled = False
                chkbxLogTime.Enabled = False
                chkbxLogX.Enabled = False
                chkbxLogY.Enabled = False
                chkbxLogElev.Enabled = False
                chkbxLogPDOP.Enabled = False
                chkbxLogHDOP.Enabled = False
                chkbxLogVDOP.Enabled = False

                g_SaveTracks = True
                txtbxTracksPath.Text = g_TrackPath
                btnLogTrack.Text = "Stop Logging"
                g_StatusBar.Text = "GPS Logging Enabled"
            End If
        Else
            If Not g_TrackSF Is Nothing Then
                g_SaveTracks = False
                g_TrackSF.Projection = g_MapWin.Project.ProjectProjection
                g_TrackSF.StopEditingShapes()
                g_TrackSF.Close()
                g_TrackSF = Nothing
                g_TrackPath = ""
            End If
            chkbxSelectAllTrack.Enabled = True
            chkbxLogDate.Enabled = True
            chkbxLogTime.Enabled = True
            chkbxLogX.Enabled = True
            chkbxLogY.Enabled = True
            chkbxLogElev.Enabled = True
            chkbxLogPDOP.Enabled = True
            chkbxLogHDOP.Enabled = True
            chkbxLogVDOP.Enabled = True
            txtbxTracksPath.Text = ""
            btnLogTrack.Text = "Start Logging"
            If Not g_SaveNmea Then
                g_StatusBar.Text = ""
            End If
        End If
    End Sub

    ''' <summary>
    ''' A handler to start or stop logging of NMEA sentences to a text file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogNmea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogNmea.Click
        If Not g_SaveNmea Then
            Dim dlgSave As New System.Windows.Forms.SaveFileDialog
            dlgSave.Filter = "Text (*.txt)|*.txt"
            dlgSave.AddExtension = True
            dlgSave.DefaultExt = "txt"
            If dlgSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                g_NmeaPath = dlgSave.FileName
                If File.Exists(g_NmeaPath) Then
                    File.Delete(g_NmeaPath)
                End If
                File.AppendAllText(g_NmeaPath, "")
                g_SaveNmea = True
                txtbxNmeaPath.Text = g_NmeaPath
                btnLogNmea.Text = "Stop Logging"
                g_StatusBar.Text = "GPS Logging Enabled"
            End If
        Else
            g_NmeaPath = ""
            g_SaveNmea = False
            txtbxNmeaPath.Text = ""
            btnLogNmea.Text = "Start Logging"
            If Not g_SaveTracks Then
                g_StatusBar.Text = ""
            End If
        End If
    End Sub

End Class