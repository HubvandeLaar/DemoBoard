Option Explicit On

Imports ChessGlobals

Public Module modLastPDF

    Public Sub UpdateLastPDF(pPDFFileName As String)
        My.Settings.LastPDF = pPDFFileName
    End Sub

    ''' <summary>Returns the last used folder for PDF-files</summary>
    Public Function LastPDFFolder() As String
        Return My.Settings.LastPDF.FolderName
    End Function

End Module
