''' <summary>
''' Bestimmung der Achsabweichung nach Neitzel 2006 Zfv
''' </summary>
''' <remarks></remarks>
Public Class Achsabweichungen
    Private _messungen As New ArrayList
    'Private _messungenII As New ArrayList

    Private _c As Double ' Zielachsabweichung
    Private _c_sigma As Double ' Standardabweichung Zielachsabweichung
    Private _i As Double 'Kippachsabweichung
    Private _i_sigma As Double ' Standardabweichung Kippachsabweichung

    Private _v_indexfehler As Double ' mittlerer Indexfehler des Vertikalteilkreises
    Private _v_indexfehler_sigma As Double 'Standardabweichung des mittleren Indexfehler des Vertikalteilkreises

    ''' <summary>
    ''' Zielachabweichung [gon]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property zielachsabweichung() As Double
        Get
            Return Me._c
        End Get
    End Property
    ''' <summary>
    ''' Kippachsabweichung [gon]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property kippachsabweichung() As Double
        Get
            Return Me._i
        End Get
    End Property
    ''' <summary>
    ''' Mittlere Indexabweichung des Vertikalteilkreises
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property vertikalIndexabweichung() As Double
        Get
            Return Me._v_indexfehler
        End Get
    End Property

    ''' <summary>
    ''' Fügt eine Messung ein.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub add_Messung(ByVal hz_lageI As Double, ByVal v_lageI As Double, ByVal hz_lageII As Double, ByVal v_lageII As Double)
        Dim row(3) As Double
        row(0) = hz_lageI
        row(1) = v_lageI
        row(2) = hz_lageII
        row(3) = v_lageII

        Me._messungen.Add(row)
    End Sub
    ''' <summary>
    ''' Berechnet die Zielachs- und die Kippachsabweichung und die mittlere Indexabweichung des Vertikalteilkreises.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub solve()

        Dim nl As Long = Me._messungen.Count

        Dim nu As Long = 2

        Dim ATPA(nu, nu) As Double 'N
        Dim ATPL(nu, 1) As Double 'n

        Dim LVek(nl, 1) As Double
        Dim PVek(nl, 1) As Double
        Dim AMat(nl, nu) As Double
        Dim xVek(nu, 1) As Double

        Dim zeta As Double
        Dim index_abweichung(nl) As Double

        For i1 = 1 To nl Step 1
            zeta = (Me._messungen(i1 - 1)(1) + 400.0 - Me._messungen(i1 - 1)(3)) / 2.0
            index_abweichung(i1) = zeta - Me._messungen(i1 - 1)(1)

            ' Bildung der A-Matrix (siehe Neitzel 2006 zfv)
            AMat(i1, 1) = 1.0 / (Math.Sin(gon2rad(zeta)))
            AMat(i1, 2) = 1.0 / (Math.Tan(gon2rad(zeta)))

            ' Bildung der L-Vektor (siehe Neitzel 2006 zfv)
            LVek(i1, 1) = Math.Tan(gon2rad((Me._messungen(i1 - 1)(0) - 200.0 - Me._messungen(i1 - 1)(2)) / 2.0))

            PVek(i1, 1) = 1.0
        Next

        Call MInitialize(ATPA, 0.0#)
        Call MInitialize(ATPL, 0.0#)

        Call AddWeightedObservationEquations(ATPA, AMat, PVek, AMat)
        Call AddWeightedObservationEquations(ATPL, AMat, PVek, LVek)

        For i1 = 1 To UBound(ATPL, 1)
            xVek(i1, 1) = ATPL(i1, 1)
        Next i1
        Call CholeskySolver(ATPA, xVek)

        Dim i_rad As Double
        Dim c_rad As Double

        i_rad = Math.Asin(xVek(2, 1))
        c_rad = Math.Atan(xVek(1, 1) / Math.Cos(i_rad))

        Me._i = rad2gon_digits(i_rad, 5)
        Me._c = rad2gon_digits(c_rad, 5)

        Me._v_indexfehler = Math.Round(mean(index_abweichung), 5)
    End Sub

End Class
