Option Explicit On

Imports System.Drawing
Imports System.Drawing.Graphics

Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessColor
Imports ChessMaterials.ChessPiece
Imports PGNLibrary

Public Class ctlBoard
    Private gColorBoard As Boolean = False
    Private gSwitchedSides As Boolean = False

    Private gInternalChessBoard As ChessBoard
    Private gInternalMarkerList As New PGNMarkerList()
    Private gInternalArrowList As New PGNArrowList()
    Private gInternalTextList As New PGNTextList()
    Private gBitmap As Bitmap
    Private gBitmapGraphics As Graphics
    Private gFieldSize As Integer
    Private gBorderSize As Integer
    Private gfrmBoardSize As Integer

    Private ReadOnly gSetupToolbar As New SetupToolbar(Me)

    'For Dragging Pieces
    Private gFENBeforeDragging As String
    Private gDragPiece As ChessPiece
    Private gDragMarker As Marker
    Private gDragText As Text
    Private gDragOffset As Point

    'For Drawing Arrows
    Private gFromField As ChessField

    'For Inserting Pieces by KeyPress
    Private gMouseX As Long
    Private gMouseY As Long

    <System.Runtime.InteropServices.DllImportAttribute("user32.dll")>
    Private Shared Function DestroyIcon(ByVal pHandle As IntPtr) As Boolean
    End Function

    Public Event SetupToolbarMouseDown(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs)
    Public Event MouseRightClickOnField(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs, pFieldName As String)
    ''' <summary>When a piece has been moved from the SetupToolBar on to the board</summary>
    Public Event NewChessPiece(pPiece As ChessPiece, pToFieldName As String, pChessBoard As ChessBoard)
    ''' <summary>When a piece has been moved on the board</summary>
    ''' <param name="pChessBoard">NB board contains position after the move !!!</param>
    Public Event ChessPieceStartMoving(pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard)
    Public Event ChessPieceMoved(pPiece As ChessPiece, pFromFieldName As String, pToFieldName As String,
                                 pChessBoard As ChessBoard, pCaptured As Boolean, pPromotionPiece As ChessPiece, pFEN As String, pFENBeforeDragging As String)
    Public Event ChessPieceRemoved(pSender As Object, pPiece As ChessPiece, pFromFieldName As String, pChessBoard As ChessBoard)
    Public Event ActiveColorChanged(pSender As Object, pActiveColor As ChessColor, pChessBoard As ChessBoard)
    Public Event FieldMarkerListChanged(pSender As Object, pFieldMarkerString As String)
    Public Event ArrowListChanged(pSender As Object, pArrowString As String)
    Public Event TextListChanged(pSender As Object, pTextString As String)

    Private ReadOnly Property FieldSize As Integer
        Get
            'Me.Height = Border + 8 Fields + Border + Label.Height ; (Label=Field)
            'Me.Width = Label + Border + 8 Fields + Border + ColorInd + 3 Fields; (ColorInd, Toolbar and Labels are Fields)
            Return Math.Min((Me.Height * 120) / ((2 * 16) + (9 * 120)), (Me.Width * 120) / ((2 * 16) + (13 * 120)))
        End Get
    End Property

    Private ReadOnly Property BorderSize As Integer
        Get
            Return (gFieldSize * 16) / 120  'When Field in bitmap is 120, Border is 16 
        End Get
    End Property

    Private ReadOnly Property BoardSize As Integer
        Get
            Return (gFieldSize * 8) + (gBorderSize * 2)
        End Get
    End Property

    Public ReadOnly Property SetupToolbar As SetupToolbar
        Get
            Return gSetupToolbar
        End Get
    End Property

    Public Property SetupToolbarVisible As Boolean
        Set(pSetupToolbarVisible As Boolean)
            gSetupToolbar.Visible = pSetupToolbarVisible
            Call Me.Paint()
        End Set
        Get
            Return gSetupToolbar.Visible
        End Get
    End Property

    Public Property ColorBoard As Boolean
        Set(pColorBoard As Boolean)
            gColorBoard = pColorBoard
            Me.Paint()
        End Set
        Get
            Return gColorBoard
        End Get
    End Property

    Public Property SwitchedSides As Boolean
        Set(pSwitchedSides As Boolean)
            gSwitchedSides = pSwitchedSides
            Me.Paint()
        End Set
        Get
            Return gSwitchedSides
        End Get
    End Property

    Public Overrides Property BackColor As Drawing.Color
        Set(pBackGrounColor As Drawing.Color)
            MyBase.BackColor = pBackGrounColor
            Me.picBoard.BackColor = pBackGrounColor
        End Set
        Get
            Return MyBase.BackColor
        End Get
    End Property

    Public Property FEN As String
        Set(pFEN As String)
            gInternalChessBoard.FEN = pFEN
            gFENBeforeDragging = pFEN
            Me.Paint()
        End Set
        Get
            Return gInternalChessBoard.FEN
        End Get
    End Property

    ''' <summary>FEN as it was before the move registered</summary>
    Public ReadOnly Property FENBeforeDragging As String
        Get
            Return gFENBeforeDragging
        End Get
    End Property

    Public ReadOnly Property ActiveColor As ChessColor
        Get
            Return gInternalChessBoard.ActiveColor
        End Get
    End Property

    Public Property MarkerString As String
        Set(pFieldMarkerString As String)
            gInternalChessBoard.ClearMarkers()
            gInternalMarkerList = New PGNMarkerList(pFieldMarkerString)
            For Each FieldMarker As Marker In gInternalMarkerList
                gInternalChessBoard(FieldMarker.FieldName).Marker = FieldMarker
            Next FieldMarker
            Me.Paint()
        End Set
        Get
            If gInternalMarkerList Is Nothing Then
                Return ""
            Else
                gInternalMarkerList.Clear()
                For Each Field As ChessField In gInternalChessBoard
                    If Field Is Nothing Then Continue For
                    If Field.Marker Is Nothing Then Continue For
                    gInternalMarkerList.Add(Field.Marker)
                Next Field
                Return gInternalMarkerList.ListString
            End If
        End Get
    End Property

    Public Property ArrowString As String
        Set(pArrowString As String)
            gInternalArrowList = New PGNArrowList(pArrowString)
            Me.Paint()
        End Set
        Get
            If gInternalArrowList Is Nothing Then
                Return ""
            Else
                Return gInternalArrowList.ListString
            End If
        End Get
    End Property

    Public Property TextString As String
        Set(pTextString As String)
            gInternalChessBoard.ClearTexts()
            gInternalTextList = New PGNTextList(pTextString)
            For Each Text As Text In gInternalTextList
                gInternalChessBoard(Text.FieldName).Text = Text
            Next Text
            Me.Paint()
        End Set
        Get
            If gInternalTextList Is Nothing Then
                Return ""
            Else
                gInternalTextList.Clear()
                For Each Field As ChessField In gInternalChessBoard
                    If Field Is Nothing Then Continue For
                    If Field.Text Is Nothing Then Continue For
                    gInternalTextList.Add(Field.Text)
                Next Field
                Return gInternalTextList.ListString
            End If
        End Get
    End Property

    'Public Methods
    Public Sub AddPiece(pPiece As ChessPiece, pFieldName As String)
        Dim Field As ChessField
        Field = gInternalChessBoard(pFieldName)
        Field.Piece = pPiece
        Paint()
    End Sub

    Public Sub DeletePiece(pFieldName As String)
        Dim Field As ChessField
        Field = gInternalChessBoard(pFieldName)
        Field.Piece = Nothing
        Paint()
    End Sub

    Public Sub SaveAsJPG(pFileName As String)
        Dim Bitmap As Bitmap = Me.getBitMap()
        If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(pFileName) Then
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(pFileName)
        End If
        Bitmap.Save(pFileName, System.Drawing.Imaging.ImageFormat.Jpeg)
    End Sub

    Public Function getBitMap(Optional pIncludeBorderLabels As Boolean = True)
        Dim Bitmap = New Bitmap(picBoard.Image)
        Dim Rect As Rectangle
        If pIncludeBorderLabels Then
            Rect = New Rectangle(0, 0, CInt(LeftPos(10)), (9 * gFieldSize) + (2 * gBorderSize)) 'Cut the right Toolbar
        Else
            Rect = New Rectangle(gFieldSize, 0, CInt(LeftPos(9)), (8 * FieldSize) + (2 * gBorderSize)) 'Cut right Toolbar and BorderLabels
        End If
        'Return Bitmap
        Return Bitmap.Clone(Rect, Imaging.PixelFormat.Format24bppRgb)
    End Function

    Private Shadows Sub Paint()
        ' picBoard.Left = gFieldSize : picBoard.Width = Me.Width - picBoard.Left : picBoard.Height = Me.BoardSize
        If (Me.Width = 0 Or Me.Height = 0) Then Exit Sub
        Dim EmptyBoard As Bitmap = If(gColorBoard, frmImages.BoardColor.Image, frmImages.BoardBW.Image)
        gBitmap = New Bitmap(Me.picBoard.Width, Me.picBoard.Height)
        gBitmapGraphics = Graphics.FromImage(gBitmap)
        gBitmapGraphics.Clear(Me.picBoard.Parent.BackColor)
        gBitmapGraphics.DrawImage(EmptyBoard, New Rectangle(gFieldSize, 0, gfrmBoardSize, gfrmBoardSize))

        Me.PaintBorderLabels()
        Me.PaintColorInd(gInternalChessBoard.ActiveColor)
        Me.gSetupToolbar.Paint()

        'Draw Field Markers behind Pieces
        Me.PaintMarkerList(pInFront:=False)
        Me.PaintPieces(gInternalChessBoard)
        'Draw Plus, Minus, Circle and Rectangle on top of Pieces
        Me.PaintMarkerList(pInFront:=True)
        Me.PaintArrowList()
        Me.PaintTextList()

        Me.picBoard.Image = gBitmap
        Application.DoEvents()
    End Sub

    Private Sub PaintImage(pImage As Image, pLeft As Long, pTop As Long)
        PaintIcon(frmImages.BitMapToIcon(pImage), pLeft, pTop, PieceSize(pImage), PieceOffset(pImage))
    End Sub

    Public Sub PaintImage(pImage As Image, pLeft As Long, pTop As Long, pImageSize As Long, pOffset As Long)
        PaintIcon(frmImages.BitMapToIcon(pImage), pLeft, pTop, pImageSize, pOffset)
    End Sub

    Private Sub PaintIcon(pIcon As Icon, pLeft As Long, pTop As Long)
        PaintIcon(pIcon, pLeft, pTop, PieceSize(pIcon), PieceOffset(pIcon))
    End Sub

    Private Sub PaintIcon(pIcon As Icon, pLeft As Long, pTop As Long, pIconSize As Long, pOffset As Long)
        gBitmapGraphics.DrawIcon(pIcon, New Rectangle(New Point(pLeft + pOffset, pTop + pOffset), New Size(pIconSize, pIconSize)))
        DestroyIcon(pIcon.Handle)
    End Sub

    Public Sub SetDragPiece(pPiece As ChessPiece,
                            pLeft As Long, pTop As Long, pXOffset As Long, pYOffset As Long)
        Dim Image As Image
        gDragPiece = pPiece
        gDragOffset = New Point(pXOffset, pYOffset)
        Image = frmImages.getImage(gDragPiece.IconName)
        Me.SetDragImage(pLeft, pTop, PieceSize(Image), Image)
    End Sub

    Private Sub SetDragImage(pLeft As Long, pTop As Long, pSize As Long, pImage As Image)
        picDragImage.Image = pImage : picDragImage.Visible = True
        picDragImage.Width = pSize : picDragImage.Height = pSize
        picDragImage.Left = pLeft : picDragImage.Top = pTop
        picDragImage.Refresh()
    End Sub

    Public Sub SetDragText(pColor As String,
                           pLeft As Long, pTop As Long, pXOffset As Long, pYOffset As Long)
        Dim Image As Image
        gDragText = New Text(pColor)
        gDragOffset = New Point(pXOffset, pYOffset)
        Image = frmImages.getImage(gDragText.IconName)
        Me.SetDragImage(pLeft, pTop, PieceSize(Image), Image)
    End Sub

    Public Sub SetDragMarker(pSymbol As String,
                             pLeft As Long, pTop As Long, pXOffset As Long, pYOffset As Long)
        Dim Image As Image
        gDragMarker = New Marker(pSymbol)
        gDragOffset = New Point(pXOffset, pYOffset)
        Image = frmImages.getImage(gDragMarker.IconName)
        Me.SetDragImage(pLeft, pTop, PieceSize(Image), Image)
    End Sub

    Private Sub HideDragging()
        picDragImage.Visible = False
    End Sub

    Private Sub ResetDragging()
        picDragImage.Visible = False
        gDragMarker = Nothing
        gDragOffset = Nothing
        gDragPiece = Nothing
        gDragText = Nothing
        gFromField = Nothing
    End Sub

    Public Sub PlayMove(pHalfMove As PGNHalfMove)
        Dim BoardMove As BoardMove = pHalfMove.BoardMove(gInternalChessBoard)
        PlayMove(BoardMove)
        picPlayMovePiece.Visible = False
    End Sub

    Public Sub PlayMove(pFromFieldName As String, pToFieldName As String)
        Dim Piece As ChessPiece, BoardMove As BoardMove
        Piece = gInternalChessBoard(pFromFieldName).Piece
        BoardMove = New BoardMove(Piece, pFromFieldName, pToFieldName)
        PlayMove(BoardMove)
        Wait(2000)
        picPlayMovePiece.Visible = False
    End Sub

    Private Sub PlayMove(pBoardMove As BoardMove)
        Dim Image As Image, FromPoint As Point, ToPoint As Point, Dx As Single, Dy As Single
        Dim StartFEN As String = gInternalChessBoard.FEN

        Image = frmImages.getImage(pBoardMove.Piece.IconName)
        FromPoint = New Point(LeftPos(gInternalChessBoard(pBoardMove.FromFieldName).Column) + PieceOffset(Image),
                              TopPos(gInternalChessBoard(pBoardMove.FromFieldName).Row) + PieceOffset(Image))
        ToPoint = New Point(LeftPos(gInternalChessBoard(pBoardMove.ToFieldName).Column) + PieceOffset(Image),
                            TopPos(gInternalChessBoard(pBoardMove.ToFieldName).Row) + PieceOffset(Image))
        Me.SetPlayMovePiece(FromPoint.X, FromPoint.Y, PieceSize(Image), Image)

        'Remove movinge piece from original position on board
        gInternalChessBoard(pBoardMove.FromFieldName).Piece = Nothing
        Me.Paint()

        Dx = (ToPoint.X - picPlayMovePiece.Left) / 20
        Dy = (ToPoint.Y - picPlayMovePiece.Top) / 20
        For S As Long = 1 To 20
            Wait(20)
            picPlayMovePiece.Left = FromPoint.X + CInt(Dx * S)
            picPlayMovePiece.Top = FromPoint.Y + CInt(Dy * S)
            picPlayMovePiece.Refresh()
        Next S

        gInternalChessBoard.FEN = StartFEN
        gInternalChessBoard.PerformMove(pBoardMove)
        Paint()
    End Sub

    Private Sub SetPlayMovePiece(pLeft As Long, pTop As Long, pSize As Long, pImage As Image)
        picPlayMovePiece.Image = pImage : picPlayMovePiece.Visible = True
        picPlayMovePiece.Width = pSize : picPlayMovePiece.Height = pSize
        picPlayMovePiece.Left = pLeft : picPlayMovePiece.Top = pTop
        picPlayMovePiece.Refresh()
    End Sub

    Private Sub Wait(pMilliSec As Long)
        timClock.Interval = pMilliSec
        timClock.Start()
        While (timClock.Enabled)
            Application.DoEvents()
        End While
    End Sub

    Private Sub timClock_Tick(pSender As Object, pArgs As EventArgs) Handles timClock.Tick
        timClock.Stop()
    End Sub

    'Public Functions
    Public Function getPiece(pFieldName As String) As ChessPiece
        Return gInternalChessBoard(pFieldName).Piece
    End Function

    'Events
    Private Sub picBoard_MouseClick(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles picBoard.MouseClick
        Dim R As Long, C As Long
        R = Row(pArgs.Y) : C = Column(pArgs.X)
        If R = 8 And C = 9 Then
            If gInternalChessBoard.ActiveColor = BLACK Then
                gInternalChessBoard.ActiveColor = WHITE
            Else
                gInternalChessBoard.ActiveColor = BLACK
            End If
            RaiseEvent ActiveColorChanged(pSender, gInternalChessBoard.ActiveColor, gInternalChessBoard)
            Me.Paint()
        End If
    End Sub

    Private Sub picBoard_MouseDown(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles picBoard.MouseDown
        Dim Image As Image, R As Long, C As Long
        Call ResetDragging()

        gFENBeforeDragging = Me.FEN 'Save FEN before it is destroyed by a Move
        C = Column(pArgs.X) : R = Row(pArgs.Y)

        If R >= 1 And R <= 8 And C >= 1 And C <= 8 Then  'MouseDown at Board Area

            If pArgs.Button = MouseButtons.Right Then
                'Rightclick So raise event to compose RightClick Field menu
                RaiseEvent MouseRightClickOnField(pSender, pArgs, gInternalChessBoard(C, R).Name)
                Exit Sub
            End If

            If picArrow.BackColor <> SystemColors.ButtonFace Then
                gFromField = gInternalChessBoard(C, R)
                Exit Sub
            End If

            If gInternalChessBoard(C, R).Piece IsNot Nothing _
            And gInternalChessBoard(C, R).Marker IsNot Nothing Then
                'Field contains Piece and SignOrMarker
                If CloseToCenterOfField(C, R, pArgs) Then
                    gDragPiece = gInternalChessBoard(C, R).Piece 'Save a copy of the original Piece
                    gFromField = gInternalChessBoard(C, R)
                    gInternalChessBoard(C, R).Piece = Nothing 'Remove the dragging piece
                    Image = frmImages.getImage(gDragPiece.IconName)
                    SetDragImage(LeftPos(C), TopPos(R), PieceSize(Image), Image)
                    gDragOffset = New Point(pArgs.X - LeftPos(C) - PieceOffset(Image), pArgs.Y - TopPos(R) - PieceOffset(Image))
                    RaiseEvent ChessPieceStartMoving(gDragPiece, gFromField.Name, gInternalChessBoard)
                    Me.Paint() 'As last, contains DoEvents; processing f.i. quick MouseUp event
                    Exit Sub
                Else
                    gDragMarker = gInternalChessBoard(C, R).Marker
                    gFromField = gInternalChessBoard(C, R)
                    gInternalChessBoard(C, R).Marker = Nothing 'Remove the sign or marker
                    Image = frmImages.getImage(gDragMarker.IconName)
                    SetDragImage(LeftPos(C), TopPos(R), PieceSize(Image), Image)
                    gDragOffset = New Point(pArgs.X - LeftPos(C) - PieceOffset(Image), pArgs.Y - TopPos(R) - PieceOffset(Image))
                    Me.Paint() 'As last, contains DoEvents; processing f.i. quick MouseUp event
                    Exit Sub
                End If
            End If

            If gInternalChessBoard(C, R).Piece IsNot Nothing Then
                gDragPiece = gInternalChessBoard(C, R).Piece 'Save a copy of he original
                gFromField = gInternalChessBoard(C, R)
                gInternalChessBoard(C, R).Piece = Nothing 'Remove the dragging piece
                Image = frmImages.getImage(gDragPiece.IconName)
                SetDragImage(LeftPos(C), TopPos(R), PieceSize(Image), Image)
                gDragOffset = New Point(pArgs.X - LeftPos(C) - PieceOffset(Image), pArgs.Y - TopPos(R) - PieceOffset(Image))
                RaiseEvent ChessPieceStartMoving(gDragPiece, gFromField.Name, gInternalChessBoard)
                Me.Paint() 'As last, contains DoEvents; processing f.i. quick MouseUp event
                Exit Sub
            End If

            If gInternalChessBoard(C, R).Marker IsNot Nothing Then
                gDragMarker = gInternalChessBoard(C, R).Marker
                gFromField = gInternalChessBoard(C, R)
                gInternalChessBoard(C, R).Marker = Nothing 'Remove the sign or marker
                Image = frmImages.getImage(gDragMarker.IconName)
                SetDragImage(LeftPos(C), TopPos(R), PieceSize(Image), Image)
                gDragOffset = New Point(pArgs.X - LeftPos(C) - PieceOffset(Image), pArgs.Y - TopPos(R) - PieceOffset(Image))
                Me.Paint() 'As last, contains DoEvents; processing f.i. quick MouseUp event
                Exit Sub
            End If

        ElseIf C > 9 Then 'MouseDown at SetupToolbar Area
            Call ResetDragging()
            RaiseEvent SetupToolbarMouseDown(Me, pArgs) 'NB Column-Widths are same as gFieldSize
        End If
    End Sub

    Private Sub picBoard_MouseUp(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles picBoard.MouseUp
        Dim R As Long, C As Long, Captured As Boolean, PromotionPiece As ChessPiece = Nothing
        R = Row(pArgs.Y) : C = Column(pArgs.X)

        If gDragMarker IsNot Nothing Then
            If R > 0 And R < 9 And C > 0 And C < 9 Then
                gDragMarker.FieldName = gInternalChessBoard(C, R).Name
                gInternalChessBoard(C, R).Marker = gDragMarker
            End If
            Call HideDragging()
            RaiseEvent FieldMarkerListChanged(Me, Me.MarkerString)
            Call ResetDragging()
            Me.Paint()
            Exit Sub
        End If

        If picArrow.BackColor <> SystemColors.ButtonFace _
        AndAlso gFromField IsNot Nothing Then
            gInternalArrowList.Add(New Arrow(gSetupToolbar.gMarkerColor, gFromField.Name, gInternalChessBoard(C, R).Name))
            picArrow.BackColor = SystemColors.ButtonFace
            Call HideDragging()
            RaiseEvent ArrowListChanged(Me, gInternalArrowList.ListString)
            Call ResetDragging()
            Me.Paint()
            Exit Sub
        End If

        If gDragText IsNot Nothing Then
            If R < 1 Or R > 8 Or C < 1 Or C > 8 Then 'Mouse-up outside the chessboard
                Call HideDragging()
                Me.Paint()
                RaiseEvent TextListChanged(Me, Me.TextString)
                Call ResetDragging()
                Exit Sub
            Else
                frmAddText.TextColor = gDragText.Color
                frmAddText.ShowDialog(Me)
                If frmAddText.OKPressed = True Then
                    gDragText.Color = frmAddText.TextColor
                    gDragText.FieldName = gInternalChessBoard(C, R).Name
                    gDragText.Text = frmAddText.ColoureText
                    gInternalChessBoard(C, R).Text = gDragText
                    Call HideDragging()
                    Me.Paint()
                    RaiseEvent TextListChanged(Me, Me.TextString)
                    Call ResetDragging()
                    Exit Sub
                End If
            End If
            Call ResetDragging()
            Exit Sub
        End If

        If picDragImage.Visible = False Then
            Call ResetDragging()
            Exit Sub
        End If

        If gDragPiece IsNot Nothing Then
            If R < 1 Or R > 8 Or C < 1 Or C > 8 Then 'Mouse-up outside the chessboard
                Call HideDragging()
                Me.Paint()
                If gFromField IsNot Nothing Then
                    gInternalChessBoard.RemovePiece(gDragPiece, C, R)
                    RaiseEvent ChessPieceRemoved(pSender, gDragPiece, gFromField.Name, gInternalChessBoard)
                End If
                Call ResetDragging()
                Exit Sub
            End If

            Captured = (gInternalChessBoard(C, R).Piece IsNot Nothing)
            gInternalChessBoard(C, R).Piece = gDragPiece

            If gDragPiece.Type = PieceType.PAWN _
            And (R = 1 Or R = 8) Then
                frmPromotion.ShowDialog(gDragPiece.Color)
                PromotionPiece = frmPromotion.ChoosenPiece
            End If

            If gFromField Is Nothing Then
                Call HideDragging()
                Me.Paint()
                gInternalChessBoard.AddPiece(gDragPiece, C, R)
                RaiseEvent NewChessPiece(gDragPiece, gInternalChessBoard(C, R).Name,
                                             gInternalChessBoard)
                Call ResetDragging()
                Exit Sub

            ElseIf gFromField.Name = gInternalChessBoard(C, R).Name Then
                'FormField is same as ToField, so no move !! 
                Call HideDragging()
                Me.Paint()
                Call ResetDragging()
                Exit Sub

            Else 'FromField is filled in so a Move
                Call HideDragging()
                gInternalChessBoard.ActiveColor = gDragPiece.Color.Opponent

                'Select Case gFromField.Name 'Perhaps Rook or King was moved
                '    Case "a1" : gInternalChessBoard.WhiteLongCastlingAllowed = False
                '    Case "e1" : gInternalChessBoard.WhiteShortCastlingAllowed = False 
                '                gInternalChessBoard.WhiteLongCastlingAllowed = False
                '    Case "h1" : gInternalChessBoard.WhiteShortCastlingAllowed = False
                '    Case "a8" : gInternalChessBoard.BlackLongCastlingAllowed = False
                '    Case "e8" : gInternalChessBoard.BlackShortCastlingAllowed = False
                '                gInternalChessBoard.BlackLongCastlingAllowed = False
                '    Case "h8" : gInternalChessBoard.BlackShortCastlingAllowed = False
                'End Select

                Me.Paint()
                RaiseEvent ChessPieceMoved(gDragPiece, gFromField.Name, gInternalChessBoard(C, R).Name,
                                           gInternalChessBoard, Captured, PromotionPiece, FEN, FENBeforeDragging)
                Call ResetDragging()
                Exit Sub
            End If
            Exit Sub
        End If

        Call ResetDragging()
    End Sub

    Private Sub picBoard_MouseMove(pSender As Object, pArgs As System.Windows.Forms.MouseEventArgs) Handles picBoard.MouseMove
        If picDragImage.Visible = True Then
            picDragImage.Left = pArgs.X - gDragOffset.X
            picDragImage.Top = pArgs.Y - gDragOffset.Y
        End If
        'Save current position for inserting pieces by Keypress
        gMouseX = pArgs.X
        gMouseY = pArgs.Y
    End Sub

    Public Sub KeyEntered(pMsg As Message, pKeyData As Keys)
        Dim R As Long, C As Long
        If pKeyData > Int16.MaxValue Then Exit Sub
        If pKeyData >= Keys.F1 And pKeyData <= Keys.F24 Then Exit Sub

        If picDragImage.Visible = True Then Exit Sub
        C = Column(gMouseX) : R = Row(gMouseY)
        If R < 1 Or R > 8 Or C < 1 Or C > 8 Then Exit Sub

        Select Case UCase(Chr(pKeyData))
            Case King.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New King(ActiveColor), C, R)
            Case Queen.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New Queen(ActiveColor), C, R)
            Case Rook.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New Rook(ActiveColor), C, R)
            Case Bishop.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New Bishop(ActiveColor), C, R)
            Case Knight.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New Knight(ActiveColor), C, R)
            Case Pawn.KeyStroke(CurrentLanguage)
                gInternalChessBoard.AddPiece(New Pawn(ActiveColor), C, R)
            Case Else
                If pKeyData = Keys.Delete Then
                    gInternalChessBoard(C, R).Piece = Nothing
                Else
                    Exit Sub
                End If
        End Select

        Me.Paint()
        RaiseEvent NewChessPiece(gInternalChessBoard(C, R).Piece,
                                 gInternalChessBoard(C, R).Name,
                                 gInternalChessBoard)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef pMsg As Message, pKeyData As Keys) As Boolean
        Me.KeyEntered(pMsg, pKeyData)

        Return MyBase.ProcessCmdKey(pMsg, pKeyData)
    End Function

    Private Sub ctlBoard_SizeChanged(pSender As Object, pArgs As System.EventArgs) Handles Me.SizeChanged
        'Me.Height = Border + 8 Fields + Border + Label.Height ; (Label=Field)
        'Me.Width = Label + Border + 8 Fields + Border + ColorInd + 3 Fields; (ColorInd, Toolbar and Labels are Fields)
        gFieldSize = Math.Min((Me.Height * 120) / ((2 * 16) + (9 * 120)), (Me.Width * 120) / ((2 * 16) + (13 * 120)))
        gBorderSize = (gFieldSize * 16) / 120  'When Field in bitmap is 120, Border is 16 
        gfrmBoardSize = (gFieldSize * 8) + (gBorderSize * 2)

        gSetupToolbar.Left = (gFieldSize * 2) + gfrmBoardSize : gSetupToolbar.Top = 0
        gSetupToolbar.Width = gFieldSize * 3 : gSetupToolbar.Height = gfrmBoardSize + gFieldSize
        Paint()
    End Sub

    Public Sub New()
        gInternalChessBoard = New ChessBoard()
        gInternalMarkerList = New PGNMarkerList()
        gInternalArrowList = New PGNArrowList()
        gInternalTextList = New PGNTextList()

        ' This call is required by the designer.
        InitializeComponent()

        picBoard.Controls.Add(picDragImage) 'Needed for transparency of the DragPiece
        picBoard.Controls.Add(picPlayMovePiece) 'Needed for transparency of the DragPiece
    End Sub

    'Private Methods and Functions ===========================

    Private Function LeftPos(pColumn As Long) As Long
        If gSwitchedSides = True Then
            Return (9 - pColumn) * gFieldSize + gBorderSize 'BorderLabels are as width as a field
        Else
            Return pColumn * gFieldSize + gBorderSize 'BorderLabels are as width as a field
        End If
    End Function

    Private Function TopPos(pRow As Long) As Long 'pRow 8 is at the top
        If gSwitchedSides = True Then
            Return (pRow - 1) * gFieldSize + gBorderSize
        Else
            Return (8 - pRow) * gFieldSize + gBorderSize
        End If
    End Function

    Private Function Column(pLeft As Long) As Long
        If gSwitchedSides = True Then
            Return 9 - Int((pLeft - gBorderSize) / gFieldSize)
        Else
            Return Int((pLeft - gBorderSize) / gFieldSize)
        End If
    End Function

    Private Function Row(pTop As Long) As Long
        If gSwitchedSides = True Then
            Return Int((pTop - gBorderSize) / gFieldSize) + 1
        Else
            Return 8 - Int((pTop - gBorderSize) / gFieldSize)
        End If
    End Function

    Private Sub picArrow_Click(pSender As System.Object, pArgs As System.EventArgs) Handles picArrow.Click
        If picArrow.BackColor <> SystemColors.ButtonFace Then
            picArrow.BackColor = SystemColors.ButtonFace 'Reset
        Else
            picArrow.BackColor = SystemColors.ButtonShadow
        End If
    End Sub

    Private Sub picDragImage_MouseDown(pSender As Object, pArgs As MouseEventArgs) Handles picDragImage.MouseDown
        Call ResetDragging()
    End Sub

    Private Sub PaintBorderLabels()
        'Dim FieldSizeSize As Size, TextFont As Font
        'FieldSizeSize = New Size(gFieldSize, gFieldSize)
        'TextFont = New Font(Label8.Font.FontFamily.Name, gFieldSize / 3)
        'Label8.Top = gBorderSize + gFieldSize * 0
        'Label8.Size = FieldSizeSize
        'Label8.Font = TextFont

        Dim FontSize As Long = gFieldSize / 3
        Dim Font As New Drawing.Font("Microsoft Sans Serif", FontSize, FontStyle.Bold)
        Dim Point As Point
        Dim ColumnNames As String = "abcdefgh"
        For Row As Long = 8 To 1 Step -1
            Point = New Point((gFieldSize - FontSize) / 2, TopPos(Row) + ((gFieldSize - FontSize) / 2))
            gBitmapGraphics.DrawString(Strings.Format(Row), Font, New SolidBrush(Drawing.Color.Black), Point)
        Next Row
        For Column As Long = 1 To 8
            Point = New Point(LeftPos(Column) + ((gFieldSize - FontSize) / 2), TopPos(IIf(gSwitchedSides = True, 9, 0)) + ((gFieldSize - FontSize) / 2))
            gBitmapGraphics.DrawString(Mid(ColumnNames, Column, 1), Font, New SolidBrush(Drawing.Color.Black), Point)
        Next Column


        'gBitmapGraphics.DrawString("888", Font, New SolidBrush(Drawing.Color.Black), New Point(20, 20))

        'Dim OutlinePath As New Drawing2D.GraphicsPath
        'gBitmapGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        'gBitmapGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        'OutlinePath.AddString(pText.Text, Font.FontFamily, FontStyle.Bold, FontSize,
        '                      New Drawing.Point(LeftPos(Column(pField)) + (gFieldSize / 2), TopPos(Row(pField)) + (gFieldSize / 6)),
        '                      StringFormat.GenericTypographic)


        'gBitmapGraphics.FillPath(Brushes.Black, OutlinePath)
        'gBitmapGraphics.DrawPath(Pens.Black, OutlinePath)

    End Sub

    Private Sub PaintPieces(pBoard As ChessBoard)
        Dim Icon As Icon
        For Each Field As ChessField In pBoard
            If Field IsNot Nothing _
            AndAlso Field.Piece IsNot Nothing Then
                Icon = frmImages.getIcon(Field.Piece.IconName)
                PaintIcon(Icon, LeftPos(Field.Column), TopPos(Field.Row))
            End If
        Next Field
    End Sub

    Private Sub PaintColorInd(pColorInd As ChessColor)
        Dim SideLaneWidth As Long = gFieldSize - (gBorderSize * 2)
        Dim ColorIndSize As Long = SideLaneWidth / 2
        Dim HalfFieldSize As Long = gFieldSize / 2
        Dim ColorIndLeft As Long = (gBorderSize * 2) + (gFieldSize * 9)
        Dim ColorIndBorderSize As Long = gFieldSize / 20
        'Color of chessplayer to move
        Select Case pColorInd
            Case BLACK : gBitmapGraphics.FillRectangle(Brushes.Black, New Rectangle(ColorIndLeft + ((SideLaneWidth - ColorIndSize) / 2), TopPos(8) + HalfFieldSize - (ColorIndSize / 2), ColorIndSize, ColorIndSize))
            Case WHITE : gBitmapGraphics.FillRectangle(Brushes.White, New Rectangle(ColorIndLeft + ((SideLaneWidth - ColorIndSize) / 2), TopPos(8) + HalfFieldSize - (ColorIndSize / 2), ColorIndSize, ColorIndSize))
                gBitmapGraphics.DrawRectangle(New Pen(Brushes.Black, ColorIndBorderSize), New Rectangle(ColorIndLeft + ((SideLaneWidth - ColorIndSize) / 2), TopPos(8) + HalfFieldSize - (ColorIndSize / 2), ColorIndSize, ColorIndSize))
        End Select
    End Sub

    Private Sub PaintMarkerList(pInFront As Boolean)
        For Each Marker As Marker In New PGNMarkerList(Me.MarkerString)
            If Marker.InFront = pInFront Then
                DrawMarker(Marker.IconName, Marker.FieldName)
            End If
        Next Marker
    End Sub

    Private Sub DrawMarker(pIconName As String, pFieldName As String)
        Dim Icon As Icon
        Icon = frmImages.getIcon(pIconName)
        PaintIcon(Icon, LeftPos(Column(pFieldName)), TopPos(Row(pFieldName)))
    End Sub

    Private Sub PaintArrowList()
        For Each Arrow As Arrow In New PGNArrowList(Me.ArrowString)
            DrawArrow(Arrow.Brush, Arrow.FromFieldName, Arrow.ToFieldName)
        Next Arrow
    End Sub

    Public Sub DrawArrow(pColor As Drawing.Brush, pFromFieldName As String, pToFieldName As String)
        Dim FromColumn As Long, FromRow As Long, ToColumn As Long, ToRow As Long, Angle As Double
        Dim FromLeft As Long, FromTop As Long

        FromColumn = Column(pFromFieldName) : ToColumn = Column(pToFieldName)
        FromRow = Row(pFromFieldName) : ToRow = Row(pToFieldName)

        'Arrow leaving not from center of Field; but about halfway the edge
        Angle = Math.Atan2(FromRow - ToRow, ToColumn - FromColumn)
        FromLeft = LeftPos(FromColumn) + (gFieldSize / 2) + (Math.Cos(Angle) * (gFieldSize / 5))
        FromTop = TopPos(FromRow) + (gFieldSize / 2) + (Math.Sin(Angle) * (gFieldSize / 5))

        Me.DrawArrow(pColor, New Point(FromLeft, FromTop),
                             New Point(LeftPos(ToColumn) + (gFieldSize / 2), TopPos(ToRow) + (gFieldSize / 2)))
        Me.picBoard.Image = gBitmap
    End Sub

    Private Sub DrawArrow(pColor As Brush, pFromPoint As Point, pToPoint As Point)
        Dim ArrowSize As Long, ArrowWidth As Long
        Dim Slope As Double, CosSlope As Double, SinSlope As Double
        Dim BasePoint As Point, Points(6) As Point
        ArrowSize = gFieldSize / 2
        ArrowWidth = gFieldSize / 8

        Slope = Math.Atan2((pFromPoint.Y - pToPoint.Y), (pFromPoint.X - pToPoint.X))
        CosSlope = Math.Cos(Slope)
        SinSlope = Math.Sin(Slope)
        BasePoint = New Point(pToPoint.X + (ArrowSize * CosSlope),
                              pToPoint.Y + (ArrowSize * SinSlope))

        Points(0) = pToPoint
        Points(1) = New Point((BasePoint.X + (ArrowSize / 2 * SinSlope) + 0.5),
                              (BasePoint.Y - (ArrowSize / 2 * CosSlope) + 0.5))
        Points(2) = New Point((BasePoint.X + (ArrowWidth / 2 * SinSlope) + 0.5),
                              (BasePoint.Y - (ArrowWidth / 2 * CosSlope) + 0.5))
        Points(3) = New Point((pFromPoint.X + (ArrowWidth / 2 * SinSlope) + 0.5),
                              (pFromPoint.Y - (ArrowWidth / 2 * CosSlope) + 0.5))
        Points(4) = New Point((pFromPoint.X - (ArrowWidth / 2 * SinSlope) + 0.5),
                              (pFromPoint.Y + (ArrowWidth / 2 * CosSlope) + 0.5))
        Points(5) = New Point((BasePoint.X - (ArrowWidth / 2 * SinSlope) + 0.5),
                              (BasePoint.Y + (ArrowWidth / 2 * CosSlope) + 0.5))
        Points(6) = New Point((BasePoint.X - (ArrowSize / 2 * SinSlope) + 0.5),
                              (BasePoint.Y + (ArrowSize / 2 * CosSlope) + 0.5))

        gBitmapGraphics.FillPolygon(pColor, Points)
        gBitmapGraphics.DrawPolygon(New Pen(Drawing.Color.Black, (gFieldSize / 50)), Points)
    End Sub

    Private Sub PaintTextList()
        For Each Text As Text In New PGNTextList(Me.TextString)
            DrawText(Text, Text.FieldName)
        Next Text
    End Sub

    Private Sub DrawText(pText As Text, pFieldName As String)
        Dim FontSize As Long = gFieldSize / 2
        Dim Font As New Drawing.Font("Verdana", 40, FontStyle.Bold)
        Dim OutlinePath As New Drawing2D.GraphicsPath
        gBitmapGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        gBitmapGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        OutlinePath.AddString(pText.Text, Font.FontFamily, FontStyle.Bold, FontSize,
                              New Drawing.Point(LeftPos(Column(pFieldName)) + (gFieldSize / 2), TopPos(Row(pFieldName)) + (gFieldSize / 6)),
                              StringFormat.GenericTypographic)

        'This is only needed when Shadow with offset is needed
        'gBitmapGraphics.FillPath(Brushes.Violet, OutlinePath)
        'gBitmapGraphics.TranslateTransform(-5, -5)

        Select Case pText.Color
            Case "G" : gBitmapGraphics.FillPath(Brushes.Green, OutlinePath)
            Case "Y" : gBitmapGraphics.FillPath(Brushes.Yellow, OutlinePath)
            Case "R" : gBitmapGraphics.FillPath(Brushes.Red, OutlinePath)
            Case "B" : gBitmapGraphics.FillPath(Brushes.Blue, OutlinePath)
            Case "C" : gBitmapGraphics.FillPath(Brushes.Cyan, OutlinePath)
            Case "O" : gBitmapGraphics.FillPath(Brushes.Orange, OutlinePath)
        End Select
        gBitmapGraphics.DrawPath(Pens.Black, OutlinePath)
    End Sub

    'Private Functions
    Private Function PieceOffset(pImage As Image) As Long
        'Return FieldSize / 8
        Return (gFieldSize - PieceSize(pImage)) / 2
    End Function

    Private Function PieceSize(pIcon As Icon) As Long
        'Return FieldSize - (PieceOffset() * 2)
        Return gFieldSize * pIcon.Width / 120
    End Function

    Private Function PieceSize(pImage As Image) As Long
        'Return FieldSize - (PieceOffset() * 2)
        Return gFieldSize * pImage.Width / 120
    End Function

    Private Function CloseToCenterOfField(pColumn As Long, pRow As Long, pArgs As System.Windows.Forms.MouseEventArgs) As Boolean
        Dim CenterX As Long, CenterY As Long, Distance As Long
        CenterX = LeftPos(pColumn) + (gFieldSize / 2) : CenterY = TopPos(pRow) + (gFieldSize / 2)
        Distance = Math.Sqrt(((CenterX - pArgs.X) * (CenterX - pArgs.X)) + ((CenterY - pArgs.Y) * (CenterY - pArgs.Y)))
        Return Distance < (gFieldSize / 4)
    End Function

    Private Function Column(pFieldName As String) As Long
        Const Columns As String = "abcdefgh"
        Return InStr(Columns, Strings.Left(pFieldName, 1))
    End Function

    Private Function Row(pFieldName As String) As Long
        Const Rows As String = "12345678"
        Return InStr(Rows, Strings.Mid(pFieldName, 2, 1))
    End Function

    Private Function PieceOffset(pIcon As Icon) As Long
        Return (gFieldSize - PieceSize(pIcon)) / 2
    End Function

    Private Sub ctlBoard_Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        Call ResetDragging()
        gInternalChessBoard = Nothing
        gInternalMarkerList = Nothing
        gInternalArrowList = Nothing
        gInternalTextList = Nothing
        gBitmap.Dispose()
        gBitmapGraphics.Dispose()
    End Sub

    Private Sub picGreen_Click(sender As Object, e As EventArgs) Handles picGreen.Click
        gSetupToolbar.gMarkerColor = "G"
        picGreen.BackColor = SystemColors.ButtonShadow

        picYellow.BackColor = SystemColors.ButtonFace
        picRed.BackColor = SystemColors.ButtonFace
        picBlue.BackColor = SystemColors.ButtonFace
        picCyan.BackColor = SystemColors.ButtonFace
        picOrange.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub

    Private Sub picYellow_Click(sender As Object, e As EventArgs) Handles picYellow.Click
        gSetupToolbar.gMarkerColor = "Y"
        picYellow.BackColor = SystemColors.ButtonShadow

        picGreen.BackColor = SystemColors.ButtonFace
        picRed.BackColor = SystemColors.ButtonFace
        picBlue.BackColor = SystemColors.ButtonFace
        picCyan.BackColor = SystemColors.ButtonFace
        picOrange.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub

    Private Sub picRed_Click(sender As Object, e As EventArgs) Handles picRed.Click
        gSetupToolbar.gMarkerColor = "R"
        picRed.BackColor = SystemColors.ButtonShadow

        picGreen.BackColor = SystemColors.ButtonFace
        picYellow.BackColor = SystemColors.ButtonFace
        picBlue.BackColor = SystemColors.ButtonFace
        picCyan.BackColor = SystemColors.ButtonFace
        picOrange.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub

    Private Sub picBlue_Click(sender As Object, e As EventArgs) Handles picBlue.Click
        gSetupToolbar.gMarkerColor = "B"
        picBlue.BackColor = SystemColors.ButtonShadow

        picGreen.BackColor = SystemColors.ButtonFace
        picYellow.BackColor = SystemColors.ButtonFace
        picRed.BackColor = SystemColors.ButtonFace
        picCyan.BackColor = SystemColors.ButtonFace
        picOrange.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub

    Private Sub picCyan_Click(sender As Object, e As EventArgs) Handles picCyan.Click
        gSetupToolbar.gMarkerColor = "C"
        picCyan.BackColor = SystemColors.ButtonShadow

        picGreen.BackColor = SystemColors.ButtonFace
        picYellow.BackColor = SystemColors.ButtonFace
        picRed.BackColor = SystemColors.ButtonFace
        picBlue.BackColor = SystemColors.ButtonFace
        picOrange.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub

    Private Sub picOrange_Click(sender As Object, e As EventArgs) Handles picOrange.Click
        gSetupToolbar.gMarkerColor = "O"
        picOrange.BackColor = SystemColors.ButtonShadow

        picGreen.BackColor = SystemColors.ButtonFace
        picYellow.BackColor = SystemColors.ButtonFace
        picRed.BackColor = SystemColors.ButtonFace
        picBlue.BackColor = SystemColors.ButtonFace
        picCyan.BackColor = SystemColors.ButtonFace

        Me.Paint()
    End Sub
End Class
