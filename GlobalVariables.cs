namespace TextAdventure
{
    public enum Room
    {
        End,
        Hallway,
        Basement,
        UpperFloor,
        Chamber,
        Library,
        Kitchen
    }

    public enum Item
    {
        None,
        Map,
        Key,
        Amulet,
        Cash,
        Sock,
        Goose
    }
    public class GlobalVariables
    {
        static Random rng = new Random();
        // Goose names taken from a previous github project
        private static string[] gooseNames = { "Alastair", "Antoinette", "Archibald", "Elizabeth", "Alexander", "Bartholomew", "Christopher", "Anastasia", "Angelica", "Benedict", "Evangeline", "Alexandra", "Cordelia", "Annabelle", "Constantine", "Abraham", "Katherine", "Sebastian", "Remington", "Alexander", "Mackenzie", "Gwendolyn", "Adelaide", "Victoria", "Jacqueline", "Ferdinand", "Montgomery" };
        private static string[] gooseSuccession = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XXIV", "XXXVIII", "CCCXII" };
        private static string[] gooseTitle = { "emperor", "king", "queen", "prince", "princess", "lord", "tzar", "legal guardian", "laird", "dame", "lady", "chief", "conqueror", "challenger", "accuser", "kaiser", "ruler", "overseer", "captain", "father", "mother", "head", "destroyer", "chancellor", "president", "prime minister", "mayor", "connoisseur", "owner", "master", "mistress", "pope", "arch bishop" };
        public static string[] gooseSubjects { get; private set; }= { "pebble", "snacks", "sand dune", "rock", "beach crabs", "pebbles", "toes", "antidisestablishmentarianism", "reed", "pine cone", "satanism", "mindfullness", "big stick", "democracy", "goose goose duck" };
        public static string[] itemNames = {
            "Empty",
            "Basement map",
            "Basement key",
            "Amulet of mind reading",
            "Shiny cash",
            "Lucky sock",
            $"{gooseNames[rng.Next(gooseNames.Length)]} {gooseNames[rng.Next(gooseNames.Length)]} {gooseSuccession[rng.Next(gooseSuccession.Length)]}, the {gooseTitle[rng.Next(gooseTitle.Length)]} of {gooseSubjects[rng.Next(gooseSubjects.Length)]}"
        };
        public static (int, int) basementSize { get; private set; } = (10, 10);
        public static (int, int) sockPosition { get; private set; } = (rng.Next(basementSize.Item1), rng.Next(basementSize.Item2));
        public static (int, int) goosePosition { get; private set; } = (rng.Next(basementSize.Item1), rng.Next(basementSize.Item2));

        /// <summary>
        /// Re-randomizes the goose's name
        /// </summary>
        public static void ReRollGooseName ()
        {
            itemNames[(int)Item.Goose] = $"{gooseNames[rng.Next(gooseNames.Length)]} {gooseNames[rng.Next(gooseNames.Length)]} {gooseSuccession[rng.Next(gooseSuccession.Length)]}, the {gooseTitle[rng.Next(gooseTitle.Length)]} of {gooseSubjects[rng.Next(gooseSubjects.Length)]}";
        }
    }
}