using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.BoW
{
    class BagOfWords : Dictionary<string, int>
    {
        public void AddWord(string word)
        {
            if (this.ContainsKey(word))
            {
                this[word]++;
            }
            else
            {
                this[word] = 1;
            }
        }

        public IEnumerable<string> ByCount()
        {
            var result = from entry in this
                         orderby entry.Value
                         select entry.Key;
            return result;
        }
    }
}
