namespace SaidalyTechMain.Forms.MainForms
{
    partial class FrmAddNewForms
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lpeFather = new DevExpress.XtraEditors.LookUpEdit();
            this.TextText = new System.Windows.Forms.TextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lpeType = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeFather.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 15);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 60);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Text";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(14, 154);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Father";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(14, 106);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 19);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Type";
            // 
            // lpeFather
            // 
            this.lpeFather.Location = new System.Drawing.Point(96, 151);
            this.lpeFather.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lpeFather.Name = "lpeFather";
            this.lpeFather.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lpeFather.Properties.Appearance.Options.UseFont = true;
            this.lpeFather.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpeFather.Size = new System.Drawing.Size(176, 26);
            this.lpeFather.TabIndex = 4;
            // 
            // TextText
            // 
            this.TextText.Font = new System.Drawing.Font("Tahoma", 12F);
            this.TextText.Location = new System.Drawing.Point(96, 57);
            this.TextText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextText.Name = "TextText";
            this.TextText.Size = new System.Drawing.Size(176, 27);
            this.TextText.TabIndex = 6;
            // 
            // textName
            // 
            this.textName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.textName.Location = new System.Drawing.Point(96, 11);
            this.textName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(176, 27);
            this.textName.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.button1.Location = new System.Drawing.Point(66, 219);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 41);
            this.button1.TabIndex = 8;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lpeType
            // 
            this.lpeType.Location = new System.Drawing.Point(96, 102);
            this.lpeType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lpeType.Name = "lpeType";
            this.lpeType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lpeType.Properties.Appearance.Options.UseFont = true;
            this.lpeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpeType.Size = new System.Drawing.Size(176, 26);
            this.lpeType.TabIndex = 9;
            // 
            // FrmAddNewForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 287);
            this.Controls.Add(this.lpeType);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.TextText);
            this.Controls.Add(this.lpeFather);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddNewForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddNewForms";
            this.Load += new System.EventHandler(this.FrmAddNewForms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lpeFather.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lpeFather;
        private System.Windows.Forms.TextBox TextText;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.LookUpEdit lpeType;
    }
}