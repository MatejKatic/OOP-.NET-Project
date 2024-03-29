﻿using System;
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

namespace OOPNET_WindowsPresentationFoundation
{
    /// <summary>
    /// Interaction logic for NewDialog.xaml
    /// </summary>
    public partial class NewDialog : Window
    {
        public NewDialog(string caption, string text)
        {
            InitializeComponent();
            Title = caption;
            message.Content = text;
        }

        private void yes_btnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void no_btnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
