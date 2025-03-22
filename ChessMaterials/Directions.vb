Option Explicit On

Public Class Directions
    Inherits List(Of Direction)
    Public Sub New()
        Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = 1})   'Up
        Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 1})   'Right-Up
        Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = 0})   'Right
        Me.Add(New Direction() With {.ColumnIncrement = 1, .RowIncrement = -1})  'Right-Down
        Me.Add(New Direction() With {.ColumnIncrement = 0, .RowIncrement = -1})  'Down
        Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = -1}) 'Left-Down
        Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 0})  'Left
        Me.Add(New Direction() With {.ColumnIncrement = -1, .RowIncrement = 1})  'Left-Up
    End Sub
End Class
