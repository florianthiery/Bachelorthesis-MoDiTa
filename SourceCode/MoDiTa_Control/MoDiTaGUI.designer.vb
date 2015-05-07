<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MoDiTaGUI
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MoDiTaGUI))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.tssLa_TachyErrorcodes = New System.Windows.Forms.ToolStripStatusLabel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.Tab_DeviceControl = New System.Windows.Forms.TabPage
        Me.gb_DeviceControl_totalstation = New System.Windows.Forms.GroupBox
        Me.Button_Tachymeter_Off = New System.Windows.Forms.Button
        Me.Button_Tachymeter_on = New System.Windows.Forms.Button
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation = New System.Windows.Forms.GroupBox
        Me.cmbComports = New System.Windows.Forms.ComboBox
        Me.bu_deviceControl_totalStation_CloseComport = New System.Windows.Forms.Button
        Me.gb_DeviceControl_Camera = New System.Windows.Forms.GroupBox
        Me.La_DeviceControl_Camerastatus = New System.Windows.Forms.Label
        Me.Panel_DeviceControl_Camerastatus = New System.Windows.Forms.Panel
        Me.Bu_DeviceControl_Camera_Disconnect = New System.Windows.Forms.Button
        Me.Bu_DeviceControl_Camera_connect = New System.Windows.Forms.Button
        Me.gb_livebild = New System.Windows.Forms.GroupBox
        Me.Bu_LiveModusStop = New System.Windows.Forms.Button
        Me.Bu_LiveModusStart = New System.Windows.Forms.Button
        Me.Tab_TotalStation = New System.Windows.Forms.TabPage
        Me.GroupBox_Laserpointer = New System.Windows.Forms.GroupBox
        Me.CheckBox_Laserpointer_activate = New System.Windows.Forms.CheckBox
        Me.GroupBox_LockMode = New System.Windows.Forms.GroupBox
        Me.Button_Lockin_Stop = New System.Windows.Forms.Button
        Me.Button_Lockin = New System.Windows.Forms.Button
        Me.GroupBox_ATR = New System.Windows.Forms.GroupBox
        Me.CheckBox_ATR_Activate = New System.Windows.Forms.CheckBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.TextBox_ATR_SearchRange_Hz = New System.Windows.Forms.TextBox
        Me.TextBox_ATR_SearchRange_V = New System.Windows.Forms.TextBox
        Me.Button_FineAdjust = New System.Windows.Forms.Button
        Me.gb_zenitRange = New System.Windows.Forms.GroupBox
        Me.bu_def_vertical_range = New System.Windows.Forms.Button
        Me.La_vertical_range_face2 = New System.Windows.Forms.Label
        Me.La_vertical_range_face1 = New System.Windows.Forms.Label
        Me.tb_verticale_range_lage1 = New System.Windows.Forms.TextBox
        Me.tb_verticale_range_lage2 = New System.Windows.Forms.TextBox
        Me.rb_vertical_range_user_define = New System.Windows.Forms.RadioButton
        Me.rb_vertical_range_camera = New System.Windows.Forms.RadioButton
        Me.rb_vertical_range_free = New System.Windows.Forms.RadioButton
        Me.gb_edm_mode = New System.Windows.Forms.GroupBox
        Me.rb_edmMode_RL = New System.Windows.Forms.RadioButton
        Me.rb_edmMode_IR = New System.Windows.Forms.RadioButton
        Me.Tab_TachyMove = New System.Windows.Forms.TabPage
        Me.gb_lagewechsel = New System.Windows.Forms.GroupBox
        Me.ch_precisionOn_2 = New System.Windows.Forms.CheckBox
        Me.btChangeFace = New System.Windows.Forms.Button
        Me.gb_joystick = New System.Windows.Forms.GroupBox
        Me.bu_stopmove = New System.Windows.Forms.Button
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.lbVelocityHz = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.trbVelocityHz = New System.Windows.Forms.TrackBar
        Me.cbVelocityEqual = New System.Windows.Forms.CheckBox
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.lbVelocityVz = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.trbVelocityVz = New System.Windows.Forms.TrackBar
        Me.btdownright = New System.Windows.Forms.Button
        Me.btupright = New System.Windows.Forms.Button
        Me.btdown = New System.Windows.Forms.Button
        Me.btupleft = New System.Windows.Forms.Button
        Me.btdownleft = New System.Windows.Forms.Button
        Me.btup = New System.Windows.Forms.Button
        Me.btright = New System.Windows.Forms.Button
        Me.btleft = New System.Windows.Forms.Button
        Me.gb_moveabsolute = New System.Windows.Forms.GroupBox
        Me.ch_precisionOn_1 = New System.Windows.Forms.CheckBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btDriveAbs = New System.Windows.Forms.Button
        Me.tbHzAbsolut = New System.Windows.Forms.TextBox
        Me.tbVzAbsolut = New System.Windows.Forms.TextBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupBox_camera_image = New System.Windows.Forms.GroupBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.CheckBox5 = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.GroupBox_camera_SizeLiveModus = New System.Windows.Forms.GroupBox
        Me.CheckBox_SameResolution = New System.Windows.Forms.CheckBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.ComboBox_Resolution_Single = New System.Windows.Forms.ComboBox
        Me.ComboBox_Resolution_Live = New System.Windows.Forms.ComboBox
        Me.GroupBox_CameraTiming = New System.Windows.Forms.GroupBox
        Me.Label_ExposureTime_Max = New System.Windows.Forms.Label
        Me.Label_ExposureTime_Min = New System.Windows.Forms.Label
        Me.Label_FrameRate_Max = New System.Windows.Forms.Label
        Me.Label_FrameRate_Min = New System.Windows.Forms.Label
        Me.Label_PixelClock_Max = New System.Windows.Forms.Label
        Me.Label_PixelClock_Min = New System.Windows.Forms.Label
        Me.Label_ExposureTime_act = New System.Windows.Forms.Label
        Me.Label_FrameRate_act = New System.Windows.Forms.Label
        Me.Label_PixelClock_act = New System.Windows.Forms.Label
        Me.TrackBar_ExposureTime = New System.Windows.Forms.TrackBar
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.TrackBar_FrameRate = New System.Windows.Forms.TrackBar
        Me.TrackBar_PixelClock = New System.Windows.Forms.TrackBar
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox_SK_Solve = New System.Windows.Forms.GroupBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label_SK_Error_stddev = New System.Windows.Forms.Label
        Me.Label_SK_Error_mean = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label100 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label_SK_Error_max = New System.Windows.Forms.Label
        Me.Label_SK_Error_min = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Button_Solve_SK = New System.Windows.Forms.Button
        Me.CheckBox_SaveImage_crosshair = New System.Windows.Forms.CheckBox
        Me.ComboBox_crosshair_types = New System.Windows.Forms.ComboBox
        Me.GroupBox_Field_of_View = New System.Windows.Forms.GroupBox
        Me.Label_Status_Sk = New System.Windows.Forms.Label
        Me.Panel_Status_Sk = New System.Windows.Forms.Panel
        Me.Label39 = New System.Windows.Forms.Label
        Me.ComboBox_controlpoints = New System.Windows.Forms.ComboBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.GroupBox_SKTarget2 = New System.Windows.Forms.GroupBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.Button_sk_reg_target2_center = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.TextBox_sk_target2_center_v = New System.Windows.Forms.TextBox
        Me.TextBox_sk_target2_center_hz = New System.Windows.Forms.TextBox
        Me.GroupBox_SKTarget1 = New System.Windows.Forms.GroupBox
        Me.GroupBox_Sk_target1_circle = New System.Windows.Forms.GroupBox
        Me.Button_sk_reg_target1_circle = New System.Windows.Forms.Button
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.TextBox_sk_target1_circle_v = New System.Windows.Forms.TextBox
        Me.TextBox_sk_target1_circle_hz = New System.Windows.Forms.TextBox
        Me.GroupBox_Sk_target1_center = New System.Windows.Forms.GroupBox
        Me.Button_sk_reg_target1_center = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.TextBox_sk_target1_center_v = New System.Windows.Forms.TextBox
        Me.TextBox_sk_target1_center_hz = New System.Windows.Forms.TextBox
        Me.ComboBox_Sk_typ = New System.Windows.Forms.ComboBox
        Me.CheckBox_SaveErrorImages_self_calibration = New System.Windows.Forms.CheckBox
        Me.CheckBox_SaveImage_selbstkalibrierung = New System.Windows.Forms.CheckBox
        Me.Button_SelfCalibrationStart = New System.Windows.Forms.Button
        Me.Button_Crosshair = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox18 = New System.Windows.Forms.GroupBox
        Me.CheckBox_KreuzEinschalten = New System.Windows.Forms.CheckBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.TextBox_Name_AVMessung = New System.Windows.Forms.TextBox
        Me.TextBox_Wartezeit = New System.Windows.Forms.TextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.Button_SP_Reset = New System.Windows.Forms.Button
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.Button_Messung3_Shift = New System.Windows.Forms.Button
        Me.Button_Messung3_Zentrum = New System.Windows.Forms.Button
        Me.Button_Messung2_Shift = New System.Windows.Forms.Button
        Me.Button_Messung2_Zentrum = New System.Windows.Forms.Button
        Me.CheckBox_Vz_Shift = New System.Windows.Forms.CheckBox
        Me.CheckBox_Hz_Shift = New System.Windows.Forms.CheckBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.Button_Snapshot = New System.Windows.Forms.Button
        Me.TextBox_Messzeit = New System.Windows.Forms.TextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.Button_25_Bilder_Komplett = New System.Windows.Forms.Button
        Me.Button_9_Bilder_Kreuz_Gedreht = New System.Windows.Forms.Button
        Me.Button_9_Bilder_Kreuz_Aufrecht = New System.Windows.Forms.Button
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Button_Messe_Rand = New System.Windows.Forms.Button
        Me.Button_Messe_Zentrum = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btKompOff = New System.Windows.Forms.Button
        Me.btKompOn = New System.Windows.Forms.Button
        Me.GroupBox19 = New System.Windows.Forms.GroupBox
        Me.lbPositionTime = New System.Windows.Forms.Label
        Me.btSetPosTime = New System.Windows.Forms.Button
        Me.cbTimeTol = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lbPositionTolerance = New System.Windows.Forms.Label
        Me.cbPosTol = New System.Windows.Forms.ComboBox
        Me.btSetPosTol = New System.Windows.Forms.Button
        Me.GroupBox20 = New System.Windows.Forms.GroupBox
        Me.btSetAngleCorrections = New System.Windows.Forms.Button
        Me.cbHiKorr = New System.Windows.Forms.CheckBox
        Me.cbSaKorr = New System.Windows.Forms.CheckBox
        Me.cbZaKorr = New System.Windows.Forms.CheckBox
        Me.cbKaKorr = New System.Windows.Forms.CheckBox
        Me.TabControl3 = New System.Windows.Forms.TabControl
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.GroupBox_Measure_Image = New System.Windows.Forms.GroupBox
        Me.CheckBox_Measure_Image = New System.Windows.Forms.CheckBox
        Me.gb_zoommove = New System.Windows.Forms.GroupBox
        Me.bu_ZoomAndMoveReset = New System.Windows.Forms.Button
        Me.rb_move = New System.Windows.Forms.RadioButton
        Me.rb_zoom = New System.Windows.Forms.RadioButton
        Me.HWindowControl1 = New HalconDotNet.HWindowControl
        Me.gb_Snapshot = New System.Windows.Forms.GroupBox
        Me.Bu_Snapshot = New System.Windows.Forms.Button
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.bu_export_data_as_csv = New System.Windows.Forms.Button
        Me.DGVData = New System.Windows.Forms.DataGridView
        Me.TabPage9 = New System.Windows.Forms.TabPage
        Me.bu_export_log_as_csv = New System.Windows.Forms.Button
        Me.DGVLog = New System.Windows.Forms.DataGridView
        Me.TabPage_bv1 = New System.Windows.Forms.TabPage
        Me.HWindowControl2 = New HalconDotNet.HWindowControl
        Me.uEye = New System.Windows.Forms.TabPage
        Me.Label_X2 = New System.Windows.Forms.Label
        Me.Label_X3 = New System.Windows.Forms.Label
        Me.Label_X4 = New System.Windows.Forms.Label
        Me.Label_X5 = New System.Windows.Forms.Label
        Me.Label_X1 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.ComboBox_uEye_Subsampling = New System.Windows.Forms.ComboBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.ComboBox_uEye_binning = New System.Windows.Forms.ComboBox
        Me.gb_property = New System.Windows.Forms.GroupBox
        Me.Bu_camera_property = New System.Windows.Forms.Button
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.Button7 = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.AxuEyeCam1 = New AxuEyeCamLib.AxuEyeCam
        Me.gb_measure = New System.Windows.Forms.GroupBox
        Me.cb_Measure_saveImage = New System.Windows.Forms.CheckBox
        Me.cb_Measure_withoutSave = New System.Windows.Forms.CheckBox
        Me.Bu_Measure_All = New System.Windows.Forms.Button
        Me.Bu_Measure_Distance = New System.Windows.Forms.Button
        Me.Bu_Measure_Direction = New System.Windows.Forms.Button
        Me.La_Measure_pointname = New System.Windows.Forms.Label
        Me.tb_pointname = New System.Windows.Forms.TextBox
        Me.La_Measure_pointnumber = New System.Windows.Forms.Label
        Me.tb_pointnumber = New System.Windows.Forms.TextBox
        Me.La_ID_Status = New System.Windows.Forms.Label
        Me.La_Measure_id = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ts_project = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gb_Measure_StatusStation = New System.Windows.Forms.GroupBox
        Me.La_theoinfos = New System.Windows.Forms.Label
        Me.La_Tachymeterstatus = New System.Windows.Forms.Label
        Me.Panel_Tachymeter_Status = New System.Windows.Forms.Panel
        Me.gb_status_Camera = New System.Windows.Forms.GroupBox
        Me.La_DeviceControl_Camerastatus2 = New System.Windows.Forms.Label
        Me.Panel_DeviceControl_Camerastatus2 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ComboBox4 = New System.Windows.Forms.ComboBox
        Me.gb_Camera_Timing = New System.Windows.Forms.GroupBox
        Me.gb_camera_image = New System.Windows.Forms.GroupBox
        Me.cb_camera_image_awb = New System.Windows.Forms.CheckBox
        Me.cb_camera_image_agc = New System.Windows.Forms.CheckBox
        Me.cb_camera_image_aes = New System.Windows.Forms.CheckBox
        Me.gb_camera_SizeLiveModus = New System.Windows.Forms.GroupBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.TrackBar2 = New System.Windows.Forms.TrackBar
        Me.TrackBar3 = New System.Windows.Forms.TrackBar
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.ComboBox3 = New System.Windows.Forms.ComboBox
        Me.StatusStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Tab_DeviceControl.SuspendLayout()
        Me.gb_DeviceControl_totalstation.SuspendLayout()
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.SuspendLayout()
        Me.gb_DeviceControl_Camera.SuspendLayout()
        Me.gb_livebild.SuspendLayout()
        Me.Tab_TotalStation.SuspendLayout()
        Me.GroupBox_Laserpointer.SuspendLayout()
        Me.GroupBox_LockMode.SuspendLayout()
        Me.GroupBox_ATR.SuspendLayout()
        Me.gb_zenitRange.SuspendLayout()
        Me.gb_edm_mode.SuspendLayout()
        Me.Tab_TachyMove.SuspendLayout()
        Me.gb_lagewechsel.SuspendLayout()
        Me.gb_joystick.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        CType(Me.trbVelocityHz, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox17.SuspendLayout()
        CType(Me.trbVelocityVz, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb_moveabsolute.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox_camera_image.SuspendLayout()
        Me.GroupBox_camera_SizeLiveModus.SuspendLayout()
        Me.GroupBox_CameraTiming.SuspendLayout()
        CType(Me.TrackBar_ExposureTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_FrameRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_PixelClock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox_SK_Solve.SuspendLayout()
        Me.GroupBox_Field_of_View.SuspendLayout()
        Me.GroupBox_SKTarget2.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox_SKTarget1.SuspendLayout()
        Me.GroupBox_Sk_target1_circle.SuspendLayout()
        Me.GroupBox_Sk_target1_center.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.GroupBox_Measure_Image.SuspendLayout()
        Me.gb_zoommove.SuspendLayout()
        Me.gb_Snapshot.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        CType(Me.DGVData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.DGVLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_bv1.SuspendLayout()
        Me.uEye.SuspendLayout()
        Me.gb_property.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.AxuEyeCam1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb_measure.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.gb_Measure_StatusStation.SuspendLayout()
        Me.gb_status_Camera.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gb_camera_image.SuspendLayout()
        Me.gb_camera_SizeLiveModus.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssLa_TachyErrorcodes})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 701)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1019, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tssLa_TachyErrorcodes
        '
        Me.tssLa_TachyErrorcodes.Name = "tssLa_TachyErrorcodes"
        Me.tssLa_TachyErrorcodes.Size = New System.Drawing.Size(0, 17)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tab_DeviceControl)
        Me.TabControl1.Controls.Add(Me.Tab_TotalStation)
        Me.TabControl1.Controls.Add(Me.Tab_TachyMove)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(358, 473)
        Me.TabControl1.TabIndex = 14
        '
        'Tab_DeviceControl
        '
        Me.Tab_DeviceControl.Controls.Add(Me.gb_DeviceControl_totalstation)
        Me.Tab_DeviceControl.Controls.Add(Me.gb_DeviceControl_Camera)
        Me.Tab_DeviceControl.Controls.Add(Me.gb_livebild)
        Me.Tab_DeviceControl.Location = New System.Drawing.Point(4, 22)
        Me.Tab_DeviceControl.Name = "Tab_DeviceControl"
        Me.Tab_DeviceControl.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_DeviceControl.Size = New System.Drawing.Size(350, 447)
        Me.Tab_DeviceControl.TabIndex = 0
        Me.Tab_DeviceControl.Text = "Device Control"
        Me.Tab_DeviceControl.UseVisualStyleBackColor = True
        '
        'gb_DeviceControl_totalstation
        '
        Me.gb_DeviceControl_totalstation.Controls.Add(Me.Button_Tachymeter_Off)
        Me.gb_DeviceControl_totalstation.Controls.Add(Me.Button_Tachymeter_on)
        Me.gb_DeviceControl_totalstation.Controls.Add(Me.gb_DeviceControl_TotalStation_ConnectToTotalStation)
        Me.gb_DeviceControl_totalstation.Location = New System.Drawing.Point(4, 7)
        Me.gb_DeviceControl_totalstation.Name = "gb_DeviceControl_totalstation"
        Me.gb_DeviceControl_totalstation.Size = New System.Drawing.Size(330, 122)
        Me.gb_DeviceControl_totalstation.TabIndex = 0
        Me.gb_DeviceControl_totalstation.TabStop = False
        Me.gb_DeviceControl_totalstation.Text = "Total Station"
        '
        'Button_Tachymeter_Off
        '
        Me.Button_Tachymeter_Off.Location = New System.Drawing.Point(128, 89)
        Me.Button_Tachymeter_Off.Name = "Button_Tachymeter_Off"
        Me.Button_Tachymeter_Off.Size = New System.Drawing.Size(75, 23)
        Me.Button_Tachymeter_Off.TabIndex = 57
        Me.Button_Tachymeter_Off.Text = "Off"
        Me.Button_Tachymeter_Off.UseVisualStyleBackColor = True
        '
        'Button_Tachymeter_on
        '
        Me.Button_Tachymeter_on.Location = New System.Drawing.Point(34, 89)
        Me.Button_Tachymeter_on.Name = "Button_Tachymeter_on"
        Me.Button_Tachymeter_on.Size = New System.Drawing.Size(75, 23)
        Me.Button_Tachymeter_on.TabIndex = 56
        Me.Button_Tachymeter_on.Text = "On"
        Me.Button_Tachymeter_on.UseVisualStyleBackColor = True
        '
        'gb_DeviceControl_TotalStation_ConnectToTotalStation
        '
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Controls.Add(Me.cmbComports)
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Controls.Add(Me.bu_deviceControl_totalStation_CloseComport)
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Location = New System.Drawing.Point(9, 19)
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Name = "gb_DeviceControl_TotalStation_ConnectToTotalStation"
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Size = New System.Drawing.Size(315, 54)
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.TabIndex = 55
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.TabStop = False
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.Text = "Connect"
        '
        'cmbComports
        '
        Me.cmbComports.FormattingEnabled = True
        Me.cmbComports.Location = New System.Drawing.Point(6, 19)
        Me.cmbComports.Name = "cmbComports"
        Me.cmbComports.Size = New System.Drawing.Size(140, 21)
        Me.cmbComports.TabIndex = 4
        Me.cmbComports.Text = "Choice"
        '
        'bu_deviceControl_totalStation_CloseComport
        '
        Me.bu_deviceControl_totalStation_CloseComport.Location = New System.Drawing.Point(162, 17)
        Me.bu_deviceControl_totalStation_CloseComport.Name = "bu_deviceControl_totalStation_CloseComport"
        Me.bu_deviceControl_totalStation_CloseComport.Size = New System.Drawing.Size(140, 23)
        Me.bu_deviceControl_totalStation_CloseComport.TabIndex = 6
        Me.bu_deviceControl_totalStation_CloseComport.Text = "Close Comport"
        Me.bu_deviceControl_totalStation_CloseComport.UseVisualStyleBackColor = True
        '
        'gb_DeviceControl_Camera
        '
        Me.gb_DeviceControl_Camera.Controls.Add(Me.La_DeviceControl_Camerastatus)
        Me.gb_DeviceControl_Camera.Controls.Add(Me.Panel_DeviceControl_Camerastatus)
        Me.gb_DeviceControl_Camera.Controls.Add(Me.Bu_DeviceControl_Camera_Disconnect)
        Me.gb_DeviceControl_Camera.Controls.Add(Me.Bu_DeviceControl_Camera_connect)
        Me.gb_DeviceControl_Camera.Location = New System.Drawing.Point(6, 148)
        Me.gb_DeviceControl_Camera.Name = "gb_DeviceControl_Camera"
        Me.gb_DeviceControl_Camera.Size = New System.Drawing.Size(170, 79)
        Me.gb_DeviceControl_Camera.TabIndex = 22
        Me.gb_DeviceControl_Camera.TabStop = False
        Me.gb_DeviceControl_Camera.Text = "Camera"
        '
        'La_DeviceControl_Camerastatus
        '
        Me.La_DeviceControl_Camerastatus.AutoSize = True
        Me.La_DeviceControl_Camerastatus.Location = New System.Drawing.Point(33, 52)
        Me.La_DeviceControl_Camerastatus.Name = "La_DeviceControl_Camerastatus"
        Me.La_DeviceControl_Camerastatus.Size = New System.Drawing.Size(61, 13)
        Me.La_DeviceControl_Camerastatus.TabIndex = 4
        Me.La_DeviceControl_Camerastatus.Text = "Disconnect"
        '
        'Panel_DeviceControl_Camerastatus
        '
        Me.Panel_DeviceControl_Camerastatus.BackColor = System.Drawing.Color.Red
        Me.Panel_DeviceControl_Camerastatus.Location = New System.Drawing.Point(7, 48)
        Me.Panel_DeviceControl_Camerastatus.Name = "Panel_DeviceControl_Camerastatus"
        Me.Panel_DeviceControl_Camerastatus.Size = New System.Drawing.Size(20, 20)
        Me.Panel_DeviceControl_Camerastatus.TabIndex = 3
        '
        'Bu_DeviceControl_Camera_Disconnect
        '
        Me.Bu_DeviceControl_Camera_Disconnect.Location = New System.Drawing.Point(87, 19)
        Me.Bu_DeviceControl_Camera_Disconnect.Name = "Bu_DeviceControl_Camera_Disconnect"
        Me.Bu_DeviceControl_Camera_Disconnect.Size = New System.Drawing.Size(75, 23)
        Me.Bu_DeviceControl_Camera_Disconnect.TabIndex = 1
        Me.Bu_DeviceControl_Camera_Disconnect.Text = "Disconnect"
        Me.Bu_DeviceControl_Camera_Disconnect.UseVisualStyleBackColor = True
        '
        'Bu_DeviceControl_Camera_connect
        '
        Me.Bu_DeviceControl_Camera_connect.Location = New System.Drawing.Point(6, 19)
        Me.Bu_DeviceControl_Camera_connect.Name = "Bu_DeviceControl_Camera_connect"
        Me.Bu_DeviceControl_Camera_connect.Size = New System.Drawing.Size(75, 23)
        Me.Bu_DeviceControl_Camera_connect.TabIndex = 0
        Me.Bu_DeviceControl_Camera_connect.Text = "Connect"
        Me.Bu_DeviceControl_Camera_connect.UseVisualStyleBackColor = True
        '
        'gb_livebild
        '
        Me.gb_livebild.Controls.Add(Me.Bu_LiveModusStop)
        Me.gb_livebild.Controls.Add(Me.Bu_LiveModusStart)
        Me.gb_livebild.Location = New System.Drawing.Point(182, 148)
        Me.gb_livebild.Name = "gb_livebild"
        Me.gb_livebild.Size = New System.Drawing.Size(89, 79)
        Me.gb_livebild.TabIndex = 20
        Me.gb_livebild.TabStop = False
        Me.gb_livebild.Text = "Livemodus"
        '
        'Bu_LiveModusStop
        '
        Me.Bu_LiveModusStop.Location = New System.Drawing.Point(7, 42)
        Me.Bu_LiveModusStop.Name = "Bu_LiveModusStop"
        Me.Bu_LiveModusStop.Size = New System.Drawing.Size(75, 23)
        Me.Bu_LiveModusStop.TabIndex = 1
        Me.Bu_LiveModusStop.Text = "Stop"
        Me.Bu_LiveModusStop.UseVisualStyleBackColor = True
        '
        'Bu_LiveModusStart
        '
        Me.Bu_LiveModusStart.Location = New System.Drawing.Point(7, 17)
        Me.Bu_LiveModusStart.Name = "Bu_LiveModusStart"
        Me.Bu_LiveModusStart.Size = New System.Drawing.Size(75, 23)
        Me.Bu_LiveModusStart.TabIndex = 0
        Me.Bu_LiveModusStart.Text = "Start"
        Me.Bu_LiveModusStart.UseVisualStyleBackColor = True
        '
        'Tab_TotalStation
        '
        Me.Tab_TotalStation.Controls.Add(Me.GroupBox_Laserpointer)
        Me.Tab_TotalStation.Controls.Add(Me.GroupBox_LockMode)
        Me.Tab_TotalStation.Controls.Add(Me.GroupBox_ATR)
        Me.Tab_TotalStation.Controls.Add(Me.gb_zenitRange)
        Me.Tab_TotalStation.Controls.Add(Me.gb_edm_mode)
        Me.Tab_TotalStation.Location = New System.Drawing.Point(4, 22)
        Me.Tab_TotalStation.Name = "Tab_TotalStation"
        Me.Tab_TotalStation.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_TotalStation.Size = New System.Drawing.Size(350, 447)
        Me.Tab_TotalStation.TabIndex = 1
        Me.Tab_TotalStation.Text = "Total Station"
        Me.Tab_TotalStation.UseVisualStyleBackColor = True
        '
        'GroupBox_Laserpointer
        '
        Me.GroupBox_Laserpointer.Controls.Add(Me.CheckBox_Laserpointer_activate)
        Me.GroupBox_Laserpointer.Location = New System.Drawing.Point(202, 359)
        Me.GroupBox_Laserpointer.Name = "GroupBox_Laserpointer"
        Me.GroupBox_Laserpointer.Size = New System.Drawing.Size(90, 42)
        Me.GroupBox_Laserpointer.TabIndex = 57
        Me.GroupBox_Laserpointer.TabStop = False
        Me.GroupBox_Laserpointer.Text = "Laserpointer"
        '
        'CheckBox_Laserpointer_activate
        '
        Me.CheckBox_Laserpointer_activate.AutoSize = True
        Me.CheckBox_Laserpointer_activate.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox_Laserpointer_activate.Name = "CheckBox_Laserpointer_activate"
        Me.CheckBox_Laserpointer_activate.Size = New System.Drawing.Size(65, 17)
        Me.CheckBox_Laserpointer_activate.TabIndex = 0
        Me.CheckBox_Laserpointer_activate.Text = "Activate"
        Me.CheckBox_Laserpointer_activate.UseVisualStyleBackColor = True
        '
        'GroupBox_LockMode
        '
        Me.GroupBox_LockMode.Controls.Add(Me.Button_Lockin_Stop)
        Me.GroupBox_LockMode.Controls.Add(Me.Button_Lockin)
        Me.GroupBox_LockMode.Location = New System.Drawing.Point(9, 359)
        Me.GroupBox_LockMode.Name = "GroupBox_LockMode"
        Me.GroupBox_LockMode.Size = New System.Drawing.Size(98, 82)
        Me.GroupBox_LockMode.TabIndex = 56
        Me.GroupBox_LockMode.TabStop = False
        Me.GroupBox_LockMode.Text = "Lock Mode"
        '
        'Button_Lockin_Stop
        '
        Me.Button_Lockin_Stop.Location = New System.Drawing.Point(11, 48)
        Me.Button_Lockin_Stop.Name = "Button_Lockin_Stop"
        Me.Button_Lockin_Stop.Size = New System.Drawing.Size(75, 23)
        Me.Button_Lockin_Stop.TabIndex = 1
        Me.Button_Lockin_Stop.Text = "Stop"
        Me.Button_Lockin_Stop.UseVisualStyleBackColor = True
        '
        'Button_Lockin
        '
        Me.Button_Lockin.Location = New System.Drawing.Point(11, 19)
        Me.Button_Lockin.Name = "Button_Lockin"
        Me.Button_Lockin.Size = New System.Drawing.Size(75, 23)
        Me.Button_Lockin.TabIndex = 0
        Me.Button_Lockin.Text = "LockIn"
        Me.Button_Lockin.UseVisualStyleBackColor = True
        '
        'GroupBox_ATR
        '
        Me.GroupBox_ATR.Controls.Add(Me.CheckBox_ATR_Activate)
        Me.GroupBox_ATR.Controls.Add(Me.Label24)
        Me.GroupBox_ATR.Controls.Add(Me.Label23)
        Me.GroupBox_ATR.Controls.Add(Me.TextBox_ATR_SearchRange_Hz)
        Me.GroupBox_ATR.Controls.Add(Me.TextBox_ATR_SearchRange_V)
        Me.GroupBox_ATR.Controls.Add(Me.Button_FineAdjust)
        Me.GroupBox_ATR.Location = New System.Drawing.Point(8, 250)
        Me.GroupBox_ATR.Name = "GroupBox_ATR"
        Me.GroupBox_ATR.Size = New System.Drawing.Size(331, 103)
        Me.GroupBox_ATR.TabIndex = 55
        Me.GroupBox_ATR.TabStop = False
        Me.GroupBox_ATR.Text = "ATR"
        '
        'CheckBox_ATR_Activate
        '
        Me.CheckBox_ATR_Activate.AutoSize = True
        Me.CheckBox_ATR_Activate.Location = New System.Drawing.Point(194, 80)
        Me.CheckBox_ATR_Activate.Name = "CheckBox_ATR_Activate"
        Me.CheckBox_ATR_Activate.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox_ATR_Activate.TabIndex = 60
        Me.CheckBox_ATR_Activate.Text = "Activate ATR"
        Me.CheckBox_ATR_Activate.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 49)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(111, 13)
        Me.Label24.TabIndex = 59
        Me.Label24.Text = "Search range V  [gon]"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(114, 13)
        Me.Label23.TabIndex = 58
        Me.Label23.Text = "Search range Hz [gon]"
        '
        'TextBox_ATR_SearchRange_Hz
        '
        Me.TextBox_ATR_SearchRange_Hz.Location = New System.Drawing.Point(120, 20)
        Me.TextBox_ATR_SearchRange_Hz.Name = "TextBox_ATR_SearchRange_Hz"
        Me.TextBox_ATR_SearchRange_Hz.Size = New System.Drawing.Size(82, 20)
        Me.TextBox_ATR_SearchRange_Hz.TabIndex = 57
        '
        'TextBox_ATR_SearchRange_V
        '
        Me.TextBox_ATR_SearchRange_V.Location = New System.Drawing.Point(120, 46)
        Me.TextBox_ATR_SearchRange_V.Name = "TextBox_ATR_SearchRange_V"
        Me.TextBox_ATR_SearchRange_V.Size = New System.Drawing.Size(82, 20)
        Me.TextBox_ATR_SearchRange_V.TabIndex = 56
        '
        'Button_FineAdjust
        '
        Me.Button_FineAdjust.Location = New System.Drawing.Point(209, 30)
        Me.Button_FineAdjust.Name = "Button_FineAdjust"
        Me.Button_FineAdjust.Size = New System.Drawing.Size(75, 23)
        Me.Button_FineAdjust.TabIndex = 4
        Me.Button_FineAdjust.Text = "Fine Adjust"
        Me.Button_FineAdjust.UseVisualStyleBackColor = True
        '
        'gb_zenitRange
        '
        Me.gb_zenitRange.Controls.Add(Me.bu_def_vertical_range)
        Me.gb_zenitRange.Controls.Add(Me.La_vertical_range_face2)
        Me.gb_zenitRange.Controls.Add(Me.La_vertical_range_face1)
        Me.gb_zenitRange.Controls.Add(Me.tb_verticale_range_lage1)
        Me.gb_zenitRange.Controls.Add(Me.tb_verticale_range_lage2)
        Me.gb_zenitRange.Controls.Add(Me.rb_vertical_range_user_define)
        Me.gb_zenitRange.Controls.Add(Me.rb_vertical_range_camera)
        Me.gb_zenitRange.Controls.Add(Me.rb_vertical_range_free)
        Me.gb_zenitRange.Location = New System.Drawing.Point(9, 9)
        Me.gb_zenitRange.Name = "gb_zenitRange"
        Me.gb_zenitRange.Size = New System.Drawing.Size(240, 94)
        Me.gb_zenitRange.TabIndex = 54
        Me.gb_zenitRange.TabStop = False
        Me.gb_zenitRange.Text = "Vertical Range"
        '
        'bu_def_vertical_range
        '
        Me.bu_def_vertical_range.Location = New System.Drawing.Point(130, 67)
        Me.bu_def_vertical_range.Name = "bu_def_vertical_range"
        Me.bu_def_vertical_range.Size = New System.Drawing.Size(81, 22)
        Me.bu_def_vertical_range.TabIndex = 28
        Me.bu_def_vertical_range.Text = "Change"
        Me.bu_def_vertical_range.UseVisualStyleBackColor = True
        '
        'La_vertical_range_face2
        '
        Me.La_vertical_range_face2.AutoSize = True
        Me.La_vertical_range_face2.Location = New System.Drawing.Point(106, 44)
        Me.La_vertical_range_face2.Name = "La_vertical_range_face2"
        Me.La_vertical_range_face2.Size = New System.Drawing.Size(40, 13)
        Me.La_vertical_range_face2.TabIndex = 27
        Me.La_vertical_range_face2.Text = "Face 2"
        '
        'La_vertical_range_face1
        '
        Me.La_vertical_range_face1.AutoSize = True
        Me.La_vertical_range_face1.Location = New System.Drawing.Point(106, 17)
        Me.La_vertical_range_face1.Name = "La_vertical_range_face1"
        Me.La_vertical_range_face1.Size = New System.Drawing.Size(40, 13)
        Me.La_vertical_range_face1.TabIndex = 26
        Me.La_vertical_range_face1.Text = "Face 1"
        '
        'tb_verticale_range_lage1
        '
        Me.tb_verticale_range_lage1.Location = New System.Drawing.Point(152, 15)
        Me.tb_verticale_range_lage1.Name = "tb_verticale_range_lage1"
        Me.tb_verticale_range_lage1.Size = New System.Drawing.Size(82, 20)
        Me.tb_verticale_range_lage1.TabIndex = 25
        '
        'tb_verticale_range_lage2
        '
        Me.tb_verticale_range_lage2.Location = New System.Drawing.Point(152, 41)
        Me.tb_verticale_range_lage2.Name = "tb_verticale_range_lage2"
        Me.tb_verticale_range_lage2.Size = New System.Drawing.Size(82, 20)
        Me.tb_verticale_range_lage2.TabIndex = 24
        '
        'rb_vertical_range_user_define
        '
        Me.rb_vertical_range_user_define.AutoSize = True
        Me.rb_vertical_range_user_define.Location = New System.Drawing.Point(6, 66)
        Me.rb_vertical_range_user_define.Name = "rb_vertical_range_user_define"
        Me.rb_vertical_range_user_define.Size = New System.Drawing.Size(79, 17)
        Me.rb_vertical_range_user_define.TabIndex = 2
        Me.rb_vertical_range_user_define.TabStop = True
        Me.rb_vertical_range_user_define.Text = "User define"
        Me.rb_vertical_range_user_define.UseVisualStyleBackColor = True
        '
        'rb_vertical_range_camera
        '
        Me.rb_vertical_range_camera.AutoSize = True
        Me.rb_vertical_range_camera.Location = New System.Drawing.Point(7, 43)
        Me.rb_vertical_range_camera.Name = "rb_vertical_range_camera"
        Me.rb_vertical_range_camera.Size = New System.Drawing.Size(61, 17)
        Me.rb_vertical_range_camera.TabIndex = 1
        Me.rb_vertical_range_camera.TabStop = True
        Me.rb_vertical_range_camera.Text = "Camera"
        Me.rb_vertical_range_camera.UseVisualStyleBackColor = True
        '
        'rb_vertical_range_free
        '
        Me.rb_vertical_range_free.AutoSize = True
        Me.rb_vertical_range_free.Location = New System.Drawing.Point(7, 20)
        Me.rb_vertical_range_free.Name = "rb_vertical_range_free"
        Me.rb_vertical_range_free.Size = New System.Drawing.Size(97, 17)
        Me.rb_vertical_range_free.TabIndex = 0
        Me.rb_vertical_range_free.TabStop = True
        Me.rb_vertical_range_free.Text = "Without Range"
        Me.rb_vertical_range_free.UseVisualStyleBackColor = True
        '
        'gb_edm_mode
        '
        Me.gb_edm_mode.Controls.Add(Me.rb_edmMode_RL)
        Me.gb_edm_mode.Controls.Add(Me.rb_edmMode_IR)
        Me.gb_edm_mode.Location = New System.Drawing.Point(113, 359)
        Me.gb_edm_mode.Name = "gb_edm_mode"
        Me.gb_edm_mode.Size = New System.Drawing.Size(83, 82)
        Me.gb_edm_mode.TabIndex = 53
        Me.gb_edm_mode.TabStop = False
        Me.gb_edm_mode.Text = "EDM Mode"
        '
        'rb_edmMode_RL
        '
        Me.rb_edmMode_RL.AutoSize = True
        Me.rb_edmMode_RL.Location = New System.Drawing.Point(6, 44)
        Me.rb_edmMode_RL.Name = "rb_edmMode_RL"
        Me.rb_edmMode_RL.Size = New System.Drawing.Size(39, 17)
        Me.rb_edmMode_RL.TabIndex = 1
        Me.rb_edmMode_RL.TabStop = True
        Me.rb_edmMode_RL.Text = "RL"
        Me.rb_edmMode_RL.UseVisualStyleBackColor = True
        '
        'rb_edmMode_IR
        '
        Me.rb_edmMode_IR.AutoSize = True
        Me.rb_edmMode_IR.Location = New System.Drawing.Point(7, 20)
        Me.rb_edmMode_IR.Name = "rb_edmMode_IR"
        Me.rb_edmMode_IR.Size = New System.Drawing.Size(36, 17)
        Me.rb_edmMode_IR.TabIndex = 0
        Me.rb_edmMode_IR.TabStop = True
        Me.rb_edmMode_IR.Text = "IR"
        Me.rb_edmMode_IR.UseVisualStyleBackColor = True
        '
        'Tab_TachyMove
        '
        Me.Tab_TachyMove.Controls.Add(Me.gb_lagewechsel)
        Me.Tab_TachyMove.Controls.Add(Me.gb_joystick)
        Me.Tab_TachyMove.Controls.Add(Me.gb_moveabsolute)
        Me.Tab_TachyMove.Location = New System.Drawing.Point(4, 22)
        Me.Tab_TachyMove.Name = "Tab_TachyMove"
        Me.Tab_TachyMove.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_TachyMove.Size = New System.Drawing.Size(350, 447)
        Me.Tab_TachyMove.TabIndex = 3
        Me.Tab_TachyMove.Text = "Move"
        Me.Tab_TachyMove.UseVisualStyleBackColor = True
        '
        'gb_lagewechsel
        '
        Me.gb_lagewechsel.Controls.Add(Me.ch_precisionOn_2)
        Me.gb_lagewechsel.Controls.Add(Me.btChangeFace)
        Me.gb_lagewechsel.Location = New System.Drawing.Point(6, 395)
        Me.gb_lagewechsel.Name = "gb_lagewechsel"
        Me.gb_lagewechsel.Size = New System.Drawing.Size(279, 46)
        Me.gb_lagewechsel.TabIndex = 52
        Me.gb_lagewechsel.TabStop = False
        Me.gb_lagewechsel.Text = "Change Face"
        '
        'ch_precisionOn_2
        '
        Me.ch_precisionOn_2.AutoSize = True
        Me.ch_precisionOn_2.Location = New System.Drawing.Point(108, 21)
        Me.ch_precisionOn_2.Name = "ch_precisionOn_2"
        Me.ch_precisionOn_2.Size = New System.Drawing.Size(86, 17)
        Me.ch_precisionOn_2.TabIndex = 52
        Me.ch_precisionOn_2.Text = "Precision On"
        Me.ch_precisionOn_2.UseVisualStyleBackColor = True
        '
        'btChangeFace
        '
        Me.btChangeFace.Location = New System.Drawing.Point(20, 17)
        Me.btChangeFace.Name = "btChangeFace"
        Me.btChangeFace.Size = New System.Drawing.Size(82, 23)
        Me.btChangeFace.TabIndex = 51
        Me.btChangeFace.Text = "Change Face"
        Me.btChangeFace.UseVisualStyleBackColor = True
        '
        'gb_joystick
        '
        Me.gb_joystick.Controls.Add(Me.bu_stopmove)
        Me.gb_joystick.Controls.Add(Me.GroupBox16)
        Me.gb_joystick.Controls.Add(Me.cbVelocityEqual)
        Me.gb_joystick.Controls.Add(Me.GroupBox17)
        Me.gb_joystick.Controls.Add(Me.btdownright)
        Me.gb_joystick.Controls.Add(Me.btupright)
        Me.gb_joystick.Controls.Add(Me.btdown)
        Me.gb_joystick.Controls.Add(Me.btupleft)
        Me.gb_joystick.Controls.Add(Me.btdownleft)
        Me.gb_joystick.Controls.Add(Me.btup)
        Me.gb_joystick.Controls.Add(Me.btright)
        Me.gb_joystick.Controls.Add(Me.btleft)
        Me.gb_joystick.Location = New System.Drawing.Point(6, 118)
        Me.gb_joystick.Name = "gb_joystick"
        Me.gb_joystick.Size = New System.Drawing.Size(327, 276)
        Me.gb_joystick.TabIndex = 51
        Me.gb_joystick.TabStop = False
        Me.gb_joystick.Text = "Joystick"
        '
        'bu_stopmove
        '
        Me.bu_stopmove.Location = New System.Drawing.Point(175, 229)
        Me.bu_stopmove.Name = "bu_stopmove"
        Me.bu_stopmove.Size = New System.Drawing.Size(75, 23)
        Me.bu_stopmove.TabIndex = 100
        Me.bu_stopmove.Text = "Stop"
        Me.bu_stopmove.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.lbVelocityHz)
        Me.GroupBox16.Controls.Add(Me.Label10)
        Me.GroupBox16.Controls.Add(Me.Label13)
        Me.GroupBox16.Controls.Add(Me.trbVelocityHz)
        Me.GroupBox16.Location = New System.Drawing.Point(4, 18)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(310, 79)
        Me.GroupBox16.TabIndex = 97
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Hz"
        '
        'lbVelocityHz
        '
        Me.lbVelocityHz.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbVelocityHz.Location = New System.Drawing.Point(14, 53)
        Me.lbVelocityHz.Name = "lbVelocityHz"
        Me.lbVelocityHz.Size = New System.Drawing.Size(280, 17)
        Me.lbVelocityHz.TabIndex = 57
        Me.lbVelocityHz.Text = "V(Hz) = 8.0689 gon/s"
        Me.lbVelocityHz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(244, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 56
        Me.Label10.Text = "50 gon/s"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(14, 40)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 13)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "0 gon/s"
        '
        'trbVelocityHz
        '
        Me.trbVelocityHz.Location = New System.Drawing.Point(6, 19)
        Me.trbVelocityHz.Maximum = 1000
        Me.trbVelocityHz.Name = "trbVelocityHz"
        Me.trbVelocityHz.Size = New System.Drawing.Size(295, 45)
        Me.trbVelocityHz.TabIndex = 54
        Me.trbVelocityHz.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'cbVelocityEqual
        '
        Me.cbVelocityEqual.AutoSize = True
        Me.cbVelocityEqual.Checked = True
        Me.cbVelocityEqual.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbVelocityEqual.Location = New System.Drawing.Point(174, 206)
        Me.cbVelocityEqual.Name = "cbVelocityEqual"
        Me.cbVelocityEqual.Size = New System.Drawing.Size(89, 17)
        Me.cbVelocityEqual.TabIndex = 99
        Me.cbVelocityEqual.Text = "V(Hz) = V(Vz)"
        Me.cbVelocityEqual.UseVisualStyleBackColor = True
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.lbVelocityVz)
        Me.GroupBox17.Controls.Add(Me.Label14)
        Me.GroupBox17.Controls.Add(Me.Label15)
        Me.GroupBox17.Controls.Add(Me.trbVelocityVz)
        Me.GroupBox17.Location = New System.Drawing.Point(4, 103)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(310, 79)
        Me.GroupBox17.TabIndex = 98
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Vz"
        '
        'lbVelocityVz
        '
        Me.lbVelocityVz.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbVelocityVz.Location = New System.Drawing.Point(14, 53)
        Me.lbVelocityVz.Name = "lbVelocityVz"
        Me.lbVelocityVz.Size = New System.Drawing.Size(280, 17)
        Me.lbVelocityVz.TabIndex = 57
        Me.lbVelocityVz.Text = "V(Vz) = 8.0689 gon/s"
        Me.lbVelocityVz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(244, 40)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 56
        Me.Label14.Text = "50 gon/s"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(14, 40)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 13)
        Me.Label15.TabIndex = 55
        Me.Label15.Text = "0 gon/s"
        '
        'trbVelocityVz
        '
        Me.trbVelocityVz.Location = New System.Drawing.Point(6, 19)
        Me.trbVelocityVz.Maximum = 1000
        Me.trbVelocityVz.Name = "trbVelocityVz"
        Me.trbVelocityVz.Size = New System.Drawing.Size(295, 45)
        Me.trbVelocityVz.TabIndex = 54
        Me.trbVelocityVz.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btdownright
        '
        Me.btdownright.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btdownright.Location = New System.Drawing.Point(118, 245)
        Me.btdownright.Name = "btdownright"
        Me.btdownright.Size = New System.Drawing.Size(28, 23)
        Me.btdownright.TabIndex = 96
        Me.btdownright.UseVisualStyleBackColor = True
        '
        'btupright
        '
        Me.btupright.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btupright.Location = New System.Drawing.Point(118, 188)
        Me.btupright.Name = "btupright"
        Me.btupright.Size = New System.Drawing.Size(28, 23)
        Me.btupright.TabIndex = 91
        Me.btupright.UseVisualStyleBackColor = True
        '
        'btdown
        '
        Me.btdown.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btdown.Location = New System.Drawing.Point(85, 245)
        Me.btdown.Name = "btdown"
        Me.btdown.Size = New System.Drawing.Size(28, 23)
        Me.btdown.TabIndex = 95
        Me.btdown.UseVisualStyleBackColor = True
        '
        'btupleft
        '
        Me.btupleft.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btupleft.Location = New System.Drawing.Point(51, 188)
        Me.btupleft.Name = "btupleft"
        Me.btupleft.Size = New System.Drawing.Size(28, 23)
        Me.btupleft.TabIndex = 89
        Me.btupleft.UseVisualStyleBackColor = True
        '
        'btdownleft
        '
        Me.btdownleft.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btdownleft.Location = New System.Drawing.Point(51, 245)
        Me.btdownleft.Name = "btdownleft"
        Me.btdownleft.Size = New System.Drawing.Size(28, 23)
        Me.btdownleft.TabIndex = 94
        Me.btdownleft.UseVisualStyleBackColor = True
        '
        'btup
        '
        Me.btup.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btup.Location = New System.Drawing.Point(84, 188)
        Me.btup.Name = "btup"
        Me.btup.Size = New System.Drawing.Size(28, 23)
        Me.btup.TabIndex = 90
        Me.btup.UseVisualStyleBackColor = True
        '
        'btright
        '
        Me.btright.BackColor = System.Drawing.Color.Transparent
        Me.btright.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btright.Location = New System.Drawing.Point(118, 216)
        Me.btright.Name = "btright"
        Me.btright.Size = New System.Drawing.Size(28, 23)
        Me.btright.TabIndex = 93
        Me.btright.UseVisualStyleBackColor = False
        '
        'btleft
        '
        Me.btleft.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btleft.Location = New System.Drawing.Point(51, 216)
        Me.btleft.Name = "btleft"
        Me.btleft.Size = New System.Drawing.Size(28, 23)
        Me.btleft.TabIndex = 92
        Me.btleft.UseVisualStyleBackColor = True
        '
        'gb_moveabsolute
        '
        Me.gb_moveabsolute.BackColor = System.Drawing.SystemColors.Control
        Me.gb_moveabsolute.Controls.Add(Me.ch_precisionOn_1)
        Me.gb_moveabsolute.Controls.Add(Me.Label16)
        Me.gb_moveabsolute.Controls.Add(Me.Label5)
        Me.gb_moveabsolute.Controls.Add(Me.btDriveAbs)
        Me.gb_moveabsolute.Controls.Add(Me.tbHzAbsolut)
        Me.gb_moveabsolute.Controls.Add(Me.tbVzAbsolut)
        Me.gb_moveabsolute.Location = New System.Drawing.Point(6, 7)
        Me.gb_moveabsolute.Name = "gb_moveabsolute"
        Me.gb_moveabsolute.Size = New System.Drawing.Size(279, 102)
        Me.gb_moveabsolute.TabIndex = 49
        Me.gb_moveabsolute.TabStop = False
        Me.gb_moveabsolute.Text = "Moving Absolute"
        '
        'ch_precisionOn_1
        '
        Me.ch_precisionOn_1.AutoSize = True
        Me.ch_precisionOn_1.Location = New System.Drawing.Point(183, 37)
        Me.ch_precisionOn_1.Name = "ch_precisionOn_1"
        Me.ch_precisionOn_1.Size = New System.Drawing.Size(86, 17)
        Me.ch_precisionOn_1.TabIndex = 27
        Me.ch_precisionOn_1.Text = "Precision On"
        Me.ch_precisionOn_1.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 13)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "Vertical Angle:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Horizontal Angle:"
        '
        'btDriveAbs
        '
        Me.btDriveAbs.Location = New System.Drawing.Point(6, 71)
        Me.btDriveAbs.Name = "btDriveAbs"
        Me.btDriveAbs.Size = New System.Drawing.Size(171, 23)
        Me.btDriveAbs.TabIndex = 24
        Me.btDriveAbs.Text = "Drive"
        Me.btDriveAbs.UseVisualStyleBackColor = True
        '
        'tbHzAbsolut
        '
        Me.tbHzAbsolut.Location = New System.Drawing.Point(95, 19)
        Me.tbHzAbsolut.Name = "tbHzAbsolut"
        Me.tbHzAbsolut.Size = New System.Drawing.Size(82, 20)
        Me.tbHzAbsolut.TabIndex = 22
        '
        'tbVzAbsolut
        '
        Me.tbVzAbsolut.Location = New System.Drawing.Point(95, 45)
        Me.tbVzAbsolut.Name = "tbVzAbsolut"
        Me.tbVzAbsolut.Size = New System.Drawing.Size(82, 20)
        Me.tbVzAbsolut.TabIndex = 23
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox_camera_image)
        Me.TabPage4.Controls.Add(Me.GroupBox_camera_SizeLiveModus)
        Me.TabPage4.Controls.Add(Me.GroupBox_CameraTiming)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(350, 447)
        Me.TabPage4.TabIndex = 6
        Me.TabPage4.Text = "Camera"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox_camera_image
        '
        Me.GroupBox_camera_image.Controls.Add(Me.CheckBox4)
        Me.GroupBox_camera_image.Controls.Add(Me.CheckBox5)
        Me.GroupBox_camera_image.Controls.Add(Me.CheckBox6)
        Me.GroupBox_camera_image.Location = New System.Drawing.Point(6, 249)
        Me.GroupBox_camera_image.Name = "GroupBox_camera_image"
        Me.GroupBox_camera_image.Size = New System.Drawing.Size(200, 90)
        Me.GroupBox_camera_image.TabIndex = 13
        Me.GroupBox_camera_image.TabStop = False
        Me.GroupBox_camera_image.Text = "Image"
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(7, 67)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(155, 17)
        Me.CheckBox4.TabIndex = 2
        Me.CheckBox4.Text = "AWB (Auto White Balance)"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(7, 43)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(140, 17)
        Me.CheckBox5.TabIndex = 1
        Me.CheckBox5.Text = "AGC (Auto Gain Control)"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(7, 20)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(162, 17)
        Me.CheckBox6.TabIndex = 0
        Me.CheckBox6.Text = "AES (Auto Exposure Shutter)"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'GroupBox_camera_SizeLiveModus
        '
        Me.GroupBox_camera_SizeLiveModus.Controls.Add(Me.CheckBox_SameResolution)
        Me.GroupBox_camera_SizeLiveModus.Controls.Add(Me.Label20)
        Me.GroupBox_camera_SizeLiveModus.Controls.Add(Me.Label19)
        Me.GroupBox_camera_SizeLiveModus.Controls.Add(Me.ComboBox_Resolution_Single)
        Me.GroupBox_camera_SizeLiveModus.Controls.Add(Me.ComboBox_Resolution_Live)
        Me.GroupBox_camera_SizeLiveModus.Location = New System.Drawing.Point(6, 345)
        Me.GroupBox_camera_SizeLiveModus.Name = "GroupBox_camera_SizeLiveModus"
        Me.GroupBox_camera_SizeLiveModus.Size = New System.Drawing.Size(200, 96)
        Me.GroupBox_camera_SizeLiveModus.TabIndex = 12
        Me.GroupBox_camera_SizeLiveModus.TabStop = False
        Me.GroupBox_camera_SizeLiveModus.Text = "Resolution"
        '
        'CheckBox_SameResolution
        '
        Me.CheckBox_SameResolution.AutoSize = True
        Me.CheckBox_SameResolution.Location = New System.Drawing.Point(80, 47)
        Me.CheckBox_SameResolution.Name = "CheckBox_SameResolution"
        Me.CheckBox_SameResolution.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox_SameResolution.TabIndex = 3
        Me.CheckBox_SameResolution.Text = "Same Resolution"
        Me.CheckBox_SameResolution.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 71)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 13)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "SingleImage"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 22)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 13)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "Livemodus"
        '
        'ComboBox_Resolution_Single
        '
        Me.ComboBox_Resolution_Single.FormattingEnabled = True
        Me.ComboBox_Resolution_Single.Location = New System.Drawing.Point(80, 68)
        Me.ComboBox_Resolution_Single.Name = "ComboBox_Resolution_Single"
        Me.ComboBox_Resolution_Single.Size = New System.Drawing.Size(107, 21)
        Me.ComboBox_Resolution_Single.TabIndex = 0
        '
        'ComboBox_Resolution_Live
        '
        Me.ComboBox_Resolution_Live.FormattingEnabled = True
        Me.ComboBox_Resolution_Live.Location = New System.Drawing.Point(80, 22)
        Me.ComboBox_Resolution_Live.Name = "ComboBox_Resolution_Live"
        Me.ComboBox_Resolution_Live.Size = New System.Drawing.Size(107, 21)
        Me.ComboBox_Resolution_Live.TabIndex = 0
        '
        'GroupBox_CameraTiming
        '
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_ExposureTime_Max)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_ExposureTime_Min)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_FrameRate_Max)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_FrameRate_Min)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_PixelClock_Max)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_PixelClock_Min)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_ExposureTime_act)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_FrameRate_act)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label_PixelClock_act)
        Me.GroupBox_CameraTiming.Controls.Add(Me.TrackBar_ExposureTime)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label28)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label29)
        Me.GroupBox_CameraTiming.Controls.Add(Me.Label30)
        Me.GroupBox_CameraTiming.Controls.Add(Me.TrackBar_FrameRate)
        Me.GroupBox_CameraTiming.Controls.Add(Me.TrackBar_PixelClock)
        Me.GroupBox_CameraTiming.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox_CameraTiming.Name = "GroupBox_CameraTiming"
        Me.GroupBox_CameraTiming.Size = New System.Drawing.Size(339, 237)
        Me.GroupBox_CameraTiming.TabIndex = 9
        Me.GroupBox_CameraTiming.TabStop = False
        Me.GroupBox_CameraTiming.Text = "Timing"
        '
        'Label_ExposureTime_Max
        '
        Me.Label_ExposureTime_Max.AutoSize = True
        Me.Label_ExposureTime_Max.Location = New System.Drawing.Point(276, 207)
        Me.Label_ExposureTime_Max.Name = "Label_ExposureTime_Max"
        Me.Label_ExposureTime_Max.Size = New System.Drawing.Size(13, 13)
        Me.Label_ExposureTime_Max.TabIndex = 14
        Me.Label_ExposureTime_Max.Text = "0"
        '
        'Label_ExposureTime_Min
        '
        Me.Label_ExposureTime_Min.AutoSize = True
        Me.Label_ExposureTime_Min.Location = New System.Drawing.Point(9, 207)
        Me.Label_ExposureTime_Min.Name = "Label_ExposureTime_Min"
        Me.Label_ExposureTime_Min.Size = New System.Drawing.Size(13, 13)
        Me.Label_ExposureTime_Min.TabIndex = 13
        Me.Label_ExposureTime_Min.Text = "0"
        '
        'Label_FrameRate_Max
        '
        Me.Label_FrameRate_Max.AutoSize = True
        Me.Label_FrameRate_Max.Location = New System.Drawing.Point(276, 141)
        Me.Label_FrameRate_Max.Name = "Label_FrameRate_Max"
        Me.Label_FrameRate_Max.Size = New System.Drawing.Size(13, 13)
        Me.Label_FrameRate_Max.TabIndex = 12
        Me.Label_FrameRate_Max.Text = "0"
        '
        'Label_FrameRate_Min
        '
        Me.Label_FrameRate_Min.AutoSize = True
        Me.Label_FrameRate_Min.Location = New System.Drawing.Point(9, 141)
        Me.Label_FrameRate_Min.Name = "Label_FrameRate_Min"
        Me.Label_FrameRate_Min.Size = New System.Drawing.Size(13, 13)
        Me.Label_FrameRate_Min.TabIndex = 11
        Me.Label_FrameRate_Min.Text = "0"
        '
        'Label_PixelClock_Max
        '
        Me.Label_PixelClock_Max.AutoSize = True
        Me.Label_PixelClock_Max.Location = New System.Drawing.Point(278, 73)
        Me.Label_PixelClock_Max.Name = "Label_PixelClock_Max"
        Me.Label_PixelClock_Max.Size = New System.Drawing.Size(13, 13)
        Me.Label_PixelClock_Max.TabIndex = 10
        Me.Label_PixelClock_Max.Text = "0"
        '
        'Label_PixelClock_Min
        '
        Me.Label_PixelClock_Min.AutoSize = True
        Me.Label_PixelClock_Min.Location = New System.Drawing.Point(9, 73)
        Me.Label_PixelClock_Min.Name = "Label_PixelClock_Min"
        Me.Label_PixelClock_Min.Size = New System.Drawing.Size(13, 13)
        Me.Label_PixelClock_Min.TabIndex = 9
        Me.Label_PixelClock_Min.Text = "0"
        '
        'Label_ExposureTime_act
        '
        Me.Label_ExposureTime_act.AutoSize = True
        Me.Label_ExposureTime_act.Location = New System.Drawing.Point(297, 187)
        Me.Label_ExposureTime_act.Name = "Label_ExposureTime_act"
        Me.Label_ExposureTime_act.Size = New System.Drawing.Size(25, 13)
        Me.Label_ExposureTime_act.TabIndex = 8
        Me.Label_ExposureTime_act.Text = "100"
        '
        'Label_FrameRate_act
        '
        Me.Label_FrameRate_act.AutoSize = True
        Me.Label_FrameRate_act.Location = New System.Drawing.Point(297, 121)
        Me.Label_FrameRate_act.Name = "Label_FrameRate_act"
        Me.Label_FrameRate_act.Size = New System.Drawing.Size(25, 13)
        Me.Label_FrameRate_act.TabIndex = 7
        Me.Label_FrameRate_act.Text = "100"
        '
        'Label_PixelClock_act
        '
        Me.Label_PixelClock_act.AutoSize = True
        Me.Label_PixelClock_act.Location = New System.Drawing.Point(297, 49)
        Me.Label_PixelClock_act.Name = "Label_PixelClock_act"
        Me.Label_PixelClock_act.Size = New System.Drawing.Size(25, 13)
        Me.Label_PixelClock_act.TabIndex = 6
        Me.Label_PixelClock_act.Text = "100"
        '
        'TrackBar_ExposureTime
        '
        Me.TrackBar_ExposureTime.Location = New System.Drawing.Point(9, 178)
        Me.TrackBar_ExposureTime.Name = "TrackBar_ExposureTime"
        Me.TrackBar_ExposureTime.Size = New System.Drawing.Size(280, 45)
        Me.TrackBar_ExposureTime.TabIndex = 5
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 162)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(95, 13)
        Me.Label28.TabIndex = 4
        Me.Label28.Text = "Exposure time [ms]"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 96)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(80, 13)
        Me.Label29.TabIndex = 3
        Me.Label29.Text = "Frame rate [fps]"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 29)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(89, 13)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Pixel clock [MHz]"
        '
        'TrackBar_FrameRate
        '
        Me.TrackBar_FrameRate.Location = New System.Drawing.Point(9, 112)
        Me.TrackBar_FrameRate.Name = "TrackBar_FrameRate"
        Me.TrackBar_FrameRate.Size = New System.Drawing.Size(280, 45)
        Me.TrackBar_FrameRate.TabIndex = 1
        '
        'TrackBar_PixelClock
        '
        Me.TrackBar_PixelClock.Location = New System.Drawing.Point(9, 45)
        Me.TrackBar_PixelClock.Name = "TrackBar_PixelClock"
        Me.TrackBar_PixelClock.Size = New System.Drawing.Size(282, 45)
        Me.TrackBar_PixelClock.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox_SK_Solve)
        Me.TabPage1.Controls.Add(Me.CheckBox_SaveImage_crosshair)
        Me.TabPage1.Controls.Add(Me.ComboBox_crosshair_types)
        Me.TabPage1.Controls.Add(Me.GroupBox_Field_of_View)
        Me.TabPage1.Controls.Add(Me.Button_Crosshair)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(350, 447)
        Me.TabPage1.TabIndex = 7
        Me.TabPage1.Text = "Self Calibration"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox_SK_Solve
        '
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label36)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label37)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label_SK_Error_stddev)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label_SK_Error_mean)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label40)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label100)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label35)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label34)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label_SK_Error_max)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label_SK_Error_min)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label33)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Label32)
        Me.GroupBox_SK_Solve.Controls.Add(Me.Button_Solve_SK)
        Me.GroupBox_SK_Solve.Location = New System.Drawing.Point(3, 380)
        Me.GroupBox_SK_Solve.Name = "GroupBox_SK_Solve"
        Me.GroupBox_SK_Solve.Size = New System.Drawing.Size(341, 64)
        Me.GroupBox_SK_Solve.TabIndex = 13
        Me.GroupBox_SK_Solve.TabStop = False
        Me.GroupBox_SK_Solve.Text = "Solve Self Calibration"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(300, 38)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(33, 13)
        Me.Label36.TabIndex = 12
        Me.Label36.Text = "mgon"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(300, 17)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(33, 13)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "mgon"
        '
        'Label_SK_Error_stddev
        '
        Me.Label_SK_Error_stddev.AutoSize = True
        Me.Label_SK_Error_stddev.Location = New System.Drawing.Point(273, 38)
        Me.Label_SK_Error_stddev.Name = "Label_SK_Error_stddev"
        Me.Label_SK_Error_stddev.Size = New System.Drawing.Size(28, 13)
        Me.Label_SK_Error_stddev.TabIndex = 10
        Me.Label_SK_Error_stddev.Text = "0.00"
        '
        'Label_SK_Error_mean
        '
        Me.Label_SK_Error_mean.AutoSize = True
        Me.Label_SK_Error_mean.Location = New System.Drawing.Point(273, 17)
        Me.Label_SK_Error_mean.Name = "Label_SK_Error_mean"
        Me.Label_SK_Error_mean.Size = New System.Drawing.Size(28, 13)
        Me.Label_SK_Error_mean.TabIndex = 9
        Me.Label_SK_Error_mean.Text = "0.00"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(224, 38)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(50, 13)
        Me.Label40.TabIndex = 8
        Me.Label40.Text = "Std.dev.:"
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(224, 17)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(37, 13)
        Me.Label100.TabIndex = 7
        Me.Label100.Text = "Mean:"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(180, 38)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(33, 13)
        Me.Label35.TabIndex = 6
        Me.Label35.Text = "mgon"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(180, 17)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(33, 13)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "mgon"
        '
        'Label_SK_Error_max
        '
        Me.Label_SK_Error_max.AutoSize = True
        Me.Label_SK_Error_max.Location = New System.Drawing.Point(153, 38)
        Me.Label_SK_Error_max.Name = "Label_SK_Error_max"
        Me.Label_SK_Error_max.Size = New System.Drawing.Size(28, 13)
        Me.Label_SK_Error_max.TabIndex = 4
        Me.Label_SK_Error_max.Text = "0.00"
        '
        'Label_SK_Error_min
        '
        Me.Label_SK_Error_min.AutoSize = True
        Me.Label_SK_Error_min.Location = New System.Drawing.Point(153, 17)
        Me.Label_SK_Error_min.Name = "Label_SK_Error_min"
        Me.Label_SK_Error_min.Size = New System.Drawing.Size(28, 13)
        Me.Label_SK_Error_min.TabIndex = 3
        Me.Label_SK_Error_min.Text = "0.00"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(123, 38)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(30, 13)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "Max:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(123, 17)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(27, 13)
        Me.Label32.TabIndex = 1
        Me.Label32.Text = "Min:"
        '
        'Button_Solve_SK
        '
        Me.Button_Solve_SK.Location = New System.Drawing.Point(12, 24)
        Me.Button_Solve_SK.Name = "Button_Solve_SK"
        Me.Button_Solve_SK.Size = New System.Drawing.Size(75, 23)
        Me.Button_Solve_SK.TabIndex = 0
        Me.Button_Solve_SK.Text = "Solve"
        Me.Button_Solve_SK.UseVisualStyleBackColor = True
        '
        'CheckBox_SaveImage_crosshair
        '
        Me.CheckBox_SaveImage_crosshair.AutoSize = True
        Me.CheckBox_SaveImage_crosshair.Location = New System.Drawing.Point(235, 14)
        Me.CheckBox_SaveImage_crosshair.Name = "CheckBox_SaveImage_crosshair"
        Me.CheckBox_SaveImage_crosshair.Size = New System.Drawing.Size(83, 17)
        Me.CheckBox_SaveImage_crosshair.TabIndex = 12
        Me.CheckBox_SaveImage_crosshair.Text = "Save Image"
        Me.CheckBox_SaveImage_crosshair.UseVisualStyleBackColor = True
        '
        'ComboBox_crosshair_types
        '
        Me.ComboBox_crosshair_types.FormattingEnabled = True
        Me.ComboBox_crosshair_types.Location = New System.Drawing.Point(14, 12)
        Me.ComboBox_crosshair_types.Name = "ComboBox_crosshair_types"
        Me.ComboBox_crosshair_types.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_crosshair_types.TabIndex = 2
        '
        'GroupBox_Field_of_View
        '
        Me.GroupBox_Field_of_View.Controls.Add(Me.Label_Status_Sk)
        Me.GroupBox_Field_of_View.Controls.Add(Me.Panel_Status_Sk)
        Me.GroupBox_Field_of_View.Controls.Add(Me.Label39)
        Me.GroupBox_Field_of_View.Controls.Add(Me.ComboBox_controlpoints)
        Me.GroupBox_Field_of_View.Controls.Add(Me.Label38)
        Me.GroupBox_Field_of_View.Controls.Add(Me.GroupBox_SKTarget2)
        Me.GroupBox_Field_of_View.Controls.Add(Me.GroupBox_SKTarget1)
        Me.GroupBox_Field_of_View.Controls.Add(Me.ComboBox_Sk_typ)
        Me.GroupBox_Field_of_View.Controls.Add(Me.CheckBox_SaveErrorImages_self_calibration)
        Me.GroupBox_Field_of_View.Controls.Add(Me.CheckBox_SaveImage_selbstkalibrierung)
        Me.GroupBox_Field_of_View.Controls.Add(Me.Button_SelfCalibrationStart)
        Me.GroupBox_Field_of_View.Location = New System.Drawing.Point(3, 37)
        Me.GroupBox_Field_of_View.Name = "GroupBox_Field_of_View"
        Me.GroupBox_Field_of_View.Size = New System.Drawing.Size(341, 340)
        Me.GroupBox_Field_of_View.TabIndex = 1
        Me.GroupBox_Field_of_View.TabStop = False
        Me.GroupBox_Field_of_View.Text = "Field of View"
        '
        'Label_Status_Sk
        '
        Me.Label_Status_Sk.AutoSize = True
        Me.Label_Status_Sk.Location = New System.Drawing.Point(172, 308)
        Me.Label_Status_Sk.Name = "Label_Status_Sk"
        Me.Label_Status_Sk.Size = New System.Drawing.Size(58, 13)
        Me.Label_Status_Sk.TabIndex = 25
        Me.Label_Status_Sk.Text = "Not Ready"
        '
        'Panel_Status_Sk
        '
        Me.Panel_Status_Sk.BackColor = System.Drawing.Color.Red
        Me.Panel_Status_Sk.Location = New System.Drawing.Point(148, 306)
        Me.Panel_Status_Sk.Name = "Panel_Status_Sk"
        Me.Panel_Status_Sk.Size = New System.Drawing.Size(20, 20)
        Me.Panel_Status_Sk.TabIndex = 24
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(101, 15)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(52, 13)
        Me.Label39.TabIndex = 18
        Me.Label39.Text = "Controlpt."
        '
        'ComboBox_controlpoints
        '
        Me.ComboBox_controlpoints.FormattingEnabled = True
        Me.ComboBox_controlpoints.Location = New System.Drawing.Point(101, 32)
        Me.ComboBox_controlpoints.Name = "ComboBox_controlpoints"
        Me.ComboBox_controlpoints.Size = New System.Drawing.Size(82, 21)
        Me.ComboBox_controlpoints.TabIndex = 17
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(14, 16)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(28, 13)
        Me.Label38.TabIndex = 16
        Me.Label38.Text = "Typ:"
        '
        'GroupBox_SKTarget2
        '
        Me.GroupBox_SKTarget2.Controls.Add(Me.GroupBox10)
        Me.GroupBox_SKTarget2.Location = New System.Drawing.Point(8, 200)
        Me.GroupBox_SKTarget2.Name = "GroupBox_SKTarget2"
        Me.GroupBox_SKTarget2.Size = New System.Drawing.Size(133, 134)
        Me.GroupBox_SKTarget2.TabIndex = 15
        Me.GroupBox_SKTarget2.TabStop = False
        Me.GroupBox_SKTarget2.Text = "Target 2"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.Button_sk_reg_target2_center)
        Me.GroupBox10.Controls.Add(Me.Label27)
        Me.GroupBox10.Controls.Add(Me.Label31)
        Me.GroupBox10.Controls.Add(Me.TextBox_sk_target2_center_v)
        Me.GroupBox10.Controls.Add(Me.TextBox_sk_target2_center_hz)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(115, 109)
        Me.GroupBox10.TabIndex = 17
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Centerpoint"
        '
        'Button_sk_reg_target2_center
        '
        Me.Button_sk_reg_target2_center.Location = New System.Drawing.Point(20, 72)
        Me.Button_sk_reg_target2_center.Name = "Button_sk_reg_target2_center"
        Me.Button_sk_reg_target2_center.Size = New System.Drawing.Size(75, 23)
        Me.Button_sk_reg_target2_center.TabIndex = 21
        Me.Button_sk_reg_target2_center.Text = "Registration"
        Me.Button_sk_reg_target2_center.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(7, 49)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(20, 13)
        Me.Label27.TabIndex = 20
        Me.Label27.Text = "V.:"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(7, 24)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(26, 13)
        Me.Label31.TabIndex = 19
        Me.Label31.Text = "Hz.:"
        '
        'TextBox_sk_target2_center_v
        '
        Me.TextBox_sk_target2_center_v.Location = New System.Drawing.Point(34, 46)
        Me.TextBox_sk_target2_center_v.Name = "TextBox_sk_target2_center_v"
        Me.TextBox_sk_target2_center_v.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target2_center_v.TabIndex = 18
        '
        'TextBox_sk_target2_center_hz
        '
        Me.TextBox_sk_target2_center_hz.Location = New System.Drawing.Point(34, 20)
        Me.TextBox_sk_target2_center_hz.Name = "TextBox_sk_target2_center_hz"
        Me.TextBox_sk_target2_center_hz.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target2_center_hz.TabIndex = 17
        '
        'GroupBox_SKTarget1
        '
        Me.GroupBox_SKTarget1.Controls.Add(Me.GroupBox_Sk_target1_circle)
        Me.GroupBox_SKTarget1.Controls.Add(Me.GroupBox_Sk_target1_center)
        Me.GroupBox_SKTarget1.Location = New System.Drawing.Point(8, 60)
        Me.GroupBox_SKTarget1.Name = "GroupBox_SKTarget1"
        Me.GroupBox_SKTarget1.Size = New System.Drawing.Size(328, 134)
        Me.GroupBox_SKTarget1.TabIndex = 14
        Me.GroupBox_SKTarget1.TabStop = False
        Me.GroupBox_SKTarget1.Text = "Target 1"
        '
        'GroupBox_Sk_target1_circle
        '
        Me.GroupBox_Sk_target1_circle.Controls.Add(Me.Button_sk_reg_target1_circle)
        Me.GroupBox_Sk_target1_circle.Controls.Add(Me.Label25)
        Me.GroupBox_Sk_target1_circle.Controls.Add(Me.Label26)
        Me.GroupBox_Sk_target1_circle.Controls.Add(Me.TextBox_sk_target1_circle_v)
        Me.GroupBox_Sk_target1_circle.Controls.Add(Me.TextBox_sk_target1_circle_hz)
        Me.GroupBox_Sk_target1_circle.Location = New System.Drawing.Point(127, 17)
        Me.GroupBox_Sk_target1_circle.Name = "GroupBox_Sk_target1_circle"
        Me.GroupBox_Sk_target1_circle.Size = New System.Drawing.Size(195, 109)
        Me.GroupBox_Sk_target1_circle.TabIndex = 18
        Me.GroupBox_Sk_target1_circle.TabStop = False
        Me.GroupBox_Sk_target1_circle.Text = "Circlepoint"
        '
        'Button_sk_reg_target1_circle
        '
        Me.Button_sk_reg_target1_circle.Location = New System.Drawing.Point(20, 72)
        Me.Button_sk_reg_target1_circle.Name = "Button_sk_reg_target1_circle"
        Me.Button_sk_reg_target1_circle.Size = New System.Drawing.Size(75, 23)
        Me.Button_sk_reg_target1_circle.TabIndex = 21
        Me.Button_sk_reg_target1_circle.Text = "Registration"
        Me.Button_sk_reg_target1_circle.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(7, 49)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(20, 13)
        Me.Label25.TabIndex = 20
        Me.Label25.Text = "V.:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(7, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(26, 13)
        Me.Label26.TabIndex = 19
        Me.Label26.Text = "Hz.:"
        '
        'TextBox_sk_target1_circle_v
        '
        Me.TextBox_sk_target1_circle_v.Location = New System.Drawing.Point(34, 46)
        Me.TextBox_sk_target1_circle_v.Name = "TextBox_sk_target1_circle_v"
        Me.TextBox_sk_target1_circle_v.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target1_circle_v.TabIndex = 18
        '
        'TextBox_sk_target1_circle_hz
        '
        Me.TextBox_sk_target1_circle_hz.Location = New System.Drawing.Point(34, 20)
        Me.TextBox_sk_target1_circle_hz.Name = "TextBox_sk_target1_circle_hz"
        Me.TextBox_sk_target1_circle_hz.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target1_circle_hz.TabIndex = 17
        '
        'GroupBox_Sk_target1_center
        '
        Me.GroupBox_Sk_target1_center.Controls.Add(Me.Button_sk_reg_target1_center)
        Me.GroupBox_Sk_target1_center.Controls.Add(Me.Label22)
        Me.GroupBox_Sk_target1_center.Controls.Add(Me.Label21)
        Me.GroupBox_Sk_target1_center.Controls.Add(Me.TextBox_sk_target1_center_v)
        Me.GroupBox_Sk_target1_center.Controls.Add(Me.TextBox_sk_target1_center_hz)
        Me.GroupBox_Sk_target1_center.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox_Sk_target1_center.Name = "GroupBox_Sk_target1_center"
        Me.GroupBox_Sk_target1_center.Size = New System.Drawing.Size(115, 109)
        Me.GroupBox_Sk_target1_center.TabIndex = 17
        Me.GroupBox_Sk_target1_center.TabStop = False
        Me.GroupBox_Sk_target1_center.Text = "Centerpoint"
        '
        'Button_sk_reg_target1_center
        '
        Me.Button_sk_reg_target1_center.Location = New System.Drawing.Point(20, 72)
        Me.Button_sk_reg_target1_center.Name = "Button_sk_reg_target1_center"
        Me.Button_sk_reg_target1_center.Size = New System.Drawing.Size(75, 23)
        Me.Button_sk_reg_target1_center.TabIndex = 21
        Me.Button_sk_reg_target1_center.Text = "Registration"
        Me.Button_sk_reg_target1_center.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(7, 49)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(20, 13)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "V.:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(7, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(26, 13)
        Me.Label21.TabIndex = 19
        Me.Label21.Text = "Hz.:"
        '
        'TextBox_sk_target1_center_v
        '
        Me.TextBox_sk_target1_center_v.Location = New System.Drawing.Point(34, 46)
        Me.TextBox_sk_target1_center_v.Name = "TextBox_sk_target1_center_v"
        Me.TextBox_sk_target1_center_v.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target1_center_v.TabIndex = 18
        '
        'TextBox_sk_target1_center_hz
        '
        Me.TextBox_sk_target1_center_hz.Location = New System.Drawing.Point(34, 20)
        Me.TextBox_sk_target1_center_hz.Name = "TextBox_sk_target1_center_hz"
        Me.TextBox_sk_target1_center_hz.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_sk_target1_center_hz.TabIndex = 17
        '
        'ComboBox_Sk_typ
        '
        Me.ComboBox_Sk_typ.FormattingEnabled = True
        Me.ComboBox_Sk_typ.Location = New System.Drawing.Point(11, 32)
        Me.ComboBox_Sk_typ.Name = "ComboBox_Sk_typ"
        Me.ComboBox_Sk_typ.Size = New System.Drawing.Size(82, 21)
        Me.ComboBox_Sk_typ.TabIndex = 13
        '
        'CheckBox_SaveErrorImages_self_calibration
        '
        Me.CheckBox_SaveErrorImages_self_calibration.AutoSize = True
        Me.CheckBox_SaveErrorImages_self_calibration.Location = New System.Drawing.Point(205, 36)
        Me.CheckBox_SaveErrorImages_self_calibration.Name = "CheckBox_SaveErrorImages_self_calibration"
        Me.CheckBox_SaveErrorImages_self_calibration.Size = New System.Drawing.Size(131, 17)
        Me.CheckBox_SaveErrorImages_self_calibration.TabIndex = 12
        Me.CheckBox_SaveErrorImages_self_calibration.Text = "Save only Errorimages"
        Me.CheckBox_SaveErrorImages_self_calibration.UseVisualStyleBackColor = True
        '
        'CheckBox_SaveImage_selbstkalibrierung
        '
        Me.CheckBox_SaveImage_selbstkalibrierung.AutoSize = True
        Me.CheckBox_SaveImage_selbstkalibrierung.Location = New System.Drawing.Point(205, 15)
        Me.CheckBox_SaveImage_selbstkalibrierung.Name = "CheckBox_SaveImage_selbstkalibrierung"
        Me.CheckBox_SaveImage_selbstkalibrierung.Size = New System.Drawing.Size(83, 17)
        Me.CheckBox_SaveImage_selbstkalibrierung.TabIndex = 11
        Me.CheckBox_SaveImage_selbstkalibrierung.Text = "Save Image"
        Me.CheckBox_SaveImage_selbstkalibrierung.UseVisualStyleBackColor = True
        '
        'Button_SelfCalibrationStart
        '
        Me.Button_SelfCalibrationStart.Location = New System.Drawing.Point(276, 303)
        Me.Button_SelfCalibrationStart.Name = "Button_SelfCalibrationStart"
        Me.Button_SelfCalibrationStart.Size = New System.Drawing.Size(50, 23)
        Me.Button_SelfCalibrationStart.TabIndex = 2
        Me.Button_SelfCalibrationStart.Text = "Start"
        Me.Button_SelfCalibrationStart.UseVisualStyleBackColor = True
        '
        'Button_Crosshair
        '
        Me.Button_Crosshair.Location = New System.Drawing.Point(145, 11)
        Me.Button_Crosshair.Name = "Button_Crosshair"
        Me.Button_Crosshair.Size = New System.Drawing.Size(75, 23)
        Me.Button_Crosshair.TabIndex = 0
        Me.Button_Crosshair.Text = "Crosshair"
        Me.Button_Crosshair.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox18)
        Me.TabPage2.Controls.Add(Me.GroupBox15)
        Me.TabPage2.Controls.Add(Me.GroupBox14)
        Me.TabPage2.Controls.Add(Me.GroupBox13)
        Me.TabPage2.Controls.Add(Me.GroupBox12)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(350, 447)
        Me.TabPage2.TabIndex = 8
        Me.TabPage2.Text = "Resolution Test"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.CheckBox_KreuzEinschalten)
        Me.GroupBox18.Controls.Add(Me.Label44)
        Me.GroupBox18.Controls.Add(Me.TextBox_Name_AVMessung)
        Me.GroupBox18.Controls.Add(Me.TextBox_Wartezeit)
        Me.GroupBox18.Controls.Add(Me.Label45)
        Me.GroupBox18.Location = New System.Drawing.Point(189, 191)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(148, 142)
        Me.GroupBox18.TabIndex = 8
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "Einstellungen"
        '
        'CheckBox_KreuzEinschalten
        '
        Me.CheckBox_KreuzEinschalten.AutoSize = True
        Me.CheckBox_KreuzEinschalten.Location = New System.Drawing.Point(6, 110)
        Me.CheckBox_KreuzEinschalten.Name = "CheckBox_KreuzEinschalten"
        Me.CheckBox_KreuzEinschalten.Size = New System.Drawing.Size(110, 17)
        Me.CheckBox_KreuzEinschalten.TabIndex = 10
        Me.CheckBox_KreuzEinschalten.Text = "Kreuz einschalten"
        Me.CheckBox_KreuzEinschalten.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(3, 20)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(102, 13)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "Name der Messung:"
        '
        'TextBox_Name_AVMessung
        '
        Me.TextBox_Name_AVMessung.Location = New System.Drawing.Point(6, 36)
        Me.TextBox_Name_AVMessung.Name = "TextBox_Name_AVMessung"
        Me.TextBox_Name_AVMessung.Size = New System.Drawing.Size(135, 20)
        Me.TextBox_Name_AVMessung.TabIndex = 7
        Me.TextBox_Name_AVMessung.Text = "unknown"
        '
        'TextBox_Wartezeit
        '
        Me.TextBox_Wartezeit.Location = New System.Drawing.Point(6, 84)
        Me.TextBox_Wartezeit.Name = "TextBox_Wartezeit"
        Me.TextBox_Wartezeit.Size = New System.Drawing.Size(135, 20)
        Me.TextBox_Wartezeit.TabIndex = 9
        Me.TextBox_Wartezeit.Text = "2000"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(3, 68)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(131, 13)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "Wartezeit zw. Bildern [ms]:"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.Button_SP_Reset)
        Me.GroupBox15.Location = New System.Drawing.Point(189, 133)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(148, 52)
        Me.GroupBox15.TabIndex = 7
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Neue Messung"
        '
        'Button_SP_Reset
        '
        Me.Button_SP_Reset.Location = New System.Drawing.Point(6, 19)
        Me.Button_SP_Reset.Name = "Button_SP_Reset"
        Me.Button_SP_Reset.Size = New System.Drawing.Size(135, 23)
        Me.Button_SP_Reset.TabIndex = 6
        Me.Button_SP_Reset.Text = "Zurücksetzen"
        Me.Button_SP_Reset.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.Button_Messung3_Shift)
        Me.GroupBox14.Controls.Add(Me.Button_Messung3_Zentrum)
        Me.GroupBox14.Controls.Add(Me.Button_Messung2_Shift)
        Me.GroupBox14.Controls.Add(Me.Button_Messung2_Zentrum)
        Me.GroupBox14.Controls.Add(Me.CheckBox_Vz_Shift)
        Me.GroupBox14.Controls.Add(Me.CheckBox_Hz_Shift)
        Me.GroupBox14.Location = New System.Drawing.Point(189, 12)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(148, 115)
        Me.GroupBox14.TabIndex = 6
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Weitere Stützpunkte"
        '
        'Button_Messung3_Shift
        '
        Me.Button_Messung3_Shift.Location = New System.Drawing.Point(79, 80)
        Me.Button_Messung3_Shift.Name = "Button_Messung3_Shift"
        Me.Button_Messung3_Shift.Size = New System.Drawing.Size(63, 23)
        Me.Button_Messung3_Shift.TabIndex = 5
        Me.Button_Messung3_Shift.Text = "3-Shift"
        Me.Button_Messung3_Shift.UseVisualStyleBackColor = True
        '
        'Button_Messung3_Zentrum
        '
        Me.Button_Messung3_Zentrum.Location = New System.Drawing.Point(6, 80)
        Me.Button_Messung3_Zentrum.Name = "Button_Messung3_Zentrum"
        Me.Button_Messung3_Zentrum.Size = New System.Drawing.Size(63, 23)
        Me.Button_Messung3_Zentrum.TabIndex = 4
        Me.Button_Messung3_Zentrum.Text = "3-Center"
        Me.Button_Messung3_Zentrum.UseVisualStyleBackColor = True
        '
        'Button_Messung2_Shift
        '
        Me.Button_Messung2_Shift.Location = New System.Drawing.Point(79, 52)
        Me.Button_Messung2_Shift.Name = "Button_Messung2_Shift"
        Me.Button_Messung2_Shift.Size = New System.Drawing.Size(63, 23)
        Me.Button_Messung2_Shift.TabIndex = 3
        Me.Button_Messung2_Shift.Text = "2-Shift"
        Me.Button_Messung2_Shift.UseVisualStyleBackColor = True
        '
        'Button_Messung2_Zentrum
        '
        Me.Button_Messung2_Zentrum.Location = New System.Drawing.Point(6, 52)
        Me.Button_Messung2_Zentrum.Name = "Button_Messung2_Zentrum"
        Me.Button_Messung2_Zentrum.Size = New System.Drawing.Size(63, 23)
        Me.Button_Messung2_Zentrum.TabIndex = 2
        Me.Button_Messung2_Zentrum.Text = "2-Center"
        Me.Button_Messung2_Zentrum.UseVisualStyleBackColor = True
        '
        'CheckBox_Vz_Shift
        '
        Me.CheckBox_Vz_Shift.AutoSize = True
        Me.CheckBox_Vz_Shift.Location = New System.Drawing.Point(79, 25)
        Me.CheckBox_Vz_Shift.Name = "CheckBox_Vz_Shift"
        Me.CheckBox_Vz_Shift.Size = New System.Drawing.Size(62, 17)
        Me.CheckBox_Vz_Shift.TabIndex = 1
        Me.CheckBox_Vz_Shift.Text = "Vz Shift"
        Me.CheckBox_Vz_Shift.UseVisualStyleBackColor = True
        '
        'CheckBox_Hz_Shift
        '
        Me.CheckBox_Hz_Shift.AutoSize = True
        Me.CheckBox_Hz_Shift.Location = New System.Drawing.Point(6, 25)
        Me.CheckBox_Hz_Shift.Name = "CheckBox_Hz_Shift"
        Me.CheckBox_Hz_Shift.Size = New System.Drawing.Size(63, 17)
        Me.CheckBox_Hz_Shift.TabIndex = 0
        Me.CheckBox_Hz_Shift.Text = "Hz Shift"
        Me.CheckBox_Hz_Shift.UseVisualStyleBackColor = True
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.Button_Snapshot)
        Me.GroupBox13.Controls.Add(Me.TextBox_Messzeit)
        Me.GroupBox13.Controls.Add(Me.Label46)
        Me.GroupBox13.Controls.Add(Me.Button_25_Bilder_Komplett)
        Me.GroupBox13.Controls.Add(Me.Button_9_Bilder_Kreuz_Gedreht)
        Me.GroupBox13.Controls.Add(Me.Button_9_Bilder_Kreuz_Aufrecht)
        Me.GroupBox13.Location = New System.Drawing.Point(13, 97)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(167, 207)
        Me.GroupBox13.TabIndex = 5
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Bildererzeugung"
        '
        'Button_Snapshot
        '
        Me.Button_Snapshot.Location = New System.Drawing.Point(6, 106)
        Me.Button_Snapshot.Name = "Button_Snapshot"
        Me.Button_Snapshot.Size = New System.Drawing.Size(153, 23)
        Me.Button_Snapshot.TabIndex = 12
        Me.Button_Snapshot.Text = "Snapshot"
        Me.Button_Snapshot.UseVisualStyleBackColor = True
        '
        'TextBox_Messzeit
        '
        Me.TextBox_Messzeit.Location = New System.Drawing.Point(6, 178)
        Me.TextBox_Messzeit.Name = "TextBox_Messzeit"
        Me.TextBox_Messzeit.ReadOnly = True
        Me.TextBox_Messzeit.Size = New System.Drawing.Size(153, 20)
        Me.TextBox_Messzeit.TabIndex = 11
        Me.TextBox_Messzeit.Text = "not active"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(3, 162)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(51, 13)
        Me.Label46.TabIndex = 10
        Me.Label46.Text = "Messzeit:"
        '
        'Button_25_Bilder_Komplett
        '
        Me.Button_25_Bilder_Komplett.Enabled = False
        Me.Button_25_Bilder_Komplett.Location = New System.Drawing.Point(6, 77)
        Me.Button_25_Bilder_Komplett.Name = "Button_25_Bilder_Komplett"
        Me.Button_25_Bilder_Komplett.Size = New System.Drawing.Size(153, 23)
        Me.Button_25_Bilder_Komplett.TabIndex = 2
        Me.Button_25_Bilder_Komplett.Text = "25 Bilder - Komplett"
        Me.Button_25_Bilder_Komplett.UseVisualStyleBackColor = True
        '
        'Button_9_Bilder_Kreuz_Gedreht
        '
        Me.Button_9_Bilder_Kreuz_Gedreht.Enabled = False
        Me.Button_9_Bilder_Kreuz_Gedreht.Location = New System.Drawing.Point(6, 48)
        Me.Button_9_Bilder_Kreuz_Gedreht.Name = "Button_9_Bilder_Kreuz_Gedreht"
        Me.Button_9_Bilder_Kreuz_Gedreht.Size = New System.Drawing.Size(153, 23)
        Me.Button_9_Bilder_Kreuz_Gedreht.TabIndex = 1
        Me.Button_9_Bilder_Kreuz_Gedreht.Text = "9 Bilder - Kreuz gedreht"
        Me.Button_9_Bilder_Kreuz_Gedreht.UseVisualStyleBackColor = True
        '
        'Button_9_Bilder_Kreuz_Aufrecht
        '
        Me.Button_9_Bilder_Kreuz_Aufrecht.Enabled = False
        Me.Button_9_Bilder_Kreuz_Aufrecht.Location = New System.Drawing.Point(6, 19)
        Me.Button_9_Bilder_Kreuz_Aufrecht.Name = "Button_9_Bilder_Kreuz_Aufrecht"
        Me.Button_9_Bilder_Kreuz_Aufrecht.Size = New System.Drawing.Size(153, 23)
        Me.Button_9_Bilder_Kreuz_Aufrecht.TabIndex = 0
        Me.Button_9_Bilder_Kreuz_Aufrecht.Text = "9 Bilder - Kreuz aufrecht"
        Me.Button_9_Bilder_Kreuz_Aufrecht.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Button_Messe_Rand)
        Me.GroupBox12.Controls.Add(Me.Button_Messe_Zentrum)
        Me.GroupBox12.Location = New System.Drawing.Point(13, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(167, 79)
        Me.GroupBox12.TabIndex = 4
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Erste Stützpunkte"
        '
        'Button_Messe_Rand
        '
        Me.Button_Messe_Rand.Location = New System.Drawing.Point(6, 50)
        Me.Button_Messe_Rand.Name = "Button_Messe_Rand"
        Me.Button_Messe_Rand.Size = New System.Drawing.Size(153, 23)
        Me.Button_Messe_Rand.TabIndex = 3
        Me.Button_Messe_Rand.Text = "Messe Rand Oben Links"
        Me.Button_Messe_Rand.UseVisualStyleBackColor = True
        '
        'Button_Messe_Zentrum
        '
        Me.Button_Messe_Zentrum.Location = New System.Drawing.Point(6, 21)
        Me.Button_Messe_Zentrum.Name = "Button_Messe_Zentrum"
        Me.Button_Messe_Zentrum.Size = New System.Drawing.Size(153, 23)
        Me.Button_Messe_Zentrum.TabIndex = 0
        Me.Button_Messe_Zentrum.Text = "Messe Zentrum"
        Me.Button_Messe_Zentrum.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btKompOff)
        Me.GroupBox2.Controls.Add(Me.btKompOn)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(139, 50)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Compensator"
        '
        'btKompOff
        '
        Me.btKompOff.Location = New System.Drawing.Point(73, 21)
        Me.btKompOff.Name = "btKompOff"
        Me.btKompOff.Size = New System.Drawing.Size(59, 23)
        Me.btKompOff.TabIndex = 28
        Me.btKompOff.Text = "OFF"
        Me.btKompOff.UseVisualStyleBackColor = True
        '
        'btKompOn
        '
        Me.btKompOn.Location = New System.Drawing.Point(6, 21)
        Me.btKompOn.Name = "btKompOn"
        Me.btKompOn.Size = New System.Drawing.Size(59, 23)
        Me.btKompOn.TabIndex = 27
        Me.btKompOn.Text = "ON"
        Me.btKompOn.UseVisualStyleBackColor = True
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.lbPositionTime)
        Me.GroupBox19.Controls.Add(Me.btSetPosTime)
        Me.GroupBox19.Controls.Add(Me.cbTimeTol)
        Me.GroupBox19.Location = New System.Drawing.Point(6, 75)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(139, 65)
        Me.GroupBox19.TabIndex = 47
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Position Time"
        '
        'lbPositionTime
        '
        Me.lbPositionTime.Location = New System.Drawing.Point(6, 45)
        Me.lbPositionTime.Name = "lbPositionTime"
        Me.lbPositionTime.Size = New System.Drawing.Size(120, 15)
        Me.lbPositionTime.TabIndex = 55
        '
        'btSetPosTime
        '
        Me.btSetPosTime.Location = New System.Drawing.Point(93, 19)
        Me.btSetPosTime.Name = "btSetPosTime"
        Me.btSetPosTime.Size = New System.Drawing.Size(39, 23)
        Me.btSetPosTime.TabIndex = 34
        Me.btSetPosTime.Text = "Set"
        Me.btSetPosTime.UseVisualStyleBackColor = True
        '
        'cbTimeTol
        '
        Me.cbTimeTol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbTimeTol.FormattingEnabled = True
        Me.cbTimeTol.Items.AddRange(New Object() {"1s", "5s", "10s", "20s", "60s"})
        Me.cbTimeTol.Location = New System.Drawing.Point(6, 19)
        Me.cbTimeTol.Name = "cbTimeTol"
        Me.cbTimeTol.Size = New System.Drawing.Size(81, 21)
        Me.cbTimeTol.TabIndex = 33
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lbPositionTolerance)
        Me.GroupBox4.Controls.Add(Me.cbPosTol)
        Me.GroupBox4.Controls.Add(Me.btSetPosTol)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 145)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(139, 60)
        Me.GroupBox4.TabIndex = 46
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Position Tolerance"
        '
        'lbPositionTolerance
        '
        Me.lbPositionTolerance.Location = New System.Drawing.Point(6, 41)
        Me.lbPositionTolerance.Name = "lbPositionTolerance"
        Me.lbPositionTolerance.Size = New System.Drawing.Size(120, 15)
        Me.lbPositionTolerance.TabIndex = 56
        '
        'cbPosTol
        '
        Me.cbPosTol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbPosTol.FormattingEnabled = True
        Me.cbPosTol.Items.AddRange(New Object() {"10,0 mgon", "3,0 mgon", "1,0 mgon", "0,5mgon", "0,3 mgon", "0,1 mgon"})
        Me.cbPosTol.Location = New System.Drawing.Point(6, 17)
        Me.cbPosTol.Name = "cbPosTol"
        Me.cbPosTol.Size = New System.Drawing.Size(81, 21)
        Me.cbPosTol.TabIndex = 30
        '
        'btSetPosTol
        '
        Me.btSetPosTol.Location = New System.Drawing.Point(93, 19)
        Me.btSetPosTol.Name = "btSetPosTol"
        Me.btSetPosTol.Size = New System.Drawing.Size(39, 23)
        Me.btSetPosTol.TabIndex = 31
        Me.btSetPosTol.Text = "Set"
        Me.btSetPosTol.UseVisualStyleBackColor = True
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.btSetAngleCorrections)
        Me.GroupBox20.Controls.Add(Me.cbHiKorr)
        Me.GroupBox20.Controls.Add(Me.cbSaKorr)
        Me.GroupBox20.Controls.Add(Me.cbZaKorr)
        Me.GroupBox20.Controls.Add(Me.cbKaKorr)
        Me.GroupBox20.Location = New System.Drawing.Point(7, 211)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(138, 141)
        Me.GroupBox20.TabIndex = 48
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Angle Corrections"
        '
        'btSetAngleCorrections
        '
        Me.btSetAngleCorrections.Location = New System.Drawing.Point(6, 19)
        Me.btSetAngleCorrections.Name = "btSetAngleCorrections"
        Me.btSetAngleCorrections.Size = New System.Drawing.Size(125, 23)
        Me.btSetAngleCorrections.TabIndex = 40
        Me.btSetAngleCorrections.Text = "Set Corrections"
        Me.btSetAngleCorrections.UseVisualStyleBackColor = True
        '
        'cbHiKorr
        '
        Me.cbHiKorr.AutoSize = True
        Me.cbHiKorr.Location = New System.Drawing.Point(7, 48)
        Me.cbHiKorr.Name = "cbHiKorr"
        Me.cbHiKorr.Size = New System.Drawing.Size(125, 17)
        Me.cbHiKorr.TabIndex = 36
        Me.cbHiKorr.Text = "Höhenindexkorrektur"
        Me.cbHiKorr.UseVisualStyleBackColor = True
        '
        'cbSaKorr
        '
        Me.cbSaKorr.AutoSize = True
        Me.cbSaKorr.Location = New System.Drawing.Point(7, 71)
        Me.cbSaKorr.Name = "cbSaKorr"
        Me.cbSaKorr.Size = New System.Drawing.Size(113, 17)
        Me.cbSaKorr.TabIndex = 37
        Me.cbSaKorr.Text = "Stehachskorrektur"
        Me.cbSaKorr.UseVisualStyleBackColor = True
        '
        'cbZaKorr
        '
        Me.cbZaKorr.AutoSize = True
        Me.cbZaKorr.Location = New System.Drawing.Point(7, 94)
        Me.cbZaKorr.Name = "cbZaKorr"
        Me.cbZaKorr.Size = New System.Drawing.Size(108, 17)
        Me.cbZaKorr.TabIndex = 38
        Me.cbZaKorr.Text = "Zielachskorrektur"
        Me.cbZaKorr.UseVisualStyleBackColor = True
        '
        'cbKaKorr
        '
        Me.cbKaKorr.AutoSize = True
        Me.cbKaKorr.Location = New System.Drawing.Point(7, 117)
        Me.cbKaKorr.Name = "cbKaKorr"
        Me.cbKaKorr.Size = New System.Drawing.Size(112, 17)
        Me.cbKaKorr.TabIndex = 39
        Me.cbKaKorr.Text = "Kippachskorrektur"
        Me.cbKaKorr.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage7)
        Me.TabControl3.Controls.Add(Me.TabPage8)
        Me.TabControl3.Controls.Add(Me.TabPage9)
        Me.TabControl3.Controls.Add(Me.TabPage_bv1)
        Me.TabControl3.Controls.Add(Me.uEye)
        Me.TabControl3.Location = New System.Drawing.Point(363, 27)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(656, 570)
        Me.TabControl3.TabIndex = 18
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.GroupBox_Measure_Image)
        Me.TabPage7.Controls.Add(Me.gb_zoommove)
        Me.TabPage7.Controls.Add(Me.HWindowControl1)
        Me.TabPage7.Controls.Add(Me.gb_Snapshot)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(648, 544)
        Me.TabPage7.TabIndex = 0
        Me.TabPage7.Text = "Liveimage"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'GroupBox_Measure_Image
        '
        Me.GroupBox_Measure_Image.Controls.Add(Me.CheckBox_Measure_Image)
        Me.GroupBox_Measure_Image.Location = New System.Drawing.Point(99, 489)
        Me.GroupBox_Measure_Image.Name = "GroupBox_Measure_Image"
        Me.GroupBox_Measure_Image.Size = New System.Drawing.Size(227, 52)
        Me.GroupBox_Measure_Image.TabIndex = 27
        Me.GroupBox_Measure_Image.TabStop = False
        Me.GroupBox_Measure_Image.Text = "Meausre"
        '
        'CheckBox_Measure_Image
        '
        Me.CheckBox_Measure_Image.AutoSize = True
        Me.CheckBox_Measure_Image.Location = New System.Drawing.Point(16, 23)
        Me.CheckBox_Measure_Image.Name = "CheckBox_Measure_Image"
        Me.CheckBox_Measure_Image.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox_Measure_Image.TabIndex = 0
        Me.CheckBox_Measure_Image.Text = "Measure activate"
        Me.CheckBox_Measure_Image.UseVisualStyleBackColor = True
        '
        'gb_zoommove
        '
        Me.gb_zoommove.Controls.Add(Me.bu_ZoomAndMoveReset)
        Me.gb_zoommove.Controls.Add(Me.rb_move)
        Me.gb_zoommove.Controls.Add(Me.rb_zoom)
        Me.gb_zoommove.Location = New System.Drawing.Point(437, 489)
        Me.gb_zoommove.Name = "gb_zoommove"
        Me.gb_zoommove.Size = New System.Drawing.Size(208, 52)
        Me.gb_zoommove.TabIndex = 26
        Me.gb_zoommove.TabStop = False
        Me.gb_zoommove.Text = "Zoom / Move"
        '
        'bu_ZoomAndMoveReset
        '
        Me.bu_ZoomAndMoveReset.Location = New System.Drawing.Point(125, 17)
        Me.bu_ZoomAndMoveReset.Name = "bu_ZoomAndMoveReset"
        Me.bu_ZoomAndMoveReset.Size = New System.Drawing.Size(75, 23)
        Me.bu_ZoomAndMoveReset.TabIndex = 2
        Me.bu_ZoomAndMoveReset.Text = "Reset"
        Me.bu_ZoomAndMoveReset.UseVisualStyleBackColor = True
        '
        'rb_move
        '
        Me.rb_move.AutoSize = True
        Me.rb_move.Location = New System.Drawing.Point(67, 20)
        Me.rb_move.Name = "rb_move"
        Me.rb_move.Size = New System.Drawing.Size(52, 17)
        Me.rb_move.TabIndex = 1
        Me.rb_move.TabStop = True
        Me.rb_move.Text = "Move"
        Me.rb_move.UseVisualStyleBackColor = True
        '
        'rb_zoom
        '
        Me.rb_zoom.AutoSize = True
        Me.rb_zoom.Location = New System.Drawing.Point(9, 20)
        Me.rb_zoom.Name = "rb_zoom"
        Me.rb_zoom.Size = New System.Drawing.Size(52, 17)
        Me.rb_zoom.TabIndex = 0
        Me.rb_zoom.TabStop = True
        Me.rb_zoom.Text = "Zoom"
        Me.rb_zoom.UseVisualStyleBackColor = True
        '
        'HWindowControl1
        '
        Me.HWindowControl1.BackColor = System.Drawing.Color.Black
        Me.HWindowControl1.BorderColor = System.Drawing.Color.Black
        Me.HWindowControl1.ImagePart = New System.Drawing.Rectangle(0, 0, 640, 480)
        Me.HWindowControl1.Location = New System.Drawing.Point(5, 3)
        Me.HWindowControl1.Name = "HWindowControl1"
        Me.HWindowControl1.Size = New System.Drawing.Size(640, 480)
        Me.HWindowControl1.TabIndex = 25
        Me.HWindowControl1.WindowSize = New System.Drawing.Size(640, 480)
        '
        'gb_Snapshot
        '
        Me.gb_Snapshot.Controls.Add(Me.Bu_Snapshot)
        Me.gb_Snapshot.Location = New System.Drawing.Point(6, 491)
        Me.gb_Snapshot.Name = "gb_Snapshot"
        Me.gb_Snapshot.Size = New System.Drawing.Size(87, 51)
        Me.gb_Snapshot.TabIndex = 19
        Me.gb_Snapshot.TabStop = False
        Me.gb_Snapshot.Text = "Snapshot"
        '
        'Bu_Snapshot
        '
        Me.Bu_Snapshot.Location = New System.Drawing.Point(6, 19)
        Me.Bu_Snapshot.Name = "Bu_Snapshot"
        Me.Bu_Snapshot.Size = New System.Drawing.Size(75, 23)
        Me.Bu_Snapshot.TabIndex = 0
        Me.Bu_Snapshot.Text = "Snapshot"
        Me.Bu_Snapshot.UseVisualStyleBackColor = True
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.bu_export_data_as_csv)
        Me.TabPage8.Controls.Add(Me.DGVData)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage8.Size = New System.Drawing.Size(648, 544)
        Me.TabPage8.TabIndex = 1
        Me.TabPage8.Text = "Data"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'bu_export_data_as_csv
        '
        Me.bu_export_data_as_csv.Location = New System.Drawing.Point(6, 457)
        Me.bu_export_data_as_csv.Name = "bu_export_data_as_csv"
        Me.bu_export_data_as_csv.Size = New System.Drawing.Size(83, 23)
        Me.bu_export_data_as_csv.TabIndex = 15
        Me.bu_export_data_as_csv.Text = "Export as csv"
        Me.bu_export_data_as_csv.UseVisualStyleBackColor = True
        '
        'DGVData
        '
        Me.DGVData.Location = New System.Drawing.Point(5, 6)
        Me.DGVData.Name = "DGVData"
        Me.DGVData.Size = New System.Drawing.Size(640, 441)
        Me.DGVData.TabIndex = 14
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.bu_export_log_as_csv)
        Me.TabPage9.Controls.Add(Me.DGVLog)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage9.Size = New System.Drawing.Size(648, 544)
        Me.TabPage9.TabIndex = 2
        Me.TabPage9.Text = "Log"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'bu_export_log_as_csv
        '
        Me.bu_export_log_as_csv.Location = New System.Drawing.Point(3, 453)
        Me.bu_export_log_as_csv.Name = "bu_export_log_as_csv"
        Me.bu_export_log_as_csv.Size = New System.Drawing.Size(84, 23)
        Me.bu_export_log_as_csv.TabIndex = 16
        Me.bu_export_log_as_csv.Text = "Export as csv"
        Me.bu_export_log_as_csv.UseVisualStyleBackColor = True
        '
        'DGVLog
        '
        Me.DGVLog.Location = New System.Drawing.Point(4, 6)
        Me.DGVLog.Name = "DGVLog"
        Me.DGVLog.Size = New System.Drawing.Size(640, 441)
        Me.DGVLog.TabIndex = 15
        '
        'TabPage_bv1
        '
        Me.TabPage_bv1.Controls.Add(Me.HWindowControl2)
        Me.TabPage_bv1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_bv1.Name = "TabPage_bv1"
        Me.TabPage_bv1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_bv1.Size = New System.Drawing.Size(648, 544)
        Me.TabPage_bv1.TabIndex = 3
        Me.TabPage_bv1.Text = "Result"
        Me.TabPage_bv1.UseVisualStyleBackColor = True
        '
        'HWindowControl2
        '
        Me.HWindowControl2.BackColor = System.Drawing.Color.Black
        Me.HWindowControl2.BorderColor = System.Drawing.Color.Black
        Me.HWindowControl2.ImagePart = New System.Drawing.Rectangle(0, 0, 640, 480)
        Me.HWindowControl2.Location = New System.Drawing.Point(5, 3)
        Me.HWindowControl2.Name = "HWindowControl2"
        Me.HWindowControl2.Size = New System.Drawing.Size(640, 480)
        Me.HWindowControl2.TabIndex = 26
        Me.HWindowControl2.WindowSize = New System.Drawing.Size(640, 480)
        '
        'uEye
        '
        Me.uEye.Controls.Add(Me.Label_X2)
        Me.uEye.Controls.Add(Me.Label_X3)
        Me.uEye.Controls.Add(Me.Label_X4)
        Me.uEye.Controls.Add(Me.Label_X5)
        Me.uEye.Controls.Add(Me.Label_X1)
        Me.uEye.Controls.Add(Me.Label43)
        Me.uEye.Controls.Add(Me.ComboBox_uEye_Subsampling)
        Me.uEye.Controls.Add(Me.Label42)
        Me.uEye.Controls.Add(Me.ComboBox_uEye_binning)
        Me.uEye.Controls.Add(Me.gb_property)
        Me.uEye.Controls.Add(Me.GroupBox11)
        Me.uEye.Controls.Add(Me.GroupBox8)
        Me.uEye.Controls.Add(Me.GroupBox9)
        Me.uEye.Controls.Add(Me.AxuEyeCam1)
        Me.uEye.Location = New System.Drawing.Point(4, 22)
        Me.uEye.Name = "uEye"
        Me.uEye.Padding = New System.Windows.Forms.Padding(3)
        Me.uEye.Size = New System.Drawing.Size(648, 544)
        Me.uEye.TabIndex = 4
        Me.uEye.Text = "uEye"
        Me.uEye.UseVisualStyleBackColor = True
        '
        'Label_X2
        '
        Me.Label_X2.BackColor = System.Drawing.Color.GreenYellow
        Me.Label_X2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_X2.ForeColor = System.Drawing.Color.Black
        Me.Label_X2.Location = New System.Drawing.Point(151, 218)
        Me.Label_X2.Name = "Label_X2"
        Me.Label_X2.Size = New System.Drawing.Size(20, 20)
        Me.Label_X2.TabIndex = 38
        Me.Label_X2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label_X2.Visible = False
        '
        'Label_X3
        '
        Me.Label_X3.BackColor = System.Drawing.Color.GreenYellow
        Me.Label_X3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_X3.ForeColor = System.Drawing.Color.Black
        Me.Label_X3.Location = New System.Drawing.Point(473, 218)
        Me.Label_X3.Name = "Label_X3"
        Me.Label_X3.Size = New System.Drawing.Size(20, 20)
        Me.Label_X3.TabIndex = 37
        Me.Label_X3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label_X3.Visible = False
        '
        'Label_X4
        '
        Me.Label_X4.BackColor = System.Drawing.Color.GreenYellow
        Me.Label_X4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_X4.ForeColor = System.Drawing.Color.Black
        Me.Label_X4.Location = New System.Drawing.Point(312, 104)
        Me.Label_X4.Name = "Label_X4"
        Me.Label_X4.Size = New System.Drawing.Size(20, 20)
        Me.Label_X4.TabIndex = 36
        Me.Label_X4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label_X4.Visible = False
        '
        'Label_X5
        '
        Me.Label_X5.BackColor = System.Drawing.Color.GreenYellow
        Me.Label_X5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_X5.ForeColor = System.Drawing.Color.Black
        Me.Label_X5.Location = New System.Drawing.Point(312, 332)
        Me.Label_X5.Name = "Label_X5"
        Me.Label_X5.Size = New System.Drawing.Size(20, 20)
        Me.Label_X5.TabIndex = 35
        Me.Label_X5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label_X5.Visible = False
        '
        'Label_X1
        '
        Me.Label_X1.BackColor = System.Drawing.Color.GreenYellow
        Me.Label_X1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_X1.ForeColor = System.Drawing.Color.Black
        Me.Label_X1.Location = New System.Drawing.Point(312, 218)
        Me.Label_X1.Name = "Label_X1"
        Me.Label_X1.Size = New System.Drawing.Size(20, 20)
        Me.Label_X1.TabIndex = 34
        Me.Label_X1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label_X1.Visible = False
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(561, 473)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(67, 13)
        Me.Label43.TabIndex = 30
        Me.Label43.Text = "Subsampling"
        '
        'ComboBox_uEye_Subsampling
        '
        Me.ComboBox_uEye_Subsampling.FormattingEnabled = True
        Me.ComboBox_uEye_Subsampling.Location = New System.Drawing.Point(564, 492)
        Me.ComboBox_uEye_Subsampling.Name = "ComboBox_uEye_Subsampling"
        Me.ComboBox_uEye_Subsampling.Size = New System.Drawing.Size(52, 21)
        Me.ComboBox_uEye_Subsampling.TabIndex = 29
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(493, 473)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(42, 13)
        Me.Label42.TabIndex = 28
        Me.Label42.Text = "Binning"
        '
        'ComboBox_uEye_binning
        '
        Me.ComboBox_uEye_binning.FormattingEnabled = True
        Me.ComboBox_uEye_binning.Location = New System.Drawing.Point(493, 492)
        Me.ComboBox_uEye_binning.Name = "ComboBox_uEye_binning"
        Me.ComboBox_uEye_binning.Size = New System.Drawing.Size(52, 21)
        Me.ComboBox_uEye_binning.TabIndex = 27
        '
        'gb_property
        '
        Me.gb_property.Controls.Add(Me.Bu_camera_property)
        Me.gb_property.Location = New System.Drawing.Point(277, 462)
        Me.gb_property.Name = "gb_property"
        Me.gb_property.Size = New System.Drawing.Size(108, 51)
        Me.gb_property.TabIndex = 26
        Me.gb_property.TabStop = False
        Me.gb_property.Text = "Camera Property"
        '
        'Bu_camera_property
        '
        Me.Bu_camera_property.Location = New System.Drawing.Point(6, 19)
        Me.Bu_camera_property.Name = "Bu_camera_property"
        Me.Bu_camera_property.Size = New System.Drawing.Size(75, 23)
        Me.Bu_camera_property.TabIndex = 0
        Me.Bu_camera_property.Text = "Property"
        Me.Bu_camera_property.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.Button7)
        Me.GroupBox11.Location = New System.Drawing.Point(391, 463)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(87, 51)
        Me.GroupBox11.TabIndex = 25
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Snapshot"
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(6, 19)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 23)
        Me.Button7.TabIndex = 0
        Me.Button7.Text = "Snapshot"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label41)
        Me.GroupBox8.Controls.Add(Me.Panel1)
        Me.GroupBox8.Controls.Add(Me.Button3)
        Me.GroupBox8.Controls.Add(Me.Button4)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 462)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(170, 79)
        Me.GroupBox8.TabIndex = 24
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Camera"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(33, 52)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(61, 13)
        Me.Label41.TabIndex = 4
        Me.Label41.Text = "Disconnect"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Location = New System.Drawing.Point(7, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 20)
        Me.Panel1.TabIndex = 3
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(87, 19)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Disconnect"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(6, 19)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Connect"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Button5)
        Me.GroupBox9.Controls.Add(Me.Button6)
        Me.GroupBox9.Location = New System.Drawing.Point(182, 462)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(89, 79)
        Me.GroupBox9.TabIndex = 23
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Livemodus"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(7, 42)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 1
        Me.Button5.Text = "Stop"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(7, 17)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 0
        Me.Button6.Text = "Start"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'AxuEyeCam1
        '
        Me.AxuEyeCam1.Enabled = True
        Me.AxuEyeCam1.Location = New System.Drawing.Point(0, 0)
        Me.AxuEyeCam1.Name = "AxuEyeCam1"
        Me.AxuEyeCam1.OcxState = CType(resources.GetObject("AxuEyeCam1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxuEyeCam1.Size = New System.Drawing.Size(644, 455)
        Me.AxuEyeCam1.TabIndex = 0
        '
        'gb_measure
        '
        Me.gb_measure.Controls.Add(Me.cb_Measure_saveImage)
        Me.gb_measure.Controls.Add(Me.cb_Measure_withoutSave)
        Me.gb_measure.Controls.Add(Me.Bu_Measure_All)
        Me.gb_measure.Controls.Add(Me.Bu_Measure_Distance)
        Me.gb_measure.Controls.Add(Me.Bu_Measure_Direction)
        Me.gb_measure.Controls.Add(Me.La_Measure_pointname)
        Me.gb_measure.Controls.Add(Me.tb_pointname)
        Me.gb_measure.Controls.Add(Me.La_Measure_pointnumber)
        Me.gb_measure.Controls.Add(Me.tb_pointnumber)
        Me.gb_measure.Controls.Add(Me.La_ID_Status)
        Me.gb_measure.Controls.Add(Me.La_Measure_id)
        Me.gb_measure.Location = New System.Drawing.Point(7, 581)
        Me.gb_measure.Name = "gb_measure"
        Me.gb_measure.Size = New System.Drawing.Size(349, 109)
        Me.gb_measure.TabIndex = 24
        Me.gb_measure.TabStop = False
        Me.gb_measure.Text = "Measure"
        '
        'cb_Measure_saveImage
        '
        Me.cb_Measure_saveImage.AutoSize = True
        Me.cb_Measure_saveImage.Location = New System.Drawing.Point(254, 66)
        Me.cb_Measure_saveImage.Name = "cb_Measure_saveImage"
        Me.cb_Measure_saveImage.Size = New System.Drawing.Size(83, 17)
        Me.cb_Measure_saveImage.TabIndex = 10
        Me.cb_Measure_saveImage.Text = "Save Image"
        Me.cb_Measure_saveImage.UseVisualStyleBackColor = True
        '
        'cb_Measure_withoutSave
        '
        Me.cb_Measure_withoutSave.AutoSize = True
        Me.cb_Measure_withoutSave.Location = New System.Drawing.Point(254, 86)
        Me.cb_Measure_withoutSave.Name = "cb_Measure_withoutSave"
        Me.cb_Measure_withoutSave.Size = New System.Drawing.Size(88, 17)
        Me.cb_Measure_withoutSave.TabIndex = 9
        Me.cb_Measure_withoutSave.Text = "without Save"
        Me.cb_Measure_withoutSave.UseVisualStyleBackColor = True
        '
        'Bu_Measure_All
        '
        Me.Bu_Measure_All.Location = New System.Drawing.Point(13, 73)
        Me.Bu_Measure_All.Name = "Bu_Measure_All"
        Me.Bu_Measure_All.Size = New System.Drawing.Size(75, 23)
        Me.Bu_Measure_All.TabIndex = 8
        Me.Bu_Measure_All.Text = "All"
        Me.Bu_Measure_All.UseVisualStyleBackColor = True
        '
        'Bu_Measure_Distance
        '
        Me.Bu_Measure_Distance.Location = New System.Drawing.Point(94, 73)
        Me.Bu_Measure_Distance.Name = "Bu_Measure_Distance"
        Me.Bu_Measure_Distance.Size = New System.Drawing.Size(75, 23)
        Me.Bu_Measure_Distance.TabIndex = 7
        Me.Bu_Measure_Distance.Text = "Distance"
        Me.Bu_Measure_Distance.UseVisualStyleBackColor = True
        '
        'Bu_Measure_Direction
        '
        Me.Bu_Measure_Direction.Location = New System.Drawing.Point(175, 73)
        Me.Bu_Measure_Direction.Name = "Bu_Measure_Direction"
        Me.Bu_Measure_Direction.Size = New System.Drawing.Size(75, 23)
        Me.Bu_Measure_Direction.TabIndex = 6
        Me.Bu_Measure_Direction.Text = "Direction"
        Me.Bu_Measure_Direction.UseVisualStyleBackColor = True
        '
        'La_Measure_pointname
        '
        Me.La_Measure_pointname.AutoSize = True
        Me.La_Measure_pointname.Location = New System.Drawing.Point(129, 17)
        Me.La_Measure_pointname.Name = "La_Measure_pointname"
        Me.La_Measure_pointname.Size = New System.Drawing.Size(57, 13)
        Me.La_Measure_pointname.TabIndex = 5
        Me.La_Measure_pointname.Text = "Pointname"
        '
        'tb_pointname
        '
        Me.tb_pointname.Location = New System.Drawing.Point(129, 37)
        Me.tb_pointname.Name = "tb_pointname"
        Me.tb_pointname.Size = New System.Drawing.Size(205, 20)
        Me.tb_pointname.TabIndex = 4
        '
        'La_Measure_pointnumber
        '
        Me.La_Measure_pointnumber.AutoSize = True
        Me.La_Measure_pointnumber.Location = New System.Drawing.Point(57, 17)
        Me.La_Measure_pointnumber.Name = "La_Measure_pointnumber"
        Me.La_Measure_pointnumber.Size = New System.Drawing.Size(66, 13)
        Me.La_Measure_pointnumber.TabIndex = 3
        Me.La_Measure_pointnumber.Text = "Pointnumber"
        '
        'tb_pointnumber
        '
        Me.tb_pointnumber.Location = New System.Drawing.Point(60, 37)
        Me.tb_pointnumber.Name = "tb_pointnumber"
        Me.tb_pointnumber.Size = New System.Drawing.Size(63, 20)
        Me.tb_pointnumber.TabIndex = 2
        '
        'La_ID_Status
        '
        Me.La_ID_Status.AutoSize = True
        Me.La_ID_Status.Location = New System.Drawing.Point(10, 40)
        Me.La_ID_Status.Name = "La_ID_Status"
        Me.La_ID_Status.Size = New System.Drawing.Size(43, 13)
        Me.La_ID_Status.TabIndex = 1
        Me.La_ID_Status.Text = "000001"
        '
        'La_Measure_id
        '
        Me.La_Measure_id.AutoSize = True
        Me.La_Measure_id.Location = New System.Drawing.Point(10, 17)
        Me.La_Measure_id.Name = "La_Measure_id"
        Me.La_Measure_id.Size = New System.Drawing.Size(18, 13)
        Me.La_Measure_id.TabIndex = 0
        Me.La_Measure_id.Text = "ID"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_project})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1019, 24)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ts_project
        '
        Me.ts_project.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenProjectToolStripMenuItem})
        Me.ts_project.Name = "ts_project"
        Me.ts_project.Size = New System.Drawing.Size(56, 20)
        Me.ts_project.Text = "Project"
        '
        'OpenProjectToolStripMenuItem
        '
        Me.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem"
        Me.OpenProjectToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OpenProjectToolStripMenuItem.Text = "Open Project ..."
        '
        'gb_Measure_StatusStation
        '
        Me.gb_Measure_StatusStation.Controls.Add(Me.La_theoinfos)
        Me.gb_Measure_StatusStation.Controls.Add(Me.La_Tachymeterstatus)
        Me.gb_Measure_StatusStation.Controls.Add(Me.Panel_Tachymeter_Status)
        Me.gb_Measure_StatusStation.Location = New System.Drawing.Point(7, 506)
        Me.gb_Measure_StatusStation.Name = "gb_Measure_StatusStation"
        Me.gb_Measure_StatusStation.Size = New System.Drawing.Size(176, 69)
        Me.gb_Measure_StatusStation.TabIndex = 11
        Me.gb_Measure_StatusStation.TabStop = False
        Me.gb_Measure_StatusStation.Text = "Status Station"
        '
        'La_theoinfos
        '
        Me.La_theoinfos.AutoSize = True
        Me.La_theoinfos.Location = New System.Drawing.Point(11, 46)
        Me.La_theoinfos.Name = "La_theoinfos"
        Me.La_theoinfos.Size = New System.Drawing.Size(0, 13)
        Me.La_theoinfos.TabIndex = 24
        '
        'La_Tachymeterstatus
        '
        Me.La_Tachymeterstatus.AutoSize = True
        Me.La_Tachymeterstatus.Location = New System.Drawing.Point(35, 23)
        Me.La_Tachymeterstatus.Name = "La_Tachymeterstatus"
        Me.La_Tachymeterstatus.Size = New System.Drawing.Size(61, 13)
        Me.La_Tachymeterstatus.TabIndex = 23
        Me.La_Tachymeterstatus.Text = "Disconnect"
        '
        'Panel_Tachymeter_Status
        '
        Me.Panel_Tachymeter_Status.BackColor = System.Drawing.Color.Red
        Me.Panel_Tachymeter_Status.Location = New System.Drawing.Point(9, 19)
        Me.Panel_Tachymeter_Status.Name = "Panel_Tachymeter_Status"
        Me.Panel_Tachymeter_Status.Size = New System.Drawing.Size(20, 20)
        Me.Panel_Tachymeter_Status.TabIndex = 22
        '
        'gb_status_Camera
        '
        Me.gb_status_Camera.Controls.Add(Me.La_DeviceControl_Camerastatus2)
        Me.gb_status_Camera.Controls.Add(Me.Panel_DeviceControl_Camerastatus2)
        Me.gb_status_Camera.Location = New System.Drawing.Point(190, 506)
        Me.gb_status_Camera.Name = "gb_status_Camera"
        Me.gb_status_Camera.Size = New System.Drawing.Size(156, 69)
        Me.gb_status_Camera.TabIndex = 20
        Me.gb_status_Camera.TabStop = False
        Me.gb_status_Camera.Text = "Status Camera"
        '
        'La_DeviceControl_Camerastatus2
        '
        Me.La_DeviceControl_Camerastatus2.AutoSize = True
        Me.La_DeviceControl_Camerastatus2.Location = New System.Drawing.Point(32, 23)
        Me.La_DeviceControl_Camerastatus2.Name = "La_DeviceControl_Camerastatus2"
        Me.La_DeviceControl_Camerastatus2.Size = New System.Drawing.Size(61, 13)
        Me.La_DeviceControl_Camerastatus2.TabIndex = 22
        Me.La_DeviceControl_Camerastatus2.Text = "Disconnect"
        '
        'Panel_DeviceControl_Camerastatus2
        '
        Me.Panel_DeviceControl_Camerastatus2.BackColor = System.Drawing.Color.Red
        Me.Panel_DeviceControl_Camerastatus2.Location = New System.Drawing.Point(6, 19)
        Me.Panel_DeviceControl_Camerastatus2.Name = "Panel_DeviceControl_Camerastatus2"
        Me.Panel_DeviceControl_Camerastatus2.Size = New System.Drawing.Size(20, 20)
        Me.Panel_DeviceControl_Camerastatus2.TabIndex = 21
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ComboBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(159, 345)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(147, 51)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Resolution - Singleimage"
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(12, 19)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox4.TabIndex = 0
        '
        'gb_Camera_Timing
        '
        Me.gb_Camera_Timing.Location = New System.Drawing.Point(0, 0)
        Me.gb_Camera_Timing.Name = "gb_Camera_Timing"
        Me.gb_Camera_Timing.Size = New System.Drawing.Size(200, 100)
        Me.gb_Camera_Timing.TabIndex = 0
        Me.gb_Camera_Timing.TabStop = False
        '
        'gb_camera_image
        '
        Me.gb_camera_image.Controls.Add(Me.cb_camera_image_awb)
        Me.gb_camera_image.Controls.Add(Me.cb_camera_image_agc)
        Me.gb_camera_image.Controls.Add(Me.cb_camera_image_aes)
        Me.gb_camera_image.Location = New System.Drawing.Point(6, 249)
        Me.gb_camera_image.Name = "gb_camera_image"
        Me.gb_camera_image.Size = New System.Drawing.Size(200, 90)
        Me.gb_camera_image.TabIndex = 10
        Me.gb_camera_image.TabStop = False
        Me.gb_camera_image.Text = "Image"
        '
        'cb_camera_image_awb
        '
        Me.cb_camera_image_awb.AutoSize = True
        Me.cb_camera_image_awb.Location = New System.Drawing.Point(7, 67)
        Me.cb_camera_image_awb.Name = "cb_camera_image_awb"
        Me.cb_camera_image_awb.Size = New System.Drawing.Size(155, 17)
        Me.cb_camera_image_awb.TabIndex = 2
        Me.cb_camera_image_awb.Text = "AWB (Auto White Balance)"
        Me.cb_camera_image_awb.UseVisualStyleBackColor = True
        '
        'cb_camera_image_agc
        '
        Me.cb_camera_image_agc.AutoSize = True
        Me.cb_camera_image_agc.Location = New System.Drawing.Point(7, 43)
        Me.cb_camera_image_agc.Name = "cb_camera_image_agc"
        Me.cb_camera_image_agc.Size = New System.Drawing.Size(140, 17)
        Me.cb_camera_image_agc.TabIndex = 1
        Me.cb_camera_image_agc.Text = "AGC (Auto Gain Control)"
        Me.cb_camera_image_agc.UseVisualStyleBackColor = True
        '
        'cb_camera_image_aes
        '
        Me.cb_camera_image_aes.AutoSize = True
        Me.cb_camera_image_aes.Location = New System.Drawing.Point(7, 20)
        Me.cb_camera_image_aes.Name = "cb_camera_image_aes"
        Me.cb_camera_image_aes.Size = New System.Drawing.Size(162, 17)
        Me.cb_camera_image_aes.TabIndex = 0
        Me.cb_camera_image_aes.Text = "AES (Auto Exposure Shutter)"
        Me.cb_camera_image_aes.UseVisualStyleBackColor = True
        '
        'gb_camera_SizeLiveModus
        '
        Me.gb_camera_SizeLiveModus.Controls.Add(Me.ComboBox1)
        Me.gb_camera_SizeLiveModus.Location = New System.Drawing.Point(6, 345)
        Me.gb_camera_SizeLiveModus.Name = "gb_camera_SizeLiveModus"
        Me.gb_camera_SizeLiveModus.Size = New System.Drawing.Size(147, 51)
        Me.gb_camera_SizeLiveModus.TabIndex = 9
        Me.gb_camera_SizeLiveModus.TabStop = False
        Me.gb_camera_SizeLiveModus.Text = "Resolution - Livemodus"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(12, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(159, 345)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(147, 51)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Resolution - Singleimage"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(12, 19)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.TrackBar1)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Controls.Add(Me.Label18)
        Me.GroupBox5.Controls.Add(Me.TrackBar2)
        Me.GroupBox5.Controls.Add(Me.TrackBar3)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(328, 237)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Timing"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(276, 207)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(276, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(278, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(13, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(297, 187)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "100"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(297, 121)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(25, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "100"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(297, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(25, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "100"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(9, 178)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(280, 45)
        Me.TrackBar1.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 162)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Exposure time [ms]"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 96)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 13)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Frame rate [fps]"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 29)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(89, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Pixel clock [MHz]"
        '
        'TrackBar2
        '
        Me.TrackBar2.Location = New System.Drawing.Point(9, 112)
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(280, 45)
        Me.TrackBar2.TabIndex = 1
        '
        'TrackBar3
        '
        Me.TrackBar3.Location = New System.Drawing.Point(9, 45)
        Me.TrackBar3.Name = "TrackBar3"
        Me.TrackBar3.Size = New System.Drawing.Size(282, 45)
        Me.TrackBar3.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.CheckBox1)
        Me.GroupBox6.Controls.Add(Me.CheckBox2)
        Me.GroupBox6.Controls.Add(Me.CheckBox3)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 249)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(200, 90)
        Me.GroupBox6.TabIndex = 10
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Image"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(7, 67)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(155, 17)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "AWB (Auto White Balance)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(7, 43)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(140, 17)
        Me.CheckBox2.TabIndex = 1
        Me.CheckBox2.Text = "AGC (Auto Gain Control)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(7, 20)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(162, 17)
        Me.CheckBox3.TabIndex = 0
        Me.CheckBox3.Text = "AES (Auto Exposure Shutter)"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.ComboBox3)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 345)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(147, 51)
        Me.GroupBox7.TabIndex = 9
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Resolution - Livemodus"
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(12, 19)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox3.TabIndex = 0
        '
        'MoDiTaGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 723)
        Me.Controls.Add(Me.gb_measure)
        Me.Controls.Add(Me.gb_status_Camera)
        Me.Controls.Add(Me.TabControl3)
        Me.Controls.Add(Me.gb_Measure_StatusStation)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "MoDiTaGUI"
        Me.Text = "MoDiTa Control v0.002"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.Tab_DeviceControl.ResumeLayout(False)
        Me.gb_DeviceControl_totalstation.ResumeLayout(False)
        Me.gb_DeviceControl_TotalStation_ConnectToTotalStation.ResumeLayout(False)
        Me.gb_DeviceControl_Camera.ResumeLayout(False)
        Me.gb_DeviceControl_Camera.PerformLayout()
        Me.gb_livebild.ResumeLayout(False)
        Me.Tab_TotalStation.ResumeLayout(False)
        Me.GroupBox_Laserpointer.ResumeLayout(False)
        Me.GroupBox_Laserpointer.PerformLayout()
        Me.GroupBox_LockMode.ResumeLayout(False)
        Me.GroupBox_ATR.ResumeLayout(False)
        Me.GroupBox_ATR.PerformLayout()
        Me.gb_zenitRange.ResumeLayout(False)
        Me.gb_zenitRange.PerformLayout()
        Me.gb_edm_mode.ResumeLayout(False)
        Me.gb_edm_mode.PerformLayout()
        Me.Tab_TachyMove.ResumeLayout(False)
        Me.gb_lagewechsel.ResumeLayout(False)
        Me.gb_lagewechsel.PerformLayout()
        Me.gb_joystick.ResumeLayout(False)
        Me.gb_joystick.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        CType(Me.trbVelocityHz, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        CType(Me.trbVelocityVz, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb_moveabsolute.ResumeLayout(False)
        Me.gb_moveabsolute.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox_camera_image.ResumeLayout(False)
        Me.GroupBox_camera_image.PerformLayout()
        Me.GroupBox_camera_SizeLiveModus.ResumeLayout(False)
        Me.GroupBox_camera_SizeLiveModus.PerformLayout()
        Me.GroupBox_CameraTiming.ResumeLayout(False)
        Me.GroupBox_CameraTiming.PerformLayout()
        CType(Me.TrackBar_ExposureTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_FrameRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_PixelClock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox_SK_Solve.ResumeLayout(False)
        Me.GroupBox_SK_Solve.PerformLayout()
        Me.GroupBox_Field_of_View.ResumeLayout(False)
        Me.GroupBox_Field_of_View.PerformLayout()
        Me.GroupBox_SKTarget2.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox_SKTarget1.ResumeLayout(False)
        Me.GroupBox_Sk_target1_circle.ResumeLayout(False)
        Me.GroupBox_Sk_target1_circle.PerformLayout()
        Me.GroupBox_Sk_target1_center.ResumeLayout(False)
        Me.GroupBox_Sk_target1_center.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.GroupBox_Measure_Image.ResumeLayout(False)
        Me.GroupBox_Measure_Image.PerformLayout()
        Me.gb_zoommove.ResumeLayout(False)
        Me.gb_zoommove.PerformLayout()
        Me.gb_Snapshot.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        CType(Me.DGVData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.DGVLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_bv1.ResumeLayout(False)
        Me.uEye.ResumeLayout(False)
        Me.uEye.PerformLayout()
        Me.gb_property.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.AxuEyeCam1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb_measure.ResumeLayout(False)
        Me.gb_measure.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.gb_Measure_StatusStation.ResumeLayout(False)
        Me.gb_Measure_StatusStation.PerformLayout()
        Me.gb_status_Camera.ResumeLayout(False)
        Me.gb_status_Camera.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.gb_camera_image.ResumeLayout(False)
        Me.gb_camera_image.PerformLayout()
        Me.gb_camera_SizeLiveModus.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Tab_DeviceControl As System.Windows.Forms.TabPage
    Friend WithEvents Tab_TotalStation As System.Windows.Forms.TabPage
    Friend WithEvents gb_DeviceControl_totalstation As System.Windows.Forms.GroupBox
    Friend WithEvents gb_DeviceControl_TotalStation_ConnectToTotalStation As System.Windows.Forms.GroupBox
    Friend WithEvents cmbComports As System.Windows.Forms.ComboBox
    Friend WithEvents bu_deviceControl_totalStation_CloseComport As System.Windows.Forms.Button
    Friend WithEvents Tab_TachyMove As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btKompOff As System.Windows.Forms.Button
    Friend WithEvents btKompOn As System.Windows.Forms.Button
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents lbPositionTime As System.Windows.Forms.Label
    Friend WithEvents btSetPosTime As System.Windows.Forms.Button
    Friend WithEvents cbTimeTol As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lbPositionTolerance As System.Windows.Forms.Label
    Friend WithEvents cbPosTol As System.Windows.Forms.ComboBox
    Friend WithEvents btSetPosTol As System.Windows.Forms.Button
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents btSetAngleCorrections As System.Windows.Forms.Button
    Friend WithEvents cbHiKorr As System.Windows.Forms.CheckBox
    Friend WithEvents cbSaKorr As System.Windows.Forms.CheckBox
    Friend WithEvents cbZaKorr As System.Windows.Forms.CheckBox
    Friend WithEvents cbKaKorr As System.Windows.Forms.CheckBox
    Friend WithEvents gb_moveabsolute As System.Windows.Forms.GroupBox
    Friend WithEvents ch_precisionOn_1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btDriveAbs As System.Windows.Forms.Button
    Friend WithEvents tbHzAbsolut As System.Windows.Forms.TextBox
    Friend WithEvents tbVzAbsolut As System.Windows.Forms.TextBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents DGVData As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents DGVLog As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ts_project As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gb_livebild As System.Windows.Forms.GroupBox
    Friend WithEvents Bu_LiveModusStop As System.Windows.Forms.Button
    Friend WithEvents Bu_LiveModusStart As System.Windows.Forms.Button
    Friend WithEvents gb_Snapshot As System.Windows.Forms.GroupBox
    Friend WithEvents Bu_Snapshot As System.Windows.Forms.Button
    Friend WithEvents gb_DeviceControl_Camera As System.Windows.Forms.GroupBox
    Friend WithEvents Bu_DeviceControl_Camera_Disconnect As System.Windows.Forms.Button
    Friend WithEvents Bu_DeviceControl_Camera_connect As System.Windows.Forms.Button
    Friend WithEvents bu_export_data_as_csv As System.Windows.Forms.Button
    Friend WithEvents gb_joystick As System.Windows.Forms.GroupBox
    Friend WithEvents bu_stopmove As System.Windows.Forms.Button
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents lbVelocityHz As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents trbVelocityHz As System.Windows.Forms.TrackBar
    Friend WithEvents cbVelocityEqual As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents lbVelocityVz As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents trbVelocityVz As System.Windows.Forms.TrackBar
    Friend WithEvents btdownright As System.Windows.Forms.Button
    Friend WithEvents btupright As System.Windows.Forms.Button
    Friend WithEvents btdown As System.Windows.Forms.Button
    Friend WithEvents btupleft As System.Windows.Forms.Button
    Friend WithEvents btdownleft As System.Windows.Forms.Button
    Friend WithEvents btup As System.Windows.Forms.Button
    Friend WithEvents btright As System.Windows.Forms.Button
    Friend WithEvents btleft As System.Windows.Forms.Button
    Friend WithEvents gb_lagewechsel As System.Windows.Forms.GroupBox
    Friend WithEvents ch_precisionOn_2 As System.Windows.Forms.CheckBox
    Friend WithEvents btChangeFace As System.Windows.Forms.Button
    Friend WithEvents bu_export_log_as_csv As System.Windows.Forms.Button
    Friend WithEvents La_DeviceControl_Camerastatus As System.Windows.Forms.Label
    Friend WithEvents Panel_DeviceControl_Camerastatus As System.Windows.Forms.Panel
    Friend WithEvents gb_edm_mode As System.Windows.Forms.GroupBox
    Friend WithEvents rb_edmMode_RL As System.Windows.Forms.RadioButton
    Friend WithEvents rb_edmMode_IR As System.Windows.Forms.RadioButton
    Friend WithEvents gb_measure As System.Windows.Forms.GroupBox
    Friend WithEvents gb_Measure_StatusStation As System.Windows.Forms.GroupBox
    Friend WithEvents cb_Measure_saveImage As System.Windows.Forms.CheckBox
    Friend WithEvents cb_Measure_withoutSave As System.Windows.Forms.CheckBox
    Friend WithEvents Bu_Measure_All As System.Windows.Forms.Button
    Friend WithEvents Bu_Measure_Distance As System.Windows.Forms.Button
    Friend WithEvents Bu_Measure_Direction As System.Windows.Forms.Button
    Friend WithEvents La_Measure_pointname As System.Windows.Forms.Label
    Friend WithEvents tb_pointname As System.Windows.Forms.TextBox
    Friend WithEvents La_Measure_pointnumber As System.Windows.Forms.Label
    Friend WithEvents tb_pointnumber As System.Windows.Forms.TextBox
    Friend WithEvents La_ID_Status As System.Windows.Forms.Label
    Friend WithEvents La_Measure_id As System.Windows.Forms.Label
    Friend WithEvents gb_status_Camera As System.Windows.Forms.GroupBox
    Friend WithEvents La_DeviceControl_Camerastatus2 As System.Windows.Forms.Label
    Friend WithEvents Panel_DeviceControl_Camerastatus2 As System.Windows.Forms.Panel
    Friend WithEvents gb_zenitRange As System.Windows.Forms.GroupBox
    Friend WithEvents rb_vertical_range_camera As System.Windows.Forms.RadioButton
    Friend WithEvents rb_vertical_range_free As System.Windows.Forms.RadioButton
    Friend WithEvents La_vertical_range_face2 As System.Windows.Forms.Label
    Friend WithEvents La_vertical_range_face1 As System.Windows.Forms.Label
    Friend WithEvents tb_verticale_range_lage1 As System.Windows.Forms.TextBox
    Friend WithEvents tb_verticale_range_lage2 As System.Windows.Forms.TextBox
    Friend WithEvents rb_vertical_range_user_define As System.Windows.Forms.RadioButton
    Friend WithEvents bu_def_vertical_range As System.Windows.Forms.Button
    Friend WithEvents La_Tachymeterstatus As System.Windows.Forms.Label
    Friend WithEvents Panel_Tachymeter_Status As System.Windows.Forms.Panel
    Friend WithEvents La_theoinfos As System.Windows.Forms.Label
    Friend WithEvents tssLa_TachyErrorcodes As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents HWindowControl1 As HalconDotNet.HWindowControl
    Friend WithEvents ComboBox_Resolution_Single As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox_camera_image As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox_camera_SizeLiveModus As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox_Resolution_Live As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox_CameraTiming As System.Windows.Forms.GroupBox
    Friend WithEvents Label_ExposureTime_Max As System.Windows.Forms.Label
    Friend WithEvents Label_ExposureTime_Min As System.Windows.Forms.Label
    Friend WithEvents Label_FrameRate_Max As System.Windows.Forms.Label
    Friend WithEvents Label_FrameRate_Min As System.Windows.Forms.Label
    Friend WithEvents Label_PixelClock_Max As System.Windows.Forms.Label
    Friend WithEvents Label_PixelClock_Min As System.Windows.Forms.Label
    Friend WithEvents Label_ExposureTime_act As System.Windows.Forms.Label
    Friend WithEvents Label_FrameRate_act As System.Windows.Forms.Label
    Friend WithEvents Label_PixelClock_act As System.Windows.Forms.Label
    Friend WithEvents TrackBar_ExposureTime As System.Windows.Forms.TrackBar
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TrackBar_FrameRate As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar_PixelClock As System.Windows.Forms.TrackBar
    Friend WithEvents gb_zoommove As System.Windows.Forms.GroupBox
    Friend WithEvents bu_ZoomAndMoveReset As System.Windows.Forms.Button
    Friend WithEvents rb_move As System.Windows.Forms.RadioButton
    Friend WithEvents rb_zoom As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents gb_Camera_Timing As System.Windows.Forms.GroupBox
    'Friend WithEvents La_Camera_Timing_Exposure_max As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_Exposure_min As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_framerate_max As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_framerate_min As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_pixelclock_max As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_pixelclock_min As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_Exposure_act As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_framerate_act As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_Pixelclock_act As System.Windows.Forms.Label
    'Friend WithEvents Trackbar_Camera_Timing_Exposure As System.Windows.Forms.TrackBar
    'Friend WithEvents La_Camera_Timing_Exposure As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_FrameRate As System.Windows.Forms.Label
    'Friend WithEvents La_Camera_Timing_pixelClock As System.Windows.Forms.Label
    'Friend WithEvents Trackbar_Camera_Timing_FrameRate As System.Windows.Forms.TrackBar
    'Friend WithEvents TrackBar_Camera_Timing_PixelClock As System.Windows.Forms.TrackBar
    Friend WithEvents gb_camera_image As System.Windows.Forms.GroupBox
    Friend WithEvents cb_camera_image_awb As System.Windows.Forms.CheckBox
    Friend WithEvents cb_camera_image_agc As System.Windows.Forms.CheckBox
    Friend WithEvents cb_camera_image_aes As System.Windows.Forms.CheckBox
    Friend WithEvents gb_camera_SizeLiveModus As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar3 As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_SameResolution As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TabPage_bv1 As System.Windows.Forms.TabPage
    Friend WithEvents HWindowControl2 As HalconDotNet.HWindowControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox_Field_of_View As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Crosshair As System.Windows.Forms.Button
    Friend WithEvents Button_SelfCalibrationStart As System.Windows.Forms.Button
    Friend WithEvents ComboBox_crosshair_types As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox_SaveImage_selbstkalibrierung As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_SaveImage_crosshair As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_SaveErrorImages_self_calibration As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_Sk_typ As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox_SKTarget1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox_Sk_target1_center As System.Windows.Forms.GroupBox
    Friend WithEvents Button_sk_reg_target1_center As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TextBox_sk_target1_center_v As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_sk_target1_center_hz As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox_SK_Solve As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Solve_SK As System.Windows.Forms.Button
    Friend WithEvents Label_SK_Error_max As System.Windows.Forms.Label
    Friend WithEvents Label_SK_Error_min As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label_SK_Error_stddev As System.Windows.Forms.Label
    Friend WithEvents Label_SK_Error_mean As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox_Sk_target1_circle As System.Windows.Forms.GroupBox
    Friend WithEvents Button_sk_reg_target1_circle As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TextBox_sk_target1_circle_v As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_sk_target1_circle_hz As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox_SKTarget2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_sk_reg_target2_center As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TextBox_sk_target2_center_v As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_sk_target2_center_hz As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox_controlpoints As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label_Status_Sk As System.Windows.Forms.Label
    Friend WithEvents Panel_Status_Sk As System.Windows.Forms.Panel
    Friend WithEvents GroupBox_Measure_Image As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_Measure_Image As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Tachymeter_Off As System.Windows.Forms.Button
    Friend WithEvents Button_Tachymeter_on As System.Windows.Forms.Button
    Friend WithEvents GroupBox_ATR As System.Windows.Forms.GroupBox
    Friend WithEvents Button_FineAdjust As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TextBox_ATR_SearchRange_Hz As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_ATR_SearchRange_V As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox_ATR_Activate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox_LockMode As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Lockin_Stop As System.Windows.Forms.Button
    Friend WithEvents Button_Lockin As System.Windows.Forms.Button
    Friend WithEvents GroupBox_Laserpointer As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_Laserpointer_activate As System.Windows.Forms.CheckBox
    Friend WithEvents OpenProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents uEye As System.Windows.Forms.TabPage
    Friend WithEvents gb_property As System.Windows.Forms.GroupBox
    Friend WithEvents Bu_camera_property As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents AxuEyeCam1 As AxuEyeCamLib.AxuEyeCam
    Friend WithEvents ComboBox_uEye_binning As System.Windows.Forms.ComboBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_uEye_Subsampling As System.Windows.Forms.ComboBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox_Name_AVMessung As System.Windows.Forms.TextBox
    Friend WithEvents Button_SP_Reset As System.Windows.Forms.Button
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_25_Bilder_Komplett As System.Windows.Forms.Button
    Friend WithEvents Button_9_Bilder_Kreuz_Gedreht As System.Windows.Forms.Button
    Friend WithEvents Button_9_Bilder_Kreuz_Aufrecht As System.Windows.Forms.Button
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Messe_Rand As System.Windows.Forms.Button
    Friend WithEvents Button_Messe_Zentrum As System.Windows.Forms.Button
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Wartezeit As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Messzeit As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_Hz_Shift As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Vz_Shift As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Messung2_Shift As System.Windows.Forms.Button
    Friend WithEvents Button_Messung2_Zentrum As System.Windows.Forms.Button
    Friend WithEvents Button_Messung3_Shift As System.Windows.Forms.Button
    Friend WithEvents Button_Messung3_Zentrum As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Snapshot As System.Windows.Forms.Button
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents Label_X1 As System.Windows.Forms.Label
    Friend WithEvents Label_X2 As System.Windows.Forms.Label
    Friend WithEvents Label_X3 As System.Windows.Forms.Label
    Friend WithEvents Label_X4 As System.Windows.Forms.Label
    Friend WithEvents Label_X5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_KreuzEinschalten As System.Windows.Forms.CheckBox

End Class
