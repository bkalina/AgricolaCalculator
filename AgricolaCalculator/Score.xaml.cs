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
        private int fieldsScore, pasturesScore, grainScore, vegetablesScore, sheepScore,
                    wildBoarScore, cattleScore, fencedStablesScore, unuseedSpacesScore,
                    roomsScore, roomTypeScore, familyMembersScore, bonusPointsScore, beggingCardsScore = 0;

        List<ScoreButton> fieldstBtnList;
        List<ScoreButton> pasturestBtnList;
        

        public Score()
        {
            InitializeComponent();

            fieldstBtnList = new List<ScoreButton> { new ScoreButton(-1, fieldsBtn1), new ScoreButton(2, fieldsBtn2), new ScoreButton(3, fieldsBtn3), new ScoreButton(4, fieldsBtn4), new ScoreButton(5, fieldsBtn5) };
            pasturestBtnList = new List<ScoreButton> { new ScoreButton(-1, pasturesBtn1), new ScoreButton(2, pasturesBtn2), new ScoreButton(3, pasturesBtn3), new ScoreButton(4, pasturesBtn4), new ScoreButton(5, pasturesBtn5) };

            Loaded += (s, e) =>
            {
                setupSelectedPlayer();
            };
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

        private void setupSelectedPlayer()
        {
            player = StateManager.Get<Player>(selectedPlayer);
            playerNameTxt.Text = player.name;
        }

        private void fieldsBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach(ScoreButton fieldBtn in fieldstBtnList)
            {
                if (fieldBtn.btn.IsPressed)
                {
                    fieldsScore = fieldBtn.points;
                }
                else
                {
                    fieldBtn.btn.IsChecked = false;
                }
            }
            
            countTotalScore();
        }

        private void pasturesBtn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ScoreButton pastureBtn in pasturestBtnList)
            {
                if (pastureBtn.btn.IsPressed)
                {
                    pasturesScore = pastureBtn.points;
                }
                else
                {
                    pastureBtn.btn.IsChecked = false;
                }
            }

            countTotalScore();
        }

        private void countTotalScore()
        {
            player.score = fieldsScore + pasturesScore + grainScore + vegetablesScore + sheepScore +
                    wildBoarScore + cattleScore + fencedStablesScore + unuseedSpacesScore +
                    roomsScore + roomTypeScore + familyMembersScore + bonusPointsScore + beggingCardsScore;
            StateManager.Set<Player>(selectedPlayer, player);
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