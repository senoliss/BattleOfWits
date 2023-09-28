using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace BattleOfWits
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*###############################SETTING THE CONSOLE###############################*/
            // using BackgroundColor property
            Console.BackgroundColor = ConsoleColor.DarkGray;
            // using ForegroundColor property
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(60, 30);
            Console.SetBufferSize(60, 40);

            // Variables
            bool gameRuns = true;
            bool playerExists = false;
            bool isLoggedIn = false;
            string menuChoice = null;
            (string Name, string Surname) = ("", "");
            Dictionary<string, string> players = new Dictionary<string, string>();

            // Writing the welcome message until login
            printWelcome();

            // Writing the Rules
            printRules(Name, Surname);

            // Login Window
            (Name, Surname) = logIn();
            // Add player
            players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);


            //while (!playerExists)
            //{
            //    // Login Window
            //    (Name, Surname) = logIn();
            //    // Add player
            //    players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);

            //}
            // Init the game
            while (gameRuns) 
            {
                menuChoice = printMenu(Name, Surname);
                switch (menuChoice)
                {
                    case "1":
                        if (playerExists)
                        {
                            // Login Window
                            (Name, Surname) = logIn();
                            // Add player
                            players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
                            Console.WriteLine("Ran");
                        }
                        else
                        {
                            Console.WriteLine("Do you want to see rules and your hiscore again? y/n:");
                            var answer = Console.ReadKey().Key;
                            Thread.Sleep(1000);
                            Console.Clear();
                            if (answer == ConsoleKey.Y)
                            {
                                printRules(Name, Surname);
                                Console.WriteLine();
                                Console.WriteLine("--Press anything to continue--");
                                Console.ReadKey();
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                        }
                        Console.WriteLine("Chose 1");
                        Thread.Sleep(1000);
                        Console.Clear();
                        logIn();
                        break;
                    case "2":
                        Console.WriteLine("Chose 2");
                        Thread.Sleep(1000);
                        Console.Clear();
                        logOut();
                        break;
                    case "3":
                        Console.WriteLine("Chose 3");
                        Thread.Sleep(1000);
                        Console.Clear();
                        printRules(Name, Surname);
                        break;
                    case "4":
                        Console.WriteLine("Chose 4");
                        Thread.Sleep(1000);
                        Console.Clear();
                        printMenu(Name, Surname);
                        break;
                    case "5":
                        Console.WriteLine("Chose 5");
                        Thread.Sleep(1000);
                        Console.Clear();
                        printMenu(Name, Surname);
                        break;
                    default:
                        Console.WriteLine("My buddy, a wrong choice you've made here!");
                        break;
                }
            }
            Console.ReadKey();
        }

        public static void printWelcome()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("++++++++++++++++++++++++BRAINUS+++++++++++++++++++++++++++");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("                 WELCOME TO THE BRAINUS! " +
                "\n       A BATTLE OF WITS WHERE FASTEST NEURONS CLASHES, " +
                "\n     MIGHTIEST REFLEXES SURPASS AND SHARPEST MINDS WINS!");
        }
        public static void printRules(string Name, string Surname)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write($"                      SET OF RULES:        U:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} {Surname}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("User has to use credentials to participate in " +
                "\ngame (Name & Surname)");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("User can use MENU freely and at any point of " +
                "\ninput in game can exit to MENU by typing 'q'");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("User has to");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("User are able to bla");
        }
        public static string printMenu(string Name, string Surname)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("                         MENU:        U:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} {Surname}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Log-In");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Log-Out");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Rules");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hiscore Table");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("5. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Begin The Game");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("6. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quit The game");
            Console.Write("Choose your action : ");
            string key = Console.ReadLine();
            return key;
        }
        public static (string, string) logIn()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Let us Log you In to the game! ");
            Console.Write("How thee are calling you? ");
            string Name = Console.ReadLine();
            Console.Write("How thee are calling your family? ");
            string Surname = Console.ReadLine();

            return (Name, Surname);
        }
        public static (string, string) logOut()
        {
            (string Name, string Surname) = ("", "");
            Console.WriteLine("User has been cleared!");
            return (Name, Surname);
        }
        public static Dictionary<string, string> AddOrUpdatePlayer(Dictionary<string, string> players, string Name, string Surname, out bool playerExists)
        {
            Dictionary<string, string> playersUpd = new Dictionary<string, string>();
            if (players.ContainsKey(Name))
            {
                // Fruit already exists, update the quantity.
                playerExists = true;
                Console.WriteLine($"Player {{{Name} {players[Name]}}} already exists in our hiscores!");
            }
            else
            {
                // Add a new fruit.
                players.Add(Name, Surname);
                playerExists = false;
                Console.WriteLine($"Great player {{{Name} {players[Name]}}} was added to the game board" +
                    $"\n and can now participate");
            }

            return players;
        }

        public static void saveScores(string Name, string Surname, int score, bool saveOrPrint)
        {
            if(saveOrPrint)
            {
                // save score
            }
            if (!saveOrPrint)
            {
                // print score
            }
        }

        public static void printHiscores(Dictionary<string, string> players)
        {

        }
    }
}