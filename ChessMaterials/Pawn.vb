Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlType>
Public Class Pawn
    Inherits ChessPiece

    <XmlIgnore>
    Public Overrides ReadOnly Property Type As ChessPiece.PieceType
        Get
            Return PieceType.PAWN
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "Pion"
            Else
                Return "Pawn"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return ""
        End Get
    End Property

    <XmlIgnore>
    Public Shared ReadOnly Property KeyStroke(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return " "
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Witte ", "Zwarte ") & "pion"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "pawn"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FENName As String
        Get
            If Me.Color = WHITE Then
                Return "P"
            Else
                Return "p"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IconName As String
        Get
            Return If(Me.Color = WHITE, "W", "B") & "Pawn"
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove), ToField As ChessField
        Dim Move As BoardMove
        Dim FromField As ChessField = pChessBoard.Fields(pFromFieldName)

        If FromField.Row = 1 _
        Or FromField.Row = 8 Then
            Return Moves 'No Possible Moves; Invalid Position for a Pawn
        End If

        'Promotion
        If FromField.Row = If(Me.Color = WHITE, 7, 2) Then
            'Promotion and Move straight Ahead
            ToField = pChessBoard.Fields(FromField.Column, FromField.Row + If(Me.Color = WHITE, 1, -1))
            If ToField.Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New QUEEN(Me.Color))
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Rook(Me.Color))
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Bishop(Me.Color))
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Knight(Me.Color))
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If

            'Promotion and Capture Left Ahead
            If FromField.Column > 1 Then
                ToField = pChessBoard.Fields(FromField.Column - 1, FromField.Row + If(Me.Color = WHITE, 1, -1))
                If ToField.Piece IsNot Nothing Then
                    If ToField.Piece.Color <> Me.Color Then
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Queen(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Rook(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Bishop(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Knight(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                    End If
                End If
            End If

            'Promotion and Çapture Right Ahead
            If FromField.Column < 8 Then
                ToField = pChessBoard.Fields(FromField.Column + 1, FromField.Row + If(Me.Color = WHITE, 1, -1))
                If ToField.Piece IsNot Nothing Then
                    If ToField.Piece.Color <> Me.Color Then
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Queen(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Rook(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Bishop(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                        Move = New BoardMove(Me, pFromFieldName, ToField.Name, pPromotionPiece:=New Knight(Me.Color))
                        If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                    End If
                End If
            End If

            Return Moves
        End If

        'Allways One-step Normal Move ahead
        ToField = pChessBoard.Fields(FromField.Column, FromField.Row + If(Me.Color = WHITE, 1, -1))
        If ToField.Piece Is Nothing Then
            Move = New BoardMove(Me, pFromFieldName, ToField.Name)
            If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            'And somtimes Also Two steps ahead
            If FromField.Row = If(Me.Color = WHITE, 2, 7) Then
                ToField = pChessBoard.Fields(FromField.Column, FromField.Row + If(Me.Color = WHITE, 2, -2))
                If ToField.Piece Is Nothing Then
                    Move = New BoardMove(Me, pFromFieldName, ToField.Name)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If
        End If

        'Capturing Left-Ahead
        If FromField.Column > 1 Then
            ToField = pChessBoard.Fields(FromField.Column - 1, FromField.Row + If(Me.Color = WHITE, 1, -1))
            If ToField.Piece IsNot Nothing Then
                If ToField.Piece.Color <> Me.Color Then
                    Move = New BoardMove(Me, pFromFieldName, ToField.Name)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If
        End If

        'Capturing Right-Ahead
        If FromField.Column < 8 Then
            ToField = pChessBoard.Fields(FromField.Column + 1, FromField.Row + If(Me.Color = WHITE, 1, -1))
            If ToField.Piece IsNot Nothing Then
                If ToField.Piece.Color <> Me.Color Then
                    Move = New BoardMove(Me, pFromFieldName, ToField.Name)
                    If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
                End If
            End If
        End If

        'En-Passant capturing
        If pChessBoard.EpFieldName <> "" Then
            ToField = pChessBoard.Fields(pChessBoard.EpFieldName)
            If Math.Abs(FromField.Column - ToField.Column) = 1 _
            And FromField.Row + If(Me.Color = WHITE, 1, -1) = ToField.Column Then
                Move = New BoardMove(Me, pFromFieldName, ToField.Name, pEnPassant:=True)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Return Moves
    End Function

    Public Sub New(pColor As ChessColor)
        MyBase.New(pColor)
        Me.Color = pColor
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

