using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Net;

namespace Updater
{
    public partial class Form1 : Form
    {
        public string downloadedFile = Application.StartupPath + "/Update/update.zip";
        public string updatePath = Application.StartupPath;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void UpdateSoftware(string version)
        {
            WebClient webreq = new WebClient();
            webreq.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webreq_DLPC);
            webreq.DownloadFileAsync(new Uri("https://github.com/Oskang09/CrackshotBuilder/releases/download/" + version + "/update.zip"), downloadedFile);
            title.Text = "Downloading ...";
            webreq.DownloadFileCompleted += delegate
            {
                S_U_Progress.Value = 100;
                S_U_ProgressNum.Text = "Download Completed!";
                // Web Req Version , Update Features , File Need Del
                WebClient req = new WebClient();
                string newver = req.DownloadString("http://pastebin.com/raw/ddUGMbJy");
                string[] array = newver.Split('\n');
                int from = 0;
                int to = 0;
                foreach (string line in array)
                {
                    if (line.Contains("[FILE]"))
                    {
                        from = GetIndexOfArray(line, array);
                    }
                    if (line.Contains("[UPDATE]"))
                    {
                        to = GetIndexOfArray(line, array);
                    }
                }
                from++;
                to++;
                for (int j = from; j < to; j++)
                {
                    string pathe = array[j];
                    string path = System.Text.RegularExpressions.Regex.Replace(array[j], @"\t|\n|\r", "");
                    File.Delete(Path.Combine(Application.StartupPath + path));
                }
                for (int i = to; i < array.Count(); i++)
                {
                    string update = System.Text.RegularExpressions.Regex.Replace(array[i], @"\t|\n|\r", "");
                    updateTxt.AppendText(update + "\n");
                }
                // 
                unZipUpdateFile();
            };
        }
        void webreq_DLPC(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double byteIn = double.Parse(e.BytesReceived.ToString());
                double totalbyte = double.Parse(e.TotalBytesToReceive.ToString());
                double percentag = byteIn / totalbyte * 100;
                S_U_Progress.Value = int.Parse(Math.Truncate(percentag).ToString());
                S_U_ProgressNum.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive + "...";
            });
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WebClient req = new WebClient();
            string newver = req.DownloadString("http://pastebin.com/raw/ddUGMbJy");
            string[] array = newver.Split('\n');
            string version = System.Text.RegularExpressions.Regex.Replace(array[0], @"\t|\n|\r", "");
            UpdateSoftware(version);
        }
        public int GetIndexOfArray(string Element, string[] Array)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                if (Element == Array[i])
                {
                    return i;
                }
            }
            return -1;
        }
        void unZipUpdateFile()
        {
            using (ZipArchive zip = ZipFile.OpenRead(downloadedFile))
            {
                int totalfile = zip.Entries.Count;
                int extracted = 0;
                title.Text = "Installing ...";
                foreach (ZipArchiveEntry files in zip.Entries)
                {
                    string comFile = Path.Combine(updatePath ,files.FullName);
                    if (files.Name == "")
                    {
                        if (!Directory.Exists(comFile))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(comFile));
                        }
                    }
                    if (files.Name != "")
                    {
                        files.ExtractToFile(comFile, true);
                    }
                    // Progressing
                    extracted++;
                    double percentag = extracted / totalfile * 100;
                    S_U_Progress.Value = int.Parse(Math.Truncate(percentag).ToString());
                    S_U_ProgressNum.Text = "Installed " + extracted.ToString() + " of " + totalfile.ToString() + "...";
                    // End 
                }
                title.Text = "Update Completed.";
                MessageBox.Show("Update Completed.");
            }
        }
    }
}
