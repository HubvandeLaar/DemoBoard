Option Explicit On

Imports ChessGlobals

Public Class frmRecentFiles

    Dim MainForm As frmMainForm

    Public Overloads Sub ShowDialog(pMainform As frmMainForm)
        MainForm = pMainform
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = MainForm.Top : Me.Left = MainForm.Left
        Me.Width = MainForm.Width : Me.Height = MainForm.Height

        Me.ListRecentFiles()

        MyBase.ShowDialog()
    End Sub

    Private Sub ListRecentFiles()
        Try
            Dim RecentFiles As Specialized.StringCollection = My.Settings.RecentFiles
            Dim ListText(1) As String 'For 2 Columns

            'A StringCollection setting will be Nothing by default, unless you edit it in the Settings designer.
            If RecentFiles Is Nothing Then
                My.Settings.RecentFiles = New Specialized.StringCollection()
                RecentFiles = My.Settings.RecentFiles
            End If

            'Get rid of any existing List-entries
            Me.lstRecentFiles.Items.Clear()
            For Each RecentFile As String In RecentFiles
                Dim P As Long = InStrRev(RecentFile, "\")
                If P > 0 And P < RecentFile.Length Then
                    ListText(0) = Strings.Mid(RecentFile, P + 1) 'FileName
                    ListText(1) = Strings.Left(RecentFile, P)    'Path including \
                Else
                    ListText(0) = RecentFile
                    ListText(1) = ""
                End If
                Me.lstRecentFiles.Items.Add(New ListViewItem(ListText))
            Next

            Application.DoEvents()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lstRecentFiles_MouseDoubleClick(pSender As Object, pArgs As MouseEventArgs) Handles lstRecentFiles.MouseDoubleClick
        Try
            If lstRecentFiles.SelectedItems.Count > 0 Then
                Dim SelectedFile As String = lstRecentFiles.SelectedItems(0).SubItems(1).Text _
                                           & lstRecentFiles.SelectedItems(0).SubItems(0).Text
                Me.Hide()
                MainForm.OpenFile(SelectedFile)
            End If
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdNew_Click(pSender As Object, pArgs As EventArgs) Handles cmdNew.Click
        Me.Hide()
        MainForm.OpenFile()
    End Sub

    Private Sub cmdOpen_Click(pSender As Object, pArgs As EventArgs) Handles cmdOpen.Click
        Me.Hide()
        MainForm.mnuOpen_Click(Nothing, Nothing)
    End Sub

End Class