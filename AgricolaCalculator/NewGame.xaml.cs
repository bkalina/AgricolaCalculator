using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AgricolaCalculator
{
    public partial class AddGame : PhoneApplicationPage
    {
        
        private string id;
        private List<Player> playersList;
        private Player player1, player2, player3, player4, player5;

        public AddGame()
        {
            InitializeComponent();

            setupPlayers();
            id = Guid.NewGuid().ToString();        

            Loaded += (s, e) =>
            {
                reloadPlayers();
                setupPlayersGUI();
            };
        }

        private void player1Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player1&editMode=edit", UriKind.Relative));
        }

        private void player2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player2&editMode=edit", UriKind.Relative));
        }

        private void player3Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player3&editMode=edit", UriKind.Relative));
        }

        private void player4Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player4&editMode=edit", UriKind.Relative));
        }

        private void player5Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player5&editMode=edit", UriKind.Relative));
        }

        private void setupPlayersGUI()
        {
            player1Btn.Content = player1.name;
            player1Score.Text = player1.score.ToString();
            player2Btn.Content = player2.name;
            player2Score.Text = player2.score.ToString();
            player3Btn.Content = player3.name;
            player3Score.Text = player3.score.ToString();
            player4Btn.Content = player4.name;
            player4Score.Text = player4.score.ToString();
            player5Btn.Content = player5.name;
            player5Score.Text = player5.score.ToString();
            playersList = new List<Player> { player1, player2, player3, player4, player5 };
        }

        private void reloadPlayers()
        {
            player1 = StateManager.Get<Player>("player1");
            player2 = StateManager.Get<Player>("player2");
            player3 = StateManager.Get<Player>("player3");
            player4 = StateManager.Get<Player>("player4");
            player5 = StateManager.Get<Player>("player5");
        }

        private void setupPlayers()
        {
            player1 = new Player("Player");
            StateManager.Set<Player>("player1", player1);
            player2 = new Player("Player");
            StateManager.Set<Player>("player2", player2);
            player3 = new Player("Player");
            StateManager.Set<Player>("player3", player3);
            player4 = new Player("Player");
            StateManager.Set<Player>("player4", player4);
            player5 = new Player("Player");
            StateManager.Set<Player>("player5", player5);
        }

        private void saveGame_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), playersList);
            (Application.Current as App).db.addGame(game);
            MessageBox.Show("Game saved \nNow you can update this game until exit");
            tileUpdate();
            saveGame.Content = "Update";
        }

        private void tileUpdate()
        {
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                StandardTileData data = new StandardTileData();
                data.Count = (Application.Current as App).db.gamesCount();
                tile.Update(data);
            }
        }
    }
}