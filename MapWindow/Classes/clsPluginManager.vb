'********************************************************************************************************
'File Name: clsPluginManager.vb
'Description: Classes for managing interaction with MapWindow plugins
'These variables provide customization for the MapWindow Application interface.   
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
'5/9/2008 -  Jiri Kadlec - MapMouseMove event - Improved formatting and display of status bar coordinates
'8/28/2008 - Jiri Kadlec - MapMouseMove event - Corrected conversion of coordinates in alternative units
'********************************************************************************************************

Imports System.IO
Imports Microsoft.Win32
Imports System.Drawing

Public Class PluginTracker
    ' NOTE: This class will be merged with Plugins_IBasePlugin once Plugins_IPlugins
    ' is gone -- see notes at top of Plugins_IPlugin. These lists will also be unnecessary,
    ' since this class is a broadcaster to send to both lists of plugins -- but only one list
    ' will be here eventually.
    Friend oldInt As New Plugins_IPlugin
    Friend newInt As New Plugins_IBasePlugin

    Public Function m_ApplicationPlugins() As Hashtable
        Dim n As New Hashtable
        Dim en As IDictionaryEnumerator = oldInt.m_ApplicationPlugins.GetEnumerator()
        While en.MoveNext
            n.Add(en.Key, en.Value)
        End While
        en = newInt.m_ApplicationPlugins.GetEnumerator()
        While en.MoveNext
            n.Add(en.Key, en.Value)
        End While
        Return n
    End Function

    Public Function PluginsList() As Hashtable
        Dim n As New Hashtable
        Dim en As IDictionaryEnumerator = oldInt.m_PluginList.GetEnumerator()
        While en.MoveNext
            n.Add(en.Key, en.Value)
        End While
        en = newInt.m_PluginList.GetEnumerator()
        While en.MoveNext
            n.Add(en.Key, en.Value)
        End While
        Return n
    End Function

    Public Function AddFromFile(ByVal File As String) As Boolean
        Return oldInt.AddFromFile(File) OrElse newInt.AddFromFile(File)
    End Function

    Public Function MapDragFinished(ByVal Bounds As Rectangle) As Boolean
        Return oldInt.MapDragFinished(Bounds) OrElse newInt.MapDragFinished(Bounds)
    End Function

    Public Sub ShowPluginDialog()
        oldInt.ShowPluginDialog()
    End Sub

    Public Sub MapExtentsChanged()
        oldInt.MapExtentsChanged()
        newInt.MapExtentsChanged()
    End Sub

    Public Function LoadedPlugins() As Collection
        Dim n As New Collection
        Dim Plugin As PluginInfo
        Dim ar As Collection = oldInt.LoadedPlugins
        For i As Integer = 1 To ar.Count
            Plugin = CType(frmMain.m_PluginManager.PluginsList(MapWinUtility.PluginManagementTools.GenerateKey(ar(i).GetType())), Interfaces.PluginInfo)
            n.Add(ar(i), Plugin.Key)
        Next
        ar = newInt.LoadedPlugins
        For i As Integer = 1 To ar.Count
            Plugin = CType(frmMain.m_PluginManager.PluginsList(MapWinUtility.PluginManagementTools.GenerateKey(ar(i).GetType())), Interfaces.PluginInfo)
            n.Add(ar(i), Plugin.Key)
        Next
        Return n
    End Function

    Public Sub LoadPlugins()
        oldInt.LoadPlugins()
        newInt.LoadPlugins()
    End Sub

    Public Sub LayersCleared()
        oldInt.LayersCleared()
        newInt.LayersCleared()
    End Sub

    Public Sub LayerRemoved(ByVal LayerHandle As Integer)
        oldInt.LayerRemoved(LayerHandle)
        newInt.LayerRemoved(LayerHandle)
    End Sub

    <CLSCompliant(False)> _
    Public Sub LayersAdded(ByVal Handle() As MapWindow.Interfaces.Layer)
        oldInt.LayersAdded(Handle)
        newInt.LayersAdded(Handle)
    End Sub

    Public Sub UnloadApplicationPlugins()
        oldInt.UnloadApplicationPlugins()
        newInt.UnloadApplicationPlugins()
    End Sub

    Public Function StartPlugin(ByVal Key As String) As Boolean
        Dim retval As Boolean = oldInt.StartPlugin(Key) OrElse newInt.StartPlugin(Key)
        If retval = False Then 'Neither one had it
            MapWinUtility.Logger.Dbg("Warning:" + vbCrLf + "A plugin may be missing. This plugin is identified as: " & Key, MsgBoxStyle.OkOnly)
        End If
        Return retval
    End Function

    Public Sub LoadApplicationPlugins(ByVal ApplicationPluginPath As String)
        If Not IO.Directory.Exists(ApplicationPluginPath) Then
            ApplicationPluginPath = BinFolder + "\ApplicationPlugins"
            If Not IO.Directory.Exists(ApplicationPluginPath) Then
                MapWinUtility.Logger.Msg("ApplicationPlugInPath " & ApplicationPluginPath & " not found, edit default.mwcfg to specify location")
            End If
        End If
        oldInt.LoadApplicationPlugins(ApplicationPluginPath)
        newInt.LoadApplicationPlugins(ApplicationPluginPath)
    End Sub

    Public Sub UnloadAll()
        MapWinUtility.Logger.Dbg("Unload all plugins")
        oldInt.UnloadAll()
        newInt.UnloadAll()
    End Sub

    Public Sub ProjectLoading(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)
        oldInt.ProjectLoading(Key, ProjectFile, SettingsString)
        newInt.ProjectLoading(Key, ProjectFile, SettingsString)
    End Sub

    Public Sub ProjectSaving(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)
        oldInt.ProjectSaving(Key, ProjectFile, SettingsString)
        newInt.ProjectSaving(Key, ProjectFile, SettingsString)
    End Sub

    <CLSCompliant(False)> _
    Public Function LegendMouseDown(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation) As Boolean
        Return oldInt.LegendMouseDown(Handle, Button, Location) OrElse newInt.LegendMouseDown(Handle, Button, Location)
    End Function

    <CLSCompliant(False)> _
    Public Function LegendMouseUp(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation) As Boolean
        Return oldInt.LegendMouseUp(Handle, Button, Location) OrElse newInt.LegendMouseDown(Handle, Button, Location)
    End Function

    Public Function MapMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Return oldInt.MapMouseDown(Button, Shift, X, Y) OrElse newInt.MapMouseDown(Button, Shift, X, Y)
    End Function

    Public Function MapMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Return oldInt.MapMouseUp(Button, Shift, X, Y) OrElse newInt.MapMouseUp(Button, Shift, X, Y)
    End Function

    <CLSCompliant(False)> _
    Public Sub ShapesSelected(ByVal Handle As Integer, ByVal SelectInfo As MapWindow.Interfaces.SelectInfo)
        oldInt.ShapesSelected(Handle, SelectInfo)
        newInt.ShapesSelected(Handle, SelectInfo)
    End Sub

    Public Function ItemClicked(ByVal ItemName As String)
        Return oldInt.ItemClicked(ItemName) OrElse newInt.ItemClicked(ItemName)
    End Function

    Public Sub StopPlugin(ByVal Key As String)
        oldInt.StopPlugin(Key)
        newInt.StopPlugin(Key)
    End Sub

    Public Function PluginIsLoaded(ByVal Key As String) As Boolean
        Return oldInt.PluginIsLoaded(Key) OrElse newInt.PluginIsLoaded(Key)
    End Function

    Public Function ContainsKey(ByVal c As Collection, ByVal key As String) As Boolean
        Return oldInt.ContainsKey(c, key) OrElse newInt.ContainsKey(c, key)
    End Function

    Public Sub LayerSelected(ByVal LayerHandle As Integer)
        oldInt.LayerSelected(LayerHandle)
        newInt.LayerSelected(LayerHandle)
    End Sub

    <CLSCompliant(False)> _
    Public Function LegendDoubleClick(ByVal LayerHandle As Integer, ByVal Location As MapWindow.Interfaces.ClickLocation) As Boolean
        Return oldInt.LegendDoubleClick(LayerHandle, Location) OrElse newInt.LegendDoubleClick(LayerHandle, Location)
    End Function

    Public Function MapMouseMove(ByVal ScreenX As Integer, ByVal ScreenY As Integer) As Boolean
        Return oldInt.MapMouseMove(ScreenX, ScreenY) OrElse newInt.MapMouseMove(ScreenX, ScreenY)
    End Function
