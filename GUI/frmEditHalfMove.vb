Option Explicit On

Imports System.ComponentModel
Imports ChessGlobals
Imports ChessMessaging
Imports PGNLibrary

Public Class frmEditHalfMove

    Public Property HalfMove As PGNHalfMove

    Private gfrmMainForm As frmMainForm

    Public Event HalfMoveChanged(pPGNHalfMove As PGNHalfMove)

    Public Overloads Sub Show(pPGNHalfMove As PGNHalfMove, pfrmMainForm As frmMainForm)
        Try
            gfrmMainForm = pfrmMainForm
            HalfMove = pPGNHalfMove

            lblMoveNr.Text = pPGNHalfMove.MoveNrString
            lblMoveText.Text = pPGNHalfMove.MoveText(CurrentLanguage)
            lblColor.Text = pPGNHalfMove.Color.Text
            lblVariantLevel.Text = Str(pPGNHalfMove.VariantLevel)
            lblVariantNumber.Text = Str(pPGNHalfMove.VariantNumber)
            lblIndex.Text = Str(pPGNHalfMove.Index)

            lblNAGs.Text = pPGNHalfMove.NAGs.PGNString
            If pPGNHalfMove.CommentBefore Is Nothing Then
                txtCommentBefore.Text = ""
                lblTrainingQuestion.Text = ""
            Else
                txtCommentBefore.Text = pPGNHalfMove.CommentBefore.Text
                If HalfMove.HasTrainingQuestion Then
                    lblTrainingQuestion.Text = ""
                Else
                    lblTrainingQuestion.Text = HalfMove.TrainingQuestion.PGNString
                End If
            End If
            If pPGNHalfMove.CommentAfter Is Nothing Then
                txtCommentAfter.Text = ""
                lblMarkers.Text = ""
                lblArrows.Text = ""
                lblTexts.Text = ""
            Else
                txtCommentAfter.Text = pPGNHalfMove.CommentAfter.Text
                lblMarkers.Text = pPGNHalfMove.CommentAfter.MarkerList.XPGNString
                If pPGNHalfMove.CommentAfter Is Nothing _
                OrElse pPGNHalfMove.CommentAfter.ArrowList Is Nothing Then
                    lblArrows.Text = ""
                Else
                    lblArrows.Text = pPGNHalfMove.CommentAfter.ArrowList.XPGNString
                End If
                lblTexts.Text = pPGNHalfMove.CommentAfter.TextList.XPGNString
            End If

            MyBase.Show(gfrmMainForm)
            Me.Left = gfrmMainForm.Left + (gfrmMainForm.Width - Me.Width) / 2
            Me.Top = gfrmMainForm.Top + (gfrmMainForm.Height - Me.Height) / 2

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditNAGs_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditNAGs.Click
        Try
            Using frmEditNAGs = New frmEditNAGs()
                frmEditNAGs.ShowDialog(lblNAGs.Text)
                lblNAGs.Text = frmEditNAGs.NAGList.PGNString
            End Using

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditTrainingQuestion_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditTrainingQuestion.Click
        Try
            If Me.HalfMove IsNot Nothing Then
                Using frmEditTrainingQuestion = New frmEditTrainingQuestion
                    frmEditTrainingQuestion.ShowDialog(Me.HalfMove.TrainingQuestion)

                    If frmEditTrainingQuestion.TrainingQuestion Is Nothing Then
                        lblTrainingQuestion.Text = ""
                    Else
                        lblTrainingQuestion.Text = frmEditTrainingQuestion.TrainingQuestion.PGNString
                    End If
                End Using
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditMarkerList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditMarkerList.Click
        Try
            Using frmEditMarkers = New frmEditMarkers()
                frmEditMarkers.ShowDialog(lblMarkers.Text)
                lblMarkers.Text = frmEditMarkers.MarkerList.XPGNString
            End Using
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditArrowList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditArrowList.Click
        Try
            Using frmEditArrows = New frmEditArrows()
                frmEditArrows.ShowDialog(lblArrows.Text)
                lblArrows.Text = frmEditArrows.ArrowList.XPGNString
            End Using
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditTextList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditTextList.Click
        Try
            Using frmEditTexts = New frmEditTexts()
                frmEditTexts.ShowDialog(lblTexts.Text)
                lblTexts.Text = frmEditTexts.TextList.XPGNString
            End Using
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            HalfMove.NAGs.PGNString = lblNAGs.Text

            If txtCommentBefore.Text = "" _
            And lblTrainingQuestion.Text = "" Then
                HalfMove.CommentBefore = Nothing 'Zet eventuele Training vraag ook op Nothing !!!!
            Else
                If HalfMove.CommentBefore Is Nothing Then
                    HalfMove.CommentBefore = New PGNComment(txtCommentBefore.Text)
                Else
                    HalfMove.CommentBefore.Text = txtCommentBefore.Text
                End If
                If lblTrainingQuestion.Text = "" Then
                    HalfMove.TrainingQuestion = Nothing
                Else
                    HalfMove.TrainingQuestion = New PGNTrainingQuestion(lblTrainingQuestion.Text)
                End If
            End If

            If txtCommentAfter.Text = "" _
            And lblMarkers.Text = "" _
            And lblArrows.Text = "" _
            And lblTexts.Text = "" Then
                HalfMove.CommentAfter = Nothing
            Else
                If HalfMove.CommentAfter Is Nothing Then
                    HalfMove.CommentAfter = New PGNComment(txtCommentAfter.Text)
                Else
                    HalfMove.CommentAfter.Text = txtCommentAfter.Text
                End If
                HalfMove.MarkerListString = lblMarkers.Text
                HalfMove.ArrowListString = lblArrows.Text
                HalfMove.TextListString = lblTexts.Text
            End If

            Me.Hide()
            RaiseEvent HalfMoveChanged(HalfMove)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Protected Overrides Sub Finalize()
        Me.HalfMove = Nothing
        gfrmMainForm = Nothing

        MyBase.Finalize()
    End Sub

    Private Sub frmEditHalfMove_Closing(pSender As Object, pArgs As CancelEventArgs) Handles Me.Closing
        Me.Hide()
        pArgs.Cancel = True
    End Sub
End Class
