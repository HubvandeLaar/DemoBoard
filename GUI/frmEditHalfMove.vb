Option Explicit On

Imports ChessGlobals
Imports PGNLibrary
Imports ChessMaterials

Public Class frmEditHalfMove

    Public PGNHalfMove As PGNHalfMove

    Private gfrmMainForm As frmMainForm

    Public Overloads Sub ShowDialog(pPGNHalfMove As PGNHalfMove, pfrmMainForm As frmMainForm)
        Try
            gfrmMainForm = pfrmMainForm
            PGNHalfMove = pPGNHalfMove

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
                If PGNHalfMove.TrainingQuestion Is Nothing Then
                    lblTrainingQuestion.Text = ""
                Else
                    lblTrainingQuestion.Text = PGNHalfMove.TrainingQuestion.PGNString
                End If
            End If
            If pPGNHalfMove.CommentAfter Is Nothing Then
                txtCommentAfter.Text = ""
                lblMarkers.Text = ""
                lblArrows.Text = ""
                lblTexts.Text = ""
            Else
                txtCommentAfter.Text = pPGNHalfMove.CommentAfter.Text
                lblMarkers.Text = pPGNHalfMove.MarkerListString
                If pPGNHalfMove.CommentAfter Is Nothing _
                OrElse pPGNHalfMove.CommentAfter.ArrowList Is Nothing Then
                    lblArrows.Text = ""
                Else
                    lblArrows.Text = pPGNHalfMove.CommentAfter.ArrowList.XPGNString
                End If
                lblTexts.Text = pPGNHalfMove.TextListString
            End If

            MyBase.ShowDialog()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditNAGs_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditNAGs.Click
        Try
            Dim EditNAGs = New frmEditNAGs()
            EditNAGs.ShowDialog(lblNAGs.Text)
            lblNAGs.Text = EditNAGs.NAGList.PGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditTrainingQuestion_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditTrainingQuestion.Click
        Try
            If Me.PGNHalfMove IsNot Nothing Then
                Dim frmEditTrainingQuestion As New frmEditTrainingQuestion
                frmEditTrainingQuestion.ShowDialog(Me.PGNHalfMove.TrainingQuestion)

                If frmEditTrainingQuestion.TrainingQuestion Is Nothing Then
                    lblTrainingQuestion.Text = ""
                Else
                    lblTrainingQuestion.Text = frmEditTrainingQuestion.TrainingQuestion.PGNString
                End If
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditMarkerList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditMarkerList.Click
        Try
            Dim EditMarkers = New frmEditMarkers()
            EditMarkers.ShowDialog(lblMarkers.Text)
            lblMarkers.Text = EditMarkers.MarkerList.PGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditArrowList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditArrowList.Click
        Try
            Dim EditArrows = New frmEditArrows()
            EditArrows.ShowDialog(lblArrows.Text)
            lblArrows.Text = EditArrows.ArrowList.PGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdEditTextList_Click(pSender As Object, pArgs As EventArgs) Handles cmdEditTextList.Click
        Try
            Dim EditTexts = New frmEditTexts()
            EditTexts.ShowDialog(lblTexts.Text)
            lblTexts.Text = EditTexts.TextList.XPGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            PGNHalfMove.NAGs.PGNString = lblNAGs.Text

            If txtCommentBefore.Text = "" _
            And lblTrainingQuestion.Text = "" Then
                PGNHalfMove.CommentBefore = Nothing 'Zet eventuele Training vraag ook op Nothing !!!!
            Else
                If PGNHalfMove.CommentBefore Is Nothing Then
                    PGNHalfMove.CommentBefore = New PGNComment(txtCommentBefore.Text)
                Else
                    PGNHalfMove.CommentBefore.Text = txtCommentBefore.Text
                End If
                If lblTrainingQuestion.Text = "" Then
                    PGNHalfMove.TrainingQuestion = Nothing
                Else
                    PGNHalfMove.TrainingQuestion = New PGNTrainingQuestion(lblTrainingQuestion.Text)
                End If
            End If

            If txtCommentAfter.Text = "" _
            And lblMarkers.Text = "" _
            And lblArrows.Text = "" _
            And lblTexts.Text = "" Then
                PGNHalfMove.CommentAfter = Nothing
            Else
                If PGNHalfMove.CommentAfter Is Nothing Then
                    PGNHalfMove.CommentAfter = New PGNComment(txtCommentAfter.Text)
                Else
                    PGNHalfMove.CommentAfter.Text = txtCommentAfter.Text
                End If
                PGNHalfMove.MarkerListString = lblMarkers.Text
                PGNHalfMove.ArrowListString = lblArrows.Text
                PGNHalfMove.TextListString = lblTexts.Text
            End If

            Me.Hide()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Protected Overrides Sub Finalize()
        Me.PGNHalfMove = Nothing
        Me.gfrmMainForm = Nothing

        MyBase.Finalize()
    End Sub

End Class
