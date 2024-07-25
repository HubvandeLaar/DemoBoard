Option Explicit On

Imports System.Xml.Serialization

<XmlRoot("Positions")>
Public Class CPSPositions
    <XmlAttribute()>
    Public Name As String
    <XmlAttribute()>
    Public Institute As String
    <XmlAttribute()>
    Public Theme As String
    <XmlAttribute()>
    Public Student As String
    <XmlElement("Position")>
    Public PositionList As New List(Of CPSPosition)

    Public Sub Serialize(pFileName As String)
        Dim XMLNameSpace As New XmlSerializerNamespaces()
        XMLNameSpace.Add("", "")
        Dim XMLSerializer As New XmlSerializer(GetType(CPSPositions))
        Dim Writer As New System.IO.StreamWriter(pFileName)
        XMLSerializer.Serialize(Writer, Me, XMLNameSpace)
        Writer.Close()
    End Sub

    Public Shared Function DeSerialize(pFileName As String) As CPSPositions
        Dim XMLNameSpace As New XmlSerializerNamespaces()
        XMLNameSpace.Add("", "")
        Dim XMLSerializer As New XmlSerializer(GetType(CPSPositions))
        Dim Reader As New System.IO.StreamReader(pFileName)
        Dim Objects As CPSPositions = CType(XMLSerializer.Deserialize(Reader), CPSPositions)
        Reader.Close()
        Return Objects
    End Function

    Protected Overrides Sub Finalize()
        Me.PositionList = Nothing

        MyBase.Finalize()
    End Sub
End Class

