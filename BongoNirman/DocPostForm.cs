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
    public partial class DocPostForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        int currentUser;
        int imageView = 1;
        List<Image> pictures = new List<Image>();
        public DocPostForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.paper_clip__3_;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.clip;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete__1_;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.delete;
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            imageView = 1;
            pictures.Clear();
            pictureBox2.Image = Properties.Resources.empty;
            richTextBox1.Clear();
            this.Hide();
        }

        private byte[] savePhoto(int i)
        {
            MemoryStream ms = new MemoryStream();
            pictures[i].Save(ms, pictures[i].RawFormat);
            return ms.GetBuffer();
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Fill up all the fields first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string usrQuery = "insert into DocTbl values(NEXT VALUE FOR DocTbl_seq,@content,@verify,@profNo)";
                SqlCommand cmd = new SqlCommand(usrQuery, con);
                cmd.Parameters.AddWithValue("@content", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@profNo", currentUser);
                cmd.Parameters.AddWithValue("@verify", "no");

                con.Open();

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Document submitted for varification!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.Clear();
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT TOP 1 * FROM DocTbl ORDER BY DocNo DESC";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);

                    con2.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    dr2.Read();
                    if (pictures.Count > 0)
                    {
                        for (int i = 0; i < pictures.Count; i++)
                        {
                            SqlConnection con3 = new SqlConnection(cs);
                            string query3 = "insert into DocPicTbl values(NEXT VALUE FOR DocPicTbl_seq,@docNo,@docPicture)";
                            SqlCommand cmd3 = new SqlCommand(query3, con3);
                            cmd3.Parameters.AddWithValue("@docNo", Convert.ToInt32(dr2["DocNo"]));
                            cmd3.Parameters.AddWithValue("@docPicture", savePhoto(i));

                            con3.Open();

                            int b = cmd3.ExecuteNonQuery();
                            if (b == 0)
                            {
                                MessageBox.Show("Database Error! while uploading picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            con3.Close();
                        }
                        imageView = 1;
                        pictures.Clear();
                        pictureBox2.Image = Properties.Resources.empty;
                        button3.Visible = false;
                    }
                    con2.Close();
                }
                else
                {
                    MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }
    }
}
