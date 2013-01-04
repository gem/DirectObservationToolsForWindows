Imports System.Xml
Public Class clsPersistantGPSSettings
    Public ConfigFileName As String

    Public DrawLocation As Boolean
    Public DrawArrow As Boolean
    Public CrosshairThickness As Integer
    Public CrosshairColor As Drawing.Color
    Public ArrowColor As Drawing.Color
    Public PanWithGPS As Boolean
    Public CenterOnPan As Boolean

    Public TrackPointsLogPath As String
    Public TrackLineLogPath As String
    Public NMEALogPath As String
    Public LogDate As Boolean
    Public LogTime As Boolean
    Public LogLocX As Boolean
    Public LogLocY As Boolean
    Public LogElev As Boolean
    Public LogPDOP As Boolean
    Public LogHDOP As Boolean
    Public LogVDOP As Boolean

    Public SampleAveragingCount As Integer

    Public Sub Initialize()
        ConfigFileName = IO.Path.GetDirectoryName(g_MapWin.Project.ConfigFileName) + "\gps.cfg"

        DrawLocation = True
        DrawArrow = True
        CrosshairThickness = 1
        CrosshairColor = Drawing.Color.Black
        ArrowColor = Drawing.Color.Red
        PanWithGPS = True
        CenterOnPan = True

        TrackPointsLogPath = ""
        TrackLineLogPath = ""
        NMEALogPath = ""
        LogDate = True
        LogTime = True
        LogLocX = True
        LogLocY = True
        LogElev = True
        LogPDOP = True
        LogHDOP = True
        LogVDOP = True

        SampleAveragingCount = 1
    End Sub

    Public Sub LoadConfig()
        Dim Doc As New XmlDocument
        Dim Root As XmlElement

        Try
            If System.IO.File.Exists(ConfigFileName) Then
                Doc = New XmlDocument
                Doc.Load(ConfigFileName)
                Root = Doc.DocumentElement

                With Root.Item("Drawing_Settings")
                    DrawLocation = .ChildNodes(0).Attributes(0).InnerText
                    DrawArrow = .ChildNodes(1).Attributes(0).InnerText
                    CrosshairThickness = .ChildNodes(2).Attributes(0).InnerText
                    CrosshairColor = Drawing.Color.FromName(.ChildNodes(3).Attributes(0).InnerText)
                    ArrowColor = Drawing.Color.FromName(.ChildNodes(4).Attributes(0).InnerText)
                    PanWithGPS = .ChildNodes(5).Attributes(0).InnerText
                    CenterOnPan = .ChildNodes(6).Attributes(0).InnerText
                End With

                With Root.Item("Log_Settings")
                    TrackPointsLogPath = .ChildNodes(0).Attributes(0).InnerText
                    TrackLineLogPath = .ChildNodes(1).Attributes(0).InnerText
                    NMEALogPath = .ChildNodes(2).Attributes(0).InnerText
                    LogDate = .ChildNodes(3).Attributes(0).InnerText
                    LogTime = .ChildNodes(4).Attributes(0).InnerText
                    LogLocX = .ChildNodes(5).Attributes(0).InnerText
                    LogLocY = .ChildNodes(6).Attributes(0).InnerText
                    LogElev = .ChildNodes(7).Attributes(0).InnerText
                    LogPDOP = .ChildNodes(8).Attributes(0).InnerText
                    LogHDOP = .ChildNodes(9).Attributes(0).InnerText
                    LogVDOP = .ChildNodes(10).Attributes(0).InnerText
                End With

                With Root.Item("Sampling_Settings")
                    SampleAveragingCount = .ChildNodes(2).Attributes(0).InnerText
                End With
            Else
                Initialize()
                SaveConfig()
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

