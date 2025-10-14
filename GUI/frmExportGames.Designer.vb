<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportGames
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportGames))
        Me.lstGames = New System.Windows.Forms.ListView()
        Me.colNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWhite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBlack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colResult = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGameDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdSavePDF = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'lstGames
        '
        resources.ApplyResources(Me.lstGames, "lstGames")
        Me.lstGames.CheckBoxes = True
        Me.lstGames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNumber, Me.colWhite, Me.colBlack, Me.colResult, Me.colGameDate, Me.colTitle})
        Me.lstGames.GridLines = True
        Me.lstGames.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstGames.HideSelection = False
        Me.lstGames.MultiSelect = False
        Me.lstGames.Name = "lstGames"
        Me.lstGames.UseCompatibleStateImageBehavior = False
        Me.lstGames.View = System.Windows.Forms.View.Details
        '
        'colNumber
        '
        resources.ApplyResources(Me.colNumber, "colNumber")
        '
        'colWhite
        '
        resources.ApplyResources(Me.colWhite, "colWhite")
        '
        'colBlack
        '
        resources.ApplyResources(Me.colBlack, "colBlack")
        '
        'colResult
        '
        resources.ApplyResources(Me.colResult, "colResult")
        '
        'colGameDate
        '
        resources.ApplyResources(Me.colGameDate, "colGameDate")
        '
        'colTitle
        '
        resources.ApplyResources(Me.colTitle, "colTitle")
        '
        'cmdSavePDF
        '
        resources.ApplyResources(Me.cmdSavePDF, "cmdSavePDF")
        Me.cmdSavePDF.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdSavePDF.Name = "cmdSavePDF"
        Me.cmdSavePDF.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'dlgSaveFile
        '
        resources.ApplyResources(Me.dlgSaveFile, "dlgSaveFile")
        '
        'frmExportGames
        '
        Me.AcceptButton = Me.cmdSavePDF
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.cmdSavePDF)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lstGames)
        Me.KeyPreview = True
        Me.Name = "frmExportGames"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstGames As ListView
    Friend WithEvents colNumber As ColumnHeader
    Friend WithEvents colWhite As ColumnHeader
    Friend WithEvents colBlack As ColumnHeader
    Friend WithEvents colResult As ColumnHeader
    Friend WithEvents colGameDate As ColumnHeader
    Friend WithEvents colTitle As ColumnHeader
    Friend WithEvents cmdSavePDF As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents dlgSaveFile As SaveFileDialog
End Class
