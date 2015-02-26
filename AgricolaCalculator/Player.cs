using System;
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

        public int fields { get; set; }
        public int pastures { get; set; }
        public int grain { get; set; }
        public int vegetables { get; set; }
        public int sheep { get; set; }
        public int wildBoar { get; set; }
        public int cattle { get; set; }
        public int fencedStables { get; set; }
        public int unuseedSpaces { get; set; }
        public int rooms { get; set; }
        public int roomType { get; set; }
        public int familyMembers { get; set; }
        public int bonusPoints { get; set; }
        public int beggingCards { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.score = 0;

            this.fields = 0;
            this.pastures = 0;
            this.grain = 0;
            this.vegetables = 0;
            this.sheep = 0;
            this.wildBoar = 0;
            this.cattle = 0;
            this.fencedStables = 0;
            this.unuseedSpaces = 0;
            this.rooms = 0;
            this.roomType = 0;
            this.familyMembers = 0;
            this.bonusPoints = 0;
            this.beggingCards = 0;
        }

    }
}
