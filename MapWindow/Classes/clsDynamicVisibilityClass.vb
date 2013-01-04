'********************************************************************************************************
'File Name: clsDynamicVisibilityClass.vb
'Description: Friend class used to set and get dynamic visibility of layers. 
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
'28 nov 2010 - sleschinsky - Blocked the Add method for the new version of MapWinGis, dynamic visiblity 
'              is implemented internally by ocx now
'********************************************************************************************************

Friend Class DynamicVisibilityClass
    Private ht As New Hashtable

    Private Class Point
        Public x As Double
        Public y As Double

        Public Sub New(ByVal newX As Double, ByVal newY As Double)
            x = newX
            y = newY
        End Sub

        'calculate the dist from this point to point p
        Public Function Dist(ByVal p As Point) As Double
            Return Math.Sqrt(Math.Pow((y - p.y), 2) + Math.Pow((x - p.x), 2))
        End Function

    End Class

    Friend Class DVInfo
        Private m_Scale As Double
        Private m_Extents As MapWinGIS.Extents
        Private m_UseExtents As Boolean = False
        Private m_handle As Integer

        Public Sub New(ByVal Exts As MapWinGIS.Extents, ByVal UseExts As Boolean, ByVal LayerHandle As Integer)
            m_Extents = Exts
            m_Scale = frmMain.ExtentsToScale(m_Extents)
            m_handle = LayerHandle
            If (frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology) Then
                m_UseExtents = UseExts
            Else
                m_UseExtents = False
            End If
        End Sub

        Public Property UseDynamicExtents() As Boolean
            Get
                Return m_UseExtents
            End Get
            Set(ByVal Value As Boolean)
                ' this will block the using of old dynamic visiblity with new ocx
                If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
                    m_UseExtents = Value
                    frmMain.Legend.Layers.ItemByHandle(m_handle).UseDynamicVisibility = Value
                End If
            End Set
        End Property

        Public Property DynamicExtents() As MapWinGIS.Extents
            Get
                Return m_Extents
            End Get
            Set(ByVal Value As MapWinGIS.Extents)
                m_Extents = Value
                m_Scale = frmMain.ExtentsToScale(m_Extents)
            End Set
        End Property

        Public Property DynamicScale() As Double
            Get
                Return m_Scale
            End Get
            Set(ByVal Value As Double)
                m_Scale = Value
                m_Extents = frmMain.ScaleToExtents(m_Scale, frmMain.MapMain.Extents)
            End Set
        End Property
    End Class

    Default Property Item(ByVal LayerHandle As Integer) As DVInfo
        Get
            If Not ht.ContainsKey(LayerHandle) Then
                Dim emptyexts As New MapWinGIS.Extents
                emptyexts.SetBounds(0, 0, 0, 0, 0, 0)
                Add(LayerHandle, emptyexts, False)
            End If

            Return CType(ht(LayerHandle), DVInfo)
        End Get
        Set(ByVal Value As DVInfo)
            If ht.ContainsKey(LayerHandle) Then
                ht(LayerHandle) = Value
            Else
                ht.Add(LayerHandle, Value)
            End If
        End Set
    End Property

    Public Sub Add(ByVal LayerHandle As Integer, ByVal Extents As MapWinGIS.Extents, ByVal FeatureEnabled As Boolean)
        ' this will block the using of old dynamic visiblity with new ocx
        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            ht.Add(LayerHandle, New DVInfo(Extents, FeatureEnabled, LayerHandle))
            If Not frmMain.Legend.Layers.ItemByHandle(LayerHandle) Is Nothing Then
                frmMain.Legend.Layers.ItemByHandle(LayerHandle).UseDynamicVisibility = FeatureEnabled
            End If
        End If
    End Sub

    Public Sub Remove(ByVal LayerHandle As Integer)
        ht.Remove(LayerHandle)
    End Sub

    Public Function Contains(ByVal LayerHandle As Integer) As Boolean
        Return ht.Contains(LayerHandle)
    End Function

    Public Sub Clear()
        ht.Clear()
    End Sub

    Friend Sub TestLayerZoomExtents()
        Dim h As Integer
        For Each h In ht.Keys
            ' make sure the handle is still valid, ie. hasn't been removed.
            If (Not frmMain.Layers.IsValidHandle(h)) Then
                ht.Remove(h)
                Return
            End If

            Dim obj As DVInfo = CType(ht(h), DVInfo)

            If (obj.UseDynamicExtents = True) Then
                If obj.DynamicScale >= frmMain.ExtentsToScale(frmMain.MapMain.Extents) Then
                    frmMain.Layers(h).Visible = True
                Else
                    frmMain.Layers(h).Visible = False
                End If

                ' deprecated code only for record
                ''calculate the dist from the saved extents
                'Dim p1 As Point = New Point(obj.DynamicExtents.xMin, obj.DynamicExtents.yMin)
                'Dim p2 As Point = New Point(obj.DynamicExtents.xMax, obj.DynamicExtents.yMax)

                'Dim dist1 As Double = p1.Dist(p2)

                ''calculate the dist form the view extents
                'Dim tExtents As MapWinGIS.Extents = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
                'Dim p3 As Point = New Point(tExtents.xMin, tExtents.yMin)
                'Dim p4 As Point = New Point(tExtents.xMax, tExtents.yMax)

                'Dim dist2 As Double = p3.Dist(p4)

                ''check to see if the labels are within tolerance
                'If (dist1 >= dist2) Then
                '    If Not frmMain.Layers(h).Visible = True Then
                '        frmMain.Layers(h).Visible = True
                '    End If
                'Else
                '    If frmMain.Layers(h).Visible = True Then
                '        frmMain.Layers(h).Visible = False
                '    End If
                'End If
            End If
        Next
    End Sub
End Class
