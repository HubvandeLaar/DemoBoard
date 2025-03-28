﻿Imports ChessGlobals.modChessLanguage.ChessLanguage

Public Module modMessages

    ''' <summary>Array with Muitlingual Messages using %1, %2 and %3 to insert values</summary>
    Private ReadOnly MessageTexts(,) As String = {
        {"Key               ", "Lang", "Message with %1, %2 en %3"},
        {"DeleteAllMoves    ", "nl", "Alle zetten verwijderen en zet ""%1"" toevoegen ?"},
        {"DeleteAllMoves    ", "en", "Delete all moves and add move ""%1"" ?"},
        {"InsertMove        ", "nl", "Zet ""%1"" toevoegen en volgende zetten verwijderen ?"},
        {"InsertMove        ", "en", "Insert move ""%1"", and delete all subsequent moves ?"},
        {"InsertSubVariant  ", "nl", "Zet ""%1"" toevoegen als nieuwe subvariant ?"},
        {"InsertSubVariant  ", "en", "Insert move ""%1"", as a new subvariant ?"},
        {"AddNewSubVariant  ", "nl", "Zet ""%1"" toevoegen als subvariant nummer %2 ?"},
        {"AddNewSubVariant  ", "nl", "Insert move: ""%1"", as %2th subvariant ? "},
        {"UnkownLanguage    ", "nl", "Onbekende taal opgegeven"},
        {"UnkownLanguage    ", "en", "Unkown Language specified to store"},
        {"NoSpaceFound      ", "nl", "Geen spatie gevonden in ""%1"""},
        {"NoSpaceFound      ", "en", "No Space found at ""%1"""},
        {"CommentNotEmpty   ", "nl", "Commentaar-achteraf is niet leeg ""%1"""},
        {"CommentNotEmpty   ", "en", "CommentAfter is not empty""%1"""},
        {"ReallyWrong       ", "nl", "Het spijt me, maar er gaat iets goed fout..."},
        {"ReallyWrong       ", "en", "Sorry, but something went really wrong..."},
        {"DeleteMove        ", "nl", "Zet ""%1"" en opvolgende zetten verwijderen ?"},
        {"DeleteMove        ", "en", "Delete move ""%1"" and subsequent moves ?"},
        {"MessageNotFound   ", "nl", "Bericht-tekst voor sleutel ""%1"" niet gevonden"},
        {"MessageNotFound   ", "en", "Message text with key ""%1""  not found"},
        {"ColumnRowRange    ", "nl", "Kolom ""%1"", of Rij ""%2"" is ongeldig (moet 1 t/m 8 zijn)"},
        {"ColumnRowRange    ", "en", "Column ""%1"", or Row ""%2"" out of range (Has to be be 1 to 8)"},
        {"InvalidFENPiece   ", "nl", "Ongeldig stuk ""%1"" in FEN"},
        {"InvalidFENPiece   ", "en", "Invalid Piece ""%1"" in FEN"},
        {"InvalidPiece      ", "nl", "Ongeldig stuk ""%1"""},
        {"InvalidPiece      ", "en", "Invalid Piece ""%1"""},
        {"NAGBeforeOrAfter  ", "nl", "Voor of Na niet kunnen vaststellen: ""%1"""},
        {"NAGBeforeOrAfter  ", "en", "Before Or After not recoginized: ""%1"""},
        {"UnknownNAG        ", "nl", "Onbekende NAG: ""%1"""},
        {"UnknownNAG        ", "en", "Unknown NAG: ""%1"""},
        {"PGNMoveInvalid    ", "nl", "PGN was niet specifiek genoeg voor stuk ""%1"" met doel ""%2"""},
        {"PGNMoveInvalid    ", "en", "PGN was not specific enough for Piece ""%1"" moving to ""%2"""},
        {"StrangePiece      ", "nl", "Onvoorziene situatie met het stuk : ""%1"""},
        {"StrangePiece      ", "en", "Unforseen condition with a piece named: ""%1"""},
        {"UpdateFEN         ", "nl", "Alle zetten verwijderen en de beginstelling aanpassen ?"},
        {"UpdateFEN         ", "en", "Delete all moves and update the initial position ?"},
        {"MissingSpace      ", "nl", "Geen spatie gevonden om ""%1"" op te splitsen in delen !"},
        {"MissingSpace      ", "en", "Missing space to divide ""%1"" into parts !"},
        {"DeleteMarker      ", "nl", "Verwijder markering op %1"},
        {"DeleteMarker      ", "en", "Delete marker at %1"},
        {"DeleteArrow       ", "nl", "Verwijder pijl van %1 naar %2"},
        {"DeleteArrow       ", "en", "Delete arrow from %1 to %2"},
        {"DeleteText        ", "nl", "Verwijder tekst ""%1"" van %2"},
        {"DeleteText        ", "en", "Delete text ""%1"" from %2"},
        {"AddText           ", "nl", "Tekst toevoegen"},
        {"AddText           ", "en", "Add text"},
        {"AddPiece          ", "nl", "%1 toevoegen"},
        {"AddPiece          ", "en", "Add %1"},
        {"OpenPGNFile       ", "nl", "Open PGN Bestand"},
        {"OpenPGNFile       ", "en", "Open PGN File"},
        {"DeletePiece       ", "nl", "%1 verwijderen"},
        {"DeletePiece       ", "en", "Delete %1"},
        {"SameColor         ", "nl", "Twee keer achter elkaar dezelfde kleure gezet; Toch opslaan ?"},
        {"SameColor         ", "en", "Twice or more moves of the same color; Save anyhow ?"},
        {"IntendedCastling  ", "nl", "De koning werd 2 plaatsen verplaatst; Aangenomen dat een rokade bedoeld werd"},
        {"IntendedCastling  ", "en", "King was moves 2 places; Assumed castling is intended"},
        {"InvalidImageName  ", "nl", "De opgegeven naam ""%1"" is onbekend als standaard Image"},
        {"InvalidImageName  ", "en", "The specified name ""%1"" is not defined as a standard Image"},
        {"InvalidIconName   ", "nl", "De opgegeven naam ""%1"" is onbekend als standaard Icon"},
        {"InvalidIconName   ", "en", "The specified name ""%1"" is not defined as a standard Icon"},
        {"InvalidMarkerList ", "nl", "Ongeldige indeling voor een markeringlijst in PGN"},
        {"InvalidMarerkList ", "en", "Invalid marker list format at PGN"},
        {"InvalidArrowList  ", "nl", "Ongeldige indeling voor een pijlenlijst in PGN"},
        {"InvalidArrowList  ", "en", "Invalid arrow list format at PGN"},
        {"InvalidTextList   ", "nl", "Ongeldige indeling voor een tekstlijst in PGN"},
        {"InvalidTextList   ", "en", "Invalid text list format at PGN"},
        {"TrainingQuestion  ", "nl", "Wat is de beste zet ?"},
        {"TrainingQuestion  ", "en", "What is the best move ?"},
        {"InvalidQuestion   ", "nl", "Ongeldige indeling voor een Trainingsvraag in PGN"},
        {"InvalidQuestion   ", "en", "Invalid training question format at PGN"},
        {"CorrectAnswer     ", "nl", "Goed zo !" & vbCrLf & "Het antwoord %1 is juist."},
        {"CorrectAnswer     ", "en", "Well done !" & vbCrLf & "The answer %1 is correct."},
        {"IncorrectAnswer   ", "nl", "Het antwoord %1 is niet juist." & vbCrLf & "Probeer het nog eens."},
        {"IncorrectAnswer   ", "en", "The answer %1 is incorrect." & vbCrLf & "Please try again."},
        {"IncorrectAnswer2  ", "nl", "Het antwoord %1 is niet helemaal juist." & vbCrLf & "Probeer het nog eens." & vbCrLf & "Totaal score is verhoogd met %2 punten."},
        {"IncorrectAnswer2  ", "en", "The answer %1 isn't quite correct." & vbCrLf & "Please try again." & vbCrLf & "Total score increased by %2 points."},
        {"Setup             ", "nl", "Opzetten"},
        {"Setup             ", "en", "Setup"},
        {"Play              ", "nl", "Spelen"},
        {"Play              ", "en", "Play"},
        {"Training          ", "nl", "Training"},
        {"Training          ", "en", "Training"},
        {"InvalidNAGCode    ", "nl", "De code ""%1"" is ongeldig. De waarde moet een ""$"" gevolgd door een geldig NAG-nummer zijn."},
        {"InvalidNAGCode    ", "en", "Code ""%1"" is invalid. Should be a ""$"" with valid NAG number."},
        {"InvalidTQUMove    ", "nl", "De zet ""%1"" is niet juist. Het formaat zou ""x9x9"" moeten zijn, met Van en Naar veld."},
        {"InvalidTQUMove    ", "en", "The move ""%1"" is invalid. Should be in the format ""x9x9"" specifying from field and to field."},
        {"InvalidSymbol     ", "nl", "Het symbool ""%1"" is ongeldig. De waarde zou ""R"", ""G"", ""Y"", ""+"", ""-"", ""O"", ""#"", ""."" of ""*"" moeten zijn."},
        {"InvalidSymbol     ", "en", "Symbol ""%1"" is invalid. Should be ""R"", ""G"", ""Y"", ""+"", ""-"", ""O"", ""#"", ""."" or ""*""."},
        {"InvalidColor      ", "nl", "De kleur ""%1"" is ongeldig. De waarde zou ""R"", ""G"" of ""Y"" moeten zijn."},
        {"InvalidColor      ", "en", "Color ""%1"" is invalid. Should be ""R"", ""G"" or ""Y""."},
        {"InvalidFieldName  ", "nl", "Veldnaam ""%1"" is ongeldig. De waarde zou tussen ""a1"" en ""h8"" moeten liggen."},
        {"InvalidFieldName  ", "en", "Field name ""%1"" is invalid. Should be ""a1"" thru ""h8""."},
        {"Are You Sure      ", "nl", "Ben u zeker ?"},
        {"Are You Sure      ", "en", "Are you sure ?"},
        {"UndoTooltipText   ", "nl", "Ongedaan maken van wijzigingen: ""%1"""},
        {"UndoTooltipText   ", "en", "Undo of changes: ""%1"""},
        {"RedoTooltipText   ", "nl", "Opnieuw wijzigen: ""%1"""},
        {"RedoTooltipText   ", "en", "Redo of changes:""%1"""},
        {"UndoEmpty         ", "nl", "Niets om ongedaan te maken"},
        {"UndoEmpty         ", "en", "Undo empty"},
        {"RedoEmpty         ", "nl", "Niets om opnieuw te doen"},
        {"RedoEmpty         ", "en", "Redo empty"},
        {"CommentBefore     ", "nl", "Commentaar (vooraf)"},
        {"CommentBefore     ", "en", "Comment (before)"},
        {"CommentAfter      ", "nl", "Commentaar (achteraf)"},
        {"CommentAfter      ", "en", "Comment (after)"},
        {"SwitchToTraining  ", "nl", "De partij bevat een of meerdere trainingsvragen." _
                            & vbLf & "Overschakelen naar modus TRAINING ?"},
        {"SwitchToTraining  ", "en", "The game contains one or more training questions." _
                            & vbLf & "Switch to TRAINING mode ?"},
        {"SaveChanges       ", "nl", "Het huidige (PGN/XPGN)-bestand is aangepast !" _
                            & vbLf & "Het huidige bestand eerst opslaan ?"},
        {"SaveChanges       ", "en", "Current (PGN/XPGN)-file has been changed !" _
                            & vbLf & "Save current file first ?"},
        {"AlreadyExists     ", "nl", "Het bestand  ""%1"" bestaat al in ""%2"" !" _
                            & vbLf & "Wilt u het bestaande bestand overschrijven ?"},
        {"AlreadyExists     ", "en", "File ""%1"" already exists in ""%2"" !" _
                            & vbLf & "Do you want to overwrite the existing file ?"},
        {"InvalidVariantMove", "nl", "De huidige zet is geen onderdeel van een variant"},
        {"InvalidVariantMove", "en", "Current move is not part of a variant"},
        {"IncorrectInitPos  ", "nl", "Start positie van %1 is niet helemaal goed"},
        {"IncorrectInitPos  ", "en", "Initial position of %1 is incorrect"},
        {"SameColorTwice    ", "nl", "%1 heeft twee keer gezet"},
        {"SameColorTwice    ", "en", "%1 has moved twice"},
        {"InCheckAfterMove  ", "nl", "%1 staat (nog) schaak na de zet"},
        {"InCheckAfterMove  ", "en", "%1 is (still) in check after the move"},
        {"InvalidMove       ", "nl", "Ongeldige zet voor een %1"},
        {"InvalidMove       ", "en", "Invalid move for a %1"},
        {"InvalidCastling1  ", "nl", "Ongeldige rokade; Koning staat schaak voor of na de zet"},
        {"InvalidCastling1  ", "en", "Invalid castling; King in check before or after move"},
        {"InvalidCastling2  ", "nl", "Ongeldige rokade; Koning staat even schaak tijdens de zet"},
        {"InvalidCastling2  ", "en", "Invalid castling; King in check during the move"},
        {"InvalidCastling3  ", "nl", "Ongeldige rokade; Er stond een stuk in de weg op %1"},
        {"InvalidCastling3  ", "en", "Invalid castling; A piece was standing at %1"},
        {"InvalidCastling4  ", "nl", "Ongeldige rokade; Koning stond niet op start positie"},
        {"InvalidCastling4  ", "en", "Invalid castling; King was not at intial position "},
        {"InvalidCastling5  ", "nl", "Ongeldige korte rokade; Er stond al een stuk op %1"},
        {"InvalidCastling5  ", "en", "Invalid castling on the King's side; A piece was standing at %1"},
        {"InvalidCastling6  ", "nl", "Ongeldige lange rokade; Er stond al een stuk op %1"},
        {"InvalidCastling6  ", "en", "Invalid castling on the Queen's side; A piece was standing at %1"},
        {"InvalidCastling6  ", "nl", "Ongeldige lange rokade; Koning gaat niet naar het goede veld"},
        {"InvalidCastling6  ", "en", "Invalid castling on the Queen's side; King doesn't go to the right field"},
        {"InvalidCastling7  ", "nl", "Ongeldige rokade; Koning gaat niet naar het goede veld"},
        {"InvalidCastling7  ", "en", "Invalid castling; King doesn't go to the right field"},
        {"InvalidCastling8  ", "nl", "Ongeldige korte rokade; Stukken zijn verzet"},
        {"InvalidCastling8  ", "en", "Invalid castling on the King's side; Pieces were moved"},
        {"InvalidCastling9  ", "nl", "Ongeldige lange rokade; Stukken zijn verzet"},
        {"InvalidCastling9  ", "en", "Invalid castling on the Queen's side; Pieces were moved"},
        {"MissedMateIn1     ", "nl", "Met %1 was het mat geweest"},
        {"MissedMateIn1     ", "en", "%1 would have been checkmate"},
        {"UncoverdedPiece   ", "nl", "Kon een ongedekt stuk op %1 slaan"},
        {"UncoverdedPiece   ", "en", "Could capture an uncovered piece at %1"},
        {"NotCoverded       ", "nl", "Ongedekt stuk op %1"},
        {"NotCoverded       ", "en", "Uncovered piece at %1"},
        {"TwoFoldAttack     ", "nl", "Kon tweevoudig aangevallen stuk op %1 slaan"},
        {"TwoFoldAttack     ", "en", "Could capture a twice attacked piece at %1"},
        {"TwoFoldAttacked   ", "nl", "Tweevoudig aangevallen stuk op %1"},
        {"TwoFoldAttacked   ", "en", "Twice attacked piece at %1"},
        {"ProfitExchange    ", "nl", "Voordelige ruil op %1"},
        {"ProfitExchange    ", "en", "Profitable exchange at %1"},
        {"UnprofitExchange  ", "nl", "Onvoordelige ruil op %1"},
        {"UnprofitExchange  ", "en", "Unprofitable exchange at %1"},
        {"CheckMateOpponent ", "nl", "Tegenstand kan mat zetten met %1"},
        {"CheckMateOpponent ", "en", "Opponent can checkmate using %1"},
        {"CheckMateIn2      ", "nl", "Met %1 en %2 was het mat in 2 geweest"},
        {"CheckMateIn2      ", "en", "Missed checkmate in 2 by %1 and %2"},
        {"MissedPinning     ", "nl", "Kon stuk op %1 pennen met de zet %2"},
        {"MissedPinning     ", "en", "Could pin piece at %1 with move %2"},
        {"MissedDoubleAttack", "nl", "Dubble aanval met %1 op %2 en %3 gemist"},
        {"MissedDoubleAttack", "en", "Missed double attack on %2 and %3 after %1"},
        {"MissedTripleAttack", "nl", "Driedubble aanval met %1 op %2, %3 en %4 gemist"},
        {"MissedTripleAttack", "en", "Missed triple attack on %2, %3 and %4 after %1"},
        {"MissedMultiAttack ", "nl", "Veelvuldige aanval met %1 op %2, %3, %4,... gemist"},
        {"MissedMultiAttack ", "en", "Missed multiple attack on %2, %3, %4,... after %1"},
        {"EliminateDefence  ", "nl", "Met %1 kon verdediger %2 van %3 worden uitgeschakeld"},
        {"EliminateDefence  ", "en", "Using %1 defender %2 of %3 could be eliminated"},
        {"MissedDiscoverdAttack", "nl", "Aftrekaaval %1 gemist. Kopstuk %2 met aanvalsdoel %3 en staartstuk %4 met doel %5"},
        {"MissedDiscoverdAttack", "en", "Missed discovered attack %1. Head-piece %2 could attack %3 and tail-piece &4 could attack %5"},
        {"", "", ""},
        {"", "", ""}
        }

    Public Function MessageText(pKey As String, Optional pValue1 As String = "", Optional pValue2 As String = "",
                                                Optional pValue3 As String = "", Optional pValue4 As String = "",
                                                Optional pValue5 As String = "") As String
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
                Text = Replace(Text, "%4", pValue4)
                Text = Replace(Text, "%5", pValue5)
                Return Text
            End If
        Next R
        Throw New KeyNotFoundException(MessageText("MessageNotFound", pKey))
    End Function

End Module
