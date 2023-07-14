Option Explicit On

Imports System.Xml.Serialization

<XmlType("Field")>
Public Class CPSField
    <XmlAttribute()>
    Public Name As String
    <XmlAttribute()>
    Public Piece As String
    <XmlAttribute()>
    Public Color As String
End Class
