using JournalMdServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Notes.Calculations
{
    /// <summary>
    /// https://de.wikipedia.org/wiki/Ponderal-Index
    /// </summary>
    public class PonderalIndex : ICalculation
    {
        public string Calculate(Dictionary<string, string> values)
        {
            if (!values.ContainsKey("weight") || !double.TryParse(values["weight"], out double Weight))
                return "";

            if (!values.ContainsKey("height") || !double.TryParse(values["height"], out double Height))
                return "";

            if (Weight <= 0 || Height <= 0)
                return "";

            return Math.Round((Weight / (Height * Height * Height) * 1000000.0), 2).ToString();
        }
    }
}
