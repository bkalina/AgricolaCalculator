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

namespace AgricolaCalculator
{
    public partial class AddGame : PhoneApplicationPage
    {
        private Player player1, player2, player3, player4, player5;

        public AddGame()
        {
            InitializeComponent();

            setupPlayers();

            Loaded += (s, e) =>
            {
                reloadPlayers();
                setupPlayersGUI();
            };
            
        }

        private void player1Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player1", UriKind.Relative));
        }

        private void player2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player2", UriKind.Relative));
        }

        private void player3Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player3", UriKind.Relative));
        }

        private void player4Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player4", UriKind.Relative));
        }

        private void player5Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Score.xaml?player=player5", UriKind.Relative));
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
    }
}