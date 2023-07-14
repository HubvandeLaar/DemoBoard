<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPromotion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPromotion))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.picQueen = New System.Windows.Forms.PictureBox()
        Me.picRook = New System.Windows.Forms.PictureBox()
        Me.picBishop = New System.Windows.Forms.PictureBox()
        Me.picKnight = New System.Windows.Forms.PictureBox()
        CType(Me.picQueen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBishop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picKnight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        resources.ApplyResources(Me.lblHeader, "lblHeader")
        Me.lblHeader.Name = "lblHeader"
        '
        'picQueen
        '
        resources.ApplyResources(Me.picQueen, "picQueen")
        Me.picQueen.Name = "picQueen"
        Me.picQueen.TabStop = False
        '
        'picRook
        '
        resources.ApplyResources(Me.picRook, "picRook")
        Me.picRook.Name = "picRook"
        Me.picRook.TabStop = False
        '
        'picBishop
        '
        resources.ApplyResources(Me.picBishop, "picBishop")
        Me.picBishop.Name = "picBishop"
        Me.picBishop.TabStop = False
        '
        'picKnight
        '
        resources.ApplyResources(Me.picKnight, "picKnight")
        Me.picKnight.Name = "picKnight"
        Me.picKnight.TabStop = False
        '
        'frmPromotion
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picKnight)
        Me.Controls.Add(Me.picBishop)
        Me.Controls.Add(Me.picRook)
        Me.Controls.Add(Me.picQueen)
        Me.Controls.Add(Me.lblHeader)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPromotion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        CType(Me.picQueen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBishop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picKnight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents picQueen As System.Windows.Forms.PictureBox
    Friend WithEvents picRook As System.Windows.Forms.PictureBox
    Friend WithEvents picBishop As System.Windows.Forms.PictureBox
    Friend WithEvents picKnight As System.Windows.Forms.PictureBox
End Class
