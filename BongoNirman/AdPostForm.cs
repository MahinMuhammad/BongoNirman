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
    public partial class AdPostForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        int profileNo;
        string prodType;
        int imageView = 1;
        List<Image> pictures = new List<Image>();

        public void setProfNoAndProdType(int profileNo, string prodType)
        {
            this.profileNo = profileNo;
            this.prodType = prodType;
        }

        public AdPostForm()
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
            button3.Visible = false;
            imageView = 1;
            pictures.Clear();
            pictureBox2.Image = Properties.Resources.empty;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
            this.Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.paper_clip__3_;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.clip;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image File (*.*) | *.*";
            ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictures.Insert(0, new Bitmap(ofd.FileName));
                pictureBox2.Image = pictures[0];
                button3.Visible = true;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictures.Count > 0)
            {
                if (imageView >= pictures.Count)
                {
                    imageView = 0;
                }
                pictureBox2.Image = pictures[imageView];
                imageView++;
            }
            else
            {
                MessageBox.Show("No image is selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.empty;
            pictures.Clear();
            imageView = 1;
            button3.Visible = false;
        }

        private byte[] savePhoto(int i)
        {
            MemoryStream ms = new MemoryStream();
            pictures[i].Save(ms, pictures[i].RawFormat);
            return ms.GetBuffer();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Fill up all the fields first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(IsDigitsOnly(textBox3.Text) && IsDigitsOnly(textBox4.Text))
                {
                    SqlConnection con = new SqlConnection(cs);
                    string usrQuery = "insert into MarketTbl values(NEXT VALUE FOR MarketTbl_seq,@price,@prodType,@loacation,@prodVarify,@quantity,@title, @prodContent, @profileNo)";
                    SqlCommand cmd = new SqlCommand(usrQuery, con);
                    cmd.Parameters.AddWithValue("@prodContent", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@profileNo", profileNo);
                    cmd.Parameters.AddWithValue("@prodType", prodType);
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(textBox4.Text));
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox3.Text));
                    cmd.Parameters.AddWithValue("@loacation", textBox2.Text);
                    cmd.Parameters.AddWithValue("@title", textBox1.Text);
                    cmd.Parameters.AddWithValue("@prodVarify", "no");

                    con.Open();

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Post done Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        richTextBox1.Clear();
                        SqlConnection con2 = new SqlConnection(cs);
                        string query2 = "SELECT TOP 1 * FROM MarketTbl ORDER BY ProductNo DESC";
                        SqlCommand cmd2 = new SqlCommand(query2, con2);

                        con2.Open();
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        dr2.Read();
                        if (pictures.Count > 0)
                        {
                            for (int i = 0; i < pictures.Count; i++)
                            {
                                SqlConnection con3 = new SqlConnection(cs);
                                string query3 = "insert into ProdPicTbl values(NEXT VALUE FOR ProdPicTbl_seq,@prodtNo,@prodtPic)";
                                SqlCommand cmd3 = new SqlCommand(query3, con3);
                                cmd3.Parameters.AddWithValue("@prodtNo", Convert.ToInt32(dr2["ProductNo"]));
                                cmd3.Parameters.AddWithValue("@prodtPic", savePhoto(i));

                                con3.Open();

                                int b = cmd3.ExecuteNonQuery();
                                if (b == 0)
                                {
                                    MessageBox.Show("Database Error! while uploading picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                con3.Close();
                            }
                            button3.Visible = false;
                            imageView = 1;
                            pictures.Clear();
                            pictureBox2.Image = Properties.Resources.empty;
                        }
                        con2.Close();
                    }
                    else
                    {
                        MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Price and Quantity must be digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
