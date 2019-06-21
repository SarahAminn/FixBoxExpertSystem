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
        SqlConnection con = new SqlConnection();
        public DeviceMain()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void DeviceMain_Load(object sender, EventArgs e)
        {
            FillUCP();
        }

        private void FillUCP() {
            try
            {
                string Id = Device.id;
                string query = " SELECT Printers.printer_model, Companys.Company_name,Printers.printer_Image FROM Printers INNER JOIN Companys ON Printers.printer_Company = Companys.Company_Id where Printers.Printer_Id ='"+Id+"'";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows) {
                    textBox1.Text = reader[1].ToString();
                    textBox2.Text = reader[0].ToString();
                    byte[] img = (byte[])(reader[2]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    
                    }
                    con.Close();
                
                } else { 
                    con.Close();
                    MessageBox.Show("this printer doesn't exist!!"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
