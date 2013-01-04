'********************************************************************************************************
'File Name: LegendEditorObjects.vb
'Description: Controls the properties of MapWindow legend editor
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
'5/6/2008 - Jiri Kadlec (jk) - enabled internationalization of the property grid. The "Category", "Name" 
'                              and "Description" entries are now stored in the resource file 
'                              (PrjSetGrid.resx) making translation to other languages possible.
'28-nov-2010 - sleschinski - Added LayerInfo class and made it to be parent for ImageInfo and GridInfo.
'                           New dynamic visibillity is implemented in LayerInfo class. Dynamic visiblity
'                           can be set for shapefile layer through symbology plug-in. 
'********************************************************************************************************

Option Strict Off

Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Drawing.Design
Imports System.Windows.Forms.Design
Imports MapWindow.PropertyGridUtils 'added by jk


Public Class LabelEx
    <CLSCompliant(False)> Public alignment As MapWinGIS.tkHJustification
    Public font As Font
    Public field As Integer
    Public UseMinExtents As Boolean
    <CLSCompliant(False)> Public extents As MapWinGIS.Extents
    Public color As System.Drawing.Color
    Public points As System.Collections.ArrayList
    Public CalculatePos As Boolean
    Public Modified As Boolean
    Public LabelExtentsChanged As Boolean
End Class


#Region "Enumerations"

Friend Enum FillSyles
    DiagonalDownLeft = MapWinGIS.tkFillStipple.fsDiagonalDownLeft
    DiagonalDownRight = MapWinGIS.tkFillStipple.fsDiagonalDownRight
    HorizontalBars = MapWinGIS.tkFillStipple.fsHorizontalBars
    None = MapWinGIS.tkFillStipple.fsNone
    PolkaDot = MapWinGIS.tkFillStipple.fsPolkaDot
    VerticalBars = MapWinGIS.tkFillStipple.fsVerticalBars
End Enum

Friend Enum LineStyles
    Solid = MapWinGIS.tkLineStipple.lsNone
    Dotted = MapWinGIS.tkLineStipple.lsDotted
    Dashed = MapWinGIS.tkLineStipple.lsDashed
    DashDot = MapWinGIS.tkLineStipple.lsDashDotDash
    TrainTracks = MapWinGIS.tkLineStipple.lsTrainTracks
End Enum

Friend Enum PointStyles
    Square = MapWinGIS.tkPointType.ptSquare
    Circle = MapWinGIS.tkPointType.ptCircle
    Diamond = MapWinGIS.tkPointType.ptDiamond
    TriangleUp = MapWinGIS.tkPointType.ptTriangleUp
    TriangleDown = MapWinGIS.tkPointType.ptTriangleDown
    TriangleLeft = MapWinGIS.tkPointType.ptTriangleLeft
    TriangleRight = MapWinGIS.tkPointType.ptTriangleRight
End Enum

#End Region

#Region "Type Converters"

Friend Class DynamicVisibilityTypeConverter
    Inherits StringConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If destinationType Is GetType(String) Then
            If CBool(value) = True Then
                Return "Enabled"
            Else
                Return "Disabled"
            End If
        End If

        Return Nothing
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(Boolean) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Friend Class ColoringSchemeTypeConverter
    Inherits StringConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If destinationType Is GetType(String) Then
            If TypeOf value Is MapWinGIS.ShapefileColorScheme Then
                If (Not value Is Nothing) AndAlso (CType(value, MapWinGIS.ShapefileColorScheme).NumBreaks <> 0) Then
                    Return "Edit..."
                Else
                    Return "[None]"
                End If
            ElseIf TypeOf value Is MapWinGIS.GridColorScheme Then
                If (Not value Is Nothing) AndAlso (CType(value, MapWinGIS.GridColorScheme).NumBreaks <> 0) Then
                    Return "Edit..."
                Else
                    Return "[None]"
                End If
            End If

        End If

        Return Nothing
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(MapWinGIS.ShapefileColorScheme) OrElse sourceType Is GetType(MapWinGIS.GridColorScheme) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Friend Class FillStippleSchemeTypeConverter
    Inherits StringConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If destinationType Is GetType(String) Then
            If TypeOf value Is MapWindow.Interfaces.ShapefileFillStippleScheme Then
                If (Not value Is Nothing) Then
                    Return "Edit..."
                End If
            End If
        End If
        Return "[None]"
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(MapWindow.Interfaces.ShapefileFillStippleScheme) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class


Friend Class PointImageSchemeTypeConverter
    Inherits StringConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If destinationType Is GetType(String) Then
            If TypeOf value Is MapWindow.Interfaces.ShapefilePointImageScheme Then
                If (Not value Is Nothing) AndAlso (CType(value, MapWindow.Interfaces.ShapefilePointImageScheme).NumberItems <> 0) Then
                    Return "Edit..."
                Else
                    Return "[None]"
                End If
            Else
                Return "[None]"
            End If
        End If

        Return Nothing
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(MapWindow.Interfaces.ShapefilePointImageScheme) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

'Added by Lailin Chen May-19-2006 to use USU Labeler Plugin-in in Lengend Editor to edit labels
Friend Class LabelerTypeConverter
    Inherits StringConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        Return "Edit..."
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(MapWinGIS.ShapefileColorScheme) OrElse sourceType Is GetType(MapWinGIS.GridColorScheme) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
' -----------------------------------------------------------------------------------------------------
#End Region

#Region "Type Editors"
' ---------------------------------------Type Editors--------------------------------------------------
Friend Class DynamicVisibilityEditor
    Inherits UITypeEditor
    Dim m_handle As Integer

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        If frmMain.MapMain.ShapeDrawingMethod <> MapWinGIS.tkShapeDrawingMethod.dmNewSymbology Then
            Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
            DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
            Dim MyDialog As New DynamicVisibilityControl(DialogProvider, m_handle)
            DialogProvider.DropDownControl(MyDialog)
            frmMain.SetModified(True)
            Return MyDialog.retval
        Else
            ' nothing will be returned in case the new version of ocx is used
            Return Nothing
        End If
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        m_handle = frmMain.Layers.CurrentLayer
        Return UITypeEditorEditStyle.DropDown
    End Function
End Class

Friend Class GridColoringSchemeEditor
    Inherits UITypeEditor
    Private m_ColoringScheme As MapWinGIS.GridColorScheme

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New GridColoringSchemeForm(DialogProvider, m_ColoringScheme)
        frmMain.SetModified(True)

        If DialogProvider.ShowDialog(MyDialog) = DialogResult.OK Then
            Return MyDialog.Retval()
        Else
            Return m_ColoringScheme
        End If
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        If TypeOf Context.Instance Is GridInfo Then
            m_ColoringScheme = CType(Context.Instance, GridInfo).m_ColoringScheme
        Else
            Return UITypeEditorEditStyle.None
        End If
        Return UITypeEditorEditStyle.Modal
    End Function
End Class

Friend Class SFColoringSchemeEditor
    Inherits UITypeEditor
    Private m_ColoringScheme As MapWinGIS.ShapefileColorScheme

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New SFColoringSchemeForm(DialogProvider, m_ColoringScheme)
        frmMain.SetModified(True)

        If DialogProvider.ShowDialog(MyDialog) = DialogResult.OK Then
            Return MyDialog.Retval()
        Else
            Return m_ColoringScheme
        End If
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        If TypeOf Context.Instance Is PolygonSFInfo Then
            m_ColoringScheme = CType(Context.Instance, PolygonSFInfo).m_Legend
        ElseIf TypeOf Context.Instance Is PolylineSFInfo Then
            m_ColoringScheme = CType(Context.Instance, PolylineSFInfo).m_Legend
        ElseIf TypeOf Context.Instance Is PointSFInfo Then
            m_ColoringScheme = CType(Context.Instance, PointSFInfo).m_Legend
        Else
            Return UITypeEditorEditStyle.None
        End If
        Return UITypeEditorEditStyle.Modal
    End Function
End Class

Friend Class SFFillStippleSchemeEditor
    Inherits UITypeEditor
    Private m_FillStippleScheme As MapWindow.Interfaces.ShapefileFillStippleScheme

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New SFFillStippleSchemeForm(DialogProvider, m_FillStippleScheme)
        frmMain.SetModified(True)

        If DialogProvider.ShowDialog(MyDialog) = DialogResult.OK Then
            Return MyDialog.Retval()
        Else
            Return m_FillStippleScheme
        End If
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        If TypeOf Context.Instance Is PolygonSFInfo Then
            m_FillStippleScheme = CType(Context.Instance, PolygonSFInfo).FillStippleScheme 
            Return UITypeEditorEditStyle.Modal
        Else
            Return UITypeEditorEditStyle.None
        End If
    End Function
End Class

Friend Class PointImageSchemeEditor
    Inherits UITypeEditor
    Private m_Scheme As MapWindow.Interfaces.ShapefilePointImageScheme

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        If m_Scheme Is Nothing Then m_Scheme = New MapWindow.Interfaces.ShapefilePointImageScheme(frmMain.Layers.CurrentLayer)
        Dim MyDialog As New PointImageSchemeForm(DialogProvider, m_Scheme)
        frmMain.SetModified(True)

        If DialogProvider.ShowDialog(MyDialog) = DialogResult.OK Then
            Dim tmpobj As Object = MyDialog.Retval
            frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageScheme = tmpobj
            Return tmpobj
        Else
            frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageScheme = m_Scheme
            Return m_Scheme

        End If
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        If TypeOf Context.Instance Is PolygonSFInfo Then
            Return UITypeEditorEditStyle.None
        ElseIf TypeOf Context.Instance Is PolylineSFInfo Then
            Return UITypeEditorEditStyle.None
        ElseIf TypeOf Context.Instance Is PointSFInfo Then
            m_Scheme = CType(Context.Instance, PointSFInfo).m_PointImageScheme
        Else
            Return UITypeEditorEditStyle.None
        End If
        Return UITypeEditorEditStyle.Modal
    End Function
End Class

'Added by Lailin Chen May-19-2006 to use USU Labeler Plugin-in in Lengend Editor to edit labels
Friend Class LabelEditor
    Inherits UITypeEditor

    Public Shared handle

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        frmMain.Plugins.BroadcastMessage("LABEL_EDIT:" + LabelEditor.handle.ToString())
        Return Nothing
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Friend Class LineWidthEditor
    Inherits UITypeEditor
    Private m_LineWidth As Integer = 1

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New LineWidthControl(DialogProvider)
        DialogProvider.DropDownControl(MyDialog)
        frmMain.SetModified(True)

        If MyDialog.SelectedSize <> -1 Then m_LineWidth = MyDialog.SelectedSize
        Return m_LineWidth
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        If TypeOf context.Instance Is PolygonSFInfo Then
            m_LineWidth = CType(context.Instance, PolygonSFInfo).OutlineWidth
        ElseIf TypeOf context.Instance Is PolylineSFInfo Then
            m_LineWidth = CType(context.Instance, PolylineSFInfo).LineWidth
        Else
            Return False
        End If
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim midY As Integer = CInt(((e.Bounds.Top + e.Bounds.Bottom) / 2) - 0.5)
        Dim p As New System.Drawing.Pen(System.Drawing.Color.Black, m_LineWidth)
        g.DrawLine(p, e.Bounds.Left + 2, midY, e.Bounds.Right - 3, midY)
    End Sub
