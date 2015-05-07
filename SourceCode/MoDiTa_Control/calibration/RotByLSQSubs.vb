Imports System.Math

Module RotByLSQSubs
    ' Copyright 2007 Prof. Dr.-Ing. Martin Schlueter, FH Mainz, schlueter@fh-mainz.de



    Function MS_Rot2x2(ByVal vOld1(,) As Double, ByVal vOld2(,) As Double, ByVal vNew1(,) As Double, ByVal vNew2(,) As Double) As Double(,)
        ' Drehmatrix aus genau zwei Vektoren (altes System, neues System) rechnen:

        Dim i0 As Long, n As Long

        Dim RotStart(3, 3) As Double
        Dim RotFinal(3, 3) As Double

        '    ' Alternativ: umspeichern nach Double:
        '    Dim myOld1(1 To 3, 1) As Double
        '    Dim myOld2(1 To 3, 1) As Double
        '    Dim myNew1(1 To 3, 1) As Double
        '    Dim myNew2(1 To 3, 1) As Double
        '    For i0 = 1 To 3
        '        myOld1(i0, 1) = vOld1(i0, 1)
        '        myOld2(i0, 1) = vOld2(i0, 1)
        '        myNew1(i0, 1) = vNew1(i0, 1)
        '        myNew2(i0, 1) = vNew2(i0, 1)
        '    Next
        '    RotStart = MS_RotBySchut(myOld1, myOld2, myNew1, myNew2)

        RotStart = MS_RotBySchut(vOld1, vOld2, vNew1, vNew2)

        ' Umspeichern (nach Double):
        n = 2
        Dim OldList(,) As Double
        ReDim OldList(3 * n, 1)
        Dim NewList(,) As Double
        ReDim NewList(3 * n, 1)
        For i0 = 1 To 3
            OldList(i0, 1) = vOld1(i0, 1)
            OldList(i0 + 3, 1) = vOld2(i0, 1)
            NewList(i0, 1) = vNew1(i0, 1)
            NewList(i0 + 3, 1) = vNew2(i0, 1)
        Next
        RotFinal = MS_RotLsq(OldList, NewList, RotStart)

        'MS_Rot2x2 = RotStart
        MS_Rot2x2 = RotFinal

    End Function

    'Public Function MS_RotLsq(XYZold As Variant, XYZNew As Variant, RotStartMatrix As Variant, Optional QisResult As Long = 0) As Variant
    Public Function MS_RotLsq(ByVal XYZold(,) As Double, ByVal XYZNew(,) As Double, ByVal RotStartMatrix(,) As Double) As Double(,)
        ' Least squares adjustment

        Dim i1 As Long, i0 As Long, i2 As Long
        Dim nl As Long, nu As Long

        nl = UBound(XYZold, 1)
        nu = 3

        ' Dim new arrays:
        Dim alpha As Double, Beta As Double, Gamma As Double, maxchange As Double
        Dim SinusRow As Long, SinusCol As Long
        Dim XYZoldTmp(3, 1) As Double
        Dim XYZNewTmp(3, 1) As Double
        Dim RotTmp(3, 3) As Double
        Dim dRot(3, 3) As Double
        Dim RotStartTrans(3, 3) As Double
        Dim LVek(nl, 1) As Double
        Dim PVek(nl, 1) As Double
        Dim AMat(nl, nu) As Double
        Dim ATPA(nu, nu) As Double
        Dim ATPL(nu, 1) As Double
        Dim xVek(nu, 1) As Double

        For i1 = 1 To nl Step 3
            PVek(i1, 1) = 1.0#
            PVek(i1 + 1, 1) = 1.0#
            PVek(i1 + 2, 1) = 1.0#

            AMat(i1, 1) = 0.0#
            AMat(i1, 2) = XYZold(i1 + 2, 1)
            AMat(i1, 3) = XYZold(i1 + 1, 1) * (-1.0#)
            AMat(i1 + 1, 1) = XYZold(i1 + 2, 1) * (-1.0#)
            AMat(i1 + 1, 2) = 0.0#
            AMat(i1 + 1, 3) = XYZold(i1, 1)
            AMat(i1 + 2, 1) = XYZold(i1 + 1, 1)
            AMat(i1 + 2, 2) = XYZold(i1, 1) * (-1.0#)
            AMat(i1 + 2, 3) = 0.0#
        Next

        i0 = 0
        ' select one of six representations as rotation matrix:
        Call RotSelectRepresentation(RotStartMatrix, SinusRow, SinusCol)
        'MsgBox "Minimum is R(" & SinusRow & ";" & SinusCol & ") = " & RotStart(SinusRow, SinusCol)
        Do
            i0 = i0 + 1
            For i1 = 1 To nl Step 3
                XYZoldTmp(1, 1) = XYZold(i1, 1)
                XYZoldTmp(2, 1) = XYZold(i1 + 1, 1)
                XYZoldTmp(3, 1) = XYZold(i1 + 2, 1)
                XYZNewTmp(1, 1) = XYZNew(i1, 1)
                XYZNewTmp(2, 1) = XYZNew(i1 + 1, 1)
                XYZNewTmp(3, 1) = XYZNew(i1 + 2, 1)
                RotStartTrans = RotStartMatrix
                ' RotStartTrans = Application.WorksheetFunction.Transpose(RotStartTrans)
                Call MatNxNT(RotStartTrans)
                'XYZNewTmp = Application.WorksheetFunction.MMult(RotStartTrans, XYZNewTmp)
                Call MatMult(RotStartTrans, XYZNewTmp, XYZNewTmp)
                LVek(i1, 1) = XYZNewTmp(1, 1) - XYZoldTmp(1, 1)
                LVek(i1 + 1, 1) = XYZNewTmp(2, 1) - XYZoldTmp(2, 1)
                LVek(i1 + 2, 1) = XYZNewTmp(3, 1) - XYZoldTmp(3, 1)
            Next

            'Calculate N=ATPA and ATPL; invert N; multiply inverse with ATPL:
            'Call NormalEquationBuild(nu, nl, AMat, LVek, PVek, ATPA, ATPL)
            'ATPA = MS_ATPA(AMat, PVek)
            'ATPL = MS_ATPL(AMat, PVek, LVek)
            'ATPA = Application.WorksheetFunction.MInverse(ATPA)
            'xVek = Application.WorksheetFunction.MMult(ATPA, ATPL)
            Call MInitialize(ATPA, 0.0#)
            Call MInitialize(ATPL, 0.0#)
            Call AddWeightedObservationEquations(ATPA, AMat, PVek, AMat)
            Call AddWeightedObservationEquations(ATPL, AMat, PVek, LVek)
            For i1 = 1 To UBound(ATPL, 1)
                xVek(i1, 1) = ATPL(i1, 1)
            Next i1
            Call CholeskySolver(ATPA, xVek) ' overwrites ATPA, xVek

            '  ' THIS should work fine, but is not yet tested:
            '  ' update angles directly:
            '  'Call RotSelectRepresentation(Rotstart, SinusRow, SinusCol)
            '  Call RotGetAngles(Rotstart, SinusRow, SinusCol, Alpha, Beta, Gamma)
            '  Alpha = Alpha + XVek(1, 1)
            '  Beta = Beta + XVek(2, 1)
            '  Gamma = Gamma + XVek(3, 1)
            '  Rotstart = RotBuild(Alpha, Beta, Gamma, SinusRow, SinusCol)

            ' THIS works fine:
            ' first update R=R*dR and orthogonalize afterwards:
            alpha = xVek(1, 1)
            Beta = xVek(2, 1)
            Gamma = xVek(3, 1)
            dRot = RotBuild(alpha, Beta, Gamma, 1, 2)
            'RotStart = Application.WorksheetFunction.MMult(RotStart, dRot)
            Call MatMult(RotStartMatrix, dRot, RotStartMatrix)

            ' orthogonalise R:
            'Call RotSelectRepresentation(Rotstart, SinusRow, SinusCol)
            Call RotGetAngles(RotStartMatrix, SinusRow, SinusCol, alpha, Beta, Gamma)
            RotStartMatrix = RotBuild(alpha, Beta, Gamma, SinusRow, SinusCol)

            'maxchange = Max(Abs(xVek(1, 1)), Abs(xVek(2, 1)), Abs(xVek(3, 1)))
            maxchange = Max(Abs(xVek(1, 1)), Abs(xVek(2, 1)))
            maxchange = Max(Abs(maxchange), Abs(xVek(3, 1)))

        Loop Until (maxchange < 0.000000000000001 Or i0 > 500)
        'MsgBox "WGS84Direction = R * LCTDirection" & vbCrLf & _
        '       "LCTDirection = RT * WGS84Direction" & vbCrLf & vbCrLf _
        '       & "Number of Iterations:" & i0 & vbCrLf _
        '       & "Max. Change in last Iteration:" & maxchange _
        '       , vbInformation, "© 2007 Prof. Schlueter, FH Mainz University of Applied Sciences"

        MS_RotLsq = RotStartMatrix
        'If (QisResult = 1) Then
        '   MS_RotLsq = ATPA
        'ElseIf (QisResult = 2) Then
        '   MS_RotLsq = AMat
        'End If

    End Function

    'Public Function MS_ATPA(rAMat As Variant, rPVek As Variant) As Variant
    '
    '    Dim nl As Long, nu As Long, i As Long, j As Long, k As Long
    '    Dim AMat As Variant, PVek As Variant
    '    AMat = rAMat
    '    PVek = rPVek
    '
    '    nl = UBound(AMat, 1)
    '    nu = UBound(AMat, 2)
    '    Dim AMatTPAMat As Variant
    '    ReDim AMatTPAMat(1 To nu, 1 To nu)
    '
    '    For i = 1 To nu
    '        For j = 1 To nu
    '            AMatTPAMat(i, j) = 0#
    '            For k = 1 To nl
    '                AMatTPAMat(i, j) = AMatTPAMat(i, j) + AMat(k, i) * PVek(k, 1) * AMat(k, j)
    '            Next k
    '        Next j
    '    Next i
    '
    '    MS_ATPA = AMatTPAMat
    '
    'End Function

    'Public Function MS_ATPL(rAMat As Variant, rPVek As Variant, rLVek As Variant) As Variant
    '
    '    Dim nl As Long, nu As Long, i As Long, k As Long
    '    Dim AMat As Variant, PVek As Variant, LVek As Variant
    '    AMat = rAMat
    '    PVek = rPVek
    '    LVek = rLVek
    '
    '    nl = UBound(AMat, 1)
    '    nu = UBound(AMat, 2)
    '    Dim AMatTPLVek As Variant
    '    ReDim AMatTPLVek(1 To nu, 1 To 1)
    '
    '    For i = 1 To nu
    '        AMatTPLVek(i, 1) = 0#
    '        For k = 1 To nl
    '            AMatTPLVek(i, 1) = AMatTPLVek(i, 1) + AMat(k, i) * PVek(k, 1) * LVek(k, 1)
    '        Next k
    '    Next i
    '
    '    MS_ATPL = AMatTPLVek
    '
    'End Function

    'Private Sub NormalEquationBuild(nu As Long, nl As Long, AMat As Variant, LVek As Variant, PVek As Variant, AMatTPAMat As Variant, AMatTPLVek As Variant)
    '
    '    Dim i As Long, j As Long, k As Long
    '
    '    For i = 1 To nu
    '        For j = 1 To nu
    '            AMatTPAMat(i, j) = 0#
    '            For k = 1 To nl
    '                AMatTPAMat(i, j) = AMatTPAMat(i, j) + AMat(k, i) * PVek(k, 1) * AMat(k, j)
    '            Next k
    '        Next j
    '        AMatTPLVek(i, 1) = 0#
    '        For k = 1 To nl
    '            AMatTPLVek(i, 1) = AMatTPLVek(i, 1) + AMat(k, i) * PVek(k, 1) * LVek(k, 1)
    '        Next k
    '    Next i
    '
    'End Sub

    Private Sub RotSelectRepresentation(ByVal rot(,) As Double, ByVal SinusRow As Long, ByVal SinusCol As Long)
        ' select one of six representations as rotation matrix

        Dim MinVal As Double
        Dim i1 As Long, i2 As Long

        ' initialize:
        SinusRow = 1
        SinusCol = 2
        MinVal = 1.0#

        ' select minimum value besides main diagonal:
        For i1 = 1 To 3
            For i2 = 1 To 3
                If (i1 <> i2) Then
                    If (Abs(rot(i1, i2)) < MinVal) Then
                        MinVal = Abs(rot(i1, i2))
                        SinusRow = i1
                        SinusCol = i2
                    End If
                End If
            Next
        Next

    End Sub

    Private Sub RotGetAngles(ByVal rot(,) As Double, ByVal SinusRow As Long, ByVal SinusCol As Long, _
                     ByVal alpha As Double, ByVal Beta As Double, ByVal Gamma As Double)
        ' Calculate angles depending on rotation matrix representation


        If (SinusRow = 1 And SinusCol = 2) Then
            Gamma = Asin((-1.0#) * rot(1, 2))
            'alpha = Atan2(rot(2, 2) / Cos(Gamma), rot(3, 2) / Cos(Gamma))
            'Beta = Atan2(rot(1, 1) / Cos(Gamma), rot(1, 3) / Cos(Gamma))
            alpha = Atan2(rot(3, 2) / Cos(Gamma), rot(2, 2) / Cos(Gamma))
            Beta = Atan2(rot(1, 3) / Cos(Gamma), rot(1, 1) / Cos(Gamma))
        ElseIf (SinusRow = 2 And SinusCol = 1) Then
            Gamma = Asin(rot(2, 1))
            'alpha = Atan2(rot(2, 2) / Cos(Gamma), (-1.0#) * rot(2, 3) / Cos(Gamma))
            'Beta = Atan2(rot(1, 1) / Cos(Gamma), (-1.0#) * rot(3, 1) / Cos(Gamma))
            alpha = Atan2((-1.0#) * rot(2, 3) / Cos(Gamma), rot(2, 2) / Cos(Gamma))
            Beta = Atan2((-1.0#) * rot(3, 1) / Cos(Gamma), rot(1, 1) / Cos(Gamma))
        ElseIf (SinusRow = 1 And SinusCol = 3) Then
            Beta = Asin(rot(1, 3))
            'alpha = Atan2(rot(3, 3) / Cos(Beta), (-1.0#) * rot(2, 3) / Cos(Beta))
            'Gamma = Atan2(rot(1, 1) / Cos(Beta), (-1.0#) * rot(1, 2) / Cos(Beta))
            alpha = Atan2((-1.0#) * rot(2, 3) / Cos(Beta), rot(3, 3) / Cos(Beta))
            Gamma = Atan2((-1.0#) * rot(1, 2) / Cos(Beta), rot(1, 1) / Cos(Beta))
        ElseIf (SinusRow = 3 And SinusCol = 1) Then
            Beta = Asin((-1.0#) * rot(3, 1))
            'alpha = Atan2(rot(3, 3) / Cos(Beta), rot(3, 2) / Cos(Beta))
            'Gamma = Atan2(rot(1, 1) / Cos(Beta), rot(2, 1) / Cos(Beta))
            alpha = Atan2(rot(3, 2) / Cos(Beta), rot(3, 3) / Cos(Beta))
            Gamma = Atan2(rot(2, 1) / Cos(Beta), rot(1, 1) / Cos(Beta))
        ElseIf (SinusRow = 2 And SinusCol = 3) Then
            alpha = Asin((-1.0#) * rot(2, 3))
            'Beta = Atan2(rot(3, 3) / Cos(alpha), rot(1, 3) / Cos(alpha))
            'Gamma = Atan2(rot(2, 2) / Cos(alpha), rot(2, 1) / Cos(alpha))
            Beta = Atan2(rot(1, 3) / Cos(alpha), rot(3, 3) / Cos(alpha))
            Gamma = Atan2(rot(2, 1) / Cos(alpha), rot(2, 2) / Cos(alpha))
        ElseIf (SinusRow = 3 And SinusCol = 2) Then
            alpha = Asin(rot(3, 2))
            'Beta = Atan2(rot(3, 3) / Cos(alpha), (-1.0#) * rot(3, 1) / Cos(alpha))
            'Gamma = Atan2(rot(2, 2) / Cos(alpha), (-1.0#) * rot(1, 2) / Cos(alpha))
            Beta = Atan2((-1.0#) * rot(3, 1) / Cos(alpha), rot(3, 3) / Cos(alpha))
            Gamma = Atan2((-1.0#) * rot(1, 2) / Cos(alpha), rot(2, 2) / Cos(alpha))
        Else
            MsgBox("Error, no matrix for this case: " & SinusRow & " ; " & SinusCol)
        End If

    End Sub

    Private Function RotBuild(ByVal alpha As Double, ByVal Beta As Double, ByVal Gamma As Double, ByVal SinusRow As Long, ByVal SinusCol As Long) As Double(,)

        Dim Rot1(3, 3) As Double
        Dim Rot2(3, 3) As Double

        Dim RotA(3, 3) As Double
        Dim RotB(3, 3) As Double
        Dim RotC(3, 3) As Double

        Call RotBuildFromSingleAngle(RotA, alpha, 2, 3)
        Call RotBuildFromSingleAngle(RotB, Beta, 3, 1)
        Call RotBuildFromSingleAngle(RotC, Gamma, 1, 2)

        'With Application.WorksheetFunction
        If (SinusRow = 1 And SinusCol = 2) Then
            'Rot1 = .MMult(RotC, RotB)
            Call MatMult(RotC, RotB, Rot1)
            'Rot2 = .MMult(RotA, Rot1)
            Call MatMult(RotA, Rot1, Rot2)
        ElseIf (SinusRow = 2 And SinusCol = 1) Then
            'Rot1 = .MMult(RotC, RotA)
            Call MatMult(RotC, RotA, Rot1)
            'Rot2 = .MMult(RotB, Rot1)
            Call MatMult(RotB, Rot1, Rot2)
        ElseIf (SinusRow = 1 And SinusCol = 3) Then
            'Rot1 = .MMult(RotB, RotC)
            Call MatMult(RotB, RotC, Rot1)
            'Rot2 = .MMult(RotA, Rot1)
            Call MatMult(RotA, Rot1, Rot2)
        ElseIf (SinusRow = 3 And SinusCol = 1) Then
            'Rot1 = .MMult(RotB, RotA)
            Call MatMult(RotB, RotA, Rot1)
            'Rot2 = .MMult(RotC, Rot1)
            Call MatMult(RotC, Rot1, Rot2)
        ElseIf (SinusRow = 2 And SinusCol = 3) Then
            'Rot1 = .MMult(RotA, RotC)
            Call MatMult(RotA, RotC, Rot1)
            'Rot2 = .MMult(RotB, Rot1)
            Call MatMult(RotB, Rot1, Rot2)
        ElseIf (SinusRow = 3 And SinusCol = 2) Then
            'Rot1 = .MMult(RotA, RotB)
            Call MatMult(RotA, RotB, Rot1)
            'Rot2 = .MMult(RotC, Rot1)
            Call MatMult(RotC, Rot1, Rot2)
        Else
            MsgBox("Error, no matrix for this case: " & SinusRow & " ; " & SinusCol)
        End If
        'End With

        RotBuild = Rot2

    End Function

    Private Sub RotBuildFromSingleAngle(ByVal rot(,) As Double, ByVal Angle As Double, ByVal Index1 As Long, ByVal Index2 As Long)

        Dim i1 As Long
        Dim i2 As Long

        For i1 = 1 To 3
            For i2 = 1 To 3
                If (i1 = i2) Then
                    rot(i1, i2) = 1.0#
                Else
                    rot(i1, i2) = 0.0#
                End If
            Next
        Next
        rot(Index1, Index1) = Cos(Angle)
        rot(Index2, Index2) = Cos(Angle)
        rot(Index1, Index2) = (-1.0#) * Sin(Angle)
        rot(Index2, Index1) = Sin(Angle)

    End Sub

    ' Gewichtetes Mittel, Eingabedaten: 2 Matrizen
    Public Function MS_WeightedMean(ByVal LVek(,) As Double, ByVal PVek(,) As Double) As Double

        Dim n1 As Long, n2 As Long, i1 As Long, i2 As Long
        Dim SummeLP As Double, SummeP As Double

        n1 = UBound(LVek, 1)
        n2 = UBound(LVek, 2)

        SummeP = 0.0#
        SummeLP = 0.0#
        For i1 = 1 To n1
            For i2 = 1 To n2
                SummeP = SummeP + PVek(i1, i2)
                SummeLP = SummeLP + LVek(i1, i2) * PVek(i1, i2)
            Next
        Next

        MS_WeightedMean = SummeLP / SummeP

    End Function



End Module
