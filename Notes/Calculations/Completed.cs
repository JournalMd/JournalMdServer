using JournalMdServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Notes.Calculations
{
    public class Completed : ICalculation
    {
        public string Calculate(Dictionary<string, string> values)
        {
            string description = values["description"].ToLower(); // X => x

            bool openTodos = description.Contains("- [ ]") || description.Contains("- []") || description.Contains("* [ ]") || description.Contains("* []");
            bool closedTodos = description.Contains("- [x]") || description.Contains("* [x]");

            // completed: only closed tasks or no tasks at all
            if((closedTodos && !openTodos) || (!openTodos && !closedTodos))
                return "true";

            return "false";
        }
    }
}
