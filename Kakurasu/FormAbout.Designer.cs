namespace Kakurasu
{
    partial class FormAbout
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
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.linkLabelOtherGames = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Location = new System.Drawing.Point(45, 25);
            this.linkLabelGithub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(273, 21);
            this.linkLabelGithub.TabIndex = 0;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "https://github.com/kdevzilla/kakurasu";
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // linkLabelOtherGames
            // 
            this.linkLabelOtherGames.AutoSize = true;
            this.linkLabelOtherGames.Location = new System.Drawing.Point(45, 65);
            this.linkLabelOtherGames.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelOtherGames.Name = "linkLabelOtherGames";
            this.linkLabelOtherGames.Size = new System.Drawing.Size(190, 21);
            this.linkLabelOtherGames.TabIndex = 1;
            this.linkLabelOtherGames.TabStop = true;
            this.linkLabelOtherGames.Text = "https://kdevzilla.github.io/";
            this.linkLabelOtherGames.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(289, 140);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 195);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.linkLabelOtherGames);
            this.Controls.Add(this.linkLabelGithub);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormAbout";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelGithub;
        private System.Windows.Forms.LinkLabel linkLabelOtherGames;
        private System.Windows.Forms.Button btnClose;
    }
}