Public Class PGNVariants
    Inherits List(Of PGNVariant)

    Public CurrentMoveIndex As Long

    Private ReadOnly PGNHalfMoves As PGNHalfMoves
    Private ReadOnly FirstMoveOfParentVariant As Long
    Private ReadOnly LastMoveOfParentVariant As Long

    Public ReadOnly Property CurrentVariantIndex As Long
        Get
            For I As Integer = 0 To Me.Count - 1
                If Me(I).ParentFirstMoveIndex = CurrentMoveIndex Then
                    Return I
                End If
                If CurrentMoveIndex >= Me(I).FromIndex _
                And CurrentMoveIndex <= Me(I).ToIndex Then
                    Return I
                End If
            Next I
            Return -1
        End Get
    End Property

    Public Sub New(pCurrentMoveIndex As Long, pPGNHalfMoves As PGNHalfMoves, Optional pDemotion As Boolean = False)
        CurrentMoveIndex = pCurrentMoveIndex
        PGNHalfMoves = pPGNHalfMoves

        If pDemotion = True _
        AndAlso PGNHalfMoves(CurrentMoveIndex).SubVariants.Count > 0 Then
            'If Demotion then ParentVariant can be demoted by pointing to the first Move
            FirstMoveOfParentVariant = CurrentMoveIndex
        Else
            'pCurrentMoveIndex indicates currentmove; of a move within a variant
            'Find first move of ParentVariant
            FirstMoveOfParentVariant = FindFirstMoveOfParentVariant(CurrentMoveIndex)
        End If

        If FirstMoveOfParentVariant = -1 Then
            MsgBox(ChessGlobals.MessageText("InvalidMove"))
            Exit Sub
        End If

        Dim SubVariants As List(Of PGNHalfMove) = PGNHalfMoves(FirstMoveOfParentVariant).SubVariants
        For Each Move As PGNHalfMove In SubVariants
            Me.AddVariant(Move.Index, FindLastMoveOfVariant(Move.Index))
        Next Move

        'Hoofdvariant nog invoegen
        LastMoveOfParentVariant = FindLastMoveOfVariant(Me.Last.ToIndex + 1)
        Me.InsertParentVariant(FirstMoveOfParentVariant, Me.Last.ToIndex + 1, LastMoveOfParentVariant)
    End Sub

    Public Sub InsertParentVariant(pMainFirstMoveIndex As Long, pFromIndex As Long, pToIndex As Long)
        Me.Insert(0, New PGNVariant(pMainFirstMoveIndex, pFromIndex, pToIndex))
    End Sub

    Public Sub AddVariant(pFromIndex As Long, pToIndex As Long)
        Me.Add(New PGNVariant(-1, pFromIndex, pToIndex))
    End Sub

    Public Sub Promote()
        Dim VariantIndex As Long = CurrentVariantIndex()
        If VariantIndex = -1 _
        Or VariantIndex = 0 Then
            Exit Sub 'Invalid position or can't promote the Parent variant
        End If
        If VariantIndex = 1 Then
            'First Variant Promotes to Parent Variant
            Me(1).VariantLevelIncrement = -1
            'So Parent Variant Demotes to Subvariant
            Me(0).VariantLevelIncrement = 1
        End If
        Dim PGNVariant As PGNVariant = Me(VariantIndex)
        Me.RemoveAt(VariantIndex)
        Me.Insert(VariantIndex - 1, PGNVariant)
        UpdateHalfMoves()
    End Sub

    Public Sub Demote()
        Dim VariantIndex As Long = CurrentVariantIndex()
        If VariantIndex = -1 _
        Or VariantIndex >= Me.Count - 1 Then
            Exit Sub 'Invalid position or can't demote te last variant
        End If
        If VariantIndex = 0 Then
            'Parent Variant Demotes to Subvariant
            Me(0).VariantLevelIncrement = 1
            'So First Variant Promotes to Parent Variant
            Me(1).VariantLevelIncrement = -1
        End If
        Dim PGNVariant As PGNVariant = Me(VariantIndex)
        Me.RemoveAt(VariantIndex)
        Me.Insert(VariantIndex + 1, PGNVariant)
        UpdateHalfMoves()
    End Sub

    Public Sub UpdateHalfMoves()
        Dim NewCurrentMoveIndex As Long = CurrentMoveIndex
        Dim Moves As New List(Of PGNHalfMove)
        Moves.AddRange(PGNHalfMoves)
        PGNHalfMoves.Clear()

        Dim ParentVariantLevel As Long = Moves(FirstMoveOfParentVariant).VariantLevel
        Dim ParentVariantNumber As Long = Moves(FirstMoveOfParentVariant).VariantNumber

        'Transfer not impacted part before these variants
        If Me.First.ParentFirstMoveIndex > 0 Then
            PGNHalfMoves.AddRange(Moves.GetRange(0, Me.First.ParentFirstMoveIndex))
        Else
            PGNHalfMoves.AddRange(Moves.GetRange(0, Me(1).ParentFirstMoveIndex))
        End If

        'Transfer ParentFirstMove
        If Me.First.ParentFirstMoveIndex <> -1 Then
            'New ParentVariant is Old ParentVariant
            Dim Move As PGNHalfMove = Moves(FirstMoveOfParentVariant)
            If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
            PGNHalfMoves.Add(Move, False)
        Else
            'New ParentVariant is Old SubVariant
            Dim Move As PGNHalfMove = Moves(Me.First.FromIndex)
            Move.VariantLevel = ParentVariantLevel
            Move.VariantNumber = ParentVariantNumber
            If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
            PGNHalfMoves.Add(Move, False)
        End If

        'Transfer SubVariants one by one
        For V As Integer = 1 To Me.Count - 1 'Skip first (0) because this is the new parent variant
            Dim PGNVariant As PGNVariant = Me(V)
            If PGNVariant.ParentFirstMoveIndex <> -1 Then
                'Old ParentVariant becomes Subvariant
                'First Move
                Dim Move = Moves(PGNVariant.ParentFirstMoveIndex)
                Move.VariantLevel += PGNVariant.VariantLevelIncrement
                Move.VariantNumber = V
                If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
                PGNHalfMoves.Add(Move, False)
                'Rest of Moves
                For I As Integer = PGNVariant.FromIndex To PGNVariant.ToIndex
                    Move = Moves(I)
                    Move.VariantLevel += PGNVariant.VariantLevelIncrement
                    If Move.VariantLevel = (ParentVariantLevel + 1) Then
                        Move.VariantNumber = V
                    End If
                    If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
                    PGNHalfMoves.Add(Move, False)
                Next I
            Else
                'Old SubVariant remains SubVariant, Only VariantNumber changes
                For I As Integer = Me(V).FromIndex To Me(V).ToIndex
                    Dim Move As PGNHalfMove = Moves(I)
                    If Move.VariantLevel = (ParentVariantLevel + 1) Then
                        Move.VariantNumber = V
                    End If
                    If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
                    PGNHalfMoves.Add(Move, False)
                Next I
            End If
        Next

        'Add Rest of Moves of ParentVariant
        If Me.First.ParentFirstMoveIndex <> -1 Then
            'New ParentVariant is Old ParentVariant Nothing Changes
            For I As Integer = Me.First.FromIndex To Me.First.ToIndex
                Dim Move As PGNHalfMove = Moves(I)
                If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
                PGNHalfMoves.Add(Move, False)
            Next I
        Else
            'New ParentVariant is Old SubVariant
            For I As Integer = Me.First.FromIndex + 1 To Me.First.ToIndex 'First move already inserted before all subvariants
                Dim Move As PGNHalfMove = Moves(I)
                Move.VariantLevel += Me.First.VariantLevelIncrement
                If Move.VariantLevel = ParentVariantLevel Then
                    Move.VariantNumber = ParentVariantNumber
                End If
                If CurrentMoveIndex = Move.Index Then NewCurrentMoveIndex = PGNHalfMoves.Count
                PGNHalfMoves.Add(Move, False)
            Next I
        End If

        'Transfer Not Impacted Part after the subvariants
        If PGNHalfMoves.Count < Moves.Count Then
            PGNHalfMoves.AddRange(Moves.GetRange(Moves.Count, PGNHalfMoves.Count - Moves.Count))
        End If

        CurrentMoveIndex = NewCurrentMoveIndex
        PGNHalfMoves.ReNumber()
    End Sub

    Private Function FindLastMoveOfVariant(pFirstMove) As Long
        For I As Integer = pFirstMove + 1 To PGNHalfMoves.Count - 1
            If PGNHalfMoves(I).VariantLevel < PGNHalfMoves(pFirstMove).VariantLevel Then
                Return I - 1
            End If
            If PGNHalfMoves(I).VariantLevel = PGNHalfMoves(pFirstMove).VariantLevel _
            And PGNHalfMoves(I).VariantNumber <> PGNHalfMoves(pFirstMove).VariantNumber Then
                Return I - 1
            End If
        Next
        Return PGNHalfMoves.Count - 1
    End Function

    Private Function FindFirstMoveOfParentVariant(pCurrentMoveIndex) As Long
        If PGNHalfMoves(pCurrentMoveIndex).VariantLevel = 0 Then 'Main Level
            'Exception for clicking mainlevel; Find related move with subvariants
            For I As Integer = pCurrentMoveIndex - 1 To 0 Step -1
                If PGNHalfMoves(I).SubVariants.Count > 0 Then
                    Return I
                End If
            Next I
        End If

        For I As Integer = pCurrentMoveIndex - 1 To 0 Step -1
            If PGNHalfMoves(I).VariantLevel < PGNHalfMoves(pCurrentMoveIndex).VariantLevel Then
                Return I
            End If
        Next I

        'No variant found
        Return -1
    End Function

End Class


