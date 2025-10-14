Option Explicit On

Imports ChessGlobals

Public Class RecentFiles

    Public ReadOnly Property List As Specialized.StringCollection
        Get
            Dim RecentFiles As Specialized.StringCollection = My.Settings.RecentFiles
            If RecentFiles Is Nothing Then
                My.Settings.RecentFiles = New Specialized.StringCollection()
                RecentFiles = My.Settings.RecentFiles
            End If
            Return RecentFiles
        End Get
    End Property

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

        My.Settings.Save()
    End Sub

    ''' <summary>Returns the Name of the last used Folder</summary>
    Public Function LastFolder() As String
        Dim RecentFiles As Specialized.StringCollection = My.Settings.RecentFiles
        If RecentFiles Is Nothing _
        OrElse RecentFiles.Count = 0 Then
            Return ChessGlobals.CurrentLessonsFolder
        Else
            Return RecentFiles(0).FolderName
        End If
    End Function

End Class
