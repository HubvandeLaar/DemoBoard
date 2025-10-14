Option Explicit On

Imports System.Xml.Serialization
Imports PGNLibrary.PGNComment.BeforeOrAfterDiagram

<XmlType()>
Public Class PGNComment

    Public Enum BeforeOrAfterDiagram
        ORIGINAL = 0
        BEFOREDIAGRAM = 1
        AFTERDIAGRAM = 2
        REMOVEDIAGRAM = 3
    End Enum

    Public Enum CommentType
        COMMENTBEFORE
        COMMENTAFTER
    End Enum

    <XmlElement()>
    Public Property MarkerList As PGNMarkerList = Nothing
    <XmlElement()>
    Public Property ArrowList As PGNArrowList = Nothing
    <XmlElement()>
    Public Property TextList As PGNTextList = Nothing
    <XmlElement()>
    Public Property TrainingQuestion As PGNTrainingQuestion = Nothing

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
                XPGN &= Me.MarkerList.XPGNString
            End If
            If Me.ArrowList IsNot Nothing Then
                XPGN &= Me.ArrowList.XPGNString
            End If
            If Me.TextList IsNot Nothing Then
                XPGN &= Me.TextList.XPGNString
            End If
            If Me.TrainingQuestion IsNot Nothing Then
                XPGN &= Me.TrainingQuestion.PGNString
            End If
            XPGN &= Me.Text

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
                PGN &= Me.MarkerList.PGNString
            End If
            If Me.ArrowList IsNot Nothing Then
                PGN &= Me.ArrowList.PGNString
            End If
            If Me.TrainingQuestion IsNot Nothing Then
                PGN &= Me.TrainingQuestion.PGNString
            End If
            PGN &= Me.Text

            If PGN = "" Then
                Return ""
            Else
                Return PGNHeader & PGN & PGNTrailer
            End If
        End Get
    End Property

    <XmlIgnore>
    Public Property Text(Optional pBeforeOrAfterDiagram As BeforeOrAfterDiagram = ORIGINAL) As String
        Set(pText As String)
            gText = pText
        End Set
        Get
            Select Case pBeforeOrAfterDiagram
                Case ORIGINAL
                    Return gText
                Case BEFOREDIAGRAM
                    Return CommentBeforeDiagram()
                Case AFTERDIAGRAM
                    Return CommentAfterDiagram()
                Case REMOVEDIAGRAM
                    Return RemoveDiagramString()
                Case Else
                    Return gText
            End Select
        End Get
    End Property

    Public ReadOnly Property ContainsDiagram() As Boolean
        Get
            Return DiagramPosition() > 0
        End Get
    End Property

    <XmlAttribute("Text")>
    Private gText As String = ""

    Public Const PGNHeader As String = "{"
    Public Const PGNTrailer As String = "}"


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
                MarkerList += pB.MarkerList.ListString
            End If
            If MarkerList <> "" Then
                C.MarkerList = New PGNMarkerList(MarkerList)
            End If

            Dim ArrowList As String = ""
            If pA.ArrowList IsNot Nothing Then
                ArrowList = pA.ArrowList.ListString
            End If
            If pB.ArrowList IsNot Nothing Then
                ArrowList += pB.ArrowList.ListString
            End If
            If ArrowList <> "" Then
                C.ArrowList = New PGNArrowList(ArrowList)
            End If

            Dim TextList As String = ""
            If pA.TextList IsNot Nothing Then
                TextList = pA.TextList.ListString
            End If
            If pB.TextList IsNot Nothing Then
                TextList += pB.TextList.ListString
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

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return Me.XPGNString
    End Function

    ''' <summary>Returns the Comment String without Diagram request</summary>
    Private Function RemoveDiagramString() As String
        Dim Comment As String = gText
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

    ''' <summary>Returns the position of the Diagram request</summary>
    Private Function DiagramPosition() As Integer
        Dim Position As Integer
        Position = InStr(gText, Chr(4))
        If Position > 0 Then Return Position
        Position = InStr(gText, "Diagram #")
        If Position > 0 Then Return Position
        Position = InStr(gText, "Diagram [#]")
        If Position > 0 Then Return Position
        Position = InStr(gText, "Diagram  [#]")
        If Position > 0 Then Return Position
        Position = InStr(gText, "Diagram (#)")
        If Position > 0 Then Return Position
        Position = InStr(gText, "(Diagram#)")
        If Position > 0 Then Return Position
        Position = InStr(gText, "[#]")
        If Position > 0 Then Return Position
        Position = InStr(gText, "(#)")
        Return Position
    End Function

    ''' <summary>Returns the Diagram request</summary>
    Private Function DiagramText() As String
        Dim Position As Integer
        Position = InStr(gText, Chr(4))
        If Position > 0 Then Return Chr(4)
        Position = InStr(gText, "Diagram #")
        If Position > 0 Then Return "Diagram #"
        Position = InStr(gText, "Diagram [#]")
        If Position > 0 Then Return "Diagram [#]"
        Position = InStr(gText, "Diagram  [#]")
        If Position > 0 Then Return "Diagram  [#]"
        Position = InStr(gText, "Diagram (#)")
        If Position > 0 Then Return "Diagram (#)"
        Position = InStr(gText, "(Diagram#)")
        If Position > 0 Then Return "(Diagram#)"
        Position = InStr(gText, "[#]")
        If Position > 0 Then Return "[#]"
        Position = InStr(gText, "(#)")
        If Position > 0 Then Return "(#)"
        Return ""
    End Function

    ''' <summary>Returns the Comment string before the Diagram request</summary>
    Private Function CommentBeforeDiagram() As String
        Dim Pos As Integer = DiagramPosition()
        If Pos > 0 Then
            Return Left(gText, Pos - 1)
        Else
            Return gText
        End If
    End Function

    ''' <summary>Returns the Comment string after the Diagram request</summary>
    Private Function CommentAfterDiagram() As String
        Dim Pos As Integer = DiagramPosition()
        Dim Len As Integer = DiagramText().Length
        If Pos > 0 Then
            Return Mid(gText, Pos + Len + 1)
        Else
            Return gText
        End If
    End Function

    Protected Overrides Sub Finalize()
        Me.MarkerList = Nothing
        Me.ArrowList = Nothing
        Me.TextList = Nothing
        Me.TrainingQuestion = Nothing

        MyBase.Finalize()
    End Sub

End Class
