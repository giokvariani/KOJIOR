using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.Objects;
using EnglishSelfTraining.StaticHelpers;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EnglishSelfTraining
{
    public class TrasnlateAttempt
    {
        public TrasnlateAttempt(string source, string target, string attempt, bool isCorrect, string filePath, Expression expresion)
        {
            Source = source;
            Target = target;
            Attempt = attempt;
            FilePath = filePath;
            Expression = expresion;
        }
        public string FilePath { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string Attempt { get; set; }
        public Expression Expression { get; set; }
        private void SecretCodes()
        {
            if (Attempt.Contains("Code:OpenCurrentFile"))
            {
                FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Write, FileShare.None);

            }
            if (Attempt.Contains("Code:ForceClean"))
            {
                var a = new FileStream(FilePath, FileMode.Open);
                a.Close();
            }
        }
       
        public virtual bool IsCorrect
        {
            get
            {
                

                try
                {
                    //SecretCodes();

                    var exceptionLines = System.IO.File.ReadAllLines(@"..\..\..\ReplaceableWords.txt");
                    var a = exceptionLines.Select(x => $"{x}/{x.ToLower()}");
                    if (Attempt.Contains("'"))
                    {

                        if (Target.Contains("’"))
                        {
                            Attempt = Attempt.Replace("'", "’");
                            //var replacedBy = Target.Split(' ').First(x => x.Contains("’"));
                            //Attempt = Attempt.Replace(apostropWord, replacedBy);
                            return Attempt == Target;
                        }
                        else
                        {
                            var attemptWords = Attempt.Split(' ');
                            var apostropWord = attemptWords.First(x => x.Contains("'"));
                            var replaceableWords = exceptionLines.Single(x => x.Contains(apostropWord) || x.ToLower().Contains(apostropWord));
                            var potentialReplacedBy = replaceableWords.Split('/').Single(x => x.Contains(' '));
                            var index = attemptWords.ToList().FindIndex(x => x == apostropWord);
                            Attempt = Attempt.Replace(apostropWord, index == 0 ? potentialReplacedBy : potentialReplacedBy.ToLower());
                            return Attempt == Target;
                        }
                    }

                    return Attempt == Target;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
    }
}
