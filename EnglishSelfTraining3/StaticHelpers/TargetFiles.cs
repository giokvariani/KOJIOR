using EnglishSelfTraining.Objects;
using System.Collections.Generic;

namespace EnglishSelfTraining.StaticHelpers
{


    public static class TargetFiles
    {
        const string DataPath = nameof(DataPath);
        const string TrainingOthers = nameof(TrainingOthers);
        const string OrderByLevel = nameof(OrderByLevel);

        public static List<EnglishFile> CacheLevelFiles;
        public static List<DefinedLine> DefinedLines = new List<DefinedLine>();
        public static void GenerateFiles()
        {
            //var now = DateTime.Now;
            //var path = ConfigurationManager.AppSettings[OrderByLevel];
            //var pathOfFolders = Directory.GetDirectories(path);
            //var englishFolders = EnglishFoldersHelper.GetEnglishFolders<DefinedEnglishFile>(pathOfFolders);
            //var specificFolder = englishFolders.SingleOrDefault(x => x.Month.Value == now.Month && x.Month.Year == now.Year);
            //CacheLevelFiles = specificFolder.EnglishFiles.Where(x => x.Day.Value == now.Day).ToList();
        }
    }
}
