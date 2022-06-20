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
    public partial class ProfessionalForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        PortfolioForm portfolioForm;
        ProfileForm profileForm;
        int currentUser;

        public void setPortfolioForm(PortfolioForm portfolioForm)
        {
            this.portfolioForm = portfolioForm;
        }
        public PortfolioForm getPortfolioForm()
        {
            return portfolioForm;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm getProfileForm()
        {
            return profileForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public ProfessionalForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.photo;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.photo_gallery;
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
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //profileForm.Hide();
            this.Hide();
            portfolioForm.setCurrentUser(currentUser);
            portfolioForm.showPictures();
            portfolioForm.Show();
        }

        public void showProfInfo()
        {
            SqlConnection con = new SqlConnection(cs);
            string usrQuery = "select * from UsrTbl where ProfileNo=@profNo";
            SqlCommand cmd = new SqlCommand(usrQuery, con);
            cmd.Parameters.AddWithValue("@profNo", currentUser);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            textBox1.Text = sdr["JobTitle"].ToString();
            textBox2.Text = sdr["WorksAt"].ToString();
            textBox3.Text = sdr["EducationQualification"].ToString();
        }
    }
}
