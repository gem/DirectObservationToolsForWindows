'********************************************************************************************************
'File Name: SFColoringSchemeForm.vb
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

Friend Class SFColoringSchemeForm
    Inherits System.Windows.Forms.Form

    Private m_Provider As IWindowsFormsEditorService
    Private m_OriginalScheme As MapWinGIS.ShapefileColorScheme
    Private m_ColoringScheme As MapWinGIS.ShapefileColorScheme
    'Private m_SelectedBreaks() As Boolean
    Private m_Precision As Integer = 3
    Friend WithEvents btnClearBreaks As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbMoveUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbbMoveDown As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtCaption As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private m_NumberFormat As String = "G" 'G = auto
    Private Loading As Boolean = True

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal ColoringScheme As MapWinGIS.ShapefileColorScheme)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
        m_OriginalScheme = ColoringScheme
        CopyScheme(ColoringScheme, m_ColoringScheme)
        ColoringSchemeViewer1.ClearSelectedBreaks()
        ColoringSchemeViewer1.InitializeControl(m_ColoringScheme)
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
    Friend WithEvents ColoringSchemeViewer1 As MapWindow.ColoringSchemeViewer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tbAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbSub As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbImport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbWizard As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents WizardMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuContinuousRamp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEqualBreaks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUniqueValues As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNumberFormat As System.Windows.Forms.ComboBox
    Friend WithEvents PanelRange As System.Windows.Forms.Panel
    Friend WithEvents RangeColors As MapWindow.RangeBar
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SFColoringSchemeForm))
        Me.lblField = New System.Windows.Forms.Label
        Me.cmbField = New System.Windows.Forms.ComboBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdApply = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbImport = New System.Windows.Forms.ToolStripButton
        Me.tbExport = New System.Windows.Forms.ToolStripButton
        Me.tbAdd = New System.Windows.Forms.ToolStripButton
        Me.tbSub = New System.Windows.Forms.ToolStripButton
        Me.btnClearBreaks = New System.Windows.Forms.ToolStripButton
        Me.tbWizard = New System.Windows.Forms.ToolStripDropDownButton
        Me.WizardMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuContinuousRamp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEqualBreaks = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUniqueValues = New System.Windows.Forms.ToolStripMenuItem
        Me.tbbMoveUp = New System.Windows.Forms.ToolStripButton
        Me.tbbMoveDown = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbNumberFormat = New System.Windows.Forms.ComboBox
        Me.PanelRange = New System.Windows.Forms.Panel
        Me.RangeColors = New MapWindow.RangeBar
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.ColoringSchemeViewer1 = New MapWindow.ColoringSchemeViewer
        Me.txtCaption = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.WizardMenu.SuspendLayout()
        Me.PanelRange.SuspendLayout()
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbImport, Me.tbExport, Me.tbAdd, Me.tbSub, Me.btnClearBreaks, Me.tbWizard, Me.tbbMoveUp, Me.tbbMoveDown})
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
        'tbImport
        '
        Me.tbImport.AccessibleDescription = Nothing
        Me.tbImport.AccessibleName = Nothing
        resources.ApplyResources(Me.tbImport, "tbImport")
        Me.tbImport.BackgroundImage = Nothing
        Me.tbImport.Image = Global.MapWindow.GlobalResource.imgFolder
        Me.tbImport.Name = "tbImport"
        '
        'tbExport
        '
        Me.tbExport.AccessibleDescription = Nothing
        Me.tbExport.AccessibleName = Nothing
        resources.ApplyResources(Me.tbExport, "tbExport")
        Me.tbExport.BackgroundImage = Nothing
        Me.tbExport.Image = Global.MapWindow.GlobalResource.imgSave
        Me.tbExport.Name = "tbExport"
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
        Me.WizardMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuContinuousRamp, Me.mnuEqualBreaks, Me.mnuUniqueValues})
        Me.WizardMenu.Name = "WizardMenu"
        Me.WizardMenu.OwnerItem = Me.tbWizard
        '
        'mnuContinuousRamp
        '
        Me.mnuContinuousRamp.AccessibleDescription = Nothing
        Me.mnuContinuousRamp.AccessibleName = Nothing
        resources.ApplyResources(Me.mnuContinuousRamp, "mnuContinuousRamp")
        Me.mnuContinuousRamp.BackgroundImage = Nothing
        Me.mnuContinuousRamp.Name = "mnuContinuousRamp"
        Me.mnuContinuousRamp.ShortcutKeyDisplayString = Nothing
        '
        'mnuEqualBreaks
        '
        Me.mnuEqualBreaks.AccessibleDescription = Nothing
        Me.mnuEqualBreaks.AccessibleName = Nothing
        resources.ApplyResources(Me.mnuEqualBreaks, "mnuEqualBreaks")
        Me.mnuEqualBreaks.BackgroundImage = Nothing
        Me.mnuEqualBreaks.Name = "mnuEqualBreaks"
        Me.mnuEqualBreaks.ShortcutKeyDisplayString = Nothing
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
        'tbbMoveUp
        '
        Me.tbbMoveUp.AccessibleDescription = Nothing
        Me.tbbMoveUp.AccessibleName = Nothing
        resources.ApplyResources(Me.tbbMoveUp, "tbbMoveUp")
        Me.tbbMoveUp.BackgroundImage = Nothing
        Me.tbbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbbMoveUp.Image = Global.MapWindow.GlobalResource.imgUp
        Me.tbbMoveUp.Name = "tbbMoveUp"
        '
        'tbbMoveDown
        '
        Me.tbbMoveDown.AccessibleDescription = Nothing
        Me.tbbMoveDown.AccessibleName = Nothing
        resources.ApplyResources(Me.tbbMoveDown, "tbbMoveDown")
        Me.tbbMoveDown.BackgroundImage = Nothing
        Me.tbbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbbMoveDown.Image = Global.MapWindow.GlobalResource.imgDown
        Me.tbbMoveDown.Name = "tbbMoveDown"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'cmbNumberFormat
        '
        Me.cmbNumberFormat.AccessibleDescription = Nothing
        Me.cmbNumberFormat.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbNumberFormat, "cmbNumberFormat")
        Me.cmbNumberFormat.BackgroundImage = Nothing
        Me.cmbNumberFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNumberFormat.Font = Nothing
        Me.cmbNumberFormat.Items.AddRange(New Object() {resources.GetString("cmbNumberFormat.Items"), resources.GetString("cmbNumberFormat.Items1"), resources.GetString("cmbNumberFormat.Items2"), resources.GetString("cmbNumberFormat.Items3")})
        Me.cmbNumberFormat.Name = "cmbNumberFormat"
        '
        'PanelRange
        '
        Me.PanelRange.AccessibleDescription = Nothing
        Me.PanelRange.AccessibleName = Nothing
        resources.ApplyResources(Me.PanelRange, "PanelRange")
        Me.PanelRange.BackgroundImage = Nothing
        Me.PanelRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelRange.Controls.Add(Me.RangeColors)
        Me.PanelRange.Font = Nothing
        Me.PanelRange.Name = "PanelRange"
        '
        'RangeColors
        '
        Me.RangeColors.AccessibleDescription = Nothing
        Me.RangeColors.AccessibleName = Nothing
        resources.ApplyResources(Me.RangeColors, "RangeColors")
        Me.RangeColors.BackgroundImage = Nothing
        Me.RangeColors.Font = Nothing
        Me.RangeColors.Histogram = New Integer(-1) {}
        Me.RangeColors.Name = "RangeColors"
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
        'ColoringSchemeViewer1
        '
        Me.ColoringSchemeViewer1.AccessibleDescription = Nothing
        Me.ColoringSchemeViewer1.AccessibleName = Nothing
        resources.ApplyResources(Me.ColoringSchemeViewer1, "ColoringSchemeViewer1")
        Me.ColoringSchemeViewer1.BackgroundImage = Nothing
        Me.ColoringSchemeViewer1.Font = Nothing
        Me.ColoringSchemeViewer1.Name = "ColoringSchemeViewer1"
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
        'Label2
        '
        Me.Label2.AccessibleDescription = Nothing
        Me.Label2.AccessibleName = Nothing
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Font = Nothing
        Me.Label2.Name = "Label2"
        '
        'SFColoringSchemeForm
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCaption)
        Me.Controls.Add(Me.PanelRange)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.cmbNumberFormat)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ColoringSchemeViewer1)
        Me.Controls.Add(Me.cmbField)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblField)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = Nothing
        Me.MinimizeBox = False
        Me.Name = "SFColoringSchemeForm"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.WizardMenu.ResumeLayout(False)
        Me.PanelRange.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub CopyScheme(ByVal Original As MapWinGIS.ShapefileColorScheme, ByRef NewScheme As MapWinGIS.ShapefileColorScheme)
        If NewScheme Is Nothing Then NewScheme = New MapWinGIS.ShapefileColorScheme
        Dim i As Integer, brk As MapWinGIS.ShapefileColorBreak
        While NewScheme.NumBreaks > 0
            NewScheme.Remove(0)
        End While
        For i = 0 To Original.NumBreaks - 1
            With Original.ColorBreak(i)
                brk = New MapWinGIS.ShapefileColorBreak
                brk.Caption = .Caption
                brk.EndColor = .EndColor
                brk.EndValue = .EndValue
                brk.StartColor = .StartColor
                brk.StartValue = .StartValue
                brk.Visible = .Visible
            End With
            NewScheme.Add(brk)
            brk = Nothing
        Next
        NewScheme.Key = Original.Key
        NewScheme.FieldIndex = Original.FieldIndex
        NewScheme.LayerHandle = Original.LayerHandle
    End Sub
    Private Sub ColoringSchemeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBoxes()
        cmbNumberFormat.SelectedIndex = 3 'Auto
    End Sub

    Private Sub cmbField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbField.SelectedIndexChanged
        m_ColoringScheme.FieldIndex = cmbField.SelectedIndex
        If Not Loading Then
            txtCaption.Text = cmbField.Text
        End If
    End Sub

    Private Function GetFieldMinMax(ByRef Min As Object, ByRef Max As Object) As Boolean
        Dim sf As MapWinGIS.Shapefile
        Dim i, fld As Integer

        fld = m_ColoringScheme.FieldIndex

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

    Private Sub PopulateComboBoxes()
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

        If m_ColoringScheme.FieldIndex >= 0 AndAlso m_ColoringScheme.FieldIndex < sf.NumFields() Then
            cmbField.SelectedIndex = m_ColoringScheme.FieldIndex
        Else
            cmbField.SelectedIndex = 0
        End If
        txtCaption.Text = frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).ColorSchemeFieldCaption
        sf = Nothing
        Loading = False
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

        'Remove current colors:
        While m_ColoringScheme.NumBreaks > 0
            m_ColoringScheme.Remove(0)
        End While

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

        'Add the null value break
        If (nullValFound) Then 'And sf.Field(fld).Type = MapWinGIS.FieldType.STRING_FIELD) Then
            Dim brk As New MapWinGIS.ShapefileColorBreak
            brk.StartColor = System.Convert.ToUInt32(RGB(255, 255, 255))
            brk.EndColor = brk.StartColor
            brk.Caption = "null"
            brk.StartValue = Nothing
            brk.EndValue = Nothing
            m_ColoringScheme.Add(brk)
        End If

        'Create color for each unique value
        For i = 0 To arr.Length - 1
            Dim brk As New MapWinGIS.ShapefileColorBreak
            Dim randomColor As UInt32
            Dim usedColors As New Hashtable

            'because the coloring is randomly chosen, it's possible (and happens often) that
            'the same color is chosen twice.
            'Beacuse of this I save the colors per scheme to avoid this.
            randomColor = getRandomColor(usedColors)

            'Original -- brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
            brk.StartColor = randomColor
            brk.EndColor = brk.StartColor
            brk.StartValue = arr(i)
            brk.EndValue = arr(i)
            If IsNumeric(arr(i)) Then
                brk.Caption = CDbl(arr(i)).ToString(m_NumberFormat & m_Precision)
            Else
                brk.Caption = CStr(arr(i))
            End If

            m_ColoringScheme.Add(brk)

            'Clean up:
            brk = Nothing
            usedColors.Clear()
            usedColors = Nothing
        Next

        'Clean up:
        ht = Nothing
        sf = Nothing
        ColoringSchemeViewer1.ClearSelectedBreaks()
        Return True
    End Function

    Private Function getRandomColor(ByRef usedColors As Hashtable) As UInt32
        Dim randomColor As UInt32
        Dim webSafeColor As String
        Dim r, g, b As Integer

        'because the coloring is randomly chosen, it's possible (and happens often) that
        'the same color is chosen twice.
        'Beacuse of this I save the colors per scheme to avoid this.
        'I want nice contrasting colors so I save the websafe colorvalues so I don't get
        'for example several shades of lightgreen.

        r = CInt(Rnd() * 255)
        g = CInt(Rnd() * 255)
        b = CInt(Rnd() * 255)
        webSafeColor = Hex(r).Substring(0, 1) & Hex(g).Substring(0, 1) & Hex(b).Substring(0, 1)
        randomColor = System.Convert.ToUInt32(RGB(r, g, b))
        Do While usedColors.ContainsKey(webSafeColor)
            r = CInt(Rnd() * 255)
            g = CInt(Rnd() * 255)
            b = CInt(Rnd() * 255)
            webSafeColor = Hex(r).Substring(0, 1) & Hex(g).Substring(0, 1) & Hex(b).Substring(0, 1)
            randomColor = System.Convert.ToUInt32(RGB(r, g, b))
        Loop
        usedColors.Add(webSafeColor, webSafeColor)

        Return randomColor
    End Function

    Public ReadOnly Property Retval() As MapWinGIS.ShapefileColorScheme
        Get
            CopyScheme(m_ColoringScheme, m_OriginalScheme)
            frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).ColorSchemeFieldCaption = txtCaption.Text
            Return m_OriginalScheme
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If cmdApply.Enabled = True Then
            ApplyChanges()
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub MakeContinuousRamp(ByVal numDesired As Integer, ByVal StartColor As Color, ByVal EndColor As Color)
        Dim sf As MapWinGIS.Shapefile
        Dim arr() As Object, i As Integer, delta As Integer
        Dim ht As New Hashtable
        Dim brk As MapWinGIS.ShapefileColorBreak
        Dim allNumeric As Boolean = True
        'Added by Rob Cairns 20-Mar-06 support for null values
        Dim nullValFound As Boolean, j As Integer
        Dim sR, sG, sB As Integer
        Dim eR, eG, eB As Integer
        Dim r, g, b As Integer
        Dim rStep, gStep, bStep As Integer

        '2007-11-01 Jack MacDonald - continuous ramp may specify zero breaks: ie, to retain current colorscheme
        If numDesired = 0 Then
            numDesired = m_ColoringScheme.NumBreaks()
            ' calculate the rgb steps from start to end colours (copy existing code)
            Try
                MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(StartColor), sR, sG, sB)
                MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(EndColor), eR, eG, eB)
                r = sR
                g = sG
                b = sB
            Catch e As System.ArgumentException
                sR = r = StartColor.R
                sG = g = StartColor.G
                sB = b = StartColor.B
                eR = EndColor.R
                eG = EndColor.G
                eB = EndColor.B
            End Try
            rStep = CInt((eR - sR) / numDesired)
            gStep = CInt((eG - sG) / numDesired)
            bStep = CInt((eB - sB) / numDesired)
            ' set colors of start and end breaks
            m_ColoringScheme.ColorBreak(0).StartColor = System.Convert.ToUInt32(RGB(sR, sG, sB))
            m_ColoringScheme.ColorBreak(0).EndColor = m_ColoringScheme.ColorBreak(0).StartColor
            m_ColoringScheme.ColorBreak(numDesired - 1).StartColor = System.Convert.ToUInt32(RGB(eR, eG, eB))
            m_ColoringScheme.ColorBreak(numDesired - 1).EndColor = m_ColoringScheme.ColorBreak(numDesired - 1).StartColor
            ' remaining breaks; increment by rgb steps
            For i = 1 To m_ColoringScheme.NumBreaks - 2
                r += rStep
                g += gStep
                b += bStep
                m_ColoringScheme.ColorBreak(i).StartColor = System.Convert.ToUInt32(RGB(r, g, b))
                m_ColoringScheme.ColorBreak(i).EndColor = m_ColoringScheme.ColorBreak(i).StartColor
            Next
            Exit Sub
        End If

        Try
            sf = CType(frmMain.Layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
            If m_ColoringScheme.FieldIndex < 0 OrElse m_ColoringScheme.FieldIndex >= sf.NumFields Then Exit Sub
            If sf.NumShapes = 0 Then Exit Sub

            ReDim arr(sf.NumShapes - 1)
            nullValFound = False
            If sf.Field(m_ColoringScheme.FieldIndex).Type = MapWinGIS.FieldType.STRING_FIELD Then
                allNumeric = False
            Else
                allNumeric = True
            End If
            j = 0
            For i = 0 To sf.NumShapes() - 1
                If Not IsDBNull(sf.CellValue(m_ColoringScheme.FieldIndex, i)) Then
                    If ht.ContainsKey(sf.CellValue(m_ColoringScheme.FieldIndex, i)) = False Then
                        arr(j) = sf.CellValue(m_ColoringScheme.FieldIndex, i)
                        ht.Add(arr(j), arr(j))
                        j = j + 1
                    End If
                    'For some reason allNumeric is not evaluating correctly here, see above fix
                    'allNumeric = allNumeric And IsNumeric(arr(j)) ' as soon as a string is encountered this gets set to false for the rest of the time
                Else
                    nullValFound = True
                End If
            Next
            ReDim Preserve arr(ht.Keys.Count - 1)
            Array.Sort(arr)

            While m_ColoringScheme.NumBreaks > 0
                m_ColoringScheme.Remove(0)
            End While



            Try
                MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(StartColor), sR, sG, sB)
                MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(EndColor), eR, eG, eB)
                r = sR
                g = sG
                b = sB
            Catch e As System.ArgumentException
                sR = r = StartColor.R
                sG = g = StartColor.G
                sB = b = StartColor.B
                eR = EndColor.R
                eG = EndColor.G
                eB = EndColor.B
            End Try

            'Add the null value to the beginning of the ColoringScheme as an empty value break
            If (nullValFound) Then
                brk = New MapWinGIS.ShapefileColorBreak
                brk.StartValue = Nothing
                brk.EndValue = Nothing
                brk.Caption = "null"
                brk.StartColor = System.Convert.ToUInt32(RGB(255, 255, 255))
                brk.EndColor = brk.StartColor
                m_ColoringScheme.Add(brk)
                brk = Nothing
            End If

            If numDesired = 1 Then
                'No advanced processing; just set it as start and end color
                'on the single break.
                brk = New MapWinGIS.ShapefileColorBreak
                If arr.Length > 0 Then
                    brk.StartValue = arr(0)
                    brk.EndValue = arr(arr.Length - 1)
                Else
                    brk.StartValue = ""
                    brk.EndValue = ""
                End If

                brk.StartColor = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(StartColor))
                brk.EndColor = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(EndColor))
                m_ColoringScheme.Add(brk)
            ElseIf ht.Keys.Count <= numDesired Then
                Dim brkArr() As Object
                ReDim brkArr(ht.Keys.Count - 1)
                ht.Keys.CopyTo(brkArr, 0)
                Array.Sort(brkArr)
                rStep = CInt((eR - sR) / brkArr.Length)
                gStep = CInt((eG - sG) / brkArr.Length)
                bStep = CInt((eB - sB) / brkArr.Length)
                For i = 0 To brkArr.Length - 1
                    brk = New MapWinGIS.ShapefileColorBreak

                    brk.StartValue = brkArr(i)
                    brk.EndValue = brkArr(i)
                    If IsNumeric(brk.StartValue) Then
                        brk.Caption = CDbl(brkArr(i)).ToString(m_NumberFormat & m_Precision)
                    Else
                        brk.Caption = CStr(brkArr(i))
                    End If
                    brk.StartColor = System.Convert.ToUInt32(RGB(r, g, b))
                    brk.EndColor = brk.StartColor
                    m_ColoringScheme.Add(brk)
                    r += rStep
                    g += gStep
                    b += bStep
                Next
            Else
                rStep = CInt((eR - sR) / numDesired)
                gStep = CInt((eG - sG) / numDesired)
                bStep = CInt((eB - sB) / numDesired)
                If allNumeric = True Then
                    Dim min As Double, max As Double, range As Double
                    min = CDbl(arr(0))
                    max = CDbl(arr(arr.Length() - 1))
                    range = max - min

                    Dim prev As Double = min
                    Dim t As Double = range / numDesired

                    For i = 1 To numDesired - 1
                        brk = New MapWinGIS.ShapefileColorBreak
                        brk.StartValue = prev
                        brk.EndValue = prev + t
                        brk.StartColor = System.Convert.ToUInt32(RGB(r, g, b))
                        brk.EndColor = brk.StartColor
                        If CDbl(brk.EndValue) = CDbl(brk.StartValue) Then
                            If CDbl(brk.StartValue) = min Then
                                brk.Caption = CStr(min)
                            Else
                                brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                            End If
                        Else
                            If CDbl(brk.StartValue) = min Then
                                brk.Caption = CStr(min)
                            Else
                                brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                            End If
                            brk.Caption &= " - "
                            brk.Caption &= CDbl(brk.EndValue).ToString(m_NumberFormat & m_Precision)
                        End If
                        m_ColoringScheme.Add(brk)
                        prev = CDbl(brk.EndValue)
                        r += rStep
                        g += gStep
                        b += bStep
                    Next
                    ' now do the last break
                    brk = New MapWinGIS.ShapefileColorBreak
                    brk.StartValue = prev
                    brk.EndValue = max
                    brk.StartColor = System.Convert.ToUInt32(RGB(r, g, b))
                    brk.EndColor = brk.StartColor
                    brk.EndColor = brk.StartColor
                    If CDbl(brk.EndValue) = CDbl(brk.StartValue) Then
                        brk.Caption = CStr(brk.StartValue)
                    Else
                        brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                    End If
                    m_ColoringScheme.Add(brk)
                Else
                    ' The values are not all numeric
                    delta = CInt(Math.Round(arr.Length / numDesired))
                    For i = 0 To arr.Length - 1 Step delta
                        If i = arr.Length - 1 Then Exit For
                        brk = New MapWinGIS.ShapefileColorBreak
                        brk.StartValue = arr(i)
                        ' Now the fun begins.  Make sure I don't end in the middle of a group of a given value.
                        Dim offset1 As Integer = 0
                        Dim offset2 As Integer = 0
                        Dim endIndex As Integer = i + delta - 1
                        If endIndex >= arr.Length Then endIndex = arr.Length - 1

                        While (endIndex + offset1 < arr.Length) AndAlso (CStr(arr(endIndex + offset1)) = CStr(arr(endIndex)))
                            offset1 += 1
                        End While

                        While (endIndex + offset2 > 0) AndAlso (CStr(arr(endIndex + offset2)) = CStr(arr(endIndex)))
                            offset2 -= 1
                        End While

                        ' find the minimum distance to another value
                        If Math.Abs(offset1) > Math.Abs(offset2) Then
                            offset1 = offset2
                        End If
                        If (endIndex + offset1 >= arr.Length) Then offset1 = arr.Length - endIndex - 1
                        brk.EndValue = arr(endIndex + offset1)

                        'CDM 11/30/2006 What's with the random colors? We chose some...!
                        'brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                        brk.StartColor = System.Convert.ToUInt32(RGB(r, g, b))
                        brk.EndColor = brk.StartColor
                        If CStr(brk.EndValue) = CStr(brk.StartValue) Then
                            brk.Caption = CStr(brk.StartValue)
                        Else
                            brk.Caption = CStr(brk.StartValue) & " - " & CStr(brk.EndValue)
                        End If
                        m_ColoringScheme.Add(brk)
                        r += rStep
                        g += gStep
                        b += bStep
                    Next
                End If
            End If

        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Private Sub MakeEqualBreaks(ByVal numDesired As Integer)
        Dim sf As MapWinGIS.Shapefile
        Dim arr() As Object, i As Integer, delta As Integer
        Dim ht As New Hashtable
        Dim brk As MapWinGIS.ShapefileColorBreak
        Dim allNumeric As Boolean = True
        'Added by Rob Cairns 20-Mar-06 support for null values
        Dim nullValFound As Boolean, j As Integer

        Try
            sf = CType(frmMain.Layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
            If m_ColoringScheme.FieldIndex < 0 OrElse m_ColoringScheme.FieldIndex >= sf.NumFields Then Exit Sub
            If sf.NumShapes = 0 Then Exit Sub

            ReDim arr(sf.NumShapes - 1)
            nullValFound = False
            If sf.Field(m_ColoringScheme.FieldIndex).Type = MapWinGIS.FieldType.STRING_FIELD Then
                allNumeric = False
            Else
                allNumeric = True
            End If
            j = 0
            For i = 0 To sf.NumShapes() - 1
                If Not IsDBNull(sf.CellValue(m_ColoringScheme.FieldIndex, i)) Then
                    If ht.ContainsKey(sf.CellValue(m_ColoringScheme.FieldIndex, i)) = False Then
                        arr(j) = sf.CellValue(m_ColoringScheme.FieldIndex, i)
                        ht.Add(arr(j), arr(j))
                        j = j + 1
                    End If
                    'For some reason allNumeric is not evaluating correctly here, see above fix
                    'allNumeric = allNumeric And IsNumeric(arr(j)) ' as soon as a string is encountered this gets set to false for the rest of the time
                Else
                    nullValFound = True
                End If
            Next
            ReDim Preserve arr(ht.Keys.Count - 1)
            Array.Sort(arr)

            While m_ColoringScheme.NumBreaks > 0
                m_ColoringScheme.Remove(0)
            End While

            'Add the null value to the beginning of the ColoringScheme as an empty value break
            If (nullValFound) Then
                brk = New MapWinGIS.ShapefileColorBreak
                brk.StartValue = Nothing
                brk.EndValue = Nothing
                brk.Caption = "null"
                brk.StartColor = System.Convert.ToUInt32(RGB(255, 255, 255))
                brk.EndColor = brk.StartColor
                m_ColoringScheme.Add(brk)
                brk = Nothing
            End If

            If ht.Keys.Count <= numDesired Then
                Dim brkArr() As Object
                ReDim brkArr(ht.Keys.Count - 1)
                ht.Keys.CopyTo(brkArr, 0)
                Array.Sort(brkArr)
                For i = 0 To brkArr.Length - 1
                    brk = New MapWinGIS.ShapefileColorBreak
                    brk.StartValue = brkArr(i)
                    brk.EndValue = brkArr(i)
                    If IsNumeric(brk.StartValue) Then
                        brk.Caption = CDbl(brkArr(i)).ToString(m_NumberFormat & m_Precision)
                        'If CDbl(arr(i)) = 0 Then
                        '    tPrecision = 0
                        'Else
                        '    tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brkArr(i)))))
                        'End If
                        'If tPrecision < 0 Then tPrecision = 0
                        'brk.Caption = CStr(Math.Round(CDbl(brkArr(i)), tPrecision))
                    Else
                        brk.Caption = CStr(brkArr(i))
                    End If
                    brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                    brk.EndColor = brk.StartColor
                    m_ColoringScheme.Add(brk)
                Next

            Else
                If allNumeric = True Then

                    Dim min As Double, max As Double, range As Double
                    min = CDbl(arr(0))

                    max = CDbl(arr(arr.Length() - 1))
                    range = max - min

                    Dim prev As Double = min
                    Dim t As Double = range / numDesired

                    For i = 1 To numDesired - 1

                        brk = New MapWinGIS.ShapefileColorBreak
                        brk.StartValue = prev
                        brk.EndValue = prev + t
                        brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                        brk.EndColor = brk.StartColor
                        If CDbl(brk.EndValue) = CDbl(brk.StartValue) Then
                            If CDbl(brk.StartValue) = min Then
                                brk.Caption = CStr(min)
                            Else
                                brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                                'If CDbl(arr(i)) = 0 Then
                                '    tPrecision = 0
                                'Else
                                '    tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.StartValue))))
                                'End If
                                'If tPrecision < 0 Then tPrecision = 0
                                'brk.Caption = CStr(Math.Round(CDbl(brk.StartValue), tPrecision))
                            End If
                        Else
                            If CDbl(brk.StartValue) = min Then
                                brk.Caption = CStr(min)
                            Else
                                brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                                'If CDbl(arr(i)) = 0 Then
                                '    tPrecision = 0
                                'Else
                                '    tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.StartValue))))
                                'End If
                                'If tPrecision < 0 Then tPrecision = 0
                                'brk.Caption = CStr(Math.Round(CDbl(brk.StartValue), tPrecision))
                            End If
                            brk.Caption &= " - "
                            brk.Caption &= CDbl(brk.EndValue).ToString(m_NumberFormat & m_Precision)
                            'If CDbl(arr(i)) = 0 Then
                            '    tPrecision = 0
                            'Else
                            '    tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.EndValue))))
                            'End If
                            'If tPrecision < 0 Then tPrecision = 0
                            'brk.Caption &= Math.Round(CDbl(brk.EndValue), tPrecision)
                        End If
                        m_ColoringScheme.Add(brk)
                        prev = CDbl(brk.EndValue)
                    Next
                    ' now do the last one
                    brk = New MapWinGIS.ShapefileColorBreak
                    brk.StartValue = prev
                    brk.EndValue = arr(arr.Length - 1)
                    brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                    brk.EndColor = brk.StartColor
                    If CDbl(brk.EndValue) = CDbl(brk.StartValue) Then
                        brk.Caption = CStr(brk.StartValue)
                    Else
                        brk.Caption = CDbl(brk.StartValue).ToString(m_NumberFormat & m_Precision)
                        'If CDbl(arr(i)) = 0 Then
                        '    tPrecision = 0
                        'Else
                        '    tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.StartValue))))
                        'End If
                        'If tPrecision < 0 Then tPrecision = 0
                        'brk.Caption = CStr(Math.Round(CDbl(brk.StartValue), tPrecision)) & " - " & CStr(brk.EndValue)
                    End If
                    m_ColoringScheme.Add(brk)
                Else
                    ' The values are not all numeric
                    delta = CInt(Math.Round(arr.Length / numDesired))
                    For i = 0 To arr.Length - 1 Step delta
                        If i = arr.Length - 1 Then Exit For
                        brk = New MapWinGIS.ShapefileColorBreak
                        brk.StartValue = arr(i)
                        ' Now the fun begins.  Make sure I don't end in the middle of a group of a given value.
                        Dim offset1 As Integer = 0
                        Dim offset2 As Integer = 0
                        Dim endIndex As Integer = i + delta - 1
                        If endIndex >= arr.Length Then endIndex = arr.Length - 1

                        While (endIndex + offset1 < arr.Length) AndAlso (CStr(arr(endIndex + offset1)) = CStr(arr(endIndex)))
                            offset1 += 1
                        End While

                        While (endIndex + offset2 > 0) AndAlso (CStr(arr(endIndex + offset2)) = CStr(arr(endIndex)))
                            offset2 -= 1
                        End While

                        ' find the minimum distance to another value
                        If Math.Abs(offset1) > Math.Abs(offset2) Then
                            offset1 = offset2
                        End If
                        If (endIndex + offset1 >= arr.Length) Then offset1 = arr.Length - endIndex - 1
                        brk.EndValue = arr(endIndex + offset1)
                        brk.StartColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                        brk.EndColor = brk.StartColor
                        If CStr(brk.EndValue) = CStr(brk.StartValue) Then
                            brk.Caption = CStr(brk.StartValue)
                        Else
                            brk.Caption = CStr(brk.StartValue) & " - " & CStr(brk.EndValue)
                        End If
                        m_ColoringScheme.Add(brk)
                    Next
                End If
            End If

        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        End Try
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
 
        ApplyChanges()

    End Sub

    Private Sub ApplyChanges()

        ' 5/3/2010 Cho and DK -- If pointimagescheme exists and there was a change in color scheme,
        ' ask user if they want to clear point image scheme.

        If (frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageScheme IsNot Nothing _
        OrElse frmMain.Layers(frmMain.Layers.CurrentLayer).PointType = MapWinGIS.tkPointType.ptUserDefined) _
            AndAlso Object.Equals(m_ColoringScheme, m_OriginalScheme) = False Then


            Dim MsgBoxAnswer As MsgBoxResult = _
                    MsgBox("Icon symbology (point image scheme) exists." + ControlChars.NewLine + _
                    "If you apply this change, the existing icon symbology will be deleted." + ControlChars.NewLine + ControlChars.NewLine + _
                    "Would you like to go ahead?", MsgBoxStyle.YesNo, "Icon Symbology Warning")
            If MsgBoxAnswer = MsgBoxResult.Yes Then
                frmMain.MapMain.ClearUDPointImageList(frmMain.Layers.CurrentLayer)
                frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageScheme = Nothing
                For i As Integer = 0 To frmMain.Layers(frmMain.Layers.CurrentLayer).Shapes.NumShapes - 1
                    'Set back to force reset
                    frmMain.MapMain.set_ShapePointType(frmMain.Layers.CurrentLayer, i, frmMain.m_layers(frmMain.Layers.CurrentLayer).PointType)
                    frmMain.MapMain.set_ShapePointSize(frmMain.Layers.CurrentLayer, i, frmMain.m_layers(frmMain.Layers.CurrentLayer).LineOrPointSize)
                Next
                frmMain.Layers(frmMain.Layers.CurrentLayer).PointType = MapWinGIS.tkPointType.ptSquare
                frmMain.Layers(frmMain.Layers.CurrentLayer).LineOrPointSize = 12

                frmMain.MapMain.Redraw()
                frmMain.MapMain.Refresh()
            Else
                Exit Sub
            End If
        End If


        'Set the fill color to the existing fill color, to force the map to draw in any fills not covered by the above scheme.
        frmMain.Layers(frmMain.Layers.CurrentLayer).Color = frmMain.Layers(frmMain.Layers.CurrentLayer).Color

        CopyScheme(m_ColoringScheme, m_OriginalScheme)
        frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme = m_OriginalScheme
        frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).ColorSchemeFieldCaption = txtCaption.Text
        frmMain.Layers(frmMain.Layers.CurrentLayer).HatchingRecalculate() 'Reapply this if needed
        cmdApply.Enabled = False
        frmMain.Plugins.BroadcastMessage("ColoringSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
    End Sub

    Private Sub DataChanged()
        cmdApply.Enabled = True
        RangeFromScheme()
        Preview()
    End Sub

    Private Sub ColoringSchemeViewer1_DataChanged() Handles ColoringSchemeViewer1.DataChanged
        DataChanged()
    End Sub

    Private Sub ToolStrip1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Select Case e.ClickedItem.Name
            Case "tbAdd"
                If m_ColoringScheme Is Nothing Then Exit Sub

                Dim b As New MapWinGIS.ShapefileColorBreak
                b.Caption = ""
                b.StartColor = MapWinUtility.Colors.ColorToUInteger(System.Drawing.Color.Black)
                b.EndColor = b.StartColor
                b.StartValue = 0
                b.EndValue = b.StartValue

                m_ColoringScheme.Add(b)
                ColoringSchemeViewer1.ClearSelectedBreaks()
                ColoringSchemeViewer1.Refresh()
                DataChanged()

            Case "tbSub"
                Dim i As Integer

                For i = ColoringSchemeViewer1.SelectedBreaks.Length - 1 To 0 Step -1
                    If ColoringSchemeViewer1.SelectedBreaks(i) Then m_ColoringScheme.Remove(i)
                Next
                ColoringSchemeViewer1.ClearSelectedBreaks()
                ColoringSchemeViewer1.Refresh()
                DataChanged()

            Case "tbExport"
                Dim dlg As New SaveFileDialog
                dlg.DefaultExt = "mwleg"
                dlg.Filter = "MapWindow Coloring Schemes (*.mwleg)|*.mwleg"
                dlg.CheckPathExists = True
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    ColoringSchemeTools.ExportScheme(frmMain.Layers(frmMain.Layers.CurrentLayer), dlg.FileName)
                End If

            Case "tbImport"
                Dim dlg As New OpenFileDialog
                Dim scheme As Object

                dlg.DefaultExt = "mwleg"
                dlg.Filter = "MapWindow Coloring Schemes (*.mwleg)|*.mwleg"
                dlg.Multiselect = False
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    scheme = ColoringSchemeTools.ImportScheme(frmMain.Layers(frmMain.Layers.CurrentLayer), dlg.FileName)
                    If Not scheme Is Nothing Then
                        If TypeOf scheme Is MapWinGIS.ShapefileColorScheme Then
                            m_ColoringScheme = CType(scheme, MapWinGIS.ShapefileColorScheme)
                            Me.ColoringSchemeViewer1.m_SFColoringScheme = m_ColoringScheme
                            If cmbField.Items.Count >= m_ColoringScheme.FieldIndex Then
                                cmbField.SelectedIndex = m_ColoringScheme.FieldIndex
                            End If
                            ColoringSchemeViewer1.ClearSelectedBreaks()
                            Me.ColoringSchemeViewer1.Refresh()
                        End If
                    End If
                End If
                DataChanged()
                ' Case Wizard and Wizard subitems -- these are handled on individual context menu click events below
        End Select
    End Sub

    Private Sub mnuContinuousRamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContinuousRamp.Click
        Dim dlg As New RampDialog
        If dlg.ShowDialog() = DialogResult.OK Then
            MakeContinuousRamp(dlg.GetValues.NumBreaks, dlg.GetValues.StartColor, dlg.GetValues.EndColor)
            ColoringSchemeViewer1.ClearSelectedBreaks()
            ColoringSchemeViewer1.Refresh()
        End If
        DataChanged()
    End Sub

    Private Sub mnuEqualBreaks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEqualBreaks.Click
        Dim numBreaks As Integer
        Dim resString As String
        resString = InputBox("How many breaks?", "Input number of breaks...", CStr(m_ColoringScheme.NumBreaks))
        If resString Is Nothing OrElse resString.Length = 0 OrElse (Not IsNumeric(resString)) Then resString = "0"
        numBreaks = CInt(resString)
        If numBreaks <= 0 Then Exit Sub
        MakeEqualBreaks(numBreaks)
        ColoringSchemeViewer1.ClearSelectedBreaks()
        ColoringSchemeViewer1.Refresh()
        DataChanged()
    End Sub

    Private Sub mnuUniqueValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUniqueValues.Click
        GetUniqueBreaks()
        ColoringSchemeViewer1.Refresh()
        DataChanged()
    End Sub

    Private Sub cmbNumberFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNumberFormat.SelectedIndexChanged
        'E for scientific notation,
        'F for standard number, fixed decimal places
        'N for standard number, no rounding
        'G for auto
        Select Case cmbNumberFormat.Text
            Case "Automatic (Shortest Text)"
                m_NumberFormat = "G"
            Case "Decimals, Rounded"
                m_NumberFormat = "F"
            Case "Decimals, Not Rounded"
                m_NumberFormat = "N"
            Case "Scientific Notation"
                m_NumberFormat = "E"
        End Select
    End Sub

    Private Sub RangeFromScheme()
        Try
            If RangeColors.HistogramMaxPerBin < 1 Then
                'Dim lMin, lMax As Object
                '         GetFieldMinMax(lMin, lMax)
                RangeColors.HistogramFromValues(GetFieldValues)
            End If
            Dim lastBreak As Integer = m_ColoringScheme.NumBreaks - 1
            Dim lColors(lastBreak + 1) As Color
            Dim lValues(lastBreak + 1) As Double
            Dim lBreak As MapWinGIS.ShapefileColorBreak = m_ColoringScheme.ColorBreak(0)

            For lBreakIndex As Integer = 0 To lastBreak
                lBreak = m_ColoringScheme.ColorBreak(lBreakIndex)
                lValues(lBreakIndex) = CDbl(lBreak.StartValue)
                lColors(lBreakIndex) = MapWinUtility.Colors.IntegerToColor(lBreak.StartColor)
            Next
            lValues(lastBreak + 1) = CDbl(lBreak.EndValue)
            lColors(lastBreak + 1) = MapWinUtility.Colors.IntegerToColor(lBreak.EndColor)

            RangeColors.SetHandles(lValues, lColors)
            RangeColors.Visible = True
            RangeColors.Refresh()
        Catch e As Exception
            RangeColors.Visible = False
        End Try
    End Sub

    Private Sub RangeColors_UserDraggedHandle(ByVal aHandle As Integer, ByVal aValue As Double) Handles RangeColors.UserDraggedHandle
        cmdApply.Enabled = True
    End Sub

    Private Sub RangeColors_UserDraggingHandle(ByVal aHandle As Integer, ByVal aValue As Double) Handles RangeColors.UserDraggingHandle
        aValue = CLng(aValue * 100) / 100          'TODO: round with significant digits
        'TODO: change caption of break if it is numeric
        m_ColoringScheme.ColorBreak(aHandle).StartValue = aValue
        m_ColoringScheme.ColorBreak(aHandle - 1).EndValue = aValue
        ColoringSchemeViewer1.ClearSelectedBreaks()
        ColoringSchemeViewer1.Refresh()
        Preview()
    End Sub

    Private Sub Preview()
        If chkPreview.Checked Then
            'Set the fill color to the existing fill color, to force the map to draw in any fills not covered by the above scheme.
            frmMain.Layers(frmMain.Layers.CurrentLayer).Color = frmMain.Layers(frmMain.Layers.CurrentLayer).Color
            frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme = m_ColoringScheme
            frmMain.Layers(frmMain.Layers.CurrentLayer).HatchingRecalculate() 'Reapply this if needed
            frmMain.Plugins.BroadcastMessage("ColoringSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
            'frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme = m_OriginalScheme
        End If
    End Sub

    Private Function GetFieldValues() As Double()
        Dim sf As MapWinGIS.Shapefile
        Dim i, fld As Integer
        Dim lValues() As Double = Nothing
        Try
            fld = m_ColoringScheme.FieldIndex
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
            While m_ColoringScheme.NumBreaks > 0
                m_ColoringScheme.Remove(0)
            End While
            ColoringSchemeViewer1.ClearSelectedBreaks()
            ColoringSchemeViewer1.Refresh()
            DataChanged()
        End If
    End Sub

    Private Sub tbbMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbMoveUp.Click
        'Note "To 1" -- we don't want to let people move top item up
        For i As Integer = ColoringSchemeViewer1.SelectedBreaks.Length - 1 To 1 Step -1
            If ColoringSchemeViewer1.SelectedBreaks(i) Then
                Dim br As MapWinGIS.ShapefileColorBreak = m_ColoringScheme.ColorBreak(i)
                m_ColoringScheme.Remove(i)
                m_ColoringScheme.InsertAt(i - 1, br)
                ColoringSchemeViewer1.ClearSelectedBreaks()
                ColoringSchemeViewer1.Refresh()
                ColoringSchemeViewer1.SetSelectedBreak(i - 1, True)
                DataChanged()
                Return
            End If
        Next
    End Sub

    Private Sub tbbMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbMoveDown.Click
        'Note Length - 2 -- we don't want to let people move bottom item down
        For i As Integer = ColoringSchemeViewer1.SelectedBreaks.Length - 2 To 0 Step -1
            If ColoringSchemeViewer1.SelectedBreaks(i) Then
                Dim br As MapWinGIS.ShapefileColorBreak = m_ColoringScheme.ColorBreak(i)
                m_ColoringScheme.Remove(i)
                m_ColoringScheme.InsertAt(i + 1, br)
                ColoringSchemeViewer1.ClearSelectedBreaks()
                ColoringSchemeViewer1.Refresh()
                ColoringSchemeViewer1.SetSelectedBreak(i + 1, True)
                DataChanged()
                Return
            End If
        Next
    End Sub
End Class