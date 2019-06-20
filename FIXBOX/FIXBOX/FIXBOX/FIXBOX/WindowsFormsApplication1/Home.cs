using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIXBOX
{
    public partial class Home : Form
    {
        
        public static int op=0;
        public Home()
        {
            InitializeComponent();
            
            
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Home_Load(object sender, EventArgs e)
        {
           
        }

        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            op = 1;
            button2.ForeColor = Color.FromArgb(80,50,124);
            
            button3.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(255, 239, 0);
            
            button3.BackColor = Color.DimGray;
            Device D = new Device();
            D.Dock = DockStyle.Fill;
            this.Controls.Add(D);
            D.BringToFront();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            op = 2;
            button3.ForeColor = Color.FromArgb(80, 50, 124);
            
            button2.ForeColor = Color.White;
            button2.BackColor = Color.DimGray;
            
            button3.BackColor = Color.FromArgb(255, 239, 0);
            Device D = new Device();
            D.Dock = DockStyle.Fill;
            this.Controls.Add(D);
            D.BringToFront();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
           About aa = new About();
            aa.ShowDialog();
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin A = new Admin();
            A.Show();
        }

        
    }
}
