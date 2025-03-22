<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainForm))
        Me.pnlDragPanel = New System.Windows.Forms.Panel()
        Me.pnlMainPanel = New System.Windows.Forms.Panel()
        Me.ctlTabControl = New DemoBoard.ctlTabControl()
        Me.stsStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblStatusText = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.mnuMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyGamePGN = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyGameXPGN = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPasteGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGameAnalysis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeleteGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditTitleAndMemo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreviousGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGameNumber = New System.Windows.Forms.ToolStripLabel()
        Me.mnuNextGame = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMode = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDiagram = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyDiagram = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPasteDiagram = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDiagramClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDiagramInitial = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSwitchSides = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDiagramSaveAsJPG = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGraphicals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStatusBar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBoard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetupToolbar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMoveList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGameDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuValidMoves = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTitleAndMemo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStockfish = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBoardStyle = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorBoard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBlackAndWhiteBoard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnglish = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNederlands = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLessonsFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMenuLocation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMenuTop = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMenuBottom = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadLayout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveLayout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpContents = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpIndex = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.dlgLessonsFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlMainPanel.SuspendLayout()
        Me.stsStatusStrip.SuspendLayout()
        Me.mnuMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDragPanel
        '
        Me.pnlDragPanel.BackColor = System.Drawing.Color.MidnightBlue
        Me.pnlDragPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.pnlDragPanel, "pnlDragPanel")
        Me.pnlDragPanel.Name = "pnlDragPanel"
        '
        'pnlMainPanel
        '
        resources.ApplyResources(Me.pnlMainPanel, "pnlMainPanel")
        Me.pnlMainPanel.BackColor = System.Drawing.Color.MidnightBlue
        Me.pnlMainPanel.Controls.Add(Me.ctlTabControl)
        Me.pnlMainPanel.Name = "pnlMainPanel"
        '
        'ctlTabControl
        '
        resources.ApplyResources(Me.ctlTabControl, "ctlTabControl")
        Me.ctlTabControl.BackColor = System.Drawing.Color.MidnightBlue
        Me.ctlTabControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctlTabControl.Name = "ctlTabControl"
        '
        'stsStatusStrip
        '
        resources.ApplyResources(Me.stsStatusStrip, "stsStatusStrip")
        Me.stsStatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.stsStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusText})
        Me.stsStatusStrip.Name = "stsStatusStrip"
        '
        'lblStatusText
        '
        Me.lblStatusText.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusText.Name = "lblStatusText"
        resources.ApplyResources(Me.lblStatusText, "lblStatusText")
        '
        'mnuMenuStrip
        '
        resources.ApplyResources(Me.mnuMenuStrip, "mnuMenuStrip")
        Me.mnuMenuStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.mnuMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUndo, Me.mnuRedo, Me.mnuFile, Me.mnuGame, Me.mnuPreviousGame, Me.mnuGameNumber, Me.mnuNextGame, Me.mnuMode, Me.mnuDiagram, Me.mnuGraphicals, Me.mnuView, Me.mnuSettings, Me.mnuHelp})
        Me.mnuMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.mnuMenuStrip.Name = "mnuMenuStrip"
        Me.mnuMenuStrip.ShowItemToolTips = True
        Me.mnuMenuStrip.Tag = ""
        '
        'mnuUndo
        '
        resources.ApplyResources(Me.mnuUndo, "mnuUndo")
        Me.mnuUndo.Name = "mnuUndo"
        '
        'mnuRedo
        '
        resources.ApplyResources(Me.mnuRedo, "mnuRedo")
        Me.mnuRedo.Name = "mnuRedo"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNew, Me.mnuOpen, Me.ToolStripSeparator3, Me.mnuSave, Me.ToolStripSeparator4, Me.ToolStripSeparator5, Me.mnuExport, Me.mnuExit})
        resources.ApplyResources(Me.mnuFile, "mnuFile")
        Me.mnuFile.Name = "mnuFile"
        '
        'mnuNew
        '
        resources.ApplyResources(Me.mnuNew, "mnuNew")
        Me.mnuNew.Name = "mnuNew"
        '
        'mnuOpen
        '
        resources.ApplyResources(Me.mnuOpen, "mnuOpen")
        Me.mnuOpen.Name = "mnuOpen"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'mnuSave
        '
        resources.ApplyResources(Me.mnuSave, "mnuSave")
        Me.mnuSave.Name = "mnuSave"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'mnuExport
        '
        resources.ApplyResources(Me.mnuExport, "mnuExport")
        Me.mnuExport.Name = "mnuExport"
        '
        'mnuExit
        '
        resources.ApplyResources(Me.mnuExit, "mnuExit")
        Me.mnuExit.Name = "mnuExit"
        '
        'mnuGame
        '
        Me.mnuGame.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopyGame, Me.mnuPasteGame, Me.mnuSelectGame, Me.mnuNewGame, Me.mnuEditGame, Me.mnuGameAnalysis, Me.mnuDeleteGame, Me.mnuEditTitleAndMemo})
        Me.mnuGame.Name = "mnuGame"
        resources.ApplyResources(Me.mnuGame, "mnuGame")
        '
        'mnuCopyGame
        '
        Me.mnuCopyGame.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopyGamePGN, Me.mnuCopyGameXPGN})
        resources.ApplyResources(Me.mnuCopyGame, "mnuCopyGame")
        Me.mnuCopyGame.Name = "mnuCopyGame"
        '
        'mnuCopyGamePGN
        '
        Me.mnuCopyGamePGN.Name = "mnuCopyGamePGN"
        resources.ApplyResources(Me.mnuCopyGamePGN, "mnuCopyGamePGN")
        '
        'mnuCopyGameXPGN
        '
        Me.mnuCopyGameXPGN.Name = "mnuCopyGameXPGN"
        resources.ApplyResources(Me.mnuCopyGameXPGN, "mnuCopyGameXPGN")
        '
        'mnuPasteGame
        '
        resources.ApplyResources(Me.mnuPasteGame, "mnuPasteGame")
        Me.mnuPasteGame.Name = "mnuPasteGame"
        '
        'mnuSelectGame
        '
        resources.ApplyResources(Me.mnuSelectGame, "mnuSelectGame")
        Me.mnuSelectGame.Name = "mnuSelectGame"
        '
        'mnuNewGame
        '
        resources.ApplyResources(Me.mnuNewGame, "mnuNewGame")
        Me.mnuNewGame.Name = "mnuNewGame"
        '
        'mnuEditGame
        '
        resources.ApplyResources(Me.mnuEditGame, "mnuEditGame")
        Me.mnuEditGame.Name = "mnuEditGame"
        '
        'mnuGameAnalysis
        '
        resources.ApplyResources(Me.mnuGameAnalysis, "mnuGameAnalysis")
        Me.mnuGameAnalysis.Name = "mnuGameAnalysis"
        '
        'mnuDeleteGame
        '
        resources.ApplyResources(Me.mnuDeleteGame, "mnuDeleteGame")
        Me.mnuDeleteGame.Name = "mnuDeleteGame"
        '
        'mnuEditTitleAndMemo
        '
        resources.ApplyResources(Me.mnuEditTitleAndMemo, "mnuEditTitleAndMemo")
        Me.mnuEditTitleAndMemo.Name = "mnuEditTitleAndMemo"
        '
        'mnuPreviousGame
        '
        resources.ApplyResources(Me.mnuPreviousGame, "mnuPreviousGame")
        Me.mnuPreviousGame.Name = "mnuPreviousGame"
        '
        'mnuGameNumber
        '
        Me.mnuGameNumber.Name = "mnuGameNumber"
        resources.ApplyResources(Me.mnuGameNumber, "mnuGameNumber")
        '
        'mnuNextGame
        '
        resources.ApplyResources(Me.mnuNextGame, "mnuNextGame")
        Me.mnuNextGame.Name = "mnuNextGame"
        '
        'mnuMode
        '
        Me.mnuMode.BackColor = System.Drawing.SystemColors.Menu
        resources.ApplyResources(Me.mnuMode, "mnuMode")
        Me.mnuMode.ForeColor = System.Drawing.Color.Navy
        Me.mnuMode.Margin = New System.Windows.Forms.Padding(2)
        Me.mnuMode.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.mnuMode.Name = "mnuMode"
        '
        'mnuDiagram
        '
        Me.mnuDiagram.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopyDiagram, Me.mnuPasteDiagram, Me.mnuDiagramClear, Me.mnuDiagramInitial, Me.mnuSwitchSides, Me.mnuDiagramSaveAsJPG})
        Me.mnuDiagram.Name = "mnuDiagram"
        resources.ApplyResources(Me.mnuDiagram, "mnuDiagram")
        '
        'mnuCopyDiagram
        '
        resources.ApplyResources(Me.mnuCopyDiagram, "mnuCopyDiagram")
        Me.mnuCopyDiagram.Name = "mnuCopyDiagram"
        '
        'mnuPasteDiagram
        '
        resources.ApplyResources(Me.mnuPasteDiagram, "mnuPasteDiagram")
        Me.mnuPasteDiagram.Name = "mnuPasteDiagram"
        '
        'mnuDiagramClear
        '
        resources.ApplyResources(Me.mnuDiagramClear, "mnuDiagramClear")
        Me.mnuDiagramClear.Name = "mnuDiagramClear"
        '
        'mnuDiagramInitial
        '
        resources.ApplyResources(Me.mnuDiagramInitial, "mnuDiagramInitial")
        Me.mnuDiagramInitial.Name = "mnuDiagramInitial"
        '
        'mnuSwitchSides
        '
        resources.ApplyResources(Me.mnuSwitchSides, "mnuSwitchSides")
        Me.mnuSwitchSides.Name = "mnuSwitchSides"
        '
        'mnuDiagramSaveAsJPG
        '
        resources.ApplyResources(Me.mnuDiagramSaveAsJPG, "mnuDiagramSaveAsJPG")
        Me.mnuDiagramSaveAsJPG.Name = "mnuDiagramSaveAsJPG"
        '
        'mnuGraphicals
        '
        Me.mnuGraphicals.Name = "mnuGraphicals"
        resources.ApplyResources(Me.mnuGraphicals, "mnuGraphicals")
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStatusBar, Me.mnuBoard, Me.mnuSetupToolbar, Me.mnuMoveList, Me.mnuGameDetails, Me.mnuValidMoves, Me.mnuTitleAndMemo, Me.mnuStockfish})
        Me.mnuView.Name = "mnuView"
        resources.ApplyResources(Me.mnuView, "mnuView")
        '
        'mnuStatusBar
        '
        Me.mnuStatusBar.Checked = True
        Me.mnuStatusBar.CheckOnClick = True
        Me.mnuStatusBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuStatusBar.Name = "mnuStatusBar"
        resources.ApplyResources(Me.mnuStatusBar, "mnuStatusBar")
        '
        'mnuBoard
        '
        Me.mnuBoard.CheckOnClick = True
        resources.ApplyResources(Me.mnuBoard, "mnuBoard")
        Me.mnuBoard.Name = "mnuBoard"
        '
        'mnuSetupToolbar
        '
        Me.mnuSetupToolbar.Checked = True
        Me.mnuSetupToolbar.CheckOnClick = True
        Me.mnuSetupToolbar.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuSetupToolbar, "mnuSetupToolbar")
        Me.mnuSetupToolbar.Name = "mnuSetupToolbar"
        '
        'mnuMoveList
        '
        Me.mnuMoveList.Checked = True
        Me.mnuMoveList.CheckOnClick = True
        Me.mnuMoveList.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuMoveList, "mnuMoveList")
        Me.mnuMoveList.Name = "mnuMoveList"
        '
        'mnuGameDetails
        '
        Me.mnuGameDetails.Checked = True
        Me.mnuGameDetails.CheckOnClick = True
        Me.mnuGameDetails.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuGameDetails.Name = "mnuGameDetails"
        resources.ApplyResources(Me.mnuGameDetails, "mnuGameDetails")
        '
        'mnuValidMoves
        '
        Me.mnuValidMoves.CheckOnClick = True
        resources.ApplyResources(Me.mnuValidMoves, "mnuValidMoves")
        Me.mnuValidMoves.Name = "mnuValidMoves"
        '
        'mnuTitleAndMemo
        '
        Me.mnuTitleAndMemo.Checked = True
        Me.mnuTitleAndMemo.CheckOnClick = True
        Me.mnuTitleAndMemo.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuTitleAndMemo, "mnuTitleAndMemo")
        Me.mnuTitleAndMemo.Name = "mnuTitleAndMemo"
        '
        'mnuStockfish
        '
        Me.mnuStockfish.CheckOnClick = True
        resources.ApplyResources(Me.mnuStockfish, "mnuStockfish")
        Me.mnuStockfish.Name = "mnuStockfish"
        '
        'mnuSettings
        '
        Me.mnuSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBoardStyle, Me.mnuLanguage, Me.mnuLessonsFolder, Me.mnuMenuLocation, Me.mnuLoadLayout, Me.mnuSaveLayout})
        Me.mnuSettings.Name = "mnuSettings"
        resources.ApplyResources(Me.mnuSettings, "mnuSettings")
        '
        'mnuBoardStyle
        '
        Me.mnuBoardStyle.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColorBoard, Me.mnuBlackAndWhiteBoard})
        resources.ApplyResources(Me.mnuBoardStyle, "mnuBoardStyle")
        Me.mnuBoardStyle.Name = "mnuBoardStyle"
        '
        'mnuColorBoard
        '
        Me.mnuColorBoard.CheckOnClick = True
        Me.mnuColorBoard.Name = "mnuColorBoard"
        resources.ApplyResources(Me.mnuColorBoard, "mnuColorBoard")
        '
        'mnuBlackAndWhiteBoard
        '
        Me.mnuBlackAndWhiteBoard.Checked = True
        Me.mnuBlackAndWhiteBoard.CheckOnClick = True
        Me.mnuBlackAndWhiteBoard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuBlackAndWhiteBoard.Name = "mnuBlackAndWhiteBoard"
        resources.ApplyResources(Me.mnuBlackAndWhiteBoard, "mnuBlackAndWhiteBoard")
        '
        'mnuLanguage
        '
        Me.mnuLanguage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnglish, Me.mnuNederlands})
        resources.ApplyResources(Me.mnuLanguage, "mnuLanguage")
        Me.mnuLanguage.Name = "mnuLanguage"
        '
        'mnuEnglish
        '
        Me.mnuEnglish.Checked = True
        Me.mnuEnglish.CheckOnClick = True
        Me.mnuEnglish.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuEnglish, "mnuEnglish")
        Me.mnuEnglish.Name = "mnuEnglish"
        '
        'mnuNederlands
        '
        Me.mnuNederlands.CheckOnClick = True
        resources.ApplyResources(Me.mnuNederlands, "mnuNederlands")
        Me.mnuNederlands.Name = "mnuNederlands"
        '
        'mnuLessonsFolder
        '
        resources.ApplyResources(Me.mnuLessonsFolder, "mnuLessonsFolder")
        Me.mnuLessonsFolder.Name = "mnuLessonsFolder"
        '
        'mnuMenuLocation
        '
        Me.mnuMenuLocation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMenuTop, Me.mnuMenuBottom})
        resources.ApplyResources(Me.mnuMenuLocation, "mnuMenuLocation")
        Me.mnuMenuLocation.Name = "mnuMenuLocation"
        '
        'mnuMenuTop
        '
        Me.mnuMenuTop.Checked = True
        Me.mnuMenuTop.CheckOnClick = True
        Me.mnuMenuTop.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.mnuMenuTop, "mnuMenuTop")
        Me.mnuMenuTop.Name = "mnuMenuTop"
        '
        'mnuMenuBottom
        '
        Me.mnuMenuBottom.CheckOnClick = True
        resources.ApplyResources(Me.mnuMenuBottom, "mnuMenuBottom")
        Me.mnuMenuBottom.Name = "mnuMenuBottom"
        '
        'mnuLoadLayout
        '
        resources.ApplyResources(Me.mnuLoadLayout, "mnuLoadLayout")
        Me.mnuLoadLayout.Name = "mnuLoadLayout"
        '
        'mnuSaveLayout
        '
        resources.ApplyResources(Me.mnuSaveLayout, "mnuSaveLayout")
        Me.mnuSaveLayout.Name = "mnuSaveLayout"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpContents, Me.mnuHelpIndex, Me.ToolStripSeparator8, Me.mnuAbout})
        Me.mnuHelp.Name = "mnuHelp"
        resources.ApplyResources(Me.mnuHelp, "mnuHelp")
        '
        'mnuHelpContents
        '
        resources.ApplyResources(Me.mnuHelpContents, "mnuHelpContents")
        Me.mnuHelpContents.Name = "mnuHelpContents"
        '
        'mnuHelpIndex
        '
        resources.ApplyResources(Me.mnuHelpIndex, "mnuHelpIndex")
        Me.mnuHelpIndex.Name = "mnuHelpIndex"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        resources.ApplyResources(Me.ToolStripSeparator8, "ToolStripSeparator8")
        '
        'mnuAbout
        '
        resources.ApplyResources(Me.mnuAbout, "mnuAbout")
        Me.mnuAbout.Name = "mnuAbout"
        '
        'dlgLessonsFolder
        '
        resources.ApplyResources(Me.dlgLessonsFolder, "dlgLessonsFolder")
        '
        'frmMainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.stsStatusStrip)
        Me.Controls.Add(Me.mnuMenuStrip)
        Me.Controls.Add(Me.pnlDragPanel)
        Me.Controls.Add(Me.pnlMainPanel)
        Me.HelpButton = True
        Me.KeyPreview = True
        Me.Name = "frmMainForm"
        Me.pnlMainPanel.ResumeLayout(False)
        Me.stsStatusStrip.ResumeLayout(False)
        Me.stsStatusStrip.PerformLayout()
        Me.mnuMenuStrip.ResumeLayout(False)
        Me.mnuMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlDragPanel As Panel
    Friend WithEvents pnlMainPanel As Panel
    Friend WithEvents ctlTabControl As ctlTabControl
    Friend WithEvents stsStatusStrip As StatusStrip
    Friend WithEvents lblStatusText As ToolStripStatusLabel
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents mnuFile As ToolStripMenuItem
    Friend WithEvents mnuNew As ToolStripMenuItem
    Friend WithEvents mnuOpen As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents mnuSave As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents mnuExit As ToolStripMenuItem
    Friend WithEvents mnuView As ToolStripMenuItem
    Friend WithEvents mnuStatusBar As ToolStripMenuItem
    Friend WithEvents mnuSettings As ToolStripMenuItem
    Friend WithEvents mnuHelp As ToolStripMenuItem
    Friend WithEvents mnuHelpContents As ToolStripMenuItem
    Friend WithEvents mnuHelpIndex As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents mnuAbout As ToolStripMenuItem
    Friend WithEvents mnuMenuStrip As MenuStrip
    Friend WithEvents mnuGame As ToolStripMenuItem
    Friend WithEvents mnuTitleAndMemo As ToolStripMenuItem
    Friend WithEvents mnuMoveList As ToolStripMenuItem
    Friend WithEvents mnuBoardStyle As ToolStripMenuItem
    Friend WithEvents mnuColorBoard As ToolStripMenuItem
    Friend WithEvents mnuBlackAndWhiteBoard As ToolStripMenuItem
    Friend WithEvents mnuLanguage As ToolStripMenuItem
    Friend WithEvents mnuEnglish As ToolStripMenuItem
    Friend WithEvents mnuNederlands As ToolStripMenuItem
    Friend WithEvents mnuGameNumber As ToolStripLabel
    Friend WithEvents mnuDiagram As ToolStripMenuItem
    Friend WithEvents mnuGraphicals As ToolStripMenuItem
    Friend WithEvents mnuLoadLayout As ToolStripMenuItem
    Friend WithEvents mnuSaveLayout As ToolStripMenuItem
    Friend WithEvents mnuSetupToolbar As ToolStripMenuItem
    Friend WithEvents mnuCopyDiagram As ToolStripMenuItem
    Friend WithEvents mnuPasteDiagram As ToolStripMenuItem
    Friend WithEvents mnuDiagramClear As ToolStripMenuItem
    Friend WithEvents mnuDiagramInitial As ToolStripMenuItem
    Friend WithEvents mnuDiagramSaveAsJPG As ToolStripMenuItem
    Friend WithEvents dlgSaveFile As SaveFileDialog
    Friend WithEvents mnuSelectGame As ToolStripMenuItem
    Friend WithEvents mnuEditGame As ToolStripMenuItem
    Friend WithEvents mnuDeleteGame As ToolStripMenuItem
    Friend WithEvents mnuBoard As ToolStripMenuItem
    Friend WithEvents mnuGameDetails As ToolStripMenuItem
    Friend WithEvents mnuValidMoves As ToolStripMenuItem
    Friend WithEvents mnuNewGame As ToolStripMenuItem
    Friend WithEvents mnuMode As ToolStripMenuItem
    Friend WithEvents mnuUndo As ToolStripMenuItem
    Friend WithEvents mnuRedo As ToolStripMenuItem
    Friend WithEvents mnuPreviousGame As ToolStripMenuItem
    Friend WithEvents mnuNextGame As ToolStripMenuItem
    Friend WithEvents mnuEditTitleAndMemo As ToolStripMenuItem
    Friend WithEvents mnuExport As ToolStripMenuItem
    Friend WithEvents mnuStockfish As ToolStripMenuItem
    Friend WithEvents mnuSwitchSides As ToolStripMenuItem
    Friend WithEvents mnuCopyGame As ToolStripMenuItem
    Friend WithEvents mnuPasteGame As ToolStripMenuItem
    Friend WithEvents mnuCopyGamePGN As ToolStripMenuItem
    Friend WithEvents mnuCopyGameXPGN As ToolStripMenuItem
    Friend WithEvents mnuLessonsFolder As ToolStripMenuItem
    Friend WithEvents dlgLessonsFolder As FolderBrowserDialog
    Friend WithEvents mnuMenuLocation As ToolStripMenuItem
    Friend WithEvents mnuMenuTop As ToolStripMenuItem
    Friend WithEvents mnuMenuBottom As ToolStripMenuItem
    Friend WithEvents mnuGameAnalysis As ToolStripMenuItem
End Class
