Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessMode
Imports ChessGlobals.ChessColor
Imports ChessGlobals.ChessLanguage
Imports ChessMaterials
Imports PGNLibrary
Imports CPSLibrary
Imports System.ComponentModel

Public Class frmMainForm
    Public WithEvents gfrmBoard As frmBoard
    Public WithEvents gfrmStockfish As frmStockfish
    Public WithEvents gfrmMoveList As frmMoveList
    Public WithEvents gfrmValidMoves As frmValidMoves
    Public WithEvents gfrmGameDetails As frmGameDetails
    Public WithEvents gfrmTitleAndMemo As frmTitleAndMemo
    Public WithEvents gfrmDockCross As frmDockCross

    Public WithEvents gfrmEditGame As frmEditGame
    Public WithEvents gfrmEditTitleAndMemo As frmEditTitleAndMemo
    Public WithEvents gfrmTrainingQuestion As frmTrainingQuestion = Nothing

    Public WithEvents gJournaling As New Journaling()

    Private gPGNFile As PGNFile
    Private WithEvents gPGNGame As PGNGame

    Public Event ModeChanged(pMode As ChessMode)
    Public Event LanguageChanged(pLanguage As ChessLanguage)
    Public Event GameChanged(pPGNGame As PGNGame)
    Public Event ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard)
    Public Event ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String, pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece)
    Public Event MoveListPositionChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove)
    Public Event HalfMoveChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove)
    Public Event ColorBoardChanged(pColorBoard As Boolean)
    Public Event ValidMovesSelectionChanged(pSender As Object, pMove As BoardMove)
    Public Event BoardKeyDown(pMsg As Message, pKeyData As Keys)
    Public Event FENChanged(pFEN As String)
    Public Event MovePlayed(pHalfMove As PGNHalfMove)

    Public Property Mode() As ChessMode
        Set(pMode As ChessMode)
            Select Case pMode
                Case SETUP
                    mnuMode.Text = MessageText("Setup")
                    RaiseEvent ModeChanged(pMode)
                    If gfrmTrainingQuestion IsNot Nothing Then
                        gfrmTrainingQuestion.Hide()
                        RaiseEvent GameChanged(Me.PGNGame)
                    End If
                Case PLAY
                    mnuMode.Text = MessageText("Play")
                    RaiseEvent ModeChanged(pMode)
                    If gfrmTrainingQuestion IsNot Nothing Then
                        gfrmTrainingQuestion.Hide()
                        RaiseEvent GameChanged(Me.PGNGame)
                    End If
                Case TRAINING
                    mnuMode.Text = MessageText("Training")
                    RaiseEvent ModeChanged(pMode)
                    Dim TrainingHalfMove As PGNHalfMove = gPGNGame.NextHalfMoveWithTrainingQuestion()
                    If TrainingHalfMove IsNot Nothing Then
                        Me.StartTraining(TrainingHalfMove)
                    End If
                Case Else
                    Throw New ArgumentOutOfRangeException("Invalid Mode !")
            End Select
        End Set
        Get
            Select Case mnuMode.Text
                Case MessageText("Setup")
                    Return SETUP
                Case MessageText("Play")
                    Return PLAY
                Case MessageText("Training")
                    Return TRAINING
                Case Else
                    Throw New ArgumentOutOfRangeException("Invalid Mode !")
            End Select
        End Get
    End Property

    Public Property PGNFile As PGNFile
        Set(pPGNFile As PGNFile)
            gPGNFile = pPGNFile
            If Me.PGNFile.PGNGames.Count > 0 Then
                mnuSelectGame.Enabled = True
                Me.PGNGame = Me.PGNFile.PGNGames(0)
            Else
                mnuSelectGame.Enabled = False
                Me.PGNGame = Me.PGNFile.PGNGames.Add()
            End If
        End Set
        Get
            Return gPGNFile
        End Get
    End Property

    Public Property PGNGame As PGNGame
        Set(pPGNGame As PGNGame)
            gPGNGame = pPGNGame
            mnuGameNumber.Text = Strings.Format(gPGNGame.Index + 1) & "/" & Strings.Format(Me.PGNFile.PGNGames.Count)
            mnuPreviousGame.Enabled = (gPGNGame.Index > 0)
            mnuNextGame.Enabled = (gPGNGame.Index < Me.PGNFile.PGNGames.Count - 1)

            Dim TrainingHalfMove As PGNHalfMove = pPGNGame.NextHalfMoveWithTrainingQuestion()
            If TrainingHalfMove IsNot Nothing Then
                If Me.Mode = TRAINING Then
                    Me.StartTraining(TrainingHalfMove)
                    Exit Property
                ElseIf MsgBox(MessageText("SwitchToTraining"), vbYesNo) = vbYes Then
                    Me.Mode = TRAINING
                    'Automatically triggers Me.StartTraining(TrainingHalfMove)
                    Exit Property
                End If
            End If

            RaiseEvent GameChanged(Me.PGNGame)
        End Set
        Get
            Return gPGNGame
        End Get
    End Property

    Public Property MenuLocation As DockStyle
        Set(pMenuLocation As DockStyle)
            If pMenuLocation = DockStyle.Top Then
                stsStatusStrip.Dock = mnuMenuStrip.Dock
                mnuMenuStrip.Dock = pMenuLocation
                frmMainForm_SizeChanged(Nothing, Nothing)
                Exit Property
            End If
            If pMenuLocation = DockStyle.Bottom Then
                stsStatusStrip.Dock = mnuMenuStrip.Dock
                mnuMenuStrip.Dock = pMenuLocation
                frmMainForm_SizeChanged(Nothing, Nothing)
                Exit Property
            End If
            Throw New Exception("Invalid Menu Location")
        End Set
        Get
            Return mnuMenuStrip.Dock
        End Get
    End Property


