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
                Sound.Items.Add(sound);
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
    }
}
