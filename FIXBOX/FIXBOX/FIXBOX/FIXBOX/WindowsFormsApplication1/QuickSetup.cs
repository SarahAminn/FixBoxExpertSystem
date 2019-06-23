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
    public partial class QuickSetup : UserControl
    {
        public QuickSetup()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            DeviceMain DM = new DeviceMain();
            this.Parent.Controls.Add(DM);
            DM.Dock = DockStyle.Fill;
            DM.BringToFront();
            this.Hide();
        }

        private void QuickSetup_Load(object sender, EventArgs e)
        {

        }

        private string GetCompanyP() {
            string co = "";

            return (co);
        }
    }
}
