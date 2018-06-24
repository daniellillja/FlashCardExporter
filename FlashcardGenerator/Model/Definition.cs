using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FlashcardGenerator.Model
{
    public class Definition
    {
        public string FullText { get; set; }
        public List<Definition> Subdefinitions { get; set; }
        static Regex TermExtractionRegex = new Regex(@"(.*?):\s*(.*)\s*$");
        public string Term
        {
            get
            {
                var matches = TermExtractionRegex.Match(FullText).Groups;
                return matches[1].Value;
            }
        }

        public string Description
        {
            get
            {
                var matches = TermExtractionRegex.Match(FullText).Groups;
                return matches[2].Value;
            }
        }
    }
}
