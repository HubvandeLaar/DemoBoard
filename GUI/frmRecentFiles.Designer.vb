<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecentFiles
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecentFiles))
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.lblListView = New System.Windows.Forms.Label()
        Me.lstRecentFiles = New System.Windows.Forms.ListView()
        Me.colFileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.picSchakelaar = New System.Windows.Forms.PictureBox()
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.Navy
        resources.ApplyResources(Me.cmdNew, "cmdNew")
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.Color.Navy
        Me.cmdOpen.CausesValidation = False
        resources.ApplyResources(Me.cmdOpen, "cmdOpen")
        Me.cmdOpen.ForeColor = System.Drawing.Color.White
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'lblListView
        '
        resources.ApplyResources(Me.lblListView, "lblListView")
        Me.lblListView.Name = "lblListView"
        '
        'lstRecentFiles
        '
        resources.ApplyResources(Me.lstRecentFiles, "lstRecentFiles")
        Me.lstRecentFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFileName, Me.colPath})
        Me.lstRecentFiles.FullRowSelect = True
        Me.lstRecentFiles.GridLines = True
        Me.lstRecentFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstRecentFiles.HideSelection = False
        Me.lstRecentFiles.MultiSelect = False
        Me.lstRecentFiles.Name = "lstRecentFiles"
        Me.lstRecentFiles.UseCompatibleStateImageBehavior = False
        Me.lstRecentFiles.View = System.Windows.Forms.View.Details
        '
        'colFileName
        '
        resources.ApplyResources(Me.colFileName, "colFileName")
        '
        'colPath
        '
        resources.ApplyResources(Me.colPath, "colPath")
        '
        'picSchakelaar
        '
        resources.ApplyResources(Me.picSchakelaar, "picSchakelaar")
        Me.picSchakelaar.BackColor = System.Drawing.Color.White
        Me.picSchakelaar.Name = "picSchakelaar"
        Me.picSchakelaar.TabStop = False
        '
        'frmRecentFiles
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picSchakelaar)
        Me.Controls.Add(Me.lstRecentFiles)
        Me.Controls.Add(Me.lblListView)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.cmdNew)
        Me.Name = "frmRecentFiles"
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdNew As Button
    Friend WithEvents cmdOpen As Button
    Friend WithEvents lblListView As Label
    Friend WithEvents lstRecentFiles As ListView
    Friend WithEvents colFileName As ColumnHeader
    Friend WithEvents colPath As ColumnHeader
    Friend WithEvents picSchakelaar As PictureBox
End Class
