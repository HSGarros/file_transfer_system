using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Show : Form
    {
        public Show(string filePath)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Image.FromFile(filePath);
            this.Size = new Size(1200, 900);
            this.Controls.Add(pictureBox);
        }

        private void Show_Load(object sender, EventArgs e)
        {

        }
    }
}
