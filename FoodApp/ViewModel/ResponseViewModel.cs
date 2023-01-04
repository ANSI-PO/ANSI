using FoodApp.Commands;
using FoodApp.ViewModel;
using FoodApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FrontEnd.Model;
using Domain.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace FoodApp.ViewModel
{
    public class ResponseViewModel : BaseViewModel
    {
        private bool isViewVisible = true;
        private string _newWindowName;
        private DishModel? response;
        private string messageToShow;
        private string additionalMessage;
        public string NewWindowName
        {
            get
            {
                return _newWindowName;
            }
            set
            {
                _newWindowName = value;
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return isViewVisible;
            }

            set
            {
                isViewVisible = value;
                OnPropertyChange(nameof(isViewVisible));
            }
        }
        public string MessageToShow
        {
            get
            {
                return messageToShow;
            }

            set
            {
                messageToShow = value;
                OnPropertyChange(nameof(messageToShow));
            }
        }

        public string AdditionalMessage
        {
            get
            {
                return additionalMessage;
            }

            set
            {
                additionalMessage = value;
                OnPropertyChange(nameof(additionalMessage));
            }
        }

        public DishModel? Response { get => response; set => response = value; }
        public ICommand goToMainView { get; }
        public ICommand NextTry { get; }
        public ICommand OpenLinkCommand { get; set; }


        public ResponseViewModel()
        {


            Messenger.Default.Register<DishModel?>(this, (responseReceived) =>
            {
                Response = responseReceived;
            });
            OpenLinkCommand = new ViewModelCommand(GoToRecipe, RecipeFound);
            goToMainView = new ViewModelCommand(GoToMainView);
            NextTry = new ViewModelCommand(GoToQuestionView);
        }

        private void GoToQuestionView(object obj)
        {
            IsViewVisible = false;
            var questionView = new QuestionView();
            App.Current.Windows[0].Close();
            questionView.Show();
        }

        private bool RecipeFound(object obj)
        {
            bool isValid;

            if (Response != null)
            {
                isValid = true;
                MessageToShow = Response.Value.DishName;
                AdditionalMessage = "Nice choice!";
            }
            else
            {
                isValid = false;
                MessageToShow = "Sorry,we were not able to find the recipe considering your answers";
                AdditionalMessage = " Try again!";
            }


            return isValid;
        }

        private void GoToRecipe(object obj)
        {

            var url = Response.Value.DishRecipeUrl.ToString();
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void GoToMainView(object obj)
        {

            IsViewVisible = false;
            var mainView = new MainView();
            App.Current.Windows[0].Close();
            mainView.Show();

        }
    }
}
