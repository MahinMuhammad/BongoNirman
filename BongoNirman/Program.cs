using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;

namespace BongoNirman
{
    public class NewsFeedClass
    {
        private int newsNo;
        private string date;
        private string content;
        private Image newsPic;

        public NewsFeedClass(int newsNo, string date, string content, Image newsPic)
        {
            this.newsNo = newsNo;
            this.date = date;
            this.content = content;
            this.newsPic = newsPic;
        }
        public int getNewsNo()
        {
            return newsNo;
        }
        public string getDate()
        {
            return date;
        }
        public string getContent()
        {
            return content;
        }
        public Image getnewsPic()
        {
            return newsPic;
        }
    }

    public class ProductClass
    {
        private int productNo;
        private string price;
        private string location;
        private string prodTitle;
        private Image prodPic;

        public ProductClass(int productNo, string price, string location, string prodTitle, Image prodPic)
        {
            this.productNo = productNo;
            this.price = price;
            this.location = location;
            this.prodPic = prodPic;
            this.prodTitle = prodTitle;
        }
        public int getProdNo()
        {
            return productNo;
        }
        public string getPrice()
        {
            return price;
        }
        public string getLocation()
        {
            return location;
        }
        public Image getProdPic()
        {
            return prodPic;
        }
        public string getProdTitle()
        {
            return prodTitle;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

            //creating instances of all forms
            LogInForm logInForm = new LogInForm();
            RegisterForm registerForm = new RegisterForm();
            ProfRegFrom profRegFrom = new ProfRegFrom();
            ProfileForm profileForm = new ProfileForm();
            ProfileForm othersProfileForm = new ProfileForm();
            InboxForm inboxForm = new InboxForm();
            MsgForm msgForm = new MsgForm();
            ProfessionalForm professionalForm = new ProfessionalForm();
            PortfolioForm portfolioForm = new PortfolioForm();
            OTPForm oTPForm = new OTPForm();
            PassResetForm passResetForm = new PassResetForm();
            NewsForm newsForm = new NewsForm();
            MenuForm menuForm = new MenuForm();
            MarketForm marketForm = new MarketForm();
            AdForm adForm = new AdForm();
            AdPostForm adPostForm = new AdPostForm();
            ProductForm productForm = new ProductForm();
            DocForm docForm = new DocForm();
            DocPrevForm docPrevForm = new DocPrevForm();
            PostForm postForm = new PostForm();
            NewsPrevForm newsPrevForm = new NewsPrevForm();
            CommentForm commentForm = new CommentForm();
            HndSForm hndSForm = new HndSForm();
            AboutForm aboutForm = new AboutForm();
            FandQForm fandQForm = new FandQForm();
            EditorForm editorForm = new EditorForm();
            DocPostForm docPostForm = new DocPostForm();

            //connecting forms with instances
            logInForm.setRegisterForm(registerForm);
            logInForm.setProfileForm(profileForm);
            logInForm.setOTPform(oTPForm);
            logInForm.setEditorForm(editorForm);

            registerForm.setLogInForm(logInForm);
            registerForm.setProfRegFrom(profRegFrom);
            registerForm.setProfileForm(profileForm);

            profRegFrom.setLogInForm(logInForm);
            profRegFrom.setRegisterForm(registerForm);

            oTPForm.setLogInForm(logInForm);
            oTPForm.setPassResetForm(passResetForm);

            passResetForm.setProfileForm(profileForm);
            passResetForm.setOTPform(oTPForm);

            profileForm.setLogInForm(logInForm);
            profileForm.setPostForm(postForm);
            profileForm.setMenuForm(menuForm);
            profileForm.setProfessionalForm(professionalForm);
            profileForm.setInboxForm(inboxForm);

            othersProfileForm.setLogInForm(logInForm);
            othersProfileForm.setMenuForm(menuForm);
            othersProfileForm.setProfessionalForm(professionalForm);
            othersProfileForm.setMsgForm(msgForm);
            othersProfileForm.setOthersProfile();

            professionalForm.setPortfolioForm(portfolioForm);
            professionalForm.setProfileForm(profileForm);

            portfolioForm.setMenuForm(menuForm);

            inboxForm.setLogInForm(logInForm);
            inboxForm.setProfileForm(profileForm);
            inboxForm.setMsgForm(msgForm);

            msgForm.setInboxForm(inboxForm);

            menuForm.setLogInForm(logInForm);
            menuForm.setNewsForm(newsForm);
            menuForm.setProfileForm(profileForm);
            menuForm.setOthersProfileForm(othersProfileForm);
            menuForm.setMarketForm(marketForm);
            menuForm.setDocForm(docForm);
            menuForm.setPassResetForm(passResetForm);
            menuForm.setHndSForm(hndSForm);

            newsForm.setLogInForm(logInForm);
            newsForm.setMenuForm(menuForm);
            newsForm.setPostForm(postForm);
            newsForm.setNewsPrevForm(newsPrevForm);

            postForm.setNewsForm(newsForm);

            newsPrevForm.setLogInForm(logInForm);
            newsPrevForm.setNewsForm(newsForm);
            newsPrevForm.setCommentForm(commentForm);
            newsPrevForm.setProfileForm(profileForm);
            newsPrevForm.setOthersProfileForm(othersProfileForm);

            commentForm.setNewsPrevForm(newsPrevForm);

            marketForm.setLogInForm(logInForm);
            marketForm.setMenuForm(menuForm);
            marketForm.setAdForm(adForm);

            adForm.setLogInForm(logInForm);
            adForm.setMarketForm(marketForm);
            adForm.setProductForm(productForm);
            adForm.setAdPostForm(adPostForm);

            productForm.setLogInForm(logInForm);
            productForm.setAdForm(adForm);
            productForm.setEditorForm(editorForm);
            productForm.setMsgForm(msgForm);
            productForm.setProfileForm(profileForm);
            productForm.setOthersProfileForm(othersProfileForm);

            docForm.setLogInForm(logInForm);
            docForm.setMenuForm(menuForm);
            docForm.setDocPrevForm(docPrevForm);
            docForm.setDocPostForm(docPostForm);

            docPrevForm.setLogInForm(logInForm);
            docPrevForm.setDocForm(docForm);
            docPrevForm.setEditorForm(editorForm);
            docPrevForm.setProfileForm(profileForm);
            docPrevForm.setOthersProfileForm(othersProfileForm);

            hndSForm.setLogInForm(logInForm);
            hndSForm.setMenuForm(menuForm);
            hndSForm.setFandQForm(fandQForm);
            hndSForm.setAboutForm(aboutForm);

            fandQForm.setLogInForm(logInForm);
            fandQForm.setHndSForm(hndSForm);

            editorForm.setLogInForm(logInForm);
            editorForm.setDocPrevForm(docPrevForm);
            editorForm.setProductForm(productForm);

            Application.Run(logInForm);
        }
    }
}
