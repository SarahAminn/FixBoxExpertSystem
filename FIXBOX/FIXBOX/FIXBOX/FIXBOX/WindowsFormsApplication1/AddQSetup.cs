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
    public partial class AddQSetup : UserControl
    {
        SqlConnection con = new SqlConnection();
        string imgLoc = "";
        public AddQSetup()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            loadComboBox("select Company_name from Companys", cbCo);
            loadComboBox("select QSP_Id from QuickSetupPrinters", cbDelete);
            //LoadTable(dataGridView1);
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

        // Get a Value From the data base 
        public string getvaluefromDB(String Col, String SelectedCol, String DBname, SqlConnection con, ComboBox comboBox2)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select '" + Col + "' from '" + DBname + "' where '" + SelectedCol + "'='" + comboBox2.SelectedItem.ToString() + "'", con);
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

        // Loads Data Grid Views
        public void LoadTable(DataGridView dataGridView1)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("select * from QuickSetupPrinters", con);
                con.Open();
                SqlDataReader DR = Cmd.ExecuteReader();
                BindingSource source = new BindingSource();
                source.DataSource = DR;
                dataGridView1.DataSource = source;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*,jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                dlg.Title = "Select Printer Picture";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    pictureBox_QS.ImageLocation = imgLoc;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbIType.Clear();
            tbOrder.Clear();
            cbCo.ResetText();
            imgLoc = null;
            pictureBox_QS.Image = null;
            pictureBox_QS.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_Del = new SqlCommand("delete from QuickSetupPrinters where QSP_Id='" + cbDelete.SelectedItem.ToString() + "'", con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd_Del.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                String query = "insert into QuickSetupPrinters(QSP_IType,QSP_Order,QSP_Company,QSP_QSetup) values('"+tbIType.Text+"',"+tbOrder.Text+",'"+getvaluefromDB("Company_Id","Company_name","FIXBOX",con,cbCo)+"',@img)";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter("@img", img));
                int x = command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + " record(s) saved.");


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
