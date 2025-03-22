Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Devices

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

    <XmlIgnore>
    Public Overrides ReadOnly Property Value As Integer
        Get
            Return 9999
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        Dim Move As BoardMove
        Dim Column As Long, Row As Long
        Dim FromField As ChessField = pChessBoard(pFromFieldName)

        If pChessBoard.ShortCastlingAllowed(pFromFieldName) Then
            Move = New BoardMove(Me, pFromFieldName, If(Me.Color = WHITE, "g1", "g8"))
            Moves.Add(Move)
        End If

        If pChessBoard.LongCastlingAllowed(pFromFieldName) Then
            Move = New BoardMove(Me, pFromFieldName, If(Me.Color = WHITE, "c1", "c8"))
            Moves.Add(Move)
        End If

        Column = FromField.Column
        Row = FromField.Row + 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row + 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row - 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column
        Row = FromField.Row - 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row - 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row + 1
        If pChessBoard.Exists(Column, Row) = True Then
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            ElseIf pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
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
