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
    public partial class MsgForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        InboxForm inboxForm;
        int sendertUser;
        int receiverUser;

        public void setInboxForm(InboxForm inboxForm)
        {
            this.inboxForm = inboxForm;
        }
        public InboxForm getInboxForm()
        {
            return inboxForm;
        }

        public MsgForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.send__1_;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.send;
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
            richTextBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
        }
        public void setMsgBox(int sendertUser)
        {
            this.sendertUser = sendertUser;
            this.receiverUser = 0;
            textBox2.Text = "";
            textBox2.ReadOnly = false;
        }
        public void setMsgBox(int sendertUser, int receiverUser)
        {
            this.sendertUser = sendertUser;
            this.receiverUser = receiverUser;
            textBox2.Text = receiverUser.ToString();
            textBox2.ReadOnly = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Fill up all the fields first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(IsDigitsOnly(textBox2.Text))
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "insert into MsgTbl values(NEXT VALUE FOR MsgTbl_seq,@content,@incoProfNo,@profNo,@sub)";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@content", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@incoProfNo", sendertUser);
                    cmd.Parameters.AddWithValue("@profNo", Convert.ToInt32(textBox2.Text));
                    cmd.Parameters.AddWithValue("@sub", textBox1.Text);

                    con.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        MessageBox.Show("Message sent Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        richTextBox1.Clear();
                        inboxForm.showMessages(sendertUser);
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("WRONG USER NUM", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
