<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlAnswerList
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
        Me.pnlAnswerList = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pnlAnswerList
        '
        Me.pnlAnswerList.AutoScroll = True
        Me.pnlAnswerList.BackColor = System.Drawing.SystemColors.Info
        Me.pnlAnswerList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAnswerList.Location = New System.Drawing.Point(0, 0)
        Me.pnlAnswerList.Name = "pnlAnswerList"
        Me.pnlAnswerList.Size = New System.Drawing.Size(150, 150)
        Me.pnlAnswerList.TabIndex = 1
        '
        'ctlAnswerList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlAnswerList)
        Me.Name = "ctlAnswerList"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlAnswerList As Panel
End Class
