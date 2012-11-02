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
        private const string WORNDET_DIR = @"C:\Users\Geso\workspace\mgr\WNdb-3.0\dict";
        private const bool IS_WORNET_IN_MEMORY = false;
        private const bool NOT_RECURSIVE = false;

        static void Main(string[] args)
        {
            Recognizer recognizer = new Recognizer(WORNDET_DIR, IS_WORNET_IN_MEMORY);
            ISentenceReader reader = new StdinSentenceReader();

            //recognizer.Run(reader);
            
            WordNetEngine wordnet = new WordNetEngine(WORNDET_DIR, IS_WORNET_IN_MEMORY);

            Set<SynSet> synsets = wordnet.GetSynSets("ass", WordNetEngine.POS.Noun);
            foreach (SynSet synset in synsets)
            {
                foreach (WordNetEngine.SynSetRelation relation in synset.SemanticRelations)
                {
                    Console.WriteLine(relation.ToString());
                    foreach (SynSet relatedSynset in synset.GetRelatedSynSets(relation, NOT_RECURSIVE))
                    {
                        Console.WriteLine(relatedSynset.Gloss);
                        foreach (String word in synset.Words)
                        {
                            Console.Write(word);
                            Console.Write(" ");

                        }
                        Console.WriteLine();
 
                    }
                }
                Console.WriteLine("-------------------------------------");
            }
            Console.ReadLine();
        }
    }
}
