Option Explicit On

Imports ChessGlobals
Imports PGNLibrary

Public Class frmSelectVariant

    Public ChoosenVariant As PGNHalfMove
    Private Variants As List(Of PGNHalfMove)

    Public Overloads Sub ShowDialog(pVariants As List(Of PGNHalfMove))
        Dim VariantsRow(3) As String
        Try
            Me.lstVariants.Items.Clear()
            Me.ChoosenVariant = Nothing
            Me.Variants = pVariants

            'Store Main Variant
            For Each PGNHalfMove As PGNHalfMove In pVariants
                VariantsRow(0) = Space(PGNHalfMove.VariantLevel * 2) & PGNHalfMove.MoveNrString(True) & PGNHalfMove.MoveText
                If PGNHalfMove.CommentAfter Is Nothing Then
                    VariantsRow(1) = ""
                Else
                    VariantsRow(1) = PGNHalfMove.CommentAfter.Text(True)
                End If
                Me.lstVariants.Items.Add(New ListViewItem(VariantsRow))
            Next PGNHalfMove
            Me.lstVariants.TopItem.Selected = True

            MyBase.ShowDialog()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstVariants_SelectedIndexChanged(pSender As System.Object, pArgs As System.EventArgs) Handles lstVariants.SelectedIndexChanged
        Dim Index As Long
        Try
            If lstVariants.SelectedItems.Count > 0 Then
                Index = lstVariants.SelectedItems(0).Index
                Me.ChoosenVariant = Me.Variants(Index)
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstVariants_DoubleClick(pSender As Object, pArgs As System.EventArgs) Handles lstVariants.DoubleClick
        Dim Index As Long
        Try
            If lstVariants.SelectedItems.Count > 0 Then
                Index = lstVariants.SelectedItems(0).Index
                Me.ChoosenVariant = Me.Variants(Index)
                Me.Hide()
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Dim Index As Long
        Try
            If lstVariants.SelectedItems.Count > 0 Then
                Index = lstVariants.SelectedItems(0).Index
                Me.ChoosenVariant = Me.Variants(Index)
                Me.Hide()
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef pMsg As System.Windows.Forms.Message, pKeyData As System.Windows.Forms.Keys) As Boolean
        Select Case pKeyData
            Case Keys.Right : cmdOK_Click(Nothing, Nothing)
        End Select

        Return MyBase.ProcessCmdKey(pMsg, pKeyData)
    End Function

End Class