<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStockfish
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
        Me.chkOnOff = New System.Windows.Forms.CheckBox()
        Me.pnlPanel = New System.Windows.Forms.Panel()
        Me.lstVariants = New System.Windows.Forms.ListBox()
        Me.pnlPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkOnOff
        '
        Me.chkOnOff.AutoSize = True
        Me.chkOnOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnOff.ForeColor = System.Drawing.Color.Black
        Me.chkOnOff.Location = New System.Drawing.Point(8, 4)
        Me.chkOnOff.Margin = New System.Windows.Forms.Padding(0)
        Me.chkOnOff.Name = "chkOnOff"
        Me.chkOnOff.Size = New System.Drawing.Size(228, 24)
        Me.chkOnOff.TabIndex = 0
        Me.chkOnOff.Text = "Stockfisch Engine v12 32bit "
        Me.chkOnOff.UseVisualStyleBackColor = True
        '
        'pnlPanel
        '
        Me.pnlPanel.Controls.Add(Me.lstVariants)
        Me.pnlPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPanel.Location = New System.Drawing.Point(0, 0)
        Me.pnlPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlPanel.Name = "pnlPanel"
        Me.pnlPanel.Padding = New System.Windows.Forms.Padding(4, 24, 4, 0)
        Me.pnlPanel.Size = New System.Drawing.Size(428, 99)
        Me.pnlPanel.TabIndex = 1
        '
        'lstVariants
        '
        Me.lstVariants.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstVariants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstVariants.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstVariants.ForeColor = System.Drawing.Color.Black
        Me.lstVariants.FormattingEnabled = True
        Me.lstVariants.ItemHeight = 18
        Me.lstVariants.Items.AddRange(New Object() {" ", " ", " "})
        Me.lstVariants.Location = New System.Drawing.Point(4, 24)
        Me.lstVariants.Margin = New System.Windows.Forms.Padding(0)
        Me.lstVariants.Name = "lstVariants"
        Me.lstVariants.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lstVariants.Size = New System.Drawing.Size(420, 75)
        Me.lstVariants.TabIndex = 7
        '
        'frmStockfish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(428, 99)
        Me.Controls.Add(Me.chkOnOff)
        Me.Controls.Add(Me.pnlPanel)
        Me.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Name = "frmStockfish"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Stockfish"
        Me.pnlPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkOnOff As CheckBox
    Friend WithEvents pnlPanel As Panel
    Friend WithEvents lstVariants As ListBox
End Class
