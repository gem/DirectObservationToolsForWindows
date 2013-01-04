'********************************************************************************************************
'File Name: Globals.vb
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

Module Globals
#Region "Base Globals"
    Public g_MapWin As MapWindow.Interfaces.IMapWin
    Public g_handle As Integer
    Public g_StatusBar As System.Windows.Forms.StatusBarPanel
    Public g_DockPanel As System.Windows.Forms.Panel

    Public g_DisplayFrm As New frmDisplayGPS
#End Region

#Region "Menu Globals"
    Public g_mnuGPSMain As String = "gpsMainMenu"
    Public g_mnuCOMConnect As String = "gpsCOMChooseMenu"
    Public g_mnuDisplayGPSInfo As String = "gpsDisplayNmeaMenu"
    Public g_mnuDivider1 As String = "gpsDivMenu"
    Public g_mnuPanCheck As String = "gpsPanCheckMenu"
    Public g_mnuDrawPointCheck As String = "gpsDrawPointCheckMenu"
    Public g_mnuCenterOnPanCheck As String = "gpsCenterOnPanCheckMenu"
    Public g_mnuArrowCheck As String = "gpsArrowCheckMenu"
    Public g_mnuGPSSettings As String = "gpsLogManagerMenu"
    Public g_mnuDivider5 As String = "gpsDiv5Menu"

    Public g_mnuLogTracks As String = "gpsLogTracksMenu"
    Public g_mnuLogNmea As String = "gpsLogNMEAMenu"
#End Region

#Region "Toolbar Globals"
    Public g_ToolBarName As String = "gpsToolbar"
    Public g_btnConnect As String = "gpsConnectBtn"
    Public g_btnDisplay As String = "gpsDisplayBtn"
    Public g_btnGPSSettings As String = "gpsLogManagerBtn"
    Public g_btnDrawPoint As String = "gpsDrawPointBtn"
    Public g_btnDrawArrow As String = "gpsDrawArrowBtn"
    Public g_btnPan As String = "gpsPanBtn"
    Public g_btnCenterOnPan As String = "gpsCenterOnPanBtn"
    Public g_btnDiv1 As String = "gpsDiv1Btn"
    Public g_btnDiv2 As String = "gpsDiv2Btn"
#End Region

#Region "GPS Globals"
    Public g_GPS As New clsGPSController
    Public g_Settings As New clsPersistantGPSSettings

    Public g_promptIfNotConnected As Boolean = True
    Public g_showSettingButtons As Boolean = False

    Public g_ArrowDrawingHandle As Integer = -1

    Public g_SaveNmea As Boolean = False
    Public g_SaveTracks As Boolean = False
    Public g_TrackSF As MapWinGIS.Shapefile = Nothing
    Public g_TrackLineSF As MapWinGIS.Shapefile = Nothing

    Public g_FileInputSpeed As Integer = 50
    Public g_FileInputPath As String = "D:\dev\zSampleData\GPS Data\TestInput.txt"
#End Region

#Region "Global functions"

    ''' <summary>
    ''' Gets a field index from a shapefile by its name
    ''' </summary>
    ''' <param name="sf"></param>
    ''' <param name="currField"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getShapefileFieldByName(ByVal sf As MapWinGIS.Shapefile, ByVal currField As String) As Integer
        For i As Integer = 0 To sf.NumFields - 1
            If sf.Field(i).Name = currField Then
                Return i
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Gets a layer index by a given path
    ''' </summary>
    ''' <param name="currPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLayerIndexByPath(ByVal currPath As String) As Integer
        For i As Integer = 0 To g_MapWin.Layers.NumLayers - 1
            If g_MapWin.Layers.Item(i).FileName = currPath Then
                Return i
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Function used to get a layer path by its name
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLayerPathByName(ByVal strName As String) As String
        Dim i As Integer
        For i = 0 To g_MapWin.Layers.NumLayers - 1
            If g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).Name = strName Then
                Return g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).FileName
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' Function used to get a layer name by its path
    ''' </summary>
    ''' <param name="strPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLayerNameByPath(ByVal strPath As String) As String
        Dim i As Integer
        For i = 0 To g_MapWin.Layers.NumLayers - 1
            If g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).FileName = strPath Then
                Return g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).Name
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' Function used to get a layer index by its name
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLayerIndexByName(ByVal strName As String) As Integer
        Dim i As Integer
        For i = 0 To g_MapWin.Layers.NumLayers - 1
            If g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).Name = strName Then
                Return g_MapWin.Layers.GetHandle(i)
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Function used to check if a layer of a given path exists
    ''' </summary>
    ''' <param name="strPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function layerExists(ByVal strPath As String) As Boolean
        Dim i As Integer
        For i = 0 To g_MapWin.Layers.NumLayers - 1
            If g_MapWin.Layers.Item(g_MapWin.Layers.GetHandle(i)).FileName = strPath Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

End Module
