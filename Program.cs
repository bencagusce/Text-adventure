using TextAdventure;

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
    enum RockPaperScissors
    {
        Rock,
        Paper,
        Scissors,
    }
    
    class Program
    {
        // Goose names taken from a previous github project
        static string[] gooseNames = {"Alastair","Antoinette","Archibald","Elizabeth","Alexander","Bartholomew","Christopher","Anastasia","Angelica","Benedict","Evangeline","Alexandra","Cordelia","Annabelle","Constantine","Abraham","Katherine","Sebastian","Remington","Alexander","Mackenzie","Gwendolyn","Adelaide","Victoria","Jacqueline","Ferdinand","Montgomery"};
        static string[] gooseSuccession = {"I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII","XIII","XIV","XV","XVI","XVII","XVIII","XXIV","XXXVIII","CCCXII"};
        static string[] gooseTitle = {"emperor","king","queen","prince","princess","lord","tzar","legal guardian","laird","dame","lady","chief","conqueror","challenger","accuser","kaiser","ruler","overseer","captain","father","mother","head","destroyer","chancellor","president","prime minister","mayor","connoisseur","owner","master","mistress","pope","arch bishop"};
        static string[] gooseSubjects = {"pebble","snacks","sand dune","rock","beach crabs","pebbles","toes","antidisestablishmentarianism","reed","pine cone","satanism","mindfullness","big stick","democracy","goose goose duck"};

        static string[] itemNames = {"Empty","Basement map","Basement key","Amulet of mind reading","Lucky sock"};

        static Random rng = new Random ();

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
            while(!int.TryParse("" + Console.ReadKey(), out input) || input < 0 || input > inventory.Length)
            {
                Console.WriteLine("Please enter a valid number");   
            }

            string textOutput = $"you replaced {itemNames[(int)inventory[input]]}.";
            inventory[input] = addedItem;
            Console.WriteLine(textOutput + $"with {addedItem}. {itemNames[(int)addedItem]}");
            return true;
      
        }
        static bool Combat(ref Item[] inventory)
        {
            // bool combatDone = true; 
            Console.WriteLine("Rock, Paper, Scissors Fight!");
            bool hasAmulet = HasItem(ref inventory, Item.Amulet);
            Console.WriteLine("1. Rock \n2. Paper \n3. Scissors");
            if (hasAmulet)
            {
                Console.WriteLine($"4. Use {itemNames[(int)Item.Amulet].ToLower()}");
            }
            int enemyChoice = rng.Next(3);
            Console.WriteLine(enemyChoice);

            Console.ReadKey();
            return true;
            
                // continue                 
                // continue                 
                // continue                 
                // continue                 
                // continue                 
                // continue                 
                // continue                 
                // continue                                 
                // continue          
            
            
            // Combat logic
        }

        static void TypeWrite(string text){
            for(int i = 0; i < text.Length; i++){
                Console.Write(text[i]);
                if(text[i] == ',' || text[i] == '.' || text[i] == '!' || text[i] == '?' || text[i] == '\n'){
                    System.Threading.Thread.Sleep(200);
                }else{
                    System.Threading.Thread.Sleep(20);
                }
            }
        }

        static void Main()
        {
            do {
                Room currentRoom = Room.Hallway;
                
                Item[] inventory = {Item.None, Item.None, Item.None};
                
                // -----
                // INTRO
                // -----
                Console.WriteLine("Welcome to the BencaGusce Text Adventure!\nWhat is the name of our adventurer?\n");
                string name = Console.ReadLine();
                if (name == null) name = "Bob McEwen";
                Console.WriteLine(
                    $"This is the story of {name} who lost their left sock in the washing machine.\n" +
                    $"This Sock was very important to {name} as it was their favourite lucky left sock.\n" +
                    $"In {name}'s desperate predicament they decided to visit the manor of lost left socks in search of their so missed sock.\n\n" +
                    "Press any key to continue...\n"
                );
                Console.ReadKey();       

                while(currentRoom != Room.End)
                {
                    Console.Clear();
                    switch (currentRoom)
                    {
                        case Room.Hallway:
                        {
                            Console.WriteLine(
                                "You enter the grand hallway of the manor.\n"+
                                "1. Go to the basement.\n"+
                                "2. Go to the upper floor.\n"+
                                "3. Exit the manor.\n" +
                                "press a number to choose an option"
                            );
                            bool validAnswer = false;
                            while (!validAnswer) 
                            {
                                validAnswer = true;                    
                                switch ("" + Console.ReadKey().Key)
                                {
                                    case "D1":
                                    {
                                        if (HasItem(ref inventory, Item.Key))
                                        {
                                            Console.WriteLine ("You use your key to open the door and enter.");
                                            currentRoom = Room.Basement;
                                        }
                                        else 
                                        {
                                            Console.WriteLine("It seems that the door is locked");
                                        }
                                        break;
                                    }
                                    case "D2":
                                    {
                                        Console.WriteLine("You go up the stairway to the second floor. ");
                                        currentRoom = Room.UpperFloor;
                                        break;
                                    }
                                    case "D3":
                                    {                                       
                                        if (HasItem(ref inventory, Item.Sock))
                                        {
                                            Console.WriteLine ("You leave the manor with your sock.");
                                            currentRoom = Room.End;
                                        }
                                        else 
                                        {
                                            Console.WriteLine("It seems your foot is unwiling to leave without it's sock");
                                        }
                                        break;
                                    }                               
                                    default: 
                                    {
                                        Console.WriteLine("Please select a valid number. ");
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
                            Console.WriteLine(
                                "You go up the stairs and enter the upper floors. Before you Hangs a sign with some beatufilly inscirbed letters\n" +
                                "+---------------------------------+\n" +
                                "|   <-- Library ▲ Chamber -->     |\n" +
                                "|            Kitchen              |\n" +
                                "|                                 |\n" +
                                "+---------------------------------+\n\n" +
                                "1. Go to the Library.\n"+
                                "2. Go to the Chamber.\n"+
                                "3. Exit the Kitchen.\n" +
                                "press a number to choose an option"
                            );
                            bool validAnswer = false;
                            while (!validAnswer) 
                            {
                                validAnswer = true;                    
                                switch ("" + Console.ReadKey())
                                {
                                    case "1": //solve a riddle and get the mind reading amulet
                                    {
                                        Console.WriteLine("you enter the Library\n" + 
                                        " In the library you're surrounded by books and bookshelves as far as you can see.");
                                        //intro to library, 
                                        //riddle
                                        // if (riddle == answer)
                                        // {
                                        //     Console.WriteLine("you have guessed correctly, take this"); //or something idk

                                        // }



                                        break;
                                    }
                                    case "2":                                  
                                    {
                                        Console.WriteLine("you enter the Chamber");
                                        break;
                                    }
                                    case "3":
                                    {  
                                        Console.WriteLine("you enter the Kitchen");
                                        break;
                                    }                               
                                    default: 
                                    {
                                        Console.WriteLine("Please select a valid number. ");
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
            } while("" + Console.ReadKey() == "y");
        }
    }    
}
