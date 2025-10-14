Option Explicit On

Imports ChessGlobals
Imports ChessMessaging
Imports ChessMessaging.Messages
Imports PGNLibrary

Public Class frmEditNAGs

    Private gRowIndex As Integer

    Public Property NAGList As PGNNAGs

    Public Overloads Sub ShowDialog(pNAGList As String)
        Try
            'Populate Grid with existing NAGs in NAGList
            NAGList = New PGNNAGs(pNAGList)
            grdNAGs.Rows.Clear()
            For Each NAG As PGNNAG In NAGList
                grdNAGs.Rows.Add(New String() {CStr(NAG.PGNString), CStr(NAG.PrintPosition), NAG.Text(CurrentLanguage)})
            Next NAG

            'Populate the Combo-box rows with allPossibleNAGs
            Dim AllPossibleNAGs As New DataTable("AllNAGs")
            AllPossibleNAGs.Columns.Add("Code", GetType(String))
            AllPossibleNAGs.Columns.Add("Text", GetType(String))
            AllPossibleNAGs.PrimaryKey = New DataColumn() {AllPossibleNAGs.Columns("Code")}
            Dim NAG2 As New PGNNAG("$0")
            For C As Long = 1 To 255
                Try
                    NAG2.StoreValues(C) 'Throws exception at unknown NAGs
                    AllPossibleNAGs.Rows.Add(New String() {"$" & CStr(C),
                                                "$" & CStr(C) & "     " &
                                                If(NAG2.PrintPosition = PGNNAG.NAGPrintPosition.BEFORE, "Before", "After") & "     " &
                                                NAG2.Text(CurrentLanguage)})
                Catch
                End Try
            Next C

            Dim CodeColumn As DataGridViewComboBoxColumn = grdNAGs.Columns(0)
            CodeColumn.DataSource = AllPossibleNAGs
            CodeColumn.ValueMember = "Code"
            CodeColumn.DataPropertyName = "Code"
            CodeColumn.DisplayMember = "Text"
            CodeColumn.ValueType = GetType(String)

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub grdNAGs_CellValueChanged(pSender As Object, pArgs As DataGridViewCellEventArgs) Handles grdNAGs.CellValueChanged
        If pArgs.RowIndex = -1 Then Exit Sub
        If pArgs.ColumnIndex = 0 Then
            If Len(grdNAGs.Rows(pArgs.RowIndex).Cells(0).Value.ToString) > 4 Then
                grdNAGs.Rows(pArgs.RowIndex).Cells(0).Value = Trim(Microsoft.VisualBasic.Strings.Left(grdNAGs.Rows(pArgs.RowIndex).Cells(0).Value.ToString, 4))
            End If
            ' Stop
        End If
    End Sub

    Private Sub grdNAGs_CellValidating(pSender As Object, pArgs As DataGridViewCellValidatingEventArgs) Handles grdNAGs.CellValidating
        grdNAGs.Rows(pArgs.RowIndex).ErrorText = ""

        ' Don't try to validate the just created  'new row' until finished editing since there
        If grdNAGs.Rows(pArgs.RowIndex).IsNewRow Then Return


        grdNAGs.Rows(pArgs.RowIndex).Cells(1).Value = ""
        grdNAGs.Rows(pArgs.RowIndex).Cells(2).Value = ""
        Select Case pArgs.ColumnIndex
            Case 0  'Code
                Try 'NB New PGNNAG can throw exception
                    Dim NAG As New PGNNAG(Trim(Microsoft.VisualBasic.Strings.Left(pArgs.FormattedValue.ToString, 4)))
                    If Len(pArgs.FormattedValue.ToString) > 4 Then
                        grdNAGs.Rows(pArgs.RowIndex).Cells(0).Value = Trim(Microsoft.VisualBasic.Strings.Left(pArgs.FormattedValue.ToString, 4))
                    End If
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
            gRowIndex = pArgs.RowIndex
            mnuPopUp.Show(Me.grdNAGs, pArgs.Location)
        End If
    End Sub

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        If Not grdNAGs.Rows(gRowIndex).IsNewRow Then
            grdNAGs.Rows.RemoveAt(gRowIndex)
        End If
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.NAGList.Clear()
        End If
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.NAGList = New PGNNAGs("")
        For Each Row As DataGridViewRow In grdNAGs.Rows
            If Row.IsNewRow Then Continue For
            Dim NAG As New PGNNAG(CStr(Row.Cells(0).Value))
            Me.NAGList.Add(NAG)
        Next Row
        Me.Hide()
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Protected Overrides Sub Finalize()
        Me.NAGList = Nothing

        MyBase.Finalize()
    End Sub

End Class