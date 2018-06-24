using FlashcardGenerator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace FlashcardGenerator.Tests.Data
{
    [TestClass]
    public class MindmapReaderTests
    {
        [TestMethod]
        public void Read_populates_title_from_root_element_text()
        {
            var assembly = typeof(MindmapReaderTests).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("FlashcardGenerator.Tests.Data.mindmap.mm");
            Assert.IsNotNull(resource);

            var sut = new MindmapReader(resource);
            var deck = sut.Read();
            Assert.AreEqual("root", deck.Title);
        }

        [TestMethod]
        public void Read_populates_cards_in_tree()
        {
            var assembly = typeof(MindmapReaderTests).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("FlashcardGenerator.Tests.Data.mindmap.mm");
            Assert.IsNotNull(resource);

            var sut = new MindmapReader(resource);
            var deck = sut.Read();
            var cards = deck.Definitions;
            Assert.AreEqual(2, cards.Count);
            Assert.AreEqual("term1: def1", cards[0].FullText);
            Assert.AreEqual("term2: def2", cards[1].FullText);

            Assert.AreEqual("term1-1: def1-1", cards[0].Subdefinitions[0].FullText);
            Assert.AreEqual("term2-1: def2-1", cards[1].Subdefinitions[0].FullText);
            Assert.AreEqual("term1-2: def1-2", cards[0].Subdefinitions[1].FullText);
            Assert.AreEqual("term1-1-1:def1-1-1", cards[0].Subdefinitions[0].Subdefinitions[0].FullText);
        }
    }
}
