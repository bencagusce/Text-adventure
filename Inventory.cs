namespace TextAdventure
{
    public class Inventory
    {
        Item[] contents = { Item.None, Item.None, Item.None };

        public bool hasRecievedCash { get; private set; } = false;

        /// <summary>
        /// Checks if the player has a specific item in their inventory. If removeCash is true, cash will be removed from the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="removeCash"></param>
        /// <returns></returns>
        public bool HasItem(Item item, bool removeCash = false)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i] == item)
                {
                    if (contents[i] == Item.Cash && removeCash) contents[i] = Item.None;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds an item to the player's iventory
        /// </summary>
        /// <param name="addedItem"></param>
        /// <returns></returns>
        public void AddItem(Item addedItem)
        {
            // If the player has recieved cash, they can't pick up more cash
            if (addedItem == Item.Cash) hasRecievedCash = true;

            // Find an empty slot in the inventory and add the item there
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i] == Item.None)
                {
                    contents[i] = addedItem;
                    Program.TypeWrite($"\nYou have picked up *{GlobalVariables.itemNames[(int)addedItem]}*!\n");
                    return;
                }
            }

            // If the inventory is full, ask the player to throw out an item
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

            // Replace the item in the inventory with the new item
            string textOutput = $"\nYou replaced {GlobalVariables.itemNames[(int)contents[input]]} ";
            contents[input] = addedItem;
            Console.WriteLine(textOutput + $"with {GlobalVariables.itemNames[(int)addedItem]}");
            return;

        }
    }
}