Imports System.Math

Module own_math

#Region "Konstanten"
    Private Const PI As Double = 3.14159265358979
    Private Const VelocityMinGonPerS As Double = 0.0012 ' should be between 0.0010 and 0.0020
    Private Const VelocityMaxGonPerS As Double = 49.9999895977954
#End Region

    ''' <summary>
    ''' Rechnet RAD in GON um und rundet auf angegebene Stellen.
    ''' </summary>
    ''' <param name="rad">Winkel in Rad</param>
    ''' <param name="digits">Anzahl der Stellen</param>
    ''' <returns>Winkel in GON mit angegebenen Stellen</returns>
    ''' <remarks></remarks>
    Function rad2gon_digits(ByVal rad As Object, ByVal digits As Object) As Double

        rad2gon_digits = Round((CDbl(Replace(rad, ".", ",")) * 200.0# / PI), digits)

    End Function

    ''' <summary>
    ''' Rechnet GON so um, dass ein Vollkreis entsteht.
    ''' </summary>
    ''' <param name="dblWinkel">Winkel in GON</param>
    ''' <returns>Winkel in VollkreisGON</returns>
    ''' <remarks></remarks>
    Function WinkelNachVollkreisGon(ByVal dblWinkel As Double) As Double

        If (Abs(dblWinkel) < 10000) Then
            Do While dblWinkel > 400.0#
                dblWinkel = dblWinkel - 400.0#
            Loop

            Do While dblWinkel < 0.0#
                dblWinkel = dblWinkel + 400.0#
            Loop
        End If

        WinkelNachVollkreisGon = dblWinkel

    End Function

    ''' <summary>
    ''' Rechnet GON in RAD um, mit Vollkreis.
    ''' </summary>
    ''' <param name="gon">Winkel in GON</param>
    ''' <returns>Winkel in RAD</returns>
    ''' <remarks></remarks>
    Function gon2rad(ByVal gon As Double) As Double

        'gon2rad = WinkelNachVollkreisGon(gon)
        gon2rad = Round(WinkelNachVollkreisGon(gon) / 200.0# * PI, 9)

    End Function

    ''' <summary>
    ''' Rechnet die Geschwindigkeit auf der Scollbar in RAD um.
    ''' </summary>
    ''' <param name="scrollbar">Wert auf der Scrollbar</param>
    ''' <returns>Geschwindigkeit in rad/s</returns>
    ''' <remarks>mit Exponentialfunktion gearbeitet</remarks>
    Function Scroll2RadPerSec(ByVal scrollbar As Long) As Double

        Dim VeloA As Double
        Dim VeloB As Double

        ' Exponentialkoeff. für Geschwindigkeitsschieber
        VeloA = VelocityMinGonPerS / 200.0# * PI
        VeloB = (VelocityMaxGonPerS / 200.0# * PI) / VeloA
        VeloB = Log(VeloB) / 1000.0#

        Scroll2RadPerSec = VeloA * Exp(VeloB * scrollbar)

    End Function

    ''' <summary>
    ''' Rechnet die Geschwindigkeit auf der Scollbar in GON um.
    ''' </summary>
    ''' <param name="scrollbar">Wert auf der Scrollbar</param>
    ''' <returns>Geschwindigkeit in gon/s</returns>
    ''' <remarks>mit Exponentialfunktion gearbeitet</remarks>
    Function Scroll2GonPerSec(ByVal scrollbar As Long) As Double

        Dim VeloA As Double
        Dim VeloB As Double

        ' Exponentialkoeff. für Geschwindigkeitsschieber
        VeloA = VelocityMinGonPerS / 200.0# * PI
        VeloB = (VelocityMaxGonPerS / 200.0# * PI) / VeloA
        VeloB = Log(VeloB) / 1000.0#

        Scroll2GonPerSec = VeloA * Exp(VeloB * scrollbar)
        Scroll2GonPerSec = Scroll2GonPerSec * 200 / PI

    End Function

    ''' <summary>
    ''' Rechnet Wert auf der Scollbar Fein in GON um
    ''' </summary>
    ''' <param name="scrollbar">Wert der Scollbar</param>
    ''' <returns>Winkel in gon</returns>
    ''' <remarks></remarks>
    Function Scroll2GonFein(ByVal scrollbar As Long) As Double
        Scroll2GonFein = scrollbar / 100000
    End Function

    ''' <summary>
    ''' Rechnet Wert auf der Scollbar Grob in GON um
    ''' </summary>
    ''' <param name="scrollbar">Wert der Scollbar</param>
    ''' <returns>Winkel in gon</returns>
    ''' <remarks></remarks>
    Function Scroll2GonGrob(ByVal scrollbar As Long) As Double
        Scroll2GonGrob = scrollbar / 1000
    End Function



End Module
