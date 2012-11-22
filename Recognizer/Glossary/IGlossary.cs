using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Glossary
{
    interface IGlossary
    {
        void Add(IGlossaryEntry entry);

        void Add(string word, string definition);

        IGlossaryEntry Get(string ID);

        IEnumerable<IGlossaryEntry> GetAll();

        void DelEntry(IGlossaryEntry entry);

        void DelEntry(string ID);

        IGlossaryEntry FindWord(string word);

        void ProcessSynsets(WordNetEngine wordnet);
    }
}
