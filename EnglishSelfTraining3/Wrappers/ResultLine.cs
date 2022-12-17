using CSharpFunctionalExtensions;
using System.Linq.Expressions;

namespace EnglishSelfTraining.Wrappers
{
    public class ResultLine
    {
        public ResultLine(Result<Objects.Expression> resultOfExpression, string value, string filePath)
        {
            ResultOfExpression = resultOfExpression;
            Value = value;
            FilePath = filePath;
        }
        public Result<Objects.Expression> ResultOfExpression { get; }
        public string Value { get; }
        public string FilePath { get; }
    }
}
