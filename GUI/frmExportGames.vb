Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports ChessMessaging
Imports ChessMessaging.Messages
Imports PGNLibrary.PGNComment.BeforeOrAfterDiagram
Imports PDFLibrary
Imports PdfSharp.Drawing
Imports PGNLibrary
Imports PGNLibrary.PGNNAG
Imports PGNLibrary.PGNComment

Public Class frmExportGames
    Private gPGNFile As PGNFile
    Private gSelectedGames As New List(Of PGNGame)
    Private gPDFDocument As PDFGameDocument
    Private gDiagramIndex As Integer = 0
    Private gAfterCommentOrVariantChange As Boolean = True

    Public Overloads Sub ShowDialog(pPgnFile As PGNFile)
        gPGNFile = pPgnFile
        If gPGNFile Is Nothing _
            OrElse gPGNFile.PGNGames.Count = 0 Then
            gSelectedGames.Clear()
            Me.Hide()
            Exit Sub
        End If

        Me.ListGames(gPGNFile)

        Application.DoEvents()
        MyBase.ShowDialog()
    End Sub

    Sub ListGames(pPGNFile As PGNFile)
        Dim GameIndex As Long, GameText(5) As String '6 Columns
        Try
            Me.Text = pPGNFile.FullFileName

            lstGames.Items.Clear()

            For GameIndex = 0 To pPGNFile.PGNGames.Count - 1
                With pPGNFile.PGNGames(GameIndex)
                    GameText(0) = Str(GameIndex + 1)
                    GameText(1) = .Tags("White").Value
                    GameText(2) = .Tags("Black").Value
                    GameText(3) = .Tags("Result").Value
                    GameText(4) = .Tags("Date").Value
                    GameText(5) = .Tags("Title").Value

                    lstGames.Items.Add(New ListViewItem(GameText))
                End With
            Next GameIndex

            If lstGames.Items.Count = 1 Then
                lstGames.Items(0).Checked = True 'Select first game by default
            End If

            Application.DoEvents()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdSavePDF_Click(pSender As Object, pArgs As System.EventArgs) Handles cmdSavePDF.Click
        Try
            'Determine the selected games
            gSelectedGames.Clear()
            If lstGames.CheckedItems.Count > 0 Then
                For Each ListViewItem As ListViewItem In lstGames.CheckedItems
                    gSelectedGames.Add(gPGNFile.PGNGames(ListViewItem.Index))
                Next ListViewItem

                CreatePDFDocument()
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        Try
            gSelectedGames.Clear()
            Me.Hide()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        gPGNFile = Nothing
        gSelectedGames = Nothing

        MyBase.Finalize()
    End Sub

    Private Sub frmExportGames_KeyDown(pSender As Object, pArgs As KeyEventArgs) Handles Me.KeyDown
        If pArgs.Control = True _
        AndAlso pArgs.KeyCode = Keys.A Then ' Select all games
            For Each ListViewItem As ListViewItem In lstGames.Items
                ListViewItem.Checked = True
            Next ListViewItem
        End If
    End Sub

    Private Sub CreatePDFDocument()
        Try
            With dlgSaveFile
                .CheckFileExists = False
                .CheckPathExists = True
                .DefaultExt = ".pdf"
                .InitialDirectory = LastPDFFolder() ' CurrentLessonsFolder
                .FileName = gPGNFile.FileName.WithoutExtention & ".pdf"
                .Filter = "PDF file (*.pdf)|*.pdf"
                .ShowDialog()
                If .FileName = "" Then Exit Sub
            End With

            Me.UseWaitCursor = True

            gPDFDocument = New PDFGameDocument()
            gPDFDocument.PageHeader = gPGNFile.FileName.WithoutExtention
            gPDFDocument.InsertPageHeader()
            For Each Game As PGNGame In gSelectedGames
                ExportGame(Game)
            Next Game

            gPDFDocument.Save(dlgSaveFile.FileName)
            UpdateLastPDF(dlgSaveFile.FileName) 'Save last-used PDF file name

            gPDFDocument.Close()

            Me.UseWaitCursor = False
            Me.Hide()

        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Public Sub ExportGame(pPGNGame As PGNGame)

        ExportHeader(pPGNGame)

        gDiagramIndex = 0
        If pPGNGame.Tags("FEN").Value <> "" Then
            ExportDiagram(pPGNGame, pCommentFEN:=pPGNGame.FENComment)
        End If

        'Export MainLine
        gAfterCommentOrVariantChange = False
        For Each HalfMove As PGNHalfMove In pPGNGame.HalfMoves

            If HalfMove.VariantLevel <> 0 Then
                Continue For 'Already exported in SubVariant
            End If

            If gAfterCommentOrVariantChange Then
                gPDFDocument.NewLine() : gPDFDocument.NewLine()
                gAfterCommentOrVariantChange = False
            End If

            'CommentBefore
            If HalfMove.CommentBefore IsNot Nothing _
            AndAlso HalfMove.CommentBefore.Text <> "" Then
                ExportComment(CommentType.COMMENTBEFORE, HalfMove.CommentBefore, pPGNGame, HalfMove)
            End If

            If gAfterCommentOrVariantChange = True Then
                gPDFDocument.NewLine() : gPDFDocument.NewLine()
            End If

            'MoveText
            ExportMoveText(HalfMove)
            gAfterCommentOrVariantChange = False

            'CommentAfter
            If HalfMove.CommentAfter IsNot Nothing _
            AndAlso HalfMove.CommentAfter.Text <> "" Then
                ExportComment(CommentType.COMMENTAFTER, HalfMove.CommentAfter, pPGNGame, HalfMove)
            End If

            If HalfMove.SubVariants.Count > 0 Then
                If IsOneSimpleSubVariant(pPGNGame, HalfMove) Then
                    ExportSimpleSubVariant(pPGNGame, HalfMove)
                    Continue For
                Else
                    ExportSubVariants(pPGNGame, HalfMove.SubVariants)
                    Continue For
                End If
                gAfterCommentOrVariantChange = True
            End If

        Next HalfMove

        gPDFDocument.NewLine() : gPDFDocument.NewLine()
    End Sub

    Sub ExportComment(pCommentType As CommentType, pComment As PGNComment, pPGNGame As PGNGame, pHalfMove As PGNHalfMove)
        If pHalfMove.VariantLevel = 0 Then
            gPDFDocument.NewLine()
        End If

        If pComment.ContainsDiagram = False Then
            'No Diagram, just text
            gPDFDocument.InsertText(pComment.Text)
            gAfterCommentOrVariantChange = True
            Exit Sub
        End If

        'Text before Diagram
        Dim TextBefore As String = pComment.Text(BEFOREDIAGRAM)
        If TextBefore <> "" Then
            gPDFDocument.InsertText(TextBefore)
            gPDFDocument.NewLine()
        End If

        'Diagram
        If pPGNGame.Tags("Annotator").Value = "DemoBoard" Then
            'Analysed by Demoboard, better view of diagrams with 'Position Before' and arrow indicating the move in error
            If pCommentType = CommentType.COMMENTBEFORE Then
                ExportDiagram(pPGNGame, pPositionBefore:=True, pHalfMove.PreviousHalfMove, pComment, pHalfMove)
            Else
                'So pCommentType is COMMENTAFTER (Getest)
                Dim NextMoves As List(Of PGNHalfMove) = pHalfMove.NextHalfMoves
                Dim NextMove As PGNHalfMove = If(NextMoves Is Nothing, Nothing, NextMoves.First)
                ExportDiagram(pPGNGame, pPositionBefore:=True, pHalfMove, pComment, NextMove)
            End If
        Else
            'No DemoBoard Analysis, just use the common 'Position After'
            If pCommentType = CommentType.COMMENTBEFORE Then
                ExportDiagram(pPGNGame, pPositionBefore:=False, pHalfMove.PreviousHalfMove, pComment, pHalfMove.PreviousHalfMove)
            Else
                'So pCommentType is COMMENTAFTER (Getest)
                ExportDiagram(pPGNGame, pPositionBefore:=False, pHalfMove.PreviousHalfMove, pComment, pHalfMove.PreviousHalfMove)
            End If
        End If
        gAfterCommentOrVariantChange = False 'ExportDiagram already contains NewLine()

        'Text after Diagram
        Dim TextAfter As String = pComment.Text(AFTERDIAGRAM)
        If TextAfter <> "" Then
            gPDFDocument.InsertText(TextAfter)
            gAfterCommentOrVariantChange = True
        End If
    End Sub

    Sub ExportHeader(pPGNGame As PGNGame)
        Dim ELO As String, Site As String, EventDate As String
        With gPDFDocument
            .InsertLine(Color.Navy)

            .InsertChar("I", pFont:=KNSB(11))
            .Cursor.X = .Column(.CurrentCol).Left + 15 'Tab
            .InsertWord(pPGNGame.Tags("White").Value)
            ELO = pPGNGame.Tags("WhiteElo").Value
            If ELO <> "" Then
                .Cursor.X = .Column(.CurrentCol).Right - .WordLength("(" & ELO & ")") 'Right-Aligned
                .InsertWord("(" & ELO & ")")
            End If
            .NewLine()

            .InsertChar("J", pFont:=KNSB(11))
            .Cursor.X = .Column(.CurrentCol).Left + 15 'Tab
            .InsertWord(pPGNGame.Tags("Black").Value)
            ELO = pPGNGame.Tags("BlackElo").Value
            If ELO <> "" Then
                .Cursor.X = .Column(.CurrentCol).Right - .WordLength("(" & ELO & ")") 'Right-Aligned
                .InsertWord("(" & ELO & ")")
            End If
            .NewLine()

            Site = pPGNGame.Tags("Site").Value
            EventDate = pPGNGame.Tags("EventDate").Value
            If EventDate <> "" Then
                If Site = "" Then Site = "?"
                Site = Site & ", " & Strings.Left(EventDate, 4)
                .InsertChar(Chr(252), pFont:=New Font("Webdings", 11))
                .Cursor.X = .Column(.CurrentCol).Left + 15 'Tab
                .InsertWord(Site)
                .NewLine()
            End If

            .InsertLine(Color.Navy)
            .NewLine() 'Blank line after header
        End With
    End Sub

    ''' <summary>Returns True when it's only one variant with not any other subvariant</summary>
    Private Function IsOneSimpleSubVariant(pPGNGame As PGNGame, pParentHalfMove As PGNHalfMove) As Boolean
        If pParentHalfMove.SubVariants.Count <> 1 Then
            Return False
        End If

        Dim FirstHalfMove As PGNHalfMove = pParentHalfMove.SubVariants.First
        For I As Long = FirstHalfMove.Index To pPGNGame.HalfMoves.Count
            If pPGNGame.HalfMoves(I).VariantLevel <> FirstHalfMove.VariantLevel Then 'End of search
                Return True
            End If
            If pPGNGame.HalfMoves(I).VariantNumber <> FirstHalfMove.VariantNumber Then 'Multiple Variants
                Return False
            End If
            If pPGNGame.HalfMoves(I).SubVariants.Count > 0 Then 'SubVariant found in this Subvariant
                Return False
            End If
        Next
        Return True
    End Function

    Sub ExportSimpleSubVariant(pPGNGame As PGNGame, pParentHalfMove As PGNHalfMove)
        Dim FirstHalfMove As PGNHalfMove = pParentHalfMove.SubVariants.First
        Dim HalfMove As PGNHalfMove
        gPDFDocument.InsertChar("(")
        gAfterCommentOrVariantChange = True 'Force ExportMoveText to add MoveNr.
        For I As Long = FirstHalfMove.Index To pPGNGame.HalfMoves.Count
            HalfMove = pPGNGame.HalfMoves(I)

            If HalfMove.VariantLevel <> FirstHalfMove.VariantLevel Then 'End of search
                Exit For
            End If
            If HalfMove.VariantNumber <> FirstHalfMove.VariantNumber Then 'Multiple Variants
                MsgBox("Multiple Variants found in simple subvariant. Please check the PGN file.", MsgBoxStyle.Exclamation, "Export Error")
            End If

            'CommentBefore
            If HalfMove.CommentBefore IsNot Nothing _
            AndAlso HalfMove.CommentBefore.Text <> "" Then
                ExportComment(CommentType.COMMENTBEFORE, HalfMove.CommentBefore, pPGNGame, HalfMove)
            End If

            'MoveText
            ExportMoveText(HalfMove)
            gAfterCommentOrVariantChange = False

            'CommentAfter
            If HalfMove.CommentAfter IsNot Nothing _
            AndAlso HalfMove.CommentAfter.Text <> "" Then
                ExportComment(CommentType.COMMENTAFTER, HalfMove.CommentAfter, pPGNGame, HalfMove)
            End If

        Next I

        gPDFDocument.BackSpace() : gPDFDocument.InsertChar(")")
        gAfterCommentOrVariantChange = True
    End Sub

    Sub ExportSubVariants(pPGNGame As PGNGame, pSubVariants As List(Of PGNHalfMove))
        With gPDFDocument
            .Indent += 17 'Indent SubVariant
            For Each Subvariant As PGNHalfMove In pSubVariants
                .NewLine()
                .Cursor.X = .Column(.CurrentCol).Left + .Indent - 17 'Set Cursor to Previous start of column
                .Cursor.Y += 2 : .InsertUniCode(109, pFontName:="Wingdings") : .Cursor.Y -= 2  'White Dot
                .Cursor.X = .Column(.CurrentCol).Left + .Indent 'Reset Cursor to start of column for this variant
                ExportSubVariant(pPGNGame, Subvariant)
            Next Subvariant
            .Indent -= 17 'Indent SubVariant
            gAfterCommentOrVariantChange = True
        End With
    End Sub

    Sub ExportSubVariant(pPGNGame As PGNGame, pFirstHalfMove As PGNHalfMove)
        Dim HalfMove As PGNHalfMove
        gAfterCommentOrVariantChange = True 'Force ExportMoveText to add MoveNr.
        For I As Long = pFirstHalfMove.Index To pPGNGame.HalfMoves.Count
            HalfMove = pPGNGame.HalfMoves(I)

            If HalfMove.VariantLevel < pFirstHalfMove.VariantLevel Then 'End of search
                Exit Sub
            End If
            If HalfMove.VariantLevel <> pFirstHalfMove.VariantLevel _
            Or HalfMove.VariantNumber <> pFirstHalfMove.VariantNumber Then
                Continue For
            End If

            'CommentBefore
            If HalfMove.CommentBefore IsNot Nothing _
            AndAlso HalfMove.CommentBefore.Text <> "" Then
                ExportComment(CommentType.COMMENTBEFORE, HalfMove.CommentBefore, pPGNGame, HalfMove)
            End If

            'MoveText
            ExportMoveText(HalfMove)
            gAfterCommentOrVariantChange = False

            'CommentAfter
            If HalfMove.CommentAfter IsNot Nothing _
            AndAlso HalfMove.CommentAfter.Text <> "" Then
                ExportComment(CommentType.COMMENTAFTER, HalfMove.CommentAfter, pPGNGame, HalfMove)
            End If

            If HalfMove.SubVariants.Count > 0 Then
                If IsOneSimpleSubVariant(pPGNGame, HalfMove) Then
                    ExportSimpleSubVariant(pPGNGame, HalfMove)
                    Continue For
                Else
                    ExportSubVariants(pPGNGame, HalfMove.SubVariants)
                    Continue For
                End If
                gAfterCommentOrVariantChange = True
            End If
        Next I
    End Sub

    Sub ExportMoveText(pHalfMove As PGNHalfMove)
        With gPDFDocument
            If pHalfMove.NAGs.Count(NAGPrintPosition.BEFORE) > 0 Then
                Call ExportNAGs(pHalfMove, NAGPrintPosition.BEFORE, pBold:=(pHalfMove.VariantLevel = 0))
                .InsertSpace()
            End If

            If pHalfMove.Color = WHITE Then
                .InsertWord(pHalfMove.MoveNr & ". ", pBold:=(pHalfMove.VariantLevel = 0))
            ElseIf gAfterCommentOrVariantChange Then
                .InsertWord(pHalfMove.MoveNr & "... ", pBold:=(pHalfMove.VariantLevel = 0))
            End If
            If (.Cursor.X + .WordLength(pHalfMove.MoveText)) > .Column(.CurrentCol).Right Then
                .NewLine()
            End If
            For C As Integer = 1 To Len(pHalfMove.MoveText)
                Select Case Mid(pHalfMove.MoveText, C, 1)
                    Case "K", "D", "T", "L", "P",
                                     "K", "Q", "R", "B", "N"
                        .InsertChar(Mid(pHalfMove.MoveText, C, 1),
                                   pBold:=False,
                                   pFont:=KNSBFigurine(11))
                    Case Else
                        .InsertChar(Mid(pHalfMove.MoveText, C, 1),
                                   pBold:=(pHalfMove.VariantLevel = 0))
                End Select
            Next C

            If pHalfMove.NAGs.Count(NAGPrintPosition.AFTER) > 0 Then
                Call ExportNAGs(pHalfMove, NAGPrintPosition.AFTER, pBold:=(pHalfMove.VariantLevel = 0))
            End If

            .InsertSpace()
        End With
    End Sub

    Sub ExportNAGs(pHalfMove As PGNHalfMove, pPrintPosition As PGNNAG.NAGPrintPosition, Optional pBold As Boolean = False)
        For Each NAG As PGNNAG In pHalfMove.NAGs
            If NAG.PrintPosition = pPrintPosition Then
                Select Case NAG.Type
                    Case PGNNAG.NAGType.CODE
                        gPDFDocument.InsertUniCode(NAG.Code, pBold:=pBold, pFontName:=NAG.FontName)
                    Case PGNNAG.NAGType.TEXT
                        gPDFDocument.InsertText(NAG.Text, pBold:=pBold) 'Text not in Bold
                End Select
            End If
        Next NAG
    End Sub

    Sub ExportDiagram(pPGNGame As PGNGame, Optional pPositionBefore As Boolean = False,
                      Optional pHalfMoveFEN As PGNHalfMove = Nothing, Optional pCommentFEN As PGNComment = Nothing,
                      Optional pHalmoveBottomText As PGNHalfMove = Nothing)
        'Diagram
        Using Board As New ctlBoard() With {.BackColor = Color.White, .Width = 1060, .Height = 660}
            Board.FEN = pPGNGame.FEN(pHalfMoveFEN)
            If pCommentFEN IsNot Nothing Then
                Board.MarkerString = If(pCommentFEN.MarkerList Is Nothing, "", pCommentFEN.MarkerList.ListString)
                Board.ArrowString = If(pCommentFEN.ArrowList Is Nothing, "", pCommentFEN.ArrowList.ListString)
                Board.TextString = If(pCommentFEN.TextList Is Nothing, "", pCommentFEN.TextList.ListString)
            End If
            Dim Diagram As Bitmap = Board.getBitMap(True) 'Output is for Children

            Dim Size As Integer
            If pHalfMoveFEN Is Nothing _
            OrElse pHalfMoveFEN.VariantLevel = 0 Then
                Size = 13 'MainLine
            Else
                Size = 11 'SubVariant
            End If

            Dim BottomText As String
            If pHalfMoveFEN Is Nothing Then
                BottomText = MessageText("Starting Position")
            Else
                If pPositionBefore = True _
            And pHalmoveBottomText IsNot Nothing Then
                    BottomText = MessageText("Position Before", pHalmoveBottomText.MoveNrString(True) & " " & pHalmoveBottomText.MoveText(CurrentLanguage))
                Else
                    BottomText = MessageText("Position After", pHalfMoveFEN.MoveNrString(True) & " " & pHalfMoveFEN.MoveText(CurrentLanguage))
                End If
            End If

            gDiagramIndex += 1
            gPDFDocument.InsertDiagramImage(gDiagramIndex, Diagram, Size, BottomText)
        End Using
    End Sub

End Class