Module modTheodolite


    'Public Function BuildNameByDateTimer(ByVal prefix As String, ByVal suffix As String) As String

    '    Dim varDate As Object, strDate As String
    '    Dim dblTimer As Double, strTimer As String
    '    Dim lngHours As Long, lngMins As Long, lngSecs As Long

    'varDate = Split(Date, ".")
    '    strDate = Format(varDate(2) - 2000, "00") & Format(varDate(1), "00") & Format(varDate(0), "00")

    '    dblTimer = Timer
    '    lngHours = Int(dblTimer / 3600.0#)
    '    lngMins = Int(((dblTimer - (lngHours * 3600.0#)) / 60.0#))
    '    lngSecs = Int(100.0# * (dblTimer - (lngHours * 3600.0#) - (lngMins * 60.0#)))
    '    strTimer = Format(lngHours, "00") & Format(lngMins, "00") & Format(lngSecs, "0000")

    '    BuildNameByDateTimer = prefix & "_" & strDate & "_" & strTimer & suffix

    'End Function

    Sub KalibrierRasterPunkt(ByVal n As Long, ByVal i As Long, ByRef fAlpha As Double, ByRef fRadius As Double)

        'Fadenkreuzkreisringe TM5100 bei:
        ' Ring innen:   1 / 12
        ' Ring 2:       2 / 12
        ' Ring 3:       4 / 12
        ' Ring 4:       8 / 12
        ' Ring aussen: 12 / 12

        ' fAlpha:  [0, ..., 1]
        ' fRadius: [0, ..., 1, ...]

        Dim n01 As Long, n02 As Long, n03 As Long, n04 As Long, n05 As Long, n06 As Long, _
            n07 As Long, n08 As Long, n09 As Long, n10 As Long, n11 As Long, n12 As Long

        Select Case n

            Case Is <= 4
                n01 = 4  ' Radius innen mit 4*1 Rastermessungen
                fRadius = 3.0# / 12.0#
                fAlpha = 1.0# / CDbl(n01) * (CDbl(i) + 0.5)

            Case Is <= 24
                n01 = 4  ' Radius innen mit 4*1 Rastermessungen
                n02 = 8  ' Radius 2 mit 4*2 Rastermessungen
                n03 = 12 ' Radius aussen mit 4*3 Rastermessungen
                Select Case i
                    Case Is <= n01
                        fRadius = 3.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n01) * (CDbl(i) + 0.5)
                    Case Is <= n01 + n02
                        fRadius = 6.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n02) * (CDbl(i - n01) + 0.5)
                    Case Else
                        fRadius = 10.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n03) * (CDbl(i - (n01 + n02)) + 0.5)
                End Select

            Case Is <= 36
                n01 = 8  ' Radius innen mit 4*2 Rastermessungen
                n02 = 12 ' Radius 2 mit 4*3 Rastermessungen
                n03 = 16 ' Radius aussen mit 4*4 Rastermessungen
                Select Case i
                    Case Is <= n01
                        fRadius = 3.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n01) * (CDbl(i) + 0.5)
                    Case Is <= n01 + n02
                        fRadius = 6.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n02) * (CDbl(i - n01) + 0.5)
                    Case Else
                        fRadius = 10.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n03) * (CDbl(i - (n01 + n02)) + 0.5)
                End Select

            Case Is <= 60
                n01 = 8  ' Radius innen mit 4*2 Rastermessungen
                n02 = 12 ' Radius 2 mit 4*3 Rastermessungen
                n03 = 16 ' Radius aussen mit 4*4 Rastermessungen
                n04 = 24 ' Radius ausserhalb mit 4*6 Rastermessungen
                Select Case i
                    Case Is <= n01
                        fRadius = 3.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n01) * (CDbl(i) + 0.5)
                    Case Is <= n01 + n02
                        fRadius = 6.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n02) * (CDbl(i - n01) + 0.5)
                    Case Is <= n01 + n02 + n03
                        fRadius = 10.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n03) * (CDbl(i - (n01 + n02)) + 0.5)
                    Case Else
                        fRadius = 14.0# / 12.0#
                        fAlpha = 1.0# / CDbl(n04) * (CDbl(i - (n01 + n02 + n03)) + 0.5)
                End Select

                'Case Is <= 252 ' 12 Radien mit ... Rastermessungen
            Case Else
                n01 = 4  ' Radius innen mit 4*1 Rastermessungen
                n02 = 8  ' Radius 2 mit 4*2 Rastermessungen
                n03 = 8  ' Radius 3 mit 4*2 Rastermessungen
                n04 = 8  ' Radius 4 mit 4*2 Rastermessungen
                n05 = 16 ' Radius 5 mit 4*4 Rastermessungen
                n06 = 16 ' Radius 6 mit 4*4 Rastermessungen
                n07 = 32 ' Radius 7 mit 4*8 Rastermessungen
                n08 = 32 ' Radius 8 mit 4*8 Rastermessungen
                n09 = 32 ' Radius 9 mit 4*8 Rastermessungen
                n10 = 32 ' Radius 10 mit 4*8 Rastermessungen
                n11 = 32 ' Radius 11 mit 4*8 Rastermessungen
                n12 = 32 ' Radius aussen mit 4*8 Rastermessungen
                Select Case i
                    Case Is <= n01
                        fRadius = 0.04
                        fAlpha = 1.0# / CDbl(n01) * (CDbl(i) + 0.5)
                    Case Is <= n01 + n02
                        fRadius = 0.12
                        fAlpha = 1.0# / CDbl(n02) * (CDbl(i - n01) + 0.5)
                    Case Is <= n01 + n02 + n03
                        fRadius = 0.21
                        fAlpha = 1.0# / CDbl(n03) * (CDbl(i - (n01 + n02)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04
                        fRadius = 0.27
                        fAlpha = 1.0# / CDbl(n04) * (CDbl(i - (n01 + n02 + n03)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05
                        fRadius = 0.4
                        fAlpha = 1.0# / CDbl(n05) * (CDbl(i - (n01 + n02 + n03 + n04)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06
                        fRadius = 0.48
                        fAlpha = 1.0# / CDbl(n06) * (CDbl(i - (n01 + n02 + n03 + n04 + n05)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06 + n07
                        fRadius = 0.56
                        fAlpha = 1.0# / CDbl(n07) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08
                        fRadius = 0.72
                        fAlpha = 1.0# / CDbl(n08) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06 + n07)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09
                        fRadius = 0.79
                        fAlpha = 1.0# / CDbl(n09) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09 + n10
                        fRadius = 0.87
                        fAlpha = 1.0# / CDbl(n10) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09)) + 0.5)
                    Case Is <= n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09 + n10 + n11
                        fRadius = 1.05
                        fAlpha = 1.0# / CDbl(n11) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09 + n10)) + 0.5)
                    Case Else
                        fRadius = 1.12
                        fAlpha = 1.0# / CDbl(n12) * (CDbl(i - (n01 + n02 + n03 + n04 + n05 + n06 + n07 + n08 + n09 + n10 + n11)) + 0.5)
                End Select

        End Select

        'fAlpha = fAlpha * 2# * pi

    End Sub

    Function KalibrierRasterRichtung(ByVal n As Long, ByVal i As Long, ByVal HzMitte As Double, ByVal ZdMitte As Double, ByVal HzRand As Double, ByVal ZdRand As Double) As Double(,)
        ' Theodolitablesung in Gon rekonstruieren; aus normiertem Richtungsvektor:

        Dim myAngles(1, 2) As Double

        Dim Lage As Long
        If (ZdMitte < 200.0#) Then
            Lage = 1
        Else
            Lage = 2
        End If

        Dim myRtgMitte(3, 1) As Double
        Dim myRtgRand(3, 1) As Double
        Dim myRtgNeu(3, 1) As Double
        Dim dLength As Double

        myRtgMitte = MS_RtgTheodolite(HzMitte, ZdMitte)
        myRtgRand = MS_RtgTheodolite(HzRand, ZdRand)

        ' Vektor von Mitte zum Rand, senkrecht auf der Vektor von Mitte:
        dLength = SkalarProd(myRtgMitte, myRtgRand)
        myRtgRand(1, 1) = myRtgRand(1, 1) - myRtgMitte(1, 1) * dLength
        myRtgRand(2, 1) = myRtgRand(2, 1) - myRtgMitte(2, 1) * dLength
        myRtgRand(3, 1) = myRtgRand(3, 1) - myRtgMitte(3, 1) * dLength

        ' Drehmatrix für Drehung umd den Winkelbetrag Alpha um eine gegebene Achse:
        Dim fAlpha As Double, fAlphaRad As Double, fRadius As Double
        Call KalibrierRasterPunkt(n, i, fAlpha, fRadius)

        fAlphaRad = fAlpha * 2 * Math.PI
        Dim rot(3, 3) As Double
        myRtgNeu(1, 1) = myRtgRand(1, 1) * fRadius
        myRtgNeu(2, 1) = myRtgRand(2, 1) * fRadius
        myRtgNeu(3, 1) = myRtgRand(3, 1) * fRadius
        rot = MS_RotAroundVec(fAlphaRad, myRtgMitte)
        'myRtgNeu = Application.WorksheetFunction.MMult(rot, myRtgNeu)
        Call MatMult(rot, myRtgNeu, myRtgNeu)

        myRtgNeu(1, 1) = myRtgMitte(1, 1) * dLength + myRtgNeu(1, 1)
        myRtgNeu(2, 1) = myRtgMitte(2, 1) * dLength + myRtgNeu(2, 1)
        myRtgNeu(3, 1) = myRtgMitte(3, 1) * dLength + myRtgNeu(3, 1)
        myRtgNeu = MS_RtgNorm((myRtgNeu(1, 1)), (myRtgNeu(2, 1)), (myRtgNeu(3, 1)))

        myAngles = MS_AnglesTheodolite(myRtgNeu, Lage)
        KalibrierRasterRichtung = myAngles

    End Function

End Module
