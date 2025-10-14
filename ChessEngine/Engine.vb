Option Explicit On

'Requires .NET Framework 4.7.2
'Requires NuGet of System.Reactive.Linq

Imports System.IO
Imports System.Reactive.Linq
Imports System.Text.RegularExpressions
Imports System.Windows 'for .Forms

Public Class Engine

    Public Enum ScoreType
        cp         'Centipoints
        mate       'Checkmate
        upperbound 'Score is upperbound
        lowerbound 'Score is lowerbound
    End Enum

    Public Event BestMoveMessage(pBestMove As String, pMessage As String)
    Public Event InfoMessage(pDepth As Integer, pIndex As Integer, pScoreType As ScoreType, pScore As Integer, pMoves As String)
    Public Event InfoStringMessage(pMessage As String)
    Public Event ErrorMessage(pMessage As String)

    Private gstrmReader As StreamReader
    Private gstrmWriter As StreamWriter
    Private WithEvents gEngineProcess As Process
    Private gEngineListener As IDisposable

    Public Sub StartEngine()
        Dim EngineFile As New FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "stockfish_12_32bit.exe"))
        If EngineFile.Exists _
        AndAlso EngineFile.Extension = ".exe" Then
            gEngineProcess = New Process
            gEngineProcess.StartInfo.FileName = EngineFile.FullName
            gEngineProcess.StartInfo.UseShellExecute = False
            gEngineProcess.StartInfo.RedirectStandardInput = True
            gEngineProcess.StartInfo.RedirectStandardOutput = True
            gEngineProcess.StartInfo.RedirectStandardError = True
            gEngineProcess.StartInfo.CreateNoWindow = True
            gEngineProcess.Start()
            gstrmWriter = gEngineProcess.StandardInput
            gstrmReader = gEngineProcess.StandardOutput

            gEngineListener = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1)).Subscribe(Sub() ReadEngineMessages())

            SendCommand("uci")
            SendCommand("isready")
        Else
            Throw New FileNotFoundException("Engine file stockfish_12_32bit.exe not found")
        End If
    End Sub

    Public Sub StopEngine()
        If gEngineProcess IsNot Nothing _
        AndAlso gEngineProcess.HasExited = False Then
            SendCommand("stop")
            Forms.Application.DoEvents()
            gEngineListener.Dispose()
            Try
                gstrmReader.Close()
                gstrmWriter.Close()
                Forms.Application.DoEvents()
                gEngineProcess.Kill()
            Catch
            Finally
                Forms.Application.DoEvents()
                gEngineProcess.Dispose()
            End Try
        End If
    End Sub

    Public Sub EvaluateFEN(pFEN As String)
        SendCommand("setoption name MultiPV value 3") '3 variants
        SendCommand("setoption name Use NNUE value false")
        SendCommand("ucinewgame")
        SendCommand("position fen " & pFEN)
        SendCommand("go depth 12")
    End Sub

    Public Sub Best3Variants(pFEN)
        SendCommand("setoption name MultiPV value 3") '3 variants
        SendCommand("setoption name Use NNUE value false")
        SendCommand("ucinewgame")
        SendCommand("position fen " & pFEN)
        SendCommand("go movetime 2000") '2 seconds or infinite") 
    End Sub

    ''' <summary>Returns True if StockFish found Mate in #x</summary>
    Public Function CheckMate(pMessage) As Boolean
        If pMessage.Contains("ponder") Then
            CheckMate = False
        Else
            CheckMate = True
        End If
    End Function

    Private Sub SendCommand(pCommand As String)
        If gstrmWriter IsNot Nothing Then
            'Debug.Print(">" & pCommand)
            gstrmWriter.WriteLine(pCommand)
        End If
    End Sub

    Private Sub ReadEngineMessages()
        Dim Message = gstrmReader.ReadLine()
        If Message IsNot Nothing _
        AndAlso Message <> String.Empty Then
            'Debug.Print("<" & Message)

            If Message.StartsWith("info string") Then
                RaiseEvent InfoStringMessage(Message)

            ElseIf Message.StartsWith("info") Then
                Dim Match As Match = Regex.Match(Message, "depth (\d+) .*?multipv (\d+) .*?score (cp|mate|lowerbound|upperbound) (-?\d+) .*?pv (.*?)$")
                If Match.Success = False Then Exit Sub
                Dim ScoreType As ScoreType = [Enum].Parse(ScoreType.GetType(), Match.Groups(3).Value)
                RaiseEvent InfoMessage(pDepth:=Val(Match.Groups(1).Value),
                                       pIndex:=Val(Match.Groups(2).Value),
                                       pScoreType:=ScoreType,
                                       pScore:=Val(Match.Groups(4).Value),
                                       pMoves:=Match.Groups(5).Value)
                Exit Sub

            ElseIf Message.StartsWith("bestmove") Then
                RaiseEvent BestMoveMessage(Message.Split(" ")(1), Message)
                Exit Sub

            ElseIf Message.StartsWith("Error") Then
                RaiseEvent ErrorMessage(Message)
                Exit Sub

            End If

        End If
    End Sub

    Private Sub gEngineProcess_ErrorDataReceived(pSender As Object, pArgs As DataReceivedEventArgs) Handles gEngineProcess.ErrorDataReceived
        RaiseEvent ErrorMessage("Error: " & pArgs.Data)
    End Sub

    Protected Overrides Sub Finalize()
        gEngineProcess = Nothing
        gstrmReader = Nothing
        gstrmWriter = Nothing
        gEngineListener = Nothing

        MyBase.Finalize()
    End Sub
End Class