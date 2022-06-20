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
    public partial class MarketForm : Form
    {
        MenuForm menuForm;
        LogInForm logInForm;
        AdForm adForm;
        int currentUser;

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

        public void setAdForm(AdForm adForm)
        {
            this.adForm = adForm;
        }
        public AdForm getAdForm()
        {
            return adForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public MarketForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuForm.Show();
        }

        private void MarketForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Left = 5;
            label2.Font = new Font("Palatino Linotype", 20, FontStyle.Bold);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label2.Left = 15;
            label2.Font = new Font("Palatino Linotype", 18, FontStyle.Bold);
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            label3.Left = 5;
            label3.Font = new Font("Palatino Linotype", 20, FontStyle.Bold);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            label3.Left = 15;
            label3.Font = new Font("Palatino Linotype", 18, FontStyle.Bold);

        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            label4.Left = 5;
            label4.Font = new Font("Palatino Linotype", 20, FontStyle.Bold);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            label4.Left = 15;
            label4.Font = new Font("Palatino Linotype", 18, FontStyle.Bold);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            label5.Left = 5;
            label5.Font = new Font("Palatino Linotype", 20, FontStyle.Bold);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            label5.Left = 15;
            label5.Font = new Font("Palatino Linotype", 18, FontStyle.Bold);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            adForm.setImageAndTitle(1);
            adForm.setCurrentUser(currentUser);
            adForm.showAd();
            adForm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            adForm.setImageAndTitle(2);
            adForm.setCurrentUser(currentUser);
            adForm.showAd();
            adForm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            adForm.setImageAndTitle(3);
            adForm.setCurrentUser(currentUser);
            adForm.showAd();
            adForm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            adForm.setImageAndTitle(4);
            adForm.setCurrentUser(currentUser);
            adForm.showAd();
            adForm.Show();
            this.Hide();
        }
    }
}
