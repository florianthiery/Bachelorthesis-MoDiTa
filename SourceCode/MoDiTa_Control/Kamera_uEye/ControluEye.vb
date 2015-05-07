Imports HalconDotNet
Imports System.Threading

Public Class ControluEye
    Private _GUI As MoDiTaGUI
    Private _LiveBildRunning As Boolean = False
    Private _Camera As CamerauEye


    Private _pixel_clock_value As Integer
    Private _frame_rate_value As Double

    Private backThread As Thread

    Private _max_sensor_width As Long
    Private _max_sensor_heigth As Long

    Private _resolution_single_image As Integer = 1

    'Private _resolution_live As Integer = 1

    Private _liveModusIsStopped As Boolean = False


    Private Delegate Sub DisplayLiveBildDelegate(ByVal image As HImage)
    Private Delegate Sub DisplayCameraStatusDelegate(ByVal value As Integer)
    

    Public Sub New(ByVal gui As Form)
        Me._GUI = CType(gui, MoDiTaGUI)
        Me._Camera = New CamerauEye()
    End Sub

    Public Function connect() As Boolean
        Me._Camera.connectCamera(Me)
        Me.get_sensor_size()
        Return Me._Camera.IsCameraConnected()
    End Function

    Public Sub disconnect()
        If (Me._Camera.IsCameraConnected() = True) Then
            Me._Camera.disconnectCamera()
        End If
    End Sub

    Public ReadOnly Property sensor_width() As Long
        Get
            Return Me._max_sensor_width
        End Get
    End Property

    Public ReadOnly Property sensor_heigth() As Long
        Get
            Return Me._max_sensor_heigth
        End Get
    End Property

    Public Function startLiveBild()
        If (Me._LiveBildRunning = False) Then
            Me._LiveBildRunning = True
            ' Hintergrundthread: in diesem läuft die Do-Schleife, die das aktuelle Bild von der Kamera holt und an die GUI weitergibt.
            backThread = New Thread(AddressOf Me._Camera.grabLiveBild)
            backThread.Start()

            Dim disp2 As New DisplayCameraStatusDelegate(AddressOf Me._GUI.ChangeCameraStatusSymbol)
            Me._GUI.Invoke(disp2, 2)
        End If
        Return Me._LiveBildRunning
    End Function

    ''' <summary>
    ''' Stoppt das Livebild.
    ''' </summary>
    ''' <remarks>
    ''' Der Thread wird hier nicht sofort beendet, sondern die Do-While wird beendet und die Methode wird fertig ausgeführt.
    ''' </remarks>
    Public Sub stopLiveBild()
        Me._LiveBildRunning = False
    End Sub

    Public Sub liveModusIsStopped()
        Me._liveModusIsStopped = True
        Dim disp As New DisplayCameraStatusDelegate(AddressOf Me._GUI.ChangeCameraStatusSymbol)
        Me._GUI.Invoke(disp, 1)
    End Sub

    ''' <summary>
    ''' Abbruch des Livebildes.
    ''' </summary>
    ''' <remarks>
    ''' Im Gegensatz zu stopLiveBild, wird der Thread sofort abgebrochen.
    ''' </remarks>
    Public Sub abortLiveBild()
        Try
            If (backThread.IsAlive) Then
                backThread.Abort()
            End If
            Me.liveModusIsStopped()
        Catch ex As NullReferenceException
            ' Wenn die das Formular geschlossen wird, bevor eine Kamera aktiviert wurde, gibt es eine NullReferenceException.
            ' Dies hat aber keine weitere Auswirkungen, deswegen keine weitere Anweisung.
        End Try

    End Sub

    ''' <summary>
    ''' Auflösung des Livemodus
    ''' </summary>
    ''' <value>
    ''' Ändert die Auflösung, beinhaltet auch die Funktion "change_resolution"
    ''' </value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resolution_live() As Integer
        Get
            Return Me._Camera.get_resolution
        End Get
        Set(ByVal value As Integer)
            If (value <> Me._Camera.get_resolution) Then
                'Me._resolution_live = value
                Me.change_resolution(value)
            End If
        End Set
    End Property

    Private Sub change_resolution(ByVal value As Long)
        Dim backgroundThread As New Thread(AddressOf thread_change_resolution)
        backgroundThread.Start(value)
    End Sub

    Private Sub thread_change_resolution(ByVal value As Long)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.resolution(value)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.resolution(value)
        End If
    End Sub

    Public Property resolution_single_image() As Integer
        Get
            Return Me._resolution_single_image
        End Get
        Set(ByVal value As Integer)
            Me._resolution_single_image = value
        End Set
    End Property

    Public Sub set_auto_exposure(ByVal flag As Boolean)
        Dim backgroundThread As New Thread(AddressOf thread_set_auto_exposure)
        backgroundThread.Start(flag)
    End Sub

    Private Sub thread_set_auto_exposure(ByVal flag As Boolean)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.set_auto_exposure(flag)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.set_auto_exposure(flag)
        End If
    End Sub

    Public Sub set_auto_white_balance(ByVal flag As Boolean)
        Dim backgroundThread As New Thread(AddressOf thread_set_auto_white_balance)
        backgroundThread.Start(flag)
    End Sub

    Private Sub thread_set_auto_white_balance(ByVal flag As Boolean)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.set_auto_white_balance(flag)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.set_auto_white_balance(flag)
        End If
    End Sub

