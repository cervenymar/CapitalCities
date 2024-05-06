using CapitalCities;

public class CapitalData
{
    public List<Capitals> ReadCapitalCitiesFromCSV(string filePath)
    {
        List<Capitals> capitalList = new List<Capitals>();

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

                capitalList.Add(capital);
            }
        }

        return capitalList;
    }

    public List<Capitals> FilterCapitalsByRegion(List<Capitals> capitals, string selectedRegion)
    {
        List<Capitals> filteredCapitals = new List<Capitals>();

        if (selectedRegion.ToLower() == "the entire world")
        {
            // If selected region is "The entire world", return all capitals
            filteredCapitals = capitals;
        }
        else
        {
            // Filter capitals based on selected region
            foreach (var capital in capitals)
            {
                if (capital.Continent.Equals(selectedRegion, StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredCapitals.Add(capital);
                }
            }
        }

        return filteredCapitals;
    }

    public void SaveGameResult(DateTime timestamp, int correctGuesses, int wrongGuesses, double successRate, string selectedRegion)
    {
        string filePath = "gameResults.csv";

        // Create or append to the CSV file
        using (var writer = new StreamWriter(filePath, true))
        {
            // Format the date time stamp
            string formattedTimestamp = timestamp.ToString("yyyy-MM-dd HH:mm:ss");

            // Write the game result to the CSV file
            writer.WriteLine($"{formattedTimestamp},{selectedRegion},{correctGuesses},{wrongGuesses},{successRate:F2}");
        }
    }
}