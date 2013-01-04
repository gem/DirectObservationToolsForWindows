'********************************************************************************************************
'File Name: clsLayers.vb
'Description: Public class on the plugin interface for managing layers.    
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
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'1/31/2005 - No change from the public domain version. 
'4/23/2005 - dpa - modified remove layer and add layer functions to not lock images after removed.  
'6/30/2005 - cdm - Corrected a small problem where LineOrPointSize wasn't being set.
'7/2/2005 - cdm - Added CheckWriteTimes test to AddLayer to optimize grid loading by not rebuilding the grid image every time if unnecessary.
'3/24/2008 - dpa - Modified error associted with corrupt shapefiles to include the problem shapefile name.
' 8/8/2011 Teva - added avoid collision property
'********************************************************************************************************

Option Strict Off

Imports System.Drawing
Imports System.IO
Imports MapWindow.Controls.Projections
Imports System.Collections.Generic
Imports MapWindow.Controls

Public Class Layers
    Implements Interfaces.Layers

    Implements MapWinGIS.ICallback
    Implements IEnumerable

#Region "Declrations"
    Private m_PluginCall As Boolean = False
    Friend m_Grids As New Hashtable()

    ' Projection mismatch tester, used during adding sesion
    Private m_mismatchTester As MismatchTester = Nothing

    ' List of layers added during the adding session
    Private m_newLayers As New List(Of MapWindow.Interfaces.Layer)

    ' Number of layers before starting adding session
    Private m_initCount As Integer
#End Region

