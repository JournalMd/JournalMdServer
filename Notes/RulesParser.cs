using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Notes
{
    public class RulesParser
    {
        private string rule;

        public RulesParser(string rule)
        {
            this.rule = rule;
        }

        // TODO IMPROVE a lot
        public string GetValue(string key)
        {
            var ruleKeyValue = rule.Split('='); // TODO - right now this only works if this has one rule ;-)
            if (ruleKeyValue[0] == key)
            {
                return ruleKeyValue[1];
            } else
            {
                return "";
            }
        }
    }
}
