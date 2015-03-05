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
        private DBmanager _db;
        public DBmanager db
        {
            get
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                if (_db == null)
                    _db = new DBmanager(assem.FullName.Substring(0, assem.FullName.IndexOf(',')), "gamesDB");
                return _db;
            }
        }

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
            //ObservableCollection<string> 
            List<Game> gamesEntries = null;

            string strSelect = "SELECT * FROM Games ORDER BY ID ASC";
            gamesEntries = db.SelectList<Game>(strSelect); //SelectObservableCollection<string>(strSelect);
            foreach (Game game in gamesEntries)
            {
                Console.Write(game.id);
            }
        }
    }
}