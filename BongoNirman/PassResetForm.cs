using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace BongoNirman
{
    public partial class PassResetForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        ProfileForm profileForm;
        OTPForm oTPForm;
        int currentUser;

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm GetProfileForm()
        {
            return profileForm;
        }

        public void setOTPform(OTPForm oTPForm)
        {
            this.oTPForm = oTPForm;
        }
        public OTPForm GetOTPForm()
        {
            return oTPForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public PassResetForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Two password fields cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(textBox1.Text == textBox2.Text)
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "UPDATE UsrTbl SET pass = @pass where ProfileNo = @profNo";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@profNo", currentUser);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                    con.Open();

                    int a = cmd.ExecuteNonQuery();

                    if(a>0)
                    {
                        MessageBox.Show("Password changed successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        profileForm.Show();
                        this.Hide();
                        oTPForm.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Confirm Password first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete__1_;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
