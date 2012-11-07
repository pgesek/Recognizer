using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Terms
{
    class FlatRepository : ITermRepository
    {
        private List<Term> terms = new List<Term>();

        #region ITermRepository Members

        public IEnumerable<Term> GetAll()
        {
            return terms;
        }

        public void Add(Term term)
        {
            terms.Add(term);
        }

        public IEnumerable<Term> FindTermsByPOS(Helper.POS pos)
        {
            var result = from term in terms
                         where term.PoS == pos
                         select term;
            return result;
        }

        public IEnumerable<Term> FindTermsByWordnetPOS(WordNetEngine.POS pos)
        {
            var result = from term in terms
                         where term.PoS.FitsWordnetPOS(pos)
                         select term;
            return result;
        }

        public IEnumerable<Term> FindByWord(string word)
        {
            var result = from term in terms
                         where term.Word == word
                         select term;
            return result;
        }

        #endregion
    }
}
