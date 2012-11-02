using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Terms
{
    class Term
    {
        public SynSet Synset { get; set; }
        public int Count { get; set; }

        public Term(SynSet synset)
        {
            this.Synset = synset;
            this.Count = 1;
        }

        /*public bool IsHypernym(SynSet synset)
        {
            var hypernyms = from s in Synsets 
                            where s.GetRelatedSynSets(WordNetEngine.SynSetRelation.Hypernym, true).Contains(synset)
                            select s;
            return hypernyms.Count() > 0;
        }

        public void AddWord(SynSet synset)
        {
            if (!Synsets.Contains(synset))
            {
                Synsets.Add(synset);
            }
            Count++;
        }

        public bool WordFits(SynSet synset)
        {
            return Synsets.Contains(synset) || IsHypernym(synset);
        }*/
    }
}
