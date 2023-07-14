Option Explicit On

Imports System.Text
Imports System.Reflection
Imports Microsoft.VisualBasic.FileIO.FileSystem

Module Refection

    Sub Main()
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
        Dim Writer As New System.IO.StreamWriter("..\..\Help\" & pAssemblyName & ".htm")
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
    End Sub

    Sub ListClass(pType As Type, pAssemblyName As String, pModuleName As String)
        Dim Writer As New System.IO.StreamWriter("..\..\Help\" & pType.Name & ".htm")
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
    End Sub

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

    Function ControlName(pMethodName As String) As String
        Dim P As Integer = InStr(pMethodName, "_")
        If P > 1 Then
            Return Left(pMethodName, P - 1)
        Else
            Return pMethodName
        End If
    End Function

End Module
