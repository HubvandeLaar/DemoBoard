Imports System.ComponentModel
Imports System.Security.Cryptography.X509Certificates
Imports System.Windows.Forms.VisualStyles
Imports ChessGlobals
Imports PGNLibrary

Public Class frmTitleAndMemo

    Private WithEvents gfrmMainform As frmMainForm
    Public Event DoubleClicked()

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainform = pfrmMainForm
    End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNGame) Handles gfrmMainform.GameChanged
        Me.UpdateTitleAndMemo(pPGNGame)
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainform.LanguageChanged
        Call ChangeLanguageCurrentForm(Me)
    End Sub

    Private Sub gfrmMainForm_SizeChanged(pSender As Object, pArgs As EventArgs) Handles gfrmMainform.SizeChanged
        txtTitle.Font = New Font(txtTitle.Font.FontFamily, gfrmMainform.Height / 32)
        txtMemo.Font = New Font(txtMemo.Font.FontFamily, gfrmMainform.Height / 42)
        Me.ReArrange()
    End Sub

    Private Sub UpdateTitleAndMemo(pPGNGame As PGNGame)
        txtTitle.Text = pPGNGame.Tags.GetPGNTag("Title")
        txtMemo.Text = pPGNGame.Tags.GetPGNTag("Memo")
        Me.ReArrange()
    End Sub

    Private Sub ReArrange()
        txtTitle.Height = TitleHeight()
        txtMemo.Top = txtTitle.Top + txtTitle.Height
        txtMemo.Height = Me.ClientSize.Height - txtTitle.Height
    End Sub

    Private Function TitleHeight()
        If txtTitle.Text = "" Then
            Return 0
        End If
        Dim TitleSize As Size = New Size(txtTitle.Width, Int32.MaxValue)
        TitleSize = TextRenderer.MeasureText(txtTitle.Text, txtTitle.Font, TitleSize, TextFormatFlags.WordBreak)
        Return TitleSize.Height + 4
    End Function

    Private Sub frmTitleAndMemo__Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainform = Nothing
    End Sub

    Private Sub frmTitleAndMemo_DoubleClick(pSender As Object, pArgs As EventArgs) Handles Me.DoubleClick,
                                                                                           txtTitle.DoubleClick,
                                                                                           txtMemo.DoubleClick
        RaiseEvent DoubleClicked()
    End Sub


    Private Sub txtTitle_GotFocus(pSender As Object, pArgs As EventArgs) Handles txtTitle.GotFocus
        txtTitle.Enabled = False
        Application.DoEvents()
        txtTitle.Enabled = True
    End Sub

    Private Sub txtMemo_GotFocus(pSender As Object, pArgs As EventArgs) Handles txtMemo.GotFocus
        txtMemo.Enabled = False
        Application.DoEvents()
        txtMemo.Enabled = True
    End Sub

End Class