using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;
using Recognizer.Helper;

namespace Recognizer.Wordnet
{
    class MostUsedSelector : ISynsetSelector
    {

        #region ISynsetSelector Members

        public SynSet SelectSysnet(string word, POS pos, WordNetEngine wordnet)
        {
            return (pos.ForWordnet() == WordNetEngine.POS.None) ? null : wordnet.GetMostCommonSynSet(word, pos.ForWordnet());
        }

        #endregion
    }
}
