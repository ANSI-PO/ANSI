using Database.Infrastructure;
using Domain.Infrastructure;
using Database.Repository;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Domain.Services;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Models;
using System.Linq.Expressions;

namespace DomainIntegrationTests
{
    public class DishSelectorTests
    {
        private readonly DomainIntegrationTestFixture _fixture = new DomainIntegrationTestFixture();


        
        [Fact]
        public async Task QuestionsReturnsListsOfAnswers()
        {
            var sut = _fixture.CreateSut();
            var temp = await sut.GetFirstQuestion();
            var temp2 = await sut.GetSecondQuestion(temp);
            var temp3 = await sut.GetThirdQuestion(temp2);
            var temp4 = await sut.GetFourthQuestion(temp3);
            var response = await sut.GetFifthQuestion(temp4);

            temp.Should().HaveCountGreaterThan(0);
            temp2.Should().HaveCountGreaterThan(0);
            temp3.Should().HaveCountGreaterThan(0);
            temp4.Should().HaveCountGreaterThan(0);
            response.Should().ContainItemsAssignableTo<QuestionModel>();
            response.Should().NotBeNull();
            response[0].QuestionName.Should().Be("Jak¹ kuchnie wybierasz? ");
            response[1].QuestionName.Should().Be("Ile czasu masz na przygotowanie posi³ku? ");
            response[2].QuestionName.Should().Be("Wybierz trudnoœæ przygotowywanego posi³ku: ");
            response[3].QuestionName.Should().Be("Wybierz podstawowy sk³adkik do posi³ku: ");
            response[4].QuestionName.Should().Be("Wybierz kategoriê sk³adników: ");
        }
        [Fact]
        public async Task GetDishTest()
        {
            var sut = _fixture.CreateSut();
            
            List<QuestionModel> models = new List<QuestionModel>();
            models.Add(
                new QuestionModel { 
                    QuestionId = 1,
                    QuestionName = "MAIN_CATEGORY", 
                    Answers = new List<AnswerModel> { 
                        new AnswerModel
                        {
                            AnswerId = 1,
                            AnswerName = "Chiñska",
                            isPicked = true
                        }
                        } });

            models.Add(
                new QuestionModel
                {
                    QuestionId = 2,
                    QuestionName = "MAKE_TIME_MIN",
                    Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            AnswerId = 1,
                            AnswerName = "30",
                            isPicked = true
                        }
                        }
                });
            models.Add(
                new QuestionModel
                {
                    QuestionId = 3,
                    QuestionName = "PREPARATION_DIFFICULTY",
                    Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            AnswerId = 1,
                            AnswerName = "³atwy",
                            isPicked = true
                        }
                        }
                });
            models.Add(
                new QuestionModel
                {
                    QuestionId = 4,
                    QuestionName = "INGREDIENTS_CATEGORY",
                    Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            AnswerId = 1,
                            AnswerName = "Oba",
                            isPicked = true
                        }
                        }
                });
            models.Add(
                new QuestionModel
                {
                    QuestionId = 5,
                    QuestionName = "INGREDIENTS_TAGS",
                    Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            AnswerId = 1,
                            AnswerName = "Wieprzowina",
                            isPicked = true
                        }
                        }
                });

            var response = await sut.GetDish(models);

            response.Should().NotBeNull();
        }


        private class DomainIntegrationTestFixture
        {
            public IDishSelectorService CreateSut()
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
                return serviceCollection.SetupDomainDi();
            }
        }
    }
}