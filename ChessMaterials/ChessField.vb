Option Explicit On

Imports ChessGlobals

Public Class ChessField
    Public Name As String
    Public Column As Integer
    Public Row As Integer
    Public Piece As ChessPiece
    Public Marker As Marker
    Public Text As Text

    Public Function DarkField() As Boolean
        If (Row + Column) Mod 2 = 0 Then
            DarkField = True
        Else
            DarkField = False
        End If
    End Function

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

    Public Sub New(pColumn As Integer, pRow As Integer)
        Column = pColumn
        Row = pRow
        Name = Me.ColumnName & Me.RowName
    End Sub

    Protected Overrides Sub Finalize()
        Me.Piece = Nothing
        Me.Marker = Nothing
        Me.Text = Nothing

        MyBase.Finalize()
    End Sub

    Public Overrides Function ToString() As String
        If Me.Piece Is Nothing Then
            Return Me.Name
        Else
            Return Me.Name & " " & Me.Piece.ToString()
        End If
    End Function

End Class
