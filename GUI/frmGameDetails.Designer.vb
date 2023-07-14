<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGameDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGameDetails))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblWhiteName = New System.Windows.Forms.Label()
        Me.lblBlackName = New System.Windows.Forms.Label()
        Me.lblSiteYear = New System.Windows.Forms.Label()
        Me.lblEvent = New System.Windows.Forms.Label()
        Me.lblWhiteELO = New System.Windows.Forms.Label()
        Me.lblBlackELO = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'lblWhiteName
        '
        resources.ApplyResources(Me.lblWhiteName, "lblWhiteName")
        Me.lblWhiteName.Name = "lblWhiteName"
        '
        'lblBlackName
        '
        resources.ApplyResources(Me.lblBlackName, "lblBlackName")
        Me.lblBlackName.Name = "lblBlackName"
        '
        'lblSiteYear
        '
        resources.ApplyResources(Me.lblSiteYear, "lblSiteYear")
        Me.lblSiteYear.Name = "lblSiteYear"
        '
        'lblEvent
        '
        resources.ApplyResources(Me.lblEvent, "lblEvent")
        Me.lblEvent.Name = "lblEvent"
        '
        'lblWhiteELO
        '
        resources.ApplyResources(Me.lblWhiteELO, "lblWhiteELO")
        Me.lblWhiteELO.Name = "lblWhiteELO"
        '
        'lblBlackELO
        '
        resources.ApplyResources(Me.lblBlackELO, "lblBlackELO")
        Me.lblBlackELO.Name = "lblBlackELO"
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'frmGameDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        resources.ApplyResources(Me, "$this")
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblBlackELO)
        Me.Controls.Add(Me.lblWhiteELO)
        Me.Controls.Add(Me.lblEvent)
        Me.Controls.Add(Me.lblSiteYear)
        Me.Controls.Add(Me.lblBlackName)
        Me.Controls.Add(Me.lblWhiteName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frmGameDetails"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblWhiteName As Label
    Friend WithEvents lblBlackName As Label
    Friend WithEvents lblSiteYear As Label
    Friend WithEvents lblEvent As Label
    Friend WithEvents lblWhiteELO As Label
    Friend WithEvents lblBlackELO As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
End Class
