Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessMaterials
Imports ChessMessaging
Imports ChessMessaging.Messages
Imports PGNLibrary

Public Class frmBoard

    Private WithEvents gfrmMainForm As frmMainForm

    Public Event FENChanged(pFEN As String) 'By User-triggered changes on the Board
    Public Event MovePlayed(pHalfMove As PGNHalfMove)
    Public Event ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard)
    Public Event ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                                 pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pFEN As String, pFENBeforeDragging As String)
    Public Event FieldMarkerListChanged(pHalfMove As PGNHalfMove, pMarkerString As String)
    Public Event ArrowListChanged(pHalfMove As PGNHalfMove, pArrowString As String)
    Public Event TextListChanged(pHalfMove As PGNHalfMove, pTextString As String)
    Public Event BoardShown(pFEN As String)

    Public Property CurrentFieldName As String 'Needed to save Current Field when Right-Click menu is shown
    Public Property CurrentHalfMove As PGNHalfMove 'Being updated by MoveList PositionChanged

    Public Property FEN As String
        Set(pFEN As String)
            ctlBoard.FEN = pFEN
        End Set
        Get
            Return ctlBoard.FEN
        End Get
    End Property

    Public Property MarkerString As String
        Set(pFieldMarkerString As String)
            ctlBoard.MarkerString = pFieldMarkerString
        End Set
        Get
            Return ctlBoard.MarkerString
        End Get
    End Property

    Public Property ArrowString As String
        Set(pArrowString As String)
            ctlBoard.ArrowString = pArrowString
        End Set
        Get
            Return ctlBoard.ArrowString
        End Get
    End Property

    Public Property TextString As String
        Set(pTextString As String)
            ctlBoard.TextString = pTextString
        End Set
        Get
            Return ctlBoard.TextString
        End Get
    End Property

    Public Property SwitchSides As Boolean
        Set(pSwitchSides As Boolean)
            ctlBoard.SwitchedSides = pSwitchSides
        End Set
        Get
            Return ctlBoard.SwitchedSides
        End Get
    End Property

    Public Property SetupToolbarVisible As String
        Set(pSetupToolbarVisible As String)
            ctlBoard.SetupToolbarVisible = pSetupToolbarVisible
        End Set
        Get
            Return ctlBoard.SetupToolbarVisible
        End Get
    End Property

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
    End Sub

    Public Sub SaveAsJPG(pFileName As String)
        Call ctlBoard.SaveAsJPG(pFileName)
    End Sub

    ''' <summary>Return the BitMap created by ctlBoard</summary>
    Private Function getBitMap() As Bitmap
        Return ctlBoard.getBitMap
    End Function

    Private Sub ctlBoard_ActiveColorChanged(pSender As Object, pActiveColor As ChessColor, pChessBoard As ChessBoard) Handles ctlBoard.ActiveColorChanged
        Try
            RaiseEvent FENChanged(ctlBoard.FEN)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    ''' <summary>When a piece has been moved from the SetupToolBar on to the board</summary>
    Private Sub ctlBoard_NewChessPiece(pPiece As ChessPiece, pToFieldName As String, pChessBoard As ChessBoard) Handles ctlBoard.NewChessPiece
        Try
            RaiseEvent FENChanged(ctlBoard.FEN)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ctlBoard_ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard) Handles ctlBoard.ChessPieceStartMoving
        RaiseEvent ChessPieceStartMoving(pPiece, pFromFieldName, pChessBoard)
    End Sub

    ''' <summary>When a piece has been moved on the board</summary>
    ''' <param name="pChessBoard">NB board contains position after the move !!!</param>
    Private Sub ctlBoard_ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                                         pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pFEN As String, pFENBeforeDragging As String) Handles ctlBoard.ChessPieceMoved
        Try
            RaiseEvent ChessPieceMoved(pPiece, pFromFieldName, pToFieldName, pChessBoard, pCaptured, pPromotionPiece, pFEN, pFENBeforeDragging)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
            ctlBoard.FEN = ctlBoard.FENBeforeDragging
        End Try
    End Sub

    Private Sub ctlBoard_ChessPieceRemoved(pSender As Object, pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard) Handles ctlBoard.ChessPieceRemoved
        Try
            RaiseEvent FENChanged(ctlBoard.FEN)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
            ctlBoard.FEN = ctlBoard.FENBeforeDragging
        End Try
    End Sub

    ''' <summary>Is Called by frmValidMoves after selecting one of the moves</summary>
    Public Sub PerformMove(pBoardMove As BoardMove)
        Try
            Dim Captured As Boolean
            Dim Board As New ChessBoard(ctlBoard.FEN)
            Captured = (Board(pBoardMove.ToFieldName).Piece IsNot Nothing)

            RaiseEvent ChessPieceMoved(pBoardMove.Piece, pBoardMove.FromFieldName, pBoardMove.ToFieldName,
                                       Board, Captured, pBoardMove.PromotionPiece, ctlBoard.FEN, ctlBoard.FENBeforeDragging)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Public Sub PlayMoves(pMovesToPlay As List(Of PGNHalfMove))
        For Each HalfMove As PGNHalfMove In pMovesToPlay
            If HalfMove.CommentBefore IsNot Nothing _
            AndAlso HalfMove.CommentBefore.Text <> "" Then
                MsgBox(HalfMove.CommentBefore.Text, MsgBoxStyle.OkOnly, MessageText("CommentBefore"))
            End If

            Me.PlayMove(HalfMove)

            If HalfMove.CommentAfter IsNot Nothing Then
                If HalfMove.CommentAfter.Text <> "" Then
                    MsgBox(HalfMove.CommentAfter.Text, MsgBoxStyle.OkOnly, MessageText("CommentAfter"))
                Else
                    'No Comment that causes delay
                    If (HalfMove.CommentAfter.MarkerList IsNot Nothing _
                    Or HalfMove.CommentAfter.ArrowList IsNot Nothing _
                    Or HalfMove.CommentAfter.TextList IsNot Nothing) Then
                        Wait(2000) 'Time to look at graphicals
                    End If
                End If
            End If
            Wait(200)
        Next HalfMove
    End Sub

    Public Sub PlayMove(pHalfMove As PGNHalfMove)
        ctlBoard.PlayMove(pHalfMove)

        RaiseEvent MovePlayed(pHalfMove)
    End Sub

    Private Sub ctlBoard_FieldMarkerListChanged(pSender As Object, pMarkerString As String) Handles ctlBoard.FieldMarkerListChanged
        Try
            RaiseEvent FieldMarkerListChanged(CurrentHalfMove, pMarkerString)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ctlBoard_ArrowListChanged(pSender As Object, pArrowString As String) Handles ctlBoard.ArrowListChanged
        Try
            RaiseEvent ArrowListChanged(CurrentHalfMove, pArrowString)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ctlBoard_TextListChanged(pSender As Object, pTextString As String) Handles ctlBoard.TextListChanged
        Try
            RaiseEvent TextListChanged(CurrentHalfMove, pTextString)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub CtlBoard_MouseRightClickOnField(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs, pFieldName As String) Handles ctlBoard.MouseRightClickOnField
        Try
            Me.mnuFieldMenuUpdate(pSender, pArgs, pFieldName)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    ''' <summary>Called from CtlBoard_MouseRightClickOnField</summary>
    Private Sub mnuFieldMenuUpdate(pSender As Object, pArgs As MouseEventArgs, pFieldName As String)
        Dim MenuItem As ToolStripItem, Piece As ChessPiece, Image As Image

        Try
            mnuFieldMenu.Items.Clear()

            Piece = ctlBoard.getPiece(pFieldName)
            If Piece IsNot Nothing Then
                Image = frmImages.getImage(Piece.IconName)
                MenuItem = mnuFieldMenu.Items.Add(MessageText("DeletePiece", Piece.Name(CurrentLanguage)), Image)
                MenuItem.Tag = pFieldName
            End If

            AddPieceMenuItem(mnuFieldMenu, New King(WHITE), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Queen(WHITE), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Rook(WHITE), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Bishop(WHITE), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Knight(WHITE), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Pawn(WHITE), pFieldName)

            AddPieceMenuItem(mnuFieldMenu, New King(BLACK), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Queen(BLACK), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Rook(BLACK), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Bishop(BLACK), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Knight(BLACK), pFieldName)
            AddPieceMenuItem(mnuFieldMenu, New Pawn(BLACK), pFieldName)

            Image = frmImages.getImage(New Text("G").IconName)
            MenuItem = mnuFieldMenu.Items.Add(MessageText("AddText"), Image)
            MenuItem.Tag = pFieldName

            'Markers
            For Each Marker As Marker In New PGNMarkerList(ctlBoard.MarkerString)
                If Marker.FieldName = pFieldName Then
                    Image = frmImages.getImage(Marker.IconName)
                    MenuItem = mnuFieldMenu.Items.Add(MessageText("DeleteMarker", pFieldName), Image)
                    MenuItem.Tag = Marker
                End If
            Next Marker

            'Arrows
            For Each Arrow As Arrow In New PGNArrowList(ctlBoard.ArrowString)
                If Arrow.FromFieldName = pFieldName _
                Or Arrow.ToFieldName = pFieldName Then
                    Image = frmImages.getImage(Arrow.IconName)
                    MenuItem = mnuFieldMenu.Items.Add(MessageText("DeleteArrow", Arrow.FromFieldName, Arrow.ToFieldName), Image)
                    MenuItem.Tag = Arrow
                End If
            Next Arrow

            'Texts
            For Each Text As Text In New PGNTextList(ctlBoard.TextString)
                If Text.FieldName = pFieldName Then
                    Image = frmImages.getImage(Text.IconName)
                    MenuItem = mnuFieldMenu.Items.Add(MessageText("DeleteText", Text.Text, pFieldName), Image)
                    MenuItem.Tag = Text
                End If
            Next Text

            mnuFieldMenu.Show(MousePosition)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    ''' <summary>Called from mnuFieldMenuUpdate</summary>
    Private Sub AddPieceMenuItem(pFieldMenu As System.Windows.Forms.ContextMenuStrip, pPiece As ChessPiece, pFieldName As String)
        Try
            Dim MenuItem As ToolStripItem, Image As Image
            mnuFieldMenu.Hide()
            Image = frmImages.getImage(pPiece.IconName)
            MenuItem = pFieldMenu.Items.Add(MessageText("AddPiece", pPiece.FullName(CurrentLanguage)), Image)
            MenuItem.Tag = pPiece

            CurrentFieldName = pFieldName

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuFieldMenu_ItemClicked(pSender As Object, pArgs As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuFieldMenu.ItemClicked
        Try
            mnuFieldMenu.Hide()
            If pArgs.ClickedItem.Text = MessageText("AddText") Then
                Using frmAddText = New frmAddText()
                    frmAddText.TextColor = ctlBoard.SetupToolbar.MarkerColor
                    frmAddText.ShowDialog()
                    If frmAddText.OKPressed = True Then
                        Dim TextList As New PGNTextList(ctlBoard.TextString) From
                        {New Text(frmAddText.TextColor, pArgs.ClickedItem.Tag, frmAddText.txtText.Text)}
                        ctlBoard.TextString = TextList.ListString
                        RaiseEvent TextListChanged(CurrentHalfMove, TextList.ListString)
                    End If
                End Using
            ElseIf TypeOf pArgs.ClickedItem.Tag Is ChessPiece Then
                ctlBoard.AddPiece(pArgs.ClickedItem.Tag, CurrentFieldName)
                RaiseEvent FENChanged(ctlBoard.FEN)
            ElseIf TypeOf pArgs.ClickedItem.Tag Is Marker Then
                Dim FieldMarkerList As New PGNMarkerList(ctlBoard.MarkerString)
                FieldMarkerList.Remove(pArgs.ClickedItem.Tag)
                ctlBoard.MarkerString = FieldMarkerList.ListString
                RaiseEvent FieldMarkerListChanged(CurrentHalfMove, FieldMarkerList.ListString)
            ElseIf TypeOf pArgs.ClickedItem.Tag Is Arrow Then
                Dim Arrowlist As New PGNArrowList(ctlBoard.ArrowString)
                Arrowlist.Remove(pArgs.ClickedItem.Tag)
                ctlBoard.ArrowString = Arrowlist.ListString
                RaiseEvent ArrowListChanged(CurrentHalfMove, Arrowlist.ListString)
            ElseIf TypeOf pArgs.ClickedItem.Tag Is Text Then
                Dim TextList As New PGNTextList(ctlBoard.TextString)
                TextList.Remove(pArgs.ClickedItem.Tag)
                ctlBoard.TextString = TextList.ListString
                RaiseEvent TextListChanged(CurrentHalfMove, TextList.ListString)
            ElseIf pArgs.ClickedItem.Text Like MessageText("DeletePiece", "*") Then 'Where place of piece should come, now comes a "*"  
                ctlBoard.DeletePiece(pArgs.ClickedItem.Tag)
                RaiseEvent FENChanged(ctlBoard.FEN)
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    'gfrmMainForm Events ======================================

    Private Sub gfrmMainForm_ColorBoardChanged(pIsColorBoard As Boolean) Handles gfrmMainForm.ColorBoardChanged
        ctlBoard.ColorBoard = pIsColorBoard
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNGame) Handles gfrmMainForm.GameChanged
        CurrentHalfMove = pPGNGame.HalfMoves.CurrentHalfMove
        Me.ShowBoard(pPGNGame, CurrentHalfMove)
    End Sub

    Private Sub gfrmMainForm_MoveListPositionChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove) Handles gfrmMainForm.MoveListPositionChanged
        CurrentHalfMove = pCurrentHalfMove
        Me.ShowBoard(pPGNGame, pCurrentHalfMove)
    End Sub

    Private Sub gfrmMainForm_HalfMoveChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove) Handles gfrmMainForm.HalfMoveChanged
        CurrentHalfMove = pCurrentHalfMove
        Me.ShowBoard(pPGNGame, pCurrentHalfMove)
    End Sub

    Private Sub gfrmMainForm_ValidMovesSelectionChanged(pSender As Object, pMove As BoardMove) Handles gfrmMainForm.ValidMovesSelectionChanged
        If pMove Is Nothing Then Exit Sub
        Dim Brush As New SolidBrush(Color.LightBlue)
        ctlBoard.DrawArrow(Brush, pMove.FromFieldName, pMove.ToFieldName)
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ApplyLanguageToCurrentForm(Me)
    End Sub

    Private Sub gfrmMainForm_BoardKeyDown(pMsg As Message, pKeyData As Keys) Handles gfrmMainForm.BoardKeyDown
        Call ctlBoard.KeyEntered(pMsg, pKeyData)
    End Sub

    Private Sub ShowBoard(pPGNGame As PGNGame, Optional pCurrentHalfMove As PGNHalfMove = Nothing)
        Dim FEN As String
        If pCurrentHalfMove Is Nothing Then
            FEN = pPGNGame.FEN()
            ctlBoard.FEN = FEN
        Else
            FEN = pPGNGame.FEN(pCurrentHalfMove)
            ctlBoard.FEN = FEN
        End If
        ctlBoard.MarkerString = pPGNGame.HalfMoves.MarkerListString(pCurrentHalfMove)
        ctlBoard.ArrowString = pPGNGame.HalfMoves.ArrowListString(pCurrentHalfMove)
        ctlBoard.TextString = pPGNGame.HalfMoves.TextListString(pCurrentHalfMove)

        RaiseEvent BoardShown(FEN)
    End Sub

    Private Sub ctlBoard_MoveAborted(pFEN As String) Handles ctlBoard.MoveAborted
        RaiseEvent BoardShown(FEN) 'To update ValidMoves with previous position
    End Sub

    Private Sub frmBoard_Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        gfrmMainForm = Nothing
        CurrentHalfMove = Nothing

        MyBase.Finalize()
    End Sub

End Class
