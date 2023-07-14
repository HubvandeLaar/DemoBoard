Option Explicit On

Imports ChessGlobals
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTrainingMove

    <XmlIgnore>
    Private gFromField As String
    <XmlIgnore>
    Private gToField As String

    <XmlAttribute()>
    Public Property FromField As String
        Set(pFromField As String)
            gFromField = pFromField
        End Set
        Get
            Return gFromField
        End Get
    End Property

    <XmlAttribute()>
    Public Property ToField As String
        Set(pToField As String)
            gToField = pToField
        End Set
        Get
            Return gToField
        End Get
    End Property

    <XmlIgnore>
    Public Property PGNString As String
        Set(pPGNString As String)
            If pPGNString Like "[abcdefgh][12345678][abcdefgh][12345678]" Then
                gFromField = Left(pPGNString, 2)
                gToField = Right(pPGNString, 2)
            Else
                Throw New ArgumentException(MessageText("InvalidTQUMove", pPGNString))
            End If
        End Set
        Get
            Return gFromField & gToField
        End Get
    End Property

    Public Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New()
    End Sub

End Class
