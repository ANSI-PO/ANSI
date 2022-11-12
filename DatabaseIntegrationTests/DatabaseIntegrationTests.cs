using System.Linq.Expressions;
using Database.Infrastructure;
using Database.Infrastructure.Exceptions;
using Database.Models;
using Database.Models.Database;
using Database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseIntegrationTests;

// integration tests for entire flow
public class DatabaseIntegrationTests
{
    private readonly DatabaseIntegrationTestFixture _fixture;

    public DatabaseIntegrationTests()
    {
        _fixture = new DatabaseIntegrationTestFixture();
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForSpecificDish_ShouldReturnResult()
    {
        //arrange
        var sut = _fixture.CreateSut();
        Expression<Func<Dish, bool>> input = x => x.DishId == 332;

        //act
        var result = (await sut.GetDishes(input)).ToArray();

        //assert
        result.Should().HaveCount(1);
        var dish = result.Single();

        dish.DishId.Should().Be(332);
        dish.DishName.Should().Be("Antrykot wołowy po amerykańsku");
        dish.MainCategory.Should().Be(MainCategoryTypes.American);
        dish.PreparationDifficulty.Should().Be(PreparationDifficultyTypes.Easy);
        dish.IngredientsCategory.Should().Be(IngredientsCategoryTypes.Meat);
        dish.IngredientsTags.Should().Be(IngredientsTagsTypes.Beef);
    }

    [Fact]
    public async Task GivenValidData_WhenReducingRangeOfResultWithExpression_ShouldReturnMultipleResults()
    {
        //arrange
        var sut = _fixture.CreateSut();
        Expression<Func<Dish, bool>> input = x =>
            x.DishId != 332
            && x.MakeTimeMin >= 60
            && x.IngredientsCategory == IngredientsCategoryTypes.Meat
            && x.MainCategory == MainCategoryTypes.American
            && x.PreparationDifficulty == PreparationDifficultyTypes.Easy;

        var testConstraints = (DishModel dish) =>
            dish.DishId != 332
            && dish.MakeTimeMin >= 60
            && dish.IngredientsCategory == IngredientsCategoryTypes.Meat
            && dish.MainCategory == MainCategoryTypes.American
            && dish.PreparationDifficulty == PreparationDifficultyTypes.Easy;

        //act
        var result = (await sut.GetDishes(input)).ToArray();

        //assert
        result.Should().HaveCount(2);
        result.Should().Match(x => x.All(model => testConstraints(model)));
    }

    [Fact]
    public async Task
        GivenInvalidData_WhenRequestIsTooComplicatedForEntityFramework_ShouldThrowDatabaseQueryExecutionException()
    {
        //arrange
        var sut = _fixture.CreateSut();
        Expression<Func<Dish, bool>> input = x =>
            x.DishName.StartsWith('a');


        //act
        Func<Task> act = () => sut.GetDishes(input);

        //assert
        await act.Should().ThrowExactlyAsync<DatabaseQueryExecutionException>()
            .WithMessage("Query was not able to execute. Please check your query and inner error message");
    }

    private class DatabaseIntegrationTestFixture
    {
        public IDishRepository CreateSut()
        {
            var dishRepository = BuildDiContainer()
                .BuildServiceProvider()
                .GetService<IDishRepository>();

            return dishRepository ?? throw new NullReferenceException("Unable to initialize container");
        }

        private static IServiceCollection BuildDiContainer()
        {
            const string connString = "server=localhost;port=3306;database=ANSI;uid=root;pwd=DevUserPassword";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            return serviceCollection.SetupDatabaseDi(connString);
        }
    }
}