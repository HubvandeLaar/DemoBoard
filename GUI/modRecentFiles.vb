Option Explicit On

Public Module modRecentFiles

    Public Sub Add(pFullFileName As String)
        Dim RecentFiles As Specialized.StringCollection = My.Settings.RecentFiles

        'If the specified file is already in the list, remove it from its old position.
        If RecentFiles.Contains(pFullFileName) Then
            RecentFiles.Remove(pFullFileName)
        End If

        'Add the new file at the top of the list.
        RecentFiles.Insert(0, pFullFileName)

        'Trim the list if it is too long.
        While RecentFiles.Count > 10
            RecentFiles.RemoveAt(RecentFiles.Count - 1)
        End While
    End Sub

    Public Function LastFolder() As String
        Dim RecentFiles As Specialized.StringCollection = My.Settings.RecentFiles
        If RecentFiles Is Nothing _
        OrElse RecentFiles.Count = 0 Then
            Return ChessGlobals.CurrentLessonsFolder
        Else
            Return FolderName(RecentFiles(0))
        End If
    End Function

    Private Function FolderName(pFullName As String) As String
        Dim P As Long = InStrRev(pFullName, "\")
        If P > 0 And P < pFullName.Length Then
            Return Strings.Left(pFullName, P)    'Path including \
        Else
            Return ChessGlobals.CurrentLessonsFolder
        End If
    End Function

End Module
