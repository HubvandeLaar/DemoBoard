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

    ''' <summary>Returns all valid possible moves</summary>
    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove), Move As BoardMove
        Dim FromField As ChessField = pChessBoard(pFromFieldName)

        For Each Direction As Direction In New Directions(Me.Type)
            For Distance As Long = 1 To 8
                Dim Column As Long = FromField.Column + (Distance * Direction.ColumnIncrement)
                Dim Row As Long = FromField.Row + (Distance * Direction.RowIncrement)
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
        Next Direction

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