#Region "Enumerator"
    ''' <summary>
    ''' Implements strongly-typed enumerator for layers collection (can be used with LINQ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class LayerEnumerator
        Implements System.Collections.Generic.IEnumerator(Of MapWindow.Interfaces.Layer)
        Implements System.Collections.IEnumerator

        Private m_layers As MapWindow.Interfaces.Layers
        Private m_index As Integer = -1

        Public Sub New(ByVal layers As MapWindow.Layers)
            m_layers = layers
            m_index = -1
        End Sub

        Public ReadOnly Property Current() As Interfaces.Layer Implements System.Collections.Generic.IEnumerator(Of Interfaces.Layer).Current
            Get
                Return m_layers.Item(m_layers.GetHandle(m_index))
            End Get
        End Property

        Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return m_layers.Item(m_layers.GetHandle(m_index))
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            m_index += 1
            If m_index >= m_layers.NumLayers Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            m_index = -1
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.

                ' lsu: I suppose there is nothing to release here, it's just a wrapper
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    ''' <summary>
    ''' Returns untyped enuerator
    ''' </summary>
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return New LayerEnumerator(Me)
    End Function

    ''' <summary>
    ''' Returns strongly-typed enumerator
    ''' </summary>
    Public Function GetGenericEnumerator() As Generic.IEnumerator(Of MapWindow.Interfaces.Layer) Implements Generic.IEnumerable(Of MapWindow.Interfaces.Layer).GetEnumerator
        Return New LayerEnumerator(Me)
    End Function
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor. Creates a new instance of Layers class.
    ''' </summary>
    Public Sub New()
        Randomize()
    End Sub
#End Region

#Region "Properties"

    Public Property CurrentLayer() As Integer Implements Interfaces.Layers.CurrentLayer
        Get
            CurrentLayer = frmMain.Legend.SelectedLayer
        End Get
        Set(ByVal Value As Integer)
            frmMain.Legend.SelectedLayer = Value
        End Set
    End Property

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal LayerHandle As Integer) As Interfaces.Layer Implements Interfaces.Layers.Item
        Get
            If frmMain.MapMain.NumLayers = 0 Then
                g_error = "No layers to return!"
                Return Nothing
            End If

            If Not IsValidHandle(LayerHandle) Then
                g_error = "Invalid layer handle " + LayerHandle.ToString() + " requested. If cycling from 0 to NumLayers, route through '.GetHandle'"
                Return Nothing
            End If

            Dim newLyr As New MapWindow.Layer
            newLyr.Handle = LayerHandle
            Return newLyr
        End Get
    End Property

    Public ReadOnly Property NumLayers() As Integer Implements Interfaces.Layers.NumLayers
        Get
            NumLayers = frmMain.MapMain.NumLayers
        End Get
    End Property

    <CLSCompliant(False)> _
    ReadOnly Property Groups() As LegendControl.Groups Implements MapWindow.Interfaces.Layers.Groups
        Get
            Return frmMain.Legend.Groups
        End Get
    End Property

    Public Function GetHandle(ByVal Position As Integer) As Integer Implements MapWindow.Interfaces.Layers.GetHandle
        Try
            If Position < NumLayers() And Position >= 0 Then
                Return frmMain.MapMain.get_LayerHandle(Position)
            End If

            Return -1
        Catch ex As System.Exception
            g_error = ex.Message
            ShowError(ex)
            Return -1
        End Try
    End Function

    Public Function IsValidHandle(ByVal LayerHandle As Integer) As Boolean Implements MapWindow.Interfaces.Layers.IsValidHandle
        Return frmMain.Legend.Layers.IsValidHandle(LayerHandle)
    End Function
#End Region

#Region "Methods"
    Public Sub Clear() Implements Interfaces.Layers.Clear
        Try
            Dim i As Integer
            For i = 0 To frmMain.MapMain.NumLayers - 1
                With Item(Me.GetHandle(i))
                    Select Case .LayerType
                        Case Interfaces.eLayerType.Grid
                            Try
                                If Not .GetGridObject Is Nothing Then
                                    CType(.GetGridObject, MapWinGIS.Grid).Close()
                                End If
                            Catch
                            End Try

                            'Chris Michaelis Aug 10 2006 -- Also close the image behind
                            'the grid, to avoid locking it until app ends
                            Dim o As MapWinGIS.Image
                            o = CType(frmMain.MapMain.get_GetObject(Me.GetHandle(i)), MapWinGIS.Image)
                            If Not o Is Nothing Then o.Close()
                            o = Nothing
                        Case Interfaces.eLayerType.Image
                            CType(.GetObject, MapWinGIS.Image).Close()
                        Case Interfaces.eLayerType.Invalid
                            Exit Select
                        Case Else
                            CType(.GetObject, MapWinGIS.Shapefile).Close()
                    End Select
                End With
            Next
            frmMain.Legend.Layers.Clear()
            frmMain.Legend.Groups.Clear()
            m_Grids.Clear()
            If Not frmMain.m_PluginManager Is Nothing Then
                frmMain.m_PluginManager.LayersCleared()
            End If
            frmMain.m_AutoVis.Clear()

            'No layers - no need to keep the project projection.
            modMain.ProjInfo.ProjectProjection = ""

        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            ' if anything happens I must be shutting down and ptrs are no longer valid
        End Try
    End Sub

    Public Sub MoveLayer(ByVal Handle As Integer, ByVal NewPosition As Integer, ByVal TargetGroup As Integer) Implements Interfaces.Layers.MoveLayer

        If TargetGroup = -1 Then
            frmMain.Legend.Layers.MoveLayerWithinGroup(Handle, NewPosition)
        Else
            frmMain.Legend.Layers.MoveLayer(Handle, TargetGroup, NewPosition)
        End If

    End Sub

    Public Sub Remove(ByVal LayerHandle As Integer) Implements Interfaces.Layers.Remove
        'Removes a layer from the map
        'dpa 4/25/2005 modified to close the associated object.
        If LayerHandle >= 0 AndAlso frmMain.MapMain.get_LayerPosition(LayerHandle) >= 0 Then
            'Close the object associated with the layer we are removing.
            With Item(LayerHandle)
                Select Case .LayerType
                    Case Interfaces.eLayerType.Grid
                        Try
                            CType(.GetGridObject, MapWinGIS.Grid).Close()
                        Catch
                        End Try

                        'Chris Michaelis Aug 10 2006 -- Also close the image behind
                        'the grid, to avoid locking it until app ends
                        Dim o As MapWinGIS.Image
                        o = CType(frmMain.MapMain.get_GetObject(LayerHandle), MapWinGIS.Image)
                        If Not o Is Nothing Then o.Close()
                        o = Nothing
                    Case Interfaces.eLayerType.Image
                        CType(.GetObject, MapWinGIS.Image).Close()
                    Case Interfaces.eLayerType.Invalid
                        Exit Select
                    Case Else
                        CType(.GetObject, MapWinGIS.Shapefile).Close()
                End Select
            End With
            frmMain.Legend.Layers.Remove(LayerHandle)
            frmMain.m_PluginManager.LayerRemoved(LayerHandle)
            frmMain.UpdateButtons()
            If Not frmMain.m_AutoVis(LayerHandle) Is Nothing Then
                frmMain.m_AutoVis.Remove(LayerHandle)
            End If
        End If
    End Sub
#End Region

#Region "Layers.Add Overloads"
    <CLSCompliant(False)> _
    Public Function Add(ByRef ShapefileObject As MapWinGIS.Shapefile, ByVal LayerName As String, ByVal Color As Integer, ByVal OutlineColor As Integer, ByVal LineOrPointSize As Integer) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        Return AddLayer(ShapefileObject, LayerName, , , Color, OutlineColor, , LineOrPointSize)(0)
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef ShapefileObject As MapWinGIS.Shapefile, ByVal LayerName As String, ByVal Color As Integer, ByVal OutlineColor As Integer) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        Return AddLayer(ShapefileObject, LayerName, , , Color, OutlineColor)(0)
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByVal Filename As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Filename, "", -1, GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByVal Filename As String, ByVal Layername As String, ByVal Visible As Boolean, ByVal PlaceAboveSelected As Boolean) As MapWindow.Interfaces.Layer Implements Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Filename, Layername, , Visible, , , , , , , , PlaceAboveSelected)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByVal Filename As String, ByVal LayerName As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Filename, LayerName, -1, GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByVal Filename As String, ByVal LayerName As String, ByVal LegendVisible As Boolean) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Filename, LayerName, -1, GetDefaultLayerVis, -1, -1, True, 1.0, 0, Nothing, LegendVisible)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef ImageObject As MapWinGIS.Image) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(ImageObject, , , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef Image As MapWinGIS.Image, ByVal LayerName As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Image, LayerName, , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function
    ' Start Paul Meems May 28 2010
    <CLSCompliant(False)> _
    Public Function Add(ByRef Image As MapWinGIS.Image, ByVal LayerName As String, ByVal Visible As Boolean, ByVal TargetGroup As Integer, ByVal LayerPosition As Integer) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(ObjectOrFilename:=Image, LayerName:=LayerName, Group:=TargetGroup, LayerVisible:=Visible, LayerPosition:=LayerPosition)(0)
        m_PluginCall = False
        Return b
    End Function
    ' End Paul Meems May 28 2010

    <CLSCompliant(False)> _
    Public Function Add(ByRef Shapefile As MapWinGIS.Shapefile) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Shapefile, , , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef Shapefile As MapWinGIS.Shapefile, ByVal LayerName As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(Shapefile, LayerName, , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef GridObject As MapWinGIS.Grid) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(GridObject, , , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef GridObject As MapWinGIS.Grid, ByVal LayerName As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(GridObject, LayerName, , GetDefaultLayerVis)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef GridObject As MapWinGIS.Grid, ByVal ColorScheme As MapWinGIS.GridColorScheme) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(GridObject, , , GetDefaultLayerVis, , , , , , ColorScheme)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add(ByRef GridObject As MapWinGIS.Grid, ByVal ColorScheme As MapWinGIS.GridColorScheme, ByVal LayerName As String) As MapWindow.Interfaces.Layer Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim b As MapWindow.Interfaces.Layer = AddLayer(GridObject, LayerName, , GetDefaultLayerVis, , , , , , ColorScheme)(0)
        m_PluginCall = False
        Return b
    End Function

    <CLSCompliant(False)> _
    Public Function Add() As MapWindow.Interfaces.Layer() Implements MapWindow.Interfaces.Layers.Add
        m_PluginCall = True
        Dim retval As MapWindow.Interfaces.Layer()
        Dim b As Object = AddLayer(, , , GetDefaultLayerVis)
        If TypeOf (b) Is MapWindow.Interfaces.Layer() Then
            'Great
            retval = b
        Else
            Dim newret(0) As MapWindow.Interfaces.Layer
            newret(0) = CType(b, MapWindow.Interfaces.Layer)
            retval = newret
        End If
        m_PluginCall = False
        Return retval
    End Function
#End Region

#Region "Adding session"
    ''' <summary>
    ''' Start adding session, locks map and legend, intializes projection mismatch report
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartAddingSession() Implements Interfaces.Layers.StartAddingSession
        m_mismatchTester = New MismatchTester(modMain.frmMain)
        Cursor.Current = Cursors.WaitCursor
        Cursor.Show()
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
        frmMain.Legend.Lock()

        m_initCount = frmMain.Layers.NumLayers

        ' make sure there are no old extent history items left in the mapwindow
        If frmMain.MapMain.NumLayers = 0 Then
            frmMain.m_Extents.Clear()
            frmMain.m_CurrentExtent = -1
        End If
    End Sub

    Public Sub StopAddingSession() Implements Interfaces.Layers.StopAddingSession
        Me.StopAddingSession(m_initCount = 0 And frmMain.Layers.NumLayers > 0)
    End Sub

    ''' <summary>
    ''' Stop adding session, unlocks map and legend, shows projection mismatch report
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopAddingSession(ByVal zoomToExtents As Boolean) Implements Interfaces.Layers.StopAddingSession

        If zoomToExtents Then frmMain.MapMain.ZoomToMaxExtents()

        If (m_newLayers.Count > 0) Then frmMain.m_PluginManager.LayersAdded(m_newLayers.ToArray())

        MapWinUtility.Logger.Progress(100, 100)
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        frmMain.Legend.Unlock()
        Cursor.Current = Cursors.Default
        Cursor.Show()
        frmMain.UpdateButtons()

        If m_mismatchTester.FileCount > 0 And modMain.frmMain.ApplicationInfo.ShowLoadingReport Then
            m_mismatchTester.ShowReport(modMain.frmMain.Project.GeoProjection)
        Else
            m_mismatchTester.HideProgress()
        End If

        ' clearing
        m_initCount = 0
        m_newLayers.Clear()
        m_mismatchTester = Nothing
    End Sub
#End Region

#Region "Add layer"
    ''' <summary>
    ''' The central routine for adding layers.
    ''' </summary>
    Friend Function AddLayer(Optional ByVal ObjectOrFilename As Object = "", _
                             Optional ByVal LayerName As String = "", _
                             Optional ByVal Group As Integer = -1, _
                             Optional ByVal LayerVisible As Boolean = True, _
                             Optional ByVal Color As Integer = -1, _
                             Optional ByVal OutlineColor As Integer = -1, _
                             Optional ByVal DrawFill As Boolean = True, _
                             Optional ByVal LineOrPointSize As Single = 1, _
                             Optional ByVal PointType As MapWinGIS.tkPointType = 0, _
                             Optional ByVal GrdColorScheme As MapWinGIS.GridColorScheme = Nothing, _
                             Optional ByVal LegendVisible As Boolean = True, _
                             Optional ByVal PositionFromSelected As Boolean = False, _
                             Optional ByVal LayerPosition As Integer = -1, _
                             Optional ByVal LoadXMLInfo As Boolean = True) As Interfaces.Layer()

        Dim layers() As MapWindow.Interfaces.Layer

        ' an empty filename was passed, open dialog to choose files
        If TypeName(ObjectOrFilename) = "String" AndAlso ObjectOrFilename.ToString = "" Then

            Dim filenames As String() = ShowLayerDialog()
            If filenames Is Nothing Then Return Nothing
            layers = Me.AddLayerFromFilename(filenames, LayerName, Group, LayerVisible, GrdColorScheme, LegendVisible, PositionFromSelected, LayerPosition, LoadXMLInfo)

        Else
            ' a single filename
            If TypeName(ObjectOrFilename) = "String" Then

                Dim filenames() As String = {ObjectOrFilename}
                layers = Me.AddLayerFromFilename(filenames, LayerName, Group, LayerVisible, GrdColorScheme, LegendVisible, PositionFromSelected, LayerPosition, LoadXMLInfo)
            Else

                Dim addingSession As Boolean = Not m_mismatchTester Is Nothing
                Try
                    If Not addingSession Then Me.StartAddingSession()

                    Dim source As New MapWindow.Controls.LayerSource(ObjectOrFilename)
                    If source.Type = LayerSourceType.Undefined Then
                        MapWinUtility.Logger.Msg("File doesn't exists: " & ObjectOrFilename, "MapWin.Layers.AddLayer")
                        Return Nothing
                    End If

                    ' projection testing; switched off for tiles
                    Dim tileLayer As Boolean = TypeOf ObjectOrFilename Is MapWinGIS.Image AndAlso LayerName.StartsWith("mwTile-")

                    If Not tileLayer Then
                        Dim sourceTemp As LayerSource = Nothing
                        If m_mismatchTester.TestLayer(source, sourceTemp) = TestingResult.Substituted Then source = sourceTemp
                    End If

                    Dim layer As MapWindow.Interfaces.Layer = Me.AddLayerCore(source.GetObject, LayerName, Group, LayerVisible, GrdColorScheme, _
                                                                              LegendVisible, PositionFromSelected, LayerPosition, LoadXMLInfo)
                    If Not layer Is Nothing Then m_newLayers.Add(layer)

                    layers = Array.CreateInstance(GetType(MapWindow.Interfaces.Layer), 1)
                    layers(0) = layer                   'PM Possible fix for mapWin.Layers.Add() returning nothing
                Finally
                    ' session must be closed only in case it was opened in this function
                    ' otherwise it's a responsibility of caller
                    If Not addingSession Then Me.StopAddingSession()
                End Try
            End If
        End If

        ' 08 sep 2011 lsu - the property should be applied to the newly added layers only I suppose
        For i As Integer = 0 To layers.Length - 1
            Dim handle As Integer = layers(i).Handle
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(handle)
            If Not layer Is Nothing Then layer.UseLabelCollision = AppInfo.ShowAvoidCollision
        Next

        ' setting the properties; makes sense to do for a single layer only
        If Not layers Is Nothing AndAlso layers.Length = 1 Then
            Me.SetLayerProperties(layers(0), Color, OutlineColor, DrawFill, LineOrPointSize, PointType)
        End If

        Return layers
    End Function

    ''' <summary>
    ''' Adds arrays of filenames to the map. Uses adding session for the files
    ''' </summary>
    Private Function AddLayerFromFilename(ByVal filenames() As String, _
                             Optional ByVal LayerName As String = "", _
                             Optional ByVal Group As Integer = -1, _
                             Optional ByVal LayerVisible As Boolean = True, _
                             Optional ByVal GrdColorScheme As MapWinGIS.GridColorScheme = Nothing, _
                             Optional ByVal LegendVisible As Boolean = True, _
                             Optional ByVal PositionFromSelected As Boolean = False, _
                             Optional ByVal LayerPosition As Integer = -1, _
                             Optional ByVal LoadXMLInfo As Boolean = True) As Interfaces.Layer()

        If filenames Is Nothing Then Return Nothing
        If filenames.Length = 0 Then Return Nothing
        Dim addingSession As Boolean = Not m_mismatchTester Is Nothing

        Dim layers As New List(Of MapWindow.Interfaces.Layer)

        Try
            If Not addingSession Then Me.StartAddingSession()
            For i As Integer = 0 To filenames.Length - 1

                ' projection testing
                Dim newName As String = ""
                Dim result As TestingResult = m_mismatchTester.TestLayer(filenames(i), newName)
                If result <> TestingResult.Substituted Then newName = filenames(i)

                Select Case result
                    Case TestingResult.Ok, TestingResult.Substituted

                        Dim layer As MapWindow.Interfaces.Layer = AddLayerCore(newName, LayerName, _
                             Group, LayerVisible, GrdColorScheme, LegendVisible, PositionFromSelected, LayerPosition, LoadXMLInfo, True)

                        If Not layer Is Nothing Then
                            m_newLayers.Add(layer)  ' for plug-in notification (the whole group)
                            layers.Add(layer)       ' for immediate caller
                        End If

                        If filenames.Length > 1 Then MapWinUtility.Logger.Progress(i, filenames.Length)

                    Case TestingResult.CancelOperation
                        Exit For
                    Case Else
                        Continue For
                End Select
            Next i
        Finally
            ' session must be closed only in case it was opened in this function
            ' otherwise it's a responsibility of caller
            If Not addingSession Then Me.StopAddingSession()
        End Try

        Return layers.ToArray()
    End Function

    ''' <summary>
    '''  Core routine for adding a single layer to the map. A filename or an object (shapefile, image or grid) can be passed to it.
    ''' </summary>
    Private Function AddLayerCore(Optional ByVal ObjectOrFilename As Object = "", _
                             Optional ByVal LayerName As String = "", _
                             Optional ByVal Group As Integer = -1, _
                             Optional ByVal LayerVisible As Boolean = True, _
                             Optional ByVal GrdColorScheme As MapWinGIS.GridColorScheme = Nothing, _
                             Optional ByVal LegendVisible As Boolean = True, _
                             Optional ByVal PositionFromSelected As Boolean = False, _
                             Optional ByVal LayerPosition As Integer = -1, _
                             Optional ByVal LoadXMLInfo As Boolean = True, _
                             Optional ByVal BatchMode As Boolean = False) As Interfaces.Layer

        Dim lyrFilename As String = ""      ' full name of the layer with path
        Dim lyrName As String               ' short name
        Dim lyrType As Interfaces.eLayerType = Interfaces.eLayerType.Invalid

        Dim newObject As Object = Nothing
        Dim newGrid As MapWinGIS.Grid = New MapWinGIS.Grid
        Dim newLyr As MapWindow.Interfaces.Layer = Nothing

        Dim groupCount = frmMain.Legend.Groups.Count

        If TypeName(ObjectOrFilename) = "String" Then

            ' --------------------------------------------------
            '  Filename was passed
            ' --------------------------------------------------
            If ObjectOrFilename.tolower().startswith("ecwp://") Then
                lyrName = ObjectOrFilename.ToString
            Else
                If Not File.Exists(ObjectOrFilename) Then
                    MapWinUtility.Logger.Msg("File doesn't exists: " & ObjectOrFilename, "MapWin.Layers.AddLayer")
                    GoTo ENDFUNC
                End If

                If LayerName = "" Then
                    lyrName = MapWinUtility.MiscUtils.GetBaseName(ObjectOrFilename.ToString)
                Else
                    lyrName = LayerName
                End If
            End If

            lyrFilename = ObjectOrFilename.ToString

            'Opening the file
            frmMain.Plugins.BroadcastMessage("Layer addition initiated, file name :" + lyrFilename)
            newObject = OpenDataFile(lyrFilename, newGrid, GrdColorScheme, lyrType)
            If newObject Is Nothing Then GoTo ENDFUNC
        Else

            ' --------------------------------------------------
            '  Object was passed
            ' --------------------------------------------------
            newObject = ObjectOrFilename

            ' extracting name
            Dim obj As New LayerSource(newObject)
            If (obj.Type = LayerSourceType.Undefined) Then
                MapWinUtility.Logger.Msg("Interface of the object being added isn't supported", "MapWin.Layers.AddLayer")
                GoTo ENDFUNC
            End If
            lyrFilename = obj.Filename

            Static addCnt As Integer = 0
            If LayerName <> "" Then
                ' in case layer name is passed by user, we use it
                lyrName = LayerName
            Else
                If lyrFilename <> "" Then
                    lyrName = MapWinUtility.MiscUtils.GetBaseName(lyrFilename)
                Else
                    ' if there is no way to extract the name, we'll generate a unique one
                    lyrName = "Layer " & addCnt
                    addCnt = addCnt + 1
                End If
            End If

            ' Creating bmp for grids, handling projections
            If TypeOf (newObject) Is MapWinGIS.Grid Then
                Dim image As MapWinGIS.Image = Me.OpenGridCore(lyrFilename, CType(newObject, MapWinGIS.Grid), GrdColorScheme)  ' Open grid as image was used
                If Not image Is Nothing Then
                    newGrid = CType(newObject, MapWinGIS.Grid)
                    lyrType = Interfaces.eLayerType.Grid
                    newObject = image
                Else
                    GoTo ENDFUNC
                End If
            End If
        End If

        ' -----------------------------------------------------------------
        '   Adding object to the map
        ' -----------------------------------------------------------------
        Dim mapHandle As Integer
        If (PositionFromSelected And Not frmMain.Legend.SelectedLayer = -1) Then
            'Chris Michaelis for Bugzilla 310 - add above currently selected layer (if any)
            Dim addPos As Integer = frmMain.Legend.Layers.PositionInGroup(frmMain.m_layers.CurrentLayer) + 1
            Dim addGrp As Integer = frmMain.Legend.Layers.GroupOf(frmMain.m_layers.CurrentLayer)

            mapHandle = frmMain.Legend.Layers.Add(LegendVisible, newObject, LayerVisible)
            frmMain.Legend.Layers.MoveLayer(mapHandle, addGrp, addPos)
        Else
            mapHandle = frmMain.Legend.Layers.Add(LegendVisible, newObject, LayerVisible)
        End If

        If Not IsValidHandle(mapHandle) Then
            MapWinUtility.Logger.Dbg("Failed to open a layer: " + ObjectOrFilename.ToString())
            GoTo ENDFUNC
        End If

        modMain.frmMain.SetModified(True) ' the layer was added successfully

        ' treating the position parameters passed
        If (LayerPosition > -1 AndAlso Group > -1) Then
            ' Paul Meems May 28 2010: Added layer position:
            frmMain.Legend.Layers.MoveLayer(mapHandle, Group, LayerPosition)
        End If

        ' -----------------------------------------------------------------
        ' Saving the grid object: the lyrType is ignored for all other types other than grid
        ' (the legend is smart enough to figure the others out by itself)
        ' -----------------------------------------------------------------
        If lyrType = Interfaces.eLayerType.Grid Then
            If m_Grids.Contains(mapHandle) Then m_Grids.Remove(mapHandle)
            m_Grids.Add(mapHandle, newGrid)
            frmMain.Legend.Layers.ItemByHandle(mapHandle).Type = eLayerType.Grid
        End If

        ' -----------------------------------------------------------------
        ' Importing color shceme from the mwleg file
        ' -----------------------------------------------------------------
        If lyrType = Interfaces.eLayerType.Grid AndAlso GrdColorScheme Is Nothing AndAlso IsValidHandle(mapHandle) Then
            Dim name As String = IO.Path.ChangeExtension(lyrFilename, ".mwleg")
            If IO.File.Exists(name) Then
                Dim obj As Object = ColoringSchemeTools.ImportScheme(frmMain.Layers(mapHandle), name)
                GrdColorScheme = CType(obj, MapWinGIS.GridColorScheme)
            End If
        End If

        ' ----------------------------------------------
        '  creating layer object
        ' ----------------------------------------------
        newLyr = New MapWindow.Layer
        newLyr.Handle = mapHandle
        newLyr.Name = lyrName

        ' ------------------------------------------------------------
        ' Loads rendring info (.mwsymb or .mwsr).
        ' ------------------------------------------------------------
        If LoadXMLInfo Then 'Not frmMain.m_LoadingProject And LoadXMLInfo Then
            Select Case AppInfo.SymbologyLoadingBehavior
                Case SymbologyBehavior.RandomOptions
                    ' do nothing, all code embedded in ocx
                Case SymbologyBehavior.DefaultOptions
                    If newLyr.LayerType = Interfaces.eLayerType.LineShapefile Or _
                        newLyr.LayerType = Interfaces.eLayerType.PointShapefile Or _
                        newLyr.LayerType = Interfaces.eLayerType.PolygonShapefile Then

                        ' first we'll try .mwsymb with deafult name (.shp.mwsymb and .shp.view-deafult.mwsymb)
                        ' restoring of the labels is incorporated
                        Dim description As String = ""
                        Dim res As Boolean = frmMain.MapMain.LoadLayerOptions(mapHandle, "", description)

                        ' let's try the older format
                        If Not res Then
                            ' .mwsr
                            frmMain.LoadRenderingOptions(newLyr.Handle, "", m_PluginCall)
                            ' .lbl
                            Dim sf As MapWinGIS.Shapefile = CType(newObject, MapWinGIS.Shapefile)
                            Dim name As String = System.IO.Path.ChangeExtension(sf.Filename, ".lbl")
                            sf.Labels.LoadFromXML(name)
                        End If

                    ElseIf newLyr.LayerType = Interfaces.eLayerType.Grid Then
                        ' .mwsymb file
                        Dim description As String = ""
                        Dim res As Boolean = frmMain.MapMain.LoadLayerOptions(mapHandle, "", description)

                        ' older format
                        If LayerName = "" And Not res Then
                            frmMain.LoadRenderingOptions(newLyr.Handle, "", m_PluginCall)
                        End If
                    End If
                Case SymbologyBehavior.UserPrompting
                    frmMain.Plugins.BroadcastMessage("SYMBOLOGY_CHOOSE:" + mapHandle.ToString() + "!" + lyrFilename)

                    Dim position = frmMain.MapMain.get_LayerPosition(newLyr.Handle)
                    If position < 0 Then
                        ' operation was cancelled, layer was removed in the plug-in
                        ' in case default Data layers group was added, we should delete it
                        If groupCount = 0 And frmMain.Legend.Groups.Count > 0 Then
                            frmMain.Legend.Groups.Clear()
                        End If
                        newLyr = Nothing
                        GoTo ENDFUNC
                    End If
            End Select
        End If

        ' -------------------------------------------------------------
        '   Saving rendering info for grids
        ' -------------------------------------------------------------
        ' Run this after LoadRenderingOptions - it will resave out any mwsr file with latest information (e.g., last used rendering)
        If Not GrdColorScheme Is Nothing Then
            frmMain.MapMain.SetImageLayerColorScheme(mapHandle, GrdColorScheme)
            frmMain.MapMain.set_GridFileName(mapHandle, lyrFilename)

            Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(mapHandle), MapWinGIS.Image)
            If Not img Is Nothing Then
                img.TransparencyColor = GrdColorScheme.NoDataColor
                img.TransparencyColor2 = GrdColorScheme.NoDataColor
                img.UseTransparencyColor = True
                img = Nothing
            End If
            frmMain.Legend.Layers.ItemByHandle(mapHandle).Refresh()

            Dim imgName As String = CType(newObject, MapWinGIS.Image).Filename
            ColoringSchemeTools.ExportScheme(frmMain.Layers(mapHandle), IO.Path.ChangeExtension(imgName, ".mwleg"))
        End If

