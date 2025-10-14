Option Explicit On

Imports ChessMessaging.Messages
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNFile

    <XmlElement()>
    Public Property PGNGames As PGNGames
    <XmlAttribute()>
    Public Property FullFileName As String
    <XmlAttribute()>
    Public Property ContainsUniCodes As Boolean = False

    Public ReadOnly Property Extension As String
        Get
            Dim Pos As Integer = InStrRev(Me.FullFileName, ".")
            If Pos = 0 Then Return ""
            Return LCase(Mid(Me.FullFileName, Pos))
        End Get
    End Property

    Public ReadOnly Property FileName As String
        Get
            Dim P1 As Long = InStrRev(Me.FullFileName, "/")
            Dim P2 As Long = InStrRev(Me.FullFileName, "\")
            Return Mid(Me.FullFileName, Math.Max(P1, P2) + 1)
        End Get
    End Property

    ''' <summary>Returns True when the FileName is a PGN of XPGN file</summary>
    Public Shared Function IsPGNFile(pFileName As String) As Boolean
        If LCase(pFileName) Like "*.pgn" _
        Or LCase(pFileName) Like "*.xpgn" Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub New(pFileName As String)
        Dim FN As Long, PGNRecord As String = "      ", TagExtend As String
        Dim CurrentGame As PGNGame

        Me.PGNGames = New PGNGames()
        Me.FullFileName = pFileName

        FN = FreeFile()
        FileOpen(FN, pFileName, OpenMode.Input)
        If Not EOF(FN) Then PGNRecord = LineInput(FN) 'Read First Record
        If Mid$(PGNRecord, 4, 1) = "[" Then 'Crap in first three positions When UniCodes in Text-file
            PGNRecord = Mid(PGNRecord, 4)
            Me.ContainsUniCodes = True
        End If
        While (Not EOF(FN))
            CurrentGame = Me.PGNGames.Add()
            While (PGNRecord Like "[[][!%]*")
                While (Right(PGNRecord, 1) <> "]" And Not EOF(FN))
                    TagExtend = LineInput(FN)
                    PGNRecord = PGNRecord & vbCrLf & TagExtend
                End While
                CurrentGame.Tags.Add(PGNRecord)
                If EOF(FN) Then Exit While
                PGNRecord = LineInput(FN)
            End While

            Dim PGNMoveList As String = ""
            While (Not EOF(FN))
                PGNRecord = LineInput(FN)
                If PGNRecord = "" Then Exit While 'Empty Line
                PGNMoveList = PGNMoveList & " " & PGNRecord
            End While
            CurrentGame.HalfMoves.PGNString = PGNMoveList

            Dim FENGraphicals As String = CurrentGame.Tags("FENGraphicals").Value
            If FENGraphicals <> "" Then
                CurrentGame.HalfMoves.FENComment = New PGNComment(FENGraphicals)
            End If

            If PGNRecord = "" And Not EOF(FN) Then PGNRecord = LineInput(FN)
        End While
        FileClose(FN)
    End Sub

    Public Sub SaveAs(pFullFileName As String)
        Dim FN As Long, PGNRecord As String
        Dim PGNMoveList As String, P As Long
        Me.FullFileName = pFullFileName
        FN = FreeFile()
        FileOpen(FN, pFullFileName, OpenMode.Output)
        For Each PGNGame As PGNGame In Me.PGNGames

            For Each TAG As PGNTag In PGNGame.Tags
                If Me.Extension = ".pgn" _
                And (TAG.Key = "Title" _
                    Or TAG.Key = "Memo" _
                    Or TAG.Key = "FENGraphicals") Then
                    Continue For
                End If
                PGNRecord = TAG.PGNString()
                If PGNRecord <> "" Then
                    PrintLine(FN, PGNRecord)
                End If
            Next TAG
            PrintLine(FN, "")

            If Me.Extension = ".pgn" Then
                PGNMoveList = PGNGame.HalfMoves.PGNString
            Else
                PGNMoveList = PGNGame.HalfMoves.XPGNString
            End If

            While (Len(PGNMoveList) > 80)
                P = InStrRev(Left(PGNMoveList, 80), " ")
                If P = 0 Then Throw New DataMisalignedException(MessageText("MissingSpace", Left(PGNMoveList, 80)))
                PGNRecord = Left(PGNMoveList, P - 1)
                PrintLine(FN, PGNRecord)
                PGNMoveList = Mid(PGNMoveList, P + 1)
            End While
            If PGNMoveList <> "" Then
                PrintLine(FN, PGNMoveList)
            End If
            PrintLine(FN, "")
        Next PGNGame
        FileClose(FN)
    End Sub

    Public Sub New()
        Me.PGNGames = New PGNGames()
    End Sub

    Protected Overrides Sub Finalize()
        Me.PGNGames = Nothing

        MyBase.Finalize()
    End Sub
End Class
