'********************************************************************************************************
'File Name: SelectedShape.vb
'Description: public class for holding the information about selected shape, namely it's coloring.
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
'28 nov 2010 - sleschinski - turned off the settings of underlying MapWinGis colours for the new version of ocx.
'                            Selection is managed in ocx internally now.
'********************************************************************************************************

Public Class SelectedShape
    Implements Interfaces.SelectedShape

    '-------------------Private members for public properties-------------------
    'Private m_Fields() As String
    'Private m_NumFields As Integer
    'Private m_Values() As Object
    Private m_ShapeIndex As Integer
    Private m_OriginalColor As UInt32
    Private m_OriginalDrawFill As Boolean
    Private m_OriginalTransparency As Single
    Private m_OriginalOutlineColor As UInt32

    '--------------------------------------SelectedShape Public Interface----------------------------------------
    '30 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 2
    '------------------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Quick and fast way  to add shape to the selection for new  symbology, not included in the interface
    ''' </summary>
    Public Sub Add(ByVal ShapeIndex As Integer)
        m_ShapeIndex = ShapeIndex
    End Sub

    '-------------------Subs-------------------
    Public Sub Add(ByVal ShapeIndex As Integer, ByVal SelectColor As System.Drawing.Color) Implements Interfaces.SelectedShape.Add

        Dim tShpObj As MapWinGIS.Shapefile
        Dim curLyr As Integer = frmMain.Legend.SelectedLayer
        If frmMain.Legend.SelectedLayer = -1 Then Exit Sub
        tShpObj = CType(frmMain.MapMain.get_GetObject(curLyr), MapWinGIS.Shapefile) : If tShpObj Is Nothing Then Exit Sub

        '' for the old version of ocx only; new version doesn't need it
        'If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then

        '    m_OriginalOutlineColor = MapWinUtility.Colors.ColorToUInteger(frmMain.MapMain.get_ShapeLineColor(curLyr, ShapeIndex))
        '    m_OriginalDrawFill = frmMain.MapMain.get_ShapeDrawFill(curLyr, ShapeIndex)
        '    m_ShapeIndex = ShapeIndex
        '    m_OriginalTransparency = frmMain.MapMain.get_ShapeFillTransparency(curLyr, ShapeIndex)

        '    Select Case tShpObj.ShapefileType
        '        Case MapWinGIS.ShpfileType.SHP_POLYGON, MapWinGIS.ShpfileType.SHP_POLYGONM, MapWinGIS.ShpfileType.SHP_POLYGONZ
        '            m_OriginalColor = MapWinUtility.Colors.ColorToUInteger(frmMain.MapMain.get_ShapeFillColor(curLyr, ShapeIndex))
        '            frmMain.MapMain.set_ShapeDrawFill(curLyr, ShapeIndex, True)
        '            frmMain.MapMain.set_ShapeFillColor(curLyr, ShapeIndex, MapWinUtility.Colors.ColorToUInteger(SelectColor))

        '            'Bugzilla 222 and Bugzilla 520
        '            If ProjInfo.TransparentSelection Then frmMain.MapMain.set_ShapeFillTransparency(curLyr, ShapeIndex, 0.5)

        '        Case MapWinGIS.ShpfileType.SHP_POINT, MapWinGIS.ShpfileType.SHP_POINTM, MapWinGIS.ShpfileType.SHP_POINTZ
        '            m_OriginalColor = MapWinUtility.Colors.ColorToUInteger(frmMain.MapMain.get_ShapePointColor(curLyr, ShapeIndex))
        '            frmMain.MapMain.set_ShapePointColor(curLyr, ShapeIndex, MapWinUtility.Colors.ColorToUInteger(SelectColor))

        '        Case Else
        '            m_OriginalColor = MapWinUtility.Colors.ColorToUInteger(frmMain.MapMain.get_ShapeLineColor(curLyr, ShapeIndex))
        '            frmMain.MapMain.set_ShapeLineColor(curLyr, ShapeIndex, MapWinUtility.Colors.ColorToUInteger(SelectColor))
        '    End Select
        'Else
        m_ShapeIndex = ShapeIndex
        tShpObj.ShapeSelected(ShapeIndex) = True
        'End If
    End Sub


    '-------------------Properties-------------------
    <CLSCompliant(False)> _
    Public ReadOnly Property Extents() As MapWinGIS.Extents Implements Interfaces.SelectedShape.Extents
        Get
            Dim tShpObj As MapWinGIS.Shapefile, tLyr As Integer, i As Integer
            If frmMain.Legend.SelectedLayer = -1 Then Return Nothing

            On Error Resume Next
            tLyr = frmMain.Legend.SelectedLayer

            tShpObj = CType(frmMain.MapMain.get_GetObject(tLyr), MapWinGIS.Shapefile)
            If tShpObj Is Nothing Then Return Nothing
            On Error GoTo 0

            Extents = tShpObj.Shape(m_ShapeIndex).Extents
        End Get
    End Property

    Friend ReadOnly Property OriginalColor() As UInt32
        Get
            OriginalColor = m_OriginalColor
        End Get
    End Property

    Friend ReadOnly Property OriginalDrawFill() As Boolean
        Get
            OriginalDrawFill = m_OriginalDrawFill
        End Get
    End Property

    Friend ReadOnly Property OriginalTransparency() As Single
        Get
            Return m_OriginalTransparency
        End Get
    End Property

    Friend ReadOnly Property OriginalOutlineColor() As UInt32
        Get
            OriginalOutlineColor = m_OriginalOutlineColor
        End Get
    End Property

    Public ReadOnly Property ShapeIndex() As Integer Implements Interfaces.SelectedShape.ShapeIndex
        Get
            ShapeIndex = m_ShapeIndex
        End Get
    End Property
End Class


