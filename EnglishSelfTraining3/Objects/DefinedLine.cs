using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSelfTraining.Objects
{
    public class DefinedLine
    {
        public DefinedLine(string originalValue, SentenceLevel level)
        {
            OriginalValue = originalValue;
            Level = level;
        }
        public string OriginalValue { get; }
        public SentenceLevel Level { get; }
    }
}
