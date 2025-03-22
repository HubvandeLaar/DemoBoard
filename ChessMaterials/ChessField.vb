Option Explicit On

Imports System.Drawing
Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessMaterials.ChessPiece

Public Class ChessField
    Public Name As String
    Public Column As Integer
    Public Row As Integer
    Public Piece As ChessPiece
    Public Marker As Marker
    Public Text As Text
    Public ParentBoard As ChessBoard

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

    Public Function DefendedBy(pColor As ChessColor) As List(Of ChessField) 'NB piece(s) from this postion
        Return DefendedOrAttackedBy(pColor)
    End Function
    Public Function AttackedBy(pColor As ChessColor) As List(Of ChessField) 'NB piece(s) from this postion
        Return DefendedOrAttackedBy(pColor)
    End Function
    Private Function DefendedOrAttackedBy(pColor As ChessColor) As List(Of ChessField) 'NB piece(s) from this postion
        Dim DefendersOrAttackers As New List(Of ChessField)
        Dim Distance As Integer, Column As Integer, Row As Integer, Piece As ChessPiece

        If Me.Piece.Type = PieceType.KING Then
            'King's defender's always come too late...
            Return DefendersOrAttackers 'Zero
        End If

        'Straight upward
        For Distance = 1 To 8
            Column = Me.Column
            Row = Me.Row + Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Straight downward
        For Distance = 1 To 8
            Column = Me.Column
            Row = Me.Row - Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'To the Right
        For Distance = 1 To 8
            Column = Me.Column + Distance
            Row = Me.Row
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
                Exit For 'No more Moves in this line
            End If
        Next Distance

        'To the Left
        For Distance = 1 To 8
            Column = Me.Column - Distance
            Row = Me.Row
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.ROOK) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Direction Right Up
        For Distance = 1 To 8
            Column = Me.Column + Distance
            Row = Me.Row + Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Direction Right Down
        For Distance = 1 To 8
            Column = Me.Column + Distance
            Row = Me.Row - Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Direction Left Up
        For Distance = 1 To 8
            Column = Me.Column - Distance
            Row = Me.Row + Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Direction Left Down
        For Distance = 1 To 8
            Column = Me.Column - Distance
            Row = Me.Row - Distance
            If ParentBoard.Exists(Column, Row) = False Then Exit For
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing Then
                If Piece.Color = pColor Then
                    If Distance = 1 _
                    And Piece.Type = PieceType.KING Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    ElseIf (Piece.Type = PieceType.QUEEN Or Piece.Type = PieceType.BISHOP) Then
                        DefendersOrAttackers.Add(ParentBoard(Column, Row))
                        'Continue for
                    Else
                        Exit For 'No more Pieces in this line
                    End If
                Else
                    Exit For
                End If
            End If
        Next Distance

        'Pawn Left Down or Up
        Column = Me.Column - 1
        Row = Me.Row + If(pColor = WHITE, -1, 1)
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.PAWN) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        'Pawn Right Down or Up
        Column = Me.Column + 1
        Row = Me.Row + If(pColor = WHITE, -1, 1)
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.PAWN) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        'Knights
        Column = Me.Column + 1
        Row = Me.Row + 2
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column - 1
        Row = Me.Row + 2
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column + 1
        Row = Me.Row - 2
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column - 1
        Row = Me.Row - 2
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column + 2
        Row = Me.Row + 1
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column + 2
        Row = Me.Row - 1
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column - 2
        Row = Me.Row + 1
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Column = Me.Column - 2
        Row = Me.Row - 1
        If ParentBoard.Exists(Column, Row) = True Then
            Piece = ParentBoard(Column, Row).Piece
            If Piece IsNot Nothing _
            AndAlso Piece.Color = pColor _
            AndAlso (Piece.Type = PieceType.KNIGHT) Then
                DefendersOrAttackers.Add(ParentBoard(Column, Row))
            End If
        End If

        Return DefendersOrAttackers
    End Function

    Public Function FirstPieceInLine(pDirection As Direction) As ChessField
        Dim C As Integer = Me.Column
        Dim R As Integer = Me.Row
        While (Me.ParentBoard.Exists(C, R) = True _
               AndAlso Me.ParentBoard(C, R).Piece Is Nothing)
            C += pDirection.ColumnIncrement
            R += pDirection.RowIncrement
        End While
        If Me.ParentBoard.Exists(C, R) = True Then
            Return Me.ParentBoard(C, R)
        Else
            Return Nothing
        End If
    End Function


    Public Sub New(pColumn As Integer, pRow As Integer, pParentBoard As ChessBoard)
        Column = pColumn
        Row = pRow
        Name = Me.ColumnName & Me.RowName
        Me.ParentBoard = pParentBoard
    End Sub

    Protected Overrides Sub Finalize()
        Me.Piece = Nothing
        Me.Marker = Nothing
        Me.Text = Nothing
        Me.ParentBoard = Nothing

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
