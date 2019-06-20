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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "9798") {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;
                textBox1.Clear();
                textBox1.Enabled = false;
                button5.Text = "Lock";
                addCompany1.Enabled = true;
                addPrinter1.Enabled = true;
                addCodeOrMsg1.Enabled = true;
                addQSetup1.Enabled = true;
                addQuestionP1.Enabled = true;
            
            }else{
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button6.Enabled = false;
                textBox1.Clear();
                textBox1.Enabled = true;
                button5.Text = "Unlock";
                addPrinter1.Enabled = false;
                addCompany1.Enabled = false;
                addCodeOrMsg1.Enabled = false;
                addQSetup1.Enabled = false;
                addQuestionP1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 239, 0);
            button1.ForeColor = Color.FromArgb(80, 50, 124);

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;

            button6.BackColor = Color.White;
            button6.ForeColor = Color.Black;
            addPrinter1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.FromArgb(255, 239, 0);
            button2.ForeColor = Color.FromArgb(80, 50, 124); 

            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;

            button6.BackColor = Color.White;
            button6.ForeColor = Color.Black;
            addQSetup1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;

            button2.BackColor =  Color.White;
            button2.ForeColor =  Color.Black;

            button3.BackColor = Color.FromArgb(255, 239, 0);
            button3.ForeColor = Color.FromArgb(80, 50, 124);

            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;

            button6.BackColor = Color.White;
            button6.ForeColor = Color.Black;
            addQuestionP1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.FromArgb(255, 239, 0);
            button4.ForeColor = Color.FromArgb(80, 50, 124); 

            button6.BackColor = Color.White;
            button6.ForeColor = Color.Black;
            addCodeOrMsg1.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;

            button6.BackColor = Color.FromArgb(255, 239, 0);
            button6.ForeColor = Color.FromArgb(80, 50, 124);

            addCompany1.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home H = new Home();
            H.Show();
        }
    }
}
