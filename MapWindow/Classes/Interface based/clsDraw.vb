'********************************************************************************************************
'File Name: clsDraw.vb
'Description: Public class used to access drawing functions through the plugin interface.
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
'7/3/2007 - Dan Ames - Modified DrawPolygon to work.  Pass in the full array of points, not just the first element.
'********************************************************************************************************

Public Class Draw
    Implements Interfaces.Draw

    '--------------------------------------Draw Public Interface--------------------------------------
    '27 Aug 2001  Darrel Brown.  Refer to Document "MapWindow 2.0 Public Interface" Page 3
    '------------------------------------------------------------------------------------------------------------

    '-------------------Subs-------------------
    Public Sub ClearDrawing(ByVal DrawHandle As Integer) Implements Interfaces.Draw.ClearDrawing
        frmMain.MapMain.ClearDrawing(DrawHandle)
    End Sub

    Public Sub ClearDrawings() Implements Interfaces.Draw.ClearDrawings
        frmMain.MapMain.ClearDrawings()
    End Sub

    Public Sub DrawCircle(ByVal x As Double, ByVal y As Double, ByVal PixelRadius As Double, ByVal Color As System.Drawing.Color, ByVal FillCircle As Boolean) Implements Interfaces.Draw.DrawCircle
        frmMain.MapMain.DrawCircle(x, y, PixelRadius, MapWinUtility.Colors.ColorToUInteger(Color), FillCircle)
    End Sub

    Public Sub DrawLine(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double, ByVal PixelWidth As Integer, ByVal Color As System.Drawing.Color) Implements Interfaces.Draw.DrawLine
        frmMain.MapMain.DrawLine(X1, Y1, X2, Y2, PixelWidth, MapWinUtility.Colors.ColorToUInteger(Color))
    End Sub

    Public Sub DrawPoint(ByVal x As Double, ByVal y As Double, ByVal PixelSize As Integer, ByVal Color As System.Drawing.Color) Implements Interfaces.Draw.DrawPoint
        frmMain.MapMain.DrawPoint(x, y, PixelSize, MapWinUtility.Colors.ColorToUInteger(Color))
    End Sub

    Public Sub DrawPolygon(ByVal x() As Double, ByVal y() As Double, ByVal Color As System.Drawing.Color, ByVal FillPolygon As Boolean) Implements Interfaces.Draw.DrawPolygon
        'frmMain.MapMain.DrawPolygon(CType(x(0), Object), CType(y(0), Object), y.Length, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon)
        'Dan Ames July 2007 - pass in the full array, not just the first element!
        frmMain.MapMain.DrawPolygon(CType(x, Object), CType(y, Object), y.Length, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon)
    End Sub
    '-------------------Functions-------------------
    <CLSCompliant(False)> _
    Public Function NewDrawing(ByVal Projection As MapWinGIS.tkDrawReferenceList) As Integer Implements Interfaces.Draw.NewDrawing
        NewDrawing = frmMain.MapMain.NewDrawing(Projection)
    End Function
    '-------------------Properties-------------------
    Public Property DoubleBuffer() As Boolean Implements Interfaces.Draw.DoubleBuffer
        Get
            DoubleBuffer = frmMain.MapMain.DoubleBuffer
        End Get
        Set(ByVal Value As Boolean)
            frmMain.MapMain.DoubleBuffer = Value
        End Set
    End Property
    '-------------------Start Paul Meems 12 May 2010-------------------

    'Fixing bug 1566 (Label handling in Interface.Draw is missing)
    Public Sub AddDrawingLabel(ByVal DrawHandle As Integer, ByVal Text As String, ByVal Color As System.Drawing.Color, ByVal x As Double, ByVal y As Double, ByVal hJustification As MapWinGIS.tkHJustification) _
      Implements Interfaces.Draw.AddDrawingLabel
        frmMain.MapMain.AddDrawingLabel(DrawHandle, Text, MapWinUtility.Colors.ColorToUInteger(Color), x, y, hJustification)
    End Sub
    Public Sub AddDrawingLabelEx(ByVal DrawHandle As Integer, ByVal Text As String, ByVal Color As System.Drawing.Color, ByVal x As Double, ByVal y As Double, ByVal hJustification As MapWinGIS.tkHJustification, ByVal Rotation As Double) _
      Implements Interfaces.Draw.AddDrawingLabelEx
        frmMain.MapMain.AddDrawingLabelEx(DrawHandle, Text, MapWinUtility.Colors.ColorToUInteger(Color), x, y, hJustification, Rotation)
    End Sub
    Public Sub ClearDrawingLabels(ByVal DrawHandle As Integer) _
      Implements Interfaces.Draw.ClearDrawingLabels
        frmMain.MapMain.ClearDrawingLabels(DrawHandle)
    End Sub
    'Fixing  1567 The *Ex methods are missing in Interface.Draw) 
    Public Sub DrawCircleEx(ByVal DrawHandle As Integer, ByVal x As Double, ByVal y As Double, ByVal pixelRadius As Double, ByVal Color As System.Drawing.Color, ByVal fill As Boolean) _
      Implements Interfaces.Draw.DrawCircleEx
        frmMain.MapMain.DrawCircleEx(DrawHandle, x, y, pixelRadius, MapWinUtility.Colors.ColorToUInteger(Color), fill)
    End Sub
    Public Sub DrawLineEx(ByVal DrawHandle As Integer, ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal pixelWidth As Integer, ByVal Color As System.Drawing.Color) _
      Implements Interfaces.Draw.DrawLineEx
        frmMain.MapMain.DrawLineEx(DrawHandle, x1, y1, x2, y2, pixelWidth, MapWinUtility.Colors.ColorToUInteger(Color))
    End Sub
    Public Sub DrawPointEx(ByVal DrawHandle As Integer, ByVal x As Double, ByVal y As Double, ByVal pixelSize As Integer, ByVal Color As System.Drawing.Color) _
      Implements Interfaces.Draw.DrawPointEx
        frmMain.MapMain.DrawPointEx(DrawHandle, x, y, pixelSize, MapWinUtility.Colors.ColorToUInteger(Color))
    End Sub
    Public Sub DrawPolygonEx(ByVal DrawHandle As Integer, ByVal x() As Double, ByVal y() As Double, ByVal Color As System.Drawing.Color, ByVal FillPolygon As Boolean) _
      Implements Interfaces.Draw.DrawPolygonEx
        frmMain.MapMain.DrawPolygonEx(DrawHandle, CType(x, Object), CType(y, Object), y.Length, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon)
    End Sub
    Public Sub DrawWideCircle(ByVal x As Double, ByVal y As Double, ByVal pixelRadius As Double, ByVal Color As System.Drawing.Color, ByVal fill As Boolean, ByVal Width As Int16) _
      Implements Interfaces.Draw.DrawWideCircle
        frmMain.MapMain.DrawWideCircle(x, y, pixelRadius, MapWinUtility.Colors.ColorToUInteger(Color), fill, Width)
    End Sub
    Public Sub DrawWidePolygon(ByVal x() As Double, ByVal y() As Double, ByVal Color As System.Drawing.Color, ByVal FillPolygon As Boolean, ByVal Width As Short) _
      Implements Interfaces.Draw.DrawWidePolygon
        frmMain.MapMain.DrawWidePolygon(CType(x, Object), CType(y, Object), y.Length, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon, Width)
    End Sub
    Public Sub DrawingFont(ByVal DrawHandle As Integer, ByVal FontName As String, ByVal FontSize As Integer) _
      Implements Interfaces.Draw.DrawingFont
        frmMain.MapMain.DrawingFont(DrawHandle, FontName, FontSize)
    End Sub
    ' TODO Don't we have a GetDrawingLayerVisible?
    Public Sub SetDrawingLayerVisible(ByVal DrawHandle As Integer, ByVal Visible As Boolean) _
      Implements Interfaces.Draw.SetDrawingLayerVisible
        frmMain.MapMain.SetDrawingLayerVisible(DrawHandle, Visible)
    End Sub
    '-------------------End Paul Meems 12 May 2010-------------------

    '-------------------Start Paul Meems March 11 2011-------------------
    Public Sub SetDrawingLabelsVisible(ByVal DrawHandle As Integer, ByVal Visible As Boolean) _
    Implements Interfaces.Draw.SetDrawingLabelsVisible
        frmMain.MapMain.set_DrawingLabelsVisible(DrawHandle, Visible)
    End Sub
    '-------------------End Paul Meems March 11 2011-------------------

    '-------------------Start Paul Meems May 26 2010-------------------
    Public Sub DrawWidePolygonEx(ByVal DrawHandle As Integer, ByVal x() As Double, ByVal y() As Double, ByVal Color As System.Drawing.Color, ByVal FillPolygon As Boolean, ByVal PixelWidth As Short) _
      Implements Interfaces.Draw.DrawWidePolygonEx
        frmMain.MapMain.DrawWidePolygonEx(DrawHandle, CType(x, Object), CType(y, Object), y.Length, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon, PixelWidth)
    End Sub
    Public Sub DrawWideCircleEx(ByVal DrawHandle As Integer, ByVal x As Double, ByVal y As Double, ByVal Radius As Double, ByVal Color As System.Drawing.Color, ByVal FillPolygon As Boolean, ByVal PixelWidth As Short) _
      Implements Interfaces.Draw.DrawWideCircleEx
        frmMain.MapMain.DrawWideCircleEx(DrawHandle, x, y, Radius, MapWinUtility.Colors.ColorToUInteger(Color), FillPolygon, PixelWidth)
    End Sub

    '-------------------End Paul Meems May 26 2010-------------------

    'Public Property DrawingKey(ByVal DrawingHandle As Integer) As Integer Implements Interfaces.Draw.DrawingKey
    '    Get
    '        DrawingKey = frmMain.MapMain.get_DrawingKey(DrawingHandle)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        frmMain.MapMain.set_DrawingKey(DrawingHandle, Value)
    '    End Set
    'End Property

End Class


