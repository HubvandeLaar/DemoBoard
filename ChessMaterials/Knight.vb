Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlType>
Public Class Knight
    Inherits ChessPiece

    <XmlIgnore>
    Public Overrides ReadOnly Property Type As ChessPiece.PieceType
        Get
            Return PieceType.KNIGHT
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "Paard"
            Else
                Return "Knight"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "P"
            Else
                Return "N"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Shared ReadOnly Property KeyStroke(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return "P"
            Else
                Return "N"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Wit ", "Zwart ") & "paard"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "knight"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property FENName As String
        Get
            If Me.Color = WHITE Then
                Return "N"
            Else
                Return "n"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IconName As String
        Get
            Return If(Me.Color = WHITE, "W", "B") & "Knight"
        End Get
    End Property

    Public Overrides Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Dim Moves As New List(Of BoardMove)
        Dim Move As BoardMove
        Dim Column As Long, Row As Long
        Dim FromField As ChessField = pChessBoard.Fields(pFromFieldName)

        Column = FromField.Column + 1
        Row = FromField.Row + 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row + 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 1
        Row = FromField.Row - 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 1
        Row = FromField.Row - 2
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 2
        Row = FromField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column + 2
        Row = FromField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 2
        Row = FromField.Row + 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
                If King.InCheckAfterMove(Move, Me.Color, pChessBoard) = False Then Moves.Add(Move)
            End If
        End If

        Column = FromField.Column - 2
        Row = FromField.Row - 1
        If pChessBoard.Fields.Exists(Column, Row) = True Then
            If pChessBoard.Fields(Column, Row).Piece Is Nothing _
            OrElse pChessBoard.Fields(Column, Row).Piece.Color <> Me.Color Then  'Capture piece
                Move = New BoardMove(Me, pFromFieldName, pChessBoard.Fields(Column, Row).Name)
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
