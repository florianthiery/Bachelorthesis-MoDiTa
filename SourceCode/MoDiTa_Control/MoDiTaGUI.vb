Imports System.IO
Imports HalconDotNet

Public Class MoDiTaGUI
    Private _camera As ControluEye

    'Resolutiontest
    Private _resolutiontest As New resolutiontest

    '#tmp_uEye: Diese Variable dienen zur Nutzung der der uEye dll. Wird dies nicht gebraucht, können diese gelöscht werden.
    Private _camera_uEye As uEyeCamera
    Private _uEye_dll_activatet As Boolean = False

    Private _int_zoomfacor As Integer = 1

    Private _int_imageheigth As Integer
    Private _int_imagewidth As Integer

    Private _measureID As Integer 'Diese ID darf nur einmal auftauchen, dient zur Verknüpfung zwischen Bilddaten und Messungen

    Private _isTachymeterConnect As Boolean = False
    Private _isCameraConnect As Boolean = False

    Private _tachymeter As LeicaTPS1000
    Private _tachymetercontrol As ControlLeicaTPS1000

    Private _messdaten As Punktliste

    Private _data_table As New DataTable
    Private _log_table As New DataTable

    Private _project_dir As String
    Private _data_file_name As String
    Private _log_file_name As String

    Private _kalibrierung As Calibration

    Private _selfcalibration_hz_lage1_target1_center As Double, _selfcalibration_v_lage1_target1_center As Double

    Private _selfcalibration_hz_lage1_target1_circle As Double, _selfcalibration_v_lage1_target1_circle As Double

    Private _selfcalibration_hz_lage1_target2_center As Double, _selfcalibration_v_lage1_target2_center As Double

    Private _selfcalibration_hz_lage1_target2_circle As Double, _selfcalibration_v_lage1_target2_circle As Double

    Private Delegate Sub DisplaySKData(ByVal data As Messpunkt)


    Private Sub MoDiTaGUI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me._HWindow = HWindowControl1.HalconWindow
        Me.setCameraButtons("disconnected")
        Me._camera = New ControluEye(Me)

        For Each COMString As String In My.Computer.Ports.SerialPortNames ' Load all available COM ports.
            cmbComports.Items.Add(COMString)
        Next

        Me._tachymeter = New LeicaTPS1000()
        Me.setTachyButtons("disconnected")

        ' Datentable
        Me._data_table.Columns.Add(New DataColumn("ID"))
        Me._data_table.Columns.Add(New DataColumn("Datum"))
        Me._data_table.Columns.Add(New DataColumn("Zeit"))
        Me._data_table.Columns.Add(New DataColumn("Punktnummer"))
        Me._data_table.Columns.Add(New DataColumn("Punktname"))
        Me._data_table.Columns.Add(New DataColumn("Horizontalrichtung"))
        Me._data_table.Columns.Add(New DataColumn("Zenitwinkel"))
        Me._data_table.Columns.Add(New DataColumn("Schrägstrecke"))
        Me._data_table.Columns.Add(New DataColumn("CrossInc"))
        Me._data_table.Columns.Add(New DataColumn("LengthInc"))
        Me._data_table.Columns.Add(New DataColumn("Errorstring"))
        Me._data_table.Columns.Add(New DataColumn("yc"))
        Me._data_table.Columns.Add(New DataColumn("xc"))

        Me._log_table.Columns.Add(New DataColumn("Datum"))
        Me._log_table.Columns.Add(New DataColumn("Zeit"))
        Me._log_table.Columns.Add(New DataColumn("Anfrage"))
        Me._log_table.Columns.Add(New DataColumn("Antwort"))
        Me._log_table.Columns.Add(New DataColumn("Errorstring"))

        ' Unterschiedliche Auflösungen für den Live-Modus und den Singleimage-Modus stellt sich als problematisch heraus:
        ' Einstellungen für Bildrate und Belichtungszeit ändern sich mit der Auflösungsänderung, hier sind noch Nachbesserungen in der
        ' Controllklasse notwendig.
        CheckBox_SameResolution.Enabled = False
        CheckBox_SameResolution.Checked = True


        ' Joystickbuttons
        btdown.Text = Chr(234)
        btup.Text = Chr(233)
        btleft.Text = Chr(231)
        btright.Text = Chr(232)
        btupleft.Text = Chr(235)
        btupright.Text = Chr(236)
        btdownleft.Text = Chr(237)
        btdownright.Text = Chr(238)

        cmbComports.Sorted = True

        projet_programm_start()
    End Sub

    '############################# Datenverwaltung #####################################

    Private Sub projet_programm_start()
        ' Pfad des Programms
        Dim sAppPath As String
        sAppPath = Application.StartupPath

        ' Pfad + Dateiname der Default.ini
        Dim inidatei As New INIDatei
        inidatei.Pfad = sAppPath + "\default.ini"

        ' Eintrag des letzten Projekts aus der ini holen
        Dim lastdir As String
        lastdir = inidatei.WertLesen("Start", "LastDir")

        Dim abfrage As New StartUp_Project()

        If (lastdir <> "") Then 'Ist ein Eintrag vorhanden
            abfrage.lastproject = True
        Else
            abfrage.lastproject = False
        End If

        If (abfrage.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
            If (abfrage.message = "last") Then
                Me.open_project(lastdir)
            ElseIf (abfrage.message = "open") Then
                Dim folder As New FolderBrowserDialog
                folder.ShowDialog()
                Me.open_project(folder.SelectedPath())
            ElseIf (abfrage.message = "new") Then
                Me.new_project()
            End If
        Else
            'Me.Close()
        End If
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenProjectToolStripMenuItem.Click
        Dim folder As New FolderBrowserDialog
        folder.ShowDialog()
        Me.open_project(folder.SelectedPath())
    End Sub

    Private Sub new_project()
        Dim folder As New FolderBrowserDialog
        folder.ShowDialog()

        ' Verzeichnisse anlegen
        Me._project_dir = folder.SelectedPath()
        Directory.CreateDirectory(Me._project_dir + "\image_measure")
        Directory.CreateDirectory(Me._project_dir + "\calibration")
        Directory.CreateDirectory(Me._project_dir + "\calibration\crosshair")

        Me._messdaten = New Punktliste
        Me._messdaten.CSV_File() = Me._project_dir + "\data.csv"
        Me.measureID = Me._messdaten.freeID

        ' Erzeugt die log.csv
        Dim log_csv_file As New CSVData
        Me._log_file_name = Me._project_dir + "\log.csv"
        log_csv_file.CSVDataTable = Me._log_table
        log_csv_file.SaveAsCSV(Me._log_file_name, True)
        log_csv_file.Dispose()
        DGVLog.DataSource = Me._log_table

        ' Letztes Projekt in die ini
        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dim inidatei As New INIDatei
        inidatei.Pfad = sAppPath + "\default.ini"
        inidatei.WertSchreiben("Start", "LastDir", Me._project_dir)
    End Sub

    Private Sub open_project(ByVal projectdir As String)
        Me._project_dir = projectdir

        ' Lese data.csv
        Me._messdaten = New Punktliste
        Me._messdaten.CSV_File() = Me._project_dir + "\data.csv"
        Me._messdaten.csv_To_punkte(Me._project_dir + "\data.csv")
        For i = 0 To Me._messdaten.freeID - 1 Step 1
            Me.messdaten_To_DataTable(Me._messdaten.get_punkt(i))
        Next
        Me.measureID = Me._messdaten.freeID

        ' Lese log.csv
        Dim log_csv_file As New CSVData
        Me._log_file_name = Me._project_dir + "\log.csv"
        log_csv_file.LoadCSV(Me._log_file_name, True, ";")
        Me._log_table = log_csv_file.CSVDataTable
        log_csv_file.Dispose()
        DGVLog.DataSource = Me._log_table

        ' Kalibrierung öffnen bzw. Transformationsdaten
        Me._kalibrierung = New Calibration
        Me._kalibrierung.import_calibration(Me._project_dir + "\calibration\")

        ' Letztes Projekt in die ini
        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dim inidatei As New INIDatei
        inidatei.Pfad = sAppPath + "\default.ini"
        inidatei.WertSchreiben("Start", "LastDir", Me._project_dir)

    End Sub

    Private Sub messdaten_To_DataTable(ByVal pkt As Messpunkt)
        ' Anzeige in der GUI
        Dim id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring As String
        id = pkt.ID.ToString
        datum = pkt.messdatum
        zeit = pkt.messzeit
        punktnummer = pkt.punktnummer.ToString
        punktname = pkt.punktname
        hz = pkt.horizontalrichtung.ToString
        zw = pkt.zenitwinkel.ToString
        d = pkt.distanz.ToString
        crossInc = pkt.crossInclination.ToString
        lengthInc = pkt.lengthInclination.ToString
        errorstring = pkt.tachymeter_errorstring

        Me._data_table.Rows.Add(id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring)
        DGVData.DataSource = Me._data_table

        Me.measureID() = Me._messdaten.freeID()
    End Sub

    Private Sub MoDiTaGUI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me._camera.abortLiveBild()
        If (Me._tachymeter.isComportopen() = True) Then
            Me._tachymeter.closeComport()
        End If
    End Sub

    Private WriteOnly Property measureID() As Integer
        Set(ByVal value As Integer)
            Me._measureID = value
            La_ID_Status.Text = Me._measureID.ToString
        End Set
    End Property

    Private Sub bu_export_log_as_csv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_export_log_as_csv.Click
        Dim export As New CsvExport
        Dim Filename As String = export.FileName(Me._project_dir)
        export.Export(Me._log_table, Filename, ";")
    End Sub

    Private Sub save_Image_To_Disk(ByVal filename As String, Optional ByVal type As String = "tiff")
        '#tmp_uEye:
        If (Me._uEye_dll_activatet = True) Then
            filename = filename + ".bmp"
            Me._camera_uEye.save_Image(filename)
        Else
            Dim Image As HImage
            filename = filename + ".tif"
            Image = Me._camera.makeSingleImage()
            Image.WriteImage(type, 0, filename)
        End If

    End Sub

    Public Sub dis_logger(ByRef daten As LeicaTPS1000data)
        Me.logger(daten)
    End Sub

    Private Sub logger(ByRef daten As LeicaTPS1000data)
        Dim datum, zeit, anfrage, antwort, errorstring As String

        datum = daten.mesuresdate
        zeit = daten.measuretime
        anfrage = daten.requestString.Trim
        antwort = daten.reply.Trim
        errorstring = daten.errorstring

        Me._log_table.Rows.Add(datum, zeit, anfrage, antwort, errorstring)
        DGVLog.DataSource = Me._log_table
    End Sub

    Private Sub save_point(ByRef daten As LeicaTPS1000data)

        ' Messdaten abspeichern und abspeichern in die CSV (wenn eingestellt)
        Dim pkt As New Messpunkt

        pkt.ID = Me._messdaten.freeID()
        pkt.messdatum = daten.mesuresdate
        pkt.messzeit = daten.measuretime
        ' Überprüfung ob Punktnummer vergeben und nummerisch ist
        If (tb_pointnumber.Text.Trim() <> String.Empty) Then
            If (IsNumeric(tb_pointnumber.Text.Trim())) Then
                pkt.punktnummer = CInt(tb_pointnumber.Text)
            End If
        End If
        pkt.punktname = tb_pointname.Text
        pkt.horizontalrichtung = daten.horizontalrichtung
        pkt.zenitwinkel = daten.zenitwinkel
        pkt.distanz = daten.distanz
        pkt.crossInclination = daten.CrossIncline
        pkt.lengthInclination = daten.LengthIncline
        pkt.tachymeter_errorstring = daten.errorstring
        pkt.bildkoordinate_X = daten.xc
        pkt.bildkoordinate_Y = daten.yc

        Me._messdaten.add_punkt(pkt)

        ' Anzeige in der GUI
        Dim id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring As String
        id = pkt.ID.ToString
        datum = daten.mesuresdate
        zeit = daten.measuretime
        punktnummer = tb_pointnumber.Text
        punktname = tb_pointname.Text
        hz = daten.horizontalrichtung.ToString
        zw = daten.zenitwinkel.ToString
        d = daten.distanz.ToString
        crossInc = daten.CrossIncline
        lengthInc = daten.LengthIncline
        errorstring = daten.errorstring

        Me._data_table.Rows.Add(id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring)
        DGVData.DataSource = Me._data_table

        Me.measureID() = Me._messdaten.freeID()

    End Sub

    '############################# Steuerung Tachymeter################################

#Region "Startprozedur, wenn der Comport zum Tachymeter geöffnet wurde"
    Private Sub cmbComports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComports.SelectedIndexChanged

        Me._tachymetercontrol = New ControlLeicaTPS1000(Me, Me._tachymeter)
        Me._tachymeter.openComport(cmbComports.Text)

        ' Schaltet Gerät an, danach theoinfos
        Me.changeTachyStatusSymbol(2)
        Me._tachymetercontrol.set_theo_on()
        AddHandler Me._tachymetercontrol.set_theo_on_ready, AddressOf start_theo_infos

    End Sub

    Public Sub start_theo_infos(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_theo_on_ready, AddressOf start_theo_infos

        If (data.errorcode = 0) Then
            ' Gerät wurde erfolgreich eingeschaltet und die Theoinfos können abgefragt werden:
            Me.changeTachyStatusSymbol(2)

            Me._tachymetercontrol.theo_info()
            AddHandler Me._tachymetercontrol.theo_info_ready, AddressOf display_theo_info
        Else
            Me.changeTachyStatusSymbol(0)
            display_tachy_errorcodes(data.errorstring)
        End If
    End Sub
    Public Sub display_theo_info(ByRef daten As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.theo_info_ready, AddressOf display_theo_info
        Me.changeTachyStatusSymbol(1)
        display_tachy_errorcodes(daten.errorstring)
        If (daten.errorcode = 0) Then
            Dim name, nummer As String
            name = Replace(CStr(daten.TheodoliteName), Chr(34), "")
            nummer = CStr(daten.TheodoliteNumber)
            La_theoinfos.Text = name + "  Sno.: " + nummer

            Me.setTachyButtons("connected")
            rb_vertical_range_camera.Checked = True 'Default
            Me._isTachymeterConnect = True
        End If
    End Sub
#End Region
#Region "Gerät einschalten (Button)"
    Private Sub Button_Tachymeter_on_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Tachymeter_on.Click
        Me.changeTachyStatusSymbol(2)
        AddHandler Me._tachymetercontrol.set_theo_on_ready, AddressOf Me.tachymeter_ist_an
        Me._tachymetercontrol.set_theo_on()
    End Sub

    Public Sub tachymeter_ist_an(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_theo_on_ready, AddressOf Me.tachymeter_ist_an
        If (data.errorcode = 0) Then
            Me.changeTachyStatusSymbol(2)
            Me._tachymetercontrol.theo_info()
            AddHandler Me._tachymetercontrol.theo_info_ready, AddressOf display_theo_info
        Else
            Me.changeTachyStatusSymbol(0)
            display_tachy_errorcodes(data.errorstring)
        End If
    End Sub
#End Region
#Region "Gerät ausschalten (Button)"
    Private Sub Button_Tachymeter_Off_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Tachymeter_Off.Click
        AddHandler Me._tachymetercontrol.set_theo_off_ready, AddressOf Me.tachymeter_ist_aus
        Me._tachymetercontrol.set_theo_off()
    End Sub

    Public Sub tachymeter_ist_aus(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_theo_off_ready, AddressOf Me.tachymeter_ist_aus
        display_tachy_errorcodes(data.errorstring)
        If (data.errorcode = 0) Then setTachyButtons("off")
    End Sub
#End Region
#Region "Winkelmessung"
    Private Sub Bu_Measure_Direction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_Measure_Direction.Click
        Me.changeTachyStatusSymbol(2)
        Dim data_object As New LeicaTPS1000data
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_All

        If (cb_Measure_saveImage.Checked = True) Then
            Dim filename As String
            filename = Me._project_dir + "\image_measure\" + Me._measureID.ToString
            Me.save_Image_To_Disk(filename, "tiff")
        End If
    End Sub

    Public Sub display_get_Angle_All(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_All
    End Sub
#End Region
#Region "Messung All"
    Private Sub Bu_Measure_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_Measure_All.Click
        ' Ist ATR und IR aktiviert?
        If (CheckBox_ATR_Activate.Checked = True And rb_edmMode_IR.Checked = True) Then
            If IsNumeric(TextBox_ATR_SearchRange_Hz.Text) And IsNumeric(TextBox_ATR_SearchRange_V.Text) Then
                Me.changeTachyStatusSymbol(2)
                AddHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.messung_all_starten
                Me._tachymetercontrol.fine_adjust(CDbl(TextBox_ATR_SearchRange_Hz.Text), CDbl(TextBox_ATR_SearchRange_V.Text))
            Else
                MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.changeTachyStatusSymbol(1)
            End If
        Else
            ' Messung ohne ATR oder Messung RL
            Dim tmpdata As New LeicaTPS1000data
            tmpdata.errorcode = 0
            ' Messung All wird direkt gestartet, tmpdata zeigt eine "erfolgreiche ATR" an 
            Me.messung_all_starten(tmpdata)
        End If
    End Sub
    Private Sub messung_all_starten(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.messung_all_starten

        If (data.errorcode = 0) Then ' ATR hat funktioniert:
            ' IR oder RL
            If (rb_edmMode_IR.Checked = True) Then
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.get_Angle_And_Distance(1)

                AddHandler Me._tachymetercontrol.get_Angle_And_Distance_ready, AddressOf Me.display_get_Angle_And_Distance

                If (cb_Measure_saveImage.Checked = True) Then
                    Dim filename As String
                    filename = Me._project_dir + "\image_measure\" + Me._measureID.ToString + ".tif"
                    Me.save_Image_To_Disk(filename, "tiff")
                End If
            ElseIf (rb_edmMode_RL.Checked = True) Then
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.get_Angle_And_Distance(10, 5)

                AddHandler Me._tachymetercontrol.get_Angle_And_Distance_ready, AddressOf Me.display_get_Angle_And_Distance

                If (cb_Measure_saveImage.Checked = True) Then
                    Dim filename As String
                    filename = Me._project_dir + "\image_measure\" + Me._measureID.ToString + ".tif"
                    Me.save_Image_To_Disk(filename, "tiff")
                End If
            End If
        Else
            display_tachy_errorcodes(data.errorstring)
            Me.changeTachyStatusSymbol(1)
        End If

        
    End Sub


    Public Sub display_get_Angle_And_Distance(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)
        RemoveHandler Me._tachymetercontrol.get_Angle_And_Distance_ready, AddressOf Me.display_get_Angle_And_Distance
    End Sub
#End Region
#Region "Messung nur Strecke"
    Private Sub Bu_Measure_Distance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_Measure_Distance.Click
        ' Ist ATR und IR aktiviert?
        If (CheckBox_ATR_Activate.Checked = True And rb_edmMode_IR.Checked = True) Then
            If IsNumeric(TextBox_ATR_SearchRange_Hz.Text) And IsNumeric(TextBox_ATR_SearchRange_V.Text) Then
                Me.changeTachyStatusSymbol(2)
                AddHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.streckenmessung
                Me._tachymetercontrol.fine_adjust(CDbl(TextBox_ATR_SearchRange_Hz.Text), CDbl(TextBox_ATR_SearchRange_V.Text))
            Else
                MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.changeTachyStatusSymbol(1)
            End If
        Else
            ' Messung ohne ATR oder Messung RL
            Dim tmpdata As New LeicaTPS1000data
            tmpdata.errorcode = 0
            ' Streckenmessung wird direkt gestartet, tmpdata zeigt eine "erfolgreiche ATR" an 
            Me.streckenmessung(tmpdata)
        End If

    End Sub

    Private Sub streckenmessung(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.streckenmessung

        If (data.errorcode = 0) Then ' ATR hat funktioniert:
            ' IR oder RL
            If (rb_edmMode_IR.Checked = True) Then
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.get_single_distance(1)

                AddHandler Me._tachymetercontrol.get_single_distance_ready, AddressOf Me.display_get_single_distance

                If (cb_Measure_saveImage.Checked = True) Then
                    Dim filename As String
                    filename = Me._project_dir + "\image_measure\" + Me._measureID.ToString + ".tif"
                    Me.save_Image_To_Disk(filename, "tiff")
                End If
            ElseIf (rb_edmMode_RL.Checked = True) Then
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.get_single_distance(10, 5)

                AddHandler Me._tachymetercontrol.get_single_distance_ready, AddressOf Me.display_get_single_distance

                If (cb_Measure_saveImage.Checked = True) Then
                    Dim filename As String
                    filename = Me._project_dir + "\image_measure\" + Me._measureID.ToString + ".tif"
                    Me.save_Image_To_Disk(filename, "tiff")
                End If
            End If
        Else
            display_tachy_errorcodes(data.errorstring)
            Me.changeTachyStatusSymbol(1)
        End If
    End Sub

    Public Sub display_get_single_distance(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)
        RemoveHandler Me._tachymetercontrol.get_single_distance_ready, AddressOf Me.display_get_single_distance
    End Sub
#End Region
#Region "Joystick"
    ''' <summary>
    ''' Zeigt gon/s in Label an (Hz).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzt folgende Funktionen:
    ''' Scroll2GonPerSec</remarks>
    Private Sub trbVelocityHz_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trbVelocityHz.Scroll
        'Dim local_math As New own_math
        lbVelocityHz.Text = "V(Hz) = " + ((Scroll2GonPerSec(CLng(trbVelocityHz.Value)))).ToString("0.0000 gon/s")
        If cbVelocityEqual.Checked = True Then
            trbVelocityVz.Value = trbVelocityHz.Value
            lbVelocityVz.Text = lbVelocityHz.Text
        End If
    End Sub

    ''' <summary>
    ''' Zeigt gon/s in Label an (Vz).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzt folgende Funktionen:
    ''' Scroll2GonPerSec</remarks>
    Private Sub trbVelocityVz_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trbVelocityVz.Scroll
        'Dim local_math As New own_math
        lbVelocityVz.Text = "V(Vz) = " + _
                    ((Scroll2GonPerSec(CLng(trbVelocityVz.Value)))).ToString("0.0000 gon/s")
        If cbVelocityEqual.Checked = True Then
            trbVelocityHz.Value = trbVelocityVz.Value
            lbVelocityHz.Text = lbVelocityVz.Text
        End If
    End Sub

    ''' <summary>
    ''' Setzt die Geschwindigkeiten von Hz und Vz gleich.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbVelocityEqual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVelocityEqual.CheckedChanged
        trbVelocityVz.Value = trbVelocityHz.Value
        lbVelocityVz.Text = lbVelocityHz.Text
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Rechts (in der 1.Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks> 'Versuch mit Threading...
    Private Sub btright_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btright.MouseDown
        btright.BackColor = Color.GreenYellow
        'Dim a As Integer = 2 'Faktor zum multiplizieren
        'a = Me._tachymeter.getDirectionFromFace()
        'System.Threading.Thread.Sleep(100)
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(hz_speed_rad, 0)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Links (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btleft_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btleft.MouseDown
        btleft.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(-hz_speed_rad, 0)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Oben (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btup_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btup.MouseDown
        btup.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(0, -zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Unten (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btdown_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btdown.MouseDown
        btdown.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(0, zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Oben und Links (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btupleft_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btupleft.MouseDown
        btupleft.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(-hz_speed_rad, -zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument nach Oben und Rechts (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btupright_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btupright.MouseDown
        btupright.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(hz_speed_rad, -zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Verfährt das Instrument anch Unten und Links (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btdownleft_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btdownleft.MouseDown
        btdownleft.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(-hz_speed_rad, zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Verfährt das Gerät nach Unten und Rechts (in der 1. Lage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Benutzte Funktionen:
    ''' Scroll2RadPerSec</remarks>
    Private Sub btdownright_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btdownright.MouseDown
        btdownright.BackColor = Color.GreenYellow
        'Dim local_math As New own_math
        Dim hz_speed_rad, zw_speed_rad As Double
        hz_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityHz.Value))
        zw_speed_rad = Scroll2RadPerSec(CDbl(trbVelocityVz.Value))
        Me._tachymetercontrol.start_move_joystick(hz_speed_rad, zw_speed_rad)
    End Sub

    ''' <summary>
    ''' Hält das Gerät an (bei MouseUp) und setzt die ButtonFarbe auf Standard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Joystick_MouseUp(ByVal sender As System.Object, _
                                ByVal e As System.Windows.Forms.MouseEventArgs) _
                                Handles btright.MouseUp, btleft.MouseUp, btup.MouseUp, btdown.MouseUp, _
                                btupleft.MouseUp, btupright.MouseUp, btdownleft.MouseUp, btdownright.MouseUp
        'Buttons wieder auf ursprüngliche Farbe setzten
        btright.BackColor = Color.Transparent
        btleft.BackColor = Color.Transparent
        btup.BackColor = Color.Transparent
        btdown.BackColor = Color.Transparent
        btupleft.BackColor = Color.Transparent
        btupright.BackColor = Color.Transparent
        btdownleft.BackColor = Color.Transparent
        btdownright.BackColor = Color.Transparent
        'Stop Controller
        Me._tachymetercontrol.stop_move_joystick()
    End Sub
    Private Sub bu_stopmove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_stopmove.Click
        Me._tachymetercontrol.stop_move_joystick()
    End Sub
    Public Sub display_start_move_joystick(ByRef daten As LeicaTPS1000data)
        Try
            Me.logger(daten)
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try
    End Sub
    Public Sub display_stop_move_joystick(ByRef daten As LeicaTPS1000data)
        Try
            Me.logger(daten)
        Catch e As ObjectDisposedException
            MessageBox.Show(e.ToString)
        End Try
    End Sub
#End Region
#Region "Lagewechsel"
    Private Sub btChangeFace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btChangeFace.Click
        If (ch_precisionOn_2.Checked = True) Then
            AddHandler Me._tachymetercontrol.change_face_ready, AddressOf Me.display_change_face
            Me.changeTachyStatusSymbol(2)
            Me._tachymetercontrol.change_face(1)
        ElseIf (ch_precisionOn_2.Checked = False) Then
            AddHandler Me._tachymetercontrol.change_face_ready, AddressOf Me.display_change_face
            Me.changeTachyStatusSymbol(2)
            Me._tachymetercontrol.change_face(0)
        End If

    End Sub
    Public Sub display_change_face(ByRef daten As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.change_face_ready, AddressOf Me.display_change_face
        display_tachy_errorcodes(daten.errorstring)
        Me.changeTachyStatusSymbol(1)
        Me.logger(daten)
    End Sub
#End Region
#Region "Move Absolute"
    Private Sub btDriveAbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDriveAbs.Click
        'Überprüfung ob Eingabe nummerisch
        If IsNumeric(tbHzAbsolut.Text) And IsNumeric(tbVzAbsolut.Text) Then
            If (ch_precisionOn_1.Checked = True) Then
                AddHandler Me._tachymetercontrol.move_Absolute_HzV_ready, AddressOf display_move_Absolute_HzV
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.move_Absolute_HzV(CDbl(tbHzAbsolut.Text), CDbl(tbVzAbsolut.Text), 1)
            ElseIf (ch_precisionOn_1.Checked = False) Then
                AddHandler Me._tachymetercontrol.move_Absolute_HzV_ready, AddressOf display_move_Absolute_HzV
                Me.changeTachyStatusSymbol(2)
                Me._tachymetercontrol.move_Absolute_HzV(CDbl(tbHzAbsolut.Text), CDbl(tbVzAbsolut.Text), 0)
            End If
        Else
            MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub display_move_Absolute_HzV(ByRef daten As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.move_Absolute_HzV_ready, AddressOf display_move_Absolute_HzV
        display_tachy_errorcodes(daten.errorstring)
        Me.changeTachyStatusSymbol(1)
        Me.logger(daten)
    End Sub
#End Region
#Region "Fine Adjust manuell gestartet"
    Private Sub Button_FineAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_FineAdjust.Click
        If IsNumeric(TextBox_ATR_SearchRange_Hz.Text) And IsNumeric(TextBox_ATR_SearchRange_V.Text) Then
            Me.changeTachyStatusSymbol(2)
            AddHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.fineAdjust_manuell_gestartet
            Me._tachymetercontrol.fine_adjust(CDbl(TextBox_ATR_SearchRange_Hz.Text), CDbl(TextBox_ATR_SearchRange_V.Text))
        Else
            MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Sub fineAdjust_manuell_gestartet(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.fineAdjust_manuell_gestartet
        display_tachy_errorcodes(data.errorstring)
        Me.changeTachyStatusSymbol(1)
    End Sub

#End Region
#Region "Lockin"
    Private Sub Button_Lockin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Lockin.Click
        AddHandler Me._tachymetercontrol.set_Lock_Mode_ready, AddressOf Me.lockmode_is_on
        Me.changeTachyStatusSymbol(2)
        Me._tachymetercontrol.set_Lock_Mode(1)
    End Sub

    Public Sub lockmode_is_on(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_Lock_Mode_ready, AddressOf Me.lockmode_is_on
        If (data.errorcode = 0) Then
            Me.changeTachyStatusSymbol(2)
            display_tachy_errorcodes(data.errorstring)
            If IsNumeric(TextBox_ATR_SearchRange_Hz.Text) And IsNumeric(TextBox_ATR_SearchRange_V.Text) Then
                Me.changeTachyStatusSymbol(2)
                AddHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.target_gefunden_lockmode
                Me._tachymetercontrol.fine_adjust(CDbl(TextBox_ATR_SearchRange_Hz.Text), CDbl(TextBox_ATR_SearchRange_V.Text))
            Else
                MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.changeTachyStatusSymbol(1)
            End If
        Else
            Me.changeTachyStatusSymbol(1)
            display_tachy_errorcodes(data.errorstring)
        End If
    End Sub

    Public Sub target_gefunden_lockmode(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.target_gefunden_lockmode
        If (data.errorcode = 0) Then
            Me.changeTachyStatusSymbol(2)
            display_tachy_errorcodes(data.errorstring)
            AddHandler Me._tachymetercontrol.lock_in_ready, AddressOf Me.lockin_startet_lockmode
            Me._tachymetercontrol.lock_in()
        Else
            Me.changeTachyStatusSymbol(1)
            display_tachy_errorcodes(data.errorstring)
        End If
    End Sub

    Public Sub lockin_startet_lockmode(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.lock_in_ready, AddressOf Me.lockin_startet_lockmode
        Me.changeTachyStatusSymbol(1)
        display_tachy_errorcodes(data.errorstring)
        If (data.errorcode = 0) Then
            Button_Lockin.Enabled = False
            Button_Lockin_Stop.Enabled = True
        Else
            Button_Lockin.Enabled = True
            Button_Lockin_Stop.Enabled = False
        End If
    End Sub
#End Region
#Region "Stop Lockin"
    Private Sub Button_Lockin_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Lockin_Stop.Click
        AddHandler Me._tachymetercontrol.set_Lock_Mode_ready, AddressOf Me.lockmode_stopt
        Me.changeTachyStatusSymbol(2)
        Me._tachymetercontrol.set_Lock_Mode(0)
    End Sub
    Public Sub lockmode_stopt(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_Lock_Mode_ready, AddressOf Me.lockmode_stopt
        Me.changeTachyStatusSymbol(2)
        display_tachy_errorcodes(data.errorstring)
        If (data.errorcode = 0) Then
            Button_Lockin.Enabled = True
            Button_Lockin_Stop.Enabled = False
        End If
        ' Electronic Guide Lights werden nicht mit dem Befehl Lockin(off) ausgeschaltet und würden bis zum auschalten des Gerätes
        ' ohne Grund weiterblinken. Deshalb werden diese extra ausgeschaltet, der Lockin-Befehl schaltet sie wieder automatisch ein.
        AddHandler Me._tachymetercontrol.set_electronic_guide_light_ready, AddressOf Me.egl_off
        Me._tachymetercontrol.set_electronic_guide_light(0)
    End Sub
    Public Sub egl_off(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_electronic_guide_light_ready, AddressOf Me.egl_off
        Me.changeTachyStatusSymbol(1)
        display_tachy_errorcodes(data.errorstring)
    End Sub
#End Region
#Region "Laserpointer an/aus"
    Private Sub CheckBox_Laserpointer_activate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Laserpointer_activate.CheckedChanged
        If (CheckBox_Laserpointer_activate.Checked = True) Then
            AddHandler Me._tachymetercontrol.set_Laserpointer_ready, AddressOf Me.laserpointer_on
            Me.changeTachyStatusSymbol(2)
            Me._tachymetercontrol.set_Laserpointer(1)
        Else
            AddHandler Me._tachymetercontrol.set_Laserpointer_ready, AddressOf Me.laserpointer_on
            Me.changeTachyStatusSymbol(2)
            Me._tachymetercontrol.set_Laserpointer(0)
        End If
    End Sub
    Public Sub laserpointer_on(ByRef data As LeicaTPS1000data)
        RemoveHandler Me._tachymetercontrol.set_Laserpointer_ready, AddressOf Me.laserpointer_on
        Me.changeTachyStatusSymbol(1)
        display_tachy_errorcodes(data.errorstring)
    End Sub
#End Region
#Region "Vertikaler Verfahrbereich definieren"
    Private Sub rb_vertical_range_camera_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_vertical_range_camera.CheckedChanged
        If (rb_vertical_range_camera.Checked = True) Then
            Me._tachymetercontrol.change_vertical_range(60.0, 340.0)
            tb_verticale_range_lage1.Text = "60.0000"
            tb_verticale_range_lage2.Text = "340.0000"
            tb_verticale_range_lage1.Enabled = False
            tb_verticale_range_lage2.Enabled = False
            bu_def_vertical_range.Enabled = False
        End If
    End Sub

    Private Sub rb_vertical_range_free_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_vertical_range_free.CheckedChanged
        If (rb_vertical_range_free.Checked = True) Then
            Me._tachymetercontrol.change_vertical_range(0.0, 400.0)
            tb_verticale_range_lage1.Text = "0.0000"
            tb_verticale_range_lage2.Text = "400.0000"
            tb_verticale_range_lage1.Enabled = False
            tb_verticale_range_lage2.Enabled = False
            bu_def_vertical_range.Enabled = False
        End If
    End Sub

    Private Sub rb_vertical_range_user_define_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_vertical_range_user_define.CheckedChanged
        If (rb_vertical_range_user_define.Checked = True) Then
            tb_verticale_range_lage1.Enabled = True
            tb_verticale_range_lage2.Enabled = True
            bu_def_vertical_range.Enabled = True
        End If
    End Sub

    Private Sub bu_def_vertical_range_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_def_vertical_range.Click
        If (rb_vertical_range_user_define.Checked = True) Then
            If IsNumeric(tb_verticale_range_lage1.Text) And IsNumeric(tb_verticale_range_lage2.Text) Then
                Me._tachymetercontrol.change_vertical_range(CDbl(tbHzAbsolut.Text), CDbl(tbVzAbsolut.Text))
            Else
                MessageBox.Show("Eingabe nicht nummerisch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
#End Region

    Public Sub display_tachy_errorcodes(ByVal text As String)
        tssLa_TachyErrorcodes.Text = text
    End Sub

    Private Sub bu_deviceControl_totalStation_CloseComport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_deviceControl_totalStation_CloseComport.Click
        Me._tachymeter.closeComport()
        Me.setTachyButtons("disconnected")
        Me._isTachymeterConnect = False
    End Sub

    Private Sub bu_export_data_as_csv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_export_data_as_csv.Click
        Dim save As New SaveFileDialog
        save.InitialDirectory = Me._project_dir
        save.Filter = "CSV (*.csv)|*.csv"
        save.Title = "Save As"
        If save.ShowDialog() = DialogResult.OK Then
            Me._messdaten.punkte_To_csv(save.FileName())
        End If
    End Sub

    '################################### GUI Elemente ########################################
#Region "Farbe der Labels"
    ''' <summary>
    ''' Setzt die Farbe des Camerastatussymbol
    ''' </summary>
    ''' <param name="value">
    ''' 0 - Disconnected
    ''' 1 - Standby
    ''' 2 - Live-Modus
    ''' </param>
    ''' <remarks></remarks>
    Public Sub ChangeCameraStatusSymbol(ByVal value As Integer)

        Select Case value
            Case 0
                Panel_DeviceControl_Camerastatus.BackColor = Color.Red
                La_DeviceControl_Camerastatus.Text = "Disconnect"
                Me.setCameraButtons("disconnected")
            Case 1
                Panel_DeviceControl_Camerastatus.BackColor = Color.Yellow
                La_DeviceControl_Camerastatus.Text = "Standby"
                Me.setCameraButtons("liveIsStopped")
            Case 2
                Panel_DeviceControl_Camerastatus.BackColor = Color.Green
                La_DeviceControl_Camerastatus.Text = "Live-Modus"
                Me.setCameraButtons("liveIsRunning")
            Case Else

        End Select

    End Sub
    ''' <summary>
    ''' Setzt die Farbe des Tachymeterstatussymbol
    ''' </summary>
    ''' <param name="value">
    ''' 0 - Disconnected
    ''' 1 - Connected
    ''' 2 - In Process
    ''' </param>
    ''' <remarks></remarks>
    Public Sub changeTachyStatusSymbol(ByVal value As Integer)
        Select Case value
            Case 0
                Panel_Tachymeter_Status.BackColor = Color.Red
                La_Tachymeterstatus.Text = "Disconnect"
            Case 1
                Panel_Tachymeter_Status.BackColor = Color.Green
                La_Tachymeterstatus.Text = "Connect (Ready)"
            Case 2
                Panel_Tachymeter_Status.BackColor = Color.Yellow
                La_Tachymeterstatus.Text = "In-Process"
        End Select
    End Sub
#End Region

    Private Sub ch_precisionOn_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ch_precisionOn_1.CheckedChanged
        If (ch_precisionOn_1.Checked = True) Then
            ch_precisionOn_2.Checked = True
        ElseIf (ch_precisionOn_1.Checked = False) Then
            ch_precisionOn_2.Checked = False
        End If
    End Sub

    Private Sub ch_precisionOn_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ch_precisionOn_2.CheckedChanged
        If (ch_precisionOn_2.Checked = True) Then
            ch_precisionOn_1.Checked = True
        ElseIf (ch_precisionOn_2.Checked = False) Then
            ch_precisionOn_1.Checked = False
        End If
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.test_request_strings(vbLf + "%R1Q,5011:" + vbCrLf)
    'End Sub

    Private Sub set_Image_Part(ByVal linksoben_X As Long, ByVal linksoben_Y As Long, ByVal width As Long, ByVal heigth As Long)
        Dim rec As System.Drawing.Rectangle
        rec = New System.Drawing.Rectangle()
        rec.X = linksoben_X
        rec.Y = linksoben_Y
        rec.Width = width
        rec.Height = heigth
        Me.HWindowControl1.ImagePart = rec
        Me.HWindowControl2.ImagePart = rec
    End Sub

    Private Sub setCameraButtons(ByVal status As String)

        Select Case status
            Case "disconnected"
                gb_DeviceControl_Camera.Enabled = True
                Bu_DeviceControl_Camera_connect.Enabled = True
                Bu_DeviceControl_Camera_Disconnect.Enabled = False
                Panel_DeviceControl_Camerastatus.BackColor = Color.Red
                Panel_DeviceControl_Camerastatus2.BackColor = Color.Red
                La_DeviceControl_Camerastatus.Text = "Disconnected"
                La_DeviceControl_Camerastatus2.Text = "Disconnected"

                gb_livebild.Enabled = False
                gb_Snapshot.Enabled = False
                gb_zoommove.Enabled = False
                'gb_property.Enabled = False

                cb_Measure_saveImage.Checked = False
                cb_Measure_saveImage.Enabled = False

                GroupBox_CameraTiming.Enabled = False
                GroupBox_camera_image.Enabled = False
                GroupBox_camera_SizeLiveModus.Enabled = False

                'Bestimmung Strichkreuz
                ComboBox_crosshair_types.Enabled = False
                Button_Crosshair.Enabled = False
                CheckBox_SaveImage_crosshair.Enabled = False

                'Selbstkalibrierung
                GroupBox_Field_of_View.Enabled = False
                GroupBox_SKTarget1.Enabled = False
                GroupBox_SKTarget2.Enabled = False
                GroupBox_SK_Solve.Enabled = False
            Case "connected"
                gb_DeviceControl_Camera.Enabled = True
                Bu_DeviceControl_Camera_connect.Enabled = False
                Bu_DeviceControl_Camera_Disconnect.Enabled = True
                Panel_DeviceControl_Camerastatus.BackColor = Color.Yellow
                Panel_DeviceControl_Camerastatus2.BackColor = Color.Yellow
                La_DeviceControl_Camerastatus.Text = "Connected"
                La_DeviceControl_Camerastatus2.Text = "Connected"

                gb_livebild.Enabled = True
                gb_Snapshot.Enabled = True
                gb_zoommove.Enabled = True
                'gb_property.Enabled = True

                cb_Measure_saveImage.Checked = False
                cb_Measure_saveImage.Enabled = True

                GroupBox_CameraTiming.Enabled = True
                GroupBox_camera_image.Enabled = True
                GroupBox_camera_SizeLiveModus.Enabled = True

                def_comboBox_crosshair_types()
                def_comboBox_field_of_view()

                'Bestimmung Strichkreuz
                ComboBox_crosshair_types.Enabled = True
                Button_Crosshair.Enabled = True
                CheckBox_SaveImage_crosshair.Enabled = True

                'Selbstkalibrierung
                GroupBox_Field_of_View.Enabled = False
                GroupBox_SKTarget1.Enabled = False
                GroupBox_SKTarget2.Enabled = False
                GroupBox_SK_Solve.Enabled = False
            Case "liveIsRunning"
                Bu_LiveModusStart.Enabled = False
                Bu_LiveModusStop.Enabled = True
                Panel_DeviceControl_Camerastatus.BackColor = Color.Green
                Panel_DeviceControl_Camerastatus2.BackColor = Color.Green
                La_DeviceControl_Camerastatus.Text = "Live is Running"
                La_DeviceControl_Camerastatus2.Text = "Live is Running"
            Case "liveIsStopped"
                Bu_LiveModusStart.Enabled = True
                Bu_LiveModusStop.Enabled = False
                Panel_DeviceControl_Camerastatus.BackColor = Color.Yellow
                Panel_DeviceControl_Camerastatus2.BackColor = Color.Yellow
                La_DeviceControl_Camerastatus.Text = "Live is Stopped"
                La_DeviceControl_Camerastatus2.Text = "Live is Stopped"
            Case Else

        End Select

    End Sub

    Private Sub setTachyButtons(ByVal status As String)
        Select Case status
            Case "disconnected"
                ' Motor:
                gb_moveabsolute.Enabled = False
                gb_joystick.Enabled = False
                gb_lagewechsel.Enabled = False
                gb_zenitRange.Enabled = False

                ' Measure
                gb_measure.Enabled = False
                ' Options
                gb_edm_mode.Enabled = False
                changeTachyStatusSymbol(0)

                La_theoinfos.Text = ""

                Button_Tachymeter_Off.Enabled = False
                Button_Tachymeter_on.Enabled = False

                'ATR
                GroupBox_ATR.Enabled = False

                'Lockmode
                GroupBox_LockMode.Enabled = False


            Case "connected" 'entsprcht auch dem Status on
                ' Motor:
                gb_moveabsolute.Enabled = True
                gb_joystick.Enabled = True
                gb_lagewechsel.Enabled = True
                gb_zenitRange.Enabled = True
                ' Measure
                gb_measure.Enabled = True
                ' Options
                gb_edm_mode.Enabled = True
                rb_edmMode_IR.Checked = True
                changeTachyStatusSymbol(1)


                Button_Tachymeter_Off.Enabled = True
                Button_Tachymeter_on.Enabled = False

                'ATR
                GroupBox_ATR.Enabled = True
                TextBox_ATR_SearchRange_Hz.Text = "5,0000" ' Startwerte
                TextBox_ATR_SearchRange_V.Text = "5,0000"  ' Startwerte

                'Lockmode
                GroupBox_LockMode.Enabled = True


            Case "off"
                ' Motor:
                gb_moveabsolute.Enabled = False
                gb_joystick.Enabled = False
                gb_lagewechsel.Enabled = False
                gb_zenitRange.Enabled = False

                ' Measure
                gb_measure.Enabled = False
                ' Options
                gb_edm_mode.Enabled = False
                changeTachyStatusSymbol(0)

                La_theoinfos.Text = ""

                Button_Tachymeter_Off.Enabled = False
                Button_Tachymeter_on.Enabled = True

                'ATR
                GroupBox_ATR.Enabled = False

                'Lockmode
                GroupBox_LockMode.Enabled = False
        End Select

    End Sub

    '################################# Kamera #############################################
#Region "Belichtung, Bildrate ...."

    ' NUR für die Beschriftung der Labels
    Private Sub TB_Pixeltakt_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_PixelClock.Scroll
        Label_PixelClock_act.Text() = TrackBar_PixelClock.Value()
        'La_Camera_Timing_Pixelclock_act.Text() = TrackBar_Camera_Timing_PixelClock.Value()
    End Sub

    Private Sub TB_Bildrate_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_FrameRate.Scroll
        Label_FrameRate_act.Text() = TrackBar_FrameRate.Value()
        'La_Camera_Timing_framerate_act.Text() = Trackbar_Camera_Timing_FrameRate.Value().ToString("0.0")
    End Sub

    Private Sub TB_Belichtung_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_ExposureTime.Scroll
        Label_ExposureTime_act.Text() = TrackBar_ExposureTime.Value()
        'La_Camera_Timing_Exposure_act.Text() = Trackbar_Camera_Timing_Exposure.Value().ToString("0.0")
    End Sub

    ' MouseUp ist in diesem Fall besser als .Scroll, weil die Methode erst dann ausgeführt, wenn die Maustaste losgelassen wird.
    ' Bei Scroll wird bei jeden Schritt ausgeführt, dies führt dazu, dass die Methode sehr oft ausgeführt wird.
    Private Sub TB_Pixeltakt_MouseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_PixelClock.MouseUp
        Label_PixelClock_act.Text() = TrackBar_PixelClock.Value()
        Me._camera.pixel_clock = TrackBar_PixelClock.Value()
        'Me.def_pixeltakt()
        Me.def_bildrate()
        Me.def_belichtung()
    End Sub

    Private Sub TB_Bildrate_MouseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_FrameRate.MouseUp
        Label_FrameRate_act.Text() = TrackBar_FrameRate.Value().ToString("0.0")
        Me._camera.frame_rate = TrackBar_FrameRate.Value()
        Me.def_belichtung()
    End Sub

    Private Sub TB_Belichtung_MouseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_ExposureTime.MouseUp
        Label_ExposureTime_act.Text() = TrackBar_ExposureTime.Value().ToString("0.0")
        Me._camera.exposure_time = TrackBar_ExposureTime.Value()
    End Sub

    Private Sub def_pixeltakt()
        Dim parameter_pixeltakt(4) As Integer
        parameter_pixeltakt = Me._camera.pixel_clock_range()
        TrackBar_PixelClock.Minimum() = parameter_pixeltakt(0)
        TrackBar_PixelClock.Maximum() = parameter_pixeltakt(1)

        If (parameter_pixeltakt(2) < 1) Then
            TrackBar_PixelClock.TickFrequency() = 1
        Else
            TrackBar_PixelClock.TickFrequency() = parameter_pixeltakt(2)
        End If

        Try
            TrackBar_PixelClock.Value() = Me._camera.pixel_clock()
            Label_PixelClock_act.Text() = TrackBar_PixelClock.Value()
            Label_PixelClock_Min.Text() = parameter_pixeltakt(0)
            Label_PixelClock_Max.Text() = parameter_pixeltakt(1)
        Catch ex As ArgumentOutOfRangeException
            Me.def_pixeltakt()
        End Try

    End Sub

    Private Sub def_bildrate()
        Dim parameter_bildrate(4) As Double
        Dim frame_rate As Double
        parameter_bildrate = Me._camera.frame_rate_range()
        TrackBar_FrameRate.Minimum() = CInt(parameter_bildrate(0))
        TrackBar_FrameRate.Maximum() = CInt(parameter_bildrate(1))

        If (parameter_bildrate(2) < 1) Then
            TrackBar_FrameRate.TickFrequency() = 1
        Else
            TrackBar_FrameRate.TickFrequency() = CInt(parameter_bildrate(2))
        End If

        Try
            frame_rate = Me._camera.frame_rate()
            TrackBar_FrameRate.Value() = CInt(frame_rate)
            Label_FrameRate_act.Text() = frame_rate.ToString("0.0")
            Label_FrameRate_Min.Text() = parameter_bildrate(0).ToString("0.0")
            Label_FrameRate_Max.Text() = parameter_bildrate(1).ToString("0.0")
        Catch ex As ArgumentOutOfRangeException
            Me.def_bildrate()
        End Try
    End Sub

    Private Sub def_belichtung()
        Dim parameter(4) As Double
        Dim exposure As Double
        parameter = Me._camera.exposure_time_range()
        TrackBar_ExposureTime.Minimum() = CInt(parameter(0))
        TrackBar_ExposureTime.Maximum() = CInt(parameter(1))

        If (parameter(2) < 1) Then
            TrackBar_ExposureTime.TickFrequency() = 1
        Else
            TrackBar_ExposureTime.TickFrequency() = CInt(parameter(2))
        End If

        Try
            exposure = Me._camera.exposure_time
            TrackBar_ExposureTime.Value() = CInt(exposure)
            Label_ExposureTime_act.Text() = exposure.ToString("0.0")
            Label_ExposureTime_Min.Text() = parameter(0).ToString("0.0")
            Label_ExposureTime_Max.Text() = parameter(1).ToString("0.0")
        Catch ex As ArgumentOutOfRangeException
            Me.def_belichtung()
        End Try
    End Sub

#End Region
#Region "Auflösung"
    Private Sub def_resolution_combobox()
        'Me._camera.get_sensor_size()
        Dim width, heigth As Long
        Dim text As String

        width = Me._camera.sensor_width / 1
        heigth = Me._camera.sensor_heigth / 1
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)

        width = Me._camera.sensor_width / 2
        heigth = Me._camera.sensor_heigth / 2
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)

        width = Me._camera.sensor_width / 3
        heigth = Me._camera.sensor_heigth / 3
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)

        width = Me._camera.sensor_width / 4
        heigth = Me._camera.sensor_heigth / 4
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)

        width = Me._camera.sensor_width / 5
        heigth = Me._camera.sensor_heigth / 5
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)

        width = Me._camera.sensor_width / 6
        heigth = Me._camera.sensor_heigth / 6
        text = width.ToString + " x " + heigth.ToString
        ComboBox_Resolution_Live.Items.Add(text)
        ComboBox_Resolution_Single.Items.Add(text)
    End Sub

    Private Sub CheckBox_SameResolution_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_SameResolution.CheckedChanged
        If (CheckBox_SameResolution.Checked = True) Then
            ComboBox_Resolution_Single.Enabled = False
        Else
            ComboBox_Resolution_Single.Enabled = True
        End If
    End Sub

    Private Sub ComboBox_Resolution_Live_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Resolution_Live.SelectedIndexChanged
        Dim res As Long

        Select Case (ComboBox_Resolution_Live.SelectedIndex)
            Case 0
                res = 1
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 0
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
            Case 1
                res = 2
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 1
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
            Case 2
                res = 3
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 2
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
            Case 3
                res = 4
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 3
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
            Case 4
                res = 5
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 4
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
            Case 5
                res = 6
                Me._camera.resolution_live = res
                If (CheckBox_SameResolution.Checked = True) Then
                    ComboBox_Resolution_Single.SelectedIndex = 5
                    Me._camera.resolution_single_image = res
                End If
                'Me._camera.change_resolution()
                Me.set_Image_Part(0, 0, Me._camera.sensor_width / res, Me._camera.sensor_heigth / res)
        End Select
        Me.def_pixeltakt()
        Me.def_bildrate()
        Me.def_belichtung()
    End Sub

    Private Sub ComboBox_Resolution_Single_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Resolution_Single.SelectedIndexChanged
        Dim res As Long

        Select Case (ComboBox_Resolution_Live.SelectedIndex)
            Case 0
                res = 1
                Me._camera.resolution_single_image = res
            Case 1
                res = 2
                Me._camera.resolution_single_image = res
            Case 2
                res = 3
                Me._camera.resolution_single_image = res
            Case 3
                res = 4
                Me._camera.resolution_single_image = res
            Case 4
                res = 5
                Me._camera.resolution_single_image = res
            Case 5
                res = 6
                Me._camera.resolution_single_image = res
        End Select
    End Sub
#End Region

    Private Sub Bu_Snapshot_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_Snapshot.Click
        Dim FileName As String
        Dim SaveFileDialog1 As New SaveFileDialog
        With SaveFileDialog1
            .Filter = "TIFF format (*.tif)|*.tif|Alle Dateien (*.*)|*.*"
            .FilterIndex = 1
            .InitialDirectory = Me._project_dir
            .Title = "Speichern als Tiff-Datei *.tif"
            If (.ShowDialog() = DialogResult.OK) Then
                FileName = SaveFileDialog1.FileName
                Me.save_Image_To_Disk(FileName, "tiff")
            Else
                Exit Sub
            End If
        End With
    End Sub

    Private Sub Bu_DeviceControl_Camera_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_DeviceControl_Camera_connect.Click
        If (Me._camera.connect() = True) Then
            Me.setCameraButtons("connected")
            ChangeCameraStatusSymbol(2)
            Me.def_resolution_combobox()
            Me.ComboBox_Resolution_Live.SelectedIndex = 0
            Me._isCameraConnect = True
            Me.start_Live_Modus()
        End If
    End Sub

    Private Sub Bu_DeviceControl_Camera_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_DeviceControl_Camera_Disconnect.Click
        Me._camera.disconnect()
        ChangeCameraStatusSymbol(0)
        Me._isCameraConnect = False
    End Sub

    Private Sub Bu_LiveModusStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_LiveModusStart.Click
        Me.start_Live_Modus()
    End Sub

    Private Sub start_Live_Modus()
        If (Me._camera.startLiveBild() = True) Then
            ChangeCameraStatusSymbol(2)
            Me.set_Image_Part(0, 0, Me._camera.sensor_width / Me._camera.resolution_live, Me._camera.sensor_heigth / Me._camera.resolution_live)
            Me.def_pixeltakt()
            Me.def_bildrate()
            Me.def_belichtung()
        End If
    End Sub

    Private Sub Bu_LiveModusStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_LiveModusStop.Click
        Me.stop_Live_Modus()
    End Sub

    Private Sub stop_Live_Modus()
        Me._camera.stopLiveBild()
        ChangeCameraStatusSymbol(1)
    End Sub

    Public Sub display_Livepicture(ByVal image As HImage)
        Dim tmptyp As String = ""
        image.GetImagePointer1(tmptyp, Me._int_imagewidth, Me._int_imageheigth)
        image.DispObj(Me.HWindowControl1.HalconWindow)
    End Sub



    '################################## Sonstiges ######################################

#Region "Methoden die SK betreffen, evtl. muss noch was an Benutzerführung verbessert werden"

    Private Sub def_comboBox_crosshair_types()
        ComboBox_crosshair_types.Items.Add("TM5100/TDA5005")
        ComboBox_crosshair_types.Items.Add("TCRM1103/TS30")
        ComboBox_crosshair_types.Items.Add("Testbild")
    End Sub

    Private Sub def_comboBox_field_of_view()
        ComboBox_Sk_typ.Items.Add("SK1P")
        ComboBox_Sk_typ.Items.Add("SK2P")
        ComboBox_Sk_typ.Items.Add("SK4P")

        ComboBox_controlpoints.Items.Add("24")
        ComboBox_controlpoints.Items.Add("36")
        ComboBox_controlpoints.Items.Add("60")
        ComboBox_controlpoints.Items.Add("4")

    End Sub

    Private Sub Button_SelfCalibrationStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelfCalibrationStart.Click
        Dim n As Integer
        If (ComboBox_controlpoints.SelectedIndex = 0) Then
            n = 24
        ElseIf (ComboBox_controlpoints.SelectedIndex = 1) Then
            n = 36
        ElseIf (ComboBox_controlpoints.SelectedIndex = 2) Then
            n = 60
        ElseIf (ComboBox_controlpoints.SelectedIndex = 3) Then
            n = 4
        Else
            MessageBox.Show("Please select number of controlpoints")
        End If

        If (ComboBox_Sk_typ.SelectedIndex = 0) Then 'SK1P
            Panel_Status_Sk.BackColor = Color.Yellow
            Label_Status_Sk.Text = "Measure"
            Me._tachymetercontrol.sk1_messen(n, Me._selfcalibration_hz_lage1_target1_center, Me._selfcalibration_v_lage1_target1_center, _
                                                Me._selfcalibration_hz_lage1_target1_circle, Me._selfcalibration_v_lage1_target1_circle)
        ElseIf (ComboBox_Sk_typ.SelectedIndex = 1) Then 'SK2P
            Panel_Status_Sk.BackColor = Color.Yellow
            Label_Status_Sk.Text = "Measure"
            Me._tachymetercontrol.sk2_messen(n, Me._selfcalibration_hz_lage1_target1_center, Me._selfcalibration_v_lage1_target1_center, _
                                                Me._selfcalibration_hz_lage1_target1_circle, Me._selfcalibration_v_lage1_target1_circle)
        ElseIf (ComboBox_Sk_typ.SelectedIndex = 2) Then
            Panel_Status_Sk.BackColor = Color.Yellow
            Label_Status_Sk.Text = "Measure"
            Me._tachymetercontrol.sk4_messen(n, Me._selfcalibration_hz_lage1_target1_center, Me._selfcalibration_v_lage1_target1_center, _
                                                Me._selfcalibration_hz_lage1_target1_circle, Me._selfcalibration_v_lage1_target1_circle, _
                                                Me._selfcalibration_hz_lage1_target2_center, Me._selfcalibration_v_lage1_target2_center, _
                                                Me._selfcalibration_hz_lage1_target2_circle, Me._selfcalibration_v_lage1_target2_circle)
        Else
            MessageBox.Show("Please select a typ")
        End If
    End Sub

    Public Sub bild_selbstkalibrierung(ByRef daten As LeicaTPS1000data, ByVal target As Integer, ByVal lage As Integer)
        Dim image As HImage
        Dim image_reduced As HImage
        Dim bv As New bildverarbeitung

        image = Me._camera.makeSingleImage()

        image_reduced = image.ReduceDomain(Me._kalibrierung.strichkreuz.maske)

        Dim flag As Boolean
        flag = bv.strichkreuz_kollimator01(image_reduced, daten.yc, daten.xc)

        ' Messdaten abspeichern und abspeichern in die CSV (wenn eingestellt)
        Dim pkt As New Messpunkt

        pkt.ID = Me._messdaten.freeID()
        pkt.messdatum = daten.mesuresdate
        pkt.messzeit = daten.measuretime
        ' Überprüfung ob Punktnummer vergeben und nummerisch ist
        If (tb_pointnumber.Text.Trim() <> String.Empty) Then
            If (IsNumeric(tb_pointnumber.Text.Trim())) Then
                pkt.punktnummer = CInt(tb_pointnumber.Text)
            End If
        End If
        pkt.punktname = target.ToString & "_" & lage.ToString
        pkt.horizontalrichtung = daten.horizontalrichtung
        pkt.zenitwinkel = daten.zenitwinkel
        pkt.distanz = daten.distanz
        pkt.crossInclination = daten.CrossIncline
        pkt.lengthInclination = daten.LengthIncline
        pkt.bildkoordinate_X = daten.xc
        pkt.bildkoordinate_Y = daten.yc
        pkt.tachymeter_errorstring = daten.errorstring

        Me._messdaten.add_punkt(pkt)

        If (flag = True And daten.errorcode = 0) Then
            pkt.gewicht_Selbstkalibrierung = 1.0
            If (target = 1 And lage = 1) Then
                Me._kalibrierung.add_sk_punkt(0, pkt)
            ElseIf (target = 1 And lage = 2) Then
                Me._kalibrierung.add_sk_punkt(1, pkt)
            ElseIf (target = 2 And lage = 1) Then
                Me._kalibrierung.add_sk_punkt(2, pkt)
            ElseIf (target = 2 And lage = 2) Then
                Me._kalibrierung.add_sk_punkt(3, pkt)
            End If
        Else
            pkt.gewicht_Selbstkalibrierung = 0.0
            If (target = 1 And lage = 1) Then
                Me._kalibrierung.add_sk_punkt(0, pkt)
            ElseIf (target = 1 And lage = 2) Then
                Me._kalibrierung.add_sk_punkt(1, pkt)
            ElseIf (target = 2 And lage = 1) Then
                Me._kalibrierung.add_sk_punkt(2, pkt)
            ElseIf (target = 2 And lage = 2) Then
                Me._kalibrierung.add_sk_punkt(3, pkt)
            End If
        End If


        Dim disp As New DisplaySKData(AddressOf display_sk_ergebnisse)
        Me.Invoke(disp, pkt)


        'Es werden nur Bild auf die Festplatte gespeichert, wenn die Erkennung kein Erfolg hatte:
        If (CheckBox_SaveErrorImages_self_calibration.Checked = True) Then
            If (flag = False) Then
                Dim filename As String
                filename = Me._project_dir + "\self_calibration\" + pkt.ID.ToString + ".tif"
                Me.save_Image_To_Disk(filename, "tiff")
            End If
        End If


        'Bilder auf die Festplatte abspeichern?:
        If (CheckBox_SaveImage_selbstkalibrierung.Checked = True) Then
            Dim filename As String
            filename = Me._project_dir + "\self_calibration\" + pkt.ID.ToString + ".tif"
            Me.save_Image_To_Disk(filename, "tiff")
        End If


        'Darstellung in der Grafik
        If (flag = True) Then
            TabControl3.SelectedTab() = TabPage_bv1
            Dim window As HWindow
            window = HWindowControl2.HalconWindow
            window.ClearWindow()
            image_reduced.DispImage(window)
            window.DispCross(pkt.bildkoordinate_Y, pkt.bildkoordinate_X, 20, 0)
        End If
    End Sub

    Private Sub display_sk_ergebnisse(ByVal pkt As Messpunkt)
        ' Anzeige in der GUI
        Dim id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring As String
        id = pkt.ID.ToString
        datum = pkt.messdatum
        zeit = pkt.messzeit
        punktnummer = pkt.punktnummer.ToString
        punktname = pkt.punktname
        hz = pkt.horizontalrichtung.ToString
        zw = pkt.zenitwinkel.ToString
        d = pkt.distanz.ToString
        crossInc = pkt.crossInclination.ToString
        lengthInc = pkt.lengthInclination.ToString
        errorstring = pkt.tachymeter_errorstring

        Me._data_table.Rows.Add(id, datum, zeit, punktnummer, punktname, hz, zw, d, crossInc, lengthInc, errorstring)
        DGVData.DataSource = Me._data_table

        Me.measureID = Me._messdaten.freeID()
    End Sub

    Public Sub display_sk_fertig()
        Label_Status_Sk.Text = "Ready"
        Panel_Status_Sk.BackColor = Color.Green
        GroupBox_SK_Solve.Enabled = True
    End Sub

    Private Sub Button_Crosshair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Crosshair.Click

        ' Bild von der Kamera
        Dim bild As HImage
        bild = Me._camera.makeSingleImage()

        ' Bild im zweiten Fenster darstellen
        Dim window As HWindow
        window = HWindowControl2.HalconWindow
        window.ClearWindow()

        Me._kalibrierung = New Calibration(Me._project_dir + "\calibration\")

        Dim folder As String = Me._project_dir + "\crosshair\"
        Dim strichkreuz As New Strichkreuz

        'Bilder auf die Festplatte abspeichern?:
        If (CheckBox_SaveImage_crosshair.Checked = True) Then
            strichkreuz.save_strichkreuz_image(folder, bild)
        End If

        Dim flag As Boolean

        If (ComboBox_crosshair_types.SelectedIndex = 0) Then ' TM5100
            strichkreuz.def_strichkreuz_tm5100(bild, 2)
            flag = strichkreuz.strichkreuz_erkannt
        ElseIf (ComboBox_crosshair_types.SelectedIndex = 1) Then ' TS30
            strichkreuz.def_strichkreuz_ts30(bild, 2)
            flag = strichkreuz.strichkreuz_erkannt
            'ElseIf (ComboBox_crosshair_types.SelectedIndex = 2) Then ' Testbild
            'bild.ReadImage("G:\fadenkreuztest.tif")
            'flag = strichkreuz.strichkreuz_erkannt
            'flag = Me._kalibrierung.def_strichkreuz_tm5100(bild, 2)
        Else
            MessageBox.Show("Bitte eine Strichkreuz verwenden")
        End If

        'Wenn Erkennung erfolgreich, dann
        If (flag = True) Then
            ' Darstellung der Punkte:
            window.DispCross(strichkreuz.zentrum_y, strichkreuz.zentrum_x, 20, 0)
            For i = 0 To strichkreuz.anzahl_strichkreuzpunkte - 1
                window.DispCross(strichkreuz.strichkreuzpunkte_Y(i), strichkreuz.strichkreuzpunkte_X(i), 20, 0)
            Next

            ' GUI
            ComboBox_crosshair_types.BackColor = Color.Green
            GroupBox_Field_of_View.Enabled = True

            ' Speichern auf die Festplatte
            'strichkreuz.export_strichkreuz(folder)

            ' Zuweisung des Strichkreuz zur Kalibrierung
            Me._kalibrierung.strichkreuz = strichkreuz
            Me._kalibrierung.export_strichkreuz()
        End If

        'Dim daten_strichkreuz As New DataTable



        'Dim filename_basic As String = Me._project_dir + "\crosshair\crosshair_" + DateTime.Now.ToString("yyyyMMdd-HHmmssff")




        'If (ComboBox_crosshair_types.SelectedIndex = 0) Then

        '    Dim flag As Boolean
        '    flag = Me._kalibrierung.def_strichkreuz_tm5100(bild, 2)
        '    If (flag = True) Then
        '        TabControl3.SelectedTab() = TabPage_bv1
        '        ' Darstellung des Zentrums im Bild
        '        window.DispCross(Me._kalibrierung.zentrum_Y, Me._kalibrierung.zentrum_X, 20, 0)

        '        ' Position in ein Datenfeld
        '        daten_strichkreuz.Columns.Add(New DataColumn("Y"))
        '        daten_strichkreuz.Columns.Add(New DataColumn("X"))
        '        daten_strichkreuz.Rows.Add(Me._kalibrierung.zentrum_Y.ToString, Me._kalibrierung.zentrum_X.ToString)
        '        Dim i As Integer

        '        For i = 0 To Me._kalibrierung.strichkreuz_punkte_Y.Length - 1
        '            window.DispCross(Me._kalibrierung.strichkreuz_punkte_Y(i), Me._kalibrierung.strichkreuz_punkte_X(i), 20, 0)
        '            daten_strichkreuz.Rows.Add(Me._kalibrierung.strichkreuz_punkte_Y(i).ToString, Me._kalibrierung.strichkreuz_punkte_X(i).ToString)
        '        Next

        '        ' Datenfeld as csv
        '        Dim filename As String
        '        filename = filename_basic + ".csv"
        '        Me._kalibrierung.export_transformation_to_csv(filename)

        '        ' Maske
        '        Me._kalibrierung.save_Maske(filename_basic + ".tif")

        '        ComboBox_crosshair_types.BackColor = Color.Green
        '        GroupBox_Field_of_View.Enabled = True
        '    End If
        'ElseIf (ComboBox_crosshair_types.SelectedIndex = 1) Then
        '    Dim flag As Boolean
        '    flag = Me._kalibrierung.def_strichkreuz_ts30(bild, 0, 2)
        '    If (flag = True) Then
        '        TabControl3.SelectedTab() = TabPage_bv1
        '        ' Darstellung des Zentrums im Bild
        '        window.DispCross(Me._kalibrierung.zentrum_Y, Me._kalibrierung.zentrum_X, 20, 0)

        '        ' Position in ein Datenfeld
        '        daten_strichkreuz.Columns.Add(New DataColumn("Y"))
        '        daten_strichkreuz.Columns.Add(New DataColumn("X"))
        '        daten_strichkreuz.Rows.Add(Me._kalibrierung.zentrum_Y.ToString, Me._kalibrierung.zentrum_X.ToString)
        '        Dim i As Integer

        '        For i = 0 To Me._kalibrierung.strichkreuz_punkte_Y.Length - 1
        '            window.DispCross(Me._kalibrierung.strichkreuz_punkte_Y(i), Me._kalibrierung.strichkreuz_punkte_X(i), 20, 0)
        '            daten_strichkreuz.Rows.Add(Me._kalibrierung.strichkreuz_punkte_Y(i).ToString, Me._kalibrierung.strichkreuz_punkte_X(i).ToString)
        '        Next

        '        ' Datenfeld as csv
        '        Dim filename As String
        '        filename = filename_basic + ".csv"
        '        Me._kalibrierung.export_transformation_to_csv(filename)

        '        ' Maske
        '        Me._kalibrierung.save_Maske(filename_basic + ".tif")

        '        ComboBox_crosshair_types.BackColor = Color.Green
        '        GroupBox_Field_of_View.Enabled = True
        '    End If
        'ElseIf (ComboBox_crosshair_types.SelectedIndex = 2) Then ' Testbild
        '    Dim flag As Boolean
        '    bild.ReadImage("G:\fadenkreuztest.tif")
        '    flag = Me._kalibrierung.def_strichkreuz_tm5100(bild, 2)
        '    If (flag = True) Then
        '        TabControl3.SelectedTab() = TabPage_bv1
        '        bild.DispImage(Me.HWindowControl2.HalconWindow)
        '        ' Darstellung des Zentrums im Bild
        '        window.DispCross(Me._kalibrierung.zentrum_Y, Me._kalibrierung.zentrum_X, 20, 0)

        '        ' Position in ein Datenfeld
        '        daten_strichkreuz.Columns.Add(New DataColumn("Y"))
        '        daten_strichkreuz.Columns.Add(New DataColumn("X"))
        '        daten_strichkreuz.Rows.Add(Me._kalibrierung.zentrum_Y.ToString, Me._kalibrierung.zentrum_X.ToString)
        '        Dim i As Integer

        '        For i = 0 To Me._kalibrierung.strichkreuz_punkte_Y.Length - 1
        '            window.DispCross(Me._kalibrierung.strichkreuz_punkte_Y(i), Me._kalibrierung.strichkreuz_punkte_X(i), 20, 0)
        '            daten_strichkreuz.Rows.Add(Me._kalibrierung.strichkreuz_punkte_Y(i).ToString, Me._kalibrierung.strichkreuz_punkte_X(i).ToString)
        '        Next

        '        ' Datenfeld as csv
        '        Dim filename As String
        '        filename = filename_basic + ".csv"
        '        Me._kalibrierung.export_transformation_to_csv(filename)

        '        ComboBox_crosshair_types.BackColor = Color.Green
        '        GroupBox_Field_of_View.Enabled = True
        '    End If
        'Else
        '    MessageBox.Show("Bitte eine Strichkreuz verwenden")
        'End If


    End Sub

    Private Sub Button_sk_reg_target1_center_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_sk_reg_target1_center.Click
        Me.changeTachyStatusSymbol(2)
        Dim data_object As New LeicaTPS1000data
        Me._tachymetercontrol.get_Angle_All(1, data_object)

        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target1_centerpoint
    End Sub

    Public Sub display_target1_centerpoint(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.logger(daten)

        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target1_centerpoint
        Me._selfcalibration_hz_lage1_target1_center = daten.horizontalrichtung
        Me._selfcalibration_v_lage1_target1_center = daten.zenitwinkel

        TextBox_sk_target1_center_hz.Text = daten.horizontalrichtung.ToString("0.0000")
        TextBox_sk_target1_center_v.Text = daten.zenitwinkel.ToString("0.0000")

    End Sub

    Private Sub Button_sk_reg_target1_circle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_sk_reg_target1_circle.Click
        Me.changeTachyStatusSymbol(2)
        Dim data_object As New LeicaTPS1000data
        Me._tachymetercontrol.get_Angle_All(1, data_object)

        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target1_circlepoint
    End Sub

    Public Sub display_target1_circlepoint(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.logger(daten)

        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)

        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target1_circlepoint
        Me._selfcalibration_hz_lage1_target1_circle = daten.horizontalrichtung
        Me._selfcalibration_v_lage1_target1_circle = daten.zenitwinkel

        TextBox_sk_target1_circle_hz.Text = daten.horizontalrichtung.ToString("0.0000")
        TextBox_sk_target1_circle_v.Text = daten.zenitwinkel.ToString("0.0000")
    End Sub

    Private Sub ComboBox_Sk_typ_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Sk_typ.SelectedIndexChanged
        If (ComboBox_Sk_typ.SelectedIndex = 0) Then ' SK1P
            GroupBox_SKTarget1.Enabled = True
            GroupBox_SKTarget2.Enabled = False
        ElseIf (ComboBox_Sk_typ.SelectedIndex = 1) Then ' SK2P
            GroupBox_SKTarget1.Enabled = True
            GroupBox_SKTarget2.Enabled = False
        ElseIf (ComboBox_Sk_typ.SelectedIndex = 2) Then ' SK4P
            GroupBox_SKTarget1.Enabled = True
            GroupBox_SKTarget2.Enabled = True
        End If
    End Sub

    Private Sub Button_Solve_SK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Solve_SK.Click
        If (ComboBox_Sk_typ.SelectedIndex = 0) Then ' SK1P
            Dim name_skdaten As String
            Dim name_tranformationen As String
            Me._kalibrierung.do_sk1P()
            'name_skdaten = Me._project_dir + "\self_calibration\sk1_" + DateTime.Now.ToString("yyyyMMdd-HHmmssff") + ".csv"
            'Me._kalibrierung.export_sk_daten_to_csv(name_skdaten)
            'name_tranformationen = Me._project_dir + "\self_calibration\transformation_" + DateTime.Now.ToString("yyyyMMdd-HHmmssff") + ".csv"
            'Me._kalibrierung.export_transformation_to_csv(name_tranformationen)
        ElseIf (ComboBox_Sk_typ.SelectedIndex = 1) Then ' SK2P

        ElseIf (ComboBox_Sk_typ.SelectedIndex = 2) Then ' SK4P
            Dim name_skdaten As String
            Dim name_tranformationen As String
            Me._kalibrierung.do_sk4P()
            'name_skdaten = Me._project_dir + "\self_calibration\sk4_" + DateTime.Now.ToString("yyyyMMdd-HHmmssff") + ".csv"
            'Me._kalibrierung.export_sk_daten_to_csv(name_skdaten)
            'name_tranformationen = Me._project_dir + "\self_calibration\transformation_" + DateTime.Now.ToString("yyyyMMdd-HHmmssff") + ".csv"
            'Me._kalibrierung.export_transformation_to_csv(name_tranformationen)
        End If

        MessageBox.Show("Transformationwerte:" + vbCrLf + _
                                Me._kalibrierung.transformation(1, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(2, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(3, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(4, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(5, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(6, 1).ToString)

        MessageBox.Show("Statistik" + vbCrLf + _
                        "Min    vHz: " + (Me._kalibrierung.hzMin * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Max    vHz: " + (Me._kalibrierung.hzMax * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Min    vZd: " + (Me._kalibrierung.zdMin * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Max    vZd: " + (Me._kalibrierung.zdMax * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Mean   vHz: " + (Me._kalibrierung.hzMean * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Mean   vZd: " + (Me._kalibrierung.zdMean * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Stddev vHz: " + (Me._kalibrierung.hzstdDev * 1000).ToString("0.0") + " mgon" + vbCrLf + _
                        "Stddev vZd: " + (Me._kalibrierung.zdstdDev * 1000).ToString("0.0") + " mgon")
    End Sub

    Private Sub Button_sk_reg_target2_center_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_sk_reg_target2_center.Click
        Me.changeTachyStatusSymbol(2)
        Dim data_object As New LeicaTPS1000data
        Me._tachymetercontrol.get_Angle_All(1, data_object)

        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target2_centerpoint
    End Sub

    Public Sub display_target2_centerpoint(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.logger(daten)

        Me.save_point(daten)
        Me.changeTachyStatusSymbol(1)


        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_target2_centerpoint
        Me._selfcalibration_hz_lage1_target2_center = daten.horizontalrichtung
        Me._selfcalibration_v_lage1_target2_center = daten.zenitwinkel

        ' Für Target 2 gilt der Radius, wie für Target 1
        Dim dhz As Double, dzw As Double
        dhz = Me._selfcalibration_hz_lage1_target1_center - Me._selfcalibration_hz_lage1_target1_circle
        dzw = Me._selfcalibration_v_lage1_target1_center - Me._selfcalibration_v_lage1_target1_circle
        Me._selfcalibration_hz_lage1_target2_circle = daten.horizontalrichtung + dhz
        Me._selfcalibration_v_lage1_target2_circle = daten.zenitwinkel + dzw

        TextBox_sk_target2_center_hz.Text = daten.horizontalrichtung.ToString("0.0000")
        TextBox_sk_target2_center_v.Text = daten.zenitwinkel.ToString("0.0000")

    End Sub
#End Region

#Region "Click in Halcon-Window Live"
    Private Sub HWindowControl1_HMouseDown(ByVal sender As System.Object, ByVal e As HalconDotNet.HMouseEventArgs) Handles HWindowControl1.HMouseDown
        If (CheckBox_Measure_Image.Checked = True) Then ' Messen im Bild
            Dim y, x, mb As Integer
            Me.HWindowControl1.HalconWindow.GetMposition(y, x, mb)
            Me.image_measure(x, y)
        Else
            If (rb_zoom.Checked = True) Then
                Dim y, x, mb As Integer
                Me.HWindowControl1.HalconWindow.GetMposition(y, x, mb)
                If (mb = 1) Then ' linker Mbutton
                    Me.zoom_in(x, y)
                ElseIf (mb = 4) Then ' rechter Mbutton
                    Me.zoom_out(x, y)
                End If
            ElseIf (rb_move.Checked = True) Then
                Dim y, x, mb As Integer
                Me.HWindowControl1.HalconWindow.GetMposition(y, x, mb)
                If (mb = 1) Then
                    move_image(x, y)
                End If
            End If
        End If
    End Sub
#End Region
#Region "Zoom und Move im Livebild"
    Private Sub bu_ZoomAndMoveReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bu_ZoomAndMoveReset.Click
        Me._int_zoomfacor = 1
        Me.HWindowControl1.HalconWindow.SetPart(0, 0, Me._int_imageheigth, Me._int_imagewidth)
    End Sub

    Private Sub zoom_in(ByVal x As Double, ByVal y As Double)
        Me._int_zoomfacor = Me._int_zoomfacor * 2
        Dim dy As Integer, dx As Integer
        dy = (Me._int_imageheigth / 2) / Me._int_zoomfacor
        dx = (Me._int_imagewidth / 2) / Me._int_zoomfacor
        Me.HWindowControl1.HalconWindow.SetPart(y - dy, x - dx, y + dy, x + dx)
    End Sub

    Private Sub zoom_out(ByVal x As Double, ByVal y As Double)
        Me._int_zoomfacor = Me._int_zoomfacor / 2
        If (Me._int_zoomfacor <= 1) Then
            Me._int_zoomfacor = 1
            Me.HWindowControl1.HalconWindow.SetPart(0, 0, Me._int_imageheigth, Me._int_imagewidth)
        Else
            Dim dy As Integer, dx As Integer
            dy = (Me._int_imageheigth / 2) / Me._int_zoomfacor
            dx = (Me._int_imagewidth / 2) / Me._int_zoomfacor
            Me.HWindowControl1.HalconWindow.SetPart(y - dy, x - dx, y + dy, x + dx)
        End If
    End Sub

    Private Sub move_image(ByVal x As Double, ByVal y As Double)
        If (Me._int_zoomfacor <= 1) Then
            Me._int_zoomfacor = 1
            Me.HWindowControl1.HalconWindow.SetPart(0, 0, Me._int_imageheigth, Me._int_imagewidth)
        Else
            Dim dy As Integer, dx As Integer
            dy = (Me._int_imageheigth / 2) / Me._int_zoomfacor
            dx = (Me._int_imagewidth / 2) / Me._int_zoomfacor
            Me.HWindowControl1.HalconWindow.SetPart(y - dy, x - dx, y + dy, x + dx)
        End If
    End Sub
#End Region

#Region "Messen im Bild (interaktiv)"
    Private Sub image_measure(ByVal x As Double, ByVal y As Double)
        Me.changeTachyStatusSymbol(2)
        Dim data_object As New LeicaTPS1000data
        data_object.xc = CDbl(x)
        data_object.yc = CDbl(y)

        Me._tachymetercontrol.get_Angle_All(1, data_object)
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.imageXY_To_direction
    End Sub

    Public Sub imageXY_To_direction(ByRef daten As LeicaTPS1000data)
        Dim hz_zw_cI_lI(1, 4) As Double
        Dim xc_yc(1, 2) As Double
        Dim richtung(1, 2) As Double

        hz_zw_cI_lI(1, 1) = daten.horizontalrichtung
        hz_zw_cI_lI(1, 2) = daten.zenitwinkel
        hz_zw_cI_lI(1, 3) = daten.CrossIncline
        hz_zw_cI_lI(1, 4) = daten.LengthIncline

        xc_yc(1, 1) = daten.xc
        xc_yc(1, 2) = daten.yc

        richtung = MessBlickP(hz_zw_cI_lI, xc_yc, Me._kalibrierung.transformation)

        MessageBox.Show("X = " + xc_yc(1, 1).ToString + "  Y = " + xc_yc(1, 2).ToString + vbCrLf + _
                        "Hz Strichkreuz = " + hz_zw_cI_lI(1, 1).ToString + "  Zw Strichkreuz = " + hz_zw_cI_lI(1, 2).ToString + vbCrLf + _
                        "Hz Mausposition = " + richtung(1, 1).ToString + "  Zw Mausposition = " + richtung(1, 2).ToString)
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.imageXY_To_direction

        Dim point As New Messpunkt

        point.horizontalrichtung = daten.horizontalrichtung
        point.zenitwinkel = daten.zenitwinkel
        point.lengthInclination = daten.LengthIncline
        point.crossInclination = daten.CrossIncline

        point.bildkoordinate_X() = daten.xc
        point.bildkoordinate_Y() = daten.yc

        point.trans_horizontalrichtung = richtung(1, 1)
        point.trans_zenitwinkel = richtung(1, 2)


        Me.display_bildmessung(daten)
    End Sub
    Public Sub display_bildmessung(ByRef daten As LeicaTPS1000data)
        display_tachy_errorcodes(daten.errorstring)
        Me.logger(daten)

        Me.save_point(daten)

        Me.changeTachyStatusSymbol(1)
    End Sub
#End Region

#Region "Autoeinstellung der Kamera, funktionieren noch nicht richtig, zurzeit deaktiviert"
    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If (CheckBox6.Checked = True) Then
            Me._camera.set_auto_exposure(True)
        ElseIf (CheckBox6.Checked = False) Then
            Me._camera.set_auto_exposure(False)
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If (CheckBox6.Checked = True) Then
            Me._camera.set_auto_white_balance(True)
        ElseIf (CheckBox6.Checked = False) Then
            Me._camera.set_auto_white_balance(False)
        End If
    End Sub
#End Region
#Region "Testmethoden, können gelöscht werden"
    Public Sub display_test_request_strings(ByRef daten As LeicaTPS1000data)
        Me.logger(daten)
    End Sub

    'Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.fine_adjust(5, 5)
    '    AddHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.refl_gefunden
    'End Sub

    'Public Sub refl_gefunden(ByRef data As LeicaTPS1000data)
    '    RemoveHandler Me._tachymetercontrol.fine_adjust_ready, AddressOf Me.refl_gefunden
    '    MessageBox.Show(data.errorstring)
    'End Sub


    'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.get_ATR_State()
    '    AddHandler Me._tachymetercontrol.get_ATR_State_ready, AddressOf Me.display_atr_state
    'End Sub

    'Public Sub display_atr_state(ByRef data As LeicaTPS1000data)
    '    RemoveHandler Me._tachymetercontrol.get_ATR_State_ready, AddressOf Me.display_atr_state
    '    If (data.errorcode = 0) Then
    '        MessageBox.Show(data.ATR_Status_String)
    '    Else
    '        MessageBox.Show(data.errorstring)
    '    End If
    'End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.set_ATR_State(1)
    'End Sub

    'Public Sub display_set_atr_state_ok(ByRef data As LeicaTPS1000data)

    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.set_ATR_State(0)
    'End Sub




    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    AddHandler Me._tachymetercontrol.lock_in_ready, AddressOf Me.lockin_fertig
    '    Me._tachymetercontrol.lock_in()
    'End Sub

    'Public Sub lockin_fertig(ByRef data As LeicaTPS1000data)
    '    RemoveHandler Me._tachymetercontrol.lock_in_ready, AddressOf Me.lockin_fertig
    '    MessageBox.Show(data.errorstring)
    'End Sub

    'Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    AddHandler Me._tachymetercontrol.get_Lock_Mode_ready, AddressOf Me.display_lock_mode
    '    Me._tachymetercontrol.get_Lock_Mode()
    'End Sub

    'Public Sub display_lock_mode(ByRef data As LeicaTPS1000data)
    '    RemoveHandler Me._tachymetercontrol.get_Lock_Mode_ready, AddressOf Me.display_lock_mode
    '    MessageBox.Show(data.LOCKMode_Status_string)
    'End Sub

    'Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.set_Lock_Mode(1)
    'End Sub

    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me._tachymetercontrol.set_Lock_Mode(0)
    'End Sub
#End Region


    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)



        MessageBox.Show("Zentrum X = " & Me._kalibrierung.strichkreuz.zentrum_x.ToString & vbCrLf & _
                        "Zentrum Y = " & Me._kalibrierung.strichkreuz.zentrum_y.ToString & vbCrLf)

        MessageBox.Show("Transformationwerte:" + vbCrLf + _
                                Me._kalibrierung.transformation(1, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(2, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(3, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(4, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(5, 1).ToString + vbCrLf + _
                                Me._kalibrierung.transformation(6, 1).ToString)
    End Sub
    '#tmp_uEye: Dies ist eine temporäre Einbingung der uEye-dlls zum Testen der Kamerafunktionen. Diese Funktionen nicht für zukünftige Programmteile nutzen,
    '           weil die uEye-dlls mittelfristig wieder entfernt, bzw. deaktiviert werden.
#Region "uEye-Modus"
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me._camera_uEye = New uEyeCamera(AxuEyeCam1)
        Me._camera_uEye.Init()
        Me.setCameraButtons("connected")
        ChangeCameraStatusSymbol(2)
        Me._uEye_dll_activatet = True

        ComboBox_uEye_binning.Items.Add(1)
        ComboBox_uEye_binning.Items.Add(2)
        ComboBox_uEye_binning.Items.Add(3)
        ComboBox_uEye_binning.Items.Add(4)
        ComboBox_uEye_binning.Items.Add(5)
        ComboBox_uEye_binning.Items.Add(6)
        ComboBox_uEye_binning.Items.Add(8)
        ComboBox_uEye_binning.Items.Add(16)

        ComboBox_uEye_Subsampling.Items.Add(1)
        ComboBox_uEye_Subsampling.Items.Add(2)
        ComboBox_uEye_Subsampling.Items.Add(3)
        ComboBox_uEye_Subsampling.Items.Add(4)
        ComboBox_uEye_Subsampling.Items.Add(5)
        ComboBox_uEye_Subsampling.Items.Add(6)
        ComboBox_uEye_Subsampling.Items.Add(8)
        ComboBox_uEye_Subsampling.Items.Add(16)

        'Me._isCameraConnect = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me._camera_uEye.exit_camera()
        ChangeCameraStatusSymbol(0)
        Me._uEye_dll_activatet = False
        'Me._isCameraConnect = False
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim FileName As String
        Dim SaveFileDialog1 As New SaveFileDialog
        With SaveFileDialog1
            .Filter = "BMP format (*.bmp)|*.bmp|Alle Dateien (*.*)|*.*"
            .FilterIndex = 1
            .InitialDirectory = Me._project_dir
            .Title = "Speichern als BMP-Datei *.bmp"
            If (.ShowDialog() = DialogResult.OK) Then
                FileName = SaveFileDialog1.FileName
                Me._camera_uEye.save_Image(FileName)
            Else
                Exit Sub
            End If
        End With
    End Sub

    Private Sub Bu_camera_property_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bu_camera_property.Click
        Me._camera_uEye.camera_proberties()
    End Sub

    Private Sub ComboBox_uEye_binning_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_uEye_binning.SelectedIndexChanged
        Dim antwort As Boolean = False
        Select Case (ComboBox_uEye_binning.SelectedIndex)
            Case 0
                antwort = Me._camera_uEye.set_binning(1)
            Case 1
                antwort = Me._camera_uEye.set_binning(2)
            Case 2
                antwort = Me._camera_uEye.set_binning(3)
            Case 3
                antwort = Me._camera_uEye.set_binning(4)
            Case 4
                antwort = Me._camera_uEye.set_binning(5)
            Case 5
                antwort = Me._camera_uEye.set_binning(6)
            Case 6
                antwort = Me._camera_uEye.set_binning(8)
            Case 7
                antwort = Me._camera_uEye.set_binning(16)
        End Select
        If (antwort = True) Then
            ComboBox_uEye_binning.BackColor = Color.Green
            ComboBox_uEye_Subsampling.BackColor = Color.White
        ElseIf (antwort = False) Then
            ComboBox_uEye_binning.BackColor = Color.Red
            ComboBox_uEye_Subsampling.BackColor = Color.White
        End If
    End Sub

    Private Sub ComboBox_uEye_Subsampling_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_uEye_Subsampling.SelectedIndexChanged
        Dim antwort As Boolean = False
        Select Case (ComboBox_uEye_Subsampling.SelectedIndex)
            Case 0
                antwort = Me._camera_uEye.set_subsampling(1)
            Case 1
                antwort = Me._camera_uEye.set_subsampling(2)
            Case 2
                antwort = Me._camera_uEye.set_subsampling(3)
            Case 3
                antwort = Me._camera_uEye.set_subsampling(4)
            Case 4
                antwort = Me._camera_uEye.set_subsampling(5)
            Case 5
                antwort = Me._camera_uEye.set_subsampling(6)
            Case 6
                antwort = Me._camera_uEye.set_subsampling(8)
            Case 7
                antwort = Me._camera_uEye.set_subsampling(16)
        End Select

        If (antwort = True) Then
            ComboBox_uEye_binning.BackColor = Color.White
            ComboBox_uEye_Subsampling.BackColor = Color.Green
        ElseIf (antwort = False) Then
            ComboBox_uEye_binning.BackColor = Color.White
            ComboBox_uEye_Subsampling.BackColor = Color.Red
        End If

    End Sub

#End Region

    
    
#Region "Resolutiontest Thiery"

    'Button - Stützpunkte

    ''' <summary>
    ''' Misst 1. Messung Zentrum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messe_Zentrum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messe_Zentrum.Click

        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum

    End Sub

    ''' <summary>
    ''' Misst 1. Messung Rand Oben
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messe_Rand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messe_Rand.Click

        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand

    End Sub

    ''' <summary>
    ''' Misst 2. Messung Zentrum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messung2_Zentrum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messung2_Zentrum.Click

        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum_M2

    End Sub

    ''' <summary>
    ''' Misst 2. Messung Shift
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messung2_Shift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messung2_Shift.Click

        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand_M2

    End Sub

    ''' <summary>
    ''' Misst 3. Messung Zentrum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messung3_Zentrum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messung3_Zentrum.Click

        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum_M3

    End Sub

    ''' <summary>
    ''' Misst 3. Messung Shift
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Messung3_Shift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Messung3_Shift.Click
        'Status der Messung
        Me.changeTachyStatusSymbol(2)
        'Objekt mit Ausgabedaten erzeugen
        Dim data_object As New LeicaTPS1000data
        'Winkel messen
        Me._tachymetercontrol.get_Angle_All(1, data_object)
        'Handler setzen und kommende Funktion angeben
        AddHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand_M3
    End Sub

    'Button - Reset

    ''' <summary>
    ''' Setzt Werte in _resolutiontest zurück und lädt GUI "neu"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_SP_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SP_Reset.Click

        'Buttons reseten
        Me.Button_Messe_Zentrum.Enabled = True
        Me.Button_Messe_Rand.Enabled = True
        Me.Button_Messung2_Zentrum.Enabled = True
        Me.Button_Messung3_Zentrum.Enabled = True
        Me.Button_Messung2_Shift.Enabled = True
        Me.Button_Messung3_Shift.Enabled = True
        Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = False
        Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = False
        Me.Button_25_Bilder_Komplett.Enabled = False
        Me.CheckBox_Hz_Shift.Checked = False
        Me.CheckBox_Vz_Shift.Checked = False

        'Variablen reseten
        _resolutiontest.reset()

    End Sub

    'Button - Messungen

    ''' <summary>
    ''' Messung mit 25 Bildern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_25_Bilder_Komplett_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_25_Bilder_Komplett.Click

        Me.TextBox_Messzeit.Text = DateTime.Now.ToString("dd.MM.yyyy") + "_" + _
                                   DateTime.Now.ToString("HH.mm.ss")

        'Modi
        '1: 25 Felder
        '2: 9 Felder (Kreuz)
        '3: 9 Felder (gedrehtes Kreuz)  
        Dim mode As Integer = 1
        _resolutiontest.Starte_Messung(_tachymetercontrol, _camera_uEye, _project_dir, _
                                            TextBox_Name_AVMessung.Text, mode, _
                                            TextBox_Wartezeit.Text, TextBox_Messzeit.Text, _
                                            CheckBox_Hz_Shift.Checked, CheckBox_Vz_Shift.Checked)

    End Sub

    ''' <summary>
    ''' Messung mir 9 Bildern mit aufrechtem Kreuz
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_9_Bilder_Kreuz_Aufrecht_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_9_Bilder_Kreuz_Aufrecht.Click


        Me.TextBox_Messzeit.Text = DateTime.Now.ToString("dd.MM.yyyy") + "_" + _
                                   DateTime.Now.ToString("HH.mm.ss")

        'Modi
        '1: 25 Felder
        '2: 9 Felder (Kreuz)
        '3: 9 Felder (gedrehtes Kreuz)  
        Dim mode As Integer = 2
        _resolutiontest.Starte_Messung(_tachymetercontrol, _camera_uEye, _project_dir, _
                                            TextBox_Name_AVMessung.Text, mode, _
                                            TextBox_Wartezeit.Text, TextBox_Messzeit.Text, _
                                            CheckBox_Hz_Shift.Checked, CheckBox_Vz_Shift.Checked)

    End Sub

    ''' <summary>
    ''' Messung mit 9 Bildern mit gedrehtem Kreuz
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_9_Bilder_Kreuz_Gedreht_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_9_Bilder_Kreuz_Gedreht.Click

        Me.TextBox_Messzeit.Text = DateTime.Now.ToString("dd.MM.yyyy") + "_" + _
                                   DateTime.Now.ToString("HH.mm.ss")

        'Modi
        '1: 25 Felder
        '2: 9 Felder (Kreuz)
        '3: 9 Felder (gedrehtes Kreuz)  
        Dim mode As Integer = 3
        _resolutiontest.Starte_Messung(_tachymetercontrol, _camera_uEye, _project_dir, _
                                            TextBox_Name_AVMessung.Text, mode, _
                                            TextBox_Wartezeit.Text, TextBox_Messzeit.Text, _
                                            CheckBox_Hz_Shift.Checked, CheckBox_Vz_Shift.Checked)

    End Sub
    
    'Darstellung in der GUI und Übergabe der Daten

    ''' <summary>
    ''' Übergibt 1. Messung Zentrum an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Zentrum(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._zentrum_Hz = daten.horizontalrichtung
        _resolutiontest._zentrum_Vz = daten.zenitwinkel

        'Button enablen
        Me.Button_Messe_Zentrum.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung1_zentrum = True

        If _resolutiontest._zentrum_Hz > 300 And _resolutiontest._zentrum_Hz < 400 _
           And _resolutiontest._rand_Hz > 0 And _resolutiontest._rand_Hz < 100 Then

            MsgBox("Messungen überschreiten 399gon/0gon Grenze. Daten werden zurückgesetzt. Bitte Zapfen verstellen!", _
                   MsgBoxStyle.Critical, "Verfahrweg überschreitet Bereich!")

            Me.Button_Messe_Zentrum.Enabled = True
            Me.Button_Messe_Rand.Enabled = True
            Me.Button_Messung2_Zentrum.Enabled = True
            Me.Button_Messung3_Zentrum.Enabled = True
            Me.Button_Messung2_Shift.Enabled = True
            Me.Button_Messung3_Shift.Enabled = True
            Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = False
            Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = False
            Me.Button_25_Bilder_Komplett.Enabled = False
            Me.CheckBox_Hz_Shift.Checked = False
            Me.CheckBox_Vz_Shift.Checked = False

            Call _resolutiontest.reset()

        End If

        If _resolutiontest._messung1_zentrum = True And _
           _resolutiontest._messung1_rand = True Then

            Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = True
            Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = True
            Me.Button_25_Bilder_Komplett.Enabled = True

        End If

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum

    End Sub

    ''' <summary>
    ''' Übergibt 1. Messung Rand an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Rand(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._rand_Hz = daten.horizontalrichtung
        _resolutiontest._rand_Vz = daten.zenitwinkel

        'Button enablen
        Me.Button_Messe_Rand.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung1_rand = True

        If _resolutiontest._zentrum_Hz > 300 And _resolutiontest._zentrum_Hz < 400 _
           And _resolutiontest._rand_Hz > 0 And _resolutiontest._rand_Hz < 100 Then

            MsgBox("Messungen überschreiten 399gon/0gon Grenze. Daten werden zurückgesetzt. Bitte Zapfen verstellen!", _
                   MsgBoxStyle.Critical, "Verfahrweg überschreitet Bereich!")

            Me.Button_Messe_Zentrum.Enabled = True
            Me.Button_Messe_Rand.Enabled = True
            Me.Button_Messung2_Zentrum.Enabled = True
            Me.Button_Messung3_Zentrum.Enabled = True
            Me.Button_Messung2_Shift.Enabled = True
            Me.Button_Messung3_Shift.Enabled = True
            Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = False
            Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = False
            Me.Button_25_Bilder_Komplett.Enabled = False
            Me.CheckBox_Hz_Shift.Checked = False
            Me.CheckBox_Vz_Shift.Checked = False

            Call _resolutiontest.reset()

        End If

        If _resolutiontest._messung1_zentrum = True And _
           _resolutiontest._messung1_rand = True Then

            Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = True
            Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = True
            Me.Button_25_Bilder_Komplett.Enabled = True

        End If

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand

    End Sub

    ''' <summary>
    ''' Übergibt 2. Messung Zentrum an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Zentrum_M2(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._zentrum_Hz_M2 = daten.horizontalrichtung
        _resolutiontest._zentrum_Vz_M2 = daten.zenitwinkel

        'Button enablen
        Me.Button_Messung2_Zentrum.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung2_zentrum = True

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum_M2

    End Sub

    ''' <summary>
    ''' Übergibt 2. Messung Rand an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Rand_M2(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._rand_Hz_M2 = daten.horizontalrichtung
        _resolutiontest._rand_Vz_M2 = daten.zenitwinkel

        'Button enablen
        Me.Button_Messung2_Shift.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung2_rand = True

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand_M2

    End Sub

    ''' <summary>
    ''' Übergibt 3. Messung Zentrum an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Rand_M3(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._rand_Hz_M3 = daten.horizontalrichtung
        _resolutiontest._rand_Vz_M3 = daten.zenitwinkel

        'Button enablen
        Me.Button_Messung3_Shift.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung3_rand = True

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Rand_M3

    End Sub

    ''' <summary>
    ''' Übergibt 3. Messung Rand an _resolutiontest und stellt in GUI dar
    ''' </summary>
    ''' <param name="daten"></param>
    ''' <remarks></remarks>
    Public Sub display_get_Angle_Zentrum_M3(ByRef daten As LeicaTPS1000data)

        'Errorcode anzeigen
        display_tachy_errorcodes(daten.errorstring)

        'Hz und Vz an ResolutionTest übergeben
        _resolutiontest._zentrum_Hz_M3 = daten.horizontalrichtung
        _resolutiontest._zentrum_Vz_M3 = daten.zenitwinkel

        'Button enablen
        Me.Button_Messung3_Zentrum.Enabled = False

        'Messung aktivieren
        _resolutiontest._messung3_zentrum = True

        'Status der Messung
        Me.changeTachyStatusSymbol(1)
        'Handler und Pfad kommender Funktion entfernen
        RemoveHandler Me._tachymetercontrol.get_Angle_All_ready, AddressOf Me.display_get_Angle_Zentrum_M3

    End Sub

    Private Sub Button_Snapshot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Snapshot.Click

        Try

            Me.TextBox_Messzeit.Text = DateTime.Now.ToString("dd.MM.yyyy") + "_" + _
                                       DateTime.Now.ToString("HH.mm.ss")

            _resolutiontest.Do_Snapshot(_camera_uEye, _project_dir, TextBox_Name_AVMessung.Text, _
                                        TextBox_Messzeit.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

#End Region

    

    Private Sub CheckBox_KreuzEinschalten_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_KreuzEinschalten.CheckedChanged
        If CheckBox_KreuzEinschalten.Checked = True Then
            Label_X1.Visible = True
            Label_X2.Visible = True
            Label_X3.Visible = True
            Label_X4.Visible = True
            Label_X5.Visible = True
        Else
            Label_X1.Visible = False
            Label_X2.Visible = False
            Label_X3.Visible = False
            Label_X4.Visible = False
            Label_X5.Visible = False
        End If
    End Sub
End Class
