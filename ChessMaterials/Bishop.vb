Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlType>
Public Class Bishop
    Inherits ChessPiece

    <XmlIgnore>
    Public Overrides ReadOnly Property Type As ChessPiece.PieceType
        Get
            Return PieceType.BISHOP
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "Loper"
            Else
                Return "Bishop"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "L"
            Else
                Return "B"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Shared ReadOnly Property KeyStroke(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "L"
            Else
                Return "B"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Witte ", "Zwarte ") & "loper"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "bishop"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FENName As String
        Get
            If Me.Color = WHITE Then
                Return "B"
            Else
                Return "b"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IconName As String
        Get
            Return If(Me.Color = WHITE, "W", "B") & "Bishop"
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Value As Integer
        Get
            Return 3
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        Dim Move As BoardMove
        Dim Distance As Long, Column As Long, Row As Long
        Dim FromField As ChessField = pChessBoard(pFromFieldName)

        'Direction Right Up
        For Distance = 1 To 8
            Column = FromField.Column + Distance
            Row = FromField.Row + Distance
            If pChessBoard.Exists(Column, Row) = False Then Exit For
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Right Down
        For Distance = 1 To 8
            Column = FromField.Column + Distance
            Row = FromField.Row - Distance
            If pChessBoard.Exists(Column, Row) = False Then Exit For
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Left Up
        For Distance = 1 To 8
            Column = FromField.Column - Distance
            Row = FromField.Row + Distance
            If pChessBoard.Exists(Column, Row) = False Then Exit For
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'Direction Left Down
        For Distance = 1 To 8
            Column = FromField.Column - Distance
            Row = FromField.Row - Distance
            If pChessBoard.Exists(Column, Row) = False Then Exit For
            If pChessBoard(Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

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
