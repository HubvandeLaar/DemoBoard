Imports System.Windows.Forms

Public Class frmAbout

    Private Sub frmAbout_Load(pSender As Object, pArgs As System.EventArgs) Handles Me.Load
        lblApplicationTitle.Text = My.Application.Info.Title
        lblVersion.Text = System.String.Format("Version {0}.{1:00}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        lblCopyright.Text = My.Application.Info.Copyright
    End Sub

    Private Sub cmdOK_Click(ByVal pSender As System.Object, ByVal pArgs As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class
