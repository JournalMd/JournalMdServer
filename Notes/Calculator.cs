using JournalMdServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Notes.Calculations;
using JournalMdServer.Models;

namespace JournalMdServer.Notes
{
    public static class Calculator
    {
        private static Dictionary<string, ICalculation> calculations = new Dictionary<string, ICalculation>{
            { "bodymassindex", new BodyMassIndex() },
            { "ponderalindex", new PonderalIndex() },
            { "waisttohipratio", new WaistToHipRatio() },
            { "completed", new Completed() }
        };

        public static string CalculateField(string calculation, Note note, List<NoteField> noteFields)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var nField in noteFields)
            {
                values.Add(nField.Name, note.NoteValues.Single(nf => nf.NoteFieldId == nField.Id).Value);
            }
            values.Add("description", note.Description);
            values.Add("title", note.Title);

            return calculations[calculation].Calculate(values);
        }
    }
}
