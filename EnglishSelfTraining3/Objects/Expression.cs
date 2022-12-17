namespace EnglishSelfTraining.Objects
{
    public class Expression
    {
        public Expression(string english, string georgian, string originalValue)
        {
            English = english;
            Georgian = georgian;
            OriginalValue = originalValue;
        }
        public string English { get; }
        public string Georgian { get; }
        public string OriginalValue { get; }
    }
}
