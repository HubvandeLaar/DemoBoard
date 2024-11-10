<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEditTexts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditTexts))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grdTexts = New System.Windows.Forms.DataGridView()
        Me.lblAnswersNl = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.mnuPopUp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdTexts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuPopUp.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdTexts
        '
        resources.ApplyResources(Me.grdTexts, "grdTexts")
        Me.grdTexts.AllowUserToResizeColumns = False
        Me.grdTexts.AllowUserToResizeRows = False
        Me.grdTexts.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTexts.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTexts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTexts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.grdTexts.MultiSelect = False
        Me.grdTexts.Name = "grdTexts"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTexts.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdTexts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdTexts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'lblAnswersNl
        '
        resources.ApplyResources(Me.lblAnswersNl, "lblAnswersNl")
        Me.lblAnswersNl.Name = "lblAnswersNl"
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        resources.ApplyResources(Me.cmdRemove, "cmdRemove")
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'mnuPopUp
        '
        resources.ApplyResources(Me.mnuPopUp, "mnuPopUp")
        Me.mnuPopUp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteRow, Me.mnuClearAll})
        Me.mnuPopUp.Name = "ContextMenuStrip1"
        '
        'mnuDeleteRow
        '
        resources.ApplyResources(Me.mnuDeleteRow, "mnuDeleteRow")
        Me.mnuDeleteRow.Name = "mnuDeleteRow"
        '
        'mnuClearAll
        '
        resources.ApplyResources(Me.mnuClearAll, "mnuClearAll")
        Me.mnuClearAll.Name = "mnuClearAll"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DividerWidth = 1
        Me.DataGridViewTextBoxColumn1.FillWeight = 50.0!
        resources.ApplyResources(Me.DataGridViewTextBoxColumn1, "DataGridViewTextBoxColumn1")
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DividerWidth = 1
        resources.ApplyResources(Me.DataGridViewTextBoxColumn2, "DataGridViewTextBoxColumn2")
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DividerWidth = 1
        resources.ApplyResources(Me.DataGridViewTextBoxColumn3, "DataGridViewTextBoxColumn3")
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'frmEditTexts
        '
        Me.AcceptButton = Me.cmdOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.grdTexts)
        Me.Controls.Add(Me.lblAnswersNl)
        Me.Name = "frmEditTexts"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.grdTexts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuPopUp.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents grdTexts As DataGridView
    Friend WithEvents lblAnswersNl As Label
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents cmdOK As Button
    Friend WithEvents mnuPopUp As ContextMenuStrip
    Friend WithEvents mnuDeleteRow As ToolStripMenuItem
    Friend WithEvents mnuClearAll As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
End Class
