using FlashcardGenerator.Model;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace FlashcardGenerator.Data
{
    public class MindmapReader
    {
        private const string NodeTag = "node";
        private const string Name = "TEXT";
        private readonly Stream _freemindXmlStream;
        private int _maxLevel;

        public MindmapReader(Stream freemindXmlStream)
        {
            _freemindXmlStream = freemindXmlStream;
        }

        public DefinitionOutline Read()
        {
            using (var reader = new XmlTextReader(_freemindXmlStream))
            {
                while (reader.NodeType != XmlNodeType.Element)
                    reader.Read();
                XElement xmlTree = XElement.Load(reader);

                var rootEl = xmlTree
                    .Elements(NodeTag)
                    .First();

                var title = rootEl.Attribute(Name).Value;
                var rootChildrenEls = rootEl.Elements(NodeTag);
                var defs = rootChildrenEls.Select(ParseDefinitionEl);

                var outline = new DefinitionOutline
                {
                    Title = title,
                    Definitions = defs.ToList(),
                    MaxDepth = _maxLevel+1
                };

                return outline;
            }
        }

        private Definition ParseDefinitionEl(XElement rootFirstChild, int level = 0)
        {
            _maxLevel = level;
            var subDefsEl = rootFirstChild.Elements(NodeTag);
            var fullText = rootFirstChild.Attribute(Name).Value;
            if (subDefsEl.Count() == 0)
            {
                return new Definition() { FullText = fullText };
            }

            return new Definition()
            {
                FullText = fullText,
                Subdefinitions = subDefsEl.Select(el => ParseDefinitionEl(el, level+1)).ToList()
            };
        }
    }
}
