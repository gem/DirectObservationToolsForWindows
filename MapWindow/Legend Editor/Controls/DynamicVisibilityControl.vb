Imports System.Windows.Forms.Design

Friend Class DynamicVisibilityControl
    Inherits System.Windows.Forms.UserControl

    Private m_Provider As IWindowsFormsEditorService
    Private m_handle As Integer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtScale As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button

    Public retval As Boolean

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal LayerHandle As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
        m_handle = LayerHandle
        If Not frmMain.m_AutoVis(m_handle) Is Nothing Then
            retval = frmMain.m_AutoVis(m_handle).UseDynamicExtents
            txtScale.Text = "1:" & Math.Round(frmMain.m_AutoVis(m_handle).DynamicScale).ToString()
        Else
            chkUseDynamicVisibility.Enabled = False
            txtScale.Text = "1:" & frmMain.GetCurrentScale()
        End If
        chkUseDynamicVisibility.Checked = retval
    End Sub

#Region " Windows Form Designer generated code "


    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents chkUseDynamicVisibility As System.Windows.Forms.CheckBox
    Friend WithEvents btnGrabExtents As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DynamicVisibilityControl))
        Me.chkUseDynamicVisibility = New System.Windows.Forms.CheckBox
        Me.btnGrabExtents = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtScale = New System.Windows.Forms.TextBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'chkUseDynamicVisibility
        '
        Me.chkUseDynamicVisibility.AccessibleDescription = Nothing
        Me.chkUseDynamicVisibility.AccessibleName = Nothing
        resources.ApplyResources(Me.chkUseDynamicVisibility, "chkUseDynamicVisibility")
        Me.chkUseDynamicVisibility.BackgroundImage = Nothing
        Me.chkUseDynamicVisibility.Font = Nothing
        Me.chkUseDynamicVisibility.Name = "chkUseDynamicVisibility"
        '
        'btnGrabExtents
        '
        Me.btnGrabExtents.AccessibleDescription = Nothing
        Me.btnGrabExtents.AccessibleName = Nothing
        resources.ApplyResources(Me.btnGrabExtents, "btnGrabExtents")
        Me.btnGrabExtents.BackColor = System.Drawing.SystemColors.Control
        Me.btnGrabExtents.BackgroundImage = Nothing
        Me.btnGrabExtents.Font = Nothing
        Me.btnGrabExtents.Name = "btnGrabExtents"
        Me.btnGrabExtents.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'txtScale
        '
        Me.txtScale.AccessibleDescription = Nothing
        Me.txtScale.AccessibleName = Nothing
        resources.ApplyResources(Me.txtScale, "txtScale")
        Me.txtScale.BackgroundImage = Nothing
        Me.txtScale.Font = Nothing
        Me.txtScale.Name = "txtScale"
        '
        'btnOk
        '
        Me.btnOk.AccessibleDescription = Nothing
        Me.btnOk.AccessibleName = Nothing
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.BackColor = System.Drawing.SystemColors.Control
        Me.btnOk.BackgroundImage = Nothing
        Me.btnOk.Font = Nothing
        Me.btnOk.Name = "btnOk"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'DynamicVisibilityControl
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtScale)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnGrabExtents)
        Me.Controls.Add(Me.chkUseDynamicVisibility)
        Me.Font = Nothing
        Me.Name = "DynamicVisibilityControl"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub chkUseDynamicVisibility_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseDynamicVisibility.CheckedChanged
        If frmMain.m_AutoVis(m_handle) Is Nothing Then
            frmMain.m_AutoVis.Add(m_handle, CType(frmMain.MapMain.Extents, MapWinGIS.Extents), chkUseDynamicVisibility.Checked)
        Else
            frmMain.m_AutoVis(m_handle).UseDynamicExtents = chkUseDynamicVisibility.Checked
        End If
        retval = chkUseDynamicVisibility.Checked
        'Paul Meems (3 aug 2009): Don't hide let the use see what is happening:
        'Me.Hide()
        'm_Provider.CloseDropDown()
        'Causes Bug 1451 - revert back to old code
        Me.Hide()
        m_Provider.CloseDropDown()
    End Sub

    Private Sub btnGrabExtents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabExtents.Click
        If frmMain.m_AutoVis(m_handle) Is Nothing Then
            frmMain.m_AutoVis.Add(m_handle, CType(frmMain.MapMain.Extents, MapWinGIS.Extents), True)
        Else
            frmMain.m_AutoVis(m_handle).DynamicExtents = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
        End If
        chkUseDynamicVisibility.Checked = True
        retval = True
        'Paul Meems (3 aug 2009): Don't hide let the use see what is happening:
        'Me.Hide()
        'm_Provider.CloseDropDown()
        'Causes Bug 1451 - revert back to old code
        Me.Hide()
        m_Provider.CloseDropDown()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If chkUseDynamicVisibility.Checked Then
            If Not IsNumeric(txtScale.Text.Replace("1:", "")) Then
                MsgBox("Please enter the scale in the form of 1:100000, or click Use Current to use the current zoom level.", MsgBoxStyle.Information, "Invalid Scale")
                Exit Sub
            End If

            Dim scaleValue As Double = Double.Parse(txtScale.Text.Replace("1:", ""))
            Dim ext As MapWinGIS.Extents = frmMain.MapMain.Extents 'Initial scale - exact location irrelevant
            If frmMain.m_AutoVis(m_handle) Is Nothing Then
                frmMain.m_AutoVis.Add(m_handle, frmMain.ScaleToExtents(scaleValue, ext), True)
            Else
                frmMain.m_AutoVis(m_handle).DynamicExtents = frmMain.ScaleToExtents(scaleValue, ext)
            End If
            'Paul Meems (3 aug 2009): Don't set it always to true
            'chkUseDynamicVisibility.Checked = True
            'Causes Bug 1451 - revert back to old code
            chkUseDynamicVisibility.Checked = True
            retval = True
        End If

        Me.Hide()
        m_Provider.CloseDropDown()
    End Sub
End Class
