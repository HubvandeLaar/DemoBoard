Option Explicit On

Imports ChessGlobals
Imports PGNLibrary
Imports ChessMaterials

Class frmEditTexts

    Public TextList As PGNTextList

    Private RowIndex As Integer

    Public Overloads Sub ShowDialog(pTextList As String)
        Try
            TextList = New PGNTextList(pTextList)
            grdTexts.Rows.Clear()
            For Each Text As Text In TextList
                grdTexts.Rows.Add(New String() {Text.Color, Text.FieldName, Text.Text})
            Next Text

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub grdTexts_CellValidating(pSender As Object, pArgs As DataGridViewCellValidatingEventArgs) Handles grdTexts.CellValidating
        grdTexts.Rows(pArgs.RowIndex).ErrorText = ""

        ' Don't try to validate the just created  'new row' until finished editing since there
        If grdTexts.Rows(pArgs.RowIndex).IsNewRow Then Return

        Select Case pArgs.ColumnIndex
            Case 0  'Color
                If Not pArgs.FormattedValue.ToString() Like "[RGY]" Then
                    MsgBox(MessageText("InvalidColor", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
            Case 1 'Field Name
                If Not pArgs.FormattedValue.ToString() Like "[abcdefgh][12345678]" Then
                    MsgBox(MessageText("InvalidFieldName", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
        End Select
    End Sub

    Private Sub grdTexts_CellMouseDown(pSender As Object, pArgs As DataGridViewCellMouseEventArgs) Handles grdTexts.CellMouseDown
        If pArgs.Button = MouseButtons.Right Then
            grdTexts.Rows(pArgs.RowIndex).Selected = True
            Me.RowIndex = pArgs.RowIndex
            mnuPopUp.Show(Me.grdTexts, pArgs.Location)
        End If
    End Sub

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        If Not grdTexts.Rows(Me.RowIndex).IsNewRow Then
            grdTexts.Rows.RemoveAt(Me.RowIndex)
        End If
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.TextList.Clear()
            Me.Hide()
        End If
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.TextList = New PGNTextList("")
        For Each Row As DataGridViewRow In grdTexts.Rows
            If Row.IsNewRow Then Continue For
            Dim Text As New Text(CStr(Row.Cells(0).Value), CStr(Row.Cells(1).Value), CStr(Row.Cells(2).Value))
            Me.TextList.Add(Text)
        Next Row
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

End Class
