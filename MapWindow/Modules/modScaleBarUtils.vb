'********************************************************************************************************
'File Name: modScaleBarUtils.vb
'Description: utility functions for the MapWindow scale bar
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
'8/28/2008 - Jiri Kadlec (jk) - use a new conversion method (MapWinGeoProc.UnitConverter.ConvertLength)
'                               for converting between different distance units
'********************************************************************************************************

Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports MapWindow.Interfaces
Imports System.Drawing
Imports Math = System.Math

'8/28/2008 jk - No longer needed, replaced by MapWinGeoProc.UnitConverter
'Friend Module ConversionFactors
'    Public KilometersToMillimeters As Double = 1000000
'    Public KilometersToCentimeters As Double = 100000
'    Public KilometersToInches As Double = 39370.07874
'    Public KilometersToFeet As Double = 3280.839895
'    Public KilometersToYards As Double = 1093.613298
'    Public KilometersToMeters As Double = 1000
'    Public KilometersToMiles As Double = 0.621371

'    Public MilesToMillimeters As Double = 1609344
'    Public MilesToCentimeters As Double = 160934.4
'    Public MilesToInches As Double = 63360
'    Public MilesToFeet As Double = 5280
'    Public MilesToYards As Double = 1760
'    Public MilesToMeters As Double = 1609.344
'    Public MilesToKilometers As Double = 1.609344

'    Public MeterToMillimeters As Double = 1000
'    Public MeterToCentimeters As Double = 100
'    Public MeterToInches As Double = 39.370079
'    Public MeterToFeet As Double = 3.28084
'    Public MeterToYards As Double = 1.093613
'    Public MeterToMiles As Double = 0.000621
'    Public MeterToKilometers As Double = 0.001

'    Public YardsToMillimeters As Double = 914.4
'    Public YardsToCentimeters As Double = 91.44
'    Public YardsToInches As Double = 36
'    Public YardsToFeet As Double = 3
'    Public YardsToMeters As Double = 0.9144
'    Public YardsToMiles As Double = 0.000568
'    Public YardsToKilometers As Double = 0.000914

'    Public FeetToMillimeters As Double = 304.8
'    Public FeetToCentemeters As Double = 30.48
'    Public FeetToInches As Double = 12
'    Public FeetToYards As Double = 0.333333
'    Public FeetToMeters As Double = 0.3048
'    Public FeetToMiles As Double = 0.000189
'    Public FeetToKilometers As Double = 0.000305

'    Public InchesToMillimeters As Double = 25.4
'    Public InchesToCentimeters As Double = 2.54
'    Public InchesToFeet As Double = 0.083333
'    Public InchesToYards As Double = 0.027778
'    Public InchesToMeters As Double = 0.0254
'    Public InchesToMiles As Double = 0.000016
'    Public InchesToKilometers As Double = 0.000025

'    Public CentimetersToMillimeters As Double = 10
'    Public CentimetersToInches As Double = 0.393701
'    Public CentimetersToFeet As Double = 0.032808
'    Public CentimetersToYards As Double = 0.010936
'    Public CentimetersToMeters As Double = 0.01
'    Public CentimetersToMiles As Double = 0.000006
'    Public CentimetersToKilometers As Double = 0.00001

'    Public MillimeterToCentimeters As Double = 0.1
'    Public MillimeterToInches As Double = 0.03937
'    Public MillimeterToFeet As Double = 0.003281
'    Public MillimeterToYards As Double = 0.001094
'    Public MillimeterToMeters As Double = 0.001
'    Public MillimeterToMiles As Double = 0.000001
'    Public MillimeterToKilometers As Double = 0.000001
'End Module

