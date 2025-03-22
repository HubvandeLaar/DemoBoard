<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlBoard
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlBoard))
        Me.picDragImage = New System.Windows.Forms.PictureBox()
        Me.picPlayMovePiece = New System.Windows.Forms.PictureBox()
        Me.timClock = New System.Windows.Forms.Timer(Me.components)
        Me.picArrow = New System.Windows.Forms.PictureBox()
        Me.picBoard = New System.Windows.Forms.PictureBox()
        Me.picOrange = New System.Windows.Forms.PictureBox()
        Me.picCyan = New System.Windows.Forms.PictureBox()
        Me.picBlue = New System.Windows.Forms.PictureBox()
        Me.picRed = New System.Windows.Forms.PictureBox()
        Me.picYellow = New System.Windows.Forms.PictureBox()
        Me.picGreen = New System.Windows.Forms.PictureBox()
        CType(Me.picDragImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPlayMovePiece, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCyan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBlue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picYellow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picDragImage
        '
        Me.picDragImage.BackColor = System.Drawing.Color.Transparent
        Me.picDragImage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picDragImage.ErrorImage = Nothing
        Me.picDragImage.InitialImage = Nothing
        Me.picDragImage.Location = New System.Drawing.Point(3, 402)
        Me.picDragImage.Name = "picDragImage"
        Me.picDragImage.Size = New System.Drawing.Size(50, 50)
        Me.picDragImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDragImage.TabIndex = 59
        Me.picDragImage.TabStop = False
        Me.picDragImage.Visible = False
        '
        'picPlayMovePiece
        '
        Me.picPlayMovePiece.BackColor = System.Drawing.Color.Transparent
        Me.picPlayMovePiece.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picPlayMovePiece.ErrorImage = Nothing
        Me.picPlayMovePiece.InitialImage = Nothing
        Me.picPlayMovePiece.Location = New System.Drawing.Point(17, 392)
        Me.picPlayMovePiece.Name = "picPlayMovePiece"
        Me.picPlayMovePiece.Size = New System.Drawing.Size(50, 50)
        Me.picPlayMovePiece.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPlayMovePiece.TabIndex = 63
        Me.picPlayMovePiece.TabStop = False
        Me.picPlayMovePiece.Visible = False
        '
        'timClock
        '
        Me.timClock.Interval = 20
        '
        'picArrow
        '
        Me.picArrow.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picArrow.Image = CType(resources.GetObject("picArrow.Image"), System.Drawing.Image)
        Me.picArrow.Location = New System.Drawing.Point(595, 307)
        Me.picArrow.Name = "picArrow"
        Me.picArrow.Size = New System.Drawing.Size(40, 40)
        Me.picArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picArrow.TabIndex = 60
        Me.picArrow.TabStop = False
        '
        'picBoard
        '
        Me.picBoard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBoard.ErrorImage = Nothing
        Me.picBoard.InitialImage = Nothing
        Me.picBoard.Location = New System.Drawing.Point(0, 0)
        Me.picBoard.Name = "picBoard"
        Me.picBoard.Size = New System.Drawing.Size(654, 456)
        Me.picBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBoard.TabIndex = 42
        Me.picBoard.TabStop = False
        '
        'picOrange
        '
        Me.picOrange.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picOrange.Image = CType(resources.GetObject("picOrange.Image"), System.Drawing.Image)
        Me.picOrange.Location = New System.Drawing.Point(595, 399)
        Me.picOrange.Name = "picOrange"
        Me.picOrange.Size = New System.Drawing.Size(40, 40)
        Me.picOrange.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picOrange.TabIndex = 64
        Me.picOrange.TabStop = False
        '
        'picCyan
        '
        Me.picCyan.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picCyan.Image = CType(resources.GetObject("picCyan.Image"), System.Drawing.Image)
        Me.picCyan.Location = New System.Drawing.Point(549, 399)
        Me.picCyan.Name = "picCyan"
        Me.picCyan.Size = New System.Drawing.Size(40, 40)
        Me.picCyan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picCyan.TabIndex = 65
        Me.picCyan.TabStop = False
        '
        'picBlue
        '
        Me.picBlue.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picBlue.Image = CType(resources.GetObject("picBlue.Image"), System.Drawing.Image)
        Me.picBlue.Location = New System.Drawing.Point(503, 399)
        Me.picBlue.Name = "picBlue"
        Me.picBlue.Size = New System.Drawing.Size(40, 40)
        Me.picBlue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBlue.TabIndex = 66
        Me.picBlue.TabStop = False
        '
        'picRed
        '
        Me.picRed.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picRed.Image = CType(resources.GetObject("picRed.Image"), System.Drawing.Image)
        Me.picRed.Location = New System.Drawing.Point(595, 353)
        Me.picRed.Name = "picRed"
        Me.picRed.Size = New System.Drawing.Size(40, 40)
        Me.picRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picRed.TabIndex = 67
        Me.picRed.TabStop = False
        '
        'picYellow
        '
        Me.picYellow.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picYellow.Image = CType(resources.GetObject("picYellow.Image"), System.Drawing.Image)
        Me.picYellow.Location = New System.Drawing.Point(549, 353)
        Me.picYellow.Name = "picYellow"
        Me.picYellow.Size = New System.Drawing.Size(40, 40)
        Me.picYellow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picYellow.TabIndex = 68
        Me.picYellow.TabStop = False
        '
        'picGreen
        '
        Me.picGreen.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.picGreen.Image = CType(resources.GetObject("picGreen.Image"), System.Drawing.Image)
        Me.picGreen.Location = New System.Drawing.Point(503, 353)
        Me.picGreen.Name = "picGreen"
        Me.picGreen.Size = New System.Drawing.Size(40, 40)
        Me.picGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picGreen.TabIndex = 69
        Me.picGreen.TabStop = False
        '
        'ctlBoard
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.picGreen)
        Me.Controls.Add(Me.picYellow)
        Me.Controls.Add(Me.picRed)
        Me.Controls.Add(Me.picBlue)
        Me.Controls.Add(Me.picCyan)
        Me.Controls.Add(Me.picOrange)
        Me.Controls.Add(Me.picDragImage)
        Me.Controls.Add(Me.picPlayMovePiece)
        Me.Controls.Add(Me.picArrow)
        Me.Controls.Add(Me.picBoard)
        Me.Name = "ctlBoard"
        Me.Size = New System.Drawing.Size(654, 456)
        CType(Me.picDragImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPlayMovePiece, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBoard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCyan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBlue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picYellow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picDragImage As PictureBox
    Friend WithEvents picPlayMovePiece As PictureBox
    Friend WithEvents timClock As Timer
    Friend WithEvents picArrow As PictureBox
    Friend WithEvents picBoard As PictureBox
    Friend WithEvents picOrange As PictureBox
    Friend WithEvents picCyan As PictureBox
    Friend WithEvents picBlue As PictureBox
    Friend WithEvents picRed As PictureBox
    Friend WithEvents picYellow As PictureBox
    Friend WithEvents picGreen As PictureBox
End Class
