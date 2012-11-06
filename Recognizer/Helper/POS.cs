using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAIR.ResourceAPIs.WordNet;

namespace Recognizer.Helper
{
    class POS 
    {
        /*    
        CC    Coordinating conjunction  RP    Particle
        CD    Cardinal number           SYM   Symbol
        DT    Determiner                TO    to
        EX    Existential there         UH    Interjection
        FW    Foreign word              VB    Verb, base form
        IN    Preposition/subordinate   VBD   Verb, past tense
                conjunction
        JJ    Adjective                 VBG   Verb, gerund/present participle
        JJR   Adjective, comparative    VBN   Verb, past participle
        JJS   Adjective, superlative    VBP   Verb, non-3rd ps. sing. present
        LS    List item marker          VBZ   Verb, 3rd ps. sing. present
        MD    Modal                     WDT   wh-determiner
        NN    Noun, singular or mass    WP    wh-pronoun
        NNP   Proper noun, singular     WP$   Possessive wh-pronoun
        NNPS  Proper noun, plural       WRB   wh-adverb
        NNS   Noun, plural            	``    Left open double quote
        PDT   Predeterminer             ,     Comma
        POS   Possessive ending         ''    Right close double quote
        PRP   Personal pronoun          .     Sentence-final punctuation
        PRP$  Possessive pronoun        :     Colon, semi-colon
        RB    Adverb                    $     Dollar sign
        RBR   Adverb, comparative       #     Pound sign
        RBS   Adverb, superlative       -LRB- Left parenthesis *
                                        -RRB- Right parenthesis *
         
        * The Penn Treebank uses the ( and ) symbols, but these are used elsewhere by the OpenNLP parser.
        */

        public enum Parts
        {
            CC, CD, DT, EX, FW, IN, JJ, JJR, JJS, LS, MD, NN, NNP, NNPS, NNS, PDT, POS, PRP, PRPDOLLAR, RB,
            RBR, RBS, RP, SYM, TO ,UH, VB, VBD, VBG, VBN, VBP, VBZ, WDT, WP, WPDOLLAR, WRB, DOUBLE_QUOTE, COMMA, RIGHT_DOUBLE_QUOTE,
            SENTENCE_FINAL, COLON, DOLLAR, POUND, LRB, RRB
        }

        private readonly Parts _self;

        private static readonly Parts[] nouns = new Parts[] { Parts.NN, Parts.NNP, Parts.NNPS, Parts.NNS };
        private static readonly Parts[] verbs = new Parts[] { Parts.VB, Parts.VBD, Parts.VBN, Parts.VBZ, Parts.VBP, Parts.VBG };
        private static readonly Parts[] adverbs = new Parts[] { Parts.RB, Parts.RBR, Parts.RBS, Parts.WRB };
        private static readonly Parts[] adjectives = new Parts[] { Parts.JJ, Parts.JJR, Parts.JJS };

        public POS(string tag)
        {
            _self = FromString(tag);
        }

        public Parts Value { get { return _self; } }

        public WordNetEngine.POS ForWordnet()
        {
            if (IsNoun())
            {
                return WordNetEngine.POS.Noun;
            }
            else if (IsVerb())
            {
                return WordNetEngine.POS.Verb;
            }
            else if (IsAdverb())
            {
                return WordNetEngine.POS.Adverb;
            }
            else if (IsAdjective())
            {
                return WordNetEngine.POS.Adjective;
            }
            else
            {
                return WordNetEngine.POS.None;
            }
        }

        public bool IsNoun()
        {
            return nouns.Contains(_self);
        }

        public bool IsVerb()
        {
            return verbs.Contains(_self);
        }

        public bool IsAdverb()
        {
            return adverbs.Contains(_self);
        }

        public bool IsAdjective()
        {
            return adjectives.Contains(_self);
        }

        private Parts FromString(string tag)
        {
            switch (tag)
            {
                case "PRP$":
                    return Parts.PRPDOLLAR;
                case "WP$":
                    return Parts.WPDOLLAR;
                case "``":
                    return Parts.DOUBLE_QUOTE;
                case ",":
                    return Parts.COMMA;
                case "''":
                    return Parts.RIGHT_DOUBLE_QUOTE;
                case ".":
                    return Parts.SENTENCE_FINAL;
                case ":":
                    return Parts.COLON;
                case "$":
                    return Parts.DOLLAR;
                case "#":
                    return Parts.POUND;
                case "-LRB-":
                    return Parts.LRB;
                case "-RRB-":
                    return Parts.RRB;
                default:
                    return (Parts) Enum.Parse(typeof(Parts), tag);
            }
        }
    }    
}
