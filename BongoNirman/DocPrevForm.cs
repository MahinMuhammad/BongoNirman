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
    public partial class DocPrevForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        DocForm docForm;
        LogInForm logInForm;
        EditorForm editorForm;
        ProfileForm profileForm;
        ProfileForm othersProfileform;
        int currentDoc;
        int currentUser;
        bool editor;
        int imageView = 1;
        List<Image> pictures = new List<Image>();

        public void setDocForm(DocForm docForm)
        {
            this.docForm = docForm;
        }
        public DocForm GetDocForm()
        {
            return docForm;
        }

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public void setEditorForm(EditorForm editorForm)
        {
            this.editorForm = editorForm;
        }
        public EditorForm GetEditorForm()
        {
            return editorForm;
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

        public void setCurrentDocAndEditorAndUser(int currentDoc, bool editor, int currentUser)
        {
            this.currentDoc = currentDoc;
            this.editor = editor;
            this.currentUser = currentUser;
        }

        public DocPrevForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (editor)
            {
                button1.Visible = false;
                editorForm.showSellPosts();
                editorForm.showDoc();
                editorForm.Show();
            }
            else
            {
                docForm.Show();
            }
            this.Hide();
        }

        private void DocPrevForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        public void showDocument()
        {
            if (editor)
            {
                button1.Visible = true;
            }
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM DocTbl WHERE DocNo=@docNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@docNo", currentDoc);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            richTextBox1.Text = sdr2["DocContent"].ToString();
            if (sdr2["DocVarification"].ToString().Equals("no"))
            {
                label1.Text = "NOT VERIFIED";
            }
            else
            {
                label1.Text = "VERIFIED";
            }

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
                label4.Text = sdr4["Name"].ToString();
            }
            else
            {
                label4.Text = sdr4["ProfileNo"].ToString();
            }

            int picCount;
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(DocPictureNo) as pic_count from DocPicTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            picCount = Convert.ToInt32(sdr["pic_count"]);

            con.Close();

            if (picCount > 0)
            {
                for (int i = picCount; i > 0; i--)
                {
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "SELECT * FROM DocPicTbl WHERE DocPictureNo=@docPictureNo";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@docPictureNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    sdr3.Read();
                    if (currentDoc == Convert.ToInt32(sdr3["DocNo"]))
                    {
                        pictures.Add(convertToImage((Byte[])sdr3["DocPicture"]));
                        pictureBox1.Image = pictures[0];
                    }
                }
            }
        }

        private Image convertToImage(byte[] s)
        {
            MemoryStream ms = new MemoryStream(s);
            return Image.FromStream(ms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string usrQuery = "UPDATE DocTbl SET DocVarification = 'yes' where DocNo = @docNo";
            SqlCommand cmd = new SqlCommand(usrQuery, con);
            cmd.Parameters.AddWithValue("@docNo", currentDoc);

            con.Open();

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Product Verified Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Visible = false;
                label1.Text = "VERIFIED";
            }
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

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.Font = new Font("Palatino Linotype", 9, FontStyle.Underline);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font("Palatino Linotype", 9, FontStyle.Bold);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM DocTbl WHERE DocNo=@docNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@docNo", currentDoc);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            if(currentUser == 0)
            {

            }
            else if (currentUser == Convert.ToInt32(sdr2["ProfileNo"]))
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
    }
}
