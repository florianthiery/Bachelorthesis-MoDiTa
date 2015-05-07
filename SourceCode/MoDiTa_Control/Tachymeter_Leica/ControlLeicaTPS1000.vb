' #### Klasse zur Steuerung Leica Tachymeter #####

Imports System.Threading

Public Class ControlLeicaTPS1000
    Private _tachymeter As LeicaTPS1000
    Private _GUI As MoDiTaGUI
    Private _tpsTRId As Short = 0


    Private _isComReady As Boolean = True

    Private Delegate Sub DisplayData1(ByRef data As LeicaTPS1000data)
    Private Delegate Sub DisplayData0()

    Public Event get_Angle_All_ready(ByRef data_object As LeicaTPS1000data)
    Public Event theo_info_ready(ByRef data_object As LeicaTPS1000data)
    Public Event get_Angle_And_Distance_ready(ByRef data_object As LeicaTPS1000data)
    Public Event get_single_distance_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_theo_on_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_theo_off_ready(ByRef data_object As LeicaTPS1000data)
    Public Event move_Absolute_HzV_ready(ByRef data_object As LeicaTPS1000data)
    Public Event change_face_ready(ByRef data_object As LeicaTPS1000data)

    Public Event get_ATR_State_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_ATR_State_ready(ByRef data_object As LeicaTPS1000data)

    Public Event get_Fine_Adjust_Mode_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_Fine_Adjust_Mode_ready(ByRef data_object As LeicaTPS1000data)
    Public Event fine_adjust_ready(ByRef data_object As LeicaTPS1000data)

    Public Event lock_in_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_Lock_Mode_ready(ByRef data_object As LeicaTPS1000data)
    Public Event get_Lock_Mode_ready(ByRef data_object As LeicaTPS1000data)
    Public Event set_electronic_guide_light_ready(ByRef data_object As LeicaTPS1000data)

    Public Event set_Laserpointer_ready(ByRef data_object As LeicaTPS1000data)





    Public Sub New(ByVal gui As Form, ByVal tachymeter As LeicaTPS1000)
        Me._GUI = CType(gui, MoDiTaGUI)
        Me._tachymeter = tachymeter
        Me._isComReady = True
    End Sub
    Private Function build_TransID() As Short
        ' Die Transaction ID ist ein signed short (Wertebereich -32768 bis 32767), 
        ' negative IDs wären möglich, aber unübersichtlich
        If (Me._tpsTRId < 1 Or Me._tpsTRId >= 32767) Then Me._tpsTRId = 0
        Me._tpsTRId = Me._tpsTRId + 1
        Return Me._tpsTRId
    End Function
    Private Sub comlocker()
        Do While (Me._isComReady = False)
            Thread.Sleep(1)
        Loop
    End Sub

