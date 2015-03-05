using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;
using SQLiteClient;

namespace AgricolaCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        private DBmanager db;
 
        public MainPage()
        {
            InitializeComponent();
        }

        private void addGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddGame.xaml", UriKind.Relative));
        }

        private void gameHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/History.xaml", UriKind.Relative));
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            db = new DBmanager();
            string strSelect = "SELECT * FROM Games";
        }
    }
}