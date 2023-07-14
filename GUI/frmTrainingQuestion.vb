Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports PGNLibrary

Public Class frmTrainingQuestion

    '    Public ButtonPressed As String = ""  '(Retry, Next)

    Private gTrainingHalfMove As PGNHalfMove
    Private gLocalizedQuestion As PGNTrainingLocalizedQuestion
    Private gTimesHintRequested As Integer = 0
    Private gCorrectAnswer As PGNTrainingAnswer
    Private gScore As Integer = 0
    Private gTotalScore As Integer = 0
    Private gIncorrectSubVariant As PGNHalfMove = Nothing

    Dim WithEvents gfrmMainform As frmMainForm

    Public Event RetryPressed(pTrainingHalfMove As PGNHalfMove)
    Public Event NextPressed(pTrainingHalfMove As PGNHalfMove)
    Public Event DetailsPressed(pIncorrectSubVariant As PGNHalfMove)
    Public Event SolutionPressed(pTrainingHalfMove As PGNHalfMove, pCorrectAnswer As PGNTrainingAnswer)

    Private Property Score As Integer
        Set(pScore As Integer)
            gScore = pScore
            lblScore.Text = Format(gScore) & If(gCorrectAnswer Is Nothing, "", "/" & Format(gCorrectAnswer.Points))
        End Set
        Get
            Return gScore
        End Get
    End Property

    Private Property TotalScore As Integer
        Set(pTotalScore As Integer)
            gTotalScore = pTotalScore
            'NB here only add results in presentation
            'This because every retry of a not so good answer would add up the total score
            'Now only added when Next is pressed
            lblTotalScore.Text = Format(gTotalScore + gScore) & "/" & Format(gCorrectAnswer.Points)
        End Set
        Get
            Return gTotalScore
        End Get
    End Property

    Private ReadOnly Property HintToShow As String
        Get
            Select Case gTimesHintRequested
                Case 0 : Return gLocalizedQuestion.Hint1
                Case 1 : Return gLocalizedQuestion.Hint2
                Case Else : Return ""
            End Select
        End Get
    End Property

    Public Overloads Sub Show(pTrainingHalfMove As PGNHalfMove, pfrmMainForm As frmMainForm)
        gfrmMainform = pfrmMainForm

        'Show me Right from Mainform
        Me.Top = gfrmMainform.Top
        Me.Left = gfrmMainform.Left + gfrmMainform.Width
        If Screen.FromPoint(gfrmMainform.Location).Bounds.Width < Me.Left + Me.Width Then
            Me.Left = Screen.FromPoint(gfrmMainform.Location).Bounds.Width - Me.Width
        End If

        gTrainingHalfMove = pTrainingHalfMove
        gLocalizedQuestion = pTrainingHalfMove.TrainingQuestion.GetLocalizedQuestion()
        gCorrectAnswer = gLocalizedQuestion.CorrectAnswer
        gTimesHintRequested = 0

        Score = 0
        lblText.Text = If(gLocalizedQuestion.Question = "", MessageText("TrainingQuestion"), gLocalizedQuestion.Question)
        cmdHint.Enabled = (HintToShow <> "")
        cmdDetails.Enabled = False
        cmdSolution.Enabled = True
        cmdRetry.Enabled = False
        cmdNext.Enabled = False
        MyBase.Show()
    End Sub

    Private Sub SetModal()
        'Make myself like a Modal Form
        gfrmMainform.Enabled = False
        Me.Activate()
    End Sub

    Private Sub SetModeLess()
        'Make myself like a Modal Form
        gfrmMainform.Enabled = True
        gfrmMainform.Activate()
    End Sub

    Public Sub CheckAnswer(pPiece As String, pFromFieldName As String, pToFieldName As String, pPromotionPiece As String)
        Try
            If Compare(gCorrectAnswer.Move, pPiece, pFromFieldName, pToFieldName, pPromotionPiece) = True Then
                'The correct answer
                lblText.Text = gLocalizedQuestion.Question & vbCrLf & vbCrLf & If(gCorrectAnswer.FeedBack = "", MessageText("CorrectAnswer"), gCorrectAnswer.FeedBack)
                Score = gCorrectAnswer.Points
                cmdHint.Enabled = False
                cmdDetails.Enabled = False
                cmdSolution.Enabled = False
                cmdRetry.Enabled = False
                cmdNext.Enabled = True
                Exit Sub
            End If

            gIncorrectSubVariant = FindSubVariant(pPiece, pFromFieldName, pToFieldName, pPromotionPiece)

            For Each Answer As PGNTrainingAnswer In gLocalizedQuestion.Answers
                If Compare(Answer.Move, pPiece, pFromFieldName, pToFieldName, pPromotionPiece) = True Then
                    'One of the specified answers
                    lblText.Text = gLocalizedQuestion.Question & vbCrLf & vbCrLf & If(Answer.FeedBack = "", MessageText("IncorrectAnswer2"), Answer.FeedBack)
                    Score = Answer.Points
                    cmdHint.Enabled = (HintToShow <> "")
                    cmdDetails.Enabled = (gIncorrectSubVariant IsNot Nothing)
                    cmdSolution.Enabled = True
                    cmdRetry.Enabled = True
                    cmdNext.Enabled = True
                    Exit Sub
                End If
            Next Answer

            'Not specified incorrect answers
            lblText.Text = gLocalizedQuestion.Question & vbCrLf & vbCrLf & MessageText("IncorrectAnswer")
            Score = 0
            cmdHint.Enabled = (HintToShow <> "")
            cmdDetails.Enabled = (gIncorrectSubVariant IsNot Nothing)
            cmdSolution.Enabled = True
            cmdRetry.Enabled = True
            cmdNext.Enabled = True

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Function Compare(pMove As PGNHalfMove, pPiece As String, pFromFieldName As String, pToFieldName As String, pPromotionPiece As String) As Boolean
        'Compare Castlings
        If pMove.Castling <> "" Then
            If pMove.Castling = "O-O" Then
                If (pFromFieldName = "e1" Or pFromFieldName = "e8") _
                And (pToFieldName = "g1" Or pToFieldName = "g8") Then Return True
                If pPiece <> "" _
                AndAlso pPiece = "K" _
                AndAlso (pToFieldName = "g1" Or pToFieldName = "g8") Then Return True
            End If
            If pMove.Castling = "O-O-O" Then
                If (pFromFieldName = "e1" Or pFromFieldName = "e8") _
                And (pToFieldName = "c1" Or pToFieldName = "c8") Then Return True
                If pPiece <> "" _
                AndAlso pPiece = "K" _
                AndAlso (pToFieldName = "c1" Or pToFieldName = "c8") Then Return True
            End If
            Return False
        End If

        'Compare Pieces
        If pMove.Piece IsNot Nothing _
        AndAlso pPiece <> "" _
        AndAlso pMove.Piece.MoveName <> pPiece Then Return False
        'Compare FromColumns
        If pMove.FromColumnName <> "" _
        AndAlso pMove.FromColumnName <> Strings.Left(pFromFieldName, 1) Then Return False
        'Compare FromRows
        If pMove.FromRowName <> "" _
        AndAlso pMove.FromRowName <> Strings.Right(pFromFieldName, 1) Then Return False
        'Compare ToFields
        If pMove.ToFieldName <> "" _
        AndAlso pMove.ToFieldName <> pToFieldName Then Return False
        'Compare PromotionPieces
        If pMove.PromotionPiece IsNot Nothing _
        AndAlso pMove.PromotionPiece.MoveName <> pPromotionPiece Then Return False

        'Passed all tests
        Return True
    End Function

    Private Function FindSubVariant(pPiece As String, pFromFieldName As String, pToFieldName As String, pPromotionPiece As String) As PGNHalfMove
        For Each Move As PGNHalfMove In gTrainingHalfMove.SubVariants
            If Move.Piece.MoveName = pPiece _
            And Move.ToFieldName = pToFieldName Then
                If Move.FromColumnName <> "" And Move.FromColumnName <> Strings.Left(pFromFieldName, 1) Then Continue For
                If Move.FromRowName <> "" And Move.FromRowName <> Strings.Right(pFromFieldName, 1) Then Continue For
                If pPromotionPiece <> "" And Not (Move.Rest Like "*" & pPromotionPiece & "*") Then Continue For
                Return Move
            End If
        Next
        Return Nothing
    End Function

    Private Sub cmdNext_Click(pSender As Object, pArgs As EventArgs) Handles cmdNext.Click
        Try
            SetModeLess()
            Me.Hide()
            gTotalScore += gScore 'Now really add to Total
            RaiseEvent NextPressed(Me.gTrainingHalfMove)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdHint_Click(pSender As Object, pArgs As EventArgs) Handles cmdHint.Click
        Try
            lblText.Text = gLocalizedQuestion.Question & vbCrLf & vbCrLf & HintToShow
            gTimesHintRequested += 1

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdDetails_Click(pSender As Object, pArgs As EventArgs) Handles cmdDetails.Click
        Try
            RaiseEvent DetailsPressed(gIncorrectSubVariant)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdRetry_Click(pSender As Object, pArgs As EventArgs) Handles cmdRetry.Click
        Try
            Me.SetModeLess()
            lblText.Text = If(gLocalizedQuestion.Question = "", MessageText("TrainingQuestion"), gLocalizedQuestion.Question)
            RaiseEvent RetryPressed(gTrainingHalfMove)

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdSolution_Click(pSender As Object, pArgs As EventArgs) Handles cmdSolution.Click
        Try
            'Me.SetModal()
            RaiseEvent SolutionPressed(gTrainingHalfMove, gCorrectAnswer)
            cmdHint.Enabled = False
            cmdDetails.Enabled = False
            cmdSolution.Enabled = False
            cmdRetry.Enabled = False
            cmdNext.Enabled = True

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gfrmMainform_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainform.LanguageChanged
        Call ChangeLanguageCurrentForm(Me)
    End Sub

End Class