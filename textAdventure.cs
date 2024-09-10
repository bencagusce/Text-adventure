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
    enum Rock/Paper/Scissors
    {
        Rock,
        Paper,
        Scissors,
    }
    
    class Program
    {
        // Goose names taken from a previous github project
        string[] gooseNames = {"Alastair","Antoinette","Archibald","Elizabeth","Alexander","Bartholomew","Christopher","Anastasia","Angelica","Benedict","Evangeline","Alexandra","Cordelia","Annabelle","Constantine","Abraham","Katherine","Sebastian","Remington","Alexander","Mackenzie","Gwendolyn","Adelaide","Victoria","Jacqueline","Ferdinand","Montgomery"};
        string[] gooseSuccession = {"I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII","XIII","XIV","XV","XVI","XVII","XVIII","XXIV","XXXVIII","CCCXII"};
        string[] gooseTitle = {"emperor","king","queen","prince","princess","lord","tzar","legal guardian","laird","dame","lady","chief","conqueror","challenger","accuser","kaiser","ruler","overseer","captain","father","mother","head","destroyer","chancellor","president","prime minister","mayor","connoisseur","owner","master","mistress","pope","arch bishop"};
        string[] gooseSubjects = {"pebble","snacks","sand dune","rock","beach crabs","pebbles","toes","antidisestablishmentarianism","reed","pine cone","satanism","mindfullness","big stick","democracy","goose goose duck"};

        string[] itemNames = {"Empty","Basement map","Basement key","Amulet of mind reading","Lucky sock"};

        bool HasItem(ref inventory, Item item)
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
        bool AddItem(ref inventory, Item item)
        {   
            foreach (Item i in inventory)
            {
                if (i == Item.None)
                {
                    i = item;
                    Console.WriteLine($"you have picked up {itemNames[item]}")
                    //addItem = true;
                    return true;                   
                }      
            }
            Console.WriteLine("Your inventory is full :(.\nEnter the number of the item you want to throw out.");
            foreach (Item i in inventory)
            {
                Console.WriteLine($"{i}. {itemNames[i]}");  
            }
            

            int input = -1;
            while(!int.TryParse(Console.ReadKey(), out input) || input < 0 || input > inventory.Length())
            {
                Console.WriteLine("Please enter a valid number");   
            }

            string textOutput = $"you replaced {itemNames[inventory[result]]}.";
            inventory[result] = item;
            Console.WriteLine(textOutput + $"with {i}. {itemNames[i]}");
            return true;
      
        }
        bool Combat(ref inventory)
        {
            bool hasAmulet = HasItem(ref inventory, Item.Amulet);
            
            
            // Combat logic
        }

        void Main()
        {
            do {
                Room currentRoom = Room.Hallway;
                
                Item[] inventory = {Item.None, Item.None, Item.None};
                
                // -----
                // INTRO
                // -----
                Console.WriteLine("Welcome to the BencaGusce Text Adventure!\nWhat is the name of our adventurer?");
                string name = Console.ReadLine();
                Console.WriteLine(
                    $"This is the story of {name} who lost their left sock in the washing machine.\n" +
                    $"This Sock was very important to {name} as it was their favourite lucky left sock.\n" +
                    $"In {name}'s desperate predicament they decided to visit the manor of lost left socks in search of their so missed sock.\n\n" +
                    "Press any key to continue..."
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
                                switch (Console.ReadKey())
                                {
                                    case '1':
                                    {
                                        if (HasItem(ref inventory, Item.Key))
                                        {
                                            Console.WriteLine ("You use your key to open the door and enter.");
                                            currentRoom = Room.Basement;
                                        }
                                        else 
                                        {
                                            Console.WriteLine("It seems that the door is locked");
                                            break;
                                        }

                                    }
                                    case '2':
                                    {
                                        Console.WriteLine("You go up the stairway to the second floor. ")
                                        currentRoom = Room.UpperFloor;
                                        break;
                                    }
                                    case '3':
                                    {                                       
                                        if (HasItem(ref inventory, Item.Sock))
                                        {
                                            Console.WriteLine ("You leave the manor with your sock.");
                                            currentRoom = Room.End;
                                        }
                                        else 
                                        {
                                            Console.WriteLine("It seems your foot is unwiling to leave without it's sock");
                                            break;
                                        }
                                        break;
                                    }                               
                                    default: 
                                    {
                                        Console.WriteLine("Please select a valid number. ");
                                        validAnswer = false;
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
                                "⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶\n" +
                                "⫪   <-- Library ▲ Chamber -->     ⫪\n" +
                                "Ⅱ            Kitchen              Ⅱ\n" +
                                "⫫                                 ⫫\n" +
                                "⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶⩶\n\n" +
                                "1. Go to the Library.\n"+
                                "2. Go to the Chamber.\n"+
                                "3. Exit the Kitchen.\n" +
                                "press a number to choose an option"
                            );
                            bool validAnswer = false;
                            while (!validAnswer) 
                            {
                                validAnswer = true;                    
                                switch (Console.ReadKey())
                                {
                                    case '1': //solve a riddle and get the mind reading amulet
                                    {
                                        Console.WriteLine("you enter the Library\n" + 
                                        " In the library you're surrounded by books and bookshelves as far as you can see.")
                                        //intro to library, 
                                        //riddle
                                        if (riddle == answer)
                                        {
                                            Console.WriteLine("you have guessed correctly, take this") //or something idk

                                        }



                                        break;
                                    }
                                    case '2':                                  
                                    {
                                        Console.WriteLine("you enter the Chamber")
                                        break;
                                    }
                                    case '3':
                                    {  
                                        Console.WriteLine("you enter the Kitchen")
                                        break;
                                    }                               
                                    default: 
                                    {
                                        Console.WriteLine("Please select a valid number. ");
                                        validAnswer = false;
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
            } while(Console.ReadKey() == "y");
        }
    }    
}
