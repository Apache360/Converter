using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter_1._0
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxFileStructure.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void radioButtonWAV_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxFileStructure.Image = Properties.Resources.WAV_file_structure;
            Width = pictureBoxLogo.Width+Properties.Resources.WAV_file_structure.Width+50;
            Height = Properties.Resources.WAV_file_structure.Height + 60;
        }

        private void radioButtonAIFF_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxFileStructure.Image = Properties.Resources.AIFF_file_structure;
            Width = pictureBoxLogo.Width + Properties.Resources.AIFF_file_structure.Width + 50;
            Height = Properties.Resources.AIFF_file_structure.Height + 60;
        }
    }
}
