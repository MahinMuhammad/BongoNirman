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
    public partial class NewsPrevForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        NewsForm newsForm;
        LogInForm logInForm;
        CommentForm commentForm;
        ProfileForm profileForm;
        ProfileForm othersProfileform;
        int currentUser;
        int currentPost;
        int imageView = 1;
        int commentView = 0;
        bool userLiked;
        string likeType;
        List<Image> pictures = new List<Image>();
        List<int> commentsList = new List<int>();

        public void setNewsForm(NewsForm newsForm)
        {
            this.newsForm = newsForm;
        }
        public NewsForm getNewsForm()
        {
            return newsForm;
        }

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public void setCommentForm(CommentForm commentForm)
        {
            this.commentForm = commentForm;
        }
        public CommentForm GetCommentForm()
        {
            return commentForm;
        }

        public void setCurrentUserAndPost(int currentUser, int currentPost)
        {
            this.currentPost = currentPost;
            this.currentUser = currentUser;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm getProfileForm()
        {
            return profileForm;
        }

        public void setOthersProfileForm(ProfileForm othersProfileform)
        {
            this.othersProfileform = othersProfileform;
        }
        public ProfileForm getOthersProfileForm()
        {
            return othersProfileform;
        }

        public NewsPrevForm()
        {
            InitializeComponent();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.next;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.next__2_;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.prevOn;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.prev;
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Font = new Font("Palatino Linotype", 9, FontStyle.Underline);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font("Palatino Linotype", 9, FontStyle.Bold);
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Image = Properties.Resources.comment__1_;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Image = Properties.Resources.comment;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            imageView = 1;
            pictures.Clear();
            pictureBox1.Image = Properties.Resources.empty;
            newsForm.Show();
            this.Hide();
        }

        private void NewsPrevForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            commentForm.setCurrentUserAndPost(currentUser, currentPost);
            commentForm.Show();
        }

        public void showPost()
        {
            imageView = 1;
            pictures.Clear();
            pictureBox1.Image = Properties.Resources.empty;
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM PostTbl WHERE PostNo=@postNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@postNo", currentPost);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            richTextBox1.Text = sdr2["PostContent"].ToString();
            textBox1.Text = sdr2["PostDate"].ToString();

            SqlConnection con4 = new SqlConnection(cs);
            string query4 = "SELECT * FROM UsrTbl WHERE ProfileNo=@profileNo";
            SqlCommand cmd4 = new SqlCommand(query4, con4);
            cmd4.Parameters.AddWithValue("@profileNo", Convert.ToInt32(sdr2["ProfileNo"]));

            con2.Close();

            con4.Open();

            SqlDataReader sdr4 = cmd4.ExecuteReader();
            sdr4.Read();

            if(sdr4["hiddenName"].ToString().Equals("no"))
            {
                label2.Text = sdr4["Name"].ToString();
            }
            else
            {
                label2.Text = sdr4["ProfileNo"].ToString();
            }

            int picCount;
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(PostPicNo) as pic_count from PostPicTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            picCount = Convert.ToInt32(sdr["pic_count"]);

            con.Close();

            if(picCount > 0)
            {
                for(int i=picCount; i>0; i--)
                {
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "SELECT * FROM PostPicTbl WHERE PostPicNo=@postPicNo";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@postPicNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    sdr3.Read();
                    if(currentPost == Convert.ToInt32(sdr3["PostNo"]))
                    {
                        pictures.Add(convertToImage((Byte[])sdr3["PostPic"]));
                        pictureBox1.Image = pictures[0];
                    }
                }
            }
            loadComments();
            showComment();
            loadLikes();
        }

        public void refreshComment()
        {
            loadComments();
            showComment();
        }

        private void loadComments()
        {
            commentsList.Clear();
            commentView = 0;

            int commentCount;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(ComNo) as com_count from ComTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            commentCount = Convert.ToInt32(sdr["com_count"]);

            con.Close();

            if(commentCount > 0)
            {
                for (int i = commentCount; i > 0; i--)
                {
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM ComTbl WHERE ComNo=@comNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@comNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();
                    if(Convert.ToInt32(sdr2["PostNo"]) == currentPost)
                    {
                        commentsList.Add(i);
                    }
                }
            }
        }

        private void showComment()
        {
            if(commentsList.Count == 0)
            {
                richTextBox2.Visible = false;
                label1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
            }
            else
            {
                richTextBox2.Visible = true;
                label1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;

                SqlConnection con2 = new SqlConnection(cs);
                string query2 = "SELECT * FROM ComTbl WHERE ComNo=@comNo";
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@comNo", commentsList[commentView]);

                con2.Open();

                SqlDataReader sdr2 = cmd2.ExecuteReader();
                sdr2.Read();

                richTextBox2.Text = sdr2["ComContent"].ToString();

                SqlConnection con3 = new SqlConnection(cs);
                string query3 = "SELECT * FROM UsrTbl WHERE ProfileNo=@profileNo";
                SqlCommand cmd3 = new SqlCommand(query3, con3);
                cmd3.Parameters.AddWithValue("@profileNo", Convert.ToInt32(sdr2["ProfileNo"]));

                con3.Open();

                SqlDataReader sdr3 = cmd3.ExecuteReader();
                sdr3.Read();
                if (sdr3["hiddenName"].ToString().Equals("no"))
                {
                    label1.Text = sdr3["Name"].ToString();
                }
                else
                {
                    label1.Text = sdr3["ProfileNo"].ToString();
                }
            }
        }

        private Image convertToImage(byte[] s)
        {
            MemoryStream ms = new MemoryStream(s);
            return Image.FromStream(ms);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictures.Count > 0)
            {
                if (imageView >= pictures.Count)
                {
                    imageView = 0;
                }
                pictureBox1.Image = pictures[imageView];
                imageView++;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            commentView++;
            if(commentView >= commentsList.Count)
            {
                commentView--;
            }
            showComment();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            commentView--;
            if (commentView < 0)
            {
                commentView++;
            }
            showComment();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM ComTbl WHERE ComNo=@comNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@comNo", commentsList[commentView]);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            if(currentUser == Convert.ToInt32(sdr2["ProfileNo"]))
            {
                this.Hide();
                profileForm.Show();
            }
            else
            {
                this.Hide();
                othersProfileform.showProfile(Convert.ToInt32(sdr2["ProfileNo"]));
                othersProfileform.Show();
            }
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.Font = new Font("Palatino Linotype", 9, FontStyle.Underline);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font("Palatino Linotype", 9, FontStyle.Bold);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM PostTbl WHERE PostNo=@postNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@postNo", currentPost);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            if (currentUser == Convert.ToInt32(sdr2["ProfileNo"]))
            {
                this.Hide();
                profileForm.Show();
            }
            else
            {
                this.Hide();
                othersProfileform.showProfile(Convert.ToInt32(sdr2["ProfileNo"]));
                othersProfileform.Show();
            }
        }

        private void loadLikes()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(LikeNo) as like_count from LikeTbl where PostNo = @postNo and LikeType = 'like'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@postNo", currentPost);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            label3.Text = sdr["like_count"].ToString();

            con.Close();

            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "select count(LikeNo) as like_count from LikeTbl where PostNo = @postNo and LikeType = 'love'";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@postNo", currentPost);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();
            label4.Text = sdr2["like_count"].ToString();

            con2.Close();

            SqlConnection con3 = new SqlConnection(cs);
            string query3 = "select count(LikeNo) as like_count from LikeTbl where PostNo = @postNo and LikeType = 'dislike'";
            SqlCommand cmd3 = new SqlCommand(query3, con3);
            cmd3.Parameters.AddWithValue("@postNo", currentPost);

            con3.Open();

            SqlDataReader sdr3 = cmd3.ExecuteReader();
            sdr3.Read();
            label5.Text = sdr3["like_count"].ToString();

            con3.Close();

            SqlConnection con4 = new SqlConnection(cs);
            string query4 = "select * from LikeTbl where PostNo = @postNo and ProfileNo = @profileNo";
            SqlCommand cmd4 = new SqlCommand(query4, con4);
            cmd4.Parameters.AddWithValue("@postNo", currentPost);
            cmd4.Parameters.AddWithValue("@profileNo", currentUser);

            con4.Open();

            SqlDataReader sdr4 = cmd4.ExecuteReader();
            sdr4.Read();
            if(sdr4.HasRows)
            {
                userLiked = true;
                if(sdr4["LikeType"].ToString().Equals("like"))
                {
                    pictureBox5.Image = Properties.Resources.like;
                    pictureBox7.Image = Properties.Resources.heart__1_;
                    pictureBox6.Image = Properties.Resources.thumb_down;
                    likeType = "like";
                }
                else if(sdr4["LikeType"].ToString().Equals("love"))
                {
                    pictureBox5.Image = Properties.Resources.like__1_;
                    pictureBox7.Image = Properties.Resources.heart;
                    pictureBox6.Image = Properties.Resources.thumb_down;
                    likeType = "love";
                }
                else if(sdr4["LikeType"].ToString().Equals("dislike"))
                {
                    pictureBox5.Image = Properties.Resources.like__1_;
                    pictureBox7.Image = Properties.Resources.heart__1_;
                    pictureBox6.Image = Properties.Resources.thumbs_down;
                    likeType = "dislike";
                }
            }
            else
            {
                pictureBox5.Image = Properties.Resources.like__1_;
                pictureBox7.Image = Properties.Resources.heart__1_;
                pictureBox6.Image = Properties.Resources.thumb_down;
                likeType = "nolike";
                userLiked = false;
            }
            con4.Close();
        }

        private void refreshLikeType()
        {
            if(likeType.Equals("like"))
            {
                pictureBox5.Image = Properties.Resources.like;
                pictureBox7.Image = Properties.Resources.heart__1_;
                pictureBox6.Image = Properties.Resources.thumb_down;
            }
            else if(likeType.Equals("love"))
            {
                pictureBox5.Image = Properties.Resources.like__1_;
                pictureBox7.Image = Properties.Resources.heart;
                pictureBox6.Image = Properties.Resources.thumb_down;
            }
            else if(likeType.Equals("dislike"))
            {
                pictureBox5.Image = Properties.Resources.like__1_;
                pictureBox7.Image = Properties.Resources.heart__1_;
                pictureBox6.Image = Properties.Resources.thumbs_down;
            }
            else if(likeType.Equals("nolike"))
            {
                pictureBox5.Image = Properties.Resources.like__1_;
                pictureBox7.Image = Properties.Resources.heart__1_;
                pictureBox6.Image = Properties.Resources.thumb_down;
            }

            if(likeType.Equals("nolike"))
            {
                SqlConnection con = new SqlConnection(cs);
                string usrQuery = "delete from LikeTbl where PostNo = @postNo and ProfileNo = @profileNo";
                SqlCommand cmd = new SqlCommand(usrQuery, con);
                cmd.Parameters.AddWithValue("@postNo", currentPost);
                cmd.Parameters.AddWithValue("@profileNo", currentUser);

                con.Open();

                int a = cmd.ExecuteNonQuery();
                if(a > 0)
                {
                    loadLikes();
                }
                else
                {
                    MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {
                if (userLiked == true)
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "UPDATE LikeTbl SET LikeType = @likeType where PostNo = @postNo and ProfileNo = @profileNo";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@likeType", likeType);
                    cmd.Parameters.AddWithValue("@postNo", currentPost);
                    cmd.Parameters.AddWithValue("@profileNo", currentUser);

                    con.Open();

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        loadLikes();
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "insert into LikeTbl values(NEXT VALUE FOR LikeTbl_seq,@likeType,@profileNo, @postNo)";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@likeType", likeType);
                    cmd.Parameters.AddWithValue("@postNo", currentPost);
                    cmd.Parameters.AddWithValue("@profileNo", currentUser);

                    con.Open();

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        loadLikes();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if(likeType.Equals("nolike") || likeType.Equals("dislike") || likeType.Equals("love"))
            {
                likeType = "like";
                refreshLikeType();
            }
            else
            {
                likeType = "nolike";
                refreshLikeType();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (likeType.Equals("nolike") || likeType.Equals("like") || likeType.Equals("dislike"))
            {
                likeType = "love";
                refreshLikeType();
            }
            else
            {
                likeType = "nolike";
                refreshLikeType();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (likeType.Equals("nolike") || likeType.Equals("like") || likeType.Equals("love"))
            {
                likeType = "dislike";
                refreshLikeType();
            }
            else
            {
                likeType = "nolike";
                refreshLikeType();
            }
        }
    }
}
