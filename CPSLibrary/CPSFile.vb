Option Explicit On

Imports System.Xml.Serialization
Imports ChessGlobals.ChessColor
Imports ChessMaterials
Imports PGNLibrary

<XmlType("File")>
Public Class CPSFile
    Public FullFileName As String
    Public Positions As New CPSPositions()

    Public ReadOnly Property FileName As String
        Get
            Dim P1 As Long, P2 As Long
            P1 = InStrRev(Me.FullFileName, "/")
            P2 = InStrRev(Me.FullFileName, "\")
            Return Mid(Me.FullFileName, Math.Max(P1, P2) + 1)
        End Get
    End Property

    Public Sub Open(pFileName As String)
        Me.FullFileName = pFileName
        Positions = CPSPositions.DeSerialize(pFileName)
    End Sub

    Public Function ConvertToPGN() As PGNGames
        Dim ChessBoard As New ChessBoard()
        Dim PGNGames = New PGNGames()
        For Each Position As CPSPosition In Me.Positions.PositionList
            ChessBoard.Clear()
            Dim PGNGame As PGNGame = PGNGames.Add()
            PGNGame.Tags.Add("Title", Position.Name)
            PGNGame.Tags.Add("Memo", Position.Description)
            For Each Arrow As CPSArrow In Position.Arrows
                Dim NewArrow = New Arrow(Arrow.PGNColor(), Arrow.FieldName(Arrow.StartPoint), Arrow.FieldName(Arrow.EndPoint))
                If NewArrow.FromFieldName = NewArrow.ToFieldName Then Continue For
                If PGNGame.HalfMoves.FENComment Is Nothing Then
                    PGNGame.HalfMoves.FENComment = New PGNComment("")
                End If
                If PGNGame.HalfMoves.FENComment.ArrowList Is Nothing Then
                    PGNGame.HalfMoves.FENComment.ArrowList = New PGNArrowList()
                End If
                PGNGame.HalfMoves.FENComment.ArrowList.Add(NewArrow)
            Next Arrow
            For Each Field As CPSField In Position.Fields
                Select Case Field.Piece
                    Case "GreenPlus"
                        If PGNGame.HalfMoves.FENComment Is Nothing Then
                            PGNGame.HalfMoves.FENComment = New PGNComment("")
                        End If
                        If PGNGame.HalfMoves.FENComment.MarkerList Is Nothing Then
                            PGNGame.HalfMoves.FENComment.MarkerList = New PGNMarkerList()
                        End If
                        PGNGame.HalfMoves.FENComment.MarkerList.Add(New Marker("+", Field.Name))
                    Case "RedMin"
                        If PGNGame.HalfMoves.FENComment Is Nothing Then
                            PGNGame.HalfMoves.FENComment = New PGNComment("")
                        End If
                        If PGNGame.HalfMoves.FENComment.MarkerList Is Nothing Then
                            PGNGame.HalfMoves.FENComment.MarkerList = New PGNMarkerList()
                        End If
                        PGNGame.HalfMoves.FENComment.MarkerList.Add(New Marker("-", Field.Name))
                    Case "BlueStar"
                        If PGNGame.HalfMoves.FENComment Is Nothing Then
                            PGNGame.HalfMoves.FENComment = New PGNComment("")
                        End If
                        If PGNGame.HalfMoves.FENComment.MarkerList Is Nothing Then
                            PGNGame.HalfMoves.FENComment.MarkerList = New PGNMarkerList()
                        End If
                        PGNGame.HalfMoves.FENComment.MarkerList.Add(New Marker("*", Field.Name))
                    Case "King"
                        ChessBoard.Fields(Field.Name).Piece = New King(If(Field.Color = "White", WHITE, BLACK))
                    Case "Queen"
                        ChessBoard.Fields(Field.Name).Piece = New Queen(If(Field.Color = "White", WHITE, BLACK))
                    Case "Rook"
                        ChessBoard.Fields(Field.Name).Piece = New Rook(If(Field.Color = "White", WHITE, BLACK))
                    Case "Bishop"
                        ChessBoard.Fields(Field.Name).Piece = New Bishop(If(Field.Color = "White", WHITE, BLACK))
                    Case "Knight"
                        ChessBoard.Fields(Field.Name).Piece = New Knight(If(Field.Color = "White", WHITE, BLACK))
                    Case "Pawn"
                        ChessBoard.Fields(Field.Name).Piece = New Pawn(If(Field.Color = "White", WHITE, BLACK))
                End Select
            Next Field
            PGNGame.Tags.Add("FEN", ChessBoard.FEN)
        Next Position
        Return PGNGames
    End Function

    Public Sub Save(pFileName As String)
        Positions.Serialize(pFileName)
    End Sub

    Public Sub New(pFileName As String)
        Me.Open(pFileName)
    End Sub

    Protected Overrides Sub Finalize()
        Me.Positions = Nothing

        MyBase.Finalize()
    End Sub
End Class
