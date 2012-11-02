using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LAIR.ResourceAPIs.WordNet;
using LAIR.Collections.Generic;
using Recognizer.IO;

namespace Recognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Recognizer recognizer = new Recognizer();
            IInputReader reader = new StdinReader();

            recognizer.Run(reader);
        }
    }
}