End Class

Friend Class FillStyleEditor
    Inherits UITypeEditor
    Private m_FillStyle As FillSyles

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New FillStyleControl(DialogProvider)
        DialogProvider.DropDownControl(MyDialog)
        m_FillStyle = MyDialog.SelectedValue
        frmMain.SetModified(True)

        Return CType(m_FillStyle, MapWinGIS.tkFillStipple)
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        m_FillStyle = CType(CType(context.Instance, PolygonSFInfo).FillStyle, MapWindow.FillSyles)
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim br As System.Drawing.Brush = Nothing

        Select Case m_FillStyle
            Case FillSyles.DiagonalDownLeft
                br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White)
            Case FillSyles.DiagonalDownRight
                br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.ForwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White)
            Case FillSyles.HorizontalBars
                br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.Horizontal, System.Drawing.Color.Black, System.Drawing.Color.White)
            Case FillSyles.None
                br = New System.Drawing.SolidBrush(System.Drawing.Color.White)
            Case FillSyles.PolkaDot
                br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.DottedGrid, System.Drawing.Color.Black, System.Drawing.Color.White)
            Case FillSyles.VerticalBars
                br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.Vertical, System.Drawing.Color.Black, System.Drawing.Color.White)
        End Select

        g.FillRectangle(br, e.Bounds)
    End Sub
End Class

Friend Class LineStyleEditor
    Inherits UITypeEditor
    Private m_LineStyle As LineStyles

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New LineStyleControl(DialogProvider)
        DialogProvider.DropDownControl(MyDialog)
        m_LineStyle = MyDialog.SelectedValue
        frmMain.SetModified(True)

        Return CType(m_LineStyle, MapWinGIS.tkLineStipple)
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        If TypeOf context.Instance Is PolygonSFInfo Then
            m_LineStyle = CType(CType(context.Instance, PolygonSFInfo).LineStyle, MapWindow.LineStyles)
        ElseIf TypeOf context.Instance Is PolylineSFInfo Then
            m_LineStyle = CType(CType(context.Instance, PolylineSFInfo).LineStyle, MapWindow.LineStyles)
        Else
            Return False
        End If
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim tPen As New Pen(System.Drawing.Color.Black)

        Dim ar() As Single = {0.0, 0.3, 0.6, 1.0}

        tPen.CompoundArray = ar
        tPen.Width = 2

        Select Case m_LineStyle
            Case LineStyles.Solid
                tPen.DashStyle = Drawing.Drawing2D.DashStyle.Solid
            Case LineStyles.Dotted
                tPen.DashStyle = Drawing.Drawing2D.DashStyle.Dot
            Case LineStyles.Dashed
                tPen.DashStyle = Drawing.Drawing2D.DashStyle.Dash
            Case LineStyles.DashDot
                tPen.DashStyle = Drawing.Drawing2D.DashStyle.DashDot
        End Select

        g.DrawLine(tPen, e.Bounds.Left + 2, (e.Bounds.Top + e.Bounds.Bottom) \ 2, e.Bounds.Right - 2, (e.Bounds.Top + e.Bounds.Bottom) \ 2)
    End Sub
End Class

Public Class FieldList : Inherits System.ComponentModel.StringConverter
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Dim FieldList As New ArrayList
        Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(frmMain.Legend.SelectedLayer)
        For i As Integer = 0 To sf.NumFields - 1
            If Not FieldList.Contains(sf.Field(i).Name) Then FieldList.Add(sf.Field(i).Name)
        Next
        Return New StandardValuesCollection(FieldList.ToArray())
    End Function

    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Sub New()

    End Sub
End Class

Friend Class PointSizeEditor
    Inherits UITypeEditor
    Private m_PointSize As Double
    Private m_PointStyle As PointStyles

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        m_PointSize = CType(context.Instance, PointSFInfo).PointSize
        m_PointStyle = CType(CType(context.Instance, PointSFInfo).PointStyle, MapWindow.PointStyles)
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New PointSizeControl(DialogProvider, CType(m_PointStyle, MapWinGIS.tkPointType), m_PointSize)
        DialogProvider.DropDownControl(MyDialog)
        m_PointSize = MyDialog.SelectedSize
        frmMain.SetModified(True)

        Return m_PointSize
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        If TypeOf context.Instance Is PolygonSFInfo Then
            m_PointSize = CType(context.Instance, PointSFInfo).PointSize
            m_PointStyle = CType(CType(context.Instance, PointSFInfo).PointStyle, MapWindow.PointStyles)
        Else
            Return False
        End If
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim tBrush As New Drawing.SolidBrush(Color.Black)
        Dim tri(2) As Point, diamond(3) As Point
        Dim midX, midY As Single

        midX = CSng(e.Bounds.X + e.Bounds.Width / 2)
        midY = CSng(e.Bounds.Y + e.Bounds.Height / 2)
        Dim rect As New Rectangle(CInt(midX - m_PointSize / 2), CInt(midY - m_PointSize / 2), CInt(m_PointSize), CInt(m_PointSize))

        Select Case m_PointStyle
            Case PointStyles.Square
                e.Graphics.FillRectangle(tBrush, rect)

            Case PointStyles.Circle
                e.Graphics.FillEllipse(tBrush, rect)

            Case PointStyles.Diamond
                diamond(0).X = CInt(midX)
                diamond(0).Y = rect.Top
                diamond(1).X = rect.Left + rect.Width
                diamond(1).Y = CInt(midY)
                diamond(2).X = CInt(midX)
                diamond(2).Y = rect.Top + rect.Height
                diamond(3).X = rect.Left
                diamond(3).Y = CInt(midY)
                e.Graphics.FillPolygon(tBrush, diamond)

            Case PointStyles.TriangleUp
                tri(0).X = rect.Left
                tri(0).Y = rect.Top + rect.Height
                tri(1).X = CInt(midX)
                tri(1).Y = rect.Top
                tri(2).X = rect.Left + rect.Width
                tri(2).Y = rect.Top + rect.Height
                e.Graphics.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleDown
                tri(0).X = rect.Left
                tri(0).Y = rect.Top
                tri(1).X = rect.Left + rect.Width
                tri(1).Y = rect.Top
                tri(2).X = CInt(midX)
                tri(2).Y = rect.Top + rect.Height
                e.Graphics.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleLeft
                tri(0).X = rect.Left
                tri(0).Y = CInt(midY)
                tri(1).X = rect.Left + rect.Width
                tri(1).Y = rect.Top
                tri(2).X = rect.Left + rect.Width
                tri(2).Y = rect.Top + rect.Height
                e.Graphics.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleRight
                tri(0).X = rect.Left
                tri(0).Y = rect.Top
                tri(1).X = rect.Left + rect.Width
                tri(1).Y = CInt(midY)
                tri(2).X = rect.Left
                tri(2).Y = rect.Top + rect.Height
                e.Graphics.FillPolygon(tBrush, tri)
        End Select
    End Sub
End Class

Friend Class PointStyleEditor
    Inherits UITypeEditor
    Private m_PointStyle As PointStyles

    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Dim DialogProvider As IWindowsFormsEditorService ' This object will start the dialog form
        DialogProvider = CType(provider.GetService(GetType(IWindowsFormsEditorService)), Windows.Forms.Design.IWindowsFormsEditorService)
        Dim MyDialog As New PointStyleControl(DialogProvider)
        DialogProvider.DropDownControl(MyDialog)
        m_PointStyle = MyDialog.SelectedValue
        frmMain.SetModified(True)

        Return CType(m_PointStyle, MapWinGIS.tkPointType)
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        If TypeOf context.Instance Is PolygonSFInfo Then
            m_PointStyle = CType(CType(context.Instance, PointSFInfo).PointStyle, PointStyles)
        Else
            Return False
        End If
        Return True
    End Function

    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim tPen As New Pen(System.Drawing.Color.Black)
        Dim tBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Black)
        Dim tri(2) As Point
        Dim diamond(3) As Point
        Dim drawRect As Rectangle

        If e.Bounds.Width > e.Bounds.Height Then
            drawRect = New Rectangle(CInt((e.Bounds.Width / 2) - (e.Bounds.Height / 2) + 2), e.Bounds.Y + 2, e.Bounds.Height - 4, e.Bounds.Height - 4)
        Else
            drawRect = New Rectangle(e.Bounds.X + 2, CInt((e.Bounds.Height / 2) - (e.Bounds.Width / 2) + 2), e.Bounds.Width - 4, e.Bounds.Width - 4)
        End If

        Select Case m_PointStyle
            Case PointStyles.Circle
                g.FillEllipse(tBrush, drawRect)

            Case PointStyles.Diamond
                diamond(0).X = CInt(e.Bounds.Width / 2)
                diamond(0).Y = 2
                diamond(1).X = e.Bounds.Width - 2
                diamond(1).Y = CInt(e.Bounds.Height / 2)
                diamond(2).X = CInt(e.Bounds.Width / 2)
                diamond(2).Y = e.Bounds.Height - 2
                diamond(3).X = 2
                diamond(3).Y = CInt(e.Bounds.Height / 2)
                g.FillPolygon(tBrush, diamond)

            Case PointStyles.Square
                g.FillRectangle(tBrush, drawRect)

            Case PointStyles.TriangleDown
                tri(0).X = 2
                tri(0).Y = 2
                tri(1).X = e.Bounds.Width - 2
                tri(1).Y = 2
                tri(2).X = CInt(e.Bounds.Width / 2)
                tri(2).Y = e.Bounds.Height - 2
                g.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleLeft
                tri(0).X = e.Bounds.Width - 2
                tri(0).Y = 2
                tri(1).X = e.Bounds.Width - 2
                tri(1).Y = e.Bounds.Height - 2
                tri(2).X = 2
                tri(2).Y = CInt(e.Bounds.Height / 2)
                g.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleRight
                tri(0).X = 2
                tri(0).Y = 2
                tri(1).X = e.Bounds.Width - 2
                tri(1).Y = CInt(e.Bounds.Height / 2)
                tri(2).X = 2
                tri(2).Y = e.Bounds.Height - 2
                g.FillPolygon(tBrush, tri)

            Case PointStyles.TriangleUp
                tri(0).X = CInt(e.Bounds.Width / 2)
                tri(0).Y = 2
                tri(1).X = e.Bounds.Width - 2
                tri(1).Y = e.Bounds.Height - 2
                tri(2).X = 2
                tri(2).Y = e.Bounds.Height - 2
                g.FillPolygon(tBrush, tri)
        End Select
    End Sub
End Class

#End Region

