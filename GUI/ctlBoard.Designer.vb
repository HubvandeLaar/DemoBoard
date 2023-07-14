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
        Me.picGArrow = New System.Windows.Forms.PictureBox()
        Me.picYArrow = New System.Windows.Forms.PictureBox()
        Me.picRArrow = New System.Windows.Forms.PictureBox()
        Me.picBoard = New System.Windows.Forms.PictureBox()
        CType(Me.picDragImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPlayMovePiece, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picYArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBoard, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'picGArrow
        '
        Me.picGArrow.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picGArrow.Image = CType(resources.GetObject("picGArrow.Image"), System.Drawing.Image)
        Me.picGArrow.Location = New System.Drawing.Point(503, 412)
        Me.picGArrow.Name = "picGArrow"
        Me.picGArrow.Size = New System.Drawing.Size(40, 40)
        Me.picGArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picGArrow.TabIndex = 60
        Me.picGArrow.TabStop = False
        '
        'picYArrow
        '
        Me.picYArrow.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picYArrow.Image = CType(resources.GetObject("picYArrow.Image"), System.Drawing.Image)
        Me.picYArrow.Location = New System.Drawing.Point(549, 413)
        Me.picYArrow.Name = "picYArrow"
        Me.picYArrow.Size = New System.Drawing.Size(40, 40)
        Me.picYArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picYArrow.TabIndex = 61
        Me.picYArrow.TabStop = False
        '
        'picRArrow
        '
        Me.picRArrow.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.picRArrow.Image = CType(resources.GetObject("picRArrow.Image"), System.Drawing.Image)
        Me.picRArrow.Location = New System.Drawing.Point(595, 415)
        Me.picRArrow.Name = "picRArrow"
        Me.picRArrow.Size = New System.Drawing.Size(40, 40)
        Me.picRArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picRArrow.TabIndex = 62
        Me.picRArrow.TabStop = False
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
        'ctlBoard
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.picDragImage)
        Me.Controls.Add(Me.picPlayMovePiece)
        Me.Controls.Add(Me.picRArrow)
        Me.Controls.Add(Me.picYArrow)
        Me.Controls.Add(Me.picGArrow)
        Me.Controls.Add(Me.picBoard)
        Me.Name = "ctlBoard"
        Me.Size = New System.Drawing.Size(654, 456)
        CType(Me.picDragImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPlayMovePiece, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picYArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBoard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picDragImage As PictureBox
    Friend WithEvents picPlayMovePiece As PictureBox
    Friend WithEvents timClock As Timer
    Friend WithEvents picGArrow As PictureBox
    Friend WithEvents picYArrow As PictureBox
    Friend WithEvents picRArrow As PictureBox
    Friend WithEvents picBoard As PictureBox
End Class
