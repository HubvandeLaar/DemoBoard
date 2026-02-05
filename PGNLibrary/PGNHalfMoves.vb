Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessMessaging
Imports ChessMessaging.Messages
Imports PGNLibrary.PGNTrainingQuestion
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNHalfMoves
    Inherits List(Of PGNHalfMove)

    Private HalfMoveIndex As String = ""

    'Public Event Changed(pPGNHalfMove As PGNHalfMove)
    'Public Event CurrentHalfMoveChanged(pCurrentHalfMove As PGNHalfMove)

    <XmlIgnore>
    Public Property FENComment As PGNComment

    <XmlIgnore>
    Public ReadOnly Property CurrentHalfMove As PGNHalfMove
        Get
            If HalfMoveIndex = "" _
            Or Val(HalfMoveIndex) < 0 _
            Or Val(HalfMoveIndex) > Me.Count - 1 Then
                Return Nothing
            Else
                Return Me(Val(HalfMoveIndex))
            End If
        End Get
    End Property

    '<XmlAttribute()>
    Public Property CurrentHalfMoveIndex As String
        Set(pCurrentHalfMoveIndex As String)
            Me.HalfMoveIndex = pCurrentHalfMoveIndex
            'RaiseEvent CurrentHalfMoveChanged(CurrentHalfMove)
        End Set
        Get
            Return Me.HalfMoveIndex
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.XPGNString = pPGNString
        End Set
        Get
            Try
                'SAN (Standard Algebraic Notation)
                Dim VariantLevel As Long = 0, VariantNumber As Long = 1
                Dim AfterCommentOrVariantChange As Boolean = False
                Dim PGNMovelist As String = ""

                If FENComment IsNot Nothing Then
                    If Me.Count > 0 _
                    AndAlso Me.FirstHalfMove.CommentBefore IsNot Nothing Then
                        'Combine two comments to avoid two subsequent comments
                        PGNMovelist = (Me.FENComment & Me.FirstHalfMove.CommentBefore).PGNString
                    Else
                        PGNMovelist = Me.FENComment.PGNString
                    End If
                End If

                For Each HalfMove As PGNHalfMove In Me

                    'Changes in Level and Variant
                    If VariantLevel = HalfMove.VariantLevel _
                    And VariantNumber <> HalfMove.VariantNumber Then
                        PGNMovelist += ") ("
                        AfterCommentOrVariantChange = True
                    End If
                    While VariantLevel < HalfMove.VariantLevel
                        PGNMovelist += " ("
                        VariantLevel += 1
                        AfterCommentOrVariantChange = True
                    End While
                    While VariantLevel > HalfMove.VariantLevel
                        PGNMovelist += ")"
                        VariantLevel -= 1
                        AfterCommentOrVariantChange = True
                    End While
                    VariantLevel = HalfMove.VariantLevel
                    VariantNumber = HalfMove.VariantNumber

                    'Print Commentbefore
                    If HalfMove.CommentBefore IsNot Nothing _
                    AndAlso HalfMove.CommentBefore.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.CommentBefore.PGNString
                        AfterCommentOrVariantChange = True
                    End If

                    'Print Move
                    If PGNMovelist <> "" And Right(PGNMovelist, 1) <> "(" Then
                        PGNMovelist += " "
                    End If
                    PGNMovelist += HalfMove.MoveNrString(AfterCommentOrVariantChange)
                    PGNMovelist += HalfMove.MoveText
                    If HalfMove.NAGs.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.NAGs.PGNString
                    End If
                    AfterCommentOrVariantChange = False

                    'Print Commentafter
                    If HalfMove.CommentAfter IsNot Nothing _
                    AndAlso HalfMove.CommentAfter.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.CommentAfter.PGNString
                        AfterCommentOrVariantChange = True
                    End If

                Next HalfMove

                'Closing brackets at the end
                While VariantLevel > 0
                    PGNMovelist += ")"
                    VariantLevel -= 1
                End While

                Return PGNMovelist

            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
                Return Nothing
            End Try
        End Get
    End Property

    <XmlIgnore>
    Public Property XPGNString() As String
        Set(pXPGNString As String)  'Also works when PGNString is offered as input
            Try
                'SAN (Standard Algebraic Notation) With extensions
                Dim P As Long, Q As Long
                Dim CurrentHalfMove As PGNHalfMove, MoveNr(0 To 25) As String
                Dim VariantLevel As Long, VariantNumber() As Long

                Dim SavedComment As String, NewComment As String
                Dim MoveText As String
                Dim NAG As String
                Dim Color As ChessColor

                CurrentHalfMove = Nothing
                Me.Clear()
                Me.FENComment = Nothing
                VariantLevel = 0 : ReDim VariantNumber(0) : VariantNumber(0) = 0
                SavedComment = "" : MoveNr(VariantLevel) = "" : MoveText = "" : NAG = "" : Color = ChessColor.UNKNOWN
                P = 1
                While P <= Len(pXPGNString)

                    If Mid$(pXPGNString, P, 1) = " " Then 'Sometimes there is a space behind '}'and sometimes it's a ')'
                        P += 1 'Skip spaces

                    ElseIf Mid$(pXPGNString, P, 1) = PGNComment.PGNHeader Then 'Begin of Comment
                        Q = InStr(P + 1, pXPGNString, PGNComment.PGNTrailer, vbBinaryCompare)
                        NewComment = Mid(pXPGNString, P, Q - P + 1) 'Before this statement Comment Commonly contains ""

                        If PGNMarkerList.ContainsMarkerList(NewComment) _
                        OrElse PGNArrowList.ContainsArrowList(NewComment) _
                        OrElse PGNTextList.ContainsTextList(NewComment) Then
                            ' SavedComment (when not empty) and NewComment belong to CommentAfter of previous move
                            If SavedComment <> "" Then
                                NewComment = Mid(SavedComment, Len(SavedComment) - Len(PGNComment.PGNTrailer)) & " " _
                                         & Mid(NewComment, Len(PGNComment.PGNHeader) + 1)
                            End If
                            If CurrentHalfMove Is Nothing Then
                                Me.FENComment = New PGNComment(NewComment)
                            Else
                                CurrentHalfMove.CommentAfter = New PGNComment(NewComment)
                            End If
                            SavedComment = "" : NewComment = ""
                        ElseIf SavedComment <> "" Then 'Savedcomment belongs probably to previous Halfmove
                            If ContainsTrainingQuestion(SavedComment) Then
                                'Somtimes comments are split although belonging to same move
                                'TrainigQuestion belongs in CommentBefore of next move
                                NewComment = SavedComment & " " & NewComment
                            Else
                                'Savedcomment belongs to previous move
                                If CurrentHalfMove Is Nothing Then
                                    Me.FENComment = New PGNComment(SavedComment)
                                Else
                                    CurrentHalfMove.CommentAfter = New PGNComment(SavedComment)
                                End If
                            End If
                        End If
                        SavedComment = NewComment
                        P = Q + 1

                    ElseIf Mid$(pXPGNString, P, 1) = "(" Then 'Start of New Variant
                        If SavedComment <> "" Then 'Preceding comment belongs to previous Halfmove
                            If CurrentHalfMove Is Nothing Then Throw New DataMisalignedException(MessageText("ReallyWrong"))
                            If CurrentHalfMove.CommentAfter IsNot Nothing Then Throw New DataException(MessageText("CommentNotEmpty", CurrentHalfMove.CommentAfter.PGNString()))
                            CurrentHalfMove.CommentAfter = New PGNComment(SavedComment) : SavedComment = ""
                        End If
                        VariantLevel += 1
                        If UBound(VariantNumber) = VariantLevel Then                     'Continuing at the same level
                            VariantNumber(VariantLevel) = VariantNumber(VariantLevel) + 1 'Increment Variant Number
                        Else
                            If UBound(VariantNumber) < VariantLevel Then                  'Previous Variant Level was lower
                                ReDim Preserve VariantNumber(VariantLevel)
                                VariantNumber(VariantLevel) = 1
                            Else                                                          'Returning to lower Level
                                ReDim Preserve VariantNumber(VariantLevel)
                                VariantNumber(VariantLevel) = VariantNumber(VariantLevel) + 1
                            End If
                        End If
                        P += 1

                    ElseIf Mid$(pXPGNString, P, 1) = ")" Then 'End of variant
                        If SavedComment <> "" Then 'Preceding comment belongs to previous Halfmove
                            If CurrentHalfMove Is Nothing Then Throw New DataMisalignedException(MessageText("ReallyWrong"))
                            CurrentHalfMove.CommentAfter = New PGNComment(SavedComment) : SavedComment = ""
                        End If
                        VariantLevel -= 1
                        P += 1

                    ElseIf Mid$(pXPGNString, P, 2) Like "#." Then
                        MoveNr(VariantLevel) = Mid(pXPGNString, P, 2)
                        Color = WHITE
                        P += 2
                    ElseIf Mid$(pXPGNString, P, 3) Like "##." Then
                        MoveNr(VariantLevel) = Mid(pXPGNString, P, 3)
                        Color = WHITE
                        P += 3
                    ElseIf Mid$(pXPGNString, P, 4) Like "###." Then
                        MoveNr(VariantLevel) = Mid(pXPGNString, P, 4)
                        Color = WHITE
                        P += 4

                    ElseIf Mid$(pXPGNString, P, 2) = ".." Then
                        Color = BLACK
                        P += 2

                    ElseIf Mid$(pXPGNString, P, 1) = "$" Then 'NAG, always belongs to previous Halfmove
                        Q = FirstNonNumeric(P + 1, pXPGNString) 'NAGS are store one by one
                        CurrentHalfMove.NAGs.PGNString = Mid(pXPGNString, P, Q - P)
                        P = Q

                    ElseIf Mid$(pXPGNString, P, 3) = "1-0" _
                        Or Mid$(pXPGNString, P, 3) = "0-1" _
                        Or Mid$(pXPGNString, P, 3) = "½-½" _
                        Or Mid$(pXPGNString, P, 7) = "1/2-1/2" _
                        Or Mid$(pXPGNString, P, 1) = "*" Then
                        If SavedComment <> "" Then
                            If CurrentHalfMove IsNot Nothing Then 'Preceding comment belongs to previous Halfmove
                                If CurrentHalfMove.CommentAfter IsNot Nothing Then Throw New DataException(MessageText("CommentNotEmpty", CurrentHalfMove.CommentAfter.PGNString()))
                                CurrentHalfMove.CommentAfter = New PGNComment(SavedComment) : SavedComment = ""
                            End If
                        End If
                        Q = InStr(P + 1, pXPGNString, " ", vbBinaryCompare)
                        If Q = 0 Then Q = Len(pXPGNString) + 1
                        MoveText = Mid$(pXPGNString, P, Q - P)
                        'Store Result as move
                        CurrentHalfMove = Me.Add(SavedComment, "", MoveText, ChessColor.UNKNOWN,
                                                 VariantLevel, VariantNumber(VariantLevel))
                        CurrentHalfMove.Index = Me.Count
                        SavedComment = "" : MoveText = "" : Color = UNKNOWN
                        P = Q

                    Else  'The actual move
                        Q = FirstSpaceOrBracket(P + 1, pXPGNString)
                        MoveText = Mid(pXPGNString, P, Q - P)
                        'Store Move
                        CurrentHalfMove = Me.Add(SavedComment, MoveNr(VariantLevel), MoveText, Color,
                                                 VariantLevel, VariantNumber(VariantLevel))
                        CurrentHalfMove.Index = Me.Count
                        SavedComment = "" : MoveText = "" : Color = Color.Opponent
                        If UBound(VariantNumber) <> VariantLevel Then ReDim Preserve VariantNumber(VariantLevel) 'Causes reset of the lower levels when a move at higher level is found
                        P = Q

                    End If
                End While

                If FENComment IsNot Nothing _
                AndAlso FENComment.Text <> "" _
                AndAlso Me.Count > 0 Then
                    If Me.FirstHalfMove.CommentBefore Is Nothing Then
                        Me.FirstHalfMove.CommentBefore = New PGNComment("")
                    End If
                    If Me.FirstHalfMove.CommentBefore.Text = "" Then
                        Me.FirstHalfMove.CommentBefore.Text = FENComment.Text
                    Else
                        Me.FirstHalfMove.CommentBefore.Text = FENComment.Text & " " & Me.FirstHalfMove.CommentBefore.Text
                    End If
                    FENComment.Text = ""
                End If

                Me.ReNumber()

                'RaiseEvent Changed(Nothing)

            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
            End Try
        End Set
        Get
            Try
                'SAN (Standard Algebraic Notation)
                Dim VariantLevel As Long = 0, VariantNumber As Long = 1
                Dim AfterCommentOrVariantChange As Boolean = False
                Dim PGNMovelist As String = ""

                If FENComment IsNot Nothing Then
                    If Me.Count > 0 _
                    AndAlso Me.FirstHalfMove.CommentBefore IsNot Nothing Then
                        'Combine two comments to avoid two subsequent comments
                        PGNMovelist = (Me.FENComment & Me.FirstHalfMove.CommentBefore).XPGNString
                    Else
                        PGNMovelist = Me.FENComment.XPGNString
                    End If
                End If

                For Each HalfMove As PGNHalfMove In Me

                    'Changes in Level and Variant
                    If VariantLevel = HalfMove.VariantLevel _
                    And VariantNumber <> HalfMove.VariantNumber Then
                        PGNMovelist += ") ("
                        AfterCommentOrVariantChange = True
                    End If
                    While VariantLevel < HalfMove.VariantLevel
                        PGNMovelist += " ("
                        VariantLevel += 1
                        AfterCommentOrVariantChange = True
                    End While
                    While VariantLevel > HalfMove.VariantLevel
                        PGNMovelist += ")"
                        VariantLevel -= 1
                        AfterCommentOrVariantChange = True
                    End While
                    VariantLevel = HalfMove.VariantLevel
                    VariantNumber = HalfMove.VariantNumber

                    'Print Commentbefore
                    If HalfMove.CommentBefore IsNot Nothing _
                    AndAlso HalfMove.CommentBefore.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.CommentBefore.XPGNString
                        AfterCommentOrVariantChange = True
                    End If

                    'Enforce MoveNr and "..." when previous move also was black
                    If HalfMove.Color = BLACK Then
                        If HalfMove.PreviousHalfMove Is Nothing Then
                            AfterCommentOrVariantChange = True
                        ElseIf HalfMove.PreviousHalfMove.Color = BLACK Then
                            AfterCommentOrVariantChange = True
                        End If
                    End If

                    'Print Move
                    If PGNMovelist <> "" And Right(PGNMovelist, 1) <> "(" Then
                        PGNMovelist += " "
                    End If
                    PGNMovelist += HalfMove.MoveNrString(AfterCommentOrVariantChange)
                    PGNMovelist += HalfMove.MoveText
                    If HalfMove.NAGs.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.NAGs.PGNString
                    End If
                    AfterCommentOrVariantChange = False

                    'Print Commentafter
                    If HalfMove.CommentAfter IsNot Nothing _
                        AndAlso HalfMove.CommentAfter.PGNString <> "" Then
                        PGNMovelist += " " & HalfMove.CommentAfter.XPGNString
                        AfterCommentOrVariantChange = True
                    End If

                Next HalfMove

                'Closing brackets at the end
                While VariantLevel > 0
                    PGNMovelist += ")"
                    VariantLevel -= 1
                End While

                Return PGNMovelist

            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
                Return Nothing
            End Try
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property FirstHalfMove() As PGNHalfMove
        Get
            For I As Long = 0 To Me.Count - 1
                If Me(I).Result = "" Then
                    Return Me(I)
                End If
            Next I
            Return Nothing
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property LastHalfMove() As PGNHalfMove
        Get
            For I As Long = Me.Count - 1 To 0 Step -1
                If Me(I).VariantLevel = 0 _
                And Me(I).Result = "" Then
                    Return Me(I)
                End If
            Next
            Return Nothing
        End Get
    End Property

    ''' <summary>Returns a List of possible HalfMoves (NB Given HalfMove can be Nothing)</summary>
    Public Function NextHalfMoves(pPGNHalfMove As PGNHalfMove) As List(Of PGNHalfMove)
        Dim Variants As List(Of PGNHalfMove)
        Dim NextMove As PGNHalfMove
        If pPGNHalfMove Is Nothing Then
            NextMove = Me.FirstHalfMove
        Else
            NextMove = FindNextHalfMove(pPGNHalfMove, pPGNHalfMove.VariantLevel, pPGNHalfMove.VariantNumber)
        End If
        If NextMove Is Nothing Then Return Nothing

        'Find (sub-)Variants of this (One level deeper)
        Variants = NextMove.SubVariants
        'Add NextMove itselve also as an option to choose
        Variants.Insert(0, NextMove)

        Return Variants
    End Function

    ''' <summary>Returns the Previous HalfMove of a specified HalfMove</summary>
    Public Function PreviousHalfMove(pPGNHalfMove As PGNHalfMove) As PGNHalfMove
        Dim PreviousMove As PGNHalfMove
        If pPGNHalfMove Is Nothing Then Return Nothing
        'Find Previous Move at the same Level
        PreviousMove = FindPreviousHalfMove(pPGNHalfMove, pPGNHalfMove.VariantLevel, pPGNHalfMove.VariantNumber)
        If PreviousMove Is Nothing Then
            'Find Variants One level higher
            PreviousMove = FindPreviousHalfMove(pPGNHalfMove, (pPGNHalfMove.VariantLevel - 1))  'Don't know at what variant we end up !
            If PreviousMove Is Nothing Then Return Nothing
            PreviousMove = FindPreviousHalfMove(PreviousMove, (pPGNHalfMove.VariantLevel - 1))  'The first basic move is one of the alternatives
            If PreviousMove Is Nothing Then Return Nothing
        End If
        Return PreviousMove
    End Function

    ''' <summary>Returns next HalfMove matching pVariantLevel and pVariantNumber</summary>
    Public Function FindNextHalfMove(pPGNHalfMove As PGNHalfMove, pVariantLevel As Long, pVariantNumber As Long) As PGNHalfMove
        For I As Long = pPGNHalfMove.Index + 1 To Me.Count - 1
            With Me(I)
                If .Result = "" Then
                    If (.VariantLevel < pVariantLevel) Then 'intermediate higher Variant found
                        Return Nothing
                    End If
                    If (.VariantLevel = pVariantLevel) _
                    And (.VariantNumber = pVariantNumber) Then
                        Return Me(I)
                    End If
                End If
            End With
        Next I
        Return Nothing
    End Function

    ''' <summary>Returns the index of the next move matching these criteria</summary>
    Private Function FindPreviousHalfMove(pPGNHalfMove As PGNHalfMove, Optional pVariantLevel As Long = -1, Optional pVariantNumber As Long = -1) As PGNHalfMove
        For I As Long = pPGNHalfMove.Index - 1 To 0 Step -1
            With Me(I)
                If .Result = "" Then
                    If (pVariantLevel <> -1 And .VariantLevel < pVariantLevel) Then 'intermediate higher Variant found
                        Return Nothing
                    End If
                    If (pVariantLevel = -1 Or .VariantLevel = pVariantLevel) _
                    And (pVariantNumber = -1 Or .VariantNumber = pVariantNumber) Then
                        Return Me(I)
                    End If
                End If
            End With
        Next I
        Return Nothing
    End Function

    ''' <summary>Collects Moves to Play From a specified HalfMove</summary>
    Public Function CollectMoves(pFromHalfMove As PGNHalfMove) As List(Of PGNHalfMove)
        Dim MovesToPlay As New List(Of PGNHalfMove)
        Dim NextMoves As List(Of PGNHalfMove)
        Dim Move As PGNHalfMove = pFromHalfMove
        While (Move IsNot Nothing _
               AndAlso Move.VariantLevel = pFromHalfMove.VariantLevel)
            MovesToPlay.Add(Move)
            'Next Move
            NextMoves = Move.NextHalfMoves
            If NextMoves Is Nothing Then Exit While
            Move = Move.NextHalfMoves(0)
            While (Move IsNot Nothing _
                   AndAlso Move.VariantLevel > pFromHalfMove.VariantLevel)
                'Skip Subvariants
                NextMoves = Move.NextHalfMoves
                If NextMoves Is Nothing Then Exit While
                Move = Move.NextHalfMoves(0)
            End While
        End While
        Return MovesToPlay
    End Function

    ''' <summary>Collect Moves to Play From a specified HalfMove To a specified HalfMove</summary>
    Public Function CollectMoves(pFromHalfMove As PGNHalfMove, pToHalfMove As PGNHalfMove) As List(Of PGNHalfMove)
        Dim MovesToPlay As New List(Of PGNHalfMove)
        'Collect the last move and moves leading to this position
        Dim Move As PGNHalfMove = pToHalfMove
        While (Move IsNot Nothing)
            MovesToPlay.Insert(0, Move)
            If Move Is pFromHalfMove Then Exit While
            Move = Move.PreviousHalfMove
        End While
        Return MovesToPlay
    End Function

    ''' <summary>Determine FirstMove within a specific Variant</summary>
    Public Function FirstMoveInVariant(pPGNHalfMove As PGNHalfMove) As PGNHalfMove
        Dim FirstMove As PGNHalfMove = pPGNHalfMove
        If pPGNHalfMove IsNot Nothing Then
            For I As Long = pPGNHalfMove.Index - 1 To 0 Step -1
                With Me(I)
                    If .VariantLevel < pPGNHalfMove.VariantLevel Then Exit For
                    If .VariantLevel = pPGNHalfMove.VariantLevel _
                    And .VariantNumber <> pPGNHalfMove.VariantNumber Then Exit For
                    FirstMove = Me(I)
                End With
            Next I
        End If
        Return FirstMove
    End Function

    Public Overloads Sub Clear()
        MyBase.Clear()
        Me.CurrentHalfMoveIndex = ""
    End Sub

    ''' <summary>Adds a New HalfMove with specified attributed</summary>
    Public Overloads Function Add(pCommentBefore As String, pMoveNr As String, pMoveText As String, pColor As ChessColor,
                                  Optional pVariantLevel As Long = 0, Optional pVariantNumber As Long = 0) As PGNHalfMove
        Dim HalfMove As New PGNHalfMove(Me, pCommentBefore, pMoveNr, pMoveText, pColor, pVariantLevel, pVariantNumber)
        Me.Add(HalfMove, pRaiseEvent:=False)
        Me.ReNumber()

        Return HalfMove
    End Function

    ''' <summary>Adds a given HalfMove</summary>
    Public Overloads Function Add(pHalfMove As PGNHalfMove, Optional pRaiseEvent As Boolean = True) As PGNHalfMove
        pHalfMove.Index = Me.Count
        MyBase.Add(pHalfMove)
        Me.ReNumber()

        'If pRaiseEvent Then
        '    RaiseEvent Changed(pHalfMove)
        'End If
        Return pHalfMove
    End Function

    ''' <summary>Insert a new move into the MoveList</summary>
    ''' <returns>Returns True when move was added</returns>
    Public Overloads Function Insert(pHalfMove As PGNHalfMove, pCurrentHalfMove As PGNHalfMove) As Boolean
        Dim NextMoves As List(Of PGNHalfMove), ContinuingMoves As List(Of PGNHalfMove)
        NextMoves = Me.NextHalfMoves(pCurrentHalfMove)

        If pCurrentHalfMove IsNot Nothing Then
            If pHalfMove.Piece.Color = pCurrentHalfMove.Color Then
                'Twice or more Moves of same color !!
                'If MsgBox(MessageText("SameColor"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes Then
                '    Return False
                'End If
                If pHalfMove.Color = BLACK Then 'White moves are incremented automatically
                    pHalfMove.MoveNr += 1
                End If
                Dim Halfmove = InsertAfter(pCurrentHalfMove.Index, pHalfMove)
                Halfmove.VariantLevel = pCurrentHalfMove.VariantLevel
                Halfmove.VariantNumber = pCurrentHalfMove.VariantNumber

                'RaiseEvent Changed(pHalfMove)
                Return True
            End If
        End If

        If NextMoves Is Nothing Then
            'Append to the Current Variant
            If pCurrentHalfMove Is Nothing Then
                pHalfMove.VariantLevel = 0
                pHalfMove.VariantNumber = 0
                Me.Add(pHalfMove, pRaiseEvent:=False)
            Else
                pHalfMove.VariantLevel = pCurrentHalfMove.VariantLevel
                pHalfMove.VariantNumber = pCurrentHalfMove.VariantNumber
                Me.InsertAfter(pCurrentHalfMove.Index, pHalfMove)
            End If

            'RaiseEvent Changed(pHalfMove)
            Return True
        End If

        'Ask if a New Subvariant should be added
        If MsgBox(MessageText("InsertSubVariant", pHalfMove.MoveText(CurrentLanguage)), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Add New Subvariant
            If NextMoves.Count = 1 Then 'No Subvariant yet; create first one
                pHalfMove.VariantLevel = NextMoves(0).VariantLevel + 1
                pHalfMove.VariantNumber = 1
            Else
                pHalfMove.VariantLevel = NextMoves(NextMoves.Count - 1).VariantLevel
                pHalfMove.VariantNumber = NextMoves(NextMoves.Count - 1).VariantNumber + 1
            End If
            ContinuingMoves = Me.NextHalfMoves(NextMoves(0))
            If ContinuingMoves Is Nothing Then
                Me.Add(pHalfMove, pRaiseEvent:=False)
            Else
                Me.InsertBefore(ContinuingMoves(0).Index, pHalfMove)
            End If

            'RaiseEvent Changed(pHalfMove)
            Return True
        End If

        If MsgBox(MessageText("InsertMove", pHalfMove.MoveText(CurrentLanguage)), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Insert into current (Main) Variant and remove rest
            If pCurrentHalfMove Is Nothing Then
                'Insert at start of list
                Me.Clear()
                pHalfMove.VariantLevel = 0
                pHalfMove.VariantNumber = 0
                Me.Add(pHalfMove, pRaiseEvent:=False)
            Else
                pHalfMove.VariantLevel = pCurrentHalfMove.VariantLevel
                pHalfMove.VariantNumber = pCurrentHalfMove.VariantNumber
                Me.InsertAfter(pCurrentHalfMove.Index, pHalfMove)
                Me.DeleteVariantAfter(pHalfMove)
            End If

            'RaiseEvent Changed(pHalfMove)
            Return True
        End If

        Return False
    End Function

    ''' <summary>Inserts a given HalfMove at the specified Index</summary>
    Public Function InsertBefore(pMoveIndex As Long, pHalfMove As PGNHalfMove) As PGNHalfMove
        If pMoveIndex > Me.Count - 1 Then
            pHalfMove.Index = Me.Count
            Me.Add(pHalfMove, pRaiseEvent:=False)
        Else
            pHalfMove.Index = pMoveIndex
            Me.Insert(pHalfMove.Index, pHalfMove)
        End If
        Me.ReNumber()
        Return pHalfMove
    End Function

    ''' <summary>Inserts a given HalfMove after a specified Index</summary>
    Private Function InsertAfter(pMoveIndex As Long, pHalfMove As PGNHalfMove) As PGNHalfMove
        If pMoveIndex >= Me.Count - 1 Then
            pHalfMove.Index = Me.Count
            Me.Add(pHalfMove, pRaiseEvent:=False)
        Else
            pHalfMove.Index = pMoveIndex + 1
            Me.Insert(pHalfMove.Index, pHalfMove)
        End If
        Me.ReNumber()
        Return pHalfMove
    End Function

    ''' <summary>Removes the Result Move at the end of the list</summary>
    Public Sub RemoveResult()
        If Me.Count > 0 _
        AndAlso Me.Last.Result <> "" Then
            Me.Remove(Me.Last)
        End If
    End Sub

    ''' <summary>Creates a New Result Move</summary>
    Public Sub UpdateResult(pResult As String)
        If pResult = "" Then Exit Sub
        Dim HalfMove As New PGNHalfMove(Me, pResult:=pResult)
        Me.Add(HalfMove)
    End Sub

    ''' <summary>Update Indexes stored at Halfmove</summary>
    Public Sub ReNumber()
        For I As Long = 0 To Me.Count - 1
            Me(I).Index = I
            Me(I).gHalfMoves = Me
        Next
    End Sub

    ''' <summary>Delete specified and subsequent HalfMoves in a Variant</summary>
    Public Sub DeleteVariantFrom(pHalfMove As PGNHalfMove)
        Dim I As Integer, VariantLevel As Long, VariantNumber As Long
        If pHalfMove Is Nothing Then Exit Sub
        VariantLevel = pHalfMove.VariantLevel
        VariantNumber = pHalfMove.VariantNumber
        I = pHalfMove.Index
        Do While I < Me.Count
            If Me(I).VariantLevel < VariantLevel Then Exit Do
            If Me(I).VariantLevel = VariantLevel _
            And Me(I).VariantNumber <> VariantNumber Then Exit Do
            Me.Remove(I)
        Loop
        Me.ReNumber()

        'RaiseEvent Changed(Nothing)
    End Sub

    ''' <summary>Remove remaining moves from current (sub-) variant</summary>
    Public Sub DeleteVariantAfter(pHalfMove As PGNHalfMove)
        Dim I As Integer, VariantLevel As Long, VariantNumber As Long
        If pHalfMove Is Nothing Then Exit Sub
        VariantLevel = pHalfMove.VariantLevel
        VariantNumber = pHalfMove.VariantNumber
        I = pHalfMove.Index + 1
        Do While I < Me.Count
            With Me(I)
                If .VariantLevel < VariantLevel Then Exit Do
                If .VariantLevel = VariantLevel _
                And .VariantNumber <> VariantNumber Then Exit Do
                Me.Remove(I)
            End With
        Loop
        Me.ReNumber()

        'RaiseEvent Changed(Nothing)
    End Sub

    Private Overloads Sub Remove(pIndexKey As Object)
        MyBase.Remove(Me(pIndexKey))
    End Sub

    <XmlIgnore> 'Is actual member of PGNMove
    Public Property MarkerListString(pCurrentMove As PGNHalfMove) As String
        Set(pMarkerListString As String)
            If pMarkerListString = "" Then 'Remove MarkerList
                If pCurrentMove Is Nothing Then
                    If FENComment Is Nothing _
                    OrElse FENComment.MarkerList Is Nothing Then
                        Exit Property
                    End If
                    FENComment.MarkerList = Nothing
                    Exit Property
                Else
                    If pCurrentMove.CommentAfter Is Nothing _
                    OrElse pCurrentMove.CommentAfter.MarkerList Is Nothing Then
                        Exit Property
                    End If
                    pCurrentMove.CommentAfter.MarkerList = Nothing
                    Exit Property
                End If
            End If
            'So pMarkerString <> ""
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing Then
                    FENComment = New PGNComment("")
                End If
                FENComment.MarkerList = New PGNMarkerList(pMarkerListString)
                Exit Property
            Else
                If pCurrentMove.CommentAfter Is Nothing Then
                    pCurrentMove.CommentAfter = New PGNComment("")
                End If
                pCurrentMove.CommentAfter.MarkerList = New PGNMarkerList(pMarkerListString)
            End If
        End Set
        Get
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing _
                OrElse FENComment.MarkerList Is Nothing Then
                    Return ""
                Else
                    Return FENComment.MarkerList.ListString
                End If
            Else
                Return pCurrentMove.MarkerListString
            End If
        End Get
    End Property

    <XmlIgnore> 'Is actual member of PGNMove
    Public Property ArrowListString(pCurrentMove As PGNHalfMove) As String
        Set(pArrowListString As String)
            If pArrowListString = "" Then 'Remove ArrowList
                If pCurrentMove Is Nothing Then
                    If FENComment Is Nothing _
                    OrElse FENComment.ArrowList Is Nothing Then
                        Exit Property
                    End If
                    FENComment.ArrowList = Nothing
                    Exit Property
                Else
                    If pCurrentMove.CommentAfter Is Nothing _
                    OrElse pCurrentMove.CommentAfter.ArrowList Is Nothing Then
                        Exit Property
                    End If
                    pCurrentMove.CommentAfter.ArrowList = Nothing
                    Exit Property
                End If
            End If
            'So pArrowString <> ""
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing Then
                    FENComment = New PGNComment("")
                End If
                FENComment.ArrowList = New PGNArrowList(pArrowListString)
                Exit Property
            Else
                If pCurrentMove.CommentAfter Is Nothing Then
                    pCurrentMove.CommentAfter = New PGNComment("")
                End If
                pCurrentMove.CommentAfter.ArrowList = New PGNArrowList(pArrowListString)
            End If
        End Set
        Get
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing _
                OrElse FENComment.ArrowList Is Nothing Then
                    Return ""
                Else
                    Return FENComment.ArrowList.ListString
                End If
            Else
                Return pCurrentMove.ArrowListString
            End If
        End Get
    End Property

    <XmlIgnore> 'Is actual member of PGNMove
    Public Property TextListString(pCurrentMove As PGNHalfMove) As String
        Set(pTextListString As String)
            If pTextListString = "" Then 'Remove TextList
                If pCurrentMove Is Nothing Then
                    If FENComment Is Nothing _
                    OrElse FENComment.TextList Is Nothing Then
                        Exit Property
                    End If
                    FENComment.TextList = Nothing
                    Exit Property
                Else
                    If pCurrentMove.CommentAfter Is Nothing _
                    OrElse pCurrentMove.CommentAfter.TextList Is Nothing Then
                        Exit Property
                    End If
                    pCurrentMove.CommentAfter.TextList = Nothing
                    Exit Property
                End If
            End If
            'So pTextString <> ""
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing Then
                    FENComment = New PGNComment("")
                End If
                FENComment.TextList = New PGNTextList(pTextListString)
                Exit Property
            Else
                If pCurrentMove.CommentAfter Is Nothing Then
                    pCurrentMove.CommentAfter = New PGNComment("")
                End If
                pCurrentMove.CommentAfter.TextList = New PGNTextList(pTextListString)
            End If
        End Set
        Get
            If pCurrentMove Is Nothing Then
                If FENComment Is Nothing _
                OrElse FENComment.TextList Is Nothing Then
                    Return ""
                Else
                    Return FENComment.TextList.ListString
                End If
            Else
                Return pCurrentMove.TextListString
            End If
        End Get
    End Property

    Public Sub Print()
        For Each Move As PGNHalfMove In Me
            Debug.Print(Space(3 * Move.VariantLevel) & Move.VariantNumber & "  " & Move.ToString())
        Next
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return "CurrentMoveIndex: " & CurrentHalfMoveIndex & " " & Me.XPGNString
    End Function

    ''' <summary>Returns the First NonNumeric position within the MoveList starting with a given StartPos </summary>
    Private Function FirstNonNumeric(pStartPos As Long, pMoveList As String) As Long
        For P As Long = pStartPos To Len(pMoveList)
            Select Case Mid(pMoveList, P, 1)
                Case "0" To "9"
                Case Else : Return P
            End Select
        Next P
        Return 0
    End Function

    ''' <summary>Returns the First Space or Bracket within the MoveList starting at StartPos</summary>
    Private Function FirstSpaceOrBracket(pStartPos As Long, pMoveList As String) As Long
        Dim P As Long
        For P = pStartPos To Len(pMoveList)
            Select Case Mid(pMoveList, P, 1)
                Case " ", ")", "{" : Return P
            End Select
        Next P
        Return P
    End Function

    Public Sub New()
        Me.Clear()
        Me.FENComment = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        Me.FENComment = Nothing

        MyBase.Finalize()
    End Sub

End Class
