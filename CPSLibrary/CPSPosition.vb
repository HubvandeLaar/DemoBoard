Option Explicit On

Imports System.Xml.Serialization

<XmlType("Position")>
Public Class CPSPosition

    <XmlAttribute()>
    Public Property Name As String

    <XmlAttribute()>
    Public Property ToPlay As String

    Public Property Description As String

    Public Property Arrows As New List(Of CPSArrow)

    Public Property Fields As New List(Of CPSField)

    Protected Overrides Sub Finalize()
        Me.Arrows = Nothing
        Me.Fields = Nothing

        MyBase.Finalize()
    End Sub

End Class
