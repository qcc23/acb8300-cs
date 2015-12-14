using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace FakeColourFinder
{
    public class BrightnessMonitor
    {
        private BackgroundWorker _worker = new BackgroundWorker();
        private FCFForm.SetStatusDelegate _setStatus = null;
        private bool _running = true;
        AutoResetEvent _exitEvent = new AutoResetEvent(false);

        public BrightnessMonitor(FCFForm.SetStatusDelegate setStatus)
        {
            _setStatus = setStatus;
            _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
            _worker.RunWorkerAsync();
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_running)
            {
                double brightness = Calibrator.GetBrightness();
                _setStatus(string.Format("Brightness: {0:F1}.", brightness));
            }

            _exitEvent.Set();
        }

        public void Stop()
        {
            _running = false;
            _exitEvent.WaitOne();
        }
    }
}
