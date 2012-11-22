using System.Collections.Generic;
using System.Linq;
using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.Parser;
using Recognizer.IO;
using Recognizer.NLP;
using Recognizer.Util;
using Recognizer.Glossary;
using Recognizer.BoW;
using Recognizer.Log;
using Recognizer.Terms;
using Recognizer.Helper;
using Recognizer.Exec;

namespace Recognizer
{
    class Recognizer
    {
        private WordNetEngine wordnet;
        private OpenNLPService nlp;
        private IGlossary glossary;

        private BagOfWords bow;
        private ITermRepository terms;

        private string input;

        private List<IExecution> executions;

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
            terms = new FlatRepository();
            
            executions = new List<IExecution>();
            executions.Add(new FirstExecution(wordnet, nlp));
        }

        public void Run(IInputReader inputReader, IInputReader glossaryReader)
        {
            glossary = new DefaultGlossary(glossaryReader, Properties.Settings.Default.ReadGlossaryIds);
            
            input = inputReader.ReadInput();

            foreach (IExecution execution in executions)
            {
                execution.Run(input, glossary);
            }
        }
    }
}
