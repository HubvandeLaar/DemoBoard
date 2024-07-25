Option Explicit On

Imports System.Text
Imports System.Reflection
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports System.Runtime.InteropServices

Public Class AssemblyInfo
    Public Property ID As String
    Public Property Folder As String
    Public Sub New(pID As String, pFolder As String)
        ID = pID
        Folder = pFolder
    End Sub
End Class

Module LifeCycle

    Public AssemblyNames As New List(Of AssemblyInfo)

    Sub Main()
        AssemblyNames.Add(New AssemblyInfo("ChessEngine", "ChessEngine"))
        AssemblyNames.Add(New AssemblyInfo("ChessGlobals", "ChessGlobals"))
        AssemblyNames.Add(New AssemblyInfo("ChessMaterials", "ChessMaterials"))
        AssemblyNames.Add(New AssemblyInfo("CPSLibrary", "CPSLibrary"))
        AssemblyNames.Add(New AssemblyInfo("DemoBoard", "GUI"))
        AssemblyNames.Add(New AssemblyInfo("PDFLibrary", "PDFLibrary"))
        AssemblyNames.Add(New AssemblyInfo("PGNLibrary", "PGNLibrary"))
        For Each Assembly As AssemblyInfo In AssemblyNames
            Call ListClasses(Assembly)
        Next Assembly
    End Sub

    Sub ListClasses(pAssembly As AssemblyInfo)
        'Dim Writer As New System.IO.StreamWriter("..\..\Help\" & pAssemblyName & ".htm")
        'Writer.AutoFlush = True

        Dim Assembly As Assembly = Assembly.Load(pAssembly.ID)
        Dim Types() As Type = Assembly.GetTypes()
        For Each TypeInfo As Type In Types
            If TypeInfo.IsPublic = True Then
                Debug.Print(TypeInfo.FullName)
                ListClass(TypeInfo.Name)
                'Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Yellow'><b>2</b></td>")
                'Writer.WriteLine("  <td><a href=""" & TypeInfo.Name & ".htm"">" & TypeInfo.Name & "</a></td>")
                'Writer.WriteLine("</tr>")
            End If
        Next TypeInfo


        'Console.WriteLine("Hello World!")

        'Writer.Close()
    End Sub

    Sub ListClass(pClassName As String)
        'Dim Reader As New System.IO.StreamReader("..\..\Help\" & pAssemblyName & ".htm")
        Dim FileNames() As String
        For Each Assembly As AssemblyInfo In AssemblyNames
            FileNames = System.IO.Directory.GetFiles("..\..\..\" & Assembly.Folder & "\")
            For Each FileName As String In FileNames
                If FileName Like "*.vb" Then
                    Debug.Print(pClassName & " - " & FileName)
                End If
            Next FileName
        Next Assembly

    End Sub

End Module
