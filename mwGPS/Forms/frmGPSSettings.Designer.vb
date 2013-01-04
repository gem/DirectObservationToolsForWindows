<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGPSSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGPSSettings))
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtbxTrackPath = New System.Windows.Forms.TextBox
        Me.txtbxNmeaPath = New System.Windows.Forms.TextBox
        Me.lblNmea = New System.Windows.Forms.Label
        Me.lblTrack = New System.Windows.Forms.Label
        Me.grpbxLogging = New System.Windows.Forms.GroupBox
        Me.btnBrowseTrackLine = New System.Windows.Forms.Button
        Me.lblTrackLine = New System.Windows.Forms.Label
        Me.txtbxTrackLine = New System.Windows.Forms.TextBox
        Me.btnBrowseNMEA = New System.Windows.Forms.Button
        Me.btnBrowseTrack = New System.Windows.Forms.Button
        Me.chkbxSelectAllTrack = New System.Windows.Forms.CheckBox
        Me.chkbxLogX = New System.Windows.Forms.CheckBox
        Me.chkbxLogY = New System.Windows.Forms.CheckBox
        Me.chkbxLogPDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogTime = New System.Windows.Forms.CheckBox
        Me.chkbxLogHDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogVDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogElev = New System.Windows.Forms.CheckBox
        Me.chkbxLogDate = New System.Windows.Forms.CheckBox
        Me.grpbxDrawing = New System.Windows.Forms.GroupBox
        Me.lblArrowColor = New System.Windows.Forms.Label
        Me.cmbxArrowColor = New System.Windows.Forms.ComboBox
        Me.lblCrosshaircolor = New System.Windows.Forms.Label
        Me.cmbxCrosshairColor = New System.Windows.Forms.ComboBox
        Me.lblCrosshairLine = New System.Windows.Forms.Label
        Me.cmbxCrosshairLineWidth = New System.Windows.Forms.ComboBox
        Me.ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpbxLogging.SuspendLayout()
        Me.grpbxDrawing.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnClose.Location = New System.Drawing.Point(363, 328)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtbxTrackPath
        '
        Me.txtbxTrackPath.Enabled = False
        Me.txtbxTrackPath.Location = New System.Drawing.Point(6, 107)
        Me.txtbxTrackPath.Name = "txtbxTrackPath"
        Me.txtbxTrackPath.Size = New System.Drawing.Size(383, 20)
        Me.txtbxTrackPath.TabIndex = 1
        Me.txtbxTrackPath.TabStop = False
        '
        'txtbxNmeaPath
        '
        Me.txtbxNmeaPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbxNmeaPath.Enabled = False
        Me.txtbxNmeaPath.Location = New System.Drawing.Point(6, 204)
        Me.txtbxNmeaPath.Name = "txtbxNmeaPath"
        Me.txtbxNmeaPath.Size = New System.Drawing.Size(383, 20)
        Me.txtbxNmeaPath.TabIndex = 4
        Me.txtbxNmeaPath.TabStop = False
        '
        'lblNmea
        '
        Me.lblNmea.AutoSize = True
        Me.lblNmea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNmea.Location = New System.Drawing.Point(3, 185)
        Me.lblNmea.Name = "lblNmea"
        Me.lblNmea.Size = New System.Drawing.Size(133, 13)
        Me.lblNmea.TabIndex = 8
        Me.lblNmea.Text = "NMEA Sentence Log Path"
        '
        'lblTrack
        '
        Me.lblTrack.AutoSize = True
        Me.lblTrack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrack.Location = New System.Drawing.Point(3, 87)
        Me.lblTrack.Name = "lblTrack"
        Me.lblTrack.Size = New System.Drawing.Size(87, 13)
        Me.lblTrack.TabIndex = 9
        Me.lblTrack.Text = "Track Point Path"
        '
        'grpbxLogging
        '
        Me.grpbxLogging.Controls.Add(Me.btnBrowseTrackLine)
        Me.grpbxLogging.Controls.Add(Me.lblTrackLine)
        Me.grpbxLogging.Controls.Add(Me.txtbxTrackLine)
        Me.grpbxLogging.Controls.Add(Me.btnBrowseNMEA)
        Me.grpbxLogging.Controls.Add(Me.btnBrowseTrack)
        Me.grpbxLogging.Controls.Add(Me.lblNmea)
        Me.grpbxLogging.Controls.Add(Me.chkbxSelectAllTrack)
        Me.grpbxLogging.Controls.Add(Me.txtbxNmeaPath)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogX)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogY)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogPDOP)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogTime)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogHDOP)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogVDOP)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogElev)
        Me.grpbxLogging.Controls.Add(Me.chkbxLogDate)
        Me.grpbxLogging.Controls.Add(Me.lblTrack)
        Me.grpbxLogging.Controls.Add(Me.txtbxTrackPath)
        Me.grpbxLogging.Location = New System.Drawing.Point(12, 88)
        Me.grpbxLogging.Name = "grpbxLogging"
        Me.grpbxLogging.Size = New System.Drawing.Size(425, 237)
        Me.grpbxLogging.TabIndex = 12
        Me.grpbxLogging.TabStop = False
        Me.grpbxLogging.Text = "Logging Settings"
        '
        'btnBrowseTrackLine
        '
        Me.btnBrowseTrackLine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseTrackLine.Image = CType(resources.GetObject("btnBrowseTrackLine.Image"), System.Drawing.Image)
        Me.btnBrowseTrackLine.Location = New System.Drawing.Point(395, 155)
        Me.btnBrowseTrackLine.Name = "btnBrowseTrackLine"
        Me.btnBrowseTrackLine.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowseTrackLine.TabIndex = 15
        '
        'lblTrackLine
        '
        Me.lblTrackLine.AutoSize = True
        Me.lblTrackLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrackLine.Location = New System.Drawing.Point(3, 136)
        Me.lblTrackLine.Name = "lblTrackLine"
        Me.lblTrackLine.Size = New System.Drawing.Size(83, 13)
        Me.lblTrackLine.TabIndex = 14
        Me.lblTrackLine.Text = "Track Line Path"
        '
        'txtbxTrackLine
        '
        Me.txtbxTrackLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbxTrackLine.Enabled = False
        Me.txtbxTrackLine.Location = New System.Drawing.Point(6, 155)
        Me.txtbxTrackLine.Name = "txtbxTrackLine"
        Me.txtbxTrackLine.Size = New System.Drawing.Size(383, 20)
        Me.txtbxTrackLine.TabIndex = 13
        Me.txtbxTrackLine.TabStop = False
        '
        'btnBrowseNMEA
        '
        Me.btnBrowseNMEA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseNMEA.Image = CType(resources.GetObject("btnBrowseNMEA.Image"), System.Drawing.Image)
        Me.btnBrowseNMEA.Location = New System.Drawing.Point(395, 204)
        Me.btnBrowseNMEA.Name = "btnBrowseNMEA"
        Me.btnBrowseNMEA.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowseNMEA.TabIndex = 12
        '
        'btnBrowseTrack
        '
        Me.btnBrowseTrack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseTrack.Image = CType(resources.GetObject("btnBrowseTrack.Image"), System.Drawing.Image)
        Me.btnBrowseTrack.Location = New System.Drawing.Point(395, 104)
        Me.btnBrowseTrack.Name = "btnBrowseTrack"
        Me.btnBrowseTrack.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowseTrack.TabIndex = 11
        '
        'chkbxSelectAllTrack
        '
        Me.chkbxSelectAllTrack.Location = New System.Drawing.Point(136, 19)
        Me.chkbxSelectAllTrack.Name = "chkbxSelectAllTrack"
        Me.chkbxSelectAllTrack.Size = New System.Drawing.Size(149, 18)
        Me.chkbxSelectAllTrack.TabIndex = 0
        Me.chkbxSelectAllTrack.Text = "Select All Log Attributes"
        Me.chkbxSelectAllTrack.UseVisualStyleBackColor = True
        '
        'chkbxLogX
        '
        Me.chkbxLogX.Checked = True
        Me.chkbxLogX.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogX.Location = New System.Drawing.Point(204, 43)
        Me.chkbxLogX.Name = "chkbxLogX"
        Me.chkbxLogX.Size = New System.Drawing.Size(107, 17)
        Me.chkbxLogX.TabIndex = 4
        Me.chkbxLogX.Text = "Log Location X"
        Me.chkbxLogX.UseVisualStyleBackColor = True
        '
        'chkbxLogY
        '
        Me.chkbxLogY.Checked = True
        Me.chkbxLogY.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogY.Location = New System.Drawing.Point(317, 43)
        Me.chkbxLogY.Name = "chkbxLogY"
        Me.chkbxLogY.Size = New System.Drawing.Size(102, 17)
        Me.chkbxLogY.TabIndex = 5
        Me.chkbxLogY.Text = "Log Location Y"
        Me.chkbxLogY.UseVisualStyleBackColor = True
        '
        'chkbxLogPDOP
        '
        Me.chkbxLogPDOP.Checked = True
        Me.chkbxLogPDOP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogPDOP.Location = New System.Drawing.Point(111, 66)
        Me.chkbxLogPDOP.Name = "chkbxLogPDOP"
        Me.chkbxLogPDOP.Size = New System.Drawing.Size(87, 17)
        Me.chkbxLogPDOP.TabIndex = 7
        Me.chkbxLogPDOP.Text = "Log PDOP"
        Me.chkbxLogPDOP.UseVisualStyleBackColor = True
        '
        'chkbxLogTime
        '
        Me.chkbxLogTime.Checked = True
        Me.chkbxLogTime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogTime.Location = New System.Drawing.Point(111, 43)
        Me.chkbxLogTime.Name = "chkbxLogTime"
        Me.chkbxLogTime.Size = New System.Drawing.Size(70, 17)
        Me.chkbxLogTime.TabIndex = 3
        Me.chkbxLogTime.Text = "Log Time"
        Me.chkbxLogTime.UseVisualStyleBackColor = True
        '
        'chkbxLogHDOP
        '
        Me.chkbxLogHDOP.Checked = True
        Me.chkbxLogHDOP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogHDOP.Location = New System.Drawing.Point(204, 66)
        Me.chkbxLogHDOP.Name = "chkbxLogHDOP"
        Me.chkbxLogHDOP.Size = New System.Drawing.Size(98, 17)
        Me.chkbxLogHDOP.TabIndex = 8
        Me.chkbxLogHDOP.Text = "Log HDOP"
        Me.chkbxLogHDOP.UseVisualStyleBackColor = True
        '
        'chkbxLogVDOP
        '
        Me.chkbxLogVDOP.Checked = True
        Me.chkbxLogVDOP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogVDOP.Location = New System.Drawing.Point(317, 65)
        Me.chkbxLogVDOP.Name = "chkbxLogVDOP"
        Me.chkbxLogVDOP.Size = New System.Drawing.Size(102, 17)
        Me.chkbxLogVDOP.TabIndex = 9
        Me.chkbxLogVDOP.Text = "Log VDOP"
        Me.chkbxLogVDOP.UseVisualStyleBackColor = True
        '
        'chkbxLogElev
        '
        Me.chkbxLogElev.Checked = True
        Me.chkbxLogElev.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogElev.Location = New System.Drawing.Point(6, 66)
        Me.chkbxLogElev.Name = "chkbxLogElev"
        Me.chkbxLogElev.Size = New System.Drawing.Size(104, 17)
        Me.chkbxLogElev.TabIndex = 6
        Me.chkbxLogElev.Text = "Log Elevation"
        Me.chkbxLogElev.UseVisualStyleBackColor = True
        '
        'chkbxLogDate
        '
        Me.chkbxLogDate.Checked = True
        Me.chkbxLogDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxLogDate.Location = New System.Drawing.Point(6, 39)
        Me.chkbxLogDate.Name = "chkbxLogDate"
        Me.chkbxLogDate.Size = New System.Drawing.Size(104, 24)
        Me.chkbxLogDate.TabIndex = 2
        Me.chkbxLogDate.Text = "Log Date"
        Me.chkbxLogDate.UseVisualStyleBackColor = True
        '
        'grpbxDrawing
        '
        Me.grpbxDrawing.Controls.Add(Me.lblArrowColor)
        Me.grpbxDrawing.Controls.Add(Me.cmbxArrowColor)
        Me.grpbxDrawing.Controls.Add(Me.lblCrosshaircolor)
        Me.grpbxDrawing.Controls.Add(Me.cmbxCrosshairColor)
        Me.grpbxDrawing.Controls.Add(Me.lblCrosshairLine)
        Me.grpbxDrawing.Controls.Add(Me.cmbxCrosshairLineWidth)
        Me.grpbxDrawing.Location = New System.Drawing.Point(12, 4)
        Me.grpbxDrawing.Name = "grpbxDrawing"
        Me.grpbxDrawing.Size = New System.Drawing.Size(425, 78)
        Me.grpbxDrawing.TabIndex = 14
        Me.grpbxDrawing.TabStop = False
        Me.grpbxDrawing.Text = "Drawing Settings"
        '
        'lblArrowColor
        '
        Me.lblArrowColor.AutoSize = True
        Me.lblArrowColor.Location = New System.Drawing.Point(5, 49)
        Me.lblArrowColor.Name = "lblArrowColor"
        Me.lblArrowColor.Size = New System.Drawing.Size(108, 13)
        Me.lblArrowColor.TabIndex = 5
        Me.lblArrowColor.Text = "Location Arrow Color:"
        '
        'cmbxArrowColor
        '
        Me.cmbxArrowColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxArrowColor.FormattingEnabled = True
        Me.cmbxArrowColor.Location = New System.Drawing.Point(140, 46)
        Me.cmbxArrowColor.Name = "cmbxArrowColor"
        Me.cmbxArrowColor.Size = New System.Drawing.Size(116, 21)
        Me.cmbxArrowColor.TabIndex = 4
        '
        'lblCrosshaircolor
        '
        Me.lblCrosshaircolor.AutoSize = True
        Me.lblCrosshaircolor.Location = New System.Drawing.Point(5, 22)
        Me.lblCrosshaircolor.Name = "lblCrosshaircolor"
        Me.lblCrosshaircolor.Size = New System.Drawing.Size(124, 13)
        Me.lblCrosshaircolor.TabIndex = 3
        Me.lblCrosshaircolor.Text = "Location Crosshair Color:"
        '
        'cmbxCrosshairColor
        '
        Me.cmbxCrosshairColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxCrosshairColor.FormattingEnabled = True
        Me.cmbxCrosshairColor.Location = New System.Drawing.Point(140, 19)
        Me.cmbxCrosshairColor.Name = "cmbxCrosshairColor"
        Me.cmbxCrosshairColor.Size = New System.Drawing.Size(116, 21)
        Me.cmbxCrosshairColor.TabIndex = 2
        '
        'lblCrosshairLine
        '
        Me.lblCrosshairLine.AutoSize = True
        Me.lblCrosshairLine.Location = New System.Drawing.Point(268, 22)
        Me.lblCrosshairLine.Name = "lblCrosshairLine"
        Me.lblCrosshairLine.Size = New System.Drawing.Size(107, 13)
        Me.lblCrosshairLine.TabIndex = 1
        Me.lblCrosshairLine.Text = "Crosshair Line Width:"
        '
        'cmbxCrosshairLineWidth
        '
        Me.cmbxCrosshairLineWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxCrosshairLineWidth.FormattingEnabled = True
        Me.cmbxCrosshairLineWidth.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbxCrosshairLineWidth.Location = New System.Drawing.Point(381, 19)
        Me.cmbxCrosshairLineWidth.Name = "cmbxCrosshairLineWidth"
        Me.cmbxCrosshairLineWidth.Size = New System.Drawing.Size(38, 21)
        Me.cmbxCrosshairLineWidth.TabIndex = 0
        '
        'frmGPSSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 363)
        Me.Controls.Add(Me.grpbxDrawing)
        Me.Controls.Add(Me.grpbxLogging)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGPSSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GPS Settings"
        Me.grpbxLogging.ResumeLayout(False)
        Me.grpbxLogging.PerformLayout()
        Me.grpbxDrawing.ResumeLayout(False)
        Me.grpbxDrawing.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtbxTrackPath As System.Windows.Forms.TextBox
    Friend WithEvents txtbxNmeaPath As System.Windows.Forms.TextBox
    Friend WithEvents lblNmea As System.Windows.Forms.Label
    Friend WithEvents lblTrack As System.Windows.Forms.Label
    Friend WithEvents grpbxLogging As System.Windows.Forms.GroupBox
    Friend WithEvents chkbxLogDate As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogPDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogHDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogVDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogElev As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogX As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogY As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxSelectAllTrack As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseTrack As System.Windows.Forms.Button
    Friend WithEvents btnBrowseNMEA As System.Windows.Forms.Button
    Friend WithEvents grpbxDrawing As System.Windows.Forms.GroupBox
    Friend WithEvents lblCrosshairLine As System.Windows.Forms.Label
    Friend WithEvents cmbxCrosshairLineWidth As System.Windows.Forms.ComboBox
    Friend WithEvents lblArrowColor As System.Windows.Forms.Label
    Friend WithEvents cmbxArrowColor As System.Windows.Forms.ComboBox
    Friend WithEvents lblCrosshaircolor As System.Windows.Forms.Label
    Friend WithEvents cmbxCrosshairColor As System.Windows.Forms.ComboBox
    Friend WithEvents ttip As System.Windows.Forms.ToolTip
    Friend WithEvents btnBrowseTrackLine As System.Windows.Forms.Button
    Friend WithEvents lblTrackLine As System.Windows.Forms.Label
    Friend WithEvents txtbxTrackLine As System.Windows.Forms.TextBox
End Class
