<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogManager
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
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtbxTracksPath = New System.Windows.Forms.TextBox
        Me.txtbxNmeaPath = New System.Windows.Forms.TextBox
        Me.lblNmea = New System.Windows.Forms.Label
        Me.lblTrack = New System.Windows.Forms.Label
        Me.btnLogTrack = New System.Windows.Forms.Button
        Me.btnLogNmea = New System.Windows.Forms.Button
        Me.grpbxTracks = New System.Windows.Forms.GroupBox
        Me.chkbxSelectAllTrack = New System.Windows.Forms.CheckBox
        Me.chkbxLogX = New System.Windows.Forms.CheckBox
        Me.chkbxLogY = New System.Windows.Forms.CheckBox
        Me.chkbxLogPDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogTime = New System.Windows.Forms.CheckBox
        Me.chkbxLogHDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogVDOP = New System.Windows.Forms.CheckBox
        Me.chkbxLogElev = New System.Windows.Forms.CheckBox
        Me.chkbxLogDate = New System.Windows.Forms.CheckBox
        Me.grpbxNmea = New System.Windows.Forms.GroupBox
        Me.grpbxTracks.SuspendLayout()
        Me.grpbxNmea.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnClose.Location = New System.Drawing.Point(362, 260)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtbxTracksPath
        '
        Me.txtbxTracksPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbxTracksPath.Enabled = False
        Me.txtbxTracksPath.Location = New System.Drawing.Point(6, 124)
        Me.txtbxTracksPath.Name = "txtbxTracksPath"
        Me.txtbxTracksPath.Size = New System.Drawing.Size(414, 20)
        Me.txtbxTracksPath.TabIndex = 1
        Me.txtbxTracksPath.TabStop = False
        '
        'txtbxNmeaPath
        '
        Me.txtbxNmeaPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbxNmeaPath.Enabled = False
        Me.txtbxNmeaPath.Location = New System.Drawing.Point(6, 50)
        Me.txtbxNmeaPath.Name = "txtbxNmeaPath"
        Me.txtbxNmeaPath.Size = New System.Drawing.Size(414, 20)
        Me.txtbxNmeaPath.TabIndex = 4
        Me.txtbxNmeaPath.TabStop = False
        '
        'lblNmea
        '
        Me.lblNmea.AutoSize = True
        Me.lblNmea.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNmea.Location = New System.Drawing.Point(3, 34)
        Me.lblNmea.Name = "lblNmea"
        Me.lblNmea.Size = New System.Drawing.Size(126, 17)
        Me.lblNmea.TabIndex = 8
        Me.lblNmea.Text = "Current NMEA Log"
        '
        'lblTrack
        '
        Me.lblTrack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTrack.AutoSize = True
        Me.lblTrack.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrack.Location = New System.Drawing.Point(3, 104)
        Me.lblTrack.Name = "lblTrack"
        Me.lblTrack.Size = New System.Drawing.Size(159, 17)
        Me.lblTrack.TabIndex = 9
        Me.lblTrack.Text = "Current Track Point Log"
        '
        'btnLogTrack
        '
        Me.btnLogTrack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogTrack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogTrack.Location = New System.Drawing.Point(301, 88)
        Me.btnLogTrack.Name = "btnLogTrack"
        Me.btnLogTrack.Size = New System.Drawing.Size(119, 30)
        Me.btnLogTrack.TabIndex = 1
        Me.btnLogTrack.Text = "Start Logging"
        Me.btnLogTrack.UseVisualStyleBackColor = True
        '
        'btnLogNmea
        '
        Me.btnLogNmea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogNmea.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogNmea.Location = New System.Drawing.Point(305, 14)
        Me.btnLogNmea.Name = "btnLogNmea"
        Me.btnLogNmea.Size = New System.Drawing.Size(115, 30)
        Me.btnLogNmea.TabIndex = 1
        Me.btnLogNmea.Text = "Start Logging NMEA Sentences"
        Me.btnLogNmea.UseVisualStyleBackColor = True
        '
        'grpbxTracks
        '
        Me.grpbxTracks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpbxTracks.Controls.Add(Me.chkbxSelectAllTrack)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogX)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogY)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogPDOP)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogTime)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogHDOP)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogVDOP)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogElev)
        Me.grpbxTracks.Controls.Add(Me.chkbxLogDate)
        Me.grpbxTracks.Controls.Add(Me.lblTrack)
        Me.grpbxTracks.Controls.Add(Me.txtbxTracksPath)
        Me.grpbxTracks.Controls.Add(Me.btnLogTrack)
        Me.grpbxTracks.Location = New System.Drawing.Point(12, 3)
        Me.grpbxTracks.Name = "grpbxTracks"
        Me.grpbxTracks.Size = New System.Drawing.Size(425, 150)
        Me.grpbxTracks.TabIndex = 12
        Me.grpbxTracks.TabStop = False
        Me.grpbxTracks.Text = "Tracks to Shapefile Logging"
        '
        'chkbxSelectAllTrack
        '
        Me.chkbxSelectAllTrack.Checked = True
        Me.chkbxSelectAllTrack.CheckState = System.Windows.Forms.CheckState.Checked
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
        'grpbxNmea
        '
        Me.grpbxNmea.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpbxNmea.Controls.Add(Me.btnLogNmea)
        Me.grpbxNmea.Controls.Add(Me.lblNmea)
        Me.grpbxNmea.Controls.Add(Me.txtbxNmeaPath)
        Me.grpbxNmea.Location = New System.Drawing.Point(12, 178)
        Me.grpbxNmea.Name = "grpbxNmea"
        Me.grpbxNmea.Size = New System.Drawing.Size(425, 78)
        Me.grpbxNmea.TabIndex = 13
        Me.grpbxNmea.TabStop = False
        Me.grpbxNmea.Text = "NMEA Sentence Logging"
        '
        'frmLogManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 292)
        Me.Controls.Add(Me.grpbxNmea)
        Me.Controls.Add(Me.grpbxTracks)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogManager"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Log Management"
        Me.grpbxTracks.ResumeLayout(False)
        Me.grpbxTracks.PerformLayout()
        Me.grpbxNmea.ResumeLayout(False)
        Me.grpbxNmea.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtbxTracksPath As System.Windows.Forms.TextBox
    Friend WithEvents txtbxNmeaPath As System.Windows.Forms.TextBox
    Friend WithEvents lblNmea As System.Windows.Forms.Label
    Friend WithEvents lblTrack As System.Windows.Forms.Label
    Friend WithEvents btnLogTrack As System.Windows.Forms.Button
    Friend WithEvents btnLogNmea As System.Windows.Forms.Button
    Friend WithEvents grpbxTracks As System.Windows.Forms.GroupBox
    Friend WithEvents grpbxNmea As System.Windows.Forms.GroupBox
    Friend WithEvents chkbxLogDate As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogPDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogHDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogVDOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogElev As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogX As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLogY As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxSelectAllTrack As System.Windows.Forms.CheckBox
End Class
