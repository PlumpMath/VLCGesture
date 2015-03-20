using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoHandGesture
{
    public partial class Gestures : Form
    {
        public Camera _camera;
        public Process WorkingProcess { get; set; }

        public Gestures()
        {
            InitializeComponent();

            this._camera = new Camera(this.onFiredGesture);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("searching for process..");

            List<Process> st = Process.GetProcesses().ToList();
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
