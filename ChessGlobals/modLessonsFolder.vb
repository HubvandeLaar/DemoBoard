Imports Microsoft.Win32
Imports System.Windows.Forms

Public Module modLessonsFolder

    Public Property CurrentLessonsFolder As String
        Set(pLessonsFolder As String)
            Dim SubKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey(REGISTRYNAME)
            SubKey.SetValue("Lessons", pLessonsFolder)
        End Set
        Get
            Dim Folder As String = My.Computer.Registry.CurrentUser.GetValue(REGISTRYNAME & "Lessons", "")
            If Folder = "" _
            OrElse IO.Directory.Exists(Folder) = False Then
                'No (valid) registry entry
                Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\DemoBoard Lessen\"
                If IO.Directory.Exists(Folder) = False Then
                    'So use the EXE-location (as for USB-stick)
                    Folder = Application.StartupPath & "\DemoBoard Lessen\"
                End If
            End If
            Return Folder
        End Get
    End Property

    Sub AssociateXPGN()
        If Registry.ClassesRoot.OpenSubKey(".xpgn") Is Nothing Then
            Exit Sub
        End If
        Registry.ClassesRoot.CreateSubKey(".xpgn").SetValue _
            ("", "xpgn", RegistryValueKind.String)
        Registry.ClassesRoot.CreateSubKey("xpgn\shell\open\command").SetValue _
            ("", Application.ExecutablePath & " ""%l"" ", RegistryValueKind.String)
    End Sub

End Module
