Friend Class frmSchemeAttributes
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cmbSchemeType As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents cmbField1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblNumBreaks As System.Windows.Forms.Label
    Friend WithEvents udNumBreaks1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPrecision1 As System.Windows.Forms.Label
    Friend WithEvents udPrecision As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblColorBy1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSchemeType As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlContinuousRamp As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSchemeAttributes))
        Me.pnlContinuousRamp = New System.Windows.Forms.Panel
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.lblColorBy1 = New System.Windows.Forms.Label
        Me.udPrecision = New System.Windows.Forms.NumericUpDown
        Me.lblPrecision1 = New System.Windows.Forms.Label
        Me.udNumBreaks1 = New System.Windows.Forms.NumericUpDown
        Me.lblNumBreaks = New System.Windows.Forms.Label
        Me.cmbField1 = New System.Windows.Forms.ComboBox
        Me.cmbSchemeType = New System.Windows.Forms.ComboBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblSchemeType = New System.Windows.Forms.Label
        Me.pnlContinuousRamp.SuspendLayout()
        CType(Me.udPrecision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udNumBreaks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlContinuousRamp
        '
        Me.pnlContinuousRamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlContinuousRamp.Controls.Add(Me.ComboBox2)
        Me.pnlContinuousRamp.Controls.Add(Me.Label2)
        Me.pnlContinuousRamp.Controls.Add(Me.Label1)
        Me.pnlContinuousRamp.Controls.Add(Me.ComboBox1)
        Me.pnlContinuousRamp.Controls.Add(Me.lblColorBy1)
        Me.pnlContinuousRamp.Controls.Add(Me.udPrecision)
        Me.pnlContinuousRamp.Controls.Add(Me.lblPrecision1)
        Me.pnlContinuousRamp.Controls.Add(Me.udNumBreaks1)
        Me.pnlContinuousRamp.Controls.Add(Me.lblNumBreaks)
        Me.pnlContinuousRamp.Controls.Add(Me.cmbField1)
        resources.ApplyResources(Me.pnlContinuousRamp, "pnlContinuousRamp")
        Me.pnlContinuousRamp.Name = "pnlContinuousRamp"
        '
        'ComboBox2
        '
        resources.ApplyResources(Me.ComboBox2, "ComboBox2")
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Name = "ComboBox2"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'ComboBox1
        '
        resources.ApplyResources(Me.ComboBox1, "ComboBox1")
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Name = "ComboBox1"
        '
        'lblColorBy1
        '
        resources.ApplyResources(Me.lblColorBy1, "lblColorBy1")
        Me.lblColorBy1.Name = "lblColorBy1"
        '
        'udPrecision
        '
        resources.ApplyResources(Me.udPrecision, "udPrecision")
        Me.udPrecision.Name = "udPrecision"
        '
        'lblPrecision1
        '
        resources.ApplyResources(Me.lblPrecision1, "lblPrecision1")
        Me.lblPrecision1.Name = "lblPrecision1"
        '
        'udNumBreaks1
        '
        resources.ApplyResources(Me.udNumBreaks1, "udNumBreaks1")
        Me.udNumBreaks1.Name = "udNumBreaks1"
        '
        'lblNumBreaks
        '
        resources.ApplyResources(Me.lblNumBreaks, "lblNumBreaks")
        Me.lblNumBreaks.Name = "lblNumBreaks"
        '
        'cmbField1
        '
        resources.ApplyResources(Me.cmbField1, "cmbField1")
        Me.cmbField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbField1.Name = "cmbField1"
        '
        'cmbSchemeType
        '
        resources.ApplyResources(Me.cmbSchemeType, "cmbSchemeType")
        Me.cmbSchemeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSchemeType.Items.AddRange(New Object() {resources.GetString("cmbSchemeType.Items"), resources.GetString("cmbSchemeType.Items1"), resources.GetString("cmbSchemeType.Items2")})
        Me.cmbSchemeType.Name = "cmbSchemeType"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.Name = "btnOK"
        '
        'lblSchemeType
        '
        resources.ApplyResources(Me.lblSchemeType, "lblSchemeType")
        Me.lblSchemeType.Name = "lblSchemeType"
        '
        'frmSchemeAttributes
        '
        Me.AcceptButton = Me.btnOK
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.lblSchemeType)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.cmbSchemeType)
        Me.Controls.Add(Me.pnlContinuousRamp)
        Me.Name = "frmSchemeAttributes"
        Me.pnlContinuousRamp.ResumeLayout(False)
        CType(Me.udPrecision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udNumBreaks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


End Class
