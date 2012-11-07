using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;
using Recognizer.Helper;

namespace Recognizer.Terms
{
    class Term
    {
        public string Word { get; set; }

        public POS PoS { get; set; }

        public SynSet Synset { get; set; }

        public int Occurences { get; set; }

        public string ID { get { return (Synset == null) ? null : Synset.ID; } }
    }
}
