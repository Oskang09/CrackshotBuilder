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
using System.Media;

namespace CrackshotBuilder
{
    public partial class Sounds : Form
    {

        private Form1 frm;
        public Sounds(Form1 fm)
        {
            frm = fm;
            InitializeComponent();
        }
        private void LoadSound()
        {
            string[] lang = File.ReadAllLines(frm.filepath + "/Resource/lang/sounds.yml");
            Soundlabel.Text = lang[0].Split(':').LastOrDefault();
            Volumelabel.Text = lang[1].Split(':').LastOrDefault();
            Pitchlabel.Text = lang[2].Split(':').LastOrDefault();
            Delaylabel.Text = lang[3].Split(':').LastOrDefault();
            ASlabel.Text = lang[4].Split(':').LastOrDefault();
            string[] sounds = File.ReadAllLines(frm.filepath + "/sound.txt");
            foreach (string sound in sounds)
            {
                Sound.Items.Add(sound.Split('=').First());
            }
        }
        private void AddSound_Click(object sender, EventArgs e)
        {
            string soundstr = Sound.Text;
            soundstr += "-" + Volume.Text;
            soundstr += "-" + Pitch.Text;
            soundstr += "-" + Delay.Text;
            frm.AddSound(this.Text, soundstr);
            this.Close();
        }

        private void Sounds_Load(object sender, EventArgs e)
        {
            LoadSound();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Sound.Text != "")
            {
                string[] text = { };
                string[] sounds = File.ReadAllLines(frm.filepath + "/sound.txt");
                foreach (string sound in sounds)
                {
                    if (sound.Contains(Sound.Text))
                    {
                        text = sound.Split('=');
                    }
                }
                int num;
                string lastnum = "";
                string soundname = text[1].Replace("/", "\\") + ".wav";
                int.TryParse(text[1].Substring(0, text[1].Length - 1), out num);
                if (num > 2)
                {
                    soundname = text[1].Substring(0, text[1].Length - 1);
                    Random rnd = new Random();
                    lastnum = rnd.Next(num).ToString();
                }
                string soundfile = Application.StartupPath + "/CrackshotFiles/soundpack";
                if (!File.Exists(soundfile + "/" + soundname + lastnum))
                {
                    MessageBox.Show("You didn't download soundpack yet!\nDownload at Github!","SoundPack");
                }
                else
                {
                    SoundPlayer sp = new SoundPlayer(soundfile + "/" + soundname + lastnum);
                    sp.PlaySync();
                    sp.Dispose();
                }
            }
        }
    }
}
