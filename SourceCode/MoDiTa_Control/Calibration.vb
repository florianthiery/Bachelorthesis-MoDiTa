Public Class Calibration
    Private _strichkreuz As Strichkreuz
    Private _strichkreuz_flag As Boolean = False

    Private _skdaten() As SKDaten
    Private _skdaten_flag As Boolean = False

    Private _save_folder As String
    Private _calibration_time As Date
    Private Const _time_String_format As String = "yyyyMMdd-HHmmssff"

    Private _transformation(6, 1) As Double

    ' SK Statistiken
    Private _vhzMax As Double, _vhzMin As Double, _vzdMax As Double, _vzdMin As Double
    Private _hzStddev As Double, _zdStddev As Double
    Private _hzMean As Double, _zdMean As Double

    ''' <summary>
    ''' Erzeugt eine neue Instanz.
    ''' </summary>
    ''' <param name="folder">
    ''' Ordner in dem die Kalibrierung abgespeichert wird.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal folder As String)
        Me._calibration_time = Date.Now
        Me._save_folder = folder + Me._calibration_time.ToString(Calibration._time_String_format) + "\"

    End Sub

    Public Sub New()
    End Sub

    Public ReadOnly Property transformation() As Double(,)
        Get
            Return Me._transformation
        End Get
    End Property

    Public Sub import_latest_calibration(ByVal folder As String)
        Dim calibrationlist() As String

        calibrationlist = System.IO.Directory.GetDirectories(folder)

        Me._save_folder = Me.latest_calibration(calibrationlist) + "\"

        ' Strichkreuz einlesen
        Me._strichkreuz.import_latest_strichkreuz(Me._save_folder + "crosshair\")

        ' Skdatensätze einlesen
        Me.load_all_skdaten(Me._save_folder + "skdatas\")
    End Sub

    Public Sub import_calibration(ByVal folder As String)
        Me._save_folder = folder
        ' Strichkreuz einlesen
        Me._strichkreuz = New Strichkreuz
        Me._strichkreuz.import_latest_strichkreuz(Me._save_folder + "crosshair\")

        ' Skdatensätze einlesen
        Me.load_all_skdaten(Me._save_folder + "skdatas\")

        If (Me._skdaten_flag = True And Me._strichkreuz_flag = True) Then
            ' Ist Strichkreuz eingeladen und ein Sk-Satz eingelesen, Sk rechnen
            Me.do_sk()
        End If
    End Sub

    Private Sub load_all_skdaten(ByVal folder As String)
        Dim fileliste() As String
        If (IO.Directory.Exists(folder) = False) Then
            MessageBox.Show("SK-Files does not exist.")
            Exit Sub
        End If
        fileliste = System.IO.Directory.GetDirectories(folder)

        Dim pos As Integer = 0
        Dim temp_array_skdaten(pos) As SKDaten

        For i = 0 To fileliste.Length - 1 Step 1
            Dim temp1 As String = New IO.FileInfo(fileliste(i)).Extension
            Dim temp2 As String = New IO.FileInfo(fileliste(i)).Name
            If (InStr(temp1, ".csv") <> 0 And InStr(temp2, "skdaten") <> 0) Then
                Dim temp3() As String = Split(temp2, "_")
                If (temp3.Length = 2) Then
                    Dim tempsk As New SKDaten
                    tempsk.skID() = Integer.Parse(temp3(1), "D4", Nothing)
                    tempsk.csv_To_punkte(fileliste(i))
                    tempsk.csv_dir = folder

                    ReDim Preserve temp_array_skdaten(pos)
                    temp_array_skdaten(pos) = tempsk
                    pos += 1
                End If
            End If
        Next

        If (pos > 0) Then
            Me._skdaten_flag = True
            ' Sortieren: skid = Position im Array
            ReDim Me._skdaten(temp_array_skdaten.Length - 1)
            For j = 0 To temp_array_skdaten.Length - 1 Step 1
                If (temp_array_skdaten(j).skID < Me._skdaten.Length) Then
                    Me._skdaten(temp_array_skdaten(j).skID) = temp_array_skdaten(j)
                End If
            Next
        End If
    End Sub

    Private Function latest_calibration(ByVal liste() As String) As String
        ' Function sucht letztes (neustes) Verzeichnis, mit Hilfe des Datum im Dateinamen(!)
        Dim maxindex As Integer = 0
        Dim maxdate As Date = Date.ParseExact("19000101-01000000", Calibration._time_String_format, Nothing)

        For i = 0 To liste.Length - 1 Step 1
            Dim temp1 As String = New IO.DirectoryInfo(liste(i)).Name
            Dim tempdate As Date
            tempdate = Date.ParseExact(temp1, Calibration._time_String_format, Nothing)
            If (Date.Compare(maxdate, tempdate) = -1) Then
                maxindex = i
                maxdate = tempdate
            End If
        Next
        Return liste(maxindex)
    End Function


    Public Sub export_strichkreuz()
        Me._strichkreuz.export_strichkreuz(Me._save_folder + "crosshair\")
    End Sub
    Public Property strichkreuz() As Strichkreuz
        Get
            Return Me._strichkreuz
        End Get
        Set(ByVal value As Strichkreuz)
            Me._strichkreuz = value
        End Set
    End Property

    ''' <summary>
    ''' Fügt ein neuen SKDatensatz hinzu (= neues Ziel)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add_new_skdaten() As Integer
        Dim length As Integer
        Try
            length = Me._skdaten.Length
            ReDim Preserve Me._skdaten(length)
        Catch ex As NullReferenceException
            Me._skdaten(0) = New SKDaten
            length = 0
        End Try
        Me._skdaten(length) = New SKDaten()
        Me._skdaten(length).skID() = length
        Me._skdaten(length).csv_dir() = Me._save_folder + "skdatas\"
        Return length
    End Function

    ''' <summary>
    ''' Fügt ein Messpunkt hinzu
    ''' </summary>
    ''' <param name="zielnummer">
    ''' Nummer des Ziel/Datensatz
    ''' </param>
    ''' <param name="punkt"></param>
    ''' <remarks></remarks>
    Public Sub add_sk_punkt(ByVal zielnummer As Integer, ByVal punkt As Messpunkt)
        If (zielnummer = Me._skdaten.Length) Then
            'Legt einen neuen Datensatz an
            add_new_skdaten()
        ElseIf (zielnummer > Me._skdaten.Length) Then
            'Liegt die Zielnummer deutlich über Arraylänge, wird die Methode beendet.
            Exit Sub
        End If

        Me._skdaten(zielnummer).add_punkt(punkt)
    End Sub

    Public Sub do_sk()
        ' ToDo: Flexibler....
        If (Me._skdaten.Length = 1) Then
            Me.do_sk1P()

        ElseIf (Me._skdaten.Length = 4) Then
            Me.do_sk4P()
        End If
    End Sub

    Public Sub do_sk1P()

        ' Berechnung der Transformationsmatrix
        Me._transformation = SK1P(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._skdaten(0).matrix_Gewicht)

        ' Für die Statistik:
        Dim skblick(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick = SKBlickP(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._skdaten(0).matrix_Gewicht, Me._transformation)

        Dim n As Integer
        Dim i As Integer
        n = Me._skdaten(0).anzahl_messpunkte

        Dim vHz(n) As Double, vZd(n) As Double
        Dim mess(1, 2) As Double

        For i = 1 To n Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._skdaten(0).get_punkt(i - 1).gewicht_Selbstkalibrierung = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._transformation)
                vHz(i) = skblick(1, 1) - mess(1, 1)
                vZd(i) = skblick(1, 2) - mess(1, 2)
            End If
        Next

        Me._vhzMax = vHz.Max()
        Me._vhzMin = vHz.Min()
        Me._vzdMax = vZd.Max()
        Me._vzdMin = vZd.Min()
        Me._hzMean = mean(vHz)
        Me._zdMean = mean(vZd)
        Me._hzStddev = stddev(vHz)
        Me._zdStddev = stddev(vZd)

    End Sub

    Public Sub do_sk4P()

        Me._transformation = SK4P(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._skdaten(0).matrix_Gewicht, _
                                                                Me._skdaten(1).matrix_Theo, Me._skdaten(1).matrix_Kamera, Me._skdaten(1).matrix_Gewicht, _
                                                                Me._skdaten(2).matrix_Theo, Me._skdaten(2).matrix_Kamera, Me._skdaten(2).matrix_Gewicht, _
                                                                Me._skdaten(3).matrix_Theo, Me._skdaten(3).matrix_Kamera, Me._skdaten(3).matrix_Gewicht)

        Dim count_insgesamt As Integer
        Dim offset As Integer
        count_insgesamt = Me._skdaten(0).anzahl_messpunkte + Me._skdaten(1).anzahl_messpunkte + Me._skdaten(2).anzahl_messpunkte + Me._skdaten(3).anzahl_messpunkte
        Dim vHz(count_insgesamt) As Double, vZd(count_insgesamt) As Double

        ' Für die Statistik:
        Dim skblick1(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick1 = SKBlickP(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._skdaten(0).matrix_Gewicht, Me._transformation)

        Dim n As Integer
        Dim i As Integer
        n = Me._skdaten(0).anzahl_messpunkte

        Dim mess(1, 2) As Double

        For i = 1 To n Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._skdaten(0).get_punkt(i - 1).gewicht_Selbstkalibrierung = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._skdaten(0).matrix_Theo, Me._skdaten(0).matrix_Kamera, Me._transformation)
                vHz(i) = skblick1(1, 1) - mess(1, 1)
                vZd(i) = skblick1(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick2(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick2 = SKBlickP(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(1).matrix_Theo, Me._skdaten(1).matrix_Kamera, Me._skdaten(1).matrix_Gewicht, Me._transformation)

        n = Me._skdaten(1).anzahl_messpunkte

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._skdaten(1).get_punkt(i - 1).gewicht_Selbstkalibrierung = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._skdaten(1).matrix_Theo, Me._skdaten(1).matrix_Kamera, Me._transformation)
                vHz(i) = skblick2(1, 1) - mess(1, 1)
                vZd(i) = skblick2(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick3(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick3 = SKBlickP(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(2).matrix_Theo, Me._skdaten(2).matrix_Kamera, Me._skdaten(2).matrix_Gewicht, Me._transformation)

        n = Me._skdaten(2).anzahl_messpunkte

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._skdaten(2).get_punkt(i - 1).gewicht_Selbstkalibrierung = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._skdaten(2).matrix_Theo, Me._skdaten(2).matrix_Kamera, Me._transformation)
                vHz(i) = skblick3(1, 1) - mess(1, 1)
                vZd(i) = skblick3(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick4(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick4 = SKBlickP(Me.strichkreuz.zentrum_x, Me.strichkreuz.zentrum_y, Me._skdaten(3).matrix_Theo, Me._skdaten(3).matrix_Kamera, Me._skdaten(3).matrix_Gewicht, Me._transformation)

        n = Me._skdaten(3).anzahl_messpunkte

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._skdaten(3).get_punkt(i - 1).gewicht_Selbstkalibrierung = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._skdaten(3).matrix_Theo, Me._skdaten(3).matrix_Kamera, Me._transformation)
                vHz(i) = skblick4(1, 1) - mess(1, 1)
                vZd(i) = skblick4(1, 2) - mess(1, 2)
            End If
        Next

        Me._vhzMax = vHz.Max()
        Me._vhzMin = vHz.Min()
        Me._vzdMax = vZd.Max()
        Me._vzdMin = vZd.Min()
        Me._hzMean = mean(vHz)
        Me._zdMean = mean(vZd)
        Me._hzStddev = stddev(vHz)
        Me._zdStddev = stddev(vZd)


    End Sub

#Region "Abweichungen"
    Public ReadOnly Property hzMax() As Double
        Get
            Return Me._vhzMax
        End Get
    End Property
    Public ReadOnly Property hzMin() As Double
        Get
            Return Me._vhzMin
        End Get
    End Property
    Public ReadOnly Property zdMax() As Double
        Get
            Return Me._vzdMax
        End Get
    End Property
    Public ReadOnly Property zdMin() As Double
        Get
            Return Me._vzdMin
        End Get
    End Property
    Public ReadOnly Property hzstdDev() As Double
        Get
            Return Me._hzStddev
        End Get
    End Property
    Public ReadOnly Property zdstdDev() As Double
        Get
            Return Me._zdStddev
        End Get
    End Property
    Public ReadOnly Property hzMean() As Double
        Get
            Return Me._hzMean
        End Get
    End Property
    Public ReadOnly Property zdMean() As Double
        Get
            Return Me._zdMean
        End Get
    End Property

#End Region

End Class
