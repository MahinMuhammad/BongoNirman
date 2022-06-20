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
    public partial class RegisterForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        ProfRegFrom profRegForm;
        LogInForm logInForm;
        ProfileForm profileForm;
        int currentUser;

        public void setProfRegFrom(ProfRegFrom profRegForm)
        {
            this.profRegForm = profRegForm;
        }
        public ProfRegFrom GetProfRegFrom()
        {
            return profRegForm;
        }
        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm GetProfileForm()
        {
            return profileForm;
        }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Fill up all the fields first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
               if(IsDigitsOnly(textBox2.Text) && IsDigitsOnly(textBox3.Text))
                {
                    if (radioButton1.Checked)
                    {
                        SqlConnection con = new SqlConnection(cs);
                        string usrQuery = "insert into UsrTbl values(NEXT VALUE FOR UsrTbl_seq,@name,@district,'General',@mob,@age,NULL,NULL,NULL,NULL,NULL,@pass,@hidName,@hidMob)";
                        SqlCommand cmd = new SqlCommand(usrQuery, con);
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@mob", Convert.ToInt32(textBox2.Text));
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt32(textBox3.Text));
                        cmd.Parameters.AddWithValue("@district", textBox4.Text);
                        cmd.Parameters.AddWithValue("@pass", textBox5.Text);
                        if (checkBox2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@hidName", "yes");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@hidName", "no");
                        }

                        if (checkBox4.Checked)
                        {
                            cmd.Parameters.AddWithValue("@hidMob", "yes");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@hidMob", "no");
                        }

                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Profile is created Successfully!", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SqlConnection con2 = new SqlConnection(cs);
                            string query2 = "SELECT TOP 1 * FROM UsrTbl ORDER BY ProfileNo DESC";
                            SqlCommand cmd2 = new SqlCommand(query2, con2);

                            con2.Open();
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            dr2.Read();
                            currentUser = Convert.ToInt32(dr2["ProfileNo"]);
                            con2.Close();

                            profileForm.setCurrentUser(currentUser);
                            profileForm.showProfile(currentUser);
                            profileForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();
                    }
                    else if (radioButton2.Checked)
                    {
                        MessageBox.Show("Enter Professional Information as a specialist", "Professional Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string hidName;
                        string hidMob;
                        if (checkBox2.Checked)
                        {
                            hidName = "yes";
                        }
                        else
                        {
                            hidName = "no";
                        }
                        if (checkBox4.Checked)
                        {
                            hidMob = "yes";
                        }
                        else
                        {
                            hidMob = "no";
                        }
                        profRegForm.setInfo(textBox1.Text, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), textBox4.Text, textBox5.Text, hidName, hidMob);
                        profRegForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Must choose Profile type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
               else
                {
                    MessageBox.Show("Mobile number and age must be digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            logInForm.Show();
            this.Hide();
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
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
