using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;

namespace AgricolaCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void addGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewGame.xaml", UriKind.Relative));
        }

        private void gameHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/History.xaml", UriKind.Relative));
        }
    }
}