#Region "Save Config"
    Public Function SaveConfig() As Boolean
        Dim doc As New Xml.XmlDocument
        Dim Root As XmlElement
        Dim Ver As String

        Try
            Ver = 1.0 'App.VersionString()
            doc.LoadXml("<GPS_Settings type=""configfile"" version=""" + Ver + """>" + vbNewLine + "</GPS_Settings>")
            Root = doc.DocumentElement

            AddSettingsXML(doc, Root)

            doc.Save(ConfigFileName)
            Return True
        Catch ex As System.Exception
            Return False
        End Try
    End Function

    Private Sub AddSettingsXML(ByRef doc As XmlDocument, ByRef Parent As XmlElement)
        Dim drawingSettingsXML As XmlElement = doc.CreateElement("Drawing_Settings")

        Dim setDrawGPSLocation As XmlNode = doc.CreateNode(XmlNodeType.Element, "Draw_GPS_Location", "")
        Dim setDrawGPSLocationVal As XmlAttribute = doc.CreateAttribute("value")
        setDrawGPSLocationVal.InnerText = DrawLocation
        drawingSettingsXML.AppendChild(setDrawGPSLocation).Attributes().Append(setDrawGPSLocationVal)

        Dim setDrawGPSArrow As XmlNode = doc.CreateNode(XmlNodeType.Element, "Draw_Location_As_Arrow", "")
        Dim setDrawGPSArrowVal As XmlAttribute = doc.CreateAttribute("value")
        setDrawGPSArrowVal.InnerText = DrawArrow
        drawingSettingsXML.AppendChild(setDrawGPSArrow).Attributes().Append(setDrawGPSArrowVal)

        Dim setCrosshairThickness As XmlNode = doc.CreateNode(XmlNodeType.Element, "Location_Crosshair_Thickness", "")
        Dim setCrosshairThicknessVal As XmlAttribute = doc.CreateAttribute("value")
        setCrosshairThicknessVal.InnerText = CrosshairThickness.ToString
        drawingSettingsXML.AppendChild(setCrosshairThickness).Attributes().Append(setCrosshairThicknessVal)

        Dim setCrosshairColor As XmlNode = doc.CreateNode(XmlNodeType.Element, "Location_Crosshair_Color", "")
        Dim setCrosshairColorVal As XmlAttribute = doc.CreateAttribute("value")
        setCrosshairColorVal.InnerText = CrosshairColor.Name
        drawingSettingsXML.AppendChild(setCrosshairColor).Attributes().Append(setCrosshairColorVal)

        Dim setArrowColor As XmlNode = doc.CreateNode(XmlNodeType.Element, "Location_Arrow_Color", "")
        Dim setArrowColorVal As XmlAttribute = doc.CreateAttribute("value")
        setArrowColorVal.InnerText = ArrowColor.Name
        drawingSettingsXML.AppendChild(setArrowColor).Attributes().Append(setArrowColorVal)

        Dim setPanWithGPS As XmlNode = doc.CreateNode(XmlNodeType.Element, "Pan_With_GPS_Location", "")
        Dim setPanWithGPSVal As XmlAttribute = doc.CreateAttribute("value")
        setPanWithGPSVal.InnerText = PanWithGPS
        drawingSettingsXML.AppendChild(setPanWithGPS).Attributes().Append(setPanWithGPSVal)

        Dim setCenterOnPan As XmlNode = doc.CreateNode(XmlNodeType.Element, "Center_On_Pan", "")
        Dim setCenterOnPanVal As XmlAttribute = doc.CreateAttribute("value")
        setCenterOnPanVal.InnerText = CenterOnPan
        drawingSettingsXML.AppendChild(setCenterOnPan).Attributes().Append(setCenterOnPanVal)

        Parent.AppendChild(drawingSettingsXML)

        Dim logSettingsXML As XmlElement = doc.CreateElement("Log_Settings")

        Dim setTrackPointPath As XmlNode = doc.CreateNode(XmlNodeType.Element, "Track_Log_Points_Path", "")
        Dim setTrackPointPathVal As XmlAttribute = doc.CreateAttribute("value")
        setTrackPointPathVal.InnerText = TrackPointsLogPath
        logSettingsXML.AppendChild(setTrackPointPath).Attributes().Append(setTrackPointPathVal)

        Dim setTracklinePath As XmlNode = doc.CreateNode(XmlNodeType.Element, "Track_Log_Lines_Path", "")
        Dim setTracklinePathVal As XmlAttribute = doc.CreateAttribute("value")
        setTracklinePathVal.InnerText = TrackLineLogPath
        logSettingsXML.AppendChild(setTracklinePath).Attributes().Append(setTracklinePathVal)

        Dim setNMEALogPath As XmlNode = doc.CreateNode(XmlNodeType.Element, "NMEA_Log_Path", "")
        Dim setNMEALogPathVal As XmlAttribute = doc.CreateAttribute("value")
        setNMEALogPathVal.InnerText = NMEALogPath
        logSettingsXML.AppendChild(setNMEALogPath).Attributes().Append(setNMEALogPathVal)

        Dim setlogDate As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_Date", "")
        Dim setlogDateVal As XmlAttribute = doc.CreateAttribute("value")
        setlogDateVal.InnerText = LogDate
        logSettingsXML.AppendChild(setlogDate).Attributes().Append(setlogDateVal)

        Dim setlogtime As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_Time", "")
        Dim setlogtimeVal As XmlAttribute = doc.CreateAttribute("value")
        setlogtimeVal.InnerText = LogTime
        logSettingsXML.AppendChild(setlogtime).Attributes().Append(setlogtimeVal)

        Dim setlogX As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_Location_X", "")
        Dim setlogXVal As XmlAttribute = doc.CreateAttribute("value")
        setlogXVal.InnerText = LogLocX
        logSettingsXML.AppendChild(setlogX).Attributes().Append(setlogXVal)

        Dim setlogY As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_Location_Y", "")
        Dim setlogYVal As XmlAttribute = doc.CreateAttribute("value")
        setlogYVal.InnerText = LogLocY
        logSettingsXML.AppendChild(setlogY).Attributes().Append(setlogYVal)

        Dim setlogElev As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_Elevation", "")
        Dim setlogElevVal As XmlAttribute = doc.CreateAttribute("value")
        setlogElevVal.InnerText = LogElev
        logSettingsXML.AppendChild(setlogElev).Attributes().Append(setlogElevVal)

        Dim setlogPDOP As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_PDOP", "")
        Dim setlogPDOPVal As XmlAttribute = doc.CreateAttribute("value")
        setlogPDOPVal.InnerText = LogPDOP
        logSettingsXML.AppendChild(setlogPDOP).Attributes().Append(setlogPDOPVal)

        Dim setlogHDOP As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_HDOP", "")
        Dim setlogHDOPVal As XmlAttribute = doc.CreateAttribute("value")
        setlogHDOPVal.InnerText = LogHDOP
        logSettingsXML.AppendChild(setlogHDOP).Attributes().Append(setlogHDOPVal)

        Dim setlogVDOP As XmlNode = doc.CreateNode(XmlNodeType.Element, "Log_VDOP", "")
        Dim setlogVDOPVal As XmlAttribute = doc.CreateAttribute("value")
        setlogVDOPVal.InnerText = LogVDOP
        logSettingsXML.AppendChild(setlogVDOP).Attributes().Append(setlogVDOPVal)

        Parent.AppendChild(logSettingsXML)

        Dim sampleSettingsXML As XmlElement = doc.CreateElement("Sampling_Settings")

        Dim setAveragePoints As XmlNode = doc.CreateNode(XmlNodeType.Element, "Default_Sample_Averaging_Point_Count", "")
        Dim setAveragePointsVal As XmlAttribute = doc.CreateAttribute("value")
        setAveragePointsVal.InnerText = SampleAveragingCount
        sampleSettingsXML.AppendChild(setAveragePoints).Attributes().Append(setAveragePointsVal)

        Parent.AppendChild(sampleSettingsXML)
    End Sub
#End Region

End Class
