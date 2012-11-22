using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recognizer.IO;
using LAIR.ResourceAPIs.WordNet;
using Recognizer.Helper;

namespace Recognizer.Glossary 
{
    class DefaultGlossary : IGlossary
    {
        private ISet<IGlossaryEntry> glossary;

        public DefaultGlossary(IInputReader reader) : this(reader, true) { }

        public DefaultGlossary(IInputReader reader, bool readIds)
        {
            string input = reader.ReadInput();
            glossary = GlossaryParser.ParseInput(input,readIds);
        }

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

        public void ProcessSynsets(WordNetEngine wordnet)
        {
            foreach (IGlossaryEntry entry in glossary)
            {
                SynSet synset = FindSynset(entry, wordnet);
                entry.Synset = synset;
            }
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

            return (maxID + 1).ToString();
        }

        private SynSet FindSynset(IGlossaryEntry entry, WordNetEngine wordnet)
        {
            // TODO: find POS, use only nouns for now
            int? minDistance = null;
            SynSet result = null;
            foreach (SynSet synset in wordnet.GetSynSets(entry.Word, WordNetEngine.POS.Noun))
            {
                int distance = LevenshteinDistance.Compute(entry.Definition, synset.Gloss);
                if (minDistance == null || minDistance > distance)
                {
                    result = synset;
                }
            }
            return result;
        }
    }
}
