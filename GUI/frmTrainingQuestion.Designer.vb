<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTrainingQuestion
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrainingQuestion))
        Me.cmdHint = New System.Windows.Forms.Button()
        Me.timSeconds = New System.Windows.Forms.Timer(Me.components)
        Me.lblTimeLeft = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTotalScore = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblText = New System.Windows.Forms.Label()
        Me.cmdSolution = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdRetry = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdDetails = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdHint
        '
        resources.ApplyResources(Me.cmdHint, "cmdHint")
        Me.cmdHint.Name = "cmdHint"
        Me.cmdHint.UseVisualStyleBackColor = True
        '
        'timSeconds
        '
        Me.timSeconds.Interval = 1000
        '
        'lblTimeLeft
        '
        resources.ApplyResources(Me.lblTimeLeft, "lblTimeLeft")
        Me.lblTimeLeft.BackColor = System.Drawing.Color.Black
        Me.lblTimeLeft.ForeColor = System.Drawing.Color.Gold
        Me.lblTimeLeft.Name = "lblTimeLeft"
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.lblTotalScore)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblScore)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'lblTotalScore
        '
        resources.ApplyResources(Me.lblTotalScore, "lblTotalScore")
        Me.lblTotalScore.Name = "lblTotalScore"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'lblScore
        '
        resources.ApplyResources(Me.lblScore, "lblScore")
        Me.lblScore.Name = "lblScore"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'lblText
        '
        resources.ApplyResources(Me.lblText, "lblText")
        Me.lblText.BackColor = System.Drawing.SystemColors.Info
        Me.lblText.Name = "lblText"
        '
        'cmdSolution
        '
        resources.ApplyResources(Me.cmdSolution, "cmdSolution")
        Me.cmdSolution.Name = "cmdSolution"
        Me.cmdSolution.UseVisualStyleBackColor = True
        '
        'cmdNext
        '
        resources.ApplyResources(Me.cmdNext, "cmdNext")
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'cmdRetry
        '
        resources.ApplyResources(Me.cmdRetry, "cmdRetry")
        Me.cmdRetry.Name = "cmdRetry"
        Me.cmdRetry.UseVisualStyleBackColor = True
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'cmdDetails
        '
        resources.ApplyResources(Me.cmdDetails, "cmdDetails")
        Me.cmdDetails.Name = "cmdDetails"
        Me.cmdDetails.UseVisualStyleBackColor = True
        '
        'frmTrainingQuestion
        '
        Me.AcceptButton = Me.cmdRetry
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdDetails)
        Me.Controls.Add(Me.cmdRetry)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdSolution)
        Me.Controls.Add(Me.lblText)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblTimeLeft)
        Me.Controls.Add(Me.cmdHint)
        Me.Controls.Add(Me.Label6)
        Me.Name = "frmTrainingQuestion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdHint As Button
    Friend WithEvents timSeconds As Timer
    Friend WithEvents lblTimeLeft As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblScore As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblTotalScore As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblText As Label
    Friend WithEvents cmdSolution As Button
    Friend WithEvents cmdNext As Button
    Friend WithEvents cmdRetry As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmdDetails As Button
End Class
