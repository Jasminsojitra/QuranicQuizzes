﻿using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using QuranicQuizzes.Helpers;
using QuranicQuizzes.Interfaces;
using QuranicQuizzes.Models;
using QuranicQuizzes.Views;
using Xamarin.Forms;

namespace QuranicQuizzes.ViewModels
{
    public class LearTabFlipWebViewPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IClientAPI _clientAPI;
        public Command<object> StudyCommmand { get; set; }
        public Command<object> FlashCardCommmand { get; set; }
        public LearTabFlipWebViewPageViewModel(INavigationService navigationService, IClientAPI clientAPI) : base(navigationService)
        {
            _navigationService = navigationService;
            _clientAPI = clientAPI;
            //StudyCommmand = new Command<object>(StudyCommmandClick, (x) => CanNavigate);
            //FlashCardCommmand = new Command<object>(FlashCardClick, (x) => CanNavigate);

        }

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

        private SubCategories _lvSelectedSubCategoryName { get; set; }
        public SubCategories LvSelectedSubCategoryName
        {
            get
            {
                return _lvSelectedSubCategoryName;

            }
            set
            {
                _lvSelectedSubCategoryName = value;
                RaisePropertyChanged(nameof(LvSelectedSubCategoryName));

            }
        }

        public async void FlashCardClick(object obj)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                var parameters = new NavigationParameters();
                parameters.Add("PlayTab", Quizzesdata);
                parameters.Add("Categories", Category);
                parameters.Add("SubCategories", LvSelectedSubCategoryName);
                GlobalConst.isLearnTabStudy = false;
                if (Quizzesdata.IsLearnTabFree)
                {
                    if (Quizzesdata.CategoryID == 2)
                        await _navigationService.NavigateAsync(nameof(LearnTabQuranQuestionSelectionPage), parameters);
                    else
                        await _navigationService.NavigateAsync(nameof(LearnTabQuestionSelectionPage), parameters);
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

        private string _SourceURL { get; set; }
        public string SourceURL
        {
            get => _SourceURL;
            set
            {
                _SourceURL = value;
                RaisePropertyChanged(nameof(SourceURL));
            }
        }

        public async void StudyCommmandClick(object obj)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                var parameters = new NavigationParameters();
                parameters.Add("PlayTab", Quizzesdata);
                parameters.Add("Categories", Category);
                parameters.Add("SubCategories", LvSelectedSubCategoryName);
                if (Quizzesdata.IsLearnTabFree)
                {
                    await _navigationService.NavigateAsync(nameof(LearnTabPage), parameters);
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

        Quizze Quizzesdata = new Quizze();
        CheckSession SessionInfo = new CheckSession();
        Categories Category = new Categories();
        Coures Coureses = new Coures();
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            try
            {
                UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                Quizzesdata = parameters["PlayTab"] as Quizze;
                //CheckSession SessionInfo = await _clientAPI.GetCheckSession(Quizzesdata.Id, GlobalConst.isCourse, 0);
                Category = parameters["Categories"] as Categories;
                Coureses = parameters["Coureses"] as Coures;
                //SourceURL = "http://quranicquizzes.com/" + "Quizzes/LearnApp/" + Quizzesdata.Id + "?" + GlobalConst.ApiUrlKey + "&iscourse=" + GlobalConst.isCourse;

                if (Quizzesdata.CategoryID == 2)
                    SourceURL = "http://QuranicQuizzes.com/FlashCard/Chapters/"+Quizzesdata.LearnID+"?isApp=true";

                else
                    SourceURL = "http://QuranicQuizzes.com/FlashCard/Index/"+Quizzesdata.Id+"?categoryID="+Category.ID+"&isApp=true";

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
    }
}
