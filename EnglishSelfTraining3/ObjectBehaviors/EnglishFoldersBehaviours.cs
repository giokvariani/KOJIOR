using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.Objects;
using EnglishSelfTraining.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSelfTraining.ObjectBehaviors
{
    public sealed class EnglishFoldersBehaviours
    {
        public static EnglishFoldersBehaviours Create(IReadOnlyCollection<EnglishFolder> englishFolders) => new EnglishFoldersBehaviours(englishFolders);
        private EnglishFoldersBehaviours(IReadOnlyCollection<EnglishFolder> englishFolders) => EnglishFolders = englishFolders;
        private IReadOnlyCollection<EnglishFolder> EnglishFolders { get; }
        public EnglishFolder GetEnglishFolder(Month month) => EnglishFolders.SingleOrDefault(x => x.Month == month);
        public IReadOnlyCollection<EnglishFile> GetEnglishFiles(Day day) => EnglishFolders
            .Where(x => x.Month.Year > DateTime.Now.AddYears(-2).Year)
            .SelectMany(x => x.EnglishFiles)
            .Where(x => x.Day == day)
            .AsReadOnlyList();
    }
}
