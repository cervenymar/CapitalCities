using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCities
{
    public class CapitalGameResult
    {
        public DateTime Timestamp { get; set; }
        public string SelectedRegion { get; set; }
        public int CorrectGuesses { get; set; }
        public int WrongGuesses { get; set; }
        public double SuccessRate { get; set; }
    }
}
