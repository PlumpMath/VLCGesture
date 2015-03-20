using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoHandGesture
{
    public partial class Gestures : Form
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const string PROCESS_NAME = "vlc";

        public Camera _camera;
        public Process WorkingProcess { get; set; }

        public Gestures()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("searching for process..");

            Process[] prc = Process.GetProcessesByName(PROCESS_NAME);

            // attach to process if possible
            if (prc.Length == 0)
            {
                Debug.WriteLine("No process with name " + PROCESS_NAME + " wasn't found. ", "FAILURE");
            }
            else
            {
                this.WorkingProcess = prc.First<Process>();
                Debug.WriteLine("attached to process " + this.WorkingProcess.ProcessName + "!", "SUCCESS");
                this.WorkingProcess.EnableRaisingEvents = true;
                this.WorkingProcess.Exited += WorkingProcess_Exited;

                // Start camera
                this._camera = new Camera(this.onFiredGesture);
                this._camera.Start();
            }
        }

        void WorkingProcess_Exited(object sender, EventArgs e)
        {
            Debug.WriteLine("working process exited!");

            // Stop the streaming
            this._camera.Stop();
            this._camera = null;
        }

        private void onFiredGesture(PXCMHandData.GestureData gestureData)
        {
            if (gestureData.name.CompareTo("spreadfingers") == 0)
            {
                this.BackColor = Color.Red;
            }

            if (gestureData.name.CompareTo("thumb_up") == 0)
            {
                this.BackColor = Color.Green;
            }
        }
    }
}
