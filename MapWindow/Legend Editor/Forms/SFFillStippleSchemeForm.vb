'********************************************************************************************************
'File Name: SFFillStippleSchemeForm.vb
'Description: Class used to work with and create coloring schemes.
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
'8/3/2005 - Total overhaul to remove DotNetBar components. (Chris M)
'10/1/2008 - Earljon Hidalgo - Modify menu icons for toolstrip. Icons provided by famfamfam
Imports System.Windows.Forms.Design

Friend Class SFFillStippleSchemeForm
    Inherits System.Windows.Forms.Form

    Private m_Provider As IWindowsFormsEditorService
    Private m_OriginalScheme As MapWindow.Interfaces.ShapefileFillStippleScheme
    Private m_FillStippleScheme As MapWindow.Interfaces.ShapefileFillStippleScheme
    'Private m_SelectedBreaks() As Boolean
    Friend WithEvents btnClearBreaks As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Private SkipDataChanged As Boolean = True
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDefaultFill As System.Windows.Forms.ComboBox
    Friend WithEvents chbDefaultTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlDefaultColor As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCaption As System.Windows.Forms.TextBox

    Private ColorMaps As New Hashtable 'Used to keep track of colors, since they must be set in paint event only

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal FillStippleScheme As MapWindow.Interfaces.ShapefileFillStippleScheme)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        m_Provider = DialogProvider
        m_OriginalScheme = FillStippleScheme
        CopyScheme(FillStippleScheme, m_FillStippleScheme)
        txtCaption.Text = frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).StippleSchemeFieldCaption
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
    Friend WithEvents lblField As System.Windows.Forms.Label
    Friend WithEvents cmbField As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tbAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbSub As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbWizard As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents WizardMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuUniqueValues As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SFFillStippleSchemeForm))
        Me.lblField = New System.Windows.Forms.Label
        Me.cmbField = New System.Windows.Forms.ComboBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdApply = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbAdd = New System.Windows.Forms.ToolStripButton
        Me.tbSub = New System.Windows.Forms.ToolStripButton
        Me.btnClearBreaks = New System.Windows.Forms.ToolStripButton
        Me.tbWizard = New System.Windows.Forms.ToolStripDropDownButton
        Me.WizardMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuUniqueValues = New System.Windows.Forms.ToolStripMenuItem
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.dgv = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbDefaultFill = New System.Windows.Forms.ComboBox
        Me.chbDefaultTransparent = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlDefaultColor = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCaption = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        Me.WizardMenu.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblField
        '
        Me.lblField.AccessibleDescription = Nothing
        Me.lblField.AccessibleName = Nothing
        resources.ApplyResources(Me.lblField, "lblField")
        Me.lblField.Font = Nothing
        Me.lblField.Name = "lblField"
        '
        'cmbField
        '
        Me.cmbField.AccessibleDescription = Nothing
        Me.cmbField.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbField, "cmbField")
        Me.cmbField.BackgroundImage = Nothing
        Me.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbField.Font = Nothing
        Me.cmbField.Name = "cmbField"
        '
        'cmdOK
        '
        Me.cmdOK.AccessibleDescription = Nothing
        Me.cmdOK.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.BackgroundImage = Nothing
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Font = Nothing
        Me.cmdOK.Name = "cmdOK"
        '
        'cmdCancel
        '
        Me.cmdCancel.AccessibleDescription = Nothing
        Me.cmdCancel.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.BackgroundImage = Nothing
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        '
        'cmdApply
        '
        Me.cmdApply.AccessibleDescription = Nothing
        Me.cmdApply.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdApply, "cmdApply")
        Me.cmdApply.BackgroundImage = Nothing
        Me.cmdApply.Font = Nothing
        Me.cmdApply.Name = "cmdApply"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AccessibleDescription = Nothing
        Me.ToolStrip1.AccessibleName = Nothing
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.BackgroundImage = Nothing
        Me.ToolStrip1.Font = Nothing
        Me.ToolStrip1.ImageList = Me.ImageList1
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbAdd, Me.tbSub, Me.btnClearBreaks, Me.tbWizard})
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        '
        'tbAdd
        '
        Me.tbAdd.AccessibleDescription = Nothing
        Me.tbAdd.AccessibleName = Nothing
        resources.ApplyResources(Me.tbAdd, "tbAdd")
        Me.tbAdd.BackgroundImage = Nothing
        Me.tbAdd.Image = Global.MapWindow.GlobalResource.imgAdd
        Me.tbAdd.Name = "tbAdd"
        '
        'tbSub
        '
        Me.tbSub.AccessibleDescription = Nothing
        Me.tbSub.AccessibleName = Nothing
        resources.ApplyResources(Me.tbSub, "tbSub")
        Me.tbSub.BackgroundImage = Nothing
        Me.tbSub.Image = Global.MapWindow.GlobalResource.imgDelete
        Me.tbSub.Name = "tbSub"
        '
        'btnClearBreaks
        '
        Me.btnClearBreaks.AccessibleDescription = Nothing
        Me.btnClearBreaks.AccessibleName = Nothing
        resources.ApplyResources(Me.btnClearBreaks, "btnClearBreaks")
        Me.btnClearBreaks.BackgroundImage = Nothing
        Me.btnClearBreaks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnClearBreaks.Image = Global.MapWindow.GlobalResource.mnuLayerClear
        Me.btnClearBreaks.Name = "btnClearBreaks"
        '
        'tbWizard
        '
        Me.tbWizard.AccessibleDescription = Nothing
        Me.tbWizard.AccessibleName = Nothing
        resources.ApplyResources(Me.tbWizard, "tbWizard")
        Me.tbWizard.BackgroundImage = Nothing
        Me.tbWizard.DropDown = Me.WizardMenu
        Me.tbWizard.Image = Global.MapWindow.GlobalResource.imgFlash
        Me.tbWizard.Name = "tbWizard"
        '
        'WizardMenu
        '
        Me.WizardMenu.AccessibleDescription = Nothing
        Me.WizardMenu.AccessibleName = Nothing
        resources.ApplyResources(Me.WizardMenu, "WizardMenu")
        Me.WizardMenu.BackgroundImage = Nothing
        Me.WizardMenu.Font = Nothing
        Me.WizardMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUniqueValues})
        Me.WizardMenu.Name = "WizardMenu"
        Me.WizardMenu.OwnerItem = Me.tbWizard
        '
        'mnuUniqueValues
        '
        Me.mnuUniqueValues.AccessibleDescription = Nothing
        Me.mnuUniqueValues.AccessibleName = Nothing
        resources.ApplyResources(Me.mnuUniqueValues, "mnuUniqueValues")
        Me.mnuUniqueValues.BackgroundImage = Nothing
        Me.mnuUniqueValues.Name = "mnuUniqueValues"
        Me.mnuUniqueValues.ShortcutKeyDisplayString = Nothing
        '
        'chkPreview
        '
        Me.chkPreview.AccessibleDescription = Nothing
        Me.chkPreview.AccessibleName = Nothing
        resources.ApplyResources(Me.chkPreview, "chkPreview")
        Me.chkPreview.BackgroundImage = Nothing
        Me.chkPreview.Font = Nothing
        Me.chkPreview.Name = "chkPreview"
        '
        'dgv
        '
        Me.dgv.AccessibleDescription = Nothing
        Me.dgv.AccessibleName = Nothing
        resources.ApplyResources(Me.dgv, "dgv")
        Me.dgv.BackgroundImage = Nothing
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv.Font = Nothing
        Me.dgv.Name = "dgv"
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'cmbDefaultFill
        '
        Me.cmbDefaultFill.AccessibleDescription = Nothing
        Me.cmbDefaultFill.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbDefaultFill, "cmbDefaultFill")
        Me.cmbDefaultFill.BackgroundImage = Nothing
        Me.cmbDefaultFill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultFill.Font = Nothing
        Me.cmbDefaultFill.Items.AddRange(New Object() {resources.GetString("cmbDefaultFill.Items"), resources.GetString("cmbDefaultFill.Items1"), resources.GetString("cmbDefaultFill.Items2"), resources.GetString("cmbDefaultFill.Items3"), resources.GetString("cmbDefaultFill.Items4"), resources.GetString("cmbDefaultFill.Items5")})
        Me.cmbDefaultFill.Name = "cmbDefaultFill"
        '
        'chbDefaultTransparent
        '
        Me.chbDefaultTransparent.AccessibleDescription = Nothing
        Me.chbDefaultTransparent.AccessibleName = Nothing
        resources.ApplyResources(Me.chbDefaultTransparent, "chbDefaultTransparent")
        Me.chbDefaultTransparent.BackgroundImage = Nothing
        Me.chbDefaultTransparent.Font = Nothing
        Me.chbDefaultTransparent.Name = "chbDefaultTransparent"
        Me.chbDefaultTransparent.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AccessibleDescription = Nothing
        Me.Label2.AccessibleName = Nothing
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Font = Nothing
        Me.Label2.Name = "Label2"
        '
        'pnlDefaultColor
        '
        Me.pnlDefaultColor.AccessibleDescription = Nothing
        Me.pnlDefaultColor.AccessibleName = Nothing
        resources.ApplyResources(Me.pnlDefaultColor, "pnlDefaultColor")
        Me.pnlDefaultColor.BackColor = System.Drawing.Color.Black
        Me.pnlDefaultColor.BackgroundImage = Nothing
        Me.pnlDefaultColor.Font = Nothing
        Me.pnlDefaultColor.Name = "pnlDefaultColor"
        '
        'Label3
        '
        Me.Label3.AccessibleDescription = Nothing
        Me.Label3.AccessibleName = Nothing
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Font = Nothing
        Me.Label3.Name = "Label3"
        '
        'txtCaption
        '
        Me.txtCaption.AccessibleDescription = Nothing
        Me.txtCaption.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCaption, "txtCaption")
        Me.txtCaption.BackgroundImage = Nothing
        Me.txtCaption.Font = Nothing
        Me.txtCaption.Name = "txtCaption"
        '
        'SFFillStippleSchemeForm
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.txtCaption)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.pnlDefaultColor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDefaultFill)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chbDefaultTransparent)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.cmbField)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblField)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = Nothing
        Me.MinimizeBox = False
        Me.Name = "SFFillStippleSchemeForm"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.WizardMenu.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub CopyScheme(ByVal Original As MapWindow.Interfaces.ShapefileFillStippleScheme, ByRef NewScheme As MapWindow.Interfaces.ShapefileFillStippleScheme)
        If NewScheme Is Nothing Then NewScheme = New MapWindow.Interfaces.ShapefileFillStippleScheme
        NewScheme.ClearHatches()
        If Not Original Is Nothing Then
            Dim i As IEnumerator = Original.GetHatchesEnumerator()
            While i.MoveNext()
                NewScheme.AddHatch(i.Current.key, i.Current.value)
            End While

            NewScheme.FieldHandle = Original.FieldHandle
            NewScheme.LayerHandle = Original.LayerHandle
        End If
    End Sub

    Private Sub cmbField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbField.SelectedIndexChanged
        m_FillStippleScheme.FieldHandle = cmbField.SelectedIndex
        If Not SkipDataChanged Then txtCaption.Text = cmbField.Text
    End Sub

    Private Function GetFieldMinMax(ByRef Min As Object, ByRef Max As Object) As Boolean
        Dim sf As MapWinGIS.Shapefile
        Dim i, fld As Integer

        fld = m_FillStippleScheme.FieldHandle

        Try
            sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
            If sf Is Nothing Then
                g_error = "Failed to get Shapefile object"
                Return False
            End If
            If fld > sf.NumFields - 1 OrElse fld < 0 Then
                g_error = "Field index specified in cmbField is out of bounds"
                Return False ' field is out of bounds
            End If
            If sf.Field(fld).Type = MapWinGIS.FieldType.STRING_FIELD Then
                Dim tMin, tMax, val As String
                tMin = CStr(sf.CellValue(fld, 0))
                tMax = tMin
                For i = 1 To sf.NumShapes - 1
                    val = CStr(sf.CellValue(fld, i))
                    If val > tMax Then
                        Max = val
                    ElseIf val < tMin Then
                        Min = val
                    End If
                Next
                Min = tMin
                Max = tMax
            ElseIf sf.Field(fld).Type = MapWinGIS.FieldType.DOUBLE_FIELD Then
                Dim tMin, tMax, val As Double
                tMin = CDbl(sf.CellValue(fld, 0))
                tMax = tMin
                For i = 1 To sf.NumShapes - 1
                    val = CDbl(sf.CellValue(fld, i))
                    If val > tMax Then
                        Max = val
                    ElseIf val < tMin Then
                        Min = val
                    End If
                Next
                Min = tMin
                Max = tMax
            Else
                Dim tMin, tMax, val As Integer
                tMin = CInt(sf.CellValue(fld, 0))
                tMax = tMin
                For i = 1 To sf.NumShapes - 1
                    val = CInt(sf.CellValue(fld, i))
                    If val > tMax Then
                        Max = val
                    ElseIf val < tMin Then
                        Min = val
                    End If
                Next
                Min = tMin
                Max = tMax
            End If

        Catch ex As Exception
            ' it didn't work! 
            g_error = ex.Message
            ShowError(ex)
            Return False
        End Try
        Return True
    End Function

    Private Sub Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim sf As MapWinGIS.Shapefile
        Dim i As Integer

        sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then
            g_error = "Failed to get Shapefile object"
            Exit Sub
        End If
        For i = 0 To sf.NumFields - 1
            cmbField.Items.Add(sf.Field(i).Name)
        Next

        If m_FillStippleScheme.FieldHandle >= 0 AndAlso m_FillStippleScheme.FieldHandle < sf.NumFields() Then
            cmbField.SelectedIndex = m_FillStippleScheme.FieldHandle
        Else
            cmbField.SelectedIndex = 0
        End If
        sf = Nothing

        GridSetup()
        Dim q As IEnumerator = m_FillStippleScheme.GetHatchesEnumerator()
        While q.MoveNext()
            Dim brk As MapWindow.Interfaces.ShapefileFillStippleBreak = q.Current.value
            dgv.Rows.Add(q.Current.key, StippleToString(brk.Hatch), brk.Transparent, "")
            If brk.LineColor.A = 0 Then brk.LineColor = System.Drawing.Color.FromArgb(255, brk.LineColor)
            If (ColorMaps.Contains(brk.Value)) Then
                ColorMaps(brk.Value) = brk.LineColor
            Else
                ColorMaps.Add(brk.Value, brk.LineColor)
            End If
        End While
        dgv.Columns(0).SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Sort(dgv.Columns(0), System.ComponentModel.ListSortDirection.Ascending)

        cmbDefaultFill.SelectedIndex = 0

        SkipDataChanged = False
    End Sub

    Public ReadOnly Property Retval() As MapWindow.Interfaces.ShapefileFillStippleScheme
        Get
            CopyScheme(m_FillStippleScheme, m_OriginalScheme)
            frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).StippleSchemeFieldCaption = txtCaption.Text
            Return m_OriginalScheme
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If cmdApply.Enabled = True Then
            ApplyChanges()
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        ApplyChanges()
    End Sub

    Private Sub ApplyChanges()
        CopyScheme(m_FillStippleScheme, m_OriginalScheme)
        frmMain.Layers(frmMain.Layers.CurrentLayer).FillStippleScheme = m_OriginalScheme
        frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).StippleSchemeFieldCaption = txtCaption.Text
        cmdApply.Enabled = False
        frmMain.Plugins.BroadcastMessage("FillStippleSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
    End Sub

    Private Sub DataChanged()
        If SkipDataChanged Then Exit Sub
        cmdApply.Enabled = True

        m_FillStippleScheme.ClearHatches()
        For i As Integer = 0 To dgv.Rows.Count - 1
            Try
                m_FillStippleScheme.AddHatch(dgv.Rows(i).Cells(0).Value, dgv.Rows(i).Cells(2).Value, ColorMaps(dgv.Rows(i).Cells(0).Value), StringToStipple(dgv.Rows(i).Cells(1).Value))
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
            End Try
        Next

        Preview()
    End Sub

    Private Sub ColoringSchemeViewer1_DataChanged()
        DataChanged()
    End Sub

    Private Sub ToolStrip1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Select Case e.ClickedItem.Name
            Case "tbAdd"
                If m_FillStippleScheme Is Nothing Then Exit Sub

                Dim b As New MapWinGIS.ShapefileColorBreak
                b.Caption = ""
                b.StartColor = MapWinUtility.Colors.ColorToUInteger(System.Drawing.Color.Black)
                b.EndColor = b.StartColor
                b.StartValue = 0
                b.EndValue = b.StartValue

                dgv.Rows.Add("None", cmbDefaultFill.Text, chbDefaultTransparent.Checked, "")
                If Not ColorMaps.Contains("None") Then
                    ColorMaps.Add("None", pnlDefaultColor.BackColor)
                End If
                DataChanged()

            Case "tbSub"
                If Not dgv.CurrentRow.Index = -1 Then
                    dgv.Rows.Remove(dgv.Rows(dgv.CurrentRow.Index))
                End If
                DataChanged()
        End Select
    End Sub

    Private Function GetUniqueBreaks() As Boolean
        Dim i As Integer
        Dim ht As New Hashtable
        Dim sf As MapWinGIS.Shapefile
        Dim val As Object, fld As Integer
        Dim arr() As Object
        'Added by Rob Cairns 20-Mar-06 support for null values
        Dim nullValFound As Boolean

        fld = cmbField.SelectedIndex
        If fld = -1 Then Exit Function

        'Get shapefile
        sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then
            g_error = "Failed to get Shapefile object"
            Return False
        End If

        'Check input:	  
        If fld > sf.NumFields - 1 OrElse fld < 0 Then
            g_error = "Field index in cmbField is out of range."
            Return False
        End If

        'Clear out old datasource
        dgv.DataSource = Nothing
        dgv.Rows.Clear()
        dgv.Columns.Clear()

        'Get unique values
        nullValFound = False
        For i = 0 To sf.NumShapes - 1
            If Not IsDBNull(sf.CellValue(fld, i)) Then
                val = sf.CellValue(fld, i)
                If ht.ContainsKey(val) = False Then
                    ht.Add(val, val)
                End If
            Else
                nullValFound = True
            End If
        Next

        'Create sorted array:
        ReDim arr(ht.Count - 1)
        ht.Values().CopyTo(arr, 0)
        Array.Sort(arr)

        'Set up the grid
        GridSetup()

        'Add the null value break
        If (nullValFound) Then
            dgv.Rows.Add("null", StippleToString(cmbDefaultFill.Text), chbDefaultTransparent.Checked, "")
            dgv.Rows(dgv.Rows.Count - 1).Cells(3).Style.BackColor = Color.Black
            If Not (ColorMaps.Contains("null".ToString())) Then
                ColorMaps.Add("null", pnlDefaultColor.BackColor)
            End If
        End If

        'Create color for each unique value
        For i = 0 To arr.Length - 1
            dgv.Rows.Add(arr(i), cmbDefaultFill.Text, chbDefaultTransparent.Checked, "")
            If (ColorMaps.Contains(arr(i).ToString())) Then
                ColorMaps(arr(i).ToString()) = pnlDefaultColor.BackColor
            Else
                ColorMaps.Add(arr(i).ToString(), pnlDefaultColor.BackColor)
            End If
        Next

        DataChanged()

        'Clean up:
        ht = Nothing
        sf = Nothing
        Return True
    End Function

    Private Sub mnuUniqueValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUniqueValues.Click
        SkipDataChanged = True
        GetUniqueBreaks()
        SkipDataChanged = False
        DataChanged()
    End Sub

    Private Sub Preview()
        If chkPreview.Checked Then
            'Set the fill color to the existing fill color, to force the map to draw in any fills not covered by the above scheme.
            frmMain.Layers(frmMain.Layers.CurrentLayer).FillStippleScheme = m_FillStippleScheme
            frmMain.Plugins.BroadcastMessage("StippleSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
        End If
    End Sub

    Private Function GetFieldValues() As Double()
        Dim sf As MapWinGIS.Shapefile
        Dim i, fld As Integer
        Dim lValues() As Double = Nothing
        Try
            fld = m_FillStippleScheme.FieldHandle
            sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
            ReDim lValues(sf.NumShapes - 1)
            For i = 0 To sf.NumShapes - 1
                lValues(i) = CDbl(sf.CellValue(fld, i))
            Next
        Catch ex As Exception
        End Try
        Return lValues
    End Function

    Private Sub chkPreview_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPreview.CheckedChanged
        Preview()
    End Sub

    Private Sub btnClearBreaks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearBreaks.Click
        If MapWinUtility.Logger.Msg("Are you sure you wish to clear all breaks?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Clear Breaks?") = MsgBoxResult.Yes Then
            'Clear out old datasource
            GridSetup()
            DataChanged()
        End If
    End Sub

    Private Sub GridSetup()
        'Set up the grid
        dgv.Rows.Clear()
        dgv.Columns.Clear()
        Dim cmbFillStipple As New DataGridViewComboBoxColumn
        cmbFillStipple.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        cmbFillStipple.DataPropertyName = "Fill Stipple"
        cmbFillStipple.Items.AddRange("None", "Diagonal Down-Left", "Dialgonal Down-Right", "Vertical", "Horizontal", "Cross/Dot")
        cmbFillStipple.Sorted = True
        cmbFillStipple.HeaderText = "Fill Stipple"
        cmbFillStipple.Name = "Fill Stipple"
        cmbFillStipple.ReadOnly = False
        Dim txtFieldValue As New DataGridViewTextBoxColumn
        txtFieldValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        txtFieldValue.DataPropertyName = "Field Value"
        txtFieldValue.HeaderText = "Field Value"
        txtFieldValue.Name = "Field Value"
        txtFieldValue.ReadOnly = False
        Dim transField As New DataGridViewCheckBoxColumn
        transField.HeaderText = "Transparent Background"
        transField.Name = "Transparent Background"
        transField.ReadOnly = False
        Dim clrField As New DataGridViewTextBoxColumn
        clrField.HeaderText = "Line Color"
        clrField.Name = "Line Color"
        clrField.ReadOnly = False
        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.AutoResizeColumns()
        dgv.AutoResizeRows()
        dgv.ShowCellToolTips = True
        dgv.ShowEditingIcon = False
        dgv.Columns.Add(txtFieldValue)
        dgv.Columns(0).SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns.Add(cmbFillStipple)
        dgv.Columns.Add(transField)
        dgv.Columns.Add(clrField)
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.ColumnIndex = -1 Then Exit Sub

        If dgv.Columns(e.ColumnIndex).Name = "Line Color" Then
            Dim dlg As New ColorDialog()
            Dim currentCell As DataGridViewTextBoxCell = dgv(e.ColumnIndex, e.RowIndex)
            dlg.Color = currentCell.Style.BackColor
            If dlg.ShowDialog() = DialogResult.OK Then
                If Not currentCell Is Nothing Then
                    currentCell.Style.BackColor = dlg.Color
                    If ColorMaps.Contains(dgv(0, e.RowIndex).Value) Then
                        ColorMaps(dgv(0, e.RowIndex).Value) = dlg.Color
                    Else
                        ColorMaps.Add(dgv(0, e.RowIndex).Value, dlg.Color)
                    End If
                    dgv.ClearSelection()
                    DataChanged()
                    cmdApply.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        DataChanged()
    End Sub

    Private Sub dgv_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting
        If dgv.Columns(e.ColumnIndex).Name = "Line Color" AndAlso Not dgv(0, e.RowIndex).Value Is Nothing Then
            If ColorMaps.Contains(dgv(0, e.RowIndex).Value) Then
                e.CellStyle.BackColor = ColorMaps(dgv(0, e.RowIndex).Value)
            Else
                e.CellStyle.BackColor = Color.Black
            End If
            If e.CellStyle.BackColor.A = 0 Then e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, e.CellStyle.BackColor)
        End If
    End Sub

    Private Sub pnlDefaultColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlDefaultColor.Click
        Dim dlg As New ColorDialog
        dlg.Color = pnlDefaultColor.BackColor
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            pnlDefaultColor.BackColor = dlg.Color
        End If
    End Sub
End Class

Public Class ColorPickerColumn
    'This class is mostly just so we can compare the type
    Inherits DataGridViewButtonColumn
End Class
