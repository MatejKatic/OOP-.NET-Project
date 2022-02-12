using OOPNET_PodatkovniSloj.Models;
using OOPNET_PodatkovniSloj.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAplikacija
{
    public partial class SettingsF : Form
    {

        private Championship prvenstvo { get; set; }
        private string Language { get; set; }

        public SettingsF(Championship prevenstvo, string language)
        {
            SetCulture(language);
            InitializeComponent();
            this.prvenstvo = prevenstvo;
            this.Language = language;

            ddlChampionship.Items.Add(Championship.Musko);
            ddlChampionship.Items.Add(Championship.Zensko);
        }

        private void SetCulture(string cultureName)
        {
            if (String.IsNullOrEmpty(cultureName)) cultureName = "hr";
            CultureInfo culture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void SettingsF_Load(object sender, EventArgs e)
        {
            ddlChampionship.SelectedItem = prvenstvo;

            if (Language is null)
            {
                ddlLanguage.SelectedIndex = 0;
            }
            else
            {
                ddlLanguage.SelectedItem = Language;
            }
        }

        public string GetLanguage()
        {

            return ddlLanguage.SelectedItem.ToString();

        }
        public string GetChampionship()
        {

            return ddlChampionship.SelectedItem.ToString();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            NewDialog c = new NewDialog("", Properties.Resources.areyousure, ddlLanguage.SelectedItem.ToString());
            var result = c.ShowDialog();
            if (result != DialogResult.Yes) return;
            DialogResult = DialogResult.OK;
        }
    }
}
