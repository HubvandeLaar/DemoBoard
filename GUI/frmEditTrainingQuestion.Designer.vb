<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditTrainingQuestion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditTrainingQuestion))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tabTexts = New System.Windows.Forms.TabControl()
        Me.tabEn = New System.Windows.Forms.TabPage()
        Me.grdAnswersEn = New System.Windows.Forms.DataGridView()
        Me.TrainingMove = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Points = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Feedback = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblAnswersEn = New System.Windows.Forms.Label()
        Me.rtbHint2En = New System.Windows.Forms.RichTextBox()
        Me.rtbHint1En = New System.Windows.Forms.RichTextBox()
        Me.rtbQuestionEn = New System.Windows.Forms.RichTextBox()
        Me.lblHint2En = New System.Windows.Forms.Label()
        Me.lblHint1En = New System.Windows.Forms.Label()
        Me.lblQuestionEn = New System.Windows.Forms.Label()
        Me.tabNl = New System.Windows.Forms.TabPage()
        Me.grdAnswersNl = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblAnswersNl = New System.Windows.Forms.Label()
        Me.rtbHint2Nl = New System.Windows.Forms.RichTextBox()
        Me.rtbHint1Nl = New System.Windows.Forms.RichTextBox()
        Me.rtbQuestionNl = New System.Windows.Forms.RichTextBox()
        Me.lblHint2Nl = New System.Windows.Forms.Label()
        Me.lblHint1Nl = New System.Windows.Forms.Label()
        Me.lblQuestionNl = New System.Windows.Forms.Label()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.mnuPopUp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabTexts.SuspendLayout()
        Me.tabEn.SuspendLayout()
        CType(Me.grdAnswersEn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabNl.SuspendLayout()
        CType(Me.grdAnswersNl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuPopUp.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabTexts
        '
        resources.ApplyResources(Me.tabTexts, "tabTexts")
        Me.tabTexts.Controls.Add(Me.tabEn)
        Me.tabTexts.Controls.Add(Me.tabNl)
        Me.tabTexts.Name = "tabTexts"
        Me.tabTexts.SelectedIndex = 0
        '
        'tabEn
        '
        resources.ApplyResources(Me.tabEn, "tabEn")
        Me.tabEn.Controls.Add(Me.grdAnswersEn)
        Me.tabEn.Controls.Add(Me.lblAnswersEn)
        Me.tabEn.Controls.Add(Me.rtbHint2En)
        Me.tabEn.Controls.Add(Me.rtbHint1En)
        Me.tabEn.Controls.Add(Me.rtbQuestionEn)
        Me.tabEn.Controls.Add(Me.lblHint2En)
        Me.tabEn.Controls.Add(Me.lblHint1En)
        Me.tabEn.Controls.Add(Me.lblQuestionEn)
        Me.tabEn.Name = "tabEn"
        Me.tabEn.UseVisualStyleBackColor = True
        '
        'grdAnswersEn
        '
        resources.ApplyResources(Me.grdAnswersEn, "grdAnswersEn")
        Me.grdAnswersEn.AllowUserToResizeColumns = False
        Me.grdAnswersEn.AllowUserToResizeRows = False
        Me.grdAnswersEn.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersEn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdAnswersEn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnswersEn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TrainingMove, Me.Points, Me.Feedback})
        Me.grdAnswersEn.MultiSelect = False
        Me.grdAnswersEn.Name = "grdAnswersEn"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersEn.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdAnswersEn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdAnswersEn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'TrainingMove
        '
        Me.TrainingMove.DividerWidth = 1
        resources.ApplyResources(Me.TrainingMove, "TrainingMove")
        Me.TrainingMove.Name = "TrainingMove"
        '
        'Points
        '
        Me.Points.DividerWidth = 1
        resources.ApplyResources(Me.Points, "Points")
        Me.Points.Name = "Points"
        '
        'Feedback
        '
        Me.Feedback.DividerWidth = 1
        resources.ApplyResources(Me.Feedback, "Feedback")
        Me.Feedback.Name = "Feedback"
        '
        'lblAnswersEn
        '
        resources.ApplyResources(Me.lblAnswersEn, "lblAnswersEn")
        Me.lblAnswersEn.Name = "lblAnswersEn"
        '
        'rtbHint2En
        '
        resources.ApplyResources(Me.rtbHint2En, "rtbHint2En")
        Me.rtbHint2En.Name = "rtbHint2En"
        '
        'rtbHint1En
        '
        resources.ApplyResources(Me.rtbHint1En, "rtbHint1En")
        Me.rtbHint1En.Name = "rtbHint1En"
        '
        'rtbQuestionEn
        '
        resources.ApplyResources(Me.rtbQuestionEn, "rtbQuestionEn")
        Me.rtbQuestionEn.Name = "rtbQuestionEn"
        '
        'lblHint2En
        '
        resources.ApplyResources(Me.lblHint2En, "lblHint2En")
        Me.lblHint2En.Name = "lblHint2En"
        '
        'lblHint1En
        '
        resources.ApplyResources(Me.lblHint1En, "lblHint1En")
        Me.lblHint1En.Name = "lblHint1En"
        '
        'lblQuestionEn
        '
        resources.ApplyResources(Me.lblQuestionEn, "lblQuestionEn")
        Me.lblQuestionEn.Name = "lblQuestionEn"
        '
        'tabNl
        '
        resources.ApplyResources(Me.tabNl, "tabNl")
        Me.tabNl.Controls.Add(Me.grdAnswersNl)
        Me.tabNl.Controls.Add(Me.lblAnswersNl)
        Me.tabNl.Controls.Add(Me.rtbHint2Nl)
        Me.tabNl.Controls.Add(Me.rtbHint1Nl)
        Me.tabNl.Controls.Add(Me.rtbQuestionNl)
        Me.tabNl.Controls.Add(Me.lblHint2Nl)
        Me.tabNl.Controls.Add(Me.lblHint1Nl)
        Me.tabNl.Controls.Add(Me.lblQuestionNl)
        Me.tabNl.Name = "tabNl"
        Me.tabNl.UseVisualStyleBackColor = True
        '
        'grdAnswersNl
        '
        resources.ApplyResources(Me.grdAnswersNl, "grdAnswersNl")
        Me.grdAnswersNl.AllowUserToResizeColumns = False
        Me.grdAnswersNl.AllowUserToResizeRows = False
        Me.grdAnswersNl.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersNl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdAnswersNl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnswersNl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.grdAnswersNl.MultiSelect = False
        Me.grdAnswersNl.Name = "grdAnswersNl"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersNl.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdAnswersNl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdAnswersNl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DividerWidth = 1
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
        'lblAnswersNl
        '
        resources.ApplyResources(Me.lblAnswersNl, "lblAnswersNl")
        Me.lblAnswersNl.Name = "lblAnswersNl"
        '
        'rtbHint2Nl
        '
        resources.ApplyResources(Me.rtbHint2Nl, "rtbHint2Nl")
        Me.rtbHint2Nl.Name = "rtbHint2Nl"
        '
        'rtbHint1Nl
        '
        resources.ApplyResources(Me.rtbHint1Nl, "rtbHint1Nl")
        Me.rtbHint1Nl.Name = "rtbHint1Nl"
        '
        'rtbQuestionNl
        '
        resources.ApplyResources(Me.rtbQuestionNl, "rtbQuestionNl")
        Me.rtbQuestionNl.Name = "rtbQuestionNl"
        '
        'lblHint2Nl
        '
        resources.ApplyResources(Me.lblHint2Nl, "lblHint2Nl")
        Me.lblHint2Nl.Name = "lblHint2Nl"
        '
        'lblHint1Nl
        '
        resources.ApplyResources(Me.lblHint1Nl, "lblHint1Nl")
        Me.lblHint1Nl.Name = "lblHint1Nl"
        '
        'lblQuestionNl
        '
        resources.ApplyResources(Me.lblQuestionNl, "lblQuestionNl")
        Me.lblQuestionNl.Name = "lblQuestionNl"
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        resources.ApplyResources(Me.cmdRemove, "cmdRemove")
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
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
        'frmEditTrainingQuestion
        '
        Me.AcceptButton = Me.cmdOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.tabTexts)
        Me.Name = "frmEditTrainingQuestion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.tabTexts.ResumeLayout(False)
        Me.tabEn.ResumeLayout(False)
        Me.tabEn.PerformLayout()
        CType(Me.grdAnswersEn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabNl.ResumeLayout(False)
        Me.tabNl.PerformLayout()
        CType(Me.grdAnswersNl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuPopUp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tabTexts As TabControl
    Friend WithEvents tabEn As TabPage
    Friend WithEvents tabNl As TabPage
    Friend WithEvents rtbHint2En As RichTextBox
    Friend WithEvents rtbHint1En As RichTextBox
    Friend WithEvents rtbQuestionEn As RichTextBox
    Friend WithEvents lblHint2En As Label
    Friend WithEvents lblHint1En As Label
    Friend WithEvents lblQuestionEn As Label
    Friend WithEvents rtbHint2Nl As RichTextBox
    Friend WithEvents rtbHint1Nl As RichTextBox
    Friend WithEvents rtbQuestionNl As RichTextBox
    Friend WithEvents lblHint2Nl As Label
    Friend WithEvents lblHint1Nl As Label
    Friend WithEvents lblQuestionNl As Label
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents grdAnswersEn As DataGridView
    Friend WithEvents lblAnswersEn As Label
    Friend WithEvents grdAnswersNl As DataGridView
    Friend WithEvents lblAnswersNl As Label
    Friend WithEvents mnuPopUp As ContextMenuStrip
    Friend WithEvents mnuDeleteRow As ToolStripMenuItem
    Friend WithEvents mnuClearAll As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents TrainingMove As DataGridViewTextBoxColumn
    Friend WithEvents Points As DataGridViewTextBoxColumn
    Friend WithEvents Feedback As DataGridViewTextBoxColumn
End Class
