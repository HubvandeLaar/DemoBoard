Option Explicit On

Imports ChessGlobals
Imports PGNLibrary

Public Class frmSelectGame
    Private CurrentFile As PGNFile
    Public SelectedGame As PGNGame

    Public Overloads Sub ShowDialog(pPgnFile As PGNFile)
        Me.ListGames(pPgnFile)

        MyBase.ShowDialog()
    End Sub

    Sub ListGames(pPGNFile As PGNFile)
        Dim GameIndex As Long, GameText(5) As String '6 Columns
        Try
            Me.Text = pPGNFile.FullFileName

            CurrentFile = pPGNFile
            lstGames.Items.Clear()

            For GameIndex = 0 To pPGNFile.PGNGames.Count - 1
                With pPGNFile.PGNGames(GameIndex)
                    GameText(0) = Str(GameIndex + 1)
                    GameText(1) = .Tags.GetPGNTag("White")
                    GameText(2) = .Tags.GetPGNTag("Black")
                    GameText(3) = .Tags.GetPGNTag("Result")
                    GameText(4) = .Tags.GetPGNTag("Date")
                    GameText(5) = .Tags.GetPGNTag("Title")

                    lstGames.Items.Add(New ListViewItem(GameText))
                End With
            Next GameIndex

            SelectedGame = Nothing

            Application.DoEvents()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub Games_MouseDoubleClick(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles lstGames.MouseDoubleClick
        Try
            If lstGames.SelectedItems.Count > 0 Then
                SelectedGame = CurrentFile.PGNGames(lstGames.SelectedItems(0).Index)
                Me.Hide()
            Else
                SelectedGame = Nothing
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdUp_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdUp.Click
        Dim Index As Integer, PGNGame As PGNGame
        Try
            If lstGames.SelectedItems.Count = 0 Then Exit Sub
            Index = lstGames.SelectedItems(0).Index
            If Index > 0 Then
                PGNGame = CurrentFile.PGNGames(Index)
                CurrentFile.PGNGames.MoveUp(PGNGame)
                ListGames(CurrentFile)
                lstGames.Items(Index - 1).Selected = True
                lstGames.Items(Index - 1).EnsureVisible()
                lstGames.Refresh() : lstGames.Focus()
            End If

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdDown_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdDown.Click
        Dim Index As Integer, PGNGame As PGNGame
        Try
            If lstGames.SelectedItems.Count = 0 Then Exit Sub
            Index = lstGames.SelectedItems(0).Index
            If Index < lstGames.Items.Count - 1 Then
                PGNGame = CurrentFile.PGNGames(Index)
                CurrentFile.PGNGames.MoveDown(PGNGame)
                ListGames(CurrentFile)
                lstGames.Items(Index + 1).Selected = True
                lstGames.Items(Index + 1).EnsureVisible()
                lstGames.Refresh() : lstGames.Focus()
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click1(pSender As Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            If lstGames.SelectedItems.Count > 0 Then
                SelectedGame = CurrentFile.PGNGames(lstGames.SelectedItems(0).Index)
                Me.Hide()
            Else
                SelectedGame = Nothing
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click1(pSender As Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        Try
            SelectedGame = Nothing
            Me.Hide()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

End Class