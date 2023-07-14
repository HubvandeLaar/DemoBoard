Option Explicit On

Imports ChessGlobals
Imports PGNLibrary
Imports ChessMaterials

Class frmEditArrows

    Public ArrowList As PGNArrowList

    Private RowIndex As Integer

    Public Overloads Sub ShowDialog(pArrowList As String)
        Try
            ArrowList = New PGNArrowList(pArrowList)
            grdArrows.Rows.Clear()
            For Each Arrow As Arrow In ArrowList
                grdArrows.Rows.Add(New String() {Arrow.Color, Arrow.FromFieldName, Arrow.ToFieldName})
            Next Arrow

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub grdArrows_CellValidating(pSender As Object, pArgs As DataGridViewCellValidatingEventArgs) Handles grdArrows.CellValidating
        grdArrows.Rows(pArgs.RowIndex).ErrorText = ""

        ' Don't try to validate the just created  'new row' until finished editing since there
        If grdArrows.Rows(pArgs.RowIndex).IsNewRow Then Return

        Select Case pArgs.ColumnIndex
            Case 0  'Color
                If Not pArgs.FormattedValue.ToString() Like "[RGY]" Then
                    MsgBox(MessageText("InvalidColor", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
            Case 1, 2 'Form and To Field
                If Not pArgs.FormattedValue.ToString() Like "[abcdefgh][12345678]" Then
                    MsgBox(MessageText("InvalidFieldName", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
        End Select
    End Sub

    Private Sub grdArrows_CellMouseDown(pSender As Object, pArgs As DataGridViewCellMouseEventArgs) Handles grdArrows.CellMouseDown
        If pArgs.Button = MouseButtons.Right Then
            grdArrows.Rows(pArgs.RowIndex).Selected = True
            Me.RowIndex = pArgs.RowIndex
            mnuPopUp.Show(Me.grdArrows, pArgs.Location)
        End If
    End Sub

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        If Not grdArrows.Rows(Me.RowIndex).IsNewRow Then
            grdArrows.Rows.RemoveAt(Me.RowIndex)
        End If
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.ArrowList.Clear()
            Me.Hide()
        End If
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.ArrowList = New PGNArrowList("")
        For Each Row As DataGridViewRow In grdArrows.Rows
            If Row.IsNewRow Then Continue For
            Dim Arrow As New Arrow(CStr(Row.Cells(0).Value), CStr(Row.Cells(1).Value), CStr(Row.Cells(2).Value))
            Me.ArrowList.Add(Arrow)
        Next Row
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

End Class