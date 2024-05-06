namespace CapitalCities { 

public class Game
    {
        private string selectedRegion;
        private List<Capitals> filteredCapitals;
        private CapitalData capitalData;

        public Game()
        {
            capitalData = new CapitalData();
        }

        public void Start()
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("What range would you like to play?");
                Console.WriteLine("1. The entire world");
                Console.WriteLine("2. America");
                Console.WriteLine("3. Europe");
                Console.WriteLine("4. Asia");
                Console.WriteLine("5. Africa");

                // Prompt user for input
                Console.Write("Enter your choice (or 'stop' to quit): ");
                string userInput = Console.ReadLine();

                if (userInput.Equals("stop", StringComparison.InvariantCultureIgnoreCase))
                {
                    EndGame();
                    break; // Exit the loop
                }

                // Determine the selected region based on user input
                switch (userInput)
                {
                    case "1":
                        selectedRegion = "The entire world";
                        break;
                    case "2":
                        selectedRegion = "America";
                        break;
                    case "3":
                        selectedRegion = "Europe";
                        break;
                    case "4":
                        selectedRegion = "Asia";
                        break;
                    case "5":
                        selectedRegion = "Africa";
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        continue; // Restart the loop
                }

                List<Capitals> allCapitals = capitalData.ReadCapitalCitiesFromCSV("capitalCities.csv");
                filteredCapitals = capitalData.FilterCapitalsByRegion(allCapitals, selectedRegion);

                PlayGame();
            }
        }

        private void PlayGame()
        {
            int correctCount = 0;
            int wrongCount = 0;

            // Shuffle the list of capitals
            var rnd = new Random();
            filteredCapitals = filteredCapitals.OrderBy(c => rnd.Next()).ToList();

            foreach (var capital in filteredCapitals)
            {
                
                Console.WriteLine($"What is the capital city of {capital.Country}?");

                // Get user input for the guess
                string userGuess = Console.ReadLine();

                // Check if the user wants to stop midgame
                if (userGuess.Equals("stop", StringComparison.InvariantCultureIgnoreCase))
                {
                    EndGame();
                    return; // Exit the method
                }

                // Check if the guess is correct
                if (userGuess.Equals(capital.Capital, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Correct!");
                    correctCount++;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Incorrect. The correct capital city of {capital.Country} is {capital.Capital}.");
                    wrongCount++;
                }
            }

            double successRate = (double)correctCount / (correctCount + wrongCount) * 100;

            // Save game result to CSV file
            capitalData.SaveGameResult(DateTime.Now, correctCount, wrongCount, successRate, selectedRegion);

            EndGame();
        }

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Saving current game data...");
            Console.WriteLine("The game has stopped.");

            // Ask user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine();

            bool playAgain = playAgainInput.Equals("yes", StringComparison.InvariantCultureIgnoreCase);

            if (playAgain)
            {
                Start(); // Start a new game
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Goodbye!");
            }
        }
    }
}
