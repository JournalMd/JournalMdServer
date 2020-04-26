using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Interfaces
{
    interface ICalculation
    {
        string Calculate(Dictionary<string, string> values);
    }
}
