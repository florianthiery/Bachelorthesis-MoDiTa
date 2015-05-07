Imports System.IO
Public Class CsvExport
    Private _project_dir As String = "C:\"

    Function FileName(ByVal project_dir As String) As String

        FileName = ""
        Me._project_dir = project_dir
        Dim SaveFileDialog1 As New SaveFileDialog
        With SaveFileDialog1
            .Filter = "Excel-Mappen (*.csv)|*.csv|Alle Dateien (*.*)|*.*"
            .FilterIndex = 1
            .InitialDirectory = Me._project_dir
            '.InitialDirectory = IO.Path.Combine(Application.StartupPath, "PeakTech.csv")
            .Title = "Speichern als Excel-Datei *.csv"
            If (.ShowDialog() = DialogResult.OK) Then
                FileName = SaveFileDialog1.FileName
            Else
                Exit Function
            End If
        End With

    End Function

    Function Export(ByVal DT As DataTable, ByVal FullPath As String, ByVal Delimiter As String) As Boolean

        Dim intI As Integer, intC As Integer
        Dim strValue As String
        Dim strRowValue As String
        Dim enc As System.Text.Encoding
        Dim Qualifier As String = """"
        Dim row As DataRow

        enc = System.Text.Encoding.Default
        'enc = System.Text.Encoding.GetEncoding(1252)

        'Fehlerüberwachung einschalten
        Try
            'Using swriter As IO.StreamWriter = New StreamWriter(FullPath, False, enc)
            Dim swriter As IO.StreamWriter = New StreamWriter(FullPath, False, enc)

            'header
            intC = DT.Columns.Count
            strRowValue = ""
            strValue = ""
            For intI = 0 To intC - 1
                strValue = DT.Columns(intI).ColumnName
                strValue = strValue.Replace(Qualifier, Qualifier & Qualifier)
                strRowValue = strRowValue & Qualifier & strValue & Qualifier
                If intI < intC - 1 Then
                    strRowValue = strRowValue & Delimiter
                End If
            Next
            swriter.Write(strRowValue)
            swriter.WriteLine()

            For Each row In DT.Rows
                strRowValue = ""
                strValue = ""
                For intI = 0 To intC - 1
                    strValue = row.Item(intI).ToString
                    strValue = strValue.Replace(Qualifier, Qualifier & Qualifier)

                    strRowValue = strRowValue & Qualifier & strValue & Qualifier
                    If intI < intC - 1 Then
                        strRowValue = strRowValue & Delimiter
                    End If
                Next
                swriter.Write(strRowValue)
                swriter.WriteLine()
            Next

            swriter.Close()
            'End Using

            ' Erfolgskontrolle
            If Not IO.File.Exists(FullPath) Then
                MessageBox.Show(FullPath & Environment.NewLine _
                                & "wurde nicht gespeichert!", "Fehler", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As IO.IOException
            ' Eventuell auftretenden Fehler abfangen
            MessageBox.Show(ex.Message(), "Info - IOException", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Info - Exception", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

End Class
