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
using System.Data.SqlClient;

namespace FIXBOX
{
    public partial class QuickSetup : UserControl
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        string co = "", it = "";
        int order = 1, Max=1;
        public QuickSetup()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            DeviceMain DM = new DeviceMain();
            this.Parent.Controls.Add(DM);
            DM.Dock = DockStyle.Fill;
            DM.BringToFront();
            this.Hide();
        }

        private void QuickSetup_Load(object sender, EventArgs e)
        {
            GetData();
            getMax();
            loadpicturebox();
            if (Max == order)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;

            }
        }

        private void GetData() {
            try
            {
                cmd = new SqlCommand("select printer_Company,printers_IType from Printers where printer_Id='" + Device.id + "'", con);
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
            catch (Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void loadpicturebox() {
            try {
                cmd = new SqlCommand("select QSP_QSetup from QuickSetupPrinters where QSP_Company='"+co+"' and QSP_IType='"+it+"' and QSP_Order='"+order+"' and QSP_Printer='"+Device.id+"'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])reader[0];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox_QS.Image = Image.FromStream(ms);
                    
                }
                con.Close();
            }
            catch (Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message); }
        
        
        }

        private void getMax(){
            try {
                cmd = new SqlCommand("select MAX(QSP_Order) from QuickSetupPrinters where QSP_Company='" + co + "' and QSP_IType='" + it + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Max = (int)reader[0];
                    
                }
                con.Close();
            
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        
        
        
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (order != Max)
            {
                order++;
                loadpicturebox();
            }
            else { MessageBox.Show("No more pictures!","Limit reached!",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (order > 1)
            {
                order--;
                loadpicturebox();
            }
            else { MessageBox.Show("No more pictures!", "Limit reached!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}
