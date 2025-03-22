Option Explicit On

Imports ChessGlobals.ChessColor
Imports ChessCoach.StudentLevelEnum
Imports ChessMaterials
Imports PGNLibrary
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports System.Net.Mime.MediaTypeNames
Imports ChessEngine

Public Module modFeedback
    Public Enum StudentLevelEnum
        <XmlEnum()>
        Step1 = 1
        <XmlEnum()>
        Step2 = 2
        <XmlEnum()>
        Step3 = 3
    End Enum

    'Complimenten geven is te moeilijk.
    'De evaluatie van de Engine is niet ineens 3.0,
    'als het de beste zet is, is die in de stelling vooraf ook al 3.0
    'en wordt de zet op 0.0 geevalueerd.

    Public StudentLevel As StudentLevelEnum = Step2

    'Related to StockFish
    Public WithEvents Engine As ChessEngine.Engine
    Private DataReceived As Boolean = True
    Private Score As Integer = 0
    Private BestMove As String = ""
    Private Mate As String = ""
    Private MoveList As String = ""

    Public Sub AnaLyze(pPGNGame As PGNGame)
        Dim BoardBefore As New ChessBoard(), BoardAfter As ChessBoard, Move As BoardMove
        Dim Before As EngineResult, After As EngineResult

        Engine = New ChessEngine.Engine()
        Engine.StartEngine()

        BoardAfter = New ChessBoard(pPGNGame.FEN())
        After = GetEngineResult(BoardAfter.FEN)
        For Each HalfMove As PGNHalfMove In pPGNGame.HalfMoves
            If HalfMove.VariantLevel = 0 _
            And HalfMove.Result = "" Then 'Alleen gespeelde zetten
                'Shift previous After to current Before
                BoardBefore.FEN = BoardAfter.FEN
                Before = After

                Move = HalfMove.BoardMove(BoardAfter)
                BoardAfter.PerformMove(Move)
                After = GetEngineResult(BoardAfter.FEN)

                Dim Result As New ChessEngine.EngineResults(Before, After)

                'Print Move
                Debug.Print(Fixed(HalfMove.MoveNr, 3) &
                                If(HalfMove.Color = WHITE, "     ", " ... ") &
                                Fixed(HalfMove.MoveText(), 10) & "  " &
                                Fixed(Microsoft.VisualBasic.Strings.Format(Result.Score, "000"), 10) & "  " &
                                Fixed(Before.BestMove.ToString, 10))

                Debug.Print(Space(5) & Feedback(BoardBefore, Move, BoardAfter, Result))

            End If
        Next HalfMove

        Engine.StopEngine()
        Engine = Nothing
    End Sub

    Private Function Fixed(pString As String, ByVal pLen As Integer) As String
        'Needed to Debug.print movelist a bit properly
        Return pString & Space(pLen - pString.Length)
    End Function

    Private Function GetEngineResult(pFENBefore As String) As EngineResult
        Score = -9999
        BestMove = ""
        MoveList = ""
        DataReceived = False
        Engine.EvaluateFEN(pFENBefore)
        While DataReceived = False
            System.Windows.Forms.Application.DoEvents()
        End While
        Return New EngineResult(Score, New EngineMove(BestMove), Mate, MoveList)
    End Function

    Private Sub Engine_Message(pMessage As String) Handles Engine.Message
        Dim P As Integer, Q As Integer
        Debug.Print("Eng Msg: " & pMessage)
        If pMessage Is Nothing Then
            Exit Sub

        ElseIf pMessage.StartsWith("info string") Then
            Exit Sub

        ElseIf pMessage.StartsWith("info") Then

            P = InStr(pMessage, " cp ")
            Q = InStr(P + 4, pMessage, " ")
            If P > 0 And Q > P Then
                Score = Val(Mid(pMessage, P + 4, Q - P - 4))
            End If

            P = InStr(pMessage, " mate ")
            Q = InStr(P + 6, pMessage, " ")
            If P > 0 And Q > P Then
                Mate = Mid(pMessage, P + 6, Q - P - 6)
                If Val(Mate) < 0 Then
                    Score = -5000
                Else
                    Score = +5000
                End If
            End If

            P = InStr(pMessage, " pv ")
            If P > 0 Then
                MoveList = Mid(pMessage, P + 4)
            End If


        ElseIf pMessage.StartsWith("bestmove") Then
            BestMove = Mid(pMessage, 10, 4)
            DataReceived = True

        ElseIf pMessage.StartsWith("Error") Then
            Debug.Print(pMessage)

        End If
    End Sub


    Public Function Feedback(PBoardBefore As ChessBoard, pMove As BoardMove, pBoardAfter As ChessBoard, pResult As ChessEngine.EngineResults) As String
        Dim Message As String = ""
        Select Case StudentLevel
            Case Step1
                Message = Step1Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

            Case Step2
                Message = Step1Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message
                Message = Step2Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

            Case Step3
                Message = Step1Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message
                Message = Step2Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message
                Message = Step3Errors(PBoardBefore, pMove, pBoardAfter, pResult)
                If Message <> "" Then Return Message

        End Select
        Return ""
    End Function

End Module
