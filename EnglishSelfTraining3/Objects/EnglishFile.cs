using CSharpFunctionalExtensions;
using EnglishSelfTraining.ValueObjects;
using EnglishSelfTraining.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EnglishSelfTraining.Objects
{
    public class DefinedEnglishFile : EnglishFile
    {
        public DefinedEnglishFile(string path) : base(path)
        {
            var level = Path.Split("\\").Last().Split(' ').Last().Split('.').First();
            Level = (SentenceLevel)Enum.Parse(typeof(SentenceLevel), level);
            LevelString = Level.ToString();
        }
        protected override string GetDayKey
        {
            get
            {
                var level = Path.Split("\\").Last().Split(' ').Last().Split('.').First();
                var a = Path.Replace($" {level.ToString()}", string.Empty);
                return a;
            }
        }

        public SentenceLevel Level { get; }
        public string LevelString { get; }
    }

    [DebuggerDisplay("{" + nameof(Path) + "}")]
    public class EnglishFile
    {
        public EnglishFile(string path)
        {
            Lines = System.IO.File.ReadLines(path).ToList().AsReadOnly();
            Path = path;
            //var level = Path.Split("\\").Last().Split(' ').Last().Split('.').First();
            Day = new Day(Convert.ToInt32(new string(GetDayKey.Split('\\').Last().Where(x => char.IsDigit(x)).ToArray())));
        }
        protected virtual string GetDayKey => Path; 
        public string Path { get; }
        public Day Day { get; }
        public IReadOnlyCollection<string> Lines { get; }
        public IReadOnlyCollection<ResultLine> GetResults()
        {
            return Lines.Select(line =>
            {
                var result = Result.Try(() =>
                {
                    var a = Path;
                    var tookExpression = line.Split('/');
                    var english = tookExpression.First();
                    var georgian = tookExpression.Skip(1).First();
                    var expression = new Expression(english, georgian, line);
                    return expression;
                });
                return new ResultLine(result, line, Path);
            })
            .ToList()
            .AsReadOnly();
        }
    }
}
