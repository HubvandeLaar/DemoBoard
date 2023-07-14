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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.tabTexts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabTexts.Controls.Add(Me.tabEn)
        Me.tabTexts.Controls.Add(Me.tabNl)
        Me.tabTexts.Location = New System.Drawing.Point(0, 0)
        Me.tabTexts.Name = "tabTexts"
        Me.tabTexts.SelectedIndex = 0
        Me.tabTexts.Size = New System.Drawing.Size(609, 357)
        Me.tabTexts.TabIndex = 0
        '
        'tabEn
        '
        Me.tabEn.Controls.Add(Me.grdAnswersEn)
        Me.tabEn.Controls.Add(Me.lblAnswersEn)
        Me.tabEn.Controls.Add(Me.rtbHint2En)
        Me.tabEn.Controls.Add(Me.rtbHint1En)
        Me.tabEn.Controls.Add(Me.rtbQuestionEn)
        Me.tabEn.Controls.Add(Me.lblHint2En)
        Me.tabEn.Controls.Add(Me.lblHint1En)
        Me.tabEn.Controls.Add(Me.lblQuestionEn)
        Me.tabEn.Location = New System.Drawing.Point(4, 22)
        Me.tabEn.Name = "tabEn"
        Me.tabEn.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEn.Size = New System.Drawing.Size(601, 331)
        Me.tabEn.TabIndex = 0
        Me.tabEn.Text = "English (default)"
        Me.tabEn.UseVisualStyleBackColor = True
        '
        'grdAnswersEn
        '
        Me.grdAnswersEn.AllowUserToResizeColumns = False
        Me.grdAnswersEn.AllowUserToResizeRows = False
        Me.grdAnswersEn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAnswersEn.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersEn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdAnswersEn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnswersEn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TrainingMove, Me.Points, Me.Feedback})
        Me.grdAnswersEn.Location = New System.Drawing.Point(73, 131)
        Me.grdAnswersEn.MultiSelect = False
        Me.grdAnswersEn.Name = "grdAnswersEn"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersEn.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdAnswersEn.RowHeadersWidth = 30
        Me.grdAnswersEn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdAnswersEn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdAnswersEn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAnswersEn.Size = New System.Drawing.Size(516, 155)
        Me.grdAnswersEn.TabIndex = 33
        '
        'TrainingMove
        '
        Me.TrainingMove.DividerWidth = 1
        Me.TrainingMove.HeaderText = "Move"
        Me.TrainingMove.MinimumWidth = 50
        Me.TrainingMove.Name = "TrainingMove"
        '
        'Points
        '
        Me.Points.DividerWidth = 1
        Me.Points.HeaderText = "Points"
        Me.Points.MinimumWidth = 50
        Me.Points.Name = "Points"
        '
        'Feedback
        '
        Me.Feedback.DividerWidth = 1
        Me.Feedback.HeaderText = "Feedback"
        Me.Feedback.MinimumWidth = 50
        Me.Feedback.Name = "Feedback"
        Me.Feedback.Width = 250
        '
        'lblAnswersEn
        '
        Me.lblAnswersEn.AutoSize = True
        Me.lblAnswersEn.Location = New System.Drawing.Point(7, 134)
        Me.lblAnswersEn.Name = "lblAnswersEn"
        Me.lblAnswersEn.Size = New System.Drawing.Size(47, 13)
        Me.lblAnswersEn.TabIndex = 32
        Me.lblAnswersEn.Text = "Answers"
        '
        'rtbHint2En
        '
        Me.rtbHint2En.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbHint2En.Location = New System.Drawing.Point(73, 92)
        Me.rtbHint2En.Name = "rtbHint2En"
        Me.rtbHint2En.Size = New System.Drawing.Size(517, 33)
        Me.rtbHint2En.TabIndex = 5
        Me.rtbHint2En.Text = ""
        '
        'rtbHint1En
        '
        Me.rtbHint1En.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbHint1En.Location = New System.Drawing.Point(73, 53)
        Me.rtbHint1En.Name = "rtbHint1En"
        Me.rtbHint1En.Size = New System.Drawing.Size(517, 33)
        Me.rtbHint1En.TabIndex = 4
        Me.rtbHint1En.Text = ""
        '
        'rtbQuestionEn
        '
        Me.rtbQuestionEn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbQuestionEn.Location = New System.Drawing.Point(73, 14)
        Me.rtbQuestionEn.Name = "rtbQuestionEn"
        Me.rtbQuestionEn.Size = New System.Drawing.Size(517, 33)
        Me.rtbQuestionEn.TabIndex = 3
        Me.rtbQuestionEn.Text = ""
        '
        'lblHint2En
        '
        Me.lblHint2En.AutoSize = True
        Me.lblHint2En.Location = New System.Drawing.Point(8, 95)
        Me.lblHint2En.Name = "lblHint2En"
        Me.lblHint2En.Size = New System.Drawing.Size(35, 13)
        Me.lblHint2En.TabIndex = 2
        Me.lblHint2En.Text = "Hint 2"
        '
        'lblHint1En
        '
        Me.lblHint1En.AutoSize = True
        Me.lblHint1En.Location = New System.Drawing.Point(8, 56)
        Me.lblHint1En.Name = "lblHint1En"
        Me.lblHint1En.Size = New System.Drawing.Size(35, 13)
        Me.lblHint1En.TabIndex = 1
        Me.lblHint1En.Text = "Hint 1"
        '
        'lblQuestionEn
        '
        Me.lblQuestionEn.AutoSize = True
        Me.lblQuestionEn.Location = New System.Drawing.Point(8, 17)
        Me.lblQuestionEn.Name = "lblQuestionEn"
        Me.lblQuestionEn.Size = New System.Drawing.Size(49, 13)
        Me.lblQuestionEn.TabIndex = 0
        Me.lblQuestionEn.Text = "Question"
        '
        'tabNl
        '
        Me.tabNl.Controls.Add(Me.grdAnswersNl)
        Me.tabNl.Controls.Add(Me.lblAnswersNl)
        Me.tabNl.Controls.Add(Me.rtbHint2Nl)
        Me.tabNl.Controls.Add(Me.rtbHint1Nl)
        Me.tabNl.Controls.Add(Me.rtbQuestionNl)
        Me.tabNl.Controls.Add(Me.lblHint2Nl)
        Me.tabNl.Controls.Add(Me.lblHint1Nl)
        Me.tabNl.Controls.Add(Me.lblQuestionNl)
        Me.tabNl.Location = New System.Drawing.Point(4, 22)
        Me.tabNl.Name = "tabNl"
        Me.tabNl.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNl.Size = New System.Drawing.Size(601, 331)
        Me.tabNl.TabIndex = 1
        Me.tabNl.Text = "Nederlands"
        Me.tabNl.UseVisualStyleBackColor = True
        '
        'grdAnswersNl
        '
        Me.grdAnswersNl.AllowUserToResizeColumns = False
        Me.grdAnswersNl.AllowUserToResizeRows = False
        Me.grdAnswersNl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAnswersNl.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersNl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.grdAnswersNl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnswersNl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.grdAnswersNl.Location = New System.Drawing.Point(73, 131)
        Me.grdAnswersNl.MultiSelect = False
        Me.grdAnswersNl.Name = "grdAnswersNl"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ScrollBar
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAnswersNl.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdAnswersNl.RowHeadersWidth = 30
        Me.grdAnswersNl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdAnswersNl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdAnswersNl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAnswersNl.Size = New System.Drawing.Size(516, 155)
        Me.grdAnswersNl.TabIndex = 33
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DividerWidth = 1
        Me.DataGridViewTextBoxColumn1.HeaderText = "Zet"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DividerWidth = 1
        Me.DataGridViewTextBoxColumn2.HeaderText = "Punten"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DividerWidth = 1
        Me.DataGridViewTextBoxColumn3.HeaderText = "Feedback"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 250
        '
        'lblAnswersNl
        '
        Me.lblAnswersNl.AutoSize = True
        Me.lblAnswersNl.Location = New System.Drawing.Point(7, 134)
        Me.lblAnswersNl.Name = "lblAnswersNl"
        Me.lblAnswersNl.Size = New System.Drawing.Size(64, 13)
        Me.lblAnswersNl.TabIndex = 32
        Me.lblAnswersNl.Text = "Antwoorden"
        '
        'rtbHint2Nl
        '
        Me.rtbHint2Nl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbHint2Nl.Location = New System.Drawing.Point(73, 92)
        Me.rtbHint2Nl.Name = "rtbHint2Nl"
        Me.rtbHint2Nl.Size = New System.Drawing.Size(517, 33)
        Me.rtbHint2Nl.TabIndex = 11
        Me.rtbHint2Nl.Text = ""
        '
        'rtbHint1Nl
        '
        Me.rtbHint1Nl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbHint1Nl.Location = New System.Drawing.Point(73, 53)
        Me.rtbHint1Nl.Name = "rtbHint1Nl"
        Me.rtbHint1Nl.Size = New System.Drawing.Size(517, 33)
        Me.rtbHint1Nl.TabIndex = 10
        Me.rtbHint1Nl.Text = ""
        '
        'rtbQuestionNl
        '
        Me.rtbQuestionNl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbQuestionNl.Location = New System.Drawing.Point(73, 14)
        Me.rtbQuestionNl.Name = "rtbQuestionNl"
        Me.rtbQuestionNl.Size = New System.Drawing.Size(517, 33)
        Me.rtbQuestionNl.TabIndex = 9
        Me.rtbQuestionNl.Text = ""
        '
        'lblHint2Nl
        '
        Me.lblHint2Nl.AutoSize = True
        Me.lblHint2Nl.Location = New System.Drawing.Point(8, 95)
        Me.lblHint2Nl.Name = "lblHint2Nl"
        Me.lblHint2Nl.Size = New System.Drawing.Size(35, 13)
        Me.lblHint2Nl.TabIndex = 8
        Me.lblHint2Nl.Text = "Hint 2"
        '
        'lblHint1Nl
        '
        Me.lblHint1Nl.AutoSize = True
        Me.lblHint1Nl.Location = New System.Drawing.Point(8, 56)
        Me.lblHint1Nl.Name = "lblHint1Nl"
        Me.lblHint1Nl.Size = New System.Drawing.Size(35, 13)
        Me.lblHint1Nl.TabIndex = 7
        Me.lblHint1Nl.Text = "Hint 1"
        '
        'lblQuestionNl
        '
        Me.lblQuestionNl.AutoSize = True
        Me.lblQuestionNl.Location = New System.Drawing.Point(8, 17)
        Me.lblQuestionNl.Name = "lblQuestionNl"
        Me.lblQuestionNl.Size = New System.Drawing.Size(35, 13)
        Me.lblQuestionNl.TabIndex = 6
        Me.lblQuestionNl.Text = "Vraag"
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Location = New System.Drawing.Point(303, 376)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 23
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.Location = New System.Drawing.Point(408, 376)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(75, 23)
        Me.cmdRemove.TabIndex = 24
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(513, 376)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 25
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'mnuPopUp
        '
        Me.mnuPopUp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteRow, Me.mnuClearAll})
        Me.mnuPopUp.Name = "ContextMenuStrip1"
        Me.mnuPopUp.Size = New System.Drawing.Size(158, 48)
        '
        'mnuDeleteRow
        '
        Me.mnuDeleteRow.Name = "mnuDeleteRow"
        Me.mnuDeleteRow.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.mnuDeleteRow.Size = New System.Drawing.Size(157, 22)
        Me.mnuDeleteRow.Text = "Delete Row"
        '
        'mnuClearAll
        '
        Me.mnuClearAll.Name = "mnuClearAll"
        Me.mnuClearAll.Size = New System.Drawing.Size(157, 22)
        Me.mnuClearAll.Text = "Clear All"
        '
        'frmEditTrainingQuestion
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(602, 414)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.tabTexts)
        Me.Name = "frmEditTrainingQuestion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Training Question"
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
    Friend WithEvents TrainingMove As DataGridViewTextBoxColumn
    Friend WithEvents Points As DataGridViewTextBoxColumn
    Friend WithEvents Feedback As DataGridViewTextBoxColumn
    Friend WithEvents lblAnswersEn As Label
    Friend WithEvents grdAnswersNl As DataGridView
    Friend WithEvents lblAnswersNl As Label
    Friend WithEvents mnuPopUp As ContextMenuStrip
    Friend WithEvents mnuDeleteRow As ToolStripMenuItem
    Friend WithEvents mnuClearAll As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
End Class
