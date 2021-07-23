using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using QuranicQuizzes.ViewModels;
using Xamarin.Forms;

namespace QuranicQuizzes.Views
{
    public partial class LearnTabFlipPage : ContentPage
    {
        private LearnTabFlipPageViewModel vm;

        public LearnTabFlipPage()
        {
            InitializeComponent();
            vm = BindingContext as LearnTabFlipPageViewModel;
        }

        private void flipItButton_OnClicked(object sender, EventArgs e)
        {
            XFFlipViewControl1.IsFlipped = !XFFlipViewControl1.IsFlipped;
        }

        private async void Reset_OnClicked(object sender, EventArgs e)
        {
            try
            {
                XFFlipViewControl1.IsVisible = false;
                XFFlipViewControl1.FlipFromBackToFront();
                await vm.ResetTap();
                XFFlipViewControl1.IsVisible = true;
            }
            catch (Exception ex)
            {

            }
        }

        private async void PlayAgain_OnClicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait...", MaskType.Gradient);
                XFFlipViewControl1.IsVisible = false;
                XFFlipViewControl1.FlipFromBackToFront();
                await vm.SetQuestionData();
                XFFlipViewControl1.IsVisible = true;
            }
            catch (Exception ex)
            {

            }
        }
        
        private void GotIt_OnClicked(object sender, EventArgs e)
        {
            vm.gotIt();
            XFFlipViewControl1.IsFlipped = !XFFlipViewControl1.IsFlipped;
        }

        private void StudyAgain_OnClicked(object sender, EventArgs e)
        {
            vm.StudyCount++;
            XFFlipViewControl1.IsFlipped = !XFFlipViewControl1.IsFlipped;
            vm.studyAgain();
        }

        protected override bool OnBackButtonPressed()
        {
            vm.CloseTap.Execute();
            return true;
        }
    }
}