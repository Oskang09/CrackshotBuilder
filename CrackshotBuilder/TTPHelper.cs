using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrackshotBuilder
{
    public partial class TTPHelper : Form
    {
        public TTPHelper()
        {
            InitializeComponent();
        }
        public string TTP_TextSet
        {
            set { TTP_Text.Text = value; }
        }
        public string TTP_LabelSet
        {
            set { TTPLabel.Text = value; }
        }
    }
}
