using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.NLP
{
    interface INLPService
    {
        IEnumerable<string> DetectSentences(string input);

        IEnumerable<string> Tokenize(string sentence);
        
        IEnumerable<string> PosTag(IEnumerable<string> tokens);

        string Chunk(IEnumerable<string> tokens, IEnumerable<string> tags);

        IEnumerable<string> ChunkInput(string input);
    }
}
