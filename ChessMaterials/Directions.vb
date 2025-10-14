Option Explicit On

Imports ChessMaterials.ChessPiece

Public Class Directions
    Inherits List(Of Direction)

    Public Sub New(Optional pPieceType As PieceType = PieceType.QUEEN)
        Select Case pPieceType
            Case PieceType.QUEEN, PieceType.KING
                Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = 1})   'Up
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 1})   'Right-Up
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 0})   'Right
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = -1})  'Right-Down
                Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = -1})  'Down
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = -1}) 'Left-Down
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 0})  'Left
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 1})  'Left-Up
            Case PieceType.ROOK
                Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = 1})   'Up
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 0})   'Right
                Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = -1})  'Down
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 0})  'Left
            Case PieceType.BISHOP
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 1})   'Right-Up
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = -1})  'Right-Down
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = -1}) 'Left-Down
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 1})  'Left-Up
            Case PieceType.KNIGHT
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 2})  'Up-Up-Left
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 2})   'Up-Up-Right
                Me.Add(New Direction() With {.ColumnIncrement = 2, .RowIncrement = 1})   'Right-Right-Up
                Me.Add(New Direction() With {.ColumnIncrement = 2, .RowIncrement = -1})  'Right-Right-Down
                Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = -2})  'Down-Down-Right
                Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = -2}) 'Down-Down-Left
                Me.Add(New Direction() With {.ColumnIncrement = -2, .RowIncrement = -1}) 'Left-Left-Down
                Me.Add(New Direction() With {.ColumnIncrement = -2, .RowIncrement = 1})  'Left-Left-Up
            Case PieceType.PAWN 'Not being used
            Case Else
                Throw New ArgumentOutOfRangeException("Invalid PieceType")
        End Select
    End Sub

End Class
