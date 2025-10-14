Option Explicit On

Imports ChessGlobals
Imports ChessMaterials.ChessPiece

Public Class ChessField
    Public Property Name As String
    Public Property Column As Integer
    Public Property Row As Integer
    Public Property Piece As ChessPiece
    Public Property Marker As Marker
    Public Property Text As Text

    Private gParentBoard As ChessBoard

    Public ReadOnly Property ColumnName As String
        Get
            Return Mid("abcdefgh", Me.Column, 1)
        End Get
    End Property

    Public ReadOnly Property RowName As String
        Get
            Return Mid("12345678", Me.Row, 1)
        End Get
    End Property

    ''' <summary>Returns the Name of the Field based on a Point as used for Arrows</summary>
    Public Shared Function FieldName(pPoint As String) As String
        Dim Values() As String = pPoint.Split(";")
        Dim Column As String, Row As String
        Select Case Val(Values(0))
            Case 0 To 9 : Column = "a"
            Case 10 To 19 : Column = "b"
            Case 20 To 29 : Column = "c"
            Case 30 To 39 : Column = "d"
            Case 40 To 49 : Column = "e"
            Case 50 To 59 : Column = "f"
            Case 60 To 69 : Column = "g"
            Case 70 To 79 : Column = "h"
            Case Else : Column = ""
        End Select
        Select Case Val(Values(1))
            Case 0 To 9 : Row = "8"
            Case 10 To 19 : Row = "7"
            Case 20 To 29 : Row = "6"
            Case 30 To 39 : Row = "5"
            Case 40 To 49 : Row = "4"
            Case 50 To 59 : Row = "3"
            Case 60 To 69 : Row = "2"
            Case 70 To 79 : Row = "1"
            Case Else : Row = ""
        End Select
        Return Column & Row
    End Function

    ''' <summary>Returns if the Field is a Dark Field</summary>
    Public Function DarkField() As Boolean
        If (Row + Column) Mod 2 = 0 Then
            DarkField = True
        Else
            DarkField = False
        End If
    End Function


    ''' <summary>Returns True if the field contains a Piece with specified Type and Color</summary>
    Public Function IsPiece(pPieceType As PieceType, pColor As ChessColor) As Boolean
        If Me.Piece Is Nothing Then Return False
        If Me.Piece.Type <> pPieceType Then Return False
        If Me.Piece.Color <> pColor Then Return False
        Return True
    End Function

    ''' <summary>Returns the Fields with Pieces that attack this Field</summary>
    Public Function AttackedBy(pColor As ChessColor) As List(Of ChessField)
        Dim Attackers As New List(Of ChessField)
        Dim Moves As List(Of BoardMove) = Me.gParentBoard.AllPossibleMoves(pColor)
        For Each Move As BoardMove In Moves
            If Move.ToFieldName = Me.Name Then
                Attackers.Add(Me.gParentBoard(Move.FromFieldName))
            End If
        Next Move
        Return Attackers
    End Function

    ''' <summary>Returns the Fields with Pieces that defend this Field</summary>
    Public Function DefendedBy(pColor As ChessColor) As List(Of ChessField)
        'Change color of Piece of the board, and look how many valid moves there are to this field
        Dim Board As New ChessBoard(Me.gParentBoard.FEN)
        Board(Me.Name).Piece = New Knight(pColor.Opponent) 'Set Opponent Piece from this field
        Dim Defenders As New List(Of ChessField)
        Dim Moves As List(Of BoardMove) = Board.AllPossibleMoves(pColor)
        For Each Move As BoardMove In Moves
            If Move.ToFieldName = Me.Name Then
                Defenders.Add(Me.gParentBoard(Move.FromFieldName))
            End If
        Next Move
        Return Defenders
    End Function

    ''' <summary>Returns the first Field with a Piece, starting from Me in specified Direction</summary>
    Public Function FirstPieceInLine(pDirection As Direction) As ChessField
        Dim C As Integer = Me.Column
        Dim R As Integer = Me.Row
        While (Me.gParentBoard.Exists(C, R) = True _
               AndAlso Me.gParentBoard(C, R).Piece Is Nothing)
            C += pDirection.ColumnIncrement
            R += pDirection.RowIncrement
        End While
        If Me.gParentBoard.Exists(C, R) = True Then
            Return Me.gParentBoard(C, R)
        Else
            Return Nothing
        End If
    End Function

    Public Sub New(pColumn As Integer, pRow As Integer, pParentBoard As ChessBoard)
        Column = pColumn
        Row = pRow
        Name = Me.ColumnName & Me.RowName
        Me.gParentBoard = pParentBoard
    End Sub

    Protected Overrides Sub Finalize()
        Me.Piece = Nothing
        Me.Marker = Nothing
        Me.Text = Nothing
        Me.gParentBoard = Nothing

        MyBase.Finalize()
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        If Me.Piece Is Nothing Then
            Return Me.Name
        Else
            Return Me.Name & " " & Me.Piece.ToString()
        End If
    End Function

End Class
