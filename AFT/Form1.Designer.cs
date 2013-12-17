namespace AntiForensicToolkit
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.HostsButton = new System.Windows.Forms.Button();
            this.PanicButton = new System.Windows.Forms.Button();
            this.TruecryptButton = new System.Windows.Forms.Button();
            this.DMSButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HostsPanel = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.EditHost = new System.Windows.Forms.Button();
            this.HostData = new System.Windows.Forms.ListView();
            this.HostAlive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ManualHostCheck = new System.Windows.Forms.Button();
            this.RemoveHostButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.HostName = new System.Windows.Forms.TextBox();
            this.HostPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.AddHostButton = new System.Windows.Forms.Button();
            this.HostPort = new System.Windows.Forms.TextBox();
            this.HostIp = new System.Windows.Forms.TextBox();
            this.HostsExplained = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DMSPanel = new System.Windows.Forms.Panel();
            this.RemoveFromKillList = new System.Windows.Forms.Button();
            this.ClearKillProcessList = new System.Windows.Forms.Button();
            this.GetProcessList = new System.Windows.Forms.Button();
            this.AddToKillList = new System.Windows.Forms.Button();
            this.KillProcessList = new System.Windows.Forms.ListBox();
            this.RunningProcesses = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GetUSBBaseline = new System.Windows.Forms.Button();
            this.IdentifyUSB = new System.Windows.Forms.Button();
            this.SaveDMS = new System.Windows.Forms.Button();
            this.ClearDMS = new System.Windows.Forms.Button();
            this.DMSDevice = new System.Windows.Forms.ComboBox();
            this.DMSPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.KillProcesses = new System.Windows.Forms.CheckBox();
            this.PasswordAuthentication = new System.Windows.Forms.CheckBox();
            this.Unmount = new System.Windows.Forms.CheckBox();
            this.Screensaver = new System.Windows.Forms.CheckBox();
            this.ShutPC = new System.Windows.Forms.CheckBox();
            this.USBProtect = new System.Windows.Forms.CheckBox();
            this.X = new System.Windows.Forms.TextBox();
            this.ACProtect = new System.Windows.Forms.CheckBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DMSExplained = new System.Windows.Forms.TextBox();
            this.TempBaseline = new System.Windows.Forms.ListBox();
            this.BaselineUSBDevices = new System.Windows.Forms.ListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.Home = new System.Windows.Forms.Button();
            this.HomePanel = new System.Windows.Forms.Panel();
            this.EnableDMS = new System.Windows.Forms.Button();
            this.SendDMS = new System.Windows.Forms.Button();
            this.UnmountRemoteVolues = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UnmountTrueCryptVolumes = new System.Windows.Forms.Button();
            this.AboutAFT = new System.Windows.Forms.Button();
            this.ShutdownComputer = new System.Windows.Forms.Button();
            this.PasswordCheck = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.HTTPListen = new System.Windows.Forms.CheckBox();
            this.Forward = new System.Windows.Forms.CheckBox();
            this.SettingsExplained = new System.Windows.Forms.TextBox();
            this.UseBroadcast = new System.Windows.Forms.CheckBox();
            this.Autostart = new System.Windows.Forms.CheckBox();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.ShareConfiguration = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AllowTesting = new System.Windows.Forms.CheckBox();
            this.RemoteDMS = new System.Windows.Forms.CheckBox();
            this.ReceiveConfig = new System.Windows.Forms.CheckBox();
            this.CheckHosts = new System.Windows.Forms.CheckBox();
            this.EnableLogging = new System.Windows.Forms.CheckBox();
            this.AutostartDMS = new System.Windows.Forms.CheckBox();
            this.StartMinimized = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UDPListen = new System.Windows.Forms.CheckBox();
            this.DeleteAuthKey = new System.Windows.Forms.Button();
            this.GenerateAuthKey = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.AuthKey = new System.Windows.Forms.MaskedTextBox();
            this.UDPPort = new System.Windows.Forms.TextBox();
            this.HTTPPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OpenLogFile = new System.Windows.Forms.LinkLabel();
            this.EventLabel = new System.Windows.Forms.Label();
            this.TestingButton = new System.Windows.Forms.Button();
            this.TestingPanel = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.CleanLogs = new System.Windows.Forms.Button();
            this.LogTable = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.PermformTest = new System.Windows.Forms.Button();
            this.TestData = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label15 = new System.Windows.Forms.Label();
            this.HostsPanel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.DMSPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.HomePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SettingsPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TestingPanel.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostsButton
            // 
            this.HostsButton.BackColor = System.Drawing.Color.Transparent;
            this.HostsButton.Image = ((System.Drawing.Image)(resources.GetObject("HostsButton.Image")));
            this.HostsButton.Location = new System.Drawing.Point(12, 219);
            this.HostsButton.Name = "HostsButton";
            this.HostsButton.Size = new System.Drawing.Size(89, 63);
            this.HostsButton.TabIndex = 3;
            this.HostsButton.UseVisualStyleBackColor = false;
            this.HostsButton.Click += new System.EventHandler(this.HostsButton_Click);
            this.HostsButton.MouseHover += new System.EventHandler(this.HostsButton_MouseHover);
            // 
            // PanicButton
            // 
            this.PanicButton.BackColor = System.Drawing.Color.Transparent;
            this.PanicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanicButton.Image = ((System.Drawing.Image)(resources.GetObject("PanicButton.Image")));
            this.PanicButton.Location = new System.Drawing.Point(12, 150);
            this.PanicButton.Name = "PanicButton";
            this.PanicButton.Size = new System.Drawing.Size(89, 63);
            this.PanicButton.TabIndex = 2;
            this.PanicButton.UseVisualStyleBackColor = false;
            this.PanicButton.Click += new System.EventHandler(this.PanicButton_Click);
            this.PanicButton.MouseHover += new System.EventHandler(this.PanicButton_MouseHover);
            // 
            // TruecryptButton
            // 
            this.TruecryptButton.BackColor = System.Drawing.Color.Transparent;
            this.TruecryptButton.Image = ((System.Drawing.Image)(resources.GetObject("TruecryptButton.Image")));
            this.TruecryptButton.Location = new System.Drawing.Point(12, 81);
            this.TruecryptButton.Name = "TruecryptButton";
            this.TruecryptButton.Size = new System.Drawing.Size(89, 63);
            this.TruecryptButton.TabIndex = 1;
            this.TruecryptButton.UseVisualStyleBackColor = false;
            this.TruecryptButton.Click += new System.EventHandler(this.TruecryptButton_Click);
            this.TruecryptButton.MouseHover += new System.EventHandler(this.TruecryptButton_MouseHover);
            // 
            // DMSButton
            // 
            this.DMSButton.BackColor = System.Drawing.Color.Transparent;
            this.DMSButton.Image = ((System.Drawing.Image)(resources.GetObject("DMSButton.Image")));
            this.DMSButton.Location = new System.Drawing.Point(12, 288);
            this.DMSButton.Name = "DMSButton";
            this.DMSButton.Size = new System.Drawing.Size(89, 63);
            this.DMSButton.TabIndex = 4;
            this.DMSButton.UseVisualStyleBackColor = false;
            this.DMSButton.Click += new System.EventHandler(this.DMSButton_Click);
            this.DMSButton.MouseHover += new System.EventHandler(this.DMSButton_MouseHover);
            // 
            // SettingsButton
            // 
            this.SettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.SettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsButton.Image")));
            this.SettingsButton.Location = new System.Drawing.Point(12, 358);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(89, 63);
            this.SettingsButton.TabIndex = 5;
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            this.SettingsButton.MouseHover += new System.EventHandler(this.SettingsButton_MouseHover);
            // 
            // HostsPanel
            // 
            this.HostsPanel.BackColor = System.Drawing.Color.Transparent;
            this.HostsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HostsPanel.Controls.Add(this.groupBox6);
            this.HostsPanel.Controls.Add(this.groupBox5);
            this.HostsPanel.Controls.Add(this.HostsExplained);
            this.HostsPanel.Controls.Add(this.label2);
            this.HostsPanel.Location = new System.Drawing.Point(112, 15);
            this.HostsPanel.Name = "HostsPanel";
            this.HostsPanel.Size = new System.Drawing.Size(610, 477);
            this.HostsPanel.TabIndex = 8;
            this.HostsPanel.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.EditHost);
            this.groupBox6.Controls.Add(this.HostData);
            this.groupBox6.Controls.Add(this.ManualHostCheck);
            this.groupBox6.Controls.Add(this.RemoveHostButton);
            this.groupBox6.Location = new System.Drawing.Point(14, 203);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(577, 259);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Existing hosts";
            // 
            // EditHost
            // 
            this.EditHost.Location = new System.Drawing.Point(9, 229);
            this.EditHost.Name = "EditHost";
            this.EditHost.Size = new System.Drawing.Size(173, 23);
            this.EditHost.TabIndex = 26;
            this.EditHost.Text = "Edit host";
            this.EditHost.UseVisualStyleBackColor = true;
            this.EditHost.Click += new System.EventHandler(this.EditHost_Click);
            // 
            // HostData
            // 
            this.HostData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HostAlive,
            this.RHostName,
            this.IP,
            this.Port,
            this.Key});
            this.HostData.FullRowSelect = true;
            this.HostData.GridLines = true;
            this.HostData.Location = new System.Drawing.Point(9, 19);
            this.HostData.MultiSelect = false;
            this.HostData.Name = "HostData";
            this.HostData.Size = new System.Drawing.Size(550, 198);
            this.HostData.TabIndex = 21;
            this.HostData.UseCompatibleStateImageBehavior = false;
            this.HostData.View = System.Windows.Forms.View.Details;
            // 
            // HostAlive
            // 
            this.HostAlive.Text = "Alive";
            this.HostAlive.Width = 40;
            // 
            // RHostName
            // 
            this.RHostName.Text = "Name";
            this.RHostName.Width = 91;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 87;
            // 
            // Port
            // 
            this.Port.Text = "Port";
            this.Port.Width = 47;
            // 
            // Key
            // 
            this.Key.Text = "Key";
            this.Key.Width = 278;
            // 
            // ManualHostCheck
            // 
            this.ManualHostCheck.Location = new System.Drawing.Point(386, 229);
            this.ManualHostCheck.Name = "ManualHostCheck";
            this.ManualHostCheck.Size = new System.Drawing.Size(173, 23);
            this.ManualHostCheck.TabIndex = 28;
            this.ManualHostCheck.Text = "Check hosts";
            this.ManualHostCheck.UseVisualStyleBackColor = true;
            this.ManualHostCheck.Click += new System.EventHandler(this.ManualHostCheck_Click);
            // 
            // RemoveHostButton
            // 
            this.RemoveHostButton.Location = new System.Drawing.Point(199, 229);
            this.RemoveHostButton.Name = "RemoveHostButton";
            this.RemoveHostButton.Size = new System.Drawing.Size(173, 23);
            this.RemoveHostButton.TabIndex = 27;
            this.RemoveHostButton.Text = "Remove host";
            this.RemoveHostButton.UseVisualStyleBackColor = true;
            this.RemoveHostButton.Click += new System.EventHandler(this.RemoveHostButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.HostName);
            this.groupBox5.Controls.Add(this.HostPassword);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.AddHostButton);
            this.groupBox5.Controls.Add(this.HostPort);
            this.groupBox5.Controls.Add(this.HostIp);
            this.groupBox5.Location = new System.Drawing.Point(14, 53);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(312, 144);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Add host";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Host name";
            // 
            // HostName
            // 
            this.HostName.Location = new System.Drawing.Point(9, 39);
            this.HostName.Name = "HostName";
            this.HostName.Size = new System.Drawing.Size(135, 20);
            this.HostName.TabIndex = 21;
            // 
            // HostPassword
            // 
            this.HostPassword.Location = new System.Drawing.Point(9, 86);
            this.HostPassword.Name = "HostPassword";
            this.HostPassword.Size = new System.Drawing.Size(292, 20);
            this.HostPassword.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Auth key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(246, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Port";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(151, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Remote host IP";
            // 
            // AddHostButton
            // 
            this.AddHostButton.Location = new System.Drawing.Point(9, 112);
            this.AddHostButton.Name = "AddHostButton";
            this.AddHostButton.Size = new System.Drawing.Size(292, 23);
            this.AddHostButton.TabIndex = 25;
            this.AddHostButton.Text = "Add host";
            this.AddHostButton.UseVisualStyleBackColor = true;
            this.AddHostButton.Click += new System.EventHandler(this.AddHostButon_Click);
            // 
            // HostPort
            // 
            this.HostPort.Location = new System.Drawing.Point(246, 39);
            this.HostPort.Name = "HostPort";
            this.HostPort.Size = new System.Drawing.Size(55, 20);
            this.HostPort.TabIndex = 23;
            // 
            // HostIp
            // 
            this.HostIp.Location = new System.Drawing.Point(150, 39);
            this.HostIp.Name = "HostIp";
            this.HostIp.Size = new System.Drawing.Size(90, 20);
            this.HostIp.TabIndex = 22;
            // 
            // HostsExplained
            // 
            this.HostsExplained.AcceptsReturn = true;
            this.HostsExplained.BackColor = System.Drawing.Color.White;
            this.HostsExplained.Location = new System.Drawing.Point(332, 16);
            this.HostsExplained.Multiline = true;
            this.HostsExplained.Name = "HostsExplained";
            this.HostsExplained.ReadOnly = true;
            this.HostsExplained.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.HostsExplained.Size = new System.Drawing.Size(259, 181);
            this.HostsExplained.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Manage hosts";
            // 
            // DMSPanel
            // 
            this.DMSPanel.BackColor = System.Drawing.Color.Transparent;
            this.DMSPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DMSPanel.Controls.Add(this.RemoveFromKillList);
            this.DMSPanel.Controls.Add(this.ClearKillProcessList);
            this.DMSPanel.Controls.Add(this.GetProcessList);
            this.DMSPanel.Controls.Add(this.AddToKillList);
            this.DMSPanel.Controls.Add(this.KillProcessList);
            this.DMSPanel.Controls.Add(this.RunningProcesses);
            this.DMSPanel.Controls.Add(this.groupBox2);
            this.DMSPanel.Controls.Add(this.groupBox1);
            this.DMSPanel.Controls.Add(this.DMSExplained);
            this.DMSPanel.Controls.Add(this.TempBaseline);
            this.DMSPanel.Controls.Add(this.BaselineUSBDevices);
            this.DMSPanel.Controls.Add(this.linkLabel1);
            this.DMSPanel.Controls.Add(this.label5);
            this.DMSPanel.Location = new System.Drawing.Point(112, 15);
            this.DMSPanel.Name = "DMSPanel";
            this.DMSPanel.Size = new System.Drawing.Size(610, 477);
            this.DMSPanel.TabIndex = 10;
            this.DMSPanel.Visible = false;
            // 
            // RemoveFromKillList
            // 
            this.RemoveFromKillList.Location = new System.Drawing.Point(410, 331);
            this.RemoveFromKillList.Name = "RemoveFromKillList";
            this.RemoveFromKillList.Size = new System.Drawing.Size(23, 23);
            this.RemoveFromKillList.TabIndex = 43;
            this.RemoveFromKillList.Text = "X";
            this.RemoveFromKillList.UseVisualStyleBackColor = true;
            this.RemoveFromKillList.Click += new System.EventHandler(this.RemoveFromKillList_Click);
            // 
            // ClearKillProcessList
            // 
            this.ClearKillProcessList.Location = new System.Drawing.Point(439, 209);
            this.ClearKillProcessList.Name = "ClearKillProcessList";
            this.ClearKillProcessList.Size = new System.Drawing.Size(150, 22);
            this.ClearKillProcessList.TabIndex = 42;
            this.ClearKillProcessList.Text = "Clear kill list";
            this.ClearKillProcessList.UseVisualStyleBackColor = true;
            this.ClearKillProcessList.Click += new System.EventHandler(this.ClearKillProcessList_Click);
            // 
            // GetProcessList
            // 
            this.GetProcessList.Location = new System.Drawing.Point(263, 209);
            this.GetProcessList.Name = "GetProcessList";
            this.GetProcessList.Size = new System.Drawing.Size(141, 23);
            this.GetProcessList.TabIndex = 41;
            this.GetProcessList.Text = "Refresh process list";
            this.GetProcessList.UseVisualStyleBackColor = true;
            this.GetProcessList.Click += new System.EventHandler(this.GetProcessList_Click);
            // 
            // AddToKillList
            // 
            this.AddToKillList.Location = new System.Drawing.Point(410, 300);
            this.AddToKillList.Name = "AddToKillList";
            this.AddToKillList.Size = new System.Drawing.Size(23, 23);
            this.AddToKillList.TabIndex = 40;
            this.AddToKillList.Text = ">";
            this.AddToKillList.UseVisualStyleBackColor = true;
            this.AddToKillList.Click += new System.EventHandler(this.AddToKillList_Click);
            // 
            // KillProcessList
            // 
            this.KillProcessList.FormattingEnabled = true;
            this.KillProcessList.Location = new System.Drawing.Point(439, 240);
            this.KillProcessList.Name = "KillProcessList";
            this.KillProcessList.Size = new System.Drawing.Size(151, 199);
            this.KillProcessList.TabIndex = 39;
            // 
            // RunningProcesses
            // 
            this.RunningProcesses.FormattingEnabled = true;
            this.RunningProcesses.Location = new System.Drawing.Point(263, 238);
            this.RunningProcesses.Name = "RunningProcesses";
            this.RunningProcesses.Size = new System.Drawing.Size(141, 199);
            this.RunningProcesses.TabIndex = 38;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.GetUSBBaseline);
            this.groupBox2.Controls.Add(this.IdentifyUSB);
            this.groupBox2.Controls.Add(this.SaveDMS);
            this.groupBox2.Controls.Add(this.ClearDMS);
            this.groupBox2.Controls.Add(this.DMSDevice);
            this.groupBox2.Controls.Add(this.DMSPassword);
            this.groupBox2.Location = new System.Drawing.Point(14, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 179);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DMS Device";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Device";
            // 
            // GetUSBBaseline
            // 
            this.GetUSBBaseline.Location = new System.Drawing.Point(5, 19);
            this.GetUSBBaseline.Name = "GetUSBBaseline";
            this.GetUSBBaseline.Size = new System.Drawing.Size(219, 23);
            this.GetUSBBaseline.TabIndex = 11;
            this.GetUSBBaseline.Text = "Get baseline";
            this.GetUSBBaseline.UseVisualStyleBackColor = true;
            this.GetUSBBaseline.Click += new System.EventHandler(this.GetUSBBaseline_Click);
            // 
            // IdentifyUSB
            // 
            this.IdentifyUSB.Location = new System.Drawing.Point(6, 52);
            this.IdentifyUSB.Name = "IdentifyUSB";
            this.IdentifyUSB.Size = new System.Drawing.Size(217, 23);
            this.IdentifyUSB.TabIndex = 11;
            this.IdentifyUSB.Text = "Identify USB";
            this.IdentifyUSB.UseVisualStyleBackColor = true;
            this.IdentifyUSB.Click += new System.EventHandler(this.IdentifyUSB_Click);
            // 
            // SaveDMS
            // 
            this.SaveDMS.Location = new System.Drawing.Point(9, 141);
            this.SaveDMS.Name = "SaveDMS";
            this.SaveDMS.Size = new System.Drawing.Size(101, 23);
            this.SaveDMS.TabIndex = 11;
            this.SaveDMS.Text = "Save switch";
            this.SaveDMS.UseVisualStyleBackColor = true;
            this.SaveDMS.Click += new System.EventHandler(this.SaveDMS_Click);
            // 
            // ClearDMS
            // 
            this.ClearDMS.Location = new System.Drawing.Point(127, 141);
            this.ClearDMS.Name = "ClearDMS";
            this.ClearDMS.Size = new System.Drawing.Size(96, 23);
            this.ClearDMS.TabIndex = 16;
            this.ClearDMS.Text = "Clear";
            this.ClearDMS.UseVisualStyleBackColor = true;
            this.ClearDMS.Click += new System.EventHandler(this.ClearDMS_Click);
            // 
            // DMSDevice
            // 
            this.DMSDevice.FormattingEnabled = true;
            this.DMSDevice.Location = new System.Drawing.Point(70, 88);
            this.DMSDevice.Name = "DMSDevice";
            this.DMSDevice.Size = new System.Drawing.Size(154, 21);
            this.DMSDevice.TabIndex = 12;
            // 
            // DMSPassword
            // 
            this.DMSPassword.Location = new System.Drawing.Point(70, 115);
            this.DMSPassword.Name = "DMSPassword";
            this.DMSPassword.Size = new System.Drawing.Size(154, 20);
            this.DMSPassword.TabIndex = 13;
            this.DMSPassword.UseSystemPasswordChar = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.KillProcesses);
            this.groupBox1.Controls.Add(this.PasswordAuthentication);
            this.groupBox1.Controls.Add(this.Unmount);
            this.groupBox1.Controls.Add(this.Screensaver);
            this.groupBox1.Controls.Add(this.ShutPC);
            this.groupBox1.Controls.Add(this.USBProtect);
            this.groupBox1.Controls.Add(this.X);
            this.groupBox1.Controls.Add(this.ACProtect);
            this.groupBox1.Controls.Add(this.Y);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(14, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 191);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security configuration";
            // 
            // KillProcesses
            // 
            this.KillProcesses.AutoSize = true;
            this.KillProcesses.Location = new System.Drawing.Point(6, 159);
            this.KillProcesses.Name = "KillProcesses";
            this.KillProcesses.Size = new System.Drawing.Size(90, 17);
            this.KillProcesses.TabIndex = 37;
            this.KillProcesses.Text = "Kill processes";
            this.KillProcesses.UseVisualStyleBackColor = true;
            this.KillProcesses.CheckedChanged += new System.EventHandler(this.KillProcesses_CheckedChanged);
            // 
            // PasswordAuthentication
            // 
            this.PasswordAuthentication.AutoSize = true;
            this.PasswordAuthentication.Location = new System.Drawing.Point(6, 137);
            this.PasswordAuthentication.Name = "PasswordAuthentication";
            this.PasswordAuthentication.Size = new System.Drawing.Size(93, 17);
            this.PasswordAuthentication.TabIndex = 36;
            this.PasswordAuthentication.Text = "Use password";
            this.PasswordAuthentication.UseVisualStyleBackColor = true;
            this.PasswordAuthentication.CheckedChanged += new System.EventHandler(this.PasswordAuthentication_CheckedChanged);
            // 
            // Unmount
            // 
            this.Unmount.AutoSize = true;
            this.Unmount.Checked = true;
            this.Unmount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Unmount.Location = new System.Drawing.Point(6, 23);
            this.Unmount.Name = "Unmount";
            this.Unmount.Size = new System.Drawing.Size(69, 17);
            this.Unmount.TabIndex = 24;
            this.Unmount.Text = "Unmount";
            this.Unmount.UseVisualStyleBackColor = true;
            this.Unmount.CheckedChanged += new System.EventHandler(this.Unmount_CheckedChanged);
            // 
            // Screensaver
            // 
            this.Screensaver.AutoSize = true;
            this.Screensaver.Location = new System.Drawing.Point(6, 115);
            this.Screensaver.Name = "Screensaver";
            this.Screensaver.Size = new System.Drawing.Size(106, 17);
            this.Screensaver.TabIndex = 35;
            this.Screensaver.Text = "Use screensaver";
            this.Screensaver.UseVisualStyleBackColor = true;
            // 
            // ShutPC
            // 
            this.ShutPC.AutoSize = true;
            this.ShutPC.Location = new System.Drawing.Point(6, 46);
            this.ShutPC.Name = "ShutPC";
            this.ShutPC.Size = new System.Drawing.Size(74, 17);
            this.ShutPC.TabIndex = 23;
            this.ShutPC.Text = "Shutdown";
            this.ShutPC.UseVisualStyleBackColor = true;
            // 
            // USBProtect
            // 
            this.USBProtect.AutoSize = true;
            this.USBProtect.Location = new System.Drawing.Point(6, 92);
            this.USBProtect.Name = "USBProtect";
            this.USBProtect.Size = new System.Drawing.Size(99, 17);
            this.USBProtect.TabIndex = 34;
            this.USBProtect.Text = "USB Protection";
            this.USBProtect.UseVisualStyleBackColor = true;
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(145, 23);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(79, 20);
            this.X.TabIndex = 25;
            this.X.Text = "10";
            // 
            // ACProtect
            // 
            this.ACProtect.AutoSize = true;
            this.ACProtect.Location = new System.Drawing.Point(6, 69);
            this.ACProtect.Name = "ACProtect";
            this.ACProtect.Size = new System.Drawing.Size(91, 17);
            this.ACProtect.TabIndex = 33;
            this.ACProtect.Text = "AC Protection";
            this.ACProtect.UseVisualStyleBackColor = true;
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(145, 49);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(79, 20);
            this.Y.TabIndex = 26;
            this.Y.Text = "10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Y";
            // 
            // DMSExplained
            // 
            this.DMSExplained.AcceptsReturn = true;
            this.DMSExplained.BackColor = System.Drawing.Color.White;
            this.DMSExplained.Location = new System.Drawing.Point(263, 16);
            this.DMSExplained.Multiline = true;
            this.DMSExplained.Name = "DMSExplained";
            this.DMSExplained.ReadOnly = true;
            this.DMSExplained.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DMSExplained.Size = new System.Drawing.Size(328, 181);
            this.DMSExplained.TabIndex = 17;
            // 
            // TempBaseline
            // 
            this.TempBaseline.FormattingEnabled = true;
            this.TempBaseline.Location = new System.Drawing.Point(146, 449);
            this.TempBaseline.Name = "TempBaseline";
            this.TempBaseline.Size = new System.Drawing.Size(44, 17);
            this.TempBaseline.TabIndex = 15;
            this.TempBaseline.Visible = false;
            // 
            // BaselineUSBDevices
            // 
            this.BaselineUSBDevices.FormattingEnabled = true;
            this.BaselineUSBDevices.Location = new System.Drawing.Point(96, 450);
            this.BaselineUSBDevices.Name = "BaselineUSBDevices";
            this.BaselineUSBDevices.Size = new System.Drawing.Size(44, 17);
            this.BaselineUSBDevices.TabIndex = 15;
            this.BaselineUSBDevices.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(11, 450);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "What is a DMS?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "DMS";
            // 
            // Home
            // 
            this.Home.BackColor = System.Drawing.Color.Transparent;
            this.Home.Image = ((System.Drawing.Image)(resources.GetObject("Home.Image")));
            this.Home.Location = new System.Drawing.Point(12, 15);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(89, 60);
            this.Home.TabIndex = 16;
            this.Home.UseVisualStyleBackColor = false;
            this.Home.Click += new System.EventHandler(this.Home_Click_1);
            this.Home.MouseHover += new System.EventHandler(this.Home_MouseHover);
            // 
            // HomePanel
            // 
            this.HomePanel.BackColor = System.Drawing.Color.Transparent;
            this.HomePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HomePanel.Controls.Add(this.EnableDMS);
            this.HomePanel.Controls.Add(this.SendDMS);
            this.HomePanel.Controls.Add(this.UnmountRemoteVolues);
            this.HomePanel.Controls.Add(this.pictureBox1);
            this.HomePanel.Controls.Add(this.UnmountTrueCryptVolumes);
            this.HomePanel.Controls.Add(this.AboutAFT);
            this.HomePanel.Controls.Add(this.ShutdownComputer);
            this.HomePanel.Controls.Add(this.PasswordCheck);
            this.HomePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomePanel.Location = new System.Drawing.Point(112, 15);
            this.HomePanel.Name = "HomePanel";
            this.HomePanel.Size = new System.Drawing.Size(610, 477);
            this.HomePanel.TabIndex = 17;
            // 
            // EnableDMS
            // 
            this.EnableDMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableDMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EnableDMS.Location = new System.Drawing.Point(19, 108);
            this.EnableDMS.Name = "EnableDMS";
            this.EnableDMS.Size = new System.Drawing.Size(572, 60);
            this.EnableDMS.TabIndex = 0;
            this.EnableDMS.Text = "Enable DMS";
            this.EnableDMS.UseVisualStyleBackColor = true;
            this.EnableDMS.Click += new System.EventHandler(this.EnableDMS_Click);
            // 
            // SendDMS
            // 
            this.SendDMS.Location = new System.Drawing.Point(19, 171);
            this.SendDMS.Name = "SendDMS";
            this.SendDMS.Size = new System.Drawing.Size(572, 60);
            this.SendDMS.TabIndex = 8;
            this.SendDMS.Text = "Remote DMS";
            this.SendDMS.UseVisualStyleBackColor = true;
            this.SendDMS.Click += new System.EventHandler(this.SendDMS_Click);
            // 
            // UnmountRemoteVolues
            // 
            this.UnmountRemoteVolues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UnmountRemoteVolues.Location = new System.Drawing.Point(20, 298);
            this.UnmountRemoteVolues.Name = "UnmountRemoteVolues";
            this.UnmountRemoteVolues.Size = new System.Drawing.Size(572, 60);
            this.UnmountRemoteVolues.TabIndex = 6;
            this.UnmountRemoteVolues.Text = "Unmount remote volumes";
            this.UnmountRemoteVolues.UseVisualStyleBackColor = true;
            this.UnmountRemoteVolues.Click += new System.EventHandler(this.UnmountRemoteVolumes_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(231, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 81);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // UnmountTrueCryptVolumes
            // 
            this.UnmountTrueCryptVolumes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UnmountTrueCryptVolumes.Location = new System.Drawing.Point(19, 234);
            this.UnmountTrueCryptVolumes.Name = "UnmountTrueCryptVolumes";
            this.UnmountTrueCryptVolumes.Size = new System.Drawing.Size(572, 60);
            this.UnmountTrueCryptVolumes.TabIndex = 4;
            this.UnmountTrueCryptVolumes.Text = "Unmount TrueCrypt volumes";
            this.UnmountTrueCryptVolumes.UseVisualStyleBackColor = true;
            this.UnmountTrueCryptVolumes.Click += new System.EventHandler(this.UnmountTrueCryptVolumes_Click);
            // 
            // AboutAFT
            // 
            this.AboutAFT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AboutAFT.Location = new System.Drawing.Point(495, 432);
            this.AboutAFT.Name = "AboutAFT";
            this.AboutAFT.Size = new System.Drawing.Size(96, 30);
            this.AboutAFT.TabIndex = 3;
            this.AboutAFT.Text = "About AFT";
            this.AboutAFT.UseVisualStyleBackColor = true;
            this.AboutAFT.Click += new System.EventHandler(this.AboutAFT_Click);
            // 
            // ShutdownComputer
            // 
            this.ShutdownComputer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ShutdownComputer.Location = new System.Drawing.Point(19, 362);
            this.ShutdownComputer.Name = "ShutdownComputer";
            this.ShutdownComputer.Size = new System.Drawing.Size(572, 60);
            this.ShutdownComputer.TabIndex = 5;
            this.ShutdownComputer.Text = "Shutdown computer";
            this.ShutdownComputer.UseVisualStyleBackColor = true;
            this.ShutdownComputer.Click += new System.EventHandler(this.ShutdownComputer_Click);
            // 
            // PasswordCheck
            // 
            this.PasswordCheck.Location = new System.Drawing.Point(3, 443);
            this.PasswordCheck.Name = "PasswordCheck";
            this.PasswordCheck.Size = new System.Drawing.Size(100, 20);
            this.PasswordCheck.TabIndex = 1;
            this.PasswordCheck.Visible = false;
            this.PasswordCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordCheck_KeyDown);
            this.PasswordCheck.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PasswordCheck_KeyUp);
            this.PasswordCheck.Leave += new System.EventHandler(this.PasswordCheck_Leave);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Anti-Forensic Toolkit";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Settings";
            // 
            // HTTPListen
            // 
            this.HTTPListen.AutoSize = true;
            this.HTTPListen.Checked = true;
            this.HTTPListen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HTTPListen.Location = new System.Drawing.Point(6, 23);
            this.HTTPListen.Name = "HTTPListen";
            this.HTTPListen.Size = new System.Drawing.Size(95, 17);
            this.HTTPListen.TabIndex = 9;
            this.HTTPListen.Text = "HTTP Listener";
            this.HTTPListen.UseVisualStyleBackColor = true;
            this.HTTPListen.CheckedChanged += new System.EventHandler(this.Listener_CheckedChanged);
            // 
            // Forward
            // 
            this.Forward.AutoSize = true;
            this.Forward.Location = new System.Drawing.Point(6, 66);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(93, 17);
            this.Forward.TabIndex = 9;
            this.Forward.Text = "Panic to hosts";
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.CheckedChanged += new System.EventHandler(this.Forward_CheckedChanged);
            // 
            // SettingsExplained
            // 
            this.SettingsExplained.AcceptsReturn = true;
            this.SettingsExplained.BackColor = System.Drawing.Color.White;
            this.SettingsExplained.Location = new System.Drawing.Point(263, 16);
            this.SettingsExplained.Multiline = true;
            this.SettingsExplained.Name = "SettingsExplained";
            this.SettingsExplained.ReadOnly = true;
            this.SettingsExplained.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SettingsExplained.Size = new System.Drawing.Size(328, 446);
            this.SettingsExplained.TabIndex = 18;
            // 
            // UseBroadcast
            // 
            this.UseBroadcast.AutoSize = true;
            this.UseBroadcast.Location = new System.Drawing.Point(6, 89);
            this.UseBroadcast.Name = "UseBroadcast";
            this.UseBroadcast.Size = new System.Drawing.Size(103, 17);
            this.UseBroadcast.TabIndex = 19;
            this.UseBroadcast.Text = "Broadcast panic";
            this.UseBroadcast.UseVisualStyleBackColor = true;
            this.UseBroadcast.CheckedChanged += new System.EventHandler(this.UseBroadcast_CheckedChanged);
            // 
            // Autostart
            // 
            this.Autostart.AutoSize = true;
            this.Autostart.Location = new System.Drawing.Point(6, 19);
            this.Autostart.Name = "Autostart";
            this.Autostart.Size = new System.Drawing.Size(91, 17);
            this.Autostart.TabIndex = 23;
            this.Autostart.Text = "Autostart AFT";
            this.Autostart.UseVisualStyleBackColor = true;
            this.Autostart.CheckedChanged += new System.EventHandler(this.Autostart_CheckedChanged);
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.BackColor = System.Drawing.Color.Transparent;
            this.SettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsPanel.Controls.Add(this.ShareConfiguration);
            this.SettingsPanel.Controls.Add(this.groupBox4);
            this.SettingsPanel.Controls.Add(this.groupBox3);
            this.SettingsPanel.Controls.Add(this.SettingsExplained);
            this.SettingsPanel.Controls.Add(this.label4);
            this.SettingsPanel.Location = new System.Drawing.Point(112, 15);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(610, 477);
            this.SettingsPanel.TabIndex = 9;
            this.SettingsPanel.Visible = false;
            // 
            // ShareConfiguration
            // 
            this.ShareConfiguration.Location = new System.Drawing.Point(159, 439);
            this.ShareConfiguration.Name = "ShareConfiguration";
            this.ShareConfiguration.Size = new System.Drawing.Size(96, 23);
            this.ShareConfiguration.TabIndex = 26;
            this.ShareConfiguration.Text = "Share config";
            this.ShareConfiguration.UseVisualStyleBackColor = true;
            this.ShareConfiguration.Click += new System.EventHandler(this.ShareConfiguration_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AllowTesting);
            this.groupBox4.Controls.Add(this.RemoteDMS);
            this.groupBox4.Controls.Add(this.ReceiveConfig);
            this.groupBox4.Controls.Add(this.CheckHosts);
            this.groupBox4.Controls.Add(this.EnableLogging);
            this.groupBox4.Controls.Add(this.AutostartDMS);
            this.groupBox4.Controls.Add(this.StartMinimized);
            this.groupBox4.Controls.Add(this.Autostart);
            this.groupBox4.Location = new System.Drawing.Point(14, 249);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 184);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "AFT settings";
            // 
            // AllowTesting
            // 
            this.AllowTesting.AutoSize = true;
            this.AllowTesting.Enabled = false;
            this.AllowTesting.Location = new System.Drawing.Point(128, 19);
            this.AllowTesting.Name = "AllowTesting";
            this.AllowTesting.Size = new System.Drawing.Size(85, 17);
            this.AllowTesting.TabIndex = 28;
            this.AllowTesting.Text = "Allow testing";
            this.AllowTesting.UseVisualStyleBackColor = true;
            this.AllowTesting.CheckedChanged += new System.EventHandler(this.AllowTesting_CheckedChanged);
            // 
            // RemoteDMS
            // 
            this.RemoteDMS.AutoSize = true;
            this.RemoteDMS.Enabled = false;
            this.RemoteDMS.Location = new System.Drawing.Point(5, 88);
            this.RemoteDMS.Name = "RemoteDMS";
            this.RemoteDMS.Size = new System.Drawing.Size(90, 17);
            this.RemoteDMS.TabIndex = 27;
            this.RemoteDMS.Text = "Remote DMS";
            this.RemoteDMS.UseVisualStyleBackColor = true;
            this.RemoteDMS.CheckedChanged += new System.EventHandler(this.RemoteDMS_CheckedChanged);
            // 
            // ReceiveConfig
            // 
            this.ReceiveConfig.AutoSize = true;
            this.ReceiveConfig.Enabled = false;
            this.ReceiveConfig.Location = new System.Drawing.Point(6, 156);
            this.ReceiveConfig.Name = "ReceiveConfig";
            this.ReceiveConfig.Size = new System.Drawing.Size(97, 17);
            this.ReceiveConfig.TabIndex = 27;
            this.ReceiveConfig.Text = "Config updates";
            this.ReceiveConfig.UseVisualStyleBackColor = true;
            this.ReceiveConfig.CheckedChanged += new System.EventHandler(this.ReceiveConfig_CheckedChanged);
            // 
            // CheckHosts
            // 
            this.CheckHosts.AutoSize = true;
            this.CheckHosts.Enabled = false;
            this.CheckHosts.Location = new System.Drawing.Point(6, 134);
            this.CheckHosts.Name = "CheckHosts";
            this.CheckHosts.Size = new System.Drawing.Size(85, 17);
            this.CheckHosts.TabIndex = 27;
            this.CheckHosts.Text = "Check hosts";
            this.CheckHosts.UseVisualStyleBackColor = true;
            this.CheckHosts.CheckedChanged += new System.EventHandler(this.CheckHosts_CheckedChanged);
            // 
            // EnableLogging
            // 
            this.EnableLogging.AutoSize = true;
            this.EnableLogging.Location = new System.Drawing.Point(6, 111);
            this.EnableLogging.Name = "EnableLogging";
            this.EnableLogging.Size = new System.Drawing.Size(79, 17);
            this.EnableLogging.TabIndex = 26;
            this.EnableLogging.Text = "Log events";
            this.EnableLogging.UseVisualStyleBackColor = true;
            this.EnableLogging.CheckedChanged += new System.EventHandler(this.EnableLogging_CheckedChanged);
            // 
            // AutostartDMS
            // 
            this.AutostartDMS.AutoSize = true;
            this.AutostartDMS.Location = new System.Drawing.Point(5, 65);
            this.AutostartDMS.Name = "AutostartDMS";
            this.AutostartDMS.Size = new System.Drawing.Size(95, 17);
            this.AutostartDMS.TabIndex = 25;
            this.AutostartDMS.Text = "Autostart DMS";
            this.AutostartDMS.UseVisualStyleBackColor = true;
            this.AutostartDMS.CheckedChanged += new System.EventHandler(this.AutostartDMS_CheckedChanged);
            // 
            // StartMinimized
            // 
            this.StartMinimized.AutoSize = true;
            this.StartMinimized.Location = new System.Drawing.Point(5, 42);
            this.StartMinimized.Name = "StartMinimized";
            this.StartMinimized.Size = new System.Drawing.Size(96, 17);
            this.StartMinimized.TabIndex = 24;
            this.StartMinimized.Text = "Start minimized";
            this.StartMinimized.UseVisualStyleBackColor = true;
            this.StartMinimized.CheckedChanged += new System.EventHandler(this.StartMinimized_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UDPListen);
            this.groupBox3.Controls.Add(this.DeleteAuthKey);
            this.groupBox3.Controls.Add(this.GenerateAuthKey);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.AuthKey);
            this.groupBox3.Controls.Add(this.UDPPort);
            this.groupBox3.Controls.Add(this.HTTPListen);
            this.groupBox3.Controls.Add(this.HTTPPort);
            this.groupBox3.Controls.Add(this.Forward);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.UseBroadcast);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(14, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 187);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Panic settings";
            // 
            // UDPListen
            // 
            this.UDPListen.AutoSize = true;
            this.UDPListen.Location = new System.Drawing.Point(6, 45);
            this.UDPListen.Name = "UDPListen";
            this.UDPListen.Size = new System.Drawing.Size(89, 17);
            this.UDPListen.TabIndex = 27;
            this.UDPListen.Text = "UDP Listener";
            this.UDPListen.UseVisualStyleBackColor = true;
            this.UDPListen.CheckedChanged += new System.EventHandler(this.UDPListen_CheckedChanged);
            // 
            // DeleteAuthKey
            // 
            this.DeleteAuthKey.Location = new System.Drawing.Point(127, 156);
            this.DeleteAuthKey.Name = "DeleteAuthKey";
            this.DeleteAuthKey.Size = new System.Drawing.Size(96, 23);
            this.DeleteAuthKey.TabIndex = 41;
            this.DeleteAuthKey.Text = "Delete key";
            this.DeleteAuthKey.UseVisualStyleBackColor = true;
            this.DeleteAuthKey.Click += new System.EventHandler(this.DeleteAuthKey_Click);
            // 
            // GenerateAuthKey
            // 
            this.GenerateAuthKey.Location = new System.Drawing.Point(11, 156);
            this.GenerateAuthKey.Name = "GenerateAuthKey";
            this.GenerateAuthKey.Size = new System.Drawing.Size(96, 23);
            this.GenerateAuthKey.TabIndex = 40;
            this.GenerateAuthKey.Text = "Hash key";
            this.GenerateAuthKey.UseVisualStyleBackColor = true;
            this.GenerateAuthKey.Click += new System.EventHandler(this.GenerateAuthKey_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 133);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Auth key";
            // 
            // AuthKey
            // 
            this.AuthKey.Location = new System.Drawing.Point(58, 130);
            this.AuthKey.Name = "AuthKey";
            this.AuthKey.Size = new System.Drawing.Size(166, 20);
            this.AuthKey.TabIndex = 38;
            // 
            // UDPPort
            // 
            this.UDPPort.Enabled = false;
            this.UDPPort.Location = new System.Drawing.Point(163, 48);
            this.UDPPort.Name = "UDPPort";
            this.UDPPort.Size = new System.Drawing.Size(61, 20);
            this.UDPPort.TabIndex = 36;
            this.UDPPort.Text = "1337";
            // 
            // HTTPPort
            // 
            this.HTTPPort.Location = new System.Drawing.Point(163, 22);
            this.HTTPPort.Name = "HTTPPort";
            this.HTTPPort.Size = new System.Drawing.Size(61, 20);
            this.HTTPPort.TabIndex = 35;
            this.HTTPPort.Text = "8080";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "UDP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "HTTP";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.OpenLogFile);
            this.panel1.Controls.Add(this.EventLabel);
            this.panel1.Location = new System.Drawing.Point(12, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 23);
            this.panel1.TabIndex = 18;
            // 
            // OpenLogFile
            // 
            this.OpenLogFile.AutoSize = true;
            this.OpenLogFile.LinkColor = System.Drawing.Color.Black;
            this.OpenLogFile.Location = new System.Drawing.Point(655, 4);
            this.OpenLogFile.Name = "OpenLogFile";
            this.OpenLogFile.Size = new System.Drawing.Size(50, 13);
            this.OpenLogFile.TabIndex = 1;
            this.OpenLogFile.TabStop = true;
            this.OpenLogFile.Text = "Open log";
            this.OpenLogFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLogFile_LinkClicked);
            // 
            // EventLabel
            // 
            this.EventLabel.AutoSize = true;
            this.EventLabel.Location = new System.Drawing.Point(3, 4);
            this.EventLabel.Name = "EventLabel";
            this.EventLabel.Size = new System.Drawing.Size(72, 13);
            this.EventLabel.TabIndex = 0;
            this.EventLabel.Text = "Latest event: ";
            // 
            // TestingButton
            // 
            this.TestingButton.BackColor = System.Drawing.Color.Transparent;
            this.TestingButton.Image = ((System.Drawing.Image)(resources.GetObject("TestingButton.Image")));
            this.TestingButton.Location = new System.Drawing.Point(12, 429);
            this.TestingButton.Name = "TestingButton";
            this.TestingButton.Size = new System.Drawing.Size(89, 63);
            this.TestingButton.TabIndex = 19;
            this.TestingButton.UseVisualStyleBackColor = false;
            this.TestingButton.Click += new System.EventHandler(this.TestingButton_Click);
            // 
            // TestingPanel
            // 
            this.TestingPanel.BackColor = System.Drawing.Color.Transparent;
            this.TestingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestingPanel.Controls.Add(this.groupBox8);
            this.TestingPanel.Controls.Add(this.groupBox7);
            this.TestingPanel.Controls.Add(this.label15);
            this.TestingPanel.Location = new System.Drawing.Point(112, 15);
            this.TestingPanel.Name = "TestingPanel";
            this.TestingPanel.Size = new System.Drawing.Size(610, 477);
            this.TestingPanel.TabIndex = 20;
            this.TestingPanel.Visible = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.CleanLogs);
            this.groupBox8.Controls.Add(this.LogTable);
            this.groupBox8.Location = new System.Drawing.Point(14, 252);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(577, 210);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Log viewer";
            // 
            // CleanLogs
            // 
            this.CleanLogs.Location = new System.Drawing.Point(425, 178);
            this.CleanLogs.Name = "CleanLogs";
            this.CleanLogs.Size = new System.Drawing.Size(134, 23);
            this.CleanLogs.TabIndex = 1;
            this.CleanLogs.Text = "Clean log file";
            this.CleanLogs.UseVisualStyleBackColor = true;
            this.CleanLogs.Click += new System.EventHandler(this.CleanLogs_Click);
            // 
            // LogTable
            // 
            this.LogTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.LogTable.FullRowSelect = true;
            this.LogTable.GridLines = true;
            this.LogTable.Location = new System.Drawing.Point(9, 23);
            this.LogTable.Name = "LogTable";
            this.LogTable.Size = new System.Drawing.Size(550, 147);
            this.LogTable.TabIndex = 0;
            this.LogTable.UseCompatibleStateImageBehavior = false;
            this.LogTable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Date";
            this.columnHeader8.Width = 124;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            this.columnHeader9.Width = 69;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Event";
            this.columnHeader10.Width = 319;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.PermformTest);
            this.groupBox7.Controls.Add(this.TestData);
            this.groupBox7.Location = new System.Drawing.Point(14, 53);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(577, 193);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Test communications";
            // 
            // PermformTest
            // 
            this.PermformTest.Location = new System.Drawing.Point(425, 160);
            this.PermformTest.Name = "PermformTest";
            this.PermformTest.Size = new System.Drawing.Size(134, 23);
            this.PermformTest.TabIndex = 12;
            this.PermformTest.Text = "Perform test";
            this.PermformTest.UseVisualStyleBackColor = true;
            this.PermformTest.Click += new System.EventHandler(this.PermformTest_Click);
            // 
            // TestData
            // 
            this.TestData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader11});
            this.TestData.FullRowSelect = true;
            this.TestData.GridLines = true;
            this.TestData.Location = new System.Drawing.Point(9, 21);
            this.TestData.Name = "TestData";
            this.TestData.Size = new System.Drawing.Size(550, 131);
            this.TestData.TabIndex = 11;
            this.TestData.UseCompatibleStateImageBehavior = false;
            this.TestData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Host";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.Width = 91;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Port";
            this.columnHeader7.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Unmount";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Panic";
            this.columnHeader4.Width = 55;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "DMS";
            this.columnHeader5.Width = 55;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Config";
            this.columnHeader6.Width = 55;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Auth";
            this.columnHeader11.Width = 55;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(10, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 24);
            this.label15.TabIndex = 10;
            this.label15.Text = "Testing / Logging";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(739, 545);
            this.Controls.Add(this.TestingButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DMSButton);
            this.Controls.Add(this.TruecryptButton);
            this.Controls.Add(this.PanicButton);
            this.Controls.Add(this.HostsButton);
            this.Controls.Add(this.SettingsPanel);
            this.Controls.Add(this.HostsPanel);
            this.Controls.Add(this.TestingPanel);
            this.Controls.Add(this.HomePanel);
            this.Controls.Add(this.DMSPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Anti-Forensic Toolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.form1_Resize);
            this.HostsPanel.ResumeLayout(false);
            this.HostsPanel.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.DMSPanel.ResumeLayout(false);
            this.DMSPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.HomePanel.ResumeLayout(false);
            this.HomePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TestingPanel.ResumeLayout(false);
            this.TestingPanel.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HostsButton;
        private System.Windows.Forms.Button PanicButton;
        private System.Windows.Forms.Button TruecryptButton;
        private System.Windows.Forms.Button DMSButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel HostsPanel;
        private System.Windows.Forms.TextBox HostIp;
        private System.Windows.Forms.Button RemoveHostButton;
        private System.Windows.Forms.Button AddHostButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel DMSPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox DMSDevice;
        private System.Windows.Forms.Button IdentifyUSB;
        private System.Windows.Forms.Button GetUSBBaseline;
        private System.Windows.Forms.TextBox DMSPassword;
        private System.Windows.Forms.Button SaveDMS;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ListBox BaselineUSBDevices;
        private System.Windows.Forms.ListBox TempBaseline;
        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.Panel HomePanel;
        private System.Windows.Forms.Button EnableDMS;
        private System.Windows.Forms.TextBox PasswordCheck;
        private System.Windows.Forms.TextBox HostPort;
        private System.Windows.Forms.Button ClearDMS;
        private System.Windows.Forms.Button AboutAFT;
        private System.Windows.Forms.TextBox DMSExplained;
        private System.Windows.Forms.TextBox HostsExplained;
        private System.Windows.Forms.Button UnmountTrueCryptVolumes;
        private System.Windows.Forms.Button ShutdownComputer;
        private System.Windows.Forms.Button UnmountRemoteVolues;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Unmount;
        private System.Windows.Forms.CheckBox Screensaver;
        private System.Windows.Forms.CheckBox ShutPC;
        private System.Windows.Forms.CheckBox USBProtect;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.CheckBox ACProtect;
        private System.Windows.Forms.TextBox Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox HTTPListen;
        private System.Windows.Forms.CheckBox Forward;
        private System.Windows.Forms.TextBox SettingsExplained;
        private System.Windows.Forms.CheckBox UseBroadcast;
        private System.Windows.Forms.CheckBox Autostart;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox UDPPort;
        private System.Windows.Forms.TextBox HTTPPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox StartMinimized;
        private System.Windows.Forms.CheckBox AutostartDMS;
        private System.Windows.Forms.Button ShareConfiguration;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox HostPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button GenerateAuthKey;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox AuthKey;
        private System.Windows.Forms.Button DeleteAuthKey;
        private System.Windows.Forms.CheckBox UDPListen;
        private System.Windows.Forms.CheckBox EnableLogging;
        private System.Windows.Forms.CheckBox CheckHosts;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label EventLabel;
        private System.Windows.Forms.CheckBox PasswordAuthentication;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel OpenLogFile;
        private System.Windows.Forms.Button ManualHostCheck;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox HostName;
        private System.Windows.Forms.Button EditHost;
        private System.Windows.Forms.CheckBox ReceiveConfig;
        private System.Windows.Forms.Button SendDMS;
        private System.Windows.Forms.CheckBox RemoteDMS;
        private System.Windows.Forms.ListView HostData;
        private System.Windows.Forms.ColumnHeader RHostName;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader Port;
        private System.Windows.Forms.ColumnHeader Key;
        private System.Windows.Forms.ColumnHeader HostAlive;
        private System.Windows.Forms.Button TestingButton;
        private System.Windows.Forms.Panel TestingPanel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListView TestData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListView LogTable;
        private System.Windows.Forms.Button PermformTest;
        private System.Windows.Forms.Button CleanLogs;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.CheckBox AllowTesting;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListBox KillProcessList;
        private System.Windows.Forms.ListBox RunningProcesses;
        private System.Windows.Forms.Button GetProcessList;
        private System.Windows.Forms.Button AddToKillList;
        private System.Windows.Forms.Button RemoveFromKillList;
        private System.Windows.Forms.Button ClearKillProcessList;
        private System.Windows.Forms.CheckBox KillProcesses;

    }
}

