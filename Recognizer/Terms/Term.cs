using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Terms
{
    class Term
    {
        public string Word { get; set; }

        public string PoS { get; set; }

        public SynSet Synset { get; set; }

        public int Occurences { get; set; }

        public string ID { get { return Synset.ID ?? null; } }
    }
}
