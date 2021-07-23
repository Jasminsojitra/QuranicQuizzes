using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class LearnTabQuestionSelectionViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IClientAPI _clientAPI;
        public Command<object> StudyCommmand { get; set; }
        //public Command<object> GenrateCommmand { get; set; }
        public Command<object> SelectedQuizes { get; set; }
        public Command<object> InfoClickCommand { get; set; }

        public LearnTabQuestionSelectionViewModel(INavigationService navigationService, IClientAPI clientAPI) : base(navigationService)
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
                    await MaterialDialog.Instance.AlertAsync(message: "This removes answers as you go along and provides results at the end only", configuration: alertDialogConfiguration);
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
                    UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                    bool qShuffle = false;
                    bool removeDuplicates = false;
                    bool testMode = false;

                    foreach (var item in QuizzesTypes)
                    {
                        if (item.IconImage.Equals("done.png"))
                        {
                            if (item.Name.Equals("Shuffle Questions"))
                                qShuffle = true;
                            if (item.Name.Equals("Remove Duplicates"))
                                removeDuplicates = true;
                            if (item.Name.Equals("Test Mode"))
                            {
                                //if(GlobalConst.isCourse)
                                //    testMode = false;
                                //else
                                testMode = true;
                                GlobalConst.istestMode = true;
                            }

                        }
                    }
                    
                    
                        var parameters = new NavigationParameters();
                        //parameters.Add("SessionInfo", SessionInfo);
                        parameters.Add("PlayTab", Quizzesdata);
                        parameters.Add("Shuffle", qShuffle.ToString());
                        parameters.Add("RemoveDuplicates", removeDuplicates.ToString());
                        parameters.Add("Categories", Category);
                        parameters.Add("Coureses", Coureses);
                        //await _navigationService.GoBackAsync();

                        await _navigationService.NavigateAsync(nameof(LearnTabFlipPage), parameters);
                    
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

        Quizze Quizzesdata = new Quizze();
        CheckSession SessionInfo = new CheckSession();
        Categories Category = new Categories();
        Coures Coureses = new Coures();
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
                //var chapterList= await _clientAPI.GetChapter();

                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        // You're on a phone
                        if (IsASmallDevice())
                            QuizzesTypesHeight = "100";
                        else
                            QuizzesTypesHeight = "110";

                    }
                    else
                    {
                        // You're on a tablet
                        QuizzesTypesHeight = "120";
                    }

                    QuizzesTypes.Add(new QuizzesType { Id = 0, Name = "Shuffle Questions", BackgroundColor = "#868e96", IconImage = "close.png", IsEnables = false, IsVisibles = true, LblNote = "This Quiz can only be played with shuffle enabled" });
                    QuizzesTypes.Add(new QuizzesType { Id = 1, Name = "Remove Duplicates", BackgroundColor = "#868e96", IconImage = "close.png", IsEnables = false, IsVisibles = true, LblNote = "This Quiz can only be played in Test Mode" });


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