ENDFUNC:
        ' cleaning
        newObject = Nothing
        GC.Collect()
        Return newLyr
    End Function
#End Region

#Region "OpenDatafile"
    ''' <summary>
    ''' Opens the files provided and returnes intance of the shapefile or image class as a result
    ''' </summary>
    Private Function OpenDataFile(ByRef filename As String, ByRef newGrid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme, _
                                  ByRef layerType As eLayerType) As Object

        'get cdlgfilter from image, then see if the extension is in that filter
        Dim ExtName As String = LCase(MapWinUtility.MiscUtils.GetExtensionName(filename))
        Dim newImage As New MapWinGIS.Image

        ' ---------------------------------------------------
        '  Adding layer file (.mwlyr)
        ' ---------------------------------------------------
        If filename.ToLower().EndsWith(".mwsymb") Then

            ' .shp.mwsymb
            Dim name As String = filename.Substring(0, filename.Length - 7)
            If (Not File.Exists(name)) Then
                Dim pos As Integer = name.LastIndexOf(".")
                If pos > 0 Then
                    name = name.Substring(0, pos)
                End If
            End If

            If (File.Exists(name)) Then
                Dim options As String = filename.Substring(name.Length)     ' stripping name part
                options = options.Substring(0, options.Length - 7)          ' stripping .mwsymb part
                If options.Length > 0 Then options = options.Substring(1)

                ' recursive call
                Dim layer As MapWindow.Interfaces.Layer = Me.AddLayerCore(name, LoadXMLInfo:=False)
                If Not layer Is Nothing Then
                    Dim description As String = ""
                    frmMain.MapMain.LoadLayerOptions(layer.Handle, options, description)
                End If

                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
                frmMain.Legend.Refresh()
                Return Nothing
            Else
                MessageBox.Show("Unable to find dataset to load: " + Environment.NewLine + _
                                name, frmMain.ApplicationInfo.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End If
        End If

        ' ------------------------------------------------
        '  convert wmf to bitmap
        ' ------------------------------------------------
        If (InStr(1, filename, ".wmf", vbTextCompare) <> 0) Then
            Dim cvter As New System.Drawing.Bitmap(filename)
            filename = GetMWTempFile() + ".bmp"
            ExtName = "bmp"
            cvter.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp)
        End If

        ' -------------------------------------------------------------------
        ' Convert flt, grd into binary grids that can be opened reliably. 
        ' This causes it to open twice technically, but is worth it 
        ' in the long run for speed versus running with SuperGrid
        ' -------------------------------------------------------------------
        If ExtName.ToLower().EndsWith("flt") Or ExtName.ToLower().EndsWith("grd") Then
            Dim bgdequiv As String = System.IO.Path.ChangeExtension(filename, ".bgd")
            If IO.File.Exists(bgdequiv) AndAlso MapWinUtility.DataManagement.CheckFile2Newest(filename, bgdequiv) Then
                filename = bgdequiv
            Else
                'Convert it
                Dim cvg As New SuperGrid
                If cvg.Open(filename) Then
                    'Opening it will cause the equiv. bgd to be written
                    cvg.Close()
                    filename = bgdequiv
                    'else -- try to open with grid handler in ocx - may fail for these three formats though.
                End If
            End If
        End If

        ' ------------------------------------------------------
        ' Treat aux file: 
        ' ------------------------------------------------------
        Try
            Dim IsAuxHeader As Boolean = False
            Dim lyrFileStm As System.IO.FileStream = System.IO.File.OpenRead(filename)
            If Not lyrFileStm Is Nothing Then
                Dim header(10) As Byte
                lyrFileStm.Read(header, 0, 11)
                lyrFileStm.Close()
                Dim headerEncoding As New System.Text.ASCIIEncoding
                Dim strheader As String = headerEncoding.GetString(header)
                If strheader = "EHFA_HEADER" Then 'open the sta.adf fi le instead
                    ' Chris M 1/27/2007 -- IF it exists
                    If IO.File.Exists(filename.Substring(0, filename.Length - 4) + "\sta.adf") Then
                        filename = filename.Substring(0, filename.Length - 4) + "\sta.adf"
                    End If
                End If
            End If
        Catch ex As Exception
            'This is a minor test -- don't show the error. ShowError(ex)
        End Try

        ' ----------------------------------------------------------
        '  Opening .adf and .asc files (ESRI grids)
        ' ----------------------------------------------------------
        If InStr(1, filename, ".adf", vbTextCompare) <> 0 Or _
           InStr(1, filename, ".asc", vbTextCompare) <> 0 Then
            newImage = OpenEsriGrid(filename, newGrid, GrdColorScheme)
            If Not newImage Is Nothing Then
                layerType = eLayerType.Grid
                Return newImage
            Else
                ' we'll try to open it as image below
            End If

            ' --------------------------------------------------------------
            '   opening tif, img, dem, flt, grd
            ' --------------------------------------------------------------'
        ElseIf InStr(1, filename, ".tif", vbTextCompare) <> 0 Or _
               InStr(1, filename, ".img", vbTextCompare) <> 0 Or _
               InStr(1, filename, ".dem", vbTextCompare) <> 0 Or _
               InStr(1, filename, ".flt", vbTextCompare) <> 0 Or _
               InStr(1, filename, ".grd", vbTextCompare) <> 0 Then
            ' Chris Michaelis August 31 2005 (for tif) and Feb 8 2007 (for img)
            ' Note -- treat these together because they both have the potential of having
            ' an embedded coloring scheme (whereas ESRI grids do not).
            newImage = OpenTiffAsGridFile(filename, newGrid, GrdColorScheme)
            If Not newImage Is Nothing Then
                layerType = eLayerType.Grid
                Return newImage
            Else
                ' we'll try to open it as image below
            End If

            ' ---------------------------------------------------
            '   opening shapefiles
            ' ---------------------------------------------------
        ElseIf InStr(1, filename, ".shp", vbTextCompare) <> 0 Then

            Dim sf As MapWinGIS.Shapefile
            sf = OpenShapefile(filename)
            Return sf

            ' --------------------------------------------------
            '  opening grids
            ' --------------------------------------------------
        ElseIf InStr(1, LCase(newGrid.CdlgFilter), ExtName, vbTextCompare) <> 0 Or _
                ExtName.ToLower().EndsWith("flt") Or _
                ExtName.ToLower().EndsWith("dem") Or _
                ExtName.ToLower().EndsWith("grd") Then

            layerType = Interfaces.eLayerType.Grid
            newImage = OpenGrid(filename, newGrid, GrdColorScheme)
            Return newImage
        End If

        ' ----------------------------------------------
        ' Opening images
        ' If we failed to load it as grid, try to do it as image
        ' ----------------------------------------------
        Dim img As MapWinGIS.Image = New MapWinGIS.Image
        If InStr(1, LCase(img.CdlgFilter), ExtName, vbTextCompare) <> 0 Then
            If img.Open(filename, MapWinGIS.ImageType.USE_FILE_EXTENSION, False, CType(Me, MapWinGIS.ICallback)) = False Then
                MapWinUtility.Logger.Msg("Failed to open file", "MapWin.Layers.AddLayer")
                Return Nothing
            Else
                Return img
            End If
        Else
            '   Unsupported format
            MapWinUtility.Logger.Msg("File format not supported.", "MapWin.Layers.AddLayer")
            Return Nothing
        End If
    End Function
#End Region

#Region "Open grid"
    ''' <summary>
    ''' Opens grid of the following formats: adf, asc
    ''' </summary>
    Private Function OpenEsriGrid(ByRef filename As String, ByRef grid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme) As MapWinGIS.Image
        If AppInfo.LoadESRIAsGrid = ESRIBehavior.LoadAsGrid Then
            If Not grid.Open(filename, MapWinGIS.GridDataType.UnknownDataType, True, MapWinGIS.GridFileType.UseExtension, CType(Me, MapWinGIS.ICallback)) Then Return Nothing
            Return Me.OpenGridCore(filename, grid, GrdColorScheme)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Load TIFF file as grid. The supported extentions: tif, img, dem, flt, grd. Opening as image via GDAL in the calling sub.
    ''' </summary>
    Private Function OpenTiffAsGridFile(ByRef filename As String, ByRef grid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme) As MapWinGIS.Image
        If LoadingTIForIMGasGrid(filename) Then
            'Convert dem into binary grids that can be opened reliably. 
            'This causes it to open twice technically, but is worth it 
            'in the long run for speed versus running with SuperGrid
            If IO.Path.GetExtension(filename).ToLower().EndsWith("dem") Then
                Dim bgdequiv As String = System.IO.Path.ChangeExtension(filename, ".bgd")
                If IO.File.Exists(bgdequiv) AndAlso MapWinUtility.DataManagement.CheckFile2Newest(filename, bgdequiv) Then
                    filename = bgdequiv
                Else
                    'Convert it
                    Dim cvg As New SuperGrid
                    If cvg.Open(filename) Then
                        'Opening it will cause the equiv. bgd to be written
                        cvg.Close()
                        filename = bgdequiv
                        'else -- try to open with grid handler in ocx - may fail for these three formats though.
                    End If
                End If
            End If

            If Not grid.Open(filename, MapWinGIS.GridDataType.UnknownDataType, True, MapWinGIS.GridFileType.GeoTiff, Nothing) Then Return Nothing

            Return OpenGridCore(filename, grid, GrdColorScheme)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Opens grid with specified filename
    ''' </summary>
    Private Function OpenGrid(ByVal filename As String, ByRef grid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme) As MapWinGIS.Image

        If filename.ToLower.EndsWith("sta.adf") Then
            'chop ESRI files down to just the directory name
            filename = filename.Substring(0, filename.Length - 8)
        End If

        Try
            If (grid.Open(filename, MapWinGIS.GridDataType.UnknownDataType, True, MapWinGIS.GridFileType.UseExtension, CType(Me, MapWinGIS.ICallback)) = False) Then
                Return Nothing
            End If
        Catch ex As Exception
            Dim e As New Exception("GRID OPEN failure: " + filename + " -- " + ex.ToString())
            CustomExceptionHandler.OnThreadException(e)
            Return Nothing
        End Try

        Return OpenGridCore(filename, grid, GrdColorScheme)
    End Function

    ''' <summary>
    ''' Generic function seeks bitmap representation of grid, and if it's not present or out of sync with colorscheme, generates a new one
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OpenGridCore(ByVal filename As String, ByRef grid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme) As MapWinGIS.Image
        'Check the projection of the layer; ensure it's in sync with everything
        'else, prompt the user if not. For full details see the frmProjMismatch.
        'This function will alter "newObject" however necessary.
        Dim abort As Boolean = False
        'Dim mismatchTester As New frmProjMismatch
        'mismatchTester.TestProjection(CType(grid, Object), abort, filename)

        If abort Then
            TryCloseObject(CType(grid, Object))
            Return Nothing
        End If

        Dim legFile As String = IO.Path.ChangeExtension(filename, ".mwleg")
        Dim imgName As String = IO.Path.ChangeExtension(filename, ".bmp")
        Dim image As New MapWinGIS.Image

        ' -------------------------------------------------
        ' check whether we have relevant bmp file and it is 
        ' the right one (by date and color scheme)
        ' -------------------------------------------------
        If IO.File.Exists(imgName) _
        AndAlso MapWinUtility.DataManagement.CheckFile2Newest(filename, imgName) _
        AndAlso IO.File.Exists(legFile) _
        AndAlso (GrdColorScheme Is Nothing OrElse ColoringSchemeTools.ColoringSchemesAreEqual(GrdColorScheme, legFile)) Then
            ' opening previously saved .bmp representation
            image.Open(IO.Path.ChangeExtension(filename, ".bmp"), MapWinGIS.ImageType.USE_FILE_EXTENSION, False, CType(Me, MapWinGIS.ICallback))
        Else

            'The grid coloring scheme either doesn't exist or is out of sync with the image, or the grid doesn't have an image.
            If GrdColorScheme Is Nothing And System.IO.File.Exists(legFile) Then
                'if an existing .mwleg is there, generate with it rather than generating a fully new one.
                'Just because a few values in a grid changed doesn't mean they want a fully new coloring scheme regenerated
                GrdColorScheme = New MapWinGIS.GridColorScheme
                Dim doc As New System.Xml.XmlDocument
                doc.Load(legFile)
                ColoringSchemeTools.ImportScheme(GrdColorScheme, doc.DocumentElement.Item("GridColoringScheme"))
            End If

            If GrdColorScheme Is Nothing Then
                ' lsu: this check was initially intended for tif's only, but I leave it for all types for now
                ' if there are problem extention check can be added
                GrdColorScheme = grid.RasterColorTableColoringScheme()
            End If


            'Create a generic random grid coloring scheme.
            If GrdColorScheme Is Nothing OrElse GrdColorScheme.NumBreaks < 1 Then
                GenerateGridColorScheme(grid, GrdColorScheme)
            End If

            If GrdColorScheme Is Nothing Then
                MapWinUtility.Logger.Msg("Failed to Generate Coloring for Grid", "OpenGridCore")
                Return Nothing
            End If

            'Create an image using the grid coloring scheme.
            GetImageRep(filename, image, grid, GrdColorScheme, CType(Me, MapWinGIS.ICallback))

            ' transparency color is determined based on no data value in rather complex way,
            ' it should be set for color scheme
            GrdColorScheme.NoDataColor = image.TransparencyColor

            ReportProgress("", 0, "")
        End If

        Return image
    End Function

    Private Function LoadingTIForIMGasGrid(ByVal fn As String) As Boolean
        If fn.ToLower().EndsWith(".img") Then
            If AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsGrid Then
                Return True
            Else
                Return False
            End If
        ElseIf fn.ToLower().EndsWith(".tif") Then
            If AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsGrid Then Return True
            If AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.Automatic And frmMain.MapMain.IsTIFFGrid(fn) Then Return True
            Return False
        Else 'is some other grid return true unless specifically told to open as image
            If AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.LoadAsGrid Then Return True
            If AppInfo.LoadTIFFandIMGasgrid = GeoTIFFAndImgBehavior.Automatic Then Return True
            Return False
        End If
    End Function
