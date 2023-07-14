Option Explicit On
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO

Public Class PreviewDocument
    Inherits Printing.PrintDocument

    Public PageHeader As String = ""
    Public PageNumber As Long = 0
    Public DiagramsPerPage As Integer = 12 ', 9, 6 or 4
    Public PageMarge As Long = CentimetersToPoints(1.5)   'cm
    Public ColumnMarge As Long = CentimetersToPoints(0.5) 'cm
    Public HeaderHeight As Long = CentimetersToPoints(1.5)   'cm
    Public FooterMarge As Long = CentimetersToPoints(1.5) 'cm

    Public ReadOnly Property ExerciseWidth(pPageWidth As Integer) As Long
        Get
            Dim Columns As Integer = NumberOfColumns(DiagramsPerPage)
            Return (pPageWidth - (PageMarge * 2) - (ColumnMarge * (Columns - 1))) / Columns
        End Get
    End Property

    Public ReadOnly Property ExerciseHeight(pPageHeight As Integer) As Long
        Get
            Dim Rows As Integer = NumberOfRows(DiagramsPerPage)
            Return (pPageHeight - PageMarge - HeaderHeight - FooterMarge - (ColumnMarge * (Rows - 1))) / Rows
        End Get
    End Property

    Public ReadOnly Property ExerciseRect(pPageBounds As Rectangle, pGameIndex As Long) As Rectangle
        Get
            Dim PageIndex As Long = pGameIndex 'Could be on second page and be nr 15
            While (PageIndex > DiagramsPerPage)
                PageIndex -= DiagramsPerPage
            End While
            Dim PageRow = 1 + Int((PageIndex - 1) / NumberOfColumns(DiagramsPerPage))
            Dim PageCol = 1 + ((PageIndex - 1) Mod NumberOfColumns(DiagramsPerPage))
            Dim Left As Long = PageMarge + ((ExerciseWidth(pPageBounds.Width) + ColumnMarge) * (PageCol - 1))
            Dim Top As Long = PageMarge + HeaderHeight + ((ExerciseHeight(pPageBounds.Height) + ColumnMarge) * (PageRow - 1))

            Return New Rectangle(New Point(Left, Top),
                                 New Size(ExerciseWidth(pPageBounds.Width), ExerciseHeight(pPageBounds.Height)))
        End Get
    End Property

    Sub InsertPageHeader(pPageHeader As String, pGraphics As Graphics, pPageBounds As Rectangle)
        Dim HeaderRect As Rectangle = New Rectangle(New Point(PageMarge, PageMarge), New Size(pPageBounds.Width - (PageMarge * 2), HeaderHeight))
        pGraphics.DrawString(pPageHeader, New Font("Calibri", 24), Brushes.Black, HeaderRect)
    End Sub

    Sub InsertDiagram(pGraphics As Graphics, pPageBounds As Rectangle, pGameIndex As Long, pDiagram As Bitmap, pBottomText As String, pFontSize As Double)
        Dim Exercise As Rectangle = ExerciseRect(pPageBounds, ((pGameIndex - 1) Mod DiagramsPerPage) + 1)

        'Create Bitmap Stream
        Dim Stream As MemoryStream = New MemoryStream()
        pDiagram.Save(Stream, System.Drawing.Imaging.ImageFormat.Jpeg) 'save bitmap into memory stream In jpeg format

        'Insert Diagram with the right AspectRatio
        Dim DiagramRect As Rectangle = Exercise
        DiagramRect.Height = (Exercise.Width * pDiagram.Height) / pDiagram.Width
        pGraphics.DrawImage(Image.FromStream(Stream), DiagramRect)

        'Draw Bottom Text
        Dim TextRect As Rectangle = New Rectangle(Exercise.Left, Exercise.Top + DiagramRect.Height, Exercise.Width, Exercise.Height - DiagramRect.Height + ColumnMarge)
        pGraphics.DrawString(pBottomText, New Font("Calibri", pFontSize), Brushes.Black, TextRect)

        'Draw Exercise outline
        'pGraphics.DrawRectangle(New Pen(Color.Navy, 1), Exercise)

        Stream.Close()
    End Sub

    Public Shared Function CentimetersToPoints(pCentimeters As Single) As Long
        Return pCentimeters * 39.38
    End Function

    Private Sub PreviewDoc_BeginPrint(sender As Object, e As PrintEventArgs) Handles Me.BeginPrint
        PageNumber = 0
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
