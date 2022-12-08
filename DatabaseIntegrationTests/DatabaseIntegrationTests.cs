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
        dish.MainCategory.Should().Be("Amerykańska");
        dish.PreparationDifficulty.Should().Be("łatwy");
        dish.IngredientsCategory.Should().Be("Mięso");
        dish.IngredientsTags.Should().Be("Wołowina");
    }

    [Fact]
    public async Task GivenValidData_WhenReducingRangeOfResultWithExpression_ShouldReturnMultipleResults()
    {
        //arrange
        var sut = _fixture.CreateSut();
        Expression<Func<Dish, bool>> input = x =>
            x.DishId != 332
            && x.MakeTimeMin >= 60
            && x.IngredientsCategory == "Mięso"
            && x.MainCategory == "Amerykańska"
            && x.PreparationDifficulty == "łatwy";

        var testConstraints = (DishModel dish) =>
            dish.DishId != 332
            && dish.MakeTimeMin >= 60
            && dish.IngredientsCategory == "Mięso"
            && dish.MainCategory == "Amerykańska"
            && dish.PreparationDifficulty == "łatwy";

        //act
        var result = (await sut.GetDishes(input)).ToArray();

        //assert
        result.Should().HaveCount(2);
        result.Should().Match(x => x.All(model => testConstraints(model)));
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForIngredientsCategory_ShouldReturnUniqueResults()
    {
        //arrange
        var sut = _fixture.CreateSut();

        //act
        var result = (await sut.GetUniqueIngredientsCategory()).ToArray();

        //assert
        result.Should().NotBeEmpty();
        result.Should().OnlyHaveUniqueItems();
        result.Should().Contain(new[] { "Mięso", "Oba", "Ryby" });
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForPreparationDifficulty_ShouldReturnUniqueResults()
    {
        //arrange
        var sut = _fixture.CreateSut();

        //act
        var result = (await sut.GetUniquePreparationDifficulty()).ToArray();

        //assert
        result.Should().NotBeEmpty();
        result.Should().OnlyHaveUniqueItems();
        result.Should().Contain(new[] { "łatwy", "średni", "trudny" });
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForMainCategories_ShouldReturnUniqueResults()
    {
        //arrange
        var sut = _fixture.CreateSut();

        //act
        var result = (await sut.GetUniqueMainCategories()).ToArray();

        //assert
        result.Should().NotBeEmpty();
        result.Should().OnlyHaveUniqueItems();
        result.Should().Contain(new[] { "Francuska", "Orientalna", "Polska" });
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForPreparationTime_ShouldReturnUniqueResults()
    {
        //arrange
        var sut = _fixture.CreateSut();

        //act
        var result = (await sut.GetUniquePreparationTime()).ToArray();

        //assert
        result.Should().NotBeEmpty();
        result.Should().OnlyHaveUniqueItems();
        result.Select(x => int.TryParse(x, out _)).Should().Match(seq => seq.All(b => b == true));
        result.Should().Contain(new[] { "15", "45", "70" });
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingForIngredientsTags_ShouldReturnUniqueResults()
    {
        //arrange
        var sut = _fixture.CreateSut();

        //act
        var result = (await sut.GetAvailableIngredientsTags()).ToArray();
        //make flatten list and generate values id hashes
        var customHashes = result
            .Select(x => x.Aggregate("", (accu, item) => accu + item.GetHashCode()));
        
        //assert
        result.Should().NotBeEmpty();
        customHashes.Should().OnlyHaveUniqueItems();
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