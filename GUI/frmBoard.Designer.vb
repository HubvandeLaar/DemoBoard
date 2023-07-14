<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBoard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBoard))
        Me.mnuFieldMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctlBoard = New DemoBoard.ctlBoard()
        Me.SuspendLayout()
        '
        'mnuFieldMenu
        '
        resources.ApplyResources(Me.mnuFieldMenu, "mnuFieldMenu")
        Me.mnuFieldMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuFieldMenu.Name = "mnuFieldMenu"
        Me.mnuFieldMenu.ShowItemToolTips = False
        '
        'ctlBoard
        '
        resources.ApplyResources(Me.ctlBoard, "ctlBoard")
        Me.ctlBoard.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField
        Me.ctlBoard.AllowDrop = True
        Me.ctlBoard.ArrowString = ""
        Me.ctlBoard.BackColor = System.Drawing.SystemColors.Control
        Me.ctlBoard.ColorBoard = False
        Me.ctlBoard.FEN = "8/8/8/8/8/8/8/8 w KQkq - 0 0"
        Me.ctlBoard.MarkerString = ""
        Me.ctlBoard.Name = "ctlBoard"
        Me.ctlBoard.SetupToolbarVisible = True
        Me.ctlBoard.TextString = ""
        '
        'frmBoard
        '
        resources.ApplyResources(Me, "$this")
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ControlBox = False
        Me.Controls.Add(Me.ctlBoard)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmBoard"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ctlBoard As ctlBoard
    Friend WithEvents mnuFieldMenu As ContextMenuStrip
End Class
