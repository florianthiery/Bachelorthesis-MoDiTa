Public Class Calibration_OLD

    Private _strichkreuz As Strichkreuz


    Private _maske_strichkreuz As HRegion
    Private _strichkreuz_punkte_Y(), _strichkreuz_punkte_X() As Double
    Private _zentrum_Y, _zentrum_X As Double
    Private _is_strichkreuz_definiert As Boolean = False
    'Private _anzahl_strichkreuz_punkte As Integer

    ' Dient hauptsächlich zur Idendifikation der Kalibrierung
    Private _kalibrierungs_zeit As Date

    Private _skdaten(0) As SKDaten
    Private _folder As String


    Private _transformation(6, 1) As Double

    ' Für SK Ziel 1 Lage 1
    Private _sk_daten1 As New ArrayList
    Private _array_Theodlit_Hz_Zd_CI_LI1(,) As Double
    Private _array_Kamera_XC_YC1(,) As Double
    Private _array_Gewicht1(,) As Double

    ' Für SK Ziel 1 Lage 2
    Private _sk_daten2 As New ArrayList
    Private _array_Theodlit_Hz_Zd_CI_LI2(,) As Double
    Private _array_Kamera_XC_YC2(,) As Double
    Private _array_Gewicht2(,) As Double

    ' Für SK Ziel 2 Lage 1
    Private _sk_daten3 As New ArrayList
    Private _array_Theodlit_Hz_Zd_CI_LI3(,) As Double
    Private _array_Kamera_XC_YC3(,) As Double
    Private _array_Gewicht3(,) As Double

    ' Für SK Ziel 2 Lage 2
    Private _sk_daten4 As New ArrayList
    Private _array_Theodlit_Hz_Zd_CI_LI4(,) As Double
    Private _array_Kamera_XC_YC4(,) As Double
    Private _array_Gewicht4(,) As Double

    ' SK Statistiken
    Private _vhzMax As Double, _vhzMin As Double, _vzdMax As Double, _vzdMin As Double
    Private _hzStddev As Double, _zdStddev As Double
    Private _hzMean As Double, _zdMean As Double
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

    Public Sub New()
    End Sub

    Public Sub New(ByVal calibration_folder As String)
        Me._kalibrierungs_zeit = Date.Now
        Me._folder = calibration_folder + Me._kalibrierungs_zeit.ToString("yyyyMMdd-HHmmssff") + "\"
    End Sub

    Public Property strichkreuz() As Strichkreuz
        Get
            Return Me._strichkreuz
        End Get
        Set(ByVal value As Strichkreuz)
            Me._strichkreuz = value
        End Set
    End Property

    Public Property folder() As String
        Get
            Return Me._folder
        End Get
        Set(ByVal value As String)
            Me._folder = value
        End Set
    End Property

    ''' <summary>
    ''' Fügt ein neuen SKDatensatz hinzu (= neues Ziel)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add_new_skdaten() As Integer
        Dim length As Integer
        length = Me._skdaten.Length
        ReDim Preserve Me._skdaten(length)
        Me._skdaten(length) = New SKDaten()
        Me._skdaten(length).skID() = length
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

