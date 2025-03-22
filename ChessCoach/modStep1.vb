Imports ChessGlobals.modMessages
Imports ChessGlobals.modChessLanguage
Imports ChessGlobals.modChessColor
Imports ChessGlobals.modChessColor.ChessColor
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports ChessEngine

Module modStep1

    Public Function Step1Errors(PBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim Message As String = ""

        'Checks independent of pScore
        Message = IncorrectInitPos(PBoardBefore, pMove, pBoardAfter)
        If Message <> "" Then Return Message
        Message = SameColorTwice(PBoardBefore, pMove, pBoardAfter)
        If Message <> "" Then Return Message
        Message = InvalidCastling(PBoardBefore, pMove, pBoardAfter)
        If Message <> "" Then Return Message
        Message = InCheckAfterMove(PBoardBefore, pMove, pBoardAfter)
        If Message <> "" Then Return Message
        Message = IsInvalidMove(PBoardBefore, pMove, pBoardAfter)
        If Message <> "" Then Return Message

        If pResults.Before.BestMove.FromFieldName = pMove.FromFieldName _
        And pResults.Before.BestMove.FromFieldName = pMove.FromFieldName Then
            'Best Move already played; No comment to make
            Return ""
        End If

        If pResults.Score < -100 Then 'Losing more than a Pawn
            Message = MissedMateIn1(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = UncoverdedPiece(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = NotCoverded(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = TwoFoldAttack(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = TwoFoldAttacked(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = ProfitExchange(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = UnProfitExchange(PBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
        End If

        Return ""
    End Function

    Private Function IncorrectInitPos(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard) As String
        With pBoardBefore
            'Only looking for mistakes at the bottom row, and assuming all pawns are in line
            'Knights kan jump, so they have to be at the bottom line
            If .ActiveColor = WHITE Then
                If .IsPiece(pBoardBefore("a2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("b2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("c2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("d2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("e2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("f2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("g2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(pBoardBefore("h2"), PieceType.PAWN, WHITE) = False _
                Or TwoKnightsAtFirstRow(pBoardBefore, WHITE) = False Then
                    'One of Pawns not in starting position or a Knight not at bottom row,
                    'assuming it's no Starting position...
                    Return ""
                End If
                If .IsPiece(pBoardBefore("a1"), PieceType.ROOK, WHITE) = False _
                Or .IsPiece(pBoardBefore("b1"), PieceType.KNIGHT, WHITE) = False _
                Or .IsPiece(pBoardBefore("c1"), PieceType.BISHOP, WHITE) = False _
                Or .IsPiece(pBoardBefore("d1"), PieceType.QUEEN, WHITE) = False _
                Or .IsPiece(pBoardBefore("e1"), PieceType.KING, WHITE) = False _
                Or .IsPiece(pBoardBefore("f1"), PieceType.BISHOP, WHITE) = False _
                Or .IsPiece(pBoardBefore("g1"), PieceType.KNIGHT, WHITE) = False _
                Or .IsPiece(pBoardBefore("h1"), PieceType.ROOK, WHITE) = False Then
                    Return MessageText("IncorrectInitPos", WHITE.Text)
                Else
                    Return ""  'Correct starting position
                End If

            Else 'Black
                If .IsPiece(pBoardBefore("a7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("b7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("c7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("d7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("e7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("f7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("g7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(pBoardBefore("h7"), PieceType.PAWN, BLACK) = False _
                Or TwoKnightsAtFirstRow(pBoardBefore, BLACK) = False Then
                    'One of Pawns not in starting position or a Knight not at top row,
                    'assuming it's no Starting position...
                    Return ""
                End If
                If .IsPiece(pBoardBefore("a8"), PieceType.ROOK, BLACK) = False _
                Or .IsPiece(pBoardBefore("b8"), PieceType.KNIGHT, BLACK) = False _
                Or .IsPiece(pBoardBefore("c8"), PieceType.BISHOP, BLACK) = False _
                Or .IsPiece(pBoardBefore("d8"), PieceType.QUEEN, BLACK) = False _
                Or .IsPiece(pBoardBefore("e8"), PieceType.KING, BLACK) = False _
                Or .IsPiece(pBoardBefore("f8"), PieceType.BISHOP, BLACK) = False _
                Or .IsPiece(pBoardBefore("g8"), PieceType.KNIGHT, BLACK) = False _
                Or .IsPiece(pBoardBefore("h8"), PieceType.ROOK, BLACK) = False Then
                    Return MessageText("IncorrectInitPos", BLACK.Text)
                Else
                    Return ""  'Correct starting position
                End If

            End If
        End With
    End Function

    Private Function TwoKnightsAtFirstRow(pBoard As ChessBoard, pColor As ChessColor) As Boolean
        Dim Row As Integer = If(pColor = WHITE, 1, 8)
        Dim Knights As Integer = 0
        For Column = 1 To 8
            If pBoard.IsPiece(pBoard(Column, Row), PieceType.KNIGHT, pColor) Then
                Knights += 1
            End If
        Next Column
        If Knights = 2 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function SameColorTwice(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard) As String
        If pBoardBefore.ActiveColor <> pMove.Piece.Color Then
            Return MessageText("SameColorTwice", pMove.Piece.Color.Text)
        Else
            Return ""
        End If
    End Function

    Private Function InvalidCastling(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard) As String
        If pMove.Piece.Type <> PieceType.KING _
        Or Math.Abs(BoardMove.ColumnNr(pMove.FromFieldName) - BoardMove.ColumnNr(pMove.ToFieldName)) < 2 Then
            'No castling
            Return ""
        End If

        'So an intention to Castle (King moved more than one step)
        If pBoardBefore.InCheck(pBoardBefore.ActiveColor) _
        Or pBoardAfter.InCheck(pBoardBefore.ActiveColor) Then
            Return MessageText("InvalidCastling1", pBoardBefore.ActiveColor.Text)
        End If

        Dim Col As Integer = Int((BoardMove.ColumnNr(pMove.FromFieldName) + BoardMove.ColumnNr(pMove.ToFieldName)) / 2)
        Dim ToFieldName As String = BoardMove.ColumnName(Col) & Mid(pMove.FromFieldName, 2, 1)
        Dim Move = New BoardMove(pBoardBefore(pMove.FromFieldName).Piece, pMove.FromFieldName, ToFieldName)
        If pBoardBefore.InCheckAfterMove(Move, pBoardBefore.ActiveColor) = True Then
            'King in check on his way
            Return MessageText("InvalidCastling2", pBoardBefore.ActiveColor.Text)
        End If

        If pBoardBefore(ToFieldName).Piece IsNot Nothing Then
            'Piece in between
            Return MessageText("InvalidCastling3", ToFieldName, pBoardBefore.ActiveColor.Text)
        End If

        If pMove.FromFieldName <> If(pBoardBefore.ActiveColor = WHITE, "e1", "e8") Then
            'Not at correct starting field
            Return MessageText("InvalidCastling4", pBoardBefore.ActiveColor.Text)
        End If

        If pMove.ToFieldName = If(pBoardBefore.ActiveColor = WHITE, "g1", "g8") Then
            If pBoardBefore(If(pBoardBefore.ActiveColor = WHITE, "g1", "g8")).Piece IsNot Nothing Then
                'Piece already on target
                Return MessageText("InvalidCastling5", pMove.ToFieldName, pBoardBefore.ActiveColor.Text)
            End If
        ElseIf pMove.ToFieldName = If(pBoardBefore.ActiveColor = WHITE, "c1", "c8") Then
            If pBoardBefore(If(pBoardBefore.ActiveColor = WHITE, "c1", "c8")).Piece IsNot Nothing Then
                'Piece already on target
                Return MessageText("InvalidCastling6", pMove.ToFieldName, pBoardBefore.ActiveColor.Text)
            End If
        Else
            'Not correct target field
            Return MessageText("InvalidCastling7", pBoardBefore.ActiveColor.Text)
        End If

        If pBoardBefore.ShortCastlingAllowed(pMove.FromFieldName) = False Then
            'Pieces were moved
            Return MessageText("InvalidCastling6", pBoardBefore.ActiveColor.Text)
        ElseIf pBoardBefore.LongCastlingAllowed(pMove.FromFieldName) = False Then
            'Pieces were moved
            Return MessageText("InvalidCastling7", pBoardBefore.ActiveColor.Text)
        End If

        Return ""
    End Function

    Private Function InCheckAfterMove(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard) As String
        If pBoardAfter.InCheck(pBoardBefore.ActiveColor) Then
            Return MessageText("InCheckAfterMove", pBoardBefore.ActiveColor.Text)
        Else
            Return ""
        End If
    End Function

    Private Function IsInvalidMove(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard) As String
        If pMove.Piece.IsValidMove(pBoardBefore, pMove.FromFieldName, pMove.ToFieldName) Then
            Return ""
        Else
            Return MessageText("InvalidMove", pMove.Piece.Name(CurrentLanguage))
        End If
    End Function

    Private Function MissedMateIn1(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        'Assuming the best move also would be checkmate
        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove = New ChessBoard(pBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove)

        If BoardAfterBestMove.CheckMate(pBoardBefore.ActiveColor) Then
            Return MessageText("MissedMateIn1", BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage))
        End If
        Return ""
    End Function

    Private Function UncoverdedPiece(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming the BestMove would have captured the uncoveredpiece
        Dim Field As ChessField = pBoardBefore(pResult.Before.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = pBoardBefore.ActiveColor Then
            Return ""
        End If

        If Field.DefendedBy(pBoardBefore.Opponent).Count = 0 _
        And Field.AttackedBy(pBoardBefore.ActiveColor).Count > 0 Then
            'Opponent's piece; Not being defended; but being attacked once or more 
            Return MessageText("UncoverdedPiece", Field.Name)
        End If

        Return ""
    End Function

    Private Function NotCoverded(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming the next BestMove would capture my uncovered piece 
        Dim Field As ChessField = pBoardBefore(pResult.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = pBoardBefore.Opponent Then
            Return ""
        End If

        If Field.DefendedBy(pBoardBefore.ActiveColor).Count = 0 _
        And Field.AttackedBy(pBoardBefore.Opponent).Count > 0 Then
            'Not being defended bij myself; but being attacked once or more by the opponent 
            Return MessageText("NotCoverded", Field.Name)
        End If

        Return ""
    End Function

    Private Function TwoFoldAttack(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming the Bestmove would capture the Twofold attacked piece
        Dim Field As ChessField = pBoardBefore(pResult.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = pBoardBefore.ActiveColor Then
            Return ""
        End If

        If Field.DefendedBy(pBoardBefore.Opponent).Count = 1 _
        And Field.AttackedBy(pBoardBefore.ActiveColor).Count = 2 Then
            'Opponent's piece; Being defended once; but being attacked twice 
            Return MessageText("TwoFoldAttack", Field.Name)
        End If

        Return ""
    End Function

    Private Function TwoFoldAttacked(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming the next BestMove would capture my TwoFoldAttacked piece 
        Dim Field As ChessField = pBoardBefore(pResult.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = pBoardBefore.Opponent Then
            Return ""
        End If

        If Field.DefendedBy(pBoardBefore.ActiveColor).Count = 1 _
        And Field.AttackedBy(pBoardBefore.Opponent).Count = 2 Then
            'Own Piece; Being defenced once, but being attacked twice by the opponent 
            Return MessageText("TwoFoldAttacked", Field.Name)
        End If

        Return ""
    End Function

    Private Function ProfitExchange(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming Bestmove would capture the piece when it's a Profitable Exchange
        Dim ToField As ChessField = pBoardBefore(pResult.Before.BestMove.ToFieldName)
        If ToField.Piece Is Nothing Then
            Return ""
        End If
        If ToField.Piece.Type = PieceType.PAWN _
        Or ToField.Piece.Color = pBoardBefore.ActiveColor Then
            Return ""
        End If

        Dim FromField As ChessField = pBoardBefore(pResult.Before.BestMove.FromFieldName)
        If FromField.Piece Is Nothing Then
            Return ""
        End If

        If ToField.Piece.Value > FromField.Piece.Value _
        And ToField.DefendedBy(pBoardBefore.Opponent).Count > 0 Then
            'Opponent's piece; Being defended once or more, but with higher value
            Return MessageText("ProfitExchange", ToField.Name)
        End If

        Return ""
    End Function

    Private Function UnProfitExchange(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        'Assuming BestmoveAfter would capture the piece when it's a Profitable Exchange
        Dim ToField As ChessField = pBoardBefore(pResult.After.BestMove.ToFieldName)
        If ToField.Piece Is Nothing Then
            Return ""
        End If
        If ToField.Piece.Type = PieceType.PAWN _
        Or ToField.Piece.Color = pBoardBefore.Opponent Then
            Return ""
        End If

        Dim FromField As ChessField = pBoardBefore(pResult.After.BestMove.FromFieldName)
        If FromField.Piece Is Nothing Then
            Return ""
        End If

        If ToField.Piece.Value > FromField.Piece.Value _
        And ToField.DefendedBy(pBoardBefore.Opponent).Count > 0 Then
            'Opponent's piece; Being defended once or more, but with higher value
            Return MessageText("UnprofitExchange", ToField.Name)
        End If

        Return ""
    End Function

End Module
