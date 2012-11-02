using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.IO
{
    class StdinReader : IInputReader
    {
        #region ISentenceReader Members

        public string ReadInput()
        {
            StringBuilder sb = new StringBuilder();
            string line;

            while ((line = Console.ReadLine()) != "\\q") 
            {
                sb.AppendLine(line);    
            }

            return sb.ToString();
        }

        #endregion
    }
}
