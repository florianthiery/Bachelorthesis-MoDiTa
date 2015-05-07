Imports HalconDotNet
''' <summary>
''' Klasse für IDS µEye Kamera. Getestet mit µEye LE USB
''' Version: 1.0, März 2010
''' Fachhochschule Mainz, i3mainz, Hauth
''' </summary>
''' <remarks></remarks>
Public Class CamerauEye
    Private _framegrabber As HFramegrabber
    Private _Controller As ControluEye
    Private _CameraConnected As Boolean = False

    Public ReadOnly Property IsCameraConnected() As Boolean
        Get
            Return Me._CameraConnected
        End Get
    End Property

    Public ReadOnly Property framegrabber() As HFramegrabber
        Get
            Return Me._framegrabber
        End Get
    End Property

    Public Sub connectCamera(ByVal control As ControluEye)
        Me._framegrabber = New HFramegrabber("uEye", 1, 1, 0, 0, 0, 0, "default", 8, "default", -1, "false", _
                                         "default", "1", 0, -1)
        Me._Controller = control
        Me._CameraConnected = True
    End Sub

    Public Sub disconnectCamera()
        Me._framegrabber.Dispose()
        Me._Controller.cameraIsDisconnected()
        Me._CameraConnected = False
    End Sub

    Public Sub getFramegrabber(ByRef fg As HFramegrabber)
        fg = Me._framegrabber
    End Sub

    ''' <summary>
    ''' Legt die Auflösung fest. Dadurch lässt sich die Datenrate reduzieren.
    ''' </summary>
    ''' <param name="resolution">Faktor, um wieviel die Auflösung in Horizontalrichtung minimiert wird. Werte: 1(keine Veränderung),2,3,4,5,6,8,16)</param>
    ''' <remarks></remarks>
    Public Sub resolution(ByVal resolution As Integer)
        Me._framegrabber.SetFramegrabberParam("horizontal_resolution", resolution)
        Me._framegrabber.SetFramegrabberParam("vertical_resolution", resolution)
    End Sub

    Public Function get_resolution() As Long
        Dim hz As Long
        'Dim v As Long
        hz = Me._framegrabber.GetFramegrabberParam(New HTuple("horizontal_resolution"))
        'v = Me._framegrabber.GetFramegrabberParam("vertical_resolution")
        Return hz
    End Function

    Public Sub grabLiveBild()
        Dim image As HImage

        Do While (Me._Controller.LiveBildRunning)
            Try
                image = Me._framegrabber.GrabImage()
                Me._Controller.displayLiveBild(image)
                ' Um ein Überlauf des Speichers zu verhindern, sollte der Speicher des Bildobjekts nach der Verwendung wieder freigegeben werden.
                ' Normalerweise kümmert sich automatisch der Garbage Collector von .NET um die Entfernung von nicht verwendeten Objekten. Bei Bilddaten
                ' von HALCON kann es aber passieren, das diese vom GC nicht als freie Variablen erkannt werden kann. Deswegen ist es empfehlenswert, den
                ' GC manuell zu starten, wenn ein Objekt nicht mehr benötigt wird. (siehe auch: HALCON "Programmers Guide" Kapitel 10.4.3.3)
                ' Dazu gibt es zwei Möglichkeiten:
                ' 1. Möglichkeit:
                'GC.Collect()
                'GC.WaitForPendingFinalizers()

                ' 2. Möglichkeit:
                image.Dispose()
            Catch ex As HOperatorException
                ' Bei dem Livebild soll die Schleife einfach weitergeführt werden.
            End Try
        Loop
        Me._Controller.liveModusIsStopped()
    End Sub

    Public Function grabSingleImage() As HImage
        Dim image As HImage
        image = Me._framegrabber.GrabImageAsync(-1)
        Return image
    End Function
    ''' <summary>
    ''' Gibt den Bereich für den Pixeltakt zurück.
    ''' </summary>
    ''' <returns>
    ''' Integer-Array; [minimum, maximum, stepwidth, default values]
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_pixel_clock_range() As Integer()
        Dim parameter As HTuple
        Dim parameterArray(4) As Integer

        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("pixel_clock_range"))

        For i As Integer = 0 To parameter.Length - 1
            parameterArray(i) = parameter(i).I         ' .I = Ausgabe der Werte als Integer
        Next i
        Return parameterArray
    End Function
    ''' <summary>
    ''' Gibt den aktuellen Pixeltakt zurück.
    ''' </summary>
    ''' <returns>
    ''' Integer; Pixeltakt
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_pixel_clock() As Integer
        Dim parameter As HTuple
        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("pixel_clock"))
        Return parameter(0).I
    End Function
    ''' <summary>
    ''' Legt den Pixeltakt fest.
    ''' </summary>
    ''' <param name="value">
    ''' Integer; Pixeltakt
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_pixel_clock(ByVal value As Integer)
        Try
            Me._framegrabber.SetFramegrabberParam("pixel_clock", value)
        Catch ex As HalconException
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Gibt den Bereich für die Bildrate zurück.
    ''' </summary>
    ''' <returns>
    ''' Double-Array; [minimum, maximum, stepwidth, default values]
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_frame_rate_range() As Double()
        Dim parameter As HTuple
        Dim parameterArray(4) As Double

        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("frame_rate_range"))

        For i As Integer = 0 To 3
            parameterArray(i) = parameter(i).D         ' .D = Ausgabe der Werte als Double
        Next i
        Return parameterArray
    End Function
    ''' <summary>
    ''' Gibt die aktuellen Bildrate zurück.
    ''' </summary>
    ''' <returns>
    ''' Double; Bildrate
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_frame_rate() As Double
        Dim parameter As HTuple
        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("frame_rate"))
        Return parameter(0).D
    End Function
    ''' <summary>
    ''' Legt die Bildrate fest.
    ''' </summary>
    ''' <param name="value">
    ''' Double; Bildrate
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_frame_rate(ByVal value As Double)
        Me._framegrabber.SetFramegrabberParam("frame_rate", value)
    End Sub

    ''' <summary>
    ''' Gibt den Bereich für die Belichtung zurück.
    ''' </summary>
    ''' <returns>
    ''' Double-Array; [minimum, maximum, stepwidth, default values]
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_exposure_range() As Double()
        Dim parameter As HTuple
        Dim parameterArray(4) As Double

        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("exposure_range"))

        For i As Integer = 0 To 3
            parameterArray(i) = parameter(i).D         ' .D = Ausgabe der Werte als Double
        Next i
        Return parameterArray
    End Function

    ''' <summary>
    ''' Gibt die aktuellen Belichtungszeit zurück.
    ''' </summary>
    ''' <returns>
    ''' Double; Bildrate
    ''' </returns>
    ''' <remarks></remarks>
    Public Function get_exposure() As Double
        Dim parameter As HTuple
        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("exposure"))
        Return parameter(0).D
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value">
    ''' Double; 
    ''' </param>
    ''' <remarks></remarks>
    Public Sub set_exposure(ByVal value As Double)
        Me._framegrabber.SetFramegrabberParam("exposure", value)
    End Sub

    Public Sub set_auto_exposure(ByVal flag As Boolean)
        If (flag = True) Then
            Me._framegrabber.SetFramegrabberParam("exposure", "auto")
        ElseIf (flag = False) Then
            Me._framegrabber.SetFramegrabberParam("exposure", "manual")
        End If
    End Sub

    Public Sub set_auto_white_balance(ByVal flag As Boolean)
        If (flag = True) Then
            Me._framegrabber.SetFramegrabberParam("gain_master", "auto")
            Me._framegrabber.SetFramegrabberParam("white_balance", "daylight")
            Me._framegrabber.SetFramegrabberParam("white_balance", "auto_next_frame")
        ElseIf (flag = False) Then
            Me._framegrabber.SetFramegrabberParam("white_balance", "disable")
        End If
    End Sub

    Public Function get_sensor_size() As Long()
        Dim parameter As HTuple
        parameter = Me._framegrabber.GetFramegrabberParam(New HTuple("sensor_size"))

        Dim para(1) As Long
        para(0) = parameter(0).L
        para(1) = parameter(1).L
        Return para
    End Function
End Class
