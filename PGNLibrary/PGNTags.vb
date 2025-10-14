Option Explicit On

Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTags
    Inherits List(Of PGNTag)

    Default Public Overloads ReadOnly Property Item(pPGNTagKey) As PGNTag
        Get
            For Each Tag In Me
                If Tag.Key = pPGNTagKey Then Return Tag
            Next
            Return New PGNTag(pPGNTagKey, "")
        End Get
    End Property

    ''' <summary>Adds a New (or updates matching) PGNTag from a PGNString</summary>
    Overloads Function Add(pPGNString As String) As PGNTag
        Dim PGNTag As New PGNTag(pPGNString)
        If Me.Exists(Function(Item) Item.Key = PGNTag.Key) Then
            Me(PGNTag.Key).Value = PGNTag.Value
            Return Me(PGNTag.Key)
        Else
            Me.Add(PGNTag)
            Return PGNTag
        End If
    End Function

    ''' <summary>Adds a New (or updates matching) PGNTag from given Key and Value</summary>
    Overloads Function Add(pPGNTagKey As String, pPGNTagValue As String) As PGNTag
        Dim PGNTag As PGNTag
        For Each PGNTag In Me
            If PGNTag.Key = pPGNTagKey Then
                PGNTag.Value = pPGNTagValue
                Return PGNTag
            End If
        Next
        PGNTag = New PGNTag(pPGNTagKey, pPGNTagValue)
        Me.Add(PGNTag)
        Return PGNTag
    End Function

    Public Overloads Sub Clear()
        MyBase.Clear()

        Me.Add("Event", "")
        Me.Add("Site", "")
        Me.Add("Date", "")
        Me.Add("Round", "")
        Me.Add("White", "")
        Me.Add("Black", "")
        Me.Add("Result", "")

        Me.Add("Title", "")
        Me.Add("Memo", "")
    End Sub

    Public Sub New(pInitial As Boolean)
        If pInitial = True Then
            Me.Clear()
        End If
    End Sub

    Public Sub New()
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return String.Join(" ", Me)
    End Function

End Class
