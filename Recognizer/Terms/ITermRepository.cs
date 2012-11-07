using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recognizer.Helper;

namespace Recognizer.Terms
{
    interface ITermRepository
    {
        IEnumerable<Term> GetAll();

        void Add(Term term);

        IEnumerable<Term> FindTermsByPOS(POS pos);

        IEnumerable<Term> FindByWord(string word);
    }
}
