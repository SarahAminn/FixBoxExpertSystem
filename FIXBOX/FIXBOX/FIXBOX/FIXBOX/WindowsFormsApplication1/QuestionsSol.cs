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
    public partial class QuestionsSol : UserControl
    {
        SqlConnection con = new SqlConnection();
        public static string type, it, co;
        int order = 1, max = 1;
        public QuestionsSol()
        {
            InitializeComponent();
        }

        private void getMax()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select MAX(CHSol_Order) from ChoiceSolutions where where CHSol_Company='" + co + "' and CHSol_Printer='" + Device.id + "' and CHSol_Choice='" + QuestionsHome.cho + "'", con);
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


        private void loadpicturebox()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select CHSol_Solution from ChoiceSolutions where CHSol_Company='" + co + "' and CHSol_Printer='" + Device.id + "' and CHSol_Order='" + order + "' and CHSol_Choice='" + QuestionsHome.cho + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])reader[0];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }


        }

        private void QuestionsSol_Load(object sender, EventArgs e)
        {
            GetData();
            getMax();
            loadpicturebox();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (order != max)
            {
                order++;
                loadpicturebox();


            }
            else {
                MessageBox.Show("No more details.","Info",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (order > 0)
            {
                order--;
                loadpicturebox();


            }
            else
            {
                MessageBox.Show("No more details.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
