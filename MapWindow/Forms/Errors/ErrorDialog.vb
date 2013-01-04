'********************************************************************************************************
'File Name: frmErrorDialog.vb
'Description: Dialog form to display errors to users and allow reporting to developer team.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'1/12/2005 - new entry point for MapWindow (dpa)
'10/19/2005 - Paul Meems - Starting with translating resourcefile into Dutch.
'10/19/2005 - dpa - modified the show details function.
'5/23/2006 - Chris Michaelis - Added the automatic exception mailer capability.
'8/1/2006 - pm - Translation of new strings into Dutch and renamed frmErrorDialog to ErrorDialog
'                because else the translation files cannot be found.
'28/1/2008 - Jiri Kadlec - changed ResourceManager (texts of messages moved to a separate resource file)
'********************************************************************************************************
Friend Class ErrorDialog
    Inherits System.Windows.Forms.Form

    Private destEmail As String = ""

#Region "Declarations"
    '1/28/2008 changed by Jiri Kadlec
    Private Shared resources As System.Resources.ResourceManager = _
    New System.Resources.ResourceManager("MapWindow.GlobalResource", System.Reflection.Assembly.GetExecutingAssembly())

    Private m_exception As Exception
    Friend WithEvents lblErr As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtEMail As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkNoReport As System.Windows.Forms.CheckBox

#End Region

    Public Sub New(ByVal ex As System.Exception, ByVal SendNextToEmail As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        m_exception = ex

        destEmail = SendNextToEmail
        MapWinUtility.Logger.Dbg("UNHANDLED EXCEPTION: " + ex.ToString())
    End Sub

#Region " Windows Form Designer generated code "
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
    Friend WithEvents lblAltLink As System.Windows.Forms.LinkLabel
    Friend WithEvents btnSend As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorDialog))
        Me.btnSend = New System.Windows.Forms.Button
        Me.lblAltLink = New System.Windows.Forms.LinkLabel
        Me.lblErr = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.lblComments = New System.Windows.Forms.Label
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.chkNoReport = New System.Windows.Forms.CheckBox
        Me.txtEMail = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSend
        '
        resources.ApplyResources(Me.btnSend, "btnSend")
        Me.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSend.Name = "btnSend"
        '
        'lblAltLink
        '
        resources.ApplyResources(Me.lblAltLink, "lblAltLink")
        Me.lblAltLink.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline
        Me.lblAltLink.Name = "lblAltLink"
        Me.lblAltLink.TabStop = True
        Me.lblAltLink.UseCompatibleTextRendering = True
        Me.lblAltLink.UseMnemonic = False
        '
        'lblErr
        '
        resources.ApplyResources(Me.lblErr, "lblErr")
        Me.lblErr.Name = "lblErr"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Name = "Button1"
        '
        'LinkLabel1
        '
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = True
        '
        'lblComments
        '
        resources.ApplyResources(Me.lblComments, "lblComments")
        Me.lblComments.Name = "lblComments"
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        resources.ApplyResources(Me.txtComments, "txtComments")
        Me.txtComments.Name = "txtComments"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.MapWindow.My.Resources.Resources.PeopleGlobe
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'chkNoReport
        '
        resources.ApplyResources(Me.chkNoReport, "chkNoReport")
        Me.chkNoReport.Name = "chkNoReport"
        Me.chkNoReport.UseVisualStyleBackColor = True
        '
        'txtEMail
        '
        resources.ApplyResources(Me.txtEMail, "txtEMail")
        Me.txtEMail.Name = "txtEMail"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'ErrorDialog
        '
        Me.AcceptButton = Me.btnSend
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.Button1
        Me.Controls.Add(Me.txtEMail)
        Me.Controls.Add(Me.chkNoReport)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.lblComments)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblErr)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.lblAltLink)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ErrorDialog"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub lblReportError_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblAltLink.LinkClicked
        Try
            System.Diagnostics.Process.Start("http://bugs.MapWindow.org")
        Catch
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim moreInfo As New frmErrorDialogMoreInfo()
        '8/1/2006 - pm
        'moreInfo.txtFullText.Text = "MapWindow " + App.VersionString + " (" + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString() + ")" + vbCrLf + vbCrLf + m_exception.ToString() + vbCrLf + vbCrLf + GetProcessInfo()
        moreInfo.txtFullText.Text = AppInfo.Name + " " + App.VersionString + " (" + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString() + ")" + vbCrLf + vbCrLf + m_exception.ToString() + vbCrLf + vbCrLf + MapWinUtility.MiscUtils.GetDebugInfo()
        moreInfo.Owner = Me
        moreInfo.Show()
        moreInfo.BringToFront()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            Cursor = Cursors.WaitCursor
            If Not m_exception Is Nothing Then
                If destEmail Is Nothing Then destEmail = ""
                Dim post As String = "prog=" + System.Web.HttpUtility.UrlEncode("MapWindow " + App.VersionString + " (" + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString() + ")") + "&ex=" + System.Web.HttpUtility.UrlEncode(m_exception.ToString()) + "&fromemail=" + System.Web.HttpUtility.UrlEncode(txtEMail.Text) + "&adtnl=" + System.Web.HttpUtility.UrlEncode(MapWinUtility.MiscUtils.GetDebugInfo()) + "&comments=" + System.Web.HttpUtility.UrlEncode(txtComments.Text) + "&copy=" + System.Web.HttpUtility.UrlEncode(destEmail)

                'Post string above has everything in needs -- 
                'MW version, exception text, and debug information. Also comments/email if applicable

                Try
                    MapWinUtility.Net.ExecuteUrl("http://www.MapWindow.org/AutoExceptionMail.php", post, True)
                Catch ex As Exception
                    MapWinUtility.Logger.Dbg("DEBUG: " + ex.ToString())
                End Try
            End If
        Catch
        Finally
            Cursor = Cursors.Default
        End Try

        Try
            Dim msg As String = resources.GetString("msgDataSubmitted.Text")
            If msg.Trim() = "" Then msg = "Thank you! The data has been submitted."
            MapWinUtility.Logger.Msg(msg, MsgBoxStyle.Information, AppInfo.Name)
        Catch
            'Give up on localization (resources object may be null in the case of a 
            'particularly bad crash)
            MapWinUtility.Logger.Msg("Thank you! The data has been submitted.", MsgBoxStyle.Information, "Thank you!")
        End Try
        Me.Close()
    End Sub

    Private Sub chkNoReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoReport.CheckedChanged
        ProjInfo.NoPromptToSendErrors = chkNoReport.Checked
        ProjInfo.SaveConfig()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
