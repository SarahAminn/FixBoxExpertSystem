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
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
