using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Terms
{
    interface ITermRepository
    {
        IEnumerable<Term> GetAll();

        void Add(Term term);
    }
}
