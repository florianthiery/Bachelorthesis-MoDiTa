Module modBasicMatrixCalculations
    ' Nicht betroffenen Module, temporaer entfernt:
    '   TMByHess
    '   CFormChanger
    '   theoErrCodes

    ' Folgende Module wurden angepasst:
    '   modAnglesVectors
    '   modEinzelbildOri
    '   modTheodolite
    '   modTransformation2D
    '   RotByLSQSubs
    '   RotBySchutSubs

    ' Folgende Module koennen ab sofort entfallen:
    '   modToVector

    ' =========================================
    ' NEU via Modul modBasicMatrixCalculations:

    ' Sub AddObservationEquations(ATL, A, L)
    ' Sub AddWeightedObservationEquations(ATPL, A, P, L)
    ' dafuer entfallen in RotByLSQSubs:
    ' Function MS_ATPA, Function MS_ATPL, Sub NormalEquationBuild

    ' alt: Determinante = Abs(Application.WorksheetFunction.MDeterm(Eqs))
    ' neu: fuer 3x3 DURCH DIREKTE BERECHNUNG nach Cramer ersetzt

    ' alt: rot = Application.WorksheetFunction.Transpose(rot)
    ' neu: call MatNxNT(rot)
    ' NUR FUER QUADRATISCHE MATRIZEN (wird auch NUR so benoetigt!)

    ' alt: myRtgNeu = Application.WorksheetFunction.MMult(rot, myRtgNeu)
    ' neu: Call MatMult(rot, myRtgNeu, myRtgNeu)
    ' UNFERTIG?! Fuer den Fall Eingabematrix = Ausgabematrix wird in matmult zur Zeit noch umkopiert

    ' alt: Eqs = Application.WorksheetFunction.MInverse(Eqs) UND
    ' alt: Rhs = Application.WorksheetFunction.MMult(Eqs, Rhs)
    ' neu: Call GaussSolver(Eqs, Rhs) FUER ASSYMMETRISCHE SYSTEME

    ' alt: ATA = Application.WorksheetFunction.MInverse(ATA) UND
    ' alt: Solution = Application.WorksheetFunction.MMult(ATA, ATL)
    ' neu: Zuerst Solution = ATL umkopieren, dann:
    ' neu: Call CholeskySolver(ATA, Solution) FUER NGL-SYSTEME

    ' Die Application.WorksheetFunction (s)
    '   min(von zwei Zahlen),
    '   max(von zwei Zahlen),
    '   asin
    '   ln (in VB.NET = "log")
    '   pi
    '   atan2
    '   sind in System.Math unter VB.NET verfuegbar und wurden nicht umgestellt

    ' ===
    ' To Do in VB.NET:
    '   - Ueberfluessige Felder (Range UND NOCH Variant Array) eliminieren
    '   - Alle Arrays in Double statt Variant
    '   - (nicht wichtig: zur Zeit werden noch komplette NG-Systeme gespeichert, fuer Cholesky reicht
    '      allein die obere Dreiecksmatrix aus)

    ' ===
    ' Uebergabe von Feldern in VB.NET: "Einstieg in Visual Basic 2008", Thomas Theis, Kap. 4.6.6

    ' Hilfsfunktionen fuer die Nutzung vom Tabellenblatt aus

    'Function MyGaussSolver(ByVal rA, ByVal rB)
    '    Dim A, B
    '    A = rA
    '    B = rB
    '    Call GaussSolver(A, B)
    '    MyGaussSolver = B
    'End Function

    'Function MyGaussInverse(ByVal rA)
    '    Dim A
    '    A = rA
    '    Call GaussInverse(A)
    '    MyGaussInverse = A
    'End Function

    'Function MyCholeskySolver(ByVal rA, ByVal rB)
    '    Dim A, B
    '    A = rA
    '    B = rB
    '    Call CholeskySolver(A, B)
    '    MyCholeskySolver = B
    'End Function

    'Function MyCholeskyInverse(ByVal rA)
    '    Dim A
    '    A = rA
    '    Call CholeskyInverse(A)
    '    MyCholeskyInverse = A
    'End Function

    'Function MyMatMult(ByVal rA, ByVal rB)
    '    Dim A, B, C
    '    Dim n1 As Long, n2 As Long
    '    A = rA
    '    B = rB
    '    n1 = UBound(A, 1)
    '    n2 = UBound(B, 2)
    'ReDim C(1 To n1, 1 To n2)

    '    Call MatMult(A, B, C)
    '    MyMatMult = C
    'End Function

    'Function MyMatNxNT(ByVal rA)
    '    Dim A
    '    A = rA
    '    Call MatNxNT(A)
    '    MyMatNxNT = A
    'End Function



    ' VBA - Funktionen

    ' Skalarprodukt fuer zwei zeilenweise Vektoren
    Function SkalarProd(ByVal A, ByVal B)

        Dim n1 As Long
        Dim i1 As Long
        Dim sum As Double
        n1 = UBound(A, 1)

        sum = 0.0#
        For i1 = 1 To n1
            sum = sum + A(i1, 1) * B(i1, 1)
        Next
        SkalarProd = sum
    End Function


    ' Matrizenmultiplikation C = A * B
    Sub MatMult(ByVal A, ByVal B, ByVal C)

        Dim n1 As Long, n2 As Long, n3 As Long
        Dim i1 As Long, i2 As Long, i3 As Long
        n1 = UBound(A, 1)
        n2 = UBound(B, 2)
        n3 = UBound(A, 2) ' = UBound(B, 1)

        Dim D(n1, n2)

        For i1 = 1 To n1
            For i2 = 1 To n2
                D(i1, i2) = 0.0#
                For i3 = 1 To n3
                    D(i1, i2) = D(i1, i2) + A(i1, i3) * B(i3, i2)
                Next i3
            Next i2
        Next i1

        ' Umkopieren
        ' Warum so kompliziert? Zur Zeit wird of noch so aufgerufen: call MatMult(Rot, Vek, Vek)
        ' Noch unklar was passiert, wenn Vek in der Sub veraendert wird ...
        For i1 = 1 To n1
            For i2 = 1 To n2
                C(i1, i2) = D(i1, i2)
            Next i2
        Next i1
    End Sub

    ' Transponiere (und ueberschreibe) eine quadratische Matrix
    Sub MatNxNT(ByVal A)

        Dim n As Long, m As Long
        Dim i1 As Long, i2 As Long
        Dim keep As Double

        n = UBound(A, 1) 'number of rows

        ' verify whether n = m:
        m = UBound(A, 2) 'number of unknows
        If (n <> m Or n < 1) Then MsgBox("Error in MatNxNT")

        ' Die Hauptdiagonalelemente werden NICHT angefasst:
        For i1 = 1 To n - 1
            For i2 = i1 + 1 To n
                keep = A(i1, i2)
                A(i1, i2) = A(i2, i1)
                A(i2, i1) = keep
            Next i2
        Next i1

    End Sub

    ' NGL-Systeme aufaddieren
    ' (funktioniert fuer komplette A-Matrizen oder auch fuer einzelne Verbesserungsgleichungen)

    ' Verwendung: ATPA und APTL mit 0 initialisieren, dann
    ' call AddObservationEquations(ATPA, A, A)
    ' call AddObservationEquations(ATPL, A, L)
    ' keine Gewichte, P = 1
    Sub AddObservationEquations(ByVal ATL, ByVal A, ByVal L)

        Dim nl As Long, nc As Long, nu As Long
        Dim i1 As Long, i2 As Long, i3 As Long
        nu = UBound(ATL, 1)
        nc = UBound(L, 2)
        nl = UBound(A, 1)

        For i1 = 1 To nu
            For i2 = 1 To nc
                For i3 = 1 To nl
                    ATL(i1, i2) = ATL(i1, i2) + A(i3, i1) * L(i3, i2)
                Next i3
            Next i2
        Next i1
    End Sub

    ' Verwendung: ATPA und APTL mit 0 initialisieren, dann
    ' call AddWeightedObservationEquations(ATPA, A, P, A)
    ' call AddWeightedObservationEquations(ATPL, A, P, L)
    ' P(1 to nl, 1 to 1) ist ein Vektor (nur die Hauptdiagonalelemente der Gewichtsmatrix)
    Sub AddWeightedObservationEquations(ByVal ATPL, ByVal A, ByVal P, ByVal L)

        Dim nl As Long, nc As Long, nu As Long
        Dim i1 As Long, i2 As Long, i3 As Long
        nu = UBound(ATPL, 1)
        nc = UBound(L, 2)
        nl = UBound(A, 1)

        For i1 = 1 To nu
            For i2 = 1 To nc
                For i3 = 1 To nl
                    ATPL(i1, i2) = ATPL(i1, i2) + A(i3, i1) * P(i3, 1) * L(i3, i2)
                Next i3
            Next i2
        Next i1
    End Sub

    ' Loesung eines linearen Gleichungssystems nach Gauss mit Pivotisierung
    ' nach Stoecker, TB math. Formeln, 9.6
    Sub GaussSolver(ByVal A, ByVal B)

        Dim n As Long, m As Long, i1 As Long, i2 As Long, i3 As Long, i4 As Long
        Dim factor As Double, summe As Double
        'Dim sbefore As String
        Dim pivot As Long, maxA As Double, dummy As Double
        'Dim X

        n = UBound(A, 1) 'number of unknows
        m = UBound(B, 2) 'number of right hand sides
        'ReDim X(1 To n, 1 To m)

        ' forward elimination
        For i1 = 1 To n - 1

            ' find pivot, change rows
            pivot = i1
            maxA = Math.Abs(A(i1, i1))
            For i2 = i1 + 1 To n
                dummy = Math.Abs(A(i2, i1))
                If (dummy > maxA) Then
                    maxA = dummy
                    pivot = i2
                End If
            Next
            If (pivot <> i1) Then
                For i3 = 1 To n ' Fehler bei Stoecker!
                    dummy = A(pivot, i3)
                    A(pivot, i3) = A(i1, i3)
                    A(i1, i3) = dummy
                Next i3
                For i4 = 1 To m
                    dummy = B(pivot, i4)
                    B(pivot, i4) = B(i1, i4)
                    B(i1, i4) = dummy
                Next i4
            End If

            For i2 = i1 + 1 To n
                factor = A(i2, i1) / A(i1, i1)
                For i3 = i1 + 1 To n
                    A(i2, i3) = A(i2, i3) - factor * A(i1, i3)
                Next i3
                For i4 = 1 To m
                    B(i2, i4) = B(i2, i4) - factor * B(i1, i4)
                Next i4
            Next i2
        Next i1

        'back substitution
        For i4 = 1 To m
            'X(n, i4) = B(n, i4) / A(n, n)
            B(n, i4) = B(n, i4) / A(n, n)
            For i2 = n - 1 To 1 Step -1
                summe = 0.0#
                For i3 = i2 + 1 To n
                    'summe = summe + A(i2, i3) * X(i3, i4)
                    summe = summe + A(i2, i3) * B(i3, i4)
                Next i3
                'X(i2, i4) = (B(i2, i4) - summe) / A(i2, i2)
                B(i2, i4) = (B(i2, i4) - summe) / A(i2, i2)
            Next i2
        Next i4

        'B = X

    End Sub

    ' Matrizeninversion nach Gauss mit Pivotisierung
    Sub GaussInverse(ByVal A)

        Dim n As Long, i1 As Long, i2 As Long

        n = UBound(A, 1) 'number of unknows
        Dim B(n, n)

        For i1 = 1 To n
            For i2 = 1 To n
                If (i1 <> i2) Then
                    B(i1, i2) = 0.0#
                Else
                    B(i1, i2) = 1.0#
                End If
            Next
        Next

        Call GaussSolver(A, B)

        A = B

    End Sub

    ' Loesung eines symmetrischen linearen Gleichungssystems nach Cholesky

    'Sub CholeskySolver(A() As Double, B() As Double)
    Sub CholeskySolver(ByVal A, ByVal B)

        Dim n As Long, m As Long, i1 As Long, i2 As Long, i3 As Long
        Dim summe As Double

        n = UBound(A, 1) 'number of unknows
        m = UBound(B, 2) 'number of right hand sides

        On Error GoTo Errorhandler

        'Build upper right triangular matrix R
        'from upper right triangular matrix A, overwrite A:
        For i1 = 1 To n
            For i2 = i1 To n
                summe = 0
                If (i1 = i2) Then
                    For i3 = 1 To i1 - 1
                        summe = summe + A(i3, i1) ^ 2
                    Next
                    summe = A(i1, i2) - summe
                    A(i1, i2) = Math.Sqrt(summe)
                Else
                    For i3 = 1 To i1 - 1
                        summe = summe + A(i3, i1) * A(i3, i2)
                    Next
                    A(i1, i2) = (A(i1, i2) - summe) / A(i1, i1)
                End If
            Next i2
        Next i1

        'Build vector C, overwrite B:
        For i3 = 1 To m
            For i1 = 1 To n
                summe = 0.0#
                For i2 = 1 To i1 - 1
                    summe = summe + A(i2, i1) * B(i2, i3)
                Next
                B(i1, i3) = (B(i1, i3) - summe) / A(i1, i1)
            Next
        Next

        'Build solution, overwrite C:
        For i3 = 1 To m
            For i1 = n To 1 Step -1
                summe = 0.0#
                For i2 = i1 + 1 To n
                    summe = summe + A(i1, i2) * B(i2, i3)
                Next
                B(i1, i3) = (B(i1, i3) - summe) / A(i1, i1)
            Next
        Next

        Exit Sub

