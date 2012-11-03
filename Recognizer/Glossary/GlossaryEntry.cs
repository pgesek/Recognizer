using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Glossary
{
    class GlossaryEntry : IGlossaryEntry
    {
        private string _word;
        private string _definition;
        private string _ID;

        #region IGlossaryEntry Members

        public string Word
        {
           get { return _word; } 
        }

        public string Definition
        {
            get { return _definition; }
        }

        public string ID
        {
            get { return _ID; }
        }

        #endregion
        
        public GlossaryEntry(string ID, string word, string definition)
        {
            _ID = ID;
            _word = word;
            _definition = definition;
        }

    }
}
