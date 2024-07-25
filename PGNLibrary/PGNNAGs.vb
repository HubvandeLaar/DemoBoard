Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessLanguage
Imports PGNLibrary.PGNNAG
Imports PGNLibrary.PGNNAG.NAGType
Imports PGNLibrary.PGNNAG.NAGPrintPosition
Imports System.Xml.Serialization

<XmlType()>
Public Class PGNNAGs
    Inherits List(Of PGNNAG)

    <XmlIgnore>
    Public Property PGNString() As String
        Set(pPGNString As String)  'Parse NAGText into seperate NAGs
            Dim NAG As PGNNAG, P As Long
            Me.Clear()  'The NAG as created from the MoveText can contain multiple $123 sequences
            For P = 1 To Len(pPGNString)
                If Mid$(pPGNString, P, 1) = "$" Then 'Assumption subsequent NAGs start with $ and Val function only takes the first NAG
                    NAG = New PGNNAG("$" & Microsoft.VisualBasic.Strings.Format(Val(Mid$(pPGNString, P + 1)), "0"))
                    Me.Add(NAG)
                End If
            Next P
        End Set
        Get
            Dim NAGString As String = ""
            For Each NAG As PGNNAG In Me
                If NAGString <> "" Then NAGString = NAGString & " "
                NAGString = NAGString & NAG.PGNString
            Next NAG
            Return NAGString
        End Get
    End Property

    <XmlIgnore>
    Public Overloads ReadOnly Property Count(Optional pRequestedNAGPlacement As NAGPrintPosition = BEFOREANDAFTER) As Long
        Get
            Dim I As Long, Counter As Long
            Counter = 0
            Select Case pRequestedNAGPlacement
                Case BEFOREANDAFTER
                    Return MyBase.Count
                Case BEFORE
                    For I = 0 To MyBase.Count - 1
                        If Me(I).PrintPosition = BEFORE Then Counter = Counter + 1
                    Next I
                    Return Counter
                Case AFTER
                    For I = 0 To MyBase.Count - 1
                        If Me(I).PrintPosition = AFTER Then Counter = Counter + 1
                    Next I
                    Return Counter
                Case Else
                    Throw New DataException(MessageText("NAGBeforeOrAfter", pRequestedNAGPlacement))
                    Return 0
            End Select
        End Get
    End Property

    Public Sub New(pPGNString)
        Me.PGNString = pPGNString
    End Sub

    Public Sub New()
    End Sub

End Class
