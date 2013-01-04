'10/18/2005 - Paul Meems (pm) - Starting with translating resourcefile into Dutch.
'7/31/2006 PM - Translated new strings into Dutch and set the localization to true again
'6/18/2009 Paul Meems - Redesign of about form. Adding names and versions of loaded DLLs and OCX

Imports System.Reflection

Friend Class frmAbout
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents BuildDate As System.Windows.Forms.Label
    Friend WithEvents MapwinVersion As System.Windows.Forms.Label
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents notes As System.Windows.Forms.RichTextBox
    Friend WithEvents lbURL As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picMapWindow As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblProjFile As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Developer As System.Windows.Forms.Label
    Friend WithEvents lboxLoadedAssemblies As System.Windows.Forms.ListBox
    Friend WithEvents lblMapWinGISVersion As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Copyrights As System.Windows.Forms.TextBox
    Friend WithEvents lblConfigFile As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.lbName = New System.Windows.Forms.Label()
        Me.Version = New System.Windows.Forms.Label()
        Me.BuildDate = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMapWinGISVersion = New System.Windows.Forms.Label()
        Me.lboxLoadedAssemblies = New System.Windows.Forms.ListBox()
        Me.Developer = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.MapwinVersion = New System.Windows.Forms.Label()
        Me.picMapWindow = New System.Windows.Forms.PictureBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.notes = New System.Windows.Forms.RichTextBox()
        Me.lbURL = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblProjFile = New System.Windows.Forms.Label()
        Me.lblConfigFile = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Copyrights = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picMapWindow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbName
        '
        resources.ApplyResources(Me.lbName, "lbName")
        Me.lbName.Name = "lbName"
        '
        'Version
        '
        resources.ApplyResources(Me.Version, "Version")
        Me.Version.Name = "Version"
        '
        'BuildDate
        '
        resources.ApplyResources(Me.BuildDate, "BuildDate")
        Me.BuildDate.Name = "BuildDate"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblMapWinGISVersion)
        Me.GroupBox1.Controls.Add(Me.lboxLoadedAssemblies)
        Me.GroupBox1.Controls.Add(Me.Developer)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.LinkLabel2)
        Me.GroupBox1.Controls.Add(Me.MapwinVersion)
        Me.GroupBox1.Controls.Add(Me.picMapWindow)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'lblMapWinGISVersion
        '
        resources.ApplyResources(Me.lblMapWinGISVersion, "lblMapWinGISVersion")
        Me.lblMapWinGISVersion.Name = "lblMapWinGISVersion"
        '
        'lboxLoadedAssemblies
        '
        Me.lboxLoadedAssemblies.BackColor = System.Drawing.SystemColors.Control
        Me.lboxLoadedAssemblies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lboxLoadedAssemblies.FormattingEnabled = True
        resources.ApplyResources(Me.lboxLoadedAssemblies, "lboxLoadedAssemblies")
        Me.lboxLoadedAssemblies.Name = "lboxLoadedAssemblies"
        Me.lboxLoadedAssemblies.Sorted = True
        '
        'Developer
        '
        resources.ApplyResources(Me.Developer, "Developer")
        Me.Developer.Name = "Developer"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'LinkLabel2
        '
        resources.ApplyResources(Me.LinkLabel2, "LinkLabel2")
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.TabStop = True
        '
        'MapwinVersion
        '
        resources.ApplyResources(Me.MapwinVersion, "MapwinVersion")
        Me.MapwinVersion.Name = "MapwinVersion"
        '
        'picMapWindow
        '
        Me.picMapWindow.BackColor = System.Drawing.Color.Transparent
        Me.picMapWindow.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.picMapWindow, "picMapWindow")
        Me.picMapWindow.Name = "picMapWindow"
        Me.picMapWindow.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.Name = "btnOk"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'notes
        '
        Me.notes.BackColor = System.Drawing.SystemColors.Window
        Me.notes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.notes, "notes")
        Me.notes.Name = "notes"
        Me.notes.ReadOnly = True
        '
        'lbURL
        '
        resources.ApplyResources(Me.lbURL, "lbURL")
        Me.lbURL.Name = "lbURL"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'lblProjFile
        '
        Me.lblProjFile.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblProjFile, "lblProjFile")
        Me.lblProjFile.Name = "lblProjFile"
        '
        'lblConfigFile
        '
        Me.lblConfigFile.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblConfigFile, "lblConfigFile")
        Me.lblConfigFile.Name = "lblConfigFile"
        '
        'LinkLabel1
        '
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.Copyrights)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'Copyrights
        '
        Me.Copyrights.BackColor = System.Drawing.SystemColors.Control
        Me.Copyrights.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.Copyrights, "Copyrights")
        Me.Copyrights.Name = "Copyrights"
        '
        'frmAbout
        '
        resources.ApplyResources(Me, "$this")
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnOk
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lblConfigFile)
        Me.Controls.Add(Me.lblProjFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbURL)
        Me.Controls.Add(Me.notes)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BuildDate)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.lbName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picMapWindow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Function GetVersionstringMapWinGIS() As String
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("MapWinGIS.Grid\CLSID", False)
        Dim clsid As String = regKey.GetValue("", 0.0)
        regKey.Close()

        '29-Sep-09 Rob Cairns. Changed writable to false to prevent exception thrown here
        regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\" + clsid + "\InprocServer32", False)
        Dim fileLocation As String = regKey.GetValue("", 0.0)
        regKey.Close()

        If System.IO.File.Exists(fileLocation) Then
            ' Get the file version:
            Dim myFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(fileLocation)
            ' Print the file name and version number.
            Return myFileVersionInfo.FileMajorPart.ToString() + "." + _
                   myFileVersionInfo.FileMinorPart.ToString() + "." + _
                   myFileVersionInfo.FileBuildPart.ToString() + _
                   " (" + System.IO.File.GetLastWriteTime(fileLocation).ToShortDateString() + ")"
        End If

        Return "Version not found"
    End Function
    Public Sub ShowAbout(ByVal frm As System.Windows.Forms.Form)
        Try
            'Me.Icon = frmMain.Icon

            With AppInfo
                lbName.Text = .Name
                Developer.Text += .Developer

                'Deprecated:
                BuildDate.Text += .BuildDate
                LinkLabel1.Text = .URL
                Version.Text += .Version

                'load the application license agrement
                'Chris M 3/14/2006 added ability to read from a txt or rtf file

                If (.Comments = "") Then
                    Dim lic As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetAssembly(Me.GetType).Location), "MapWindowNotes.rtf")
                    If System.IO.File.Exists(lic) Then
                        notes.LoadFile(lic, RichTextBoxStreamType.RichText)
                    End If
                ElseIf (.Comments.ToLower().EndsWith(".txt") Or .Comments.ToLower().EndsWith(".rtf")) And System.IO.File.Exists(.Comments) Then
                    If .Comments.ToLower().EndsWith(".txt") Then
                        notes.LoadFile(.Comments, RichTextBoxStreamType.RichText)
                    Else
                        notes.LoadFile(.Comments, RichTextBoxStreamType.PlainText)
                    End If
                Else
                    notes.Text = .Comments
                End If

                'Get all loaded dll's
                Dim objExecutingAssemblies As Assembly = System.Reflection.Assembly.GetExecutingAssembly
                Dim arrReferencedAssmbNames As AssemblyName() = objExecutingAssemblies.GetReferencedAssemblies

                'Loop through the array of referenced assembly names.
                For Each strAssmbName As AssemblyName In arrReferencedAssmbNames
                    Debug.WriteLine(strAssmbName.ToString())
                    If strAssmbName.Name.StartsWith("System") Then Continue For
                    If strAssmbName.Name.StartsWith("Microsoft") Then Continue For
                    If strAssmbName.Name.StartsWith("mscorlib") Then Continue For
                    If strAssmbName.Name.StartsWith("stdole") Then Continue For

                    lboxLoadedAssemblies.Items.Add(strAssmbName.Name + " (" + strAssmbName.Version.ToString() + ")")
                Next

                'The version of OCX:
                lblMapWinGISVersion.Text += GetVersionstringMapWinGIS()

                'MapwinVersion.Text += App.VersionString + " (" + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString() + ")"
                'Paul Meems, June 22, 2011
                'Dave Eslinger requested to show the build number as well:
                MapwinVersion.Text += "4.8.6"
                lblProjFile.Text = ProjInfo.ProjectFileName
                lblConfigFile.Text = ProjInfo.ConfigFileName

                If lblProjFile.Text.Length > 38 Then lblProjFile.Text = lblProjFile.Text.Substring(0, 10) + " . . . " + lblProjFile.Text.Substring(lblProjFile.Text.Length - 38)
                If lblConfigFile.Text.Length > 38 Then lblConfigFile.Text = lblConfigFile.Text.Substring(0, 10) + " . . . " + lblConfigFile.Text.Substring(lblConfigFile.Text.Length - 38)

                ' The copyright of MapWindow:
                Copyrights.Text = "MapWindow " & App.AssemblyCopyright & Environment.NewLine & Copyrights.Text
            End With

            ' Me.ShowDialog(frm)
        Catch ex As System.Exception
            ' Paul Meems, 7 sept 2011: No need to show the exception form
            ' frmMain.ShowErrorDialog(ex)
            MapWinUtility.Logger.Dbg("Error in showing the About form: " & ex.Message)
        End Try

        ' Paul Meems, 7 sept 2011: Always show the About form
        Me.ShowDialog(frm)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Hide()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            ' Call the Process.Start method to open the default browser 
            ' with a URL:
            System.Diagnostics.Process.Start("http://www.MapWindow.org")
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub PicMapWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Call the Process.Start method to open the default browser 
            ' with a URL:
            System.Diagnostics.Process.Start("http://www.MapWindow.org")
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub lbURL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbURL.LinkClicked
        ' Call the Process.Start method to open the default browser 
        ' with a URL:
        System.Diagnostics.Process.Start(AppInfo.URL)
    End Sub

    Private Sub Pic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Call the Process.Start method to open the default browser 
        ' with a URL:
        Try
            System.Diagnostics.Process.Start(AppInfo.URL)
        Catch
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If Not LinkLabel1.Text.ToLower().StartsWith("http://") Then
            Diagnostics.Process.Start("http://" + LinkLabel1.Text)
        Else
            Diagnostics.Process.Start(LinkLabel1.Text)
        End If
    End Sub

    Private Sub btnProcInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim userTempPath = Application.LocalUserAppDataPath + "\" + System.DateTime.Now.Ticks.ToString() + ".txt"
        Dim procInfofile As System.IO.StreamWriter = System.IO.File.CreateText(userTempPath)
        procInfofile.WriteLine(MapWinUtility.MiscUtils.GetDebugInfo())
        procInfofile.Flush()
        procInfofile.Close()
        System.Diagnostics.Process.Start(userTempPath)

    End Sub

End Class
