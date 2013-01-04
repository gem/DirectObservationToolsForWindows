'********************************************************************************************************
' Filename:  clsXMLProjectLegacy.vb
' Description: handles legacy VWR format
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'The Original Code is MapWindow Open Source. 
' --------------------------------------------------------------------------------------------------
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'05-apr-2011 - sleschinski - copied the code form the clsXmlProjectFile

Imports System.Xml

Partial Friend Class XmlProjectFile

    ' TODO: Is this functions are still in use?

    Private Function TranslateLegacyVWR(ByVal projectPath As String) As Boolean
        Dim oldDoc As New XmlDocument
        Dim newDoc As New XmlDocument
        Dim oldRoot, newRoot As XmlElement

        If Not System.IO.File.Exists(projectPath) Then
            Return False
        Else
            ChDir(System.IO.Path.GetDirectoryName(projectPath))
            Try
                oldDoc.Load(projectPath)

                If oldDoc.InnerXml.StartsWith("<Mapwin") Then
                    Return True
                ElseIf oldDoc.InnerXml.StartsWith("<DFViewer") Then
                    'save the old vwr to .bak and overwrite the vwr with new format
                    If IO.File.Exists(projectPath + ".bak") Then
                        IO.File.Delete(projectPath + ".bak")
                    End If
                    oldDoc.Save(projectPath + ".bak")
                    IO.File.Delete(projectPath)
                    oldRoot = oldDoc.DocumentElement

                    Dim Ver As String = App.VersionString()
                    Dim ConfigPath As XmlAttribute

                    Dim prjName As String = frmMain.Text.Replace("'", "")
                    newDoc.LoadXml("<Mapwin name='" + System.Web.HttpUtility.UrlEncode(prjName) + "' type='projectfile' version='" + System.Web.HttpUtility.UrlEncode(Ver) + "'></Mapwin>")
                    newRoot = newDoc.DocumentElement


                    'Add the configuration path
                    ConfigPath = newDoc.CreateAttribute("ConfigurationPath")
                    ConfigPath.InnerText = GetRelativePath(ConfigFileName, ProjectFileName)
                    newRoot.Attributes.Append(ConfigPath)


                    'Add the projection
                    Dim proj As Xml.XmlAttribute = newDoc.CreateAttribute("ProjectProjection")
                    Try
                        proj.InnerText = oldRoot.Attributes("ProjectProjection").InnerText
                    Catch ex As Exception
                        proj.InnerText = ""
                    End Try
                    newRoot.Attributes.Append(proj)


                    'Add the map units
                    Dim mapunit As Xml.XmlAttribute = newDoc.CreateAttribute("MapUnits")
                    Try
                        mapunit.InnerText = oldRoot.Attributes("MapUnits").InnerText
                    Catch ex As Exception
                        mapunit.InnerText = MapWindow.Interfaces.UnitOfMeasure.Inches.ToString() 'Will default back later
                    End Try
                    newRoot.Attributes.Append(mapunit)


                    'Add the status bar coord customizations
                    Dim xStatusBarAlternateCoordsNumDecimals As Xml.XmlAttribute = newDoc.CreateAttribute("StatusBarAlternateCoordsNumDecimals")
                    xStatusBarAlternateCoordsNumDecimals.InnerText = 3.ToString
                    newRoot.Attributes.Append(xStatusBarAlternateCoordsNumDecimals)
                    Dim xStatusBarCoordsNumDecimals As Xml.XmlAttribute = newDoc.CreateAttribute("StatusBarCoordsNumDecimals")
                    xStatusBarCoordsNumDecimals.InnerText = 3.ToString
                    newRoot.Attributes.Append(xStatusBarCoordsNumDecimals)
                    Dim xStatusBarAlternateCoordsUseCommas As Xml.XmlAttribute = newDoc.CreateAttribute("StatusBarAlternateCoordsUseCommas")
                    xStatusBarAlternateCoordsUseCommas.InnerText = True.ToString
                    newRoot.Attributes.Append(xStatusBarAlternateCoordsUseCommas)
                    Dim xStatusBarCoordsUseCommas As Xml.XmlAttribute = newDoc.CreateAttribute("StatusBarCoordsUseCommas")
                    xStatusBarCoordsUseCommas.InnerText = True.ToString
                    newRoot.Attributes.Append(xStatusBarCoordsUseCommas)

                    Dim ShowFloatingScaleBar As Xml.XmlAttribute = newDoc.CreateAttribute("ShowFloatingScaleBar")
                    ShowFloatingScaleBar.InnerText = False.ToString
                    newRoot.Attributes.Append(ShowFloatingScaleBar)

                    Dim FloatingScaleBarPosition As Xml.XmlAttribute = newDoc.CreateAttribute("FloatingScaleBarPosition")
                    FloatingScaleBarPosition.InnerText = "Lower Right"
                    newRoot.Attributes.Append(FloatingScaleBarPosition)

                    Dim FloatingScaleBarUnit As Xml.XmlAttribute = newDoc.CreateAttribute("FloatingScaleBarUnit")
                    FloatingScaleBarUnit.InnerText = ""
                    newRoot.Attributes.Append(FloatingScaleBarUnit)

                    Dim FloatingScaleBarForecolor As Xml.XmlAttribute = newDoc.CreateAttribute("FloatingScaleBarForecolor")
                    FloatingScaleBarForecolor.InnerText = System.Drawing.Color.Black.ToString
                    newRoot.Attributes.Append(FloatingScaleBarForecolor)

                    Dim FloatingScaleBarBackcolor As Xml.XmlAttribute = newDoc.CreateAttribute("FloatingScaleBarBackcolor")
                    FloatingScaleBarBackcolor.InnerText = System.Drawing.Color.White.ToString
                    newRoot.Attributes.Append(FloatingScaleBarBackcolor)


                    'Add the map resize behavior
                    Dim resizebehavior As Xml.XmlAttribute = newDoc.CreateAttribute("MapResizeBehavior")
                    resizebehavior.InnerText = "0"
                    newRoot.Attributes.Append(resizebehavior)


                    'Add whether to display various coordinate systems in the status bar
                    Dim coord_projected As Xml.XmlAttribute = newDoc.CreateAttribute("ShowStatusBarCoords_Projected")
                    coord_projected.InnerText = True.ToString
                    newRoot.Attributes.Append(coord_projected)
                    Dim coord_alternate As Xml.XmlAttribute = newDoc.CreateAttribute("ShowStatusBarCoords_Alternate")
                    coord_alternate.InnerText = "Kilometers"
                    newRoot.Attributes.Append(coord_alternate)


                    'Add the save shape settings behavior
                    Dim saveshapesettinfgsbehavior As Xml.XmlAttribute = newDoc.CreateAttribute("SaveShapeSettings")
                    saveshapesettinfgsbehavior.InnerText = False.ToString
                    newRoot.Attributes.Append(saveshapesettinfgsbehavior)


                    'Add the project-level map background color settings (5/4/2008 added by JK)
                    Dim backColor_useDefault As Xml.XmlAttribute = newDoc.CreateAttribute("ViewBackColor_UseDefault")
                    backColor_useDefault.InnerText = True.ToString
                    newRoot.Attributes.Append(backColor_useDefault)
                    Dim backColor As Xml.XmlAttribute = newDoc.CreateAttribute("ViewBackColor")
                    backColor.InnerText = (MapWinUtility.Colors.ColorToInteger(System.Drawing.Color.White)).ToString
                    newRoot.Attributes.Append(backColor)


                    'Add the list of the plugins to the project file
                    Dim Plugins As XmlElement = newDoc.CreateElement("Plugins")
                    newRoot.AppendChild(Plugins)


                    'Add the application plugins
                    Dim AppPlugins As XmlElement = newDoc.CreateElement("ApplicationPlugins")
                    Dim Dir As XmlAttribute = newDoc.CreateAttribute("PluginDir")
                    Dir.InnerText = ""
                    AppPlugins.Attributes.Append(Dir)


                    Try
                        Dim AppPlugin As XmlElement = newDoc.CreateElement("Plugin")
                        Dim settingstring As XmlAttribute = newDoc.CreateAttribute("SettingsString")
                        Dim SSKey As XmlAttribute = newDoc.CreateAttribute("Key")
                        'format of <Plugin SettingsString="4{}c:\temp\dfirm_database" Key="RasterCatalog_clsRasterCatalog" />
                        settingstring.InnerText = oldRoot.Attributes("noOfAutho").InnerText + "{}" + oldRoot.Attributes("OrthoLocation").InnerText
                        SSKey.InnerText = "RasterCatalog_clsRasterCatalog"
                        AppPlugin.Attributes.Append(settingstring)
                        AppPlugin.Attributes.Append(SSKey)
                        AppPlugins.AppendChild(AppPlugin)
                    Catch ex As Exception
                    End Try

                    newRoot.AppendChild(AppPlugins)

                    'Add extents of map
                    Dim Extents As XmlElement = newDoc.CreateElement("Extents")
                    Dim xMax As XmlAttribute = newDoc.CreateAttribute("xMax")
                    Dim yMax As XmlAttribute = newDoc.CreateAttribute("yMax")
                    Dim xMin As XmlAttribute = newDoc.CreateAttribute("xMin")
                    Dim yMin As XmlAttribute = newDoc.CreateAttribute("yMin")
                    Try
                        xMax.InnerText = oldRoot.Item("Extents").Attributes("xMax").InnerText
                    Catch ex As Exception
                        xMax.InnerText = "0"
                    End Try
                    Try
                        yMax.InnerText = oldRoot.Item("Extents").Attributes("yMax").InnerText
                    Catch ex As Exception
                        yMax.InnerText = "0"
                    End Try
                    Try
                        xMin.InnerText = oldRoot.Item("Extents").Attributes("xMin").InnerText
                    Catch ex As Exception
                        xMin.InnerText = "0"
                    End Try
                    Try
                        yMin.InnerText = oldRoot.Item("Extents").Attributes("yMax").InnerText
                    Catch ex As Exception
                        yMin.InnerText = "0"
                    End Try

                    Dim ext As New MapWinGIS.Extents
                    ext.SetBounds(xMin.InnerText, yMin.InnerText, 0, xMax.InnerText, yMax.InnerText, 0)

                    Extents.Attributes.Append(xMax)
                    Extents.Attributes.Append(yMax)
                    Extents.Attributes.Append(xMin)
                    Extents.Attributes.Append(yMin)
                    newRoot.AppendChild(Extents)

                    'Add the layers
                    TranslateLegacyVWRLayers(oldDoc, oldRoot, newDoc, newRoot, projectPath, proj.InnerText, ext)

                    ''Add view bookmarks
                    Dim bookmarksElem As XmlElement = newDoc.CreateElement("Bookmarks")
                    newRoot.AppendChild(bookmarksElem)

                    'Add the properies fo the preview Map to the project file
                    Dim prevMap As XmlElement = newDoc.CreateElement("PreviewMap")
                    Dim visible As XmlAttribute = newDoc.CreateAttribute("Visible")
                    Dim dx As XmlAttribute = newDoc.CreateAttribute("dx")
                    Dim dy As XmlAttribute = newDoc.CreateAttribute("dy")
                    Dim xllcenter As XmlAttribute = newDoc.CreateAttribute("xllcenter")
                    Dim yllcenter As XmlAttribute = newDoc.CreateAttribute("yllcenter")
                    dx.InnerText = "0"
                    dy.InnerText = "0"
                    xllcenter.InnerText = "0"
                    yllcenter.InnerText = "0"
                    prevMap.Attributes.Append(dx)
                    prevMap.Attributes.Append(dy)
                    prevMap.Attributes.Append(xllcenter)
                    prevMap.Attributes.Append(yllcenter)
                    Dim image As XmlElement = newDoc.CreateElement("Image")
                    Dim type As XmlAttribute = newDoc.CreateAttribute("Type")
                    image.InnerText = ""
                    type.InnerText = ""
                    image.Attributes.Append(type)
                    prevMap.AppendChild(image)
                    'add the elements to the prevMap
                    newRoot.AppendChild(prevMap)


                    'Save the project file.
                    MapWinUtility.Logger.Dbg("Saving Project: " + projectPath)
                    Try
                        newDoc.Save(projectPath)
                        Return True
                    Catch e As System.UnauthorizedAccessException
                        Dim ro As Boolean = False
                        If System.IO.File.Exists(projectPath) Then
                            Dim fi As New System.IO.FileInfo(projectPath)
                            If fi.IsReadOnly Then ro = True
                        End If
                        If ro Then
                            MapWinUtility.Logger.Msg("The project file could not be saved because it is read-only." + Environment.NewLine + Environment.NewLine + "Please have your system administrator grant write access to the file:" + Environment.NewLine + projectPath, MsgBoxStyle.Exclamation, "Read-Only File")
                        Else
                            MapWinUtility.Logger.Msg("The project file could not be saved due to insufficient access." + Environment.NewLine + Environment.NewLine + "Please have your system administrator grant access to the file:" + Environment.NewLine + projectPath, MsgBoxStyle.Exclamation, "Insufficient Access")
                        End If
                        Return False
                    End Try

                    Return True
                End If
            Catch ex As Exception
                Return False
            End Try
        End If

        Return True
    End Function

    Private Sub TranslateLegacyVWRLayers(ByRef oldDoc As XmlDocument, ByRef oldRoot As XmlElement, ByRef newDoc As XmlDocument, ByRef newRoot As XmlElement, ByVal projectPath As String, ByVal projection As String, ByVal extents As MapWinGIS.Extents)
        Dim newGroups As XmlElement = newDoc.CreateElement("Groups")

        If Not oldRoot.Item("Groups") Is Nothing Then
            Dim oldGroups As XmlElement = oldRoot.Item("Groups")
            For Each oldgroup As XmlElement In oldGroups
                Dim newGroup As XmlElement = newDoc.CreateElement("Group")
                Dim gName As XmlAttribute = newDoc.CreateAttribute("Name")
                Dim gExpanded As XmlAttribute = newDoc.CreateAttribute("Expanded")
                Dim Position As XmlAttribute = newDoc.CreateAttribute("Position")

                Try
                    gName.InnerText = oldgroup.Attributes("Name").InnerText
                Catch ex As Exception
                End Try
                Try
                    gExpanded.InnerText = oldgroup.Attributes("Expanded").InnerText
                Catch ex As Exception
                End Try
                Try
                    Position.InnerText = oldgroup.Attributes("Position").InnerText
                Catch ex As Exception
                End Try

                newGroup.Attributes.Append(gName)
                newGroup.Attributes.Append(gExpanded)
                newGroup.Attributes.Append(Position)
                Dim image As XmlElement = newDoc.CreateElement("Image")
                Dim itype As XmlAttribute = newDoc.CreateAttribute("Type")
                image.Attributes.Append(itype)
                newGroup.AppendChild(image)

                If (Not oldgroup.Item("Layers") Is Nothing) Then
                    Dim newLayers As XmlElement = newDoc.CreateElement("Layers")
                    For Each oldLayer As XmlElement In oldgroup.Item("Layers")
                        Dim newlayer As XmlElement = newDoc.CreateElement("Layer")
                        Dim name As XmlAttribute = newDoc.CreateAttribute("Name")
                        Dim groupname As XmlAttribute = newDoc.CreateAttribute("GroupName")
                        Dim type As XmlAttribute = newDoc.CreateAttribute("Type")
                        Dim path As XmlAttribute = newDoc.CreateAttribute("Path")
                        Dim tag As XmlAttribute = newDoc.CreateAttribute("Tag")
                        Dim legPic As XmlAttribute = newDoc.CreateAttribute("LegendPicture")
                        Dim visible As XmlAttribute = newDoc.CreateAttribute("Visible")
                        Dim labelsVisible As XmlAttribute = newDoc.CreateAttribute("LabelsVisible")
                        Dim expanded As XmlAttribute = newDoc.CreateAttribute("Expanded")

                        Try
                            '---Cho 3/3/2009: shapefileAlias has the layer name displayed in TOC.
                            If Not oldLayer.Item("ShapeFileProperties") Is Nothing AndAlso oldLayer.Item("ShapeFileProperties").Attributes("shapefileAlias").InnerText <> "0" Then
                                name.InnerText = oldLayer.Item("ShapeFileProperties").Attributes("shapefileAlias").InnerText
                            Else
                                name.InnerText = oldLayer.Attributes("Name").InnerText
                            End If
                        Catch ex As Exception
                        End Try
                        Try
                            groupname.InnerText = gName.InnerText
                        Catch ex As Exception
                        End Try
                        Try
                            type.InnerText = oldLayer.Attributes("Type").InnerText
                        Catch ex As Exception
                        End Try
                        Try
                            path.InnerText = oldLayer.Attributes("Path").InnerText
                        Catch ex As Exception
                        End Try
                        Try
                            tag.InnerText = oldLayer.Attributes("Tag").InnerText
                        Catch ex As Exception
                        End Try
                        Try
                            visible.InnerText = oldLayer.Attributes("Visible").InnerText
                        Catch ex As Exception
                        End Try
                        Try
                            labelsVisible.InnerText = oldLayer.Attributes("LabelsVisible").InnerText
                        Catch ex As Exception
                            labelsVisible.InnerText = False.ToString
                        End Try
                        Try
                            expanded.InnerText = oldLayer.Attributes("Expanded").InnerText
                            If expanded.InnerText = "" Then
                                expanded.InnerText = False.ToString
                            End If
                        Catch ex As Exception
                            expanded.InnerText = False.ToString
                        End Try

                        newlayer.Attributes.Append(name)
                        newlayer.Attributes.Append(groupname)
                        newlayer.Attributes.Append(type)
                        newlayer.Attributes.Append(path)
                        newlayer.Attributes.Append(tag)
                        newlayer.Attributes.Append(legPic)
                        newlayer.Attributes.Append(visible)
                        newlayer.Attributes.Append(labelsVisible)
                        newlayer.Attributes.Append(expanded)

                        If labelsVisible.InnerText = "True" Then
                            TranslateLegacyVWRLabelElements(path.InnerText, oldLayer, projection, extents)
                        End If

                        'ARA 2/17/2009 It turns out the type isn't a layer type, but instead a shapefile type
                        ' and grids are never read by the legacy vwr, so only way to tell type is by extension.
                        'Dim typenum As MapWindow.Interfaces.eLayerType = type.InnerText
                        'If typenum = MapWindow.Interfaces.eLayerType.LineShapefile Or typenum = MapWindow.Interfaces.eLayerType.PointShapefile Or typenum = MapWindow.Interfaces.eLayerType.PolygonShapefile Then
                        Dim typenum As MapWinGIS.ShpfileType = type.InnerText
                        Dim lyrNum As Integer
                        If IO.Path.GetExtension(path.InnerText) = ".shp" Then
                            'if it is a shapfile then add the shape properties to the layer
                            TranslateLegacyVWRShapeFileElement(newDoc, newlayer, oldLayer, projectPath)
                            If typenum = MapWinGIS.ShpfileType.SHP_POINT Or typenum = MapWinGIS.ShpfileType.SHP_POINTM Or typenum = MapWinGIS.ShpfileType.SHP_POINTZ Or typenum = MapWinGIS.ShpfileType.SHP_MULTIPOINT Or typenum = MapWinGIS.ShpfileType.SHP_MULTIPOINTM Or typenum = MapWinGIS.ShpfileType.SHP_MULTIPOINTZ Then
                                lyrNum = Interfaces.eLayerType.PointShapefile
                                type.InnerText = lyrNum.ToString
                            ElseIf typenum = MapWinGIS.ShpfileType.SHP_POLYGON Or typenum = MapWinGIS.ShpfileType.SHP_POLYGONM Or typenum = MapWinGIS.ShpfileType.SHP_POLYGONZ Or typenum = MapWinGIS.ShpfileType.SHP_MULTIPATCH Then
                                lyrNum = Interfaces.eLayerType.PolygonShapefile
                                type.InnerText = lyrNum.ToString
                            ElseIf typenum = MapWinGIS.ShpfileType.SHP_POLYLINE Or typenum = MapWinGIS.ShpfileType.SHP_POLYLINEM Or typenum = MapWinGIS.ShpfileType.SHP_POLYLINEZ Then
                                lyrNum = Interfaces.eLayerType.LineShapefile
                                type.InnerText = lyrNum.ToString
                            End If
                        Else 'If typenum = MapWindow.Interfaces.eLayerType.Grid Or typenum = MapWindow.Interfaces.eLayerType.Image Then
                            'add the grid file properties
                            TranslateLegacyVWRGridElement(newDoc, newlayer, oldLayer)
                            lyrNum = Interfaces.eLayerType.Grid
                            type.InnerText = lyrNum.ToString
                        End If

                        'add DynamicVisibility options
                        Dim dynamicVisibility As XmlElement = newDoc.CreateElement("DynamicVisibility")
                        Dim useDynamicVisibility As XmlAttribute = newDoc.CreateAttribute("UseDynamicVisibility")
                        Dim xMin As XmlAttribute = newDoc.CreateAttribute("xMin")
                        Dim yMin As XmlAttribute = newDoc.CreateAttribute("yMin")
                        Dim xMax As XmlAttribute = newDoc.CreateAttribute("xMax")
                        Dim yMax As XmlAttribute = newDoc.CreateAttribute("yMax")
                        useDynamicVisibility.InnerText = False.ToString

                        xMin.InnerText = "0"
                        yMin.InnerText = "0"
                        xMax.InnerText = "0"
                        yMax.InnerText = "0"

                        dynamicVisibility.Attributes.Append(useDynamicVisibility)
                        dynamicVisibility.Attributes.Append(xMin)
                        dynamicVisibility.Attributes.Append(yMin)
                        dynamicVisibility.Attributes.Append(xMax)
                        dynamicVisibility.Attributes.Append(yMax)

                        newlayer.AppendChild(dynamicVisibility)

                        newLayers.AppendChild(newlayer)
                    Next
                    newGroup.AppendChild(newLayers)
                End If
                newGroups.AppendChild(newGroup)
            Next
        End If

        newRoot.AppendChild(newGroups)
    End Sub

    Private Sub TranslateLegacyVWRLabelElements(ByVal LayerFileName As String, ByRef oldLayer As XmlNode, ByVal projection As String, ByVal extents As MapWinGIS.Extents)
        Dim lblFileName = IO.Path.ChangeExtension(LayerFileName, ".lbl")

        If IO.File.Exists(lblFileName) Then
            IO.File.Move(lblFileName, lblFileName + ".bak")
            IO.File.Delete(lblFileName)
        End If

        Dim doc As New XmlDocument
        Dim root As XmlElement
        doc.LoadXml("<Mapwin version='" + System.Web.HttpUtility.UrlEncode(App.VersionString()) + "'></Mapwin>")
        root = doc.DocumentElement

        Dim Labels As XmlElement = doc.CreateElement("Labels")

        Dim AppendLine1 As XmlAttribute = doc.CreateAttribute("AppendLine1")
        Dim AppendLine2 As XmlAttribute = doc.CreateAttribute("AppendLine2")
        Dim PrependLine1 As XmlAttribute = doc.CreateAttribute("PrependLine1")
        Dim PrependLine2 As XmlAttribute = doc.CreateAttribute("PrependLine2")
        Dim Field As XmlAttribute = doc.CreateAttribute("Field")
        Dim Font As XmlAttribute = doc.CreateAttribute("Font")
        Dim Size As XmlAttribute = doc.CreateAttribute("Size")
        Dim Color As XmlAttribute = doc.CreateAttribute("Color")
        Dim Justification As XmlAttribute = doc.CreateAttribute("Justification")
        Dim UseMinZoomLevel As XmlAttribute = doc.CreateAttribute("UseMinZoomLevel")
        Dim Scaled As XmlAttribute = doc.CreateAttribute("Scaled")
        Dim UseShadows As XmlAttribute = doc.CreateAttribute("UseShadows")
        Dim ShadowColor As XmlAttribute = doc.CreateAttribute("ShadowColor")
        Dim Offset As XmlAttribute = doc.CreateAttribute("Offset")
        Dim StandardViewWidth As XmlAttribute = doc.CreateAttribute("StandardViewWidth")
        Dim UseLabelCollision As XmlAttribute = doc.CreateAttribute("UseLabelCollision")
        Dim RemoveDuplicateLabels As XmlAttribute = doc.CreateAttribute("RemoveDuplicateLabels")
        Dim xMin As XmlAttribute = doc.CreateAttribute("xMin")
        Dim yMin As XmlAttribute = doc.CreateAttribute("yMin")
        Dim xMax As XmlAttribute = doc.CreateAttribute("xMax")
        Dim yMax As XmlAttribute = doc.CreateAttribute("yMax")
        Dim rotationField As XmlAttribute = doc.CreateAttribute("RotationField")

        AppendLine1.InnerText = ""
        AppendLine2.InnerText = ""
        PrependLine1.InnerText = ""
        PrependLine2.InnerText = ""

        Font.InnerText = "Microsoft Sans Serif"
        Size.InnerText = "8.25"
        UseLabelCollision.InnerText = "True"
        StandardViewWidth.InnerText = "0"
        Scaled.InnerText = "False"

        Dim lScale, lblRotation, lblShadowR, lblShadowB, lblShadowG, lblColorR, lblColorG, lblColorB As Integer
        Dim lblFieldName As String
        Try
            lblFieldName = oldLayer.Attributes("fieldName").InnerText
        Catch ex As Exception
            lblFieldName = ""
        End Try

        Dim sf As New MapWinGIS.Shapefile
        sf.Open(LayerFileName)
        For i As Integer = 0 To sf.NumFields - 1
            If sf.Field(i).Name = lblFieldName Then
                Field.InnerText = (i + 1).ToString
                Exit For
            End If
        Next
        If Field.InnerText = "" Then
            Field.InnerText = "0"
        End If
        sf.Close()
        Try
            lblColorR = oldLayer.Attributes("colorRed").InnerText
        Catch ex As Exception
            lblColorR = 0
        End Try
        Try
            lblColorG = oldLayer.Attributes("colorGreen").InnerText
        Catch ex As Exception
            lblColorG = 0
        End Try
        Try
            lblColorB = oldLayer.Attributes("colorBlue").InnerText
        Catch ex As Exception
            lblColorB = 0
        End Try
        Color.InnerText = Convert.ToUInt32(RGB(lblColorR, lblColorG, lblColorB)).ToString

        Try
            Justification.InnerText = oldLayer.Attributes("tkhJustification").InnerText
        Catch ex As Exception
            Justification.InnerText = "0"
        End Try

        Try
            RemoveDuplicateLabels.InnerText = oldLayer.Attributes("uniqueValues").InnerText
        Catch ex As Exception
            RemoveDuplicateLabels.InnerText = "False"
        End Try

        Try
            UseShadows.InnerText = oldLayer.Attributes("UseShadows").InnerText
        Catch ex As Exception
            UseShadows.InnerText = "False"
        End Try

        Try
            lblShadowR = oldLayer.Attributes("ShadowR").InnerText
        Catch ex As Exception
            lblShadowR = 0
        End Try
        Try
            lblShadowG = oldLayer.Attributes("ShadowG").InnerText
        Catch ex As Exception
            lblShadowG = 0
        End Try
        Try
            lblShadowB = oldLayer.Attributes("ShadowB").InnerText
        Catch ex As Exception
            lblShadowB = 0
        End Try
        ShadowColor.InnerText = Convert.ToUInt32(RGB(lblShadowR, lblShadowG, lblShadowB)).ToString

        Try
            UseMinZoomLevel.InnerText = oldLayer.Attributes("addDynamicVisiblity").InnerText
        Catch ex As Exception
            UseMinZoomLevel.InnerText = "False"
        End Try

        Try
            xMin.InnerText = oldLayer.Attributes("lXMin").InnerText
        Catch ex As Exception
            xMin.InnerText = "0"
        End Try
        Try
            xMax.InnerText = oldLayer.Attributes("lXMax").InnerText
        Catch ex As Exception
            xMax.InnerText = "0"
        End Try
        Try
            yMin.InnerText = oldLayer.Attributes("lYMin").InnerText
        Catch ex As Exception
            yMin.InnerText = "0"
        End Try
        Try
            yMax.InnerText = oldLayer.Attributes("lYMax").InnerText
        Catch ex As Exception
            yMax.InnerText = "0"
        End Try

        Try
            lScale = oldLayer.Attributes("lScale").InnerText
        Catch ex As Exception
            lScale = 0
        End Try
        If lScale > 0 Then
            Dim centerPoint As New MapWinGIS.Point
            centerPoint.x = extents.xMin + ((extents.xMax - extents.xMin) / 2)
            centerPoint.y = extents.yMin + ((extents.yMax - extents.yMin) / 2)
            Dim MapWidth As Integer = frmMain.MapMain.Width
            Dim MapHeight As Integer = frmMain.MapMain.Height

            Dim mapUnits As String = ""
            'taken from clsProject.vb
            If Not projection = "" Then
                If InStr(projection.ToLower, "+proj=longlat") > 0 Or InStr(projection.ToLower, "+proj=latlong") > 0 Then
                    mapUnits = "Lat/Long"
                ElseIf InStr(projection.ToLower, "+units=m") > 0 Then
                    mapUnits = "Meters"
                ElseIf InStr(projection.ToLower, "+units=ft") > 0 Then
                    mapUnits = "Feet"
                ElseIf InStr(projection.ToLower, "+to_meter=") > 0 Then
                    '---Cho 1/20/2009: Support for feet.
                    Dim toMeter As Double
                    toMeter = Convert.ToDouble(System.Text.RegularExpressions.Regex.Replace(projection.ToLower, "^.*to_meter=([.0-9]+).*$", "$1"))
                    If toMeter > 0.3047 And toMeter < 0.3049 Then
                        mapUnits = "Feet"
                    End If
                End If
            End If

            Dim tmpExt As MapWinGIS.Extents
            tmpExt = MapWinGeoProc.ScaleTools.ExtentFromScale(lScale, centerPoint, mapUnits, MapWidth, MapHeight)

            If tmpExt.xMax <> 0 Then
                xMin.InnerText = tmpExt.xMin.ToString
                xMax.InnerText = tmpExt.xMax.ToString
                yMin.InnerText = tmpExt.yMin.ToString
                yMax.InnerText = tmpExt.yMax.ToString
            End If
        End If

        If xMin.InnerText = "" Then
            xMin.InnerText = "0"
        End If
        If xMax.InnerText = "" Then
            xMax.InnerText = "0"
        End If
        If yMin.InnerText = "" Then
            yMin.InnerText = "0"
        End If
        If yMax.InnerText = "" Then
            yMax.InnerText = "0"
        End If
        'TODO: Do something with rotation info
        Try
            rotationField.InnerText = oldLayer.Attributes("labelRotationFieldName").InnerText
        Catch ex As Exception
            rotationField.InnerText = "None"
        End Try
        Try
            lblRotation = oldLayer.Attributes("rotation").InnerText
        Catch ex As Exception
            lblRotation = 0
        End Try

        Labels.Attributes.Append(AppendLine1)
        Labels.Attributes.Append(AppendLine2)
        Labels.Attributes.Append(PrependLine1)
        Labels.Attributes.Append(PrependLine2)
        Labels.Attributes.Append(Field)
        Labels.Attributes.Append(Font)
        Labels.Attributes.Append(Size)
        Labels.Attributes.Append(Color)
        Labels.Attributes.Append(Justification)
        Labels.Attributes.Append(UseMinZoomLevel)
        Labels.Attributes.Append(Scaled)
        Labels.Attributes.Append(UseShadows)
        Labels.Attributes.Append(ShadowColor)
        Labels.Attributes.Append(Offset)
        Labels.Attributes.Append(StandardViewWidth)
        Labels.Attributes.Append(UseLabelCollision)
        Labels.Attributes.Append(RemoveDuplicateLabels)
        Labels.Attributes.Append(rotationField)
        Labels.Attributes.Append(xMin)
        Labels.Attributes.Append(yMin)
        Labels.Attributes.Append(xMax)
        Labels.Attributes.Append(yMax)

        root.AppendChild(Labels)
        doc.Save(lblFileName)
    End Sub

    Private Sub TranslateLegacyVWRShapeFileElement(ByRef newDoc As Xml.XmlDocument, ByRef newLayer As Xml.XmlNode, ByRef oldLayer As Xml.XmlNode, ByVal projectPath As String)
        Dim shpFileProp As XmlElement = newDoc.CreateElement("ShapeFileProperties")
        Dim color As XmlAttribute = newDoc.CreateAttribute("Color")
        Dim drawFill As XmlAttribute = newDoc.CreateAttribute("DrawFill")
        Dim transPercent As XmlAttribute = newDoc.CreateAttribute("TransparencyPercent")
        Dim fillStipple As XmlAttribute = newDoc.CreateAttribute("FillStipple")
        Dim lineOrPointSize As XmlAttribute = newDoc.CreateAttribute("LineOrPointSize")
        Dim lineStipple As XmlAttribute = newDoc.CreateAttribute("LineStipple")
        Dim outLineColor As XmlAttribute = newDoc.CreateAttribute("OutLineColor")
        Dim pointType As XmlAttribute = newDoc.CreateAttribute("PointType")
        Dim customFillStipple As XmlAttribute = newDoc.CreateAttribute("CustomFillStipple")
        Dim customLineStipple As XmlAttribute = newDoc.CreateAttribute("CustomLineStipple")
        Dim useTransparency As XmlAttribute = newDoc.CreateAttribute("UseTransparency")
        Dim transparencyColor As XmlAttribute = newDoc.CreateAttribute("TransparencyColor")
        Dim MapTooltipField As XmlAttribute = newDoc.CreateAttribute("MapTooltipField")
        Dim MapTooltipsEnabled As XmlAttribute = newDoc.CreateAttribute("MapTooltipsEnabled")
        Dim VertVisible As XmlAttribute = newDoc.CreateAttribute("VerticesVisible")
        Dim LabelsVisible As XmlAttribute = newDoc.CreateAttribute("LabelsVisible")
        Dim customPointType As XmlElement = newDoc.CreateElement("CustomPointType")

        If Not oldLayer.Item("ShapeFileProperties") Is Nothing Then
            With oldLayer.Item("ShapeFileProperties")
                Try
                    LabelsVisible.InnerText = oldLayer.Attributes("LabelsVisible").InnerText
                Catch ex As Exception
                End Try
                Try
                    Dim colorR, colorG, colorB As Integer
                    Try
                        colorR = .Attributes("ColorR").InnerText
                    Catch ex As Exception
                        colorR = 0
                    End Try
                    Try
                        colorG = .Attributes("ColorG").InnerText
                    Catch ex As Exception
                        colorG = 0
                    End Try
                    Try
                        colorB = .Attributes("ColorB").InnerText
                    Catch ex As Exception
                        colorB = 0
                    End Try
                    color.InnerText = Convert.ToUInt32(RGB(colorR, colorG, colorB)).ToString
                    outLineColor.InnerText = Convert.ToUInt32(RGB(colorR, colorG, colorB)).ToString
                Catch ex As Exception
                    color.InnerText = "0"
                    outLineColor.InnerText = "0"
                End Try


                Try
                    fillStipple.InnerText = .Attributes("FillStipple").InnerText
                Catch ex As Exception
                    fillStipple.InnerText = "0"
                End Try
                Try
                    lineOrPointSize.InnerText = .Attributes("LineOrPointSize").InnerText
                Catch ex As Exception
                    lineOrPointSize.InnerText = "1"
                End Try
                Try
                    lineStipple.InnerText = .Attributes("LineStipple").InnerText
                Catch ex As Exception
                    lineStipple.InnerText = "0"
                End Try
                Try
                    pointType.InnerText = .Attributes("PointType").InnerText
                Catch ex As Exception
                    pointType.InnerText = "0"
                End Try
                Try
                    transPercent.InnerText = .Attributes("TransparencyPercent").InnerText
                Catch ex As Exception
                    transPercent.InnerText = "0"
                End Try
                If transPercent.InnerText = "" Then
                    transPercent.InnerText = "0"
                End If
                Try
                    Dim numtype As MapWindow.Interfaces.eLayerType = oldLayer.Attributes("Type").InnerText
                    If numtype = MapWindow.Interfaces.eLayerType.PointShapefile Then
                        VertVisible.InnerText = True.ToString
                    Else
                        VertVisible.InnerText = False.ToString
                    End If

                    If numtype = MapWindow.Interfaces.eLayerType.PolygonShapefile Then
                        drawFill.InnerText = True.ToString
                    Else
                        drawFill.InnerText = False.ToString
                    End If
                Catch ex As Exception
                    VertVisible.InnerText = False.ToString
                    drawFill.InnerText = False.ToString
                End Try
                customLineStipple.InnerText = ""
                useTransparency.InnerText = False.ToString
                transparencyColor.InnerText = ""
                MapTooltipField.InnerText = ""
                MapTooltipsEnabled.InnerText = False.ToString
            End With
        End If

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
        shpFileProp.Attributes.Append(customLineStipple)
        shpFileProp.Attributes.Append(useTransparency)
        shpFileProp.Attributes.Append(transparencyColor)

        Dim image As XmlElement = newDoc.CreateElement("Image")
        Dim itype As XmlAttribute = newDoc.CreateAttribute("Type")
        image.Attributes.Append(itype)
        customPointType.AppendChild(image)
        shpFileProp.AppendChild(customPointType)


        Dim leg As XmlElement = newDoc.CreateElement("Legend")
        Dim colorBreaks As XmlElement = newDoc.CreateElement("ColorBreaks")
        Dim fieldIndex As XmlAttribute = newDoc.CreateAttribute("FieldIndex")
        Dim key As XmlAttribute = newDoc.CreateAttribute("Key")
        Dim numBreaks As XmlAttribute = newDoc.CreateAttribute("NumberOfBreaks")

        If Not oldLayer.Item("ShapeFileColorProperties") Is Nothing Then
            With oldLayer.Item("ShapeFileColorProperties")
                'TODO: save shpColorProMarker
                Dim sfpath As String
                Try
                    sfpath = IO.Path.GetDirectoryName(projectPath) + IO.Path.DirectorySeparatorChar + IO.Path.GetFileName(oldLayer.Attributes("Path").InnerText)
                    Dim sf As New MapWinGIS.Shapefile
                    If sf.Open(sfpath) Then
                        Try
                            Dim fieldName As String = .Attributes("cFieldName").InnerText
                            For i As Integer = 0 To sf.NumFields - 1
                                If sf.Field(i).Name = fieldName Then
                                    fieldIndex.InnerText = i
                                    Exit For
                                End If
                            Next
                        Catch ex As Exception
                        Finally
                            sf.Close()
                        End Try
                    End If
                Catch ex As Exception
                End Try


                key.InnerText = ""
                numBreaks.InnerText = .ChildNodes.Count.ToString

                For i As Integer = 0 To .ChildNodes.Count - 1
                    Dim break As XmlElement = newDoc.CreateElement("Break")
                    Dim endColor As XmlAttribute = newDoc.CreateAttribute("EndColor")
                    Dim endValue As XmlAttribute = newDoc.CreateAttribute("EndValue")
                    Dim startColor As XmlAttribute = newDoc.CreateAttribute("StartColor")
                    Dim StartValue As XmlAttribute = newDoc.CreateAttribute("StartValue")
                    Dim caption As XmlAttribute = newDoc.CreateAttribute("Caption")
                    Dim Visible As XmlAttribute = newDoc.CreateAttribute("Visible")

                    Try
                        startColor.InnerText = .ChildNodes(i).Attributes("colorVal").InnerText
                    Catch ex As Exception
                    End Try
                    Try
                        endColor.InnerText = .ChildNodes(i).Attributes("colorVal").InnerText
                    Catch ex As Exception
                    End Try
                    Try
                        StartValue.InnerText = .ChildNodes(i).Attributes("Value").InnerText
                    Catch ex As Exception
                    End Try
                    Try
                        endValue.InnerText = .ChildNodes(i).Attributes("Value").InnerText
                    Catch ex As Exception
                    End Try
                    Try
                        caption.InnerText = .ChildNodes(i).Attributes("TextVal").InnerText
                    Catch ex As Exception
                    End Try
                    If StartValue.InnerText = "(Blank / Empty)" Then
                        StartValue.InnerText = ""
                        endValue.InnerText = ""
                    End If

                    Dim breakTrans As String
                    Try
                        breakTrans = .ChildNodes(i).Attributes("TransPercent").InnerText
                    Catch ex As Exception
                        breakTrans = "0"
                    End Try
                    If breakTrans = "100" Then
                        Visible.InnerText = False.ToString
                    Else
                        Visible.InnerText = True.ToString
                    End If

                    break.Attributes.Append(startColor)
                    break.Attributes.Append(endColor)
                    break.Attributes.Append(StartValue)
                    break.Attributes.Append(endValue)
                    break.Attributes.Append(caption)
                    break.Attributes.Append(Visible)

                    colorBreaks.AppendChild(break)
                Next

                If .ChildNodes.Count > 0 Then
                    leg.Attributes.Append(fieldIndex)
                    leg.Attributes.Append(key)
                    leg.Attributes.Append(numBreaks)
                    leg.AppendChild(colorBreaks)

                    shpFileProp.AppendChild(leg)
                End If
            End With
        End If

        newLayer.AppendChild(shpFileProp)
    End Sub

    Private Sub TranslateLegacyVWRGridElement(ByRef newDoc As Xml.XmlDocument, ByRef newLayer As Xml.XmlNode, ByRef oldLayer As Xml.XmlNode)
        Dim grid As XmlElement = newDoc.CreateElement("GridProperty")
        Dim transparentColor As XmlAttribute = newDoc.CreateAttribute("TransparentColor")
        Dim useTransparency As XmlAttribute = newDoc.CreateAttribute("UseTransparency")

        Try
            transparentColor.InnerText = oldLayer.Item("GridProperty").Attributes("TransparentColor").InnerText
        Catch ex As Exception
        End Try

        Try
            useTransparency.InnerText = oldLayer.Item("GridProperty").Attributes("TransparentColor").InnerText
        Catch ex As Exception
        End Try

        grid.Attributes.Append(transparentColor)
        grid.Attributes.Append(useTransparency)

        newLayer.AppendChild(grid)
    End Sub
End Class
