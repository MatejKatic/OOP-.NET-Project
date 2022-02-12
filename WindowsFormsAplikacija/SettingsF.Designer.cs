namespace WindowsFormsAplikacija
{
    partial class SettingsF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsF));
            this.label1 = new System.Windows.Forms.Label();
            this.ddlChampionship = new System.Windows.Forms.ComboBox();
            this.ddlLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ddlChampionship
            // 
            resources.ApplyResources(this.ddlChampionship, "ddlChampionship");
            this.ddlChampionship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlChampionship.FormattingEnabled = true;
            this.ddlChampionship.Name = "ddlChampionship";
            // 
            // ddlLanguage
            // 
            resources.ApplyResources(this.ddlLanguage, "ddlLanguage");
            this.ddlLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLanguage.FormattingEnabled = true;
            this.ddlLanguage.Items.AddRange(new object[] {
            resources.GetString("ddlLanguage.Items"),
            resources.GetString("ddlLanguage.Items1")});
            this.ddlLanguage.Name = "ddlLanguage";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnConfirm
            // 
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // SettingsF
            // 
            this.AcceptButton = this.btnConfirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.ddlLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlChampionship);
            this.Controls.Add(this.label1);
            this.Name = "SettingsF";
            this.Load += new System.EventHandler(this.SettingsF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlChampionship;
        private System.Windows.Forms.ComboBox ddlLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
    }
}