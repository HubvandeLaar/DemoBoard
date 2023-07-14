<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlAnswerListRow
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
        Me.rtbMoveText = New System.Windows.Forms.RichTextBox()
        Me.cmdReply = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbMoveText
        '
        Me.rtbMoveText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbMoveText.BackColor = System.Drawing.SystemColors.Info
        Me.rtbMoveText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbMoveText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbMoveText.Location = New System.Drawing.Point(30, 5)
        Me.rtbMoveText.Name = "rtbMoveText"
        Me.rtbMoveText.ReadOnly = True
        Me.rtbMoveText.Size = New System.Drawing.Size(195, 20)
        Me.rtbMoveText.TabIndex = 8
        Me.rtbMoveText.Text = "MoveText"
        '
        'cmdReply
        '
        Me.cmdReply.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdReply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdReply.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.cmdReply.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReply.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmdReply.Location = New System.Drawing.Point(0, 0)
        Me.cmdReply.Name = "cmdReply"
        Me.cmdReply.Size = New System.Drawing.Size(25, 30)
        Me.cmdReply.TabIndex = 11
        Me.cmdReply.TabStop = False
        Me.cmdReply.Text = "A"
        Me.cmdReply.UseVisualStyleBackColor = False
        '
        'ctlAnswerListRow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.Controls.Add(Me.cmdReply)
        Me.Controls.Add(Me.rtbMoveText)
        Me.Name = "ctlAnswerListRow"
        Me.Size = New System.Drawing.Size(225, 30)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbMoveText As RichTextBox
    Friend WithEvents cmdReply As Button
End Class
