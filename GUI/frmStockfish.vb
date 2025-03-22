Imports System.Text.RegularExpressions
Imports ChessGlobals.modChessLanguage
Imports ChessMaterials
Imports PGNLibrary

Public Class frmStockfish
    Private WithEvents gfrmMainForm As frmMainForm
    Private WithEvents gfrmBoard As frmBoard
    Private WithEvents Engine As ChessEngine.Engine

    Public Sub New(pfrmMainForm As frmMainForm)
        InitializeComponent()

        gfrmMainForm = pfrmMainForm
        gfrmBoard = pfrmMainForm.gfrmBoard
        Engine = New ChessEngine.Engine()
    End Sub

    Private Sub chkOnOff_CheckedChanged(pSender As Object, pArgs As EventArgs) Handles chkOnOff.CheckedChanged
        If chkOnOff.Checked = True Then
            Engine.StartEngine()
            ClearVariants()
            Engine.Best3Variants(gfrmBoard.FEN)
        Else
            Engine.StopEngine()
        End If
    End Sub

    Private Sub Engine_EngineMessage(pMessage As String) Handles Engine.Message
        'Debug.Print("Eng Msg: " & pMessage)
        If pMessage Is Nothing Then
            Exit Sub

        ElseIf pMessage.StartsWith("info") Then
            Dim Match As Match = Regex.Match(pMessage, "depth (\d+) .*?multipv (\d+) .*?score (cp|mate|lowerbound|upperbound) [-]?(\d+) .*?pv (.*?)$")
            Dim Depth As Integer = Val(Match.Groups(1).Value) 'Depth
            Dim Index As Integer = Val(Match.Groups(2).Value) 'Index
            Dim ScoreType As String = Match.Groups(3).Value   'Score cp, mate upperbound, lowerbound 
            Dim Score As Integer = Val(Match.Groups(4).Value) 'Score in CentiPoints
            Dim Moves As String = Match.Groups(5).Value       'Pv
            If Index > 0 Then
                Select Case ScoreType
                    Case "cp"
                        UpdateVariant(Index - 1, String.Format(If(CurrentLanguage = ChessLanguage.NEDERLANDS, "Score {0:0.00} Diepte {1:0} {2}", "Score {0:0.00} Depth {1:0} {2}"), Score / 100, Depth, Moves))
                    Case "mate"
                        UpdateVariant(Index - 1, String.Format(If(CurrentLanguage = ChessLanguage.NEDERLANDS, "Mat in {0} Diepte {1:0} {2}", "Checkmate in {0} Depth {1:0} {2}"), Score, Depth, Moves))
                    Case "upperbound"
                        UpdateVariant(Index - 1, String.Format(If(CurrentLanguage = ChessLanguage.NEDERLANDS, "Score <{0:0.00} Diepte {1:0} {2}", "Score <{0:0.00} Depth {1:0} {2}"), Score / 100, Depth, Moves))
                    Case "lowerbound"
                        UpdateVariant(Index - 1, String.Format(If(CurrentLanguage = ChessLanguage.NEDERLANDS, "Score >{0:0.00} Diepte {1:0} {2}", "Score >{0:0.00} Depth {1:0} {2}"), Score / 100, Depth, Moves))
                End Select
            End If

        ElseIf pMessage.StartsWith("bestmove") Then
            Exit Sub

        ElseIf pMessage.StartsWith("Error") Then
            UpdateVariant(1, pMessage)
        End If
    End Sub

    Private Sub frmStockfish_FormClosing(pSender As Object, pArgs As FormClosingEventArgs) Handles Me.FormClosing
        Engine = Nothing
    End Sub

    'Private Sub gfrmMainForm_FENChanged(pFEN As String) Handles gfrmMainForm.FENChanged
    '    Debug.Print("FEN Main !!!!!!! " & pFEN)
    '    If chkOnOff.Checked = True Then
    '        Engine.Stop()
    '        Engine.Start()
    '        Engine.SetPosition(pFEN)
    '        ClearVariants()
    '    End If
    'End Sub

    Private Sub gfrmMainForm_GameChanged(pPGNGame As PGNGame) Handles gfrmMainForm.GameChanged
        'Debug.Print("Game Changed: " & pPGNGame.FEN)
        If chkOnOff.Checked = True Then
            ClearVariants()
            Engine.Best3Variants(pPGNGame.FEN)
        End If
    End Sub

    Private Sub gfrmBoard_FENChanged(pFEN As String) Handles gfrmBoard.FENChanged
        'Debug.Print("FEN Changed: " & pFEN)
        If chkOnOff.Checked = True Then
            ClearVariants()
            Engine.Best3Variants(pFEN)
        End If
    End Sub

    Private Sub gfrmBoard_ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String, pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pFEN As String, pFENBeforeDragging As String) Handles gfrmBoard.ChessPieceMoved
        'Debug.Print("Piece Moved: " & pFEN)
        If chkOnOff.Checked = True Then
            ClearVariants()
            Engine.Best3Variants(pFEN)
        End If
    End Sub

    Private Sub gfrmBoard_MovePlayed(pHalfMove As PGNHalfMove) Handles gfrmBoard.MovePlayed
        'Debug.Print("Move Played: " & gfrmBoard.FEN)
        If chkOnOff.Checked = True Then
            ClearVariants()
            Engine.Best3Variants(gfrmBoard.FEN)
        End If
    End Sub

    Private Sub gfrmBoard_BoardShown(pFen As String) Handles gfrmBoard.BoardShown
        'Debug.Print("BoardShown: " & pFen)
        If chkOnOff.Checked = True Then
            ClearVariants()
            Engine.Best3Variants(pFen)
        End If
    End Sub

    Private Sub ClearVariants()
        'Debug.Print("ClearVariants")
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
        Me.gfrmMainForm = Nothing
        Me.gfrmBoard = Nothing
        Me.Engine = Nothing

        MyBase.Finalize()
    End Sub

End Class