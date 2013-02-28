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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSync))
        Me.TargetDatabase = New System.Windows.Forms.TextBox()
        Me.SelectFiles = New System.Windows.Forms.Button()
        Me.TargetBrowse = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Ok = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SourceDatabases = New System.Windows.Forms.CheckedListBox()
        Me.SelectFolder = New System.Windows.Forms.Button()
        Me.ClearList = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SourceProject = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TargetDatabase
        '
        Me.TargetDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TargetDatabase.Location = New System.Drawing.Point(80, 300)
        Me.TargetDatabase.Name = "TargetDatabase"
        Me.TargetDatabase.Size = New System.Drawing.Size(444, 22)
        Me.TargetDatabase.TabIndex = 1
        '
        'SelectFiles
        '
        Me.SelectFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelectFiles.Location = New System.Drawing.Point(530, 73)
        Me.SelectFiles.Name = "SelectFiles"
        Me.SelectFiles.Size = New System.Drawing.Size(109, 24)
        Me.SelectFiles.TabIndex = 4
        Me.SelectFiles.Text = "Select Files"
        Me.SelectFiles.UseVisualStyleBackColor = True
        '
        'TargetBrowse
        '
        Me.TargetBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TargetBrowse.Location = New System.Drawing.Point(530, 300)
        Me.TargetBrowse.Name = "TargetBrowse"
        Me.TargetBrowse.Size = New System.Drawing.Size(109, 24)
        Me.TargetBrowse.TabIndex = 5
        Me.TargetBrowse.Text = "Browse"
        Me.TargetBrowse.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.Location = New System.Drawing.Point(415, 350)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(109, 24)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Ok
        '
        Me.Ok.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ok.Location = New System.Drawing.Point(530, 350)
        Me.Ok.Name = "Ok"
        Me.Ok.Size = New System.Drawing.Size(109, 24)
        Me.Ok.TabIndex = 7
        Me.Ok.Text = "Merge"
        Me.Ok.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(20, 22)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(23, 274)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox4.TabIndex = 21
        Me.PictureBox4.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(77, 274)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(182, 16)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Target GEM Database (new):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(77, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Source GEM Databases:"
        '
        'SourceDatabases
        '
        Me.SourceDatabases.CheckOnClick = True
        Me.SourceDatabases.FormattingEnabled = True
        Me.SourceDatabases.Location = New System.Drawing.Point(84, 49)
        Me.SourceDatabases.Name = "SourceDatabases"
        Me.SourceDatabases.Size = New System.Drawing.Size(440, 124)
        Me.SourceDatabases.TabIndex = 23
        '
        'SelectFolder
        '
        Me.SelectFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelectFolder.Location = New System.Drawing.Point(530, 103)
        Me.SelectFolder.Name = "SelectFolder"
        Me.SelectFolder.Size = New System.Drawing.Size(109, 24)
        Me.SelectFolder.TabIndex = 24
        Me.SelectFolder.Text = "Select Folder"
        Me.SelectFolder.UseVisualStyleBackColor = True
        '
        'ClearList
        '
        Me.ClearList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearList.Location = New System.Drawing.Point(530, 133)
        Me.ClearList.Name = "ClearList"
        Me.ClearList.Size = New System.Drawing.Size(109, 24)
        Me.ClearList.TabIndex = 25
        Me.ClearList.Text = "Clear List"
        Me.ClearList.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(23, 198)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox2.TabIndex = 28
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(77, 198)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 16)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Source GEM Project:"
        '
        'SourceProject
        '
        Me.SourceProject.FormattingEnabled = True
        Me.SourceProject.Location = New System.Drawing.Point(80, 225)
        Me.SourceProject.Name = "SourceProject"
        Me.SourceProject.Size = New System.Drawing.Size(444, 21)
        Me.SourceProject.TabIndex = 29
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 399)
        Me.Controls.Add(Me.SourceProject)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ClearList)
        Me.Controls.Add(Me.SelectFolder)
        Me.Controls.Add(Me.SourceDatabases)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Ok)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.TargetBrowse)
        Me.Controls.Add(Me.SelectFiles)
        Me.Controls.Add(Me.TargetDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Merge GEM Databases"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TargetDatabase As System.Windows.Forms.TextBox
    Friend WithEvents SelectFiles As System.Windows.Forms.Button
    Friend WithEvents TargetBrowse As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Ok As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SourceDatabases As System.Windows.Forms.CheckedListBox
    Friend WithEvents SelectFolder As System.Windows.Forms.Button
    Friend WithEvents ClearList As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SourceProject As System.Windows.Forms.ComboBox
End Class
