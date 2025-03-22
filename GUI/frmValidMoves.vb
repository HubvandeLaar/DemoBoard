Imports System.ComponentModel
Imports ChessGlobals
Imports ChessMaterials
Imports DemoBoard
Imports PGNLibrary

Public Class frmValidMoves

    Private WithEvents gfrmMainForm As frmMainForm
    Private WithEvents gfrmBoard As frmBoard

    Public Event ValidMovesSelectionChanged(pSender As Object, pMove As BoardMove)

    'Private gOriginalFEN As String
    Private gMoves As List(Of BoardMove)

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
        gfrmBoard = pfrmMainForm.gfrmBoard
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ChangeLanguageCurrentForm(Me)
        UpdateValidMoves(gfrmBoard.FEN)
    End Sub

    Private Sub gfrmMainForm_ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard) Handles gfrmMainForm.ChessPieceStartMoving
        If gfrmMainForm.Visible = True Then
            Me.UpdateValidMoves(pChessBoard.FEN, pPiece, pFromFieldName)
        End If
    End Sub

    Private Sub gfrmMainForm_ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String, pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece) Handles gfrmMainForm.ChessPieceMoved
        If gfrmMainForm.Visible = True Then
            Me.UpdateValidMoves(pChessBoard.FEN)
        End If
    End Sub

    Private Sub UpdateValidMoves(pFEN As String, pPiece As ChessPiece, pFromFieldName As String)
        Dim Board As New ChessBoard(pFEN)
        Board(pFromFieldName).Piece = pPiece
        lstValidMoves.Items.Clear()
        gMoves = pPiece.PossibleMoves(pFromFieldName, Board)
        Me.ListMoves()
    End Sub

    Private Sub UpdateValidMoves(pFEN As String)
        Dim Board As New ChessBoard(pFEN)
        lstValidMoves.Items.Clear()
        gMoves = Board.AllPossibleMoves(Board.ActiveColor)
        Me.ListMoves()
    End Sub

    Private Sub ListMoves()
        For Each Move As BoardMove In gMoves
            If Move.Castle = True Then
                If Strings.Left(Move.ToFieldName, 1) = "g" Then
                    lstValidMoves.Items.Add("O-O")
                ElseIf Strings.Left(Move.ToFieldName, 1) = "c" Then
                    lstValidMoves.Items.Add("O-O-O")
                End If
            Else
                lstValidMoves.Items.Add(Move.Piece.MoveName(CurrentLanguage) & Move.FromFieldName & "-" & Move.ToFieldName & If(Move.PromotionPiece Is Nothing, "", Move.PromotionPiece.MoveName(CurrentLanguage)))
            End If
        Next Move
    End Sub

    Private Sub lstValidMoves_SelectedIndexChanged(pSender As Object, pArgs As EventArgs) Handles lstValidMoves.SelectedIndexChanged
        Try
            If lstValidMoves.SelectedIndex = -1 Then
                RaiseEvent ValidMovesSelectionChanged(pSender, Nothing)
                Exit Sub
            End If
            RaiseEvent ValidMovesSelectionChanged(pSender, gMoves(lstValidMoves.SelectedIndex))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstValidMoves_DoubleClick(pSender As Object, pArgs As EventArgs) Handles lstValidMoves.DoubleClick
        Try
            If lstValidMoves.SelectedIndex = -1 Then Exit Sub
            gfrmBoard.PerformMove(gMoves(lstValidMoves.SelectedIndex))
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmMainForm_FENChanged(pFEN As String) Handles gfrmMainForm.FENChanged
        UpdateValidMoves(pFEN)
    End Sub

    Private Sub frmValidMoves__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        Me.gMoves = Nothing
        Me.gfrmMainForm = Nothing
        Me.gfrmBoard = Nothing

        MyBase.Finalize()
    End Sub
End Class