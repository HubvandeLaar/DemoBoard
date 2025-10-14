Option Explicit On

Imports System.IO
Imports System.Reflection
Imports System.Text
Imports Microsoft.VisualBasic.FileIO.FileSystem

Module Refection

    Dim AssemblyName As String
    Dim FileName As String
    Dim BlockName As String
    Dim BlockType As String

    Const Root As String = "C:\Users\hubva\Documents\Visual Studio 2019\DemoBoard\"
    Dim Writer As New System.IO.StreamWriter(Root & "Events.txt")

    Sub Main()
        Writer.AutoFlush = True

        SeekAssembly("ChessCoach")
        SeekAssembly("ChessEngine")
        SeekAssembly("ChessGlobals")
        SeekAssembly("ChessMaterials")
        SeekAssembly("ChessMessaging")
        SeekAssembly("GUI")
        SeekAssembly("Journaling")
        SeekAssembly("PDFLibrary")
        SeekAssembly("PGNLibrary")

        Writer.Close()
    End Sub

    Private Sub SeekAssembly(pAssemblyName As String)
        Dim FileNames() = Directory.GetFiles(Root & pAssemblyName & "\")

        AssemblyName = pAssemblyName
        Debug.Print("Assembly: " & AssemblyName)

        For Each FileName As String In FileNames
            If FileName Like "*.Designer*" Then Continue For
            If FileName Like "*.vb" Then
                SeekFile(FileName)
            End If
        Next FileName
    End Sub

    Private Sub SeekFile(pFileName As String)
        Dim Line As String

        Dim P As Integer = InStrRev(pFileName, "\")
        FileName = Replace(Mid(pFileName, P + 1), ".vb", "")
        Debug.Print("File: " & FileName)

        Using StreamReader = New StreamReader(pFileName, Encoding.UTF8, True, 256)
            Line = StreamReader.ReadLine()
            While (Line IsNot Nothing)

                If Line Like "* Event *" Then
                    BlockType = "Event"
                    ProcessEvent(StreamReader, Line)
                    Continue While

                ElseIf Line Like "* Handles *" Then
                    BlockType = "EventHandler"
                    ProcessEventHandler(StreamReader, Line)
                    Continue While

                ElseIf Line Like "* Property *" Then
                    BlockType = "Property"
                    ProcessProperty(StreamReader, Line)
                    Continue While

                ElseIf Line Like "* Sub *" Then
                    BlockType = "Sub"
                    ProcessSubOrFunction(StreamReader, Line)
                    Continue While

                ElseIf Line Like "* Function *" Then
                    BlockType = "Function"
                    ProcessSubOrFunction(StreamReader, Line)
                    Continue While
                End If

                Line = StreamReader.ReadLine()
            End While
        End Using
    End Sub

    Private Sub ProcessEvent(ByRef pStreamReader As StreamReader, ByRef pLine As String)
        Dim P As Integer, Q As Integer

        P = InStr(pLine, BlockType) + Len(BlockType)
        Q = InStr(pLine, "(") - 1
        Dim EventName As String = Mid(pLine, P + 1, Q - P)
        Debug.Print("EventName: " & EventName)
        Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Event" & vbTab & EventName)

        pLine = pStreamReader.ReadLine()
    End Sub

    Private Sub ProcessEventHandler(ByRef pStreamReader As StreamReader, ByRef pLine As String)
        Dim P As Integer

        P = InStr(pLine, " Handles ") + 8
        Dim EventHandler As String = Mid(pLine, P + 1)
        Debug.Print(BlockType & ": " & EventHandler)
        Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "EventHandler" & vbTab & EventHandler)
        pLine = pStreamReader.ReadLine()

        Do
            If pLine Like "* RaiseEvent *" Then
                P = InStr(pLine, "(")
                Dim EventName As String = Trim(Replace(Left(pLine, P - 1), "RaiseEvent", ""))
                Debug.Print(EventName)
                Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Raisevent from Handler" & vbTab & EventHandler & vbTab & EventName)

            ElseIf SkipLine(pLine) Then
                'Skip

            Else
                P = InStr(pLine, "(") 'Call of a Sub or Function
                If P > 0 Then
                    Dim SubOrFunctionName As String = Trim(Left(pLine, P - 1).Replace(" Call ", ""))
                    Debug.Print("HandlerCalls:" & SubOrFunctionName)
                    Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Call from Handler" & vbTab & EventHandler & vbTab & SubOrFunctionName)
                End If

            End If

            pLine = pStreamReader.ReadLine()
        Loop Until (pLine Is Nothing OrElse pLine Like "* End Sub*")

        pLine = pStreamReader.ReadLine()
        BlockType = ""
    End Sub


    Private Sub ProcessProperty(ByRef pStreamReader As StreamReader, ByRef pLine As String)
        Dim P As Integer, Q As Integer

        P = InStr(pLine, BlockType) + Len(BlockType) + 1
        Q = InStr(pLine, " As ")
        BlockName = Mid(pLine, P, Q - P)
        Debug.Print(BlockType & ": " & BlockName)
        Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Property" & vbTab & BlockName)
        pLine = pStreamReader.ReadLine()

        If pLine Like "* Set*" _
        Or pLine Like "* Get*" Then
            'Block type like Property
        Else
            'Single line Property
            Exit Sub
        End If

        Do
            If pLine Like "* RaiseEvent *" Then
                P = InStr(pLine, "(")
                Dim EventName As String = Trim(Replace(Left(pLine, P - 1), "RaiseEvent", ""))
                Debug.Print(EventName)
                Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "RaiseEvent from Property" & vbTab & BlockName & vbTab & EventName)
            End If

            pLine = pStreamReader.ReadLine()
        Loop Until (pLine Is Nothing OrElse pLine Like "* End Property*")

        pLine = pStreamReader.ReadLine()
        BlockType = ""
    End Sub


    Private Sub ProcessSubOrFunction(ByRef pStreamReader As StreamReader, ByRef pLine As String)
        Dim P As Integer, Q As Integer

        P = InStr(pLine, BlockType) + Len(BlockType) + 1
        Q = InStr(pLine, "(")
        BlockName = Mid(pLine, P, Q - P)
        Debug.Print(BlockType & ": " & BlockName)
        Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & BlockType & vbTab & BlockName)
        pLine = pStreamReader.ReadLine()

        Do
            If pLine Like "* RaiseEvent *" Then
                P = InStr(pLine, "(")
                Dim EventName As String = Trim(Replace(Left(pLine, P - 1), "RaiseEvent", ""))
                Debug.Print(EventName)
                Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Raisevent from " & BlockType & vbTab & BlockName & vbTab & EventName)

            ElseIf SkipLine(pLine) Then
                'Skip

            Else
                P = InStr(pLine, "(") 'Call of a Sub or Function
                If P > 0 Then
                    Dim SubOrFunctionName As String = Trim(Left(pLine, P - 1).Replace(" Call ", ""))
                    Debug.Print("Calls: " & SubOrFunctionName)
                    Writer.WriteLine(AssemblyName & vbTab & FileName & vbTab & "Call from " & BlockType & vbTab & BlockName & vbTab & SubOrFunctionName)
                End If

            End If

            pLine = pStreamReader.ReadLine()
        Loop Until (pLine Is Nothing OrElse pLine Like "* End " & BlockType & "*")

        pLine = pStreamReader.ReadLine()
        BlockType = ""
    End Sub

    Private Function SkipLine(pLine As String) As Boolean
        If pLine Like "" Then Return True
        If Trim(pLine) Like "'*" Then Return True
        If Trim(pLine) Like "<*" Then Return True
        If pLine Like "*#Region*" Then Return True
        If pLine Like "* Exit*" Then Return True
        If pLine Like "* Return*" Then Return True
        If pLine Like "* Goto *" Then Return True
        If pLine Like "* Try*" Then Return True
        If pLine Like "* Catch *" Then Return True
        If pLine Like "* Finally *" Then Return True
        If pLine Like "* Throw *" Then Return True
        If pLine Like "* If*" Then Return True
        If pLine Like "* Or*" Then Return True
        If pLine Like "* And*" Then Return True
        If pLine Like "* Else*" Then Return True
        If pLine Like "* Dim *" Then Return True
        If pLine Like "* = *" Then Return True
        If pLine Like "*DoEvents*" Then Return True
        Return False
    End Function



    ''OUDE GEBIED

    Sub MainIetsOud()
        'Counting source lines of code
        List("ChessCoach")
        List("ChessEngine")
        List("ChessGlobals")
        List("ChessMaterials")
        List("ChessMessaging")
        List("GUI")
        List("Journaling")
        List("PDFLibrary")
        List("PGNLibrary")
    End Sub

    Private Sub List(pAssemblyName As String)
        Const Root As String = "C:\Users\hubva\Documents\Visual Studio 2019\DemoBoard\"
        Dim FileNames() = Directory.GetFiles(Root & pAssemblyName & "\")

        Debug.Print("Assembly: " & pAssemblyName)
        For Each FileName As String In FileNames
            If FileName Like "*.vb" Then
                Debug.Print("  File: " & FileName & "  Records: " & Records(FileName))
            End If
        Next FileName
    End Sub

    ''' <summary>Resurns the Number of Records in a given File</summary>
    Private Function Records(pFileName As String) As Integer
        Dim Line As String, Count As Integer = 0
        Using StreamReader = New StreamReader(pFileName, Encoding.UTF8, True, 256)
            Line = StreamReader.ReadLine()
            While (Line IsNot Nothing)
                Count += 1
                Line = StreamReader.ReadLine()
            End While
        End Using
        Return Count
    End Function


    'OUDE GEBIED

    Sub MainOUD()
        If MsgBox("Are you sure to destroy the Help Folder ?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        Stop

        Call ListAssembly("ChessGlobals", "ChessGlobals.dll")
        Call ListAssembly("DemoBoard", "DemoBoard.exe") '=GUI
        Call ListAssembly("ChessMaterials", "ChessMaterials.dll")
        Call ListAssembly("CPSLibrary", "CPSLibrary.dll")
        Call ListAssembly("PGNLibrary", "PGNLibrary.dll")
    End Sub

    Sub ListAssembly(pAssemblyName As String, pModuleName As String)
        Using Writer As New System.IO.StreamWriter("..\..\Help\" & pAssemblyName & ".htm")
            Writer.AutoFlush = True

            Writer.WriteLine("<!DOCTYPE HTML PUBLIC ""-//IETF//DTD HTML//EN"">")
            Writer.WriteLine("<html>")
            Writer.WriteLine("")
            Writer.WriteLine("<head>")
            Writer.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"">")
            Writer.WriteLine("<meta name=""GENERATOR"" content=""DemoBoard Help"">")
            Writer.WriteLine("<title>" & pAssemblyName & "</title>")
            Writer.WriteLine("<style>@import url(Styles.css);</style>")
            Writer.WriteLine("<link disabled rel=""stylesheet"" href=""htmlhelp.css"">")
            Writer.WriteLine("</head>")
            Writer.WriteLine("")
            Writer.WriteLine("<body>")
            Writer.WriteLine("<table style='border:solid darkgray 1.5pt;' BGColor=LightGray>")

            Writer.WriteLine("<tr><td></td>")
            Writer.WriteLine("  <td><b><a>" & pAssemblyName & "</a></b></td>")
            Writer.WriteLine("</tr>")
            Dim Assembly As Assembly = Assembly.Load(pAssemblyName)
            Dim Types() As Type = Assembly.GetTypes()
            For Each TypeInfo As Type In Types
                If TypeInfo.IsPublic = True Then
                    ListClass(TypeInfo, pAssemblyName, pModuleName)
                    Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Yellow'><b>2</b></td>")
                    Writer.WriteLine("  <td><a href=""" & TypeInfo.Name & ".htm"">" & TypeInfo.Name & "</a></td>")
                    Writer.WriteLine("</tr>")
                End If
            Next TypeInfo
            Writer.WriteLine("")

            Writer.WriteLine("</body>")
            Writer.WriteLine("</html>")
            Writer.Close()
        End Using
    End Sub

    Sub ListClass(pType As Type, pAssemblyName As String, pModuleName As String)
        Using Writer As New System.IO.StreamWriter("..\..\Help\" & pType.Name & ".htm")
            Dim DirName As String = Replace(CurrentDirectory(), "\bin\Debug", "\Help\")
            Writer.AutoFlush = True

            Writer.WriteLine("<!DOCTYPE HTML PUBLIC ""-//IETF//DTD HTML//EN"">")
            Writer.WriteLine("<html>")
            Writer.WriteLine("")
            Writer.WriteLine("<head>")
            Writer.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"">")
            Writer.WriteLine("<meta name=""GENERATOR"" content=""DemoBoard Help"">")
            Writer.WriteLine("<title>" & pType.Name & "</title>")
            Writer.WriteLine("<style>@import url(Styles.css);</style>")
            Writer.WriteLine("<link disabled rel=""stylesheet"" href=""htmlhelp.css"">")
            Writer.WriteLine("</head>")
            Writer.WriteLine("")
            Writer.WriteLine("<body>")
            Writer.WriteLine("<table style='border:solid darkblue 1.5pt;' BGColor=lightblue>")

            Writer.WriteLine("<tr><td></td>")
            Writer.WriteLine("  <td><b><a href=""../" & pType.Name & ".htm"">" & pType.Name & "</a></b></td>")
            Writer.WriteLine("</tr>")
            Writer.WriteLine("")

            Dim Events() As EventInfo = pType.GetEvents()
            For Each EventInfo As EventInfo In Events
                If EventInfo.Module.Name = pModuleName Then
                    Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Yellow'><b>~</b></td>")
                    Writer.WriteLine("  <td>" & EventInfo.Name & "</td>")
                    Writer.WriteLine("</tr>")
                End If
            Next EventInfo
            Writer.WriteLine("")

            Dim Enums() As Type = pType.GetNestedTypes()
            For Each EnumInfo As Type In Enums
                If EnumInfo.BaseType.Name = "Enum" Then
                    Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Yellow'><b>2</b></td>")
                    Writer.WriteLine("  <td>" & EnumInfo.Name & "</td>")
                    Writer.WriteLine("</tr>")
                End If
            Next EnumInfo
            Writer.WriteLine("")

            Dim Properties() As PropertyInfo = pType.GetProperties()
            For Each PropertyInfo As PropertyInfo In Properties
                If PropertyInfo.GetMethod Is Nothing Then Continue For
                If PropertyInfo.GetMethod.IsPublic() = True _
                And PropertyInfo.Module.Name = pModuleName Then
                    Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Blue'><b>(</b></td>")
                    If FileExists(DirName & PropertyInfo.PropertyType.Name & ".htm") Then
                        Writer.WriteLine("  <td><a href=""" & PropertyInfo.PropertyType.Name & ".htm"">" & PropertyInfo.Name & "</a></td>")
                    Else
                        Writer.WriteLine("  <td>" & PropertyInfo.Name & "</td>")
                    End If
                    Writer.WriteLine("</tr>")
                End If
            Next PropertyInfo
            Writer.WriteLine("")

            Dim Fields() As FieldInfo = pType.GetFields
            For Each FieldInfo As FieldInfo In Fields
                If FieldInfo.IsPublic() = True Then
                    Writer.WriteLine("<tr><td style='font-family:""Wingdings"";color:Blue'><b>o</b></td>")
                    If FileExists(DirName & FieldInfo.FieldType.Name & ".htm") Then
                        Writer.WriteLine("  <td><a href=""" & FieldInfo.FieldType.Name & ".htm"">" & FieldInfo.Name & "</a></td>")
                    Else
                        Writer.WriteLine("  <td>" & FieldInfo.Name & "</td>")
                    End If
                    Writer.WriteLine("</tr>")
                End If
            Next FieldInfo
            Writer.WriteLine("")

            Dim Methods() As MethodInfo = pType.GetMethods()
            For Each MethodInfo As MethodInfo In Methods
                If MethodInfo.IsPublic() _
                And InStr(MethodInfo.Name, "_") = 0 _
                And MethodInfo.Module.Name = pModuleName Then
                    Writer.WriteLine("<tr><td style='font-family:""Webdings"";color:Blue'><b>@</b></td>")
                    Writer.WriteLine("  <td>" & MethodInfo.Name & Parameters(MethodInfo.GetParameters) & "</td>")
                    Writer.WriteLine("</tr>")
                End If
            Next MethodInfo
            Writer.WriteLine("")

            Dim Controls() As MethodInfo = pType.GetRuntimeMethods()
            For Each ControlInfo As MethodInfo In Controls
                If ControlInfo.Module.Name = pModuleName Then
                    If ControlInfo.Name Like "cmd*_Click" _
                    Or ControlInfo.Name Like "mnu*_Click" _
                    Or ControlInfo.Name Like "mnu*_DropDownItemClicked" Then
                        Writer.WriteLine("<tr><td style='font-family:""Wingdings"";color:Red'><b>F</b></td>")
                        Writer.WriteLine("  <td>" & ControlName(ControlInfo.Name) & "</td>")
                        Writer.WriteLine("</tr>")
                    End If
                End If
            Next ControlInfo
            Writer.WriteLine("")

            Writer.WriteLine("</table>")
            Writer.WriteLine("")

            Writer.WriteLine("<p>Parents</p>")
            Writer.WriteLine("<div style=""margin-left:  20px"">")
            Writer.WriteLine("<li><a href=""" & pAssemblyName & ".htm"">" & pAssemblyName & "</a>")
            Writer.WriteLine("</div></p>")
            Writer.WriteLine("")

            Writer.WriteLine("<p>Children</p>")
            Writer.WriteLine("<div style=""margin-left: 20px"">")
            Writer.WriteLine("<li><a>None</a>")
            Writer.WriteLine("<li><a href=""frmChessBoard.htm"">frmChessBoard</a>")
            Writer.WriteLine("</div></p>")
            Writer.WriteLine("")

            Writer.WriteLine("</body>")
            Writer.WriteLine("</html>")
            Writer.Close()
        End Using
    End Sub

    ''' <summary>Returns a string with all Parameters delimitted by comma</summary>
    Function Parameters(pParameters() As ParameterInfo) As String
        Dim OutString As New StringBuilder()
        For P As Integer = 0 To pParameters.Length - 1
            If P = pParameters.Length - 1 Then
                OutString.Append(pParameters(P).Name)
            Else
                OutString.Append(pParameters(P).Name & ", ")
            End If
        Next P
        Return "(" & OutString.ToString() & ")"
    End Function

    ''' <summary>Return the clean MethodName</summary>
    Function ControlName(pMethodName As String) As String
        Dim P As Integer = InStr(pMethodName, "_")
        If P > 1 Then
            Return Left(pMethodName, P - 1)
        Else
            Return pMethodName
        End If
    End Function

End Module
