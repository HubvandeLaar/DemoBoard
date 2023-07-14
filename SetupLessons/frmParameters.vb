Option Explicit On

Imports System.Environment

Public Class frmParameters

    Const REGISTRYNAME As String = "Software\DemoBoard\"

    Private Sub frmParameters_Load(pSender As Object, pArgs As EventArgs) Handles Me.Load
        cmbLanguage.Items.Clear()
        cmbLanguage.Items.Add("Nederlands")
        cmbLanguage.Items.Add("English")
        txtLessonsFolder.Text = GetFolderPath(SpecialFolder.MyDocuments) & "\DemoBoard Lessen\"
        Select Case System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            Case "nl"
                lblLanguage.Text = "Taal"
                lblLessonsFolder.Text = "Folder voor schaaklessen"
                cmbLanguage.Text = "Nederlands"
            Case Else
                lblLanguage.Text = "Language"
                lblLessonsFolder.Text = "Folder for chess lessons"
                cmbLanguage.Text = "English"
        End Select
    End Sub

    Private Sub cmdLessonsFolder_Click(pSender As Object, pArgs As EventArgs) Handles cmdLessonsFolder.Click
        With dlgLessonsFolder
            .Description = "Select folder for Lesson-files"
            .RootFolder = SpecialFolder.MyComputer
            .SelectedPath = txtLessonsFolder.Text
            .ShowNewFolderButton = True
            If .ShowDialog() = DialogResult.OK Then
                txtLessonsFolder.Text = .SelectedPath
            End If
        End With
    End Sub

    Private Sub cmdOK_Click(pSender As Object, pArgs As EventArgs) Handles cmdOK.Click

        'During Setup, this program is in Win32 mode. The the registry-entries are redirected unless...
        'Dim Win64Registry = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry64)
        'Dim RegistrySubKey = Win64Registry.CreateSubKey(REGISTRYNAME)
        'MsgBox(RegistrySubKey.Name)

        Dim SubKey = "" 'My.Computer.Registry.Users.CreateSubKey(GetLoggedOnUserSID() & REGISTRYNAME)
        If cmbLanguage.Text = "Nederlands" _
        Or cmbLanguage.Text = "English" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & REGISTRYNAME, "Language", cmbLanguage.Text)
            'RegistrySubKey.SetValue("Language", cmbLanguage.Text)
        Else
            Exit Sub
        End If
        If txtLessonsFolder.Text.Last <> "\" Then
            txtLessonsFolder.Text = txtLessonsFolder.Text & "\"
        End If
        If IO.Directory.Exists(txtLessonsFolder.Text) = False Then
            IO.Directory.CreateDirectory(txtLessonsFolder.Text)
        End If
        If IO.Directory.Exists(txtLessonsFolder.Text) = True Then
            'RegistrySubKey.SetValue("Lessons", txtLessonsFolder.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & REGISTRYNAME, "Lessons", txtLessonsFolder.Text)
        Else
            Exit Sub
        End If

        MoveLessons(Application.StartupPath & "\DemoBoard Lessen\", txtLessonsFolder.Text)
        Me.Close()
    End Sub

    Public Sub MoveLessons(pSourceDirectory As String, pTargetDirectory As String)
        Dim LessonFiles As New IO.DirectoryInfo(pSourceDirectory)
        For Each LessonFile As IO.FileInfo In LessonFiles.GetFiles()
            If IO.File.Exists(pTargetDirectory & LessonFile.Name) = False Then
                LessonFile.CopyTo(pTargetDirectory & LessonFile.Name)
                Continue For
            End If
            If IO.File.GetLastWriteTime(pTargetDirectory & LessonFile.Name) < LessonFile.LastWriteTime Then
                'Target is older than Source
                LessonFile.CopyTo(pTargetDirectory & LessonFile.Name, overwrite:=True)
                Continue For
            End If
        Next LessonFile
    End Sub

End Class
