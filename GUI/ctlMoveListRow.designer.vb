<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlMoveListRow
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlMoveListRow))
        Me.cmdExpand = New System.Windows.Forms.Button()
        Me.cmdCollapse = New System.Windows.Forms.Button()
        Me.rtbWhiteMoveText = New System.Windows.Forms.RichTextBox()
        Me.rtbCommentAfter = New System.Windows.Forms.RichTextBox()
        Me.rtbCommentBefore = New System.Windows.Forms.RichTextBox()
        Me.rtbBlackMoveText = New System.Windows.Forms.RichTextBox()
        Me.rtbMoveNumber = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'cmdExpand
        '
        Me.cmdExpand.BackgroundImage = CType(resources.GetObject("cmdExpand.BackgroundImage"), System.Drawing.Image)
        Me.cmdExpand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExpand.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExpand.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdExpand.Location = New System.Drawing.Point(0, 0)
        Me.cmdExpand.Name = "cmdExpand"
        Me.cmdExpand.Size = New System.Drawing.Size(16, 16)
        Me.cmdExpand.TabIndex = 0
        Me.cmdExpand.TabStop = False
        Me.cmdExpand.UseVisualStyleBackColor = True
        '
        'cmdCollapse
        '
        Me.cmdCollapse.BackgroundImage = CType(resources.GetObject("cmdCollapse.BackgroundImage"), System.Drawing.Image)
        Me.cmdCollapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCollapse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCollapse.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdCollapse.Location = New System.Drawing.Point(0, 0)
        Me.cmdCollapse.Name = "cmdCollapse"
        Me.cmdCollapse.Size = New System.Drawing.Size(16, 16)
        Me.cmdCollapse.TabIndex = 4
        Me.cmdCollapse.TabStop = False
        Me.cmdCollapse.UseVisualStyleBackColor = True
        Me.cmdCollapse.Visible = False
        '
        'rtbWhiteMoveText
        '
        Me.rtbWhiteMoveText.BackColor = System.Drawing.SystemColors.Info
        Me.rtbWhiteMoveText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbWhiteMoveText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbWhiteMoveText.Location = New System.Drawing.Point(60, 22)
        Me.rtbWhiteMoveText.Name = "rtbWhiteMoveText"
        Me.rtbWhiteMoveText.ReadOnly = True
        Me.rtbWhiteMoveText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbWhiteMoveText.Size = New System.Drawing.Size(80, 20)
        Me.rtbWhiteMoveText.TabIndex = 5
        Me.rtbWhiteMoveText.Text = "MoveText"
        '
        'rtbCommentAfter
        '
        Me.rtbCommentAfter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbCommentAfter.BackColor = System.Drawing.SystemColors.Info
        Me.rtbCommentAfter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbCommentAfter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbCommentAfter.Location = New System.Drawing.Point(60, 42)
        Me.rtbCommentAfter.Name = "rtbCommentAfter"
        Me.rtbCommentAfter.ReadOnly = True
        Me.rtbCommentAfter.Size = New System.Drawing.Size(165, 20)
        Me.rtbCommentAfter.TabIndex = 6
        Me.rtbCommentAfter.Text = "CommentAfter"
        '
        'rtbCommentBefore
        '
        Me.rtbCommentBefore.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbCommentBefore.BackColor = System.Drawing.SystemColors.Info
        Me.rtbCommentBefore.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbCommentBefore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbCommentBefore.Location = New System.Drawing.Point(60, 2)
        Me.rtbCommentBefore.Name = "rtbCommentBefore"
        Me.rtbCommentBefore.ReadOnly = True
        Me.rtbCommentBefore.Size = New System.Drawing.Size(165, 20)
        Me.rtbCommentBefore.TabIndex = 7
        Me.rtbCommentBefore.Text = "CommentBefore"
        '
        'rtbBlackMoveText
        '
        Me.rtbBlackMoveText.BackColor = System.Drawing.SystemColors.Info
        Me.rtbBlackMoveText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbBlackMoveText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbBlackMoveText.Location = New System.Drawing.Point(146, 22)
        Me.rtbBlackMoveText.Name = "rtbBlackMoveText"
        Me.rtbBlackMoveText.ReadOnly = True
        Me.rtbBlackMoveText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbBlackMoveText.Size = New System.Drawing.Size(80, 20)
        Me.rtbBlackMoveText.TabIndex = 8
        Me.rtbBlackMoveText.Text = "MoveText"
        '
        'rtbMoveNumber
        '
        Me.rtbMoveNumber.BackColor = System.Drawing.SystemColors.Info
        Me.rtbMoveNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbMoveNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbMoveNumber.Location = New System.Drawing.Point(25, 21)
        Me.rtbMoveNumber.Name = "rtbMoveNumber"
        Me.rtbMoveNumber.ReadOnly = True
        Me.rtbMoveNumber.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbMoveNumber.Size = New System.Drawing.Size(35, 20)
        Me.rtbMoveNumber.TabIndex = 9
        Me.rtbMoveNumber.Text = "Nr."
        '
        'ctlMoveListRow
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.Controls.Add(Me.rtbBlackMoveText)
        Me.Controls.Add(Me.rtbWhiteMoveText)
        Me.Controls.Add(Me.rtbMoveNumber)
        Me.Controls.Add(Me.rtbCommentBefore)
        Me.Controls.Add(Me.rtbCommentAfter)
        Me.Controls.Add(Me.cmdExpand)
        Me.Controls.Add(Me.cmdCollapse)
        Me.Name = "ctlMoveListRow"
        Me.Size = New System.Drawing.Size(225, 62)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdExpand As Button
    Friend WithEvents cmdCollapse As Button
    Friend WithEvents rtbWhiteMoveText As RichTextBox
    Friend WithEvents rtbCommentAfter As RichTextBox
    Friend WithEvents rtbCommentBefore As RichTextBox
    Friend WithEvents rtbBlackMoveText As RichTextBox
    Friend WithEvents rtbMoveNumber As RichTextBox
End Class
