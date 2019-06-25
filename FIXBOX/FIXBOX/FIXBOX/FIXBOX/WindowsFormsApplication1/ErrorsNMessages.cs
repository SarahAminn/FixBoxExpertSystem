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
    public partial class ErrorsNMessages : UserControl
    {
        SqlConnection con = new SqlConnection();
        public ErrorsNMessages()
        {
            InitializeComponent();
            con.ConnectionString = "data source = (local);database = FIXBOX;integrated security = SSPI";
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {

        }
    }
}
