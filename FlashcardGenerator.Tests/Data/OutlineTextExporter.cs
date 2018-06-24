using FlashcardGenerator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace FlashcardGenerator.Tests.Data
{
    [TestClass]
    public class OutlineTextExporterTests
    {
        [TestMethod]
        public void Export_properly_formatted_document_string()
        {
            var assembly = typeof(MindmapReaderTests).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("FlashcardGenerator.Tests.Data.mindmap.mm");
            Assert.IsNotNull(resource);

            var reader = new MindmapReader(resource);
            var outline = reader.Read();

            var sut = new OutlineTextExporter();
            var text = sut.TextExport(outline);
            Console.WriteLine(text);
            var textLines = text.Split(Environment.NewLine);
            Assert.AreEqual("root", textLines[0]);
            Assert.AreEqual("\tterm1\t\t\tdef1", textLines[1]);
            Assert.AreEqual("\t\tterm1-1\t\tdef1-1", textLines[2]);
            Assert.AreEqual("\t\t\tterm1-1-1\tdef1-1-1", textLines[3]);
            Assert.AreEqual(3, outline.MaxDepth);
        }
    }
}
