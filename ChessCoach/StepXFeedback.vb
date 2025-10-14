Option Explicit On

Imports ChessEngine
Imports ChessGlobals.ChessColor
Imports ChessMaterials
Imports PGNLibrary

Public MustInherit Class StepXFeedback

    Private Protected gCurrentHalfMove As PGNHalfMove
    Private Protected gBoardBefore As ChessBoard
    Private Protected gMove As BoardMove
    Private Protected gBoardAfter As ChessBoard
    Private Protected gResults As EngineResults

    MustOverride Function FindErrors(pCurrentHalfMove As PGNHalfMove, pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String

    Protected Sub InsertSubVariant(pMessageText As String, pBoardBefore As ChessBoard, pMove1 As BoardMove, Optional pMove2 As BoardMove = Nothing)
        Dim HalfMoves As PGNHalfMoves = gCurrentHalfMove.gHalfMoves
        Dim Captured As Boolean = pBoardBefore(pMove1.ToFieldName).Piece IsNot Nothing
        Dim HalfMove As New PGNHalfMove(HalfMoves, pBoardBefore,
                                       gCurrentHalfMove.MoveNr, pMove1.Piece, pMove1.FromFieldName, pMove1.ToFieldName,
                                       Captured, pMove1.PromotionPiece, pBoardBefore.FEN)
        HalfMove.VariantLevel = gCurrentHalfMove.VariantLevel + 1
        HalfMove.VariantNumber = gCurrentHalfMove.SubVariants.Count + 1

        Dim Pos As Integer = InStr(pMessageText, "|")
        If Pos = 0 Then 'Character not found
            HalfMove.CommentAfter = New PGNComment(pMessageText)
        Else
            HalfMove.CommentBefore = New PGNComment(Left(pMessageText, Pos - 1))
            HalfMove.CommentAfter = New PGNComment(Mid(pMessageText, Pos + 1))
        End If

        Dim CurrentBoard As New ChessBoard(pBoardBefore.FEN)
        Dim CurrentBoardMove As BoardMove = gCurrentHalfMove.BoardMove(CurrentBoard)
        InsertDiagram(gCurrentHalfMove.PreviousHalfMove(), CurrentBoardMove)

        Dim NextMove As PGNHalfMove = HalfMoves.FindNextHalfMove(gCurrentHalfMove, gCurrentHalfMove.VariantLevel, gCurrentHalfMove.VariantNumber)
        If NextMove Is Nothing Then
            HalfMoves.Add(HalfMove, pRaiseEvent:=False) 'Achteraan toevoegen
        Else
            HalfMoves.InsertBefore(NextMove.Index, HalfMove)
        End If

        If pMove2 Is Nothing Then Exit Sub
        Dim Board As New ChessBoard(pBoardBefore.FEN)
        Dim Captured2 As Boolean = Board(pMove2.ToFieldName).Piece IsNot Nothing
        Dim MoveNr As Integer = If(Board.ActiveColor = WHITE, gCurrentHalfMove.MoveNr + 1, gCurrentHalfMove.MoveNr)
        Board.PerformMove(pMove2)
        Dim HalfMove2 As New PGNHalfMove(HalfMoves, Board,
                                        MoveNr, pMove2.Piece, pMove2.FromFieldName, pMove2.ToFieldName,
                                        Captured2, pMove2.PromotionPiece, Board.FEN)

        Dim Pos2 As Integer = InStr(HalfMove.CommentAfter.Text, "|")
        If Pos2 = 0 Then 'Character not found
            'Leave Comment with first subvariant move
        Else
            HalfMove.CommentAfter = New PGNComment(Left(pMessageText, Pos2 - 1))
            HalfMove2.CommentAfter = New PGNComment(Mid(pMessageText, Pos2 + 1))
        End If

        'NextMove was already determined above
        If NextMove Is Nothing Then
            HalfMoves.Add(HalfMove2, pRaiseEvent:=False) 'Achteraan toevoegen
        Else
            HalfMoves.InsertBefore(NextMove.Index, HalfMove2)
        End If

    End Sub

    Protected Sub InsertComment(pMessageText As String, pCurrentBoardMove As BoardMove)
        If gCurrentHalfMove.CommentAfter Is Nothing Then
            gCurrentHalfMove.CommentAfter = New PGNComment(pMessageText)
        Else
            gCurrentHalfMove.CommentAfter.Text = gCurrentHalfMove.CommentAfter.Text & " (" & pMessageText & ")"
        End If

        InsertDiagram(gCurrentHalfMove.PreviousHalfMove(), pCurrentBoardMove)
    End Sub

    Private Sub InsertDiagram(pHalfMove As PGNHalfMove, pCurrentBoardMove As BoardMove)
        If pHalfMove Is Nothing Then
            Exit Sub
        End If

        If pHalfMove.CommentAfter Is Nothing Then
            pHalfMove.CommentAfter = New PGNComment("[#]")
        Else
            pHalfMove.CommentAfter.Text &= " [#]"
        End If

        If pHalfMove.CommentAfter.ArrowList Is Nothing Then pHalfMove.CommentAfter.ArrowList = New PGNArrowList()
        pHalfMove.CommentAfter.ArrowList.Add(New Arrow("Y", pCurrentBoardMove.FromFieldName, pCurrentBoardMove.ToFieldName))
    End Sub

End Class
