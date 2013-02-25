<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhotoMan
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
        Me.dgMedia = New System.Windows.Forms.DataGridView()
        Me.MEDIADETAILBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GEMDataset = New MapWindow.GEMDataset()
        Me.MEDIA_DETAILTableAdapter = New MapWindow.GEMDatasetTableAdapters.MEDIA_DETAILTableAdapter()
        Me.ShowAll = New System.Windows.Forms.RadioButton()
        Me.ShowMediaNotLinked = New System.Windows.Forms.RadioButton()
        Me.mnuRow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowMediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinkToMediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GEMOBJUIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEDIAUIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEDIATYPEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEDIA_NUMB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORIGFILENDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FILENAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COMMENTSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.dgMedia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MEDIADETAILBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GEMDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuRow.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgMedia
        '
        Me.dgMedia.AutoGenerateColumns = False
        Me.dgMedia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMedia.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GEMOBJUIDDataGridViewTextBoxColumn, Me.MEDIAUIDDataGridViewTextBoxColumn, Me.MEDIATYPEDataGridViewTextBoxColumn, Me.MEDIA_NUMB, Me.ORIGFILENDataGridViewTextBoxColumn, Me.FILENAMEDataGridViewTextBoxColumn, Me.COMMENTSDataGridViewTextBoxColumn})
        Me.dgMedia.DataSource = Me.MEDIADETAILBindingSource
        Me.dgMedia.Location = New System.Drawing.Point(7, 50)
        Me.dgMedia.Name = "dgMedia"
        Me.dgMedia.Size = New System.Drawing.Size(743, 453)
        Me.dgMedia.TabIndex = 0
        '
        'MEDIADETAILBindingSource
        '
        Me.MEDIADETAILBindingSource.DataMember = "MEDIA_DETAIL"
        Me.MEDIADETAILBindingSource.DataSource = Me.GEMDataset
        '
        'GEMDataset
        '
        Me.GEMDataset.DataSetName = "GEMDataset"
        Me.GEMDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'MEDIA_DETAILTableAdapter
        '
        Me.MEDIA_DETAILTableAdapter.ClearBeforeFill = True
        '
        'ShowAll
        '
        Me.ShowAll.AutoSize = True
        Me.ShowAll.Checked = True
        Me.ShowAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowAll.Location = New System.Drawing.Point(104, 12)
        Me.ShowAll.Name = "ShowAll"
        Me.ShowAll.Size = New System.Drawing.Size(125, 20)
        Me.ShowAll.TabIndex = 1
        Me.ShowAll.TabStop = True
        Me.ShowAll.Text = "Show all records"
        Me.ShowAll.UseVisualStyleBackColor = True
        '
        'ShowMediaNotLinked
        '
        Me.ShowMediaNotLinked.AutoSize = True
        Me.ShowMediaNotLinked.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowMediaNotLinked.Location = New System.Drawing.Point(288, 12)
        Me.ShowMediaNotLinked.Name = "ShowMediaNotLinked"
        Me.ShowMediaNotLinked.Size = New System.Drawing.Size(212, 20)
        Me.ShowMediaNotLinked.TabIndex = 2
        Me.ShowMediaNotLinked.Text = "Show where media is not linked"
        Me.ShowMediaNotLinked.UseVisualStyleBackColor = True
        '
        'mnuRow
        '
        Me.mnuRow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowMediaToolStripMenuItem, Me.LinkToMediaToolStripMenuItem})
        Me.mnuRow.Name = "mnuRow"
        Me.mnuRow.Size = New System.Drawing.Size(140, 48)
        '
        'ShowMediaToolStripMenuItem
        '
        Me.ShowMediaToolStripMenuItem.Name = "ShowMediaToolStripMenuItem"
        Me.ShowMediaToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ShowMediaToolStripMenuItem.Text = "Show Media"
        '
        'LinkToMediaToolStripMenuItem
        '
        Me.LinkToMediaToolStripMenuItem.Name = "LinkToMediaToolStripMenuItem"
        Me.LinkToMediaToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.LinkToMediaToolStripMenuItem.Text = "Add Media"
        '
        'GEMOBJUIDDataGridViewTextBoxColumn
        '
        Me.GEMOBJUIDDataGridViewTextBoxColumn.DataPropertyName = "GEMOBJ_UID"
        Me.GEMOBJUIDDataGridViewTextBoxColumn.HeaderText = "OBJECT UID"
        Me.GEMOBJUIDDataGridViewTextBoxColumn.Name = "GEMOBJUIDDataGridViewTextBoxColumn"
        '
        'MEDIAUIDDataGridViewTextBoxColumn
        '
        Me.MEDIAUIDDataGridViewTextBoxColumn.DataPropertyName = "MEDIA_UID"
        Me.MEDIAUIDDataGridViewTextBoxColumn.HeaderText = "MEDIA UID"
        Me.MEDIAUIDDataGridViewTextBoxColumn.Name = "MEDIAUIDDataGridViewTextBoxColumn"
        '
        'MEDIATYPEDataGridViewTextBoxColumn
        '
        Me.MEDIATYPEDataGridViewTextBoxColumn.DataPropertyName = "MEDIA_TYPE"
        Me.MEDIATYPEDataGridViewTextBoxColumn.HeaderText = "MEDIA TYPE"
        Me.MEDIATYPEDataGridViewTextBoxColumn.Name = "MEDIATYPEDataGridViewTextBoxColumn"
        '
        'MEDIA_NUMB
        '
        Me.MEDIA_NUMB.DataPropertyName = "MEDIA_NUMB"
        Me.MEDIA_NUMB.HeaderText = "MEDIA NUMBER"
        Me.MEDIA_NUMB.Name = "MEDIA_NUMB"
        '
        'ORIGFILENDataGridViewTextBoxColumn
        '
        Me.ORIGFILENDataGridViewTextBoxColumn.DataPropertyName = "ORIG_FILEN"
        Me.ORIGFILENDataGridViewTextBoxColumn.HeaderText = "ORIGINAL FILENAME"
        Me.ORIGFILENDataGridViewTextBoxColumn.Name = "ORIGFILENDataGridViewTextBoxColumn"
        '
        'FILENAMEDataGridViewTextBoxColumn
        '
        Me.FILENAMEDataGridViewTextBoxColumn.DataPropertyName = "FILENAME"
        Me.FILENAMEDataGridViewTextBoxColumn.HeaderText = "GEM FILENAME"
        Me.FILENAMEDataGridViewTextBoxColumn.Name = "FILENAMEDataGridViewTextBoxColumn"
        '
        'COMMENTSDataGridViewTextBoxColumn
        '
        Me.COMMENTSDataGridViewTextBoxColumn.DataPropertyName = "COMMENTS"
        Me.COMMENTSDataGridViewTextBoxColumn.HeaderText = "COMMENTS"
        Me.COMMENTSDataGridViewTextBoxColumn.Name = "COMMENTSDataGridViewTextBoxColumn"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(561, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Auto Link Photos"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmPhotoMan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 519)
        Me.Controls.Add(Me.dgMedia)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ShowMediaNotLinked)
        Me.Controls.Add(Me.ShowAll)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPhotoMan"
        Me.Text = "Photo and Media Manager"
        CType(Me.dgMedia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MEDIADETAILBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GEMDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuRow.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgMedia As System.Windows.Forms.DataGridView
    Friend WithEvents GEMDataset As MapWindow.GEMDataset
    Friend WithEvents MEDIADETAILBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MEDIA_DETAILTableAdapter As MapWindow.GEMDatasetTableAdapters.MEDIA_DETAILTableAdapter
    Friend WithEvents ShowAll As System.Windows.Forms.RadioButton
    Friend WithEvents ShowMediaNotLinked As System.Windows.Forms.RadioButton
    Friend WithEvents mnuRow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowMediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkToMediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GEMOBJUIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MEDIAUIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MEDIATYPEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MEDIA_NUMB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ORIGFILENDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FILENAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COMMENTSDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
