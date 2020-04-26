using JournalMdServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Notes.Calculations
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Waist%E2%80%93hip_ratio	
    /// https://de.wikipedia.org/wiki/Taille-H%C3%BCft-Verh%C3%A4ltnis	
    /// </summary>
    public class WaistToHipRatio : ICalculation
    {
        public string Calculate(Dictionary<string, string> values)
        {
            if (!values.ContainsKey("waist") || !double.TryParse(values["waist"], out double Waist))
                return "";

            if (!values.ContainsKey("hips") || !double.TryParse(values["hips"], out double Hips))
                return "";

            if (Waist <= 0 || Hips <= 0)
                return "";

            return Math.Round((Waist / Hips), 2).ToString();
        }
    }
}
