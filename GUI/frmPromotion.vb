Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports ChessMaterials.ChessPiece
Imports ChessGlobals.ChessColor

Public Class frmPromotion

    Public ChoosenPiece As ChessPiece = Nothing
    Private Color As ChessColor

    Public Overloads Sub ShowDialog(pColor As ChessColor)
        Try
            If CurrentLanguage = ChessLanguage.NEDERLANDS Then
                Me.lblHeader.Text = "Selecteer een van de volgende stukken"
            End If
            Me.Color = pColor

            If pColor = WHITE Then
                Me.picQueen.Image = frmImages.WQueen.Image
                Me.picRook.Image = frmImages.WRook.Image
                Me.picBishop.Image = frmImages.WBishop.Image
                Me.picKnight.Image = frmImages.WKnight.Image
            Else
                Me.picQueen.Image = frmImages.BQueen.Image
                Me.picRook.Image = frmImages.BRook.Image
                Me.picBishop.Image = frmImages.BBishop.Image
                Me.picKnight.Image = frmImages.BKnight.Image
            End If

            MyBase.ShowDialog()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub picQueen_Click(pSender As System.Object, pArgs As System.EventArgs) Handles picQueen.Click
        Me.ChoosenPiece = New Queen(Me.Color)
        Me.Hide()
    End Sub

    Private Sub picRook_Click(pSender As System.Object, pArgs As System.EventArgs) Handles picRook.Click
        Me.ChoosenPiece = New Rook(Me.Color)
        Me.Hide()
    End Sub

    Private Sub picBishop_Click(pSender As System.Object, pArgs As System.EventArgs) Handles picBishop.Click
        Me.ChoosenPiece = New Bishop(Me.Color)
        Me.Hide()
    End Sub

    Private Sub picKnight_Click(pSender As System.Object, pArgs As System.EventArgs) Handles picKnight.Click
        Me.ChoosenPiece = New Knight(Me.Color)
        Me.Hide()
    End Sub

    Private Sub frmPromotion_FormClosing(pSender As Object, pArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If pArgs.CloseReason = CloseReason.UserClosing Then
            Me.ChoosenPiece = New Pawn(Me.Color)
            Me.Hide()
            pArgs.Cancel = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Me.ChoosenPiece = Nothing
        Me.Color = Nothing

        MyBase.Finalize()
    End Sub

End Class