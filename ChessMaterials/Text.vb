Option Explicit On

Imports System.Drawing
Imports System.Xml.Serialization

<XmlType()>
Public Class Text
    <XmlAttribute()>
    Public Color As String
    <XmlAttribute()>
    Public FieldName As String
    <XmlAttribute()>
    Public gText As String

    <XmlIgnore>
    Public Property Text As String
        Set(pText As String)
            If pText Like """*""" Then
                gText = Mid(pText, 2, pText.Length - 2)
            Else
                gText = pText
            End If
        End Set
        Get
            Return gText
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property IconName() As String
        Get
            Select Case Color
                Case "G" : Return "GText"
                Case "Y" : Return "YText"
                Case "R" : Return "RText"
                Case "B" : Return "BText"
                Case "C" : Return "CText"
                Case "O" : Return "OText"
                Case Else : Return ""
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property Brush As Brush
        Get
            Select Case Color
                Case "G" : Return Brushes.Green
                Case "Y" : Return Brushes.Yellow
                Case "R" : Return Brushes.Red
                Case "B" : Return Brushes.Blue
                Case "C" : Return Brushes.Cyan
                Case "O" : Return Brushes.Orange
                Case Else : Return Brushes.Black
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.Color = Mid(pPGNString, 1, 1)
            If pPGNString.Length < 3 Then Exit Property
            Me.FieldName = Mid(pPGNString, 2, 2)
            If pPGNString.Length < 5 Then Exit Property
            Me.Text = Trim(Mid(pPGNString, 4))
        End Set
        Get
            Return Me.Color & Me.FieldName & """" & Me.Text & """"
        End Get
    End Property

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New(pColor As String, pFieldName As String, pText As String)
        Me.Color = pColor
        Me.FieldName = pFieldName
        Me.Text = Trim(pText)
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Color & " " & Me.FieldName & " """ & Me.Text & """"
    End Function

End Class


