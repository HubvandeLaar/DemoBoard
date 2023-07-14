Option Explicit On

Imports System.Drawing
Imports System.Xml.Serialization

Public Class Arrow
    <XmlAttribute()>
    Public Color As String
    <XmlAttribute()>
    Public FromFieldName As String
    <XmlAttribute()>
    Public ToFieldName As String

    <XmlIgnore>
    Public ReadOnly Property IconName() As String
        Get
            Select Case Color
                Case "G" : Return "GArrow"
                Case "Y" : Return "YArrow"
                Case "R" : Return "RArrow"
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
                Case Else : Return Brushes.Black
            End Select
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.Color = Mid(pPGNString, 1, 1)
            If pPGNString.Length < 3 Then Exit Property
            Me.FromFieldName = Mid(pPGNString, 2, 2)
            If pPGNString.Length < 5 Then Exit Property
            Me.ToFieldName = Mid(pPGNString, 4, 2)
        End Set
        Get
            Return Me.Color & Me.FromFieldName & Me.ToFieldName
        End Get
    End Property

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New(pColor As String, pFromFieldName As String, pToFieldName As String)
        Me.Color = pColor
        Me.FromFieldName = pFromFieldName
        Me.ToFieldName = pToFieldName
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Color & " " & Me.FromFieldName & "-" & Me.ToFieldName
    End Function

End Class