#Region "Layer properties"
' ------------------------------------------------------
' * Generiñ class with properties common for all layers
' ------------------------------------------------------
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class LayerInfo

    Protected m_Handle As Integer

    ''' <summary>
    ''' Creates new instance of LayerInfo class
    ''' </summary>
    ''' <param name="LayerHandle">The handle of layer which properties are due to be displayed</param>
    Public Sub New(ByVal LayerHandle As Integer)
        m_Handle = LayerHandle
    End Sub

    ' DynamicVisibility - toggles the use of dynamic visiblity
    <GlobalizedProperty(CategoryId:="DynamicVisibility"), ReadOnlyAttribute(False)> _
    Public Property UseDynamicVisibility() As Boolean
        Get
            Return frmMain.Layers(m_Handle).UseDynamicVisibility
        End Get
        Set(ByVal Value As Boolean)

            If frmMain.Layers(m_Handle).UseDynamicVisibility <> Value Then
                frmMain.Layers(m_Handle).UseDynamicVisibility = Value
                frmMain.MapMain.Redraw()
                ' TODO: implement
                'frmMain.Plugins.BroadcastMessage("UseTransparencyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ' MaxVisibleScale
    <GlobalizedProperty(CategoryId:="DynamicVisibility"), ReadOnlyAttribute(False)> _
    Public Property MaxVisibleScale() As Double
        Get
            Return frmMain.Layers(m_Handle).MaxVisibleScale
        End Get
        Set(ByVal Value As Double)

            If frmMain.Layers(m_Handle).MaxVisibleScale <> Value Then
                frmMain.Layers(m_Handle).MaxVisibleScale = Value
                frmMain.MapMain.Redraw()
                ' TODO: implement
                'frmMain.Plugins.BroadcastMessage("UseTransparencyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ' MinVisibleScale
    <GlobalizedProperty(CategoryId:="DynamicVisibility"), ReadOnlyAttribute(False)> _
    Public Property MinVisibleScale() As Double
        Get
            Return frmMain.Layers(m_Handle).MinVisibleScale
        End Get
        Set(ByVal Value As Double)

            If frmMain.Layers(m_Handle).MinVisibleScale <> Value Then
                frmMain.Layers(m_Handle).MinVisibleScale = Value
                frmMain.MapMain.Redraw()
                ' TODO: implement
                'frmMain.Plugins.BroadcastMessage("UseTransparencyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

End Class
#End Region

#Region "Property Grid Objects"

' ------------------------------------------Property Grid Objects------------------------------------------------
' -------------------------
' * GRID layer properties *
' -------------------------

