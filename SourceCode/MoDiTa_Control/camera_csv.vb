Public Class camera_csv

    Function CreateTable() As DataTable
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.Add("Filename", GetType(Object))
        table.Columns.Add("ExposureTime", GetType(Object))
        table.Columns.Add("FrameRate", GetType(Object))
        table.Columns.Add("PixelClock", GetType(Object))
        table.Columns.Add("MasterGain", GetType(Object))
        table.Columns.Add("BlackLevel", GetType(Object))
        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("Filename", "ExposureTime", "FrameRate", "PixelClock", "MasterGain", "BlackLevel")
        Return table
    End Function

    Function AddRow(ByVal table As DataTable, ByVal filename As String, ByVal exptime As Double, _
                    ByVal framerate As Double, ByVal pixclock As Long, _
                    ByVal mastergain As Long, ByVal blacklevel As Long) As DataTable
        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add(filename, exptime, framerate, pixclock, mastergain, blacklevel)
        Return table
    End Function

    Sub KameraData_to_CSV(ByVal table As DataTable, ByVal speicherort As String)

        'Neues Element der CSVData
        Dim wertetocsv As New CSVData

        'Festlegung des Seperators
        wertetocsv.Separator = (";")
        'Übergabe der DataTable
        wertetocsv.CSVDataTable = table
        'Werte in Datei schreiben
        wertetocsv.SaveAsCSV(speicherort, False)

    End Sub

End Class
