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
    public partial class HndSForm : Form
    {
        MenuForm menuForm;
        LogInForm logInForm;
        FandQForm fandQForm;
        AboutForm aboutForm;

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
        public LogInForm getLogInForm()
        {
            return logInForm;
        }

        public void setFandQForm(FandQForm fandQForm)
        {
            this.fandQForm = fandQForm;
        }
        public FandQForm getFandQForm()
        {
            return fandQForm;
        }

        public void setAboutForm(AboutForm aboutForm)
        {
            this.aboutForm = aboutForm;
        }
        public AboutForm getAboutForm()
        {
            return aboutForm;
        }

        public HndSForm()
        {
            InitializeComponent();
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

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            menuForm.Show();
        }

        private void HndSForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            fandQForm.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            aboutForm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UpdateInfoForm updateInfoForm = new UpdateInfoForm();
            updateInfoForm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UpdateInfoForm updateInfoForm = new UpdateInfoForm();
            updateInfoForm.Show();
        }
    }
}