End Class

Public Class Plugins_IPlugin
    Implements IEnumerable
    Implements Interfaces.Plugins
    ' NOTE: This class will go away once the old interface is gone, after people have had a chance
    ' to switch to the 6.0 interface(s) and once they've been finished.
    '
    ' This class supports MapWindow.Interfaces.Plugins,
    ' but is very similar to CondensedPlugins which supports MapWindow.Interfaces.PluginDetails.
    ' The original interface (Plugins) cannot be changed due to backward compatibility requirement,
    ' thus a new class was created for interacting with the condensed plugins which share the same 
    ' IBasePlugin base instead of simply making IPlugin inherit from IBasePlugin.

    Friend Class PluginEnumerator
        Implements System.Collections.IEnumerator

        Private m_Collection As MapWindow.Interfaces.Plugins
        Private m_Idx As Integer = -1

        Public Sub New(ByVal inp As MapWindow.Interfaces.Plugins)
            m_Collection = inp
            m_Idx = -1
        End Sub

        Public Sub Reset() Implements IEnumerator.Reset
            m_Idx = -1
        End Sub

        Public ReadOnly Property Current() As Object Implements IEnumerator.Current
            Get
                Return m_Collection.Item(m_Idx)
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            m_Idx += 1

            If m_Idx >= m_Collection.Count Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return New PluginEnumerator(Me)
    End Function

    Public m_PluginList As Hashtable
    Public m_LoadedPlugins As Collection
    Public m_ApplicationPlugins As Hashtable
    Private m_PluginFolder As String
    Private m_dlg As PluginsForm
    Private m_Locked As Boolean
    'Private m_ProjectedCoordToolbar As Windows.Forms.ToolStripStatusLabel = Nothing
    'Private m_AlternateCoordToolbar As Windows.Forms.ToolStripStatusLabel = Nothing
    'Private m_ProjectedCoordFormat As String
    'Private m_AlternateCoordFormat As String
    'Private m_UOMOrig As String
    'Private m_UOMDest As String

    Public Sub New()
        MyBase.New()
        m_PluginList = New Hashtable()
        m_PluginList.Clear()
        m_LoadedPlugins = New Collection()
        m_ApplicationPlugins = New Hashtable()
        m_dlg = New PluginsForm()
    End Sub

    Protected Overrides Sub Finalize()
        m_dlg = Nothing
        m_PluginList.Clear()
        m_PluginList = Nothing
        m_LoadedPlugins = Nothing
        m_ApplicationPlugins = Nothing
        MyBase.Finalize()
    End Sub

    Friend Function Contains(ByVal Key As String) As Boolean
        Return m_PluginList.ContainsKey(Key)
    End Function

    Public Sub Clear() Implements Interfaces.Plugins.Clear
        m_PluginList.Clear()
    End Sub

    Friend Function LoadPlugins() As Boolean
        If m_Locked Then Exit Function

        PluginFolder = App.Path & "\Plugins"
        Dim PotentialPlugins As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(PluginFolder)
        For i As Integer = 0 To PotentialPlugins.Count - 1
            Dim info As New PluginInfo()

            Try
                If PotentialPlugins(i).IndexOf("RemoveMe-Script") > 0 Then
                    Kill(PotentialPlugins(i))
                ElseIf CStr(PotentialPlugins(i)).Contains("mwLabeler.dll") Or CStr(PotentialPlugins(i)).Contains("mwIdentifier.dll") Then
                    'Nothing
                    'CDM June 7 2006 - Skip over any old plug-ins that are still lingering in 'Plugins' that have been moved to 'ApplicationPlugins'
                ElseIf info.Init(PotentialPlugins(i), GetType(Interfaces.IPlugin).GUID) = True Then
                    If m_PluginList.ContainsKey(info.Key) = False Then
                        m_PluginList.Add(info.Key, info)
                    Else
                        'Chris Michaelis, May 18 2006, for BugZilla 171
                        Dim dupfile As String = CType(m_PluginList(info.Key), PluginInfo).FileName
                        If Not dupfile.ToLower() = PotentialPlugins(i).ToLower() Then
                            MapWinUtility.Logger.Msg("Warning: A duplicate plug-in has been detected." + vbCrLf + vbCrLf + _
                            "The loaded plug-in is: " + dupfile + vbCrLf + _
                            "The duplicate that was skipped: " + PotentialPlugins(i), MsgBoxStyle.Information, "Duplicate Plug-in Detected")
                        End If
                    End If
                End If

            Catch ex As Exception
                ' Keep it quiet because the MapWindow is loading
                MapWinUtility.Logger.Dbg(ex.ToString())
            End Try
        Next
    End Function

    Public Function AddFromFile(ByVal Path As String) As Boolean Implements Interfaces.Plugins.AddFromFile
        If m_Locked Then Exit Function
        Dim info As New PluginInfo()
        Dim retval As Boolean
        Try
            retval = info.Init(Path, GetType(Interfaces.IPlugin).GUID)
            If retval = True Then
                If m_PluginList.ContainsKey(info.Key) = False Then
                    m_PluginList.Add(info.Key, info)
                    modMain.frmMain.SynchPluginMenu()
                Else
                    'Chris Michaelis, May 18 2006, for BugZilla 171
                    Dim dupfile As String = CType(m_PluginList(info.Key), PluginInfo).FileName
                    If Not dupfile.ToLower() = Path.ToLower() Then
                        MapWinUtility.Logger.Msg("Warning: A duplicate plug-in has been detected." + vbCrLf + vbCrLf + _
                        "The loaded plug-in is: " + dupfile + vbCrLf + _
                        "The duplicate that was skipped: " + Path, MsgBoxStyle.Information, "Duplicate Plug-in Detected")
                    End If
                End If
            End If
            Return retval
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    Public Function AddFromDir(ByVal Path As String) As Boolean Implements Interfaces.Plugins.AddFromDir
        If m_Locked Then Exit Function
        Dim ar As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(Path)
        For Each s As String In ar
            AddFromFile(s)
        Next
    End Function

    Public Function StartPlugin(ByVal Key As String) As Boolean Implements Interfaces.Plugins.StartPlugin
        Dim Plugin As Interfaces.IPlugin
        Dim info As PluginInfo

        If m_Locked Then Exit Function

        If ContainsKey(m_LoadedPlugins, Key) Then Return True

        If m_PluginList.ContainsKey(Key) Then
            Try
                info = CType(m_PluginList(Key), PluginInfo)
                Plugin = MapWinUtility.PluginManagementTools.CreatePluginObject(info.FileName, info.CreateString)
                If Plugin Is Nothing Then Return False

                m_LoadedPlugins.Add(Plugin, info.Key)
                Plugin.Initialize(frmMain, frmMain.Handle.ToInt32)
                frmMain.SynchPluginMenu()
                Return True
            Catch ex As Exception
                g_error = "Error in plugin:  " & ex.Message
                ShowError(ex)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Sub StopPlugin(ByVal Key As String) Implements Interfaces.Plugins.StopPlugin
        If m_Locked Then Exit Sub
        Try
            If ContainsKey(m_LoadedPlugins, Key) Then
                Dim Plugin As Interfaces.IPlugin
                If Not m_LoadedPlugins(Key) Is Nothing Then
                    Plugin = CType(m_LoadedPlugins(Key), Interfaces.IPlugin)
                    If Not Plugin Is Nothing Then
                        Try
                            Plugin.Terminate()
                        Catch ex As Exception
                            MapWinUtility.Logger.Msg("Warning: The plugin '" + Key + "' raised an error in its Terminate() function." + vbCrLf + vbCrLf + "The full details appear in the application log (if enabled).", MsgBoxStyle.Exclamation, "Plug-in Error Warning")
                            MapWinUtility.Logger.Dbg("Plugin Exception in " + Key + ": " + ex.ToString())
                            'Proceed
                        End Try
                        Plugin = Nothing
                    End If
                End If
                m_LoadedPlugins.Remove(Key)
                frmMain.SynchPluginMenu()
            End If

            frmMain.m_Menu.MenuTrackerRemoveIfLastOwner(Key)
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Public ReadOnly Property Count() As Integer Implements Interfaces.Plugins.Count
        Get
            Return m_PluginList.Count
        End Get
    End Property

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal Index As Integer) As MapWindow.Interfaces.IPlugin Implements MapWindow.Interfaces.Plugins.Item
        Get
            If Index <= m_LoadedPlugins.Count AndAlso Index > 0 Then  'collections are 1-based
                Return CType(m_LoadedPlugins(Index), Interfaces.IPlugin)
            End If

            Return Nothing
        End Get
    End Property

    Public Sub Remove(ByVal IndexOrKey As Object) Implements Interfaces.Plugins.Remove
        If m_Locked Then Exit Sub
        Try
            Select Case LCase(TypeName(IndexOrKey))
                Case "string", "long", "integer", "short"
                    If ContainsKey(m_LoadedPlugins, IndexOrKey) Then StopPlugin(CStr(IndexOrKey))
                    m_PluginList.Remove(IndexOrKey)
                Case Else
                    g_error = "Error: Invalid 'IndexOrKey' parameter type. Must be 'String', 'Integer', 'Long', or 'Short'"
            End Select
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Friend ReadOnly Property LoadedPlugins() As Collection
        Get
            Return m_LoadedPlugins
        End Get
    End Property

    Friend ReadOnly Property PluginsList() As Hashtable
        Get
            Return m_PluginList
        End Get
    End Property

    Public Property PluginFolder() As String Implements Interfaces.Plugins.PluginFolder
        Get
            Return m_PluginFolder
        End Get
        Set(ByVal Value As String)
            m_PluginFolder = Value
        End Set
    End Property

    Public Function PluginIsLoaded(ByVal Key As String) As Boolean Implements Interfaces.Plugins.PluginIsLoaded
        Return (ContainsKey(m_LoadedPlugins, Key) Or m_ApplicationPlugins.ContainsKey(Key))
    End Function

    Public Sub ShowPluginDialog() Implements Interfaces.Plugins.ShowPluginDialog
        If Not m_Locked Then m_dlg.ShowDialog()
    End Sub

    Friend Sub UnloadAll()
        Try
            For i As Integer = m_LoadedPlugins.Count To 1 Step -1
                Dim Plugin As Interfaces.PluginInfo = CType(frmMain.m_PluginManager.PluginsList(MapWinUtility.PluginManagementTools.GenerateKey(m_LoadedPlugins(i).GetType())), Interfaces.PluginInfo)
                StopPlugin(Plugin.Key)
            Next
            m_LoadedPlugins = New Collection
            GC.Collect()
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Friend Sub UnloadApplicationPlugins()
        Dim plugin As Interfaces.IPlugin

        Try
            Dim item As DictionaryEntry
            For Each item In m_ApplicationPlugins
                plugin = CType(item.Value, Interfaces.IPlugin)
                plugin.Terminate()
                plugin = Nothing
            Next

            m_ApplicationPlugins.Clear()
            m_ApplicationPlugins = New Hashtable
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub
    '--------------------------------------------------------------------------------------------
    'Event generators
    '--------------------------------------------------------------------------------------------
    Friend Function LegendDoubleClick(ByVal Handle As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LegendDoubleClick(Handle, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LegendDoubleClick(Handle, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function LegendMouseDown(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LegendMouseDown(Handle, Button, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LegendMouseDown(Handle, Button, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function LegendMouseUp(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LegendMouseUp(Handle, Button, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LegendMouseUp(Handle, Button, Location, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Sub MapExtentsChanged()
        Dim tPlugin As Interfaces.IPlugin

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.MapExtentsChanged()
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.MapExtentsChanged()
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Function MapMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.MapMouseDown(Button, Shift, x, y, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.MapMouseDown(Button, Shift, x, y, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function MapMouseMove(ByVal ScreenX As Integer, ByVal ScreenY As Integer) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        ''Update the status bar items if necessary, depending on the project settings.
        'Try
        '    Dim x, y As Double

        '    modMain.frmMain.MapMain.PixelToProj(ScreenX, ScreenY, x, y)

        '    'Provide lat/long (unprojected)
        '    'If no projection is provided, cannot do this.
        '    Try

        '        If Not (modMain.ProjInfo.ProjectProjection = "" Or modMain.ProjInfo.m_MapUnits = "Lat/Long") Then
        '            'If it's not set to anything and the map units are not lat/long, default to lat/long.
        '            'TODO : shouldn't we rather show 'unknown units?'
        '            If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" And Not modMain.ProjInfo.m_MapUnits = "" And Not modMain.ProjInfo.m_MapUnits = "Lat/Long" Then
        '                modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long"
        '            End If

        '            'status bar for coordinates in alternate units
        '            If Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "" And Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" Then
        '                If m_AlternateCoordToolbar Is Nothing Then
        '                    m_AlternateCoordToolbar = New Windows.Forms.ToolStripStatusLabel
        '                    m_AlternateCoordToolbar.AutoSize = StatusBarPanelAutoSize.Contents
        '                    m_AlternateCoordToolbar.BorderStyle = Border3DStyle.Etched
        '                    m_AlternateCoordToolbar.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Right
        '                    frmMain.StatusBar1.Items.Insert(1, m_AlternateCoordToolbar)
        '                End If


        '                If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long" Then
        '                    'alternate units are in Lat/Long (decimal degrees) - reproject the point
        '                    If (Not MapWinGeoProc.SpatialReference.ProjectPoint(x, y, modMain.ProjInfo.ProjectProjection, "+proj=latlong +ellps=WGS84 +datum=WGS84")) Then
        '                        MapWinUtility.Logger.Dbg("DEBUG: " + MapWinGeoProc.Error.GetLastErrorMsg())
        '                    Else
        '                        m_AlternateCoordToolbar.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals, modMain.ProjInfo.StatusBarAlternateCoordsUseCommas, "Lat/Long")
        '                    End If

        '                Else
        '                    'else do conversion from units to units
        '                    Dim UOMOrig As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Meters
        '                    Dim UOMDest As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Kilometers

        '                    '08/28/2008 Jiri Kadlec - use the new unit conversion function from MapWinGeoProc
        '                    UOMOrig = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnits)
        '                    UOMDest = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnitsAlternate)

        '                    Dim newText As String = ""
        '                    Dim numDecimals As Integer = modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals
        '                    Dim useCommas As Boolean = modMain.ProjInfo.StatusBarAlternateCoordsUseCommas
        '                    Try
        '                        Dim strX, strY As Double
        '                        strX = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, x)
        '                        strY = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, y)

        '                        newText = FormatCoords(strX, strY, numDecimals, useCommas, UOMDest.ToString())

        '                    Catch ex As Exception
        '                        newText = FormatCoords(x, y, numDecimals, useCommas, UOMOrig.ToString())
        '                    End Try
        '                    m_AlternateCoordToolbar.Text = newText

        '                End If
        '            ElseIf Not m_AlternateCoordToolbar Is Nothing Then
        '                frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '                m_AlternateCoordToolbar = Nothing
        '            End If
        '        Else 'Can't do it - make sure the status bar panel isn't there
        '            If Not m_AlternateCoordToolbar Is Nothing Then
        '                frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '                m_AlternateCoordToolbar = Nothing
        '            End If
        '        End If
        '    Catch ex As Exception
        '        'Very likely a projection error.
        '        'Just display the coords.
        '        If Not m_AlternateCoordToolbar Is Nothing Then
        '            frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '            m_AlternateCoordToolbar = Nothing
        '        End If
        '    End Try

        '    ' Re-get the coords...
        '    modMain.frmMain.MapMain.PixelToProj(ScreenX, ScreenY, x, y)

        '    'Provide coordinate in projected system (may be unknown or just pixels in the case of images, etc)
        '    If (modMain.ProjInfo.ShowStatusBarCoords_Projected) Then
        '        If m_ProjectedCoordToolbar Is Nothing Then
        '            m_ProjectedCoordToolbar = New Windows.Forms.ToolStripStatusLabel
        '            m_ProjectedCoordToolbar.AutoSize = StatusBarPanelAutoSize.Contents
        '            m_ProjectedCoordToolbar.BorderStyle = Border3DStyle.Etched
        '            m_ProjectedCoordToolbar.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Right
        '            frmMain.StatusBar1.Items.Insert(1, m_ProjectedCoordToolbar)
        '        End If
        '        'Dim cvter As New ScaleBarUtility

        '        m_ProjectedCoordToolbar.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarCoordsNumDecimals, modMain.ProjInfo.StatusBarCoordsUseCommas, modMain.frmMain.Project.MapUnits)

        '    ElseIf Not m_ProjectedCoordToolbar Is Nothing Then
        '        frmMain.StatusBar1.Items.Remove(m_ProjectedCoordToolbar)
        '        m_ProjectedCoordToolbar = Nothing
        '    End If
        'Catch ex As Exception
        '    System.Diagnostics.Debug.WriteLine(ex.ToString())
        'End Try

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)

                tPlugin.MapMouseMove(ScreenX, ScreenY, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try

                tPlugin.MapMouseMove(ScreenX, ScreenY, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Private Function FormatCoords(ByVal x As Double, ByVal y As Double, ByVal decimals As Integer, ByVal useCommas As String, ByVal units As String) As String
        '5/9/2008 Jiri Kadlec - this function is used by MapMouseMove() to format the coordinates in
        'the MW status bar.
        Dim nf As String 'the number formatting string

        If useCommas = True Then
            nf = "N" + decimals.ToString
        Else
            nf = "F" + decimals.ToString
        End If
        If units = "Lat/Long" Then
            Return String.Format("Lat: {0} Long: {1}", y.ToString(nf), x.ToString(nf))
        Else
            Return String.Format("X: {0} Y: {1} {2}", x.ToString(nf), y.ToString(nf), units)
        End If
    End Function

    Friend Function MapMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.MapMouseUp(Button, Shift, x, y, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.MapMouseUp(Button, Shift, x, y, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function MapDragFinished(ByVal Bounds As Rectangle) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.MapDragFinished(Bounds, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.MapDragFinished(Bounds, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    '' Layer Events
    Friend Sub LayersAdded(ByVal Handle As MapWindow.Interfaces.Layer())
        Dim tPlugin As Interfaces.IPlugin

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LayersAdded(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LayersAdded(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Sub LayerRemoved(ByVal Handle As Integer)
        Dim tPlugin As Interfaces.IPlugin

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LayerRemoved(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LayerRemoved(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Sub LayerSelected(ByVal Handle As Integer)
        Dim tPlugin As Interfaces.IPlugin

        '3 Nov. 2010 Paul Meems - No need to fire when the layer is invalid:
        If Handle = -1 Then Return

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LayerSelected(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LayerSelected(Handle)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Sub LayersCleared()

        Dim tPlugin As Interfaces.IPlugin
        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.LayersCleared()
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.LayersCleared()
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Sub ShapesSelected(ByVal Handle As Integer, ByVal SelectInfo As MapWindow.SelectInfo)
        'frmMain.Menus("mnuClearSelectedShapes").Enabled = (SelectInfo.NumSelected > 0)
        frmMain.UpdateButtons()

        Dim tPlugin As Interfaces.IPlugin

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.ShapesSelected(Handle, SelectInfo)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.ShapesSelected(Handle, SelectInfo)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Sub

    Friend Function ItemClicked(ByVal ItemName As String) As Boolean
        'Called by the MapWindow GUI when a user clicks a toolbar button that was added by a plugin.
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.ItemClicked(ItemName, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                ' tPlugin = CType(Item.Value, Interfaces.IPlugin)
                tPlugin.ItemClicked(ItemName, handled)
                'if one of the plugins returns "handled=true" then we stop sending the click to other plugins. 
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                MapWinUtility.Logger.Dbg("Exception in plugin '" & tPlugin.Name & "': " & ex.Message)
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function Message(ByVal msg As String) As Boolean
        Dim tPlugin As Interfaces.IPlugin
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                tPlugin = CType(item.Value, Interfaces.IPlugin)
                tPlugin.Message(msg, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each tPlugin In m_LoadedPlugins
            Try
                tPlugin.Message(msg, handled)
                If handled Then Return True
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Sub ProjectLoading(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)

        Dim tPlugin As Interfaces.IPlugin = Nothing

        Try
            If m_ApplicationPlugins.Contains(Key) = False Then Exit Try
            tPlugin = CType(m_ApplicationPlugins(Key), Interfaces.IPlugin)
            tPlugin.ProjectLoading(ProjectFile, SettingsString)
            tPlugin = Nothing
        Catch ex As Exception
            g_error = ex.Message
            If Not tPlugin Is Nothing Then
                Dim new_ex As New System.Exception("An error occured in the plugin named: " & tPlugin.Name, ex)
                ShowError(new_ex)
            Else
                ShowError(ex)
            End If
        End Try

        Try
            If ContainsKey(m_LoadedPlugins, Key) = False Then Exit Sub
            tPlugin = CType(m_LoadedPlugins(Key), Interfaces.IPlugin)
            tPlugin.ProjectLoading(ProjectFile, SettingsString)
            tPlugin = Nothing
        Catch ex As Exception
            g_error = ex.Message
            If Not tPlugin Is Nothing Then
                Dim new_ex As New System.Exception("An error occured in the plugin named: " & tPlugin.Name, ex)
                ShowError(new_ex)
            Else
                ShowError(ex)
            End If
        End Try
    End Sub

    Friend Sub ProjectSaving(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)
        Dim tPlugin As Interfaces.IPlugin

        Try
            If m_ApplicationPlugins.Contains(Key) = False Then Exit Try
            tPlugin = CType(m_ApplicationPlugins(Key), Interfaces.IPlugin)
            tPlugin.ProjectSaving(ProjectFile, SettingsString)
            tPlugin = Nothing
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try

        Try
            If ContainsKey(m_LoadedPlugins, Key) = False Then Exit Sub
            tPlugin = CType(m_LoadedPlugins(Key), Interfaces.IPlugin)
            tPlugin.ProjectSaving(ProjectFile, SettingsString)
            tPlugin = Nothing
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    <CLSCompliant(False)> _
    Public Function LoadFromObject(ByVal Plugin As Interfaces.IPlugin, ByVal PluginKey As String) As Boolean Implements Interfaces.Plugins.LoadFromObject
        Try
            If Plugin Is Nothing Then
                g_error = "LoadFromObject failed.  The parameter 'Plugin' was not set."
                Return False
            Else
                If ContainsKey(m_LoadedPlugins, PluginKey) Then
                    g_error = "Key already exists!  Cannot load plugins with duplicate keys."
                    Return False
                Else
                    '3/1/2005 dpa - this check isn't necessary now.
                    'If IsAuthorizedPlugin(Plugin.Author, Plugin.SerialNumber) = False Then
                    '    Dim nag As New frmNag(Plugin.Name)
                    '    nag.ShowDialog()
                    'End If
                    Plugin.Initialize(frmMain, frmMain.Handle.ToInt32)
                    Plugin.ProjectLoading(ProjInfo.ProjectFileName, "")
                    m_LoadedPlugins.Add(Plugin, PluginKey)
                    Return True
                End If
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    <CLSCompliant(False)> _
    Public Function LoadFromObject(ByVal Plugin As Interfaces.IPlugin, ByVal PluginKey As String, ByVal SettingsString As String) As Boolean Implements Interfaces.Plugins.LoadFromObject
        Try
            If Plugin Is Nothing Then
                g_error = "LoadFromObject failed.  The parameter 'Plugin' was not set."
                Return False
            Else
                If ContainsKey(m_LoadedPlugins, PluginKey) Then
                    g_error = "Key already exists!  Cannot load plugins with duplicate keys."
                    Return False
                Else
                    '3/1/2005 dpa - this check isn't necessary now.
                    'If IsAuthorizedPlugin(Plugin.Author, Plugin.SerialNumber) = False Then
                    '    Dim nag As New frmNag(Plugin.Name)
                    '    nag.ShowDialog()
                    'End If
                    Plugin.Initialize(frmMain, frmMain.Handle.ToInt32)
                    Plugin.ProjectLoading(ProjInfo.ProjectFileName, SettingsString)
                    m_LoadedPlugins.Add(Plugin, PluginKey)
                    Return True
                End If
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    Friend Function ContainsKey(ByVal c As Collection, ByVal key As Object) As Boolean
        '02/20/08 LCW: use direct function to avoid time-consuming exceptions
        Return c.Contains(key)
        'Dim o As Object = Nothing
        'Try
        '    o = c(key)
        '    Return True
        'Catch
        '    Return False
        'End Try
    End Function

    Public Sub BroadcastMessage(ByVal Message As String) Implements MapWindow.Interfaces.Plugins.BroadcastMessage
        'CDM 4/24/2006 - This is a simplified version of Message, it just doesn't have a
        '"handled" return value consideration. Call the one function, to reduce duplication of code.

        Me.Message(Message)

        'Dim tPlugin As Interfaces.IPlugin
        'Dim handled As Boolean = False

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.Message(Message, handled)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Public Function GetPluginKey(ByVal PluginName As String) As String Implements Interfaces.Plugins.GetPluginKey
        'Returns the plugin key associated with a plugin name.  The plugin name is the name that is displayed in the 
        'Plugins menu.
        'dpa 1/25/2005
        Dim info As PluginInfo
        Dim obj As DictionaryEntry

        For Each obj In m_PluginList
            info = CType(obj.Value, PluginInfo)
            If info.Name = PluginName Then
                Return info.Key
            End If
        Next
        Return ""
    End Function

    Friend Sub LoadApplicationPlugins(ByVal ApplicationPluginPath As String)
        Dim arr As New ArrayList

        Try
            If ApplicationPluginPath = "" Then Return
            If System.IO.Directory.Exists(ApplicationPluginPath) = False Then Return

            Dim PotentialPlugins As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(ApplicationPluginPath)
            Dim info As PluginInfo
            Dim plugin As Interfaces.IPlugin
            Dim pluginName As String
            For Each pluginName In PotentialPlugins
                Try
                    info = New PluginInfo
                    If info.Init(pluginName, GetType(Interfaces.IPlugin).GUID) Then
                        If Not info.Key Is Nothing AndAlso Not m_ApplicationPlugins.ContainsKey(info.Key) Then
                            plugin = MapWinUtility.PluginManagementTools.CreatePluginObject(pluginName, info.CreateString)
                            If Not plugin Is Nothing Then
                                m_ApplicationPlugins.Add(info.Key, plugin)
                                plugin.Initialize(frmMain, frmMain.Handle.ToInt32)
                            End If
                        End If
                    End If
                Catch e As Exception
                    ' There was an error in this one plugin.  Keep going on.
                    MapWinUtility.Logger.Dbg(e.ToString())
                Finally
                    '2/20/08 LCW: to see how often plugins are being loaded
                    '    Debug.Print("--->LoadApplicationPlugins: " & pluginName)
                End Try
            Next
        Catch e As Exception
            MapWinUtility.Logger.Dbg(e.ToString())
        End Try
    End Sub
End Class

Public Class Plugins_IBasePlugin
    Implements PluginInterfaces.PluginTracker
    ' This class supports MapWindow.Interfaces.Plugins,
    ' but is very similar to CondensedPlugins which supports MapWindow.Interfaces.PluginDetails.
    ' The original interface (Plugins) cannot be changed due to backward compatibility requirement,
    ' thus a new class for interacting with the condensed plugins which share the same IBasePlugin base
    ' instead of simply making IPlugin inherit from IBasePlugin.

    ' This new class also takes the responsibility of listing the loaded plugins sorted
    ' by their plug-in type and tracking these consensed additional plug-in types.

    Public m_PluginList As Hashtable
    Public m_LoadedPlugins As Collection
    Public m_ApplicationPlugins As Hashtable
    Private m_PluginFolder As String
    Private m_dlg As PluginsForm
    Private m_Locked As Boolean
    Private m_ProjectedCoordToolbar As Windows.Forms.StatusBarPanel = Nothing
    Private m_AlternateCoordToolbar As Windows.Forms.StatusBarPanel = Nothing

    Public Sub New()
        MyBase.New()
        m_PluginList = New Hashtable()
        m_PluginList.Clear()
        m_LoadedPlugins = New Collection()
        m_ApplicationPlugins = New Hashtable()
        m_dlg = New PluginsForm()
    End Sub

    Protected Overrides Sub Finalize()
        m_dlg = Nothing
        m_PluginList.Clear()
        m_PluginList = Nothing
        m_LoadedPlugins = Nothing
        m_ApplicationPlugins = Nothing
        MyBase.Finalize()
    End Sub

    Friend Function Contains(ByVal Key As String) As Boolean
        Return m_PluginList.ContainsKey(Key)
    End Function

    Public Sub Clear() Implements PluginInterfaces.PluginTracker.Clear
        m_PluginList.Clear()
    End Sub

    Public Function AddFromDir(ByVal Path As String) As Boolean Implements PluginInterfaces.PluginTracker.AddFromDir
        If m_Locked Then Exit Function
        Dim ar As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(Path)
        For Each s As String In ar
            AddFromFile(s)
        Next
    End Function

    Friend Function LoadPlugins() As Boolean
        If m_Locked Then Exit Function

        PluginFolder = App.Path & "\Plugins"
        Dim PotentialPlugins As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(PluginFolder)
        For i As Integer = 0 To PotentialPlugins.Count - 1
            Dim info As New PluginInfo()

            Try
                If PotentialPlugins(i).IndexOf("RemoveMe-Script") > 0 Then
                    Kill(PotentialPlugins(i))
                ElseIf info.Init(PotentialPlugins(i), GetType(PluginInterfaces.IBasePlugin).GUID) = True Then
                    If m_PluginList.ContainsKey(info.Key) = False Then
                        m_PluginList.Add(info.Key, info)
                    Else
                        'Chris Michaelis, May 18 2006, for BugZilla 171
                        Dim dupfile As String = CType(m_PluginList(info.Key), PluginInfo).FileName
                        If Not dupfile.ToLower() = PotentialPlugins(i).ToLower() Then
                            MapWinUtility.Logger.Msg("Warning: A duplicate plug-in has been detected." + vbCrLf + vbCrLf + _
                            "The loaded plug-in is: " + dupfile + vbCrLf + _
                            "The duplicate that was skipped: " + PotentialPlugins(i), MsgBoxStyle.Information, "Duplicate Plug-in Detected")
                        End If
                    End If
                End If

            Catch ex As Exception
                ' Keep it quiet because the MapWindow is loading
                MapWinUtility.Logger.Dbg(ex.ToString())
            End Try
        Next
    End Function

    Public Function AddFromFile(ByVal Path As String) As Boolean Implements PluginInterfaces.PluginTracker.AddFromFile
        If m_Locked Then Exit Function
        Dim info As New PluginInfo()
        Dim retval As Boolean
        Try
            retval = info.Init(Path, GetType(PluginInterfaces.IBasePlugin).GUID)
            If retval = True Then 'if still true
                If m_PluginList.ContainsKey(info.Key) = False Then
                    m_PluginList.Add(info.Key, info)
                    modMain.frmMain.SynchPluginMenu()
                Else
                    'Chris Michaelis, May 18 2006, for BugZilla 171
                    Dim dupfile As String = CType(m_PluginList(info.Key), PluginInfo).FileName
                    If Not dupfile.ToLower() = Path.ToLower() Then
                        MapWinUtility.Logger.Msg("Warning: A duplicate plug-in has been detected." + vbCrLf + vbCrLf + _
                        "The loaded plug-in is: " + dupfile + vbCrLf + _
                        "The duplicate that was skipped: " + Path, MsgBoxStyle.Information, "Duplicate Plug-in Detected")
                    End If
                End If
            End If
            Return retval
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    Public Function StartPlugin(ByVal Key As String) As Boolean Implements PluginInterfaces.PluginTracker.StartPlugin
        Dim Plugin As PluginInterfaces.IBasePlugin
        Dim info As PluginInfo

        If m_Locked Then Exit Function
        If ContainsKey(m_LoadedPlugins, Key) Then Return True
        If m_PluginList.ContainsKey(Key) Then
            Try
                info = CType(m_PluginList(Key), PluginInfo)
                Plugin = MapWinUtility.PluginManagementTools.CreatePluginObject(info.FileName, info.CreateString)
                If Plugin Is Nothing Then
                    Return False
                Else
                    m_LoadedPlugins.Add(Plugin, info.Key)
                    frmMain.SynchPluginMenu()

                    'EVERY POSSIBLE TYPE of plug-in must call initialize here as appropriate!
                    Plugin.Initialize() 'This form of initialize is guaranteed to be there if the plugin loaded (IBasePlugin)
                    If TypeOf Plugin Is PluginInterfaces.IMapWinGUI Then CType(Plugin, PluginInterfaces.IMapWinGUI).Initialize(frmMain.Menus, frmMain.Toolbar, frmMain.StatusBar, frmMain.UIPanel, frmMain.Handle)
                    If TypeOf Plugin Is PluginInterfaces.IPluginInteraction Then CType(Plugin, PluginInterfaces.IPluginInteraction).Initialize(frmMain.m_PluginManager.newInt)
                    If TypeOf Plugin Is PluginInterfaces.IProject Then CType(Plugin, PluginInterfaces.IProject).Initialize(frmMain.Project)
                    If TypeOf Plugin Is PluginInterfaces.IProjectEvents Then CType(Plugin, PluginInterfaces.IProjectEvents).Initialize()

                    Return True
                End If
            Catch ex As Exception
                g_error = "Error in plugin:  " & ex.Message
                ShowError(ex)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Sub StopPlugin(ByVal Key As String) Implements PluginInterfaces.PluginTracker.StopPlugin
        If m_Locked Then Exit Sub
        Try
            If ContainsKey(m_LoadedPlugins, Key) Then
                Dim Plugin As PluginInterfaces.IBasePlugin
                Plugin = CType(m_LoadedPlugins(Key), PluginInterfaces.IBasePlugin)
                'Plugin.Unload()

                'EVERY POSSIBLE TYPE of plug-in must call terminate here as appropriate!
                Try
                    Plugin.Terminate() 'This form of initialize is guaranteed to be there if the plugin loaded (IBasePlugin)
                    If TypeOf Plugin Is PluginInterfaces.IMapWinGUI Then CType(Plugin, PluginInterfaces.IMapWinGUI).Terminate()
                    If TypeOf Plugin Is PluginInterfaces.IPluginInteraction Then CType(Plugin, PluginInterfaces.IPluginInteraction).Terminate()
                    If TypeOf Plugin Is PluginInterfaces.IProject Then CType(Plugin, PluginInterfaces.IProject).Terminate()
                    If TypeOf Plugin Is PluginInterfaces.IProjectEvents Then CType(Plugin, PluginInterfaces.IProjectEvents).Terminate()
                Catch ex As Exception
                    MapWinUtility.Logger.Msg("Warning: The plugin '" + Key + "' raised an error in its Terminate() function." + vbCrLf + vbCrLf + "The full details appear in the application log (if enabled).", MsgBoxStyle.Exclamation, "Plug-in Error Warning")
                    MapWinUtility.Logger.Dbg("Plugin Exception in " + Key + ": " + ex.ToString())
                    'Proceed
                End Try

                Plugin = Nothing
                m_LoadedPlugins.Remove(Key)
                frmMain.SynchPluginMenu()

                frmMain.m_Menu.MenuTrackerRemoveIfLastOwner(Key)
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Public ReadOnly Property Count() As Integer Implements PluginInterfaces.PluginTracker.Count
        Get
            Return m_PluginList.Count
        End Get
    End Property

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal Index As Integer) As PluginInterfaces.IBasePlugin Implements MapWindow.PluginInterfaces.PluginTracker.Item
        Get
            If Index <= m_LoadedPlugins.Count AndAlso Index > 0 Then  'collections are 1-based
                Return CType(m_LoadedPlugins(Index), PluginInterfaces.IBasePlugin)
            End If

            Return Nothing
        End Get
    End Property

    Public Sub Remove(ByVal IndexOrKey As Object) Implements PluginInterfaces.PluginTracker.Remove
        If m_Locked Then Exit Sub
        Try
            Select Case LCase(TypeName(IndexOrKey))
                Case "string", "long", "integer", "short"
                    If ContainsKey(m_LoadedPlugins, IndexOrKey) Then StopPlugin(CStr(IndexOrKey))
                    m_PluginList.Remove(IndexOrKey)
                Case Else
                    g_error = "Error: Invalid 'IndexOrKey' parameter type. Must be 'String', 'Integer', 'Long', or 'Short'"
            End Select
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Friend ReadOnly Property LoadedPlugins() As Collection
        Get
            Return m_LoadedPlugins
        End Get
    End Property

    Friend ReadOnly Property PluginsList() As Hashtable
        Get
            Return m_PluginList
        End Get
    End Property

    Public Property PluginFolder() As String Implements PluginInterfaces.PluginTracker.PluginFolder
        Get
            Return m_PluginFolder
        End Get
        Set(ByVal Value As String)
            m_PluginFolder = Value
        End Set
    End Property

    Public Function PluginIsLoaded(ByVal Key As String) As Boolean Implements PluginInterfaces.PluginTracker.PluginIsLoaded
        Return (ContainsKey(m_LoadedPlugins, Key) Or m_ApplicationPlugins.ContainsKey(Key))
    End Function

    Public Sub ShowPluginDialog() Implements PluginInterfaces.PluginTracker.ShowPluginDialog
        If Not m_Locked Then m_dlg.ShowDialog()
    End Sub

    Friend Sub UnloadAll()
        Try
            If m_LoadedPlugins.Count = 0 Then Return

            For i As Integer = m_LoadedPlugins.Count To 1 Step -1
                Dim Plugin As Interfaces.PluginInfo = CType(frmMain.m_PluginManager.PluginsList(MapWinUtility.PluginManagementTools.GenerateKey(m_LoadedPlugins(i).GetType())), Interfaces.PluginInfo)
                StopPlugin(Plugin.Key)
            Next
            m_LoadedPlugins = New Collection
            GC.Collect()
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Friend Sub UnloadApplicationPlugins()
        Dim Plugin As PluginInterfaces.IBasePlugin

        Try
            Dim item As DictionaryEntry
            For Each item In m_ApplicationPlugins
                StopPlugin(item.Key)
                Plugin = Nothing
            Next

            m_ApplicationPlugins.Clear()
            m_ApplicationPlugins = New Hashtable
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub
    '--------------------------------------------------------------------------------------------
    'Event generators
    '--------------------------------------------------------------------------------------------
    Friend Function LegendDoubleClick(ByVal Handle As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        'NEW INTERFACE TODO Legend Interface Item
        Return False
        'Dim tPlugin As Object 'REPLACE ME WITH LEGEND INTERFACE
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LegendDoubleClick(Handle, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LegendDoubleClick(Handle, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Friend Function LegendMouseDown(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        'NEW INTERFACE TODO Legend Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LegendMouseDown(Handle, Button, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LegendMouseDown(Handle, Button, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Friend Function LegendMouseUp(ByVal Handle As Integer, ByVal Button As Integer, ByVal Location As Interfaces.ClickLocation) As Boolean
        'NEW INTERFACE TODO Legend Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LegendMouseUp(Handle, Button, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LegendMouseUp(Handle, Button, Location, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Friend Sub MapExtentsChanged()
        'NEW INTERFACE TODO View Interface Item
        Return
        'Dim tPlugin As Object

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.MapExtentsChanged()
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.MapExtentsChanged()
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Function MapMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer) As Boolean
        'NEW INTERFACE TODO View Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.MapMouseDown(Button, Shift, x, y, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.MapMouseDown(Button, Shift, x, y, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Friend Function MapMouseMove(ByVal ScreenX As Integer, ByVal ScreenY As Integer) As Boolean
        'NEW INTERFACE TODO View Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        ''Update the status bar items if necessary, depending on the project settings.
        'Try
        '    Dim x, y As Double

        '    modMain.frmMain.MapMain.PixelToProj(ScreenX, ScreenY, x, y)

        '    'Provide lat/long (unprojected)
        '    'If no projection is provided, cannot do this.
        '    Try

        '        If Not modMain.ProjInfo.ProjectProjection = "" Then
        '            'If it's not set to anything and the map units are not lat/long, default to lat/long.
        '            'TODO : shouldn't we rather show 'unknown units?'
        '            If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" And Not modMain.ProjInfo.m_MapUnits = "" And Not modMain.ProjInfo.m_MapUnits = "Lat/Long" Then
        '                modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long"
        '            End If

        '            'status bar for coordinates in alternate units
        '            If Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "" And Not modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" Then
        '                If m_AlternateCoordToolbar Is Nothing Then
        '                    m_AlternateCoordToolbar = New Windows.Forms.StatusBarPanel
        '                    m_AlternateCoordToolbar.AutoSize = StatusBarPanelAutoSize.Contents
        '                    frmMain.StatusBar1.Items.Insert(0, m_AlternateCoordToolbar)
        '                End If

        '                If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long" Then
        '                    If (Not MapWinGeoProc.SpatialReference.ProjectPoint(x, y, modMain.ProjInfo.ProjectProjection, "+proj=latlong +ellps=WGS84 +datum=WGS84")) Then
        '                        MapWinUtility.Logger.Dbg("DEBUG: " + MapWinGeoProc.Error.GetLastErrorMsg())
        '                    Else
        '                        m_AlternateCoordToolbar.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals, modMain.ProjInfo.StatusBarAlternateCoordsUseCommas, "Lat/Long")
        '                    End If
        '                    'else do conversion from units to units
        '                Else
        '                    Dim UOMOrig As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Meters
        '                    Dim UOMDest As MapWindow.Interfaces.UnitOfMeasure = MapWindow.Interfaces.UnitOfMeasure.Kilometers

        '                    '08/28/2008 Jiri Kadlec - use the new unit conversion function from MapWinGeoProc
        '                    UOMOrig = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnits)
        '                    UOMDest = MapWinGeoProc.UnitConverter.StringToUOM(modMain.ProjInfo.ShowStatusBarCoords_Alternate)

        '                    Dim newText As String = ""
        '                    Dim numDecimals As Integer = modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals
        '                    Dim useCommas As Boolean = modMain.ProjInfo.StatusBarAlternateCoordsUseCommas
        '                    Try
        '                        Dim strX, strY As Double
        '                        strX = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, x)
        '                        strY = MapWinGeoProc.UnitConverter.ConvertLength(UOMOrig, UOMDest, y)

        '                        newText = FormatCoords(x, y, numDecimals, useCommas, UOMDest.ToString())

        '                    Catch ex As Exception
        '                        newText = FormatCoords(x, y, numDecimals, useCommas, UOMOrig.ToString())
        '                    End Try
        '                    m_AlternateCoordToolbar.Text = newText

        '                End If
        '            ElseIf Not m_AlternateCoordToolbar Is Nothing Then
        '                frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '                m_AlternateCoordToolbar = Nothing
        '            End If
        '        Else 'Can't do it - make sure the status bar panel isn't there
        '            If Not m_AlternateCoordToolbar Is Nothing Then
        '                frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '                m_AlternateCoordToolbar = Nothing
        '            End If
        '        End If
        '    Catch ex As Exception
        '        'Very likely a projection error.
        '        'Just display the coords.
        '        If Not m_AlternateCoordToolbar Is Nothing Then
        '            frmMain.StatusBar1.Items.Remove(m_AlternateCoordToolbar)
        '            m_AlternateCoordToolbar = Nothing
        '        End If
        '    End Try

        '    ' Re-get the coords...
        '    modMain.frmMain.MapMain.PixelToProj(ScreenX, ScreenY, x, y)

        '    'Provide coordinate in projected system (may be unknown or just pixels in the case of images, etc)
        '    If (modMain.ProjInfo.ShowStatusBarCoords_Projected) Then
        '        If m_ProjectedCoordToolbar Is Nothing Then
        '            m_ProjectedCoordToolbar = New Windows.Forms.StatusBarPanel
        '            m_ProjectedCoordToolbar.AutoSize = StatusBarPanelAutoSize.Contents
        '            frmMain.StatusBar1.Items.Insert(0, m_ProjectedCoordToolbar)
        '        End If
        '        'Dim cvter As New ScaleBarUtility

        '        m_ProjectedCoordToolbar.Text = FormatCoords(x, y, modMain.ProjInfo.StatusBarCoordsNumDecimals, modMain.ProjInfo.StatusBarCoordsUseCommas, modMain.frmMain.Project.MapUnits)

        '    ElseIf Not m_ProjectedCoordToolbar Is Nothing Then
        '        frmMain.StatusBar1.Items.Remove(m_ProjectedCoordToolbar)
        '        m_ProjectedCoordToolbar = Nothing
        '    End If
        'Catch ex As Exception
        '    Debug.WriteLine(ex.ToString())
        'End Try

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Interfaces.IPlugin)

        '        tPlugin.MapMouseMove(ScreenX, ScreenY, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try

        '        tPlugin.MapMouseMove(ScreenX, ScreenY, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Private Function FormatCoords(ByVal x As Double, ByVal y As Double, ByVal decimals As Integer, ByVal useCommas As String, ByVal units As String) As String
        '5/9/2008 Jiri Kadlec - this function is used by MapMouseMove() to format the coordinates in
        'the MW status bar.
        Dim nf As String 'the number formatting string

        If useCommas = True Then
            nf = "N" + decimals.ToString
        Else
            nf = "F" + decimals.ToString
        End If
        If units = "Lat/Long" Then
            Return String.Format("Lat: {0} Long: {1}", y.ToString(nf), x.ToString(nf))
        Else
            Return String.Format("X: {0} Y: {1} {2}", x.ToString(nf), y.ToString(nf), units)
        End If
    End Function

    Friend Function MapMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Integer, ByVal y As Integer) As Boolean
        'NEW INTERFACE TODO View Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.MapMouseUp(Button, Shift, x, y, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.MapMouseUp(Button, Shift, x, y, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    Friend Function MapDragFinished(ByVal Bounds As Rectangle) As Boolean
        'NEW INTERFACE TODO View Interface Item
        Return False
        'Dim tPlugin As Object
        'Dim handled As Boolean = False

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.MapDragFinished(Bounds, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.MapDragFinished(Bounds, handled)
        '        If handled Then Return True
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Function

    '' Layer Events
    Friend Sub LayersAdded(ByVal Handle As MapWindow.Interfaces.Layer())
        'NEW INTERFACE TODO Legend Interface Item
        Return
        'Dim tPlugin As Object

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LayersAdded(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LayersAdded(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Sub LayerRemoved(ByVal Handle As Integer)
        'NEW INTERFACE TODO Legend Interface Item
        Return
        'Dim tPlugin As Object

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LayerRemoved(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LayerRemoved(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Sub LayerSelected(ByVal Handle As Integer)
        'NEW INTERFACE TODO Legend Interface Item
        Return
        'Dim tPlugin As Object

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LayerSelected(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LayerSelected(Handle)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Sub LayersCleared()
        'NEW INTERFACE TODO Legend Interface Item
        Return
        'Dim tPlugin As Object
        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.LayersCleared()
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.LayersCleared()
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Sub ShapesSelected(ByVal Handle As Integer, ByVal SelectInfo As MapWindow.SelectInfo)
        'NEW INTERFACE TODO View Interface Item
        Return
        'frmMain.Menus("mnuClearSelectedShapes").Enabled = (SelectInfo.NumSelected > 0)

        'Dim tPlugin As Object

        'Dim item As DictionaryEntry
        'For Each item In m_ApplicationPlugins
        '    Try
        '        tPlugin = CType(item.Value, Object)
        '        tPlugin.ShapesSelected(Handle, SelectInfo)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.ShapesSelected(Handle, SelectInfo)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Friend Function ItemClicked(ByVal ItemName As String) As Boolean
        'Called by the MapWindow GUI when a user clicks a toolbar button that was added by a plugin.
        Dim tPlugin As PluginInterfaces.IMapWinGUI
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                If TypeOf item.Value Is PluginInterfaces.IMapWinGUI Then
                    tPlugin = CType(item.Value, PluginInterfaces.IMapWinGUI)
                    tPlugin.ItemClicked(ItemName, handled)
                    If handled Then Return True
                End If
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each o As Object In m_LoadedPlugins
            Try
                If TypeOf o Is PluginInterfaces.IMapWinGUI Then
                    tPlugin = CType(item.Value, PluginInterfaces.IMapWinGUI)
                    tPlugin.ItemClicked(ItemName, handled)
                    'if one of the plugins returns "handled=true" then we stop sending the click to other plugins. 
                    If handled Then Return True
                End If
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Function Message(ByVal msg As String) As Boolean
        Dim tPlugin As PluginInterfaces.IPluginInteraction = Nothing
        Dim handled As Boolean = False

        Dim item As DictionaryEntry
        For Each item In m_ApplicationPlugins
            Try
                If TypeOf tPlugin Is PluginInterfaces.IPluginInteraction Then
                    tPlugin = CType(item.Value, PluginInterfaces.IPluginInteraction)
                    tPlugin.Message(msg, handled)
                    If handled Then Return True
                End If
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next

        For Each o As Object In m_LoadedPlugins
            Try
                If TypeOf o Is PluginInterfaces.IPluginInteraction Then
                    tPlugin = CType(o, PluginInterfaces.IPluginInteraction)
                    tPlugin.Message(msg, handled)
                    If handled Then Return True
                End If
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        Next
    End Function

    Friend Sub ProjectLoading(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)
        Dim tPlugin As PluginInterfaces.IProjectEvents = Nothing

        Try
            If m_ApplicationPlugins.Contains(Key) = False Then Exit Try
            If TypeOf tPlugin Is PluginInterfaces.IPluginInteraction Then
                tPlugin = CType(m_ApplicationPlugins(Key), PluginInterfaces.IProjectEvents)
                tPlugin.ProjectLoading(ProjectFile, SettingsString)
                tPlugin = Nothing
            End If
        Catch ex As Exception
            g_error = ex.Message
            If Not tPlugin Is Nothing Then
                Dim new_ex As New System.Exception("An error occured in the plugin named: " & CType(tPlugin, PluginInterfaces.IBasePlugin).Name, ex)
                ShowError(new_ex)
            Else
                ShowError(ex)
            End If
        End Try

        Try
            If ContainsKey(m_LoadedPlugins, Key) = False Then Exit Sub
            Dim o As Object = m_LoadedPlugins(Key)
            If TypeOf o Is PluginInterfaces.IProjectEvents Then
                tPlugin = CType(o, PluginInterfaces.IProjectEvents)
                tPlugin.ProjectLoading(ProjectFile, SettingsString)
                tPlugin = Nothing
            End If
        Catch ex As Exception
            g_error = ex.Message
            If Not tPlugin Is Nothing Then
                Dim new_ex As New System.Exception("An error occured in the plugin named: " & CType(tPlugin, PluginInterfaces.IBasePlugin).Name, ex)
                ShowError(new_ex)
            Else
                ShowError(ex)
            End If
        End Try
    End Sub

    Friend Sub ProjectSaving(ByVal Key As String, ByVal ProjectFile As String, ByVal SettingsString As String)
        Dim tPlugin As PluginInterfaces.IProjectEvents

        Try
            If m_ApplicationPlugins.Contains(Key) = False Then Exit Try
            Dim o As Object = m_ApplicationPlugins(Key)
            If TypeOf o Is PluginInterfaces.IProjectEvents Then
                tPlugin = CType(o, PluginInterfaces.IProjectEvents)
                tPlugin.ProjectSaving(ProjectFile, SettingsString)
                tPlugin = Nothing
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try

        Try
            If ContainsKey(m_LoadedPlugins, Key) = False Then Exit Sub
            Dim o As Object = m_LoadedPlugins(Key)
            If TypeOf o Is PluginInterfaces.IProjectEvents Then
                tPlugin = CType(o, PluginInterfaces.IProjectEvents)
                tPlugin.ProjectSaving(ProjectFile, SettingsString)
                tPlugin = Nothing
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    <CLSCompliant(False)> _
    Public Function LoadFromObject(ByVal Plugin As PluginInterfaces.IBasePlugin, ByVal PluginKey As String) As Boolean Implements PluginInterfaces.PluginTracker.LoadFromObject
        Try
            If Plugin Is Nothing Then
                g_error = "LoadFromObject failed.  The parameter 'Plugin' was not set."
                Return False
            Else
                If ContainsKey(m_LoadedPlugins, PluginKey) Then
                    g_error = "Key already exists!  Cannot load plugins with duplicate keys."
                    Return False
                Else
                    '3/1/2005 dpa - this check isn't necessary now.
                    'If IsAuthorizedPlugin(Plugin.Author, Plugin.SerialNumber) = False Then
                    '    Dim nag As New frmNag(Plugin.Name)
                    '    nag.ShowDialog()
                    'End If
                    Plugin.Initialize()
                    If TypeOf Plugin Is PluginInterfaces.IProjectEvents Then CType(Plugin, PluginInterfaces.IProjectEvents).ProjectLoading(ProjInfo.ProjectFileName, "")
                    m_LoadedPlugins.Add(Plugin, PluginKey)
                    Return True
                End If
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    <CLSCompliant(False)> _
    Public Function LoadFromObject(ByVal Plugin As PluginInterfaces.IBasePlugin, ByVal PluginKey As String, ByVal SettingsString As String) As Boolean Implements PluginInterfaces.PluginTracker.LoadFromObject
        Try
            If Plugin Is Nothing Then
                g_error = "LoadFromObject failed.  The parameter 'Plugin' was not set."
                Return False
            Else
                If ContainsKey(m_LoadedPlugins, PluginKey) Then
                    g_error = "Key already exists!  Cannot load plugins with duplicate keys."
                    Return False
                Else
                    '3/1/2005 dpa - this check isn't necessary now.
                    'If IsAuthorizedPlugin(Plugin.Author, Plugin.SerialNumber) = False Then
                    '    Dim nag As New frmNag(Plugin.Name)
                    '    nag.ShowDialog()
                    'End If
                    Plugin.Initialize()
                    If TypeOf Plugin Is PluginInterfaces.IProjectEvents Then CType(Plugin, PluginInterfaces.IProjectEvents).ProjectLoading(ProjInfo.ProjectFileName, SettingsString)
                    m_LoadedPlugins.Add(Plugin, PluginKey)
                    Return True
                End If
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
    End Function

    Friend Function ContainsKey(ByVal c As Collection, ByVal key As Object) As Boolean
        '02/20/08 LCW: use direct function to avoid time-consuming exceptions
        Return c.Contains(key)
        'Dim o As Object = Nothing
        'Try
        '    o = c(key)
        '    Return True
        'Catch
        '    Return False
        'End Try
    End Function

    Public Sub BroadcastMessage(ByVal Message As String) Implements MapWindow.PluginInterfaces.PluginTracker.BroadcastMessage
        'CDM 4/24/2006 - This is a simplified version of Message, it just doesn't have a
        '"handled" return value consideration. Call the one function, to reduce duplication of code.

        Me.Message(Message)

        'Dim tPlugin As GOHcdbl
        'Dim handled As Boolean = False

        'For Each tPlugin In m_LoadedPlugins
        '    Try
        '        tPlugin.Message(Message, handled)
        '    Catch ex As Exception
        '        g_error = ex.Message
        '        ShowError(ex)
        '    End Try
        'Next
    End Sub

    Public Function GetPluginKey(ByVal PluginName As String) As String Implements PluginInterfaces.PluginTracker.GetPluginKey
        'Returns the plugin key associated with a plugin name.  The plugin name is the name that is displayed in the 
        'Plugins menu.
        'dpa 1/25/2005
        Dim obj As DictionaryEntry

        For Each obj In m_PluginList
            If obj.Value.Name = PluginName Then
                Return MapWinUtility.PluginManagementTools.GenerateKey(obj.Value.GetType())
            End If
        Next
        Return ""
    End Function

    Friend Sub LoadApplicationPlugins(ByVal ApplicationPluginPath As String)
        Dim arr As New ArrayList

        Try
            If ApplicationPluginPath = "" Then Return
            If System.IO.Directory.Exists(ApplicationPluginPath) = False Then Return

            Dim PotentialPlugins As ArrayList = MapWinUtility.PluginManagementTools.FindPluginDLLs(ApplicationPluginPath)
            Dim info As PluginInfo
            Dim plugin As MapWindow.PluginInterfaces.IBasePlugin
            Dim pluginName As String
            For Each pluginName In PotentialPlugins
                Try
                    info = New PluginInfo
                    If info.Init(pluginName, GetType(PluginInterfaces.IBasePlugin).GUID) Then
                        If Not info.Key Is Nothing AndAlso Not m_ApplicationPlugins.ContainsKey(info.Key) Then
                            plugin = MapWinUtility.PluginManagementTools.CreatePluginObject(pluginName, info.CreateString)
                            If Not plugin Is Nothing Then
                                m_ApplicationPlugins.Add(info.Key, plugin)
                                plugin.Initialize()
                            End If
                        End If
                    End If
                Catch e As Exception
                    ' There was an error in this one plugin.  Keep going on.
                    MapWinUtility.Logger.Dbg(e.ToString())
                End Try
            Next
        Catch e As Exception
            MapWinUtility.Logger.Dbg(e.ToString())
        End Try
    End Sub
End Class