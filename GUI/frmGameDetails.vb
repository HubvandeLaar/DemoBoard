Option Explicit On

Imports ChessGlobals
Imports PGNLibrary

Public Class frmGameDetails

    Private WithEvents gfrmMainForm As frmMainForm

    Public Event DoubleClicked()

    Public Sub New(pfrmMainForm As frmMainForm, pPGNGame As PGNGame)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
        Me.UpdateDetails(pPGNGame)
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNGame) Handles gfrmMainForm.GameChanged
        Me.UpdateDetails(pPGNGame)
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        Call ApplyLanguageToCurrentForm(Me)
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
            lblWhiteName.Text = pPGNGame.Tags("White").Value
            ELO = pPGNGame.Tags("WhiteElo").Value
            lblWhiteELO.Text = If(ELO = "", "", "(" & ELO & ")")
            lblBlackName.Text = pPGNGame.Tags("Black").Value
            ELO = pPGNGame.Tags("BlackElo").Value
            lblBlackELO.Text = If(ELO = "", "", "(" & ELO & ")")
            lblSiteYear.Text = pPGNGame.Tags("Site").Value & " " _
                             & Strings.Left(pPGNGame.Tags("Date").Value, 4)
            lblEvent.Text = pPGNGame.Tags("Event").Value
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
        gfrmMainForm = Nothing

        MyBase.Finalize()
    End Sub
End Class