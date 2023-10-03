using System.Diagnostics;
using System.Runtime.CompilerServices;
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
            Console.SetWindowSize(60, 40);
            Console.SetBufferSize(60, 50);
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
            (string Name2, string Surname2) = ("", "");
            int score = 0;
            Dictionary<string, string> players = new Dictionary<string, string>();
            Dictionary<string, int> playersScore = new Dictionary<string, int>();
            Dictionary<int, string> answersInput = new Dictionary<int, string>();
            Dictionary<int, string> answers = new Dictionary<int, string>();
            List<string[]> questions = new List<string[]>();

            (questions, answers) = ReadQuestionsFromFile(filePath);                     // Reads all the questions from Questions.txt file and formats them into printable array of strings, collects the answers and puts them into the dictionary in which key resembles test questuion and value an answer.
            Shuffle(questions);                                                         // Since the questions are read from 1 till 2oo this method shuffles the array of strings so that the questions could be randomized.

            //printWelcome(players);                                                      // Prints first welcome message with empty users as a title to the game. Parameter players is a dictionary of logged in players.

            //printQuestions(questions);

            //// Login Window
            //(Name, Surname) = logIn(Name, Surname, playerExists);
            //// Add player
            //players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
            Thread.Sleep(1000);
            Console.Clear();

            while (true)
            {

                for (int i = 0; i <= 10; i++)
                {
                    // Writing the welcome message until login
                    printWelcome(players);
                    // Writing the Rules
                    printRules();
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

                //if (!playerExists)
                //{
                //    // Login Window
                //    (Name, Surname) = logIn(Name, Surname, playerExists);
                //    // Add player
                //    players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
                //    Thread.Sleep(1000);
                //    Console.Clear();
                //}

                while (gameRuns)
                {
                    Console.Clear();
                    // Writing the welcome message until login
                    printWelcome(players);
                    menuChoice = printMenu();
                    switch (menuChoice)
                    {
                        case "1":                                                   // Log-In
                            Console.WriteLine("Chose 1");
                            logIn(ref players, ref playerExists);
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                        case "2":                                                   // Log-Out
                            Console.WriteLine("Chose 2");
                            Thread.Sleep(1000);
                            logOut(ref players, ref playerExists); // uzmest ref players dict ir pakeist kad tikrintu jei bent vienas zaidejas yra kad playerexists true
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                        case "3":                                                   // Print Rules
                            Console.WriteLine("Chose 3");
                            Thread.Sleep(1000);
                            Console.Clear();
                            printWelcome(players);
                            printRules();
                            var key = Console.ReadKey().Key;
                            do
                            {
                                Thread.Sleep(1000);
                                Console.Clear();
                                printWelcome(players);
                                printRules();
                                key = Console.ReadKey().Key;
                            }
                            while (key != ConsoleKey.Q);
                            if (key == ConsoleKey.Q)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Now, merrily waltzing to the MENU...");
                                Thread.Sleep(1000);
                                break;
                            }
                            break;
                        case "4":                                                   // Print Hiscores
                            Console.WriteLine("Chose 4");
                            Thread.Sleep(1000);
                            Console.Clear();
                            do
                            {
                                Thread.Sleep(1000);
                                Console.Clear();
                                printWelcome(players);
                                printHiscores(playersScore, playerExists);
                                key = Console.ReadKey().Key;
                            }
                            while (key != ConsoleKey.Q);
                            if (key == ConsoleKey.Q)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Now, merrily waltzing to the MENU...");
                                Thread.Sleep(1000);
                                break;
                            }
                            break;
                        case "5":                                                   // Begin the Game
                            Console.WriteLine("Chose 5");
                            Thread.Sleep(1000);
                            Console.Clear();
                            if (!playerExists)
                            {
                                printWelcome(players);
                                Console.WriteLine("----------------------------------------------------------");
                                Console.WriteLine("Alas, there's nary a Logged In User at this present moment!");
                                Console.WriteLine("Now, merrily waltzing to the MENU...");
                                Thread.Sleep(2000);
                            }
                            if (playerExists)
                            {
                                playGame(questions, players, ref playersScore, answers);
                            }

                            break;
                        case "6":                                                   // Quit the Game((console)
                            Console.WriteLine("Chose 6");
                            Thread.Sleep(1000);
                            Console.Clear();
                            printWelcome(players);
                            Console.WriteLine("----------------------------------------------------------");
                            Console.WriteLine("Thank you for participating in BATTLE OF WITS!");
                            Console.WriteLine("Saving and exiting the game...");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Aha, thou hast taken quite the amusing detour here!");
                            Thread.Sleep(1500);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                            break;
                    }
                }
            }
        }

        public static void printWelcome(Dictionary<string, string> players)
        {
            (string Name, string Surname) = ("", "");
            (string Name2, string Surname2) = ("", "");

            foreach (var player in players)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = player.Key;
                    Surname = player.Value;
                }
                else
                {
                    Name2 = player.Key;
                    Surname2 = player.Value;
                }
            }
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("++++++++++++++++++++++++BRAINUS+++++++++++++++++++++++++++");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("                 WELCOME TO THE BRAINUS! " +
                "\n       A BATTLE OF WITS WHERE FASTEST NEURONS CLASHES, " +
                "\n     MIGHTIEST REFLEXES SURPASS AND SHARPEST MINDS WINS!");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write($"User1:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} {Surname}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"User2:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{Name2} {Surname2}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void printRules()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"                      SET OF RULES:");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"{Name} {Surname}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The User must wield their credentials to partake " +
                "\n in the game (Name & Surname).");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The User can gallivant freely to YONDER MENU and," +
                "\n at any point, can escape to the MENU by uttering 'q'.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SOLO play? Simply loggeth in once and off thou goest!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("For the boisterous DUEL mode, a second user " +
                "\nmay be summoned by choosing LogIn for the second time!");

        }
        public static string printMenu()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("                         MENU:");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"{Name} {Surname}");
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
        public static void logIn(ref Dictionary<string, string> players, ref bool playerExists)
        {
            (string Name, string Surname) = ("", "");
            (string Name2, string Surname2) = ("", "");
            string key = "";
            foreach (var player in players)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = player.Key;
                    Surname = player.Value;
                }
                else
                {
                    Name2 = player.Key;
                    Surname2 = player.Value;
                }
            }
            Console.Clear();
            printWelcome(players);
            Console.WriteLine("----------------------------------------------------------");
            if (playerExists)
            {
                if (players.Count > 0 && players.Count < 2)
                {
                    Console.Write($"Oopsie-doodle! That player already resides here: ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{Name} {Surname}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1. Ahoy, we needeth a second player, posthaste!");
                    Console.WriteLine("2. Time to revieweth the quirky rules, my friend.");
                    Console.Write("Choose thy mirthful course of action: ");
                    key = Console.ReadLine();
                    switch (key)
                    {
                        case "1":
                            playerExists = false;
                            break;
                        case "2":
                            printRules();
                            break;
                        case "q":
                            Console.WriteLine("Now, merrily waltzing to the MENU...");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Aha, thou hast taken quite the amusing detour here!");
                            Thread.Sleep(1500);
                            Console.ForegroundColor = ConsoleColor.White;
                            logIn(ref players, ref playerExists);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Oh dear, both player spots art already occupied! How peculiar!");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{Name} {Surname}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" & ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{Name2} {Surname2}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1. Time to revieweth the quirky rules, my friend.");
                    Console.WriteLine("2. Waltz to MENU.");
                    key = Console.ReadLine();
                    switch (key)
                    {
                        case "1":
                            Console.Clear();
                            printWelcome(players);
                            printRules();
                            Thread.Sleep(3000);
                            break;
                        case "2" or "q":
                            Console.WriteLine("Now, merrily waltzing to the MENU...");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Aha, thou hast taken quite the amusing detour here!");
                            Thread.Sleep(1500);
                            Console.ForegroundColor = ConsoleColor.White;
                            logIn(ref players, ref playerExists);
                            break;
                    }
                }
            }

            if (!playerExists)
            {
                Console.WriteLine("Let us embark on a whimsical journey " +
                    "\ninto the realm of the game!");
                Console.WriteLine();
                Console.WriteLine("Pray, what shall we dub thee, noble player?");
                Name = Console.ReadLine();
                Name = char.ToUpper(Name[0]) + Name.Substring(1).ToLower();
                Console.WriteLine("How dost thou address thy kinsfolk " +
                    "\nin this merry adventure?");
                Surname = Console.ReadLine();
                Surname = char.ToUpper(Surname[0]) + Surname.Substring(1).ToLower();
                players = AddOrUpdatePlayer(players, Name, Surname, out playerExists);
            }
        }
        public static void logOut(ref Dictionary<string, string> players, ref bool playerExists)
        {
            (string Name, string Surname) = ("", "");
            (string Name2, string Surname2) = ("", "");
            foreach (var player in players)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = player.Key;
                    Surname = player.Value;
                }
                else
                {
                    Name2 = player.Key;
                    Surname2 = player.Value;
                }
            }
            Console.Clear();
            printWelcome(players);
            Console.WriteLine("----------------------------------------------------------");

            if (players.Count == 0)
            {
                Console.WriteLine("Lo and behold, there's nary a Logged In User at this present moment!");
                Console.WriteLine("Now, merrily waltzing to the MENU...");
                Thread.Sleep(1000);
            }

            if (players.Count == 1)
            {
                Console.WriteLine("Only one gallant player hath ventured forth into this quest, verily.");
                Console.Write("1. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{Name} {Surname}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Dost thou truly desire to part company? Yay or nay:");
                string key = Console.ReadLine().ToLower();
                switch (key)
                {
                    case "y" or "yay":
                        Console.WriteLine($"User {{{Name} {Surname}}} has been cleared from the stage!!");
                        players.Remove(Name);
                        playerExists = false;
                        Console.WriteLine("Now, merrily waltzing to the MENU...");
                        Thread.Sleep(1000);
                        break;
                    case "n" or "q" or "nay":
                        Console.WriteLine("Alas, the User hath not been cleared! What merriment!");
                        Console.WriteLine("Now, merrily waltzing to the MENU...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Aha, thou hast taken quite the amusing detour here!");
                        Thread.Sleep(1500);
                        Console.ForegroundColor = ConsoleColor.White;
                        logOut(ref players, ref playerExists);
                        break;
                }
            }

            if (players.Count > 1)
            {
                Console.WriteLine("Which player shall we jettison, or, in simpler terms, clear/logout?");
                Console.Write("1. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{Name} {Surname}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("2. ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{Name2} {Surname2}");
                Console.ForegroundColor = ConsoleColor.White;
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        players.Remove(Name);
                        Console.WriteLine($"User {{{Name} {Surname}}} hath been cleared from the stage!");
                        break;
                    case "2":
                        players.Remove(Name2);
                        Console.WriteLine($"User {{{Name2} {Surname2}}} hath been cleared from the stage!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Aha, thou hast taken quite the amusing detour here!");
                        Thread.Sleep(1500);
                        Console.ForegroundColor = ConsoleColor.White;
                        logOut(ref players, ref playerExists);
                        break;
                }
            }

        }
        public static Dictionary<string, string> AddOrUpdatePlayer(Dictionary<string, string> players, string Name, string Surname, out bool playerExists)
        {
            Dictionary<string, string> playersUpd = new Dictionary<string, string>();
            if (players.ContainsKey(Name))
            {
                // Player already exists, update the info.
                Console.WriteLine($"Player {{{Name} {players[Name]}}} hath already etched their name into our peculiar hiscores!");
            }
            else if (!players.ContainsKey(Name))
            {
                // Add a new player.
                players.Add(Name, Surname);
                Console.WriteLine($"Huzzah! A great new player, {{{Name} {players[Name]}}}, " +
                    $"\nhath emerged to grace our game board " +
                    $"\nand partake in the festivities!");
            }
            playerExists = true;
            return players;
        }
        public static void saveScores(ref Dictionary<string, int> playersScore, Dictionary<string, string> players, int score, string playerName)
        {
            string fullName = "";

            foreach (var player in players)
            {
                if (player.Key == playerName)
                {
                    fullName = player.Key + " " + player.Value;
                }
            }

            if (playersScore.TryGetValue(fullName, out int scoreOld))
            {
                scoreOld += score;
                playersScore[fullName] += scoreOld;
            }
            else
            {
                playersScore.Add(fullName, score);
            }
        }

        public static void printHiscores(Dictionary<string, int> players, bool playerExists)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"                      HISCORES:");
            if (playerExists)
            {
                int i = 1;
                foreach (var kvp in players)
                {
                    Console.Write($"\n{i} PLACE: ");
                    Console.WriteLine($"{kvp.Key}, {kvp.Value}");
                    i++;
                }
                if (players.Count == 0)
                {
                    Console.WriteLine("\nThere art no players who hath joined the whimsical fray known as the Battle of Wits!");
                }
            }
            if (!playerExists)
            {
                Console.WriteLine("Only Registered members can behold the legendary wall of Legends!");
            }
        }

        public static (List<string[]>, Dictionary<int, string>) ReadQuestionsFromFile(string filePath)
        {
            List<string[]> questions = new List<string[]>();
            string[] lines = File.ReadAllLines(filePath);
            List<string> currentQuestion = new List<string>();
            Dictionary<int, string> answers = new Dictionary<int, string>();
            int answerNum = 0;
            string answerLet;

            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    if (char.IsDigit(line[0]))
                    {
                        answerNum = Convert.ToInt32(line.Split('.')[0]);
                    }
                    if (!line.StartsWith("Correct") && !line.StartsWith("**C#"))
                    {
                        currentQuestion.Add(line); // Add question text and answer options
                    }
                }
                if (line.StartsWith("Correct:"))
                {
                    answerLet = new string(line.Substring(9));
                    //currentQuestion.Add(line.Substring(9)); // Add the correct answer
                    questions.Add(currentQuestion.ToArray());
                    currentQuestion.Clear();
                    answers.Add(answerNum, answerLet);
                }
            }

            return (questions, answers);
        }

        // Shuffle a list using the Fisher-Yates shuffle algorithm
        static void Shuffle(List<string[]> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string[] value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // A separate method to print questions not in Main method
        public static void playGame(List<string[]> list, Dictionary<string, string> players, ref Dictionary<string, int> playersScore, Dictionary<int, string> answers)
        {
            Dictionary<int, string> answersInput = new Dictionary<int, string>();
            Dictionary<string, int> tempScores = new Dictionary<string, int>(); // a dict to keep track of points after each session and maybe in future to keep track in txt file
            int qNum = 0;
            string qLet = null;
            int i = 1;
            int initQuestions = 3;
            int score = 0;
            string[] temp = null;
            string tempName = null;
            foreach (var items in list)
            {
                Console.Clear();
                printWelcome(players);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Question: {i}/{initQuestions}");
                Console.WriteLine("----------------------------------------------------------");

                foreach (var item in items)
                {
                    if (char.IsDigit(item[0]))
                    {
                        qNum = Convert.ToInt32(item.Split('.')[0]);
                    }
                    Console.WriteLine(item.ToString());
                    temp = items;
                }

                foreach (var player in players)
                {
                    tempName = player.Key + " " + player.Value;
                    if (!tempScores.ContainsKey(tempName))
                    {
                        tempScores.Add(tempName, score);     // adds the player dict pair and their score to temp dict for trACKING
                    }

                    answersInput.Clear();
                    score = 0;
                    Console.WriteLine();
                    Console.Write($"Player {player.Key} {player.Value} answer: ");
                    qLet = Console.ReadLine().ToUpper();

                    if (qLet != "A" && qLet != "B" && qLet != "C" && qLet != "D")
                    {
                        do
                        {
                            Console.WriteLine("WRONG INPUT! Thou art quite the jester!");
                            Console.Write("Pray tell, what be thy answer? ");
                            qLet = Console.ReadLine().ToUpper();
                        }
                        while (qLet != "A" && qLet != "B" && qLet != "C" && qLet != "D");
                    }
                    answersInput.Add(qNum, qLet);
                    Console.WriteLine($"We thank thee, {player.Key} {player.Value}, forsooth! " +
                        $"\nThy answer {qLet} for question {qNum} hath been dutifully noted!");
                    score = checkAnswers(answersInput, answers);
                    tempScores[tempName] += score;   // updates the score for each player after a question
                    saveScores(ref playersScore, players, score, player.Key);
                    Thread.Sleep(3000);
                }

                // clears console and prints again the same question after 1 or both players input answers but with higlighted text on answer.
                Console.Clear();
                printWelcome(players);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Question: {i}/{initQuestions}");
                Console.WriteLine("----------------------------------------------------------");
                foreach (var line in temp)
                {
                    if (line.StartsWith(answers[qNum]))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(line);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Lo and behold! The correct answer for question {qNum} was: {answers[qNum]}");

                // Loader sentence for next question, and to give time to compile answer.
                if (i < initQuestions)  // Load while not reached last question
                {
                    Console.Write("\nLoading the next enigmatic query, anon.");
                    for (int l = 0; l < 5; l++)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                }
                i++;
                // when the questions end
                if (i > initQuestions)          // initQuestions => How many questions
                {
                    i = 1;

                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("This session hath come to a close, dear friend! " +
                        "\nTotal points for this session: ");
                    foreach (var item in tempScores)
                    {
                        Console.WriteLine($"Player {item.Key} earned {item.Value} points");
                    }

                    Console.WriteLine("\nDost thou yearn to playeth again (y) or taketh thy leave to the YONDER MENU (q)?");
                    string decision = Console.ReadLine().ToUpper();
                    if (decision == "Y")
                    {
                        Shuffle(list);
                        playGame(list, players, ref playersScore, answers);
                    }
                    if (decision == "Q")
                    {
                        Console.WriteLine("Now, merrily waltzing to the MENU...");
                        Thread.Sleep(3000);
                        break;
                    }
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("WRONG INPUT! Thou art quite the jester!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Playeth again (y) or depart to the YONDER MENU (q):");

                        decision = Console.ReadLine().ToUpper();
                        if (decision == "Y")
                        {
                            Shuffle(list);
                            playGame(list, players, ref playersScore, answers);
                        }
                        if (decision == "Q")
                        {
                            Console.WriteLine("Now, merrily waltzing to the MENU...");
                            Thread.Sleep(3000);
                            break;
                        }
                    }
                    break;
                }

            }
        }

        public static int checkAnswers(Dictionary<int, string> answersInput, Dictionary<int, string> answers)
        {
            // Compare the two dictionaries
            bool allAnswersCorrect = true;
            int score = 0;
            foreach (var kvp in answersInput)
            {
                int questionNumber = kvp.Key;
                string actualAnswer = kvp.Value;

                if (answers.TryGetValue(questionNumber, out string expectedAnswer))
                {
                    if (actualAnswer != expectedAnswer)
                    {
                        //Console.WriteLine($"Question {questionNumber}: Incorrect Answer ok: {answers[questionNumber]}, nok: {answersInput[questionNumber]}");
                        allAnswersCorrect = false;
                    }
                    else
                    {
                        //Console.WriteLine($"Question {questionNumber}: Correct Answer {answers[questionNumber]}");
                        score++;
                    }
                }
                else
                {
                    Console.WriteLine($"Question {questionNumber}: No Answer Provided");
                    allAnswersCorrect = false;
                }
            }
            return score;

        }
    }
}