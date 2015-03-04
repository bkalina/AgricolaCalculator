using System;
using System.Windows;
using Microsoft.Phone.Controls;
using SQLiteClient;

namespace AgricolaCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public static SQLiteConnection mySQLiteDB = null;

        public MainPage()
        {
            InitializeComponent();
            if (mySQLiteDB == null)
            {
                mySQLiteDB = new SQLiteConnection("MyTestDB");
                mySQLiteDB.Open();
            }
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
            SQLiteCommand cmd = mySQLiteDB.CreateCommand("drop table Games");
            Console.Write(cmd.ExecuteNonQuery());
            cmd = mySQLiteDB.CreateCommand("create table Games (id text primary key, game_date text)");
            Console.Write(cmd.ExecuteNonQuery());
            string id;
            string gameDate;
            for (int j = 0; j < 10; j++)
            {
                id = Guid.NewGuid().ToString();
                gameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.CommandText = "insert into Games (id, game_date) values (\"" + id + "\",\"" + gameDate + "\")";
                cmd.ExecuteNonQuery();
            }

            cmd = mySQLiteDB.CreateCommand("select * from Games");
            Console.Write(cmd.ExecuteNonQuery());
            //SqliteDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
        }
    }
}