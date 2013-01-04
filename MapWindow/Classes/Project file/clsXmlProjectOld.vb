'********************************************************************************************************
' Filename:  clsXMLProjectOld.vb
' Description: support for the old symbology in the project file. W
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'05-apr-2011 - sleschinski - copied the code form the clsXmlProjectFile

Imports System.Xml

Partial Friend Class XmlProjectFile

    ''' <summary>
    ''' Saves shapefile properties
    ''' </summary>
    Private Sub AddShapeFileElement(ByRef m_Doc As Xml.XmlDocument, ByVal shpFileLayer As Interfaces.Layer, ByVal parent As Xml.XmlNode)
        Dim shpFileProp As XmlElement = m_Doc.CreateElement("ShapeFileProperties")
        Dim color As XmlAttribute = m_Doc.CreateAttribute("Color")
        Dim drawFill As XmlAttribute = m_Doc.CreateAttribute("DrawFill")
        Dim transPercent As XmlAttribute = m_Doc.CreateAttribute("TransparencyPercent")
        Dim fillStipple As XmlAttribute = m_Doc.CreateAttribute("FillStipple")
        Dim lineOrPointSize As XmlAttribute = m_Doc.CreateAttribute("LineOrPointSize")
        Dim lineStipple As XmlAttribute = m_Doc.CreateAttribute("LineStipple")
        Dim outLineColor As XmlAttribute = m_Doc.CreateAttribute("OutLineColor")
        Dim pointType As XmlAttribute = m_Doc.CreateAttribute("PointType")
        Dim customFillStipple As XmlAttribute = m_Doc.CreateAttribute("CustomFillStipple")
        Dim customLineStipple As XmlAttribute = m_Doc.CreateAttribute("CustomLineStipple")
        Dim customPointType As XmlElement = m_Doc.CreateElement("CustomPointType")
        Dim useTransparency As XmlAttribute = m_Doc.CreateAttribute("UseTransparency")
        Dim transparencyColor As XmlAttribute = m_Doc.CreateAttribute("TransparencyColor")
        Dim MapTooltipField As XmlAttribute = m_Doc.CreateAttribute("MapTooltipField")
        Dim MapTooltipsEnabled As XmlAttribute = m_Doc.CreateAttribute("MapTooltipsEnabled")
        Dim VertVisible As XmlAttribute = m_Doc.CreateAttribute("VerticesVisible")
        Dim LabelsVisible As XmlAttribute = m_Doc.CreateAttribute("LabelsVisible")
        Dim FillStippleTransparent As XmlAttribute = m_Doc.CreateAttribute("FillStippleTransparent")
        Dim FillStippleLineColor As XmlAttribute = m_Doc.CreateAttribute("FillStippleLineColor")

        'set the properties of the shpfile
        With shpFileLayer
            If .LayerType = Interfaces.eLayerType.PointShapefile Then
                'Vertices are always visible - layer visibility is used to
                'toggle overall visibility here.
                VertVisible.InnerText = "True"
            Else
                VertVisible.InnerText = .VerticesVisible.ToString()
            End If
            LabelsVisible.InnerText = .LabelsVisible.ToString()
            color.InnerText = RGB(.Color.R, .Color.G, .Color.B).ToString
            drawFill.InnerText = .DrawFill.ToString
            fillStipple.InnerText = CInt(.FillStipple).ToString
            lineOrPointSize.InnerText = .LineOrPointSize.ToString
            lineStipple.InnerText = CInt(.LineStipple).ToString
            outLineColor.InnerText = RGB(.OutlineColor.R, .OutlineColor.G, .OutlineColor.B).ToString
            pointType.InnerText = CInt(.PointType).ToString
            ' customFillStipple.InnerText = "".UserFillStipple
            customLineStipple.InnerText = .UserLineStipple.ToString
            transPercent.InnerText = .ShapeLayerFillTransparency.ToString()
            FillStippleTransparent.InnerText = .FillStippleTransparency.ToString()
            FillStippleLineColor.InnerText = .FillStippleLineColor.ToArgb().ToString()

            If .PointType = MapWinGIS.tkPointType.ptUserDefined Then
                SaveImage(m_Doc, .UserPointType.Picture, customPointType)
                useTransparency.InnerText = .UserPointType.UseTransparencyColor.ToString
                transparencyColor.InnerText = .UserPointType.TransparencyColor.ToString()
            Else
                SaveImage(m_Doc, Nothing, customPointType)
                useTransparency.InnerText = "False"
                transparencyColor.InnerText = "0"
            End If
        End With

        Try
            MapTooltipField.InnerText = frmMain.Legend.Layers.ItemByHandle(shpFileLayer.Handle).MapTooltipFieldIndex.ToString()
            MapTooltipsEnabled.InnerText = frmMain.Legend.Layers.ItemByHandle(shpFileLayer.Handle).MapTooltipsEnabled.ToString()
        Catch
        End Try

        'add the attributes
        shpFileProp.Attributes.Append(LabelsVisible)
        shpFileProp.Attributes.Append(MapTooltipField)
        shpFileProp.Attributes.Append(MapTooltipsEnabled)
        shpFileProp.Attributes.Append(VertVisible)
        shpFileProp.Attributes.Append(color)
        shpFileProp.Attributes.Append(drawFill)
        shpFileProp.Attributes.Append(transPercent)
        shpFileProp.Attributes.Append(fillStipple)
        shpFileProp.Attributes.Append(lineOrPointSize)
        shpFileProp.Attributes.Append(lineStipple)
        shpFileProp.Attributes.Append(outLineColor)
        shpFileProp.Attributes.Append(pointType)
        ' shpFileProp.Attributes.Append(customFillStipple)
        shpFileProp.Attributes.Append(customLineStipple)
        shpFileProp.Attributes.Append(useTransparency)
        shpFileProp.Attributes.Append(transparencyColor)
        shpFileProp.Attributes.Append(FillStippleTransparent)
        shpFileProp.Attributes.Append(FillStippleLineColor)

        shpFileProp.AppendChild(customPointType)

        'add the legend properties
        If Not shpFileLayer.ColoringScheme Is Nothing Then
            AddLegendElement(m_Doc, CType(shpFileLayer.ColoringScheme, MapWinGIS.ShapefileColorScheme), shpFileProp, shpFileLayer.Handle)
        End If

        If shpFileLayer.LayerType = Interfaces.eLayerType.PointShapefile AndAlso frmMain.Legend.Layers.ItemByHandle(shpFileLayer.Handle).PointImageScheme IsNot Nothing Then
            SerializePointImageScheme(frmMain.Legend.Layers.ItemByHandle(shpFileLayer.Handle).PointImageScheme, m_Doc, shpFileProp)
        End If

        If shpFileLayer.LayerType = Interfaces.eLayerType.PolygonShapefile AndAlso frmMain.m_FillStippleSchemes.Contains(shpFileLayer.Handle) Then
            SerializeFillStippleScheme(frmMain.m_FillStippleSchemes(shpFileLayer.Handle), m_Doc, shpFileProp)
        End If

        ' tws 04/29/2007
        AddShapeListElement(m_Doc, shpFileLayer, shpFileProp)

        parent.AppendChild(shpFileProp)
    End Sub

    ' tws 04/29/2007
    ' direct reference to the map here breaks the nesting of these routines
    ' but the per-shape formatting is only visible there, AFAIK... "sorry"
    ' anyway this is not the first ref in this stack to frmMain.MapMain.
    Private Sub AddShapeListElement(ByRef m_Doc As Xml.XmlDocument, ByVal sfl As Interfaces.Layer, ByVal parent As Xml.XmlNode)
        If Not Me.SaveShapeSettings Then Return
        Dim shpPropList As XmlElement = m_Doc.CreateElement("ShapePropertiesList")
        Dim axmap As AxMapWinGIS.AxMap = frmMain.MapMain
        Try
            Dim ccv As New System.Drawing.ColorConverter
            For i As Integer = 0 To sfl.Shapes.NumShapes - 1
                ' add any per-shape settings that differ from the layer
                Dim sProps As XmlElement = m_Doc.CreateElement("ShapeProperties")
                Dim shapeIndex As XmlAttribute = m_Doc.CreateAttribute("ShapeIndex")
                shapeIndex.InnerText = i
                sProps.Attributes.Append(shapeIndex)
                Dim xmla As XmlAttribute = Nothing

                ' we can't(?) get the ShapeInfo directly, 
                ' so we have to query the map for each of the shape properties
                ' that may need to save, if they differ from the layer-level settings
                If sfl.LayerType = Interfaces.eLayerType.LineShapefile _
                Or sfl.LayerType = Interfaces.eLayerType.PolygonShapefile Then
                    ' line and polygon share all the line properties
                    Dim sLD As Boolean = axmap.get_ShapeDrawLine(sfl.Handle, i)
                    Dim sLC As Color = axmap.get_ShapeLineColor(sfl.Handle, i)
                    Dim sLW = axmap.get_ShapeLineWidth(sfl.Handle, i)
                    Dim sLS = axmap.get_ShapeLineStipple(sfl.Handle, i)
                    If (Not sLD) Then ' layer does not have equivalent boolean, one sets linewidth to 0 or > 0
                        xmla = m_Doc.CreateAttribute("DrawLine")
                        xmla.InnerText = sLD
                        sProps.Attributes.Append(xmla)
                    End If
                    If (sLC <> sfl.Color) Then
                        xmla = m_Doc.CreateAttribute("LineColor")
                        xmla.InnerText = sLC.ToArgb
                        sProps.Attributes.Append(xmla)
                    End If
                    If (sLW <> sfl.LineOrPointSize) Then
                        xmla = m_Doc.CreateAttribute("LineWidth")
                        xmla.InnerText = sLW
                        sProps.Attributes.Append(xmla)
                    End If
                    If (sLS <> sfl.LineStipple) Then
                        xmla = m_Doc.CreateAttribute("LineStyle")
                        xmla.InnerText = sLS
                        sProps.Attributes.Append(xmla)
                    End If
                    If sfl.LayerType = Interfaces.eLayerType.PolygonShapefile Then
                        ' but polygons have fill props too
                        Dim sFD As Boolean = axmap.get_ShapeDrawFill(sfl.Handle, i)
                        Dim sFT = axmap.get_ShapeFillTransparency(sfl.Handle, i)
                        Dim sFC As Color = axmap.get_ShapeFillColor(sfl.Handle, i)
                        Dim sFS = axmap.get_ShapeFillStipple(sfl.Handle, i)
                        If (sFD <> sfl.DrawFill) Then
                            xmla = m_Doc.CreateAttribute("DrawFill")
                            xmla.InnerText = sFD
                            sProps.Attributes.Append(xmla)
                        End If
                        If (sFC <> sfl.Color) Then
                            xmla = m_Doc.CreateAttribute("FillColor")
                            xmla.InnerText = sFC.ToArgb
                            sProps.Attributes.Append(xmla)
                        End If
                        If (sFT <> sfl.ShapeLayerFillTransparency) Then
                            xmla = m_Doc.CreateAttribute("FillTransparency")
                            xmla.InnerText = sFT
                            sProps.Attributes.Append(xmla)
                        End If
                        If (sFS <> sfl.FillStipple) Then
                            xmla = m_Doc.CreateAttribute("FillStyle")
                            xmla.InnerText = sFS
                            sProps.Attributes.Append(xmla)
                        End If
                    End If
                ElseIf sfl.LayerType = Interfaces.eLayerType.PointShapefile Then
                    Dim sPD As Boolean = axmap.get_ShapeDrawPoint(sfl.Handle, i)
                    Dim sPC As Color = axmap.get_ShapePointColor(sfl.Handle, i)
                    Dim sPW = axmap.get_ShapePointSize(sfl.Handle, i)
                    If (Not sPD) Then ' layer does not have equivalent boolean, one sets linewidth to 0 or > 0
                        xmla = m_Doc.CreateAttribute("DrawPoint")
                        xmla.InnerText = sPD
                        sProps.Attributes.Append(xmla)
                    End If
                    If (sPC <> sfl.Color) Then
                        xmla = m_Doc.CreateAttribute("PointColor")
                        xmla.InnerText = sPC.ToArgb
                        sProps.Attributes.Append(xmla)
                    End If
                    If (sPW <> sfl.LineOrPointSize) Then
                        xmla = m_Doc.CreateAttribute("PointSize")
                        xmla.InnerText = sPW
                        sProps.Attributes.Append(xmla)
                    End If
                End If

                ' if we added any, include this shape in the list
                If sProps.Attributes.Count > 1 Then
                    shpPropList.AppendChild(sProps)
                End If
            Next
            parent.AppendChild(shpPropList)
        Catch e As Exception
            m_ErrorMsg += "Error in AddShapeListElement(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub AddLegendElement(ByRef m_Doc As Xml.XmlDocument, ByVal legend As MapWinGIS.ShapefileColorScheme, ByVal parent As XmlElement, ByVal handle As Integer)
        Dim leg As XmlElement = m_Doc.CreateElement("Legend")
        Dim colorBreaks As XmlElement = m_Doc.CreateElement("ColorBreaks")
        Dim fieldIndex As XmlAttribute = m_Doc.CreateAttribute("FieldIndex")
        Dim SchemeCaption As XmlAttribute = m_Doc.CreateAttribute("SchemeCaption")
        Dim key As XmlAttribute = m_Doc.CreateAttribute("Key")
        Dim numBreaks As XmlAttribute = m_Doc.CreateAttribute("NumberOfBreaks")
        Dim i As Integer

        'set the properties of the legend
        If Not legend Is Nothing Then
            With legend
                fieldIndex.InnerText = .FieldIndex.ToString
                key.InnerText = .Key
                numBreaks.InnerText = .NumBreaks.ToString
            End With
        End If

        Try
            'Note - don't use handle on legend object here - tends to be incorrect(?)
            SchemeCaption.InnerText = frmMain.Legend.Layers.ItemByHandle(handle).ColorSchemeFieldCaption
        Catch
            SchemeCaption.InnerText = ""
        End Try

        'add the elements to the legend
        leg.Attributes.Append(fieldIndex)
        leg.Attributes.Append(key)
        leg.Attributes.Append(numBreaks)

        If Not legend Is Nothing Then
            'add the elements to the colorBreaks
            For i = 0 To legend.NumBreaks - 1
                AddColorBreaksElement(m_Doc, legend.ColorBreak(i), colorBreaks)
            Next
        End If
        leg.AppendChild(colorBreaks)
        leg.Attributes.Append(SchemeCaption)

        parent.AppendChild(leg)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub AddColorBreaksElement(ByRef m_Doc As Xml.XmlDocument, ByVal colorBreak As MapWinGIS.ShapefileColorBreak, ByVal parent As XmlElement)
        Dim break As XmlElement = m_Doc.CreateElement("Break")
        Dim endColor As XmlAttribute = m_Doc.CreateAttribute("EndColor")
        Dim endValue As XmlAttribute = m_Doc.CreateAttribute("EndValue")
        Dim startColor As XmlAttribute = m_Doc.CreateAttribute("StartColor")
        Dim StartValue As XmlAttribute = m_Doc.CreateAttribute("StartValue")
        Dim caption As XmlAttribute = m_Doc.CreateAttribute("Caption")
        Dim Visible As XmlAttribute = m_Doc.CreateAttribute("Visible")

        'set the properties
        With colorBreak
            endColor.InnerText = .EndColor.ToString
            If .EndValue Is Nothing Then
                endValue.InnerText = "(null)"
            Else
                endValue.InnerText = .EndValue.ToString
            End If
            startColor.InnerText = .StartColor.ToString
            If .StartValue Is Nothing Then
                StartValue.InnerText = "(null)"
            Else
                StartValue.InnerText = .StartValue.ToString
            End If
            caption.InnerText = .Caption
            Visible.InnerText = .Visible.ToString()
        End With

        'add the elements to the break
        break.Attributes.Append(startColor)
        break.Attributes.Append(endColor)
        break.Attributes.Append(StartValue)
        break.Attributes.Append(endValue)
        break.Attributes.Append(caption)
        break.Attributes.Append(Visible)

        parent.AppendChild(break)

    End Sub

    ''' <summary>
    ''' Saves image properties and grid color schemes
    ''' </summary>
    Private Sub AddGridElement(ByRef m_Doc As Xml.XmlDocument, ByVal gridFileLayer As Interfaces.Layer, ByVal parent As Xml.XmlNode)
        Dim grid As XmlElement = m_Doc.CreateElement("GridProperty")
        Dim transparentColor As XmlAttribute = m_Doc.CreateAttribute("TransparentColor")
        Dim transparentColor2 As XmlAttribute = m_Doc.CreateAttribute("TransparentColor2")
        Dim useTransparency As XmlAttribute = m_Doc.CreateAttribute("UseTransparency")
        If TypeOf (gridFileLayer.GetObject) Is MapWinGIS.IImage Then
            Dim imageLayerFillTransparency As XmlAttribute = m_Doc.CreateAttribute("ImageLayerFillTransparency")
            Dim useHistogram As XmlAttribute = m_Doc.CreateAttribute("UseHistogram")
            Dim allowHillshade As XmlAttribute = m_Doc.CreateAttribute("AllowHillshade")
            Dim setToGrey As XmlAttribute = m_Doc.CreateAttribute("SetToGrey")
            Dim bufferSize As XmlAttribute = m_Doc.CreateAttribute("BufferSize")
            Dim imageColorScheme As XmlAttribute = m_Doc.CreateAttribute("ImageColorScheme")
            ' Start Paul Meems May 20 2010, fixes for Issue 1691 (Upsampling and downsampling method hard coded to "imNone") 
            Dim imageUpSamplingMethod As XmlAttribute = m_Doc.CreateAttribute("UpSamplingMethod")
            Dim imageDownSamplingMethod As XmlAttribute = m_Doc.CreateAttribute("DownSamplingMethod")
            ' End Paul Meems May 20 2010, Issue 1691
            ' Start Paul Meems Aug 11 2010
            Dim transparencyPercent As XmlAttribute = m_Doc.CreateAttribute("TransparencyPercent")
            ' End Paul Meems Aug 11 2010
            Dim img As MapWinGIS.Image

            Try
                img = CType(frmMain.MapMain.get_GetObject(gridFileLayer.Handle), MapWinGIS.Image)
            Catch
                Exit Sub
            End Try
            If img Is Nothing Then Exit Sub

            imageLayerFillTransparency.InnerText = CInt(gridFileLayer.ImageLayerFillTransparency * 100)
            useHistogram.InnerText = img.UseHistogram.ToString
            allowHillshade.InnerText = img.AllowHillshade.ToString
            setToGrey.InnerText = img.SetToGrey.ToString
            bufferSize.InnerText = img.BufferSize.ToString
            imageColorScheme.InnerText = img.ImageColorScheme.ToString
            imageUpSamplingMethod.InnerText = img.UpsamplingMode.ToString
            imageDownSamplingMethod.InnerText = img.DownsamplingMode.ToString
            transparencyPercent.InnerText = img.TransparencyPercent.ToString()

            grid.Attributes.Append(imageLayerFillTransparency)
            grid.Attributes.Append(useHistogram)
            grid.Attributes.Append(allowHillshade)
            grid.Attributes.Append(setToGrey)
            grid.Attributes.Append(bufferSize)
            grid.Attributes.Append(imageColorScheme)
            grid.Attributes.Append(imageUpSamplingMethod)
            grid.Attributes.Append(imageDownSamplingMethod)
            grid.Attributes.Append(transparencyPercent)

            img = Nothing
        End If

        'set the properties of the grid
        With gridFileLayer
            transparentColor.InnerText = RGB(.ImageTransparentColor.R, .ImageTransparentColor.G, .ImageTransparentColor.B).ToString
            transparentColor2.InnerText = RGB(.ImageTransparentColor2.R, .ImageTransparentColor2.G, .ImageTransparentColor2.B).ToString
            useTransparency.InnerText = .UseTransparentColor.ToString
        End With

        'add the elements
        grid.Attributes.Append(transparentColor)
        grid.Attributes.Append(transparentColor2)
        grid.Attributes.Append(useTransparency)

        'add the legend element
        If Not gridFileLayer.ColoringScheme Is Nothing Then
            AddLegendElement(m_Doc, CType(gridFileLayer.ColoringScheme, MapWinGIS.GridColorScheme), grid)
        End If

        parent.AppendChild(grid)
    End Sub

    ''' <summary>
    '''  Saves grid color scheme
    ''' </summary>
    Private Sub AddLegendElement(ByRef m_Doc As Xml.XmlDocument, ByVal legend As MapWinGIS.GridColorScheme, ByVal parent As XmlElement)
        Dim leg As XmlElement = m_Doc.CreateElement("Legend")
        Dim colorBreaks As XmlElement = m_Doc.CreateElement("ColorBreaks")
        Dim key As XmlAttribute = m_Doc.CreateAttribute("Key")
        Dim noDataColor As XmlAttribute = m_Doc.CreateAttribute("NoDataColor")
        Dim i As Integer

        'set the properties of the legend
        If Not legend Is Nothing Then
            With legend
                key.InnerText = .Key()
                noDataColor.InnerText = .NoDataColor.ToString
            End With
        End If

        'add the elements to the legend
        leg.Attributes.Append(key)
        leg.Attributes.Append(noDataColor)

        'add the elements to the colorBreaks
        If Not legend Is Nothing Then
            For i = 0 To legend.NumBreaks - 1
                AddColorBreaksElement(m_Doc, legend.Break(i), colorBreaks)
            Next
        End If
        leg.AppendChild(colorBreaks)

        parent.AppendChild(leg)
    End Sub

    ''' <summary>
    ''' Serializes grid color break
    ''' </summary>
    Private Sub AddColorBreaksElement(ByRef m_Doc As Xml.XmlDocument, ByVal colorBreak As MapWinGIS.GridColorBreak, ByVal parent As XmlElement)
        Dim break As XmlElement = m_Doc.CreateElement("Break")
        Dim highColor As XmlAttribute = m_Doc.CreateAttribute("HighColor")
        Dim highValue As XmlAttribute = m_Doc.CreateAttribute("HighValue")
        Dim lowColor As XmlAttribute = m_Doc.CreateAttribute("LowColor")
        Dim lowValue As XmlAttribute = m_Doc.CreateAttribute("LowValue")
        Dim gradientModel As XmlAttribute = m_Doc.CreateAttribute("GradientModel")
        Dim colorType As XmlAttribute = m_Doc.CreateAttribute("ColoringType")
        Dim caption As XmlAttribute = m_Doc.CreateAttribute("Caption")

        'set the properties
        With colorBreak
            highColor.InnerText = .HighColor.ToString
            highValue.InnerText = .HighValue.ToString
            lowColor.InnerText = .LowColor.ToString
            lowValue.InnerText = .LowValue.ToString
            gradientModel.InnerText = CInt(.GradientModel).ToString
            colorType.InnerText = CInt(.ColoringType).ToString
            caption.InnerText = .Caption
        End With

        'add the elements to the break
        break.Attributes.Append(highColor)
        break.Attributes.Append(lowColor)
        break.Attributes.Append(highValue)
        break.Attributes.Append(lowValue)
        break.Attributes.Append(gradientModel)
        break.Attributes.Append(colorType)
        break.Attributes.Append(caption)

        parent.AppendChild(break)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub SerializeFillStippleScheme(ByRef FillStippleScheme As MapWindow.Interfaces.ShapefileFillStippleScheme, ByRef doc As Xml.XmlDocument, ByRef root As Xml.XmlElement)
        If FillStippleScheme Is Nothing Then Return

        Dim outer As Xml.XmlElement = doc.CreateElement("FillStippleScheme")

        Dim fldIndexAttr As Xml.XmlAttribute = doc.CreateAttribute("FieldIndex")
        fldIndexAttr.InnerText = FillStippleScheme.FieldHandle
        outer.Attributes.Append(fldIndexAttr)

        Try
            Dim caption As Xml.XmlAttribute = doc.CreateAttribute("StippleCaption")
            caption.InnerText = frmMain.Legend.Layers.ItemByHandle(FillStippleScheme.LayerHandle).StippleSchemeFieldCaption
            outer.Attributes.Append(caption)
        Catch
        End Try

        'Breaks for each shape:
        Dim i As IEnumerator = FillStippleScheme.GetHatchesEnumerator()
        Dim brk As MapWindow.Interfaces.ShapefileFillStippleBreak
        While i.MoveNext()
            Dim inner2 As Xml.XmlElement = doc.CreateElement("StippleBreak")
            brk = i.Current.value

            Dim attr1 As Xml.XmlAttribute = doc.CreateAttribute("Value")
            attr1.InnerText = brk.Value

            Dim attr2 As Xml.XmlAttribute = doc.CreateAttribute("Transparent")
            attr2.InnerText = brk.Transparent.ToString()

            Dim attr3 As Xml.XmlAttribute = doc.CreateAttribute("LineColor")
            attr3.InnerText = brk.LineColor.ToArgb().ToString()

            Dim attr4 As Xml.XmlAttribute = doc.CreateAttribute("Hatch")
            attr4.InnerText = StippleToString(brk.Hatch)

            inner2.Attributes.Append(attr1)
            inner2.Attributes.Append(attr2)
            inner2.Attributes.Append(attr3)
            inner2.Attributes.Append(attr4)
            outer.AppendChild(inner2)
        End While

        root.AppendChild(outer)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub DeserializeFillStippleScheme(ByVal newHandle As Long, ByRef root As Xml.XmlElement)
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
        Try
            Dim csh As New MapWindow.Interfaces.ShapefileFillStippleScheme
            csh.FieldHandle = -1

            For Each xe As Xml.XmlElement In root
                If xe.Name = "FillStippleScheme" Then
                    csh.FieldHandle = Long.Parse(xe.Attributes("FieldIndex").InnerText)

                    Try
                        frmMain.Legend.Layers.ItemByHandle(newHandle).StippleSchemeFieldCaption = xe.Attributes("StippleCaption").InnerText
                    Catch
                    End Try

                    For Each xe2 As Xml.XmlElement In xe.ChildNodes
                        If xe2.Name = "StippleBreak" Then
                            If xe2.Attributes("Value") IsNot Nothing And xe2.Attributes("Transparent") IsNot Nothing And xe2.Attributes("LineColor") IsNot Nothing And xe2.Attributes("Hatch") IsNot Nothing Then
                                Try
                                    csh.AddHatch(xe2.Attributes("Value").InnerText, Boolean.Parse(xe2.Attributes("Transparent").InnerText), System.Drawing.Color.FromArgb(Integer.Parse(xe2.Attributes("LineColor").InnerText)), StringToStipple(xe2.Attributes("Hatch").InnerText))
                                Catch
                                End Try
                            End If
                        End If
                    Next
                End If
            Next

            If csh.FieldHandle > -1 Then
                frmMain.m_layers(newHandle).FillStippleScheme = csh
                frmMain.m_layers(newHandle).HatchingRecalculate()
            End If
        Catch ex As Exception
            MapWinUtility.Logger.Dbg("DEBUG: " + ex.ToString())
        Finally
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub SerializePointImageScheme(ByRef PointImgScheme As MapWindow.Interfaces.ShapefilePointImageScheme, ByRef doc As Xml.XmlDocument, ByRef root As Xml.XmlElement)
        If PointImgScheme Is Nothing Then Return

        Dim outer As Xml.XmlElement = doc.CreateElement("PointImageScheme")

        Dim fldIndexAttr As Xml.XmlAttribute = doc.CreateAttribute("FieldIndex")
        fldIndexAttr.InnerText = PointImgScheme.FieldIndex

        outer.Attributes.Append(fldIndexAttr)

        'Images:
        Dim imgdat As Xml.XmlElement = doc.CreateElement("ImageData")
        Dim imgcvter As New MapWinUtility.ImageUtils
        For k As Integer = 0 To frmMain.MapMain.get_UDPointImageListCount(PointImgScheme.LastKnownLayerHandle) - 1
            Dim imgItem As Xml.XmlElement = doc.CreateElement("Image")

            Dim imgIDAttr As Xml.XmlAttribute = doc.CreateAttribute("ID")
            imgIDAttr.InnerText = k.ToString()

            imgItem.Attributes.Append(imgIDAttr)

            Dim g As MapWinGIS.Image = frmMain.MapMain.get_UDPointImageListItem(PointImgScheme.LastKnownLayerHandle, k)
            Dim img As System.Drawing.Image = imgcvter.IPictureDispToImage(g.Picture)
            SaveImage(doc, img, imgItem)
            'Note: Don't close G, or you'll get access violations later on.
            'The OCX is returning a reference to the image list item, not a copy
            imgdat.AppendChild(imgItem)
        Next
        outer.AppendChild(imgdat)

        'Image Indexes assigned to shapes:
        Dim inner As Xml.XmlElement = doc.CreateElement("ItemData")
        Dim i As IDictionaryEnumerator = PointImgScheme.Items.Keys.GetEnumerator()
        While i.MoveNext()
            Dim item As Xml.XmlElement = doc.CreateElement("Item")

            Dim attr2 As Xml.XmlAttribute = doc.CreateAttribute("MatchValue")
            attr2.InnerText = i.Key.ToString()

            Dim attr3 As Xml.XmlAttribute = doc.CreateAttribute("ImgIndex")
            attr3.InnerText = i.Value.ToString()

            item.Attributes.Append(attr2)
            item.Attributes.Append(attr3)

            inner.AppendChild(item)
        End While
        outer.AppendChild(inner)

        'Visibilities assigned to shapes:
        Dim inner2 As Xml.XmlElement = doc.CreateElement("ItemVisibility")
        Dim i2 As IDictionaryEnumerator = PointImgScheme.ItemVisibility.Keys.GetEnumerator()
        While i2.MoveNext()
            Dim item As Xml.XmlElement = doc.CreateElement("Item")

            Dim attr2 As Xml.XmlAttribute = doc.CreateAttribute("MatchValue")
            attr2.InnerText = i2.Key.ToString()

            Dim attr3 As Xml.XmlAttribute = doc.CreateAttribute("Visible")
            attr3.InnerText = i2.Value.ToString()

            item.Attributes.Append(attr2)
            item.Attributes.Append(attr3)

            inner2.AppendChild(item)
        End While
        outer.AppendChild(inner2)

        root.AppendChild(outer)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub DeserializePointImageScheme(ByVal newHandle As Long, ByRef root As Xml.XmlElement)
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
        Try
            Dim imgUtil As New MapWinUtility.ImageUtils
            Dim xe, xe2, xe3 As Xml.XmlElement
            Dim found As Boolean = False
            Dim csh As New MapWindow.Interfaces.ShapefilePointImageScheme(newHandle)

            Dim TranslationTable As New Hashtable

            For Each xe In root
                If xe.Name = "PointImageScheme" Then
                    found = True
                    csh.FieldIndex = Long.Parse(xe.Attributes("FieldIndex").InnerText)

                    For Each xe2 In xe.ChildNodes
                        If xe2.Name = "ImageData" Then
                            For Each xe3 In xe2.ChildNodes
                                Dim origIndex As Long = Long.Parse(xe3.Attributes("ID").InnerText)

                                Dim Type As String
                                Type = xe3.Item("Image").Attributes("Type").InnerText
                                Dim img As System.Drawing.Image = CType(ConvertStringToImage(xe3.Item("Image").InnerText, Type), Image)

                                Dim ico As New MapWinGIS.Image
                                ico.Picture = imgUtil.ImageToIPictureDisp(CType(img, System.Drawing.Image))
                                If Not ico Is Nothing Then ico.TransparencyColor = ico.Value(0, 0)
                                'Pull from first pixel rather than assuming bluish ico.TransparencyColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 0, 211)))

                                Dim newidx As Integer = frmMain.MapMain.set_UDPointImageListAdd(newHandle, ico)
                                TranslationTable.Add(origIndex, newidx)
                                img = Nothing
                            Next
                        ElseIf xe2.Name = "ItemData" Then
                            For Each xe3 In xe2.ChildNodes
                                If xe3.Name = "Item" Then
                                    Dim tag As String = xe3.Attributes("MatchValue").InnerText
                                    Dim imgIndex As Long = Long.Parse(xe3.Attributes("ImgIndex").InnerText)
                                    Dim actualIndex As Long = -1
                                    If TranslationTable.Contains(imgIndex) Then
                                        actualIndex = TranslationTable(imgIndex)
                                    Else
                                        'Hope it's right
                                        actualIndex = imgIndex
                                    End If

                                    If Not actualIndex = -1 And Not tag = "" Then csh.Items.Add(tag, actualIndex)

                                    Dim sf As MapWinGIS.Shapefile

                                    sf = CType(frmMain.m_layers(newHandle).GetObject, MapWinGIS.Shapefile)
                                    If sf Is Nothing Then
                                        g_error = "Failed to get Shapefile object"
                                        Return
                                    End If

                                    If Not actualIndex = -1 Then
                                        For j As Integer = 0 To sf.NumShapes - 1
                                            If sf.CellValue(csh.FieldIndex, j) = tag Then
                                                frmMain.MapMain.set_ShapePointImageListID(newHandle, j, actualIndex)
                                                If Not frmMain.MapMain.get_ShapePointType(newHandle, j) = MapWinGIS.tkPointType.ptImageList Then
                                                    frmMain.MapMain.set_ShapePointType(newHandle, j, MapWinGIS.tkPointType.ptImageList)
                                                End If
                                                frmMain.MapMain.set_ShapePointSize(newHandle, j, 1)
                                            End If
                                        Next j
                                    End If

                                    sf = Nothing
                                End If
                            Next
                        ElseIf xe2.Name = "ItemVisibility" Then
                            For Each xe3 In xe2.ChildNodes
                                If xe3.Name = "Item" Then
                                    Dim tag As String = xe3.Attributes("MatchValue").InnerText
                                    Dim vis As Boolean = Boolean.Parse(xe3.Attributes("Visible").InnerText)
                                    csh.ItemVisibility.Add(tag, vis)

                                    Dim sf As MapWinGIS.Shapefile

                                    sf = CType(frmMain.m_layers(newHandle).GetObject, MapWinGIS.Shapefile)
                                    If sf Is Nothing Then
                                        g_error = "Failed to get Shapefile object"
                                        Return
                                    End If

                                    For j As Integer = 0 To sf.NumShapes - 1
                                        If sf.CellValue(csh.FieldIndex, j) = tag Then
                                            frmMain.MapMain.set_ShapeVisible(newHandle, j, vis)
                                        End If
                                    Next j

                                    sf = Nothing
                                End If
                            Next
                        End If
                    Next
                    Exit For
                End If
            Next

            If found Then
                frmMain.Legend.Layers.ItemByHandle(newHandle).PointImageScheme = csh
            End If

            TranslationTable.Clear()
            GC.Collect()
        Catch ex As Exception
            MapWinUtility.Logger.Dbg("DEBUG: " + ex.ToString())
        Finally
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        End Try
    End Sub

    Private Sub LoadShpFileColoringScheme(ByVal legend As XmlElement, ByVal handle As Integer)
        Dim shpscheme As New MapWinGIS.ShapefileColorScheme
        Dim numOfBreaks As Integer
        Dim break As MapWinGIS.ShapefileColorBreak
        Dim i As Integer

        Try
            'set the shape file color scheme properties
            shpscheme.FieldIndex = CInt(legend.Attributes("FieldIndex").InnerText)
            shpscheme.LayerHandle = handle
            shpscheme.Key = legend.Attributes("Key").InnerText

            Try
                frmMain.Legend.Layers.ItemByHandle(handle).ColorSchemeFieldCaption = legend.Attributes("SchemeCaption").InnerText
            Catch
            End Try

            'set all of the breaks
            numOfBreaks = legend.Item("ColorBreaks").ChildNodes.Count
            For i = 0 To numOfBreaks - 1
                With legend.Item("ColorBreaks").ChildNodes(i)
                    break = New MapWinGIS.ShapefileColorBreak
                    break.Caption = .Attributes("Caption").InnerText
                    break.StartColor = System.Convert.ToUInt32(.Attributes("StartColor").InnerText)
                    break.EndColor = System.Convert.ToUInt32(.Attributes("EndColor").InnerText)
                    If .Attributes("StartValue").InnerText = "(null)" Then
                        break.StartValue = Nothing
                    Else
                        break.StartValue = .Attributes("StartValue").InnerText
                    End If
                    If .Attributes("EndValue").InnerText = "(null)" Then
                        break.EndValue = Nothing
                    Else
                        break.EndValue = .Attributes("EndValue").InnerText
                    End If
                    If Not .Attributes("Visible") Is Nothing AndAlso Not .Attributes("Visible").InnerText = "" Then
                        break.Visible = Boolean.Parse(.Attributes("Visible").InnerText)
                    End If

                    shpscheme.Add(break)
                End With
            Next

            If (numOfBreaks > 0) Then
                'set that layers scheme and redraw the legend
                frmMain.Layers(handle).ColoringScheme = shpscheme
                frmMain.Legend.Refresh()
            End If

        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadShpFileColoringScheme(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        End Try

    End Sub

    ' tws 04/29/2007
    Private Sub LoadShapePropertiesList(ByVal propList As XmlElement, ByVal handle As Integer)
        Try
            For Each sProp As XmlElement In propList.ChildNodes
                Dim ix As Integer = Integer.Parse(sProp.GetAttribute("ShapeIndex"))
                'Dim sC As UInt32 = System.Convert.ToUInt32(sProp.GetAttribute("LineColor"))
                For Each xmla As XmlAttribute In sProp.Attributes
                    ' count on the output logic to get them right for the type:
                    ' just process whatever we get
                    Select Case xmla.Name
                        Case "ShapeIndex"
                            ' we already did this one

                        Case "DrawLine"
                            Dim b As Boolean = Boolean.Parse(sProp.GetAttribute("DrawLine"))
                            frmMain.MapMain.set_ShapeDrawLine(handle, ix, b)
                        Case "LineColor"
                            Dim sc As Color = Color.FromArgb(Integer.Parse(sProp.GetAttribute("LineColor")))
                            frmMain.MapMain.set_ShapeLineColor(handle, ix, ColorTranslator.ToOle(sc))
                        Case "LineWidth"
                            Dim sL As Integer = Integer.Parse(sProp.GetAttribute("LineWidth"))
                            frmMain.MapMain.set_ShapeLineWidth(handle, ix, sL)
                        Case "LineStyle"
                            Dim i As Integer = Integer.Parse(sProp.GetAttribute("LineStyle"))
                            frmMain.MapMain.set_ShapeLineStipple(handle, ix, i)

                        Case "DrawPoint"
                            Dim b As Boolean = Boolean.Parse(sProp.GetAttribute("DrawPoint"))
                            frmMain.MapMain.set_ShapeDrawPoint(handle, ix, b)
                        Case "PointColor"
                            Dim sc As Color = Color.FromArgb(Integer.Parse(sProp.GetAttribute("PointColor")))
                            frmMain.MapMain.set_ShapePointColor(handle, ix, ColorTranslator.ToOle(sc))
                        Case "PointSize"
                            Dim sP As Integer = Integer.Parse(sProp.GetAttribute("PointSize"))
                            frmMain.MapMain.set_ShapePointSize(handle, ix, sP)

                        Case "DrawFill"
                            Dim b As Boolean = Boolean.Parse(sProp.GetAttribute("DrawFill"))
                            frmMain.MapMain.set_ShapeDrawFill(handle, ix, b)
                        Case "FillColor"
                            Dim sc As Color = Color.FromArgb(Integer.Parse(sProp.GetAttribute("FillColor")))
                            frmMain.MapMain.set_ShapeFillColor(handle, ix, ColorTranslator.ToOle(sc))
                        Case "FillTransparency"
                            Dim s As Single = Single.Parse(sProp.GetAttribute("FillTransparency"))
                            frmMain.MapMain.set_ShapeFillTransparency(handle, ix, s)
                        Case "FillStyle"
                            Dim i As Integer = Integer.Parse(sProp.GetAttribute("FillStyle"))
                            frmMain.MapMain.set_ShapeFillStipple(handle, ix, i)
                        Case Else
                            ' maybe we should complain here, this case must be a coding error
                    End Select
                Next
            Next
        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadShapePropertiesList(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
        End Try
    End Sub

    Private Function LoadGridFileColoringScheme(ByVal legend As XmlElement) As MapWinGIS.GridColorScheme
        Dim gridScheme As New MapWinGIS.GridColorScheme
        Dim numOfBreaks As Integer
        Dim break As MapWinGIS.GridColorBreak
        Dim i As Integer

        Try
            'set the grid file color scheme properties
            gridScheme.NoDataColor = System.Convert.ToUInt32(legend.Attributes("NoDataColor").InnerText)
            gridScheme.Key = legend.Attributes("Key").InnerText

            'set all of the breaks
            numOfBreaks = legend.Item("ColorBreaks").ChildNodes.Count
            For i = 0 To numOfBreaks - 1
                With legend.Item("ColorBreaks").ChildNodes(i)
                    break = New MapWinGIS.GridColorBreak
                    break.Caption = .Attributes("Caption").InnerText
                    break.HighColor = System.Convert.ToUInt32(.Attributes("HighColor").InnerText)
                    break.LowColor = System.Convert.ToUInt32(.Attributes("LowColor").InnerText)
                    break.HighValue = MapWinUtility.MiscUtils.ParseDouble(.Attributes("HighValue").InnerText, 0.0) 'CDbl(.Attributes("HighValue").InnerText)
                    break.LowValue = MapWinUtility.MiscUtils.ParseDouble(.Attributes("LowValue").InnerText, 0.0)  'CDbl(.Attributes("LowValue").InnerText)
                    break.GradientModel = CType(.Attributes("GradientModel").InnerText, MapWinGIS.GradientModel)
                    break.ColoringType = CType(.Attributes("ColoringType").InnerText, MapWinGIS.ColoringType)
                    gridScheme.InsertBreak(break)
                End With
            Next

            If numOfBreaks > 0 Then
                Return gridScheme
            Else
                Return Nothing
            End If

        Catch e As System.Exception
            m_ErrorMsg += "Error in LoadGridFileColoringScheme(), Message: " + e.Message + Chr(13)
            m_ErrorOccured = True
            Return Nothing
        End Try

    End Function
End Class
