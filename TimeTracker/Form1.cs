using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTracker {
    public partial class Form1 : Form {
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        private DateTime Start = DateTime.MinValue;
        private DateTime End = DateTime.MinValue;
        private string OutputFile = "";

        public Form1() {
            InitializeComponent();

            // Read types.
            string[] types = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "types.txt"));
            TypeCombo.Items.AddRange(types);

            // Read settings.
            string[] settings = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "settings.txt"));
            foreach (string s in settings) {
                string[] keyval = s.Split('=');
                if (keyval[0] == "output")
                    this.OutputFile = keyval[1];
            }
            
            Register();
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0x0312) {
                Unregister();

                this.Start = DateTime.Now;
                this.Text = "New interruption - " + this.Start.ToShortTimeString();
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.TopMost = true;
                this.TopMost = false;
            }
            base.WndProc(ref m);
        }

        private void Register() {
            try {
                // Alt + Win + I
                RegisterHotKey(this.Handle, HashCode(), 9, (int)'I');
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                Application.Exit();
                Environment.Exit(1);
            }
        }
        private void Unregister() {
            try {
                UnregisterHotKey(this.Handle, HashCode());
            }
            catch { }
        }

        private void MyDispose() {
            UnregisterHotKey(this.Handle, HashCode());
            GC.SuppressFinalize(this);
        }

        ~Form1() {
            UnregisterHotKey(this.Handle, HashCode());
        }

        private int HashCode() {
            return 9 ^ (int)'I' ^ this.Handle.ToInt32();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            this.Visible = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to quit?", "Time Tracker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                notifyIcon1.Dispose();
                Application.Exit();
                Environment.Exit(1);
            }
        }

        private void OKButton_Click(object sender, EventArgs e) {
            this.End = DateTime.Now;

            List<string> events = new List<string>();
            events.Add(this.Start.ToString() + "," + this.End.ToString() + "," + TypeCombo.Text + ",\"" + NotesText.Text + "\"");
            File.AppendAllLines(this.OutputFile.Replace("%date%", this.End.ToString("yyyy-MM-dd")), events);

            this.Visible = false;
            this.ShowInTaskbar = false;
            //NotesText.Text = "";
            Register();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            this.Visible = false;
            this.ShowInTaskbar = false;
            //NotesText.Text = "";
            Register();
        }
    }
}
