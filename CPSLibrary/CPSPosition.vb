Option Explicit On

Imports System.Xml.Serialization

<XmlType("Position")>
Public Class CPSPosition
    <XmlAttribute()>
    Public Name As String
    <XmlAttribute()>
    Public ToPlay As String
    Public Description As String
    Public Arrows As New List(Of CPSArrow)
    Public Fields As New List(Of CPSField)

    Protected Overrides Sub Finalize()
        Me.Arrows = Nothing
        Me.Fields = Nothing

        MyBase.Finalize()
    End Sub
End Class
