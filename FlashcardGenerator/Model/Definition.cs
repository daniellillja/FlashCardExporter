using System.Collections.Generic;

namespace FlashcardGenerator.Model
{
    public class Definition
    {
        public string FullText { get; set; }
        public List<Definition> Subdefinitions { get; set; }
        public string Term {
            get
            {
                return FullText.Split(':')[0].Trim();
            }}
        public string Description
        {
            get
            {
                return FullText.Split(':')[1].Trim();
            }
        }
    }
}
