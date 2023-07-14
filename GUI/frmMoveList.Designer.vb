<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMoveList
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMoveList))
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdStart = New System.Windows.Forms.Label()
        Me.cmdPrevious = New System.Windows.Forms.Label()
        Me.cmdNext = New System.Windows.Forms.Label()
        Me.cmdLast = New System.Windows.Forms.Label()
        Me.mnuMoveMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditMove = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeleteMove = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPromoteVariant = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDemoteVariant = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctlMoveList = New DemoBoard.ctlMoveList()
        Me.mnuMoveMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "Empty.ico")
        Me.ImageList.Images.SetKeyName(1, "Next.ico")
        '
        'cmdStart
        '
        resources.ApplyResources(Me.cmdStart, "cmdStart")
        Me.cmdStart.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmdStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cmdStart.CausesValidation = False
        Me.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdStart.Name = "cmdStart"
        '
        'cmdPrevious
        '
        resources.ApplyResources(Me.cmdPrevious, "cmdPrevious")
        Me.cmdPrevious.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmdPrevious.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cmdPrevious.CausesValidation = False
        Me.cmdPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrevious.Name = "cmdPrevious"
        '
        'cmdNext
        '
        resources.ApplyResources(Me.cmdNext, "cmdNext")
        Me.cmdNext.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmdNext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cmdNext.CausesValidation = False
        Me.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNext.Name = "cmdNext"
        '
        'cmdLast
        '
        resources.ApplyResources(Me.cmdLast, "cmdLast")
        Me.cmdLast.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmdLast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cmdLast.CausesValidation = False
        Me.cmdLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLast.Name = "cmdLast"
        '
        'mnuMoveMenu
        '
        resources.ApplyResources(Me.mnuMoveMenu, "mnuMoveMenu")
        Me.mnuMoveMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuMoveMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditMove, Me.mnuDeleteMove, Me.mnuPromoteVariant, Me.mnuDemoteVariant})
        Me.mnuMoveMenu.Name = "mnuMoveMenu"
        '
        'mnuEditMove
        '
        resources.ApplyResources(Me.mnuEditMove, "mnuEditMove")
        Me.mnuEditMove.Name = "mnuEditMove"
        '
        'mnuDeleteMove
        '
        resources.ApplyResources(Me.mnuDeleteMove, "mnuDeleteMove")
        Me.mnuDeleteMove.Name = "mnuDeleteMove"
        '
        'mnuPromoteVariant
        '
        resources.ApplyResources(Me.mnuPromoteVariant, "mnuPromoteVariant")
        Me.mnuPromoteVariant.Name = "mnuPromoteVariant"
        '
        'mnuDemoteVariant
        '
        resources.ApplyResources(Me.mnuDemoteVariant, "mnuDemoteVariant")
        Me.mnuDemoteVariant.Name = "mnuDemoteVariant"
        '
        'ctlMoveList
        '
        resources.ApplyResources(Me.ctlMoveList, "ctlMoveList")
        Me.ctlMoveList.HideAfterSelectedHalfMove = False
        Me.ctlMoveList.Indent = 25
        Me.ctlMoveList.Name = "ctlMoveList"
        Me.ctlMoveList.SelectedHalfMove = Nothing
        '
        'frmMoveList
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ControlBox = False
        Me.Controls.Add(Me.ctlMoveList)
        Me.Controls.Add(Me.cmdLast)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Controls.Add(Me.cmdStart)
        Me.MaximizeBox = False
        Me.Name = "frmMoveList"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.mnuMoveMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdStart As System.Windows.Forms.Label
    Friend WithEvents cmdPrevious As System.Windows.Forms.Label
    Friend WithEvents cmdNext As System.Windows.Forms.Label
    Friend WithEvents cmdLast As System.Windows.Forms.Label
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents mnuMoveMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteMove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditMove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctlMoveList As ctlMoveList
    Friend WithEvents mnuPromoteVariant As ToolStripMenuItem
    Friend WithEvents mnuDemoteVariant As ToolStripMenuItem
End Class
