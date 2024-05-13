using CapitalCities;
using System.Security.Cryptography.X509Certificates;

class Program
{
    public static void Main(string[] args)
    {
        Startup();        
    }
    public static void Startup()
    {
        //initialize, null past Game instances
        Game game = null;
        var invalidAnswer = true;
        while (invalidAnswer)
        {
            Console.Clear();
            Console.WriteLine("Would you like to play a game? (yes/no)");
            string? playAgainInput = Console.ReadLine().ToLower();
            switch (playAgainInput)
            {
                case "yes":
                    invalidAnswer = false;
                    game = new Game();
                    game.StartGame();
                    break;
                case "no":
                    invalidAnswer = false;
                    System.Environment.Exit(0);
                    continue;
                default:
                    Console.WriteLine("Invalid answer.");
                    break;
            }
            break;

        }
    
    }

}