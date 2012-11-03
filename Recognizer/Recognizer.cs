using System.Collections.Generic;
using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.Parser;
using Recognizer.IO;
using Recognizer.NLP;
using Recognizer.Util;
using Recognizer.Glossary;

namespace Recognizer
{
    class Recognizer
    {
        private WordNetEngine wordnet;
        private OpenNLPService nlp;
        private IGlossary glossary;

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

        public void Run(IInputReader inputReader, IInputReader glossaryReader)
        {
            glossary = new DefaultGlossary(glossaryReader, Properties.Settings.Default.ReadGlossaryIds);
            
            string input = inputReader.ReadInput();

            IEnumerable<Parse> sentences = nlp.ParseInput(input);
        }
    }
}
