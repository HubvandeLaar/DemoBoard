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

    Public ReadOnly Property PGNColor()
        Get
            Select Case Brush
                Case "#FF008000" : Return "G"
                Case "#FFFF0000" : Return "R"
                Case "#FFFFFF00" : Return "Y"
                Case Else : Return ""
            End Select

        End Get
    End Property

End Class