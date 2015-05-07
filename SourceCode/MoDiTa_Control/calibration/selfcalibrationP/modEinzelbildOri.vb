Imports System.Math

Module modEinzelbildOri

    'Function TransformationEB(XAlt As Variant, YAlt As Variant, XNeu As Variant, YNeu As Variant, Weight As Variant) As Variant
    Function TransformationEB(ByVal xb(,) As Double, ByVal yb(,) As Double, ByVal XObj(,) As Double, ByVal YObj(,) As Double, ByVal Weight(,) As Double) As Double(,)

        ' Modell: Kamerakalibrierung nach Wrobel
        Dim nu As Long, nEq As Long
        Dim ATA(,), ATL(,), Design(,), Beob(,), Solution(3, 1), Startwert(,) As Double

        Dim rot(3, 3) As Double, deltaRot(3, 3) As Double, tempRot(3, 3) As Double


        Dim i0 As Long, i1 As Long, i2 As Long, iIteration As Long, maxSolution As Double
        Dim ck As Double, XminusX0 As Double, YminusY0 As Double, ZminusZ0 As Double
        Dim Absolut As Double, sX As Double, sY As Double, sN As Double, sqrW As Double
        Dim x0 As Double, y0 As Double  ', radius As Double

        ' Initialisierung:
        nu = 6  ' 9
        nEq = UBound(xb, 1)

        ' Vergleich Arraylaengen
        If ((UBound(xb, 1) <> nEq) Or (UBound(xb, 2) <> 1) _
         Or (UBound(yb, 1) <> nEq) Or (UBound(yb, 2) <> 1) _
         Or (UBound(XObj, 1) <> nEq) Or (UBound(XObj, 2) <> 1) _
         Or (UBound(YObj, 1) <> nEq) Or (UBound(YObj, 2) <> 1) _
         Or (UBound(Weight, 1) <> nEq) Or (UBound(Weight, 2) <> 1) _
         Or (nu / 2 > nEq)) Then
            MsgBox("Error in Function TransformationEB!" & _
            vbCrLf & vbCrLf & "At least " & Str(nu / 2) & " points" & _
            vbCrLf & "in five columns of equal length are required!", , "Error")


            Call MInitialize(Solution, 999)


            TransformationEB = Solution
        End If

        ReDim ATA(nu, nu)
        ReDim ATL(nu, 1)
        ReDim Startwert(nu, 1)
        ReDim Solution(nu, 1)
        ReDim Design(1, nu)
        ReDim Beob(1, 1)

        ' Startwerte festlegen:
        Startwert(1, 1) = 0.0#        'omega (rad)
        Startwert(2, 1) = 0.0#        'phi   (rad)
        Startwert(3, 1) = 0.0#        'kappa (rad)
        Startwert(4, 1) = 153750.0#        'ck (Kamerakonstante)
        Startwert(5, 1) = 707.0#      'x0 (Hauptpunkt)
        Startwert(6, 1) = 533.0#      'y0 (Hauptpunkt)
        'Startwert(7, 1) = 0#        'K3 (Verzeichnung)
        'Startwert(8, 1) = 0#        'K5 (Verzeichnung)
        'Startwert(9, 1) = 0#        'K7 (Verzeichnung)
        rot = rotWrobel(Startwert(1, 1), Startwert(2, 1), Startwert(3, 1))

        For iIteration = 1 To 100

            ' Designmatrix aufstellen:
            Call MInitialize(ATA, 0.0#)
            Call MInitialize(ATL, 0.0#)
            Call MInitialize(Design, 0.0#)
            ck = Startwert(4, 1)
            x0 = Startwert(5, 1)
            y0 = Startwert(6, 1)

            For i0 = 1 To nEq
                sqrW = Sqrt(Weight(i0, 1))
                XminusX0 = XObj(i0, 1)
                YminusY0 = YObj(i0, 1)
                ZminusZ0 = -Sqrt(1.0# - XminusX0 ^ 2 - YminusY0 ^ 2) ' Neg. Vorzeichen!
                sX = rot(1, 1) * XminusX0 + rot(2, 1) * YminusY0 + rot(3, 1) * ZminusZ0
                sY = rot(1, 2) * XminusX0 + rot(2, 2) * YminusY0 + rot(3, 2) * ZminusZ0
                sN = rot(1, 3) * XminusX0 + rot(2, 3) * YminusY0 + rot(3, 3) * ZminusZ0
                'radius = Sqr(XObj(i0, 1) ^ 2 + YObj(i0, 1) ^ 2)

                Design(1, 1) = -ck * (sX * sY) / (sN * sN) * sqrW     ' df/dOmega
                Design(1, 2) = ck * ((sX / sN) ^ 2 + 1.0#) * sqrW       ' df/dPhi
                Design(1, 3) = -ck * (sY / sN) * sqrW                 ' df/dKappa
                Design(1, 4) = -sX / sN * sqrW                        ' df/dck
                'Design(1, 5) = 1# * sqrW                              ' df/dx0 = fix!
                'Design(1, 6) = 0#                                     ' df/dy0 = fix!
                Absolut = -ck * sX / sN + x0 ' Verzeichnung???
                Beob(1, 1) = (xb(i0, 1) - Absolut) * sqrW

                'With Application.WorksheetFunction
                '     ATA = MAdd(ATA, .MMult(.Transpose(Design), Design))
                '     ATL = MAdd(ATL, .MMult(.Transpose(Design), Beob))
                'End With
                Call AddObservationEquations(ATA, Design, Design)
                Call AddObservationEquations(ATL, Design, Beob)

                Design(1, 1) = -ck * ((sY / sN) ^ 2 + 1.0#) * sqrW      ' dg/dOmega
                Design(1, 2) = ck * (sX * sY / (sN ^ 2)) * sqrW       ' dg/dPhi
                Design(1, 3) = ck * (sX / sN) * sqrW                  ' dg/dKappa
                Design(1, 4) = -sY / sN * sqrW                        ' dg/dck
                'Design(1, 5) = 0#                                     ' dg/dx0 = fix!
                'Design(1, 6) = 1# * sqrW                              ' dg/dy0 = fix!
                Absolut = -ck * sY / sN + y0 ' Verzeichnung???
                Beob(1, 1) = (yb(i0, 1) - Absolut) * sqrW
                'With Application.WorksheetFunction
                '     ATA = MAdd(ATA, .MMult(.Transpose(Design), Design))
                '     ATL = MAdd(ATL, .MMult(.Transpose(Design), Beob))
                'End With
                Call AddObservationEquations(ATA, Design, Design)
                Call AddObservationEquations(ATL, Design, Beob)
            Next i0

            ' x0 festhalten:
            Call MInitialize(Design, 0.0#)
            Design(1, 5) = 1.0#
            Beob(1, 1) = 0.0#
            'With Application.WorksheetFunction
            '     ATA = MAdd(ATA, .MMult(.Transpose(Design), Design))
            '     ATL = MAdd(ATL, .MMult(.Transpose(Design), Beob))
            'End With
            Call AddObservationEquations(ATA, Design, Design)
            Call AddObservationEquations(ATL, Design, Beob)

            ' y0 festhalten:
            Call MInitialize(Design, 0.0#)
            Design(1, 6) = 1.0#
            Beob(1, 1) = 0.0#
            'With Application.WorksheetFunction
            '     ATA = MAdd(ATA, .MMult(.Transpose(Design), Design))
            '     ATL = MAdd(ATL, .MMult(.Transpose(Design), Beob))
            'End With
            Call AddObservationEquations(ATA, Design, Design)
            Call AddObservationEquations(ATL, Design, Beob)

            ' Normalgleichungssystem loesen:
            'ATA = Application.WorksheetFunction.MInverse(ATA)
            'Solution = Application.WorksheetFunction.MMult(ATA, ATL)
            For i1 = 1 To UBound(ATL, 1)
                Solution(i1, 1) = ATL(i1, 1)
            Next i1
            Call CholeskySolver(ATA, Solution) ' overwrites ATA, Solution

            If (iIteration = -1 Or iIteration = -100) Then _
            MsgBox("Loesung:" & vbCrLf _
                   & Solution(1, 1) & vbCrLf & Solution(2, 1) & vbCrLf _
                   & Solution(3, 1) & vbCrLf & Solution(4, 1) & vbCrLf _
                   & Solution(5, 1) & vbCrLf & Solution(6, 1))

            'dr-Matrix rechnen und r mit dr multiplizieren
            deltaRot = rotWrobel(Solution(1, 1), Solution(2, 1), Solution(3, 1))
            For i1 = 1 To 3
                For i2 = 1 To 3
                    tempRot(i1, i2) = rot(i1, i2)
                Next
            Next
            'rot = Application.WorksheetFunction.MMult(deltaRot, tempRot)
            Call MatMult(deltaRot, tempRot, rot)

            ' Update; Startwerte der differentiellen Rotationsmatrix bleiben auf Null!
            For i1 = 4 To nu
                Startwert(i1, 1) = Startwert(i1, 1) + Solution(i1, 1)
                If (iIteration < 100) Then Exit For 'mstmp!!!
            Next i1

            If (iIteration = -1) Then _
            MsgBox("ROT:" & vbCrLf _
                   & rot(1, 1) & " " & rot(1, 2) & " " & rot(1, 3) & vbCrLf _
                   & rot(2, 1) & " " & rot(2, 2) & " " & rot(2, 3) & vbCrLf _
                   & rot(3, 1) & " " & rot(3, 2) & " " & rot(3, 3))

            'Abbruchkriterium pruefen:
            maxSolution = 0.0#
            For i1 = 1 To nu
                If (Abs(Solution(i1, 1)) > maxSolution) Then maxSolution = Abs(Solution(i1, 1))
            Next
            If (maxSolution < 0.00000001) Then
                'MsgBox "Anzahl Iterationen: " & iIteration, vbInformation, "Success!"
                Exit For
            End If
            If (iIteration = 1000) Then
                'MsgBox "Anzahl Iterationen: " & iIteration, vbCritical, "No Convergence!"
                Exit For
            End If

        Next iIteration

        Solution(1, 1) = Atan(-rot(2, 3) / rot(3, 3))
        'Solution(2, 1) = Atn(rot(1, 3) / Sqr(-rot(1, 3) * rot(1, 3) + 1)) 'arcsin(rot(1, 3))
        Solution(2, 1) = Asin(rot(1, 3))
        Solution(3, 1) = Atan(-rot(1, 2) / rot(1, 1))
        For i0 = 4 To nu
            Solution(i0, 1) = Startwert(i0, 1)
        Next i0

        TransformationEB = Solution

    End Function

    'Rotationsmatrix aus Naeherungswerten der Drehwinkel bestimmen;
    'Drehungen um mitbewegte Achsen (nach Photo III, 2/53)
    Function rotWrobel(ByVal omega, ByVal phi, ByVal kappa) As Double(,)

        Dim rot(,) As Double
        ReDim rot(3, 3)

        rot(1, 1) = Cos(phi) * Cos(kappa)
        rot(1, 2) = -Cos(phi) * Sin(kappa)
        rot(1, 3) = Sin(phi)
        rot(2, 1) = Cos(omega) * Sin(kappa) _
                  + Sin(omega) * Sin(phi) * Cos(kappa)
        rot(2, 2) = Cos(omega) * Cos(kappa) _
                  - Sin(omega) * Sin(phi) * Sin(kappa)
        rot(2, 3) = -Sin(omega) * Cos(phi)
        rot(3, 1) = Sin(omega) * Sin(kappa) _
                  - Cos(omega) * Sin(phi) * Cos(kappa)
        rot(3, 2) = Sin(omega) * Cos(kappa) _
                  + Cos(omega) * Sin(phi) * Sin(kappa)
        rot(3, 3) = Cos(omega) * Cos(phi)

        rotWrobel = rot
    End Function

    Function XTransformationEB(ByVal XAlt As Double, ByVal YAlt As Double, ByVal Parameter As Object) As Double

        Dim rot
        ReDim rot(3, 3)
        Dim xVek
        ReDim xVek(3, 1)
        Dim objVek
        ReDim objVek(3, 1)
        Dim Norm As Double

        rot = rotWrobel(Parameter(1, 1), Parameter(2, 1), Parameter(3, 1))

        xVek(1, 1) = XAlt - Parameter(5, 1)
        xVek(2, 1) = YAlt - Parameter(6, 1)
        xVek(3, 1) = -Parameter(4, 1)
        'objVek = Application.WorksheetFunction.MMult(rot, xVek)
        Call MatMult(rot, xVek, objVek)
        Norm = Sqrt(objVek(1, 1) ^ 2 + objVek(2, 1) ^ 2 + objVek(3, 1) ^ 2)

        XTransformationEB = objVek(1, 1) / Norm

    End Function

    Function YTransformationEB(ByVal XAlt As Double, ByVal YAlt As Double, ByVal Parameter As Object) As Double

        Dim rot
        ReDim rot(3, 3)
        Dim xVek
        ReDim xVek(3, 1)
        Dim objVek
        ReDim objVek(3, 1)
        Dim Norm As Double

        rot = rotWrobel(Parameter(1, 1), Parameter(2, 1), Parameter(3, 1))

        xVek(1, 1) = XAlt - Parameter(5, 1)
        xVek(2, 1) = YAlt - Parameter(6, 1)
        xVek(3, 1) = -Parameter(4, 1)
        'objVek = Application.WorksheetFunction.MMult(rot, xVek)
        Call MatMult(rot, xVek, objVek)
        Norm = Sqrt(objVek(1, 1) ^ 2 + objVek(2, 1) ^ 2 + objVek(3, 1) ^ 2)

        YTransformationEB = objVek(2, 1) / Norm

    End Function

    Sub MInitialize(ByRef Matrix(,) As Double, ByVal MyValue As Double)

        ' Excel error codes:
        '
        ' error       error       cell error         cell error
        ' constant:   number:     value (german):    value (english):
        ' xlErrNull   2000        #NULL!             #NULL!
        ' xlErrDiv0   2007        #DIV/0!            #DIV/0!
        ' xlErrValue  2015        #WERT!             #VALUE!
        ' xlErrRef    2023        #BEZUG!            #REF!
        ' xlErrName   2029        #NAME?             #NAME?
        ' xlErrNum    2036        #ZAHL!             #NUM!
        ' xlErrNA     2042        #NV                #N/A
        '
        ' example: matrix(i, i2) = CVErr(xlErrNum)

        Dim i1 As Long, i2 As Long

        For i1 = 1 To UBound(Matrix, 1)
            For i2 = 1 To UBound(Matrix, 2)
                Matrix(i1, i2) = MyValue
            Next i2
        Next i1

    End Sub
End Module
