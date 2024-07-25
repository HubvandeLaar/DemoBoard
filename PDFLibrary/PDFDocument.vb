Option Explicit On
Imports System.Drawing
Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Drawing.Layout
Imports PdfSharp.Pdf

Public Class PDFDocument

    Private PDFSharpDocument = New Pdf.PdfDocument()

    Public DiagramsPerPage As Integer = 12 ', 9, 6 or 4
    Public PageMarge As Long = CentimetersToPoints(1.5)   'cm
    Public ColumnMarge As Long = CentimetersToPoints(0.5) 'cm
    Public HeaderHeight As Long = CentimetersToPoints(1.5)   'cm
    Public FooterMarge As Long = CentimetersToPoints(1.5) 'cm

    Public Property PageHeader As String
        Set(pPageHeader As String)
            PDFSharpDocument.Info.Title = pPageHeader
        End Set
        Get
            Return PDFSharpDocument.Info.Title
        End Get
    End Property

    Public ReadOnly Property ExerciseWidth(pPDFPage As PdfPage) As Long
        Get
            Dim Columns As Integer = NumberOfColumns(DiagramsPerPage)
            Return (pPDFPage.Width.Point - (PageMarge * 2) - (ColumnMarge * (Columns - 1))) / Columns
        End Get
    End Property

    Public ReadOnly Property ExerciseHeight(pPDFPage As PdfPage) As Long
        Get
            Dim Rows As Integer = NumberOfRows(DiagramsPerPage)
            Return (pPDFPage.Height.Point - PageMarge - HeaderHeight - FooterMarge - (ColumnMarge * (Rows - 1))) / Rows
        End Get
    End Property

    Public ReadOnly Property ExerciseRect(pPDFPage As PdfPage, pGameIndex As Long) As XRect
        Get
            Dim PageIndex As Long = pGameIndex 'Could be on second page and be nr 15
            While (PageIndex > DiagramsPerPage)
                PageIndex -= DiagramsPerPage
            End While
            Dim PageRow = 1 + Int((PageIndex - 1) / NumberOfColumns(DiagramsPerPage))
            Dim PageCol = 1 + ((PageIndex - 1) Mod NumberOfColumns(DiagramsPerPage))
            Dim Left As Long = PageMarge + ((ExerciseWidth(pPDFPage) + ColumnMarge) * (PageCol - 1))
            Dim Top As Long = PageMarge + HeaderHeight + ((ExerciseHeight(pPDFPage) + ColumnMarge) * (PageRow - 1))

            Return New XRect(New XPoint(Left, Top),
                             New XSize(ExerciseWidth(pPDFPage), ExerciseHeight(pPDFPage)))
        End Get
    End Property

    Sub New()
        PDFSharpDocument.Info.Title = ""
        PDFSharpDocument.Info.Creator = "DemoBoard PDF Export"
        PDFSharpDocument.Info.CreationDate = Now()
    End Sub

    Sub InsertPageHeader(pPageHeader As String, pGraphics As XGraphics)
        Dim HeaderRect = New XRect(New XPoint(PageMarge, PageMarge), New XSize(pGraphics.PdfPage.Width.Point - (PageMarge * 2), PageMarge + HeaderHeight))
        Dim Formatter = New XTextFormatter(pGraphics)
        Formatter.DrawString(pPageHeader, New XFont("Calibri", 24), XBrushes.Black, HeaderRect, XStringFormats.TopLeft)
    End Sub

    Sub InsertDiagram(pGameIndex As Long, pDiagram As Bitmap, pBottomText As String, pFontSize As Double)
        Dim CurPage As PdfPage
        Dim Graphics As XGraphics
        If pGameIndex > (DiagramsPerPage * PDFSharpDocument.PageCount) Then
            CurPage = PDFSharpDocument.Pages.Add()
            Graphics = XGraphics.FromPdfPage(CurPage)
            InsertPageHeader(PageHeader, Graphics)
        Else
            CurPage = PDFSharpDocument.Pages(PDFSharpDocument.PageCount - 1)
            Graphics = XGraphics.FromPdfPage(CurPage)
        End If

        Dim Exercise As XRect = ExerciseRect(CurPage, ((pGameIndex - 1) Mod 12) + 1)

        'Create Bitmap Stream
        Dim Stream = New MemoryStream()
        pDiagram.Save(Stream, System.Drawing.Imaging.ImageFormat.Jpeg) 'save bitmap into memory stream In jpeg format

        'Insert Diagram with the right AspectRatio
        Dim DiagramRect As XRect = Exercise
        DiagramRect.Height = (Exercise.Width * pDiagram.Height) / pDiagram.Width
        Graphics.DrawImage(XImage.FromStream(Stream), DiagramRect)

        'Draw Bottom Text
        Dim TextRect = New XRect(Exercise.Left, Exercise.Top + DiagramRect.Height, Exercise.Width, Exercise.Height - DiagramRect.Height + ColumnMarge)
        Dim Formatter = New XTextFormatter(Graphics)
        Formatter.DrawString(pBottomText, New XFont("Calibri", pFontSize), XBrushes.Black, TextRect, XStringFormats.TopLeft)

        'Draw Exercise outline
        'Graphics.DrawRectangle(New XPen(XColors.Navy, 1), Exercise)

        Graphics.Dispose()
        Stream.Close()
    End Sub

    Sub Save(pFileName As String)
        PDFSharpDocument.Save(pFileName)
    End Sub

    Sub Close()
    End Sub

    Public Function CentimetersToPoints(pCentimeters As Single) As Long
        Return pCentimeters * 28.35
    End Function

    Protected Overrides Sub Finalize()
        Me.PDFSharpDocument = Nothing

        MyBase.Finalize()
    End Sub

    Private Function NumberOfRows(pDiagramsPerPage As Integer) As Integer
        Select Case pDiagramsPerPage
            Case 12 : Return 4
            Case 9 : Return 3
            Case 6 : Return 3
            Case 4 : Return 2
            Case Else
                Throw New Exception(String.Format("Diagrams per page not 12, 9, 6 or 4 but {0} specified.", DiagramsPerPage))
        End Select
    End Function

    Private Function NumberOfColumns(pDiagramsPerPage As Integer) As Integer
        Select Case pDiagramsPerPage
            Case 12 : Return 3
            Case 9 : Return 3
            Case 6 : Return 2
            Case 4 : Return 2
            Case Else
                Throw New Exception(String.Format("Diagrams per page not 12, 9, 6 or 4 but {0} specified.", DiagramsPerPage))
        End Select
    End Function

End Class
