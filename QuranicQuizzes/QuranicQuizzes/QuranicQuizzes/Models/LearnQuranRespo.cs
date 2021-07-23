using System;
using System.Collections.Generic;

namespace QuranicQuizzes.Models
{
    public class LearnQuranQuestion
    {
        public int ID { get; set; }
        public int SurahID { get; set; }
        public int VerseID { get; set; }
        public int Position { get; set; }
        public string ArabicText { get; set; }
        public string Transliteration { get; set; }
        public string MeaningText { get; set; }
        public string Language { get; set; }
        public string SoundUrl { get; set; }
    }

    public class LearnQuranRespo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public string CategoryImage { get; set; }
        public List<LearnQuranQuestion> Questions { get; set; }
        public string Language { get; set; }
    }


}
