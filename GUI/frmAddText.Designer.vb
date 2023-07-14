<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddText
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddText))
        Me.optGreenText = New System.Windows.Forms.RadioButton()
        Me.optYellowText = New System.Windows.Forms.RadioButton()
        Me.optRedText = New System.Windows.Forms.RadioButton()
        Me.picText = New System.Windows.Forms.PictureBox()
        Me.txtText = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        CType(Me.picText, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'optGreenText
        '
        resources.ApplyResources(Me.optGreenText, "optGreenText")
        Me.optGreenText.Checked = True
        Me.optGreenText.Name = "optGreenText"
        Me.optGreenText.TabStop = True
        Me.optGreenText.UseVisualStyleBackColor = True
        '
        'optYellowText
        '
        resources.ApplyResources(Me.optYellowText, "optYellowText")
        Me.optYellowText.Name = "optYellowText"
        Me.optYellowText.UseVisualStyleBackColor = True
        '
        'optRedText
        '
        resources.ApplyResources(Me.optRedText, "optRedText")
        Me.optRedText.Name = "optRedText"
        Me.optRedText.UseVisualStyleBackColor = True
        '
        'picText
        '
        resources.ApplyResources(Me.picText, "picText")
        Me.picText.Name = "picText"
        Me.picText.TabStop = False
        '
        'txtText
        '
        resources.ApplyResources(Me.txtText, "txtText")
        Me.txtText.Name = "txtText"
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frmAddText
        '
        Me.AcceptButton = Me.cmdOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.txtText)
        Me.Controls.Add(Me.picText)
        Me.Controls.Add(Me.optRedText)
        Me.Controls.Add(Me.optYellowText)
        Me.Controls.Add(Me.optGreenText)
        Me.Name = "frmAddText"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        CType(Me.picText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents optGreenText As System.Windows.Forms.RadioButton
    Friend WithEvents optYellowText As System.Windows.Forms.RadioButton
    Friend WithEvents optRedText As System.Windows.Forms.RadioButton
    Friend WithEvents picText As System.Windows.Forms.PictureBox
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class
