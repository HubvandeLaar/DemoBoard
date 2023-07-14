<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlMoveList
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
        Me.pnlMoveList = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pnlMoveList
        '
        Me.pnlMoveList.AutoScroll = True
        Me.pnlMoveList.BackColor = System.Drawing.SystemColors.Info
        Me.pnlMoveList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMoveList.Location = New System.Drawing.Point(0, 0)
        Me.pnlMoveList.Name = "pnlMoveList"
        Me.pnlMoveList.Size = New System.Drawing.Size(150, 150)
        Me.pnlMoveList.TabIndex = 0
        '
        'ctlMoveList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.pnlMoveList)
        Me.Name = "ctlMoveList"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMoveList As Panel
End Class
