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
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        public ShowWindow(CountryInfo country)
        {
            InitializeComponent();
            FillWindowWithData(country);
        }

        private void FillWindowWithData(CountryInfo country)
        {
            labelFifaCodeValue.Content = country.FifaCode;
            labelNameValue.Content = country.Country;
            labelGamesDrawValue.Content = country.Draws;
            labelGamesLostValue.Content = country.Losses;
            labelGamesPlayedValue.Content = country.GamesPlayed;
            labelGamesWinsValue.Content = country.Wins;
            labelGoalsConcededValue.Content = country.GoalsAgainst;
            labelGoalsDifferenceValue.Content = country.GoalDifferential;
            labelGoalsScoredValue.Content = country.GoalsFor;
        }
    }
}
