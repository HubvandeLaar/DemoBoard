Option Explicit On

Imports System.IO

Public Class Engine

    Private WithEvents Process As Process
    Private strmWriter As StreamWriter

    Private WaitingForUciOk As Boolean = False
    Private WaitingForReadyOK As Boolean = False
    Private WaitingFEN As String = ""

    Public Event Message(pMessage As String)

    Public Sub New()
        CreateProcess()
    End Sub

    Public Sub CreateProcess()
        Dim EngineFile As New FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "stockfish_12_32bit.exe"))
        If EngineFile.Exists Then
            Process = New Process
            Process.StartInfo.FileName = EngineFile.FullName
            Process.StartInfo.UseShellExecute = False
            Process.StartInfo.RedirectStandardInput = True
            Process.StartInfo.RedirectStandardOutput = True
            Process.StartInfo.RedirectStandardError = True
            Process.StartInfo.CreateNoWindow = True

            Process.Start()

            Process.BeginOutputReadLine()
            Process.BeginErrorReadLine()
            strmWriter = Process.StandardInput
        Else
            Throw New FileNotFoundException("Engine file stockfish_12_32bit.exe not found")
        End If
    End Sub

    Public Sub Start(pFEN As String) 'When Switching On
        If Process Is Nothing _
        OrElse Process.HasExited = True Then
            Call CreateProcess()
        End If
        WaitingFEN = pFEN
        WaitingForUciOk = True
        SendCommand("uci")
    End Sub

    Public Sub [Stop]()
        If Process IsNot Nothing _
        And Process.HasExited = False Then
            SendCommand("stop")
        End If
    End Sub

    Public Sub NewFEN(pFEN)
        SendCommand("stop")
        SendCommand("ucinewgame")
        WaitingFEN = pFEN
        WaitingForReadyOK = True
        SendCommand("isready")
    End Sub

    Public Sub Quit()
        If Process IsNot Nothing _
        And Process.HasExited = False Then
            SendCommand("quit")
        End If
    End Sub

    Private Sub SendCommand(pCommand As String)
        If strmWriter Is Nothing Then
            Return
        End If
        'Debug.Print(">" & pCommand)
        strmWriter.WriteLine(pCommand)
    End Sub

    Private Sub Process_OutputDataReceived(pSender As Object, pArgs As DataReceivedEventArgs) Handles Process.OutputDataReceived
        'Debug.Print("<" & pArgs.Data)
        If WaitingForUciOk Then
            If pArgs.Data = "uciok" Then
                WaitingForUciOk = False
                SendCommand("setoption Name MultiPV value 3")
                SendCommand("ucinewgame")
                SendCommand("isready")
                WaitingForReadyOK = True
                'WaitingFEN set wil be processed after readyok
                Exit Sub
            End If
        End If
        If WaitingForReadyOK = True Then
            If pArgs.Data = "readyok" Then
                WaitingForReadyOK = False
                If WaitingFEN <> "" Then
                    SendCommand("position fen " & WaitingFEN)
                    SendCommand("go movetime 10000")
                End If
            End If
            Exit Sub
        End If

        RaiseEvent Message(pArgs.Data)
    End Sub

    Private Sub Process_ErrorDataReceived(pSender As Object, pArgs As DataReceivedEventArgs) Handles Process.ErrorDataReceived
        RaiseEvent Message("Error: " & pArgs.Data)
    End Sub

    Protected Overrides Sub Finalize()
        Me.Process = Nothing
        Me.strmWriter = Nothing

        MyBase.Finalize()
    End Sub
End Class