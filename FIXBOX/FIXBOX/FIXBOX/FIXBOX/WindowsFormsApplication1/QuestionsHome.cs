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
    public partial class QuestionsHome : UserControl
    {
        SqlConnection con = new SqlConnection();
        public static string co, it;
        int order = 1, max=1;
        public QuestionsHome()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void getMax()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select MAX(QPrinters_Order) from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_Type='"+Questions.operation+"'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    max = (int)reader[0];

                }
                con.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }

        private void GetData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select printers_IType from Printers where printer_Id='" + Device.id + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    
                    it = reader[0].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public string getvaluefromQuestions(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+Col+" from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_Type='" + Questions.operation + "'", con);
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

        private void QuestionsHome_Load(object sender, EventArgs e)
        {
            loadComboBox("select ", comboBox1);
        }
    }
}
