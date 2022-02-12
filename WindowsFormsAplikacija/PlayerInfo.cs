using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOPNET_PodatkovniSloj.Models;
using System.Threading;
using System.Globalization;
using System.IO;

namespace WindowsFormsAplikacija
{
    public partial class PlayerInfo : UserControl
    {
        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected; set
            {
                isSelected = value;
                if (value)
                {
                    this.BorderStyle = BorderStyle.Fixed3D;
                    this.BackColor = Color.Aquamarine;
                }
                else
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
                    this.BackColor = Color.Transparent;
                }
            }
        }
        public bool Favorite
        {
            get => imgStar.Visible;
            set => imgStar.Visible = value;
        }
        public string number
        {
            get => lblNumber.Text;
            private set => lblNumber.Text = value;
        }

        public Image slika
        {
            get => imgPictureOfPlayer.Image;
            set
            {
                imgPictureOfPlayer.Image = value;
            }
        }

        public StartingInformation IgracCijeli { get; set; }

        private void SetCulture(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

        }
        public PlayerInfo(StartingInformation s, string jezik)
        {
            SetCulture(jezik);
            InitializeComponent();

            IgracCijeli = s;
            this.number = s.ShirtNumber.ToString();
            lblName.Text = s.Name;
            lblCaptain.Text = (bool)s.Captain ? Properties.Resources.captainyes : Properties.Resources.captainno;

            string position = null;
            switch (s.Position.ToString())
            {
                case ("Goalie"):
                    position = Properties.Resources.goalie;
                    break;
                case ("Midfield"):
                    position = Properties.Resources.midfield;
                    break;
                case ("Forward"):
                    position = Properties.Resources.forward;
                    break;
                case ("Defender"):
                    position = Properties.Resources.defender;
                    break;
                default:
                    break;
            }
            lblPosition.Text = position;

            var files = Directory.GetFiles("../../../playerimage/", number + s.Name + ".*");
            if (files.Length > 0)
            {
                try
                {
                    slika = Image.FromFile(files[0]);
                }
                catch (Exception)
                {
                    slika = Properties.Resources.stockphoto;
                }
            }
            else
            {
                imgPictureOfPlayer.Image = Properties.Resources.stockphoto;
            }
        }

        private void PlayerInfo_Load(object sender, EventArgs e)
        {

        }

        private void PlayerInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                imgPictureOfPlayer.Image = new Bitmap(open.FileName);
            }

            var EXT = Path.GetExtension(open.FileName);
            imgPictureOfPlayer.Image.Save("../../../playerimage/" + lblNumber.Text + lblName.Text + EXT);
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {

            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }

        private void lblNumber_MouseDown(object sender, MouseEventArgs e)
        {

            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }

        private void lblPosition_MouseDown(object sender, MouseEventArgs e)
        {

            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }

        private void lblCaptain_MouseDown(object sender, MouseEventArgs e)
        {

            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }

        private void imgPictureOfPlayer_MouseDown(object sender, MouseEventArgs e)
        {

            this.DoDragDrop(this, DragDropEffects.Move);
            IsSelected = !isSelected;
        }
    }
 }

