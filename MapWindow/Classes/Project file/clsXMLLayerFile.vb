'********************************************************************************************************
'Filename:      clsXMLLayerFile.vb
'Description:    
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
'28-oct-2010 - sleschinski - added saving loading of symbology plug-in settings and dynamic visibility

Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml
Imports System.Xml.Serialization

''' <summary>
''' A class to save and load properties of the layer in XML format. It means in a separate .mwlyr file and or in the project file
''' </summary>
''' <remarks></remarks>
Friend Class clsXMLLayerFile
    Private m_ErrorOccured As Boolean
    Private m_ErrorMsg As String = "The following errors occured:" + Chr(13) + Chr(13)

#Region "Saving"
    ''' <summary>
    ''' Creates layer file (.mwlyr) and saves layer attributes in it
    ''' </summary>
    Public Function SaveLayerFile(ByRef Filename As String, ByRef Layer As Interfaces.Layer) As Boolean

        ' checking parameters
        If (Layer Is Nothing) Then Return False

        If (String.IsNullOrEmpty(Filename)) Then Return False
        If (System.IO.Path.GetExtension(Filename) <> ".mwlyr") Then Return False
        If (IO.File.Exists(Filename)) Then Return False

        ' creating XML file
        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml("<MapWindow version= '""'></MapWindow>")     ' add more parameters: type of file and revision
        Dim xelRoot As XmlElement = xmlDoc.DocumentElement

        Dim attrType As XmlAttribute = xmlDoc.CreateAttribute("Type")
        attrType.InnerText = "Layer file"
        xelRoot.Attributes.Append(attrType)

        Dim xelLayer As XmlElement = Layer2XML(Layer, xmlDoc, Filename)
        xelRoot.AppendChild(xelLayer)

        xmlDoc.Save(Filename)

        Return True
    End Function

    ''' <summary>
    ''' Serializes layer properties
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="xmlDoc"></param>
    ''' <param name="FileName">Name of the project file or layer file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Layer2XML(ByRef Layer As Interfaces.Layer, ByRef xmlDoc As Xml.XmlDocument, ByVal FileName As String) As XmlElement

        ' layer general properties
        Dim xelLayer As XmlElement = LayerProperties2XML(Layer, xmlDoc, FileName)
        If xelLayer Is Nothing Then Return Nothing

        Dim xel As XmlElement
        If TypeOf (Layer.GetObject) Is MapWinGIS.IShapefile Then

            ' shapefile properties
            Dim obj As Object = frmMain.MapMain.get_GetObject(Layer.Handle)
            xel = Shapefile2XML(CType(obj, MapWinGIS.Shapefile), xmlDoc)
            If Not xel Is Nothing Then xelLayer.AppendChild(xel)

        ElseIf TypeOf (Layer.GetObject) Is MapWinGIS.IImage Or TypeOf (Layer.GetObject) Is MapWinGIS.Grid Then

            ' grid properties
            xel = GridToXML(Layer, xmlDoc)
            If Not xel Is Nothing Then xelLayer.AppendChild(xel)

        End If

        ' labels
        xel = Labels2XML(Layer, xmlDoc)
        If Not xel Is Nothing Then xelLayer.AppendChild(xel)

        Return xelLayer
    End Function

    ''' <summary>
    '''  Generates XML element with the general properties of the given layer
    ''' </summary>
    Public Function LayerProperties2XML(ByRef Layer As Interfaces.Layer, ByRef xmlDoc As Xml.XmlDocument, ByVal Filename As String) As XmlElement

        Dim xelLayer As XmlElement = xmlDoc.CreateElement("Layer")

        Dim names As String() = {"Name", "GroupName", "Type", "Path", "Tag", "Visible", "Expanded", "DynamicVisibility", "MinVisibleScale", "MaxVisibleScale"}

        Dim attributes(names.Length - 1) As XmlAttribute
        For i As Integer = 0 To attributes.Length - 1
            attributes(i) = xmlDoc.CreateAttribute(names(i))
        Next i

        'set the properties of the elements
        attributes(0).InnerText = Layer.Name()                                                ' name
        attributes(1).InnerText = frmMain.Layers.Groups.ItemByHandle(Layer.GroupHandle).Text  ' group name
        attributes(2).InnerText = CInt(Layer.LayerType).ToString                              ' layer type
        attributes(3).InnerText = XmlProjectFile.GetRelativePath(Layer.FileName, Filename)    ' path
        attributes(4).InnerText = Layer.Tag()                                                 ' tag
        attributes(5).InnerText = Layer.Visible().ToString                                    ' visible
        attributes(6).InnerText = Layer.Expanded.ToString                                     ' expanded
        attributes(7).InnerText = Layer.UseDynamicVisibility
        attributes(8).InnerText = Layer.MinVisibleScale
        attributes(9).InnerText = Layer.MaxVisibleScale

        For i As Integer = 0 To attributes.Length - 1
            xelLayer.Attributes.Append(attributes(i))
        Next i

        Return xelLayer

    End Function

    '''' <summary>
    ''''  Serailizes settings of the symbology plug-in
    '''' </summary>
    '''' <param name="LayerHandle"></param>
    '''' <param name="xmlDoc"></param>
    'Public Sub SymbologySettings2XML(ByVal LayerHandle As Integer, ByRef xmlDoc As Xml.XmlDocument)
    '    Dim layer As LegendControl.Layer = frmMain.View.LegendControl.Layers.ItemByHandle(LayerHandle)
    '    If layer Is Nothing Then Return

    '    Dim settings As MapWindow.Interfaces.SymbologySettings = layer.SymbologyPlugin
    '    MapWinUtility.Serialization.Serialize(settings, xmlDoc.DocumentElement)
    'End Sub

    '''' <summary>
    '''' A generic method for serialization of object as a child of the given XML node
    '''' </summary>
    'Public Sub Serialize(ByRef obj As Object, ByRef Parent As XmlElement)

    '    ' creating serializer for the object
    '    Dim serializer As New XmlSerializer(obj.GetType()) ', root)

    '    ' creating writer for the specified node
    '    Dim wr As XmlWriter = Parent.CreateNavigator.AppendChild()

    '    ' Serialize method will try to create new Doc otherwise and will violate wr.Settings.ConformanceLevel = Fragment
    '    wr.WriteComment("")

    '    ' for not writing namespace information
    '    Dim namespaces As New XmlSerializerNamespaces()
    '    namespaces.Add(String.Empty, String.Empty)

    '    ' serialization
    '    serializer.Serialize(wr, obj, namespaces)
    '    wr.Flush()
    '    wr.Close()
    'End Sub

    '''' <summary>
    '''' Generic method for deserialization of an object from the given node
    '''' </summary>
    '''' <param name="xelParent"></param>
    '''' <param name="t"></param>
    '''' <returns></returns>
    'Public Function Deserialize(ByRef xelParent As XmlElement, ByVal t As System.Type) As Object

    '    ' seeking the name of element; XmlRootAttribute of the class is used
    '    ' TODO: use name of type otherwise
    '    Dim ElementName As String = String.Empty
    '    For Each attr As Attribute In t.GetCustomAttributes(True)
    '        If TypeOf attr Is XmlRootAttribute Then
    '            ElementName = CType(attr, XmlRootAttribute).ElementName
    '        End If
    '    Next

    '    If ElementName <> String.Empty Then
    '        Dim xel As XmlElement = xelParent.Item(ElementName)
    '        If Not xel Is Nothing Then
    '            ' creating serializer
    '            Dim serializer As New XmlSerializer(t)

    '            ' deserializing
    '            Dim reader As New XmlNodeReader(xel)
    '            Return serializer.Deserialize(reader)
    '        End If
    '    End If

    '    Return Nothing
    'End Function

    ''' <summary>
    ''' Generates XML element with the properties of drawing options of the shapefile
    ''' </summary>
    Public Function Shapefile2XML(ByRef sf As MapWinGIS.Shapefile, ByRef xmlDoc As Xml.XmlDocument) As XmlElement

        If sf Is Nothing Then Return Nothing

        'Dim xelShapefile As XmlElement = xmlDoc.CreateElement("ShapefileProperties")
        Dim names(,) As String = GetPropertyMappingTable(sf)
        Dim xelShapefile As XmlElement = Me.SaveObject(xmlDoc, sf, "ShapefileProperties", names, Nothing)

        Dim xel As XmlElement

        Dim sdoDefault As MapWinGIS.ShapeDrawingOptions = sf.DefaultDrawingOptions
        names = GetPropertyMappingTable(sdoDefault)

        If (Not names Is Nothing) Then

            ' default drawing options
            xel = Me.SaveObject(xmlDoc, sdoDefault, "DefaultDrawingOptions", names, Nothing)
            If Not xel Is Nothing Then
                SaveLinePattern(sdoDefault.LinePattern, xmlDoc, xel)
                xelShapefile.AppendChild(xel)
            End If

            ' categories
            If (sf.Categories.Count > 0) Then
                Dim xelCategories As XmlElement = xmlDoc.CreateElement("ShapefileCategories")
                Dim xelOptions As XmlElement = Nothing

                Dim namesCategory(,) As String = GetPropertyMappingTable(sf.Categories.Item(0))

                If Not namesCategory Is Nothing Then
                    For i As Integer = 0 To sf.Categories.Count - 1
                        xel = Me.SaveObject(xmlDoc, sf.Categories.Item(i), "ShapefileCategory", namesCategory, Nothing)

                        If Not xel Is Nothing Then
                            xelCategories.AppendChild(xel)
                            Dim sdo As MapWinGIS.ShapeDrawingOptions = sf.Categories.Item(i).DrawingOptions
                            xelOptions = Me.SaveObject(xmlDoc, sdo, "DrawingOptions", names, sdoDefault)
                            If Not xelOptions Is Nothing Then
                                SaveLinePattern(sdo.LinePattern, xmlDoc, xel)
                                xel.AppendChild(xelOptions)
                            End If
                        End If
                    Next i


                    xelShapefile.AppendChild(xelCategories)
                End If
            End If
        End If

        ' serializing charts
        names = GetPropertyMappingTable(sf.Charts)
        Dim xelCharts As XmlElement = Me.SaveObject(xmlDoc, sf.Charts, "Charts", names, Nothing)

        If Not xelCharts Is Nothing Then

            Dim xelChartFields As XmlElement = xmlDoc.CreateElement("ChartFields")
            If Not xelChartFields Is Nothing Then
                If sf.Charts.NumFields > 0 Then
                    names = GetPropertyMappingTable(sf.Charts.Field(0))
                    For i As Integer = 0 To sf.Charts.NumFields - 1
                        xel = Me.SaveObject(xmlDoc, sf.Charts.Field(i), "ChartField", names, Nothing)
                        If Not xel Is Nothing Then xelChartFields.AppendChild(xel)
                    Next i
                    xelCharts.AppendChild(xelChartFields)
                End If

                ' positions are saved only in case the fields are present
                If (sf.Charts.Count > 0) Then
                    Dim xelChartPositions As XmlElement = xmlDoc.CreateElement("ChartPositions")
                    If Not xelChartPositions Is Nothing Then
                        If sf.Charts.Count > 0 Then
                            names = GetPropertyMappingTable(sf.Charts.Chart(0))
                            For i As Integer = 0 To sf.Charts.Count - 1
                                xel = Me.SaveObject(xmlDoc, sf.Charts.Chart(i), "Chart", names, Nothing)
                                If Not xel Is Nothing Then xelChartPositions.AppendChild(xel)
                            Next i
                            xelCharts.AppendChild(xelChartPositions)
                        End If
                    End If
                End If

                xelShapefile.AppendChild(xelCharts)

            End If
        End If

        Return xelShapefile
    End Function

    ''' <summary>
    ''' Saves line pattern as a child of the xml element provided
    ''' </summary>
    Public Sub SaveLinePattern(ByVal pattern As MapWinGIS.LinePattern, ByVal xmlDoc As XmlDocument, ByRef parent As XmlElement)
        If pattern Is Nothing Then Return

        Dim xel As XmlElement = xmlDoc.CreateElement("LinePattern")
        For i As Integer = 0 To pattern.Count - 1
            Dim line As MapWinGIS.LineSegment = pattern.Line(i)
            Dim xelLine As XmlElement = SaveObject(xmlDoc, line, "Line", GetPropertyMappingTable(line))
            xel.AppendChild(xelLine)
        Next
        parent.AppendChild(xel)
    End Sub

    ''' <summary>
    ''' Saves the properties of image. An attempt to work around strange error with casting, while doing it in GridToXml
    ''' </summary>
    Private Function SaveImageProperties(ByRef img As MapWinGIS.Image, ByRef xmlDoc As XmlDocument) As XmlElement
        Dim names(,) As String = GetPropertyMappingTable(img) : If names Is Nothing Then Return Nothing
        Return Me.SaveObject(xmlDoc, img, "ImageProperties", names, Nothing)
    End Function

    ''' <summary>
    ''' Generates XML element with the properties of grid
    ''' </summary>
    Public Function GridToXML(ByVal Layer As MapWindow.Layer, ByVal xmlDoc As XmlDocument) As XmlElement

        If Layer Is Nothing Then Return Nothing

        Dim img As New MapWinGIS.Image
        img = frmMain.MapMain.get_Image(Layer.Handle)
        If img Is Nothing Then Return Nothing
        Dim xelImage As XmlElement = Me.SaveImageProperties(img, xmlDoc)

        'If Not TypeOf (obj) Is MapWinGIS.Image Then Return Nothing
        'Dim xelImage As XmlElement = SaveImageProperties(CType(obj, MapWinGIS.Image), xmlDoc)

        ' image properties
        'names = GetPropertyMappingTable(img) : If names Is Nothing Then Return Nothing
        'xelImage = Me.SaveObject(xmlDoc, img, "ImageProperties", names, Nothing) : If xelImage Is Nothing Then Return Nothing

        ' saving GridColorScheme
        Dim names(,) As String
        If Not Layer.ColoringScheme Is Nothing Then

            Dim xelGrid As XmlElement = xmlDoc.CreateElement("GridProperties")
            Dim xel As XmlElement

            Dim scheme As MapWinGIS.GridColorScheme = Layer.ColoringScheme
            names = GetPropertyMappingTable(scheme)

            If Not names Is Nothing Then
                Dim xelColorScheme As XmlElement = Me.SaveObject(xmlDoc, scheme, "GridColorScheme", names, Nothing)

                ' saving breaks
                If Not xelColorScheme Is Nothing Then
                    If scheme.NumBreaks > 0 Then
                        names = GetPropertyMappingTable(New MapWinGIS.GridColorBreak)
                        If Not names Is Nothing Then
                            For i As Integer = 0 To scheme.NumBreaks - 1
                                xel = Me.SaveObject(xmlDoc, scheme.Break(i), "GridColorBreak", names)
                                If Not xel Is Nothing Then
                                    xelColorScheme.AppendChild(xel)
                                End If
                            Next i
                        End If
                    End If

                    xelGrid.AppendChild(xelColorScheme)
                End If
            End If

            xelImage.AppendChild(xelGrid)
        End If

        Return xelImage
    End Function

    ''' <summary>
    ''' Generates XML element with the properties of the labels
    ''' </summary>
    Public Function Labels2XML(ByRef Layer As Interfaces.Layer, ByRef xmlDoc As Xml.XmlDocument) As XmlElement

        Dim xelLabels As XmlElement = xmlDoc.CreateElement("LabelProperties")

        Dim lb As MapWinGIS.Labels = Nothing
        Dim obj As Object = Layer.GetObject

        If TypeOf (obj) Is MapWinGIS.IShapefile Then lb = CType(obj, MapWinGIS.IShapefile).Labels
        If TypeOf (obj) Is MapWinGIS.IImage Then lb = CType(obj, MapWinGIS.IImage).Labels
        If (lb Is Nothing) Then Return Nothing
        'If (lb.Count = 0) Then Return Nothing

        Dim xel As XmlElement
        Dim names(,) As String

        ' saving default options
        Dim catDefault As MapWinGIS.LabelCategory = lb.Options

        names = GetPropertyMappingTable(catDefault)
        If (Not names Is Nothing) Then
            xel = Me.SaveObject(xmlDoc, catDefault, "DefaultLabelOptions", names, Nothing)
            ' saves the properties not included in the label category
            xel = Me.SaveObject(xmlDoc, lb, xel, GetPropertyMappingTable(lb), Nothing)
            If Not xel Is Nothing Then xelLabels.AppendChild(xel)
        End If

        ' saving category options   
        names = GetPropertyMappingTable(catDefault)
        If (Not names Is Nothing) Then

            Dim xelCategories As XmlElement
            xelCategories = xmlDoc.CreateElement("LabelCategories")

            For i As Integer = 0 To lb.NumCategories - 1
                xel = SaveObject(xmlDoc, lb.Category(i), "LabelCategory", names, catDefault)
                If Not xel Is Nothing Then xelCategories.AppendChild(xel)
            Next i

            xelLabels.AppendChild(xelCategories)
        End If

        ' saving labels
        Dim label As New MapWinGIS.Label
        Dim labelDefault As New MapWinGIS.Label
        'Dim el As XmlElement
        names = GetPropertyMappingTable(label)

        If Not names Is Nothing Then

            Dim xelData As XmlElement
            xelData = xmlDoc.CreateElement("Labels")

            Dim el As XmlElement
            For i As Integer = 0 To lb.Count - 1
                label = lb.Label(i, 0)
                el = SaveLabel(label, xmlDoc)
                xelData.AppendChild(el)
                'xel = SaveObject(xmlDoc, label, "Label", names, labelDefault)
                'If Not xel Is Nothing Then xelData.AppendChild(xel)
            Next i

            xelLabels.AppendChild(xelData)
        End If

        Return xelLabels

    End Function

    ''' <summary>
    ''' Saves single label data to the XML element. Reflectio isn't used in this case to ensure performance
    ''' </summary>
    Private Function SaveLabel(ByRef label As MapWinGIS.Label, ByRef xmlDoc As Xml.XmlDocument) As XmlElement
        Dim el As XmlElement = xmlDoc.CreateElement("L")
        Dim attr As XmlAttribute

        attr = xmlDoc.CreateAttribute("X")
        attr.InnerText = Convert.ToString(CSng(label.x))
        el.Attributes.Append(attr)

        attr = xmlDoc.CreateAttribute("Y")
        attr.InnerText = Convert.ToString(CSng(label.y))
        el.Attributes.Append(attr)

        'If label.Rotation <> 0 Then
        '    attr = xmlDoc.CreateAttribute("R")
        '    attr.InnerText = Convert.ToString(CSng(label.Rotation))
        '    el.Attributes.Append(attr)
        'End If

        'If label.Category <> -1 Then
        '    attr = xmlDoc.CreateAttribute("C")
        '    attr.InnerText = Convert.ToString(label.Category)
        '    el.Attributes.Append(attr)
        'End If

        'If label.Text <> "" Then
        '    attr = xmlDoc.CreateAttribute("T")
        '    attr.InnerText = label.Text
        '    el.Attributes.Append(attr)
        'End If

        Return el
    End Function
#End Region

#Region "Loading"

    ''' <summary>
    ''' Loads layer file to the project
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadLayerFile(ByVal Filename As String) As Boolean
        If Not IO.File.Exists(Filename) Then Return False

        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(Filename)
        Dim xelRoot As XmlElement = xmlDoc.DocumentElement : If xelRoot Is Nothing Then Return False

        If (xelRoot.Name.ToLower() <> "mapwindow") Then Return False ' report error

        ' add check of revision and mapwindow version if needed

        ' checking type
        Dim node As XmlNode = xelRoot.Attributes.GetNamedItem("Type") : If node Is Nothing Then Return False
        If (node.InnerText <> "Layer file") Then Return False

        ' reading layer properties
        Dim xelLayer As XmlElement = CType(xelRoot.Item("Layer"), XmlElement) : If xelLayer Is Nothing Then Return False

        Dim cwd As String = CurDir()
        ChDir(System.IO.Path.GetDirectoryName(Filename))
        Dim Layer As MapWindow.Layer = LoadLayer(xelLayer)

        ChDir(cwd)

        If Not Layer Is Nothing Then
            frmMain.MapMain.Redraw()
        End If
    End Function

    ''' <summary>
    ''' Loads in the project from the specified XML element. Is used for layer files and project files
    ''' </summary>
    ''' <param name="xelLayer">XMLElement which holds information about the layer</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadLayer(ByVal xelLayer As XmlElement) As MapWindow.Layer

        MapWinUtility.Logger.Dbg("In LoadLayer. Adding a layer")

        ' adding a layer
        Dim node As XmlNode = xelLayer.Attributes.GetNamedItem("Path") : If node Is Nothing Then Return Nothing
        Dim path As String = node.InnerText

        Dim name As String = String.Empty
        node = xelLayer.Attributes.GetNamedItem("Name")
        If Not node Is Nothing Then name = node.InnerText

        Dim Layer As MapWindow.Layer = Nothing
        If (System.IO.File.Exists(path) = False) Then
            ' This operation will have changed the current working directory.
            ' Preserve it and set it back, or all subsequent layers will not be found either.
            Dim cwd As String = CurDir()

            ' PromptBrowse will set the file path by reference and return true,
            ' or return false if the user cancels.
            If Not XmlProjectFile.PromptToBrowse(path, name) Then Return Nothing

            Layer = frmMain.m_layers.AddLayer(IO.Path.GetFullPath(path), , , , , , , , , , , , , False)(0)  ' false - XMLInfo files must not be loaded

            'Restore CWD
            ChDir(cwd)
        Else
            'add some other layer
            Layer = frmMain.m_layers.AddLayer(IO.Path.GetFullPath(path), , , , , , , , , , , , , False)(0)  ' false - XMLInfo files must not be loaded
        End If

        Layer.Name = name
        MapWinUtility.Logger.Dbg("In LoadLayer. Layer.Name: " & Layer.Name)

        'If Not PluginCall Then
        node = xelLayer.Attributes.GetNamedItem("GroupName")
        Dim groupName As String = ""
        Dim destGroup As Integer = -1
        If Not node Is Nothing Then groupName = node.InnerText

        If Not groupName.Trim() = "" Then
            For i As Integer = 0 To frmMain.Layers.Groups.Count - 1
                If frmMain.Layers.Groups(i).Text.ToLower().Trim() = groupName.ToLower.Trim() And Not groupName.Trim() = "" Then
                    destGroup = frmMain.Layers.Groups(i).Handle
                    Exit For
                End If
            Next

            If destGroup = -1 Then
                destGroup = frmMain.Layers.Groups.Add(groupName.Trim(), 0)
            End If

            frmMain.Layers.MoveLayer(Layer.Handle, 0, destGroup)
        End If

        ' restoring visibility
        Dim bValue As Boolean
        node = xelLayer.Attributes.GetNamedItem("Visible")
        If Not node Is Nothing AndAlso Boolean.TryParse(node.InnerText, bValue) Then
            Layer.Visible = bValue
        End If

        ' loading dynamic visibility
        node = xelLayer.Attributes.GetNamedItem("DynamicVisibility")
        If Not node Is Nothing AndAlso Boolean.TryParse(node.InnerText, bValue) Then
            Layer.UseDynamicVisibility = bValue
        End If

        Dim dValue As Double
        node = xelLayer.Attributes.GetNamedItem("MaxVisibleScale")
        If Not node Is Nothing AndAlso Double.TryParse(node.InnerText, dValue) Then
            Layer.MaxVisibleScale = Convert.ToDouble(dValue)
        End If

        node = xelLayer.Attributes.GetNamedItem("MinVisibleScale")
        If Not node Is Nothing AndAlso Double.TryParse(node.InnerText, dValue) Then
            Layer.MinVisibleScale = Convert.ToDouble(node.InnerText)
        End If

        ' loading layer specific properties
        Dim obj As Object = Layer.GetObject()
        Dim labels As MapWinGIS.Labels = Nothing

        If TypeOf obj Is MapWinGIS.Shapefile Then
            LoadShapefileProperties(CType(obj, MapWinGIS.Shapefile), xelLayer)
            labels = CType(obj, MapWinGIS.Shapefile).Labels
        ElseIf TypeOf obj Is MapWinGIS.Image Then
            LoadImageProperties(CType(obj, MapWinGIS.Image), xelLayer)
            labels = CType(obj, MapWinGIS.Image).Labels
        End If

        ' loading label properties
        LoadLabelProperties(labels, xelLayer)


        MapWinUtility.Logger.Dbg("At the end of LoadLayer.")
        Return Layer
    End Function

    ''' <summary>
    ''' Loads shapefile properties from the specified layer element of XML file
    ''' </summary>
    Public Function LoadShapefileProperties(ByRef sf As MapWinGIS.Shapefile, ByRef xelLayer As XmlElement) As Boolean

        If xelLayer Is Nothing Then Exit Function
        If sf Is Nothing Then Exit Function

        Dim xelShapefile As XmlElement = xelLayer.Item("ShapefileProperties") : If xelShapefile Is Nothing Then Return False
        Dim names(,) As String = GetPropertyMappingTable(sf)
        LoadObjectState(xelShapefile, sf, names)

        names = GetPropertyMappingTable(sf.DefaultDrawingOptions)
        If names Is Nothing Then Return False

        For Each node As XmlNode In CType(xelShapefile, XmlNode).ChildNodes
            Select Case node.Name.ToLower()
                Case "defaultdrawingoptions"
                    LoadObjectState(node, sf.DefaultDrawingOptions, names)
                Case "selectiondrawingoptions"
                    LoadObjectState(node, sf.SelectionDrawingOptions, names)
                Case "shapefilecategories"
                    Dim cat As New MapWinGIS.ShapefileCategory
                    Dim namesCategories(,) As String = GetPropertyMappingTable(cat)

                    Dim options As New MapWinGIS.ShapeDrawingOptions
                    Dim namesOptions(,) As String = GetPropertyMappingTable(options)

                    Dim nodeOptions As XmlNode

                    For Each catNode As XmlNode In node.ChildNodes
                        cat = sf.Categories.Add("Category")
                        LoadObjectState(catNode, cat, namesCategories)

                        nodeOptions = catNode.Item("DrawingOptions")
                        LoadObjectState(nodeOptions, cat.DrawingOptions, namesOptions)
                    Next

                    sf.Categories.ApplyExpressions()
                Case "charts"
                    Dim chartNames(,) As String = GetPropertyMappingTable(sf.Charts)
                    LoadObjectState(node, sf.Charts, chartNames)

                    For Each catNode As XmlNode In node.ChildNodes

                        If catNode.Name.ToLower() = "chartfields" Then
                            For Each catObject As XmlNode In catNode.ChildNodes
                                Dim chartField As New MapWinGIS.ChartField
                                LoadObjectState(catObject, chartField, GetPropertyMappingTable(chartField))
                                sf.Charts.AddField(chartField)
                            Next
                        ElseIf catNode.Name.ToLower() = "chartpositions" Then

                            If sf.NumShapes > 0 Then
                                If catNode.ChildNodes.Count = sf.NumShapes Then     ' otherwise the data in the project file is obsolete
                                    ' generate uninitialized charts
                                    sf.Charts.Generate(MapWinGIS.tkLabelPositioning.lpNone)

                                    Dim chart As MapWinGIS.Chart = sf.Charts.Chart(0)
                                    Dim names2(,) As String = GetPropertyMappingTable(chart)
                                    Dim i As Integer

                                    If Not names2 Is Nothing Then
                                        For Each catObject As XmlNode In catNode.ChildNodes
                                            'Dim chart As New MapWinGIS.Chart
                                            chart = sf.Charts.Chart(i)
                                            LoadObjectState(catObject, chart, names2)
                                            'sf.Charts.Chart(i) = chart
                                            i = i + 1
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    Next
            End Select
        Next
        Return False
    End Function

    ''' <summary>
    ''' Loads image properties from the layer element of XML file
    ''' </summary>
    Public Function LoadImageProperties(ByVal img As MapWinGIS.Image, ByRef xelLayer As XmlElement) As Boolean

        If xelLayer Is Nothing Then Exit Function
        If img Is Nothing Then Exit Function

        Dim names(,) As String = GetPropertyMappingTable(img) : If names Is Nothing Then Return False
        Dim xelImage As XmlElement = xelLayer.Item("ImageProperties") : If xelImage Is Nothing Then Return False

        LoadObjectState(CType(xelImage, XmlNode), img, names)

        Return True
    End Function

    ''' <summary>
    ''' Loads grid properties from the layer element of XML file
    ''' </summary>
    Public Function LoadGridProperties() As Boolean
        ' TODO: implement !!!
        Return False
    End Function

    ''' <summary>
    ''' Loads label properties from the specified element of XML file
    ''' </summary>
    Public Function LoadLabelProperties(ByRef Labels As MapWinGIS.Labels, ByRef xelLayer As XmlElement) As Boolean
        If Labels Is Nothing Then Return False
        If xelLayer Is Nothing Then Return False

        Dim xelLabels As XmlElement = xelLayer.Item("LabelProperties") : If xelLabels Is Nothing Then Return False

        Dim catDefalt As MapWinGIS.LabelCategory = Labels.Options : If catDefalt Is Nothing Then Return False
        Dim names(,) As String = GetPropertyMappingTable(catDefalt) : If names Is Nothing Then Return False

        For Each node As XmlNode In CType(xelLabels, XmlNode).ChildNodes
            Select Case node.Name.ToLower()
                Case "defaultlabeloptions"
                    LoadObjectState(node, catDefalt, names)
                    Labels.Options = catDefalt

                    ' properties not included in label category
                    Dim names1(,) As String = GetPropertyMappingTable(Labels)
                    LoadObjectState(node, Labels, names1)

                Case "labelcategories"
                    Dim cat As New MapWinGIS.LabelCategory
                    For Each catNode As XmlNode In node.ChildNodes
                        cat = Labels.AddCategory("Category")
                        LoadObjectState(catNode, cat, names)
                    Next
                Case "labels"
                    Dim label As New MapWinGIS.Label
                    Dim labelNames(,) As String = GetPropertyMappingTable(label)
                    If Not (labelNames Is Nothing) And Not (label Is Nothing) Then
                        For Each catNode As XmlNode In node.ChildNodes
                            Labels.AddLabel("", 0.0, 0.0)
                            label = Labels.Label(Labels.Count - 1, 0)
                            LoadObjectState(catNode, label, labelNames)
                        Next
                        Labels.Synchronized = True
                    End If
            End Select
        Next
    End Function


#End Region

#Region "Reflection Utilities"

    ''' <summary>
    ''' Saves properties of the single object in the given XmlElement. Element is created in the function.
    ''' </summary>
    Private Function SaveObject(ByRef xmlDoc As Xml.XmlDocument, ByRef Obj As Object, ByRef elementName As String, ByRef names As String(,), Optional ByRef ObjDefault As Object = Nothing) As XmlElement
        Dim element As XmlElement = xmlDoc.CreateElement(elementName)
        Return SaveObject(xmlDoc, Obj, element, names, ObjDefault)
    End Function

    ''' <summary>
    ''' Saves properties of the single object in the given XmlElement. Element is passed to the function
    ''' </summary>
    ''' <param name="xmlDoc">Xml element to save options in</param>
    ''' <param name="Obj">Object which options must be saved</param>
    ''' <param name="names">Mapping table - 2 dimension array in form '(Name of attribute)(Names of property)'</param>
    ''' <param name="ObjDefault">Object with default set of properties. Only values which differ from deafult one will be saved. 
    ''' Is Nothing in case all values are to be saved</param>
    Private Function SaveObject(ByRef xmlDoc As Xml.XmlDocument, ByRef Obj As Object, ByVal element As XmlElement, ByRef names As String(,), Optional ByRef ObjDefault As Object = Nothing) As XmlElement
        Dim i As Integer
        Dim writeValue As Boolean
        Dim prop As PropertyInfo

        For i = 0 To names.GetLength(0) - 1
            prop = Obj.GetType().GetProperty(names(i, 1))

            ' do we need to write this property or it's value is a default one
            writeValue = False

            If (Not prop Is Nothing) Then

                If (ObjDefault Is Nothing) Then
                    writeValue = True
                Else
                    Dim obj1 As Object = prop.GetValue(Obj, Nothing)
                    Dim obj2 As Object = prop.GetValue(ObjDefault, Nothing)

                    If ((Not obj1 Is Nothing) And (Not obj2 Is Nothing)) Then
                        If (obj1.ToString() <> obj2.ToString()) Then writeValue = True
                    Else
                        writeValue = True
                    End If
                End If

                ' writing value
                If (writeValue) Then
                    Dim val As Object = prop.GetValue(Obj, Nothing)
                    If TypeOf val Is MapWinGIS.Image Then
                        ' saving images
                        Dim img As MapWinGIS.Image = CType(val, MapWinGIS.Image)
                        Dim typ As String = ""
                        Dim xelImage As XmlElement = SaveObject(xmlDoc, img, names(i, 0), GetPropertyMappingTable(img))
                        xelImage.InnerText = XmlProjectFile.ConvertImageToString(img, typ)
                        element.AppendChild(xelImage)
                    Else

                        Dim attr As XmlAttribute = xmlDoc.CreateAttribute(names(i, 0))
                        If (Not val Is Nothing) Then
                            attr.InnerText = val.ToString()
                        Else
                            attr.InnerText = String.Empty
                        End If

                        element.Attributes.Append(attr)
                    End If
                End If
            End If
        Next i

        Return element
    End Function

    ''' <summary>
    ''' Loads properties of a given object from the specified XmlNode. In the names array there is mapping table in a form '(Name of attribute)(Names of property)'
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="Obj"></param>
    ''' <param name="names"></param>
    ''' <remarks></remarks>
    Private Sub LoadObjectState(ByRef node As XmlNode, ByRef Obj As Object, ByRef names As String(,))

        Dim prop As PropertyInfo
        Dim attr As Xml.XmlNode

        For i As Integer = 0 To names.GetLength(0) - 1

            attr = node.Attributes.GetNamedItem(names(i, 0))

            If (Not attr Is Nothing) Then

                prop = Obj.GetType().GetProperty(names(i, 1))

                If (Not prop Is Nothing) Then

                    Select Case prop.PropertyType.Name.ToLower()
                        Case "string"
                            prop.SetValue(Obj, attr.InnerText, Nothing)
                        Case "boolean"
                            prop.SetValue(Obj, Boolean.Parse(attr.InnerText), Nothing)
                        Case "int16"
                            Dim valUInt16 As System.Int16
                            If (Int16.TryParse(attr.InnerText, valUInt16)) Then prop.SetValue(Obj, valUInt16, Nothing)
                        Case "int32"
                            Dim valUInt32 As System.Int32
                            If (Int32.TryParse(attr.InnerText, valUInt32)) Then prop.SetValue(Obj, valUInt32, Nothing)
                        Case "uint32"
                            Dim valUInt32 As System.UInt32
                            If (UInt32.TryParse(attr.InnerText, valUInt32)) Then prop.SetValue(Obj, valUInt32, Nothing)
                        Case "double"
                            Dim valDouble As System.Double
                            If (Double.TryParse(attr.InnerText, valDouble)) Then prop.SetValue(Obj, valDouble, Nothing)
                        Case "single"
                            Dim valSingle As System.Single
                            If Single.TryParse(attr.InnerText, valSingle) Then prop.SetValue(Obj, valSingle, Nothing)
                        Case "object"
                            prop.SetValue(Obj, Convert.ToString(attr.InnerText), Nothing)

                        Case Else
                            ' there is assumption here that all the types left as enumerated constants, 
                            ' so everything other than it must be treated above !!!
                            Try
                                Dim value As Object = [Enum].Parse(prop.PropertyType, attr.InnerText)
                                If Not value Is Nothing Then prop.SetValue(Obj, value, Nothing)
                            Catch ex As Exception
                                ' do nothing - leave default values
                            End Try
                    End Select
                End If
            End If

        Next i

        If TypeOf Obj Is MapWinGIS.ShapeDrawingOptions Then
            Dim xel As XmlElement = node.Item("Picture")
            If Not xel Is Nothing Then
                Dim img As MapWinGIS.Image = XmlProjectFile.ConvertStringToImage(xel.InnerText, "MapWinGIS.Image")
                LoadObjectState(xel, img, GetPropertyMappingTable(img))
                Dim options As MapWinGIS.ShapeDrawingOptions = CType(Obj, MapWinGIS.ShapeDrawingOptions)
                options.Picture = img
            End If

            Dim xelPattern As XmlElement = node.Item("LinePattern")
            If Not xelPattern Is Nothing Then
                Dim pattern As MapWinGIS.LinePattern = New MapWinGIS.LinePattern()
                For Each catNode As XmlNode In xelPattern.ChildNodes
                    If catNode.Name.ToLower() = "line" Then
                        pattern.AddLine(RGB(0, 0, 0), 1.0F, MapWinGIS.tkDashStyle.dsSolid)
                        Dim line As MapWinGIS.LineSegment = pattern.Line(pattern.Count - 1)
                        LoadObjectState(catNode, line, GetPropertyMappingTable(line))
                    End If
                Next
                Dim options As MapWinGIS.ShapeDrawingOptions = CType(Obj, MapWinGIS.ShapeDrawingOptions)
                options.LinePattern = pattern
            End If
        End If
    End Sub

    ''' <summary>
    ''' Returns mapping table for properties of ShapeDrawingOptions class in the format (Name of attribute)(Names of property)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPropertyMappingTable(ByRef obj As Object) As String(,)

        ' (name of attribute in xml file)(name of property of object)
        If TypeOf obj Is MapWinGIS.ShapeDrawingOptions Then

            Dim names As String(,) = { _
                                {"Picture", "Picture"}, _
                                {"DrawingMode", "DrawingMode"}, _
                                {"FillBgColor", "FillBgColor"}, _
                                {"FillBgTransparent", "FillBgTransparent"}, _
                                {"FillColor", "FillColor"}, _
                                {"FillColor2", "FillColor2"}, _
                                {"FillGradientBounds", "FillGradientBounds"}, _
                                {"FillGradientRotation", "FillGradientRotation"}, _
                                {"FillGradientType", "FillGradientType"}, _
                                {"FillHatchStyle", "FillHatchStyle"}, _
                                {"FillTransparency", "FillTransparency"}, _
                                {"FillType", "FillType"}, _
                                {"FillVisible", "FillVisible"}, _
                                {"FontName", "FontName"}, _
                                {"LineColor", "LineColor"}, _
                                {"LineStipple", "LineStipple"}, _
                                {"LineTransparency", "LineTransparency"}, _
                                {"LineVisible", "LineVisible"}, _
                                {"LineWidth", "LineWidth"}, _
                                {"PictureScaleX", "PictureScaleX"}, _
                                {"PictureScaleY", "PictureScaleY"}, _
                                {"PointCharacter", "PointCharacter"}, _
                                {"PointShape", "PointShape"}, _
                                {"PointSidesCount", "PointSidesCount"}, _
                                {"PointSidesRatio", "PointSidesRatio"}, _
                                {"PointSize", "PointSize"}, _
                                {"PointType", "PointType"}, _
                                {"Rotation", "Rotation"}, _
                                {"Visible", "Visible"}, _
                                {"Tag", "Tag"}}
            Return names
        ElseIf TypeOf obj Is MapWinGIS.ShapefileCategory Then

            Dim names As String(,) = { _
                                {"Expression", "Expression"}, _
                                {"MaxValue", "MaxValue"}, _
                                {"MinValue", "MinValue"}, _
                                {"Name", "Name"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.Label Then

            Dim names As String(,) = { _
                                {"X", "x"}, _
                                {"Y", "y"}, _
                                {"Rotation", "Rotation"}, _
                                {"Name", "Text"}, _
                                {"Category", "Category"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.LabelCategory Then
            'Dim cat As MapWinGIS.LabelCategory

            Dim names As String(,) = { _
                                {"Alignment", "Alignment"}, _
                                {"Expression", "Expression"}, _
                                {"FontBold", "FontBold"}, _
                                {"FontColor", "FontColor"}, _
                                {"FontColor2", "FontColor2"}, _
                                {"FontGradientMode", "FontGradientMode"}, _
                                {"FontItalic", "FontItalic"}, _
                                {"FontName", "FontName"}, _
                                {"FontOutlineColor", "FontOutlineColor"}, _
                                {"FontOutlineVisible", "FontOutlineVisible"}, _
                                {"FontOutlineWidth", "FontOutlineWidth"}, _
                                {"FontSize", "FontSize"}, _
                                {"FontStrikeout", "FontStrikeout"}, _
                                {"FontTransparency", "FontTransparency"}, _
                                {"FontUnderline", "FontUnderline"}, _
                                {"FrameBackColor", "FrameBackColor"}, _
                                {"FrameBackColor2", "FrameBackColor2"}, _
                                {"FrameGradientMode", "FrameGradientMode"}, _
                                {"FrameOutlineColor", "FrameOutlineColor"}, _
                                {"FrameOutlineStyle", "FrameOutlineStyle"}, _
                                {"FrameOutlineWidth", "FrameOutlineWidth"}, _
                                {"FramePaddingX", "FramePaddingX"}, _
                                {"FramePaddingY", "FramePaddingY"}, _
                                {"FrameTransparency", "FrameTransparency"}, _
                                {"FrameType", "FrameType"}, _
                                {"FrameVisible", "FrameVisible"}, _
                                {"HaloColor", "HaloColor"}, _
                                {"HaloSize", "HaloSize"}, _
                                {"HaloVisible", "HaloVisible"}, _
                                {"InboxAlignment", "InboxAlignment"}, _
                                {"MaxValue", "MaxValue"}, _
                                {"MinValue", "MinValue"}, _
                                {"Name", "Name"}, _
                                {"OffsetX", "OffsetX"}, _
                                {"OffsetY", "OffsetY"}, _
                                {"Priority", "Priority"}, _
                                {"ShadowColor", "ShadowColor"}, _
                                {"ShadowOffsetX", "ShadowOffsetX"}, _
                                {"ShadowOffsetY", "ShadowOffsetY"}, _
                                {"ShadowVisible", "ShadowVisible"}, _
                                {"Visible", "Visible"}}

            Return names

        ElseIf TypeOf obj Is MapWinGIS.Labels Then

            ' only the properties that aren't covered by label category
            Dim names As String(,) = { _
                                {"MaxVisibleScale", "MaxVisibleScale"}, _
                                {"MinVisibleScale", "MinVisibleScale"}, _
                                {"ScaleLabels", "ScaleLabels"}, _
                                {"CollisionBuffer", "CollisionBuffer"}, _
                                {"ClassificationField", "ClassificationField"}, _
                                {"DynamicVisiblity", "DynamicVisiblity"}, _
                                {"VisibilityExpression", "VisibilityExpression"}, _
                                {"UseGdiPlus", "UseGdiPlus"} _
                                                                                    }
            Return names

        ElseIf TypeOf obj Is MapWinGIS.Image Then

            Dim names(,) As String = { _
                               {"TransparencyColor", "TransparencyColor"}, _
                               {"TransparencyColor2", "TransparencyColor2"}, _
                               {"UseTransparencyColor", "UseTransparencyColor"}, _
                               {"TransparencyPercent", "TransparencyPercent"}, _
                               {"UseHistogram", "UseHistogram"}, _
                               {"AllowHillshade", "AllowHillshade"}, _
                               {"SetToGrey", "SetToGrey"}, _
                               {"BufferSize", "BufferSize"}, _
                               {"ImageColorScheme", "ImageColorScheme"}, _
                               {"UpsamplingMode", "UpsamplingMode"}, _
                               {"DownsamplingMode", "DownsamplingMode"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.GridColorScheme Then

            Dim names(,) As String = { _
                                {"Legend", "Legend"}, _
                                {"Key", "Key"}, _
                                {"NoDataColor", "NoDataColor"}, _
                                {"AmbientIntensity", "AmbientIntensity"}, _
                                {"LightSourceAzimuth", "LightSourceAzimuth"}, _
                                {"LightSourceElevation", "LightSourceElevation"}, _
                                {"LightSourceIntensity", "LightSourceIntensity"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.GridColorBreak Then

            Dim names(,) As String = { _
                                {"HighColor", "HighColor"}, _
                                {"HighValue", "HighValue"}, _
                                {"LowColor", "LowColor"}, _
                                {"LowValue", "LowValue"}, _
                                {"GradientModel", "GradientModel"}, _
                                {"ColoringType", "ColoringType"}, _
                                {"Caption", "Caption"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.Charts Then
            Dim names(,) As String = { _
                                {"AvoidCollisions", "AvoidCollisions"}, _
                                {"BarHeight", "BarHeight"}, _
                                {"BarWidth", "BarWidth"}, _
                                {"ChartType", "ChartType"}, _
                                {"LineColor", "LineColor"}, _
                                {"NormalizationField", "NormalizationField"}, _
                                {"PieRadius", "PieRadius"}, _
                                {"PieRadius2", "PieRadius2"}, _
                                {"PieRotation", "PieRotation"}, _
                                {"SizeField", "SizeField"}, _
                                {"Thickness", "Thickness"}, _
                                {"Tilt", "Tilt"}, _
                                {"Transparency", "Transparency"}, _
                                {"Use3DMode", "Use3DMode"}, _
                                {"UseVariableRadius", "UseVariableRadius"}, _
                                {"VerticalPosition", "VerticalPosition"}, _
                                {"Visible", "Visible"}, _
                                {"ValuesFontBold", "ValuesFontBold"}, _
                                {"ValuesFontColor", "ValuesFontColor"}, _
                                {"ValuesFontItalic", "ValuesFontItalic"}, _
                                {"ValuesFontName", "ValuesFontName"}, _
                                {"ValuesFontSize", "ValuesFontSize"}, _
                                {"ValuesFrameColor", "ValuesFrameColor"}, _
                                {"ValuesFrameVisible", "ValuesFrameVisible"}, _
                                {"ValuesStyle", "ValuesStyle"}, _
                                {"ValuesVisible", "ValuesVisible"}, _
                                {"VisibilityExpression", "VisibilityExpression"}, _
                                {"OffsetX", "OffsetX"}, _
                                {"OffsetY", "OffsetY"}, _
                                {"CollisionBuffer", "CollisionBuffer"}, _
                                {"DynamicVisiblity", "DynamicVisiblity"}, _
                                {"MaxVisibleScale", "MaxVisibleScale"}, _
                                {"MinVisibleScale", "MinVisibleScale"}, _
                                {"Caption", "Caption"} _
                                }
            Return names

        ElseIf TypeOf obj Is MapWinGIS.ChartField Then

            Dim names(,) As String = { _
                                {"Color", "Color"}, _
                                {"Index", "Index"}, _
                                {"Name", "Name"}}
            Return names

        ElseIf TypeOf obj Is MapWinGIS.Chart Then

            Dim names(,) As String = { _
                                {"PositionX", "PositionX"}, _
                                {"PositionY", "PositionY"}, _
                                {"Visible", "Visible"} _
                                                        }
            Return names

        ElseIf TypeOf obj Is MapWinGIS.Shapefile Then

            Dim names(,) As String = { _
                                     {"VisibilityExpression", "VisibilityExpression"}, _
                                     {"FastMode", "FastMode"}, _
                                     {"UseQTree", "UseQTree"}, _
                                     {"CollisionMode", "CollisionMode"}, _
                                     {"SelectionAppearance", "SelectionAppearance"}, _
                                     {"SelectionColor", "SelectionColor"}, _
                                     {"SelectionTransparency", "SelectionTransparency"}, _
                                     {"MinDrawingSize", "MinDrawingSize"} _
                                                                        }
            Return names

        ElseIf TypeOf obj Is MapWinGIS.LineSegment Then

            Dim names(,) As String = { _
                                     {"Color", "Color"}, _
                                     {"LineStyle", "LineStyle"}, _
                                     {"LineType", "LineType"}, _
                                     {"LineWidth", "LineWidth"}, _
                                     {"Marker", "Marker"}, _
                                     {"MarkerFlipFirst", "MarkerFlipFirst"}, _
                                     {"MarkerInterval", "MarkerInterval"}, _
                                     {"MarkerOffset", "MarkerOffset"}, _
                                     {"MarkerOrientation", "MarkerOrientation"}, _
                                     {"MarkerOutlineColor", "MarkerOutlineColor"}, _
                                     {"MarkerSize", "MarkerSize"} _
                                                        }
            Return names

        Else
            Return Nothing
        End If

    End Function

#End Region

#Region "Utilities"
    ''' <summary>
    '''  Saves string representation of image in the given node
    ''' </summary>
    ''' <param name="m_Doc"></param>
    ''' <param name="img"></param>
    ''' <remarks></remarks>
    Private Function ImageData2XML(ByRef m_Doc As Xml.XmlDocument, ByVal img As MapWinGIS.Image, ByVal ElementName As String) As XmlElement

        If img Is Nothing Then Return Nothing
        Dim image As XmlElement = m_Doc.CreateElement(ElementName)
        Dim typ As String = ""
        image.InnerText = XmlProjectFile.ConvertImageToString(img, typ)

        'Dim type As XmlAttribute = m_Doc.CreateAttribute("Type")
        'type.InnerText = typ
        'image.Attributes.Append(type)

        Return image
    End Function

    ''' <summary>
    ''' Reads text represetation of image from xml
    ''' </summary>
    ''' <remarks></remarks>
    Private Function XML2Image(ByVal xel As XmlElement) As MapWinGIS.Image
        Dim img As MapWinGIS.Image = XmlProjectFile.ConvertStringToImage(xel.InnerText, "MapWinGIS.Image")
        Return img
    End Function
#End Region

#Region "Grid Properties"

#Region "Grid Loading. Version 1.0"

    Public Function LoadGridFileColoringScheme(ByVal legend As XmlElement) As MapWinGIS.GridColorScheme
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
                    break.HighValue = MapWinUtility.MiscUtils.ParseDouble(.Attributes("HighValue").InnerText, 0.0)  'CDbl(.Attributes("HighValue").InnerText)
                    break.LowValue = MapWinUtility.MiscUtils.ParseDouble(.Attributes("LowValue").InnerText, 0.0)   'CDbl(.Attributes("LowValue").InnerText)
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
#End Region

#End Region

End Class
