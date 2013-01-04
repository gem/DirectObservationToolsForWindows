Imports System.Drawing

Friend Class ColoringSchemeViewer
    Inherits System.Windows.Forms.UserControl

    Friend Event DataChanged()

    Friend Enum LayerType
        None
        Grid
        Shapefile
    End Enum

    ' Drawing-related constants and variables
    Private Const c_ItemSpacing As Integer = 2
    Private Const c_ItemHeight As Integer = 18
    Private Const c_NumStyles As Integer = 4
    Private Const c_BaseItemWidth As Integer = 32
    Private Const c_HeaderHeight As Integer = 16
    Private m_Div2Pos As Double = 0.55

    Private m_SelectedBreaks() As Boolean
    Friend m_ColoringSchemeType As ColoringSchemeViewer.LayerType = LayerType.None
    Friend m_SFColoringScheme As MapWinGIS.ShapefileColorScheme
    Friend m_GridColoringScheme As MapWinGIS.GridColorScheme
    Private m_brkIndex As Integer
    Private m_HasSelected As Boolean
    Private m_TxtBoxEdited As Boolean

    Private txtBrush As System.Drawing.SolidBrush
    Private selBrush As System.Drawing.SolidBrush
    Private gridPen As Pen

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_ColoringSchemeType = LayerType.None
        txtBrush = New System.Drawing.SolidBrush(Color.Black)
        selBrush = New System.Drawing.SolidBrush(Color.LightGray)
        gridPen = New Pen(Color.Black)
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                ' Cleanup
                txtBrush.Dispose()
                selBrush.Dispose()
                gridPen.Dispose()
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtBox As System.Windows.Forms.TextBox
    Friend WithEvents sBar As System.Windows.Forms.VScrollBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColoringSchemeViewer))
        Me.txtBox = New System.Windows.Forms.TextBox
        Me.sBar = New System.Windows.Forms.VScrollBar
        Me.SuspendLayout()
        '
        'txtBox
        '
        Me.txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.txtBox, "txtBox")
        Me.txtBox.Name = "txtBox"
        '
        'sBar
        '
        resources.ApplyResources(Me.sBar, "sBar")
        Me.sBar.LargeChange = 1
        Me.sBar.Maximum = 0
        Me.sBar.Name = "sBar"
        '
        'ColoringSchemeViewer
        '
        Me.Controls.Add(Me.sBar)
        Me.Controls.Add(Me.txtBox)
        Me.Name = "ColoringSchemeViewer"
        resources.ApplyResources(Me, "$this")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Sub InitializeControl(ByVal ColoringScheme As MapWinGIS.ShapefileColorScheme)
        m_ColoringSchemeType = LayerType.Shapefile
        m_SFColoringScheme = ColoringScheme
        If Not m_SFColoringScheme Is Nothing AndAlso m_SFColoringScheme.NumBreaks > 0 Then
            ReDim m_SelectedBreaks(m_SFColoringScheme.NumBreaks - 1)
        Else
            ReDim m_SelectedBreaks(0)
        End If
    End Sub

    Public Sub InitializeControl(ByVal ColoringScheme As MapWinGIS.GridColorScheme)
        m_ColoringSchemeType = LayerType.Grid
        m_GridColoringScheme = ColoringScheme '
        If Not m_GridColoringScheme Is Nothing AndAlso m_GridColoringScheme.NumBreaks > 0 Then
            ReDim m_SelectedBreaks(m_GridColoringScheme.NumBreaks - 1)
        Else
            ReDim m_SelectedBreaks(0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        ' don't do it! It will make me flicker!
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ' Draw here
        Dim bounds As Rectangle = e.ClipRectangle
        Dim i As Integer
        Dim lastBreak As Integer = CInt(sBar.Value + Math.Ceiling(Me.ClientRectangle.Height / c_ItemHeight))
        Dim bmp As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        Dim backBuffer As Graphics = Graphics.FromImage(bmp)
        UpdateScrollBar()

        gridPen.DashStyle = Drawing.Drawing2D.DashStyle.Dot
        backBuffer.Clear(Color.White)

        If m_ColoringSchemeType = LayerType.Grid Then
            If m_GridColoringScheme Is Nothing Then Exit Sub
            If Not m_SelectedBreaks Is Nothing AndAlso m_GridColoringScheme.NumBreaks <> m_SelectedBreaks.Length Then
                ReDim m_SelectedBreaks(m_GridColoringScheme.NumBreaks - 1)
            End If
            If lastBreak > m_GridColoringScheme.NumBreaks - 1 Then
                lastBreak = m_GridColoringScheme.NumBreaks - 1
            End If

            Dim tY As Integer
            DrawHeader(backBuffer)
            For i = sBar.Value To lastBreak
                tY = (i - sBar.Value) * c_ItemHeight + c_HeaderHeight
                If tY > Me.ClientRectangle.Height Then Exit For
                DrawBreak(m_GridColoringScheme.Break(i), New Rectangle(0, tY, Me.ClientRectangle.Width, c_ItemHeight), backBuffer, m_SelectedBreaks(i))
            Next

        ElseIf m_ColoringSchemeType = LayerType.Shapefile Then
            If m_SFColoringScheme Is Nothing Then Exit Sub
            If Not m_SelectedBreaks Is Nothing AndAlso m_SFColoringScheme.NumBreaks <> m_SelectedBreaks.Length Then
                ReDim m_SelectedBreaks(m_SFColoringScheme.NumBreaks - 1)
            End If
            If lastBreak > m_SFColoringScheme.NumBreaks - 1 Then
                lastBreak = m_SFColoringScheme.NumBreaks - 1
            End If

            Dim tY As Integer
            DrawHeader(backBuffer)
            For i = sBar.Value To lastBreak
                tY = (i - sBar.Value) * c_ItemHeight + c_HeaderHeight
                If tY > Me.ClientRectangle.Height Then Exit For
                DrawBreak(m_SFColoringScheme.ColorBreak(i), New Rectangle(0, tY, Me.ClientRectangle.Width, c_ItemHeight), backBuffer, m_SelectedBreaks(i))
            Next
        Else
            ' NO COLORING SCHEME!
        End If

        If m_ColoringSchemeType = LayerType.Shapefile Then
            backBuffer.DrawLine(gridPen, CInt(Me.ClientRectangle.Width * m_Div2Pos) + 38, 0, CInt(Me.ClientRectangle.Width * m_Div2Pos) + 36, Me.ClientRectangle.Height)
        Else
            backBuffer.DrawLine(gridPen, c_BaseItemWidth, 0, c_BaseItemWidth, Me.ClientRectangle.Height)
            backBuffer.DrawLine(gridPen, CInt(Me.ClientRectangle.Width * m_Div2Pos), 0, CInt(Me.ClientRectangle.Width * m_Div2Pos), Me.ClientRectangle.Height)
        End If

        e.Graphics.DrawImage(bmp, 0, 0)
        backBuffer.Dispose()
        bmp.Dispose()
    End Sub

    Private Sub DrawHeader(ByVal g As Graphics)
        Dim p As New Pen(Color.Black)
        Dim fnt As New Font(System.Drawing.FontFamily.GenericSansSerif, 11, FontStyle.Regular, GraphicsUnit.Pixel)

        If m_ColoringSchemeType = LayerType.Shapefile Then
            g.FillRectangle(selBrush, 0, 0, c_BaseItemWidth + 2, c_HeaderHeight)
            g.DrawRectangle(p, 0, 0, c_BaseItemWidth + 2, c_HeaderHeight)

            g.FillRectangle(selBrush, c_ItemSpacing + 2 + c_BaseItemWidth, 0, c_BaseItemWidth + 2, c_HeaderHeight)
            g.DrawRectangle(p, c_ItemSpacing + 2 + c_BaseItemWidth, 0, c_BaseItemWidth + 2, c_HeaderHeight)

            g.FillRectangle(selBrush, c_BaseItemWidth + 2 + c_ItemSpacing + 36, 0, CInt(Me.ClientRectangle.Width * m_Div2Pos - c_BaseItemWidth - c_ItemSpacing), c_HeaderHeight)
            g.DrawRectangle(p, c_BaseItemWidth + 2 + c_ItemSpacing + 36, 0, CInt(Me.ClientRectangle.Width * m_Div2Pos - c_BaseItemWidth - c_ItemSpacing), c_HeaderHeight)

            g.FillRectangle(selBrush, 2 + CInt(Me.ClientRectangle.Width * m_Div2Pos + c_ItemSpacing + 36), 0, CInt(Me.ClientRectangle.Width - Me.ClientRectangle.Width * m_Div2Pos - 2 * c_ItemSpacing), c_HeaderHeight)
            g.DrawRectangle(p, 2 + CInt(Me.ClientRectangle.Width * m_Div2Pos + c_ItemSpacing + 36), 0, CInt(Me.ClientRectangle.Width - Me.ClientRectangle.Width * m_Div2Pos - 2 * c_ItemSpacing), c_HeaderHeight)

            g.DrawString("Show", fnt, txtBrush, c_ItemSpacing, c_ItemSpacing)
            g.DrawString("Color", fnt, txtBrush, c_ItemSpacing + 36, c_ItemSpacing)
            g.DrawString("Value(s)", fnt, txtBrush, c_BaseItemWidth + 36 + 2 * c_ItemSpacing, c_ItemSpacing)
            g.DrawString("Text", fnt, txtBrush, 36 + CInt(Me.ClientRectangle.Width * m_Div2Pos + 2 * c_ItemSpacing), c_ItemSpacing)
        Else
            g.FillRectangle(selBrush, 0, 0, c_BaseItemWidth, c_HeaderHeight)
            g.DrawRectangle(p, 0, 0, c_BaseItemWidth, c_HeaderHeight)
            g.FillRectangle(selBrush, c_BaseItemWidth + c_ItemSpacing, 0, CInt(Me.ClientRectangle.Width * m_Div2Pos - c_BaseItemWidth - c_ItemSpacing), c_HeaderHeight)
            g.DrawRectangle(p, c_BaseItemWidth + c_ItemSpacing, 0, CInt(Me.ClientRectangle.Width * m_Div2Pos - c_BaseItemWidth - c_ItemSpacing), c_HeaderHeight)
            g.FillRectangle(selBrush, CInt(Me.ClientRectangle.Width * m_Div2Pos + c_ItemSpacing), 0, CInt(Me.ClientRectangle.Width - Me.ClientRectangle.Width * m_Div2Pos - 2 * c_ItemSpacing), c_HeaderHeight)
            g.DrawRectangle(p, CInt(Me.ClientRectangle.Width * m_Div2Pos + c_ItemSpacing), 0, CInt(Me.ClientRectangle.Width - Me.ClientRectangle.Width * m_Div2Pos - 2 * c_ItemSpacing), c_HeaderHeight)
            g.DrawString("Color", fnt, txtBrush, c_ItemSpacing, c_ItemSpacing)
            g.DrawString("Value(s)", fnt, txtBrush, c_BaseItemWidth + 2 * c_ItemSpacing, c_ItemSpacing)
            g.DrawString("Text", fnt, txtBrush, CInt(Me.ClientRectangle.Width * m_Div2Pos + 2 * c_ItemSpacing), c_ItemSpacing)
        End If
    End Sub

    Private Sub DrawBreak(ByVal brk As MapWinGIS.ShapefileColorBreak, ByVal rect As Rectangle, ByVal g As Graphics, ByVal FillRect As Boolean)
        Dim startColor, endColor As Color
        Dim colorBox As Rectangle
        Dim colorBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim valBox, capBox As RectangleF
        Dim tDiv2 As Integer = CInt(rect.Width * m_Div2Pos)

        startColor = MapWinUtility.Colors.IntegerToColor(brk.StartColor)
        endColor = MapWinUtility.Colors.IntegerToColor(brk.EndColor)
        colorBox = New Rectangle(rect.Left + 36 + c_ItemSpacing, rect.Top + c_ItemSpacing, c_BaseItemWidth - 2 * c_ItemSpacing, rect.Height - c_ItemSpacing)
        colorBrush = New System.Drawing.Drawing2D.LinearGradientBrush(colorBox, startColor, endColor, Drawing.Drawing2D.LinearGradientMode.Horizontal)
        valBox = New RectangleF(c_BaseItemWidth + 36 + c_ItemSpacing, colorBox.Top, tDiv2 - c_BaseItemWidth - 2 * c_ItemSpacing, colorBox.Height)
        capBox = New RectangleF(tDiv2 + 36 + c_ItemSpacing, colorBox.Top, rect.Width - tDiv2 - 2 * c_ItemSpacing, colorBox.Height)

        If brk.Visible Then
            g.DrawImage(New System.Drawing.Bitmap(Me.GetType(), "checked.bmp"), 10 + rect.Left + c_ItemSpacing, rect.Top + c_ItemSpacing)
        Else
            g.DrawImage(New System.Drawing.Bitmap(Me.GetType(), "unchecked.bmp"), 10 + rect.Left + c_ItemSpacing, rect.Top + c_ItemSpacing)
        End If

        ' Do the drawing
        If FillRect Then
            Dim tRect As New Rectangle(rect.X + 36, rect.Y + c_ItemSpacing, rect.Width, rect.Height - c_ItemSpacing)
            g.FillRectangle(selBrush, tRect)
        End If

        g.FillRectangle(colorBrush, colorBox)
        If CStr(brk.StartValue) = CStr(brk.EndValue) Then
            g.DrawString(CStr(brk.StartValue), txtBox.Font, txtBrush, valBox)
        Else
            g.DrawString(CStr(brk.StartValue) & " - " & CStr(brk.EndValue), txtBox.Font, txtBrush, valBox)
        End If
        g.DrawString(brk.Caption, txtBox.Font, txtBrush, capBox)

        colorBrush.Dispose()
    End Sub

    Private Sub DrawBreak(ByVal brk As MapWinGIS.GridColorBreak, ByVal rect As Rectangle, ByVal g As Graphics, ByVal FillRect As Boolean)
        Dim startColor, endColor As Color
        Dim colorBox As Rectangle
        Dim colorBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim valBox, capBox As RectangleF
        Dim tDiv2 As Integer = CInt(rect.Width * m_Div2Pos)

        startColor = MapWinUtility.Colors.IntegerToColor(brk.LowColor)
        endColor = MapWinUtility.Colors.IntegerToColor(brk.HighColor)
        colorBox = New Rectangle(rect.Left + c_ItemSpacing, rect.Top + c_ItemSpacing, c_BaseItemWidth - 2 * c_ItemSpacing, rect.Height - c_ItemSpacing)
        colorBrush = New System.Drawing.Drawing2D.LinearGradientBrush(colorBox, startColor, endColor, Drawing.Drawing2D.LinearGradientMode.Horizontal)
        valBox = New RectangleF(c_BaseItemWidth + c_ItemSpacing, colorBox.Top, tDiv2 - c_BaseItemWidth - 2 * c_ItemSpacing, colorBox.Height)
        capBox = New RectangleF(tDiv2 + c_ItemSpacing, colorBox.Top, rect.Width - tDiv2 - 2 * c_ItemSpacing, colorBox.Height)

        ' Do the drawing
        If FillRect Then
            Dim tRect As New Rectangle(rect.X, rect.Y + c_ItemSpacing, rect.Width, rect.Height - c_ItemSpacing)
            g.FillRectangle(selBrush, tRect)
        End If

        g.FillRectangle(colorBrush, colorBox)
        Dim dlval As Double = brk.LowValue
        Dim dhval As Double = brk.HighValue
        Dim lval As String
        Dim hval As String
        If dlval = Nothing Then
            lval = "(null)"
        Else
            lval = brk.LowValue.ToString()
        End If
        If dhval = Nothing Then
            hval = "(null)"
        Else
            hval = brk.HighValue.ToString()
        End If

        If brk.LowValue = brk.HighValue Then
            g.DrawString(CStr(lval), txtBox.Font, txtBrush, valBox)
        Else
            g.DrawString(lval & " - " & hval, txtBox.Font, txtBrush, valBox)
        End If
        g.DrawString(brk.Caption, txtBox.Font, txtBrush, capBox)

        colorBrush.Dispose()
    End Sub

    Private Function GetBreakIndexAtPosition(ByVal yPos As Integer) As Integer
        If yPos < c_HeaderHeight Then Return -1
        Dim tPos As Integer = sBar.Value + (yPos - c_HeaderHeight) \ c_ItemHeight
        If m_ColoringSchemeType = LayerType.Shapefile Then
            If tPos >= m_SFColoringScheme.NumBreaks Then tPos = -1
        ElseIf m_ColoringSchemeType = LayerType.Grid Then
            If tPos >= m_GridColoringScheme.NumBreaks Then tPos = -1
        End If
        Return tPos
    End Function

    Private Sub ColoringSchemeViewer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim xIndex, xPos, xWidth As Integer

        If txtBox.Focused AndAlso e.Button = MouseButtons.Left Then
            txtBox_Validating(sender, New System.ComponentModel.CancelEventArgs())
        End If

        m_brkIndex = GetBreakIndexAtPosition(e.Y)

        If m_brkIndex = -1 Then
            ClearSelectedBreaks()
            Me.Refresh()
            Exit Sub
        End If

        If e.Button = MouseButtons.Left Then
            If ((Control.ModifierKeys And Keys.Control) = Keys.Control) OrElse ((Control.ModifierKeys And Keys.Shift) = Keys.Shift) Then
                m_SelectedBreaks(m_brkIndex) = Not (m_SelectedBreaks(m_brkIndex))
                Me.Refresh()
                Exit Sub
            Else
                ClearSelectedBreaks()
                m_SelectedBreaks(m_brkIndex) = True
                If m_ColoringSchemeType = LayerType.Shapefile Then
                    If m_brkIndex >= m_SFColoringScheme.NumBreaks Then
                        txtBox.Visible = False
                        Return
                    End If
                ElseIf m_ColoringSchemeType = LayerType.Grid Then
                    If m_brkIndex >= m_GridColoringScheme.NumBreaks Then
                        txtBox.Visible = False
                        Return
                    End If
                End If

                If m_ColoringSchemeType = LayerType.Shapefile Then
                    ' Now I've determined which break I've clicked on, now... which part of it do I edit?
                    If e.X > c_BaseItemWidth * 2 Then
                        If e.X > Me.ClientRectangle.Width * m_Div2Pos Then
                            xIndex = 2
                            xPos = CInt(Me.ClientRectangle.Width * m_Div2Pos + 2 * c_ItemSpacing)
                            xWidth = Me.ClientRectangle.Width - xPos - c_ItemSpacing - CInt(IIf(sBar.Visible, sBar.Width, 0))
                        Else
                            xIndex = 1
                            xPos = c_BaseItemWidth + 2 * c_ItemSpacing
                            xWidth = CInt(Me.ClientRectangle.Width * m_Div2Pos - xPos - c_ItemSpacing - CInt(IIf(sBar.Visible, sBar.Width, 0)))
                        End If
                    ElseIf e.X > c_BaseItemWidth Then
                        Dim dlg As New ColorDialog()
                        dlg.AllowFullOpen = True
                        frmMain.LoadCustomColors(dlg)

                        If dlg.ShowDialog(Me.ParentForm) = DialogResult.OK Then
                            frmMain.SaveCustomColors(dlg)
                            If m_ColoringSchemeType = LayerType.Shapefile Then
                                Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)

                                brk.StartColor = MapWinUtility.Colors.ColorToUInteger(dlg.Color)
                                brk.EndColor = brk.StartColor
                            ElseIf m_ColoringSchemeType = LayerType.Grid Then
                                Dim brk As MapWinGIS.GridColorBreak = m_GridColoringScheme.Break(m_brkIndex)

                                brk.LowColor = MapWinUtility.Colors.ColorToUInteger(dlg.Color)
                                brk.HighColor = brk.LowColor
                            End If
                            RaiseEvent DataChanged()
                            Me.Refresh()
                        End If

                        txtBox.Visible = False
                        Return
                    Else
                        Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)
                        brk.Visible = Not brk.Visible
                    End If
                else
                    ' Now I've determined which break I've clicked on, now... which part of it do I edit?
                    If e.X > c_BaseItemWidth Then
                        If e.X > Me.ClientRectangle.Width * m_Div2Pos Then
                            xIndex = 2
                            xPos = CInt(Me.ClientRectangle.Width * m_Div2Pos + 2 * c_ItemSpacing)
                            xWidth = Me.ClientRectangle.Width - xPos - c_ItemSpacing - CInt(IIf(sBar.Visible, sBar.Width, 0))
                        Else
                            xIndex = 1
                            xPos = c_BaseItemWidth + 2 * c_ItemSpacing
                            xWidth = CInt(Me.ClientRectangle.Width * m_Div2Pos - xPos - c_ItemSpacing - CInt(IIf(sBar.Visible, sBar.Width, 0)))
                        End If
                    Else
                        Dim dlg As New ColorDialog()
                        dlg.AllowFullOpen = True
                        frmMain.LoadCustomColors(dlg)

                        If dlg.ShowDialog(Me.ParentForm) = DialogResult.OK Then
                            frmMain.SaveCustomColors(dlg)
                            If m_ColoringSchemeType = LayerType.Shapefile Then
                                Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)

                                brk.StartColor = MapWinUtility.Colors.ColorToUInteger(dlg.Color)
                                brk.EndColor = brk.StartColor
                            ElseIf m_ColoringSchemeType = LayerType.Grid Then
                                Dim brk As MapWinGIS.GridColorBreak = m_GridColoringScheme.Break(m_brkIndex)

                                brk.LowColor = MapWinUtility.Colors.ColorToUInteger(dlg.Color)
                                brk.HighColor = brk.LowColor
                            End If
                            RaiseEvent DataChanged()
                            Me.Refresh()
                        End If

                        txtBox.Visible = False
                        Return
                    End If
                End If
            End If

            If m_ColoringSchemeType = LayerType.Shapefile Then
                Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)
                If xIndex = 1 Then
                    If CStr(brk.StartValue) <> CStr(brk.EndValue) Then
                        txtBox.Text = CStr(brk.StartValue) & " - " & CStr(brk.EndValue)
                    Else
                        txtBox.Text = CStr(brk.StartValue)
                    End If
                    txtBox.SelectAll()
                    txtBox.Tag = "Value"
                Else
                    txtBox.Text = brk.Caption
                    txtBox.Tag = "Caption"
                    txtBox.SelectAll()
                End If
            ElseIf m_ColoringSchemeType = LayerType.Grid Then
                Dim brk As MapWinGIS.GridColorBreak = m_GridColoringScheme.Break(m_brkIndex)
                If xIndex = 1 Then
                    If brk.LowValue <> brk.HighValue Then
                        txtBox.Text = brk.LowValue & " - " & brk.HighValue
                    Else
                        txtBox.Text = CStr(brk.LowValue)
                    End If
                    txtBox.Tag = "Value"
                    txtBox.SelectAll()
                Else
                    txtBox.Text = brk.Caption
                    txtBox.Tag = "Caption"
                    txtBox.SelectAll()
                End If
            Else
                ' Unsupported type
            End If
            txtBox.Location = New Point(xPos, (m_brkIndex - sBar.Value) * c_ItemHeight + c_ItemSpacing + c_HeaderHeight)
            txtBox.Size = New Size(xWidth, c_ItemHeight)
            txtBox.Visible = True
            txtBox.BringToFront()
            txtBox.Focus()
            Me.Refresh()

        ElseIf e.Button = MouseButtons.Right AndAlso e.X < c_BaseItemWidth Then
            ' right mouse button clicked over color patch
            If m_ColoringSchemeType = LayerType.Shapefile Then
                Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)
                Dim dlg As New ColorPicker(brk)
                dlg.DesktopLocation = Me.PointToScreen(New Point(e.X, e.Y))
                dlg.StartPosition = FormStartPosition.Manual
                'dlg.TopMost = True
                If dlg.ShowDialog(Me.Parent) = DialogResult.OK Then
                    Me.Refresh()
                    RaiseEvent DataChanged()
                End If

            ElseIf m_ColoringSchemeType = LayerType.Grid Then
                Dim brk As MapWinGIS.GridColorBreak = m_GridColoringScheme.Break(m_brkIndex)
                Dim dlg As New ColorPicker(brk)
                dlg.DesktopLocation = Me.PointToScreen(New Point(e.X, e.Y))
                dlg.StartPosition = FormStartPosition.Manual
                'dlg.TopMost = True
                If dlg.ShowDialog(Me.Parent) = DialogResult.OK Then
                    Me.Refresh()
                    RaiseEvent DataChanged()
                End If
            End If
        End If
    End Sub

    Private Sub UpdateScrollBar()
        Dim numOnScreen As Integer = CInt(Math.Floor(Me.ClientRectangle.Height / c_ItemHeight))
        If m_ColoringSchemeType = LayerType.Grid Then
            If m_GridColoringScheme.NumBreaks > numOnScreen Then
                sBar.Visible = True
                sBar.Maximum = m_GridColoringScheme.NumBreaks - 1
                sBar.LargeChange = numOnScreen
            Else
                sBar.Visible = False
            End If
        ElseIf m_ColoringSchemeType = LayerType.Shapefile Then
            If m_SFColoringScheme.NumBreaks > numOnScreen Then
                sBar.Visible = True
                sBar.Maximum = m_SFColoringScheme.NumBreaks - 1
                sBar.LargeChange = numOnScreen
            Else
                sBar.Visible = False
            End If
        End If
    End Sub

    Private Sub ColoringSchemeViewer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        txtBox.Visible = False
        Me.Refresh()
    End Sub

    Private Sub txtBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBox.Validating
        If m_TxtBoxEdited Then
            UpdateBreakValues()
        Else
            txtBox.Hide()
            Me.Refresh()
        End If
    End Sub

    Private Sub UpdateBreakValues()
        If m_TxtBoxEdited = False OrElse m_brkIndex = -1 Then Exit Sub
        If m_ColoringSchemeType = LayerType.Shapefile Then
            Dim brk As MapWinGIS.ShapefileColorBreak = m_SFColoringScheme.ColorBreak(m_brkIndex)
            Select Case txtBox.Tag
                Case "Value"
                    If txtBox.Text = "" Then
                        'Don't try to determine if there are numbers
                        'the .indexof() will die when it tries to go beyond the range of the input
                        brk.StartValue = ""
                        brk.EndValue = ""
                    Else
                        Dim val1, val2 As Object
                        Dim i As Integer = txtBox.Text.IndexOf("-")
                        Dim j As Integer = txtBox.Text.IndexOf(" - ", 1)
                        'cdm Aug 10 3006 -- Ensure both parts are numbers if going this route.
                        'If i > 0 Then
                        If i > 0 AndAlso (IsNumeric(txtBox.Text.Substring(0, i).Trim()) And IsNumeric(CStr(txtBox.Text.Substring(i + 1)).Trim())) Then
                            ' there are two parts
                            val1 = CStr(txtBox.Text.Substring(0, i)).Trim()
                            val2 = CStr(txtBox.Text.Substring(i + 1)).Trim()
                            brk.StartValue = val1
                            brk.EndValue = IIf(CStr(val1) = CStr(val2), val1, val2)
                        ElseIf j > 0 AndAlso (IsNumeric(txtBox.Text.Substring(0, j).Trim()) And IsNumeric(CStr(txtBox.Text.Substring(j + 3)).Trim())) Then
                            val1 = CStr(txtBox.Text.Substring(0, j)).Trim()
                            val2 = CStr(txtBox.Text.Substring(j + 1)).Trim()
                            brk.StartValue = val1
                            brk.EndValue = IIf(CStr(val1) = CStr(val2), val1, val2)
                        Else
                            brk.StartValue = txtBox.Text.Trim()
                            brk.EndValue = txtBox.Text.Trim()
                        End If
                    End If
                Case "Caption"
                    brk.Caption = txtBox.Text
            End Select
        Else
            Dim brk As MapWinGIS.GridColorBreak = m_GridColoringScheme.Break(m_brkIndex)
            Select Case txtBox.Tag
                Case "Value"
                    Dim val1, val2 As String
                    Dim i As Integer = txtBox.Text.IndexOf(" - ")
                    Dim j As Integer = txtBox.Text.IndexOf("-", 1)

                    If i > 0 Then
                        ' there are two parts
                        val1 = CStr(txtBox.Text.Substring(0, i)).Trim()
                        val2 = CStr(txtBox.Text.Substring(i + 3)).Trim()
                        If IsNumeric(val1) = False Or IsNumeric(val2) = False Then
                            MapWinUtility.Logger.Msg("Please only enter numeric values.", "Coloring Scheme Editor")
                            Exit Sub
                        End If
                        brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(val1, 0.0)
                        brk.HighValue = MapWinUtility.MiscUtils.ParseDouble(val2, 0.0)
                    ElseIf j > 0 Then
                        ' there are two parts
                        val1 = CStr(txtBox.Text.Substring(0, j)).Trim()
                        val2 = CStr(txtBox.Text.Substring(j + 1)).Trim()
                        If IsNumeric(val1) = False Or IsNumeric(val2) = False Then
                            MapWinUtility.Logger.Msg("Please only enter numeric values.", "Coloring Scheme Editor")
                            Exit Sub
                        End If
                        brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(val1, 0.0)
                        brk.HighValue = MapWinUtility.MiscUtils.ParseDouble(val2, 0.0)
                    Else
                        Try
                            brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(txtBox.Text.Trim(), 0.0)
                            brk.HighValue = MapWinUtility.MiscUtils.ParseDouble(txtBox.Text.Trim(), 0.0)
                        Catch
                            MapWinUtility.Logger.Msg("The value entered was not understood. Please use either numbers or the syntax 'start - end', e.g. '1 - 5'.", MsgBoxStyle.Exclamation, "Value Not Understood")
                            Exit Sub
                        End Try
                    End If
                Case "Caption"
                    brk.Caption = txtBox.Text
            End Select
        End If
        txtBox.Hide()
        Me.Refresh()
        m_TxtBoxEdited = False
        RaiseEvent DataChanged()
    End Sub

    Private Sub sBar_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles sBar.Scroll
        txtBox_Validating(sender, New System.ComponentModel.CancelEventArgs())
        txtBox.Hide()
        sBar.Value = sBar.Value
        Me.Refresh()
    End Sub

    Private Sub ColoringSchemeViewer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        If txtBox.Focused = True Then
            Dim ev As New System.ComponentModel.CancelEventArgs()
            txtBox_Validating(sender, ev)
        End If
    End Sub

    Private Sub txtBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBox.TextChanged
        m_TxtBoxEdited = True
    End Sub

    Friend ReadOnly Property SelectedBreaks() As Boolean()
        Get
            Return m_SelectedBreaks
        End Get
    End Property

    Friend Sub SetSelectedBreak(ByVal brk As Integer, ByVal selected As Boolean)
        m_SelectedBreaks(brk) = selected
    End Sub

    Friend Sub ClearSelectedBreaks()
        m_TxtBoxEdited = False
        txtBox.Visible = False
        If m_SelectedBreaks Is Nothing OrElse m_SelectedBreaks.Length = 0 Then
            Exit Sub
        End If
        System.Array.Clear(m_SelectedBreaks, 0, m_SelectedBreaks.Length)
    End Sub

    Private Sub ColoringSchemeViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
