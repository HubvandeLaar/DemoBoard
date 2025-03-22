Option Explicit On
Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessColor

Public Class SetupToolbar
    Public Visible As Boolean = True

    Public gMarkerColor As String = "G" 'G, Y, R, B, C, O

    'NB gPieces(C, R)
    Private ReadOnly gPieces(,) As ChessPiece = {{Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing},
                                                {Nothing, New King(WHITE), New Queen(WHITE), New Rook(WHITE), New Bishop(WHITE), New Knight(WHITE), New Pawn(WHITE)},
                                                {Nothing, New King(BLACK), New Queen(BLACK), New Rook(BLACK), New Bishop(BLACK), New Knight(BLACK), New Pawn(BLACK)}} 'Zerobased 

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
            gCtlBoard.picArrow.Visible = False
            gCtlBoard.picGreen.Visible = False
            gCtlBoard.picYellow.Visible = False
            gCtlBoard.picRed.Visible = False
            gCtlBoard.picBlue.Visible = False
            gCtlBoard.picCyan.Visible = False
            gCtlBoard.picOrange.Visible = False
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
            'Colored Markers
            .PaintImage(frmImages.getImage(gMarkerColor & "Marker"), LeftPos(1), TopPos(9), IconSize, IconOffset)
            .PaintImage(frmImages.getImage(gMarkerColor & "Text"), LeftPos(2), TopPos(9), IconSize, IconOffset)
            .picArrow.Image = frmImages.getImage(gMarkerColor & "Arrow")
            .picArrow.Left = LeftPos(3) + IconOffset : .picArrow.Top = TopPos(9) + IconOffset
            .picArrow.Visible = True : .picArrow.Size = New Size(IconSize, IconSize)
            'Colors
            .picGreen.Left = LeftPos(1) + IconOffset : .picGreen.Top = TopPos(10) + IconOffset
            .picGreen.Visible = True : .picGreen.Size = New Size(IconSize, IconSize)
            .picYellow.Left = LeftPos(2) + IconOffset : .picYellow.Top = TopPos(10) + IconOffset
            .picYellow.Visible = True : .picYellow.Size = New Size(IconSize, IconSize)
            .picRed.Left = LeftPos(3) + IconOffset : .picRed.Top = TopPos(10) + IconOffset
            .picRed.Visible = True : .picRed.Size = New Size(IconSize, IconSize)
            .picBlue.Left = LeftPos(1) + IconOffset : .picBlue.Top = TopPos(11) + IconOffset
            .picBlue.Visible = True : .picBlue.Size = New Size(IconSize, IconSize)
            .picCyan.Left = LeftPos(2) + IconOffset : .picCyan.Top = TopPos(11) + IconOffset
            .picCyan.Visible = True : .picCyan.Size = New Size(IconSize, IconSize)
            .picOrange.Left = LeftPos(3) + IconOffset : .picOrange.Top = TopPos(11) + IconOffset
            .picOrange.Visible = True : .picOrange.Size = New Size(IconSize, IconSize)
        End With
    End Sub

    Private Sub gCtlBoard_MouseDown(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles gCtlBoard.SetupToolbarMouseDown
        Dim C As Long, R As Long
        C = Column(pArgs.X) : R = Row(pArgs.Y)
        If C > 3 Then Exit Sub
        Select Case R
            Case 1, 2, 3, 4, 5, 6  'Pieces
                If C = 3 Then Exit Sub
                Dim ChessPiece = gPieces(C, R)
                If ChessPiece Is Nothing Then Exit Sub
                Call gCtlBoard.SetDragPiece(ChessPiece, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
                Exit Sub

            Case 7 'Circle, Rectangle, Dot
                Dim Symbol As String
                Select Case C
                    Case 1 : Symbol = "0"
                    Case 2 : Symbol = "#"
                    Case 3 : Symbol = "."
                    Case Else : Exit Sub
                End Select
                Call gCtlBoard.SetDragMarker(Symbol, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
                Exit Sub

            Case 8 'Plus, Minus, BlueStar
                Dim Symbol As String
                Select Case C
                    Case 1 : Symbol = "+"
                    Case 2 : Symbol = "-"
                    Case 3 : Symbol = "*"
                    Case Else : Exit Sub
                End Select
                Call gCtlBoard.SetDragMarker(Symbol, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
                Exit Sub

            Case 9  'Marker, Text, Arrow
                Select Case C
                    Case 1 : Call gCtlBoard.SetDragMarker(gMarkerColor, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
                    Case 2 : Call gCtlBoard.SetDragText(gMarkerColor, LeftPos(C), TopPos(R), pArgs.X - LeftPos(C) - IconOffset, pArgs.Y - TopPos(R) - IconOffset)
                    Case 3  'Arrow
                    Case Else : Exit Sub
                End Select
                Exit Sub

            Case 10, 11 'Colors
        End Select
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

