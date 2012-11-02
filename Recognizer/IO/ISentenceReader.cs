using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.IO
{
    interface ISentenceReader
    {
        IEnumerable<String> ReadInput(); 
    }
}
