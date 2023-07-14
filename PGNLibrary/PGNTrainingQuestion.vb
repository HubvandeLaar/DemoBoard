Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTrainingQuestion

    Public Const PGNHeader As String = "[%tqu "
    Public Const PGNTrailer As String = "]"

    <XmlElement()>
    Public LocalizedQuestions As New List(Of PGNTrainingLocalizedQuestion)

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            'pPGNString = "[%tqu ""En"",""Wat Is dan de pointe na het slaan van het paard?"", """", """", ""d4d5"", ""[#]"", 10, ""Nl"", ""Wat Is dan de pointe na het slaan van het paard?"", """", """", ""d4d5"", ""[#]"", 10]"
            Dim PGN As String = pPGNString
            If InStr(PGN, PGNHeader) = 1 Then PGN = Mid(PGN, Len(PGNHeader) + 1)
            If InStr(PGN, PGNTrailer) = Len(PGN) Then PGN = Left(PGN, Len(PGN) - Len(PGNTrailer))
            Me.ListString = PGN
        End Set
        Get
            Dim PGN As String = Me.ListString
            Return If(PGN = "", "", PGNHeader & PGN & PGNTrailer)
        End Get
    End Property

    <XmlIgnore>
    Private Property ListString() As String
        Set(pListString As String)
            Dim Elements As MatchCollection = Regex.Matches(pListString, "\""(En|De|Fr|Es|It|Ne|Nl|Pt)\""")
            Me.LocalizedQuestions.Clear()
            If Elements.Count = 0 Then
                Me.LocalizedQuestions.Add(New PGNTrainingLocalizedQuestion(TrimCommasAndSpaces(pListString)))
            Else
                For I = 0 To Elements.Count - 1
                    If I = Elements.Count - 1 Then
                        Me.LocalizedQuestions.Add(New PGNTrainingLocalizedQuestion(TrimCommasAndSpaces(Mid(pListString, Elements(I).Index + 5)), Mid(Elements(I).Value, 2, 2)))
                    Else
                        Me.LocalizedQuestions.Add(New PGNTrainingLocalizedQuestion(TrimCommasAndSpaces(Mid(pListString, Elements(I).Index + 5, Elements(I + 1).Index - Elements(I).Index - 4)), Mid(Elements(I).Value, 2, 2)))
                    End If
                Next I
            End If
        End Set
        Get
            Dim Elements As String = ""
            For Each Question As PGNTrainingLocalizedQuestion In LocalizedQuestions
                If Elements <> "" Then Elements = Elements & ","
                Elements = Elements & Question.PGNString()
            Next
            Return Elements
        End Get
    End Property

    Public Shared Function ContainsTrainingQuestion(pComment As String) As Boolean
        Return (pComment Like "*[[]%tqu *[]]*") 'Contains *[%tqu *]* 
    End Function

    Public Shared Function GetPGNQuestion(pComment As String) As String
        Dim P1 As Long = InStr(pComment, PGNHeader, vbBinaryCompare)
        If P1 = 0 Then
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidQuestion", pComment))
        End If
        Dim P2 As Long = P1 '  = InStr(P1, pComment, "]", vbBinaryCompare)
        Do : P2 = InStr(P2 + 1, pComment, PGNTrailer, vbBinaryCompare)
        Loop While ((P2 < pComment.Length And P2 <> 0) _
           AndAlso Mid(pComment, (P2 - 1), 1) = "#") 'Question can contains [#] in feedback strings
        If P2 = 0 Then
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidQuestion", pComment))
        End If
        Return Mid(pComment, P1, P2 - P1 + 1)
    End Function

    Public Function GetLocalizedQuestion(Optional ByVal pLanguage As String = "")
        If pLanguage = "" Then
            Select Case CurrentLanguage
                Case ENGLISH : pLanguage = "En"
                Case NEDERLANDS : pLanguage = "Ne"
            End Select
        End If

        Dim DefaultLocalizedQuestion As PGNTrainingLocalizedQuestion = Nothing
        For Each LocalizedQuestion As PGNTrainingLocalizedQuestion In Me.LocalizedQuestions
            If LocalizedQuestion.Language = pLanguage Then 'Best fit is Language asked for
                Return LocalizedQuestion
            End If
            If LocalizedQuestion.Language = "En" Then 'Good fit is Default
                DefaultLocalizedQuestion = LocalizedQuestion
            End If
        Next LocalizedQuestion

        If DefaultLocalizedQuestion IsNot Nothing Then
            Return DefaultLocalizedQuestion
        End If

        If Me.LocalizedQuestions.Count > 0 Then
            Return Me.LocalizedQuestions(0)
        Else
            Return Nothing
        End If
    End Function

    Public Sub New(pPGNString As String)
        If pPGNString <> "" Then
            Me.PGNString = pPGNString
        End If
    End Sub

    Public Sub New()
    End Sub

    Private Function TrimCommasAndSpaces(pText As String) As String
        Dim Text As String = pText
        While (Left(Text, 1) = "," Or Left(Text, 1) = " ")
            Text = Mid(Text, 2)
            If Text.Length = 0 Then Return Text
        End While
        While (Right(Text, 1) = "," Or Right(Text, 1) = " ")
            Text = Left(Text, Text.Length - 1)
            If Text.Length = 0 Then Return Text
        End While
        Return Text
    End Function

End Class
