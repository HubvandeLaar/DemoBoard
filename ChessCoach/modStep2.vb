Imports ChessGlobals
Imports ChessGlobals.modMessages
Imports ChessGlobals.modChessLanguage
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports ChessEngine

Module modStep2

    Public Function Step2Errors(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim Message As String = ""

        If pResults.Before.BestMove.FromFieldName = pMove.FromFieldName _
        And pResults.Before.BestMove.ToFieldName = pMove.ToFieldName Then
            'Best Move already played; No comment to make
            Return ""
        End If

        Message = CheckMateOpponent(pBoardBefore, pMove, pBoardAfter, pResults)
        If Message <> "" Then Return Message
        Message = CheckMateIn2(pBoardBefore, pMove, pBoardAfter, pResults)
        If Message <> "" Then Return Message

        If pResults.Score < -100 Then 'Losing more than a Pawn

            Message = MissedPinning(pBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = MissedDoubleAttack(pBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = EliminateDefence(pBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            Message = MissedDiscoverdAttack(pBoardBefore, pMove, pBoardAfter, pResults)
            If Message <> "" Then Return Message
            'Defend against Mate (moet uit Stap 1 gehaald worden !!!)
            'DiscoveredAttack

        End If
        Return ""
    End Function

    Private Function MissedPinning(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim Message As String

        'Straith pinning lines
        If pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece.Type = PieceType.QUEEN _
        Or pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece.Type = PieceType.ROOK Then

            'Looking Upwards
            Message = PinningStraight(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=0, pR_Increment:=1)
            If Message <> "" Then Return Message
            'Looking Downwards
            Message = PinningStraight(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=0, pR_Increment:=-1)
            If Message <> "" Then Return Message
            'Looking Right
            Message = PinningStraight(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=1, pR_Increment:=0)
            If Message <> "" Then Return Message
            'Looking Left
            Message = PinningStraight(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=-1, pR_Increment:=0)
            If Message <> "" Then Return Message
        End If


        'Diagonal pinning lines
        If pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece.Type = PieceType.QUEEN _
        Or pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece.Type = PieceType.BISHOP Then

            'Looking Right-Upwards
            Message = PinningDiagonal(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=1, pR_Increment:=1)
            If Message <> "" Then Return Message
            'Looking Right-Downwards
            Message = PinningDiagonal(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=1, pR_Increment:=-1)
            If Message <> "" Then Return Message
            'Looking Left-Downpwards
            Message = PinningDiagonal(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=-1, pR_Increment:=-1)
            If Message <> "" Then Return Message
            'Looking Left-Upwards
            Message = PinningDiagonal(pBoardBefore, pMove, pBoardAfter, pResults,
                                      pC_Increment:=-1, pR_Increment:=1)
            If Message <> "" Then Return Message
        End If

        Return ""
    End Function

    Private Function PinningStraight(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults,
                                     pC_Increment As Integer, pR_Increment As Integer) As String
        Dim C As Integer, R As Integer
        Dim PinnedPieceFieldName As String
        'BestMove is indicating a pinning
        C = pBoardBefore(pResults.Before.BestMove.ToFieldName).Column
        R = pBoardBefore(pResults.Before.BestMove.ToFieldName).Row

        'Looking for first piece in line
        C += pC_Increment
        R += pR_Increment
        While (pBoardBefore.Exists(C, R) = True _
               AndAlso pBoardBefore(C, R).Piece Is Nothing)
            C += pC_Increment
            R += pR_Increment
        End While
        If pBoardBefore.Exists(C, R) = False Then Return ""

        'Some candidate pinned piece found
        With pBoardBefore(C, R)
            If .Piece.Color = pBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN _
            Or .Piece.Type = PieceType.QUEEN _
            Or .Piece.Type = PieceType.ROOK Then
                Return "" 'Pinning only opponent, and no pawn nor a piece that's looking back
            End If
            PinnedPieceFieldName = .Name
        End With

        'Now looking for a proper piece behind
        C += pC_Increment
        R += pR_Increment
        While (pBoardBefore.Exists(C, R) = True _
               AndAlso pBoardBefore(C, R).Piece Is Nothing)
            C += pC_Increment
            R += pR_Increment
        End While
        If pBoardBefore.Exists(C, R) = False Then Return ""

        'Some piece found
        With pBoardBefore(C, R)
            If .Piece.Color = pBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN Then
                Return "" 'Pinning only opponent, and no pawns
            End If
        End With

        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        Return MessageText("MissedPinning", PinnedPieceFieldName, BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage))
    End Function

    Private Function PinningDiagonal(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults,
                                     pC_Increment As Integer, pR_Increment As Integer) As String
        Dim C As Integer, R As Integer
        Dim PinnedPieceFieldName As String
        'BestMove is indicating a pinning
        C = pBoardBefore(pResults.Before.BestMove.ToFieldName).Column
        R = pBoardBefore(pResults.Before.BestMove.ToFieldName).Row

        'Looking for first piece in line
        C += pC_Increment
        R += pR_Increment
        While (pBoardBefore.Exists(C, R) = True _
               AndAlso pBoardBefore(C, R).Piece Is Nothing)
            C += pC_Increment
            R += pR_Increment
        End While
        If pBoardBefore.Exists(C, R) = False Then Return ""

        'Some candidate pinned piece found
        With pBoardBefore(C, R)
            If .Piece.Color = pBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN _
            Or .Piece.Type = PieceType.QUEEN _
            Or .Piece.Type = PieceType.BISHOP Then
                Return "" 'Pinning only opponent, and no pawn nor a piece that's looking back
            End If
            PinnedPieceFieldName = .Name
        End With

        'Now looking for a proper piece behind
        C += pC_Increment
        R += pR_Increment
        While (pBoardBefore.Exists(C, R) = True _
               AndAlso pBoardBefore(C, R).Piece Is Nothing)
            C += pC_Increment
            R += pR_Increment
        End While
        If pBoardBefore.Exists(C, R) = False Then Return ""

        'Some piece found
        With pBoardBefore(C, R)
            If .Piece.Color = pBoardBefore.ActiveColor _
            Or .Piece.Type = PieceType.PAWN Then
                Return "" 'Pinning only opponent, and no pawns
            End If
        End With

        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        Return MessageText("MissedPinning", PinnedPieceFieldName, BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage))
    End Function

    Private Function MissedDoubleAttack(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        Dim ObjectsToAttack = New List(Of ChessField)
        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        'BestMove is indicating a Double Attack

        Dim Board As New ChessBoard(pBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the candidate Double Attack

        'Now look what's being attacked by the piece
        Dim PossibleMoves As List(Of BoardMove) = BestMove.Piece.PossibleMoves(BestMove.ToFieldName, Board)
        For Each Move As BoardMove In PossibleMoves

            If IsObjectToAttack(Move, Board) = True Then
                ObjectsToAttack.Add(Board(Move.ToFieldName))
            End If

        Next Move

        Select Case ObjectsToAttack.Count
            Case 1
                Return "" 'No Doudle Attack
            Case 2
                Return MessageText("MissedDoubleAttack", BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name)
            Case 3
                Return MessageText("MissedTripleAttack", BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name)
            Case Else
                Return MessageText("MissedMultiAttack", BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage), ObjectsToAttack(0).Name, ObjectsToAttack(1).Name, ObjectsToAttack(2).Name)
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

    Private Function EliminateDefence(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        'BestMove eliminates a defender (Capture, Chase Away, Interrupt Line of Defender, etc.)
        'So After the BestMove there's a profitable exchange or uncovered piece
        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        Dim Board As New ChessBoard(pBoardBefore.FEN)
        Board.PerformMove(BestMove) 'Execute the candidate Elimination of Defence
        Dim OpponentMove = New BoardMove(Board(pResults.Before.Movelist(1).FromFieldName).Piece, pResults.Before.Movelist(1).FromFieldName, pResults.Before.Movelist(1).ToFieldName)
        Board.PerformMove(OpponentMove) 'Execute the opponents move (needed for Chase Away)

        'Find a piece to capture
        For Each Field As ChessField In Board

            If Field Is Nothing _
            OrElse Field.Piece Is Nothing _
            OrElse Field.Piece.Color <> pBoardBefore.Opponent _
            OrElse Field.Piece.Type = PieceType.KING _
            OrElse Field.Piece.Type = PieceType.PAWN Then
                Continue For
            End If

            'So it's an opponent's piece
            If Field.DefendedBy(pBoardBefore.Opponent).Count _
             < Field.AttackedBy(pBoardBefore.ActiveColor).Count Then

                'Find out what defender was eliminated
                Dim ListOfDefendersBefore As List(Of ChessField) = pBoardBefore(Field.Name).DefendedBy(pBoardBefore.Opponent)
                Dim ListOfDefendersAfter As List(Of ChessField) = Field.DefendedBy(pBoardBefore.Opponent)
                For Each Defender As ChessField In ListOfDefendersBefore
                    If ListOfDefendersAfter.Contains(Defender) = False Then
                        Return MessageText("EliminateDefence", BestMove.Text(pBoardBefore, pBoardAfter, CurrentLanguage), Defender.Name, Field.Name)
                    End If
                Next Defender

            End If

        Next Field

        Return ""
    End Function

    Private Function CheckMateOpponent(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As EngineResults) As String
        Dim Move As BoardMove = pBoardAfter.CanCheckMate()
        If Move Is Nothing Then
            Return ""
        End If

        'So the opponent can Checkmate on next move !
        'Could this have been prevented ?

        'Assumning the BestMove would have prevented this
        Dim BestMove = New BoardMove(pBoardBefore(pResult.Before.BestMove.FromFieldName).Piece, pResult.Before.BestMove.FromFieldName, pResult.Before.BestMove.ToFieldName)
        Dim BoardAfterBestMove = New ChessBoard(pBoardBefore.FEN)
        BoardAfterBestMove.PerformMove(BestMove)

        If BoardAfterBestMove.CanCheckMate() Is Nothing Then
            'No checkmate possible after BestMove
            Return MessageText("CheckMateOpponent", Move.Text(pBoardBefore, pBoardAfter, CurrentLanguage))
        Else
            'Er kon toch niks aan gedaan worden
            Return ""
        End If
    End Function

    Private Function CheckMateIn2(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        If pResults.Before.Mate = "2" Then
            Dim Move0 = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
            Dim BoardAfter0 As New ChessBoard(pBoardBefore.FEN)
            BoardAfter0.PerformMove(Move0)
            Dim Move1 = New BoardMove(pBoardBefore(pResults.Before.Movelist(1).FromFieldName).Piece, pResults.Before.Movelist(1).FromFieldName, pResults.Before.Movelist(1).ToFieldName)
            Dim BoardAfter1 As New ChessBoard(BoardAfter0.FEN)
            BoardAfter1.PerformMove(Move1)
            Dim Move2 = New BoardMove(pBoardBefore(pResults.Before.Movelist(2).FromFieldName).Piece, pResults.Before.Movelist(2).FromFieldName, pResults.Before.Movelist(2).ToFieldName)
            Dim BoardAfter2 As New ChessBoard(BoardAfter1.FEN)
            BoardAfter2.PerformMove(Move2)
            Return MessageText("CheckMateIn2", Move1.Text(pBoardBefore, BoardAfter0, CurrentLanguage), Move2.Text(BoardAfter1, BoardAfter2, CurrentLanguage))
        End If
        Return ""
    End Function

    Private Function MissedDiscoverdAttack(pBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResults As EngineResults) As String
        'BestMove would take the Head piece and attack an Object to attack
        Dim HeadPieceField As ChessField = pBoardBefore(pResults.Before.BestMove.FromFieldName)
        Dim TailPieceField As ChessField
        Dim TargetForTailPiece As ChessField
        Dim TargetForHeadPiece As String = ""
        Dim BestMove = New BoardMove(pBoardBefore(pResults.Before.BestMove.FromFieldName).Piece, pResults.Before.BestMove.FromFieldName, pResults.Before.BestMove.ToFieldName)
        Dim Board As New ChessBoard(pBoardBefore.FEN)
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
        For Each Direction As Direction In New Directions

            TailPieceField = HeadPieceField.FirstPieceInLine(Direction)
            If TailPieceField Is Nothing _
            OrElse TailPieceField.Piece.Color <> pBoardBefore.ActiveColor _
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
            OrElse TailPieceField.Piece.Color <> pBoardBefore.ActiveColor Then
                Continue For
            End If

            'Perhaps Tailpiece should be covered; but BestMove is profitable, so probably not needed

            If IsObjectToAttack(New BoardMove(TailPieceField.Piece, TailPieceField.Name, TargetForTailPiece.Name), Board) = True Then
                Return MessageText("MissedDiscoverdAttack", BestMove.Text(pBoardBefore, Board, CurrentLanguage), HeadPieceField.Name, TargetForHeadPiece, TailPieceField.Name, TargetForTailPiece.Name)
            End If

        Next Direction

        Return ""
    End Function


End Module