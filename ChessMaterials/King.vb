Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlType>
Public Class King
    Inherits ChessPiece

    <XmlIgnore>
    Public Overrides ReadOnly Property Type As ChessPiece.PieceType
        Get
            Return PieceType.KING
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "Koning"
            Else
                Return "King"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return "K"
        End Get
    End Property

    <XmlIgnore>
    Public Shared ReadOnly Property KeyStroke(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return "K"
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Witte ", "Zwarte ") & "koning"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "king"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FENName As String
        Get
            If Me.Color = WHITE Then
                Return "K"
            Else
                Return "k"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IconName As String
        Get
            Return If(Me.Color = WHITE, "W", "B") & "King"
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        Dim Move As BoardMove
        Dim Column As Long, Row As Long
        Dim FromField As ChessField = pChessBoard.Fields(pFromFieldName)

        If King.InCheck(Me.Color, pChessBoard) = False Then 'Castling not allowed when In Check

            'Short Castling
            If Me.Color = WHITE And pChessBoard.WhiteShortCastlingAllowed Then
                If pChessBoard.Fields("f1").Piece Is Nothing _
                And pChessBoard.Fields("g1").Piece Is Nothing Then
                    Move = New BoardMove(Me, pFromFieldName, "g1", pCastle:=True)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If
            If Me.Color = BLACK And pChessBoard.BlackShortCastlingAllowed Then
                If pChessBoard.Fields("f8").Piece Is Nothing _
                And pChessBoard.Fields("g8").Piece Is Nothing Then
                    Move = New BoardMove(Me, pFromFieldName, "g8", pCastle:=True)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If

            'Long Castling
            If Me.Color = WHITE And pChessBoard.WhiteLongCastlingAllowed Then
                If pChessBoard.Fields("b1").Piece Is Nothing _
                And pChessBoard.Fields("c1").Piece Is Nothing _
                And pChessBoard.Fields("d1").Piece Is Nothing Then
                    Move = New BoardMove(Me, pFromFieldName, "c1", pCastle:=True)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If
            If Me.Color = BLACK And pChessBoard.BlackLongCastlingAllowed Then
                If pChessBoard.Fields("b1").Piece Is Nothing _
                And pChessBoard.Fields("c1").Piece Is Nothing _
                And pChessBoard.Fields("d1").Piece Is Nothing Then
                    Move = New BoardMove(Me, pFromFieldName, "c8", pCastle:=True)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If

        End If

        Column = FromField.Column
        Row = FromField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column
        Row = FromField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            ElseIf pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Return Moves
    End Function

    ''' <summary>To Test if the KING is In Check
    ''' Faster than getting all opponent moves and see if king is a target</summary>
    Public Shared Function InCheck(pColor As ChessColor, pChessBoard As ChessBoard) As Boolean
        Dim Distance As Long, Column As Long, Row As Long, Piece As ChessPiece
        Dim KingField As ChessField = FindKing(pColor, pChessBoard)

        'No King; No Check
        If KingField Is Nothing Then Return False

        'Straight upward
        For Row = KingField.Row + 1 To 8 Step 1
            Piece = pChessBoard.Fields(KingField.Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Row

        'Straight downward
        For Row = KingField.Row - 1 To 1 Step -1
            Piece = pChessBoard.Fields(KingField.Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Row

        'To the Right
        For Column = KingField.Column + 1 To 8 Step 1
            Piece = pChessBoard.Fields(Column, KingField.Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Column

        'To the Left
        For Column = KingField.Column - 1 To 1 Step -1
            Piece = pChessBoard.Fields(Column, KingField.Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Column

        'Direction Right Up
        For Distance = 1 To 8
            Column = KingField.Column + Distance
            Row = KingField.Row + Distance
            If pChessBoard.Fields.Exists(Column, Row) = False Then Exit For
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Right Down
        For Distance = 1 To 8
            Column = KingField.Column + Distance
            Row = KingField.Row - Distance
            If pChessBoard.Fields.Exists(Column, Row) = False Then Exit For
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Left Up
        For Distance = 1 To 8
            Column = KingField.Column - Distance
            Row = KingField.Row + Distance
            If pChessBoard.Fields.Exists(Column, Row) = False Then Exit For
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Left Down
        For Distance = 1 To 8
            Column = KingField.Column - Distance
            Row = KingField.Row - Distance
            If pChessBoard.Fields.Exists(Column, Row) = False Then Exit For
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                    Return True
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Pawn Left Down
        Column = KingField.Column - 1
        Row = KingField.Row + If(pColor = WHITE, 1, -1)
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.PAWN) Then
                    Return True
                End If
            End If
        End If

        'Pawn Right Down
        Column = KingField.Column + 1
        Row = KingField.Row + If(pColor = WHITE, 1, -1)
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.PAWN) Then
                    Return True
                End If
            End If
        End If

        'Knights
        Column = KingField.Column + 1
        Row = KingField.Row + 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column - 1
        Row = KingField.Row + 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column + 1
        Row = KingField.Row - 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column - 1
        Row = KingField.Row - 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column + 2
        Row = KingField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column + 2
        Row = KingField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column - 2
        Row = KingField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Column = KingField.Column - 2
        Row = KingField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            Piece = pChessBoard.Fields(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color <> pColor _
                And (Piece.Type = PieceType.KNIGHT) Then
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Public Shared Function InCheckAfterMove(pMove As BoardMove, pColor As ChessColor, pChessBoard As ChessBoard) As Boolean
        Dim Board As ChessBoard = New ChessBoard(pChessBoard.FEN)
        Board.PerformMove(pMove)
        Return King.InCheck(pColor, Board)
    End Function

    Public Shared Function CheckMate(pColor As ChessColor, pChessBoard As ChessBoard) As Boolean
        Dim KingField As ChessField = FindKing(pColor, pChessBoard)
        Dim PossibleMoves As List(Of BoardMove) = pChessBoard.AllPossibleMoves(pColor)
        If PossibleMoves.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function FindKing(pColor As ChessColor, pChessBoard As ChessBoard) As ChessField
        For Each Field As ChessField In pChessBoard.Fields
            If Field Is Nothing Then Continue For
            If Field.Piece Is Nothing Then Continue For
            If Field.Piece.Color = pColor _
            And Field.Piece.Type = PieceType.KING Then
                Return Field
            End If
        Next Field
        Return Nothing
    End Function

    Public Sub New(pColor As ChessColor)
        MyBase.New(pColor)
        Me.Color = pColor
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
