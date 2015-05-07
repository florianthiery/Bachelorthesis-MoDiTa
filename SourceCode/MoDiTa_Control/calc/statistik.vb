Module statistik
    ''' <summary>
    ''' Mittelwert
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function mean(ByVal value() As Double) As Double
        Dim i As Integer
        Dim n As Integer
        Dim summe As Double = 0

        n = value.Length

        For i = 0 To n - 1 Step 1
            summe = summe + value(i)
        Next

        Return summe / n
    End Function
    ''' <summary>
    ''' Standardabweichung aus Verbesserungen
    ''' </summary>
    ''' <param name="verbesserungen">
    ''' Array mit Verbesserungen
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function stddev(ByVal verbesserungen() As Double) As Double
        Dim i As Integer
        Dim n As Integer
        Dim summe_vv As Double = 0

        n = verbesserungen.Length

        For i = 0 To n - 1 Step 1
            summe_vv = summe_vv + verbesserungen(i) ^ 2
        Next

        Return Math.Sqrt(summe_vv / (n - 1))
    End Function
End Module
