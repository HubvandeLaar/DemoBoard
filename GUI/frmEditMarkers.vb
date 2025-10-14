Option Explicit On

Imports ChessMessaging
Imports ChessMessaging.Messages
Imports PGNLibrary
Imports ChessMaterials

Public Class frmEditMarkers

    Private gRowIndex As Integer

    Public Property MarkerList As PGNMarkerList

    Public Overloads Sub ShowDialog(pMarkerListXPGN As String)
        Try
            MarkerList = New PGNMarkerList(pMarkerListXPGN)
            grdMarkers.Rows.Clear()
            For Each Marker As Marker In MarkerList
                grdMarkers.Rows.Add(New String() {Marker.Symbol, Marker.FieldName})
            Next Marker

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub grdMarkers_CellValidating(pSender As Object, pArgs As DataGridViewCellValidatingEventArgs) Handles grdMarkers.CellValidating
        grdMarkers.Rows(pArgs.RowIndex).ErrorText = ""

        ' Don't try to validate the just created  'new row' until finished editing since there
        If grdMarkers.Rows(pArgs.RowIndex).IsNewRow Then Return

        Select Case pArgs.ColumnIndex
            Case 0  'Symbol
                If Not pArgs.FormattedValue.ToString() Like "[RGY+-O#.*]" Then
                    MsgBox(MessageText("InvalidSymbol", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
            Case 1 'Field Name
                If Not pArgs.FormattedValue.ToString() Like "[abcdefgh][12345678]" Then
                    MsgBox(MessageText("InvalidFieldName", pArgs.FormattedValue.ToString()), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    pArgs.Cancel = True
                End If
        End Select
    End Sub

    Private Sub grdMarkers_CellMouseDown(pSender As Object, pArgs As DataGridViewCellMouseEventArgs) Handles grdMarkers.CellMouseDown
        If pArgs.Button = MouseButtons.Right Then
            grdMarkers.Rows(pArgs.RowIndex).Selected = True
            gRowIndex = pArgs.RowIndex
            mnuPopUp.Show(Me.grdMarkers, pArgs.Location)
        End If
    End Sub

    Private Sub mnuDeleteRow_Click(pSender As Object, pArgs As EventArgs) Handles mnuDeleteRow.Click
        If Not grdMarkers.Rows(gRowIndex).IsNewRow Then
            grdMarkers.Rows.RemoveAt(gRowIndex)
        End If
    End Sub

    Private Sub mnuClearAll_Click(pSender As Object, pArgs As EventArgs) Handles mnuClearAll.Click
        If MsgBox(MessageText("Are You Sure"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Me.MarkerList.Clear()
            Me.Hide()
        End If
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click
        Me.MarkerList = New PGNMarkerList("")
        For Each Row As DataGridViewRow In grdMarkers.Rows
            If Row.IsNewRow Then Continue For
            Dim Marker As New Marker(CStr(Row.Cells(0).Value), CStr(Row.Cells(1).Value))
            Me.MarkerList.Add(Marker)
        Next Row
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Protected Overrides Sub Finalize()
        Me.MarkerList = Nothing

        MyBase.Finalize()
    End Sub

End Class