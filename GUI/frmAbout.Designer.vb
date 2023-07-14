<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAbout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.lblDisclaimer = New System.Windows.Forms.Label()
        Me.lblApplicationTitle = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.picSchakelaar = New System.Windows.Forms.PictureBox()
        Me.GroupBox.SuspendLayout()
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'lblDisclaimer
        '
        resources.ApplyResources(Me.lblDisclaimer, "lblDisclaimer")
        Me.lblDisclaimer.BackColor = System.Drawing.SystemColors.Info
        Me.lblDisclaimer.Name = "lblDisclaimer"
        '
        'lblApplicationTitle
        '
        resources.ApplyResources(Me.lblApplicationTitle, "lblApplicationTitle")
        Me.lblApplicationTitle.BackColor = System.Drawing.SystemColors.Info
        Me.lblApplicationTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblApplicationTitle.Name = "lblApplicationTitle"
        '
        'lblCopyright
        '
        resources.ApplyResources(Me.lblCopyright, "lblCopyright")
        Me.lblCopyright.BackColor = System.Drawing.SystemColors.Info
        Me.lblCopyright.Name = "lblCopyright"
        '
        'lblVersion
        '
        resources.ApplyResources(Me.lblVersion, "lblVersion")
        Me.lblVersion.BackColor = System.Drawing.SystemColors.Info
        Me.lblVersion.Name = "lblVersion"
        '
        'GroupBox
        '
        Me.GroupBox.BackColor = System.Drawing.SystemColors.Info
        Me.GroupBox.Controls.Add(Me.cmdOK)
        resources.ApplyResources(Me.GroupBox, "GroupBox")
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.TabStop = False
        '
        'picSchakelaar
        '
        Me.picSchakelaar.BackColor = System.Drawing.SystemColors.Info
        resources.ApplyResources(Me.picSchakelaar, "picSchakelaar")
        Me.picSchakelaar.Name = "picSchakelaar"
        Me.picSchakelaar.TabStop = False
        '
        'frmAbout
        '
        Me.AcceptButton = Me.cmdOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MidnightBlue
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblDisclaimer)
        Me.Controls.Add(Me.picSchakelaar)
        Me.Controls.Add(Me.lblApplicationTitle)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.GroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAbout"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.GroupBox.ResumeLayout(False)
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdOK As Button
    Friend WithEvents lblDisclaimer As Label
    Friend WithEvents lblApplicationTitle As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents GroupBox As GroupBox
    Friend WithEvents picSchakelaar As PictureBox
End Class
