using OOPNET_PodatkovniSloj;
using OOPNET_PodatkovniSloj.Models;
using OOPNET_PodatkovniSloj.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsAplikacija
{
    public partial class GlavnaForma : Form
    {


        private static readonly IRepo r = RepoFactory.GetRepository();


        public Championship Prvenstvo { get; set; }

        public string Jezik { get; set; }
        public GlavnaForma()
        {
            InitSetting();
            InitializeComponent();
            InitFlowLayoutPanels();
        }


        private void InitSetting()
        {
            try
            {
                setLanguage(r.ProcitajPostavke()[0]);
                setChampionship(r.ProcitajPostavke()[1]);
            }
            catch (Exception)
            {
                SettingsF s = new SettingsF(Prvenstvo, Jezik);

                if (s.ShowDialog() != DialogResult.OK)
                {
                    this.FormClosing -= new FormClosingEventHandler(this.GlavnaForma_FormClosing);
                    this.Close();
                }

                setChampionship(s.GetChampionship());
                setLanguage(s.GetLanguage());
            }
        }

        private void InitFlowLayoutPanels()
        {
            flpPlayers.AllowDrop = true;
            flpFavoritePlayers.AllowDrop = true;

            flpPlayers.DragEnter += panel_DragEnter;
            flpFavoritePlayers.DragEnter += panel_DragEnter;

            flpPlayers.DragDrop += panel_DragDrop;
            flpFavoritePlayers.DragDrop += panel_DragDrop;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            PlayerInfo item = (PlayerInfo)e.Data.GetData(typeof(PlayerInfo));
            if ((FlowLayoutPanel)item.Parent == (FlowLayoutPanel)sender) return;
            if ( flpFavoritePlayers.Controls.Count > 2 && (FlowLayoutPanel)sender ==  flpFavoritePlayers) return;



            List<PlayerInfo> selectedUCS;

            if ((FlowLayoutPanel)item.Parent == flpPlayers)
            {
                selectedUCS = flpPlayers.Controls.OfType<PlayerInfo>().Cast<PlayerInfo>().Where(c => c.IsSelected).ToList();
                if (selectedUCS.Count +  flpFavoritePlayers.Controls.Count > 3) return;
            }
            else
            {
                selectedUCS =  flpFavoritePlayers.Controls.OfType<PlayerInfo>().Cast<PlayerInfo>().Where(c => c.IsSelected).ToList();
            }

            if (selectedUCS.Count == 0)
            {
                var t = (PlayerInfo)e.Data.GetData(typeof(PlayerInfo)) as PlayerInfo;
                (t).Parent = (FlowLayoutPanel)sender; t.IsSelected = false;
            }

            foreach (var i in selectedUCS)
            {
                i.Favorite = !i.Favorite;
                (i).Parent = (FlowLayoutPanel)sender;
                i.IsSelected = false;
            }
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void setChampionship(string v)
        {
            if (v == string.Empty) throw new Exception("Datoteka nije ispravno formatirana");
            if (v == "Musko")
            {
                Prvenstvo = Championship.Musko;
            }
            else
            {
                Prvenstvo = Championship.Zensko;
            }
        }

        private void setLanguage(string v)
        {
            if (v == string.Empty) throw new Exception("Datoteka nije ispravno formatirana");
            Jezik = v;
            SetCulture(Jezik);
        }

        private void SetCulture(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void GlavnaForma_FormClosing(object sender, FormClosingEventArgs e)
        {
            NewDialog c = new NewDialog("", Properties.Resources.areyousure, Jezik);
            var result = c.ShowDialog();

            e.Cancel = (result != DialogResult.Yes);

            if (result != DialogResult.Yes) return;
            var selected = ddlCountries.SelectedItem as CountryInfo;

            var players = flpFavoritePlayers.Controls.OfType<PlayerInfo>().Cast<PlayerInfo>().Select(i => i.number).ToList();
            try
            {
                var rez = r.ProcitajPostavke()[r.ProcitajPostavke().Length - 1];
                r.ZapisiPostavke(Jezik, Prvenstvo.ToString(), selected.FifaCode, players, rez);
            }
            catch (Exception)
            {

                r.ZapisiPostavke(Jezik, Prvenstvo.ToString(), selected.FifaCode, players, null);
            }
        }

        private void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selecteddrzava = ddlCountries.SelectedItem as CountryInfo;
            FillListWithPlayerData(selecteddrzava.FifaCode);
            FillDataGridView();

        }

        private async void FillDataGridView()
        {
            loadingImage.Visible = true;
            ClearGridViews();
            var selecteddrzava = ddlCountries.SelectedItem as CountryInfo;
            List<Match> matches = null;
            try
            {
                matches = await r.GetMatches(selecteddrzava.FifaCode, Prvenstvo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.FormClosing -= new FormClosingEventHandler(this.GlavnaForma_FormClosing);
                this.Close();
            }

            List<TeamEvent> eventsZutiKartonisvi = new List<TeamEvent>();
            List<TeamEvent> eventsGoloviisvi = new List<TeamEvent>();
            foreach (var match in matches)
            {
                var eventss = match.AwayTeam.Code == selecteddrzava.FifaCode ? match.AwayTeamEvents : match.HomeTeamEvents;

                foreach (var eventt in eventss)
                {
                    if (eventt.TypeOfEvent == "yellow-card" || eventt.TypeOfEvent == "yellow-card-second") eventsZutiKartonisvi.Add(eventt);
                    if (eventt.TypeOfEvent == "goal" || eventt.TypeOfEvent == "goal-penalty") eventsGoloviisvi.Add(eventt);
                }
            }

            Dictionary<PlayerInfo, int> igracKarton = new Dictionary<PlayerInfo, int>();
            Dictionary<PlayerInfo, int> igracGol = new Dictionary<PlayerInfo, int>();


            var igraci = flpPlayers.Controls.OfType<PlayerInfo>().Cast<PlayerInfo>().ToList();
            var igraciomiljeni = flpFavoritePlayers.Controls.OfType<PlayerInfo>().Cast<PlayerInfo>().ToList();

            var sviIgraci = igraci.Concat(igraciomiljeni).ToList();


            foreach (var igrac in sviIgraci)
            {
                igracKarton.Add(igrac, eventsZutiKartonisvi.Where(x => x.Player == igrac.IgracCijeli.Name).Count());
                igracGol.Add(igrac, eventsGoloviisvi.Where(x => x.Player == igrac.IgracCijeli.Name).Count());
            }


            var orderedZuti = igracKarton.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            var orderedGolovi = igracGol.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            SetUpDataGrids();

            foreach (var igrac in orderedZuti)
            {
                dataGridView1.Rows.Add(igrac.Key.IgracCijeli.Name, igrac.Key.slika, igrac.Value);
            }

            foreach (var igrac in orderedGolovi)
            {
                dataGridView2.Rows.Add(igrac.Key.IgracCijeli.Name, igrac.Key.slika, igrac.Value);
            }

            foreach (var match in matches)
            {
                dataGridView3.Rows.Add(match.Location, match.Attendance, match.HomeTeam.Country, match.AwayTeam.Country);
            }

            ResizeDataView(dataGridView1);
            ResizeDataView(dataGridView2);
            ResizeDataView(dataGridView3);

            loadingImage.Visible = false;
        }

        private void ResizeDataView(DataGridView d)
        {
            d.Height = d.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + d.ColumnHeadersHeight;
            d.Width = d.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
        }

        private void SetUpDataGrids()
        {
            DataGridViewImageColumn imagecolumn = new DataGridViewImageColumn();
            imagecolumn.HeaderText = Properties.Resources.image;
            imagecolumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            DataGridViewTextBoxColumn namecolumn = new DataGridViewTextBoxColumn();
            namecolumn.HeaderText = Properties.Resources.name;

            DataGridViewTextBoxColumn value = new DataGridViewTextBoxColumn();
            value.HeaderText = Properties.Resources.yellowcardsnumber;

            DataGridViewImageColumn imagecolumn2 = new DataGridViewImageColumn();
            imagecolumn2.HeaderText = Properties.Resources.image;
            imagecolumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;

            DataGridViewTextBoxColumn namecolumn2 = new DataGridViewTextBoxColumn();
            namecolumn2.HeaderText = Properties.Resources.name;

            DataGridViewTextBoxColumn value2 = new DataGridViewTextBoxColumn();
            value2.HeaderText = Properties.Resources.goalsnumber;

            DataGridViewTextBoxColumn lokacija = new DataGridViewTextBoxColumn();
            lokacija.HeaderText = Properties.Resources.location;

            DataGridViewTextBoxColumn brojposjetitelja = new DataGridViewTextBoxColumn();
            brojposjetitelja.HeaderText = Properties.Resources.attendance;

            DataGridViewTextBoxColumn domacin = new DataGridViewTextBoxColumn();
            domacin.HeaderText = Properties.Resources.hometeam;

            DataGridViewTextBoxColumn gost = new DataGridViewTextBoxColumn();
            gost.HeaderText = Properties.Resources.awayteam;


            dataGridView1.Columns.Add(namecolumn);
            dataGridView1.Columns.Add(imagecolumn);
            dataGridView1.Columns.Add(value);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 30;

            dataGridView2.Columns.Add(namecolumn2);
            dataGridView2.Columns.Add(imagecolumn2);
            dataGridView2.Columns.Add(value2);

            dataGridView3.Columns.Add(lokacija);
            dataGridView3.Columns.Add(brojposjetitelja);
            dataGridView3.Columns.Add(domacin);
            dataGridView3.Columns.Add(gost);
        }

        private void ClearGridViews()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView1.Columns.Clear();


            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView2.Columns.Clear();

            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            dataGridView3.Columns.Clear();
        }




        private async void FillListWithPlayerData(string fifaCode)
        {
            loadingImage.Visible = true;
            flpPlayers.Controls.Clear();
             flpFavoritePlayers.Controls.Clear();
            List<Match> matches = null;
            try
            {
                matches = await r.GetMatches(fifaCode, Prvenstvo);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.FormClosing -= new FormClosingEventHandler(this.GlavnaForma_FormClosing);
                this.Close();
            }

            var prviMatch = matches.First();

            List<StartingInformation> starting11;
            List<StartingInformation> subs;
            if (prviMatch.AwayTeam.Code == fifaCode)
            {
                starting11 = prviMatch.AwayTeamStatistics.StartingInformation;
                subs = prviMatch.AwayTeamStatistics.Substitutes;
            }
            else
            {
                starting11 = prviMatch.HomeTeamStatistics.StartingInformation;
                subs = prviMatch.HomeTeamStatistics.Substitutes;

            }
            var sviIgraci = starting11.Concat(subs).ToList();

            foreach (var s in sviIgraci)
            {
                PlayerInfo i = new PlayerInfo(s, Jezik);
                try
                {

                    if (r.ProcitajPostavke().Contains(s.ShirtNumber.ToString()) && r.ProcitajPostavke()[2] == fifaCode)
                    {
                        i.Favorite = true;
                          flpFavoritePlayers.Controls.Add(i);
                    }
                    else
                    {
                        flpPlayers.Controls.Add(i);
                    }
                }
                catch (Exception)
                {
                    flpPlayers.Controls.Add(i);
                }
            }
            loadingImage.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

            SettingsF s = new SettingsF(Prvenstvo, Jezik);
            if (s.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var test = s.GetChampionship();
            setChampionship(s.GetChampionship());
            setLanguage(s.GetLanguage());

            FillDdlWithData(String.Empty);
        }

        private async void FillDdlWithData(string fifacod)
        {
            ddlCountries.Items.Clear();
            loadingImage.Visible = true;

            try
            {
                var drzave = await r.GetDrzave(Prvenstvo);

                foreach (var drzava in drzave)
                {
                    ddlCountries.Items.Add(drzava);
                }
                CountryInfo favteam = drzave.First();
                if (fifacod != String.Empty)
                {
                    favteam = drzave.FirstOrDefault(d => d.FifaCode == fifacod);
                }

                ddlCountries.SelectedItem = favteam;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            loadingImage.Visible = false;
        }

        private int page = 0;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            page = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void GlavnaForma_Load(object sender, EventArgs e)
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

            try
            {
                FillDdlWithData(fifacodedrzave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.FormClosing -= new FormClosingEventHandler(this.GlavnaForma_FormClosing);
                this.Close();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm;

            switch (page)
            {

                case 1:
                    bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                    dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
                    e.Graphics.DrawString(Properties.Resources.ranglistyellowcardsmessage, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(100, 50));
                    e.Graphics.DrawImage(bm, 100, 100);

                    e.HasMorePages = true;
                    break;

                case 2:
                    bm = new Bitmap(this.dataGridView2.Width, this.dataGridView2.Height);
                    dataGridView2.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView2.Width, this.dataGridView2.Height));
                    e.Graphics.DrawString(Properties.Resources.ranglistgoalsmessage, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(100, 50));
                    e.Graphics.DrawImage(bm, 100, 100);

                    e.HasMorePages = true;

                    break;

                case 3:
                    bm = new Bitmap(this.dataGridView3.Width, this.dataGridView3.Height);
                    dataGridView3.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView3.Width, this.dataGridView3.Height));
                    e.Graphics.DrawString(Properties.Resources.matchranglistmessage, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(100, 50));
                    e.Graphics.DrawImage(bm, 100, 100);

                    e.HasMorePages = false;

                    break;
            }
            page++;
        }
    }
}
