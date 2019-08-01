namespace ET4M941xA
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectInstrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsMainFrm = new System.Windows.Forms.StatusStrip();
            this.tslbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pgConnectInitStatus = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb137_60Mhz = new System.Windows.Forms.RadioButton();
            this.btnApplySetup = new System.Windows.Forms.Button();
            this.pgETSetup = new System.Windows.Forms.PropertyGrid();
            this.rb250Mhz = new System.Windows.Forms.RadioButton();
            this.rb199_68Mhz = new System.Windows.Forms.RadioButton();
            this.rb122_88Mhz = new System.Windows.Forms.RadioButton();
            this.rb30_72Mhz = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAWGHWVer = new System.Windows.Forms.ComboBox();
            this.chkStopStartAfterQueue = new System.Windows.Forms.CheckBox();
            this.btnRestartWaveform = new System.Windows.Forms.Button();
            this.chkUseDummyWaveform = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtVXTBBDelay = new System.Windows.Forms.NumericUpDown();
            this.btnSetVXTBBDelay = new System.Windows.Forms.Button();
            this.chkAWGSingle = new System.Windows.Forms.CheckBox();
            this.chkVXTSingle = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnStopWaveform = new System.Windows.Forms.Button();
            this.btnStartWaveform = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAWGSyncFreqCheck = new System.Windows.Forms.Button();
            this.chkReStartWaveformAfterStop = new System.Windows.Forms.CheckBox();
            this.chkAWGOnlyInit = new System.Windows.Forms.CheckBox();
            this.btnDebugStop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGaterOffset = new System.Windows.Forms.Button();
            this.btnDebugGenPXI0_2 = new System.Windows.Forms.Button();
            this.btnDebugStartPlayback = new System.Windows.Forms.Button();
            this.btnDebugSynchronizeModule = new System.Windows.Forms.Button();
            this.btnDebugQueueWFM = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWaveformSelection = new System.Windows.Forms.ComboBox();
            this.btnLoadWaveformIntoMemory = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.rbLog = new System.Windows.Forms.RichTextBox();
            this.rbLog2 = new System.Windows.Forms.RichTextBox();
            this.btnPXI0FromRef = new System.Windows.Forms.Button();
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
            this.groupBox3.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVXTBBDelay)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.instrumentToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1225, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSetupToolStripMenuItem,
            this.saveSetupToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // loadSetupToolStripMenuItem
            // 
            this.loadSetupToolStripMenuItem.Name = "loadSetupToolStripMenuItem";
            this.loadSetupToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadSetupToolStripMenuItem.Text = "Load Setup";
            // 
            // saveSetupToolStripMenuItem
            // 
            this.saveSetupToolStripMenuItem.Name = "saveSetupToolStripMenuItem";
            this.saveSetupToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveSetupToolStripMenuItem.Text = "Save Setup";
            // 
            // instrumentToolStripMenuItem
            // 
            this.instrumentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectInstrumentToolStripMenuItem});
            this.instrumentToolStripMenuItem.Name = "instrumentToolStripMenuItem";
            this.instrumentToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.instrumentToolStripMenuItem.Text = "Instrument";
            // 
            // connectInstrumentToolStripMenuItem
            // 
            this.connectInstrumentToolStripMenuItem.Name = "connectInstrumentToolStripMenuItem";
            this.connectInstrumentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectInstrumentToolStripMenuItem.Text = "Connect Instrument";
            this.connectInstrumentToolStripMenuItem.Click += new System.EventHandler(this.connectInstrumentToolStripMenuItem_Click);
            // 
            // stsMainFrm
            // 
            this.stsMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbStatus});
            this.stsMainFrm.Location = new System.Drawing.Point(0, 611);
            this.stsMainFrm.Name = "stsMainFrm";
            this.stsMainFrm.Size = new System.Drawing.Size(1225, 22);
            this.stsMainFrm.TabIndex = 1;
            this.stsMainFrm.Text = "statusStrip1";
            // 
            // tslbStatus
            // 
            this.tslbStatus.Name = "tslbStatus";
            this.tslbStatus.Size = new System.Drawing.Size(39, 17);
            this.tslbStatus.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pgConnectInitStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1225, 587);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 2;
            // 
            // pgConnectInitStatus
            // 
            this.pgConnectInitStatus.BackColor = System.Drawing.Color.Black;
            this.pgConnectInitStatus.CategoryForeColor = System.Drawing.Color.White;
            this.pgConnectInitStatus.CategorySplitterColor = System.Drawing.Color.Red;
            this.pgConnectInitStatus.CommandsBorderColor = System.Drawing.Color.Red;
            this.pgConnectInitStatus.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgConnectInitStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgConnectInitStatus.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgConnectInitStatus.HelpVisible = false;
            this.pgConnectInitStatus.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgConnectInitStatus.Location = new System.Drawing.Point(0, 0);
            this.pgConnectInitStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pgConnectInitStatus.Name = "pgConnectInitStatus";
            this.pgConnectInitStatus.Size = new System.Drawing.Size(287, 587);
            this.pgConnectInitStatus.TabIndex = 5;
            this.pgConnectInitStatus.ToolbarVisible = false;
            this.pgConnectInitStatus.ViewBackColor = System.Drawing.Color.Black;
            this.pgConnectInitStatus.ViewBorderColor = System.Drawing.Color.Red;
            this.pgConnectInitStatus.ViewForeColor = System.Drawing.Color.White;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.chkStopStartAfterQueue);
            this.splitContainer2.Panel1.Controls.Add(this.btnRestartWaveform);
            this.splitContainer2.Panel1.Controls.Add(this.chkUseDummyWaveform);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox10);
            this.splitContainer2.Panel1.Controls.Add(this.chkAWGSingle);
            this.splitContainer2.Panel1.Controls.Add(this.chkVXTSingle);
            this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
            this.splitContainer2.Panel1.Controls.Add(this.btnStopWaveform);
            this.splitContainer2.Panel1.Controls.Add(this.btnStartWaveform);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(934, 587);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb137_60Mhz);
            this.groupBox3.Controls.Add(this.btnApplySetup);
            this.groupBox3.Controls.Add(this.pgETSetup);
            this.groupBox3.Controls.Add(this.rb250Mhz);
            this.groupBox3.Controls.Add(this.rb199_68Mhz);
            this.groupBox3.Controls.Add(this.rb122_88Mhz);
            this.groupBox3.Controls.Add(this.rb30_72Mhz);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbAWGHWVer);
            this.groupBox3.Location = new System.Drawing.Point(217, 154);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(470, 175);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Demo for Specific Frequency";
            // 
            // rb137_60Mhz
            // 
            this.rb137_60Mhz.AutoSize = true;
            this.rb137_60Mhz.Location = new System.Drawing.Point(5, 100);
            this.rb137_60Mhz.Name = "rb137_60Mhz";
            this.rb137_60Mhz.Size = new System.Drawing.Size(81, 17);
            this.rb137_60Mhz.TabIndex = 8;
            this.rb137_60Mhz.TabStop = true;
            this.rb137_60Mhz.Text = "137.60 Mhz";
            this.rb137_60Mhz.UseVisualStyleBackColor = true;
            this.rb137_60Mhz.CheckedChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // btnApplySetup
            // 
            this.btnApplySetup.Location = new System.Drawing.Point(257, 16);
            this.btnApplySetup.Name = "btnApplySetup";
            this.btnApplySetup.Size = new System.Drawing.Size(146, 28);
            this.btnApplySetup.TabIndex = 7;
            this.btnApplySetup.Text = "Apply Setup";
            this.btnApplySetup.UseVisualStyleBackColor = true;
            this.btnApplySetup.Click += new System.EventHandler(this.btnApplySetup_Click);
            // 
            // pgETSetup
            // 
            this.pgETSetup.BackColor = System.Drawing.Color.Black;
            this.pgETSetup.CategoryForeColor = System.Drawing.Color.White;
            this.pgETSetup.CategorySplitterColor = System.Drawing.Color.Red;
            this.pgETSetup.CommandsBorderColor = System.Drawing.Color.Red;
            this.pgETSetup.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgETSetup.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgETSetup.HelpVisible = false;
            this.pgETSetup.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgETSetup.Location = new System.Drawing.Point(126, 52);
            this.pgETSetup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pgETSetup.Name = "pgETSetup";
            this.pgETSetup.Size = new System.Drawing.Size(337, 115);
            this.pgETSetup.TabIndex = 6;
            this.pgETSetup.ToolbarVisible = false;
            this.pgETSetup.ViewBackColor = System.Drawing.Color.Black;
            this.pgETSetup.ViewBorderColor = System.Drawing.Color.Red;
            this.pgETSetup.ViewForeColor = System.Drawing.Color.White;
            // 
            // rb250Mhz
            // 
            this.rb250Mhz.AutoSize = true;
            this.rb250Mhz.Location = new System.Drawing.Point(5, 146);
            this.rb250Mhz.Name = "rb250Mhz";
            this.rb250Mhz.Size = new System.Drawing.Size(66, 17);
            this.rb250Mhz.TabIndex = 5;
            this.rb250Mhz.TabStop = true;
            this.rb250Mhz.Text = "250 Mhz";
            this.rb250Mhz.UseVisualStyleBackColor = true;
            this.rb250Mhz.CheckedChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // rb199_68Mhz
            // 
            this.rb199_68Mhz.AutoSize = true;
            this.rb199_68Mhz.Location = new System.Drawing.Point(5, 123);
            this.rb199_68Mhz.Name = "rb199_68Mhz";
            this.rb199_68Mhz.Size = new System.Drawing.Size(81, 17);
            this.rb199_68Mhz.TabIndex = 4;
            this.rb199_68Mhz.TabStop = true;
            this.rb199_68Mhz.Text = "199.68 Mhz";
            this.rb199_68Mhz.UseVisualStyleBackColor = true;
            this.rb199_68Mhz.CheckedChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // rb122_88Mhz
            // 
            this.rb122_88Mhz.AutoSize = true;
            this.rb122_88Mhz.Location = new System.Drawing.Point(5, 80);
            this.rb122_88Mhz.Name = "rb122_88Mhz";
            this.rb122_88Mhz.Size = new System.Drawing.Size(81, 17);
            this.rb122_88Mhz.TabIndex = 3;
            this.rb122_88Mhz.TabStop = true;
            this.rb122_88Mhz.Text = "122.88 Mhz";
            this.rb122_88Mhz.UseVisualStyleBackColor = true;
            this.rb122_88Mhz.CheckedChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // rb30_72Mhz
            // 
            this.rb30_72Mhz.AutoSize = true;
            this.rb30_72Mhz.Location = new System.Drawing.Point(5, 57);
            this.rb30_72Mhz.Name = "rb30_72Mhz";
            this.rb30_72Mhz.Size = new System.Drawing.Size(75, 17);
            this.rb30_72Mhz.TabIndex = 2;
            this.rb30_72Mhz.TabStop = true;
            this.rb30_72Mhz.Text = "30.72 Mhz";
            this.rb30_72Mhz.UseVisualStyleBackColor = true;
            this.rb30_72Mhz.CheckedChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AWG HW Ver";
            // 
            // cbAWGHWVer
            // 
            this.cbAWGHWVer.FormattingEnabled = true;
            this.cbAWGHWVer.Items.AddRange(new object[] {
            "Ver 3.x or older",
            "Ver 4.x or newer"});
            this.cbAWGHWVer.Location = new System.Drawing.Point(95, 23);
            this.cbAWGHWVer.Name = "cbAWGHWVer";
            this.cbAWGHWVer.Size = new System.Drawing.Size(144, 21);
            this.cbAWGHWVer.TabIndex = 0;
            this.cbAWGHWVer.SelectedIndexChanged += new System.EventHandler(this.Event_ETSetupChange);
            // 
            // chkStopStartAfterQueue
            // 
            this.chkStopStartAfterQueue.AutoSize = true;
            this.chkStopStartAfterQueue.Checked = true;
            this.chkStopStartAfterQueue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStopStartAfterQueue.Location = new System.Drawing.Point(216, 130);
            this.chkStopStartAfterQueue.Name = "chkStopStartAfterQueue";
            this.chkStopStartAfterQueue.Size = new System.Drawing.Size(230, 17);
            this.chkStopStartAfterQueue.TabIndex = 56;
            this.chkStopStartAfterQueue.Text = "Start Stop and Start after Queue Waveform";
            this.chkStopStartAfterQueue.UseVisualStyleBackColor = true;
            // 
            // btnRestartWaveform
            // 
            this.btnRestartWaveform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRestartWaveform.Location = new System.Drawing.Point(17, 117);
            this.btnRestartWaveform.Name = "btnRestartWaveform";
            this.btnRestartWaveform.Size = new System.Drawing.Size(193, 43);
            this.btnRestartWaveform.TabIndex = 55;
            this.btnRestartWaveform.Text = "Restart Waveform";
            this.btnRestartWaveform.UseVisualStyleBackColor = false;
            this.btnRestartWaveform.Click += new System.EventHandler(this.btnRestartWaveform_Click);
            // 
            // chkUseDummyWaveform
            // 
            this.chkUseDummyWaveform.AutoSize = true;
            this.chkUseDummyWaveform.Location = new System.Drawing.Point(216, 109);
            this.chkUseDummyWaveform.Name = "chkUseDummyWaveform";
            this.chkUseDummyWaveform.Size = new System.Drawing.Size(135, 17);
            this.chkUseDummyWaveform.TabIndex = 54;
            this.chkUseDummyWaveform.Text = "Use Dummy Waveform";
            this.chkUseDummyWaveform.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtVXTBBDelay);
            this.groupBox10.Controls.Add(this.btnSetVXTBBDelay);
            this.groupBox10.Location = new System.Drawing.Point(17, 211);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox10.Size = new System.Drawing.Size(170, 65);
            this.groupBox10.TabIndex = 53;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Baseband Delay";
            // 
            // txtVXTBBDelay
            // 
            this.txtVXTBBDelay.DecimalPlaces = 3;
            this.txtVXTBBDelay.Location = new System.Drawing.Point(105, 29);
            this.txtVXTBBDelay.Margin = new System.Windows.Forms.Padding(2);
            this.txtVXTBBDelay.Maximum = new decimal(new int[] {
            249,
            0,
            0,
            0});
            this.txtVXTBBDelay.Minimum = new decimal(new int[] {
            249,
            0,
            0,
            -2147483648});
            this.txtVXTBBDelay.Name = "txtVXTBBDelay";
            this.txtVXTBBDelay.Size = new System.Drawing.Size(59, 20);
            this.txtVXTBBDelay.TabIndex = 45;
            this.txtVXTBBDelay.ValueChanged += new System.EventHandler(this.txtVXTBBDelay_ValueChanged);
            // 
            // btnSetVXTBBDelay
            // 
            this.btnSetVXTBBDelay.BackColor = System.Drawing.Color.Silver;
            this.btnSetVXTBBDelay.Location = new System.Drawing.Point(8, 23);
            this.btnSetVXTBBDelay.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetVXTBBDelay.Name = "btnSetVXTBBDelay";
            this.btnSetVXTBBDelay.Size = new System.Drawing.Size(93, 29);
            this.btnSetVXTBBDelay.TabIndex = 44;
            this.btnSetVXTBBDelay.Text = "Baseband Delay";
            this.btnSetVXTBBDelay.UseVisualStyleBackColor = false;
            this.btnSetVXTBBDelay.Click += new System.EventHandler(this.btnSetVXTBBDelay_Click);
            // 
            // chkAWGSingle
            // 
            this.chkAWGSingle.AutoSize = true;
            this.chkAWGSingle.Location = new System.Drawing.Point(216, 89);
            this.chkAWGSingle.Name = "chkAWGSingle";
            this.chkAWGSingle.Size = new System.Drawing.Size(90, 17);
            this.chkAWGSingle.TabIndex = 51;
            this.chkAWGSingle.Text = "AWG Single?";
            this.chkAWGSingle.UseVisualStyleBackColor = true;
            // 
            // chkVXTSingle
            // 
            this.chkVXTSingle.AutoSize = true;
            this.chkVXTSingle.Enabled = false;
            this.chkVXTSingle.Location = new System.Drawing.Point(216, 69);
            this.chkVXTSingle.Name = "chkVXTSingle";
            this.chkVXTSingle.Size = new System.Drawing.Size(91, 17);
            this.chkVXTSingle.TabIndex = 50;
            this.chkVXTSingle.Text = "VXT2 Single?";
            this.chkVXTSingle.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(466, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(221, 103);
            this.richTextBox1.TabIndex = 49;
            this.richTextBox1.Text = "Make Sure AWG and VXT2 is in same Segment. PXI0 is used for HVI, PXI2 is used for" +
    " AWG Sync Output Trigger from Source. PXI1 is used for VXT2 Sync output from Sou" +
    "rce when Playing ARB";
            // 
            // btnStopWaveform
            // 
            this.btnStopWaveform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStopWaveform.Location = new System.Drawing.Point(15, 166);
            this.btnStopWaveform.Name = "btnStopWaveform";
            this.btnStopWaveform.Size = new System.Drawing.Size(195, 40);
            this.btnStopWaveform.TabIndex = 48;
            this.btnStopWaveform.Text = "Stop Waveform";
            this.btnStopWaveform.UseVisualStyleBackColor = false;
            this.btnStopWaveform.Click += new System.EventHandler(this.btnStopWaveform_Click);
            // 
            // btnStartWaveform
            // 
            this.btnStartWaveform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnStartWaveform.Location = new System.Drawing.Point(17, 68);
            this.btnStartWaveform.Name = "btnStartWaveform";
            this.btnStartWaveform.Size = new System.Drawing.Size(193, 42);
            this.btnStartWaveform.TabIndex = 47;
            this.btnStartWaveform.Text = "Queue and Start Waveform";
            this.btnStartWaveform.UseVisualStyleBackColor = false;
            this.btnStartWaveform.Click += new System.EventHandler(this.btnStartWaveform_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnPXI0FromRef);
            this.groupBox5.Controls.Add(this.btnAWGSyncFreqCheck);
            this.groupBox5.Controls.Add(this.chkReStartWaveformAfterStop);
            this.groupBox5.Controls.Add(this.chkAWGOnlyInit);
            this.groupBox5.Controls.Add(this.btnDebugStop);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.btnDebugGenPXI0_2);
            this.groupBox5.Controls.Add(this.btnDebugStartPlayback);
            this.groupBox5.Controls.Add(this.btnDebugSynchronizeModule);
            this.groupBox5.Controls.Add(this.btnDebugQueueWFM);
            this.groupBox5.Location = new System.Drawing.Point(693, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 271);
            this.groupBox5.TabIndex = 46;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Debug";
            // 
            // btnAWGSyncFreqCheck
            // 
            this.btnAWGSyncFreqCheck.Location = new System.Drawing.Point(100, 63);
            this.btnAWGSyncFreqCheck.Name = "btnAWGSyncFreqCheck";
            this.btnAWGSyncFreqCheck.Size = new System.Drawing.Size(102, 43);
            this.btnAWGSyncFreqCheck.TabIndex = 47;
            this.btnAWGSyncFreqCheck.Text = "Sync Frequency List Up";
            this.btnAWGSyncFreqCheck.UseVisualStyleBackColor = true;
            this.btnAWGSyncFreqCheck.Click += new System.EventHandler(this.btnAWGSyncFreqCheck_Click);
            // 
            // chkReStartWaveformAfterStop
            // 
            this.chkReStartWaveformAfterStop.AutoSize = true;
            this.chkReStartWaveformAfterStop.Location = new System.Drawing.Point(7, 240);
            this.chkReStartWaveformAfterStop.Name = "chkReStartWaveformAfterStop";
            this.chkReStartWaveformAfterStop.Size = new System.Drawing.Size(164, 17);
            this.chkReStartWaveformAfterStop.TabIndex = 42;
            this.chkReStartWaveformAfterStop.Text = "ReStart Waveform After Stop";
            this.chkReStartWaveformAfterStop.UseVisualStyleBackColor = true;
            // 
            // chkAWGOnlyInit
            // 
            this.chkAWGOnlyInit.AutoSize = true;
            this.chkAWGOnlyInit.Location = new System.Drawing.Point(100, 25);
            this.chkAWGOnlyInit.Name = "chkAWGOnlyInit";
            this.chkAWGOnlyInit.Size = new System.Drawing.Size(76, 17);
            this.chkAWGOnlyInit.TabIndex = 41;
            this.chkAWGOnlyInit.Text = "AWG Only";
            this.chkAWGOnlyInit.UseVisualStyleBackColor = true;
            // 
            // btnDebugStop
            // 
            this.btnDebugStop.Location = new System.Drawing.Point(7, 192);
            this.btnDebugStop.Name = "btnDebugStop";
            this.btnDebugStop.Size = new System.Drawing.Size(87, 42);
            this.btnDebugStop.TabIndex = 40;
            this.btnDebugStop.Text = "6. Stop Waveform";
            this.btnDebugStop.UseVisualStyleBackColor = true;
            this.btnDebugStop.Click += new System.EventHandler(this.btnDebugStop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGaterOffset);
            this.groupBox2.Location = new System.Drawing.Point(99, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 76);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scope Control";
            // 
            // btnGaterOffset
            // 
            this.btnGaterOffset.Location = new System.Drawing.Point(6, 22);
            this.btnGaterOffset.Name = "btnGaterOffset";
            this.btnGaterOffset.Size = new System.Drawing.Size(102, 35);
            this.btnGaterOffset.TabIndex = 0;
            this.btnGaterOffset.Text = "Gather Offset";
            this.btnGaterOffset.UseVisualStyleBackColor = true;
            this.btnGaterOffset.Click += new System.EventHandler(this.btnGaterOffset_Click);
            // 
            // btnDebugGenPXI0_2
            // 
            this.btnDebugGenPXI0_2.Location = new System.Drawing.Point(7, 149);
            this.btnDebugGenPXI0_2.Name = "btnDebugGenPXI0_2";
            this.btnDebugGenPXI0_2.Size = new System.Drawing.Size(87, 42);
            this.btnDebugGenPXI0_2.TabIndex = 39;
            this.btnDebugGenPXI0_2.Text = "5. Generate PXI0 Trigger";
            this.btnDebugGenPXI0_2.UseVisualStyleBackColor = true;
            this.btnDebugGenPXI0_2.Click += new System.EventHandler(this.btnDebugGenPXI0_2_Click);
            // 
            // btnDebugStartPlayback
            // 
            this.btnDebugStartPlayback.Location = new System.Drawing.Point(7, 106);
            this.btnDebugStartPlayback.Name = "btnDebugStartPlayback";
            this.btnDebugStartPlayback.Size = new System.Drawing.Size(87, 42);
            this.btnDebugStartPlayback.TabIndex = 38;
            this.btnDebugStartPlayback.Text = "3.Start Playback";
            this.btnDebugStartPlayback.UseVisualStyleBackColor = true;
            this.btnDebugStartPlayback.Click += new System.EventHandler(this.btnDebugStartPlayback_Click);
            // 
            // btnDebugSynchronizeModule
            // 
            this.btnDebugSynchronizeModule.Location = new System.Drawing.Point(7, 64);
            this.btnDebugSynchronizeModule.Name = "btnDebugSynchronizeModule";
            this.btnDebugSynchronizeModule.Size = new System.Drawing.Size(87, 42);
            this.btnDebugSynchronizeModule.TabIndex = 36;
            this.btnDebugSynchronizeModule.Text = "2. Synchronize Module";
            this.btnDebugSynchronizeModule.UseVisualStyleBackColor = true;
            this.btnDebugSynchronizeModule.Click += new System.EventHandler(this.btnDebugSynchronizeModule_Click);
            // 
            // btnDebugQueueWFM
            // 
            this.btnDebugQueueWFM.Location = new System.Drawing.Point(7, 21);
            this.btnDebugQueueWFM.Name = "btnDebugQueueWFM";
            this.btnDebugQueueWFM.Size = new System.Drawing.Size(87, 42);
            this.btnDebugQueueWFM.TabIndex = 35;
            this.btnDebugQueueWFM.Text = "1. Queue Waveform";
            this.btnDebugQueueWFM.UseVisualStyleBackColor = true;
            this.btnDebugQueueWFM.Click += new System.EventHandler(this.btnDebugQueueWFM_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbWaveformSelection);
            this.groupBox1.Controls.Add(this.btnLoadWaveformIntoMemory);
            this.groupBox1.Location = new System.Drawing.Point(14, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(390, 49);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waveform Selection";
            // 
            // cbWaveformSelection
            // 
            this.cbWaveformSelection.FormattingEnabled = true;
            this.cbWaveformSelection.Location = new System.Drawing.Point(10, 16);
            this.cbWaveformSelection.Margin = new System.Windows.Forms.Padding(2);
            this.cbWaveformSelection.Name = "cbWaveformSelection";
            this.cbWaveformSelection.Size = new System.Drawing.Size(158, 21);
            this.cbWaveformSelection.TabIndex = 0;
            // 
            // btnLoadWaveformIntoMemory
            // 
            this.btnLoadWaveformIntoMemory.Location = new System.Drawing.Point(172, 11);
            this.btnLoadWaveformIntoMemory.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadWaveformIntoMemory.Name = "btnLoadWaveformIntoMemory";
            this.btnLoadWaveformIntoMemory.Size = new System.Drawing.Size(201, 29);
            this.btnLoadWaveformIntoMemory.TabIndex = 45;
            this.btnLoadWaveformIntoMemory.Text = "Load Waveform into Array Again";
            this.btnLoadWaveformIntoMemory.UseVisualStyleBackColor = true;
            this.btnLoadWaveformIntoMemory.Click += new System.EventHandler(this.btnLoadWaveformIntoMemory_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.rbLog);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.rbLog2);
            this.splitContainer3.Size = new System.Drawing.Size(934, 250);
            this.splitContainer3.SplitterDistance = 575;
            this.splitContainer3.TabIndex = 7;
            // 
            // rbLog
            // 
            this.rbLog.BackColor = System.Drawing.Color.Black;
            this.rbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLog.ForeColor = System.Drawing.Color.White;
            this.rbLog.Location = new System.Drawing.Point(0, 0);
            this.rbLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbLog.Name = "rbLog";
            this.rbLog.Size = new System.Drawing.Size(575, 250);
            this.rbLog.TabIndex = 6;
            this.rbLog.Text = "";
            // 
            // rbLog2
            // 
            this.rbLog2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rbLog2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLog2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLog2.ForeColor = System.Drawing.Color.White;
            this.rbLog2.Location = new System.Drawing.Point(0, 0);
            this.rbLog2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbLog2.Name = "rbLog2";
            this.rbLog2.Size = new System.Drawing.Size(355, 250);
            this.rbLog2.TabIndex = 7;
            this.rbLog2.Text = "";
            // 
            // btnPXI0FromRef
            // 
            this.btnPXI0FromRef.Location = new System.Drawing.Point(101, 113);
            this.btnPXI0FromRef.Name = "btnPXI0FromRef";
            this.btnPXI0FromRef.Size = new System.Drawing.Size(101, 35);
            this.btnPXI0FromRef.TabIndex = 53;
            this.btnPXI0FromRef.Text = "Test";
            this.btnPXI0FromRef.UseVisualStyleBackColor = true;
            this.btnPXI0FromRef.Click += new System.EventHandler(this.btnPXI0FromRef_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 633);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.stsMainFrm);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrm";
            this.Text = "M941xA Envelop Tracking Demo App V0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stsMainFrm.ResumeLayout(false);
            this.stsMainFrm.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtVXTBBDelay)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip stsMainFrm;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid pgConnectInitStatus;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rbLog;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectInstrumentToolStripMenuItem;
        private System.Windows.Forms.Button btnLoadWaveformIntoMemory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbWaveformSelection;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDebugStop;
        private System.Windows.Forms.Button btnDebugGenPXI0_2;
        private System.Windows.Forms.Button btnDebugStartPlayback;
        private System.Windows.Forms.Button btnDebugSynchronizeModule;
        private System.Windows.Forms.Button btnDebugQueueWFM;
        private System.Windows.Forms.CheckBox chkAWGOnlyInit;
        private System.Windows.Forms.CheckBox chkReStartWaveformAfterStop;
        private System.Windows.Forms.Button btnAWGSyncFreqCheck;
        private System.Windows.Forms.Button btnStopWaveform;
        private System.Windows.Forms.Button btnStartWaveform;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox chkAWGSingle;
        private System.Windows.Forms.CheckBox chkVXTSingle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGaterOffset;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox rbLog2;
        private System.Windows.Forms.ToolStripStatusLabel tslbStatus;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.NumericUpDown txtVXTBBDelay;
        private System.Windows.Forms.Button btnSetVXTBBDelay;
        private System.Windows.Forms.CheckBox chkUseDummyWaveform;
        private System.Windows.Forms.Button btnRestartWaveform;
        private System.Windows.Forms.CheckBox chkStopStartAfterQueue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid pgETSetup;
        private System.Windows.Forms.RadioButton rb250Mhz;
        private System.Windows.Forms.RadioButton rb199_68Mhz;
        private System.Windows.Forms.RadioButton rb122_88Mhz;
        private System.Windows.Forms.RadioButton rb30_72Mhz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAWGHWVer;
        private System.Windows.Forms.Button btnApplySetup;
        private System.Windows.Forms.RadioButton rb137_60Mhz;
        private System.Windows.Forms.Button btnPXI0FromRef;
    }
}

