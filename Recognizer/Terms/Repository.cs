using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recognizer.Helper;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Terms
{
    class Repository : ITermRepository
    {
        private Dictionary<string, Term> terms = new Dictionary<string, Term>();

        #region ITermRepository Members

        public IEnumerable<Term> GetAll()
        {
            return terms.Values;
        }

        public void Add(Term term)
        {
            if (terms.ContainsKey(term.ID))
            {
                terms[term.ID].Occurences++;
            }
        }

        public IEnumerable<Term> FindTermsByPOS(POS pos)
        {
            var result = from term in terms.Values
                         where term.PoS == pos
                         select term;
            return result;
        }

        public IEnumerable<Term> FindTermsByWordnetPOS(WordNetEngine.POS pos)
        {
            var result = from term in terms.Values
                         where term.PoS.FitsWordnetPOS(pos)
                         select term;
            return result;
        }

        public IEnumerable<Term> FindByWord(string word)
        {
            var result = from term in terms.Values
                         where term.Word == word
                         select term;
            return result;
        }

        #endregion
    }
}
