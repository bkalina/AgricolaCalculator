using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgricolaCalculator
{
    public class Game
    {
        public string id { get; set; }
        public string gameDate { get; set; }
        public List<Player> playersList { get; set; }

        public Game()
        {

        }

        public Game(string id, string gameDate, List<Player> playersList)
        {
            this.id = id;
            this.gameDate = gameDate;
            this.playersList = playersList;
        }
    }
}
