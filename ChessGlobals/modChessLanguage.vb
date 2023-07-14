Option Explicit On

Imports ChessGlobals
Imports ChessGlobals.ChessLanguage
Imports System.Globalization
Imports System.Threading
Imports System.Windows.Forms
Imports System.ComponentModel

Public Module modChessLanguage

    Public Enum ChessLanguage
        NOTDEFINED = 0
        NEDERLANDS = 1
        ENGLISH = 2
        BOTH = 99
    End Enum

    Public CurrentLanguage As ChessLanguage

    Public Function GetLanguage() As ChessLanguage
        Select Case My.Computer.Registry.CurrentUser.GetValue(REGISTRYNAME, "Language", NOTDEFINED)
            Case "Nederlands" : Return NEDERLANDS
            Case "English" : Return ENGLISH
            Case Else
                Select Case System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                    Case "nl" : Return NEDERLANDS
                    Case Else : Return ENGLISH
                End Select
        End Select
    End Function

    Public Sub SetLanguage(pLanguage As ChessLanguage, pCurrentForm As Form)
        My.Computer.Registry.CurrentUser.CreateSubKey(REGISTRYNAME)
        Select Case pLanguage
            Case NEDERLANDS : My.Computer.Registry.CurrentUser.SetValue(REGISTRYNAME & "Language", "Nederlands")
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("nl")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("nl")
            Case ENGLISH : My.Computer.Registry.CurrentUser.SetValue(REGISTRYNAME & "Language", "English")
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            Case Else : Throw New NotSupportedException(MessageText("UnkownLanguage"))
        End Select
        Call ChangeLanguageCurrentForm(pCurrentForm)
        CurrentLanguage = pLanguage
    End Sub

    Public Sub ChangeLanguageCurrentForm(pCurrentForm As Form)
        Dim Size As Drawing.Size, Location As Drawing.Point
        Dim Resources As ComponentResourceManager
        Resources = New ComponentResourceManager(pCurrentForm.GetType())
        pCurrentForm.Text = Resources.GetString("$this.Text", Thread.CurrentThread.CurrentUICulture)
        Resources.ApplyResources(pCurrentForm, pCurrentForm.Name, Thread.CurrentThread.CurrentUICulture)
        For Each Control As Control In pCurrentForm.Controls
            Select Case TypeName(Control)
                Case "ToolStrip", "MenuStrip"
                    Resources.ApplyResources(Control, Control.Name, Thread.CurrentThread.CurrentUICulture)
                    Call ChangeLanguageToolStrip(Control, Resources)
                Case "ctlSplitContainer", "ctlTabControl", "ctlTreeView", "ctlTreeViewRow", "ctlBoard",
                     "Panel"
                    Continue For
                Case Else
                    Size = Control.Size 'Save Size and location to ensure these are retained as is
                    Location = Control.Location
                    Resources.ApplyResources(Control, Control.Name, Thread.CurrentThread.CurrentUICulture)
                    Control.Size = Size
                    Control.Location = Location
            End Select
            'Resources.ApplyResources(Control, Control.Name, Thread.CurrentThread.CurrentUICulture)
            'If TypeOf Control Is ToolStrip Then
            '    Call ChangeLanguageToolStrip(Control, Resources)
            'End If
        Next Control
    End Sub

    Private Sub ChangeLanguageToolStrip(pToolStrip As ToolStrip, pResources As ComponentResourceManager)
        For Each ToolStripItem As ToolStripItem In pToolStrip.Items
            pResources.ApplyResources(ToolStripItem, ToolStripItem.Name, Thread.CurrentThread.CurrentUICulture)
            If TypeOf ToolStripItem Is ToolStripDropDownItem Then
                ChangeLanguageDropDown(ToolStripItem, pResources)
            End If
        Next ToolStripItem
    End Sub

    Private Sub ChangeLanguageDropDown(pDropDown As ToolStripDropDownItem, pResources As ComponentResourceManager)
        For Each DropDownItem As ToolStripItem In pDropDown.DropDownItems
            pResources.ApplyResources(DropDownItem, DropDownItem.Name, Thread.CurrentThread.CurrentUICulture)
            If TypeOf DropDownItem Is ToolStripDropDownItem Then
                ChangeLanguageDropDown(DropDownItem, pResources)
            End If
        Next DropDownItem
    End Sub


End Module

