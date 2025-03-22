Option Explicit On
Imports System.Xml.Serialization

<XmlType()>
Public Class Marker
    <XmlAttribute()>
    Public Symbol As String
    <XmlAttribute()>
    Public FieldName As String

    <XmlIgnore>
    Public ReadOnly Property IconName() As String
        Get
            Select Case Symbol
                Case "G" : Return "GMarker"
                Case "Y" : Return "YMarker"
                Case "R" : Return "RMarker"
                Case "B" : Return "BMarker"
                Case "C" : Return "CMarker"
                Case "O" : Return "OMarker"
                Case "+" : Return "PlusSign"
                Case "-" : Return "MinusSign"
                Case "o", "0" : Return "Circle"
                Case "#" : Return "Rectangle"
                Case "." : Return "Dot"
                Case "*" : Return "BlueStar"
                Case Else : Return ""
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property InFront() As Boolean
        Get
            Select Case Me.Symbol
                Case "G", "Y", "R", "B", "C", "O" 'Green, Yellow, Red, Blue, Cyan or Orange Rounded Rectangle
                    Return False
                Case Else
                    Return True
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.Symbol = Mid(pPGNString, 1, 1)
            If pPGNString.Length < 3 Then Exit Property
            Me.FieldName = Mid(pPGNString, 2, 2)
        End Set
        Get
            Return Me.Symbol & Me.FieldName
        End Get
    End Property

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New(pSymbol As String, pFieldName As String)
        Me.Symbol = pSymbol
        Me.FieldName = pFieldName
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Symbol & " " & Me.FieldName
    End Function

End Class
