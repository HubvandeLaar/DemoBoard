Option Explicit On

Imports ChessEngine.Engine
Imports ChessGlobals.modChessLanguage
Imports ChessGlobals.ChessLanguage
Imports ChessMaterials
Imports PGNLibrary

Public Class frmStockfish

    Private WithEvents gfrmMainForm As frmMainForm
    Private WithEvents gEngine As ChessEngine.Engine

    Private gFEN As String

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
        gEngine = New ChessEngine.Engine()
    End Sub

    Private Sub chkOnOff_CheckedChanged(pSender As Object, pArgs As EventArgs) Handles chkOnOff.CheckedChanged
        If chkOnOff.Checked = True Then
            gEngine.StartEngine()
            ClearVariants()
            gEngine.Best3Variants(gFEN)
        Else
            gEngine.StopEngine()
        End If
    End Sub

    Private Sub Engine_InfoMessage(pDepth As Integer, pIndex As Integer, pScoreType As ChessEngine.Engine.ScoreType, pScore As Integer, pMoves As String) Handles gEngine.InfoMessage
        If pIndex > 0 Then
            Select Case pScoreType
                Case ScoreType.cp
                    UpdateVariant(pIndex - 1, String.Format(If(CurrentLanguage = NEDERLANDS, "Score {0:0.00} Diepte {1:0} {2}", "Score {0:0.00} Depth {1:0} {2}"), pScore / 100, pDepth, pMoves))
                Case ScoreType.mate
                    UpdateVariant(pIndex - 1, String.Format(If(CurrentLanguage = NEDERLANDS, "Mat in {0} Diepte {1:0} {2}", "Checkmate in {0} Depth {1:0} {2}"), pScore, pDepth, pMoves))
                Case ScoreType.upperbound
                    UpdateVariant(pIndex - 1, String.Format(If(CurrentLanguage = NEDERLANDS, "Score <{0:0.00} Diepte {1:0} {2}", "Score <{0:0.00} Depth {1:0} {2}"), pScore / 100, pDepth, pMoves))
                Case ScoreType.lowerbound
                    UpdateVariant(pIndex - 1, String.Format(If(CurrentLanguage = NEDERLANDS, "Score >{0:0.00} Diepte {1:0} {2}", "Score >{0:0.00} Depth {1:0} {2}"), pScore / 100, pDepth, pMoves))
            End Select
        End If
    End Sub

    Private Sub Engine_ErrorMessage(pMessage As String) Handles gEngine.ErrorMessage
        UpdateVariant(1, pMessage)
    End Sub

    Private Sub frmStockfish_FormClosing(pSender As Object, pArgs As FormClosingEventArgs) Handles Me.FormClosing
        gEngine = Nothing
    End Sub

    Private Sub gfrmMainForm_BoardShown(pFEN As String) Handles gfrmMainForm.BoardShown
        gFEN = pFEN 'To Save for when needed at Chk-On
        If chkOnOff.Checked = True Then
            ClearVariants()
            gEngine.Best3Variants(pFEN)
        End If
    End Sub

    Private Sub ClearVariants()
        lstVariants.Items(0) = " "
        lstVariants.Items(1) = " "
        lstVariants.Items(2) = " "
        lstVariants.Refresh()
    End Sub

    Private Sub UpdateVariant(pIndex As Integer, pText As String)
        'Event from Engine triggered this; and isn't allowed to change the form...
        Invoke(Sub()
                   lstVariants.Items.Item(pIndex) = pText
               End Sub)
    End Sub

    Private Sub lstVariants_SelectedIndexChanged(pSender As Object, pArgs As EventArgs) Handles lstVariants.SelectedIndexChanged
        If lstVariants.SelectedIndex <> -1 Then
            lstVariants.SelectedIndex = -1
        End If
    End Sub

    Protected Overrides Sub Finalize()
        gfrmMainForm = Nothing
        gEngine = Nothing

        MyBase.Finalize()
    End Sub

End Class