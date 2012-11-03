using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Glossary
{
    static class GlossaryParser
    {
        public static ISet<IGlossaryEntry> ParseInput(string input)
        {
            return ParseInput(input, true);
        }

        public static ISet<IGlossaryEntry> ParseInput(string input, bool readIds)
        {
            int currentID = 1;
            ISet<IGlossaryEntry> result = new HashSet<IGlossaryEntry>();

            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                string[] tokens = line.Split(new String[] { " " }, StringSplitOptions.None);

                string ID = (readIds) ? tokens[0] : (currentID++).ToString();
                string word = (readIds) ? tokens[1] : tokens[0];

                int skip = (readIds) ? 2 : 1;
                string definition = string.Join(" ", tokens.Skip(skip).TakeWhile(x => true));

                result.Add(new GlossaryEntry(ID, word, definition));
            }

            return result;
        }
    }
}
