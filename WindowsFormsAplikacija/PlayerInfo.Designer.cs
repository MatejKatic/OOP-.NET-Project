namespace WindowsFormsAplikacija
{
    partial class PlayerInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerInfo));
            this.lblName = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblCaptain = new System.Windows.Forms.Label();
            this.imgStar = new System.Windows.Forms.PictureBox();
            this.imgPictureOfPlayer = new System.Windows.Forms.PictureBox();
            this.btnSetImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgStar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPictureOfPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            this.lblName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseDown);
            // 
            // lblNumber
            // 
            resources.ApplyResources(this.lblNumber, "lblNumber");
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblNumber_MouseDown);
            // 
            // lblPosition
            // 
            resources.ApplyResources(this.lblPosition, "lblPosition");
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPosition_MouseDown);
            // 
            // lblCaptain
            // 
            resources.ApplyResources(this.lblCaptain, "lblCaptain");
            this.lblCaptain.Name = "lblCaptain";
            this.lblCaptain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblCaptain_MouseDown);
            // 
            // imgStar
            // 
            resources.ApplyResources(this.imgStar, "imgStar");
            this.imgStar.Image = global::WindowsFormsAplikacija.Properties.Resources.star;
            this.imgStar.Name = "imgStar";
            this.imgStar.TabStop = false;
            // 
            // imgPictureOfPlayer
            // 
            resources.ApplyResources(this.imgPictureOfPlayer, "imgPictureOfPlayer");
            this.imgPictureOfPlayer.Name = "imgPictureOfPlayer";
            this.imgPictureOfPlayer.TabStop = false;
            this.imgPictureOfPlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgPictureOfPlayer_MouseDown);
            // 
            // btnSetImage
            // 
            resources.ApplyResources(this.btnSetImage, "btnSetImage");
            this.btnSetImage.Name = "btnSetImage";
            this.btnSetImage.UseVisualStyleBackColor = true;
            this.btnSetImage.Click += new System.EventHandler(this.btnSetImage_Click);
            // 
            // PlayerInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnSetImage);
            this.Controls.Add(this.imgPictureOfPlayer);
            this.Controls.Add(this.imgStar);
            this.Controls.Add(this.lblCaptain);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblName);
            this.Name = "PlayerInfo";
            this.Load += new System.EventHandler(this.PlayerInfo_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerInfo_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgStar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPictureOfPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblCaptain;
        private System.Windows.Forms.PictureBox imgStar;
        private System.Windows.Forms.PictureBox imgPictureOfPlayer;
        private System.Windows.Forms.Button btnSetImage;
    }
}
