Public Class PGNVariant

    Public ParentFirstMoveIndex As Long
    Public FromIndex As Long
    Public ToIndex As Long
    Public VariantLevelIncrement As Long

    Public Sub New(pMainFirstMoveIndex As Long, pFromIndex As Long, pToIndex As Long, Optional pVariantLevelIncrement As Long = 0)
        ParentFirstMoveIndex = pMainFirstMoveIndex
        FromIndex = pFromIndex
        ToIndex = pToIndex
        VariantLevelIncrement = pVariantLevelIncrement
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}-{2}, {3}", ParentFirstMoveIndex, FromIndex, ToIndex, VariantLevelIncrement)
    End Function

End Class