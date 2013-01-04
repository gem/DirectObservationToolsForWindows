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

        Me.DIC_OCCUPANCY_DETAILTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY_DETAIL)
        Me.DIC_OCCUPANCYTableAdapter.Fill(Me.GEMDataset.DIC_OCCUPANCY)
        Me.DIC_MATERIAL_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MATERIAL_TYPE)
        Me.DIC_MEDIA_TYPETableAdapter.Fill(Me.GEMDataset.DIC_MEDIA_TYPE)

        Me.MEDIA_DETAILTableAdapter.Fill(Me.GEMDataset.MEDIA_DETAIL)
        Me.GEM_OBJECTTableAdapter.Fill(Me.GEMDataset.GEM_OBJECT)
        Me.CONSEQUENCESTableAdapter.Fill(Me.GEMDataset.CONSEQUENCES)
        Me.GEDTableAdapter.Fill(Me.GEMDataset.GED)

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


    Private Sub filterComboBoxBinding(ByVal firstCombo As ComboBox, ByVal secondCombo As ComboBox, ByVal secondComboBinder As BindingSource)
        If Not firstCombo.SelectedValue Is Nothing And firstCombo.SelectedValue <> "" Then
            Dim previousValue As String = secondCombo.SelectedValue
            If previousValue Is Nothing Then
                previousValue = ""
            End If

            'Get Scope from dictionary
            Dim scopeValue As String = ""
            Try
                Dim dt As DataTable = CType(Me.GEMDataset.Tables(firstCombo.DataSource.DataMember), DataTable)
                Dim dr As DataRow = (From rec In dt.Rows Where rec("CODE") = firstCombo.SelectedValue Select rec).FirstOrDefault
                scopeValue = dr("SCOPE")
            Catch ex As Exception
            End Try

            secondComboBinder.Filter = "SCOPE = '" & scopeValue & "'"
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
        Else
            If newOBJ And close Then
                Dim row As GEMDataset.GEM_OBJECTRow = (From obj In Me.GEMDataset.GEM_OBJECT Where obj.OBJ_UID = mOBJECT_UID).First
                row.Delete()
                GEM_OBJECTTableAdapter.Update(Me.GEMDataset.GEM_OBJECT)
            End If
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

        If Me.GEMDataset.HasChanges Then
            If MessageBox.Show("Are you sure you want to close this form and lose any changes?", "Close form", MessageBoxButtons.YesNo) = vbYes Then
                Me.Close()
            End If
        End If


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
    End Sub

    Private Sub cbMATERIAL_TYPE_T_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMATERIAL_TYPE_T.TextChanged
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMATERIAL_TECHNOLOGY_T, DICMATERIALTECHNOLOGYBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMASONRY_REINFORCEMENT_T, DICMASONRYREINFORCEMENTBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbMASONRY_MORTAR_TYPE_T, DICMASONARYMORTARTYPEBindingSource1)
        filterComboBoxBinding(cbMATERIAL_TYPE_T, cbSTEEL_CONNECTION_TYPE_T, DICSTEELCONNECTIONTYPEBindingSource1)
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
                AddHandler (ctrl.Leave), AddressOf TrackHelpTopicCombo
            ElseIf TypeOf ctrl Is Label Then
                AddHandler (ctrl.Click), AddressOf TrackHelpTopicLabel
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
        Dim helpDir As String = Application.StartupPath & "\LocalResources\Help"
        If (Not IO.Directory.Exists(helpDir)) Then
            MsgBox("Help Folder " & helpDir & " does not exist")
            Exit Sub
        End If
        '
        ' Get path to help file assume that it is an mht file and see if it exists 
        '
        Dim helpFile As String = helpDir & "\" & currentHelpTopic & ".mht"
        If (IO.File.Exists(helpFile)) Then
            WebBrowser1.Navigate(helpFile)
            Exit Sub
        End If
        '
        ' try for htm file
        '
        helpFile = helpDir & "\" & currentHelpTopic & ".htm"
        If (IO.File.Exists(helpFile)) Then
            WebBrowser1.Navigate(helpFile)
            Exit Sub
        End If
        '
        ' Try for any other type of file in top and subdirectories
        '
        Dim helpDirInfo As New IO.DirectoryInfo(helpDir)
        Dim searchPattern As String = IO.Path.GetFileNameWithoutExtension(helpFile) & ".*"
        Dim helpFiles As IO.FileInfo() = helpDirInfo.GetFiles(searchPattern, IO.SearchOption.AllDirectories)
        '
        ' Get first file if there is more than one
        '
        If (helpFiles.Length > 0) Then
            helpFile = helpFiles(0).FullName
        End If
        If (IO.File.Exists(helpFile)) Then
            WebBrowser1.Navigate(helpFile)
            Exit Sub
        End If
        '
        ' If this fails show default htm file
        '
        helpFile = helpDir & "\default.htm"
        If (IO.File.Exists(helpFile)) Then
            WebBrowser1.Navigate(helpFile)
            Exit Sub
        Else 'If all else fails let the user know that there are no help files
            MsgBox("No help file exists for topic """ & currentHelpTopic & """. To add a help file create an htm file with the same name as the topic and place it in the Help folder.")
        End If

    End Sub

    Private Sub TabPage6_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage6.Enter
        Call ShowContextHelp()
    End Sub

    Private Sub dgMedia_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgMedia.CellClick
        If (e.ColumnIndex = -1 And e.RowIndex >= 0) Then
            Call OpenPaint("P:\kama\test2.jpg")
            Exit Sub
        End If
        'If (e.RowIndex < 0) Then Exit Sub
        'Dim pRow As DataGridViewRow = dgMedia.Rows(e.RowIndex)
        ''Dim mediaNumber As String = pRow.Cells("MEDIA_NUMB").Value.ToString
        ''Dim OriginalFilename As String = pRow.Cells("ORIG_FILEN").Value.ToString
        ''Dim gemFilename As String = pRow.Cells("FILENAME").Value.ToString
        ''If (pRow.Cells(e.ColumnIndex).OwningColumn.Name = "MEDIA_NUMB") Then
        '' MsgBox("media")
        ''End If
        ''Dim cellValue As String = pRow.Cells(e.ColumnIndex).Value.ToString
    End Sub

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
        System.Diagnostics.Process.Start("mspaint", strFileName)
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


    Private Sub CopyPhotoAsSketchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyPhotoAsSketchToolStripMenuItem.Click
        Call CopyPhotoAsSketch()
    End Sub

    Private Sub CopyPhotoAsSketch()
        '
        ' Name:
        ' Purpose: To copy the selected photo to the Sketches folder and start up Paint
        ' Written: K.Adlam, 
        '
        ' Get source photo file and check it exists
        '
        Dim projectPath As String = "E:\EXIF"
        Dim photoFile As String ' = dgMedia.CurrentRow.Cells("Comments").Value.ToString
        'MsgBox(dgMedia.Columns.Item(3).Name)
        photoFile = "Test.jpg"
        Dim sourceFile As String = projectPath & "\PHOTOGRAPHS\" & photoFile
        '
        If (Not IO.File.Exists(sourceFile)) Then
            MsgBox("Source photograph " & sourceFile & " cannot be found")
            Exit Sub
        End If
        '
        ' Get destination file and check folder exists
        '
        Dim destFolder As String = projectPath & "\SKETCHES"
        If (Not IO.Directory.Exists(destFolder)) Then IO.Directory.CreateDirectory(destFolder)
        '
        ' Get new ID for the sketch and set destination path
        '
        Dim strUID As String = System.Guid.NewGuid.ToString
        Dim destFile As String = destFolder & "\" & strUID & "." & IO.Path.GetExtension(photoFile)
        '
        ' Copy the file from the Photos folder to the Sketches folder and open paint
        '
        IO.File.Copy(sourceFile, destFile)
        '
        ' Create new row in the MEDIA_DETAILS table for the Sketch
        '
        Dim newMediaDetailRow As GEMDataset.MEDIA_DETAILRow = GEMDataset.MEDIA_DETAIL.NewMEDIA_DETAILRow
        newMediaDetailRow.MEDIA_UID = strUID
        newMediaDetailRow.GEMOBJ_UID = mOBJECT_UID
        newMediaDetailRow.FILENAME = "test"
        newMediaDetailRow.MEDIA_TYPE = "SKETCH"
        newMediaDetailRow.COMMENTS = "Sketch on Photo"
        GEMDataset.MEDIA_DETAIL.Rows.Add(newMediaDetailRow)
        '
        ' Open Paint
        '
        Call OpenPaint(destFile)
        '
    End Sub


    Private Sub ShowMediaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMediaToolStripMenuItem.Click
        MsgBox("Show media file")
        Dim pDataTable As DataTable = Me.GEMDataset.MEDIA_DETAIL
        MsgBox(pDataTable.Rows.Count)
    End Sub
End Class
