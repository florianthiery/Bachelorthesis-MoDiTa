Imports System.IO

Public Class resolutiontest

    'Stützpunkte
    Public _zentrum_Hz As Double
    Public _zentrum_Vz As Double
    Public _rand_Hz As Double
    Public _rand_Vz As Double

    'Weitere Stützpunkte
    Public _zentrum_Hz_M2 As Double
    Public _zentrum_Vz_M2 As Double
    Public _zentrum_Hz_M3 As Double
    Public _zentrum_Vz_M3 As Double
    Public _rand_Hz_M2 As Double
    Public _rand_Vz_M2 As Double
    Public _rand_Hz_M3 As Double
    Public _rand_Vz_M3 As Double

    'Verfahrweg in gon
    Private _delta_Hz As Double
    Private _delta_Vz As Double
    Private _delta_Hz_M2 As Double
    Private _delta_Vz_M2 As Double
    Private _delta_Hz_M3 As Double
    Private _delta_Vz_M3 As Double

    'Verfahrmodus
    Private _mode As Integer

    'Referenzen auf Tachymeterkontrollklasse und uEye Camera
    Private _tachymetercontrol As ControlLeicaTPS1000
    Private _camera_uEye As uEyeCamera
    Private _camera_table As DataTable
    Private _camera_csv As camera_csv

    'IDs
    Private _ID As Integer 'ID der Aktionen (Verfahren + Bilderstellung)
    Private _imageID As Integer 'Bildernummer

    'Bildspeicherung
    Private _filename As String
    Private _project_dir As String
    Private _messung_name As String
    Private _wartezeit As Integer
    Private _messzeit As String

    'Messungsverwaltung
    Public _messung1_zentrum As Boolean
    Public _messung1_rand As Boolean
    Public _messung2_zentrum As Boolean
    Public _messung2_rand As Boolean
    Public _messung3_zentrum As Boolean
    Public _messung3_rand As Boolean

    'Shifts
    Private _hz_shift As Boolean
    Private _vz_shift As Boolean
    Private _anz_messungen As Integer
    Private _akt_shift As Integer

    'Winkel zum Anfahren
    Public _zentrum_Hz_neu As Double
    Public _zentrum_Vz_neu As Double
    Private _delta_Hz_neu As Double
    Private _delta_Vz_neu As Double

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me._zentrum_Hz = 1000.0
        Me._zentrum_Vz = 0.0
        Me._rand_Hz = 1000.0
        Me._rand_Vz = 0.0
        Me._zentrum_Hz_M2 = 0.0
        Me._zentrum_Vz_M2 = 0.0
        Me._rand_Hz_M2 = 0.0
        Me._rand_Vz_M2 = 0.0
        Me._zentrum_Hz_M3 = 0.0
        Me._zentrum_Vz_M3 = 0.0
        Me._rand_Hz_M3 = 0.0
        Me._rand_Vz_M3 = 0.0
        Me._ID = 0
        Me._imageID = 1
        Me._messung_name = "noname"
        Me._filename = ""
        Me._delta_Hz = 0.0
        Me._delta_Vz = 0.0
        Me._delta_Hz_M2 = 0.0
        Me._delta_Vz_M2 = 0.0
        Me._delta_Hz_M3 = 0.0
        Me._delta_Vz_M3 = 0.0
        Me._messung1_zentrum = False
        Me._messung1_rand = False
        Me._messung2_zentrum = False
        Me._messung2_rand = False
        Me._messung3_zentrum = False
        Me._messung3_rand = False
        Me._messzeit = "noname"
        Me._hz_shift = False
        Me._vz_shift = False
        Me._anz_messungen = 0
        Me._akt_shift = 0
        Me._zentrum_Hz_neu = 0.0
        Me._zentrum_Vz_neu = 0.0
        Me._delta_Hz_neu = 0.0
        Me._delta_Vz_neu = 0.0
    End Sub

    ''' <summary>
    ''' Werte zurücksetzen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub reset()
        Me._zentrum_Hz = 1000.0
        Me._zentrum_Vz = 0.0
        Me._rand_Hz = 1000.0
        Me._rand_Vz = 0.0
        Me._zentrum_Hz_M2 = 0.0
        Me._zentrum_Vz_M2 = 0.0
        Me._rand_Hz_M2 = 0.0
        Me._rand_Vz_M2 = 0.0
        Me._zentrum_Hz_M3 = 0.0
        Me._zentrum_Vz_M3 = 0.0
        Me._rand_Hz_M3 = 0.0
        Me._rand_Vz_M3 = 0.0
        Me._ID = 0
        Me._imageID = 1
        Me._messung_name = "noname"
        Me._filename = ""
        Me._delta_Hz = 0.0
        Me._delta_Vz = 0.0
        Me._delta_Hz_M2 = 0.0
        Me._delta_Vz_M2 = 0.0
        Me._delta_Hz_M3 = 0.0
        Me._delta_Vz_M3 = 0.0
        Me._messung1_zentrum = False
        Me._messung1_rand = False
        Me._messung2_zentrum = False
        Me._messung2_rand = False
        Me._messung3_zentrum = False
        Me._messung3_rand = False
        Me._messzeit = "noname"
        Me._hz_shift = False
        Me._vz_shift = False
        Me._anz_messungen = 0
        Me._akt_shift = 0
        Me._zentrum_Hz_neu = 0.0
        Me._zentrum_Vz_neu = 0.0
        Me._delta_Hz_neu = 0.0
        Me._delta_Vz_neu = 0.0
    End Sub

    ''' <summary>
    ''' Startet Bildmessung
    ''' </summary>
    ''' <param name="_tachymetercontrol">Kontrollklasse Tachymeter</param>
    ''' <param name="_camera_uEye">Kontrollklasse Camera</param>
    ''' <param name="_project_dir">Projektverzeichnis</param>
    ''' <param name="_messung_name">Name der Messung</param>
    ''' <param name="_mode">Modus des Verfahrweges</param>
    ''' <param name="_wartezeit">Wartezeit nach Verfahren</param>
    ''' <param name="_messzeit">Start der Messung</param>
    ''' <param name="_hz_shift">Hz Shift?</param>
    ''' <param name="_vz_shit">Vz Shift?</param>
    ''' <remarks></remarks>
    Public Sub Starte_Messung(ByRef _tachymetercontrol As ControlLeicaTPS1000, _
                              ByRef _camera_uEye As uEyeCamera, _
                              ByVal _project_dir As String, _
                              ByVal _messung_name As String, _
                              ByVal _mode As Integer, _
                              ByVal _wartezeit As String, _
                              ByVal _messzeit As String, _
                              ByVal _hz_shift As Boolean, _
                              ByVal _vz_shit As Boolean)

        'Übernahme der Referenz und Value Übergaben in Klassenvariablen
        Me._tachymetercontrol = _tachymetercontrol
        Me._camera_uEye = _camera_uEye
        Me._project_dir = _project_dir
        Me._messung_name = _messung_name
        Me._mode = _mode
        Me._wartezeit = CInt(_wartezeit)
        Me._messzeit = _messzeit
        Me._hz_shift = _hz_shift
        Me._vz_shift = _vz_shit

        'Erstelle Table
        _camera_csv = New camera_csv
        _camera_table = _camera_csv.CreateTable()

        'IDs setzten
        _ID = 0
        _imageID = 1

        'Directory erzeugen
        Directory.CreateDirectory(_project_dir + "\image_measure\" + _
                                  _messung_name + "_" + _messzeit)

        'Verschiebeweg berechnen
        _delta_Vz = _zentrum_Vz - _rand_Vz
        _delta_Hz = _zentrum_Hz - _rand_Hz

        'Verschiebeweg für Shifts berechnen
        If _hz_shift = True Then
            If _messung2_zentrum = True And _messung2_rand = True Then
                _delta_Hz_M2 = _zentrum_Hz_M2 - _rand_Hz_M2
            End If
            If _messung3_zentrum = True And _messung3_rand = True Then
                _delta_Hz_M3 = _zentrum_Hz_M3 - _rand_Hz_M3
            End If
        ElseIf _vz_shift = True Then
            If _messung2_zentrum = True And _messung2_rand = True Then
                _delta_Vz_M2 = _zentrum_Vz_M2 - _rand_Vz_M2
            End If
            If _messung3_zentrum = True And _messung3_rand = True Then
                _delta_Vz_M3 = _zentrum_Vz_M3 - _rand_Vz_M3
            End If
        End If

        'Anzahl der Shifts berechnen
        If _messung1_zentrum = True And _messung1_rand = True And _
           _messung2_zentrum = False And _messung2_rand = False And _
           _messung3_zentrum = False And _messung3_rand = False Then
            _anz_messungen = 1
        ElseIf _messung1_zentrum = True And _messung1_rand = True And _
               _messung2_zentrum = True And _messung2_rand = True And _
               _messung3_zentrum = False And _messung3_rand = False Then
            _anz_messungen = 2
        ElseIf _messung1_zentrum = True And _messung1_rand = True And _
               _messung2_zentrum = True And _messung2_rand = True And _
               _messung3_zentrum = True And _messung3_rand = True Then
            _anz_messungen = 3
        End If

        'Aktuellen Shift setzen
        _akt_shift = 1

        MoDiTaGUI.changeTachyStatusSymbol(2) 'Änderung des Status des Tachymeters

        'Ersten Winkeln zum Verfahren suchen
        Detect_Angle()

    End Sub

    ''' <summary>
    ''' Ermittelt den Verfahrweg
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Detect_Angle()

        'Winkel zum Verfahren
        Dim drive_to_Vz As Double = 0.0
        Dim drive_to_Hz As Double = 0.0

        'Shifts berechnen
        If _akt_shift = 1 Then
            _zentrum_Hz_neu = _zentrum_Hz
            _zentrum_Vz_neu = _zentrum_Vz
            _delta_Hz_neu = _delta_Hz
            _delta_Vz_neu = _delta_Vz
        ElseIf _akt_shift = 2 Then
            If _hz_shift = True Then
                _zentrum_Hz_neu = _zentrum_Hz_M2
                _zentrum_Vz_neu = _zentrum_Vz
                _delta_Hz_neu = _delta_Hz_M2
                _delta_Vz_neu = _delta_Vz
            ElseIf _vz_shift = True Then
                _zentrum_Hz_neu = _zentrum_Hz
                _zentrum_Vz_neu = _zentrum_Vz_M2
                _delta_Hz_neu = _delta_Hz
                _delta_Vz_neu = _delta_Vz_M2
            End If
        ElseIf _akt_shift = 3 Then
            If _hz_shift = True Then
                _zentrum_Hz_neu = _zentrum_Hz_M3
                _zentrum_Vz_neu = _zentrum_Vz
                _delta_Hz_neu = _delta_Hz_M3
                _delta_Vz_neu = _delta_Vz
            ElseIf _vz_shift = True Then
                _zentrum_Hz_neu = _zentrum_Hz
                _zentrum_Vz_neu = _zentrum_Vz_M3
                _delta_Hz_neu = _delta_Hz
                _delta_Vz_neu = _delta_Vz_M3
            End If
        End If

        'Koordinierung der Verfahrwege
        If _mode = 1 Then 'Alle Positionen (Viereck)
            If _ID = 0 Then 'Rand Oben Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 2 Then 'Rand Oben Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 4 Then 'Rand Oben
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 6 Then 'Rand Oben Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 8 Then 'Rand Oben Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu

            ElseIf _ID = 10 Then 'Rand Oben Mitte Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 12 Then 'Rand Oben Mitte Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 14 Then 'Rand Oben Mitte
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 16 Then 'Rand Oben Mitte Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 18 Then 'Rand Oben Mitte Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)

            ElseIf _ID = 20 Then 'Rand Mitte Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 22 Then 'Rand Mitte Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 24 Then 'Rand Mitte
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 26 Then 'Rand Mitte Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 28 Then 'Rand Mitte Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu

            ElseIf _ID = 30 Then 'Rand Unten Mitte Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 32 Then 'Rand Unten Mitte Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 34 Then 'Rand Unten Mitte
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 36 Then 'Rand Unten Mitte Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 38 Then 'Rand Unten Mitte Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)

            ElseIf _ID = 40 Then 'Rand Unten Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 42 Then 'Rand Unten Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 44 Then 'Rand Unten
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 46 Then 'Rand Unten Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 48 Then 'Rand Unten Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID >= 49 Then 'Ende des Verfahrweges
                If _akt_shift < _anz_messungen Then
                    drive_to_Hz = _zentrum_Hz_neu
                    drive_to_Vz = _zentrum_Vz_neu
                    _akt_shift = _akt_shift + 1
                    _ID = -2
                    _imageID = 1
                Else
                    MoDiTaGUI.changeTachyStatusSymbol(1) 'Änderung des Status des Tachymeters
                    Dim speicherort As String
                    speicherort = _project_dir + "\image_measure\" + _
                                  _messung_name + "_" + _messzeit + "\" + _
                                  _messung_name + "_" + _messzeit + ".csv"
                    _camera_csv.KameraData_to_CSV(_camera_table, speicherort)
                    Exit Sub
                End If
            End If

        ElseIf _mode = 2 Then ' Kreuz aufrecht
            If _ID = 0 Then 'Rand Oben
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 2 Then 'Rand Oben Mitte
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 4 Then 'Zentrum
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 6 Then 'Rand Unten Mitte 
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 8 Then 'Rand Unten
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 10 Then 'Rand Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 12 Then 'Rand Links Mitte
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 14 Then 'Rand Rechts Mitte
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 16 Then 'Rand Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID >= 17 Then 'Ende des Verfahrweges
                If _akt_shift < _anz_messungen Then
                    drive_to_Hz = _zentrum_Hz_neu
                    drive_to_Vz = _zentrum_Vz_neu
                    _akt_shift = _akt_shift + 1
                    _ID = -2
                    _imageID = 1
                Else
                    MoDiTaGUI.changeTachyStatusSymbol(1) 'Änderung des Status des Tachymeters
                    Dim speicherort As String
                    speicherort = _project_dir + "\image_measure\" + _
                                  _messung_name + "_" + _messzeit + "\" + _
                                  _messung_name + "_" + _messzeit + ".csv"
                    _camera_csv.KameraData_to_CSV(_camera_table, speicherort)
                    Exit Sub
                End If
            End If

        ElseIf _mode = 3 Then ' Kreuz gedreht
            If _ID = 0 Then 'Rand Oben Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID = 2 Then 'Rand Oben Mitte Links
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 4 Then 'Zentrum 
                drive_to_Hz = _zentrum_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu
            ElseIf _ID = 6 Then 'Rand Unten Mitte Rechts
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 8 Then 'Rand Links
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 10 Then 'Rand Unten Links
                drive_to_Hz = _zentrum_Hz_neu - _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu + _delta_Vz_neu
            ElseIf _ID = 12 Then 'Rand Unten Mitte Links
                drive_to_Hz = _zentrum_Hz_neu - (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu + (_delta_Vz_neu / 2)
            ElseIf _ID = 14 Then 'Rand Oben Mitte Rechts
                drive_to_Hz = _zentrum_Hz_neu + (_delta_Hz_neu / 2)
                drive_to_Vz = _zentrum_Vz_neu - (_delta_Vz_neu / 2)
            ElseIf _ID = 16 Then 'Rand Rechts
                drive_to_Hz = _zentrum_Hz_neu + _delta_Hz_neu
                drive_to_Vz = _zentrum_Vz_neu - _delta_Vz_neu
            ElseIf _ID >= 17 Then 'Ende des Verfahrweges
                If _akt_shift < _anz_messungen Then
                    drive_to_Hz = _zentrum_Hz_neu
                    drive_to_Vz = _zentrum_Vz_neu
                    _akt_shift = _akt_shift + 1
                    _ID = -2
                    _imageID = 1
                Else
                    MoDiTaGUI.changeTachyStatusSymbol(1) 'Änderung des Status des Tachymeters
                    Dim speicherort As String
                    speicherort = _project_dir + "\image_measure\" + _
                                  _messung_name + "_" + _messzeit + "\" + _
                                  _messung_name + "_" + _messzeit + ".csv"
                    _camera_csv.KameraData_to_CSV(_camera_table, speicherort)
                    Exit Sub
                End If
            End If
        End If

        'Verfahre an Position | Precice Mode OFF
        Tachymeter_Drive(drive_to_Hz, drive_to_Vz)

    End Sub

    ''' <summary>
    ''' Steuert Tachymeter zu gewünschter Position
    ''' </summary>
    ''' <param name="Hz">Horizontalrichtung</param>
    ''' <param name="Vz">Vertikalwinkel</param>
    ''' <remarks></remarks>
    Private Sub Tachymeter_Drive(ByVal Hz As Double, ByVal Vz As Double)

        'Handler für Event hinzufügen | Verweis auf Snapshot legen
        AddHandler _tachymetercontrol.move_Absolute_HzV_ready, AddressOf Snapshot

        'Verfahre an Position | Precice Mode OFF
        _tachymetercontrol.move_Absolute_HzV(Hz, Vz, 0)

        'Nächsten Schritt anzeigen
        _ID = _ID + 1

    End Sub

    ''' <summary>
    ''' Erzeugt Snapshot
    ''' </summary>
    ''' <param name="data">data Objekt</param>
    ''' <remarks></remarks>
    Private Sub Snapshot(ByRef data As LeicaTPS1000data)

        'Handler für Event entfernen | Verweis auf Snapshot entfernen
        RemoveHandler _tachymetercontrol.move_Absolute_HzV_ready, AddressOf Snapshot

        If _ID <> -1 Then
            'Speicherverzeichnis festlegen 
            _filename = _project_dir + "\image_measure\" + _
                        _messung_name + "_" + _messzeit + "\" + _
                        _messung_name + "_" + _messzeit + "_" + "shift" + _
                        _akt_shift.ToString + "_" + _imageID.ToString + ".bmp"

            'Nur Name des Bildes ohne Pfad
            Dim imagename = _messung_name + "_" + _messzeit + "_" + "shift" + _
                            _akt_shift.ToString + "_" + _imageID.ToString + ".bmp"

            'Warten bis Kamera bereit ist
            System.Threading.Thread.Sleep(_wartezeit)

            'Speichern des Bilds mit individuellem Namen 
            _camera_uEye.save_Image(_filename)

            'Kameradaten abfragen
            Dim et As Double
            Dim fr As Double
            Dim pc As Long
            Dim mg As Long
            Dim bl As Long
            et = _camera_uEye.camera_ExposureTime
            fr = _camera_uEye.camera_FrameRate
            pc = _camera_uEye.camera_PixelClock
            mg = _camera_uEye.camera_MasterGain
            bl = _camera_uEye.camera_BlackLevel

            'Daten zu Datatable hinzufügen
            _camera_table = _camera_csv.AddRow(_camera_table, imagename, et, fr, pc, mg, bl)

            'ImageID erhöhen
            _imageID = _imageID + 1
        End If

        'Nächsten Schritt anzeigen
        _ID = _ID + 1

        'Nächster Schritt: Winkel suchen
        Call Detect_Angle()

    End Sub

    Public Sub Do_Snapshot(ByRef camera_uEye As uEyeCamera, _
                            ByVal project_dir As String, _
                            ByVal messung_name As String, _
                            ByVal messzeit As String)

        'Directory erzeugen
        Directory.CreateDirectory(project_dir + "\image_measure\" + _
                                  messung_name + "_" + messzeit)

        'Projektverzeichnis festlegen 
        Dim filename As String
        filename = project_dir + "\image_measure\" + _
                   messung_name + "_" + messzeit + "\" + _
                   messung_name + "_" + messzeit + ".bmp"

        'Nur Name des Bildes ohne Pfad
        Dim imagename As String
        imagename = messung_name + "_" + messzeit + ".bmp"

        'Warten bis Kamera bereit ist
        System.Threading.Thread.Sleep(2000)

        'Speichern des Bilds mit individuellem Namen 
        camera_uEye.save_Image(filename)

        'Kameradaten abfragen
        Dim et As Double
        Dim fr As Double
        Dim pc As Long
        Dim mg As Long
        Dim bl As Long
        et = camera_uEye.camera_ExposureTime
        fr = camera_uEye.camera_FrameRate
        pc = camera_uEye.camera_PixelClock
        mg = camera_uEye.camera_MasterGain
        bl = camera_uEye.camera_BlackLevel

        'Daten zu Datatable hinzufügen
        Dim cameracsv As New camera_csv
        Dim camera_table
        camera_table = cameracsv.CreateTable()
        camera_table = cameracsv.AddRow(camera_table, imagename, et, fr, pc, mg, bl)

        Dim speicherort As String
        speicherort = project_dir + "\image_measure\" + _
                      messung_name + "_" + messzeit + "\" + _
                      messung_name + "_" + messzeit + ".csv"
        cameracsv.KameraData_to_CSV(camera_table, speicherort)

    End Sub

End Class
