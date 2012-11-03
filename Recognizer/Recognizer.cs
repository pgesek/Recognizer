using System.Collections.Generic;
using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.Parser;
using Recognizer.IO;
using Recognizer.NLP;
using Recognizer.Util;
using Recognizer.Glossary;
using Recognizer.BoW;

namespace Recognizer
{
    class Recognizer
    {
        private WordNetEngine wordnet;
        private OpenNLPService nlp;
        private IGlossary glossary;

        private BagOfWords bow;

        private string input;

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
            bow = new BagOfWords();
        }

        public void Run(IInputReader inputReader, IInputReader glossaryReader)
        {
            glossary = new DefaultGlossary(glossaryReader, Properties.Settings.Default.ReadGlossaryIds);
            
            input = inputReader.ReadInput();

            calcBagOfWords();

            IEnumerable<Parse> sentences = nlp.ParseInput(input);
        }

        private void calcBagOfWords()
        {
            IEnumerable<string> sentences = nlp.DetectSentences(input);
            foreach (string sentence in sentences)
            {
                IEnumerable<string> words = nlp.Tokenize(sentence);
                foreach (string word in words)
                {
                    bow.AddWord(word.ToLower());
                }
            }
        }
    }
}
