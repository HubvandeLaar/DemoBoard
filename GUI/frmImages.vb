Option Explicit On

Imports ChessGlobals

Public Class frmImages

    Public Function getImage(ByRef pName As String) As Image
        Try
            Dim PictureBox As PictureBox = Me.Controls(pName)
            Return PictureBox.Image
        Catch pException As Exception
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidImageName", pName))
        End Try
    End Function

    Public Function getIcon(ByRef pName As String) As Icon
        Try
            Return Me.BitMapToIcon(Me.getImage(pName))
        Catch pException As Exception
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidIconName", pName))
        End Try
    End Function

    Public Function BitMapToIcon(pImage As Image) As Icon
        Dim Bitmap As New Bitmap(pImage)
        Return Drawing.Icon.FromHandle(Bitmap.GetHicon)
        Bitmap.Dispose() ' = Nothing
    End Function

End Class