﻿Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNTextList
    Inherits List(Of Text)

    Private Const PGNHeader As String = "[%ctl "
    Private Const PGNTrailer As String = "]"

    <XmlIgnore>
    Public Property XPGNString() As String
        Set(pXPGNString As String)
            Dim XPGN As String = pXPGNString
            If InStr(XPGN, PGNHeader) = 1 Then XPGN = Mid(XPGN, Len(PGNHeader) + 1)
            If InStr(XPGN, PGNTrailer) < Len(XPGN) Then XPGN = Left(XPGN, Len(XPGN) - Len(PGNTrailer))
            Me.ListString = XPGN
        End Set
        Get
            Dim XPGN As String = Me.ListString
            Return If(XPGN = "", "", PGNHeader & XPGN & PGNTrailer)
        End Get
    End Property

    <XmlIgnore>
    Public Property ListString() As String
        Set(pListString As String)
            Dim Elements() As String = Strings.Split(pListString, ",")
            Me.Clear()
            For Each Element As String In Elements
                If Strings.Right(Element, 1) = "]" Then
                    Element = Element.Substring(0, Element.Length - 1) 'Trim Last position
                End If
                Me.Add(New Text(Trim(Element)))
            Next Element
        End Set
        Get
            Dim Elements As String = ""
            For Each Text As Text In Me
                If Elements <> "" Then Elements = Elements & ", "
                Elements = Elements & Text.PGNString()
            Next Text
            Return Elements
        End Get
    End Property

    Public Shadows Sub Remove(pText As Text)
        For Each Text As Text In Me
            If Text.FieldName = pText.FieldName Then
                MyBase.Remove(Text)
                Exit Sub
            End If
        Next
    End Sub

    Public Sub New(Optional pString As String = "")
        MyBase.New()
        If pString <> "" Then
            If InStr(pString, PGNHeader, vbBinaryCompare) > 0 Then
                Me.XPGNString = pString
            Else
                Me.ListString = pString
            End If
        End If
    End Sub

    Public Sub New()
    End Sub

    Public Shared Function ContainsTextList(pComment As String) As Boolean
        Return (pComment Like "*[[]%ctl *[]]*") 'Contains *[%ctl *]* 
    End Function

    Public Shared Function GetTextList(pComment As String) As String
        Dim P1 As Long = InStr(pComment, PGNHeader, vbBinaryCompare)
        If P1 = 0 Then
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidTextList", pComment))
        End If
        Dim P2 As Long = InStr(P1, pComment, PGNTrailer, vbBinaryCompare)
        If P2 = 0 Then
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidTextList", pComment))
        End If
        Return Mid(pComment, P1, P2 - P1 + 1)
    End Function

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return Me.XPGNString
    End Function

End Class

