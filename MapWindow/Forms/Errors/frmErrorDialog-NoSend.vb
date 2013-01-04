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
'8/1/2006 - pm - Translation of new strings into Dutch
'********************************************************************************************************
Friend Class ErrorDialogNoSend
    Inherits System.Windows.Forms.Form

    Private m_exception As Exception
    Friend WithEvents lblErr As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button

#Region "Declarations"
    'PM
    Private resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ErrorDialog))
#End Region

    Public Sub New(ByVal ex As System.Exception)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        m_exception = ex

        'TODO - It may be desirable to also write this exception to a log file
        'somewhere at some point, beyond the automatic logging that will take place
        'when (if?) the user hits "Send Data".
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorDialogNoSend))
        Me.btnSend = New System.Windows.Forms.Button()
        Me.lblAltLink = New System.Windows.Forms.LinkLabel()
        Me.lblErr = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
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
        'txtComments
        '
        resources.ApplyResources(Me.txtComments, "txtComments")
        Me.txtComments.Name = "txtComments"
        '
        'btnCopy
        '
        Me.btnCopy.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.btnCopy, "btnCopy")
        Me.btnCopy.Name = "btnCopy"
        '
        'ErrorDialogNoSend
        '
        Me.AcceptButton = Me.btnSend
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.btnSend
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.lblErr)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.lblAltLink)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ErrorDialogNoSend"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Me.Close()
    End Sub

    Private Sub lblReportError_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblAltLink.LinkClicked
        Try
            System.Diagnostics.Process.Start("http://bugs.MapWindow.org")
        Catch
        End Try
    End Sub

    Private Sub txtComments_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtComments.KeyDown
        e.SuppressKeyPress = True
    End Sub

    Private Sub ErrorDialogNoSend_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtComments.Text = AppInfo.Name + " " + App.VersionString + " (" + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString() + ")" + vbCrLf + vbCrLf + m_exception.ToString() + vbCrLf + vbCrLf + MapWinUtility.MiscUtils.GetDebugInfo()
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtComments.Text)
    End Sub
End Class
