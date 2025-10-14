Option Explicit On

Imports ChessMessaging
Imports PGNLibrary

Public Class frmEditGame

    Private gPGNGame As PGNGame

    Public Property OKPressed As Boolean

    Public Overloads Sub ShowDialog(pPGNGame As PGNGame)
        Dim TagText(1) As String '2 Columns
        Try
            gPGNGame = pPGNGame
            OKPressed = False

            grdTAGs.Rows.Clear()
            For Each Tag As PGNTag In gPGNGame.Tags
                TagText(0) = Tag.Key
                TagText(1) = Tag.Value
                grdTAGs.Rows.Add(TagText)
            Next Tag

            If gPGNGame.HalfMoves Is Nothing _
            OrElse gPGNGame.HalfMoves.FENComment Is Nothing Then
                lblMarkers.Text = ""
                lblArrows.Text = ""
                lblTexts.Text = ""
            Else
                If gPGNGame.HalfMoves.FENComment.MarkerList Is Nothing Then
                    lblMarkers.Text = ""
                Else
                    lblMarkers.Text = gPGNGame.HalfMoves.FENComment.MarkerList.XPGNString
                End If
                If gPGNGame.HalfMoves.FENComment.ArrowList Is Nothing Then
                    lblArrows.Text = ""
                Else
                    lblArrows.Text = gPGNGame.HalfMoves.FENComment.ArrowList.XPGNString
                End If
                If gPGNGame.HalfMoves.FENComment.TextList Is Nothing Then
                    lblTexts.Text = ""
                Else
                    lblTexts.Text = gPGNGame.HalfMoves.FENComment.TextList.XPGNString
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

            gPGNGame.HalfMoves.RemoveResult()
            'Update PGNGame
            gPGNGame.Tags.Clear()
            For Each Row As DataGridViewRow In grdTAGs.Rows
                gPGNGame.Tags.Add(Row.Cells(0).Value, Row.Cells(1).Value)
                If Row.Cells(0).Value = "Result" Then
                    gPGNGame.HalfMoves.UpdateResult(Row.Cells(1).Value)
                End If
            Next Row

            If lblMarkers.Text = "" _
            And lblArrows.Text = "" _
            And lblTexts.Text = "" Then
                gPGNGame.HalfMoves.FENComment = Nothing
            Else
                If gPGNGame.HalfMoves.FENComment Is Nothing Then
                    gPGNGame.HalfMoves.FENComment = New PGNComment("")
                End If
                If lblMarkers.Text = "" Then
                    gPGNGame.HalfMoves.FENComment.MarkerList = Nothing
                Else
                    gPGNGame.HalfMoves.FENComment.MarkerList = New PGNMarkerList(lblMarkers.Text)
                End If
                If lblArrows.Text = "" Then
                    gPGNGame.HalfMoves.FENComment.ArrowList = Nothing
                Else
                    gPGNGame.HalfMoves.FENComment.ArrowList = New PGNArrowList(lblArrows.Text)
                End If
                If lblTexts.Text = "" Then
                    gPGNGame.HalfMoves.FENComment.TextList = Nothing
                Else
                    gPGNGame.HalfMoves.FENComment.TextList = New PGNTextList(lblTexts.Text)
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
            Using frmEditMarkers = New frmEditMarkers()
                frmEditMarkers.ShowDialog(lblMarkers.Text)
                lblMarkers.Text = frmEditMarkers.MarkerList.PGNString
            End Using

        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lblArrows_Click(pSender As Object, pArgs As EventArgs) Handles lblArrows.Click
        Try
            Using frmEditArrows = New frmEditArrows()
                frmEditArrows.ShowDialog(lblArrows.Text)
                lblArrows.Text = frmEditArrows.ArrowList.PGNString
            End Using
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    Private Sub lblTexts_Click(pSender As Object, pArgs As EventArgs) Handles lblTexts.Click
        Try
            Using frmEditTexts = New frmEditTexts()
                frmEditTexts.ShowDialog(lblTexts.Text)
                lblTexts.Text = frmEditTexts.TextList.XPGNString
            End Using
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
        End Try
    End Sub

    ''' <summary>Returns the Value from the grdTags-Grid of a specific TAG-Key</summary>
    Private Function GetRowValue(pKey As String) As String
        For Each Row As DataGridViewRow In grdTAGs.Rows
            If Row.Cells(0).Value = pKey Then
                Return Row.Cells(1).Value
            End If
        Next Row
        Return ""
    End Function

    Protected Overrides Sub Finalize()
        gPGNGame = Nothing

        MyBase.Finalize()
    End Sub

End Class