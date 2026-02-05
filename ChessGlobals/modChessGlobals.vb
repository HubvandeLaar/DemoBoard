Option Explicit On

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Module modChessGlobals

    ''' <summary>Returns the folder of the current Assembly</summary>
    Public Function RootFolder() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory.Replace("\bin\Debug", "")
    End Function

    ''' <summary>Adds the KNSBFigurine from fontfile to the PrivateFontCollection.
    ''' And returns the KNSBFigurine as a Font.</summary>
    Public Function KNSBFigurine(pSize As Int32) As Font
        Static FontCollection As Text.PrivateFontCollection
        If FontCollection Is Nothing Then
            FontCollection = New System.Drawing.Text.PrivateFontCollection()
            FontCollection.AddFontFile(RootFolder() & "Fonts\KNSB figurine.ttf")
        End If
        Return New Font(FontCollection.Families(0), pSize)
    End Function

    ''' <summary>Adds the KNSB from fontfile to the PrivateFontCollection.
    ''' And returns the KNSB as a Font.</summary>
    Public Function KNSB(pSize As Int32) As Font
        Static FontCollection As Text.PrivateFontCollection
        If FontCollection Is Nothing Then
            FontCollection = New System.Drawing.Text.PrivateFontCollection()
            FontCollection.AddFontFile(RootFolder() & "Fonts\KNSB.ttf")
        End If
        Return New Font(FontCollection.Families(0), pSize)
    End Function

    Public Sub Wait(ByVal pMiliSeconds As Integer)
        For T As Integer = 0 To (pMiliSeconds / 10)
            Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next T
    End Sub

    ''' <summary>Returns the FileName without the Extension</summary>
    <Extension()>
    Public Function WithoutExtention(pFileName As String) As String
        Dim P As Long = InStrRev(pFileName, ".")
        If P > 1 Then
            Return Strings.Left(pFileName, P - 1)
        Else
            Return pFileName
        End If
    End Function

    ''' <summary>Returns the FolderName of a FullName file</summary>
    <Extension()>
    Public Function FolderName(pFullName As String) As String
        Dim P As Long = InStrRev(pFullName, "\")
        If P > 0 And P < pFullName.Length Then
            Return Strings.Left(pFullName, P)    'Path including \
        Else
            Return ChessGlobals.CurrentLessonsFolder
        End If
    End Function

    ''' <summary>Returns a distance in Centimeters for a specified distance in Pixels</summary>
    <Extension()>
    Public Function Centimeters(pPixels As Integer) As Double
        Return pPixels * 2.54 / 96
    End Function

    Public Sub DebugPrint(pSub As String, pStartTime As DateTime, pStopTime As DateTime)
        Debug.Print(pSub & ": " & pStopTime.Subtract(pStartTime).TotalMilliseconds.ToString & " ms")
    End Sub

End Module
