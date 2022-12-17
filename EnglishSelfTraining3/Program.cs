using EnglishSelfTraining;
using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.ObjectBehaviors;
using EnglishSelfTraining.Objects;
using EnglishSelfTraining.StaticHelpers;
using EnglishSelfTraining.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EnglishSelfTraining3
{
    class Program
    {


        const string DataPath = nameof(DataPath);
        const string TrainingOthers = nameof(TrainingOthers);
        const string OrderByLevel = nameof(OrderByLevel);

        static void ConsoleInitialize()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }


        static void Main(string[] args)
        {
            ConsoleInitialize();

            var now = DateTime.Now;

            var path = System.Configuration.ConfigurationManager.AppSettings["DataPath"];

            var pathOfFolders = Directory.GetDirectories(path)
                .Where(x => x.Contains("2022") && !x.Contains("January"));

            var englishFolders = EnglishFoldersHelper.GetEnglishFolders<EnglishFile>(pathOfFolders);

            if (TimeFactory.IsFirstDayOfMonth())
                IdentifyMistakes(englishFolders);

            var englishFolderBehaviors = EnglishFoldersBehaviours.Create(englishFolders);
            var currentDayFiles = englishFolderBehaviors.GetEnglishFiles(new Day(now.Day));

            var englishFilesBehaviours = EnglishFilesBehaviours.Create(currentDayFiles);
            var translateAttempts = englishFilesBehaviours.GetTrasnlateAttempts();

            Console.WriteLine(translateAttempts.Count);
            var skipValue = (DateTime.Now.Hour - 11) * 20;
            Console.WriteLine(skipValue);

            var trainingOthers = Convert.ToBoolean(false);
            if (trainingOthers)
                CreateFolders();

            Presentation(translateAttempts.Skip(skipValue).Take(20).ToList(), 0);
            if (trainingOthers)
                GenerateDefinedFiles();
        }



        static void Presentation(List<TrasnlateAttempt> trasnlateAttempts, int iteration)
        {
            if (trasnlateAttempts.IsEmpty())
                return;
            foreach (var translateAttempt in trasnlateAttempts)
            {

                Console.WriteLine(translateAttempt.Source);
                var input = Console.ReadLine();
                translateAttempt.Attempt = input;
                var isCorrect = translateAttempt.IsCorrect;
                if (isCorrect)
                {
                    Console.WriteLine(isCorrect);
                }
                else
                    Console.WriteLine($"{translateAttempt.Target} {translateAttempt.FilePath}");

                if (false)
                    if (iteration == 0)
                    {
                        //var level = GetDefinedLevel();
                        //var paths = TargetFiles.CacheLevelFiles.Select(x => x.Path);
                        //if (level != SentenceLevel.Ignore)
                        //{
                        //    var desiredPath = paths.Single(x => x.Contains(level.ToString()));
                        //    var lines = System.IO.File.ReadLines(desiredPath).ToList().AsReadOnly();
                        //    if (lines.IsEmpty() || lines.All(x => x != translateAttempt.Expression.OriginalValue))
                        //    {
                        //        TargetFiles.DefinedLines.Add(new DefinedLine(translateAttempt.Expression.OriginalValue, level));
                        //    }
                        //}
                    }
            }



            if (trasnlateAttempts.Count != 1)
                Console.Clear();

            iteration = iteration + 1;

            Presentation(trasnlateAttempts.Where(x => x.IsCorrect.Opposite()).ToList(), iteration);
        }
        static void GenerateDefinedFiles()
        {
            var paths = TargetFiles.CacheLevelFiles.Select(x => x.Path).ToList();
            TargetFiles.CacheLevelFiles.Clear();


            //foreach (var definedLine in TargetFiles.DefinedLines.DistinctBy(x => (x.OriginalValue, x.Level)).GroupBy(x => x.Level))
            //{
            //    var path = paths.Single(x => x.Contains(definedLine.Key.ToString()));
            //    using StreamWriter file = new StreamWriter(path, true);
            //    foreach (var item in definedLine)
            //    {

            //        file.WriteLine(item.OriginalValue);
            //    }

            //}
        }
        SentenceLevel GetDefinedLevel()
        {
            try
            {
                var sentenceLevels = string.Join(',', (Enum.GetValues(typeof(SentenceLevel)) as SentenceLevel[]).Select(x => $"{x}-{(int)x}"));
                Console.WriteLine(sentenceLevels);
                var inputLevel = Console.ReadLine();
                var level = (SentenceLevel)Convert.ToInt16(inputLevel);
                return level;
            }
            catch (Exception ex)
            {
                return SentenceLevel.Ignore;
            }
        }
        
        static void CreateFolders()
        {
            
            //var now = DateTime.Now;
            //var path = ConfigurationManager.AppSettings[OrderByLevel];
            //var pathOfFolders = Directory.GetDirectories(path);
            //var englishFolders = EnglishFoldersHelper.GetEnglishFolders<DefinedEnglishFile>(pathOfFolders);
            //var specificFolder = englishFolders.SingleOrDefault(x => x.Month.Value == now.Month && x.Month.Year == now.Year);
            //var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(now.Month);
            //var pathLastPart = $"{monthName}{now.Year}";
            //var fullPath = Path.Combine(path, pathLastPart);
            //if (specificFolder == null)
            //{
            //    Directory.CreateDirectory(fullPath);
            //    CreateFiles(fullPath, monthName);

            //}
            //else
            //{
            //    var specificFile = specificFolder.EnglishFiles.Where(x => x.Day.Value == now.Day);
            //    if (specificFile.IsEmpty())
            //    {
            //        CreateFiles(fullPath, monthName);
            //    }
            //}
            //TargetFiles.GenerateFiles();
        }
        void CreateFiles(string path, string monthName)
        {
            var sentenceLevels = (Enum.GetValues(typeof(SentenceLevel)) as SentenceLevel[]).Where(x => x != SentenceLevel.Ignore).ToList();
            var a = sentenceLevels.GetType();
            foreach (var item in sentenceLevels)
            {
                var finallastPathForFile = $"{DateTime.Now.Day}{monthName} {item}.txt";
                var filePath = Path.Combine(path, finallastPathForFile);
                var fileStream = File.Create(filePath);
                fileStream.Dispose();
            }
        }
        static void IdentifyMistakes(IEnumerable<EnglishFolder> englishFolders)
        {
            var results = englishFolders.Select(x => x.EnglishFiles.Select(x =>
            {
                var results = x.GetResults().ToList();
                return new
                {
                    englishFolders = results,
                    Failed = results.Where(x => x.ResultOfExpression.IsFailure),
                    FilePath = x
                };
            })).SelectMany(x => x).ToList();

            var mistakes = results.Where(x => x.Failed.Any()).Select(x => new
            {
                Path = x.FilePath,
                errors = string.Join(";", x.Failed.Select(x => x.Value))
            }).ToList();

            if (mistakes.Any())
                throw new InvalidOperationException("Gaaswore shecdomebi");
        }
    }
}
