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
        private List<ScoreButton> fieldstBtnList, pasturesBtnList, grainBtnList, vegetablesBtnList, sheepBtnList, wildBoarBtnList, cattleBtnList, fencedStablesBtnList, roomTypeBtnList, familyMembersBtnList, beggingCardsBtnList;

        public Score()
        {
            InitializeComponent();

            fieldstBtnList = new List<ScoreButton> { new ScoreButton(-1, fieldsBtn1), new ScoreButton(1, fieldsBtn2), new ScoreButton(2, fieldsBtn3), new ScoreButton(3, fieldsBtn4), new ScoreButton(4, fieldsBtn5) };
            pasturesBtnList = new List<ScoreButton> { new ScoreButton(-1, pasturesBtn1), new ScoreButton(1, pasturesBtn2), new ScoreButton(2, pasturesBtn3), new ScoreButton(3, pasturesBtn4), new ScoreButton(4, pasturesBtn5) };
            grainBtnList = new List<ScoreButton> { new ScoreButton(-1, grainBtn1), new ScoreButton(1, grainBtn2), new ScoreButton(2, grainBtn3), new ScoreButton(3, grainBtn4), new ScoreButton(4, grainBtn5) };
            vegetablesBtnList = new List<ScoreButton> { new ScoreButton(-1, vegetablesBtn1), new ScoreButton(1, vegetablesBtn2), new ScoreButton(2, vegetablesBtn3), new ScoreButton(3, vegetablesBtn4), new ScoreButton(4, vegetablesBtn5) };
            sheepBtnList = new List<ScoreButton> { new ScoreButton(-1, sheepBtn1), new ScoreButton(1, sheepBtn2), new ScoreButton(2, sheepBtn3), new ScoreButton(3, sheepBtn4), new ScoreButton(4, sheepBtn5) };
            wildBoarBtnList = new List<ScoreButton> { new ScoreButton(-1, wildBoarBtn1), new ScoreButton(1, wildBoarBtn2), new ScoreButton(2, wildBoarBtn3), new ScoreButton(3, wildBoarBtn4), new ScoreButton(4, wildBoarBtn5) };
            cattleBtnList = new List<ScoreButton> { new ScoreButton(-1, cattleBtn1), new ScoreButton(1, cattleBtn2), new ScoreButton(2, cattleBtn3), new ScoreButton(3, cattleBtn4), new ScoreButton(4, cattleBtn5) };
            fencedStablesBtnList = new List<ScoreButton> { new ScoreButton(0, fencedStablesBtn1), new ScoreButton(1, fencedStablesBtn2), new ScoreButton(2, fencedStablesBtn3), new ScoreButton(3, fencedStablesBtn4), new ScoreButton(4, fencedStablesBtn5) };
            roomTypeBtnList = new List<ScoreButton> { new ScoreButton(0, roomTypeBtn1), new ScoreButton(1, roomTypeBtn2), new ScoreButton(2, roomTypeBtn3) };
            familyMembersBtnList = new List<ScoreButton> { new ScoreButton(6, familyMembersBtn1), new ScoreButton(9, familyMembersBtn2), new ScoreButton(12, familyMembersBtn3), new ScoreButton(15, familyMembersBtn4) };
            beggingCardsBtnList = new List<ScoreButton> { new ScoreButton(0, beggingCardsBtn1), new ScoreButton(-3, beggingCardsBtn2), new ScoreButton(-6, beggingCardsBtn3), new ScoreButton(-9, beggingCardsBtn4), new ScoreButton(-12, beggingCardsBtn5), new ScoreButton(-15, beggingCardsBtn6) };

            guiList = new List<List<ScoreButton>> { fieldstBtnList, pasturesBtnList, grainBtnList, vegetablesBtnList, sheepBtnList, wildBoarBtnList, cattleBtnList, fencedStablesBtnList, roomTypeBtnList, familyMembersBtnList, beggingCardsBtnList };

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
            int score = 0;
            // punkty podstawowe
            for (int i = 0; i <= 7; i++)
            {
                score += player.pointsList[i];
            }
            // rodzina
            score += player.pointsList[9];
            // karty żeb
            score += player.pointsList[10];
            // niewykorszystane pola
            score += (player.pointsList[11]*(-1));
            // pokoje * typ
            score += (player.pointsList[12] * player.pointsList[8]);
            // punkty z kart
            score += player.pointsList[13];
            // bonusy
            score += player.pointsList[14];

            player.score = score;

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
            unusedSpacesValue.Text = player.pointsList[11].ToString();
            roomsValue.Text = player.pointsList[12].ToString();
            cardsPointsValue.Text = player.pointsList[13].ToString();
            bonusPointsValue.Text = player.pointsList[14].ToString();
            countTotalScore();
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
                    player.pointsList[4] = sheepBtn.points;
                }
                else
                {
                    sheepBtn.btn.IsChecked = false;
                    sheepBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void wildBoarBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton wildBoarBtn in wildBoarBtnList)
            {
                if (wildBoarBtn.btn.IsPressed)
                {
                    player.pointsList[5] = wildBoarBtn.points;
                }
                else
                {
                    wildBoarBtn.btn.IsChecked = false;
                    wildBoarBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void cattleBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton cattleBtn in cattleBtnList)
            {
                if (cattleBtn.btn.IsPressed)
                {
                    player.pointsList[6] = cattleBtn.points;
                }
                else
                {
                    cattleBtn.btn.IsChecked = false;
                    cattleBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void fencedStablesBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton fencedStablesBtn in fencedStablesBtnList)
            {
                if (fencedStablesBtn.btn.IsPressed)
                {
                    player.pointsList[7] = fencedStablesBtn.points;
                }
                else
                {
                    fencedStablesBtn.btn.IsChecked = false;
                    fencedStablesBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void roomTypeBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton roomTypeBtn in roomTypeBtnList)
            {
                if (roomTypeBtn.btn.IsPressed)
                {
                    player.pointsList[8] = roomTypeBtn.points;
                }
                else
                {
                    roomTypeBtn.btn.IsChecked = false;
                    roomTypeBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void familyMembersBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton familyMemberBtn in familyMembersBtnList)
            {
                if (familyMemberBtn.btn.IsPressed)
                {
                    player.pointsList[9] = familyMemberBtn.points;
                }
                else
                {
                    familyMemberBtn.btn.IsChecked = false;
                    familyMemberBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void beggingCardsBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton beggingCardBtn in beggingCardsBtnList)
            {
                if (beggingCardBtn.btn.IsPressed)
                {
                    player.pointsList[10] = beggingCardBtn.points;
                }
                else
                {
                    beggingCardBtn.btn.IsChecked = false;
                    beggingCardBtn.btn.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            countTotalScore();
        }

        private void unusedSpacesPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            unusedSpacesValue.Text = (int.Parse(unusedSpacesValue.Text) + 1).ToString();
            player.pointsList[11] = int.Parse(unusedSpacesValue.Text);
        }

        private void unusedSpacesMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            unusedSpacesValue.Text = (int.Parse(unusedSpacesValue.Text) - 1).ToString();
            player.pointsList[11] = int.Parse(unusedSpacesValue.Text);
        }

        private void roomsPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            roomsValue.Text = (int.Parse(roomsValue.Text) + 1).ToString();
            player.pointsList[12] = int.Parse(roomsValue.Text);
        }

        private void roomsMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            roomsValue.Text = (int.Parse(roomsValue.Text) - 1).ToString();
            player.pointsList[12] = int.Parse(roomsValue.Text);
        }

        private void cardsPointsPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            cardsPointsValue.Text = (int.Parse(cardsPointsValue.Text) + 1).ToString();
            player.pointsList[13] = int.Parse(cardsPointsValue.Text);
        }

        private void cardsPointsMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            cardsPointsValue.Text = (int.Parse(cardsPointsValue.Text) - 1).ToString();
            player.pointsList[13] = int.Parse(cardsPointsValue.Text);
        }

        private void bonusPointsPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            bonusPointsValue.Text = (int.Parse(bonusPointsValue.Text) + 1).ToString();
            player.pointsList[14] = int.Parse(bonusPointsValue.Text);
        }

        private void bonusPointsMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            bonusPointsValue.Text = (int.Parse(bonusPointsValue.Text) - 1).ToString();
            player.pointsList[14] = int.Parse(bonusPointsValue.Text);
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