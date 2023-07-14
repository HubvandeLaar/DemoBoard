Option Explicit On

Imports ChessGlobals
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNGames
    Inherits List(Of PGNGame)

    Public Overloads Function Add() As PGNGame
        Dim PGNGame As New PGNGame(True)
        MyBase.Add(PGNGame)
        PGNGame.Index = Me.Count - 1
        Return PGNGame
    End Function

    Public Overloads Sub Insert(pIndex As Long, pPGNGame As PGNGame)
        MyBase.Insert(pIndex, pPGNGame)
        Me.Renumber()
    End Sub

    ''' <summary>Returns New Current PGNGame</summary>
    Public Overloads Function Remove(pPGNGame As PGNGame) As PGNGame
        Dim Index As Long
        Index = pPGNGame.Index - 1
        If Index < 0 Then Index = 0
        MyBase.Remove(pPGNGame)
        If Me.Count = 0 Then
            Return Me.Add()
        End If
        Me.Renumber()
        Return Me(Index)
    End Function

    Public Sub MoveUp(pPGNGame As PGNGame)
        Dim G As Integer, SavePGNGame As PGNGame
        G = Index(pPGNGame)
        If G > 0 Then
            SavePGNGame = Me(G)
            MyBase.Remove(pPGNGame)
            Me.Insert((G - 1), SavePGNGame)
            Me.Renumber()
        End If
    End Sub

    Public Sub MoveDown(pPGNGame As PGNGame)
        Dim G As Integer, SavePGNGame As PGNGame
        G = Index(pPGNGame)
        If G < Me.Count - 1 Then
            SavePGNGame = Me(G)
            MyBase.Remove(pPGNGame)
            Me.Insert(G + 1, SavePGNGame) 'NB after Remove G+1 has taken place of G
            Me.Renumber()
        End If
    End Sub

    Private Function Index(pPGNGame As PGNGame)
        Dim G As Integer
        For G = 0 To Me.Count - 1
            If Me(G) Is pPGNGame Then
                Return G
            End If
        Next G
        Return 0
    End Function

    Private Sub Renumber()
        Dim G As Integer
        For G = 0 To Me.Count - 1
            Me(G).Index = G
        Next G
    End Sub

    Protected Overrides Sub Finalize()
        For Each PGNGame As PGNGame In Me
            PGNGame = Nothing
        Next PGNGame

        MyBase.Finalize()
    End Sub

End Class
