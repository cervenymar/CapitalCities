using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapitalCities {

    public class Game
    {
        private int correctCount, wrongCount;        
        private string range, gamemode;

       private CapitalData Data = new CapitalData();
       private List<Capitals> gameData;

        public Game()
        {
            this.correctCount = 0;
            this.wrongCount = 0;
            this.range = this.selectRange();
            this.gamemode = this.selectGame();
            this.gameData = Data.FilterCapitalsByRegion(Data.ReadCapitalCitiesFromCSV("../../../capitalCities.csv"), this.range);
        }

        public void StartGame()
        {
            Console.Clear();
            if (this.gamemode == "Capitals")
            {
                
                Console.WriteLine("You are guessing the Capital Cities.");
                CapitalGuessingGame(this.gameData, this.correctCount, this.wrongCount);
            }
            else if(this.gamemode == "Country")
            {                
                Console.WriteLine("You are guessing the Countries based on Capital Cities.");
                CountryGuessingGame(this.gameData, this.correctCount, this.wrongCount);
            }
            this.EndGame();
        }

        private string selectGame()
        {
            var invalidAnswer = true;
            Console.WriteLine("Would you like to guess:\n1. Capitals\nor\n2. Countries?");
            while (invalidAnswer)
            {
                var response = Console.ReadLine();
                switch (response)
                {
                    case "stop":
                        this.EndGame();
                        break;
                    case "1":
                        return "Capitals";
                    case "2":
                        return "Country";
                    default:
                        Console.WriteLine("Invalid answer. Write 1 or 2");
                        break;
                }

            }

            return "";
        }
        private string selectRange()
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
                        this.EndGame();
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
        private void EndGame()
        {
            if (this.correctCount + this.wrongCount > 0)
                this.Data.SaveGameResult(this.correctCount, this.wrongCount, this.range);
            Console.Clear();
            Console.WriteLine("The game has stopped. Saving current game data...");
            Console.WriteLine($"Statistics:\n You have answered {this.correctCount+this.wrongCount} questions. {this.correctCount} of which were correct. Your success rate is {this.Data.successRate(this.correctCount,this.wrongCount)}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            //return to the beginning of the program
            Program.Startup();

        }


        private void CapitalGuessingGame(List<Capitals> filteredCapitals, int correctCount, int wrongCount)
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
                    this.EndGame();
                    return; // Exit the method
                }

                // Check if the guess is correct
                if (userGuess.Equals(capital.Capital, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Correct!");
                    this.correctCount++;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Incorrect. The correct capital city of {capital.Country} is {capital.Capital}.");
                    this.wrongCount++;
                }
                
            }
            
        }
        private void CountryGuessingGame(List<Capitals> filteredCapitals, int correctCount, int wrongCount)
            {

                // Shuffle the list of capitals
                var rnd = new Random();
                filteredCapitals = filteredCapitals.OrderBy(c => rnd.Next()).ToList();

                foreach (var country in filteredCapitals)
                {

                    Console.WriteLine($"What is the country of {country.Capital}?");

                    string? userGuess = Console.ReadLine();

                    // Check if the user wants to stop midgame
                    if (userGuess.Equals("stop", StringComparison.InvariantCultureIgnoreCase))
                    {
                        this.EndGame();
                        return; // Exit the method
                    }

                    // Check if the guess is correct
                    if (userGuess.Equals(country.Country, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.Clear();
                        Console.WriteLine("Correct!");
                        this.correctCount++;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Incorrect. The correct country of {country.Capital} is {country.Country}.");
                        this.wrongCount++;
                    }
                }                
            }   
        }
    }

