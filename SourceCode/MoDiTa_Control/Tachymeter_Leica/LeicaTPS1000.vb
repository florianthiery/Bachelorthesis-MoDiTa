Imports System.IO.Ports
Imports System.Threading

Public Class LeicaTPS1000
    Private _comlocking As Boolean
    Private WithEvents _myCOMPort As New IO.Ports.SerialPort

    Private _AnswerString As String

    Private _transerror As Integer
    Private _CodeNo As Long

    'Private own_math As New own_math()

    Private _zenitrange_lage1 As Double = 60.0
    Private _zenitrange_lage2 As Double = 340.0

    Public tpsTrId As Long = 1

    Private _backroundControlThread As Thread

    ''' <summary>
    ''' Objekt wird erzeugt, Comport wird NICHT geöffnet.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me._comlocking = False
        'LookupThread = New Thread(AddressOf Me.comTime)
    End Sub

    ''' <summary>
    ''' Objekt wird erzeugt und Comport geöffnet.
    ''' </summary>
    ''' <param name="portname">Portname als String. Beispiel: "COM1", "COM2" usw.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal portname As String)
        Me.openComport(portname)
        Me._comlocking = False
        'LookupThread = New Thread(AddressOf Me.comTime)
    End Sub

    ''' <summary>
    ''' Zustand des Comports.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isComportopen() As Boolean
        Get
            Return _myCOMPort.IsOpen
        End Get
    End Property

    ''' <summary>
    ''' Öffnet den Comport.
    ''' </summary>
    ''' <param name="portname">
    ''' Name des Comports (String), wo das Thermometer angeschlossen ist. Beispiel: "COM1"
    ''' </param>
    ''' <remarks></remarks>
    Public Sub openComport(ByVal portname As String)
        If _myCOMPort.IsOpen Then
            _myCOMPort.RtsEnable = False
            _myCOMPort.DtrEnable = False
            _myCOMPort.Close()

            Application.DoEvents()
            System.Threading.Thread.Sleep(200)           ' Wait 0.2 second for port to close as this does not happen immediately.
        End If

        Me._myCOMPort.PortName = portname
        Me._myCOMPort.BaudRate = 9600                       ' 
        Me._myCOMPort.DataBits = 8                          ' 
        Me._myCOMPort.Parity = IO.Ports.Parity.None         ' 
        Me._myCOMPort.StopBits = 1                          ' 
        Me._myCOMPort.Handshake = IO.Ports.Handshake.None   ' no flow control
        Me._myCOMPort.WriteTimeout = 2000                   ' Max time to wait for CTS = 2 sec.
        Me._myCOMPort.ReadBufferSize = 4096
        Me._myCOMPort.Encoding = System.Text.Encoding.GetEncoding(28591)

        Try
            Me._myCOMPort.Open()
            'setRequestGeoComOnce(111, False, False, False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If Me._myCOMPort.IsOpen Then
            Me._myCOMPort.DtrEnable = True                  ' as required by GMH3710
            Me._myCOMPort.RtsEnable = False                 ' as required by GMH3710
            'setRequestGeoComOnce(111, False, False, False)
        End If
    End Sub

    ''' <summary>
    ''' Schließt den Comport.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closeComport()
        If _myCOMPort.IsOpen Then _myCOMPort.Close()
    End Sub
    ''' <summary>
    ''' Führt einzelne GeoCom Befehle aus, ohne Schachtelung
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function setRequestGeoComOnce(ByVal out As String, ByVal thread As Thread) As Boolean

        'If (_comlocking = False And _myCOMPort.IsOpen) Then
        If (_myCOMPort.IsOpen) Then
            Me._backroundControlThread = thread
            Try
                'Dim out As String
                Me._AnswerString = ""
                'Me._CodeNo = code
                Me._transerror = 0
                'Me._Mode = mode
                'Me._WinkelTemp = Hz

                ''Verweis auf Event analyse_reply
                ''AddHandler Me._requestComplet, AddressOf Me.analyse_reply

                ''Request schreiben
                'out = ""
                'out = define_request(code, mode, Hz, Vz)

                'Zeit erfassen
                ' _TimeStartMeasure = DateTime.Now

                'Request senden
                Me._myCOMPort.Write(out)

                'Me._comlocking = True

                ' Thread: Maximale Übertragungszeit
                'LookupThread = New Thread(AddressOf Me.comTime)
                'LookupThread.Start(5000)
                Return True

                ' Exceptions für den Comport
            Catch ex As ArgumentException
                MessageBox.Show(ex.Message & "Fehler beim Schreiben auf den Comport!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me._comlocking = False
                Return False
            Catch ex As InvalidOperationException
                MessageBox.Show(ex.Message & "Fehler beim Schreiben auf den Comport!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me._comlocking = False
                Return False

                ' Excepitions für den Thread, die Exception wird sowohl in der Methode timecom und auch hier geworfen. Diese müssen 
                ' abgefangen werden, weil sonst während der Laufzeit eine Fehlermeldung kommt.
            Catch ex As ThreadInterruptedException
            Catch ex As ThreadStateException
                MessageBox.Show(ex.Message)
            End Try
        Else
            ' Findet gerade eine Kommuniktion statt, dann wird die Anfrage ohne Bearbeitung beendet. Alternativ kann auch eine Art Warteschlange programmiert werden.
            Return False
        End If

    End Function
    ''' <summary>
    ''' Diese Funktion stellt den Request zur Verfügung.
    ''' </summary>
    ''' <param name="option1">Übergabe 1 (meist mode)</param>
    ''' <param name="option2">Übergabe 2 (meist Hz)</param>
    ''' <param name="option3">Übergabe 3 (meist Vz)</param>
    ''' <returns>Request an Instrument</returns>
    ''' <remarks></remarks>
    Public Function define_request(ByVal GeoComCode As Long, ByVal transId As Short, _
                               Optional ByVal option1 As String = "", Optional ByVal option2 As String = "", _
                               Optional ByVal option3 As String = "", Optional ByVal option4 As String = "") As String


        Select Case (GeoComCode)
            Case 111
                'COM_SwitchOnTPS - Switch on TPS instrument
                '%R1Q,111:eOnMode[short] '0:local | 1:online
                'Set Mode as "Online"
                define_request = vbLf + "%R1Q,111," + CStr(transId) + ":" + option1 + vbCrLf
            Case 112
                'COM_SwitchOffTPS - Switch off or Set Sleep Mode
                '%R1Q,112:eOffMode[short] '0:down | 1:sleep
                'Set Mode as "Sleep"
                define_request = vbLf + "%R1Q,112," + CStr(transId) + ":" + option1 + vbCrLf
            Case 1004
                'EDM_Laserpointer - turning on/off the laserpointer
                '%R1Q,1004:eLaser[long]
                define_request = vbLf + "%R1Q,1004," + CStr(transId) + ":" + option1 + vbCrLf
            Case 1059
                'EDM_SetEglIntensity – changing the intensity of the electronic guide light
                '%R1Q,1059:eIntensity [long]
                define_request = vbLf + "%R1Q,1059," + CStr(transId) + ":" + option1 + vbCrLf
            Case 2003
                'TMC_GetAngle1 - Returns complete angle measurement
                '%R1Q,2003:Mode[long]
                define_request = vbLf + "%R1Q,2003," + CStr(transId) + ":" + option1 + vbCrLf
            Case 2006
                'TMC_SetInclineSwitch - Switch dual axis compensator on or off
                '%R1Q,2006:SwCorr[long]
                define_request = vbLf + "%R1Q,2006," + CStr(transId) + ":" + option1 + vbCrLf
            Case 2007
                'TMC_GetInclineSwitch - Get the dual axis compensator's state
                '%R1Q,2007:
                define_request = vbLf + "%R1Q,2007," + CStr(transId) + ":" + vbCrLf
            Case 2008
                'TMC_DoMeasure - Carries out a distance measurement
                '%R1Q,2008:Command[long],Mode[long]
                define_request = vbLf + "%R1Q,2008," + CStr(transId) + ":" + option1 + "," + option2 + vbCrLf
            Case 2009
                'TMC_GetStation - Get the coordinates of the instrument station
                '%R1Q,2009:
                define_request = vbLf + "%R1Q,2009," + CStr(transId) + ":" + vbCrLf
            Case 2010
                'TMC_SetStation - Set the coordinates of the instrument station
                '%R1Q,2010:E0[double],N0[double],H0[double],Hi[double]
                define_request = vbLf + "%R1Q,2010," + CStr(transId) + ":" + option1 + "," + option2 + "," + option3 + "," + option4 + vbCrLf
            Case 2011
                'TMC_GetHeight - Returns the current reflector height
                '%R1P,0,0:RC,Height[double]
                define_request = vbLf + "%R1Q,2011," + CStr(transId) + ":" + vbCrLf
            Case 2012
                'TMC_SetHeight - Sets new reflector height
                '%R1Q,2012:Height[double]
                define_request = vbLf + "%R1Q,2012," + CStr(transId) + ":" + option1 + vbCrLf
            Case 2014
                'TMC_GetAngSwitch - Get angular correction's states
                '%R1Q,2014:
                define_request = vbLf + "%R1Q,2014," + CStr(transId) + ":" + vbCrLf
            Case 2016
                'TMC_SetAngSwitch - Enable/disable angle corrections
                '%R1Q,2016:(correctionstring)
                define_request = vbLf + "%R1Q,2016," + CStr(transId) + ":" + option1 + "," + option2 + "," + option3 + "," + option4 + vbCrLf
            Case 2020
                'TMC_SetEdmMode - Set EDM measurement modes
                '%R1Q,2020:Mode[long]
                define_request = vbLf + "%R1Q,2020," + CStr(transId) + ":" & option1 + vbCrLf
            Case 2021
                'TMC_GetEdmMode - Get the EDM measurement mode
                '%R1Q,2021:
                define_request = vbLf + "%R1Q,2021," + CStr(transId) + ":" + vbCrLf
            Case 2022
                'TMC_GetSignal - Get information about EDM’s signal amplitude
                '%R1Q,2022:
                define_request = vbLf + "%R1Q,2022," + CStr(transId) + ":" + vbCrLf
            Case 2023
                'TMC_GetPrismCorr - Get the prism constant
                '%R1Q,2023:
                define_request = vbLf + "%R1Q,2023," + CStr(transId) + ":" + vbCrLf
            Case 2024
                'TMC_SetPrismCorr - Set the prism constant
                '%R1Q,2024:PrismCorr[double]
                define_request = vbLf + "%R1Q,2024," + CStr(transId) + ":" & option1 + vbCrLf
            Case 2028
                'TMC_SetAtmCorr - Set atmospheric correction parameters
                '%R1Q,2028:Lambda[double],Pressure[double],DryTemperature[double],WetTemperature[double]
                define_request = vbLf + "%R1Q,2028," + CStr(transId) + ":" + option1 + "," + option2 + "," + option3 + "," + option4 + vbCrLf
            Case 2029
                'TMC_GetAtmCorr - Get atmospheric correction parameters
                '%R1Q,2029:
                define_request = vbLf + "%R1Q,2029," + CStr(transId) + ":" + vbCrLf
            Case 2030
                'TMC_SetRefractiveCorr - Set the refraction factor
                '%R1Q,2030: RefOn[boolean],EarthRadius[double], RefractiveScale[double]
                define_request = vbLf + "%R1Q,2030," + CStr(transId) + ":" + option1 + "," + option2 + "," + option3 + vbCrLf
            Case 2031
                'TMC_GetRefractiveCorr - Get the refraction factor
                '%R1Q,2031:
                define_request = vbLf + "%R1Q,2031," + CStr(transId) + ":" + vbCrLf
            Case 2090
                'TMC_SetRefractiveMethod - Set the refraction model
                '%R1Q,2090:Method[unsigned short]
                define_request = vbLf + "%R1Q,2090," + CStr(transId) + ":" & option1 + vbCrLf
            Case 2091
                'TMC_GetRefractiveMethod - Get the refraction model
                '%R1Q,2091:
                define_request = vbLf + "%R1Q,2091," + CStr(transId) + ":" + vbCrLf
            Case 2107
                'TMC_GetAngle5 - Returns simple angle measurement
                '%R1Q,2107:Mode[long]
                define_request = vbLf + "%R1Q,2107," + CStr(transId) + ":" & option1 + vbCrLf
            Case 2108
                'TMC_GetSimpleMea - Returns angle and distance measurement
                '%R1Q,2108:WaitTime[long],Mode[long]
                define_request = vbLf + "%R1Q,2108," + CStr(transId) + ":" + option1 + "," + option2 + vbCrLf
            Case 2167
                'TMC_GetSimpleMea - Returns angle and distance measurement
                '%R1Q,2167:WaitTime[long],Mode[long]
                define_request = vbLf + "%R1Q,2167," + CStr(transId) + ":" + option1 + "," + option2 + vbCrLf
            Case 5003
                'CSV_GetInstrumentNo - Get factory defined instrument number
                '%R1Q,5003:
                define_request = vbLf + "%R1Q,5003," + CStr(transId) + ":" + vbCrLf
            Case 5004
                'CSV_GetInstrumentName - Get Leica specific instrument name
                '%R1Q,5004:
                define_request = vbLf + "%R1Q,5004," + CStr(transId) + ":" + vbCrLf
            Case 6001
                'MOT_StartController - Start motor controller
                '%R1Q,6001:ControlMode[long]
                define_request = vbLf + "%R1Q,6001," + CStr(transId) + ":" + option1 + vbCrLf
            Case 6002
                'MOT_StopController - Stop motor controller
                '%R1Q,6002:Mode[long]
                define_request = vbLf + "%R1Q,6002," + CStr(transId) + ":" + option1 + vbCrLf
            Case 6004
                'MOT_SetVelocity - Drive Instrument with visual control
                '%R1Q,6004:HZ-Speed[double],V-Speed[double]
                define_request = vbLf + "%R1Q,6004," + CStr(transId) + ":" + option1 + "," + option2 + vbCrLf
            Case 9007
                'AUT_SetTol - Set the positioning tolerances
                '%R1Q,9007:ToleranceHz[double], Tolerance V[double]
                define_request = vbLf + "%R1Q,9007," + CStr(transId) + ":" + option1 + "," + option1 + vbCrLf
            Case 9008
                'AUT_ReadTol - Read current setting for the positioning tolerances
                '%R1Q,9008:
                define_request = vbLf + "%R1Q,9008," + CStr(transId) + ":" + vbCrLf
            Case 9011
                'AUT_SetTimeout - Set timeout for positioning
                '%R1Q,9011:TimeoutHz[double],TimeoutV[double]
                define_request = vbLf + "%R1Q,9011," + CStr(transId) + ":" + option1 + "," + option1 + vbCrLf
            Case 9012
                'AUT_ReadTimeout - Read current timeout setting for positioning
                '%R1Q,9012:
                define_request = vbLf + "%R1Q,9012," + CStr(transId) + ":" + vbCrLf
            Case 9013
                'AUT_LockIn - starting the target tracking
                '%R1Q,9013:
                define_request = vbLf + "%R1Q,9013," + CStr(transId) + ":" + vbCrLf
            Case 9027
                'AUT_MakePositioning - Turns telescope to specified position
                '%R1Q,9027:Hz,V,PosMode,ATRMode,0
                define_request = vbLf & "%R1Q,9027," + CStr(transId) + ":" + option1 + "," + option2 + "," + option3 + ",0,0" + vbCrLf
            Case 11003
                'BMM_BeepNormal - A single beep-signal
                '%R1Q,11003:
                define_request = vbLf + "%R1Q,11003," + CStr(transId) + ":" + option1 + vbCrLf
            Case 17008
                'BAP_SetPrismType – Sets the prism type
                '%R1Q,17008: ePrismType [long]
                define_request = vbLf + "%R1Q,17008," + CStr(transId) + ":" + option1 + vbCrLf
            Case 17009
                'BAP_GetPrismType - Get actual prism type
                '%R1Q,17009:
                define_request = vbLf + "%R1Q,17009," + CStr(transId) + ":" + vbCrLf
            Case 17017
                'BAP_MeasDistanceAngle - Measure distance and angles
                '%R1Q,17017:DistMode[long]
                define_request = vbLf + "%R1Q,17017," + CStr(transId) + ":" + option1 + vbCrLf
            Case 17019
                'BAP_SetMeasPrg - Set the distance measurement program
                '%R1Q,17019:eMeasPrg [long]
                define_request = vbLf + "%R1Q,17019," + CStr(transId) + ":" + option1 + vbCrLf
            Case 17021
                'BAP_SetTargetType – Sets the target type
                '%R1Q,17021: eTargetType [long]
                define_request = vbLf + "%R1Q,17021," + CStr(transId) + ":" + option1 + vbCrLf
            Case 17022
                'BAP_GetTargetType - Get actual target type
                '%R1Q,17022:
                define_request = vbLf + "%R1Q,17022," + CStr(transId) + ":" + vbCrLf
            Case 17023
                'BAP_GetPrismDef - Get a prism definition
                '%R1Q,17023: ePrismType[long]
                define_request = vbLf + "%R1Q,17023," + CStr(transId) + ":" + option1 + vbCrLf
            Case 18005
                'AUS_SetUserAtrState - Set the status of the ATR mode
                '%R1Q,18005:OnOff[long]
                define_request = vbLf + "%R1Q,18005," + CStr(transId) + ":" + option1 + vbCrLf
            Case 18006
                'AUS_GetUserAtrState - Get the status of the ATR mode
                '%R1Q,18006:
                define_request = vbLf + "%R1Q,18006," + CStr(transId) + ":" + vbCrLf
            Case 18007
                'AUS_SetUserLockState - setting the status of the LOCK mode
                '%R1Q,18007:OnOff[long]
                define_request = vbLf + "%R1Q,18007," + CStr(transId) + ":" + option1 + vbCrLf
            Case 18008
                'AUS_GetUserLockState - getting the status of the LOCK mode
                '%R1Q,18008:
                define_request = vbLf + "%R1Q,18008," + CStr(transId) + ":" + vbCrLf
            Case 9030
                'AUT_GetFineAdjustMode – getting the fine adjust positioning mode
                '%R1Q,9030:
                define_request = vbLf + "%R1Q,9030," + CStr(transId) + ":" + vbCrLf
            Case 9031
                'AUT_SetFineAdjustMode - setting the fine adjust positioning mode
                '%R1Q,9031:mode[long]
                define_request = vbLf + "%R1Q,9031," + CStr(transId) + ":" + option1 + vbCrLf
            Case 9037
                'AUT_FineAdjust - automatic target positioning
                '%R1Q,9037: dSrchHz[double], dSrchV[double],0
                define_request = vbLf + "%R1Q,9037," + CStr(transId) + ":" + option1 + "," + option2 + ",0" + vbCrLf
            Case Else
                define_request = ""
        End Select

    End Function



    ' Note this subroutine is executed on the serial port thread - not the UI thread.
    Private Sub Receiver(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles _myCOMPort.DataReceived

        Dim RxByte As Byte

        Do
            RxByte = Me._myCOMPort.ReadByte
            Me._AnswerString = Me._AnswerString & Chr(System.Convert.ToString(RxByte))
        Loop Until (Me._myCOMPort.BytesToRead = 0)
        'And InStr(Me._AnswerString, "%R1P")
        If (InStr(Me._AnswerString, vbCrLf) And Me._transerror = 0) Then
            Me._transerror = 1
            Me._backroundControlThread.Interrupt()

        End If

    End Sub

    ''' <summary>
    ''' Analysiert den Reply und gibt Objekt an measureComp(e) über.
    ''' </summary>
    ''' <remarks>Nur für einzelne Befehle!</remarks>
    Public Sub analyse_reply(ByRef data_object As LeicaTPS1000data)
        Dim reply As String()
        Dim header As String
        Dim parameters As String

        '   0:  ok
        ' -40:  Fehler im Antwortstring
        ' -42:  unerwarteter Fehler im Antwortstring
        ' -44:  unbekannter Request
        ' -50:  Fehler wegen Zeitüberschreitung
        ' -99:  Unerwarteter Fehler
        ' > 0:  Leica ErrCode

        ' Der Befehl 111 (Gerät anschalten) ist ein Sonderfall, da er in der 1100er Serie einen abweichenden Returncode hat.
        ' Ab der 1200er Serie geht es normal weiter, wie unten.
        If (data_object.CodeNo = 111) Then
            If (InStr(data_object.reply, "%N1,0,255,,0%T0,0,0,:%R1P,0,0:0") <> 0) Then
                ' In der 1000er und 1100er Serie steht dieser String auch für eine fehlerfreie Durchführung (Nur beim 111-Befehl!).
                data_object.errorcode = 0
                Exit Sub
            End If
        End If

        'Reply auftrennen und überprüfen
        reply = Split(data_object.reply, ":")
        If UBound(reply) <> 1 Then
            data_object.errorcode = -40
            Exit Sub
        End If

        'ChLf entfernen
        If InStr(1, reply(1), vbCrLf) <> 0 Then
            reply(1) = Replace(reply(1), vbCrLf, "")
        End If

        header = reply(0)
        parameters = reply(1)

        'Überprüfung des Headers
        Dim header_array As String()
        header_array = Split(header, ",")

        'Handelt es sich um ein Geocom-Reply Typ 1?
        If (header_array(0) <> "%R1P") Then
            data_object.errorcode = -40
            Exit Sub
        End If

        'Überprüfung des Geocom Returncodes (Übertragungsfehler)
        If (header_array(1) = "0") Then
            data_object.errorcode = CInt(header_array(1))
        ElseIf (header_array(1) <> "0") Then
            data_object.errorcode = CInt(header_array(1))
            Exit Sub
        End If

        'Überprüfung der Transaction ID
        If (CInt(header_array(2)) <> data_object.tpsTrID) Then
            data_object.errorcode = -43
            Exit Sub
        End If


        Select Case (data_object.CodeNo)

            '##########################################'
            '######### Request: %R1P,0,TrId:0 #########'
            '##########################################'
            Case 9027, 9011, 9007, 17019, 6004, 6001, 6002, 6004, 2006, 11003, 17021, 17008, 2024, 2012, 2010, 2028, 2030, 2090, _
                2006, 2008, 9007, 9011, 2016, 2020, 18005, 112, 111, 9031, 9037, 9013, 18007, 1059, 1004
                '9027- AUT_MakePositioning - Turns telescope to specified position
                '9011- AUT_SetTimeout - Set timeout for positioning
                '9007 - AUT_SetTol - Set the positioning tolerances
                '17019 - BAP_SetMeasPrg - Set the distance measurement program
                '6004 - MOT_SetVelocity - Drive Instrument with visual control
                '6001 - MOT_StartController - Start motor controller
                '6002 - MOT_StopController - Stop motor controller
                '6004 - MOT_SetVelocity - Drive Instrument with visual control
                '2006 - TMC_SetInclineSwitch - Switch dual axis compensator on or off
                '11003 - BMM_BeepNormal - A single beep-signal
                '17021 - BAP_SetTargetType – Sets the target type
                '17008 - BAP_SetPrismType – Sets the prism type
                '2024 - TMC_SetPrismCorr - Set the prism constant
                '2012 - TMC_SetHeight - Sets new reflector height
                '2010 - TMC_SetStation - Set the coordinates of the instrument station
                '2028 - TMC_SetAtmCorr - Set atmospheric correction parameters
                '2030 - TMC_SetRefractiveCorr - Set the refraction factor
                '2090 - TMC_SetRefractiveMethod - Set the refraction model
                '2006 - TMC_SetInclineSwitch - Switch dual axis compensator on or off
                '2008 - TMC_DoMeasure - Carries out a distance measurement
                '9007 - AUT_SetTol - Set the positioning tolerances
                '9011 - AUT_SetTimeout - Set timeout for positioning
                '2016 - TMC_SetAngSwitch - Enable/disable angle corrections
                '2020 - TMC_SetAngSwitch - Enable/disable angle corrections
                '18005 - AUS_SetUserAtrState - Set the status of the ATR mode
                '111 - COM_SwitchOnTPS - turning on the instrument
                '112 - COM_SwitchOffTPS - turning off the instrument
                '9037 - AUT_FineAdjust - automatic target positioning
                '9013 - AUT_LockIn - starting the target tracking
                '18007 - AUS_SetUserLockState - setting the status of the LOCK mode
                '1004 - EDM_Laserpointer - turning on/off the laserpointer

                data_object.errorcode = CInt(parameters)

                '###################################################'
                '######### Request: %R1P,0,TrId:0,[string] #########'
                '###################################################'
            Case 9030 ' AUT_GetFineAdjustMode – getting the fine adjust positioning mode
                Dim content As String()
                content = Split(parameters, ",")
                If (UBound(content) = 1) Then
                    data_object.FineAdjustMode = CInt(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '###################################################'
                '######### Request: %R1P,0,TrId:0,[string] #########'
                '###################################################'
            Case 18006 ' AUS_GetUserAtrState - setting the status of the ATR mode
                Dim content As String()
                content = Split(parameters, ",")
                If (UBound(content) = 1) Then
                    data_object.ATR_Status = CInt(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                ' AUS_GetUserLockState - getting the status of the LOCK mode

                '###################################################'
                '######### Request: %R1P,0,TrId:0,[string] #########'
                '###################################################'
            Case 18008 ' AUS_GetUserLockState - getting the status of the LOCK mode
                Dim content As String()
                content = Split(parameters, ",")
                If (UBound(content) = 1) Then
                    data_object.LOCKMode_Status = CInt(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If


                '###################################################'
                '######### Request: %R1P,0,TrId:0,[string] #########'
                '###################################################'
            Case 5004 ' CSV_GetInstrumentName - Get Leica specific instrument name
                Dim content As String()
                content = Split(parameters, ",")
                'If (UBound(content) = 1 And content(0) = "0") Then
                If (UBound(content) = 1) Then
                    data_object.TheodoliteName = CStr(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '###################################################'
                '######### Request: %R1P,0,TrId:0,[long] ###########'
                '###################################################'
            Case 5003 ' CSV_GetInstrumentNo - Get factory defined instrument number
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 1 And content(0) = "0" Then
                If (UBound(content) = 1) Then
                    data_object.TheodoliteNumber = CLng(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

            Case 2007 'TMC_GetInclineSwitch - Get the dual axis compensator's state
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 1 And content(0) = "0" Then
                If (UBound(content) = 1) Then
                    data_object.theoInclineSwitch = CLng(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

            Case 2026 'TMC_GetFace - Get face information of current telescope position
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 1 And content(0) = "0" Then
                If (UBound(content) = 1) Then
                    data_object.FaceDef = CLng(content(1))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '##############################################################'
                '######### Request: %R1P,0,TrId:0,[double],[double] ###########'
                '##############################################################'
            Case 9012 'AUT_ReadTimeout - Read current timeout setting for positioning
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 2 And content(0) = "0" Then
                If (UBound(content) = 2) Then
                    data_object.theoHzTime = CDbl(content(1)) 'in Sekunden
                    data_object.theoZdTime = CDbl(content(2)) 'in Sekunden
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If
            Case 9008 'AUT_ReadTol - Read current setting for the positioning tolerances
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 2 And content(0) = "0" Then
                If (UBound(content) = 2) Then
                    data_object.theoHzTol = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.theoVzTol = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If
            Case 2107 ' TMC_GetAngle5 - Returns simple angle measurement
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 2 And content(0) = "0" Then
                If (UBound(content) = 2) Then
                    data_object.horizontalrichtung = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.zenitwinkel = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '#######################################################################'
                '######### Request: %R1P,0,TrId:0,[double],[double],[double] ###########'
                '#######################################################################'
            Case 2108 'TMC_GetSimpleMea - Returns angle and distance measurement
                Dim content As String()
                content = Split(parameters, ",")
                If UBound(content) = 3 And content(0) = "0" Then
                    data_object.horizontalrichtung = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.zenitwinkel = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    data_object.distanz = Math.Round(CDbl(Replace(content(3), ".", ",")), 4) 'Ersetz . durch , in der gemessenen Distanz
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '########################################################################'
                '######### Request: %R1P,0,TrId:0,[long],[long],[long],[long] ###########'
                '########################################################################'
            Case 2014 ' TMC_GetAngSwitch - Get angular correction's states
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 4 And content(0) = "0" Then
                If (UBound(content) = 4) Then
                    data_object.theoInclineCorr = CLng(content(1))
                    data_object.theoStandAxisCorr = CLng(content(2))
                    data_object.theoCollimationCorr = CLng(content(3))
                    data_object.theoTiltAxisCorr = CLng(content(4))
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '##############################################################################'
                '######### Request: %R1P,0,TrId:0,[double],[double],[double],[long] ###########'
                '##############################################################################'
            Case 17017 'BAP_MeasDistanceAngle - Measure distance and angles
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 4 And content(0) = "0" Then
                If (UBound(content) = 4) Then
                    data_object.horizontalrichtung = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.zenitwinkel = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    ' Achtung nicht notwendig, weil Dezimaltrennzeichen ist hier .  nicht ,   wird evtl. von vb.net automatisch wieder zurückgewandelt
                    data_object.distanz = Math.Round(CDbl(Replace(content(3), ".", ",")), 4) 'Ersetz . durch , in der gemessenen Distanz
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

                '################################################################################'
                '######### Request: %R1P,0,TrId:0,[d],[d],[d],[l],[d],[d],[d],[l],[l] ###########'
                '################################################################################'
            Case 2003 'TMC_GetAngle1 - Returns complete angle measurement
                Dim content As String()
                content = Split(parameters, ",")
                'If UBound(content) = 9 And content(0) = "0" Then
                If (UBound(content) = 9) Then
                    data_object.horizontalrichtung = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.zenitwinkel = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    data_object.AngleAccuracy = own_math.rad2gon_digits(content(3), 5)  'in Gon
                    data_object.AngleTime = content(4) 'in sec
                    data_object.CrossIncline = own_math.rad2gon_digits(content(5), 5)  'in Gon
                    data_object.LengthIncline = own_math.rad2gon_digits(content(6), 5)  'in Gon
                    data_object.AccuracyIncline = own_math.rad2gon_digits(content(7), 5)  'in Gon
                    data_object.InclineTime = content(8) 'in sec
                    data_object.FaceDef = content(9)
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

            Case 2167 'TMC_GetAngle1 - Returns complete angle measurement
                Dim content As String()
                content = Split(parameters, ",")

                If (UBound(content) = 8) Then
                    data_object.horizontalrichtung = own_math.rad2gon_digits(content(1), 5) 'in Gon
                    data_object.zenitwinkel = own_math.rad2gon_digits(content(2), 5) 'in Gon
                    data_object.AngleAccuracy = own_math.rad2gon_digits(content(3), 5)  'in Gon
                    data_object.CrossIncline = own_math.rad2gon_digits(content(4), 5)  'in Gon
                    data_object.LengthIncline = own_math.rad2gon_digits(content(5), 5)  'in Gon
                    data_object.AccuracyIncline = own_math.rad2gon_digits(content(6), 5)  'in Gon
                    data_object.distanz = Math.Round(CDbl(Replace(content(7), ".", ",")), 4) 'Ersetz . durch , in der gemessenen Distanz
                    data_object.distTime = Math.Round(CDbl(Replace(content(8), ".", ",")), 4) 'Ersetz . durch , in der gemessenen Distanz
                    data_object.errorcode = CInt(content(0))
                ElseIf (content(0) <> "0") Then
                    data_object.errorcode = CInt(content(0))
                Else
                    data_object.errorcode = -40
                End If

            Case Else
                data_object.errorcode = -44

        End Select

    End Sub





    'Public Function analyse_reply(ByVal tpsTrId As Integer) As LeicaTPS1000data

    '    Dim Out_Clear As String = "-999" 'Code in Klarschrift
    '    Dim Reply As Object, Content As Object 'Array zum auswerten



    '    'Intialisierung der Ergebnis-Variablen um Fehler aufzudecken
    '    Dim TheodoliteName As String = "-999"
    '    Dim TheodoliteNumber As Long = -999
    '    Dim theoInclineSwitch As Long = -999
    '    Dim theoHzTime As Double = -999.0#
    '    Dim theoZdTime As Double = -999.0#
    '    Dim theoHzTol As Double = -999.0#
    '    Dim theoVzTol As Double = -999.0#
    '    Dim HzReading As Double = -999.0#
    '    Dim ZdReading As Double = -999.0#
    '    Dim theoInclineCorr As Long = -999
    '    Dim theoStandAxisCorr As Long = -999
    '    Dim theoCollimationCorr As Long = -999
    '    Dim theoTiltAxisCorr As Long = -999
    '    Dim AngleAccuracy As Double = -999.0#
    '    Dim AngleTime As Long = -999
    '    Dim CrossIncline As Double = -999.0#
    '    Dim LengthIncline As Double = -999.0#
    '    Dim AccuracyIncline As Double = -999.0#
    '    Dim InclineTime As Long = -999
    '    Dim FaceDef As Long = -999
    '    Dim dDist As Double = -999.0#
    '    Dim DistMode As Long = -999

    '    Dim uebergabe As New LeicaTPS1000data()

    '    'Daten vom Tachymeter
    '    uebergabe.horizontalrichtung = HzReading
    '    uebergabe.zenitwinkel = ZdReading
    '    uebergabe.TheodoliteName = TheodoliteName
    '    uebergabe.TheodoliteNumber = TheodoliteNumber
    '    uebergabe.theoInclineSwitch = theoInclineSwitch
    '    uebergabe.theoHzTime = theoHzTime
    '    uebergabe.theoZdTime = theoZdTime
    '    uebergabe.theoHzTol = theoHzTol
    '    uebergabe.theoVzTol = theoVzTol
    '    uebergabe.theoInclineCorr = theoInclineCorr
    '    uebergabe.theoStandAxisCorr = theoStandAxisCorr
    '    uebergabe.theoCollimationCorr = theoCollimationCorr
    '    uebergabe.theoTiltAxisCorr = theoTiltAxisCorr
    '    uebergabe.AngleAccuracy = AngleAccuracy
    '    uebergabe.AngleTime = AngleTime
    '    uebergabe.CrossIncline = CrossIncline
    '    uebergabe.LengthIncline = LengthIncline
    '    uebergabe.AccuracyIncline = AccuracyIncline
    '    uebergabe.InclineTime = AccuracyIncline
    '    uebergabe.FaceDef = FaceDef
    '    uebergabe.dDist = dDist
    '    uebergabe.DistMode = DistMode
    '    ' _FaceDef = FaceDef 'für Benutzung in getAngle2Drive

    '    'Sonstige Daten
    '    uebergabe.CodeNo = _CodeNo
    '    'uebergabe.Period = _period
    '    'uebergabe.TimeStartMeasure = _TimeStartMeasure
    '    uebergabe.errorcode = 0


    '    'Handler
    '    'AddHandler Event_GetFace, AddressOf getFaceType

    '    Try

    '        'Zeitberechnung Start Messung, bis Anfang Uswertung
    '        'Dim TimeAfter As Date = DateTime.Now
    '        'Dim period_ts As TimeSpan = TimeAfter - _TimeStartMeasure
    '        '_period = period_ts.TotalMilliseconds

    '        'Request in "Klartext"
    '        Out_Clear = "%R1Q," & Me._CodeNo

    '        '   0:  ok
    '        ' -40:  Fehler im Antwortstring
    '        ' -42:  unerwarteter Fehler im Antwortstring
    '        ' -44:  unbekannter Request
    '        ' -50:  Fehler wegen Zeitüberschreitung
    '        ' -99:  Unerwarteter Fehler
    '        ' > 0:  Leica ErrCode

    '        'If (_period > _tpsWaitMeasure) Then
    '        '    Me._transerror = -50 ' Time overflow

    '        If InStr(Me._AnswerString, "%R1P,0," & tpsTrId & ":0") <> 0 _
    '                                            Or InStr(Me._AnswerString, "%R1P,0,0:0") = 1 Then

    '            'Reply auftrennen und überprüfen
    '            Reply = Split(Me._AnswerString, ":")
    '            If UBound(Reply) <> 1 Then
    '                Me._transerror = -40
    '                Return uebergabe
    '                'Exit Function
    '            End If

    '            'ChLf entfernen
    '            If InStr(1, Reply(1), vbCrLf) <> 0 Then
    '                Reply(1) = Replace(Reply(1), vbCrLf, "")
    '            End If

    '            Select Case (Out_Clear)

    '                '##########################################'
    '                '######### Request: %R1P,0,TrId:0 #########'
    '                '##########################################'
    '                Case "%R1Q,9027", "%R1Q,9011", "%R1Q,9007", "%R1Q,17019", _
    '                     "%R1Q,6004", "%R1Q,6001", "%R1Q,6002", "%R1Q,6004", _
    '                     "%R1Q,2006", "%R1Q,11004", "%R1Q,2016"
    '                    '## Requests in Reihenfolge in der Case-Anweisung ##'
    '                    'AUT_MakePositioning - Turns telescope to specified position
    '                    'AUT_SetTimeout - Set timeout for positioning
    '                    'AUT_SetTol - Set the positioning tolerances
    '                    'BAP_SetMeasPrg - Set the distance measurement program
    '                    'MOT_SetVelocity - Drive Instrument with visual control
    '                    'MOT_StartController - Start motor controller
    '                    'MOT_StopController - Stop motor controller
    '                    'MOT_SetVelocity Drive - Instrument with visual control
    '                    'TMC_SetInclineSwitch - Switch dual axis compensator on or off
    '                    'BMM_BeepNormal - A single beep-signal
    '                    'TMC_SetAngSwitch - Enable/disable angle corrections
    '                    If Reply(1) = "0" Then
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '#######################################'
    '                    '######### Request: %R1P,0,0:0 #########'
    '                    '#######################################'
    '                Case "%R1Q,111" ' COM_SwitchOnTPS - Switch on TPS instrument
    '                    'Theo war ON
    '                    If UBound(Reply) = 1 And Reply(1) = 0 Then
    '                        Me._transerror = 1
    '                        'Theo war OFF
    '                    ElseIf UBound(Reply) = 2 And Reply(0) = "%N1,0,255,,0%T0,0,0," And Reply(3) = 0 Then
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '#######################################'
    '                    '######### Request: %R1P,0,0:0 #########'
    '                    '#######################################'
    '                Case "%R1Q,112" ' COM_SwitchOffTPS - Switch off TPS1100 or Set Sleep Mode
    '                    'Theo war ON
    '                    If UBound(Reply) = 1 And Reply(1) = 0 Then
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '###################################################'
    '                    '######### Request: %R1P,0,TrId:0,[string] #########'
    '                    '###################################################'
    '                Case "%R1Q,5004" ' CSV_GetInstrumentName - Get Leica specific instrument name
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 1 And Content(0) = "0" Then
    '                        TheodoliteName = CStr(Content(1))
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '###################################################'
    '                    '######### Request: %R1P,0,TrId:0,[long] ###########'
    '                    '###################################################'
    '                Case "%R1Q,5003" ' CSV_GetInstrumentNo - Get factory defined instrument number
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 1 And Content(0) = "0" Then
    '                        TheodoliteNumber = CLng(Content(1))
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If
    '                Case "%R1Q,2007" 'TMC_GetInclineSwitch - Get the dual axis compensator's state
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 1 And Content(0) = "0" Then
    '                        theoInclineSwitch = CLng(Content(1))
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If
    '                Case "%R1Q,2026" 'TMC_GetFace - Get face information of current telescope position
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 1 And Content(0) = "0" Then
    '                        FaceDef = CLng(Content(1))
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '##############################################################'
    '                    '######### Request: %R1P,0,TrId:0,[double],[double] ###########'
    '                    '##############################################################'
    '                Case "%R1Q,9012" 'AUT_ReadTimeout - Read current timeout setting for positioning
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 2 And Content(0) = "0" Then
    '                        theoHzTime = CDbl(Content(1)) 'in Sekunden
    '                        theoZdTime = CDbl(Content(2)) 'in Sekunden
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If
    '                Case "%R1Q,9008" 'AUT_ReadTol - Read current setting for the positioning tolerances
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 2 And Content(0) = "0" Then
    '                        theoHzTol = own_math.rad2gon_digits(Content(1), 5) 'in Gon
    '                        theoVzTol = own_math.rad2gon_digits(Content(2), 5) 'in Gon
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If
    '                Case "%R1Q,2107" ' TMC_GetAngle5 - Returns simple angle measurement
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 2 And Content(0) = "0" Then
    '                        HzReading = own_math.rad2gon_digits(Content(1), 5) 'in Gon
    '                        ZdReading = own_math.rad2gon_digits(Content(2), 5) 'in Gon
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '########################################################################'
    '                    '######### Request: %R1P,0,TrId:0,[long],[long],[long],[long] ###########'
    '                    '########################################################################'
    '                Case "%R1Q,2014" ' TMC_GetAngSwitch - Get angular correction's states
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 4 And Content(0) = "0" Then
    '                        theoInclineCorr = CLng(Content(1))
    '                        theoStandAxisCorr = CLng(Content(2))
    '                        theoCollimationCorr = CLng(Content(3))
    '                        theoTiltAxisCorr = CLng(Content(4))
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '##############################################################################'
    '                    '######### Request: %R1P,0,TrId:0,[double],[double],[double],[long] ###########'
    '                    '##############################################################################'
    '                Case "%R1Q,17017" 'BAP_MeasDistanceAngle - Measure distance and angles
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 4 And Content(0) = "0" Then
    '                        HzReading = own_math.rad2gon_digits(Content(1), 5) 'in Gon
    '                        ZdReading = own_math.rad2gon_digits(Content(2), 5) 'in Gon
    '                        dDist = Math.Round(CDbl(Replace(Content(3), ".", ",")), 4) 'Ersetz . durch , in der gemessenen Distanz
    '                        DistMode = CLng(Content(4)) 'gibt Distanzmodus aus (Doku S.96)
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                    '################################################################################'
    '                    '######### Request: %R1P,0,TrId:0,[d],[d],[d],[l],[d],[d],[d],[l],[l] ###########'
    '                    '################################################################################'
    '                Case "%R1Q,2003" 'TMC_GetAngle1 - Returns complete angle measurement
    '                    Content = Split(Reply(1), ",")
    '                    If UBound(Content) = 9 And Content(0) = "0" Then
    '                        HzReading = own_math.rad2gon_digits(Content(1), 5) 'in Gon
    '                        ZdReading = own_math.rad2gon_digits(Content(2), 5) 'in Gon
    '                        AngleAccuracy = own_math.rad2gon_digits(Content(3), 5)  'in Gon
    '                        AngleTime = Content(4) 'in sec
    '                        CrossIncline = own_math.rad2gon_digits(Content(5), 5)  'in Gon
    '                        LengthIncline = own_math.rad2gon_digits(Content(6), 5)  'in Gon
    '                        AccuracyIncline = own_math.rad2gon_digits(Content(7), 5)  'in Gon
    '                        InclineTime = Content(8) 'in sec
    '                        FaceDef = Content(9)
    '                        Me._transerror = 1
    '                    Else
    '                        Me._transerror = -40
    '                    End If

    '                Case Else
    '                    Me._transerror = -44

    '            End Select

    '            If Me._transerror = 1 Then

    '                ' Event an die GUI.


    '                'Daten vom Tachymeter
    '                uebergabe.horizontalrichtung = HzReading
    '                uebergabe.zenitwinkel = ZdReading
    '                uebergabe.TheodoliteName = TheodoliteName
    '                uebergabe.TheodoliteNumber = TheodoliteNumber
    '                uebergabe.theoInclineSwitch = theoInclineSwitch
    '                uebergabe.theoHzTime = theoHzTime
    '                uebergabe.theoZdTime = theoZdTime
    '                uebergabe.theoHzTol = theoHzTol
    '                uebergabe.theoVzTol = theoVzTol
    '                uebergabe.theoInclineCorr = theoInclineCorr
    '                uebergabe.theoStandAxisCorr = theoStandAxisCorr
    '                uebergabe.theoCollimationCorr = theoCollimationCorr
    '                uebergabe.theoTiltAxisCorr = theoTiltAxisCorr
    '                uebergabe.AngleAccuracy = AngleAccuracy
    '                uebergabe.AngleTime = AngleTime
    '                uebergabe.CrossIncline = CrossIncline
    '                uebergabe.LengthIncline = LengthIncline
    '                uebergabe.AccuracyIncline = AccuracyIncline
    '                uebergabe.InclineTime = AccuracyIncline
    '                uebergabe.FaceDef = FaceDef
    '                uebergabe.dDist = dDist
    '                uebergabe.DistMode = DistMode
    '                ' _FaceDef = FaceDef 'für Benutzung in getAngle2Drive

    '                'Sonstige Daten
    '                uebergabe.CodeNo = _CodeNo
    '                'uebergabe.Period = _period
    '                'uebergabe.TimeStartMeasure = _TimeStartMeasure
    '                uebergabe.errorcode = _transerror

    '                'RemoveHandler Me._requestComplet, AddressOf Me.analyse_reply
    '                Me._comlocking = False

    '                ' Der Thread der die Übertragungszeit überprüft wird beendet.
    '                'LookupThread.Interrupt()
    '                'LookupThread.Abort()

    '                ' Evtl. Sinnvoller: Das Event in einem neuem Thread starten, damit die Nachbearbeitung der Daten nicht diesen Thread ausbremst.
    '                ' Events erzeugen keinen neuen Thread!
    '                Return uebergabe
    '                'RaiseEvent angleCompletet(uebergabe)

    '                ' 

    '                If Me._CodeNo = 2007 Then 'Kompensatorabfrage
    '                    Dim test2 As Long = 3
    '                    'RaiseEvent Event_ChangeButtonCompensator(uebergabe)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 2006 Then 'Kompensator setzen
    '                    'uebergabe.theoInclineSwitch = _Mode
    '                    'RaiseEvent Event_ChangeButtonCompensator(uebergabe)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 2014 Then 'Korrekturabfrage
    '                    '0 = Abfrage
    '                    'RaiseEvent Event_ChangeComboBoxCorrections(uebergabe, 0)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 2016 Then 'Achsenkorrekturen setzen
    '                    '1 = Setzen
    '                    'uebergabe.TheodoliteName = _Mode
    '                    'RaiseEvent Event_ChangeComboBoxCorrections(uebergabe, 1)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 9008 Then 'GetPositionTolerance
    '                    Dim test3 As Long = 3
    '                    '0 = abfragen
    '                    'RaiseEvent Event_GetActuallyPositionTolerance(uebergabe, 0)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 9012 Then 'GetPositionTime
    '                    Dim test As Long = 3
    '                    'RaiseEvent Event_GetActuallyPositionTime(uebergabe)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 9007 Then 'SetPositionTolerance
    '                    '1 = setzen
    '                    'uebergabe.theoHzTol = _WinkelTemp
    '                    'RaiseEvent Event_GetActuallyPositionTolerance(uebergabe, 1)
    '                    'RaiseEvent measureComp(uebergabe)
    '                ElseIf Me._CodeNo = 9011 Then 'SetPositionTime
    '                    'uebergabe.theoHzTime = _WinkelTemp
    '                    'RaiseEvent Event_GetActuallyPositionTime(uebergabe)
    '                    'RaiseEvent measureComp(uebergabe)
    '                Else
    '                    'RaiseEvent measureComp(uebergabe) 'Anzeige in Messagebox
    '                End If
    '            End If

    '        Else
    '            Me._transerror = -99
    '            Return uebergabe
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message + " Fehler!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return uebergabe
    '    End Try
    '    Return uebergabe
    'End Function
    Public ReadOnly Property antwortstring() As String
        Get
            Return Me._AnswerString
        End Get
    End Property


    Public Sub def_zenit_range(ByVal lage1 As Double, ByVal lage2 As Double)
        Me._zenitrange_lage1 = lage1
        Me._zenitrange_lage2 = lage2
    End Sub


    Public Function isInRange(ByVal v As Double) As Boolean
        If (v >= Me._zenitrange_lage1 And v <= Me._zenitrange_lage2) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isFaceI(ByVal v As Double) As Boolean
        If (v < Math.PI) Then
            Return True
        Else
            Return False
        End If
    End Function


#Region "Umrechnungen"

    ''' <summary>
    ''' Diese Funktion rechnet einen Winkel für das Verfahren in RAD um.
    ''' </summary>
    ''' <param name="uebergabe">Winkel zum Verfahren</param>
    ''' <returns>Winkel in Vollkreisrad zum Verfahren</returns>
    ''' <remarks>Umrechnung unter Benutzung der onw_math Klasse</remarks>
    Public Function getAngle2Drive(ByVal uebergabe As Double) As Double

        getAngle2Drive = own_math.gon2rad(uebergabe)

    End Function

#End Region
End Class
