Public Class EngineResult

    ReadOnly Property Score As Integer
    ReadOnly Property BestMove As EngineMove
    ReadOnly Property Mate As String
    ReadOnly Property Movelist As List(Of EngineMove)

    Public Sub New(pScore As Integer, pBestMove As EngineMove, pMate As String, pMovelist As String)
        Me.Score = pScore
        Me.BestMove = pBestMove
        Me.Mate = pMate
        Dim Moves() As String = pMovelist.Split(" ")
        Me.Movelist = New List(Of EngineMove)
        For Each Move As String In Moves
            If Move <> "" Then
                Me.Movelist.Add(New EngineMove(Move))
            End If
        Next
    End Sub

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return "Score " & Me.Score & " BestMove " & Me.BestMove.MoveText & " pv " & String.Join(" ", Me.Movelist)
    End Function

End Class
