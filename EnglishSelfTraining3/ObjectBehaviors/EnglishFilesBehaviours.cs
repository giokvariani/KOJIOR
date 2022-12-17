using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.Objects;
using EnglishSelfTraining.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSelfTraining.ObjectBehaviors
{
    public class EnglishFilesBehaviours
    {
        public static EnglishFilesBehaviours Create(IReadOnlyCollection<EnglishFile> englishFiles) => new EnglishFilesBehaviours(englishFiles);
        private EnglishFilesBehaviours(IReadOnlyCollection<EnglishFile> englishFiles) => EnglishFiles = englishFiles;
        private IReadOnlyCollection<EnglishFile> EnglishFiles { get; }
        public List<TrasnlateAttempt> GetTrasnlateAttempts() => EnglishFiles
            .SelectMany(x => x.GetResults())
            .Select(x => x.ResultOfExpression.Value
                .Use(expression => 
                    new TrasnlateAttempt(expression.Georgian, expression.English, string.Empty, false, x.FilePath, expression)))
            .ToList();
    }
}
