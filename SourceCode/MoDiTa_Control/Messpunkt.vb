Public Class Messpunkt

    Private _ID As Integer

    Private _punktnummer As Integer
    Private _punktname As String

    Private _messdatum As String
    Private _messzeit As String

    ' Richtungen und Distanz vom Tachymeter
    Private _hz As Double
    Private _zw As Double
    Private _d As Double

    ' Kompensatorablesungen vom Tachymeter
    Private _crossInc As Double
    Private _lengthInc As Double

    ' ermittelte Bildkoordinaten (BV oder Interaktive)
    Private _bildkoordinate_x As Double
    Private _bildkoordinate_y As Double

    ' Gewicht bei einer SK-Beobachtung
    Private _skGewicht As Double

    ' Richtungen transformiert aus den Messwerten (Tachymeter), Bildkoordinaten und Transformationparameter aus SK
    Private _hz_trans As Double
    Private _zw_trans As Double

    'Private _speicherort_bild As String

    Private _tachymeter_errorcode As Integer
    Private _tachymeter_errorstring As String

#Region "Property"
    ''' <summary>
    ''' ID für das Messobjekt, sollte nur einmal auftauchen.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID() As Integer
        Get
            Return Me._ID
        End Get
        Set(ByVal value As Integer)
            Me._ID = value
        End Set
    End Property
    ''' <summary>
    ''' Punktnummer
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property punktnummer() As Integer
        Get
            Return Me._punktnummer
        End Get
        Set(ByVal value As Integer)
            Me._punktnummer = value
        End Set
    End Property
    ''' <summary>
    ''' Punktname
    ''' </summary>
    ''' <value>String</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property punktname() As String
        Get
            Return Me._punktname
        End Get
        Set(ByVal value As String)
            Me._punktname = value
        End Set
    End Property
    ''' <summary>
    ''' Messdatum
    ''' </summary>
    ''' <value>String (dd.MM.yyyy)</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property messdatum() As String
        Get
            Return Me._messdatum
        End Get
        Set(ByVal value As String)
            Me._messdatum = value
        End Set
    End Property
    ''' <summary>
    ''' Messzeit
    ''' </summary>
    ''' <value>String (HH:mm:ss:fff)</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property messzeit() As String
        Get
            Return Me._messzeit
        End Get
        Set(ByVal value As String)
            Me._messzeit = value
        End Set
    End Property
    ''' <summary>
    ''' Vom Tachymeter gemessene Horizontalrichtung
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property horizontalrichtung() As Double
        Get
            Return Me._hz
        End Get
        Set(ByVal value As Double)
            Me._hz = value
        End Set
    End Property
    ''' <summary>
    ''' Vom Tachymeter gemessenen Zenitwinkel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property zenitwinkel() As Double
        Get
            Return Me._zw
        End Get
        Set(ByVal value As Double)
            Me._zw = value
        End Set
    End Property
    ''' <summary>
    ''' Distanz (Schrägstrecke)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property distanz() As Double
        Get
            Return Me._d
        End Get
        Set(ByVal value As Double)
            Me._d = value
        End Set
    End Property
    ''' <summary>
    ''' Kompensator (cI)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property crossInclination() As Double
        Get
            Return Me._crossInc
        End Get
        Set(ByVal value As Double)
            Me._crossInc = value
        End Set
    End Property
    ''' <summary>
    ''' Kompensator (lI)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lengthInclination() As Double
        Get
            Return Me._lengthInc
        End Get
        Set(ByVal value As Double)
            Me._lengthInc = value
        End Set
    End Property
    ''' <summary>
    ''' Horizontalrichtung (der Bildkoordinaten)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property trans_horizontalrichtung() As Double
        Get
            Return Me._hz_trans
        End Get
        Set(ByVal value As Double)
            Me._hz_trans = value
        End Set
    End Property
    ''' <summary>
    ''' Zenitwinkel (der Bildkoordinaten)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property trans_zenitwinkel() As Double
        Get
            Return Me._zw_trans
        End Get
        Set(ByVal value As Double)
            Me._zw_trans = value
        End Set
    End Property
    ''' <summary>
    ''' Bildkoordiante X (Spalten)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bildkoordinate_X() As Double
        Get
            Return Me._bildkoordinate_x
        End Get
        Set(ByVal value As Double)
            Me._bildkoordinate_x = value
        End Set
    End Property
    ''' <summary>
    ''' Bildkoordinate Y (Zeilen)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bildkoordinate_Y() As Double
        Get
            Return Me._bildkoordinate_y
        End Get
        Set(ByVal value As Double)
            Me._bildkoordinate_y = value
        End Set
    End Property
    ''' <summary>
    ''' Gewicht der Messung für die Selbstkalibrierung (wird nur bei SK-Punkten gebraucht)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property gewicht_Selbstkalibrierung() As Double
        Get
            Return Me._skGewicht
        End Get
        Set(ByVal value As Double)
            Me._skGewicht = value
        End Set
    End Property
    ''' <summary>
    ''' Horizontalrichtung der Bildkoordinate (Transformiert)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property hz_trans() As Double
        Get
            Return Me._hz_trans
        End Get
        Set(ByVal value As Double)
            Me._hz_trans = value
        End Set
    End Property
    ''' <summary>
    ''' Zenitrichtung der Bildkoordinate (Transformiert)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property zw_trans() As Double
        Get
            Return Me._zw_trans
        End Get
        Set(ByVal value As Double)
            Me._zw_trans = value
        End Set
    End Property
    ''' <summary>
    ''' Fehlermeldung vom Tachymeter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tachymeter_errorstring() As String
        Get
            Return Me._tachymeter_errorstring
        End Get
        Set(ByVal value As String)
            Me._tachymeter_errorstring = value
        End Set
    End Property
    ''' <summary>
    ''' Fehlercode vom Tachymeter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tachymeter_errorcode() As Integer
        Get
            Return Me._tachymeter_errorcode
        End Get
        Set(ByVal value As Integer)
            Me._tachymeter_errorcode = value
        End Set
    End Property
