Imports PGNLibrary
Imports ChessGlobals
Imports ChessGlobals.ChessMode
Imports ChessGlobals.ChessColor
Imports ChessGlobals.ChessLanguage

Public Class ctlMoveList

    Private gIndent As Integer = 25
    Private gHideAfterSelectedHalfMove As Boolean = False
    Private gSelectedHalfMove As PGNHalfMove 'UnColors previously selected move and Colors the new one
    '                                         Needed to capture previous postion at Click

    Public Shadows Event RightClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove)
    Public Shadows Event Clicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove)

    Public Property SelectedHalfMove As PGNHalfMove
        Set(pSelectedHalfMove As PGNHalfMove)
            Dim MoveListRow As ctlMoveListRow

            'Uncolor previously selected HalfMove
            If gSelectedHalfMove IsNot Nothing Then
                MoveListRow = FindMoveListRow(gSelectedHalfMove)
                MoveListRow.WhiteSelected = False
                MoveListRow.BlackSelected = False
            End If

            gSelectedHalfMove = pSelectedHalfMove

            If pSelectedHalfMove Is Nothing Then
                ShowTop()
            Else
                MoveListRow = FindMoveListRow(pSelectedHalfMove)
                'Color selected HalfMove
                EnsureMoveListRowVisible(MoveListRow)
                If pSelectedHalfMove.Color = WHITE Then
                    MoveListRow.WhiteSelected = True
                Else
                    MoveListRow.BlackSelected = True
                End If

                'Ensure next move is visible
                If MoveListRow.Expandable _
                AndAlso MoveListRow.Expanded = False Then
                    MoveListRow.Expanded = True
                    Call MoveListRow_ExpandClicked(MoveListRow)
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

    Private Sub MoveListRow_ExpandClicked(pMoveListRow As ctlMoveListRow)
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

        Me.ShowMoveList()
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
        Me.SelectedHalfMove = Nothing
        While pnlMoveList.Controls.Count > 0
            Me.Remove(pnlMoveList.Controls(0))
        End While
    End Sub

    Public Sub Remove(pMoveListRow As ctlMoveListRow)
        'Remove Handler
        RemoveHandler pMoveListRow.RightClicked, AddressOf MoveListRow_RightClicked
        RemoveHandler pMoveListRow.Clicked, AddressOf MoveListRow_Clicked
        RemoveHandler pMoveListRow.ExpandClicked, AddressOf MoveListRow_ExpandClicked
        RemoveHandler pMoveListRow.CollapseClicked, AddressOf MoveListRow_CollapseClicked
        pnlMoveList.Controls.Remove(pMoveListRow)
        pMoveListRow.Dispose()
    End Sub

    Public Sub UpdateMoveList(pPGNHalfMoves As PGNHalfMoves)
        Dim LastMoveListRow As ctlMoveListRow = Nothing
        Me.Visible = False 'More quiet and faster
        Me.Clear()

        For Each HalfMove As PGNHalfMove In pPGNHalfMoves
            'Debug.Print(Str(HalfMove.MoveNr) & If(HalfMove.Color = BLACK, ". ... ", ". ") & HalfMove.MoveText(ENGLISH) & " " & HalfMove.VariantLevel & "," & HalfMove.VariantNumber)

            If HalfMove.Color = BLACK _
            AndAlso LastMoveListRow IsNot Nothing _
            AndAlso LastMoveListRow.BlackMoveFits(HalfMove) Then
                LastMoveListRow.BlackHalfMove = HalfMove
            Else
                LastMoveListRow = Me.AddNew(HalfMove)
            End If
        Next HalfMove

        Me.ShowMoveList()
        Me.Visible = True
    End Sub

    Public Function AddNew(pHalfMove As PGNHalfMove) As ctlMoveListRow
        Dim MoveListRow As New ctlMoveListRow(pHalfMove)
        'Add Handler
        AddHandler MoveListRow.RightClicked, AddressOf MoveListRow_RightClicked
        AddHandler MoveListRow.Clicked, AddressOf MoveListRow_Clicked
        AddHandler MoveListRow.ExpandClicked, AddressOf MoveListRow_ExpandClicked
        AddHandler MoveListRow.CollapseClicked, AddressOf MoveListRow_CollapseClicked

        pnlMoveList.Controls.Add(MoveListRow)

        Return MoveListRow
    End Function

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

    Public Function LastViewRowBottom() As Integer
        If pnlMoveList.Controls.Count = 0 Then
            Return 0
        Else
            Return pnlMoveList.Controls(pnlMoveList.Controls.Count - 1).Top _
             + pnlMoveList.Controls(pnlMoveList.Controls.Count - 1).Height
        End If
    End Function

    Private Sub ctlMoveList_SizeChanged(pSender As Object, pArgs As EventArgs) Handles Me.SizeChanged
        ShowMoveList()
    End Sub
End Class
