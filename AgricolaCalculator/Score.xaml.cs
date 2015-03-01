using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace AgricolaCalculator
{
    public partial class Score : PhoneApplicationPage
    {
        private Player player;
        private string selectedPlayer;
        private List<List<ScoreButton>> guiList;
        private List<ScoreButton> fieldstBtnList, pasturesBtnList, grainBtnList, vegetablesBtnList, sheepBtnList;

        public Score()
        {
            InitializeComponent();

            fieldstBtnList = new List<ScoreButton> { new ScoreButton(-1, fieldsBtn1), new ScoreButton(1, fieldsBtn2), new ScoreButton(2, fieldsBtn3), new ScoreButton(3, fieldsBtn4), new ScoreButton(4, fieldsBtn5) };
            pasturesBtnList = new List<ScoreButton> { new ScoreButton(-1, pasturesBtn1), new ScoreButton(1, pasturesBtn2), new ScoreButton(2, pasturesBtn3), new ScoreButton(3, pasturesBtn4), new ScoreButton(4, pasturesBtn5) };
            grainBtnList = new List<ScoreButton> { new ScoreButton(-1, grainBtn1), new ScoreButton(1, grainBtn2), new ScoreButton(2, grainBtn3), new ScoreButton(3, grainBtn4), new ScoreButton(4, grainBtn5) };
            vegetablesBtnList = new List<ScoreButton> { new ScoreButton(-1, vegetablesBtn1), new ScoreButton(1, vegetablesBtn2), new ScoreButton(2, vegetablesBtn3), new ScoreButton(3, vegetablesBtn4), new ScoreButton(4, vegetablesBtn5) };
            sheepBtnList = new List<ScoreButton> { new ScoreButton(-1, sheepBtn1), new ScoreButton(1, sheepBtn2), new ScoreButton(2, sheepBtn3), new ScoreButton(3, sheepBtn4), new ScoreButton(4, sheepBtn5) };

            guiList = new List<List<ScoreButton>> { fieldstBtnList, pasturesBtnList, grainBtnList, vegetablesBtnList, sheepBtnList };

            Loaded += (sender, e) =>
            {
                setupSelectedPlayer();
                setupGUI();

            };
        }

        private void setupSelectedPlayer()
        {
            player = StateManager.Get<Player>(selectedPlayer);
            playerNameTxt.Text = player.name;
        }

        private void countTotalScore()
        {
            player.score = player.pointsList.Sum();
            StateManager.Set<Player>(selectedPlayer, player);
        }

        private void setupGUI()
        {
            int y = 0;
            foreach (List<ScoreButton> list in guiList)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].points == player.pointsList[y])
                    {
                        list[i].btn.Background = new SolidColorBrush(Colors.Red);
                        break;
                    }
                }
                y++;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            selectedPlayer = NavigationContext.QueryString["player"];
        }

        private void playerNameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            player.name = playerNameTxt.Text;
            StateManager.Set<Player>(selectedPlayer, player);
        }

        private void fieldsBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton fieldBtn in fieldstBtnList)
            {
                if (fieldBtn.btn.IsPressed)
                {
                    //fieldsScore = fieldBtn.points;
                    player.pointsList[0] = fieldBtn.points;
                }
                else
                {
                    fieldBtn.btn.IsChecked = false;
                    fieldBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }

            countTotalScore();
        }

        private void pasturesBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton pastureBtn in pasturesBtnList)
            {
                if (pastureBtn.btn.IsPressed)
                {
                    player.pointsList[1] = pastureBtn.points;

                }
                else
                {
                    pastureBtn.btn.IsChecked = false;
                    pastureBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }

            countTotalScore();
        }

        private void grainBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton grainBtn in grainBtnList)
            {
                if (grainBtn.btn.IsPressed)
                {
                    player.pointsList[2] = grainBtn.points;

                }
                else
                {
                    grainBtn.btn.IsChecked = false;
                    grainBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }

            countTotalScore();
        }

        private void vegetablesBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton vegetableBtn in vegetablesBtnList)
            {
                if (vegetableBtn.btn.IsPressed)
                {
                    player.pointsList[3] = vegetableBtn.points;

                }
                else
                {
                    vegetableBtn.btn.IsChecked = false;
                    vegetableBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }

            countTotalScore();
        }

        private void sheepBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton sheepBtn in sheepBtnList)
            {
                if (sheepBtn.btn.IsPressed)
                {
                    player.pointsList[5] = sheepBtn.points;

                }
                else
                {
                    sheepBtn.btn.IsChecked = false;
                    sheepBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }

            countTotalScore();
        }

        class ScoreButton
        {
            public int points { get; set; }
            public ToggleButton btn { get; set; }

            public ScoreButton(int points, ToggleButton btn)
            {
                this.points = points;
                this.btn = btn;
            }
        }
    }
}