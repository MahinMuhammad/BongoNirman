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
    public partial class ProfRegFrom : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        RegisterForm registerForm;
        LogInForm logInForm;
        string name;
        int mobNum;
        int age;
        string district;
        string pass;
        string hidName;
        string hidMob;
        string jobStatus;

        public void setInfo(string name, int mobNum, int age, string district, string pass, string hidName, string hidMob)
        {
            this.name = name;
            this.mobNum = mobNum;
            this.age = age;
            this.district = district;
            this.pass = pass;
            this.hidName = hidName;
            this.hidMob = hidMob;
        }

        public void setRegisterForm(RegisterForm registerForm)
        {
            this.registerForm = registerForm;
        }
        public RegisterForm GetRegisterForm()
        {
            return registerForm;
        }
        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public ProfRegFrom()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registerForm.Show();
            this.Hide();
        }

        private void ProfRegFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true & comboBox1.Items.Count > 0 && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string usrQuery = "insert into specWaitTbl values(NEXT VALUE FOR specWaitTbl_seq,@name,@district,'Specialist',@mob,@age,NULL,@jobTitle,@eduQuali,@worksAt,@jobStat,@pass,@hidName,@hidMob)";
                SqlCommand cmd = new SqlCommand(usrQuery, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@mob", mobNum);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@district", district);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@hidName", hidName);
                cmd.Parameters.AddWithValue("@hidMob", hidMob);
                cmd.Parameters.AddWithValue("@jobTitle", textBox1.Text);
                cmd.Parameters.AddWithValue("@eduQuali", textBox2.Text);
                cmd.Parameters.AddWithValue("@worksAt", textBox3.Text);
                if(Convert.ToInt32(comboBox1.Items.IndexOf(comboBox1.SelectedItem)) == 0)
                {
                    jobStatus = "Hiring";
                }
                else if(Convert.ToInt32(comboBox1.Items.IndexOf(comboBox1.SelectedItem)) == 1)
                {
                    jobStatus = "Finding job";
                }
                else
                {
                    jobStatus = "None";
                }
                cmd.Parameters.AddWithValue("@jobStat", jobStatus);

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if(a > 0)
                {
                    MessageBox.Show("Profile is sent for varification!", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Fill up all the fields and acknowledge the truth of the information first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
