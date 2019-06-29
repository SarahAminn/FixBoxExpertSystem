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
        public static string type, it,cho,co,chSol ;
        int order = 1, max=1;
        DataTable datat;
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
                SqlCommand cmd = new SqlCommand("select MAX(QPrinters_Order) from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='"+Questions.operation+"'and QPrinters_Type='"+type+"'", con);
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
                SqlCommand cmd = new SqlCommand("select printers_IType,printer_Type,printer_Company from Printers where printer_Id='" + Device.id + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    
                    it = reader[0].ToString();
                    type = reader[1].ToString();
                    co = reader[2].ToString();
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
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+Col+" from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='"+order.ToString()+"' and QPrinters_Type='"+type+"'", con);
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
        public string getvaluefromQuestions(SqlConnection con, String Col,String choice)
        {
           
            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='" + order.ToString() + "' and QPrinters_Type='" + type + "' and QPrinters_ConCh='"+choice+"'", con);
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

        private void loadrichtextbox()
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand("select QPrinters_Question from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='" + order.ToString() + "' and QPrinters_Type='" + type + "''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    richTextBox1.Text = reader[0].ToString();

                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }


        }

        private void loadrichtextbox(String Choice)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand("select QPrinters_Question from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='" + order.ToString() + "' and QPrinters_Type='" + type + "' and QPrinters_ConCh='" + Choice + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    richTextBox1.Text = reader[0].ToString();

                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }


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
            GetData();
            getMax();
            loadComboBox("select choice_ch from Choices where choice_Question='"+getvaluefromQuestions(con,"QPrinters_Id")+"' ", comboBox1);
            loadrichtextbox();
        }

        public string getvaluefromchoices(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from Choices where choice_Question='"+getvaluefromQuestions(con,"QPrinters_Id")+"' and choice_ch='"+comboBox1.SelectedItem.ToString()+"'", con);
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

        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                datat = new DataTable();
                SqlDataAdapter cmd_sol = new SqlDataAdapter("select CHSol_Id from ChoiceSolutions where CHSol_Choice ='" + cho + "'", con);
                con.Open();
                cmd_sol.Fill(datat);
                con.Close();
                if (datat.Rows.Count > 0) {
                    QuestionsSol QSol = new QuestionsSol();
                    this.Parent.Controls.Add(QSol);
                    QSol.BringToFront();
                    QSol.Dock = DockStyle.Fill;
                    this.Hide();
                
                }else if(datat.Rows.Count == 0){
                    
                    loadComboBox("select choice_ch from Choices where choice_Question='" + getvaluefromQuestions(con, "QPrinters_Id") + "' ", comboBox1);
                    loadrichtextbox();
                
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cho = getvaluefromchoices(con, "choice_Id");
        }
    }
}
