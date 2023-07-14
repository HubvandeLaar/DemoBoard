Option Explicit On

Imports ChessGlobals
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTag

    <XmlAttribute()>
    Public Key As String
    <XmlAttribute()>
    Public Value As String

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Dim P1 As Long, P2 As Long
            P1 = InStr(pPGNString, """")
            If P1 <= 3 Then
                Me.Key = ""
                Me.Value = pPGNString
                Exit Property
            End If
            Me.Key = Mid$(pPGNString, 2, P1 - 3)

            P2 = InStrRev(pPGNString, """", Compare:=CompareMethod.Binary)
            If P1 > 0 And P2 > P1 Then
                Me.Value = Mid$(pPGNString, P1 + 1, P2 - P1 - 1)
            Else
                Me.Value = Mid$(pPGNString, P1 + 1) 'Missing quote at the end
            End If
        End Set
        Get
            If Me.Key = "" And Me.Value = "" Then
                Return ""
            Else
                Return "[" & Key & " """ & Value & """]"
            End If
        End Get
    End Property

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New(pKey As String, pValue As String)
        Me.Key = pKey
        Me.Value = pValue
    End Sub

    Public Sub New()
        'Needed for serialization
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Key & " " & Me.Value
    End Function

End Class
