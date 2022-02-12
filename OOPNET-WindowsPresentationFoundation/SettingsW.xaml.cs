using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OOPNET_PodatkovniSloj.Models;

namespace OOPNET_WindowsPresentationFoundation
{
    /// <summary>
    /// Interaction logic for SettingsW.xaml
    /// </summary>
    public partial class SettingsW : Window
    {
        private Championship prvenstvo { get; set; }
        private string Jezik { get; set; }
        public SettingsW(Championship prvenstvo, string jezik)
        {
            InitializeComponent();
            Jezik = jezik;
            this.prvenstvo = prvenstvo;
            FillCboxesWithData();
        }

        private void FillCboxesWithData()
        {
            cbPrvenstvo.Items.Add(Championship.Musko);
            cbPrvenstvo.Items.Add(Championship.Zensko);
            cbJezik.Items.Add("HR");
            cbJezik.Items.Add("EN");
            cbResolution.Items.Add("Fullscreen");
            cbResolution.Items.Add("1280x720");
            cbResolution.Items.Add("800x800");
            cbResolution.Items.Add("1440x1080");
            cbResolution.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbPrvenstvo.SelectedItem = prvenstvo;

            if (Jezik is null)
            {
                cbJezik.SelectedIndex = 0;
            }
            else
            {
                cbJezik.SelectedItem = Jezik;
            }
        }

        public string GetLanguage()
        {
            return cbJezik.SelectedItem.ToString();
        }
        public string GetResolution()
        {
            return cbResolution.SelectedItem.ToString();
        }
        public string GetChampionship()
        {
            return cbPrvenstvo.SelectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewDialog s = new NewDialog("", Properties.Resources.areyousure);

            if (s.ShowDialog() == true)
                this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }



    }
}
