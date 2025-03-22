Option Explicit On

'Requires .NET Framework 4.7.2
'Requires NuGet of System.Reactive.Linq

Imports System.IO
Imports System.Reactive.Linq

Public Class Engine
    Private strmReader As StreamReader
    Private strmWriter As StreamWriter
    Private WithEvents EngineProcess As Process
    Private EngineListener As IDisposable

    Public Event Message(pMessage As String)

    Public Sub StartEngine()
        Dim EngineFile As New FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "stockfish_12_32bit.exe"))
        If EngineFile.Exists _
        AndAlso EngineFile.Extension = ".exe" Then
            EngineProcess = New Process
            EngineProcess.StartInfo.FileName = EngineFile.FullName
            EngineProcess.StartInfo.UseShellExecute = False
            EngineProcess.StartInfo.RedirectStandardInput = True
            EngineProcess.StartInfo.RedirectStandardOutput = True
            EngineProcess.StartInfo.RedirectStandardError = True
            EngineProcess.StartInfo.CreateNoWindow = True
            EngineProcess.Start()
            strmWriter = EngineProcess.StandardInput
            strmReader = EngineProcess.StandardOutput

            EngineListener = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1)).Subscribe(Sub() ReadEngineMessages())

            SendCommand("uci")
            SendCommand("isready")
        Else
            Throw New FileNotFoundException("Engine file stockfish_12_32bit.exe not found")
        End If
    End Sub

    Public Sub StopEngine()
        If EngineProcess IsNot Nothing _
        AndAlso EngineProcess.HasExited = False Then
            SendCommand("stop")
            System.Windows.Forms.Application.DoEvents()
            EngineListener.Dispose()
            Try
                strmReader.Close()
                strmWriter.Close()
                System.Windows.Forms.Application.DoEvents()
                EngineProcess.Kill()
            Catch
            Finally
                System.Windows.Forms.Application.DoEvents()
                EngineProcess.Dispose()
            End Try
        End If
    End Sub

    Public Sub EvaluateFEN(pFEN As String)
        SendCommand("setoption name MultiPV value 1")
        SendCommand("setoption name Use NNUE value false")
        SendCommand("ucinewgame")
        SendCommand("position fen " & pFEN)
        SendCommand("go depth 12")
    End Sub

    Public Sub Best3Variants(pFEN)
        SendCommand("setoption name MultiPV value 3")
        SendCommand("setoption name Use NNUE value false")
        SendCommand("ucinewgame")
        SendCommand("position fen " & pFEN)
        SendCommand("go movetime 2000") '2 seconds or infinite") 
    End Sub

    Public Function CheckMate(pMessage) As Boolean
        If pMessage.Contains("ponder") Then
            CheckMate = False
        Else
            CheckMate = True
        End If
    End Function

    Private Sub SendCommand(pCommand As String)
        If strmWriter IsNot Nothing Then
            'Debug.Print(">" & pCommand)
            strmWriter.WriteLine(pCommand)
        End If
    End Sub

    Private Sub ReadEngineMessages()
        Dim Message = strmReader.ReadLine()
        If Message <> String.Empty Then
            'Debug.Print("<" & Message)
            RaiseEvent Message(Message)
        End If
    End Sub

    Private Sub Process_ErrorDataReceived(pSender As Object, pArgs As DataReceivedEventArgs) Handles EngineProcess.ErrorDataReceived
        RaiseEvent Message("Error: " & pArgs.Data)
    End Sub

    Protected Overrides Sub Finalize()
        Me.EngineProcess = Nothing
        Me.strmReader = Nothing
        Me.strmWriter = Nothing
        Me.EngineListener = Nothing

        MyBase.Finalize()
    End Sub
End Class