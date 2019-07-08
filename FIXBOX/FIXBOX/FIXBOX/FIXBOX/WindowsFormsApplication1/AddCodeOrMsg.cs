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
    public partial class AddCodeOrMsg : UserControl
    {
        SqlConnection con = new SqlConnection();
        
        public AddCodeOrMsg()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            loadComboBox("select Company_name from Companys", cbCo);
            loadComboBox("select PENM_CodeOrMsg from PrintersErrNMsg", cbDelete);
             LoadTable(dataGridView1);
        }


        // Loads Comboboxes
        public void loadComboBox(string query, ComboBox combo)
        {
            combo.Items.Clear();
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
        


        // Loads Data Grid Views
        public void LoadTable(DataGridView dataGridView1)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("select * from PrintersErrNMsg", con);
                con.Open();
                SqlDataReader DR = Cmd.ExecuteReader();
                if (DR.HasRows)
                {
                    BindingSource source = new BindingSource();
                    source.DataSource = DR;
                    dataGridView1.DataSource = source;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbIT.Clear();
            tbCodeMsg.Clear();
            cbCo.Text = null;
            cbDelete.Text = null;
            
          
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_Del = new SqlCommand("delete from PrintersErrNMsg where PENM_CodeOrMsg='" + cbDelete.SelectedItem.ToString() + "'", con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd_Del.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                
                String query = "insert into PrintersErrNMsg(PENM_CodeOrMsg,PENM_Company,PENM_IType) values('" + tbCodeMsg.Text + "','" + getvaluefromDB("Company_Id", "Company_Name", "Companys", con, cbCo) + "','"+tbIT.Text+"')";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand command = new SqlCommand(query, con);
                
                int x = command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + " record(s) saved.");

                SqlCommand cmdgetid = new SqlCommand("select PENM_Id from PrintersErrNMsg where PENM_CodeOrMsg='" + tbCodeMsg.Text + "' and PENM_Company='" + getvaluefromDB("Company_Id", "Company_Name", "Companys", con, cbCo) + "' and PENM_IType='" + tbIT.Text + "' ", con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlDataReader read = cmdgetid.ExecuteReader();
                
                read.Read();
                if (read.HasRows) {
                    AddErrNMsgSolutions.id = read[0].ToString();
                }
                con.Close();
            
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAddSol_Click(object sender, EventArgs e)
        {
            AddErrNMsgSolutions SS = new AddErrNMsgSolutions();
            this.Controls.Add(SS);
            SS.BringToFront();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cbDelete.Items.Clear();
            LoadTable(dataGridView1);
            loadComboBox("select PENM_CodeOrMsg from PrintersErrNMsg", cbDelete);
        }
    }
}
