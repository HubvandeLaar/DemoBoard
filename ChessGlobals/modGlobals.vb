Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

Public Module modGlobals

    Public Enum ChessMode
        SETUP = 0
        PLAY = 1
        TRAINING = 2
    End Enum

    Public Const REGISTRYNAME As String = "Software\DemoBoard\"

    Public Function RootFolder() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory.Replace("\bin\Debug", "")
        ' Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    End Function

    Public Function KNSBFigurine(pSize As Int32) As Font
        Static FontCollection As Text.PrivateFontCollection
        If FontCollection Is Nothing Then
            FontCollection = New System.Drawing.Text.PrivateFontCollection()
            FontCollection.AddFontFile(RootFolder() & "Fonts\KNSB figurine.ttf")
        End If
        Return New Font(FontCollection.Families(0), pSize)
    End Function

    Public Sub Wait(ByVal pMiliSeconds As Integer)
        For T As Integer = 0 To (pMiliSeconds / 10)
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next T
    End Sub

End Module
