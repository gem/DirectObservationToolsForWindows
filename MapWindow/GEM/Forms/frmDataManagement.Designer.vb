<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataManagement
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
        Me.SyncDatabases = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExportToKML = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExportToShapefile = New System.Windows.Forms.Button()
        Me.ExportToCSV = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SyncDatabases
        '
        Me.SyncDatabases.Location = New System.Drawing.Point(203, 10)
        Me.SyncDatabases.Name = "SyncDatabases"
        Me.SyncDatabases.Size = New System.Drawing.Size(56, 23)
        Me.SyncDatabases.TabIndex = 0
        Me.SyncDatabases.Text = "OK"
        Me.SyncDatabases.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Synchronise two GEM databases"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(40, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Export a GEM database to KML"
        '
        'ExportToKML
        '
        Me.ExportToKML.Location = New System.Drawing.Point(203, 39)
        Me.ExportToKML.Name = "ExportToKML"
        Me.ExportToKML.Size = New System.Drawing.Size(56, 23)
        Me.ExportToKML.TabIndex = 3
        Me.ExportToKML.Text = "OK"
        Me.ExportToKML.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Export a GEM database to Shapefile"
        '
        'ExportToShapefile
        '
        Me.ExportToShapefile.Location = New System.Drawing.Point(203, 68)
        Me.ExportToShapefile.Name = "ExportToShapefile"
        Me.ExportToShapefile.Size = New System.Drawing.Size(56, 23)
        Me.ExportToShapefile.TabIndex = 5
        Me.ExportToShapefile.Text = "OK"
        Me.ExportToShapefile.UseVisualStyleBackColor = True
        '
        'ExportToCSV
        '
        Me.ExportToCSV.Location = New System.Drawing.Point(203, 97)
        Me.ExportToCSV.Name = "ExportToCSV"
        Me.ExportToCSV.Size = New System.Drawing.Size(56, 23)
        Me.ExportToCSV.TabIndex = 6
        Me.ExportToCSV.Text = "OK"
        Me.ExportToCSV.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(177, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Export a GEM database to CSV files"
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(203, 126)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(56, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'frmDataManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 161)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ExportToCSV)
        Me.Controls.Add(Me.ExportToShapefile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ExportToKML)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SyncDatabases)
        Me.Name = "frmDataManagement"
        Me.Text = "GEM Data Management"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SyncDatabases As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExportToKML As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ExportToShapefile As System.Windows.Forms.Button
    Friend WithEvents ExportToCSV As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
End Class
