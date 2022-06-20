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
    public partial class ProductForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        LogInForm logInForm;
        AdForm adForm;
        EditorForm editorForm;
        MsgForm msgForm;
        ProfileForm profileForm;
        ProfileForm othersProfileform;
        int currentProduct;
        int currentUser;
        bool editor;
        int productOwner;
        int imageView = 1;
        List<Image> pictures = new List<Image>();

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm GetLogInForm()
        {
            return logInForm;
        }

        public void setAdForm(AdForm adForm)
        {
            this.adForm = adForm;
        }
        public AdForm getAdForm()
        {
            return adForm;
        }

        public void setEditorForm(EditorForm editorForm)
        {
            this.editorForm = editorForm;
        }
        public EditorForm getEditorForm()
        {
            return editorForm;
        }

        public void setMsgForm(MsgForm msgForm)
        {
            this.msgForm = msgForm;
        }
        public MsgForm getMsgForm()
        {
            return msgForm;
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

        public ProductForm()
        {
            InitializeComponent();
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Image = Properties.Resources.comment__1_;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Image = Properties.Resources.comment;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.Font = new Font("Palatino Linotype", 9, FontStyle.Underline);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font("Palatino Linotype", 9, FontStyle.Bold);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if(editor)
            {
                button1.Visible = false;
                editorForm.showSellPosts();
                editorForm.Show();
            }
            else
            {
                adForm.Show();
            }
            this.Hide();
        }

        private void ProductForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        public void setCurrentProcut(int currentProduct, bool editor)
        {
            this.editor = editor;
            this.currentProduct = currentProduct;
        }
        public void setCurrentProcut(int currentProduct, bool editor, int currentUser)
        {
            this.editor = editor;
            this.currentProduct = currentProduct;
            this.currentUser = currentUser;
        }

        public void showProduct()
        {
            pictureBox1.Image = Properties.Resources.empty;
            pictures.Clear();
            if (editor)
            {
                pictureBox8.Visible = false;
                button1.Visible = true;
            }
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM MarketTbl WHERE ProductNo=@productNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@productNo", currentProduct);

            con2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();
            sdr2.Read();

            richTextBox1.Text = sdr2["ProdDesc"].ToString();
            label2.Text = sdr2["ProdName"].ToString();
            textBox1.Text = sdr2["Price"].ToString();
            textBox2.Text = sdr2["Location"].ToString();
            textBox3.Text = sdr2["AvailableQuantity"].ToString();
            if(sdr2["ProductVerified"].ToString().Equals("no"))
            {
                label6.Text = "NOT VERIFIED";
            }
            else
            {
                label6.Text = "VERIFIED";
            }

            SqlConnection con4 = new SqlConnection(cs);
            string query4 = "SELECT * FROM UsrTbl WHERE ProfileNo=@profileNo";
            SqlCommand cmd4 = new SqlCommand(query4, con4);
            cmd4.Parameters.AddWithValue("@profileNo", Convert.ToInt32(sdr2["ProfileNo"]));

            productOwner = Convert.ToInt32(sdr2["ProfileNo"]);

            con2.Close();

            con4.Open();

            SqlDataReader sdr4 = cmd4.ExecuteReader();
            sdr4.Read();

            if (sdr4["hiddenName"].ToString().Equals("no"))
            {
                label4.Text = sdr4["Name"].ToString();
            }
            else
            {
                label4.Text = sdr4["ProfileNo"].ToString();
            }

            int picCount;
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(ProductPicNo) as pic_count from ProdPicTbl";
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
                    string query3 = "SELECT * FROM ProdPicTbl WHERE ProductPicNo=@productPicNo";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@productPicNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    sdr3.Read();
                    if (currentProduct == Convert.ToInt32(sdr3["ProductNo"]))
                    {
                        pictures.Add(convertToImage((Byte[])sdr3["ProductPic"]));
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
            string usrQuery = "UPDATE MarketTbl SET ProductVerified = 'yes' where ProductNo = @productNo";
            SqlCommand cmd = new SqlCommand(usrQuery, con);
            cmd.Parameters.AddWithValue("@productNo", currentProduct);

            con.Open();

            int a = cmd.ExecuteNonQuery();
            if(a>0)
            {
                MessageBox.Show("Product Verified Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Visible = false;
                label6.Text = "VERIFIED";
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if(currentUser == productOwner)
            {
                MessageBox.Show("You can not send yourself message", "SORRY", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                msgForm.setMsgBox(currentUser, productOwner);
                msgForm.Show();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(cs);
            string query2 = "SELECT * FROM MarketTbl WHERE ProductNo=@productNo";
            SqlCommand cmd2 = new SqlCommand(query2, con2);
            cmd2.Parameters.AddWithValue("@productNo", currentProduct);

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
    }
}
