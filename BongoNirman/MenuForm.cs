using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BongoNirman
{
    public partial class MenuForm : Form
    {
        NewsForm newsForm;
        ProfileForm profileForm;
        ProfileForm othersProfileform;
        MarketForm marketForm;
        DocForm docForm;
        PassResetForm passResetForm;
        LogInForm logInForm;
        HndSForm hndSForm;
        int currentUser;

        public void setNewsForm(NewsForm newsForm)
        {
            this.newsForm = newsForm;
        }
        public NewsForm getNewsForm()
        {
            return newsForm;
        }

        public void setProfileForm(ProfileForm profileForm)
        {
            this.profileForm = profileForm;
        }
        public ProfileForm getProfileForm()
        {
            return profileForm;
        }

        public void setMarketForm(MarketForm marketForm)
        {
            this.marketForm = marketForm;
        }
        public MarketForm getMarketForm()
        {
            return marketForm;
        }

        public void setDocForm(DocForm docForm)
        {
            this.docForm = docForm;
        }
        public DocForm getDocForm()
        {
            return docForm;
        }

        public void setPassResetForm(PassResetForm passResetForm)
        {
            this.passResetForm = passResetForm;
        }
        public PassResetForm getPassResetForm()
        {
            return passResetForm;
        }

        public void setLogInForm(LogInForm logInForm)
        {
            this.logInForm = logInForm;
        }
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setHndSForm(HndSForm hndSForm)
        {
            this.hndSForm = hndSForm;
        }
        public HndSForm getHndSForm()
        {
            return hndSForm;
        }

        public void setOthersProfileForm(ProfileForm othersProfileform)
        {
            this.othersProfileform = othersProfileform;
        }
        public ProfileForm getOthersProfileForm()
        {
            return othersProfileform;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public int getCurrentUser()
        {
            return currentUser;
        }


        public MenuForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            profileForm.Hide();
            docForm.Hide();
            marketForm.Hide();
            hndSForm.Hide();
            othersProfileform.Hide();
            newsForm.setCurrentUser(currentUser);
            newsForm.showNews();
            profileForm.profileInfoUneditable();
            newsForm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            newsForm.Hide();
            docForm.Hide();
            marketForm.Hide();
            hndSForm.Hide();
            othersProfileform.Hide();
            profileForm.profileInfoUneditable();
            profileForm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            newsForm.Hide();
            docForm.Hide();
            profileForm.Hide();
            hndSForm.Hide();
            othersProfileform.Hide();
            profileForm.profileInfoUneditable();
            marketForm.setCurrentUser(currentUser);
            marketForm.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            newsForm.Hide();
            profileForm.Hide();
            marketForm.Hide();
            hndSForm.Hide();
            othersProfileform.Hide();
            profileForm.profileInfoUneditable();
            docForm.setCurrentUser(currentUser);
            docForm.showDocs();
            docForm.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            passResetForm.setCurrentUser(currentUser);
            profileForm.profileInfoUneditable();
            passResetForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Successfully Logged Out", "Log Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            logInForm.setTextBox("", "");
            this.Hide();
            newsForm.Hide();
            profileForm.Hide();
            marketForm.Hide();
            hndSForm.Hide();
            docForm.Hide();
            profileForm.profileInfoUneditable();
            logInForm.Show();
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

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            newsForm.Hide();
            profileForm.Hide();
            marketForm.Hide();
            docForm.Hide();
            profileForm.profileInfoUneditable();
            hndSForm.Show();
        }
    }
}