#Region "Menu Options"

    Private Sub mnuUndo_Click(pSender As Object, pArgs As EventArgs) Handles mnuUndo.Click
        gJournaling.Undo()
    End Sub

    Private Sub mnuRedo_Click(pSender As Object, pArgs As EventArgs) Handles mnuRedo.Click
        gJournaling.Redo()
    End Sub

    Private Sub mnuNew_Click(pSender As Object, pArgs As EventArgs) Handles mnuNew.Click
        If PGNFileModified() = True Then
            If MsgBox(MessageText("SaveChanges"), MessageBoxButtons.YesNo + MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                mnuSave_Click(Nothing, Nothing)
            End If
        End If

        Me.PGNFile = New PGNFile()

        gJournaling.Clear()
    End Sub

    Private Sub mnuOpen_Click(pSender As Object, pArgs As EventArgs) Handles mnuOpen.Click
        Dim Dialog As New OpenFileDialog
        Try
            If PGNFileModified() = True Then
                If MsgBox(MessageText("SaveChanges"), MessageBoxButtons.YesNo + MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                    mnuSave_Click(Nothing, Nothing)
                End If
            End If

            Dialog.AutoUpgradeEnabled = True
            Dialog.DefaultExt = "xpgn"
            Dialog.Filter = "Chess files (*.xpgn, *.pgn, *.cps)|*.xpgn; *.pgn; *.cps|Extended PGN files (*.xpgn)|*.xpgn|PGN files (*.pgn)|*.pgn|ChesserDemo files (*.cps)|*.cps|All files (*.*)|*.*"
            Dialog.FilterIndex = 1
            Dialog.InitialDirectory = CurrentLessonsFolder
            'Dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\DemoBoard Lessen\"
            Dialog.Multiselect = False
            Dialog.Title = MessageText("OpenPGNFile")
            Dialog.ShowDialog(Me)
            If Dialog.FileName <> "" Then
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If Dialog.FileName Like "*.cps" Then
                    Dim CPSFile As New CPSFile(Dialog.FileName)
                    Me.Text = CPSFile.FileName
                    Me.PGNFile = New PGNFile With {.PGNGames = CPSFile.ConvertToPGN()}
                Else
                    Me.PGNFile = New PGNFile(Dialog.FileName)
                    Me.Text = Me.PGNFile.FileName
                End If

                gJournaling.Clear()
                Cursor = Cursors.Default
            End If
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuSave_Click(pSender As Object, pArgs As EventArgs) Handles mnuSave.Click
        Try
            With dlgSaveFile
                .CheckFileExists = False
                .CheckPathExists = True
                .DefaultExt = ".xpgn"
                .InitialDirectory = CurrentLessonsFolder
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\DemoBoard Lessen\"
                .FileName = Me.PGNFile.FileName 'Default filename 
                .Filter = "Extended PGN file (*.xpgn)|*.xpgn|PGN file (*.pgn)|*.pgn"
                .ShowDialog()
                If .FileName = "" Then Exit Sub
            End With

            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.PGNFile.FullFileName = dlgSaveFile.FileName
            Me.Text = Me.PGNFile.FileName
            Me.PGNFile.SaveAs()
            Cursor = Cursors.Default
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuExport_Click(pSender As Object, pArgs As EventArgs) Handles mnuExport.Click
        Try
            Dim ExportForm As New frmExport
            ExportForm.ShowDialog(PGNFile)
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuExit_Click(pSender As Object, pArgs As EventArgs) Handles mnuExit.Click
        If PGNFileModified() = True Then
            If MsgBox(MessageText("SaveChanges"), MessageBoxButtons.YesNo + MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                mnuSave_Click(Nothing, Nothing)
            End If
        End If
        End
    End Sub

    Private Sub mnuGame_DropDownOpening(pSender As Object, pArgs As EventArgs) Handles mnuGame.DropDownOpening
        Try
            mnuSelectGame.Enabled = (Me.PGNFile.PGNGames.Count > 1)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuCopyGamePGN_Click(pSender As Object, pArgs As EventArgs) Handles mnuCopyGamePGN.Click
        Try
            Clipboard.SetData(DataFormats.Text, CType(Me.PGNGame.PGNString, Object))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuCopyGameXPGN_Click(pSender As Object, pArgs As EventArgs) Handles mnuCopyGameXPGN.Click
        Try
            Clipboard.SetData(DataFormats.Text, CType(Me.PGNGame.XPGNString, Object))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuPasteGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuPasteGame.Click
        Try
            Dim XPGN As String
            Dim BeforeImage As String = CStr(gPGNGame.Index)
            XPGN = Clipboard.GetData(DataFormats.Text)
            If XPGN Like "*[[]* [""]*[""][]]*" Then
                Dim Game = New PGNGame(XPGN)
                Me.PGNFile.PGNGames.Insert(PGNGame.Index + 1, Game)
                gJournaling.SaveImage("PasteGame", BeforeImage, XPGN)
                Me.PGNGame = Game
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuSelectGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuSelectGame.Click
        Dim BeforeImage As String, AfterImage As String
        Try
            If Me.PGNFile.PGNGames.Count < 2 Then Exit Sub
            BeforeImage = If(Me.PGNGame Is Nothing, "", CStr(Me.PGNGame.Index))

            frmSelectGame.ShowDialog(Me.PGNFile)
            If frmSelectGame.SelectedGame Is Nothing Then Exit Sub
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.PGNGame = frmSelectGame.SelectedGame

            AfterImage = CStr(Me.PGNGame.Index)
            gJournaling.SaveImage("PGNGame.Index", BeforeImage, AfterImage)
            Cursor = Cursors.Default
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuNewGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuNewGame.Click
        Try
            Dim BeforeImage As String = If(Me.PGNGame Is Nothing, "", CStr(Me.PGNGame.Index))

            Me.PGNGame = Me.PGNFile.PGNGames.Add()

            gJournaling.SaveImage("PGNGame.New", BeforeImage, "New")
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuEditGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuEditGame.Click
        If Me.PGNGame Is Nothing Then Exit Sub
        Dim BeforeImage As String = Journaling.Serialize(Me.PGNGame)
        gfrmEditGame = New frmEditGame()
        gfrmEditGame.ShowDialog(Me.PGNGame)
        If gfrmEditGame.OKPressed Then

            Dim AfterImage As String = Journaling.Serialize(Me.PGNGame)
            gJournaling.SaveImage("PGNGame", BeforeImage, AfterImage)

            RaiseEvent GameChanged(Me.PGNGame)
        End If
    End Sub

    Private Sub mnuDeleteGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteGame.Click
        Try
            Dim BeforeImage As String = Journaling.Serialize(PGNGame)

            Me.PGNGame = Me.PGNFile.PGNGames.Remove(Me.PGNGame)

            gJournaling.SaveImage("PGNGame", BeforeImage, "")
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuEditTitleAndMemo_Click(pSender As Object, pArgs As EventArgs) Handles mnuEditTitleAndMemo.Click
        If Me.PGNGame Is Nothing Then Exit Sub
        Dim BeforeImage As String = Journaling.Serialize(Me.PGNGame)
        gfrmEditTitleAndMemo = New frmEditTitleAndMemo()
        gfrmEditTitleAndMemo.ShowDialog(Me.PGNGame)
        If gfrmEditTitleAndMemo.OKPressed Then

            Dim AfterImage As String = Journaling.Serialize(Me.PGNGame)
            gJournaling.SaveImage("PGNGame", BeforeImage, AfterImage)

            RaiseEvent GameChanged(Me.PGNGame)
        End If
    End Sub

    Private Sub mnuPreviousGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuPreviousGame.Click
        Dim BeforeImage As String, AfterImage As String
        Try
            If Me.PGNGame Is Nothing Then Exit Sub
            If Me.PGNGame.Index < 1 Then Exit Sub
            BeforeImage = CStr(Me.PGNGame.Index)

            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.PGNGame = Me.PGNFile.PGNGames(Me.PGNGame.Index - 1)

            AfterImage = CStr(Me.PGNGame.Index)
            gJournaling.SaveImage("PGNGame.Index", BeforeImage, AfterImage)
            Cursor = Cursors.Default
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuNextGame_Click(pSender As Object, pArgs As EventArgs) Handles mnuNextGame.Click
        Dim BeforeImage As String, AfterImage As String
        Try
            If Me.PGNGame Is Nothing Then Exit Sub
            If Me.PGNGame.Index >= Me.PGNFile.PGNGames.Count - 1 Then Exit Sub
            BeforeImage = CStr(Me.PGNGame.Index)

            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.PGNGame = Me.PGNFile.PGNGames(Me.PGNGame.Index + 1)

            AfterImage = CStr(Me.PGNGame.Index)
            gJournaling.SaveImage("PGNGame.Index", BeforeImage, AfterImage)
            Cursor = Cursors.Default
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuMode_Click(pSender As Object, pArgs As EventArgs) Handles mnuMode.Click
        Dim BeforeImage As String = mnuMode.Text
        If Mode = SETUP Then
            Mode = PLAY
        ElseIf Mode = PLAY Then
            Mode = TRAINING
        Else
            Mode = SETUP
        End If
        gJournaling.SaveImage("Mode", BeforeImage, mnuMode.Text)
    End Sub

    Private Sub mnuCopyDiagram_Click(pSender As Object, pArgs As EventArgs) Handles mnuCopyDiagram.Click
        Try
            Clipboard.SetData(DataFormats.Text, CType(gfrmBoard.FEN, Object))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuPasteDiagram_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuPasteDiagram.Click
        Dim FEN As String
        Try
            Dim BeforeImage As String = CStr(gPGNGame.Index)
            FEN = Clipboard.GetData(DataFormats.Text)
            If FEN Like "*/*/*/*/*/*/*/* [bw] *" Then
                Dim Game As New PGNGame(FEN)
                Me.PGNFile.PGNGames.Insert(PGNGame.Index + 1, Game)
                gJournaling.SaveImage("PasteGame", BeforeImage, FEN)
                Me.PGNGame = Game
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDiagramClear_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuDiagramClear.Click
        Try
            Dim BeforeImage As String = Journaling.Serialize(Me.PGNGame)
            Me.PGNGame.Clear()

            gJournaling.SaveImage("ClearDiagram", BeforeImage, Me.PGNGame.Tags.GetPGNTag("FEN"))
            RaiseEvent GameChanged(Me.PGNGame)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDiagramInitial_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuDiagramInitial.Click
        Try
            Dim BeforeImage As String = Journaling.Serialize(Me.PGNGame)
            Me.PGNGame.Initial()

            gJournaling.SaveImage("InitialDiagram", BeforeImage, Me.PGNGame.Tags.GetPGNTag("FEN"))
            RaiseEvent GameChanged(Me.PGNGame)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuSwitchSides_Click(pSender As Object, pArgs As EventArgs) Handles mnuSwitchSides.Click
        gfrmBoard.SwitchSides = Not gfrmBoard.SwitchSides
    End Sub

    Private Sub mnuDiagramSaveAsJPG_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuDiagramSaveAsJPG.Click
        Try
            dlgSaveFile.CheckFileExists = False
            dlgSaveFile.CheckPathExists = True
            dlgSaveFile.DefaultExt = ".jpg"
            dlgSaveFile.Filter = "JPG Image (*.jpg)|*.jpg"
            dlgSaveFile.ShowDialog()
            If dlgSaveFile.FileName = "" Then Exit Sub
            gfrmBoard.SaveAsJPG(dlgSaveFile.FileName)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuGraphicals_DropDownOpening(pSender As Object, pArgs As EventArgs) Handles mnuGraphicals.DropDownOpening
        Dim MenuItem As ToolStripItem, Image As Image
        Try
            mnuGraphicals.DropDownItems.Clear()

            'Markers
            For Each Marker As Marker In New PGNMarkerList(gfrmBoard.MarkerString)
                Image = frmImages.getImage(Marker.IconName)
                MenuItem = mnuGraphicals.DropDownItems.Add(MessageText("DeleteMarker", Marker.FieldName), Image)
                MenuItem.Tag = Marker
            Next

            For Each Arrow As Arrow In New PGNArrowList(gfrmBoard.ArrowString)
                Image = frmImages.getImage(Arrow.IconName)
                MenuItem = mnuGraphicals.DropDownItems.Add(MessageText("DeleteArrow", Arrow.FromFieldName, Arrow.ToFieldName), Image)
                MenuItem.Tag = Arrow
            Next Arrow

            For Each Text As Text In New PGNTextList(gfrmBoard.TextString)
                Image = frmImages.getImage(Text.IconName)
                MenuItem = mnuGraphicals.DropDownItems.Add(MessageText("DeleteText", Text.Text, Text.FieldName), Image)
                MenuItem.Tag = Text
            Next Text

            If mnuGraphicals.DropDownItems.Count = 0 Then
                MenuItem = mnuGraphicals.DropDownItems.Add("               ")
                MenuItem.Enabled = False
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuGraphicals_DropDownItemClicked(pSender As Object, pArgs As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuGraphicals.DropDownItemClicked
        Try
            If TypeOf pArgs.ClickedItem.Tag Is Marker Then
                Dim BeforeImage As String = gfrmBoard.MarkerString
                Dim MarkerList = New PGNMarkerList(gfrmBoard.MarkerString)
                MarkerList.Remove(pArgs.ClickedItem.Tag)
                gfrmBoard.MarkerString = MarkerList.ListString
                'Store at PGNHalfMove
                Me.PGNGame.HalfMoves.MarkerListString(gfrmBoard.gCurrentHalfMove) = gfrmBoard.MarkerString
                gJournaling.SaveImage("FieldMarkerList", If(gfrmBoard.gCurrentHalfMove Is Nothing, "", CStr(gfrmBoard.gCurrentHalfMove.Index)), BeforeImage, gfrmBoard.MarkerString)

            ElseIf TypeOf pArgs.ClickedItem.Tag Is Arrow Then
                Dim BeforeImage As String = gfrmBoard.ArrowString
                Dim ArrowList = New PGNArrowList(gfrmBoard.ArrowString)
                ArrowList.Remove(pArgs.ClickedItem.Tag)
                gfrmBoard.ArrowString = ArrowList.ListString
                'Store at PGNHalfMove
                Me.PGNGame.HalfMoves.ArrowListString(gfrmBoard.gCurrentHalfMove) = gfrmBoard.ArrowString
                gJournaling.SaveImage("ArrowList", If(gfrmBoard.gCurrentHalfMove Is Nothing, "", CStr(gfrmBoard.gCurrentHalfMove.Index)), BeforeImage, gfrmBoard.ArrowString)

            ElseIf TypeOf pArgs.ClickedItem.Tag Is Text Then
                Dim BeforeImage As String = gfrmBoard.TextString
                Dim TextList = New PGNTextList(gfrmBoard.TextString)
                TextList.Remove(pArgs.ClickedItem.Tag)
                gfrmBoard.TextString = TextList.ListString
                'Store at PGNHalfMove
                Me.PGNGame.HalfMoves.TextListString(gfrmBoard.gCurrentHalfMove) = gfrmBoard.TextString
                gJournaling.SaveImage("TextList", If(gfrmBoard.gCurrentHalfMove Is Nothing, "", CStr(gfrmBoard.gCurrentHalfMove.Index)), BeforeImage, gfrmBoard.TextString)
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuView_DropDownOpening(pSender As Object, pArgs As EventArgs) Handles mnuView.DropDownOpening
        ' Stop
    End Sub

    Private Sub mnuStatusBar_Click(pSender As Object, pArgs As EventArgs) Handles mnuStatusBar.Click
        Try
            Dim BeforeImage As String = CStr(stsStatusStrip.Visible)

            stsStatusStrip.Visible = mnuStatusBar.Checked
            frmMainForm_SizeChanged(Nothing, Nothing)

            gJournaling.SaveImage("StatusBar.Visible", BeforeImage, CStr(stsStatusStrip.Visible))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuBoard_Click(pSender As Object, pArgs As EventArgs) Handles mnuBoard.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuBoard.Checked)
            Call UpdateBoardSubForm()
            gJournaling.SaveImage("mnuBoard.Checked", BeforeImage, CStr(mnuBoard.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuSetupToolbar_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuSetupToolbar.Click
        Try
            Dim BeforeImage As String = CStr(gfrmBoard.SetupToolbarVisible)
            gfrmBoard.SetupToolbarVisible = mnuSetupToolbar.Checked
            gJournaling.SaveImage("SetupToolbar.Visible", BeforeImage, CStr(gfrmBoard.SetupToolbarVisible))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuMoveList_Click(pSender As Object, pArgs As EventArgs) Handles mnuMoveList.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuMoveList.Checked)
            Call UpdateMoveListSubForm()
            gJournaling.SaveImage("mnuMoveList.Checked", BeforeImage, CStr(mnuMoveList.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuGameDetails_Click(pSender As Object, pArgs As EventArgs) Handles mnuGameDetails.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuGameDetails.Checked)
            Call UpdateGameDetailsSubForm()
            gJournaling.SaveImage("mnuGameDetails.Checked", BeforeImage, CStr(mnuGameDetails.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuValidMoves_Click(pSender As Object, pArgs As EventArgs) Handles mnuValidMoves.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuValidMoves.Checked)
            Call UpdateValidMovesSubForm()
            gJournaling.SaveImage("mnuValidMoves.Checked", BeforeImage, CStr(mnuValidMoves.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuTitleAndMemo_Click(pSender As Object, pArgs As EventArgs) Handles mnuTitleAndMemo.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuTitleAndMemo.Checked)
            Call UpdateTitleAndMemoSubForm()
            gJournaling.SaveImage("mnuTitleAndMemo.Checked", BeforeImage, CStr(mnuTitleAndMemo.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuStockfish_Click(pSender As Object, pArgs As EventArgs) Handles mnuStockfish.Click
        ' Try
        Dim BeforeImage As String = CStr(Not mnuStockfish.Checked)
        Call UpdateStockfishSubForm()
        gJournaling.SaveImage("mnuStockfish.Checked", BeforeImage, CStr(mnuStockfish.Checked))
        ' Catch pException As Exception
        'frmErrorMessageBox.Show(pException)
        'End Try
    End Sub

    Private Sub mnuColorBoard_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuColorBoard.Click
        Try
            Dim BeforeImage As String = CStr(Not mnuColorBoard.Checked)

            Call SetColorBoard(True)

            gJournaling.SaveImage("ColorBoard.Checked", BeforeImage, CStr(mnuColorBoard.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuBlackAndWhiteBoard_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuBlackAndWhiteBoard.Click
        Try
            Dim BeforeImage As String = CStr(mnuColorBoard.Checked)

            Call SetColorBoard(False)

            gJournaling.SaveImage("ColorBoard.Checked", BeforeImage, CStr(mnuColorBoard.Checked))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub SetColorBoard(pChecked As Boolean)
        mnuColorBoard.Checked = pChecked
        mnuBlackAndWhiteBoard.Checked = Not pChecked
        RaiseEvent ColorBoardChanged(mnuColorBoard.Checked)
    End Sub

    Private Sub mnuLanguage_DropDownOpening(pSender As Object, pArgs As System.EventArgs) Handles mnuLanguage.DropDownOpening
        Try
            If GetLanguage() = NEDERLANDS Then
                mnuNederlands.Checked = True
                mnuEnglish.Checked = False
            Else
                mnuNederlands.Checked = False
                mnuEnglish.Checked = True
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuEnglish_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuEnglish.Click
        Try
            Dim BeforeImage As String = CStr(CurrentLanguage)

            mnuNederlands.Checked = False
            Call SetLanguage(ENGLISH, Me)

            gJournaling.SaveImage("CurrentLanguage", BeforeImage, CStr(CurrentLanguage))
            RaiseEvent LanguageChanged(CurrentLanguage)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuNederlands_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuNederlands.Click
        Try
            Dim BeforeImage As String = CStr(CurrentLanguage)

            mnuEnglish.Checked = False
            Call SetLanguage(NEDERLANDS, Me)

            gJournaling.SaveImage("CurrentLanguage", BeforeImage, CStr(CurrentLanguage))
            RaiseEvent LanguageChanged(CurrentLanguage)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuMenuLocation_DropDownOpening(pSender As Object, pArgs As System.EventArgs) Handles mnuMenuLocation.DropDownOpening
        Try
            If Me.MenuLocation = DockStyle.Top Then
                mnuMenuTop.Checked = True
                mnuMenuBottom.Checked = False
            Else
                mnuMenuTop.Checked = False
                mnuMenuBottom.Checked = True
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuMenuTop_Click(pSender As Object, pArgs As EventArgs) Handles mnuMenuTop.Click
        Try
            Dim BeforeImage As String = CStr(Me.MenuLocation)

            mnuMenuBottom.Checked = False
            Me.MenuLocation = DockStyle.Top

            gJournaling.SaveImage("MenuLocation", BeforeImage, CStr(Me.MenuLocation))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuMenuBottom_Click(pSender As Object, pArgs As EventArgs) Handles mnuMenuBottom.Click
        Try
            Dim BeforeImage As String = CStr(Me.MenuLocation)

            mnuMenuTop.Checked = False
            Me.MenuLocation = DockStyle.Bottom

            gJournaling.SaveImage("MenuLocation", BeforeImage, CStr(Me.MenuLocation))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub


    Private Sub mnuLessonsFolder_Click(pSender As Object, pArgs As EventArgs) Handles mnuLessonsFolder.Click
        Try
            Dim OldFolder As String = CurrentLessonsFolder
            Dim NewFolder As String = ""
            With dlgLessonsFolder
                .RootFolder = Environment.SpecialFolder.Desktop
                .SelectedPath = OldFolder
                .ShowNewFolderButton = True
                If .ShowDialog() <> DialogResult.OK Then
                    Exit Sub
                End If
                NewFolder = .SelectedPath
                If IO.Directory.Exists(NewFolder) = False Then
                    IO.Directory.CreateDirectory(NewFolder)
                End If
                CurrentLessonsFolder = NewFolder

                'Copy Lessons to new directory
                CopyLessons(OldFolder, NewFolder)
            End With
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub CopyLessons(pSourceDirectory As String, pTargetDirectory As String)
        Dim LessonFiles As New IO.DirectoryInfo(pSourceDirectory)
        For Each LessonFile As IO.FileInfo In LessonFiles.GetFiles()
            If IO.File.Exists(pTargetDirectory & LessonFile.Name) = False Then
                LessonFile.CopyTo(pTargetDirectory & LessonFile.Name)
                Continue For
            End If
            If MsgBox(MessageText("AlreadyExists", LessonFile.Name, pTargetDirectory), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                LessonFile.CopyTo(pTargetDirectory & LessonFile.Name, overwrite:=True)
                Continue For
            End If
        Next LessonFile
    End Sub

    Private Sub mnuLoadLayout_DropDownOpening(pSender As Object, pArgs As EventArgs) Handles mnuLoadLayout.DropDownOpening
        Dim MenuItem As ToolStripItem
        Try
            mnuLoadLayout.DropDownItems.Clear()

            Dim Files As String() = IO.Directory.GetFiles(RootFolder() & "Settings")
            For Index As Integer = 0 To Files.Count - 1
                MenuItem = mnuLoadLayout.DropDownItems.Add("Load " & IO.Path.GetFileName(Files(Index)).Replace(".xml", ""))
                MenuItem.Tag = Files(Index)
            Next Index
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuLoadLayout_DropDownItemClicked(pSender As Object, pArgs As ToolStripItemClickedEventArgs) Handles mnuLoadLayout.DropDownItemClicked
        Try
            Dim BeforeImage As String = SerializeLayout()
            Me.DeSerializeLayout(pArgs.ClickedItem.Tag)
            gJournaling.SaveImage("Layout", BeforeImage, SerializeLayout())
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuSaveLayout_Click(pSender As Object, pArgs As EventArgs) Handles mnuSaveLayout.Click
        Try
            dlgSaveFile.CheckFileExists = False
            dlgSaveFile.CheckPathExists = True
            dlgSaveFile.InitialDirectory = RootFolder() & "Settings"
            dlgSaveFile.RestoreDirectory = False
            dlgSaveFile.DefaultExt = ".xml"
            dlgSaveFile.Filter = "Layout Settings (*.xml)|*.xml"
            dlgSaveFile.ShowDialog()
            If dlgSaveFile.FileName = "" Then Exit Sub
            SerializeLayout(dlgSaveFile.FileName)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuHelpContents_Click(pSender As Object, pArgs As EventArgs) Handles mnuHelpContents.Click
        Dim Position As Point = Me.PointToClient(MousePosition)
        Dim HelpFile As String = Application.StartupPath & If(CurrentLanguage = NEDERLANDS, "\DemoBoard NL.chm", "\DemoBoard EN.chm")
        If mnuFile.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "File Menu.htm")
        ElseIf mnuGame.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Game Menu.htm")
        ElseIf mnuPreviousGame.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Navigation.htm")
        ElseIf mnuDiagram.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Diagram Menu.htm")
        ElseIf mnuGraphicals.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Graphicals Menu.htm")
        ElseIf mnuView.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "View Menu.htm")
        ElseIf mnuSettings.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Settings Menu.htm")
        ElseIf mnuHelp.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Help Menu.htm")
        ElseIf mnuMenuStrip.Bounds.Contains(Position) Then
            Help.ShowHelp(Me, HelpFile, "Menu Bar.htm")
        ElseIf gfrmBoard.Visible = True _
            And gfrmBoard.RectangleToScreen(gfrmBoard.ctlBoard.SetupToolbar.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Setup Toolbar.htm")
        ElseIf gfrmBoard.Visible = True _
            And gfrmBoard.RectangleToScreen(gfrmBoard.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Chess Board.htm")
        ElseIf gfrmStockfish.Visible = True _
            And gfrmStockfish.RectangleToScreen(gfrmStockfish.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Stockfish.htm")
        ElseIf gfrmGameDetails.Visible = True _
            And gfrmGameDetails.RectangleToScreen(gfrmGameDetails.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Game Details.htm")
        ElseIf gfrmMoveList.Visible = True _
            And gfrmMoveList.RectangleToScreen(gfrmMoveList.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Move List.htm")
        ElseIf gfrmValidMoves.Visible = True _
            And gfrmValidMoves.RectangleToScreen(gfrmValidMoves.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Valid Moves.htm")
        ElseIf gfrmTitleAndMemo.Visible = True _
            And gfrmTitleAndMemo.RectangleToScreen(gfrmTitleAndMemo.Bounds).Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Title and Memo.htm")
        ElseIf Me.Bounds.Contains(MousePosition) Then
            Help.ShowHelp(Me, HelpFile, "Main Form.htm")
        Else
            Help.ShowHelp(Me, HelpFile)
        End If
    End Sub

    Private Sub mnuHelpIndex_Click(pSender As Object, pArgs As EventArgs) Handles mnuHelpIndex.Click
        Dim HelpFile As String = If(CurrentLanguage = NEDERLANDS, "DemoBoard EN.chm", "DemoBoard EN.chm")
        Help.ShowHelpIndex(Me, HelpFile)
    End Sub

    Private Sub mnuAbout_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuAbout.Click
        Try
            frmAbout.ShowDialog()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

#End Region

#Region "frmMainForm Events"

    Private Sub InitSubForms()
        gfrmBoard = New frmBoard(Me)
        gfrmStockfish = New frmStockfish(Me)
        gfrmMoveList = New frmMoveList(Me)
        gfrmValidMoves = New frmValidMoves(Me)
        gfrmGameDetails = New frmGameDetails(Me)
        gfrmTitleAndMemo = New frmTitleAndMemo(Me)
    End Sub

    Private Sub DisconnectSubForms()
        gfrmBoard.Parent = Nothing
        gfrmStockfish.Parent = Nothing
        gfrmMoveList.Parent = Nothing
        gfrmValidMoves.Parent = Nothing
        gfrmGameDetails.Parent = Nothing
        gfrmTitleAndMemo.Parent = Nothing
    End Sub

    Private Sub frmMainForm_Load(pSender As Object, pArgs As EventArgs) Handles Me.Load
        Try
            CurrentLanguage = GetLanguage()
            SetLanguage(CurrentLanguage, Me)

            Dim Arguments() As String = Environment.GetCommandLineArgs()
            If Arguments.Count > 1 Then
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If Arguments(1) Like "*.cps" Then
                    Dim CPSFile As New CPSFile(Arguments(1))
                    Me.Text = CPSFile.FileName
                    Me.PGNFile = New PGNFile() With {.PGNGames = CPSFile.ConvertToPGN()}
                    If Me.PGNFile.PGNGames.Count > 0 Then
                        Me.PGNGame = Me.PGNFile.PGNGames(0)
                    End If
                Else
                    Me.PGNFile = New PGNFile(Arguments(1))
                    Me.Text = Me.PGNFile.FileName
                End If
                Cursor = Cursors.Default
            Else
                Me.PGNFile = New PGNFile()
            End If

            Call InitSubForms()

            Dim DefaultFile As String = RootFolder() & "Settings\Default.xml"
            If IO.File.Exists(DefaultFile) Then
                DeSerializeLayout(DefaultFile)
            Else
                gfrmMoveList.TopLevel = False
                gfrmMoveList.Visible = True : gfrmMoveList.FormBorderStyle = FormBorderStyle.None
                gfrmMoveList.Dock = DockStyle.Fill
                ctlTabControl.AddTabPage(gfrmMoveList)

                gfrmTitleAndMemo.TopLevel = False
                gfrmTitleAndMemo.Visible = True : gfrmTitleAndMemo.FormBorderStyle = FormBorderStyle.None
                gfrmTitleAndMemo.Dock = DockStyle.Fill
                ctlTabControl.AddTabPage(gfrmTitleAndMemo)

                gfrmGameDetails.TopLevel = False
                gfrmGameDetails.Visible = True : gfrmGameDetails.FormBorderStyle = FormBorderStyle.None
                gfrmGameDetails.Dock = DockStyle.Fill
                ctlTabControl.AddTabPage(gfrmGameDetails)
            End If

        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub frmMainForm_SizeChanged(pSender As Object, pArgs As EventArgs) Handles Me.SizeChanged
        If Me.MenuLocation = DockStyle.Top Then
            pnlMainPanel.Top = mnuMenuStrip.Height + 4
        Else
            pnlMainPanel.Top = If(mnuStatusBar.Checked, stsStatusStrip.Height, 0) + 4
        End If
        pnlMainPanel.Height = Me.ClientRectangle.Height - mnuMenuStrip.Height _
                            - If(mnuStatusBar.Checked, stsStatusStrip.Height, 0) - 7
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef pMsg As Message, pKeyData As Keys) As Boolean
        RaiseEvent BoardKeyDown(pMsg, pKeyData)

        Return MyBase.ProcessCmdKey(pMsg, pKeyData)
    End Function

    Private Sub frmMainForm_KeyDown(pSender As Object, pArgs As KeyEventArgs) Handles Me.KeyDown
        pArgs.Handled = False 'To forward the event also to other controls
    End Sub

    Private Sub gfrmMoveList_PositionChanged(pBeforeHalfMove As PGNHalfMove, pAfterHalfMove As PGNHalfMove) Handles gfrmMoveList.PositionChanged
        Try
            Dim BeforeImage As String = "", AfterImage As String = ""
            If pBeforeHalfMove IsNot Nothing Then
                BeforeImage = CStr(pBeforeHalfMove.Index)
            End If
            If pAfterHalfMove IsNot Nothing Then
                AfterImage = CStr(pAfterHalfMove.Index)
            End If
            gJournaling.SaveImage("HalfMove.Index", BeforeImage, AfterImage)

            RaiseEvent MoveListPositionChanged(Me.PGNGame, pAfterHalfMove)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmMoveList_HalfMoveChanged(pBeforeImage As String, pAfterImage As String) Handles gfrmMoveList.HalfMoveChanged
        gJournaling.SaveImage("HalfMove.Changed", gfrmMoveList.CurrentHalfMoveIndex, pBeforeImage, pAfterImage)
    End Sub

    Private Sub gfrmMoveList_MoveListChanged(pBeforeImage As String, pAfterImage As String) Handles gfrmMoveList.MoveListChanged
        gJournaling.SaveImage("MoveList.Changed", pBeforeImage, pAfterImage)
    End Sub

    Private Sub frmMainForm_MouseMove(pSender As Object, pArgs As MouseEventArgs) Handles Me.MouseMove
        'Dragging Panel
        If pnlDragPanel.Visible = True Then
            Dim MouseLocation As New Point(Me.PointToScreen(pArgs.Location))
            Dragging(MouseLocation)
        End If
    End Sub

    Private Sub frmMainForm_MouseUp(pSender As Object, pArgs As MouseEventArgs) Handles Me.MouseUp
        StopDragging()
    End Sub

    Private Sub frmMainForm_MouseLeave(pSender As Object, pArgs As EventArgs) Handles Me.MouseLeave
        StopDragging()
    End Sub

#End Region

#Region "Events from Subforms"

    Private Sub gfrmBoard_ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard) Handles gfrmBoard.ChessPieceStartMoving
        lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Bezig met zet", "Moving piece")
        RaiseEvent ChessPieceStartMoving(pPiece, pFromFieldName, pChessBoard) 'To Update frmValidMoves
    End Sub

    Private Sub gfrmBoard_ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                                       pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pFEN As String, pFENBeforeDragging As String) Handles gfrmBoard.ChessPieceMoved
        Dim BeforeImage As New XElement("Before"), AfterImage As New XElement("After")
        Dim HalfMove As PGNHalfMove, MoveNr As Long

        lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Klaar", "Ready")

        If Me.Mode = TRAINING Then
            If gfrmTrainingQuestion Is Nothing Then Exit Sub
            gfrmTrainingQuestion.CheckAnswer(pPiece.MoveName, pFromFieldName, pToFieldName, If(pPromotionPiece Is Nothing, "", pPromotionPiece.MoveName))
            Exit Sub
        End If

        BeforeImage.Add(New XElement("FEN", pFENBeforeDragging))
        BeforeImage.Add(New XElement("XPGN", Me.PGNGame.HalfMoves.XPGNString))
        BeforeImage.Add(New XElement("Index", gfrmMoveList.CurrentHalfMoveIndex))

        If Me.Mode = SETUP Then
            Me.PGNGame.Tags.Add("FEN", pFEN)
            Me.PGNGame.HalfMoves.Clear()
            AfterImage.Add(New XElement("FEN", pFEN))
            AfterImage.Add(New XElement("XPGN", Me.PGNGame.HalfMoves.XPGNString))
            AfterImage.Add(New XElement("Index", ""))
            gJournaling.SaveImage("ChessPiece.Moved", BeforeImage.ToString, AfterImage.ToString)
            RaiseEvent FENChanged(pFEN) 'To Update frmValidMoves
            Exit Sub
        End If

        If gfrmMoveList.CurrentHalfMove Is Nothing Then
            MoveNr = Me.PGNGame.FirstMoveNr()
        Else
            If pPiece.Color = BLACK Then
                MoveNr = gfrmMoveList.CurrentHalfMove.MoveNr
            Else
                MoveNr = gfrmMoveList.CurrentHalfMove.MoveNr + 1
            End If
        End If

        HalfMove = New PGNHalfMove(Me.PGNGame.HalfMoves, pChessBoard,
                                   MoveNr, pPiece, pFromFieldName, pToFieldName,
                                   pCaptured, pPromotionPiece, pFENBeforeDragging)

        If Me.PGNGame.HalfMoves.Insert(HalfMove, gfrmMoveList.CurrentHalfMove) = False Then
            'HalfMove Not Inserted; Restore ChessBoard  
            gfrmBoard.FEN = pFENBeforeDragging
            Exit Sub
        End If

        gfrmMoveList.CurrentHalfMove = HalfMove
        RaiseEvent MoveListPositionChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

        AfterImage.Add(New XElement("FEN", pFEN))
        AfterImage.Add(New XElement("XPGN", Me.PGNGame.HalfMoves.XPGNString))
        AfterImage.Add(New XElement("Index", Str(HalfMove.Index)))
        gJournaling.SaveImage("ChessPiece.Moved", BeforeImage.ToString, AfterImage.ToString)

        'Update Statusbar with Check, Checkmate or Stalemate
        Dim PossibleMoves As List(Of BoardMove)
        If King.InCheck(pChessBoard.ActiveColor, pChessBoard) Then
            PossibleMoves = pChessBoard.AllPossibleMoves(pChessBoard.ActiveColor)
            'While PossibleMoves.Count > 0 'Remove castling moves
            '    If PossibleMoves(0).Castle = True Then
            '        PossibleMoves.RemoveAt(0)
            '    Else
            '        Exit While
            '    End If
            'End While
            If PossibleMoves.Count = 0 Then
                lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Mat !", "Checkmate !")
            Else
                lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Schaak !", "Check !")
            End If
        Else
            'King not in Check
            If pChessBoard.AllPossibleMoves(pChessBoard.ActiveColor).Count = 0 Then
                lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Pat !", "Stalemate !")
            End If
        End If

        RaiseEvent ChessPieceMoved(pPiece, pFromFieldName, pToFieldName, pChessBoard, pCaptured, pPromotionPiece)
    End Sub

    Private Sub gfrmBoard_FENChanged(pFEN As String) Handles gfrmBoard.FENChanged
        Dim KeyValue As String, BeforeImage As New XElement("Before"), AfterImage As New XElement("After")
        lblStatusText.Text = If(CurrentLanguage = NEDERLANDS, "Klaar", "Ready")
        If Me.Mode <> ChessMode.SETUP Then
            If Me.PGNGame.HalfMoves.Count > 0 Then
                If MsgBox(MessageText("UpdateFEN"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            End If
        End If
        KeyValue = gfrmMoveList.CurrentHalfMoveIndex
        BeforeImage.Add(New XElement("FEN", Me.PGNGame.Tags.GetPGNTag("FEN")))
        BeforeImage.Add(New XElement("XPGN", Me.PGNGame.HalfMoves.XPGNString))
        Me.PGNGame.Tags.Add("FEN", pFEN)
        Me.PGNGame.HalfMoves.Clear()
        AfterImage.Add(New XElement("FEN", pFEN))
        AfterImage.Add(New XElement("XPGN", ""))
        gJournaling.SaveImage("FEN", KeyValue, BeforeImage.ToString, AfterImage.ToString)
        RaiseEvent FENChanged(pFEN) 'To Update frmValidMoves
    End Sub

    Private Sub gfrmBoard_FieldMarkerListChanged(pHalfMove As PGNHalfMove, pMarkerString As String) Handles gfrmBoard.FieldMarkerListChanged
        Dim BeforeImage As String = Me.PGNGame.HalfMoves.MarkerListString(pHalfMove)
        Me.PGNGame.HalfMoves.MarkerListString(pHalfMove) = pMarkerString
        gJournaling.SaveImage("FieldMarkerList", If(pHalfMove Is Nothing, "", CStr(pHalfMove.Index)), BeforeImage, pMarkerString)
    End Sub

    Private Sub gfrmBoard_ArrowListChanged(pHalfMove As PGNHalfMove, pArrowString As String) Handles gfrmBoard.ArrowListChanged
        Dim BeforeImage As String = Me.PGNGame.HalfMoves.ArrowListString(pHalfMove)
        Me.PGNGame.HalfMoves.ArrowListString(pHalfMove) = pArrowString
        gJournaling.SaveImage("ArrowList", If(pHalfMove Is Nothing, "", CStr(pHalfMove.Index)), BeforeImage, pArrowString)
    End Sub

    Private Sub gfrmBoard_TextListChanged(pHalfMove As PGNHalfMove, pTextString As String) Handles gfrmBoard.TextListChanged
        Dim BeforeImage As String = Me.PGNGame.HalfMoves.TextListString(pHalfMove)
        Me.PGNGame.HalfMoves.TextListString(pHalfMove) = pTextString
        gJournaling.SaveImage("TextList", If(pHalfMove Is Nothing, "", CStr(pHalfMove.Index)), BeforeImage, pTextString)
    End Sub

    Private Sub gfrmBoard_MovePlayed(pHalfMove As PGNHalfMove) Handles gfrmBoard.MovePlayed
        RaiseEvent MovePlayed(pHalfMove)
    End Sub

    Private Sub gfrmMoveList_TrainingQuestionFound(pHalfMove As PGNHalfMove, pNextMoves As List(Of PGNHalfMove)) Handles gfrmMoveList.TrainingQuestionFound
        Me.AskQuestion(pHalfMove)
    End Sub

    Private Sub gfrmGameDetails_DoubleClicked() Handles gfrmGameDetails.DoubleClicked
        mnuEditGame_Click(Nothing, Nothing)
    End Sub

    Private Sub gfrmTitleAndMemo_DoubleClicked() Handles gfrmTitleAndMemo.DoubleClicked
        mnuEditTitleAndMemo_Click(Nothing, Nothing)
    End Sub

    Private Sub gfrmValidMoves_ValidMovesSelectionChanged(pSender As Object, pMove As BoardMove) Handles gfrmValidMoves.ValidMovesSelectionChanged
        'Forward event (to frmBoard)
        RaiseEvent ValidMovesSelectionChanged(pSender, pMove)
    End Sub

#End Region

#Region "Training Question"
    Private Sub StartTraining(pTrainingHalfMove As PGNHalfMove)
        RaiseEvent GameChanged(Me.PGNGame)
        RaiseEvent MoveListPositionChanged(Me.PGNGame, Nothing)

        Me.PlayMoves(Nothing, pTrainingHalfMove.PreviousHalfMove)
        Me.AskQuestion(pTrainingHalfMove)
    End Sub

    Private Sub PlayMoves(pFromHalfMove As PGNHalfMove)
        Dim MovesToPlay As List(Of PGNHalfMove)
        MovesToPlay = Me.PGNGame.HalfMoves.CollectMoves(pFromHalfMove)
        gfrmBoard.PlayMoves(MovesToPlay)
    End Sub

    Private Sub PlayMoves(pFromHalfMove As PGNHalfMove, pToHalfMove As PGNHalfMove)
        Dim MovesToPlay As List(Of PGNHalfMove)
        MovesToPlay = Me.PGNGame.HalfMoves.CollectMoves(pFromHalfMove, pToHalfMove)
        gfrmBoard.PlayMoves(MovesToPlay)
    End Sub

    Private Sub AskQuestion(pTrainingHalfMove As PGNHalfMove)
        'gdgfdgdf    gfrmMoveList.ctlMoveList.Visible = False
        'Make sure the right position is visible at the board
        RaiseEvent MoveListPositionChanged(Me.PGNGame, pTrainingHalfMove?.PreviousHalfMove)
        Application.DoEvents()
        If gfrmTrainingQuestion Is Nothing Then
            gfrmTrainingQuestion = New frmTrainingQuestion
        End If
        gfrmTrainingQuestion.Show(pTrainingHalfMove, Me)
    End Sub

    Private Sub gfrmTrainingQuestion_RetryPressed(pTrainingHalfMove As PGNHalfMove) Handles gfrmTrainingQuestion.RetryPressed
        'Make sure the right position is visible at the board
        RaiseEvent MoveListPositionChanged(Me.PGNGame, pTrainingHalfMove?.PreviousHalfMove)
        Application.DoEvents()
    End Sub

    Private Sub gfrmTrainingQuestion_NextPressed(pTrainingHalfMove As PGNHalfMove) Handles gfrmTrainingQuestion.NextPressed
        Dim NextHalfMoveWithTrainingQuestion As PGNHalfMove = Me.PGNGame.NextHalfMoveWithTrainingQuestion(pTrainingHalfMove)
        If NextHalfMoveWithTrainingQuestion Is Nothing Then
            mnuNextGame.PerformClick()
        Else
            RaiseEvent MoveListPositionChanged(Me.PGNGame, pTrainingHalfMove?.PreviousHalfMove)
            Application.DoEvents()
            Me.PlayMoves(pTrainingHalfMove, NextHalfMoveWithTrainingQuestion.PreviousHalfMove)
            Me.AskQuestion(NextHalfMoveWithTrainingQuestion)
        End If
    End Sub

    Private Sub gfrmTrainingQuestion_DetailsPressed(pIncorrectSubVariant As PGNHalfMove) Handles gfrmTrainingQuestion.DetailsPressed
        Me.PlayMoves(pIncorrectSubVariant.NextHalfMoves(0))
        Wait(3000)
        RaiseEvent MoveListPositionChanged(Me.PGNGame, pIncorrectSubVariant)
        Application.DoEvents()
    End Sub

    Private Sub gfrmTrainingQuestion_SolutionPressed(pTrainingHalfMove As PGNHalfMove, pCorrectAnswer As PGNTrainingAnswer) Handles gfrmTrainingQuestion.SolutionPressed
        'Make sure the right position is visible at the board
        RaiseEvent MoveListPositionChanged(Me.PGNGame, pTrainingHalfMove?.PreviousHalfMove)
        Application.DoEvents()

        gfrmBoard.PlayMove(pCorrectAnswer.Move)

        'Make sure the right position is visible at the board
        RaiseEvent MoveListPositionChanged(Me.PGNGame, pTrainingHalfMove?.PreviousHalfMove)
        Application.DoEvents()
    End Sub

#End Region

#Region "Dragging And Dropping Panels"

    'Variables =====================================

    Public Event ForwardedMouseUp(pDragPanel As Panel, pDockStyle As DockStyle, pMouseLocation As Point)
    Public Event ForwardedMousePanelLeave(pMouseLocation As Point)
    Public gTabControlWithDockingCross As ctlTabControl
    Private gPanelCloseIconLocation As New Point(-18, 5)
    Private DragPanelOffset As Point = Nothing

    Public Event RemoveForm(pForm As Form)

    ' Menu Options ==============================================

    Private Sub UpdateBoardSubForm()
        If mnuBoard.Checked = True Then
            'Default left in MainPanel; new split with Board on the Left and rest on the right 
            InsertPanel(gfrmBoard, Orientation.Vertical, 1)
        Else
            RaiseEvent RemoveForm(gfrmBoard)
        End If
    End Sub

    Private Sub UpdateMoveListSubForm()
        If mnuMoveList.Checked = True Then
            'Default right in MainPanel; new split with MoveList on the right and rest on the left 
            InsertPanel(gfrmMoveList, Orientation.Vertical, 2)
        Else
            RaiseEvent RemoveForm(gfrmMoveList)
        End If
    End Sub

    Private Sub UpdateGameDetailsSubForm()
        If mnuGameDetails.Checked = True Then
            'Default Bottom in MainPanel; new split with Game Details at the bottom and rest at the top 
            InsertPanel(gfrmGameDetails, Orientation.Horizontal, 2)
        Else
            RaiseEvent RemoveForm(gfrmGameDetails)
        End If
    End Sub

    Private Sub UpdateValidMovesSubForm()
        If mnuValidMoves.Checked = True Then
            'Default Bottom in MainPanel; new split with Valid Moves at the bottom and rest at the top 
            InsertPanel(gfrmValidMoves, Orientation.Horizontal, 2)
        Else
            RaiseEvent RemoveForm(gfrmValidMoves)
        End If
    End Sub

    Private Sub UpdateTitleAndMemoSubForm()
        If mnuTitleAndMemo.Checked = True Then
            'Default Bottom in MainPanel; new split with Title and Memo at the bottom and rest at the top 
            InsertPanel(gfrmTitleAndMemo, Orientation.Horizontal, 2)
        Else
            RaiseEvent RemoveForm(gfrmTitleAndMemo)
        End If
    End Sub

    Private Sub UpdateStockfishSubForm()
        If mnuStockfish.Checked = False Then
            RaiseEvent RemoveForm(gfrmStockfish)
            Exit Sub
        End If
        If gfrmMoveList.Parent Is Nothing _
            OrElse gfrmMoveList.Parent.Parent Is Nothing _
            OrElse gfrmMoveList.Parent.Parent.Parent Is Nothing Then
            'Movelist parent Tabcontrol not found
            'Insert left in MainPanel; new split with Board on the Left and rest on the right 
            InsertPanel(gfrmStockfish, Orientation.Horizontal, 2)
            Exit Sub
        End If

        'Insert at the bottom of frmMoveList
        Dim TabControl As ctlTabControl = gfrmMoveList.Parent.Parent.Parent
        Dim ctlSplitContainer = New ctlSplitContainer(Orientation.Horizontal)
        TabControl.Parent.Controls.Add(ctlSplitContainer)

        Dim NewTabControl As New ctlTabControl()
        AddHandlers(NewTabControl)
        NewTabControl.AddTabPage(gfrmStockfish)

        ctlSplitContainer.Panel1 = TabControl
        ctlSplitContainer.Panel2 = NewTabControl
        ctlSplitContainer.SplitContainer.SplitterDistance = ctlSplitContainer.Height - 150
        NewTabControl.Select()
    End Sub

    'Public Methods and Functions ====================================

    Public Sub AddHandlers(pctlTabControl As ctlTabControl)
        AddHandler pctlTabControl.TabPageStartDragging, AddressOf ctlTabControl_TabPageStartDragging
        AddHandler pctlTabControl.TabPageDragging, AddressOf ctlTabControl_TabPageDragging
        AddHandler pctlTabControl.MouseUp, AddressOf ctlTabControl_MouseUp
        AddHandler pctlTabControl.TabPageDropped, AddressOf ctlTabControl_TabPageDropped
        AddHandler pctlTabControl.TabPageRemoved, AddressOf ctlTabControl_TabPageRemoved
        AddHandler pctlTabControl.MouseEnter, AddressOf ctlTabControl_MouseEnter
    End Sub

    Public Sub RemoveHandlers(pctlTabControl As ctlTabControl)
        RemoveHandler pctlTabControl.TabPageStartDragging, AddressOf ctlTabControl_TabPageStartDragging
        RemoveHandler pctlTabControl.TabPageDragging, AddressOf ctlTabControl_TabPageDragging
        RemoveHandler pctlTabControl.MouseUp, AddressOf ctlTabControl_MouseUp
        RemoveHandler pctlTabControl.TabPageDropped, AddressOf ctlTabControl_TabPageDropped
        RemoveHandler pctlTabControl.TabPageRemoved, AddressOf ctlTabControl_TabPageRemoved
        RemoveHandler pctlTabControl.MouseEnter, AddressOf ctlTabControl_MouseEnter
    End Sub
    'Private Methods and Functions =======================

    Public Sub InsertPanel(pForm As Form, pOrientation As Orientation, pPanelNumber As Integer)
        Dim MainPanelControl = GetMainPanelControl()
        If MainPanelControl Is Nothing Then
            Exit Sub
        End If

        Me.pnlMainPanel.Controls.Clear()
        Dim ctlSplitContainer = New ctlSplitContainer(pOrientation)
        Me.pnlMainPanel.Controls.Add(ctlSplitContainer)

        Dim NewTabControl = New ctlTabControl()
        AddHandlers(NewTabControl)
        NewTabControl.AddTabPage(pForm)
        If pPanelNumber = 1 Then
            ctlSplitContainer.Panel1 = NewTabControl
            ctlSplitContainer.Panel2 = MainPanelControl
        Else
            ctlSplitContainer.Panel1 = MainPanelControl
            ctlSplitContainer.Panel2 = NewTabControl
        End If
        NewTabControl.Focus()
        pForm.Dock = DockStyle.Fill
        pForm.Show()
    End Sub

    Private Sub CheckViewMenu(pForm As Form, pCheck As Boolean)
        Select Case TypeName(pForm)
            Case "frmBoard"
                mnuBoard.Checked = pCheck
            Case "frmStockfish"
                mnuStockfish.Checked = pCheck
            Case "frmMoveList"
                mnuMoveList.Checked = pCheck
            Case "frmValidMoves"
                mnuValidMoves.Checked = pCheck
            Case "frmGameDetails"
                mnuGameDetails.Checked = pCheck
            Case "frmTitleAndMemo"
                mnuTitleAndMemo.Checked = pCheck
        End Select
    End Sub

    Dim BeforeLayout As String = ""

    Private Sub ctlTabControl_TabPageStartDragging(pctlTabControl As ctlTabControl, pMouseLocation As Point) Handles ctlTabControl.TabPageStartDragging
        BeforeLayout = SerializeLayout()
        'Debug.Print("Start Dragging")
        StartDragging(pctlTabControl, pMouseLocation)
    End Sub

    Private Sub ctlTabControl_TabPageDragging(pctlTabControl As ctlTabControl, pMouseLocation As Point) Handles ctlTabControl.TabPageDragging
        If pnlDragPanel.Visible = True Then
            Dragging(pMouseLocation)
        End If
    End Sub

    Private Sub ctlTabControl_TabPageDropped(pForm As Form) Handles ctlTabControl.TabPageDropped
        CheckViewMenu(pForm, True)
        StopDragging()

        Dim AfterLayout As String = SerializeLayout()
        gJournaling.SaveImage("TabPage.Dropped", BeforeLayout, AfterLayout)
    End Sub

    Private Sub ctlTabControl_TabPageRemoved(pForm As Form) Handles ctlTabControl.TabPageRemoved
        CheckViewMenu(pForm, False)
    End Sub

    Private Sub ctlTabControl_MouseEnter(pctlTabControl As Object) Handles ctlTabControl.MouseEnter
        If pnlDragPanel.Visible = True Then
            gTabControlWithDockingCross = pctlTabControl
            gfrmDockCross.CenterForm(pctlTabControl)
        End If
    End Sub

    'This MouseUp event is generated by the original ctlTabControl where the MouseDown took place
    Private Sub ctlTabControl_MouseUp(pctlTabControl As ctlTabControl, pMouseLocation As Point) Handles ctlTabControl.MouseUp
        Dim ctlSplitContainer As ctlSplitContainer, NewTabControl As ctlTabControl

        If pnlDragPanel.Visible = True Then

            'Dropping at a Docking Position
            Select Case gfrmDockCross.DockStyle
                Case DockStyle.Left
                    ctlSplitContainer = New ctlSplitContainer(Orientation.Vertical)
                    gTabControlWithDockingCross.Parent.Controls.Add(ctlSplitContainer)
                    NewTabControl = New ctlTabControl()
                    AddHandlers(NewTabControl)
                    NewTabControl.AddTabPage(Me.pnlDragPanel)
                    ctlSplitContainer.Panel1 = NewTabControl
                    ctlSplitContainer.Panel2 = gTabControlWithDockingCross
                    NewTabControl.Select()
                    StopDragging()
                Case DockStyle.Right
                    ctlSplitContainer = New ctlSplitContainer(Orientation.Vertical)
                    gTabControlWithDockingCross.Parent.Controls.Add(ctlSplitContainer)
                    NewTabControl = New ctlTabControl()
                    AddHandlers(NewTabControl)
                    NewTabControl.AddTabPage(Me.pnlDragPanel)
                    ctlSplitContainer.Panel1 = gTabControlWithDockingCross
                    ctlSplitContainer.Panel2 = NewTabControl
                    NewTabControl.Select()
                    StopDragging()
                Case DockStyle.Top
                    ctlSplitContainer = New ctlSplitContainer(Orientation.Horizontal)
                    gTabControlWithDockingCross.Parent.Controls.Add(ctlSplitContainer)
                    NewTabControl = New ctlTabControl()
                    AddHandlers(NewTabControl)
                    NewTabControl.AddTabPage(Me.pnlDragPanel)
                    ctlSplitContainer.Panel1 = NewTabControl
                    ctlSplitContainer.Panel2 = gTabControlWithDockingCross
                    NewTabControl.Select()
                    StopDragging()
                Case DockStyle.Bottom
                    ctlSplitContainer = New ctlSplitContainer(Orientation.Horizontal)
                    gTabControlWithDockingCross.Parent.Controls.Add(ctlSplitContainer)
                    NewTabControl = New ctlTabControl()
                    AddHandlers(NewTabControl)
                    NewTabControl.AddTabPage(Me.pnlDragPanel)
                    ctlSplitContainer.Panel1 = gTabControlWithDockingCross
                    ctlSplitContainer.Panel2 = NewTabControl
                    NewTabControl.Select()
                    StopDragging()
                Case DockStyle.Fill
                    gTabControlWithDockingCross.AddTabPage(Me.pnlDragPanel) 'Drop Back at previous TabControl
                    StopDragging()
                Case Else
                    'So Dropping at one of the ctlTabControls listening to this event
                    RaiseEvent ForwardedMouseUp(pnlDragPanel, gfrmDockCross.DockStyle, pMouseLocation)
            End Select
            StopDragging()
            Application.DoEvents()

            Dim AfterLayout As String = SerializeLayout()
            gJournaling.SaveImage("Layout", BeforeLayout, AfterLayout)
        End If
    End Sub

    'Private Subs And Functions =================== 

    Private Sub StartDragging(pctlTabControl As ctlTabControl, pMouseLocation As Point)
        Try
            If gfrmDockCross Is Nothing Then
                gfrmDockCross = New frmDockCross(Me)
                gfrmDockCross.CenterForm(pctlTabControl)
            End If

            gTabControlWithDockingCross = pctlTabControl

            Dim Form As Form = pctlTabControl.SubForm(pctlTabControl.SelectedIndex)
            CheckViewMenu(Form, False)
            Dim TabRectangle As Rectangle = pctlTabControl.GetTabRect(pctlTabControl.SelectedIndex)
            pnlDragPanel.Controls.Add(Form) 'Form is first and only control at DragPanel !
            pnlDragPanel.Location = Me.PointToClient(pMouseLocation) 'New Point(pMouseLocation.X + DragPanelOffset.X, pMouseLocation.Y + DragPanelOffset.Y)
            DragPanelOffset = New Point(pctlTabControl.Parent.Location.X - pnlDragPanel.Location.X, pctlTabControl.Parent.Location.Y + TabRectangle.Height - pnlDragPanel.Location.Y)
            pnlDragPanel.Location.Offset(DragPanelOffset)

            'Debug.Print(pctlTabControl.SelectedTab.Size.Width)
            'Debug.Print(pctlTabControl.SelectedTab.Size.Height)
            pnlDragPanel.Size = New Point(pctlTabControl.SelectedTab.Size.Width * 0.7, pctlTabControl.SelectedTab.Size.Height * 0.7)
            pnlDragPanel.BringToFront()
            pnlDragPanel.Visible = True
            pctlTabControl.RemoveTab(pctlTabControl.SelectedTab, Form) 'Form needs to be passed because Form was trasferred to DragPanel
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub Dragging(pMouseLocation As Point)
        Try
            pnlDragPanel.Location = Me.PointToClient(pMouseLocation)
            pnlDragPanel.Location.Offset(DragPanelOffset)

            If gTabControlWithDockingCross Is Nothing Then
                'Determine where position belongs to...
                RaiseEvent ForwardedMousePanelLeave(pMouseLocation)
            ElseIf gTabControlWithDockingCross.IsDisposed() = True Then
                'Determine where position belongs to...
                RaiseEvent ForwardedMousePanelLeave(pMouseLocation)
            Else
                Dim DockingCrossTabControlScreenRectangle = gTabControlWithDockingCross.RectangleToScreen(gTabControlWithDockingCross.DisplayRectangle)
                If DockingCrossTabControlScreenRectangle.Contains(pMouseLocation) Then
                Else
                    gTabControlWithDockingCross = Nothing
                    'Determine where position belongs to...
                    RaiseEvent ForwardedMousePanelLeave(pMouseLocation)
                End If
            End If
            gfrmDockCross.UpdateAppearance(gfrmDockCross.PointToClient(Cursor.Position))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub StopDragging()
        Try
            pnlDragPanel.Visible = False
            If gfrmDockCross IsNot Nothing Then
                gfrmDockCross.Close()
                gfrmDockCross = Nothing
            End If

            gTabControlWithDockingCross = Nothing
            Me.Activate()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

#End Region

#Region "Serializing and Deserializing Panel Layout"

    Public Function SerializeLayout() As String
        Dim Writer As New IO.StringWriter()
        SerializeLayout(Writer)
        Return Writer.ToString()
    End Function

    Public Sub SerializeLayout(pFileName As String)
        Dim Writer = New System.IO.StreamWriter(pFileName) With {.AutoFlush = True}
        Me.SerializeLayout(Writer)
    End Sub

    Private Sub SerializeLayout(pWriter As IO.TextWriter)
        pWriter.WriteLine("<MainForm Width=""" & Strings.Format(Me.Width) & """ Height=""" & Strings.Format(Me.Height) &
                                  """ WindowState=""" & Strings.Format(Me.WindowState) &
                                  """ StatusBar=""" & Strings.Format(Me.mnuStatusBar.Checked) &
                                  """ MenuLocation=""" & Strings.Format(Me.MenuLocation) &
                                  """>")
        Dim Control = GetMainPanelControl()
        Select Case TypeName(Control)
            Case "ctlTabControl"
                CType(Control, ctlTabControl).Serialize(pWriter, 1)
            Case "ctlSplitContainer"
                CType(Control, ctlSplitContainer).Serialize(pWriter, 1)
        End Select
        pWriter.WriteLine("</MainForm>")
        pWriter.Close()
    End Sub

    Private Sub DeSerializeLayoutFromString(pXMLText As String)
        If pXMLText = "" Then Exit Sub
        DeSerializeLayout(XDocument.Parse(pXMLText))
    End Sub

    Public Sub DeSerializeLayout(pFileName As String)
        Dim Reader As New IO.StreamReader(pFileName)
        Dim XMLText As String = Reader.ReadToEnd()

        mnuBoard.Checked = False
        mnuStockfish.Checked = False
        mnuMoveList.Checked = False
        mnuValidMoves.Checked = False
        mnuTitleAndMemo.Checked = False
        mnuGameDetails.Checked = False

        DeSerializeLayout(XDocument.Parse(XMLText))

        Reader.Close()
    End Sub

    Private Sub DeSerializeLayout(pXMLDocument As XDocument)
        If pXMLDocument.Root.Name = "MainForm" Then
            For Each Attrib As XAttribute In pXMLDocument.Root.Attributes()
                Select Case Attrib.Name
                    Case "Width" : Me.Width = Val(Attrib.Value)
                    Case "Height" : Me.Height = Val(Attrib.Value)
                    Case "WindowState" : Me.WindowState = Attrib.Value
                    Case "StatusBar" : Me.mnuStatusBar.Checked = Val(Attrib.Value)
                        Me.stsStatusStrip.Visible = Me.mnuStatusBar.Checked
                    Case "MenuLocation" : Me.MenuLocation = Val(Attrib.Value)
                End Select
                Me.frmMainForm_SizeChanged(Nothing, Nothing) 'To Update visibility of the Tool and Status-Bar
            Next Attrib
        End If

        Dim Index As String = gfrmMoveList.CurrentHalfMoveIndex

        Call DisconnectSubForms() 'To prevent them from being disposed too
        Me.pnlMainPanel.Controls(0).Dispose()
        Me.pnlMainPanel.Controls.Clear()

        RaiseEvent GameChanged(Me.PGNGame)
        gfrmMoveList.CurrentHalfMoveIndex = Index

        For Each Element As XElement In pXMLDocument.Root.Elements()
            Select Case Element.Name
                Case "ctlSplitContainer"
                    Dim SplitContainer As New ctlSplitContainer(Val(Element.Attribute("Orientation").Value))
                    pnlMainPanel.Controls.Add(SplitContainer)
                    SplitContainer.DeSerialize(Element)
                Case "ctlTabControl"
                    Dim ctlTabControl As New ctlTabControl()
                    AddHandlers(ctlTabControl)
                    pnlMainPanel.Controls.Add(ctlTabControl)
                    ctlTabControl.DeSerialize(Element)
            End Select
        Next Element
    End Sub

    Private Function GetMainPanelControl() As Control
        For Each Control As Control In Me.pnlMainPanel.Controls
            Select Case TypeName(Control)
                Case "ctlTabControl", "ctlSplitContainer"
                    Return Control
            End Select
        Next Control
        Return Nothing
    End Function

    'Private Function GetPanelControl(pSplitContainer As ctlSplitContainer, pForm As Form) As Control
    '    If pSplitContainer.Panel1.Controls(0) Is pForm Then Return pSplitContainer.Panel1
    '    If pSplitContainer.Panel2.Controls(0) Is pForm Then Return pSplitContainer.Panel2

    'End Function

#End Region

#Region "Journaling"

    Private Sub gJournaling_PointerUpdated(pCount As Integer, pPointer As Integer, pUndoToolTip As String, pRedoToolTip As String) Handles gJournaling.PointerUpdated
        mnuUndo.Enabled = (pPointer > -1)
        mnuUndo.ToolTipText = pUndoToolTip
        mnuRedo.Enabled = ((pPointer + 1) > -1) And ((pPointer + 1) < pCount)
        mnuRedo.ToolTipText = pRedoToolTip
    End Sub

    Private Sub gJournaling_UpdateRequested(pClassName As String, pKeyValue As String, pOldValue As String, pNewValue As String) Handles gJournaling.UpdateRequested
        Select Case pClassName
            Case "PGNGame.Index"           '(No Key) Select, First, Next, Previous, Last Game
                If pNewValue = "" Then Exit Sub
                Me.PGNGame = Me.PGNFile.PGNGames(Val(pNewValue))

            Case "PGNGame"                 '(No Key) Edit, Delete Game
                If pNewValue = "" Then 'Delete
                    Me.PGNGame = Me.PGNFile.PGNGames.Remove(Me.PGNGame)
                ElseIf pOldValue = "" Then 'New Game
                    Dim Game As PGNGame = Journaling.DeSerialize(pNewValue, PGNGame.GetType())
                    Game.HalfMoves.ReNumber()
                    Me.PGNFile.PGNGames.Insert(Game.Index, Game) 'Insert at the original position
                    Me.PGNGame = Game
                Else 'Edits to Tags and/or FENComment
                    Dim Game As PGNGame = Journaling.DeSerialize(pNewValue, PGNGame.GetType())
                    Game.HalfMoves.ReNumber()
                    Me.PGNFile.PGNGames(Game.Index) = Game 'Replace at the original position
                    Me.PGNGame = Game
                End If

            Case "PGNGame.New"                 '(No Key) New
                If pNewValue = "New" Then
                    Me.PGNGame = Me.PGNFile.PGNGames.Add()
                Else
                    Me.PGNGame = Me.PGNFile.PGNGames.Remove(Me.PGNGame)
                    If pNewValue <> "" Then
                        Me.PGNGame = Me.PGNFile.PGNGames(Val(pNewValue))
                    End If
                End If

            Case "PasteGame"               '(No Key) Paste of PGNGame 
                If pNewValue Like "*[[]* [""]*[""][]]*" Then
                    Dim Game = New PGNGame()
                    Game.XPGNString = pNewValue
                    Me.PGNFile.PGNGames.Insert(PGNGame.Index + 1, Game)
                    PGNGame = Game
                Else
                    Me.PGNGame = PGNFile.PGNGames.Remove(Me.PGNGame)
                End If

            Case "Mode"                    '(No Key) Change of Mode 
                mnuMode.Text = pNewValue

            Case "PasteDiagram"            '(No Key) Paste of Diagram 
                If pNewValue Like "*/*/*/*/*/*/*/* [bw] *" Then
                    Dim Game As New PGNGame(pNewValue)
                    Me.PGNFile.PGNGames.Insert(PGNGame.Index + 1, Game)
                    PGNGame = Game
                Else
                    Me.PGNGame = PGNFile.PGNGames.Remove(Me.PGNGame)
                End If

            Case "ClearDiagram"            '(No Key) Clear of Diagram 
                If pNewValue Like "<[?]xml *" Then 'Serialized object
                    Dim Game As PGNGame = Journaling.DeSerialize(pNewValue, PGNGame.GetType())
                    Game.HalfMoves.ReNumber()
                    Me.PGNFile.PGNGames(Game.Index) = Game 'Replace at the original position
                    Me.PGNGame = Game
                Else
                    Me.PGNGame.Clear()
                End If

            Case "InitialDiagram"          '(No Key) Clear of Diagram 
                If pNewValue Like "<[?]xml *" Then 'Serialized object
                    Dim Game As PGNGame = Journaling.DeSerialize(pNewValue, PGNGame.GetType())
                    Game.HalfMoves.ReNumber()
                    Me.PGNFile.PGNGames(Game.Index) = Game 'Replace at the original position
                    Me.PGNGame = Game
                Else
                    Me.PGNGame.Initial()
                End If

            Case "HalfMove.Changed"        '(With Key) Edit HalfMove
                gfrmMoveList.CurrentHalfMoveIndex = pKeyValue
                gfrmMoveList.CurrentHalfMove.JournalImage = pNewValue
                RaiseEvent HalfMoveChanged(Me.PGNGame, gfrmBoard.gCurrentHalfMove)

            Case "HalfMove.Index"          '(No Key) First, Next, Previous, Last Move
                gfrmMoveList.CurrentHalfMoveIndex = pNewValue
                RaiseEvent MoveListPositionChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "MoveList.Changed"        '(With Key) Delete Halfmove
                Dim NewValue As XElement = XElement.Parse(pNewValue)
                Me.PGNGame.HalfMoves.XPGNString = NewValue.Element("HalfMoves").Value
                gfrmMoveList.CurrentHalfMoveIndex = NewValue.Element("Index").Value
                RaiseEvent MoveListPositionChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "ChessPiece.Moved"        '(No Key) 
                Dim NewValue As XElement = XElement.Parse(pNewValue)
                If Me.Mode = ChessMode.SETUP Then
                    Dim FEN = NewValue.Element("FEN").Value
                    Me.PGNGame.Tags.Add("FEN", FEN)
                    gfrmMoveList.CurrentHalfMoveIndex = ""
                Else 'PLAY and TRAINING Mode
                    Dim HalfMoves = NewValue.Element("XPGN").Value
                    Me.PGNGame.HalfMoves.XPGNString = HalfMoves
                    gfrmMoveList.CurrentHalfMoveIndex = NewValue.Element("Index").Value
                End If
                RaiseEvent MoveListPositionChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "FEN"                     '(No Key)
                Dim NewValue As XElement = XElement.Parse(pNewValue)
                Dim FEN = NewValue.Element("FEN").Value
                Dim HalfMoves = NewValue.Element("XPGN").Value
                Me.PGNGame.Tags.Add("FEN", FEN)
                Me.PGNGame.HalfMoves.XPGNString = HalfMoves
                gfrmMoveList.CurrentHalfMoveIndex = pKeyValue
                RaiseEvent MoveListPositionChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "FieldMarkerList"         '(With Key)
                If pKeyValue = "" Then
                    Me.PGNGame.HalfMoves.MarkerListString(Nothing) = pNewValue
                Else
                    Me.PGNGame.HalfMoves.MarkerListString(Me.PGNGame.HalfMoves(Val(pKeyValue))) = pNewValue
                End If
                'gfrmBoard.MarkerString = pNewValue
                RaiseEvent HalfMoveChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "ArrowList"               '(With Key)
                If pKeyValue = "" Then
                    Me.PGNGame.HalfMoves.ArrowListString(Nothing) = pNewValue
                Else
                    Me.PGNGame.HalfMoves.ArrowListString(Me.PGNGame.HalfMoves(Val(pKeyValue))) = pNewValue
                End If
                RaiseEvent HalfMoveChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "TextList"                '(With Key)
                If pKeyValue = "" Then
                    Me.PGNGame.HalfMoves.TextListString(Nothing) = pNewValue
                Else
                    Me.PGNGame.HalfMoves.TextListString(Me.PGNGame.HalfMoves(Val(pKeyValue))) = pNewValue
                End If
                RaiseEvent HalfMoveChanged(Me.PGNGame, gfrmMoveList.CurrentHalfMove)

            Case "SetupToolbar.Visible"    '(No Key) 
                gfrmBoard.SetupToolbarVisible = (pNewValue = "True")

            Case "StatusBar.Visible"       '(No Key) 
                mnuStatusBar.Checked = (pNewValue = "True")
                stsStatusStrip.Visible = mnuStatusBar.Checked
                frmMainForm_SizeChanged(Nothing, Nothing)

            Case "ColorBoard.Checked"      '(No Key) 
                Call SetColorBoard(pNewValue = "True")

            Case "CurrentLanguage"         '(No Key) 
                CurrentLanguage = Val(pNewValue)
                Call SetLanguage(CurrentLanguage, Me)
                RaiseEvent LanguageChanged(CurrentLanguage)

            Case "MenuLocation"            '(No Key) 
                Me.MenuLocation = Val(pNewValue)

            Case "mnuBoard.Checked"        '(No Key) 
                mnuBoard.Checked = (pNewValue = "True")
                UpdateBoardSubForm()

            Case "mnuMoveList.Checked"     '(No Key) 
                mnuMoveList.Checked = (pNewValue = "True")
                UpdateMoveListSubForm()

            Case "mnuValidMoves.Checked"   '(No Key) 
                mnuValidMoves.Checked = (pNewValue = "True")
                UpdateValidMovesSubForm()

            Case "mnuTitleAndMemo.Checked" '(No Key) 
                mnuTitleAndMemo.Checked = (pNewValue = "True")
                UpdateTitleAndMemoSubForm()

            Case "mnuGameDetails.Checked"   '(No Key) 
                mnuGameDetails.Checked = (pNewValue = "True")
                UpdateGameDetailsSubForm()

            Case "TabPage.Dropped"         '(No Key) 
                Me.DeSerializeLayoutFromString(pNewValue)

            Case "Layout"                  '(No Key) Changes to Panel settings
                Me.DeSerializeLayoutFromString(pNewValue)
        End Select
    End Sub

    Private Function PGNFileModified() As Boolean
        'Test to see if PGN was modified using the Journal-entries
        For Each Entry As JournalEntry In gJournaling.Journal
            If Entry.ClassName Like "*.Index" _
            OrElse Entry.ClassName Like "*.Visible" _
            OrElse Entry.ClassName Like "*.Checked" Then
                Continue For
            End If
            Select Case Entry.ClassName
                Case "Mode", "CurrentLanguage", "TabPage.Dropped", "Layout"
                    Continue For
                Case Else 'Entry referring to PGNFile being modified
                    Return True
            End Select
        Next Entry

        'No entry referring to PGNFile; Not being modified
        Return False
    End Function

    Private Sub frmMainForm_Closing(pSender As Object, pArgs As CancelEventArgs) Handles Me.Closing
        If PGNFileModified() = True Then
            If MsgBox(MessageText("SaveChanges"), MessageBoxButtons.YesNo + MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                mnuSave_Click(Nothing, Nothing)
            End If
        End If

    End Sub

    Protected Overrides Sub Finalize()
        Me.gfrmBoard = Nothing
        Me.gfrmStockfish = Nothing
        Me.gfrmMoveList = Nothing
        Me.gfrmValidMoves = Nothing
        Me.gfrmGameDetails = Nothing
        Me.gfrmTitleAndMemo = Nothing
        Me.gfrmDockCross = Nothing
        Me.gfrmEditGame = Nothing
        Me.gfrmEditTitleAndMemo = Nothing
        Me.gfrmTrainingQuestion = Nothing
        Me.gJournaling = Nothing
        Me.gPGNFile = Nothing
        Me.gPGNGame = Nothing

        MyBase.Finalize()
    End Sub

#End Region

End Class
