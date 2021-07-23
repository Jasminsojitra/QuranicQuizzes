using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using QuranicQuizzes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QuranicQuizzes.Views
{
    public partial class LearTabFlipWebViewPage : ContentPage
    {
        private LearTabFlipWebViewPageViewModel vm;

        public LearTabFlipWebViewPage()
        {
            InitializeComponent();
            vm = BindingContext as LearTabFlipWebViewPageViewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            LearnWebView.Navigated += LearnWebView_Navigated;
        }
        void Handle_CloseTapped(object sender, System.EventArgs e)
        {
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
        }
        private void LearnWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                switch (e.Result)
                {
                    case WebNavigationResult.Cancel:
                        UserDialogs.Instance.HideLoading();
                        break;
                    case WebNavigationResult.Failure:
                        LearnWebView.IsVisible = false;
                        UserDialogs.Instance.HideLoading();
                        //ErrorMsg.IsVisible = true;
                        break;
                    case WebNavigationResult.Success:
                        LearnWebView.IsVisible = true;
                        UserDialogs.Instance.HideLoading();
                        //ErrorMsg.IsVisible = false;
                        break;
                    case WebNavigationResult.Timeout:
                        UserDialogs.Instance.HideLoading();
                        break;
                    default:
                        break;
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


    }
}
