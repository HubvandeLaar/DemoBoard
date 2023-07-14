Imports System.Windows.Forms
Imports System.Drawing

Public Class frmErrorMessageBox

    Public WriteOnly Property Exception As Exception
        Set(pException As Exception)
            Me.rtbMessage.Text = ""
            Me.rtbMessage.SelectionFont = New Font(rtbMessage.Font.FontFamily, 12, FontStyle.Bold)
            Me.rtbMessage.AppendText(pException.Message)
            Me.rtbMessage.SelectionFont = New Font(rtbMessage.Font.FontFamily, 10, FontStyle.Regular)
            Me.rtbMessage.AppendText(vbCrLf & vbCrLf & pException.StackTrace)
            If pException.InnerException IsNot Nothing Then
                Me.rtbMessage.AppendText(vbCrLf & vbCrLf & pException.InnerException.Message)
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

End Class
