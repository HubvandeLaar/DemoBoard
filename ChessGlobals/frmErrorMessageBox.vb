Imports System.Windows.Forms
Imports System.Drawing

Public Class frmErrorMessageBox

    Public WriteOnly Property Exception As Exception
        Set(pException As Exception)

            Me.rtbMessage.Text = ""
            Me.rtbMessage.SelectionFont = New Font(rtbMessage.Font.FontFamily, 12, FontStyle.Bold)
            Me.rtbMessage.AppendText(pException.Message)
            Me.rtbMessage.SelectionFont = New Font(rtbMessage.Font.FontFamily, 10, FontStyle.Regular)
            Me.rtbMessage.AppendText(vbCrLf & vbCrLf & RemoveDirectories(pException.StackTrace))
            If pException.InnerException IsNot Nothing Then
                Me.rtbMessage.AppendText(vbCrLf & vbCrLf & pException.InnerException.Message)
            End If
            If CurrentLanguage = ChessLanguage.NEDERLANDS Then
                Me.rtbMessage.AppendText(vbCrLf & vbCrLf & "Print a.u.b. dit scherm en stuur dit naar HubvandeLaarr@Hotmail.com," & vbCrLf _
                                                         & "samen met de gebruikte (xpgn of pgn-)bestanden en een korte verklaring van wat u probeerde te doen.")
            Else
                Me.rtbMessage.AppendText(vbCrLf & vbCrLf & "Please print this screen and send this to HubvandeLaarr@Hotmail.com," & vbCrLf _
                                                         & "together with the involved (xpgn of pgn-)files and short explanation about what you tried to do.")
            End If
        End Set
    End Property

    Public Overloads Shared Sub Show(ByVal pException As Exception)
        Dim Form As New frmErrorMessageBox()
        Form.Exception = pException
        Form.CenterToScreen()
        Form.ShowDialog()
    End Sub

    Private Sub cmdOK_Click(ByVal pSender As System.Object, ByVal pArgs As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub MessageBox_Paint(ByVal pSender As Object, ByVal pArgs As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        picCritical.Image = SystemIcons.Error.ToBitmap()
    End Sub

    Public Function RemoveDirectories(pStackTrace As String) As String
        Try
            Dim Lines() As String, Line As String, EndPos As Integer, Startpos As Integer
            Lines = pStackTrace.Split(vbCrLf)
            For I As Integer = 0 To Lines.Count - 1
                Line = Lines(I)
                EndPos = InStrRev(Line, "\")
                Startpos = InStrRev(Line, " in ")
                If Startpos > 0 And EndPos > 0 Then
                    Lines(I) = Strings.Left(Line, Startpos) & "in " & Strings.Mid(Line, EndPos + 1)
                End If
            Next I
            Return String.Join("", Lines)
        Catch
            Return pStackTrace
        End Try
    End Function

End Class
