Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Data.SQLite
Imports System.Data.EntityClient
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic


Public Class frmDetails
    Dim dAdapter As SQLiteDataAdapter
    Dim dTable As DataTable

    Private mOBJECT_UID As String

    Private newOBJ As Boolean = False
    Private newCONSEQ As Boolean = False
    Private newGED As Boolean = False
    Private currentHelpTopic As String

    Private Sub frmDetails_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim do_not As Boolean = False
        If e.CloseReason = CloseReason.UserClosing Then
            If TypeOf sender Is frmDetails Then
                Dim f As frmDetails = sender
                If TypeOf (f.ActiveControl) Is Button Then
                    Dim b As Button = f.ActiveControl
                    If b.Name = "btInsertRecord" Then
                        do_not = True
                    End If
                End If
            End If
            If Not do_not Then

                Me.GEMOBJECTBindingSource.EndEdit()
                Me.GEDBindingSource.EndEdit()
                Me.CONSEQUENCESBindingSource.EndEdit()
                dgMedia.EndEdit()

                If Me.GEMDataset.HasChanges Then
                    If MessageBox.Show("Are you sure you want to close this form and lose any changes?", "Close form", MessageBoxButtons.YesNo) = vbNo Then
                        e.Cancel = True
                    End If
                End If
            End If
        End If


    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GEMDataset.DIC_HEIGHT_QUALIFIER' table. You can move, or remove it, as needed.
        Me.DIC_HEIGHT_QUALIFIERTableAdapter.Fill(Me.GEMDataset.DIC_HEIGHT_QUALIFIER)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_DAMAGE' table. You can move, or remove it, as needed.
        Me.DIC_DAMAGETableAdapter.Fill(Me.GEMDataset.DIC_DAMAGE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_CURRENCY' table. You can move, or remove it, as needed.
        Me.DIC_CURRENCYTableAdapter.Fill(Me.GEMDataset.DIC_CURRENCY)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_PLAN_SHAPE' table. You can move, or remove it, as needed.
        Me.DIC_PLAN_SHAPETableAdapter.Fill(Me.GEMDataset.DIC_PLAN_SHAPE)
        'TODO: This line of code loads data into the 'GEMDataset.GED' table. You can move, or remove it, as needed.
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_POSITION' table. You can move, or remove it, as needed.
        Me.DIC_POSITIONTableAdapter.Fill(Me.GEMDataset.DIC_POSITION)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_YEAR_BUILT_QUAL' table. You can move, or remove it, as needed.
        Me.DIC_YEAR_BUILT_QUALTableAdapter.Fill(Me.GEMDataset.DIC_YEAR_BUILT_QUAL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_NONSTRUCTURAL_EXTERIOR_WALLS' table. You can move, or remove it, as needed.
        Me.DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter.Fill(Me.GEMDataset.DIC_NONSTRUCTURAL_EXTERIOR_WALLS)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_FOUNDATION_SYSTEM' table. You can move, or remove it, as needed.
        Me.DIC_FOUNDATION_SYSTEMTableAdapter.Fill(Me.GEMDataset.DIC_FOUNDATION_SYSTEM)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_FLOOR_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_FLOOR_TYPETableAdapter.Fill(Me.GEMDataset.DIC_FLOOR_TYPE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_FLOOR_MATERIAL' table. You can move, or remove it, as needed.
        Me.DIC_FLOOR_MATERIALTableAdapter.Fill(Me.GEMDataset.DIC_FLOOR_MATERIAL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_ROOF_SYSTEM_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_ROOF_SYSTEM_TYPETableAdapter.Fill(Me.GEMDataset.DIC_ROOF_SYSTEM_TYPE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_ROOF_SYSTEM_MATERIAL' table. You can move, or remove it, as needed.
        Me.DIC_ROOF_SYSTEM_MATERIALTableAdapter.Fill(Me.GEMDataset.DIC_ROOF_SYSTEM_MATERIAL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_ROOF_COVER_MATERIAL' table. You can move, or remove it, as needed.
        Me.DIC_ROOF_COVER_MATERIALTableAdapter.Fill(Me.GEMDataset.DIC_ROOF_COVER_MATERIAL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_ROOF_SHAPE' table. You can move, or remove it, as needed.
        Me.DIC_ROOF_SHAPETableAdapter.Fill(Me.GEMDataset.DIC_ROOF_SHAPE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_LLRS_DUCTILITY' table. You can move, or remove it, as needed.
        Me.DIC_LLRS_DUCTILITYTableAdapter.Fill(Me.GEMDataset.DIC_LLRS_DUCTILITY)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_LLRS' table. You can move, or remove it, as needed.
        Me.DIC_LLRSTableAdapter.Fill(Me.GEMDataset.DIC_LLRS)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_STRUCTURAL_VERT_IRREG' table. You can move, or remove it, as needed.
        Me.DIC_STRUCTURAL_VERT_IRREGTableAdapter.Fill(Me.GEMDataset.DIC_STRUCTURAL_VERT_IRREG)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_STRUCTURAL_HORIZ_IRREG' table. You can move, or remove it, as needed.
        Me.DIC_STRUCTURAL_HORIZ_IRREGTableAdapter.Fill(Me.GEMDataset.DIC_STRUCTURAL_HORIZ_IRREG)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_STRUCTURAL_IRREGULARITY' table. You can move, or remove it, as needed.
        Me.DIC_STRUCTURAL_IRREGULARITYTableAdapter.Fill(Me.GEMDataset.DIC_STRUCTURAL_IRREGULARITY)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_STEEL_CONNECTION_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_STEEL_CONNECTION_TYPETableAdapter.Fill(Me.GEMDataset.DIC_STEEL_CONNECTION_TYPE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_MASONRY_REINFORCEMENT' table. You can move, or remove it, as needed.
        Me.DIC_MASONRY_REINFORCEMENTTableAdapter.Fill(Me.GEMDataset.DIC_MASONRY_REINFORCEMENT)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_MASONARY_MORTAR_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_MASONARY_MORTAR_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MASONARY_MORTAR_TYPE)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_MATERIAL_TECHNOLOGY' table. You can move, or remove it, as needed.
        Me.DIC_MATERIAL_TECHNOLOGYTableAdapter.Fill(Me.GEMDataset.DIC_MATERIAL_TECHNOLOGY)
        'TODO: This line of code loads data into the 'GEMDataset.CONSEQUENCES' table. You can move, or remove it, as needed.
        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_MEDIA_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_MEDIA_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MEDIA_TYPE)
        'TODO: This line of code loads data into the 'GEMDataset.MEDIA_DETAIL' table. You can move, or remove it, as needed.
        Me.MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_OCCUPANCY_DETAIL' table. You can move, or remove it, as needed.
        Me.DIC_OCCUPANCY_DETAILTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY_DETAIL)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_OCCUPANCY' table. You can move, or remove it, as needed.
        Me.DIC_OCCUPANCYTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY)
        'TODO: This line of code loads data into the 'GEMDataset.DIC_MATERIAL_TYPE' table. You can move, or remove it, as needed.
        Me.DIC_MATERIAL_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MATERIAL_TYPE)

        'Me.DIC_OCCUPANCY_DETAILTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY_DETAIL)
        'Me.DIC_OCCUPANCYTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY)
        'Me.DIC_MATERIAL_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MATERIAL_TYPE)
        'Me.DIC_MEDIA_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MEDIA_TYPE)
        'Me.MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
        ' Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)

        Me.GEM_OBJECTTableAdapter.Fill(Me.GEMDataset.GEM_OBJECT)
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)

        Me.DiC_ROOF_CONNECTIONTableAdapter.Fill(Me.GEMDataset.DIC_ROOF_CONNECTION)
        Me.DiC_FLOOR_CONNECTIONTableAdapter.Fill(Me.GEMDataset.DIC_FLOOR_CONNECTION)
        Me.GEM_RULESTableAdapter.Fill(Me.GEMDataset.GEM_RULES)

        Call loadFavsCombo()

        Dim advancedSetting As GEMDataset.SETTINGSRow = (From obj In gemdb.Dataset.SETTINGS Where obj.KEY = "ADVANCED_VIEW").FirstOrDefault
        If advancedSetting Is Nothing Then
            Dim row As GEMDataset.SETTINGSRow = gemdb.Dataset.SETTINGS.NewSETTINGSRow
            row.KEY = "ADVANCED_VIEW"
            row.VALUE = "FALSE"
            gemdb.Dataset.SETTINGS.Rows.Add(row)
            gemdb.SettingsAdapter.Update(gemdb.Dataset.SETTINGS)
            gemdb.SettingsAdapter.Fill(gemdb.Dataset.SETTINGS)
            Call showHideControls(False)
        Else
            If advancedSetting.VALUE = "TRUE" Then
                Call showHideControls(True)
            Else
                Call showHideControls(False)
            End If
        End If

        Call SetHandlers(Me)
        '
        ' Read Form labels from file if it exists
        '
        Dim formLabelsFile As String = IO.Path.GetDirectoryName(gemdb.DatabasePath) & "\FormLabels.txt"
        Call SetLabels(formLabelsFile)
        '
        ' Disable controls that depend on other controls
        '
        cbOCCUPANCY_DETAIL.Enabled = False
        cbFLOOR_TYPE.Enabled = False
        cbROOF_SYSTEM_TYPE.Enabled = False
        cbLLRS_DUCTILITY_L.Enabled = False
        cbLLRS_DUCTILITY_T.Enabled = False

    End Sub



    Public Sub New(ByVal uid As String, ByVal x As Double, ByVal y As Double)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = frmMain.Icon

        mOBJECT_UID = uid

        'Add new record to the database and use it for the data binding
        Dim row As GEMDataset.GEM_OBJECTRow = Me.GEMDataset.GEM_OBJECT.NewGEM_OBJECTRow
        row.PROJ_UID = gemdb.CurrentProjectUID
        row.OBJ_UID = mOBJECT_UID
        row.X = x
        row.Y = y
        Me.GEMDataset.GEM_OBJECT.Rows.Add(row)
        GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)
        Me.GEM_OBJECTTableAdapter.Fill(Me.GEMDataset.GEM_OBJECT)
        GEMOBJECTBindingSource.Filter = "OBJ_UID = '" & mOBJECT_UID & "'"
        newOBJ = True

        'Add new record to the database and use it for the data binding
        Dim row2 As GEMDataset.CONSEQUENCESRow = Me.GEMDataset.CONSEQUENCES.NewCONSEQUENCESRow
        row2.CONSEQ_UID = System.Guid.NewGuid.ToString
        row2.GEMOBJ_UID = mOBJECT_UID
        Me.GEMDataset.CONSEQUENCES.Rows.Add(row2)
        CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)
        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
        CONSEQUENCESBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
        newCONSEQ = True

        'Add new record to the database and use it for the data binding
        Dim row3 As GEMDataset.GEDRow = Me.GEMDataset.GED.NewGEDRow
        row3.GED_UID = System.Guid.NewGuid.ToString
        row3.GEMOBJ_UID = mOBJECT_UID
        Me.GEMDataset.GED.Rows.Add(row3)
        GEDTableAdapter.Update(Me.GEMDataset.GED)
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)
        GEDBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
        newGED = True

        'Filter media for datagrid
        MEDIADETAILBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"

        'Min and Max
        'Longitude()
        '180:    W = -180
        '180:    E = 180

        'Latitude()
        '90:     N = 90
        '90:     S = -90

        If x > 200 Or x < -200 Or y > 200 Or y < -200 Then
            MessageBox.Show("The X and Y values are not valid latitude and longitude values. This usually happens when there is a problem re-projecting the point. Try re-creating your project using the correct projection.")
        End If

    End Sub

    Public Sub New(ByVal currentRecordUID As String)

        mOBJECT_UID = currentRecordUID

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = frmMain.Icon
        btDeleteRecord.Visible = True
        GEMOBJECTBindingSource.Filter = "OBJ_UID = '" & mOBJECT_UID & "'"
        MEDIADETAILBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
        CONSEQUENCESBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
        GEDBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"

        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)

        If (From row In Me.GEMDataset.CONSEQUENCES Where row.GEMOBJ_UID = mOBJECT_UID Select row.GEMOBJ_UID).Count = 0 Then
            Dim row2 As GEMDataset.CONSEQUENCESRow = Me.GEMDataset.CONSEQUENCES.NewCONSEQUENCESRow
            row2.CONSEQ_UID = System.Guid.NewGuid.ToString
            row2.GEMOBJ_UID = mOBJECT_UID
            Me.GEMDataset.CONSEQUENCES.Rows.Add(row2)
            CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)
            Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
            CONSEQUENCESBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
            newCONSEQ = True
        End If

        If (From row In Me.GEMDataset.GED Where row.GEMOBJ_UID = mOBJECT_UID Select row.GEMOBJ_UID).Count = 0 Then
            'Add new record to the database and use it for the data binding
            Dim row3 As GEMDataset.GEDRow = Me.GEMDataset.GED.NewGEDRow
            row3.GED_UID = System.Guid.NewGuid.ToString
            row3.GEMOBJ_UID = mOBJECT_UID
            Me.GEMDataset.GED.Rows.Add(row3)
            GEDTableAdapter.Update(Me.GEMDataset.GED)
            Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)
            GEDBindingSource.Filter = "GEMOBJ_UID = '" & mOBJECT_UID & "'"
            newGED = True
        End If

    End Sub

    Private Sub importValuesFromUID(ByVal exisitngUID As String)

        'IMPORT GEM OBJECT DATA
        Me.GEM_OBJECTTableAdapter.Fill(Me.GEMDataset.GEM_OBJECT)
        If (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = mOBJECT_UID).Count > 0 And (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = exisitngUID).Count > 0 Then
            Dim row As GEMDataset.GEM_OBJECTRow = (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = exisitngUID).First
            Dim currentRow As GEMDataset.GEM_OBJECTRow = (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = mOBJECT_UID).First
            For Each col As DataColumn In Me.GEMDataset.GEM_OBJECT.Columns
                If Not col.ColumnName.Contains("USER_") And Not col.ColumnName.Contains("DATE_") And Not col.ColumnName.Contains("_UID") And Not col.ColumnName = "X" And Not col.ColumnName = "Y" Then
                    If IsDBNull(currentRow(col.ColumnName)) And IsDBNull(row(col.ColumnName)) Then
                        Continue For
                    ElseIf IsDBNull(currentRow(col.ColumnName)) Or IsDBNull(row(col.ColumnName)) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newOBJ = False
                    ElseIf currentRow(col.ColumnName) <> row(col.ColumnName) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newOBJ = False
                    End If
                End If
            Next
        End If
        GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)
        Me.GEM_OBJECTTableAdapter.Fill(Me.GEMDataset.GEM_OBJECT)

        'IMPORT CONSEQUENCES DATA
        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
        If (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = mOBJECT_UID).Count > 0 And (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = exisitngUID).Count > 0 Then
            Dim row As GEMDataset.CONSEQUENCESRow = (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = exisitngUID).First
            Dim currentRow As GEMDataset.CONSEQUENCESRow = (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = mOBJECT_UID).First
            For Each col As DataColumn In Me.GEMDataset.CONSEQUENCES.Columns
                If Not col.ColumnName.Contains("USER_") And Not col.ColumnName.Contains("DATE_") And Not col.ColumnName.Contains("_UID") And Not col.ColumnName = "X" And Not col.ColumnName = "Y" Then
                    If IsDBNull(currentRow(col.ColumnName)) And IsDBNull(row(col.ColumnName)) Then
                        Continue For
                    ElseIf IsDBNull(currentRow(col.ColumnName)) Or IsDBNull(row(col.ColumnName)) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newOBJ = False
                    ElseIf currentRow(col.ColumnName) <> row(col.ColumnName) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newCONSEQ = False
                    End If
                End If
            Next
        End If
        Me.CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)
        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)

        'IMPORT GED DATA
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)
        If (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = mOBJECT_UID).Count > 0 And (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = exisitngUID).Count > 0 Then
            Dim row As GEMDataset.GEDRow = (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = exisitngUID).First
            Dim currentRow As GEMDataset.GEDRow = (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = mOBJECT_UID).First
            For Each col As DataColumn In Me.GEMDataset.GED.Columns
                If Not col.ColumnName.Contains("USER_") And Not col.ColumnName.Contains("DATE_") And Not col.ColumnName.Contains("_UID") And Not col.ColumnName = "X" And Not col.ColumnName = "Y" Then
                    If IsDBNull(currentRow(col.ColumnName)) And IsDBNull(row(col.ColumnName)) Then
                        Continue For
                    ElseIf IsDBNull(currentRow(col.ColumnName)) Or IsDBNull(row(col.ColumnName)) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newOBJ = False
                    ElseIf currentRow(col.ColumnName) <> row(col.ColumnName) Then
                        currentRow(col.ColumnName) = row(col.ColumnName)
                        newGED = False
                    End If
                End If
            Next
        End If
        Me.GEDTableAdapter.Update(Me.GEMDataset.GED)
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)

   
    End Sub

    Private Sub dgMedia_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgMedia.DataError
        If TypeOf e.Exception Is System.FormatException Then
            e.Cancel = True
            MessageBox.Show("The Frame Number Needs to be Numeric.", "Numeric Value Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    Private Sub dgMedia_DefaultValuesNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgMedia.DefaultValuesNeeded
        e.Row.Cells("GEMOBJUIDDataGridViewTextBoxColumn").Value = mOBJECT_UID
        e.Row.Cells("MEDIAUIDDataGridViewTextBoxColumn").Value = System.Guid.NewGuid.ToString
    End Sub


    'Private Sub filterComboBoxBinding(ByVal firstCombo As ComboBox, ByVal secondCombo As ComboBox, ByVal secondComboBinder As BindingSource)
    '    If Not firstCombo.SelectedValue Is Nothing And firstCombo.SelectedValue <> "" Then
    '        Dim previousValue As String = secondCombo.SelectedValue
    '        If previousValue Is Nothing Then
    '            previousValue = ""
    '        End If

    '        'Get Scope from dictionary
    '        Dim scopeValue As String = ""
    '        Try
    '            Dim dt As DataTable = CType(Me.GEMDataset.Tables(firstCombo.DataSource.DataMember), DataTable)
    '            Dim dr As DataRow = (From rec In dt.Rows Where rec("CODE") = firstCombo.SelectedValue Select rec).FirstOrDefault
    '            scopeValue = dr("SCOPE")
    '        Catch ex As Exception
    '        End Try

    '        secondComboBinder.Filter = "SCOPE = '" & scopeValue & "'"
    '        secondCombo.SelectedValue = previousValue
    '        If secondComboBinder.Count = 0 Then
    '            secondCombo.Enabled = False
    '        Else
    '            secondCombo.Enabled = True
    '        End If
    '    Else
    '        secondCombo.Enabled = False
    '        secondCombo.SelectedValue = ""
    '    End If
    'End Sub

    Private Sub filterComboBoxBinding(ByVal firstCombo As ComboBox, ByVal secondCombo As ComboBox, ByVal secondComboBinder As BindingSource)
        If Not firstCombo.SelectedValue Is Nothing And firstCombo.SelectedValue <> "" Then
            Dim previousValue As String = secondCombo.SelectedValue
            If previousValue Is Nothing Then
                previousValue = ""
            End If
            '
            'Get Allowed Values from GEM_RULES
            '
            Dim allowedValues As String = "'DUMMY'" ' Dummy value to ensure there is always a value sepecified in the where clause
            Try
                Dim dt As DataTable = Me.GEMDataset.GEM_RULES
                If dt Is Nothing Then Exit Sub 'If constraints table cannot be found then go ahead without constraints
                '
                ' Get rows from GEM_RULES where PARENT_CODE matches the selected value
                '
                Dim matchingRows = (From rec In dt.Rows Where rec("PARENT_CODE") = firstCombo.SelectedValue Select rec).AsEnumerable
                '
                ' Build list of allowed values
                '
                For Each matchingRow As DataRow In matchingRows
                    allowedValues = allowedValues & ",'" & matchingRow("CHILD_CODE") & "'"
                Next

            Catch ex As Exception
            End Try

            secondComboBinder.Filter = "CODE IN (" & allowedValues & ")"
            secondCombo.SelectedValue = previousValue
            If secondComboBinder.Count = 0 Then
                secondCombo.Enabled = False
            Else
                secondCombo.Enabled = True
            End If
        Else
            secondCombo.Enabled = False
            secondCombo.SelectedValue = ""
        End If
    End Sub


    Private Sub btInsertRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btInsertRecord.Click

        Call SaveChanges(True)

    End Sub

    Private Sub SaveChanges(ByVal close As Boolean)
        'Save Media Table
        dgMedia.EndEdit()
        dgMedia.Update()
        MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)

        'Save Edits
        Me.GEMOBJECTBindingSource.EndEdit()
        Me.GEDBindingSource.EndEdit()
        Me.CONSEQUENCESBindingSource.EndEdit()

        'Update OBJ, GED And Consequences table
        'If there are no edits into the temporary records delete them
        If Not Me.GEMDataset.GEM_OBJECT.GetChanges Is Nothing Then
            GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)
            newOBJ = False
            'Else
            '    If newOBJ And close Then
            '        Dim row As GEMDataset.GEM_OBJECTRow = (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = mOBJECT_UID).First
            '        row.Delete()
            '        GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)
            '    End If
        End If

        If Not Me.GEMDataset.GED.GetChanges Is Nothing Then
            GEDTableAdapter.Update(Me.GEMDataset.GED)
            newGED = False
        Else
            If newGED And close Then
                Dim row As GEMDataset.GEDRow = (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = mOBJECT_UID).First
                row.Delete()
                GEDTableAdapter.Update(Me.GEMDataset.GED)
            End If
        End If

        If Not Me.GEMDataset.CONSEQUENCES.GetChanges Is Nothing Then
            CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)
            newCONSEQ = False
        Else
            If newCONSEQ And close Then
                Dim row As GEMDataset.CONSEQUENCESRow = (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = mOBJECT_UID).First
                row.Delete()
                CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)
            End If
        End If

        If close Then
            Me.Close()
        End If

    End Sub

    Private Sub btCancelChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancelChanges.Click

        Me.GEMOBJECTBindingSource.EndEdit()
        Me.GEDBindingSource.EndEdit()
        Me.CONSEQUENCESBindingSource.EndEdit()
        dgMedia.EndEdit()

        Me.Close()


    End Sub

    Private Sub btDeleteRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDeleteRecord.Click
        If MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo) = vbYes Then

            Dim row As GEMDataset.GEM_OBJECTRow = (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = mOBJECT_UID).First
            row.Delete()
            GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)

            Dim row2 As GEMDataset.GEDRow = (From obj In Me.GEMDataset.GED Where obj.GEMOBJ_UID = mOBJECT_UID).First
            row2.Delete()
            GEDTableAdapter.Update(Me.GEMDataset.GED)
      
            Dim row3 As GEMDataset.CONSEQUENCESRow = (From obj In Me.GEMDataset.CONSEQUENCES Where obj.GEMOBJ_UID = mOBJECT_UID).First
            row3.Delete()
            CONSEQUENCESTableAdapter.Update(Me.GEMDataset.CONSEQUENCES)

            'Delete shapefile record
            Dim ids As New Object
            Dim errorStr As String = ""
            memoryShape.Table.Query("[OBJ_UID] = """ & mOBJECT_UID & """", ids, errorStr)
            memoryShape.StartEditingShapes(True)
            memoryShape.EditDeleteShape(ids(0))
            memoryShape.StopEditingShapes(True, True)

            'Delete all media records
            For Each row4 As GEMDataset.MEDIA_DETAILRow In Me.GEMDataset.MEDIA_DETAIL
                If row4.GEMOBJ_UID = mOBJECT_UID Then
                    row4.Delete()
                End If
            Next
            Me.MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)

            Me.Close()
        End If
    End Sub


    Private Sub btFavSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFavSave.Click

        If cbFavs.Text <> "" Then
            Me.GEMOBJECTBindingSource.EndEdit()
            Me.GEDBindingSource.EndEdit()
            Me.CONSEQUENCESBindingSource.EndEdit()
            dgMedia.EndEdit()

            If Me.GEMDataset.HasChanges Then
                If MessageBox.Show("Do you want to save changes to the current record? Saving a favourite will create a reference to this record.", "Save Changes", MessageBoxButtons.YesNo) = vbYes Then
                    Call SaveChanges(False)
                End If
            End If

            'Check if the fav is already in the settings else make a new row
            Dim row As GEMDataset.SETTINGSRow
            If (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "FAV_" & cbFavs.Text).Count = 0 Then
                row = gemdb.Dataset.SETTINGS.NewSETTINGSRow
                row.KEY = "FAV_" & cbFavs.Text
                row.VALUE = mOBJECT_UID
                gemdb.Dataset.SETTINGS.Rows.Add(row)
            Else
                row = (From setting In gemdb.Dataset.SETTINGS Where setting.KEY = "FAV_" & cbFavs.Text).First
                row.VALUE = mOBJECT_UID
            End If
            gemdb.SettingsAdapter.Update(gemdb.Dataset.SETTINGS)
        End If

    End Sub

    Private Sub loadFavsCombo()

        cbFavs.Items.Clear()
        gemdb.SettingsAdapter.Fill(gemdb.Dataset.SETTINGS)
        Dim itemList As New List(Of ListItem)
        For Each row As GEMDataset.SETTINGSRow In gemdb.Dataset.SETTINGS
            If row.KEY.Length > 4 Then
                If row.KEY.Substring(0, 4) = "FAV_" Then
                    Dim favString As String = row.KEY.Substring(4)
                    Dim favUID As String = row.VALUE
                    itemList.Add(New ListItem(favUID, favString))
                End If
            End If
        Next
        cbFavs.DataSource = itemList
        cbFavs.DisplayMember = "Name"
        cbFavs.ValueMember = "ID"

        cbFavs.SelectedItem = Nothing

    End Sub

    Private Sub btFavLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFavLoad.Click
        If cbFavs.SelectedValue <> "" Then
            importValuesFromUID(cbFavs.SelectedValue)
        End If
    End Sub

    Private Sub cbOCCUPANCY_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbOCCUPANCY.TextChanged
        filterComboBoxBinding(cbOCCUPANCY, cbOCCUPANCY_DETAIL, DICOCCUPANCYDETAILBindingSource)
    End Sub

    Private Sub cbMATERIAL_TYPE_L_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMATERIAL_TYPE_L.TextChanged
        filterComboBoxBinding(cbMATERIAL_TYPE_L, cbMATERIAL_TECHNOLOGY_L, DICMATERIALTECHNOLOGYBindingSource)
        filterComboBoxBinding(cbMATERIAL_TYPE_L, cbMASONRY_REINFORCEMENT_L, DICMASONRYREINFORCEMENTBindingSource)
        filterComboBoxBinding(cbMATERIAL_TYPE_L, cbMASONRY_MORTAR_TYPE_L, DICMASONARYMORTARTYPEBindingSource)
        filterComboBoxBinding(cbMATERIAL_TYPE_L, cbSTEEL_CONNECTION_TYPE_L, DICSTEELCONNECTIONTYPEBindingSource)
        filterComboBoxBinding(cbMATERIAL_TYPE_L, cbLLRS_L, DICLLRSBindingSource)
    End Sub

    Private Sub cbMATERIAL_TYPE_T_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMATERIAL_TYPE_T.TextChanged
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMATERIAL_TECHNOLOGY_T, DICMATERIALTECHNOLOGYBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMASONRY_REINFORCEMENT_T, DICMASONRYREINFORCEMENTBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMASONRY_MORTAR_TYPE_T, DICMASONARYMORTARTYPEBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbSTEEL_CONNECTION_TYPE_T, DICSTEELCONNECTIONTYPEBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbLLRS_T, DICLLRSBindingSource1)
    End Sub

    Private Sub cbROOF_SYSTEM_MATERIAL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbROOF_SYSTEM_MATERIAL.TextChanged
        filterComboBoxBinding(cbROOF_SYSTEM_MATERIAL, cbROOF_SYSTEM_TYPE, DICROOFSYSTEMTYPEBindingSource)
    End Sub

    Private Sub cbFLOOR_MATERIAL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFLOOR_MATERIAL.TextChanged
        filterComboBoxBinding(cbFLOOR_MATERIAL, cbFLOOR_TYPE, DICFLOORTYPEBindingSource)
    End Sub

    Private Sub cbLLRS_L_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLLRS_L.TextChanged
        filterComboBoxBinding(cbLLRS_L, cbLLRS_DUCTILITY_L, DICLLRSDUCTILITYBindingSource)
    End Sub

    Private Sub cbLLRS_T_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLLRS_T.TextChanged
        filterComboBoxBinding(cbLLRS_T, cbLLRS_DUCTILITY_T, DICLLRSDUCTILITYBindingSource1)
    End Sub

    Private Sub cbSTRUCTURAL_IRREGULARITY_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSTRUCTURAL_IRREGULARITY.TextChanged
        filterComboBoxBinding(cbSTRUCTURAL_IRREGULARITY, cbSTRUCTURAL_HORIZ_IRREG_P, DICSTRUCTURALHORIZIRREGBindingSource)
        filterComboBoxBinding(cbSTRUCTURAL_IRREGULARITY, cbSTRUCTURAL_HORIZ_IRREG_S, DICSTRUCTURALHORIZIRREGBindingSource1)
        filterComboBoxBinding(cbSTRUCTURAL_IRREGULARITY, cbSTRUCTURAL_VERT_IRREG_P, DICSTRUCTURALVERTIRREGBindingSource)
        filterComboBoxBinding(cbSTRUCTURAL_IRREGULARITY, cbSTRUCTURAL_VERT_IRREG_S, DICSTRUCTURALVERTIRREGBindingSource1)
    End Sub

    Private Sub chbAdvancedView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAdvancedView.CheckedChanged
        Call showHideControls(chbAdvancedView.Checked)

        Dim advancedSetting As GEMDataset.SETTINGSRow = (From obj In gemdb.Dataset.SETTINGS Where obj.KEY = "ADVANCED_VIEW").FirstOrDefault

        If advancedSetting Is Nothing Then
            Dim row As GEMDataset.SETTINGSRow = gemdb.Dataset.SETTINGS.NewSETTINGSRow
            row.KEY = "ADVANCED_VIEW"
            If chbAdvancedView.Checked Then
                row.VALUE = "TRUE"
            Else
                row.VALUE = "FALSE"
            End If
            gemdb.Dataset.SETTINGS.Rows.Add(row)
        Else
            If chbAdvancedView.Checked Then
                advancedSetting.VALUE = "TRUE"
            Else
                advancedSetting.VALUE = "FALSE"
            End If
        End If
        gemdb.SettingsAdapter.Update(gemdb.Dataset.SETTINGS)
        gemdb.SettingsAdapter.Fill(gemdb.Dataset.SETTINGS)

    End Sub

    Private Sub showHideControls(ByVal showControls As Boolean)
        If showControls Then
            lblLong1.Visible = True
            lblLong2.Visible = True
            lblTrans1.Visible = True
            lblTrans2.Visible = True
            lblPrim1.Visible = True
            lblSec1.Visible = True

            cbLLRS_DUCTILITY_T.Visible = True
            cbLLRS_T.Visible = True
            cbMASONRY_MORTAR_TYPE_T.Visible = True
            cbMASONRY_REINFORCEMENT_T.Visible = True
            cbMATERIAL_TECHNOLOGY_T.Visible = True
            cbMATERIAL_TYPE_T.Visible = True
            cbSTEEL_CONNECTION_TYPE_T.Visible = True
            cbSTRUCTURAL_HORIZ_IRREG_S.Visible = True
            cbSTRUCTURAL_VERT_IRREG_S.Visible = True
        Else
            lblLong1.Visible = False
            lblLong2.Visible = False
            lblTrans1.Visible = False
            lblTrans2.Visible = False
            lblPrim1.Visible = False
            lblSec1.Visible = False

            cbLLRS_DUCTILITY_T.Visible = False
            cbLLRS_T.Visible = False
            cbMASONRY_MORTAR_TYPE_T.Visible = False
            cbMASONRY_REINFORCEMENT_T.Visible = False
            cbMATERIAL_TECHNOLOGY_T.Visible = False
            cbMATERIAL_TYPE_T.Visible = False
            cbSTEEL_CONNECTION_TYPE_T.Visible = False
            cbSTRUCTURAL_HORIZ_IRREG_S.Visible = False
            cbSTRUCTURAL_VERT_IRREG_S.Visible = False
        End If
    End Sub


    Private Sub SetHandlers(ByVal parentCtr As Control)
        '
        ' Name: SetHandlers
        ' Purpose: To add handlers to comboboxes and Labels to track the current context to allows context sensitive help to be displayed on the Help Tab
        ' Written: K.Adlam, 26/11/12
        '
        For Each ctrl As Control In parentCtr.Controls

            If TypeOf ctrl Is ComboBox Then
                AddHandler (ctrl.TextChanged), AddressOf TrackHelpTopicCombo
            ElseIf TypeOf ctrl Is Label Then
                AddHandler (ctrl.MouseDown), AddressOf TrackHelpTopicLabel
            End If
            SetHandlers(ctrl)
        Next
    End Sub

    Sub TrackHelpTopicCombo(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ctrl As Control = sender
        currentHelpTopic = ctrl.Text
    End Sub

    Sub TrackHelpTopicLabel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ctrl As Control = sender
        currentHelpTopic = ctrl.Text
        Me.TabControl1.SelectTab(5) 'select the Help Tab
    End Sub

    Sub ShowContextHelp()
        '
        ' Name: ShowContextHelp
        ' Purpose: To show context sensitive help based on the current value of the variable currentHelpTopic
        ' Written: K.Adlam, 26/11/12
        '
        '
        ' Get help directory and check it exists
        '
        Dim helpDir As String = Application.StartupPath & "\LocalResources\Help\HelpDocs\glossary"
        If (Not IO.Directory.Exists(helpDir)) Then
            MsgBox("Help Folder " & helpDir & " does not exist")
            Exit Sub
        End If
        '
        ' Get path to help file assume that it is an file and see if it exists 
        '

        Dim helpFile As String = helpDir & "\default.html"
        If currentHelpTopic <> "" Then
            Dim fuz As New FuzzySearch
            helpFile = fuz.Search(currentHelpTopic, IO.Directory.GetFiles(helpDir).ToList, 0.5)
        End If

        If (IO.File.Exists(helpFile)) Then
            WebBrowser1.Navigate(helpFile)
            Exit Sub
        Else
            ' If this fails show default htm file
            helpFile = helpDir & "\default.html"
            If (IO.File.Exists(helpFile)) Then
                WebBrowser1.Navigate(helpFile)
                Exit Sub
            Else 'If all else fails let the user know that there are no help files
                MsgBox("No help file exists for topic """ & currentHelpTopic & """. To add a help file create an htm file with the same name as the topic and place it in the Help folder.")
            End If
        End If



    End Sub

    Private Sub TabPage6_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage6.Enter
        Call ShowContextHelp()
    End Sub

    'Private Sub dgMedia_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgMedia.CellClick
    '    If (e.ColumnIndex = -1 And e.RowIndex >= 0) Then
    '        Call OpenPaint("P:\kama\test2.jpg")
    '        Exit Sub
    '    End If
    '    'If (e.RowIndex < 0) Then Exit Sub
    '    'Dim pRow As DataGridViewRow = dgMedia.Rows(e.RowIndex)
    '    ''Dim mediaNumber As String = pRow.Cells("MEDIA_NUMB").Value.ToString
    '    ''Dim OriginalFilename As String = pRow.Cells("ORIG_FILEN").Value.ToString
    '    ''Dim gemFilename As String = pRow.Cells("FILENAME").Value.ToString
    '    ''If (pRow.Cells(e.ColumnIndex).OwningColumn.Name = "MEDIA_NUMB") Then
    '    '' MsgBox("media")
    '    ''End If
    '    ''Dim cellValue As String = pRow.Cells(e.ColumnIndex).Value.ToString
    'End Sub

    Sub OpenPaint(ByVal strFileName As String)
        '
        ' Name: OpenPaint
        ' Purpose: To open MS Paint with an existing file or with a new blank file
        ' Written: K.Adlam, 29/11/12
        '
        '
        ' Define size of image
        '
        Dim width As Integer = 800
        Dim height As Integer = 600
        '
        ' If file does not exist then create a new blank one
        '
        If (Not IO.File.Exists(strFileName)) Then
            Dim bm As New Bitmap(width, height)
            Dim pGraphics As Graphics = Graphics.FromImage(bm)
            pGraphics.FillRectangle(Brushes.White, 0, 0, width, height)
            bm.Save(strFileName, System.Drawing.Imaging.ImageFormat.Jpeg)
        End If
        '
        ' Open file in Paint
        '
        System.Diagnostics.Process.Start("mspaint", """" & strFileName & """")
        '
    End Sub

    Private Sub dgMedia_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgMedia.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim ht As DataGridView.HitTestInfo
            ht = dgMedia.HitTest(e.X, e.Y)
            If ht.Type = DataGridViewHitTestType.Cell Then
                'dgMedia.ContextMenuStrip = mnuCell
            ElseIf ht.Type = DataGridViewHitTestType.RowHeader Then
                'dgMedia.CurrentCell = dgMedia(0, ht.RowIndex)
                'dgMedia.Rows(ht.RowIndex).Selected = True
                mnuRow.Show(dgMedia, e.Location)
                'dgMedia.ContextMenuStrip = mnuRow
            ElseIf ht.Type = DataGridViewHitTestType.ColumnHeader Then
                'dgMedia.ContextMenuStrip = mnuColumn
            End If
        End If
    End Sub



    Private Sub ShowMediaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMediaToolStripMenuItem.Click
        'SHOW MEDIA
        Dim currentuid As String = getMediaUIDFromSelectedMediaRow()

        Call updateMediaTable()
        Dim currentrow As GEMDataset.MEDIA_DETAILRow = getMediaRowFromUID(currentuid)
        If currentrow Is Nothing Then
            MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not currentrow.IsFILENAMENull AndAlso currentrow.FILENAME <> "" Then
            If IO.File.Exists(gemdb.MediaPath & "\" & currentrow.FILENAME) Then
                If currentrow.MEDIA_TYPE = "SKETCH" Then
                    Call OpenPaint(gemdb.MediaPath & "\" & currentrow.FILENAME)
                Else
                    System.Diagnostics.Process.Start("""" & gemdb.MediaPath & "\" & currentrow.FILENAME & """")
                End If
            Else
                MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Cannot find the media file, check the GEM media folder exists", "Media file missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub LinkToMediaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkToMediaToolStripMenuItem.Click
        'ADD MEDIA
        Dim currentuid As String = getMediaUIDFromSelectedMediaRow()
        Call updateMediaTable()
        Dim currentrow As GEMDataset.MEDIA_DETAILRow = getMediaRowFromUID(currentuid)
        If currentrow Is Nothing Then
            MessageBox.Show("Please create a record before adding media", "Add record first", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If currentrow.MEDIA_TYPE = "SKETCH" Then
            Call OpenPaint(gemdb.MediaPath & "\" & currentrow.MEDIA_UID & ".jpg")
            currentrow.ORIG_FILEN = gemdb.MediaPath & "\" & currentrow.MEDIA_UID & ".jpg"
            currentrow.FILENAME = currentrow.MEDIA_UID & ".jpg"
            MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
        Else
            With New OpenFileDialog
                .FileName = ""
                .Filter = "All files|*.*"
                If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                    Dim newFilepath As String = gemdb.MediaPath & "\" & currentrow.MEDIA_UID & IO.Path.GetExtension(.FileName)
                    Dim newFilepathShort As String = currentrow.MEDIA_UID & IO.Path.GetExtension(.FileName)
                    If IO.File.Exists(newFilepath) Then
                        If MessageBox.Show("The current record already has a media file associated with it, do you want to overwrite it?", "Overwrite file - " & currentrow.MEDIA_UID & "." & IO.Path.GetExtension(.FileName), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    '##TODO Folder may not exist
                    IO.File.Copy(.FileName, newFilepath, True)
                    currentrow.ORIG_FILEN = .FileName
                    currentrow.FILENAME = newFilepathShort
                    MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
                End If
            End With
        End If
    End Sub


    Private Function getMediaRowFromUID(ByVal mediauid As String) As GEMDataset.MEDIA_DETAILRow
        Return (From row In Me.GEMDataset.MEDIA_DETAIL Where row.MEDIA_UID = mediauid Select row).FirstOrDefault
    End Function

    Private Function getMediaUIDFromSelectedMediaRow() As String
            Return dgMedia.CurrentRow.Cells.Item(1).Value.ToString
    End Function


    Private Sub updateMediaTable()

        Me.Validate()
        dgMedia.EndEdit()
        dgMedia.Update()
        MEDIA_DETAILTableAdapter.Update(Me.GEMDataset.MEDIA_DETAIL)
        MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
    End Sub

    Private Sub cbNO_STOREYS_ABOVE_GROUND_QUAL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNO_STOREYS_ABOVE_GROUND_QUAL.SelectedIndexChanged

        tbNO_STOREYS_ABOVE_GROUND_1.Enabled = cbNO_STOREYS_ABOVE_GROUND_QUAL.Text <> ""

        If cbNO_STOREYS_ABOVE_GROUND_QUAL.Text = "Between" Then
            tbNO_STOREYS_ABOVE_GROUND_2.Enabled = True
        Else
            tbNO_STOREYS_ABOVE_GROUND_2.Enabled = False
            tbNO_STOREYS_ABOVE_GROUND_2.Text = ""
        End If
    End Sub

    Private Sub cbHT_ABOVEGRADE_GRND_FLOOR_QUAL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.SelectedIndexChanged
        tbHT_ABOVEGRADE_GRND_FLOOR_1.Enabled = cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Text <> ""

        If cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Text = "Between" Then
            tbHT_ABOVEGRADE_GRND_FLOOR_2.Enabled = True
        Else
            tbHT_ABOVEGRADE_GRND_FLOOR_2.Enabled = False
            tbHT_ABOVEGRADE_GRND_FLOOR_2.Text = ""
        End If
    End Sub

    Private Sub cbNO_STOREYS_BELOW_GROUND_QUAL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNO_STOREYS_BELOW_GROUND_QUAL.SelectedIndexChanged
        tbNO_STOREYS_BELOW_GROUND_1.Enabled = cbNO_STOREYS_BELOW_GROUND_QUAL.Text <> ""

        If cbNO_STOREYS_BELOW_GROUND_QUAL.Text = "Between" Then
            tbNO_STOREYS_BELOW_GROUND_2.Enabled = True
        Else
            tbNO_STOREYS_BELOW_GROUND_2.Enabled = False
            tbNO_STOREYS_BELOW_GROUND_2.Text = ""
        End If
    End Sub

    Private Sub cbYEAR_BUILT_QUAL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbYEAR_BUILT_QUAL.SelectedIndexChanged
        tbYEAR_BUILT_1.Enabled = cbYEAR_BUILT_QUAL.Text <> ""

        If cbYEAR_BUILT_QUAL.Text = "Between" Then
            tbYEAR_BUILT_2.Enabled = True
        Else
            tbYEAR_BUILT_2.Enabled = False
            tbYEAR_BUILT_2.Text = ""
        End If
    End Sub





    'Private Sub CopyPhotoAsSketchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyPhotoAsSketchToolStripMenuItem.Click
    '    Call CopyPhotoAsSketch()
    'End Sub

    'Private Sub CopyPhotoAsSketch()
    '    '
    '    ' Name:
    '    ' Purpose: To copy the selected photo to the Sketches folder and start up Paint
    '    ' Written: K.Adlam, 
    '    '
    '    ' Get source photo file and check it exists
    '    '
    '    Dim projectPath As String = "E:\EXIF"
    '    Dim photoFile As String ' = dgMedia.CurrentRow.Cells("Comments").Value.ToString
    '    'MsgBox(dgMedia.Columns.Item(3).Name)
    '    photoFile = "Test.jpg"
    '    Dim sourceFile As String = projectPath & "\PHOTOGRAPHS\" & photoFile
    '    '
    '    If (Not IO.File.Exists(sourceFile)) Then
    '        MsgBox("Source photograph " & sourceFile & " cannot be found")
    '        Exit Sub
    '    End If
    '    '
    '    ' Get destination file and check folder exists
    '    '
    '    Dim destFolder As String = projectPath & "\SKETCHES"
    '    If (Not IO.Directory.Exists(destFolder)) Then IO.Directory.CreateDirectory(destFolder)
    '    '
    '    ' Get new ID for the sketch and set destination path
    '    '
    '    Dim strUID As String = System.Guid.NewGuid.ToString
    '    Dim destFile As String = destFolder & "\" & strUID & "." & IO.Path.GetExtension(photoFile)
    '    '
    '    ' Copy the file from the Photos folder to the Sketches folder and open paint
    '    '
    '    IO.File.Copy(sourceFile, destFile)
    '    '
    '    ' Create new row in the MEDIA_DETAILS table for the Sketch
    '    '
    '    Dim newMediaDetailRow As GEMDataset.MEDIA_DETAILRow = GEMDataset.MEDIA_DETAIL.NewMEDIA_DETAILRow
    '    newMediaDetailRow.MEDIA_UID = strUID
    '    newMediaDetailRow.GEMOBJ_UID = mOBJECT_UID
    '    newMediaDetailRow.FILENAME = "test"
    '    newMediaDetailRow.MEDIA_TYPE = "SKETCH"
    '    newMediaDetailRow.COMMENTS = "Sketch on Photo"
    '    GEMDataset.MEDIA_DETAIL.Rows.Add(newMediaDetailRow)
    '    '
    '    ' Open Paint
    '    '
    '    Call OpenPaint(destFile)
    '    '
    'End Sub

    Private Sub tbSLOPE_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSLOPE.TextChanged
        If Val(tbSLOPE.Text) > 90 Then
            MessageBox.Show("Invalid Value")
        End If
    End Sub

    Sub SetLabels(ByVal strLanguageFile As String)
        '
        ' Name: SetLabels
        ' Purpose: To set the Labels on Form at runtime. Labels read from specified file
        ' Written: K.Adlam, 6/3/13
        '
        Try
            If (Not IO.File.Exists(strLanguageFile)) Then Exit Sub
            '
            ' Load Language file into Hashtable
            '
            '
            ' Check if language file exists
            '
            Dim sr As New IO.StreamReader(strLanguageFile)
            '
            ' Loop through each line in language file and replace Names on the form
            '
            Do Until sr.EndOfStream
                Dim arr() As String = sr.ReadLine.Split("|")
                '
                ' Change Control Names
                '
                If (arr.Count = 2) Then
                    Try
                        Dim pControl As Control = Me.Controls.Find(arr(0).Trim, True)(0)

                        If (Not pControl Is Nothing) Then
                            '
                            ' Deal with DataGridView as a special case
                            '
                            If (TypeOf pControl Is DataGridView) Then
                                Dim arr2() As String = arr(1).Trim.Split("#")
                                If (arr2.Length = 2) Then
                                    Dim pDataGridView As DataGridView = pControl
                                    pDataGridView.Columns(arr2(0).Trim).HeaderText = arr2(1).Trim
                                End If

                            Else ' All other controls
                                pControl.Text = arr(1).Trim

                            End If
                        End If
                    Catch
                    End Try
                End If
                '
                ' Change form name
                '
                If (Me.Name = arr(0).Trim) Then Me.Text = arr(1).Trim

            Loop
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


End Class
