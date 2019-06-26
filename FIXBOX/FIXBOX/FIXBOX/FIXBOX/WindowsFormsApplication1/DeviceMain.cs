using System;
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
    public partial class DeviceMain : UserControl
    {
        public static byte[] I;
        SqlConnection con = new SqlConnection();
        public DeviceMain()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void DeviceMain_Load(object sender, EventArgs e)
        {
            if (Home.op == 1)
            {
                FillUCP();
            }
        }

        public string getvaluefromCompanys(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from Companys where Company_Id='" + Device.co+ "'", con);
                DataTable dt = new DataTable();
                con.Open();
                Cmd_CI.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    DataRow row = dt.Rows[0];
                    Val = row[Col].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Val;
        }

        private void FillUCP()
        {
            try
            {
                string Id = Device.id;
                string query = " SELECT Printers.printer_model, Companys.Company_name,Printers.printer_Image FROM Printers INNER JOIN Companys ON Printers.printer_Company = Companys.Company_Id where Printers.Printer_Id ='" + Id + "'";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    textBox1.Text = reader[1].ToString();
                    textBox2.Text = reader[0].ToString();
                    byte[] img = (byte[])(reader[2]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        I = img;
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);

                    }
                    con.Close();

                }
                else
                {
                    con.Close();
                    MessageBox.Show("this printer doesn't exist!!");
                    GoToSite(getvaluefromCompanys(con, "company_WebSearch") + Device.Model);
                    this.Hide();
                    
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public static void GoToSite(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        private void FillUCS()
        {
            try
            {
                string Id = Device.id;
                string query = " SELECT Scanners.Scanner_model, Companys.Company_name,Scanners.Scanner_Image FROM Scanners INNER JOIN Companys ON Scanners.Scanner_Company = Companys.Company_Id where Scanners.Scanner_Id ='" + Id + "'";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    textBox1.Text = reader[1].ToString();
                    textBox2.Text = reader[0].ToString();
                    byte[] img = (byte[])(reader[2]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);

                    }
                    con.Close();

                }
                else
                {
                    con.Close();
                    MessageBox.Show("this Scanner doesn't exist!!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Description D = new Description();
            this.Parent.Controls.Add(D);
            D.Dock = DockStyle.Fill;
            D.BringToFront();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuickSetup QS = new QuickSetup();
            this.Parent.Controls.Add(QS);
            QS.Dock = DockStyle.Fill;
            QS.BringToFront();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult Dial = MessageBox.Show("Is the driver installed?", "Important!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (Dial == DialogResult.Yes)
            {
                Questions Q = new Questions();
                this.Parent.Controls.Add(Q);
                Q.Dock = DockStyle.Fill;
                Q.BringToFront();
                this.Hide();

            }
            else if (Dial == DialogResult.No)
            {
                DialogResult DD = MessageBox.Show("Please install the driver!", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (DD == DialogResult.OK) {
                    QuickSetup QQ = new QuickSetup();
                    this.Parent.Controls.Add(QQ);
                    QQ.Dock = DockStyle.Fill;
                    QQ.BringToFront();
                    this.Hide();

                }
                

            }
            
           

        }

        

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            Zoom z = new Zoom();
            this.Controls.Add(z);
            z.BringToFront();

        }

        

    }
}
