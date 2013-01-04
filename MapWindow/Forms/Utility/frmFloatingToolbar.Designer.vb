<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFloatingToolbar
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tsc = New System.Windows.Forms.ToolStripContainer
        Me.tsc.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsc
        '
        '
        'tsc.ContentPanel
        '
        Me.tsc.ContentPanel.Size = New System.Drawing.Size(292, 0)
        Me.tsc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tsc.Location = New System.Drawing.Point(0, 0)
        Me.tsc.Name = "tsc"
        Me.tsc.Size = New System.Drawing.Size(292, 23)
        Me.tsc.TabIndex = 0
        Me.tsc.Text = "ToolStripContainer1"
        '
        'frmFloatingToolbar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(292, 23)
        Me.Controls.Add(Me.tsc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MinimumSize = New System.Drawing.Size(292, 23)
        Me.Name = "frmFloatingToolbar"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.tsc.ResumeLayout(False)
        Me.tsc.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsc As System.Windows.Forms.ToolStripContainer
End Class
