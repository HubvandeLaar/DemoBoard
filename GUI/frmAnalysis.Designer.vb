<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnalysis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnalysis))
        Me.cmbLevelWhite = New System.Windows.Forms.ComboBox()
        Me.cmbLevelBlack = New System.Windows.Forms.ComboBox()
        Me.lblWhite = New System.Windows.Forms.Label()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.lblWhiteHeader = New System.Windows.Forms.Label()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblBlackHeader = New System.Windows.Forms.Label()
        Me.lblBlack = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmbLevelWhite
        '
        resources.ApplyResources(Me.cmbLevelWhite, "cmbLevelWhite")
        Me.cmbLevelWhite.FormattingEnabled = True
        Me.cmbLevelWhite.Items.AddRange(New Object() {resources.GetString("cmbLevelWhite.Items"), resources.GetString("cmbLevelWhite.Items1"), resources.GetString("cmbLevelWhite.Items2")})
        Me.cmbLevelWhite.Name = "cmbLevelWhite"
        '
        'cmbLevelBlack
        '
        resources.ApplyResources(Me.cmbLevelBlack, "cmbLevelBlack")
        Me.cmbLevelBlack.FormattingEnabled = True
        Me.cmbLevelBlack.Items.AddRange(New Object() {resources.GetString("cmbLevelBlack.Items"), resources.GetString("cmbLevelBlack.Items1"), resources.GetString("cmbLevelBlack.Items2")})
        Me.cmbLevelBlack.Name = "cmbLevelBlack"
        '
        'lblWhite
        '
        resources.ApplyResources(Me.lblWhite, "lblWhite")
        Me.lblWhite.Name = "lblWhite"
        '
        'lblLevel
        '
        resources.ApplyResources(Me.lblLevel, "lblLevel")
        Me.lblLevel.Name = "lblLevel"
        '
        'lblWhiteHeader
        '
        resources.ApplyResources(Me.lblWhiteHeader, "lblWhiteHeader")
        Me.lblWhiteHeader.Name = "lblWhiteHeader"
        '
        'cmdStart
        '
        resources.ApplyResources(Me.cmdStart, "cmdStart")
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblBlackHeader
        '
        resources.ApplyResources(Me.lblBlackHeader, "lblBlackHeader")
        Me.lblBlackHeader.Name = "lblBlackHeader"
        '
        'lblBlack
        '
        resources.ApplyResources(Me.lblBlack, "lblBlack")
        Me.lblBlack.Name = "lblBlack"
        '
        'frmAnalysis
        '
        Me.AcceptButton = Me.cmdStart
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.cmdCancel
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.lblBlack)
        Me.Controls.Add(Me.lblBlackHeader)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.lblWhiteHeader)
        Me.Controls.Add(Me.lblLevel)
        Me.Controls.Add(Me.lblWhite)
        Me.Controls.Add(Me.cmbLevelBlack)
        Me.Controls.Add(Me.cmbLevelWhite)
        Me.Name = "frmAnalysis"
        Me.ShowIcon = False
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbLevelWhite As ComboBox
    Friend WithEvents cmbLevelBlack As ComboBox
    Friend WithEvents lblWhite As Label
    Friend WithEvents lblLevel As Label
    Friend WithEvents lblWhiteHeader As Label
    Friend WithEvents cmdStart As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents lblBlackHeader As Label
    Friend WithEvents lblBlack As Label
End Class