#End Region
    ''' <summary>
    ''' Erzeugt aus dem Objekt eine Ziele für eine CSV-Datei
    ''' </summary>
    ''' <param name="seperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function csv_line(Optional ByVal seperator As Char = ";") As String
        Dim line As String = ""
        ' Actung bei einer Änderungen der Reihenfolgen müssen auch die anderen Functionen geändert werden.
        line &= Me._ID.ToString & seperator
        line &= Me._punktnummer.ToString & seperator
        line &= Me._punktname & seperator
        line &= Me._messdatum & seperator
        line &= Me._messzeit & seperator
        line &= Me._hz.ToString & seperator
        line &= Me._zw.ToString & seperator
        line &= Me._d.ToString & seperator
        line &= Me._crossInc.ToString & seperator
        line &= Me._lengthInc.ToString & seperator
        line &= Me._bildkoordinate_x.ToString & seperator
        line &= Me._bildkoordinate_y.ToString & seperator
        line &= Me._hz_trans.ToString & seperator
        line &= Me._zw_trans.ToString & seperator
        line &= Me._tachymeter_errorstring & seperator

        Return line
    End Function
    ''' <summary>
    ''' Teilt den String an ; und weißt die Daten zu.
    ''' </summary>
    ''' <param name="line"></param>
    ''' <remarks></remarks>
    Public Sub csvline_To_data(ByVal line As String)
        Dim line_array() As String

        line_array = ProcessLine(line)

        ' Actung bei einer Änderungen der Reihenfolgen müssen auch die anderen Functionen geändert werden.
        Me._ID = CInt(line_array(0))
        Me._punktnummer = CInt(line_array(1))
        Me._punktname = line_array(2)
        Me._messdatum = line_array(3)
        Me._messzeit = line_array(4)
        Me._hz = CDbl(line_array(5))
        Me._zw = CDbl(line_array(6))
        Me._d = CDbl(line_array(7))
        Me._crossInc = CDbl(line_array(8))
        Me._lengthInc = CDbl(line_array(9))
        Me._bildkoordinate_x = CDbl(line_array(10))
        Me._bildkoordinate_y = CDbl(line_array(11))
        Me._hz_trans = CDbl(line_array(12))
        Me._zw_trans = CDbl(line_array(13))
        Me._tachymeter_errorstring = line_array(14)
    End Sub

    ''' <summary>
    ''' Erzeugt ein Header für eine CSV-Datei
    ''' </summary>
    ''' <param name="seperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function csv_header(Optional ByVal seperator As Char = ";") As String
        Dim line As String = ""
        ' Actung bei einer Änderungen der Reihenfolgen müssen auch die anderen Functionen geändert werden.
        line &= "ID" & seperator
        line &= "punktnummer" & seperator
        line &= "punktname" & seperator
        line &= "messdatum" & seperator
        line &= "messzeit" & seperator
        line &= "hz" & seperator
        line &= "zw" & seperator
        line &= "d" & seperator
        line &= "crossInc" & seperator
        line &= "lengthInc" & seperator
        line &= "bildkoordinate_x" & seperator
        line &= "bildkoordinate_y" & seperator
        line &= "hz_trans" & seperator
        line &= "zw_trans" & seperator
        line &= "tachymeter_errorstring" & seperator

        Return line
    End Function
    '
    ' Process Line, Teilt ein String an ";" und gibt diese als Stringarray zurück
    ' Die Basis der Funktion ist von John Priestley, angepasst von Stefan Hauth
    '
    Private Function ProcessLine(ByVal sLine As String) As String()
        Dim regQuote As New System.Text.RegularExpressions.Regex("^(\x22)(.*)(\x22)(\s*;)(.*)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.RightToLeft)
        Dim regNormal As New System.Text.RegularExpressions.Regex("^([^;]*)(\s*;)(.*)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.RightToLeft)
        Dim regQuoteLast As New System.Text.RegularExpressions.Regex("^(\x22)([\x22*]{2;})(\x22)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim regNormalLast As New System.Text.RegularExpressions.Regex("^.*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Dim sData As String
        Dim m As System.Text.RegularExpressions.Match
        Dim idx As Integer
        Dim mc As System.Text.RegularExpressions.MatchCollection
        Dim mData() As String

        Erase mData
        sLine = sLine.Replace(ControlChars.Tab, "    ") 'Replace tab with 4 spaces
        sLine = sLine.Trim

        Do While sLine.Length > 0
            sData = ""

            If regQuote.IsMatch(sLine) Then
                mc = regQuote.Matches(sLine)
                '
                ' "text",<rest of the line>
                '
                m = regQuote.Match(sLine)
                sData = m.Groups(2).Value
                sLine = m.Groups(5).Value
            ElseIf regQuoteLast.IsMatch(sLine) Then
                '
                ' "text"
                '
                m = regQuoteLast.Match(sLine)
                sData = m.Groups(2).Value
                sLine = ""
            ElseIf regNormal.IsMatch(sLine) Then
                '
                ' text,<rest of the line>
                '
                m = regNormal.Match(sLine)
                sData = m.Groups(1).Value
                sLine = m.Groups(3).Value
            ElseIf regNormalLast.IsMatch(sLine) Then
                '
                ' text
                '
                m = regNormalLast.Match(sLine)
                sData = m.Groups(0).Value
                sLine = ""
            Else
                '
                ' ERROR!!!!!
                '
                sData = ""
                sLine = ""
            End If

            sData = sData.Trim
            sLine = sLine.Trim

            If mData Is Nothing Then
                ReDim mData(0)
                idx = 0
            Else
                idx = mData.GetUpperBound(0) + 1
                ReDim Preserve mData(idx)
            End If

            mData(idx) = sData
        Loop
        Return mData
    End Function
End Class
