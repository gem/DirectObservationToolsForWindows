'10/18/2005 - Paul Meems - Starting with translating resourcefile into Dutch.
'2/25/2008 - Chris Michaelis - Rewrote printing functionality entirely.

Friend Class frmPrintSidebarLayout
    Inherits System.Windows.Forms.Form
    Private m_Image As Bitmap
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents nudTotalSections As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nudLegendColumns As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nudLegendSections As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkInclImages As System.Windows.Forms.CheckBox
    Friend WithEvents chkInclRaster As System.Windows.Forms.CheckBox
    Friend WithEvents btnFontTitle As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFontScalebar As System.Windows.Forms.Button
    Friend WithEvents chkSbTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nudSbSections As System.Windows.Forms.NumericUpDown

    Private m_MainFont As Font
    Private m_SbFont As Font
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nudMapSections As System.Windows.Forms.NumericUpDown
    Private m_LegFont As Font
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nudSbHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlSbForeColor As System.Windows.Forms.Panel
    Friend WithEvents pnlSbBackColor As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Private NorthArrow As Bitmap

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new

        'Add any initialization after the InitializeComponent() call
        NorthArrow = New Bitmap(Me.GetType, "NorthArrow.gif")
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
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintPreviewDialog As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents grpboxLegend As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents chkScaleBar As System.Windows.Forms.CheckBox
    Friend WithEvents chkLegend As System.Windows.Forms.CheckBox
    Friend WithEvents chkVisOnly As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents btnFontLegend As System.Windows.Forms.Button
    Friend WithEvents FontDialog As System.Windows.Forms.FontDialog
    Friend WithEvents chkNorth As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintSidebarLayout))
        Me.nudTotalSections = New System.Windows.Forms.NumericUpDown
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.btnPreview = New System.Windows.Forms.Button
        Me.PrintDialog = New System.Windows.Forms.PrintDialog
        Me.grpboxLegend = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.nudLegendColumns = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.nudLegendSections = New System.Windows.Forms.NumericUpDown
        Me.btnFontLegend = New System.Windows.Forms.Button
        Me.chkInclImages = New System.Windows.Forms.CheckBox
        Me.chkInclRaster = New System.Windows.Forms.CheckBox
        Me.chkLegend = New System.Windows.Forms.CheckBox
        Me.chkVisOnly = New System.Windows.Forms.CheckBox
        Me.chkTitle = New System.Windows.Forms.CheckBox
        Me.chkNorth = New System.Windows.Forms.CheckBox
        Me.chkScaleBar = New System.Windows.Forms.CheckBox
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.FontDialog = New System.Windows.Forms.FontDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.nudMapSections = New System.Windows.Forms.NumericUpDown
        Me.btnFontTitle = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pnlSbForeColor = New System.Windows.Forms.Panel
        Me.pnlSbBackColor = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.nudSbHeight = New System.Windows.Forms.NumericUpDown
        Me.btnFontScalebar = New System.Windows.Forms.Button
        Me.chkSbTransparent = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.nudSbSections = New System.Windows.Forms.NumericUpDown
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        CType(Me.nudTotalSections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpboxLegend.SuspendLayout()
        CType(Me.nudLegendColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudLegendSections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nudMapSections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudSbHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSbSections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nudTotalSections
        '
        Me.nudTotalSections.AccessibleDescription = Nothing
        Me.nudTotalSections.AccessibleName = Nothing
        resources.ApplyResources(Me.nudTotalSections, "nudTotalSections")
        Me.nudTotalSections.Font = Nothing
        Me.nudTotalSections.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudTotalSections.Name = "nudTotalSections"
        Me.nudTotalSections.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AccessibleDescription = Nothing
        Me.PrintPreviewDialog1.AccessibleName = Nothing
        resources.ApplyResources(Me.PrintPreviewDialog1, "PrintPreviewDialog1")
        Me.PrintPreviewDialog1.BackgroundImage = Nothing
        Me.PrintPreviewDialog1.Font = Nothing
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        '
        'btnPreview
        '
        Me.btnPreview.AccessibleDescription = Nothing
        Me.btnPreview.AccessibleName = Nothing
        resources.ApplyResources(Me.btnPreview, "btnPreview")
        Me.btnPreview.BackgroundImage = Nothing
        Me.btnPreview.Font = Nothing
        Me.btnPreview.Name = "btnPreview"
        '
        'grpboxLegend
        '
        Me.grpboxLegend.AccessibleDescription = Nothing
        Me.grpboxLegend.AccessibleName = Nothing
        resources.ApplyResources(Me.grpboxLegend, "grpboxLegend")
        Me.grpboxLegend.BackgroundImage = Nothing
        Me.grpboxLegend.Controls.Add(Me.Label4)
        Me.grpboxLegend.Controls.Add(Me.nudLegendColumns)
        Me.grpboxLegend.Controls.Add(Me.Label3)
        Me.grpboxLegend.Controls.Add(Me.nudLegendSections)
        Me.grpboxLegend.Controls.Add(Me.btnFontLegend)
        Me.grpboxLegend.Controls.Add(Me.chkInclImages)
        Me.grpboxLegend.Controls.Add(Me.chkInclRaster)
        Me.grpboxLegend.Controls.Add(Me.chkLegend)
        Me.grpboxLegend.Controls.Add(Me.chkVisOnly)
        Me.grpboxLegend.Font = Nothing
        Me.grpboxLegend.Name = "grpboxLegend"
        Me.grpboxLegend.TabStop = False
        '
        'Label4
        '
        Me.Label4.AccessibleDescription = Nothing
        Me.Label4.AccessibleName = Nothing
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Font = Nothing
        Me.Label4.Name = "Label4"
        '
        'nudLegendColumns
        '
        Me.nudLegendColumns.AccessibleDescription = Nothing
        Me.nudLegendColumns.AccessibleName = Nothing
        resources.ApplyResources(Me.nudLegendColumns, "nudLegendColumns")
        Me.nudLegendColumns.Font = Nothing
        Me.nudLegendColumns.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLegendColumns.Name = "nudLegendColumns"
        Me.nudLegendColumns.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AccessibleDescription = Nothing
        Me.Label3.AccessibleName = Nothing
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Font = Nothing
        Me.Label3.Name = "Label3"
        '
        'nudLegendSections
        '
        Me.nudLegendSections.AccessibleDescription = Nothing
        Me.nudLegendSections.AccessibleName = Nothing
        resources.ApplyResources(Me.nudLegendSections, "nudLegendSections")
        Me.nudLegendSections.Font = Nothing
        Me.nudLegendSections.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLegendSections.Name = "nudLegendSections"
        Me.nudLegendSections.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'btnFontLegend
        '
        Me.btnFontLegend.AccessibleDescription = Nothing
        Me.btnFontLegend.AccessibleName = Nothing
        resources.ApplyResources(Me.btnFontLegend, "btnFontLegend")
        Me.btnFontLegend.BackgroundImage = Nothing
        Me.btnFontLegend.Font = Nothing
        Me.btnFontLegend.Name = "btnFontLegend"
        '
        'chkInclImages
        '
        Me.chkInclImages.AccessibleDescription = Nothing
        Me.chkInclImages.AccessibleName = Nothing
        resources.ApplyResources(Me.chkInclImages, "chkInclImages")
        Me.chkInclImages.BackgroundImage = Nothing
        Me.chkInclImages.Font = Nothing
        Me.chkInclImages.Name = "chkInclImages"
        '
        'chkInclRaster
        '
        Me.chkInclRaster.AccessibleDescription = Nothing
        Me.chkInclRaster.AccessibleName = Nothing
        resources.ApplyResources(Me.chkInclRaster, "chkInclRaster")
        Me.chkInclRaster.BackgroundImage = Nothing
        Me.chkInclRaster.Checked = True
        Me.chkInclRaster.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInclRaster.Font = Nothing
        Me.chkInclRaster.Name = "chkInclRaster"
        '
        'chkLegend
        '
        Me.chkLegend.AccessibleDescription = Nothing
        Me.chkLegend.AccessibleName = Nothing
        resources.ApplyResources(Me.chkLegend, "chkLegend")
        Me.chkLegend.BackgroundImage = Nothing
        Me.chkLegend.Checked = True
        Me.chkLegend.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLegend.Font = Nothing
        Me.chkLegend.Name = "chkLegend"
        '
        'chkVisOnly
        '
        Me.chkVisOnly.AccessibleDescription = Nothing
        Me.chkVisOnly.AccessibleName = Nothing
        resources.ApplyResources(Me.chkVisOnly, "chkVisOnly")
        Me.chkVisOnly.BackgroundImage = Nothing
        Me.chkVisOnly.Checked = True
        Me.chkVisOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVisOnly.Font = Nothing
        Me.chkVisOnly.Name = "chkVisOnly"
        '
        'chkTitle
        '
        Me.chkTitle.AccessibleDescription = Nothing
        Me.chkTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.chkTitle, "chkTitle")
        Me.chkTitle.BackgroundImage = Nothing
        Me.chkTitle.Checked = True
        Me.chkTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTitle.Font = Nothing
        Me.chkTitle.Name = "chkTitle"
        '
        'chkNorth
        '
        Me.chkNorth.AccessibleDescription = Nothing
        Me.chkNorth.AccessibleName = Nothing
        resources.ApplyResources(Me.chkNorth, "chkNorth")
        Me.chkNorth.BackgroundImage = Nothing
        Me.chkNorth.Checked = True
        Me.chkNorth.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNorth.Font = Nothing
        Me.chkNorth.Name = "chkNorth"
        '
        'chkScaleBar
        '
        Me.chkScaleBar.AccessibleDescription = Nothing
        Me.chkScaleBar.AccessibleName = Nothing
        resources.ApplyResources(Me.chkScaleBar, "chkScaleBar")
        Me.chkScaleBar.BackgroundImage = Nothing
        Me.chkScaleBar.Checked = True
        Me.chkScaleBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScaleBar.Font = Nothing
        Me.chkScaleBar.Name = "chkScaleBar"
        '
        'txtTitle
        '
        Me.txtTitle.AccessibleDescription = Nothing
        Me.txtTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.txtTitle, "txtTitle")
        Me.txtTitle.BackgroundImage = Nothing
        Me.txtTitle.Font = Nothing
        Me.txtTitle.Name = "txtTitle"
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleDescription = Nothing
        Me.btnPrint.AccessibleName = Nothing
        resources.ApplyResources(Me.btnPrint, "btnPrint")
        Me.btnPrint.BackgroundImage = Nothing
        Me.btnPrint.Font = Nothing
        Me.btnPrint.Name = "btnPrint"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'GroupBox2
        '
        Me.GroupBox2.AccessibleDescription = Nothing
        Me.GroupBox2.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.BackgroundImage = Nothing
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.nudMapSections)
        Me.GroupBox2.Controls.Add(Me.btnFontTitle)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.nudTotalSections)
        Me.GroupBox2.Controls.Add(Me.chkTitle)
        Me.GroupBox2.Controls.Add(Me.txtTitle)
        Me.GroupBox2.Controls.Add(Me.chkNorth)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Font = Nothing
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.AccessibleDescription = Nothing
        Me.Button1.AccessibleName = Nothing
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackgroundImage = Nothing
        Me.Button1.Font = Nothing
        Me.Button1.Image = Global.MapWindow.My.Resources.Resources.FOLDER02
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = Nothing
        Me.Label5.AccessibleName = Nothing
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Font = Nothing
        Me.Label5.Name = "Label5"
        '
        'nudMapSections
        '
        Me.nudMapSections.AccessibleDescription = Nothing
        Me.nudMapSections.AccessibleName = Nothing
        resources.ApplyResources(Me.nudMapSections, "nudMapSections")
        Me.nudMapSections.Font = Nothing
        Me.nudMapSections.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudMapSections.Name = "nudMapSections"
        Me.nudMapSections.Value = New Decimal(New Integer() {7, 0, 0, 0})
        '
        'btnFontTitle
        '
        Me.btnFontTitle.AccessibleDescription = Nothing
        Me.btnFontTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.btnFontTitle, "btnFontTitle")
        Me.btnFontTitle.BackgroundImage = Nothing
        Me.btnFontTitle.Font = Nothing
        Me.btnFontTitle.Name = "btnFontTitle"
        '
        'TextBox1
        '
        Me.TextBox1.AccessibleDescription = Nothing
        Me.TextBox1.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.BackgroundImage = Nothing
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        '
        'GroupBox3
        '
        Me.GroupBox3.AccessibleDescription = Nothing
        Me.GroupBox3.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.BackgroundImage = Nothing
        Me.GroupBox3.Controls.Add(Me.pnlSbForeColor)
        Me.GroupBox3.Controls.Add(Me.pnlSbBackColor)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.nudSbHeight)
        Me.GroupBox3.Controls.Add(Me.btnFontScalebar)
        Me.GroupBox3.Controls.Add(Me.chkSbTransparent)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.nudSbSections)
        Me.GroupBox3.Controls.Add(Me.chkScaleBar)
        Me.GroupBox3.Font = Nothing
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'pnlSbForeColor
        '
        Me.pnlSbForeColor.AccessibleDescription = Nothing
        Me.pnlSbForeColor.AccessibleName = Nothing
        resources.ApplyResources(Me.pnlSbForeColor, "pnlSbForeColor")
        Me.pnlSbForeColor.BackgroundImage = Nothing
        Me.pnlSbForeColor.Font = Nothing
        Me.pnlSbForeColor.Name = "pnlSbForeColor"
        '
        'pnlSbBackColor
        '
        Me.pnlSbBackColor.AccessibleDescription = Nothing
        Me.pnlSbBackColor.AccessibleName = Nothing
        resources.ApplyResources(Me.pnlSbBackColor, "pnlSbBackColor")
        Me.pnlSbBackColor.BackgroundImage = Nothing
        Me.pnlSbBackColor.Font = Nothing
        Me.pnlSbBackColor.Name = "pnlSbBackColor"
        '
        'Label8
        '
        Me.Label8.AccessibleDescription = Nothing
        Me.Label8.AccessibleName = Nothing
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Font = Nothing
        Me.Label8.Name = "Label8"
        '
        'Label7
        '
        Me.Label7.AccessibleDescription = Nothing
        Me.Label7.AccessibleName = Nothing
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Font = Nothing
        Me.Label7.Name = "Label7"
        '
        'Label6
        '
        Me.Label6.AccessibleDescription = Nothing
        Me.Label6.AccessibleName = Nothing
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Font = Nothing
        Me.Label6.Name = "Label6"
        '
        'nudSbHeight
        '
        Me.nudSbHeight.AccessibleDescription = Nothing
        Me.nudSbHeight.AccessibleName = Nothing
        resources.ApplyResources(Me.nudSbHeight, "nudSbHeight")
        Me.nudSbHeight.Font = Nothing
        Me.nudSbHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudSbHeight.Name = "nudSbHeight"
        Me.nudSbHeight.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'btnFontScalebar
        '
        Me.btnFontScalebar.AccessibleDescription = Nothing
        Me.btnFontScalebar.AccessibleName = Nothing
        resources.ApplyResources(Me.btnFontScalebar, "btnFontScalebar")
        Me.btnFontScalebar.BackgroundImage = Nothing
        Me.btnFontScalebar.Font = Nothing
        Me.btnFontScalebar.Name = "btnFontScalebar"
        '
        'chkSbTransparent
        '
        Me.chkSbTransparent.AccessibleDescription = Nothing
        Me.chkSbTransparent.AccessibleName = Nothing
        resources.ApplyResources(Me.chkSbTransparent, "chkSbTransparent")
        Me.chkSbTransparent.BackgroundImage = Nothing
        Me.chkSbTransparent.Font = Nothing
        Me.chkSbTransparent.Name = "chkSbTransparent"
        '
        'Label2
        '
        Me.Label2.AccessibleDescription = Nothing
        Me.Label2.AccessibleName = Nothing
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Font = Nothing
        Me.Label2.Name = "Label2"
        '
        'nudSbSections
        '
        Me.nudSbSections.AccessibleDescription = Nothing
        Me.nudSbSections.AccessibleName = Nothing
        resources.ApplyResources(Me.nudSbSections, "nudSbSections")
        Me.nudSbSections.Font = Nothing
        Me.nudSbSections.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudSbSections.Name = "nudSbSections"
        Me.nudSbSections.Value = New Decimal(New Integer() {2, 0, 0, 0})
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
        'Button2
        '
        Me.Button2.AccessibleDescription = Nothing
        Me.Button2.AccessibleName = Nothing
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.BackgroundImage = Nothing
        Me.Button2.Font = Nothing
        Me.Button2.Name = "Button2"
        '
        'Button3
        '
        Me.Button3.AccessibleDescription = Nothing
        Me.Button3.AccessibleName = Nothing
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.BackgroundImage = Nothing
        Me.Button3.Font = Nothing
        Me.Button3.Name = "Button3"
        '
        'frmPrintSidebarLayout
        '
        Me.AcceptButton = Me.btnPreview
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.grpboxLegend)
        Me.Controls.Add(Me.btnPreview)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintSidebarLayout"
        CType(Me.nudTotalSections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpboxLegend.ResumeLayout(False)
        Me.grpboxLegend.PerformLayout()
        CType(Me.nudLegendColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudLegendSections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nudMapSections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nudSbHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSbSections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.Project.FileName IsNot Nothing AndAlso Not frmMain.Project.FileName = "" Then
            txtTitle.Text = System.IO.Path.GetFileNameWithoutExtension(frmMain.Project.FileName)
        Else
            txtTitle.Text = ""
            chkTitle.Checked = False
        End If

        PrintPreviewDialog1.Icon = frmMain.Icon
        m_MainFont = New Font("Arial", 10)
        m_SbFont = New Font("Arial", 8)
        m_LegFont = New Font("Times New Roman", 10, FontStyle.Bold)
        pnlSbBackColor.BackColor = Color.White
        pnlSbForeColor.BackColor = Color.Black

        PrintDocument1.DocumentName = AppInfo.Name 'Use the "Application Name" of the current app -- usually will be "MapWindow" but since this is programmable it could be something else, e.g. "BASINS"
        PrintDocument1.DefaultPageSettings.Landscape = True

        RestoreLastSettings()
    End Sub

    Private Sub btnPrintProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PrintDialog.Document = PrintDocument1
        PrintDialog.ShowDialog()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If Not ValidateOptions() Then Return
        Try
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.Size = New Size(800, 600)
            PrintPreviewDialog1.ShowDialog()
        Catch ex As System.Drawing.Printing.InvalidPrinterException
            MapWinUtility.Logger.Msg("No valid printers are installed! Unable to create a preview image.", MsgBoxStyle.Exclamation, "No Printers Available")
            Exit Sub
        Catch ex As System.Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not ValidateOptions() Then Return
        Try
            PrintDocument1.Print()
            SaveLastSettings()
            Me.Close()
        Catch ex As System.Drawing.Printing.InvalidPrinterException
            MapWinUtility.Logger.Msg("No valid printers are installed! Unable to print.", MsgBoxStyle.Exclamation, "No Printers Available")
            Exit Sub
        End Try
    End Sub

    Private Sub RestoreLastSettings()
        If Not GetSetting("MapWindow", "Printing", "SideBarLayout_LC") = "" Then
            'It's been saved at least once... load them all
            'Note that using the control current value as the default provides it with a valid value no matter what...
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_LC", nudLegendColumns.Value.ToString()), nudLegendColumns.Value)
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_LS", nudLegendSections.Value.ToString()), nudLegendSections.Value)
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_MS", nudMapSections.Value.ToString()), nudMapSections.Value)
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_SBH", nudSbHeight.Value.ToString()), nudSbHeight.Value)
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_SBS", nudSbSections.Value.ToString()), nudSbSections.Value)
            Integer.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_TS", nudTotalSections.Value.ToString()), nudTotalSections.Value)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_II", chkInclImages.Checked.ToString()), chkInclImages.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_IR", chkInclRaster.Checked.ToString()), chkInclRaster.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_IL", chkLegend.Checked.ToString()), chkLegend.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_NA", chkNorth.Checked.ToString()), chkNorth.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_SBT", chkSbTransparent.Checked.ToString()), chkSbTransparent.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_IS", chkScaleBar.Checked.ToString()), chkScaleBar.Checked)
            Boolean.TryParse(GetSetting("MapWindow", "Printing", "SideBarLayout_VO", chkVisOnly.Checked.ToString()), chkVisOnly.Checked)
        End If
    End Sub

    Private Sub SaveLastSettings()
        SaveSetting("MapWindow", "Printing", "SideBarLayout_LC", nudLegendColumns.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_LS", nudLegendSections.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_MS", nudMapSections.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_SBH", nudSbHeight.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_SBS", nudSbSections.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_TS", nudTotalSections.Value.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_II", chkInclImages.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_IR", chkInclRaster.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_IL", chkLegend.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_NA", chkNorth.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_SBT", chkSbTransparent.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_IS", chkScaleBar.Checked.ToString())
        SaveSetting("MapWindow", "Printing", "SideBarLayout_IT", chkTitle.Text)
        SaveSetting("MapWindow", "Printing", "SideBarLayout_VO", chkVisOnly.Checked.ToString())
    End Sub

    Private Function ValidateOptions() As Boolean
        If Not nudMapSections.Value < nudTotalSections.Value Then
            MsgBox("Please ensure that the number of horizontal map sections is, at most, one less than the total sections available.", MsgBoxStyle.Information, "Validation of Options")
            Return False
        End If
        If Not nudLegendSections.Value <= nudTotalSections.Value Then
            MsgBox("Please ensure that the number of horizontal legend sections is less than or equal to the total sections available.", MsgBoxStyle.Information, "Validation of Options")
            Return False
        End If
        If Not nudSbSections.Value < nudMapSections.Value Then
            MsgBox("Please ensure that the number of scale bar sections is less than or equal to the number of map sctions sections.", MsgBoxStyle.Information, "Validation of Options")
            Return False
        End If

        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cbTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTitle.CheckedChanged
        '10/12/2005 PM Added setfocus to txtTitle and used shorter code:
        btnFontLegend.Enabled = (chkTitle.Checked)
        txtTitle.Enabled = (chkTitle.Checked)
        If chkTitle.Checked Then
            txtTitle.Focus()
        Else
            txtTitle.Text = ""
        End If
    End Sub

    Private Sub btnFontTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontTitle.Click
        FontDialog.MaxSize = 28
        FontDialog.Font = m_MainFont
        FontDialog.ShowDialog()
        m_MainFont = FontDialog.Font
    End Sub

    Private Sub btnFontLegend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontLegend.Click
        FontDialog.MaxSize = 28
        FontDialog.Font = m_LegFont
        FontDialog.ShowDialog()
        m_LegFont = FontDialog.Font
    End Sub

    Private Sub btnFontScalebar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontScalebar.Click
        FontDialog.MaxSize = 28
        FontDialog.Font = m_SbFont
        FontDialog.ShowDialog()
        m_SbFont = FontDialog.Font
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Chris Michaelis mid-November 2007 cmichaelis@happysquirrel.com
        'Revised Jan 2008 to move legend below map, to split legend items and autofit
        'Adapted to MapWindow in Feb 2008
        Dim PrintAreaHeight, PrintAreaWidth, marginLeft, marginTop As Int32
        With PrintDocument1.DefaultPageSettings
            PrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            PrintAreaWidth = .PaperSize.Width - .Margins.Left - .Margins.Right
            marginLeft = .Margins.Left
            marginTop = .Margins.Top
        End With

        If PrintDocument1.DefaultPageSettings.Landscape Then
            With PrintDocument1.DefaultPageSettings
                PrintAreaHeight = .PaperSize.Width - .Margins.Top - .Margins.Bottom
                PrintAreaWidth = .PaperSize.Height - .Margins.Left - .Margins.Right
            End With
        End If

        Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, PrintAreaWidth, PrintAreaHeight)
        Dim sectionWidth As Integer = PrintAreaWidth / nudTotalSections.Value
        Dim legendSectionWidth As Integer = (sectionWidth * nudLegendSections.Value) / nudLegendColumns.Value
        Dim sbAttemptWidth As Integer = sectionWidth * nudSbSections.Value
        Dim blockBufferSpace As Integer = 20 'How far to divide between sections of "BLOCK"

        'Legend Pass 1 - Gather Snapshots (Do before drawing map to get max height of map)
        Dim LegendBlocks As New ArrayList 'Declare these variables
        Dim MaxLegendItemHeight As Integer = 0 'out of the "if" (needed later)
        If chkLegend.Checked Then
            For i As Integer = 0 To frmMain.Legend.Layers.Count - 1
                'Ignore those layer types we've been asked to
                If ((frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.Grid And chkInclRaster.Checked) _
                Or (frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.Image And chkInclImages.Checked) _
                Or (frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.LineShapefile Or frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.PointShapefile Or frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.PolygonShapefile)) _
                And (Not frmMain.Legend.Layers(i).Type = MapWindow.Interfaces.eLayerType.Invalid) _
                And (frmMain.Legend.Layers(i).Visible Or Not chkVisOnly.Checked) Then
                    Dim o As Object = frmMain.MapMain.GetColorScheme(i)
                    Dim brks As Integer = 0
                    If o IsNot Nothing AndAlso TypeOf (o) Is MapWinGIS.ShapefileColorScheme Then brks = CType(o, MapWinGIS.ShapefileColorScheme).NumBreaks
                    'We want at most 5 items per column - the snapshot may actually use less columns than we ask for depending on other factors
                    'Correct number will be picked up during placement based on width of image.
                    Dim ColsToUse As Integer = Math.Max(1, brks / 5)
                    Dim b As Bitmap = frmMain.Legend.SnapshotHQ(frmMain.Legend.Layers(i).Handle, ColsToUse * legendSectionWidth, ColsToUse, m_LegFont.FontFamily.ToString(), 8, 8, False, True)
                    ' For Debugging -> b.Save("C:\temp\test-" + i.ToString() + ".bmp")
                    If b.Height > MaxLegendItemHeight Then MaxLegendItemHeight = b.Height
                    LegendBlocks.Add(b)
                End If
            Next i
        End If

        'Map
        Dim tmpfile As String = GetMWTempFile
        Dim tmpfile2 As String = GetMWTempFile
        Dim rescaleX As Double = 1
        Dim rescaleY As Double = 1
        System.IO.File.Delete(tmpfile)
        System.IO.File.Delete(tmpfile2)
        tmpfile = System.IO.Path.ChangeExtension(tmpfile, ".bmp")
        tmpfile2 = System.IO.Path.ChangeExtension(tmpfile, ".shp")
        'Extremely high res image - create bounding box for current extents
        Dim bounds As New MapWinGIS.Shapefile
        bounds.CreateNew(tmpfile2, MapWinGIS.ShpfileType.SHP_POLYGON)
        bounds.StartEditingShapes(True)
        Dim shp1 As New MapWinGIS.Shape
        shp1.Create(MapWinGIS.ShpfileType.SHP_POLYGON)
        Dim pt1 As New MapWinGIS.Point
        pt1.x = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).xMin
        pt1.y = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).yMin
        Dim pt2 As New MapWinGIS.Point
        pt2.x = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).xMin
        pt2.y = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).yMax
        Dim pt3 As New MapWinGIS.Point
        pt3.x = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).xMax
        pt3.y = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).yMax
        Dim pt4 As New MapWinGIS.Point
        pt4.x = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).xMax
        pt4.y = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).yMin
        Dim pt5 As New MapWinGIS.Point
        pt5.x = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).xMin
        pt5.y = CType(frmMain.MapMain.Extents, MapWinGIS.Extents).yMin
        Dim ptidx As Integer = 0
        shp1.InsertPoint(pt1, ptidx)
        ptidx += 1
        shp1.InsertPoint(pt2, ptidx)
        ptidx += 1
        shp1.InsertPoint(pt3, ptidx)
        ptidx += 1
        shp1.InsertPoint(pt4, ptidx)
        ptidx += 1
        shp1.InsertPoint(pt5, ptidx)
        ptidx = 0
        bounds.EditInsertShape(shp1, ptidx)
        bounds.StopEditingShapes(True)
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)
        Dim boundhandle As Integer = frmMain.MapMain.AddLayer(bounds, True)
        frmMain.MapMain.set_ShapeLayerLineWidth(boundhandle, 0)
        frmMain.MapMain.set_ShapeLayerDrawFill(boundhandle, False)
        frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        Dim mwImg As MapWinGIS.Image = CType(frmMain.MapMain.SnapShot2(boundhandle, 0, 1000), MapWinGIS.Image)
        mwImg.Save(tmpfile, False, MapWinGIS.ImageType.BITMAP_FILE)
        mwImg.Close()
        frmMain.MapMain.RemoveLayer(boundhandle)
        Try
            bounds.Close()
            System.IO.File.Delete(tmpfile2)
            System.IO.File.Delete(System.IO.Path.ChangeExtension(tmpfile2, ".shx"))
            System.IO.File.Delete(System.IO.Path.ChangeExtension(tmpfile2, ".dbf"))
        Catch
        End Try
        Dim MapImage As New Bitmap(tmpfile) 'calculate the image size
        Dim MapImageWidth As Integer, MapImageHeight As Integer
        'If (MapImage.Width > MapImage.Height And MapImage.Height < PrintAreaHeight - MaxLegendItemHeight) Then
        ShrinkMapIfNeeded(MapImage, MaxLegendItemHeight, sectionWidth, PrintAreaHeight, MapImageWidth, MapImageHeight, rescaleX, rescaleY)
        Dim MapBounds As New Rectangle(1 + marginLeft, marginTop, MapImageWidth - 1, MapImageHeight)
        e.Graphics.DrawImage(MapImage, MapBounds)

        'Legend Pass 2 - draw them (Do this after drawing map so we know where to put it)
        'Current placement - initialize to zero for all columns
        If chkLegend.Checked Then
            Dim ColumnUsedHeights As New Hashtable
            For i As Integer = 0 To nudLegendColumns.Value - 1
                ColumnUsedHeights(i) = 0
            Next i

            While LegendBlocks.Count > 0
                'Fit biggest in first
                Dim biggestWidth As Integer = 0
                Dim biggestHeight As Integer = 0
                Dim winnerHeight As Integer = 0
                Dim winnerWidth As Integer = 0
                Dim absoluteWinner As Integer = 0
                Dim allSameWidth As Boolean = True
                For i As Integer = 0 To LegendBlocks.Count - 1
                    Dim b As Bitmap = CType(LegendBlocks(i), Bitmap)
                    If b.Height > biggestHeight Then
                        biggestHeight = b.Height
                        winnerHeight = i
                    End If
                    If b.Width > biggestWidth Then
                        If b.Width <> biggestWidth And Not biggestWidth = 0 Then allSameWidth = False
                        biggestWidth = b.Width
                        winnerWidth = i
                    End If
                Next

                Dim c As Bitmap
                If allSameWidth Then
                    c = CType(LegendBlocks(winnerHeight), Bitmap)
                    absoluteWinner = winnerHeight
                Else
                    c = CType(LegendBlocks(winnerWidth), Bitmap)
                    absoluteWinner = winnerWidth
                End If

                'Find a column
                Dim ChosenColumn As Integer = -1
                For i As Integer = 0 To ColumnUsedHeights.Count - 1
                    Dim ColumnAvailableSpace As Integer = PrintAreaHeight - MapBounds.Height - ColumnUsedHeights(i)
                    If (ColumnAvailableSpace > c.Height) Then
                        'Likely choice - but make sure that this will either
                        'only use one column, or enough column space next to it is available
                        Dim UsesColumns As Integer = c.Width / legendSectionWidth
                        Dim invalid As Boolean = False
                        For j As Integer = i To i + UsesColumns - 1
                            If Not ColumnUsedHeights.Contains(j) Then
                                'We're off the map, so to speak
                                invalid = True
                                Exit For
                            End If
                            Dim ThisColumnAvailableSpace As Integer = PrintAreaHeight - MapBounds.Height - ColumnUsedHeights(j)
                            If Not ThisColumnAvailableSpace >= c.Height Then
                                invalid = True
                                Exit For
                            End If
                        Next
                        If Not invalid Then
                            ChosenColumn = i
                            Exit For
                        End If
                    End If
                Next
                If ChosenColumn = -1 Then
                    System.Diagnostics.Debug.WriteLine("Could not place legend item: " + absoluteWinner.ToString())
                    LegendBlocks.RemoveAt(absoluteWinner) 'Meh
                    Continue While
                End If

                'Fill in the width first, then height
                Dim legendRect As Rectangle
                If c.Height < PrintAreaHeight Then
                    'All is well
                    legendRect = New Rectangle(marginLeft + (ChosenColumn * legendSectionWidth), marginTop + MapBounds.Height + ColumnUsedHeights(ChosenColumn), c.Width, c.Height)
                Else
                    Dim legendNewWidth As Integer = CInt(PrintAreaHeight / c.Height * c.Width)
                    legendRect = New Rectangle(marginLeft + (ChosenColumn * legendSectionWidth), marginTop + MapBounds.Height + ColumnUsedHeights(ChosenColumn), legendNewWidth, PrintAreaHeight)
                End If
                e.Graphics.DrawImage(c, legendRect)

                'Update used column widths...
                Dim NumberColsDrawnIn = Math.Max(1, Math.Floor((c.Width / legendSectionWidth) + 0.5))
                For i As Integer = ChosenColumn To ChosenColumn + NumberColsDrawnIn - 1
                    If ColumnUsedHeights.Contains(i) Then ColumnUsedHeights(i) = CType(ColumnUsedHeights(i), Integer) + legendRect.Height
                Next

                LegendBlocks.RemoveAt(absoluteWinner)
            End While

            Dim ColumnUsedHeightsMax As Integer = 0
            For i As Integer = 0 To nudLegendColumns.Value - 1
                If ColumnUsedHeights(i) > ColumnUsedHeightsMax Then ColumnUsedHeightsMax = ColumnUsedHeights(i)
            Next i
            Dim PostLegendCurrentY As Integer = marginTop + MapBounds.Height + ColumnUsedHeightsMax
        End If

        'Scale Bar
        If chkScaleBar.Checked Then
            'Quick default coord unit detection:
            If Reports.StringToUOM(frmMain.Project.MapUnits) = MapWindow.Interfaces.UnitOfMeasure.Inches Then 'Unlikely
                'Unlikely. Make sure... (combined with "Static", this makes
                'determination of unit happen only once for all mousemoves)
                Dim a, b As Double
                frmMain.MapMain.PixelToProj(5, 5, a, b) 'Arbitrarily select 5 pixels out from corner
                If Math.Round(a).ToString().Length < 4 Then
                    frmMain.Project.MapUnits = MapWindow.Interfaces.UnitOfMeasure.DecimalDegrees.ToString()
                Else
                    frmMain.Project.MapUnits = MapWindow.Interfaces.UnitOfMeasure.Meters.ToString()
                End If
            End If

            Dim sbutil As New ScaleBarUtility
            Dim sbImgMiles As Bitmap = sbutil.GenerateScaleBarSolid(frmMain.MapMain.Extents, Reports.StringToUOM(frmMain.Project.MapUnits), MapWindow.Interfaces.UnitOfMeasure.Miles, sbAttemptWidth, nudSbHeight.Value, IIf(chkSbTransparent.Checked, Color.Transparent, pnlSbBackColor.BackColor), pnlSbForeColor.BackColor, m_SbFont.FontFamily.ToString())
            Dim sbImgKilometers As Bitmap = sbutil.GenerateScaleBarSolid(frmMain.MapMain.Extents, Reports.StringToUOM(frmMain.Project.MapUnits), MapWindow.Interfaces.UnitOfMeasure.Kilometers, sbAttemptWidth, nudSbHeight.Value, IIf(chkSbTransparent.Checked, Color.Transparent, pnlSbBackColor.BackColor), pnlSbForeColor.BackColor, m_SbFont.FontFamily.ToString())
            Dim generalSpacing As Integer = 20
            Dim scaledSbMilesWidth As Integer = sbImgMiles.Width * rescaleX
            Dim scaledSbMilesHeight As Integer = sbImgMiles.Height * rescaleY
            Dim scaledSbKilometersWidth As Integer = sbImgKilometers.Width * rescaleX
            Dim scaledSbKilometersHeight As Integer = sbImgKilometers.Height * rescaleY

            e.Graphics.DrawImage(sbImgKilometers, MapBounds.Left + MapBounds.Width - scaledSbKilometersWidth, MapBounds.Top + MapBounds.Height - scaledSbKilometersHeight - generalSpacing, scaledSbKilometersWidth, scaledSbKilometersHeight)
            e.Graphics.DrawImage(sbImgMiles, MapBounds.Left + MapBounds.Width - scaledSbKilometersWidth - scaledSbMilesWidth, MapBounds.Top + MapBounds.Height - scaledSbMilesHeight - generalSpacing, scaledSbMilesWidth, scaledSbMilesHeight)
        End If

        'North Arrow
        Dim NorthNewHeight As Integer = 0 'Declare this out of "If", needed below whether 0 or has value
        If chkNorth.Checked Then
            Dim NorthAdjustment As Integer = 0
            Dim NorthWidth As Integer = 0
            If NorthArrow.Width < (nudTotalSections.Value - nudMapSections.Value) * sectionWidth Then
                NorthAdjustment = CInt((((nudTotalSections.Value - nudMapSections.Value) * sectionWidth) - NorthArrow.Width) / 2)
                NorthWidth = NorthArrow.Width
            End If
            'X: Left margin + legend's section + map sections + centering adjustment
            NorthNewHeight = NorthArrow.Height
            If (NorthArrow.Width > (nudTotalSections.Value - nudMapSections.Value) * sectionWidth) Then
                NorthNewHeight = CInt((((nudTotalSections.Value - nudMapSections.Value) * sectionWidth) / NorthArrow.Width) * NorthArrow.Height)
                NorthWidth = (nudTotalSections.Value - nudMapSections.Value) * sectionWidth
            End If
            'Actually draw North logo:
            e.Graphics.DrawImage(NorthArrow, marginLeft + (sectionWidth * nudMapSections.Value) + NorthAdjustment, marginTop, NorthWidth, NorthNewHeight)
        End If

        'Text block at right side
        Dim currentBlockYStart As Integer = marginTop + NorthNewHeight + blockBufferSpace

        Dim blockTextHeight As Integer = e.Graphics.MeasureString(txtTitle.Text, m_MainFont, sectionWidth).Height
        e.Graphics.FillRectangle(Brushes.White, New RectangleF(marginLeft + (sectionWidth * nudMapSections.Value), currentBlockYStart, (nudTotalSections.Value - nudMapSections.Value) * sectionWidth, blockTextHeight))
        e.Graphics.DrawString(txtTitle.Text, m_MainFont, Brushes.Black, New RectangleF(marginLeft + (sectionWidth * nudMapSections.Value), currentBlockYStart, (nudTotalSections.Value - nudMapSections.Value) * sectionWidth, blockTextHeight))
        currentBlockYStart += blockTextHeight + blockBufferSpace

        '"Block" neat line
        e.Graphics.DrawLine(Pens.DarkGray, marginLeft + (sectionWidth * nudMapSections.Value) + 1, marginTop, marginLeft + (sectionWidth * nudMapSections.Value) + 1, PrintAreaHeight + marginTop)

        'North Arrow Neat Line
        e.Graphics.DrawLine(Pens.DarkGray, marginLeft + (sectionWidth * nudMapSections.Value) + 1, marginTop + NorthNewHeight + CType(blockBufferSpace / 2, Integer), marginLeft + PrintAreaWidth, marginTop + NorthNewHeight + CType(blockBufferSpace / 2, Integer))

        Try
            mwImg = Nothing
            MapImage.Dispose()
            MapImage = Nothing
            System.IO.File.Delete(tmpfile)
        Catch
        End Try

        'Line around full margin:
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(marginLeft, marginTop, PrintAreaWidth, PrintAreaHeight))

        e.HasMorePages = False
    End Sub

    Private Sub ShrinkMapIfNeeded(ByRef MapImage As Bitmap, ByVal MaxLegendItemHeight As Integer, ByVal sectionWidth As Integer, ByVal PrintAreaHeight As Integer, ByRef MapImageWidth As Integer, ByRef MapImageHeight As Integer, ByRef rescaleX As Double, ByRef rescaleY As Double)
        'Incrementally lower the width and/or height until it fits in both dimensions
        'This doesn't necessarily need to be in a function -- could be inline in the PrintDocument,
        'but separating it out makes it more readible...
        MapImageWidth = MapImage.Width
        MapImageHeight = MapImage.Height
        While (MapImageWidth > sectionWidth * nudMapSections.Value) Or (MapImageHeight > PrintAreaHeight - MaxLegendItemHeight)
            If (MapImageWidth > sectionWidth * nudMapSections.Value) Then
                MapImageWidth = Math.Min(MapImageWidth, sectionWidth * nudMapSections.Value)
                ' x = (new width / orig width) * old height
                rescaleX = MapImageWidth / MapImage.Width
                MapImageHeight = CInt(rescaleX * MapImage.Height)
            End If
            If (MapImageHeight > PrintAreaHeight - MaxLegendItemHeight) Then
                MapImageHeight = Math.Min(MapImageHeight, PrintAreaHeight - MaxLegendItemHeight)
                rescaleY = MapImageHeight / MapImage.Height
                MapImageWidth = CInt(rescaleY * MapImage.Width)
            End If
        End While
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fod As New OpenFileDialog
        fod.Filter = "Images (*.bmp, *.gif)|*.bmp;*.gif"
        fod.CheckFileExists = True
        If fod.ShowDialog() = Windows.Forms.DialogResult.OK Then
            NorthArrow = New Bitmap(fod.FileName)
            chkNorth.Checked = True
        End If
    End Sub

    Private Sub pnlSbBackColor_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlSbBackColor.MouseDown
        Dim clr As New ColorPickerSingle
        clr.btnStartColor.BackColor = pnlSbBackColor.BackColor
        If clr.ShowDialog() = Windows.Forms.DialogResult.OK Then
            pnlSbBackColor.BackColor = clr.btnStartColor.BackColor()
        End If
    End Sub

    Private Sub pnlSbForeColor_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlSbForeColor.MouseDown
        Dim clr As New ColorPickerSingle
        clr.btnStartColor.BackColor = pnlSbForeColor.BackColor
        If clr.ShowDialog() = Windows.Forms.DialogResult.OK Then
            pnlSbForeColor.BackColor = clr.btnStartColor.BackColor()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim pagesettings As New PageSetupDialog
        pagesettings.PageSettings = PrintDocument1.DefaultPageSettings
        pagesettings.PageSettings.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        pagesettings.PageSettings.Landscape = True 'Default to landscape - will look far better.
        pagesettings.ShowDialog()
    End Sub
End Class
