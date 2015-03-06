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
using System.IO;
using System.Reflection;

namespace AgricolaCalculator
{

    public class DBmanager
    {
        private String dbName = "gamesDB.sqlite";
        private SqliteConnection db = null;
        private SqliteCommand cmd;

        public DBmanager()
        {
            // Załadowanie pliku bazy danych do IsolatedStorage jeśli jeszcze go tam nie ma
            Assembly assem = Assembly.GetExecutingAssembly();
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.FileExists(dbName))
            {
                CopyFromContentToStorage(assem.FullName.Substring(0, assem.FullName.IndexOf(',')), dbName);
            }
        }

        // Otwarcie połączenia
        private void Open()
        {
            if (db == null)
            {
                db = new SqliteConnection("Version=3,uri=file:gamesDB.sqlite");
                cmd = db.CreateCommand();
                db.Open();
            }
        }

        // Zamknięcie połączenia
        private void Close()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        // Dodanie gry do lokalnej bazy danych / edycja istniejącej gry
        public void addGame(Game game)
        {
            Open();
            cmd.CommandText = "SELECT count(*) FROM Games where id = '" + game.id + "'";

            string gameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string cmdStr = null;

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                // brak gry o takim ID
                if (reader.GetInt32(0) == 0)
                {
                    cmdStr =
                            "insert into Games " +
                            "(" +
                            "id, gameDate, " +
                            "p1name, p1score, p1fields, p1pastures, p1grain, p1vegetables, p1sheep, p1wildBoar, p1cattle, p1fencedStables, p1roomType, p1familyMembers, p1beggingCards, p1unusedSpaces, p1rooms, p1cardsPoints, p1bonusPoints, " +
                            "p2name, p2score, p2fields, p2pastures, p2grain, p2vegetables, p2sheep, p2wildBoar, p2cattle, p2fencedStables, p2roomType, p2familyMembers, p2beggingCards, p2unusedSpaces, p2rooms, p2cardsPoints, p2bonusPoints, " +
                            "p3name, p3score, p3fields, p3pastures, p3grain, p3vegetables, p3sheep, p3wildBoar, p3cattle, p3fencedStables, p3roomType, p3familyMembers, p3beggingCards, p3unusedSpaces, p3rooms, p3cardsPoints, p3bonusPoints, " +
                            "p4name, p4score, p4fields, p4pastures, p4grain, p4vegetables, p4sheep, p4wildBoar, p4cattle, p4fencedStables, p4roomType, p4familyMembers, p4beggingCards, p4unusedSpaces, p4rooms, p4cardsPoints, p4bonusPoints, " +
                            "p5name, p5score, p5fields, p5pastures, p5grain, p5vegetables, p5sheep, p5wildBoar, p5cattle, p5fencedStables, p5roomType, p5familyMembers, p5beggingCards, p5unusedSpaces, p5rooms, p5cardsPoints, p5bonusPoints, " +
                            "flaga)" +
                            " values ('" + game.id + "', '" + game.gameDate + "', '";
                    foreach (Player p in game.playersList)
                    {
                        cmdStr += (p.name + "', '" + p.score + "', '");
                        for (int i = 0; i < p.pointsList.Count; i++)
                        {
                            cmdStr += (p.pointsList[i] + "', '");
                        }
                    }
                    cmdStr += "true');";
                }
                // gra o podanym ID istnieje zatem robimy UPDATE
                else
                {
                    cmdStr = "update Games SET ";
                    List<string> points = new List<string> { "fields", "pastures", "grain", "vegetables", "sheep", "wildBoar", "cattle", "fencedStables", "roomType", "familyMembers", "beggingCards", "unusedSpaces", "rooms", "cardsPoints", "bonusPoints" };
                    int pNo = 1;
                    foreach (Player p in game.playersList)
                    {
                        cmdStr += ("p" + pNo + "name = '" + p.name + "', " + "p" + pNo + "score = '" + p.score + "', ");


                        for (int i = 0; i < p.pointsList.Count; i++)
                        {
                            cmdStr += ("p" + pNo + points[i] + " = '" + p.pointsList[i] + "', ");
                        }
                        pNo++;
                    }
                    cmdStr += "flaga = 'true' where id = '" + game.id + "'";
                }
            }
            cmd.Transaction = db.BeginTransaction();
            cmd.CommandText = cmdStr;
            cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();
            Close();
        }

        // Pobranie gry z lokalnej bazy danych
        public Game readGame(string id)
        {
            Open();
            Game game = null;
            List<Player> playersList = new List<Player>();
            cmd.CommandText = "SELECT * FROM Games where id = '" + id + "'";
            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("ID " + reader.GetString(0));
                    System.Diagnostics.Debug.WriteLine("DATA " + reader.GetString(1));

                    System.Diagnostics.Debug.WriteLine("p1name " + reader.GetString(2));
                    System.Diagnostics.Debug.WriteLine("p1score " + reader.GetString(3));
                    Player p1;
                    if (reader.GetString(3).Equals("0"))
                    {
                        p1 = null;
                    }
                    else
                    {
                        p1 = new Player(reader.GetString(2), reader.GetInt32(3),
                            new List<int> { reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18) });
                        playersList.Add(p1);
                    }

                    System.Diagnostics.Debug.WriteLine("p2name " + reader.GetString(19));
                    System.Diagnostics.Debug.WriteLine("p2score " + reader.GetString(20));
                    Player p2;
                    if (reader.GetString(20).Equals("0"))
                    {
                        p2 = null;
                    }
                    else
                    {
                        p2 = new Player(reader.GetString(19), reader.GetInt32(20),
                            new List<int> { reader.GetInt32(21), reader.GetInt32(22), reader.GetInt32(23), reader.GetInt32(24), reader.GetInt32(25), reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30), reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33), reader.GetInt32(34), reader.GetInt32(35) });
                        playersList.Add(p2);
                    }

                    System.Diagnostics.Debug.WriteLine("p3name " + reader.GetString(36));
                    System.Diagnostics.Debug.WriteLine("p3score " + reader.GetString(37));
                    Player p3;
                    if (reader.GetString(37).Equals("0"))
                    {
                        p3 = null;
                    }
                    else
                    {
                        p3 = new Player(reader.GetString(36), reader.GetInt32(37),
                            new List<int> { reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42), reader.GetInt32(43), reader.GetInt32(44), reader.GetInt32(45), reader.GetInt32(46), reader.GetInt32(47), reader.GetInt32(48), reader.GetInt32(49), reader.GetInt32(50), reader.GetInt32(51), reader.GetInt32(52) });
                        playersList.Add(p3);
                    }

                    System.Diagnostics.Debug.WriteLine("p4name " + reader.GetString(53));
                    System.Diagnostics.Debug.WriteLine("p4score " + reader.GetString(54));
                    Player p4;
                    if (reader.GetString(54).Equals("0"))
                    {
                        p4 = null;
                    }
                    else
                    {
                        p4 = new Player(reader.GetString(53), reader.GetInt32(54),
                            new List<int> { reader.GetInt32(55), reader.GetInt32(56), reader.GetInt32(57), reader.GetInt32(58), reader.GetInt32(59), reader.GetInt32(60), reader.GetInt32(61), reader.GetInt32(62), reader.GetInt32(63), reader.GetInt32(64), reader.GetInt32(65), reader.GetInt32(66), reader.GetInt32(67), reader.GetInt32(68), reader.GetInt32(69) });
                        playersList.Add(p4);
                    }

                    System.Diagnostics.Debug.WriteLine("p5name " + reader.GetString(70));
                    System.Diagnostics.Debug.WriteLine("p6score " + reader.GetString(71));
                    Player p5;
                    if (reader.GetString(71).Equals("0"))
                    {
                        p5 = null;
                    }
                    else
                    {
                        p5 = new Player(reader.GetString(70), reader.GetInt32(71),
                            new List<int> { reader.GetInt32(72), reader.GetInt32(73), reader.GetInt32(74), reader.GetInt32(75), reader.GetInt32(76), reader.GetInt32(77), reader.GetInt32(78), reader.GetInt32(79), reader.GetInt32(80), reader.GetInt32(81), reader.GetInt32(82), reader.GetInt32(83), reader.GetInt32(84), reader.GetInt32(85), reader.GetInt32(86) });
                        playersList.Add(p5);
                    }

                    game = new Game(reader.GetString(0), reader.GetString(1), playersList);
                }
            }
            Close();

            return game;
        }

        // Pobranie listy gier z lokalnej bazy danych
        public List<Game> readGames()
        {
            Open();
            List<Game> gamesList = new List<Game>();
            cmd.CommandText = "SELECT id, gameDate FROM Games";
            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("ID " + reader.GetString(0));
                    System.Diagnostics.Debug.WriteLine("DATA " + reader.GetString(1));
                    gamesList.Add(new Game(reader.GetString(0), reader.GetString(1)));
                }
            }
            Close();

            return gamesList;
        }

        // Utworzenie tabeli gier w lokalnej bazie danych
        private void createDB()
        {
            SqliteCommand cmd;
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

        // Metoda kopiująca strumień pliku do IsolatedStorage
        private void CopyFromContentToStorage(String assemblyName, String dbName)
        {
            IsolatedStorageFile store =
                IsolatedStorageFile.GetUserStoreForApplication();
            System.IO.Stream src =
                Application.GetResourceStream(
                    new Uri("/" + assemblyName + ";component/" + dbName,
                            UriKind.Relative)).Stream;
            IsolatedStorageFileStream dest =
                new IsolatedStorageFileStream(dbName,
                    System.IO.FileMode.OpenOrCreate,
                    System.IO.FileAccess.Write, store);
            src.Position = 0;
            CopyStream(src, dest);
            dest.Flush();
            dest.Close();
            src.Close();
            dest.Dispose();
        }

        // Metoda wspierająca kopiowanie strumienia pliku do IsolatedStorage
        private static void CopyStream(System.IO.Stream input,
                                        IsolatedStorageFileStream output)
        {
            byte[] buffer = new byte[32768];
            long TempPos = input.Position;
            int readCount;
            do
            {
                readCount = input.Read(buffer, 0, buffer.Length);
                if (readCount > 0)
                {
                    output.Write(buffer, 0, readCount);
                }
            } while (readCount > 0);
            input.Position = TempPos;
        }
    }
}
