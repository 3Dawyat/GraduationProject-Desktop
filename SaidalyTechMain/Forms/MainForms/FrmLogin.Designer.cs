namespace SaidalyTechMain.Forms.MainForms
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textEmail = new DevExpress.XtraEditors.TextEdit();
            this.textPassword = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.ForgotPasswordLink = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 382);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Calligraphy", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome To Saidaly Tech";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(319, 263);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Snap ITC", 20F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.label2.Location = new System.Drawing.Point(432, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Login";
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(438, 164);
            this.textEmail.Name = "textEmail";
            this.textEmail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.textEmail.Properties.Appearance.Options.UseFont = true;
            this.textEmail.Properties.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.textEmail.Size = new System.Drawing.Size(161, 26);
            this.textEmail.TabIndex = 3;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(438, 203);
            this.textPassword.Name = "textPassword";
            this.textPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.textPassword.Properties.Appearance.Options.UseFont = true;
            this.textPassword.Properties.Padding = new System.Windows.Forms.Padding(0, 4, 0, 1);
            this.textPassword.Properties.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(161, 27);
            this.textPassword.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.label3.Location = new System.Drawing.Point(345, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.label4.Location = new System.Drawing.Point(328, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.btnSubmit.Location = new System.Drawing.Point(412, 282);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(147, 35);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ForgotPasswordLink
            // 
            this.ForgotPasswordLink.AutoSize = true;
            this.ForgotPasswordLink.Font = new System.Drawing.Font("Segoe Script", 9.75F);
            this.ForgotPasswordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ForgotPasswordLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.ForgotPasswordLink.Location = new System.Drawing.Point(435, 232);
            this.ForgotPasswordLink.Name = "ForgotPasswordLink";
            this.ForgotPasswordLink.Size = new System.Drawing.Size(172, 20);
            this.ForgotPasswordLink.TabIndex = 8;
            this.ForgotPasswordLink.TabStop = true;
            this.ForgotPasswordLink.Text = "Forgot Your Password ?";
            this.ForgotPasswordLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.ForgotPasswordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ForgotPasswordLink_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Calligraphy", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(71)))), ((int)(((byte)(103)))));
            this.label5.Location = new System.Drawing.Point(328, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(314, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "All rights reserved to Team Graduation Group 1";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 379);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ForgotPasswordLink);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FrmLogin.IconOptions.Image")));
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Saidaly Tech";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit textEmail;
        private DevExpress.XtraEditors.TextEdit textPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.LinkLabel ForgotPasswordLink;
        private System.Windows.Forms.Label label5;
    }
}