#Region "Test String"
    ''' <summary>
    ''' Diese Funktion gibt den String einfach an das Gerät weiter
    ''' </summary>
    ''' <param name="requeststring">
    ''' String an Geocom
    ''' </param>
    ''' <remarks></remarks>
    Public Sub test_request_strings(ByVal requeststring As String)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data



        data_object.tpsTrID = build_TransID()

        data_object.requestString = requeststring

        Dim backgroundThread As New Thread(AddressOf thread_test_request_strings)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_test_request_strings(ByVal data_object As LeicaTPS1000data)

        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)
        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            'Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        End Try
        Dim disp As New DisplayData1(AddressOf Me._GUI.display_test_request_strings)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
#End Region
    
#Region "Joystick"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hz_speed">
    ''' Geschwindigkeit in Horizontalrichtung [rad/s]
    ''' </param>
    ''' <param name="zw_speed">
    ''' Geschwindigkeit in Vertikalrichtung [rad/s]
    ''' </param>
    ''' <remarks></remarks>
    Public Sub start_move_joystick(ByVal hz_speed As Double, ByVal zw_speed As Double)
        'If (Me._isComReady = False) Then
        '    comlocker()
        '    'Me._GUI.display_comlock_error()
        '    'Exit Sub
        'End If
        Me._isComReady = False
        Try
            Dim data_object(1) As LeicaTPS1000data

            ' Startet den Controller:
            Dim ControlMode As Long
            ' Mode laut Geocom:
            'MOT_POSIT = 0, // configured for relative postioning
            'MOT_OCONST = 1, // configured for constant speed
            'MOT_MANUPOS = 2, // configured for manual positioning
            '// default setting
            'MOT_LOCK = 3, // configured as "Lock-In"-controller
            'MOT_BREAK = 4, // configured as "Brake"-controller
            '// do not use 5 and 6
            'MOT_TERM = 7 // terminates the controller task

            ' Für Joystick gilt:
            ControlMode = 1

            ' Start Controller:
            data_object(0) = New LeicaTPS1000data
            data_object(0).CodeNo = 6001
            data_object(0).tpsTrID = build_TransID()

            data_object(0).requestString = Me._tachymeter.define_request(data_object(0).CodeNo, data_object(0).tpsTrID, CStr(ControlMode))

            ' Setze Geschwindigkeit und Motor startet:
            data_object(1) = New LeicaTPS1000data
            data_object(1).CodeNo = 6004
            data_object(1).tpsTrID = build_TransID()

            data_object(1).requestString = Me._tachymeter.define_request(data_object(1).CodeNo, data_object(1).tpsTrID, Replace(CStr(hz_speed), ",", "."), Replace(CStr(zw_speed), ",", "."))
            Me.thread_start_move_joystick(data_object)

            'Dim backgroundThread As New Thread(AddressOf thread_start_move_joystick)
            'backgroundThread.Start(data_object)
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try
    End Sub
    Private Sub thread_start_move_joystick(ByVal data_object() As LeicaTPS1000data)
        Try
            Dim array_length As Integer
            array_length = data_object.Length
            'Me._isComReady = False
            For i = 0 To array_length - 1 Step 1
                Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
                Try
                    Thread.Sleep(2000)
                    data_object(i).errorcode = -50 ' Zeitüberschreitung
                    data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                    data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                Catch ex As ThreadInterruptedException
                    data_object(i).reply = Me._tachymeter.antwortstring
                    Me._tachymeter.analyse_reply(data_object(i))
                    data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                    data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                End Try

                Dim disp As New DisplayData1(AddressOf Me._GUI.display_start_move_joystick)
                Me._GUI.Invoke(disp, data_object(i))

            Next
            Me._isComReady = True
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try

    End Sub

    Public Sub stop_move_joystick()
        'If (Me._isComReady = False) Then
        '    comlocker()
        '    'data_object(0) = New LeicaTPS1000data
        '    'data_object(0).errorcode = -45
        '    'Me._GUI.display_comlock_error()
        '    'Exit Sub
        'End If
        Me._isComReady = False
        Try
            Dim data_object(1) As LeicaTPS1000data



            data_object(0) = New LeicaTPS1000data
            data_object(0).CodeNo = 6002
            data_object(0).tpsTrID = build_TransID()

            Dim mode As Long
            ' Mode laut Geocom:
            'MOT_NORMAL = 0, // slow down with current acceleration
            'MOT_SHUTDOWN = 1 // slow down by switch off power supply
            ' Für Joystick:
            mode = 0

            data_object(0).requestString = Me._tachymeter.define_request(data_object(0).CodeNo, data_object(0).tpsTrID, CStr(mode))

            data_object(1) = New LeicaTPS1000data
            data_object(1).CodeNo = 6001
            data_object(1).tpsTrID = build_TransID()

            Dim ControlMode As Long
            ' Mode laut Geocom:
            'MOT_POSIT = 0, // configured for relative postioning
            'MOT_OCONST = 1, // configured for constant speed
            'MOT_MANUPOS = 2, // configured for manual positioning
            '// default setting
            'MOT_LOCK = 3, // configured as "Lock-In"-controller
            'MOT_BREAK = 4, // configured as "Brake"-controller
            '// do not use 5 and 6
            'MOT_TERM = 7 // terminates the controller task

            ' nach dem Joystick wieder auf "manual positioning", gibt somit den Feintrieb wieder frei:
            ControlMode = 2

            data_object(1).requestString = Me._tachymeter.define_request(data_object(1).CodeNo, data_object(1).tpsTrID, CStr(ControlMode))

            Me.thread_stop_move_joystick(data_object)
            'Dim backgroundThread As New Thread(AddressOf thread_stop_move_joystick)
            'backgroundThread.Start(data_object)
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try
    End Sub

    Private Sub thread_stop_move_joystick(ByVal data_object() As LeicaTPS1000data)
        Try
            Dim array_length As Integer
            array_length = data_object.Length
            'Me._isComReady = False
            For i = 0 To array_length - 1 Step 1
                Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
                Try
                    Thread.Sleep(2000)
                    data_object(i).errorcode = -50 ' Zeitüberschreitung
                    data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                    data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                Catch ex As ThreadInterruptedException
                    data_object(i).reply = Me._tachymeter.antwortstring
                    Me._tachymeter.analyse_reply(data_object(i))
                    data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                    data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                End Try

                Dim disp As New DisplayData1(AddressOf Me._GUI.display_start_move_joystick)
                Me._GUI.Invoke(disp, data_object(i))

                'Me.stop_move_joystick()

            Next
            Me._isComReady = True
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try
    End Sub
#End Region
#Region "Distanzmessung"
    ''' <summary>
    ''' Streckenmessung
    ''' </summary>
    ''' <param name="tmc_measurement_mode">
    ''' TMC_STOP = 0, // Stop measurement program
    ''' TMC_DEF_DIST = 1, // Default DIST-measurement program
    ''' TMC_CLEAR = 3, // TMC_STOP and clear data
    ''' TMC_SIGNAL = 4, // Signal measurement (test function)
    ''' TMC_DO_MEASURE = 6, // (Re)start measurement task
    ''' TMC_RTRK_DIST = 8, // Distance-TRK measurement program
    ''' TMC_RED_TRK_DIST = 10, // Reflectorless tracking
    ''' TMC_FREQUENCY = 11 // Frequency measurement (test)
    ''' </param>
    ''' <param name="edm_measurement_mode">
    ''' EDM_MODE_NOT_USED = 0, // Init value
    ''' EDM_SINGLE_TAPE = 1, // IR Standard Reflector Tape
    ''' EDM_SINGLE_STANDARD = 2, // IR Standard
    ''' EDM_SINGLE_FAST = 3, // IR Fast
    ''' EDM_SINGLE_LRANGE = 4, // LO Standard
    ''' EDM_SINGLE_SRANGE = 5, // RL Standard
    ''' EDM_CONT_STANDARD = 6, // Standard repeated measurement
    ''' EDM_CONT_DYNAMIC = 7, // IR Tacking EDM_CONT_REFLESS = 8, // RL Tracking
    ''' EDM_CONT_FAST = 9, // Fast repeated measurement
    ''' EDM_AVERAGE_IR = 10,// IR Average
    ''' EDM_AVERAGE_SR = 11,// RL Average
    ''' EDM_AVERAGE_LR = 12, // LO Average
    ''' EDM_PRECISE_IR = 13, // IR Precise (TS30, TM30)
    ''' EDM_PRECISE_TAPE = 14 // IR Precise Reflector Tape (TS30, TM30)
    ''' </param>
    ''' <param name="inclination_mode">
    ''' TMC_MEA_INC = 0, // Use sensor (apriori sigma)
    ''' TMC_AUTO_INC = 1, // Automatic mode (sensor/plane)
    ''' TMC_PLANE_INC = 2, // Use plane (apriori sigma
    ''' </param>
    ''' <remarks>
    ''' Zur genauen Beschreibung der Parameter bitte im Geocomhandbuch nachschauen.
    ''' Beispiele:
    ''' IR (1,2,1)
    ''' RL (10,5,1)
    ''' </remarks>
    Public Sub get_single_distance(ByVal tmc_measurement_mode As Long, Optional ByVal edm_measurement_mode As Long = 2, Optional ByVal inclination_mode As Long = 1)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object(5) As LeicaTPS1000data

        Dim index As Integer

        ' Stellt den EDM-Mode ein: 
        index = 0
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2020
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(edm_measurement_mode))

        ' Stopt Messungnen und löscht die Daten:
        index = 1
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3), CStr(1))

        ' Führt die Streckenmessung durch, mit dem jeweiligen Messtyp (z.B. IR oder RL) und dem Konpensator
        index = 2
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(tmc_measurement_mode), CStr(inclination_mode))

        ' Holt die Messergebnisse
        index = 3
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2108
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3000), CStr(inclination_mode))

        ' Stopt Messungnen und löscht die Daten: 
        index = 4
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3), CStr(inclination_mode))

        ' Stellt den EDM-Mode auf Standard ein: 
        index = 5
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2020
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(2))

        Dim backgroundThread As New Thread(AddressOf thread_get_single_distance)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_single_distance(ByVal data_object() As LeicaTPS1000data)
        Dim array_length As Integer
        array_length = data_object.Length

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object(i).errorcode = -50 ' Zeitüberschreitung
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object(i))
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            End Try
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object(i))
        Next
        Dim disp2 As New DisplayData1(AddressOf Me.display_event_get_single_distance)
        Me._GUI.Invoke(disp2, data_object(3))

        ' Comport freigeben
        Me._isComReady = True
    End Sub
    Private Sub display_event_get_single_distance(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_single_distance_ready(data_object)
    End Sub
#End Region
#Region "Richtungsmessung"
    ''' <summary>
    ''' Winkel mit allen Informationen auslesen, Geocom: TMC_GetAngle1
    ''' </summary>
    ''' <param name="mode">
    ''' Inclination sensor measurement mode
    ''' 0 - Use sensor (apriori sigma) - slow
    ''' 1 - Automatic mode (sensor/plane)
    ''' 2 - Use plane - fast
    ''' Geocom-Chapter: Sensor measurement programs
    ''' </param>
    ''' <remarks></remarks>
    Public Sub get_Angle_All(ByVal mode As Long, ByRef data_object As LeicaTPS1000data)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        'Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 2003
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(mode))

        Dim backgroundThread As New Thread(AddressOf thread_get_Angle_All)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_Angle_All(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_get_Angle_All)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
    Private Sub display_event_get_Angle_All(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_Angle_All_ready(data_object)
    End Sub
#End Region
#Region "Winkel und Distanz"
    ''' <summary>
    ''' Messung der Winkel und der Strecke.
    ''' </summary>
    ''' <param name="tmc_measurement_mode">
    ''' TMC_STOP = 0, // Stop measurement program
    ''' TMC_DEF_DIST = 1, // Default DIST-measurement program
    ''' TMC_CLEAR = 3, // TMC_STOP and clear data
    ''' TMC_SIGNAL = 4, // Signal measurement (test function)
    ''' TMC_DO_MEASURE = 6, // (Re)start measurement task
    ''' TMC_RTRK_DIST = 8, // Distance-TRK measurement program
    ''' TMC_RED_TRK_DIST = 10, // Reflectorless tracking
    ''' TMC_FREQUENCY = 11 // Frequency measurement (test)
    ''' </param>
    ''' <param name="edm_measurement_mode">
    ''' EDM_MODE_NOT_USED = 0, // Init value
    ''' EDM_SINGLE_TAPE = 1, // IR Standard Reflector Tape
    ''' EDM_SINGLE_STANDARD = 2, // IR Standard
    ''' EDM_SINGLE_FAST = 3, // IR Fast
    ''' EDM_SINGLE_LRANGE = 4, // LO Standard
    ''' EDM_SINGLE_SRANGE = 5, // RL Standard
    ''' EDM_CONT_STANDARD = 6, // Standard repeated measurement
    ''' EDM_CONT_DYNAMIC = 7, // IR Tacking EDM_CONT_REFLESS = 8, // RL Tracking
    ''' EDM_CONT_FAST = 9, // Fast repeated measurement
    ''' EDM_AVERAGE_IR = 10,// IR Average
    ''' EDM_AVERAGE_SR = 11,// RL Average
    ''' EDM_AVERAGE_LR = 12, // LO Average
    ''' EDM_PRECISE_IR = 13, // IR Precise (TS30, TM30)
    ''' EDM_PRECISE_TAPE = 14 // IR Precise Reflector Tape (TS30, TM30)
    ''' </param>
    ''' <param name="inclination_mode">
    ''' TMC_MEA_INC = 0, // Use sensor (apriori sigma)
    ''' TMC_AUTO_INC = 1, // Automatic mode (sensor/plane)
    ''' TMC_PLANE_INC = 2, // Use plane (apriori sigma
    ''' </param>
    ''' <remarks>
    ''' Zur genauen Beschreibung der Parameter bitte im Geocomhandbuch nachschauen.
    ''' Beispiele:
    ''' IR (1,2,1)
    ''' RL (10,5,1)
    ''' </remarks>
    Public Sub get_Angle_And_Distance(ByVal tmc_measurement_mode As Long, Optional ByVal edm_measurement_mode As Long = 2, Optional ByVal inclination_mode As Long = 1)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False


        ' Es handelt sich hierbei um eine Kombination von Streckenmessungen und dann eine Winkelmessung.
        ' Nur so hat man die volle Optionsvielfalt, d.h. EDM und Kompensatoreinstellungen.

        Dim data_object(6) As LeicaTPS1000data

        Dim index As Integer

        ' Stellt den EDM-Mode ein: 
        index = 0
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2020
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(edm_measurement_mode))

        ' Stopt Messungnen und löscht die Daten:
        index = 1
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3), CStr(1))

        ' Führt die Streckenmessung durch, mit dem jeweiligen Messtyp (z.B. IR oder RL) und dem Konpensator
        index = 2
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(tmc_measurement_mode), CStr(inclination_mode))

        ' Holt die Messergebnisse
        index = 3
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2108
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3000), CStr(inclination_mode))

        ' Stopt Messungnen und löscht die Daten: 
        index = 4
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2008
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(3), CStr(inclination_mode))

        ' Stellt den EDM-Mode auf Standard ein: 
        index = 5
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2020
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(2))

        ' Winkelmessung
        index = 6
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 2003
        data_object(index).tpsTrID = build_TransID()

        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID, CStr(inclination_mode))

        Dim backgroundThread As New Thread(AddressOf thread_get_Angle_And_Distance)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_Angle_And_Distance(ByVal data_object() As LeicaTPS1000data)
        Dim array_length As Integer
        array_length = data_object.Length

        Dim ergebnis_objekt As New LeicaTPS1000data

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object(i).errorcode = -50 ' Zeitüberschreitung
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object(i))
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            End Try
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object(i))
        Next

        'In dem Objekt 3 ist die Strecke, in Objekt 6 sind die Winkel:
        If (data_object(3).errorcode = 0 And data_object(6).errorcode = 0) Then
            ' Ist die Streckenmessung UND die Winkelmessung OK, dann:
            ergebnis_objekt.errorcode = 0

            ' Daten aus der Winkelmessung:
            ergebnis_objekt.measuretime = data_object(6).measuretime
            ergebnis_objekt.mesuresdate = data_object(6).mesuresdate

            ergebnis_objekt.horizontalrichtung = data_object(6).horizontalrichtung
            ergebnis_objekt.zenitwinkel = data_object(6).zenitwinkel()
            ergebnis_objekt.AngleAccuracy = data_object(6).AngleAccuracy()
            ergebnis_objekt.AngleTime = data_object(6).AngleTime()
            ergebnis_objekt.CrossIncline = data_object(6).CrossIncline()
            ergebnis_objekt.LengthIncline = data_object(6).LengthIncline()
            ergebnis_objekt.AccuracyIncline = data_object(6).AccuracyIncline()
            ergebnis_objekt.InclineTime = data_object(6).InclineTime()
            ergebnis_objekt.FaceDef = data_object(6).FaceDef()

            ' Daten aus der Streckenmessung:
            ergebnis_objekt.distanz = data_object(3).distanz()
        Else
            ' Fehler in den Messungen, dann:
            If (data_object(3).errorcode <> 0) Then ergebnis_objekt.errorcode = data_object(3).errorcode
            If (data_object(6).errorcode <> 0) Then ergebnis_objekt.errorcode = data_object(6).errorcode
        End If

        Dim disp2 As New DisplayData1(AddressOf Me.display_event_get_Angle_And_Distance)
        Me._GUI.Invoke(disp2, ergebnis_objekt)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
    Private Sub display_event_get_Angle_And_Distance(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_Angle_And_Distance_ready(data_object)
    End Sub


#End Region
#Region "Verfahre Absolut"
    ''' <summary>
    ''' Bewegt das Fernrohr zu einer bestimmten Position [gon]
    ''' </summary>
    ''' <param name="hz">
    ''' Soll-Hz-Richtung [gon]
    ''' </param>
    ''' <param name="v">
    ''' Soll-Zenitwinkel [gon]
    ''' </param>
    ''' <param name="AUT_POSMODE">
    ''' Positionierungsmode: 0 - fast positioning mode (Default), 1 - exact positioning mode  
    ''' </param>
    ''' <remarks></remarks>
    Public Sub move_Absolute_HzV(ByVal hz As Double, ByVal v As Double, Optional ByVal AUT_POSMODE As Integer = 0)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data

        Dim tmphz, tmpv As Double

        tmphz = Me._tachymeter.getAngle2Drive(hz)
        tmpv = Me._tachymeter.getAngle2Drive(v)

        data_object.CodeNo = 9027

        If (Me._tachymeter.isInRange(tmpv) = True) Then
            data_object.soll_horizontalrichtung = hz
            data_object.soll_zenitwinkel = v
            data_object.posmode = AUT_POSMODE

            Dim backgroundThread As New Thread(AddressOf thread_move_Absolute_HzV)
            backgroundThread.Start(data_object)
        Else
            data_object.errorcode = -70
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Me._GUI.display_move_Absolute_HzV(data_object)
        End If

    End Sub

    Private Sub thread_move_Absolute_HzV(ByVal soll As LeicaTPS1000data)

        Dim soll_hz_rad, soll_v_rad, ist_hz_rad, ist_v_rad As Double

        soll_hz_rad = Me._tachymeter.getAngle2Drive(soll.soll_horizontalrichtung)
        soll_v_rad = Me._tachymeter.getAngle2Drive(soll.soll_zenitwinkel)

        ' Vor dem Absoulten Verfahren sollte der Lockin ausgeschaltet werden, sonst könnten die gegeneinander arbeiten.
        Dim lockin_off As New LeicaTPS1000data
        lockin_off.CodeNo = 18007
        lockin_off.tpsTrID = build_TransID()
        lockin_off.requestString = Me._tachymeter.define_request(lockin_off.CodeNo, lockin_off.tpsTrID, CStr(0))

        Me._tachymeter.setRequestGeoComOnce(lockin_off.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            lockin_off.errorcode = -50 ' Zeitüberschreitung
            lockin_off.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            lockin_off.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, lockin_off)
        Catch ex As ThreadInterruptedException
            lockin_off.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(lockin_off)
            lockin_off.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            lockin_off.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, lockin_off)
        End Try


        ' Aktuelle Position des Fernrohrs
        Dim winkelmessung As New LeicaTPS1000data

        winkelmessung.CodeNo = 2003
        winkelmessung.tpsTrID = build_TransID()

        winkelmessung.requestString = Me._tachymeter.define_request(winkelmessung.CodeNo, winkelmessung.tpsTrID, "1")
        Me._tachymeter.setRequestGeoComOnce(winkelmessung.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            winkelmessung.errorcode = -50 ' Zeitüberschreitung
            winkelmessung.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            winkelmessung.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

            Dim disp1 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
            Me._GUI.Invoke(disp1, winkelmessung)
            Exit Sub
        Catch ex As ThreadInterruptedException
            winkelmessung.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(winkelmessung)
            winkelmessung.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            winkelmessung.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

            ist_hz_rad = Me._tachymeter.getAngle2Drive(winkelmessung.horizontalrichtung)
            ist_v_rad = Me._tachymeter.getAngle2Drive(winkelmessung.zenitwinkel)

            Dim disp2 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp2, winkelmessung)
        End Try

        If (Me._tachymeter.isFaceI(soll_v_rad) = Me._tachymeter.isFaceI(ist_v_rad)) Then
            'Lage der aktuellen Fernrohrposition ist identisch mit der Solllage

            soll.CodeNo = 9027
            soll.tpsTrID = build_TransID()

            'Direkt zur Solllage verfahren
            soll.requestString = Me._tachymeter.define_request(soll.CodeNo, soll.tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(soll.posmode))
            Me._tachymeter.setRequestGeoComOnce(soll.requestString, Thread.CurrentThread)

            Try
                Thread.Sleep(10000)
                soll.errorcode = -50 ' Zeitüberschreitung
                soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp3 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
                Me._GUI.Invoke(disp3, soll)
                Exit Sub
            Catch ex As ThreadInterruptedException
                soll.reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(soll)
                soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp4 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
                Me._GUI.Invoke(disp4, soll)
            End Try


        ElseIf (Me._tachymeter.isFaceI(soll_v_rad) <> Me._tachymeter.isFaceI(ist_v_rad)) Then
            'Lage der aktuellen Fernrohrposition ist NICHT identisch mit der Solllage
            Dim zwischenschritt As LeicaTPS1000data

            zwischenschritt = New LeicaTPS1000data
            zwischenschritt.CodeNo = 9027
            zwischenschritt.tpsTrID = build_TransID()

            'Erst das Fernrohr auf Zenitwinkel von 200 gon
            zwischenschritt.requestString = Me._tachymeter.define_request(zwischenschritt.CodeNo, zwischenschritt.tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(Me._tachymeter.getAngle2Drive(200.1)), ",", "."), 0)
            Me._tachymeter.setRequestGeoComOnce(zwischenschritt.requestString, Thread.CurrentThread)

            Try
                Thread.Sleep(10000)
                zwischenschritt.errorcode = -50 ' Zeitüberschreitung
                zwischenschritt.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                zwischenschritt.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp5 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
                Me._GUI.Invoke(disp5, zwischenschritt)
                Exit Sub
            Catch ex As ThreadInterruptedException
                zwischenschritt.reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(zwischenschritt)
                zwischenschritt.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                zwischenschritt.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp6 As New DisplayData1(AddressOf Me._GUI.dis_logger)
                Me._GUI.Invoke(disp6, zwischenschritt)
            End Try


            soll.CodeNo = 9027
            soll.tpsTrID = build_TransID()
            soll.requestString = Me._tachymeter.define_request(soll.CodeNo, soll.tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(soll.posmode))


            'Dann auf die Sollposition verfahren
            Me._tachymeter.setRequestGeoComOnce(soll.requestString, Thread.CurrentThread)

            Try
                Thread.Sleep(10000)
                soll.errorcode = -50 ' Zeitüberschreitung
                soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp7 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
                Me._GUI.Invoke(disp7, soll)
                Exit Sub
            Catch ex As ThreadInterruptedException
                soll.reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(soll)
                soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

                Dim disp8 As New DisplayData1(AddressOf Me.display_event_move_Absolute_HzV)
                Me._GUI.Invoke(disp8, soll)
            End Try
        End If
        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_move_Absolute_HzV(ByRef data_object As LeicaTPS1000data)
        'Comport freigeben
        Me._isComReady = True
        RaiseEvent move_Absolute_HzV_ready(data_object)
    End Sub

#End Region
#Region "Lagewechsel"
    ''' <summary>
    ''' Wechselt die Fernrohrlage. Berücksichtigung eines Okularaufbaus.
    ''' </summary>
    ''' <param name="AUT_POSMODE">
    ''' Positionierungsmode: 0 - fast positioning mode (Default), 1 - exact positioning mode
    ''' </param>
    ''' <remarks></remarks>
    Public Sub change_face(Optional ByVal AUT_POSMODE As Integer = 0)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.posmode = AUT_POSMODE
        Dim backgroundThread As New Thread(AddressOf thread_change_face)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_change_face(ByVal soll As LeicaTPS1000data)

        Dim soll_hz_rad, soll_v_rad As Double

        ' Aktuelle Position des Fernrohrs
        Dim winkelmessung As New LeicaTPS1000data
        winkelmessung = New LeicaTPS1000data
        winkelmessung.CodeNo = 2003
        winkelmessung.tpsTrID = build_TransID()

        winkelmessung.requestString = Me._tachymeter.define_request(winkelmessung.CodeNo, winkelmessung.tpsTrID, "1")
        Me._tachymeter.setRequestGeoComOnce(winkelmessung.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            winkelmessung.errorcode = -50 ' Zeitüberschreitung
            winkelmessung.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            winkelmessung.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me.display_event_change_face)
            Me._GUI.Invoke(disp1, winkelmessung)
            Exit Sub
        Catch ex As ThreadInterruptedException
            winkelmessung.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(winkelmessung)
            winkelmessung.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            winkelmessung.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

            Dim disp2 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp2, winkelmessung)
        End Try

        ' Berechnung der Position der 2. bzw. 1. Lage
        soll_hz_rad = Me._tachymeter.getAngle2Drive(winkelmessung.horizontalrichtung + 200.0)
        soll_v_rad = Me._tachymeter.getAngle2Drive(400.0 - winkelmessung.zenitwinkel)

        ' Fernrohr auf ein Zenitwinkel von 200.1 gon
        Dim zwischenschritt As New LeicaTPS1000data
        zwischenschritt.CodeNo = 9027
        zwischenschritt.tpsTrID = build_TransID()
        zwischenschritt.requestString = Me._tachymeter.define_request(zwischenschritt.CodeNo, zwischenschritt.tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(Me._tachymeter.getAngle2Drive(200.1)), ",", "."), 0)
        Me._tachymeter.setRequestGeoComOnce(zwischenschritt.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(10000)
            zwischenschritt.errorcode = -50 ' Zeitüberschreitung
            zwischenschritt.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            zwischenschritt.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp3 As New DisplayData1(AddressOf Me.display_event_change_face)
            Me._GUI.Invoke(disp3, zwischenschritt)
            Exit Sub
        Catch ex As ThreadInterruptedException
            zwischenschritt.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(zwischenschritt)
            zwischenschritt.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            zwischenschritt.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")

            Dim disp4 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp4, zwischenschritt)
        End Try

        ' Fernrohr auf die Sollposition
        soll.CodeNo = 9027
        soll.tpsTrID = build_TransID()
        soll.requestString = Me._tachymeter.define_request(soll.CodeNo, soll.tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(soll.posmode))
        Me._tachymeter.setRequestGeoComOnce(soll.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(10000)
            soll.errorcode = -50 ' Zeitüberschreitung
            soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp5 As New DisplayData1(AddressOf Me.display_event_change_face)
            Me._GUI.Invoke(disp5, soll)
            Exit Sub
        Catch ex As ThreadInterruptedException
            soll.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(soll)
            soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp6 As New DisplayData1(AddressOf Me.display_event_change_face)
            Me._GUI.Invoke(disp6, soll)
        End Try

        ' Comport freigeben
        Me._isComReady = True
        'RaiseEvent lagewechsel_fertig()
    End Sub

    Private Sub display_event_change_face(ByRef data_object As LeicaTPS1000data)
        RaiseEvent change_face_ready(data_object)
    End Sub
#End Region
#Region "Theodolit Infos abfragen"
    Public Sub theo_info()

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object(2) As LeicaTPS1000data
        Dim index As Integer

        index = 0
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 5004
        data_object(index).tpsTrID = build_TransID()
        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID)

        index = 1
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 5003
        data_object(index).tpsTrID = build_TransID()
        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID)

        index = 2
        data_object(index) = New LeicaTPS1000data
        data_object(index).CodeNo = 18006
        data_object(index).tpsTrID = build_TransID()
        data_object(index).requestString = Me._tachymeter.define_request(data_object(index).CodeNo, data_object(index).tpsTrID)

        Dim backgroundThread As New Thread(AddressOf thread_theo_info)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_theo_info(ByVal data_object() As LeicaTPS1000data)
        Dim array_length As Integer
        array_length = data_object.Length

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object(i).errorcode = -50 ' Zeitüberschreitung
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object(i))
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            End Try
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object(i))
        Next

        Dim theoinfos As New LeicaTPS1000data
        If (data_object(0).errorcode = 0 Or data_object(1).errorcode = 0) Then
            theoinfos.TheodoliteName = data_object(0).TheodoliteName
            theoinfos.TheodoliteNumber = data_object(1).TheodoliteNumber
            theoinfos.ATR_Status = data_object(2).ATR_Status
        Else
            theoinfos.errorcode = -99
        End If
        Dim disp2 As New DisplayData1(AddressOf Me.display_event_theo_info)
        Me._GUI.Invoke(disp2, theoinfos)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_theo_info(ByRef data_object As LeicaTPS1000data)
        RaiseEvent theo_info_ready(data_object)
    End Sub

