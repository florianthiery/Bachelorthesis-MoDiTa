Public Class Punktliste
    Private _punkte() As Messpunkt
    Private _freeid As Integer = 0

    Private _csv_file As String
    Private _save_to_csv As Boolean = False



#Region "Properties"
    ''' <summary>
    ''' Gibt nächste ID zurück
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property freeID() As Integer
        Get
            Return Me._freeid
        End Get
    End Property
    ''' <summary>
    ''' Speicherort und Name der CSV-Datei
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CSV_File() As String
        Get
            Return Me._csv_file
        End Get
        Set(ByVal value As String)
            Me._csv_file = value
            Me._save_to_csv = True
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Gibt den Messpunkt mit der ID zurück
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>Messpunkt</returns>
    ''' <remarks></remarks>
    Public Function get_punkt(ByVal id As Integer) As Messpunkt
        If (id < _freeid) Then
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
        ReDim Preserve Me._punkte(Me._freeid)
        ' Punkt hinzufügen
        Me._punkte(Me._freeid) = punkt

        ' CSV Updaten
        If (Me._save_to_csv = True) Then
            Dim line As String = ""

            Dim sw As IO.StreamWriter

            
            If (IO.File.Exists(Me._csv_file) = True) Then ' Gibt es Datei schon, dann einfach anhängen.
                ' Streamwriter mit Append = true, d.h. Ziele wird an der Datei angehängt
                sw = New IO.StreamWriter(Me._csv_file, True)

                line = Me._punkte(Me._freeid).csv_line
                sw.WriteLine(line)
                sw.Flush()
                sw.Close()
                sw = Nothing
            Else ' Wenn Datei noch nicht existiert, dann erst Header in die erste Zeile. Datei wird mit dem Befehl Writeline auch
                ' erzeugt.
                ' Header
                sw = New IO.StreamWriter(Me._csv_file)
                Dim header As String = ""
                header = Me._punkte(Me._freeid).csv_header
                sw.WriteLine(header)

                ' Punkt anhängen
                line = Me._punkte(Me._freeid).csv_line
                sw.WriteLine(line)
                sw.Flush()
                sw.Close()
                sw = Nothing
            End If
        End If

        Me._freeid += 1
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
        header = Me._punkte(Me._freeid).csv_header
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
            ReDim Preserve Me._punkte(Me._freeid)
            ' Punkt hinzufügen
            Me._punkte(Me._freeid) = New Messpunkt()
            ' Zeile aus CSV und an das Objekt übergeben
            Me._punkte(Me._freeid).csvline_To_data(sr.ReadLine())

            Me._freeid += 1
        Loop

        sr.Close()
    End Sub

End Class
