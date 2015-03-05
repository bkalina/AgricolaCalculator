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
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using Community.CsharpSqlite.SQLiteClient;

namespace AgricolaCalculator
{

    public class DBmanager
    {
        private String dbName = "gamesDB.sqlite";
        private SqliteConnection db = null;

        public DBmanager()
        {
            //Open();
            //createDB();
            //Close();
        }

        ~DBmanager()
        {
            Close();
        }

        private void Open()
        {
            if (db == null)
            {
                db = new SqliteConnection("Version=3,uri=file:gamesDB.sqlite");
                db.Open();
            }
        }

        private void Close()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        public void addGame(Game game)
        {
            Open();
            SqliteCommand cmd;
            // TODO sprawdzanie czy istnieje gra o takim id

            string gameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string cmdStr =
                            "insert into Games " +
                            "(" +
                            "id, gameDate, " +
                            "p1name, p1score, p1fields, p1pastures, p1grain, p1vegetables, p1sheep, p1wildBoar, p1cattle, p1fencedStables, p1roomType, p1familyMembers, p1beggingCards, p1unusedSpaces, p1rooms, p1cardsPoints, p1bonusPoints, " +
                            "p2name, p2score, p2fields, p2pastures, p2grain, p2vegetables, p2sheep, p2wildBoar, p2cattle, p2fencedStables, p2roomType, p2familyMembers, p2beggingCards, p2unusedSpaces, p2rooms, p2cardsPoints, p2bonusPoints, " +
                            "p3name, p3score, p3fields, p3pastures, p3grain, p3vegetables, p3sheep, p3wildBoar, p3cattle, p3fencedStables, p3roomType, p3familyMembers, p3beggingCards, p3unusedSpaces, p3rooms, p3cardsPoints, p3bonusPoints, " +
                            "p4name, p4score, p4fields, p4pastures, p4grain, p4vegetables, p4sheep, p4wildBoar, p4cattle, p4fencedStables, p4roomType, p4familyMembers, p4beggingCards, p4unusedSpaces, p4rooms, p4cardsPoints, p4bonusPoints, " +
                            "p5name, p5score, p5fields, p5pastures, p5grain, p5vegetables, p5sheep, p5wildBoar, p5cattle, p5fencedStables, p5roomType, p5familyMembers, p5beggingCards, p5unusedSpaces, p5rooms, p5cardsPoints, p5bonusPoints, " +
                            "flaga)" +
                            " values (\"" + game.id + "\", \"" + game.gameDate + "\", \"";
            foreach (Player p in game.playersList)
            {
                cmdStr += (p.name + "\", \"" + p.score + "\", \"");
                for (int i = 0; i < p.pointsList.Count; i++)
                {
                    cmdStr += (p.pointsList[i] + "\", \"");
                }
            }
            cmdStr += "true\");";
            cmd = db.CreateCommand();
            cmd.CommandText = cmdStr;
            cmd.ExecuteNonQuery();
            Close();
        }

        public void readGames()
        {
            Open();
            // TODO pobrać liste gier
            Close();
        }

        public List<Player> getGame(string id)
        {
            Open();
            List<Player> playersList = new List<Player>();
            // TODO pobrac wpis o podanym id i stworzyc liste
            Close();
            return playersList;
        }

        private void createDB()
        {
            SqliteCommand cmd;
            //cmd = db.CreateCommand("drop table if exists Games");
            //Console.Write(cmd.ExecuteNonQuery());
            cmd = db.CreateCommand();
            cmd.CommandText = "create table if not exists Games " +
                                    "(id text primary key, gameDate text, " +
                                    "p1name text, p1score text, p1fields text, p1pastures text, p1grain text, p1vegetables text, p1sheep text, p1wildBoar text, p1cattle text, p1fencedStables text, p1roomType text, p1familyMembers text, p1beggingCards text, p1unusedSpaces text, p1rooms text, p1cardsPoints text, p1bonusPoints text, " +
                                    "p2name text, p2score text, p2fields text, p2pastures text, p2grain text, p2vegetables text, p2sheep text, p2wildBoar text, p2cattle text, p2fencedStables text, p2roomType text, p2familyMembers text, p2beggingCards text, p2unusedSpaces text, p2rooms text, p2cardsPoints text, p2bonusPoints text, " +
                                    "p3name text, p3score text, p3fields text, p3pastures text, p3grain text, p3vegetables text, p3sheep text, p3wildBoar text, p3cattle text, p3fencedStables text, p3roomType text, p3familyMembers text, p3beggingCards text, p3unusedSpaces text, p3rooms text, p3cardsPoints text, p3bonusPoints text, " +
                                    "p4name text, p4score text, p4fields text, p4pastures text, p4grain text, p4vegetables text, p4sheep text, p4wildBoar text, p4cattle text, p4fencedStables text, p4roomType text, p4familyMembers text, p4beggingCards text, p4unusedSpaces text, p4rooms text, p4cardsPoints text, p4bonusPoints text, " +
                                    "p5name text, p5score text, p5fields text, p5pastures text, p5grain text, p5vegetables text, p5sheep text, p5wildBoar text, p5cattle text, p5fencedStables text, p5roomType text, p5familyMembers text, p5beggingCards text, p5unusedSpaces text, p5rooms text, p5cardsPoints text, p5bonusPoints text, " +
                                    "flaga text)";
            Console.Write(cmd.ExecuteNonQuery());
        }
    }
}
