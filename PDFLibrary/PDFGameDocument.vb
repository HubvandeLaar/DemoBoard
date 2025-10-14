Option Explicit On
Imports System.IO
Imports System.Drawing
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports PdfSharp.Drawing.Layout

Public Class PDFGameDocument

    Private PDFSharpDocument As New Pdf.PdfDocument()

    Public ReadOnly PageMarge As Long = CentimetersToPoints(1.5)   'cm
    Public ReadOnly ColumnMarge As Long = CentimetersToPoints(0.5) 'cm
    Public ReadOnly HeaderHeight As Long = CentimetersToPoints(1.5)   'cm
    Public ReadOnly FooterMarge As Long = CentimetersToPoints(1.5) 'cm

    Public Property PageNumber As Integer = 0
    Public Property CurrentCol As Integer = 1
    Public Property Indent As Long


    Public Column(2) As Rectangle
    Public Cursor As New Point(0, 0)

    Private gGraphics As XGraphics
    Private gCurrentPage As PdfPage
    Private gFormatter As XTextFormatter
    Private gFont As New XFont("Calibri", 11, XFontStyle.Regular)
    Private Const LineHeight As Double = 13.5 'Default line height for text
    Private Const SpaceWidth As Double = 2.2607421875 'Default space between words

    Public Property PageHeader As String
        Set(pPageHeader As String)
            PDFSharpDocument.Info.Title = pPageHeader
        End Set
        Get
            Return PDFSharpDocument.Info.Title
        End Get
    End Property

    Private ReadOnly Property ExerciseWidth(pPDFPage As PdfPage) As Long
        Get
            Return (pPDFPage.Width.Point - (PageMarge * 2) - ColumnMarge) / 2
        End Get
    End Property

    Private ReadOnly Property ExerciseHeight(pPDFPage As PdfPage) As Long
        Get
            Dim Rows As Integer = 7
            Return (pPDFPage.Height.Point - PageMarge - HeaderHeight - FooterMarge - (ColumnMarge * (Rows - 1))) / Rows
        End Get
    End Property

    Private ReadOnly Property ExerciseRect(pPDFPage As PdfPage, pGameIndex As Long) As XRect
        Get
            Dim Left As Long = PageMarge + ((ExerciseWidth(pPDFPage) + ColumnMarge))
            Dim Top As Long = PageMarge + HeaderHeight + ((ExerciseHeight(pPDFPage) + ColumnMarge))

            Return New XRect(New XPoint(Left, Top),
                             New XSize(ExerciseWidth(pPDFPage), ExerciseHeight(pPDFPage)))
        End Get
    End Property

    Public Sub New()
        PDFSharpDocument.Info.Title = ""
        PDFSharpDocument.Info.Creator = "DemoBoard PDF Export"
        PDFSharpDocument.Info.CreationDate = Now()

        NewPage()
    End Sub

    Public Sub NewLine()
        If Cursor.Y = Column(CurrentCol).Top _
        And Cursor.X = Column(CurrentCol).Left + Indent Then
            'No newline at the top of the column
            Exit Sub
        End If

        Cursor.Y += LineHeight
        If Cursor.Y > (gCurrentPage.Height.Point - PageMarge - FooterMarge) Then 'New page
            NewColumnOrPage()
        Else
            Cursor.X = Column(CurrentCol).Left + Indent
        End If
    End Sub

    Public Sub NewColumnOrPage()
        If CurrentCol = 1 Then 'Switch to second column
            CurrentCol = 2
            Cursor.X = Column(CurrentCol).Left + Indent
            Cursor.Y = Column(CurrentCol).Top
        Else 'New page
            NewPage()
        End If
    End Sub

    Public Sub NewPage()
        gCurrentPage = PDFSharpDocument.AddPage()
        PageNumber += 1
        gGraphics = XGraphics.FromPdfPage(gCurrentPage)
        gFormatter = New XTextFormatter(gGraphics)

        Dim ColumnWidth As Double = (gCurrentPage.Width.Point - (2 * PageMarge) - ColumnMarge) / 2
        Column(1) = New Rectangle(PageMarge,
                                  PageMarge + HeaderHeight,
                                  ColumnWidth,
                                  (gCurrentPage.Height.Point - PageMarge - HeaderHeight - FooterMarge))
        Column(2) = New Rectangle(PageMarge + ColumnMarge + ColumnWidth,
                                  PageMarge + HeaderHeight,
                                  ColumnWidth,
                                  (gCurrentPage.Height.Point - PageMarge - HeaderHeight - FooterMarge))

        CurrentCol = 1
        Cursor.X = Column(CurrentCol).Left + Indent
        Cursor.Y = Column(CurrentCol).Top

        InsertPageHeader()
    End Sub

    Public Sub InsertPageHeader()
        Dim HeaderRect As New XRect(New XPoint(PageMarge, PageMarge), New XSize(gGraphics.PdfPage.Width.Point - (PageMarge * 2), PageMarge + HeaderHeight))
        gFormatter.DrawString(PageHeader, New XFont("Calibri", 24), XBrushes.Black, HeaderRect, XStringFormats.TopLeft)
        Dim PageNumberRect As New XRect(New XPoint(PageMarge, gGraphics.PdfPage.Height.Point - PageMarge), New XSize(gGraphics.PdfPage.Width.Point - (PageMarge * 2), PageMarge))
        gFormatter.DrawString(Format(PageNumber), New XFont("Calibri", 12), XBrushes.Black, PageNumberRect, XStringFormats.TopLeft)
    End Sub

    Public Sub InsertText(pText As String, Optional pBold As Boolean = False, Optional pFontName As String = "Calibri", Optional pColor As Brush = Nothing)
        Dim Words() As String = pText.Replace(vbCrLf, vbLf).Split({CChar(" "), CChar(vbLf), CChar(vbCr)})
        For Each Word As String In Words
            InsertWord(Word, pBold, pFontName, pColor)
        Next Word
    End Sub

    Public Sub TestWord(pWord As String, Optional pBold As Boolean = False, Optional pFontName As String = "Calibri")
        If gFont.Bold <> If(pBold = True, XFontStyle.Bold, XFontStyle.Regular) Then
            gFont = New XFont(gFont.Name, gFont.Size, If(pBold = True, XFontStyle.Bold, XFontStyle.Regular))
        End If
        If gFont.Name <> pFontName Then
            gFont = New XFont(pFontName, gFont.Size, gFont.Style)
        End If

        If pWord = vbCr Or pWord = vbLf Then
            NewLine()
            Exit Sub
        End If

        Dim Width = gGraphics.MeasureString(pWord, gFont).Width
        If Cursor.X + Width > Column(CurrentCol).Right Then
            NewLine()
        End If

        If Width > Column(CurrentCol).Width Then 'Word is too long, split it
            MsgBox("Word '" & pWord & "' is too long for the column width. Please adjust this", MsgBoxStyle.Exclamation, "Word Too Long")
        End If
    End Sub

    Public Function WordLength(pWord As String, Optional pBold As Boolean = False, Optional pFontName As String = "Calibri") As Double
        If gFont.Bold <> If(pBold = True, XFontStyle.Bold, XFontStyle.Regular) Then
            gFont = New XFont(gFont.Name, gFont.Size, If(pBold = True, XFontStyle.Bold, XFontStyle.Regular))
        End If
        If gFont.Name <> pFontName Then
            gFont = New XFont(pFontName, gFont.Size, gFont.Style)
        End If
        Return gGraphics.MeasureString(pWord, gFont).Width
    End Function

    Public Sub InsertWord(pWord As String, Optional pBold As Boolean = False, Optional pFontName As String = "Calibri", Optional pColor As Brush = Nothing)
        If gFont.Bold <> If(pBold = True, XFontStyle.Bold, XFontStyle.Regular) Then
            gFont = New XFont(gFont.Name, gFont.Size, If(pBold = True, XFontStyle.Bold, XFontStyle.Regular))
        End If
        If gFont.Name <> pFontName Then
            gFont = New XFont(pFontName, gFont.Size, gFont.Style)
        End If

        If pWord = vbCr Or pWord = vbLf Then
            NewLine()
            Exit Sub
        End If

        Dim Width = gGraphics.MeasureString(pWord, gFont).Width
        If Cursor.X + Width > Column(CurrentCol).Right Then
            NewLine()
        End If

        If Width > Column(CurrentCol).Width Then 'Word is too long, split it
            MsgBox("Word '" & pWord & "' is too long for the column width. Please adjust this", MsgBoxStyle.Exclamation, "Word Too Long")
        End If

        gFormatter.DrawString(pWord,
                             gFont,
                             If(pColor Is Nothing, XBrushes.Black, Brush2XBrush(pColor)),
                             New XRect(Cursor.X, Cursor.Y, Width, LineHeight),
                             XStringFormats.TopLeft)
        Cursor.X += Width
        InsertSpace()
    End Sub

    Public Sub InsertSpace()
        Cursor.X += SpaceWidth 'Add some space after the word
    End Sub

    Public Sub BackSpace()
        Cursor.X -= SpaceWidth 'Go backward one space
    End Sub

    Public Sub InsertChar(pChar As String, Optional pBold As Boolean = False, Optional pFont As Font = Nothing, Optional pColor As Brush = Nothing)
        Dim Font As XFont = Font2XFont(pFont)
        If gFont.Bold <> If(pBold = True, XFontStyle.Bold, XFontStyle.Regular) Then
            gFont = New XFont(gFont.Name, gFont.Size, If(pBold = True, XFontStyle.Bold, XFontStyle.Regular))
        End If
        If pChar = vbCr Or pChar = vbLf Then
            NewLine()
            Exit Sub
        End If

        Dim Width = gGraphics.MeasureString(pChar, If(Font Is Nothing, gFont, Font)).Width
        gFormatter.DrawString(pChar,
                             If(Font Is Nothing, gFont, Font),
                             If(pColor Is Nothing, XBrushes.Black, Brush2XBrush(pColor)),
                             New XRect(Cursor.X, Cursor.Y, Width, LineHeight),
                             XStringFormats.TopLeft)
        Cursor.X += Width
    End Sub

    Public Sub InsertUniCode(pUnicode As Long, Optional pBold As Boolean = False, Optional pFontName As String = "Calibri", Optional pColor As Brush = Nothing)
        Dim Character As String = ChrW(pUnicode) 'Convert Unicode to character
        Cursor.Y -= 2
        InsertChar(Character, pBold, New Font(pFontName, CSng(gFont.Size + 2), If(pBold = True, FontStyle.Bold, FontStyle.Regular)), pColor)
        Cursor.Y += 2
    End Sub

    Public Sub InsertLine(pColor As Color)
        'Already Newline before at every componnent
        gGraphics.DrawLine(New XPen(Color2XColor(pColor), 2), Column(CurrentCol).Left + Indent, Cursor.Y + LineHeight / 2, Column(CurrentCol).Right, Cursor.Y + LineHeight / 2)
        Cursor.X = Column(CurrentCol).Right 'So NewLine() creates a NewLine...
        NewLine()
    End Sub

    Public Sub Save(pFileName As String)
        PDFSharpDocument.Save(pFileName)
    End Sub

    Public Sub Close()
        If PDFSharpDocument IsNot Nothing Then
            PDFSharpDocument.Close()
            PDFSharpDocument = Nothing
        End If
    End Sub

    Public Function CentimetersToPoints(pCentimeters As Single) As Long
        Return pCentimeters * 28.35
    End Function

    Protected Overrides Sub Finalize()
        Me.PDFSharpDocument = Nothing

        MyBase.Finalize()
    End Sub

    Sub InsertDiagramImage(pDiagramIndex As Long, pDiagram As Bitmap, pSize As Integer, pBottomText As String)
        gFont = New XFont("Calibri", 11)
        NewLine()

        'Does it all fit in current column
        If Cursor.Y + ((pSize + 2) * LineHeight) > Column(CurrentCol).Bottom Then
            NewColumnOrPage()
        End If

        'HeaderText
        Dim HeaderText As String = "Diagram " & pDiagramIndex.ToString()
        Dim HeaderLength As Double = WordLength(HeaderText)
        Cursor.X = Column(CurrentCol).Left + Indent + (Column(CurrentCol).Width - HeaderLength) / 2 'Center the header text
        InsertWord(HeaderText)
        NewLine()

        'Create Bitmap Stream
        Dim Stream As New MemoryStream()
        pDiagram.Save(Stream, System.Drawing.Imaging.ImageFormat.Jpeg) 'save bitmap into memory stream In jpeg format

        Dim DiagramRect As New XRect(Cursor.X, Cursor.Y, Column(CurrentCol).Width, pSize * LineHeight)
        DiagramRect.Width = (pDiagram.Width * DiagramRect.Height) / pDiagram.Height
        DiagramRect.X = Column(CurrentCol).Left + Indent + (Column(CurrentCol).Width - DiagramRect.Width) / 2 'Center the Diagram
        Cursor.Y = DiagramRect.Bottom
        gGraphics.DrawImage(XImage.FromStream(Stream), DiagramRect)
        Stream.Close()

        'Draw Bottom Text
        If pBottomText <> "" Then
            Dim BottomLength As Double = WordLength(pBottomText)
            Cursor.X = Column(CurrentCol).Left + Indent + (Column(CurrentCol).Width - BottomLength) / 2 'Center the BottomText
            InsertWord(pBottomText)
            NewLine()
        End If
        NewLine()
    End Sub

    Private Function Font2XFont(pFont As Font) As XFont
        Dim emSize As Single = pFont.Size '* 0.35277777777778 '1 point (computer) = 0.35277777777778 millimeter [mm]
        Return New XFont(New Font(pFont.FontFamily.Name, emSize, pFont.Style, GraphicsUnit.World))
    End Function

    Private Function Color2XColor(ByRef pColor As Color) As XColor
        Return XColor.FromArgb(pColor.A, pColor.R, pColor.G, pColor.B)
    End Function

    Private Function Brush2XBrush(ByRef pBrush As Brush) As XBrush
        If TypeOf pBrush Is SolidBrush Then
            Dim Solid As SolidBrush = CType(pBrush, SolidBrush)
            Return New XSolidBrush(Color2XColor(Solid.Color))
        Else
            'Other Brush types not implemented yet
            Return XBrushes.Black
        End If
    End Function

End Class
