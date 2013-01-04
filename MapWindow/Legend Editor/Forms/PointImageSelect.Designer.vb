<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PointImageSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PointImageSelect))
        Me.flp1 = New System.Windows.Forms.FlowLayoutPanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.chbHidePoint = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'flp1
        '
        Me.flp1.AccessibleDescription = Nothing
        Me.flp1.AccessibleName = Nothing
        resources.ApplyResources(Me.flp1, "flp1")
        Me.flp1.BackgroundImage = Nothing
        Me.flp1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flp1.Font = Nothing
        Me.flp1.Name = "flp1"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.AccessibleDescription = Nothing
        Me.cmdBrowse.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdBrowse, "cmdBrowse")
        Me.cmdBrowse.BackgroundImage = Nothing
        Me.cmdBrowse.Font = Nothing
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.AccessibleDescription = Nothing
        Me.cmdCancel.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.BackgroundImage = Nothing
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chbHidePoint
        '
        Me.chbHidePoint.AccessibleDescription = Nothing
        Me.chbHidePoint.AccessibleName = Nothing
        resources.ApplyResources(Me.chbHidePoint, "chbHidePoint")
        Me.chbHidePoint.BackgroundImage = Nothing
        Me.chbHidePoint.Font = Nothing
        Me.chbHidePoint.Name = "chbHidePoint"
        Me.chbHidePoint.UseVisualStyleBackColor = True
        '
        'PointImageSelect
        '
        Me.AcceptButton = Me.cmdBrowse
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.chbHidePoint)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.flp1)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PointImageSelect"
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flp1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chbHidePoint As System.Windows.Forms.CheckBox
End Class
