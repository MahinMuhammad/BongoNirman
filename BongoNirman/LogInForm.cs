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
    public partial class LogInForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        RegisterForm registerForm;
        OTPForm oTPForm;
        ProfileForm profileForm;
        EditorForm editorForm;
        int currentUser;
        string currentEditor;

        public void setRegisterForm(RegisterForm registerForm)
        {
            this.registerForm = registerForm;
        }
        public RegisterForm getRegisterForm()
        {
            return registerForm;
        }

        public void setOTPform(OTPForm oTPForm)
        {
            this.oTPForm = oTPForm;
        }
        public OTPForm getOTPForm()
        {
            return oTPForm;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm getProfileForm()
        {
            return profileForm;
        }

        public void setEditorForm(EditorForm editorForm)
        {
            this.editorForm = editorForm;
        }
        public EditorForm getEditorForm()
        {
            return editorForm;
        }

        public LogInForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)==true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "This Field needed to fill up!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "This Field needed to fill up!");
            }
            else
            {
                errorProvider2.Clear();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //oTPForm.Show();
            UpdateInfoForm updateInfoForm = new UpdateInfoForm();
            updateInfoForm.Show();
            //this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {  
            registerForm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
                pictureBox2.Image = Properties.Resources._private;
            }
            else if(textBox2.UseSystemPasswordChar == false)
            {
                textBox2.UseSystemPasswordChar = true;
                pictureBox2.Image = Properties.Resources.view;
            }
        }

        public void setTextBox(String text1, String text2)
        {
            textBox1.Text = text1;
            textBox2.Text = text2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("USER NAME and PASSWORD can not be empty!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (IsDigitsOnly(textBox1.Text) == false)
                {
                    SqlConnection con2 = new SqlConnection(cs);
                    string edtrQuery = "select * from EditorTbl where EditorId=@edId and pass=@pass";
                    SqlCommand cmd2 = new SqlCommand(edtrQuery, con2);
                    cmd2.Parameters.AddWithValue("@edId", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@pass", textBox2.Text);

                    con2.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows == true)
                    {
                        MessageBox.Show("EDITOR'S SPACE!", "WELCOME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentEditor = textBox1.Text;
                        this.Hide();
                        editorForm.showSellPosts();
                        editorForm.showDoc();
                        editorForm.Show();
                    }
                    con2.Close();
                }

                else
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "select * from UsrTbl where ProfileNo=@profNo and pass=@pass";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@profNo", Convert.ToInt32(textBox1.Text));
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        MessageBox.Show("WELCOME!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentUser = Convert.ToInt32(textBox1.Text);
                        profileForm.setCurrentUser(currentUser);
                        profileForm.showProfile(currentUser);
                        this.Hide();
                        profileForm.Show();
                    }
                    else
                    {

                        MessageBox.Show("WRONG USER NUM OR PASSWORD", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
