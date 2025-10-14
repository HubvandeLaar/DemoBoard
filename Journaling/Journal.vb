Option Explicit On

Imports System.Xml.Serialization
Imports ChessMessaging.Messages

<XmlType()>
Public Class Journal
    Inherits List(Of JournalEntry)      '0-Based List

    Private gJournalPointer As Integer = -1        'Points to Last inserted JournalEntry

    Public Event PointerUpdated(pCount As Integer, pPointer As Integer, pUndoToolTip As String, pRedoToolTip As String)
    Public Event UpdateRequested(pClassName As String, pKeyValue As String, pOldValue As String, pNewValue As String)
    Public Event ErrorOccured(ByVal pException As Exception)

    <XmlElement>
    Public Property Pointer As Integer 'Points to Last inserted JournalEntry
        Set(pJournalPointer As Integer)
            Dim UndoToolTip As String = MessageText("UndoEmpty")
            Dim RedoToolTip As String = MessageText("RedoEmpty")
            gJournalPointer = pJournalPointer

            If gJournalPointer > -1 _
            And (gJournalPointer) < Me.Count Then
                UndoToolTip = MessageText("UndoTooltipText", Me(gJournalPointer).ClassName, Me(gJournalPointer).KeyValue)
            End If
            If (gJournalPointer + 1) > -1 _
            And (gJournalPointer + 1) < Me.Count Then
                RedoToolTip = MessageText("RedoTooltipText", Me(gJournalPointer + 1).ClassName, Me(gJournalPointer + 1).KeyValue)
            End If

            RaiseEvent PointerUpdated(Me.Count, gJournalPointer, UndoToolTip, RedoToolTip)
        End Set
        Get
            Return gJournalPointer
        End Get
    End Property

    Public Sub SaveImage(pClassName As String, pBeforeImage As String, pAfterImage As String)
        SaveImage(pClassName, "", pBeforeImage, pAfterImage)
    End Sub

    Public Sub SaveImage(pClassName As String, pKeyValue As String, pBeforeImage As String, pAfterImage As String)
        If pBeforeImage <> pAfterImage Then
            Dim JournalEntry As New JournalEntry(pClassName, pKeyValue, pBeforeImage, pAfterImage)
            While (Pointer > -1 And Pointer < (Me.Count - 1))
                Me.Remove(Me.Last)
            End While
            Me.Add(JournalEntry)
            Pointer += 1
        End If
    End Sub

    ''' <summary>Returns a serialize string of an Object</summary>
    <STAThread()>
    Public Function Serialize(pObject As Object, Optional pOmitHeader As Boolean = False) As String
        Try
            Dim Serializer As New XmlSerializer(pObject.GetType())
            Dim EmptyNamespace As New XmlSerializerNamespaces()
            EmptyNamespace.Add("", "")
            Using Writer As New IO.StringWriter()
                If pOmitHeader = True Then
                    Serializer.Serialize(Writer, pObject, EmptyNamespace)
                Else
                    Serializer.Serialize(Writer, pObject)
                End If
                Return Writer.ToString()
            End Using

        Catch pException As Exception
            RaiseEvent ErrorOccured(pException)
            Return ""
        End Try
    End Function

    ''' <summary>Returns a new Object from a serialized string</summary>
    <STAThread()>
    Public Function DeSerialize(pString As String, pType As Type) As Object
        Try
            Dim Serializer As New XmlSerializer(pType)
            Using Reader As New IO.StringReader(pString)
                Return Serializer.Deserialize(Reader)
            End Using

        Catch pException As Exception
            RaiseEvent ErrorOccured(pException)
            Return ""
        End Try
    End Function

    Public Overloads Sub Clear()
        MyBase.Clear()
        Pointer = -1
    End Sub

    Public Sub Undo()
        If Pointer > -1 _
        And Pointer < Me.Count Then
            With Me(Pointer)
                RaiseEvent UpdateRequested(.ClassName, .KeyValue, .AfterImage, .BeforeImage)
                Pointer -= 1
            End With
        End If
    End Sub

    Public Sub Redo()
        If Pointer < Me.Count Then
            With Me(Pointer + 1)
                RaiseEvent UpdateRequested(.ClassName, .KeyValue, .BeforeImage, .AfterImage)
                Pointer += 1
            End With
        End If
    End Sub

    ''' <summary>Returns True if a Journal-Entry with a part of a PGNFile was found</summary>
    Public Function PGNFileModified() As Boolean
        'Test to see if PGNFile was modified using the Journal-entries
        For Each Entry As Journaling.JournalEntry In Me
            If Entry.ClassName Like "*.Index" _
            OrElse Entry.ClassName Like "*.Visible" _
            OrElse Entry.ClassName Like "*.Checked" Then
                Continue For
            End If
            Select Case Entry.ClassName
                Case "Mode", "CurrentLanguage", "TabPage.Dropped", "Layout"
                    Continue For
                Case Else 'Entry referring to PGNFile being modified
                    Return True
            End Select
        Next Entry

        'No entry referring to PGNFile; Not being modified
        Return False
    End Function

End Class

