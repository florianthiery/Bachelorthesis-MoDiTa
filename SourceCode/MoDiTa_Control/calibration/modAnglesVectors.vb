Module modAnglesVectors

    ' Copyright 2007 Prof. Dr.-Ing. Martin Schlueter, FH Mainz, schlueter@fh-mainz.de
    Function MS_AnglesCamera2Theo(ByVal HzTarget As Double, ByVal ZdTarget As Double, ByVal myAngles(,) As Double) As Double(,)
        ' ... nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget    : Kompensatorkorrigierte Ablesungen ("World")
        ' MyAngles              : dX und dZ (Kamerasystem)

        Dim Vec(3, 1) As Double
        'ReDim Vec(0 To 2, 0 To 1)
        Vec(1, 1) = myAngles(1, 1) ' dX
        Vec(3, 1) = myAngles(1, 2) ' dZ
        Vec(2, 1) = Math.Sqrt(1.0# - Vec(1, 1) ^ 2 - Vec(3, 1) ^ 2)

        Dim Lage As Long
        If (ZdTarget < 200.0#) Then
            Lage = 1
        Else
            Lage = 2
        End If

        ' Drehmatrix für Rotation aus Standardkameralage in Theodolitsystem:
        Dim rot(3, 1) As Double
        rot = MS_RotTheo2Camera(HzTarget, ZdTarget)

        ' Ruecktransformation:
        ' rot = Application.WorksheetFunction.Transpose(rot)
        Call MatNxNT(rot)

        'Rueckdrehung ins Theodolitsystem:
        'Vec = Application.WorksheetFunction.MMult(rot, Vec)
        Call MatMult(rot, Vec, Vec)

        'Ubergang von Richtungsvektor auf Winkel:
        MS_AnglesCamera2Theo = MS_AnglesTheodolite(Vec, Lage)

    End Function

    Function MS_AnglesTheo2Camera(ByVal HzTarget As Double, ByVal ZdTarget As Double, _
                                  ByVal HzLOSCorrected As Double, ByVal ZdLOSCorrected As Double) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget             : Kompensatorkorrigierte Ablesungen ("World")
        ' HzLOSCorrected, ZdLOSCorrected : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        '                                  (Standardablesung am Theo, wenn alle Korrekturen EINgeschaltet sind)

        Dim Lage As Long
        'If (ZdTarget < 200#) Then
        Lage = 1
        'Else
        '   Lage = 2
        'End If

        ' Drehmatrix für Theodolitverkippung:
        Dim rot(3, 3) As Double
        rot = MS_RotTheo2Camera(HzTarget, ZdTarget)

        ' Ruecktransformation; hier auskommentiert!
        ' rot = Application.WorksheetFunction.Transpose(Rot)

        ' Drehmatrix für Theodolitverkippung auf Theodolitablesung anwenden:
        MS_AnglesTheo2Camera = MS_TransTheo2Camera(HzLOSCorrected, ZdLOSCorrected, rot, Lage)

    End Function

    Function MS_TransTheo2Camera(ByVal HZ As Double, ByVal Zd As Double, ByVal rot(,) As Double, Optional ByVal Lage As Long = 0) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget    : Ablesungen OHNE Kompensatorkorrektur
        ' Rot                   : Drehmatrix aus Hz-Zielachse und Kompensatorstellung

        ' Die Richtung zum Ziel (ist im Videomodus meist ungleich der Zielachse):
        ' im Theodolitsystem:
        Dim LOTargetVec(3, 1) As Double
        LOTargetVec = MS_RtgTheodolite(HZ, Zd)

        ' Die Richtung zum Ziel nach der Kompensatorkorrektur:
        Dim LOCorrectedVec(3, 1) As Double
        Call MatMult(rot, LOTargetVec, LOCorrectedVec)

        ' Die Richtung zum Ziel in Theodolitablesungen (gon):
        Dim myAngles(1, 2) As Double
        myAngles(1, 1) = LOCorrectedVec(1, 1) ' dX
        myAngles(1, 2) = LOCorrectedVec(3, 1) ' dZ
        'MyAngles = MS_AnglesTheodolite(LOCorrectedVec, Lage)

        MS_TransTheo2Camera = myAngles

    End Function

    Function MS_AnglesTheo2World(ByVal HzTarget As Double, ByVal ZdTarget As Double, _
                                 ByVal HzLOSCorrected As Double, ByVal ZdLOSCorrected As Double, _
                                 ByVal CrossTilt As Double, ByVal LengthTilt As Double) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget             : Ablesungen OHNE Kompensatorkorrektur
        ' HzLOSCorrected, ZdLOSCorrected : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        '                                  (Standardablesung am Theo, wenn alle Korrekturen EINgeschaltet sind)
        ' CrossTilt, LengthTilt          : Kompensatorstellung; 1. quer und 2. längs zur Zielachse

        Dim Lage As Long
        If (ZdTarget < 200.0#) Then
            Lage = 1
        Else
            Lage = 2
        End If

        ' Drehmatrix für Theodolitverkippung:
        Dim rot(3, 3) As Double
        rot = MS_RotTheo2World(HzLOSCorrected, ZdLOSCorrected, CrossTilt, LengthTilt)

        ' Drehmatrix für Theodolitverkippung auf Theodolitablesung anwenden:
        MS_AnglesTheo2World = MS_TransTheo2World(HzTarget, ZdTarget, rot, Lage)

    End Function

    Function MS_AnglesWorld2Theo(ByVal HzTarget As Double, ByVal ZdTarget As Double, _
                                 ByVal HzLOSCorrected As Double, ByVal ZdLOSCorrected As Double, _
                                 ByVal CrossTilt As Double, ByVal LengthTilt As Double) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget             : Kompensatorkorrigierte Ablesungen ("World")
        ' HzLOSCorrected, ZdLOSCorrected : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        '                                  (Standardablesung am Theo, wenn alle Korrekturen EINgeschaltet sind)
        ' CrossTilt, LengthTilt          : Kompensatorstellung; 1. quer und 2. längs zur Zielachse

        Dim Lage As Long
        If (ZdTarget < 200.0#) Then
            Lage = 1
        Else
            Lage = 2
        End If

        ' Drehmatrix für Theodolitverkippung:
        Dim rot(3, 3) As Double
        rot = MS_RotTheo2World(HzLOSCorrected, ZdLOSCorrected, CrossTilt, LengthTilt)

        ' Ruecktransformation:
        Call MatNxNT(rot)

        ' Drehmatrix für Theodolitverkippung auf Theodolitablesung anwenden:
        MS_AnglesWorld2Theo = MS_TransTheo2World(HzTarget, ZdTarget, rot, Lage)

    End Function

    Function MS_TransTheo2World(ByVal HzTarget As Double, ByVal ZdTarget As Double, ByVal rot(,) As Double, Optional ByVal Lage As Long = 0) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzTarget, ZdTarget    : Ablesungen OHNE Kompensatorkorrektur
        ' Rot                   : Drehmatrix aus Hz-Zielachse und Kompensatorstellung

        ' Die Richtung zum Ziel (ist im Videomodus meist ungleich der Zielachse):
        ' im Theodolitsystem:
        Dim LOTargetVec(3, 1) As Double
        LOTargetVec = MS_RtgTheodolite(HzTarget, ZdTarget)

        ' Die Richtung zum Ziel nach der Kompensatorkorrektur:
        Dim LOCorrectedVec(3, 1) As Double
        'LOCorrectedVec = Application.WorksheetFunction.MMult(rot, LOTargetVec)
        Call MatMult(rot, LOTargetVec, LOCorrectedVec)

        ' Die Richtung zum Ziel in Theodolitablesungen (gon):
        Dim myAngles(1, 2) As Double
        myAngles = MS_AnglesTheodolite(LOCorrectedVec, Lage)

        MS_TransTheo2World = myAngles

    End Function

    Function MS_RotTheo2World(ByVal HzLOSCorrected As Double, ByVal ZdLOSCorrected As Double, ByVal CrossTilt As Double, ByVal LengthTilt As Double) As Double(,)
        ' Kompensatorkorrektur nach Schlueter mit expliziter Drehmatrix

        ' HzLOSCorrected, ZdLOSCorrected : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        '                                  (Standardablesung am Theo, wenn alle Korrekturen EINgeschaltet sind)
        ' CrossTilt, LengthTilt          : Kompensatorstellung; quer und längs zur Zielachse

        ' Betrag und Richtung der Theodolitverkippung:
        Dim MyTilt(1, 2) As Double
        MyTilt = MS_TiltAbs(HzLOSCorrected, ZdLOSCorrected, CrossTilt, LengthTilt)

        'Einheitsvektor, um welchen gedreht wird:
        Dim AbsTilt As Double, DirectionTilt As Double
        AbsTilt = MyTilt(1, 1) / 200 * Math.PI 'rad
        DirectionTilt = MyTilt(1, 2) - 100.0# ' gon
        Dim Vec(3, 1) As Double
        Vec = MS_RtgTheodolite(DirectionTilt, 100.0#)

        ' Drehmatrix für Theodolitverkippung aus
        ' Drehung mit beliebigem Einheitsvektor als Drehachse:
        Dim rot(3, 3) As Double
        rot = MS_RotAroundVec(AbsTilt, Vec)

        MS_RotTheo2World = rot

    End Function

    Function MS_RotAroundVec(ByVal alpha As Double, ByVal Vec(,) As Double) As Double(,)
        ' Drehmatrix für Drehung umd den Winkelbetrag Alpha um die Achse Vec

        Dim rot(3, 3) As Double, dCosA As Double, dSinA As Double

        dCosA = Math.Cos(alpha)
        dSinA = Math.Sin(alpha)
        rot(1, 1) = dCosA + Vec(1, 1) ^ 2 * (1 - dCosA)
        rot(1, 2) = Vec(1, 1) * Vec(2, 1) * (1 - dCosA) - Vec(3, 1) * dSinA
        rot(1, 3) = Vec(1, 1) * Vec(3, 1) * (1 - dCosA) + Vec(2, 1) * dSinA
        rot(2, 1) = Vec(1, 1) * Vec(2, 1) * (1 - dCosA) + Vec(3, 1) * dSinA
        rot(2, 2) = dCosA + Vec(2, 1) ^ 2 * (1 - dCosA)
        rot(2, 3) = Vec(2, 1) * Vec(3, 1) * (1 - dCosA) - Vec(1, 1) * dSinA
        rot(3, 1) = Vec(1, 1) * Vec(3, 1) * (1 - dCosA) - Vec(2, 1) * dSinA
        rot(3, 2) = Vec(2, 1) * Vec(3, 1) * (1 - dCosA) + Vec(1, 1) * dSinA
        rot(3, 3) = dCosA + Vec(3, 1) ^ 2 * (1 - dCosA)

        MS_RotAroundVec = rot

    End Function


    Function MS_TiltAbs(ByVal HzLOSCorrected As Double, ByVal ZdLOSCorrected As Double, ByVal CrossTilt As Double, ByVal LengthTilt As Double) As Double(,)
        ' Berechnung der Theodolitverkippung aus Kompensatorablesung: Betrag und Richtungswinkel in gon

        ' HzLOSCorrected, ZdLOSCorrected : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        '                                  (Standardablesung am Theo, wenn alle Korrekturen EINgeschaltet sind)
        ' CrossTilt, LengthTilt          : Kompensatorstellung; 1. quer und 2. längs zur Zielachse

        Dim myAngles(1, 2) As Double
        Dim HzLOSUnCorrectedRad As Double, CrossTiltRad As Double, LengthTiltRad As Double
        Dim AbsTilt As Double, DirectionTilt As Double, Rho As Double

        ' Hz-Ablesung um Kompensatorkorrektur bereinigen:
        Dim HzLOSUnCorrected As Double
        ' HzLOSUnCorrected = HzLOSCorrected ' Fall: Kompensatorablesung bezieht sich auc korrigerte Hz-Ablesung
        ' ZdLOSUnCorrected = HzLOSCorrected ' Fall: Kompensatorablesung bezieht sich auc korrigerte Hz-Ablesung
        myAngles = MS_AnglesWorld2TheoWalser(HzLOSCorrected, ZdLOSCorrected, CrossTilt, LengthTilt)
        HzLOSUnCorrected = myAngles(1, 1)

        Rho = 200.0# / Math.PI
        HzLOSUnCorrectedRad = HzLOSUnCorrected / Rho
        CrossTiltRad = CrossTilt / Rho
        LengthTiltRad = LengthTilt / Rho

        AbsTilt = Math.Sqrt(CrossTiltRad ^ 2 + LengthTiltRad ^ 2)
        If (AbsTilt < 0.00000001) Then
            DirectionTilt = HzLOSUnCorrectedRad
        Else
            ' clockwise (Richtungswinkel):

            ' Unterschied atan2 zwischen vba und vb.net:
            ' vba (x,y)
            ' vb.net (y,x)
            'DirectionTilt = Application.WorksheetFunction.Atan2(LengthTiltRad, CrossTiltRad)
            DirectionTilt = Math.Atan2(CrossTiltRad, LengthTiltRad)
            DirectionTilt = HzLOSUnCorrectedRad + DirectionTilt
        End If

        AbsTilt = AbsTilt * Rho
        DirectionTilt = DirectionTilt * Rho
        DirectionTilt = MS_WinkelNachVollkreisGon(DirectionTilt)

        myAngles(1, 1) = AbsTilt
        myAngles(1, 2) = DirectionTilt
        MS_TiltAbs = myAngles

    End Function

    Function MS_RotTheo2Camera(ByVal HZ As Double, ByVal Zd As Double) As Double(,)
        ' Drehung Theodolitsystem nach Kamerasystem mit expliziter Drehmatrix
        ' Hz, Zd : Ablesung Zielachse (Fadenkreuzmitte) OHNE Kompensatorkorrektur

        Dim HzRad As Double, ZdRad As Double
        HzRad = HZ / 200.0# * Math.PI
        ZdRad = Zd / 200.0# * Math.PI

        ' 2. Drehung um Z-Achse:
        Dim RotZ(3, 3) As Double
        RotZ(1, 1) = Math.Cos(HzRad)
        RotZ(1, 2) = -Math.Sin(HzRad)
        RotZ(1, 3) = 0.0#
        RotZ(2, 1) = Math.Sin(HzRad)
        RotZ(2, 2) = Math.Cos(HzRad)
        RotZ(2, 3) = 0.0#
        RotZ(3, 1) = 0.0#
        RotZ(3, 2) = 0.0#
        RotZ(3, 3) = 1.0#

        ' 1. Drehung um X-Achse:
        Dim RotX(3, 3) As Double
        RotX(1, 1) = 1.0#
        RotX(1, 2) = 0.0#
        RotX(1, 3) = 0.0#
        RotX(2, 1) = 0.0#
        RotX(2, 2) = Math.Sin(ZdRad)
        RotX(2, 3) = Math.Cos(ZdRad)
        RotX(3, 1) = 0.0#
        RotX(3, 2) = Math.Cos(ZdRad)
        RotX(3, 3) = -Math.Sin(ZdRad)

        Call MatMult(RotX, RotZ, RotX)

        MS_RotTheo2Camera = RotX

    End Function

    Function MS_AnglesTheodolite(ByVal MyRtg(,) As Double, Optional ByVal Lage As Long = 0) As Double(,)
        ' Theodolitablesung in Gon rekonstruieren; aus normiertem Richtungsvektor:

        Dim myAngles(1, 2) As Double
        Dim HzGon As Double, ZdGon As Double
        Dim SinHz As Double, CosHz As Double

        If (Lage = 2) Then
            'HzGon = Application.WorksheetFunction.Atan2(-1.0# * MyRtg(2, 1), -1.0# * MyRtg(1, 1))
            HzGon = Math.Atan2(-1.0# * MyRtg(1, 1), -1.0# * MyRtg(2, 1))
        Else
            'HzGon = Application.WorksheetFunction.Atan2(MyRtg(2, 1), MyRtg(1, 1))
            HzGon = Math.Atan2(MyRtg(1, 1), MyRtg(2, 1))
        End If

        SinHz = Math.Sin(HzGon)
        CosHz = Math.Cos(HzGon)
        If (Math.Abs(SinHz) > Math.Abs(CosHz)) Then
            'ZdGon = Application.WorksheetFunction.Atan2(MyRtg(3, 1), MyRtg(1, 1) / SinHz)
            ZdGon = Math.Atan2(MyRtg(1, 1) / SinHz, MyRtg(3, 1))
        Else
            'ZdGon = Application.WorksheetFunction.Atan2(MyRtg(3, 1), MyRtg(2, 1) / CosHz)
            ZdGon = Math.Atan2(MyRtg(2, 1) / CosHz, MyRtg(3, 1))
        End If

        HzGon = HzGon * 200.0# / Math.PI
        ZdGon = ZdGon * 200.0# / Math.PI
        HzGon = MS_WinkelNachVollkreisGon(HzGon)
        ZdGon = MS_WinkelNachVollkreisGon(ZdGon)

        myAngles(1, 1) = HzGon
        myAngles(1, 2) = ZdGon
        MS_AnglesTheodolite = myAngles

    End Function

    Function MS_RtgNorm(ByVal X As Double, ByVal y As Double, ByVal z As Double) As Double(,)
        ' Richtungsvektor auf Laenge Eins normieren:

        Dim MyRtg(3, 1) As Double, Norm As Double

        Norm = Math.Sqrt(X * X + y * y + z * z)
        MyRtg(1, 1) = X / Norm
        MyRtg(2, 1) = y / Norm
        MyRtg(3, 1) = z / Norm
        MS_RtgNorm = MyRtg

    End Function

    Function MS_RtgTerminalRad(ByVal AzRad As Double, ByVal ElevRad As Double) As Double(,)
        ' Richtungsvektor berechnen; aus Terminalablesung im Bogenmaß:

        Dim MyRtg(3, 1) As Double

        MyRtg(1, 1) = Math.Cos(ElevRad) * Math.Cos(AzRad)
        MyRtg(2, 1) = Math.Cos(ElevRad) * Math.Sin(AzRad)
        MyRtg(3, 1) = Math.Sin(ElevRad)
        MS_RtgTerminalRad = MyRtg

    End Function

    Function MS_RtgTheodolite(ByVal HzGon As Double, ByVal ZdGon As Double) As Double(,)
        ' Richtungsvektor berechnen; aus Theodolitablesung in Gon:

        Dim MyRtg(3, 1) As Double, HzRad As Double, ZdRad As Double

        HzRad = HzGon / 200.0# * Math.PI
        ZdRad = ZdGon / 200.0# * Math.PI

        MyRtg(1, 1) = Math.Sin(ZdRad) * Math.Sin(HzRad)
        MyRtg(2, 1) = Math.Sin(ZdRad) * Math.Cos(HzRad)
        MyRtg(3, 1) = Math.Cos(ZdRad)
        MS_RtgTheodolite = MyRtg

    End Function

    Function MS_WinkelNachVollkreisGon(ByVal dblWinkel As Double) As Double

        If (Math.Abs(dblWinkel) < 10000) Then
            Do While dblWinkel > 400.0#
                dblWinkel = dblWinkel - 400.0#
            Loop

            Do While dblWinkel < 0.0#
                dblWinkel = dblWinkel + 400.0#
            Loop
        End If

        MS_WinkelNachVollkreisGon = dblWinkel

    End Function

    Function MS_AnglesWorld2TheoWalser(ByVal HzLineOfSight As Double, ByVal ZdLineofsight As Double, ByVal CrossTilt As Double, ByVal LengthTilt As Double) As Double(,)
        ' Kompensatorkorrektur abziehen; nach Walser

        ' HzLineOfSight, ZdLineofsight  : Ablesung Zielachse (Fadenkreuzmitte) MIT Kompensatorkorrektur
        ' CrossTilt, LengthTilt           : Kompensatorstellung; quer und längs zur Zielachse

        Dim myAngles(1, 2) As Double
        Dim HzLineOfSightRad As Double, ZdLineofsightRad As Double
        Dim CrossTiltRad As Double, LengthTiltRad As Double
        Dim HzUncorrected As Double, ZdUncorrected As Double

        HzLineOfSightRad = HzLineOfSight / 200.0# * Math.PI
        ZdLineofsightRad = ZdLineofsight / 200.0# * Math.PI
        CrossTiltRad = CrossTilt / 200.0# * Math.PI
        LengthTiltRad = LengthTilt / 200.0# * Math.PI

        ZdUncorrected = ZdLineofsightRad - LengthTiltRad
        HzUncorrected = HzLineOfSightRad - CrossTiltRad / Math.Tan(ZdLineofsightRad)

        HzUncorrected = HzUncorrected * 200.0# / Math.PI
        ZdUncorrected = ZdUncorrected * 200.0# / Math.PI
        HzUncorrected = MS_WinkelNachVollkreisGon(HzUncorrected)
        ZdUncorrected = MS_WinkelNachVollkreisGon(ZdUncorrected)

        myAngles(1, 1) = HzUncorrected
        myAngles(1, 2) = ZdUncorrected
        MS_AnglesWorld2TheoWalser = myAngles

    End Function

End Module
