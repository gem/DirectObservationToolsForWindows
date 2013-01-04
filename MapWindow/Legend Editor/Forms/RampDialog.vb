Friend Structure RampInfo
    Public StartColor As Color
    Public EndColor As Color
    Public NumBreaks As Integer
End Structure


Friend Class RampDialog
    Inherits System.Windows.Forms.Form

    Private curButton As Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEnd As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEndColor As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbStart As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents btnStartColor As System.Windows.Forms.Button
    Friend WithEvents lblEndColor As System.Windows.Forms.Label
    Friend WithEvents chkCalculateNewBreaks As System.Windows.Forms.CheckBox
    Friend WithEvents chkSingleColorRamp As System.Windows.Forms.CheckBox
    Friend WithEvents chkWideRange As System.Windows.Forms.CheckBox

    Private m_retval As RampInfo
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
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
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents lblNumBreaks As System.Windows.Forms.Label
    Friend WithEvents txtNumBreaks As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RampDialog))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.lblNumBreaks = New System.Windows.Forms.Label
        Me.txtNumBreaks = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pnlPreview = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkWideRange = New System.Windows.Forms.CheckBox
        Me.chkSingleColorRamp = New System.Windows.Forms.CheckBox
        Me.cmbEnd = New System.Windows.Forms.ComboBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEndColor = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbStart = New System.Windows.Forms.ComboBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.btnStartColor = New System.Windows.Forms.Button
        Me.lblEndColor = New System.Windows.Forms.Label
        Me.chkCalculateNewBreaks = New System.Windows.Forms.CheckBox
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleDescription = Nothing
        Me.btnCancel.AccessibleName = Nothing
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Nothing
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = Nothing
        Me.btnCancel.Name = "btnCancel"
        '
        'btnOk
        '
        Me.btnOk.AccessibleDescription = Nothing
        Me.btnOk.AccessibleName = Nothing
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.BackgroundImage = Nothing
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.Font = Nothing
        Me.btnOk.Name = "btnOk"
        '
        'lblNumBreaks
        '
        Me.lblNumBreaks.AccessibleDescription = Nothing
        Me.lblNumBreaks.AccessibleName = Nothing
        resources.ApplyResources(Me.lblNumBreaks, "lblNumBreaks")
        Me.lblNumBreaks.Font = Nothing
        Me.lblNumBreaks.Name = "lblNumBreaks"
        '
        'txtNumBreaks
        '
        Me.txtNumBreaks.AccessibleDescription = Nothing
        Me.txtNumBreaks.AccessibleName = Nothing
        resources.ApplyResources(Me.txtNumBreaks, "txtNumBreaks")
        Me.txtNumBreaks.BackgroundImage = Nothing
        Me.txtNumBreaks.Font = Nothing
        Me.txtNumBreaks.Name = "txtNumBreaks"
        '
        'GroupBox3
        '
        Me.GroupBox3.AccessibleDescription = Nothing
        Me.GroupBox3.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.BackgroundImage = Nothing
        Me.GroupBox3.Controls.Add(Me.pnlPreview)
        Me.GroupBox3.Font = Nothing
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'pnlPreview
        '
        Me.pnlPreview.AccessibleDescription = Nothing
        Me.pnlPreview.AccessibleName = Nothing
        resources.ApplyResources(Me.pnlPreview, "pnlPreview")
        Me.pnlPreview.BackgroundImage = Nothing
        Me.pnlPreview.Font = Nothing
        Me.pnlPreview.Name = "pnlPreview"
        '
        'GroupBox2
        '
        Me.GroupBox2.AccessibleDescription = Nothing
        Me.GroupBox2.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.BackgroundImage = Nothing
        Me.GroupBox2.Controls.Add(Me.chkWideRange)
        Me.GroupBox2.Controls.Add(Me.chkSingleColorRamp)
        Me.GroupBox2.Controls.Add(Me.cmbEnd)
        Me.GroupBox2.Controls.Add(Me.LinkLabel2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.btnEndColor)
        Me.GroupBox2.Font = Nothing
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'chkWideRange
        '
        Me.chkWideRange.AccessibleDescription = Nothing
        Me.chkWideRange.AccessibleName = Nothing
        resources.ApplyResources(Me.chkWideRange, "chkWideRange")
        Me.chkWideRange.BackgroundImage = Nothing
        Me.chkWideRange.Font = Nothing
        Me.chkWideRange.Name = "chkWideRange"
        Me.chkWideRange.UseVisualStyleBackColor = True
        '
        'chkSingleColorRamp
        '
        Me.chkSingleColorRamp.AccessibleDescription = Nothing
        Me.chkSingleColorRamp.AccessibleName = Nothing
        resources.ApplyResources(Me.chkSingleColorRamp, "chkSingleColorRamp")
        Me.chkSingleColorRamp.BackgroundImage = Nothing
        Me.chkSingleColorRamp.Font = Nothing
        Me.chkSingleColorRamp.Name = "chkSingleColorRamp"
        Me.chkSingleColorRamp.UseVisualStyleBackColor = True
        '
        'cmbEnd
        '
        Me.cmbEnd.AccessibleDescription = Nothing
        Me.cmbEnd.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbEnd, "cmbEnd")
        Me.cmbEnd.BackgroundImage = Nothing
        Me.cmbEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEnd.Font = Nothing
        Me.cmbEnd.FormattingEnabled = True
        Me.cmbEnd.Name = "cmbEnd"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AccessibleDescription = Nothing
        Me.LinkLabel2.AccessibleName = Nothing
        resources.ApplyResources(Me.LinkLabel2, "LinkLabel2")
        Me.LinkLabel2.Font = Nothing
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.TabStop = True
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'btnEndColor
        '
        Me.btnEndColor.AccessibleDescription = Nothing
        Me.btnEndColor.AccessibleName = Nothing
        resources.ApplyResources(Me.btnEndColor, "btnEndColor")
        Me.btnEndColor.BackgroundImage = Nothing
        Me.btnEndColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEndColor.Font = Nothing
        Me.btnEndColor.Name = "btnEndColor"
        '
        'GroupBox1
        '
        Me.GroupBox1.AccessibleDescription = Nothing
        Me.GroupBox1.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.BackgroundImage = Nothing
        Me.GroupBox1.Controls.Add(Me.cmbStart)
        Me.GroupBox1.Controls.Add(Me.LinkLabel1)
        Me.GroupBox1.Controls.Add(Me.btnStartColor)
        Me.GroupBox1.Controls.Add(Me.lblEndColor)
        Me.GroupBox1.Font = Nothing
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'cmbStart
        '
        Me.cmbStart.AccessibleDescription = Nothing
        Me.cmbStart.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbStart, "cmbStart")
        Me.cmbStart.BackgroundImage = Nothing
        Me.cmbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStart.Font = Nothing
        Me.cmbStart.FormattingEnabled = True
        Me.cmbStart.Name = "cmbStart"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AccessibleDescription = Nothing
        Me.LinkLabel1.AccessibleName = Nothing
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.LinkLabel1.Font = Nothing
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = True
        '
        'btnStartColor
        '
        Me.btnStartColor.AccessibleDescription = Nothing
        Me.btnStartColor.AccessibleName = Nothing
        resources.ApplyResources(Me.btnStartColor, "btnStartColor")
        Me.btnStartColor.BackgroundImage = Nothing
        Me.btnStartColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartColor.Font = Nothing
        Me.btnStartColor.Name = "btnStartColor"
        '
        'lblEndColor
        '
        Me.lblEndColor.AccessibleDescription = Nothing
        Me.lblEndColor.AccessibleName = Nothing
        resources.ApplyResources(Me.lblEndColor, "lblEndColor")
        Me.lblEndColor.Font = Nothing
        Me.lblEndColor.Name = "lblEndColor"
        '
        'chkCalculateNewBreaks
        '
        Me.chkCalculateNewBreaks.AccessibleDescription = Nothing
        Me.chkCalculateNewBreaks.AccessibleName = Nothing
        Me.chkCalculateNewBreaks.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        resources.ApplyResources(Me.chkCalculateNewBreaks, "chkCalculateNewBreaks")
        Me.chkCalculateNewBreaks.BackgroundImage = Nothing
        Me.chkCalculateNewBreaks.Checked = True
        Me.chkCalculateNewBreaks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCalculateNewBreaks.Font = Nothing
        Me.chkCalculateNewBreaks.Name = "chkCalculateNewBreaks"
        Me.chkCalculateNewBreaks.UseVisualStyleBackColor = True
        '
        'RampDialog
        '
        Me.AcceptButton = Me.btnOk
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.chkCalculateNewBreaks)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtNumBreaks)
        Me.Controls.Add(Me.lblNumBreaks)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = Nothing
        Me.Name = "RampDialog"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Friend Function GetValues() As RampInfo
        Return m_retval
    End Function

    Private Sub btnEndColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEndColor.Click
        Dim cdlg As New ColorDialog()
        frmMain.LoadCustomColors(cdlg)

        cdlg.Color = btnEndColor.BackColor
        If cdlg.ShowDialog() = DialogResult.OK Then
            frmMain.SaveCustomColors(cdlg)
            btnEndColor.BackColor = cdlg.Color
            UpdatePreview()
        End If
    End Sub

    Private Sub btnStartColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartColor.Click
        Dim cdlg As New ColorDialog()
        frmMain.LoadCustomColors(cdlg)

        cdlg.Color = btnStartColor.BackColor
        If cdlg.ShowDialog() = DialogResult.OK Then
            frmMain.SaveCustomColors(cdlg)
            btnStartColor.BackColor = cdlg.Color
            ' 2007-10-28 Jack MacDonald
            SetSingleColourRamp()

            UpdatePreview()
        End If
    End Sub

    Private Sub UpdatePreview()
        Dim br As New Drawing2D.LinearGradientBrush(pnlPreview.ClientRectangle, btnStartColor.BackColor, btnEndColor.BackColor, Drawing.Drawing2D.LinearGradientMode.Vertical)
        Dim g As Graphics = pnlPreview.CreateGraphics()

        g.FillRectangle(br, pnlPreview.ClientRectangle)
        g.Dispose()
        br.Dispose()

        ' 2007-10-28 Jack MacDonald
        ' preserve the start and end colors regardless how they were selected
        m_retval.EndColor = btnEndColor.BackColor
        m_retval.StartColor = btnStartColor.BackColor

        If Not btnEndColor.BackColor.Name = "" Then
            For i As Integer = 0 To cmbEnd.Items.Count - 1
                If cmbEnd.Items(i).ToString() = btnEndColor.BackColor.Name Then
                    cmbEnd.SelectedIndex = i
                    Exit For
                End If
            Next
        End If

        If Not btnStartColor.BackColor.Name = "" Then
            For i As Integer = 0 To cmbStart.Items.Count - 1
                If cmbStart.Items(i).ToString() = btnStartColor.BackColor.Name Then
                    cmbStart.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub txtNumBreaks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumBreaks.TextChanged
        If IsNumeric(txtNumBreaks.Text) Then
            m_retval.NumBreaks = CInt(txtNumBreaks.Text)
        Else
            txtNumBreaks.Text = CStr(m_retval.NumBreaks)
        End If
    End Sub

    Private Sub RampDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnStartColor.BackColor = MapWinUtility.Colors.IntegerToColor(Integer.Parse(GetSetting("MapWindow", "Preferences", "LastRampStartColor", CInt(Rnd() * Int32.MaxValue).ToString())))
            m_retval.StartColor = btnStartColor.BackColor
            btnEndColor.BackColor = MapWinUtility.Colors.IntegerToColor(Integer.Parse(GetSetting("MapWindow", "Preferences", "LastRampEndColor", CInt(Rnd() * Int32.MaxValue).ToString())))
            m_retval.EndColor = btnEndColor.BackColor
        Catch
            btnStartColor.BackColor = MapWinUtility.Colors.IntegerToColor(CInt(Rnd() * Int32.MaxValue))
            m_retval.StartColor = btnStartColor.BackColor
            btnEndColor.BackColor = MapWinUtility.Colors.IntegerToColor(CInt(Rnd() * Int32.MaxValue))
            m_retval.EndColor = btnEndColor.BackColor
        End Try

        For Each s As String In Split("Black,DimGray,Gray,DarkGray,Silver,LightGray,Gainsboro,WhiteSmoke,White,RosyBrown,IndianRed,Brown,Firebrick,LightCoral,Maroon,DarkRed,Red,Snow,MistyRose,Salmon,Tomato,DarkSalmon,Coral,OrangeRed,LightSalmon,Sienna,SeaShell,Chocalate,SaddleBrown,SandyBrown,PeachPuff,Peru,Linen,Bisque,DarkOrange,BurlyWood,Tan,AntiqueWhite,NavajoWhite,BlanchedAlmond,PapayaWhip,Mocassin,Orange,Wheat,OldLace,FloralWhite,DarkGoldenrod,Cornsilk,Gold,Khaki,LemonChiffon,PaleGoldenrod,DarkKhaki,Beige,LightGoldenrod,Olive,Yellow,LightYellow,Ivory,OliveDrab,YellowGreen,DarkOliveGreen,GreenYellow,Chartreuse,LawnGreen,DarkSeaGreen,ForestGreen,LimeGreen,PaleGreen,DarkGreen,Green,Lime,Honeydew,SeaGreen,MediumSeaGreen,SpringGreen,MintCream,MediumSpringGreen,MediumAquaMarine,YellowAquaMarine,Turquoise,LightSeaGreen,MediumTurquoise,DarkSlateGray,PaleTurquoise,Teal,DarkCyan,Aqua,Cyan,LightCyan,Azure,DarkTurquoise,CadetBlue,PowderBlue,LightBlue,DeepSkyBlue,SkyBlue,LightSkyBlue,SteelBlue,AliceBlue,DodgerBlue,SlateGray,LightSlateGray,LightSteelBlue,CornflowerBlue,RoyalBlue,MidnightBlue,Lavender,Navy,DarkBlue,MediumBlue,Blue,GhostWhite,SlateBlue,DarkSlateBlue,MediumSlateBlue,MediumPurple,BlueViolet,Indigo,DarkOrchid,DarkViolet,MediumOrchid,Thistle,Plum,Violet,Purple,DarkMagenta,Magenta,Fuchsia,Orchid,MediumVioletRed,DeepPink,HotPink,LavenderBlush,PaleVioletRed,Crimson,Pink,LightPink", ",")
            cmbStart.Items.Add(s)
            cmbEnd.Items.Add(s)
        Next
        cmbStart.Sorted = True
        cmbEnd.Sorted = True

        Me.Show()
        UpdatePreview()
    End Sub

    Private Sub cmbStart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStart.SelectedIndexChanged
        btnStartColor.BackColor = System.Drawing.Color.FromName(cmbStart.Items(cmbStart.SelectedIndex))
        If Me.Visible Then UpdatePreview()
    End Sub

    Private Sub cmbEnd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEnd.SelectedIndexChanged
        btnEndColor.BackColor = System.Drawing.Color.FromName(cmbStart.Items(cmbEnd.SelectedIndex))
        If Me.Visible Then UpdatePreview()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        btnEndColor_Click(btnEndColor, New EventArgs())
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        btnStartColor_Click(btnEndColor, New EventArgs())
    End Sub

    Private Sub pnlPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlPreview.Paint
        UpdatePreview()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            SaveSetting("MapWindow", "Preferences", "LastRampStartColor", MapWinUtility.Colors.ColorToInteger(btnStartColor.BackColor).ToString())
            SaveSetting("MapWindow", "Preferences", "LastRampEndColor", MapWinUtility.Colors.ColorToInteger(btnEndColor.BackColor).ToString())
        Catch
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    End Sub

    Private Sub chkSingleColorRamp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSingleColorRamp.CheckedChanged
        ' 2007-10-28 Jack MacDonald
        cmbEnd.Enabled = Not (chkSingleColorRamp.Checked)
        btnEndColor.Enabled = Not (chkSingleColorRamp.Checked)
        chkWideRange.Enabled = (chkSingleColorRamp.Checked)
        SetSingleColourRamp()

        UpdatePreview()
    End Sub

    Sub SetSingleColourRamp()
        ' create a ramp of the same HUE and SATURATION as selected in the StartColor; full ALPHA
        ' but vary the BRIGHTNESS
        ' wide range is from 20% to 90%
        ' narrow range is ends at 50% brightness, on either the light or dark side depending on the selected seed colour
        Dim B As Double
        Dim dDark As Double
        Dim dLight As Double

        If chkSingleColorRamp.Checked Then

            ' saturation of the selected start colour
            B = btnStartColor.BackColor.GetBrightness
            ' set the range to widest possible BRIGHTNESS for the selected hue
            dDark = 0.2
            dLight = 0.9
            If Not (chkWideRange.Checked) Then ' colour ramp ends at selected colour; light or dark
                If B = 0.5 Then
                    dLight = 0.5
                Else
                    dDark = 0.5
                End If
            End If

            If B < 0.5 Then ' user selected a dark colour for the start
                btnStartColor.BackColor = ColorFromAhsb(255, btnStartColor.BackColor.GetHue, btnStartColor.BackColor.GetSaturation, dDark)
                btnEndColor.BackColor = ColorFromAhsb(255, btnStartColor.BackColor.GetHue, btnStartColor.BackColor.GetSaturation, dLight)
            Else ' user selected a light colour for the start
                btnStartColor.BackColor = ColorFromAhsb(255, btnStartColor.BackColor.GetHue, btnStartColor.BackColor.GetSaturation, dLight)
                btnEndColor.BackColor = ColorFromAhsb(255, btnStartColor.BackColor.GetHue, btnStartColor.BackColor.GetSaturation, dDark)
            End If
        End If
    End Sub

    Public Function ColorFromAhsb(ByVal a As Integer, ByVal h As Double, ByVal s As Double, ByVal b As Double) As Color

        If (0 > a) Or (255 < a) Then
            Throw New ArgumentOutOfRangeException("a", a, "Invalid Alpha Value")
        End If

        If (0.0F > h) Or (360.0F < h) Then
            Throw New ArgumentOutOfRangeException("h", h, "Invalid Hue Value")
        End If

        If (0.0F > s) Or (1.0F < s) Then
            Throw New ArgumentOutOfRangeException("s", s, "Invalid Saturation Value")
        End If

        If (0.0F > b) Or (1.0F < b) Then
            Throw New ArgumentOutOfRangeException("b", b, "Invalid Brightness Value")
        End If

        If (0 = s) Then
            Return Color.FromArgb(a, Int32.Parse(b * 255), Int32.Parse(b * 255), Int32.Parse(b * 255))
        End If

        Dim fMax, fMid, fMin As Double
        Dim iSextant, iMax, iMid, iMin As Integer

        If (0.5 < b) Then
            fMax = b - (b * s) + s
            fMin = b + (b * s) - s
        Else
            fMax = b + (b * s)
            fMin = b - (b * s)
        End If

        iSextant = Int32.Parse(Math.Floor(h / 60.0F))

        If 300.0F <= h Then
            h -= 360.0F
        End If

        h /= 60.0F
        h -= 2.0F * Double.Parse(Math.Floor(((iSextant + 1.0F) Mod 6.0F) / 2.0F))

        If 0 = (iSextant Mod 2) Then
            fMid = h * (fMax - fMin) + fMin
        Else
            fMid = fMin - h * (fMax - fMin)
        End If

        iMax = (fMax * 255)
        iMid = (fMid * 255)
        iMin = (fMin * 255)

        Select Case iSextant
            Case 1
                Return Color.FromArgb(a, iMid, iMax, iMin)
            Case 2
                Return Color.FromArgb(a, iMin, iMax, iMid)
            Case 3
                Return Color.FromArgb(a, iMin, iMid, iMax)
            Case 4
                Return Color.FromArgb(a, iMid, iMin, iMax)
            Case 5
                Return Color.FromArgb(a, iMax, iMin, iMid)
            Case Else
                Return Color.FromArgb(a, iMax, iMid, iMin)
        End Select

    End Function


    Private Sub chkCalculateNewBreaks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCalculateNewBreaks.CheckedChanged
        txtNumBreaks.Enabled = chkCalculateNewBreaks.Checked
        If Not (chkCalculateNewBreaks.Checked) Then
            txtNumBreaks.Text = 0
        End If
    End Sub
End Class
