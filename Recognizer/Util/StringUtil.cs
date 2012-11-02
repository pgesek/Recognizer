using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Recognizer.Util
{
    static class StringUtil
    {
        private const string SENTENCE_TRIM_PATTERN = "[\\.,;\\\"\\(\\)\\-:]";

        public static string TrimSentence(string sentence)
        {
            return Regex.Replace(sentence, SENTENCE_TRIM_PATTERN, string.Empty);
        }
    }
}
