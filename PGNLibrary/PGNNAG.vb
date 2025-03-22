Option Explicit On

Imports ChessGlobals
Imports ChessMaterials
Imports ChessGlobals.ChessLanguage
Imports PGNLibrary.PGNNAG.NAGPrintPosition
Imports System.Xml.Serialization

<XmlType>
Public Class PGNNAG

    Public Enum NAGType
        <XmlEnum()>
        TEXT
        <XmlEnum()>
        CODE
    End Enum

    Public Enum NAGPrintPosition
        <XmlEnum()>
        BEFORE
        <XmlEnum()>
        AFTER
        <XmlEnum()>
        BEFOREANDAFTER
    End Enum

    <XmlAttribute()>
    Public Type As NAGType
    <XmlAttribute()>
    Public Code As Long = 0
    <XmlAttribute()>
    Public Font As String = ""
    <XmlAttribute()>
    Public PrintPosition As NAGPrintPosition = AFTER

    <XmlAttribute()>
    Public gPGNString As String = ""
    <XmlAttribute()>
    Public gEnglishText As String = ""
    <XmlAttribute()>
    Public gDutchText As String = ""


    ''' <summary>First position contains "$"</summary>
    <XmlIgnore>
    Property PGNString() As String
        Set(pPGNString As String)
            gPGNString = pPGNString
            If Left(pPGNString, 1) <> "$" Then
                Throw New NotImplementedException("Unknown Language: ")
            End If
            Me.StoreValues(Val(Mid(pPGNString, 2)))
        End Set
        Get
            Return gPGNString
        End Get
    End Property

    <XmlIgnore>
    ReadOnly Property Text(Optional pLanguage As ChessLanguage = ENGLISH) As String
        Get
            Select Case pLanguage
                Case ENGLISH
                    Return gEnglishText
                Case NEDERLANDS
                    Return gDutchText
                    If gDutchText = "" Then Text = gEnglishText
                Case Else
                    Throw New NotImplementedException("Unknown Language: " & pLanguage)
                    Return ""
            End Select
        End Get
    End Property

    Public Overrides Function ToString() As String
        'For debugging puposes 
        Return Me.PGNString
    End Function

    Private Sub StoreValues(pNumber As Long)
        'First the standard codes, independend from Language
        'See http://unicode-table.com
        Const FigurineCB As String = "FigurineCB TimeSP"
        Select Case pNumber
            Case 0 : Me.StoreNAGText(BOTH, "")
                'Move assessments
            Case 1 : Me.StoreNAGText(BOTH, "!")  'An excellent move
            Case 2 : Me.StoreNAGText(BOTH, "?")  'A brilliant move
            Case 3 : Me.StoreNAGText(BOTH, "!!") 'A bad move
            Case 4 : Me.StoreNAGText(BOTH, "??") 'A blunder
            Case 5 : Me.StoreNAGText(BOTH, "!?") 'An interesting move, probably good
            Case 6 : Me.StoreNAGText(BOTH, "?!") 'A dubious move
            Case 7 : Me.StoreNAGCode(9633)    'The only reasonable move
            Case 8 : Me.StoreNAGCode(9633)    'Singular move (no reasonable alternatives)
            Case 9 : Me.StoreNAGText(ENGLISH, " Worst Move")
                Me.StoreNAGText(NEDERLANDS, " allerslechtste zet")
                'Positional Assessments
            Case 10 : Me.StoreNAGText(BOTH, "=", FigurineCB)  'The chances are equal
            Case 11 : Me.StoreNAGText(BOTH, "=", FigurineCB)  'The chances are equal
            Case 12 : Me.StoreNAGText(ENGLISH, " Equal chances, active position")
                Me.StoreNAGText(NEDERLANDS, " gelijke kansen, actieve stelling")
            Case 13 : Me.StoreNAGCode(8734)    'Unclear Position
            Case 14 : Me.StoreNAGCode(10866)   'White is slightly better
            Case 15 : Me.StoreNAGCode(10865)   'Black is slightly better
            Case 16 : Me.StoreNAGCode(177)     'White has a moderate advantage
            Case 17 : Me.StoreNAGCode(8723)    'Black has a moderate advantage
            Case 18 : Me.StoreNAGText(BOTH, "+-", FigurineCB) 'White has a decisive advantage
            Case 19 : Me.StoreNAGText(BOTH, "-+", FigurineCB) 'Black has a decisive advantage
            Case 20 : Me.StoreNAGText(BOTH, " White has a crushing advantage (Black should resign)")
            Case 21 : Me.StoreNAGText(BOTH, " Black has a crushing advantage (White should resign)")
            Case 22 : Me.StoreNAGCode(10752)   'White is in zugzwang
            Case 23 : Me.StoreNAGCode(10752)   'Black is in zugzwang
            Case 24 : Me.StoreNAGText(BOTH, " White has a slight space advantage")
            Case 25 : Me.StoreNAGText(BOTH, " Black has a slight space advantage")
            Case 26 : Me.StoreNAGText(BOTH, " White has a moderate space advantage")
            Case 27 : Me.StoreNAGText(BOTH, " Black has a moderate space advantage")
            Case 28 : Me.StoreNAGText(BOTH, " White has a decisive space advantage")
            Case 29 : Me.StoreNAGText(BOTH, " Black has a decisive space advantage")
            Case 30 : Me.StoreNAGText(BOTH, " White has a slight time (development) advantage")
            Case 31 : Me.StoreNAGText(BOTH, " Black has a slight time (development) advantage")
            Case 32 : Me.StoreNAGCode(10227)   'White has advantage in development
            Case 33 : Me.StoreNAGCode(10227)   'Black has advantage in development
            Case 34 : Me.StoreNAGText(BOTH, " White has a decisive time (development) advantage")
            Case 35 : Me.StoreNAGText(BOTH, " Black has a decisive time (development) advantage")
            Case 36 : Me.StoreNAGCode(8594)    'with an attack
            Case 37 : Me.StoreNAGCode(8594)    'with an attack
            Case 38 : Me.StoreNAGText(ENGLISH, " White has a lasting initiative")
                Me.StoreNAGText(NEDERLANDS, " met ontwikkelingsvoorsprong voor wit")
            Case 39 : Me.StoreNAGText(ENGLISH, " Black has a lasting initiative")
                Me.StoreNAGText(NEDERLANDS, " met ontwikkelingsvoorsprong voor zwart")
            Case 40 : Me.StoreNAGCode(8593)    'White has the attack
            Case 41 : Me.StoreNAGCode(8593)    'Black has the attack
            Case 42 : Me.StoreNAGCode(176, FigurineCB) 'White has insufficient compensation for material deficit")
            Case 43 : Me.StoreNAGCode(176, FigurineCB) 'Black has insufficient compensation for material deficit")
            Case 44 : Me.StoreNAGCode(169, FigurineCB) 'White has sufficient compensation for material deficit")
            Case 45 : Me.StoreNAGCode(169, FigurineCB) 'Black has sufficient compensation for material deficit")
            Case 46 : Me.StoreNAGCode(169, FigurineCB) 'White has more than adequate compensation for material deficit")
            Case 47 : Me.StoreNAGCode(169, FigurineCB) 'Black has more than adequate compensation for material deficit")
            Case 48 : Me.StoreNAGText(BOTH, " White has a slight center control advantage")
            Case 49 : Me.StoreNAGText(BOTH, " Black has a slight center control advantage")
            Case 50 : Me.StoreNAGText(BOTH, " White has a moderate center control advantage")
            Case 51 : Me.StoreNAGText(BOTH, " Black has a moderate center control advantage")
            Case 52 : Me.StoreNAGText(BOTH, " White has a decisive center control advantage")
            Case 53 : Me.StoreNAGText(BOTH, " Black has a decisive center control advantage")
            Case 54 : Me.StoreNAGText(BOTH, " White has a slight kingside control advantage")
            Case 55 : Me.StoreNAGText(BOTH, " Black has a slight kingside control advantage")
            Case 56 : Me.StoreNAGText(BOTH, " White has a moderate kingside control advantage")
            Case 57 : Me.StoreNAGText(BOTH, " Black has a moderate kingside control advantage")
            Case 58 : Me.StoreNAGText(BOTH, " White has a decisive kingside control advantage")
            Case 59 : Me.StoreNAGText(BOTH, " Black has a decisive kingside control advantage")
            Case 60 : Me.StoreNAGText(BOTH, " White has a slight queenside control advantage")
            Case 61 : Me.StoreNAGText(BOTH, " Black has a slight queenside control advantage")
            Case 62 : Me.StoreNAGText(BOTH, " White has a moderate queenside control advantage")
            Case 63 : Me.StoreNAGText(BOTH, " Black has a moderate queenside control advantage")
            Case 64 : Me.StoreNAGText(BOTH, " White has a decisive queenside control advantage")
            Case 65 : Me.StoreNAGText(BOTH, " Black has a decisive queenside control advantage")
            Case 66 : Me.StoreNAGText(BOTH, " White has a vulnerable first rank")
            Case 67 : Me.StoreNAGText(BOTH, " Black has a vulnerable first rank")
            Case 68 : Me.StoreNAGText(BOTH, " White has a well protected first rank")
            Case 69 : Me.StoreNAGText(BOTH, " Black has a well protected first rank")
            Case 70 : Me.StoreNAGText(BOTH, " White has a poorly protected king")
            Case 71 : Me.StoreNAGText(BOTH, " Black has a poorly protected king")
            Case 72 : Me.StoreNAGText(BOTH, " White has a well protected king")
            Case 73 : Me.StoreNAGText(BOTH, " Black has a well protected king")
            Case 74 : Me.StoreNAGText(BOTH, " White has a poorly placed king")
            Case 75 : Me.StoreNAGText(BOTH, " Black has a poorly placed king")
            Case 76 : Me.StoreNAGText(BOTH, " White has a well placed king")
            Case 77 : Me.StoreNAGText(BOTH, " Black has a well placed king")
            Case 78 : Me.StoreNAGText(BOTH, " White has a very weak pawn structure")
            Case 79 : Me.StoreNAGText(BOTH, " Black has a very weak pawn structure")
            Case 80 : Me.StoreNAGText(BOTH, " White has a moderately weak pawn structure")
            Case 81 : Me.StoreNAGText(BOTH, " Black has a moderately weak pawn structure")
            Case 82 : Me.StoreNAGText(BOTH, " White has a moderately strong pawn structure")
            Case 83 : Me.StoreNAGText(BOTH, " Black has a moderately strong pawn structure")
            Case 84 : Me.StoreNAGText(BOTH, " White has a very strong pawn structure")
            Case 85 : Me.StoreNAGText(BOTH, " Black has a very strong pawn structure")
            Case 86 : Me.StoreNAGText(BOTH, " White has poor knight placement")
            Case 87 : Me.StoreNAGText(BOTH, " Black has poor knight placement")
            Case 88 : Me.StoreNAGText(BOTH, " White has good knight placement")
            Case 89 : Me.StoreNAGText(BOTH, " Black has good knight placement")
            Case 90 : Me.StoreNAGText(BOTH, " White has poor bishop placement")
            Case 91 : Me.StoreNAGText(BOTH, " Black has poor bishop placement")
            Case 92 : Me.StoreNAGText(BOTH, " White has good bishop placement")
            Case 93 : Me.StoreNAGText(BOTH, " Black has good bishop placement")
            Case 94 : Me.StoreNAGText(BOTH, " White has poor rook placement")
            Case 95 : Me.StoreNAGText(BOTH, " Black has poor rook placement")
            Case 96 : Me.StoreNAGText(BOTH, " White has good rook placement")
            Case 97 : Me.StoreNAGText(BOTH, " Black has good rook placement")
            Case 98 : Me.StoreNAGText(BOTH, " White has poor queen placement")
            Case 99 : Me.StoreNAGText(BOTH, " Black has poor queen placement")
            Case 100 : Me.StoreNAGText(BOTH, " White has good queen placement")
            Case 101 : Me.StoreNAGText(BOTH, " Black has good queen placement")
            Case 102 : Me.StoreNAGText(BOTH, " White has poor piece coordination")
            Case 103 : Me.StoreNAGText(BOTH, " Black has poor piece coordination")
            Case 104 : Me.StoreNAGText(BOTH, " White has good piece coordination")
            Case 105 : Me.StoreNAGText(BOTH, " Black has good piece coordination")
            Case 106 : Me.StoreNAGText(BOTH, " White has played the opening very poorly")
            Case 107 : Me.StoreNAGText(BOTH, " Black has played the opening very poorly")
            Case 108 : Me.StoreNAGText(BOTH, " White has played the opening poorly")
            Case 109 : Me.StoreNAGText(BOTH, " Black has played the opening poorly")
            Case 110 : Me.StoreNAGText(BOTH, " White has played the opening well")
            Case 111 : Me.StoreNAGText(BOTH, " Black has played the opening well")
            Case 112 : Me.StoreNAGText(BOTH, " White has played the opening very well")
            Case 113 : Me.StoreNAGText(BOTH, " Black has played the opening very well")
            Case 114 : Me.StoreNAGText(BOTH, " White has played the middlegame very poorly")
            Case 115 : Me.StoreNAGText(BOTH, " Black has played the middlegame very poorly")
            Case 116 : Me.StoreNAGText(BOTH, " White has played the middlegame poorly")
            Case 117 : Me.StoreNAGText(BOTH, " Black has played the middlegame poorly")
            Case 118 : Me.StoreNAGText(BOTH, " White has played the middlegame well")
            Case 119 : Me.StoreNAGText(BOTH, " Black has played the middlegame well")
            Case 120 : Me.StoreNAGText(BOTH, " White has played the middlegame very well")
            Case 121 : Me.StoreNAGText(BOTH, " Black has played the middlegame very well")
            Case 122 : Me.StoreNAGText(BOTH, " White has played the ending very poorly")
            Case 123 : Me.StoreNAGText(BOTH, " Black has played the ending very poorly")
            Case 124 : Me.StoreNAGText(BOTH, " White has played the ending poorly")
            Case 125 : Me.StoreNAGText(BOTH, " Black has played the ending poorly")
            Case 126 : Me.StoreNAGText(BOTH, " White has played the ending well")
            Case 127 : Me.StoreNAGText(BOTH, " Black has played the ending well")
            Case 128 : Me.StoreNAGText(BOTH, " White has played the ending very well")
            Case 129 : Me.StoreNAGText(BOTH, " Black has played the ending very well")
            Case 130 : Me.StoreNAGCode(8646)    'White has slight counterplay
            Case 131 : Me.StoreNAGCode(8646)    'Black has slight counterplay
            Case 132 : Me.StoreNAGCode(8646)    'White has moderate counterplay
            Case 133 : Me.StoreNAGCode(8646)    'Black has moderate counterplay
            Case 134 : Me.StoreNAGText(BOTH, " White has decisive counterplay")
            Case 135 : Me.StoreNAGText(BOTH, " Black has decisive counterplay")
                'Time Pressure Commentaries
            Case 136 : Me.StoreNAGCode(8220, FigurineCB) 'White has moderate time control pressure
            Case 137 : Me.StoreNAGCode(8220, FigurineCB) 'Black has moderate time control pressure
            Case 138 : Me.StoreNAGCode(8220, FigurineCB) 'White has severe time control pressure
            Case 139 : Me.StoreNAGCode(8220, FigurineCB) 'Black has severe time control pressure
                'Non-standard NAGs (ChessPad)
            Case 140 : Me.StoreNAGCode(8710, pPrintPosition:=BEFORE)     'With the idea
            Case 141 : Me.StoreNAGText(BOTH, " Aimed against ", pPrintPosition:=BEFORE)
            Case 142 : Me.StoreNAGCode(8979, pPrintPosition:=BEFORE)    'Better is
            Case 143 : Me.StoreNAGCode(8804, pPrintPosition:=BEFORE)    'Worse is
            Case 144 : Me.StoreNAGText(BOTH, " Equivalent is ", pPrintPosition:=BEFORE)
            Case 145 : Me.StoreNAGText(BOTH, "RR") 'Editorial comment
            Case 146 : Me.StoreNAGText(BOTH, "N")  'Novelty
                '147–219  Not defined
            Case 220 : Me.StoreNAGText(BOTH, " Diagram")
            Case 221 : Me.StoreNAGText(BOTH, " Diagram (from Black)")
                '222–237  Not defined
            Case 238 : Me.StoreNAGCode(8224, FigurineCB) 'Space advantage
            Case 239 : Me.StoreNAGCode(8660)    'File (columns on the chessboard labeled a-h)
            Case 240 : Me.StoreNAGCode(8663)    'Diagonal
            Case 241 : Me.StoreNAGCode(8862)    'Center
            Case 242 : Me.StoreNAGCode(10219)   'King-side
            Case 243 : Me.StoreNAGCode(10218)   'Queen-side
            Case 244 : Me.StoreNAGText(BOTH, "X")  'Weak point
            Case 245 : Me.StoreNAGCode(8869)    'Ending
            Case 246 : Me.StoreNAGCode(173, FigurineCB) 'bishop pair
            Case 247 : Me.StoreNAGCode(174, FigurineCB) 'Opposite bishops
            Case 248 : Me.StoreNAGCode(175, FigurineCB) 'Same bishops
            Case 249 : Me.StoreNAGText(BOTH, " Connected pawns text")
            Case 250 : Me.StoreNAGText(BOTH, " Isolated pawns text")
            Case 251 : Me.StoreNAGText(BOTH, " Doubled pawns text")
            Case 252 : Me.StoreNAGText(BOTH, " Passed pawn")
            Case 253 : Me.StoreNAGText(BOTH, " Pawn majority text")
            Case 254 : Me.StoreNAGCode(170, FigurineCB) ' With
            Case 255 : Me.StoreNAGCode(186, FigurineCB) ' Without
            Case Else : Throw New NotImplementedException(MessageText("UnknownNAG", Str(pNumber)))
        End Select
    End Sub

    Private Sub StoreNAGText(pLanguage As ChessLanguage, pText As String,
                                  Optional pFont As String = "Calibri",
                                  Optional pPrintPosition As NAGPrintPosition = AFTER)
        Type = NAGType.TEXT
        Font = pFont
        PrintPosition = pPrintPosition
        Select Case pLanguage
            Case BOTH
                gEnglishText = pText
                gDutchText = pText
            Case ENGLISH
                gEnglishText = pText
            Case NEDERLANDS
                gDutchText = pText
        End Select
    End Sub

    Private Sub StoreNAGCode(pCode As Long,
                                  Optional pFont As String = "Cambria Math",
                                  Optional pPrintPosition As NAGPrintPosition = AFTER)
        Type = NAGType.CODE
        Code = pCode
        Font = pFont
        PrintPosition = pPrintPosition
    End Sub

    Sub New(pPGNString As String)
        Me.PGNString = pPGNString
    End Sub

    Sub New() 'Needed for (de)serialization
        Me.PGNString = ""
    End Sub

End Class
