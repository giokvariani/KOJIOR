using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Diagnostics;

namespace EnglishSelfTraining.ValueObjects
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public class Month : ValueObject
    {
        public Month(string name, int year, int value)
        {
            Value = value;
            Name = name;
            Year = year;
        }
        public string Name { get; }
        public int Year { get; }
        public int Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Year;
        }
        public static bool operator <(Month m1, Month m2) =>
            BothAreNotNull(m1, m2) && (m1.Year < m2.Year || (m1.Year == m2.Year && m1.Value < m2.Value));
        public static bool operator >(Month m1, Month m2) =>
            BothAreNotNull(m1, m2) && (m1.Year > m2.Year || (m1.Year == m2.Year && m1.Value > m2.Value));
        public static bool operator <=(Month m1, Month m2) =>
            BothAreNotNull(m1, m2) && (m1.Year < m2.Year || (m1.Year == m2.Year && m1.Value <= m2.Value));
        public static bool operator >=(Month m1, Month m2) =>
            BothAreNotNull(m1, m2) && (m1.Year > m2.Year || (m1.Year == m2.Year && m1.Value >= m2.Value));
        static bool BothAreNotNull(Month m1, Month m2) => m1 != null && m2 != null;
        public override string ToString() => $"{Value}-{Year}";
    }
}
