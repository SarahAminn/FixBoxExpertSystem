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
    public partial class AddPrinter : UserControl
    {
        SqlConnection con = new SqlConnection();
        string imgLoc = "";
        public AddPrinter()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            loadComboBox("select Company_name from Companys", comboBox1);
            pictureBox_Img.SizeMode = PictureBoxSizeMode.Zoom;
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Get a Value From the data base 
        public string getvaluefromDB(String Col,String SelectedCol,String DBname,SqlConnection con,ComboBox comboBox2) {

            string Val=" ";
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return Val;
        }

        private void AddPrinter_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbDescription.Clear();
            tbIType.Clear();
            tbModel.Clear();
            tbPType.Clear();
            comboBox1.ResetText();
            pictureBox_Img.Image = null;
            pictureBox_Img.Refresh();
            imgLoc = null;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*,jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                dlg.Title = "Select Printer Picture";
                if (dlg.ShowDialog() == DialogResult.OK) {
                    imgLoc = dlg.FileName.ToString();
                    pictureBox_Img.ImageLocation = imgLoc;
                }
            
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String Co = getvaluefromDB("Company_Id", "Company_Name", "FIXBOX", con, comboBox1);
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                String query = "insert into Printers(printer_model,printer_Type,printer_Company,printer_IType,printer_Desc,printer_Image) values('"+tbModel.Text+"','"+tbPType.Text+"','"+Co+"','"+tbIType.Text+"','"+tbDescription.Text+"',@img)";
                if(con.State!=ConnectionState.Open)
                      con.Open();
                 SqlCommand command = new SqlCommand(query,con);
                 command.Parameters.Add(new SqlParameter("@img",img));
                 int x = command.ExecuteNonQuery();
                 con.Close();
                 MessageBox.Show(x.ToString() + " record(s) saved.");
                
            }
            catch (Exception ex) {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        

    }
}
