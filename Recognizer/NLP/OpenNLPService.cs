using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenNLP.Tools.Chunker;
using OpenNLP.Tools.Parser;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using Recognizer.Util;

namespace Recognizer.NLP
{
    class OpenNLPService : INLPService
    {
        private string _modelDir;

        public string ModelDir 
        {
            get
            {
                return _modelDir;
            }
            set
            {
                _modelDir = value;
                initComponents();
            }
        }

        private EnglishMaximumEntropySentenceDetector sentenceDetector;
        private EnglishMaximumEntropyTokenizer tokenizer;
        private EnglishMaximumEntropyPosTagger posTagger;
        private EnglishTreebankChunker chunker;
        private EnglishTreebankParser parser;
        
        public OpenNLPService(string modelDir) 
        {
            ModelDir = modelDir;
        }

        private void initComponents()
        {
            sentenceDetector = new EnglishMaximumEntropySentenceDetector(Path.Combine(ModelDir, "EnglishSD.nbin"));
            tokenizer = new EnglishMaximumEntropyTokenizer(Path.Combine(ModelDir, "EnglishTok.nbin"));
            posTagger = new EnglishMaximumEntropyPosTagger(Path.Combine(ModelDir, "EnglishPOS.nbin"));
            chunker = new EnglishTreebankChunker(Path.Combine(ModelDir, "EnglishChunk.nbin"));
            parser = new EnglishTreebankParser(FileUtils.WithSeparator(ModelDir), true, false);
        }

        #region INLPService Members

        public IEnumerable<string> DetectSentences(string input)
        {
            return sentenceDetector.SentenceDetect(input);
        }

        public IEnumerable<string> Tokenize(string sentence)
        {
            return tokenizer.Tokenize(sentence);
        }

        public IEnumerable<string> PosTag(IEnumerable<string> tokens)
        {
            return posTagger.Tag(tokens.ToArray());
        }

        public string Chunk(IEnumerable<string> tokens, IEnumerable<string> tags)
        {
            return chunker.GetChunks(tokens.ToArray(), tags.ToArray());
        }

        public IEnumerable<string> ChunkInput(string input)
        {
            IEnumerable<string> sentences = DetectSentences(input);
            foreach (string sentence in sentences)
            {
                IEnumerable<string> tokens = Tokenize(sentence);
                IEnumerable<string> posTags = PosTag(tokens);

                yield return Chunk(tokens, posTags);
            }
        }

        #endregion

        public Parse ParseSentence(string sentence)
        {
            return parser.DoParse(sentence);
        }

        public IEnumerable<Parse> ParseInput(string input)
        {
            IEnumerable<string> sentences = DetectSentences(input);
            foreach (string sentence in sentences)
            {
                yield return parser.DoParse(sentence);
            }
        }
    }
}
