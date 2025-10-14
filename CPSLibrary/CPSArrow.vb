Option Explicit On

Imports System.Xml.Serialization

<XmlType("Arrow")>
Public Class CPSArrow

    <XmlAttribute()>
    Public Property StartPoint As String

    <XmlAttribute()>
    Public Property EndPoint As String

    <XmlAttribute()>
    Public Property Brush As String

    <XmlAttribute()>
    Public Property BorderBrush As String

    <XmlAttribute()>
    Public Property Stroke As String

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