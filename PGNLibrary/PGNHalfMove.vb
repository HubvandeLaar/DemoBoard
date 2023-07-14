Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNHalfMove

    <XmlAttribute()>
    Public Color As ChessColor
    <XmlElement()>
    Public NAGs As New PGNNAGs()

    <XmlAttribute()>
    Public VariantLevel As Long
    <XmlAttribute()>
    Public VariantNumber As Long

    <XmlAttribute()>
    Public Index As Long

    <XmlAttribute()>
    Public Result As String
    <XmlAttribute()>
    Public Castling As String
    <XmlElement()>
    Public Piece As ChessPiece
    <XmlAttribute()>
    Public ToFieldName As String
    <XmlAttribute()>
    Public Capture As String
    <XmlAttribute()>
    Public Rest As String 'ep, pat/stalemate, QRBN (promotion), + or #
    <XmlAttribute()>
    Public FromColumnName As String, FromRowName As String

    <XmlElement()>
    Public CommentBefore As PGNComment = Nothing
    <XmlElement()>
    Public CommentAfter As PGNComment = Nothing
    <XmlIgnore>
    Public gHalfMoves As PGNHalfMoves
    <XmlAttribute()>
    Public gMoveNr As Long

    <XmlIgnore>
    Property MoveText(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Set(pMoveText As String) 'Parse MoveText into other properties
            Dim Move As String, P As Long
            Move = Trim(pMoveText) 'Found parts will be removed from the local 'Move'

            'Find Result
            If InStr(Move, "1-0") > 0 Then Me.Result = "1-0"
            If InStr(Move, "0-1") > 0 Then Me.Result = "0-1"
            If InStr(Move, "1/2-1/2") > 0 Then Me.Result = "1/2-1/2"
            If InStr(Move, "½-½") > 0 Then Me.Result = "½-½"
            If InStr(Move, "*") > 0 Then Me.Result = "*"
            Move = Replace(Move, Me.Result, "", 1, 1)  'Remove found result
            If Me.Result = "1/2-1/2" Then Me.Result = "½-½"

            'Find Castling
            If InStr(Move, "O-O-O") > 0 Then
                Me.Castling = "O-O-O"
            ElseIf InStr(Move, "0-0-0") > 0 Then
                Me.Castling = "0-0-0"
            ElseIf InStr(Move, "O-O") > 0 Then
                Me.Castling = "O-O"
            ElseIf InStr(Move, "0-0") > 0 Then
                Me.Castling = "0-0"
            Else
                Me.Castling = ""
            End If
            Move = Replace(Move, Me.Castling, "", 1, 1) 'Remove found castling

            'Find Piece
            If InStr("KQRBN", Left(Move, 1)) <> 0 Then
                Me.Piece = ChessPiece.CreatePiece(Left(Move, 1), Me.Color)
                Move = Mid(Move, 2) 'Remove found piece
            ElseIf Castling = "" And Result = "" Then
                Me.Piece = New Pawn(Me.Color)
            Else
                Me.Piece = Nothing
            End If

            'Find To field and Rest
            Me.ToFieldName = "" : Me.Rest = ""
            For P = Len(Move) To 2 Step -1 'Find backwards an fieldnumber
                If Mid(Move, P - 1, 2) Like "[abcdefgh][12345678]" Then
                    Me.ToFieldName = Mid(Move, (P - 1), 2)
                    Me.Rest = Mid(Move, P + 1)
                    Exit For
                End If
            Next P
            Move = Replace(Move, Me.ToFieldName, "", 1, 1) 'Remove found To
            Move = Replace(Move, Me.Rest, "", 1, 1)  'Remove found rest

            'Find Capture
            If InStr(Move, "x", vbTextCompare) > 0 Then
                Me.Capture = "x"
            ElseIf InStr(Move, "-", vbTextCompare) > 0 Then
                Me.Capture = "-"
            Else
                Me.Capture = ""
            End If
            Move = Replace(Move, Me.Capture, "", 1, 1) 'Remove found capture-sign

            'Find Frominfo
            Me.FromColumnName = "" : Me.FromRowName = ""
            For P = 1 To Len(Move)
                If Mid(Move, P, 1) Like "[abcdefgh]" Then
                    Me.FromColumnName = Mid(Move, P, 1)
                ElseIf Mid(Move, P, 1) Like "[12345678]" Then
                    Me.FromRowName = Mid(Move, P, 1)
                End If
            Next P
            Move = Replace(Move, Me.FromColumnName & Me.FromRowName, "", 1, 1) 'Remove found From

            If Trim(Move) <> "" Then
                If Me.Rest <> "" Then Stop
                Me.Rest = Move
            End If
        End Set

        Get
            If Me.Result <> "" Then Return Me.Result
            If Me.Castling <> "" Then Return Me.Castling & Me.Rest
            If Me.Piece.Type = PieceType.PAWN Then
                If PromotionPiece() Is Nothing Then
                    Return Me.FromColumnName &
                           Me.FromRowName &
                           Me.Capture &
                           Me.ToFieldName &
                           Me.Rest
                Else
                    Return Me.FromColumnName &
                           Me.FromRowName &
                           Me.Capture &
                           Me.ToFieldName &
                           Replace(Me.Rest, PromotionPiece.MoveName, PromotionPiece.MoveName(pLanguage))
                End If
            End If
            Return Me.Piece.MoveName(pLanguage) &
                       Me.FromColumnName &
                       Me.FromRowName &
                       Me.Capture &
                       Me.ToFieldName &
                       Me.Rest
        End Get
    End Property

    <XmlIgnore>
    Public Property MoveNr() As Long
        Set(pMoveNr As Long)
            gMoveNr = pMoveNr
        End Set
        Get
            Return gMoveNr
        End Get
    End Property

    <XmlIgnore>
    Public Property MoveNrString(Optional pIncludeDummyWhiteMove As Boolean = False) As String
        Set(pMoveNrString As String)
            gMoveNr = Val(pMoveNrString)
        End Set
        Get
            Select Case Me.Color
                Case WHITE
                    Return Strings.Format(gMoveNr) & ". "
                Case BLACK
                    If pIncludeDummyWhiteMove = True Then
                        Return Strings.Format(gMoveNr) & "... "
                    Else
                        Return "" 'Black move directly after White Move needs no number
                    End If
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property PromotionPiece() As ChessPiece
        Get
            If Me.Piece.Type <> ChessPiece.PieceType.PAWN Then
                Return Nothing
            End If
            If Me.Rest Like "*Q*" Then
                Return New Queen(Me.Color)
            ElseIf Me.Rest Like "*R*" Then
                Return New Rook(Me.Color)
            ElseIf Me.Rest Like "*B*" Then
                Return New Bishop(Me.Color)
            ElseIf Me.Rest Like "*N*" Then
                Return New Knight(Me.Color)
            Else
                Return Nothing
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Property JournalImage As String
        Set(pJournalImage As String) 'Needed to restore from Journaling
            Dim Image As XElement = XElement.Parse(pJournalImage)
            Me.CommentBefore = New PGNComment(Image.Element("CommentBefore").Value)
            Me.MoveNrString = Image.Element("MoveNr").Value
            Me.MoveText = Image.Element("MoveText").Value
            Me.CommentAfter = New PGNComment(Image.Element("CommentAfter").Value)
        End Set
        Get 'Needed to store Before and After Image in Journaling
            Dim Image As New XElement("Image")
            Image.Add(New XElement("CommentBefore", If(Me.CommentBefore Is Nothing, "", Me.CommentBefore.XPGNString)))
            Image.Add(New XElement("MoveNr", Me.MoveNrString(True)))
            Image.Add(New XElement("MoveText", Me.MoveText()))
            Image.Add(New XElement("CommentAfter", If(Me.CommentAfter Is Nothing, "", Me.CommentAfter.XPGNString)))
            Return Image.ToString()
        End Get
    End Property

    <XmlIgnore>
    Public Property MarkerListString As String
        Set(pMarkerListString As String)
            If pMarkerListString = "" Then
                If Me.CommentAfter Is Nothing Then Exit Property
                Me.CommentAfter.MarkerList = Nothing
                Exit Property
            End If
            If Me.CommentAfter Is Nothing Then
                Me.CommentAfter = New PGNComment("")
            End If
            If Me.CommentAfter.MarkerList Is Nothing Then
                Me.CommentAfter.MarkerList = New PGNMarkerList(pMarkerListString)
            Else
                Me.CommentAfter.MarkerList.ListString = pMarkerListString
            End If
        End Set
        Get
            If Me.CommentAfter Is Nothing _
            OrElse Me.CommentAfter.MarkerList Is Nothing Then Return ""
            Return Me.CommentAfter.MarkerList.ListString
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property ToColumn() As String
        Get
            If ToFieldName = "" Then
                Return ""
            Else
                Return Left(ToFieldName, 1)
            End If
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property ToRow() As String
        Get
            If ToFieldName = "" Then
                Return ""
            Else
                Return Right(ToFieldName, 1)
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Property ArrowListString As String
        Set(pArrowListString As String)
            If pArrowListString = "" Then
                If Me.CommentAfter Is Nothing Then Exit Property
                Me.CommentAfter.ArrowList = Nothing
                Exit Property
            End If
            If Me.CommentAfter Is Nothing Then
                Me.CommentAfter = New PGNComment("")
            End If
            If Me.CommentAfter.ArrowList Is Nothing Then
                Me.CommentAfter.ArrowList = New PGNArrowList(pArrowListString)
            Else
                Me.CommentAfter.ArrowList.ListString = pArrowListString
            End If
        End Set
        Get
            If Me.CommentAfter Is Nothing _
            OrElse Me.CommentAfter.ArrowList Is Nothing Then Return ""
            Return Me.CommentAfter.ArrowList.ListString
        End Get
    End Property

    <XmlIgnore>
    Public Property TextListString As String
        Set(pTextListString As String)
            If pTextListString = "" Then
                If Me.CommentAfter Is Nothing Then Exit Property
                Me.CommentAfter.TextList = Nothing
                Exit Property
            End If
            If Me.CommentAfter Is Nothing Then
                Me.CommentAfter = New PGNComment("")
            End If
            If Me.CommentAfter.TextList Is Nothing Then
                Me.CommentAfter.TextList = New PGNTextList(pTextListString)
            Else
                Me.CommentAfter.TextList.ListString = pTextListString
            End If
        End Set
        Get
            If Me.CommentAfter Is Nothing _
            OrElse Me.CommentAfter.TextList Is Nothing Then Return ""
            Return Me.CommentAfter.TextList.ListString
        End Get
    End Property

    <XmlIgnore>
    Public Property TrainingQuestion As PGNTrainingQuestion
        Get
            If Me.CommentBefore Is Nothing Then Return Nothing
            Return Me.CommentBefore.TrainingQuestion
        End Get
        Set(pTrainingQuestion As PGNTrainingQuestion)
            If Me.CommentBefore Is Nothing Then
                If pTrainingQuestion Is Nothing Then Exit Property
                Me.CommentBefore = New PGNComment("")
            End If
            Me.CommentBefore.TrainingQuestion = pTrainingQuestion
        End Set
    End Property

    <XmlIgnore>
    Public ReadOnly Property PreviousHalfMove() As PGNHalfMove
        Get
            Return gHalfMoves.PreviousHalfMove(Me)
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property NextHalfMoves() As List(Of PGNHalfMove)
        Get
            Return gHalfMoves.NextHalfMoves(Me)
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property IsFirstMoveInVariant()
        Get
            Dim PreviousHalfMove As PGNHalfMove = Me.PreviousHalfMove
            If PreviousHalfMove Is Nothing Then
                'First Move in Variant of first Main move List
                Return True
            ElseIf Me.VariantLevel <> PreviousHalfMove.VariantLevel _
                Or Me.VariantNumber <> PreviousHalfMove.VariantNumber Then
                'First Move in Variant
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property FirstMoveInVariant()
        Get
            Return gHalfMoves.FirstMoveInVariant(Me)
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property SubVariants() As List(Of PGNHalfMove)
        Get
            Dim Variants As New List(Of PGNHalfMove)
            Dim P As Long, LastVariantNumber As Long
            For P = Me.Index + 1 To gHalfMoves.Count - 1
                With gHalfMoves(P)
                    If .VariantLevel = Me.VariantLevel _
                    And .VariantNumber = Me.VariantNumber Then
                        'Back at Level/Number of the FirstMove. Store position of the Halfmove
                        Return Variants
                    End If
                    If .VariantLevel <= Me.VariantLevel Then
                        Return Variants
                    End If
                    If .VariantLevel = Me.VariantLevel + 1 _
                    And .VariantNumber <> LastVariantNumber Then
                        'Found new VariantNumber. Store position of the Halfmove
                        Variants.Add(gHalfMoves(P))
                        LastVariantNumber = .VariantNumber
                    End If
                End With
            Next P
            Return Variants
        End Get
    End Property

    Public Function BoardMove(pChessBoard As ChessBoard) As BoardMove
        Dim Field As ChessField, FromField As ChessField
        If Me.Color = ChessColor.UNKNOWN Then
            Me.Color = pChessBoard.ActiveColor
        End If
        FromField = pChessBoard.Fields(If(Me.Color = WHITE, "e1", "e8"))
        If Castling Like "?-?" Then
            Return New BoardMove(FromField.Piece, FromField.Name, If(Me.Color = WHITE, "g1", "g8"), pCastle:=True)
        ElseIf Castling Like "?-?-?" Then
            Return New BoardMove(FromField.Piece, FromField.Name, If(Me.Color = WHITE, "c1", "c8"), pCastle:=True)
        End If

        If Me.Piece.Color = ChessColor.UNKNOWN Then
            Me.Piece.Color = pChessBoard.ActiveColor
        End If
        Field = pChessBoard.FindPiece(Me.Piece, FromColumnName, FromRowName, Me.ToFieldName)
        Return New BoardMove(Me.Piece, Field.Name, Me.ToFieldName, pCastle:=False,
                                                                   pEnPassant:=(Me.Rest Like "*ep*"),
                                                                   pPromotionPiece:=PromotionPiece())
    End Function

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return MoveNrString(True) & Me.MoveText
    End Function

    Public Sub New(Optional pHalfMoves As PGNHalfMoves = Nothing, Optional pCommentBefore As String = "",
                   Optional pMoveNr As String = "", Optional pMoveText As String = "", Optional pColor As ChessColor = UNKNOWN,
                   Optional pVariantLevel As Long = 0, Optional pVariantNumber As Long = 1)
        gHalfMoves = pHalfMoves
        If pCommentBefore = "" Then
            Me.CommentBefore = Nothing
        Else
            Me.CommentBefore = New PGNComment(pCommentBefore)
        End If
        Me.CommentAfter = Nothing
        Me.MoveNrString = pMoveNr
        Me.Color = pColor
        Me.MoveText = pMoveText
        Me.VariantLevel = pVariantLevel
        Me.VariantNumber = pVariantNumber
    End Sub

    Public Sub New(pHalfMoves As PGNHalfMoves, pChessBoard As ChessBoard,
                   pMoveNr As Long, pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                   pCaptured As Boolean, pPromotionPiece As ChessPiece, pFENBeforeMove As String)
        gHalfMoves = pHalfMoves
        Me.CommentBefore = Nothing
        Me.CommentAfter = Nothing
        Me.MoveNr = pMoveNr
        Me.Color = pPiece.Color
        Me.Result = ""
        Me.VariantLevel = 0
        Me.VariantNumber = 0

        Me.Piece = pPiece
        Me.ToFieldName = pToFieldName

        Me.Rest = ""
        If pPiece.Type = PieceType.PAWN _
        And pToFieldName = pChessBoard.EpFieldName Then
            Me.Rest = Me.Rest & "ep"
        End If
        If pPiece.Type = PieceType.PAWN _
        And pPromotionPiece IsNot Nothing Then
            Me.Rest = Me.Rest & pPromotionPiece.MoveName()
        End If
        If King.InCheck(Opponent(pPiece.Color), pChessBoard) = True Then
            If King.CheckMate(Opponent(pPiece.Color), pChessBoard) = True Then
                Me.Rest = Me.Rest & "#"
            Else
                Me.Rest = Me.Rest & "+"
            End If
        End If

        Me.Castling = ""
        If pPiece.Type = PieceType.KING Then
            If pPiece.Color = WHITE _
            And pFromFieldName = "e1" And pToFieldName = "c1" Then
                Me.Castling = "O-O-O"
            ElseIf pPiece.Color = WHITE _
            And pFromFieldName = "e1" And pToFieldName = "g1" Then
                Me.Castling = "O-O"
            ElseIf pPiece.Color = BLACK _
            And pFromFieldName = "e8" And pToFieldName = "c8" Then
                Me.Castling = "O-O-O"
            ElseIf pPiece.Color = BLACK _
            And pFromFieldName = "e8" And pToFieldName = "g8" Then
                Me.Castling = "O-O"
            End If
        End If

        Dim Board As New ChessBoard(pFENBeforeMove), From As String
        If pPiece.Type = PieceType.PAWN Then
            If pPiece.IsValidMove(Board, pFromFieldName, pToFieldName) = False Then
                Me.FromColumnName = Left(pFromFieldName, 1)
                Me.FromRowName = Right(pFromFieldName, 1)
            ElseIf pCaptured = True Then
                Me.FromColumnName = Left(pFromFieldName, 1)
                Me.FromRowName = ""
            Else
                Me.FromColumnName = ""
                Me.FromRowName = ""
            End If
        Else
            From = Board.BriefNotationFieldName(pPiece, pFromFieldName, pToFieldName)
            If Len(From) = 2 Then
                Me.FromColumnName = Mid(From, 1, 1)
                Me.FromRowName = Mid(From, 2, 1)
            Else
                If Val(From) > 0 Then
                    Me.FromColumnName = ""
                    Me.FromRowName = From
                Else
                    Me.FromColumnName = From
                    Me.FromRowName = ""
                End If
            End If
        End If

        If Me.FromColumnName = "" _
            And Me.FromRowName = "" Then
            Me.Capture = If(pCaptured, "x", "")
        Else
            Me.Capture = If(pCaptured, "x", "-")
        End If

        '  Board = Nothing
    End Sub

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        Me.CommentBefore = Nothing
        Me.CommentAfter = Nothing
        Me.NAGs = Nothing

        MyBase.Finalize()
    End Sub
End Class
