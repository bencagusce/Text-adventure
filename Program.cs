using TextAdventure;
using System;
using System.Threading;

namespace TextAdventure
{
    enum Room
    {
        End,
        Hallway,
        Basement,
        UpperFloor,
        Chambers,
        Library,
        Kitchen
    }

    enum Item
    {
        None,
        Map,
        Key,
        Amulet,
        Sock,
        Goose
    }

    class Program
    {
        // Goose names taken from a previous github project
        static string[] gooseNames = { "Alastair", "Antoinette", "Archibald", "Elizabeth", "Alexander", "Bartholomew", "Christopher", "Anastasia", "Angelica", "Benedict", "Evangeline", "Alexandra", "Cordelia", "Annabelle", "Constantine", "Abraham", "Katherine", "Sebastian", "Remington", "Alexander", "Mackenzie", "Gwendolyn", "Adelaide", "Victoria", "Jacqueline", "Ferdinand", "Montgomery" };
        static string[] gooseSuccession = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XXIV", "XXXVIII", "CCCXII" };
        static string[] gooseTitle = { "emperor", "king", "queen", "prince", "princess", "lord", "tzar", "legal guardian", "laird", "dame", "lady", "chief", "conqueror", "challenger", "accuser", "kaiser", "ruler", "overseer", "captain", "father", "mother", "head", "destroyer", "chancellor", "president", "prime minister", "mayor", "connoisseur", "owner", "master", "mistress", "pope", "arch bishop" };
        static string[] gooseSubjects = { "pebble", "snacks", "sand dune", "rock", "beach crabs", "pebbles", "toes", "antidisestablishmentarianism", "reed", "pine cone", "satanism", "mindfullness", "big stick", "democracy", "goose goose duck" };

        static string[] itemNames = { "Empty", "Basement map", "Basement key", "Amulet of mind reading", "Lucky sock" };

