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
    public partial class DocForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        MenuForm menuForm;
        LogInForm logInForm;
        DocPrevForm docPrevForm;
        DocPostForm docPostForm;
        int currentUser;
        List<int> docRev = new List<int>();

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

        public void setDocPrevForm(DocPrevForm docPrevForm)
        {
            this.docPrevForm = docPrevForm;
        }
        public DocPrevForm getDocPrevForm()
        {
            return docPrevForm;
        }

        public void setDocPostForm(DocPostForm docPostForm)
        {
            this.docPostForm = docPostForm;
        }
        public DocPostForm getDocPostForm()
        {
            return docPostForm;
        }
        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public DocForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuForm.Show();
        }

        private void DocForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            docPostForm.setCurrentUser(currentUser);
            docPostForm.Show();
        }

        public void showDocs()
        {
            listBox1.Items.Clear();
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
                    if (sdr2["DocVarification"].ToString().Equals("yes"))
                    {
                        listBox1.Items.Add(sdr2["DocContent"].ToString());
                        docRev.Add(i);
                    }
                    con2.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                docPrevForm.setCurrentDocAndEditorAndUser(docRev[Convert.ToInt32(listBox1.Items.IndexOf(listBox1.SelectedItem))], false, currentUser);
                docPrevForm.showDocument();
                docPrevForm.Show();
                this.Hide();
            }
        }
    }
}
