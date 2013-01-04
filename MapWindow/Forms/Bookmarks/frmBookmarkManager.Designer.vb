<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBookmarkManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBookmarkManager))
        Me.CheckedListBookmarks = New System.Windows.Forms.CheckedListBox
        Me.GroupBoxExtents = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxYMin = New System.Windows.Forms.TextBox
        Me.TextBoxYMax = New System.Windows.Forms.TextBox
        Me.TextBoxXMax = New System.Windows.Forms.TextBox
        Me.TextBoxXMin = New System.Windows.Forms.TextBox
        Me.ButtonEdit = New System.Windows.Forms.Button
        Me.ButtonDelete = New System.Windows.Forms.Button
        Me.ButtonUnCheckAll = New System.Windows.Forms.Button
        Me.ButtonCheckAll = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMIExportXML = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMIExportCSV = New System.Windows.Forms.ToolStripMenuItem
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMIImportXML = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMIImportCSV = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBoxExtents.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckedListBookmarks
        '
        Me.CheckedListBookmarks.AccessibleDescription = Nothing
        Me.CheckedListBookmarks.AccessibleName = Nothing
        resources.ApplyResources(Me.CheckedListBookmarks, "CheckedListBookmarks")
        Me.CheckedListBookmarks.BackgroundImage = Nothing
        Me.CheckedListBookmarks.Font = Nothing
        Me.CheckedListBookmarks.FormattingEnabled = True
        Me.CheckedListBookmarks.Name = "CheckedListBookmarks"
        '
        'GroupBoxExtents
        '
        Me.GroupBoxExtents.AccessibleDescription = Nothing
        Me.GroupBoxExtents.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBoxExtents, "GroupBoxExtents")
        Me.GroupBoxExtents.BackgroundImage = Nothing
        Me.GroupBoxExtents.Controls.Add(Me.Label4)
        Me.GroupBoxExtents.Controls.Add(Me.Label3)
        Me.GroupBoxExtents.Controls.Add(Me.Label2)
        Me.GroupBoxExtents.Controls.Add(Me.Label1)
        Me.GroupBoxExtents.Controls.Add(Me.TextBoxYMin)
        Me.GroupBoxExtents.Controls.Add(Me.TextBoxYMax)
        Me.GroupBoxExtents.Controls.Add(Me.TextBoxXMax)
        Me.GroupBoxExtents.Controls.Add(Me.TextBoxXMin)
        Me.GroupBoxExtents.Font = Nothing
        Me.GroupBoxExtents.Name = "GroupBoxExtents"
        Me.GroupBoxExtents.TabStop = False
        '
        'Label4
        '
        Me.Label4.AccessibleDescription = Nothing
        Me.Label4.AccessibleName = Nothing
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Font = Nothing
        Me.Label4.Name = "Label4"
        '
        'Label3
        '
        Me.Label3.AccessibleDescription = Nothing
        Me.Label3.AccessibleName = Nothing
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Font = Nothing
        Me.Label3.Name = "Label3"
        '
        'Label2
        '
        Me.Label2.AccessibleDescription = Nothing
        Me.Label2.AccessibleName = Nothing
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Font = Nothing
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'TextBoxYMin
        '
        Me.TextBoxYMin.AccessibleDescription = Nothing
        Me.TextBoxYMin.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBoxYMin, "TextBoxYMin")
        Me.TextBoxYMin.BackgroundImage = Nothing
        Me.TextBoxYMin.Font = Nothing
        Me.TextBoxYMin.Name = "TextBoxYMin"
        Me.TextBoxYMin.ReadOnly = True
        '
        'TextBoxYMax
        '
        Me.TextBoxYMax.AccessibleDescription = Nothing
        Me.TextBoxYMax.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBoxYMax, "TextBoxYMax")
        Me.TextBoxYMax.BackgroundImage = Nothing
        Me.TextBoxYMax.Font = Nothing
        Me.TextBoxYMax.Name = "TextBoxYMax"
        Me.TextBoxYMax.ReadOnly = True
        '
        'TextBoxXMax
        '
        Me.TextBoxXMax.AccessibleDescription = Nothing
        Me.TextBoxXMax.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBoxXMax, "TextBoxXMax")
        Me.TextBoxXMax.BackgroundImage = Nothing
        Me.TextBoxXMax.Font = Nothing
        Me.TextBoxXMax.Name = "TextBoxXMax"
        Me.TextBoxXMax.ReadOnly = True
        '
        'TextBoxXMin
        '
        Me.TextBoxXMin.AccessibleDescription = Nothing
        Me.TextBoxXMin.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBoxXMin, "TextBoxXMin")
        Me.TextBoxXMin.BackgroundImage = Nothing
        Me.TextBoxXMin.Font = Nothing
        Me.TextBoxXMin.Name = "TextBoxXMin"
        Me.TextBoxXMin.ReadOnly = True
        '
        'ButtonEdit
        '
        Me.ButtonEdit.AccessibleDescription = Nothing
        Me.ButtonEdit.AccessibleName = Nothing
        resources.ApplyResources(Me.ButtonEdit, "ButtonEdit")
        Me.ButtonEdit.BackgroundImage = Nothing
        Me.ButtonEdit.Font = Nothing
        Me.ButtonEdit.Name = "ButtonEdit"
        Me.ButtonEdit.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.AccessibleDescription = Nothing
        Me.ButtonDelete.AccessibleName = Nothing
        resources.ApplyResources(Me.ButtonDelete, "ButtonDelete")
        Me.ButtonDelete.BackgroundImage = Nothing
        Me.ButtonDelete.Font = Nothing
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'ButtonUnCheckAll
        '
        Me.ButtonUnCheckAll.AccessibleDescription = Nothing
        Me.ButtonUnCheckAll.AccessibleName = Nothing
        resources.ApplyResources(Me.ButtonUnCheckAll, "ButtonUnCheckAll")
        Me.ButtonUnCheckAll.BackgroundImage = Nothing
        Me.ButtonUnCheckAll.Font = Nothing
        Me.ButtonUnCheckAll.Name = "ButtonUnCheckAll"
        Me.ButtonUnCheckAll.UseVisualStyleBackColor = True
        '
        'ButtonCheckAll
        '
        Me.ButtonCheckAll.AccessibleDescription = Nothing
        Me.ButtonCheckAll.AccessibleName = Nothing
        resources.ApplyResources(Me.ButtonCheckAll, "ButtonCheckAll")
        Me.ButtonCheckAll.BackgroundImage = Nothing
        Me.ButtonCheckAll.Font = Nothing
        Me.ButtonCheckAll.Name = "ButtonCheckAll"
        Me.ButtonCheckAll.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AccessibleDescription = Nothing
        Me.MenuStrip1.AccessibleName = Nothing
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.BackgroundImage = Nothing
        Me.MenuStrip1.Font = Nothing
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToolStripMenuItem, Me.ImportToolStripMenuItem})
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.AccessibleDescription = Nothing
        Me.ExportToolStripMenuItem.AccessibleName = Nothing
        resources.ApplyResources(Me.ExportToolStripMenuItem, "ExportToolStripMenuItem")
        Me.ExportToolStripMenuItem.BackgroundImage = Nothing
        Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIExportXML, Me.TSMIExportCSV})
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.ShortcutKeyDisplayString = Nothing
        '
        'TSMIExportXML
        '
        Me.TSMIExportXML.AccessibleDescription = Nothing
        Me.TSMIExportXML.AccessibleName = Nothing
        resources.ApplyResources(Me.TSMIExportXML, "TSMIExportXML")
        Me.TSMIExportXML.BackgroundImage = Nothing
        Me.TSMIExportXML.Name = "TSMIExportXML"
        Me.TSMIExportXML.ShortcutKeyDisplayString = Nothing
        '
        'TSMIExportCSV
        '
        Me.TSMIExportCSV.AccessibleDescription = Nothing
        Me.TSMIExportCSV.AccessibleName = Nothing
        resources.ApplyResources(Me.TSMIExportCSV, "TSMIExportCSV")
        Me.TSMIExportCSV.BackgroundImage = Nothing
        Me.TSMIExportCSV.Name = "TSMIExportCSV"
        Me.TSMIExportCSV.ShortcutKeyDisplayString = Nothing
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.AccessibleDescription = Nothing
        Me.ImportToolStripMenuItem.AccessibleName = Nothing
        resources.ApplyResources(Me.ImportToolStripMenuItem, "ImportToolStripMenuItem")
        Me.ImportToolStripMenuItem.BackgroundImage = Nothing
        Me.ImportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIImportXML, Me.TSMIImportCSV})
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.ShortcutKeyDisplayString = Nothing
        '
        'TSMIImportXML
        '
        Me.TSMIImportXML.AccessibleDescription = Nothing
        Me.TSMIImportXML.AccessibleName = Nothing
        resources.ApplyResources(Me.TSMIImportXML, "TSMIImportXML")
        Me.TSMIImportXML.BackgroundImage = Nothing
        Me.TSMIImportXML.Name = "TSMIImportXML"
        Me.TSMIImportXML.ShortcutKeyDisplayString = Nothing
        '
        'TSMIImportCSV
        '
        Me.TSMIImportCSV.AccessibleDescription = Nothing
        Me.TSMIImportCSV.AccessibleName = Nothing
        resources.ApplyResources(Me.TSMIImportCSV, "TSMIImportCSV")
        Me.TSMIImportCSV.BackgroundImage = Nothing
        Me.TSMIImportCSV.Name = "TSMIImportCSV"
        Me.TSMIImportCSV.ShortcutKeyDisplayString = Nothing
        '
        'frmBookmarkManager
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.ButtonUnCheckAll)
        Me.Controls.Add(Me.ButtonCheckAll)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonEdit)
        Me.Controls.Add(Me.GroupBoxExtents)
        Me.Controls.Add(Me.CheckedListBookmarks)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = Nothing
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmBookmarkManager"
        Me.GroupBoxExtents.ResumeLayout(False)
        Me.GroupBoxExtents.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckedListBookmarks As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBoxExtents As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonEdit As System.Windows.Forms.Button
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents TextBoxXMax As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxXMin As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYMin As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxYMax As System.Windows.Forms.TextBox
    Friend WithEvents ButtonUnCheckAll As System.Windows.Forms.Button
    Friend WithEvents ButtonCheckAll As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIExportXML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIExportCSV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIImportXML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIImportCSV As System.Windows.Forms.ToolStripMenuItem
End Class
