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
    public partial class NewDialog : Form
    {
        public NewDialog(string caption, string text, string language)
        {
            SetCulture(language);
            InitializeComponent();
            this.Text = caption;
            this.lblMessage.Text = text;
        }

        private void SetCulture(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void NewDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
