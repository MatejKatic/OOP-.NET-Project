using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using OOPNET_PodatkovniSloj.Repo;

namespace OOPNET_WindowsPresentationFoundation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private static readonly IRepo r = RepoFactory.GetRepository();
        public Championship prvenstvo { get; set; }

        public string Languages { get; set; }

        public string Resolution { get; set; }
        public MainWindow()
        {
            InitSettings();
            InitializeComponent();
            SetCulture(Languages);
            setResolution(Resolution);
            SetUpWindow();
        }

        private void setResolution(string resolution)
        {
            if (resolution == "Fullscreen")
            {
                WindowState = WindowState.Maximized;
                Resolution = resolution;
            }
            else
            {
                WindowState = WindowState.Normal;

                var reso = resolution.Split('x');
                Width = double.Parse(reso[0]);
                Height = double.Parse(reso[1]);
                Resolution = Width.ToString() + "x" + Height.ToString();
            }
        }

        private void InitSettings()
        {
           try
            {
                setLanguage(r.ProcitajPostavke()[0]);
                setChampionship(r.ProcitajPostavke()[1]);
                setResolution(r.ProcitajPostavke()[r.ProcitajPostavke().Length - 1]);


            }
            catch (Exception)
            {
                SettingsW s = new SettingsW(prvenstvo, Languages);

                if (s.ShowDialog() != true)
                {
                    Closing -= new System.ComponentModel.CancelEventHandler(this.Window_Closing);
                    this.Close();
                }

                setChampionship(s.GetChampionship());
                setLanguage(s.GetLanguage());
                setResolution(s.GetResolution());

            }
        }

        private void setChampionship(string v)
        {
            if (v == string.Empty) throw new Exception("Datoteka nije ispravno formatirana");
            if (v == "Musko")
            {
                prvenstvo = Championship.Musko;
            }
            else
            {
                prvenstvo = Championship.Zensko;
            }
        }

        private void setLanguage(string v)
        {
            if (v == string.Empty) throw new Exception("Datoteka nije ispravno formatirana");
            Languages = v;

            SetCulture(Languages);
        }

        private void SetCulture(string Culture)
        {
            CultureInfo culture = new CultureInfo(Culture);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }


        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            var selectedteam = cBoxFirst.SelectedItem as CountryInfo;
            ShowWindow showInfo = new ShowWindow(selectedteam);
            showInfo.ShowDialog();
        }

        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            var selectedteam = cBoxSecond.SelectedItem as CountryInfo;
            ShowWindow showInfo = new ShowWindow(selectedteam);
            showInfo.ShowDialog();

        }


        private void SetUpWindow()
        {
            cBoxFirst.SelectionChanged += new SelectionChangedEventHandler(cBoxFirstSelectionChanged);
            cBoxSecond.SelectionChanged += new SelectionChangedEventHandler(cBoxSecondSelectionChanged);
            btnFirst.Padding = btnSecond.Padding = new Thickness(6);
            var path = System.IO.Path.GetFullPath(@"..\..\..\");
            string file = Directory.EnumerateFiles(path, "resource/loading.gif").FirstOrDefault();
            loadingimg.Source = new BitmapImage(new Uri(file));

            footballpitchimg.ImageSource = new BitmapImage(new Uri(@path + "resource/footballpitch.jpg"));
        }

        async void cBoxSecondSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadingimg.Visibility = Visibility.Visible;

            var prva = cBoxFirst.SelectedItem as CountryInfo;
            var druga = cBoxSecond.SelectedItem as CountryInfo;
            if (druga is null) return;
            List<Match> matches = null;
            try
            {
                matches = await r.GetMatches(prva.FifaCode, prvenstvo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Closing -= new System.ComponentModel.CancelEventHandler(this.Window_Closing);
                this.Close();
            }

            var match = matches.Where(m => (m.AwayTeam.Code == prva.FifaCode && m.HomeTeam.Code == druga.FifaCode) || (m.HomeTeam.Code == prva.FifaCode && m.AwayTeam.Code == druga.FifaCode)).First();

            ClearStackPanels();
            if (match.HomeTeam.Code == prva.FifaCode)
            {
                labelFirstGoals.Content = match.HomeTeam.Goals;
                labelSecondGoals.Content = match.AwayTeam.Goals;

                FillTeren(match.HomeTeamStatistics.StartingInformation, match.AwayTeamStatistics.StartingInformation);

            }

            else
            {
                labelSecondGoals.Content = match.HomeTeam.Goals;
                labelFirstGoals.Content = match.AwayTeam.Goals;
                FillTeren(match.AwayTeamStatistics.StartingInformation, match.HomeTeamStatistics.StartingInformation);
            }
            loadingimg.Visibility = Visibility.Hidden;
        }

        private void FillTeren(List<StartingInformation> home, List<StartingInformation> away)
        {
            foreach (var igrac in home)
            {
                PlayerUserControl i = new PlayerUserControl(igrac);
                i.MouseDown += I_MouseDown;
                switch (igrac.Position.ToString())
                {
                    case ("Goalie"):
                        spGolmanFIRST.Children.Add(i);
                        break;
                    case ("Midfield"):
                        spVezniFIRST.Children.Add(i);
                        break;
                    case ("Forward"):
                        spONapadFIRST.Children.Add(i);
                        break;
                    case ("Defender"):
                        spObranaFIRST.Children.Add(i);
                        break;
                }

            }
            foreach (var igrac in away)
            {
                PlayerUserControl i = new PlayerUserControl(igrac);
                i.MouseDown += I_MouseDown;

                switch (igrac.Position.ToString())
                {
                    case ("Goalie"):
                        spGolmanSECOND.Children.Add(i);
                        break;
                    case ("Midfield"):
                        spVezniSECOND.Children.Add(i);
                        break;
                    case ("Forward"):
                        spONapadSECOND.Children.Add(i);
                        break;
                    case ("Defender"):
                        spObranaSECOND.Children.Add(i);
                        break;
                }

            }
        }

        private void I_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadingimg.Visibility = Visibility.Visible;

            PlayerUserControl clickedIgrac = sender as PlayerUserControl;
            Match match = null;
            try
            {

                match = r.GetMatches(((CountryInfo)cBoxFirst.SelectedItem).FifaCode, prvenstvo).Result.Where(m => m.HomeTeam.Code == ((CountryInfo)cBoxSecond.SelectedItem).FifaCode || m.AwayTeam.Code == ((CountryInfo)cBoxSecond.SelectedItem).FifaCode).First();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Closing -= new System.ComponentModel.CancelEventHandler(this.Window_Closing);
                this.Close();
            }

            PlayerInfo i = new PlayerInfo(clickedIgrac.Player, match);
            loadingimg.Visibility = Visibility.Hidden;

            i.Show();
        }

        private void ClearStackPanels()
        {
            spGolmanFIRST.Children.Clear();
            spObranaFIRST.Children.Clear();
            spVezniFIRST.Children.Clear();
            spONapadFIRST.Children.Clear();

            spGolmanSECOND.Children.Clear();
            spObranaSECOND.Children.Clear();
            spVezniSECOND.Children.Clear();
            spONapadSECOND.Children.Clear();
        }

        private void cBoxFirstSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var CountryInfo = cBoxFirst.SelectedItem as CountryInfo;
            if (CountryInfo is null) return;
            FillDdlWithDataOpponent(CountryInfo.FifaCode);
        }

        private void FillDdlWithDataOpponent(string fifaCode)
        {
            loadingimg.Visibility = Visibility.Visible;
            Task<List<Match>> matches = null;
            Task<List<CountryInfo>> drzave = null;
            cBoxSecond.Items.Clear();
            try
            {
                matches = r.GetMatches(fifaCode, prvenstvo);
                drzave = r.GetDrzave(prvenstvo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Closing -= new System.ComponentModel.CancelEventHandler(this.Window_Closing);
                this.Close();
            }
            foreach (var match in matches.Result)
            {
                if (match.AwayTeam.Code == fifaCode)
                {
                    cBoxSecond.Items.Add(drzave.Result.First(d => d.FifaCode == match.HomeTeam.Code));
                }
                else
                {
                    cBoxSecond.Items.Add(drzave.Result.First(d => d.FifaCode == match.AwayTeam.Code));
                }

            }
            cBoxSecond.SelectedIndex = 0;
            loadingimg.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NewDialog s = new NewDialog("", Properties.Resources.areyousure);

            if (s.ShowDialog() != true)
            {
                e.Cancel = true;
                return;
            }


            try
            {
                string[] podstavke = r.ProcitajPostavke();
                List<string> igraci = new List<string> { podstavke[3], podstavke[4], podstavke[5] };

                r.ZapisiPostavke(Languages, prvenstvo.ToString(), podstavke[2], igraci, Resolution);
            }
            catch (Exception)
            {
                r.ZapisiPostavke(Languages, prvenstvo.ToString(), null, null, Resolution);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsW s = new SettingsW(prvenstvo, Languages);
            if (s.ShowDialog() != true) return;

            setChampionship(s.GetChampionship());
            setLanguage(s.GetLanguage());
            setResolution(s.GetResolution());
            FillDdlWithData(string.Empty);
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string fifacodedrzave;
            try
            {
                fifacodedrzave = r.ProcitajPostavke()[2];
            }
            catch (Exception)
            {
                fifacodedrzave = String.Empty;
            }


            FillDdlWithData(fifacodedrzave);
        }

        private async void FillDdlWithData(string fifacodedrzave)
        {
            cBoxFirst.Items.Clear();
            loadingimg.Visibility = Visibility.Visible;

            try
            {
                var drzave = await r.GetDrzave(prvenstvo);


                foreach (var drzava in drzave)
                {
                    cBoxFirst.Items.Add(drzava);
                }
                CountryInfo favteam = drzave.First();

                if (fifacodedrzave != String.Empty)
                {
                    favteam = drzave.FirstOrDefault(d => d.FifaCode == fifacodedrzave);
                    if (favteam is null)
                    {
                        favteam = drzave.First();
                    }

                }
                cBoxFirst.SelectedItem = favteam;
                FillDdlWithDataOpponent(favteam.FifaCode);



            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Closing -= new System.ComponentModel.CancelEventHandler(this.Window_Closing);
                this.Close();
            }


            loadingimg.Visibility = Visibility.Hidden;
        }
    }
}
