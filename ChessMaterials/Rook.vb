Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlType>
Public Class Rook
    Inherits ChessPiece

    <XmlIgnore>
    Public Overrides ReadOnly Property Type As ChessPiece.PieceType
        Get
            Return PieceType.ROOK
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "Toren"
            Else
                Return "Rook"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "T"
            Else
                Return "R"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Shared ReadOnly Property KeyStroke(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "T"
            Else
                Return "R"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Witte ", "Zwarte ") & "toren"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "rook"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FENName As String
        Get
            If Me.Color = WHITE Then
                Return "R"
            Else
                Return "r"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IconName As String
        Get
            Return If(Me.Color = WHITE, "W", "B") & "Rook"
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Value As Integer
        Get
            Return 5
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        Dim Move As BoardMove
        Dim Row As Long, Column As Long
        Dim FromField As ChessField = pChessBoard(pFromFieldName)

        'Straight upward
        For Row = FromField.Row + 1 To 8 Step 1
            If pChessBoard(FromField.Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(FromField.Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(FromField.Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(FromField.Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Row

        'Straight downward
        For Row = FromField.Row - 1 To 1 Step -1
            If pChessBoard(FromField.Column, Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(FromField.Column, Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(FromField.Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(FromField.Column, Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Row

        'To the Right
        For Column = FromField.Column + 1 To 8 Step 1
            If pChessBoard(Column, FromField.Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, FromField.Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, FromField.Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, FromField.Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Column

        'To the Left
        For Column = FromField.Column - 1 To 1 Step -1
            If pChessBoard(Column, FromField.Row).Piece Is Nothing Then
                Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, FromField.Row).Name)
                If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
            Else
                If pChessBoard(Column, FromField.Row).Piece.Color <> Me.Color Then  'Capture piece
                    Move = New BoardMove(Me, pFromFieldName, pChessBoard(Column, FromField.Row).Name)
                    If pChessBoard.InCheckAfterMove(Move, Me.Color) = False Then Moves.Add(Move)
                End If
                Exit For 'No more Moves in this line
            End If
        Next Column

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
