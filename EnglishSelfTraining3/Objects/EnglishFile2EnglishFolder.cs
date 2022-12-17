namespace EnglishSelfTraining.Objects
{
    public class EnglishFile2EnglishFolder
    {
        public EnglishFile EnglishFile { get; }
        public EnglishFolder EnglishFolder { get; }
        public EnglishFile2EnglishFolder(EnglishFile englishFile, EnglishFolder englishFolder)
        {
            EnglishFolder = englishFolder;
            EnglishFile = englishFile;
        }
    }
}
