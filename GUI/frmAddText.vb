Option Explicit On

Imports ChessGlobals

Public Class frmAddText

    Public OKPressed As Boolean
    Private Color As String

    Public Property TextColor() As String
        Set(pColor As String)
            Try
                Color = pColor
            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
            End Try
        End Set
        Get
            Return Color
        End Get
    End Property

    Public ReadOnly Property ColoureText() As String
        Get
            Return txtText.Text
        End Get
    End Property

    Private Sub frmAddText_Shown(pSender As Object, pArgs As System.EventArgs) Handles Me.Shown
        OKPressed = False
        picText.Image = frmImages.getImage(Color & "Text")
        txtText.Text = ""
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        OKPressed = True
        Me.Hide()
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        OKPressed = False
        Me.Hide()
    End Sub

    Private Sub frmAddText_Activated(pSender As Object, pArgs As EventArgs) Handles Me.Activated
        txtText.Focus()
    End Sub
End Class