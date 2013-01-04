'********************************************************************************************************
'File Name: clsImportExport.vb
'Description: Common utilities relating to import and export of data.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source Utility Library. 
'
'The Initial Developer of this version of the Original Code is Christopher Michaelis, done
'by reshifting and moving about the various utility functions from MapWindow's modPublic.vb
'(which no longer exists) and some useful utility functions from Aqua Terra Consulting.
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'
'********************************************************************************************************

Imports System.Drawing
Imports System.xml
Imports MapWindow

Public Class ColoringSchemeTools
    <CLSCompliant(False)> _
    Public Shared Function ColoringSchemesAreEqual(ByVal LoadedScheme As MapWinGIS.GridColorScheme, ByVal DiskScheme As String) As Boolean
        If IO.File.Exists(DiskScheme) = False Then Return False
        If LoadedScheme Is Nothing Then Return False

        Dim doc As New XmlDocument
        Dim root As XmlElement

        doc.Load(DiskScheme)
        root = doc.DocumentElement

        Dim s1 As MapWinGIS.GridColorScheme = LoadedScheme

        Try
            Dim sch As New MapWinGIS.GridColorScheme
            If root.Attributes("SchemeType").InnerText = "Grid" Then
                If ColoringSchemeTools.ImportScheme(sch, root.Item("GridColoringScheme")) Then
                    ' now I can compare the two
                    With s1
                        If .AmbientIntensity <> sch.AmbientIntensity Then Return False
                        If .LightSourceAzimuth <> sch.LightSourceAzimuth Then Return False
                        If .LightSourceElevation <> sch.LightSourceElevation Then Return False
                        If .LightSourceIntensity <> sch.LightSourceIntensity Then Return False
                        If .NoDataColor.Equals(sch.NoDataColor) = False Then Return False
                        If .NumBreaks <> sch.NumBreaks Then Return False
                    End With

                    Dim i As Integer
                    Dim brk As MapWinGIS.GridColorBreak

                    ' now look at each break
                    For i = 0 To sch.NumBreaks - 1
                        brk = sch.Break(i)
                        With s1.Break(i)

                            If Not ColorsAreEqual(.LowColor, brk.LowColor) Then Return False
                            If Not ColorsAreEqual(.HighColor, brk.HighColor) Then Return False

                            If .Caption <> brk.Caption Then Return False
                            If .ColoringType <> brk.ColoringType Then Return False
                            If .GradientModel <> brk.GradientModel Then Return False
                            'If .HighColor.Equals(brk.HighColor) = False Then Return False
                            If .HighValue <> brk.HighValue Then Return False
                            'If .LowColor.Equals(brk.LowColor) = False Then Return False
                            If .LowValue <> brk.LowValue Then Return False
                        End With
                    Next
                    ' If I've made it this far they are equal.
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            g_error = ex.ToString
            Return Nothing
        End Try
    End Function

    Private Shared Function ColorsAreEqual(ByRef color1 As UInteger, ByRef color2 As UInteger) As Boolean
        If color1 = color2 Then Return True

        Dim clr As UInteger

        clr = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(MapWinUtility.Colors.IntegerToColor(color1).ToArgb())))
        If clr = color2 Then Return True

        clr = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(MapWinUtility.Colors.IntegerToColor(color2).ToArgb())))
        If clr = color1 Then Return True

        Return False
    End Function

    <CLSCompliant(False)> _
    Public Shared Function ImportScheme(ByRef gsh As MapWinGIS.GridColorScheme, ByVal Filename As String, ByRef GridName As String, ByRef GroupName As String) As Object
        Dim doc As New XmlDocument
        Dim root As XmlElement

        doc.Load(Filename)
        root = doc.DocumentElement

        Try
            If root.Attributes("SchemeType").InnerText = "Grid" Then
                If ImportScheme(gsh, root.Item("GridColoringScheme")) Then
                    Return gsh
                End If

                Try
                    If Not root.Attributes("GridName") Is Nothing Then GridName = root.Attributes("GridName").InnerText
                    If Not root.Attributes("GroupName") Is Nothing Then GroupName = root.Attributes("GroupName").InnerText
                Catch
                End Try
            Else
                MapWinUtility.Logger.Msg("File contains invalid coloring scheme type.")
                Return Nothing
            End If

        Catch ex As Exception
            g_error = ex.ToString
            Return Nothing
        End Try

        Return Nothing
    End Function

    <CLSCompliant(False)> _
    Public Shared Function ImportScheme(ByVal lyr As MapWindow.Interfaces.Layer, ByVal Filename As String) As Object
        Dim doc As New XmlDocument
        Dim root As XmlElement

        If lyr Is Nothing Then Return Nothing

        doc.Load(Filename)
        root = doc.DocumentElement


        If lyr.LayerType = Interfaces.eLayerType.LineShapefile OrElse _
          lyr.LayerType = Interfaces.eLayerType.PointShapefile OrElse _
          lyr.LayerType = Interfaces.eLayerType.PolygonShapefile Then
            Try
                Dim sch As New MapWinGIS.ShapefileColorScheme
                If root.Attributes("SchemeType").InnerText = "Shapefile" Then
                    If ImportScheme(sch, CType(lyr.GetObject(), MapWinGIS.Shapefile), root.Item("ShapefileColoringScheme")) Then
                        Return sch
                    End If
                Else
                    mapwinutility.logger.msg("File contains invalid coloring scheme type.")
                    Return Nothing
                End If

            Catch ex As Exception
                g_error = ex.ToString
                Return Nothing
            End Try
        Else
            Try
                Dim sch As New MapWinGIS.GridColorScheme
                If root.Attributes("SchemeType").InnerText = "Grid" Then
                    Try
                        If Not root.Attributes("GridName") Is Nothing Then lyr.Name = root.Attributes("GridName").InnerText
                        If Not root.Attributes("GroupName") Is Nothing Then
                            Dim GroupName As String = root.Attributes("GroupName").InnerText
                            Dim Found As Boolean = False
                            For i As Integer = 0 To frmMain.Legend.Groups.Count - 1
                                If frmMain.Legend.Groups(i).Text.ToLower().Trim() = GroupName.Trim().ToLower() Then
                                    lyr.GroupHandle = frmMain.Legend.Groups(i).Handle
                                    Found = True
                                    Exit For
                                End If
                            Next

                            If Not Found Then
                                Try
                                    lyr.GroupHandle = frmMain.Legend.Groups.Add(GroupName)
                                Catch
                                End Try
                            End If
                        End If
                    Catch
                    End Try
                    If ImportScheme(sch, root.Item("GridColoringScheme")) Then
                        Return sch
                    End If
                Else
                    MapWinUtility.Logger.Msg("File contains invalid coloring scheme type.")
                    Return Nothing
                End If

            Catch ex As Exception
                g_error = ex.ToString
                Return Nothing
            End Try
        End If

        Return Nothing
    End Function

    <CLSCompliant(False)> _
    Private Shared Function ImportScheme(ByRef sch As MapWinGIS.ShapefileColorScheme, ByRef sf As MapWinGIS.Shapefile, ByVal e As XmlElement) As Boolean
        Dim FldName As String
        Dim i As Integer
        Dim brk As MapWinGIS.ShapefileColorBreak
        Dim n As XmlNode
        Dim foundField As Boolean = False

        If e Is Nothing Then Return False

        FldName = e.Attributes("FieldName").InnerText
        For i = 0 To sf.NumFields - 1
            If sf.Field(i).Name.ToLower() = FldName.ToLower() Then
                sch.FieldIndex = i
                foundField = True
                Exit For
            End If
        Next
        If Not foundField Then
            MapWinUtility.Logger.Msg("Could not find the field '" & FldName & "'  Cannot import coloring scheme.")
            Return Nothing
        End If
        sch.Key = e.Attributes("Key").InnerText

        For i = 0 To e.ChildNodes.Count - 1
            n = e.ChildNodes(i)
            brk = New MapWinGIS.ShapefileColorBreak
            brk.Caption = n.Attributes("Caption").InnerText
            brk.StartColor = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(n.Attributes("StartColor").InnerText)))
            brk.EndColor = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(n.Attributes("EndColor").InnerText)))
            brk.StartValue = n.Attributes("StartValue").InnerText
            brk.EndValue = n.Attributes("EndValue").InnerText
            sch.Add(brk)
        Next

        Return True
    End Function

    <CLSCompliant(False)> _
    Public Shared Function ImportScheme(ByRef sch As MapWinGIS.GridColorScheme, ByVal e As XmlElement) As Boolean
        Dim i As Integer
        Dim brk As MapWinGIS.GridColorBreak
        Dim t As String
        Dim azimuth, elevation As Double
        Dim n As XmlNode

        If e Is Nothing Then Return False

        sch.Key = e.Attributes("Key").InnerText

        t = e.Attributes("AmbientIntensity").InnerText
        sch.AmbientIntensity = MapWinUtility.MiscUtils.ParseDouble(t, 0.7) ' CDbl(IIf(IsNumeric(t), CDbl(t), 0.7))

        t = e.Attributes("LightSourceAzimuth").InnerText
        azimuth = MapWinUtility.MiscUtils.ParseDouble(t, 90.0)             ' CDbl(IIf(IsNumeric(t), CDbl(t), 90))

        t = e.Attributes("LightSourceElevation").InnerText
        elevation = MapWinUtility.MiscUtils.ParseDouble(t, 45.0)           'CDbl(IIf(IsNumeric(t), CDbl(t), 45))
        sch.SetLightSource(azimuth, elevation)

        t = e.Attributes("LightSourceIntensity").InnerText
        sch.LightSourceIntensity = MapWinUtility.MiscUtils.ParseDouble(t, 0.7)  'CDbl(IIf(IsNumeric(t), CDbl(t), 0.7))

        For i = 0 To e.ChildNodes.Count - 1
            n = e.ChildNodes(i)
            brk = New MapWinGIS.GridColorBreak
            brk.Caption = n.Attributes("Caption").InnerText
            brk.LowColor = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(n.Attributes("LowColor").InnerText)))
            brk.HighColor = MapWinUtility.Colors.ColorToUInteger(Color.FromArgb(CInt(n.Attributes("HighColor").InnerText)))
            brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(n.Attributes("LowValue").InnerText, 0.0)            'CDbl(n.Attributes("LowValue").InnerText)
            brk.HighValue = MapWinUtility.MiscUtils.ParseDouble(n.Attributes("HighValue").InnerText, 0.0)          'CDbl(n.Attributes("HighValue").InnerText)
            brk.ColoringType = CType(n.Attributes("ColoringType").InnerText, MapWinGIS.ColoringType)
            brk.GradientModel = CType(n.Attributes("GradientModel").InnerText, MapWinGIS.GradientModel)
            sch.InsertBreak(brk)
        Next
        Return True
    End Function

    <CLSCompliant(False)> _
    Public Shared Function ExportScheme(ByRef lyr As Interfaces.Layer, ByVal Path As String) As Boolean
        Dim doc As New XmlDocument
        Dim mainScheme, root As XmlElement
        Dim schemeType As XmlAttribute
        root = doc.CreateElement("ColoringScheme")

        If lyr Is Nothing Then Return False

        If lyr.LayerType = Interfaces.eLayerType.LineShapefile OrElse _
          lyr.LayerType = Interfaces.eLayerType.PointShapefile OrElse _
          lyr.LayerType = Interfaces.eLayerType.PolygonShapefile Then

            Dim sch As MapWinGIS.ShapefileColorScheme = CType(lyr.ColoringScheme, MapWinGIS.ShapefileColorScheme)
            Dim sf As MapWinGIS.Shapefile = CType(lyr.GetObject, MapWinGIS.Shapefile)
            Dim fldName, key As XmlAttribute

            If sch Is Nothing OrElse sch.NumBreaks = 0 Then Return False
            schemeType = doc.CreateAttribute("SchemeType")
            schemeType.InnerText = "Shapefile"
            root.Attributes.Append(schemeType)
            mainScheme = doc.CreateElement("ShapefileColoringScheme")
            fldName = doc.CreateAttribute("FieldName")
            key = doc.CreateAttribute("Key")
            fldName.InnerText = sf.Field(sch.FieldIndex).Name
            key.InnerText = sch.Key
            mainScheme.Attributes.Append(fldName)
            mainScheme.Attributes.Append(key)
            root.AppendChild(mainScheme)
            doc.AppendChild(root)
            If ExportScheme(CType(lyr.ColoringScheme, MapWinGIS.ShapefileColorScheme), doc, mainScheme) Then
                doc.Save(Path)
                Return True
            Else
                MapWinUtility.Logger.Msg("Failed to export coloring scheme.", MsgBoxStyle.Exclamation, "Error")
                Return False
            End If

        ElseIf lyr.LayerType = MapWindow.Interfaces.eLayerType.Grid Then
            Dim sch As MapWinGIS.GridColorScheme = CType(lyr.ColoringScheme, MapWinGIS.GridColorScheme)
            Dim grd As MapWinGIS.Grid = lyr.GetGridObject
            Dim AmbientIntensity, Key, LightSourceAzimuth As XmlAttribute
            Dim LightSourceElevation, LightSourceIntensity, NoDataColor As XmlAttribute
            Dim GridName, GroupName As XmlAttribute
            Dim ImageLayerFillTransparency As XmlAttribute  '4/16/2010 DK
            Dim ImageUpsamplingMethod As XmlAttribute  '5/10/2010 DK
            Dim ImageDownsamplingMethod As XmlAttribute     '5/10/2010 DK

            If sch Is Nothing OrElse sch.NumBreaks = 0 Then Return False
            GridName = doc.CreateAttribute("GridName")
            GroupName = doc.CreateAttribute("GroupName")
            schemeType = doc.CreateAttribute("SchemeType")
            schemeType.InnerText = "Grid"
            root.Attributes.Append(schemeType)
            AmbientIntensity = doc.CreateAttribute("AmbientIntensity")
            Key = doc.CreateAttribute("Key")
            LightSourceAzimuth = doc.CreateAttribute("LightSourceAzimuth")
            LightSourceElevation = doc.CreateAttribute("LightSourceElevation")
            LightSourceIntensity = doc.CreateAttribute("LightSourceIntensity")
            ImageLayerFillTransparency = doc.CreateAttribute("ImageLayerFillTransparency") '4/16/2010 DK
            ImageUpsamplingMethod = doc.CreateAttribute("ImageUpsamplingMethod") '4/16/2010 DK
            ImageDownsamplingMethod = doc.CreateAttribute("ImageDownsamplingMethod") '4/16/2010 DK

            NoDataColor = doc.CreateAttribute("NoDataColor")
            GridName.InnerText = lyr.Name
            GroupName.InnerText = frmMain.Legend.Groups.ItemByHandle(lyr.GroupHandle).Text
            AmbientIntensity.InnerText = CStr(sch.AmbientIntensity)
            Key.InnerText = sch.Key
            LightSourceAzimuth.InnerText = CStr(sch.LightSourceAzimuth)
            LightSourceElevation.InnerText = CStr(sch.LightSourceElevation)
            LightSourceIntensity.InnerText = CStr(sch.LightSourceIntensity)
            NoDataColor.InnerText = CStr(MapWinUtility.Colors.IntegerToColor(sch.NoDataColor).ToArgb)
            ImageLayerFillTransparency.InnerText = CStr(CInt(lyr.ImageLayerFillTransparency * 100) / 100) '4/16/2010 DK

            ' DK - Reads the interpolation method of the layer's image representation
            Dim img As New MapWinGIS.Image()
            img = CType(frmMain.MapMain.get_GetObject(lyr.Handle), MapWinGIS.Image)
            Select Case img.DownsamplingMode
                Case MapWinGIS.tkInterpolationMode.imBicubic
                    ImageDownsamplingMethod.InnerText = "Bicubic"
                Case MapWinGIS.tkInterpolationMode.imBilinear
                    ImageDownsamplingMethod.InnerText = "Bilinear"
                Case MapWinGIS.tkInterpolationMode.imHighQualityBicubic
                    ImageDownsamplingMethod.InnerText = "HighQualityBicubic"
                Case MapWinGIS.tkInterpolationMode.imHighQualityBilinear
                    ImageDownsamplingMethod.InnerText = "HighQualityBilinear"
                Case MapWinGIS.tkInterpolationMode.imNone
                    ImageDownsamplingMethod.InnerText = "None"
                Case Else
                    ImageDownsamplingMethod.InnerText = "None"
            End Select

            Select Case img.UpsamplingMode
                Case MapWinGIS.tkInterpolationMode.imBicubic
                    ImageUpsamplingMethod.InnerText = "Bicubic"
                Case MapWinGIS.tkInterpolationMode.imBilinear
                    ImageUpsamplingMethod.InnerText = "Bilinear"
                Case MapWinGIS.tkInterpolationMode.imHighQualityBicubic
                    ImageUpsamplingMethod.InnerText = "HighQualityBicubic"
                Case MapWinGIS.tkInterpolationMode.imHighQualityBilinear
                    ImageUpsamplingMethod.InnerText = "HighQualityBilinear"
                Case MapWinGIS.tkInterpolationMode.imNone
                    ImageUpsamplingMethod.InnerText = "None"
                Case Else
                    ImageUpsamplingMethod.InnerText = "None"
            End Select

            mainScheme = doc.CreateElement("GridColoringScheme")
            mainScheme.Attributes.Append(AmbientIntensity)
            mainScheme.Attributes.Append(Key)
            mainScheme.Attributes.Append(LightSourceAzimuth)
            mainScheme.Attributes.Append(LightSourceElevation)
            mainScheme.Attributes.Append(LightSourceIntensity)
            mainScheme.Attributes.Append(NoDataColor)
            mainScheme.Attributes.Append(ImageLayerFillTransparency) '4/16/2010 DK
            mainScheme.Attributes.Append(ImageUpsamplingMethod) '5/10/2010 DK
            mainScheme.Attributes.Append(ImageDownsamplingMethod) '5/10/2010 DK

            root.AppendChild(mainScheme)
            root.Attributes.Append(GridName)
            root.Attributes.Append(GroupName)
            doc.AppendChild(root)
            If ExportScheme(CType(lyr.ColoringScheme, MapWinGIS.GridColorScheme), doc, mainScheme) Then
                doc.Save(Path)
                Return True
            Else
                MapWinUtility.Logger.Msg("Failed to export coloring scheme.", MsgBoxStyle.Exclamation, "Error")
                Return False
            End If
        End If
    End Function

    <CLSCompliant(False)> _
    Private Shared Function ExportScheme(ByVal Scheme As MapWinGIS.ShapefileColorScheme, ByVal RootDoc As XmlDocument, ByVal Parent As XmlElement) As Boolean
        Dim i As Integer
        Dim brk As XmlElement
        Dim caption As XmlAttribute
        Dim sValue As XmlAttribute
        Dim eValue As XmlAttribute
        Dim sColor As XmlAttribute
        Dim eColor As XmlAttribute
        Dim curBrk As MapWinGIS.ShapefileColorBreak

        For i = 0 To Scheme.NumBreaks - 1
            curBrk = Scheme.ColorBreak(i)
            brk = RootDoc.CreateElement("Break")
            caption = RootDoc.CreateAttribute("Caption")
            sValue = RootDoc.CreateAttribute("StartValue")
            eValue = RootDoc.CreateAttribute("EndValue")
            sColor = RootDoc.CreateAttribute("StartColor")
            eColor = RootDoc.CreateAttribute("EndColor")
            caption.InnerText = curBrk.Caption
            sValue.InnerText = CStr(curBrk.StartValue)
            eValue.InnerText = CStr(curBrk.EndValue)
            sColor.InnerText = CStr(MapWinUtility.Colors.IntegerToColor(curBrk.StartColor).ToArgb)
            eColor.InnerText = CStr(MapWinUtility.Colors.IntegerToColor(curBrk.EndColor).ToArgb)
            brk.Attributes.Append(caption)
            brk.Attributes.Append(sValue)
            brk.Attributes.Append(eValue)
            brk.Attributes.Append(sColor)
            brk.Attributes.Append(eColor)
            Parent.AppendChild(brk)
            curBrk = Nothing
        Next
        Return True
    End Function

    <CLSCompliant(False)> _
    Private Shared Function ExportScheme(ByVal Scheme As MapWinGIS.GridColorScheme, ByVal RootDoc As XmlDocument, ByVal Parent As XmlElement) As Boolean
        Dim i As Integer
        Dim brk As XmlElement
        Dim caption As XmlAttribute
        Dim sValue As XmlAttribute
        Dim eValue As XmlAttribute
        Dim sColor As XmlAttribute
        Dim eColor As XmlAttribute
        Dim coloringType As XmlAttribute
        Dim gradientModel As XmlAttribute
        Dim curBrk As MapWinGIS.GridColorBreak

        If Scheme Is Nothing OrElse Scheme.NumBreaks = 0 Then Return False

        For i = 0 To Scheme.NumBreaks - 1
            curBrk = Scheme.Break(i)
            brk = RootDoc.CreateElement("Break")
            caption = RootDoc.CreateAttribute("Caption")
            sValue = RootDoc.CreateAttribute("LowValue")
            eValue = RootDoc.CreateAttribute("HighValue")
            sColor = RootDoc.CreateAttribute("LowColor")
            eColor = RootDoc.CreateAttribute("HighColor")
            coloringType = RootDoc.CreateAttribute("ColoringType")
            gradientModel = RootDoc.CreateAttribute("GradientModel")
            caption.InnerText = curBrk.Caption
            sValue.InnerText = CStr(curBrk.LowValue)
            eValue.InnerText = CStr(curBrk.HighValue)
            sColor.InnerText = CStr(MapWinUtility.Colors.IntegerToColor(curBrk.LowColor).ToArgb)
            eColor.InnerText = CStr(MapWinUtility.Colors.IntegerToColor(curBrk.HighColor).ToArgb)
            coloringType.InnerText = CStr(curBrk.ColoringType)
            gradientModel.InnerText = CStr(curBrk.GradientModel)
            brk.Attributes.Append(caption)
            brk.Attributes.Append(sValue)
            brk.Attributes.Append(eValue)
            brk.Attributes.Append(sColor)
            brk.Attributes.Append(eColor)
            brk.Attributes.Append(coloringType)
            brk.Attributes.Append(gradientModel)
            Parent.AppendChild(brk)
            curBrk = Nothing
        Next
        Return True
    End Function

End Class
