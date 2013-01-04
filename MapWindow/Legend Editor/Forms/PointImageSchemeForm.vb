'Multiple icons on a single point layer
'Added by Chris Michaelis 11/12/2006

Imports System.Windows.Forms.Design

Friend Class PointImageSchemeForm
    Inherits System.Windows.Forms.Form

    Private m_Provider As IWindowsFormsEditorService
    Private m_ImageScheme As MapWindow.Interfaces.ShapefilePointImageScheme
    Private Loading As Boolean = True
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdClear As System.Windows.Forms.Button

    Friend WithEvents itms As System.Windows.Forms.TableLayoutPanel

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal Scheme As MapWindow.Interfaces.ShapefilePointImageScheme)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
        m_ImageScheme = Scheme
        '20 August 2009 Paul Meems - Load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new
        Loading = True
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents WizardMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuContinuousRamp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEqualBreaks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUniqueValues As System.Windows.Forms.ToolStripMenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PointImageSchemeForm))
        Me.lblField = New System.Windows.Forms.Label
        Me.cmbField = New System.Windows.Forms.ComboBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.WizardMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuContinuousRamp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEqualBreaks = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUniqueValues = New System.Windows.Forms.ToolStripMenuItem
        Me.itms = New System.Windows.Forms.TableLayoutPanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdClear = New System.Windows.Forms.Button
        Me.WizardMenu.SuspendLayout()
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
        'WizardMenu
        '
        Me.WizardMenu.AccessibleDescription = Nothing
        Me.WizardMenu.AccessibleName = Nothing
        resources.ApplyResources(Me.WizardMenu, "WizardMenu")
        Me.WizardMenu.BackgroundImage = Nothing
        Me.WizardMenu.Font = Nothing
        Me.WizardMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuContinuousRamp, Me.mnuEqualBreaks, Me.mnuUniqueValues})
        Me.WizardMenu.Name = "WizardMenu"
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
        'itms
        '
        Me.itms.AccessibleDescription = Nothing
        Me.itms.AccessibleName = Nothing
        resources.ApplyResources(Me.itms, "itms")
        Me.itms.BackgroundImage = Nothing
        Me.itms.CausesValidation = False
        Me.itms.Font = Nothing
        Me.itms.Name = "itms"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'cmdClear
        '
        Me.cmdClear.AccessibleDescription = Nothing
        Me.cmdClear.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdClear, "cmdClear")
        Me.cmdClear.BackgroundImage = Nothing
        Me.cmdClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClear.Font = Nothing
        Me.cmdClear.Name = "cmdClear"
        '
        'PointImageSchemeForm
        '
        Me.AcceptButton = Me.cmdOK
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.cmdClear
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.itms)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmbField)
        Me.Controls.Add(Me.lblField)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Nothing
        Me.Name = "PointImageSchemeForm"
        Me.WizardMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub ColoringSchemeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBoxes()
    End Sub

    Private Sub cmbField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbField.SelectedIndexChanged
        If Loading Then Exit Sub

        m_ImageScheme.FieldIndex = cmbField.SelectedIndex

        Dim sf As MapWinGIS.Shapefile
        Dim i As Integer
        Dim vals As New ArrayList

        sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then
            g_error = "Failed to get Shapefile object"
            Exit Sub
        End If

        For i = 0 To sf.NumShapes - 1
            Dim v As String = sf.CellValue(cmbField.SelectedIndex, i).ToString().Trim()
            If Not vals.Contains(v) Then vals.Add(v)
        Next

        vals.Sort()

        If vals.Count > 30 Then
            If MapWinUtility.Logger.Msg("Warning: There are more than 30 unique values. Continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Many Unique Values") = MsgBoxResult.No Then Exit Sub
        End If

        itms.Controls.Clear()
        itms.RowCount = 1
        GC.Collect()

        itms.VerticalScroll.Enabled = True
        itms.HorizontalScroll.Enabled = False

        Me.Cursor = Cursors.WaitCursor
        itms.Visible = False

        Dim first As Boolean = True
        For Each s As String In vals
            If first Then
                first = False
            Else
                itms.RowCount += 1
                itms.RowStyles.Add(New System.Windows.Forms.RowStyle(SizeType.AutoSize, 18))
            End If

            Dim newlbl As New Label
            newlbl.Text = s
            newlbl.AutoSize = True
            newlbl.Width = 200
            itms.Controls.Add(newlbl, 0, itms.RowCount - 1)

            Dim newpb As New PictureBox
            Dim imgIdx As Integer = m_ImageScheme.GetImageIndex(s)
            If Not imgIdx = -1 Then
                Dim g As MapWinGIS.Image
                Dim imgcvter As New MapWinUtility.ImageUtils

                g = frmMain.MapMain.get_UDPointImageListItem(frmMain.Layers.CurrentLayer, imgIdx)

                Dim img As System.Drawing.Image = imgcvter.IPictureDispToImage(g.Picture)
                newpb.Image = img
            ElseIf Not frmMain.Layers.Item(frmMain.Layers.CurrentLayer).UserPointType Is Nothing Then
                Dim g As MapWinGIS.Image
                Dim imgcvter As New MapWinUtility.ImageUtils

                g = frmMain.Layers.Item(frmMain.Layers.CurrentLayer).UserPointType
                Dim img As System.Drawing.Image = imgcvter.IPictureDispToImage(g.Picture)
                newpb.Image = img
            End If
            If newpb.Image Is Nothing Then
                newpb.Width = 1
                newpb.Height = 1
            Else
                newpb.SizeMode = PictureBoxSizeMode.AutoSize
            End If
            newpb.Tag = s
            itms.Controls.Add(newpb, 1, itms.RowCount - 1)

            Dim newll As New LinkLabel
            newll.Height = 18
            newll.Width = 30
            newll.Text = "Select..."
            newll.AutoSize = True
            newll.Tag = s
            AddHandler newll.Click, AddressOf SelectItem
            itms.Controls.Add(newll, 2, itms.RowCount - 1)
        Next

        Label1.Visible = False
        itms.Visible = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SelectItem(ByVal sender As Object, ByVal e As EventArgs)
        Dim tag As String = CType(sender, LinkLabel).Tag
        If tag = "" Then Return

        Dim selectForm As New PointImageSelect()
        If selectForm.ShowDialog() = DialogResult.OK Then

            ' 5/3/2010 DK == Pointimage scheme and coloringscheme cannot coexist.
            ' Therefore, remove coloringscheme.

            If frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme IsNot Nothing Then
                Dim MsgBoxAnswer As MsgBoxResult = _
                    MsgBox("Coloring scheme already exists." + ControlChars.NewLine + _
                    "If you apply this change, the existing coloring scheme will be deleted." + ControlChars.NewLine + ControlChars.NewLine + _
                    "Would you like to go ahead?", MsgBoxStyle.YesNo, "Coloring scheme warning")

                If MsgBoxAnswer = MsgBoxResult.Yes Then
                    frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme = Nothing
                End If

                frmMain.MapMain.Redraw()
                frmMain.MapMain.Refresh()
                end if


                If selectForm.chbHidePoint.Checked Then
                    m_ImageScheme.SetVisible(tag, False)

                    Dim sf As MapWinGIS.Shapefile

                    sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
                    If sf Is Nothing Then
                        g_error = "Failed to get Shapefile object"
                        Exit Sub
                    End If

                    For j As Integer = 0 To sf.NumShapes - 1
                        If sf.CellValue(m_ImageScheme.FieldIndex, j) = tag Then
                            frmMain.MapMain.set_ShapeVisible(frmMain.Layers.CurrentLayer, j, False)
                        End If
                    Next j
                    sf = Nothing
                Else
                    Dim newIdx As Integer = selectForm.newidx
                    If newIdx = -1 Then Return
                    m_ImageScheme.SetImageIndex(tag, newIdx)

                    Dim sf As MapWinGIS.Shapefile

                    sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
                    If sf Is Nothing Then
                        g_error = "Failed to get Shapefile object"
                        Exit Sub
                    End If

                    For j As Integer = 0 To sf.NumShapes - 1
                        If sf.CellValue(m_ImageScheme.FieldIndex, j) = tag Then
                            frmMain.MapMain.set_ShapePointImageListID(frmMain.Layers.CurrentLayer, j, newIdx)
                            If Not frmMain.MapMain.get_ShapePointType(frmMain.Layers.CurrentLayer, j) = MapWinGIS.tkPointType.ptImageList Then
                                frmMain.MapMain.set_ShapePointType(frmMain.Layers.CurrentLayer, j, MapWinGIS.tkPointType.ptImageList)
                            End If
                            frmMain.MapMain.set_ShapePointSize(frmMain.Layers.CurrentLayer, j, 1)
                        End If
                    Next j

                    itms.Visible = False
                    Try
                        Dim img As System.Drawing.Image = Nothing
                        For j As Integer = 0 To itms.Controls.Count - 1
                            If TypeOf (itms.Controls(j)) Is PictureBox Then
                                If itms.Controls(j).Tag = tag Then
                                    If img Is Nothing Then
                                        Dim g As MapWinGIS.Image = Nothing
                                        Dim imgcvter As New MapWinUtility.ImageUtils
                                        g = frmMain.MapMain.get_UDPointImageListItem(frmMain.Layers.CurrentLayer, newIdx)
                                        img = imgcvter.IPictureDispToImage(g.Picture)
                                    End If

                                    If Not img Is Nothing Then
                                        CType(itms.Controls(j), PictureBox).Image = img
                                        CType(itms.Controls(j), PictureBox).SizeMode = PictureBoxSizeMode.AutoSize
                                    End If
                                End If
                            End If
                        Next
                    Catch ex As Exception
                        ShowError(ex)
                    End Try

                    itms.Visible = True

                    sf = Nothing
                End If
            End If
    End Sub

    Private Sub PopulateComboBoxes()
        Dim sf As MapWinGIS.Shapefile

        sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then
            g_error = "Failed to get Shapefile object"
            Exit Sub
        End If
        For i As Integer = 0 To sf.NumFields - 1
            cmbField.Items.Add(sf.Field(i).Name)
        Next

        cmbField.SelectedIndex = -1

        Loading = False
        If Not m_ImageScheme.FieldIndex = -1 Then cmbField.SelectedIndex = m_ImageScheme.FieldIndex

        sf = Nothing
    End Sub

    Public ReadOnly Property Retval() As MapWindow.Interfaces.ShapefilePointImageScheme
        Get
            Return m_ImageScheme
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = DialogResult.OK

        If Me.cmbField.SelectedText = "" Then
            Exit Sub
        End If

        '5/3/2010 DK
        frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageScheme = m_ImageScheme
        frmMain.Legend.Layers.ItemByHandle(frmMain.Layers.CurrentLayer).PointImageFieldCaption = cmbField.Items(cmbField.SelectedIndex).ToString
        frmMain.Plugins.BroadcastMessage("PointImageSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Dim sf As MapWinGIS.Shapefile

        sf = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).GetObject, MapWinGIS.Shapefile)
        If sf Is Nothing Then
            g_error = "Failed to get Shapefile object"
            Exit Sub
        End If

        frmMain.MapMain.ClearUDPointImageList(frmMain.Layers.CurrentLayer)

        For i As Integer = 0 To sf.NumShapes - 1
            'Set back to force reset
            frmMain.MapMain.set_ShapePointType(frmMain.Layers.CurrentLayer, i, frmMain.m_layers(frmMain.Layers.CurrentLayer).PointType)
            frmMain.MapMain.set_ShapePointSize(frmMain.Layers.CurrentLayer, i, frmMain.m_layers(frmMain.Layers.CurrentLayer).LineOrPointSize)
        Next

        frmMain.MapMain.Redraw()
        frmMain.MapMain.Refresh()

        sf = Nothing

        If Not m_ImageScheme.FieldIndex = -1 Then cmbField.SelectedIndex = m_ImageScheme.FieldIndex

        If Not m_ImageScheme Is Nothing Then m_ImageScheme.Clear()
        m_ImageScheme = Nothing
        Me.DialogResult = DialogResult.OK
        GC.Collect()
        Me.Hide()
    End Sub
End Class