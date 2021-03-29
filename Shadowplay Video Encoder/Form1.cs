using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Shadowplay_Video_Encoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ffmpeg  -i "D:\Video\source_file.mp4" -bsf:v h264_metadata=video_full_range_flag=1 -c:v copy -c:a copy "D:\Video\destination_file.mp4"

        string file_dest, file_name, directory, cmdText, endfile;
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "mp4",
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_dest = openFileDialog1.FileName;
                file_name = openFileDialog1.SafeFileName;
                directory = Path.GetDirectoryName(file_dest) + "\\";
                label1.Text = "Chosen File: " + file_dest;
                label1.Visible = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            endfile = "remastered_"+ file_name;
            cmdText = "ffmpeg  -i \"" + file_dest + "\" -bsf:v h264_metadata=video_full_range_flag=1 -c:v copy -c:a copy \"" + directory + endfile + "\"";

            Process p = new Process();
            p.StartInfo.WorkingDirectory = directory;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c" + cmdText;
            p.Start();
            MessageBox.Show("Task Completed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            p.WaitForExit();

            /*var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;
            
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.WorkingDirectory = directory;
            //proc1.Verb = "runas";
            proc1.Arguments = "/k " + cmdText;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);*/
        }
    }
}
