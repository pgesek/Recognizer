using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recognizer.Glossary;

namespace Recognizer.Exec
{
    interface IExecution
    {
        void Run(string input, IGlossary glossary);
    }
}
