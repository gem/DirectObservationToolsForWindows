<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSync
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
        Me.SourceDatabase = New System.Windows.Forms.TextBox()
        Me.TargetDatabase = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SourceBrowse = New System.Windows.Forms.Button()
        Me.TargetBrowse = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Ok = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'SourceDatabase
        '
        Me.SourceDatabase.Location = New System.Drawing.Point(109, 18)
        Me.SourceDatabase.Name = "SourceDatabase"
        Me.SourceDatabase.Size = New System.Drawing.Size(278, 20)
        Me.SourceDatabase.TabIndex = 0
        '
        'TargetDatabase
        '
        Me.TargetDatabase.Location = New System.Drawing.Point(109, 44)
        Me.TargetDatabase.Name = "TargetDatabase"
        Me.TargetDatabase.Size = New System.Drawing.Size(278, 20)
        Me.TargetDatabase.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source database:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Target database:"
        '
        'SourceBrowse
        '
        Me.SourceBrowse.Location = New System.Drawing.Point(393, 16)
        Me.SourceBrowse.Name = "SourceBrowse"
        Me.SourceBrowse.Size = New System.Drawing.Size(84, 22)
        Me.SourceBrowse.TabIndex = 4
        Me.SourceBrowse.Text = "Browse"
        Me.SourceBrowse.UseVisualStyleBackColor = True
        '
        'TargetBrowse
        '
        Me.TargetBrowse.Location = New System.Drawing.Point(393, 42)
        Me.TargetBrowse.Name = "TargetBrowse"
        Me.TargetBrowse.Size = New System.Drawing.Size(84, 22)
        Me.TargetBrowse.TabIndex = 5
        Me.TargetBrowse.Text = "Browse"
        Me.TargetBrowse.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(306, 81)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(81, 22)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Ok
        '
        Me.Ok.Location = New System.Drawing.Point(393, 81)
        Me.Ok.Name = "Ok"
        Me.Ok.Size = New System.Drawing.Size(84, 22)
        Me.Ok.TabIndex = 7
        Me.Ok.Text = "Ok"
        Me.Ok.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 112)
        Me.Controls.Add(Me.Ok)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.TargetBrowse)
        Me.Controls.Add(Me.SourceBrowse)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TargetDatabase)
        Me.Controls.Add(Me.SourceDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Synchronise GEM Databases"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SourceDatabase As System.Windows.Forms.TextBox
    Friend WithEvents TargetDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SourceBrowse As System.Windows.Forms.Button
    Friend WithEvents TargetBrowse As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Ok As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
