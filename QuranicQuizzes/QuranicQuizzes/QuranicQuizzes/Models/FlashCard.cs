using System;
using System.Collections.Generic;
using QuranicQuizzes.Helpers;

namespace QuranicQuizzes.Models
{
    public class FlashCardAnswer
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswer { get; set; }
        public bool IsArabic { get; set; }
        public string ImageURL { get; set; }
        public string FinalImageURL
        {
            get
            {
                return (!string.IsNullOrEmpty(ImageURL)) ? GlobalConst.ApiUrl + ImageURL : string.Empty;
            }
        }
        public bool IsImage
        {
            get
            {
                return  string.IsNullOrEmpty(ImageURL) ? false : true;
            }
        }
    }

    public class FlashCardQuestion
    {
        public List<FlashCardAnswer> Answers { get; set; }
        public int ID { get; set; }
        public int QuizID { get; set; }
        public string QuestionText { get; set; }
        public string ImageText { get; set; }
        public string ImageURL { get; set; }
        public bool IsMultipleChoice { get; set; }
        public string Notes { get; set; }
        public int QuestionOrder { get; set; }
        public string SoundURL { get; set; }
        public string FinalImageURL
        {
            get
            {
                return (!string.IsNullOrEmpty(ImageURL)) ? GlobalConst.ApiUrl + ImageURL : string.Empty;
            }
        }
        public bool IsImage
        {
            get
            {
                return string.IsNullOrEmpty(ImageText) ? (string.IsNullOrEmpty(ImageURL) ? false : true) : false;
            }
        }
        public bool IsImageText
        {
            get
            {
                return string.IsNullOrEmpty(ImageText) ? false : true;
            }
        }
    }

    public class FlashCard
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public string CategoryImage { get; set; }
        public List<FlashCardQuestion> Questions { get; set; }
    }
}
