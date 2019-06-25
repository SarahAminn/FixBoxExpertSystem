using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIXBOX
{
    public partial class Questions : UserControl
    {
        public static string operation;
        public Questions()
        {
            InitializeComponent();
        }

        private void Questions_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DeviceMain DV = new DeviceMain();
            this.Parent.Controls.Add(DV);
            DV.Dock = DockStyle.Fill;
            DV.BringToFront();
            this.Hide();
        }

        private void btnMsgOrCode_Click(object sender, EventArgs e)
        {
            ErrorsNMessages Err = new ErrorsNMessages();
            this.Parent.Controls.Add(Err);
            Err.Dock = DockStyle.Fill;
            Err.BringToFront();

        }
    }
}
