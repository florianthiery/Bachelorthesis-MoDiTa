Public Class bildverarbeitung
    Dim MyEngine As New HDevEngine()

    ''' <summary>
    ''' Erkennt die Position des Strichkreuzes eines TS30, 1103 ... "geodätsiche Tachymeter"
    ''' </summary>
    ''' <param name="image">
    ''' Bild mit Strichkreuz
    ''' </param>
    ''' <param name="blickfeld_groesse">
    ''' 0 - Blickfeld klein
    ''' 1 - Blickfeld groß
    ''' </param>
    ''' <param name="maskenfaktor">
    ''' Bestimmtt die Breite der Maske
    ''' Größe der Maske = Linienbreite des Strichkreuzes * makenfaktor
    ''' </param>
    ''' <param name="maske">
    ''' Die Maske als HRegion (Übergabe: ByRef)
    ''' </param>
    ''' <param name="points_X">
    ''' X-Bildposition (Spalten), Erste Koordiante = Zentrum, restliche Koordinaten = virtuelle Linienpunkte (Übergabe: ByRef)
    ''' </param>
    ''' <param name="points_Y">
    ''' Y-Bildposition (Zeilen), Erste Koordiante = Zentrum, restliche Koordinaten = virtuelle Linienpunkte (Übergabe: ByRef)
    ''' </param>
    ''' <remarks></remarks>
    Public Function strichkreuz_ts30(ByVal image As HImage, ByVal blickfeld_groesse As Integer, ByVal maskenfaktor As Long, _
                                    ByRef maske As HRegion, ByRef points_X() As Double, ByRef points_Y() As Double) As Boolean
        Dim MyEngine As New HDevEngine()
        Dim ProcPathString As String
        ' Legt das Verzeichnis fest, wo die Prozeduren liegen
        ProcPathString = Environment.GetEnvironmentVariable("HALCONROOT") & "\procedures"
        MyEngine.SetProcedurePath(ProcPathString)

        ' Prozedur:
        Dim hproc As New HDevProcedure("strichkreuz_ts30")

        Dim hproc_call As HDevProcedureCall
        hproc_call = New HDevProcedureCall(hproc)

        ' Parameter der Prozedur
        hproc_call.SetInputIconicParamObject("Image", image)
        hproc_call.SetInputCtrlParamTuple("maskenfaktor", maskenfaktor)
        hproc_call.SetInputCtrlParamTuple("blickfeld_gross", blickfeld_groesse)

        Try
            ' Prozedur wird ausgeführt
            hproc_call.Execute()

            'Dim geraden_punkte_begin_y, geraden_punkte_begin_x, geraden_punkte_end_y, geraden_punkte_end_x As HTuple
            Dim punkte_y_tuple, punkte_x_tuple As HTuple
            Dim laenge As Integer

            ' Rückgabewerte von der HALCON- Prozedur:
            maske = hproc_call.GetOutputIconicParamRegion("Maske")
            punkte_y_tuple = hproc_call.GetOutputCtrlParamTuple("points_out_y")
            punkte_x_tuple = hproc_call.GetOutputCtrlParamTuple("points_out_x")

            laenge = punkte_x_tuple.Length
            ReDim points_X(laenge - 1)
            ReDim points_Y(laenge - 1)

            For i = 0 To laenge - 1 Step 1
                points_Y(i) = punkte_y_tuple(i).D
                points_X(i) = punkte_x_tuple(i).D
            Next
            Return True
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Erkennt die Position des Strichkreuzes eines TM5100, TDA5005 (Industriesysteme)
    ''' </summary>
    ''' <param name="image">
    ''' Bild mit Strichkreuz
    ''' </param>
    ''' <param name="blickfeld_groesse">
    ''' 0 - Blickfeld klein
    ''' 1 - Blickfeld groß
    ''' </param>
    ''' <param name="maskenfaktor">
    ''' Bestimmtt die Breite der Maske
    ''' Größe der Maske = Linienbreite des Strichkreuzes * makenfaktor
    ''' </param>
    ''' <param name="maske">
    ''' Die Maske als HRegion (Übergabe: ByRef)
    ''' </param>
    ''' <param name="points_X">
    ''' X-Bildposition (Spalten), Erste Koordiante = Zentrum, restliche Koordinaten = virtuelle Linienpunkte (Übergabe: ByRef)
    ''' </param>
    ''' <param name="points_Y">
    ''' Y-Bildposition (Zeilen), Erste Koordiante = Zentrum, restliche Koordinaten = virtuelle Linienpunkte (Übergabe: ByRef)
    ''' </param>
    ''' <remarks></remarks>
    Public Function strichkreuz_tm5100(ByVal image As HImage, ByVal blickfeld_groesse As Integer, ByVal maskenfaktor As Long, _
                                    ByRef maske As HRegion, ByRef points_X() As Double, ByRef points_Y() As Double) As Boolean
        Dim MyEngine As New HDevEngine()
        Dim ProcPathString As String
        ' Legt das Verzeichnis fest, wo die Prozeduren liegen
        ProcPathString = Environment.GetEnvironmentVariable("HALCONROOT") & "\procedures"
        MyEngine.SetProcedurePath(ProcPathString)

        ' Prozedur:
        Dim hproc As New HDevProcedure("strichkreuz_tm5100")

        Dim hproc_call As HDevProcedureCall
        hproc_call = New HDevProcedureCall(hproc)

        ' Parameter der Prozedur
        hproc_call.SetInputIconicParamObject("Image", image)
        hproc_call.SetInputCtrlParamTuple("maskenfaktor", maskenfaktor)

        Try
            ' Prozedur wird ausgeführt
            hproc_call.Execute()

            Dim punkte_y_tuple, punkte_x_tuple As HTuple
            Dim laenge As Integer

            ' Rückgabewerte:
            maske = hproc_call.GetOutputIconicParamRegion("Maske")
            punkte_y_tuple = hproc_call.GetOutputCtrlParamTuple("fein_schnittpunkte_Y")
            punkte_x_tuple = hproc_call.GetOutputCtrlParamTuple("fein_schnittpunkte_X")

            laenge = punkte_x_tuple.Length

            ReDim points_X(laenge - 1)
            ReDim points_Y(laenge - 1)

            For i = 0 To laenge - 1 Step 1
                points_Y(i) = punkte_y_tuple(i).D
                points_X(i) = punkte_x_tuple(i).D
            Next
            Return True
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function
    ''' <summary>
    ''' Erkennt die Position des Kollimatorfadenkreuz
    ''' </summary>
    ''' <param name="image">
    ''' Bild mit Kollimatorfadenkreuz. Strichkreuz vom Tachymeter muss "entfernt" sein.
    ''' </param>
    ''' <param name="point_y">
    ''' Y-Koordinate (Zeile)
    ''' </param>
    ''' <param name="point_x">
    ''' X-Koordinate (Spalte)
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function strichkreuz_kollimator01(ByVal image As HImage, ByRef point_y As Double, ByRef point_x As Double) As Boolean
        Dim MyEngine As New HDevEngine()
        Dim ProcPathString As String
        ' Legt das Verzeichnis fest, wo die Prozeduren liegen
        ProcPathString = Environment.GetEnvironmentVariable("HALCONROOT") & "\procedures"
        MyEngine.SetProcedurePath(ProcPathString)

        ' Prozedur:
        Dim hproc As New HDevProcedure("strichkreuz_kollimator")

        Dim hproc_call As HDevProcedureCall
        hproc_call = New HDevProcedureCall(hproc)

        ' Parameter der Prozedur
        hproc_call.SetInputIconicParamObject("Image", image)
        Try
            ' Prozedur wird ausgeführt
            hproc_call.Execute()
            Dim punkte_y, punkte_x As HTuple
            ' Rückgabewerte:
            punkte_y = hproc_call.GetOutputCtrlParamTuple("Kolli_schnittpunktY_fein")
            punkte_x = hproc_call.GetOutputCtrlParamTuple("Kolli_schnittpunktX_fein")

            If (punkte_x.Length = 1) Then
                point_y = punkte_y.D
                point_x = punkte_x.D
                Return True
            Else
                point_y = -1
                point_x = -1
                Return False
            End If
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            point_y = -1
            point_x = -1
            Return False
        End Try

    End Function

  

    'Public Sub fadenkreuz(ByVal image As HImage, ByRef window As HWindow, ByRef schnittpunkte_Y() As Double, ByRef schnittpunkte_X() As Double)
    '    'Public Sub fadenkreuz()
    '    Dim ProcPathString As String

    '    ProcPathString = Environment.GetEnvironmentVariable("HALCONROOT") & "\procedures"
    '    MyEngine.SetProcedurePath(ProcPathString)

    '    Dim fadenkreuz_call As HDevProcedureCall
    '    Dim fadenkreuz As New HDevProcedure("strichkreuz_tm5100")
    '    fadenkreuz_call = New HDevProcedureCall(fadenkreuz)

    '    fadenkreuz_call.SetInputIconicParamObject("Image", image)
    '    fadenkreuz_call.SetInputCtrlParamTuple("maskenfaktor", 2)

    '    Try
    '        fadenkreuz_call.Execute()
    '        Dim region As HRegion
    '        Dim punkte_y, punkte_x As HTuple
    '        region = fadenkreuz_call.GetOutputIconicParamRegion("Maske")
    '        punkte_y = fadenkreuz_call.GetOutputCtrlParamTuple("fein_schnittpunkte_Y")
    '        punkte_x = fadenkreuz_call.GetOutputCtrlParamTuple("fein_schnittpunkte_X")

    '        Dim length As Integer
    '        length = punkte_x.Length

    '        ReDim schnittpunkte_Y(length - 1)
    '        ReDim schnittpunkte_X(length - 1)
    '        For i = 0 To length - 1 Step 1
    '            schnittpunkte_Y(i) = punkte_y(i).D
    '            schnittpunkte_X(i) = punkte_x(i).D
    '        Next

    '        window.DispCross(punkte_y, punkte_x, 10.0, 0.0)

    '        MessageBox.Show(length.ToString)
    '    Catch Ex As HDevEngineException
    '        MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub

End Class
