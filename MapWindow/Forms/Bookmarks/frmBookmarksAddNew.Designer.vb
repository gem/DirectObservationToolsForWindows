<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBookmarksAddNew
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
        Me.ButtonOK = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxName = New System.Windows.Forms.TextBox
        Me.TextBoxYMax = New System.Windows.Forms.TextBox
        Me.TextBoxYMin = New System.Windows.Forms.TextBox
        Me.TextBoxXMin = New System.Windows.Forms.TextBox
        Me.TextBoxXMax = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.ButtonRevert = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonOK.Enabled = False
        Me.ButtonOK.Location = New System.Drawing.Point(287, 180)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 5
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(368, 180)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 6
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Bookmark Name"
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(104, 12)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxName.TabIndex = 0
        '
        'TextBoxYMax
        '
        Me.TextBoxYMax.Location = New System.Drawing.Point(149, 66)
        Me.TextBoxYMax.Name = "TextBoxYMax"
        Me.TextBoxYMax.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxYMax.TabIndex = 1
        '
        'TextBoxYMin
        '
        Me.TextBoxYMin.Location = New System.Drawing.Point(149, 118)
        Me.TextBoxYMin.Name = "TextBoxYMin"
        Me.TextBoxYMin.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxYMin.TabIndex = 12
        '
        'TextBoxXMin
        '
        Me.TextBoxXMin.Location = New System.Drawing.Point(68, 92)
        Me.TextBoxXMin.Name = "TextBoxXMin"
        Me.TextBoxXMin.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxXMin.TabIndex = 2
        '
        'TextBoxXMax
        '
        Me.TextBoxXMax.Location = New System.Drawing.Point(230, 92)
        Me.TextBoxXMax.Name = "TextBoxXMax"
        Me.TextBoxXMax.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxXMax.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(209, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Y Max"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(209, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Y Min"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(392, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "X Max"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "X Min"
        '
        'ButtonRevert
        '
        Me.ButtonRevert.Location = New System.Drawing.Point(15, 180)
        Me.ButtonRevert.Name = "ButtonRevert"
        Me.ButtonRevert.Size = New System.Drawing.Size(83, 23)
        Me.ButtonRevert.TabIndex = 7
        Me.ButtonRevert.Text = "Revert"
        Me.ButtonRevert.UseVisualStyleBackColor = True
        '
        'frmBookmarksAddNew
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(455, 215)
        Me.Controls.Add(Me.ButtonRevert)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxXMax)
        Me.Controls.Add(Me.TextBoxXMin)
        Me.Controls.Add(Me.TextBoxYMin)
        Me.Controls.Add(Me.TextBoxYMax)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmBookmarksAddNew"
        Me.Text = "Add New Bookmark"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxYMax As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxYMin As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxXMin As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxXMax As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ButtonRevert As System.Windows.Forms.Button
End Class
