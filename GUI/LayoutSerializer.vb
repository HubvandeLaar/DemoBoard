Option Explicit On

Public Class LayoutSerializer
    'The Layout of the MainForm contains a ctlTabControl or ctlSplitContainer
    'The ctlTabControl can contain multiple TabPages with each a subform
    'The ctlSplitContainer contains 2 Panels each containing either a ctlTabControl or another ctlSplitContainer
    'NB See the XML files in the Settings Folder for examples of saved layouts and structure

    Private ReadOnly gfrmMainForm As frmMainForm

    ''' <summary>Initializes a new instance of the LayoutSerializer class.</summary>    
    Public Sub New(pfrmMainForm As frmMainForm)
        gfrmMainForm = pfrmMainForm
    End Sub

    ''' <summary>Returns the Serialized Layout of panels and forms</summary>
    Public Function SerializeLayout() As String
        Using Writer As New IO.StringWriter()
            SerializeLayout(Writer)
            Return Writer.ToString()
        End Using
    End Function

    Public Sub SerializeLayout(pFileName As String)
        Using Writer As New System.IO.StreamWriter(pFileName) With {.AutoFlush = True}
            SerializeLayout(Writer)
        End Using
    End Sub

    Private Sub SerializeLayout(pWriter As IO.TextWriter)
        pWriter.WriteLine("<MainForm Width=""" & Strings.Format(gfrmMainForm.Width) & """ Height=""" & Strings.Format(gfrmMainForm.Height) &
                                  """ WindowState=""" & Strings.Format(gfrmMainForm.WindowState) &
                                  """ StatusBar=""" & Strings.Format(gfrmMainForm.mnuStatusBar.Checked) &
                                  """ MenuLocation=""" & Strings.Format(gfrmMainForm.MenuLocation) &
                                  """>")
        Dim MainPanelControl = gfrmMainForm.GetMainPanelControl()
        Select Case TypeName(MainPanelControl)
            Case "ctlTabControl"
                CType(MainPanelControl, ctlTabControl).Serialize(pWriter, 1)
            Case "ctlSplitContainer"
                CType(MainPanelControl, ctlSplitContainer).Serialize(pWriter, 1)
        End Select
        pWriter.WriteLine("</MainForm>")
        pWriter.Close()
    End Sub

    Public Sub DeSerializeLayoutFromString(pXMLText As String)
        If pXMLText = "" Then Exit Sub
        DeSerializeLayout(XDocument.Parse(pXMLText))
    End Sub

    Public Sub DeSerializeLayout(pFileName As String)
        Using Reader As New IO.StreamReader(pFileName)
            Dim XMLText As String = Reader.ReadToEnd()

            gfrmMainForm.mnuBoard.Checked = False
            gfrmMainForm.mnuStockfish.Checked = False
            gfrmMainForm.mnuMoveList.Checked = False
            gfrmMainForm.mnuValidMoves.Checked = False
            gfrmMainForm.mnuTitleAndMemo.Checked = False
            gfrmMainForm.mnuGameDetails.Checked = False

            DeSerializeLayout(XDocument.Parse(XMLText))

            Reader.Close()
        End Using
    End Sub

    Public Sub DeSerializeLayout(pXMLDocument As XDocument)
        If pXMLDocument.Root.Name = "MainForm" Then
            For Each Attrib As XAttribute In pXMLDocument.Root.Attributes()
                Select Case Attrib.Name
                    Case "Width" : gfrmMainForm.Width = Val(Attrib.Value)
                    Case "Height" : gfrmMainForm.Height = Val(Attrib.Value)
                    Case "WindowState" : gfrmMainForm.WindowState = Attrib.Value
                    Case "StatusBar" : gfrmMainForm.mnuStatusBar.Checked = Val(Attrib.Value)
                        gfrmMainForm.stsStatusStrip.Visible = gfrmMainForm.mnuStatusBar.Checked
                    Case "MenuLocation" : gfrmMainForm.MenuLocation = Val(Attrib.Value)
                End Select
            Next Attrib
            gfrmMainForm.frmMainForm_SizeChanged(Nothing, Nothing) 'To Update visibility of the Tool and Status-Bar
        End If

        Call gfrmMainForm.DisconnectSubForms() 'To prevent them from being disposed too
        gfrmMainForm.pnlMainPanel.Controls(0).Dispose()
        gfrmMainForm.pnlMainPanel.Controls.Clear()

        For Each Element As XElement In pXMLDocument.Root.Elements()
            Select Case Element.Name
                Case "ctlSplitContainer"
                    Dim SplitContainer As New ctlSplitContainer(Val(Element.Attribute("Orientation").Value))
                    gfrmMainForm.pnlMainPanel.Controls.Add(SplitContainer)
                    SplitContainer.DeSerialize(Element)
                Case "ctlTabControl"
                    Dim ctlTabControl As New ctlTabControl()
                    gfrmMainForm.AddHandlers(ctlTabControl)
                    gfrmMainForm.pnlMainPanel.Controls.Add(ctlTabControl)
                    ctlTabControl.DeSerialize(Element)
            End Select
        Next Element
    End Sub

End Class
