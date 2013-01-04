'********************************************************************************************************
'File Name: frmGPSSettings.vb
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
Imports System.Drawing
Imports System.Reflection
Public Class frmGPSSettings

#Region "Event Handlers"
    ''' <summary>
    ''' A handler to initialize the log manager form when laoding
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmLogManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim c As Color = Color.Black
        Dim t As Type = c.GetType
        Dim pis As PropertyInfo() = t.GetProperties(BindingFlags.Static Or BindingFlags.DeclaredOnly Or BindingFlags.Public)

        cmbxCrosshairColor.Items.Clear()
        cmbxArrowColor.Items.Clear()
        For i As Integer = 0 To pis.Length - 1
            Dim p As PropertyInfo = pis(i)
            cmbxCrosshairColor.Items.Add(CType(p.GetValue(Nothing, Nothing), Color).Name)
            cmbxArrowColor.Items.Add(CType(p.GetValue(Nothing, Nothing), Color).Name)
        Next
        setTooltips()
        LoadSettings()
    End Sub

    ''' <summary>
    ''' Event handler for closing the form and saving settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        SaveSettings()
        g_Settings.SaveConfig()
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
    ''' A handler to browse to a shape file for logging of GPS location tracks
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTrack.Click
        If Not g_SaveTracks Then
            Dim dlgSave As New System.Windows.Forms.SaveFileDialog
            dlgSave.Filter = "Shapefiles (*.shp)|*.shp"
            dlgSave.OverwritePrompt = False
            dlgSave.AddExtension = True
            dlgSave.DefaultExt = "shp"
            If dlgSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If File.Exists(dlgSave.FileName) Then
                    Dim overwriteRes As System.Windows.Forms.DialogResult = MsgBox("That file already exists. Would you like to overwrite?" + vbNewLine + vbNewLine + "Click Yes to overwrite it." + vbNewLine + "Click No to append to it.", MsgBoxStyle.YesNoCancel, "GPS Settings")
                    If overwriteRes = Windows.Forms.DialogResult.Yes Then
                        MapWinGeoProc.DataManagement.DeleteShapefile(dlgSave.FileName)
                        createNewTrackFile(dlgSave.FileName)
                    ElseIf overwriteRes = Windows.Forms.DialogResult.No Then
                        appendToTrackFile(dlgSave.FileName)
                    Else
                        Return
                    End If
                Else
                    MapWinGeoProc.DataManagement.DeleteShapefile(dlgSave.FileName)
                    createNewTrackFile(dlgSave.FileName)
                End If
                LoadSettings()
            End If
        End If
    End Sub

    ''' <summary>
    ''' A handler to browse to a shape file for logging of GPS location path lines
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseTrackLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTrackLine.Click
        If Not g_SaveTracks Then
            Dim dlgSave As New System.Windows.Forms.SaveFileDialog
            dlgSave.Filter = "Shapefiles (*.shp)|*.shp"
            dlgSave.OverwritePrompt = False
            dlgSave.AddExtension = True
            dlgSave.DefaultExt = "shp"
            If dlgSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If File.Exists(dlgSave.FileName) Then
                    Dim overwriteRes As System.Windows.Forms.DialogResult = MsgBox("That file already exists. Would you like to overwrite?" + vbNewLine + vbNewLine + "Click Yes to overwrite it." + vbNewLine + "Click No to append to it.", MsgBoxStyle.YesNoCancel, "GPS Settings")
                    If overwriteRes = Windows.Forms.DialogResult.Yes Then
                        MapWinGeoProc.DataManagement.DeleteShapefile(dlgSave.FileName)
                        createNewTrackLineFile(dlgSave.FileName)
                    ElseIf overwriteRes = Windows.Forms.DialogResult.No Then
                        txtbxTrackLine.Text = dlgSave.FileName
                        SaveSettings()
                    Else
                        Return
                    End If
                Else
                    MapWinGeoProc.DataManagement.DeleteShapefile(dlgSave.FileName)
                    createNewTrackFile(dlgSave.FileName)
                End If
                LoadSettings()
            End If
        End If
    End Sub

    ''' <summary>
    ''' A handler to browse to a txt file for logging of NMEA sentences
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseNMEA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseNMEA.Click
        If Not g_SaveNmea Then
            Dim dlgSave As New System.Windows.Forms.SaveFileDialog
            dlgSave.Filter = "Text (*.txt)|*.txt"
            dlgSave.OverwritePrompt = False
            dlgSave.AddExtension = True
            dlgSave.DefaultExt = "txt"
            If dlgSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If File.Exists(dlgSave.FileName) Then
                    Dim overwriteRes As System.Windows.Forms.DialogResult = MsgBox("That file already exists. Would you like to overwrite?" + vbNewLine + vbNewLine + "Click Yes to overwrite it." + vbNewLine + "Click No to append to it.", MsgBoxStyle.YesNoCancel, "GPS Settings")
                    If overwriteRes = Windows.Forms.DialogResult.Yes Then
                        txtbxNmeaPath.Text = dlgSave.FileName
                        SaveSettings()
                        File.Delete(g_Settings.NMEALogPath)
                        File.AppendAllText(g_Settings.NMEALogPath, "")
                    ElseIf overwriteRes = Windows.Forms.DialogResult.No Then
                        txtbxNmeaPath.Text = dlgSave.FileName
                        SaveSettings()
                    Else
                        Return
                    End If
                Else
                    txtbxNmeaPath.Text = dlgSave.FileName
                    SaveSettings()
                    File.AppendAllText(g_Settings.NMEALogPath, "")
                End If
                LoadSettings()
            End If
        End If
    End Sub
#End Region

#Region "Helper Functions"
    Private Sub setTooltips()
        'ttip.SetToolTip(
    End Sub

    Private Sub LoadSettings()
        cmbxCrosshairLineWidth.SelectedItem = g_Settings.CrosshairThickness.ToString
        If g_Settings.CrosshairColor.Name <> "" Then
            cmbxCrosshairColor.SelectedItem = g_Settings.CrosshairColor.Name
        End If
        If g_Settings.ArrowColor.Name <> "" Then
            cmbxArrowColor.SelectedItem = g_Settings.ArrowColor.Name
        End If

        chkbxLogDate.Checked = g_Settings.LogDate
        chkbxLogTime.Checked = g_Settings.LogTime
        chkbxLogX.Checked = g_Settings.LogLocX
        chkbxLogY.Checked = g_Settings.LogLocY
        chkbxLogElev.Checked = g_Settings.LogElev
        chkbxLogHDOP.Checked = g_Settings.LogHDOP
        chkbxLogVDOP.Checked = g_Settings.LogVDOP
        chkbxLogPDOP.Checked = g_Settings.LogPDOP
        txtbxTrackPath.Text = g_Settings.TrackPointsLogPath
        txtbxTrackLine.Text = g_Settings.TrackLineLogPath
        txtbxNmeaPath.Text = g_Settings.NMEALogPath

        chkbxSelectAllTrack.Enabled = Not g_SaveTracks
        chkbxLogDate.Enabled = Not g_SaveTracks
        chkbxLogTime.Enabled = Not g_SaveTracks
        chkbxLogX.Enabled = Not g_SaveTracks
        chkbxLogY.Enabled = Not g_SaveTracks
        chkbxLogElev.Enabled = Not g_SaveTracks
        chkbxLogPDOP.Enabled = Not g_SaveTracks
        chkbxLogHDOP.Enabled = Not g_SaveTracks
        chkbxLogVDOP.Enabled = Not g_SaveTracks
        btnBrowseTrack.Enabled = Not g_SaveTracks

        btnBrowseNMEA.Enabled = Not g_SaveNmea


        'txtbxAveSampleNum.Text = g_Settings.SampleAveragingCount.ToString
    End Sub

    Private Sub SaveSettings()
        g_Settings.CrosshairColor = Color.FromName(cmbxCrosshairColor.SelectedItem)
        Try
            g_Settings.CrosshairThickness = Int32.Parse(cmbxCrosshairLineWidth.SelectedItem)
        Catch ex As Exception
            g_Settings.CrosshairThickness = 1
        End Try
        g_Settings.ArrowColor = Color.FromName(cmbxArrowColor.SelectedItem)

        g_Settings.LogDate = chkbxLogDate.Checked
        g_Settings.LogTime = chkbxLogTime.Checked
        g_Settings.LogLocX = chkbxLogX.Checked
        g_Settings.LogLocY = chkbxLogY.Checked
        g_Settings.LogElev = chkbxLogElev.Checked
        g_Settings.LogHDOP = chkbxLogHDOP.Checked
        g_Settings.LogVDOP = chkbxLogVDOP.Checked
        g_Settings.LogPDOP = chkbxLogPDOP.Checked
        g_Settings.TrackPointsLogPath = txtbxTrackPath.Text
        g_Settings.TrackLineLogPath = txtbxTrackLine.Text
        g_Settings.NMEALogPath = txtbxNmeaPath.Text

        Try
            g_Settings.SampleAveragingCount = 1 'Int32.Parse(txtbxAveSampleNum.Text)
        Catch ex As Exception
            g_Settings.SampleAveragingCount = 1
        End Try
    End Sub

    Private Sub createNewTrackFile(ByVal path As String)
        txtbxTrackPath.Text = path
        SaveSettings()
        g_TrackSF = New MapWinGIS.Shapefile
        g_TrackSF.Projection = g_MapWin.Project.ProjectProjection
        g_TrackSF.CreateNew(path, MapWinGIS.ShpfileType.SHP_POINT)
        g_TrackSF.StartEditingShapes()

        Dim mwshapeid As New MapWinGIS.Field
        mwshapeid.Name = "MWShapeID"
        mwshapeid.Type = MapWinGIS.FieldType.INTEGER_FIELD
        g_TrackSF.EditInsertField(mwshapeid, g_TrackSF.NumFields)

        If g_Settings.LogDate Then
            Dim datefield As New MapWinGIS.Field
            datefield.Name = "Date"
            datefield.Type = MapWinGIS.FieldType.STRING_FIELD
            g_TrackSF.EditInsertField(datefield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogTime Then
            Dim timefield As New MapWinGIS.Field
            timefield.Name = "Time"
            timefield.Type = MapWinGIS.FieldType.STRING_FIELD
            g_TrackSF.EditInsertField(timefield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogLocX Then
            Dim locXfield As New MapWinGIS.Field
            locXfield.Name = "LocX"
            locXfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(locXfield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogLocY Then
            Dim locYfield As New MapWinGIS.Field
            locYfield.Name = "LocY"
            locYfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(locYfield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogElev Then
            Dim elevfield As New MapWinGIS.Field
            elevfield.Name = "Elev"
            elevfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(elevfield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogPDOP Then
            Dim pdopfield As New MapWinGIS.Field
            pdopfield.Name = "PDOP"
            pdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(pdopfield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogHDOP Then
            Dim hdopfield As New MapWinGIS.Field
            hdopfield.Name = "HDOP"
            hdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(hdopfield, g_TrackSF.NumFields)
        End If

        If g_Settings.LogVDOP Then
            Dim vdopfield As New MapWinGIS.Field
            vdopfield.Name = "VDOP"
            vdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
            g_TrackSF.EditInsertField(vdopfield, g_TrackSF.NumFields)
        End If

        g_TrackSF.StopEditingShapes()
        g_TrackSF.Close()
        g_TrackSF = Nothing
    End Sub

    Private Sub appendToTrackFile(ByVal path As String)
        txtbxTrackPath.Text = path
        SaveSettings()
        g_TrackSF = New MapWinGIS.Shapefile
        g_TrackSF.Open(g_Settings.TrackPointsLogPath)

        g_TrackSF.StartEditingShapes()
        If g_Settings.LogDate Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "Date")
            If fieldIdx = -1 Then
                Dim datefield As New MapWinGIS.Field
                datefield.Name = "Date"
                datefield.Type = MapWinGIS.FieldType.STRING_FIELD
                g_TrackSF.EditInsertField(datefield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogTime Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "Time")
            If fieldIdx = -1 Then
                Dim timefield As New MapWinGIS.Field
                timefield.Name = "Time"
                timefield.Type = MapWinGIS.FieldType.STRING_FIELD
                g_TrackSF.EditInsertField(timefield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogLocX Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "LocX")
            If fieldIdx = -1 Then

                Dim locXfield As New MapWinGIS.Field
                locXfield.Name = "LocX"
                locXfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(locXfield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogLocY Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "LocY")
            If fieldIdx = -1 Then
                Dim locYfield As New MapWinGIS.Field
                locYfield.Name = "LocY"
                locYfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(locYfield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogElev Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "Elev")
            If fieldIdx = -1 Then
                Dim elevfield As New MapWinGIS.Field
                elevfield.Name = "Elev"
                elevfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(elevfield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogPDOP Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "PDOP")
            If fieldIdx = -1 Then
                Dim pdopfield As New MapWinGIS.Field
                pdopfield.Name = "PDOP"
                pdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(pdopfield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogHDOP Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "HDOP")
            If fieldIdx = -1 Then
                Dim hdopfield As New MapWinGIS.Field
                hdopfield.Name = "HDOP"
                hdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(hdopfield, g_TrackSF.NumFields)
            End If
        End If

        If g_Settings.LogVDOP Then
            Dim fieldIdx As Integer = getShapefileFieldByName(g_TrackSF, "VDOP")
            If fieldIdx = -1 Then
                Dim vdopfield As New MapWinGIS.Field
                vdopfield.Name = "VDOP"
                vdopfield.Type = MapWinGIS.FieldType.DOUBLE_FIELD
                g_TrackSF.EditInsertField(vdopfield, g_TrackSF.NumFields)
            End If
        End If

        g_TrackSF.StopEditingShapes()
        g_TrackSF.Close()
        g_TrackSF = Nothing
    End Sub


    Private Sub createNewTrackLineFile(ByVal path As String)
        txtbxTrackLine.Text = path
        SaveSettings()
        g_TrackLineSF = New MapWinGIS.Shapefile
        g_TrackLineSF.Projection = g_MapWin.Project.ProjectProjection
        g_TrackLineSF.CreateNew(path, MapWinGIS.ShpfileType.SHP_POLYLINE)
        g_TrackLineSF.StartEditingShapes()

        Dim mwshapeid As New MapWinGIS.Field
        mwshapeid.Name = "MWShapeID"
        mwshapeid.Type = MapWinGIS.FieldType.INTEGER_FIELD
        g_TrackLineSF.EditInsertField(mwshapeid, g_TrackLineSF.NumFields)

        g_TrackLineSF.StopEditingShapes()
        g_TrackLineSF.Close()
        g_TrackLineSF = Nothing
    End Sub

#End Region

End Class