Option Explicit On

Public Class ctlSplitContainer

    Public gParentForm As frmMainForm

    Public Property Panel1 As Control
        Set(pControl As Control)
            SplitContainer.Panel1.Controls.Clear()
            SplitContainer.Panel1.Controls.Add(pControl)
        End Set
        Get
            If SplitContainer.Panel1.Controls.Count > 0 Then
                Return SplitContainer.Panel1.Controls(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property Panel2 As Control
        Set(pControl As Control)
            SplitContainer.Panel2.Controls.Clear()
            SplitContainer.Panel2.Controls.Add(pControl)
        End Set
        Get
            If SplitContainer.Panel2.Controls.Count > 0 Then
                Return SplitContainer.Panel2.Controls(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub New(pOrientation As Orientation)
        InitializeComponent()

        SplitContainer.Orientation = pOrientation
    End Sub

    Private Sub ctlSplitContainer_Load(pSender As Object, pArgs As EventArgs) Handles Me.Load
        gParentForm = frmMainForm
        'If Me.DesignMode = False Then
        '    'At Designmode the frmMainform stil is a generic Form Type with no features
        '    gParentForm = Me.ParentForm
        'End If
        Me.Dock = DockStyle.Fill
    End Sub

    Public Sub Serialize(pWriter As System.IO.TextWriter, pLevel As Integer)
        pWriter.WriteLine(Space(pLevel * 3) & "<ctlSplitContainer Level=""" & Format(pLevel) &
                                                              """ Orientation=""" & Format(SplitContainer.Orientation) &
                                                              """ SplitterDistance=""" & Format(SplitContainer.SplitterDistance) & """>")

        pWriter.WriteLine(Space((pLevel + 1) * 3) & "<Panel1 Width=""" & Format(SplitContainer.Panel1.Width) & """>")
        Select Case TypeName(Panel1)
            Case "ctlTabControl"
                CType(Panel1, ctlTabControl).Serialize(pWriter, pLevel + 2)
            Case "ctlSplitContainer"
                CType(Panel1, ctlSplitContainer).Serialize(pWriter, pLevel + 2)
        End Select
        pWriter.WriteLine(Space((pLevel + 1) * 3) & "</Panel1>")

        pWriter.WriteLine(Space((pLevel + 1) * 3) & "<Panel2 Width=""" & Format(SplitContainer.Panel2.Width) & """>")
        Select Case TypeName(Panel2)
            Case "ctlTabControl"
                CType(Panel2, ctlTabControl).Serialize(pWriter, pLevel + 2)
            Case "ctlSplitContainer"
                CType(Panel2, ctlSplitContainer).Serialize(pWriter, pLevel + 2)
        End Select
        pWriter.WriteLine(Space((pLevel + 1) * 3) & "</Panel2>")

        pWriter.WriteLine(Space(pLevel * 3) & "</ctlSplitContainer>")
    End Sub

    Public Sub DeSerialize(pElement As XElement)
        Me.SplitContainer.SplitterDistance = Val(pElement.Attribute("SplitterDistance").Value)
        For Each Element As XElement In pElement.Elements()
            Select Case Element.Name
                Case "Panel1"
                    For Each SubElement In Element.Elements()
                        Select Case SubElement.Name
                            Case "ctlTabControl"
                                Dim ctlTabControl = New ctlTabControl()
                                gParentForm.AddHandlers(ctlTabControl)
                                Me.Panel1 = ctlTabControl
                                ctlTabControl.DeSerialize(SubElement)
                            Case "ctlSplitContainer"
                                Dim SplitContainer As New ctlSplitContainer(Val(SubElement.Attribute("Orientation").Value))
                                Me.Panel1 = SplitContainer
                                SplitContainer.DeSerialize(SubElement)
                        End Select
                    Next SubElement
                Case "Panel2"
                    For Each SubElement In Element.Elements()
                        Select Case SubElement.Name
                            Case "ctlTabControl"
                                Dim ctlTabControl = New ctlTabControl()
                                gParentForm.AddHandlers(ctlTabControl)
                                Me.Panel2 = ctlTabControl
                                ctlTabControl.DeSerialize(SubElement)
                            Case "ctlSplitContainer"
                                Dim SplitContainer As New ctlSplitContainer(Val(SubElement.Attribute("Orientation").Value))
                                Me.Panel2 = SplitContainer
                                SplitContainer.DeSerialize(SubElement)
                        End Select
                    Next SubElement
            End Select
        Next Element
    End Sub

    Private Sub ctlSplitContainer_Disposed(pSender As Object, pArgs As EventArgs) Handles Me.Disposed
        gParentForm = Nothing
        If SplitContainer.Panel1.Controls.Count > 0 Then
            SplitContainer.Panel1.Controls(0).Dispose()
        End If
        If SplitContainer.Panel2.Controls.Count > 0 Then
            SplitContainer.Panel2.Controls(0).Dispose()
        End If
        SplitContainer.Dispose()
    End Sub
End Class
