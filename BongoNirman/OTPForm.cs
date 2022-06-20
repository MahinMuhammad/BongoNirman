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
    public partial class OTPForm : Form
    {
        PassResetForm passResetForm;
        LogInForm logInForm;

        public void setPassResetForm(PassResetForm passResetForm)
        {
            this.passResetForm = passResetForm;
        }
        public PassResetForm GetPassResetForm()
        {
            return passResetForm;
        }

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }
        public OTPForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("OTP box cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("OTP matched!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                passResetForm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Profile no. and Mobile no. cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Enabled = false;
            }
            else
            {
                MessageBox.Show("OTP sent!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox3.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            logInForm.Show();
            this.Hide();
        }

        private void OTPForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }
    }
}
