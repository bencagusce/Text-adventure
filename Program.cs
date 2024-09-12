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
        /// Checks if the player has a specific item in their inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        static bool HasItem(ref Item[] inventory, Item item)
        {
            bool hasItem = false;
            foreach (Item i in inventory)
            {
                if (i == item)
                {
                    hasItem = true;
                }
            }
            return hasItem;
        }

        /// <summary>
        /// Adds an item to the player's iventory
        /// </summary>
        /// <param name=""></param>
        /// <param name="item"></param>
        /// <returns></returns>
        static bool AddItem(ref Item[] inventory, Item addedItem)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == Item.None)
                {
                    inventory[i] = addedItem;
                    TypeWrite($"\nYou have picked up *{GlobalVariables.itemNames[(int)addedItem]}*!\n");
                    return true;
                }
            }

            Console.WriteLine("Your inventory is full :(.\nEnter the number of the item you want to throw out.");
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine($"{i+1}. {GlobalVariables.itemNames[(int)inventory[i]]}");
            }            
            int input = -1;
            while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out input) || input < 0 || input > inventory.Length)
            {
                Console.WriteLine("Please enter a valid number");
            }
            input --;

            string textOutput = $"\nYou replaced {GlobalVariables.itemNames[(int)inventory[input]]} ";
            inventory[input] = addedItem;
            Console.WriteLine(textOutput + $"with {GlobalVariables.itemNames[(int)addedItem]}");
            return true;

        }

        /// <summary>
        /// Rock, Paper, Scissors combat
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        static bool Combat(ref Item[] inventory)
        {
            string[] alternatives = { "Rock", "Paper", "Scissors" };

            int playerScore = 0;
            int enemyScore = 0;

            bool hasAmulet = HasItem(ref inventory, Item.Amulet);

            while (playerScore < 3 && enemyScore < 3)
            {
                Console.Clear();
                Console.WriteLine($"Player: {playerScore} - Enemy: {enemyScore}");
                TypeWrite("Rock, Paper, Scissors Fight!\n");
                Console.WriteLine("1. Rock \n2. Paper \n3. Scissors");
                // If the player has the amulet, they can read the the enemy's mind
                if (hasAmulet) Console.WriteLine($"4. Use {GlobalVariables.itemNames[(int)Item.Amulet].ToLower()}");
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

                    if (playerChoice == 3 && hasAmulet)
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

            if (playerScore == 3)
            {
                TypeWrite("You are victorious! One step closer to your sock!\n");
                return true;
            }
            else
            {
                TypeWrite("You Lost.\n1. Retry\n2. Leave the room\n");
                Console.WriteLine("\npress a number to choose an option...");

                bool validAnswer = false;
                while (!validAnswer)
                {
                    validAnswer = true;
                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.D1:
                            return Combat(ref inventory);
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
        /// "Press any key to continue..." functionality
        /// </summary>
        static void WaitForInteraction()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(false);
        }

        /// <summary>/
        /// Prints text letter by letter for dramatic effect
        /// </summary>
        /// <param name="text">The text to be printed</param>        
        static void TypeWrite(string text)
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
                //Thread.Sleep(slowLetters.Contains(text[i]) ? 200 : 20);
            }
        }
        static bool correctAnswer(int question)
        {
            int answer = 0;
            while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out answer) || 1 > answer || answer > 3)
            {
                Console.WriteLine("\nPlease enter a valid number");
            }
            if (answer == question)
            {
                TypeWrite("\nVery good, heres one more question: \n");
                return true;              
            }
            else
            {
                TypeWrite("\nIm dissapointed.\nYou are forced to leave.");
                return false;
            }
        }

        static void Main()
        {
            WaitForInteraction();
            do
            {
                Room currentRoom = Room.Hallway;

                Item[] inventory = { Item.None, Item.None, Item.None };

                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                //  AddItem(ref inventory, Item.Key);
                //  AddItem(ref inventory, Item.Map);
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING
                // CHEATING

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
                                            if (HasItem(ref inventory, Item.Key))
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
                                            if (HasItem(ref inventory, Item.Sock))
                                            {
                                                Console.Clear();
                                                TypeWrite("You leave the manor with your lucky sock.\n");
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
                            (int, int) playerPosition = (5, 0);
                            GlobalVariables.ReRollGooseName();
                            bool hasMap = HasItem(ref inventory, Item.Map);
                            bool hasAmulet = HasItem(ref inventory, Item.Amulet);
                            bool hasSock = HasItem(ref inventory, Item.Sock);
                            bool hasGoose = HasItem(ref inventory, Item.Goose);

                            Console.Clear();
                            TypeWrite("You enter the basement.\nIt is damp and too dark to see anything.\n");
                            WaitForInteraction();
                            Console.Clear();
                            
                            while(currentRoom == Room.Basement)
                            {
                                if (playerPosition == GlobalVariables.sockPosition && !hasSock)
                                {
                                    TypeWrite("You found your lucky sock!\n");
                                    AddItem(ref inventory, Item.Sock);
                                    hasSock = true;
                                    hasMap = HasItem(ref inventory, Item.Map);
                                    hasAmulet = HasItem(ref inventory, Item.Amulet);
                                    hasGoose = HasItem(ref inventory, Item.Goose);
                                    WaitForInteraction();
                                    Console.Clear();
                                }

                                if (playerPosition == GlobalVariables.goosePosition && !hasGoose)
                                {
                                    TypeWrite($"You have encountered the goose, *{GlobalVariables.itemNames[(int)Item.Goose]}*!\n");
                                    Console.WriteLine($"1. Pick up *{GlobalVariables.itemNames[(int)Item.Goose]}*");
                                    Console.WriteLine($"2. Leave *{GlobalVariables.itemNames[(int)Item.Goose]}* alone");
                                    if (hasAmulet) Console.WriteLine("3. Use the amulet of mind reading");
                                    Console.WriteLine("\npress a number to choose an option...");

                                    validAnswer = false;
                                    while(!validAnswer)
                                    {
                                        validAnswer = true;
                                        switch (Console.ReadKey(false).Key)
                                        {
                                            case ConsoleKey.D1:
                                                AddItem(ref inventory, Item.Goose);
                                                hasGoose = true;
                                                hasMap = HasItem(ref inventory, Item.Map);
                                                hasAmulet = HasItem(ref inventory, Item.Amulet);
                                                hasSock = HasItem(ref inventory, Item.Sock);
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

                                string[] options = {"h1. Go back to the hallway."};
                                if (playerPosition.Item1 != 0) options = [.. options, $"l{options.Length + 1}. Go left."];
                                if (playerPosition.Item1 != GlobalVariables.basementSize.Item1 - 1) options = [.. options, $"r{options.Length + 1}. Go right"];
                                if (playerPosition.Item2 != GlobalVariables.basementSize.Item2 - 1) options = [.. options, $"f{options.Length + 1}. Go forwards"];
                                if (playerPosition.Item2 != 0) options = [.. options, $"b{options.Length + 1}. Go backwards"];
                                if (hasMap) options = [.. options, $"m{options.Length + 1}. Use the map"];

                                foreach (string option in options)
                                {
                                    Console.WriteLine(option[1..]);
                                }

                                Console.WriteLine("\npress a number to choose an option...");

                                int input = 0;
                                while(!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out input) || input < 1 || input > options.Length)
                                {
                                    Console.WriteLine("Please enter a valid number");
                                }
                                
                                Console.Clear();

                                switch (options[input-1][0])
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

                        case Room.Chamber:
                            if(HasItem(ref inventory, Item.Key))
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
                                    "The game of rock,paper,scissors shall be inscribed on your tomb\n"
                                );
                                WaitForInteraction();
                                if (Combat(ref inventory))
                                {                                           
                                    TypeWrite("Gorb the palladin of socks hands you a small key. The key smells of old, damp socks.\n" +
                                    "If you want to find your sock you must search the darkest corners of the manor, where no light may reach\n");
                                    AddItem(ref inventory, Item.Key);
                                }
                                else
                                {
                                    TypeWrite("You have lost the game and are thrown out of the room\n");
                                }
                            }
                            WaitForInteraction();
                            currentRoom = Room.UpperFloor;
                            break;

                        case Room.Library:
                            //solve a riddle and get the mind reading amulet
                            if(HasItem(ref inventory, Item.Amulet))
                            {
                                Console.Clear();
                                TypeWrite("You have already recieved your price you silly goose.\n" +                                
                                    "You leave the library.");
                            }
                            else
                            {
                                Console.Clear();
                                TypeWrite("you enter the Library\n" +
                                    "In the library you're surrounded by books and bookshelves as far as you can see. In the middle of the room there's an elderly man surrounded \n" +
                                    "by towers of books. The wizards voice is deep and rumbles quietly\n"
                                );
                                WaitForInteraction();
                                TypeWrite("Have thee come to seek knowledge, oh sarig young one. Witan is no sin, though ignorance falls heavy on thy mind.\n" +
                                    "I shall put thy mind to a test. Wisdom always come with knowledge, and knowledge shan't go unnoticed.\n" +
                                    "Answer my three questions. And i shall gift you a very special *item* in return.\n" +
                                    "Question one: what is the tastiest icecream?\n" +
                                    "1. Chocolat icecream\n" +
                                    "2. Vanilla icecream\n" +
                                    "3. Sock-cream icecream\n");                                    
                                if (correctAnswer(2))
                                {
                                    TypeWrite("Question 2: What is the best fruit?\n" +
                                    "1. pineapple\n" +
                                    "2. kiwi \n" +
                                    "3. doritos\n");
                                    if (correctAnswer(1))
                                    {
                                        TypeWrite("Question 3: what is the footwear?\n" +
                                        "1. socks\n" +
                                        "2. shoes with socks\n" +
                                        "3. socks with shoes\n");   
                                        if (correctAnswer(3)) 
                                        {
                                            TypeWrite("You have answered 3 questions and 3 you got right.\n"+
                                            "Here is my gift to you, oh wise one.\n");
                                            AddItem(ref inventory, Item.Amulet);
                                            TypeWrite("Now skedadle!\n" +
                                            "You leave the library");                                                
                                        }                                            
                                    }
                                } 
                            }
                            WaitForInteraction();
                            currentRoom = Room.UpperFloor;
                            break;

                        case Room.Kitchen:
                            if(HasItem(ref inventory, Item.Map))
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
                                if (Combat(ref inventory))
                                {
                                    TypeWrite("The chef eyes you up and down, smirks and says:\n" +
                                    "Well arent you a spicy little fellow. I have something for you, i used this back in my adventure days.\n" +
                                    "It shows you the way to what your heart desires, and more!\n");
                                    AddItem(ref inventory, Item.Map);
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

                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(false).Key == ConsoleKey.Y);
        }
    }
}
