using FlashcardGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashcardGenerator.Data
{
    public class OutlineTextExporter
    {
        public string TextExport(DefinitionOutline outline)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(outline.Title);
            foreach (var definition in outline.Definitions)
            {
                var maxDepth = outline.MaxDepth;
                BuildDefinition(definition, stringBuilder, maxDepth);
            }

            return stringBuilder.ToString();
        }

        private void BuildDefinition(Definition definition,
            StringBuilder stringBuilder, int maxDepth, int nodeLevel = 0)
        {
            // Build identation.
            var identation = "\t";
            for (int i = 0; i < nodeLevel; i++)
            {
                identation += "\t";
            }

            // Pad with central columns so that all descriptions
            // appear in same column.
            var numberOfPaddingColumns = maxDepth - nodeLevel;
            var centerPaddingColumns = string.Empty;
            for (int i = 0; i < numberOfPaddingColumns; i++)
            {
                centerPaddingColumns += "\t";
            }

            stringBuilder.Append($"{identation}{definition.Term}{centerPaddingColumns}{definition.Description}");
            stringBuilder.AppendLine();

            if (definition.Subdefinitions != null && definition.Subdefinitions.Any())
            {
                foreach (var sd in definition.Subdefinitions)
                {
                    BuildDefinition(sd, stringBuilder, maxDepth, nodeLevel + 1);
                }
            }
        }
    }
}
