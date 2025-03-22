Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessGlobals.ChessLanguage
Imports ChessMaterials.ChessPiece

Public Class BoardMove

    Public Piece As ChessPiece
    Public FromFieldName As String
    Public ToFieldName As String
    Public Castle As Boolean
    Public PromotionPiece As ChessPiece
    Public EnPassant As Boolean
    Public Score As Integer

    Public Sub New(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                   Optional pPromotionPiece As ChessPiece = Nothing,
                   Optional pEnPassant As Boolean = False,
                   Optional pScore As Integer = 0)
        Me.Piece = pPiece
        Me.FromFieldName = pFromFieldName
        Me.ToFieldName = pToFieldName
        Me.Castle = False
        Me.PromotionPiece = pPromotionPiece
        Me.EnPassant = pEnPassant
        Me.Score = pScore

        If pPiece.Type = PieceType.KING Then
            If pPiece.Color = WHITE _
            And pFromFieldName = "e1" And pToFieldName = "g1" Then
                Me.Castle = True
            ElseIf pPiece.Color = WHITE _
            And pFromFieldName = "e1" And pToFieldName = "c1" Then
                Me.Castle = True
            ElseIf pPiece.Color = BLACK _
            And pFromFieldName = "e8" And pToFieldName = "g8" Then
                Me.Castle = True
            ElseIf pPiece.Color = BLACK _
            And pFromFieldName = "e8" And pToFieldName = "c8" Then
                Me.Castle = True
            End If
        End If
    End Sub

    Public Function Text(pBoardBefore As ChessBoard, pBoardAfter As ChessBoard, Optional pLanguage As ChessLanguage = ENGLISH) As String
        Dim CheckOrMate As String = ""
        If pBoardAfter.CheckMate(pBoardAfter.ActiveColor) Then
            CheckOrMate = "#"
        ElseIf pBoardAfter.InCheck(pBoardAfter.ActiveColor) Then
            CheckOrMate = "+"
        End If
        If Me.Castle = True Then
            If Me.ToFieldName = "g1" Or Me.ToFieldName = "g8" Then
                Return "O-O" & CheckOrMate
            Else
                Return "O-O-O" & CheckOrMate
            End If
        End If
        If Me.Piece.Type = PieceType.KING Then
            If Me.FromFieldName = "e1" And Me.ToFieldName = "g1" _
            Or Me.FromFieldName = "e8" And Me.ToFieldName = "g8" Then
                Return "O-O" & CheckOrMate
            ElseIf Me.FromFieldName = "e1" And Me.ToFieldName = "c1" _
            Or Me.FromFieldName = "e8" And Me.ToFieldName = "c8" Then
                Return "O-O-O" & CheckOrMate
            End If
        End If
        If Me.Piece.Type = PieceType.PAWN Then
            Return Me.FromFieldName _
                 & If(pBoardBefore(Me.ToFieldName).Piece Is Nothing, "-", "x") _
                 & Me.ToFieldName _
                 & If(EnPassant, "ep", "") _
                 & CheckOrMate
        Else
            Return Me.Piece.MoveName(pLanguage) _
                 & Me.FromFieldName _
                 & If(pBoardBefore(Me.ToFieldName).Piece Is Nothing, "-", "x") _
                 & Me.ToFieldName _
                 & CheckOrMate
        End If
    End Function

    Public Shared Operator =(pA As BoardMove, pB As BoardMove) As Boolean
        If pA.FromFieldName <> pB.FromFieldName _
        Or pA.ToFieldName <> pB.ToFieldName _
        Or pA.EnPassant <> pB.EnPassant _
        Or pA.Castle <> pB.Castle Then
            Return False
        End If
        If pA.Piece Is Nothing And pB.Piece Is Nothing Then
            Return True
        ElseIf pA.Piece Is Nothing Or pB.Piece Is Nothing Then
            Return False
        ElseIf pA.Piece.Type = pB.Piece.Type Then
            Return True
        Else
            Return False
        End If
    End Operator

    Public Shared Operator <>(pA As BoardMove, pB As BoardMove) As Boolean
        If pA = pB Then
            Return False
        Else
            Return True
        End If
    End Operator

    Public Shared Function ColumnNr(pFieldName As String) As Integer
        Return InStr("abcdefgh", Mid(pFieldName, 1, 1))
    End Function

    Public Shared Function ColumnName(pColumnNr As Integer) As String
        Return Mid("abcdefgh", pColumnNr, 1)
    End Function

    Public Shared Function Row(pFieldName As String) As Integer
        Return Val(Mid(pFieldName, 2, 1))
    End Function

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return Me.FromFieldName & " " & Me.ToFieldName
    End Function

    Protected Overrides Sub Finalize()
        Me.Piece = Nothing
        Me.PromotionPiece = Nothing

        MyBase.Finalize()
    End Sub
End Class
