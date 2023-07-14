Public Class frmDockCross

    Public DockStyle As DockStyle

    Private gInActiveColor As Color = Color.FromArgb(255, 234, 113) 'Color.Beige
    Private gActiveColor As Color = Color.Gold

    Public Sub CenterForm(pctlTabControl As ctlTabControl)
        With pctlTabControl.Parent.ClientRectangle
            Dim DockingLocation As Point = New Point(.Left + (.Width - Me.Width) / 2,
                                                         .Top + (.Height - Me.Height) / 2)
            Me.Location = pctlTabControl.Parent.PointToScreen(DockingLocation)
        End With
    End Sub

    Public Sub UpdateAppearance(pPoint As Point)
        If WithinBoundary(lblLeft, pPoint) Then
            Me.DockStyle = DockStyle.Left
            lblGoldLeft.BackColor = gActiveColor
            lblGoldRight.BackColor = gInActiveColor
            lblGoldTop.BackColor = gInActiveColor
            lblGoldBottom.BackColor = gInActiveColor
            lblGoldCenter.BackColor = gInActiveColor
        ElseIf WithinBoundary(lblRight, pPoint) Then
            Me.DockStyle = DockStyle.Right
            lblGoldRight.BackColor = gActiveColor
            lblGoldLeft.BackColor = gInActiveColor
            lblGoldTop.BackColor = gInActiveColor
            lblGoldBottom.BackColor = gInActiveColor
            lblGoldCenter.BackColor = gInActiveColor
        ElseIf WithinBoundary(lblTop, pPoint) Then
            Me.DockStyle = DockStyle.Top
            lblGoldTop.BackColor = gActiveColor
            lblGoldLeft.BackColor = gInActiveColor
            lblGoldRight.BackColor = gInActiveColor
            lblGoldBottom.BackColor = gInActiveColor
            lblGoldCenter.BackColor = gInActiveColor
        ElseIf WithinBoundary(lblBottom, pPoint) Then
            Me.DockStyle = DockStyle.Bottom
            lblGoldBottom.BackColor = gActiveColor
            lblGoldLeft.BackColor = gInActiveColor
            lblGoldRight.BackColor = gInActiveColor
            lblGoldTop.BackColor = gInActiveColor
            lblGoldCenter.BackColor = gInActiveColor
        ElseIf WithinBoundary(lblCenter, pPoint) Then
            Me.DockStyle = DockStyle.Fill
            lblGoldCenter.BackColor = gActiveColor
            lblGoldLeft.BackColor = gInActiveColor
            lblGoldRight.BackColor = gInActiveColor
            lblGoldTop.BackColor = gInActiveColor
            lblGoldBottom.BackColor = gInActiveColor
        Else
            Me.DockStyle = DockStyle.None
            lblGoldLeft.BackColor = gInActiveColor
            lblGoldRight.BackColor = gInActiveColor
            lblGoldTop.BackColor = gInActiveColor
            lblGoldBottom.BackColor = gInActiveColor
            lblGoldCenter.BackColor = gInActiveColor
        End If
        Me.Refresh()
    End Sub

    Private Function WithinBoundary(pControl As Control, pPoint As Point) As Boolean
        If pPoint.X >= pControl.Left And pPoint.X <= (pControl.Left + pControl.Width) _
        And pPoint.Y >= pControl.Top And pPoint.Y <= (pControl.Top + pControl.Height) Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get ' Make background transparent
            Dim mCreateParams As CreateParams = MyBase.CreateParams
            mCreateParams.ExStyle = mCreateParams.ExStyle Or &H20
            Return mCreateParams
        End Get
    End Property

    Protected Overrides Sub OnPaintBackground(pArgs As PaintEventArgs)
        'Only if the backColor is not Transparent, call MyBase.OnPaintBackground
        'If Me.BackColor <> Color.Transparent Then
        '   MyBase.OnPaintBackground(pArgs)
        'End If
    End Sub

    Public Sub New(pParentForm As Form)
        InitializeComponent()

        Me.Left = pParentForm.Left + ((pParentForm.ClientSize.Width - Me.Width) / 2)
        Me.Top = pParentForm.Top + ((pParentForm.ClientSize.Height - Me.Height) / 2)
        Me.Show()
    End Sub

End Class