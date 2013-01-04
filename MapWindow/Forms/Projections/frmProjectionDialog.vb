'Option Strict Off
''11/11/2005 - Paul Meems - Starting with translating resourcefile into Dutch.
''8/9/2006 - pm - Translation of new strings into Dutch
''1/28/2008 - Jiri Kadlec - Changed ResourceManager (message strings moved to GlobalResource.resx)

'Public Class frmProjectionDialog
'    Inherits System.Windows.Forms.Form
'#Region "Declarations"
'    'Changed by Jiri Kadlec
'    Private resources As System.Resources.ResourceManager = _
'    New System.Resources.ResourceManager("MapWindow.GlobalResource", System.Reflection.Assembly.GetExecutingAssembly())

'    Private projections As New clsProjections

'    Private defaultsFlag As Boolean
'    Private pOK As Boolean
'    Private projection_working As String
'#End Region

'#Region " Windows Form Designer generated code "

'    Public Sub New()
'        MyBase.New()

'        'This call is required by the Windows Form Designer.
'        InitializeComponent()

'        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
'        Me.Icon = My.Resources.MapWindow_new

'        'Add any initialization after the InitializeComponent() call
'        projection_working = ""
'    End Sub

'    'Form overrides dispose to clean up the component list.
'    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
'        If disposing Then
'            If Not (components Is Nothing) Then
'                components.Dispose()
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    'Required by the Windows Form Designer
'    Private components As System.ComponentModel.IContainer

'    'NOTE: The following procedure is required by the Windows Form Designer
'    'It can be modified using the Windows Form Designer.  
'    'Do not modify it using the code editor.
'    Friend WithEvents Panel1 As System.Windows.Forms.Panel
'    Friend WithEvents cboMainCateg As System.Windows.Forms.ComboBox
'    Friend WithEvents cboName As System.Windows.Forms.ComboBox
'    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
'    Friend WithEvents Label4 As System.Windows.Forms.Label
'    Friend WithEvents lblName As System.Windows.Forms.Label
'    Friend WithEvents btnOK As System.Windows.Forms.Button
'    Friend WithEvents lblCategory As System.Windows.Forms.Label
'    Private WithEvents lblCaption As System.Windows.Forms.Label
'    Friend WithEvents btnCancel As System.Windows.Forms.Button
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjectionDialog))
'        Me.Panel1 = New System.Windows.Forms.Panel
'        Me.btnCancel = New System.Windows.Forms.Button
'        Me.cboMainCateg = New System.Windows.Forms.ComboBox
'        Me.cboName = New System.Windows.Forms.ComboBox
'        Me.cboCategory = New System.Windows.Forms.ComboBox
'        Me.Label4 = New System.Windows.Forms.Label
'        Me.lblName = New System.Windows.Forms.Label
'        Me.btnOK = New System.Windows.Forms.Button
'        Me.lblCategory = New System.Windows.Forms.Label
'        Me.lblCaption = New System.Windows.Forms.Label
'        Me.Panel1.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'Panel1
'        '
'        Me.Panel1.AccessibleDescription = Nothing
'        Me.Panel1.AccessibleName = Nothing
'        resources.ApplyResources(Me.Panel1, "Panel1")
'        Me.Panel1.BackgroundImage = Nothing
'        Me.Panel1.Controls.Add(Me.btnCancel)
'        Me.Panel1.Controls.Add(Me.cboMainCateg)
'        Me.Panel1.Controls.Add(Me.cboName)
'        Me.Panel1.Controls.Add(Me.cboCategory)
'        Me.Panel1.Controls.Add(Me.Label4)
'        Me.Panel1.Controls.Add(Me.lblName)
'        Me.Panel1.Controls.Add(Me.btnOK)
'        Me.Panel1.Controls.Add(Me.lblCategory)
'        Me.Panel1.Font = Nothing
'        Me.Panel1.Name = "Panel1"
'        '
'        'btnCancel
'        '
'        Me.btnCancel.AccessibleDescription = Nothing
'        Me.btnCancel.AccessibleName = Nothing
'        resources.ApplyResources(Me.btnCancel, "btnCancel")
'        Me.btnCancel.BackgroundImage = Nothing
'        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
'        Me.btnCancel.Font = Nothing
'        Me.btnCancel.Name = "btnCancel"
'        '
'        'cboMainCateg
'        '
'        Me.cboMainCateg.AccessibleDescription = Nothing
'        Me.cboMainCateg.AccessibleName = Nothing
'        resources.ApplyResources(Me.cboMainCateg, "cboMainCateg")
'        Me.cboMainCateg.BackgroundImage = Nothing
'        Me.cboMainCateg.Font = Nothing
'        Me.cboMainCateg.Name = "cboMainCateg"
'        Me.cboMainCateg.Sorted = True
'        '
'        'cboName
'        '
'        Me.cboName.AccessibleDescription = Nothing
'        Me.cboName.AccessibleName = Nothing
'        resources.ApplyResources(Me.cboName, "cboName")
'        Me.cboName.BackgroundImage = Nothing
'        Me.cboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cboName.Font = Nothing
'        Me.cboName.Name = "cboName"
'        Me.cboName.Sorted = True
'        '
'        'cboCategory
'        '
'        Me.cboCategory.AccessibleDescription = Nothing
'        Me.cboCategory.AccessibleName = Nothing
'        resources.ApplyResources(Me.cboCategory, "cboCategory")
'        Me.cboCategory.BackgroundImage = Nothing
'        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cboCategory.Font = Nothing
'        Me.cboCategory.Name = "cboCategory"
'        Me.cboCategory.Sorted = True
'        '
'        'Label4
'        '
'        Me.Label4.AccessibleDescription = Nothing
'        Me.Label4.AccessibleName = Nothing
'        resources.ApplyResources(Me.Label4, "Label4")
'        Me.Label4.Font = Nothing
'        Me.Label4.Name = "Label4"
'        '
'        'lblName
'        '
'        Me.lblName.AccessibleDescription = Nothing
'        Me.lblName.AccessibleName = Nothing
'        resources.ApplyResources(Me.lblName, "lblName")
'        Me.lblName.Font = Nothing
'        Me.lblName.Name = "lblName"
'        '
'        'btnOK
'        '
'        Me.btnOK.AccessibleDescription = Nothing
'        Me.btnOK.AccessibleName = Nothing
'        resources.ApplyResources(Me.btnOK, "btnOK")
'        Me.btnOK.BackgroundImage = Nothing
'        Me.btnOK.Font = Nothing
'        Me.btnOK.Name = "btnOK"
'        '
'        'lblCategory
'        '
'        Me.lblCategory.AccessibleDescription = Nothing
'        Me.lblCategory.AccessibleName = Nothing
'        resources.ApplyResources(Me.lblCategory, "lblCategory")
'        Me.lblCategory.Font = Nothing
'        Me.lblCategory.Name = "lblCategory"
'        '
'        'lblCaption
'        '
'        Me.lblCaption.AccessibleDescription = Nothing
'        Me.lblCaption.AccessibleName = Nothing
'        resources.ApplyResources(Me.lblCaption, "lblCaption")
'        Me.lblCaption.Font = Nothing
'        Me.lblCaption.Name = "lblCaption"
'        '
'        'frmProjectionDialog
'        '
'        Me.AcceptButton = Me.btnOK
'        Me.AccessibleDescription = Nothing
'        Me.AccessibleName = Nothing
'        resources.ApplyResources(Me, "$this")
'        Me.BackgroundImage = Nothing
'        Me.CancelButton = Me.btnCancel
'        Me.Controls.Add(Me.Panel1)
'        Me.Controls.Add(Me.lblCaption)
'        Me.Font = Nothing
'        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
'        Me.Icon = Nothing
'        Me.Name = "frmProjectionDialog"
'        Me.Panel1.ResumeLayout(False)
'        Me.ResumeLayout(False)

