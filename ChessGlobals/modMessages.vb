Imports ChessGlobals.modChessLanguage.ChessLanguage

Public Module modMessages

    ''' <summary>Array with Muitlingual Messages using %1, %2 and %3 to insert values</summary>
    Private ReadOnly MessageTexts(,) As String = {
        {"Key             ", "Lang", "Message with %1, %2 en %3"},
        {"DeleteAllMoves  ", "nl", "Alle zetten verwijderen en zet ""%1"" toevoegen ?"},
        {"DeleteAllMoves  ", "en", "Delete all moves and add move ""%1"" ?"},
        {"InsertMove      ", "nl", "Zet ""%1"" toevoegen en volgende zetten verwijderen ?"},
        {"InsertMove      ", "en", "Insert move ""%1"", and delete all subsequent moves ?"},
        {"InsertSubVariant", "nl", "Zet ""%1"" toevoegen als nieuwe subvariant ?"},
        {"InsertSubVariant", "en", "Insert move ""%1"", as a new subvariant ?"},
        {"AddNewSubVariant", "nl", "Zet ""%1"" toevoegen als subvariant nummer %2 ?"},
        {"AddNewSubVariant", "nl", "Insert move: ""%1"", as %2th subvariant ? "},
        {"UnkownLanguage  ", "nl", "Onbekende taal opgegeven"},
        {"UnkownLanguage  ", "en", "Unkown Language specified to store"},
        {"NoSpaceFound    ", "nl", "Geen spatie gevonden in ""%1"""},
        {"NoSpaceFound    ", "en", "No Space found at ""%1"""},
        {"CommentNotEmpty ", "nl", "Commentaar-achteraf is niet leeg ""%1"""},
        {"CommentNotEmpty ", "en", "CommentAfter is not empty""%1"""},
        {"ReallyWrong     ", "nl", "Het spijt me, maar er gaat iets goed fout..."},
        {"ReallyWrong     ", "en", "Sorry, but something went really wrong..."},
        {"DeleteMove      ", "nl", "Zet ""%1"" en opvolgende zetten verwijderen ?"},
        {"DeleteMove      ", "en", "Delete move ""%1"" and subsequent moves ?"},
        {"MessageNotFound ", "nl", "Bericht-tekst voor sleutel ""%1"" niet gevonden"},
        {"MessageNotFound ", "en", "Message text with key ""%1""  not found"},
        {"ColumnRowRange  ", "nl", "Kolom ""%1"", of Rij ""%2"" is ongeldig (moet 1 t/m 8 zijn)"},
        {"ColumnRowRange  ", "en", "Column ""%1"", or Row ""%2"" out of range (Has to be be 1 to 8)"},
        {"InvalidFENPiece ", "nl", "Ongeldig stuk ""%1"" in FEN"},
        {"InvalidFENPiece ", "en", "Invalid Piece ""%1"" in FEN"},
        {"InvalidPiece    ", "nl", "Ongeldig stuk ""%1"""},
        {"InvalidPiece    ", "en", "Invalid Piece ""%1"""},
        {"NAGBeforeOrAfter", "nl", "Voor of Na niet kunnen vaststellen: ""%1"""},
        {"NAGBeforeOrAfter", "en", "Before Or After not recoginized: ""%1"""},
        {"UnknownNAG      ", "nl", "Onbekende NAG: ""%1"""},
        {"UnknownNAG      ", "en", "Unknown NAG: ""%1"""},
        {"PGNMoveInvalid  ", "nl", "PGN was niet specifiek genoeg voor stuk ""%1"" met doel ""%2"""},
        {"PGNMoveInvalid  ", "en", "PGN was not specific enough for Piece ""%1"" moving to ""%2"""},
        {"StrangePiece    ", "nl", "Onvoorziene situatie met het stuk : ""%1"""},
        {"StrangePiece    ", "en", "Unforseen condition with a piece named: ""%1"""},
        {"UpdateFEN       ", "nl", "Alle zetten verwijderen en de beginstelling aanpassen ?"},
        {"UpdateFEN       ", "en", "Delete all moves and update the initial position ?"},
        {"MissingSpace    ", "nl", "Geen spatie gevonden om ""%1"" op te splitsen in delen !"},
        {"MissingSpace    ", "en", "Missing space to divide ""%1"" into parts !"},
        {"DeleteMarker    ", "nl", "Verwijder markering op %1"},
        {"DeleteMarker    ", "en", "Delete marker at %1"},
        {"DeleteArrow     ", "nl", "Verwijder pijl van %1 naar %2"},
        {"DeleteArrow     ", "en", "Delete arrow from %1 to %2"},
        {"DeleteText      ", "nl", "Verwijder tekst ""%1"" van %2"},
        {"DeleteText      ", "en", "Delete text ""%1"" from %2"},
        {"AddText         ", "nl", "Tekst toevoegen"},
        {"AddText         ", "en", "Add text"},
        {"AddPiece        ", "nl", "%1 toevoegen"},
        {"AddPiece        ", "en", "Add %1"},
        {"OpenPGNFile     ", "nl", "Open PGN Bestand"},
        {"OpenPGNFile     ", "en", "Open PGN File"},
        {"DeletePiece     ", "nl", "%1 verwijderen"},
        {"DeletePiece     ", "en", "Delete %1"},
        {"SameColor       ", "nl", "Twee keer achter elkaar dezelfde kleure gezet; Toch opslaan ?"},
        {"SameColor       ", "en", "Twice or more moves of the same color; Save anyhow ?"},
        {"IntendedCastling", "nl", "De koning werd 2 plaatsen verplaatst; Aangenomen dat een rokade bedoeld werd"},
        {"IntendedCastling", "en", "King was moves 2 places; Assumed castling is intended"},
        {"InvalidImageName", "nl", "De opgegeven naam ""%1"" is onbekend als standaard Image"},
        {"InvalidImageName", "en", "The specified name ""%1"" is not defined as a standard Image"},
        {"InvalidIconName ", "nl", "De opgegeven naam ""%1"" is onbekend als standaard Icon"},
        {"InvalidIconName ", "en", "The specified name ""%1"" is not defined as a standard Icon"},
        {"InvalidMarkerList", "nl", "Ongeldige indeling voor een markeringlijst in PGN"},
        {"InvalidMarerkList", "en", "Invalid marker list format at PGN"},
        {"InvalidArrowList", "nl", "Ongeldige indeling voor een pijlenlijst in PGN"},
        {"InvalidArrowList", "en", "Invalid arrow list format at PGN"},
        {"InvalidTextList ", "nl", "Ongeldige indeling voor een tekstlijst in PGN"},
        {"InvalidTextList ", "en", "Invalid text list format at PGN"},
        {"TrainingQuestion", "nl", "Wat is de beste zet ?"},
        {"TrainingQuestion", "en", "What is the best move ?"},
        {"InvalidQuestion ", "nl", "Ongeldige indeling voor een Trainingsvraag in PGN"},
        {"InvalidQuestion ", "en", "Invalid training question format at PGN"},
        {"CorrectAnswer   ", "nl", "Goed zo !" & vbCrLf & "Het antwoord %1 is juist."},
        {"CorrectAnswer   ", "en", "Well done !" & vbCrLf & "The answer %1 is correct."},
        {"IncorrectAnswer ", "nl", "Het antwoord %1 is niet juist." & vbCrLf & "Probeer het nog eens."},
        {"IncorrectAnswer ", "en", "The answer %1 is incorrect." & vbCrLf & "Please try again."},
        {"IncorrectAnswer2", "nl", "Het antwoord %1 is niet helemaal juist." & vbCrLf & "Probeer het nog eens." & vbCrLf & "Totaal score is verhoogd met %2 punten."},
        {"IncorrectAnswer2", "en", "The answer %1 isn't quite correct." & vbCrLf & "Please try again." & vbCrLf & "Total score increased by %2 points."},
        {"Setup           ", "nl", "Opzetten"},
        {"Setup           ", "en", "Setup"},
        {"Play            ", "nl", "Spelen"},
        {"Play            ", "en", "Play"},
        {"Training        ", "nl", "Training"},
        {"Training        ", "en", "Training"},
        {"InvalidNAGCode  ", "nl", "De code ""%1"" is ongeldig. De waarde moet een ""$"" gevolgd door een geldig NAG-nummer zijn."},
        {"InvalidNAGCode  ", "en", "Code ""%1"" is invalid. Should be a ""$"" with valid NAG number."},
        {"InvalidTQUMove  ", "nl", "De zet ""%1"" is niet juist. Het formaat zou ""x9x9"" moeten zijn, met Van en Naar veld."},
        {"InvalidTQUMove  ", "en", "The move ""%1"" is invalid. Should be in the format ""x9x9"" specifying from field and to field."},
        {"InvalidSymbol   ", "nl", "Het symbool ""%1"" is ongeldig. De waarde zou ""R"", ""G"", ""Y"", ""+"", ""-"", ""O"", ""#"", ""."" of ""*"" moeten zijn."},
        {"InvalidSymbol   ", "en", "Symbol ""%1"" is invalid. Should be ""R"", ""G"", ""Y"", ""+"", ""-"", ""O"", ""#"", ""."" or ""*""."},
        {"InvalidColor    ", "nl", "De kleur ""%1"" is ongeldig. De waarde zou ""R"", ""G"" of ""Y"" moeten zijn."},
        {"InvalidColor    ", "en", "Color ""%1"" is invalid. Should be ""R"", ""G"" or ""Y""."},
        {"InvalidFieldName", "nl", "Veldnaam ""%1"" is ongeldig. De waarde zou tussen ""a1"" en ""h8"" moeten liggen."},
        {"InvalidFieldName", "en", "Field name ""%1"" is invalid. Should be ""a1"" thru ""h8""."},
        {"Are You Sure    ", "nl", "Ben u zeker ?"},
        {"Are You Sure    ", "en", "Are you sure ?"},
        {"UndoTooltipText ", "nl", "Ongedaan maken van wijzigingen: ""%1"""},
        {"UndoTooltipText ", "en", "Undo of changes: ""%1"""},
        {"RedoTooltipText ", "nl", "Opnieuw wijzigen: ""%1"""},
        {"RedoTooltipText ", "en", "Redo of changes:""%1"""},
        {"UndoEmpty       ", "nl", "Niets om ongedaan te maken"},
        {"UndoEmpty       ", "en", "Undo empty"},
        {"RedoEmpty       ", "nl", "Niets om opnieuw te doen"},
        {"RedoEmpty       ", "en", "Redo empty"},
        {"CommentBefore   ", "nl", "Commentaar (vooraf)"},
        {"CommentBefore   ", "en", "Comment (before)"},
        {"CommentAfter    ", "nl", "Commentaar (achteraf)"},
        {"CommentAfter    ", "en", "Comment (after)"},
        {"SwitchToTraining", "nl", "De partij bevat een of meerdere trainingsvragen." _
                          & vbLf & "Overschakelen naar modus TRAINING ?"},
        {"SwitchToTraining", "en", "The game contains one or more training questions." _
                          & vbLf & "Switch to TRAINING mode ?"},
        {"SaveChanges     ", "nl", "Het huidige (PGN/XPGN)-bestand is aangepast !" _
                          & vbLf & "Het huidige bestand eerst opslaan ?"},
        {"SaveChanges     ", "en", "Current (PGN/XPGN)-file has been changed !" _
                          & vbLf & "Save current file first ?"},
        {"AlreadyExists   ", "nl", "Het bestand  ""%1"" bestaat al in ""%2"" !" _
                          & vbLf & "Wilt u het bestaande bestand overschrijven ?"},
        {"AlreadyExists   ", "en", "File ""%1"" already exists in ""%2"" !" _
                          & vbLf & "Do you want to overwrite the existing file ?"},
        {"InvalidMove     ", "nl", "De huidige zet is geen onderdeel van een variant"},
        {"InvalidMove     ", "en", "Current move is not part of a variant"},
        {"", "", ""},
        {"", "", ""}
        }

    Public Function MessageText(pKey As String, Optional pValue1 As String = "", Optional pValue2 As String = "", Optional pValue3 As String = "") As String
        Dim R As Long, Text As String
        For R = 0 To UBound(MessageTexts)
            If Trim(MessageTexts(R, 0)) = pKey _
            And ((MessageTexts(R, 1) = "nl" And CurrentLanguage = NEDERLANDS) _
              Or (MessageTexts(R, 1) = "en" And CurrentLanguage = ENGLISH) _
              Or (MessageTexts(R, 1) = "en" And CurrentLanguage = NOTDEFINED)) Then
                Text = MessageTexts(R, 2)
                Text = Replace(Text, "%1", pValue1)
                Text = Replace(Text, "%2", pValue2)
                Text = Replace(Text, "%3", pValue3)
                Return Text
            End If
        Next R
        Throw New KeyNotFoundException(MessageText("MessageNotFound", pKey))
    End Function

End Module
