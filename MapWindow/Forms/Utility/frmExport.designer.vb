<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExport))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblSize = New System.Windows.Forms.Label
        Me.lblWidth = New System.Windows.Forms.Label
        Me.pbSaveExport = New System.Windows.Forms.Button
        Me.txtExportFile = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtOutFileWidth = New System.Windows.Forms.TextBox
        Me.lblZoom = New System.Windows.Forms.Label
        Me.cbClipToLayer = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pcCommitLimit = New System.Diagnostics.PerformanceCounter
        Me.pcCommittedBytes = New System.Diagnostics.PerformanceCounter
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pcCommitLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcCommittedBytes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AccessibleDescription = Nothing
        Me.TableLayoutPanel1.AccessibleName = Nothing
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.BackgroundImage = Nothing
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 0, 0)
        Me.TableLayoutPanel1.Font = Nothing
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'OK_Button
        '
        Me.OK_Button.AccessibleDescription = Nothing
        Me.OK_Button.AccessibleName = Nothing
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.BackgroundImage = Nothing
        Me.OK_Button.Font = Nothing
        Me.OK_Button.Name = "OK_Button"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.AccessibleDescription = Nothing
        Me.Cancel_Button.AccessibleName = Nothing
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.BackgroundImage = Nothing
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = Nothing
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'GroupBox1
        '
        Me.GroupBox1.AccessibleDescription = Nothing
        Me.GroupBox1.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.BackgroundImage = Nothing
        Me.GroupBox1.Controls.Add(Me.lblSize)
        Me.GroupBox1.Controls.Add(Me.lblWidth)
        Me.GroupBox1.Controls.Add(Me.pbSaveExport)
        Me.GroupBox1.Controls.Add(Me.txtExportFile)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtOutFileWidth)
        Me.GroupBox1.Controls.Add(Me.lblZoom)
        Me.GroupBox1.Controls.Add(Me.cbClipToLayer)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = Nothing
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'lblSize
        '
        Me.lblSize.AccessibleDescription = Nothing
        Me.lblSize.AccessibleName = Nothing
        resources.ApplyResources(Me.lblSize, "lblSize")
        Me.lblSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize.Font = Nothing
        Me.lblSize.Name = "lblSize"
        '
        'lblWidth
        '
        Me.lblWidth.AccessibleDescription = Nothing
        Me.lblWidth.AccessibleName = Nothing
        resources.ApplyResources(Me.lblWidth, "lblWidth")
        Me.lblWidth.Font = Nothing
        Me.lblWidth.Name = "lblWidth"
        '
        'pbSaveExport
        '
        Me.pbSaveExport.AccessibleDescription = Nothing
        Me.pbSaveExport.AccessibleName = Nothing
        resources.ApplyResources(Me.pbSaveExport, "pbSaveExport")
        Me.pbSaveExport.BackgroundImage = Nothing
        Me.pbSaveExport.Font = Nothing
        Me.pbSaveExport.Image = Global.MapWindow.My.Resources.Resources.FOLDER02
        Me.pbSaveExport.Name = "pbSaveExport"
        Me.pbSaveExport.UseVisualStyleBackColor = True
        '
        'txtExportFile
        '
        Me.txtExportFile.AccessibleDescription = Nothing
        Me.txtExportFile.AccessibleName = Nothing
        resources.ApplyResources(Me.txtExportFile, "txtExportFile")
        Me.txtExportFile.BackgroundImage = Nothing
        Me.txtExportFile.Font = Nothing
        Me.txtExportFile.Name = "txtExportFile"
        '
        'Label3
        '
        Me.Label3.AccessibleDescription = Nothing
        Me.Label3.AccessibleName = Nothing
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Font = Nothing
        Me.Label3.Name = "Label3"
        '
        'txtOutFileWidth
        '
        Me.txtOutFileWidth.AccessibleDescription = Nothing
        Me.txtOutFileWidth.AccessibleName = Nothing
        resources.ApplyResources(Me.txtOutFileWidth, "txtOutFileWidth")
        Me.txtOutFileWidth.BackgroundImage = Nothing
        Me.txtOutFileWidth.Font = Nothing
        Me.txtOutFileWidth.Name = "txtOutFileWidth"
        '
        'lblZoom
        '
        Me.lblZoom.AccessibleDescription = Nothing
        Me.lblZoom.AccessibleName = Nothing
        resources.ApplyResources(Me.lblZoom, "lblZoom")
        Me.lblZoom.Font = Nothing
        Me.lblZoom.Name = "lblZoom"
        '
        'cbClipToLayer
        '
        Me.cbClipToLayer.AccessibleDescription = Nothing
        Me.cbClipToLayer.AccessibleName = Nothing
        resources.ApplyResources(Me.cbClipToLayer, "cbClipToLayer")
        Me.cbClipToLayer.BackgroundImage = Nothing
        Me.cbClipToLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClipToLayer.Font = Nothing
        Me.cbClipToLayer.FormattingEnabled = True
        Me.cbClipToLayer.Name = "cbClipToLayer"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'pcCommitLimit
        '
        Me.pcCommitLimit.CategoryName = "Memory"
        Me.pcCommitLimit.CounterName = "Commit Limit"
        '
        'pcCommittedBytes
        '
        Me.pcCommittedBytes.CategoryName = "Memory"
        Me.pcCommittedBytes.CounterName = "Committed Bytes"
        '
        'frmExport
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = Nothing
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExport"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pcCommitLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcCommittedBytes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbClipToLayer As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOutFileWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblZoom As System.Windows.Forms.Label
    Friend WithEvents pbSaveExport As System.Windows.Forms.Button
    Friend WithEvents txtExportFile As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblWidth As System.Windows.Forms.Label
    Friend WithEvents pcCommitLimit As System.Diagnostics.PerformanceCounter
    Friend WithEvents pcCommittedBytes As System.Diagnostics.PerformanceCounter
    Friend WithEvents lblSize As System.Windows.Forms.Label

End Class
