<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditNAGs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditNAGs))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grdNAGs = New System.Windows.Forms.DataGridView()
        Me.lblAnswersNl = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.mnuPopUp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdNAGs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuPopUp.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdNAGs
        '
        resources.ApplyResources(Me.grdNAGs, "grdNAGs")
        Me.grdNAGs.AllowUserToResizeColumns = False
        Me.grdNAGs.AllowUserToResizeRows = False
        Me.grdNAGs.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdNAGs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdNAGs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdNAGs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.grdNAGs.MultiSelect = False
        Me.grdNAGs.Name = "grdNAGs"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdNAGs.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdNAGs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdNAGs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
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
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn2.DividerWidth = 1
        Me.DataGridViewTextBoxColumn2.FillWeight = 50.0!
        resources.ApplyResources(Me.DataGridViewTextBoxColumn2, "DataGridViewTextBoxColumn2")
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn3.DividerWidth = 1
        resources.ApplyResources(Me.DataGridViewTextBoxColumn3, "DataGridViewTextBoxColumn3")
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'frmEditNAGs
        '
        Me.AcceptButton = Me.cmdOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.grdNAGs)
        Me.Controls.Add(Me.lblAnswersNl)
        Me.Name = "frmEditNAGs"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.grdNAGs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuPopUp.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents grdNAGs As DataGridView
    Friend WithEvents lblAnswersNl As Label
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cmdOK As Button
    Friend WithEvents mnuPopUp As ContextMenuStrip
    Friend WithEvents mnuDeleteRow As ToolStripMenuItem
    Friend WithEvents mnuClearAll As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
End Class
