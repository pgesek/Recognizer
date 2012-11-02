using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Recognizer.IO
{
    class FileSentenceReader : ISentenceReader
    {
        private string filePath;
        
        public FileSentenceReader(string filePath)
        {
            this.filePath = filePath;
        }

        #region ISentenceReader Members

        public IEnumerable<string> ReadInput()
        {
            return File.ReadLines(filePath);
        }

        #endregion
    }
}
