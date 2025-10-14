Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.modChessLanguage
Imports ChessMessaging.Messages
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports ChessEngine
Imports PGNLibrary

Public Class Step2Feedback
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
                If Math.Abs(.EngineVariant(PV).Score - .Score) < 75 Then 'Score in Centipoints
                    If gMove.FromFieldName = .EngineVariant(PV).FirstMove.FromFieldName _
                    And gMove.ToFieldName = .EngineVariant(PV).FirstMove.ToFieldName Then
                        'BestMove, or almost best move was played; No comment to make
                        Return ""
                    End If
                End If
            End With
        Next PV

        Message = CheckMateOpponent()
        If Message <> "" Then Return Message
        Message = CheckMateIn2()
        If Message <> "" Then Return Message

        If gResults.Score < -100 Then 'Losing more than a Pawn

            Message = MissedPinning()
            If Message <> "" Then Return Message
            Message = MissedDoubleAttack()
            If Message <> "" Then Return Message
            Message = EliminateDefence()
            If Message <> "" Then Return Message
            Message = MissedDiscoverdAttack()
            If Message <> "" Then Return Message
            'Defend against Mate (moet uit Stap 1 gehaald worden !!!)
            'DiscoveredAttack

        End If
        Return ""
    End Function

    Private Function MissedPinning() As String
        Dim Message As String
        Dim PinningPiece As ChessPiece = gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece
        If PinningPiece.Type = PieceType.KING _
        Or PinningPiece.Type = PieceType.PAWN Then
            Return "" 'No pinning possible
        End If
        Dim Directions As New Directions(PinningPiece.Type)
        For Each Direction As Direction In Directions
            Message = FindPinning(Direction)
            If Message <> "" Then Return Message
        Next Direction

        Return ""
    End Function

    Private Function FindPinning(pDirection As Direction) As String
        Dim PinnedPieceFieldName As String
        'BestMove is indicating a pinning
        Dim C As Integer = gBoardBefore(gResults.Before.BestMove.ToFieldName).Column
        Dim R As Integer = gBoardBefore(gResults.Before.BestMove.ToFieldName).Row

        'Looking for first piece in line
        C += pDirection.ColumnIncrement
        R += pDirection.RowIncrement
        While (gBoardBefore.Exists(C, R) = True _
               AndAlso gBoardBefore(C, R).Piece Is Nothing)
            C += pDirection.ColumnIncrement
            R += pDirection.RowIncrement
        End While
        If gBoardBefore.Exists(C, R) = False Then Return ""

        'Some candidate pinned piece found
        With gBoardBefore(C, R)
            If .Piece.Color = gBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN _
            Or .Piece.Type = PieceType.QUEEN Then
                Return "" 'Pinned piece shoulde be of opponent, and no pawn nor a queen that's looking back
            End If
            If pDirection.Diagonal = True _
            And .Piece.Type = PieceType.BISHOP Then
                Return "" 'Pinned piece shoulde not be looking back
            End If
            If pDirection.Diagonal = False _
            And .Piece.Type = PieceType.ROOK Then
                Return "" 'Pinned piece shoulde not be looking back
            End If
            PinnedPieceFieldName = .Name
        End With

        'Now looking for a proper piece behind
        C += pDirection.ColumnIncrement
        R += pDirection.RowIncrement
        While (gBoardBefore.Exists(C, R) = True _
               AndAlso gBoardBefore(C, R).Piece Is Nothing)
            C += pDirection.ColumnIncrement
            R += pDirection.RowIncrement
        End While
        If gBoardBefore.Exists(C, R) = False Then Return ""

        'Some piece found
        With gBoardBefore(C, R)
            If .Piece.Color = gBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN Then
                Return "" 'Tail piece should only be of opponent, and no pawns
            End If
        End With

        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        InsertSubVariant(MessageText("MissedPinning", PinnedPieceFieldName, "|"), gBoardBefore, BestMove)
        Return MessageText("MissedPinning", PinnedPieceFieldName, BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
    End Function

    Private Function MissedDoubleAttack() As String
        Dim ObjectsToAttack As New List(Of ChessField)
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        'BestMove is indicating a Double Attack

        Dim Board As New ChessBoard(gBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the candidate Double Attack

        'Now look what's being attacked by the piece
        Dim PossibleMoves As List(Of BoardMove) = BestMove.Piece.PossibleMoves(BestMove.ToFieldName, Board)
        For Each Move As BoardMove In PossibleMoves

            If IsObjectToAttack(Move, Board) = True Then
                ObjectsToAttack.Add(Board(Move.ToFieldName))
            End If

        Next Move

        Select Case ObjectsToAttack.Count
            Case 0, 1
                Return "" 'No Doudle Attack
            Case 2
                InsertSubVariant(MessageText("MissedDoubleAttack", "|", ObjectsToAttack(0).Name, ObjectsToAttack(1).Name), gBoardBefore, BestMove)
                Return MessageText("MissedDoubleAttack", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name)
            Case 3
                InsertSubVariant(MessageText("MissedTripleAttack", "|", ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name), gBoardBefore, BestMove)
                Return MessageText("MissedTripleAttack", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name)
            Case Else
                InsertSubVariant(MessageText("MissedMultiAttack", "|", ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name), gBoardBefore, BestMove)
                Return MessageText("MissedMultiAttack", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name)
        End Select

        Return ""
    End Function

    Private Function IsObjectToAttack(pMove As BoardMove, pBoard As ChessBoard)
        Dim ActiveColor As ChessColor = pBoard.Opponent 'BestMove has been executed
        'pMove is the actual execution of the Threat

        'Look if it's a King
        If pBoard(pMove.ToFieldName).Piece IsNot Nothing _
        AndAlso pBoard(pMove.ToFieldName).Piece.Color = ActiveColor.Opponent _
        AndAlso pBoard(pMove.ToFieldName).Piece.Type = PieceType.KING Then
            Return True
        End If

        'Look if it's checkmate after the move
        Dim TempBoard As New ChessBoard(pBoard.FEN)
        TempBoard.PerformMove(pMove)
        If TempBoard.CheckMate(ActiveColor.Opponent) Then
            Return True
        End If

        'Look if attacked piece is of more value than the attacker
        If pBoard(pMove.ToFieldName).Piece IsNot Nothing _
        AndAlso pBoard(pMove.ToFieldName).Piece.Color = ActiveColor.Opponent _
        AndAlso pBoard(pMove.ToFieldName).Piece.Value > pMove.Piece.Value Then
            Return True
        End If

        'Look if the piece is an Unsufficient covered piece..
        If pBoard(pMove.ToFieldName).Piece IsNot Nothing _
        AndAlso pBoard(pMove.ToFieldName).Piece.Color = ActiveColor.Opponent _
        AndAlso pBoard(pMove.ToFieldName).DefendedBy(ActiveColor.Opponent).Count _
              < pBoard(pMove.ToFieldName).AttackedBy(ActiveColor).Count Then
            Return True
        End If

        Return False
    End Function

    Private Function EliminateDefence() As String
        'BestMove eliminates a defender (Capture, Chase Away, Interrupt Line of Defender, etc.)
        'So After the BestMove there's a profitable exchange or uncovered piece
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim Board As New ChessBoard(gBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the candidate Elimination of Defence
        Dim OpponentMove As New BoardMove(Board(gResults.Before.EngineVariant(0).MoveList(1).FromFieldName).Piece, gResults.Before.EngineVariant(0).MoveList(1).FromFieldName, gResults.Before.EngineVariant(0).MoveList(1).ToFieldName)
        Board.PerformMove(OpponentMove) 'Execute the opponents move (needed for Chase Away)

        'Find a piece to capture
        For Each Field As ChessField In gBoardBefore

            If Field Is Nothing _
            OrElse Field.Piece Is Nothing _
            OrElse Field.Piece.Color <> gBoardBefore.Opponent _
            OrElse Field.Piece.Type = PieceType.KING _
            OrElse Field.Piece.Type = PieceType.PAWN Then
                Continue For
            End If

            'So it's an opponent's piece
            If Field.DefendedBy(gBoardBefore.Opponent).Count _
             < Field.AttackedBy(gBoardBefore.ActiveColor).Count Then

                'Find out what defender was eliminated
                Dim ListOfDefendersBefore As List(Of ChessField) = gBoardBefore(Field.Name).DefendedBy(gBoardBefore.Opponent)
                Dim ListOfDefendersAfter As List(Of ChessField) = Field.DefendedBy(gBoardBefore.Opponent)
                For Each Defender As ChessField In ListOfDefendersBefore
                    If ListOfDefendersAfter.Contains(Defender) = False Then
                        InsertSubVariant(MessageText("EliminateDefence", "|", Defender.Name, Field.Name), gBoardBefore, BestMove)
                        Return MessageText("EliminateDefence", BestMove.Text(gBoardBefore, gBoardAfter, CurrentLanguage), Defender.Name, Field.Name)
                    End If
                Next Defender

            End If

        Next Field

        Return ""
    End Function

    Private Function CheckMateOpponent() As String
        Dim Move As BoardMove = gBoardAfter.CanCheckMate()
        If Move Is Nothing Then
            Return ""
        End If

        'So the opponent can Checkmate on next move !
        'Could this have been prevented ?

        'Assumning the BestMove would have prevented this
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove As New ChessBoard(gBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove)

        If BoardAfterBestMove.CanCheckMate() Is Nothing Then
            'No checkmate possible after BestMove
            InsertSubVariant(MessageText("CheckMateOpponent", "|"), gBoardBefore, Move)
            Return MessageText("CheckMateOpponent", Move.Text(gBoardBefore, gBoardAfter, CurrentLanguage))
        Else
            'Er kon toch niks aan gedaan worden
            Return ""
        End If
    End Function

    Private Function CheckMateIn2() As String
        If gResults.Before.EngineVariant(0).MateIn = "2" Then
            Dim Move0 As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
            Dim BoardAfter0 As New ChessBoard(gBoardBefore.FEN)
            BoardAfter0.PerformMove(Move0)
            Dim Move1 As New BoardMove(gBoardBefore(gResults.Before.EngineVariant(0).MoveList(1).FromFieldName).Piece, gResults.Before.EngineVariant(0).MoveList(1).FromFieldName, gResults.Before.EngineVariant(0).MoveList(1).ToFieldName)
            Dim BoardAfter1 As New ChessBoard(BoardAfter0.FEN)
            BoardAfter1.PerformMove(Move1)
            Dim Move2 As New BoardMove(gBoardBefore(gResults.Before.EngineVariant(0).MoveList(2).FromFieldName).Piece, gResults.Before.EngineVariant(0).MoveList(2).FromFieldName, gResults.Before.EngineVariant(0).MoveList(2).ToFieldName)
            Dim BoardAfter2 As New ChessBoard(BoardAfter1.FEN)
            BoardAfter2.PerformMove(Move2)
            InsertSubVariant(MessageText("CheckMateIn2", "|", "|"), gBoardBefore, Move1, Move2)
            Return MessageText("CheckMateIn2", Move1.Text(gBoardBefore, BoardAfter0, CurrentLanguage), Move2.Text(BoardAfter1, BoardAfter2, CurrentLanguage))
        End If
        Return ""
    End Function

    Private Function MissedDiscoverdAttack() As String
        'BestMove would take the Head piece and attack an Object to attack
        Dim HeadPieceField As ChessField = gBoardBefore(gResults.Before.BestMove.FromFieldName)
        Dim TailPieceField As ChessField
        Dim TargetForTailPiece As ChessField
        Dim TargetForHeadPiece As String = ""
        Dim BestMove As New BoardMove(gBoardBefore(gResults.Before.BestMove.FromFieldName).Piece, gResults.Before.BestMove.FromFieldName, gResults.Before.BestMove.ToFieldName)
        Dim Board As New ChessBoard(gBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the candidate Discovered Attack

        'Now look what's being attacked by the Headpiece
        Dim PossibleMoves As List(Of BoardMove) = HeadPieceField.Piece.PossibleMoves(BestMove.ToFieldName, Board)
        For Each Move As BoardMove In PossibleMoves
            If IsObjectToAttack(Move, Board) = True Then
                TargetForHeadPiece = Move.ToFieldName
                Exit For
            End If
        Next Move
        If TargetForHeadPiece = "" Then
            Return "" 'No valid target for headpiecefound
        End If

        'Candidate Discovered Attack is already executed, so Tailpiece is looking at Targetpiece
        'Find Candidate TailPiece
        For Each Direction As Direction In New Directions()

            TailPieceField = HeadPieceField.FirstPieceInLine(Direction)
            If TailPieceField Is Nothing _
            OrElse TailPieceField.Piece.Color <> gBoardBefore.ActiveColor _
            OrElse TailPieceField.Piece.Type = PieceType.ROOK Then
                Continue For
            End If
            If TailPieceField.Piece.Type = PieceType.ROOK _
            And Direction.Diagonal = True Then
                Continue For
            End If
            If TailPieceField.Piece.Type = PieceType.BISHOP _
            And Direction.Diagonal = False Then
                Continue For
            End If
            'Candidate TailPiece

            'Find Target for TailPiece
            TargetForTailPiece = HeadPieceField.FirstPieceInLine(Direction.OppositDirection)
            If TailPieceField Is Nothing _
            OrElse TailPieceField.Piece.Color <> gBoardBefore.ActiveColor Then
                Continue For
            End If

            'Perhaps Tailpiece should be covered; but BestMove is profitable, so probably not needed

            If IsObjectToAttack(New BoardMove(TailPieceField.Piece, TailPieceField.Name, TargetForTailPiece.Name), Board) = True Then
                InsertSubVariant(MessageText("MissedDiscoverdAttack", "|", HeadPieceField.Name, TargetForHeadPiece, TailPieceField.Name, TargetForTailPiece.Name), gBoardBefore, BestMove)
                Return MessageText("MissedDiscoverdAttack", BestMove.Text(gBoardBefore, Board, CurrentLanguage), HeadPieceField.Name, TargetForHeadPiece, TailPieceField.Name, TargetForTailPiece.Name)
            End If

        Next Direction

        Return ""
    End Function

End Class