Errorhandler:

        MsgBox("Fehler, aber keine Fehlerbehandlung!")
        'Call InitializeMatrix(B, CVErr(xlErrNum))

    End Sub

    ' Inversion einer symmetrischen Matrix nach Cholesky

    'Sub CholeskyInverse(A() As Double)
    Sub CholeskyInverse(ByVal A)

        Dim n As Long, i1 As Long, i2 As Long, i3 As Long
        Dim C(n, 1) As Double, summe As Double

        n = UBound(A, 1)
        'ReDim C(n, 1) ' keep copy of main diagonal

        On Error GoTo Errorhandler

        'Build upper right triangular matrix R
        'from upper right triangular matrix A, overwrite A:
        For i1 = 1 To n
            For i2 = i1 To n
                summe = 0
                If (i1 = i2) Then
                    For i3 = 1 To i1 - 1
                        summe = summe + A(i3, i1) ^ 2
                    Next
                    summe = A(i1, i2) - summe
                    'If IsError(Sqr(Summe)) Then
                    '   Call InitializeMatrix(A, CVErr(xlErrNum))
                    '   Exit Sub
                    'End If
                    A(i1, i2) = Math.Sqrt(summe)
                Else
                    For i3 = 1 To i1 - 1
                        summe = summe + A(i3, i1) * A(i3, i2)
                    Next
                    A(i1, i2) = (A(i1, i2) - summe) / A(i1, i1)
                End If
            Next i2
        Next i1

        'Build upper right matric C, store C in lower left of A,
        'keep a copy of main diagonal in vector C()
        For i1 = 1 To n
            C(i1, 1) = A(i1, i1) ' keep copy of main diagonal
            For i2 = 1 To i1
                '           B(i2, i1) = 1 / A(i1, i1)
                A(i1, i2) = 1 / A(i1, i1)
                If (i1 <> i2) Then
                    summe = 0.0#
                    For i3 = i2 To i1 - 1
                        '                  Summe = Summe - A(i3, i1) * B(i2, i3)
                        summe = summe - A(i3, i1) * A(i3, i2)
                    Next
                    '              B(i2, i1) = B(i2, i1) * Summe
                    A(i1, i2) = A(i1, i2) * summe
                End If
            Next i2
        Next i1

        'Build inverse matrix and store in lower left of A:
        For i1 = n To 1 Step -1
            For i2 = i1 To 1 Step -1
                '           Summe = B(i2, i1)
                summe = A(i1, i2)
                For i3 = i1 + 1 To n
                    '                Summe = Summe - A(i1, i3) * B(i2, i3)
                    summe = summe - A(i1, i3) * A(i3, i2)
                Next
                '           B(i2, i1) = Summe / A(i1, i1)
                A(i1, i2) = summe / C(i1, 1)
            Next i2
        Next i1

        'Copy to get full (symmetric) inverse matrix (not really necessary):
        For i1 = 1 To n
            For i2 = i1 + 1 To n
                A(i1, i2) = A(i2, i1)
            Next i2
        Next i1

        Exit Sub

Errorhandler:

        MsgBox("Fehler, aber keine Fehlerbehandlung!")
        'Call InitializeMatrix(a, CVErr(xlErrNum))

    End Sub

End Module
