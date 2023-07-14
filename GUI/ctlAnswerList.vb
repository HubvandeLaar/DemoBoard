Imports PGNLibrary

Public Class ctlAnswerList

    Public Event AnswerClicked(pCorrectAnswer As Boolean, pHalfMove As PGNHalfMove)

    Public Sub Clear()
        While pnlAnswerList.Controls.Count > 0
            Me.Remove(pnlAnswerList.Controls(0))
        End While
    End Sub

    Public Sub Add(pHalfMoves As List(Of PGNHalfMove))
        Dim HalfMoves As List(Of PGNHalfMove) = Shuffle(pHalfMoves)
        Dim CharCode As Integer = Asc("A")
        For Each HalfMove As PGNHalfMove In HalfMoves
            Add((HalfMove Is pHalfMoves(0)), Chr(CharCode), HalfMove)
            CharCode += 1
        Next HalfMove
        ShowAnswerList()
    End Sub

    Public Sub Add(pCorrectAnswer As Boolean, pChar As String, pPGNHalfMove As PGNHalfMove)
        Dim AnswerListRow As New ctlAnswerListRow(pCorrectAnswer, pChar, pPGNHalfMove)
        'Add Handler
        AddHandler AnswerListRow.AnswerClicked, AddressOf AnswerListRow_AnswerClicked
        pnlAnswerList.Controls.Add(AnswerListRow)

        'Fill contents of MoveListRow; calculating the Height
        AnswerListRow.HalfMove = pPGNHalfMove
        ' Return AnswerListRow
    End Sub

    Public Sub Remove(pAnswerListRow As ctlAnswerListRow)
        'Remove Handler
        RemoveHandler pAnswerListRow.AnswerClicked, AddressOf AnswerListRow_AnswerClicked
        pnlAnswerList.Controls.Remove(pAnswerListRow)
        pAnswerListRow.Dispose()
    End Sub

    Public Sub ShowAnswerList()
        Dim AnswerListRowTop As Long = -pnlAnswerList.VerticalScroll.Value
        For Each AnswerListRow As ctlAnswerListRow In pnlAnswerList.Controls
            AnswerListRow.Top = AnswerListRowTop
            AnswerListRow.Left = 0
            AnswerListRow.Width = Me.Width - AnswerListRow.Left - 18
            Application.DoEvents()
            AnswerListRowTop += AnswerListRow.Height
        Next AnswerListRow
    End Sub

    Private Sub AnswerListRow_AnswerClicked(pCorrectAnswer As Boolean, pHalfMove As PGNHalfMove)
        RaiseEvent AnswerClicked(pCorrectAnswer, pHalfMove)
    End Sub

    Private Function Shuffle(Of tItem)(pList As List(Of tItem)) As List(Of tItem)
        Dim RandomNumber As Random = New Random()
        Shuffle = pList.OrderBy(Function(pParm) RandomNumber.Next()).ToList()
    End Function

End Class
