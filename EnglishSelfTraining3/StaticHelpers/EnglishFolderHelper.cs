using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnglishSelfTraining.StaticHelpers
{
    public static class EnglishFoldersHelper
    {
        public static IReadOnlyCollection<EnglishFolder> GetEnglishFolders<T>(IEnumerable<string> pathOfFolders) where T : EnglishFile => 
            pathOfFolders
                .Select(pathOfFolder => new EnglishFolder(pathOfFolder, Directory.GetFiles(pathOfFolder)
                        .Select(x => (T)Activator.CreateInstance(typeof(T), new object[] { x })).ToList())).AsReadOnlyList();
    }
}
