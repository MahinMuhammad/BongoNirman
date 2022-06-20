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

    public partial class InboxForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        LogInForm logInForm;
        ProfileForm profileForm;
        MsgForm msgForm;
        int currentUser;
        List<int> ownMsgNo = new List<int>();
        List<int> senderMsgNo = new List<int>();

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm getProfileForm()
        {
            return profileForm;
        }

        public void setMsgForm(MsgForm msgForm)
        {
            this.msgForm = msgForm;
        }
        public MsgForm getMsgForm()
        {
            return msgForm;
        }

        public InboxForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.pencil;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.pencil__1_;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            senderMsgNo.Clear();
            ownMsgNo.Clear();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            this.Hide();
            profileForm.Show();
        }

        private void InboxForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            msgForm.setMsgBox(currentUser);
            msgForm.Show();
        }

        public void showMessages(int currentUser)
        {
            senderMsgNo.Clear();
            ownMsgNo.Clear();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            int msgCount;
            this.currentUser = currentUser;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(MsgNo) as msg_count from MsgTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            msgCount = Convert.ToInt32(sdr["msg_count"]);

            con.Close();

            if(msgCount>0)
            {
                for (int i = msgCount; i > 0; i--)
                {
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM MsgTbl WHERE MsgNo=@msgNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@msgNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();
                    if(currentUser == Convert.ToInt32(sdr2["ProfileNo"]))
                    {
                        listBox1.Items.Add(sdr2["Sub"].ToString());
                        ownMsgNo.Add(i);
                    }
                    else if(currentUser == Convert.ToInt32(sdr2["IncoProfNo"]))
                    {
                        listBox2.Items.Add(sdr2["Sub"].ToString());
                        senderMsgNo.Add(i);
                    }
                    con2.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT * FROM MsgTbl WHERE MsgNo=@msgNo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@msgNo", ownMsgNo[Convert.ToInt32(listBox1.Items.IndexOf(listBox1.SelectedItem))]);

                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                textBox1.Text = sdr["Sub"].ToString();
                richTextBox1.Text = sdr["Content"].ToString();

                SqlConnection con2 = new SqlConnection(cs);
                string query2 = "SELECT Name, ProfileNo, hiddenName FROM UsrTbl WHERE ProfileNo=@profNo";
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@profNo", Convert.ToInt32(sdr["IncoProfNo"].ToString()));

                con2.Open();

                SqlDataReader sdr2 = cmd2.ExecuteReader();
                sdr2.Read();

                if(sdr2["hiddenName"].ToString().Equals("no"))
                {
                    label1.Text = sdr2["Name"].ToString();
                }
                else
                {
                    label1.Text = sdr2["ProfileNo"].ToString();
                }

                con2.Close();

                con.Close();
                groupBox1.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT * FROM MsgTbl WHERE MsgNo=@msgNo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@msgNo", senderMsgNo[Convert.ToInt32(listBox2.Items.IndexOf(listBox2.SelectedItem))]);

                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                textBox2.Text = sdr["Sub"].ToString();
                richTextBox2.Text = sdr["Content"].ToString();

                SqlConnection con2 = new SqlConnection(cs);
                string query2 = "SELECT Name, ProfileNo, hiddenName FROM UsrTbl WHERE ProfileNo=@profNo";
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@profNo", Convert.ToInt32(sdr["ProfileNo"].ToString()));

                con2.Open();

                SqlDataReader sdr2 = cmd2.ExecuteReader();
                sdr2.Read();
                if (sdr2["hiddenName"].ToString().Equals("no"))
                {
                    label3.Text = sdr2["Name"].ToString();
                }
                else
                {
                    label3.Text = sdr2["ProfileNo"].ToString();
                }

                con2.Close();

                con.Close();
                groupBox2.Visible = true;
            }
        }
    }
}
