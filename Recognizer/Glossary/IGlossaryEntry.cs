using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Glossary
{
    interface IGlossaryEntry
    {
        string Word { get; }
        
        string Definition { get; }
        
        string ID { get; }

        SynSet Synset { get; set; }
    }
}
