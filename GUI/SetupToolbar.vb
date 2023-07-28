Option Explicit On
Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessColor

Public Class SetupToolbar
    Public Visible As Boolean = True

    'NB gPieces(C, R)
    Private ReadOnly gPieces(,) As ChessPiece = {{Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing},
                                                {Nothing, New King(WHITE), New Queen(WHITE), New Rook(WHITE), New Bishop(WHITE), New Knight(WHITE), New Pawn(WHITE)},
                                                {Nothing, New King(BLACK), New Queen(BLACK), New Rook(BLACK), New Bishop(BLACK), New Knight(BLACK), New Pawn(BLACK)}} 'Zerobased 
    'NB gMarkerSymbol(C, R)
    Private ReadOnly gMarkerSymbol(,) As String = {{"", "", "", "", "", "", "", "", "", "", "10", "11"},
                                                  {"", "", "", "", "", "", "", "O", "+", "G", "10", "11"},
                                                  {"", "", "", "", "", "", "", "#", "-", "Y", "10", "11"},
                                                  {"", "", "", "", "", "", "", ".", "*", "R", "10", "11"}}

    Private WithEvents gCtlBoard As ctlBoard

    Public Property Left As Long

    Public Property Top As Long

    Public Property Width As Long

    Public Property Height() As Long

    Public ReadOnly Property Bounds() As Rectangle
        Get
            Return New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        End Get
    End Property


    Private ReadOnly Property ColumnWidth As Long
        Get
            Return Me.Width / 3
        End Get
    End Property

    Private ReadOnly Property RowHeight As Long
        Get
            Return Me.Height / 11.2
        End Get
    End Property

    Private ReadOnly Property IconSize As Long
        Get
            Return Me.RowHeight * 0.8
        End Get
    End Property

    Private ReadOnly Property IconOffset As Long
        Get
            Return (Me.RowHeight - Me.IconSize) / 2
        End Get
    End Property

    Public Sub Paint()
        If Visible = False Then
            gCtlBoard.picGArrow.Visible = False
            gCtlBoard.picYArrow.Visible = False
            gCtlBoard.picRArrow.Visible = False
            Exit Sub
        End If

        With gCtlBoard
            'Pieces
            .PaintImage(frmImages.WKing.Image, LeftPos(1), TopPos(1), IconSize, IconOffset)
            .PaintImage(frmImages.WQueen.Image, LeftPos(1), TopPos(2), IconSize, IconOffset)
            .PaintImage(frmImages.WRook.Image, LeftPos(1), TopPos(3), IconSize, IconOffset)
            .PaintImage(frmImages.WBishop.Image, LeftPos(1), TopPos(4), IconSize, IconOffset)
            .PaintImage(frmImages.WKnight.Image, LeftPos(1), TopPos(5), IconSize, IconOffset)
            .PaintImage(frmImages.WPawn.Image, LeftPos(1), TopPos(6), IconSize, IconOffset)

            .PaintImage(frmImages.BKing.Image, LeftPos(2), TopPos(1), IconSize, IconOffset)
            .PaintImage(frmImages.BQueen.Image, LeftPos(2), TopPos(2), IconSize, IconOffset)
            .PaintImage(frmImages.BRook.Image, LeftPos(2), TopPos(3), IconSize, IconOffset)
            .PaintImage(frmImages.BBishop.Image, LeftPos(2), TopPos(4), IconSize, IconOffset)
            .PaintImage(frmImages.BKnight.Image, LeftPos(2), TopPos(5), IconSize, IconOffset)
            .PaintImage(frmImages.BPawn.Image, LeftPos(2), TopPos(6), IconSize, IconOffset)
            'Markers
            .PaintImage(frmImages.Circle.Image, LeftPos(1), TopPos(7), IconSize, IconOffset)
            .PaintImage(frmImages.Rectangle.Image, LeftPos(2), TopPos(7), IconSize, IconOffset)
            .PaintImage(frmImages.Dot.Image, LeftPos(3), TopPos(7), IconSize, IconOffset)
            .PaintImage(frmImages.PlusSign.Image, LeftPos(1), TopPos(8), IconSize, IconOffset)
            .PaintImage(frmImages.MinusSign.Image, LeftPos(2), TopPos(8), IconSize, IconOffset)
            .PaintImage(frmImages.BlueStar.Image, LeftPos(3), TopPos(8), IconSize, IconOffset)
            .PaintImage(frmImages.GMarker.Image, LeftPos(1), TopPos(9), IconSize, IconOffset)
            .PaintImage(frmImages.YMarker.Image, LeftPos(2), TopPos(9), IconSize, IconOffset)
            .PaintImage(frmImages.RMarker.Image, LeftPos(3), TopPos(9), IconSize, IconOffset)
            'Text
            .PaintImage(frmImages.GText.Image, LeftPos(1), TopPos(10), IconSize, IconOffset)
            .PaintImage(frmImages.YText.Image, LeftPos(2), TopPos(10), IconSize, IconOffset)
            .PaintImage(frmImages.RText.Image, LeftPos(3), TopPos(10), IconSize, IconOffset)
            'Arrows
            .picGArrow.Left = LeftPos(1) + IconOffset : .picGArrow.Top = TopPos(11) + IconOffset
            .picGArrow.Visible = True : .picGArrow.Size = New Size(IconSize, IconSize)
            .picYArrow.Left = LeftPos(2) + IconOffset : .picYArrow.Top = TopPos(11) + IconOffset
            .picYArrow.Visible = True : .picYArrow.Size = New Size(IconSize, IconSize)
            .picRArrow.Left = LeftPos(3) + IconOffset : .picRArrow.Top = TopPos(11) + IconOffset
            .picRArrow.Visible = True : .picRArrow.Size = New Size(IconSize, IconSize)
        End With
    End Sub

    Private Sub gCtlBoard_MouseDown(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles gCtlBoard.SetupToolbarMouseDown
        Dim C As Long, R As Long
        C = Column(pArgs.X) : R = Row(pArgs.Y)
        If C > 3 Then Exit Sub
        If R < 7 Then 'Pieces
            If C = 3 Then Exit Sub
            Dim ChessPiece = gPieces(C, R)
            If ChessPiece Is Nothing Then Exit Sub
            Call gCtlBoard.SetDragPiece(ChessPiece, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
            Exit Sub

        ElseIf R < 10 Then 'FieldMarkers
            Dim Symbol As String = gMarkerSymbol(C, R)
            If Symbol = "" Then Exit Sub
            Call gCtlBoard.SetDragMarker(Symbol, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
            Exit Sub

        ElseIf R = 10 Then 'Text
            Dim Color As String
            Select Case C
                Case 1 : Color = "G"
                Case 2 : Color = "Y"
                Case 3 : Color = "R"
                Case Else : Exit Sub
            End Select
            Call gCtlBoard.SetDragText(Color, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
            Exit Sub

        End If
    End Sub

    Private Function LeftPos(pColumn As Long) As Long
        Return Me.Left + ((pColumn - 1) * Me.ColumnWidth)
    End Function

    Private Function Column(pLeft As Long) As Long
        Return Int((pLeft - Me.Left) / Me.ColumnWidth) + 1
    End Function

    Private Function TopPos(pRow As Long) As Long
        Return (pRow - 1) * Me.RowHeight
    End Function

    Private Function Row(pTop As Long) As Long
        'Counting from Top to Bottom
        Return Int(pTop / Me.RowHeight) + 1
    End Function

    Public Sub New(pCtlBoard As ctlBoard)
        gCtlBoard = pCtlBoard
    End Sub

End Class

