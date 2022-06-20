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
    public partial class ProfileForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        PostForm postForm;
        MenuForm menuForm;
        LogInForm logInForm;
        ProfessionalForm professionalForm;
        InboxForm inboxForm;
        MsgForm msgForm;
        int currentUser;
        int shownUser; //only for othersProfile object
        bool inbox = true;

        public void setPostForm(PostForm postForm)
        {
            this.postForm = postForm;
        }
        public PostForm getPostForm()
        {
            return postForm;
        }

        public void setMenuForm(MenuForm menuForm)
        {
            this.menuForm = menuForm;
        }
        public MenuForm getMenuForm()
        {
            return menuForm;
        }

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setProfessionalForm(ProfessionalForm professionalForm)
        {
            this.professionalForm = professionalForm;
        }
        public ProfessionalForm getProfessionalForm()
        {
            return professionalForm;
        }

        public void setInboxForm(InboxForm inboxForm)
        {
            this.inboxForm = inboxForm;
        }
        public InboxForm getInboxForm()
        {
            return inboxForm;
        }

        public void setMsgForm(MsgForm msgForm)
        {
            this.msgForm = msgForm;
        }
        public MsgForm getMsgForm()
        {
            return msgForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public void setOthersProfile()
        {
            richTextBox1.Visible = false;
        }

        public ProfileForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            postForm.setPost(currentUser);
            postForm.Show();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            menuForm.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Specialist")
            {
                professionalForm.setCurrentUser(shownUser);
                professionalForm.showProfInfo();
                professionalForm.Show();
            }
            else
            {
                MessageBox.Show("More feature is only for Specialist profile", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ProfileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            profileInfoUneditable();
            logInForm.Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.inbox;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.email;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(inbox)
            {
                this.Hide();
                inboxForm.showMessages(currentUser);
                inboxForm.Show();
            }
            else
            {
                msgForm.setMsgBox(menuForm.getCurrentUser(), shownUser);
                msgForm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Enabled==false)
            {
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                button3.Text = "Submit";
                button1.Visible = false;
            }
            else
            { 
                if(IsDigitsOnly(textBox4.Text) && IsDigitsOnly(textBox5.Text))
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "UPDATE UsrTbl SET name = @usrname, MobileNo = @mobnum, Age = @age, District = @district, hiddenName = @hidName, hiddenMob = @hidMob where ProfileNo = @profNo";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@profNo", currentUser);
                    cmd.Parameters.AddWithValue("@usrname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@mobnum", textBox4.Text);
                    cmd.Parameters.AddWithValue("@age", textBox5.Text);
                    cmd.Parameters.AddWithValue("@district", textBox6.Text);
                    if (checkBox2.Checked)
                    {
                        cmd.Parameters.AddWithValue("@hidName", "yes");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@hidName", "no");
                    }

                    if (checkBox1.Checked)
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
                        MessageBox.Show("Informations Changed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        profileInfoUneditable();
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Mobile number and age must be digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        public void showProfile(int userNum)
        {
            this.shownUser = userNum;
            if(currentUser != userNum)
            {
                button3.Visible = false;
                richTextBox1.Visible = false;
                inbox = false;
            }
            else
            {
                menuForm.setCurrentUser(currentUser);
            }
            SqlConnection con = new SqlConnection(cs);
            string usrQuery = "select ProfileType, Name, ProfileNo, MobileNo, Age, District, hiddenName, hiddenMob from UsrTbl where ProfileNo=@profNo";
            SqlCommand cmd = new SqlCommand(usrQuery, con);
            cmd.Parameters.AddWithValue("@profNo", userNum);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            textBox1.Text = sdr["ProfileType"].ToString();
            textBox2.Text = sdr["Name"].ToString();
            textBox3.Text = sdr["ProfileNo"].ToString();
            textBox4.Text = sdr["MobileNo"].ToString();
            textBox5.Text = sdr["Age"].ToString();
            textBox6.Text = sdr["District"].ToString();
            string hideName = sdr["hiddenName"].ToString();
            string hideMob = sdr["hiddenMob"].ToString();

            if(hideName == "yes")
            {
                textBox2.Visible = false;
                checkBox2.Checked = true;
            }
            else
            {
                textBox2.Visible = true;
                checkBox2.Checked = false;
            }

            if(hideMob == "yes")
            {
                textBox4.Visible = false;
                checkBox1.Checked = true;
            }
            else
            {
                textBox4.Visible = true;
                checkBox1.Checked = false;
            }

            con.Close();
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
        public void profileInfoUneditable()
        {
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            button3.Text = "Edit";
            button1.Visible = true;
            if (checkBox1.Checked)
            {
                textBox4.Visible = false;
            }
            else
            {
                textBox4.Visible = true;
            }
            if (checkBox2.Checked)
            {
                textBox2.Visible = false;
            }
            else
            {
                textBox2.Visible = true;
            }
        }
    }
}
