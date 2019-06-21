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
        public static string Model, Co;
        SqlConnection con = new SqlConnection();
        public Device()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        // Get a Value From the data base 
        public string getvaluefromDB(String Col, String SelectedCol, String DBname, SqlConnection con, ComboBox comboBox2)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from " + DBname + " where " + SelectedCol + "='" + comboBox2.SelectedItem.ToString() + "'", con);
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (op != 0 && comboBox1.Text != "Choose the Device Company..." && (textBox1.Text != " " || textBox1.Text != null))
            {
                Model = textBox1.Text;
                Co = comboBox1.Text;
            }
            else if (op == 0)
            {
                MessageBox.Show("Please choose a Device from the left side!", "ERROR!!");
            }
            else
            {
                MessageBox.Show("Company or Model is not set!", "ERROR!!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {



            DeviceMain DM = new DeviceMain();
            
            this.Parent.Controls.Add(DM);
            DM.Dock = DockStyle.Fill;
            DM.BringToFront();
            this.Hide();
        }
    }
}
