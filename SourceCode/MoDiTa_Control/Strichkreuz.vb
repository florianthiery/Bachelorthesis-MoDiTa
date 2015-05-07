
Public Class Strichkreuz
    ' Dient hauptsächlich zur Idendifikation der Strichkreuzerkennung
    Private _strichkreuz_zeit As Date
    ' Format wie das Datum als String im Dateiname dargestellt wird
    Private _format_Date As String = "yyyyMMdd-HHmmssff"

    Private _Maske As HRegion

    Private _strichkreuz_erkannt As Boolean = False

    Private _strichkreuz_punkte_Y() As Double
    Private _strichkreuz_punkte_X() As Double
    Private _zentrum_Y As Double
    Private _zentrum_X As Double

#Region "Property"

    Public Sub New()
        Me._strichkreuz_zeit = Date.Now
    End Sub
    ''' <summary>
    ''' Wurde das Strichkreuz schon erkannt?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property strichkreuz_erkannt() As Boolean
        Get
            Return Me._strichkreuz_erkannt
        End Get
    End Property
    ''' <summary>
    ''' Zeitpunkt Strichkreuzerkennung, dient im wesentlich zur Idendifikation mehrer Strichkreuze.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property strichkreuz_zeit() As Date
        Get
            Return Me._strichkreuz_zeit
        End Get
        Set(ByVal value As Date)
            Me._strichkreuz_zeit = value
        End Set
    End Property
    ''' <summary>
    ''' Format wie das Datum als String im Dateiname dargestellt wird. Default: "yyyyMMdd-HHmmssff"
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property formate_Date() As String
        Get
            Return Me._format_Date
        End Get
        Set(ByVal value As String)
            Me._format_Date = value
        End Set
    End Property
    ''' <summary>
    ''' X-Koordiante des Strichkreuzzentrums
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property zentrum_x() As Double
        Get
            Return Me._zentrum_X
        End Get
    End Property
    ''' <summary>
    ''' Y-Koordiante des Strichkreuzzentrums
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property zentrum_y() As Double
        Get
            Return Me._zentrum_Y
        End Get
    End Property
    ''' <summary>
    ''' X-Koordianten der restlichen Strichkreuzpunkten
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property strichkreuzpunkte_X() As Double()
        Get
            Return Me._strichkreuz_punkte_X
        End Get
    End Property
    ''' <summary>
    ''' Y-Koordianten der restlichen Strichkreuzpunkten
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property strichkreuzpunkte_Y() As Double()
        Get
            Return Me._strichkreuz_punkte_Y
        End Get
    End Property
    Public ReadOnly Property anzahl_strichkreuzpunkte() As Integer
        Get
            Return Me._strichkreuz_punkte_Y.Length
        End Get
    End Property
    ''' <summary>
    ''' Strichkreuzmaske
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property maske() As HRegion
        Get
            Return Me._Maske
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Speichert die Koordinaten (.csv) und die Maske auf Festplatte
    ''' </summary>
    ''' <param name="folder"></param>
    ''' <remarks></remarks>
    Public Sub export_strichkreuz(ByVal folder As String)
        Me.save_strichkreuz_make(folder)
        Me.export_strichkreuz_to_csv(folder)
    End Sub
    ''' <summary>
    ''' Lädt die Koordianten (.csv) und die Maske von der Festplatte
    ''' </summary>
    ''' <param name="file">Ein Dateiname angeben csv oder tif, beide werden geladen</param>
    ''' <remarks></remarks>
    Public Sub import_strichkreuz(ByVal file As String)
        'Auslesen der Zeitinformation aus Dateiname
        If IO.File.Exists(file) = False Then
            MessageBox.Show("Crosshair-Files does not exist.")
            Exit Sub
        End If
        Dim filename As String = New IO.FileInfo(file).Name
        Dim temp1() As String = Split(filename, ".")
        Dim temp2() As String = Split(temp1(0), "_")

        If (temp2.Length = 1) Then
            'Dateinamen
            Dim filename_csv As String = New IO.FileInfo(file).DirectoryName + "crosshairxy_" + temp2(1) + ".csv"
            Dim filename_maske As String = New IO.FileInfo(file).DirectoryName + "crosshairmaske_" + temp2(1) + ".tif"

            'Strichkreuzzeit setzen
            Me._strichkreuz_zeit = Date.ParseExact(temp2(1), Me._format_Date, Nothing)

            'Einladen
            Try
                Me.import_strichkreuz_from_csv(filename_csv)
                Me.load_strichkreuz_maske(filename_maske)
                Me._strichkreuz_erkannt = True
            Catch ex As Exception
                Me._strichkreuz_erkannt = False
            End Try
        Else
            MessageBox.Show("Files does not exist.")
        End If

    End Sub
    ''' <summary>
    ''' Lädt das letzte Strichkreuz (Koordianten und Maske)
    ''' </summary>
    ''' <param name="folder"></param>
    ''' <remarks></remarks>
    Public Sub import_latest_strichkreuz(ByVal folder As String)
        Dim filename As String
        Dim filelist() As String

        filelist = System.IO.Directory.GetFiles(folder)
        filename = latest_file(filelist, "crosshairxy")

        Me.import_strichkreuz(filename)
    End Sub
    Private Function latest_file(ByVal liste() As String, ByVal prefix As String, Optional ByVal extension As String = ".csv") As String
        ' Function sucht letzte (neuste) Datei im Verzeichnis, mit Hilfe des Datum im Dateinamen(!)
        Dim maxindex As Integer = -1
        Dim maxdate As Date = Date.ParseExact("19000101-01000000", Me._format_Date, Nothing)

        For i = 0 To liste.Length - 1 Step 1
            If (String.Compare(New IO.FileInfo(liste(i)).Extension, extension) = 0) Then
                Dim temp1 As String = New IO.FileInfo(liste(i)).Name
                Dim temp2() As String = Split(temp1, ".")
                If (temp2.Length = 2) Then
                    Dim temp3() As String = Split(temp2(0), "_")
                    If (temp3.Length = 2) Then
                        If (InStr(temp3(0), prefix) <> 0) Then
                            Dim tempdate As Date
                            tempdate = Date.ParseExact(temp3(1), Me._format_Date, Nothing)
                            If (Date.Compare(maxdate, tempdate) = -1) Then
                                maxindex = i
                                maxdate = tempdate
                            End If
                        End If
                    End If
                End If
            End If
        Next
        If (maxindex >= 0) Then
            Return liste(maxindex)
        Else
            Return ""
        End If
    End Function
#Region "Maske und Bild"
    ''' <summary>
    ''' Speichert das Bild auf Festplatte
    ''' </summary>
    ''' <param name="folder"></param>
    ''' <param name="bild"></param>
    ''' <remarks></remarks>
    Public Sub save_strichkreuz_image(ByVal folder As String, ByVal bild As HImage)
        Dim filename As String
        filename = folder + "crosshairimage_" + Me._strichkreuz_zeit.ToString(Me._format_Date) + ".tif"
        Try
            bild.WriteImage("tiff", 0, filename)
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub save_strichkreuz_make(ByVal folder As String)
        Dim filename As String
        filename = folder + "crosshairmaske_" + Me._strichkreuz_zeit.ToString(Me._format_Date) + ".tif"
        Try
            Me._Maske.WriteRegion(filename)
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub load_strichkreuz_maske(ByVal file As String)
        Try
            Me._Maske.ReadRegion(file)
        Catch Ex As HDevEngineException
            MessageBox.Show(Ex.Message, "Fehler in einer HALCON-Prozedur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "Koordinaten"
    Private Sub export_strichkreuz_to_csv(ByVal folder As String)
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

        Dim filename As String
        filename = folder + "crosshairxy_" + Me._strichkreuz_zeit.ToString(Me._format_Date) + ".csv"

        Dim csv As New CSVData
        csv.Separator = ";"
        csv.TextQualifier = """"
        csv.CSVDataTable = dt

        csv.SaveAsCSV(filename, True)
    End Sub

    Private Sub import_strichkreuz_from_csv(ByVal name As String)
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
#End Region
#Region "Erkennung Strichkreuze"
    ''' <summary>
    ''' Erkennt die Position des Strichkreuzes eines TM5100, TDA5005 (Industriesysteme)"
    ''' </summary>
    ''' <param name="image">
    ''' Bild mit Strichkreuz vor einen homogenen Hintergrund
    ''' </param>
    ''' <param name="blickfeld_groesse">
    ''' 0 - Blickfeld klein
    ''' 1 - Blickfeld groß
    ''' </param>
    ''' <param name="maskenfaktor">
    ''' Faktor, um wieviel die Maske zur Strichstärke vergrößert wird
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub def_strichkreuz_tm5100(ByVal image As HImage, ByVal blickfeld_groesse As Integer, Optional ByVal maskenfaktor As Long = 2)
        Dim bv As New bildverarbeitung
        Dim punkte_x(1), punkte_y(1) As Double
        Dim flag As Boolean

        flag = bv.strichkreuz_tm5100(image, blickfeld_groesse, maskenfaktor, Me._Maske, punkte_x, punkte_y)
        If (flag = True) Then
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

            Me._strichkreuz_erkannt = True
        Else
            Me._strichkreuz_erkannt = False
        End If
    End Sub
    ''' <summary>
    ''' Erkennung Strichkreuz eines TS30, TCRM1103 ("Geodätische Geräte")
    ''' </summary>
    ''' <param name="image">
    ''' Bild mit Strichkreuz vor einen homogenen Hintergrund
    ''' </param>
    ''' <param name="blickfeld_groesse">
    ''' 0 - Blickfeld klein
    ''' 1 - Blickfeld groß
    ''' </param>
    ''' <param name="maskenfaktor">
    ''' Faktor, um wieviel die Maske zur Strichstärke vergrößert wird
    ''' </param>
    ''' <remarks></remarks>
    Public Sub def_strichkreuz_ts30(ByVal image As HImage, ByVal blickfeld_groesse As Integer, Optional ByVal maskenfaktor As Long = 2)
        Dim bv As New bildverarbeitung
        Dim punkte_x(1), punkte_y(1) As Double
        Dim flag As Boolean

        flag = bv.strichkreuz_ts30(image, blickfeld_groesse, maskenfaktor, Me._Maske, punkte_x, punkte_y)
        If (flag = True) Then

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

            Me._strichkreuz_erkannt = True
        Else
            Me._strichkreuz_erkannt = False
        End If
    End Sub
#End Region

End Class
