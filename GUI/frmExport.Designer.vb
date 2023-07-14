<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExport))
        Me.cmdSavePDF = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.txtPageHeader = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.cmbZoom = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.lblPageNr = New System.Windows.Forms.Label()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ppvPrintPreview = New System.Windows.Forms.PrintPreviewControl()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkSideLabels = New System.Windows.Forms.CheckBox()
        Me.cmdUpdatePreview = New System.Windows.Forms.Button()
        Me.colLayout = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDiagrams = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDiagramSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBottomText = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstLayout = New System.Windows.Forms.ListView()
        Me.cmbFontSize = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSavePDF
        '
        resources.ApplyResources(Me.cmdSavePDF, "cmdSavePDF")
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
        'txtPageHeader
        '
        Me.txtPageHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPageHeader, "txtPageHeader")
        Me.txtPageHeader.Name = "txtPageHeader"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Panel
        '
        resources.ApplyResources(Me.Panel, "Panel")
        Me.Panel.BackColor = System.Drawing.SystemColors.Window
        Me.Panel.Controls.Add(Me.cmbZoom)
        Me.Panel.Controls.Add(Me.Label1)
        Me.Panel.Controls.Add(Me.cmdPrevious)
        Me.Panel.Controls.Add(Me.lblPageNr)
        Me.Panel.Controls.Add(Me.cmdNext)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.ppvPrintPreview)
        Me.Panel.Name = "Panel"
        '
        'cmbZoom
        '
        resources.ApplyResources(Me.cmbZoom, "cmbZoom")
        Me.cmbZoom.FormattingEnabled = True
        Me.cmbZoom.Items.AddRange(New Object() {resources.GetString("cmbZoom.Items"), resources.GetString("cmbZoom.Items1"), resources.GetString("cmbZoom.Items2"), resources.GetString("cmbZoom.Items3"), resources.GetString("cmbZoom.Items4")})
        Me.cmbZoom.Name = "cmbZoom"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cmdPrevious
        '
        resources.ApplyResources(Me.cmdPrevious, "cmdPrevious")
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.UseVisualStyleBackColor = True
        '
        'lblPageNr
        '
        resources.ApplyResources(Me.lblPageNr, "lblPageNr")
        Me.lblPageNr.Name = "lblPageNr"
        '
        'cmdNext
        '
        resources.ApplyResources(Me.cmdNext, "cmdNext")
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'ppvPrintPreview
        '
        resources.ApplyResources(Me.ppvPrintPreview, "ppvPrintPreview")
        Me.ppvPrintPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.ppvPrintPreview.Name = "ppvPrintPreview"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'chkSideLabels
        '
        resources.ApplyResources(Me.chkSideLabels, "chkSideLabels")
        Me.chkSideLabels.Checked = True
        Me.chkSideLabels.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSideLabels.Name = "chkSideLabels"
        Me.chkSideLabels.TabStop = False
        Me.chkSideLabels.UseVisualStyleBackColor = True
        '
        'cmdUpdatePreview
        '
        resources.ApplyResources(Me.cmdUpdatePreview, "cmdUpdatePreview")
        Me.cmdUpdatePreview.Name = "cmdUpdatePreview"
        Me.cmdUpdatePreview.UseVisualStyleBackColor = True
        '
        'colLayout
        '
        resources.ApplyResources(Me.colLayout, "colLayout")
        '
        'colDiagrams
        '
        resources.ApplyResources(Me.colDiagrams, "colDiagrams")
        '
        'colDiagramSize
        '
        resources.ApplyResources(Me.colDiagramSize, "colDiagramSize")
        '
        'colBottomText
        '
        resources.ApplyResources(Me.colBottomText, "colBottomText")
        '
        'lstLayout
        '
        Me.lstLayout.BackColor = System.Drawing.SystemColors.Window
        Me.lstLayout.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colLayout, Me.colDiagrams, Me.colDiagramSize, Me.colBottomText})
        resources.ApplyResources(Me.lstLayout, "lstLayout")
        Me.lstLayout.FullRowSelect = True
        Me.lstLayout.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstLayout.HideSelection = False
        Me.lstLayout.Items.AddRange(New System.Windows.Forms.ListViewItem() {CType(resources.GetObject("lstLayout.Items"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("lstLayout.Items1"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("lstLayout.Items2"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("lstLayout.Items3"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("lstLayout.Items4"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("lstLayout.Items5"), System.Windows.Forms.ListViewItem)})
        Me.lstLayout.MultiSelect = False
        Me.lstLayout.Name = "lstLayout"
        Me.lstLayout.Scrollable = False
        Me.lstLayout.UseCompatibleStateImageBehavior = False
        Me.lstLayout.View = System.Windows.Forms.View.Details
        '
        'cmbFontSize
        '
        resources.ApplyResources(Me.cmbFontSize, "cmbFontSize")
        Me.cmbFontSize.FormatString = "N0"
        Me.cmbFontSize.FormattingEnabled = True
        Me.cmbFontSize.Items.AddRange(New Object() {resources.GetString("cmbFontSize.Items"), resources.GetString("cmbFontSize.Items1"), resources.GetString("cmbFontSize.Items2"), resources.GetString("cmbFontSize.Items3"), resources.GetString("cmbFontSize.Items4")})
        Me.cmbFontSize.Name = "cmbFontSize"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'frmExport
        '
        Me.AcceptButton = Me.cmdUpdatePreview
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbFontSize)
        Me.Controls.Add(Me.lstLayout)
        Me.Controls.Add(Me.chkSideLabels)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.cmdUpdatePreview)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPageHeader)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSavePDF)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExport"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.Panel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdSavePDF As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents dlgSaveFile As SaveFileDialog
    Friend WithEvents txtPageHeader As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdPrevious As Button
    Friend WithEvents lblPageNr As Label
    Friend WithEvents cmdNext As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ppvPrintPreview As PrintPreviewControl
    Friend WithEvents Label3 As Label
    Friend WithEvents chkSideLabels As CheckBox
    Friend WithEvents cmbZoom As ComboBox
    Friend WithEvents cmdUpdatePreview As Button
    Friend WithEvents colLayout As ColumnHeader
    Friend WithEvents colDiagrams As ColumnHeader
    Friend WithEvents colDiagramSize As ColumnHeader
    Friend WithEvents colBottomText As ColumnHeader
    Friend WithEvents lstLayout As ListView
    Friend WithEvents cmbFontSize As ComboBox
    Friend WithEvents Label4 As Label
End Class
