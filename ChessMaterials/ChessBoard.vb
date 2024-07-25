Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports ChessMaterials.ChessPiece

Public Class ChessBoard

    Public Fields As ChessFields                  'Fields 1 to 64 

    'FEN Variables
    Public ActiveColor As ChessColor              'The side to move
    Public WhiteLongCastlingAllowed As Boolean    'True if castling is allowed 
    Public WhiteShortCastlingAllowed As Boolean
    Public BlackLongCastlingAllowed As Boolean
    Public BlackShortCastlingAllowed As Boolean
    Public EpFieldName As String
    Public MovesSincePawnMoveOrCapture As Integer 'Used to handle the fifty-move-draw rule
    Public MoveNr As Integer

    Public Sub New(Optional pFEN As String = "")
        Fields = New ChessFields()
        Clear()
        Me.FEN = pFEN
    End Sub

    Public Property FEN As String
        Set(pFEN As String)
            'FEN is Forsyth-Edwards Notation
            Dim P As Long, R As Long, C As Long
            Dim NrOfHalfMovesString As String = "", MoveNrString As String = ""

            Fields.Clear()

            'Set up Pieces from Black-side to White-side
            R = 8 : C = 1
            For P = 1 To Len(pFEN)
                Select Case Mid$(pFEN, P, 1)
                    Case " " : Exit For
                    Case "/" 'R = R - 1: C = 1
                    Case "R", "N", "B", "Q", "K", "P",
                         "r", "n", "b", "q", "k", "p"
                        Fields(C, R).Piece = ChessPiece.CreatePiece(Mid$(pFEN, P, 1))
                        C += 1
                    Case "0" To "9"  'Empty fields: if 32 then first add 30 later 2
                        If IsNumeric(Mid$(pFEN, P + 1, 1)) Then
                            C += (Val(Mid$(pFEN, P, 1)) * 10)
                        Else
                            C += Val(Mid$(pFEN, P, 1))
                        End If
                End Select
                While C > 8
                    R -= 1
                    C -= 8
                End While
            Next P

            'Active Color
            If Mid$(pFEN, P, 1) = " " Then P += 1
            For P = P To Len(pFEN)
                Select Case Mid$(pFEN, P, 1)
                    Case " " : Exit For
                    Case "w" : ActiveColor = WHITE
                    Case "b" : ActiveColor = BLACK
                End Select
            Next P

            'Castling
            WhiteLongCastlingAllowed = False : WhiteShortCastlingAllowed = False
            BlackLongCastlingAllowed = False : BlackShortCastlingAllowed = False
            If Mid$(pFEN, P, 1) = " " Then P += 1
            For P = P To Len(pFEN)
                Select Case Mid$(pFEN, P, 1)
                    Case " " : Exit For
                    Case "-"
                    Case "K" : WhiteShortCastlingAllowed = True
                    Case "Q" : WhiteLongCastlingAllowed = True
                    Case "k" : BlackShortCastlingAllowed = True
                    Case "q" : BlackLongCastlingAllowed = True
                    Case Else : Exit For
                End Select
            Next P

            'En Passant
            EpFieldName = ""
            If Mid$(pFEN, P, 1) Like " " Then P += 1
            If Mid$(pFEN, P, 2) Like "[abcdefgh]#" Then
                EpFieldName = Mid$(pFEN, P, 2)
            End If

            'NrHalfMoves since the last PAWN advance or capturing move
            If Mid$(pFEN, P, 1) = " " Then P += 1
            For P = P To Len(pFEN)
                Select Case Mid$(pFEN, P, 1)
                    Case " " : Exit For
                    Case "0" To "9" : NrOfHalfMovesString = NrOfHalfMovesString & Mid$(pFEN, P, 1)
                End Select
            Next P
            MovesSincePawnMoveOrCapture = Val(NrOfHalfMovesString)

            'Nbr of Moves
            If Mid$(pFEN, P, 1) = " " Then P += 1
            For P = P To Len(pFEN)
                Select Case Mid$(pFEN, P, 1)
                    Case " " : Exit For
                    Case "0" To "9" : MoveNrString = MoveNrString & Mid$(pFEN, P, 1)
                End Select
            Next P
            MoveNr = Val(MoveNrString)
        End Set

        Get
            'FEN is Forsyth-Edwards Notation
            Dim P As Long = 0, R As Long, C As Long
            Dim PiecePlacement As String = "", Color As String, Castling As String, EPFieldName As String
            'Piece Placement
            For R = 8 To 1 Step -1
                For C = 1 To 8
                    If Fields(C, R).Piece Is Nothing Then
                        P += 1
                    Else
                        If P > 0 Then
                            PiecePlacement = PiecePlacement & String.Format(P)
                            P = 0
                        End If
                        PiecePlacement = PiecePlacement & Fields(C, R).Piece.FENName
                    End If
                Next C
                If P > 0 Then
                    PiecePlacement = PiecePlacement & String.Format(P)
                    P = 0
                End If
                If R > 1 Then PiecePlacement = PiecePlacement & "/"
            Next R

            'Color
            Color = If(ActiveColor = WHITE, "w", "b")

            'Castling
            Castling = If(WhiteShortCastlingAllowed = True, "K", "") _
                     & If(WhiteLongCastlingAllowed = True, "Q", "") _
                     & If(BlackShortCastlingAllowed = True, "k", "") _
                     & If(BlackLongCastlingAllowed = True, "q", "")
            If Castling = "" Then Castling = "-"

            'En Passant
            If Me.EpFieldName = "" Then
                EPFieldName = "-"
            Else
                EPFieldName = Me.EpFieldName
            End If

            Return PiecePlacement & " " & Color & " " & Castling & " " & EPFieldName & " " & String.Format(MovesSincePawnMoveOrCapture) & " " & String.Format(MoveNr)
        End Get
    End Property

    Public Sub Clear()
        FEN = "8/8/8/8/8/8/8/8 w - - 0 1"
    End Sub

    Public Sub InitialPosition()
        FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
    End Sub

    Public Sub AddPiece(pPiece As ChessPiece, pColumn As Integer, pRow As Integer)
        Fields(pColumn, pRow).Piece = pPiece

        'Set Castling Options (might be a little too eager)
        If pPiece.Type = PieceType.KING Then
            If pPiece.Color = ChessColor.WHITE Then
                If IsPiece(Fields("a1"), PieceType.ROOK, WHITE) = True Then
                    WhiteLongCastlingAllowed = True
                ElseIf IsPiece(Fields("h1"), PieceType.ROOK, WHITE) = True Then
                    WhiteShortCastlingAllowed = True
                End If
            Else 'Color is Black
                If IsPiece(Fields("a8"), PieceType.ROOK, BLACK) = True Then
                    BlackLongCastlingAllowed = True
                ElseIf IsPiece(Fields("h8"), PieceType.ROOK, BLACK) = True Then
                    BlackShortCastlingAllowed = True
                End If
            End If

        ElseIf pPiece.Type = PieceType.ROOK Then
            If pPiece.Color = ChessColor.WHITE Then
                If IsPiece(Fields("e1"), PieceType.KING, WHITE) = True Then
                    If Fields(pColumn, pRow).Name = "a1" Then
                        WhiteLongCastlingAllowed = True
                    ElseIf Fields(pColumn, pRow).Name = "h1" Then
                        WhiteShortCastlingAllowed = True
                    End If
                End If
            Else 'Color is Black
                If IsPiece(Fields("e8"), PieceType.KING, BLACK) = True Then
                    If Fields(pColumn, pRow).Name = "a8" Then
                        BlackLongCastlingAllowed = True
                    ElseIf Fields(pColumn, pRow).Name = "h8" Then
                        BlackShortCastlingAllowed = True
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub RemovePiece(pPiece As ChessPiece, pColumn As Integer, pRow As Integer)
        If pRow < 0 Or pRow > 8 Or pColumn < 0 Or pColumn > 8 Then Exit Sub

        Fields(pColumn, pRow).Piece = Nothing

        'Set Castling Options (might be a little too eager)
        If pPiece.Type = PieceType.KING Then
            If pPiece.Color = ChessColor.WHITE Then
                WhiteLongCastlingAllowed = False
                WhiteShortCastlingAllowed = False
            Else 'Color is Black
                BlackLongCastlingAllowed = False
                BlackShortCastlingAllowed = False
            End If

        ElseIf pPiece.Type = PieceType.ROOK Then
            If pPiece.Color = ChessColor.WHITE Then
                If Fields(pColumn, pRow).Name = "a1" Then
                    WhiteLongCastlingAllowed = False
                ElseIf Fields(pColumn, pRow).Name = "h1" Then
                    WhiteShortCastlingAllowed = False
                End If
            Else 'Color is Black
                If Fields(pColumn, pRow).Name = "a8" Then
                    BlackLongCastlingAllowed = False
                ElseIf Fields(pColumn, pRow).Name = "h8" Then
                    BlackShortCastlingAllowed = False
                End If
            End If
        End If

    End Sub

    Public Function IsPiece(pField As ChessField, pPieceType As PieceType, pColor As ChessColor)
        If pField.Piece Is Nothing Then Return False
        If pField.Piece.Type <> pPieceType Then Return False
        If pField.Piece.Color <> pColor Then Return False
        Return True
    End Function

    Public Function PerformMove(pMove As BoardMove) As Boolean

        Select Case pMove.FromFieldName 'Perhaps Rook or King was moved
            Case "a1" : WhiteLongCastlingAllowed = False
            Case "e1" : WhiteShortCastlingAllowed = False : WhiteLongCastlingAllowed = False
            Case "h1" : WhiteShortCastlingAllowed = False
            Case "a8" : BlackLongCastlingAllowed = False
            Case "e8" : BlackShortCastlingAllowed = False : BlackLongCastlingAllowed = False
            Case "h8" : BlackShortCastlingAllowed = False
        End Select

        If pMove.Castle = True Then
            If pMove.Piece.Color = WHITE Then
                If pMove.ToFieldName = "c1" Then
                    'If Me.WhiteLongCastling = False Then Return False
                    Me.Fields("c1").Piece = Me.Fields("e1").Piece : Fields("e1").Piece = Nothing
                    Fields("d1").Piece = Fields("a1").Piece : Fields("a1").Piece = Nothing
                    WhiteShortCastlingAllowed = False : WhiteLongCastlingAllowed = False
                Else
                    'If Me.WhiteShortCastling = False Then Return False
                    Fields("g1").Piece = Fields("e1").Piece : Fields("e1").Piece = Nothing
                    Fields("f1").Piece = Fields("h1").Piece : Fields("h1").Piece = Nothing
                    WhiteShortCastlingAllowed = False : WhiteLongCastlingAllowed = False
                End If
                Me.ActiveColor = Opponent(pMove.Piece.Color)
                Return True
            Else 'Black
                If pMove.ToFieldName = "c8" Then
                    'If Me.BlackLongCastling = False Then Return False
                    Fields("c8").Piece = Fields("e8").Piece : Fields("e8").Piece = Nothing
                    Fields("d8").Piece = Fields("a8").Piece : Fields("a8").Piece = Nothing
                    BlackShortCastlingAllowed = False : BlackLongCastlingAllowed = False
                Else
                    'If Me.BlackShortCastling = False Then Return False
                    Fields("g8").Piece = Fields("e8").Piece : Fields("e8").Piece = Nothing
                    Fields("f8").Piece = Fields("h8").Piece : Fields("h8").Piece = Nothing
                    BlackShortCastlingAllowed = False : BlackLongCastlingAllowed = False
                End If
                Me.ActiveColor = Opponent(pMove.Piece.Color)
                Return True
            End If
        End If

        If IsIntendedCastling(pMove) = True Then
            Me.ActiveColor = Opponent(pMove.Piece.Color)
            MsgBox(MessageText("IntendedCastling"), MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            Return True
        End If

        If pMove.Piece.Type = PieceType.PAWN Or (Me.Fields(pMove.ToFieldName).Piece IsNot Nothing) Then
            MovesSincePawnMoveOrCapture = 0
        Else
            MovesSincePawnMoveOrCapture += 1
        End If

        'Move the piece
        Me.Fields(pMove.ToFieldName).Piece = pMove.Piece
        Me.Fields(pMove.FromFieldName).Piece = Nothing

        'Promotion
        If pMove.PromotionPiece IsNot Nothing Then
            Me.Fields(pMove.ToFieldName).Piece = pMove.PromotionPiece
        End If

        'When en passant move then Erase the pawn 
        If pMove.EnPassant = True _
        Or (pMove.Piece.Type = PieceType.PAWN And pMove.ToFieldName = EpFieldName) Then
            If pMove.Piece.Color = WHITE Then
                Fields(Left(pMove.ToFieldName, 1), Val(Right(pMove.ToFieldName, 1)) - 1).Piece = Nothing
            Else 'BLACK
                Fields(Left(pMove.ToFieldName, 1), Val(Right(pMove.ToFieldName, 1)) + 1).Piece = Nothing
            End If
        End If

        'Setting the En-Passant Field
        EpFieldName = ""
        If pMove.Piece.Type = PieceType.PAWN Then
            If Val(Right(pMove.ToFieldName, 1)) - Val(Right(pMove.FromFieldName, 1)) = 2 Then 'White Pawn move of two steps
                EpFieldName = Fields(Left(pMove.FromFieldName, 1), Val(Right(pMove.FromFieldName, 1)) + 1).Name
            End If
            If Val(Right(pMove.FromFieldName, 1)) - Val(Right(pMove.ToFieldName, 1)) = 2 Then 'Black Pawn move of two steps
                EpFieldName = Fields(Left(pMove.FromFieldName, 1), Val(Right(pMove.FromFieldName, 1)) - 1).Name
            End If
        End If

        Me.ActiveColor = Opponent(pMove.Piece.Color)
        Return True
    End Function

    Public Function BriefNotationFieldName(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String) As String
        Dim CandidateFields As New List(Of ChessField), Field As ChessField, F As Long

        'If not a Valid Move Remember the origin
        If pPiece.IsValidMove(Me, pFromFieldName, pToFieldName) = False Then
            Return pFromFieldName
        End If

        'Find all Other ChessPieces of same type and color 
        For Each Field In Me.Fields
            If Field Is Nothing Then Continue For
            If IsPiece(Field, pPiece.Type, pPiece.Color) = True _
            And Field.Name <> pFromFieldName Then
                CandidateFields.Add(Field)
            End If
        Next Field

        If CandidateFields.Count = 0 Then 'Not Other pieces
            Return ""
        End If

        'Find ChessPieces with PossibleMoves that can access the Target  
        Dim PossibleMoves As List(Of BoardMove), IsAccessable As Boolean

        For F = CandidateFields.Count - 1 To 0 Step -1
            Field = CandidateFields(F)
            PossibleMoves = Field.Piece.PossibleMoves(Field.Name, Me)
            'Check to see if this piece can move to the specified Target
            IsAccessable = False
            For Each Move As BoardMove In PossibleMoves
                If Move.ToFieldName = pToFieldName Then
                    IsAccessable = True
                    Exit For
                End If
            Next Move
            If IsAccessable = False Then CandidateFields.Remove(Field)
        Next F

        If CandidateFields.Count = 0 Then 'Not Other pieces
            Return ""
        ElseIf CandidateFields.Count = 1 Then
            If CandidateFields(0).ColumnName <> Left(pFromFieldName, 1) Then
                Return Left(pFromFieldName, 1)
            Else
                Return Right(pFromFieldName, 1)
            End If
        Else
            Return pFromFieldName
        End If
    End Function

    Public Function FindPiece(pPiece As ChessPiece, pFromColumn As String, pFromRow As String, pTo As String) As ChessField
        Dim CandidateFields As New List(Of ChessField), Field As ChessField, F As Long
        'When FromField has been specified 
        If pFromColumn <> "" And pFromRow <> "" Then
            Return Me.Fields(pFromColumn & pFromRow)
        End If

        'Find all ChessPieces of same type and color 
        For Each Field In Me.Fields
            If Field Is Nothing Then Continue For
            If Field.Piece IsNot Nothing Then
                If Field.Piece.Type = pPiece.Type _
                AndAlso (pPiece.Color = UNKNOWN OrElse Field.Piece.Color = pPiece.Color) _
                AndAlso (pFromColumn = "" OrElse Left(Field.Name, 1) = pFromColumn) _
                AndAlso (pFromRow = "" OrElse Right(Field.Name, 1) = pFromRow) Then
                    CandidateFields.Add(Field)
                End If
            End If
        Next Field

        If CandidateFields.Count = 1 Then
            Return CandidateFields(0)
        End If

        'Find ChessPieces with PossibleMoves that can access the Target  
        Dim PossibleMoves As List(Of BoardMove), IsAccessable As Boolean

        For F = CandidateFields.Count - 1 To 0 Step -1
            Field = CandidateFields(F)
            PossibleMoves = Field.Piece.PossibleMoves(Field.Name, Me)
            'Check to see if this piece can move to the specified Target
            IsAccessable = False
            For Each Move As BoardMove In PossibleMoves
                If Move.ToFieldName = pTo Then
                    IsAccessable = True
                    Exit For
                End If
            Next Move
            If IsAccessable = False Then CandidateFields.Remove(Field)
        Next F

        If CandidateFields.Count = 1 Then
            Return CandidateFields(0)
        End If

        Throw New DataMisalignedException(MessageText("PGNMoveInvalid", pPiece.Name(CurrentLanguage), pTo))
        'Perhaps eliminate moves that cause Check...
        Return Nothing
    End Function

    Public Function AllPossibleMoves(pColor As ChessColor) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        For Each Field As ChessField In Me.Fields
            If Field Is Nothing Then Continue For
            If Field.Piece Is Nothing Then Continue For
            If Field.Piece.Color <> pColor Then Continue For
            Moves.AddRange(Field.Piece.PossibleMoves(Field.Name, Me))
        Next Field
        Return Moves
    End Function

    Private Function IsIntendedCastling(pMove As BoardMove) As Boolean
        'Perhaps Castling; but from wrong starting position, or rooks moved, or whatever...
        Dim FromField As ChessField = Fields(pMove.FromFieldName)
        Dim ToField As ChessField = Fields(pMove.ToFieldName)
        If pMove.Piece.Type <> PieceType.KING _
        Or Math.Abs(FromField.Column - ToField.Column) <> 2 Then
            Return False
        End If

        Dim KingWalkOverField As ChessField = Fields((FromField.Column + ToField.Column) / 2, FromField.Row)
        If ToField.Piece IsNot Nothing _
        Or KingWalkOverField.Piece IsNot Nothing Then 'King's target and walk-over field are empty
            Return False
        End If

        If pMove.Piece.Color = WHITE Then
            If FromField.Row <> 1 Or ToField.Row <> 1 Then 'King walking on row 1
                Return False
            End If
            If FromField.Column < ToField.Column Then 'King moving to the Right side
                If Fields("h1").Piece.Type = PieceType.ROOK Then
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("h1").Piece : Fields("h1").Piece = Nothing
                    Return True
                ElseIf Fields("g1").Piece.Type = PieceType.ROOK Then 'Rook at invalid place, but probably castling intended
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("g1").Piece : Fields("g1").Piece = Nothing
                    Return True
                End If
            Else 'King moving to the Left side
                If Fields("a1").Piece.Type = PieceType.ROOK Then
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("a1").Piece : Fields("a1").Piece = Nothing
                    Return True
                ElseIf Fields("b1").Piece.Type = PieceType.ROOK Then 'Rook at invalid place, but probably castling intended
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("b1").Piece : Fields("b1").Piece = Nothing
                    Return True
                End If
            End If
        Else 'Black
            If FromField.Row <> 8 Or ToField.Row <> 8 Then 'King walking on row 8
                Return False
            End If
            If FromField.Column < ToField.Column Then 'King moving to the Right side
                If Fields("h8").Piece.Type = PieceType.ROOK Then
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("h8").Piece : Fields("h8").Piece = Nothing
                    Return True
                ElseIf Fields("g8").Piece.Type = PieceType.ROOK Then 'Rook at invalid place, but probably castling intended
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("g8").Piece : Fields("g8").Piece = Nothing
                    Return True
                End If
            Else 'King moving to the Left side
                If Fields("a8").Piece.Type = PieceType.ROOK Then
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("a8").Piece : Fields("a8").Piece = Nothing
                    Return True
                ElseIf Fields("b8").Piece.Type = PieceType.ROOK Then 'Rook at invalid place, but probably castling intended
                    ToField.Piece = FromField.Piece : FromField.Piece = Nothing
                    KingWalkOverField.Piece = Fields("b8").Piece : Fields("b8").Piece = Nothing
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Overrides Function ToString() As String
        Return Me.FEN
    End Function

    Protected Overrides Sub Finalize()
        Me.Fields = Nothing
        Me.ActiveColor = Nothing

        MyBase.Finalize()
    End Sub
End Class

