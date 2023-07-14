Option Explicit On

Imports System.Xml.Serialization

<XmlType>
Public Class Journaling
    <XmlArray>
    Public Journal As New List(Of JournalEntry)  '0-Based List

    Private mJournalPointer As Integer = -1        'Points to Last inserted JournalEntry

    Public Event PointerUpdated(pCount As Integer, pPointer As Integer, pUndoToolTip As String, pRedoToolTip As String)
    Public Event UpdateRequested(pClassName As String, pKeyValue As String, pOldValue As String, pNewValue As String)

    Public Sub SaveImage(pClassName As String, pBeforeImage As String, pAfterImage As String)
        SaveImage(pClassName, "", pBeforeImage, pAfterImage)
    End Sub
    Public Sub SaveImage(pClassName As String, pKeyValue As String, pBeforeImage As String, pAfterImage As String)
        If pBeforeImage <> pAfterImage Then
            Dim JournalEntry As New JournalEntry(pClassName, pKeyValue, pBeforeImage, pAfterImage)
            While (Pointer < (Journal.Count - 1))
                Journal.Remove(Journal.Last)
            End While
            Journal.Add(JournalEntry)
            Pointer += 1
        End If
    End Sub

    <XmlElement>
    Public Property Pointer As Integer 'Points to Last inserted JournalEntry
        Set(pJournalPointer As Integer)
            Dim UndoToolTip As String = MessageText("UndoEmpty")
            Dim RedoToolTip As String = MessageText("RedoEmpty")

            mJournalPointer = pJournalPointer

            If mJournalPointer > -1 _
            And (mJournalPointer) < Journal.Count Then
                UndoToolTip = MessageText("UndoTooltipText", Journal(mJournalPointer).ClassName, Journal(mJournalPointer).KeyValue)
            End If
            If (mJournalPointer + 1) > -1 _
            And (mJournalPointer + 1) < Journal.Count Then
                RedoToolTip = MessageText("RedoTooltipText", Journal(mJournalPointer + 1).ClassName, Journal(mJournalPointer + 1).KeyValue)
            End If

            RaiseEvent PointerUpdated(Journal.Count, mJournalPointer, UndoToolTip, RedoToolTip)
        End Set
        Get
            Return mJournalPointer
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return Journal.Count
        End Get
    End Property

    <STAThread()>
    Public Shared Function Serialize(pObject As Object, Optional pOmitHeader As Boolean = False) As String
        Try
            Dim Serializer As New XmlSerializer(pObject.GetType())
            Dim EmptyNamespace As New XmlSerializerNamespaces()
            EmptyNamespace.Add("", "")
            Dim Writer As New IO.StringWriter()
            If pOmitHeader = True Then
                Serializer.Serialize(Writer, pObject, EmptyNamespace)
            Else
                Serializer.Serialize(Writer, pObject)
            End If
            Return Writer.ToString()
        Catch pException As Exception
            frmErrorMessageBox.Show(pException)
            Return ""
        End Try
    End Function

    <STAThread()>
    Public Shared Function DeSerialize(pString As String, pType As Type) As Object
        Dim Serializer As New XmlSerializer(pType)
        Dim Reader As New IO.StringReader(pString)
        Return Serializer.Deserialize(Reader)
    End Function

    Public Sub Clear()
        Journal.Clear()
        Pointer = -1
    End Sub

    Public Sub Undo()
        If Pointer > -1 _
        And Pointer < Journal.Count Then
            With Journal(Pointer)
                RaiseEvent UpdateRequested(.ClassName, .KeyValue, .AfterImage, .BeforeImage)
                Pointer -= 1
            End With
        End If
    End Sub

    Public Sub Redo()
        If Pointer < Journal.Count Then
            With Journal(Pointer + 1)
                RaiseEvent UpdateRequested(.ClassName, .KeyValue, .BeforeImage, .AfterImage)
                Pointer += 1
            End With
        End If
    End Sub

End Class

