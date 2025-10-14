Option Explicit On

Imports System.Globalization
Imports System.Threading
Imports System.Windows.Forms
Imports System.ComponentModel
Imports ChessGlobals.ChessLanguage

Public Module modChessLanguage

    Public Enum ChessLanguage
        UNDEFINED = 0
        NEDERLANDS = 1
        ENGLISH = 2
        BOTH = 99
    End Enum

    Public Const REGISTRYNAME As String = "Software\DemoBoard\"

    Private gCurrentLanguage As ChessLanguage = UNDEFINED

    Public Property CurrentLanguage As ChessLanguage
        Set(pCurrentLanguage As ChessLanguage)
            gCurrentLanguage = pCurrentLanguage
            Call SaveLanguage(pCurrentLanguage)
        End Set
        Get
            Return gCurrentLanguage
        End Get
    End Property

    ''' <summary>Returns the Laguage stored at the Registry or CurrentCulture</summary>
    Public Function LoadLanguage() As ChessLanguage
        Select Case My.Computer.Registry.CurrentUser.GetValue(REGISTRYNAME, "Language", UNDEFINED)
            Case "Nederlands" : Return NEDERLANDS
            Case "English" : Return ENGLISH
            Case Else
                Select Case System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                    Case "nl" : Return NEDERLANDS
                    Case Else : Return ENGLISH
                End Select
        End Select
    End Function

    Public Sub ApplyLanguageToCurrentForm(pCurrentForm As Form)
        Dim Size As Drawing.Size, Location As Drawing.Point, Visible As Boolean
        Dim Resources As New ComponentResourceManager(pCurrentForm.GetType())
        pCurrentForm.Text = Resources.GetString("$this.Text", Thread.CurrentThread.CurrentUICulture)
        Resources.ApplyResources(pCurrentForm, pCurrentForm.Name, Thread.CurrentThread.CurrentUICulture)
        For Each Control As Control In pCurrentForm.Controls
            Select Case TypeName(Control)
                Case "ToolStrip", "MenuStrip"
                    Resources.ApplyResources(Control, Control.Name, Thread.CurrentThread.CurrentUICulture)
                    Call ApplyLanguageToToolStrip(Control, Resources)
                Case "ctlSplitContainer", "ctlTabControl", "ctlTreeView", "ctlTreeViewRow", "ctlBoard",
                     "Panel"
                    Continue For
                Case Else
                    Size = Control.Size 'Save Size and location to ensure these are retained as is
                    Location = Control.Location
                    Visible = Control.Visible
                    Resources.ApplyResources(Control, Control.Name, Thread.CurrentThread.CurrentUICulture)
                    Control.Size = Size
                    Control.Location = Location
                    Control.Visible = Visible
            End Select
        Next Control
    End Sub

    Private Sub ApplyLanguageToToolStrip(pToolStrip As ToolStrip, pResources As ComponentResourceManager)
        For Each ToolStripItem As ToolStripItem In pToolStrip.Items
            pResources.ApplyResources(ToolStripItem, ToolStripItem.Name, Thread.CurrentThread.CurrentUICulture)
            If TypeOf ToolStripItem Is ToolStripDropDownItem Then
                ApplyLanguageToDropDown(ToolStripItem, pResources)
            End If
        Next ToolStripItem
    End Sub

    Private Sub ApplyLanguageToDropDown(pDropDown As ToolStripDropDownItem, pResources As ComponentResourceManager)
        For Each DropDownItem As ToolStripItem In pDropDown.DropDownItems
            pResources.ApplyResources(DropDownItem, DropDownItem.Name, Thread.CurrentThread.CurrentUICulture)
            If TypeOf DropDownItem Is ToolStripDropDownItem Then
                ApplyLanguageToDropDown(DropDownItem, pResources)
            End If
        Next DropDownItem
    End Sub

    Private Sub SaveLanguage(pLanguage As ChessLanguage)
        My.Computer.Registry.CurrentUser.CreateSubKey(REGISTRYNAME)
        Select Case pLanguage
            Case NEDERLANDS : My.Computer.Registry.CurrentUser.SetValue(REGISTRYNAME & "Language", "Nederlands")
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("nl")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("nl")
            Case ENGLISH : My.Computer.Registry.CurrentUser.SetValue(REGISTRYNAME & "Language", "English")
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            Case Else : Throw New NotSupportedException("UnkownLanguage")
        End Select
    End Sub

End Module

