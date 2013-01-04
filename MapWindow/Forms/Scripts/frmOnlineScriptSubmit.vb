'8/2/2006 - Paul Meems (pm) - Started Dutch translation
'1/28/2008 - Jiri Kadlec (jk) - Changed ResourceManager (message strings moved to a new resource file)
Public Class frmOnlineScriptSubmit
    Inherits System.Windows.Forms.Form

#Region "Declarations"
    'jk
    Private resources As System.Resources.ResourceManager = _
       New System.Resources.ResourceManager("MapWindow.GlobalResource", System.Reflection.Assembly.GetExecutingAssembly())
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents txtSubmitter As System.Windows.Forms.TextBox
    Friend WithEvents txtLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents txtScript As System.Windows.Forms.RichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnlineScriptSubmit))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSubmitter = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.txtLanguage = New System.Windows.Forms.ComboBox
        Me.txtScript = New System.Windows.Forms.RichTextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        Me.Label2.AccessibleDescription = Nothing
        Me.Label2.AccessibleName = Nothing
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Font = Nothing
        Me.Label2.Name = "Label2"
        '
        'Label4
        '
        Me.Label4.AccessibleDescription = Nothing
        Me.Label4.AccessibleName = Nothing
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Font = Nothing
        Me.Label4.Name = "Label4"
        '
        'txtDesc
        '
        Me.txtDesc.AccessibleDescription = Nothing
        Me.txtDesc.AccessibleName = Nothing
        resources.ApplyResources(Me.txtDesc, "txtDesc")
        Me.txtDesc.BackgroundImage = Nothing
        Me.txtDesc.Font = Nothing
        Me.txtDesc.Name = "txtDesc"
        '
        'Label3
        '
        Me.Label3.AccessibleDescription = Nothing
        Me.Label3.AccessibleName = Nothing
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Font = Nothing
        Me.Label3.Name = "Label3"
        '
        'txtName
        '
        Me.txtName.AccessibleDescription = Nothing
        Me.txtName.AccessibleName = Nothing
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.BackgroundImage = Nothing
        Me.txtName.Font = Nothing
        Me.txtName.Name = "txtName"
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = Nothing
        Me.Label5.AccessibleName = Nothing
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Font = Nothing
        Me.Label5.Name = "Label5"
        '
        'txtSubmitter
        '
        Me.txtSubmitter.AccessibleDescription = Nothing
        Me.txtSubmitter.AccessibleName = Nothing
        resources.ApplyResources(Me.txtSubmitter, "txtSubmitter")
        Me.txtSubmitter.BackgroundImage = Nothing
        Me.txtSubmitter.Font = Nothing
        Me.txtSubmitter.Name = "txtSubmitter"
        '
        'Label6
        '
        Me.Label6.AccessibleDescription = Nothing
        Me.Label6.AccessibleName = Nothing
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Font = Nothing
        Me.Label6.Name = "Label6"
        '
        'Label8
        '
        Me.Label8.AccessibleDescription = Nothing
        Me.Label8.AccessibleName = Nothing
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Font = Nothing
        Me.Label8.Name = "Label8"
        '
        'Label9
        '
        Me.Label9.AccessibleDescription = Nothing
        Me.Label9.AccessibleName = Nothing
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Font = Nothing
        Me.Label9.Name = "Label9"
        '
        'btnClose
        '
        Me.btnClose.AccessibleDescription = Nothing
        Me.btnClose.AccessibleName = Nothing
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.BackgroundImage = Nothing
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = Nothing
        Me.btnClose.Name = "btnClose"
        '
        'btnSubmit
        '
        Me.btnSubmit.AccessibleDescription = Nothing
        Me.btnSubmit.AccessibleName = Nothing
        resources.ApplyResources(Me.btnSubmit, "btnSubmit")
        Me.btnSubmit.BackgroundImage = Nothing
        Me.btnSubmit.Font = Nothing
        Me.btnSubmit.Name = "btnSubmit"
        '
        'txtLanguage
        '
        Me.txtLanguage.AccessibleDescription = Nothing
        Me.txtLanguage.AccessibleName = Nothing
        resources.ApplyResources(Me.txtLanguage, "txtLanguage")
        Me.txtLanguage.BackgroundImage = Nothing
        Me.txtLanguage.Font = Nothing
        Me.txtLanguage.Items.AddRange(New Object() {resources.GetString("txtLanguage.Items"), resources.GetString("txtLanguage.Items1")})
        Me.txtLanguage.Name = "txtLanguage"
        '
        'txtScript
        '
        Me.txtScript.AccessibleDescription = Nothing
        Me.txtScript.AccessibleName = Nothing
        resources.ApplyResources(Me.txtScript, "txtScript")
        Me.txtScript.BackgroundImage = Nothing
        Me.txtScript.Font = Nothing
        Me.txtScript.Name = "txtScript"
        '
        'Button1
        '
        Me.Button1.AccessibleDescription = Nothing
        Me.Button1.AccessibleName = Nothing
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackgroundImage = Nothing
        Me.Button1.Font = Nothing
        Me.Button1.Name = "Button1"
        '
        'frmOnlineScriptSubmit
        '
        Me.AcceptButton = Me.btnSubmit
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnClose
        Me.Controls.Add(Me.txtScript)
        Me.Controls.Add(Me.txtLanguage)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtSubmitter)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnlineScriptSubmit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://www.MapWindow.org/team.php?action=Add")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cdOpen As New Windows.Forms.OpenFileDialog
        cdOpen.Filter = "VB.net (*.vb)|*.vb|C#.net (*.cs)|*.cs|All files|*.*"
        If cdOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtScript.Text = MapWinUtility.Strings.WholeFileString(cdOpen.FileName)

            If cdOpen.FileName.ToLower.EndsWith(".vb") Then
                txtLanguage.Text = "VB.Net"
            Else
                txtLanguage.Text = "C#"
            End If
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not txtLanguage.Text = "" And Not txtSubmitter.Text = "" And Not txtSubmitter.Text = "" And Not txtName.Text = "" And Not txtDesc.Text = "" And Not txtScript.Text = "" Then
            'Try to submit it.
            'Don't submit it encoded
            'Dim encData_byte As Byte()
            'encData_byte = System.Text.Encoding.UTF8.GetBytes(txtScript.Text)
            'Dim encbody As String = Convert.ToBase64String(encData_byte)
            Dim urlString As String = "http://MapWindow.org/scriptdirectory.php?"
            Dim queryString As String = "author=" + System.Web.HttpUtility.UrlEncode(txtSubmitter.Text) + "&scriptname=" + System.Web.HttpUtility.UrlEncode(txtName.Text) + "&scriptdesc=" + System.Web.HttpUtility.UrlEncode(txtDesc.Text) + "&scriptlang=" + System.Web.HttpUtility.UrlEncode(txtLanguage.Text) + "&scriptbody=" + System.Web.HttpUtility.UrlEncode(txtScript.Text) + "&MWSubmit=MWSubmit"

            Dim results As String = MapWinUtility.Net.ExecuteUrl(urlString, queryString)

            If results.Trim() = "INVALID_USERNAME_OR_PASS" Then
                'pm
                'mapwinutility.logger.msg("The submission failed -- the username or password is invalid. Please double-check it.", MsgBoxStyle.Exclamation, "Bad Username or Password")
                mapwinutility.logger.msg(resources.GetString("msgBadUsernamePassword.Text"), MsgBoxStyle.Exclamation, AppInfo.Name)
            ElseIf results.Trim() = "OK_SUCCESS" Then
                'pm
                'mapwinutility.logger.msg("The submission succeeded. Thank you!", MsgBoxStyle.Information, "Submitted Successfully")
                mapwinutility.logger.msg(resources.GetString("msgSubmittedSuccessfully.Text"), MsgBoxStyle.Information, AppInfo.Name)
                Me.Close()
            Else
                'pm
                'mapwinutility.logger.msg("The response from the server was not understood. Please try submitting again.", MsgBoxStyle.Exclamation, "Garbled Server Response")
                mapwinutility.logger.msg(resources.GetString("msgGarbledServerResponse.Text"), MsgBoxStyle.Exclamation, AppInfo.Name)
            End If
        Else
            'pm
            'mapwinutility.logger.msg("All fields are required; please make sure to enter something in every field.", MsgBoxStyle.Exclamation, "Incomplete")
            MapWinUtility.Logger.Msg(resources.GetString("msgIncomplete.Text"), MsgBoxStyle.Exclamation, AppInfo.Name)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub frmOnlineScriptSubmit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    txtScript.Text = Scripts.txtScript.Text
    'End Sub
End Class
