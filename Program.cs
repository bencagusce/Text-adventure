using TextAdventure;
using System;
using System.Linq;
using System.Threading;

namespace TextAdventure
{
    class Program
    {
        static Random rng = new Random();

        /// <summary>
        /// Rock, Paper, Scissors combat
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        static bool Combat(Inventory inventory)
        {
            string[] alternatives = { "Rock", "Paper", "Scissors" };

            int playerScore = 0;
            int enemyScore = 0;

            bool hasAmulet = inventory.HasItem(Item.Amulet);
            bool hasCash = inventory.HasItem(Item.Cash);

            while (playerScore < 3 && enemyScore < 3)
            {
                Console.Clear();
                Console.WriteLine($"Player: {playerScore} - Enemy: {enemyScore}");
                TypeWrite("Rock, Paper, Scissors Fight!\n");
                Console.WriteLine("1. Rock \n2. Paper \n3. Scissors");
                // If the player has the amulet, they can read the the enemy's mind
                if (hasAmulet) Console.WriteLine($"4. Use {GlobalVariables.itemNames[(int)Item.Amulet].ToLower()}");
                else if (hasCash) Console.WriteLine($"4. Use {GlobalVariables.itemNames[(int)Item.Cash].ToLower()}");
                Console.WriteLine("\npress a number to choose an option...");

                // Enemy choice
                int enemyChoice = rng.Next(3);

                // Player choice
                int playerChoice = -1;

                // Has used amulet
                bool hasUsedAmulet = false;

                while (playerChoice < 0 || playerChoice > 2)
                {
                    while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out playerChoice))
                    {
                        Console.WriteLine("Please enter a valid number");
                    }
                    playerChoice--;

                    if (playerChoice == 3)
                    {
                        if (hasAmulet)
                        {
                            if (!hasUsedAmulet)
                            {
                                TypeWrite("\nYou use the amulet of mind reading to see the enemy's choice.\n");
                                TypeWrite($"The enemy didn't chose {alternatives[(enemyChoice + 1 + rng.Next(2)) % 3]}.\n");
                                Console.WriteLine("\npress a number to choose an option...");
                                hasUsedAmulet = true;
                            }
                            else
                            {
                                Console.WriteLine("You have already used the amulet.");
                            }
                        }
                        else if (hasCash)
                        {
                            inventory.HasItem(Item.Cash, true);
                            TypeWrite("\nYou use the shiny cash to bribe the enemy.\n");
                            return true;
                        }
                    }
                    else if (playerChoice < 0 || playerChoice > 2)
                    {
                        Console.WriteLine("Please enter a valid number");
                    }
                }

                Console.Write($"\nYou chose {alternatives[playerChoice]} and they chose {alternatives[enemyChoice]}.");

