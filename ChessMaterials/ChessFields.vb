Option Explicit On

Imports ChessGlobals
Imports Microsoft.VisualBasic.Strings

Public Class ChessFields
    Implements IEnumerable

    Private Fields(64) As ChessField

    Default Public ReadOnly Property Item(pColIndexOrName As Object, Optional pRow As Integer = 0) As ChessField
        Get
            Dim Row As Integer, Column As Integer, ColIndexOrName As String
            If pRow > 0 Then
                Row = pRow
                If TypeOf pColIndexOrName Is String Then
                    ColIndexOrName = CStr(pColIndexOrName)
                    Column = InStr("abcdefgh", ColIndexOrName)
                Else 'Assumed to be numeric
                    Column = CInt(pColIndexOrName)
                End If
                If Row < 0 Or Row > 8 Or Column < 0 Or Column > 8 Then
                    Throw New System.ArgumentOutOfRangeException(MessageText("ColumnRowRange", Column, Row))
                End If
                Return Fields((Row - 1) * 8 + Column)
            End If
            'pRow not specified
            If TypeOf pColIndexOrName Is String Then 'Assumed to be a name of length two
                ColIndexOrName = CStr(pColIndexOrName)
                Row = Val(Mid(ColIndexOrName, 2))
                Column = InStr("abcdefgh", Left(ColIndexOrName, 1))
                If Row < 1 Or Row > 8 Or Column < 1 Or Column > 8 Then
                    Throw New System.ArgumentOutOfRangeException(MessageText("ColumnRowRange", Column, Row))
                End If
                Return Fields((Row - 1) * 8 + Column)
            Else 'Assumed to be numeric specifying the index
                Return Fields(CInt(pColIndexOrName))
            End If
        End Get
    End Property

    ReadOnly Property Count() As Long
        Get
            Return Fields.Count
        End Get
    End Property

    Public Function GetEnumerator() As IEnumerator _
    Implements IEnumerable.GetEnumerator
        Return Fields.GetEnumerator
    End Function

    Public Sub Clear()
        For Each Field As ChessField In Me.Fields
            If Field Is Nothing Then Continue For
            Field.Piece = Nothing
            Field.Marker = Nothing
            Field.Text = Nothing
        Next Field
    End Sub

    Public Sub ClearMarkers()
        For Each Field As ChessField In Me.Fields
            If Field Is Nothing Then Continue For
            Field.Marker = Nothing
        Next Field
    End Sub

    Public Sub ClearTexts()
        For Each Field As ChessField In Me.Fields
            If Field Is Nothing Then Continue For
            Field.Text = Nothing
        Next Field
    End Sub

    Public Function Exists(pField As ChessField) As Boolean
        For Each Field As ChessField In Me.Fields
            If Field Is pField Then Return True
        Next Field
        Return False
    End Function

    Public Function Exists(pColumn As Long, pRow As Long) As Boolean
        If pColumn < 1 Or pColumn > 8 Then Return False
        If pRow < 1 Or pRow > 8 Then Return False
        Return True
    End Function

    Public Sub New()
        Dim I As Integer, R As Integer, C As Integer
        I = 0
        For R = 1 To 8
            For C = 1 To 8
                I = I + 1
                Fields(I) = New ChessField(C, R)
            Next C
        Next R
    End Sub

    Protected Overrides Sub Finalize()
        Me.Fields = Nothing

        MyBase.Finalize()
    End Sub

End Class

