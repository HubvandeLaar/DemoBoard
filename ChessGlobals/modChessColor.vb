Option Explicit On

Imports System.Xml.Serialization
Imports System.Runtime.CompilerServices

<XmlType()>
Public Module modChessColor

    Public Enum ChessColor
        <XmlEnum()>
        UNKNOWN = 0
        <XmlEnum()>
        WHITE = 1
        <XmlEnum()>
        BLACK = 2
    End Enum

    <Extension()>
    Public Function Text(pColor As ChessColor) As String
        If CurrentLanguage = ChessLanguage.NEDERLANDS Then
            Return If(pColor = ChessColor.WHITE, "Wit", "Zwart")
        Else
            Return If(pColor = ChessColor.WHITE, "White", "Black")
        End If
    End Function

    <Extension()>
    Public Function Opponent(pColor As ChessColor) As ChessColor
        If pColor = ChessColor.WHITE Then
            Return ChessColor.BLACK
        ElseIf pColor = ChessColor.BLACK Then
            Return ChessColor.WHITE
        Else
            Return ChessColor.UNKNOWN
        End If
    End Function

End Module
