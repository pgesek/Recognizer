using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Util
{
    static class FileUtils
    {
        public static string WithSeparator(string path)
        {
            return (path.EndsWith(Path.DirectorySeparatorChar.ToString())) ? path : path + Path.DirectorySeparatorChar;
        }
    }
}
