<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGEM2CSV
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
        Me.GEMDatabase = New System.Windows.Forms.TextBox()
        Me.CSVFilename = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SourceBrowse = New System.Windows.Forms.Button()
        Me.CSVFileBrowse = New System.Windows.Forms.Button()
        Me.Ok = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'GEMDatabase
        '
        Me.GEMDatabase.Location = New System.Drawing.Point(101, 15)
        Me.GEMDatabase.Name = "GEMDatabase"
        Me.GEMDatabase.Size = New System.Drawing.Size(339, 20)
        Me.GEMDatabase.TabIndex = 1
        '
        'CSVFilename
        '
        Me.CSVFilename.Location = New System.Drawing.Point(101, 41)
        Me.CSVFilename.Name = "CSVFilename"
        Me.CSVFilename.Size = New System.Drawing.Size(339, 20)
        Me.CSVFilename.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "GEM Database:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "CSV Filename:"
        '
        'SourceBrowse
        '
        Me.SourceBrowse.Location = New System.Drawing.Point(446, 13)
        Me.SourceBrowse.Name = "SourceBrowse"
        Me.SourceBrowse.Size = New System.Drawing.Size(75, 22)
        Me.SourceBrowse.TabIndex = 6
        Me.SourceBrowse.Text = "Browse"
        Me.SourceBrowse.UseVisualStyleBackColor = True
        '
        'CSVFileBrowse
        '
        Me.CSVFileBrowse.Location = New System.Drawing.Point(446, 41)
        Me.CSVFileBrowse.Name = "CSVFileBrowse"
        Me.CSVFileBrowse.Size = New System.Drawing.Size(75, 22)
        Me.CSVFileBrowse.TabIndex = 7
        Me.CSVFileBrowse.Text = "Browse"
        Me.CSVFileBrowse.UseVisualStyleBackColor = True
        '
        'Ok
        '
        Me.Ok.Location = New System.Drawing.Point(446, 69)
        Me.Ok.Name = "Ok"
        Me.Ok.Size = New System.Drawing.Size(75, 22)
        Me.Ok.TabIndex = 8
        Me.Ok.Text = "Ok"
        Me.Ok.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(366, 69)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(74, 22)
        Me.Cancel.TabIndex = 9
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmGEM2CSV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 100)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Ok)
        Me.Controls.Add(Me.CSVFileBrowse)
        Me.Controls.Add(Me.SourceBrowse)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CSVFilename)
        Me.Controls.Add(Me.GEMDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGEM2CSV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export GEM database to CSV"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GEMDatabase As System.Windows.Forms.TextBox
    Friend WithEvents CSVFilename As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SourceBrowse As System.Windows.Forms.Button
    Friend WithEvents CSVFileBrowse As System.Windows.Forms.Button
    Friend WithEvents Ok As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
