<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectVariant
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectVariant))
        Me.lstVariants = New System.Windows.Forms.ListView()
        Me.colMoveText = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colComment = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstVariants
        '
        resources.ApplyResources(Me.lstVariants, "lstVariants")
        Me.lstVariants.BackColor = System.Drawing.SystemColors.Info
        Me.lstVariants.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colMoveText, Me.colComment, Me.colIndex})
        Me.lstVariants.FullRowSelect = True
        Me.lstVariants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstVariants.HideSelection = False
        Me.lstVariants.MultiSelect = False
        Me.lstVariants.Name = "lstVariants"
        Me.lstVariants.UseCompatibleStateImageBehavior = False
        Me.lstVariants.View = System.Windows.Forms.View.Details
        '
        'colMoveText
        '
        resources.ApplyResources(Me.colMoveText, "colMoveText")
        '
        'colComment
        '
        resources.ApplyResources(Me.colComment, "colComment")
        '
        'colIndex
        '
        resources.ApplyResources(Me.colIndex, "colIndex")
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'frmSelectVariant
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lstVariants)
        Me.KeyPreview = True
        Me.Name = "frmSelectVariant"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstVariants As System.Windows.Forms.ListView
    Friend WithEvents colMoveText As System.Windows.Forms.ColumnHeader
    Friend WithEvents colComment As System.Windows.Forms.ColumnHeader
    Friend WithEvents colIndex As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdOK As System.Windows.Forms.Button
End Class
