using EnglishSelfTraining.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EnglishSelfTraining.Objects
{
    public class EnglishFolder
    {
        public EnglishFolder(string path, IReadOnlyCollection<EnglishFile> englishFiles)
        {
            Path = path;
            EnglishFiles = englishFiles;
            var latPartOfPath = Path.Split('\\').Last();
            var monthName = new string(latPartOfPath.Where(x => char.IsLetter(x)).ToArray());
            var year = Convert.ToInt32(new string(latPartOfPath.Where(x => char.IsDigit(x)).ToArray()));
            var month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
            Month = new Month(monthName, year, month);
        }
        public string Path { get; }
        public Month Month { get; }
        public IReadOnlyCollection<EnglishFile> EnglishFiles { get; }
    }
}
