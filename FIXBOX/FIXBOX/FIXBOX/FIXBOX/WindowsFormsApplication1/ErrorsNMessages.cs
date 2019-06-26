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
    public partial class ErrorsNMessages : UserControl
    {
        SqlConnection con = new SqlConnection();
        string Code,co,it;
        int order = 1, max=1;
        public ErrorsNMessages()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }


        private void GetDeviceData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select printer_Company,printers_IType from Printers where printer_Id='" + Device.id + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    co = reader[0].ToString();
                    it = reader[1].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCode() {

            try
            {
                string cc = tbErr.Text;
                SqlCommand cmd = new SqlCommand("select PENM_Id from PrintersErrNMsg where PENM_Company='"+co+"' and PENM_IType='"+it+"' and PENM_CodeOrMsg='"+cc.ToUpper()+"'",con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Code = reader[0].ToString();
                }
                con.Close();
            }
            catch (Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message);
            
            }
        
        
        
        }

        private void loadpicturebox()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select ErrSols_Solution from ErrNMsSolutions where ErrSols_CodeOMsg='" + Code + "' and ErrSols_Order='"+order+"'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])reader[0];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }


        }

        private void getMax()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select MAX(ErrSols_Order) from ErrNMsSolutions where ErrSols_CodeOMsg='" + Code + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    max = (int)reader[0];

                }
                con.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            GetDeviceData();
            GetCode();
            getMax();
            loadpicturebox();
            if (max == order) {
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
            
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (order != max)
            {
                order++;
                loadpicturebox();
            }
            else { MessageBox.Show("No more pictures!", "Limit reached!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

       

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (order > 1)
            {
                order--;
                loadpicturebox();
            }
            else { MessageBox.Show("No more pictures!", "Limit reached!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void ErrorsNMessages_Load(object sender, EventArgs e)
        {
            
        }
    }
}
