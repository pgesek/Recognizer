using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Recognizer.IO
{
    class FileReader : IInputReader
    {
        private string filePath;
        private Encoding encoding;

        public FileReader(string filePath) : this(filePath, Encoding.Default) { }

        public FileReader(string filePath, Encoding encoding)
        {
            this.filePath = filePath;
            this.encoding = encoding;
        }

        #region ISentenceReader Members

        public string ReadInput()
        {
            return File.ReadAllText(filePath, encoding);
        }

        #endregion
    }
}
