using CapitalCities;
using System.ComponentModel.DataAnnotations;

public class CapitalData
{
    List<Capitals> capitalList;
    public CapitalData() {
        this.capitalList = new List<Capitals>();
    }
    
    public List<Capitals> ReadCapitalCitiesFromCSV(string filePath)    {
        

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Capitals capital = new Capitals
                    {
                        Country = values[0],
                        Capital = values[1],
                        Continent = values[2]
                    };

                    this.capitalList.Add(capital);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File '{filePath}' not found. Error: {ex.Message}");
            // Handle the missing file scenario here
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading file '{filePath}': {ex.Message}");
            // Handle other IO related errors here
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Error: Missing data in file '{filePath}'. Ensure the file is not empty and formatted correctly.");
            // Handle the scenario where the file is empty or doesn't contain enough data
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            // Handle other unexpected errors here
        }

        return capitalList;
    }

    public List<Capitals> FilterCapitalsByRegion(List<Capitals> capitals, string selectedRegion)
    {
        List<Capitals> filteredCapitals = new List<Capitals>();
        switch (selectedRegion)
        {
            case "The entire world":
                filteredCapitals = capitals;
                break;
            default:
                foreach (var capital in capitals)
                {
                    if (capital.Continent.Contains(selectedRegion, StringComparison.InvariantCultureIgnoreCase))
                    {
                        filteredCapitals.Add(capital);
                    }
                }
                break;
        }        

        return filteredCapitals;
    }

    public void SaveGameResult(int correctGuesses, int wrongGuesses, string selectedRegion)
    {
        
        DateTime timestamp = DateTime.Now; 
        string filePath = "../../../gameResults.csv";     
               
        
        // Create or append to the CSV file
        using (var writer = new StreamWriter(filePath, true))
        {
            // Format the date time stamp
            string formattedTimestamp = timestamp.ToString("yyyy-MM-dd HH:mm:ss");

            // Write the game result to the CSV file
            writer.WriteLine($"{formattedTimestamp},{selectedRegion},{correctGuesses},{wrongGuesses},{this.successRate(correctGuesses, wrongGuesses)}");
        }
    }

    public string successRate(float correctGuesses, float wrongGuesses)
    {
        
        if (correctGuesses == 0)
            return "0%";
        else
            return $"{(correctGuesses / (correctGuesses + wrongGuesses))*100}%";
    }
}