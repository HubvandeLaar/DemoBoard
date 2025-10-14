Public Class PGNVariant

    Public Property ParentFirstMoveIndex As Long
    Public Property FromIndex As Long
    Public Property ToIndex As Long
    Public Property VariantLevelIncrement As Long

    Public Sub New(pMainFirstMoveIndex As Long, pFromIndex As Long, pToIndex As Long, Optional pVariantLevelIncrement As Long = 0)
        ParentFirstMoveIndex = pMainFirstMoveIndex
        FromIndex = pFromIndex
        ToIndex = pToIndex
        VariantLevelIncrement = pVariantLevelIncrement
    End Sub

    ''' <summary>For debugging purposes</summary>
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}-{2}, {3}", ParentFirstMoveIndex, FromIndex, ToIndex, VariantLevelIncrement)
    End Function

End Class