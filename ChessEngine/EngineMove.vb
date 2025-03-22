Public Class EngineMove
    Public ReadOnly Property MoveText As String
    Public ReadOnly Property FromFieldName As String
    Public ReadOnly Property ToFieldName As String
    Public ReadOnly Property PromotionPieceName As String
    Public ReadOnly Property EnPassant As Boolean

    Public Sub New(pMoveText As String)
        MoveText = pMoveText
        If pMoveText Like "[a-h][1-8][a-h][1-8]*" Then
            FromFieldName = Mid(pMoveText, 1, 2)
            ToFieldName = Mid(pMoveText, 3, 2)
            PromotionPieceName = If(pMoveText.Length = 5, UCase(Mid(pMoveText, 5, 1)), "")
            EnPassant = (pMoveText Like "*ep*")
        Else
            FromFieldName = ""
            ToFieldName = ""
            PromotionPieceName = ""
            EnPassant = False
        End If
    End Sub

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return Me.MoveText
    End Function

End Class