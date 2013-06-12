'********************************************************************************************************
'File Name: frmWelcomeScreen.vb
'Description: MapWindow Welcome Screen
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
'2/3/2005 - made spacing of text relative if no recent projexts, added TODOs (jlk)
'8/9/2006 - Paul Meems (pm) - Started Duth translation
'1/28/2008 - Jiri Kadlec - changed ResourceManager (message strings moved to GlobalResource.resx)
'10/1/2008 - Earljon Hidalgo (ejh) Modify icons modern look. Icons provided by famfamfam
'********************************************************************************************************
Public Class frmWelcomeScreen
    Inherits System.Windows.Forms.Form

#Region "Declarations"
    'changed by Jiri Kadlec
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
        'Me.Icon = My.Resources.MapWindow_new
        ' Paul Meems, The above does not seem to work.
        ' This does:
        Me.Icon = frmMain.Icon
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
    Friend WithEvents lbAddData As System.Windows.Forms.LinkLabel
    Friend WithEvents lbOpenProject As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents lbProject1 As System.Windows.Forms.LinkLabel
    Friend WithEvents lbProject2 As System.Windows.Forms.LinkLabel
    Friend WithEvents lbProject3 As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents lbHelpFile As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWelcomeScreen))
        Me.lbAddData = New System.Windows.Forms.LinkLabel()
        Me.lbOpenProject = New System.Windows.Forms.LinkLabel()
        Me.lbProject1 = New System.Windows.Forms.LinkLabel()
        Me.lbProject2 = New System.Windows.Forms.LinkLabel()
        Me.lbProject3 = New System.Windows.Forms.LinkLabel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.lbHelpFile = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbAddData
        '
        resources.ApplyResources(Me.lbAddData, "lbAddData")
        Me.lbAddData.Name = "lbAddData"
        Me.lbAddData.TabStop = True
        Me.lbAddData.UseCompatibleTextRendering = True
        '
        'lbOpenProject
        '
        resources.ApplyResources(Me.lbOpenProject, "lbOpenProject")
        Me.lbOpenProject.Name = "lbOpenProject"
        Me.lbOpenProject.TabStop = True
        Me.lbOpenProject.UseCompatibleTextRendering = True
        '
        'lbProject1
        '
        resources.ApplyResources(Me.lbProject1, "lbProject1")
        Me.lbProject1.Name = "lbProject1"
        Me.lbProject1.TabStop = True
        '
        'lbProject2
        '
        resources.ApplyResources(Me.lbProject2, "lbProject2")
        Me.lbProject2.Name = "lbProject2"
        Me.lbProject2.TabStop = True
        '
        'lbProject3
        '
        resources.ApplyResources(Me.lbProject3, "lbProject3")
        Me.lbProject3.Name = "lbProject3"
        Me.lbProject3.TabStop = True
        '
        'PictureBox4
        '
        resources.ApplyResources(Me.PictureBox4, "PictureBox4")
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.MapWindow.GlobalResource.open
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.MapWindow.GlobalResource.layer_add
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'PictureBox6
        '
        resources.ApplyResources(Me.PictureBox6, "PictureBox6")
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.TabStop = False
        '
        'lbHelpFile
        '
        resources.ApplyResources(Me.lbHelpFile, "lbHelpFile")
        Me.lbHelpFile.Name = "lbHelpFile"
        Me.lbHelpFile.TabStop = True
        Me.lbHelpFile.UseCompatibleTextRendering = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'frmWelcomeScreen
        '
        resources.ApplyResources(Me, "$this")
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.lbProject3)
        Me.Controls.Add(Me.lbProject2)
        Me.Controls.Add(Me.lbProject1)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lbHelpFile)
        Me.Controls.Add(Me.lbOpenProject)
        Me.Controls.Add(Me.lbAddData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWelcomeScreen"
        Me.ShowInTaskbar = False
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub lbAddData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbAddData.LinkClicked

        frmMain.DoNew()

        Me.DialogResult = DialogResult.OK
        Me.Close()

        'If (Not frmMain.m_layers.Add() Is Nothing) Then
        '    Me.DialogResult = DialogResult.OK
        '    Me.Close()
        'End If
    End Sub

    Private Sub lbOpenProject_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbOpenProject.LinkClicked
        frmMain.DoOpen()
        Me.Close()
    End Sub



    Private Sub lbHelpFile_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbHelpFile.LinkClicked

        System.Diagnostics.Process.Start(App.Path & "\LocalResources\MapWindow486Help.pdf")
    End Sub


    Private Sub openLinkedProject(ByVal recentProjectID As Short)
        Dim fileName As String = CType(ProjInfo.RecentProjects(recentProjectID), String)
        If (System.IO.File.Exists(fileName)) Then
            frmMain.DoOpen(fileName)
            Me.Close()
        Else
            'TODO - 2/3/2005 - jlk - need a findFile here 
            'pm
            'mapwinutility.logger.msg("Could not find " & fileName, MsgBoxStyle.Exclamation)
            Dim sMsg As String = String.Format(resources.GetString("msgFileNotFound.Text"), fileName)
            MapWinUtility.Logger.Msg(sMsg, MsgBoxStyle.Exclamation)
        End If

    End Sub
    Private Sub lbProject1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbProject1.LinkClicked
        openLinkedProject(0)
    End Sub

    Private Sub lbProject2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbProject2.LinkClicked
        openLinkedProject(1)
    End Sub

    Private Sub lbProject3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbProject3.LinkClicked
        openLinkedProject(2)
    End Sub

    Private Sub frmWelcomeScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' gemdb = New GEMDatabase("c:\test.db3")
        ' Dim detailsForm As New frmDetails(1, 2)
        '  detailsForm.ShowDialog()


        'check to see if there were any recent projects
        If (ProjInfo.RecentProjects.Count = 1) Then
            lbProject1.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(0), String))
            lbProject2.Visible = False
            lbProject3.Visible = False
            ' Fix for Bug #2041 Me.Height = Me.Height - (2 * (lbProject2.Top - lbProject1.Top))
        ElseIf (ProjInfo.RecentProjects.Count = 2) Then
            lbProject1.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(0), String))
            lbProject2.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(1), String))
            lbProject3.Visible = False
            ' Fix for Bug #2041 Me.Height = Me.Height - (lbProject2.Top - lbProject1.Top)
        ElseIf (ProjInfo.RecentProjects.Count >= 3) Then
            lbProject1.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(0), String))
            lbProject2.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(1), String))
            lbProject3.Text = System.IO.Path.GetFileName(CType(ProjInfo.RecentProjects(2), String))
        Else
            lbProject1.Visible = False
            lbProject2.Visible = False
            lbProject3.Visible = False
            ' Fix for Bug #2041 Me.Height = Me.Height - (3 * (lbProject2.Top - lbProject1.Top))
        End If

        'Chris M May 2006 - This makes the underline portion extend to the end of the line.
        If lbProject1.Visible Then lbProject1.LinkArea = New LinkArea(0, lbProject1.Text.Length)
        If lbProject2.Visible Then lbProject2.LinkArea = New LinkArea(0, lbProject2.Text.Length)
        If lbProject3.Visible Then lbProject3.LinkArea = New LinkArea(0, lbProject3.Text.Length)
        'PM August 2006 - After translation the size of the linkarea might be changed so create them on run-time

        lbAddData.LinkArea = New LinkArea(0, lbAddData.Text.Length)
        lbOpenProject.LinkArea = New LinkArea(0, lbOpenProject.Text.Length)
        lbHelpFile.LinkArea = New LinkArea(0, lbHelpFile.Text.Length)

    End Sub


End Class
