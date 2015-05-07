Imports System.Math


Module RotBySchutSubs
    ' Copyright 2007 Prof. Dr.-Ing. Martin Schlueter, FH Mainz, schlueter@fh-mainz.de
    Public Function MS_RotBySchut(ByVal vR1O(,) As Double, ByVal vR2O(,) As Double, ByVal vR1N(,) As Double, ByVal vR2N(,) As Double) As Double(,)
        ' callable from both Excel and VBA:
        '    vR10(1 To 3, 1) as Double
        '    vR10(1 To 3, 1) as Variant
        '    vR10(1 To 3, 1) as Range

        Dim RotStart(3, 3) As Double
        Dim Eqs(3, 3) As Double
        Dim Rhs(3, 1) As Double

        Dim R1O(3) As Double, R2O(3) As Double
        Dim R1N(3) As Double, R2N(3) As Double
        Dim Coeff(8, 4) As Double

        Dim Sol(4) As Double
        Dim Determinante As Double, DetMax As Double, DetFlag As Boolean
        Dim i1 As Long, i2 As Long, i3 As Long, i0 As Long
        Dim m1 As Long, m2 As Long, m3 As Long, m0 As Long

        ' put Excel-Ranges to (Double) vectors:
        For i0 = 1 To 3
            R1O(i0) = vR1O(i0, 1)
            R2O(i0) = vR2O(i0, 1)
            R1N(i0) = vR1N(i0, 1)
            R2N(i0) = vR2N(i0, 1)
        Next

        ' put Excel-Ranges to (Variant) vectors:
        '    Call LoadVector(R1O, vR1O, i0)
        '    Call LoadVector(R2O, vR2O, i0)
        '    Call LoadVector(R1N, vR1N, i0)
        '    Call LoadVector(R2N, vR2N, i0)

        Call BuildSchutCoeff(Coeff, R1O, R2O, R1N, R2N)

        ' try 48*4=192 possibilities:
        DetMax = 0.0#
        DetFlag = False
        For i1 = 1 To 4
            If (DetFlag) Then Exit For
            For i2 = i1 + 1 To 7
                If (DetFlag) Then Exit For
                For i3 = Max(5, i2 + 1) To 8
                    If (DetFlag) Then Exit For
                    For i0 = 1 To 4 'which parameter equal to one?
                        If (DetFlag) Then Exit For
                        Eqs = BuildSchutEqs(i1, i2, i3, i0, Coeff)
                        Determinante = Abs( _
                            Eqs(1, 1) * (Eqs(2, 2) * Eqs(3, 3) - Eqs(2, 3) * Eqs(3, 2)) _
                          + Eqs(1, 2) * (Eqs(2, 3) * Eqs(3, 1) - Eqs(2, 1) * Eqs(3, 3)) _
                          + Eqs(1, 3) * (Eqs(2, 1) * Eqs(3, 2) - Eqs(2, 2) * Eqs(3, 1)))
                        'Determinante = Abs(Application.WorksheetFunction.MDeterm(Eqs))

                        If (Determinante > DetMax) Then
                            DetMax = Determinante
                            m1 = i1
                            m2 = i2
                            m3 = i3
                            m0 = i0
                            If (DetMax > 1.0#) Then DetFlag = True ' skip other possiblities
                        End If
                    Next
                Next
            Next
        Next

        Eqs = BuildSchutEqs(m1, m2, m3, m0, Coeff)
        Rhs = BuildSchutRhs(m1, m2, m3, m0, Coeff)
        'Eqs = Application.WorksheetFunction.MInverse(Eqs)
        'Rhs = Application.WorksheetFunction.MMult(Eqs, Rhs)
        Call GaussSolver(Eqs, Rhs) ' overwrites Eqs and puts solution to Rhs
        Call BuildSchutSol(Sol, m0, Rhs)
        RotStart = BuildSchutRot(m1, m2, m3, m0, Sol)

        MS_RotBySchut = RotStart

    End Function

    Private Sub BuildSchutCoeff(ByVal Coeff(,) As Double, ByVal R1O() As Double, ByVal R2O() As Double, ByVal R1N() As Double, ByVal R2N() As Double)

        Coeff(1, 1) = 0.0#
        Coeff(1, 2) = -R1N(3) - R1O(3)
        Coeff(1, 3) = R1N(2) + R1O(2)
        Coeff(1, 4) = R1N(1) - R1O(1)
        Coeff(2, 1) = R1N(3) + R1O(3)
        Coeff(2, 2) = 0.0#
        Coeff(2, 3) = -R1N(1) - R1O(1)
        Coeff(2, 4) = R1N(2) - R1O(2)
        Coeff(3, 1) = -R1N(2) - R1O(2)
        Coeff(3, 2) = R1N(1) + R1O(1)
        Coeff(3, 3) = 0.0#
        Coeff(3, 4) = R1N(3) - R1O(3)
        Coeff(4, 1) = R1N(1) - R1O(1)
        Coeff(4, 2) = R1N(2) - R1O(2)
        Coeff(4, 3) = R1N(3) - R1O(3)
        Coeff(4, 4) = 0.0#
        Coeff(5, 1) = 0.0#
        Coeff(5, 2) = -R2N(3) - R2O(3)
        Coeff(5, 3) = R2N(2) + R2O(2)
        Coeff(5, 4) = R2N(1) - R2O(1)
        Coeff(6, 1) = R2N(3) + R2O(3)
        Coeff(6, 2) = 0.0#
        Coeff(6, 3) = -R2N(1) - R2O(1)
        Coeff(6, 4) = R2N(2) - R2O(2)
        Coeff(7, 1) = -R2N(2) - R2O(2)
        Coeff(7, 2) = R2N(1) + R2O(1)
        Coeff(7, 3) = 0.0#
        Coeff(7, 4) = R2N(3) - R2O(3)
        Coeff(8, 1) = R2N(1) - R2O(1)
        Coeff(8, 2) = R2N(2) - R2O(2)
        Coeff(8, 3) = R2N(3) - R2O(3)
        Coeff(8, 4) = 0.0#

    End Sub

    Private Function BuildSchutEqs(ByVal i1 As Long, ByVal i2 As Long, ByVal i3 As Long, ByVal i4 As Long, ByVal Coeff(,) As Double) As Double(,)

        Dim Eqs(3, 3) As Double

        Dim j1 As Long, j2 As Long, j3 As Long

        If (i4 = 1) Then
            j1 = 2
            j2 = 3
            j3 = 4
        ElseIf (i4 = 2) Then
            j1 = 1
            j2 = 3
            j3 = 4
        ElseIf (i4 = 3) Then
            j1 = 1
            j2 = 2
            j3 = 4
        Else
            j1 = 1
            j2 = 2
            j3 = 3
        End If
        Eqs(1, 1) = Coeff(i1, j1)
        Eqs(1, 2) = Coeff(i1, j2)
        Eqs(1, 3) = Coeff(i1, j3)
        Eqs(2, 1) = Coeff(i2, j1)
        Eqs(2, 2) = Coeff(i2, j2)
        Eqs(2, 3) = Coeff(i2, j3)
        Eqs(3, 1) = Coeff(i3, j1)
        Eqs(3, 2) = Coeff(i3, j2)
        Eqs(3, 3) = Coeff(i3, j3)

        BuildSchutEqs = Eqs

    End Function

    Private Function BuildSchutRhs(ByVal i1 As Long, ByVal i2 As Long, ByVal i3 As Long, ByVal i4 As Long, ByVal Coeff(,) As Double) As Double(,)

        Dim Rhs(3, 1) As Double

        Rhs(1, 1) = (-1.0#) * Coeff(i1, i4)
        Rhs(2, 1) = (-1.0#) * Coeff(i2, i4)
        Rhs(3, 1) = (-1.0#) * Coeff(i3, i4)

        BuildSchutRhs = Rhs

    End Function

    Private Sub BuildSchutSol(ByVal Sol() As Double, ByVal i4 As Long, ByVal Rhs(,) As Double)

        Dim i, j1 As Long, j2 As Long, j3 As Long

        For i = 1 To 4
            Sol(i) = 1.0#
        Next

        If (i4 = 1) Then
            j1 = 2
            j2 = 3
            j3 = 4
        ElseIf (i4 = 2) Then
            j1 = 1
            j2 = 3
            j3 = 4
        ElseIf (i4 = 3) Then
            j1 = 1
            j2 = 2
            j3 = 4
        Else
            j1 = 1
            j2 = 2
            j3 = 3
        End If
        Sol(j1) = Rhs(1, 1)
        Sol(j2) = Rhs(2, 1)
        Sol(j3) = Rhs(3, 1)

    End Sub

    Private Function BuildSchutRot(ByVal i1 As Long, ByVal i2 As Long, ByVal i3 As Long, ByVal i4 As Long, ByVal Sol() As Double) As Double(,)

        Dim rot(3, 3) As Double

        'Dim i, j1 As Long, j2 As Long, j3 As Long
        Dim factor As Double

        factor = 0.0#
        factor = factor + Sol(1) * Sol(1)
        factor = factor + Sol(2) * Sol(2)
        factor = factor + Sol(3) * Sol(3)
        factor = factor + Sol(4) * Sol(4)
        factor = 1.0# / factor

        rot(1, 1) = factor * (Sol(4) * Sol(4) + Sol(1) * Sol(1) - Sol(2) * Sol(2) - Sol(3) * Sol(3))
        rot(1, 2) = factor * 2.0# * (Sol(1) * Sol(2) - Sol(3) * Sol(4))
        rot(1, 3) = factor * 2.0# * (Sol(1) * Sol(3) + Sol(2) * Sol(4))

        rot(2, 1) = factor * 2.0# * (Sol(1) * Sol(2) + Sol(3) * Sol(4))
        rot(2, 2) = factor * (Sol(4) * Sol(4) - Sol(1) * Sol(1) + Sol(2) * Sol(2) - Sol(3) * Sol(3))
        rot(2, 3) = factor * 2.0# * (Sol(2) * Sol(3) - Sol(1) * Sol(4))

        rot(3, 1) = factor * 2.0# * (Sol(1) * Sol(3) - Sol(2) * Sol(4))
        rot(3, 2) = factor * 2.0# * (Sol(2) * Sol(3) + Sol(1) * Sol(4))
        rot(3, 3) = factor * (Sol(4) * Sol(4) - Sol(1) * Sol(1) - Sol(2) * Sol(2) + Sol(3) * Sol(3))

        BuildSchutRot = rot

    End Function

End Module
