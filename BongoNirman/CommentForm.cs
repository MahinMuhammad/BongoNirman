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
    public partial class CommentForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        NewsPrevForm newsPrevForm;
        int currentPost;
        int currentUser;

        public void setNewsPrevForm(NewsPrevForm newsPrevForm)
        {
            this.newsPrevForm = newsPrevForm;
        }
        public NewsPrevForm GetNewsPrevForm()
        {
            return newsPrevForm;
        }

        public void setCurrentUserAndPost(int currentUser, int currentPost)
        {
            this.currentPost = currentPost;
            this.currentUser = currentUser;
        }

        public CommentForm()
        {
            InitializeComponent();
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
            richTextBox1.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Fill up all the comment box first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string usrQuery = "insert into ComTbl values(NEXT VALUE FOR ComTbl_seq,@content,@profNo,@postfNo)";
                SqlCommand cmd = new SqlCommand(usrQuery, con);
                cmd.Parameters.AddWithValue("@content", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@profNo", currentUser);
                cmd.Parameters.AddWithValue("@postfNo", currentPost);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Comment made Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.Clear();
                    newsPrevForm.refreshComment();
                }
                else
                {
                    MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
