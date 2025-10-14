Option Explicit On

Imports ChessMessaging
Imports PGNLibrary
Imports PDFLibrary
Imports System.Drawing.Printing
Imports System.ComponentModel
Imports ChessGlobals

Public Class frmExportDiagrams

    Dim gPGNFile As PGNFile

    Dim WithEvents gPreviewDocument As PreviewDocument

    Private Property NumberOfPages As Integer = 0

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
            gPGNFile = pPGNFile
            If gPGNFile Is Nothing _
            OrElse gPGNFile.PGNGames.Count = 0 Then
                Me.Hide()
                Exit Sub
            End If

            txtPageHeader.Text = gPGNFile.FileName.WithoutExtention
            chkSideLabels.Checked = True
            cmbZoom.SelectedIndex = 1 'Page Width
            cmbFontSize.SelectedIndex = 1 'Fontsize 11
            lstLayout.Items(0).Selected = True 'Triggers CreatePreview()
            'CreatePreview()

            Application.DoEvents()
            MyBase.ShowDialog()

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
                .InitialDirectory = LastPDFFolder() ' CurrentLessonsFolder
                .FileName = gPGNFile.FileName.WithoutExtention & ".pdf"
                .Filter = "PDF file (*.pdf)|*.pdf"
                .ShowDialog()
                If .FileName = "" Then Exit Sub
            End With

            Me.UseWaitCursor = True

            Dim PdfDocument As New PDFDiagramsDocument()
            PdfDocument.PageHeader = txtPageHeader.Text
            PdfDocument.DiagramsPerPage = Me.DiagramsPerPage
            For GameIndex As Integer = 0 To (gPGNFile.PGNGames.Count - 1)
                Using Board As New ctlBoard() With {.BackColor = Color.White, .Width = 1060, .Height = 660}
                    With gPGNFile.PGNGames(GameIndex)
                        Board.FEN = .FEN
                        Board.MarkerString = .HalfMoves.MarkerListString(Nothing)
                        Board.ArrowString = .HalfMoves.ArrowListString(Nothing)
                        Board.TextString = .HalfMoves.TextListString(Nothing)
                        Dim Diagram As Bitmap = Board.getBitMap(chkSideLabels.Checked)
                        Dim BottomText As String
                        Select Case Me.BottomText
                            Case "None" : BottomText = ""
                            Case "Title" : BottomText = .Tags("Title").Value
                            Case "Memo" : BottomText = .Tags("Memo").Value
                            Case Else : Throw New ArgumentOutOfRangeException("Unknown BottomText: " & Me.BottomText)
                        End Select
                        PdfDocument.InsertDiagram(GameIndex + 1, Diagram, BottomText, Val(cmbFontSize.Text))
                    End With
                End Using
            Next GameIndex

            PdfDocument.Save(dlgSaveFile.FileName)
            UpdateLastPDF(dlgSaveFile.FileName) 'Save last-used PDF file name

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
        gPreviewDocument = New PreviewDocument()
        'PreviewDocument.PrintController = New PreviewPrintController() ' StandardPrintController() 'Hides progress dialog
        gPreviewDocument.PageHeader = txtPageHeader.Text
        gPreviewDocument.DiagramsPerPage = Me.DiagramsPerPage

        ppvPrintPreview.Document = gPreviewDocument
        ppvPrintPreview.UseAntiAlias = True 'Smooth fonts
        ScalePrintPreview()
    End Sub

    Private Sub gPreviewDocument_PrintPage(pSender As Object, pArgs As PrintPageEventArgs) Handles gPreviewDocument.PrintPage
        Try
            NumberOfPages += 1
            gPreviewDocument.InsertPageHeader(gPreviewDocument.PageHeader, pArgs.Graphics, pArgs.PageBounds)

            Dim LastExerciseIndexPreviousPage As Integer = gPreviewDocument.PageNumber * DiagramsPerPage
            Dim ExerciseIndex As Integer
            For ExerciseIndex = 1 To DiagramsPerPage
                If LastExerciseIndexPreviousPage + ExerciseIndex > gPGNFile.PGNGames.Count Then
                    Exit For
                End If
                Using Board As New ctlBoard() With {.BackColor = Color.White, .Width = 1060, .Height = 660}
                    With gPGNFile.PGNGames(LastExerciseIndexPreviousPage + ExerciseIndex - 1) 'Zerobased
                        Board.FEN = .FEN
                        Board.MarkerString = .HalfMoves.MarkerListString(Nothing)
                        Board.ArrowString = .HalfMoves.ArrowListString(Nothing)
                        Board.TextString = .HalfMoves.TextListString(Nothing)
                        Dim Diagram As Bitmap = Board.getBitMap(chkSideLabels.Checked)
                        Dim Bottom As String
                        Select Case Me.BottomText
                            Case "None" : Bottom = ""
                            Case "Title" : Bottom = .Tags("Title").Value
                            Case "Memo" : Bottom = .Tags("Memo").Value
                            Case Else : Throw New ArgumentOutOfRangeException("Unknown BottomText: " & Me.BottomText)
                        End Select
                        gPreviewDocument.InsertDiagram(pArgs.Graphics, pArgs.PageBounds, ExerciseIndex, Diagram, Bottom, Val(cmbFontSize.Text))
                    End With
                End Using
            Next ExerciseIndex

            If LastExerciseIndexPreviousPage + ExerciseIndex > gPGNFile.PGNGames.Count Then
                pArgs.HasMorePages = False
            Else
                pArgs.HasMorePages = True
            End If
            gPreviewDocument.PageNumber += 1

        Catch pException As Exception
            Cursor = Cursors.Default
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub gPreviewDocument_EndPrint(pSender As Object, pArgs As PrintEventArgs) Handles gPreviewDocument.EndPrint
        lblPageNr.Text = "1/" & Format(NumberOfPages)
        cmdPrevious.Enabled = False
        cmdNext.Enabled = (1 < NumberOfPages)
    End Sub

    Private Sub ScalePrintPreview()
        Select Case cmbZoom.Text
            Case "Fit to Page"
                ppvPrintPreview.AutoZoom = True
                ppvPrintPreview.Zoom = Math.Min(ppvPrintPreview.Width.Centimeters / (21 + 2),
                                        ppvPrintPreview.Height.Centimeters / (30 + 2)) 'Fit to Page
            Case "Page Width"
                ppvPrintPreview.AutoZoom = True
                ppvPrintPreview.Zoom = ppvPrintPreview.Width.Centimeters / (21 + 2) ' Page Width
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


    Protected Overrides Sub Finalize()
        gPGNFile = Nothing
        gPreviewDocument = Nothing

        MyBase.Finalize()
    End Sub

End Class
