using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LAIR.ResourceAPIs.WordNet;
using LAIR.Collections.Generic;
using Recognizer.Util;
using Recognizer.IO;
using Recognizer.Terms;

namespace Recognizer
{
    class Recognizer
    {
        private WordNetEngine wordnet;

        private Dictionary<string, Term> nouns = new Dictionary<string, Term>();

        public Recognizer()
        {
            string wordnetDir = Properties.Settings.Default.WordnetDir;
            bool inMemory = Properties.Settings.Default.WordnetInMemory;

            wordnet = new WordNetEngine(wordnetDir, inMemory);
        }

        public void Run(IInputReader reader)
        {
            Parse(reader.ReadInput());
        }

        public void Parse(IEnumerable<string> sentences)
        {
            foreach (string sentence in sentences)
            {
                ParseSentence(sentence);
            }
        }

        private void ParseSentence(string sentence)
        {
            string trimmedSentence = StringUtil.TrimSentence(sentence);
            string[] words = trimmedSentence.Split(' ');
            
            foreach (string word in words)
            {
                ParseWord(word);
            }
        }

        private void ParseWord(string word)
        {
            ParseAsNoun(word);    
        }

        private void ParseAsNoun(string word)
        {
            Set<SynSet> synsets = wordnet.GetSynSets(word, WordNetEngine.POS.Noun);
            foreach (SynSet synset in synsets) 
            {
                if (nouns.ContainsKey(synset.ID))
                {
                    nouns[synset.ID].Count++;
                }
                else
                {
                    nouns[synset.ID] = new Term(synset);
                }
            }
        }
    }
}
