﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FIXBOX
{
    public partial class AddErrNMsgSolutions : UserControl
    {
        SqlConnection con = new SqlConnection();

        
        public static string id ;
        string imgLoc ;
        public AddErrNMsgSolutions()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TBOrder.Clear();
            pictureBoxSolution.Image = null;

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



        

        private void btnBrowse_Click(object sender, EventArgs e)
        {
             try {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*,jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                dlg.Title = "Select Solution Picture";
                if (dlg.ShowDialog() == DialogResult.OK) {
                    imgLoc = dlg.FileName.ToString();
                    pictureBoxSolution.ImageLocation = imgLoc;
                }
            
            }
             catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                String query = "insert into ErrNMsSolutions(ErrSols_CodeOMsg,ErrSols_Order,ErrSols_Solution) values('"+id+"','"+TBOrder.Text+"',@img)";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter("@img", img));
                int x = command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + " record(s) saved.");

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        }
    }