#End Region

#Region "Open shapefile"
    ''' <summary>
    ''' Opens shapefile layer (read-only state, projection, table)
    ''' Test whether shapefile is read-only, ask to copy it to another location
    ''' Probably it's not needed any more, as after changes in ocx 
    ''' </summary>
    Private Function OpenShapefile(ByVal filename As String) As MapWinGIS.Shapefile

        Dim fi As New FileInfo(filename)
        If (fi.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
            'Note parenthesis placement above (important)
            Static LastAnswer As frmYesNoToAll.DialogResult = frmYesNoToAll.DialogResult.Undefined
            Static LastPath As String = ""

            If LastAnswer = frmYesNoToAll.DialogResult.NoToAll Then
                'No action needed
            ElseIf LastAnswer = frmYesNoToAll.DialogResult.YesToAll And Not LastPath = "" Then
                'Don't show dialog - just do it
                MapWinGeoProc.DataManagement.CopyShapefile(filename, LastPath + "\" + System.IO.Path.GetFileName(filename))
                filename = LastPath + "\" + System.IO.Path.GetFileName(filename)
                'Path is updated - proceed
            Else
                LastAnswer = frmYesNoToAll.ShowPrompt("Warning: The layer you are adding is read-only. Do you wish to copy the layer to another location before adding it?", "Read-Only Layer - Copy?")
                If LastAnswer = frmYesNoToAll.DialogResult.Yes Or LastAnswer = frmYesNoToAll.DialogResult.YesToAll Then
                    Dim fb As New FolderBrowserDialog
                    fb.SelectedPath = AppInfo.DefaultDir
                    If fb.ShowDialog() = DialogResult.OK Then
                        MapWinGeoProc.DataManagement.CopyShapefile(filename, fb.SelectedPath + "\" + System.IO.Path.GetFileName(filename))
                        filename = fb.SelectedPath + "\" + System.IO.Path.GetFileName(filename)
                        LastPath = fb.SelectedPath
                        'Path is updated - proceed
                    End If
                End If
            End If
        End If

        ' open
        Dim sf As New MapWinGIS.Shapefile
        If sf.Open(filename, CType(Me, MapWinGIS.ICallback)) = False Then
            MapWinUtility.Logger.Msg("Failed to open file: " & sf.ErrorMsg(sf.LastErrorCode), "MapWin.Layers.AddLayer")
        End If

        'Perform some basic testing on the shapefile (dbf is present, num rows is equal to num shapes)
        Dim abort As Boolean = False
        TestShapefile(sf, abort)

        If abort Then
            TryCloseObject(CType(sf, Object))
            MapWinUtility.Logger.Msg("Failed to open file", "MapWin.Layers.AddLayer")
            Return Nothing
        Else
            Return sf
        End If
    End Function

    ''' <summary>
    ''' Sets the properties of the layer that was passed as arguments
    ''' (Why on earth it was done? Isn't it more reasonable a) to add the layer; b) to set any parameters needed in the calling procedure.
    ''' The reference to the layer is returned to the caller all the same)
    ''' </summary>
    Private Sub SetLayerProperties(ByVal layer As MapWindow.Interfaces.Layer, _
                                   Optional ByVal Color As Integer = -1, _
                                   Optional ByVal OutlineColor As Integer = -1, _
                                   Optional ByVal DrawFill As Boolean = True, _
                                   Optional ByVal LineOrPointSize As Single = 1, _
                                   Optional ByVal PointType As MapWinGIS.tkPointType = 0)

        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then

            layer.DrawFill = DrawFill

            If Color = -1 Then
                layer.Color = MapWinUtility.Colors.IntegerToColor(MakeRandomColor())
            Else
                layer.Color = MapWinUtility.Colors.IntegerToColor(Color)
            End If

            If OutlineColor = -1 Then
                layer.OutlineColor = MapWinUtility.Colors.IntegerToColor(MakeRandomColor())
            Else
                layer.OutlineColor = MapWinUtility.Colors.IntegerToColor(OutlineColor)
            End If

            If layer.LayerType = eLayerType.PointShapefile Then
                If PointType = MapWinGIS.tkPointType.ptUserDefined Then
                    layer.LineOrPointSize = LineOrPointSize
                Else
                    If LineOrPointSize = 1 Then
                        layer.LineOrPointSize = 3
                    Else
                        layer.LineOrPointSize = LineOrPointSize
                    End If
                End If
            Else
                layer.LineOrPointSize = LineOrPointSize
            End If

            layer.PointType = PointType
        End If

    End Sub

    Private Sub TestShapefile(ByRef sf As MapWinGIS.Shapefile, ByRef abort As Boolean)
        abort = False

        'Test 1 - Ensure that the DBF exists
        If Not System.IO.File.Exists(System.IO.Path.ChangeExtension(sf.Filename, ".dbf")) Then
            If Not MapWinUtility.Logger.Msg("Warning: This shapefile appears to have no database table (.dbf) associated with it!" + vbCrLf + vbCrLf + "Do you wish to continue adding this layer?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Missing Database Table") = MsgBoxResult.Yes Then abort = True
            Exit Sub
        End If

        'Test 2 -- Check that the number of DBF records matches the number of shapes.
        Dim tbl As New MapWinGIS.Table
        tbl.Open(System.IO.Path.ChangeExtension(sf.Filename, ".dbf"))
        If Not sf.NumShapes = tbl.NumRows Then
            If Not MapWinUtility.Logger.Msg("The selected shapefile's related file is being accessed by another program:" & vbCrLf & vbCrLf & _
                       sf.Filename & vbCrLf & vbCrLf & "The ShapeCheck utility (http://www.mapwindow.org/download/shapechk.zip) can correct this and other shapefile errors." + vbCrLf + vbCrLf + "Continue adding this layer?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Corrupt Shapefile") = MsgBoxResult.Yes Then abort = True
            tbl.Close()
            Exit Sub
        Else
            tbl.Close()
        End If
    End Sub
#End Region

#Region "Utilities"
    ''' <summary>
    ''' Gets the default visiblity of the new layer taking into accout the group it belongs to
    ''' </summary>
    Public Shared Function GetDefaultLayerVis()
        Dim Grp As Integer = -1
        If frmMain.Legend.SelectedLayer <> -1 Then
            Grp = frmMain.Legend.Layers.GroupOf(frmMain.m_layers.CurrentLayer)
        ElseIf frmMain.Legend.Groups.Count > 0 Then
            Grp = 0
        End If

        If Grp = -1 Then Return True
        Dim oGrp As LegendControl.Group = frmMain.Legend.Groups.ItemByHandle(Grp)
        If oGrp Is Nothing Then Return True
        Return oGrp.LayersVisible()
    End Function

    Private Function MakeRandomColor() As Integer
        Return RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255))
    End Function

    Public Function GetSupportedFormats() As String
        'build the new common dialog filter from what is available
        Dim GridUtil As New GridUtils, sf As New MapWinGIS.Shapefile, img As New MapWinGIS.Image
        Dim vArr() As String, allNames As New ArrayList, allVals As New ArrayList
        Dim i As Integer

        vArr = Split(sf.CdlgFilter, "|")

        On Error Resume Next
        For i = 0 To UBound(vArr) Step 2
            If LCase(Left(vArr(i), Len("all supported"))) <> "all supported" And Not allVals.Contains(vArr(i + 1)) And Not allNames.Contains(vArr(i)) Then
                allNames.Add(vArr(i)) ' value
                allVals.Add(vArr(i + 1)) ' key
            End If
        Next i

        vArr = Split(GridUtil.GridCdlgFilter, "|")
        GridUtil = Nothing

        On Error Resume Next
        For i = 0 To UBound(vArr) Step 2
            If LCase(Left(vArr(i), Len("all supported"))) <> "all supported" And Not allVals.Contains(vArr(i + 1)) And Not allNames.Contains(vArr(i)) Then
                allNames.Add(vArr(i)) ' value
                allVals.Add(vArr(i + 1)) ' key
            End If
        Next i

        vArr = Split(img.CdlgFilter, "|")

        For i = 0 To UBound(vArr) Step 2
            If LCase(Left(vArr(i), Len("all supported"))) <> "all supported" And Not allVals.Contains(vArr(i + 1)) And Not allNames.Contains(vArr(i)) Then
                If Left(vArr(i), Len("MRSID")) <> "MrSID" Then 'Fix the dumplicate SID problem. Laiin Chen 2006/4/7
                    allNames.Add(vArr(i))
                    allVals.Add(vArr(i + 1))
                End If
            End If
        Next i

        allNames.Add("Windows Metafile (*.wmf)")
        allVals.Add("*.WMF")

        'allNames.Add("MapWindow Layer (*.mwsymb)")   ' lsu: 31-jul-2010
        'allVals.Add("*.mwsymb")

        Dim keys() As Object
        keys = allVals.ToArray()

        Dim allExtensions As String = ""
        Dim allTypes As String = ""

        For i = 0 To UBound(keys)
            If Len(allExtensions) = 0 Then
                If Right(CStr(keys(i)), 1) = ";" Then
                    allExtensions = Trim(Left(keys(i).ToString, Len(keys(i)) - 1))
                Else
                    allExtensions = Trim(keys(i).ToString)
                End If
            Else
                If Right(keys(i).ToString, 1) = ";" Then
                    allExtensions &= ";" & Trim(Left(keys(i).ToString, Len(keys(i)) - 1))
                Else
                    allExtensions &= ";" & Trim(keys(i).ToString)
                End If
            End If

            If Len(allTypes) = 0 Then
                allTypes = allNames(allVals.IndexOf(keys(i))).ToString & "|" & Trim(keys(i).ToString)
            Else
                allTypes &= "|" & Trim(allNames(allVals.IndexOf(keys(i))).ToString) & "|" & Trim(keys(i).ToString)
            End If
        Next i

        Return "All supported formats|" & allExtensions & "|" & allTypes
    End Function

    Private Sub TryCloseObject(ByVal newObject As Object)
        Try
            If Not newObject Is Nothing Then
                If TypeOf (newObject) Is MapWinGIS.Grid Then
                    CType(newObject, MapWinGIS.Grid).Close()
                ElseIf TypeOf (newObject) Is MapWinGIS.Shapefile Then
                    CType(newObject, MapWinGIS.Shapefile).Close()
                ElseIf TypeOf (newObject) Is MapWinGIS.Image Then
                    CType(newObject, MapWinGIS.Image).Close()
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Shows layer dialog in case no file name was specified
    ''' </summary>
    Private Function ShowLayerDialog() As String()
        Dim cdlOpen As New OpenFileDialog

        'set the default dir
        If (System.IO.Directory.Exists(AppInfo.DefaultDir)) Then
            cdlOpen.InitialDirectory = AppInfo.DefaultDir
        End If

        cdlOpen.FileName = ""
        cdlOpen.Title = "Add Map Layer"
        cdlOpen.Filter = GetSupportedFormats()

        cdlOpen.CheckFileExists = True
        cdlOpen.CheckPathExists = True
        cdlOpen.Multiselect = True
        cdlOpen.ShowReadOnly = False

        Dim filenames() As String = Nothing

        If cdlOpen.ShowDialog() = DialogResult.Cancel Then
            ' do nothing
        Else
            filenames = cdlOpen.FileNames()
        End If

        ' save the location of the last open dir
        If (File.Exists(cdlOpen.FileName)) Then
            Dim dir As String = IO.Path.GetDirectoryName(cdlOpen.FileName)
            If (IO.Directory.Exists(dir)) Then
                AppInfo.DefaultDir = System.IO.Path.GetDirectoryName(cdlOpen.FileName)
            End If
        End If

        cdlOpen.Dispose()
        frmMain.Update()

        Return filenames
    End Function
#End Region

#Region "Grid utilities"
    ''' <summary>
    ''' Changed to create unique breaks if less than one hundred unique values.
    ''' </summary>
    ''' <param name="newGrid"></param>
    ''' <param name="GrdColorScheme"></param>
    ''' <remarks>The function returns false if there were > 100 or if unique break creation failed.</remarks>
    Private Sub GenerateGridColorScheme(ByRef newGrid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme)
        If Not GridColoringSchemeForm.GetUniqueBreaks(newGrid, True, GrdColorScheme, MapWinGIS.GradientModel.Linear, MapWinGIS.ColoringType.Random, "N", 3) Then
            GrdColorScheme = New MapWinGIS.GridColorScheme
            Dim rnd As New Random
            GrdColorScheme.UsePredefined(newGrid.Minimum, newGrid.Maximum, CType(rnd.Next(0, 7), MapWinGIS.PredefinedColorScheme))
        End If
    End Sub

    <CLSCompliant(False)> _
    Public Function RebuildGridLayer(ByVal LayerHandle As Integer, ByVal GridObject As MapWinGIS.Grid, ByVal ColorScheme As MapWinGIS.GridColorScheme) As Boolean Implements MapWindow.Interfaces.Layers.RebuildGridLayer
        'Rebuilds an image associated with a grid layer.
        '4/23/2005 - dpa - Updated to sync the legend with the new coloring scheme that may have been passed in. 
        Dim img As MapWinGIS.Image = Nothing
        Dim NewScheme As MapWinGIS.GridColorScheme = Nothing
        Dim fileName As String
        Dim tmpImage As MapWinGIS.Image
        Dim gc As New MapWinGIS.Utils
        Dim oldUseTrans As Boolean
        Dim oldTransColor As UInt32

        Try
            'Make sure there is a valid grid object at the current layer handle.
            img = CType(frmMain.MapMain.get_GetObject(LayerHandle), MapWinGIS.Image)

            If GridObject Is Nothing Then
                g_error = "RebuildGridLayer:  GridObject parameter is 'Nothing'"
                Return False
            End If
            If img Is Nothing Then
                g_error = frmMain.MapMain.get_ErrorMsg(frmMain.MapMain.LastErrorCode)
                Return False
            End If

            'Make sure there is a valid coloring scheme object.
            If ColorScheme Is Nothing OrElse ColorScheme.NumBreaks < 1 Then
                GenerateGridColorScheme(GridObject, NewScheme)
            Else
                NewScheme = ColorScheme
            End If

            'Create the new image.
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
            Try
                '50 MEG seems to be the breaking point between reading from temp bmp and directly
                If (img.Filename.EndsWith(".tif") OrElse img.Filename.EndsWith(".tiff") OrElse img.Filename.EndsWith(".img")) Then
                    Try
                        CType(frmMain.Layers(LayerHandle).GetObject(), MapWinGIS.Image)._pushSchemetkRaster(ColorScheme)
                    Catch e As Exception
                        Debug.WriteLine(e.ToString())
                    End Try
                Else
                    fileName = img.Filename
                    'Ensure that the filename ends in BMP -- don't rewrite TIF or BIL with bitmap data into the TIF or BIL extension
                    fileName = System.IO.Path.ChangeExtension(fileName, ".bmp")

                    oldUseTrans = img.UseTransparencyColor
                    oldTransColor = img.TransparencyColor
                    img.Close()
                    tmpImage = gc.GridToImage(GridObject, NewScheme)
                    tmpImage.Save(fileName, True, MapWinGIS.ImageType.BITMAP_FILE, Me)
                    tmpImage.Close()
                    tmpImage = Nothing
                    img.Open(fileName, MapWinGIS.ImageType.BITMAP_FILE, False, Me)
                    img.UseTransparencyColor = oldUseTrans
                    img.TransparencyColor = oldTransColor
                    img.TransparencyColor2 = oldTransColor
                End If
            Finally
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End Try
            frmMain.View.Extents = frmMain.View.Extents
            frmMain.MapMain.UpdateImage(LayerHandle)
            frmMain.MapMain.Redraw()
            Application.DoEvents()

            'Update the Legend (added 4/22/2005 - dpa)
            frmMain.MapMain.SetImageLayerColorScheme(LayerHandle, NewScheme)
            frmMain.MapMain.set_GridFileName(LayerHandle, GridObject.Filename)
            frmMain.Legend.Layers.ItemByHandle(LayerHandle).Refresh()
            ColoringSchemeTools.ExportScheme(frmMain.Layers(LayerHandle), IO.Path.ChangeExtension(img.Filename, ".mwleg"))

            ReportProgress("", 0, "")

        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    'Chris Michaelis Sept 2006
    'Find a suitable image representation of the grid data
    <CLSCompliant(False)> _
    Public Sub GetImageRep(ByVal filename As String, ByRef newImage As MapWinGIS.Image, ByRef newGrid As MapWinGIS.Grid, ByRef GrdColorScheme As MapWinGIS.GridColorScheme, ByRef cb As MapWinGIS.ICallback)
        Try
            If newGrid Is Nothing Then Return

            MapWinUtility.Logger.Status("Creating image representation of raster " + filename, True)

            If newImage Is Nothing Then newImage = New MapWinGIS.Image

            Dim ext As String = System.IO.Path.GetExtension(filename).ToLower()

            '50 MEG seems to be the breaking point between reading from temp bmp and directly
            If (ext.EndsWith(".tif") OrElse ext.EndsWith(".tiff") OrElse ext.EndsWith(".img")) Then
                'A tiff returning false here likely has a colormap and can be opened 
                'as an image by GDAL -- do so
                'Chris Michaelis March 2009 - If the file is >2GB, force opening it with GDAL
                'and note that we'll force our "coloring scheme" in through tkRaster
                MapWinUtility.Logger.Dbg("Try opening TIFF with GDAL")
                If newImage.Open(filename, MapWinGIS.ImageType.USE_FILE_EXTENSION, False, cb) Then
                    'Push our coloring scheme in - normally this will have no effect for an image, but
                    'iff tkRaster is rendering it, it will indeed get used and ought to be faster than bmp generation                    
                    newImage._pushSchemetkRaster(GrdColorScheme)
                Else
                    'ok, ok, do it the slow way - convert image
                    Dim converter As New MapWinGIS.Utils
                    newImage = converter.GridToImage(newGrid, GrdColorScheme, CType(Me, MapWinGIS.ICallback))
                    Dim imgName As String = IO.Path.ChangeExtension(filename, ".bmp")
                    newImage.Save(imgName, True, MapWinGIS.ImageType.BITMAP_FILE, CType(Me, MapWinGIS.ICallback))
                    'Open with inram=false now that conversion is done:
                    newImage.Close()
                    newImage.Open(imgName, MapWinGIS.ImageType.BITMAP_FILE, False, CType(Me, MapWinGIS.ICallback))
                    newImage.TransparencyColor = GrdColorScheme.NoDataColor
                    ' 04/29/2010 DK --- Transparency Color2 is also set up as transaprent color.==
                    newImage.TransparencyColor2 = GrdColorScheme.NoDataColor
                    '===============
                    'CDM 11/14/05 Set the below to true to enable using the nodatavalue as transparency color
                    newImage.UseTransparencyColor = True
                End If
            ElseIf ext.EndsWith(".bil") Then
                'Open the underlying BIL instead
                MapWinUtility.Logger.Dbg("Open the underlying BIL instead")
                If Not newImage.Open(filename, MapWinGIS.ImageType.USE_FILE_EXTENSION, False, cb) Then
                    Try
                        newImage.Close()
                    Catch
                    End Try
                    newImage = Nothing
                    MapWinUtility.Logger.Msg("An error occurred opening the image. Please try again, and if the problem persists, submit a bug report including sample data at http://bugs.MapWindow.org/. Thank you!", MsgBoxStyle.Exclamation, "Error Opening Image")
                End If
            Else
                'Convert image
                MapWinUtility.Logger.Dbg("Convert image")
                Dim converter As New MapWinGIS.Utils
                newImage = converter.GridToImage(newGrid, GrdColorScheme, CType(Me, MapWinGIS.ICallback))
                Dim imgName As String = IO.Path.ChangeExtension(filename, ".bmp")
                newImage.Save(imgName, True, MapWinGIS.ImageType.BITMAP_FILE, CType(Me, MapWinGIS.ICallback))
                'Open with inram=false now that conversion is done:
                newImage.Close()
                newImage.Open(imgName, MapWinGIS.ImageType.BITMAP_FILE, False, CType(Me, MapWinGIS.ICallback))
                newImage.TransparencyColor = GrdColorScheme.NoDataColor
                ' 04/29/2010 DK --- Transparency Color2 is also set up as transaprent color.==
                newImage.TransparencyColor2 = GrdColorScheme.NoDataColor
                'CDM 11/14/05 Set the below to true to enable using the nodatavalue as transparency color
                newImage.UseTransparencyColor = True
            End If

            ' 4/16/2010 DK - Here, we set up the image resampling mode to imNone
            ' If not, we cannot see the raster layer's cell when we closely zoom in. 
            ' 5/10/2010 DK ==> I was wrong. This functionality is so well sought of that
            ' my client actually requested it recently.

            ' This needs to be
            ' - a setting in the project file, 
            ' - added to the mwsr file, 
            ' - an application settings,
            ' - changeable in the legend control.
            ' See issue report #1691
            'newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imNone
            'newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imNone
            '5/10/2010 DK === Done as requested by Paul Meems.


            ' Lines to integrate grid/image transparency and interpolation method when adding new layers
            Dim imageTransparencyValue As Single
            Dim imageUpsamplingMethod As String
            Dim imageDownsamplingMethod As String
            Dim xmlfilename As String

            xmlfilename = filename.Substring(0, filename.LastIndexOf("."))
            xmlfilename = xmlfilename + ".mwsr"

            ' Image Layer Fill Transparency
            Try
                imageTransparencyValue = CType(ReadLayerPropertyValueFromXML(xmlfilename, "GridColoringScheme", "ImageLayerFillTransparency"), Single)
            Catch ex As Exception
                imageTransparencyValue = 1
            End Try
            newImage.TransparencyPercent = imageTransparencyValue

            ' Image Downsampling method
            Try
                imageDownsamplingMethod = ReadLayerPropertyValueFromXML(xmlfilename, "GridColoringScheme", "ImageDownsamplingMethod")
            Catch ex As Exception
                imageDownsamplingMethod = "None"
            End Try
            Select Case imageDownsamplingMethod.ToLower
                Case "none"
                    newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imNone
                Case "bicubic"
                    newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imBicubic
                Case "bilinear"
                    newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imBilinear
                Case "highqualitybicubic"
                    newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic
                Case "highqualityBilinear"
                    newImage.DownsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBilinear
            End Select

            ' Image Upsampling method
            Try
                imageUpsamplingMethod = ReadLayerPropertyValueFromXML(xmlfilename, "GridColoringScheme", "ImageUpsamplingMethod")
            Catch ex As Exception
                imageUpsamplingMethod = "None"
            End Try

            Select Case imageUpsamplingMethod.ToLower
                Case "none"
                    newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imNone
                Case "bicubic"
                    newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imBicubic
                Case "bilinear"
                    newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imBilinear
                Case "highqualitybicubic"
                    newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic
                Case "highqualitybilinear"
                    newImage.UpsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBilinear
            End Select

        Catch ex As Exception
            Dim e As New Exception("GetImageRep failed processing filename: " + filename + vbCrLf + vbCrLf + "Full Exception:" + vbCrLf + ex.ToString())
            ShowError(e)
            MapWinUtility.Logger.Dbg("Error occurred in GetImageRep: " + ex.ToString())
        End Try

        MapWinUtility.Logger.Dbg("Finished creating image representation of raster " + filename)
        MapWinUtility.Logger.Status("")
    End Sub
#End Region

#Region "ICallback memebers"
    Public Sub ReportProgress(ByVal KeyOfSender As String, ByVal Percent As Integer, ByVal Message As String) Implements MapWinGIS.ICallback.Progress
        ' Paul Meems, May 30 2010
        ' Added try catch. When a plug-in is running on a seperate thread and is also using the progress it throws an error, 
        ' no need for that ;)
        Try
            'loading project is set in xmlProjectFile so it can keep track of entire progress  
            'of the project not every layer
            If m_mismatchTester Is Nothing Then
                'frmMain.StatusBar.ProgressBarValue = Percent
                If String.IsNullOrEmpty(Message) Then
                    MapWinUtility.Logger.Progress(Percent, 100)
                Else
                    MapWinUtility.Logger.Progress(Message, Percent, 100)
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine("Warning in ReportProgress: " & ex.Message)
        End Try
    End Sub

    Public Sub ReportError(ByVal KeyOfSender As String, ByVal ErrorMsg As String) Implements MapWinGIS.ICallback.Error
        g_error = ErrorMsg
    End Sub
#End Region


End Class