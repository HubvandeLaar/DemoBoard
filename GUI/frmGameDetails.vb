Imports System.ComponentModel
Imports ChessGlobals
Imports PGNLibrary

Public Class frmGameDetails

    Private WithEvents gfrmMainForm As frmMainForm

    Public Event DoubleClicked()

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
        Me.UpdateDetails(gfrmMainForm.PGNGame)
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNGame) Handles gfrmMainForm.GameChanged
        Me.UpdateDetails(pPGNGame)
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ChangeLanguageCurrentForm(Me)
    End Sub

    Private Sub UpdateDetails(pPGNGame As PGNGame)
        If pPGNGame Is Nothing Then
            lblWhiteName.Text = ""
            lblWhiteELO.Text = ""
            lblBlackName.Text = ""
            lblSiteYear.Text = ""
            lblEvent.Text = ""
        Else
            Dim ELO As String
            lblWhiteName.Text = pPGNGame.Tags.GetPGNTag("White")
            ELO = pPGNGame.Tags.GetPGNTag("WhiteElo")
            lblWhiteELO.Text = If(ELO = "", "", "(" & ELO & ")")
            lblBlackName.Text = pPGNGame.Tags.GetPGNTag("Black")
            ELO = pPGNGame.Tags.GetPGNTag("BlackElo")
            lblBlackELO.Text = If(ELO = "", "", "(" & ELO & ")")
            lblSiteYear.Text = pPGNGame.Tags.GetPGNTag("Site") & " " _
                             & Strings.Left(pPGNGame.Tags.GetPGNTag("Date"), 4)
            lblEvent.Text = pPGNGame.Tags.GetPGNTag("Event")
        End If
    End Sub

    Private Sub frmGameDetails_DoubleClick(pSender As Object, pArgs As EventArgs) Handles Me.DoubleClick,
                                                                                          PictureBox2.DoubleClick, lblWhiteName.DoubleClick, lblWhiteELO.DoubleClick,
                                                                                          PictureBox1.DoubleClick, lblBlackName.DoubleClick, lblBlackELO.DoubleClick,
                                                                                          Label4.DoubleClick, lblSiteYear.DoubleClick,
                                                                                          Label5.DoubleClick, lblEvent.DoubleClick
        RaiseEvent DoubleClicked()
    End Sub

    Private Sub frmGameDetails__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        Me.gfrmMainForm = Nothing

        MyBase.Finalize()
    End Sub
End Class