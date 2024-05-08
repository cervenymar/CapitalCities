namespace CapitalCities {

    public class Game
    {
        public string? selectedRegion;
        public List<Capitals>? filteredCapitals;
        public CapitalData capitalData;
        int correctCount = 0;
        int wrongCount = 0;
        public Game()
        {
            capitalData = new CapitalData();
        }

        public bool selectGame()
        {
            var invalidAnswer = true;
            Console.WriteLine("Would you like to guess:\n1. Capitals\nor\n2. Countries?");
            while (invalidAnswer)
            {
                var response = Console.ReadLine();
                switch (response)
                {
                    case "stop":
                        EndGame();
                        break;
                    case "1":
                        return true;
                    case "2":
                        return false;
                    default:
                        Console.WriteLine("Invalid answer. Write 1 or 2");
                        break; //end switch
                }

            }

            return false;
        }
        public String selectRange()
        {
            Console.Clear();
            Console.WriteLine("What range would you like to play?");
            Console.WriteLine("1. The entire world");
            Console.WriteLine("2. America");
            Console.WriteLine("3. Europe");
            Console.WriteLine("4. Asia");
            Console.WriteLine("5. Africa");
            Console.WriteLine("Enter your choice (or 'stop' to quit): ");
            
            // Determine the selected region based on user input
            var invalidAnswer = true;
            while (invalidAnswer)
            {
                string? userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "stop":
                        EndGame();
                        break;
                    case "1":
                        return "The entire world";

                    case "2":
                        return "America";

                    case "3":
                        return "Europe";

                    case "4":
                        return "Asia";

                    case "5":
                        return "Africa";
                    default:
                        Console.WriteLine("Invalid choice! Write 1 to 5 or stop to quit.");
                        break;
                }

            }
            return "";
        }
        public void Start()
        {
                var range = selectRange();                
            
                CapitalGuessingGame();

        }

            private void EndGame()
            {
                Console.Clear();
                Console.WriteLine("Saving current game data...");
                Console.WriteLine("The game has stopped.");

                // Ask user if they want to play again
                Console.Write("Do you want to play again? (yes/no): ");
                string? playAgainInput = Console.ReadLine();
                if (playAgainInput == "yes")
                {
                    
                }   

                bool playAgain = playAgainInput.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
                switch(playAgainInput)
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
        

        private void CapitalGuessingGame()
        {

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
    }
}