#Region "Alt"
    ''' <summary>
    ''' Fügt eine Messung in das Array für SK ein. Ziel 1 Lage 1
    ''' </summary>
    ''' <param name="hz"></param>
    ''' <param name="zd"></param>
    ''' <param name="crossInc"></param>
    ''' <param name="lengthInc"></param>
    ''' <param name="xc"></param>
    ''' <param name="yc"></param>
    ''' <param name="gewicht"></param>
    ''' <remarks></remarks>
    Public Sub add_sk_daten1(ByVal hz As Double, ByVal zd As Double, ByVal crossInc As Double, ByVal lengthInc As Double, ByVal xc As Double, ByVal yc As Double, ByVal gewicht As Double)
        Dim row(7) As Double
        row(1) = hz
        row(2) = zd
        row(3) = crossInc
        row(4) = lengthInc
        row(5) = xc
        row(6) = yc
        row(7) = gewicht

        Me._sk_daten1.Add(row)
    End Sub

    ' Macht aus der eine Arraylist, drei Arrays für die Berechnug der Selbstkalibrierung, Ziel 1 Lage 1
    Private Sub build_sk_matrizen1()
        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten1.Count

        ReDim Me._array_Theodlit_Hz_Zd_CI_LI1(n, 4)
        ReDim Me._array_Kamera_XC_YC1(n, 2)
        ReDim Me._array_Gewicht1(n, 1)

        For i = 1 To n Step 1
            Me._array_Theodlit_Hz_Zd_CI_LI1(i, 1) = Me._sk_daten1.Item(i - 1)(1)
            Me._array_Theodlit_Hz_Zd_CI_LI1(i, 2) = Me._sk_daten1.Item(i - 1)(2)
            Me._array_Theodlit_Hz_Zd_CI_LI1(i, 3) = Me._sk_daten1.Item(i - 1)(3)
            Me._array_Theodlit_Hz_Zd_CI_LI1(i, 4) = Me._sk_daten1.Item(i - 1)(4)

            Me._array_Kamera_XC_YC1(i, 1) = Me._sk_daten1.Item(i - 1)(5)
            Me._array_Kamera_XC_YC1(i, 2) = Me._sk_daten1.Item(i - 1)(6)

            Me._array_Gewicht1(i, 1) = Me._sk_daten1.Item(i - 1)(7)
        Next
    End Sub

    Public Sub do_sk1P()
        ' Erzeugen der Matrizen
        Me.build_sk_matrizen1()
        ' Berechnung der Transformationsmatrix
        Me._transformation = SK1P(Me._zentrum_X, Me._zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._array_Gewicht1)

        ' Für die Statistik:
        Dim skblick(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick = SKBlickP(Me.zentrum_X, Me.zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._array_Gewicht1, Me._transformation)

        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten1.Count

        Dim vHz(n) As Double, vZd(n) As Double
        Dim mess(1, 2) As Double

        For i = 1 To n Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._sk_daten1.Item(i - 1)(7) = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._transformation)
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

    ''' <summary>
    ''' Fügt eine Messung in das Array für SK ein. Ziel 1 Lage 2
    ''' </summary>
    ''' <param name="hz"></param>
    ''' <param name="zd"></param>
    ''' <param name="crossInc"></param>
    ''' <param name="lengthInc"></param>
    ''' <param name="xc"></param>
    ''' <param name="yc"></param>
    ''' <param name="gewicht"></param>
    ''' <remarks></remarks>
    Public Sub add_sk_daten2(ByVal hz As Double, ByVal zd As Double, ByVal crossInc As Double, ByVal lengthInc As Double, ByVal xc As Double, ByVal yc As Double, ByVal gewicht As Double)
        Dim row(7) As Double
        row(1) = hz
        row(2) = zd
        row(3) = crossInc
        row(4) = lengthInc
        row(5) = xc
        row(6) = yc
        row(7) = gewicht

        Me._sk_daten2.Add(row)
    End Sub

    ' Macht aus der eine Arraylist, drei Arrays für die Berechnug der Selbstkalibrierung, Ziel 1 Lage 2
    Private Sub build_sk_matrizen2()
        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten2.Count

        ReDim Me._array_Theodlit_Hz_Zd_CI_LI2(n, 4)
        ReDim Me._array_Kamera_XC_YC2(n, 2)
        ReDim Me._array_Gewicht2(n, 1)

        For i = 1 To n Step 1
            Me._array_Theodlit_Hz_Zd_CI_LI2(i, 1) = Me._sk_daten2.Item(i - 1)(1)
            Me._array_Theodlit_Hz_Zd_CI_LI2(i, 2) = Me._sk_daten2.Item(i - 1)(2)
            Me._array_Theodlit_Hz_Zd_CI_LI2(i, 3) = Me._sk_daten2.Item(i - 1)(3)
            Me._array_Theodlit_Hz_Zd_CI_LI2(i, 4) = Me._sk_daten2.Item(i - 1)(4)

            Me._array_Kamera_XC_YC2(i, 1) = Me._sk_daten2.Item(i - 1)(5)
            Me._array_Kamera_XC_YC2(i, 2) = Me._sk_daten2.Item(i - 1)(6)

            Me._array_Gewicht2(i, 1) = Me._sk_daten2.Item(i - 1)(7)
        Next
    End Sub

    ''' <summary>
    ''' Fügt eine Messung in das Array für SK ein. Ziel 2 Lage 1
    ''' </summary>
    ''' <param name="hz"></param>
    ''' <param name="zd"></param>
    ''' <param name="crossInc"></param>
    ''' <param name="lengthInc"></param>
    ''' <param name="xc"></param>
    ''' <param name="yc"></param>
    ''' <param name="gewicht"></param>
    ''' <remarks></remarks>
    Public Sub add_sk_daten3(ByVal hz As Double, ByVal zd As Double, ByVal crossInc As Double, ByVal lengthInc As Double, ByVal xc As Double, ByVal yc As Double, ByVal gewicht As Double)
        Dim row(7) As Double
        row(1) = hz
        row(2) = zd
        row(3) = crossInc
        row(4) = lengthInc
        row(5) = xc
        row(6) = yc
        row(7) = gewicht

        Me._sk_daten3.Add(row)
    End Sub

    ' Macht aus der einen Arraylist, drei Arrays für die Berechnug der Selbstkalibrierung, Ziel 2 Lage 1
    Private Sub build_sk_matrizen3()
        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten3.Count

        ReDim Me._array_Theodlit_Hz_Zd_CI_LI3(n, 4)
        ReDim Me._array_Kamera_XC_YC3(n, 2)
        ReDim Me._array_Gewicht3(n, 1)

        For i = 1 To n Step 1
            Me._array_Theodlit_Hz_Zd_CI_LI3(i, 1) = Me._sk_daten3.Item(i - 1)(1)
            Me._array_Theodlit_Hz_Zd_CI_LI3(i, 2) = Me._sk_daten3.Item(i - 1)(2)
            Me._array_Theodlit_Hz_Zd_CI_LI3(i, 3) = Me._sk_daten3.Item(i - 1)(3)
            Me._array_Theodlit_Hz_Zd_CI_LI3(i, 4) = Me._sk_daten3.Item(i - 1)(4)

            Me._array_Kamera_XC_YC3(i, 1) = Me._sk_daten3.Item(i - 1)(5)
            Me._array_Kamera_XC_YC3(i, 2) = Me._sk_daten3.Item(i - 1)(6)

            Me._array_Gewicht3(i, 1) = Me._sk_daten3.Item(i - 1)(7)
        Next
    End Sub

    ''' <summary>
    ''' Fügt eine Messung in das Array für SK ein. Ziel 2 Lage 2
    ''' </summary>
    ''' <param name="hz"></param>
    ''' <param name="zd"></param>
    ''' <param name="crossInc"></param>
    ''' <param name="lengthInc"></param>
    ''' <param name="xc"></param>
    ''' <param name="yc"></param>
    ''' <param name="gewicht"></param>
    ''' <remarks></remarks>
    Public Sub add_sk_daten4(ByVal hz As Double, ByVal zd As Double, ByVal crossInc As Double, ByVal lengthInc As Double, ByVal xc As Double, ByVal yc As Double, ByVal gewicht As Double)
        Dim row(7) As Double
        row(1) = hz
        row(2) = zd
        row(3) = crossInc
        row(4) = lengthInc
        row(5) = xc
        row(6) = yc
        row(7) = gewicht

        Me._sk_daten4.Add(row)
    End Sub

    ' Macht aus der einen Arraylist, drei Arrays für die Berechnug der Selbstkalibrierung, Ziel 2 Lage 2
    Private Sub build_sk_matrizen4()
        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten4.Count

        ReDim Me._array_Theodlit_Hz_Zd_CI_LI4(n, 4)
        ReDim Me._array_Kamera_XC_YC4(n, 2)
        ReDim Me._array_Gewicht4(n, 1)

        For i = 1 To n Step 1
            Me._array_Theodlit_Hz_Zd_CI_LI4(i, 1) = Me._sk_daten4.Item(i - 1)(1)
            Me._array_Theodlit_Hz_Zd_CI_LI4(i, 2) = Me._sk_daten4.Item(i - 1)(2)
            Me._array_Theodlit_Hz_Zd_CI_LI4(i, 3) = Me._sk_daten4.Item(i - 1)(3)
            Me._array_Theodlit_Hz_Zd_CI_LI4(i, 4) = Me._sk_daten4.Item(i - 1)(4)

            Me._array_Kamera_XC_YC4(i, 1) = Me._sk_daten4.Item(i - 1)(5)
            Me._array_Kamera_XC_YC4(i, 2) = Me._sk_daten4.Item(i - 1)(6)

            Me._array_Gewicht4(i, 1) = Me._sk_daten4.Item(i - 1)(7)
        Next
    End Sub

    Public Sub do_sk4P()
        Me.build_sk_matrizen1()
        Me.build_sk_matrizen2()
        Me.build_sk_matrizen3()
        Me.build_sk_matrizen4()
        Me._transformation = SK4P(Me._zentrum_X, Me._zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._array_Gewicht1, _
                                                                Me._array_Theodlit_Hz_Zd_CI_LI2, Me._array_Kamera_XC_YC2, Me._array_Gewicht2, _
                                                                Me._array_Theodlit_Hz_Zd_CI_LI3, Me._array_Kamera_XC_YC3, Me._array_Gewicht3, _
                                                                Me._array_Theodlit_Hz_Zd_CI_LI4, Me._array_Kamera_XC_YC4, Me._array_Gewicht4)

        Dim count_insgesamt As Integer
        Dim offset As Integer
        count_insgesamt = Me._sk_daten1.Count + Me._sk_daten2.Count + Me._sk_daten3.Count + Me._sk_daten4.Count
        Dim vHz(count_insgesamt) As Double, vZd(count_insgesamt) As Double

        ' Für die Statistik:
        Dim skblick1(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick1 = SKBlickP(Me.zentrum_X, Me.zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._array_Gewicht1, Me._transformation)

        Dim n As Integer
        Dim i As Integer
        n = Me._sk_daten1.Count

        Dim mess(1, 2) As Double

        For i = 1 To n Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._sk_daten1.Item(i - 1)(7) = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._array_Theodlit_Hz_Zd_CI_LI1, Me._array_Kamera_XC_YC1, Me._transformation)
                vHz(i) = skblick1(1, 1) - mess(1, 1)
                vZd(i) = skblick1(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick2(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick2 = SKBlickP(Me.zentrum_X, Me.zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI2, Me._array_Kamera_XC_YC2, Me._array_Gewicht2, Me._transformation)

        n = Me._sk_daten2.Count

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._sk_daten2.Item(i - 1)(7) = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._array_Theodlit_Hz_Zd_CI_LI2, Me._array_Kamera_XC_YC2, Me._transformation)
                vHz(i) = skblick2(1, 1) - mess(1, 1)
                vZd(i) = skblick2(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick3(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick3 = SKBlickP(Me.zentrum_X, Me.zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI3, Me._array_Kamera_XC_YC3, Me._array_Gewicht3, Me._transformation)

        n = Me._sk_daten3.Count

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._sk_daten3.Item(i - 1)(7) = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._array_Theodlit_Hz_Zd_CI_LI3, Me._array_Kamera_XC_YC3, Me._transformation)
                vHz(i) = skblick3(1, 1) - mess(1, 1)
                vZd(i) = skblick3(1, 2) - mess(1, 2)
            End If
        Next

        offset = i

        ' Für die Statistik:
        Dim skblick4(1, 2) As Double

        ' SkBlick, Richtung des Ziels
        skblick4 = SKBlickP(Me.zentrum_X, Me.zentrum_Y, Me._array_Theodlit_Hz_Zd_CI_LI4, Me._array_Kamera_XC_YC4, Me._array_Gewicht4, Me._transformation)

        n = Me._sk_daten4.Count

        For i = offset To n + offset Step 1
            ' Ist das Gewicht = 1, dann Abweichung berechnen
            If (Me._sk_daten4.Item(i - 1)(7) = 1) Then
                ' Messblick, Richtung des Ziels, für die jeweilige Beobachtung
                mess = MessBlickP(Me._array_Theodlit_Hz_Zd_CI_LI4, Me._array_Kamera_XC_YC4, Me._transformation)
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
#End Region


    Public Sub export_sk_daten_to_csv(ByVal name As String)
        Dim data As New DataSet("csv")
        data.Tables.Add("CSVData")

        data.Tables("CSVData").Columns.Add("Hz")
        data.Tables("CSVData").Columns.Add("Zd")
        data.Tables("CSVData").Columns.Add("CI")
        data.Tables("CSVData").Columns.Add("LI")
        data.Tables("CSVData").Columns.Add("XC")
        data.Tables("CSVData").Columns.Add("YC")
        data.Tables("CSVData").Columns.Add("Gewicht")

        Dim hz As String, zd As String, CI As String, LI As String, XC As String, YC As String, gewicht As String

        ' Erste Zeile: Koordinate des Strichkreuzes
        hz = ""
        zd = ""
        CI = ""
        LI = ""
        XC = Me.zentrum_X
        YC = Me.zentrum_Y
        gewicht = ""

        data.Tables("CSVData").Rows.Add(hz, zd, CI, LI, XC, YC, gewicht)

        Dim n As Integer
        Dim i As Integer

        n = Me._sk_daten1.Count
        For i = 1 To n Step 1
            hz = Me._sk_daten1.Item(i - 1)(1).ToString
            zd = Me._sk_daten1.Item(i - 1)(2).ToString
            CI = Me._sk_daten1.Item(i - 1)(3).ToString
            LI = Me._sk_daten1.Item(i - 1)(4).ToString

            XC = Me._sk_daten1.Item(i - 1)(5).ToString
            YC = Me._sk_daten1.Item(i - 1)(6).ToString

            gewicht = Me._sk_daten1.Item(i - 1)(7).ToString

            data.Tables("CSVData").Rows.Add(hz, zd, CI, LI, XC, YC, gewicht)
        Next

        n = Me._sk_daten2.Count
        For i = 1 To n Step 1
            hz = Me._sk_daten2.Item(i - 1)(1).ToString
            zd = Me._sk_daten2.Item(i - 1)(2).ToString
            CI = Me._sk_daten2.Item(i - 1)(3).ToString
            LI = Me._sk_daten2.Item(i - 1)(4).ToString

            XC = Me._sk_daten2.Item(i - 1)(5).ToString
            YC = Me._sk_daten2.Item(i - 1)(6).ToString

            gewicht = Me._sk_daten2.Item(i - 1)(7).ToString

            data.Tables("CSVData").Rows.Add(hz, zd, CI, LI, XC, YC, gewicht)
        Next

        n = Me._sk_daten3.Count
        For i = 1 To n Step 1
            hz = Me._sk_daten3.Item(i - 1)(1).ToString
            zd = Me._sk_daten3.Item(i - 1)(2).ToString
            CI = Me._sk_daten3.Item(i - 1)(3).ToString
            LI = Me._sk_daten3.Item(i - 1)(4).ToString

            XC = Me._sk_daten3.Item(i - 1)(5).ToString
            YC = Me._sk_daten3.Item(i - 1)(6).ToString

            gewicht = Me._sk_daten3.Item(i - 1)(7).ToString

            data.Tables("CSVData").Rows.Add(hz, zd, CI, LI, XC, YC, gewicht)
        Next

        n = Me._sk_daten4.Count
        For i = 1 To n Step 1
            hz = Me._sk_daten4.Item(i - 1)(1).ToString
            zd = Me._sk_daten4.Item(i - 1)(2).ToString
            CI = Me._sk_daten4.Item(i - 1)(3).ToString
            LI = Me._sk_daten4.Item(i - 1)(4).ToString

            XC = Me._sk_daten4.Item(i - 1)(5).ToString
            YC = Me._sk_daten4.Item(i - 1)(6).ToString

            gewicht = Me._sk_daten4.Item(i - 1)(7).ToString

            data.Tables("CSVData").Rows.Add(hz, zd, CI, LI, XC, YC, gewicht)
        Next

        Dim csv As New CSVData
        csv.Separator = ";"
        csv.TextQualifier = """"

        csv.CSVDataTable = data.Tables("CSVData")
        csv.SaveAsCSV(name, True)
    End Sub

    Public Sub export_transformation_to_csv(ByVal name As String)
        Dim data As New DataSet("csv")
        data.Tables.Add("trans")

        data.Tables("trans").Columns.Add("omega")
        data.Tables("trans").Columns.Add("phi")
        data.Tables("trans").Columns.Add("kappa")
        data.Tables("trans").Columns.Add("ck")
        data.Tables("trans").Columns.Add("x0")
        data.Tables("trans").Columns.Add("y0")

        Dim omega As String, phi As String, kappa As String, ck As String, x0 As String, y0 As String

        omega = transformation(1, 1).ToString
        phi = transformation(2, 1).ToString
        kappa = transformation(3, 1).ToString
        ck = transformation(4, 1).ToString
        x0 = transformation(5, 1).ToString
        y0 = transformation(6, 1).ToString

        data.Tables("trans").Rows.Add(omega, phi, kappa, ck, x0, y0)


        Dim csv As New CSVData
        csv.Separator = ";"
        csv.TextQualifier = """"

        csv.CSVDataTable = data.Tables("trans")
        csv.SaveAsCSV(name, True)
    End Sub

    Public Sub import_transformation_from_csv(ByVal name As String)
        Dim csv As New CSVData
        csv.TextQualifier = """"

        csv.LoadCSV(name, False, ";")
        Dim dt As New DataTable
        dt = csv.CSVDataTable

        transformation(1, 1) = CDbl(dt.Rows(0).Item(0))
        transformation(2, 1) = CDbl(dt.Rows(0).Item(1))
        transformation(3, 1) = CDbl(dt.Rows(0).Item(2))
        transformation(4, 1) = CDbl(dt.Rows(0).Item(3))
        transformation(5, 1) = CDbl(dt.Rows(0).Item(4))
        transformation(6, 1) = CDbl(dt.Rows(0).Item(5))
    End Sub

    Public Sub export_strichkreuz_to_csv(ByVal name As String)
        Dim dt As New DataTable
        dt.Columns.Add("X")
        dt.Columns.Add("Y")

        Dim x As String, y As String
        Dim length As Integer

        x = Me._zentrum_X.ToString
        y = Me._zentrum_Y.ToString
        dt.Rows.Add(x, y)

        length = Me._strichkreuz_punkte_X.Length

        For i = 0 To length - 1
            x = Me._strichkreuz_punkte_X(i).ToString
            y = Me._strichkreuz_punkte_Y(i).ToString
            dt.Rows.Add(x, y)
        Next

        Dim csv As New CSVData
        csv.Separator = ";"
        csv.TextQualifier = """"
        csv.CSVDataTable = dt
        csv.SaveAsCSV(name, True)
    End Sub


    Public Sub import_strichkreuz_from_csv(ByVal name As String)
        Dim csv As New CSVData
        csv.TextQualifier = """"

        csv.LoadCSV(name, True, ";")
        Dim dt As New DataTable
        dt = csv.CSVDataTable

        'Dim row As DataRow
        Dim x As Double, y As Double
        Me._zentrum_X = CDbl(dt.Rows(0).Item(0))
        Me._zentrum_Y = CDbl(dt.Rows(0).Item(1))

        Dim length As Integer = dt.Rows.Count()
        For i = 1 To length - 1 Step 1
            x = CDbl(dt.Rows(i).Item(0))
            y = CDbl(dt.Rows(i).Item(1))
            ReDim Preserve Me._strichkreuz_punkte_X(i - 1)
            ReDim Preserve Me._strichkreuz_punkte_Y(i - 1)
            Me._strichkreuz_punkte_X(i - 1) = x
            Me._strichkreuz_punkte_Y(i - 1) = y
        Next
    End Sub

    Public Sub import_latest_strichkreuz_from_csv(ByVal folder As String)
        Dim filename As String
        Dim filelist() As String

        filelist = System.IO.Directory.GetFiles(folder)
        filename = latest_file(filelist, "crosshair")

        Me.import_strichkreuz_from_csv(filename)

    End Sub

    Private Function latest_file(ByVal liste() As String, ByVal prefix As String, Optional ByVal extension As String = ".csv") As String
        Dim maxindex As Integer = 0
        Dim maxdate As Date = Date.ParseExact("19000101-01000000", "yyyyMMdd-HHmmssff", Nothing)

        For i = 0 To liste.Length - 1 Step 1
            If (String.Compare(New IO.FileInfo(liste(i)).Extension, extension) = 0) Then
                Dim temp1 As String = New IO.FileInfo(liste(i)).Name
                Dim temp2() As String = Split(temp1, ".")
                If (temp2.Length = 2) Then
                    Dim temp3() As String = Split(temp2(0), "_")
                    If (temp3.Length = 2) Then
                        If (InStr(temp3(0), prefix) <> 0) Then
                            Dim tempdate As DateTime
                            tempdate = Date.ParseExact(temp3(1), "yyyyMMdd-HHmmssff", Nothing)
                            If (DateTime.Compare(maxdate, tempdate) = -1) Then
                                maxindex = i
                                maxdate = tempdate
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Return liste(maxindex)
    End Function

    Public Sub save_Maske(ByVal file As String)
        Me._maske_strichkreuz.WriteRegion(file)
    End Sub

    Public Sub load_Maske(ByVal file As String)
        Me._maske_strichkreuz.ReadRegion(file)
    End Sub

    Public Sub import_sk_daten_from_csv(ByVal filename As String)

    End Sub

    Public ReadOnly Property transformation() As Double(,)
        Get
            Return Me._transformation
        End Get
    End Property
    '## temp Public ReadOnly Property zentrum_Y() As Double
    Public Property zentrum_Y() As Double
        Get
            Return Me._zentrum_Y
        End Get
        Set(ByVal value As Double)
            Me._zentrum_Y = value
        End Set
    End Property

    '## temp Public ReadOnly Property zentrum_X() As Double
    Public Property zentrum_X() As Double
        Get
            Return Me._zentrum_X
        End Get
        Set(ByVal value As Double)
            Me._zentrum_X = value
        End Set
    End Property

    Public ReadOnly Property strichkreuz_punkte_Y() As Double()
        Get
            Return Me._strichkreuz_punkte_Y
        End Get
    End Property


    Public ReadOnly Property strichkreuz_punkte_X() As Double()
        Get
            Return Me._strichkreuz_punkte_X
        End Get
    End Property

    Public ReadOnly Property maske() As HRegion
        Get
            Return Me._maske_strichkreuz
        End Get
    End Property

    Public Function def_strichkreuz_tm5100(ByVal image As HImage, ByVal blickfeld_groesse As Integer, Optional ByVal maskenfaktor As Long = 2) As Boolean
        Dim bv As New bildverarbeitung
        Dim punkte_x(1), punkte_y(1) As Double
        Dim flag As Boolean

        flag = bv.strichkreuz_tm5100(image, blickfeld_groesse, maskenfaktor, Me._maske_strichkreuz, punkte_x, punkte_y)
        If (flag = True) Then
            If (Me._is_strichkreuz_definiert = False) Then
                Me._zentrum_X = punkte_x(0)
                Me._zentrum_Y = punkte_y(0)

                Dim len As Integer
                len = punkte_x.Length
                ReDim Me._strichkreuz_punkte_Y(len - 2)
                ReDim Me._strichkreuz_punkte_X(len - 2)
                For i = 0 To len - 2 Step 1
                    Me._strichkreuz_punkte_Y(i) = punkte_y(i + 1)
                    Me._strichkreuz_punkte_X(i) = punkte_x(i + 1)
                Next
                Me._is_strichkreuz_definiert = True
            Else
                Dim dy, dx As Double
                dy = Me._zentrum_Y - punkte_y(0)
                dx = Me._zentrum_X - punkte_x(0)
                MessageBox.Show("Verschiebung des Strichkreuzes" + _
                                vbCr + "in x = " + dx.ToString("F2") + _
                                vbCr + "in y = " + dy.ToString("F2"))

                Me._zentrum_X = punkte_x(0)
                Me._zentrum_Y = punkte_y(0)

                Dim len As Integer
                len = punkte_x.Length
                ReDim Me._strichkreuz_punkte_Y(len - 2)
                ReDim Me._strichkreuz_punkte_X(len - 2)
                For i = 0 To len - 2 Step 1
                    Me._strichkreuz_punkte_Y(i) = punkte_y(i + 1)
                    Me._strichkreuz_punkte_X(i) = punkte_x(i + 1)
                Next
                Me._is_strichkreuz_definiert = True
            End If
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="image"></param>
    ''' <param name="blickfeld_groesse">
    ''' 0 - Blickfeld klein
    ''' 1 - Blickfeld groß
    ''' </param>
    ''' <param name="maskenfaktor"></param>
    ''' <remarks></remarks>
    Public Function def_strichkreuz_ts30(ByVal image As HImage, ByVal blickfeld_groesse As Integer, Optional ByVal maskenfaktor As Long = 2) As Boolean
        Dim bv As New bildverarbeitung
        Dim punkte_x(1), punkte_y(1) As Double
        Dim flag As Boolean

        flag = bv.strichkreuz_ts30(image, blickfeld_groesse, maskenfaktor, Me._maske_strichkreuz, punkte_x, punkte_y)
        If (flag = True) Then
            If (Me._is_strichkreuz_definiert = False) Then
                Me._zentrum_X = punkte_x(0)
                Me._zentrum_Y = punkte_y(0)

                Dim len As Integer
                len = punkte_x.Length
                ReDim Me._strichkreuz_punkte_Y(len - 2)
                ReDim Me._strichkreuz_punkte_X(len - 2)
                For i = 0 To len - 2 Step 1
                    Me._strichkreuz_punkte_Y(i) = punkte_y(i + 1)
                    Me._strichkreuz_punkte_X(i) = punkte_x(i + 1)
                Next
                Me._is_strichkreuz_definiert = True
            Else
                Dim dy, dx As Double
                dy = Me._zentrum_Y - punkte_y(0)
                dx = Me._zentrum_X - punkte_x(0)
                MessageBox.Show("Verschiebung des Strichkreuzes" + _
                                vbCr + "in x = " + dx.ToString("F2") + _
                                vbCr + "in y = " + dy.ToString("F2"))

                Me._zentrum_X = punkte_x(0)
                Me._zentrum_Y = punkte_y(0)

                Dim len As Integer
                len = punkte_x.Length
                ReDim Me._strichkreuz_punkte_Y(len - 2)
                ReDim Me._strichkreuz_punkte_X(len - 2)
                For i = 0 To len - 2 Step 1
                    Me._strichkreuz_punkte_Y(i) = punkte_y(i + 1)
                    Me._strichkreuz_punkte_X(i) = punkte_x(i + 1)
                Next
                Me._is_strichkreuz_definiert = True
            End If
            Return True
        Else
            Return False
        End If
    End Function

End Class
