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
    public partial class AddQuestionP : UserControl
    {
        SqlConnection con = new SqlConnection();
        
        public AddQuestionP()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            loadComboBox("select QPrinters_Id from QuestionsPrinters", cbDelete);
            LoadTable(dataGridView1);
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
        public string getvaluefromDB( SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+Col+" from QuestionsPrinters where QPrinters_Question='" + tbQuestion.Text + "'and QPrinters_Type='" + tbType.Text + "'and QPrinters_Order='" + tbOrder.Text + "'and QPrinters_IType='" + tbIT.Text + "'and QPrinters_QType='" + tbQType.Text + "'", con);
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
                SqlCommand Cmd = new SqlCommand("select * from QuestionsPrinters", con);
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


        private void AddQuestionP_Load(object sender, EventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if(tbOrder.Enabled==false){
            tbOrder.Enabled = true;
            btnOrder.Text = "Lock";
            }else{
                tbOrder.Enabled = false;
                btnOrder.Text = "Unlock";
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            AddChoice AC = new AddChoice();
            this.Parent.Controls.Add(AC);
            AC.Dock = DockStyle.Fill;
            AC.BringToFront();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbIT.Clear();
            tbOrder.Clear();
            tbQuestion.Clear();
            tbType.Clear();
            tbQType.Clear();
            
            cbDelete.ResetText();
           
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_Del = new SqlCommand("delete from QuestionsPrinters where QPrinters_Id='" + cbDelete.SelectedItem.ToString() + "'", con);
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

                String query = "insert into QuestionsPrinters(QPrinters_Question,QPrinters_Type,QPrinters_Order,QPrinters_IType,QPrinters_QType) values('" + tbQuestion.Text + "','" + tbType.Text + "','" + tbOrder.Text + "','" + tbIT.Text + "','" + tbQType.Text + "')";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand command = new SqlCommand(query, con);
                
                int x = command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + " record(s) saved.");
                

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTable(dataGridView1);
        }
    }
}
