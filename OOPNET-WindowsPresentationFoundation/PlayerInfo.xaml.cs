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
using System.Windows.Shapes;
using OOPNET_PodatkovniSloj.Models;

namespace OOPNET_WindowsPresentationFoundation
{
    /// <summary>
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {
        public PlayerInfo(StartingInformation player, Match match)
        {
            InitializeComponent();
            FillWindowData(player, match);
        }

        private void FillWindowData(StartingInformation player, Match match)
        {
            setImage(player);

            var events = match.AwayTeamEvents.Concat(match.HomeTeamEvents).ToList();

            lblCaptainVALUE.Content = player.Captain.Value ? Properties.Resources.yes : Properties.Resources.no;
            lblGoalsThisGameVALUE.Content = events.Count(e => (e.Player == player.Name) && (e.TypeOfEvent == "goal"));
            lblNameVALUE.Content = player.Name;
            lblNumberVALUE.Content = player.ShirtNumber;
            lblPositionVALUE.Content = player.Position;
            lblYellowCardsThisGameVALUE.Content = events.Count(e => (e.Player == player.Name) && (e.TypeOfEvent == "yellow-card"));
        }

        private void setImage(StartingInformation player)
        {
            var path = System.IO.Path.GetFullPath(@"..\..\..\");

            try
            {
                string file = Directory.EnumerateFiles(path, "playerimage/" + player.ShirtNumber + player.Name + ".*").FirstOrDefault();
                imgPlayer.Source = new BitmapImage(new Uri(file));
            }
            catch (Exception)
            {
                imgPlayer.Source = new BitmapImage(new Uri(@path + "resource/stockphoto.png"));
            }
        }
    }
}
