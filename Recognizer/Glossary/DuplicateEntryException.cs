using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Glossary
{
    class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message) { }
    }
}
