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
    public partial class Questions : UserControl
    {
        SqlConnection con = new SqlConnection();
        string co;
        public static string operation;
        public Questions()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void GetData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select printer_Company from Printers where printer_Id='" + Device.id + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                   
                    co = reader[0].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void Questions_Load(object sender, EventArgs e)
        {
            GetData();
           // check if the err n msg is found
            try
            {
                SqlCommand cmd = new SqlCommand("select Company_ErrNMsg from Companys where Company_Id='" + co + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows) {
                    bool ms = (bool)reader[0];
                    if (ms == true)
                    {
                        btnMsgOrCode.Visible = true;
                    }
                    else {
                        btnMsgOrCode.Visible = false;
                    }
                }
                con.Close();
                

            }
            catch (Exception ex) { con.Close(); MessageBox.Show(ex.Message); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DeviceMain DV = new DeviceMain();
            this.Parent.Controls.Add(DV);
            DV.Dock = DockStyle.Fill;
            DV.BringToFront();
            this.Hide();
        }

        private void btnMsgOrCode_Click(object sender, EventArgs e)
        {
            ErrorsNMessages Err = new ErrorsNMessages();
            this.Parent.Controls.Add(Err);
            Err.Dock = DockStyle.Fill;
            Err.BringToFront();

        }

        private void btnPJ_Click(object sender, EventArgs e)
        {
            operation = "PAPERJAM";
            QuestionsHome QH = new QuestionsHome();
            this.Parent.Controls.Add(QH);
            QH.Dock = DockStyle.Fill;
            QH.BringToFront();
            this.Hide();
        }

        private void btnNoAct_Click(object sender, EventArgs e)
        {
            operation = "NETWORK";
            QuestionsHome QH = new QuestionsHome();
            this.Parent.Controls.Add(QH);
            QH.Dock = DockStyle.Fill;
            QH.BringToFront();
            this.Hide();
        }

        private void btnPE_Click(object sender, EventArgs e)
        {
            operation = "PRINTINGERROR";
            QuestionsHome QH = new QuestionsHome();
            this.Parent.Controls.Add(QH);
            QH.Dock = DockStyle.Fill;
            QH.BringToFront();
            this.Hide();
        }
    }
}
