Option Explicit On

Imports ChessGlobals
Imports PGNLibrary

Public Class frmEditTrainingQuestion

    Public TrainingQuestion As PGNTrainingQuestion

    Private RowIndex As Integer

    'Defines what languages are used / defined / updated
    Private EnAnswers As Boolean = False
    Private NlAnswers As Boolean = False
    Private NoneAnswers As Boolean = False

    Public Overloads Sub ShowDialog(pTrainingQuestion As PGNTrainingQuestion)
        Try
            Me.TrainingQuestion = pTrainingQuestion

            If Me.TrainingQuestion Is Nothing Then
                Me.TrainingQuestion = Nothing
                NoneAnswers = True
                EnAnswers = False : NlAnswers = False
                rtbQuestionEn.Text = "" : rtbQuestionNl.Text = ""
                rtbHint1En.Text = "" : rtbHint1Nl.Text = ""
                rtbHint2En.Text = "" : rtbHint2Nl.Text = ""
                grdAnswersEn.Rows.Clear() : grdAnswersNl.Rows.Clear()
            Else
                For Each LocalizedQuestion As PGNTrainingLocalizedQuestion In Me.TrainingQuestion.LocalizedQuestions
                    Select Case LocalizedQuestion.Language
                        Case "En" : EnAnswers = True
                        Case "Nl" : NlAnswers = True
                        Case "" : NoneAnswers = True
                    End Select
                    Select Case LocalizedQuestion.Language
                        Case "En", ""
                            tabEn.Select()
                            rtbQuestionEn.Text = LocalizedQuestion.Question
                            rtbHint1En.Text = LocalizedQuestion.Hint1
                            rtbHint2En.Text = LocalizedQuestion.Hint2
                            grdAnswersEn.Rows.Clear()
                            For Each Answer As PGNTrainingAnswer In LocalizedQuestion.Answers
                                Dim Cells(2) As String
                                Cells(0) = Answer.Move.MoveText(CurrentLanguage)
                                Cells(1) = String.Format(Answer.Points)
                                Cells(2) = Answer.FeedBack
                                grdAnswersEn.Rows.Add(Cells)
                            Next Answer
                       ' txtTimerEn.Text = LocalizedQuestion.
                        Case "Nl"
                            tabNl.Select()
                            rtbQuestionNl.Text = LocalizedQuestion.Question
                            rtbHint1Nl.Text = LocalizedQuestion.Hint1
                            rtbHint2Nl.Text = LocalizedQuestion.Hint2
                            grdAnswersNl.Rows.Clear()
                            For Each Answer As PGNTrainingAnswer In LocalizedQuestion.Answers
                                Dim Cells(2) As String
                                Cells(0) = Answer.Move.MoveText(CurrentLanguage)
                                Cells(1) = String.Format(Answer.Points)
                                Cells(2) = Answer.FeedBack
                                grdAnswersNl.Rows.Add(Cells)
                            Next Answer
                            ' txtTimerEn.Text = LocalizedQuestion.
                    End Select
                Next LocalizedQuestion
            End If

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub tabTexts_TabIndexChanged(pSender As Object, pArgs As EventArgs) Handles tabTexts.TabIndexChanged
        Select Case tabTexts.SelectedTab.Name
            Case "tabEn"
                If Me.EnAnswers = False _
                And Me.NoneAnswers = False Then
                    If Me.NlAnswers = True Then
                        Call CopyNlToEn()
                        Me.EnAnswers = True
                        Me.NoneAnswers = False
                    Else
                        Me.EnAnswers = False
                        Me.NoneAnswers = True
                    End If
                End If
            Case "tabNl"
                If Me.NlAnswers = False Then
                    If Me.NoneAnswers = True _
                    Or Me.EnAnswers = True Then
                        Call CopyEnToNl()
                    End If
                    Me.NoneAnswers = False
                    Me.EnAnswers = True
                    Me.NlAnswers = True
                End If
        End Select
    End Sub

    Private Sub CopyEnToNl()
        rtbQuestionNl.Text = rtbQuestionEn.Text
        rtbHint1Nl.Text = rtbHint1En.Text
        rtbHint2Nl.Text = rtbHint2En.Text
        grdAnswersEn.Rows.Clear()
        For Each Row As DataGridViewRow In grdAnswersEn.Rows
            Dim Cells As New Object()
            For Each Cell As DataGridViewCell In Row.Cells
                Cells.Add(Cell.Value)
            Next Cell
            grdAnswersNl.Rows.Add(Cells)
        Next Row
    End Sub

    Private Sub CopyNlToEn()
        rtbQuestionEn.Text = rtbQuestionNl.Text
        rtbHint1En.Text = rtbHint1Nl.Text
        rtbHint2En.Text = rtbHint2Nl.Text
        grdAnswersEn.Rows.Clear()
        For Each Row As DataGridViewRow In grdAnswersNl.Rows
            Dim Cells As New Object()
            For Each Cell As DataGridViewCell In Row.Cells
                Cells.Add(Cell.Value)
            Next Cell
            grdAnswersEn.Rows.Add(Cells)
        Next Row
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.TrainingQuestion = New PGNTrainingQuestion("")

        Dim LocalizedQuestion As PGNTrainingLocalizedQuestion

        If EnAnswers = True Then
            LocalizedQuestion = GetLocalizedQuestion("En")
            If LocalizedQuestion Is Nothing Then
                LocalizedQuestion = New PGNTrainingLocalizedQuestion("", "En")
            End If
            LocalizedQuestion.Question = rtbQuestionEn.Text
            LocalizedQuestion.Hint1 = rtbHint1En.Text
            LocalizedQuestion.Hint2 = rtbHint2En.Text
            LocalizedQuestion.Answers.Clear()
            Dim Index As Integer = 0
            For Each Row As DataGridViewRow In grdAnswersEn.Rows
                If Row.IsNewRow = True Then Continue For
                Dim Answer As New PGNTrainingAnswer(CStr(Row.Cells(0).Value), CStr(Row.Cells(2).Value), CStr(Row.Cells(1).Value), Index)
                Index = Index + 1
                LocalizedQuestion.Answers.Add(Answer)
            Next Row

            If LocalizedQuestion.Question <> "" _
            Or LocalizedQuestion.Hint1 <> "" _
            Or LocalizedQuestion.Hint2 <> "" _
            Or LocalizedQuestion.Answers.Count > 0 Then
                Me.TrainingQuestion.LocalizedQuestions.Add(LocalizedQuestion)
            End If
        End If

        If NlAnswers = True Then
            LocalizedQuestion = GetLocalizedQuestion("Nl")
            If LocalizedQuestion Is Nothing Then
                LocalizedQuestion = New PGNTrainingLocalizedQuestion("", "Nl")
            End If
            LocalizedQuestion.Question = rtbQuestionNl.Text
            LocalizedQuestion.Hint1 = rtbHint1Nl.Text
            LocalizedQuestion.Hint2 = rtbHint2Nl.Text
            LocalizedQuestion.Answers.Clear()
            Dim Index As Integer = 0
            For Each Row As DataGridViewRow In grdAnswersNl.Rows
                If Row.IsNewRow = True Then Continue For
                Dim Answer As New PGNTrainingAnswer(CStr(Row.Cells(0).Value), CStr(Row.Cells(2).Value), CStr(Row.Cells(1).Value), Index)
                Index = Index + 1
                LocalizedQuestion.Answers.Add(Answer)
            Next Row

            If LocalizedQuestion.Question <> "" _
            Or LocalizedQuestion.Hint1 <> "" _
            Or LocalizedQuestion.Hint2 <> "" _
            Or LocalizedQuestion.Answers.Count > 0 Then
                Me.TrainingQuestion.LocalizedQuestions.Add(LocalizedQuestion)
            End If
        End If

        If NoneAnswers = True Then
            LocalizedQuestion = GetLocalizedQuestion("")
            If LocalizedQuestion Is Nothing Then
                LocalizedQuestion = New PGNTrainingLocalizedQuestion("")
            End If
            LocalizedQuestion.Question = rtbQuestionEn.Text
            LocalizedQuestion.Hint1 = rtbHint1En.Text
            LocalizedQuestion.Hint2 = rtbHint2En.Text
            LocalizedQuestion.Answers.Clear()
            Dim Index As Integer = 0
            For Each Row As DataGridViewRow In grdAnswersEn.Rows
                If Row.Cells(0).Value IsNot Nothing _
                And Row.Cells(1).Value IsNot Nothing _
                And Row.Cells(2).Value IsNot Nothing Then
                    Dim Answer As New PGNTrainingAnswer(CStr(Row.Cells(0).Value), CStr(Row.Cells(2).Value), CStr(Row.Cells(1).Value), Index)
                    Index = Index + 1
                    LocalizedQuestion.Answers.Add(Answer)
                End If
            Next Row

            If LocalizedQuestion.Question <> "" _
            Or LocalizedQuestion.Hint1 <> "" _
            Or LocalizedQuestion.Hint2 <> "" _
            Or LocalizedQuestion.Answers.Count > 0 Then
                Me.TrainingQuestion.LocalizedQuestions.Add(LocalizedQuestion)
            End If
        End If

        If TrainingQuestion.LocalizedQuestions.Count = 0 Then
            Me.TrainingQuestion = Nothing
        End If

        Me.Hide()
    End Sub

    Private Sub cmdRemove_Click(pSender As Object, pArgs As EventArgs) Handles cmdRemove.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.TrainingQuestion = Nothing
            Me.Hide()
        End If
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Private Function GetLocalizedQuestion(pLanguage As String) As PGNTrainingLocalizedQuestion
        For Each LocalizedQuestion As PGNTrainingLocalizedQuestion In Me.TrainingQuestion.LocalizedQuestions
            If LocalizedQuestion.Language = pLanguage Then
                Return LocalizedQuestion
            End If
        Next LocalizedQuestion
        Return Nothing
    End Function

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        Select Case Me.tabTexts.SelectedTab.Name
            Case "TabEn"
                If grdAnswersEn.SelectedRows.Count > 0 Then
                    Me.RowIndex = grdAnswersEn.SelectedRows(0).Index
                Else
                    Exit Sub
                End If
                If Not grdAnswersEn.Rows(Me.RowIndex).IsNewRow Then
                    grdAnswersEn.Rows.RemoveAt(Me.RowIndex)
                End If
            Case "TabNl"
                If grdAnswersEn.SelectedRows.Count > 0 Then
                    Me.RowIndex = grdAnswersEn.SelectedRows(0).Index
                Else
                    Exit Sub
                End If
                If Not grdAnswersNl.Rows(Me.RowIndex).IsNewRow Then
                    grdAnswersNl.Rows.RemoveAt(Me.RowIndex)
                End If
        End Select
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        Select Case Me.tabTexts.SelectedTab.Name
            Case "TabEn"
                If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    Me.grdAnswersEn.Rows.Clear()
                End If
            Case "TabNl"
                If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    Me.grdAnswersNl.Rows.Clear()
                End If
        End Select

    End Sub

    Protected Overrides Sub Finalize()
        Me.TrainingQuestion = Nothing

        MyBase.Finalize()
    End Sub
End Class