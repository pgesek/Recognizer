using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.Collections.Generic;
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
        private readonly FlatRepository flatRepo = new FlatRepository();
        private readonly Repository terms = new Repository();

        #region IExecution Members

        public FirstExecution(WordNetEngine wordnet, INLPService nlp, IGlossary glossary)
            :base(wordnet, nlp, glossary) 
        {
            bow = new BagOfWords();
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
                    flatRepo.Add(term);
                }
            }
        }

        protected override SynSet SelectSynset(string word, POS pos)
        {
            SynSet result = base.SelectSynset(word, pos); // temporary
            
            WordNetEngine.POS wordnetPos = pos.ForWordnet();
            if (wordnetPos != WordNetEngine.POS.None)
            {
                Set<SynSet> synsets = wordnet.GetSynSets(word, wordnetPos);
                foreach (SynSet synset in synsets)
                {
                    // great algorythms will be added here       
                }
            }

            return result;
        }

        #endregion
    }
}
