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
    public partial class AddChoiceSolution : UserControl
    {
        public static string id;
        SqlConnection con = new SqlConnection();
        string imgLoc;
        public AddChoiceSolution()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
            cbPrinters.Enabled = false;
            loadComboBox("select Company_name from Companys", cbCo);

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

        public string getvaluefromCompanys( SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select "+Col+" from Companys where Company_name='"+cbCo.SelectedItem.ToString()+"'", con);
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

        public string getvaluefromPrinters(SqlConnection con, String Col)
        {

            string Val = " ";
            try
            {
                SqlDataAdapter Cmd_CI = new SqlDataAdapter("select " + Col + " from Printers where printer_model='" + cbPrinters.SelectedItem.ToString() + "'", con);
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


        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbOrder.Clear();
            pictureBox_Sol.Image = null;
            cbCo.Text = null;
            cbPrinters.Text = null;
            loadComboBox("select Company_name from Companys", cbCo);
            
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
                    pictureBox_Sol.ImageLocation = imgLoc;
                }

            }
            catch (Exception ex)
            {
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
                String query = "insert into ChoiceSolutions(CHSol_Order,CHSol_Choice,CHSol_Printer,CHSol_Company,CHSol_Solution) values('" + tbOrder.Text + "'," + id + ",'"+getvaluefromPrinters(con,"printer_Id")+"','"+getvaluefromCompanys(con,"Company_Id")+"',@img)";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@img", img);
                int x = command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + " record(s) saved.");
                

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbCo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPrinters.Enabled = true;
            loadComboBox("select printer_model from Printers where printer_Company = '" + getvaluefromCompanys(con, "Company_Id") + "'", cbPrinters);
        }

    }
}
