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
    public partial class Device : UserControl
    {
        
        int op = Home.op;
        public static string id=null;
        public static string co, Model;
        SqlConnection con = new SqlConnection();
        public Device()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        // Get a Value From the data base 
        public string getvaluefromDB(String WantedCol, String TableName, String CompanyCol, String ModelCol)
        {

            string Val = " ";
            try
            {
                string Coid = "";
                SqlDataAdapter Cmd_C = new SqlDataAdapter("select Company_Id from Companys where Company_name='"+comboBox1.SelectedItem.ToString()+"'", con);
                DataTable dt2 = new DataTable();
                con.Open();
                Cmd_C.Fill(dt2);

                if (dt2.Rows.Count == 1)
                {

                    DataRow row = dt2.Rows[0];
                    Coid = row["Company_Id"].ToString();
                }
                con.Close();
                string Mod = textBox1.Text;
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+WantedCol+" from "+TableName+" where "+CompanyCol+"='"+Coid+"'and "+ModelCol+"='"+Mod.ToUpper()+"'", con);
                DataTable dt = new DataTable();
                con.Open();
                Cmd_CI.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    DataRow row = dt.Rows[0];
                    Val = row[WantedCol].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Val;
        }
        

        private void Device_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Choose the Device Company...";
            loadComboBox("select Company_name from Companys", comboBox1);
            textBox1.Enabled = false;
        }

        public string getvaluefromCompanys(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from Companys where Company_name='" + comboBox1.SelectedItem.ToString() + "'", con);
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

        // Loads Comboboxes
        public void loadComboBox(string query, ComboBox combo)
        {
            try
            {
                SqlCommand cmd_InsertIntoCombo = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd_InsertIntoCombo);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    combo.Items.Add(ds.Tables[0].Rows[i][0]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (Home.op == 1)
                {
                    
                        id = getvaluefromDB("printer_Id", "Printers", "printer_Company", "printer_model");
                        co = getvaluefromCompanys(con, "Company_Id");
                        string Mod = textBox1.Text;
                        Model = Mod.ToUpper();
                       
                    
                }
            
            if (id != null)
            {
                DeviceMain DM = new DeviceMain();

                this.Parent.Controls.Add(DM);
                DM.Dock = DockStyle.Fill;
                DM.BringToFront();
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }
    }
}
