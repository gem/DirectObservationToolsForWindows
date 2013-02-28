'********************************************************************************************************
' Filename:  clsXMLProjectNew.vb
' Description: implements loading and saving of the project file in new format
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
'21-may-2011 - sleschinski - copied the code form the clsXmlProjectFile
Imports System.Xml

' The saving in the new format is carried out by Map.MapState property
' For loading the following methods are used: Map.DeserializeMapState(string State, bool LoadLayers),
' Map.LoadLayerOptions(long LayerHandle);
Partial Friend Class XmlProjectFile

#Region "Save project"
    ''' <summary>
    ''' New version of the procedure for saving project file based on ocx methods
    ''' </summary>
    Private Function SaveProjectNew() As Boolean
        Dim Root As XmlElement
        Dim Ver As String

        If ProjectFileName.Length = 0 Then
            Return False
        End If

        Try
            If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                Throw New Exception("Invalid saving procedure", Nothing)
                Return False
            End If

            p_Doc = New XmlDocument

            ' <MapWin>
            Ver = App.VersionString()
            Dim prjName As String = frmMain.Text.Replace("'", "")
            Dim type As String = "projectfile.2"
            p_Doc.LoadXml("<Mapwin name='" + System.Web.HttpUtility.UrlEncode(prjName) + "' type='" & type & "' version='" + System.Web.HttpUtility.UrlEncode(Ver) + "'></Mapwin>")
            Root = p_Doc.DocumentElement

            ' <OCX>
            Dim state As String = frmMain.MapMain.SerializeMapState(True, ProjectFileName)
            Dim nodeOCX As XmlNode = p_Doc.CreateElement("OCX")
            nodeOCX.InnerXml = state
            If (nodeOCX.ChildNodes.Count > 0) Then
                Root.AppendChild(nodeOCX.ChildNodes(0))
            End If

            ' <MapWindow 4>
            Dim nodeMW4 As XmlNode = SaveMW4Properties()
            If Not nodeMW4 Is Nothing Then Root.AppendChild(nodeMW4)

            'Add this project to the list of recent projects
            AddToRecentProjects(ProjectFileName)

            'Save the project file.
            MapWinUtility.Logger.Dbg("Saving Project: " + ProjectFileName)
            Try
                p_Doc.Save(ProjectFileName)
                frmMain.SetModified(False)
                Return True
            Catch e As System.UnauthorizedAccessException
                Dim ro As Boolean = False
                If System.IO.File.Exists(ProjectFileName) Then
                    Dim fi As New System.IO.FileInfo(ProjectFileName)
                    If fi.IsReadOnly Then ro = True
                End If
                If ro Then
                    Dim msg As String = String.Format(frmMain.resources.GetString("msgProjectReadOnly.Text"), ProjectFileName)
                    MapWinUtility.Logger.Msg(msg, MsgBoxStyle.Exclamation, resources.GetString("msgProjectReadOnly.Title"))
                Else
                    Dim msg As String = String.Format(resources.GetString("msgProjectInsufficientAccess.Text"), ProjectFileName)
                    MapWinUtility.Logger.Msg(msg, MsgBoxStyle.Exclamation, resources.GetString("msgProjectInsufficientAccess.Title"))
                End If
                Return False
            End Try
        Catch ex As System.Exception
            ShowError(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Saves MW4 properties
    ''' </summary>
    Private Function SaveMW4Properties() As XmlNode

        Dim nodeMW4 = p_Doc.CreateElement("MapWindow4")

        Dim xelLayer As XmlElement = p_Doc.CreateElement("Layer")

        Dim names As String() = {"ConfigurationPath", _
                                 "ProjectProjection", _
                                 "MapUnits", _
                                 "StatusBarAlternateCoordsNumDecimals", _
                                 "StatusBarCoordsNumDecimals", _
                                 "StatusBarAlternateCoordsUseCommas", _
                                 "StatusBarCoordsUseCommas", _
                                 "ShowFloatingScaleBar", _
                                 "FloatingScaleBarPosition", _
                                 "FloatingScaleBarUnit", _
                                 "FloatingScaleBarForecolor", _
                                 "FloatingScaleBarBackcolor", _
                                 "MapResizeBehavior", _
                                 "ShowStatusBarCoords_Projected", _
                                 "ShowStatusBarCoords_Alternate", _
                                 "SaveShapeSettings", _
                                 "ViewBackColor_UseDefault", _
                                 "ViewBackColor", _
                                 "ProjectProjectionWKT"}

        Dim attributes(names.Length - 1) As XmlAttribute
        For i As Integer = 0 To attributes.Length - 1
            attributes(i) = p_Doc.CreateAttribute(names(i))
        Next i

        'set the properties of the elements
        attributes(0).InnerText = GetRelativePath(ConfigFileName, ProjectFileName)
        attributes(1).InnerText = ProjectProjection

        attributes(2).InnerText = modMain.frmMain.Project.MapUnits
        attributes(3).InnerText = StatusBarAlternateCoordsNumDecimals.ToString()
        attributes(4).InnerText = StatusBarCoordsNumDecimals.ToString()
        attributes(5).InnerText = StatusBarAlternateCoordsUseCommas.ToString()
        attributes(6).InnerText = StatusBarCoordsUseCommas.ToString()
        attributes(7).InnerText = frmMain.m_FloatingScalebar_Enabled.ToString()
        attributes(8).InnerText = frmMain.m_FloatingScalebar_ContextMenu_SelectedPosition
        attributes(9).InnerText = frmMain.m_FloatingScalebar_ContextMenu_SelectedUnit
        attributes(10).InnerText = frmMain.m_FloatingScalebar_ContextMenu_ForeColor.ToArgb().ToString()
        attributes(11).InnerText = frmMain.m_FloatingScalebar_ContextMenu_BackColor.ToArgb().ToString()
        attributes(12).InnerText = CType(modMain.frmMain.MapMain.MapResizeBehavior, Short).ToString()
        attributes(13).InnerText = ShowStatusBarCoords_Projected.ToString()
        attributes(14).InnerText = ShowStatusBarCoords_Alternate
        attributes(15).InnerText = Me.SaveShapeSettings.ToString()
        attributes(16).InnerText = UseDefaultBackColor.ToString
        attributes(17).InnerText = (MapWinUtility.Colors.ColorToInteger(ProjectBackColor)).ToString
        attributes(18).InnerText = ProjectProjectionWKT

        For i As Integer = 0 To attributes.Length - 1
            nodeMW4.Attributes.Append(attributes(i))
        Next i

        'Add the list of the plugins to the project file
        AddPluginsElement(p_Doc, nodeMW4, False)

        'Add the application plugins
        AddApplicationPluginsElement(p_Doc, nodeMW4, False)

        'Add view bookmarks
        AddBookmarks(p_Doc, nodeMW4)

        'Add the properies fo the preview Map to the project file
        AddPreViewMapElement(p_Doc, nodeMW4)

        ' groups a and additional layer properties
        AddGroups(p_Doc, nodeMW4)

        ' legend-related layer properties
        AddLayersNew(p_Doc, nodeMW4)

        Return nodeMW4
    End Function

    ''' <summary>
    ''' Saves information legend specific information about layer (group handle, expanded state, etc)
    ''' </summary>
    Private Sub AddLayersNew(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        Dim nodeLayers As XmlElement = m_Doc.CreateElement("Layers")

        Dim count As Integer = frmMain.Legend.Layers.Count
        For i As Integer = 0 To count - 1
            Dim lyr As MapWindow.Interfaces.Layer = frmMain.Layers(frmMain.Layers.GetHandle(i))
            If Not lyr Is Nothing Then
                If lyr.SkipOverDuringSave Then Continue For

                Dim nodeLayer As XmlElement = m_Doc.CreateElement("Layer")
                Dim names As String() = {"Name", "Tag", "Expanded", "Handle", "PositionInGroup", "GroupIndex", "GroupName"}
                Dim attributes(names.Length - 1) As XmlAttribute

                For j As Integer = 0 To names.Length - 1
                    attributes(j) = m_Doc.CreateAttribute(names(j))
                Next j

                attributes(0).InnerText = lyr.Name                              ' layer name
                attributes(1).InnerText = lyr.Tag()                             ' tag
                attributes(2).InnerText = lyr.Expanded.ToString                 ' expanded
                attributes(3).InnerText = lyr.Handle                            ' handle
                attributes(4).InnerText = lyr.GroupPosition                     ' position of layer within group

                Dim position As Integer = frmMain.Legend.Groups.PositionOf(lyr.GroupHandle)
                attributes(5).InnerText = position                              ' global position of group

                Dim group As LegendControl.Group = frmMain.Legend.Groups(position)
                If Not group Is Nothing Then attributes(6).InnerText = group.Text

                For j As Integer = 0 To names.Length - 1
                    nodeLayer.Attributes.Append(attributes(j))
                Next j

                ' serializing custom objects
                If TypeOf lyr Is Layer Then
                    Dim legendLayer As LegendControl.Layer = frmMain.Legend.Layers.ItemByHandle(lyr.Handle)
                    If Not legendLayer Is Nothing Then
                        Dim hash As Hashtable = legendLayer.m_CustomObjects
                        If Not hash Is Nothing Then
                            If hash.Count > 0 Then

                                Dim nodeObjects As XmlElement = m_Doc.CreateElement("CustomObjects")

                                Dim keys As System.Collections.IEnumerator = hash.Keys.GetEnumerator()
                                Dim values As System.Collections.IEnumerator = hash.Values.GetEnumerator()

                                Do While (values.MoveNext() And keys.MoveNext())
                                    If Not values.Current Is Nothing _
                                        AndAlso Not keys.Current Is Nothing _
                                        AndAlso values.Current.GetType().IsSerializable Then

                                        Dim el As XmlElement = m_Doc.CreateElement("Object")
                                        Dim attr As XmlAttribute = m_Doc.CreateAttribute("Key")
                                        attr.InnerText = keys.Current
                                        el.Attributes.Append(attr)
                                        nodeObjects.AppendChild(el)

                                        MapWinUtility.Serialization.Serialize(values.Current, el)
                                    End If
                                Loop

                                nodeLayer.AppendChild(nodeObjects)
                            End If
                        End If
                    End If
                End If

                nodeLayers.AppendChild(nodeLayer)
            End If
        Next i
        Parent.AppendChild(nodeLayers)
    End Sub

    ''' <summary>
    ''' Adds information about ocx state and layers, the structure of tree *MUST BE*
    ''' the same as in Map.SaveMapState method to ensure compatibility
    ''' </summary>
    Private Sub AddGroups(ByRef m_Doc As Xml.XmlDocument, ByVal Parent As XmlElement)
        'Add info about the current layers to the XML project file
        Dim Groups As XmlElement = m_Doc.CreateElement("Groups")
        'Dim Layers As XmlElement
        Dim Group As XmlElement

        Dim Name As XmlAttribute
        Dim Expanded As XmlAttribute
        Dim Position As XmlAttribute
        Dim LayerPos As XmlAttribute = m_Doc.CreateAttribute("Position")
        Dim NumGroups As Integer ', numLayers As Integer
        'Dim LHandle As Integer
        Dim g As Integer

        'saving groups
        NumGroups = frmMain.Legend.Groups.Count
        For g = 0 To NumGroups - 1
            Group = m_Doc.CreateElement("Group")
            Name = m_Doc.CreateAttribute("Name")
            Expanded = m_Doc.CreateAttribute("Expanded")
            Position = m_Doc.CreateAttribute("Position")

            'Add the properties of the element
            Name.InnerText = frmMain.Legend.Groups(g).Text
            Expanded.InnerText = frmMain.Legend.Groups(g).Expanded.ToString
            Position.InnerText = g.ToString
            Group.Attributes.Append(Name)
            Group.Attributes.Append(Expanded)
            Group.Attributes.Append(Position)
            SaveImage(m_Doc, frmMain.Legend.Groups(g).Icon, Group)
            Groups.AppendChild(Group)
        Next
        Parent.AppendChild(Groups)
    End Sub
#End Region

#Region "Loading"

    ''' <summary>
    ''' Loads the layers based on the map state
    ''' </summary>
    ''' <param name="nodeOcx">MapWinGIS node of the project file</param>
    ''' <param name="nodeMW4">MapWindow4 node of the project file</param>
    ''' <param name="ExistingLayerHandle"></param>
    ''' <param name="PluginCall"></param>
    ''' <remarks></remarks>
    Friend Sub LoadLayers(ByRef nodeOcx As XmlNode, ByRef nodeMW4 As XmlNode, Optional ByVal ExistingLayerHandle As Integer = -1, Optional ByVal PluginCall As Boolean = False)
        If nodeOcx Is Nothing Then Return

        Dim nodeLayers As XmlNode = nodeOcx.Item("Layers")
        If nodeLayers Is Nothing Then Return

        Dim nodeLegend As XmlNode = Nothing
        If Not nodeMW4 Is Nothing Then
            nodeLegend = nodeMW4.Item("Layers")
            ' the number of layer in meta data should be the same, otherwise the file is corrupted and legend information will be skipped
            If Not nodeLegend Is Nothing AndAlso nodeLayers.ChildNodes.Count <> nodeLegend.ChildNodes.Count Then
                nodeLegend = Nothing
            End If
        End If

        Dim filepath, name As String
        Dim nodeLayer As XmlElement
        Dim nodeMeta As XmlElement

        'frmMain.m_LoadingProject = True

        Dim layerCount As Integer = nodeLayers.ChildNodes.Count - 1
        For i As Integer = 0 To layerCount

            ' reading ocx-based properties
            Dim handle As Integer = -1
            nodeLayer = nodeLayers.ChildNodes(i)
            Dim initLayerName As String = ""

            If nodeLayer.Name.ToLower() = "layer" Then
                If nodeLayer.HasAttribute("Filename") Then
                    filepath = nodeLayer.Attributes("Filename").Value.ToString()
                    If nodeLayer.HasAttribute("LayerName") Then
                        name = nodeLayer.Attributes("LayerName").Value.ToString()
                        If name = "" Then name = System.IO.Path.GetFileName(filepath)
                    Else
                        name = System.IO.Path.GetFileName(filepath)
                    End If
                    initLayerName = name

                    If nodeLayer.HasAttribute("GridFilename") Then
                        ' is is grid?
                        Dim nodeScheme As XmlElement = nodeLayer.Item("GridColorSchemeClass")
                        If Not nodeScheme Is Nothing Then
                            Dim scheme As MapWinGIS.GridColorScheme = New MapWinGIS.GridColorScheme
                            scheme.Deserialize(nodeScheme.OuterXml)

                            Dim gridName As String = nodeLayer.Attributes("GridFilename").Value.ToString()

                            handle = frmMain.m_layers.AddLayer(gridName, name, GrdColorScheme:=scheme, LoadXMLInfo:=False)(0).Handle
                        End If
                    End If

                    If handle = -1 Then
                        ' failed to load as grid; usual loading
                        handle = Me.AddLayerFromPath(filepath, name, ExistingLayerHandle)
                    End If

                    If handle = -1 Then Continue For

                    frmMain.MapMain.DeserializeLayer(handle, nodeLayer.OuterXml)
                End If
            End If

            MapWinUtility.Logger.Progress(i, layerCount)

            ' reading legend based properties
            If Not nodeLegend Is Nothing Then
                nodeMeta = nodeLegend.ChildNodes(i)
            Else
                nodeMeta = Nothing
            End If

            If Not nodeMeta Is Nothing Then
                Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(handle)
                If Not layer Is Nothing Then
                    If nodeMeta.HasAttribute("Name") Then
                        If layer.Name.ToLower() <> nodeMeta.Attributes("Name").Value.ToLower() Then
                            Continue For    ' names must be the same
                        End If
                    Else
                        Continue For      ' names must be the same
                    End If
                End If

                ' moving to the appropriate group
                If nodeMeta.HasAttribute("GroupIndex") And nodeMeta.HasAttribute("PositionInGroup") Then
                    Dim groupIndex As Integer = -1
                    Dim inGroupIndex As Integer = -1
                    Int32.TryParse(nodeMeta.Attributes("GroupIndex").Value, groupIndex)
                    Int32.TryParse(nodeMeta.Attributes("PositionInGroup").Value, inGroupIndex)
                    If groupIndex <> -1 And inGroupIndex <> -1 And handle <> -1 Then
                        Dim group As LegendControl.Group = frmMain.Legend.Groups.Item(groupIndex)
                        If Not group Is Nothing Then
                            frmMain.Legend.Layers.MoveLayer(handle, group.Handle, inGroupIndex)
                        End If
                    End If
                End If

                If nodeMeta.HasAttribute("Expanded") Then
                    Dim val As Boolean = True
                    If Boolean.TryParse(nodeMeta.Attributes("Expanded").Value, val) Then
                        layer.Expanded = val
                    End If
                End If

                If nodeMeta.HasAttribute("Tag") Then
                    layer.Tag = nodeMeta.Attributes("Tag").Value
                End If

                ' loading custom objects
                Dim nodeObjects As XmlElement = nodeMeta.Item("CustomObjects")
                If Not nodeObjects Is Nothing Then
                    For j As Integer = 0 To nodeObjects.ChildNodes.Count - 1
                        Dim node As XmlElement = nodeObjects.ChildNodes(j)
                        If node.Name.ToString().ToLower() = "object" AndAlso node.HasAttribute("Key") AndAlso node.ChildNodes.Count = 1 Then
                            Dim key As String = node.Attributes("Key").Value.ToString()
                            Dim state As String = node.ChildNodes(0).OuterXml

                            Dim l As Layer = CType(layer, Layer)
                            Dim handled As Boolean = False
                            Try
                                frmMain.FireCustomEventLoaded(handle, key, state, handled)
                            Catch
                                MapWinUtility.Logger.Status("Error while loading custom object: " + key)
                            End Try
                            ' passing the state to plugins to deserialize it
                            'state = "CUSTOM_OBJECT:" + handle.ToString() + "!" + key + "=" + state
                            'frmMain.Plugins.BroadcastMessage(state)
                            Exit For
                        End If
                    Next j
                End If
            End If
        Next

        'frmMain.m_LoadingProject = False
    End Sub

    ''' <summary>
    ''' Adds a layer from the path specified in the project file
    ''' </summary>
    Private Function AddLayerFromPath(ByVal filepath As String, ByVal name As String, Optional ByVal ExistingLayerHandle As Integer = -1) As Integer
        Dim handle As Integer = -1
        Try
            '
            ' If Layer is GEM Observations then create a new shapefile from the database
            ' and set layer to point at new shapefile
            '
            If name = "GEM Observations" Then
                memoryShape = frmMain.CreateShapefileAndImportData()
                filepath = memoryshape.Filename
            End If

            'If ExistingLayerHandle = -1 Then
            'the order is specified by project file
            If (System.IO.File.Exists(filepath) = False) Then
                If filepath.ToLower().Trim().StartsWith("ecwp://") Then
                    handle = frmMain.m_layers.AddLayer(filepath)(0).Handle
                Else
                    ' This operation will have changed the current working directory.
                    ' Preserve it and set it back, or all subsequent layers will not be found either.
                    Dim cwd As String = CurDir()

                    ' PromptBrowse will set the file path by reference and return true,
                    ' or return false if the user cancels.
                    If Not PromptToBrowse(filepath, name) Then
                        Me.m_ForcedModified = True
                        Return -1
                    End If

                    'add layer
                    handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filepath), name, LoadXMLInfo:=False)(0).Handle

                    'Restore CWD
                    ChDir(cwd)
                End If
            Else
                'add layer
                handle = frmMain.m_layers.AddLayer(System.IO.Path.GetFullPath(filepath), name, LoadXMLInfo:=False)(0).Handle
            End If
        Catch ex As Exception
            Debug.WriteLine("Error in LoadLayerProperties: " & ex.ToString)
        Finally
        End Try
        Return handle
    End Function

    ''' <summary>
    ''' New procedure for loading groups, groups are loaded before layers are added
    ''' </summary>
    Private Sub LoadGroupsNew(ByVal nodeGroups As XmlElement)

        Dim count As Integer = 0    ' the layer processed

        For Each group As XmlElement In nodeGroups
            'set the group properties and icon
            Dim name As String = group.Attributes("Name").InnerText

            Dim hGroup As Integer = -1
            If name.ToLower() = "data layers" Then
                For i As Integer = 0 To frmMain.Legend.Groups.Count - 1
                    Dim gr As LegendControl.Group = (frmMain.Legend.Groups.ItemByPosition(i))
                    If Not gr Is Nothing Then
                        If gr.Text.ToLower() = "data layers" Then
                            hGroup = gr.Handle
                            Exit For
                        End If
                    End If
                Next i
            End If

            Dim position As Integer = CInt(group.Attributes("Position").InnerText)
            If hGroup < 0 Then
                hGroup = frmMain.Legend.Groups.Add(name, position)
            Else
                frmMain.Legend.Groups.MoveGroup(hGroup, position)
            End If

            frmMain.Legend.Groups.ItemByHandle(hGroup).Expanded = CBool(group.Attributes("Expanded").InnerText)
            If (Len(group.Item("Image").InnerText) > 0) Then
                frmMain.Legend.Groups.ItemByHandle(hGroup).Icon = ConvertStringToImage(group.Item("Image").InnerText, group.Item("Image").Attributes("Type").InnerText)
            End If
        Next
    End Sub
#End Region
End Class
