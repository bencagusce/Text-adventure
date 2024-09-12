namespace TextAdventure
{
    public class Inventory
    {
        Item[] contents = { Item.None, Item.None, Item.None };

        /// <summary>
        /// Checks if the player has a specific item in their inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasItem(Item item)
        {
            bool hasItem = false;
            foreach (Item i in contents)
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
        public bool AddItem(Item addedItem)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i] == Item.None)
                {
                    contents[i] = addedItem;
                    Program.TypeWrite($"\nYou have picked up *{GlobalVariables.itemNames[(int)addedItem]}*!\n");
                    return true;
                }
            }

            Console.WriteLine("Your inventory is full :(.\nEnter the number of the item you want to throw out.");
            for (int i = 0; i < contents.Length; i++)
            {
                Console.WriteLine($"{i+1}. {GlobalVariables.itemNames[(int)contents[i]]}");
            }            
            int input = -1;
            while (!int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out input) || input < 0 || input > contents.Length)
            {
                Console.WriteLine("Please enter a valid number");
            }
            input --;

            string textOutput = $"\nYou replaced {GlobalVariables.itemNames[(int)contents[input]]} ";
            contents[input] = addedItem;
            Console.WriteLine(textOutput + $"with {GlobalVariables.itemNames[(int)addedItem]}");
            return true;

        }
    }
}