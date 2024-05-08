using CapitalCities;

class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
            Game game = new Game();
            game.Start();

            Console.WriteLine("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine();

            playAgain = playAgainInput.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}