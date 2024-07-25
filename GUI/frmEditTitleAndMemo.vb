Imports System.ComponentModel
Imports ChessGlobals
Imports PGNLibrary

Public Class frmEditTitleAndMemo

    Public PGNGame As PGNGame
    Public OKPressed As Boolean

    Public Overloads Sub ShowDialog(pPGNGame As PGNGame)
        Try
            PGNGame = pPGNGame
            OKPressed = False

            txtTitle.Text = PGNGame.Tags.GetPGNTag("Title")
            txtMemo.Text = PGNGame.Tags.GetPGNTag("Memo")

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            OKPressed = True

            PGNGame.Tags.Add("Title", txtTitle.Text)
            PGNGame.Tags.Add("Memo", txtMemo.Text)

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
        Me.PGNGame = Nothing

        MyBase.Finalize()
    End Sub

End Class