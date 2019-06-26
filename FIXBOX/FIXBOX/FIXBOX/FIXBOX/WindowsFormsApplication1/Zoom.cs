using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FIXBOX
{
    public partial class Zoom : UserControl
    {
        public Zoom()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Zoom_Load(object sender, EventArgs e)
        {
           byte[] img= DeviceMain.I;
           if (img == null)
           {
               pictureBox1.Image = null;

           }
           else {
               MemoryStream ms = new MemoryStream(img);
               pictureBox1.Image = Image.FromStream(ms);
           
           }
        }
    }
}
