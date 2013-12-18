/*
BSD License
Copyright © belongs to Björn Ringmann
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of the owner nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;
using System.Linq.Expressions;


namespace AntiForensicToolkit
{
    public partial class Form1 : Form
    {
        /*
         *  Variables used by different parts of AFT
         *  
         */

        RegistryKey rkApp =         Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        string PanicParam =         "panic";
        string UnmountParam =       "AFTUnmount=";
        string ConfParam =          "AFTConfig=";
        string DMSParam =           "AFTDMS=";
        string AuthParam =          "AFTAuth=";
        string TestParam =          "AFTTest=";
        string srcParam =           "AFTSrc=";
        string ConfirmParam =       "AFTConfirm=";
        string IDParam =            "AFTID=";

        string AuthorizationKey =   ""; // This is the key the UDP broadcast will listen for!        
        string FilePath =           "C:\\Program Files\\Truecrypt\\Truecrypt.exe";
        string RunningPath =        Assembly.GetExecutingAssembly().Location;
        string LogFile =            "AFT.log";
        string ConfigFile =         "AFTC.ini";
        string Device =             "";
        string Password =           "";

        bool DMSEnabled =           false;
        bool ShutdownPC =           false;
        bool UnmountTC =            false;
        bool UDP =                  false;
        bool HTTP =                 false;
        bool Transmit =             false;
        bool Broadcast =            false;
        bool ACProtection =         false;
        bool USBProtection =        false;
        bool UseScreensaver =       false;
        bool AutostartTP =          false;
        bool MinimizedStartup =     false;
        bool DMSAutostart =         false;
        bool LoggingEnabled =       false;
        bool HostCheck =            false;
        bool UsePassword =          false;
        bool ConfUpdate =           false;
        bool EnableRemoteDMS =      false;
        bool Testing =              false;
        bool KillProc =             false;
        bool NetworkProtection =    false;

        int maximumXmovement =  10;
        int maximumYmovement =  10;

        int XPosition =         0;
        int YPosition =         0;

        Thread HttpSrv;
        Thread UdpSrv;

        private ContextMenu m_menu;   

        
        public Form1()
        {
            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (DMSEnabled && NetworkProtection)
           { 
                if (!e.IsAvailable)
                    MessageBox.Show("Network disconnected panic!!");
            }
        }

        /*
         * Form loaded, input some explanations for the interface
         * 
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.BringToFront();
            this.Focus();
            DMSExplained.Text = " 1. Unplug the USB device you would like to use as your switch. \r\n 2. Click the 'Get baseline' button \r\n 3. Insert your USB device. \r\n 4. Click the 'Identify USB' button. \r\n 5. Select a password. \r\n 6. Save your DMS. \r\n\r\n";
            DMSExplained.Text += "Unmount - Unmounts all mounted Truecrypt partitions\r\n\r\n";
            DMSExplained.Text += "Shutdown - Shuts down your computer.\r\n\r\n";
            DMSExplained.Text += "AC Protection - Panic if power is unplugged (laptops).\r\n\r\n";
            DMSExplained.Text += "USB Protection - Panic if any other USB device is plugged in.\r\n\r\n";
            DMSExplained.Text += "Use screensaver - Enable the screensaver with the DMS.\r\n\r\n";
            DMSExplained.Text += "Use password - Use the specified password to disable DMS.\r\n\r\n";
            DMSExplained.Text += "X / Y - Acceptable mouse movements.\r\n\r\n";

            SettingsExplained.Text += "============== Panic settings ==============\r\n\r\n";
            SettingsExplained.Text = "HTTP Listen - Listens for panics from HTTP.\r\n\r\n";
            SettingsExplained.Text = "UDP Listen - Listens for panics from UDP.\r\n\r\n";
            SettingsExplained.Text += "Panic to hosts - Send panic signal to selected hosts.\r\n\r\n";
            SettingsExplained.Text += "Panic to broadcast - Send panic signal to broadcast.\r\n\r\n";
            SettingsExplained.Text += "HTTP port - Listen port for HTTP.\r\n\r\n";
            SettingsExplained.Text += "UDP port - Listen port for UDP.\r\n\r\n";

            SettingsExplained.Text += "============== Enabling authentication ==============\r\n\r\n";
            SettingsExplained.Text += "1. Pick a password \r\n";
            SettingsExplained.Text += "2. Click Hash key \r\n\r\n";
            SettingsExplained.Text += "This auth is used for extra protection, select the same key when adding this host to another computer.\r\n";
            SettingsExplained.Text += "This also makes AFT compatible with panic_bcast and the -k argument.\r\n\r\n";

            SettingsExplained.Text += "============== Panic settings ==============\r\n\r\n";
            SettingsExplained.Text += "Autostart - Start AFT with computer.\r\n\r\n";
            SettingsExplained.Text += "Start minimized - Start AFT in system tray.\r\n\r\n";
            SettingsExplained.Text += "Autostart DMS - Start AFT with DMS enabled.\r\n\r\n";
            SettingsExplained.Text += "Remote DMS - Allows other AFT clients to enable DMS here.\r\n\r\n";
            SettingsExplained.Text += "Enable logging - Saves events to AFT.log .\r\n\r\n";
            SettingsExplained.Text += "Check hosts - Checks if selected hosts are alive.\r\n\r\n";
            SettingsExplained.Text += "Config updates - Allows other AFT clients to share their settings.\r\n\r\n";
            SettingsExplained.Text += "Testing - Allows other AFT clients to test connectivity.\r\n\r\n";


            HostsExplained.Text += "Host IP - Remote host running AFT or panic_bcast\r\n\r\n";
            HostsExplained.Text += "Host port - The remote port of the host\r\n\r\n";
            HostsExplained.Text += "Auth key - The auth key used for the remote host (defaults to panic)\r\n\r\n";
            HostsExplained.Text += "Add / Remove / edit hosts in the list\r\n\r\n";
            HostsExplained.Text += "Check hosts - Checks if hosts are alive.";

            this.ShowInTaskbar = false;

            //if (Unmount.Checked)
            //    UnmountTC = true;
            //if (HTTPListen.Checked)
            //    HTTP = true;
            //if (UDPListen.Checked)
            //    UDP = true;
            //if (Forward.Checked)
            //    Transmit = true;
            //if (UseBroadcast.Checked)
            //    Broadcast = true;
            //
            // Check and apply our configuration
            ArrayList config = checkConfiguration();
            if (config.Count == 0)
                checkFileExistance();
            ApplyConfiguration(config);

            if (LoggingEnabled)
                Log(3, "AFT starting up");

            // Check if we should start minimized.
            if (MinimizedStartup)
                this.WindowState = FormWindowState.Minimized;

            // Check if we should start with DMS enabled.
            if (DMSAutostart)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                EnableDMS.PerformClick();
            }
                

            // Start the HTTP Listener thread
            HttpSrv = new Thread(HTTPListener);
            HttpSrv.IsBackground = true;
            HttpSrv.Start();

            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0,
                new MenuItem("Open", new System.EventHandler(Show_Click)));
            m_menu.MenuItems.Add(1,
                new MenuItem("Panic", new System.EventHandler(Panic_Click)));
            m_menu.MenuItems.Add(1,
                new MenuItem("Enable DMS", new System.EventHandler(EnableDMS_Click)));
            m_menu.MenuItems.Add(2,
                new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            notifyIcon1.ContextMenu = m_menu;

            ReadLog();
        }

        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
        }

        protected void Panic_Click(Object sender, System.EventArgs e)
        {
            Panic();
        }

        protected void DMS_Click(Object sender, System.EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            EnableDMS.PerformClick();
        }

        protected void Show_Click(Object sender, System.EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        private void form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "AFT";
            notifyIcon1.BalloonTipText = "I'm still running";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }




        /*
         * Save configuration when closing the form.
         * 
         */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DMSEnabled)
                Panic();
            else
            {
                SaveConfiguration();
                notifyIcon1.Visible = false;
                notifyIcon1.Icon = null;
            }
        }



        /*
         * Tooltips...
         *  
         */
        private void Home_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.Home, "Home sweet home");
        }

        private void TruecryptButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.TruecryptButton, "Locate TrueCrypt executable.");
        }

        private void PanicButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.PanicButton, "Panic!");
        }

        private void HostsButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.HostsButton, "Specify which hosts to notify during a panic.");
        }

        private void DMSButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.DMSButton, "Configure a Dead Man's Switch and settings.");
        }

        private void SettingsButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.SettingsButton, "General settings for this program to work as expected.");
        }

        private void TestingButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.SettingsButton, "Communication testing and logging.");
        }

        /*
         * Show our home screen and hide everything else.
         * 
         */
        private void Home_Click_1(object sender, EventArgs e)
        {
            HomePanel.Show();
            DMSPanel.Hide();
            HostsPanel.Hide();
            SettingsPanel.Hide();
            TestingPanel.Hide();
        }

        /*
         * Locate our truecrypt executable - VITAL
         * 
         */
        private void TruecryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\Program Files\";
            openFileDialog1.Filter = "Executable files (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog1.FileName.ToString();
            }
        }

        /*
         * Cause a manual panic!
         * 
         */
        private void PanicButton_Click(object sender, EventArgs e)
        {
            Panic();
        }

        /*
         * Configure your hosts
         * 
         */
        private void HostsButton_Click(object sender, EventArgs e)
        {
            HomePanel.Hide();
            DMSPanel.Hide();
            HostsPanel.Show();
            SettingsPanel.Hide();
            TestingPanel.Hide();
        }

        /*
         * Configure your DMS
         * 
         */
        private void DMSButton_Click(object sender, EventArgs e)
        {
            HomePanel.Hide();
            DMSPanel.Show();
            HostsPanel.Hide();
            SettingsPanel.Hide();
            TestingPanel.Hide();
        }

        /*
         * General configurations
         * 
         */
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            HomePanel.Hide();
            DMSPanel.Hide();
            HostsPanel.Hide();
            SettingsPanel.Show();
            TestingPanel.Hide();
        }

        /*
         * Testing configuration
         * 
         */
        private void TestingButton_Click(object sender, EventArgs e)
        {
            HomePanel.Hide();
            DMSPanel.Hide();
            HostsPanel.Hide();
            SettingsPanel.Hide();
            TestingPanel.Show();
        }




        /*
         *  Show information about AFT.
         */

        private void AboutAFT_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        /*
         *  Unmount local truecrypt volumes.
         *  
         */
        private void UnmountTrueCryptVolumes_Click(object sender, EventArgs e)
        {
            UnmountEncryptedPartitions();
        }

        /*
         *  Unmount local truecrypt volumes.
         *  
         */

        /*
         *  Unmount remote truecrypt volumes.
         *  
         */
        private void UnmountRemoteVolumes_Click(object sender, EventArgs e)
        {
            //UDPBroadcast(GetBroadcastAddress(), CalculateMD5(PanicParam));
            UnmountRemoteVolumes();
            //SendPanicToHosts();
        }

        public void UnmountRemoteVolumes()
        {
            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    string ip = HostData.Items[i].SubItems[2].Text;
                    string port = HostData.Items[i].SubItems[3].Text;
                    string authHash = HostData.Items[i].SubItems[4].Text;
                    UDPBroadcast(ip + ":" + port, UnmountParam + "&" + AuthParam + authHash);
                }
            }
        }

        /*
         *  Manually shut down computer.
         *  
         */
        private void ShutdownComputer_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        /* 
         * Enable our DMS when we leave the computer.
         * If used correct this will call for a panic if someone is touching the computer.
         * 
         */
        private void EnableDMS_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("HelloDMS");
            if (Device != "")
            {
               
                if (LoggingEnabled)
                    Log(3, "DMS ENABLED");
                this.Show();
                this.WindowState = FormWindowState.Normal;

                DMSPanel.Hide();
                SettingsPanel.Hide();
                HostsPanel.Hide();
                HomePanel.Show();

                if (UseScreensaver)
                    TurnOnScreenSaver();

                PasswordCheck.Show();
                PasswordCheck.Select();
                this.Opacity = 0.01;
                DMSEnabled = true;
                XPosition = Cursor.Position.X;
                YPosition = Cursor.Position.Y;

                //Create a background worker to check for mouse movement
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;

                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    DMS();
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    this.Opacity = 1;
                    PasswordCheck.Hide();
                    PasswordCheck.Text = "";
                });

                bw.RunWorkerAsync();
                PasswordCheck.Select();
            }
            else
            {
                MessageBox.Show("You need to specify a DMS");
            }
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage
            (IntPtr hWnd,
            uint Msg,
            uint wParam,
            uint lParam);
        public const uint WM_SYSCOMMAND = 0x112;
        public const uint SC_SCREENSAVE = 0xF140;
        public enum SpecialHandles
        {
            HWND_DESKTOP = 0x0,
            HWND_BROADCAST = 0xFFFF
        }
        public static void TurnOnScreenSaver()
        {
            SendMessage(
                new IntPtr((int)SpecialHandles.HWND_BROADCAST),
                WM_SYSCOMMAND,
                SC_SCREENSAVE,
                0);
        }







        /*
         * Add a host to the alarm list.
         * 
         */
        private void AddHostButon_Click(object sender, EventArgs e)
        {
            int portNum;
            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ip.Matches(HostIp.Text);

            string authHash = "";
            if (HostPassword.Text != "")
                authHash = CalculateMD5(PanicParam + HostPassword.Text).ToLower();

            if (HostName.Text != "")
            {
                if (result.Count > 0)
                {
                    if (int.TryParse(HostPort.Text, out portNum))
                    {
                        HostData.Items.Add(new ListViewItem(new[] { " - ", HostName.Text, HostIp.Text, HostPort.Text, authHash }));
                        TestData.Items.Add(new ListViewItem(new[] { HostName.Text, HostIp.Text, HostPort.Text, "-", "-", "-", "-", "-" }));

                        //FriendlyHostList.Items.Add(HostName.Text);
                        HostName.Text = "";

                        //Hosts.Items.Add(HostIp.Text + ":" + HostPort.Text.Trim() + " - " + authHash);
                        HostIp.Text = "";
                        HostPort.Text = "";
                        HostPassword.Text = "";
                    }
                    else
                        MessageBox.Show("Enter a valid port number");
                }
                else
                    MessageBox.Show("Enter a valid IP address");
            }
            else
                MessageBox.Show("Please enter a hostname");
            
        }

        /*
         * Edit selected host
         * 
         */
        private void EditHost_Click(object sender, EventArgs e)
        {
            
            try {
                string hostname = HostData.SelectedItems[0].SubItems[1].Text;
                string hostip = HostData.SelectedItems[0].SubItems[2].Text;
                string hostport = HostData.SelectedItems[0].SubItems[3].Text;
                HostName.Text = hostname;
                HostIp.Text = hostip;
                HostPort.Text = hostport;
                TestData.Items.RemoveAt(HostData.SelectedIndices[0]);
                HostData.Items.Remove(HostData.SelectedItems[0]);
            }
            catch (Exception) { }
        }

        /*
         * Remove selected host
         * 
         */
        private void RemoveHostButton_Click(object sender, EventArgs e)
        {
            try
            {
                TestData.Items.RemoveAt(HostData.SelectedIndices[0]);
                HostData.Items.Remove(HostData.SelectedItems[0]);
            }
            catch (Exception)
            {

            }

        }





        /*
         * Add connected USB devices to our Baseline list.
         * 
         */
        private void GetUSBBaseline_Click(object sender, EventArgs e)
        {
            // Clean previous results.
            DMSDevice.Items.Clear();
            BaselineUSBDevices.Items.Clear();
            TempBaseline.Items.Clear();

            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                BaselineUSBDevices.Items.Add(usbDevice.DeviceID.ToString());
            }
        }

        /*
         * Identifies DMS (Compares connected USB devices against Basline list)
         * 
         */
        private void IdentifyUSB_Click(object sender, EventArgs e)
        {
            DMSDevice.Items.Clear();
            TempBaseline.Items.Clear();

            var usbDevices = GetUSBDevices();

            foreach (var usbDevice in usbDevices)
            {
                string deviceID = usbDevice.DeviceID.ToString();
                TempBaseline.Items.Add(usbDevice.DeviceID.ToString());
                DMSDevice.Items.Add(usbDevice.DeviceID.ToString());
            }
            foreach (var objLib in TempBaseline.Items)
            {
                if (!BaselineUSBDevices.Items.Contains(objLib))
                {
                    DMSDevice.Text = objLib.ToString();
                    Device = objLib.ToString();
                }
            }
        }

        /*
         * Save our DMS password and deviceID in global variables.
         * 
         */
        private void SaveDMS_Click(object sender, EventArgs e)
        {
            if (DMSDevice.Items.Count > 0 && DMSPassword.Text != "")
            {
                Device = DMSDevice.SelectedItem.ToString();
                Password = CalculateMD5(DMSPassword.Text);
                if(LoggingEnabled)
                    Log(3, "DMS device saved");
            }
            else
            {
                MessageBox.Show("You need to specify the device ID and password");
            }
        }

        /*
         *  Reset the values for our DMS configuration.
         *  
         */
        private void ClearDMS_Click(object sender, EventArgs e)
        {
            DMSDevice.Items.Clear();
            BaselineUSBDevices.Items.Clear();
            TempBaseline.Items.Clear();
        }




        /*
         * Is our DMS USB device present?
         * 
         * @returns bool
         */
        public bool checkDMS()
        {
            bool deviceDetected = false;
            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                if (Device == usbDevice.DeviceID.ToString())
                    deviceDetected = true;
            }
            return deviceDetected;
        }


        /*
         * Lets see if the user has entered the correct passphrase 
         * 
         */
        private void PasswordCheck_Leave(object sender, EventArgs e)
        {
            if (DMSEnabled)
                Panic();
        }

        private void PasswordCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt || e.Control || e.KeyCode == Keys.Tab)
            {
                Panic();
            }
        }
        private void PasswordCheck_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CalculateMD5(PasswordCheck.Text) != Password)
                {
                    // Not correct? Lets panic.
                    Panic();
                    //this.Opacity = 100;
                    PasswordCheck.Hide();
                    PasswordCheck.Text = "";
                }
                else
                {
                    // Correct, disable our switches :)
                    DMSEnabled = false;
                    this.Opacity = 100;
                    PasswordCheck.Hide();
                    PasswordCheck.Text = "";
                }
            }
        }


        /*
         * Want to know what a DMS is? Check this out :)
         * 
         */
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process myProcess = new Process();
            Process.Start("http://en.wikipedia.org/wiki/Dead-man_switch");
        }


        /*
         * Starting/stopping our UDP listener thread.
         * 
         */
        private void UDPListen_CheckedChanged(object sender, EventArgs e)
        {
            if (UDPListen.Checked)
            {
                UDPPort.Enabled = true;
                Forward.Enabled = true;
                UseBroadcast.Enabled = true;
                AllowTesting.Enabled = true;
            }
            else
            {
                UDPPort.Enabled = false;
                Forward.Enabled = false;
                Forward.Checked = false;
                UseBroadcast.Enabled = false;
                UseBroadcast.Checked = false;
                AllowTesting.Enabled = false;
                AllowTesting.Checked = false;
            }
            UdpSrv = new Thread(new ThreadStart(UDPListener));
            UdpSrv.IsBackground = true;

            if (UDPListen.Checked)
            {
                UDP = true;
                UdpSrv.Start();
                if (LoggingEnabled)
                    Log(3, "UDP listener started");
                ReceiveConfig.Enabled = true;
                RemoteDMS.Enabled = true;
            }
            else
            {
                UDP = false;
                UdpSrv.Abort();
                if (LoggingEnabled)
                    Log(2, "UDP listener stopped!");
                ReceiveConfig.Checked = false;
                ReceiveConfig.Enabled = false;
                RemoteDMS.Checked = false;
                RemoteDMS.Enabled = false;
            }
        }

        /*
         * Starting/stopping our HTTP listener thread.
         * 
         */
        private void Listener_CheckedChanged(object sender, EventArgs e)
        {
            if (HTTPListen.Checked)
            {
                HTTPPort.Enabled = true;
                HTTP = true;
                try
                {
                    if (HttpSrv == null)
                    {
                        HttpSrv = new Thread(HTTPListener);
                        HttpSrv.IsBackground = true;
                        HttpSrv.Start();
                    }
                    else if (!HttpSrv.IsAlive)
                    {
                        HttpSrv.Abort();
                        HttpSrv = new Thread(HTTPListener);
                        HttpSrv.IsBackground = true;
                        HttpSrv.Start();
                    }
                    
                }
                catch (Exception)
                {
                    
                }
                if (LoggingEnabled)
                    Log(3, "HTTP listener started");
            }
            else
            {
                HTTPPort.Enabled = false;
                HTTP = false;
                HttpSrv.Abort();
                if (LoggingEnabled)
                    Log(2, "HTTP listener stopped");
            }
        }

        /*
         * Setting our bool ( Forward/Transmit panics )
         * 
         */
        private void Forward_CheckedChanged(object sender, EventArgs e)
        {
            if (Forward.Checked)
            {
                Transmit = true;
                if (LoggingEnabled)
                    Log(3, "Sending panic signals to selected hosts");
            }
            else
            {
                Transmit = false;
                if (LoggingEnabled)
                    Log(2, "Not panic sending signals to selected hosts.");
            }
        }

        /*
        * Setting our bool ( Broadcast panics )
        * 
        */
        private void UseBroadcast_CheckedChanged(object sender, EventArgs e)
        {
            if (UseBroadcast.Checked)
            {
                Broadcast = true;
                if (LoggingEnabled)
                    Log(3, "Sending panic signals to broadcast address");
            }
            else
            {
                Broadcast = false;
                if (LoggingEnabled)
                    Log(3, "Not sending panic signals to broadcast address");
            }
        }



        /*
         * Setting our bool ( Unmount local partitions )
         * 
         */
        private void Unmount_CheckedChanged(object sender, EventArgs e)
        {
            if (Unmount.Checked)
            {
                UnmountTC = true;
                if (LoggingEnabled)
                    Log(3, "Will unmount TrueCrypt volumes on panic");
            }
            else
            {
                UnmountTC = false;
                if (LoggingEnabled)
                    Log(2, "Will NOT unmount TrueCrypt volumes!");
            }
        }


        /*
         * Setting our bool ( Shutdown computer )
         * 
         */
        private void ShutPC_CheckedChanged(object sender, EventArgs e)
        {
            if (ShutPC.Checked)
                ShutdownPC = true;
            else
                ShutdownPC = false;
        }

        /*
         * Setting our bool ( Panic of power chord is unplugged )
         * 
         */
        private void ACProtect_CheckedChanged(object sender, EventArgs e)
        {
            if (ACProtect.Checked)
                ACProtection = true;
            else
                ACProtection = false;
        }

        /*
         * Setting our bool ( Panic if any other USB is being plugged in )
         * 
         */
        private void USBProtect_CheckedChanged(object sender, EventArgs e)
        {
            if (USBProtect.Checked)
                USBProtection = true;
            else
                USBProtection = false;
        }

        /*
         * Setting our bool ( Enable the screensaver with DMS )
         * 
         */
        private void Screensaver_CheckedChanged(object sender, EventArgs e)
        {
            if (Screensaver.Checked)
                UseScreensaver = true;
            else
                UseScreensaver = false;
        }

        /*
         * Setting our bool ( Autostart AFT with computer )
         * 
         */
        private void Autostart_CheckedChanged(object sender, EventArgs e)
        {
            if (Autostart.Checked)
            {
                rkApp.SetValue("AFT", Application.ExecutablePath.ToString());
                AutostartTP = true;
                if(LoggingEnabled)
                    Log(3, "Added to autostart");
            }
            else
            {
                rkApp.DeleteValue("AFT", false);
                AutostartTP = false;
                if (LoggingEnabled)
                    Log(3, "Removed from autostart");
            }
        }

        /*
         * Setting our bool ( Start TP minimized )
         * 
         */
        private void StartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (StartMinimized.Checked)
            {
                MinimizedStartup = true;
                if (LoggingEnabled)
                    Log(3, "Starting minimized");

            }
            else
            {
                MinimizedStartup = false;
                if (LoggingEnabled)
                    Log(3, "Starting normal");
            }
        }


        /*
         * Setting our bool ( Start AFT with DMS enabled )
         * 
         */
        private void AutostartDMS_CheckedChanged(object sender, EventArgs e)
        {
            if (AutostartDMS.Checked)
            {
                DMSAutostart = true;
                if (LoggingEnabled)
                    Log(3, "AFT starts with DMS enabled");
            }
            else
            {
                DMSAutostart = false;
                if (LoggingEnabled)
                    Log(3, "AFT starts with DMS disabled");
            }
        }

        /*
         * Setting our bool ( logging )
         * 
         */
        private void EnableLogging_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableLogging.Checked)
            {
                LoggingEnabled = true;
                CheckHosts.Enabled = true;
                if (LoggingEnabled)
                    Log(3, "Logging enabled");
            }
            else
            {
                LoggingEnabled = false;
                CheckHosts.Enabled = false;
                CheckHosts.Checked = false;
                if (LoggingEnabled)
                    Log(3, "Logging disabled");
            }
        }

        /*
         * Setting our bool ( Password authentication use )
         * 
         */
        private void PasswordAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            if (PasswordAuthentication.Checked)
            {
                UsePassword = true;
                if (LoggingEnabled)
                    Log(3, "Using password authentication when DMS disable");
            }
            else
            {
                UsePassword = false;
                if (LoggingEnabled)
                    Log(2, "Only using USB device as authentication!");
            }
        }


        /*
         * Setting our bool ( Configuration updates )
         * 
         */
        private void ReceiveConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (ReceiveConfig.Checked)
            {
                ConfUpdate = true;
                if (LoggingEnabled)
                    Log(2, "Configuration updates enabled!");
            }
            else
            {
                ConfUpdate = false;
                if (LoggingEnabled)
                    Log(3, "Configuration updates disabled");
            }
            
        }

        /*
         * Setting our bool ( Remote DMS feature )
         * 
         */
        private void RemoteDMS_CheckedChanged(object sender, EventArgs e)
        {
            if (RemoteDMS.Checked)
            {
                EnableRemoteDMS = true;
                if (LoggingEnabled)
                    Log(3, "Remote DMS has been enabled");
            }
            else
            {
                EnableRemoteDMS = false;
                if (LoggingEnabled)
                    Log(3, "Remote DMS has been disabled");
            }
        }



        /*
         * Setting our bool ( Check if our hosts are responding )
         * 
         */
        private void CheckHosts_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckHosts.Checked)
            {
                HostCheck = true;
                //Create a background worker to check for mouse movement
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;

                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    Ping();
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    this.Opacity = 1;
                    PasswordCheck.Hide();
                    PasswordCheck.Text = "";
                });

                bw.RunWorkerAsync();
                if (LoggingEnabled)
                    Log(3, "Host check enabled");
            }
            else
            {
                HostCheck = false;
                if (LoggingEnabled)
                    Log(3, "Host check disabled");
            }
        }

        private void AllowTesting_CheckedChanged(object sender, EventArgs e)
        {
            if (AllowTesting.Checked)
            {
                MessageBox.Show("Allowing testing will ONLY allow testing, hence no panics, DMS or configuration updates will be possible during this time!\n\nSHUT THIS OFF WHEN YOU ARE DONE!",
                    "Critical Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                Testing = true;
            }
            else
            {
                Testing = false;
            }
        }

        private void NetworkProtect_CheckedChanged(object sender, EventArgs e)
        {
            if (NetworkProtect.Checked)
                NetworkProtection = true;
            else
                NetworkProtection = false;
        }

        private void ManualHostCheck_Click(object sender, EventArgs e)
        {
            ManualPing();
        }

        public void ManualPing()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "AFTAFTAFTAFTAFTAFTAFTAFT";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    HostData.Items[i].UseItemStyleForSubItems = false;
                    string host = HostData.Items[i].SubItems[2].Text;

                    PingReply reply = pingSender.Send(host, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        HostData.Items[i].SubItems[0].Text = "Yes";
                        HostData.Items[i].SubItems[0].BackColor = Color.Green;
                    }
                    else
                    {
                        HostData.Items[i].SubItems[0].Text = "No";
                        HostData.Items[i].SubItems[0].BackColor = Color.Red;
                    }
                }
                
            }

        }



        public  void Log(int logLevel, string Event)
        {
            string level;
            switch (logLevel)
            {
                case 1:
                    level = "Error";
                    break;
                case 2:
                    level = "Warning";
                    break;
                default:
                    level = "Info";
                    break;
            }

            DateTime now = DateTime.Now;
            string date = String.Format("{0:u}", now);
            date = date.Remove(date.Length - 1);
            string[] dt = date.Split(' ');
            
            

            this.Invoke((MethodInvoker)delegate
            {
                if(logLevel != 3)
                    EventLabel.ForeColor = System.Drawing.Color.Red;
                else
                    EventLabel.ForeColor = System.Drawing.Color.Black;

                EventLabel.Text = "Latest event: " + dt[1] + " - " + Event;
                LogTable.Items.Add(new ListViewItem(new[] { dt[1], level, Event }));
            });

            try
            {
                TextWriter tw = new StreamWriter(LogFile, true);
                tw.WriteLine("[ " + date + " ]" + " [ " + level + " ]\t" + Event);
                tw.Close();
            }
            catch (Exception)
            {

            }
            
        }

        private void OpenLogFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(LogFile);
        }


        public void Ping()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "AFTAFTAFTAFTAFTAFTAFTAFT";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;


            while (HostCheck && !DMSEnabled)
            {

                if (HostData.Items.Count > 0)
                {
                    for (int i = 0; i < HostData.Items.Count; i++)
                    {
                        string host = "";
                        this.Invoke((MethodInvoker)delegate
                        {
                            HostData.Items[i].UseItemStyleForSubItems = false;
                            host = HostData.Items[i].SubItems[2].Text;
                        });
                        
                        

                        PingReply reply = pingSender.Send(host, timeout, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Log(3, host + " is up!");
                            this.Invoke((MethodInvoker)delegate
                            {
                                HostData.Items[i].SubItems[0].Text = "Yes";
                                HostData.Items[i].SubItems[0].BackColor = Color.Green;
                            });
                        }
                        else
                        {
                            Log(1, host + " is not responding!");
                            this.Invoke((MethodInvoker)delegate
                            {
                                HostData.Items[i].SubItems[0].Text = "No";
                                HostData.Items[i].SubItems[0].BackColor = Color.Red;
                            });
                        }

                    }
                }
                System.Threading.Thread.Sleep(600000);
            }
        }


        /*
         * DMS - Check to see if the mouse has been touched, is our DMS USB plugged in? 
         * 
         */
        public void DMS()
        {
            int maximumX = XPosition + Convert.ToInt32(X.Text);
            int minimumX = XPosition - Convert.ToInt32(X.Text);
            int maximumY = YPosition + Convert.ToInt32(Y.Text);
            int minimumY = YPosition - Convert.ToInt32(Y.Text);

            Boolean initValue = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline; // Initial power value.
            Boolean isRunningOnBattery;

            ArrayList baselineDevices = GetDevices();
            ArrayList newDevices = new ArrayList();
            while (DMSEnabled == true)
            {
                // Check if any unrecognized USB device is plugged in.
                if (USBProtection)
                {
                    newDevices = GetDevices();
                    foreach (string device in newDevices)
                    {
                        if (!baselineDevices.Contains(device) && !device.Equals(Device))
                        {
                            DMSEnabled = false;
                            Panic();
                            break;
                        }

                        if(device.Equals(Device) && !UsePassword)
                        {
                            if (LoggingEnabled)
                                Log(3, "DMS disabled using USB key only!");

                            DMSEnabled = false;
                            break;
                        }
                    }
                    newDevices.Clear();
                    newDevices.TrimToSize();
                }


                // Check if the mouse position is out of the accepted area.
                if (Cursor.Position.X > maximumX || Cursor.Position.X < minimumX || Cursor.Position.Y > maximumY || Cursor.Position.Y < minimumY)
                {
                    if (checkDMS() == true)
                    {
                        // Everything seems fine and dandy
                        break;
                    }
                    else
                    {
                        // Okay, lets panic.
                        DMSEnabled = false;
                        Panic();
                        break;
                    }
                }

                // User has enabled the AC power protection (Unplugging AC will cause panics!)
                if (ACProtection)
                {
                    isRunningOnBattery = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline;
                    if (initValue != isRunningOnBattery)
                    {
                        DMSEnabled = false;
                        Panic();
                        break;
                    }
                }

                System.Threading.Thread.Sleep(1000);
            }
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced); // DON'T DO THIS!!!
        }


        /*
         *  Panic - Our magic, calls to the needed functions
         *  
         */
        public void Panic()
        {
            if(UnmountTC)
                UnmountEncryptedPartitions();
            if(Transmit)
                SendPanicToHosts();
            if (Broadcast)
                UDPBroadcast(GetBroadcastAddress() + ":" + UDPPort.Text, CalculateMD5(PanicParam));
            if (KillProc)
                KillProcess();
            if (ShutdownPC)
                Shutdown();

            DMSEnabled = false;
        }


        /*
         *  Unmount truecrypt partitions - @FilePath containing path to Truecrypt is required.
         *  
         */
        public void UnmountEncryptedPartitions()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = @FilePath;
            info.Arguments = "/d /f";
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Process.Start(info);
        }


        /*
         *  Function to alert remote hosts specified in our Host list.
         *  This is useful when having multiple computers spread out at different locations (Home, Work, Secret bunker)
         * 
         */
        public void SendPanicToHosts()
        {
            
            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    string ip = HostData.Items[i].SubItems[2].Text;
                    string port = HostData.Items[i].SubItems[3].Text;
                    string authHash = HostData.Items[i].SubItems[4].Text;
                    Thread PB_client = new Thread(() => UDPBroadcast(ip+":"+port, authHash));
                    PB_client.Start();
                }
            }
        }

        /*
         *  Grab the broadcast address from our users local subnet.
         *  
         *  @returns string
         */
        public string GetBroadcastAddress()
        {
            IPHostEntry host;
            string localIP = "";
            string bcastAddr = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    string[] octets = localIP.Split('.');
                    bcastAddr += octets[0] + "." + octets[1] + "." + octets[2] + "." + "255";
                    break;
                }
            }
            return bcastAddr;
        }

        public string GetLocalAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        

        /*
         * Lets craft a UDP packet which will be either broadcasted or directly sent to a host from our host list.
         * 
         */
        public void UDPBroadcast(string host, string data)
        {
            //MessageBox.Show("Sending to: " + host + "\n\n" + data);
            string[] hostData = host.Split(':');
            string ip = hostData[0];
            string port = hostData[1];
            
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress send_to_address = IPAddress.Parse(ip);


            bool panicSignal = data.StartsWith("panic");
            if(!panicSignal)
                data = EncodeTo64(data);

            data = Regex.Replace(data, @"(.{2})", "$1\\x");
            data = "/\\x" + data;
            data = data.Remove(data.Length - 2);

            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, Convert.ToInt32(port));
            byte[] send_buffer = Encoding.ASCII.GetBytes(data);

            try
            {
                
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                //if(LoggingEnabled)
                 //   Log(1, "Could not send panic signal to " + host);
            }
        }

        /*
         *  Shut down the computer
         *  
         */
        public static void Shutdown()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = @"C:\Windows\System32\shutdown.exe";
            info.Arguments = "-f -t 1 -s";
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Process.Start(info);
        }

        /*
         *  Function to calculate an MD5 hash from a string
         * 
         *  @returns string
         */
        public string CalculateMD5(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /*
         *  Starts a local webserver listen on the specified port for incoming requests.
         *  
         */
        public void HTTPListener()
        {
            HttpListener listener = new HttpListener();
            string localServerAddr = "http://*:" + HTTPPort.Text + "/panic/";
            listener.Prefixes.Add(localServerAddr);
            try
            {
                listener.Start();
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
                result.AsyncWaitHandle.WaitOne();
                Panic();
                listener.Stop();
                return;
            }
            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.Net.HttpListenerException")
                {
                    if (LoggingEnabled)
                        Log(1, "Could not start HTTP listener!");
                    MessageBox.Show("Could not start HTTP Listener! \rPlease check that your current HTTP port is not in use!\rPort 8080 could be used by Skype.",
                    "Critical Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
                listener.Prefixes.Remove(localServerAddr);
                return;
            }
        }

        /*
         *  Listener callback function
         *  
         */
        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
        }


        public void SetTestResults(int id, int field, bool ok)
        {
            if (ok)
            {
                TestData.Items[id].UseItemStyleForSubItems = false;
                TestData.Items[id].SubItems[field].BackColor = Color.Green;
            }
        }


        /*
         *  Starts a UDP Listener to recieve broadcasts
         *  
         */
        public void UDPListener()
        {
            int listenPort = Convert.ToInt32(UDPPort.Text);
            bool done = false;

            try
            {
                UdpClient listener = new UdpClient(listenPort);
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
                string received_data;
                byte[] receive_byte_array;

                while (!done)
                {
                    receive_byte_array = listener.Receive(ref groupEP);
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    received_data = received_data.Replace("\\x", "");
                    received_data = received_data.Replace("/", "");
                    received_data = DecodeFrom64(received_data); // Data is encoded using base64, decode it.
                    //MessageBox.Show(received_data);
                    if (!Testing)
                    {
                        bool foundAuthParam = received_data.StartsWith(AuthParam);
                        bool foundDMSParam = received_data.StartsWith(DMSParam);
                        bool foundPanicParam = received_data.StartsWith(PanicParam);
                        bool foundUnmountParam = received_data.StartsWith(UnmountParam);
                        bool foundConfParam = received_data.StartsWith(ConfParam);

                        if (received_data == AuthorizationKey.ToLower() && UDP == true)
                            Panic();
                        else
                            listener.Close();

                        if (foundUnmountParam)
                        {
                            string[] auth = Regex.Split(received_data, "&AFTAuth=");
                            if (AuthorizationKey.Equals(auth[1].ToString()))
                            {
                                UnmountEncryptedPartitions();
                            }
                        }
                        if (foundDMSParam)
                        {
                            string[] auth = Regex.Split(received_data, "&AFTAuth=");

                            if (EnableRemoteDMS && AuthorizationKey.Equals(auth[1].ToString()))
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    EnableDMS.PerformClick();
                                });
                            }
                        }
                        if (foundConfParam)
                        {
                            received_data = received_data.Replace(ConfParam, "");
                            string[] data = Regex.Split(received_data, AuthParam);
                            if (ConfUpdate && AuthorizationKey == data[1])
                            {
                                using (StringReader reader = new StringReader(DecodeFrom64(data[0])))
                                {
                                    TextWriter tw = new StreamWriter(ConfigFile);
                                    string line;
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        tw.WriteLine(line);
                                    }
                                    tw.Close();
                                }


                                this.Invoke((MethodInvoker)delegate
                                {
                                    ArrayList config = checkConfiguration();
                                    HostData.Items.Clear();
                                    TestData.Items.Clear();
                                    ApplyConfiguration(config);
                                });


                                if (LoggingEnabled)
                                    Log(2, "Received configuration file!");
                            }
                        }
                    }
                    else
                    {
                        bool foundConfirmParam = received_data.StartsWith(ConfirmParam);
                        bool foundTestParam = received_data.StartsWith(TestParam);
                        bool foundIdParam = received_data.StartsWith(IDParam);

                        if (foundTestParam) // We have received a test signal
                        {
                            received_data = received_data.Replace(TestParam + "&", "");

                            bool foundSrcParam = received_data.StartsWith(srcParam);

                            if (foundSrcParam) // To test we need to know where to send the ACK.
                            {
                                string[] testData = received_data.Split('&');
                                string host = testData[0].Replace(srcParam, "");
                                string hostID = "";
                                string Confirm = "";

                                for (int i = 0; i < testData.Count(); i++)
                                {
                                    foundIdParam = testData[i].StartsWith(IDParam);
                                    if (foundIdParam)
                                    {
                                        hostID = testData[i].Replace(IDParam, "");
                                    }
                                }

                                Confirm = ConfirmParam + "&" + IDParam + hostID;

                                for (int i = 0; i < testData.Count(); i++)
                                {
                                    bool foundDMSParam = testData[i].StartsWith(DMSParam);
                                    bool foundConfParam = testData[i].StartsWith(ConfParam);
                                    bool foundAuthParam = testData[i].StartsWith(AuthParam);
                                    bool foundPanicParam = testData[i].StartsWith(PanicParam);
                                    bool foundUnmountParam = testData[i].StartsWith(UnmountParam);
                                    //foundIdParam = testData[i].StartsWith(IDParam);


                                    if (foundDMSParam)
                                    {
                                        //MessageBox.Show("Got DMS test");
                                        Confirm += "&" + DMSParam;
                                        //UDPBroadcast(host, ConfirmParam + "&" + IDParam + hostID + "&" + DMSParam);
                                    }
                                    if (foundConfParam)
                                    {
                                        Confirm += "&" + ConfParam;
                                        //MessageBox.Show("Got Config test");
                                        //UDPBroadcast(host, ConfirmParam + "&" + IDParam + hostID + "&" + ConfParam);
                                    }
                                    if (foundPanicParam)
                                    {
                                        Confirm += "&" + PanicParam;
                                        //MessageBox.Show("Got Panic test");
                                        //UDPBroadcast(host, ConfirmParam + "&" + IDParam + hostID + "&" + PanicParam);
                                    }
                                    if (foundAuthParam)
                                    {
                                        
                                        //MessageBox.Show("Got Auth test");
                                        string auth = testData[i].Replace(AuthParam, "");
                                        if (AuthorizationKey == auth)
                                            Confirm += "&" + AuthParam;
                                    }
                                    if (foundUnmountParam)
                                    {
                                        Confirm += "&" + UnmountParam;
                                        //MessageBox.Show("Got Unmount test");
                                        //UDPBroadcast(host, ConfirmParam + "&" + IDParam + hostID + "&" + UnmountParam);
                                    }

                                }
                                UDPBroadcast(host, Confirm);
                            }
                        }

                        if (foundConfirmParam) // We have received a confirm signal 
                        {
                            bool foundDMSParam = false;
                            bool foundConfParam = false;
                            bool foundAuthParam = false;
                            bool foundPanicParam = false;
                            bool foundUnmountParam = false;

                            received_data = received_data.Replace(ConfirmParam + "&", "");

                            string[] confirmData = received_data.Split('&');
                            int id = 0;

                            for (int i = 0; i < confirmData.Count(); i++)
                            {
                                foundIdParam = confirmData[i].StartsWith(IDParam);
                                if (foundIdParam)
                                    id = int.Parse(confirmData[i].Replace(IDParam, ""));
                            }
                            this.Invoke((MethodInvoker)delegate
                            {
                                TestData.Items[id].UseItemStyleForSubItems = false;
                            });

                            for (int i = 0; i < confirmData.Count(); i++)
                            {
                                if(confirmData[i].StartsWith(DMSParam))
                                    foundDMSParam = confirmData[i].StartsWith(DMSParam);
                                if (confirmData[i].StartsWith(ConfParam))
                                    foundConfParam = confirmData[i].StartsWith(ConfParam);
                                if (confirmData[i].StartsWith(AuthParam))
                                    foundAuthParam = confirmData[i].StartsWith(AuthParam);
                                if (confirmData[i].StartsWith(PanicParam))
                                    foundPanicParam = confirmData[i].StartsWith(PanicParam);
                                if (confirmData[i].StartsWith(UnmountParam))
                                    foundUnmountParam = confirmData[i].StartsWith(UnmountParam);
                            }

                            if (foundUnmountParam)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    TestData.Items[id].SubItems[3].BackColor = Color.Green;
                                    TestData.Items[id].SubItems[3].Text = "YES";
                                });
                            }
                            if (foundPanicParam)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    TestData.Items[id].SubItems[4].BackColor = Color.Green;
                                    TestData.Items[id].SubItems[4].Text = "YES";
                                });
                            }
                            if (foundDMSParam)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    TestData.Items[id].SubItems[5].BackColor = Color.Green;
                                    TestData.Items[id].SubItems[5].Text = "YES";
                                });
                            }
                            if (foundConfParam)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    TestData.Items[id].SubItems[6].BackColor = Color.Green;
                                    TestData.Items[id].SubItems[6].Text = "YES";
                                });
                            }
                            if (foundAuthParam)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    TestData.Items[id].SubItems[7].BackColor = Color.Green;
                                    TestData.Items[id].SubItems[7].Text = "YES";
                                });
                            }

                        }
                    }                    
                }
            }
            catch (Exception)
            {
                
            }
        }

        /*
         * Check if a TrueCrypt executable exists in our guessed location.
         * 
         */
        private void checkFileExistance()
        {
            bool FileExists = File.Exists(FilePath);
            if (FileExists)
            {
                DialogResult dialogResult = MessageBox.Show("We found Truecrypt.exe for you, is this correct?\n" + FilePath, "Found truecrypt file", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    FilePath = "";
                }
                else
                {
                    if (LoggingEnabled)
                        Log(3, "Found TrueCrypt.exe");
                }
            }
        }

        /*
         * Read and load our configuration to an ArrayList used by ApplyConfiguration()
         * 
         * @returns ArrayList
         */
        public static ArrayList checkConfiguration()
        {
            bool FileExists = File.Exists("AFTC.ini");
            ArrayList config = new ArrayList();

            if (FileExists)
            {
                string line = "";
                TextReader tr = new StreamReader("AFTC.ini");
                int counter = 0;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] options = line.ToString().Split('=');
                    config.Add(options[1]);
                    counter++;
                }
                tr.Close();
            }
            return config;
        }

        /*
         * Save our running configuration
         * 
         */
        public void SaveConfiguration()
        {
            TextWriter tw = new StreamWriter(ConfigFile);
            tw.WriteLine("FILEPATH=" + FilePath);
            tw.WriteLine("DEVICE=" + Device);
            tw.WriteLine("KEY=" + Password);
            tw.WriteLine("HTTP_LISTEN=" + HTTPListen.Checked.ToString());
            tw.WriteLine("UNMOUNT=" + Unmount.Checked.ToString());
            tw.WriteLine("SHUTDOWN=" + ShutPC.Checked.ToString());
            tw.WriteLine("FORWARD=" + Forward.Checked.ToString());
            tw.WriteLine("MAX_X=" + X.Text);
            tw.WriteLine("MAX_Y=" + Y.Text);
            tw.WriteLine("HTTP=" + HTTPPort.Text);
            tw.WriteLine("UDP=" + UDPPort.Text);

            string HostList = "";
            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    string name = HostData.Items[i].SubItems[1].Text;
                    string ip = HostData.Items[i].SubItems[2].Text;
                    string port = HostData.Items[i].SubItems[3].Text;
                    string key = HostData.Items[i].SubItems[4].Text;
                    HostList += name + " @ " + ip + ":" + port + " - " + key + ",";
                }
                HostList = HostList.Remove(HostList.Length - 1);
            }

            tw.WriteLine("WARN=" + HostList);
            tw.WriteLine("BROADCAST=" + UseBroadcast.Checked.ToString());
            tw.WriteLine("AC_PROTECTION=" + ACProtect.Checked.ToString());
            tw.WriteLine("USB_PROTECTION=" + USBProtect.Checked.ToString());
            tw.WriteLine("SCREENSAVER=" + Screensaver.Checked.ToString());
            tw.WriteLine("AUTOSTART=" + Autostart.Checked.ToString());
            tw.WriteLine("START_MINIMIZED=" + StartMinimized.Checked.ToString());
            tw.WriteLine("LOGGING=" + EnableLogging.Checked.ToString());
            tw.WriteLine("AUTOSTART_DMS=" + AutostartDMS.Checked.ToString());
            tw.WriteLine("AUTH_KEY=" + AuthKey.Text);
            tw.WriteLine("UDP_LISTEN=" + UDPListen.Checked.ToString());
            tw.WriteLine("CHECK_HOSTS=" + CheckHosts.Checked.ToString());
            tw.WriteLine("USE_PASSWORD=" + PasswordAuthentication.Checked.ToString());
            tw.WriteLine("CONF_UPDATES=" + ReceiveConfig.Checked.ToString());
            tw.WriteLine("REMOTE_DMS=" + RemoteDMS.Checked.ToString());
            tw.WriteLine("TESTING=" + AllowTesting.Checked.ToString());
            tw.WriteLine("KILL_PROCESSES=" + KillProcesses.Checked.ToString());
            if (KillProcessList.Items.Count > 0)
            {
                string pKill = "";
                for (int i = 0; i < KillProcessList.Items.Count; i++)
                {
                    pKill += KillProcessList.Items[i].ToString() + ",";
                }
                pKill = pKill.Remove(pKill.Length - 1);
                tw.WriteLine("PKILL=" + pKill);
            }
            else
                tw.WriteLine("PKILL=");
            tw.WriteLine("NW_PROTECTION=" + NetworkProtect.Checked.ToString());
            
            tw.Close();
        }

        /*
        *  Apply configuration from our config file.
         *  
        */
        public void ApplyConfiguration(ArrayList config)
        {
            if (config.Count > 0)
            {
                if (config[0].ToString() == "")
                    checkFileExistance();
                else
                    FilePath = config[0].ToString();

                if (config[1].ToString() != "")
                {
                    Device = config[1].ToString();
                    DMSDevice.Items.Add(Device);
                    DMSDevice.Text = Device;
                }

                if (config[2].ToString() != "")
                {
                    Password = config[2].ToString();
                }

                UnmountTC = Convert.ToBoolean(config[4].ToString());
                Unmount.Checked = UnmountTC;

                ShutdownPC = Convert.ToBoolean(config[5].ToString());
                ShutPC.Checked = ShutdownPC;

                Transmit = Convert.ToBoolean(config[6].ToString());
                Forward.Checked = Transmit;

                Broadcast = Convert.ToBoolean(config[12].ToString());
                UseBroadcast.Checked = Broadcast;
               
                ACProtection = Convert.ToBoolean(config[13].ToString());
                ACProtect.Checked = ACProtection;

                USBProtection = Convert.ToBoolean(config[14].ToString());
                USBProtect.Checked = USBProtection;

                UseScreensaver = Convert.ToBoolean(config[15].ToString());
                Screensaver.Checked = UseScreensaver;

                AutostartTP = Convert.ToBoolean(config[16].ToString());
                Autostart.Checked = AutostartTP;

                MinimizedStartup = Convert.ToBoolean(config[17].ToString());
                StartMinimized.Checked = MinimizedStartup;

                DMSAutostart = Convert.ToBoolean(config[19].ToString());
                AutostartDMS.Checked = DMSAutostart;

                if (config[20].ToString() != "")
                {
                    AuthorizationKey = config[20].ToString();
                    AuthKey.Text = AuthorizationKey;
                    AuthKey.Enabled = false;
                }
                else
                {
                    AuthorizationKey = CalculateMD5(PanicParam);
                }

                UDP = Convert.ToBoolean(config[21].ToString());
                UDPListen.Checked = UDP;

                HostCheck = Convert.ToBoolean(config[22].ToString());
                CheckHosts.Checked = HostCheck;

                UsePassword = Convert.ToBoolean(config[23].ToString());
                PasswordAuthentication.Checked = UsePassword;

                if (UDP)
                {
                    ConfUpdate = Convert.ToBoolean(config[24].ToString());
                    ReceiveConfig.Checked = ConfUpdate;
                }
                else
                    ConfUpdate = false;

                EnableRemoteDMS = Convert.ToBoolean(config[25].ToString());
                RemoteDMS.Checked = EnableRemoteDMS;

                Testing = Convert.ToBoolean(config[26].ToString());
                AllowTesting.Checked = Testing;

                KillProc = Convert.ToBoolean(config[27].ToString());
                KillProcesses.Checked = KillProc;

                string[] KillList = config[28].ToString().Split(',');

                if(KillList.Count() > 0 && KillList[0] != "")
                    for (int i = 0; i < KillList.Count(); i++)
                    {
                        KillProcessList.Items.Add(KillList[i]);
                    }

                NetworkProtection = Convert.ToBoolean(config[29].ToString());
                NetworkProtect.Checked = NetworkProtection;

                maximumXmovement = Convert.ToInt32(config[7]);
                X.Text = config[7].ToString();
                maximumYmovement = Convert.ToInt32(config[8]);
                Y.Text = config[7].ToString();

                HTTPPort.Text = config[9].ToString();

                UDPPort.Text = config[10].ToString();

                string[] WarnList = config[11].ToString().Split(',');

                if(WarnList.Count() > 0 && WarnList[0] != "")
                    for (int i = 0; i < WarnList.Count(); i++)
                    {
                        string[] hostdata = Regex.Split(WarnList[i], " @ ");
                        string hostname = hostdata[0];
                        hostdata = Regex.Split(hostdata[1], ":");
                        string ip = hostdata[0];
                        hostdata = Regex.Split(hostdata[1], " - ");
                        string port = hostdata[0];
                        string authKey = hostdata[1];

                        HostData.Items.Add(new ListViewItem(new[] { " - ", hostname, ip, port, authKey }));
                        TestData.Items.Add(new ListViewItem(new[] { hostname, ip, port, "-", "-", "-", "-", "-" }));
                    }

                LoggingEnabled = Convert.ToBoolean(config[18].ToString());
                EnableLogging.Checked = LoggingEnabled;

                HTTP = Convert.ToBoolean(config[3].ToString());
                HTTPListen.Checked = HTTP;
            }
        }


        /*
         * Grab the USB connected devices.
         * 
         * @returns List
         */
        public static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }

        public static ArrayList GetDevices()
        {
            ArrayList list = new ArrayList();

            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                list.Add(usbDevice.DeviceID.ToString());
            }
            return list;
        }

        private void GenerateAuthKey_Click(object sender, EventArgs e)
        {
            if (AuthKey.Text != "")
            {
                AuthKey.Text = CalculateMD5(PanicParam + AuthKey.Text).ToLower();
                AuthorizationKey = AuthKey.Text;
                AuthKey.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter a password to be used as an auth key!");
            }
        }

        private void DeleteAuthKey_Click(object sender, EventArgs e)
        {
            AuthKey.Text = "";
            AuthorizationKey = CalculateMD5(PanicParam);
            AuthKey.Enabled = true;
            if (LoggingEnabled)
                Log(2, "No auth key in use!");
        }

        private void ShareConfiguration_Click(object sender, EventArgs e)
        {
            SaveConfiguration();

            bool FileExists = File.Exists(ConfigFile);
            string config = "";
            if (FileExists)
            {
                string line = "";
                TextReader tr = new StreamReader(ConfigFile);
                while ((line = tr.ReadLine()) != null)
                {
                    config += line.ToString() + "\n";
                }
                config = EncodeTo64(config);
                tr.Close();

                

                if (HostData.Items.Count > 0)
                {
                    for (int i = 0; i < HostData.Items.Count; i++)
                    {
                        string host = HostData.Items[i].SubItems[2].Text;
                        string port = HostData.Items[i].SubItems[3].Text;
                        string authKey = HostData.Items[i].SubItems[4].Text;
                        UDPBroadcast(host + ":" + port, ConfParam + config + AuthParam + authKey);
                    }
                }
            }
        }

        public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        private void SendDMS_Click(object sender, EventArgs e)
        {
            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    string host = HostData.Items[i].SubItems[2].Text;
                    string port = HostData.Items[i].SubItems[3].Text;
                    string authKey = HostData.Items[i].SubItems[4].Text;
                    UDPBroadcast(host + ":" + port, DMSParam + "&" + AuthParam + authKey);
                }
            }
        }

        private void CleanLogs_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter(LogFile, false);
            tw.Write("");
            tw.Close();
            LogTable.Items.Clear();

        }

        public void ReadLog()
        {
            LogTable.Items.Clear();
            bool FileExists = File.Exists(LogFile);
            if (FileExists)
            {
                string line = "";
                TextReader tr = new StreamReader(LogFile);
                while ((line = tr.ReadLine()) != null)
                {
                    string[] logData = Regex.Split(line, "\t");
                    string logEvent = logData[1];


                    logData = Regex.Split(logData[0], "]");
                    string date = logData[0].Replace("[ ", "");
                    logData = Regex.Split(logData[1], " ]");
                    string type = logData[0].Replace("[ ", "");
                    LogTable.Items.Add(new ListViewItem(new[] { date, type, logEvent }));
                }
                tr.Close();
            }
        }

        private void PermformTest_Click(object sender, EventArgs e)
        {
            if (Testing)
            {
                if (HostData.Items.Count > 0)
                {
                    for (int i = 0; i < HostData.Items.Count; i++)
                    {
                        TestData.Items[i].UseItemStyleForSubItems = false;
                        TestData.Items[i].SubItems[3].Text = "NO";
                        TestData.Items[i].SubItems[3].BackColor = Color.Red;
                        TestData.Items[i].SubItems[4].Text = "NO";
                        TestData.Items[i].SubItems[4].BackColor = Color.Red;
                        TestData.Items[i].SubItems[5].Text = "NO";
                        TestData.Items[i].SubItems[5].BackColor = Color.Red;
                        TestData.Items[i].SubItems[6].Text = "NO";
                        TestData.Items[i].SubItems[6].BackColor = Color.Red;
                        TestData.Items[i].SubItems[7].Text = "NO";
                        TestData.Items[i].SubItems[7].BackColor = Color.Red;

                        string host = HostData.Items[i].SubItems[2].Text;
                        string port = HostData.Items[i].SubItems[3].Text;
                        string authKey = HostData.Items[i].SubItems[4].Text;
                        UDPBroadcast(host + ":" + port,
                            TestParam + "&" +
                            srcParam + GetLocalAddress() + ":" + UDPPort.Text + "&" +
                            PanicParam + "&" +
                            UnmountParam + "&" +
                            DMSParam + "&" +
                            ConfParam + "&" +
                            IDParam + i + "&" +
                            AuthParam + authKey);
                    }
                }
            }
            else
            {
                MessageBox.Show("You need to allow testing for this to work!");
            }
        }

        private void GetProcessList_Click(object sender, EventArgs e)
        {
            RunningProcesses.Items.Clear();
            try
            {
                Process[] processes = Process.GetProcesses();
                ArrayList procList = new ArrayList();
                foreach(Process proc in processes)
                {
                    procList.Add(proc.ProcessName);
                }

                procList.Sort();
                foreach(object proc in procList)
                {
                    if(!RunningProcesses.Items.Contains(proc))
                    {
                        RunningProcesses.Items.Add(proc);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void AddToKillList_Click(object sender, EventArgs e)
        {
            int procId = RunningProcesses.SelectedIndex;
            string procName = RunningProcesses.Items[procId].ToString();
            if(!KillProcessList.Items.Contains(procName))
                KillProcessList.Items.Add(procName);
        }

        private void RemoveFromKillList_Click(object sender, EventArgs e)
        {
            int procId = KillProcessList.SelectedIndex;
            KillProcessList.Items.RemoveAt(procId);
        }

        private void ClearKillProcessList_Click(object sender, EventArgs e)
        {
            KillProcessList.Items.Clear();
        }

        private void KillProcesses_CheckedChanged(object sender, EventArgs e)
        {
            if (KillProcesses.Checked)
                KillProc = true;
            else
                KillProc = false;
        }

        public void KillProcess()
        {
            foreach(object procName in KillProcessList.Items)
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(procName.ToString());
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                }
                catch (Exception)
                {

                }
            }
        }








    }

    /*
     *  Our USB class
     * 
     */
    public class USBDeviceInfo
    {
        public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
    }
}