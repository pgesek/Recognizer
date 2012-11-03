using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognizer.Glossary 
{
    class DefaultGlossary : IGlossary
    {
        private ISet<IGlossaryEntry> glossary = new HashSet<IGlossaryEntry>();

        #region IGlossary Members

        public void Add(IGlossaryEntry entry)
        {
            CheckForDuplicate(entry.ID, entry.Word);
            glossary.Add(entry);
        }

        public void Add(string word, string definition)
        {
            string ID = GenerateID();
            Add(new GlossaryEntry(ID, word, definition));
        }

        public IGlossaryEntry Get(string ID)
        {
            IGlossaryEntry result = (from entry in glossary
                                     where entry.ID == ID
                                     select entry).FirstOrDefault();
            return result;
        }

        public IEnumerable<IGlossaryEntry> GetAll()
        {
            return glossary.AsEnumerable();
        } 

        public void DelEntry(IGlossaryEntry entry)
        {
            glossary.Remove(entry);
        }

        public void DelEntry(string ID)
        {
            IGlossaryEntry entry = Get(ID);
            if (entry != null)
            {
                glossary.Remove(entry);
            }
        }

        public IGlossaryEntry FindWord(string word)
        {
            IGlossaryEntry result = (from entry in glossary
                                     where entry.Word == word
                                     select entry).FirstOrDefault();
            return result;
        }

        #endregion

        private void CheckForDuplicate(string ID, string word)
        {
            IGlossaryEntry entry = Get(ID);
            
            if (entry == null)
            {
                entry = FindWord(word);
            }

            if (entry != null)
            {
                throw new DuplicateEntryException(String.Format("Found duplicate - ID: {0}, word: {1}", entry.ID, entry.Word));
            }
        }

        private string GenerateID()
        {
            int id = 0; // for TryParse

            int maxID  = (from entry in glossary
                      where int.TryParse(entry.ID, out id)
                      select id).Max();

            return (maxID++).ToString();
        }
    }
}
