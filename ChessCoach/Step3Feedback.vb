Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessMessaging.Messages
Imports ChessEngine
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports PGNLibrary

Public Class Step3Feedback
    Inherits StepXFeedback

    Public Overrides Function FindErrors(pCurrentHalfMove As PGNHalfMove, pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim Message As String = ""
        gCurrentHalfMove = pCurrentHalfMove
        gBoardBefore = pBoardBefore
        gMove = pMove
        gBoardAfter = pBoardAfter
        gResults = pResults

        For PV As Integer = 0 To 2
            With gResults.Before
                If Math.Abs(.EngineVariant(PV).Score - .Score) < 50 Then 'Score in Centipoints
                    If gMove.FromFieldName = .EngineVariant(PV).FirstMove.FromFieldName _
                    And gMove.ToFieldName = .EngineVariant(PV).FirstMove.ToFieldName Then
                        'BestMove, or almost best move was played; No comment to make
                        Return ""
                    End If
                End If
            End With
        Next PV

        If gResults.Score < -100 Then 'Losing more than a Pawn
            Message = MissedFinishingOpening()
            If Message <> "" Then Return Message
            Message = DiscoveredCheck()
            If Message <> "" Then Return Message
            Message = DoubleCheck()
            If Message <> "" Then Return Message
            Message = AttackOnPinnedPiece()
            If Message <> "" Then Return Message
            Message = PinnedPieceIsBadDefender()
            If Message <> "" Then Return Message
            Message = DefendAgainstPinning()
            If Message <> "" Then Return Message
        End If
        Return ""
    End Function

    Private Function MissedFinishingOpening() As String
        Dim Message As String = ""

        If gResults.Before.BestMove.FromFieldName = gMove.FromFieldName _
        And gResults.Before.BestMove.ToFieldName = gMove.ToFieldName Then
            'Best Move already played; No comment to make
            Return ""
        End If

        'See if Opening is finished...
        If ReportOpeningErrorsNow() = False Then
            Return ""
        End If

        Dim UnDevPieces As Integer = UndevelopedPieces()
        If UnDevPieces > 1 Then
            InsertComment(MessageText("UndevelopedPieces", UnDevPieces), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("UndevelopedPieces", UnDevPieces)
        End If

        If gBoardBefore.ActiveColor = WHITE Then
            If gBoardBefore("e1").IsPiece(PieceType.KING, WHITE) Then
                InsertComment(MessageText("KingNotSafe"), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("KingNotSafe")
            End If
        Else
            If gBoardBefore("e8").IsPiece(PieceType.KING, BLACK) Then
                InsertComment(MessageText("KingNotSafe"), gCurrentHalfMove.BoardMove(gBoardBefore))
                Return MessageText("KingNotSafe")
            End If
        End If

        If PawnAtCenter() = False Then
            InsertComment(MessageText("NoPawnAtCenter"), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("NoPawnAtCenter")
        End If

        If RooksConnected() = False Then
            InsertComment(MessageText("RooksNotConnected"), gCurrentHalfMove.BoardMove(gBoardBefore))
            Return MessageText("RooksNotConnected")
        End If

        Return Message
    End Function

    Private Function ReportOpeningErrorsNow() As Boolean
        Static WhiteReported As Boolean = False, BlackReported As Boolean = False  'To avoid messages are shown twice 
        If gBoardBefore.MoveNr < 12 Then Return False
        If gBoardBefore.ActiveColor = WHITE Then
            If WhiteReported = True Then Return False
            WhiteReported = True
        Else  'Black
            If BlackReported = True Then Return False
            BlackReported = True
        End If
        Return True
    End Function

    Private Function UndevelopedPieces() As String
        Dim NbrOfUndevelopedPieces As Integer = 0
        If gBoardBefore.ActiveColor = WHITE Then
            If gBoardBefore("a1").IsPiece(PieceType.ROOK, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("b1").IsPiece(PieceType.KNIGHT, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("c1").IsPiece(PieceType.BISHOP, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("d1").IsPiece(PieceType.QUEEN, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("f1").IsPiece(PieceType.BISHOP, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("g1").IsPiece(PieceType.KNIGHT, WHITE) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("h1").IsPiece(PieceType.ROOK, WHITE) Then NbrOfUndevelopedPieces += 1
        Else
            If gBoardBefore("a8").IsPiece(PieceType.ROOK, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("b8").IsPiece(PieceType.KNIGHT, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("c8").IsPiece(PieceType.BISHOP, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("d8").IsPiece(PieceType.QUEEN, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("f8").IsPiece(PieceType.BISHOP, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("g8").IsPiece(PieceType.KNIGHT, BLACK) Then NbrOfUndevelopedPieces += 1
            If gBoardBefore("h8").IsPiece(PieceType.ROOK, BLACK) Then NbrOfUndevelopedPieces += 1
        End If
        Return NbrOfUndevelopedPieces
    End Function

    Private Function PawnAtCenter() As Boolean
        If gBoardBefore.ActiveColor = WHITE Then
            If gBoardBefore("d4").IsPiece(PieceType.PAWN, WHITE) Then Return True
            If gBoardBefore("e4").IsPiece(PieceType.PAWN, WHITE) Then Return True
            If gBoardBefore("d5").IsPiece(PieceType.PAWN, WHITE) Then Return True
            If gBoardBefore("e5").IsPiece(PieceType.PAWN, WHITE) Then Return True
            If gBoardBefore("C4").IsPiece(PieceType.PAWN, WHITE) Then Return True
        Else
            If gBoardBefore("d5").IsPiece(PieceType.PAWN, BLACK) Then Return True
            If gBoardBefore("e5").IsPiece(PieceType.PAWN, BLACK) Then Return True
            If gBoardBefore("d4").IsPiece(PieceType.PAWN, BLACK) Then Return True
            If gBoardBefore("e4").IsPiece(PieceType.PAWN, BLACK) Then Return True
            If gBoardBefore("c5").IsPiece(PieceType.PAWN, BLACK) Then Return True
        End If
        Return False
    End Function

    Private Function RooksConnected() As Boolean
        Dim Rooks As List(Of ChessField) = gBoardBefore.FindPiece(PieceType.ROOK, gBoardBefore.ActiveColor)
        If Rooks.Count < 2 Then Return False 'Assuming there are 1 or 2 rooks on the board after opening
        If Rooks.First.DefendedBy(gBoardBefore.ActiveColor).Contains(Rooks.Last) Then
            'The rooks are connected
            Return True
        Else
            Return False
        End If
    End Function

    Private Function DiscoveredCheck() As String
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        'BestMove is indicating a DiscoverdCheck
        Dim Board As New ChessBoard(gBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the Discovered Check

        Dim KingField As ChessField = Board.FindKing(gBoardBefore.ActiveColor.Opponent)
        Dim Attackers As List(Of ChessField) = KingField.AttackedBy(gBoardBefore.ActiveColor)
        If Attackers.Count = 1 _
        AndAlso Attackers.First.Name <> gMove.ToFieldName Then
            InsertSubVariant(MessageText("MissedDiscoveredCheck", "|"), gBoardBefore, BestMove)
            Return MessageText("MissedDiscoveredCheck", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
        Else
            Return ""
        End If
    End Function

    Private Function DoubleCheck() As String
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        'BestMove is indicating a DoubleCheck
        Dim Board As New ChessBoard(gBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the Finishing of the Opening

        Dim KingField As ChessField = Board.FindKing(gBoardBefore.ActiveColor.Opponent)
        If KingField.AttackedBy(gBoardBefore.ActiveColor).Count > 1 Then
            InsertSubVariant(MessageText("MissedDoubleCheck", "|"), gBoardBefore, BestMove)
            Return MessageText("MissedDoubleCheck", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
        Else
            Return ""
        End If
    End Function

    Private Function AttackOnPinnedPiece() As String
        'Find Pinned Piece(s) of Opponent at BoardBefore
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove As New ChessBoard(gBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove) 'Execute the Attack on a pinned piece

        Dim PinnedPieces As List(Of ChessField) = GetPinnedPieces(gBoardBefore, gBoardBefore.ActiveColor.Opponent)
        For Each PinnedPiece As ChessField In PinnedPieces
            Dim AttackersBefore As List(Of ChessField) = PinnedPiece.AttackedBy(gBoardBefore.ActiveColor)
            Dim AttackersAfter As List(Of ChessField) = BoardAfterBestMove(PinnedPiece.Name).AttackedBy(gBoardBefore.ActiveColor)
            If AttackersAfter.Count > AttackersBefore.Count Then
                'If there's One attacker more; then BestMove is an attack on the pinned piece    
                InsertSubVariant(MessageText("MissedAttackOnPinnedPiece", "|"), gBoardBefore, BestMove)
                Return MessageText("MissedAttackOnPinnedPiece", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
            End If
        Next PinnedPiece
        Return ""
    End Function

    Private Function PinnedPieceIsBadDefender() As String
        Dim EmptyBoard As New ChessBoard("8/8/8/8/8/8/8/8 w - - 0 1")
        'Find Pinned Piece(s) of Opponent at BoardBefore
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove As New ChessBoard(gBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove) 'Execute the Pinned Piece is a bad defender

        Dim PinnedPieces As List(Of ChessField) = GetPinnedPieces(gBoardBefore, gBoardBefore.ActiveColor.Opponent)
        For Each PinnedPiece As ChessField In PinnedPieces
            'Create empty Board with only Pinned Piece, to get all possible moves
            EmptyBoard.Clear()
            EmptyBoard(PinnedPiece.Name).Piece = PinnedPiece.Piece
            Dim PossibleMoves As List(Of BoardMove) = PinnedPiece.Piece.PossibleMoves(PinnedPiece.Name, EmptyBoard)
            For Each PossibleMove As BoardMove In PossibleMoves
                If PossibleMove.ToFieldName = BestMove.ToFieldName Then
                    'BestMove is putting a piece on a field where the pinned piece can move to
                    InsertSubVariant(MessageText("MissedPinnedPieceIsBadDefender", PinnedPiece.Name, "|"), gBoardBefore, BestMove)
                    Return MessageText("MissedPinnedPieceIsBadDefender", PinnedPiece.Name, BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
                End If
            Next PossibleMove
        Next PinnedPiece

        Return ""
    End Function

    Private Function DefendAgainstPinning() As String
        'Find Own Pinned Piece(s) at BoardBefore
        Dim PinnedPiecesBefore As List(Of ChessField) = GetPinnedPieces(gBoardBefore, gBoardBefore.ActiveColor)

        'Find Own Pinned Piece(s) at BoardAfterBestMove
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove As New ChessBoard(gBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove) 'Execute the Pinned Piece is a bad defender
        Dim PinnedPiecesAfter As List(Of ChessField) = GetPinnedPieces(BoardAfterBestMove, gBoardBefore.ActiveColor)

        'If One Pinning less, then he BestMove is a defense against pinning
        If PinnedPiecesAfter.Count < PinnedPiecesBefore.Count Then
            InsertSubVariant(MessageText("DefendAgainstPinning", GetMissingPinning(PinnedPiecesBefore, PinnedPiecesAfter), "|"), gBoardBefore, BestMove)
            Return MessageText("DefendAgainstPinning", GetMissingPinning(PinnedPiecesBefore, PinnedPiecesAfter), BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
        Else
            Return ""
        End If
    End Function

    Private Function GetMissingPinning(pPinnedPiecesBefore As List(Of ChessField), pPinnedPiecesAfter As List(Of ChessField)) As String
        For Each PinnedPiece As ChessField In pPinnedPiecesBefore
            If pPinnedPiecesAfter.Contains(PinnedPiece) = False Then
                Return PinnedPiece.Name
            End If
        Next PinnedPiece
        Return ""
    End Function

    Private Function GetPinnedPieces(pBoardBefore As ChessBoard, pColor As ChessColor) As List(Of ChessField)
        'Look for opponent pieces, except King
        'Get the attackers of the pinned piece at BoardBefore
        'Find the TailPiece, In opposite direction of the Attacker
        'Remark: Not all pinnings found here, are effective. Assuming the BestMove wil use the best pinning
        Dim PinnedPieces As New List(Of ChessField)
        Dim Fields As New List(Of ChessField)
        For Each Field As ChessField In gBoardBefore
            If Field Is Nothing Then Continue For
            If Field.Piece Is Nothing Then Continue For
            If Field.Piece.Color = pColor Then
                Fields.Add(Field)
            End If
        Next Field

        For Each Field As ChessField In Fields
            Dim Attackers As List(Of ChessField) = Field.AttackedBy(gBoardBefore.ActiveColor)
            If Attackers.Count > 0 Then
                For Each Attacker As ChessField In Attackers
                    Dim TailPiece As ChessField = FindTailPiece(Field, Attacker, gBoardBefore)
                    If TailPiece IsNot Nothing Then
                        PinnedPieces.Add(Field)
                    End If
                Next Attacker

                PinnedPieces.Add(Field)
            End If
        Next
        Return PinnedPieces
    End Function

    Private Function FindTailPiece(Piece As ChessField, Attacker As ChessField, pBoard As ChessBoard) As ChessField
        Dim DCol As Integer = If(Piece.Column = Attacker.Column, 0, If(Piece.Column < Attacker.Column, -1, 1))
        Dim DRow As Integer = If(Piece.Row = Attacker.Row, 0, If(Piece.Row < Attacker.Row, -1, 1))

        Dim Col As Integer = Piece.Column + DCol
        Dim Row As Integer = Piece.Row + DRow
        Do While (pBoard.Exists(Col, Row))
            Dim TailPiece As ChessField = pBoard(Col, Row)
            If TailPiece.Piece Is Nothing Then
                'Empty field; continue searching
                Col += DCol : Row += DRow
            ElseIf TailPiece.Piece.Color = Piece.Piece.Color _
            AndAlso TailPiece.Piece.Value > Piece.Piece.Value Then
                'Found the TailPiece
                Return TailPiece
            Else
                'Something else on this field
                Return Nothing
            End If
        Loop
        Return Nothing
    End Function

End Class
