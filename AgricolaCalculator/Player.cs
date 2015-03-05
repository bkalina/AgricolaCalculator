using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AgricolaCalculator
{
    public class Player
    {
        public string name { get; set; }
        public int score { get; set; }
        public List<int> pointsList { get; set; }

        private int fields = -1;
        private int pastures = -1;
        private int grain = -1;
        private int vegetables = -1;
        private int sheep = -1;
        private int wildBoar = -1;
        private int cattle = -1;
        private int fencedStables = 0;
        private int roomType = 0;
        private int familyMembers = 6;
        private int beggingCards = 0;
        private int unusedSpaces = 0;
        private int rooms = 2;
        private int cardsPoints = 0;
        private int bonusPoints = 0;

        public Player()
        {

        }

        public Player(string name)
        {
            this.name = name;
            this.score = 0;

            pointsList = new List<int> { fields, pastures, grain, vegetables, sheep, wildBoar, cattle, fencedStables, roomType, familyMembers, beggingCards, unusedSpaces, rooms, cardsPoints, bonusPoints };
        }

    }
}
