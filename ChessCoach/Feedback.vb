Option Explicit On

Imports ChessGlobals.ChessColor
Imports ChessCoach.Feedback.StudentLevelEnum
Imports ChessMaterials
Imports PGNLibrary
Imports System.Xml.Serialization
Imports ChessEngine

Public Class Feedback
    Public Enum StudentLevelEnum
        <XmlEnum()>
        Step1 = 1
        <XmlEnum()>
        Step2 = 2
        <XmlEnum()>
        Step3 = 3
    End Enum

    'Complimenten geven blijkt te moeilijk.
    'De evaluatie van de Engine is nl. niet ineens 3.0,
    'als het de beste zet is, is die in de stelling vooraf ook al 3.0
    'en wordt die zet op 0.0 geevalueerd.

    Private ReadOnly Step1Feedback As New Step1Feedback()
    Private ReadOnly Step2Feedback As New Step2Feedback()
    Private ReadOnly Step3Feedback As New Step3Feedback()

    Private ReadOnly gLevelWhite As StudentLevelEnum
    Private ReadOnly gLevelBlack As StudentLevelEnum
    Private gCurrentHalfMove As PGNHalfMove

    'Related to StockFish
    Private WithEvents gEngine As ChessEngine.Engine
    Private gDataReceived As Boolean = True
    Private gEngineResult As New EngineResult()

    Public Sub AnaLyze(pPGNGame As PGNGame)
        Dim BoardBefore As New ChessBoard(), BoardAfter As ChessBoard, Move As BoardMove
        Dim Before As EngineResult, After As EngineResult

        gEngine = New ChessEngine.Engine()
        gEngine.StartEngine()

        BoardAfter = New ChessBoard(pPGNGame.FEN())
        After = GetEngineResult(BoardAfter.FEN)
        For I As Integer = 0 To pPGNGame.HalfMoves.Count - 1
            gCurrentHalfMove = pPGNGame.HalfMoves(I)
            If gCurrentHalfMove.VariantLevel = 0 _
            And gCurrentHalfMove.Result = "" Then 'Alleen gespeelde zetten
                'Shift previous After to current Before
                BoardBefore.FEN = BoardAfter.FEN
                Before = After

                Move = gCurrentHalfMove.BoardMove(BoardAfter)
                BoardAfter.PerformMove(Move)
                After = GetEngineResult(BoardAfter.FEN)

                Dim Results As New ChessEngine.EngineResults(Before, After)

                'Print Move
                Debug.Print(Fixed(gCurrentHalfMove.MoveNr, 3) &
                                If(gCurrentHalfMove.Color = WHITE, "     ", " ... ") &
                                Fixed(gCurrentHalfMove.MoveText(), 10) & "  " &
                                Fixed(Microsoft.VisualBasic.Strings.Format(Results.Score, "000"), 10) & "  " &
                                Fixed(Before.BestMove.ToString, 10))

                Dim Message As String = GetFeedback(BoardBefore, Move, BoardAfter, Results)
                If Message <> "" Then Debug.Print(Space(5) & Message)

            End If
        Next I ' CurrentHalfMove 'NA INVOEGEN VAN SUBVARIANT IS DE POINTER NIET MEER GOED !!!!

        gEngine.StopEngine()
        gEngine = Nothing
    End Sub

    ''' <summary>Needed to Debug.print movelist a bit properly</summary>
    Private Function Fixed(pString As String, ByVal pLen As Integer) As String
        Return pString & Space(pLen - pString.Length)
    End Function

    ''' <summary>Returns the results from StockFish</summary>
    Private Function GetEngineResult(pFENBefore As String) As EngineResult
        gEngineResult = New EngineResult()
        gDataReceived = False
        gEngine.EvaluateFEN(pFENBefore)
        While gDataReceived = False
            Windows.Forms.Application.DoEvents()
        End While
        Return gEngineResult
    End Function

    Private Sub gEngine_InfoMessage(pDepth As Integer, pIndex As Integer, pScoreType As Engine.ScoreType, pScore As Integer, pMoves As String) Handles gEngine.InfoMessage
        If pIndex <= 0 OrElse pIndex > gEngineResult.EngineVariant.Length Then
            Exit Sub
        End If

        Select Case pScoreType
            Case Engine.ScoreType.cp
                gEngineResult.EngineVariant(pIndex - 1) = New EngineVariant(pScoreType, pScore, pDepth, pMoves)
            Case Engine.ScoreType.mate
                If pDepth < 0 Then
                    gEngineResult.EngineVariant(pIndex - 1) = New EngineVariant(pScoreType, -5000, pDepth, pMoves)
                Else
                    gEngineResult.EngineVariant(pIndex - 1) = New EngineVariant(pScoreType, +5000, pDepth, pMoves)
                End If
            Case Engine.ScoreType.upperbound, Engine.ScoreType.lowerbound
                'Not used in this module
        End Select
    End Sub

    Private Sub gEngine_BestMoveMessage(pBestMove As String, pMessage As String) Handles gEngine.BestMoveMessage
        'NB. BestMove is always the first move in EngineResult.EngineVariant(0).MoveList
        gDataReceived = True
    End Sub

    Private Sub gEngine_ErrorMessage(pMessage As String) Handles gEngine.ErrorMessage
        Debug.Print(pMessage)
    End Sub

    ''' <summary>Returns a string containing the feedback on pMove</summary>
    Public Function GetFeedback(PBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As ChessEngine.EngineResults) As String
        Dim Message As String
        Select Case If(PBoardBefore.ActiveColor = WHITE, gLevelWhite, gLevelBlack)
            Case Step1
                Message = Step1Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

            Case Step2
                Message = Step1Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

                Message = Step2Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

            Case Step3
                Message = Step1Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

                Message = Step2Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

                Message = Step3Feedback.FindErrors(gCurrentHalfMove, PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

        End Select
        Return ""
    End Function


    Public Sub New(pLevelWhite As StudentLevelEnum, pLevelBlack As StudentLevelEnum)
        gLevelWhite = pLevelWhite
        gLevelBlack = pLevelBlack
    End Sub

End Class
