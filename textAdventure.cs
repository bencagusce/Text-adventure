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
        Goose,
        Sock
    }
    enum Rock/Paper/Scissors
    {
        Rock,
        Paper,
        Scissors,
    }
    
    class Program
    {
        string[] gooseNames = {"Alastair","Antoinette","Archibald","Elizabeth","Alexander","Bartholomew","Christopher","Anastasia","Angelica","Benedict","Evangeline","Alexandra","Cordelia","Annabelle","Constantine","Abraham","Katherine","Sebastian","Remington","Alexander","Mackenzie","Gwendolyn","Adelaide","Victoria","Jacqueline","Ferdinand","Montgomery"};
        string[] gooseSuccession = {"I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII","XIII","XIV","XV","XVI","XVII","XVIII","XXIV","XXXVIII","CCCXII"};
        string[] gooseTitle = {"emperor","king","queen","prince","princess","lord","tzar","legal guardian","laird","dame","lady","chief","conqueror","challenger","accuser","kaiser","ruler","overseer","captain","father","mother","head","destroyer","chancellor","president","prime minister","mayor","connoisseur","owner","master","mistress","pope","arch bishop"};
        string[] gooseSubjects = {"pebble","snacks","sand dune","rock","beach crabs","pebbles","toes","antidisestablishmentarianism","reed","pine cone","satanism","mindfullness","big stick","democracy","goose goose duck"};

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



        bool Combat(ref inventory)
        {
            bool hasAmulet = HasItem(ref inventory, Item.Amulet);
            
            // Combat logic
        }

        void Main()
        {
            do {
                Room currentRoom = Room.Hallway;
                
                Item[] inventory = new Item[2];I
                
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
                                "you are in hallway waaaaaa\n"+
                                "1. Go to the basement.\n"+
                                "2. Go to the upper floor.\n"+
                                "3. Exit the manor.\n" +
                                "press a number to choose an option"
                            );
                            bool validAnswer = false;
                            while (!validAnswer) 
                            {                                
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
                                        break;
                                    }
                                    case '3':
                                    {
                                        break;
                                    }                               
                                    default: 
                                    {
                                        Console.WriteLine("that is not ")
                                    }

                                }
                            }

                        }
                            break;
                        case Room.Basement:
                        {
                            
                        }
                            break;
                        case Room.UpperFloor:
                        {
                            
                        }
                            break;
                        case Room.Chambers:
                        {
                            
                        }
                            break;
                        case Room.Library:
                        {
                            
                        }
                            break;
                    }
                }

                Console.WriteLine("Do you want to play again? (y/n)");
            } while(Console.ReadKey() == "y");
        }
    }    
}
