Option Explicit On

Imports System.ComponentModel
Imports System.Threading
Imports ChessGlobals
Imports ChessMaterials
Imports ChessMessaging
Imports ChessMessaging.Messages
Imports DemoBoard.frmMainForm
Imports DemoBoard.frmMainForm.ChessMode
Imports PGNLibrary

Public Class frmMoveList
    Private WithEvents gfrmMainForm As frmMainForm
    Private WithEvents gfrmEditHalfMove As New frmEditHalfMove()

    Private gHalfMoves As PGNHalfMoves
    Private gBeforeImage As String

    Public Event PositionChanged(pBeforeHalfMove As PGNHalfMove, pAfterHalfMove As PGNHalfMove)
    Public Event HalfMoveChanged(pHalfMove As PGNHalfMove, pBeforeImage As String, pAfterImage As String)
    Public Event MoveListChanged(pBeforeImage As String, pAfterImage As String)
    Public Event TrainingQuestionFound(pHalfMove As PGNHalfMove, pNextMoves As List(Of PGNHalfMove))

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
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
        Try
            If pMoveListRow IsNot Nothing Then
                If gfrmMainForm.Mode = TRAINING _
                AndAlso pMoveListRow.WhiteHalfMove.HasTrainingQuestion Then
                    RaiseEvent TrainingQuestionFound(pMoveListRow.WhiteHalfMove, pMoveListRow.WhiteHalfMove.SubVariants)
                    Exit Sub
                End If
            End If

            RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ctlMoveList_DoubleClicked(pMoveListRow As ctlMoveListRow, pHalfMove As PGNHalfMove, pPreviousHalfMove As PGNHalfMove) Handles ctlMoveList.DoubleClicked
        Try
            RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)
            Application.DoEvents()

            gBeforeImage = pHalfMove.JournalImage
            'NB Show() is used because closing a ShowDialog() within a ShowDialog() closes both forms
            gfrmEditHalfMove.Show(pHalfMove, gfrmMainForm)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNLibrary.PGNGame) Handles gfrmMainForm.GameChanged
        gHalfMoves = pPGNGame.HalfMoves

        If Me.Visible = True Then
            ctlMoveList.UpdateMoveList(gHalfMoves)
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove
        End If
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ApplyLanguageToCurrentForm(Me)

        'ContextMenuStrip is not in Form.Controls, but in private Form.components property
        Dim Resources As ComponentResourceManager
        Resources = New ComponentResourceManager(Me.GetType())
        For Each MenuItem As Object In Me.mnuMoveMenu.Items
            Resources.ApplyResources(MenuItem, MenuItem.Name, Thread.CurrentThread.CurrentUICulture)
        Next MenuItem

        If Me.Visible = True Then
            ctlMoveList.UpdateMoveList(gHalfMoves)
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove
        End If

        'Debug.Print("frmMoveList: Event PositionChanged")
        'RaiseEvent PositionChanged(Nothing, ctlMoveList.SelectedHalfMove)
    End Sub

    Private Sub gfrmMainForm_KeyDown(pSender As Object, pArgs As KeyEventArgs) Handles gfrmMainForm.KeyDown
        If Me.Visible = True Then
            Select Case pArgs.KeyCode
                Case Keys.Left : cmdPrevious_Click(Nothing, Nothing)
                Case Keys.Right : cmdNext_Click(Nothing, Nothing)
                Case Keys.PageUp, Keys.Home : cmdStart_Click(Nothing, Nothing)
                Case Keys.PageDown, Keys.End : cmdLast_Click(Nothing, Nothing)
                Case Else : Exit Sub
            End Select

            'To avoid TabControl to jump to next tab
            pArgs.Handled = True
        End If
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
                AndAlso gfrmMainForm.Mode = TRAINING _
                AndAlso PreviousHalfMove.HasTrainingQuestion Then
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
            NextMoves = gHalfMoves.NextHalfMoves(ctlMoveList.SelectedHalfMove)
            If NextMoves Is Nothing Then Exit Sub

            'Question or Multiple Choice ?
            If gfrmMainForm.Mode = TRAINING _
            AndAlso NextMoves(0).HasTrainingQuestion Then
                RaiseEvent TrainingQuestionFound(NextMoves(0), NextMoves)
                Exit Sub
            End If

            If NextMoves.Count = 1 Then
                ctlMoveList.SelectedHalfMove = NextMoves(0)

                RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
            Else
                Using frmSelectVariant = New frmSelectVariant()
                    frmSelectVariant.ShowDialog(NextMoves)
                    frmSelectVariant.Hide()
                    If frmSelectVariant.ChoosenVariant IsNot Nothing Then
                        ctlMoveList.SelectedHalfMove = frmSelectVariant.ChoosenVariant

                        RaiseEvent PositionChanged(PreviousHalfMove, ctlMoveList.SelectedHalfMove)
                    End If
                End Using
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdLast_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdLast.Click
        Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove
        Try
            Dim LastHalfMove As PGNHalfMove = gHalfMoves.LastHalfMove
            If LastHalfMove IsNot Nothing _
            AndAlso gfrmMainForm.Mode = TRAINING _
            AndAlso LastHalfMove.HasTrainingQuestion Then
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

            'Debug.Print("frmMoveList: Event PositionChanged")
            'RaiseEvent PositionChanged(pPreviousHalfMove, pHalfMove)
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDeleteMove_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuDeleteMove.Click
        Try
            Dim PreviousHalfMove As PGNHalfMove = ctlMoveList.SelectedHalfMove.PreviousHalfMove
            If MsgBox(MessageText("DeleteMove", ctlMoveList.SelectedHalfMove.MoveNrString(True) & ctlMoveList.SelectedHalfMove.MoveText()), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim BeforeImage As New XElement("Change")
                BeforeImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
                BeforeImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))

                gHalfMoves.DeleteVariantFrom(ctlMoveList.SelectedHalfMove)
                ctlMoveList.UpdateMoveList(gHalfMoves)
                ctlMoveList.SelectedHalfMove = PreviousHalfMove

                Dim AfterImage As New XElement("Change")
                AfterImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
                AfterImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))
                RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuEditMove_Click(pSender As System.Object, pArgs As System.EventArgs) Handles mnuEditMove.Click
        Try
            gBeforeImage = ctlMoveList.SelectedHalfMove.JournalImage
            'NB Show() is used because closing a ShowDialog() within a ShowDialog() closes both forms
            gfrmEditHalfMove.Show(ctlMoveList.SelectedHalfMove, gfrmMainForm)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmEditHalfMove_HalfMoveChanged(pHalfMove As PGNHalfMove) Handles gfrmEditHalfMove.HalfMoveChanged
        Dim AfterImage As String = pHalfMove.JournalImage
        If AfterImage <> gBeforeImage Then
            RaiseEvent HalfMoveChanged(pHalfMove, gBeforeImage, AfterImage)

            ctlMoveList.UpdateMoveList(gHalfMoves)
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove
        End If
    End Sub

    Private Sub mnuPromoteVariant_Click(pSender As Object, pArgs As EventArgs) Handles mnuPromoteVariant.Click
        Try
            Dim BeforeImage As New XElement("Change")
            BeforeImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
            BeforeImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))

            Dim PGNVariants As New PGNVariants(gHalfMoves.CurrentHalfMoveIndex, gHalfMoves)
            PGNVariants.Promote()

            ctlMoveList.UpdateMoveList(gHalfMoves)
            gHalfMoves.CurrentHalfMoveIndex = PGNVariants.CurrentMoveIndex

            Dim AfterImage As New XElement("Change")
            AfterImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
            AfterImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))
            RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub mnuDemoteVariant_Click(pSender As Object, pArgs As EventArgs) Handles mnuDemoteVariant.Click
        Try
            Dim BeforeImage As New XElement("Change")
            BeforeImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
            BeforeImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))

            Dim PGNVariants As New PGNVariants(gHalfMoves.CurrentHalfMoveIndex, gHalfMoves, True)
            PGNVariants.Demote()

            ctlMoveList.UpdateMoveList(gHalfMoves)
            gHalfMoves.CurrentHalfMoveIndex = PGNVariants.CurrentMoveIndex
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove

            Dim AfterImage As New XElement("Change")
            AfterImage.Add(New XElement("Index", gHalfMoves.CurrentHalfMoveIndex))
            AfterImage.Add(New XElement("HalfMoves", gHalfMoves.XPGNString))

            RaiseEvent MoveListChanged(BeforeImage.ToString(), AfterImage.ToString())
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub frmMoveList_VisibleChanged(pSender As Object, pArgs As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True _
        And gHalfMoves IsNot Nothing Then
            ctlMoveList.UpdateMoveList(gHalfMoves)
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove
        End If
    End Sub

    Private Sub frmMoveList__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Private Sub gfrmMainForm_FENChanged(pFEN As String) Handles gfrmMainForm.FENChanged
        If Me.Visible = True Then
            ctlMoveList.UpdateMoveList(gHalfMoves)
            ctlMoveList.SelectedHalfMove = gHalfMoves.CurrentHalfMove
        End If
    End Sub

    Private Sub gfrmMainForm_MoveListPositionChanged(pPGNGame As PGNGame, pCurrentHalfMove As PGNHalfMove) Handles gfrmMainForm.MoveListPositionChanged
        If Me.Visible = True Then
            ctlMoveList.SelectedHalfMove = pCurrentHalfMove
            ctlMoveList.ShowMoveList()
        End If
    End Sub

    Private Sub gfrmMainForm_ModeChanged(pMode As ChessMode) Handles gfrmMainForm.ModeChanged
        If pMode = TRAINING Then
            ctlMoveList.HideAfterSelectedHalfMove = True
        Else
            ctlMoveList.HideAfterSelectedHalfMove = False
        End If
    End Sub

    Private Sub gfrmMainForm_MovePlayed(pHalfMove As PGNHalfMove) Handles gfrmMainForm.MovePlayed
        If Me.Visible = True Then
            ctlMoveList.SelectedHalfMove = pHalfMove
            ctlMoveList.ShowMoveList()
        End If
    End Sub

    Private Sub gfrmMainForm_ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String, pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pHalfMove As PGNHalfMove) Handles gfrmMainForm.ChessPieceMoved
        If Me.Visible = True Then
            ctlMoveList.SelectedHalfMove = pHalfMove
            ctlMoveList.ShowMoveList()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        gfrmMainForm = Nothing
        gHalfMoves = Nothing

        MyBase.Finalize()
    End Sub

End Class