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
using System.Configuration;
using InTheHand.Net.Sockets;


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

        ArrayList USBDMSDevices =      new ArrayList();
        ArrayList USBDMSEvents =       new ArrayList();
        ArrayList BTDMSDevices =       new ArrayList();
        ArrayList BTDMSEvents =        new ArrayList();
        ArrayList BTDMSActions =       new ArrayList();

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
        bool AutostartAFT =         false;
        bool MinimizedStartup =     false;
        bool DMSAutostart =         false;
        bool LoggingEnabled =       false;
        bool HostCheck =            false;
        bool ConfUpdate =           false;
        bool EnableRemoteDMS =      false;
        bool Testing =              false;
        bool KillProc =             false;
        bool NetworkProtection =    false;
        bool DetectingDMS =         false;
        bool BluetoothEnabled =     false;
        bool BluetoothDisableDMS =  false;

        int maximumXmovement =  10;
        int maximumYmovement =  10;

        int XPosition =         0;
        int YPosition =         0;

        Thread HttpSrv;
        Thread UdpSrv;
        Thread btListener;

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
                   Panic();
            }
        }

        /*
         * Form loaded, input some explanations for the interface
         * 
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
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

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                ConfigFile = config.FilePath;
                ApplyConfiguration();
            }
            catch (Exception)
            {

            }

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
                

            // Start the Bluetooth listener
            btListener = new Thread(new ThreadStart(HandleBluetoothDMS));
            if(BluetoothEnabled)
                btListener.Start();

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
                btListener.Abort();
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
            if (USBDMSDevices.Count > 0)
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

                        HostName.Text = "";
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
            USBDMSDetect.Text = "";
            USBLoading.Visible = true;
            DetectingDMS = true;
            Thread workerThread = new Thread(DetectNewDMS);
            workerThread.Start();
        }



        /*
         * Save our DMS password and deviceID in global variables.
         * 
         */
        private void SaveDMS_Click(object sender, EventArgs e)
        {
            string dmsName = "";
            string dmsId = "";
            string dmsEvent = "";
            if (USBDMSDevice.Items.Count > 0)
            {
                dmsId = USBDMSDevice.SelectedItem.ToString();

                if (USBDMSName.Text != "")
                {
                    dmsName = USBDMSName.Text;
                    if (USBDMSAction.SelectedIndex != -1 && USBDMSAction.SelectedItem.ToString() != "")
                    {
                        dmsEvent = USBDMSAction.SelectedItem.ToString();

                        USBDMSDeviceList.Items.Add(new ListViewItem(new[] { "False", dmsEvent, dmsName, dmsId }));
                        USBDMSName.Text = "";
                        if (LoggingEnabled)
                            Log(3, "DMS device saved");
                        USBDMSDevice.Items.Clear();
                    }
                    else
                        MessageBox.Show("You need to specify an event");
                }
                
                
            }
            else
            {
                MessageBox.Show("No DMS device has been selected");
            }
        }

        /*
         *  Reset the values for our DMS configuration.
         *  
         */
        private void ClearDMS_Click(object sender, EventArgs e)
        {
            USBDMSDevice.Items.Clear();
            USBDMSName.Text = "";
            //DMSPassword.Text = "";
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
            UdpSrv = new Thread(new ThreadStart(UDPListener));
            UdpSrv.IsBackground = true;

            if (UDPListen.Checked)
            {
                UDPPort.Enabled = true;
                Forward.Enabled = true;
                UseBroadcast.Enabled = true;
                RemoteDMS.Enabled = true;
                ReceiveConfig.Enabled = true;
                AllowTesting.Enabled = true;
                UDP = true;
                UdpSrv.Start();
                if (LoggingEnabled)
                    Log(3, "UDP listener started");
            }
            else
            {
                Forward.Enabled = false;
                Forward.Checked = false;
                UseBroadcast.Enabled = false;
                UseBroadcast.Checked = false;
                RemoteDMS.Enabled = false;
                RemoteDMS.Checked = false;
                ReceiveConfig.Enabled = false;
                ReceiveConfig.Checked = false;
                AllowTesting.Enabled = false;
                AllowTesting.Checked = false;
                UDP = false;
                UdpSrv.Abort();
                UDPBroadcast("127.0.0.1:"+UDPPort.Text, "KILL");
                if (LoggingEnabled)
                    Log(2, "UDP listener stopped!");
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
                AutostartAFT = true;
                if(LoggingEnabled)
                    Log(3, "Added to autostart");
            }
            else
            {
                rkApp.DeleteValue("AFT", false);
                AutostartAFT = false;
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
                MessageBox.Show("Testing only works with local addresses so far!");
                Testing = true;
                if (LoggingEnabled)
                    Log(3, "Testing enabled");
            }
            else
            {
                Testing = false;
                if (LoggingEnabled)
                    Log(3, "Testing disabled");
            }
        }

        private void NetworkProtect_CheckedChanged(object sender, EventArgs e)
        {
            if (NetworkProtect.Checked)
            {
                NetworkProtection = true;
                if (LoggingEnabled)
                    Log(3, "Network protection enabled");
            }
            else
            {
                NetworkProtection = false;
                if (LoggingEnabled)
                    Log(2, "Network protection disabled!");
            }
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

        public void DetectNewDMS()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddSeconds(15);
            DateTime now = new DateTime();
            ArrayList baselineDevices = GetDevices();
            ArrayList newDevices = new ArrayList();
            while(DetectingDMS)
            {
                now = DateTime.Now;
                int result = DateTime.Compare(now, end);
                this.Invoke((MethodInvoker)delegate
                {
                    int countdown = 15 - (now - start).Seconds;
                    USBDMSDetect.Text = "         " + countdown.ToString();
                });
                if (result > 0)
                {
                    this.Invoke((MethodInvoker)delegate {
                        USBLoading.Visible = false;
                        USBDMSDetect.Text = "No device found!"; 
                    });
                    return;
                }
                newDevices = GetDevices();
                foreach (string device in newDevices)
                {
                    if (!baselineDevices.Contains(device) && !device.Equals(Device))
                    {
                        bool exists = false;
                        for (int i = 0; i < USBDMSDeviceList.Items.Count; i++)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                if (device.Equals(USBDMSDeviceList.Items[i].SubItems[3].Text))
                                    exists = true;
                            });
                        }

                        if (!exists)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                USBLoading.Visible = false;
                                USBDMSDetect.Text = "Device found!";
                                USBDMSDevice.Items.Clear();
                                USBDMSDevice.Items.Add(device);
                                USBDMSDevice.SelectedIndex = 0;
                                //DMSID.Text = device;
                            });
                            DetectingDMS = false;
                            return;
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate {
                                USBLoading.Visible = false;
                                USBDMSDetect.Text = "Duplicate device!"; 
                            });
                            DetectingDMS = false;
                            return;
                        }
                    }
                }
                
            }
        }

        [DllImport("user32")]
        public static extern void LockWorkStation();

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

                if (BluetoothDisableDMS)
                {
                    DMSEnabled = false;
                    if (LoggingEnabled)
                        Log(2, "DMS Disabled with bluetooth device");
                    break;
                }


                // Check if any unrecognized USB device is plugged in.
                newDevices = GetDevices();

                foreach (string device in newDevices) // Loop through the device list.
                {
                    if (!baselineDevices.Contains(device)) // Baseline does not contain the new USB device.
                    {
                        if (USBProtection && !USBDMSDevices.Contains(device)) // USB protection enabled and device is not recognized, panic!
                        {
                            DMSEnabled = false;
                            Panic();
                        }
                        if (USBDMSDevices.Contains(device)) // Our plugged in device is in our saved device-list, perform actions.
                        {
                            for (int i = 0; i < USBDMSDevices.Count; i++) // Loop through saved devices
                            {
                                if (USBDMSDevices[i].Equals(device)) // Device found
                                {
                                    if (USBDMSEvents[i].Equals("Key"))
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "DMS Disabled with key device");
                                        DMSEnabled = false;
                                        break;
                                    }
                                    if (USBDMSEvents[i].Equals("Panic"))
                                    {
                                        DMSEnabled = false;
                                        Panic();
                                        break;
                                    }
                                    if (USBDMSEvents[i].Equals("Lock"))
                                    {
                                        LockWorkStation();
                                        if (LoggingEnabled)
                                            Log(2, "Desktop locked with device");
                                    }
                                }
                            }
                        }
                    }
                }


                // Check if the mouse position is out of the accepted area.
                if (Cursor.Position.X > maximumX || Cursor.Position.X < minimumX || Cursor.Position.Y > maximumY || Cursor.Position.Y < minimumY)
                {
                    // Okay, lets panic.
                    DMSEnabled = false;
                    Panic();
                    break;
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
            catch (Exception)
            {

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
            while (HTTP)
            {
                string localServerAddr = "http://*:" + HTTPPort.Text + "/panic/";
                listener.Prefixes.Add(localServerAddr);
                try
                {
                    listener.Start();
                    IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
                    result.AsyncWaitHandle.WaitOne();
                    Panic();
                    listener.Prefixes.Remove(localServerAddr);
                    //listener.Stop();
                    //return;
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

        /*
         *  Starts a UDP Listener to recieve broadcasts
         *  
         */
        public void UDPListener()
        {
            int listenPort = Convert.ToInt32(UDPPort.Text);
                try
                {
                    UdpClient listener = new UdpClient(listenPort);
                    IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
                    string received_data;
                    byte[] receive_byte_array;
                    
                    while (UDP)
                    {
                        receive_byte_array = listener.Receive(ref groupEP);
                        received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                        received_data = received_data.Replace("\\x", "");
                        received_data = received_data.Replace("/", "");
                        received_data = DecodeFrom64(received_data); // Data is encoded using base64, decode it.

                        bool foundAuthParam = received_data.StartsWith(AuthParam);
                        bool foundDMSParam = received_data.StartsWith(DMSParam);
                        bool foundPanicParam = received_data.StartsWith(PanicParam);
                        bool foundUnmountParam = received_data.StartsWith(UnmountParam);
                        bool foundConfParam = received_data.StartsWith(ConfParam);

                        if (received_data == AuthorizationKey.ToLower())
                            Panic(); 

                        if (foundUnmountParam)
                        {
                            string[] auth = Regex.Split(received_data, "&AFTAuth=");
                            if (AuthorizationKey.Equals(auth[1].ToString()))
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UnmountEncryptedPartitions();
                                });
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
                                    HostData.Items.Clear();
                                    TestData.Items.Clear();
                                    USBDMSDeviceList.Items.Clear();
                                    KillProcessList.Items.Clear();
                                    ApplyConfiguration();
                                });


                                if (LoggingEnabled)
                                    Log(2, "Received configuration file!");
                            }
                        }
                        if (Testing)
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
                                        foundDMSParam = testData[i].StartsWith(DMSParam);
                                        foundConfParam = testData[i].StartsWith(ConfParam);
                                        foundAuthParam = testData[i].StartsWith(AuthParam);
                                        foundPanicParam = testData[i].StartsWith(PanicParam);
                                        foundUnmountParam = testData[i].StartsWith(UnmountParam);


                                        if (foundDMSParam)
                                        {
                                            if (LoggingEnabled)
                                                Log(3, "Recieved DMS test from " + host);
                                            Confirm += "&" + DMSParam;
                                        }
                                        if (foundConfParam)
                                        {
                                            if (LoggingEnabled)
                                                Log(3, "Recieved configuration test from " + host);
                                            Confirm += "&" + ConfParam;
                                        }
                                        if (foundPanicParam)
                                        {
                                            if (LoggingEnabled)
                                                Log(3, "Recieved panic test from " + host);
                                            Confirm += "&" + PanicParam;
                                        }
                                        if (foundAuthParam)
                                        {
                                            if (LoggingEnabled)
                                                Log(3, "Recieved auth test from " + host);
                                            string auth = testData[i].Replace(AuthParam, "");
                                            if (AuthorizationKey == auth)
                                            {
                                                Confirm += "&" + AuthParam;

                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    if (LoggingEnabled)
                                                        Log(2, "Received auth test from " + host);
                                                });

                                            }
                                            else
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    if (LoggingEnabled)
                                                        Log(2, "Received auth test from " + host + " - FAILED!");
                                                });
                                            }
                                        }
                                        if (foundUnmountParam)
                                        {
                                            if (LoggingEnabled)
                                                Log(3, "Recieved unmount test from " + host);
                                            Confirm += "&" + UnmountParam;
                                        }

                                    }
                                    UDPBroadcast(host, Confirm);
                                }
                            }

                            if (foundConfirmParam) // We have received a confirm signal 
                            {
                                foundDMSParam = false;
                                foundConfParam = false;
                                foundAuthParam = false;
                                foundPanicParam = false;
                                foundUnmountParam = false;

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
                                    if (confirmData[i].StartsWith(DMSParam))
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
                                        if (LoggingEnabled)
                                            Log(3, "Unmount OK from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "Unmount FAILED from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                if (foundPanicParam)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        TestData.Items[id].SubItems[4].BackColor = Color.Green;
                                        TestData.Items[id].SubItems[4].Text = "YES";
                                        if (LoggingEnabled)
                                            Log(3, "Panic OK from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "Panic FAILED from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                if (foundDMSParam)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        TestData.Items[id].SubItems[5].BackColor = Color.Green;
                                        TestData.Items[id].SubItems[5].Text = "YES";
                                        if (LoggingEnabled)
                                            Log(3, "DMS OK from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "DMS FAILED from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                if (foundConfParam)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        TestData.Items[id].SubItems[6].BackColor = Color.Green;
                                        TestData.Items[id].SubItems[6].Text = "YES";
                                        if (LoggingEnabled)
                                            Log(3, "Config OK from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "Config FAILED from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                if (foundAuthParam)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        TestData.Items[id].SubItems[7].BackColor = Color.Green;
                                        TestData.Items[id].SubItems[7].Text = "YES";
                                        if (LoggingEnabled)
                                            Log(3, "Auth OK from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "Auth FAILED from " + TestData.Items[id].SubItems[0].Text);
                                    });
                                }

                            }
                        }
                    } listener.Close();
                }
                catch (Exception)
                {

                }
        }

        /*
         * Check if a TrueCrypt executable exists in our guessed location.
         * 
         */
        private void checkFileExistance(string FilePath)
        {
            bool FileExists = File.Exists(FilePath);
            if (FileExists)
            {
                DialogResult dialogResult = MessageBox.Show("We found Truecrypt.exe for you, is this correct?\n" + FilePath, "Found truecrypt file", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
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
                else
                {

                }
            }
            else if(!FileExists || FilePath == "")
            {
                DialogResult dialogResult = MessageBox.Show("AFT was unable to locate TrueCrypt.exe! \nLocate now?", "Could not find TrueCrypt.exe", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    FilePath = "";
                }
                else
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
            }
        }

        /*
         * Save our running configuration
         * 
         */
        public void SaveConfiguration()
        {

            // General settings
            Properties.Settings.Default.TrueCryptPath = FilePath;
            Properties.Settings.Default.AuthKey = AuthKey.Text;
            Properties.Settings.Default.HTTPListener = HTTPListen.Checked;
            Properties.Settings.Default.UDPListener = UDPListen.Checked;
            
            Properties.Settings.Default.TransmitToBC = UseBroadcast.Checked;
            Properties.Settings.Default.TransmitToHosts = Forward.Checked;
            Properties.Settings.Default.Autostart = Autostart.Checked;
            Properties.Settings.Default.StartMinimized = StartMinimized.Checked;
            Properties.Settings.Default.Logging = EnableLogging.Checked;
            Properties.Settings.Default.AutostartDMS = AutostartDMS.Checked;
            Properties.Settings.Default.CheckHosts = CheckHosts.Checked;
            Properties.Settings.Default.ConfigUpdates = ReceiveConfig.Checked;
            Properties.Settings.Default.RemoteDMS = RemoteDMS.Checked;
            Properties.Settings.Default.Testing = AllowTesting.Checked;
            

            // Host settings
            string HostList = "";
            if (HostData.Items.Count > 0)
            {
                for (int i = 0; i < HostData.Items.Count; i++)
                {
                    string name = HostData.Items[i].SubItems[1].Text;
                    string ip = HostData.Items[i].SubItems[2].Text;
                    string port = HostData.Items[i].SubItems[3].Text;
                    string key = HostData.Items[i].SubItems[4].Text;
                    HostList += name + ";" + ip + ";" + port + ";" + key + ",";
                }
                HostList = HostList.Remove(HostList.Length - 1);
            }
            Properties.Settings.Default.Hosts = HostList;

            // USB DMS Settings
            if (USBDMSDeviceList.Items.Count > 0)
            {
                string dmsString = "";
                for (int i = 0; i < USBDMSDeviceList.Items.Count; i++)
                {
                    dmsString += USBDMSDeviceList.Items[i].SubItems[0].Text + ";" +
                        USBDMSDeviceList.Items[i].SubItems[1].Text + ";" +
                        USBDMSDeviceList.Items[i].SubItems[2].Text + ";" +
                        USBDMSDeviceList.Items[i].SubItems[3].Text + ","; 
                }
                dmsString = dmsString.Remove(dmsString.Length - 1);
                Properties.Settings.Default.DMSDevices = dmsString;
            }
            else
                Properties.Settings.Default.DMSDevices = "";

            // BT DMS Settings
            if (BTDMSDeviceList.Items.Count > 0)
            {
                string dmsString = "";
                for (int i = 0; i < BTDMSDeviceList.Items.Count; i++)
                {
                    dmsString += BTDMSDeviceList.Items[i].SubItems[0].Text + ";" +
                        BTDMSDeviceList.Items[i].SubItems[1].Text + ";" +
                        BTDMSDeviceList.Items[i].SubItems[2].Text + ";" +
                        BTDMSDeviceList.Items[i].SubItems[3].Text + ",";
                }
                dmsString = dmsString.Remove(dmsString.Length - 1);
                Properties.Settings.Default.BTDMSDevices = dmsString;
            }
            else
                Properties.Settings.Default.BTDMSDevices = "";

            Properties.Settings.Default.Shutdown = ShutPC.Checked;
            Properties.Settings.Default.Unmount = Unmount.Checked;
            Properties.Settings.Default.MouseMax_X = maximumXmovement;
            Properties.Settings.Default.MouseMax_Y = maximumYmovement;
            Properties.Settings.Default.ACProtection = ACProtect.Checked;
            Properties.Settings.Default.USBProtection = USBProtect.Checked;
            Properties.Settings.Default.Screensaver = Screensaver.Checked;
            Properties.Settings.Default.NetworkProtection = NetworkProtect.Checked;
            Properties.Settings.Default.KillProcesses = KillProcesses.Checked;
            Properties.Settings.Default.EnableBluetooth = EnableBluetooth.Checked;

            if (KillProcessList.Items.Count > 0)
            {
                string pKill = "";
                for (int i = 0; i < KillProcessList.Items.Count; i++)
                {
                    pKill += KillProcessList.Items[i].ToString() + ",";
                }
                pKill = pKill.Remove(pKill.Length - 1);
                Properties.Settings.Default.ProcessList = pKill;
            }
            else
                Properties.Settings.Default.ProcessList = "";

            Properties.Settings.Default.Save();
        }

        /*
        *  Apply configuration from our config file.
         *  
        */
        public void ApplyConfiguration()
        {
            
            // General settings
            if (Properties.Settings.Default.TrueCryptPath == "")
                checkFileExistance(FilePath);
            else
                FilePath = Properties.Settings.Default.TrueCryptPath;
                

            

            if (Properties.Settings.Default.AuthKey != "")
            {
                AuthorizationKey = Properties.Settings.Default.AuthKey;
                AuthKey.Text = Properties.Settings.Default.AuthKey;
                AuthKey.Enabled = false;
            }

            HTTPListen.Checked = Properties.Settings.Default.HTTPListener;
            UDPListen.Checked = Properties.Settings.Default.UDPListener;            

            UseBroadcast.Checked = Properties.Settings.Default.TransmitToBC;
            Broadcast = Properties.Settings.Default.TransmitToBC;

            Forward.Checked = Properties.Settings.Default.TransmitToHosts;
            Transmit = Properties.Settings.Default.TransmitToHosts;

            Autostart.Checked = Properties.Settings.Default.Autostart;
            AutostartAFT = Properties.Settings.Default.Autostart;

            StartMinimized.Checked = Properties.Settings.Default.StartMinimized;
            MinimizedStartup = Properties.Settings.Default.StartMinimized;

            EnableLogging.Checked = Properties.Settings.Default.Logging;
            LoggingEnabled = Properties.Settings.Default.Logging;

            AutostartDMS.Checked = Properties.Settings.Default.AutostartDMS;
            DMSAutostart = Properties.Settings.Default.AutostartDMS;

            CheckHosts.Checked = Properties.Settings.Default.CheckHosts;
            HostCheck = Properties.Settings.Default.CheckHosts;

            ReceiveConfig.Checked = Properties.Settings.Default.ConfigUpdates;
            ConfUpdate = Properties.Settings.Default.ConfigUpdates;

            RemoteDMS.Checked = Properties.Settings.Default.RemoteDMS;
            EnableRemoteDMS = Properties.Settings.Default.RemoteDMS;

            AllowTesting.Checked = Properties.Settings.Default.Testing;
            Testing = Properties.Settings.Default.Testing;

            EnableBluetooth.Checked = Properties.Settings.Default.EnableBluetooth;
            BluetoothEnabled = Properties.Settings.Default.EnableBluetooth;

            string[] HostList = Properties.Settings.Default.Hosts.Split(',');
            if (HostList.Count() > 0 && Properties.Settings.Default.Hosts != "")
            {
                string local = GetLocalAddress();
                for (int i = 0; i < HostList.Count(); i++)
                {
                    string[] HostOpt = HostList[i].Split(';');
                    if (!HostOpt[1].Equals(local)) // Ignore the own local IP when adding hosts
                    {
                        HostData.Items.Add(new ListViewItem(new[] { " - ", HostOpt[0], HostOpt[1], HostOpt[2], HostOpt[3] }));
                        TestData.Items.Add(new ListViewItem(new[] { HostOpt[0], HostOpt[1], HostOpt[2], "-", "-", "-", "-", "-" }));
                    }
                }
            }

            // USB DMS Settings
            string[] USBDMSList = Properties.Settings.Default.DMSDevices.Split(',');
            if (USBDMSList.Count() > 0 && Properties.Settings.Default.DMSDevices != "")
            {
                for (int i = 0; i < USBDMSList.Count(); i++)
                {
                    string[] DMSOpt = USBDMSList[i].Split(';');
                    USBDMSDeviceList.Items.Add(new ListViewItem(new[] { DMSOpt[0], DMSOpt[1], DMSOpt[2], DMSOpt[3] }));
                    if (Convert.ToBoolean(DMSOpt[0]))
                    {
                        USBDMSDevices.Add(DMSOpt[3]);
                        USBDMSEvents.Add(DMSOpt[1]);
                    }
                }
            }

            // BT DMS Settings
            string[] BTDMSList = Properties.Settings.Default.BTDMSDevices.Split(',');
            if (BTDMSList.Count() > 0 && Properties.Settings.Default.BTDMSDevices != "")
            {
                for (int i = 0; i < BTDMSList.Count(); i++)
                {
                    string[] DMSOpt = BTDMSList[i].Split(';');
                    BTDMSDeviceList.Items.Add(new ListViewItem(new[] { DMSOpt[0], DMSOpt[1], DMSOpt[2], DMSOpt[3] }));
                    if (Convert.ToBoolean(DMSOpt[0]))
                    {
                        BTDMSDevices.Add(DMSOpt[3]);
                        BTDMSEvents.Add(DMSOpt[2]);
                        BTDMSActions.Add(DMSOpt[1]);
                    }
                }
            }



            ShutPC.Checked = Properties.Settings.Default.Shutdown;
            ShutdownPC = Properties.Settings.Default.Shutdown;

            Unmount.Checked = Properties.Settings.Default.Unmount;
            UnmountTC = Properties.Settings.Default.Unmount;

            X.Text = Properties.Settings.Default.MouseMax_X.ToString();
            maximumXmovement = Properties.Settings.Default.MouseMax_X;

            Y.Text = Properties.Settings.Default.MouseMax_Y.ToString();
            maximumYmovement = Properties.Settings.Default.MouseMax_Y;

            ACProtect.Checked = Properties.Settings.Default.ACProtection;
            ACProtection = Properties.Settings.Default.ACProtection;

            USBProtect.Checked = Properties.Settings.Default.USBProtection;
            USBProtection = Properties.Settings.Default.USBProtection;

            NetworkProtect.Checked = Properties.Settings.Default.NetworkProtection;
            NetworkProtection = Properties.Settings.Default.NetworkProtection;

            Screensaver.Checked = Properties.Settings.Default.Screensaver;
            UseScreensaver = Properties.Settings.Default.Screensaver;

            KillProcesses.Checked = Properties.Settings.Default.KillProcesses;
            KillProc = Properties.Settings.Default.KillProcesses;
            
            
            string[] ProcList = Properties.Settings.Default.ProcessList.Split(',');
            if (ProcList.Count() > 0)
            {
                for (int i = 0; i < ProcList.Count(); i++)
                {
                    KillProcessList.Items.Add(ProcList[i]);
                }
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

        private void RemoveDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = USBDMSDeviceList.SelectedIndices[0];

                for (int i = 0; i < USBDMSDevices.Count; i++)
                {
                    if (USBDMSDeviceList.Items[selected].SubItems[3].Text.Equals(USBDMSDevices[i]))
                    {
                        USBDMSDevices.RemoveAt(i);
                        USBDMSEvents.RemoveAt(i);
                        
                    }
                }
                USBDMSDeviceList.Items.RemoveAt(selected);
            }
            catch (Exception)
            {

            }
        }

        private void ActivateDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = USBDMSDeviceList.SelectedIndices[0];
                bool exists = false;

                for(int i = 0; i < USBDMSDevices.Count; i++)
                    if (USBDMSDevices[i].Equals(USBDMSDeviceList.Items[selected].SubItems[3].Text))
                        exists = true;

                if (!exists)
                {
                    USBDMSDevices.Add(USBDMSDeviceList.Items[selected].SubItems[3].Text);
                    USBDMSEvents.Add(USBDMSDeviceList.Items[selected].SubItems[1].Text);

                    USBDMSDeviceList.Items[selected].SubItems[0].Text = "True";
                }
            }
            catch (Exception)
            {

            }
        }

        private void DeactivateDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = USBDMSDeviceList.SelectedIndices[0];
                string device = USBDMSDeviceList.Items[selected].SubItems[3].Text;
                for (int i = 0; i < USBDMSDevices.Count; i++)
                {
                    if (device.Equals(USBDMSDevices[i]))
                    {
                        USBDMSDevices.RemoveAt(i);
                        USBDMSEvents.RemoveAt(i);
                    }
                }
                USBDMSDeviceList.Items[selected].SubItems[0].Text = "False";
            }
            catch (Exception)
            {

            }
        }

        private void X_TextChanged(object sender, EventArgs e)
        {
            if (X.Text != "")
            {
                try
                {
                    maximumXmovement = int.Parse(X.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not read value");
                }
            }
            else
                maximumXmovement = 10;
            
        }

        private void Y_TextChanged(object sender, EventArgs e)
        {
            if (Y.Text != "")
            {
                try
                {
                    maximumYmovement = int.Parse(Y.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not read value");
                }
            }
            else
                maximumYmovement = 10;
        }

        private void EditDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = USBDMSDeviceList.SelectedIndices[0];
                USBDMSName.Text = USBDMSDeviceList.Items[selected].SubItems[2].Text;
                USBDMSDevice.Items.Clear();
                USBDMSDevice.Items.Add(USBDMSDeviceList.Items[selected].SubItems[3].Text);
                USBDMSDevice.SelectedIndex = 0;

                for (int i = 0; i < USBDMSDevices.Count; i++)
                {
                    if (USBDMSDeviceList.Items[selected].SubItems[3].Text.Equals(USBDMSDevices[i]))
                    {
                        USBDMSDevices.RemoveAt(i);
                        USBDMSEvents.RemoveAt(i);
                    }
                }
                USBDMSDeviceList.Items.RemoveAt(selected);
            }
            catch (Exception)
            {

            }
        }

        private void DetectBluetoothDevices_Click(object sender, EventArgs e)
        {
            BTLoading.Visible = true;
            BTDMSDetect.Text = "";
            Thread bluetoothServerThread = new Thread(new ThreadStart(BluetoothDetect));
            bluetoothServerThread.Start();
            BTDMSDevice.Items.Clear();
        }



        public void BluetoothDetect()
        {
            try
            {
                List<Device> devices = new List<Device>();
                BluetoothClient bc = new InTheHand.Net.Sockets.BluetoothClient();
                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                int count = array.Length;
                for (int i = 0; i < count; i++)
                {
                    Device device = new Device(array[i]);
                    
                    this.Invoke((MethodInvoker)delegate
                    {
                        BTDMSDevice.Items.Add(device);
                    });
                }
                this.Invoke((MethodInvoker)delegate
                {
                    BTLoading.Visible = false;
                    BTDMSDetect.Text = count + " devices found!";
                    BTDMSDevice.SelectedIndex = 0;
                });

            }
            catch (Exception)
            {
                if (LoggingEnabled)
                    Log(2, "Something wen't wrong trying to fetch BT devices!");
            }
        }

        private void SaveBTDMS_Click(object sender, EventArgs e)
        {
            string deviceName = "";
            string dmsAction = "";
            string dmsEvent = "";
            if (BTDMSDevice.Items.Count > 0)
            {
                deviceName = BTDMSDevice.SelectedItem.ToString();

                if (BTDMSAction.SelectedIndex != -1 && BTDMSAction.SelectedItem.ToString() != "")
                {
                    dmsAction = BTDMSAction.SelectedItem.ToString();

                    if (BTDMSEvent.SelectedIndex != -1 && BTDMSEvent.SelectedItem.ToString() != "")
                    {
                        dmsEvent = BTDMSEvent.SelectedItem.ToString();
                        BTDMSDeviceList.Items.Add(new ListViewItem(new[] { "False", dmsAction, dmsEvent, deviceName }));
                        if (LoggingEnabled)
                            Log(3, "DMS device saved");
                        BTDMSDevice.Items.Clear();
                    }
                    else
                        MessageBox.Show("You need to specify an event");
                }
                else
                    MessageBox.Show("You need to specify an action");
            }
            else
                MessageBox.Show("No DMS device has been selected");
        }

        public void HandleBluetoothDMS()
        {
            while (BluetoothEnabled)
            {
                if (BTDMSDevices.Count > 0)
                {
                    try
                    {
                        ArrayList InRangeDevices = new ArrayList();
                        List<Device> devices = new List<Device>();
                        BluetoothClient bc = new InTheHand.Net.Sockets.BluetoothClient();
                        BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                        int count = array.Length;
                        for (int i = 0; i < count; i++)
                        {
                            Device device = new Device(array[i]);
                            InRangeDevices.Add(device.DeviceName);
                        }

                        for (int i = 0; i < BTDMSDevices.Count; i++)
                        {
                            if (BTDMSEvents[i].ToString().Equals("Out of range")) // Event out of range condition met
                            {
                                if (!InRangeDevices.Contains(BTDMSDevices[i]))
                                {
                                    if (BTDMSActions[i].ToString().Equals("Enable DMS") && DMSEnabled == false)
                                    {
                                        if (LoggingEnabled)
                                            Log(2, "DMS enabled - bluetooth device not in range");
                                        this.Invoke((MethodInvoker)delegate {
                                            EnableDMS.PerformClick();
                                        });
                                    }
                                    if (BTDMSActions[i].ToString().Equals("Panic"))
                                    {
                                        Panic();
                                    }
                                    if (BTDMSActions[i].ToString().Equals("Lock"))
                                    {
                                        LockWorkStation();
                                        if (LoggingEnabled)
                                            Log(2, "Desktop locked with device");
                                    }
                                }
                            }
                            if (BTDMSEvents[i].ToString().Equals("In range")) // Event in range condition met
                            {
                                if (InRangeDevices.Contains(BTDMSDevices[i]))
                                {
                                    if (BTDMSActions[i].ToString().Equals("Disable DMS") && DMSEnabled == true)
                                    {
                                        BluetoothDisableDMS = true;
                                    }
                                }
                            }
                        }      
                    }
                    catch (Exception)
                    {
                       // if (LoggingEnabled)
                       //     Log(2, "Something wen't wrong trying to fetch BT devices!");
                    }
                }
            }
        }

        private void EnableBluetooth_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableBluetooth.Checked)
            {
                BluetoothEnabled = true;
                BTGroupBox.Enabled = true;
            }
            else
            {
                BluetoothEnabled = false;
                BTGroupBox.Enabled = false;
            }
        }

        private void ActivateBTDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = BTDMSDeviceList.SelectedIndices[0];
                bool exists = false;
                for (int i = 0; i < BTDMSDevices.Count; i++)
                    if (BTDMSDevices[i].Equals(BTDMSDeviceList.Items[selected].SubItems[3].Text))
                        if(BTDMSEvents[i].Equals(BTDMSDeviceList.Items[selected].SubItems[2].Text))
                            exists = true;

                if (!exists)
                {
                    BTDMSDevices.Add(BTDMSDeviceList.Items[selected].SubItems[3].Text);
                    BTDMSActions.Add(BTDMSDeviceList.Items[selected].SubItems[2].Text);
                    BTDMSEvents.Add(BTDMSDeviceList.Items[selected].SubItems[1].Text);

                    BTDMSDeviceList.Items[selected].SubItems[0].Text = "True";
                }
            }
            catch (Exception)
            {

            }
        }

        private void DeactivateBTDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = BTDMSDeviceList.SelectedIndices[0];
                
                for (int i = 0; i < BTDMSDevices.Count; i++)
                {
                    if(BTDMSDevices[i].Equals(BTDMSDeviceList.Items[selected].SubItems[3].Text))
                    {
                        BTDMSDevices.RemoveAt(i);
                        BTDMSEvents.RemoveAt(i);
                        BTDMSActions.RemoveAt(i);
                        BTDMSDeviceList.Items[selected].SubItems[0].Text = "False";
                    }
                }
                
            }
            catch (Exception)
            {

            }
        }

        private void EditBTDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = BTDMSDeviceList.SelectedIndices[0];
                BTDMSDevice.Items.Clear();
                BTDMSDevice.Items.Add(BTDMSDeviceList.Items[selected].SubItems[3].Text);
                BTDMSDevice.SelectedIndex = 0;

                for (int i = 0; i < BTDMSDevices.Count; i++)
                {
                    if (BTDMSDeviceList.Items[selected].SubItems[3].Text.Equals(BTDMSDevices[i]))
                    {
                        BTDMSDevices.RemoveAt(i);
                        BTDMSEvents.RemoveAt(i);
                        BTDMSActions.RemoveAt(i);
                    }
                }
                BTDMSDeviceList.Items.RemoveAt(selected);
            }
            catch (Exception)
            {

            }
        }

        private void RemoveBTDMSDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = BTDMSDeviceList.SelectedIndices[0];
                for (int i = 0; i < BTDMSDevices.Count; i++)
                {
                    if (BTDMSDeviceList.Items[selected].SubItems[3].Text.Equals(BTDMSDevices[i]))
                    {
                        BTDMSDevices.RemoveAt(i);
                        BTDMSEvents.RemoveAt(i);
                        BTDMSActions.RemoveAt(i);
                    }
                }
                BTDMSDeviceList.Items.RemoveAt(selected);
            }
            catch (Exception)
            {

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