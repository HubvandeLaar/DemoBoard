Imports ChessGlobals
Imports PGNLibrary
Imports PGNLibrary.PGNNAG
Imports PGNLibrary.PGNNAG.NAGPrintPosition

Public Class ctlAnswerListRow

    Private gHalfMove As PGNHalfMove
    Private gCorrectAnswer As Boolean

    Public Event AnswerClicked(pCorrectAnswer As Boolean, pHalfMove As PGNHalfMove)

    Public Property HalfMove As PGNHalfMove
        Set(pHalfMove As PGNHalfMove)
            gHalfMove = pHalfMove

            rtbMoveText.Clear()
            If pHalfMove.Result <> "" Then
                rtbMoveText.AppendText(pHalfMove.MoveText(CurrentLanguage))
            Else
                rtbMoveText.AppendText(pHalfMove.MoveNr & If(pHalfMove.Color = ChessColor.WHITE, ". ", "... "))
                If pHalfMove.NAGs.Count(BEFORE) > 0 Then
                    Me.PrintNAGs(pHalfMove, BEFORE)
                End If
                rtbMoveText.AppendText(pHalfMove.MoveText(CurrentLanguage))
                If pHalfMove.NAGs.Count(AFTER) > 0 Then
                    Me.PrintNAGs(pHalfMove, AFTER)
                End If
            End If
        End Set
        Get
            Return gHalfMove
        End Get
    End Property

    Public Property CorrectAnswer As Boolean
        Set(pCorrectAnswer As Boolean)
            gCorrectAnswer = pCorrectAnswer
        End Set
        Get
            Return gCorrectAnswer
        End Get
    End Property

    Public Sub New(pCorrectAnswer As Boolean, pChar As String, pPGNHalfMove As PGNHalfMove)
        ' This call is required by the designer.
        InitializeComponent()

        Me.HalfMove = pPGNHalfMove
        Me.cmdReply.Text = pChar
        Me.CorrectAnswer = pCorrectAnswer
    End Sub

    Private Sub cmdReply_Click(pSender As Object, pArgs As EventArgs) Handles cmdReply.Click
        RaiseEvent AnswerClicked(gCorrectAnswer, gHalfMove)
    End Sub

    'Private Methods and Functions

    Private Sub PrintNAGs(pHalfMove As PGNHalfMove, pPrintPosition As NAGPrintPosition)
        Dim NAGText As String
        For Each NAG As PGNNAG In pHalfMove.NAGs
            If NAG.PrintPosition = pPrintPosition Then
                Select Case NAG.Type
                    Case NAGType.CODE 'Inserting one Symbol specified by pTextacterNumber
                        rtbMoveText.SelectionStart = rtbMoveText.TextLength 'Set Start to End of Text
                        rtbMoveText.AppendText(ChrW(NAG.Code))
                        rtbMoveText.SelectionLength = 1
                        rtbMoveText.SelectionFont = New Font(NAG.Font, 14)
                    Case NAGType.TEXT 'Inserting the text using the default font for the current style
                        NAGText = NAG.Text(CurrentLanguage)
                        If NAGText <> "" Then
                            rtbMoveText.SelectionStart = rtbMoveText.TextLength 'Set Start to End of Text
                            Me.rtbMoveText.AppendText(NAGText & " ")
                            rtbMoveText.SelectionLength = Len(NAGText)
                            rtbMoveText.SelectionFont = New Font(NAG.Font, rtbMoveText.Font.Size)
                        End If
                End Select
            End If
        Next NAG
    End Sub

End Class
