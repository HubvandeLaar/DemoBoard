Option Explicit On

Imports Microsoft.Win32
Imports System.Windows.Forms

Public Module modLessonsFolder

    Public Property CurrentLessonsFolder As String
        Set(pLessonsFolder As String)
            My.Settings.LessonsFolder = pLessonsFolder
            My.Settings.Save()
        End Set
        Get
            Dim Folder As String = My.Settings.LessonsFolder
            If Folder = "" Then
                Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\DemoBoard Lessen\"
                If IO.Directory.Exists(Folder) = False Then
                    'So use the EXE-location (as for USB-stick)
                    Folder = Application.StartupPath & "\DemoBoard Lessen\"
                End If
            End If
            Return Folder
        End Get
    End Property

    Public Property UseLastUsedLessonsFolder As Boolean
        Set(pUseLastUsedLessonsFolder As Boolean)
            My.Settings.UseLastUsedFolder = pUseLastUsedLessonsFolder
            My.Settings.Save()
        End Set
        Get
            Return My.Settings.UseLastUsedFolder
        End Get
    End Property

    'Unused Method
    Private Sub AssociateXPGN()
        If Registry.ClassesRoot.OpenSubKey(".xpgn") Is Nothing Then
            Exit Sub
        End If
        Registry.ClassesRoot.CreateSubKey(".xpgn").SetValue _
            ("", "xpgn", RegistryValueKind.String)
        Registry.ClassesRoot.CreateSubKey("xpgn\shell\open\command").SetValue _
            ("", Application.ExecutablePath & " ""%l"" ", RegistryValueKind.String)
    End Sub

End Module
