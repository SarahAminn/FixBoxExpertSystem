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
    public partial class AddCompany : UserControl
    {
        SqlConnection con = new SqlConnection();

        public AddCompany()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            LoadTable();
        }

        // Loads Data Grid Views
        public void LoadTable()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("select * from Companys", con);
                con.Open();
                SqlDataReader DR = Cmd.ExecuteReader();
                BindingSource source = new BindingSource();
                source.DataSource = DR;
                dataGridView1.DataSource = source;
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_Del = new SqlCommand("delete from Companys where Company_name='" + textBox1.Text + "'", con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd_Del.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_insert = new SqlCommand("insert into Companys(Company_name) values('"+textBox1.Text+"')", con);
                if(con.State!=ConnectionState.Open)
                    con.Open();
                cmd_insert.ExecuteNonQuery();
                con.Close();

            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
