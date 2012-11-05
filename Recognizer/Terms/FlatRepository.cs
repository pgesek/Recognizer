using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion
    }
}
