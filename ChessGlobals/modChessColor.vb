Option Explicit On

Imports System.Xml.Serialization

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
