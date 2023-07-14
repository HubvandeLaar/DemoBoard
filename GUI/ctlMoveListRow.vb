Imports ChessGlobals
Imports ChessGlobals.ChessColor
Imports PGNLibrary
Imports PGNLibrary.PGNNAG
Imports PGNLibrary.PGNNAG.NAGPrintPosition

Public Class ctlMoveListRow

    Private gWhiteHalfMove As PGNHalfMove
    Private gBlackHalfMove As PGNHalfMove
    Private gWhiteSelected As Boolean = False
    Private gBlackSelected As Boolean = False
    Private gPerceptable As Boolean = False
    Private gExpandable As Boolean = True
    Private gExpanded As Boolean = False

    Public Event RightClicked(pSender As ctlMoveListRow, pHalfMove As PGNHalfMove)
    Public Event Clicked(pSender As ctlMoveListRow, pHalfMove As PGNHalfMove)
    Public Event ExpandClicked(pSender As ctlMoveListRow)
    Public Event CollapseClicked(pSender As ctlMoveListRow)

    Public Property WhiteSelected As Boolean
        Set(pWhiteSelected As Boolean)
            gWhiteSelected = pWhiteSelected
            rtbWhiteMoveText.Select()
            If pWhiteSelected = True Then
                rtbWhiteMoveText.BackColor = Color.MidnightBlue
                rtbWhiteMoveText.ForeColor = Color.White
            Else
                rtbWhiteMoveText.BackColor = SystemColors.Info
                rtbWhiteMoveText.ForeColor = SystemColors.ControlText
            End If
            rtbWhiteMoveText.SelectionStart = 0
            rtbWhiteMoveText.SelectionLength = 0
        End Set
        Get
            Return gWhiteSelected
        End Get
    End Property

    Public Property BlackSelected As Boolean
        Set(pBlackSelected As Boolean)
            gBlackSelected = pBlackSelected
            rtbBlackMoveText.Select()
            If pBlackSelected = True Then
                rtbBlackMoveText.BackColor = Color.MidnightBlue
                rtbBlackMoveText.ForeColor = Color.White
            Else
                rtbBlackMoveText.BackColor = SystemColors.Info
                rtbBlackMoveText.ForeColor = SystemColors.ControlText
            End If
            rtbBlackMoveText.SelectionStart = 0
            rtbBlackMoveText.SelectionLength = 0
        End Set
        Get
            Return gBlackSelected
        End Get
    End Property

    ''' <summary> Perceptable replaces Visible.  
    ''' Because Visible is set to False when Form is hidden.
    ''' To determine what was originally Visible is difficult </summary>
    Public Property Perceptable As Boolean
        Set(pPerceptable As Boolean)
            gPerceptable = pPerceptable
        End Set
        Get
            Return gPerceptable
        End Get
    End Property

    Public Property Expandable As Boolean
        Set(pExpandable As Boolean)
            gExpandable = pExpandable
            cmdExpand.Visible = gExpandable
            cmdCollapse.Visible = gExpandable
        End Set
        Get
            Return gExpandable
        End Get
    End Property

    Public Property Expanded As Boolean
        Set(pExpanded As Boolean)
            gExpanded = pExpanded
            If Me.Expandable = False Then
                gExpanded = False
                cmdExpand.Visible = False
                cmdCollapse.Visible = False
                Exit Property
            End If
            If gExpanded = True Then
                cmdExpand.Visible = False
                cmdCollapse.Visible = True
                Me.ReArrange()
            Else
                cmdExpand.Visible = True
                cmdCollapse.Visible = False
                Me.ReArrange()
            End If
        End Set
        Get
            Return gExpanded
        End Get
    End Property

    Public Property WhiteHalfMove As PGNHalfMove
        Set(pWhiteHalfMove As PGNHalfMove)
            gWhiteHalfMove = pWhiteHalfMove
            If pWhiteHalfMove Is Nothing Then
                Me.Expandable = False
                Me.Perceptable = True
                Exit Property
            End If
            If gBlackHalfMove IsNot Nothing _
            AndAlso gBlackHalfMove.CommentBefore.Text <> "" Then
                Throw New Exception("CommentBefore already belongs to BlackMove !")
            End If

            'Setting Expandablebility and initial Visibility
            If pWhiteHalfMove.Result <> "" Then
                Me.rtbMoveNumber.Text = pWhiteHalfMove.Result
                Me.Expandable = False
                Me.Perceptable = True
                Me.ReArrange()
                Exit Property

            ElseIf pWhiteHalfMove.VariantLevel = 0 Then _
                'Main Variant Move
                Me.Expandable = False
                Me.Perceptable = True

            Else
                If pWhiteHalfMove.IsFirstMoveInVariant = True Then
                    If pWhiteHalfMove.VariantLevel = 1 Then
                        Me.Expandable = True
                        Me.Perceptable = True
                    Else
                        Me.Expandable = True
                        Me.Perceptable = False
                    End If
                Else
                    'Second or Next Move in Variant
                    Me.Expandable = False
                    Me.Perceptable = False
                End If
            End If

            'Transfer 
            If pWhiteHalfMove.CommentBefore Is Nothing Then
                rtbCommentBefore.Text = ""
            Else
                rtbCommentBefore.Text = pWhiteHalfMove.CommentBefore.Text(True)
            End If

            rtbMoveNumber.Clear()
            rtbMoveNumber.Text = pWhiteHalfMove.MoveNr & "."

            rtbWhiteMoveText.Clear()
            If pWhiteHalfMove.Result <> "" Then
                rtbWhiteMoveText.AppendText(pWhiteHalfMove.MoveText(CurrentLanguage))

            Else
                If pWhiteHalfMove.NAGs.Count(BEFORE) > 0 Then
                    Me.PrintNAGs(pWhiteHalfMove, BEFORE, rtbWhiteMoveText)
                End If
                rtbWhiteMoveText.AppendText(pWhiteHalfMove.MoveText(CurrentLanguage))
                If pWhiteHalfMove.NAGs.Count(AFTER) > 0 Then
                    Me.PrintNAGs(pWhiteHalfMove, AFTER, rtbWhiteMoveText)
                End If
            End If

            If pWhiteHalfMove.VariantLevel = 0 Then
                rtbMoveNumber.Font = New Font(rtbMoveNumber.Font.FontFamily, rtbMoveNumber.Font.Size, FontStyle.Bold)
                rtbWhiteMoveText.Font = New Font(rtbWhiteMoveText.Font.FontFamily, rtbWhiteMoveText.Font.Size, FontStyle.Bold)
            Else
                rtbMoveNumber.Font = New Font(rtbMoveNumber.Font.FontFamily, rtbMoveNumber.Font.Size, FontStyle.Regular)
                rtbWhiteMoveText.Font = New Font(rtbWhiteMoveText.Font.FontFamily, rtbWhiteMoveText.Font.Size, FontStyle.Regular)
            End If

            UpdateFigurines(rtbWhiteMoveText, rtbWhiteMoveText.Font.Size * 1.2)

            If pWhiteHalfMove.CommentAfter Is Nothing Then
                rtbCommentAfter.Text = ""
            Else
                rtbCommentAfter.Text = pWhiteHalfMove.CommentAfter.Text(True)
            End If

            Me.ReArrange()
        End Set
        Get
            Return gWhiteHalfMove
        End Get
    End Property

    Public Property BlackHalfMove As PGNHalfMove
        Set(pBlackHalfMove As PGNHalfMove)
            gBlackHalfMove = pBlackHalfMove
            If pBlackHalfMove Is Nothing Then
                Me.rtbBlackMoveText.Clear()
                Exit Property
            End If

            If gWhiteHalfMove Is Nothing Then
                Me.rtbWhiteMoveText.Text = ". . ."
            End If

            If gWhiteHalfMove IsNot Nothing _
            AndAlso gWhiteHalfMove.CommentAfter IsNot Nothing _
            AndAlso gWhiteHalfMove.CommentAfter.Text <> "" Then
                Throw New Exception("CommentAfter already belongs to WhiteMove !")
            End If

            'Setting Expandability and initial Visibility
            If pBlackHalfMove.VariantLevel = 0 Then _
                'Main Variant Move
                Me.Expandable = False
                Me.Perceptable = True

            Else
                If pBlackHalfMove.IsFirstMoveInVariant = True Then
                    'VariantLevel an Number are different, so first move on the row 
                    If Me.Expandable = True Then
                        'Already expandable because of WhiteMove
                        Throw New Exception("Two expanble moves in one Listrow !")
                    End If
                    Me.Expandable = True
                    If pBlackHalfMove.VariantLevel = 1 Then
                        Me.Perceptable = True
                    Else
                        Me.Perceptable = False
                    End If

                Else
                    'Second or Next Move in Variant
                    If WhiteHalfMove IsNot Nothing Then
                        'White move is leading for expandable and Perceptable
                    Else
                        Me.Expandable = False
                        Me.Perceptable = False
                    End If
                End If
            End If

            'Transfer 
            If pBlackHalfMove.CommentBefore Is Nothing Then
                rtbCommentBefore.Text = ""
            Else
                rtbCommentBefore.Text = pBlackHalfMove.CommentBefore.Text(True)
            End If

            rtbMoveNumber.Clear()
            rtbMoveNumber.Text = pBlackHalfMove.MoveNr & "."

            rtbBlackMoveText.Clear()
            If pBlackHalfMove.Result <> "" Then
                rtbBlackMoveText.AppendText(pBlackHalfMove.MoveText(CurrentLanguage))
            Else
                If pBlackHalfMove.NAGs.Count(BEFORE) > 0 Then
                    Me.PrintNAGs(pBlackHalfMove, BEFORE, rtbBlackMoveText)
                End If
                rtbBlackMoveText.AppendText(pBlackHalfMove.MoveText(CurrentLanguage))
                If pBlackHalfMove.NAGs.Count(AFTER) > 0 Then
                    Me.PrintNAGs(pBlackHalfMove, AFTER, rtbBlackMoveText)
                End If
            End If

            If pBlackHalfMove.VariantLevel = 0 Then
                rtbMoveNumber.Font = New Font(rtbMoveNumber.Font.FontFamily, rtbMoveNumber.Font.Size, FontStyle.Bold)
                rtbBlackMoveText.Font = New Font(rtbBlackMoveText.Font.FontFamily, rtbBlackMoveText.Font.Size, FontStyle.Bold)
            Else
                rtbMoveNumber.Font = New Font(rtbMoveNumber.Font.FontFamily, rtbMoveNumber.Font.Size, FontStyle.Regular)
                rtbBlackMoveText.Font = New Font(rtbBlackMoveText.Font.FontFamily, rtbBlackMoveText.Font.Size, FontStyle.Regular)
            End If

            UpdateFigurines(rtbBlackMoveText, rtbBlackMoveText.Font.Size * 1.2)

            If pBlackHalfMove.CommentAfter Is Nothing Then
                rtbCommentAfter.Text = ""
            Else
                rtbCommentAfter.Text = pBlackHalfMove.CommentAfter.Text(True)
            End If

            Me.ReArrange()
        End Set
        Get
            Return gBlackHalfMove
        End Get
    End Property

    Public ReadOnly Property VariantLevel As Long
        Get
            If Me.WhiteHalfMove IsNot Nothing Then
                Return Me.WhiteHalfMove.VariantLevel
            ElseIf Me.BlackHalfMove IsNot Nothing Then
                Return Me.BlackHalfMove.VariantLevel
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property VariantNumber As Integer
        Get
            If Me.WhiteHalfMove IsNot Nothing Then
                Return Me.WhiteHalfMove.VariantNumber
            ElseIf Me.BlackHalfMove IsNot Nothing Then
                Return Me.BlackHalfMove.VariantNumber
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property PreviousHalfMove As PGNHalfMove
        Get
            If Me.WhiteHalfMove IsNot Nothing Then
                Return Me.WhiteHalfMove.PreviousHalfMove
            ElseIf Me.BlackHalfMove IsNot Nothing Then
                Return Me.BlackHalfMove.PreviousHalfMove
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub UpdateFigurines(pMoveText As RichTextBox, pFontSize As Single)
        Dim Font As Font = KNSBFigurine(pFontSize)
        For I = 0 To pMoveText.TextLength - 1
            pMoveText.Select(I, 1)
            If pMoveText.SelectionFont.Name <> pMoveText.Font.Name Then
                Continue For
            End If
            Select Case pMoveText.SelectedText
                Case "K", "D", "T", "L", "P",
                     "K", "Q", "R", "B", "N"
                    pMoveText.SelectionFont = New Font(Font, FontStyle.Regular)
            End Select
        Next I
    End Sub

    Public Sub New(pHalfMove As PGNHalfMove)
        ' This call is required by the designer.
        InitializeComponent()
        Me.rtbCommentBefore.Clear()
        Me.rtbWhiteMoveText.Clear()
        Me.rtbBlackMoveText.Clear()
        Me.rtbCommentAfter.Clear()
        If pHalfMove.Result <> "" Then
            Me.WhiteHalfMove = pHalfMove
            Me.BlackHalfMove = Nothing
        ElseIf pHalfMove.Color = WHITE Then
            Me.WhiteHalfMove = pHalfMove
            Me.BlackHalfMove = Nothing
        Else
            Me.WhiteHalfMove = Nothing
            Me.BlackHalfMove = pHalfMove
        End If
    End Sub

    Public Function BlackMoveFits(pHalfMove As PGNHalfMove) As Boolean
        If pHalfMove.Result <> "" Then
            Return False
        ElseIf Me.BlackHalfMove IsNot Nothing Then
            Return False
        ElseIf Me.rtbCommentAfter.Text <> "" Then
            Return False
        ElseIf pHalfMove.CommentBefore IsNot Nothing _
        AndAlso pHalfMove.CommentBefore.Text <> "" Then
            Return False
        ElseIf gWhiteHalfMove IsNot Nothing Then
            If gWhiteHalfMove.VariantLevel <> pHalfMove.VariantLevel _
            OrElse gWhiteHalfMove.VariantNumber <> pHalfMove.VariantNumber Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub PrintNAGs(pHalfMove As PGNHalfMove, pPrintPosition As NAGPrintPosition, pMoveText As RichTextBox)
        Dim NAGText As String
        For Each NAG As PGNNAG In pHalfMove.NAGs
            If NAG.PrintPosition = pPrintPosition Then
                Select Case NAG.Type
                    Case NAGType.CODE 'Inserting one Symbol specified by pTextacterNumber
                        pMoveText.SelectionStart = pMoveText.TextLength 'Set Start to End of Text
                        pMoveText.AppendText(ChrW(NAG.Code))
                        pMoveText.SelectionLength = 1
                        pMoveText.SelectionFont = New Font(NAG.Font, 14)
                    Case NAGType.TEXT 'Inserting the text using the default font for the current style
                        NAGText = NAG.Text(CurrentLanguage)
                        If NAGText <> "" Then
                            pMoveText.SelectionStart = pMoveText.TextLength 'Set Start to End of Text
                            pMoveText.AppendText(NAGText & " ")
                            pMoveText.SelectionLength = Len(NAGText)
                            pMoveText.SelectionFont = New Font(NAG.Font, pMoveText.Font.Size)
                        End If
                End Select
            End If
        Next NAG
    End Sub

    Public Sub ReArrange(Optional pHideFrom As Long = Long.MaxValue)
        'Calculate Heights
        If Me.Perceptable = False Then
            rtbCommentBefore.Height = 0
            rtbMoveNumber.Height = 0
            rtbWhiteMoveText.Height = 0
            rtbBlackMoveText.Height = 0
            rtbCommentAfter.Height = 0
            Application.DoEvents()
            Me.Height = 0
        Else
            'Perceptable rows
            rtbMoveNumber.Height = RichTextBoxHeight(rtbMoveNumber) * 1.2
            rtbWhiteMoveText.Height = RichTextBoxHeight(rtbWhiteMoveText) * 1.2
            rtbBlackMoveText.Height = RichTextBoxHeight(rtbBlackMoveText) * 1.2
            If Expandable = False Or Expanded = True Then
                rtbCommentBefore.Height = RichTextBoxHeight(rtbCommentBefore)
                rtbCommentAfter.Height = RichTextBoxHeight(rtbCommentAfter)
            Else
                rtbCommentBefore.Height = 0
                rtbCommentAfter.Height = 0
                If Me.WhiteHalfMove IsNot Nothing Then
                    rtbBlackMoveText.Height = 0
                End If
            End If
            Application.DoEvents()

            'When TRAINING is active, all move with higher index than pHideFrom should bee hidden
            If WhiteHalfMove IsNot Nothing _
            AndAlso WhiteHalfMove.Index > pHideFrom Then
                rtbWhiteMoveText.Height = 0 'Hide
                rtbCommentBefore.Height = 0
                If BlackHalfMove Is Nothing Then
                    rtbCommentAfter.Height = 0
                End If
            End If
            If BlackHalfMove IsNot Nothing _
            AndAlso BlackHalfMove.Index > pHideFrom Then
                rtbBlackMoveText.Height = 0 'Hide
                rtbCommentAfter.Height = 0
                If WhiteHalfMove Is Nothing Then
                    rtbCommentBefore.Height = 0
                    rtbWhiteMoveText.Height = 0 'Is filled with ". . ."
                End If
            End If
            If rtbWhiteMoveText.Height = 0 _
            AndAlso rtbBlackMoveText.Height = 0 Then
                rtbMoveNumber.Height = 0
            End If

            'Resize myself
            Me.Height = rtbCommentBefore.Top + rtbCommentBefore.Height _
                      + Math.Max(rtbMoveNumber.Height, Math.Max(rtbWhiteMoveText.Height, rtbBlackMoveText.Height)) _
                      + rtbCommentAfter.Height + 4
        End If
        'Rearrange Labels
        rtbMoveNumber.Top = rtbCommentBefore.Top + rtbCommentBefore.Height
        rtbWhiteMoveText.Top = rtbMoveNumber.Top
        rtbBlackMoveText.Top = rtbMoveNumber.Top
        rtbCommentAfter.Top = rtbMoveNumber.Top + rtbMoveNumber.Height
        '                        + Math.Max(rtbWhiteMoveText.Height, rtbBlackMoveText.Height)
        rtbWhiteMoveText.Width = Math.Min((Width - rtbWhiteMoveText.Left) / 2, 80)
        rtbBlackMoveText.Width = rtbWhiteMoveText.Width
        rtbBlackMoveText.Left = rtbWhiteMoveText.Left + rtbWhiteMoveText.Width
    End Sub

    Private Sub cmdExpand_Click(pSender As Object, pArgs As EventArgs) Handles cmdExpand.Click
        Me.Expanded = True
        RaiseEvent ExpandClicked(Me)
    End Sub

    Private Sub cmdCollapse_Click(pSender As Object, pArgs As EventArgs) Handles cmdCollapse.Click
        Me.Expanded = False
        RaiseEvent CollapseClicked(Me)
    End Sub

    Private Sub rtbWhiteMoveText_MouseDown(pSender As Object, pArgs As MouseEventArgs) Handles rtbWhiteMoveText.MouseDown
        If gWhiteHalfMove IsNot Nothing _
        AndAlso pArgs.Button = MouseButtons.Right Then
            RaiseEvent RightClicked(Me, gWhiteHalfMove)
        End If
    End Sub

    Private Sub rtbBlackMoveText_MouseDown(pSender As Object, pArgs As MouseEventArgs) Handles rtbBlackMoveText.MouseDown
        If gBlackHalfMove IsNot Nothing _
        AndAlso pArgs.Button = MouseButtons.Right Then
            RaiseEvent RightClicked(Me, gBlackHalfMove)
        End If
    End Sub

    Private Sub rtbWhiteMoveText_Click(pSender As Object, pArgs As EventArgs) Handles rtbWhiteMoveText.Click
        If gWhiteHalfMove IsNot Nothing Then
            RaiseEvent Clicked(Me, Me.WhiteHalfMove)
        End If
    End Sub

    Private Sub rtbBlackMoveText_Click(pSender As Object, pArgs As EventArgs) Handles rtbBlackMoveText.Click
        If gBlackHalfMove IsNot Nothing Then
            RaiseEvent Clicked(Me, Me.BlackHalfMove)
        End If
    End Sub

    Private Function RichTextBoxHeight(pRichTextBox As RichTextBox)
        If pRichTextBox.Text = "" Then
            Return 0
        End If
        Dim RichTextBoxSize As Size = New Size(pRichTextBox.Width, Int32.MaxValue)
        RichTextBoxSize = TextRenderer.MeasureText(pRichTextBox.Text, pRichTextBox.Font, RichTextBoxSize, TextFormatFlags.WordBreak)
        Return RichTextBoxSize.Height
    End Function

End Class
