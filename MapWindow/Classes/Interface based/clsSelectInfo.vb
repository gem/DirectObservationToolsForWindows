'********************************************************************************************************
'File Name: SelectInfo.vb
'Description: public class for holding the information about selected shapes
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
'28 nov 2010 - sleschinski - added code for the support of shapefile selection implemented inside MapWinGis.
'                            the old methods will be deprecated eventually
'********************************************************************************************************

Public Class SelectInfo
    Implements IEnumerable
    Implements Interfaces.SelectInfo

    Friend Class SelectedShapeEnumerator
        Implements System.Collections.IEnumerator

        Private m_Collection As MapWindow.SelectInfo
        Private m_Idx As Integer = -1

        Public Sub New(ByVal inp As MapWindow.SelectInfo)
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

            If m_Idx >= m_Collection.NumSelected Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return New SelectedShapeEnumerator(Me)
    End Function

    Public Sub New(ByVal LayerHandle As Integer)
        m_SelectBounds = New MapWinGIS.Extents()
        m_LayerHandle = LayerHandle
    End Sub

    Protected Overrides Sub Finalize()
        'ClearSelectedShapes()
    End Sub

    '-------------------Private members for public properties-------------------
    Private m_LayerHandle As Integer
    Private m_NumSelected As Integer
    Private m_SelectBounds As MapWinGIS.Extents
    Private m_Shapes() As MapWindow.SelectedShape

    '--------------------------------------SelectInfo Public Interface--------------------------------------
    '30 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 2
    '---------------------------------------------------------------------------------------------------------

    '-------------------Subs-------------------
    <CLSCompliant(False)> _
    Public Sub AddSelectedShape(ByVal newShape As Interfaces.SelectedShape) Implements Interfaces.SelectInfo.AddSelectedShape
        If m_LayerHandle = -1 Then m_LayerHandle = frmMain.Legend.SelectedLayer

        If newShape Is Nothing Then
            g_error = "AddSelectedShape:  Object variable not set."

        Else
            ReDim Preserve m_Shapes(m_NumSelected)
            m_Shapes(m_NumSelected) = CType(newShape, MapWindow.SelectedShape)
            m_NumSelected = m_NumSelected + 1
        End If
    End Sub

    Public Sub AddByIndex(ByVal ShapeIndex As Integer, ByVal SelectColor As System.Drawing.Color) Implements Interfaces.SelectInfo.AddByIndex
        Dim newShp As New MapWindow.SelectedShape()

        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            If frmMain.MapMain.get_ShapeVisible(frmMain.Legend.SelectedLayer, ShapeIndex) <> False Then
                newShp.Add(ShapeIndex, SelectColor)
                AddSelectedShape(newShp)
            End If
        Else
            newShp.Add(ShapeIndex, SelectColor)
            AddSelectedShape(newShp)
        End If
        newShp = Nothing
    End Sub


    ''' <summary>
    ''' Clears the array only, leaving selection as it. Temporary.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearSelectedShapesTemp()
        Erase m_Shapes
        m_NumSelected = 0
    End Sub

    ''' <summary>
    ''' Excludes all the shapes form the selection. Is used together with old version of ocx
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearSelectedShapes() Implements Interfaces.SelectInfo.ClearSelectedShapes

        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            Dim isLocked As Boolean = False
            Try
                Dim oneShp As MapWindow.SelectedShape
                Dim i As Integer, tLyrHandle As Integer

                tLyrHandle = m_LayerHandle

                If frmMain.MapMain Is Nothing Then
                    For i = m_NumSelected - 1 To 0 Step -1
                        m_Shapes(i) = Nothing
                    Next i
                    Erase m_Shapes
                    m_SelectBounds = Nothing
                    Exit Sub
                End If

                If frmMain.MapMain.IsLocked = MapWinGIS.tkLockMode.lmUnlock Then
                    frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
                    isLocked = True
                End If

                For i = m_NumSelected - 1 To 0 Step -1
                    oneShp = m_Shapes(i)
                    If Not oneShp Is Nothing Then
                        With oneShp
                            frmMain.MapMain.set_ShapePointColor(tLyrHandle, .ShapeIndex, .OriginalColor)
                            frmMain.MapMain.set_ShapeLineColor(tLyrHandle, .ShapeIndex, .OriginalOutlineColor)
                            frmMain.MapMain.set_ShapeFillColor(tLyrHandle, .ShapeIndex, .OriginalColor)
                            frmMain.MapMain.set_ShapeDrawFill(tLyrHandle, .ShapeIndex, .OriginalDrawFill)
                            'Bugzilla 520
                            If ProjInfo.TransparentSelection Then frmMain.MapMain.set_ShapeFillTransparency(tLyrHandle, .ShapeIndex, .OriginalTransparency)
                        End With
                    End If

                    m_NumSelected = m_NumSelected - 1
                    oneShp = Nothing

                    m_Shapes(i) = Nothing
                Next i

                Erase m_Shapes
                m_SelectBounds = Nothing
                frmMain.UpdateButtons()
                m_LayerHandle = -1

            Catch ex As Exception
                ' something went wrong
                g_error = ex.Message
                ShowError(ex)
            End Try
            If isLocked Then
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End If
        Else
            ' for the new ocx version we don't need to deal with colors, but simply to call Shapefile.SelectNone
            Dim handle As Integer
            For i As Integer = 0 To frmMain.Layers.NumLayers - 1
                handle = frmMain.MapMain.get_LayerHandle(i)
                Dim sf As MapWinGIS.Shapefile
                Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers.Item(handle)
                If Not layer Is Nothing Then
                    If layer.LayerType = Interfaces.eLayerType.LineShapefile Or _
                       layer.LayerType = Interfaces.eLayerType.PointShapefile Or _
                       layer.LayerType = Interfaces.eLayerType.PolygonShapefile Then
                        sf = CType(layer.GetObject, MapWinGIS.Shapefile)
                        If Not sf Is Nothing Then sf.SelectNone()
                    End If
                End If
            Next i

            Erase m_Shapes
            m_LayerHandle = -1
            m_NumSelected = 0
            frmMain.UpdateButtons()
            frmMain.MapMain.Redraw()
        End If
    End Sub

    ''' <summary>
    ''' Removes the shape with the given index from selection
    ''' </summary>
    Public Sub RemoveSelectedShape(ByVal ListIndex As Integer) Implements Interfaces.SelectInfo.RemoveSelectedShape
        Dim i As Integer, tShp As MapWindow.SelectedShape, mapIndex As Integer, mapHandle As Integer

        If frmMain.MapMain Is Nothing Then Exit Sub

        mapHandle = m_LayerHandle

        If ListIndex >= 0 And ListIndex < m_NumSelected Then
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)

            Try
                tShp = m_Shapes(ListIndex)
                mapIndex = tShp.ShapeIndex

                If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                    frmMain.MapMain.set_ShapePointColor(mapHandle, mapIndex, tShp.OriginalColor)
                    frmMain.MapMain.set_ShapeLineColor(mapHandle, mapIndex, tShp.OriginalOutlineColor)
                    frmMain.MapMain.set_ShapeFillColor(mapHandle, mapIndex, tShp.OriginalColor)
                    frmMain.MapMain.set_ShapeDrawFill(mapHandle, mapIndex, tShp.OriginalDrawFill)
                    'Bugzilla 520
                    If ProjInfo.TransparentSelection Then frmMain.MapMain.set_ShapeFillTransparency(mapHandle, mapIndex, tShp.OriginalTransparency)
                Else
                    ' ocx will choose the right colors on it's own in the new version
                    Dim layer As MapWindow.Layer = frmMain.Layers(m_LayerHandle)
                    If Not layer Is Nothing Then
                        Dim sf As MapWinGIS.Shapefile
                        sf = CType(layer.GetObject(), MapWinGIS.Shapefile)
                        If Not sf Is Nothing Then
                            tShp = m_Shapes(ListIndex)
                            mapIndex = tShp.ShapeIndex
                            sf.ShapeSelected(mapIndex) = False
                        End If
                    End If
                End If
                tShp = Nothing

                For i = ListIndex To m_NumSelected - 2
                    m_Shapes(i) = m_Shapes(i + 1)
                Next i

                m_Shapes(m_NumSelected - 1) = Nothing
                m_NumSelected = m_NumSelected - 1

                If m_NumSelected = 0 Then
                    Erase m_Shapes
                Else
                    ReDim Preserve m_Shapes(m_NumSelected - 1)
                End If
            Finally
                frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            End Try

        Else
            g_error = "RemoveSelectedShape:  Invalid index"

        End If
    End Sub

    ''' <summary>
    ''' Removes the shape with the given index inside the shapefile from selection
    ''' </summary>
    Public Sub RemoveByShapeIndex(ByVal ShapeIndex As Integer) Implements MapWindow.Interfaces.SelectInfo.RemoveByShapeIndex
        Dim i, j As Integer
        Dim tShp As MapWindow.SelectedShape

        If frmMain.MapMain Is Nothing Then Exit Sub

        For i = 0 To m_Shapes.Length - 1
            If m_Shapes(i).ShapeIndex = ShapeIndex Then
                tShp = m_Shapes(i)

                If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                    frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
                    Try
                        frmMain.MapMain.set_ShapePointColor(m_LayerHandle, ShapeIndex, tShp.OriginalColor)
                        frmMain.MapMain.set_ShapeLineColor(m_LayerHandle, ShapeIndex, tShp.OriginalOutlineColor)
                        frmMain.MapMain.set_ShapeFillColor(m_LayerHandle, ShapeIndex, tShp.OriginalColor)
                        frmMain.MapMain.set_ShapeDrawFill(m_LayerHandle, ShapeIndex, tShp.OriginalDrawFill)
                        'Bugzilla 520
                        If ProjInfo.TransparentSelection Then frmMain.MapMain.set_ShapeFillTransparency(m_LayerHandle, ShapeIndex, tShp.OriginalTransparency)
                    Finally
                        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
                    End Try
                Else
                    ' ocx will choose the right colors on it's own in the new version
                    Dim layer As MapWindow.Layer = frmMain.Layers(m_LayerHandle)
                    If Not layer Is Nothing Then
                        Dim sf As MapWinGIS.Shapefile
                        sf = CType(layer.GetObject(), MapWinGIS.Shapefile)
                        If Not sf Is Nothing Then
                            sf.ShapeSelected(ShapeIndex) = False
                        End If
                    End If
                End If

                For j = i To m_NumSelected - 2
                    m_Shapes(j) = m_Shapes(j + 1)
                Next j

                m_Shapes(m_NumSelected - 1) = Nothing
                m_NumSelected = m_NumSelected - 1

                If m_NumSelected = 0 Then
                    Erase m_Shapes
                Else
                    ReDim Preserve m_Shapes(m_NumSelected - 1)
                End If

                tShp = Nothing
                ' all done removing so exit
                Exit Sub
            End If
        Next i
    End Sub

    '-------------------Properties-------------------
    Public ReadOnly Property NumSelected() As Integer Implements Interfaces.SelectInfo.NumSelected
        Get
            ' lsu: why not to return the length of array?
            NumSelected = m_NumSelected
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property SelectBounds() As MapWinGIS.Extents Implements Interfaces.SelectInfo.SelectBounds
        Get
            ' Calculate the total bounds of the selected items
            Dim NewBounds As New MapWinGIS.Extents(), k As Integer

            Dim curmaxX As Double, curminX As Double
            Dim curmaxY As Double, curminY As Double
            Dim curmaxZ As Double, curminZ As Double

            Dim shpExts As MapWinGIS.Extents


            If m_NumSelected > 0 Then
                shpExts = m_Shapes(0).Extents
                curmaxX = shpExts.xMax
                curminX = shpExts.xMin
                curmaxY = shpExts.yMax
                curminY = shpExts.yMin
                curmaxZ = shpExts.zMax
                curminZ = shpExts.zMin

            Else
                Return Nothing
                Exit Property

            End If

            For k = 1 To m_NumSelected - 1
                shpExts = Nothing
                shpExts = m_Shapes(k).Extents
                If shpExts.xMax > curmaxX Then curmaxX = shpExts.xMax
                If shpExts.xMin < curminX Then curminX = shpExts.xMin
                If shpExts.yMax > curmaxY Then curmaxY = shpExts.yMax
                If shpExts.yMin < curminY Then curminY = shpExts.yMin
                If shpExts.zMax > curmaxZ Then curmaxZ = shpExts.zMax
                If shpExts.zMin < curminZ Then curminZ = shpExts.zMin
            Next k

            NewBounds.SetBounds(curminX, curminY, curminZ, curmaxX, curmaxY, curmaxZ)
            SelectBounds = NewBounds
        End Get
    End Property

    <CLSCompliant(False)> _
    Default Public ReadOnly Property Item(ByVal Index As Integer) As Interfaces.SelectedShape Implements Interfaces.SelectInfo.Item
        Get
            If m_NumSelected = 0 Then
                Return Nothing

            ElseIf Index >= 0 And Index < m_NumSelected Then
                Return m_Shapes(Index)

            Else
                g_error = "SelectedShape:  Invalid Index"
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property LayerHandle() As Integer Implements Interfaces.SelectInfo.LayerHandle
        Get
            Return m_LayerHandle
        End Get
    End Property

    'Friend Sub SetLayerHandle(ByVal NewHandle As Integer)
    '    ClearSelectedShapes()
    '    m_LayerHandle = NewHandle
    'End Sub
End Class


