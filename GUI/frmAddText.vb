Option Explicit On

Imports ChessGlobals

Public Class frmAddText

    Public OKPressed As Boolean

    Public Property TextColor() As String
        Set(pColor As String)
            Try
                Select Case pColor
                    Case "G"
                        optGreenText.Checked = True
                        optYellowText.Checked = False
                        optRedText.Checked = False
                    Case "Y"
                        optGreenText.Checked = False
                        optYellowText.Checked = True
                        optRedText.Checked = False
                    Case "R"
                        optGreenText.Checked = False
                        optYellowText.Checked = False
                        optRedText.Checked = True
                End Select
            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
            End Try
        End Set
        Get
            Try
                If optGreenText.Checked = True Then Return "G"
                If optYellowText.Checked = True Then Return "Y"
                If optRedText.Checked = True Then Return "R"
                Return ""
            Catch pException As Exception
                frmErrorMessageBox.Show(pException)
                Return ""
            End Try
        End Get
    End Property

    Public ReadOnly Property ColoureText() As String
        Get
            Return txtText.Text
        End Get
    End Property

    Private Sub optGreenText_CheckedChanged(pSender As System.Object, pArgs As System.EventArgs) Handles optGreenText.CheckedChanged
        If optGreenText.Checked = True Then
            picText.Image = frmImages.GText.Image
        End If
    End Sub

    Private Sub optYellowText_CheckedChanged(pSender As System.Object, pArgs As System.EventArgs) Handles optYellowText.CheckedChanged
        If optYellowText.Checked = True Then
            picText.Image = frmImages.YText.Image
        End If
    End Sub

    Private Sub optRedText_CheckedChanged(pSender As System.Object, pArgs As System.EventArgs) Handles optRedText.CheckedChanged
        If optRedText.Checked = True Then
            picText.Image = frmImages.RText.Image
        End If
    End Sub

    Private Sub frmAddText_Shown(pSender As Object, pArgs As System.EventArgs) Handles Me.Shown
        OKPressed = False
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