Option Explicit On

Imports System.Xml.Serialization

<XmlType("Field")>
Public Class CPSField

    <XmlAttribute()>
    Public Property Name As String

    <XmlAttribute()>
    Public Property Piece As String

    <XmlAttribute()>
    Public Property Color As String

End Class
