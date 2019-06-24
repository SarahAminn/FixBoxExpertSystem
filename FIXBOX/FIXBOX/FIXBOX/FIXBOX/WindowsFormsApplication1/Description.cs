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

namespace FIXBOX
{
    public partial class Description : UserControl
    {
        SqlConnection con = new SqlConnection();
        public Description()
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

        private void Description_Load(object sender, EventArgs e)
        {
            if (Home.op == 1)
            {
                try
                {
                    SqlDataAdapter Cmd = new SqlDataAdapter("Select printer_Desc from Printers where printer_Id='" + Device.id + "'", con);
                    con.Open();
                    DataTable dt = new DataTable();
                    Cmd.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        DataRow row = dt.Rows[0];
                        string Decs = row["printer_Desc"].ToString();
                        richTextBox1.Text = Decs;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
