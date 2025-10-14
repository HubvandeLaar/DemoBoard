Option Explicit On

Imports ChessMessaging.Messages

Public Class frmImages

    ''' <summary>Returns an Image with specified Name</summary>
    Public Function getImage(pName As String) As Image
        Try
            Dim PictureBox As PictureBox = Me.Controls(pName)
            Return PictureBox.Image

        Catch pException As Exception
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidImageName", pName))
        End Try
    End Function

    ''' <summary>Returns an Icon with specified Name</summary>
    Public Function getIcon(pName As String) As Icon
        Try
            Return Me.BitMapToIcon(Me.getImage(pName))

        Catch pException As Exception
            Throw New System.ArgumentOutOfRangeException(MessageText("InvalidIconName", pName))
        End Try
    End Function

    ''' <summary>Returns a Bitmat from an Image</summary>
    Public Function BitMapToIcon(pImage As Image) As Icon
        Using Bitmap As New Bitmap(pImage)
            Return Drawing.Icon.FromHandle(Bitmap.GetHicon)
        End Using
    End Function

End Class