'5/7/2008 - attributes added by jk for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class GridInfo
    Inherits LayerInfo

    Public Enum InterpolationMode
        None = 5
        Bilinear = 3
        Bicubic = 4
        HighQualityBilinear = 6
        HighQualityBicubic = 7
    End Enum

    'Private m_Handle As Integer
    Private m_Name As String
    Private m_Filename As String
    Private m_AutoApply As Boolean = True
    Friend m_ColoringScheme As MapWinGIS.GridColorScheme
    Private m_TransparentColor As Color
    Private m_TransparentColor2 As Color
    Private m_LegendPicture As Icon
    Private m_Grid As New MapWinGIS.Grid
    Private m_imageLayerFillTransparency As Single
    Private m_useTransparency As Boolean
    Private m_downsamplingMethod As InterpolationMode
    Private m_upsamplingMethod As InterpolationMode

    Public Sub New(ByVal LayerHandle As Integer)
        MyBase.New(LayerHandle)
        m_Handle = LayerHandle
        m_Filename = frmMain.m_layers(LayerHandle).FileName
        m_Name = frmMain.MapMain.get_LayerName(LayerHandle)
        m_ColoringScheme = CType(frmMain.Layers(m_Handle).ColoringScheme(), MapWinGIS.GridColorScheme)
        Dim img As New MapWinGIS.Image()
        img = CType(frmMain.MapMain.get_GetObject(LayerHandle), MapWinGIS.Image)
        If Not img Is Nothing Then
            Dim transparencyColor As Integer = System.Convert.ToInt32(img.TransparencyColor)
            ' Start Paul Meems May 26 2010
            Dim transparencyColor2 As Integer = System.Convert.ToInt32(img.TransparencyColor2)
            ' End Paul Meems May 26 2010
            Dim r, g, b As Integer
            MapWinUtility.Colors.GetRGB(transparencyColor, r, g, b)
            m_TransparentColor = Color.FromArgb(r, g, b)
            ' Start Paul Meems May 26 2010
            MapWinUtility.Colors.GetRGB(transparencyColor2, r, g, b)
            m_TransparentColor2 = Color.FromArgb(r, g, b)
            ' End Paul Meems May 26 2010
            m_useTransparency = CBool(img.UseTransparencyColor)
            m_downsamplingMethod = img.DownsamplingMode
            m_upsamplingMethod = img.UpsamplingMode
            img = Nothing
        End If
        m_LegendPicture = CType(frmMain.Layers(m_Handle).Icon, Drawing.Icon)
        m_Grid = frmMain.Layers(m_Handle).GetGridObject()
        m_imageLayerFillTransparency = (CInt((frmMain.m_layers(LayerHandle).ImageLayerFillTransparency) * 100)) / 100

    End Sub

    'SYMBOLOGY -- Coloring Scheme
    'Define how to color this grid.
    <PropertyGridUtils.GlobalizedProperty(CategoryId:="SymbologyCategory", _
    DescriptionId:="ColoringSchemeDescription_Grid"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(ColoringSchemeTypeConverter)), _
    Editor(GetType(GridColoringSchemeEditor), GetType(UITypeEditor))> _
    Public Property ColoringScheme() As Object
        Get
            Return m_ColoringScheme
        End Get
        Set(ByVal Value As Object)
            m_ColoringScheme = CType(Value, MapWinGIS.GridColorScheme)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).ColoringScheme = m_ColoringScheme
                frmMain.SetModified(True)
            End If
        End Set
    End Property
    'SYMBOLOGY -- Transparency Percent
    'Sets transparency percent of the grid layer (0 is no transparency, 100 is invisible)
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ImageLayerFillTransparency() As Integer
        Get
            Dim transparencyPercent As Integer = (1 - m_imageLayerFillTransparency) * 100
            Return transparencyPercent
        End Get

        Set(ByVal Value As Integer)

            Dim transparencyPercent As Integer
            transparencyPercent = Value

            If Value < 0 Then
                Value = 0
            ElseIf Value > 100 Then
                Value = 100
            End If
            m_imageLayerFillTransparency = (100 - transparencyPercent) / 100
            frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_imageLayerFillTransparency

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UseTransparencyColor = True
                    img.TransparencyPercent = m_imageLayerFillTransparency
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("TransparentPercentChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property

    'SYMBOLOGY -- Transparency Color
    'Sets the start color that will be transparent when using transparency on an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property TransparentColor() As Color
        Get
            Return m_TransparentColor
        End Get
        Set(ByVal Value As Color)
            If m_AutoApply = True And Not m_TransparentColor.Equals(Value) Then
                m_TransparentColor = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.TransparencyColor = System.Convert.ToUInt32(RGB(m_TransparentColor.R, m_TransparentColor.G, m_TransparentColor.B))
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    If m_useTransparency Then frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("TransparentColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Transparency Color2
    'Sets the end color that will be transparent when using transparency on an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property TransparentColor2() As Color
        Get
            Return m_TransparentColor2
        End Get
        Set(ByVal Value As Color)
            If m_AutoApply = True And Not m_TransparentColor2.Equals(Value) Then
                m_TransparentColor2 = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.TransparencyColor2 = System.Convert.ToUInt32(RGB(m_TransparentColor2.R, m_TransparentColor2.G, m_TransparentColor2.B))
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    If m_useTransparency Then frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("TransparentColor2Changed Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Use Transparency
    'Toggles the use of transparency with an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property UseTransparency() As Boolean
        Get
            Return m_useTransparency
        End Get
        Set(ByVal Value As Boolean)

            If m_AutoApply = True And m_useTransparency <> Value Then
                m_useTransparency = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UseTransparencyColor = m_useTransparency
                    frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("UseTransparencyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property
    'Grid value downsampling method
    'Determines the downsampling method to be used in displaying rasters.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ImageLayerDownsamplingMethod() As InterpolationMode
        Get
            Return m_downsamplingMethod
        End Get

        Set(ByVal Value As InterpolationMode)

            'TODO: Need to find a way to assign downsamplingmode as a property of layer.
            'frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_imageLayerFillTransparency

            m_downsamplingMethod = Value

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.DownsamplingMode = Value
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("ImageLayerDownSamplingModeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ImageLayerUpsamplingMethod() As InterpolationMode
        Get
            Return m_upsamplingMethod
        End Get

        Set(ByVal Value As InterpolationMode)

            'TODO: Need to find a way to assign downsamplingmode as a property of layer.
            'frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_imageLayerFillTransparency

            m_upsamplingMethod = Value

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UpsamplingMode = Value
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("ImageLayerUpsamplingModeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory")> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Icon = Value
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)

            If m_AutoApply = True And Value <> m_Name Then
                m_Name = Value
                frmMain.MapMain.set_LayerName(m_Handle, m_Name)
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- File Name
    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property


    'LAYER PROPERTIES -- Bounding Box (Min X, Max X)
    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Y, Max Y)
    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
DisplayName("Bounding Box (Min Y, Max Y)"), _
Description("The Y dimensions of the bounding box."), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Z, Max Z)
    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Projection
    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

    'LAYER PROPERTIES -- Handle
    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    'LAYER PROPERTIES -- Cell Size X
    'The The width of each cell.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property CellSizeX() As Double
        Get
            Return m_Grid.Header.dX
        End Get
    End Property

    'LAYER PROPERTIES -- Cell Size Y
    'The height of each cell.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property CellSizeY() As Double
        Get
            Return m_Grid.Header.dY
        End Get
    End Property

    'LAYER PROPERTIES -- Rows
    'The number of rows in the grid.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Rows() As Long
        Get
            Return m_Grid.Header.NumberRows
        End Get
    End Property

    'LAYER PROPERTIES -- Comments
    'Free-form comments about this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(False)> _
Public Property Tag() As String
        Get
            Return frmMain.Layers(m_Handle).Tag
        End Get
        Set(ByVal value As String)
            frmMain.Layers(m_Handle).Tag = value
        End Set
    End Property

    'LAYER PROPERTIES -- Columns
    'The number of columns in the grid.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Cols() As Long
        Get
            Return m_Grid.Header.NumberCols
        End Get
    End Property

    'LAYER PROPERTIES -- No Data Value
    'The value in the grid that represents missing data.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property NodataValue() As Double
        Get
            Return m_Grid.Header.NodataValue
        End Get
    End Property


    'LAYER PROPERTIES -- Data Type
    'The type of data in this grid.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property GridDataType() As String '5/11/2008 jk: name of property changed from Cols() to GridDataType()
        Get
            Select Case m_Grid.DataType
                Case MapWinGIS.GridDataType.DoubleDataType
                    Return "Double-Precision Floating Point"
                Case MapWinGIS.GridDataType.FloatDataType
                    Return "Single-Precision Floating Point"
                Case MapWinGIS.GridDataType.InvalidDataType
                    Return "Invalid/Unknown"
                Case MapWinGIS.GridDataType.LongDataType
                    Return "Long Integer"
                Case MapWinGIS.GridDataType.ShortDataType
                    Return "Short Integer"
                Case MapWinGIS.GridDataType.ByteDataType
                    Return "Byte"
                Case MapWinGIS.GridDataType.UnknownDataType
                    Return "Unknown"
            End Select

            Return "Unknown"
        End Get
    End Property

    'LAYER PROPERTIES -- Minimum Value
    'The smallest value found in the grid.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property MinVal() As Double
        Get
            Return m_Grid.Minimum
        End Get
    End Property

    'LAYER PROPERTIES -- Maximum Value
    'The largest value found in the grid.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property MaxVal() As Double
        Get
            Return m_Grid.Maximum
        End Get
    End Property

    ''SYMBOLOGY -- Dynamic Visibility
    ''Defines the zoom level at which this layer becomes visible.
    '<GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    'ReadOnlyAttribute(False), _
    'TypeConverter(GetType(DynamicVisibilityTypeConverter)), _
    'Editor(GetType(DynamicVisibilityEditor), GetType(UITypeEditor))> _
    'Public Property DynamicVisibility() As Object
    '    Get
    '        If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
    '            Return frmMain.m_AutoVis(m_Handle).UseDynamicExtents
    '        Else
    '            Return False
    '        End If
    '    End Get
    '    Set(ByVal Value As Object)
    '        If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
    '            frmMain.m_AutoVis(m_Handle).UseDynamicExtents = CBool(Value)
    '            frmMain.Plugins.BroadcastMessage("DynamicVisibilityChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
    '            frmMain.SetModified(True)
    '        End If
    '    End Set
    'End Property
End Class
#End Region

#Region "Properties Image Objects"
'------------------------------------------------------------------------------------------------------------------------------
' -------------------------
' * IMAGE layer properties *
' -------------------------

'5/11/2008 Jiri Kadlec: attributes added for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class ImageInfo
    Inherits LayerInfo

    Public Enum InterpolationMode
        None = 5
        Bilinear = 3
        Bicubic = 4
        HighQualityBilinear = 6
        HighQualityBicubic = 7
    End Enum

    'Private m_TransparentColor As Color
    Private m_UseTransparency As Boolean = True
    Private m_ImageLayerFillTransparency As Single
    Private m_UseHistogram As Boolean = False
    Private m_AllowHillshade As Boolean = True
    Private m_BufferSize As Integer = 100
    Private m_SetToGrey As Boolean = False
    Private m_ImageColorScheme As MapWinGIS.PredefinedColorScheme = MapWinGIS.PredefinedColorScheme.FallLeaves
    'Private m_Handle As Integer
    Private m_Name As String
    Private m_Filename As String
    Private m_LegendPicture As Icon
    Private m_AutoApply As Boolean = True
    Private m_TransparentColor As Color
    Private m_TransparentColor2 As Color
    Private m_downsamplingMethod As InterpolationMode
    Private m_upsamplingMethod As InterpolationMode

    Public Sub New(ByVal LayerHandle As Integer)
        MyBase.New(LayerHandle)

        Dim img As MapWinGIS.Image

        img = CType(frmMain.MapMain.get_GetObject(LayerHandle), MapWinGIS.Image)
        If Not img Is Nothing Then
            Dim transparencyColor As Integer = System.Convert.ToInt32(img.TransparencyColor)
            ' Start Paul Meems May 26 2010
            Dim transparencyColor2 As Integer = System.Convert.ToInt32(img.TransparencyColor2)
            ' End Paul Meems May 26 2010
            Dim r, g, b As Integer
            MapWinUtility.Colors.GetRGB(transparencyColor, r, g, b)
            m_TransparentColor = Color.FromArgb(r, g, b)
            ' Start Paul Meems May 26 2010
            MapWinUtility.Colors.GetRGB(transparencyColor2, r, g, b)
            m_TransparentColor2 = Color.FromArgb(r, g, b)
            ' End Paul Meems May 26 2010            
            m_UseTransparency = CBool(img.UseTransparencyColor)
            m_ImageLayerFillTransparency = (CInt((frmMain.m_layers(LayerHandle).ImageLayerFillTransparency) * 100)) / 100
            m_UseHistogram = CBool(img.UseHistogram)
            m_AllowHillshade = CBool(img.AllowHillshade)
            m_SetToGrey = CBool(img.SetToGrey)
            m_BufferSize = CInt(img.BufferSize)
            m_ImageColorScheme = img.ImageColorScheme
            m_Handle = LayerHandle
            m_Name = frmMain.MapMain.get_LayerName(LayerHandle)
            m_Filename = frmMain.m_layers(LayerHandle).FileName
            m_downsamplingMethod = img.DownsamplingMode
            m_upsamplingMethod = img.UpsamplingMode
            img = Nothing
            m_LegendPicture = CType(frmMain.Layers(m_Handle).Icon, Icon)
        End If
    End Sub

    'LAYER PROPERTIES -- Bounding Box (Min X, Max X)
    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Y, Max Y)
    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Z, Max Z)
    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory")> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Icon = Value
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property ImageLayerFillTransparency() As Integer
        Get
            Dim transparencyPercent As Integer = (1 - m_ImageLayerFillTransparency) * 100
            Return transparencyPercent
        End Get

        Set(ByVal Value As Integer)

            Dim transparencyPercent As Integer
            transparencyPercent = Value

            If Value < 0 Then
                Value = 0
            ElseIf Value > 100 Then
                Value = 100
            End If
            m_ImageLayerFillTransparency = (100 - transparencyPercent) / 100
            frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_ImageLayerFillTransparency

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UseTransparencyColor = True
                    img.TransparencyPercent = m_ImageLayerFillTransparency
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("TransparentPercentChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    'SYMBOLOGY -- Transparency Color
    'Sets the starting color that will be transparent when using transparency on an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property TransparentColor() As Color
        Get
            Return m_TransparentColor
        End Get
        Set(ByVal Value As Color)
            If m_AutoApply = True And Not m_TransparentColor.Equals(Value) Then
                m_TransparentColor = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.TransparencyColor = System.Convert.ToUInt32(RGB(m_TransparentColor.R, m_TransparentColor.G, m_TransparentColor.B))
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    If m_UseTransparency Then frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("TransparentColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property
    'SYMBOLOGY -- Transparency Color2
    'Sets the ending color that will be transparent when using transparency on an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property TransparentColor2() As Color
        Get
            Return m_TransparentColor2
        End Get
        Set(ByVal Value As Color)
            If m_AutoApply = True And Not m_TransparentColor2.Equals(Value) Then
                m_TransparentColor2 = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.TransparencyColor2 = System.Convert.ToUInt32(RGB(m_TransparentColor2.R, m_TransparentColor2.G, m_TransparentColor2.B))
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    If m_UseTransparency Then frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("TransparentColor2Changed Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Use Transparency
    'Toggles the use of transparency with an image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property UseTransparency() As Boolean
        Get
            Return m_UseTransparency
        End Get
        Set(ByVal Value As Boolean)

            If m_AutoApply = True And m_UseTransparency <> Value Then
                m_UseTransparency = Value
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UseTransparencyColor = m_UseTransparency
                    frmMain.MapMain.Redraw()
                End If
                frmMain.Plugins.BroadcastMessage("UseTransparencyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property
    'Grid value downsampling method
    'Determines the downsampling method to be used in displaying rasters.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ImageLayerDownsamplingMethod() As InterpolationMode
        Get
            Return m_downsamplingMethod
        End Get

        Set(ByVal Value As InterpolationMode)

            'TODO: Need to find a way to assign downsamplingmode as a property of layer.
            'frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_imageLayerFillTransparency

            m_downsamplingMethod = Value

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.DownsamplingMode = Value
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("ImageLayerDownSamplingModeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ImageLayerUpsamplingMethod() As InterpolationMode
        Get
            Return m_upsamplingMethod
        End Get

        Set(ByVal Value As InterpolationMode)

            'TODO: Need to find a way to assign downsamplingmode as a property of layer.
            'frmMain.Layers(m_Handle).ImageLayerFillTransparency = m_imageLayerFillTransparency

            m_upsamplingMethod = Value

            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UpsamplingMode = Value
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                    frmMain.MapMain.Redraw()
                    frmMain.Plugins.BroadcastMessage("ImageLayerUpsamplingModeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property

    'SYMBOLOGY -- Use Histogram Equalization
    'Toggles the use of histogram equalization with a greyscale image.
    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property UseHistogram() As Boolean
        Get
            Return m_UseHistogram
        End Get
        Set(ByVal Value As Boolean)
            m_UseHistogram = Value
            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.UseHistogram = m_UseHistogram
                    frmMain.Plugins.BroadcastMessage("UseHistogramChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.MapMain.Redraw()
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    'SYMBOLOGY -- Set image to grey
    'Display as greyscale.
    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property SetToGrey() As Boolean
        Get
            Return m_SetToGrey
        End Get
        Set(ByVal Value As Boolean)
            m_SetToGrey = Value
            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.SetToGrey = m_SetToGrey
                    frmMain.Plugins.BroadcastMessage("SetToGreyChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.MapMain.Redraw()
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    'SYMBOLOGY -- Allow hillshade
    'Allows the image to be displayed as a grid with hillshading. Single band images with non-byte data and no color table are hillshaded by default. (16-bit unsigned data images are not hillshaded because they are regarded as Quickbird satellite imagery).
    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property AllowHillshade() As Boolean
        Get
            Return m_AllowHillshade
        End Get
        Set(ByVal Value As Boolean)
            m_AllowHillshade = Value
            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.AllowHillshade = m_AllowHillshade
                    frmMain.Plugins.BroadcastMessage("AllowHillshadeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.MapMain.Redraw()
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property
    'SYMBOLOGY -- Buffer size
    'Set the image buffersize 1-100 (has no effect on bitmap images)
    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property BufferSize() As Integer
        Get
            Return m_BufferSize
        End Get
        Set(ByVal Value As Integer)
            m_BufferSize = Value
            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.BufferSize = m_BufferSize
                    frmMain.Plugins.BroadcastMessage("BufferSizeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.MapMain.Redraw()
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property

    'SYMBOLOGY -- Image Color Scheme
    'Sets the predefined color scheme if the image is hillshaded.
    <GlobalizedProperty(CategoryId:="SymbologyCategory")> _
    Public Property ImageColorScheme() As MapWinGIS.PredefinedColorScheme
        Get
            Return m_ImageColorScheme
        End Get
        Set(ByVal Value As MapWinGIS.PredefinedColorScheme)
            m_ImageColorScheme = Value
            If m_AutoApply = True Then
                Dim img As MapWinGIS.Image = CType(frmMain.MapMain.get_GetObject(m_Handle), MapWinGIS.Image)
                If Not img Is Nothing Then
                    img.ImageColorScheme = m_ImageColorScheme
                    frmMain.Plugins.BroadcastMessage("ImageColorSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                    frmMain.MapMain.Redraw()
                    frmMain.SetModified(True)
                End If
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- Comments
    'Free-form comments about this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Tag() As String
        Get
            Return frmMain.Layers(m_Handle).Tag
        End Get
        Set(ByVal value As String)
            frmMain.Layers(m_Handle).Tag = value
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
            If m_AutoApply = True Then
                frmMain.MapMain.set_LayerName(m_Handle, m_Name)
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- File Name
    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    'LAYER PROPERTIES -- Handle
    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    'LAYER PROPERTIES -- Projection
    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

End Class
#End Region

#Region "Properties point shapefile objects"
' ------------------------------------
' * POINT SHAPEFILE layer properties *
' ------------------------------------

'May/11/2008 Jiri Kadlec: attributes added for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class PointSFInfo
    Implements IDisposable
    ' coloring related properties
    Friend m_Legend As MapWinGIS.ShapefileColorScheme
    Private m_PointStyle As MapWinGIS.tkPointType
    Private m_PointColor As Color

    ' read/write layer properties
    Private m_PointSize As Double
    Private m_Name As String

    ' read-only layer properties
    Private m_Extents As MapWinGIS.Extents
    Private m_Filename As String
    Private m_Handle As Integer
    Private m_LayerType As MapWindow.Interfaces.eLayerType

    ' Legend properties
    Private m_Expanded As Boolean
    Private m_LegendPicture As Icon
    Private m_MapBitmap As Bitmap
    Private m_LabelfieldChanged As Boolean = False

    'Chris Michaelis 11/12/2006 - Point image scheme
    Friend m_PointImageScheme As MapWindow.Interfaces.ShapefilePointImageScheme

    ' Internal to this class
    Private m_AutoApply As Boolean

    '---------------------------------------------------------------------------------
    Public Sub Dispose() Implements IDisposable.Dispose
        'Save the whole thing into disk when the field is changed
        If Me.m_LabelfieldChanged Then
            MapWinUtility.Logger.Dbg("DEBUG: " + "Saving the whole lable config file")

        Else 'Only save the first line of the config file to save the font change of the labels.
            MapWinUtility.Logger.Dbg("DEBUG: " + "Only saving the first line of the config file")
        End If
    End Sub

    'LAYER PROPERTIES -- Comments
    'Free-form comments about this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Tag() As String
        Get
            Return frmMain.Layers(m_Handle).Tag
        End Get
        Set(ByVal value As String)
            frmMain.Layers(m_Handle).Tag = value
        End Set
    End Property

    Public Sub New(ByVal LayerHandle As Integer)
        'Dim r, g, b As Integer

        m_Handle = LayerHandle
        m_AutoApply = True

        LabelEditor.handle = LayerHandle

        With frmMain.m_layers(LayerHandle)
            ' coloring related properties
            m_Legend = CType(.ColoringScheme, MapWinGIS.ShapefileColorScheme)
            If m_Legend Is Nothing Then
                m_Legend = New MapWinGIS.ShapefileColorScheme()
            End If
            m_PointStyle = .PointType
            If .PointType = MapWinGIS.tkPointType.ptUserDefined Then
                Dim cvter As New MapWinUtility.ImageUtils
                m_MapBitmap = CType(cvter.IPictureDispToImage(.UserPointType.Picture), Bitmap)
            Else
                m_PointColor = .Color
            End If

            ' read/write layer properties
            m_PointSize = .LineOrPointSize
            m_Name = .Name

            ' read-only layer properties
            m_Extents = .Extents
            m_Filename = .FileName
            m_Handle = .Handle
            m_LayerType = .LayerType
            m_PointImageScheme = frmMain.Legend.Layers.ItemByHandle(m_Handle).PointImageScheme

            m_Expanded = .Expanded
            If TypeOf (.Icon) Is Icon Then
                m_LegendPicture = CType(.Icon, Icon)
            ElseIf TypeOf (.Icon) Is Bitmap Then
                m_LegendPicture = Icon.FromHandle(CType(.Icon, Bitmap).GetHbitmap())
            End If
        End With
    End Sub

    '---------------------------------------------------------------------------------
    'SYMBOLOGY -- Coloring Scheme
    'Define how to color these shapes.
    <GlobalizedProperty(CategoryId:="SymbologyCategory", _
    DescriptionId:="ColoringSchemeDescription_Shape"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(ColoringSchemeTypeConverter)), _
    Editor(GetType(SFColoringSchemeEditor), GetType(UITypeEditor))> _
    Public Property ColoringScheme() As Object
        Get
            'This is a get property. why are we setting?
            'Causes other settings to be overridden
            'If m_AutoApply = True Then
            '    If m_MapBitmap Is Nothing Then
            '        If m_Legend Is Nothing OrElse m_Legend.NumBreaks = 0 Then
            '            frmMain.m_layers(m_Handle).Color = m_PointColor
            '        Else
            '            frmMain.m_layers(m_Handle).ColoringScheme = m_Legend
            '        End If
            '        frmMain.m_layers(m_Handle).ColoringScheme = m_Legend
            '    End If
            'End If
            Return m_Legend

        End Get
        Set(ByVal Value As Object)
            m_Legend = CType(Value, MapWinGIS.ShapefileColorScheme)
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).ColoringScheme = Value
                frmMain.SetModified(True)
                If m_MapBitmap Is Nothing Then
                    If m_Legend Is Nothing OrElse m_Legend.NumBreaks = 0 Then
                        frmMain.m_layers(m_Handle).Color = m_PointColor
                        frmMain.SetModified(True)
                    End If
                End If
            End If
        End Set
    End Property

    'Added by Lailin Chen May-19-2006 to use USU Labeler Plugin-in in Lengend Editor to edit labels
    'SYMBOLOGY -- Label Setup
    'Configure how labels are displayed for this layer.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(LabelerTypeConverter)), _
    Editor(GetType(LabelEditor), GetType(UITypeEditor))> _
    Public Property LabelingSetup() As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)
        End Set
    End Property

    'SYMBOLOGY -- Labels Visible
    'Indicates whether labels (if any have been set up or added) are visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property LabelsVisible() As Boolean
        Get
            Return frmMain.Layers(m_Handle).LabelsVisible
        End Get
        Set(ByVal Value As Boolean)
            frmMain.SetModified(True)
            frmMain.Layers(m_Handle).LabelsVisible = Value
            frmMain.Plugins.BroadcastMessage("LabelsVisibleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
        End Set
    End Property

    'SYMBOLOGY -- Point Style
    'The point style used to display the point shapes.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    Editor(GetType(PointStyleEditor), GetType(UITypeEditor))> _
    Public Property PointStyle() As MapWinGIS.tkPointType
        Get
            Return m_PointStyle
        End Get
        Set(ByVal Value As MapWinGIS.tkPointType)
            m_PointStyle = Value
            m_MapBitmap = Nothing
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).PointType = m_PointStyle
                frmMain.MapMain.Redraw()
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("PointStyleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Point Color
    'The color used to display the points
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property PointColor() As Color
        Get
            Return m_PointColor
        End Get
        Set(ByVal Value As Color)
            m_PointColor = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Color = Value
                frmMain.MapMain.Redraw()
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("PointColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ' read/write layer properties
    'SYMBOLOGY -- Point Size
    'The size of the points in pixels.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    Description("The size of the points in pixels."), _
    DefaultValueAttribute(3.0), _
    Editor(GetType(PointSizeEditor), GetType(UITypeEditor))> _
    Public Property PointSize() As Double
        Get
            Return m_PointSize
        End Get
        Set(ByVal Value As Double)
            m_PointSize = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).LineOrPointSize = CSng(Value)
                frmMain.MapMain.Redraw()
                frmMain.Plugins.BroadcastMessage("PointSizeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
            If m_AutoApply = True Then
                frmMain.MapMain.set_LayerName(m_Handle, Value)
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property


    'LAYER PROPERTIES -- File Name
    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    Description("The physical file behind this layer."), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min X, Max X)
    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Y, Max Y)
    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Z, Max Z)
    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Handle
    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    'LAYER PROPERTIES -- Projection
    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

    'LEGEND PROPERTIES -- Expanded Legend
    'Indicates whether any additional data (i.e., coloring scheme) should be shown expanded in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Expanded() As Boolean
        Get
            Return m_Expanded
        End Get
        Set(ByVal Value As Boolean)
            m_Expanded = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Expanded = Value
                frmMain.Plugins.BroadcastMessage("LegendExpanded Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Icon = Value
                frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ' Read/write layer properties
    'SYMBOLOGY -- Map Tooltip Field
    'The field to display for Map Tooltips.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute("None"), _
    TypeConverter(GetType(FieldList))> _
    Public Property MapTooltipField() As String
        Get
            Try
                Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
                Return sf.Field(frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex).Name
            Catch
                Return "None"
            End Try
        End Get
        Set(ByVal Value As String)
            Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
            For i As Integer = 0 To sf.NumFields - 1
                If sf.Field(i).Name.ToLower().Trim() = Value.ToLower().Trim() Then
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex = i
                End If
            Next
        End Set
    End Property


    'SYMBOLOGY -- Map Tooltips Enabled
    'Sets whether or not to show map tooltips for the specified field.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
     ReadOnlyAttribute(False)> _
     Public Property MapTooltipsEnabled() As Boolean
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled
        End Get
        Set(ByVal value As Boolean)
            If MapTooltipField = "None" Then
                MsgBox("Please select a field to use for Map Tooltips before turning them on.", MsgBoxStyle.Information, "Select Field First")
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = False
            Else
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = value
            End If
            frmMain.UpdateMapToolTipsAtLeastOneLayer()
        End Set
    End Property

    'MAP BITMAP -- Default Point Image
    'The image which should be used to display this point on the map, unless overridden by an Image Scheme.
    <GlobalizedProperty(CategoryId:="MapBitmapCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property MapBitmap() As Bitmap
        Get
            Return m_MapBitmap
        End Get
        Set(ByVal Value As Bitmap)
            m_MapBitmap = Value
            Try
                If m_AutoApply = True Then
                    If frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme IsNot Nothing Then
                        Dim MsgBoxAnswer As MsgBoxResult = _
                                MsgBox("Coloring scheme already exists." + ControlChars.NewLine + _
                                    "If you apply this change, the existing coloring scheme will be deleted." + ControlChars.NewLine + ControlChars.NewLine + _
                                    "Would you like to go ahead?", MsgBoxStyle.YesNo, "Coloring scheme warning")

                        If MsgBoxAnswer = MsgBoxResult.Yes Then
                            With frmMain.m_layers(m_Handle)
                                .ColoringScheme = Nothing
                                ' May/03/2010 DK === The size property runs diffrently for user defined images
                                ' the size of the icon will be multiplied by the size property.
                                ' For other type of point symbol, the size property is simply the pixel of the 
                                ' symbol to be displayed.
                                .LineOrPointSize = 1
                                .PointType = MapWinGIS.tkPointType.ptUserDefined
                                Dim tImg As MapWinGIS.Image = New MapWinGIS.Image
                                Dim cvter As New MapWinUtility.ImageUtils
                                tImg.Picture = CType(cvter.ImageToIPictureDisp(m_MapBitmap), stdole.IPictureDisp)
                                'Try to find transparency color from the first pixel
                                tImg.TransparencyColor = tImg.Value(0, 0)
                                .UserPointType = tImg
                            End With
                            frmMain.Plugins.BroadcastMessage("MapBitmapChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                            frmMain.SetModified(True)
                        End If
                    End If
                End If
            Catch
                MapWinUtility.Logger.Msg("Could not load the specified image", "MapWindow Legend Editor")
            End Try
        End Set
    End Property

    'MAP BITMAP -- Transparent Color
    'The color in the Default Point Image that should be considered transparent.
    <GlobalizedProperty(CategoryId:="MapBitmapCategory", _
    DescriptionId:="TransparentColorDescription_Point")> _
    Public Property TransparentColor() As Color
        Get
            If Not frmMain.m_layers(m_Handle).UserPointType Is Nothing AndAlso frmMain.m_layers(m_Handle).PointType = MapWinGIS.tkPointType.ptUserDefined Then
                Return MapWinUtility.Colors.IntegerToColor(frmMain.m_layers(m_Handle).UserPointType.TransparencyColor)
            Else
                Return Color.White
            End If
        End Get
        Set(ByVal Value As Color)
            If Not frmMain.m_layers(m_Handle).UserPointType Is Nothing AndAlso frmMain.m_layers(m_Handle).PointType = MapWinGIS.tkPointType.ptUserDefined Then
                frmMain.m_layers(m_Handle).UserPointType.TransparencyColor = MapWinUtility.Colors.ColorToUInteger(Value)
                frmMain.m_layers(m_Handle).UserPointType = frmMain.m_layers(m_Handle).UserPointType
                frmMain.Plugins.BroadcastMessage("TransparentColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.MapMain.Redraw()
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'MAP BITMAP -- Point Image Scheme
    'Sets up a scheme defining an image to show for each point.
    <GlobalizedProperty(CategoryId:="MapBitmapCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(PointImageSchemeTypeConverter)), _
    Editor(GetType(PointImageSchemeEditor), GetType(UITypeEditor))> _
    Public Property PointImages() As Object
        Get
            Return m_PointImageScheme
        End Get
        Set(ByVal Value As Object)
            m_PointImageScheme = Value
            frmMain.Legend.Layers.ItemByHandle(m_Handle).PointImageScheme = Value
            frmMain.SetModified(True)
        End Set
    End Property

    'SYMBOLOGY -- Dynamic Visibility
    'Defines the zoom level at which this layer becomes visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    TypeConverter(GetType(DynamicVisibilityTypeConverter)), _
    Editor(GetType(DynamicVisibilityEditor), GetType(UITypeEditor))> _
    Public Property DynamicVisibility() As Object
        Get
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                Return frmMain.m_AutoVis(m_Handle).UseDynamicExtents
            Else
                Return False
            End If
        End Get
        Set(ByVal Value As Object)
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                frmMain.m_AutoVis(m_Handle).UseDynamicExtents = CBool(Value)
                frmMain.Plugins.BroadcastMessage("DynamicVisibilityChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property
End Class
#End Region

#Region "Properties polyline shapefile objects"
' -------------------------------------------------------------------------------------------------------------
' ---------------------------------------
' * POLYLINE SHAPEFILE layer properties *
' ---------------------------------------
'
'May/11/2008 Jiri Kadlec: attributes added for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class PolylineSFInfo
    Implements IDisposable
    ' coloring related properties
    Friend m_Legend As MapWinGIS.ShapefileColorScheme
    Private m_LineStyle As MapWinGIS.tkLineStipple
    Private m_LineColor As Color

    ' read/write layer properties
    Private m_LineWidth As Integer
    Private m_Name As String
    Private m_Tag As String

    ' read-only layer properties
    Private m_Extents As MapWinGIS.Extents
    Private m_Filename As String
    Private m_Handle As Integer
    Private m_LayerType As MapWindow.Interfaces.eLayerType
    Private m_Shapes As MapWindow.Interfaces.Shapes

    ' Legend properties
    Private m_Expanded As Boolean
    Private m_LegendPicture As Icon
    Private m_LabelfieldChanged As Boolean = False

    ' Internal to this class
    Private m_Layer As MapWindow.Layer
    Private m_AutoApply As Boolean
    '---------------------------------------------------------------------------------
    Public Sub Dispose() Implements IDisposable.Dispose
        'Save the whole thing into disk when the field is changed
        If Me.m_LabelfieldChanged Then
            MapWinUtility.Logger.Dbg("DEBUG: " + "Saving the whole label config file")

        Else 'Only save the first line of the config file to save the font change of the labels.
            MapWinUtility.Logger.Dbg("DEBUG: " + "Only saving the first line of the config file")
        End If

    End Sub

    Public Sub New(ByVal LayerHandle As Integer)
        m_Handle = LayerHandle
        m_AutoApply = True
        LabelEditor.handle = LayerHandle

        With frmMain.m_layers(LayerHandle)
            ' coloring related properties
            m_Legend = CType(.ColoringScheme, MapWinGIS.ShapefileColorScheme)
            If m_Legend Is Nothing Then
                m_Legend = New MapWinGIS.ShapefileColorScheme()
            End If
            m_LineStyle = .LineStipple
            m_LineColor = .Color

            ' read/write layer properties
            m_LineWidth = CInt(.LineOrPointSize)
            m_Name = .Name
            m_Tag = .Tag

            ' read-only layer properties
            m_Extents = .Extents
            m_Filename = .FileName
            m_Handle = .Handle
            m_LayerType = .LayerType
            m_Shapes = .Shapes
            m_LegendPicture = CType(.Icon, Icon)
            m_Expanded = .Expanded
        End With
    End Sub

    '---------------------------------------------------------------------------------

    'SYMBOLOGY -- Coloring Scheme
    'Define how to color these shapes.
    <GlobalizedProperty(CategoryId:="SymbologyCategory", _
    DescriptionId:="ColoringSchemeDescription_Shape"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(ColoringSchemeTypeConverter)), _
    Editor(GetType(SFColoringSchemeEditor), GetType(UITypeEditor))> _
    Public Property ColoringScheme() As Object
        Get
            'This is a get property. No setting things here...
            'If m_AutoApply = True Then
            'frmMain.m_layers(m_Handle).ColoringScheme = m_Legend
            'End If

            Return m_Legend

        End Get
        Set(ByVal Value As Object)
            m_Legend = CType(Value, MapWinGIS.ShapefileColorScheme)
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).ColoringScheme = m_Legend
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Label Setup
    'Configure how labels are displayed for this layer.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(LabelerTypeConverter)), _
    Editor(GetType(LabelEditor), GetType(UITypeEditor))> _
    Public Property LabelingSetup() As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)
        End Set
    End Property

    'SYMBOLOGY -- Labels Visible
    'Indicates whether labels (if any have been set up or added) are visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property LabelsVisible() As Boolean
        Get
            Return frmMain.Layers(m_Handle).LabelsVisible
        End Get
        Set(ByVal Value As Boolean)
            frmMain.SetModified(True)
            frmMain.Layers(m_Handle).LabelsVisible = Value
            frmMain.Plugins.BroadcastMessage("LabelsVisibleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
        End Set
    End Property

    'SYMBOLOGY -- Line Style
    'The line style used on the lines in the shapefile.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    Editor(GetType(LineStyleEditor), GetType(UITypeEditor))> _
    Public Property LineStyle() As MapWinGIS.tkLineStipple
        Get
            Return m_LineStyle
        End Get
        Set(ByVal Value As MapWinGIS.tkLineStipple)
            m_LineStyle = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).LineStipple = Value
                frmMain.Plugins.BroadcastMessage("LineStyleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Vertex Color
    'Color vertices are drawn with when Vertices Visible is True.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property VertexColor() As Color
        Get
            Return frmMain.MapMain.get_ShapeLayerPointColor(m_Handle)
        End Get
        Set(ByVal Value As Color)
            frmMain.MapMain.set_ShapeLayerPointColor(m_Handle, Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Value)))
        End Set
    End Property

    'SYMBOLOGY -- Vertex Size
    'Size (in pixels) that vertices are drawn with when Vertices Visible is True.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property VertexSize() As Integer
        Get
            Return frmMain.MapMain.get_ShapeLayerPointSize(m_Handle)
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.set_ShapeLayerPointSize(m_Handle, CSng(Value))
        End Set
    End Property

    'SYMBOLOGY -- Vertices Visible
    'Display the vertices of the shapefile?
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property VerticesVisible() As Boolean
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_Handle).VerticesVisible
        End Get
        Set(ByVal Value As Boolean)
            frmMain.Legend.Layers.ItemByHandle(m_Handle).VerticesVisible = Value
            frmMain.MapMain.set_ShapeLayerDrawPoint(m_Handle, Value)
        End Set
    End Property

    'SYMBOLOGY -- Line Color
    'The color to use when drawing lines in this shapefile.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property LineColor() As Color
        Get
            Return m_LineColor
        End Get
        Set(ByVal Value As Color)
            m_LineColor = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Color = Value
                frmMain.Plugins.BroadcastMessage("LineColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Line Width
    'The width of the lines. This property does not apply when using a line style.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute(1), _
    Editor(GetType(LineWidthEditor), GetType(UITypeEditor))> _
    Public Property LineWidth() As Integer
        Get
            Return m_LineWidth
        End Get
        Set(ByVal Value As Integer)
            m_LineWidth = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).LineOrPointSize = Value
                frmMain.Plugins.BroadcastMessage("LineWidthChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- Comments
    'Free-form comments about this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Tag() As String
        Get
            Return frmMain.Layers(m_Handle).Tag
        End Get
        Set(ByVal value As String)
            frmMain.Layers(m_Handle).Tag = value
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Name = Value
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- File Name
    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min X, Max X)
    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Y, Max Y)
    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Z, Max Z)
    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Handle
    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    'LAYER PROPERTIES -- Projection
    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

    ' Legend properties
    'LEGEND PROPERTIES -- Expanded Legend
    'Indicates whether any additional data (i.e., coloring scheme) should be shown expanded in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Expanded() As Boolean
        Get
            Return m_Expanded
        End Get
        Set(ByVal Value As Boolean)
            m_Expanded = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Expanded = Value
                frmMain.Plugins.BroadcastMessage("LegendExpanded Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ' Read/write layer properties
    'SYMBOLOGY -- Map Tooltip Field
    'The field to display for Map Tooltips.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute("None"), _
    TypeConverter(GetType(FieldList))> _
    Public Property MapTooltipField() As String
        Get
            Try
                Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
                Return sf.Field(frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex).Name
            Catch
                Return "None"
            End Try
        End Get
        Set(ByVal Value As String)
            Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
            For i As Integer = 0 To sf.NumFields - 1
                If sf.Field(i).Name.ToLower().Trim() = Value.ToLower().Trim() Then
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex = i
                End If
            Next
        End Set
    End Property

    'SYMBOLOGY -- Map Tooltips Enabled
    'Sets whether or not to show map tooltips for the specified field.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
     ReadOnlyAttribute(False)> _
     Public Property MapTooltipsEnabled() As Boolean
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled
        End Get
        Set(ByVal value As Boolean)
            If MapTooltipField = "None" Then
                MsgBox("Please select a field to use for Map Tooltips before turning them on.", MsgBoxStyle.Information, "Select Field First")
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = False
            Else
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = value
            End If
            frmMain.UpdateMapToolTipsAtLeastOneLayer()
        End Set
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).Icon = Value
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Dynamic Visibility
    'Defines the zoom level at which this layer becomes visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(DynamicVisibilityTypeConverter)), _
    Editor(GetType(DynamicVisibilityEditor), GetType(UITypeEditor))> _
    Public Property DynamicVisibility() As Object
        Get
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                Return frmMain.m_AutoVis(m_Handle).UseDynamicExtents
            Else
                Return False
            End If
        End Get
        Set(ByVal Value As Object)
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                frmMain.m_AutoVis(m_Handle).UseDynamicExtents = CBool(Value)
                frmMain.Plugins.BroadcastMessage("DynamicVisibilityChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property
End Class
#End Region

#Region "Properties polygon shapefile objects"
' -------------------------------------------------------------------------------------------------------------
' ---------------------------------------
' * POLYGON SHAPEFILE layer properties *
' ---------------------------------------
'
'May/11/2008 Jiri Kadlec: attributes added for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class PolygonSFInfo
    Implements IDisposable 'Added by Lailin Chen Mar-24-2006
    ' coloring related properties
    Private m_FillColor As Color
    Private m_ShowFill As Boolean
    Private m_FillStyle As MapWinGIS.tkFillStipple
    Friend m_Legend As MapWinGIS.ShapefileColorScheme
    Friend m_FillStippleScheme As MapWindow.Interfaces.ShapefileFillStippleScheme
    Private m_LineStyle As MapWinGIS.tkLineStipple
    Private m_OutlineColor As Color

    ' read/write layer properties
    Private m_OutlineWidth As Integer
    Private m_Name As String
    Private m_Tag As String

    ' read-only layer properties
    Private m_Extents As MapWinGIS.Extents
    Private m_Filename As String
    Private m_Handle As Integer
    Private m_LayerType As MapWindow.Interfaces.eLayerType
    Private m_Shapes As MapWindow.Interfaces.Shapes

    ' Legend properties
    Private m_Expanded As Boolean
    Private m_LegendPicture As Icon

    'Label related properties --- added by Lailin Chen, Mar-13-2006
    Public alignment As MapWinGIS.tkHJustification
    Public m_font As Font
    Public m_color As System.Drawing.Color
    Public m_align As Integer
    Public m_field As Integer
    Public m_UseMinExtents As Boolean
    Public m_ShowupExtents As MapWinGIS.Extents

    Public m_points As System.Collections.ArrayList
    Public m_CalculatePos As Boolean
    Public m_Modified As Boolean
    Public m_LabelExtentsChanged As Boolean
    Public m_label As New LabelEx

    ' Internal to this class
    Private m_AutoApply As Boolean
    Private m_LabelfieldChanged As Boolean = False
    Private m_InitialField As Integer
    ' Internal to this class

    Private m_Map As AxMapWinGIS.AxMap

    'External accessable:
    Public m_Layer As MapWindow.Layer
    '---------------------------------------------------------------------------------

    Public Sub New(ByVal LayerHandle As Integer, ByVal map As AxMapWinGIS.AxMap)
        m_Handle = LayerHandle
        m_AutoApply = True
        m_Layer = frmMain.m_layers(LayerHandle)

        LabelEditor.handle = LayerHandle

        With frmMain.m_layers(LayerHandle)
            ' coloring related properties
            m_FillColor = .Color
            m_ShowFill = .DrawFill
            m_FillStyle = .FillStipple
            m_Legend = CType(.ColoringScheme, MapWinGIS.ShapefileColorScheme)
            If m_Legend Is Nothing Then
                m_Legend = New MapWinGIS.ShapefileColorScheme()
            End If
            m_LineStyle = .LineStipple
            m_OutlineColor = .OutlineColor

            ' read/write layer properties
            m_OutlineWidth = CInt(.LineOrPointSize)
            m_Name = .Name
            m_Tag = .Tag

            ' read-only layer properties
            m_Extents = .Extents
            m_Filename = .FileName
            m_Handle = .Handle
            m_LayerType = .LayerType
            m_Shapes = .Shapes
            m_LegendPicture = CType(.Icon, Icon)
            m_Expanded = .Expanded
        End With
        m_Layer = frmMain.m_layers(LayerHandle)
        m_InitialField = m_label.field
    End Sub

    ' coloring related properties
    'SYMBOLOGY -- Fill Color
    'Sets the color to fill the polygon with.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property FillColor() As Color
        Get
            Return m_FillColor
        End Get
        Set(ByVal Value As Color)
            m_FillColor = Value
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).Color = Value
                frmMain.Plugins.BroadcastMessage("FillColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Labels Visible
    'Indicates whether labels (if any have been set up or added) are visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
    Public Property LabelsVisible() As Boolean
        Get
            Return frmMain.Layers(m_Handle).LabelsVisible
        End Get
        Set(ByVal Value As Boolean)
            frmMain.SetModified(True)
            frmMain.Layers(m_Handle).LabelsVisible = Value
            frmMain.Plugins.BroadcastMessage("LabelsVisibleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
        End Set
    End Property

    'SYMBOLOGY -- Show Fill
    'Indicates whether the polygons in this shapefile should be drawn with a fill color.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ShowFill() As Boolean
        Get
            Return m_ShowFill
        End Get
        Set(ByVal Value As Boolean)
            m_ShowFill = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).DrawFill = Value
                frmMain.Plugins.BroadcastMessage("ShowFillChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Vertex Color
    'Color vertices are drawn with when Vertices Visible is True.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property VertexColor() As Color
        Get
            Return frmMain.MapMain.get_ShapeLayerPointColor(m_Handle)
        End Get
        Set(ByVal Value As Color)
            frmMain.MapMain.set_ShapeLayerPointColor(m_Handle, Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Value)))
        End Set
    End Property

    'SYMBOLOGY -- Vertex Size
    'Size (in pixels) that vertices are drawn with when Vertices Visible is True.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
ReadOnlyAttribute(False)> _
Public Property VertexSize() As Integer
        Get
            Return frmMain.MapMain.get_ShapeLayerPointSize(m_Handle)
        End Get
        Set(ByVal Value As Integer)
            frmMain.MapMain.set_ShapeLayerPointSize(m_Handle, CSng(Value))
        End Set
    End Property

    'SYMBOLOGY -- Vertices Visible
    'Display the vertices of the shapefile?
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property VerticesVisible() As Boolean
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_Handle).VerticesVisible
        End Get
        Set(ByVal Value As Boolean)
            frmMain.Legend.Layers.ItemByHandle(m_Handle).VerticesVisible = Value
            frmMain.MapMain.set_ShapeLayerDrawPoint(m_Handle, Value)
        End Set
    End Property

    'SYMBOLOGY -- Fill Style
    'The style of fill to use if the polygons are drawn filled.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    Editor(GetType(FillStyleEditor), GetType(UITypeEditor))> _
    Public Property FillStyle() As MapWinGIS.tkFillStipple
        Get
            Return m_FillStyle
        End Get
        Set(ByVal Value As MapWinGIS.tkFillStipple)
            m_FillStyle = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).FillStipple = Value
                frmMain.Plugins.BroadcastMessage("FillStyleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Coloring Scheme
    'Define how to color these shapes.
    <GlobalizedProperty(CategoryId:="SymbologyCategory", _
    DescriptionId:="ColoringSchemeDescription_Shape"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(ColoringSchemeTypeConverter)), _
    Editor(GetType(SFColoringSchemeEditor), GetType(UITypeEditor))> _
    Public Property ColoringScheme() As Object
        Get
            Return m_Legend
        End Get
        Set(ByVal Value As Object)
            m_Legend = CType(Value, MapWinGIS.ShapefileColorScheme)
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).ColoringScheme = m_Legend
            End If
        End Set
    End Property

    'SYMBOLOGY -- Hatching Scheme
    'Define how to fill these shapes with hatching.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(FillStippleSchemeTypeConverter)), _
    Editor(GetType(SFFillStippleSchemeEditor), GetType(UITypeEditor)), DisplayName("Fill Stipple Scheme")> _
    Public Property FillStippleScheme() As Object
        Get
            Return frmMain.m_layers(m_Handle).FillStippleScheme
        End Get
        Set(ByVal Value As Object)
            m_FillStippleScheme = CType(Value, MapWindow.Interfaces.ShapefileFillStippleScheme)
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.m_layers(m_Handle).FillStippleScheme = m_FillStippleScheme
            End If
        End Set
    End Property

    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute(True), DisplayName("Fill Stipple Transparent?")> _
    Public Property FillStippleTransparent() As Boolean
        Get
            Return frmMain.MapMain.get_ShapeLayerStippleTransparent(m_Handle)
        End Get
        Set(ByVal Value As Boolean)
            frmMain.MapMain.set_ShapeLayerStippleTransparent(m_Handle, Value)
        End Set
    End Property

    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute(True), DisplayName("Fill Stipple Line Color")> _
    Public Property FillStippleLineColor() As Color
        Get
            Return frmMain.MapMain.get_ShapeLayerStippleColor(m_Handle)
        End Get
        Set(ByVal Value As Color)
            frmMain.MapMain.set_ShapeLayerStippleColor(m_Handle, System.Drawing.ColorTranslator.ToOle(Value))
        End Set
    End Property

    'SYMBOLOGY -- Map Tooltip Field
    'The field to display for Map Tooltips.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute("None"), _
    TypeConverter(GetType(FieldList))> _
    Public Property MapTooltipField() As String
        Get
            Try
                Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
                Return sf.Field(frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex).Name
            Catch
                Return "None"
            End Try
        End Get
        Set(ByVal Value As String)
            Dim sf As MapWinGIS.Shapefile = frmMain.MapMain.get_GetObject(m_Handle)
            For i As Integer = 0 To sf.NumFields - 1
                If sf.Field(i).Name.ToLower().Trim() = Value.ToLower().Trim() Then
                    frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipFieldIndex = i
                End If
            Next
        End Set
    End Property

    'SYMBOLOGY -- Map Tooltips Enabled
    'Sets whether or not to show map tooltips for the specified field.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
     ReadOnlyAttribute(False)> _
     Public Property MapTooltipsEnabled() As Boolean
        Get
            Return frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled
        End Get
        Set(ByVal value As Boolean)
            If MapTooltipField = "None" Then
                MsgBox("Please select a field to use for Map Tooltips before turning them on.", MsgBoxStyle.Information, "Select Field First")
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = False
            Else
                frmMain.Legend.Layers.ItemByHandle(m_Handle).MapTooltipsEnabled = value
            End If
            frmMain.UpdateMapToolTipsAtLeastOneLayer()
        End Set
    End Property

    'Added by Lailin Chen May-19-2006 to use USU Labeler Plugin-in in Lengend Editor to edit labels
    'SYMBOLOGY -- Label Setup
    'Configure how labels are displayed for this layer.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
     ReadOnlyAttribute(False), _
     TypeConverter(GetType(LabelerTypeConverter)), _
     Editor(GetType(LabelEditor), GetType(UITypeEditor))> _
    Public Property LabelingSetup() As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)
        End Set
    End Property


    'SYMBOLOGY -- Line Style
    'The line style used to display the polygon outlines.
    <GlobalizedProperty(CategoryId:="SymbologyCategory", _
    DescriptionId:="LineStyleDescription_Polygon"), _
    Editor(GetType(LineStyleEditor), GetType(UITypeEditor))> _
    Public Property LineStyle() As MapWinGIS.tkLineStipple
        Get
            Return m_LineStyle
        End Get
        Set(ByVal Value As MapWinGIS.tkLineStipple)
            m_LineStyle = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).LineStipple = Value
                frmMain.Plugins.BroadcastMessage("LineStyleChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Outline Color
    'The color to use when drawing an outline of the polygons in this shapefile.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property OutlineColor() As Color
        Get
            Return m_OutlineColor
        End Get
        Set(ByVal Value As Color)
            m_OutlineColor = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).OutlineColor = Value
                frmMain.Plugins.BroadcastMessage("OutlineColorChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Line Width
    'The width of the polygon outline in pixels.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    DefaultValueAttribute(1), _
    Editor(GetType(LineWidthEditor), GetType(UITypeEditor))> _
    Public Property OutlineWidth() As Integer
        Get
            Return m_OutlineWidth
        End Get
        Set(ByVal Value As Integer)
            m_OutlineWidth = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).LineOrPointSize = Value
                frmMain.Plugins.BroadcastMessage("OutlineWidthChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).Name = Value
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property


    'LAYER PROPERTIES -- File Name
    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min X, Max X)
    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Y, Max Y)
    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Bounding Box (Min Z, Max Z)
    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'LAYER PROPERTIES -- Comments
    'Free-form comments about this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Tag() As String
        Get
            Return frmMain.Layers(m_Handle).Tag
        End Get
        Set(ByVal value As String)
            frmMain.Layers(m_Handle).Tag = value
        End Set
    End Property

    'LAYER PROPERTIES -- Handle
    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
    ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    ' Legend properties
    'SYMBOLOGY -- Transparency Percent
    'Indicates that this layer should be partially (or wholly) transparent.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Transparency() As Integer
        Get
            Return -1 * ((frmMain.Layers(m_Handle).ShapeLayerFillTransparency * 100) - 100)
        End Get
        Set(ByVal Value As Integer)
            frmMain.Layers(m_Handle).ShapeLayerFillTransparency = (100 - Value) / 100
            frmMain.Legend.Refresh()
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Plugins.BroadcastMessage("LegendExpanded Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'LAYER PROPERTIES -- Projection
    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), _
ReadOnlyAttribute(True)> _
Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

    'LEGEND PROPERTIES -- Expanded Legend
    'Indicates whether any additional data (i.e., coloring scheme) should be shown expanded in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Expanded() As Boolean
        Get
            Return m_Expanded
        End Get
        Set(ByVal Value As Boolean)
            m_Expanded = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).Expanded = Value
                frmMain.Plugins.BroadcastMessage("LegendExpanded Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Layers(m_Handle).Icon = Value
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'SYMBOLOGY -- Dynamic Visibility
    'Defines the zoom level at which this layer becomes visible.
    <GlobalizedProperty(CategoryId:="SymbologyCategory"), _
    ReadOnlyAttribute(False), _
    TypeConverter(GetType(DynamicVisibilityTypeConverter)), _
    Editor(GetType(DynamicVisibilityEditor), GetType(UITypeEditor))> _
    Public Property DynamicVisibility() As Object
        Get
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                Return frmMain.m_AutoVis(m_Handle).UseDynamicExtents
            Else
                Return False
            End If
        End Get
        Set(ByVal Value As Object)
            If Not frmMain.m_AutoVis(m_Handle) Is Nothing Then
                frmMain.SetModified(True)
                frmMain.m_AutoVis(m_Handle).UseDynamicExtents = CBool(Value)
                frmMain.Plugins.BroadcastMessage("DynamicVisibilityChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Private Sub ApplyStyleChanges(ByVal type As String)
        Try
            Dim shpFile As MapWinGIS.Shapefile = Nothing

            shpFile = CType(m_Layer.GetObject(), MapWinGIS.Shapefile)
            shpFile.BeginPointInShapefile()
            If (type = "T_FONT") Then
                m_Layer.Font(m_label.font.Name, m_label.font.Size)
            End If

            If (type = "T_COLOR") Then
                m_Layer.LabelColor(m_label.color)
            End If


        Catch ex As System.Exception
            MessageBox.Show("ApplyChanges()" + ex.Message)
        End Try
    End Sub

    Private Sub FindXYValues(ByVal shpFile As MapWinGIS.Shapefile, ByVal shapeIndex As Integer, ByRef x As Double, ByRef y As Double)

        Dim dist As Double = 0
        Dim area As Double = 0
        Dim cX As Double = 0
        Dim cY As Double = 0
        Dim p1, p2 As MapWinGIS.Point

        Dim Shape As MapWinGIS.Shape = shpFile.Shape(shapeIndex)
        Dim count As Integer = Shape.numPoints

        If (Shape.ShapeType = MapWinGIS.ShpfileType.SHP_POLYGON) Then
            If count <= 4 Then

                dist = Math.Sqrt(Math.Pow(Shape.Extents.xMin - Shape.Extents.xMax, 2) + Math.Pow((Shape.Extents.yMin - Shape.Extents.yMin), 2)) / 2
                cX = Shape.Extents.xMax - dist

                dist = Math.Sqrt(Math.Pow(Shape.Extents.xMin - Shape.Extents.xMin, 2) + Math.Pow((Shape.Extents.yMin - Shape.Extents.yMax), 2)) / 2
                cY = Shape.Extents.yMax - dist

                x = cX
                y = cY
                Return
            End If
        Else

            Dim i As Integer = 0
            For i = 0 To count - 1 Step 1
                p1 = Shape.Point(i)

                If (i = count - 1) Then
                    p2 = Shape.Point(0)
                Else
                    p2 = Shape.Point(i + 1)
                End If
                area += (p1.x * p2.y - p2.x * p1.y)
            Next
            area *= 0.5

            'calculate the centroid
            Dim j As Integer = 0

            For j = 0 To count - 1

                p1 = Shape.Point(j)

                If (j = count - 1) Then
                    p2 = Shape.Point(0)
                Else
                    p2 = Shape.Point(j + 1)
                End If

                cX += (p1.x + p2.x) * (p1.x * p2.y - p2.x * p1.y)
                cY += (p1.y + p2.y) * (p1.x * p2.y - p2.x * p1.y)
            Next
            cX *= 1 / (6 * area)
            cY *= 1 / (6 * area)

        End If
    End Sub
    Public Sub OpenLabelingInfo()
        'Dim label As LabelEx
        'mwLabeler.Classes.XMLLabelFile xmlLabel = new mwLabeler.Classes.XMLLabelFile(this.m_parent.m_MapWin,this.m_MapWinVersion);

        '      'clear all previous labels
        '	m_Layers.Clear();

        '      'load all labeling info
        '	int numlayers = this.m_parent.m_MapWin.Layers.NumLayers;
        '	for(int i=0; i < numlayers;i++) loop	
        '		label = new Label();
        '		handle = this.m_parent.m_MapWin.Layers.GetHandle(i);

        '              if(xmlLabel.LoadLabelInfo(m_parent.m_MapWin.Layers[handle],ref label,this))
        '			m_Layers.Add(handle,label);
        '	next
    End Sub
End Class
#End Region

#Region "ShapefileInfo"
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class ShapefileInfo
    Inherits LayerInfo

    Private m_shapefile As MapWinGIS.Shapefile = Nothing
    Private m_LegendPicture As Icon = Nothing

    ''' <summary>
    ''' Constructor. Creates new instance of ShapefileInfo class
    ''' </summary>
    ''' <param name="LayerHandle"></param>
    ''' <remarks></remarks>
    Sub New(ByVal LayerHandle As Integer)
        MyBase.New(LayerHandle)
        m_shapefile = CType(frmMain.MapMain.get_GetObject(LayerHandle), MapWinGIS.Shapefile)
        m_Handle = LayerHandle
    End Sub

    'The physical file behind this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property Filename() As String
        Get
            If Not m_shapefile Is Nothing Then
                Return m_shapefile.Filename
            Else
                Return ""
            End If
        End Get
    End Property

    'The internal indexing handle of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property Handle() As Integer
        Get
            Return m_Handle
        End Get
    End Property

    'The projection of this layer.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property Projection() As String
        Get
            Return frmMain.GetLayerPrettyProjection(m_Handle)
        End Get
    End Property

    'The X dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property BoundingBoxX() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.xMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.xMax.ToString()
        End Get
    End Property

    'The Y dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property BoundingBoxY() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.yMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.yMax.ToString()
        End Get
    End Property

    'The Z dimensions of the bounding box.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(True)> _
    Public ReadOnly Property BoundingBoxZ() As String
        Get
            Return frmMain.Layers(m_Handle).Extents.zMin.ToString() + ", " + frmMain.Layers(m_Handle).Extents.zMax.ToString()
        End Get
    End Property

    'Indicates whether any additional data (i.e., coloring scheme) should be shown expanded in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), ReadOnlyAttribute(False)> _
    Public Property Expanded() As Boolean
        Get
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(m_Handle)
            If Not layer Is Nothing Then
                Return layer.Expanded
            End If
        End Get
        Set(ByVal Value As Boolean)
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(m_Handle)
            If Not layer Is Nothing Then
                frmMain.Plugins.BroadcastMessage("LegendExpanded Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
                layer.Expanded = Value
            End If
        End Set
    End Property

    'LEGEND PROPERTIES -- Legend Picture
    'Sets the picture which will appear in the legend.
    <GlobalizedProperty(CategoryId:="LegendPropertiesCategory"), ReadOnlyAttribute(False)> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            frmMain.m_layers(m_Handle).Icon = Value
            frmMain.Legend.Layers.ItemByHandle(m_Handle).Refresh()
            frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            frmMain.SetModified(True)
        End Set
    End Property

    'SYMBOLOGY -- Display Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="LayerPropertiesCategory"), ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(m_Handle)
            If Not layer Is Nothing Then
                Return frmMain.Layers(m_Handle).Name
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            Dim layer As MapWindow.Interfaces.Layer = frmMain.Layers(m_Handle)
            If Not layer Is Nothing Then
                frmMain.Layers(m_Handle).Name = Value
                frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                frmMain.SetModified(True)
            End If
        End Set
    End Property

End Class
#End Region

#Region "Group properties"
' -------------------------------------------------------------------------------------------------------------
' --------------------------
' * GROUP layer properties *
' --------------------------
'
'May/11/2008 Jiri Kadlec: attributes added for internationalization
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter)), _
GlobalizedType(BaseName:="MapWindow.LegendEditorResource")> _
Friend Class GroupInfo
    Private m_LegendPicture As Icon
    Private m_Handle As Integer
    Private m_AutoApply As Boolean = True
    Private m_Name As String

    Public Sub New(ByVal GroupHandle As Integer)
        m_Handle = GroupHandle
        Dim img As Object = frmMain.Legend.Groups.ItemByHandle(m_Handle).Icon
        If TypeOf img Is Icon Then
            m_LegendPicture = CType(frmMain.Legend.Groups.ItemByHandle(m_Handle).Icon, Icon)
        ElseIf TypeOf img Is Image OrElse TypeOf img Is Bitmap Then
            Dim myImage As Bitmap = CType(img, Bitmap)
            m_LegendPicture = System.Drawing.Icon.FromHandle(myImage.GetHbitmap())
        End If

        m_Name = frmMain.Legend.Groups.ItemByHandle(m_Handle).Text
    End Sub

    'GROUP PROPERTIES -- Legend Picture
    'The picture or icon that appears in the legend next to the group heading.
    <GlobalizedProperty(CategoryId:="GroupPropertiesCategory", _
    DescriptionId:="LegendPictureDescription_Group")> _
    Public Property LegendPicture() As Icon
        Get
            Return m_LegendPicture
        End Get
        Set(ByVal Value As Icon)
            m_LegendPicture = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Legend.Groups.ItemByHandle(m_Handle).Icon = Value
                frmMain.Plugins.BroadcastMessage("LegendPictureChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            End If
        End Set
    End Property

    'GROUP PROPERTIES -- Name
    'The name displayed in the legend.
    <GlobalizedProperty(CategoryId:="GroupPropertiesCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
            frmMain.SetModified(True)
            If m_AutoApply = True Then
                frmMain.Legend.Groups.ItemByHandle(m_Handle).Text = Value
                If (frmMain.Layers.NumLayers > 0) Then
                    frmMain.Plugins.BroadcastMessage("LayerNameChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
                End If
            End If
        End Set
    End Property
End Class

#End Region

