Friend Class ColorPicker
    Inherits System.Windows.Forms.Form

    Private m_SFBreak As MapWinGIS.ShapefileColorBreak
    Private m_GrdBreak As MapWinGIS.GridColorBreak
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbStart As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEnd As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Private curButton As Windows.Forms.Button

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call
    'End Sub

    Public Sub New(ByRef Break As MapWinGIS.ShapefileColorBreak)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        btnStartColor.BackColor = MapWinUtility.Colors.IntegerToColor(Break.StartColor)
        btnEndColor.BackColor = MapWinUtility.Colors.IntegerToColor(Break.EndColor)

        m_SFBreak = Break
        m_GrdBreak = Nothing
    End Sub

    Public Sub New(ByRef Break As MapWinGIS.GridColorBreak)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        btnStartColor.BackColor = MapWinUtility.Colors.IntegerToColor(Break.LowColor)
        btnEndColor.BackColor = MapWinUtility.Colors.IntegerToColor(Break.HighColor)

        m_SFBreak = Nothing
        m_GrdBreak = Break
    End Sub

#Region " Windows Form Designer generated code "
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
    Friend WithEvents btnStartColor As System.Windows.Forms.Button
    Friend WithEvents btnEndColor As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblEndColor As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColorPicker))
        Me.btnStartColor = New System.Windows.Forms.Button
        Me.btnEndColor = New System.Windows.Forms.Button
        Me.lblEndColor = New System.Windows.Forms.Label
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.pnlPreview = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbStart = New System.Windows.Forms.ComboBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbEnd = New System.Windows.Forms.ComboBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
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
        'lblEndColor
        '
        Me.lblEndColor.AccessibleDescription = Nothing
        Me.lblEndColor.AccessibleName = Nothing
        resources.ApplyResources(Me.lblEndColor, "lblEndColor")
        Me.lblEndColor.Font = Nothing
        Me.lblEndColor.Name = "lblEndColor"
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
        'pnlPreview
        '
        Me.pnlPreview.AccessibleDescription = Nothing
        Me.pnlPreview.AccessibleName = Nothing
        resources.ApplyResources(Me.pnlPreview, "pnlPreview")
        Me.pnlPreview.BackgroundImage = Nothing
        Me.pnlPreview.Font = Nothing
        Me.pnlPreview.Name = "pnlPreview"
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
        'GroupBox2
        '
        Me.GroupBox2.AccessibleDescription = Nothing
        Me.GroupBox2.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.BackgroundImage = Nothing
        Me.GroupBox2.Controls.Add(Me.cmbEnd)
        Me.GroupBox2.Controls.Add(Me.LinkLabel2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.btnEndColor)
        Me.GroupBox2.Font = Nothing
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
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
        'ColorPicker
        '
        Me.AcceptButton = Me.btnOk
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorPicker"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnStartColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartColor.Click
        Dim dlg As New ColorDialog()
        dlg.Color = btnStartColor.BackColor
        frmMain.LoadCustomColors(dlg)
        dlg.AllowFullOpen = True
        If dlg.ShowDialog = DialogResult.OK Then
            btnStartColor.BackColor = dlg.Color
            frmMain.SaveCustomColors(dlg)
            UpdatePreview()
        End If
    End Sub

    Private Sub btnEndColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEndColor.Click
        Dim dlg As New ColorDialog()
        dlg.AllowFullOpen = True
        dlg.AnyColor = True
        dlg.Color = btnEndColor.BackColor
        frmMain.LoadCustomColors(dlg)
        dlg.AllowFullOpen = True
        If dlg.ShowDialog = DialogResult.OK Then
            btnEndColor.BackColor = dlg.Color
            frmMain.SaveCustomColors(dlg)
            UpdatePreview()
        End If
    End Sub

    Private Sub UpdatePreview()
        Dim br As New Drawing2D.LinearGradientBrush(pnlPreview.ClientRectangle, btnStartColor.BackColor, btnEndColor.BackColor, Drawing.Drawing2D.LinearGradientMode.Vertical)
        Dim g As Graphics = pnlPreview.CreateGraphics()
        g.FillRectangle(br, pnlPreview.ClientRectangle)
        g.Dispose()
        br.Dispose()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
        If Not m_GrdBreak Is Nothing Then
            m_GrdBreak.LowColor = MapWinUtility.Colors.ColorToUInteger(btnStartColor.BackColor)
            m_GrdBreak.HighColor = MapWinUtility.Colors.ColorToUInteger(btnEndColor.BackColor)
        ElseIf Not m_SFBreak Is Nothing Then
            m_SFBreak.StartColor = MapWinUtility.Colors.ColorToUInteger(btnStartColor.BackColor)
            m_SFBreak.EndColor = MapWinUtility.Colors.ColorToUInteger(btnEndColor.BackColor)
        End If
        Me.Hide()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub pnlPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlPreview.Paint
        UpdatePreview()
    End Sub

    Private Sub ColorPicker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each s As String In Split("Black,DimGray,Gray,DarkGray,Silver,LightGray,Gainsboro,WhiteSmoke,White,RosyBrown,IndianRed,Brown,Firebrick,LightCoral,Maroon,DarkRed,Red,Snow,MistyRose,Salmon,Tomato,DarkSalmon,Coral,OrangeRed,LightSalmon,Sienna,SeaShell,Chocalate,SaddleBrown,SandyBrown,PeachPuff,Peru,Linen,Bisque,DarkOrange,BurlyWood,Tan,AntiqueWhite,NavajoWhite,BlanchedAlmond,PapayaWhip,Mocassin,Orange,Wheat,OldLace,FloralWhite,DarkGoldenrod,Cornsilk,Gold,Khaki,LemonChiffon,PaleGoldenrod,DarkKhaki,Beige,LightGoldenrod,Olive,Yellow,LightYellow,Ivory,OliveDrab,YellowGreen,DarkOliveGreen,GreenYellow,Chartreuse,LawnGreen,DarkSeaGreen,ForestGreen,LimeGreen,PaleGreen,DarkGreen,Green,Lime,Honeydew,SeaGreen,MediumSeaGreen,SpringGreen,MintCream,MediumSpringGreen,MediumAquaMarine,YellowAquaMarine,Turquoise,LightSeaGreen,MediumTurquoise,DarkSlateGray,PaleTurquoise,Teal,DarkCyan,Aqua,Cyan,LightCyan,Azure,DarkTurquoise,CadetBlue,PowderBlue,LightBlue,DeepSkyBlue,SkyBlue,LightSkyBlue,SteelBlue,AliceBlue,DodgerBlue,SlateGray,LightSlateGray,LightSteelBlue,CornflowerBlue,RoyalBlue,MidnightBlue,Lavender,Navy,DarkBlue,MediumBlue,Blue,GhostWhite,SlateBlue,DarkSlateBlue,MediumSlateBlue,MediumPurple,BlueViolet,Indigo,DarkOrchid,DarkViolet,MediumOrchid,Thistle,Plum,Violet,Purple,DarkMagenta,Magenta,Fuchsia,Orchid,MediumVioletRed,DeepPink,HotPink,LavenderBlush,PaleVioletRed,Crimson,Pink,LightPink", ",")
            cmbStart.Items.Add(s)
            cmbEnd.Items.Add(s)
        Next
        cmbStart.Sorted = True
        cmbEnd.Sorted = True
    End Sub

    Public Sub New(ByVal startc As Color, ByVal endc As Color)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.m_GrdBreak = Nothing
        Me.m_SFBreak = Nothing

        btnStartColor.BackColor = startc
        btnEndColor.BackColor = endc
        UpdatePreview()
    End Sub

    Private Sub cmbStart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStart.SelectedIndexChanged
        btnStartColor.BackColor = System.Drawing.Color.FromName(cmbStart.Items(cmbStart.SelectedIndex))
        UpdatePreview()
    End Sub

    Private Sub cmbEnd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEnd.SelectedIndexChanged
        btnEndColor.BackColor = System.Drawing.Color.FromName(cmbStart.Items(cmbEnd.SelectedIndex))
        UpdatePreview()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        btnEndColor_Click(btnEndColor, New EventArgs())
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        btnStartColor_Click(btnStartColor, New EventArgs())
    End Sub

    Private Sub lblEndColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblEndColor.Click

    End Sub
End Class
