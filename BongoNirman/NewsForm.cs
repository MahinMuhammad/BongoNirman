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
using System.IO;

namespace BongoNirman
{
    public partial class NewsForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        MenuForm menuForm;
        PostForm postForm;
        LogInForm logInForm;
        NewsPrevForm newsPrevForm;
        int currentUser;
        List<NewsFeedClass> newsList = new List<NewsFeedClass>();
        int nextNews;
        int prev1;
        int prev2;
        int prev3;
        int prev4;

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
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public void setNewsPrevForm(NewsPrevForm newsPrevForm)
        {
            this.newsPrevForm = newsPrevForm;
        }
        public NewsPrevForm GetNewsPrevForm()
        {
            return newsPrevForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public NewsForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuForm.Show();
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            postForm.setPost(currentUser);
            postForm.Show();
        }

        private void NewsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newsPrevForm.setCurrentUserAndPost(currentUser, prev1);
            newsPrevForm.showPost();
            newsPrevForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newsPrevForm.setCurrentUserAndPost(currentUser, prev2);
            newsPrevForm.showPost();
            newsPrevForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            newsPrevForm.setCurrentUserAndPost(currentUser, prev3);
            newsPrevForm.showPost();
            newsPrevForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            newsPrevForm.setCurrentUserAndPost(currentUser, prev4);
            newsPrevForm.showPost();
            newsPrevForm.Show();
            this.Hide();
        }

        public void showNews()
        {
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
            pictureBox2.Image = Properties.Resources.empty;
            pictureBox3.Image = Properties.Resources.empty;
            pictureBox4.Image = Properties.Resources.empty;
            pictureBox5.Image = Properties.Resources.empty;
            nextNews = 4;
            newsList.Clear();
            int newsCount;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(PostNo) as news_count from PostTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            newsCount = Convert.ToInt32(sdr["news_count"]);

            con.Close();

            if(newsCount > 0)
            {
                for (int i = newsCount; i > 0; i--)
                {
                    //getting post infos from post table
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM PostTbl WHERE PostNo=@postNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@postNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();

                    //geting pictuers from post picture table
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "SELECT TOP 1 PostPic FROM PostPicTbl WHERE PostNo=@postNo ORDER BY PostPicNo DESC";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@postNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    if (sdr3.Read())
                    {
                        newsList.Add(new NewsFeedClass(Convert.ToInt32(sdr2["PostNo"]), sdr2["PostDate"].ToString(), sdr2["PostContent"].ToString(), convertToImage((Byte[])sdr3["PostPic"])));
                    }
                    else
                    {
                        newsList.Add(new NewsFeedClass(Convert.ToInt32(sdr2["PostNo"]), sdr2["PostDate"].ToString(), sdr2["PostContent"].ToString(), null));
                    }

                }
                setNews(0); 
            }
        }

        private Image convertToImage(byte[] s)
        {
            MemoryStream ms = new MemoryStream(s);
            return Image.FromStream(ms);
        }

        private void setNews(int i)
        {
            if(i >= newsList.Count)
            {
                return;
            }
            label2.Text = newsList[i].getContent();
            textBox1.Text = newsList[i].getDate();
            if (newsList[i].getnewsPic() != null)
            {
                pictureBox2.Image = newsList[i].getnewsPic();
            }
            else
            {
                pictureBox2.Image = Properties.Resources.empty;
            }
            prev1 = newsList[i].getNewsNo();
            i++;

            if (i >= newsList.Count)
            {
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                return;
            }
            label3.Text = newsList[i].getContent();
            textBox2.Text = newsList[i].getDate();
            if (newsList[i].getnewsPic() != null)
            {
                pictureBox3.Image = newsList[i].getnewsPic();
            }
            else
            {
                pictureBox3.Image = Properties.Resources.empty;
            }
            prev2 = newsList[i].getNewsNo();
            i++;

            if (i >= newsList.Count)
            {
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                return;
            }
            label4.Text = newsList[i].getContent();
            textBox3.Text = newsList[i].getDate();
            if (newsList[i].getnewsPic() != null)
            {
                pictureBox4.Image = newsList[i].getnewsPic();
            }
            else
            {
                pictureBox4.Image = Properties.Resources.empty;
            }
            prev3 = newsList[i].getNewsNo();
            i++;

            if (i >= newsList.Count)
            {
                groupBox4.Visible = false;
                return;
            }
            label5.Text = newsList[i].getContent();
            textBox4.Text = newsList[i].getDate();
            if(newsList[i].getnewsPic()!=null)
            {
                pictureBox5.Image = newsList[i].getnewsPic();
            }
            else
            {
                pictureBox5.Image = Properties.Resources.empty;
            }
            prev4 = newsList[i].getNewsNo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showNews();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setNews(nextNews);
            nextNews = nextNews + 4;
        }
    }
}
