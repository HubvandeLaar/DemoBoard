Option Explicit On

Imports System.Xml.Serialization

<XmlType("Arrow")>
Public Class CPSArrow
    <XmlAttribute()>
    Public StartPoint As String
    <XmlAttribute()>
    Public EndPoint As String
    <XmlAttribute()>
    Public Brush As String
    <XmlAttribute()>
    Public BorderBrush As String
    <XmlAttribute()>
    Public Stroke As String

    Public Function PGNColor()
        Select Case Brush
            Case "#FF008000" : Return "G"
            Case "#FFFF0000" : Return "R"
            Case "#FFFFFF00" : Return "Y"
            Case Else : Return ""
        End Select
    End Function

    Public Function FieldName(pPoint As String) As String
        Dim Values() As String = pPoint.Split(";")
        Dim Column As String, Row As String
        Select Case Val(Values(0))
            Case 0 To 9 : Column = "a"
            Case 10 To 19 : Column = "b"
            Case 20 To 29 : Column = "c"
            Case 30 To 39 : Column = "d"
            Case 40 To 49 : Column = "e"
            Case 50 To 59 : Column = "f"
            Case 60 To 69 : Column = "g"
            Case 70 To 79 : Column = "h"
            Case Else : Column = ""
        End Select
        Select Case Val(Values(1))
            Case 0 To 9 : Row = "8"
            Case 10 To 19 : Row = "7"
            Case 20 To 29 : Row = "6"
            Case 30 To 39 : Row = "5"
            Case 40 To 49 : Row = "4"
            Case 50 To 59 : Row = "3"
            Case 60 To 69 : Row = "2"
            Case 70 To 79 : Row = "1"
            Case Else : Row = ""
        End Select
        Return Column & Row
    End Function
End Class