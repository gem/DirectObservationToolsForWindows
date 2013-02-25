<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetails))
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.DataGridButton = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.cbMATERIAL_TYPE_L = New System.Windows.Forms.ComboBox()
        Me.GEMOBJECTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GEMDataset = New MapWindow.GEMDataset()
        Me.DICMATERIALTYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMATERIAL_TECHNOLOGY_L = New System.Windows.Forms.ComboBox()
        Me.DICMATERIALTECHNOLOGYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMASONRY_MORTAR_TYPE_L = New System.Windows.Forms.ComboBox()
        Me.DICMASONARYMORTARTYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMASONRY_REINFORCEMENT_L = New System.Windows.Forms.ComboBox()
        Me.DICMASONRYREINFORCEMENTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbSTEEL_CONNECTION_TYPE_L = New System.Windows.Forms.ComboBox()
        Me.DICSTEELCONNECTIONTYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblMTYPE = New System.Windows.Forms.Label()
        Me.lblMTECH = New System.Windows.Forms.Label()
        Me.lblMORT = New System.Windows.Forms.Label()
        Me.lblMREIN = New System.Windows.Forms.Label()
        Me.lblSCONN = New System.Windows.Forms.Label()
        Me.cbSTRUCTURAL_IRREGULARITY = New System.Windows.Forms.ComboBox()
        Me.DICSTRUCTURALIRREGULARITYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbSTRUCTURAL_HORIZ_IRREG_P = New System.Windows.Forms.ComboBox()
        Me.DICSTRUCTURALHORIZIRREGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbSTRUCTURAL_VERT_IRREG_P = New System.Windows.Forms.ComboBox()
        Me.DICSTRUCTURALVERTIRREGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblSTRI = New System.Windows.Forms.Label()
        Me.lblSTRHI = New System.Windows.Forms.Label()
        Me.lblSTRHVI = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.tbLocationX = New System.Windows.Forms.TextBox()
        Me.tbLocationY = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTrans1 = New System.Windows.Forms.Label()
        Me.cbSTEEL_CONNECTION_TYPE_T = New System.Windows.Forms.ComboBox()
        Me.DICSTEELCONNECTIONTYPEBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMASONRY_REINFORCEMENT_T = New System.Windows.Forms.ComboBox()
        Me.DICMASONRYREINFORCEMENTBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMASONRY_MORTAR_TYPE_T = New System.Windows.Forms.ComboBox()
        Me.DICMASONARYMORTARTYPEBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMATERIAL_TECHNOLOGY_T = New System.Windows.Forms.ComboBox()
        Me.DICMATERIALTECHNOLOGYBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbMATERIAL_TYPE_T = New System.Windows.Forms.ComboBox()
        Me.DICMATERIALTYPEBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblLong1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSec1 = New System.Windows.Forms.Label()
        Me.cbSTRUCTURAL_VERT_IRREG_S = New System.Windows.Forms.ComboBox()
        Me.DICSTRUCTURALVERTIRREGBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblPrim1 = New System.Windows.Forms.Label()
        Me.cbSTRUCTURAL_HORIZ_IRREG_S = New System.Windows.Forms.ComboBox()
        Me.DICSTRUCTURALHORIZIRREGBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.tbOBJECT_COMMENTS = New System.Windows.Forms.TextBox()
        Me.cbFavs = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chbAdvancedView = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.btFavSave = New System.Windows.Forms.Button()
        Me.btFavLoad = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS = New System.Windows.Forms.ComboBox()
        Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbFOUNDATION_SYSTEM = New System.Windows.Forms.ComboBox()
        Me.DICFOUNDATIONSYSTEMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblFTYPE = New System.Windows.Forms.Label()
        Me.lblFMAT = New System.Windows.Forms.Label()
        Me.cbFLOOR_TYPE = New System.Windows.Forms.ComboBox()
        Me.DICFLOORTYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbFLOOR_MATERIAL = New System.Windows.Forms.ComboBox()
        Me.DICFLOORMATERIALBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cbROOF_SHAPE = New System.Windows.Forms.ComboBox()
        Me.DICROOFSHAPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbROOF_SYSTEM_TYPE = New System.Windows.Forms.ComboBox()
        Me.DICROOFSYSTEMTYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblRTYPE = New System.Windows.Forms.Label()
        Me.lblRMAT = New System.Windows.Forms.Label()
        Me.cbROOF_SYSTEM_MATERIAL = New System.Windows.Forms.ComboBox()
        Me.DICROOFSYSTEMMATERIALBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbROOF_COVER_MATERIAL = New System.Windows.Forms.ComboBox()
        Me.DICROOFCOVERMATERIALBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbLLRS_DUCTILITY_T = New System.Windows.Forms.ComboBox()
        Me.DICLLRSDUCTILITYBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbLLRS_T = New System.Windows.Forms.ComboBox()
        Me.DICLLRSBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbLLRS_DUCTILITY_L = New System.Windows.Forms.ComboBox()
        Me.DICLLRSDUCTILITYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbLLRS_L = New System.Windows.Forms.ComboBox()
        Me.DICLLRSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblTrans2 = New System.Windows.Forms.Label()
        Me.lblLong2 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblOCCD = New System.Windows.Forms.Label()
        Me.lblOCC = New System.Windows.Forms.Label()
        Me.cbOCCUPANCY_DETAIL = New System.Windows.Forms.ComboBox()
        Me.DICOCCUPANCYDETAILBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbOCCUPANCY = New System.Windows.Forms.ComboBox()
        Me.DICOCCUPANCYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cbPLAN_SHAPE = New System.Windows.Forms.ComboBox()
        Me.DICPLANSHAPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbPOSITION = New System.Windows.Forms.ComboBox()
        Me.DICPOSITIONBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbSLOPE = New MapWindow.NumericTextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL = New System.Windows.Forms.ComboBox()
        Me.DICHEIGHTQUALIFIERBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2 = New MapWindow.NumericTextBox()
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1 = New MapWindow.NumericTextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL = New System.Windows.Forms.ComboBox()
        Me.DICHEIGHTQUALIFIERBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbNO_STOREYS_BELOW_GROUND_2 = New MapWindow.NumericTextBox()
        Me.tbNO_STOREYS_BELOW_GROUND_1 = New MapWindow.NumericTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL = New System.Windows.Forms.ComboBox()
        Me.DICHEIGHTQUALIFIERBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbNO_STOREYS_ABOVE_GROUND_2 = New MapWindow.NumericTextBox()
        Me.tbNO_STOREYS_ABOVE_GROUND_1 = New MapWindow.NumericTextBox()
        Me.lblH1 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.tbYEAR_BUILT_2 = New MapWindow.NumericTextBox()
        Me.cbYEAR_BUILT_QUAL = New System.Windows.Forms.ComboBox()
        Me.DICYEARBUILTQUALBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbYEAR_BUILT_1 = New MapWindow.NumericTextBox()
        Me.lblD1 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.tbCONSEQUENCES_COMMENTS = New System.Windows.Forms.TextBox()
        Me.CONSEQUENCESBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbDAMAGE_CODE = New System.Windows.Forms.ComboBox()
        Me.DICDAMAGEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbNUM_MISSING = New MapWindow.NumericTextBox()
        Me.tbNUM_INJURED = New MapWindow.NumericTextBox()
        Me.tbNUM_FATALITIES = New MapWindow.NumericTextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.tbEXPOSURE_COMMENTS = New System.Windows.Forms.TextBox()
        Me.GEDBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbREPLACEMENT_COST = New MapWindow.NumericTextBox()
        Me.tbPLAN_AREA = New MapWindow.NumericTextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cbCURRENCY = New System.Windows.Forms.ComboBox()
        Me.DICCURRENCYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tbNUM_DWELLINGS = New MapWindow.NumericTextBox()
        Me.tbNUM_TRANSIT_OCCUPANTS = New MapWindow.NumericTextBox()
        Me.tbNUM_NIGHT_OCCUPANTS = New MapWindow.NumericTextBox()
        Me.tbNUM_DAY_OCCUPANTS = New MapWindow.NumericTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.dgMedia = New System.Windows.Forms.DataGridView()
        Me.MEDIATYPEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DICMEDIATYPEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MEDIAUIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEDIANUMBDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORIGFILENDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FILENAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COMMENTSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GEMOBJUIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEDIADETAILBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.btInsertRecord = New System.Windows.Forms.Button()
        Me.btCancelChanges = New System.Windows.Forms.Button()
        Me.btDeleteRecord = New System.Windows.Forms.Button()
        Me.GEM_OBJECTTableAdapter = New MapWindow.GEMDatasetTableAdapters.GEM_OBJECTTableAdapter()
        Me.DIC_MATERIAL_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_MATERIAL_TYPETableAdapter()
        Me.DIC_OCCUPANCYTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_OCCUPANCYTableAdapter()
        Me.DIC_OCCUPANCY_DETAILTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_OCCUPANCY_DETAILTableAdapter()
        Me.MEDIA_DETAILTableAdapter = New MapWindow.GEMDatasetTableAdapters.MEDIA_DETAILTableAdapter()
        Me.DIC_MEDIA_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_MEDIA_TYPETableAdapter()
        Me.CONSEQUENCESTableAdapter = New MapWindow.GEMDatasetTableAdapters.CONSEQUENCESTableAdapter()
        Me.DIC_MATERIAL_TECHNOLOGYTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_MATERIAL_TECHNOLOGYTableAdapter()
        Me.DIC_MASONARY_MORTAR_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_MASONARY_MORTAR_TYPETableAdapter()
        Me.DIC_MASONRY_REINFORCEMENTTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_MASONRY_REINFORCEMENTTableAdapter()
        Me.DIC_STEEL_CONNECTION_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_STEEL_CONNECTION_TYPETableAdapter()
        Me.DIC_STRUCTURAL_IRREGULARITYTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_IRREGULARITYTableAdapter()
        Me.DIC_STRUCTURAL_HORIZ_IRREGTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_HORIZ_IRREGTableAdapter()
        Me.DIC_STRUCTURAL_VERT_IRREGTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_VERT_IRREGTableAdapter()
        Me.DIC_LLRSTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_LLRSTableAdapter()
        Me.DIC_LLRS_DUCTILITYTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_LLRS_DUCTILITYTableAdapter()
        Me.DIC_ROOF_SHAPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SHAPETableAdapter()
        Me.DIC_ROOF_COVER_MATERIALTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_ROOF_COVER_MATERIALTableAdapter()
        Me.DIC_ROOF_SYSTEM_MATERIALTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SYSTEM_MATERIALTableAdapter()
        Me.DIC_ROOF_SYSTEM_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SYSTEM_TYPETableAdapter()
        Me.DIC_FLOOR_MATERIALTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_FLOOR_MATERIALTableAdapter()
        Me.DIC_FLOOR_TYPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_FLOOR_TYPETableAdapter()
        Me.DIC_FOUNDATION_SYSTEMTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_FOUNDATION_SYSTEMTableAdapter()
        Me.DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter()
        Me.DIC_YEAR_BUILT_QUALTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_YEAR_BUILT_QUALTableAdapter()
        Me.DIC_POSITIONTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_POSITIONTableAdapter()
        Me.GEDTableAdapter = New MapWindow.GEMDatasetTableAdapters.GEDTableAdapter()
        Me.DIC_PLAN_SHAPETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_PLAN_SHAPETableAdapter()
        Me.DIC_CURRENCYTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_CURRENCYTableAdapter()
        Me.DIC_DAMAGETableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_DAMAGETableAdapter()
        Me.DIC_HEIGHT_QUALIFIERTableAdapter = New MapWindow.GEMDatasetTableAdapters.DIC_HEIGHT_QUALIFIERTableAdapter()
        Me.mnuRow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowMediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinkToMediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.GEMOBJECTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GEMDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMATERIALTYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMATERIALTECHNOLOGYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMASONARYMORTARTYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMASONRYREINFORCEMENTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICSTEELCONNECTIONTYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICSTRUCTURALIRREGULARITYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICSTRUCTURALHORIZIRREGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICSTRUCTURALVERTIRREGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DICSTEELCONNECTIONTYPEBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMASONRYREINFORCEMENTBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMASONARYMORTARTYPEBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMATERIALTECHNOLOGYBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMATERIALTYPEBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DICSTRUCTURALVERTIRREGBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICSTRUCTURALHORIZIRREGBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        CType(Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.DICFOUNDATIONSYSTEMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICFLOORTYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICFLOORMATERIALBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DICROOFSHAPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICROOFSYSTEMTYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICROOFSYSTEMMATERIALBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICROOFCOVERMATERIALBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DICLLRSDUCTILITYBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICLLRSBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICLLRSDUCTILITYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICLLRSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DICOCCUPANCYDETAILBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICOCCUPANCYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox14.SuspendLayout()
        CType(Me.DICPLANSHAPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICPOSITIONBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox13.SuspendLayout()
        CType(Me.DICHEIGHTQUALIFIERBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICHEIGHTQUALIFIERBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICHEIGHTQUALIFIERBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        CType(Me.DICYEARBUILTQUALBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        CType(Me.CONSEQUENCESBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICDAMAGEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox15.SuspendLayout()
        CType(Me.GEDBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICCURRENCYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        CType(Me.dgMedia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DICMEDIATYPEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MEDIADETAILBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.mnuRow.SuspendLayout()
        Me.SuspendLayout()
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(526, 821)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(77, 37)
        Me.DeleteButton.TabIndex = 5
        Me.DeleteButton.Text = "delete"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(528, 879)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(77, 38)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "populate combo"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DataGridButton
        '
        Me.DataGridButton.Location = New System.Drawing.Point(529, 934)
        Me.DataGridButton.Name = "DataGridButton"
        Me.DataGridButton.Size = New System.Drawing.Size(74, 39)
        Me.DataGridButton.TabIndex = 8
        Me.DataGridButton.Text = "Populate Datagrid"
        Me.DataGridButton.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(533, 979)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(71, 40)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "Insert Multi"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(511, 1047)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(108, 34)
        Me.Button6.TabIndex = 10
        Me.Button6.Text = "Button6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'cbMATERIAL_TYPE_L
        '
        Me.cbMATERIAL_TYPE_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAT_TYPE_L", True))
        Me.cbMATERIAL_TYPE_L.DataSource = Me.DICMATERIALTYPEBindingSource
        Me.cbMATERIAL_TYPE_L.DisplayMember = "DESCRIPTION"
        Me.cbMATERIAL_TYPE_L.DropDownHeight = 400
        Me.cbMATERIAL_TYPE_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMATERIAL_TYPE_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMATERIAL_TYPE_L.FormattingEnabled = True
        Me.cbMATERIAL_TYPE_L.IntegralHeight = False
        Me.cbMATERIAL_TYPE_L.Location = New System.Drawing.Point(206, 35)
        Me.cbMATERIAL_TYPE_L.Name = "cbMATERIAL_TYPE_L"
        Me.cbMATERIAL_TYPE_L.Size = New System.Drawing.Size(254, 24)
        Me.cbMATERIAL_TYPE_L.TabIndex = 11
        Me.cbMATERIAL_TYPE_L.ValueMember = "CODE"
        '
        'GEMOBJECTBindingSource
        '
        Me.GEMOBJECTBindingSource.DataMember = "GEM_OBJECT"
        Me.GEMOBJECTBindingSource.DataSource = Me.GEMDataset
        '
        'GEMDataset
        '
        Me.GEMDataset.DataSetName = "GEMDataset"
        Me.GEMDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DICMATERIALTYPEBindingSource
        '
        Me.DICMATERIALTYPEBindingSource.DataMember = "DIC_MATERIAL_TYPE"
        Me.DICMATERIALTYPEBindingSource.DataSource = Me.GEMDataset
        '
        'cbMATERIAL_TECHNOLOGY_L
        '
        Me.cbMATERIAL_TECHNOLOGY_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAT_TECH_L", True))
        Me.cbMATERIAL_TECHNOLOGY_L.DataSource = Me.DICMATERIALTECHNOLOGYBindingSource
        Me.cbMATERIAL_TECHNOLOGY_L.DisplayMember = "DESCRIPTION"
        Me.cbMATERIAL_TECHNOLOGY_L.DropDownHeight = 400
        Me.cbMATERIAL_TECHNOLOGY_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMATERIAL_TECHNOLOGY_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMATERIAL_TECHNOLOGY_L.FormattingEnabled = True
        Me.cbMATERIAL_TECHNOLOGY_L.IntegralHeight = False
        Me.cbMATERIAL_TECHNOLOGY_L.Location = New System.Drawing.Point(206, 62)
        Me.cbMATERIAL_TECHNOLOGY_L.Name = "cbMATERIAL_TECHNOLOGY_L"
        Me.cbMATERIAL_TECHNOLOGY_L.Size = New System.Drawing.Size(254, 24)
        Me.cbMATERIAL_TECHNOLOGY_L.TabIndex = 12
        Me.cbMATERIAL_TECHNOLOGY_L.ValueMember = "CODE"
        '
        'DICMATERIALTECHNOLOGYBindingSource
        '
        Me.DICMATERIALTECHNOLOGYBindingSource.DataMember = "DIC_MATERIAL_TECHNOLOGY"
        Me.DICMATERIALTECHNOLOGYBindingSource.DataSource = Me.GEMDataset
        '
        'cbMASONRY_MORTAR_TYPE_L
        '
        Me.cbMASONRY_MORTAR_TYPE_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAS_MORT_L", True))
        Me.cbMASONRY_MORTAR_TYPE_L.DataSource = Me.DICMASONARYMORTARTYPEBindingSource
        Me.cbMASONRY_MORTAR_TYPE_L.DisplayMember = "DESCRIPTION"
        Me.cbMASONRY_MORTAR_TYPE_L.DropDownHeight = 400
        Me.cbMASONRY_MORTAR_TYPE_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMASONRY_MORTAR_TYPE_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMASONRY_MORTAR_TYPE_L.FormattingEnabled = True
        Me.cbMASONRY_MORTAR_TYPE_L.IntegralHeight = False
        Me.cbMASONRY_MORTAR_TYPE_L.Location = New System.Drawing.Point(206, 89)
        Me.cbMASONRY_MORTAR_TYPE_L.Name = "cbMASONRY_MORTAR_TYPE_L"
        Me.cbMASONRY_MORTAR_TYPE_L.Size = New System.Drawing.Size(254, 24)
        Me.cbMASONRY_MORTAR_TYPE_L.TabIndex = 13
        Me.cbMASONRY_MORTAR_TYPE_L.ValueMember = "CODE"
        '
        'DICMASONARYMORTARTYPEBindingSource
        '
        Me.DICMASONARYMORTARTYPEBindingSource.DataMember = "DIC_MASONARY_MORTAR_TYPE"
        Me.DICMASONARYMORTARTYPEBindingSource.DataSource = Me.GEMDataset
        '
        'cbMASONRY_REINFORCEMENT_L
        '
        Me.cbMASONRY_REINFORCEMENT_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAS_REIN_L", True))
        Me.cbMASONRY_REINFORCEMENT_L.DataSource = Me.DICMASONRYREINFORCEMENTBindingSource
        Me.cbMASONRY_REINFORCEMENT_L.DisplayMember = "DESCRIPTION"
        Me.cbMASONRY_REINFORCEMENT_L.DropDownHeight = 400
        Me.cbMASONRY_REINFORCEMENT_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMASONRY_REINFORCEMENT_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMASONRY_REINFORCEMENT_L.FormattingEnabled = True
        Me.cbMASONRY_REINFORCEMENT_L.IntegralHeight = False
        Me.cbMASONRY_REINFORCEMENT_L.Location = New System.Drawing.Point(206, 116)
        Me.cbMASONRY_REINFORCEMENT_L.Name = "cbMASONRY_REINFORCEMENT_L"
        Me.cbMASONRY_REINFORCEMENT_L.Size = New System.Drawing.Size(254, 24)
        Me.cbMASONRY_REINFORCEMENT_L.TabIndex = 14
        Me.cbMASONRY_REINFORCEMENT_L.ValueMember = "CODE"
        '
        'DICMASONRYREINFORCEMENTBindingSource
        '
        Me.DICMASONRYREINFORCEMENTBindingSource.DataMember = "DIC_MASONRY_REINFORCEMENT"
        Me.DICMASONRYREINFORCEMENTBindingSource.DataSource = Me.GEMDataset
        '
        'cbSTEEL_CONNECTION_TYPE_L
        '
        Me.cbSTEEL_CONNECTION_TYPE_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STEELCON_L", True))
        Me.cbSTEEL_CONNECTION_TYPE_L.DataSource = Me.DICSTEELCONNECTIONTYPEBindingSource
        Me.cbSTEEL_CONNECTION_TYPE_L.DisplayMember = "DESCRIPTION"
        Me.cbSTEEL_CONNECTION_TYPE_L.DropDownHeight = 400
        Me.cbSTEEL_CONNECTION_TYPE_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTEEL_CONNECTION_TYPE_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTEEL_CONNECTION_TYPE_L.FormattingEnabled = True
        Me.cbSTEEL_CONNECTION_TYPE_L.IntegralHeight = False
        Me.cbSTEEL_CONNECTION_TYPE_L.Location = New System.Drawing.Point(206, 143)
        Me.cbSTEEL_CONNECTION_TYPE_L.Name = "cbSTEEL_CONNECTION_TYPE_L"
        Me.cbSTEEL_CONNECTION_TYPE_L.Size = New System.Drawing.Size(254, 24)
        Me.cbSTEEL_CONNECTION_TYPE_L.TabIndex = 15
        Me.cbSTEEL_CONNECTION_TYPE_L.ValueMember = "CODE"
        '
        'DICSTEELCONNECTIONTYPEBindingSource
        '
        Me.DICSTEELCONNECTIONTYPEBindingSource.DataMember = "DIC_STEEL_CONNECTION_TYPE"
        Me.DICSTEELCONNECTIONTYPEBindingSource.DataSource = Me.GEMDataset
        '
        'lblMTYPE
        '
        Me.lblMTYPE.AutoSize = True
        Me.lblMTYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMTYPE.Location = New System.Drawing.Point(107, 38)
        Me.lblMTYPE.Name = "lblMTYPE"
        Me.lblMTYPE.Size = New System.Drawing.Size(91, 16)
        Me.lblMTYPE.TabIndex = 20
        Me.lblMTYPE.Text = "Material Type"
        '
        'lblMTECH
        '
        Me.lblMTECH.AutoSize = True
        Me.lblMTECH.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMTECH.Location = New System.Drawing.Point(67, 65)
        Me.lblMTECH.Name = "lblMTECH"
        Me.lblMTECH.Size = New System.Drawing.Size(131, 16)
        Me.lblMTECH.TabIndex = 21
        Me.lblMTECH.Text = "Material Technology"
        '
        'lblMORT
        '
        Me.lblMORT.AutoSize = True
        Me.lblMORT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMORT.Location = New System.Drawing.Point(53, 92)
        Me.lblMORT.Name = "lblMORT"
        Me.lblMORT.Size = New System.Drawing.Size(144, 16)
        Me.lblMORT.TabIndex = 22
        Me.lblMORT.Text = "Masonary Mortar Type"
        '
        'lblMREIN
        '
        Me.lblMREIN.AutoSize = True
        Me.lblMREIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMREIN.Location = New System.Drawing.Point(39, 119)
        Me.lblMREIN.Name = "lblMREIN"
        Me.lblMREIN.Size = New System.Drawing.Size(158, 16)
        Me.lblMREIN.TabIndex = 23
        Me.lblMREIN.Text = "Masonary Reinforcement"
        '
        'lblSCONN
        '
        Me.lblSCONN.AutoSize = True
        Me.lblSCONN.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSCONN.Location = New System.Drawing.Point(53, 143)
        Me.lblSCONN.Name = "lblSCONN"
        Me.lblSCONN.Size = New System.Drawing.Size(144, 16)
        Me.lblSCONN.TabIndex = 24
        Me.lblSCONN.Text = "Steel Connection Type"
        '
        'cbSTRUCTURAL_IRREGULARITY
        '
        Me.cbSTRUCTURAL_IRREGULARITY.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STR_IRREG", True))
        Me.cbSTRUCTURAL_IRREGULARITY.DataSource = Me.DICSTRUCTURALIRREGULARITYBindingSource
        Me.cbSTRUCTURAL_IRREGULARITY.DisplayMember = "DESCRIPTION"
        Me.cbSTRUCTURAL_IRREGULARITY.DropDownHeight = 400
        Me.cbSTRUCTURAL_IRREGULARITY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTRUCTURAL_IRREGULARITY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTRUCTURAL_IRREGULARITY.FormattingEnabled = True
        Me.cbSTRUCTURAL_IRREGULARITY.IntegralHeight = False
        Me.cbSTRUCTURAL_IRREGULARITY.Location = New System.Drawing.Point(208, 37)
        Me.cbSTRUCTURAL_IRREGULARITY.Name = "cbSTRUCTURAL_IRREGULARITY"
        Me.cbSTRUCTURAL_IRREGULARITY.Size = New System.Drawing.Size(254, 24)
        Me.cbSTRUCTURAL_IRREGULARITY.TabIndex = 33
        Me.cbSTRUCTURAL_IRREGULARITY.ValueMember = "CODE"
        '
        'DICSTRUCTURALIRREGULARITYBindingSource
        '
        Me.DICSTRUCTURALIRREGULARITYBindingSource.DataMember = "DIC_STRUCTURAL_IRREGULARITY"
        Me.DICSTRUCTURALIRREGULARITYBindingSource.DataSource = Me.GEMDataset
        '
        'cbSTRUCTURAL_HORIZ_IRREG_P
        '
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STR_HZIR_P", True))
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.DataSource = Me.DICSTRUCTURALHORIZIRREGBindingSource
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.DisplayMember = "DESCRIPTION"
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.DropDownHeight = 400
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.FormattingEnabled = True
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.IntegralHeight = False
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.Location = New System.Drawing.Point(208, 64)
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.Name = "cbSTRUCTURAL_HORIZ_IRREG_P"
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.Size = New System.Drawing.Size(254, 24)
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.TabIndex = 34
        Me.cbSTRUCTURAL_HORIZ_IRREG_P.ValueMember = "CODE"
        '
        'DICSTRUCTURALHORIZIRREGBindingSource
        '
        Me.DICSTRUCTURALHORIZIRREGBindingSource.DataMember = "DIC_STRUCTURAL_HORIZ_IRREG"
        Me.DICSTRUCTURALHORIZIRREGBindingSource.DataSource = Me.GEMDataset
        '
        'cbSTRUCTURAL_VERT_IRREG_P
        '
        Me.cbSTRUCTURAL_VERT_IRREG_P.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STR_VEIR_P", True))
        Me.cbSTRUCTURAL_VERT_IRREG_P.DataSource = Me.DICSTRUCTURALVERTIRREGBindingSource
        Me.cbSTRUCTURAL_VERT_IRREG_P.DisplayMember = "DESCRIPTION"
        Me.cbSTRUCTURAL_VERT_IRREG_P.DropDownHeight = 400
        Me.cbSTRUCTURAL_VERT_IRREG_P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTRUCTURAL_VERT_IRREG_P.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTRUCTURAL_VERT_IRREG_P.FormattingEnabled = True
        Me.cbSTRUCTURAL_VERT_IRREG_P.IntegralHeight = False
        Me.cbSTRUCTURAL_VERT_IRREG_P.Location = New System.Drawing.Point(208, 91)
        Me.cbSTRUCTURAL_VERT_IRREG_P.Name = "cbSTRUCTURAL_VERT_IRREG_P"
        Me.cbSTRUCTURAL_VERT_IRREG_P.Size = New System.Drawing.Size(254, 24)
        Me.cbSTRUCTURAL_VERT_IRREG_P.TabIndex = 35
        Me.cbSTRUCTURAL_VERT_IRREG_P.ValueMember = "CODE"
        '
        'DICSTRUCTURALVERTIRREGBindingSource
        '
        Me.DICSTRUCTURALVERTIRREGBindingSource.DataMember = "DIC_STRUCTURAL_VERT_IRREG"
        Me.DICSTRUCTURALVERTIRREGBindingSource.DataSource = Me.GEMDataset
        '
        'lblSTRI
        '
        Me.lblSTRI.AutoSize = True
        Me.lblSTRI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTRI.Location = New System.Drawing.Point(71, 40)
        Me.lblSTRI.Name = "lblSTRI"
        Me.lblSTRI.Size = New System.Drawing.Size(128, 16)
        Me.lblSTRI.TabIndex = 38
        Me.lblSTRI.Text = "Structural Irregularity"
        '
        'lblSTRHI
        '
        Me.lblSTRHI.AutoSize = True
        Me.lblSTRHI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTRHI.Location = New System.Drawing.Point(8, 70)
        Me.lblSTRHI.Name = "lblSTRHI"
        Me.lblSTRHI.Size = New System.Drawing.Size(191, 16)
        Me.lblSTRHI.TabIndex = 39
        Me.lblSTRHI.Text = "Structural Horizontal Irregularity"
        '
        'lblSTRHVI
        '
        Me.lblSTRHVI.AutoSize = True
        Me.lblSTRHVI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTRHVI.Location = New System.Drawing.Point(23, 99)
        Me.lblSTRHVI.Name = "lblSTRHVI"
        Me.lblSTRHVI.Size = New System.Drawing.Size(176, 16)
        Me.lblSTRHVI.TabIndex = 40
        Me.lblSTRHVI.Text = "Structural Vertical Irregularity"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 26)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(19, 16)
        Me.Label19.TabIndex = 58
        Me.Label19.Text = "X:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(165, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(20, 16)
        Me.Label21.TabIndex = 59
        Me.Label21.Text = "Y:"
        '
        'tbLocationX
        '
        Me.tbLocationX.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "X", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbLocationX.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLocationX.Location = New System.Drawing.Point(31, 22)
        Me.tbLocationX.Name = "tbLocationX"
        Me.tbLocationX.Size = New System.Drawing.Size(94, 22)
        Me.tbLocationX.TabIndex = 60
        '
        'tbLocationY
        '
        Me.tbLocationY.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "Y", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbLocationY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLocationY.Location = New System.Drawing.Point(191, 20)
        Me.tbLocationY.Name = "tbLocationY"
        Me.tbLocationY.Size = New System.Drawing.Size(94, 22)
        Me.tbLocationY.TabIndex = 61
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTrans1)
        Me.GroupBox1.Controls.Add(Me.cbSTEEL_CONNECTION_TYPE_T)
        Me.GroupBox1.Controls.Add(Me.cbMASONRY_REINFORCEMENT_T)
        Me.GroupBox1.Controls.Add(Me.cbMASONRY_MORTAR_TYPE_T)
        Me.GroupBox1.Controls.Add(Me.cbMATERIAL_TECHNOLOGY_T)
        Me.GroupBox1.Controls.Add(Me.cbMATERIAL_TYPE_T)
        Me.GroupBox1.Controls.Add(Me.lblSCONN)
        Me.GroupBox1.Controls.Add(Me.lblMREIN)
        Me.GroupBox1.Controls.Add(Me.lblMORT)
        Me.GroupBox1.Controls.Add(Me.lblMTECH)
        Me.GroupBox1.Controls.Add(Me.lblLong1)
        Me.GroupBox1.Controls.Add(Me.lblMTYPE)
        Me.GroupBox1.Controls.Add(Me.cbSTEEL_CONNECTION_TYPE_L)
        Me.GroupBox1.Controls.Add(Me.cbMASONRY_REINFORCEMENT_L)
        Me.GroupBox1.Controls.Add(Me.cbMASONRY_MORTAR_TYPE_L)
        Me.GroupBox1.Controls.Add(Me.cbMATERIAL_TECHNOLOGY_L)
        Me.GroupBox1.Controls.Add(Me.cbMATERIAL_TYPE_L)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(11, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(796, 189)
        Me.GroupBox1.TabIndex = 62
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Building Materials"
        '
        'lblTrans1
        '
        Me.lblTrans1.AutoSize = True
        Me.lblTrans1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrans1.Location = New System.Drawing.Point(598, 16)
        Me.lblTrans1.Name = "lblTrans1"
        Me.lblTrans1.Size = New System.Drawing.Size(77, 16)
        Me.lblTrans1.TabIndex = 30
        Me.lblTrans1.Text = "Transverse"
        '
        'cbSTEEL_CONNECTION_TYPE_T
        '
        Me.cbSTEEL_CONNECTION_TYPE_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STEELCON_T", True))
        Me.cbSTEEL_CONNECTION_TYPE_T.DataSource = Me.DICSTEELCONNECTIONTYPEBindingSource1
        Me.cbSTEEL_CONNECTION_TYPE_T.DisplayMember = "DESCRIPTION"
        Me.cbSTEEL_CONNECTION_TYPE_T.DropDownHeight = 400
        Me.cbSTEEL_CONNECTION_TYPE_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTEEL_CONNECTION_TYPE_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTEEL_CONNECTION_TYPE_T.FormattingEnabled = True
        Me.cbSTEEL_CONNECTION_TYPE_T.IntegralHeight = False
        Me.cbSTEEL_CONNECTION_TYPE_T.Location = New System.Drawing.Point(511, 143)
        Me.cbSTEEL_CONNECTION_TYPE_T.Name = "cbSTEEL_CONNECTION_TYPE_T"
        Me.cbSTEEL_CONNECTION_TYPE_T.Size = New System.Drawing.Size(254, 24)
        Me.cbSTEEL_CONNECTION_TYPE_T.TabIndex = 29
        Me.cbSTEEL_CONNECTION_TYPE_T.ValueMember = "CODE"
        '
        'DICSTEELCONNECTIONTYPEBindingSource1
        '
        Me.DICSTEELCONNECTIONTYPEBindingSource1.DataMember = "DIC_STEEL_CONNECTION_TYPE"
        Me.DICSTEELCONNECTIONTYPEBindingSource1.DataSource = Me.GEMDataset
        '
        'cbMASONRY_REINFORCEMENT_T
        '
        Me.cbMASONRY_REINFORCEMENT_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAS_REIN_T", True))
        Me.cbMASONRY_REINFORCEMENT_T.DataSource = Me.DICMASONRYREINFORCEMENTBindingSource1
        Me.cbMASONRY_REINFORCEMENT_T.DisplayMember = "DESCRIPTION"
        Me.cbMASONRY_REINFORCEMENT_T.DropDownHeight = 400
        Me.cbMASONRY_REINFORCEMENT_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMASONRY_REINFORCEMENT_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMASONRY_REINFORCEMENT_T.FormattingEnabled = True
        Me.cbMASONRY_REINFORCEMENT_T.IntegralHeight = False
        Me.cbMASONRY_REINFORCEMENT_T.Location = New System.Drawing.Point(511, 116)
        Me.cbMASONRY_REINFORCEMENT_T.Name = "cbMASONRY_REINFORCEMENT_T"
        Me.cbMASONRY_REINFORCEMENT_T.Size = New System.Drawing.Size(254, 24)
        Me.cbMASONRY_REINFORCEMENT_T.TabIndex = 28
        Me.cbMASONRY_REINFORCEMENT_T.ValueMember = "CODE"
        '
        'DICMASONRYREINFORCEMENTBindingSource1
        '
        Me.DICMASONRYREINFORCEMENTBindingSource1.DataMember = "DIC_MASONRY_REINFORCEMENT"
        Me.DICMASONRYREINFORCEMENTBindingSource1.DataSource = Me.GEMDataset
        '
        'cbMASONRY_MORTAR_TYPE_T
        '
        Me.cbMASONRY_MORTAR_TYPE_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAS_MORT_T", True))
        Me.cbMASONRY_MORTAR_TYPE_T.DataSource = Me.DICMASONARYMORTARTYPEBindingSource1
        Me.cbMASONRY_MORTAR_TYPE_T.DisplayMember = "DESCRIPTION"
        Me.cbMASONRY_MORTAR_TYPE_T.DropDownHeight = 400
        Me.cbMASONRY_MORTAR_TYPE_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMASONRY_MORTAR_TYPE_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMASONRY_MORTAR_TYPE_T.FormattingEnabled = True
        Me.cbMASONRY_MORTAR_TYPE_T.IntegralHeight = False
        Me.cbMASONRY_MORTAR_TYPE_T.Location = New System.Drawing.Point(511, 89)
        Me.cbMASONRY_MORTAR_TYPE_T.Name = "cbMASONRY_MORTAR_TYPE_T"
        Me.cbMASONRY_MORTAR_TYPE_T.Size = New System.Drawing.Size(254, 24)
        Me.cbMASONRY_MORTAR_TYPE_T.TabIndex = 27
        Me.cbMASONRY_MORTAR_TYPE_T.ValueMember = "CODE"
        '
        'DICMASONARYMORTARTYPEBindingSource1
        '
        Me.DICMASONARYMORTARTYPEBindingSource1.DataMember = "DIC_MASONARY_MORTAR_TYPE"
        Me.DICMASONARYMORTARTYPEBindingSource1.DataSource = Me.GEMDataset
        '
        'cbMATERIAL_TECHNOLOGY_T
        '
        Me.cbMATERIAL_TECHNOLOGY_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAT_TECH_T", True))
        Me.cbMATERIAL_TECHNOLOGY_T.DataSource = Me.DICMATERIALTECHNOLOGYBindingSource1
        Me.cbMATERIAL_TECHNOLOGY_T.DisplayMember = "DESCRIPTION"
        Me.cbMATERIAL_TECHNOLOGY_T.DropDownHeight = 400
        Me.cbMATERIAL_TECHNOLOGY_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMATERIAL_TECHNOLOGY_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMATERIAL_TECHNOLOGY_T.FormattingEnabled = True
        Me.cbMATERIAL_TECHNOLOGY_T.IntegralHeight = False
        Me.cbMATERIAL_TECHNOLOGY_T.Location = New System.Drawing.Point(511, 62)
        Me.cbMATERIAL_TECHNOLOGY_T.Name = "cbMATERIAL_TECHNOLOGY_T"
        Me.cbMATERIAL_TECHNOLOGY_T.Size = New System.Drawing.Size(254, 24)
        Me.cbMATERIAL_TECHNOLOGY_T.TabIndex = 26
        Me.cbMATERIAL_TECHNOLOGY_T.ValueMember = "CODE"
        '
        'DICMATERIALTECHNOLOGYBindingSource1
        '
        Me.DICMATERIALTECHNOLOGYBindingSource1.DataMember = "DIC_MATERIAL_TECHNOLOGY"
        Me.DICMATERIALTECHNOLOGYBindingSource1.DataSource = Me.GEMDataset
        '
        'cbMATERIAL_TYPE_T
        '
        Me.cbMATERIAL_TYPE_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "MAT_TYPE_T", True))
        Me.cbMATERIAL_TYPE_T.DataSource = Me.DICMATERIALTYPEBindingSource1
        Me.cbMATERIAL_TYPE_T.DisplayMember = "DESCRIPTION"
        Me.cbMATERIAL_TYPE_T.DropDownHeight = 400
        Me.cbMATERIAL_TYPE_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMATERIAL_TYPE_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMATERIAL_TYPE_T.FormattingEnabled = True
        Me.cbMATERIAL_TYPE_T.IntegralHeight = False
        Me.cbMATERIAL_TYPE_T.Location = New System.Drawing.Point(511, 35)
        Me.cbMATERIAL_TYPE_T.Name = "cbMATERIAL_TYPE_T"
        Me.cbMATERIAL_TYPE_T.Size = New System.Drawing.Size(254, 24)
        Me.cbMATERIAL_TYPE_T.TabIndex = 25
        Me.cbMATERIAL_TYPE_T.ValueMember = "CODE"
        '
        'DICMATERIALTYPEBindingSource1
        '
        Me.DICMATERIALTYPEBindingSource1.DataMember = "DIC_MATERIAL_TYPE"
        Me.DICMATERIALTYPEBindingSource1.DataSource = Me.GEMDataset
        '
        'lblLong1
        '
        Me.lblLong1.AutoSize = True
        Me.lblLong1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLong1.Location = New System.Drawing.Point(288, 16)
        Me.lblLong1.Name = "lblLong1"
        Me.lblLong1.Size = New System.Drawing.Size(80, 16)
        Me.lblLong1.TabIndex = 20
        Me.lblLong1.Text = "Longitudinal"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblSec1)
        Me.GroupBox2.Controls.Add(Me.cbSTRUCTURAL_VERT_IRREG_S)
        Me.GroupBox2.Controls.Add(Me.lblPrim1)
        Me.GroupBox2.Controls.Add(Me.cbSTRUCTURAL_HORIZ_IRREG_S)
        Me.GroupBox2.Controls.Add(Me.lblSTRHVI)
        Me.GroupBox2.Controls.Add(Me.lblSTRHI)
        Me.GroupBox2.Controls.Add(Me.lblSTRI)
        Me.GroupBox2.Controls.Add(Me.cbSTRUCTURAL_VERT_IRREG_P)
        Me.GroupBox2.Controls.Add(Me.cbSTRUCTURAL_HORIZ_IRREG_P)
        Me.GroupBox2.Controls.Add(Me.cbSTRUCTURAL_IRREGULARITY)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 295)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(796, 129)
        Me.GroupBox2.TabIndex = 63
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Structural Irregularity"
        '
        'lblSec1
        '
        Me.lblSec1.AutoSize = True
        Me.lblSec1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSec1.Location = New System.Drawing.Point(605, 45)
        Me.lblSec1.Name = "lblSec1"
        Me.lblSec1.Size = New System.Drawing.Size(74, 16)
        Me.lblSec1.TabIndex = 32
        Me.lblSec1.Text = "Secondary"
        '
        'cbSTRUCTURAL_VERT_IRREG_S
        '
        Me.cbSTRUCTURAL_VERT_IRREG_S.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STR_VEIR_S", True))
        Me.cbSTRUCTURAL_VERT_IRREG_S.DataSource = Me.DICSTRUCTURALVERTIRREGBindingSource1
        Me.cbSTRUCTURAL_VERT_IRREG_S.DisplayMember = "DESCRIPTION"
        Me.cbSTRUCTURAL_VERT_IRREG_S.DropDownHeight = 400
        Me.cbSTRUCTURAL_VERT_IRREG_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTRUCTURAL_VERT_IRREG_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTRUCTURAL_VERT_IRREG_S.FormattingEnabled = True
        Me.cbSTRUCTURAL_VERT_IRREG_S.IntegralHeight = False
        Me.cbSTRUCTURAL_VERT_IRREG_S.Location = New System.Drawing.Point(511, 91)
        Me.cbSTRUCTURAL_VERT_IRREG_S.Name = "cbSTRUCTURAL_VERT_IRREG_S"
        Me.cbSTRUCTURAL_VERT_IRREG_S.Size = New System.Drawing.Size(254, 24)
        Me.cbSTRUCTURAL_VERT_IRREG_S.TabIndex = 43
        Me.cbSTRUCTURAL_VERT_IRREG_S.ValueMember = "CODE"
        '
        'DICSTRUCTURALVERTIRREGBindingSource1
        '
        Me.DICSTRUCTURALVERTIRREGBindingSource1.DataMember = "DIC_STRUCTURAL_VERT_IRREG"
        Me.DICSTRUCTURALVERTIRREGBindingSource1.DataSource = Me.GEMDataset
        '
        'lblPrim1
        '
        Me.lblPrim1.AutoSize = True
        Me.lblPrim1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrim1.Location = New System.Drawing.Point(306, 18)
        Me.lblPrim1.Name = "lblPrim1"
        Me.lblPrim1.Size = New System.Drawing.Size(54, 16)
        Me.lblPrim1.TabIndex = 31
        Me.lblPrim1.Text = "Primary"
        '
        'cbSTRUCTURAL_HORIZ_IRREG_S
        '
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STR_HZIR_S", True))
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.DataSource = Me.DICSTRUCTURALHORIZIRREGBindingSource1
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.DisplayMember = "DESCRIPTION"
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.DropDownHeight = 400
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.FormattingEnabled = True
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.IntegralHeight = False
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.Location = New System.Drawing.Point(511, 64)
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.Name = "cbSTRUCTURAL_HORIZ_IRREG_S"
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.Size = New System.Drawing.Size(254, 24)
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.TabIndex = 42
        Me.cbSTRUCTURAL_HORIZ_IRREG_S.ValueMember = "CODE"
        '
        'DICSTRUCTURALHORIZIRREGBindingSource1
        '
        Me.DICSTRUCTURALHORIZIRREGBindingSource1.DataMember = "DIC_STRUCTURAL_HORIZ_IRREG"
        Me.DICSTRUCTURALHORIZIRREGBindingSource1.DataSource = Me.GEMDataset
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.tbLocationY)
        Me.GroupBox8.Controls.Add(Me.tbLocationX)
        Me.GroupBox8.Controls.Add(Me.Label21)
        Me.GroupBox8.Controls.Add(Me.Label19)
        Me.GroupBox8.Location = New System.Drawing.Point(491, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(315, 56)
        Me.GroupBox8.TabIndex = 70
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Location"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.tbOBJECT_COMMENTS)
        Me.GroupBox9.Location = New System.Drawing.Point(11, 443)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(796, 135)
        Me.GroupBox9.TabIndex = 71
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "General Building Comments"
        '
        'tbOBJECT_COMMENTS
        '
        Me.tbOBJECT_COMMENTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "COMMENTS", True))
        Me.tbOBJECT_COMMENTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOBJECT_COMMENTS.Location = New System.Drawing.Point(12, 19)
        Me.tbOBJECT_COMMENTS.Multiline = True
        Me.tbOBJECT_COMMENTS.Name = "tbOBJECT_COMMENTS"
        Me.tbOBJECT_COMMENTS.Size = New System.Drawing.Size(778, 108)
        Me.tbOBJECT_COMMENTS.TabIndex = 0
        '
        'cbFavs
        '
        Me.cbFavs.DropDownHeight = 400
        Me.cbFavs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFavs.FormattingEnabled = True
        Me.cbFavs.IntegralHeight = False
        Me.cbFavs.Location = New System.Drawing.Point(40, 22)
        Me.cbFavs.Name = "cbFavs"
        Me.cbFavs.Size = New System.Drawing.Size(275, 24)
        Me.cbFavs.TabIndex = 25
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 27)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 73
        Me.PictureBox1.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(823, 615)
        Me.TabControl1.TabIndex = 74
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.chbAdvancedView)
        Me.TabPage1.Controls.Add(Me.GroupBox10)
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.GroupBox9)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(815, 586)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Materials and Irregularity"
        '
        'chbAdvancedView
        '
        Me.chbAdvancedView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAdvancedView.Location = New System.Drawing.Point(15, 68)
        Me.chbAdvancedView.Name = "chbAdvancedView"
        Me.chbAdvancedView.Size = New System.Drawing.Size(440, 17)
        Me.chbAdvancedView.TabIndex = 72
        Me.chbAdvancedView.Text = "Advanced view: show longitudinal, transverse, primary and secondary fields" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " "
        Me.chbAdvancedView.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btFavSave)
        Me.GroupBox10.Controls.Add(Me.btFavLoad)
        Me.GroupBox10.Controls.Add(Me.PictureBox1)
        Me.GroupBox10.Controls.Add(Me.cbFavs)
        Me.GroupBox10.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(476, 56)
        Me.GroupBox10.TabIndex = 71
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Favourites"
        '
        'btFavSave
        '
        Me.btFavSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btFavSave.Location = New System.Drawing.Point(401, 23)
        Me.btFavSave.Name = "btFavSave"
        Me.btFavSave.Size = New System.Drawing.Size(62, 23)
        Me.btFavSave.TabIndex = 75
        Me.btFavSave.Text = "Save"
        Me.btFavSave.UseVisualStyleBackColor = True
        '
        'btFavLoad
        '
        Me.btFavLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btFavLoad.Location = New System.Drawing.Point(325, 23)
        Me.btFavLoad.Name = "btFavLoad"
        Me.btFavLoad.Size = New System.Drawing.Size(62, 23)
        Me.btFavLoad.TabIndex = 74
        Me.btFavLoad.Text = "Load"
        Me.btFavLoad.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.GroupBox11)
        Me.TabPage2.Controls.Add(Me.GroupBox6)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(815, 586)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Building Components"
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.Label15)
        Me.GroupBox11.Controls.Add(Me.cbNONSTRUCTURAL_EXTERIOR_WALLS)
        Me.GroupBox11.Location = New System.Drawing.Point(9, 400)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(797, 70)
        Me.GroupBox11.TabIndex = 83
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Walls"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(50, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 32)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Non Structural" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "External Walls"
        '
        'cbNONSTRUCTURAL_EXTERIOR_WALLS
        '
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "NONSTRCEXW", True))
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.DataSource = Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.DisplayMember = "DESCRIPTION"
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.DropDownHeight = 400
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.FormattingEnabled = True
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.IntegralHeight = False
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.Location = New System.Drawing.Point(158, 26)
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.Name = "cbNONSTRUCTURAL_EXTERIOR_WALLS"
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.Size = New System.Drawing.Size(386, 24)
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.TabIndex = 29
        Me.cbNONSTRUCTURAL_EXTERIOR_WALLS.ValueMember = "CODE"
        '
        'DICNONSTRUCTURALEXTERIORWALLSBindingSource
        '
        Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource.DataMember = "DIC_NONSTRUCTURAL_EXTERIOR_WALLS"
        Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource.DataSource = Me.GEMDataset
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.cbFOUNDATION_SYSTEM)
        Me.GroupBox6.Controls.Add(Me.lblFTYPE)
        Me.GroupBox6.Controls.Add(Me.lblFMAT)
        Me.GroupBox6.Controls.Add(Me.cbFLOOR_TYPE)
        Me.GroupBox6.Controls.Add(Me.cbFLOOR_MATERIAL)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 270)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(797, 124)
        Me.GroupBox6.TabIndex = 82
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Floor and Foundations"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 81)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 16)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Foundation System"
        '
        'cbFOUNDATION_SYSTEM
        '
        Me.cbFOUNDATION_SYSTEM.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "FOUNDN_SYS", True))
        Me.cbFOUNDATION_SYSTEM.DataSource = Me.DICFOUNDATIONSYSTEMBindingSource
        Me.cbFOUNDATION_SYSTEM.DisplayMember = "DESCRIPTION"
        Me.cbFOUNDATION_SYSTEM.DropDownHeight = 400
        Me.cbFOUNDATION_SYSTEM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFOUNDATION_SYSTEM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFOUNDATION_SYSTEM.FormattingEnabled = True
        Me.cbFOUNDATION_SYSTEM.IntegralHeight = False
        Me.cbFOUNDATION_SYSTEM.Location = New System.Drawing.Point(158, 78)
        Me.cbFOUNDATION_SYSTEM.Name = "cbFOUNDATION_SYSTEM"
        Me.cbFOUNDATION_SYSTEM.Size = New System.Drawing.Size(386, 24)
        Me.cbFOUNDATION_SYSTEM.TabIndex = 33
        Me.cbFOUNDATION_SYSTEM.ValueMember = "CODE"
        '
        'DICFOUNDATIONSYSTEMBindingSource
        '
        Me.DICFOUNDATIONSYSTEMBindingSource.DataMember = "DIC_FOUNDATION_SYSTEM"
        Me.DICFOUNDATIONSYSTEMBindingSource.DataSource = Me.GEMDataset
        '
        'lblFTYPE
        '
        Me.lblFTYPE.AutoSize = True
        Me.lblFTYPE.BackColor = System.Drawing.Color.Transparent
        Me.lblFTYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFTYPE.Location = New System.Drawing.Point(70, 53)
        Me.lblFTYPE.Name = "lblFTYPE"
        Me.lblFTYPE.Size = New System.Drawing.Size(74, 16)
        Me.lblFTYPE.TabIndex = 32
        Me.lblFTYPE.Text = "Floor Type"
        '
        'lblFMAT
        '
        Me.lblFMAT.AutoSize = True
        Me.lblFMAT.BackColor = System.Drawing.Color.Transparent
        Me.lblFMAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFMAT.Location = New System.Drawing.Point(55, 26)
        Me.lblFMAT.Name = "lblFMAT"
        Me.lblFMAT.Size = New System.Drawing.Size(90, 16)
        Me.lblFMAT.TabIndex = 31
        Me.lblFMAT.Text = "Floor Material"
        '
        'cbFLOOR_TYPE
        '
        Me.cbFLOOR_TYPE.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "FLOOR_TYPE", True))
        Me.cbFLOOR_TYPE.DataSource = Me.DICFLOORTYPEBindingSource
        Me.cbFLOOR_TYPE.DisplayMember = "DESCRIPTION"
        Me.cbFLOOR_TYPE.DropDownHeight = 400
        Me.cbFLOOR_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFLOOR_TYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFLOOR_TYPE.FormattingEnabled = True
        Me.cbFLOOR_TYPE.IntegralHeight = False
        Me.cbFLOOR_TYPE.Location = New System.Drawing.Point(158, 50)
        Me.cbFLOOR_TYPE.Name = "cbFLOOR_TYPE"
        Me.cbFLOOR_TYPE.Size = New System.Drawing.Size(386, 24)
        Me.cbFLOOR_TYPE.TabIndex = 30
        Me.cbFLOOR_TYPE.ValueMember = "CODE"
        '
        'DICFLOORTYPEBindingSource
        '
        Me.DICFLOORTYPEBindingSource.DataMember = "DIC_FLOOR_TYPE"
        Me.DICFLOORTYPEBindingSource.DataSource = Me.GEMDataset
        '
        'cbFLOOR_MATERIAL
        '
        Me.cbFLOOR_MATERIAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "FLOOR_MAT", True))
        Me.cbFLOOR_MATERIAL.DataSource = Me.DICFLOORMATERIALBindingSource
        Me.cbFLOOR_MATERIAL.DisplayMember = "DESCRIPTION"
        Me.cbFLOOR_MATERIAL.DropDownHeight = 400
        Me.cbFLOOR_MATERIAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFLOOR_MATERIAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFLOOR_MATERIAL.FormattingEnabled = True
        Me.cbFLOOR_MATERIAL.IntegralHeight = False
        Me.cbFLOOR_MATERIAL.Location = New System.Drawing.Point(158, 23)
        Me.cbFLOOR_MATERIAL.Name = "cbFLOOR_MATERIAL"
        Me.cbFLOOR_MATERIAL.Size = New System.Drawing.Size(386, 24)
        Me.cbFLOOR_MATERIAL.TabIndex = 29
        Me.cbFLOOR_MATERIAL.ValueMember = "CODE"
        '
        'DICFLOORMATERIALBindingSource
        '
        Me.DICFLOORMATERIALBindingSource.DataMember = "DIC_FLOOR_MATERIAL"
        Me.DICFLOORMATERIALBindingSource.DataSource = Me.GEMDataset
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cbROOF_SHAPE)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.cbROOF_SYSTEM_TYPE)
        Me.GroupBox5.Controls.Add(Me.lblRTYPE)
        Me.GroupBox5.Controls.Add(Me.lblRMAT)
        Me.GroupBox5.Controls.Add(Me.cbROOF_SYSTEM_MATERIAL)
        Me.GroupBox5.Controls.Add(Me.cbROOF_COVER_MATERIAL)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 121)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(797, 143)
        Me.GroupBox5.TabIndex = 81
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Roof"
        '
        'cbROOF_SHAPE
        '
        Me.cbROOF_SHAPE.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "ROOF_SHAPE", True))
        Me.cbROOF_SHAPE.DataSource = Me.DICROOFSHAPEBindingSource
        Me.cbROOF_SHAPE.DisplayMember = "DESCRIPTION"
        Me.cbROOF_SHAPE.DropDownHeight = 400
        Me.cbROOF_SHAPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbROOF_SHAPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbROOF_SHAPE.FormattingEnabled = True
        Me.cbROOF_SHAPE.IntegralHeight = False
        Me.cbROOF_SHAPE.Location = New System.Drawing.Point(158, 14)
        Me.cbROOF_SHAPE.Name = "cbROOF_SHAPE"
        Me.cbROOF_SHAPE.Size = New System.Drawing.Size(386, 24)
        Me.cbROOF_SHAPE.TabIndex = 32
        Me.cbROOF_SHAPE.ValueMember = "CODE"
        '
        'DICROOFSHAPEBindingSource
        '
        Me.DICROOFSHAPEBindingSource.DataMember = "DIC_ROOF_SHAPE"
        Me.DICROOFSHAPEBindingSource.DataSource = Me.GEMDataset
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(65, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 16)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "Roof Shape"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(25, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 16)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Roof System Type"
        '
        'cbROOF_SYSTEM_TYPE
        '
        Me.cbROOF_SYSTEM_TYPE.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "ROOFSYSTYP", True))
        Me.cbROOF_SYSTEM_TYPE.DataSource = Me.DICROOFSYSTEMTYPEBindingSource
        Me.cbROOF_SYSTEM_TYPE.DisplayMember = "DESCRIPTION"
        Me.cbROOF_SYSTEM_TYPE.DropDownHeight = 400
        Me.cbROOF_SYSTEM_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbROOF_SYSTEM_TYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbROOF_SYSTEM_TYPE.FormattingEnabled = True
        Me.cbROOF_SYSTEM_TYPE.IntegralHeight = False
        Me.cbROOF_SYSTEM_TYPE.Location = New System.Drawing.Point(158, 106)
        Me.cbROOF_SYSTEM_TYPE.Name = "cbROOF_SYSTEM_TYPE"
        Me.cbROOF_SYSTEM_TYPE.Size = New System.Drawing.Size(386, 24)
        Me.cbROOF_SYSTEM_TYPE.TabIndex = 29
        Me.cbROOF_SYSTEM_TYPE.ValueMember = "CODE"
        '
        'DICROOFSYSTEMTYPEBindingSource
        '
        Me.DICROOFSYSTEMTYPEBindingSource.DataMember = "DIC_ROOF_SYSTEM_TYPE"
        Me.DICROOFSYSTEMTYPEBindingSource.DataSource = Me.GEMDataset
        '
        'lblRTYPE
        '
        Me.lblRTYPE.AutoSize = True
        Me.lblRTYPE.BackColor = System.Drawing.Color.Transparent
        Me.lblRTYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRTYPE.Location = New System.Drawing.Point(9, 78)
        Me.lblRTYPE.Name = "lblRTYPE"
        Me.lblRTYPE.Size = New System.Drawing.Size(136, 16)
        Me.lblRTYPE.TabIndex = 28
        Me.lblRTYPE.Text = "Roof System Material"
        '
        'lblRMAT
        '
        Me.lblRMAT.AutoSize = True
        Me.lblRMAT.BackColor = System.Drawing.Color.Transparent
        Me.lblRMAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMAT.Location = New System.Drawing.Point(18, 48)
        Me.lblRMAT.Name = "lblRMAT"
        Me.lblRMAT.Size = New System.Drawing.Size(127, 16)
        Me.lblRMAT.TabIndex = 27
        Me.lblRMAT.Text = "Roof Cover Material"
        '
        'cbROOF_SYSTEM_MATERIAL
        '
        Me.cbROOF_SYSTEM_MATERIAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "ROOFSYSMAT", True))
        Me.cbROOF_SYSTEM_MATERIAL.DataSource = Me.DICROOFSYSTEMMATERIALBindingSource
        Me.cbROOF_SYSTEM_MATERIAL.DisplayMember = "DESCRIPTION"
        Me.cbROOF_SYSTEM_MATERIAL.DropDownHeight = 400
        Me.cbROOF_SYSTEM_MATERIAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbROOF_SYSTEM_MATERIAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbROOF_SYSTEM_MATERIAL.FormattingEnabled = True
        Me.cbROOF_SYSTEM_MATERIAL.IntegralHeight = False
        Me.cbROOF_SYSTEM_MATERIAL.Location = New System.Drawing.Point(158, 76)
        Me.cbROOF_SYSTEM_MATERIAL.Name = "cbROOF_SYSTEM_MATERIAL"
        Me.cbROOF_SYSTEM_MATERIAL.Size = New System.Drawing.Size(386, 24)
        Me.cbROOF_SYSTEM_MATERIAL.TabIndex = 19
        Me.cbROOF_SYSTEM_MATERIAL.ValueMember = "CODE"
        '
        'DICROOFSYSTEMMATERIALBindingSource
        '
        Me.DICROOFSYSTEMMATERIALBindingSource.DataMember = "DIC_ROOF_SYSTEM_MATERIAL"
        Me.DICROOFSYSTEMMATERIALBindingSource.DataSource = Me.GEMDataset
        '
        'cbROOF_COVER_MATERIAL
        '
        Me.cbROOF_COVER_MATERIAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "ROOFCOVMAT", True))
        Me.cbROOF_COVER_MATERIAL.DataSource = Me.DICROOFCOVERMATERIALBindingSource
        Me.cbROOF_COVER_MATERIAL.DisplayMember = "DESCRIPTION"
        Me.cbROOF_COVER_MATERIAL.DropDownHeight = 400
        Me.cbROOF_COVER_MATERIAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbROOF_COVER_MATERIAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbROOF_COVER_MATERIAL.FormattingEnabled = True
        Me.cbROOF_COVER_MATERIAL.IntegralHeight = False
        Me.cbROOF_COVER_MATERIAL.Location = New System.Drawing.Point(158, 46)
        Me.cbROOF_COVER_MATERIAL.Name = "cbROOF_COVER_MATERIAL"
        Me.cbROOF_COVER_MATERIAL.Size = New System.Drawing.Size(386, 24)
        Me.cbROOF_COVER_MATERIAL.TabIndex = 18
        Me.cbROOF_COVER_MATERIAL.ValueMember = "CODE"
        '
        'DICROOFCOVERMATERIALBindingSource
        '
        Me.DICROOFCOVERMATERIALBindingSource.DataMember = "DIC_ROOF_COVER_MATERIAL"
        Me.DICROOFCOVERMATERIALBindingSource.DataSource = Me.GEMDataset
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cbLLRS_DUCTILITY_T)
        Me.GroupBox4.Controls.Add(Me.cbLLRS_T)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.cbLLRS_DUCTILITY_L)
        Me.GroupBox4.Controls.Add(Me.cbLLRS_L)
        Me.GroupBox4.Controls.Add(Me.lblTrans2)
        Me.GroupBox4.Controls.Add(Me.lblLong2)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 13)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(798, 102)
        Me.GroupBox4.TabIndex = 80
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Lateral Load Resisting System"
        '
        'cbLLRS_DUCTILITY_T
        '
        Me.cbLLRS_DUCTILITY_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "LLRS_DCT_T", True))
        Me.cbLLRS_DUCTILITY_T.DataSource = Me.DICLLRSDUCTILITYBindingSource1
        Me.cbLLRS_DUCTILITY_T.DisplayMember = "DESCRIPTION"
        Me.cbLLRS_DUCTILITY_T.DropDownHeight = 400
        Me.cbLLRS_DUCTILITY_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLLRS_DUCTILITY_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLLRS_DUCTILITY_T.FormattingEnabled = True
        Me.cbLLRS_DUCTILITY_T.IntegralHeight = False
        Me.cbLLRS_DUCTILITY_T.Location = New System.Drawing.Point(485, 61)
        Me.cbLLRS_DUCTILITY_T.Name = "cbLLRS_DUCTILITY_T"
        Me.cbLLRS_DUCTILITY_T.Size = New System.Drawing.Size(295, 24)
        Me.cbLLRS_DUCTILITY_T.TabIndex = 44
        Me.cbLLRS_DUCTILITY_T.ValueMember = "CODE"
        '
        'DICLLRSDUCTILITYBindingSource1
        '
        Me.DICLLRSDUCTILITYBindingSource1.DataMember = "DIC_LLRS_DUCTILITY"
        Me.DICLLRSDUCTILITYBindingSource1.DataSource = Me.GEMDataset
        '
        'cbLLRS_T
        '
        Me.cbLLRS_T.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "LLRS_T", True))
        Me.cbLLRS_T.DataSource = Me.DICLLRSBindingSource1
        Me.cbLLRS_T.DisplayMember = "DESCRIPTION"
        Me.cbLLRS_T.DropDownHeight = 400
        Me.cbLLRS_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLLRS_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLLRS_T.FormattingEnabled = True
        Me.cbLLRS_T.IntegralHeight = False
        Me.cbLLRS_T.Location = New System.Drawing.Point(485, 34)
        Me.cbLLRS_T.Name = "cbLLRS_T"
        Me.cbLLRS_T.Size = New System.Drawing.Size(295, 24)
        Me.cbLLRS_T.TabIndex = 43
        Me.cbLLRS_T.ValueMember = "CODE"
        '
        'DICLLRSBindingSource1
        '
        Me.DICLLRSBindingSource1.DataMember = "DIC_LLRS"
        Me.DICLLRSBindingSource1.DataSource = Me.GEMDataset
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(65, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 16)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "LLRS Ductility"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(113, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 16)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "LLRS"
        '
        'cbLLRS_DUCTILITY_L
        '
        Me.cbLLRS_DUCTILITY_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "LLRS_DCT_L", True))
        Me.cbLLRS_DUCTILITY_L.DataSource = Me.DICLLRSDUCTILITYBindingSource
        Me.cbLLRS_DUCTILITY_L.DisplayMember = "DESCRIPTION"
        Me.cbLLRS_DUCTILITY_L.DropDownHeight = 400
        Me.cbLLRS_DUCTILITY_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLLRS_DUCTILITY_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLLRS_DUCTILITY_L.FormattingEnabled = True
        Me.cbLLRS_DUCTILITY_L.IntegralHeight = False
        Me.cbLLRS_DUCTILITY_L.Location = New System.Drawing.Point(160, 61)
        Me.cbLLRS_DUCTILITY_L.Name = "cbLLRS_DUCTILITY_L"
        Me.cbLLRS_DUCTILITY_L.Size = New System.Drawing.Size(301, 24)
        Me.cbLLRS_DUCTILITY_L.TabIndex = 40
        Me.cbLLRS_DUCTILITY_L.ValueMember = "CODE"
        '
        'DICLLRSDUCTILITYBindingSource
        '
        Me.DICLLRSDUCTILITYBindingSource.DataMember = "DIC_LLRS_DUCTILITY"
        Me.DICLLRSDUCTILITYBindingSource.DataSource = Me.GEMDataset
        '
        'cbLLRS_L
        '
        Me.cbLLRS_L.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "LLRS_L", True))
        Me.cbLLRS_L.DataSource = Me.DICLLRSBindingSource
        Me.cbLLRS_L.DisplayMember = "DESCRIPTION"
        Me.cbLLRS_L.DropDownHeight = 400
        Me.cbLLRS_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLLRS_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLLRS_L.FormattingEnabled = True
        Me.cbLLRS_L.IntegralHeight = False
        Me.cbLLRS_L.Location = New System.Drawing.Point(160, 34)
        Me.cbLLRS_L.Name = "cbLLRS_L"
        Me.cbLLRS_L.Size = New System.Drawing.Size(301, 24)
        Me.cbLLRS_L.TabIndex = 39
        Me.cbLLRS_L.ValueMember = "CODE"
        '
        'DICLLRSBindingSource
        '
        Me.DICLLRSBindingSource.DataMember = "DIC_LLRS"
        Me.DICLLRSBindingSource.DataSource = Me.GEMDataset
        '
        'lblTrans2
        '
        Me.lblTrans2.AutoSize = True
        Me.lblTrans2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrans2.Location = New System.Drawing.Point(601, 15)
        Me.lblTrans2.Name = "lblTrans2"
        Me.lblTrans2.Size = New System.Drawing.Size(77, 16)
        Me.lblTrans2.TabIndex = 38
        Me.lblTrans2.Text = "Transverse"
        '
        'lblLong2
        '
        Me.lblLong2.AutoSize = True
        Me.lblLong2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLong2.Location = New System.Drawing.Point(267, 15)
        Me.lblLong2.Name = "lblLong2"
        Me.lblLong2.Size = New System.Drawing.Size(80, 16)
        Me.lblLong2.TabIndex = 37
        Me.lblLong2.Text = "Longitudinal"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.GroupBox14)
        Me.TabPage3.Controls.Add(Me.GroupBox13)
        Me.TabPage3.Controls.Add(Me.GroupBox7)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(815, 586)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Building Information"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblOCCD)
        Me.GroupBox3.Controls.Add(Me.lblOCC)
        Me.GroupBox3.Controls.Add(Me.cbOCCUPANCY_DETAIL)
        Me.GroupBox3.Controls.Add(Me.cbOCCUPANCY)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(798, 81)
        Me.GroupBox3.TabIndex = 90
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Occupancy"
        '
        'lblOCCD
        '
        Me.lblOCCD.AutoSize = True
        Me.lblOCCD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOCCD.Location = New System.Drawing.Point(33, 50)
        Me.lblOCCD.Name = "lblOCCD"
        Me.lblOCCD.Size = New System.Drawing.Size(114, 16)
        Me.lblOCCD.TabIndex = 42
        Me.lblOCCD.Text = "Occupancy Detail"
        '
        'lblOCC
        '
        Me.lblOCC.AutoSize = True
        Me.lblOCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOCC.Location = New System.Drawing.Point(71, 20)
        Me.lblOCC.Name = "lblOCC"
        Me.lblOCC.Size = New System.Drawing.Size(76, 16)
        Me.lblOCC.TabIndex = 41
        Me.lblOCC.Text = "Occupancy"
        '
        'cbOCCUPANCY_DETAIL
        '
        Me.cbOCCUPANCY_DETAIL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "OCCUPCY_DT", True))
        Me.cbOCCUPANCY_DETAIL.DataSource = Me.DICOCCUPANCYDETAILBindingSource
        Me.cbOCCUPANCY_DETAIL.DisplayMember = "DESCRIPTION"
        Me.cbOCCUPANCY_DETAIL.DropDownHeight = 400
        Me.cbOCCUPANCY_DETAIL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOCCUPANCY_DETAIL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOCCUPANCY_DETAIL.FormattingEnabled = True
        Me.cbOCCUPANCY_DETAIL.IntegralHeight = False
        Me.cbOCCUPANCY_DETAIL.Location = New System.Drawing.Point(153, 47)
        Me.cbOCCUPANCY_DETAIL.Name = "cbOCCUPANCY_DETAIL"
        Me.cbOCCUPANCY_DETAIL.Size = New System.Drawing.Size(479, 24)
        Me.cbOCCUPANCY_DETAIL.TabIndex = 37
        Me.cbOCCUPANCY_DETAIL.ValueMember = "CODE"
        '
        'DICOCCUPANCYDETAILBindingSource
        '
        Me.DICOCCUPANCYDETAILBindingSource.DataMember = "DIC_OCCUPANCY_DETAIL"
        Me.DICOCCUPANCYDETAILBindingSource.DataSource = Me.GEMDataset
        '
        'cbOCCUPANCY
        '
        Me.cbOCCUPANCY.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "OCCUPCY", True))
        Me.cbOCCUPANCY.DataSource = Me.DICOCCUPANCYBindingSource
        Me.cbOCCUPANCY.DisplayMember = "DESCRIPTION"
        Me.cbOCCUPANCY.DropDownHeight = 400
        Me.cbOCCUPANCY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOCCUPANCY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOCCUPANCY.FormattingEnabled = True
        Me.cbOCCUPANCY.IntegralHeight = False
        Me.cbOCCUPANCY.Location = New System.Drawing.Point(153, 20)
        Me.cbOCCUPANCY.Name = "cbOCCUPANCY"
        Me.cbOCCUPANCY.Size = New System.Drawing.Size(479, 24)
        Me.cbOCCUPANCY.TabIndex = 36
        Me.cbOCCUPANCY.ValueMember = "CODE"
        '
        'DICOCCUPANCYBindingSource
        '
        Me.DICOCCUPANCYBindingSource.DataMember = "DIC_OCCUPANCY"
        Me.DICOCCUPANCYBindingSource.DataSource = Me.GEMDataset
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.Label35)
        Me.GroupBox14.Controls.Add(Me.cbPLAN_SHAPE)
        Me.GroupBox14.Controls.Add(Me.Label13)
        Me.GroupBox14.Controls.Add(Me.cbPOSITION)
        Me.GroupBox14.Controls.Add(Me.tbSLOPE)
        Me.GroupBox14.Controls.Add(Me.Label22)
        Me.GroupBox14.Location = New System.Drawing.Point(9, 331)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(803, 145)
        Me.GroupBox14.TabIndex = 89
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Building Setting"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(28, 104)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(99, 16)
        Me.Label35.TabIndex = 111
        Me.Label35.Text = "Building Shape"
        '
        'cbPLAN_SHAPE
        '
        Me.cbPLAN_SHAPE.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "PLAN_SHAPE", True))
        Me.cbPLAN_SHAPE.DataSource = Me.DICPLANSHAPEBindingSource
        Me.cbPLAN_SHAPE.DisplayMember = "DESCRIPTION"
        Me.cbPLAN_SHAPE.DropDownHeight = 400
        Me.cbPLAN_SHAPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPLAN_SHAPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPLAN_SHAPE.FormattingEnabled = True
        Me.cbPLAN_SHAPE.IntegralHeight = False
        Me.cbPLAN_SHAPE.Location = New System.Drawing.Point(141, 101)
        Me.cbPLAN_SHAPE.Name = "cbPLAN_SHAPE"
        Me.cbPLAN_SHAPE.Size = New System.Drawing.Size(376, 24)
        Me.cbPLAN_SHAPE.TabIndex = 110
        Me.cbPLAN_SHAPE.ValueMember = "CODE"
        '
        'DICPLANSHAPEBindingSource
        '
        Me.DICPLANSHAPEBindingSource.DataMember = "DIC_PLAN_SHAPE"
        Me.DICPLANSHAPEBindingSource.DataSource = Me.GEMDataset
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(21, 67)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(106, 16)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Position in Block"
        '
        'cbPOSITION
        '
        Me.cbPOSITION.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "POSITION", True))
        Me.cbPOSITION.DataSource = Me.DICPOSITIONBindingSource
        Me.cbPOSITION.DisplayMember = "DESCRIPTION"
        Me.cbPOSITION.DropDownHeight = 400
        Me.cbPOSITION.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPOSITION.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPOSITION.FormattingEnabled = True
        Me.cbPOSITION.IntegralHeight = False
        Me.cbPOSITION.Location = New System.Drawing.Point(141, 64)
        Me.cbPOSITION.Name = "cbPOSITION"
        Me.cbPOSITION.Size = New System.Drawing.Size(376, 24)
        Me.cbPOSITION.TabIndex = 29
        Me.cbPOSITION.ValueMember = "CODE"
        '
        'DICPOSITIONBindingSource
        '
        Me.DICPOSITIONBindingSource.DataMember = "DIC_POSITION"
        Me.DICPOSITIONBindingSource.DataSource = Me.GEMDataset
        '
        'tbSLOPE
        '
        Me.tbSLOPE.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "SLOPE", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbSLOPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSLOPE.Location = New System.Drawing.Point(141, 28)
        Me.tbSLOPE.Name = "tbSLOPE"
        Me.tbSLOPE.Size = New System.Drawing.Size(88, 22)
        Me.tbSLOPE.TabIndex = 109
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(21, 31)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(106, 16)
        Me.Label22.TabIndex = 44
        Me.Label22.Text = "Slope (degrees)"
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL)
        Me.GroupBox13.Controls.Add(Me.tbHT_ABOVEGRADE_GRND_FLOOR_2)
        Me.GroupBox13.Controls.Add(Me.tbHT_ABOVEGRADE_GRND_FLOOR_1)
        Me.GroupBox13.Controls.Add(Me.Label18)
        Me.GroupBox13.Controls.Add(Me.cbNO_STOREYS_BELOW_GROUND_QUAL)
        Me.GroupBox13.Controls.Add(Me.tbNO_STOREYS_BELOW_GROUND_2)
        Me.GroupBox13.Controls.Add(Me.tbNO_STOREYS_BELOW_GROUND_1)
        Me.GroupBox13.Controls.Add(Me.Label16)
        Me.GroupBox13.Controls.Add(Me.cbNO_STOREYS_ABOVE_GROUND_QUAL)
        Me.GroupBox13.Controls.Add(Me.tbNO_STOREYS_ABOVE_GROUND_2)
        Me.GroupBox13.Controls.Add(Me.tbNO_STOREYS_ABOVE_GROUND_1)
        Me.GroupBox13.Controls.Add(Me.lblH1)
        Me.GroupBox13.Location = New System.Drawing.Point(8, 179)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(803, 137)
        Me.GroupBox13.TabIndex = 89
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Height"
        '
        'cbHT_ABOVEGRADE_GRND_FLOOR_QUAL
        '
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "HT_GR_GF_Q", True))
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.DataSource = Me.DICHEIGHTQUALIFIERBindingSource2
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.DisplayMember = "DESCRIPTION"
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.DropDownHeight = 400
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.FormattingEnabled = True
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.IntegralHeight = False
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Location = New System.Drawing.Point(190, 90)
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Name = "cbHT_ABOVEGRADE_GRND_FLOOR_QUAL"
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.Size = New System.Drawing.Size(153, 24)
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.TabIndex = 108
        Me.cbHT_ABOVEGRADE_GRND_FLOOR_QUAL.ValueMember = "CODE"
        '
        'DICHEIGHTQUALIFIERBindingSource2
        '
        Me.DICHEIGHTQUALIFIERBindingSource2.DataMember = "DIC_HEIGHT_QUALIFIER"
        Me.DICHEIGHTQUALIFIERBindingSource2.DataSource = Me.GEMDataset
        '
        'tbHT_ABOVEGRADE_GRND_FLOOR_2
        '
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "HT_GR_GF_2", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.Location = New System.Drawing.Point(447, 92)
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.Name = "tbHT_ABOVEGRADE_GRND_FLOOR_2"
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.Size = New System.Drawing.Size(88, 22)
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.TabIndex = 106
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_2.Visible = False
        '
        'tbHT_ABOVEGRADE_GRND_FLOOR_1
        '
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "HT_GR_GF_1", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.Location = New System.Drawing.Point(353, 92)
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.Name = "tbHT_ABOVEGRADE_GRND_FLOOR_1"
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.Size = New System.Drawing.Size(88, 22)
        Me.tbHT_ABOVEGRADE_GRND_FLOOR_1.TabIndex = 105
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(43, 90)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(141, 32)
        Me.Label18.TabIndex = 104
        Me.Label18.Text = "Ground Floor Height" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Above Grade (metres)"
        '
        'cbNO_STOREYS_BELOW_GROUND_QUAL
        '
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STORY_BG_Q", True))
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.DataSource = Me.DICHEIGHTQUALIFIERBindingSource1
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.DisplayMember = "DESCRIPTION"
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.DropDownHeight = 400
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.FormattingEnabled = True
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.IntegralHeight = False
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.Location = New System.Drawing.Point(190, 60)
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.Name = "cbNO_STOREYS_BELOW_GROUND_QUAL"
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.Size = New System.Drawing.Size(153, 24)
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.TabIndex = 103
        Me.cbNO_STOREYS_BELOW_GROUND_QUAL.ValueMember = "CODE"
        '
        'DICHEIGHTQUALIFIERBindingSource1
        '
        Me.DICHEIGHTQUALIFIERBindingSource1.DataMember = "DIC_HEIGHT_QUALIFIER"
        Me.DICHEIGHTQUALIFIERBindingSource1.DataSource = Me.GEMDataset
        '
        'tbNO_STOREYS_BELOW_GROUND_2
        '
        Me.tbNO_STOREYS_BELOW_GROUND_2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "STORY_BG_2", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNO_STOREYS_BELOW_GROUND_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNO_STOREYS_BELOW_GROUND_2.Location = New System.Drawing.Point(447, 60)
        Me.tbNO_STOREYS_BELOW_GROUND_2.Name = "tbNO_STOREYS_BELOW_GROUND_2"
        Me.tbNO_STOREYS_BELOW_GROUND_2.Size = New System.Drawing.Size(88, 22)
        Me.tbNO_STOREYS_BELOW_GROUND_2.TabIndex = 101
        Me.tbNO_STOREYS_BELOW_GROUND_2.Visible = False
        '
        'tbNO_STOREYS_BELOW_GROUND_1
        '
        Me.tbNO_STOREYS_BELOW_GROUND_1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "STORY_BG_1", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNO_STOREYS_BELOW_GROUND_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNO_STOREYS_BELOW_GROUND_1.Location = New System.Drawing.Point(353, 60)
        Me.tbNO_STOREYS_BELOW_GROUND_1.Name = "tbNO_STOREYS_BELOW_GROUND_1"
        Me.tbNO_STOREYS_BELOW_GROUND_1.Size = New System.Drawing.Size(88, 22)
        Me.tbNO_STOREYS_BELOW_GROUND_1.TabIndex = 100
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(19, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(165, 16)
        Me.Label16.TabIndex = 99
        Me.Label16.Text = "No. Storeys Below Ground"
        '
        'cbNO_STOREYS_ABOVE_GROUND_QUAL
        '
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "STORY_AG_Q", True))
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.DataSource = Me.DICHEIGHTQUALIFIERBindingSource
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.DisplayMember = "DESCRIPTION"
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.DropDownHeight = 400
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.FormattingEnabled = True
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.IntegralHeight = False
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.Location = New System.Drawing.Point(190, 30)
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.Name = "cbNO_STOREYS_ABOVE_GROUND_QUAL"
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.Size = New System.Drawing.Size(153, 24)
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.TabIndex = 98
        Me.cbNO_STOREYS_ABOVE_GROUND_QUAL.ValueMember = "CODE"
        '
        'DICHEIGHTQUALIFIERBindingSource
        '
        Me.DICHEIGHTQUALIFIERBindingSource.DataMember = "DIC_HEIGHT_QUALIFIER"
        Me.DICHEIGHTQUALIFIERBindingSource.DataSource = Me.GEMDataset
        '
        'tbNO_STOREYS_ABOVE_GROUND_2
        '
        Me.tbNO_STOREYS_ABOVE_GROUND_2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "STORY_AG_2", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNO_STOREYS_ABOVE_GROUND_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNO_STOREYS_ABOVE_GROUND_2.Location = New System.Drawing.Point(447, 30)
        Me.tbNO_STOREYS_ABOVE_GROUND_2.Name = "tbNO_STOREYS_ABOVE_GROUND_2"
        Me.tbNO_STOREYS_ABOVE_GROUND_2.Size = New System.Drawing.Size(88, 22)
        Me.tbNO_STOREYS_ABOVE_GROUND_2.TabIndex = 96
        Me.tbNO_STOREYS_ABOVE_GROUND_2.Visible = False
        '
        'tbNO_STOREYS_ABOVE_GROUND_1
        '
        Me.tbNO_STOREYS_ABOVE_GROUND_1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "STORY_AG_1", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNO_STOREYS_ABOVE_GROUND_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNO_STOREYS_ABOVE_GROUND_1.Location = New System.Drawing.Point(353, 30)
        Me.tbNO_STOREYS_ABOVE_GROUND_1.Name = "tbNO_STOREYS_ABOVE_GROUND_1"
        Me.tbNO_STOREYS_ABOVE_GROUND_1.Size = New System.Drawing.Size(88, 22)
        Me.tbNO_STOREYS_ABOVE_GROUND_1.TabIndex = 95
        '
        'lblH1
        '
        Me.lblH1.AutoSize = True
        Me.lblH1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblH1.Location = New System.Drawing.Point(16, 33)
        Me.lblH1.Name = "lblH1"
        Me.lblH1.Size = New System.Drawing.Size(168, 16)
        Me.lblH1.TabIndex = 94
        Me.lblH1.Text = "No. Storeys Above Ground"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.tbYEAR_BUILT_2)
        Me.GroupBox7.Controls.Add(Me.cbYEAR_BUILT_QUAL)
        Me.GroupBox7.Controls.Add(Me.tbYEAR_BUILT_1)
        Me.GroupBox7.Controls.Add(Me.lblD1)
        Me.GroupBox7.Location = New System.Drawing.Point(8, 103)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(803, 68)
        Me.GroupBox7.TabIndex = 88
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Date"
        '
        'tbYEAR_BUILT_2
        '
        Me.tbYEAR_BUILT_2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "YR_BUILT_2", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbYEAR_BUILT_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbYEAR_BUILT_2.Location = New System.Drawing.Point(447, 28)
        Me.tbYEAR_BUILT_2.Name = "tbYEAR_BUILT_2"
        Me.tbYEAR_BUILT_2.Size = New System.Drawing.Size(88, 22)
        Me.tbYEAR_BUILT_2.TabIndex = 58
        '
        'cbYEAR_BUILT_QUAL
        '
        Me.cbYEAR_BUILT_QUAL.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEMOBJECTBindingSource, "YR_BUILT_Q", True))
        Me.cbYEAR_BUILT_QUAL.DataSource = Me.DICYEARBUILTQUALBindingSource
        Me.cbYEAR_BUILT_QUAL.DisplayMember = "DESCRIPTION"
        Me.cbYEAR_BUILT_QUAL.DropDownHeight = 400
        Me.cbYEAR_BUILT_QUAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbYEAR_BUILT_QUAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbYEAR_BUILT_QUAL.FormattingEnabled = True
        Me.cbYEAR_BUILT_QUAL.IntegralHeight = False
        Me.cbYEAR_BUILT_QUAL.Location = New System.Drawing.Point(190, 26)
        Me.cbYEAR_BUILT_QUAL.Name = "cbYEAR_BUILT_QUAL"
        Me.cbYEAR_BUILT_QUAL.Size = New System.Drawing.Size(157, 24)
        Me.cbYEAR_BUILT_QUAL.TabIndex = 56
        Me.cbYEAR_BUILT_QUAL.ValueMember = "CODE"
        '
        'DICYEARBUILTQUALBindingSource
        '
        Me.DICYEARBUILTQUALBindingSource.DataMember = "DIC_YEAR_BUILT_QUAL"
        Me.DICYEARBUILTQUALBindingSource.DataSource = Me.GEMDataset
        '
        'tbYEAR_BUILT_1
        '
        Me.tbYEAR_BUILT_1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEMOBJECTBindingSource, "YR_BUILT_1", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbYEAR_BUILT_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbYEAR_BUILT_1.Location = New System.Drawing.Point(353, 28)
        Me.tbYEAR_BUILT_1.Name = "tbYEAR_BUILT_1"
        Me.tbYEAR_BUILT_1.Size = New System.Drawing.Size(89, 22)
        Me.tbYEAR_BUILT_1.TabIndex = 48
        '
        'lblD1
        '
        Me.lblD1.AutoSize = True
        Me.lblD1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblD1.Location = New System.Drawing.Point(18, 31)
        Me.lblD1.Name = "lblD1"
        Me.lblD1.Size = New System.Drawing.Size(166, 16)
        Me.lblD1.TabIndex = 44
        Me.lblD1.Text = "Year of Construction (yyyy)"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage4.Controls.Add(Me.GroupBox16)
        Me.TabPage4.Controls.Add(Me.GroupBox15)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(815, 586)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Consequences and Exposure"
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.Label27)
        Me.GroupBox16.Controls.Add(Me.tbCONSEQUENCES_COMMENTS)
        Me.GroupBox16.Controls.Add(Me.cbDAMAGE_CODE)
        Me.GroupBox16.Controls.Add(Me.tbNUM_MISSING)
        Me.GroupBox16.Controls.Add(Me.tbNUM_INJURED)
        Me.GroupBox16.Controls.Add(Me.tbNUM_FATALITIES)
        Me.GroupBox16.Controls.Add(Me.Label28)
        Me.GroupBox16.Controls.Add(Me.Label29)
        Me.GroupBox16.Controls.Add(Me.Label30)
        Me.GroupBox16.Controls.Add(Me.Label31)
        Me.GroupBox16.Location = New System.Drawing.Point(8, 321)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(798, 259)
        Me.GroupBox16.TabIndex = 102
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Consequences"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(124, 149)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(72, 16)
        Me.Label27.TabIndex = 104
        Me.Label27.Text = "Comments"
        '
        'tbCONSEQUENCES_COMMENTS
        '
        Me.tbCONSEQUENCES_COMMENTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CONSEQUENCESBindingSource, "COMMENTS", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbCONSEQUENCES_COMMENTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCONSEQUENCES_COMMENTS.Location = New System.Drawing.Point(206, 146)
        Me.tbCONSEQUENCES_COMMENTS.Multiline = True
        Me.tbCONSEQUENCES_COMMENTS.Name = "tbCONSEQUENCES_COMMENTS"
        Me.tbCONSEQUENCES_COMMENTS.Size = New System.Drawing.Size(584, 98)
        Me.tbCONSEQUENCES_COMMENTS.TabIndex = 103
        '
        'CONSEQUENCESBindingSource
        '
        Me.CONSEQUENCESBindingSource.DataMember = "CONSEQUENCES"
        Me.CONSEQUENCESBindingSource.DataSource = Me.GEMDataset
        '
        'cbDAMAGE_CODE
        '
        Me.cbDAMAGE_CODE.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.CONSEQUENCESBindingSource, "DAMAGE", True))
        Me.cbDAMAGE_CODE.DataSource = Me.DICDAMAGEBindingSource
        Me.cbDAMAGE_CODE.DisplayMember = "DESCRIPTION"
        Me.cbDAMAGE_CODE.DropDownHeight = 400
        Me.cbDAMAGE_CODE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDAMAGE_CODE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDAMAGE_CODE.FormattingEnabled = True
        Me.cbDAMAGE_CODE.IntegralHeight = False
        Me.cbDAMAGE_CODE.Location = New System.Drawing.Point(206, 111)
        Me.cbDAMAGE_CODE.Name = "cbDAMAGE_CODE"
        Me.cbDAMAGE_CODE.Size = New System.Drawing.Size(376, 24)
        Me.cbDAMAGE_CODE.TabIndex = 102
        Me.cbDAMAGE_CODE.ValueMember = "CODE"
        '
        'DICDAMAGEBindingSource
        '
        Me.DICDAMAGEBindingSource.DataMember = "DIC_DAMAGE"
        Me.DICDAMAGEBindingSource.DataSource = Me.GEMDataset
        '
        'tbNUM_MISSING
        '
        Me.tbNUM_MISSING.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CONSEQUENCESBindingSource, "MISSING", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_MISSING.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_MISSING.Location = New System.Drawing.Point(206, 83)
        Me.tbNUM_MISSING.Name = "tbNUM_MISSING"
        Me.tbNUM_MISSING.Size = New System.Drawing.Size(96, 22)
        Me.tbNUM_MISSING.TabIndex = 99
        '
        'tbNUM_INJURED
        '
        Me.tbNUM_INJURED.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CONSEQUENCESBindingSource, "INJURED", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_INJURED.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_INJURED.Location = New System.Drawing.Point(206, 56)
        Me.tbNUM_INJURED.Name = "tbNUM_INJURED"
        Me.tbNUM_INJURED.Size = New System.Drawing.Size(96, 22)
        Me.tbNUM_INJURED.TabIndex = 98
        '
        'tbNUM_FATALITIES
        '
        Me.tbNUM_FATALITIES.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CONSEQUENCESBindingSource, "FATALITIES", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_FATALITIES.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_FATALITIES.Location = New System.Drawing.Point(206, 28)
        Me.tbNUM_FATALITIES.Name = "tbNUM_FATALITIES"
        Me.tbNUM_FATALITIES.Size = New System.Drawing.Size(96, 22)
        Me.tbNUM_FATALITIES.TabIndex = 97
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(91, 86)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(105, 16)
        Me.Label28.TabIndex = 44
        Me.Label28.Text = "Number Missing"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(83, 56)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(113, 16)
        Me.Label29.TabIndex = 43
        Me.Label29.Text = "Number of Injured"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(69, 28)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(127, 16)
        Me.Label30.TabIndex = 42
        Me.Label30.Text = "Number of Fatalities"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(94, 114)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(102, 16)
        Me.Label31.TabIndex = 41
        Me.Label31.Text = "Damage Grade"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.Label34)
        Me.GroupBox15.Controls.Add(Me.tbEXPOSURE_COMMENTS)
        Me.GroupBox15.Controls.Add(Me.tbREPLACEMENT_COST)
        Me.GroupBox15.Controls.Add(Me.tbPLAN_AREA)
        Me.GroupBox15.Controls.Add(Me.Label33)
        Me.GroupBox15.Controls.Add(Me.Label32)
        Me.GroupBox15.Controls.Add(Me.Label26)
        Me.GroupBox15.Controls.Add(Me.cbCURRENCY)
        Me.GroupBox15.Controls.Add(Me.tbNUM_DWELLINGS)
        Me.GroupBox15.Controls.Add(Me.tbNUM_TRANSIT_OCCUPANTS)
        Me.GroupBox15.Controls.Add(Me.tbNUM_NIGHT_OCCUPANTS)
        Me.GroupBox15.Controls.Add(Me.tbNUM_DAY_OCCUPANTS)
        Me.GroupBox15.Controls.Add(Me.Label25)
        Me.GroupBox15.Controls.Add(Me.Label24)
        Me.GroupBox15.Controls.Add(Me.Label20)
        Me.GroupBox15.Controls.Add(Me.Label23)
        Me.GroupBox15.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(798, 303)
        Me.GroupBox15.TabIndex = 87
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Exposure"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(123, 196)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(72, 16)
        Me.Label34.TabIndex = 105
        Me.Label34.Text = "Comments"
        '
        'tbEXPOSURE_COMMENTS
        '
        Me.tbEXPOSURE_COMMENTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "COMMENTS", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbEXPOSURE_COMMENTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbEXPOSURE_COMMENTS.Location = New System.Drawing.Point(208, 193)
        Me.tbEXPOSURE_COMMENTS.Multiline = True
        Me.tbEXPOSURE_COMMENTS.Name = "tbEXPOSURE_COMMENTS"
        Me.tbEXPOSURE_COMMENTS.Size = New System.Drawing.Size(584, 98)
        Me.tbEXPOSURE_COMMENTS.TabIndex = 105
        '
        'GEDBindingSource
        '
        Me.GEDBindingSource.DataMember = "GED"
        Me.GEDBindingSource.DataSource = Me.GEMDataset
        '
        'tbREPLACEMENT_COST
        '
        Me.tbREPLACEMENT_COST.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "REPLC_COST", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbREPLACEMENT_COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbREPLACEMENT_COST.Location = New System.Drawing.Point(208, 165)
        Me.tbREPLACEMENT_COST.Name = "tbREPLACEMENT_COST"
        Me.tbREPLACEMENT_COST.Size = New System.Drawing.Size(128, 22)
        Me.tbREPLACEMENT_COST.TabIndex = 105
        '
        'tbPLAN_AREA
        '
        Me.tbPLAN_AREA.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "PLAN_AREA", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbPLAN_AREA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLAN_AREA.Location = New System.Drawing.Point(208, 137)
        Me.tbPLAN_AREA.Name = "tbPLAN_AREA"
        Me.tbPLAN_AREA.Size = New System.Drawing.Size(128, 22)
        Me.tbPLAN_AREA.TabIndex = 104
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(15, 84)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(181, 16)
        Me.Label33.TabIndex = 103
        Me.Label33.Text = "Number of Transit Occupants"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(25, 56)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(171, 16)
        Me.Label32.TabIndex = 102
        Me.Label32.Text = "Number of Night Occupants"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(360, 166)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(61, 16)
        Me.Label26.TabIndex = 101
        Me.Label26.Text = "Currency"
        '
        'cbCURRENCY
        '
        Me.cbCURRENCY.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.GEDBindingSource, "CURRENCY", True))
        Me.cbCURRENCY.DataSource = Me.DICCURRENCYBindingSource
        Me.cbCURRENCY.DisplayMember = "DESCRIPTION"
        Me.cbCURRENCY.DropDownHeight = 400
        Me.cbCURRENCY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCURRENCY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCURRENCY.FormattingEnabled = True
        Me.cbCURRENCY.IntegralHeight = False
        Me.cbCURRENCY.Location = New System.Drawing.Point(427, 163)
        Me.cbCURRENCY.Name = "cbCURRENCY"
        Me.cbCURRENCY.Size = New System.Drawing.Size(363, 24)
        Me.cbCURRENCY.TabIndex = 100
        Me.cbCURRENCY.ValueMember = "CODE"
        '
        'DICCURRENCYBindingSource
        '
        Me.DICCURRENCYBindingSource.DataMember = "DIC_CURRENCY"
        Me.DICCURRENCYBindingSource.DataSource = Me.GEMDataset
        '
        'tbNUM_DWELLINGS
        '
        Me.tbNUM_DWELLINGS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "NUM_DWELL", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_DWELLINGS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_DWELLINGS.Location = New System.Drawing.Point(208, 109)
        Me.tbNUM_DWELLINGS.Name = "tbNUM_DWELLINGS"
        Me.tbNUM_DWELLINGS.Size = New System.Drawing.Size(128, 22)
        Me.tbNUM_DWELLINGS.TabIndex = 99
        '
        'tbNUM_TRANSIT_OCCUPANTS
        '
        Me.tbNUM_TRANSIT_OCCUPANTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "TRANS_OCC", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_TRANSIT_OCCUPANTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_TRANSIT_OCCUPANTS.Location = New System.Drawing.Point(208, 81)
        Me.tbNUM_TRANSIT_OCCUPANTS.Name = "tbNUM_TRANSIT_OCCUPANTS"
        Me.tbNUM_TRANSIT_OCCUPANTS.Size = New System.Drawing.Size(128, 22)
        Me.tbNUM_TRANSIT_OCCUPANTS.TabIndex = 98
        '
        'tbNUM_NIGHT_OCCUPANTS
        '
        Me.tbNUM_NIGHT_OCCUPANTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "NIGHT_OCC", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_NIGHT_OCCUPANTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_NIGHT_OCCUPANTS.Location = New System.Drawing.Point(208, 53)
        Me.tbNUM_NIGHT_OCCUPANTS.Name = "tbNUM_NIGHT_OCCUPANTS"
        Me.tbNUM_NIGHT_OCCUPANTS.Size = New System.Drawing.Size(128, 22)
        Me.tbNUM_NIGHT_OCCUPANTS.TabIndex = 97
        '
        'tbNUM_DAY_OCCUPANTS
        '
        Me.tbNUM_DAY_OCCUPANTS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GEDBindingSource, "DAY_OCC", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbNUM_DAY_OCCUPANTS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNUM_DAY_OCCUPANTS.Location = New System.Drawing.Point(208, 25)
        Me.tbNUM_DAY_OCCUPANTS.Name = "tbNUM_DAY_OCCUPANTS"
        Me.tbNUM_DAY_OCCUPANTS.Size = New System.Drawing.Size(128, 22)
        Me.tbNUM_DAY_OCCUPANTS.TabIndex = 96
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(76, 168)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(119, 16)
        Me.Label25.TabIndex = 44
        Me.Label25.Text = "Replacement Cost"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(129, 140)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(67, 16)
        Me.Label24.TabIndex = 43
        Me.Label24.Text = "Plan Area"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(65, 112)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(131, 16)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Number fo Dwellings"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(31, 28)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(165, 16)
        Me.Label23.TabIndex = 41
        Me.Label23.Text = "Number of Day Occupants"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.GroupBox17)
        Me.TabPage5.Location = New System.Drawing.Point(4, 25)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(815, 586)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Photographs and Media"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.dgMedia)
        Me.GroupBox17.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(803, 545)
        Me.GroupBox17.TabIndex = 88
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Photographs and Media"
        '
        'dgMedia
        '
        Me.dgMedia.AutoGenerateColumns = False
        Me.dgMedia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgMedia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMedia.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MEDIATYPEDataGridViewTextBoxColumn, Me.MEDIAUIDDataGridViewTextBoxColumn, Me.MEDIANUMBDataGridViewTextBoxColumn, Me.ORIGFILENDataGridViewTextBoxColumn, Me.FILENAMEDataGridViewTextBoxColumn, Me.COMMENTSDataGridViewTextBoxColumn, Me.GEMOBJUIDDataGridViewTextBoxColumn})
        Me.dgMedia.DataSource = Me.MEDIADETAILBindingSource
        Me.dgMedia.Location = New System.Drawing.Point(6, 19)
        Me.dgMedia.Name = "dgMedia"
        Me.dgMedia.Size = New System.Drawing.Size(791, 520)
        Me.dgMedia.TabIndex = 0
        '
        'MEDIATYPEDataGridViewTextBoxColumn
        '
        Me.MEDIATYPEDataGridViewTextBoxColumn.DataPropertyName = "MEDIA_TYPE"
        Me.MEDIATYPEDataGridViewTextBoxColumn.DataSource = Me.DICMEDIATYPEBindingSource
        Me.MEDIATYPEDataGridViewTextBoxColumn.DisplayMember = "DESCRIPTION"
        Me.MEDIATYPEDataGridViewTextBoxColumn.FillWeight = 25.0!
        Me.MEDIATYPEDataGridViewTextBoxColumn.HeaderText = "Media Type"
        Me.MEDIATYPEDataGridViewTextBoxColumn.Name = "MEDIATYPEDataGridViewTextBoxColumn"
        Me.MEDIATYPEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MEDIATYPEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.MEDIATYPEDataGridViewTextBoxColumn.ValueMember = "CODE"
        '
        'DICMEDIATYPEBindingSource
        '
        Me.DICMEDIATYPEBindingSource.DataMember = "DIC_MEDIA_TYPE"
        Me.DICMEDIATYPEBindingSource.DataSource = Me.GEMDataset
        '
        'MEDIAUIDDataGridViewTextBoxColumn
        '
        Me.MEDIAUIDDataGridViewTextBoxColumn.DataPropertyName = "MEDIA_UID"
        Me.MEDIAUIDDataGridViewTextBoxColumn.HeaderText = "MEDIA_UID"
        Me.MEDIAUIDDataGridViewTextBoxColumn.Name = "MEDIAUIDDataGridViewTextBoxColumn"
        Me.MEDIAUIDDataGridViewTextBoxColumn.Visible = False
        '
        'MEDIANUMBDataGridViewTextBoxColumn
        '
        Me.MEDIANUMBDataGridViewTextBoxColumn.DataPropertyName = "MEDIA_NUMB"
        Me.MEDIANUMBDataGridViewTextBoxColumn.FillWeight = 15.0!
        Me.MEDIANUMBDataGridViewTextBoxColumn.HeaderText = "Frame Number"
        Me.MEDIANUMBDataGridViewTextBoxColumn.Name = "MEDIANUMBDataGridViewTextBoxColumn"
        '
        'ORIGFILENDataGridViewTextBoxColumn
        '
        Me.ORIGFILENDataGridViewTextBoxColumn.DataPropertyName = "ORIG_FILEN"
        Me.ORIGFILENDataGridViewTextBoxColumn.FillWeight = 20.0!
        Me.ORIGFILENDataGridViewTextBoxColumn.HeaderText = "Original Filename"
        Me.ORIGFILENDataGridViewTextBoxColumn.Name = "ORIGFILENDataGridViewTextBoxColumn"
        '
        'FILENAMEDataGridViewTextBoxColumn
        '
        Me.FILENAMEDataGridViewTextBoxColumn.DataPropertyName = "FILENAME"
        Me.FILENAMEDataGridViewTextBoxColumn.FillWeight = 20.0!
        Me.FILENAMEDataGridViewTextBoxColumn.HeaderText = "GEM Filename"
        Me.FILENAMEDataGridViewTextBoxColumn.Name = "FILENAMEDataGridViewTextBoxColumn"
        '
        'COMMENTSDataGridViewTextBoxColumn
        '
        Me.COMMENTSDataGridViewTextBoxColumn.DataPropertyName = "COMMENTS"
        Me.COMMENTSDataGridViewTextBoxColumn.HeaderText = "Comments"
        Me.COMMENTSDataGridViewTextBoxColumn.Name = "COMMENTSDataGridViewTextBoxColumn"
        '
        'GEMOBJUIDDataGridViewTextBoxColumn
        '
        Me.GEMOBJUIDDataGridViewTextBoxColumn.DataPropertyName = "GEMOBJ_UID"
        Me.GEMOBJUIDDataGridViewTextBoxColumn.HeaderText = "GEMOBJ_UID"
        Me.GEMOBJUIDDataGridViewTextBoxColumn.Name = "GEMOBJUIDDataGridViewTextBoxColumn"
        Me.GEMOBJUIDDataGridViewTextBoxColumn.Visible = False
        '
        'MEDIADETAILBindingSource
        '
        Me.MEDIADETAILBindingSource.DataMember = "MEDIA_DETAIL"
        Me.MEDIADETAILBindingSource.DataSource = Me.GEMDataset
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.WebBrowser1)
        Me.TabPage6.Location = New System.Drawing.Point(4, 25)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(815, 586)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Help"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(809, 580)
        Me.WebBrowser1.TabIndex = 0
        '
        'btInsertRecord
        '
        Me.btInsertRecord.Location = New System.Drawing.Point(140, 618)
        Me.btInsertRecord.Name = "btInsertRecord"
        Me.btInsertRecord.Size = New System.Drawing.Size(145, 23)
        Me.btInsertRecord.TabIndex = 75
        Me.btInsertRecord.Text = "Update Changes"
        Me.btInsertRecord.UseVisualStyleBackColor = True
        '
        'btCancelChanges
        '
        Me.btCancelChanges.Location = New System.Drawing.Point(545, 618)
        Me.btCancelChanges.Name = "btCancelChanges"
        Me.btCancelChanges.Size = New System.Drawing.Size(145, 23)
        Me.btCancelChanges.TabIndex = 76
        Me.btCancelChanges.Text = "Cancel Changes"
        Me.btCancelChanges.UseVisualStyleBackColor = True
        '
        'btDeleteRecord
        '
        Me.btDeleteRecord.Location = New System.Drawing.Point(341, 618)
        Me.btDeleteRecord.Name = "btDeleteRecord"
        Me.btDeleteRecord.Size = New System.Drawing.Size(145, 23)
        Me.btDeleteRecord.TabIndex = 77
        Me.btDeleteRecord.Text = "Delete Record"
        Me.btDeleteRecord.UseVisualStyleBackColor = True
        Me.btDeleteRecord.Visible = False
        '
        'GEM_OBJECTTableAdapter
        '
        Me.GEM_OBJECTTableAdapter.ClearBeforeFill = True
        '
        'DIC_MATERIAL_TYPETableAdapter
        '
        Me.DIC_MATERIAL_TYPETableAdapter.ClearBeforeFill = True
        '
        'DIC_OCCUPANCYTableAdapter
        '
        Me.DIC_OCCUPANCYTableAdapter.ClearBeforeFill = True
        '
        'DIC_OCCUPANCY_DETAILTableAdapter
        '
        Me.DIC_OCCUPANCY_DETAILTableAdapter.ClearBeforeFill = True
        '
        'MEDIA_DETAILTableAdapter
        '
        Me.MEDIA_DETAILTableAdapter.ClearBeforeFill = True
        '
        'DIC_MEDIA_TYPETableAdapter
        '
        Me.DIC_MEDIA_TYPETableAdapter.ClearBeforeFill = True
        '
        'CONSEQUENCESTableAdapter
        '
        Me.CONSEQUENCESTableAdapter.ClearBeforeFill = True
        '
        'DIC_MATERIAL_TECHNOLOGYTableAdapter
        '
        Me.DIC_MATERIAL_TECHNOLOGYTableAdapter.ClearBeforeFill = True
        '
        'DIC_MASONARY_MORTAR_TYPETableAdapter
        '
        Me.DIC_MASONARY_MORTAR_TYPETableAdapter.ClearBeforeFill = True
        '
        'DIC_MASONRY_REINFORCEMENTTableAdapter
        '
        Me.DIC_MASONRY_REINFORCEMENTTableAdapter.ClearBeforeFill = True
        '
        'DIC_STEEL_CONNECTION_TYPETableAdapter
        '
        Me.DIC_STEEL_CONNECTION_TYPETableAdapter.ClearBeforeFill = True
        '
        'DIC_STRUCTURAL_IRREGULARITYTableAdapter
        '
        Me.DIC_STRUCTURAL_IRREGULARITYTableAdapter.ClearBeforeFill = True
        '
        'DIC_STRUCTURAL_HORIZ_IRREGTableAdapter
        '
        Me.DIC_STRUCTURAL_HORIZ_IRREGTableAdapter.ClearBeforeFill = True
        '
        'DIC_STRUCTURAL_VERT_IRREGTableAdapter
        '
        Me.DIC_STRUCTURAL_VERT_IRREGTableAdapter.ClearBeforeFill = True
        '
        'DIC_LLRSTableAdapter
        '
        Me.DIC_LLRSTableAdapter.ClearBeforeFill = True
        '
        'DIC_LLRS_DUCTILITYTableAdapter
        '
        Me.DIC_LLRS_DUCTILITYTableAdapter.ClearBeforeFill = True
        '
        'DIC_ROOF_SHAPETableAdapter
        '
        Me.DIC_ROOF_SHAPETableAdapter.ClearBeforeFill = True
        '
        'DIC_ROOF_COVER_MATERIALTableAdapter
        '
        Me.DIC_ROOF_COVER_MATERIALTableAdapter.ClearBeforeFill = True
        '
        'DIC_ROOF_SYSTEM_MATERIALTableAdapter
        '
        Me.DIC_ROOF_SYSTEM_MATERIALTableAdapter.ClearBeforeFill = True
        '
        'DIC_ROOF_SYSTEM_TYPETableAdapter
        '
        Me.DIC_ROOF_SYSTEM_TYPETableAdapter.ClearBeforeFill = True
        '
        'DIC_FLOOR_MATERIALTableAdapter
        '
        Me.DIC_FLOOR_MATERIALTableAdapter.ClearBeforeFill = True
        '
        'DIC_FLOOR_TYPETableAdapter
        '
        Me.DIC_FLOOR_TYPETableAdapter.ClearBeforeFill = True
        '
        'DIC_FOUNDATION_SYSTEMTableAdapter
        '
        Me.DIC_FOUNDATION_SYSTEMTableAdapter.ClearBeforeFill = True
        '
        'DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter
        '
        Me.DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter.ClearBeforeFill = True
        '
        'DIC_YEAR_BUILT_QUALTableAdapter
        '
        Me.DIC_YEAR_BUILT_QUALTableAdapter.ClearBeforeFill = True
        '
        'DIC_POSITIONTableAdapter
        '
        Me.DIC_POSITIONTableAdapter.ClearBeforeFill = True
        '
        'GEDTableAdapter
        '
        Me.GEDTableAdapter.ClearBeforeFill = True
        '
        'DIC_PLAN_SHAPETableAdapter
        '
        Me.DIC_PLAN_SHAPETableAdapter.ClearBeforeFill = True
        '
        'DIC_CURRENCYTableAdapter
        '
        Me.DIC_CURRENCYTableAdapter.ClearBeforeFill = True
        '
        'DIC_DAMAGETableAdapter
        '
        Me.DIC_DAMAGETableAdapter.ClearBeforeFill = True
        '
        'DIC_HEIGHT_QUALIFIERTableAdapter
        '
        Me.DIC_HEIGHT_QUALIFIERTableAdapter.ClearBeforeFill = True
        '
        'mnuRow
        '
        Me.mnuRow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowMediaToolStripMenuItem, Me.LinkToMediaToolStripMenuItem})
        Me.mnuRow.Name = "mnuRow"
        Me.mnuRow.Size = New System.Drawing.Size(153, 70)
        '
        'ShowMediaToolStripMenuItem
        '
        Me.ShowMediaToolStripMenuItem.Name = "ShowMediaToolStripMenuItem"
        Me.ShowMediaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ShowMediaToolStripMenuItem.Text = "Show Media"
        '
        'LinkToMediaToolStripMenuItem
        '
        Me.LinkToMediaToolStripMenuItem.Name = "LinkToMediaToolStripMenuItem"
        Me.LinkToMediaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LinkToMediaToolStripMenuItem.Text = "Add Media"
        '
        'frmDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 653)
        Me.Controls.Add(Me.btDeleteRecord)
        Me.Controls.Add(Me.btCancelChanges)
        Me.Controls.Add(Me.btInsertRecord)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.DataGridButton)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.DeleteButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add New Observation"
        CType(Me.GEMOBJECTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GEMDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMATERIALTYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMATERIALTECHNOLOGYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMASONARYMORTARTYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMASONRYREINFORCEMENTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICSTEELCONNECTIONTYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICSTRUCTURALIRREGULARITYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICSTRUCTURALHORIZIRREGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICSTRUCTURALVERTIRREGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DICSTEELCONNECTIONTYPEBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMASONRYREINFORCEMENTBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMASONARYMORTARTYPEBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMATERIALTECHNOLOGYBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMATERIALTYPEBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DICSTRUCTURALVERTIRREGBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICSTRUCTURALHORIZIRREGBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        CType(Me.DICNONSTRUCTURALEXTERIORWALLSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.DICFOUNDATIONSYSTEMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICFLOORTYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICFLOORMATERIALBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.DICROOFSHAPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICROOFSYSTEMTYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICROOFSYSTEMMATERIALBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICROOFCOVERMATERIALBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DICLLRSDUCTILITYBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICLLRSBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICLLRSDUCTILITYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICLLRSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DICOCCUPANCYDETAILBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICOCCUPANCYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        CType(Me.DICPLANSHAPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICPOSITIONBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        CType(Me.DICHEIGHTQUALIFIERBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICHEIGHTQUALIFIERBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICHEIGHTQUALIFIERBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.DICYEARBUILTQUALBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        CType(Me.CONSEQUENCESBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICDAMAGEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        CType(Me.GEDBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICCURRENCYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        CType(Me.dgMedia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DICMEDIATYPEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MEDIADETAILBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.mnuRow.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents DataGridButton As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents cbMATERIAL_TYPE_L As System.Windows.Forms.ComboBox
    Friend WithEvents cbMATERIAL_TECHNOLOGY_L As System.Windows.Forms.ComboBox
    Friend WithEvents cbMASONRY_MORTAR_TYPE_L As System.Windows.Forms.ComboBox
    Friend WithEvents cbMASONRY_REINFORCEMENT_L As System.Windows.Forms.ComboBox
    Friend WithEvents cbSTEEL_CONNECTION_TYPE_L As System.Windows.Forms.ComboBox
    Friend WithEvents lblMTYPE As System.Windows.Forms.Label
    Friend WithEvents lblMTECH As System.Windows.Forms.Label
    Friend WithEvents lblMORT As System.Windows.Forms.Label
    Friend WithEvents lblMREIN As System.Windows.Forms.Label
    Friend WithEvents lblSCONN As System.Windows.Forms.Label
    Friend WithEvents cbSTRUCTURAL_IRREGULARITY As System.Windows.Forms.ComboBox
    Friend WithEvents cbSTRUCTURAL_HORIZ_IRREG_P As System.Windows.Forms.ComboBox
    Friend WithEvents cbSTRUCTURAL_VERT_IRREG_P As System.Windows.Forms.ComboBox
    Friend WithEvents lblSTRI As System.Windows.Forms.Label
    Friend WithEvents lblSTRHI As System.Windows.Forms.Label
    Friend WithEvents lblSTRHVI As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents tbLocationX As System.Windows.Forms.TextBox
    Friend WithEvents tbLocationY As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents tbOBJECT_COMMENTS As System.Windows.Forms.TextBox
    Friend WithEvents cbFavs As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblTrans1 As System.Windows.Forms.Label
    Friend WithEvents cbSTEEL_CONNECTION_TYPE_T As System.Windows.Forms.ComboBox
    Friend WithEvents cbMASONRY_REINFORCEMENT_T As System.Windows.Forms.ComboBox
    Friend WithEvents cbMASONRY_MORTAR_TYPE_T As System.Windows.Forms.ComboBox
    Friend WithEvents cbMATERIAL_TECHNOLOGY_T As System.Windows.Forms.ComboBox
    Friend WithEvents cbMATERIAL_TYPE_T As System.Windows.Forms.ComboBox
    Friend WithEvents lblLong1 As System.Windows.Forms.Label
    Friend WithEvents lblSec1 As System.Windows.Forms.Label
    Friend WithEvents cbSTRUCTURAL_VERT_IRREG_S As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrim1 As System.Windows.Forms.Label
    Friend WithEvents cbSTRUCTURAL_HORIZ_IRREG_S As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbFOUNDATION_SYSTEM As System.Windows.Forms.ComboBox
    Friend WithEvents lblFTYPE As System.Windows.Forms.Label
    Friend WithEvents lblFMAT As System.Windows.Forms.Label
    Friend WithEvents cbFLOOR_TYPE As System.Windows.Forms.ComboBox
    Friend WithEvents cbFLOOR_MATERIAL As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cbROOF_SHAPE As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbROOF_SYSTEM_TYPE As System.Windows.Forms.ComboBox
    Friend WithEvents lblRTYPE As System.Windows.Forms.Label
    Friend WithEvents lblRMAT As System.Windows.Forms.Label
    Friend WithEvents cbROOF_SYSTEM_MATERIAL As System.Windows.Forms.ComboBox
    Friend WithEvents cbROOF_COVER_MATERIAL As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbLLRS_DUCTILITY_T As System.Windows.Forms.ComboBox
    Friend WithEvents cbLLRS_T As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbLLRS_DUCTILITY_L As System.Windows.Forms.ComboBox
    Friend WithEvents cbLLRS_L As System.Windows.Forms.ComboBox
    Friend WithEvents lblTrans2 As System.Windows.Forms.Label
    Friend WithEvents lblLong2 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbNONSTRUCTURAL_EXTERIOR_WALLS As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOCCD As System.Windows.Forms.Label
    Friend WithEvents lblOCC As System.Windows.Forms.Label
    Friend WithEvents cbOCCUPANCY_DETAIL As System.Windows.Forms.ComboBox
    Friend WithEvents cbOCCUPANCY As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents cbHT_ABOVEGRADE_GRND_FLOOR_QUAL As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbNO_STOREYS_BELOW_GROUND_QUAL As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cbNO_STOREYS_ABOVE_GROUND_QUAL As System.Windows.Forms.ComboBox
    Friend WithEvents lblH1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cbYEAR_BUILT_QUAL As System.Windows.Forms.ComboBox
    Friend WithEvents lblD1 As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents tbCONSEQUENCES_COMMENTS As System.Windows.Forms.TextBox
    Friend WithEvents cbDAMAGE_CODE As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cbCURRENCY As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents tbEXPOSURE_COMMENTS As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents btFavSave As System.Windows.Forms.Button
    Friend WithEvents btFavLoad As System.Windows.Forms.Button
    Friend WithEvents btInsertRecord As System.Windows.Forms.Button
    Friend WithEvents btCancelChanges As System.Windows.Forms.Button
    Friend WithEvents btDeleteRecord As System.Windows.Forms.Button
    Friend WithEvents tbSLOPE As MapWindow.NumericTextBox
    Friend WithEvents tbHT_ABOVEGRADE_GRND_FLOOR_2 As MapWindow.NumericTextBox
    Friend WithEvents tbHT_ABOVEGRADE_GRND_FLOOR_1 As MapWindow.NumericTextBox
    Friend WithEvents tbNO_STOREYS_BELOW_GROUND_2 As MapWindow.NumericTextBox
    Friend WithEvents tbNO_STOREYS_BELOW_GROUND_1 As MapWindow.NumericTextBox
    Friend WithEvents tbNO_STOREYS_ABOVE_GROUND_2 As MapWindow.NumericTextBox
    Friend WithEvents tbNO_STOREYS_ABOVE_GROUND_1 As MapWindow.NumericTextBox
    Friend WithEvents tbYEAR_BUILT_2 As MapWindow.NumericTextBox
    Friend WithEvents tbYEAR_BUILT_1 As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_MISSING As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_INJURED As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_FATALITIES As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_DWELLINGS As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_TRANSIT_OCCUPANTS As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_NIGHT_OCCUPANTS As MapWindow.NumericTextBox
    Friend WithEvents tbNUM_DAY_OCCUPANTS As MapWindow.NumericTextBox
    Friend WithEvents tbREPLACEMENT_COST As MapWindow.NumericTextBox
    Friend WithEvents tbPLAN_AREA As MapWindow.NumericTextBox
    Friend WithEvents dgMedia As System.Windows.Forms.DataGridView
    Friend WithEvents GEMDataset As MapWindow.GEMDataset
    Friend WithEvents GEMOBJECTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GEM_OBJECTTableAdapter As MapWindow.GEMDatasetTableAdapters.GEM_OBJECTTableAdapter
    Friend WithEvents DICMATERIALTYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_MATERIAL_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_MATERIAL_TYPETableAdapter
    Friend WithEvents DICOCCUPANCYBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_OCCUPANCYTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_OCCUPANCYTableAdapter
    Friend WithEvents DICOCCUPANCYDETAILBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_OCCUPANCY_DETAILTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_OCCUPANCY_DETAILTableAdapter
    Friend WithEvents MEDIADETAILBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MEDIA_DETAILTableAdapter As MapWindow.GEMDatasetTableAdapters.MEDIA_DETAILTableAdapter
    Friend WithEvents DICMEDIATYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_MEDIA_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_MEDIA_TYPETableAdapter
    Friend WithEvents CONSEQUENCESBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CONSEQUENCESTableAdapter As MapWindow.GEMDatasetTableAdapters.CONSEQUENCESTableAdapter
    Friend WithEvents DICMATERIALTECHNOLOGYBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_MATERIAL_TECHNOLOGYTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_MATERIAL_TECHNOLOGYTableAdapter
    Friend WithEvents DICMASONARYMORTARTYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_MASONARY_MORTAR_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_MASONARY_MORTAR_TYPETableAdapter
    Friend WithEvents DICMASONRYREINFORCEMENTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_MASONRY_REINFORCEMENTTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_MASONRY_REINFORCEMENTTableAdapter
    Friend WithEvents DICSTEELCONNECTIONTYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_STEEL_CONNECTION_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_STEEL_CONNECTION_TYPETableAdapter
    Friend WithEvents DICSTRUCTURALIRREGULARITYBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_STRUCTURAL_IRREGULARITYTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_IRREGULARITYTableAdapter
    Friend WithEvents DICSTRUCTURALHORIZIRREGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_STRUCTURAL_HORIZ_IRREGTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_HORIZ_IRREGTableAdapter
    Friend WithEvents DICSTRUCTURALVERTIRREGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_STRUCTURAL_VERT_IRREGTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_STRUCTURAL_VERT_IRREGTableAdapter
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbPOSITION As System.Windows.Forms.ComboBox
    Friend WithEvents DICMATERIALTYPEBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICSTEELCONNECTIONTYPEBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICMASONRYREINFORCEMENTBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICMASONARYMORTARTYPEBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICMATERIALTECHNOLOGYBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICSTRUCTURALVERTIRREGBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICSTRUCTURALHORIZIRREGBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents chbAdvancedView As System.Windows.Forms.CheckBox
    Friend WithEvents DICLLRSBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_LLRSTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_LLRSTableAdapter
    Friend WithEvents DICLLRSBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICLLRSDUCTILITYBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_LLRS_DUCTILITYTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_LLRS_DUCTILITYTableAdapter
    Friend WithEvents DICLLRSDUCTILITYBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DICROOFSHAPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_ROOF_SHAPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SHAPETableAdapter
    Friend WithEvents DICROOFCOVERMATERIALBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_ROOF_COVER_MATERIALTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_ROOF_COVER_MATERIALTableAdapter
    Friend WithEvents DICROOFSYSTEMMATERIALBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_ROOF_SYSTEM_MATERIALTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SYSTEM_MATERIALTableAdapter
    Friend WithEvents DICROOFSYSTEMTYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_ROOF_SYSTEM_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_ROOF_SYSTEM_TYPETableAdapter
    Friend WithEvents DICFLOORMATERIALBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_FLOOR_MATERIALTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_FLOOR_MATERIALTableAdapter
    Friend WithEvents DICFLOORTYPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_FLOOR_TYPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_FLOOR_TYPETableAdapter
    Friend WithEvents DICFOUNDATIONSYSTEMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_FOUNDATION_SYSTEMTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_FOUNDATION_SYSTEMTableAdapter
    Friend WithEvents DICNONSTRUCTURALEXTERIORWALLSBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_NONSTRUCTURAL_EXTERIOR_WALLSTableAdapter
    Friend WithEvents DICYEARBUILTQUALBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_YEAR_BUILT_QUALTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_YEAR_BUILT_QUALTableAdapter
    Friend WithEvents DICPOSITIONBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_POSITIONTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_POSITIONTableAdapter
    Friend WithEvents GEDBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GEDTableAdapter As MapWindow.GEMDatasetTableAdapters.GEDTableAdapter
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cbPLAN_SHAPE As System.Windows.Forms.ComboBox
    Friend WithEvents DICPLANSHAPEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_PLAN_SHAPETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_PLAN_SHAPETableAdapter
    Friend WithEvents DICCURRENCYBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_CURRENCYTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_CURRENCYTableAdapter
    Friend WithEvents DICDAMAGEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_DAMAGETableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_DAMAGETableAdapter
    Friend WithEvents DICHEIGHTQUALIFIERBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DIC_HEIGHT_QUALIFIERTableAdapter As MapWindow.GEMDatasetTableAdapters.DIC_HEIGHT_QUALIFIERTableAdapter
    Friend WithEvents DICHEIGHTQUALIFIERBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents DICHEIGHTQUALIFIERBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents mnuRow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowMediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkToMediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MEDIATYPEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents MEDIAUIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MEDIANUMBDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ORIGFILENDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FILENAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COMMENTSDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GEMOBJUIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