'    End Sub

'#End Region

'    Public Sub SetProjection(ByVal inproj As String)
'        projection_working = inproj
'        If Not inproj = "" Then
'            Dim projection As clsProjections.clsProjection = projections.FindProjectionByPROJ4(inproj)
'            If Not projection Is Nothing Then
'                cboMainCateg.Text = projection.MainCateg
'                cboCategory.Text = projection.Category
'                cboName.Text = projection.Name
'            End If
'        End If
'    End Sub

'    Public Function GetProjection() As String
'        Return projection_working
'    End Function

'    Protected Overrides Sub Finalize()
'        MyBase.Finalize()
'    End Sub

'    Private Sub frmProjectionDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        Dim q As Windows.Forms.Cursor = Me.Cursor
'        Me.Cursor = Windows.Forms.Cursors.WaitCursor

'        'Load the projections combo box
'        projections.LoadMainCategComboBox(cboMainCateg)

'        'If there's a project projection, load it
'        If Not projection_working = "" Then
'            Dim p As clsProjections.clsProjection = projections.FindProjectionByPROJ4(projection_working)
'            'PM Added:
'            Dim userDefined As String = resources.GetString("UserDefined.Text")
'            Dim customProjection As String = resources.GetString("CustomProjection.Text")
'            'PM Changed
'            'If p.Category = "Custom Projection" And p.MainCateg = "Custom Projection" And p.Name = "Custom Projection" Then
'            '    cboMainCateg.Items.Add("Custom Projection")
'            '    cboCategory.Items.Add("Custom Projection")
'            '    cboName.Items.Add("Custom Projection")
'            'End If
'            If p.Category = userDefined And p.MainCateg = userDefined And p.Name = customProjection Then
'                cboMainCateg.Items.Add(userDefined)
'                cboCategory.Items.Add(userDefined)
'                cboName.Items.Add(customProjection)
'            End If

'            cboMainCateg.Text = p.MainCateg
'            cboCategory.Text = p.Category
'            cboName.Text = p.Name
'        End If

'        Me.Cursor = q
'    End Sub

'    Private Sub cboMainCateg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMainCateg.SelectedIndexChanged
'        projections.LoadCategComboBox(cboMainCateg.Text, cboCategory)
'    End Sub

'    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
'        projections.LoadNamesComboBox(cboMainCateg.Text, cboCategory.Text, cboName)
'    End Sub

'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
'        Me.DialogResult = Windows.Forms.DialogResult.Cancel
'        Me.Hide()
'    End Sub

'    Public Sub SetCaptionText(ByVal captionText As String)
'        'Adjust the height of the form and the position of the panel
'        'depending on length of text passed into the form
'        lblCaption.Text = captionText

'        Dim layoutsize As SizeF = New SizeF(lblCaption.Width, 5000.0)
'        Dim g As Graphics = Graphics.FromHwnd(lblCaption.Handle)
'        Dim StringSize As SizeF = g.MeasureString(lblCaption.Text, lblCaption.Font, layoutsize)
'        g.Dispose()

'        lblCaption.Height = StringSize.Height
'        Panel1.Location = New Point(Panel1.Location.X, Panel1.Location.Y + lblCaption.Height - 20)
'        Me.Height += lblCaption.Height - 20
'    End Sub

'    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
'        projection_working = projections.FindProjectionByCatAndName(cboMainCateg.Text, cboCategory.Text, cboName.Text)
'        Me.DialogResult = Windows.Forms.DialogResult.OK
'        Me.Hide()
'    End Sub
'End Class