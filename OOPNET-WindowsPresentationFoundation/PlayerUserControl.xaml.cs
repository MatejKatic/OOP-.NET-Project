using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OOPNET_PodatkovniSloj.Models;

namespace OOPNET_WindowsPresentationFoundation
{
    /// <summary>
    /// Interaction logic for PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl : UserControl
    {
        public StartingInformation Player { get; private set; }
        public PlayerUserControl(StartingInformation player)
        {
            Player = player;
            InitializeComponent();

            FillUCWithData(player);
        }

        private void FillUCWithData(StartingInformation player)
        {
            var partsofname = Player.Name.Split(' ');
            lblImeBroj.Content = Player.ShirtNumber + " " + partsofname[partsofname.Length - 1];

            var path = System.IO.Path.GetFullPath(@"..\..\..\");

            try
            {
                string file = Directory.EnumerateFiles(path, "playerimage/" + Player.ShirtNumber + Player.Name + ".*").FirstOrDefault();
                imgPlayer.Source = new BitmapImage(new Uri(file));

            }
            catch (Exception)
            {
                imgPlayer.Source = new BitmapImage(new Uri(@path + "resource/stockphoto.png"));
            }
        }
    }
}
