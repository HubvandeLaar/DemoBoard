Imports ChessGlobals
Imports ChessMessaging
Imports ChessMaterials

Public Class frmValidMoves

    Private WithEvents gfrmMainForm As frmMainForm

    Public Event ValidMovesSelectionChanged(pSender As Object, pMove As BoardMove)
    Public Event ValidMovesDoubleClick(pSender As Object, pMove As BoardMove)

    'Private gOriginalFEN As String
    Private gMoves As List(Of BoardMove)
    Private gFEN As String

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ApplyLanguageToCurrentForm(Me)
        Application.DoEvents()
    End Sub

    Private Sub gfrmMainForm_ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard) Handles gfrmMainForm.ChessPieceStartMoving
        If gfrmMainForm.Visible = True Then
            If Me.Visible = True Then
                Application.DoEvents()
                Me.UpdateValidMoves(pChessBoard.FEN, pPiece, pFromFieldName)
            End If
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
            Else
                RaiseEvent ValidMovesSelectionChanged(pSender, gMoves(lstValidMoves.SelectedIndex))
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstValidMoves_DoubleClick(pSender As Object, pArgs As EventArgs) Handles lstValidMoves.DoubleClick
        Try
            If lstValidMoves.SelectedIndex = -1 Then
                Exit Sub
            Else
                RaiseEvent ValidMovesDoubleClick(pSender, gMoves(lstValidMoves.SelectedIndex))
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmMainForm_BoardShown(pFEN As String) Handles gfrmMainForm.BoardShown
        gFEN = pFEN
        If Me.Visible = True Then
            Me.UpdateValidMoves(pFEN)
        End If
    End Sub

    Private Sub gfrmMainForm_MouseUp(pSender As Object, pArgs As MouseEventArgs) Handles gfrmMainForm.MouseUp
        If Me.Visible = True Then
            Me.UpdateValidMoves(gFEN)
        End If
    End Sub

    Private Sub frmValidMoves_VisibleChanged(pSender As Object, pArs As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            Me.UpdateValidMoves(gFEN)
        End If
    End Sub

    Private Sub frmValidMoves__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        gMoves = Nothing
        gfrmMainForm = Nothing

        MyBase.Finalize()
    End Sub

End Class