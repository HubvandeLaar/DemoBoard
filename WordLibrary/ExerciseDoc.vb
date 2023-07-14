Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports System.Drawing

Public Class ExerciseDoc

    Private gDocument As New Word.Document
    Private gTable As Word.Table = Nothing

    Sub New()
        gDocument.Paragraphs.Add()
        gDocument.Paragraphs.Add()
        gDocument.Paragraphs.Add()
        gTable = gDocument.Content.Tables.Add(gDocument.Paragraphs(3).Range, 1, 3, WdDefaultTableBehavior.wdWord9TableBehavior, WdAutoFitBehavior.wdAutoFitFixed)
        gTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleNone
        gTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleNone
    End Sub

    Sub InsertTitle(pTitle As String)
        Dim Header As Word.Range
        Header = gDocument.Paragraphs(1).Range
        Header.Text = pTitle
        Header.Font.Size = 24
        Header.Font.Bold = True
        Header.Font.Name = "Calibri"
        Header.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
        Header.ParagraphFormat.SpaceAfter = 12 'Points
    End Sub

    Sub InsertDiagram(pFileName As String, pIndex As Long, Optional pBottomText As String = "")
        Dim Row As Long = Int((pIndex - 1) / 3) + 1
        Dim Column As Long = pIndex - ((Row - 1) * 3)
        Me.ExtendTable(pIndex)
        With gTable.Cell(Row, Column)
            .Range.InlineShapes.AddPicture(pFileName, False, True)
            .Range.InlineShapes(1).Height = 119 '4,21 cm
            .Range.InlineShapes(1).Width = 141  '5 cm
            .Range.InsertAfter(vbCrLf & pBottomText)
        End With
    End Sub

    Sub InsertText(pIndex As Long, pText As String)
        Dim Row As Long = Int((pIndex - 1) / 3) + 1
        Dim Column As Long = pIndex - ((Row - 1) * 3)
        Me.ExtendTable(pIndex)
        With gTable.Cell(Row, Column)
            .Range.InsertAfter(vbCrLf & pText)
        End With
    End Sub

    Sub InsertDoubleText(pIndex As Long, pText As String)
        Dim Row As Long = Int((pIndex - 1) / 3) + 1
        Dim Column As Long = pIndex - ((Row - 1) * 3)
        Me.ExtendTable(pIndex)
        gTable.Rows.Add() 'To save the three column layout
        If Column = 3 Then
            Throw New IndexOutOfRangeException("Column: " & Format(Column) & " is out of range")
        End If
        gTable.Cell(Row, Column).Merge(gTable.Cell(Row, Column + 1))
        With gTable.Cell(Row, Column)
            .Range.InsertAfter(vbCrLf & pText)
        End With
    End Sub

    Sub ExtendTable(pIndex As Long)
        While ((gTable.Rows.Count * 3) < pIndex)
            gTable.Rows.Add()
        End While
    End Sub

    Sub Save(pFileName As String)
        gDocument.SaveAs2(pFileName, WdSaveFormat.wdFormatDocument97)
    End Sub

    Sub Close()
        gDocument.Close()
    End Sub

    Protected Overrides Sub Finalize()
        'MSWord = Nothing
        gDocument = Nothing

        MyBase.Finalize()
    End Sub

End Class
