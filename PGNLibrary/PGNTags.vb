Option Explicit On

Imports ChessGlobals
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTags
    Inherits List(Of PGNTag)

    Default Public Overloads ReadOnly Property Item(pPGNTagKey) As PGNTag
        Get
            For Each PGNTag In Me
                If PGNTag.Key = pPGNTagKey Then
                    Return PGNTag
                End If
            Next
            Return Nothing
        End Get
    End Property

    Function GetPGNTag(pKey As String) As String
        For Each Tag As PGNTag In Me
            If Tag.Key = pKey Then Return Tag.Value
        Next
        Return ""
    End Function

    Overloads Function Add(pPGNTag As String) As PGNTag
        Dim PGNTag As New PGNTag(pPGNTag)
        If Me.Exists(Function(Item) Item.Key = PGNTag.Key) Then
            Me(PGNTag.Key).Value = PGNTag.Value
            Return Me(PGNTag.Key)
        Else
            Me.Add(PGNTag)
            Return PGNTag
        End If
    End Function

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

    'Public Property JournalImage As String
    '    Set(pJournalImage As String)
    '        Dim Elements() As String = pJournalImage.Split("<:>")
    '        For Each Element In Elements
    '            Me.Add(Element)
    '        Next Element
    '    End Set
    '    Get
    '        Dim Elements As String = ""
    '        For Each TAG As PGNTag In Me
    '            Elements = Elements & If(Elements = "", "", "<:>") & TAG.PGNString
    '        Next TAG
    '        Return Elements
    '    End Get
    'End Property

    Public Sub New(pInitial As Boolean)
        If pInitial = True Then
            Me.Clear()
        End If
    End Sub

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        For Each PGNTag As PGNTag In Me
            PGNTag = Nothing
        Next PGNTag

        MyBase.Finalize()
    End Sub
End Class
