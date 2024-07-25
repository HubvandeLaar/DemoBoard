Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessMaterials
Imports ChessGlobals.ChessColor
Imports PGNLibrary
Imports System.Windows.Forms
Imports DemoBoard
Imports System.ComponentModel
Imports System.Threading

Public Class frmMoveList
    Private WithEvents gfrmMainForm As frmMainForm
    Private WithEvents gPGNHalfMoves As PGNHalfMoves

    Public Event PositionChanged(pBeforeHalfMove As PGNHalfMove, pAfterHalfMove As PGNHalfMove)
    Public Event HalfMoveChanged(pBeforeImage As String, pAfterImage As String)
    Public Event MoveListChanged(pBeforeImage As String, pAfterImage As String)
    ' Public Event LanguageChanged(pCurrentMoveListRow As ctlMoveListRow)
    Public Event TrainingQuestionFound(pHalfMove As PGNHalfMove, pNextMoves As List(Of PGNHalfMove))

    Public Property CurrentHalfMove As PGNHalfMove
        Set(pCurrentHalfMove As PGNHalfMove)
            ctlMoveList.SelectedHalfMove = pCurrentHalfMove
        End Set
        Get
            If ctlMoveList.SelectedHalfMove Is Nothing Then
                Return Nothing
            Else
                Return ctlMoveList.SelectedHalfMove
            End If
        End Get
    End Property

    Public Property CurrentHalfMoveIndex As String
        Set(pCurrentHalfMoveIndex As String)
            If gPGNHalfMoves.Count = 0 _
            Or pCurrentHalfMoveIndex = "" Then
                Me.ctlMoveList.SelectedHalfMove = Nothing
            Else
                Me.ctlMoveList.SelectedHalfMove = gPGNHalfMoves(Val(pCurrentHalfMoveIndex))
            End If
        End Set
        Get
            If Me.CurrentHalfMove Is Nothing Then
                Return ""
            Else
                Return Str(CurrentHalfMove.Index)
            End If
        End Get
    End Property

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
    End Sub

    Public Sub NextMove()
        Me.cmdNext_Click(Nothing, Nothing)
    End Sub

    Private Sub frmMoveList_SizeChanged(pSender As Object, pArgs As System.EventArgs) Handles Me.SizeChanged
        Dim Center As Long = Me.ClientSize.Width / 2
        Try
            cmdStart.Left = Center - 4 - cmdPrevious.Width - 8 - cmdStart.Width
            cmdPrevious.Left = Center - 4 - cmdPrevious.Width
            cmdNext.Left = Center + 4
            cmdLast.Left = Center + 4 + cmdNext.Width + 8
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ctlMoveList_Clicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove) Handles ctlMoveList.Clicked
        If pMoveListRow Is Nothing Then
            RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)
        Else
            If gfrmMainForm.Mode = ChessMode.TRAINING _
            AndAlso pMoveListRow.WhiteHalfMove.TrainingQuestion IsNot Nothing Then
                RaiseEvent TrainingQuestionFound(pMoveListRow.WhiteHalfMove, pMoveListRow.WhiteHalfMove.SubVariants)
                Exit Sub
            End If
        End If
        RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)
    End Sub

    Private Sub gPGNHalfMoves_Changed(pPGNHalfMove As PGNHalfMove) Handles gPGNHalfMoves.Changed
        ctlMoveList.UpdateMoveList(gPGNHalfMoves)
        ctlMoveList.SelectedHalfMove = pPGNHalfMove
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNLibrary.PGNGame) Handles gfrmMainForm.GameChanged
        gPGNHalfMoves = pPGNGame.HalfMoves
        Me.ctlMoveList.UpdateMoveList(gPGNHalfMoves)
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ChangeLanguageCurrentForm(Me)

        'ContextMenuStrip is not in Form.Controls, but in private Form.components property
        Dim Resources As ComponentResourceManager
        Resources = New ComponentResourceManager(Me.GetType())
        For Each MenuItem As Object In Me.mnuMoveMenu.Items
            Resources.ApplyResources(MenuItem, MenuItem.Name, Thread.CurrentThread.CurrentUICulture)
        Next MenuItem

        Me.ctlMoveList.UpdateMoveList(gPGNHalfMoves)
    End Sub

    Private Sub gfrmMainForm_BoardKeyDown(pMsg As Message, pKeyData As Keys) Handles gfrmMainForm.BoardKeyDown
        Select Case pKeyData
            Case Keys.Left : cmdPrevious_Click(Nothing, Nothing)
            Case Keys.Right : cmdNext_Click(Nothing, Nothing)
            Case Keys.PageUp : cmdStart_Click(Nothing, Nothing)
            Case Keys.PageDown : cmdLast_Click(Nothing, Nothing)
        End Select
    End Sub

    'Buttons
    Private Sub cmdStart_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdStart.Click
        Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove
        Try
            ctlMoveList.SelectedHalfMove = Nothing
            RaiseEvent PositionChanged(PreviousHalfMove, Nothing)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdPrevious_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdPrevious.Click
        Try
            If ctlMoveList.SelectedHalfMove Is Nothing Then
                Exit Sub
            Else
                Dim PreviousHalfMove As PGNHalfMove
                PreviousHalfMove = ctlMoveList.SelectedHalfMove.PreviousHalfMove

                'Question or Multiple Choice ?
                If PreviousHalfMove IsNot Nothing _
                AndAlso gfrmMainForm.Mode = ChessMode.TRAINING _
                AndAlso PreviousHalfMove.TrainingQuestion IsNot Nothing Then
                    RaiseEvent TrainingQuestionFound(PreviousHalfMove, PreviousHalfMove.SubVariants)
                    Exit Sub
                End If

                ctlMoveList.SelectedHalfMove = PreviousHalfMove
                RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdNext_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdNext.Click
        Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove
        Dim NextMoves As List(Of PGNHalfMove)
        Try
            NextMoves = gPGNHalfMoves.NextHalfMoves(ctlMoveList.SelectedHalfMove)
            If NextMoves Is Nothing Then Exit Sub

            'Question or Multiple Choice ?
            If gfrmMainForm.Mode = ChessMode.TRAINING _
            AndAlso NextMoves(0).TrainingQuestion IsNot Nothing Then
                RaiseEvent TrainingQuestionFound(NextMoves(0), NextMoves)
                Exit Sub
            End If

            If NextMoves.Count = 1 Then
                ctlMoveList.SelectedHalfMove = NextMoves(0)
                RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
            Else
                With frmSelectVariant
                    .ShowDialog(NextMoves)
                    .Hide()
                    If .ChoosenVariant IsNot Nothing Then
                        ctlMoveList.SelectedHalfMove = .ChoosenVariant
                        RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
                    End If
                End With
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdLast_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdLast.Click
        Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove
        Try
            Dim LastHalfMove As PGNHalfMove = gPGNHalfMoves.LastHalfMove
            If LastHalfMove IsNot Nothing _
            AndAlso gfrmMainForm.Mode = ChessMode.TRAINING _
            AndAlso LastHalfMove.TrainingQuestion IsNot Nothing Then
                RaiseEvent TrainingQuestionFound(LastHalfMove, LastHalfMove.SubVariants)
                Exit Sub
            End If

            ctlMoveList.SelectedHalfMove = LastHalfMove
            RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    'Menu Choises
    Private Sub ctlMoveList_RightClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove) Handles ctlMoveList.RightClicked
        Try
            mnuMoveMenu.Show(MousePosition)
            RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDeleteMove_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuDeleteMove.Click
        Try
            Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove.PreviousHalfMove
            If MsgBox(MessageText("DeleteMove", ctlMoveList.SelectedHalfMove.MoveNrString(True) & ctlMoveList.SelectedHalfMove.MoveText()), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim BeforeImage As New XElement("Change")
                BeforeImage.Add(New XElement("Index", CurrentHalfMoveIndex))
                BeforeImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))

                gPGNHalfMoves.DeleteVariantFrom(ctlMoveList.SelectedHalfMove)
                ctlMoveList.SelectedHalfMove = PreviousHalfMove

                Dim AfterImage As New XElement("Change")
                AfterImage.Add(New XElement("Index", CurrentHalfMoveIndex))
                AfterImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))
                RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuEditMove_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuEditMove.Click
        Try
            Dim HalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove
            Dim BeforeImage As String = ctlMoveList.SelectedHalfMove.JournalImage
            Dim frmEditHalfMove As New frmEditHalfMove
            frmEditHalfMove.ShowDialog(ctlMoveList.SelectedHalfMove, gfrmMainForm)
            Dim AfterImage As String = ctlMoveList.SelectedHalfMove.JournalImage
            If AfterImage <> BeforeImage Then
                RaiseEvent HalfMoveChanged(BeforeImage, AfterImage)
                ctlMoveList.UpdateMoveList(gfrmMainForm.PGNGame.HalfMoves)
                ctlMoveList.SelectedHalfMove = HalfMove
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuPromoteVariant_Click(pSender As Object, pArgs As EventArgs) Handles mnuPromoteVariant.Click
        Try
            Dim BeforeImage As New XElement("Change")
            BeforeImage.Add(New XElement("Index", CurrentHalfMoveIndex))
            BeforeImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))

            Dim PGNVariants As New PGNVariants(CurrentHalfMoveIndex, gPGNHalfMoves)
            PGNVariants.Promote()

            Me.ctlMoveList.UpdateMoveList(gPGNHalfMoves)
            CurrentHalfMoveIndex = PGNVariants.CurrentMoveIndex

            Dim AfterImage As New XElement("Change")
            AfterImage.Add(New XElement("Index", CurrentHalfMoveIndex))
            AfterImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))
            RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDemoteVariant_Click(pSender As Object, pArgs As EventArgs) Handles mnuDemoteVariant.Click
        Try
            Dim BeforeImage As New XElement("Change")
            BeforeImage.Add(New XElement("Index", CurrentHalfMoveIndex))
            BeforeImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))

            Dim PGNVariants As New PGNVariants(CurrentHalfMoveIndex, gPGNHalfMoves, True)
            PGNVariants.Demote()

            Me.ctlMoveList.UpdateMoveList(gPGNHalfMoves)
            CurrentHalfMoveIndex = PGNVariants.CurrentMoveIndex

            Dim AfterImage As New XElement("Change")
            AfterImage.Add(New XElement("Index", CurrentHalfMoveIndex))
            AfterImage.Add(New XElement("HalfMoves", gPGNHalfMoves.XPGNString))
            RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub frmMoveList__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Private Sub gfrmMainForm_MoveListPositionChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove) Handles gfrmMainForm.MoveListPositionChanged
        ctlMoveList.SelectedHalfMove = pCurrentHalfMove
        ctlMoveList.ShowMoveList()
    End Sub

    Private Sub gfrmMainForm_ModeChanged(pMode As ChessMode) Handles gfrmMainForm.ModeChanged
        If pMode = ChessMode.TRAINING Then
            ctlMoveList.HideAfterSelectedHalfMove = True
        Else
            ctlMoveList.HideAfterSelectedHalfMove = False
        End If
    End Sub

    Private Sub gfrmMainForm_MovePlayed(pHalfMove As PGNHalfMove) Handles gfrmMainForm.MovePlayed
        ctlMoveList.SelectedHalfMove = pHalfMove
        ctlMoveList.ShowMoveList()
    End Sub

    Protected Overrides Sub Finalize()
        Me.gfrmMainForm = Nothing
        Me.gPGNHalfMoves = Nothing

        MyBase.Finalize()
    End Sub
End Class