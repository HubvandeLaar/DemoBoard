Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

Module ListEvents

    Public Classes As List(Of Type)
    Public Methods() As MethodInfo
    Public Events As List(Of String)

    Sub Mainewfwe()
        Dim Assembly As Assembly = Assembly.Load("DemoBoard") '= GUI
        Dim Form As Type = Assembly.GetType("DemoBoard.frmBoard")
        Dim Events As EventInfo() = Form.GetEvents(BindingFlags.Public Or BindingFlags.Instance)

        Stop
        Dim Method As MethodInfo = Form.GetMethod("remove_RightToLeftChanged")
        Dim Body As MethodBody = Method.GetMethodBody()


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
