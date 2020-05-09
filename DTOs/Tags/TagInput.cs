using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Tags
{
    public class TagInput
    {
        // public string Name { get; set; } // intern technical // will be generated

        public string Title { get; set; }

        public string Name 
        { 
            get
            {
                // tags[a-zA-Z0-9-] => _ wird zu leer, case-insensitive
                string tmpName = Title.ToLowerInvariant().Trim().Replace(" ", "_");
                return Regex.Replace(tmpName, "[^a-zA-Z0-9_-]+", "#");
            } 
        }
    }
}
