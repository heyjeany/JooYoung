namespace KeysightDynamicEVM
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestWithMXG = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtRepeatTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtFreqToTest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtVoltChange = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn6700OutputOff = new System.Windows.Forms.Button();
            this.btn6700OutputOn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMeasureEVM = new System.Windows.Forms.Button();
            this.btnSetupMXA = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTestStatus = new System.Windows.Forms.Button();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtMXGPowerLevelToTest = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentAndComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectInstrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLogSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.stsMainFrm = new System.Windows.Forms.StatusStrip();
            this.tslbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPrgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pgParameter = new System.Windows.Forms.PropertyGrid();
            this.rbLog = new System.Windows.Forms.RichTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.stsMainFrm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestWithMXG
            // 
            this.btnTestWithMXG.Location = new System.Drawing.Point(6, 25);
            this.btnTestWithMXG.Name = "btnTestWithMXG";
            this.btnTestWithMXG.Size = new System.Drawing.Size(194, 55);
            this.btnTestWithMXG.TabIndex = 0;
            this.btnTestWithMXG.Text = "Download Waveform";
            this.btnTestWithMXG.UseVisualStyleBackColor = true;
            this.btnTestWithMXG.Click += new System.EventHandler(this.btnTestWithMXG_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtRepeatTime);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(532, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(414, 115);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Measure Parameter";
            // 
            // txtRepeatTime
            // 
            this.txtRepeatTime.Location = new System.Drawing.Point(178, 35);
            this.txtRepeatTime.Name = "txtRepeatTime";
            this.txtRepeatTime.Size = new System.Drawing.Size(100, 26);
            this.txtRepeatTime.TabIndex = 1;
            this.txtRepeatTime.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Repeat per freq/Volt";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtFreqToTest);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(15, 146);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(501, 118);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Frequency Change";
            // 
            // txtFreqToTest
            // 
            this.txtFreqToTest.Location = new System.Drawing.Point(26, 51);
            this.txtFreqToTest.Multiline = true;
            this.txtFreqToTest.Name = "txtFreqToTest";
            this.txtFreqToTest.Size = new System.Drawing.Size(464, 47);
            this.txtFreqToTest.TabIndex = 2;
            this.txtFreqToTest.Text = "5180,5190,5460";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Frequency to Test(Separated by Comma)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtVoltChange);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(15, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(501, 115);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Voltage Change";
            // 
            // txtVoltChange
            // 
            this.txtVoltChange.Location = new System.Drawing.Point(26, 71);
            this.txtVoltChange.Name = "txtVoltChange";
            this.txtVoltChange.Size = new System.Drawing.Size(464, 26);
            this.txtVoltChange.TabIndex = 1;
            this.txtVoltChange.Text = "2.8,2.7,2.6,2.5,2.4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Volt to Test(Separated by Comma)";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1274, 441);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MXG xSA Power Supply Debug";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn6700OutputOff);
            this.groupBox3.Controls.Add(this.btn6700OutputOn);
            this.groupBox3.Location = new System.Drawing.Point(459, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 258);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "N6700x";
            // 
            // btn6700OutputOff
            // 
            this.btn6700OutputOff.Location = new System.Drawing.Point(6, 86);
            this.btn6700OutputOff.Name = "btn6700OutputOff";
            this.btn6700OutputOff.Size = new System.Drawing.Size(192, 49);
            this.btn6700OutputOff.TabIndex = 1;
            this.btn6700OutputOff.Text = "Output Off";
            this.btn6700OutputOff.UseVisualStyleBackColor = true;
            this.btn6700OutputOff.Click += new System.EventHandler(this.btn6700OutputOff_Click);
            // 
            // btn6700OutputOn
            // 
            this.btn6700OutputOn.Location = new System.Drawing.Point(6, 29);
            this.btn6700OutputOn.Name = "btn6700OutputOn";
            this.btn6700OutputOn.Size = new System.Drawing.Size(192, 49);
            this.btn6700OutputOn.TabIndex = 0;
            this.btn6700OutputOn.Text = "Output On";
            this.btn6700OutputOn.UseVisualStyleBackColor = true;
            this.btn6700OutputOn.Click += new System.EventHandler(this.btn6700OutputOn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMeasureEVM);
            this.groupBox2.Controls.Add(this.btnSetupMXA);
            this.groupBox2.Location = new System.Drawing.Point(248, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 258);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "xSA";
            // 
            // btnMeasureEVM
            // 
            this.btnMeasureEVM.Location = new System.Drawing.Point(6, 85);
            this.btnMeasureEVM.Name = "btnMeasureEVM";
            this.btnMeasureEVM.Size = new System.Drawing.Size(160, 49);
            this.btnMeasureEVM.TabIndex = 1;
            this.btnMeasureEVM.Text = "Measure EVM";
            this.btnMeasureEVM.UseVisualStyleBackColor = true;
            this.btnMeasureEVM.Click += new System.EventHandler(this.btnMeasureEVM_Click);
            // 
            // btnSetupMXA
            // 
            this.btnSetupMXA.Location = new System.Drawing.Point(6, 26);
            this.btnSetupMXA.Name = "btnSetupMXA";
            this.btnSetupMXA.Size = new System.Drawing.Size(160, 52);
            this.btnSetupMXA.TabIndex = 0;
            this.btnSetupMXA.Text = "Setup xSA";
            this.btnSetupMXA.UseVisualStyleBackColor = true;
            this.btnSetupMXA.Click += new System.EventHandler(this.btnSetupMXA_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestWithMXG);
            this.groupBox1.Location = new System.Drawing.Point(20, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 258);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SigGen";
            // 
            // btnTestStatus
            // 
            this.btnTestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestStatus.Location = new System.Drawing.Point(532, 146);
            this.btnTestStatus.Name = "btnTestStatus";
            this.btnTestStatus.Size = new System.Drawing.Size(414, 118);
            this.btnTestStatus.TabIndex = 3;
            this.btnTestStatus.Text = "START TEST";
            this.btnTestStatus.UseVisualStyleBackColor = true;
            this.btnTestStatus.Click += new System.EventHandler(this.btnTestStatus_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Size = new System.Drawing.Size(1282, 283);
            this.splitContainer4.SplitterDistance = 201;
            this.splitContainer4.SplitterWidth = 6;
            this.splitContainer4.TabIndex = 0;
            // 
            // txtMXGPowerLevelToTest
            // 
            this.txtMXGPowerLevelToTest.Location = new System.Drawing.Point(26, 51);
            this.txtMXGPowerLevelToTest.Multiline = true;
            this.txtMXGPowerLevelToTest.Name = "txtMXGPowerLevelToTest";
            this.txtMXGPowerLevelToTest.Size = new System.Drawing.Size(464, 47);
            this.txtMXGPowerLevelToTest.TabIndex = 2;
            this.txtMXGPowerLevelToTest.Text = "-19,-15,-13,-10,-7,-5,-2,0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.instrumentAndComponentToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1809, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSetupToolStripMenuItem,
            this.saveSetupToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.systemToolStripMenuItem.Text = "&System";
            // 
            // loadSetupToolStripMenuItem
            // 
            this.loadSetupToolStripMenuItem.Name = "loadSetupToolStripMenuItem";
            this.loadSetupToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.loadSetupToolStripMenuItem.Text = "&Load Setup";
            this.loadSetupToolStripMenuItem.Click += new System.EventHandler(this.loadSetupToolStripMenuItem_Click);
            // 
            // saveSetupToolStripMenuItem
            // 
            this.saveSetupToolStripMenuItem.Name = "saveSetupToolStripMenuItem";
            this.saveSetupToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.saveSetupToolStripMenuItem.Text = "&Save Setup";
            this.saveSetupToolStripMenuItem.Click += new System.EventHandler(this.saveSetupToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // instrumentAndComponentToolStripMenuItem
            // 
            this.instrumentAndComponentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectInstrumentToolStripMenuItem});
            this.instrumentAndComponentToolStripMenuItem.Name = "instrumentAndComponentToolStripMenuItem";
            this.instrumentAndComponentToolStripMenuItem.Size = new System.Drawing.Size(249, 29);
            this.instrumentAndComponentToolStripMenuItem.Text = "Instrument and &Component";
            // 
            // connectInstrumentToolStripMenuItem
            // 
            this.connectInstrumentToolStripMenuItem.Name = "connectInstrumentToolStripMenuItem";
            this.connectInstrumentToolStripMenuItem.Size = new System.Drawing.Size(256, 30);
            this.connectInstrumentToolStripMenuItem.Text = "C&onnect Instrument";
            this.connectInstrumentToolStripMenuItem.Click += new System.EventHandler(this.connectInstrumentToolStripMenuItem_Click);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLogSetting});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.loggingToolStripMenuItem.Text = "Logging";
            // 
            // tsLogSetting
            // 
            this.tsLogSetting.Name = "tsLogSetting";
            this.tsLogSetting.Size = new System.Drawing.Size(155, 30);
            this.tsLogSetting.Text = "Disable";
            this.tsLogSetting.Click += new System.EventHandler(this.tsLogSetting_Click);
            // 
            // stsMainFrm
            // 
            this.stsMainFrm.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stsMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbState,
            this.tsPrgressBar});
            this.stsMainFrm.Location = new System.Drawing.Point(0, 798);
            this.stsMainFrm.Name = "stsMainFrm";
            this.stsMainFrm.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.stsMainFrm.Size = new System.Drawing.Size(1809, 35);
            this.stsMainFrm.TabIndex = 5;
            this.stsMainFrm.Text = "statusStrip1";
            // 
            // tslbState
            // 
            this.tslbState.AutoSize = false;
            this.tslbState.BackColor = System.Drawing.Color.Black;
            this.tslbState.ForeColor = System.Drawing.Color.White;
            this.tslbState.Name = "tslbState";
            this.tslbState.Size = new System.Drawing.Size(800, 30);
            this.tslbState.Text = "...";
            this.tslbState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsPrgressBar
            // 
            this.tsPrgressBar.AutoSize = false;
            this.tsPrgressBar.Name = "tsPrgressBar";
            this.tsPrgressBar.Size = new System.Drawing.Size(150, 29);
            this.tsPrgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1809, 763);
            this.splitContainer1.SplitterDistance = 521;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 7;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pgParameter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rbLog);
            this.splitContainer2.Size = new System.Drawing.Size(521, 763);
            this.splitContainer2.SplitterDistance = 409;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 4;
            // 
            // pgParameter
            // 
            this.pgParameter.BackColor = System.Drawing.Color.Black;
            this.pgParameter.CategoryForeColor = System.Drawing.Color.White;
            this.pgParameter.CategorySplitterColor = System.Drawing.Color.Red;
            this.pgParameter.CommandsBorderColor = System.Drawing.Color.Red;
            this.pgParameter.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgParameter.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgParameter.HelpVisible = false;
            this.pgParameter.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgParameter.Location = new System.Drawing.Point(0, 0);
            this.pgParameter.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pgParameter.Name = "pgParameter";
            this.pgParameter.Size = new System.Drawing.Size(521, 409);
            this.pgParameter.TabIndex = 5;
            this.pgParameter.ToolbarVisible = false;
            this.pgParameter.ViewBackColor = System.Drawing.Color.Black;
            this.pgParameter.ViewBorderColor = System.Drawing.Color.Red;
            this.pgParameter.ViewForeColor = System.Drawing.Color.White;
            // 
            // rbLog
            // 
            this.rbLog.BackColor = System.Drawing.Color.Black;
            this.rbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLog.ForeColor = System.Drawing.Color.White;
            this.rbLog.Location = new System.Drawing.Point(0, 0);
            this.rbLog.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.rbLog.Name = "rbLog";
            this.rbLog.Size = new System.Drawing.Size(521, 348);
            this.rbLog.TabIndex = 6;
            this.rbLog.Text = "";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(1282, 763);
            this.splitContainer3.SplitterDistance = 474;
            this.splitContainer3.SplitterWidth = 6;
            this.splitContainer3.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1282, 474);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.btnTestStatus);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1274, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TestVector Control";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtMXGPowerLevelToTest);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Location = new System.Drawing.Point(15, 270);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(501, 118);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Power Level Change";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(352, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "MXG Power Level to Test(Separated by Comma)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1809, 833);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.stsMainFrm);
            this.Name = "Form1";
            this.Text = "Keysight Dynamic EVM Evaluation V1.4";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stsMainFrm.ResumeLayout(false);
            this.stsMainFrm.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestWithMXG;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtRepeatTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtFreqToTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtVoltChange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn6700OutputOff;
        private System.Windows.Forms.Button btn6700OutputOn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMeasureEVM;
        private System.Windows.Forms.Button btnSetupMXA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTestStatus;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox txtMXGPowerLevelToTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentAndComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectInstrumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsLogSetting;
        private System.Windows.Forms.StatusStrip stsMainFrm;
        private System.Windows.Forms.ToolStripStatusLabel tslbState;
        private System.Windows.Forms.ToolStripProgressBar tsPrgressBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid pgParameter;
        private System.Windows.Forms.RichTextBox rbLog;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label4;
    }
}

