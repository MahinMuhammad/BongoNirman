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
    public partial class EditorForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        LogInForm logInForm;
        ProductForm productForm;
        DocPrevForm docPrevForm;
        List<int> salePost = new List<int>();
        List<int> docRev = new List<int>();

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setProductForm(ProductForm productForm)
        {
            this.productForm = productForm;
        }
        public ProductForm getProductForm()
        {
            return productForm;
        }

        public void setDocPrevForm(DocPrevForm docPrevForm)
        {
            this.docPrevForm = docPrevForm;
        }
        public DocPrevForm getDocPrevForm()
        {
            return docPrevForm;
        }

        public EditorForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Successfully Logged Out", "Log Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            logInForm.setTextBox("", "");
            salePost.Clear();
            docRev.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            this.Hide();
            logInForm.Show();
        }

        private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        public void showSellPosts()
        {
            int postCount;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(ProductNo) as post_count from MarketTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            postCount = Convert.ToInt32(sdr["post_count"]);

            con.Close();

            if (postCount > 0)
            {
                for (int i = postCount; i > 0; i--)
                {
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM MarketTbl WHERE ProductNo=@productNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@productNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();
                    if (sdr2["ProductVerified"].ToString().Equals("no"))
                    {
                        listBox1.Items.Add(sdr2["ProdName"].ToString());
                        salePost.Add(i);
                    }
                    con2.Close();
                }
            }
        }

        public void showDoc()
        {
            int postCount;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(DocNo) as doc_count from DocTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            postCount = Convert.ToInt32(sdr["doc_count"]);

            con.Close();

            if (postCount > 0)
            {
                for (int i = postCount; i > 0; i--)
                {
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM DocTbl WHERE DocNo=@docNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@docNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();
                    if (sdr2["DocVarification"].ToString().Equals("no"))
                    {
                        listBox2.Items.Add(sdr2["DocContent"].ToString());
                        docRev.Add(i);
                    }
                    con2.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                productForm.setCurrentProcut(salePost[Convert.ToInt32(listBox1.Items.IndexOf(listBox1.SelectedItem))], true);
                productForm.showProduct();
                productForm.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                docPrevForm.setCurrentDocAndEditorAndUser(docRev[Convert.ToInt32(listBox2.Items.IndexOf(listBox2.SelectedItem))], true, 0);
                docPrevForm.showDocument();
                docPrevForm.Show();
                this.Hide();
            }
        }
    }
}
