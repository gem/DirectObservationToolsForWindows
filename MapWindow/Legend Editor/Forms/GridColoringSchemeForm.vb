'********************************************************************************************************
'File Name: frmMain.vb
'Description: Main GUI interface for the MapWindow application.
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
'8/29/2005 - Lailin Chen - Fixed the problem of empty form(There was no control on the form when shown)
'10/1/2008 - Earljon Hidalgo - Modify menu icons for toolstrip. Icons provided by famfamfam
'********************************************************************************************************


Imports System.Windows.Forms.Design

Friend Class GridColoringSchemeForm
    Inherits System.Windows.Forms.Form

    Private m_Provider As IWindowsFormsEditorService
    Private m_OriginalScheme As MapWinGIS.GridColorScheme
    Private m_ColoringScheme As MapWinGIS.GridColorScheme
    'Friend m_SelectedBreaks() As Boolean
    Private m_Precision As Integer = 3
    Friend WithEvents ContinuousRampToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EqualBreaksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UniqueValuesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeadSea As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Desert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FallLeaves As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Glaciers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Highway As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Meadow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SummerMountains As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValleyFires As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tbMoveUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbMoveDown As System.Windows.Forms.ToolStripButton
    Private m_NumberFormat As String

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal ColoringScheme As MapWinGIS.GridColorScheme)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
        m_OriginalScheme = ColoringScheme
        CopyScheme(ColoringScheme, m_ColoringScheme)
        ColoringSchemeViewer1.ClearSelectedBreaks()
        Me.ColoringSchemeViewer1.InitializeControl(m_ColoringScheme)
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
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents ColoringSchemeViewer1 As MapWindow.ColoringSchemeViewer
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents chkComputeHillshade As System.Windows.Forms.CheckBox
    Friend WithEvents cmbGradientStyle As System.Windows.Forms.ComboBox
    Friend WithEvents btnAdvanced As System.Windows.Forms.Button
    Friend WithEvents lblGradientStyle As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tbImport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbRemove As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbWizard As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents cmbNumberFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GridColoringSchemeForm))
        Me.cmdApply = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.chkComputeHillshade = New System.Windows.Forms.CheckBox
        Me.cmbGradientStyle = New System.Windows.Forms.ComboBox
        Me.btnAdvanced = New System.Windows.Forms.Button
        Me.lblGradientStyle = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tbImport = New System.Windows.Forms.ToolStripButton
        Me.tbExport = New System.Windows.Forms.ToolStripButton
        Me.tbAdd = New System.Windows.Forms.ToolStripButton
        Me.tbRemove = New System.Windows.Forms.ToolStripButton
        Me.tbWizard = New System.Windows.Forms.ToolStripDropDownButton
        Me.ContinuousRampToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EqualBreaksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UniqueValuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.DeadSea = New System.Windows.Forms.ToolStripMenuItem
        Me.Desert = New System.Windows.Forms.ToolStripMenuItem
        Me.FallLeaves = New System.Windows.Forms.ToolStripMenuItem
        Me.Glaciers = New System.Windows.Forms.ToolStripMenuItem
        Me.Highway = New System.Windows.Forms.ToolStripMenuItem
        Me.Meadow = New System.Windows.Forms.ToolStripMenuItem
        Me.SummerMountains = New System.Windows.Forms.ToolStripMenuItem
        Me.ValleyFires = New System.Windows.Forms.ToolStripMenuItem
        Me.tbMoveUp = New System.Windows.Forms.ToolStripButton
        Me.tbMoveDown = New System.Windows.Forms.ToolStripButton
        Me.cmbNumberFormat = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ColoringSchemeViewer1 = New MapWindow.ColoringSchemeViewer
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdApply
        '
        resources.ApplyResources(Me.cmdApply, "cmdApply")
        Me.cmdApply.Name = "cmdApply"
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        '
        'chkComputeHillshade
        '
        resources.ApplyResources(Me.chkComputeHillshade, "chkComputeHillshade")
        Me.chkComputeHillshade.Name = "chkComputeHillshade"
        '
        'cmbGradientStyle
        '
        resources.ApplyResources(Me.cmbGradientStyle, "cmbGradientStyle")
        Me.cmbGradientStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGradientStyle.Items.AddRange(New Object() {resources.GetString("cmbGradientStyle.Items"), resources.GetString("cmbGradientStyle.Items1"), resources.GetString("cmbGradientStyle.Items2")})
        Me.cmbGradientStyle.Name = "cmbGradientStyle"
        '
        'btnAdvanced
        '
        resources.ApplyResources(Me.btnAdvanced, "btnAdvanced")
        Me.btnAdvanced.Name = "btnAdvanced"
        '
        'lblGradientStyle
        '
        resources.ApplyResources(Me.lblGradientStyle, "lblGradientStyle")
        Me.lblGradientStyle.Name = "lblGradientStyle"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbImport, Me.tbExport, Me.tbAdd, Me.tbRemove, Me.tbWizard, Me.tbMoveUp, Me.tbMoveDown})
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'tbImport
        '
        Me.tbImport.Image = Global.MapWindow.GlobalResource.imgFolder
        Me.tbImport.Name = "tbImport"
        resources.ApplyResources(Me.tbImport, "tbImport")
        Me.tbImport.Tag = "tbImport"
        '
        'tbExport
        '
        Me.tbExport.Image = Global.MapWindow.GlobalResource.imgSave
        Me.tbExport.Name = "tbExport"
        resources.ApplyResources(Me.tbExport, "tbExport")
        Me.tbExport.Tag = "tbExport"
        '
        'tbAdd
        '
        Me.tbAdd.Image = Global.MapWindow.GlobalResource.imgAdd
        Me.tbAdd.Name = "tbAdd"
        resources.ApplyResources(Me.tbAdd, "tbAdd")
        Me.tbAdd.Tag = "tbAdd"
        '
        'tbRemove
        '
        Me.tbRemove.Image = Global.MapWindow.GlobalResource.imgDelete
        Me.tbRemove.Name = "tbRemove"
        resources.ApplyResources(Me.tbRemove, "tbRemove")
        Me.tbRemove.Tag = "tbRemove"
        '
        'tbWizard
        '
        Me.tbWizard.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContinuousRampToolStripMenuItem, Me.EqualBreaksToolStripMenuItem, Me.UniqueValuesToolStripMenuItem, Me.ToolStripMenuItem4})
        Me.tbWizard.Image = Global.MapWindow.GlobalResource.imgFlash
        Me.tbWizard.Name = "tbWizard"
        resources.ApplyResources(Me.tbWizard, "tbWizard")
        Me.tbWizard.Tag = "tbWizard"
        '
        'ContinuousRampToolStripMenuItem
        '
        Me.ContinuousRampToolStripMenuItem.Name = "ContinuousRampToolStripMenuItem"
        resources.ApplyResources(Me.ContinuousRampToolStripMenuItem, "ContinuousRampToolStripMenuItem")
        '
        'EqualBreaksToolStripMenuItem
        '
        Me.EqualBreaksToolStripMenuItem.Name = "EqualBreaksToolStripMenuItem"
        resources.ApplyResources(Me.EqualBreaksToolStripMenuItem, "EqualBreaksToolStripMenuItem")
        '
        'UniqueValuesToolStripMenuItem
        '
        Me.UniqueValuesToolStripMenuItem.Name = "UniqueValuesToolStripMenuItem"
        resources.ApplyResources(Me.UniqueValuesToolStripMenuItem, "UniqueValuesToolStripMenuItem")
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeadSea, Me.Desert, Me.FallLeaves, Me.Glaciers, Me.Highway, Me.Meadow, Me.SummerMountains, Me.ValleyFires})
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        resources.ApplyResources(Me.ToolStripMenuItem4, "ToolStripMenuItem4")
        '
        'DeadSea
        '
        Me.DeadSea.Name = "DeadSea"
        resources.ApplyResources(Me.DeadSea, "DeadSea")
        '
        'Desert
        '
        Me.Desert.Name = "Desert"
        resources.ApplyResources(Me.Desert, "Desert")
        '
        'FallLeaves
        '
        Me.FallLeaves.Name = "FallLeaves"
        resources.ApplyResources(Me.FallLeaves, "FallLeaves")
        '
        'Glaciers
        '
        Me.Glaciers.Name = "Glaciers"
        resources.ApplyResources(Me.Glaciers, "Glaciers")
        '
        'Highway
        '
        Me.Highway.Name = "Highway"
        resources.ApplyResources(Me.Highway, "Highway")
        '
        'Meadow
        '
        Me.Meadow.Name = "Meadow"
        resources.ApplyResources(Me.Meadow, "Meadow")
        '
        'SummerMountains
        '
        Me.SummerMountains.Name = "SummerMountains"
        resources.ApplyResources(Me.SummerMountains, "SummerMountains")
        '
        'ValleyFires
        '
        Me.ValleyFires.Name = "ValleyFires"
        resources.ApplyResources(Me.ValleyFires, "ValleyFires")
        '
        'tbMoveUp
        '
        Me.tbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbMoveUp.Image = Global.MapWindow.GlobalResource.imgUp
        resources.ApplyResources(Me.tbMoveUp, "tbMoveUp")
        Me.tbMoveUp.Name = "tbMoveUp"
        '
        'tbMoveDown
        '
        Me.tbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbMoveDown.Image = Global.MapWindow.GlobalResource.imgDown
        resources.ApplyResources(Me.tbMoveDown, "tbMoveDown")
        Me.tbMoveDown.Name = "tbMoveDown"
        '
        'cmbNumberFormat
        '
        resources.ApplyResources(Me.cmbNumberFormat, "cmbNumberFormat")
        Me.cmbNumberFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNumberFormat.Items.AddRange(New Object() {resources.GetString("cmbNumberFormat.Items"), resources.GetString("cmbNumberFormat.Items1"), resources.GetString("cmbNumberFormat.Items2"), resources.GetString("cmbNumberFormat.Items3")})
        Me.cmbNumberFormat.Name = "cmbNumberFormat"
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
        'ColoringSchemeViewer1
        '
        resources.ApplyResources(Me.ColoringSchemeViewer1, "ColoringSchemeViewer1")
        Me.ColoringSchemeViewer1.Name = "ColoringSchemeViewer1"
        '
        'GridColoringSchemeForm
        '
        Me.AcceptButton = Me.cmdApply
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbNumberFormat)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.ColoringSchemeViewer1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.chkComputeHillshade)
        Me.Controls.Add(Me.cmbGradientStyle)
        Me.Controls.Add(Me.btnAdvanced)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "GridColoringSchemeForm"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub CopyScheme(ByVal Original As MapWinGIS.GridColorScheme, ByRef NewScheme As MapWinGIS.GridColorScheme)
        If NewScheme Is Nothing Then NewScheme = New MapWinGIS.GridColorScheme()
        Dim i As Integer, brk As MapWinGIS.GridColorBreak
        While NewScheme.NumBreaks > 0
            NewScheme.DeleteBreak(0)
        End While
        For i = 0 To Original.NumBreaks - 1
            With Original.Break(i)
                brk = New MapWinGIS.GridColorBreak()
                brk.Caption = .Caption
                brk.ColoringType = .ColoringType
                brk.GlobalCallback = .GlobalCallback
                brk.GradientModel = .GradientModel
                brk.HighColor = .HighColor
                brk.HighValue = .HighValue
                brk.Key = .Key
                brk.LowColor = .LowColor
                brk.LowValue = .LowValue
            End With
            NewScheme.InsertBreak(brk)
            brk = Nothing
        Next
        NewScheme.Key = Original.Key
        NewScheme.AmbientIntensity = Original.AmbientIntensity
        NewScheme.GlobalCallback = Original.GlobalCallback
        NewScheme.Key = Original.Key
        NewScheme.SetLightSource(Original.LightSourceAzimuth, Original.LightSourceElevation)
        NewScheme.LightSourceIntensity = Original.LightSourceIntensity
        NewScheme.NoDataColor = Original.NoDataColor
    End Sub

    Private Sub ColoringSchemeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBoxes()
        cmbNumberFormat.SelectedIndex = 3 'Auto
    End Sub

    Private Sub PopulateComboBoxes()
        Dim grd As MapWinGIS.GridColorScheme

        grd = CType(frmMain.m_layers(frmMain.Layers.CurrentLayer).ColoringScheme, MapWinGIS.GridColorScheme)
        If grd Is Nothing Then
            g_error = "Failed to get Grid coloring scheme object"
            Exit Sub
        End If
        If grd.NumBreaks > 0 Then
            If cmbGradientStyle.Items.Contains(grd.Break(0).GradientModel.ToString) Then
                cmbGradientStyle.SelectedItem = grd.Break(0).GradientModel.ToString
            Else
                cmbGradientStyle.SelectedIndex = 0
            End If
        Else
            'cmbGradientStyle.Text = "Linear"
            cmbGradientStyle.SelectedIndex = 0
        End If
        If grd.Break(0).ColoringType = MapWinGIS.ColoringType.Hillshade Then
            chkComputeHillshade.Checked = True
        Else
            chkComputeHillshade.Checked = False
        End If
    End Sub

    Public ReadOnly Property Retval() As MapWinGIS.GridColorScheme
        Get
            CopyScheme(m_ColoringScheme, m_OriginalScheme)
            Return m_OriginalScheme
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If cmdApply.Enabled = True Then
            ApplyChanges()
        End If
        Me.DialogResult = DialogResult.OK
        'Me.Hide()
        Me.Dispose(False)
    End Sub

    Private Sub SetDefaultScheme(ByVal Value As MapWinGIS.PredefinedColorScheme)
        Dim grd As MapWinGIS.Grid = frmMain.Layers(frmMain.Layers.CurrentLayer).GetGridObject
        m_ColoringScheme.Clear()
        m_ColoringScheme.UsePredefined(Double.Parse(Convert.ToString(grd.Minimum)), Double.Parse(Convert.ToString(grd.Maximum)), Value)
        chkComputeHillshade.Checked = (m_ColoringScheme.Break(0).ColoringType = MapWinGIS.ColoringType.Hillshade)
        cmdApply.Enabled = True
        ColoringSchemeViewer1.Refresh()
        grd = Nothing
    End Sub

    Private Function GetUniqueBreaks(Optional ByVal randomColors As Boolean = False) As Boolean
        Dim grd As MapWinGIS.Grid
        Dim gradientModel As MapWinGIS.GradientModel
        Dim coloringType As MapWinGIS.ColoringType

        grd = frmMain.m_layers(frmMain.Layers.CurrentLayer).GetGridObject
        If grd Is Nothing Then
            g_error = "Failed to get Grid object"
            Return False
        End If

        Select Case Me.cmbGradientStyle.Text
            Case "Linear"
                gradientModel = MapWinGIS.GradientModel.Linear
            Case "Exponential"
                gradientModel = MapWinGIS.GradientModel.Exponential
            Case "Logorithmic"
                gradientModel = MapWinGIS.GradientModel.Logorithmic
        End Select

        If randomColors Then
            coloringType = MapWinGIS.ColoringType.Random
        Else
            If Me.chkComputeHillshade.Checked = True Then
                coloringType = MapWinGIS.ColoringType.Hillshade
            Else
                coloringType = MapWinGIS.ColoringType.Gradient
            End If
        End If

        Dim rt As Boolean = GetUniqueBreaks(grd, False, m_ColoringScheme, gradientModel, coloringType, m_NumberFormat, m_Precision)
        ColoringSchemeViewer1.ClearSelectedBreaks()
        Return rt
    End Function

    ''' <summary>
    ''' Creates new instance of grid color scheme and populates it with unique values
    ''' </summary>
    ''' <param name="grd"></param>
    ''' <param name="Silent"></param>
    ''' <param name="ColorScheme"></param>
    ''' <param name="gradientModel"></param>
    ''' <param name="coloringType"></param>
    ''' <param name="NumberFormat"></param>
    ''' <param name="Precision"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUniqueBreaks(ByRef grd As MapWinGIS.Grid, ByVal Silent As Boolean, ByRef ColorScheme As MapWinGIS.GridColorScheme, _
                                           ByVal gradientModel As MapWinGIS.GradientModel, ByVal coloringType As MapWinGIS.ColoringType, _
                                           ByVal NumberFormat As String, ByVal Precision As Integer) As Boolean
        Dim ht As New Hashtable
        Dim val As Object

        For i As Integer = 0 To grd.Header.NumberRows - 1
            For j As Integer = 0 To grd.Header.NumberCols - 1
                val = grd.Value(j, i)
                If ht.ContainsKey(val) = False Then
                    If Not Double.IsNaN(MapWinUtility.MiscUtils.ParseDouble(val, 0.0)) Then
                        ht.Add(val, val)
                        If ht.Count > 100 Then
                            If Not Silent Then
                                MapWinUtility.Logger.Msg("There are too many unique values to create this scheme.", _
                                MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Coloring Scheme Editor")
                            End If
                            Return False
                        Else
                            System.Diagnostics.Debug.Print("NaN value was found in grid")
                        End If
                    End If
                End If
            Next
        Next

        If ht.Count <= 1 Then Return False

        If ht.Count > 100 Then
            If Not Silent AndAlso MapWinUtility.Logger.Msg("There are " & ht.Count & " unique values.  Are you sure you want to continue?", _
                                  MsgBoxStyle.YesNo, "Continue?") = MsgBoxResult.No Then
                Return False
            End If
        End If

        If ColorScheme Is Nothing Then ColorScheme = New MapWinGIS.GridColorScheme()
        ColorScheme.Clear()

        Dim arr() As Object
        ReDim arr(ht.Count - 1)
        ht.Values().CopyTo(arr, 0)
        Array.Sort(arr)

        Dim noDataValue As Double = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(grd.Header.NodataValue), 0.0)
        Dim minValue As Double = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(0)), 0.0)
        Dim maxValue As Double = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(arr.Length - 1)), 0.0)

        If minValue = noDataValue Then
            minValue = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(1)), 0.0)
        End If

        If maxValue = noDataValue Then
            maxValue = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(arr.Length - 2)), 0.0)
        End If

        Dim scheme As New MapWinGIS.ColorScheme
        Dim rnd As New Random

        scheme.SetColors4(CType(rnd.Next(0, 7), MapWinGIS.PredefinedColorScheme))

        For i As Integer = 0 To arr.Length - 1
            'This must be double.parse(convert.tostring) so that it can handle SByte values also.
            If MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(i)), 0.0) <> noDataValue Then

                Dim brk As MapWinGIS.GridColorBreak = New MapWinGIS.GridColorBreak
                brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(i)), 0.0)
                brk.HighValue = brk.LowValue

                If maxValue = minValue Then
                    brk.LowColor = scheme.RandomColor(rnd.NextDouble())
                    brk.HighColor = brk.LowColor
                Else
                    Dim ratio As Double = 0.0
                    ratio = (brk.LowValue - minValue) / (maxValue - minValue)
                    If coloringType = MapWinGIS.ColoringType.Random Then
                        brk.LowColor = scheme.RandomColor(ratio)
                    Else
                        brk.LowColor = scheme.GraduatedColor(ratio)
                    End If

                    brk.HighColor = brk.LowColor
                End If

                If IsNumeric(arr(i)) Then
                    brk.Caption = Double.Parse(Convert.ToString(arr(i))).ToString(NumberFormat & Precision)
                    'brk.Caption = CDbl(arr(i)).ToString(m_NumberFormat & m_Precision)
                    '    Dim tPrecision As Integer = 0
                    '    If CDbl(arr(i)) <> 0 Then tPrecision = CInt(3 - Math.Log10(CDbl(arr(i))))
                    '    If tPrecision < 0 Then tPrecision = 0
                    '    brk.Caption = CStr(Math.Round(CDbl(arr(i)), tPrecision))
                End If
                brk.ColoringType = coloringType
                brk.GradientModel = gradientModel
                ColorScheme.InsertBreak(brk)
                brk = Nothing
            End If
        Next

        ht = Nothing
        Return True
    End Function

    Private Sub MakeEqualBreaks(ByVal numDesired As Integer)
        Dim grd As MapWinGIS.Grid
        Dim arr() As Object, i, j As Integer
        Dim ht As New Hashtable
        Dim brk As MapWinGIS.GridColorBreak
        Dim allNumeric As Boolean = True
        Dim val As Object
        Dim gradientModel As MapWinGIS.GradientModel
        Dim coloringType As MapWinGIS.ColoringType
        Dim startVal As Integer

        Try
            grd = frmMain.Layers(frmMain.Layers.CurrentLayer).GetGridObject
            If grd Is Nothing Then Exit Sub

            For i = 0 To grd.Header.NumberRows - 1
                For j = 0 To grd.Header.NumberCols - 1
                    val = grd.Value(j, i)
                    If Not ht.ContainsKey(val) Then
                        If Not Double.IsNaN(MapWinUtility.MiscUtils.ParseDouble(val, 0.0)) Then
                            ht.Add(val, val)
                        Else
                            System.Diagnostics.Debug.Print("NaN value was present in grid")
                        End If
                    End If
                Next
            Next

            ReDim arr(ht.Count - 1)
            ht.Values().CopyTo(arr, 0)
            Array.Sort(arr)

            While m_ColoringScheme.NumBreaks > 0
                m_ColoringScheme.DeleteBreak(0)
            End While

            Select Case Me.cmbGradientStyle.Text
                Case "Linear"
                    gradientModel = MapWinGIS.GradientModel.Linear
                Case "Exponential"
                    gradientModel = MapWinGIS.GradientModel.Exponential
                Case "Logorithmic"
                    gradientModel = MapWinGIS.GradientModel.Logorithmic
            End Select
            If Me.chkComputeHillshade.Checked = True Then
                coloringType = MapWinGIS.ColoringType.Hillshade
            Else
                coloringType = MapWinGIS.ColoringType.Gradient
            End If

            If ht.Keys.Count <= numDesired Then
                Dim brkArr() As Object
                ReDim brkArr(ht.Keys.Count - 1)
                ht.Keys.CopyTo(brkArr, 0)
                Array.Sort(brkArr)
                ' Changed this line to use Double.Parase(Convert.ToString so that
                ' it can handle SByte data types also:
                'startVal = CInt(IIf(CDbl(grd.Header.NodataValue) = CDbl(brkArr(0)), 1, 0))
                startVal = CInt(IIf(Double.Parse(Convert.ToString(grd.Header.NodataValue)) = Double.Parse(Convert.ToString(brkArr(0))), 1, 0))
                For i = startVal To brkArr.Length - 1
                    brk = New MapWinGIS.GridColorBreak
                    If IsNumeric(arr(i)) Then
                        'Caption is a string.
                        'Therefore, why convert to double just to convert back to string?
                        'brk.Caption = CDbl(arr(i)).ToString(m_NumberFormat & m_Precision)
                        brk.Caption = Convert.ToString(arr(i))
                        '    tPrecision = 0
                        '    If CDbl(arr(i)) <> 0 Then tPrecision = CInt(3 - Math.Log10(CDbl(arr(i))))
                        '    If tPrecision < 0 Then tPrecision = 0
                        '    brk.Caption = Math.Round(CDbl(arr(i)), tPrecision).ToString
                    End If
                    'Changed these from direct cast to parse. CDM 11/13/05
                    'brk.LowValue = CDbl(arr(i))
                    'brk.HighValue = CDbl(arr(i))
                    brk.LowValue = Double.Parse(Convert.ToString(arr(i)))
                    brk.HighValue = Double.Parse(Convert.ToString(arr(i)))
                    brk.LowColor = System.Convert.ToUInt32(RGB(CInt(Rnd() * 255), CInt(Rnd() * 255), CInt(Rnd() * 255)))
                    brk.HighColor = brk.LowColor
                    brk.ColoringType = coloringType
                    brk.GradientModel = gradientModel
                    m_ColoringScheme.InsertBreak(brk)
                Next
            Else
                Dim min As Double, max As Double, range As Double
                ' Changed this line to use Double.Parase(Convert.ToString so that
                ' it can handle SByte data types also:
                'startVal = CInt(IIf(CDbl(grd.Header.NodataValue) = CDbl(arr(0)), 1, 0))
                startVal = CInt(IIf(Double.Parse(Convert.ToString(grd.Header.NodataValue)) = Double.Parse(Convert.ToString(arr(0))), 1, 0))

                'min = CDbl(arr(startVal))
                'max = CDbl(arr(arr.Length() - 1))
                min = Double.Parse(Convert.ToString(arr(startVal)))
                max = Double.Parse(Convert.ToString(arr(arr.Length() - 1)))
                range = max - min

                Dim prev As Double = min
                Dim t As Double = range / numDesired

                For i = 1 To numDesired - 1
                    brk = New MapWinGIS.GridColorBreak
                    brk.LowValue = prev
                    brk.HighValue = prev + t
                    brk.LowColor = System.Convert.ToUInt32(Rnd() * Integer.MaxValue)
                    brk.HighColor = brk.LowColor
                    If brk.HighValue = brk.LowValue Then
                        If brk.LowValue = min Then
                            brk.Caption = CStr(min)
                        Else
                            brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                            'tPrecision = 0
                            'If CDbl(brk.LowValue) <> 0 Then tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                            'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                            'If tPrecision < 0 Then tPrecision = 0
                            'brk.Caption = CStr(Math.Round(CDbl(brk.LowValue), tPrecision))
                        End If
                    Else
                        If brk.LowValue = min Then
                            brk.Caption = CStr(min)
                        Else
                            brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                            'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                            'If tPrecision < 0 Then tPrecision = 0
                            'brk.Caption = CStr(Math.Round(CDbl(brk.LowValue), tPrecision))
                        End If
                        brk.Caption &= " - "
                        brk.Caption = brk.HighValue.ToString(m_NumberFormat & m_Precision)
                        'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.HighValue))))
                        'If tPrecision < 0 Then tPrecision = 0
                        'brk.Caption &= Math.Round(CDbl(brk.HighValue), tPrecision)
                    End If
                    brk.ColoringType = coloringType
                    brk.GradientModel = gradientModel
                    m_ColoringScheme.InsertBreak(brk)
                    prev = brk.HighValue
                Next
                ' now do the last one
                brk = New MapWinGIS.GridColorBreak
                brk.LowValue = prev
                brk.HighValue = max
                brk.LowColor = System.Convert.ToUInt32(Rnd() * Integer.MaxValue)
                brk.HighColor = brk.LowColor
                If brk.HighValue = brk.LowValue Then
                    brk.Caption = CStr(brk.LowValue)
                Else
                    brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                    'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                    'If tPrecision < 0 Then tPrecision = 0
                    'brk.Caption = Math.Round(CDbl(brk.LowValue), tPrecision) & " - " & brk.HighValue
                End If
                brk.ColoringType = coloringType
                brk.GradientModel = gradientModel
                m_ColoringScheme.InsertBreak(brk)
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
        Dim c As Cursor = Me.Cursor
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'Set the fill color to the existing fill color, to force the map to draw in any fills not covered by the above scheme.
        frmMain.Layers(frmMain.Layers.CurrentLayer).Color = frmMain.Layers(frmMain.Layers.CurrentLayer).Color

        CopyScheme(m_ColoringScheme, m_OriginalScheme)
        frmMain.Layers(frmMain.Layers.CurrentLayer).ColoringScheme = m_OriginalScheme
        Me.Cursor = c
        cmdApply.Enabled = False
        frmMain.Plugins.BroadcastMessage("ColoringSchemeChanged Handle=" + frmMain.Layers.CurrentLayer.ToString() + " Name=" + frmMain.Layers(frmMain.Layers.CurrentLayer).Name)
    End Sub

    Private Sub MakeContinuousRamp(ByVal numDesired As Integer, ByVal StartColor As Color, ByVal EndColor As Color)
        Dim grd As MapWinGIS.Grid
        Dim arr(), val As Object, i, j As Integer
        Dim ht As New Hashtable
        Dim brk As MapWinGIS.GridColorBreak
        Dim gradientModel As MapWinGIS.GradientModel
        Dim coloringType As MapWinGIS.ColoringType

        Try
            grd = frmMain.Layers(frmMain.Layers.CurrentLayer).GetGridObject
            If grd Is Nothing Then Exit Sub

            For i = 0 To grd.Header.NumberRows - 1
                For j = 0 To grd.Header.NumberCols - 1
                    val = grd.Value(j, i)
                    If ht.ContainsKey(val) = False Then
                        If Not Double.IsNaN(MapWinUtility.MiscUtils.ParseDouble(val, 0.0)) Then
                            ht.Add(val, val)
                        Else
                            System.Diagnostics.Debug.Print("NaN value was present in grid")
                        End If
                    End If
                Next
            Next

            ReDim arr(ht.Count - 1)
            ht.Values().CopyTo(arr, 0)
            Array.Sort(arr)

            While m_ColoringScheme.NumBreaks > 0
                m_ColoringScheme.DeleteBreak(0)
            End While

            Select Case Me.cmbGradientStyle.Text
                Case "Linear"
                    gradientModel = MapWinGIS.GradientModel.Linear
                Case "Exponential"
                    gradientModel = MapWinGIS.GradientModel.Exponential
                Case "Logorithmic"
                    gradientModel = MapWinGIS.GradientModel.Logorithmic
            End Select
            If Me.chkComputeHillshade.Checked = True Then
                coloringType = MapWinGIS.ColoringType.Hillshade
            Else
                coloringType = MapWinGIS.ColoringType.Gradient
            End If

            '' No Data break
            'brk = New MapWinGIS.GridColorBreak()
            'brk.Caption = "No Data"
            'brk.LowValue = grd.Header.NodataValue
            'brk.HighValue = brk.LowValue
            'brk.LowColor = MapWinUtility.Colors.ColorToUInteger(Color.Black)
            'brk.HighColor = brk.LowColor
            'brk.GradientModel = gradientModel
            'brk.ColoringType = coloringType
            'm_ColoringScheme.InsertBreak(brk)

            Dim sR, sG, sB As Integer
            Dim eR, eG, eB As Integer
            Dim r, g, b As Double
            Dim rStep, gStep, bStep As Double
            Dim startVal As Integer

            MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(StartColor), sR, sG, sB)
            MapWinUtility.Colors.GetRGB(MapWinUtility.Colors.ColorToInteger(EndColor), eR, eG, eB)

            r = sR
            g = sG
            b = sB

            If ht.Keys.Count <= numDesired Then
                Dim brkArr() As Object
                ReDim brkArr(ht.Keys.Count - 1)
                ht.Keys.CopyTo(brkArr, 0)
                Array.Sort(brkArr)

                rStep = (eR - sR) / brkArr.Length
                gStep = (eG - sG) / brkArr.Length
                bStep = (eB - sB) / brkArr.Length

                'This must be double.parse(convert.tostring) for handling of sbyte values - cdm 11/13/2005
                startVal = CInt(IIf(MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(grd.Header.NodataValue), 0.0) = _
                           MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(brkArr(0)), 0.0), 1, 0))
                'startVal = CInt(IIf(CDbl(grd.Header.NodataValue) = CDbl(brkArr(0)), 1, 0))
                For i = startVal To brkArr.Length - 1
                    brk = New MapWinGIS.GridColorBreak
                    If IsNumeric(brkArr(i)) Then
                        brk.Caption = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(brkArr(i)), 0.0).ToString(m_NumberFormat & m_Precision)
                        'brk.Caption = CDbl(brkArr(i)).ToString(m_NumberFormat & m_Precision)
                        'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(arr(i)))))
                        'If tPrecision < 0 Then tPrecision = 0
                        'brk.Caption = CStr(Math.Round(CDbl(brkArr(i)), tPrecision))
                    End If
                    brk.LowValue = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(brkArr(i)), 0.0)
                    brk.HighValue = brk.LowValue
                    brk.LowColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                    r += rStep
                    g += gStep
                    b += bStep
                    brk.HighColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                    brk.ColoringType = coloringType
                    brk.GradientModel = gradientModel
                    m_ColoringScheme.InsertBreak(brk)
                Next
            Else
                rStep = (eR - sR) / numDesired
                gStep = (eG - sG) / numDesired
                bStep = (eB - sB) / numDesired

                Dim min As Double, max As Double, range As Double
                Dim noDataValue As Double = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(grd.Header.NodataValue), 0.0)
                startVal = CInt(IIf(noDataValue = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(0)), 0.0), 1, 0))

                min = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(startVal)), 0.0)
                max = MapWinUtility.MiscUtils.ParseDouble(Convert.ToString(arr(arr.Length() - 1)), 0.0)
                range = max - min

                Dim prev As Double = min
                Dim t As Double = range / numDesired

                For i = 1 To numDesired - 1
                    brk = New MapWinGIS.GridColorBreak
                    brk.LowValue = prev
                    brk.HighValue = prev + t
                    brk.LowColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                    r += rStep
                    g += gStep
                    b += bStep
                    brk.HighColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                    If brk.HighValue = brk.LowValue Then
                        If brk.LowValue = min Then
                            brk.Caption = CStr(min)
                        Else
                            brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                            'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                            'If tPrecision < 0 Then tPrecision = 0
                            'brk.Caption = CStr(Math.Round(CDbl(brk.LowValue), tPrecision))
                        End If
                    Else
                        If brk.LowValue = min Then
                            brk.Caption = CStr(min)
                        Else
                            brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                            'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                            'If tPrecision < 0 Then tPrecision = 0
                            'brk.Caption = CStr(Math.Round(CDbl(brk.LowValue), tPrecision))
                        End If
                        brk.Caption &= " - "
                        brk.Caption = brk.HighValue.ToString(m_NumberFormat & m_Precision)
                        'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.HighValue))))
                        'If tPrecision < 0 Then tPrecision = 0
                        'brk.Caption &= Math.Round(CDbl(brk.HighValue), tPrecision)
                    End If
                    brk.ColoringType = coloringType
                    brk.GradientModel = gradientModel
                    m_ColoringScheme.InsertBreak(brk)
                    prev = brk.HighValue
                Next
                ' now do the last break
                brk = New MapWinGIS.GridColorBreak
                brk.LowValue = prev
                brk.HighValue = max
                brk.LowColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                r = eR
                g = eG
                b = eB
                brk.HighColor = System.Convert.ToUInt32(RGB(CInt(r), CInt(g), CInt(b)))
                If brk.HighValue = brk.LowValue Then
                    brk.Caption = CStr(brk.LowValue)
                Else
                    brk.Caption = brk.LowValue.ToString(m_NumberFormat & m_Precision)
                    'tPrecision = CInt(Math.Floor(m_Precision - Math.Log10(CDbl(brk.LowValue))))
                    'If tPrecision < 0 Then tPrecision = 0
                    'brk.Caption = Math.Round(CDbl(brk.LowValue), tPrecision) & " - " & brk.HighValue
                End If
                brk.ColoringType = coloringType
                brk.GradientModel = gradientModel
                m_ColoringScheme.InsertBreak(brk)
            End If
        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
            Debug.WriteLine(g_error)
        End Try
    End Sub

    Private Sub chkComputeHillshade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComputeHillshade.CheckedChanged
        If m_ColoringScheme Is Nothing Then Exit Sub

        Dim i As Integer
        Dim coloringType As MapWinGIS.ColoringType
        If chkComputeHillshade.Checked Then
            coloringType = MapWinGIS.ColoringType.Hillshade
        Else
            coloringType = MapWinGIS.ColoringType.Gradient
        End If
        For i = 0 To m_ColoringScheme.NumBreaks - 1
            m_ColoringScheme.Break(i).ColoringType = coloringType
        Next
        DataChanged()
    End Sub

    Private Sub ColoringSchemeViewer1_DataChanged() Handles ColoringSchemeViewer1.DataChanged
        DataChanged()
    End Sub

    Private Sub DataChanged()
        cmdApply.Enabled = True
    End Sub

    Private Sub cmbGradientStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGradientStyle.SelectedIndexChanged
        Dim i As Integer
        Dim t As MapWinGIS.GradientModel
        t = CType(System.Enum.Parse(GetType(MapWinGIS.GradientModel), cmbGradientStyle.Text), MapWinGIS.GradientModel)
        For i = 0 To m_ColoringScheme.NumBreaks - 1
            m_ColoringScheme.Break(i).GradientModel = t
        Next
        cmdApply.Enabled = True
    End Sub

    Private Sub ToolStrip1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        '2005/08/26 added by Lailin Chen to handle the button events after removing the DotNetBar.
        Select Case e.ClickedItem.Name
            Case "tbAdd"
                Dim brk As New MapWinGIS.GridColorBreak
                Select Case Me.cmbGradientStyle.Text
                    Case "Linear"
                        brk.GradientModel = MapWinGIS.GradientModel.Linear
                    Case "Exponential"
                        brk.GradientModel = MapWinGIS.GradientModel.Exponential
                    Case "Logorithmic"
                        brk.GradientModel = MapWinGIS.GradientModel.Logorithmic
                End Select
                brk.LowValue = 0
                brk.HighValue = 0
                brk.LowColor = System.Convert.ToUInt32(0)
                brk.HighColor = System.Convert.ToUInt32(0)
                If Me.chkComputeHillshade.Checked = True Then
                    brk.ColoringType = MapWinGIS.ColoringType.Hillshade
                Else
                    brk.ColoringType = MapWinGIS.ColoringType.Gradient
                End If
                m_ColoringScheme.InsertBreak(brk)
                ColoringSchemeViewer1.Refresh()
                DataChanged()

            Case "tbRemove"
                Dim i As Integer

                For i = ColoringSchemeViewer1.SelectedBreaks.Length - 1 To 0 Step -1
                    If ColoringSchemeViewer1.SelectedBreaks(i) Then m_ColoringScheme.DeleteBreak(i)
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
                        If TypeOf scheme Is MapWinGIS.GridColorScheme Then
                            m_ColoringScheme = CType(scheme, MapWinGIS.GridColorScheme)
                            Me.ColoringSchemeViewer1.m_GridColoringScheme = m_ColoringScheme
                            If m_ColoringScheme.NumBreaks > 0 AndAlso m_ColoringScheme.Break(0).ColoringType = MapWinGIS.ColoringType.Hillshade Then
                                chkComputeHillshade.Checked = True
                            Else
                                chkComputeHillshade.Checked = False
                            End If
                            ColoringSchemeViewer1.ClearSelectedBreaks()
                            Me.ColoringSchemeViewer1.Refresh()
                        End If
                    End If
                End If
                DataChanged()
                'Case Wizard and Wizard subitems -- these are handled on individual context menu click events below
        End Select
    End Sub

    Private Sub EqualValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UniqueValuesToolStripMenuItem.Click
        If GetUniqueBreaks(True) Then
            DataChanged()
        End If
        ColoringSchemeViewer1.Refresh()
    End Sub

    Private Sub EqualBreaks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EqualBreaksToolStripMenuItem.Click
        Dim numBreaks As Integer
        Dim resString As String
        resString = InputBox("How many breaks?", "Input number of breaks...", m_ColoringScheme.NumBreaks.ToString)
        If resString Is Nothing OrElse resString.Length = 0 OrElse (Not IsNumeric(resString)) Then resString = "0"
        numBreaks = CInt(resString)
        If numBreaks <= 0 Then Exit Sub
        MakeEqualBreaks(numBreaks)
        ColoringSchemeViewer1.ClearSelectedBreaks()
        ColoringSchemeViewer1.Refresh()
        DataChanged()
    End Sub

    Private Sub ContinuousRamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContinuousRampToolStripMenuItem.Click
        Dim dlg As New RampDialog
        If dlg.ShowDialog() = DialogResult.OK Then
            MakeContinuousRamp(dlg.GetValues.NumBreaks, dlg.GetValues.StartColor, dlg.GetValues.EndColor)
            ColoringSchemeViewer1.ClearSelectedBreaks()
            ColoringSchemeViewer1.Refresh()
            DataChanged()
        End If
    End Sub

    Private Sub DeadSea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeadSea.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.DeadSea)
    End Sub

    Private Sub Desert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Desert.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.Desert)
    End Sub

    Private Sub FallLeaves_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FallLeaves.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.FallLeaves)
    End Sub

    Private Sub Glaciers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Glaciers.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.Glaciers)
    End Sub

    Private Sub Highway_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Highway.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.Highway1)
    End Sub

    Private Sub Meadow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Meadow.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.Meadow)
    End Sub

    Private Sub SummerMountains_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummerMountains.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.SummerMountains)
    End Sub

    Private Sub ValleyFires_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValleyFires.Click
        SetDefaultScheme(MapWinGIS.PredefinedColorScheme.ValleyFires)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
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

    Private Sub tbbMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMoveUp.Click
        'Note "To 1" -- we don't want to let people move top item up
        For i As Integer = ColoringSchemeViewer1.SelectedBreaks.Length - 1 To 1 Step -1
            If ColoringSchemeViewer1.SelectedBreaks(i) Then
                Dim br As MapWinGIS.GridColorBreak = m_ColoringScheme.Break(i)
                m_ColoringScheme.DeleteBreak(i)
                m_ColoringScheme.InsertAt(i - 1, br)
                ColoringSchemeViewer1.ClearSelectedBreaks()
                ColoringSchemeViewer1.Refresh()
                ColoringSchemeViewer1.SetSelectedBreak(i - 1, True)
                DataChanged()
                Return
            End If
        Next
    End Sub

    Private Sub tbbMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMoveDown.Click
        'Note Length - 2 -- we don't want to let people move bottom item down
        For i As Integer = ColoringSchemeViewer1.SelectedBreaks.Length - 2 To 0 Step -1
            If ColoringSchemeViewer1.SelectedBreaks(i) Then
                Dim br As MapWinGIS.GridColorBreak = m_ColoringScheme.Break(i)
                m_ColoringScheme.DeleteBreak(i)
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
