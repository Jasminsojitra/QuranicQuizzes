using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Plugin.SimpleAudioPlayer;
using Prism.Commands;
using Prism.Navigation;
using QuranicQuizzes.Helpers;
using QuranicQuizzes.Interfaces;
using QuranicQuizzes.Models;
using Xamarin.Forms;

namespace QuranicQuizzes.ViewModels
{
    public class LearnTabFlipPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IClientAPI _clientAPI;
        public Command<object> StudyCommmand { get; set; }
        ISimpleAudioPlayer player;

        public LearnTabFlipPageViewModel(INavigationService navigationService, IClientAPI clientAPI) : base(navigationService)
        {
            _navigationService = navigationService;
            _clientAPI = clientAPI;
            //StudyCommmand = new Command<object>(StudyCommmandClick, (x) => CanNavigate);
        }
        
        public int _LearnCount;
        public int LearnCount
        {
            get
            {
                return _LearnCount;
            }
            set
            {
                _LearnCount = value;
                RaisePropertyChanged(nameof(LearnCount));
            }
        }
        public int _QuestionId;
        public int QuestionId
        {
            get
            {
                return _QuestionId;
            }
            set
            {
                _QuestionId = value;
                RaisePropertyChanged(nameof(QuestionId));
            }
        }

        public int _StudyCount;
        public int StudyCount
        {
            get
            {
                return _StudyCount;
            }
            set
            {
                _StudyCount = value;
                RaisePropertyChanged(nameof(StudyCount));
            }
        }