        static ConsoleColor defaultConsoleColor = ConsoleColor.Gray;

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
                    Console.WriteLine($"you have picked up {itemNames[(int)addedItem]}");
                    return true;
                }
            }

            Console.WriteLine("Your inventory is full :(.\nEnter the number of the item you want to throw out.");
            foreach (Item item in inventory)
            {
                Console.WriteLine($"{item}. {itemNames[(int)item]}");
            }

            int input = -1;
            while (!int.TryParse("" + Console.ReadKey(), out input) || input < 0 || input > inventory.Length)
            {
                Console.WriteLine("Please enter a valid number");
            }

            string textOutput = $"you replaced {itemNames[(int)inventory[input]]}.";
            inventory[input] = addedItem;
            Console.WriteLine(textOutput + $"with {addedItem}. {itemNames[(int)addedItem]}");
            return true;

        }

        /// <summary>
        /// Rock, Paper, Scissors combat
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        static bool Combat(ref Item[] inventory)
        {
            Console.Clear();

            string[] alternatives = { "Rock", "Paper", "Scissors" };

            bool hasAmulet = HasItem(ref inventory, Item.Amulet);
            
            // bool combatDone = true; 
            TypeWrite("Rock, Paper, Scissors Fight!\n");
            TypeWrite("1. Rock \n2. Paper \n3. Scissors\n");
            // If the player has the amulet, they can read the the enemy's mind
            if (hasAmulet) TypeWrite($"4. Use {itemNames[(int)Item.Amulet].ToLower()}\n");
            Console.WriteLine("\npress a number to choose an option...");

            // Enemy choice
            int enemyChoice = rng.Next(3);

            

            
            
            // Console.WriteLine(alternatives[enemyChoice]);

            WaitForInteraction();
            return true;
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
            WaitForInteraction();
            do
            {
                Room currentRoom = Room.Hallway;

                Item[] inventory = { Item.None, Item.None, Item.None };

                // -----
                // INTRO
                // -----
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;

                TypeWrite("Welcome to the BencaGusce Text Adventure!\nWhat is the name of our adventurer?\n");
                string name = Console.ReadLine() ?? "Bob McEwen";
                TypeWrite(
                    $"This is the story of *{name}* who lost their left sock in the washing machine.\n" +
                    $"This Sock was very important to {name} as it was their favourite lucky left sock.\n" +
                    $"In {name}'s desperate predicament they decided to visit the manor of lost left socks in search of their so missed sock.\n"
                );
                WaitForInteraction();

                while (currentRoom != Room.End)
                {
                    Console.Clear();
                    switch (currentRoom)
                    {
                        case Room.Hallway:
                            {
                                TypeWrite(
                                    "You enter the grand hallway of the manor.\n" +
                                    "1. Go to the basement.\n" +
                                    "2. Go to the upper floor.\n" +
                                    "3. Exit the manor.\n"
                                );
                                Console.WriteLine("\npress a number to choose an option...");

                                bool validAnswer = false;
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
                                                    Console.WriteLine("You leave the manor with your lucky sock.\n");
                                                    currentRoom = Room.End;
                                                    WaitForInteraction();
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("It seems your foot is unwiling to leave without it's sock\n");
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
                            }

                        case Room.Basement:
                            {

                                break;
                            }
                        case Room.UpperFloor:
                            {
                                TypeWrite("You go up the stairs and enter the upper floors. Before you Hangs a sign with some beatufilly inscirbed letters\n");
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

                                bool validAnswer = false;
                                while (!validAnswer)
                                {
                                    validAnswer = true;
                                    switch (Console.ReadKey(false).Key)
                                    {
                                        case ConsoleKey.D1:
                                            {
                                                //solve a riddle and get the mind reading amulet
                                                //Console.ForegroundColor  = ConsoleColor.Yellow; 
                                                Console.Clear();
                                                TypeWrite("you enter the Library\n" +
                                                    "In the library you're surrounded by books and bookshelves as far as you can see. In the middle of the room there's an elderly man surrounded \n" +
                                                    "by towers of books. The wizards voice is deep and rumbles quietly\n"
                                                );
                                                WaitForInteraction();
                                                TypeWrite("Have thee come to seek knowledge, oh sarig young one. Witan is no sin, though ignorance falls heavy on thy mind.\n" +
                                                    "I shall put thy mind to a test. Wisdom always come with knowledge, and knowledge shan't go unnoticed.\n" +
                                                    "Answer my three questions. And i shall gift you a very special *item* in return."
                                                );
                                                //questions:
                                                // error code blaaaa

                                                //intro to library, 
                                                //riddle
                                                // if (riddle == answer)
                                                // {
                                                //     Console.WriteLine("you have guessed correctly, take this"); //or something idk

                                                // }



                                                break;
                                            }
                                        case ConsoleKey.D2:
                                            {
                                                TypeWrite("You enter the Chambers\n" +
                                                    "The giant room is laced with paintings and expensive decorations of only the most aftersought of socks, but sadly not yours.\n" +
                                                    "As you look around you see a giant bed the size of your room standing tall in the middle of the room.\n" +
                                                    "\"BANG\"\n"
                                                );
                                                WaitForInteraction();
                                                TypeWrite("What do we have here\n" +
                                                    "Is that a little boy i peer\n" +
                                                    "Why must you test my faith\n" +
                                                    "with theese hands i will scathe\n" +
                                                    "Are you ready to meet your doom.\n" +
                                                    "The game of rock,paper,scissors shall be inscribed on your tomb\n"
                                                );
                                                break;
                                            }
                                        case ConsoleKey.D3:
                                            {
                                                Console.Clear();
                                                TypeWrite("You enter the Kitchen\n" +
                                                    "It reeks of spices and mixtures. If you didn't read the sign it could be misstaken for a lab.\n" +
                                                    "Jars filled with extinct species and animal parts you have only heard of in stories. You take down a saltchaker that says \"Unicorn dust\" on it.\n" +
                                                    "As soon as your about to put it back you hear a voice behind you."
                                                );
                                                WaitForInteraction();
                                                TypeWrite("Oh, challanger take a seat.\n" +
                                                    "It's time for a change of beat\n" +
                                                    "Into my kitchen you have stumbled\n" +
                                                    "Let's see how much you fumbled\n" +
                                                    "May your fate be decided \n" +
                                                    "with a game of rock, paper, scissors divided\n"
                                                );
                                                WaitForInteraction();
                                                Combat(ref inventory);
                                                break;
                                            }
                                        case ConsoleKey.D4:
                                            {
                                                Console.Clear();
                                                TypeWrite("You go back down to the hallway.\n");
                                                currentRoom = Room.Hallway;
                                                WaitForInteraction();
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
                            }
                        case Room.Chambers:
                            {
                                break;
                            }
                        case Room.Library:
                            {
                                break;
                            }
                        case Room.Kitchen:
                            {
                                break;
                            }
                    }
                }

                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(false).Key == ConsoleKey.Y);
        }
    }
}
