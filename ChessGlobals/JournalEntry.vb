Imports System.Xml.Serialization

<XmlType()>
Public Class JournalEntry
    <XmlAttribute>
    Public ClassName As String
    <XmlAttribute>
    Public KeyValue As String
    <XmlElement>
    Public BeforeImage As String
    <XmlElement>
    Public AfterImage As String

    Public Sub New(pClassName As String, pKeyValue As String, pBeforeImage As String, pAfterImage As String)
        Me.ClassName = pClassName
        Me.KeyValue = pKeyValue
        Me.BeforeImage = pBeforeImage
        Me.AfterImage = pAfterImage
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return "Class " & Me.ClassName & " Key " & Me.KeyValue & vbCrLf &
               "      Before " & Me.BeforeImage & vbCrLf &
               "      After  " & Me.AfterImage
    End Function

End Class
