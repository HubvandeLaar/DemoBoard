Option Explicit On

Imports ChessGlobals

Public Class EngineResults

    Public ReadOnly Property Score As Integer 'In Centipoints
    Public ReadOnly Property Before As EngineResult
    Public ReadOnly Property After As EngineResult

    Public Sub New(pBefore As EngineResult, pAfter As EngineResult)
        'pScoreBefore with perspective of pColor
        'pScoreAfter with perspecive of Opponent
        Me.Score = (pAfter.Score * -1) - pBefore.Score
        Before = pBefore
        After = pAfter
    End Sub

End Class
