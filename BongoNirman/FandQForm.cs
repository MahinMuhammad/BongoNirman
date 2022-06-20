using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BongoNirman
{
    public partial class FandQForm : Form
    {
        LogInForm logInForm;
        HndSForm hndSForm;

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setHndSForm(HndSForm hndSForm)
        {
            this.hndSForm = hndSForm;
        }
        public HndSForm getHndSForm()
        {
            return hndSForm;
        }

        public FandQForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            hndSForm.Show();
        }

        private void FandQForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }
    }
}
