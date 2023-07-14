Imports ChessGlobals
Imports PGNLibrary
Imports PDFLibrary
Imports System.Drawing.Printing
Imports System.ComponentModel

Public Class frmExport

    Dim PGNFile As PGNFile

    Dim WithEvents PreviewDocument As PreviewDocument
    Dim NumberOfPages As Integer = 0

    Private ReadOnly Property DiagramsPerPage As Integer
        Get
            If lstLayout.SelectedItems.Count = 0 Then
                Return 12
            ElseIf lstLayout.SelectedItems.Count = 1 Then
                Return Val(lstLayout.SelectedItems(0).SubItems(0).Text)
            Else
                Return 12
            End If
        End Get
    End Property

    Private ReadOnly Property DiagramSize As String
        Get
            If lstLayout.SelectedItems.Count = 0 Then
                Return "Small"
            ElseIf lstLayout.SelectedItems.Count = 1 Then
                Return lstLayout.SelectedItems(0).SubItems(2).Text
            Else
                Return "Small"
            End If
        End Get
    End Property

    Private ReadOnly Property BottomText As String
        Get
            If lstLayout.SelectedItems.Count = 0 Then
                Return "None"
            ElseIf lstLayout.SelectedItems.Count = 1 Then
                Return lstLayout.SelectedItems(0).SubItems(3).Text
            Else
                Return "None"
            End If
        End Get
    End Property


    Public Overloads Sub ShowDialog(pPGNFile As PGNFile)
        Try
            PGNFile = pPGNFile
            If PGNFile Is Nothing _
            OrElse PGNFile.PGNGames.Count = 0 Then
                Me.Hide()
                Exit Sub
            End If

            txtPageHeader.Text = StripExtension(PGNFile.FileName)
            chkSideLabels.Checked = True
            cmbZoom.SelectedIndex = 1 'Page Width
            cmbFontSize.SelectedIndex = 1 'Fontsize 11
            lstLayout.Items(0).Selected = True 'Triggers CreatePreview()
            'CreatePreview()

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdPrevious_Click_1(pSender As Object, pArgs As EventArgs) Handles cmdPrevious.Click
        Dim PageNr As Integer = Val(lblPageNr.Text)
        If PageNr > 1 Then
            PageNr -= 1
        End If
        lblPageNr.Text = Format(PageNr) & "/" & Format(NumberOfPages)
        ppvPrintPreview.StartPage = (PageNr - 1)
        cmdPrevious.Enabled = (PageNr > 1)
        cmdNext.Enabled = (PageNr < NumberOfPages)
    End Sub

    Private Sub cmdNext_Click_1(pSender As Object, pArgs As EventArgs) Handles cmdNext.Click
        Dim PageNr As Integer = Val(lblPageNr.Text)
        If PageNr < NumberOfPages Then
            PageNr += 1
        End If
        lblPageNr.Text = Format(PageNr) & "/" & Format(NumberOfPages)
        ppvPrintPreview.StartPage = (PageNr - 1)
        cmdPrevious.Enabled = (PageNr > 1)
        cmdNext.Enabled = (PageNr < NumberOfPages)
    End Sub

    Private Sub cmbZoom_TextChanged(pSender As Object, pArgs As EventArgs) Handles cmbZoom.TextChanged
        ScalePrintPreview()
    End Sub

    Private Sub cmbFontSize_SelectedIndexChanged(pSender As Object, pArgs As EventArgs) Handles cmbFontSize.SelectedIndexChanged
        CreatePreview()
    End Sub

    Private Sub cmbFontSize_Validating(pSender As Object, pArgs As CancelEventArgs) Handles cmbFontSize.Validating
        If Val(cmbFontSize.Text) < 1 _
        Or Val(cmbFontSize.Text) > 128 Then
            pArgs.Cancel = True
            cmbFontSize.SelectAll()
        End If
    End Sub

    Private Sub lstLayout_SelectedIndexChanged(pSender As Object, pArgs As EventArgs) Handles lstLayout.SelectedIndexChanged
        If lstLayout.SelectedItems.Count = 0 Then
            'Setting another index, first clears the selectedItems and fires this event too
            Exit Sub
        End If
        CreatePreview()
    End Sub

    Private Sub cmdSavePDF_Click(pSender As Object, pArgs As EventArgs) Handles cmdSavePDF.Click
        Try
            With dlgSaveFile
                .CheckFileExists = False
                .CheckPathExists = True
                .DefaultExt = ".pdf"
                .InitialDirectory = CurrentLessonsFolder
                .FileName = StripExtension(PGNFile.FileName) & ".pdf"
                .Filter = "PDF file (*.pdf)|*.pdf"
                .ShowDialog()
                If .FileName = "" Then Exit Sub
            End With

            Me.UseWaitCursor = True

            Dim PdfDocument = New PDFLibrary.PDFDocument()
            PdfDocument.PageHeader = txtPageHeader.Text
            PdfDocument.DiagramsPerPage = Me.DiagramsPerPage
            For GameIndex As Integer = 0 To (PGNFile.PGNGames.Count - 1)
                Dim Board As New ctlBoard() With {.BackColor = Color.White, .Width = 1060, .Height = 660}
                With PGNFile.PGNGames(GameIndex)
                    Board.FEN = .FEN
                    Board.MarkerString = .HalfMoves.MarkerListString(Nothing)
                    Board.ArrowString = .HalfMoves.ArrowListString(Nothing)
                    Board.TextString = .HalfMoves.TextListString(Nothing)
                    Dim Diagram As Bitmap = Board.getBitMap(chkSideLabels.Checked)
                    Dim Bottom As String
                    Select Case Me.BottomText
                        Case "None" : Bottom = ""
                        Case "Title" : Bottom = .Tags.GetPGNTag("Title")
                        Case "Memo" : Bottom = .Tags.GetPGNTag("Memo")
                        Case Else : Throw New Exception("Unknown BottomText: " & Me.BottomText)
                    End Select
                    PdfDocument.InsertDiagram(GameIndex + 1, Diagram, Bottom, Val(cmbFontSize.Text))
                End With
            Next GameIndex

            PdfDocument.Save(dlgSaveFile.FileName)

            Me.UseWaitCursor = False
            Me.Hide()
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As Object, pArgs As EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Private Sub cmdUpdatePreview_Click(pSender As Object, pArgs As EventArgs) Handles cmdUpdatePreview.Click
        CreatePreview()
    End Sub

    Private Sub lstDiagramsPerPage_SelectedIndexChanged(pSender As Object, pArgs As EventArgs)
        CreatePreview()
    End Sub

    Private Sub chkSideLabels_CheckedChanged(pSender As Object, pArgs As EventArgs) Handles chkSideLabels.CheckedChanged
        CreatePreview()
    End Sub

    Private Sub ppvPrintPreview_Resize(pSender As Object, pArgs As EventArgs) Handles ppvPrintPreview.Resize
        ScalePrintPreview()
    End Sub

    Private Sub CreatePreview()
        NumberOfPages = 0
        PreviewDocument = New PreviewDocument()
        'PreviewDocument.PrintController = New PreviewPrintController() ' StandardPrintController() 'Hides progress dialog
        PreviewDocument.PageHeader = txtPageHeader.Text
        PreviewDocument.DiagramsPerPage = Me.DiagramsPerPage

        ppvPrintPreview.Document = PreviewDocument
        ppvPrintPreview.UseAntiAlias = True 'Smooth fonts
        ScalePrintPreview()
    End Sub

    Private Sub PreviewDocument_PrintPage(pSender As Object, pArgs As PrintPageEventArgs) Handles PreviewDocument.PrintPage
        Try
            NumberOfPages += 1
            'Debug.Print("Start creating Page " & NumberOfPages & " Or pageNr " & PreviewDocument.PageNumber)
            PreviewDocument.InsertPageHeader(PreviewDocument.PageHeader, pArgs.Graphics, pArgs.PageBounds)

            Dim LastExerciseIndexPreviousPage As Integer = PreviewDocument.PageNumber * DiagramsPerPage
            Dim ExerciseIndex As Integer
            For ExerciseIndex = 1 To DiagramsPerPage
                If LastExerciseIndexPreviousPage + ExerciseIndex > PGNFile.PGNGames.Count Then
                    Exit For
                End If
                Dim Board As New ctlBoard() With {.BackColor = Color.White, .Width = 1060, .Height = 660}
                With PGNFile.PGNGames(LastExerciseIndexPreviousPage + ExerciseIndex - 1) 'Zerobased
                    'Debug.Print("Inserting game (zb) " & (LastExerciseIndexPreviousPage + ExerciseIndex - 1))
                    Board.FEN = .FEN
                    Board.MarkerString = .HalfMoves.MarkerListString(Nothing)
                    Board.ArrowString = .HalfMoves.ArrowListString(Nothing)
                    Board.TextString = .HalfMoves.TextListString(Nothing)
                    Dim Diagram As Bitmap = Board.getBitMap(chkSideLabels.Checked)
                    Dim Bottom As String
                    Select Case Me.BottomText
                        Case "None" : Bottom = ""
                        Case "Title" : Bottom = .Tags.GetPGNTag("Title")
                        Case "Memo" : Bottom = .Tags.GetPGNTag("Memo")
                        Case Else : Throw New Exception("Unknown BottomText: " & Me.BottomText)
                    End Select
                    PreviewDocument.InsertDiagram(pArgs.Graphics, pArgs.PageBounds, ExerciseIndex, Diagram, Bottom, Val(cmbFontSize.Text))
                End With
            Next ExerciseIndex

            If LastExerciseIndexPreviousPage + ExerciseIndex > PGNFile.PGNGames.Count Then
                pArgs.HasMorePages = False
            Else
                pArgs.HasMorePages = True
            End If
            PreviewDocument.PageNumber += 1
        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub PreviewDocument_EndPrint(pSender As Object, pArgs As PrintEventArgs) Handles PreviewDocument.EndPrint
        lblPageNr.Text = "1/" & Format(NumberOfPages)
        cmdPrevious.Enabled = False
        cmdNext.Enabled = (1 < NumberOfPages)
    End Sub

    Private Sub ScalePrintPreview()
        Select Case cmbZoom.Text
            Case "Fit to Page"
                ppvPrintPreview.AutoZoom = True
                ppvPrintPreview.Zoom = Math.Min(PixelsToCentimeters(ppvPrintPreview.Width) / (21 + 2),
                                        PixelsToCentimeters(ppvPrintPreview.Height) / (30 + 2)) 'Fit to Page
            Case "Page Width"
                ppvPrintPreview.AutoZoom = True
                ppvPrintPreview.Zoom = PixelsToCentimeters(ppvPrintPreview.Width) / (21 + 2) ' Page Width
            Case Else
                ppvPrintPreview.AutoZoom = False
                If cmbZoom.Text Like "* %" Then ' "50 %"
                    If Val(cmbZoom.Text) > 10 Then
                        ppvPrintPreview.AutoZoom = True
                        ppvPrintPreview.Zoom = Val(cmbZoom.Text) / 100
                    End If
                End If
        End Select
    End Sub

    Private Function StripExtension(pFileName As String)
        Dim P As Long = InStrRev(pFileName, ".")
        If P > 1 Then
            Return Strings.Left(pFileName, P - 1)
        Else
            Return pFileName
        End If
    End Function

    Private Function PixelsToCentimeters(pPixels As Integer) As Double
        Return pPixels * 2.54 / 96
    End Function

End Class
