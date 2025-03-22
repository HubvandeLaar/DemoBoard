Option Explicit On

Public Class Direction
    Public Property ColumnIncrement As Integer
    Public Property RowIncrement As Integer
    Public ReadOnly Property Diagonal As Boolean
        Get
            If ColumnIncrement = 0 _
            Or RowIncrement = 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property
    Public ReadOnly Property OppositDirection As Direction
        Get
            Return New Direction With {.ColumnIncrement = Me.ColumnIncrement * -1,
                                       .RowIncrement = Me.RowIncrement * -1}
        End Get
    End Property
End Class
