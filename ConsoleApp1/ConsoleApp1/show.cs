using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    internal class show
    {
    }
}
class MainForm
{
    public MainForm(string filePath)
    {
        // 获取文件扩展名
        public Show(string filePath)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Image.FromFile(filePath);
            this.Size = new Size(1200, 900);

            this.Controls.Add(pictureBox);
        }


    }
}