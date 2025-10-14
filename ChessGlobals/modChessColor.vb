Option Explicit On

Imports ChessGlobals.ChessColor
Imports ChessGlobals.ChessLanguage
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

    ''' <summary>ChesColor.Text returns the color in text for the specified language</summary>
    <Extension()>
    Public Function Text(pColor As ChessColor) As String
        If CurrentLanguage = NEDERLANDS Then
            Return If(pColor = WHITE, "Wit", "Zwart")
        Else
            Return If(pColor = WHITE, "White", "Black")
        End If
    End Function

    ''' <summary>ChessColor.Opponent returns the color of the opponent</summary>
    <Extension()>
    Public Function Opponent(pColor As ChessColor) As ChessColor
        If pColor = WHITE Then
            Return BLACK
        ElseIf pColor = BLACK Then
            Return WHITE
        Else
            Return UNKNOWN
        End If
    End Function

End Module
