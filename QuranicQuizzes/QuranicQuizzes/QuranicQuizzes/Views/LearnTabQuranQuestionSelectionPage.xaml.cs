using System;
using System.Collections.Generic;
using QuranicQuizzes.ViewModels;
using Xamarin.Forms;

namespace QuranicQuizzes.Views
{
    public partial class LearnTabQuranQuestionSelectionPage : ContentPage
    {
        private LearnTabQuranQuestionSelectionPageViewModel vm;

        public LearnTabQuranQuestionSelectionPage()
        {
            InitializeComponent();
            vm = BindingContext as LearnTabQuranQuestionSelectionPageViewModel;
        }

        void btnDropDownpickerHeader_Clicked(System.Object sender, System.EventArgs e)
        {
            pickerHeader.Focus();
        }

        void btnDropDownpickerStartVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            pickerStartVerbs.Focus();
        }

        void btnDropDownpickerEndVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            pickerEndVerbs.Focus();
        }

        void btnDropDownLanguagePicker_Clicked(System.Object sender, System.EventArgs e)
        {
            LanguagePicker.Focus();
        }
    }
}
