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
    public partial class AdForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        MarketForm marketForm;
        LogInForm logInForm;
        ProductForm productForm;
        AdPostForm adPostForm;
        int currentUser;
        int prodType;
        List<ProductClass> prodList = new List<ProductClass>();
        int nextProd;
        int prev1;
        int prev2;
        int prev3;
        int prev4;

        public void setMarketForm(MarketForm marketForm)
        {
            this.marketForm = marketForm;
        }
        public MarketForm getMarketForm()
        {
            return marketForm;
        }

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

        public void setAdPostForm(AdPostForm adPostForm)
        {
            this.adPostForm = adPostForm;
        }
        public AdPostForm getAdPostForm()
        {
            return adPostForm;
        }

        public void setCurrentUser(int currentUser)
        {
            this.currentUser = currentUser;
        }

        public AdForm()
        {
            InitializeComponent();
        }

        public void setImageAndTitle (int choice)
        {
            this.prodType = choice;
            switch(choice)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.engineer;
                    label13.Text = "SPECIALISTS";
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.blueprint__1_;
                    label13.Text = "BLUEPRINT";
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.pilates;
                    label13.Text = "MATERIALS";
                    break;
                case 4:
                    pictureBox1.Image = Properties.Resources.worker;
                    label13.Text = "LABOR";
                    break;
            }
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Image = Properties.Resources.ads;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = Properties.Resources.advertisement;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            label2.Text = "";
            label6.Text = "";
            label9.Text = "";
            label12.Text = "";
            prodList.Clear();
            marketForm.Show();
            this.Hide();
        }

        private void AdForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Close();
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.refresh;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.refresh__1_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productForm.setCurrentProcut(prev1, false, currentUser);
            productForm.showProduct();
            productForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productForm.setCurrentProcut(prev2, false, currentUser);
            productForm.showProduct();
            productForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productForm.setCurrentProcut(prev3, false, currentUser);
            productForm.showProduct();
            productForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            productForm.setCurrentProcut(prev4, false, currentUser);
            productForm.showProduct();
            productForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if(prodType == 1)
            {
                adPostForm.setProfNoAndProdType(currentUser, "SPECIALISTS");
            }
            else if(prodType == 2)
            {
                adPostForm.setProfNoAndProdType(currentUser, "BLUEPRINT");
            }
            else if(prodType == 3)
            {
                adPostForm.setProfNoAndProdType(currentUser, "MATERIALS");
            }
            else
            {
                adPostForm.setProfNoAndProdType(currentUser, "LABOR");
            }
            adPostForm.Show();
        }

        public void showAd()
        {
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
            pictureBox2.Image = Properties.Resources.empty;
            pictureBox3.Image = Properties.Resources.empty;
            pictureBox4.Image = Properties.Resources.empty;
            pictureBox5.Image = Properties.Resources.empty;
            nextProd = 4;
            prodList.Clear();
            int prodCount;

            SqlConnection con = new SqlConnection(cs);
            string query = "select count(ProductNo) as prod_count from MarketTbl";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            prodCount = Convert.ToInt32(sdr["prod_count"]);

            con.Close();

            if (prodCount > 0)
            {
                for (int i = prodCount; i > 0; i--)
                {
                    //getting post infos from post table
                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "SELECT * FROM MarketTbl WHERE ProductNo=@productNo";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@productNo", i);

                    con2.Open();

                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    sdr2.Read();

                    //geting pictuers from post picture table
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "SELECT TOP 1 ProductPic FROM ProdPicTbl WHERE ProductNo=@productNo ORDER BY ProductPicNo DESC";
                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@productNo", i);

                    con3.Open();

                    SqlDataReader sdr3 = cmd3.ExecuteReader();
                    if(label13.Text.Equals(sdr2["ProductType"].ToString()))
                    {
                        if (sdr3.Read())
                        {
                            prodList.Add(new ProductClass(Convert.ToInt32(sdr2["ProductNo"]), sdr2["Price"].ToString(), sdr2["Location"].ToString(), sdr2["ProdName"].ToString(), convertToImage((Byte[])sdr3["ProductPic"])));
                        }
                        else
                        {
                            prodList.Add(new ProductClass(Convert.ToInt32(sdr2["ProductNo"]), sdr2["Price"].ToString(), sdr2["Location"].ToString(), sdr2["ProdName"].ToString(), null));
                        }
                    }

                }
                setProd(0);
            }
        }

        private Image convertToImage(byte[] s)
        {
            MemoryStream ms = new MemoryStream(s);
            return Image.FromStream(ms);
        }

        private void setProd(int i)
        {
            if (i >= prodList.Count)
            {
                return;
            }
            label2.Text = prodList[i].getProdTitle();
            textBox1.Text = prodList[i].getPrice();
            textBox2.Text = prodList[i].getLocation();
            if (prodList[i].getProdPic() != null)
            {
                pictureBox2.Image = prodList[i].getProdPic();
            }
            else
            {
                pictureBox2.Image = Properties.Resources.empty;
            }
            prev1 = prodList[i].getProdNo();
            i++;

            if (i >= prodList.Count)
            {
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                return;
            }
            label6.Text = prodList[i].getProdTitle();
            textBox4.Text = prodList[i].getPrice();
            textBox3.Text = prodList[i].getLocation();
            if (prodList[i].getProdPic() != null)
            {
                pictureBox3.Image = prodList[i].getProdPic();
            }
            else
            {
                pictureBox3.Image = Properties.Resources.empty;
            }
            prev2 = prodList[i].getProdNo();
            i++;

            if (i >= prodList.Count)
            {
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                return;
            }
            label9.Text = prodList[i].getProdTitle();
            textBox6.Text = prodList[i].getPrice();
            textBox5.Text = prodList[i].getLocation();
            if (prodList[i].getProdPic() != null)
            {
                pictureBox4.Image = prodList[i].getProdPic();
            }
            else
            {
                pictureBox4.Image = Properties.Resources.empty;
            }
            prev3 = prodList[i].getProdNo();
            i++;

            if (i >= prodList.Count)
            {
                groupBox4.Visible = false;
                return;
            }
            label12.Text = prodList[i].getProdTitle();
            textBox8.Text = prodList[i].getPrice();
            textBox7.Text = prodList[i].getLocation();
            if (prodList[i].getProdPic() != null)
            {
                pictureBox5.Image = prodList[i].getProdPic();
            }
            else
            {
                pictureBox5.Image = Properties.Resources.empty;
            }
            prev4 = prodList[i].getProdNo();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            showAd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setProd(nextProd);
            nextProd = nextProd + 4;
        }
    }
}
