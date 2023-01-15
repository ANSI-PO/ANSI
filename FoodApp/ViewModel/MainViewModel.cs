using FoodApp.Commands;
using FoodApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Domain.Services;
using Database.Models;

namespace FoodApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private bool isViewVisible = true;
        private string _newWindowName;
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

        public ICommand ExecuteStart { get; }

        public MainViewModel()
        {

            ExecuteStart = new ViewModelCommand(StartChoice);


        }

        private void StartChoice(object obj)
        {

            IsViewVisible = false;
            var questionView = new QuestionView();
            App.Current.Windows[0].Close();
            questionView.Show();

        }


    }
}
