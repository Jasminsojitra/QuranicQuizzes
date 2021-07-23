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
    public class LearnTabFlipPageQuranViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IClientAPI _clientAPI;
        public Command<object> StudyCommmand { get; set; }
        ISimpleAudioPlayer player;

        public LearnTabFlipPageQuranViewModel(INavigationService navigationService, IClientAPI clientAPI) : base(navigationService)
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

        public string _Transliteration;
        public string Transliteration
        {
            get
            {
                return _Transliteration;
            }

            set
            {
                _Transliteration = value;
                RaisePropertyChanged(nameof(Transliteration));
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
                bool isSave = await _clientAPI.SaveFlashCardProgress(SelectedSurah.ID, Quizzesdata.CategoryID, QuestionId);
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
                bool isReset = await _clientAPI.ResetFlashCardProgress(SelectedSurah.ID, Quizzesdata.CategoryID);
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
                UserDialogs.Instance.HideLoading();
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
            parameters.Add("Categories", Category);
            await _navigationService.GoBackAsync();
            //if (GlobalConst.isLearnTabStudy)
            //    await _navigationService.NavigateAsync("../../../../MainLearnPage", parameters);
            //else
            //    await _navigationService.NavigateAsync("../../../MainLearnPage", parameters);
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
                 QuizzesTypesQuestions = await _clientAPI.GetQuranFlashCards(SelectedSurah.ID, Quizzesdata.CategoryID, qShuffle, removeDuplicates, IsSwitch, StartVerb, EndVerb, Language);
                CategoryImageURL = (QuizzesTypesQuestions != null) ? GlobalConst.ApiUrl + QuizzesTypesQuestions.CategoryImage : "";
                if (QuizzesTypesQuestions != null && QuizzesTypesQuestions.Questions.Count > 0)
                {
                    //QuizzesTypesQuestions = datas;

                    int QuestionCount = QuizzesTypesQuestions.Questions.Count;
                    TotalQuestionCount = (CurrentQuestion + 1) + "/" + QuestionCount;

                    //QuizzesTypesQuestions.Questions[CurrentQuestion].Answers = shuffle(QuizzesTypesQuestions.Questions[CurrentQuestion].Answers);
                    data = QuizzesTypesQuestions.Questions[CurrentQuestion];
                    QuestionId = data.ID;
                    QuestionText = "What is the meaning of";
                    ImageText = data.ArabicText;
                    Transliteration = data.Transliteration;

                     //answer = QuizzesTypesQuestions.Questions[CurrentQuestion].Answers.Where(x => x.IsAnswer == true).FirstOrDefault();
                     AnswerText = data.MeaningText;

                    if (!string.IsNullOrEmpty(data.SoundUrl))
                    {
                        SoundURL = GlobalConst.ApiUrl + data.SoundUrl;
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
                else
                {
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
                    data = new LearnQuranQuestion();
                    answer = new FlashCardAnswer();
                    //QuizzesTypesQuestions.Questions[CurrentQuestion].Answers = shuffle(QuizzesTypesQuestions.Questions[CurrentQuestion].Answers);
                    data = QuizzesTypesQuestions.Questions[CurrentQuestion];
                    QuestionId = data.ID;
                    data = QuizzesTypesQuestions.Questions[CurrentQuestion];
                    QuestionId = data.ID;
                    QuestionText = "What is the meaning of";
                    ImageText = data.ArabicText;
                    Transliteration = data.Transliteration;

                    //answer = QuizzesTypesQuestions.Questions[CurrentQuestion].Answers.Where(x => x.IsAnswer == true).FirstOrDefault();
                    AnswerText = data.MeaningText;

                    if (!string.IsNullOrEmpty(data.SoundUrl))
                    {
                        SoundURL = GlobalConst.ApiUrl + data.SoundUrl;
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
                else
                {
                    IsQuestionVisible = false;
                    IsLastPageVisible = true;
                    IsQuestionNoAvailable = false;
                }
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
        public DelegateCommand SoundTap => new DelegateCommand(async () =>
        {
            // await CrossMediaManager.Current.Play(SoundURL);
            try
            {
                player.Play();
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
        public bool _IsSwitch;
        public bool IsSwitch
        {
            get
            {
                return _IsSwitch;
            }
            set
            {
                _IsSwitch = value;
                RaisePropertyChanged(nameof(IsSwitch));
            }
        }

        public bool _IsUrdu;
        public bool IsUrdu
        {
            get
            {
                return _IsUrdu;
            }
            set
            {
                _IsUrdu = value;
                RaisePropertyChanged(nameof(IsUrdu));
            }
        }

        public bool _IsEnglish;
        public bool IsEnglish
        {
            get
            {
                return _IsEnglish;
            }
            set
            {
                _IsEnglish = value;
                RaisePropertyChanged(nameof(IsEnglish));
            }
        }

        public bool _IsnotSwitch;
        public bool IsnotSwitch
        {
            get
            {
                return _IsnotSwitch;
            }
            set
            {
                _IsnotSwitch = value;
                RaisePropertyChanged(nameof(IsnotSwitch));
            }
        }
        LearnQuranRespo QuizzesTypesQuestions = new LearnQuranRespo();
        LearnQuranQuestion data = new LearnQuranQuestion();
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
        string Language="";
        int StartVerb = 1;
        int EndVerb = 1;
        Surah SelectedSurah;
        Categories Category = new Categories();
        public override async void Initialize(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                Quizzesdata = parameters["PlayTab"] as Quizze;
                SessionInfo = parameters["SessionInfo"] as CheckSession;
                parameters.Add("Categories", Category);
                Language = parameters["Language"] as string;
                IsSwitch = ((parameters["Switch"] as string).ToLower() == "true") ? true : false;
                IsnotSwitch = !IsSwitch;
                qShuffle = ((parameters["Shuffle"] as string).ToLower() == "true") ? true : false;
                removeDuplicates = ((parameters["RemoveDuplicates"] as string).ToLower() == "true") ? true : false;
                Coureses = parameters["Coureses"] as Coures;
                assignmentData = parameters["assignmentData"] as Assignment;
                LvSelectedSubCategoryName = parameters["SubCategories"] as SubCategories;
                StartVerb=int.Parse(parameters["StartVerbs"] as string);
                EndVerb = int.Parse(parameters["EndVerbs"] as string);
                SelectedSurah = parameters["SelectedSurah"] as Surah;
                TitleName = SelectedSurah.FinalName;
                if (Language.Equals("Urdu"))
                {
                    IsEnglish = false;
                    IsUrdu = true;
                }
                else {
                    IsEnglish = true;
                    IsUrdu = false;
                }
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
