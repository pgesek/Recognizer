using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.IO
{
    class StdinSentenceReader : ISentenceReader
    {
        #region ISentenceReader Members

        public IEnumerable<string> ReadInput()
        {
            string line;
            while ((line = Console.ReadLine()) != "\\q") 
            {
                yield return line;
            }
        }

        #endregion
    }
}
