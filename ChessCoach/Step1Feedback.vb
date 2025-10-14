Option Explicit On

Imports ChessGlobals.modChessLanguage
Imports ChessGlobals.modChessColor
Imports ChessGlobals.modChessColor.ChessColor
Imports ChessMessaging.Messages
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports ChessEngine
Imports PGNLibrary

Public Class Step1Feedback
    Inherits StepXFeedback

    Public Overrides Function FindErrors(pCurrentHalfMove As PGNHalfMove, pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim Message As String = ""
        gCurrentHalfMove = pCurrentHalfMove
        gBoardBefore = pBoardBefore
        gMove = pMove
        gBoardAfter = pBoardAfter
        gResults = pResults

        'Checks indeMe.endent of Me.Score
        Message = IncorrectInitPos()
        If Message <> "" Then Return Message
        Message = SameColorTwice()
        If Message <> "" Then Return Message
        Message = InvalidCastling()
        If Message <> "" Then Return Message
        Message = InCheckAfterMove()
        If Message <> "" Then Return Message
        Message = IsInvalidMove()
        If Message <> "" Then Return Message

        For PV As Integer = 0 To 2
            With gResults.Before
                If Math.Abs(.EngineVariant(PV).Score - .Score) < 100 Then 'Score in Centipoints
                    If gMove.FromFieldName = .EngineVariant(PV).FirstMove.FromFieldName _
                    And gMove.ToFieldName = .EngineVariant(PV).FirstMove.ToFieldName Then
                        'BestMove, or almost best move was played; No comment to make
                        Return ""
                    End If
                End If
            End With
        Next PV

        If gResults.Score < -100 Then 'Losing more than a Pawn
            Message = MissedMateIn1()
            If Message <> "" Then Return Message
            Message = MissedUncoverdedPiece()
            If Message <> "" Then Return Message
            Message = NotCoverded()
            If Message <> "" Then Return Message
            Message = MissedTwoFoldAttack()
            If Message <> "" Then Return Message
            Message = TwoFoldAttacked()
            If Message <> "" Then Return Message
            Message = MissedProfitExchange()
            If Message <> "" Then Return Message
            Message = UnprofitTwoFoldCapture()
            If Message <> "" Then Return Message
            Message = UnProfitExchange()
            If Message <> "" Then Return Message
            Message = OfferingPieces()
            If Message <> "" Then Return Message
        End If

        Return ""
    End Function

    Private Function IncorrectInitPos() As String
        With gBoardBefore
            'Only looking for mistakes at the bottom row, and assuming all pawns are in line
            'Knights kan jump, so they have to be at the bottom line
            If .ActiveColor = WHITE Then
                If .IsPiece(gBoardBefore("a2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("b2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("c2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("d2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("e2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("f2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("g2"), PieceType.PAWN, WHITE) = False _
                Or .IsPiece(gBoardBefore("h2"), PieceType.PAWN, WHITE) = False _
                Or TwoKnightsAtFirstRow(gBoardBefore, WHITE) = False Then
                    'One of Pawns not in starting position or a Knight not at bottom row,
                    'assuming it's no Starting position...
                    Return ""
                End If
                If .IsPiece(gBoardBefore("a1"), PieceType.ROOK, WHITE) = False _
                Or .IsPiece(gBoardBefore("b1"), PieceType.KNIGHT, WHITE) = False _
                Or .IsPiece(gBoardBefore("c1"), PieceType.BISHOP, WHITE) = False _
                Or .IsPiece(gBoardBefore("d1"), PieceType.QUEEN, WHITE) = False _
                Or .IsPiece(gBoardBefore("e1"), PieceType.KING, WHITE) = False _
                Or .IsPiece(gBoardBefore("f1"), PieceType.BISHOP, WHITE) = False _
                Or .IsPiece(gBoardBefore("g1"), PieceType.KNIGHT, WHITE) = False _
                Or .IsPiece(gBoardBefore("h1"), PieceType.ROOK, WHITE) = False Then
                    InsertComment(MessageText("IncorrectInitPos", WHITE.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
                    Return MessageText("IncorrectInitPos", WHITE.Text)
                Else
                    Return ""  'Correct starting position
                End If

            Else 'Black
                If .IsPiece(gBoardBefore("a7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("b7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("c7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("d7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("e7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("f7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("g7"), PieceType.PAWN, BLACK) = False _
                Or .IsPiece(gBoardBefore("h7"), PieceType.PAWN, BLACK) = False _
                Or TwoKnightsAtFirstRow(gBoardBefore, BLACK) = False Then
                    'One of Pawns not in starting position or a Knight not at top row,
                    'assuming it's no Starting position...
                    Return ""
                End If
                If .IsPiece(gBoardBefore("a8"), PieceType.ROOK, BLACK) = False _
                Or .IsPiece(gBoardBefore("b8"), PieceType.KNIGHT, BLACK) = False _
                Or .IsPiece(gBoardBefore("c8"), PieceType.BISHOP, BLACK) = False _
                Or .IsPiece(gBoardBefore("d8"), PieceType.QUEEN, BLACK) = False _
                Or .IsPiece(gBoardBefore("e8"), PieceType.KING, BLACK) = False _
                Or .IsPiece(gBoardBefore("f8"), PieceType.BISHOP, BLACK) = False _
                Or .IsPiece(gBoardBefore("g8"), PieceType.KNIGHT, BLACK) = False _
                Or .IsPiece(gBoardBefore("h8"), PieceType.ROOK, BLACK) = False Then
                    InsertComment(MessageText("IncorrectInitPos", BLACK.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
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

    Private Function SameColorTwice() As String
        If gBoardBefore.ActiveColor <> gMove.Piece.Color Then
            InsertComment(MessageText("SameColorTwice", gMove.Piece.Color.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("SameColorTwice", gMove.Piece.Color.Text)
        Else
            Return ""
        End If
    End Function

    Private Function InvalidCastling() As String
        If gMove.Piece.Type <> PieceType.KING _
        Or Math.Abs(BoardMove.ColumnNr(gMove.FromFieldName) - BoardMove.ColumnNr(gMove.ToFieldName)) < 2 Then
            'No castling
            Return ""
        End If

        'So an intention to Castle (King moved more than one step)
        If gBoardBefore.InCheck(gBoardBefore.ActiveColor) _
        Or gBoardAfter.InCheck(gBoardBefore.ActiveColor) Then
            InsertComment(MessageText("InvalidCastling1", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidCastling1", gBoardBefore.ActiveColor.Text)
        End If

        Dim Col As Integer = Int((BoardMove.ColumnNr(gMove.FromFieldName) + BoardMove.ColumnNr(gMove.ToFieldName)) / 2)
        Dim ToFieldName As String = BoardMove.ColumnName(Col) & Mid(gMove.FromFieldName, 2, 1)
        Dim Move As New BoardMove(gBoardBefore(gMove.FromFieldName).Piece, gMove.FromFieldName, ToFieldName)
        If gBoardBefore.InCheckAfterMove(Move, gBoardBefore.ActiveColor) = True Then
            'King in check on his way
            InsertComment(MessageText("InvalidCastling2", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidCastling2", gBoardBefore.ActiveColor.Text)
        End If

        If gBoardBefore(ToFieldName).Piece IsNot Nothing Then
            'Piece in between
            InsertComment(MessageText("InvalidCastling3", ToFieldName, gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidCastling3", ToFieldName, gBoardBefore.ActiveColor.Text)
        End If

        If gMove.FromFieldName <> If(gBoardBefore.ActiveColor = WHITE, "e1", "e8") Then
            'Not at correct starting field
            InsertComment(MessageText("InvalidCastling4", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidCastling4", gBoardBefore.ActiveColor.Text)
        End If

        If gMove.ToFieldName = If(gBoardBefore.ActiveColor = WHITE, "g1", "g8") Then 'King's Side
            If gBoardBefore(If(gBoardBefore.ActiveColor = WHITE, "g1", "g8")).Piece IsNot Nothing Then
                'Piece already on target
                InsertComment(MessageText("InvalidCastling5", gMove.ToFieldName, gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("InvalidCastling5", gMove.ToFieldName, gBoardBefore.ActiveColor.Text)
            End If
            If gBoardBefore.ShortCastlingAllowed(gMove.FromFieldName) = False Then
                'Pieces were moved
                InsertComment(MessageText("InvalidCastling6b", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("InvalidCastling6b", gBoardBefore.ActiveColor.Text)
            End If
        ElseIf gMove.ToFieldName = If(gBoardBefore.ActiveColor = WHITE, "c1", "c8") Then 'Queen's Side
            If gBoardBefore(If(gBoardBefore.ActiveColor = WHITE, "c1", "c8")).Piece IsNot Nothing Then
                'Piece already on target
                InsertComment(MessageText("InvalidCastling6", gMove.ToFieldName, gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("InvalidCastling6", gMove.ToFieldName, gBoardBefore.ActiveColor.Text)
            End If
            If gBoardBefore.LongCastlingAllowed(gMove.FromFieldName) = False Then
                'Pieces were moved
                InsertComment(MessageText("InvalidCastling7", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("InvalidCastling7", gBoardBefore.ActiveColor.Text)
            End If
        Else
            'Not correct target field
            InsertComment(MessageText("InvalidCastling7", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidCastling7", gBoardBefore.ActiveColor.Text)
        End If

        Return ""
    End Function

    Private Function InCheckAfterMove() As String
        If gBoardAfter.InCheck(gBoardBefore.ActiveColor) Then
            InsertComment(MessageText("InCheckAfterMove", gBoardBefore.ActiveColor.Text), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InCheckAfterMove", gBoardBefore.ActiveColor.Text)
        Else
            Return ""
        End If
    End Function

    Private Function IsInvalidMove() As String
        If gMove.Piece.IsValidMove(gBoardBefore, gMove.FromFieldName, gMove.ToFieldName) Then
            Return ""
        Else
            InsertComment(MessageText("InvalidMove", gMove.Piece.Name(CurrentLanguage)), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("InvalidMove", gMove.Piece.Name(CurrentLanguage))
        End If
    End Function

    Private Function MissedMateIn1() As String
        'Assuming the best move also would be checkmate
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove As New ChessBoard(gBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove)

        If BoardAfterBestMove.CheckMate(gBoardBefore.ActiveColor) Then
            InsertSubVariant(MessageText("MissedMateIn1", "|"), gBoardBefore, BestMove)
            Return MessageText("MissedMateIn1", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
        End If
        Return ""
    End Function

    Private Function MissedUncoverdedPiece() As String
        'Assuming the BestMove would have captured the uncoveredpiece
        Dim Field As ChessField = gBoardBefore(gResults.Before.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = gBoardBefore.ActiveColor Then
            Return ""
        End If

        If Field.DefendedBy(gBoardBefore.Opponent).Count = 0 _
        And Field.AttackedBy(gBoardBefore.ActiveColor).Count > 0 Then
            'Opponent's piece; Not being defended; but being attacked once or more 
            InsertComment(MessageText("MissedUncoverdedPiece", Field.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("MissedUncoverdedPiece", Field.Name)
        End If

        Return ""
    End Function

    Private Function NotCoverded() As String
        'Assuming the next BestMove would capture my uncovered piece 
        Dim Field As ChessField = gBoardBefore(gResults.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = gBoardBefore.Opponent Then
            Return ""
        End If

        If Field.DefendedBy(gBoardBefore.ActiveColor).Count = 0 _
        And Field.AttackedBy(gBoardBefore.Opponent).Count > 0 Then
            'Not being defended bij myself; but being attacked once or more by the opponent 
            InsertComment(MessageText("NotCoverded", Field.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("NotCoverded", Field.Name)
        End If

        Dim FromField As ChessField = gBoardBefore(gResults.After.BestMove.FromFieldName)
        If Field.Piece.Value > FromField.Piece.Value Then
            InsertComment(MessageText("ThreatUnprofitExchange", Field.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("ThreatUnprofitExchange", Field.Name)
        End If

        Return ""
    End Function

    Private Function MissedTwoFoldAttack() As String
        'Assuming the Bestmove would capture the Twofold attacked piece
        Dim Field As ChessField = gBoardBefore(gResults.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = gBoardBefore.ActiveColor Then
            Return ""
        End If

        If gMove.ToFieldName = Field.Name Then
            'Player captures with other piece
            Dim BestPiece As ChessPiece = gBoardBefore(gResults.After.BestMove.ToFieldName).Piece
            Dim MovePiece As ChessPiece = gBoardBefore(gMove.FromFieldName).Piece
            If BestPiece.Value < MovePiece.Value Then
                InsertComment(MessageText("CaptureWithLowerPiece", Field.Name, BestPiece.Name(CurrentLanguage), gResults.After.BestMove.FromFieldName), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("CaptureWithLowerPiece", Field.Name, BestPiece.Name(CurrentLanguage), gResults.After.BestMove.FromFieldName)
            Else
                Return ""
            End If
        End If

        If Field.DefendedBy(gBoardBefore.Opponent).Count = 1 _
        And Field.AttackedBy(gBoardBefore.ActiveColor).Count = 2 Then
            'Opponent's piece; Being defended once; but being attacked twice 
            InsertComment(MessageText("MissedTwoFoldAttack", Field.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("MissedTwoFoldAttack", Field.Name)
        End If

        Return ""
    End Function

    Private Function TwoFoldAttacked() As String
        'Assuming the next BestMove would capture my TwoFoldAttacked piece 
        Dim Field As ChessField = gBoardBefore(gResults.After.BestMove.ToFieldName)
        If Field.Piece Is Nothing Then
            Return ""
        End If
        If Field.Piece.Type = PieceType.PAWN _
        Or Field.Piece.Color = gBoardBefore.Opponent Then
            Return ""
        End If

        If Field.DefendedBy(gBoardBefore.ActiveColor).Count = 1 _
        And Field.AttackedBy(gBoardBefore.Opponent).Count = 2 Then
            'Own Piece; Being defenced once, but being attacked twice by the opponent 
            InsertComment(MessageText("TwoFoldAttacked", Field.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("TwoFoldAttacked", Field.Name)
        End If

        Return ""
    End Function

    Private Function MissedProfitExchange() As String
        'Assuming Bestmove would capture the piece when it's a Profitable Exchange
        Dim ToField As ChessField = gBoardBefore(gResults.Before.BestMove.ToFieldName)
        If ToField.Piece Is Nothing Then
            Return ""
        End If
        If ToField.Piece.Type = PieceType.PAWN _
        Or ToField.Piece.Color = gBoardBefore.ActiveColor Then
            Return ""
        End If

        Dim FromField As ChessField = gBoardBefore(gResults.Before.BestMove.FromFieldName)
        If FromField.Piece Is Nothing Then
            Return ""
        End If

        If ToField.Piece.Value > FromField.Piece.Value _
        And ToField.DefendedBy(gBoardBefore.Opponent).Count > 0 Then
            'Opponent's piece; Being defended once or more, but with higher value
            InsertComment(MessageText("MissedProfitExchange", ToField.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("MissedProfitExchange", ToField.Name)
        End If

        Return ""
    End Function

    Private Function UnprofitTwoFoldCapture() As String
        With gBoardBefore
            Dim Field As ChessField = gBoardBefore(gResults.After.BestMove.ToFieldName)

            If Field.Piece Is Nothing Then
                Return ""
            End If
            If Field.Piece.Value >= gMove.Piece.Value Then
                'A profitable exchange or equal exchange
                Return ""
            End If
            Dim Attackers As Long = Field.AttackedBy(gBoardBefore.ActiveColor).Count
            Dim Defenders As Long = Field.DefendedBy(gBoardBefore.Opponent).Count
            If Attackers < 2 Then Return ""
            If Attackers <= Defenders Then
                'Capture of a piece, Being defenced suitable 
                InsertComment(MessageText("UnprofitTwoFoldCapture", Field.Piece.Name(CurrentLanguage), Field.Name, Attackers, Defenders), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("UnprofitTwoFoldCapture", Field.Piece.Name(CurrentLanguage), Field.Name, Attackers, Defenders)
            End If

            Return ""
        End With
    End Function

    Private Function UnProfitExchange() As String
        'Assuming Me.Move did capture the piece when it's a UnProfitable Exchange
        Dim ToField As ChessField = gBoardBefore(gMove.ToFieldName)
        If ToField.Piece Is Nothing Then
            Return ""
        End If
        If gMove.Piece.Type = PieceType.PAWN Then
            Return ""
        End If

        If ToField.Piece.Value < gMove.Piece.Value Then
            InsertComment(MessageText("UnprofitExchange", ToField.Name), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("UnprofitExchange", ToField.Name)
        End If

        Return ""
    End Function

    Private Function OfferingPieces() As String
        'When it would be a good offer, Stockfish should have rated it higer
        Dim Field As ChessField = gBoardAfter(gMove.ToFieldName)
        Dim Attackers As List(Of ChessField) = Field.AttackedBy(gBoardAfter.ActiveColor) 'Attacked by opponent

        'Look if opponent can capture the piece with a lower piece
        For Each Attacker As ChessField In Attackers
            If Attacker.Piece IsNot Nothing Then
                If Attacker.Piece.Value < Field.Piece.Value Then
                    'Offering Profitable Exchange
                    InsertComment(MessageText("OfferingPieces", Field.Name, Attacker.Piece.Name(CurrentLanguage)), gCurrentHalfMove.BoardMove(gBoardBefore))
                    Return MessageText("OfferingPieces", Field.Name, Attacker.Piece.Name(CurrentLanguage))
                End If
            End If
        Next Attacker

        Dim FieldBefore As ChessField = gBoardBefore(gMove.ToFieldName)
        If FieldBefore.Piece IsNot Nothing _
        AndAlso FieldBefore.Piece.Value >= Field.Piece.Value Then
            'This move is a capture or equal exchange; No comment to make
            Return ""
        End If

        'Look if opponent can capture the piece, because it's insufficient defended
        If Field.DefendedBy(gBoardAfter.Opponent).Count < Attackers.Count Then
            InsertComment(MessageText("OfferingPieces2", Field.Name, Field.Piece.Name(CurrentLanguage)), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("OfferingPieces2", Field.Name, Field.Piece.Name(CurrentLanguage))
        End If

        Return ""
    End Function

End Class
