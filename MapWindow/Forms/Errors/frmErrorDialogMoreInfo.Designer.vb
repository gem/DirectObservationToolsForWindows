<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErrorDialogMoreInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrorDialogMoreInfo))
        Me.lblErr = New System.Windows.Forms.Label
        Me.txtFullText = New System.Windows.Forms.TextBox
        Me.btnCopy = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblErr
        '
        Me.lblErr.AccessibleDescription = Nothing
        Me.lblErr.AccessibleName = Nothing
        resources.ApplyResources(Me.lblErr, "lblErr")
        Me.lblErr.Font = Nothing
        Me.lblErr.Name = "lblErr"
        '
        'txtFullText
        '
        Me.txtFullText.AccessibleDescription = Nothing
        Me.txtFullText.AccessibleName = Nothing
        resources.ApplyResources(Me.txtFullText, "txtFullText")
        Me.txtFullText.BackgroundImage = Nothing
        Me.txtFullText.Font = Nothing
        Me.txtFullText.Name = "txtFullText"
        '
        'btnCopy
        '
        Me.btnCopy.AccessibleDescription = Nothing
        Me.btnCopy.AccessibleName = Nothing
        resources.ApplyResources(Me.btnCopy, "btnCopy")
        Me.btnCopy.BackgroundImage = Nothing
        Me.btnCopy.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnCopy.Font = Nothing
        Me.btnCopy.Name = "btnCopy"
        '
        'btnSend
        '
        Me.btnSend.AccessibleDescription = Nothing
        Me.btnSend.AccessibleName = Nothing
        resources.ApplyResources(Me.btnSend, "btnSend")
        Me.btnSend.BackgroundImage = Nothing
        Me.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSend.Font = Nothing
        Me.btnSend.Name = "btnSend"
        '
        'frmErrorDialogMoreInfo
        '
        Me.AcceptButton = Me.btnSend
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnSend
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtFullText)
        Me.Controls.Add(Me.lblErr)
        Me.Font = Nothing
        Me.Icon = Nothing
        Me.Name = "frmErrorDialogMoreInfo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblErr As System.Windows.Forms.Label
    Friend WithEvents txtFullText As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new
    End Sub
End Class
