using System.Collections.Generic;

namespace FlashcardGenerator.Model
{
    public class Definition
    {
        public string FullText { get; set; }
        public List<Definition> Subdefinitions { get; set; }
    }
}
