Option Explicit On

Imports ChessMaterials
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNComment

    <XmlElement()>
    Public MarkerList As PGNMarkerList = Nothing
    <XmlElement()>
    Public ArrowList As PGNArrowList = Nothing
    <XmlElement()>
    Public TextList As PGNTextList = Nothing
    <XmlElement()>
    Public TrainingQuestion As PGNTrainingQuestion = Nothing

    <XmlAttribute("Text")>
    Public mText As String = ""

    Public Const PGNHeader As String = "{"
    Public Const PGNTrailer As String = "}"

    <XmlIgnore>
    Public Property XPGNString() As String
        Set(pXPGNString As String)
            Me.Text = pXPGNString
            If InStr(Me.Text, PGNHeader) = 1 Then Me.Text = Mid(Me.Text, Len(PGNHeader) + 1)
            If Me.Text Like "*" & PGNTrailer Then Me.Text = Left(Me.Text, Len(Me.Text) - Len(PGNTrailer))

            Dim List As String

            If PGNMarkerList.ContainsMarkerList(pXPGNString) Then
                List = PGNMarkerList.GetMarkerList(pXPGNString)
                Me.MarkerList = New PGNMarkerList(List)
                Me.Text = Me.Text.Replace(List, "")
            End If

            If PGNArrowList.ContainsArrowList(pXPGNString) Then
                List = PGNArrowList.GetArrowList(pXPGNString)
                Me.ArrowList = New PGNArrowList(List)
                Me.Text = Me.Text.Replace(List, "")
            End If

            If PGNTextList.ContainsTextList(pXPGNString) Then
                List = PGNTextList.GetTextList(pXPGNString)
                Me.TextList = New PGNTextList(List)
                Me.Text = Me.Text.Replace(List, "")
            End If

            If PGNTrainingQuestion.ContainsTrainingQuestion(pXPGNString) Then
                Dim PGNQuestion As String = PGNTrainingQuestion.GetPGNQuestion(pXPGNString)
                Me.TrainingQuestion = New PGNTrainingQuestion(PGNQuestion)
                Me.Text = Me.Text.Replace(PGNQuestion, "")
            End If
        End Set
        Get
            Dim XPGN As String = ""
            If Me.MarkerList IsNot Nothing Then
                XPGN = XPGN & Me.MarkerList.XPGNString
            End If
            If Me.ArrowList IsNot Nothing Then
                XPGN = XPGN & Me.ArrowList.XPGNString
            End If
            If Me.TextList IsNot Nothing Then
                XPGN = XPGN & Me.TextList.XPGNString
            End If
            If Me.TrainingQuestion IsNot Nothing Then
                XPGN = XPGN & Me.TrainingQuestion.PGNString
            End If
            XPGN = XPGN & Me.Text

            If XPGN = "" Then
                Return ""
            Else
                Return PGNHeader & XPGN & PGNTrailer
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)
            Me.XPGNString = pPGNString
        End Set
        Get
            Dim PGN As String = ""
            If Me.MarkerList IsNot Nothing Then
                PGN = PGN & Me.MarkerList.PGNString
            End If
            If Me.ArrowList IsNot Nothing Then
                PGN = PGN & Me.ArrowList.PGNString
            End If
            If Me.TrainingQuestion IsNot Nothing Then
                PGN = PGN & Me.TrainingQuestion.PGNString
            End If
            PGN = PGN & Me.Text

            If PGN = "" Then
                Return ""
            Else
                Return PGNHeader & PGN & PGNTrailer
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Property Text(Optional pRevoveDiagran As Boolean = False) As String
        Set(pText As String)
            mText = pText
        End Set
        Get
            If pRevoveDiagran = True Then
                Return RemoveDiagram(mText)
            Else
                Return mText
            End If
        End Get
    End Property

    Public Shared Operator &(pA As PGNComment, pB As PGNComment) As PGNComment
        If pA Is Nothing Then
            Return pB
        ElseIf pB Is Nothing Then
            Return pA
        Else
            Dim C As New PGNComment(pA.XPGNString)

            Dim MarkerList As String = ""
            If pA.MarkerList IsNot Nothing Then
                MarkerList = pA.MarkerList.ListString
            End If
            If pB.MarkerList IsNot Nothing Then
                MarkerList = MarkerList + pB.MarkerList.ListString
            End If
            If MarkerList <> "" Then
                C.MarkerList = New PGNMarkerList(MarkerList)
            End If

            Dim ArrowList As String = ""
            If pA.ArrowList IsNot Nothing Then
                ArrowList = pA.ArrowList.ListString
            End If
            If pB.ArrowList IsNot Nothing Then
                ArrowList = ArrowList + pB.ArrowList.ListString
            End If
            If ArrowList <> "" Then
                C.ArrowList = New PGNArrowList(ArrowList)
            End If

            Dim TextList As String = ""
            If pA.TextList IsNot Nothing Then
                TextList = pA.TextList.ListString
            End If
            If pB.TextList IsNot Nothing Then
                TextList = TextList + pB.TextList.ListString
            End If
            If TextList <> "" Then
                C.TextList = New PGNTextList(TextList)
            End If

            If pA.Text = "" Then
                C.Text = pB.Text
            ElseIf pB.Text = "" Then
                C.Text = pA.Text
            Else
                C.Text = pA.Text & " " & pB.Text
            End If

            Return C
        End If
    End Operator

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return Me.XPGNString
    End Function

    'Private Functions and Methods

    Private Function RemoveDiagram(pComment As String) As String
        Dim Comment As String = pComment
        Comment = Replace(Comment, Chr(4), " ")               'Chessbase 9
        Comment = Replace(Comment, "Diagram #", " ")          'Chessbase 10
        Comment = Replace(Comment, "Diagram [#]", " ")        'Chessbase 12
        Comment = Replace(Comment, "Diagram  [#]", " ")       'Chessbase 12
        Comment = Replace(Comment, "Diagram (#)", " ")        'Chessbase 13
        Comment = Replace(Comment, "(Diagram#)", " ")
        Comment = Replace(Comment, "[#]", " ")
        Comment = Replace(Comment, "(#)", " ")
        Comment = Replace(Comment, "  ", " ")
        Return Comment
    End Function

    Protected Overrides Sub Finalize()
        Me.MarkerList = Nothing
        Me.ArrowList = Nothing
        Me.TextList = Nothing
        Me.TrainingQuestion = Nothing

        MyBase.Finalize()
    End Sub
End Class
