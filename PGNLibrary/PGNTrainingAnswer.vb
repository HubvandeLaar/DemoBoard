Option Explicit On

Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTrainingAnswer
    <XmlElement()>
    Public Property Move As PGNHalfMove = Nothing
    <XmlAttribute()>
    Public Property Points As Integer
    <XmlAttribute()>
    Public Property FeedBack As String
    <XmlAttribute()>
    Public Property Index As Integer

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

    Public Sub New(pMoveText As String, pFeedBack As String, pPoints As Integer, pIndex As Integer)
        Me.Move = New PGNHalfMove(pMoveText:=pMoveText)
        Me.FeedBack = pFeedBack
        Me.Points = pPoints
        Me.Index = pIndex
    End Sub

    Public Sub New()
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return Me.PGNString
    End Function

End Class
