using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace EnglishSelfTraining.ValueObjects
{
    public class Day : ValueObject
    {
        public Day(int value)
        {
            Value = value;
        }
        public int Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