                // Combat result
                // 0 = tie, 1 = win, 2 = lose
                switch ((3 + playerChoice - enemyChoice) % 3)
                {
                    case 0:
                        {
                            Console.WriteLine(" You tie!");
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine(" You win!");
                            playerScore++;
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(" You lose!");
                            enemyScore++;
                            break;
                        }
                }
                WaitForInteraction();
            }
            //wins
            if (playerScore == 3)
            {
                TypeWrite("You are victorious! One step closer to your sock!\n");
                return true;
            }
            else
            {
                //lost
                TypeWrite("You Lost.\n1. Retry\n2. Leave the room\n");
                Console.WriteLine("\npress a number to choose an option...");

                bool validAnswer = false;
                while (!validAnswer)
                {
                    validAnswer = true;
                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.D1:
                            return Combat(inventory);
                        case ConsoleKey.D2:
                            return false;
                        default:
                            Console.WriteLine("Please enter a valid number");
                            validAnswer = false;
                            break;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// Checks if the player answered the question correctly. If lastQuestion is true the dialogue will be different
        /// </summary>
        /// <param name="question"></param>
        /// <param name="lastQuestion"></param>
        /// <returns></returns>
        static bool correctAnswer(int question, bool lastQuestion = false)
        {
            int answer = 0;
            while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out answer) || 1 > answer || answer > 3)
            {
                Console.WriteLine("\nPlease enter a valid number");
            }
            if (answer == question)
            {
                if (lastQuestion) TypeWrite("\nYou have answered correctly!\n");
                else TypeWrite("\nVery good, heres one more question: \n");
                return true;
            }
            else
            {
                TypeWrite("\nIm dissapointed.\nYou are forced to leave.");
                return false;
            }
        }
        /// <summary>
        /// "Press any key to continue..." functionality
        /// </summary>
        public static void WaitForInteraction()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(false);
        }

        /// <summary>/
        /// Prints text letter by letter for dramatic effect
        /// </summary>
        /// <param name="text">The text to be printed</param>        
        public static void TypeWrite(string text)
        {
            ConsoleColor defaultConsoleColor = ConsoleColor.Gray;

            // Punctuation slows down printing for a more natrual feeling
            char[] slowLetters = { ',', '.', '!', '?', '\n' };

            // Whether or not the text should be printed in a fancy color
            bool fancyText = false;

            // Print the text letter by letter
            for (int i = 0; i < text.Length; i++)
            {
                // If an asterisk is found, toggle fancy text
                if (text[i] == '*')
                {
                    fancyText = !fancyText;
                    Console.ForegroundColor = fancyText ? ConsoleColor.Yellow : defaultConsoleColor;
                    continue;
                }

                // Print the letter
                Console.Write(text[i]);

                // Wait a bit before printing the next letter
                Thread.Sleep(slowLetters.Contains(text[i]) ? 200 : 20);
            }
        }
        static void Main()
        {
            // Wait for any errors or warnings to be read before starting the game
            WaitForInteraction();
            // Allow for the game to be replayed
            do
            {
                Room currentRoom = Room.Hallway;

                Inventory inventory = new Inventory();

                // -----
                // INTRO
                // -----
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;

                TypeWrite("Welcome to the BencaGusce Text Adventure!\nWhat is the name of our adventurer?\n");
                string name = Console.ReadLine() ?? "Bob McEwen";
                TypeWrite(
                    $"This is the story of {name} who lost their left sock in the washing machine.\n" +
                    $"This Sock was very important to {name} as it was their favourite lucky left sock.\n" +
                    $"In {name}'s desperate predicament they decided to visit the manor of lost left socks in search of their so missed sock.\n"
                );
                WaitForInteraction();

                while (currentRoom != Room.End)
                {
                    Console.Clear();
                    bool validAnswer = false;
                    switch (currentRoom)
                    {
                        case Room.Hallway:                            
                            TypeWrite(
                                "You enter the grand hallway of the manor.\n" +
                                "1. Go to the basement.\n" +
                                "2. Go to the upper floor.\n" +
                                "3. Exit the manor.\n"
                            );
                            Console.WriteLine("\npress a number to choose an option...");

                            while (!validAnswer)
                            {
                                validAnswer = true;
                                switch (Console.ReadKey(false).Key)
                                {
                                    case ConsoleKey.D1:
                                        {
                                            if (inventory.HasItem(Item.Key))
                                            {
                                                Console.Clear();
                                                TypeWrite("You use your key to open the door and enter.\n");
                                                currentRoom = Room.Basement;
                                                WaitForInteraction();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                TypeWrite("It seems that the door is locked.\n");
                                                WaitForInteraction();
                                            }
                                            break;
                                        }
                                    case ConsoleKey.D2:
                                        {
                                            Console.Clear();
                                            TypeWrite("You go up the stairway to the second floor.\n");
                                            currentRoom = Room.UpperFloor;
                                            WaitForInteraction();
                                            break;
                                        }
                                    case ConsoleKey.D3:
                                        {
                                            if (inventory.HasItem(Item.Sock))
                                            {
                                                Console.Clear();
                                                TypeWrite("You leave the manor with your *lucky sock*");
                                                if (inventory.HasItem(Item.Goose) && inventory.HasItem(Item.Cash)) TypeWrite($", *{GlobalVariables.itemNames[(int)Item.Goose]}* and some *shiny cash*.\n");
                                                else if (inventory.HasItem(Item.Goose)) TypeWrite($" and *{GlobalVariables.itemNames[(int)Item.Goose]}*.\n");
                                                else if (inventory.HasItem(Item.Cash)) TypeWrite(" and some *shiny cash*.\n");
                                                else TypeWrite(".\n");
                                                currentRoom = Room.End;
                                                WaitForInteraction();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                TypeWrite("It seems your foot is unwiling to leave without it's sock\n");
                                                WaitForInteraction();
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("\nPlease select a valid number.");
                                            validAnswer = false;
                                            break;
                                        }

                                }
                            }
                            break;

                        case Room.Basement:
                            // Store important information about the player's inventory in bools to improve performance
                            bool hasMap = inventory.HasItem(Item.Map);
                            bool hasAmulet = inventory.HasItem(Item.Amulet);
                            bool hasSock = inventory.HasItem(Item.Sock);
                            bool hasGoose = inventory.HasItem(Item.Goose);

                            (int, int) playerPosition = (5, 0);
                            
                            // Change the goose's name if it isn't in the inventory
                            if (!hasGoose) GlobalVariables.ReRollGooseName();

                            Console.Clear();
                            TypeWrite("You enter the basement.\nIt is damp and too dark to see anything.\n");
                            WaitForInteraction();
                            Console.Clear();

                            // Continue exploring the basement until the player leaves
                            while (currentRoom == Room.Basement)
                            {
                                // If the player is standing on the sock give it to them
                                if (playerPosition == GlobalVariables.sockPosition && !hasSock)
                                {
                                    TypeWrite("You found your lucky sock!\n");
                                    inventory.AddItem(Item.Sock);
                                    hasSock = true;
                                    hasMap = inventory.HasItem(Item.Map);
                                    hasAmulet = inventory.HasItem(Item.Amulet);
                                    hasGoose = inventory.HasItem(Item.Goose);
                                    WaitForInteraction();
                                    Console.Clear();
                                }

                                // If the player is standing on the goose, give them options to interact with it
                                if (playerPosition == GlobalVariables.goosePosition && !hasGoose)
                                {
                                    TypeWrite($"You have encountered the goose, *{GlobalVariables.itemNames[(int)Item.Goose]}*!\n");
                                    TypeWrite($"1. Pick up *{GlobalVariables.itemNames[(int)Item.Goose]}*\n");
                                    TypeWrite($"2. Leave *{GlobalVariables.itemNames[(int)Item.Goose]}* alone\n");
                                    if (hasAmulet) TypeWrite("3. Use the *amulet of mind reading*\n");
                                    Console.WriteLine("\npress a number to choose an option...");

                                    validAnswer = false;
                                    while (!validAnswer)
                                    {
                                        validAnswer = true;
                                        switch (Console.ReadKey(false).Key)
                                        {
                                            case ConsoleKey.D1:
                                                inventory.AddItem(Item.Goose);
                                                hasGoose = true;
                                                hasMap = inventory.HasItem(Item.Map);
                                                hasAmulet = inventory.HasItem(Item.Amulet);
                                                hasSock = inventory.HasItem(Item.Sock);
                                                break;
                                            case ConsoleKey.D2:
                                                TypeWrite($"\nYou leave {GlobalVariables.itemNames[(int)Item.Goose]} alone.\n");
                                                break;
                                            case ConsoleKey.D3:
                                                if (hasAmulet)
                                                {
                                                    TypeWrite($"\n{GlobalVariables.itemNames[(int)Item.Goose]} is thinking about {GlobalVariables.gooseSubjects[rng.Next(GlobalVariables.gooseSubjects.Length)]}.\n");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Please enter a valid number");
                                                    validAnswer = false;
                                                }
                                                break;
                                            default:
                                                Console.WriteLine("Please enter a valid number");
                                                validAnswer = false;
                                                break;
                                        }
                                    }
                                }

                                // Construct a list of options based on the player's position and inventory
                                // The first letter is used later to determine the player's choice
                                string[] options = { "h1. Go back to the hallway." };
                                if (playerPosition.Item1 != 0) options = [.. options, $"l{options.Length + 1}. Go left."];
                                if (playerPosition.Item1 != GlobalVariables.basementSize.Item1 - 1) options = [.. options, $"r{options.Length + 1}. Go right"];
                                if (playerPosition.Item2 != GlobalVariables.basementSize.Item2 - 1) options = [.. options, $"f{options.Length + 1}. Go forwards"];
                                if (playerPosition.Item2 != 0) options = [.. options, $"b{options.Length + 1}. Go backwards"];
                                if (hasMap) options = [.. options, $"m{options.Length + 1}. Use the map"];

                                // Print the options while ignoring the first letter
                                foreach (string option in options)
                                {
                                    Console.WriteLine(option[1..]);
                                }

                                Console.WriteLine("\npress a number to choose an option...");

                                int input = 0;
                                while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out input) || input < 1 || input > options.Length)
                                {
                                    Console.WriteLine("Please enter a valid number");
                                }

                                Console.Clear();

                                // Determine the course of action based on the first character of the chosen option
                                switch (options[input - 1][0])
                                {
                                    case 'h':
                                        currentRoom = Room.Hallway;
                                        Console.WriteLine("You go back to the hallway.");
                                        break;
                                    case 'l':
                                        playerPosition.Item1--;
                                        Console.WriteLine("You go left.");
                                        break;
                                    case 'r':
                                        playerPosition.Item1++;
                                        Console.WriteLine("You go right.");
                                        break;
                                    case 'b':
                                        playerPosition.Item2--;
                                        Console.WriteLine("You go backwards.");
                                        break;
                                    case 'f':
                                        playerPosition.Item2++;
                                        Console.WriteLine("You go forwards.");
                                        break;
                                    case 'm':
                                        Console.WriteLine("You use the map.");
                                        
                                        // Draw the map
                                        for (int y = GlobalVariables.basementSize.Item2; y >= 0; y--)
                                        {
                                            for (int x = 0; x < GlobalVariables.basementSize.Item1; x++)
                                            {
                                                if ((x, y) == playerPosition)
                                                {
                                                    Console.Write(" O");
                                                }
                                                else if ((x, y) == GlobalVariables.sockPosition && !hasSock)
                                                {
                                                    Console.Write(" X");
                                                }
                                                else if ((x, y) == GlobalVariables.goosePosition && !hasGoose)
                                                {
                                                    Console.Write(" X");
                                                }
                                                else
                                                {
                                                    Console.Write(" ~");
                                                }
                                            }
                                            Console.Write("\n");
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Default case reached. This should not happen.");
                                        WaitForInteraction();
                                        break;
                                }
                            }

                            break;
                        case Room.UpperFloor:

                            TypeWrite("Before you Hangs a sign with some beatufilly inscirbed letters\n");
                            Console.WriteLine(
                                "+---------------------------------+\n" +
                                "|   <-- Library ▲ Chamber -->     |\n" +
                                "|            Kitchen              |\n" +
                                "|                                 |\n" +
                                "+---------------------------------+\n"
                            );
                            TypeWrite(
                                "1. Go to the Library.\n" +
                                "2. Go to the Chamber.\n" +
                                "3. Go to the Kitchen.\n" +
                                "4. Go back down to the hallway.\n"
                            );
                            Console.WriteLine("\npress a number to choose an option...");

                            // bool validAnswer = false;
                            while (!validAnswer)
                            {
                                validAnswer = true;
                                switch (Console.ReadKey(false).Key)
                                {
                                    case ConsoleKey.D1:
                                        currentRoom = Room.Library;
                                        break;

                                    case ConsoleKey.D2:
                                        currentRoom = Room.Chamber;
                                        break;

                                    case ConsoleKey.D3:
                                        currentRoom = Room.Kitchen;
                                        break;

                                    case ConsoleKey.D4:
                                        Console.Clear();
                                        TypeWrite("\nYou go back down to the hallway.\n");
                                        currentRoom = Room.Hallway;
                                        WaitForInteraction();
                                        break;

                                    default:
                                        Console.WriteLine("\nPlease select a valid number.\n");
                                        validAnswer = false;
                                        break;

                                }
                            }
                            break;
                        case Room.Library:
                            //solve a riddle and get the mind reading amulet
                            if (inventory.HasItem(Item.Amulet) || inventory.hasRecievedCash)
                            {
                                Console.Clear();
                                TypeWrite("You have already recieved your price you silly goose.\n" +
                                    "You leave the library.");
                            }
                            else
                            {
                                Console.Clear();
                                TypeWrite("You enter the Library\n" +
                                    "In the library you're surrounded by books and bookshelves as far as you can see. In the middle of the room there's an elderly man surrounded \n" +
                                    "by towers of books. The wizards voice is deep and rumbles quietly\n"
                                );
                                WaitForInteraction();
                                TypeWrite("Have thee come to seek knowledge, oh sarig young one. Witan is no sin, though ignorance falls heavy on thy mind.\n" +
                                    "I shall put thy mind to a test. Wisdom always come with knowledge, and knowledge shan't go unnoticed.\n" +
                                    "Answer my three questions. And i shall gift you a very special *item* in return.\n" +
                                    "Question one: what is the worlds fastest animal?\n" +
                                    "1. Cheetah\n" +
                                    "2. Horse\n" +
                                    "3. Peregrine falcon\n");
                                if (correctAnswer(3))
                                {
                                    TypeWrite("Question 2: What is the chemical abreviation for salt?\n" +
                                    "1. NaCl\n" +
                                    "2. NaOH \n" +
                                    "3. HOH\n");
                                    if (correctAnswer(1))
                                    {
                                        TypeWrite("Question 3: I go out of my house for a walk. I walk one kilometer south, one kilometer east and one kilometer north.\n" +
                                        "Now back at my house I see a bear so I quickly close the door.\n" +
                                        "What colour was the bear?\n" +
                                        "1. Black\n" +
                                        "2. White\n" +
                                        "3. Brown\n");
                                        if (correctAnswer(2, true))
                                        {
                                            TypeWrite("You have answered 3 questions and 3 you got right.\n" +
                                                "Please select a price from my fine treasures.\n" +
                                                $"1. *{GlobalVariables.itemNames[(int)Item.Amulet]}*\n" +
                                                $"2. *{GlobalVariables.itemNames[(int)Item.Cash]}*\n"
                                            );
                                            int answer = 0;
                                            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out answer) || answer < 1 || answer > 3)
                                            {
                                                Console.WriteLine("Please enter a valid number");
                                            }
                                            if (answer == 1) inventory.AddItem(Item.Amulet);
                                            else inventory.AddItem(Item.Cash);
                                            TypeWrite("Now skedadle!\nYou leave the library");
                                        }
                                    }
                                }
                            }
                            WaitForInteraction();
                            currentRoom = Room.UpperFloor;
                            break;
                        case Room.Chamber:
                            //battle with rock/paper/scissors to get the Basement key
                            if (inventory.HasItem(Item.Key))
                            {
                                Console.Clear();
                                TypeWrite("You have already recieved your price you silly goose.\n" +
                                    "You leave the chamber.");
                            }
                            else
                            {
                                TypeWrite("You enter the Chamber\n" +
                                    "The giant room is laced with paintings and expensive decorations of only the most aftersought of socks, but sadly not yours.\n" +
                                    "As you look around you see a giant bed the size of your room standing tall in the middle of the room.\n" +
                                    "\"BANG\"The infamous *Gorb the palladin of socks* stands behind you.\n"
                                );
                                WaitForInteraction();
                                Console.Clear();
                                TypeWrite("What do we have here\n" +
                                    "Is that a little boy i peer\n" +
                                    "Why must you test my faith\n" +
                                    "with theese hands i will scathe\n" +
                                    "Are you ready to meet your doom.\n" +
                                    "The game of rock, paper, scissors shall be inscribed on your tomb\n"
                                );
                                WaitForInteraction();
                                if (Combat(inventory))
                                {
                                    TypeWrite("*Gorb the palladin of socks* hands you a small key. The key smells of old, damp socks.\n" +
                                    "If you want to find your sock you must search the darkest corners of the manor, where no light may reach\n");
                                    inventory.AddItem(Item.Key);
                                }
                                else
                                {
                                    TypeWrite("You have lost the game and are thrown out of the room\n");
                                }
                            }
                            WaitForInteraction();
                            currentRoom = Room.UpperFloor;
                            break;
                        case Room.Kitchen:
                            //battle with rock/paper/scissors to get the Map for the basement.
                            if (inventory.HasItem(Item.Map))
                            {
                                Console.Clear();
                                TypeWrite("You have already recieved your price you silly goose.\n" +
                                    "You leave the kitchen.");
                            }
                            else
                            {
                                Console.Clear();
                                TypeWrite("You enter the Kitchen\n" +
                                    "It reeks of spices and mixtures. If you didn't read the sign it could be misstaken for a lab.\n" +
                                    "Jars filled with extinct species and animal parts you have only heard of in stories. You pick up a saltshaker that says \"Unicorn dust\" on it.\n" +
                                    "As soon as your about to put it back you hear a voice behind you.\n"
                                );
                                WaitForInteraction();
                                Console.Clear();
                                TypeWrite("Oh, challanger take a seat.\n" +
                                    "It's time for a change of beat\n" +
                                    "Into my kitchen you have stumbled\n" +
                                    "Let's see how much you fumbled\n" +
                                    "May your fate be decided \n" +
                                    "with a game of rock, paper, scissors divided\n"
                                );
                                WaitForInteraction();
                                if (Combat(inventory))
                                {
                                    TypeWrite("The chef eyes you up and down, smirks and says:\n" +
                                    "Well arent you a spicy little fellow. I have something for you, i used this back in my adventure days.\n" +
                                    "It shows you the way to what your heart desires, and more!\n");
                                    inventory.AddItem(Item.Map);
                                    TypeWrite("You leave the kitchen.\n");
                                }
                                else
                                {
                                    TypeWrite("You have lost the game and are thrown out of the room\n");
                                }
                            }
                            WaitForInteraction();
                            currentRoom = Room.UpperFloor;
                            break;
                    }
                }

                // Ask the player if they want to play again
                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(false).Key == ConsoleKey.Y);
        }
    }
}
