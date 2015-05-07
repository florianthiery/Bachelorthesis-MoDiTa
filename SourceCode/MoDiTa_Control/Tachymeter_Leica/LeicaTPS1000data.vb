''' <summary>
''' Klasse für die Daten vom LeicaTPS1000
''' </summary>
''' <remarks></remarks>
Public Class LeicaTPS1000data
    ' Messwerte
    Private _horizontalrichtung As Double
    Private _zenitwinkel As Double
    Private _distanz As Double

    Private _measuretime As String
    Private _measuredate As String

    ' Verfahrziel
    Private _soll_horizontalrichtung As Double
    Private _soll_zenitwinkel As Double
    Private _AUT_POSMODE As Integer

    Private _TheodoliteName As String
    Private _TheodoliteNumber As Long
    Private _theoHzTime As Double
    Private _theoZdTime As Double
    Private _theoHzTol As Double
    Private _theoVzTol As Double
    Private _AngleAccuracy As Double
    Private _AngleTime As Long
    Private _CrossIncline As Double
    Private _LengthIncline As Double
    Private _AccuracyIncline As Double
    Private _InclineTime As Long
    Private _distTime As Double


    'Varibelen für die Kommunikation mit dem Gerät
    Private _codeNo As Long
    Private _codeNoString As String = ""
    Private _requestString As String = ""
    Private _reply As String = ""
    Private _tpsTrID As Short 'TransaktionsID


    'Errorcode und Verbalisierung
    Private _errorcode As Integer
    Private _errorstring As String

    'Kompensatorstatus und Verbalisierung und "Booleanisierung"
    Private _theoInclineSwitch As Long
    Private _theoInclineSwitchString As String
    Private _theoInclineSwitchBool As String

    'Fernrohrlage und Verbalisierung
    Private _FaceDef As Long
    Private _FaceDefString As String

    'Korrektionen und "Booleanisierung"
    Private _theoInclineCorr As Long
    Private _theoStandAxisCorr As Long
    Private _theoCollimationCorr As Long
    Private _theoTiltAxisCorr As Long
    Private _theoInclineCorrBool As Boolean
    Private _theoStandAxisCorrBool As Boolean
    Private _theoCollimationCorrBool As Boolean
    Private _theoTiltAxisCorrBool As Boolean

    'ATR und Lock Parameter
    Private _ATR_status As Integer
    Private _ATR_status_string As String
    Private _LOCK_status As Integer
    Private _LOCK_status_string As String

    Private _FineAdjustMode As Integer
    Private _FineAdjustMode_string As String




    Public yc, xc As Double



    Public Sub New()

    End Sub

    Public Property LOCKMode_Status() As Integer
        Get
            Return Me._LOCK_status
        End Get
        Set(ByVal value As Integer)
            Me._LOCK_status = value

            Select Case (value)
                Case 0
                    Me._LOCK_status_string = "Off"
                Case 1
                    Me._LOCK_status_string = "On"
            End Select
        End Set
    End Property

    Public ReadOnly Property LOCKMode_Status_string() As String
        Get
            Return Me._LOCK_status_string
        End Get
    End Property

    Public Property FineAdjustMode() As Integer
        Get
            Return Me._FineAdjustMode
        End Get
        Set(ByVal value As Integer)
            Me._FineAdjustMode = value

            Select Case (value)
                Case 0
                    Me._FineAdjustMode_string = "AUT_NORM_MODE"
                Case 1
                    Me._FineAdjustMode_string = "AUT_POINT_MODE"
                Case 2
                    Me._FineAdjustMode_string = "AUT_DEFINE_MODE"
            End Select
        End Set
    End Property

    Public ReadOnly Property FineAdjustMode_string() As String
        Get
            Return Me._FineAdjustMode_string
        End Get
    End Property


    Public Property ATR_Status() As Integer
        Get
            Return Me._ATR_status
        End Get
        Set(ByVal value As Integer)
            Me._ATR_status = value

            Select Case (value)
                Case 0
                    Me._ATR_status_string = "Off"
                Case 1
                    Me._ATR_status_string = "On"
            End Select
        End Set
    End Property

    Public ReadOnly Property ATR_Status_String() As String
        Get
            Return Me._ATR_status_string
        End Get
    End Property

    Public Property measuretime() As String
        Get
            Return Me._measuretime
        End Get
        Set(ByVal value As String)
            Me._measuretime = value
        End Set
    End Property

    Public Property mesuresdate() As String
        Get
            Return Me._measuredate
        End Get
        Set(ByVal value As String)
            Me._measuredate = value
        End Set
    End Property



    ''' <summary>
    ''' Name des Theodolits
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property TheodoliteName() As String
        Get
            Return Me._TheodoliteName
        End Get
        Set(ByVal value As String)
            Me._TheodoliteName = value
        End Set
    End Property
    ''' <summary>
    ''' Nummer des Theodolits
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property TheodoliteNumber() As Long
        Get
            Return Me._TheodoliteNumber
        End Get
        Set(ByVal value As Long)
            Me._TheodoliteNumber = value
        End Set
    End Property
    ''' <summary>
    ''' HzTime
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property theoHzTime() As Double
        Get
            Return Me._theoHzTime
        End Get
        Set(ByVal value As Double)
            Me._theoHzTime = value
        End Set
    End Property
    ''' <summary>
    ''' ZdTime
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property theoZdTime() As Double
        Get
            Return Me._theoZdTime
        End Get
        Set(ByVal value As Double)
            Me._theoZdTime = value
        End Set
    End Property
    ''' <summary>
    ''' HzTol
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property theoHzTol() As Double
        Get
            Return Me._theoHzTol
        End Get
        Set(ByVal value As Double)
            Me._theoHzTol = value
        End Set
    End Property
    ''' <summary>
    ''' VzTol
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property theoVzTol() As Double
        Get
            Return Me._theoVzTol
        End Get
        Set(ByVal value As Double)
            Me._theoVzTol = value
        End Set
    End Property
    ''' <summary>
    ''' AngleAccuracy
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property AngleAccuracy() As Double
        Get
            Return Me._AngleAccuracy
        End Get
        Set(ByVal value As Double)
            Me._AngleAccuracy = value
        End Set
    End Property
    ''' <summary>
    ''' AngleTime
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property AngleTime() As Long
        Get
            Return Me._AngleTime
        End Get
        Set(ByVal value As Long)
            Me._AngleTime = value
        End Set
    End Property
    ''' <summary>
    ''' CrossIncline
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property CrossIncline() As Double
        Get
            Return Me._CrossIncline
        End Get
        Set(ByVal value As Double)
            Me._CrossIncline = value
        End Set
    End Property
    ''' <summary>
    ''' LengthIncline
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property LengthIncline() As Double
        Get
            Return Me._LengthIncline
        End Get
        Set(ByVal value As Double)
            Me._LengthIncline = value
        End Set
    End Property
    ''' <summary>
    ''' AccuracyIncline
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property AccuracyIncline() As Double
        Get
            Return Me._AccuracyIncline
        End Get
        Set(ByVal value As Double)
            Me._AccuracyIncline = value
        End Set
    End Property
    ''' <summary>
    ''' InclineTime
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property InclineTime() As Long
        Get
            Return Me._InclineTime
        End Get
        Set(ByVal value As Long)
            Me._InclineTime = value
        End Set
    End Property

    ''' <summary>
    ''' Time of the distance measurement [ms].
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property distTime() As Double
        Get
            Return Me._distTime
        End Get
        Set(ByVal value As Double)
            Me._distTime = value
        End Set
    End Property

    ''' <summary>
    ''' Horizontalrichtung [gon]
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property horizontalrichtung() As Double
        Get
            Return Me._horizontalrichtung
        End Get
        Set(ByVal value As Double)
            Me._horizontalrichtung = value
        End Set
    End Property
    ''' <summary>
    ''' Zenitwinkel [gon]
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property zenitwinkel() As Double
        Get
            Return Me._zenitwinkel
        End Get
        Set(ByVal value As Double)
            Me._zenitwinkel = value
        End Set
    End Property
    ''' <summary>
    ''' Distanz [m]
    ''' </summary>
    ''' <value>Double</value>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Property distanz() As Double
        Get
            Return Me._distanz
        End Get
        Set(ByVal value As Double)
            Me._distanz = value
        End Set
    End Property
    ''' <summary>
    ''' Soll-Hz-Richtung für das Verfahren [gon]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property soll_horizontalrichtung() As Double
        Get
            Return Me._soll_horizontalrichtung
        End Get
        Set(ByVal value As Double)
            Me._soll_horizontalrichtung = value
        End Set
    End Property
    ''' <summary>
    ''' Soll-Zenitwinkel für das Verfahren [gon]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property soll_zenitwinkel() As Double
        Get
            Return Me._soll_zenitwinkel
        End Get
        Set(ByVal value As Double)
            Me._soll_zenitwinkel = value
        End Set
    End Property
    ''' <summary>
    ''' Positionierungsmode: 0 - fast positioning mode (Default), 1 - exact positioning mode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property posmode() As Integer
        Get
            Return Me._AUT_POSMODE
        End Get
        Set(ByVal value As Integer)
            Me._AUT_POSMODE = value
        End Set
    End Property

    'Booleanisierung Korrektionen
    ''' <summary>
    ''' theoInclineCorr
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property theoInclineCorr() As Long
        Get
            Return Me._theoInclineCorr
        End Get
        Set(ByVal value As Long)
            Me._theoInclineCorr = value
            Select Case Me._theoInclineCorr
                Case 0
                    Me._theoInclineCorrBool = False
                Case 1
                    Me._theoInclineCorrBool = True
            End Select
        End Set
    End Property
    ''' <summary>
    ''' theoInclineCorr, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property theoInclineCorrBool() As Boolean
        Get
            Return Me._theoInclineCorrBool
        End Get
    End Property
    ''' <summary>
    ''' theoStandAxisCorr
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property theoStandAxisCorr() As Long
        Get
            Return Me._theoStandAxisCorr
        End Get
        Set(ByVal value As Long)
            Me._theoStandAxisCorr = value
            Select Case Me._theoStandAxisCorr
                Case 0
                    Me._theoStandAxisCorrBool = False
                Case 1
                    Me._theoStandAxisCorrBool = True
            End Select
        End Set
    End Property
    ''' <summary>
    ''' theoStandAxisCorrBool, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property theoStandAxisCorrBool() As Boolean
        Get
            Return Me._theoStandAxisCorrBool
        End Get
    End Property
    ''' <summary>
    ''' theoCollimationCorr
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property theoCollimationCorr() As Long
        Get
            Return Me._theoCollimationCorr
        End Get
        Set(ByVal value As Long)
            Me._theoCollimationCorr = value
            Select Case Me._theoCollimationCorr
                Case 0
                    Me._theoCollimationCorrBool = False
                Case 1
                    Me._theoCollimationCorrBool = True
            End Select
        End Set
    End Property
    ''' <summary>
    ''' theoStandAxisCorrBool, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property theoCollimationCorrBool() As Boolean
        Get
            Return Me._theoCollimationCorrBool
        End Get
    End Property
    ''' <summary>
    ''' theoTiltAxisCorr
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property theoTiltAxisCorr() As Long
        Get
            Return Me._theoTiltAxisCorr
        End Get
        Set(ByVal value As Long)
            Me._theoTiltAxisCorr = value
            Select Case Me._theoTiltAxisCorr
                Case 0
                    Me._theoTiltAxisCorrBool = False
                Case 1
                    Me._theoTiltAxisCorrBool = True
            End Select
        End Set
    End Property
    Public ReadOnly Property theoTiltAxisCorrBool() As Boolean
        Get
            Return Me._theoTiltAxisCorrBool
        End Get
    End Property

    'Verbalisierung Fernrohrlage
    ''' <summary>
    ''' FaceDef
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property FaceDef() As Long
        Get
            Return Me._FaceDef
        End Get
        Set(ByVal value As Long)
            Me._FaceDef = value
            Select Case Me._FaceDef
                Case 0
                    Me._FaceDefString = "1. Lage"
                Case 1
                    Me._FaceDefString = "2. Lage"
            End Select
        End Set
    End Property
    ''' <summary>
    ''' Fernrohrlage, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FaceDefString() As String
        Get
            Return Me._FaceDefString
        End Get
    End Property

    'Verbalisierung Kompensatorstatus
    ''' <summary>
    ''' InclineSwitch
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property theoInclineSwitch() As Long
        Get
            Return Me._theoInclineSwitch
        End Get
        Set(ByVal value As Long)
            Me._theoInclineSwitch = value
            Select Case Me._theoInclineSwitch
                Case 0
                    Me._theoInclineSwitchString = "Kompensator ist OFF"
                    Me._theoInclineSwitchBool = True
                Case 1
                    Me._theoInclineSwitchString = "Kompensator ist ON"
                    Me._theoInclineSwitchBool = False
            End Select
        End Set
    End Property
    ''' <summary>
    ''' Kompensatorstatus, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property theoInclineSwitchString() As String
        Get
            Return Me._theoInclineSwitchString
        End Get
    End Property

    ''' <summary>
    ''' Kompensatorstatus, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property theoInclineSwitchBool() As String
        Get
            Return Me._theoInclineSwitchBool
        End Get
    End Property

    'Mehr Informationen
    ''' <summary>
    ''' CodeNo
    ''' </summary>
    ''' <value>Long</value>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Property CodeNo() As Long
        Get
            Return Me._codeNo
        End Get
        Set(ByVal value As Long)
            Me._codeNo = value
            Select Case Me._codeNo
                Case 2003
                    Me._codeNoString = "Returns complete angle measurement"
                Case 2006
                    Me._codeNoString = "Switch dual axis compensator ON/OFF"
                Case 2007
                    Me._codeNoString = "Get the dual axis compensator's state"
                Case 2107
                    Me._codeNoString = "Returns simple angle measurement"
                Case 9027
                    Me._codeNoString = "Turns telescope to specified position"
                Case 9008
                    Me._codeNoString = "Read current setting for the positioning tolerances"
                Case 9007
                    Me._codeNoString = "Set the positioning tolerances"
                Case 9012
                    Me._codeNoString = "Read current timeout setting for positioning"
                Case 9011
                    Me._codeNoString = "Set timeout for positioning"
                Case 2014
                    Me._codeNoString = "Get angular correction's states"
                Case 2016
                    Me._codeNoString = "Enable/disable angle corrections"
                Case 11004
                    Me._codeNoString = "Output of an alarm-signal"
                Case 2026
                    Me._codeNoString = "Get face information of current telescope position"
            End Select
        End Set
    End Property
    ''' <summary>
    ''' Code, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CodeNoString() As String
        Get
            Return Me._codeNoString
        End Get
    End Property

    Public Property requestString() As String
        Get
            Return Me._requestString
        End Get
        Set(ByVal value As String)
            Me._requestString = value
        End Set
    End Property
    ''' <summary>
    ''' TransaktionsID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tpsTrID() As Long
        Get
            Return Me._tpsTrID
        End Get
        Set(ByVal value As Long)
            Me._tpsTrID = value
        End Set
    End Property

    Public Property reply() As String
        Get
            Return Me._reply
        End Get
        Set(ByVal value As String)
            Me._reply = value
        End Set
    End Property

    'Verbalisierung Errorcode
    ''' <summary>
    ''' Errorcode, erzeugt aus dem Code den Errorstring
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Integer</returns>
    ''' <remarks></remarks>
    Public Property errorcode() As Integer
        Get
            Return Me._errorcode
        End Get
        Set(ByVal value As Integer)
            Me._errorcode = value
            Me._errorstring = errorcodes_to_strings(Me._errorcode)
        End Set
    End Property
    ''' <summary>
    ''' Errorstring, String entsprechend dem Code.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property errorstring() As String
        Get
            Return Me._errorstring
        End Get
    End Property

    Private Function errorcodes_to_strings(ByVal code As Integer) As String
        Select Case code
            ' Errorcodes Commport (eigene):
            Case -10
                Return "Kein Comport [1-16] verfügbar!" & _
                  " Microsoft Excel schließen. Einstellungen für COM Anschlüsse im Windows-Gerätemanager prüfen."
            Case -12
                Return "Commport liess sich wider Erwarten nicht öffnen!"
            Case -20
                Return "Excel-Formular voll!" _
                & " Laut Spalte A sind die Zeilen 3 bis 65536 mit Daten belegt." _
                & " Programm schliessen und Tabelle bereinigen!"
            Case -30
                Return "Kein Comport [1-16] verfügbar! Microsoft Excel schließen. Datenkabel und Stromversorgung Instrument überprüfen."
            Case -40
                Return "Fehler beim Datentransfer - fehlerhafte Antwort vom Instrument!"
            Case -42
                Return "Fehler beim Datentransfer - unerwartete Antwort vom Instrument!"
            Case -43
                Return "Fehler beim Datentransfer - Transaction ID stimmt nicht überein!"
            Case -44
                Return "Fehler beim Datentransfer - unbekannter Request"
            Case -45
                Return "Comport nicht empfangsbereit oder beschäftigt"
            Case -50
                Return "Keine Antwort vom Theodolit im vorgesehenen Zeitfenster!"
            Case -60
                Return "Theodolit aus, keine Messung!"
            Case (-70)
                Return "Außerhalb des definierten Verfahrbereich!"
            Case -99
                Return "Unerwarteter Fehler!"
            Case -11
                Return "Angegebner Code nicht verfügbar!"
                'Case -888
                'Return "Signalstärke nur bei IR verfügbar!"

                ' Errorcodes Leica TPS1100:
            Case 0
                Return "RC_OK - Function successfully completed."
            Case 1
                Return "RC_UNDEFINED - Unknown error, result unspecified."
            Case 2
                Return "RC_IVPARAM - Invalid parameter detected. Result unspecified."
            Case 3
                Return "RC_IVRESULT - Invalid result."
            Case 4
                Return "RC_FATAL - Fatal error."
            Case 5
                Return "RC_NOT_IMPL - Not implemented yet."
            Case 6
                Return "RC_TIME_OUT - Function execution timed out. Result unspecified."
            Case 7
                Return "RC_SET_INCOMPL - Parameter setup for subsystem is incomplete."
            Case 8
                Return "RC_ABORT - Function execution has been aborted."
            Case 9
                Return "RC_NOMEMORY - Fatal error - not enough memory."
            Case 10
                Return "RC_NOTINIT - Fatal error - subsystem not initialized."
            Case 12
                Return "RC_SHUT_DOWN - Subsystem is down."
            Case 13
                Return "RC_SYSBUSY - System busy/already in use of another process. Cannot execute function."
            Case 14
                Return "RC_HWFAILURE - Fatal error - hardware failure."
            Case 15
                Return "RC_ABORT_APPL - Execution of application has been aborted (SHIFT-ESC)."
            Case 16
                Return "RC_LOW_POWER - Operation aborted - insufficient power supply level."
            Case 17
                Return "RC_IVVERSION - Invalid version of file, ..."
            Case 18
                Return "RC_BATT_EMPTY - Battery empty"
            Case 20
                Return "RC_NO_EVENT - no event pending."
            Case 21
                Return "RC_OUT_OF_TEMP - out of temperature range"
            Case 22
                Return "RC_INSTRUMENT_TILT - instrument tilting out of range"
            Case 23
                Return "RC_COM_SETTING - communication error"
            Case 24
                Return "RC_NO_ACTION - RC_TYPE Input 'do no action'"
            Case 25
                Return "RC_SLEEP_MODE - Instr. run into the sleep mode"

            Case 257
                Return "ANG_ERROR - Angles and Inclinations not valid"
            Case 258
                Return "ANG_INCL_ERROR - inclinations not valid"
            Case 259
                Return "ANG_BAD_ACC - value accuracy not reached"
            Case 260
                Return "ANG_BAD_ANGLE_ACC - angle-accuracy not reached"
            Case 261
                Return "ANG_BAD_INCLIN_ACC - inclination accuracy not reached"
            Case 266
                Return "ANG_WRITE_PROTECTED - no write access allowed"
            Case 267
                Return "ANG_OUT_OF_RANGE - value out of range"
            Case 268
                Return "ANG_IR_OCCURED - function aborted due to interrupt"
            Case 269
                Return "ANG_HZ_MOVED - hz moved during incline measurement"
            Case 270
                Return "ANG_OS_ERROR - troubles with operation system"
            Case 271
                Return "ANG_DATA_ERROR - overflow at parameter values"
            Case 272
                Return "ANG_PEAK_CNT_UFL - too less peaks"
            Case 273
                Return "ANG_TIME_OUT - reading timeout"
            Case 274
                Return "ANG_TOO_MANY_EXPOS - too many exposures wanted"
            Case 275
                Return "ANG_PIX_CTRL_ERR - picture height out of range"
            Case 276
                Return "ANG_MAX_POS_SKIP - positive exposure dynamic overflow"
            Case 277
                Return "ANG_MAX_NEG_SKIP - negative exposure dynamic overflow"
            Case 278
                Return "ANG_EXP_LIMIT - exposure time overflow"
            Case 279
                Return "ANG_UNDER_EXPOSURE - picture under-exposured"
            Case 280
                Return "ANG_OVER_EXPOSURE - picture over-exposured"
            Case 300
                Return "ANG_TMANY_PEAKS - too many peaks detected"
            Case 301
                Return "ANG_TLESS_PEAKS - too less peaks detected"
            Case 302
                Return "ANG_PEAK_TOO_SLIM - peak too slim"
            Case 303
                Return "ANG_PEAK_TOO_WIDE - peak to wide"
            Case 304
                Return "ANG_BAD_PEAKDIFF - bad peak difference"
            Case 305
                Return "ANG_UNDER_EXP_PICT - too less peak amplitude"
            Case 306
                Return "ANG_PEAKS_INHOMOGEN - in-homogenous peak amplitudes"
            Case 307
                Return "ANG_NO_DECOD_POSS - no peak decoding possible"
            Case 308
                Return "ANG_UNSTABLE_DECOD - peak decoding not stable"
            Case 309
                Return "ANG_TLESS_FPEAKS - too less valid fine-peaks"

            Case 512
                Return "ATA_RC_NOT_READY - ATR-System is not ready."
            Case 513
                Return "ATA_RC_NO_RESULT - Result isn't available yet."
            Case 514
                Return "ATA_RC_SEVERAL_TARGETS - Several Targets detected."
            Case 515
                Return "ATA_RC_BIG_SPOT - Spot is too big for analyze."
            Case 516
                Return "ATA_RC_BACKGROUND - Background is too bright."
            Case 517
                Return "ATA_RC_NO_TARGETS - No targets detected."
            Case 518
                Return "ATA_RC_NOT_ACCURAT - Accuracy worse than asked for."
            Case 519
                Return "ATA_RC_SPOT_ON_EDGE - Spot is on the edge of the sensing area."
            Case 522
                Return "ATA_RC_BLOOMING - Blooming or spot on edge detected."
            Case 523
                Return "ATA_RC_NOT_BUSY - ATR isn't in a continuous mode."
            Case 524
                Return "ATA_RC_STRANGE_LIGHT - Not the spot of the own target illuminator."
            Case 525
                Return "ATA_RC_V24_FAIL - Communication error to sensor (ATR)."
            Case 527
                Return "ATA_RC_HZ_FAIL - No Spot detected in Hz-direction."
            Case 528
                Return "ATA_RC_V_FAIL - No Spot detected in V-direction."
            Case 529
                Return "ATA_RC_HZ_STRANGE_L - Strange light in Hz-direction."
            Case 530
                Return "ATA_RC_V_STRANGE_L - Strange light in V-direction."
            Case 531
                Return "ATA_SLDR_TRANSFER_PENDING - On multiple ATA_SLDR_OpenTransfer."
            Case 532
                Return "ATA_SLDR_TRANSFER_ILLEGAL - No ATA_SLDR_OpenTransfer happened."
            Case 533
                Return "ATA_SLDR_DATA_ERROR - Unexpected data format received."
            Case 534
                Return "ATA_SLDR_CHK_SUM_ERROR - Checksum error in transmitted data."
            Case 535
                Return "ATA_SLDR_ADDRESS_ERROR - Address out of valid range."
            Case 536
                Return "ATA_SLDR_INV_LOADFILE - Firmware file has invalid format."
            Case 537
                Return "ATA_SLDR_UNSUPPORTED - Current (loaded) Firmware doesn't support upload."
            Case 538
                Return "ATA_PS_RC_NOT_READY - PS-System is not ready."

            Case 769
                Return "EDM_SYSTEM_ERR - Fatal EDM sensor error. See for the exact reason the original EDM sensor error number. In the most cases a service problem."
            Case 770
                Return "EDM_INVALID_COMMAND - Invalid command or unknown command, see command syntax."
            Case 771
                Return "EDM_BOOM_ERR - Boomerang error."
            Case 772
                Return "EDM_SIGN_LOW_ERR - Received signal to low, prism to far away, or natural barrier, bad environment, etc."
            Case 773
                Return "EDM_DIL_ERR - DIL distance measurement out of limit."
            Case 774
                Return "EDM_SIGN_HIGH_ERR - Received signal to strong, prism to near, stranger light effect."
            Case 778
                Return "EDM_DEV_NOT_INSTALLED - Device like EGL, DL is not installed."
            Case 779
                Return "EDM_NOT_FOUND - Search result invalid. For the exact explanation see in the description of the called function."
            Case 780
                Return "EDM_ERROR_RECEIVED - Communication ok, but an error reported from the EDM sensor."
            Case 781
                Return "EDM_MISSING_SRVPWD - No service password is set."
            Case 782
                Return "EDM_INVALID_ANSWER - Communication ok, but an unexpected answer received."
            Case 783
                Return "EDM_SEND_ERR - Data send error, sending buffer is full."
            Case 784
                Return "EDM_RECEIVE_ERR - Data receive error, like parity buffer overflow."
            Case 785
                Return "EDM_INTERNAL_ERR - Internal EDM subsystem error."
            Case 786
                Return "EDM_BUSY - Sensor is working already, abort current measuring first."
            Case 787
                Return "EDM_NO_MEASACTIVITY - No measurement activity started."
            Case 788
                Return "EDM_CHKSUM_ERR - Calculated checksum, resp. received data wrong (only in binary communication mode possible)."
            Case 789
                Return "EDM_INIT_OR_STOP_ERR - During start up or shut down phase an error occured. It is saved in the DEL buffer."
            Case 790
                Return "EDM_SRL_NOT_AVAILABLE - Red laser not available on this sensor HW."
            Case 791
                Return "EDM_MEAS_ABORTED - Measurement will be aborted (will be used for the lasersecurity)"
            Case 798
                Return "EDM_SLDR_TRANSFER_PENDING - Multiple OpenTransfer calls."
            Case 799
                Return "EDM_SLDR_TRANSFER_ILLEGAL - No opentransfer happened."
            Case 800
                Return "EDM_SLDR_DATA_ERROR - Unexpected data format received."
            Case 801
                Return "EDM_SLDR_CHK_SUM_ERROR - Checksum error in transmitted data."
            Case 802
                Return "EDM_SLDR_ADDR_ERROR - Address out of valid range."
            Case 803
                Return "EDM_SLDR_INV_LOADFILE - Firmware file has invalid format."
            Case 804
                Return "EDM_SLDR_UNSUPPORTED - Current (loaded) firmware doesn't support upload."
            Case 808
                Return "EDM_UNKNOW_ERR - Undocumented error from the EDM sensor, should not occur."

            Case 1025
                Return "GM_WRONG_AREA_DEF - Wrong Area Definition."
            Case 1026
                Return "GM_IDENTICAL_PTS - Identical Points."
            Case 1027
                Return "GM_PTS_IN_LINE - Points on one line."
            Case 1028
                Return "GM_OUT_OF_RANGE - Out of range."
            Case 1029
                Return "GM_PLAUSIBILITY_ERR - Plausibility error."
            Case 1030
                Return "GM_TOO_FEW_OBSERVATIONS - To few Observations to calculate the average."
            Case 1031
                Return "GM_NO_SOLUTION - No Solution."
            Case 1032
                Return "GM_ONE_SOLUTION - Only one solution."
            Case 1033
                Return "GM_TWO_SOLUTIONS - Second solution."
            Case 1034
                Return "GM_ANGLE_SMALLER_15GON - Warning: Intersection angle < 15gon."
            Case 1035
                Return "GM_INVALID_TRIANGLE_TYPE - Invalid triangle."
            Case 1036
                Return "GM_INVALID_ANGLE_SYSTEM - Invalid angle unit."
            Case 1037
                Return "GM_INVALID_DIST_SYSTEM - Invalid distance unit."
            Case 1038
                Return "GM_INVALID_V_SYSTEM - Invalid vertical angle."
            Case 1039
                Return "GM_INVALID_TEMP_SYSTEM - Invalid temperature system."
            Case 1040
                Return "GM_INVALID_PRES_SYSTEM - Invalid pressure unit."
            Case 1041
                Return "GM_RADIUS_NOT_POSSIBLE - Invalid radius."
            Case 1042
                Return "GM_NO_PROVISIONAL_VALUES - GM2: insufficient data."
            Case 1043
                Return "GM_SINGULAR_MATRIX - GM2: bad data"
            Case 1044
                Return "GM_TOO_MANY_ITERATIONS - GM2: bad data distr."
            Case 1045
                Return "GM_IDENTICAL_TIE_POINTS - GM2: same tie points."
            Case 1046
                Return "GM_SETUP_EQUALS_TIE_POINT - GM2: sta/tie point same."

            Case 1283
                Return "TMC_NO_FULL_CORRECTION - Warning: measurement without full correction"
            Case 1284
                Return "TMC_ACCURACY_GUARANTEE - Info : accuracy can not be guaranteed"
            Case 1285
                Return "TMC_ANGLE_OK - Warning: only angle measurement valid"
            Case 1288
                Return "TMC_ANGLE_NO_FULL_CORRECTION - Warning: only angle measurement valid but without full correction"
            Case 1289
                Return "TMC_ANGLE_ACCURACY_GUARANTEE - Info : only angle measurement valid but accuracy can not be guarantee"
            Case 1290
                Return "TMC_ANGLE_ERROR - Error : no angle measurement"
            Case 1291
                Return "TMC_DIST_PPM - Error : wrong setting of PPM or MM on EDM"
            Case 1292
                Return "TMC_DIST_ERROR - Error : distance measurement not done (no aim, etc.)"
            Case 1293
                Return "TMC_BUSY - Error : system is busy (no measurement done)"
            Case 1294
                Return "TMC_SIGNAL_ERROR - Error : no signal on EDM (only in signal mode)"

            Case 1536
                Return "MEM_OUT_OF_MEMORY - out of memory"
            Case 1537
                Return "MEM_OUT_OF_HANDLES - out of memory handles"
            Case 1538
                Return "MEM_TAB_OVERFLOW - memory table overflow"
            Case 1539
                Return "MEM_HANDLE_INVALID - used handle is invalid"
            Case 1540
                Return "MEM_DATA_NOT_FOUND - memory data not found"
            Case 1541
                Return "MEM_DELETE_ERROR - memory delete error"
            Case 1542
                Return "MEM_ZERO_ALLOC_ERR - tried to allocate 0 bytes"
            Case 1543
                Return "MEM_REORG_ERR - can't reorganize memory"

            Case 1792
                Return "MOT_RC_UNREADY - Motorization not ready"
            Case 1793
                Return "MOT_RC_BUSY - Motorization is handling another task"
            Case 1794
                Return "MOT_RC_NOT_OCONST - Not in velocity mode"
            Case 1795
                Return "MOT_RC_NOT_CONFIG - Motorization is in the wrong mode or busy"
            Case 1796
                Return "MOT_RC_NOT_POSIT - Not in posit mode"
            Case 1797
                Return "MOT_RC_NOT_SERVICE - Not in service mode"
            Case 1798
                Return "MOT_RC_NOT_BUSY - Motorization is handling no task"
            Case 1799
                Return "MOT_RC_NOT_LOCK - Not in tracking mode"
            Case 1800
                Return "MOT_RC_NOT_SPIRAL - Not in spiral mode"

            Case 2048
                Return "LDR_PENDING - Transfer is already open"
            Case 2049
                Return "LDR_PRGM_OCC - Maximal number of applications reached"
            Case 2050
                Return "LDR_TRANSFER_ILLEGAL - No Transfer is open"
            Case 2051
                Return "LDR_NOT_FOUND - Function or program not found"
            Case 2052
                Return "LDR_ALREADY_EXIST - Loadable object already exists"
            Case 2053
                Return "LDR_NOT_EXIST - Can't delete. Object does not exist"
            Case 2054
                Return "LDR_SIZE_ERROR - Error in loading object"
            Case 2055
                Return "LDR_MEM_ERROR - Error at memory allocation/release"
            Case 2056
                Return "LDR_PRGM_NOT_EXIST - Can't load text-object because application does not exist"
            Case 2057
                Return "LDR_FUNC_LEVEL_ERR - Call-stack limit reached"
            Case 2058
                Return "LDR_RECURSIV_ERR - Recursive calling of an loaded function"
            Case 2059
                Return "LDR_INST_ERR - Error in installation function"
            Case 2060
                Return "LDR_FUNC_OCC - Maximal number of functions reached"
            Case 2061
                Return "LDR_RUN_ERROR - Error during a loaded application program"
            Case 2062
                Return "LDR_DEL_MENU_ERR - Error during deleting of menu entries of an application"
            Case 2063
                Return "LDR_OBJ_TYPE_ERROR - Loadable object is unknown"
            Case 2064
                Return "LDR_WRONG_SECKEY - Wrong security key"
            Case 2065
                Return "LDR_ILLEGAL_LOADADR - Illegal application memory address"
            Case 2066
                Return "LDR_IEEE_ERROR - Loadable object file is not IEEE format"
            Case 2067
                Return "LDR_WRONG_APPL_VERSION - Bad application version number"

            Case 2305
                Return "BMM_XFER_PENDING - Loading process already opened"
            Case 2306
                Return "BMM_NO_XFER_OPEN - Transfer not opened"
            Case 2307
                Return "BMM_UNKNOWN_CHARSET - Unknown character set"
            Case 2308
                Return "BMM_NOT_INSTALLED - Display module not present"
            Case 2309
                Return "BMM_ALREADY_EXIST - Character set already exists"
            Case 2310
                Return "BMM_CANT_DELETE - Character set cannot be deleted"
            Case 2311
                Return "BMM_MEM_ERROR - Memory cannot be allocated"
            Case 2312
                Return "BMM_CHARSET_USED - Character set still used"
            Case 2313
                Return "BMM_CHARSET_SAVED - Char-set cannot be deleted or is protected"
            Case 2314
                Return "BMM_INVALID_ADR - Attempt to copy a character block outside the allocated memory"
            Case 2315
                Return "BMM_CANCELANDADR_ERROR - Error during release of allocated memory"
            Case 2316
                Return "BMM_INVALID_SIZE - Number of bytes specified in header does not match the bytes read"
            Case 2317
                Return "BMM_CANCELANDINVSIZE_ERROR - Allocated memory could not be released"
            Case 2318
                Return "BMM_ALL_GROUP_OCC - Max. number of character sets already loaded"
            Case 2319
                Return "BMM_CANT_DEL_LAYERS - Layer cannot be deleted"
            Case 2320
                Return "BMM_UNKNOWN_LAYER - Required layer does not exist"
            Case 2321
                Return "BMM_INVALID_LAYERLEN - Layer length exceeds maximum"

            Case 2560
                Return "TXT_OTHER_LANG - text found, but in an other language"
            Case 2561
                Return "TXT_UNDEF_TOKEN - text not found, token is undefined"
            Case 2562
                Return "TXT_UNDEF_LANG - language is not defined"
            Case 2563
                Return "TXT_TOOMANY_LANG - maximal number of languages reached"
            Case 2564
                Return "TXT_GROUP_OCC - desired text group is already in use"
            Case 2565
                Return "TXT_INVALID_GROUP - text group is invalid"
            Case 2566
                Return "TXT_OUT_OF_MEM - out of text memory"
            Case 2567
                Return "TXT_MEM_ERROR - memory write / allocate error"
            Case 2568
                Return "TXT_TRANSFER_PENDING - text transfer is already open"
            Case 2569
                Return "TXT_TRANSFER_ILLEGAL - text transfer is not opened"
            Case 2570
                Return "TXT_INVALID_SIZE - illegal text data size"
            Case 2571
                Return "TXT_ALREADY_EXIST - language already exists"

            Case 2817
                Return "MMI_BUTTON_ID_EXISTS - Button ID already exists"
            Case 2818
                Return "MMI_DLG_NOT_OPEN - Dialog not open"
            Case 2819
                Return "MMI_DLG_OPEN - Dialog already open"
            Case 2820
                Return "MMI_DLG_SPEC_MISMATCH - Number of fields specified with OpenDialogDef does not match"
            Case 2821
                Return "MMI_DLGDEF_EMPTY - Empty dialog definition"
            Case 2822
                Return "MMI_DLGDEF_NOT_OPEN - Dialog definition not open"
            Case 2823
                Return "MMI_DLGDEF_OPEN - Dialog definition still open"
            Case 2824
                Return "MMI_FIELD_ID_EXISTS - Field ID already exists"
            Case 2825
                Return "MMI_ILLEGAL_APP_ID - Illegal application ID"
            Case 2826
                Return "MMI_ILLEGAL_BUTTON_ID - Illegal button ID"
            Case 2827
                Return "MMI_ILLEGAL_DLG_ID - Illegal dialog ID"
            Case 2828
                Return "MMI_ILLEGAL_FIELD_COORDS - Illegal field coordinates or length/height"
            Case 2829
                Return "MMI_ILLEGAL_FIELD_ID - Illegal field ID"
            Case 2830
                Return "MMI_ILLEGAL_FIELD_TYPE - Illegal field type"
            Case 2831
                Return "MMI_ILLEGAL_FIELD_FORMAT - Illegal field format"
            Case 2832
                Return "MMI_ILLEGAL_FIXLINES - Illegal number of fix dialog lines"
            Case 2833
                Return "MMI_ILLEGAL_MB_TYPE - Illegal message box type"
            Case 2834
                Return "MMI_ILLEGAL_MENU_ID - Illegal menu ID"
            Case 2835
                Return "MMI_ILLEGAL_MENUITEM_ID - Illegal menu item ID"
            Case 2836
                Return "MMI_ILLEGAL_NEXT_ID - Illegal next field ID"
            Case 2837
                Return "MMI_ILLEGAL_TOPLINE - Illegal topline number"
            Case 2838
                Return "MMI_NOMORE_BUTTONS - No more buttons per dialog/menu available"
            Case 2839
                Return "MMI_NOMORE_DLGS - No more dialogs available"
            Case 2840
                Return "MMI_NOMORE_FIELDS - No more fields per dialog available"
            Case 2841
                Return "MMI_NOMORE_MENUS - No more menus available"
            Case 2842
                Return "MMI_NOMORE_MENUITEMS - No more menu items available"
            Case 2843
                Return "MMI_NOMORE_WINDOWS - No more windows available"
            Case 2844
                Return "MMI_SYS_BUTTON - The button belongs to the MMI"
            Case 2845
                Return "MMI_VREF_UNDEF - The parameter list for OpenDialog is uninitialized"
            Case 2846
                Return "MMI_EXIT_DLG - The MMI should exit the dialog"
            Case 2847
                Return "MMI_KEEP_FOCUS - The MMI should keep focus within field being edited"
            Case 2848
                Return "MMI_NOMORE_ITEMS - Notification to the MMI that no more items available"

            Case 3072
                Return "RC_COM_ERO - Initiate Extended Runtime Operation (ERO)."
            Case 3073
                Return "RC_COM_CANT_ENCODE - Cannot encode arguments in client."
            Case 3074
                Return "RC_COM_CANT_DECODE - Cannot decode results in client."
            Case 3075
                Return "RC_COM_CANT_SEND - Hardware error while sending."
            Case 3076
                Return "RC_COM_CANT_RECV - Hardware error while receiving."
            Case 3077
                Return "RC_COM_TIMEDOUT - Request timed out."
            Case 3078
                Return "RC_COM_WRONG_FORMAT - Packet format error."
            Case 3079
                Return "RC_COM_VER_MISMATCH - Version mismatch between client and server."
            Case 3080
                Return "RC_COM_CANT_DECODE_REQ - Cannot decode arguments in server."
            Case 3081
                Return "RC_COM_PROC_UNAVAIL - Unknown RPC, procedure ID invalid."
            Case 3082
                Return "RC_COM_CANT_ENCODE_REP - Cannot encode results in server."
            Case 3083
                Return "RC_COM_SYSTEM_ERR - Unspecified generic system error."
            Case 3085
                Return "RC_COM_FAILED - Unspecified error."
            Case 3086
                Return "RC_COM_NO_BINARY - Binary protocol not available."
            Case 3087
                Return "RC_COM_INTR - Call interrupted."
            Case 3090
                Return "RC_COM_REQUIRES_8DBITS - Protocol needs 8bit encoded characters."
            Case 3093
                Return "RC_COM_TR_ID_MISMATCH - Transaction ID mismatch error."
            Case 3094
                Return "RC_COM_NOT_GEOCOM - Protocol not recognizable."
            Case 3095
                Return "RC_COM_UNKNOWN_PORT - (WIN) Invalid port address."
            Case 3099
                Return "RC_COM_ERO_END - ERO is terminating."
            Case 3100
                Return "RC_COM_OVERRUN - Internal error: data buffer overflow."
            Case 3101
                Return "RC_COM_SRVR_RX_CHECKSUM_ERROR - Invalid checksum on server side received."
            Case 3102
                Return "RC_COM_CLNT_RX_CHECKSUM_ERROR - Invalid checksum on client side received."
            Case 3103
                Return "RC_COM_PORT_NOT_AVAILABLE - (WIN) Port not available."
            Case 3104
                Return "RC_COM_PORT_NOT_OPEN - (WIN) Port not opened."
            Case 3105
                Return "RC_COM_NO_PARTNER - (WIN) Unable to find TPS."
            Case 3106
                Return "RC_COM_ERO_NOT_STARTED - Extended Runtime Operation could not be started."
            Case 3107
                Return "RC_COM_CONS_REQ - Att to send cons reqs"
            Case 3108
                Return "RC_COM_SRVR_IS_SLEEPING - TPS has gone to sleep. Wait and try again."
            Case 3109
                Return "RC_COM_SRVR_IS_OFF - TPS has shut down. Wait and try again."

            Case 3328
                Return "DPL_RC_NOCREATE - no file creation, fatal"
            Case 3329
                Return "DPL_RC_NOTOPEN - bank not open"
            Case 3330
                Return "DPL_RC_ALRDYOPEN - a databank is already open"
            Case 3331
                Return "DPL_RC_NOTFOUND - databank file does not exist"
            Case 3332
                Return "DPL_RC_EXISTS - databank already exists"
            Case 3333
                Return "DPL_RC_EMPTY - databank is empty"
            Case 3334
                Return "DPL_RC_BADATA - bad data detected"
            Case 3335
                Return "DPL_RC_BADFIELD - bad field type"
            Case 3336
                Return "DPL_RC_BADINDEX - bad index information"
            Case 3337
                Return "DPL_RC_BADKEY - bad key type"
            Case 3338
                Return "DPL_RC_BADMODE - bad mode"
            Case 3339
                Return "DPL_RC_BADRANGE - bad range"
            Case 3340
                Return "DPL_RC_DUPLICATE - duplicate keys not allowed"
            Case 3341
                Return "DPL_RC_INCOMPLETE - record is incomplete"
            Case 3342
                Return "DPL_RC_IVDBID - invalid db project id"
            Case 3343
                Return "DPL_RC_IVNAME - invalid name"
            Case 3344
                Return "DPL_RC_LOCKED - data locked"
            Case 3345
                Return "DPL_RC_NOTLOCKED - data not locked"
            Case 3346
                Return "DPL_RC_NODATA - no data found"
            Case 3347
                Return "DPL_RC_NOMATCH - no matching key found"
            Case 3348
                Return "DPL_RC_NOSPACE - no more (disk) space left"
            Case 3349
                Return "DPL_RC_NOCLOSE - could not close db (sys. error)"
            Case 3350
                Return "DPL_RC_RELATIONS - record still has relations"
            Case 3351
                Return "DPL_RC_NULLPTR - null pointer"
            Case 3352
                Return "DPL_RC_BADFORMAT - bad databank format, wrong version"
            Case 3353
                Return "DPL_RC_BADRECTYPE - bad record type"
            Case 3354
                Return "DPL_RC_OUTOFMEM - no more (memory) space left"
            Case 3355
                Return "DPL_RC_CODE_MISMATCH - code mismatch"
            Case 3356
                Return "DPL_RC_NOTINIT - db has not been initialized"
            Case 3357
                Return "DPL_RC_NOTEXIST - trf. for old db's does not exist"
            Case 4864
                Return "DPL_RC_NOTOK - not ok"
            Case 4865
                Return "DPL_RC_IVAPPL - invalid database system appl."
            Case 4866
                Return "DPL_RC_NOT_AVAILABLE - database not available"
            Case 4867
                Return "DPL_RC_NO_CODELIST - no codelist found"
            Case 4868
                Return "DPL_RC_TO_MANY_CODELISTS - more then DPL_MAX_CODELISTS found"
            Case 3840
                Return "RC_FIL_NO_ERROR - Operation completed successfully."
            Case 3845
                Return "RC_FIL_FILNAME_NOT_FOUND - File name not found."
            Case 3880
                Return "RC_FIL_NO_MAKE_DIRECTORY - Cannot create directory."
            Case 3886
                Return "RC_FIL_RENAME_FILE_FAILED - Rename of file failed."
            Case 3888
                Return "RC_FIL_INVALID_PATH - Invalid path specified."
            Case 3898
                Return "RC_FIL_FILE_NOT_DELETED - Cannot delete file."
            Case 3906
                Return "RC_FIL_ILLEGAL_ORIGIN - Illegal origin."
            Case 3924
                Return "RC_FIL_END_OF_FILE - End of file reached."
            Case 3931
                Return "RC_FIL_NO_MORE_ROOM_ON_MEDIUM - Medium full."
            Case 3932
                Return "RC_FIL_PATTERN_DOES_NOT_MATCH - Pattern does not match file names."
            Case 3948
                Return "RC_FIL_FILE_ALREADY_OPEND_FOR_WR - File is already open with write permission."
            Case 3957
                Return "RC_FIL_WRITE_TO_MEDIUM_FAILED - Write operation to medium failed."
            Case 3963
                Return "RC_FIL_START_SEARCH_NOT_CALLED - FIL_StartList not called."
            Case 3964
                Return "RC_FIL_NO_STORAGE_MEDIUM_IN_DEVICE - No medium existent in device."
            Case 3965
                Return "RC_FIL_ILLEGAL_FILE_OPEN_TYPE - Illegal file open type."
            Case 3966
                Return "RC_FIL_MEDIUM_NEWLY_INSERTED - Medium freshly inserted into device."
            Case 3967
                Return "RC_FIL_MEMORY_FAILED - Memory failure. No more memory available."
            Case 3968
                Return "RC_FIL_FATAL_ERROR - Fatal error during file operation."
            Case 3969
                Return "RC_FIL_FAT_ERROR - Fatal error in file allocation table."
            Case 3970
                Return "RC_FIL_ILLEGAL_DRIVE - Illegal drive chosen."
            Case 3971
                Return "RC_FIL_INVALID_FILE_DESCR - Illegal file descriptor."
            Case 3972
                Return "RC_FIL_SEEK_FAILED - Seek failed."
            Case 3973
                Return "RC_FIL_CANNOT_DELETE - Cannot delete file."
            Case 3974
                Return "RC_FIL_MEDIUM_WRITE_PROTECTED - Medium is write protected."
            Case 3975
                Return "RC_FIL_BATTERY_LOW - Medium backup battery is low."
            Case 3976
                Return "RC_FIL_BAD_FORMAT - Bad medium format."
            Case 3977
                Return "RC_FIL_UNSUPPORTED_MEDIUM - Unsupported PC-Card detected."
            Case 3978
                Return "RC_FIL_RENAME_DIR_FAILED - Directory exists already"

            Case 5121
                Return "WIR_PTNR_OVERFLOW - point number overflow"
            Case 5122
                Return "WIR_NUM_ASCII_CARRY - carry from number to ascii conversion"
            Case 5123
                Return "WIR_PTNR_NO_INC - can't increment point number"
            Case 5124
                Return "WIR_STEP_SIZE - wrong step size"
            Case 5125
                Return "WIR_BUSY - resource occupied"
            Case 5127
                Return "WIR_CONFIG_FNC - user function selected"
            Case 5128
                Return "WIR_CANT_OPEN_FILE - can't open file"
            Case 5129
                Return "WIR_FILE_WRITE_ERROR - can't write into file"
            Case 5130
                Return "WIR_MEDIUM_NOMEM - no anymore memory on PC-Card"
            Case 5131
                Return "WIR_NO_MEDIUM - no PC-Card"
            Case 5132
                Return "WIR_EMPTY_FILE - empty GSI file"
            Case 5133
                Return "WIR_INVALID_DATA - invalid data in GSI file"
            Case 5134
                Return "WIR_F2_BUTTON - F2 button pressed"
            Case 5135
                Return "WIR_F3_BUTTON - F3 button pressed"
            Case 5136
                Return "WIR_F4_BUTTON - F4 button pressed"
            Case 5137
                Return "WIR_F5_BUTTON - F5 button pressed"
            Case 5138
                Return "WIR_F6_BUTTON - F6 button pressed"
            Case 5139
                Return "WIR_SHF2_BUTTON - SHIFT F2 button pressed"

            Case 8704
                Return "AUT_RC_TIMEOUT - Position not reached"
            Case 8705
                Return "AUT_RC_DETENT_ERROR - Positioning not possible due to mounted EDM"
            Case 8706
                Return "AUT_RC_ANGLE_ERROR - Angle measurement error"
            Case 8707
                Return "AUT_RC_MOTOR_ERROR - Motorization error"
            Case 8708
                Return "AUT_RC_INCACC - Position not exactly reached"
            Case 8709
                Return "AUT_RC_DEV_ERROR - Deviation measurement error"
            Case 8710
                Return "AUT_RC_NO_TARGET - No target detected"
            Case 8711
                Return "AUT_RC_MULTIPLE_TARGETS - Multiple target detected"
            Case 8712
                Return "AUT_RC_BAD_ENVIRONMENT - Bad environment conditions"
            Case 8713
                Return "AUT_RC_DETECTOR_ERROR - Error in target acquisition"
            Case 8714
                Return "AUT_RC_NOT_ENABLED - Target acquisition not enabled"
            Case 8715
                Return "AUT_RC_CALACC - ATR-Calibration failed"
            Case 8716
                Return "AUT_RC_ACCURACY - Target position not exactly reached"
            Case 8717
                Return "AUT_RC_DIST_STARTED - Info: dist. Measurement has beenstarted"
            Case 8718
                Return "AUT_RC_SUPPLY_TOO_HIGH - external Supply voltage is too high"
            Case 8719
                Return "AUT_RC_SUPPLY_TOO_LOW - int. or ext. Supply voltage is too low"
            Case 8720
                Return "AUT_RC_NO_WORKING_AREA - working area not set"
            Case 8721
                Return "AUT_RC_ARRAY_FULL - power search data array is filled"
            Case 8722
                Return "AUT_RC_NO_DATA - no data available"

            Case 9217
                Return "BAP_CHANGE_ALL_TO_DIST - Command changed from ALL to DIST"

            Case 9473
                Return "SAP_ILLEGAL_SYSMENU_NUM - Illegal system menu number"

            Case 9728
                Return "COD_RC_LIST_NOT_VALID - List not initialized."
            Case 9729
                Return "COD_RC_SHORTCUT_UNKNOWN - Shortcut or code unknown."
            Case 9730
                Return "COD_RC_NOT_SELECTED - Codelist selection wasn't possible."
            Case 9731
                Return "COD_RC_MANDATORY_FAIL - Mandatory field has no valid value."
            Case 9732
                Return "COD_RC_NO_MORE_ATTRIB - maximal number of attr. are defined."

            Case 9984
                Return "BAS_ILL_OPCODE - Illegal opcode."
            Case 9985
                Return "BAS_DIV_BY_ZERO - Division by Zero occured."
            Case 9986
                Return "BAS_STACK_UNDERFLOW - Interpreter stack underflow."
            Case 9987
                Return "BAS_STACK_OVERFLOW - Interpreter stack overflow."
            Case 9988
                Return "BAS_NO_DLG_EXIST - No dialog is defined."
            Case 9989
                Return "BAS_DLG_ALREADY_EXIST - Only one dialog may be defined at once."
            Case 9990
                Return "BAS_INSTALL_ERR - General error during installation."
            Case 9995
                Return "BAS_FIL_INV_MODE - Invalid file access mode."
            Case 9996
                Return "BAS_FIL_TABLE_FULL - Maximum number of open files overflow."
            Case 9997
                Return "BAS_FIL_ILL_NAME - Illegal file name."
            Case 9998
                Return "BAS_FIL_ILL_POS - Illegal file position, hence < 1."
            Case 9999
                Return "BAS_FIL_ILL_OPER - Illegal operation on this kind of file."
            Case 10000
                Return "BAS_MENU_ID_INVALID - Invalid menu id detected."
            Case 10001
                Return "BAS_MENU_TABLE_FULL - Internal menu id table overflow."
            Case 10014
                Return "BAS_COM_ERR - General communication error."
            Case 10015
                Return "BAS_COM_NOCONN_ERR - Connection not established."
            Case 10016
                Return "BAS_COM_RCV_ERR - Receive error."
            Case 10017
                Return "BAS_COM_SEND_ERR - Send error."
            Case 10018
                Return "BAS_COM_FORMAT_ERR - Packet format error."
            Case 10019
                Return "BAS_COM_CHKSUM_ERR - Checksum error."
            Case 10020
                Return "BAS_COM_TIME_OUT_ERR - Action timed out error."
            Case 10021
                Return "BAS_COM_BUF_OVERRUN_ERR - Buffer overrun error."
            Case 10022
                Return "BAS_COM_TRANSID_ERR - Transaction mismatch error."
            Case 10023
                Return "BAS_GBD_LOAD_ERR - GBDbgInfo load error."
            Case 10024
                Return "BAS_GBD_IVADDR_ERR - Invalid memory address detected."
            Case 10025
                Return "BAS_GBDS_INV_OPER_ERR - Invalid operation GBDS."
            Case 10026
                Return "BAS_GBDS_NO_SESSION_ERR - Session not active."
            Case 10027
                Return "BAS_GBDC_RCV_LOCK_ERR - Couldn't enter receive CR."
            Case 10028
                Return "BAS_GBDS_TABLE_FULL_ERR - Internal table overflow."
            Case 10184
                Return "BAS_RESTART_EVT - Not an error, used to restart"
            Case 10190
                Return "BAS_LDR_MEM - FATAL Error, unhandled MEM error."
            Case 10191
                Return "BAS_LDR_ERR - FATAL Error, unhandled LDR error."

            Case 10240
                Return "IOS_CHNL_DISABLED - channel is disabled"
            Case 10241
                Return "IOS_NO_MORE_CHAR - no more data available"
            Case 10242
                Return "IOS_MAX_BLOCK_LEN - reached max. block length"
            Case 10243
                Return "IOS_HW_BUF_OVERRUN - hardware buffer overrun (highest priority)"
            Case 10244
                Return "IOS_PARITY_ERROR - parity error"
            Case 10245
                Return "IOS_FRAMING_ERROR - framing error"
            Case 10246
                Return "IOS_DECODE_ERROR - decode error"
            Case 10247
                Return "IOS_CHKSUM_ERROR - checksum error (lowest priority)"
            Case 10248
                Return "IOS_COM_ERROR - general communication error"
            Case 10280
                Return "IOS_FL_RD_ERROR - flash read error"
            Case 10281
                Return "IOS_FL_WR_ERROR - flash write error"
            Case 10282
                Return "IOS_FL_CL_ERROR - flash erase error"

            Case 10497
                Return "CNF_INI_NOTOPEN - INI-file not opened"
            Case 10498
                Return "CNF_INI_NOTFOUND - Warning: Could not find section or key"
            Case 10499
                Return "CNF_CONT - Return code of system function"
            Case 10500
                Return "CNF_ESC - Return code of system function"
            Case 10501
                Return "CNF_QUIT - Return code of system function"
            Case 10502
                Return "CNF_DATA_INVALID - Config. file data not valid"
            Case 10503
                Return "CNF_DATA_OVERFLOW - Config. file data exceed valid amount"
            Case 10504
                Return "CNF_NOT_COMPLETE - Config. file data not complete"
            Case 10505
                Return "CNF_DLG_CNT_OVERFLOW - Too many executed dialogs"
            Case 10506
                Return "CNF_NOT_EXECUTABLE - Item not executable"
            Case 10507
                Return "CNF_AEXE_OVERFLOW - Autoexec table full"
            Case 10508
                Return "CNF_PAR_LOAD_ERR - Error in loading parameter"
            Case 10509
                Return "CNF_PAR_SAVE_ERR - Error in saving parameter"
            Case 10510
                Return "CNF_FILE_MISSING - Parameter filename/path not valid"
            Case 10511
                Return "CNF_SECTION_MISSING - Section in parameter file missing"
            Case 10512
                Return "CNF_HEADER_FAIL - Default file wrong or an entry is missing"
            Case 10513
                Return "CNF_PARMETER_FAIL - Parameter-line not complete or missing"
            Case 10514
                Return "CNF_PARMETER_SET - Parameter-set caused an error"
            Case 10515
                Return "CNF_RECMASK_FAIL - RecMask-line not complete or missing"
            Case 10516
                Return "CNF_RECMASK_SET - RecMask-set caused an error"
            Case 10517
                Return "CNF_MEASDLGLIST_FAIL - MeasDlgList-line not complete or missing"
            Case 10518
                Return "CNF_MEASDLGLIST_SET - MeasDlgList-set caused an error"
            Case 10519
                Return "CNF_APPL_OVERFLOW - Application table full"
            Case Else
                Return "Unbekannter Fehlercode!!!"
        End Select
    End Function
End Class
