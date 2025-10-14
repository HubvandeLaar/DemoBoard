Public Class EngineResult

    Public Property EngineVariant As EngineVariant() '0=BestMove, 1=SecondBestMove, 2=ThirdBestMove)

    Public ReadOnly Property BestMove As EngineMove
        Get
            Return EngineVariant(0).FirstMove()
        End Get
    End Property

    Public ReadOnly Property Score As Integer
        Get
            Return EngineVariant(0).Score
        End Get
    End Property

    Public Sub New()
        ReDim EngineVariant(2)
        EngineVariant(0) = New EngineVariant(Engine.ScoreType.cp, 0, 0, "")
        EngineVariant(1) = New EngineVariant(Engine.ScoreType.cp, 0, 0, "")
        EngineVariant(2) = New EngineVariant(Engine.ScoreType.cp, 0, 0, "")
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return "Score " & Me.Score _
               & " BestMove " & "  " _
               & Me.BestMove.MoveText & [Enum].GetName(EngineVariant(0).ScoreType.GetType(), EngineVariant(0).ScoreType) & "  " _
               & String.Join(" ", EngineVariant(0).MoveList)
    End Function

    Protected Overrides Sub Finalize()
        EngineVariant(0) = Nothing
        EngineVariant(1) = Nothing
        EngineVariant(2) = Nothing

        MyBase.Finalize()
    End Sub
End Class
