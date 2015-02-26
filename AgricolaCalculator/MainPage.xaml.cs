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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
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

        }
    }
}