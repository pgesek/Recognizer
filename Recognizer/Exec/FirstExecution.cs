using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;
using Recognizer.NLP;
using Recognizer.Glossary;
using Recognizer.BoW;
using Recognizer.Terms;
using Recognizer.Helper;

namespace Recognizer.Exec
{
    class FirstExecution : ExecutionBase
    {
        #region IExecution Members

        public FirstExecution(WordNetEngine wordnet, INLPService nlp, IGlossary glossary)
            :base(wordnet, nlp, glossary) 
        {
            bow = new BagOfWords();
            terms = new FlatRepository();
        }

        public override void Run(string input )
        {
            IEnumerable<string> sentences = nlp.DetectSentences(input);
            foreach (string sentence in sentences)
            {
                IEnumerable<string> words = nlp.Tokenize(sentence);
                IEnumerable<string> tags = nlp.PosTag(words);

                foreach (Term term in words.Zip(tags, (word, tag) => new Term { PoS = new POS(tag), Word = word }))
                {
                    // bag of words
                    bow.AddWord(term.Word.ToLower());
                    // main stuff
                    term.Synset = SelectSynset(term.Word, term.PoS);
                    terms.Add(term);
                }
            }
        }

        #endregion
    }
}
