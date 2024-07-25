Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization
Imports PGNLibrary
Imports System.Text
Imports System.Text.RegularExpressions

<XmlType()>
Public Class PGNGame

    <XmlElement()>
    Public Tags As PGNTags
    <XmlElement()>
    Public WithEvents HalfMoves As PGNHalfMoves
    <XmlAttribute()>
    Public Index As Long '0-Based index within PGNGames

    'Public Event HalfMovesChanged(pPGNHalfMove As PGNHalfMove, pBeforeImage As String, pAfterImage As String)

    <XmlIgnore>
    Public Property XPGNString() As String
        Set(pXPGNString As String)
            Dim TagKey As String, TagValue As String, BeginMoveList As Long = 0
            Dim Matches = Regex.Matches(pXPGNString, "\[([^\s]*)\s([""'])(.+?(?=\2\]))\2\]", RegexOptions.Multiline)
            For Each Match As Match In Matches
                If Match.Groups.Count > 2 Then
                    TagKey = Match.Groups(1).Value
                    TagValue = Match.Groups(3).Value
                    Me.Tags.Add(TagKey, TagValue)
                    BeginMoveList = Match.Groups(0).Index + Match.Groups(0).Length + 1
                End If
            Next
            If BeginMoveList < pXPGNString.Length Then
                Dim MoveList As String = Mid(pXPGNString, BeginMoveList)
                MoveList = Replace(MoveList, vbCrLf, " ") 'Remove all kind of linefeeds
                MoveList = Replace(MoveList, vbCr, " ")
                MoveList = Replace(MoveList, vbLf, " ")
                MoveList = MoveList.TrimStart()
                Me.HalfMoves.XPGNString = MoveList
            End If
        End Set
        Get
            Dim XPGNBuilder As New StringBuilder()
            Dim PGNRecord As String, P As Long
            For Each TAG As PGNTag In Me.Tags
                PGNRecord = TAG.PGNString()
                If PGNRecord <> "" Then
                    XPGNBuilder.Append(PGNRecord & vbCrLf)
                End If
            Next TAG
            XPGNBuilder.Append("" & vbCrLf)

            Dim MoveList As String = Me.HalfMoves.XPGNString
            While (MoveList.Length > 80)
                P = InStrRev(Left(MoveList, 80), " ")
                If P = 0 Then Throw New DataMisalignedException(MessageText("MissingSpace", Left(MoveList, 80)))
                PGNRecord = Left(MoveList, P - 1)
                XPGNBuilder.Append(PGNRecord & vbCrLf)
                MoveList = Mid(MoveList, P + 1)
            End While
            If MoveList <> "" Then
                XPGNBuilder.Append(MoveList & vbCrLf)
            End If
            XPGNBuilder.Append("" & vbCrLf)

            Return XPGNBuilder.ToString()
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.XPGNString = pPGNString 'XPGN accepts all PGN features
        End Set
        Get
            Dim PGNBuilder As New StringBuilder()
            Dim PGNRecord As String, P As Long
            For Each TAG As PGNTag In Me.Tags
                If (TAG.Key = "Title" _
                Or TAG.Key = "Memo" _
                Or TAG.Key = "FENGraphicals") Then
                    Continue For 'Only valid Tags in XPGN
                End If
                PGNRecord = TAG.PGNString()
                If PGNRecord <> "" Then
                    PGNBuilder.Append(PGNRecord & vbCrLf)
                End If
            Next TAG
            PGNBuilder.Append("" & vbCrLf)

            Dim MoveList As String = Me.HalfMoves.PGNString
            While (MoveList.Length > 80)
                P = InStrRev(Left(MoveList, 80), " ")
                If P = 0 Then Throw New DataMisalignedException(MessageText("MissingSpace", Left(MoveList, 80)))
                PGNRecord = Left(MoveList, P - 1)
                PGNBuilder.Append(PGNRecord & vbCrLf)
                MoveList = Mid(MoveList, P + 1)
            End While
            If MoveList <> "" Then
                PGNBuilder.Append(MoveList & vbCrLf)
            End If
            PGNBuilder.Append("" & vbCrLf)

            Return PGNBuilder.ToString()
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property FirstMoveNr() As Long
        Get
            Dim FEN As String, P As Long, Nr As Long
            FEN = Me.Tags.GetPGNTag("FEN")
            If FEN = "" Then Return 1
            P = InStrRev(FEN, " ")
            Nr = Val(Mid(FEN, P))
            If Nr = 0 Then Return 1
            Return Nr
        End Get
    End Property

    <XmlElement()> 'Included prperty only for serialization 
    Public Property FENComment As PGNComment
        Set(pFENComment As PGNComment)
            Me.HalfMoves.FENComment = pFENComment
        End Set
        Get
            Return Me.HalfMoves.FENComment
        End Get
    End Property

    Public ReadOnly Property ContainsTrainingQuestions As Boolean
        Get
            Dim HalfMove As PGNHalfMove
            For Each HalfMove In Me.HalfMoves
                If HalfMove.TrainingQuestion IsNot Nothing Then
                    Return True
                End If
            Next HalfMove
            Return False
        End Get
    End Property

    Function FEN(Optional pLastMove As PGNHalfMove = Nothing) As String
        Dim Max As Long, PreviousHalfMove() As PGNHalfMove = Nothing  'In reversed order
        Dim FENstring As String
        Dim I As Long, Move As PGNHalfMove

        'Find last move before the Movelist position
        If pLastMove Is Nothing Then
            FENstring = Me.Tags.GetPGNTag("FEN")
            If FENstring = "" Then FENstring = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            Return FENstring
        End If

        'Collect the last move and Moves leading to this postion
        Max = 0
        Move = pLastMove
        While Move IsNot Nothing
            Max += 1
            ReDim Preserve PreviousHalfMove(Max)
            PreviousHalfMove(Max) = Move
            Move = HalfMoves.PreviousHalfMove(Move)
        End While

        'Perform all Moves on an initial board
        Dim TempBoard As New ChessBoard()
        FENstring = Tags.GetPGNTag("FEN")
        If FENstring <> "" Then
            TempBoard.FEN = FENstring  'Set-up position in game specified
        Else
            TempBoard.InitialPosition()  'Initial Position
        End If
        For I = Max To 1 Step -1
            If PreviousHalfMove(I).Color <> UNKNOWN Then
                TempBoard.PerformMove(PreviousHalfMove(I).BoardMove(TempBoard))
                TempBoard.ActiveColor = Opponent(PreviousHalfMove(I).Color)
            End If
        Next I

        'Return FEN
        Return TempBoard.FEN
    End Function

    <XmlIgnore>
    Public ReadOnly Property NextHalfMoveWithTrainingQuestion(Optional pHalfMove As PGNHalfMove = Nothing) As PGNHalfMove
        Get
            For Each HalfMove As PGNHalfMove In Me.HalfMoves
                If pHalfMove IsNot Nothing _
                AndAlso HalfMove.Index <= pHalfMove.Index Then
                    Continue For
                End If
                If HalfMove.TrainingQuestion IsNot Nothing Then
                    Return HalfMove
                End If
            Next HalfMove
            Return Nothing
        End Get
    End Property

    Public Sub Clear()
        Me.Tags.Clear()
        Me.FENComment = Nothing
        Me.Tags.Add("[FEN ""8/8/8/8/8/8/8/8 w - - 0 1""]")
        Me.HalfMoves.Clear()
    End Sub

    Public Sub Initial()
        Me.Tags.Clear()
        Me.FENComment = Nothing
        Me.Tags.Add("[FEN ""rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1""]")
        Me.HalfMoves.Clear()
    End Sub

    Public Sub New(pInitial As Boolean)
        Me.New()
        If pInitial = True Then
            Me.Initial()
        End If
    End Sub

    Public Sub New(pFENOrXPGN As String)
        Me.New()
        Me.Tags.Clear()
        If pFENOrXPGN Like "*[[]* [""]*[""][]]*" Then 'PGN or XPGN
            Me.XPGNString = pFENOrXPGN
        ElseIf pFENOrXPGN Like "*/*/*/*/*/*/*/* [bw] *" Then
            Me.Tags.Add("FEN", pFENOrXPGN)
        End If
    End Sub

    Public Sub New()
        Me.Tags = New PGNTags()
        Me.HalfMoves = New PGNHalfMoves()
    End Sub

    Protected Overrides Sub Finalize()
        Me.Tags = Nothing
        Me.HalfMoves = Nothing

        MyBase.Finalize()
    End Sub

End Class

