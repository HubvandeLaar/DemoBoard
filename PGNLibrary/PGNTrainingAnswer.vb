Option Explicit On

Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTrainingAnswer
    <XmlElement()>
    Public Move As PGNHalfMove = Nothing
    <XmlAttribute()>
    Public Points As Integer
    <XmlAttribute()>
    Public FeedBack As String
    <XmlAttribute()>
    Public Index As Integer

    Public Sub New(pMoveText As String, pFeedBack As String, pPoints As Integer, pIndex As Integer)
        Me.Move = New PGNHalfMove(pMoveText:=pMoveText)
        Me.FeedBack = pFeedBack
        Me.Points = pPoints
        Me.Index = pIndex
    End Sub

    Public Sub New()
    End Sub

    <XmlIgnore>
    Public ReadOnly Property PGNString() As String
        Get
            If Me.Move Is Nothing Then
                Return ""
            Else
                Return """" & Me.Move.MoveText() & """,""" & Me.FeedBack & """," & Strings.Format(Me.Points)
            End If
        End Get
    End Property

End Class
