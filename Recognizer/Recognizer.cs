using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using LAIR.ResourceAPIs.WordNet;
using LAIR.Collections.Generic;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Parser;

using Recognizer.Util;
using Recognizer.IO;
using Recognizer.Terms;
using Recognizer.NLP;

namespace Recognizer
{
    class Recognizer
    {
        private WordNetEngine wordnet;
        private OpenNLPService nlp;

        private Dictionary<string, Term> nouns = new Dictionary<string, Term>();

        public Recognizer()
        {
            Init();
        }

        public void Init()
        {
            string wordnetDir = Properties.Settings.Default.WordnetDir;
            bool inMemory = Properties.Settings.Default.WordnetInMemory;
            string modelDir = Properties.Settings.Default.ModelDir;

            Init(wordnetDir, inMemory, modelDir);
        }

        public void Init(string wordnetDir, bool inMemory, string modelDir)
        {
            wordnet = new WordNetEngine(wordnetDir, inMemory);
            nlp = new OpenNLPService(modelDir);
        }

        public void Run(IInputReader reader)
        {
            string input = reader.ReadInput();

            IEnumerable<Parse> sentences = nlp.ParseInput(input);
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
