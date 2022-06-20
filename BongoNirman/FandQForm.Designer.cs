
namespace BongoNirman
{
    partial class FandQForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Yes, you can contact us 24/7 using the RepRev feature in Help and Services sectio" +
        "n.");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Do you offer 24/7 customer support? ", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Please visit the About section in Help and Services to know about us.");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Can you tell me more about Bongonirman?", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Click Edit from your profile window and then check the regarding boxes to hide or" +
        " unhide your name or mobile number.");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("How to hide my name and mobile number?", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Click on the post here box from profile window or newsfeed.");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("How can I post on news feed?", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(152, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(422, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frequently Asked Questions";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = global::BongoNirman.Properties.Resources.query;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Palatino Linotype", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(12, 125);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node4";
            treeNode1.Text = "Yes, you can contact us 24/7 using the RepRev feature in Help and Services sectio" +
    "n.";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Do you offer 24/7 customer support? ";
            treeNode3.Name = "Node5";
            treeNode3.Text = "Please visit the About section in Help and Services to know about us.";
            treeNode4.Name = "Node1";
            treeNode4.Text = "Can you tell me more about Bongonirman?";
            treeNode5.Name = "Node6";
            treeNode5.Text = "Click Edit from your profile window and then check the regarding boxes to hide or" +
    " unhide your name or mobile number.";
            treeNode6.Name = "Node2";
            treeNode6.Text = "How to hide my name and mobile number?";
            treeNode7.Name = "Node7";
            treeNode7.Text = "Click on the post here box from profile window or newsfeed.";
            treeNode8.Name = "Node3";
            treeNode8.Text = "How can I post on news feed?";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode6,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(658, 313);
            this.treeView1.TabIndex = 3;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DeepSkyBlue;
            this.linkLabel1.Location = new System.Drawing.Point(621, 24);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(32, 13);
            this.linkLabel1.TabIndex = 27;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Back";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // FandQForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(682, 450);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "FandQForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frequently Asked Questions";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FandQForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}