#Region "Pixelclock"
    Public Property pixel_clock() As Integer
        Get
            Return Me._Camera.get_pixel_clock()
        End Get
        Set(ByVal value As Integer)
            Dim backgroundThread As New Thread(AddressOf thread_set_pixel_clock)
            backgroundThread.Start(value)
        End Set
    End Property

    Private Sub thread_set_pixel_clock(ByVal value As Integer)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.set_pixel_clock(value)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.set_pixel_clock(value)
        End If
    End Sub

    Public ReadOnly Property pixel_clock_range() As Integer()
        Get
            Return Me._Camera.get_pixel_clock_range()
        End Get
    End Property
#End Region

#Region "Framerate"
    Public Property frame_rate() As Double
        Get
            Return Me._Camera.get_frame_rate()
        End Get
        Set(ByVal value As Double)
            Dim backgroundThread As New Thread(AddressOf thread_set_frame_rate)
            backgroundThread.Start(value)
        End Set
    End Property

    Private Sub thread_set_frame_rate(ByVal value As Double)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.set_frame_rate(value)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.set_frame_rate(value)
        End If
    End Sub

    Public ReadOnly Property frame_rate_range() As Double()
        Get
            Return Me._Camera.get_frame_rate_range()
        End Get
    End Property
#End Region

#Region "Belichtungszeit"
    Public Property exposure_time() As Double
        Get
            Return Me._Camera.get_exposure()
        End Get
        Set(ByVal value As Double)
            Dim backgroundThread As New Thread(AddressOf thread_set_exposure)
            backgroundThread.Start(value)
        End Set
    End Property

    Private Sub thread_set_exposure(ByVal value As Double)
        If (Me._LiveBildRunning = True) Then
            Me._liveModusIsStopped = False
            Me.stopLiveBild()
            Do While (Me._liveModusIsStopped = False)
                Thread.Sleep(1)
            Loop
            Me._Camera.set_exposure(value)
            Me.startLiveBild()
        ElseIf (Me._LiveBildRunning = False) Then
            Me._Camera.set_exposure(value)
        End If
    End Sub

    Public ReadOnly Property exposure_time_range() As Double()
        Get
            Return Me._Camera.get_exposure_range()
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Schließt den Framegrabber.
    ''' </summary>
    ''' <remarks>
    ''' Abbruch des Livebilds und schließen des Framegrabbers
    ''' </remarks>
    Public Sub closeFramegrabber()
        Me.stopLiveBild()
        If (backThread.IsAlive) Then
            backThread.Abort()
        End If
        Me._Camera.disconnectCamera()
    End Sub


    Private Sub get_sensor_size()
        Dim para(1) As Long
        para = Me._Camera.get_sensor_size()
        Me._max_sensor_width = para(0)
        Me._max_sensor_heigth = para(1)
    End Sub



    Public ReadOnly Property LiveBildRunning() As Boolean
        Get
            Return Me._LiveBildRunning
        End Get
    End Property

    Public Sub displayLiveBild(ByVal image As HImage)
        Dim disp As New DisplayLiveBildDelegate(AddressOf Me._GUI.display_Livepicture)

        ' Das Invoke sorgt dafür, dass der Befehl im Hauptthread (GUI) durchgeführt wird. Aber erst dann, wenn der Hauptthread
        ' nicht beschäftigt ist. Dadurch ist sichergestellt, dass nicht zwei Threads auf ein GUI-Element zugreift.
        Me._GUI.Invoke(disp, image)

    End Sub

    Public Function makeSingleImage() As HImage
        Return Me._Camera.grabSingleImage()
    End Function

    Public Sub cameraIsDisconnected()
        Dim disp As New DisplayCameraStatusDelegate(AddressOf Me._GUI.ChangeCameraStatusSymbol)
        Me._GUI.Invoke(disp, 0)
    End Sub



End Class
