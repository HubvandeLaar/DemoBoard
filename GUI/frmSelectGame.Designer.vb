<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectGame
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectGame))
        Me.colBlack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.colWhite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colResult = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstGames = New System.Windows.Forms.ListView()
        Me.colGameDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdUp = New System.Windows.Forms.Button()
        Me.cmdDown = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'colBlack
        '
        resources.ApplyResources(Me.colBlack, "colBlack")
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'colWhite
        '
        resources.ApplyResources(Me.colWhite, "colWhite")
        '
        'colResult
        '
        resources.ApplyResources(Me.colResult, "colResult")
        '
        'colNumber
        '
        resources.ApplyResources(Me.colNumber, "colNumber")
        '
        'lstGames
        '
        resources.ApplyResources(Me.lstGames, "lstGames")
        Me.lstGames.AllowDrop = True
        Me.lstGames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNumber, Me.colWhite, Me.colBlack, Me.colResult, Me.colGameDate, Me.colTitle})
        Me.lstGames.FullRowSelect = True
        Me.lstGames.GridLines = True
        Me.lstGames.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstGames.HideSelection = False
        Me.lstGames.MultiSelect = False
        Me.lstGames.Name = "lstGames"
        Me.lstGames.UseCompatibleStateImageBehavior = False
        Me.lstGames.View = System.Windows.Forms.View.Details
        '
        'colGameDate
        '
        resources.ApplyResources(Me.colGameDate, "colGameDate")
        '
        'colTitle
        '
        resources.ApplyResources(Me.colTitle, "colTitle")
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdUp
        '
        resources.ApplyResources(Me.cmdUp, "cmdUp")
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        resources.ApplyResources(Me.cmdDown, "cmdDown")
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'frmSelectGame
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.cmdUp)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lstGames)
        Me.Controls.Add(Me.cmdCancel)
        Me.Name = "frmSelectGame"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents colBlack As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents colWhite As System.Windows.Forms.ColumnHeader
    Friend WithEvents colResult As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstGames As System.Windows.Forms.ListView
    Friend WithEvents colGameDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents colTitle As ColumnHeader
End Class
