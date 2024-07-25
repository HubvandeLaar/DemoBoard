Option Explicit On

Imports System.Text.RegularExpressions
Imports ChessGlobals
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTrainingLocalizedQuestion
    <XmlAttribute()>
    Public Language As String 'Optional
    <XmlAttribute()>
    Public Question As String
    <XmlAttribute()>
    Public Hint1 As String
    <XmlAttribute()>
    Public Hint2 As String
    <XmlElement()>
    Public Answers As New List(Of PGNTrainingAnswer)

    Public ReadOnly Property CorrectAnswer As PGNTrainingAnswer
        Get
            Dim BestAnswer As PGNTrainingAnswer = Nothing
            For Each Answer As PGNTrainingAnswer In Me.Answers
                If BestAnswer Is Nothing Then
                    BestAnswer = Answer
                ElseIf BestAnswer.Points < Answer.Points Then
                    BestAnswer = Answer
                End If
            Next Answer
            Return BestAnswer
        End Get
    End Property

    Public Sub New(pPGNString As String, Optional pLanguage As String = "")
        Me.Language = pLanguage
        Me.PGNString = pPGNString
    End Sub

    Public Sub New()
    End Sub

    Private Function SplitCSV(pText) As List(Of String)
        Dim P As Integer = 1, Q As Integer
        Dim Text As New List(Of String)
        While P <= Len(pText)
            Select Case Mid(pText, P, 1)
                Case " ", "," 'skip spaces and comma's between values
                    P = P + 1
                    Continue While
                Case """"
                    Q = InStr(P + 1, pText, """,")
                    If Q > 0 Then
                        Text.Add(Mid(pText, P + 1, Q - P - 1))
                        P = Q + 2 'Skip Quote and comma
                    Else
                        Text.Add(Mid(pText, P + 1))
                        P = Integer.MaxValue
                    End If
                Case Else 'Probably Move or number
                    Q = InStr(P + 1, pText, ",")
                    If Q > 0 Then
                        Text.Add(Mid(pText, P, Q - P))
                        P = Q + 1 'Skip Comma
                    Else
                        Text.Add(Mid(pText, P))
                        P = Integer.MaxValue
                    End If
            End Select
        End While
        Return Text '.ToArray()
    End Function

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Dim Elements As List(Of String) = SplitCSV(pPGNString)   ' pPGNString.Split(",")
            Question = "" : Hint1 = "" : Hint2 = ""
            If Elements.Count > 0 Then Question = TrimQuotes(Elements(0))
            If Elements.Count > 1 Then Hint1 = TrimQuotes(Elements(1))
            If Elements.Count > 2 Then Hint2 = TrimQuotes(Elements(2))

            Answers.Clear()
            For I As Integer = 3 To Elements.Count - 1 Step 3
                Answers.Add(New PGNTrainingAnswer(Elements(I), Elements(I + 1), Val(Elements(I + 2)), Answers.Count))
            Next I
        End Set
        Get
            Dim AnswersString As String = ""
            For Each Answer As PGNTrainingAnswer In Answers
                AnswersString += "," & Answer.PGNString()
            Next
            Return If(Language <> "", """" & Language & """,""", """") _
                  & Question & """,""" & Hint1 & """,""" & Hint2 & """" _
                  & AnswersString
        End Get
    End Property

    Private Function TrimQuotes(pText As String) As String
        Dim Text As String = Trim(pText)
        If Left(Text, 1) = """" Then
            Return Mid(Text, 2, Len(Text) - 2)
        Else
            Return Text
        End If
    End Function

    Protected Overrides Sub Finalize()
        Me.Answers = Nothing

        MyBase.Finalize()
    End Sub

End Class