#End Region
#Region "Gerät einschalten"
    Public Sub set_theo_on()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 111
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(1))

        Dim backgroundThread As New Thread(AddressOf thread_set_theo_on)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_set_theo_on(ByVal data_object As LeicaTPS1000data)
        Dim flag As Boolean = False
        Dim count As Integer = 0

        ' Das Gerät gibt beim Enschaltvorgang keine Antwort und gibt auch keine Antwort wenn der Einschaltvorgang fertig vollzogen ist.
        ' Ist das Gerät eingeschaltet und Betriebsbereit, dann wird auf diesen Befehl ein "OK" geantwortet.
        ' Es wird der Befehl jetzt einmal pro Sekunde an das Gerät gesendet. Wenn das Gerät ein "Ok" sendet, dann ist es betriebsbereit.
        ' Dies wird aber max. 25 wiederholt (=max. Wartezeit = 25s). Der TS30 z.b. benötigt relativ lang zum booten.

        Do
            Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(1000)
                data_object.errorcode = -50 ' Zeitüberschreitung
                data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object.reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object)
                data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                flag = True
            End Try
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
            count = count + 1
            If (count = 25) Then flag = True
        Loop While (flag = False)

        Dim disp As New DisplayData1(AddressOf Me.display_event_set_theo_on)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_theo_on(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_theo_on_ready(data_object)
    End Sub
#End Region
#Region "Gerät ausschalten"
    Public Sub set_theo_off()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 112
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(0))

        Dim backgroundThread As New Thread(AddressOf thread_set_theo_off)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_set_theo_off(ByVal data_object As LeicaTPS1000data)

        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)
        Try
            Thread.Sleep(5000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            'flag = True
        End Try

        Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
        Me._GUI.Invoke(disp1, data_object)


        Dim disp As New DisplayData1(AddressOf Me.display_event_set_theo_off)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_theo_off(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_theo_off_ready(data_object)
    End Sub
#End Region
#Region "Fragt den ATR-Status ab"
    Public Sub get_ATR_State()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 18006
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID)

        Dim backgroundThread As New Thread(AddressOf thread_get_ATR_State)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_ATR_State(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_get_ATR_State)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_get_ATR_State(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_ATR_State_ready(data_object)
    End Sub
#End Region
#Region "Setzt den ATR-Status"
    ''' <summary>
    ''' Aktiviert bzw. Deaktiviert den ATR Mode.
    ''' Diese Funktion aktiviert den ATR Mode nur, bei der nächsten Messung wird NICHT automatisch das Ziel gesucht,
    ''' dafür muss noch der Befehl FineAdjust durchgeführt werden. Ist ATR on und man ist nicht genau auf den Reflektor,
    ''' dann kommt bei der Streckenmessung die Fehlermeldung, dass nur Winkelmessung möglich ist.
    ''' </summary>
    ''' <param name="onoff">
    ''' 0 - off
    ''' 1 - on
    ''' </param>
    ''' <remarks>
    ''' Activates respectively deactivates the ATR mode.
    ''' Activate ATR mode: The ATR mode gets activated. If LOCK mode is on and the command is sent, then LOCK mode changes to ATR mode.
    ''' Deactivate ATR mode: The ATR mode gets deactivated. If LOCK mode is on and the command is sent, then LOCK mode stays on
    ''' This command is valid for automated instrument models only.
    ''' </remarks>
    Public Sub set_ATR_State(ByVal onoff As Long)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 18005
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(onoff))

        Dim backgroundThread As New Thread(AddressOf thread_set_ATR_State)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_set_ATR_State(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_set_ATR_State)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_ATR_State(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_ATR_State_ready(data_object)
    End Sub
#End Region
#Region "Fragt den FineAdjustMode ab"
    Public Sub get_Fine_Adjust_Mode()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 9030
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID)

        Dim backgroundThread As New Thread(AddressOf thread_get_Fine_Adjust_Mode)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_Fine_Adjust_Mode(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_get_Fine_Adjust_Mode)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_get_Fine_Adjust_Mode(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_Fine_Adjust_Mode_ready(data_object)
    End Sub
#End Region
#Region "Setzt den FineAdjustMode"
    ''' <summary>
    ''' Setzt den FineAdjustMode
    ''' </summary>
    ''' <param name="mode">
    ''' AUT_NORM_MODE = 0 // Angle tolerance
    ''' AUT_POINT_MODE = 1 // Point tolerance
    ''' AUT_DEFINE_MODE = 2 // System independent positioning // tolerance. Set with AUT_SetTol
    ''' </param>
    ''' <remarks>
    ''' This function sets the positioning tolerances (default values for both modes) relating the angle accuracy or the point accuracy for the fine adjust. This command is valid for all instruments, but has only effects for instruments equipped with ATR. If a target is very near or held by hand, it’s recommended to set the adjust-mode to AUT_POINT_MODE.
    ''' </remarks>
    Public Sub set_Fine_Adjust_Mode(ByVal mode As Long)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 9031
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(mode))

        Dim backgroundThread As New Thread(AddressOf thread_set_Fine_Adjust_Mode)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_set_Fine_Adjust_Mode(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_set_Fine_Adjust_Mode)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_Fine_Adjust_Mode(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_Fine_Adjust_Mode_ready(data_object)
    End Sub
#End Region
#Region "Fine Adjust"
    ''' <summary>
    ''' Start automatische Target Positionierung
    ''' </summary>
    ''' <param name="dSrchHz">
    ''' Search Range Hz-axis [gon]
    ''' </param>
    ''' <param name="dSrchv">
    ''' Search Range V-axis [gon]
    ''' </param>
    ''' <remarks></remarks>
    Public Sub fine_adjust(ByVal dSrchHz As Double, ByVal dSrchv As Double)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        ' Umrechnung in rad
        dSrchHz = Me._tachymeter.getAngle2Drive(dSrchHz)
        dSrchv = Me._tachymeter.getAngle2Drive(dSrchv)


        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 9037
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, Replace(CStr(dSrchHz), ",", "."), Replace(CStr(dSrchv), ",", "."))

        Dim backgroundThread As New Thread(AddressOf thread_fine_adjust)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_fine_adjust(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            'Wird innerhalb von 5s kein Reflektor gefunden, kommt vom Gerät ein Timeoutfehler.
            Thread.Sleep(10000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_fine_adjust)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
    Private Sub display_event_fine_adjust(ByRef data_object As LeicaTPS1000data)
        RaiseEvent fine_adjust_ready(data_object)
    End Sub
#End Region
#Region "LockIn"
    ''' <summary>
    ''' Startet das "target tracking"
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub lock_in()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 9013
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID)

        Dim backgroundThread As New Thread(AddressOf thread_lock_in)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_lock_in(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_lock_in)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
    Private Sub display_event_lock_in(ByRef data_object As LeicaTPS1000data)
        RaiseEvent lock_in_ready(data_object)
    End Sub
#End Region
#Region "Setzt den Lock Mode on/off"
    ''' <summary>
    ''' Lockmode ein oder anschalten. Das Target verfolgen mit Lockin.
    ''' </summary>
    ''' <param name="onoff">
    ''' 0 - off
    ''' 1 - on
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_Lock_Mode(ByVal onoff As Long)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 18007
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(onoff))

        Dim backgroundThread As New Thread(AddressOf thread_set_Lock_Mode)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_set_Lock_Mode(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_set_Lock_Mode)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_Lock_Mode(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_Lock_Mode_ready(data_object)
    End Sub
#End Region
#Region "Fragt den Lock mode ab"
    Public Sub get_Lock_Mode()
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 18008
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID)

        Dim backgroundThread As New Thread(AddressOf thread_get_Lock_Mode)
        backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_get_Lock_Mode(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_get_Lock_Mode)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_get_Lock_Mode(ByRef data_object As LeicaTPS1000data)
        RaiseEvent get_Lock_Mode_ready(data_object)
    End Sub
#End Region
#Region "electronic guide light einstellen"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="intensity">
    ''' EDM_EGLINTEN_OFF = 0, EDM_EGLINTEN_LOW = 1, EDM_EGLINTEN_MID = 2, EDM_EGLINTEN_HIGH = 3
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_electronic_guide_light(ByVal intensity As Long)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 1059
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(intensity))

        Dim backgroundThread As New Thread(AddressOf thread_set_electronic_guide_light)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_set_electronic_guide_light(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_set_electronic_guide_light)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_electronic_guide_light(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_electronic_guide_light_ready(data_object)
    End Sub

#End Region
#Region "Laserpointer on/off"
    ''' <summary>
    ''' Schaltet den Laserpointer ein/aus
    ''' </summary>
    ''' <param name="onoff">
    ''' 0 - off
    ''' 1 - on
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_Laserpointer(ByVal onoff As Long)
        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim data_object As New LeicaTPS1000data
        data_object.CodeNo = 1004
        data_object.tpsTrID = build_TransID()
        data_object.requestString = Me._tachymeter.define_request(data_object.CodeNo, data_object.tpsTrID, CStr(onoff))

        Dim backgroundThread As New Thread(AddressOf thread_set_Laserpointer)
        backgroundThread.Start(data_object)
    End Sub
    Private Sub thread_set_Laserpointer(ByVal data_object As LeicaTPS1000data)
        Me._tachymeter.setRequestGeoComOnce(data_object.requestString, Thread.CurrentThread)

        Try
            Thread.Sleep(2000)
            data_object.errorcode = -50 ' Zeitüberschreitung
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
        Catch ex As ThreadInterruptedException
            data_object.reply = Me._tachymeter.antwortstring
            Me._tachymeter.analyse_reply(data_object)
            data_object.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Dim disp1 As New DisplayData1(AddressOf Me._GUI.dis_logger)
            Me._GUI.Invoke(disp1, data_object)
        End Try
        Dim disp As New DisplayData1(AddressOf Me.display_event_set_Laserpointer)
        Me._GUI.BeginInvoke(disp, data_object)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Private Sub display_event_set_Laserpointer(ByRef data_object As LeicaTPS1000data)
        RaiseEvent set_Laserpointer_ready(data_object)
    End Sub
#End Region

    Public Sub change_vertical_range(ByVal lage1 As Double, ByVal lage2 As Double)
        Dim tmplage1, tmplage2 As Double
        tmplage1 = Me._tachymeter.getAngle2Drive(lage1)
        tmplage2 = Me._tachymeter.getAngle2Drive(lage2)
        Me._tachymeter.def_zenit_range(tmplage1, tmplage2)
    End Sub

#Region "Selbstkalibrierung"
    ''' <summary>
    ''' Messung der Selbstkalibrierung 1
    ''' </summary>
    ''' <param name="n"></param>
    ''' <param name="HzMitte"></param>
    ''' <param name="ZdMitte"></param>
    ''' <param name="HzRand"></param>
    ''' <param name="ZdRand"></param>
    ''' <remarks>
    ''' Kein Backgroundthread! Die Durchführung findet im Haupttread statt.
    ''' </remarks>
    Public Sub sk1_messen(ByVal n As Integer, ByVal HzMitte As Double, ByVal ZdMitte As Double, ByVal HzRand As Double, ByVal ZdRand As Double)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        Dim sollpos(n - 1) As LeicaTPS1000data

        For i = 0 To n - 1 Step 1
            sollpos(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte, ZdMitte, HzRand, ZdRand)
            sollpos(i).CodeNo = 9027
            sollpos(i).soll_horizontalrichtung = soll(1, 1)
            sollpos(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos(i).soll_zenitwinkel)
            sollpos(i).posmode = 0
            sollpos(i).tpsTrID = build_TransID()
            sollpos(i).requestString = Me._tachymeter.define_request(sollpos(i).CodeNo, sollpos(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos(i).posmode))
        Next

        Dim data_object((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object(j) = sollpos(j / 2)

            data_object(j + 1) = New LeicaTPS1000data
            data_object(j + 1).CodeNo = 2003
            data_object(j + 1).tpsTrID = build_TransID()
            data_object(j + 1).requestString = Me._tachymeter.define_request(data_object(j + 1).CodeNo, data_object(j + 1).tpsTrID, CStr(0))

        Next


        thread_sk1_messen(HzMitte, ZdMitte, data_object)
    End Sub

    Private Sub thread_sk1_messen(ByVal hzmitte As Double, ByVal zdmitte As Double, ByVal data_object() As LeicaTPS1000data)

        ' Zuerst verfährt das Gerät auf Mitte, damit wird verhindert, dass die Kamera anschlägt.
        Dim data_object_soll As New LeicaTPS1000data

        Dim tmphz, tmpv As Double

        tmphz = Me._tachymeter.getAngle2Drive(hzmitte)
        tmpv = Me._tachymeter.getAngle2Drive(zdmitte)

        data_object_soll.CodeNo = 9027

        If (Me._tachymeter.isInRange(tmpv) = True) Then
            data_object_soll.soll_horizontalrichtung = hzmitte
            data_object_soll.soll_zenitwinkel = zdmitte
            data_object_soll.posmode = 0

            thread_move_Absolute_HzV(data_object_soll)
        Else
            data_object_soll.errorcode = -70
            data_object_soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object_soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Me._GUI.display_move_Absolute_HzV(data_object_soll)
        End If


        Dim array_length As Integer
        array_length = data_object.Length
        Dim n As Integer = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object(i).errorcode = -50 ' Zeitüberschreitung
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object(i))
                data_object(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object(i), 1, 1)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next
        Dim disp2 As New DisplayData0(AddressOf Me._GUI.display_sk_fertig)
        Me._GUI.Invoke(disp2)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Public Sub sk2_messen(ByVal n As Integer, ByVal HzMitte As Double, ByVal ZdMitte As Double, ByVal HzRand As Double, ByVal ZdRand As Double)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        ' Target 1 Lage 1
        Dim sollpos1(n - 1) As LeicaTPS1000data

        For i = 0 To n - 1 Step 1
            sollpos1(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte, ZdMitte, HzRand, ZdRand)
            sollpos1(i).CodeNo = 9027
            sollpos1(i).soll_horizontalrichtung = soll(1, 1)
            sollpos1(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos1(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos1(i).soll_zenitwinkel)
            sollpos1(i).posmode = 0
            sollpos1(i).tpsTrID = build_TransID()
            sollpos1(i).requestString = Me._tachymeter.define_request(sollpos1(i).CodeNo, sollpos1(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos1(i).posmode))
        Next

        Dim data_object1((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object1(j) = sollpos1(j / 2)

            data_object1(j + 1) = New LeicaTPS1000data
            data_object1(j + 1).CodeNo = 2003
            data_object1(j + 1).tpsTrID = build_TransID()
            data_object1(j + 1).requestString = Me._tachymeter.define_request(data_object1(j + 1).CodeNo, data_object1(j + 1).tpsTrID, CStr(0))

        Next

        ' Target 1 Lage 2
        Dim sollpos2(n - 1) As LeicaTPS1000data
        Dim HzMitte_Lage2 As Double, ZdMitte_Lage2 As Double, HzRand_Lage2 As Double, ZdRand_Lage2 As Double

        HzMitte_Lage2 = WinkelNachVollkreisGon(HzMitte + 200.0)
        ZdMitte_Lage2 = WinkelNachVollkreisGon(400.0 - ZdMitte)

        HzRand_Lage2 = WinkelNachVollkreisGon(HzRand + 200.0)
        ZdRand_Lage2 = WinkelNachVollkreisGon(400.0 - ZdRand)

        For i = 0 To n - 1 Step 1
            sollpos2(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte_Lage2, ZdMitte_Lage2, HzRand_Lage2, ZdRand_Lage2)
            sollpos2(i).CodeNo = 9027
            sollpos2(i).soll_horizontalrichtung = soll(1, 1)
            sollpos2(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos2(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos2(i).soll_zenitwinkel)
            sollpos2(i).posmode = 0
            sollpos2(i).tpsTrID = build_TransID()
            sollpos2(i).requestString = Me._tachymeter.define_request(sollpos2(i).CodeNo, sollpos2(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos2(i).posmode))
        Next

        Dim data_object2((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object2(j) = sollpos2(j / 2)

            data_object2(j + 1) = New LeicaTPS1000data
            data_object2(j + 1).CodeNo = 2003
            data_object2(j + 1).tpsTrID = build_TransID()
            data_object2(j + 1).requestString = Me._tachymeter.define_request(data_object2(j + 1).CodeNo, data_object2(j + 1).tpsTrID, CStr(0))

        Next


        thread_sk2_messen(HzMitte, ZdMitte, data_object1, data_object2)
        'Dim backgroundThread As New Thread(AddressOf thread_selbstkalibrierung)
        'backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_sk2_messen(ByVal hzmitte As Double, ByVal zdmitte As Double, ByVal data_object1() As LeicaTPS1000data, _
                                  ByVal data_object2() As LeicaTPS1000data)

        ' Zuerst verfährt das Gerät auf Mitte, damit wird verhindert, dass die Kamera anschlägt.
        Dim data_object_soll As New LeicaTPS1000data

        Dim tmphz, tmpv As Double

        tmphz = Me._tachymeter.getAngle2Drive(hzmitte)
        tmpv = Me._tachymeter.getAngle2Drive(zdmitte)

        data_object_soll.CodeNo = 9027

        If (Me._tachymeter.isInRange(tmpv) = True) Then
            data_object_soll.soll_horizontalrichtung = hzmitte
            data_object_soll.soll_zenitwinkel = zdmitte
            data_object_soll.posmode = 0 ' Schnell

            thread_move_Absolute_HzV(data_object_soll)
        Else
            data_object_soll.errorcode = -70
            data_object_soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object_soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Me._GUI.display_move_Absolute_HzV(data_object_soll)
        End If


        Dim array_length As Integer

        ' Ziel 1 Lage 1
        array_length = data_object1.Length
        Dim n As Integer = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object1(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object1(i).errorcode = -50 ' Zeitüberschreitung
                data_object1(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object1(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object1(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object1(i))
                data_object1(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object1(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object1(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object1(i), 1, 1)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next

        ' Lagewechsel
        Dim data_object_soll2 As New LeicaTPS1000data
        data_object_soll2.posmode = 0   'Schnell
        Me.thread_change_face(data_object_soll2)

        ' Ziel 1 Lage 2
        array_length = data_object2.Length
        n = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object2(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object2(i).errorcode = -50 ' Zeitüberschreitung
                data_object2(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object2(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object2(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object2(i))
                data_object2(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object2(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object2(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object2(i), 1, 2)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next


        Dim disp2 As New DisplayData0(AddressOf Me._GUI.display_sk_fertig)
        Me._GUI.Invoke(disp2)

        ' Comport freigeben
        Me._isComReady = True
    End Sub

    Public Sub sk4_messen(ByVal n As Integer, ByVal HzMitte1 As Double, ByVal ZdMitte1 As Double, ByVal HzRand1 As Double, ByVal ZdRand1 As Double, _
                                              ByVal HzMitte2 As Double, ByVal ZdMitte2 As Double, ByVal HzRand2 As Double, ByVal ZdRand2 As Double)

        ' Wird der Comport benutzt?, dann geht das Programm in einer Warteschleife bis der Comport wieder frei ist
        If (Me._isComReady = False) Then
            Me.comlocker()
        End If
        ' Comport wird benutzt
        Me._isComReady = False

        ' Target 1 Lage 1
        Dim sollpos1(n - 1) As LeicaTPS1000data

        For i = 0 To n - 1 Step 1
            sollpos1(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte1, ZdMitte1, HzRand1, ZdRand1)
            sollpos1(i).CodeNo = 9027
            sollpos1(i).soll_horizontalrichtung = soll(1, 1)
            sollpos1(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos1(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos1(i).soll_zenitwinkel)
            sollpos1(i).posmode = 0
            sollpos1(i).tpsTrID = build_TransID()
            sollpos1(i).requestString = Me._tachymeter.define_request(sollpos1(i).CodeNo, sollpos1(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos1(i).posmode))
        Next

        Dim data_object1((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object1(j) = sollpos1(j / 2)

            data_object1(j + 1) = New LeicaTPS1000data
            data_object1(j + 1).CodeNo = 2003
            data_object1(j + 1).tpsTrID = build_TransID()
            data_object1(j + 1).requestString = Me._tachymeter.define_request(data_object1(j + 1).CodeNo, data_object1(j + 1).tpsTrID, CStr(0))

        Next

        ' Target 1 Lage 2
        Dim sollpos2(n - 1) As LeicaTPS1000data
        Dim HzMitte1_Lage2 As Double, ZdMitte1_Lage2 As Double, HzRand1_Lage2 As Double, ZdRand1_Lage2 As Double

        HzMitte1_Lage2 = WinkelNachVollkreisGon(HzMitte1 + 200.0)
        ZdMitte1_Lage2 = WinkelNachVollkreisGon(400.0 - ZdMitte1)

        HzRand1_Lage2 = WinkelNachVollkreisGon(HzRand1 + 200.0)
        ZdRand1_Lage2 = WinkelNachVollkreisGon(400.0 - ZdRand1)

        For i = 0 To n - 1 Step 1
            sollpos2(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte1_Lage2, ZdMitte1_Lage2, HzRand1_Lage2, ZdRand1_Lage2)
            sollpos2(i).CodeNo = 9027
            sollpos2(i).soll_horizontalrichtung = soll(1, 1)
            sollpos2(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos2(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos2(i).soll_zenitwinkel)
            sollpos2(i).posmode = 0
            sollpos2(i).tpsTrID = build_TransID()
            sollpos2(i).requestString = Me._tachymeter.define_request(sollpos2(i).CodeNo, sollpos2(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos2(i).posmode))
        Next

        Dim data_object2((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object2(j) = sollpos2(j / 2)

            data_object2(j + 1) = New LeicaTPS1000data
            data_object2(j + 1).CodeNo = 2003
            data_object2(j + 1).tpsTrID = build_TransID()
            data_object2(j + 1).requestString = Me._tachymeter.define_request(data_object2(j + 1).CodeNo, data_object2(j + 1).tpsTrID, CStr(0))

        Next


        ' Target 2 Lage 1
        Dim sollpos3(n - 1) As LeicaTPS1000data

        For i = 0 To n - 1 Step 1
            sollpos3(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte2, ZdMitte2, HzRand2, ZdRand2)
            sollpos3(i).CodeNo = 9027
            sollpos3(i).soll_horizontalrichtung = soll(1, 1)
            sollpos3(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos3(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos3(i).soll_zenitwinkel)
            sollpos3(i).posmode = 0
            sollpos3(i).tpsTrID = build_TransID()
            sollpos3(i).requestString = Me._tachymeter.define_request(sollpos3(i).CodeNo, sollpos3(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos3(i).posmode))
        Next

        Dim data_object3((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object3(j) = sollpos3(j / 2)

            data_object3(j + 1) = New LeicaTPS1000data
            data_object3(j + 1).CodeNo = 2003
            data_object3(j + 1).tpsTrID = build_TransID()
            data_object3(j + 1).requestString = Me._tachymeter.define_request(data_object3(j + 1).CodeNo, data_object3(j + 1).tpsTrID, CStr(0))

        Next

        ' Target 2 Lage 2
        Dim sollpos4(n - 1) As LeicaTPS1000data
        Dim HzMitte2_Lage2 As Double, ZdMitte2_Lage2 As Double, HzRand2_Lage2 As Double, ZdRand2_Lage2 As Double

        HzMitte2_Lage2 = WinkelNachVollkreisGon(HzMitte2 + 200.0)
        ZdMitte2_Lage2 = WinkelNachVollkreisGon(400.0 - ZdMitte2)

        HzRand2_Lage2 = WinkelNachVollkreisGon(HzRand2 + 200.0)
        ZdRand2_Lage2 = WinkelNachVollkreisGon(400.0 - ZdRand2)

        For i = 0 To n - 1 Step 1
            sollpos4(i) = New LeicaTPS1000data
            Dim soll(1, 2) As Double
            Dim soll_hz_rad, soll_v_rad As Double
            soll = KalibrierRasterRichtung(n, i, HzMitte2_Lage2, ZdMitte2_Lage2, HzRand2_Lage2, ZdRand2_Lage2)
            sollpos4(i).CodeNo = 9027
            sollpos4(i).soll_horizontalrichtung = soll(1, 1)
            sollpos4(i).soll_zenitwinkel = soll(1, 2)
            soll_hz_rad = Me._tachymeter.getAngle2Drive(sollpos4(i).soll_horizontalrichtung)
            soll_v_rad = Me._tachymeter.getAngle2Drive(sollpos4(i).soll_zenitwinkel)
            sollpos4(i).posmode = 0
            sollpos4(i).tpsTrID = build_TransID()
            sollpos4(i).requestString = Me._tachymeter.define_request(sollpos4(i).CodeNo, sollpos4(i).tpsTrID, Replace(CStr(soll_hz_rad), ",", "."), Replace(CStr(soll_v_rad), ",", "."), CStr(sollpos4(i).posmode))
        Next

        Dim data_object4((n * 2) - 1) As LeicaTPS1000data

        For j = 0 To ((n * 2) - 1) Step 2
            data_object4(j) = sollpos4(j / 2)

            data_object4(j + 1) = New LeicaTPS1000data
            data_object4(j + 1).CodeNo = 2003
            data_object4(j + 1).tpsTrID = build_TransID()
            data_object4(j + 1).requestString = Me._tachymeter.define_request(data_object4(j + 1).CodeNo, data_object4(j + 1).tpsTrID, CStr(0))

        Next

        thread_sk4_messen(HzMitte1, ZdMitte1, data_object1, data_object2, HzMitte2, ZdMitte2, data_object3, data_object4)
        'Dim backgroundThread As New Thread(AddressOf thread_selbstkalibrierung)
        'backgroundThread.Start(data_object)
    End Sub

    Private Sub thread_sk4_messen(ByVal hzmitte1 As Double, ByVal zdmitte1 As Double, ByVal data_object1() As LeicaTPS1000data, _
                                  ByVal data_object2() As LeicaTPS1000data, _
                                  ByVal hzmitte2 As Double, ByVal zdmitte2 As Double, ByVal data_object3() As LeicaTPS1000data, _
                                  ByVal data_object4() As LeicaTPS1000data)

        ' Zuerst verfährt das Gerät auf Mitte, damit wird verhindert, dass die Kamera anschlägt.
        Dim data_object_soll As New LeicaTPS1000data

        Dim tmphz, tmpv As Double

        tmphz = Me._tachymeter.getAngle2Drive(hzmitte1)
        tmpv = Me._tachymeter.getAngle2Drive(zdmitte1)

        data_object_soll.CodeNo = 9027

        If (Me._tachymeter.isInRange(tmpv) = True) Then
            data_object_soll.soll_horizontalrichtung = hzmitte1
            data_object_soll.soll_zenitwinkel = zdmitte1
            data_object_soll.posmode = 0 ' Schnell

            thread_move_Absolute_HzV(data_object_soll)
        Else
            data_object_soll.errorcode = -70
            data_object_soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object_soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Me._GUI.display_move_Absolute_HzV(data_object_soll)
        End If


        Dim array_length As Integer

        ' Ziel 1 Lage 1
        array_length = data_object1.Length
        Dim n As Integer = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object1(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object1(i).errorcode = -50 ' Zeitüberschreitung
                data_object1(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object1(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object1(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object1(i))
                data_object1(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object1(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object1(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object1(i), 1, 1)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next

        ' Lagewechsel
        Dim data_object_soll2 As New LeicaTPS1000data
        data_object_soll2.posmode = 0   'Schnell
        Me.thread_change_face(data_object_soll2)

        ' Ziel 1 Lage 2
        array_length = data_object2.Length
        n = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object2(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object2(i).errorcode = -50 ' Zeitüberschreitung
                data_object2(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object2(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object2(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object2(i))
                data_object2(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object2(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object2(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object2(i), 1, 2)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next


        ' Zuerst verfährt das Gerät auf Mitte des zweiten Ziel, damit wird verhindert, dass die Kamera anschlägt.
        tmphz = Me._tachymeter.getAngle2Drive(hzmitte2)
        tmpv = Me._tachymeter.getAngle2Drive(zdmitte2)

        data_object_soll.CodeNo = 9027

        If (Me._tachymeter.isInRange(tmpv) = True) Then
            data_object_soll.soll_horizontalrichtung = hzmitte2
            data_object_soll.soll_zenitwinkel = zdmitte2
            data_object_soll.posmode = 0 ' Schnell

            thread_move_Absolute_HzV(data_object_soll)
        Else
            data_object_soll.errorcode = -70
            data_object_soll.measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
            data_object_soll.mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Me._GUI.display_move_Absolute_HzV(data_object_soll)
        End If


        ' Ziel 2 Lage 1
        array_length = data_object1.Length
        n = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object3(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object3(i).errorcode = -50 ' Zeitüberschreitung
                data_object3(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object3(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object3(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object3(i))
                data_object3(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object3(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object3(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object3(i), 2, 1)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next

        ' Lagewechsel
        data_object_soll2.posmode = 0   'Schnell
        Me.thread_change_face(data_object_soll2)

        ' Ziel 2 Lage 2
        array_length = data_object4.Length
        n = 0

        For i = 0 To array_length - 1 Step 1
            Me._tachymeter.setRequestGeoComOnce(data_object4(i).requestString, Thread.CurrentThread)
            Try
                Thread.Sleep(5000)
                data_object4(i).errorcode = -50 ' Zeitüberschreitung
                data_object4(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object4(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
            Catch ex As ThreadInterruptedException
                data_object4(i).reply = Me._tachymeter.antwortstring
                Me._tachymeter.analyse_reply(data_object4(i))
                data_object4(i).measuretime = DateTime.Now.ToString("HH:mm:ss:fff")
                data_object4(i).mesuresdate = DateTime.Now.ToString("dd.MM.yyyy")
                If (data_object4(i).CodeNo = 2003) Then
                    n += 1
                    'Bild und Strichkreuzerkennung:
                    Me._GUI.bild_selbstkalibrierung(data_object4(i), 2, 2)
                End If
            End Try
            'Dim disp1 As New DisplayData(AddressOf Me._GUI.dis_logger)
            'Me._GUI.Invoke(disp1, data_object(i))
        Next


        Dim disp2 As New DisplayData0(AddressOf Me._GUI.display_sk_fertig)
        Me._GUI.Invoke(disp2)

        ' Comport freigeben
        Me._isComReady = True
    End Sub
#End Region


End Class
