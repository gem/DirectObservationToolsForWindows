<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDisplayGPS
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
        Me.tbctrlTabs = New System.Windows.Forms.TabControl
        Me.tbpgBasic = New System.Windows.Forms.TabPage
        Me.lblVdop = New System.Windows.Forms.Label
        Me.lblPdop = New System.Windows.Forms.Label
        Me.lblBearing = New System.Windows.Forms.Label
        Me.lblSpeed = New System.Windows.Forms.Label
        Me.lblElev = New System.Windows.Forms.Label
        Me.lblSats = New System.Windows.Forms.Label
        Me.picCompass = New System.Windows.Forms.PictureBox
        Me.lblLonDD = New System.Windows.Forms.Label
        Me.lblLatDD = New System.Windows.Forms.Label
        Me.lblHdop = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.tpbgSats = New System.Windows.Forms.TabPage
        Me.grpbxSNR = New System.Windows.Forms.GroupBox
        Me.picSatBars = New System.Windows.Forms.PictureBox
        Me.lblSatVdop = New System.Windows.Forms.Label
        Me.lblSatHdop = New System.Windows.Forms.Label
        Me.lblSatPdop = New System.Windows.Forms.Label
        Me.lblSatsInUse = New System.Windows.Forms.Label
        Me.picSatellites = New System.Windows.Forms.PictureBox
        Me.tbpgNmea = New System.Windows.Forms.TabPage
        Me.lstbxNmea = New System.Windows.Forms.ListBox
        Me.btnFeedFile = New System.Windows.Forms.Button
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.chkbxCompassMode = New System.Windows.Forms.CheckBox
        Me.chkbxShowBorder = New System.Windows.Forms.CheckBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.tbctrlTabs.SuspendLayout()
        Me.tbpgBasic.SuspendLayout()
        CType(Me.picCompass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpbgSats.SuspendLayout()
        Me.grpbxSNR.SuspendLayout()
        CType(Me.picSatBars, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSatellites, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpgNmea.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbctrlTabs
        '
        Me.tbctrlTabs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbctrlTabs.Controls.Add(Me.tbpgBasic)
        Me.tbctrlTabs.Controls.Add(Me.tpbgSats)
        Me.tbctrlTabs.Controls.Add(Me.tbpgNmea)
        Me.tbctrlTabs.Location = New System.Drawing.Point(1, 3)
        Me.tbctrlTabs.Name = "tbctrlTabs"
        Me.tbctrlTabs.SelectedIndex = 0
        Me.tbctrlTabs.Size = New System.Drawing.Size(417, 175)
        Me.tbctrlTabs.TabIndex = 9
        '
        'tbpgBasic
        '
        Me.tbpgBasic.Controls.Add(Me.lblVdop)
        Me.tbpgBasic.Controls.Add(Me.lblPdop)
        Me.tbpgBasic.Controls.Add(Me.lblBearing)
        Me.tbpgBasic.Controls.Add(Me.lblSpeed)
        Me.tbpgBasic.Controls.Add(Me.lblElev)
        Me.tbpgBasic.Controls.Add(Me.lblSats)
        Me.tbpgBasic.Controls.Add(Me.picCompass)
        Me.tbpgBasic.Controls.Add(Me.lblLonDD)
        Me.tbpgBasic.Controls.Add(Me.lblLatDD)
        Me.tbpgBasic.Controls.Add(Me.lblHdop)
        Me.tbpgBasic.Controls.Add(Me.lblStatus)
        Me.tbpgBasic.Location = New System.Drawing.Point(4, 22)
        Me.tbpgBasic.Name = "tbpgBasic"
        Me.tbpgBasic.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpgBasic.Size = New System.Drawing.Size(409, 149)
        Me.tbpgBasic.TabIndex = 0
        Me.tbpgBasic.Text = "Basic"
        Me.tbpgBasic.UseVisualStyleBackColor = True
        '
        'lblVdop
        '
        Me.lblVdop.AutoSize = True
        Me.lblVdop.Location = New System.Drawing.Point(161, 106)
        Me.lblVdop.Name = "lblVdop"
        Me.lblVdop.Size = New System.Drawing.Size(40, 13)
        Me.lblVdop.TabIndex = 27
        Me.lblVdop.Text = "VDOP:"
        '
        'lblPdop
        '
        Me.lblPdop.AutoSize = True
        Me.lblPdop.Location = New System.Drawing.Point(8, 106)
        Me.lblPdop.Name = "lblPdop"
        Me.lblPdop.Size = New System.Drawing.Size(40, 13)
        Me.lblPdop.TabIndex = 26
        Me.lblPdop.Text = "PDOP:"
        '
        'lblBearing
        '
        Me.lblBearing.AutoSize = True
        Me.lblBearing.Location = New System.Drawing.Point(87, 130)
        Me.lblBearing.Name = "lblBearing"
        Me.lblBearing.Size = New System.Drawing.Size(46, 13)
        Me.lblBearing.TabIndex = 12
        Me.lblBearing.Text = "Bearing:"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Location = New System.Drawing.Point(8, 130)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(41, 13)
        Me.lblSpeed.TabIndex = 11
        Me.lblSpeed.Text = "Speed:"
        '
        'lblElev
        '
        Me.lblElev.AutoSize = True
        Me.lblElev.Location = New System.Drawing.Point(8, 81)
        Me.lblElev.Name = "lblElev"
        Me.lblElev.Size = New System.Drawing.Size(54, 13)
        Me.lblElev.TabIndex = 22
        Me.lblElev.Text = "Elevation:"
        '
        'lblSats
        '
        Me.lblSats.AutoSize = True
        Me.lblSats.Location = New System.Drawing.Point(127, 81)
        Me.lblSats.Name = "lblSats"
        Me.lblSats.Size = New System.Drawing.Size(74, 13)
        Me.lblSats.TabIndex = 21
        Me.lblSats.Text = "# of Satellites:"
        '
        'picCompass
        '
        Me.picCompass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCompass.BackgroundImage = Global.mwGPS.My.Resources.Resources.compass2
        Me.picCompass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCompass.InitialImage = Nothing
        Me.picCompass.Location = New System.Drawing.Point(244, 6)
        Me.picCompass.Name = "picCompass"
        Me.picCompass.Size = New System.Drawing.Size(139, 139)
        Me.picCompass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCompass.TabIndex = 20
        Me.picCompass.TabStop = False
        '
        'lblLonDD
        '
        Me.lblLonDD.AutoSize = True
        Me.lblLonDD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLonDD.Location = New System.Drawing.Point(6, 55)
        Me.lblLonDD.Name = "lblLonDD"
        Me.lblLonDD.Size = New System.Drawing.Size(82, 13)
        Me.lblLonDD.TabIndex = 18
        Me.lblLonDD.Text = "Longitude (DD):"
        '
        'lblLatDD
        '
        Me.lblLatDD.AutoSize = True
        Me.lblLatDD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLatDD.Location = New System.Drawing.Point(7, 31)
        Me.lblLatDD.Name = "lblLatDD"
        Me.lblLatDD.Size = New System.Drawing.Size(73, 13)
        Me.lblLatDD.TabIndex = 17
        Me.lblLatDD.Text = "Latitude (DD):"
        '
        'lblHdop
        '
        Me.lblHdop.AutoSize = True
        Me.lblHdop.Location = New System.Drawing.Point(87, 106)
        Me.lblHdop.Name = "lblHdop"
        Me.lblHdop.Size = New System.Drawing.Size(41, 13)
        Me.lblHdop.TabIndex = 15
        Me.lblHdop.Text = "HDOP:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(7, 7)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblStatus.TabIndex = 13
        Me.lblStatus.Text = "Status: No Fix"
        '
        'tpbgSats
        '
        Me.tpbgSats.Controls.Add(Me.grpbxSNR)
        Me.tpbgSats.Controls.Add(Me.lblSatVdop)
        Me.tpbgSats.Controls.Add(Me.lblSatHdop)
        Me.tpbgSats.Controls.Add(Me.lblSatPdop)
        Me.tpbgSats.Controls.Add(Me.lblSatsInUse)
        Me.tpbgSats.Controls.Add(Me.picSatellites)
        Me.tpbgSats.Location = New System.Drawing.Point(4, 22)
        Me.tpbgSats.Name = "tpbgSats"
        Me.tpbgSats.Padding = New System.Windows.Forms.Padding(3)
        Me.tpbgSats.Size = New System.Drawing.Size(409, 149)
        Me.tpbgSats.TabIndex = 1
        Me.tpbgSats.Text = "Satellites"
        Me.tpbgSats.UseVisualStyleBackColor = True
        '
        'grpbxSNR
        '
        Me.grpbxSNR.Controls.Add(Me.picSatBars)
        Me.grpbxSNR.Location = New System.Drawing.Point(152, 51)
        Me.grpbxSNR.Name = "grpbxSNR"
        Me.grpbxSNR.Size = New System.Drawing.Size(254, 95)
        Me.grpbxSNR.TabIndex = 29
        Me.grpbxSNR.TabStop = False
        Me.grpbxSNR.Text = "Signal-to-Noise Ratios"
        '
        'picSatBars
        '
        Me.picSatBars.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSatBars.BackColor = System.Drawing.Color.Transparent
        Me.picSatBars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSatBars.InitialImage = Nothing
        Me.picSatBars.Location = New System.Drawing.Point(0, 17)
        Me.picSatBars.Name = "picSatBars"
        Me.picSatBars.Size = New System.Drawing.Size(251, 75)
        Me.picSatBars.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSatBars.TabIndex = 23
        Me.picSatBars.TabStop = False
        '
        'lblSatVdop
        '
        Me.lblSatVdop.AutoSize = True
        Me.lblSatVdop.Location = New System.Drawing.Point(330, 27)
        Me.lblSatVdop.Name = "lblSatVdop"
        Me.lblSatVdop.Size = New System.Drawing.Size(43, 13)
        Me.lblSatVdop.TabIndex = 28
        Me.lblSatVdop.Text = "VDOP: "
        '
        'lblSatHdop
        '
        Me.lblSatHdop.AutoSize = True
        Me.lblSatHdop.Location = New System.Drawing.Point(238, 27)
        Me.lblSatHdop.Name = "lblSatHdop"
        Me.lblSatHdop.Size = New System.Drawing.Size(44, 13)
        Me.lblSatHdop.TabIndex = 27
        Me.lblSatHdop.Text = "HDOP: "
        '
        'lblSatPdop
        '
        Me.lblSatPdop.AutoSize = True
        Me.lblSatPdop.Location = New System.Drawing.Point(152, 27)
        Me.lblSatPdop.Name = "lblSatPdop"
        Me.lblSatPdop.Size = New System.Drawing.Size(43, 13)
        Me.lblSatPdop.TabIndex = 25
        Me.lblSatPdop.Text = "PDOP: "
        '
        'lblSatsInUse
        '
        Me.lblSatsInUse.AutoSize = True
        Me.lblSatsInUse.Location = New System.Drawing.Point(152, 3)
        Me.lblSatsInUse.Name = "lblSatsInUse"
        Me.lblSatsInUse.Size = New System.Drawing.Size(138, 13)
        Me.lblSatsInUse.TabIndex = 24
        Me.lblSatsInUse.Text = "Number of Satellites In Use:"
        '
        'picSatellites
        '
        Me.picSatellites.BackColor = System.Drawing.Color.Transparent
        Me.picSatellites.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSatellites.Image = Global.mwGPS.My.Resources.Resources.satellite_position
        Me.picSatellites.InitialImage = Nothing
        Me.picSatellites.Location = New System.Drawing.Point(6, 3)
        Me.picSatellites.Name = "picSatellites"
        Me.picSatellites.Size = New System.Drawing.Size(140, 140)
        Me.picSatellites.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSatellites.TabIndex = 21
        Me.picSatellites.TabStop = False
        '
        'tbpgNmea
        '
        Me.tbpgNmea.Controls.Add(Me.lstbxNmea)
        Me.tbpgNmea.Location = New System.Drawing.Point(4, 22)
        Me.tbpgNmea.Name = "tbpgNmea"
        Me.tbpgNmea.Size = New System.Drawing.Size(409, 149)
        Me.tbpgNmea.TabIndex = 2
        Me.tbpgNmea.Text = "NMEA"
        Me.tbpgNmea.UseVisualStyleBackColor = True
        '
        'lstbxNmea
        '
        Me.lstbxNmea.FormattingEnabled = True
        Me.lstbxNmea.HorizontalScrollbar = True
        Me.lstbxNmea.Location = New System.Drawing.Point(5, 4)
        Me.lstbxNmea.Name = "lstbxNmea"
        Me.lstbxNmea.Size = New System.Drawing.Size(398, 147)
        Me.lstbxNmea.TabIndex = 3
        '
        'btnFeedFile
        '
        Me.btnFeedFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFeedFile.Location = New System.Drawing.Point(3, 186)
        Me.btnFeedFile.Name = "btnFeedFile"
        Me.btnFeedFile.Size = New System.Drawing.Size(75, 23)
        Me.btnFeedFile.TabIndex = 10
        Me.btnFeedFile.Text = "Input Log"
        Me.btnFeedFile.UseVisualStyleBackColor = True
        Me.btnFeedFile.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMain.Controls.Add(Me.chkbxCompassMode)
        Me.pnlMain.Controls.Add(Me.chkbxShowBorder)
        Me.pnlMain.Controls.Add(Me.tbctrlTabs)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnFeedFile)
        Me.pnlMain.Location = New System.Drawing.Point(4, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(421, 219)
        Me.pnlMain.TabIndex = 13
        '
        'chkbxCompassMode
        '
        Me.chkbxCompassMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxCompassMode.Location = New System.Drawing.Point(220, 180)
        Me.chkbxCompassMode.Name = "chkbxCompassMode"
        Me.chkbxCompassMode.Size = New System.Drawing.Size(112, 17)
        Me.chkbxCompassMode.TabIndex = 29
        Me.chkbxCompassMode.Text = "Compass Mode"
        Me.chkbxCompassMode.UseVisualStyleBackColor = True
        '
        'chkbxShowBorder
        '
        Me.chkbxShowBorder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxShowBorder.Location = New System.Drawing.Point(220, 199)
        Me.chkbxShowBorder.Name = "chkbxShowBorder"
        Me.chkbxShowBorder.Size = New System.Drawing.Size(105, 17)
        Me.chkbxShowBorder.TabIndex = 28
        Me.chkbxShowBorder.Text = "Lock Window"
        Me.chkbxShowBorder.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(338, 186)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmDisplayGPS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 220)
        Me.Controls.Add(Me.pnlMain)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(431, 244)
        Me.MinimizeBox = False
        Me.Name = "frmDisplayGPS"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Display GPS Information"
        Me.tbctrlTabs.ResumeLayout(False)
        Me.tbpgBasic.ResumeLayout(False)
        Me.tbpgBasic.PerformLayout()
        CType(Me.picCompass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpbgSats.ResumeLayout(False)
        Me.tpbgSats.PerformLayout()
        Me.grpbxSNR.ResumeLayout(False)
        CType(Me.picSatBars, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSatellites, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpgNmea.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbctrlTabs As System.Windows.Forms.TabControl
    Friend WithEvents tbpgBasic As System.Windows.Forms.TabPage
    Friend WithEvents lblHdop As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblBearing As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents tpbgSats As System.Windows.Forms.TabPage
    Friend WithEvents lblLonDD As System.Windows.Forms.Label
    Friend WithEvents lblLatDD As System.Windows.Forms.Label
    Friend WithEvents tbpgNmea As System.Windows.Forms.TabPage
    Friend WithEvents picCompass As System.Windows.Forms.PictureBox
    Friend WithEvents btnFeedFile As System.Windows.Forms.Button
    Friend WithEvents lblSats As System.Windows.Forms.Label
    Friend WithEvents lblElev As System.Windows.Forms.Label
    Friend WithEvents picSatellites As System.Windows.Forms.PictureBox
    Friend WithEvents lblVdop As System.Windows.Forms.Label
    Friend WithEvents lblPdop As System.Windows.Forms.Label
    Friend WithEvents lstbxNmea As System.Windows.Forms.ListBox
    Friend WithEvents lblSatHdop As System.Windows.Forms.Label
    Friend WithEvents lblSatPdop As System.Windows.Forms.Label
    Friend WithEvents lblSatsInUse As System.Windows.Forms.Label
    Friend WithEvents lblSatVdop As System.Windows.Forms.Label
    Friend WithEvents picSatBars As System.Windows.Forms.PictureBox
    Friend WithEvents grpbxSNR As System.Windows.Forms.GroupBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents chkbxShowBorder As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkbxCompassMode As System.Windows.Forms.CheckBox
End Class
