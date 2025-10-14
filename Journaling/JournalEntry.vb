Imports System.Xml.Serialization

<XmlType()>
Public Class JournalEntry
    <XmlAttribute>
    Public Property ClassName As String
    <XmlAttribute>
    Public Property KeyValue As String
    <XmlElement>
    Public Property BeforeImage As String
    <XmlElement>
    Public Property AfterImage As String

    Public Sub New(pClassName As String, pKeyValue As String, pBeforeImage As String, pAfterImage As String)
        Me.ClassName = pClassName
        Me.KeyValue = pKeyValue
        Me.BeforeImage = pBeforeImage
        Me.AfterImage = pAfterImage
    End Sub

    Public Sub New()
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return "Class " & Me.ClassName & " Key " & Me.KeyValue & vbCrLf &
               "      Before " & Me.BeforeImage & vbCrLf &
               "      After  " & Me.AfterImage
    End Function

End Class
