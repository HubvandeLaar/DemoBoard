Option Explicit On

Imports ChessMessaging
Imports PGNLibrary

Public Class frmEditTitleAndMemo

    Private gPGNGame As PGNGame

    Public Property OKPressed As Boolean

    Public Overloads Sub ShowDialog(pPGNGame As PGNGame)
        Try
            gPGNGame = pPGNGame
            OKPressed = False

            txtTitle.Text = gPGNGame.Tags("Title").Value
            txtMemo.Text = gPGNGame.Tags("Memo").Value

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            OKPressed = True

            gPGNGame.Tags.Add("Title", txtTitle.Text)
            gPGNGame.Tags.Add("Memo", txtMemo.Text)

            Me.Hide()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        OKPressed = False
        Me.Hide()
    End Sub

    Protected Overrides Sub Finalize()
        gPGNGame = Nothing

        MyBase.Finalize()
    End Sub

End Class