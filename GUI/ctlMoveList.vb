Option Explicit On

Imports PGNLibrary
Imports ChessGlobals.ChessColor

Public Class ctlMoveList

    Private gIndent As Integer = 25
    Private gHideAfterSelectedHalfMove As Boolean = False
    Private gSelectedHalfMove As PGNHalfMove 'UnColors previously selected move and Colors the new one
    '                                         Also needed to capture previous position fi. at Click

    Public Shadows Event RightClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove)
    Public Shadows Event Clicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove)
    Public Shadows Event DoubleClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove)

    Public Property SelectedHalfMove As PGNHalfMove
        Set(pSelectedHalfMove As PGNHalfMove)
            Dim MoveListRow As ctlMoveListRow

            'Uncolor previously selected HalfMove
            If gSelectedHalfMove IsNot Nothing Then
                MoveListRow = FindMoveListRow(gSelectedHalfMove)
                If MoveListRow IsNot Nothing Then 'Selected Halfmove not in list anymore (due to clear of Halfmoves after dropping piece in Play mode)
                    MoveListRow.WhiteSelected = False
                    MoveListRow.BlackSelected = False
                End If
            End If

            gSelectedHalfMove = pSelectedHalfMove

            If gSelectedHalfMove Is Nothing Then
                ShowTop()
            Else
                MoveListRow = FindMoveListRow(gSelectedHalfMove)
                If MoveListRow Is Nothing Then Exit Property
                'Color selected HalfMove
                EnsureMoveListRowVisible(MoveListRow)
                If pSelectedHalfMove.Color = WHITE Then
                    MoveListRow.WhiteSelected = True
                Else
                    MoveListRow.BlackSelected = True
                End If

                'Ensure next move is also visible
                If MoveListRow.Expandable _
                AndAlso MoveListRow.Expanded = False Then
                    MoveListRow.Expanded = True
                    Call Expand(MoveListRow)
                    Me.ShowMoveList()
                End If
            End If
        End Set
        Get
            Return gSelectedHalfMove
        End Get
    End Property

    Public Property Indent As Integer
        Set(pIndent As Integer)
            gIndent = pIndent
        End Set
        Get
            Return gIndent
        End Get
    End Property

    Public Property HideAfterSelectedHalfMove As Boolean
        Set(pHideAfterSelectedHalfMove As Boolean)
            gHideAfterSelectedHalfMove = pHideAfterSelectedHalfMove
        End Set
        Get
            Return gHideAfterSelectedHalfMove
        End Get
    End Property

    Private Sub MoveListRow_RightClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove)
        Dim PreviousHalfMove As PGNHalfMove = Me.SelectedHalfMove
        Me.SelectedHalfMove = pHalfMove
        RaiseEvent RightClicked(pMoveListRow, pHalfMove, PreviousHalfMove)
    End Sub

    Private Sub MoveListRow_Clicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove)
        Dim PreviousHalfMove As PGNHalfMove = Me.SelectedHalfMove
        Me.SelectedHalfMove = pHalfMove
        RaiseEvent Clicked(pMoveListRow, pHalfMove, PreviousHalfMove)
    End Sub

    Private Sub MoveListRow_DoubleClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove)
        Dim PreviousHalfMove As PGNHalfMove = Me.SelectedHalfMove
        Me.SelectedHalfMove = pHalfMove
        RaiseEvent DoubleClicked(pMoveListRow, pHalfMove, PreviousHalfMove)
    End Sub

    Private Sub MoveListRow_ExpandClicked(pMoveListRow As ctlMoveListRow)
        Call Me.Expand(pMoveListRow)
        Me.ShowMoveList()
    End Sub

    Private Sub Expand(pMoveListRow As ctlMoveListRow)
        Dim MoveListRowFound As Boolean = False
        For Each MoveListRow As ctlMoveListRow In pnlMoveList.Controls
            If MoveListRowFound = False Then
                'Finding involved Variant
                If MoveListRow Is pMoveListRow Then
                    MoveListRowFound = True
                    Continue For 'Skip the involved First Move for this already is Perceptable and set to Expanded
                End If
                Continue For
            End If

            'Update Variant and Subvariant First Moves Visibilty
            If MoveListRow.VariantLevel < pMoveListRow.VariantLevel Then _
                'Return because Higher Variant Level found
                Exit For
            ElseIf MoveListRow.VariantLevel = pMoveListRow.VariantLevel _
            AndAlso MoveListRow.VariantNumber <> pMoveListRow.VariantNumber Then
                'Return because Next Variant of same level found 
                Exit For
            ElseIf MoveListRow.VariantLevel = pMoveListRow.VariantLevel Then
                MoveListRow.Perceptable = True
            ElseIf MoveListRow.VariantLevel = pMoveListRow.VariantLevel + 1 _
            AndAlso MoveListRow.Expandable Then
                MoveListRow.Perceptable = True
            Else
                MoveListRow.Perceptable = False
            End If

        Next MoveListRow
    End Sub

    Private Sub MoveListRow_CollapseClicked(pMoveListRow As ctlMoveListRow)
        Dim MoveListRowFound As Boolean = False
        For Each MoveListRow As ctlMoveListRow In pnlMoveList.Controls
            If MoveListRowFound = False Then
                'Finding involved Variant
                If MoveListRow Is pMoveListRow Then
                    MoveListRowFound = True
                    MoveListRow.Expanded = False
                    MoveListRow.Perceptable = True
                    Continue For 'Skip the involved First Move for this already is Perceptable and set to Expanded
                End If
                Continue For
            End If

            'Update Variant and Subvariant First Moves Visibilty
            If MoveListRow.VariantLevel < pMoveListRow.VariantLevel Then _
                'Return because Higher Variant Level found
                Exit For
            ElseIf MoveListRow.VariantLevel = pMoveListRow.VariantLevel _
            AndAlso MoveListRow.VariantNumber <> pMoveListRow.VariantNumber Then
                'Return because Next Variant of same level found 
                Exit For
            Else
                MoveListRow.Perceptable = False
            End If
        Next MoveListRow

        Me.ShowMoveList()
    End Sub

    Public Sub ShowTop()
        pnlMoveList.VerticalScroll.Value = 0
    End Sub

    Public Sub Clear()
        While pnlMoveList.Controls.Count > 0
            Me.Remove(pnlMoveList.Controls(0))
        End While
    End Sub

    Public Sub Remove(pMoveListRow As ctlMoveListRow)
        'Remove Handler
        RemoveHandler pMoveListRow.RightClicked, AddressOf MoveListRow_RightClicked
        RemoveHandler pMoveListRow.Clicked, AddressOf MoveListRow_Clicked
        RemoveHandler pMoveListRow.DoubleClicked, AddressOf MoveListRow_DoubleClicked
        RemoveHandler pMoveListRow.ExpandClicked, AddressOf MoveListRow_ExpandClicked
        RemoveHandler pMoveListRow.CollapseClicked, AddressOf MoveListRow_CollapseClicked
        pnlMoveList.Controls.Remove(pMoveListRow)
        pMoveListRow.Dispose()
    End Sub

    Public Sub UpdateMoveList(pPGNHalfMoves As PGNHalfMoves)
        Dim LastMoveListRow As ctlMoveListRow = Nothing
        Me.Visible = False 'More quiet and faster
        Me.Clear() '(SelectedHalfMove stays intact)

        For Each HalfMove As PGNHalfMove In pPGNHalfMoves

            If HalfMove.Color = BLACK _
            AndAlso LastMoveListRow IsNot Nothing _
            AndAlso LastMoveListRow.BlackMoveFits(HalfMove) Then
                LastMoveListRow.BlackHalfMove = HalfMove
            Else
                LastMoveListRow = Me.AddNew(HalfMove)
            End If
        Next HalfMove

        Me.ShowMoveList()

        'SelectedMove could be within an unexpanded Subvariant, so expand to see it
        ExpandToSeeSelectedMove(Me.SelectedHalfMove)

        Me.Visible = True
    End Sub

    ''' <summary>Adds a New MoveListRow to the MoveList</summary>
    Public Function AddNew(pHalfMove As PGNHalfMove) As ctlMoveListRow
        Dim MoveListRow As New ctlMoveListRow(pHalfMove)
        'Add Handler
        AddHandler MoveListRow.RightClicked, AddressOf MoveListRow_RightClicked
        AddHandler MoveListRow.Clicked, AddressOf MoveListRow_Clicked
        AddHandler MoveListRow.DoubleClicked, AddressOf MoveListRow_DoubleClicked
        AddHandler MoveListRow.ExpandClicked, AddressOf MoveListRow_ExpandClicked
        AddHandler MoveListRow.CollapseClicked, AddressOf MoveListRow_CollapseClicked

        pnlMoveList.Controls.Add(MoveListRow)

        Return MoveListRow
    End Function

    ''' <summary>returns the found PGNHalfMove or Nothing when not found </summary>
    Private Function FindMoveListRow(pPGNHalfMove As PGNHalfMove) As ctlMoveListRow
        For Each MoveListRow As ctlMoveListRow In pnlMoveList.Controls
            If MoveListRow.WhiteHalfMove Is pPGNHalfMove _
            OrElse MoveListRow.BlackHalfMove Is pPGNHalfMove Then
                Return MoveListRow
            End If
        Next MoveListRow
        Return Nothing
    End Function

    Private Sub EnsureMoveListRowVisible(pMoveListRow As ctlMoveListRow)
        pnlMoveList.ScrollControlIntoView(pMoveListRow)
    End Sub

    Private Sub ExpandToSeeSelectedMove(pPGNHalfMove As PGNHalfMove)
        Dim Index As Integer, MoveListRow As ctlMoveListRow
        If pPGNHalfMove Is Nothing Then Exit Sub

        If pPGNHalfMove.VariantLevel = 0 Then
            'Halfmove not part of Subvariant
            Exit Sub
        End If
        For Index = 0 To pnlMoveList.Controls.Count - 1
            MoveListRow = pnlMoveList.Controls.Item(Index)
            If MoveListRow.WhiteHalfMove Is pPGNHalfMove _
            OrElse MoveListRow.BlackHalfMove Is pPGNHalfMove Then
                Exit For
            End If
        Next Index
        If Index > pnlMoveList.Controls.Count - 1 Then
            'Move Not found
            Throw New ArgumentException("HalfMove not found at MoveList: " & pPGNHalfMove.MoveText())
        End If

        'Now looking back for rows needing to be expanded
        For I = Index - 1 To 0 Step -1 'The row with the HalfMove does not need to be expanded to be visible
            MoveListRow = pnlMoveList.Controls.Item(I)
            If MoveListRow.Expandable = True Then
                If MoveListRow.Expanded = False Then
                    MoveListRow.Expanded = True
                    Call Me.Expand(MoveListRow)
                End If
            End If
            If MoveListRow.VariantLevel = 0 Then
                'Highest Level found, stop looking back
                Exit For
            End If
        Next I
    End Sub

    Public Sub ShowMoveList()
        Dim MoveListRowTop As Long = -pnlMoveList.VerticalScroll.Value
        For Each MoveListRow As ctlMoveListRow In pnlMoveList.Controls
            MoveListRow.Top = MoveListRowTop
            MoveListRow.Left = MoveListRow.VariantLevel * Me.Indent
            MoveListRow.Width = Me.Width - MoveListRow.Left - 18
            If HideAfterSelectedHalfMove = True Then
                MoveListRow.ReArrange(If(SelectedHalfMove Is Nothing, -1, SelectedHalfMove.Index))
            Else
                MoveListRow.ReArrange()
            End If
            Application.DoEvents()
            MoveListRowTop += MoveListRow.Height
        Next MoveListRow
    End Sub

    ''' <summary>Returns the Bottom position of the Last ViewRow</summary>
    Public Function LastViewRowBottom() As Integer
        If pnlMoveList.Controls.Count = 0 Then
            Return 0
        Else
            Return pnlMoveList.Controls(pnlMoveList.Controls.Count - 1).Top _
             + pnlMoveList.Controls(pnlMoveList.Controls.Count - 1).Height
        End If
    End Function

End Class
