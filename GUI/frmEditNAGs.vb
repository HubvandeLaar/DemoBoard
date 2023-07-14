Option Explicit On

Imports ChessGlobals
Imports PGNLibrary
Imports ChessMaterials

Public Class frmEditNAGs

    Public NAGList As PGNNAGs

    Private RowIndex As Integer

    Public Overloads Sub ShowDialog(pNAGList As String)
        Try
            NAGList = New PGNNAGs(pNAGList)
            grdNAGs.Rows.Clear()
            For Each NAG As PGNNAG In NAGList
                grdNAGs.Rows.Add(New String() {CStr(NAG.PGNString), CStr(NAG.PrintPosition), NAG.Text})
            Next NAG

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub grdNAGs_CellValidating(pSender As Object, pArgs As DataGridViewCellValidatingEventArgs) Handles grdNAGs.CellValidating
        grdNAGs.Rows(pArgs.RowIndex).ErrorText = ""

        ' Don't try to validate the just created  'new row' until finished editing since there
        If grdNAGs.Rows(pArgs.RowIndex).IsNewRow Then Return

        grdNAGs.Rows(pArgs.RowIndex).Cells(1).Value = ""
        grdNAGs.Rows(pArgs.RowIndex).Cells(2).Value = ""
        Select Case pArgs.ColumnIndex
            Case 0  'Code
                Try 'New PGNNAG can throw exception
                    Dim NAG As New PGNNAG(pArgs.FormattedValue.ToString())
                    grdNAGs.Rows(pArgs.RowIndex).Cells(1).Value = If(NAG.PrintPosition = PGNNAG.NAGPrintPosition.BEFORE, "Before", "After")
                    grdNAGs.Rows(pArgs.RowIndex).Cells(2).Value = NAG.Text
                Catch Exception As Exception
                    MsgBox(MessageText("InvalidNAGCode", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End Try
        End Select
    End Sub

    Private Sub grdNAGs_CellMouseDown(pSender As Object, pArgs As DataGridViewCellMouseEventArgs) Handles grdNAGs.CellMouseDown
        If pArgs.Button = MouseButtons.Right Then
            grdNAGs.Rows(pArgs.RowIndex).Selected = True
            Me.RowIndex = pArgs.RowIndex
            mnuPopUp.Show(Me.grdNAGs, pArgs.Location)
        End If
    End Sub

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        If Not grdNAGs.Rows(Me.RowIndex).IsNewRow Then
            grdNAGs.Rows.RemoveAt(Me.RowIndex)
        End If
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.NAGList.Clear()
            Me.Hide()
        End If
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.NAGList = New PGNNAGs("")
        For Each Row As DataGridViewRow In grdNAGs.Rows
            If Row.IsNewRow Then Continue For
            Dim NAG As New PGNNAG(CStr(CStr(Row.Cells(0).Value)))
            Me.NAGList.Add(NAG)
        Next Row
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

End Class