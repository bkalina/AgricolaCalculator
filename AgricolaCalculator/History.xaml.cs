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
    public partial class History : PhoneApplicationPage
    {
        public History()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<Game> gamesList = (Application.Current as App).db.readGames();
            listBoxobj.ItemsSource = gamesList;
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listBoxobj.SelectedIndex != -1)
            {
                Game listitem = listBoxobj.SelectedItem as Game;
                NavigationService.Navigate(new Uri("/HistoryDetails.xaml?id=" + listitem.id, UriKind.Relative));
            }
        }
    }
}