        public int _currentQuestion;
        public int CurrentQuestion
        {
            get
            {
                return _currentQuestion;
            }
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged(nameof(CurrentQuestion));
            }
        }
        public string _TotalQuestionCount;
        public string TotalQuestionCount
        {
            get
            {
                return _TotalQuestionCount;
            }

            set
            {
                _TotalQuestionCount = value;
                RaisePropertyChanged(nameof(TotalQuestionCount));
            }
        }

        public string _QuestionText;
        public string QuestionText
        {
            get
            {
                return _QuestionText;
            }

            set
            {
                _QuestionText = value;
                RaisePropertyChanged(nameof(QuestionText));
            }
        }

        public bool _IsQuestionNoAvailable;
        public bool IsQuestionNoAvailable
        {
            get
            {
                return _IsQuestionNoAvailable;
            }

            set
            {
                _IsQuestionNoAvailable = value;
                RaisePropertyChanged(nameof(IsQuestionNoAvailable));
            }
        }

        public bool _IsLastPageVisible;
        public bool IsLastPageVisible
        {
            get
            {
                return _IsLastPageVisible;
            }

            set
            {
                _IsLastPageVisible = value;
                RaisePropertyChanged(nameof(IsLastPageVisible));
            }
        }
        
        public bool _IsQuestionVisible;
        public bool IsQuestionVisible
        {
            get
            {
                return _IsQuestionVisible;
            }

            set
            {
                _IsQuestionVisible = value;
                RaisePropertyChanged(nameof(IsQuestionVisible));
            }
        }

        public string _AnswerText;
        public string AnswerText
        {
            get
            {
                return _AnswerText;
            }

            set
            {
                _AnswerText = value;
                RaisePropertyChanged(nameof(AnswerText));
            }
        }

        public bool _IsAnswerImageText;
        public bool IsAnswerImageText
        {
            get
            {
                return _IsAnswerImageText;
            }

            set
            {
                _IsAnswerImageText = value;
                RaisePropertyChanged(nameof(IsAnswerImageText));
            }
        }

        public string _AnswerImageURL;
        public string AnswerImageURL
        {
            get
            {
                return _AnswerImageURL;
            }

            set
            {
                _AnswerImageURL = value;
                RaisePropertyChanged(nameof(AnswerImageURL));
            }
        }

        public bool _IsAnswerImageURL;
        public bool IsAnswerImageURL
        {
            get
            {
                return _IsAnswerImageURL;
            }

            set
            {
                _IsAnswerImageURL = value;
                RaisePropertyChanged(nameof(IsAnswerImageURL));
            }
        }
        public string _AnswerImageText;
        public string AnswerImageText
        {
            get
            {
                return _AnswerImageText;
            }

            set
            {
                _AnswerImageText = value;
                RaisePropertyChanged(nameof(AnswerImageText));
            }
        }

        public string _Notes;
        public string Notes
        {
            get
            {
                return _Notes;
            }

            set
            {
                _Notes = value;
                RaisePropertyChanged(nameof(Notes));
            }
        }


        public bool _IsNotes;
        public bool IsNotes
        {
            get
            {
                return _IsNotes;
            }

            set
            {
                _IsNotes = value;
                RaisePropertyChanged(nameof(IsNotes));
            }
        }



        public string _ResultBackground;
        public string ResultBackground
        {
            get
            {
                return _ResultBackground;
            }
            set
            {
                _ResultBackground = value;
                RaisePropertyChanged(nameof(ResultBackground));
            }
        }

        public string _SoundURL;
        public string SoundURL
        {
            get
            {
                return _SoundURL;
            }
            set
            {
                _SoundURL = value;
                RaisePropertyChanged(nameof(SoundURL));
            }
        }

        public bool _IsSoundURL;
        public bool IsSoundURL
        {
            get
            {
                return _IsSoundURL;
            }
            set
            {
                _IsSoundURL = value;
                RaisePropertyChanged(nameof(IsSoundURL));
            }
        }

        public bool _IsArabic;
        public bool IsArabic
        {
            get
            {
                return _IsArabic;
            }
            set
            {
                _IsArabic = value;
                RaisePropertyChanged(nameof(IsArabic));
            }
        }

        public bool _IsNotArabic;
        public bool IsNotArabic
        {
            get
            {
                return _IsNotArabic;
            }
            set
            {
                _IsNotArabic = value;
                RaisePropertyChanged(nameof(IsNotArabic));
            }
        }

        public bool _IsQuestionText;
        public bool IsQuestionText
        {
            get
            {
                return _IsQuestionText;
            }

            set
            {
                _IsQuestionText = value;
                RaisePropertyChanged(nameof(IsQuestionText));
            }
        }

        public string _ImageText;
        public string ImageText
        {
            get
            {
                return _ImageText;
            }

            set
            {
                _ImageText = value;
                RaisePropertyChanged(nameof(ImageText));
            }
        }

        public bool _IsImageText;
        public bool IsImageText
        {
            get
            {
                return _IsImageText;
            }

            set
            {
                _IsImageText = value;
                RaisePropertyChanged(nameof(IsImageText));
            }
        }
        public bool _isVisibleLayout;
        public bool isVisibleLayout
        {
            get
            {
                return _isVisibleLayout;
            }

            set
            {
                _isVisibleLayout = value;
                RaisePropertyChanged(nameof(isVisibleLayout));
            }
        }
        
        public string _ImageURL;
        public string ImageURL
        {
            get
            {
                return _ImageURL;
            }

            set
            {
                _ImageURL = value;
                RaisePropertyChanged(nameof(ImageURL));
            }
        }

        public bool _IsImageURL;
        public bool IsImageURL
        {
            get
            {
                return _IsImageURL;
            }

            set
            {
                _IsImageURL = value;
                RaisePropertyChanged(nameof(IsImageURL));
            }
        }

        public async Task gotIt()
        {
            try
            {
                LearnCount++;
                ///UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                bool isSave = await _clientAPI.SaveFlashCardProgress(Quizzesdata.Id, Quizzesdata.CategoryID,QuestionId);
                studyAgain();
                //UserDialogs.Instance.HideLoading();

            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
                UserDialogs.Instance.HideLoading();

            }
        }

        public async Task ResetTap()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                bool isReset = await _clientAPI.ResetFlashCardProgress(Quizzesdata.Id, Quizzesdata.CategoryID);
                if (isReset)
                    await SetQuestionData();
                IsQuestionVisible = true;
                IsLastPageVisible = false;
                IsQuestionNoAvailable = false;
                CurrentQuestion = 0;
                LearnCount = 0;
                StudyCount = 0;
                UserDialogs.Instance.HideLoading();

            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
                UserDialogs.Instance.HideLoading();

            }
        }


        public DelegateCommand CloseTap => new DelegateCommand(async () =>
        {
            var parameters = new NavigationParameters();
            parameters.Add("PlayTab", Quizzesdata);
            parameters.Add("Coureses", Coureses);
            if(GlobalConst.isLearnTabStudy)
                await _navigationService.NavigateAsync("../../../../MainLearnPage", parameters);
            else
                await _navigationService.NavigateAsync("../../../MainLearnPage", parameters);
        });

        //Set new Question with answer
        public async Task SetQuestionData()
        {
            try
            {
                isVisibleLayout = false;

                IsQuestionVisible = true;
                IsLastPageVisible = false;
                IsQuestionNoAvailable = false;
                CurrentQuestion = 0;
                LearnCount = 0;
                StudyCount = 0;
                var datas = await _clientAPI.GetFlashCards(Quizzesdata.Id, Quizzesdata.CategoryID, qShuffle, removeDuplicates, 0, 0, "");
                CategoryImageURL = (datas!=null)?GlobalConst.ApiUrl + datas.CategoryImage:"";
                if (datas != null && datas.Questions.Count > 0)
                {
                    QuizzesTypesQuestions = datas;
                    
                    int QuestionCount = QuizzesTypesQuestions.Questions.Count;
                    TotalQuestionCount = (CurrentQuestion + 1) + "/" + QuestionCount;

                    //QuizzesTypesQuestions.Questions[CurrentQuestion].Answers = shuffle(QuizzesTypesQuestions.Questions[CurrentQuestion].Answers);
                    data = QuizzesTypesQuestions.Questions[CurrentQuestion];
                    QuestionId = data.ID;
                    QuestionText = data.QuestionText;
                    ImageText = data.ImageText;
                    IsImageText = data.IsImageText;
                    IsImageURL = data.IsImage;
                    ImageURL = data.FinalImageURL;

                    answer = QuizzesTypesQuestions.Questions[CurrentQuestion].Answers.Where(x => x.IsAnswer == true).FirstOrDefault();
                    AnswerText = answer.AnswerText;
                    IsArabic=answer.IsArabic;
                    IsNotArabic= !answer.IsArabic;
                    AnswerImageURL = answer.FinalImageURL;
                    IsAnswerImageURL = answer.IsImage;

                    if (!string.IsNullOrEmpty(data.SoundURL))
                    {
                        SoundURL = GlobalConst.ApiUrl + data.SoundURL;
                        IsSoundURL = true;
                        //CrossMediaManager.Current.Play(SoundURL);

                        player = CrossSimpleAudioPlayer.Current;
                        //string url = "some-url-string";
                        WebClient wc = new WebClient();
                        Stream fileStream = wc.OpenRead(SoundURL);
                        player.Load(fileStream);
                        //player.Play();

                    }
                }
                else {
                    IsQuestionVisible = false;
                    IsLastPageVisible = false;
                    IsQuestionNoAvailable = true;
                }
                isVisibleLayout = true;
                UserDialogs.Instance.HideLoading();
                // IsQuestionAnswerInfoVisible = false;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
            }
        }


        public void studyAgain()
        {
            // await CrossMediaManager.Current.Play(SoundURL);
            try
            {
                CurrentQuestion++;             
                int QuestionCount = QuizzesTypesQuestions.Questions.Count;
                if (CurrentQuestion < QuestionCount)
                {
                    TotalQuestionCount = (CurrentQuestion + 1) + "/" + QuestionCount;
                    data = new FlashCardQuestion();
                    answer = new FlashCardAnswer();
                    //QuizzesTypesQuestions.Questions[CurrentQuestion].Answers = shuffle(QuizzesTypesQuestions.Questions[CurrentQuestion].Answers);
                    data = QuizzesTypesQuestions.Questions[CurrentQuestion];
                    QuestionId = data.ID;
                    QuestionText = data.QuestionText;
                    ImageText = data.ImageText;
                    IsImageText = data.IsImageText;
                    IsImageURL = data.IsImage;
                    ImageURL = data.FinalImageURL;

                    answer = QuizzesTypesQuestions.Questions[CurrentQuestion].Answers.Where(x => x.IsAnswer == true).FirstOrDefault();
                    AnswerText = answer.AnswerText;
                    AnswerImageURL = answer.FinalImageURL;
                    IsAnswerImageURL = answer.IsImage;
                    IsArabic = answer.IsArabic;
                    IsNotArabic = !answer.IsArabic;

                    if (!string.IsNullOrEmpty(data.SoundURL))
                    {
                        SoundURL = GlobalConst.ApiUrl + data.SoundURL;
                        IsSoundURL = true;
                        //CrossMediaManager.Current.Play(SoundURL);

                        player = CrossSimpleAudioPlayer.Current;
                        //string url = "some-url-string";
                        WebClient wc = new WebClient();
                        Stream fileStream = wc.OpenRead(SoundURL);
                        player.Load(fileStream);
                        //player.Play();

                    }
                }
                else {
                    IsQuestionVisible = false;
                    IsLastPageVisible = true;
                    IsQuestionNoAvailable = false;
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
            }

        }
        public DelegateCommand SoundTap => new DelegateCommand(async () =>
        {
            // await CrossMediaManager.Current.Play(SoundURL);
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
            }

        });

        public string _CategoryImageURL;
        public string CategoryImageURL
        {
            get
            {
                return _CategoryImageURL;
            }

            set
            {
                _CategoryImageURL = value;
                RaisePropertyChanged(nameof(CategoryImageURL));
            }
        }

        public string _TitleName;
        public string TitleName
        {
            get
            {
                return _TitleName;
            }

            set
            {
                _TitleName = value;
                RaisePropertyChanged(nameof(TitleName));
            }
        }
        FlashCard QuizzesTypesQuestions = new FlashCard();
        FlashCardQuestion data = new FlashCardQuestion();
        FlashCardAnswer answer = new FlashCardAnswer();
        Quizze Quizzesdata = new Quizze();
        CheckSession SessionInfo = new CheckSession();
        Coures Coureses = new Coures();
        string SourceURL;
        string SourceURLHeaderName;
        Assignment assignmentData = new Assignment();
        int CourseId = 0;
        int QuizID = 0;
        int HWID = 0;
        int CategoryID = 0;
        SubCategories LvSelectedSubCategoryName = new SubCategories();
        bool qShuffle;
        bool removeDuplicates;
        public override async void Initialize(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                Quizzesdata = parameters["PlayTab"] as Quizze;
                SessionInfo = parameters["SessionInfo"] as CheckSession;
                qShuffle = ((parameters["Shuffle"] as string).ToLower() == "true")?true:false;
                removeDuplicates = ((parameters["RemoveDuplicates"] as string).ToLower() == "true") ? true : false;

                assignmentData = parameters["assignmentData"] as Assignment;
                LvSelectedSubCategoryName = parameters["SubCategories"] as SubCategories;
                TitleName = Quizzesdata.Name;
                await SetQuestionData(); 
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                var properties = new Dictionary<string, string>
                {
                    { "Messge", ex.Message },
                    { "StackTrace", ex.StackTrace }
                };

                Crashes.TrackError(ex, properties);
            }
        }


    }
}
