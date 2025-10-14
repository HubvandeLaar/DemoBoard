Option Explicit On

Imports ChessMessaging
Imports ChessCoach
Imports PGNLibrary

Public Class frmAnalysis

    Private gPGNGame As PGNGame

    Public Overloads Sub ShowDialog(pPGNGame As PGNGame)
        Try
            gPGNGame = pPGNGame
            lblWhite.Text = gPGNGame.Tags("White").Value
            lblBlack.Text = gPGNGame.Tags("Black").Value

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdStart_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdStart.Click
        Try
            Dim Feedback As New Feedback(cmbLevelWhite.SelectedIndex + 1, cmbLevelBlack.SelectedIndex + 1)

            UseWaitCursor = True
            cmbLevelWhite.Enabled = False
            cmbLevelBlack.Enabled = False
            cmdStart.Enabled = False
            cmdCancel.Enabled = False
            Refresh()
            Application.DoEvents()

            Feedback.AnaLyze(gPGNGame)
            gPGNGame.Tags.Add("Annotator", "DemoBoard")

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)

        Finally
            UseWaitCursor = False
            Close()

        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        Close()
    End Sub

    Protected Overrides Sub Finalize()
        gPGNGame = Nothing

        MyBase.Finalize()
    End Sub

End Class