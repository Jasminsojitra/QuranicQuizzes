using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Prism.Commands;
using Prism.Navigation;
using QuranicQuizzes.Helpers;
using QuranicQuizzes.Interfaces;
using QuranicQuizzes.Models;
using QuranicQuizzes.Views;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace QuranicQuizzes.ViewModels
{
    public class LearnTabQuranQuestionSelectionPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IClientAPI _clientAPI;
        public Command<object> StudyCommmand { get; set; }
        //public Command<object> GenrateCommmand { get; set; }
        public Command<object> SelectedQuizes { get; set; }
        public Command<object> InfoClickCommand { get; set; }

        public LearnTabQuranQuestionSelectionPageViewModel(INavigationService navigationService, IClientAPI clientAPI) : base(navigationService)
        {
            _navigationService = navigationService;
            _clientAPI = clientAPI;
            //StudyCommmand = new Command<object>(StudyCommmandClick, (x) => CanNavigate);
            //GenrateCommmand = new Command<object>(GenrateCommmandClick, (x) => CanNavigate);
            SelectedQuizes = new Command<object>(SelectedQuizesData);
            InfoClickCommand = new Command<object>(InfoClicksCommand);
        }

        public string _titleName;
        public string TitleName
        {
            get
            {
                return _titleName;
            }

            set
            {
                SetProperty(ref _titleName, value);
            }
        }

        //Info Click
        private async void InfoClicksCommand(object obj)
        {
            if (obj != null)
            {
                var data = obj as QuizzesType;

                var alertDialogConfiguration = new MaterialAlertDialogConfiguration
                {
                    MessageTextColor = Color.Black,
                    MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("QuranFont"),
                    TintColor = Color.Black
                };

                if (data.Id == 0)
                {
                    await MaterialDialog.Instance.AlertAsync(message: "This will change the order of questions in quiz", configuration: alertDialogConfiguration);
                }
                else if (data.Id == 1)
                {
                    await MaterialDialog.Instance.AlertAsync(message: "This removes any duplicate words in quiz.e.g.repeated words like “An - Nas” in surah An - Nas in Quran category", configuration: alertDialogConfiguration);
                }
                else if (data.Id == 2)
                {
                    await MaterialDialog.Instance.AlertAsync(message: "This Switch Language order", configuration: alertDialogConfiguration);
                }
            }
        }

        //Selected Quizes type
        private void SelectedQuizesData(object obj)
        {
            try
            {
                if (obj != null)
                {
                    var data = obj as QuizzesType;
                    if (data.Name.Equals("Switch Order"))
                    {
                            var d = LeftSideLanguage;
                            LeftSideLanguage = RightSideLanguage;
                            RightSideLanguage = d;
                        IsSwitch= (data.IconImage).Equals("close.png") ? true : false;
                    }
                    //if (!data.IsEnables)
                    //{
                    QuizzesType qt = new QuizzesType();
                    qt.Id = data.Id;
                    qt.Name = data.Name;
                    qt.IconImage = (data.IconImage).Equals("close.png") ? "done.png" : "close.png";
                    qt.BackgroundColor = (data.BackgroundColor).Equals("#868e96") ? "#28a745" : "#868e96";
                    qt.IsEnables = false;
                    qt.IsVisibles = data.IsVisibles;
                    var index = QuizzesTypes.IndexOf(data);
                    QuizzesTypes.RemoveAt(index);
                    QuizzesTypes.Insert(index, qt);
                    //}
                    
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

        private ObservableCollection<QuizzesType> _QuizzesTypes = new ObservableCollection<QuizzesType>();
        public ObservableCollection<QuizzesType> QuizzesTypes
        {
            get { return _QuizzesTypes; }
            set
            {
                _QuizzesTypes = value;
                RaisePropertyChanged("QuizzesTypes");
            }
        }

        //Play quize button
        public DelegateCommand GenrateCommmand => new DelegateCommand(async () =>
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (!errorLblVisible)
                    {
                        UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                        bool qShuffle = false;
                        bool removeDuplicates = false;
                        bool switchOrder = false;

                        foreach (var item in QuizzesTypes)
                        {
                            if (item.IconImage.Equals("done.png"))
                            {
                                if (item.Name.Equals("Shuffle Questions"))
                                    qShuffle = true;
                                if (item.Name.Equals("Remove Duplicates"))
                                    removeDuplicates = true;
                                if (item.Name.Equals("Switch Order"))
                                {
                                    switchOrder = true;
                                }

                            }
                        }

                        var parameters = new NavigationParameters();
                        //parameters.Add("SessionInfo", SessionInfo);
                        parameters.Add("PlayTab", Quizzesdata);
                        parameters.Add("Language", LvLanguageSelected);
                        parameters.Add("Switch", switchOrder.ToString());
                        parameters.Add("StartVerbs", SelectedStartVerse.ToString());
                        parameters.Add("EndVerbs", SelectedEndVerse.ToString());
                        parameters.Add("Shuffle", qShuffle.ToString());
                        parameters.Add("RemoveDuplicates", removeDuplicates.ToString());
                        parameters.Add("Categories", Category);
                        parameters.Add("Coureses", Coureses);
                        parameters.Add("SelectedSurah", SelectedItemLvSurah);
                        //await _navigationService.GoBackAsync();

                        await _navigationService.NavigateAsync(nameof(LearnTabFlipQuranPage), parameters);
                    }
                }
                else
                {
                    ShowMessage("Please check your internet connection");
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
        });


        bool _canNavigate = true;
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set
            {
                _canNavigate = value;
                RaisePropertyChanged(nameof(CanNavigate));
                // OnPropertyChanged();
            }
        }

        public string _QuizzesTypesHeight;
        public string QuizzesTypesHeight
        {
            get
            {
                return _QuizzesTypesHeight;
            }

            set
            {
                _QuizzesTypesHeight = value;
                RaisePropertyChanged(nameof(QuizzesTypesHeight));
            }
        }
        
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

        public string _LeftSideLanguage;
        public string LeftSideLanguage
        {
            get
            {
                return _LeftSideLanguage;
            }

            set
            {
                _LeftSideLanguage = value;
                RaisePropertyChanged(nameof(LeftSideLanguage));
            }
        }
        public bool _errorLblVisible;
        public bool errorLblVisible
        {
            get
            {
                return _errorLblVisible;
            }

            set
            {
                _errorLblVisible = value;
                RaisePropertyChanged(nameof(errorLblVisible));
            }
        }

        public bool _wordCountVisible;
        public bool wordCountVisible
        {
            get
            {
                return _wordCountVisible;
            }

            set
            {
                _wordCountVisible = value;
                RaisePropertyChanged(nameof(wordCountVisible));
            }
        }

        public string _RightSideLanguage;
        public string RightSideLanguage
        {
            get
            {
                return _RightSideLanguage;
            }

            set
            {
                _RightSideLanguage = value;
                RaisePropertyChanged(nameof(RightSideLanguage));
            }
        }

        public string _ErrorCountText;
        public string ErrorCountText
        {
            get
            {
                return _ErrorCountText;
            }

            set
            {
                _ErrorCountText = value;
                RaisePropertyChanged(nameof(ErrorCountText));
            }
        }

        private ObservableCollection<string> _LvLanguage { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> LvLanguage
        {
            get
            {
                return _LvLanguage;

            }
            set
            {
                _LvLanguage = value;
                RaisePropertyChanged(nameof(LvLanguage));
                //SelectedItem();
            }
        }

        public int _SelectedLanguageIndex { get; set; }
        public int SelectedLanguageIndex
        {
            get
            {
                return _SelectedLanguageIndex;
            }

            set
            {
                _SelectedLanguageIndex = value;
                RaisePropertyChanged(nameof(SelectedLanguageIndex));
            }
        }


        public int _totalWordCount { get; set; }
        public int totalWordCount
        {
            get
            {
                return _totalWordCount;
            }

            set
            {
                _totalWordCount = value;
                RaisePropertyChanged(nameof(totalWordCount));
            }
        }

        public int _SelectedStartVerse { get; set; }
        public int SelectedStartVerse
        {
            get
            {
                return _SelectedStartVerse;
            }

            set
            {
                if (value <= (SelectedEndVerse==0?1: SelectedEndVerse))
                {
                    
                        totalWordCount = 0;
                        _SelectedStartVerse = value;
                        wordCountVisible = true;
                        errorLblVisible = false;
                        List<int> total =(LvWordCount.Where(x => x.SurahID == SelectedSurahId && x.VerseID >= SelectedStartVerse && x.VerseID <= SelectedEndVerse).Select(x => x.Count).ToList());
                        for (int i = 0; i < total.Count; i++)
                        {
                            totalWordCount += total[i];
                        }
                        if (totalWordCount > 300)
                        {

                            wordCountVisible = false;
                            errorLblVisible = true;
                            ErrorCountText = "Please reduce range of Verses as Word count is above 300!";
                        }
                        else
                        {
                            wordCountVisible = true;
                            errorLblVisible = false;
                        }
                        RaisePropertyChanged(nameof(SelectedStartVerse));

                    
                }
                else {
                    wordCountVisible = false;
                    errorLblVisible = true;
                    ErrorCountText = "Start value is great than End value!";
                }
            }
        }
        
        public int _SelectedEndVerse { get; set; }
        public int SelectedEndVerse
        {
            get
            {
                return _SelectedEndVerse;
            }

            set
            {
                totalWordCount = 0;
                if (value >= (SelectedStartVerse == 0 ? 1 : SelectedStartVerse))
                {
                    if (_SelectedEndVerse != value)
                    {
                        totalWordCount = 0;
                        _SelectedEndVerse = value;
                        List<int> total = (LvWordCount.Where(x => x.SurahID == SelectedSurahId && x.VerseID >= SelectedStartVerse && x.VerseID <= SelectedEndVerse).Select(x => x.Count).ToList());

                        RaisePropertyChanged(nameof(SelectedEndVerse));
                        for (int i = 0; i < total.Count; i++)
                        {
                            totalWordCount += total[i];
                        }
                        if (totalWordCount > 300)
                        {

                            wordCountVisible = false;
                            errorLblVisible = true;
                            ErrorCountText = "Please reduce range of Verses as Word count is above 300!";
                        }
                        else
                        {
                            wordCountVisible = true;
                            errorLblVisible = false;
                        }
                    }
                }
                else
                {
                    wordCountVisible = false;
                    errorLblVisible = true;
                    ErrorCountText = "Start value is great than End value!";
                }
            }
        }

        public int _SelectedIndexLvSurah { get; set; }
        public int SelectedIndexLvSurah
        {
            get
            {
                return _SelectedIndexLvSurah;
            }

            set
            {
                _SelectedIndexLvSurah = value;
                RaisePropertyChanged(nameof(SelectedIndexLvSurah));
            }
        }
        
        //public int _SelectedEndVerseIndex { get; set; }
        //public int SelectedEndVerseIndex
        //{
        //    get
        //    {
        //        return _SelectedEndVerseIndex;
        //    }

        //    set
        //    {
        //        _SelectedEndVerseIndex = value;
        //        RaisePropertyChanged(nameof(SelectedEndVerseIndex));
        //    }
        //}

        //public int _SelectedStartVerseIndex { get; set; }
        //public int SelectedStartVerseIndex
        //{
        //    get
        //    {
        //        return _SelectedStartVerseIndex;
        //    }

        //    set
        //    {
        //        _SelectedStartVerseIndex = value;
        //        RaisePropertyChanged(nameof(SelectedStartVerseIndex));
        //    }
        //}

        public int _SelectedSurahId { get; set; }
        public int SelectedSurahId
        {
            get
            {
                return _SelectedSurahId;
            }

            set
            {
                _SelectedSurahId = value;
                RaisePropertyChanged(nameof(SelectedSurahId));
            }
        }

        public Surah _SelectedItemLvSurah { get; set; }
        public Surah SelectedItemLvSurah
        {
            get
            {
                return _SelectedItemLvSurah;
            }

            set
            {
                _SelectedItemLvSurah = value;
                RaisePropertyChanged(nameof(SelectedItemLvSurah));
                SelectedSurah();
            }
        }

        private void SelectedSurah()
        {
            SelectedSurahId = SelectedItemLvSurah.ID;
            var ayahCount = SelectedItemLvSurah.AyahCount;
            LvVerse.Clear();
            for (int i = 1; i <= ayahCount; i++)
            {
                LvVerse.Add(i);
            }
            totalWordCount = 0;
            SelectedStartVerse = 1;
            SelectedEndVerse = 1;
            if (isSelected)
            {
                List<int> total = (LvWordCount.Where(x => x.SurahID == SelectedSurahId && x.VerseID >= SelectedStartVerse && x.VerseID <= SelectedEndVerse).Select(x => x.Count).ToList());
                for (int i = 0; i < total.Count; i++)
                {
                    totalWordCount += total[i];
                }
            }
            isSelected = true;
            //totalWordCount = (LvWordCount.Where(x => x.SurahID == SelectedSurahId && x.VerseID >= SelectedStartVerse && x.VerseID <= SelectedEndVerse).Select(x=>x.Count).FirstOrDefault());

        }

        public string _LvLanguageSelected { get; set; }
        public string LvLanguageSelected
        {
            get
            {
                return _LvLanguageSelected;
            }

            set
            {
                _LvLanguageSelected = value;
                RaisePropertyChanged(nameof(LvLanguageSelected));
                LanguageLbl();
            }
        }

        private void LanguageLbl()
        {
            if(IsSwitch)
                LeftSideLanguage = LvLanguageSelected;
            else
                RightSideLanguage = LvLanguageSelected;

        }

        private ObservableCollection<int> _LvVerse { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<int> LvVerse
        {
            get
            {
                return _LvVerse;

            }
            set
            {
                _LvVerse = value;
                RaisePropertyChanged(nameof(LvVerse));
                //SelectedItem();
            }
        }

        private ObservableCollection<Surah> _LvSurah { get; set; } = new ObservableCollection<Surah>();
        public ObservableCollection<Surah> LvSurah
        {
            get
            {
                return _LvSurah;

            }
            set
            {
                _LvSurah = value;
                RaisePropertyChanged(nameof(LvSurah));
                //SelectedItem();
            }
        }

        private ObservableCollection<WordCount> _LvWordCount { get; set; } = new ObservableCollection<WordCount>();
        public ObservableCollection<WordCount> LvWordCount
        {
            get
            {
                return _LvWordCount;

            }
            set
            {
                _LvWordCount = value;
                RaisePropertyChanged(nameof(LvWordCount));
                //SelectedItem();
            }
        }

        bool isSelected = false;
        Quizze Quizzesdata = new Quizze();
        CheckSession SessionInfo = new CheckSession();
        Categories Category = new Categories();
        Coures Coureses = new Coures();
        bool IsSwitch = false;
        Chapters chapterList = new Chapters();

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            try
            {
                Quizzesdata = parameters["PlayTab"] as Quizze;
                TitleName = Quizzesdata.Name;
                CategoryImageURL = Quizzesdata.ImageURL;
                //CheckSession SessionInfo = await _clientAPI.GetCheckSession(Quizzesdata.Id, GlobalConst.isCourse, 0);
                Category = parameters["Categories"] as Categories;
                Coureses = parameters["Coureses"] as Coures;
                chapterList = await _clientAPI.GetChapter();
                LvLanguage = new ObservableCollection<string>(chapterList.Languages);
                LvWordCount= new ObservableCollection<WordCount>(chapterList.WordCount);
                SelectedLanguageIndex = 0;
                LeftSideLanguage = "Arabic";
                LvSurah = new ObservableCollection<Surah>(chapterList.Surahs);
                var countLv=LvSurah.Count;
                var s = LvSurah.Where(x => x.ID == Quizzesdata.LearnID).FirstOrDefault();
                var indexOfSurah = LvSurah.IndexOf(s);
                SelectedIndexLvSurah = indexOfSurah;
                wordCountVisible = true;
                errorLblVisible = false;
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    // You're on a phone
                    if (IsASmallDevice())
                        QuizzesTypesHeight = "150";
                    else
                        QuizzesTypesHeight = "160";

                }
                else
                {
                    // You're on a tablet
                    QuizzesTypesHeight = "200";
                }

                QuizzesTypes.Add(new QuizzesType { Id = 0, Name = "Shuffle Questions", BackgroundColor = "#868e96", IconImage = "close.png", IsEnables = false, IsVisibles = true, LblNote = "This Quiz can only be played with shuffle enabled" });
                QuizzesTypes.Add(new QuizzesType { Id = 1, Name = "Remove Duplicates", BackgroundColor = "#868e96", IconImage = "close.png", IsEnables = false, IsVisibles = true, LblNote = "This Quiz can only be played in Test Mode" });
                QuizzesTypes.Add(new QuizzesType { Id = 2, Name = "Switch Order", BackgroundColor = "#868e96", IconImage = "close.png", IsEnables = false, IsVisibles = true, LblNote = "Switch Order" });


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
            }
        }
        //Close Quizze click
        public DelegateCommand CloseQuizze => new DelegateCommand(async () =>
        {
            try
            {
                var parameters = new NavigationParameters();
                parameters.Add("PlayTab", Quizzesdata);
                parameters.Add("Categories", Category);
                parameters.Add("Coureses", Coureses);
                //parameters.Add("SubCategories", LvSelectedSubCategoryName);

                await _navigationService.GoBackAsync(parameters);
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

    }
}