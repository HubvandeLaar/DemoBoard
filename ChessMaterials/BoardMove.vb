Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
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
                   Optional pCastle As Boolean = False,
                   Optional pPromotionPiece As ChessPiece = Nothing,
                   Optional pEnPassant As Boolean = False,
                   Optional pScore As Integer = 0)
        Me.Piece = pPiece
        Me.FromFieldName = pFromFieldName
        Me.ToFieldName = pToFieldName
        Me.Castle = pCastle
        Me.PromotionPiece = pPromotionPiece
        Me.EnPassant = pEnPassant
        Me.Score = pScore

        'Kweet niet of dit nodig is......
        If pPiece.Type = PieceType.KING _
        And pPiece.Color = WHITE _
        And pFromFieldName = "e1" And pToFieldName = "g1" Then
            Me.Castle = True
        ElseIf pPiece.Type = PieceType.KING _
        And pPiece.Color = WHITE _
        And pFromFieldName = "e1" And pToFieldName = "c1" Then
            Me.Castle = True
        ElseIf pPiece.Type = PieceType.KING _
        And pPiece.Color = BLACK _
        And pFromFieldName = "e8" And pToFieldName = "g8" Then
            Me.Castle = True
        ElseIf pPiece.Type = PieceType.KING _
        And pPiece.Color = BLACK _
        And pFromFieldName = "e8" And pToFieldName = "c8" Then
            Me.Castle = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Me.Piece = Nothing
        Me.PromotionPiece = Nothing

        MyBase.Finalize()
    End Sub
End Class
