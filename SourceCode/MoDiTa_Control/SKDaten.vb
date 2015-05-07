Public Class SKDaten

    Private _skID As Integer
    Private _free_punktID As Integer
    Private _punkte() As Messpunkt

    Private _csv_file As String
    Private _save_to_csv As Boolean = False

    Private _Matrix_Theo(,) As Double
    Private _Matrix_Kamera(,) As Double
    Private _Matrix_Gewicht(,) As Double


#Region "Properties"
    Public ReadOnly Property anzahl_messpunkte() As Integer
        Get
            Return Me._free_punktID
        End Get
    End Property
    ''' <summary>
    ''' Gibt nächste ID zurück
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property skID() As Integer
        Get
            Return Me._skID
        End Get
        Set(ByVal value As Integer)
            Me._skID = value
        End Set
    End Property
    ''' <summary>
    ''' Verzeichnis der CSV-Datei
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property csv_dir() As String
        Get
            Return Me._csv_file
        End Get
        Set(ByVal value As String)
            Me._csv_file = value + "skdaten_" + Me._skID.ToString("D4") + ".csv"
            Me._save_to_csv = True
        End Set
    End Property
    ''' <summary>
    ''' Matrix "Theo" Hz_Zd_CI_LI
    ''' </summary>
    ''' <value></value>
    ''' <returns>Double (1-n,1-4) </returns>
    ''' <remarks></remarks>
    Public ReadOnly Property matrix_Theo() As Double(,)
        Get
            Return Me._Matrix_Theo
        End Get
    End Property
    ''' <summary>
    ''' Matrix "Kamera" XC_YC
    ''' </summary>
    ''' <value></value>
    ''' <returns>Double (1-n,1-2)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property matrix_Kamera() As Double(,)
        Get
            Return Me._Matrix_Kamera
        End Get
    End Property
    ''' <summary>
    ''' Matrix "Gewicht"
    ''' </summary>
    ''' <value></value>
    ''' <returns>Double (1-n,1)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property matrix_Gewicht() As Double(,)
        Get
            Return Me._Matrix_Gewicht
        End Get
    End Property
#End Region

    Private Sub build_Matrix_Theo()
        Dim n As Integer
        Dim i As Integer
        n = Me._punkte.Length
        ReDim Me._Matrix_Theo(n, 4)

        For i = 1 To n Step 1
            Me._Matrix_Theo(i, 1) = Me._punkte(i - 1).horizontalrichtung
            Me._Matrix_Theo(i, 2) = Me._punkte(i - 1).zenitwinkel
            Me._Matrix_Theo(i, 3) = Me._punkte(i - 1).crossInclination
            Me._Matrix_Theo(i, 4) = Me._punkte(i - 1).lengthInclination
        Next
    End Sub

    Private Sub build_Matrix_Kamera()
        Dim n As Integer
        Dim i As Integer
        n = Me._punkte.Length
        ReDim Me._Matrix_Kamera(n, 2)

        For i = 1 To n Step 1
            Me._Matrix_Kamera(i, 1) = Me._punkte(i - 1).bildkoordinate_X
            Me._Matrix_Kamera(i, 2) = Me._punkte(i - 1).bildkoordinate_Y
        Next
    End Sub

    Private Sub build_Matrix_Gewicht()
        Dim n As Integer
        Dim i As Integer
        n = Me._punkte.Length
        ReDim Me._Matrix_Gewicht(n, 1)

        For i = 1 To n Step 1
            Me._Matrix_Gewicht(i, 1) = Me._punkte(i - 1).gewicht_Selbstkalibrierung
        Next
    End Sub


    ''' <summary>
    ''' Gibt den Messpunkt mit der ID zurück
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>Messpunkt</returns>
    ''' <remarks></remarks>
    Public Function get_punkt(ByVal id As Integer) As Messpunkt
        If (id < _free_punktID) Then
            Return Me._punkte(id)
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' Fügt ein Messpunkt zur Liste hinzu.
    ''' </summary>
    ''' <param name="punkt"></param>
    ''' <remarks></remarks>
    Public Sub add_punkt(ByVal punkt As Messpunkt)
        ' Arraygröße anpassen
        ReDim Preserve Me._punkte(Me._free_punktID)
        ' Punkt hinzufügen
        Me._punkte(Me._free_punktID) = punkt

        Me.build_Matrix_Theo()
        Me.build_Matrix_Kamera()
        Me.build_Matrix_Gewicht()

        ' CSV Updaten
        If (Me._save_to_csv = True) Then
            Dim line As String = ""

            Dim sw As IO.StreamWriter


            If (IO.File.Exists(Me._csv_file) = True) Then ' Gibt es Datei schon, dann einfach anhängen.
                ' Streamwriter mit Append = true, d.h. Ziele wird an der Datei angehängt
                sw = New IO.StreamWriter(Me._csv_file, True)

                line = Me._punkte(Me._free_punktID).csv_line
                sw.WriteLine(line)
                sw.Flush()
                sw.Close()
                sw = Nothing
            Else ' Wenn Datei noch nicht existiert, dann erst Header in die erste Zeile. Datei wird mit dem Befehl Writeline auch
                ' erzeugt.
                ' Header
                sw = New IO.StreamWriter(Me._csv_file)
                Dim header As String = ""
                header = Me._punkte(Me._free_punktID).csv_header
                sw.WriteLine(header)

                ' Punkt anhängen
                line = Me._punkte(Me._free_punktID).csv_line
                sw.WriteLine(line)
                sw.Flush()
                sw.Close()
                sw = Nothing
            End If
        End If

        _free_punktID += 1
    End Sub
    ''' <summary>
    ''' Erzeugt aus einem Datensatz eine csv-Datei
    ''' </summary>
    ''' <param name="csv_file"></param>
    ''' <remarks></remarks>
    Public Sub punkte_To_csv(ByVal csv_file As String)
        Dim sw As IO.StreamWriter
        sw = New IO.StreamWriter(csv_file)

        Dim header As String = ""
        header = Me._punkte(Me._free_punktID).csv_header
        sw.WriteLine()

        Dim line As String = ""

        For i = 0 To Me._punkte.Length - 1 Step 1
            line = Me._punkte(i).csv_line
            sw.WriteLine(line)
        Next
        sw.Flush()
        sw.Close()
        sw = Nothing
    End Sub
    ''' <summary>
    ''' Erzeugt aus einer CSV-Datei einen Datensatz. Der Aufbau der soll nach dem Schema Messpunkt.csv_header
    ''' </summary>
    ''' <param name="csv_file"></param>
    ''' <remarks></remarks>
    Public Sub csv_To_punkte(ByVal csv_file As String)
        If IO.File.Exists(csv_file) = False Then
            MessageBox.Show(csv_file & " does not exist.")
            Exit Sub
        End If

        Dim sr As New IO.StreamReader(csv_file)

        sr.ReadLine() ' Erste Zeile = Header = ignorieren

        Do While sr.Peek > -1
            ' Arraygröße anpassen
            ReDim Preserve Me._punkte(Me._free_punktID)
            ' Punkt hinzufügen
            Me._punkte(Me._free_punktID) = New Messpunkt()
            ' Zeile aus CSV und an das Objekt übergeben
            Me._punkte(Me._free_punktID).csvline_To_data(sr.ReadLine())

            Me._free_punktID += 1
        Loop

        sr.Close()
    End Sub
End Class
