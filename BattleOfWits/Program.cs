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
            Console.Title = "Battle Of Wits";

            // Variables
            string filePath = "Questions.txt";
            bool gameRuns = true;
            bool playerExists = false;
            bool isLoggedIn = false;
            string menuChoice = null;
            string gameLoader = "";
            string gameLoader2 = "==";
            string gameLoader3 = "                    ";
            (string Name, string Surname) = ("", "");
            Dictionary<string, string> players = new Dictionary<string, string>();

            List<string[]> questions = ReadQuestionsFromFile(filePath);

            printWelcome();
            // Login Window
            foreach (var items in questions)
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine(questions[i].ToString());
            }
            Console.ReadLine();
            (Name, Surname) = logIn(Name, Surname, playerExists);
            // Add player
            players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
            Thread.Sleep(1000);
            Console.Clear();

            while (true)
            {

                for (int i = 0; i <= 10; i++)
                {
                    // Writing the welcome message until login
                    printWelcome();
                    // Writing the Rules
                    printRules(Name, Surname);
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Loading the game " + i * 10 + "% [" + gameLoader + gameLoader3 + "]");
                    Console.WriteLine("----------------------------------------------------------");
                    Thread.Sleep(800);
                    Console.Clear();

                    if (gameLoader3.Length > 0)
                    {

                        gameLoader3 = gameLoader3.Remove(0, 2);
                    }
                    gameLoader += gameLoader2;
                }
                Thread.Sleep(1000);
                Console.Clear();

                if (!playerExists)
                {
                    // Login Window
                    (Name, Surname) = logIn(Name, Surname, playerExists);
                    // Add player
                    players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
                    Thread.Sleep(1000);
                    Console.Clear();
                }

                while (gameRuns)
                {
                    Console.Clear();
                    // Writing the welcome message until login
                    printWelcome();
                    menuChoice = printMenu(Name, Surname);
                    switch (menuChoice)
                    {
                        case "1":
                            (Name, Surname) = logIn(Name, Surname, playerExists);
                            //if (!playerExists)
                            //{
                            //    // Login Window
                            //    (Name, Surname) = logIn(Name, Surname, playerExists);
                            //    // Add player
                            //    players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
                            //    Console.WriteLine("Ran");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Do you want to see rules and your hiscore again? y/n:");
                            //    var answer = Console.ReadKey().Key;
                            //    Thread.Sleep(1000);
                            //    Console.Clear();
                            //    if (answer == ConsoleKey.Y)
                            //    {
                            //        printRules(Name, Surname);
                            //        Console.WriteLine();
                            //        Console.WriteLine("--Press anything to continue--");
                            //        Console.ReadKey();
                            //        Thread.Sleep(1000);
                            //        Console.Clear();
                            //    }
                            //}
                            Console.WriteLine("Chose 1");
                            Thread.Sleep(1000);
                            Console.Clear();
                            //logIn(Name, Surname, playerExists);
                            break;
                        case "2":
                            Console.WriteLine("Chose 2");
                            Thread.Sleep(1000);
                            Console.Clear();
                            if (!playerExists)
                            {
                                Console.WriteLine("There's no Logged In User at the moment!");
                                Thread.Sleep(1000);
                            }
                            if (playerExists)
                            {
                                (Name, Surname) = logOut();
                                playerExists = false;
                                Console.WriteLine("User has been cleared!");
                            }
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                        case "3":
                            Console.WriteLine("Chose 3");
                            Thread.Sleep(1000);
                            Console.Clear();
                            printWelcome();
                            printRules(Name, Surname);
                            var key = Console.ReadKey().Key;
                            do
                            {
                                Thread.Sleep(1000);
                                Console.Clear();
                                printWelcome();
                                printRules(Name, Surname);
                                key = Console.ReadKey().Key;
                            }
                            while (key != ConsoleKey.Q);
                            if (key == ConsoleKey.Q)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Redirecting to MENU...");
                                Thread.Sleep(1000);
                                break;
                            }
                            break;
                        case "4":
                            Console.WriteLine("Chose 4");
                            Thread.Sleep(1000);
                            Console.Clear();
                            printWelcome();
                            printHiscores(players, playerExists);
                            key = Console.ReadKey().Key;
                            do
                            {
                                Thread.Sleep(1000);
                                Console.Clear();
                                printWelcome();
                                printHiscores(players, playerExists);
                                key = Console.ReadKey().Key;
                            }
                            while (key != ConsoleKey.Q);
                            if (key == ConsoleKey.Q)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Redirecting to MENU...");
                                Thread.Sleep(1000);
                                break;
                            }
                            break;
                        case "5":
                            Console.WriteLine("Chose 5");
                            Thread.Sleep(1000);
                            Console.Clear();
                            printMenu(Name, Surname);
                            break;
                        default:
                            Console.WriteLine("My buddy, a wrong choice you've made here!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                    }
                }
            }




            //while (!playerExists)
            //{
            //    // Login Window
            //    (Name, Surname) = logIn(Name, Surname, playerExists);
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
                        (Name, Surname) = logIn(Name, Surname, playerExists);
                        //if (playerExists)
                        //{
                        //    // Login Window

                        //    // Add player
                        //    players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
                        //    Console.WriteLine("Ran");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Do you want to see rules and your hiscore again? y/n:");
                        //    var answer = Console.ReadKey().Key;
                        //    Thread.Sleep(1000);
                        //    Console.Clear();
                        //    if (answer == ConsoleKey.Y)
                        //    {
                        //        printRules(Name, Surname);
                        //        Console.WriteLine();
                        //        Console.WriteLine("--Press anything to continue--");
                        //        Console.ReadKey();
                        //        Thread.Sleep(1000);
                        //        Console.Clear();
                        //    }
                        //}
                        Console.WriteLine("Chose 1");
                        Thread.Sleep(1000);
                        Console.Clear();
                        //logIn(Name, Surname, playerExists);
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
            Console.WriteLine("User can play the game in SOLO mode for practice.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("For DUEL mode another user can be Logged In " +
                "\nby chosing LogIn for the second time!");

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
        public static (string, string) logIn(string Name, string Surname, bool playerExists)
        {
            if (playerExists)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Player already exists {Name} {Surname}!");
                Console.WriteLine("1. Add second player!");
                Console.WriteLine("2. Review rules.");
                Console.Write("Choose your action : ");
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        playerExists = false;
                        break;
                    case "2":
                        printRules(Name, Surname);
                        break;
                    default:
                        Console.WriteLine("Aha");
                        break;
                }

            }

            if (!playerExists)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Let us Log you In to the game! ");
                Console.Write("How thee are calling you? ");
                Name = Console.ReadLine();
                Console.Write("How thee are calling your family? ");
                Surname = Console.ReadLine();
            }

            return (Name, Surname);
        }
        public static (string, string) logOut()
        {
            (string Name, string Surname) = ("", "");
            return (Name, Surname);
        }
        public static Dictionary<string, string> AddOrUpdatePlayer(Dictionary<string, string> players, string Name, string Surname, out bool playerExists)
        {
            Dictionary<string, string> playersUpd = new Dictionary<string, string>();
            if (players.ContainsKey(Name))
            {
                // Player already exists, update the info.
                Console.WriteLine($"Player {{{Name} {players[Name]}}} already exists in our hiscores!");
            }
            // Add a new player.
            players.Add(Name, Surname);
            Console.WriteLine($"Great new player {{{Name} {players[Name]}}} was added to the game " +
                $"\nboard and can now participate");

            playerExists = true;
            return players;
        }

        public static void saveScores(string Name, string Surname, int score, bool saveOrPrint)
        {
            if (saveOrPrint)
            {
                // save score
            }
            if (!saveOrPrint)
            {
                // print score
            }
        }

        public static void printHiscores(Dictionary<string, string> players, bool playerExists)
        {
            if (!playerExists)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Only Logged In users can view the wall of Legends!");
            }

            if (playerExists)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.Write($"                      HISCORES:");
                foreach (var kvp in players)
                {
                    Console.Write("1st PLACE: ");
                    Console.WriteLine(kvp.Key, kvp.Value);
                }
            }
        }

        static List<string[]> ReadQuestionsFromFile(string filePath)
        {
            List<string[]> questions = new List<string[]>();
            string[] lines = File.ReadAllLines(filePath);
            List<string> currentQuestion = new List<string>();
            Dictionary<int, string> anwers = new Dictionary<int, string>();
            string answerNum;
            string answerLet;

            foreach (string line in lines)
            {
                if (char.IsDigit(line[0]))
                {
                    answerNum = line.Substring(1);
                }
                if (line.StartsWith("Correct:"))
                {
                    answerLet = new string(line.Substring(9));
                    currentQuestion.Add(line.Substring(9)); // Add the correct answer
                    questions.Add(currentQuestion.ToArray());
                    currentQuestion.Clear();
                }
                else
                {
                    currentQuestion.Add(line); // Add question text and answer options
                }
            }

            return questions;
        }
    }
}