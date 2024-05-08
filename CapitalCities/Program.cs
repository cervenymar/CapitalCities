using CapitalCities;

class Program
{
    static void Main(string[] args)
    {       
        var playAgain = true;
        while (playAgain)
        {
            Console.WriteLine("Would you like to play a game? (yes/no)");
            var playAgainInput = Console.ReadLine();
            if (playAgainInput.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                playAgain = false;
                return;
            }else if (playAgainInput.ToLower() == "yes")
            {
                playAgain = true;
                Game game = new Game();
                game.Start();
            }else
            {
                Console.WriteLine("Invalid answer.");
            }
            
        }
        
    }
}