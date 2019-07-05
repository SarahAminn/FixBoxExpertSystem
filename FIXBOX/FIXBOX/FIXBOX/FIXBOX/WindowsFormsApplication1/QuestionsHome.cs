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
        public static string  it,cho=null,co,chSol ;
        int order = 1, max=1,counter=0;
        
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
                SqlCommand cmd = new SqlCommand("select MAX(QPrinters_Order) from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='"+Questions.operation+"'", con);
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
                SqlCommand cmd = new SqlCommand("select printers_IType,printer_Company from Printers where printer_Id='" + Device.id + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    
                    it = reader[0].ToString();
                    
                    co = reader[1].ToString();
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
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+Col+" from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='"+order.ToString()+"'", con);
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
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_ConCh='"+choice+"'", con);
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
                SqlCommand cmd = new SqlCommand("select QPrinters_Question from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_Order='" + order.ToString() + "'", con);
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
                SqlCommand cmd = new SqlCommand("select QPrinters_Question from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_ConCh='" + Choice + "'", con);
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
            
            // find solution to getquest
            string Val = " ";
            try
            {
                string query ;
                if (cho == null)
                {
                    query = "select " + Col + " from Choices where choice_Question='" + getvaluefromQuestions(con, "QPrinters_Id") + "' and choice_ch='" + comboBox1.SelectedItem.ToString() + "'";

                }
                else {
                    query = "select " + Col + " from Choices where choice_Question='" + getvaluefromQuestions(con, "QPrinters_Id",cho) + "' and choice_ch='" + comboBox1.SelectedItem.ToString() + "'";
                }
                SqlDataAdapter Cmd_CI = new SqlDataAdapter(query, con);
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
            
           
            cho = getvaluefromchoices(con, "choice_Id");
           // try
         //   {
                
                SqlCommand cmd_sol = new SqlCommand("select CHSol_Id from ChoiceSolutions where CHSol_Choice ='" + cho + "'", con);
                con.Open();
                SqlDataReader reader = cmd_sol.ExecuteReader();
                reader.Read();
                
                if (reader.HasRows) {
                    QuestionsSol QSol = new QuestionsSol();
                    this.Parent.Controls.Add(QSol);
                    QSol.BringToFront();
                    QSol.Dock = DockStyle.Fill;
                    con.Close();
                   
                    this.Hide();
                
                }else{
                    SqlCommand cmd_ques;
                    
                     cmd_ques = new SqlCommand("select QPrinters_Question from QuestionsPrinters where QPrinters_IType='" + it + "' and QPrinters_QType='" + Questions.operation + "' and QPrinters_ConCh='" + cho + "'", con);
                    
                    con.Open();
                    SqlDataReader red = cmd_ques.ExecuteReader();
                    red.Read();
                    if (red.HasRows)
                    {
                        loadComboBox("select choice_ch from Choices where choice_Question='" + getvaluefromQuestions(con, "QPrinters_Id", cho) + "' ", comboBox1);
                        loadrichtextbox(cho);
                        con.Close();
                    }
                    else { 
                    MessageBox.Show("No Solution found Please Contact the Companys Support!!","No Solution",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    GoToSite(getvaluefromCompanys(con, "company_WebSearch") + Device.Model);
                    con.Close();
                    
                    }
                    con.Close();
                    
                
                }
                con.Close();
           // }
           // catch (Exception ex) { con.Close(); MessageBox.Show(ex.Message); }
                counter++;
        }

        public static void GoToSite(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        public string getvaluefromCompanys(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from Companys where Company_Id='" + Device.co + "'", con);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
