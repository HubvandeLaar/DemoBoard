<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmValidMoves
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmValidMoves))
        Me.lstValidMoves = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lstValidMoves
        '
        resources.ApplyResources(Me.lstValidMoves, "lstValidMoves")
        Me.lstValidMoves.BackColor = System.Drawing.Color.Lavender
        Me.lstValidMoves.FormattingEnabled = True
        Me.lstValidMoves.MultiColumn = True
        Me.lstValidMoves.Name = "lstValidMoves"
        '
        'frmValidMoves
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ControlBox = False
        Me.Controls.Add(Me.lstValidMoves)
        Me.Name = "frmValidMoves"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstValidMoves As ListBox
End Class
