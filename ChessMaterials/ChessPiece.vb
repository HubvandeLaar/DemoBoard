Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports ChessGlobals.ChessColor
Imports System.Xml.Serialization

<XmlInclude(GetType(King))>
<XmlInclude(GetType(Queen))>
<XmlInclude(GetType(Rook))>
<XmlInclude(GetType(Bishop))>
<XmlInclude(GetType(Knight))>
<XmlInclude(GetType(Pawn))>
<XmlType()>
Public Class ChessPiece
    <XmlAttribute()>
    Public Color As ChessColor

    Public Enum PieceType
        <XmlEnum()>
        KING
        <XmlEnum()>
        QUEEN
        <XmlEnum()>
        ROOK
        <XmlEnum()>
        BISHOP
        <XmlEnum()>
        KNIGHT
        <XmlEnum()>
        PAWN
        <XmlEnum()>
        UNKNOWN
    End Enum

    <XmlIgnore>
    Public Overridable ReadOnly Property Type() As PieceType
        Get
            Return UNKNOWN
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property Name(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return "??"
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property MoveName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Return "??"
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property FullName(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            If pLanguage = NEDERLANDS Then
                Return If(Me.Color = WHITE, "Witte ", "Zwarte ") & "??"
            Else
                Return If(Me.Color = WHITE, "White ", "Black ") & "??"
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property FENName As String
        Get
            Return " "
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property IconName As String
        Get
            Return ""
        End Get
    End Property

    Public Overridable Function PossibleMoves(pFromFieldName As String, pChessBoard As ChessBoard) As List(Of BoardMove)
        Return Nothing
    End Function

    Public Function IsValidMove(pChessBoard As ChessBoard, pFromFieldName As String, pToFieldName As String) As Boolean
        Dim PossibleMoves As List(Of BoardMove)
        PossibleMoves = Me.PossibleMoves(pFromFieldName, pChessBoard)
        For Each Move As BoardMove In PossibleMoves
            If Move.ToFieldName = pToFieldName Then
                Return True
            End If
        Next Move
        Return False
    End Function

    Public Shared Function CreatePiece(pFENName As String) As ChessPiece
        Select Case pFENName
            Case "K" : Return New King(WHITE)
            Case "Q" : Return New Queen(WHITE)
            Case "R" : Return New Rook(WHITE)
            Case "B" : Return New Bishop(WHITE)
            Case "N" : Return New Knight(WHITE)
            Case "P" : Return New Pawn(WHITE)
            Case "k" : Return New King(BLACK)
            Case "q" : Return New Queen(BLACK)
            Case "r" : Return New Rook(BLACK)
            Case "b" : Return New Bishop(BLACK)
            Case "n" : Return New Knight(BLACK)
            Case "p" : Return New Pawn(BLACK)
            Case Else : Throw New System.ArgumentOutOfRangeException(MessageText("InvalidFENPiece", pFENName))
        End Select
    End Function

    Public Shared Function CreatePiece(pMoveName As String, pColor As ChessColor) As ChessPiece
        Select Case pMoveName
            Case "K" : Return New King(pColor)
            Case "Q" : Return New Queen(pColor)
            Case "R" : Return New Rook(pColor)
            Case "B" : Return New Bishop(pColor)
            Case "N" : Return New Knight(pColor)
            Case "" : Return New Pawn(pColor)
            Case Else : Throw New System.ArgumentOutOfRangeException(MessageText("Invalid Piece", pMoveName))
        End Select
    End Function

    Public Sub New(pColor As ChessColor)
        Me.Color = pColor
    End Sub

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        Me.Color = Nothing

        MyBase.Finalize()
    End Sub

    Public Overrides Function ToString() As String
        Return Me.FullName
    End Function

End Class

