using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace FakeColourFinder
{
    public partial class FCFForm : Form
    {
        const string MonitorBrightness = "Brightness";
        const string MonitorStop = "Stop";

        public delegate void SetStatusDelegate(string text);

        private TestManager _test = null;
        private BrightnessMonitor _monitor = null;
        private Results _results = new Results();

        public FCFForm()
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Calibrator.Initialise())
            {
                MessageBox.Show("Could not connect to LG sensor.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void startCalibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_test != null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Place the sensor over the center of the window, and click OK when ready to begin.", "Calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result != DialogResult.OK)
            {
                return;
            }
            
            SetStatus("Starting...");
            Thread.Sleep(500);

            // Disable the calibrate and monitor menu items
            brightnessToolStripMenuItem.Enabled = false;
            startCalibrationToolStripMenuItem.Enabled = false;

            _test = new TestManager(_backdrop, new SetStatusDelegate(SetStatus));
            _test.TestCompleteEvent += new TestManager.TestCompleteDelegate(_test_TestCompleteEvent);
            _test.StartTest();
        }

        void _test_TestCompleteEvent()
        {
            _test = null;
            Reset();

            // Re-enable the calibrate and monitor menu items
            brightnessToolStripMenuItem.Enabled = true;
            startCalibrationToolStripMenuItem.Enabled = true;

            _results.ListResults();
        }

        private void viewResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _results.ListResults();
            _results.Show();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_monitor != null)
            {
                brightnessToolStripMenuItem.Text = MonitorBrightness;

                _monitor.Stop();
                _monitor = null;
                

                Reset();
                calibrationToolStripMenuItem.Enabled = true;
            }
            else
            {
                calibrationToolStripMenuItem.Enabled = false;

                brightnessToolStripMenuItem.Text = MonitorStop;

                SetStatus("Starting...");
                _backdrop.BackColor = Color.FromArgb(255, 255, 255);
                _monitor = new BrightnessMonitor(new SetStatusDelegate(SetStatus));
            }
        }

        private void Reset()
        {
            SetStatus("");
            _backdrop.BackColor = Color.FromArgb(0, 0, 0);
        }

        private void SetStatus(string text)
        {
            _statusLabel.Text = text;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

