Option Explicit On

Imports ChessMessaging

Public Class frmRecentFiles
    'NB After editing Screen a second designer file is created
    '   beneath the last resource file named: frmRecentFiles1.Designer.vb
    '   Delete this file to avoid errors during startup of this form.

    Public Property SelectedFile As String = ""
    Public Property NewGame As Boolean = False
    Public Property OpenGame As Boolean = False

    Public Overloads Sub ShowDialog(pfrmMainform As frmMainForm, pRecentFiles As RecentFiles)
        Try
            Me.StartPosition = FormStartPosition.Manual
            Me.Top = pfrmMainform.Top : Me.Left = pfrmMainform.Left
            Me.Width = pfrmMainform.Width : Me.Height = pfrmMainform.Height

            Me.ListRecentFiles(pRecentFiles)

            Application.DoEvents()
            MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub ListRecentFiles(pRecentFiles As RecentFiles)
        Try
            Dim ListText(1) As String 'For 2 Columns

            'Get rid of any existing List-entries
            Me.lstRecentFiles.Items.Clear()
            For Each RecentFile As String In pRecentFiles.List
                If IO.File.Exists(RecentFile) Then
                    Dim P As Long = InStrRev(RecentFile, "\")
                    If P > 0 And P < RecentFile.Length Then
                        ListText(0) = Strings.Mid(RecentFile, P + 1) 'FileName
                        ListText(1) = Strings.Left(RecentFile, P)    'Path including \
                    Else
                        ListText(0) = RecentFile
                        ListText(1) = ""
                    End If
                    Me.lstRecentFiles.Items.Add(New ListViewItem(ListText))
                End If
            Next

            Application.DoEvents()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstRecentFiles_MouseDoubleClick(pSender As Object, pArgs As MouseEventArgs) Handles lstRecentFiles.MouseDoubleClick
        Try
            If lstRecentFiles.SelectedItems.Count > 0 Then
                SelectedFile = lstRecentFiles.SelectedItems(0).SubItems(1).Text _
                             & lstRecentFiles.SelectedItems(0).SubItems(0).Text
                Me.Hide()
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdNew_Click(pSender As Object, pArgs As EventArgs) Handles cmdNew.Click
        SelectedFile = ""
        NewGame = True
        OpenGame = False
        Me.Hide()
    End Sub

    Private Sub cmdOpen_Click(pSender As Object, pArgs As EventArgs) Handles cmdOpen.Click
        SelectedFile = ""
        NewGame = False
        OpenGame = True
        Me.Hide()
    End Sub

End Class