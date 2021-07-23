using System;
using System.Collections.Generic;

namespace QuranicQuizzes.Models
{
    public class Surah
    {
        public int ID { get; set; }
        public int AyahCount { get; set; }
        public int StartAyah { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public int RukuCount { get; set; }
        public string FinalName
        {
            get
            {
                return ID+":"+ " Surah " + EnglishName;
            }
        }
    }

    public class WordCount
    {
        public int SurahID { get; set; }
        public int VerseID { get; set; }
        public int Count { get; set; }
    }

    public class Chapters
    {
        public List<Surah> Surahs { get; set; }
        public List<WordCount> WordCount { get; set; }
        public int WordLimit { get; set; }
        public List<string> Languages { get; set; }
    }
}
