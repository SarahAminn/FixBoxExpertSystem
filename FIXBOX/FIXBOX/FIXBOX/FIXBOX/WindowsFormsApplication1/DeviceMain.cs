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
    public partial class DeviceMain : UserControl
    {
        public DeviceMain()
        {
            InitializeComponent();
        }

        private void DeviceMain_Load(object sender, EventArgs e)
        {
           
        }

        private void FillUC() {

            string Id = Device.id;
        
        }
    }
}
