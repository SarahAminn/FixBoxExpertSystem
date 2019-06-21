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
        SqlConnection con = new SqlConnection();
        public Device()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        // Get a Value From the data base 
        public string getvaluefromDB()
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

                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select printer_Id from Printers where printer_Company='"+Coid+"'and printer_model='"+textBox1.Text+"'", con);
                DataTable dt = new DataTable();
                con.Open();
                Cmd_CI.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    DataRow row = dt.Rows[0];
                    Val = row["printer_Id"].ToString();
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
            try
            {
                id = getvaluefromDB();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message+"Printer is not in the system"); }
            if (id != null)
            {
                DeviceMain DM = new DeviceMain();

                this.Parent.Controls.Add(DM);
                DM.Dock = DockStyle.Fill;
                DM.BringToFront();
                this.Hide();
            }
        }
    }
}
