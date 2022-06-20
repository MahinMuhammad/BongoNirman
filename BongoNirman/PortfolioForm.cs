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
    public partial class PortfolioForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        MenuForm menuForm;
        int currentUser;
        int imageView = 1;
        int imageView2 = 1;
        List<Image> pictures = new List<Image>();
        List<Image> pictures2 = new List<Image>();

        public void setMenuForm(MenuForm menuForm)
        {
            this.menuForm = menuForm;
        }
        public MenuForm getMenuForm()
        {
            return menuForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }
        public PortfolioForm()
        {
            InitializeComponent();
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete__1_;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete__1_;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            imageView = 1;
            imageView2 = 1;
            pictures.Clear();
            pictures2.Clear();
            pictureBox1.Image = Properties.Resources.empty;
            pictureBox2.Image = Properties.Resources.empty;
            this.Hide();
        }

        public void showPictures()
        {
            if (menuForm.getCurrentUser() != currentUser)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                button1.Visible = false;
            }
            else
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                button1.Visible = true;
            }

            button3.Visible = false;
            imageView = 1;
            imageView2 = 1;
            pictures.Clear();
            pictures2.Clear();
            pictureBox1.Image = Properties.Resources.empty;
            pictureBox2.Image = Properties.Resources.empty;

            int picCount;
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(PortfolioPicNo) as pic_count from PortPicTbl";
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
                    string query3 = "SELECT * FROM PortPicTbl WHERE PortfolioPicNo=@portfolioPicNo";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@portfolioPicNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    sdr3.Read();
                    if (currentUser == Convert.ToInt32(sdr3["ProfileNo"]))
                    {
                        pictures.Add(convertToImage((Byte[])sdr3["PortfolioPic"]));
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

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.paper_clip__3_;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.clip;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image File (*.*) | *.*";
            ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictures2.Insert(0, new Bitmap(ofd.FileName));
                pictureBox2.Image = pictures2[0];
                button3.Visible = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictures2.Count > 0)
            {
                if (imageView2 >= pictures2.Count)
                {
                    imageView2 = 0;
                }
                pictureBox2.Image = pictures2[imageView];
                imageView2++;
            }
            else
            {
                MessageBox.Show("No image is selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.empty;
            pictures2.Clear();
            imageView2 = 1;
            button3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictures2.Count > 0)
            {
                for (int i = 0; i < pictures2.Count; i++)
                {
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "insert into PortPicTbl values(NEXT VALUE FOR PortPicTbl_seq,@profNo,@portPic)";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@profNo", menuForm.getCurrentUser());
                    cmd3.Parameters.AddWithValue("@portPic", savePhoto(i));

                    con3.Open();

                    int b = cmd3.ExecuteNonQuery();
                    if (b == 0)
                    {
                        MessageBox.Show("Database Error! while uploading picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Picture uploaded in portfolio successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        showPictures();
                    }

                    con3.Close();
                }
                imageView2 = 1;
                pictures2.Clear();
                pictureBox2.Image = Properties.Resources.empty;
                button3.Visible = false;
            }
        }

        private byte[] savePhoto(int i)
        {
            MemoryStream ms = new MemoryStream();
            pictures2[i].Save(ms, pictures2[i].RawFormat);
            return ms.GetBuffer();
        }
    }
}
