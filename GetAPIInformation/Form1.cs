using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using APIHelpers;

namespace GetAPIInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string testApiPath = @"C:\TFS Source Code\TrafficCopV2API\TrafficCopV2API_PCI\TrafficCop.EOBLockbox-BusinessRulesCheckDefaults\bin\Debug\TrafficCop.EOBLockbox-BusinessRulesCheckDefaults.dll";
            List<string> apiList = APILoader.LoadFromPath(testApiPath);            
            lbApi.DataSource = apiList;
        }
    }
}
