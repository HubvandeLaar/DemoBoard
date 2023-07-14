Option Explicit On
Imports ChessGlobals

Public Class ctlTabControl

    Public Shadows Event TabPageDragging(pctlTabControl As ctlTabControl, pMouseLocation As Point)
    Public Shadows Event MouseUp(pctlTabControl As ctlTabControl, pMouseLocation As Point)
    Public Shadows Event MouseEnter(pctlTabControl)
    Public Event TabPageStartDragging(pctlTabControl As ctlTabControl, pMouseLocation As Point)
    Public Event TabPageDropped(pFrom As Form)
    Public Event TabPageRemoved(pForm As Form)

    'Needed to enable ForwardedMouseUp and ForwardedMousePanelLeave
    Public WithEvents gfrmMainForm As frmMainForm

    Private MouseDownLocation As Point = Nothing
    Private gPanelCloseIconLocation As Point = New Point(-18, 5)

    Private BeforeImage As String = ""

    Public ReadOnly Property SubForm(pIndex As Integer) As Form
        Get
            For Each Control As Control In Me.TabControl.TabPages(pIndex).Controls
                If Control.GetType.BaseType.Name = "Form" Then
                    Return Control
                End If
            Next Control
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property SelectedIndex() As Integer
        Get
            Return Me.TabControl.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property SelectedTab() As TabPage
        Get
            Return Me.TabControl.SelectedTab
        End Get
    End Property

    Public Sub AddTabPage(pDragPanel As Panel, Optional pIndex As Integer = -1)
        AddTabPage(CType(pDragPanel.Controls(0), Form), pIndex) 'Form is first and only control at DragPanel !
    End Sub

    Public Sub AddTabPage(pForm As Form, Optional pIndex As Integer = -1)
        Dim TabPage As New TabPage(pForm.Text)
        pForm.TopLevel = False
        pForm.FormBorderStyle = FormBorderStyle.None
        pForm.Visible = True
        pForm.Dock = DockStyle.Fill
        TabPage.Text = pForm.Text
        TabPage.Controls.Add(pForm)
        If pIndex = -1 Then
            Me.TabControl.TabPages.Add(TabPage)
            Me.TabControl.SelectedIndex = Me.TabControl.TabCount - 1
        Else
            Me.TabControl.TabPages.Insert(pIndex, TabPage)
            Me.TabControl.SelectedIndex = pIndex
        End If
    End Sub

    Public Sub SelectTab(pTabName As String)
        For Index As Integer = 0 To Me.TabControl.TabPages.Count - 1
            If Me.TabControl.TabPages(Index).Text = pTabName Then
                Me.TabControl.SelectTab(Index)
                Application.DoEvents()
                Exit Sub
            End If
        Next Index
    End Sub

    Public Sub UnselectTab(pTabName As String)
        For Index As Integer = 0 To Me.TabControl.TabPages.Count - 1
            If Me.TabControl.TabPages(Index).Text <> pTabName Then
                Me.TabControl.SelectTab(Index)
                Application.DoEvents()
                Exit Sub
            End If
        Next Index
    End Sub

    Public Sub RemoveTab(pTabPage As TabPage)
        RemoveTab(pTabPage, pTabPage.Controls(0))
    End Sub

    Public Sub RemoveTab(pIndex As Integer, pForm As Form)
        RemoveTab(Me.TabControl.TabPages(pIndex), pForm)
    End Sub

    Public Sub RemoveTab(pTabPage As TabPage, pForm As Form)
        If pTabPage.Controls.Count > 0 Then
            RaiseEvent TabPageRemoved(pForm)
        End If

        Me.TabControl.TabPages.Remove(pTabPage)
        If Me.TabControl.TabPages.Count = 0 Then
            RemoveTabControl()
        End If
    End Sub

    Private Sub ctlTabControl_Load(pSender As Object, pArgs As EventArgs) Handles Me.Load
        'Needed to enable ForwardedMouseUp and ForwardedMousePanelLeave
        gfrmMainForm = frmMainForm

        Me.Dock = DockStyle.Fill
    End Sub

    Private Sub gParentForm_RemoveForm(pForm As Form) Handles gfrmMainForm.RemoveForm
        For Index As Integer = 0 To Me.TabControl.TabPages.Count - 1
            If Me.TabControl.TabPages(Index).Controls.Count > 0 Then
                If TypeName(Me.TabControl.TabPages(Index).Controls(0)) = TypeName(pForm) Then
                    RaiseEvent TabPageRemoved(pForm)
                    Me.TabControl.TabPages.Remove(Me.TabControl.TabPages(Index))
                    Exit For
                End If
            End If
        Next Index
        If Me.TabControl.TabPages.Count = 0 Then
            RemoveTabControl()
        End If
    End Sub

    Private Sub gfrmMainForm_LanguageChanged(pLanguage As ChessLanguage) Handles gfrmMainForm.LanguageChanged
        'Update all TabText
        Dim SubForm As Form
        For Index As Integer = 0 To Me.TabControl.TabCount - 1
            SubForm = Me.TabControl.TabPages(Index).Controls(0)

            Me.TabControl.TabPages(Index).Text = SubForm.Text
            DrawText(TabControl, Index)
            DrawCloseIcon(TabControl, Index)
        Next Index
    End Sub

    Private Sub RemoveTabControl()
        Dim OtherControl As Control
        If TypeName(Me.Parent.Parent) = "frmMainForm" Then
            'I'm the Top of the tree
            Exit Sub
        End If
        If TypeName(Me.Parent.Parent.Parent) = "ctlSplitContainer" Then
            'ctlTabControl with 0 pages (Me) is part of a ctlSplitContainer
            Dim ctlSplitContainer As ctlSplitContainer = Me.Parent.Parent.Parent
            If ctlSplitContainer.Panel1 Is Me Then
                'NB The Other Control can also be a SplitContainer 
                OtherControl = ctlSplitContainer.Panel2
            ElseIf ctlSplitContainer.Panel2 Is Me Then
                'NB The Other Control can also be a SplitContainer 
                OtherControl = ctlSplitContainer.Panel1
            Else
                Exit Sub
            End If

            If ctlSplitContainer.Parent Is Nothing Then
                Exit Sub
            ElseIf TypeName(ctlSplitContainer.Parent.Parent) = "frmMainForm" Then
                OtherControl.Parent = gfrmMainForm.pnlMainPanel
                gfrmMainForm.pnlMainPanel.Controls.Remove(ctlSplitContainer)
            Else
                Dim ParentctlSplitContainer As ctlSplitContainer = ctlSplitContainer.Parent.Parent.Parent
                If ParentctlSplitContainer.Panel1 Is ctlSplitContainer Then
                    ParentctlSplitContainer.Panel1 = OtherControl 'Replace existing control with OtherControl
                ElseIf ParentctlSplitContainer.Panel2 Is ctlSplitContainer Then
                    ParentctlSplitContainer.Panel2 = OtherControl 'Replace existing control with OtherControl
                End If
            End If

        End If
    End Sub

    Public Sub Serialize(pWriter As System.IO.TextWriter, pLevel As Integer)
        pWriter.WriteLine(Space(pLevel * 3) & "<ctlTabControl Level=""" & Format(pLevel) & """>")
        For Index As Integer = 0 To Me.TabControl.TabPages.Count - 1
            pWriter.WriteLine(Space((pLevel + 1) * 3) & "<TabPage Index=""" & Format(Index) & """ Form=""" & TypeName(SubForm(Index)) & """></TabPage>")
        Next Index
        pWriter.WriteLine(Space(pLevel * 3) & "</ctlTabControl>")
    End Sub

    Public Sub DeSerialize(pElement As XElement)
        ' Stop
        For Each Element As XElement In pElement.Elements()
            Select Case Element.Name
                Case "TabPage"
                    Select Case Element.Attribute("Form").Value
                        Case "frmBoard"
                            Me.AddTabPage(gfrmMainForm.gfrmBoard)
                            'gfrmMainForm.mnuBoard.Checked = True
                        Case "frmStockfish"
                            Me.AddTabPage(gfrmMainForm.gfrmStockfish)
                            'gfrmMainForm.mnuStockfish.Checked = True
                        Case "frmMoveList"
                            Me.AddTabPage(gfrmMainForm.gfrmMoveList)
                            'gfrmMainForm.mnuMoveList.Checked = True
                        Case "frmValidMoves"
                            Me.AddTabPage(gfrmMainForm.gfrmValidMoves)
                            'gfrmMainForm.mnuValidMoves.Checked = True
                        Case "frmTitleAndMemo"
                            Me.AddTabPage(gfrmMainForm.gfrmTitleAndMemo)
                            'gfrmMainForm.mnuTitleAndMemo.Checked = True
                        Case "frmGameDetails"
                            Me.AddTabPage(gfrmMainForm.gfrmGameDetails)
                            'gfrmMainForm.mnuGameDetails.Checked = True
                    End Select
            End Select
        Next Element

    End Sub

    'EventHandlers ================================

    Private Sub TabControl_MouseClick(pSender As Object, pArgs As MouseEventArgs) Handles TabControl.MouseClick
        If (TabControl.TabPages.Count > 0) Then
            If (TabCloseIconRectangle(Me.SelectedIndex).Contains(pArgs.Location)) Then
                BeforeImage = gfrmMainForm.SerializeLayout()
                Dim TabPage As TabPage = Me.TabControl.TabPages(Me.SelectedIndex)
                RemoveTab(TabPage)

                gfrmMainForm.gJournaling.SaveImage("Layout", BeforeImage, gfrmMainForm.SerializeLayout())
            End If
        End If
    End Sub

    Private Sub TabControl_MouseDown(pSender As Object, pArgs As MouseEventArgs) Handles TabControl.MouseDown
        If pArgs.Button = MouseButtons.Left Then
            If (TabTitleRectangle(Me.SelectedIndex).Contains(pArgs.Location)) Then
                MouseDownLocation = pArgs.Location
            Else
                MouseDownLocation = Nothing
            End If

            BeforeImage = gfrmMainForm.SerializeLayout()
        End If
    End Sub

    Private Sub TabControl_MouseMove(pSender As Object, pArgs As MouseEventArgs) Handles TabControl.MouseMove
        'Colour the Close crosses
        If (TabControl.TabPages.Count > 1) Then
            For Index As Integer = 0 To Me.TabControl.TabCount - 1
                DrawCloseIcon(TabControl, Index, TabCloseIconRectangle(Index).Contains(pArgs.Location))
            Next Index
        End If

        If gfrmMainForm.pnlDragPanel.Visible Then
            Dim MouseLocation As New Point(Me.PointToScreen(pArgs.Location))
            RaiseEvent TabPageDragging(Me, MouseLocation)
            Exit Sub
        End If

        If (TabControl.TabPages.Count > 0) Then
            If MouseDownLocation <> Nothing And pArgs.Button = MouseButtons.Left Then
                If Distance(pArgs.Location, MouseDownLocation) > 5 Then
                    MouseDownLocation = Nothing
                    Dim MouseLocation As New Point(Me.PointToScreen(pArgs.Location))
                    RaiseEvent TabPageStartDragging(Me, MouseLocation)
                Else
                    'Not yet moved enough, or just a Mouseclick to activate Tab
                    Exit Sub
                End If
            Else
                MouseDownLocation = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' MousUp event is forwarded to frmMainForm to Raise an ForwardedMousUp event to ALL ctlTabControls
    ''' </summary>
    Private Sub TabControl_MouseUp(pSender As Object, pArgs As MouseEventArgs) Handles TabControl.MouseUp
        If Me.IsDisposed = True Then
            Exit Sub
        End If
        Dim MouseLocation As New Point(Me.PointToScreen(pArgs.Location))
        RaiseEvent MouseUp(Me, MouseLocation)
    End Sub

    Private Sub TabControl_MouseLeave(pSender As Object, pArgs As EventArgs) Handles TabControl.MouseLeave
        'Reset all Close-crosses
        For Index As Integer = 0 To Me.TabControl.TabCount - 1
            DrawCloseIcon(TabControl, Index)
        Next Index
    End Sub

    Private Sub TabControl_DrawItem(pSender As Object, pArgs As DrawItemEventArgs) Handles TabControl.DrawItem
        Try
            DrawBackGround(TabControl, pArgs.Index)
            DrawText(TabControl, pArgs.Index)
            DrawCloseIcon(TabControl, pArgs.Index)
        Catch
        End Try
    End Sub

    Private Sub TabControl_Selected(pSender As Object, pArgs As TabControlEventArgs) Handles TabControl.Selected
        'Needed to enable visibility reset after page was invisible
        If Me.SelectedTab IsNot Nothing Then
            Me.SelectedTab.Controls(0).Refresh()
        End If
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' A MouseUp event is only detected by the control with the previous MouseDown.
    ''' To solve this, The control with the previous MouseDown fires a MouseUp event.
    ''' The frmMainForm catches this event and raises a ForwardedMouseUp to all ctlTabControls.
    ''' Each ctlTabControl checks to see if this event was intended for him.
    ''' </summary>
    Private Sub gParentForm_ForwardedMouseUp(pDragPanel As Panel, pDockStyle As DockStyle, pMouseLocation As Point) Handles gfrmMainForm.ForwardedMouseUp

        If pDragPanel.Visible = False _
        Or pDragPanel.Controls.Count = 0 Then
            'Already Dropped at another ctlTabControl
            Exit Sub
        End If

        'Dropping at one of the available tabs
        For Index As Integer = 0 To Me.TabControl.TabPages.Count - 1
            If TabRectangle(Index).Contains(pMouseLocation) Then
                Dim Form As Form = pDragPanel.Controls(0)
                AddTabPage(pDragPanel, Index)

                gfrmMainForm.gJournaling.SaveImage("Layout", BeforeImage, gfrmMainForm.SerializeLayout())
                RaiseEvent TabPageDropped(Form)
                Exit Sub
            End If
        Next Index

        'Dropping at the end of the Tab-area
        If Me.TabControl.TabPages.Count > 0 Then
            If EndOfTabAreaRectangle().Contains(pMouseLocation) Then
                Dim Form As Form = pDragPanel.Controls(0)
                AddTabPage(pDragPanel)

                gfrmMainForm.gJournaling.SaveImage("Layout", BeforeImage, gfrmMainForm.SerializeLayout())
                RaiseEvent TabPageDropped(Form)
                Exit Sub
            End If
        End If

        'Dropping at the Form-Area
        If FormRectangle().Contains(pMouseLocation) Then
            Dim Form As Form = pDragPanel.Controls(0)
            AddTabPage(pDragPanel)

            gfrmMainForm.gJournaling.SaveImage("Layout", BeforeImage, gfrmMainForm.SerializeLayout())
            RaiseEvent TabPageDropped(Form)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' A MouseUp event is only detected by to control with the previous MouseDown.
    ''' To solve this, The control with the previous MouseDown fires a MouseLeave event
    ''' when the MousePointer leaves the rectangle of the gDockingCrossTabControl.
    ''' Each ctlTabControl checks to see if this event was intended for him.
    ''' </summary>
    Private Sub gParentForm_ForwardedMousePanelLeave(pMouseLocation As Point) Handles gfrmMainForm.ForwardedMousePanelLeave
        Dim TabRectangle = Me.TabControl.RectangleToScreen(TabControl.DisplayRectangle)
        If TabRectangle.Contains(pMouseLocation) Then
            RaiseEvent MouseEnter(Me)
        End If
    End Sub

    Private Sub ctlTabControl_Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gfrmMainForm.RemoveHandlers(Me)
        gfrmMainForm = Nothing
        For Index As Integer = Me.TabControl.TabCount - 1 To 0 Step -1
            Me.TabControl.TabPages(Index).Dispose()
        Next Index
    End Sub

    'Private Subs and Functions ==============================

    Private Sub DrawBackGround(pSender As Object, pTabIndex As Integer)
        Dim TabRectangle As Rectangle = Me.TabControl.GetTabRect(pTabIndex)
        If (pTabIndex = Me.SelectedIndex) Then
            Me.TabControl.CreateGraphics.FillRectangle(New SolidBrush(Color.FromArgb(255, 234, 113)), TabRectangle)
        Else
            Me.TabControl.CreateGraphics.FillRectangle(New SolidBrush(Color.LightGray), TabRectangle) 'FromArgb( 211, 211, 211)
        End If
    End Sub

    Private Sub DrawText(pTabControl As TabControl, pTabIndex As Integer)
        Dim TabRectangle As Rectangle = pTabControl.GetTabRect(pTabIndex)
        TabRectangle.Offset(2, 2)
        Dim TitleBrush As Brush = New SolidBrush(Color.Black)
        Dim Title As String = pTabControl.TabPages(pTabIndex).Text
        pTabControl.CreateGraphics.DrawString(Title, pTabControl.Font, TitleBrush, New PointF(TabRectangle.X, TabRectangle.Y))
    End Sub

    Private Sub DrawCloseIcon(pTabControl As TabControl, pTabIndex As Integer, Optional pMouseOnImage As Boolean = False)
        Dim TabRectangle As Rectangle = pTabControl.GetTabRect(pTabIndex)
        Dim Image As Image
        TabRectangle.Offset(0, 1)
        If pMouseOnImage = True Then
            Image = New Bitmap(frmImages.getImage("CloseBlack"))
        ElseIf pTabIndex = pTabControl.SelectedIndex Then
            Image = New Bitmap(frmImages.getImage("CloseGrayOnGold"))
        Else
            Image = New Bitmap(frmImages.getImage("CloseGrayOnGray"))
        End If
        pTabControl.CreateGraphics.DrawImage(Image, TabRectangle.X + (TabRectangle.Width + gPanelCloseIconLocation.X),
                                             gPanelCloseIconLocation.Y, TabRectangle.Height - (TabRectangle.Y * 2), TabRectangle.Height - (TabRectangle.Y * 2))
    End Sub

    Private Function Distance(pPoint1 As Point, pPoint2 As Point) As Integer
        Return Math.Sqrt((pPoint1.X - pPoint2.X) * (pPoint1.X - pPoint2.X) + (pPoint1.Y - pPoint2.Y) * (pPoint1.Y - pPoint2.Y))
    End Function

    Private Function TabCloseIconRectangle(pIndex As Integer) As Rectangle
        Dim TabRectangle As Rectangle = Me.TabControl.GetTabRect(pIndex)
        Return New Rectangle(TabRectangle.X + TabRectangle.Width + gPanelCloseIconLocation.X, gPanelCloseIconLocation.Y,
                             TabRectangle.Height - (TabRectangle.Y * 2), TabRectangle.Height - (TabRectangle.Y * 2))
    End Function

    Private Function TabTitleRectangle(pIndex As Integer) As Rectangle
        Dim TabRectangle As Rectangle = Me.TabControl.GetTabRect(pIndex)
        Return New Rectangle(TabRectangle.X, TabRectangle.Y,
                             TabRectangle.Width + gPanelCloseIconLocation.X, TabRectangle.Height)
    End Function

    Private Function EndOfTabAreaRectangle() As Rectangle
        Dim TabRectangle As Rectangle
        TabRectangle = Me.TabControl.GetTabRect(Me.TabControl.TabPages.Count - 1)
        TabRectangle.X = TabRectangle.X + TabRectangle.Width
        TabRectangle.Width = Me.TabControl.Width - TabRectangle.X
        Return Me.TabControl.RectangleToScreen(TabRectangle)
    End Function

    Private Function FormRectangle() As Rectangle
        Return Me.TabControl.RectangleToScreen(Me.TabControl.ClientRectangle)
    End Function

    Private Function TabRectangle(pIndex As Integer) As Rectangle
        Return Me.TabControl.RectangleToScreen(Me.TabControl.GetTabRect(pIndex))
    End Function

    Public Function GetTabRect(pIndex As Integer) As Rectangle
        Return Me.TabControl.GetTabRect(pIndex)
    End Function

End Class
