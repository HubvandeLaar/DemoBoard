Imports System.Globalization
Imports ChessGlobals
Imports ChessGlobals.modChessLanguage.ChessLanguage

Public Class Messages

    ''' <summary>Returns the pKey Message from the Messages.resx file in CurrentLanguage.
    ''' It's a Shared Function, so this class needs no instance</summary>
    Public Shared Function MessageText(pKey As String, Optional pValue1 As String = "", Optional pValue2 As String = "",
                                                Optional pValue3 As String = "", Optional pValue4 As String = "",
                                                Optional pValue5 As String = "") As String
        Dim Text As String
        If CurrentLanguage = NEDERLANDS Then
            Text = My.Resources.Messages.ResourceManager.GetString(pKey, New CultureInfo("nl"))
        Else
            Text = My.Resources.Messages.ResourceManager.GetString(pKey, New CultureInfo("en"))
        End If
        Text = Replace(Text, "%1", pValue1)
        Text = Replace(Text, "%2", pValue2)
        Text = Replace(Text, "%3", pValue3)
        Text = Replace(Text, "%4", pValue4)
        Text = Replace(Text, "%5", pValue5)

        If Text = "" Then 'Key not found 
            Throw New KeyNotFoundException(MessageText("MessageNotFound", pKey))
        End If
        Return Text
    End Function

End Class
