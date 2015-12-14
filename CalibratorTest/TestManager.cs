using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

namespace FakeColourFinder
{
    class TestManager
    {
        public delegate void TestCompleteDelegate();
        public event TestCompleteDelegate TestCompleteEvent;

        private readonly int[] _greyLevels = { 0, 25, 51, 76, 102, 127, 153, 178, 204, 229, 255 };

        private double _gamma = 2.2; // The gamma curve to calibrate to
        private FCFForm.SetStatusDelegate _setStatus = null;
        private BackgroundWorker _worker = new BackgroundWorker();
        private Control _testArea = null;

        public TestManager(Control testArea, FCFForm.SetStatusDelegate setStatus)
        {
            _testArea = testArea;
            _setStatus = setStatus;

            _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);
        }

        public void StartTest()
        {
            _worker.RunWorkerAsync(_testArea);
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("The test has completed.", "Finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (TestCompleteEvent != null)
            {
                TestCompleteEvent();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ResultSet resultSet = new ResultSet();

            // Calibrate the brightness.
            SetTestLevel(255);
            double maxBrightness = Calibrator.GetBrightness();

            _setStatus(string.Format("Peak brightness: {0:F1}.", maxBrightness));

            // Measure every step in the array
            foreach (int level in _greyLevels)
            {
                ResultItem result = resultSet.AddResult(level);

                // Set the background and grab the reading.
                SetTestLevel(level);
                SensorReading reading = Calibrator.GetReading(maxBrightness);

                // Set the result target levels.
                result.Target.RGBY = level;

                // Set the result actual levels.
                result.Actual.R = reading.RGB.R;
                result.Actual.G = reading.RGB.G;
                result.Actual.B = reading.RGB.B;
                result.Actual.Y = Math.Pow(reading.Yyx.Y / 100.0, 1.0/_gamma) * 255;  // Gamma adjust actual reading to 0-255 linear

                // Set the statusbar
                _setStatus(string.Format("Previous reading: Y={0:F2} R={1:F2} G={2:F2} B={3:F2}", result.Actual.Y, result.Actual.R, result.Actual.G, result.Actual.B));
            }
            
            string saveError = "";
            if (!ResultSet.Save(resultSet, out saveError))
            {
                MessageBox.Show(saveError, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetTestLevel(int level)
        {
            _testArea.BackColor = Color.FromArgb(level, level, level);
            Thread.Sleep(500); // Give the sensor some time to react.
        }
    }
}
