Option Explicit On

Imports ChessGlobals
Imports PGNLibrary
Imports ChessMaterials

Public Class frmEditGame

    Public PGNGame As PGNGame
    Public OKPressed As Boolean

    Public Overloads Sub ShowDialog(pPGNGame As PGNGame)
        Dim TagText(1) As String '2 Columns
        Try
            PGNGame = pPGNGame
            OKPressed = False

            grdTAGs.Rows.Clear()
            For Each Tag As PGNTag In PGNGame.Tags
                TagText(0) = Tag.Key
                TagText(1) = Tag.Value
                grdTAGs.Rows.Add(TagText)
            Next Tag

            If PGNGame.HalfMoves Is Nothing _
            OrElse PGNGame.HalfMoves.FENComment Is Nothing Then
                lblMarkers.Text = ""
                lblArrows.Text = ""
                lblTexts.Text = ""
            Else
                If PGNGame.HalfMoves.FENComment.MarkerList Is Nothing Then
                    lblMarkers.Text = ""
                Else
                    lblMarkers.Text = PGNGame.HalfMoves.FENComment.MarkerList.XPGNString
                End If
                If PGNGame.HalfMoves.FENComment.ArrowList Is Nothing Then
                    lblArrows.Text = ""
                Else
                    lblArrows.Text = PGNGame.HalfMoves.FENComment.ArrowList.XPGNString
                End If
                If PGNGame.HalfMoves.FENComment.TextList Is Nothing Then
                    lblTexts.Text = ""
                Else
                    lblTexts.Text = PGNGame.HalfMoves.FENComment.TextList.XPGNString
                End If
            End If

            Application.DoEvents()
            Call MyBase.ShowDialog()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdOK_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdOK.Click
        Try
            OKPressed = True

            'Update PGNGame
            PGNGame.Tags.Clear()
            For Each Row As DataGridViewRow In grdTAGs.Rows
                PGNGame.Tags.Add(Row.Cells(0).Value, Row.Cells(1).Value)
            Next Row

            If lblMarkers.Text = "" _
            And lblArrows.Text = "" _
            And lblTexts.Text = "" Then
                PGNGame.HalfMoves.FENComment = Nothing
            Else
                If PGNGame.HalfMoves.FENComment Is Nothing Then
                    PGNGame.HalfMoves.FENComment = New PGNComment("")
                End If
                If lblMarkers.Text = "" Then
                    PGNGame.HalfMoves.FENComment.MarkerList = Nothing
                Else
                    PGNGame.HalfMoves.FENComment.MarkerList = New PGNMarkerList(lblMarkers.Text)
                End If
                If lblArrows.Text = "" Then
                    PGNGame.HalfMoves.FENComment.ArrowList = Nothing
                Else
                    PGNGame.HalfMoves.FENComment.ArrowList = New PGNArrowList(lblArrows.Text)
                End If
                If lblTexts.Text = "" Then
                    PGNGame.HalfMoves.FENComment.TextList = Nothing
                Else
                    PGNGame.HalfMoves.FENComment.TextList = New PGNTextList(lblTexts.Text)
                End If
            End If

            Me.Hide()

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub cmdCancel_Click(pSender As System.Object, pArgs As System.EventArgs) Handles cmdCancel.Click
        OKPressed = False
        Me.Hide()
    End Sub

    Private Sub lblMarkers_Click(pSender As Object, pArgs As EventArgs) Handles lblMarkers.Click
        Try
            Dim EditMarkers = New frmEditMarkers()
            EditMarkers.ShowDialog(lblMarkers.Text)
            lblMarkers.Text = EditMarkers.MarkerList.PGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lblArrows_Click(pSender As Object, pArgs As EventArgs) Handles lblArrows.Click
        Try
            Dim EditArrows = New frmEditArrows()
            EditArrows.ShowDialog(lblArrows.Text)
            lblArrows.Text = EditArrows.ArrowList.PGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lblTexts_Click(pSender As Object, pArgs As EventArgs) Handles lblTexts.Click
        Try
            Dim EditTexts = New frmEditTexts()
            EditTexts.ShowDialog(lblTexts.Text)
            lblTexts.Text = EditTexts.TextList.XPGNString
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Function GetRowValue(pKey As String) As String
        For Each Row As DataGridViewRow In grdTAGs.Rows
            If Row.Cells(0).Value = pKey Then
                Return Row.Cells(1).Value
            End If
        Next Row
        Return ""
    End Function

    Protected Overrides Sub Finalize()
        Me.PGNGame = Nothing

        MyBase.Finalize()
    End Sub

End Class