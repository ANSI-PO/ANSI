using FoodApp.Commands;
using FoodApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Abstractions;
using Database;
using Microsoft.Extensions.DependencyInjection;
using FrontEnd.Infrastructures;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.ObjectModel;
using FrontEnd.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace FoodApp.ViewModel
{
    public class QuestionViewModel : BaseViewModel
    {

        private bool isViewVisible = true;
        private string _newWindowName;
        private string question = "";
        private static DomainIntegration domainIntegration = new DomainIntegration();
        private int questionNumber = 0;
        private string buttonContent = "";
        private int CheckedCount { get; set; } = 0;
        private int AnswerLength { get; set; }
        private List<QuestionModel> questionModels;
        private IDishSelectorService dishSelectorService;
        private DishModel? response;
        public ObservableCollection<CheckboxItem> CheckboxItems { get; set; }
        public ObservableCollection<RadioButtonItem> RadioButtonItems { get; set; }
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

        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;

                OnPropertyChange(nameof(question));
            }
        }

        public int QuestionNumber
        {
            get
            {
                return questionNumber;
            }
            set
            {
                questionNumber = value;

                OnPropertyChange(nameof(questionNumber));
            }
        }

        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            {
                buttonContent = value;

                OnPropertyChange(nameof(buttonContent));
            }
        }

        public List<QuestionModel> QuestionModels { get => questionModels; set => questionModels = value; }
        public IDishSelectorService DishSelectorService { get => dishSelectorService; set => dishSelectorService = value; }
        // commands
        public ICommand NextQuestion { get; }

        public ICommand Test2 { get; }
        public DishModel? Response { get => response; set => response = value; }

        public QuestionViewModel()
        {
            ButtonContent = "NEXT QUESTION";
            QuestionModels = new List<QuestionModel>();
            CheckboxItems = new ObservableCollection<CheckboxItem>();
            RadioButtonItems = new ObservableCollection<RadioButtonItem>();
            NextQuestion = new ViewModelCommand(GetNextQuestion, ValidateAnswer);
            CheckboxItems.CollectionChanged += CheckboxItems_CollectionChanged;
            GetFirstQuestion();
        }

        private bool ValidateAnswer(object obj)
        {
            bool validData = false;

            if (RadioButtonItems.Any())
            {

                foreach (var item in RadioButtonItems)
                {
                    if (item.IsSelected == true)
                    {
                        validData = true;
                        break;
                    }
                    else
                    {
                        validData = false;
                    }

                }

            }
            else if (CheckboxItems.Any())
            {

                foreach (var item in CheckboxItems)
                {
                    if (item.IsChecked == true)
                    {
                        validData = true;
                        break;
                    }
                    else
                    {
                        validData = false;
                    }

                }

            }
            else
            {
                validData = false;
            }


            return validData;
        }

        private async void GetFirstQuestion()
        {

            // Set the cointainer
            DishSelectorService = domainIntegration.CreateService();
            // Get the first question 
            QuestionModels = await DishSelectorService.GetFirstQuestion();
            Question = QuestionModels[QuestionNumber].QuestionName.ToString();
            foreach (var item in QuestionModels[QuestionNumber].Answers)
            {
                RadioButtonItems.Add(new RadioButtonItem { Label = item.AnswerName });
            }
            // Set the Question nubmer to navigate 
            QuestionNumber = 1;

        }

        private async void GetNextQuestion(object obj)
        {

            switch (QuestionNumber)
            {
                // Get the second question
                case 1:
                    AnswerLength = 0;
                    foreach (var item in RadioButtonItems)
                    {

                        QuestionModels[QuestionNumber - 1].Answers[AnswerLength].isPicked = item.IsSelected;
                        AnswerLength++;
                    }
                    RadioButtonItems.Clear();
                    if (QuestionModels != null)
                    {
                        QuestionModels = await DishSelectorService.GetSecondQuestion(QuestionModels);
                        Question = QuestionModels[QuestionNumber].QuestionName.ToString();
                        // Sort the list from lowest to biggest value of time required to prepare a dish
                        QuestionModels[QuestionNumber].Answers = QuestionModels[QuestionNumber].Answers.OrderBy(x => int.Parse(x.AnswerName)).ToList();
                        var amountOfAnswers = QuestionModels[QuestionNumber].Answers.Count;
                        for (int i = 0; i < amountOfAnswers; i++)
                        {
                            if (i == 0)
                            {
                                RadioButtonItems.Add(new RadioButtonItem { Label = "0-" + QuestionModels[QuestionNumber].Answers[i].AnswerName + " min" });
                            }
                            else
                            {
                                RadioButtonItems.Add(new RadioButtonItem { Label = QuestionModels[QuestionNumber].Answers[i - 1].AnswerName + "-" + QuestionModels[QuestionNumber].Answers[i].AnswerName + " min" });
                            }
                        }
                        // Set the Question nubmer to navigate 
                        QuestionNumber = 2;
                    }
                    break;
                case 2:
                    AnswerLength = 0;
                    foreach (var item in RadioButtonItems)
                    {

                        QuestionModels[QuestionNumber - 1].Answers[AnswerLength].isPicked = item.IsSelected;
                        AnswerLength++;
                    }
                    RadioButtonItems.Clear();
                    if (QuestionModels != null)
                    {
                        QuestionModels = await DishSelectorService.GetThirdQuestion(QuestionModels);
                        Question = QuestionModels[QuestionNumber].QuestionName.ToString();
                        foreach (var item in QuestionModels[QuestionNumber].Answers)
                        {
                            RadioButtonItems.Add(new RadioButtonItem { Label = item.AnswerName });
                        }
                        // Set the Question nubmer to navigate 
                        QuestionNumber = 3;
                    }
                    break;
                case 3:

                    AnswerLength = 0;
                    foreach (var item in RadioButtonItems)
                    {

                        QuestionModels[QuestionNumber - 1].Answers[AnswerLength].isPicked = item.IsSelected;
                        AnswerLength++;
                    }
                    RadioButtonItems.Clear();
                    if (QuestionModels != null)
                    {
                        QuestionModels = await DishSelectorService.GetFourthQuestion(QuestionModels);
                        Question = QuestionModels[QuestionNumber].QuestionName.ToString();
                        foreach (var item in QuestionModels[QuestionNumber].Answers)
                        {
                            RadioButtonItems.Add(new RadioButtonItem { Label = item.AnswerName });
                        }
                        // Set the Question nubmer to navigate 
                        QuestionNumber = 4;
                    }
                    break;
                case 4:

                    AnswerLength = 0;
                    foreach (var item in RadioButtonItems)
                    {

                        QuestionModels[QuestionNumber - 1].Answers[AnswerLength].isPicked = item.IsSelected;
                        AnswerLength++;
                    }
                    RadioButtonItems.Clear();
                    if (QuestionModels != null)
                    {
                        QuestionModels = await DishSelectorService.GetFifthQuestion(QuestionModels);
                        Question = QuestionModels[QuestionNumber].QuestionName.ToString();
                        foreach (var item in QuestionModels[QuestionNumber].Answers)
                        {
                            CheckboxItems.Add(new CheckboxItem { Label = item.AnswerName });
                        }
                        // Set the Question nubmer to navigate 
                        QuestionNumber = 5;
                        ButtonContent = "Get dish!";
                    }
                    break;

                case 5:
                    AnswerLength = 0;
                    foreach (var item in CheckboxItems)
                    {

                        QuestionModels[QuestionNumber - 1].Answers[AnswerLength].isPicked = item.IsChecked;
                        AnswerLength++;

                    }
                    // create response view 


                    CheckboxItems.Clear();
                    Response = await DishSelectorService.GetDish(QuestionModels);
                    // create send message with response
                    var responseView = new ResponseView();
                    Messenger.Default.Send(Response);
                    //change view 
                    IsViewVisible = false;
                    App.Current.Windows[0].Close();
                    responseView.Show();

                    break;
                default:
                    // code block
                    break;
            }



        }
        private void CheckboxItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var item = e.NewItems[0] as CheckboxItem;
                item.PropertyChanged += Item_PropertyChanged;
            }
        }
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                var item = sender as CheckboxItem;
                if (item.IsChecked)
                {
                    CheckedCount++;
                }
                else
                {
                    CheckedCount--;
                }

                if (CheckedCount > 3)
                {

                    // Set the IsChecked property back to false
                    item.IsChecked = false;

                    // Show an error message or do something else
                    MessageBox.Show("You can only select up to 3 options!");
                }
            }
        }

        private class DomainIntegration
        {
            public IDishSelectorService CreateService()
            {
                var dishService = BuildDiContainer()
                    .BuildServiceProvider()
                    .GetService<IDishSelectorService>();

                return dishService;
            }

            private static IServiceCollection BuildDiContainer()
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddLogging();
                return serviceCollection.SetupFrontEndDi();
            }
        }

    }
}
