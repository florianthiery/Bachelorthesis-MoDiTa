Imports System.Math

Module modSelfCalibrationP
    ' Transformationsparameter aus vielen Richtungsbeobachtungen
    ' in EINER Fernrohrlage auf EIN feststehendes Ziel:
    Public Function SK1P(ByVal FadenkreuzXC As Double, ByVal FadenkreuzYC As Double, _
                  ByVal Theodlit_Hz_Zd_CI_LI1(,) As Double, ByVal Kamera_XC_YC1(,) As Double, ByVal Gewicht1(,) As Double) As Double(,)

        Dim i0 As Long, i1 As Long
        Dim vAngles(1, 2) As Double
        Dim vTargetAngles(1, 2) As Double
        Dim vTargetAngles0(1, 2) As Double
        Dim vTransformation(6, 1) As Double
        'ReDim vTransformation(1 To 9, 1 To 1) 'mstmp

        ' Eine Deklaration pro Target:
        Dim nlGes As Long, nl1 As Long
        Dim dRestHz01 As Double, dRestZd01 As Double

        nl1 = UBound(Theodlit_Hz_Zd_CI_LI1, 1)
        nlGes = nl1
        Dim TheoHz01 As Double, TheoZd01 As Double
        Dim TheoHz1(nl1, 1) As Double, TheoZd1(nl1, 1) As Double
        '
        Dim TheoHzA01 As Double, TheoZdA01 As Double
        Dim TheoHzA1(nl1, 1) As Double, TheoZdA1(nl1, 1) As Double
        
        ' Eine Gesamtdeklaration fuer Beobachtungen zu allen Targets:
        Dim CameraDX(nlGes, 1) As Double, CameraDZ(nlGes, 1) As Double
        Dim PixelXC(nlGes, 1) As Double, PixelYC(nlGes, 1) As Double
        Dim gewicht(nlGes, 1) As Double


        ' Transformation vom Weltsystem1 (kompensatorkorrigiert) ins Theodolitsystem1:
        For i1 = 1 To nl1
            vAngles = MS_AnglesWorld2Theo(Theodlit_Hz_Zd_CI_LI1(i1, 1), Theodlit_Hz_Zd_CI_LI1(i1, 2), _
                                          Theodlit_Hz_Zd_CI_LI1(i1, 1), Theodlit_Hz_Zd_CI_LI1(i1, 2), _
                                          Theodlit_Hz_Zd_CI_LI1(i1, 3), Theodlit_Hz_Zd_CI_LI1(i1, 4))
            TheoHz1(i1, 1) = vAngles(1, 1) ' Hz im Theodolitsystem
            TheoZd1(i1, 1) = vAngles(1, 2) ' Zd im Theodolitsystem
        Next
        ' Startwerte fuer Zielachsrichtung (Fadenkreuzmitte) im Theodolitsystem:
        TheoHz01 = MS_WeightedMean(TheoHz1, Gewicht1) ' gewichtetes Mittel
        TheoZd01 = MS_WeightedMean(TheoZd1, Gewicht1) ' gewichtetes Mittel

        For i0 = 1 To 10
            ' Transformation vom Theodolitsystem1 ins Kamerasystem1:
            For i1 = 1 To nl1
                vAngles = MS_AnglesTheo2Camera(CDbl(TheoHz1(i1, 1)), CDbl(TheoZd1(i1, 1)), TheoHz01, TheoZd01)
                CameraDX(i1, 1) = vAngles(1, 1)
                CameraDZ(i1, 1) = vAngles(1, 2)
                PixelXC(i1, 1) = Kamera_XC_YC1(i1, 1)
                PixelYC(i1, 1) = Kamera_XC_YC1(i1, 2)
                gewicht(i1, 1) = Gewicht1(i1, 1)
            Next

            ' Berechnung Transformationsparameter zwischen Kamerasystem und Pixelsystem:
            vTransformation = TransformationEB(PixelXC, PixelYC, CameraDX, CameraDZ, gewicht)

            ' Zielachsrichtung (Fadenkreuzmitte) vom Pixelsystem ins Kamerasystem:
            vTargetAngles0(1, 1) = XTransformationEB(FadenkreuzXC, FadenkreuzYC, vTransformation)
            vTargetAngles0(1, 2) = YTransformationEB(FadenkreuzXC, FadenkreuzYC, vTransformation)

            ' Zielachsrichtung zum Ziel 1 vom Kamerasystem ins Theodolitsystem:
            vAngles = MS_AnglesCamera2Theo(TheoHz01, TheoZd01, vTargetAngles0)
            TheoHzA01 = vAngles(1, 1)
            TheoZdA01 = vAngles(1, 2)
            ' Richtungen zum Ziel 1 vom Kamerasystem ins Theodolitsystem:
            For i1 = 1 To nl1
                vTargetAngles(1, 1) = XTransformationEB(Kamera_XC_YC1(i1, 1), Kamera_XC_YC1(i1, 2), vTransformation)
                vTargetAngles(1, 2) = YTransformationEB(Kamera_XC_YC1(i1, 1), Kamera_XC_YC1(i1, 2), vTransformation)
                vAngles = MS_AnglesCamera2Theo(CDbl(TheoHz1(i1, 1)), CDbl(TheoZd1(i1, 1)), vTargetAngles)
                TheoHzA1(i1, 1) = vAngles(1, 1)
                TheoZdA1(i1, 1) = vAngles(1, 2)
            Next
            ' Restklaffung fuer Zielachsrichtung zum Ziel 1 im Theodolitsystem:
            dRestHz01 = MS_WeightedMean(TheoHzA1, Gewicht1) - TheoHzA01
            dRestZd01 = MS_WeightedMean(TheoZdA1, Gewicht1) - TheoZdA01
            ' Neue Werte fuer Zielachsrichtung zum Ziel 1 im Theodolitsystem:
            TheoHz01 = TheoHz01 + dRestHz01
            TheoZd01 = TheoZd01 + dRestZd01

            'If (i0 = 1) Then
            If (Abs(dRestHz01) < 0.00000001 And Abs(dRestZd01) < 0.00000001) Then
                'MsgBox "Anzahl Iterationen = " & i0 & vbCrLf _
                '    & dRestHz01 & "   " & dRestZd01, vbInformation, "Information"
                SK1P = vTransformation
                Exit Function
            End If
        Next
        'MsgBox "Anzahl Iterationen > " & i0, vbCritical, "Error in SK1P"
        Call MInitialize(vTransformation, 999)
        SK1P = vTransformation
    End Function

    ' Transformationsparameter aus vielen Richtungsbeobachtungen
    ' in BEIDEN Fernrohrlagen auf ZWEI feststehende Ziele;
    ' idealerweise EIN Nivellierblick und EIN stark geneigter Blick:
    Function SK4P(ByVal FadenkreuzXC As Double, ByVal FadenkreuzYC As Double, _
                 ByVal Theodlit_Hz_Zd_CI_LI1(,) As Double, ByVal Kamera_XC_YC1(,) As Double, ByVal Gewicht1(,) As Double, _
                 ByVal Theodlit_Hz_Zd_CI_LI2(,) As Double, ByVal Kamera_XC_YC2(,) As Double, ByVal Gewicht2(,) As Double, _
                 ByVal Theodlit_Hz_Zd_CI_LI3(,) As Double, ByVal Kamera_XC_YC3(,) As Double, ByVal Gewicht3(,) As Double, _
                 ByVal Theodlit_Hz_Zd_CI_LI4(,) As Double, ByVal Kamera_XC_YC4(,) As Double, ByVal Gewicht4(,) As Double) As Double(,)

        Dim i0 As Long, i1 As Long
        Dim vAngles(1, 2) As Double
        Dim vTargetAngles(1, 2) As Double
        Dim vTargetAngles0(1, 2) As Double
        Dim vTransformation(6, 1) As Double

        ' Eine Deklaration pro Target:
        Dim nlGes As Long, nl1 As Long, nl2 As Long, nl3 As Long, nl4 As Long
        Dim dRestHz01 As Double, dRestZd01 As Double
        Dim dRestHz02 As Double, dRestZd02 As Double
        Dim dRestHz03 As Double, dRestZd03 As Double
        Dim dRestHz04 As Double, dRestZd04 As Double

        nl1 = UBound(Theodlit_Hz_Zd_CI_LI1, 1)
        nl2 = UBound(Theodlit_Hz_Zd_CI_LI2, 1)
        nl3 = UBound(Theodlit_Hz_Zd_CI_LI3, 1)
        nl4 = UBound(Theodlit_Hz_Zd_CI_LI4, 1)
        nlGes = nl1 + nl2 + nl3 + nl4
        Dim TheoHz01 As Double, TheoZd01 As Double
        Dim TheoHz02 As Double, TheoZd02 As Double
        Dim TheoHz03 As Double, TheoZd03 As Double
        Dim TheoHz04 As Double, TheoZd04 As Double
        Dim TheoHz1(nl1, 1) As Double, TheoZd1(nl1, 1) As Double
        Dim TheoHz2(nl2, 1) As Double, TheoZd2(nl2, 1) As Double
        Dim TheoHz3(nl3, 1) As Double, TheoZd3(nl3, 1) As Double
        Dim TheoHz4(nl4, 1) As Double, TheoZd4(nl4, 1) As Double
        '
        Dim TheoHzA01 As Double, TheoZdA01 As Double
        Dim TheoHzA02 As Double, TheoZdA02 As Double
        Dim TheoHzA03 As Double, TheoZdA03 As Double
        Dim TheoHzA04 As Double, TheoZdA04 As Double
        Dim TheoHzA1(nl1, 1) As Double, TheoZdA1(nl1, 1) As Double
        Dim TheoHzA2(nl2, 1) As Double, TheoZdA2(nl2, 1) As Double
        Dim TheoHzA3(nl2, 1) As Double, TheoZdA3(nl2, 1) As Double
        Dim TheoHzA4(nl2, 1) As Double, TheoZdA4(nl2, 1) As Double

        ' Eine Gesamtdeklaration fuer Beobachtungen zu allen Targets:
        Dim CameraDX(nlGes, 1) As Double, CameraDZ(nlGes, 1) As Double
        Dim PixelXC(nlGes, 1) As Double, PixelYC(nlGes, 1) As Double
        Dim gewicht(nlGes, 1) As Double

        ' Transformation vom Weltsystem1 (kompensatorkorrigiert) ins Theodolitsystem1:
        For i1 = 1 To nl1
            vAngles = MS_AnglesWorld2Theo(CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI1(i1, 4)))
            TheoHz1(i1, 1) = vAngles(1, 1) ' Hz im Theodolitsystem
            TheoZd1(i1, 1) = vAngles(1, 2) ' Zd im Theodolitsystem
        Next
        ' Startwerte fuer Zielachsrichtung (Fadenkreuzmitte) im Theodolitsystem:
        TheoHz01 = MS_WeightedMean(TheoHz1, Gewicht1) ' gewichtetes Mittel
        TheoZd01 = MS_WeightedMean(TheoZd1, Gewicht1) ' gewichtetes Mittel

        ' Transformation vom Weltsystem2 (kompensatorkorrigiert) ins Theodolitsystem2:
        For i1 = 1 To nl2
            vAngles = MS_AnglesWorld2Theo(CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI2(i1, 4)))
            TheoHz2(i1, 1) = vAngles(1, 1) ' Hz im Theodolitsystem
            TheoZd2(i1, 1) = vAngles(1, 2) ' Zd im Theodolitsystem
        Next
        ' Startwerte fuer Zielachsrichtung (Fadenkreuzmitte) im Theodolitsystem:
        TheoHz02 = MS_WeightedMean(TheoHz2, Gewicht2) ' gewichtetes Mittel
        TheoZd02 = MS_WeightedMean(TheoZd2, Gewicht2) ' gewichtetes Mittel

        ' Transformation vom Weltsystem3 (kompensatorkorrigiert) ins Theodolitsystem3:
        For i1 = 1 To nl3
            vAngles = MS_AnglesWorld2Theo(CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI3(i1, 4)))
            TheoHz3(i1, 1) = vAngles(1, 1) ' Hz im Theodolitsystem
            TheoZd3(i1, 1) = vAngles(1, 2) ' Zd im Theodolitsystem
        Next
        ' Startwerte fuer Zielachsrichtung (Fadenkreuzmitte) im Theodolitsystem:
        TheoHz03 = MS_WeightedMean(TheoHz3, Gewicht3) ' gewichtetes Mittel
        TheoZd03 = MS_WeightedMean(TheoZd3, Gewicht3) ' gewichtetes Mittel

        ' Transformation vom Weltsystem4 (kompensatorkorrigiert) ins Theodolitsystem4:
        For i1 = 1 To nl4
            vAngles = MS_AnglesWorld2Theo(CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 2)), _
                                          CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI4(i1, 4)))
            TheoHz4(i1, 1) = vAngles(1, 1) ' Hz im Theodolitsystem
            TheoZd4(i1, 1) = vAngles(1, 2) ' Zd im Theodolitsystem
        Next
        ' Startwerte fuer Zielachsrichtung (Fadenkreuzmitte) im Theodolitsystem:
        TheoHz04 = MS_WeightedMean(TheoHz4, Gewicht4) ' gewichtetes Mittel
        TheoZd04 = MS_WeightedMean(TheoZd4, Gewicht4) ' gewichtetes Mittel

        For i0 = 1 To 10
            ' Transformation vom Theodolitsystem1 ins Kamerasystem1:
            For i1 = 1 To nl1
                vAngles = MS_AnglesTheo2Camera(CDbl(TheoHz1(i1, 1)), CDbl(TheoZd1(i1, 1)), TheoHz01, TheoZd01)
                CameraDX(i1, 1) = vAngles(1, 1)
                CameraDZ(i1, 1) = vAngles(1, 2)
                PixelXC(i1, 1) = Kamera_XC_YC1(i1, 1)
                PixelYC(i1, 1) = Kamera_XC_YC1(i1, 2)
                gewicht(i1, 1) = Gewicht1(i1, 1)
            Next
            ' Transformation vom Theodolitsystem2 ins Kamerasystem2:
            For i1 = 1 To nl2
                vAngles = MS_AnglesTheo2Camera(CDbl(TheoHz2(i1, 1)), CDbl(TheoZd2(i1, 1)), TheoHz02, TheoZd02)
                CameraDX(nl1 + i1, 1) = vAngles(1, 1)
                CameraDZ(nl1 + i1, 1) = vAngles(1, 2)
                PixelXC(nl1 + i1, 1) = Kamera_XC_YC2(i1, 1)
                PixelYC(nl1 + i1, 1) = Kamera_XC_YC2(i1, 2)
                gewicht(nl1 + i1, 1) = Gewicht2(i1, 1)
            Next
            ' Transformation vom Theodolitsystem3 ins Kamerasystem3:
            For i1 = 1 To nl3
                vAngles = MS_AnglesTheo2Camera(CDbl(TheoHz3(i1, 1)), CDbl(TheoZd3(i1, 1)), TheoHz03, TheoZd03)
                CameraDX(nl1 + nl2 + i1, 1) = vAngles(1, 1)
                CameraDZ(nl1 + nl2 + i1, 1) = vAngles(1, 2)
                PixelXC(nl1 + nl2 + i1, 1) = Kamera_XC_YC3(i1, 1)
                PixelYC(nl1 + nl2 + i1, 1) = Kamera_XC_YC3(i1, 2)
                gewicht(nl1 + nl2 + i1, 1) = Gewicht3(i1, 1)
            Next
            ' Transformation vom Theodolitsystem4 ins Kamerasystem4:
            For i1 = 1 To nl4
                vAngles = MS_AnglesTheo2Camera(CDbl(TheoHz4(i1, 1)), CDbl(TheoZd4(i1, 1)), TheoHz04, TheoZd04)
                CameraDX(nl1 + nl2 + nl3 + i1, 1) = vAngles(1, 1)
                CameraDZ(nl1 + nl2 + nl3 + i1, 1) = vAngles(1, 2)
                PixelXC(nl1 + nl2 + nl3 + i1, 1) = Kamera_XC_YC4(i1, 1)
                PixelYC(nl1 + nl2 + nl3 + i1, 1) = Kamera_XC_YC4(i1, 2)
                gewicht(nl1 + nl2 + nl3 + i1, 1) = Gewicht4(i1, 1)
            Next

            ' Berechnung Transformationsparameter zwischen Kamerasystem und Pixelsystem:
            vTransformation = TransformationEB(PixelXC, PixelYC, CameraDX, CameraDZ, gewicht)

            ' Zielachsrichtung (Fadenkreuzmitte) vom Pixelsystem ins Kamerasystem:
            vTargetAngles0(1, 1) = XTransformationEB(FadenkreuzXC, FadenkreuzYC, vTransformation)
            vTargetAngles0(1, 2) = YTransformationEB(FadenkreuzXC, FadenkreuzYC, vTransformation)

            ' Zielachsrichtung zum Ziel 1 vom Kamerasystem ins Theodolitsystem:
            vAngles = MS_AnglesCamera2Theo(TheoHz01, TheoZd01, vTargetAngles0)
            TheoHzA01 = vAngles(1, 1)
            TheoZdA01 = vAngles(1, 2)
            ' Richtungen zum Ziel 1 vom Kamerasystem ins Theodolitsystem:
            For i1 = 1 To nl1
                vTargetAngles(1, 1) = XTransformationEB(CDbl(Kamera_XC_YC1(i1, 1)), CDbl(Kamera_XC_YC1(i1, 2)), vTransformation)
                vTargetAngles(1, 2) = YTransformationEB(CDbl(Kamera_XC_YC1(i1, 1)), CDbl(Kamera_XC_YC1(i1, 2)), vTransformation)
                vAngles = MS_AnglesCamera2Theo(CDbl(TheoHz1(i1, 1)), CDbl(TheoZd1(i1, 1)), vTargetAngles)
                TheoHzA1(i1, 1) = vAngles(1, 1)
                TheoZdA1(i1, 1) = vAngles(1, 2)
            Next
            ' Restklaffung fuer Zielachsrichtung zum Ziel 1 im Theodolitsystem:
            dRestHz01 = MS_WeightedMean(TheoHzA1, Gewicht1) - TheoHzA01
            dRestZd01 = MS_WeightedMean(TheoZdA1, Gewicht1) - TheoZdA01
            ' Neue Werte fuer Zielachsrichtung zum Ziel 1 im Theodolitsystem:
            TheoHz01 = TheoHz01 + dRestHz01
            TheoZd01 = TheoZd01 + dRestZd01

            ' Zielachsrichtung zum Ziel 2 vom Kamerasystem ins Theodolitsystem:
            vAngles = MS_AnglesCamera2Theo(TheoHz02, TheoZd02, vTargetAngles0)
            TheoHzA02 = vAngles(1, 1)
            TheoZdA02 = vAngles(1, 2)
            ' Richtungen zum Ziel 2 vom Kamerasystem ins Theodolitsystem:
            For i1 = 1 To nl2
                vTargetAngles(1, 1) = XTransformationEB(CDbl(Kamera_XC_YC2(i1, 1)), CDbl(Kamera_XC_YC2(i1, 2)), vTransformation)
                vTargetAngles(1, 2) = YTransformationEB(CDbl(Kamera_XC_YC2(i1, 1)), CDbl(Kamera_XC_YC2(i1, 2)), vTransformation)
                vAngles = MS_AnglesCamera2Theo(CDbl(TheoHz2(i1, 1)), CDbl(TheoZd2(i1, 1)), vTargetAngles)
                TheoHzA2(i1, 1) = vAngles(1, 1)
                TheoZdA2(i1, 1) = vAngles(1, 2)
            Next
            ' Restklaffung fuer Zielachsrichtung zum Ziel 2 im Theodolitsystem:
            dRestHz02 = MS_WeightedMean(TheoHzA2, Gewicht2) - TheoHzA02
            dRestZd02 = MS_WeightedMean(TheoZdA2, Gewicht2) - TheoZdA02
            ' Neue Werte fuer Zielachsrichtung zum Ziel 2 im Theodolitsystem:
            TheoHz02 = TheoHz02 + dRestHz02
            TheoZd02 = TheoZd02 + dRestZd02

            ' Zielachsrichtung zum Ziel 3 vom Kamerasystem ins Theodolitsystem:
            vAngles = MS_AnglesCamera2Theo(TheoHz03, TheoZd03, vTargetAngles0)
            TheoHzA03 = vAngles(1, 1)
            TheoZdA03 = vAngles(1, 2)
            ' Richtungen zum Ziel 3 vom Kamerasystem ins Theodolitsystem:
            For i1 = 1 To nl3
                vTargetAngles(1, 1) = XTransformationEB(CDbl(Kamera_XC_YC3(i1, 1)), CDbl(Kamera_XC_YC3(i1, 2)), vTransformation)
                vTargetAngles(1, 2) = YTransformationEB(CDbl(Kamera_XC_YC3(i1, 1)), CDbl(Kamera_XC_YC3(i1, 2)), vTransformation)
                vAngles = MS_AnglesCamera2Theo(CDbl(TheoHz3(i1, 1)), CDbl(TheoZd3(i1, 1)), vTargetAngles)
                TheoHzA3(i1, 1) = vAngles(1, 1)
                TheoZdA3(i1, 1) = vAngles(1, 2)
            Next
            ' Restklaffung fuer Zielachsrichtung zum Ziel 3 im Theodolitsystem:
            dRestHz03 = MS_WeightedMean(TheoHzA3, Gewicht3) - TheoHzA03
            dRestZd03 = MS_WeightedMean(TheoZdA3, Gewicht3) - TheoZdA03
            ' Neue Werte fuer Zielachsrichtung zum Ziel 3 im Theodolitsystem:
            TheoHz03 = TheoHz03 + dRestHz03
            TheoZd03 = TheoZd03 + dRestZd03

            ' Zielachsrichtung zum Ziel 4 vom Kamerasystem ins Theodolitsystem:
            vAngles = MS_AnglesCamera2Theo(TheoHz04, TheoZd04, vTargetAngles0)
            TheoHzA04 = vAngles(1, 1)
            TheoZdA04 = vAngles(1, 2)
            ' Richtungen zum Ziel 4 vom Kamerasystem ins Theodolitsystem:
            For i1 = 1 To nl4
                vTargetAngles(1, 1) = XTransformationEB(CDbl(Kamera_XC_YC4(i1, 1)), CDbl(Kamera_XC_YC4(i1, 2)), vTransformation)
                vTargetAngles(1, 2) = YTransformationEB(CDbl(Kamera_XC_YC4(i1, 1)), CDbl(Kamera_XC_YC4(i1, 2)), vTransformation)
                vAngles = MS_AnglesCamera2Theo(CDbl(TheoHz4(i1, 1)), CDbl(TheoZd4(i1, 1)), vTargetAngles)
                TheoHzA4(i1, 1) = vAngles(1, 1)
                TheoZdA4(i1, 1) = vAngles(1, 2)
            Next
            ' Restklaffung fuer Zielachsrichtung zum Ziel 4 im Theodolitsystem:
            dRestHz04 = MS_WeightedMean(TheoHzA4, Gewicht4) - TheoHzA04
            dRestZd04 = MS_WeightedMean(TheoZdA4, Gewicht4) - TheoZdA04
            ' Neue Werte fuer Zielachsrichtung zum Ziel 4 im Theodolitsystem:
            TheoHz04 = TheoHz04 + dRestHz04
            TheoZd04 = TheoZd04 + dRestZd04

            'MsgBox "Anzahl Iterationen = " & i0 & vbCrLf _
            '    & dRestHz01 & "   " & dRestZd01 & vbCrLf _
            '    & dRestHz02 & "   " & dRestZd02 & vbCrLf _
            '    & dRestHz03 & "   " & dRestZd03 & vbCrLf _
            '   & dRestHz04 & "   " & dRestZd04, vbInformation, "Information"
            'If (i0 = 1) Then
            If (Abs(dRestHz01) < 0.00000001 And Abs(dRestZd01) < 0.00000001 And _
                Abs(dRestHz02) < 0.00000001 And Abs(dRestZd02) < 0.00000001 And _
                Abs(dRestHz03) < 0.00000001 And Abs(dRestZd03) < 0.00000001 And _
                Abs(dRestHz04) < 0.00000001 And Abs(dRestZd04) < 0.00000001) Then
                SK4P = vTransformation
                Exit Function
            End If
        Next
        'MsgBox "Anzahl Iterationen > " & i0, vbCritical, "Error in SK4P"
        Call MInitialize(vTransformation, 999)
        SK4P = vTransformation

    End Function

    ' Berechnung einer ausgeglichenen Richtung:
    Function MessBlickP(ByVal Theodlit_Hz_Zd_CI_LI(,) As Double, ByVal Kamera_XC_YC(,) As Double, ByVal Transformation(,) As Double) As Double(,)

        Dim vAngles(,) As Double
        ReDim vAngles(1, 2)
        Dim TheoHz As Double, TheoZd As Double

        ' Hz, Zd vom Weltsystem ins Theodolitsystem:
        vAngles = MS_AnglesWorld2Theo(CDbl(Theodlit_Hz_Zd_CI_LI(1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI(1, 2)), _
                                      CDbl(Theodlit_Hz_Zd_CI_LI(1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI(1, 2)), _
                                      CDbl(Theodlit_Hz_Zd_CI_LI(1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI(1, 4)))
        TheoHz = vAngles(1, 1)
        TheoZd = vAngles(1, 2)

        ' dX, dZ im Kamerasystem:
        vAngles(1, 1) = XTransformationEB(CDbl(Kamera_XC_YC(1, 1)), CDbl(Kamera_XC_YC(1, 2)), Transformation)
        vAngles(1, 2) = YTransformationEB(CDbl(Kamera_XC_YC(1, 1)), CDbl(Kamera_XC_YC(1, 2)), Transformation)

        ' Hz, Zd im Theodolitsystem:
        vAngles = MS_AnglesCamera2Theo(TheoHz, TheoZd, vAngles)

        ' Hz, Zd im Weltsystem:
        vAngles = MS_AnglesTheo2World(CDbl(vAngles(1, 1)), CDbl(vAngles(1, 2)), _
                                      CDbl(Theodlit_Hz_Zd_CI_LI(1, 1)), CDbl(Theodlit_Hz_Zd_CI_LI(1, 2)), _
                                      CDbl(Theodlit_Hz_Zd_CI_LI(1, 3)), CDbl(Theodlit_Hz_Zd_CI_LI(1, 4)))

        MessBlickP = vAngles

    End Function

    ' Berechnung der Zielachsrichtung (Fadenkreuzmitte) im Weltsystem
    ' als gewichtetes Mittel aller ausgeglichenen Richtungsbeobachtungen

    Function SKBlickP(ByVal FadenkreuzXC As Double, ByVal FadenkreuzYC As Double, _
                   ByVal Theodlit_Hz_Zd_CI_LI(,) As Double, ByVal Kamera_XC_YC(,) As Double, ByVal gewicht(,) As Double, _
                   ByVal Transformation(,) As Double) As Double(,)

        Dim nl As Long, i1 As Long ', i0 As Long
        

        Dim oneTheodlit_Hz_Zd_CI_LI(1, 4) As Double
        Dim oneKamera_XC_YC(1, 2) As Double
        Dim vAngles(1, 2) As Double

        Dim Hz0 As Double, Zd0 As Double, sGewicht As Double

        Hz0 = 0.0#
        Zd0 = 0.0#
        sGewicht = 0.0#
        nl = UBound(Theodlit_Hz_Zd_CI_LI, 1)
        For i1 = 1 To nl
            oneTheodlit_Hz_Zd_CI_LI(1, 1) = Theodlit_Hz_Zd_CI_LI(i1, 1)
            oneTheodlit_Hz_Zd_CI_LI(1, 2) = Theodlit_Hz_Zd_CI_LI(i1, 2)
            oneTheodlit_Hz_Zd_CI_LI(1, 3) = Theodlit_Hz_Zd_CI_LI(i1, 3)
            oneTheodlit_Hz_Zd_CI_LI(1, 4) = Theodlit_Hz_Zd_CI_LI(i1, 4)
            oneKamera_XC_YC(1, 1) = Kamera_XC_YC(i1, 1)
            oneKamera_XC_YC(1, 2) = Kamera_XC_YC(i1, 2)
            vAngles = MessBlickP(oneTheodlit_Hz_Zd_CI_LI, oneKamera_XC_YC, Transformation)
            Hz0 = Hz0 + vAngles(1, 1) * gewicht(i1, 1)
            Zd0 = Zd0 + vAngles(1, 2) * gewicht(i1, 1)
            sGewicht = sGewicht + gewicht(i1, 1)
        Next
        vAngles(1, 1) = Hz0 / sGewicht
        vAngles(1, 2) = Zd0 / sGewicht

        SKBlickP = vAngles

    End Function

End Module
