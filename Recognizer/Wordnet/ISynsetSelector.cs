using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;
using Recognizer.Helper;

namespace Recognizer.Wordnet
{
    interface ISynsetSelector
    {
        SynSet SelectSysnet(string word, POS pos, WordNetEngine wordnet);
    }
}