Friend Class ScaleBarUtility
    Public Function ConvertUnits(ByVal val As Double, ByVal srcUnits As UnitOfMeasure, ByVal destUnits As UnitOfMeasure) As Double
        If srcUnits = destUnits Then
            Return val
        End If
        Dim DecDegException As New System.Exception("Conversion To/From Decimal Degrees requires the use of 'FromDecimalDegrees' function or 'ToDecimalDegrees' function")
        If srcUnits = UnitOfMeasure.DecimalDegrees Or destUnits = UnitOfMeasure.DecimalDegrees Then
            Throw DecDegException
        End If
        '8/28/2008 jk - use the new unit conversion function in MapWinGeoProc
        Return MapWinGeoProc.UnitConverter.ConvertLength(srcUnits, destUnits, val)
    End Function

    Public Function GenerateScaleBarSolid(ByVal MapExtents As MapWinGIS.Extents, ByVal MapUnits As UnitOfMeasure, ByVal ScaleBarUnits As UnitOfMeasure, ByVal MaxWidth As Integer, ByVal Height As Integer, ByVal BackColor As System.Drawing.Color, ByVal ForeColor As System.Drawing.Color, ByVal FontFamily As String) As System.Drawing.Image
        Dim width As Integer = 200
        Try
            Dim Range As Double
            Dim img As System.Drawing.Bitmap
            Dim DrawTool As System.Drawing.Graphics
            Dim MidX As Integer
            Const BorderPad As Integer = 3
            Dim DistancePerPixel As Double

            Dim Pen As New System.Drawing.Pen(ForeColor)
            Dim Brush As System.Drawing.Brush = New System.Drawing.SolidBrush(ForeColor)

            Range = CalcRange(MapExtents, MapUnits, ScaleBarUnits, width - 2 * BorderPad)

            DistancePerPixel = Range / (width - 2 * BorderPad)

            If DistancePerPixel > 1000000000000000 Then
                Return Nothing
            End If

            If Range > 0 Then
                Range = FindNaturalBreak(Range)
                width = CInt(Range / DistancePerPixel + 2 * BorderPad)

                While width > MaxWidth
                    Range = Range / 2
                    If Range > 1 Then
                        Range = FindNaturalBreak(Range)
                    End If
                    width = CInt(Range / DistancePerPixel + 2 * BorderPad)
                End While
            End If


            MidX = width \ 2
            Dim Values() As Double = ComputeValues(Range)

            img = New Bitmap(width, Height)
            Dim g As Graphics = Graphics.FromImage(img)
            Dim TextFont As New Font(FontFamily, 6)
            Dim TestString As String = "3 kilometers" 'For font estimate
            Dim currSize As Integer = 6
            Dim estSize As SizeF = g.MeasureString(TestString, TextFont)
            While (estSize.Height < (Height * 0.95)) 'Slight tolerance
                If (currSize + 1 > 64) Then Exit While '64 point fixed max size
                currSize += 1
                TextFont = New Font(FontFamily, currSize)
                estSize = g.MeasureString(TestString, TextFont)
            End While
            Dim leftPad As Integer = g.MeasureString("0", TextFont).Width
            Dim rightPad As Integer = g.MeasureString(" " + Values(Values.Length - 1).ToString() + " " + ScaleBarUnits.ToString(), TextFont).Width
            g.Dispose()
            img.Dispose()
            img = New Bitmap(width + leftPad + rightPad + 1, Height)
            g = Graphics.FromImage(img)

            'create the draw tool and clear the surface to white
            DrawTool = Graphics.FromImage(img)
            DrawTool.Clear(BackColor)

            Dim pnts(3) As PointF
            pnts(0) = New PointF(leftPad + BorderPad, 0)
            pnts(1) = New PointF(leftPad + width - BorderPad, 0)
            pnts(2) = New PointF(leftPad + width - BorderPad, Height)
            pnts(3) = New PointF(leftPad + BorderPad, Height)
            DrawTool.FillPolygon(New System.Drawing.SolidBrush(ForeColor), pnts)

            g.DrawString("0 ", TextFont, New System.Drawing.SolidBrush(ForeColor), 0, 0)
            g.DrawString(" " + Values(Values.Length - 1).ToString() + " " + ScaleBarUnits.ToString(), TextFont, New System.Drawing.SolidBrush(ForeColor), leftPad + width - BorderPad + 1, 0)

            DrawTool.Flush(Drawing.Drawing2D.FlushIntention.Sync)

            Return img
        Catch ex As System.Exception
            ShowError(ex)
        End Try

        Return Nothing
    End Function

    Public Function GenerateScaleBar(ByVal MapExtents As MapWinGIS.Extents, ByVal MapUnits As UnitOfMeasure, ByVal ScaleBarUnits As UnitOfMeasure, ByVal MaxWidth As Integer, ByVal BackColor As System.Drawing.Color, ByVal ForeColor As System.Drawing.Color) As System.Drawing.Image
        Dim width As Integer = 200
        Const height As Integer = 60
        Try
            Dim Range As Double
            Dim img As System.Drawing.Bitmap
            Dim MidX As Integer
            Const BorderPad As Integer = 3
            Const FontSize As Integer = 8
            Dim CaptionHeight As Integer
            Dim CaptionWidth As Integer
            Dim DistancePerPixel As Double
            Dim caption As String

            Dim brush As System.Drawing.Brush = New System.Drawing.SolidBrush(ForeColor)
            Dim pen As New System.Drawing.Pen(ForeColor)
            Dim DrawTool As System.Drawing.Graphics
            Dim textFont As New System.Drawing.Font("Arial", FontSize, FontStyle.Regular)

            Range = CalcRange(MapExtents, MapUnits, ScaleBarUnits, width - 2 * BorderPad)

            DistancePerPixel = Range / (width - 2 * BorderPad)

            If DistancePerPixel > 1000000000000000 Then
                Return Nothing
            End If

            If Range > 0 Then
                Range = FindNaturalBreak(Range)
                width = CInt(Range / DistancePerPixel + 2 * BorderPad)

                While width > MaxWidth
                    Range = Range / 2
                    If Range > 1 Then
                        Range = FindNaturalBreak(Range)
                    End If
                    width = CInt(Range / DistancePerPixel + 2 * BorderPad)
                End While
            End If

            MidX = width \ 2

            Dim extrawidth As Integer
            extrawidth = System.Convert.ToInt32(width * 1.15)
            img = New Bitmap(extrawidth, height)

            'create the draw tool and clear the surface to white
            DrawTool = Graphics.FromImage(img)
            DrawTool.Clear(BackColor)

            ' TODO: Make semi-transparent

            'Write out the text label
            ' Paul Meems, changed:
            ' caption = "Scale in " & ScaleBarUnits.ToString()
            caption = ScaleBarUnits.ToString()
            CaptionHeight = TextHeight(caption, textFont)
            CaptionWidth = TextWidth(caption, textFont)
            DrawTool.DrawString(caption, textFont, brush, CSng(MidX - 0.5 * CaptionWidth), CSng(height - CaptionHeight) + 5)

            'draw the horizontal line above the caption text
            DrawTool.DrawLine(pen, BorderPad, height - CaptionHeight - 2 + 5, width - BorderPad, height - CaptionHeight - 2 + 5)

            'draw each of the vertical lines for the breaks
            Dim linelength As Integer = width - 2 * BorderPad
            Dim segmentlength As Single = CSng(linelength / 4)

            Dim x() As Integer
            ReDim x(4)
            x(0) = BorderPad
            x(1) = CInt(BorderPad + segmentlength)
            x(2) = CInt(BorderPad + 2 * segmentlength)
            x(3) = CInt(BorderPad + 3 * segmentlength)
            x(4) = width - BorderPad

            Dim StartY As Integer = height - CaptionHeight - 2 + 5
            Dim EndY As Integer = height - CaptionHeight - 14 + 5
            Dim MidY As Integer = (EndY + StartY) \ 2

            DrawTool.DrawLine(pen, x(0), StartY, x(0), EndY)
            DrawTool.DrawLine(pen, x(1), StartY, x(1), MidY)
            DrawTool.DrawLine(pen, x(2), StartY, x(2), EndY)
            DrawTool.DrawLine(pen, x(3), StartY, x(3), MidY)
            DrawTool.DrawLine(pen, x(4), StartY, x(4), EndY)

            Dim StartX As Integer
            EndY = EndY - TextHeight(caption, textFont) - 1
            MidY = MidY - TextHeight(caption, textFont) - 1
            Dim Values() As Double
            Values = ComputeValues(Range)


            'draw the text above the first vertical line
            caption = "0"
            StartX = CInt(x(0)) - 3 '0.13 * TextWidth(caption, TextFont)
            DrawTool.DrawString(caption, textFont, brush, StartX, EndY)

            'draw the text above the Second vertical line
            caption = Values(1).ToString()
            StartX = CInt(x(1) - 0.5 * TextWidth(caption, textFont))
            DrawTool.DrawString(caption, textFont, brush, StartX, MidY)

            'draw the text above the Third vertical line
            caption = Values(2).ToString()
            StartX = CInt(x(2) - 0.5 * TextWidth(caption, textFont))
            DrawTool.DrawString(caption, textFont, brush, StartX, EndY)

            'draw the text above the Fourth vertical line
            caption = Values(3).ToString()
            StartX = CInt(x(3) - 0.5 * TextWidth(caption, textFont))
            DrawTool.DrawString(caption, textFont, brush, StartX, MidY)

            'draw the text above the last vertical line
            caption = Values(4).ToString()
            StartX = System.Convert.ToInt32(x(4) - 0.5 * TextWidth(caption, textFont))
            'StartX = (x(4) - TextWidth(caption, TextFont) + 5)
            DrawTool.DrawString(caption, textFont, brush, StartX, EndY)

            DrawTool.Flush(Drawing.Drawing2D.FlushIntention.Sync)

            'Paul Meems, Added:
            textFont.Dispose()
            pen.Dispose()
            brush.Dispose()
            DrawTool.Dispose()

            Return img
        Catch ex As System.Exception
            ShowError(ex)
        End Try

        Return Nothing
    End Function

    Private Function ComputeValues(ByVal Range As Double) As Double()
        Dim values(4) As Double
        Dim NumDigits As Integer = FindSignificantDigits(Range) + 1
        If Range < 5 Then
            values(0) = 0
            values(4) = Math.Round(Range, NumDigits)
            values(2) = Math.Round(values(4) / 2, NumDigits + 1)
            values(1) = Math.Round(values(2) / 2, NumDigits + 2)
            values(3) = Math.Round(values(1) + values(2), NumDigits + 2)
        Else
            values(0) = 0
            values(4) = Math.Round(Range, 0)
            values(2) = Math.Round(values(4) / 2, 1)
            values(1) = Math.Round(values(2) / 2, 2)
            values(3) = Math.Round(values(1) + values(2), 2)
        End If

        Return values
    End Function

    Private Function FindNaturalBreak(ByVal range As Double) As Double
        Dim StepSize As Double

        If range < 1 Then
            Return 1
        ElseIf range < 10 Then
            StepSize = 2
        ElseIf range < 100 Then
            StepSize = 10
        ElseIf range < 1000 Then
            StepSize = 100
        ElseIf range < 20000 Then
            StepSize = 1000
        Else
            StepSize = 10000
        End If

        Dim result As Double = 0
        Try
            While result < range
                result += StepSize
            End While
        Catch ex As System.Exception
            result = Double.MaxValue
        End Try


        Return result
    End Function

    Private Function FindSignificantDigits(ByVal value As Double) As Integer
        'find the number of digits after the decimal place before a non-zero value is found
        Dim str As String = value.ToString()
        str = str.Replace(",", ".")

        Dim i, count As Integer
        count = str.Length
        For i = str.IndexOf(".") + 1 To count - 1
            If CDbl(str.Substring(i, 1)) <> 0 Then
                Return i - str.IndexOf(".")
            End If
        Next

        Return 0
    End Function

    Private Function TextWidth(ByVal Text As String, ByVal font As System.Drawing.Font) As Integer
        Dim label As New System.Windows.Forms.Label()

        label.AutoSize = True
        label.Font = font
        label.Text = Text

        Dim layoutsize As SizeF = New SizeF(label.Width, 5000.0)
        Dim g As Graphics = Graphics.FromHwnd(label.Handle)
        Dim StringSize As SizeF = g.MeasureString(Text, font, layoutsize)

        g.Dispose()

        Return System.Convert.ToInt32(StringSize.Width)
    End Function

    Private Function TextHeight(ByVal Text As String, ByVal font As System.Drawing.Font) As Integer
        Dim label As New System.Windows.Forms.Label()
        label.AutoSize = True
        label.Font = font
        label.Text = Text
        Return label.Height
    End Function

    Private Function CalcRange(ByVal MapExtents As MapWinGIS.Extents, ByVal MapUnits As UnitOfMeasure, ByRef ScaleBarUnits As UnitOfMeasure, ByVal width As Integer) As Double
        Dim midY As Double = (MapExtents.yMax + MapExtents.yMin) / 2
        Dim Retval As Double

        'Let's just protect people, and assume they really want km.
        'ArcMap will gladly give you distances in DecimalDegrees, but that's not terribly useful
        If ScaleBarUnits = UnitOfMeasure.DecimalDegrees Then ScaleBarUnits = UnitOfMeasure.Kilometers
        'Note also that ScaleBarUnits is now byref so we can tell what text to display
        'If ScaleBarUnits = UnitOfMeasure.DecimalDegrees Then
        '    Throw New System.Exception("Scalebar Units cannot be Decimal Degrees")
        'End If

        If MapUnits = UnitOfMeasure.DecimalDegrees Then
            Retval = Distance(midY, MapExtents.xMin, midY, MapExtents.xMax, ScaleBarUnits)
        Else
            Retval = Distance(MapExtents.xMin, midY, MapExtents.xMax, midY)
            Retval = ConvertUnits(Retval, MapUnits, ScaleBarUnits)
        End If

        'scale back the range to match the width requested by the user 
        '(Distance represented by one pixel must match)
        Return (width / frmMain.MapMain.Width) * Retval
    End Function

    Private Function Distance(ByVal Lat1 As Double, ByVal Long1 As Double, ByVal Lat2 As Double, ByVal Long2 As Double, ByVal DesiredUnits As UnitOfMeasure) As Double
        'calculates the distance between two decimal degree points

        If DesiredUnits = UnitOfMeasure.DecimalDegrees Then
            Throw New System.Exception("Unable to convert Distance to decimal degrees")
        End If

        Const DegToRadians As Double = (2 * Math.PI) / 360
        Const RadiusEarth As Double = 3963 ' in miles

        Dim result As Double = RadiusEarth * Math.Acos(Math.Sin(Lat1 * DegToRadians) _
        * Math.Sin(Lat2 * DegToRadians) _
        + Math.Cos(Lat1 * DegToRadians) _
        * Math.Cos(Lat2 * DegToRadians) _
        * Math.Cos(Long2 * DegToRadians - Long1 * DegToRadians))

        Return ConvertUnits(result, UnitOfMeasure.Miles, DesiredUnits)
    End Function

    Private Function Distance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        'computes the distance between two points in meters, inches, etc
        Return ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) ^ 0.5
    End Function
End Class
