Public Class EngineVariant
    Public ReadOnly Property ScoreType As Engine.ScoreType
    Public ReadOnly Property Score As Integer 'In Centipoints
    Public ReadOnly Property Depth As Integer
    Public ReadOnly Property MoveList As List(Of EngineMove)

    Public ReadOnly Property FirstMove() As EngineMove
        Get
            If MoveList.Count > 0 Then
                Return MoveList(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property MateIn() As Integer
        Get
            If Me.ScoreType = Engine.ScoreType.mate Then
                Return Score
            Else
                Return 0
            End If
        End Get
    End Property

    Public Sub New(pScoretype As Engine.ScoreType, pScore As Integer, pDepth As Integer, pMoveList As String)
        Me.ScoreType = pScoretype
        Me.Score = pScore
        Me.Depth = pDepth
        Me.MoveList = New List(Of EngineMove)
        Dim Moves() As String = pMoveList.Split(" ")
        For Each Move As String In Moves
            If Move <> "" Then
                Me.MoveList.Add(New EngineMove(Move))
            End If
        Next
    End Sub

End Class
